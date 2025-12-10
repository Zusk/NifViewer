using System;
using System.Collections.Generic;
using System.IO;
using OpenTK.Mathematics;

public static class NIFLoader
{
    public static bool Debug = true;

    /// <summary>
    /// High-level load entry point. Opens the file, uses NifReader to parse
    /// all Civ4-style blocks, and returns them as a list.
    /// </summary>
    public static List<NiObject> Load(string path)
    {
        using var fs = File.OpenRead(path);
        using var br = new BinaryReader(fs);

        var reader = new NifReader(br);
        var blocks = reader.ReadAllBlocks();

        Console.WriteLine($"[NIF] Finished. Loaded {blocks.Count} blocks.");
        return blocks;
    }

    /// <summary>
    /// Schema-driven loader that uses nif.xml at runtime to read the file without
    /// hardcoded block layouts. Returns a generic NifFile structure.
    /// </summary>
    public static NifFile LoadWithSchema(string path, string? schemaPath = null)
    {
        schemaPath ??= Path.Combine(AppContext.BaseDirectory, "Content", "nif.xml");
        var schema = NifSchema.Load(schemaPath);
        var reader = new SchemaDrivenNifReader(schema);
        return reader.Read(path);
    }

    // ===================================================================
    // BUILD MODEL FROM BLOCKS  (Civ4 NiTriShape → Mesh → Model)
    // ===================================================================

    public static Model BuildModelFromNIF(List<NiObject> blocks)
    {
        Model model = new Model();

        List<NiTriShape> shapes = new();
        Dictionary<int, NiTriShapeData> shapeData = new();
        Dictionary<int, NiMaterialProperty> materials = new();
        Dictionary<int, NiTexturingProperty> texturing = new();
        Dictionary<int, NiSourceTexture> textures = new();

        foreach (var block in blocks)
        {
            switch (block)
            {
                case NiTriShape ts:
                    shapes.Add(ts);
                    break;

                case NiTriShapeData d:
                    shapeData[d.BlockIndex] = d;
                    break;

                case NiMaterialProperty mp:
                    materials[mp.BlockIndex] = mp;
                    break;

                case NiTexturingProperty tx:
                    texturing[tx.BlockIndex] = tx;
                    break;

                case NiSourceTexture st:
                    textures[st.BlockIndex] = st;
                    break;
            }
        }

        // ---- Build a mesh for each NiTriShape ----
        foreach (var shape in shapes)
        {
            if (!shapeData.TryGetValue(shape.DataIndex, out var data))
            {
                Console.WriteLine($"[WARN] Missing NiTriShapeData for shape #{shape.BlockIndex}");
                continue;
            }

            if (data.NumVertices == 0 || data.Vertices.Length == 0 || data.Indices.Length == 0)
            {
                Console.WriteLine($"[WARN] NiTriShapeData #{data.BlockIndex} has no usable geometry; skipping.");
                continue;
            }

            if (Debug)
                Console.WriteLine($"[NIF] Building mesh for NiTriShape #{shape.BlockIndex}");

            List<float> vb = new();

            for (int i = 0; i < data.NumVertices; i++)
            {
                var pos = data.Vertices[i];
                var nrm = data.HasNormals && i < data.Normals.Length
                          ? data.Normals[i]
                          : Vector3.UnitY;

                var uv = data.HasUV &&
                         data.UVSets.Length > 0 &&
                         i < data.UVSets[0].Length
                         ? data.UVSets[0][i]
                         : Vector2.Zero;

                vb.Add(pos.X); vb.Add(pos.Y); vb.Add(pos.Z);
                vb.Add(nrm.X); vb.Add(nrm.Y); vb.Add(nrm.Z);
                vb.Add(uv.X);  vb.Add(uv.Y);
            }

            uint[] indices = Array.ConvertAll(data.Indices, x => (uint)x);

            Mesh mesh = new Mesh(vb.ToArray(), indices);
            Material mat = new Material();

            // Bind material + texture from NiMaterialProperty / NiTexturingProperty / NiSourceTexture
            foreach (int prop in shape.PropertyIndices)
            {
                if (materials.TryGetValue(prop, out var mp))
                {
                    mat.Diffuse = mp.Diffuse;
                    mat.Specular = mp.Specular;
                    mat.Shininess = mp.Shininess;
                }

                if (texturing.TryGetValue(prop, out var tx))
                {
                    if (textures.TryGetValue(tx.BaseTextureIndex, out var st))
                    {
                        // Try content-relative first, then raw filename
                        mat.Texture = Texture.Load($"Content/{st.FileName}", st.FileName);
                    }
                }
            }

            // Fallback texture if no NiSourceTexture
            if (mat.Texture == null)
                mat.Texture = Texture.Load("Content/texture.png");

            model.AddMesh(mesh, mat);
        }

        return model;
    }
}

