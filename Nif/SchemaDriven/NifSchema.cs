using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;

/// <summary>
/// Represents a NIF schema loaded from nif.xml. The schema describes blocks, fields and
/// compound types so that binary NIF data can be parsed without hardcoding formats.
/// </summary>
public sealed class NifSchema
{
    public Dictionary<string, NifBlockDef> Blocks { get; } = new(StringComparer.Ordinal);
    public Dictionary<string, NifTypeDef> Types { get; } = new(StringComparer.Ordinal);

    public NifBlockDef GetBlock(string name)
    {
        if (!Blocks.TryGetValue(name, out var def))
            throw new KeyNotFoundException($"Block definition not found: {name}");
        return def;
    }

    public NifTypeDef GetType(string name)
    {
        if (!Types.TryGetValue(name, out var def))
            throw new KeyNotFoundException($"Type definition not found: {name}");
        return def;
    }

    /// <summary>
    /// Loads a schema from nif.xml. Only the common attributes that are useful to drive a
    /// binary reader are captured (name, inherit, type, ver1/ver2, arrays, conditions).
    /// </summary>
    public static NifSchema Load(string xmlPath)
    {
        if (!File.Exists(xmlPath))
            throw new FileNotFoundException($"Schema file not found: {xmlPath}");

        var schema = new NifSchema();
        var doc = XDocument.Load(xmlPath);

        foreach (var compound in doc.Descendants("compound"))
        {
            var def = ParseTypeDef(compound);
            schema.Types[def.Name] = def;
        }

        foreach (var obj in doc.Descendants("niobject"))
        {
            var def = ParseBlockDef(obj);
            schema.Blocks[def.Name] = def;
        }

        return schema;
    }

    private static NifBlockDef ParseBlockDef(XElement element)
    {
        var def = new NifBlockDef
        {
            Name = element.Attribute("name")?.Value ?? string.Empty,
            BaseName = element.Attribute("inherit")?.Value,
            VersionMin = ParseVersion(element.Attribute("ver1")?.Value),
            VersionMax = ParseVersion(element.Attribute("ver2")?.Value)
        };

        foreach (var fieldElem in element.Elements("field"))
        {
            def.Fields.Add(ParseField(fieldElem));
        }

        return def;
    }

    private static NifTypeDef ParseTypeDef(XElement element)
    {
        var def = new NifTypeDef
        {
            Name = element.Attribute("name")?.Value ?? string.Empty,
            BaseName = element.Attribute("inherit")?.Value,
            VersionMin = ParseVersion(element.Attribute("ver1")?.Value),
            VersionMax = ParseVersion(element.Attribute("ver2")?.Value)
        };

        foreach (var fieldElem in element.Elements("field"))
        {
            def.Fields.Add(ParseField(fieldElem));
        }

        return def;
    }

    private static NifFieldDef ParseField(XElement element)
    {
        string typeName = element.Attribute("type")?.Value ?? string.Empty;
        string countExpr = element.Attribute("arr1")?.Value ?? element.Attribute("arr2")?.Value ?? string.Empty;
        string? cond = element.Attribute("cond")?.Value;

        return new NifFieldDef
        {
            Name = element.Attribute("name")?.Value ?? string.Empty,
            TypeName = typeName,
            CountExpr = countExpr,
            ConditionExpr = cond,
            VersionMin = ParseVersion(element.Attribute("ver1")?.Value),
            VersionMax = ParseVersion(element.Attribute("ver2")?.Value),
            IsPointer = LooksLikePointer(typeName)
        };
    }

    private static bool LooksLikePointer(string typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
            return false;

        return typeName.StartsWith("Ref", StringComparison.OrdinalIgnoreCase)
            || typeName.StartsWith("Ptr", StringComparison.OrdinalIgnoreCase)
            || typeName.StartsWith("Link", StringComparison.OrdinalIgnoreCase)
            || typeName.EndsWith("*", StringComparison.Ordinal)
            || typeName.Equals("Ref", StringComparison.OrdinalIgnoreCase)
            || typeName.Equals("Pointer", StringComparison.OrdinalIgnoreCase);
    }

    private static uint ParseVersion(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return 0;

        if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
        {
            if (uint.TryParse(value.AsSpan(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out uint hex))
                return hex;
        }

        if (uint.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out uint dec))
            return dec;

        return 0;
    }
}

public sealed class NifBlockDef
{
    public string Name { get; set; } = string.Empty;
    public string? BaseName { get; set; }
    public uint VersionMin { get; set; }
    public uint VersionMax { get; set; }
    public List<NifFieldDef> Fields { get; } = new();
}

public sealed class NifFieldDef
{
    public string Name { get; set; } = string.Empty;
    public string TypeName { get; set; } = string.Empty;
    public string CountExpr { get; set; } = string.Empty;
    public string? ConditionExpr { get; set; }
    public uint VersionMin { get; set; }
    public uint VersionMax { get; set; }
    public bool IsPointer { get; set; }
}

public sealed class NifTypeDef
{
    public string Name { get; set; } = string.Empty;
    public string? BaseName { get; set; }
    public uint VersionMin { get; set; }
    public uint VersionMax { get; set; }
    public List<NifFieldDef> Fields { get; } = new();
}
