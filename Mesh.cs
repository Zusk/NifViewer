using System;
using OpenTK.Graphics.OpenGL4;

public class Mesh
{
    public float[] Positions { get; private set; } = Array.Empty<float>();
    public float[] Normals { get; private set; } = Array.Empty<float>();
    public float[] UVs { get; private set; } = Array.Empty<float>();
    public uint[] RawIndices { get; private set; } = Array.Empty<uint>();

    public int VAO { get; private set; }
    public int VBO { get; private set; }
    public int EBO { get; private set; }

    public int IndexCount { get; private set; }

    // vertices must contain: position (3) + normal (3) + uv (2) = 8 floats per vertex
    public Mesh(float[] vertices, uint[] indices)
    {
        RawIndices = indices;
        IndexCount = indices.Length;

        VAO = GL.GenVertexArray();
        VBO = GL.GenBuffer();
        EBO = GL.GenBuffer();

        GL.BindVertexArray(VAO);

        // Upload vertex data
        GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
        GL.BufferData(BufferTarget.ArrayBuffer,
                      vertices.Length * sizeof(float),
                      vertices,
                      BufferUsageHint.StaticDraw);

        // Upload index data
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
        GL.BufferData(BufferTarget.ElementArrayBuffer,
                      indices.Length * sizeof(uint),
                      indices,
                      BufferUsageHint.StaticDraw);

        int stride = 8 * sizeof(float);

        // Position: location = 0, vec3
        GL.EnableVertexAttribArray(0);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float,
            false, stride, 0);

        // Normal: location = 1, vec3
        GL.EnableVertexAttribArray(1);
        GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float,
            false, stride, 3 * sizeof(float));

        // UV: location = 2, vec2
        GL.EnableVertexAttribArray(2);
        GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float,
            false, stride, 6 * sizeof(float));

        GL.BindVertexArray(0);
    }

    /// <summary>
    /// Construct a mesh from separate attribute streams produced by MeshBuilder.
    /// The data is interleaved into the format expected by the renderer while also
    /// keeping the raw arrays for any future CPU-side processing.
    /// </summary>
    public Mesh(float[] positions, float[] normals, float[] uvs, uint[] indices)
        : this(Interleave(positions, normals, uvs), indices)
    {
        Positions = positions;
        Normals = normals;
        UVs = uvs;
        RawIndices = indices;
    }

    private static float[] Interleave(float[] positions, float[] normals, float[] uvs)
    {
        int vertexCount = positions.Length / 3;
        float[] interleaved = new float[vertexCount * 8];

        for (int i = 0; i < vertexCount; i++)
        {
            int posOffset = i * 3;
            int uvOffset = i * 2;
            int dst = i * 8;

            interleaved[dst] = positions[posOffset];
            interleaved[dst + 1] = positions[posOffset + 1];
            interleaved[dst + 2] = positions[posOffset + 2];

            if (normals.Length >= posOffset + 3)
            {
                interleaved[dst + 3] = normals[posOffset];
                interleaved[dst + 4] = normals[posOffset + 1];
                interleaved[dst + 5] = normals[posOffset + 2];
            }

            if (uvs.Length >= uvOffset + 2)
            {
                interleaved[dst + 6] = uvs[uvOffset];
                interleaved[dst + 7] = uvs[uvOffset + 1];
            }
        }

        return interleaved;
    }

    public void Render()
    {
        GL.BindVertexArray(VAO);
        GL.DrawElements(PrimitiveType.Triangles, IndexCount,
            DrawElementsType.UnsignedInt, 0);
    }

    public void Dispose()
    {
        GL.DeleteBuffer(VBO);
        GL.DeleteBuffer(EBO);
        GL.DeleteVertexArray(VAO);
    }
}
