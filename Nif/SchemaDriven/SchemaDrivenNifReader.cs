using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// Schema-driven NIF reader that uses nif.xml to discover block layout at runtime.
/// </summary>
public sealed class SchemaDrivenNifReader
{
    private readonly NifSchema _schema;

    public SchemaDrivenNifReader(NifSchema schema)
    {
        _schema = schema;
    }

    public NifFile Read(string path)
    {
        using var fs = File.OpenRead(path);
        using var br = new BinaryReader(fs);

        var header = NifHeader.Read(br);
        var typeNames = NifTypeDictionary.ReadTypeNames(br, header);
        NifTypeDictionary.AlignTo4Bytes(br);
        var typeIndex = NifBlockIndexTable.ReadBlockTypeIndices(br, header, typeNames);

        // String palette (and optional groups) are present in most NIF versions and we need to advance past them
        NifStringPalette.ReadStrings(br);
        NifGroupList.Read(br);

        var file = new NifFile { Header = header };

        for (int i = 0; i < header.NumBlocks; i++)
        {
            string typeName = SafeGet(typeNames, typeIndex, i) ?? "NiUnknown";
            NifBlockDef? def = _schema.Blocks.TryGetValue(typeName, out var found) ? found : null;
            def ??= new NifBlockDef { Name = typeName };

            var blockCtx = new NifReadContext(header);
            var instance = new NifBlockInstance
            {
                Index = i,
                TypeName = typeName,
                Definition = def
            };

            foreach (var field in NifDefinitionHelpers.EnumerateWithBases(_schema, def))
            {
                if (!IsFieldInVersion(field, header.Version))
                    continue;

                if (!NifExpressionEvaluator.EvaluateCondition(field.ConditionExpr, blockCtx))
                    continue;

                object? value = ReadField(br, field, blockCtx);
                instance.Fields[field.Name] = value;
                blockCtx.SetValue(field.Name, value);
            }

            file.Blocks.Add(instance);
        }

        FixupReferences(file);
        return file;
    }

    private static string? SafeGet(string[] names, int[] indices, int i)
    {
        if (i < 0 || i >= indices.Length)
            return null;
        int idx = indices[i];
        if (idx < 0 || idx >= names.Length)
            return null;
        return names[idx];
    }

    private object? ReadField(BinaryReader br, NifFieldDef field, NifReadContext ctx)
    {
        int count = string.IsNullOrWhiteSpace(field.CountExpr)
            ? 1
            : NifExpressionEvaluator.EvaluateCount(field.CountExpr, ctx);

        // Primitive types
        if (IsPrimitive(field.TypeName))
        {
            if (count == 1)
                return ReadPrimitive(br, field.TypeName);

            var list = new object?[count];
            for (int i = 0; i < count; i++)
                list[i] = ReadPrimitive(br, field.TypeName);
            return list;
        }

        // Compound type
        if (_schema.Types.TryGetValue(field.TypeName, out var typeDef))
        {
            if (count == 1)
                return ReadCompound(br, typeDef, ctx);

            var list = new object?[count];
            for (int i = 0; i < count; i++)
                list[i] = ReadCompound(br, typeDef, ctx);
            return list;
        }

        // Pointer / reference
        if (field.IsPointer)
        {
            if (count == 1)
                return br.ReadInt32();

            var arr = new int[count];
            for (int i = 0; i < count; i++)
                arr[i] = br.ReadInt32();
            return arr;
        }

        // Unknown type: try to skip using length prefix if present
        return ReadPrimitive(br, field.TypeName);
    }

    private object? ReadCompound(BinaryReader br, NifTypeDef typeDef, NifReadContext parentCtx)
    {
        var ctx = parentCtx.CreateChild();
        var values = new Dictionary<string, object?>(StringComparer.Ordinal);

        foreach (var field in EnumerateTypeFields(typeDef))
        {
            if (!IsFieldInVersion(field, parentCtx.Header.Version))
                continue;

            if (!NifExpressionEvaluator.EvaluateCondition(field.ConditionExpr, ctx))
                continue;

            object? value = ReadField(br, field, ctx);
            values[field.Name] = value;
            ctx.SetValue(field.Name, value);
        }

        return values;
    }

    private IEnumerable<NifFieldDef> EnumerateTypeFields(NifTypeDef def)
    {
        var stack = new Stack<NifTypeDef>();
        var current = def;
        while (current != null)
        {
            stack.Push(current);
            if (current.BaseName != null && _schema.Types.TryGetValue(current.BaseName, out var baseDef))
                current = baseDef;
            else
                current = null;
        }

        foreach (var d in stack.Reverse())
        {
            foreach (var f in d.Fields)
                yield return f;
        }
    }

    private static bool IsFieldInVersion(NifFieldDef field, uint version)
    {
        if (field.VersionMin != 0 && version < field.VersionMin)
            return false;
        if (field.VersionMax != 0 && version > field.VersionMax)
            return false;
        return true;
    }

    private static bool IsPrimitive(string typeName)
    {
        return typeName switch
        {
            "byte" or "ubyte" or "char" or "uint8" or "sbyte" or "short" or "ushort" or "int" or "uint" or "uint32" or "float" or "double" or "bool" or "string" => true,
            _ => false
        };
    }

    private object? ReadPrimitive(BinaryReader br, string typeName)
    {
        switch (typeName)
        {
            case "byte":
            case "ubyte":
            case "char":
            case "uint8":
                return br.ReadByte();
            case "sbyte":
                return br.ReadSByte();
            case "short":
                return br.ReadInt16();
            case "ushort":
                return br.ReadUInt16();
            case "int":
            case "long":
            case "uint32":
                return br.ReadInt32();
            case "uint":
                return br.ReadUInt32();
            case "float":
                return br.ReadSingle();
            case "double":
                return br.ReadDouble();
            case "bool":
                return br.ReadByte() != 0;
            case "string":
                return NifString.ReadSizedString(br);
            default:
                return br.ReadInt32();
        }
    }

    private void FixupReferences(NifFile file)
    {
        foreach (var block in file.Blocks)
        {
            foreach (var field in NifDefinitionHelpers.EnumerateWithBases(_schema, block.Definition))
            {
                if (!field.IsPointer || !block.Fields.TryGetValue(field.Name, out var value))
                    continue;

                if (value is int idx)
                {
                    block.Fields[field.Name] = Resolve(idx, file);
                }
                else if (value is int[] arr)
                {
                    var resolved = new NifBlockInstance?[arr.Length];
                    for (int i = 0; i < arr.Length; i++)
                        resolved[i] = Resolve(arr[i], file);
                    block.Fields[field.Name] = resolved;
                }
                else if (value is object?[] objArr)
                {
                    var resolved = new NifBlockInstance?[objArr.Length];
                    for (int i = 0; i < objArr.Length; i++)
                        resolved[i] = objArr[i] is int id ? Resolve(id, file) : null;
                    block.Fields[field.Name] = resolved;
                }
            }
        }
    }

    private static NifBlockInstance? Resolve(int idx, NifFile file)
    {
        if (idx < 0 || idx >= file.Blocks.Count)
            return null;
        return file.Blocks[idx];
    }
}
