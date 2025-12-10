using System;
using System.Collections.Generic;

namespace NifViewer.Nif.ModelBuilder;

/// <summary>
/// Creates renderable meshes from Civ4 NIF blocks.
/// Binary .nif -> Civ4 nodes (NifFile) -> MeshBuilder -> Mesh -> renderer.
/// </summary>
public static class MeshBuilder
{
    public static List<Mesh> Build(NifFile file)
    {
        var meshes = new List<Mesh>();
        var dataBlocks = new Dictionary<int, NiTriShapeData>();
        var shapes = new List<NiTriShape>();

        for (int i = 0; i < file.Blocks.Length; i++)
        {
            switch (file.Blocks[i])
            {
                case NiTriShapeData data:
                    dataBlocks[i] = data;
                    break;
                case NiTriShape shape:
                    shapes.Add(shape);
                    break;
            }
        }

        foreach (var shape in shapes)
        {
            if (!dataBlocks.TryGetValue(shape.DataIndex, out var data))
                continue;

            if (data.Vertices.Length == 0 || data.Triangles.Length == 0)
                continue;

            meshes.Add(BuildMesh(data));
        }

        return meshes;
    }

    private static Mesh BuildMesh(NiTriShapeData data)
    {
        int vertexCount = data.Vertices.Length;
        float[] positions = new float[vertexCount * 3];
        float[] normals = new float[vertexCount * 3];
        float[] uvs = new float[vertexCount * 2];

        for (int i = 0; i < vertexCount; i++)
        {
            int posOffset = i * 3;
            int uvOffset = i * 2;

            var pos = data.Vertices[i];
            positions[posOffset] = pos.X;
            positions[posOffset + 1] = pos.Y;
            positions[posOffset + 2] = pos.Z;

            Vector3 normal = default;
            if (data.HasNormals != 0 && i < data.Normals.Length)
                normal = data.Normals[i];
            else
                normal = new Vector3 { X = 0f, Y = 1f, Z = 0f };

            normals[posOffset] = normal.X;
            normals[posOffset + 1] = normal.Y;
            normals[posOffset + 2] = normal.Z;

            if (i < data.UVSets.Length)
            {
                var uv = data.UVSets[i];
                uvs[uvOffset] = uv.X;
                uvs[uvOffset + 1] = uv.Y;
            }
        }

        uint[] indices = new uint[data.Triangles.Length * 3];
        for (int i = 0; i < data.Triangles.Length; i++)
        {
            int dst = i * 3;
            indices[dst] = data.Triangles[i].X;
            indices[dst + 1] = data.Triangles[i].Y;
            indices[dst + 2] = data.Triangles[i].Z;
        }

        return new Mesh(positions, normals, uvs, indices);
    }
}
