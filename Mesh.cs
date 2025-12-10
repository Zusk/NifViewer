using OpenTK.Graphics.OpenGL4;

public class Mesh
{
    public int VAO { get; private set; }
    public int VBO { get; private set; }
    public int EBO { get; private set; }

    public int IndexCount { get; private set; }

    // vertices must contain: position (3) + normal (3) + uv (2) = 8 floats per vertex
    public Mesh(float[] vertices, uint[] indices)
    {
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
