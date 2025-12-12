using System;
using System.Collections.Generic;
using System.IO;
using OpenTK.Mathematics;

/// <summary>
/// Loader for Civilization IV (Gamebryo 20.0.0.4) NIF files.
/// The format omits several header tables; see research/block-index-notes.md for details.
/// </summary>
public sealed class Civ4NifLoader
{
    private static readonly bool TraceBlocks = Environment.GetEnvironmentVariable("NIF_TRACE_BLOCKS") == "1";

    public Model LoadModel(string path, bool createGpuMeshes = true, bool bakeTransforms = true)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException("NIF file not found.", path);

        var context = ParseFile(path);
        string contentDir = Path.GetDirectoryName(path) ?? string.Empty;
        return BuildModel(context, contentDir, createGpuMeshes, bakeTransforms);
    }

    private static Model BuildModel(NifContext context, string contentDir, bool createGpuMeshes, bool bakeTransforms)
    {
        var model = new Model();
        bool addedMesh = false;
        Dictionary<int, TransformData>? worldTransforms = bakeTransforms ? BuildWorldTransforms(context) : null;

        for (int i = 0; i < context.Blocks.Length; i++)
        {
            Vector3[] vertices;
            Vector3[] normals;
            Vector2[][] uvSets;
            Triangle[] triangles;
            TransformData transform;
            List<int> propertyRefs;

            switch (context.Blocks[i])
            {
                case NiTriShapeBlock tri:
                    var meshData = context.GetBlock<NiTriShapeDataBlock>(tri.DataRef);
                    if (meshData == null || meshData.Vertices.Length == 0 || meshData.Triangles.Length == 0)
                        continue;

                    vertices = meshData.Vertices;
                    normals = meshData.Normals;
                    uvSets = meshData.UVSets;
                    triangles = meshData.Triangles;
                    transform = bakeTransforms && worldTransforms != null && worldTransforms.TryGetValue(i, out var triTransform)
                        ? triTransform
                        : TransformData.Identity;
                    propertyRefs = tri.Properties;
                    break;

                case NiTriStripsBlock strips:
                    var stripData = context.GetBlock<NiTriStripsDataBlock>(strips.DataRef);
                    if (stripData == null || stripData.Vertices.Length == 0 || !stripData.HasPoints)
                        continue;

                    vertices = stripData.Vertices;
                    normals = stripData.Normals;
                    uvSets = stripData.UVSets;
                    triangles = ConvertStripsToTriangles(stripData);
                    if (triangles.Length == 0)
                        continue;

                    transform = bakeTransforms && worldTransforms != null && worldTransforms.TryGetValue(i, out var stripTransform)
                        ? stripTransform
                        : TransformData.Identity;
                    propertyRefs = strips.Properties;
                    break;

                default:
                    continue;
            }

            if (!createGpuMeshes)
            {
                addedMesh = true;
                continue;
            }

            Mesh mesh = BuildMesh(vertices, normals, uvSets, triangles, transform, bakeTransforms);
            Material mat = BuildMaterial(context, propertyRefs, contentDir);
            model.AddMesh(mesh, mat);
            addedMesh = true;
        }

        if (!addedMesh)
            throw new InvalidOperationException("The NIF file did not contain any drawable NiTriShape blocks.");

        return model;
    }

    private static Dictionary<int, TransformData> BuildWorldTransforms(NifContext context)
    {
        var transforms = new Dictionary<int, TransformData>();
        var parentLookup = new Dictionary<int, int>();
        var nodeIndices = new List<int>();

        for (int i = 0; i < context.Blocks.Length; i++)
        {
            if (context.Blocks[i] is not NiNodeBlock node)
                continue;

            nodeIndices.Add(i);
            foreach (var child in node.Children)
            {
                if (child < 0)
                    continue;
                if (!parentLookup.ContainsKey(child))
                    parentLookup[child] = i;
            }
        }

        var visited = new HashSet<int>();
        bool visitedRoot = false;

        foreach (int nodeIndex in nodeIndices)
        {
            if (parentLookup.ContainsKey(nodeIndex))
                continue;

            TraverseNode(nodeIndex, TransformData.Identity, transforms, context, visited);
            visitedRoot = true;
        }

        if (!visitedRoot)
        {
            foreach (int nodeIndex in nodeIndices)
                TraverseNode(nodeIndex, TransformData.Identity, transforms, context, visited);
        }

        return transforms;
    }

    private static void TraverseNode(
        int nodeIndex,
        TransformData parentTransform,
        Dictionary<int, TransformData> transforms,
        NifContext context,
        HashSet<int> visited)
    {
        if (nodeIndex < 0 || nodeIndex >= context.Blocks.Length)
            return;

        if (context.Blocks[nodeIndex] is not NiNodeBlock node)
            return;

        if (!visited.Add(nodeIndex))
            return;

        var local = TransformData.FromComponents(node.Translation, node.Rotation, node.Scale);
        var world = TransformData.Combine(parentTransform, local);
        transforms[nodeIndex] = world;

        foreach (int childIndex in node.Children)
        {
            if (childIndex < 0 || childIndex >= context.Blocks.Length)
                continue;

            switch (context.Blocks[childIndex])
            {
                case NiNodeBlock:
                    TraverseNode(childIndex, world, transforms, context, visited);
                    break;
                case NiTriShapeBlock childTri:
                    var triLocal = TransformData.FromComponents(childTri.Translation, childTri.Rotation, childTri.Scale);
                    transforms[childIndex] = TransformData.Combine(world, triLocal);
                    break;
                case NiTriStripsBlock childStrips:
                    var stripsLocal = TransformData.FromComponents(childStrips.Translation, childStrips.Rotation, childStrips.Scale);
                    transforms[childIndex] = TransformData.Combine(world, stripsLocal);
                    break;
            }
        }
    }

    private static Mesh BuildMesh(Vector3[] vertices, Vector3[] normals, Vector2[][] uvSets, Triangle[] triangles, TransformData transform, bool bakeTransforms)
    {
        float[] positions = bakeTransforms
            ? FlattenVectors(TransformPositions(vertices, transform))
            : FlattenVectors(vertices);
        float[] normalValues = normals.Length > 0
            ? FlattenVectors(bakeTransforms ? TransformNormals(normals, transform) : normals)
            : Array.Empty<float>();
        Vector2[] uvSet = uvSets.Length > 0 ? uvSets[0] : CreateZeroUVs(vertices.Length);
        float[] uvs = FlattenVector2(uvSet);
        uint[] indices = ConvertTriangles(triangles);
        return new Mesh(positions, normalValues, uvs, indices);
    }

    private static Material BuildMaterial(NifContext context, List<int> properties, string contentDir)
    {
        var material = new Material();

        foreach (var propIndex in properties)
        {
            if (propIndex < 0 || propIndex >= context.Blocks.Length)
                continue;

            switch (context.Blocks[propIndex])
            {
                case NiMaterialPropertyBlock matBlock:
                    material.Ambient = matBlock.AmbientColor;
                    material.Diffuse = matBlock.DiffuseColor;
                    material.Specular = matBlock.SpecularColor;
                    material.Emissive = matBlock.EmissiveColor;
                    material.Shininess = matBlock.Glossiness;
                    material.Alpha = matBlock.Alpha;
                    break;
                case NiAlphaPropertyBlock alphaBlock:
                    material.Alpha = alphaBlock.Threshold / 255f;
                    break;
                case NiTexturingPropertyBlock texProp:
                    material.Texture ??= TryLoadTexture(context, texProp, contentDir);
                    break;
            }
        }

        return material;
    }

    private static Triangle[] ConvertStripsToTriangles(NiTriStripsDataBlock data)
    {
        if (!data.HasPoints || data.Points.Length == 0 || data.StripLengths.Length == 0)
            return Array.Empty<Triangle>();

        var triangles = new List<Triangle>();
        int offset = 0;

        foreach (ushort length in data.StripLengths)
        {
            if (length < 3)
            {
                offset += length;
                continue;
            }

            bool flip = false;
            for (int i = 0; i < length - 2; i++)
            {
                ushort i0 = data.Points[offset + i];
                ushort i1 = data.Points[offset + i + 1];
                ushort i2 = data.Points[offset + i + 2];

                if (i0 == i1 || i1 == i2 || i0 == i2)
                {
                    flip = !flip;
                    continue;
                }

                if (flip)
                {
                    triangles.Add(new Triangle { A = i1, B = i0, C = i2 });
                }
                else
                {
                    triangles.Add(new Triangle { A = i0, B = i1, C = i2 });
                }

                flip = !flip;
            }

            offset += length;
        }

        return triangles.ToArray();
    }

    private static Texture? TryLoadTexture(NifContext context, NiTexturingPropertyBlock texProp, string contentDir)
    {
        foreach (var slot in texProp.Textures)
        {
            if (slot?.SourceRef is not int sourceIndex)
                continue;

            var source = context.GetBlock<NiSourceTextureBlock>(sourceIndex);
            if (source == null || string.IsNullOrWhiteSpace(source.FilePath))
                continue;

            var searchPaths = BuildTextureSearchPaths(contentDir, source.FilePath);
            try
            {
                return Texture.Load(searchPaths);
            }
            catch (FileNotFoundException)
            {
                // Try next slot/path combination
            }
        }

        return null;
    }

    private static string[] BuildTextureSearchPaths(string contentDir, string rawPath)
    {
        var paths = new List<string>();
        string normalized = rawPath.Replace('\\', Path.DirectorySeparatorChar);
        if (Path.IsPathRooted(normalized))
        {
            paths.Add(normalized);
        }
        else
        {
            if (!string.IsNullOrEmpty(contentDir))
                paths.Add(Path.Combine(contentDir, normalized));

            if (!string.IsNullOrEmpty(contentDir))
                paths.Add(Path.Combine(contentDir, Path.GetFileName(normalized)));
        }

        paths.Add(Path.Combine("Content", Path.GetFileName(normalized)));

        // Last resort: raw path as provided
        if (!paths.Contains(normalized))
            paths.Add(normalized);

        return paths.ToArray();
    }

    private static float[] FlattenVectors(Vector3[] values)
    {
        var data = new float[values.Length * 3];
        for (int i = 0; i < values.Length; i++)
        {
            data[i * 3 + 0] = values[i].X;
            data[i * 3 + 1] = values[i].Y;
            data[i * 3 + 2] = values[i].Z;
        }
        return data;
    }

    private static Vector3[] TransformPositions(Vector3[] values, TransformData transform)
    {
        if (values.Length == 0)
            return Array.Empty<Vector3>();

        var result = new Vector3[values.Length];
        for (int i = 0; i < values.Length; i++)
            result[i] = transform.TransformPosition(values[i]);
        return result;
    }

    private static Vector3[] TransformNormals(Vector3[] values, TransformData transform)
    {
        if (values.Length == 0)
            return Array.Empty<Vector3>();

        var result = new Vector3[values.Length];
        for (int i = 0; i < values.Length; i++)
            result[i] = transform.TransformNormal(values[i]);
        return result;
    }

    private static float[] FlattenVector2(Vector2[] values)
    {
        var data = new float[values.Length * 2];
        for (int i = 0; i < values.Length; i++)
        {
            data[i * 2 + 0] = values[i].X;
            data[i * 2 + 1] = values[i].Y;
        }
        return data;
    }

    private static Vector2[] CreateZeroUVs(int vertexCount)
    {
        var uvs = new Vector2[vertexCount];
        for (int i = 0; i < vertexCount; i++)
            uvs[i] = Vector2.Zero;
        return uvs;
    }

    private static uint[] ConvertTriangles(Triangle[] tris)
    {
        var indices = new uint[tris.Length * 3];
        for (int i = 0; i < tris.Length; i++)
        {
            indices[i * 3 + 0] = tris[i].A;
            indices[i * 3 + 1] = tris[i].B;
            indices[i * 3 + 2] = tris[i].C;
        }
        return indices;
    }

    private NifContext ParseFile(string path)
    {
        using var stream = File.OpenRead(path);
        using var reader = new BinaryReader(stream);

        var context = new NifContext
        {
            HeaderString = ReadHeaderString(reader),
            Version = reader.ReadUInt32(),
            EndianType = reader.ReadByte(),
            UserVersion = reader.ReadUInt32(),
        };

        if (context.Version != 0x14000004)
            throw new NotSupportedException($"Unsupported NIF version: {context.Version:X8}");
        if (context.EndianType != 1)
            throw new NotSupportedException("Only little-endian NIF files are supported.");

        context.NumBlocks = reader.ReadUInt32();
        ushort numBlockTypes = reader.ReadUInt16();

        context.BlockTypes = new string[numBlockTypes];
        for (int i = 0; i < numBlockTypes; i++)
            context.BlockTypes[i] = ReadSizedString(reader);

        int blockCount = checked((int)context.NumBlocks);
        context.BlockTypeIndex = new int[blockCount];
        for (int i = 0; i < blockCount; i++)
            context.BlockTypeIndex[i] = reader.ReadUInt16();

        // Placeholder field immediately before block data. Civ4 builds keep this but never populate the string table.
        reader.ReadUInt32();

        context.Blocks = new object[blockCount];
        for (int blockIndex = 0; blockIndex < blockCount; blockIndex++)
        {
            int typeIndex = context.BlockTypeIndex[blockIndex];
            if (typeIndex < 0 || typeIndex >= context.BlockTypes.Length)
                throw new InvalidDataException($"Invalid block type index {typeIndex} for block {blockIndex}");

            string typeName = context.BlockTypes[typeIndex];
            long blockStart = reader.BaseStream.Position;
            if (TraceBlocks)
                Console.WriteLine($"[TRACE] Block {blockIndex}: {typeName} (offset {blockStart:X})");
            try
            {
                context.Blocks[blockIndex] = ParseBlock(typeName, reader);
                if (TraceBlocks)
                {
                    long blockEnd = reader.BaseStream.Position;
                    Console.WriteLine($"[TRACE]   size: {blockEnd - blockStart} bytes");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"Failed to parse block {blockIndex} ({typeName})", ex);
            }
        }

        return context;
    }

    private object ParseBlock(string typeName, BinaryReader reader)
    {
        return typeName switch
        {
            "NiNode" => ParseNiNode(reader),
            "NiCollisionData" => ParseNiCollisionData(reader),
            "NiTriShape" => ParseNiTriShape(reader),
            "NiTexturingProperty" => ParseNiTexturingProperty(reader),
            "NiSourceTexture" => ParseNiSourceTexture(reader),
            "NiAlphaProperty" => ParseNiAlphaProperty(reader),
            "NiMaterialProperty" => ParseNiMaterialProperty(reader),
            "NiAlphaController" => ParseNiAlphaController(reader),
            "NiTriShapeData" => ParseNiTriShapeData(reader),
            "NiStencilProperty" => ParseNiStencilProperty(reader),
            "NiSkinInstance" => ParseNiSkinInstance(reader),
            "NiSkinData" => ParseNiSkinData(reader),
            "NiZBufferProperty" => ParseNiZBufferProperty(reader),
            "NiVertexColorProperty" => ParseNiVertexColorProperty(reader),
            "NiStringExtraData" => ParseNiStringExtraData(reader),
            "NiMultiTargetTransformController" => ParseNiMultiTargetTransformController(reader),
            "NiTransformController" => ParseNiTransformController(reader),
            "NiVisController" => ParseNiVisController(reader),
            "NiBillboardNode" => ParseNiBillboardNode(reader),
            "NiTriStrips" => ParseNiTriStrips(reader),
            "NiTriStripsData" => ParseNiTriStripsData(reader),
            "NiSkinPartition" => ParseNiSkinPartition(reader),
            _ => throw new NotSupportedException($"Unsupported block type: {typeName}")
        };
    }

    private NiNodeBlock ParseNiNode(BinaryReader reader)
    {
        var info = ReadGeneralInfo(reader, includeFlags: true);
        var block = new NiNodeBlock(info);
        PopulateNiNode(block, reader);
        return block;
    }

    private NiBillboardNodeBlock ParseNiBillboardNode(BinaryReader reader)
    {
        var info = ReadGeneralInfo(reader, includeFlags: true);
        var block = new NiBillboardNodeBlock(info);
        PopulateNiNode(block, reader);
        block.BillboardMode = reader.ReadUInt16();
        return block;
    }

    private void PopulateNiNode(NiNodeBlock block, BinaryReader reader)
    {
        block.Translation = ReadVector3(reader);
        block.Rotation = ReadMatrix3(reader);
        block.Scale = reader.ReadSingle();
        block.Properties = ReadRefList(reader);
        block.CollisionRef = reader.ReadInt32();
        block.Children = ReadRefList(reader);
        block.Effects = ReadRefList(reader);
    }

    private NiCollisionDataBlock ParseNiCollisionData(BinaryReader reader)
    {
        var block = new NiCollisionDataBlock
        {
            TargetRef = reader.ReadInt32(),
            PropagationMode = reader.ReadUInt32(),
            CollisionMode = reader.ReadUInt32()
        };
        if (reader.ReadByte() != 0)
            block.BoundingVolume = ParseBoundingVolume(reader);
        return block;
    }

    private NiTriShapeBlock ParseNiTriShape(BinaryReader reader)
    {
        var info = ReadGeneralInfo(reader, includeFlags: true);
        var block = new NiTriShapeBlock(info);
        block.Translation = ReadVector3(reader);
        block.Rotation = ReadMatrix3(reader);
        block.Scale = reader.ReadSingle();
        block.Properties = ReadRefList(reader);
        block.CollisionRef = reader.ReadInt32();
        block.DataRef = reader.ReadInt32();
        block.SkinInstanceRef = reader.ReadInt32();
        if (reader.ReadByte() != 0)
        {
            block.ShaderName = ReadSizedString(reader);
            block.ShaderExtraData = reader.ReadInt32();
        }
        return block;
    }

    private NiTriStripsBlock ParseNiTriStrips(BinaryReader reader)
    {
        var info = ReadGeneralInfo(reader, includeFlags: true);
        var block = new NiTriStripsBlock(info);
        block.Translation = ReadVector3(reader);
        block.Rotation = ReadMatrix3(reader);
        block.Scale = reader.ReadSingle();
        block.Properties = ReadRefList(reader);
        block.CollisionRef = reader.ReadInt32();
        block.DataRef = reader.ReadInt32();
        block.SkinInstanceRef = reader.ReadInt32();
        if (reader.ReadByte() != 0)
        {
            block.ShaderName = ReadSizedString(reader);
            block.ShaderExtraData = reader.ReadInt32();
        }
        return block;
    }

    private NiTexturingPropertyBlock ParseNiTexturingProperty(BinaryReader reader)
    {
        var info = ReadGeneralInfo(reader, includeFlags: false);
        var block = new NiTexturingPropertyBlock(info)
        {
            ApplyMode = reader.ReadUInt32()
        };

        uint textureCount = reader.ReadUInt32();
        for (uint i = 0; i < textureCount; i++)
        {
            if (reader.ReadByte() != 0)
            {
                var slot = new TextureSlot
                {
                    SourceRef = (int)reader.ReadUInt32(),
                    ClampMode = reader.ReadUInt32(),
                    FilterMode = reader.ReadUInt32(),
                    UVSet = reader.ReadUInt32()
                };
                if (reader.ReadByte() != 0)
                {
                    slot.Translation = ReadVector2(reader);
                    slot.Scale = ReadVector2(reader);
                    slot.Rotation = reader.ReadSingle();
                    slot.TransformMethod = reader.ReadUInt32();
                    slot.Center = ReadVector2(reader);
                }
                if (i == 5) // Bump map slot includes extra data
                {
                    slot.BumpLumaScale = reader.ReadSingle();
                    slot.BumpLumaOffset = reader.ReadSingle();
                    slot.BumpMatrix = ReadMatrix2(reader);
                }
                block.Textures.Add(slot);
            }
            else
            {
                block.Textures.Add(null);
            }
        }

        uint shaderCount = reader.ReadUInt32();
        for (uint i = 0; i < shaderCount; i++)
        {
            var shaderSlot = new ShaderTextureSlot();
            if (reader.ReadByte() != 0)
            {
                shaderSlot.MapSource = (int)reader.ReadUInt32();
                shaderSlot.ClampMode = reader.ReadUInt32();
                shaderSlot.FilterMode = reader.ReadUInt32();
                shaderSlot.UVSet = reader.ReadUInt32();
                if (reader.ReadByte() != 0)
                {
                    shaderSlot.Translation = ReadVector2(reader);
                    shaderSlot.Scale = ReadVector2(reader);
                    shaderSlot.Rotation = reader.ReadSingle();
                    shaderSlot.TransformMethod = reader.ReadUInt32();
                    shaderSlot.Center = ReadVector2(reader);
                }
                shaderSlot.MapId = reader.ReadUInt32();
            }
            block.ShaderTextures.Add(shaderSlot);
        }

        return block;
    }

    private NiSourceTextureBlock ParseNiSourceTexture(BinaryReader reader)
    {
        var info = ReadGeneralInfo(reader, includeFlags: false);
        return new NiSourceTextureBlock(info)
        {
            UseExternal = reader.ReadByte() != 0,
            FilePath = ReadSizedString(reader),
            PixelData = reader.ReadInt32(),
            PixelLayout = reader.ReadUInt32(),
            UseMipmaps = reader.ReadUInt32(),
            AlphaFormat = reader.ReadUInt32(),
            IsStatic = reader.ReadByte() != 0,
            DirectRender = reader.ReadByte() != 0
        };
    }

    private NiAlphaPropertyBlock ParseNiAlphaProperty(BinaryReader reader)
    {
        var info = ReadGeneralInfo(reader, includeFlags: true);
        return new NiAlphaPropertyBlock(info)
        {
            Threshold = reader.ReadByte()
        };
    }

    private NiZBufferPropertyBlock ParseNiZBufferProperty(BinaryReader reader)
    {
        var info = ReadGeneralInfo(reader, includeFlags: true);
        return new NiZBufferPropertyBlock(info)
        {
            Function = reader.ReadUInt32()
        };
    }

    private NiMaterialPropertyBlock ParseNiMaterialProperty(BinaryReader reader)
    {
        var info = ReadGeneralInfo(reader, includeFlags: false);
        return new NiMaterialPropertyBlock(info)
        {
            AmbientColor = ReadVector3(reader),
            DiffuseColor = ReadVector3(reader),
            SpecularColor = ReadVector3(reader),
            EmissiveColor = ReadVector3(reader),
            Glossiness = reader.ReadSingle(),
            Alpha = reader.ReadSingle()
        };
    }

    private NiVertexColorPropertyBlock ParseNiVertexColorProperty(BinaryReader reader)
    {
        var info = ReadGeneralInfo(reader, includeFlags: true);
        return new NiVertexColorPropertyBlock(info)
        {
            VertexMode = reader.ReadUInt32(),
            LightingMode = reader.ReadUInt32()
        };
    }

    private NiStringExtraDataBlock ParseNiStringExtraData(BinaryReader reader)
    {
        string name = ReadSizedString(reader);
        return new NiStringExtraDataBlock
        {
            Name = name,
            StringData = ReadSizedString(reader)
        };
    }

    private NiAlphaControllerBlock ParseNiAlphaController(BinaryReader reader)
    {
        return new NiAlphaControllerBlock
        {
            NextController = reader.ReadUInt32(),
            Flags = reader.ReadUInt16(),
            Frequency = reader.ReadSingle(),
            Phase = reader.ReadSingle(),
            StartTime = reader.ReadSingle(),
            StopTime = reader.ReadSingle(),
            TargetRef = reader.ReadInt32(),
            InterpolatorRef = reader.ReadInt32()
        };
    }

    private NiMultiTargetTransformControllerBlock ParseNiMultiTargetTransformController(BinaryReader reader)
    {
        var block = new NiMultiTargetTransformControllerBlock
        {
            NextController = reader.ReadUInt32(),
            Flags = reader.ReadUInt16(),
            Frequency = reader.ReadSingle(),
            Phase = reader.ReadSingle(),
            StartTime = reader.ReadSingle(),
            StopTime = reader.ReadSingle(),
            TargetRef = reader.ReadInt32(),
            ExtraTargets = new List<int>()
        };

        ushort count = reader.ReadUInt16();
        for (int i = 0; i < count; i++)
            block.ExtraTargets.Add(reader.ReadInt32());

        return block;
    }

    private NiTransformControllerBlock ParseNiTransformController(BinaryReader reader)
    {
        return new NiTransformControllerBlock
        {
            NextController = reader.ReadUInt32(),
            Flags = reader.ReadUInt16(),
            Frequency = reader.ReadSingle(),
            Phase = reader.ReadSingle(),
            StartTime = reader.ReadSingle(),
            StopTime = reader.ReadSingle(),
            TargetRef = reader.ReadInt32(),
            InterpolatorRef = reader.ReadInt32()
        };
    }

    private NiVisControllerBlock ParseNiVisController(BinaryReader reader)
    {
        return new NiVisControllerBlock
        {
            NextController = reader.ReadUInt32(),
            Flags = reader.ReadUInt16(),
            Frequency = reader.ReadSingle(),
            Phase = reader.ReadSingle(),
            StartTime = reader.ReadSingle(),
            StopTime = reader.ReadSingle(),
            TargetRef = reader.ReadInt32(),
            InterpolatorRef = reader.ReadInt32()
        };
    }

    private NiSkinPartitionBlock ParseNiSkinPartition(BinaryReader reader)
    {
        var block = new NiSkinPartitionBlock();
        block.PartitionCount = reader.ReadUInt32();
        for (uint i = 0; i < block.PartitionCount; i++)
            SkipSkinPartition(reader);
        return block;
    }

    private void SkipSkinPartition(BinaryReader reader)
    {
        ushort vertexCount = reader.ReadUInt16();
        ushort triangleCount = reader.ReadUInt16();
        ushort boneCount = reader.ReadUInt16();
        ushort stripsCount = reader.ReadUInt16();
        ushort weightsPerVertex = reader.ReadUInt16();

        for (int i = 0; i < boneCount; i++)
            reader.ReadUInt16();

        if (reader.ReadByte() != 0)
        {
            for (int i = 0; i < vertexCount; i++)
                reader.ReadUInt16();
        }

        if (reader.ReadByte() != 0)
        {
            for (int i = 0; i < vertexCount; i++)
                for (int j = 0; j < weightsPerVertex; j++)
                    reader.ReadSingle();
        }

        ushort[] stripLengths = new ushort[stripsCount];
        for (int i = 0; i < stripsCount; i++)
            stripLengths[i] = reader.ReadUInt16();

        reader.ReadByte(); // HasFaces

        for (int i = 0; i < stripsCount; i++)
        {
            ushort length = stripLengths[i];
            for (int j = 0; j < length; j++)
                reader.ReadUInt16();
        }

        if (stripsCount == 0)
        {
            for (int i = 0; i < triangleCount; i++)
            {
                reader.ReadUInt16();
                reader.ReadUInt16();
                reader.ReadUInt16();
            }
        }

        if (reader.ReadByte() != 0)
        {
            for (int i = 0; i < vertexCount; i++)
                for (int j = 0; j < weightsPerVertex; j++)
                    reader.ReadByte();
        }
    }

    private NiTriShapeDataBlock ParseNiTriShapeData(BinaryReader reader)
    {
        var block = new NiTriShapeDataBlock
        {
            GroupId = reader.ReadInt32()
        };

        ushort numVertices = reader.ReadUInt16();
        block.KeepFlags = reader.ReadByte();
        block.CompressFlags = reader.ReadByte();

        if (reader.ReadByte() != 0)
        {
            block.Vertices = new Vector3[numVertices];
            for (int i = 0; i < numVertices; i++)
                block.Vertices[i] = ReadVector3(reader);
        }

        block.DataFlags = reader.ReadUInt16();

        if (reader.ReadByte() != 0)
        {
            block.Normals = new Vector3[numVertices];
            for (int i = 0; i < numVertices; i++)
                block.Normals[i] = ReadVector3(reader);
        }

        if ((block.DataFlags & 0x1000) != 0)
        {
            block.Tangents = new Vector3[numVertices];
            block.Bitangents = new Vector3[numVertices];
            for (int i = 0; i < numVertices; i++)
                block.Tangents[i] = ReadVector3(reader);
            for (int i = 0; i < numVertices; i++)
                block.Bitangents[i] = ReadVector3(reader);
        }

        block.BoundingCenter = ReadVector3(reader);
        block.BoundingRadius = reader.ReadSingle();

        if (reader.ReadByte() != 0)
        {
            block.VertexColors = new Vector4[numVertices];
            for (int i = 0; i < numVertices; i++)
                block.VertexColors[i] = ReadVector4(reader);
        }

        int uvSets = block.DataFlags % 4;
        block.UVSets = new Vector2[uvSets][];
        for (int i = 0; i < uvSets; i++)
        {
            block.UVSets[i] = new Vector2[numVertices];
            for (int v = 0; v < numVertices; v++)
                block.UVSets[i][v] = ReadVector2(reader);
        }

        block.ConsistencyFlags = reader.ReadUInt16();
        block.AdditionalData = reader.ReadInt32();
        block.TrianglesCount = reader.ReadUInt16();
        block.TrianglePointsCount = reader.ReadUInt32();
        reader.ReadByte(); // Has triangles (always true in our files)

        block.Triangles = new Triangle[block.TrianglesCount];
        for (int i = 0; i < block.TrianglesCount; i++)
        {
            block.Triangles[i] = new Triangle
            {
                A = reader.ReadUInt16(),
                B = reader.ReadUInt16(),
                C = reader.ReadUInt16()
            };
        }

        ushort matchGroups = reader.ReadUInt16();
        block.MatchGroups = new List<MatchGroup>();
        for (int i = 0; i < matchGroups; i++)
        {
            ushort count = reader.ReadUInt16();
            var mg = new MatchGroup { Indices = new ushort[count] };
            for (int j = 0; j < count; j++)
                mg.Indices[j] = reader.ReadUInt16();
            block.MatchGroups.Add(mg);
        }

        return block;
    }

    private NiTriStripsDataBlock ParseNiTriStripsData(BinaryReader reader)
    {
        var block = new NiTriStripsDataBlock
        {
            GroupId = reader.ReadInt32()
        };

        ushort numVertices = reader.ReadUInt16();
        block.KeepFlags = reader.ReadByte();
        block.CompressFlags = reader.ReadByte();

        if (reader.ReadByte() != 0)
        {
            block.Vertices = new Vector3[numVertices];
            for (int i = 0; i < numVertices; i++)
                block.Vertices[i] = ReadVector3(reader);
        }

        block.DataFlags = reader.ReadUInt16();

        if (reader.ReadByte() != 0)
        {
            block.Normals = new Vector3[numVertices];
            for (int i = 0; i < numVertices; i++)
                block.Normals[i] = ReadVector3(reader);
        }

        if ((block.DataFlags & 0x1000) != 0)
        {
            block.Tangents = new Vector3[numVertices];
            block.Bitangents = new Vector3[numVertices];
            for (int i = 0; i < numVertices; i++)
                block.Tangents[i] = ReadVector3(reader);
            for (int i = 0; i < numVertices; i++)
                block.Bitangents[i] = ReadVector3(reader);
        }

        block.BoundingCenter = ReadVector3(reader);
        block.BoundingRadius = reader.ReadSingle();

        if (reader.ReadByte() != 0)
        {
            block.VertexColors = new Vector4[numVertices];
            for (int i = 0; i < numVertices; i++)
                block.VertexColors[i] = ReadVector4(reader);
        }

        int uvSets = block.DataFlags % 4;
        block.UVSets = new Vector2[uvSets][];
        for (int i = 0; i < uvSets; i++)
        {
            block.UVSets[i] = new Vector2[numVertices];
            for (int v = 0; v < numVertices; v++)
                block.UVSets[i][v] = ReadVector2(reader);
        }

        block.ConsistencyFlags = reader.ReadUInt16();
        block.AdditionalData = reader.ReadInt32();
        block.TrianglesCount = reader.ReadUInt16();
        block.NumStrips = reader.ReadUInt16();
        block.StripLengths = new ushort[block.NumStrips];

        int totalPoints = 0;
        for (int i = 0; i < block.NumStrips; i++)
        {
            ushort length = reader.ReadUInt16();
            block.StripLengths[i] = length;
            totalPoints += length;
        }

        block.HasPoints = reader.ReadByte() != 0;
        if (block.HasPoints)
        {
            block.Points = new ushort[totalPoints];
            for (int i = 0; i < totalPoints; i++)
                block.Points[i] = reader.ReadUInt16();
        }

        return block;
    }

    private NiStencilPropertyBlock ParseNiStencilProperty(BinaryReader reader)
    {
        var info = ReadGeneralInfo(reader, includeFlags: false);
        return new NiStencilPropertyBlock(info)
        {
            Enabled = reader.ReadByte() != 0,
            Function = reader.ReadUInt32(),
            ReferenceValue = reader.ReadUInt32(),
            Mask = reader.ReadUInt32(),
            FailAction = reader.ReadUInt32(),
            ZFailAction = reader.ReadUInt32(),
            PassAction = reader.ReadUInt32(),
            DrawMode = reader.ReadUInt32()
        };
    }

    private NiSkinInstanceBlock ParseNiSkinInstance(BinaryReader reader)
    {
        var block = new NiSkinInstanceBlock
        {
            DataRef = (int)reader.ReadUInt32(),
            SkinPartitionRef = (int)reader.ReadUInt32(),
            SkeletonRootRef = (int)reader.ReadUInt32()
        };
        uint boneCount = reader.ReadUInt32();
        block.Bones = new List<int>();
        for (uint i = 0; i < boneCount; i++)
            block.Bones.Add((int)reader.ReadUInt32());
        return block;
    }

    private NiSkinDataBlock ParseNiSkinData(BinaryReader reader)
    {
        var block = new NiSkinDataBlock
        {
            SkinRotation = ReadMatrix3(reader),
            SkinTranslation = ReadVector3(reader),
            SkinScale = reader.ReadUInt32(),
            BonesCount = reader.ReadUInt32()
        };

        bool hasWeights = reader.ReadByte() != 0;
        block.Bones = new List<SkinBone>();

        for (uint i = 0; i < block.BonesCount; i++)
        {
            var bone = new SkinBone
            {
                SkinRotation = ReadMatrix3(reader),
                SkinTranslation = ReadVector3(reader),
                SkinScale = reader.ReadUInt32(),
                BoundingSphereCenter = ReadVector3(reader),
                BoundingSphereRadius = reader.ReadSingle()
            };

            if (hasWeights)
            {
                ushort vertexCount = reader.ReadUInt16();
                bone.VertexWeights = new List<VertexWeight>();
                for (int v = 0; v < vertexCount; v++)
                {
                    bone.VertexWeights.Add(new VertexWeight
                    {
                        Index = reader.ReadUInt16(),
                        Weight = reader.ReadSingle()
                    });
                }
            }
            block.Bones.Add(bone);
        }

        return block;
    }

    private BoundingVolume ParseBoundingVolume(BinaryReader reader)
    {
        uint type = reader.ReadUInt32();
        return type switch
        {
            0 => new SphereBoundingVolume
            {
                Center = ReadVector3(reader),
                Radius = reader.ReadSingle()
            },
            1 => new BoxBoundingVolume
            {
                Center = ReadVector3(reader),
                Axis = new[] { ReadVector3(reader), ReadVector3(reader), ReadVector3(reader) },
                Extent = ReadVector3(reader)
            },
            2 => new CapsuleBoundingVolume
            {
                Center = ReadVector3(reader),
                Origin = ReadVector3(reader),
                Extent = reader.ReadSingle(),
                Radius = reader.ReadSingle()
            },
            4 => new UnionBoundingVolume
            {
                Members = ReadBoundingVolumeList(reader)
            },
            5 => new HalfSpaceBoundingVolume
            {
                PlaneNormal = ReadVector3(reader),
                PlaneConstant = reader.ReadSingle(),
                Center = ReadVector3(reader)
            },
            0xFFFFFFFF => new DefaultBoundingVolume(),
            _ => throw new InvalidDataException($"Unknown bounding volume type: {type}")
        };
    }

    private List<BoundingVolume> ReadBoundingVolumeList(BinaryReader reader)
    {
        uint count = reader.ReadUInt32();
        var list = new List<BoundingVolume>();
        for (uint i = 0; i < count; i++)
            list.Add(ParseBoundingVolume(reader));
        return list;
    }

    private static GeneralInfo ReadGeneralInfo(BinaryReader reader, bool includeFlags)
    {
        var info = new GeneralInfo
        {
            Name = ReadSizedString(reader),
            ExtraDataRefs = ReadRefList(reader),
            ControllerRef = reader.ReadInt32()
        };

        if (includeFlags)
            info.Flags = reader.ReadUInt16();

        return info;
    }

    private static List<int> ReadRefList(BinaryReader reader)
    {
        uint count = reader.ReadUInt32();
        var list = new List<int>((int)count);
        for (uint i = 0; i < count; i++)
            list.Add(reader.ReadInt32());
        return list;
    }

    private static string ReadHeaderString(BinaryReader reader)
    {
        var bytes = new List<byte>();
        byte value;
        while ((value = reader.ReadByte()) != (byte)'\n')
            bytes.Add(value);
        return System.Text.Encoding.ASCII.GetString(bytes.ToArray());
    }

    private static string ReadSizedString(BinaryReader reader)
    {
        uint first = reader.ReadUInt32();
        if (first == 0)
            return string.Empty;

        long markerPosition = reader.BaseStream.Position;
        if (reader.BaseStream.Position + sizeof(uint) <= reader.BaseStream.Length)
        {
            uint sentinel = reader.ReadUInt32();
            if (sentinel == 0xFFFFFFFF)
            {
                uint actualLength = reader.ReadUInt32();
                if (actualLength == 0)
                    return string.Empty;
                var inlineData = reader.ReadBytes((int)actualLength);
                return System.Text.Encoding.Latin1.GetString(inlineData);
            }
            else
            {
                reader.BaseStream.Position = markerPosition;
            }
        }

        if (first > reader.BaseStream.Length - reader.BaseStream.Position && reader.BaseStream.Position + sizeof(uint) * 3 <= reader.BaseStream.Length)
        {
            uint unknown = reader.ReadUInt32();
            uint sentinel = reader.ReadUInt32();
            if (sentinel != 0xFFFFFFFF)
                throw new InvalidDataException("Encountered unexpected NiString encoding.");
            uint actualLength = reader.ReadUInt32();
            var inlineData = reader.ReadBytes((int)actualLength);
            return System.Text.Encoding.Latin1.GetString(inlineData);
        }

        var data = reader.ReadBytes((int)first);
        return System.Text.Encoding.Latin1.GetString(data);
    }

    private static Vector3 ReadVector3(BinaryReader reader) =>
        new(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

    private static Vector4 ReadVector4(BinaryReader reader) =>
        new(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

    private static Vector2 ReadVector2(BinaryReader reader) =>
        new(reader.ReadSingle(), reader.ReadSingle());

    private static Matrix3 ReadMatrix3(BinaryReader reader)
    {
        // Stored column-major in NIF files.
        float m11 = reader.ReadSingle();
        float m21 = reader.ReadSingle();
        float m31 = reader.ReadSingle();
        float m12 = reader.ReadSingle();
        float m22 = reader.ReadSingle();
        float m32 = reader.ReadSingle();
        float m13 = reader.ReadSingle();
        float m23 = reader.ReadSingle();
        float m33 = reader.ReadSingle();
        return new Matrix3(
            m11, m12, m13,
            m21, m22, m23,
            m31, m32, m33
        );
    }

    private static Matrix2 ReadMatrix2(BinaryReader reader)
    {
        float m11 = reader.ReadSingle();
        float m12 = reader.ReadSingle();
        float m21 = reader.ReadSingle();
        float m22 = reader.ReadSingle();
        return new Matrix2(
            m11, m12,
            m21, m22
        );
    }

    private readonly struct TransformData
    {
        public TransformData(Vector3 translation, Matrix3 rotation, float scale)
        {
            Translation = translation;
            Rotation = rotation;
            Scale = scale;
        }

        public Vector3 Translation { get; }
        public Matrix3 Rotation { get; }
        public float Scale { get; }

        public static TransformData Identity { get; } = new(Vector3.Zero, Matrix3.Identity, 1f);

        public static TransformData FromComponents(Vector3 translation, Matrix3 rotation, float scale) =>
            new(translation, rotation, scale);

        public static TransformData Combine(in TransformData parent, in TransformData local)
        {
            float combinedScale = parent.Scale * local.Scale;
            Matrix3 combinedRotation = Multiply(parent.Rotation, local.Rotation);
            Vector3 translatedChild = Multiply(parent.Rotation, local.Translation * parent.Scale);
            Vector3 combinedTranslation = translatedChild + parent.Translation;
            return new TransformData(combinedTranslation, combinedRotation, combinedScale);
        }

        public Vector3 TransformPosition(Vector3 localPosition)
        {
            Vector3 scaled = localPosition * Scale;
            Vector3 rotated = Multiply(Rotation, scaled);
            return rotated + Translation;
        }

        public Vector3 TransformNormal(Vector3 normal)
        {
            Vector3 rotated = Multiply(Rotation, normal);
            return rotated.LengthSquared > 0f ? Vector3.Normalize(rotated) : rotated;
        }

        private static Matrix3 Multiply(Matrix3 left, Matrix3 right)
        {
            return new Matrix3(
                left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31,
                left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32,
                left.M11 * right.M13 + left.M12 * right.M23 + left.M13 * right.M33,
                left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31,
                left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32,
                left.M21 * right.M13 + left.M22 * right.M23 + left.M23 * right.M33,
                left.M31 * right.M11 + left.M32 * right.M21 + left.M33 * right.M31,
                left.M31 * right.M12 + left.M32 * right.M22 + left.M33 * right.M32,
                left.M31 * right.M13 + left.M32 * right.M23 + left.M33 * right.M33
            );
        }

        private static Vector3 Multiply(Matrix3 matrix, Vector3 vector)
        {
            return new Vector3(
                matrix.M11 * vector.X + matrix.M12 * vector.Y + matrix.M13 * vector.Z,
                matrix.M21 * vector.X + matrix.M22 * vector.Y + matrix.M23 * vector.Z,
                matrix.M31 * vector.X + matrix.M32 * vector.Y + matrix.M33 * vector.Z
            );
        }
    }

    private sealed class GeneralInfo
    {
        public string Name { get; set; } = string.Empty;
        public List<int> ExtraDataRefs { get; set; } = new();
        public int ControllerRef { get; set; }
        public ushort Flags { get; set; }
    }

    private abstract class NamedBlock
    {
        protected NamedBlock(GeneralInfo info)
        {
            Name = info.Name;
            ExtraDataRefs = info.ExtraDataRefs;
            ControllerRef = info.ControllerRef;
            Flags = info.Flags;
        }

        public string Name { get; }
        public List<int> ExtraDataRefs { get; }
        public int ControllerRef { get; }
        public ushort Flags { get; }
    }

    private class NiNodeBlock : NamedBlock
    {
        public NiNodeBlock(GeneralInfo info) : base(info) { }
        public Vector3 Translation { get; set; }
        public Matrix3 Rotation { get; set; }
        public float Scale { get; set; }
        public List<int> Properties { get; set; } = new();
        public int CollisionRef { get; set; }
        public List<int> Children { get; set; } = new();
        public List<int> Effects { get; set; } = new();
    }

    private sealed class NiBillboardNodeBlock : NiNodeBlock
    {
        public NiBillboardNodeBlock(GeneralInfo info) : base(info) { }
        public ushort BillboardMode { get; set; }
    }

    private sealed class NiTriShapeBlock : NamedBlock
    {
        public NiTriShapeBlock(GeneralInfo info) : base(info) { }
        public Vector3 Translation { get; set; }
        public Matrix3 Rotation { get; set; }
        public float Scale { get; set; }
        public List<int> Properties { get; set; } = new();
        public int CollisionRef { get; set; }
        public int DataRef { get; set; }
        public int SkinInstanceRef { get; set; }
        public string? ShaderName { get; set; }
        public int? ShaderExtraData { get; set; }
    }

    private sealed class NiTriStripsBlock : NamedBlock
    {
        public NiTriStripsBlock(GeneralInfo info) : base(info) { }
        public Vector3 Translation { get; set; }
        public Matrix3 Rotation { get; set; }
        public float Scale { get; set; }
        public List<int> Properties { get; set; } = new();
        public int CollisionRef { get; set; }
        public int DataRef { get; set; }
        public int SkinInstanceRef { get; set; }
        public string? ShaderName { get; set; }
        public int? ShaderExtraData { get; set; }
    }

    private sealed class NiCollisionDataBlock
    {
        public int TargetRef { get; set; }
        public uint PropagationMode { get; set; }
        public uint CollisionMode { get; set; }
        public BoundingVolume? BoundingVolume { get; set; }
    }

    private sealed class NiTexturingPropertyBlock : NamedBlock
    {
        public NiTexturingPropertyBlock(GeneralInfo info) : base(info) { }
        public uint ApplyMode { get; set; }
        public List<TextureSlot?> Textures { get; } = new();
        public List<ShaderTextureSlot> ShaderTextures { get; } = new();
    }

    private sealed class TextureSlot
    {
        public int SourceRef { get; set; }
        public uint ClampMode { get; set; }
        public uint FilterMode { get; set; }
        public uint UVSet { get; set; }
        public Vector2? Translation { get; set; }
        public Vector2? Scale { get; set; }
        public float? Rotation { get; set; }
        public uint? TransformMethod { get; set; }
        public Vector2? Center { get; set; }
        public float? BumpLumaScale { get; set; }
        public float? BumpLumaOffset { get; set; }
        public Matrix2? BumpMatrix { get; set; }
    }

    private sealed class ShaderTextureSlot
    {
        public int MapSource { get; set; }
        public uint ClampMode { get; set; }
        public uint FilterMode { get; set; }
        public uint UVSet { get; set; }
        public Vector2? Translation { get; set; }
        public Vector2? Scale { get; set; }
        public float? Rotation { get; set; }
        public uint? TransformMethod { get; set; }
        public Vector2? Center { get; set; }
        public uint MapId { get; set; }
    }

    private sealed class NiSourceTextureBlock : NamedBlock
    {
        public NiSourceTextureBlock(GeneralInfo info) : base(info) { }
        public bool UseExternal { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public int PixelData { get; set; }
        public uint PixelLayout { get; set; }
        public uint UseMipmaps { get; set; }
        public uint AlphaFormat { get; set; }
        public bool IsStatic { get; set; }
        public bool DirectRender { get; set; }
    }

    private sealed class NiAlphaPropertyBlock : NamedBlock
    {
        public NiAlphaPropertyBlock(GeneralInfo info) : base(info) { }
        public byte Threshold { get; set; }
    }

    private sealed class NiZBufferPropertyBlock : NamedBlock
    {
        public NiZBufferPropertyBlock(GeneralInfo info) : base(info) { }
        public uint Function { get; set; }
    }

    private sealed class NiMaterialPropertyBlock : NamedBlock
    {
        public NiMaterialPropertyBlock(GeneralInfo info) : base(info) { }
        public Vector3 AmbientColor { get; set; }
        public Vector3 DiffuseColor { get; set; }
        public Vector3 SpecularColor { get; set; }
        public Vector3 EmissiveColor { get; set; }
        public float Glossiness { get; set; }
        public float Alpha { get; set; }
    }

    private sealed class NiVertexColorPropertyBlock : NamedBlock
    {
        public NiVertexColorPropertyBlock(GeneralInfo info) : base(info) { }
        public uint VertexMode { get; set; }
        public uint LightingMode { get; set; }
    }

    private sealed class NiStringExtraDataBlock
    {
        public string Name { get; set; } = string.Empty;
        public string StringData { get; set; } = string.Empty;
    }

    private sealed class NiAlphaControllerBlock
    {
        public uint NextController { get; set; }
        public ushort Flags { get; set; }
        public float Frequency { get; set; }
        public float Phase { get; set; }
        public float StartTime { get; set; }
        public float StopTime { get; set; }
        public int TargetRef { get; set; }
        public int InterpolatorRef { get; set; }
    }

    private sealed class NiMultiTargetTransformControllerBlock
    {
        public uint NextController { get; set; }
        public ushort Flags { get; set; }
        public float Frequency { get; set; }
        public float Phase { get; set; }
        public float StartTime { get; set; }
        public float StopTime { get; set; }
        public int TargetRef { get; set; }
        public List<int> ExtraTargets { get; set; } = new();
    }

    private sealed class NiTransformControllerBlock
    {
        public uint NextController { get; set; }
        public ushort Flags { get; set; }
        public float Frequency { get; set; }
        public float Phase { get; set; }
        public float StartTime { get; set; }
        public float StopTime { get; set; }
        public int TargetRef { get; set; }
        public int InterpolatorRef { get; set; }
    }

    private sealed class NiVisControllerBlock
    {
        public uint NextController { get; set; }
        public ushort Flags { get; set; }
        public float Frequency { get; set; }
        public float Phase { get; set; }
        public float StartTime { get; set; }
        public float StopTime { get; set; }
        public int TargetRef { get; set; }
        public int InterpolatorRef { get; set; }
    }

    private sealed class NiSkinPartitionBlock
    {
        public uint PartitionCount { get; set; }
    }

    private sealed class NiTriShapeDataBlock
    {
        public int GroupId { get; set; }
        public byte KeepFlags { get; set; }
        public byte CompressFlags { get; set; }
        public ushort DataFlags { get; set; }
        public Vector3[] Vertices { get; set; } = Array.Empty<Vector3>();
        public Vector3[] Normals { get; set; } = Array.Empty<Vector3>();
        public Vector3[] Tangents { get; set; } = Array.Empty<Vector3>();
        public Vector3[] Bitangents { get; set; } = Array.Empty<Vector3>();
        public Vector4[] VertexColors { get; set; } = Array.Empty<Vector4>();
        public Vector2[][] UVSets { get; set; } = Array.Empty<Vector2[]>();
        public Vector3 BoundingCenter { get; set; }
        public float BoundingRadius { get; set; }
        public ushort ConsistencyFlags { get; set; }
        public int AdditionalData { get; set; }
        public ushort TrianglesCount { get; set; }
        public uint TrianglePointsCount { get; set; }
        public Triangle[] Triangles { get; set; } = Array.Empty<Triangle>();
        public List<MatchGroup> MatchGroups { get; set; } = new();
    }

    private sealed class NiTriStripsDataBlock
    {
        public int GroupId { get; set; }
        public byte KeepFlags { get; set; }
        public byte CompressFlags { get; set; }
        public ushort DataFlags { get; set; }
        public Vector3[] Vertices { get; set; } = Array.Empty<Vector3>();
        public Vector3[] Normals { get; set; } = Array.Empty<Vector3>();
        public Vector3[] Tangents { get; set; } = Array.Empty<Vector3>();
        public Vector3[] Bitangents { get; set; } = Array.Empty<Vector3>();
        public Vector4[] VertexColors { get; set; } = Array.Empty<Vector4>();
        public Vector2[][] UVSets { get; set; } = Array.Empty<Vector2[]>();
        public Vector3 BoundingCenter { get; set; }
        public float BoundingRadius { get; set; }
        public ushort ConsistencyFlags { get; set; }
        public int AdditionalData { get; set; }
        public ushort TrianglesCount { get; set; }
        public ushort NumStrips { get; set; }
        public ushort[] StripLengths { get; set; } = Array.Empty<ushort>();
        public bool HasPoints { get; set; }
        public ushort[] Points { get; set; } = Array.Empty<ushort>();
    }

    private sealed class MatchGroup
    {
        public ushort[] Indices { get; set; } = Array.Empty<ushort>();
    }

    private struct Triangle
    {
        public ushort A;
        public ushort B;
        public ushort C;
    }

    private sealed class NiStencilPropertyBlock : NamedBlock
    {
        public NiStencilPropertyBlock(GeneralInfo info) : base(info) { }
        public bool Enabled { get; set; }
        public uint Function { get; set; }
        public uint ReferenceValue { get; set; }
        public uint Mask { get; set; }
        public uint FailAction { get; set; }
        public uint ZFailAction { get; set; }
        public uint PassAction { get; set; }
        public uint DrawMode { get; set; }
    }

    private sealed class NiSkinInstanceBlock
    {
        public int DataRef { get; set; }
        public int SkinPartitionRef { get; set; }
        public int SkeletonRootRef { get; set; }
        public List<int> Bones { get; set; } = new();
    }

    private sealed class NiSkinDataBlock
    {
        public Matrix3 SkinRotation { get; set; }
        public Vector3 SkinTranslation { get; set; }
        public uint SkinScale { get; set; }
        public uint BonesCount { get; set; }
        public List<SkinBone> Bones { get; set; } = new();
    }

    private sealed class SkinBone
    {
        public Matrix3 SkinRotation { get; set; }
               public Vector3 SkinTranslation { get; set; }
        public uint SkinScale { get; set; }
        public Vector3 BoundingSphereCenter { get; set; }
        public float BoundingSphereRadius { get; set; }
        public List<VertexWeight> VertexWeights { get; set; } = new();
    }

    private sealed class VertexWeight
    {
        public ushort Index { get; set; }
        public float Weight { get; set; }
    }

    private abstract class BoundingVolume { }
    private sealed class SphereBoundingVolume : BoundingVolume
    {
        public Vector3 Center { get; set; }
        public float Radius { get; set; }
    }

    private sealed class BoxBoundingVolume : BoundingVolume
    {
        public Vector3 Center { get; set; }
        public Vector3[] Axis { get; set; } = Array.Empty<Vector3>();
        public Vector3 Extent { get; set; }
    }

    private sealed class CapsuleBoundingVolume : BoundingVolume
    {
        public Vector3 Center { get; set; }
        public Vector3 Origin { get; set; }
        public float Extent { get; set; }
        public float Radius { get; set; }
    }

    private sealed class UnionBoundingVolume : BoundingVolume
    {
        public List<BoundingVolume> Members { get; set; } = new();
    }

    private sealed class HalfSpaceBoundingVolume : BoundingVolume
    {
        public Vector3 PlaneNormal { get; set; }
        public float PlaneConstant { get; set; }
        public Vector3 Center { get; set; }
    }

    private sealed class DefaultBoundingVolume : BoundingVolume { }
}
