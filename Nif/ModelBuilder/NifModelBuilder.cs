using System;
using System.Collections.Generic;
using OpenTK.Mathematics;

/// <summary>
/// Converts NIF blocks into runtime Model objects usable by your renderer.
/// Civ4-only implementation: NiTriShape + NiTriShapeData + NiMaterialProperty.
/// </summary>
public static class NifModelBuilder
{
    /// <summary>
    /// Converts a list of parsed NiObject blocks into a renderable Model.
    /// Returns null if no usable geometry is found.
    /// </summary>
    public static Model? Build(List<NiObject> blocks, bool debug = false)
    {
        Model model = new Model();

        // --- 1. Collect blocks by type ---
        Dictionary<int, NiTriShapeData> dataBlocks = new();
        Dictionary<int, NiMaterialProperty> materialBlocks = new();
        Dictionary<int, NiTexturingProperty> texturingBlocks = new();
        Dictionary<int, NiSourceTexture> textureBlocks = new();

        List<NiTriShape> shapes = new();

        foreach (var block in blocks)
        {
            switch (block)
            {
                case NiTriShape shape:
                    shapes.Add(shape);
                    break;

                case NiTriShapeData data:
                    dataBlocks[data.BlockIndex] = data;
                    break;

                case NiMaterialProperty mat:
                    materialBlocks[mat.BlockIndex] = mat;
                    break;

                case NiTexturingProperty tx:
                    texturingBlocks[tx.BlockIndex] = tx;
                    break;

                case NiSourceTexture st:
                    textureBlocks[st.BlockIndex] = st;
                    break;
            }
        }

        if (debug)
        {
            Console.WriteLine($"[ModelBuilder] Found {shapes.Count} NiTriShape blocks.");
            Console.WriteLine($"[ModelBuilder] Found {dataBlocks.Count} NiTriShapeData blocks.");
            Console.WriteLine($"[ModelBuilder] Found {materialBlocks.Count} NiMaterialProperty blocks.");
            Console.WriteLine($"[ModelBuilder] Found {texturingBlocks.Count} NiTexturingProperty blocks.");
            Console.WriteLine($"[ModelBuilder] Found {textureBlocks.Count} NiSourceTexture blocks.");
        }

        // --- 2. Build meshes for each NiTriShape ---
        foreach (var shape in shapes)
        {
            if (!dataBlocks.TryGetValue(shape.DataIndex, out var data))
            {
                Console.WriteLine($"[WARN] Shape {shape.BlockIndex} references missing NiTriShapeData {shape.DataIndex}");
                continue;
            }

            if (data.Vertices.Length == 0 || data.Indices.Length == 0)
            {
                Console.WriteLine($"[WARN] NiTriShapeData {data.BlockIndex} has no geometry.");
                continue;
            }

            if (debug)
                Console.WriteLine($"[ModelBuilder] Building mesh for shape {shape.BlockIndex}");

            // --- Build CPU vertex buffer (pos, normal, uv) ---
            float[] vertexBuffer = BuildVertexArray(data);
            uint[] indexBuffer = ConvertIndices(data);

            Mesh mesh = new Mesh(vertexBuffer, indexBuffer);

            // --- Build material ---
            Material mat = BuildMaterial(shape, materialBlocks, texturingBlocks, textureBlocks, debug);

            // Add fully built mesh+material to the model
            model.AddMesh(mesh, mat);
        }

        if (model.Meshes.Count == 0)
            return null;

        return model;
    }

    // ============================================================
    // Internal helper methods
    // ============================================================

    private static float[] BuildVertexArray(NiTriShapeData data)
    {
        int vCount = data.Vertices.Length;
        float[] buffer = new float[vCount * 8]; // 3 pos + 3 norm + 2 uv

        bool hasNormals = data.HasNormals && data.Normals.Length == vCount;
        bool hasUVs = data.HasUV && data.NumUVSets > 0 &&
                      data.UVSets[0].Length == vCount;

        int p = 0;
        for (int i = 0; i < vCount; i++)
        {
            var pos = data.Vertices[i];
            var nrm = hasNormals ? data.Normals[i] : Vector3.UnitY;
            var uv  = hasUVs ? data.UVSets[0][i] : Vector2.Zero;

            buffer[p++] = pos.X;
            buffer[p++] = pos.Y;
            buffer[p++] = pos.Z;

            buffer[p++] = nrm.X;
            buffer[p++] = nrm.Y;
            buffer[p++] = nrm.Z;

            buffer[p++] = uv.X;
            buffer[p++] = uv.Y;
        }

        return buffer;
    }

    private static uint[] ConvertIndices(NiTriShapeData data)
    {
        uint[] output = new uint[data.Indices.Length];
        for (int i = 0; i < output.Length; i++)
            output[i] = data.Indices[i];
        return output;
    }

    private static Material BuildMaterial(
        NiTriShape shape,
        Dictionary<int, NiMaterialProperty> materialBlocks,
        Dictionary<int, NiTexturingProperty> texturingBlocks,
        Dictionary<int, NiSourceTexture> textureBlocks,
        bool debug)
    {
        Material mat = new Material();

        // --- Material & textures referenced by property list ---
        foreach (int propIndex in shape.PropertyIndices)
        {
            if (materialBlocks.TryGetValue(propIndex, out var mp))
            {
                mat.Diffuse = mp.Diffuse;
                mat.Specular = mp.Specular;
                mat.Shininess = mp.Shininess;
            }

            if (texturingBlocks.TryGetValue(propIndex, out var tx))
            {
                if (textureBlocks.TryGetValue(tx.BaseTextureIndex, out var st))
                {
                    if (debug)
                        Console.WriteLine($"[ModelBuilder] Using texture '{st.FileName}'");

                    // First try relative to Content/, then fall back to raw
                    mat.Texture =
                        Texture.Load("Content/" + st.FileName, st.FileName);
                }
            }
        }

        // --- Fallback texture ---
        if (mat.Texture == null)
        {
            if (debug)
                Console.WriteLine("[ModelBuilder] Using fallback texture Content/texture.png");

            mat.Texture = Texture.Load("Content/texture.png");
        }

        return mat;
    }
}

