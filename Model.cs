using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

public class Model
{
    public List<Mesh> Meshes { get; private set; }
    public List<Material> Materials { get; private set; }

    public Model()
    {
        Meshes = new List<Mesh>();
        Materials = new List<Material>();
    }

    /// <summary>
    /// Adds a mesh with its own material.
    /// Texture belongs to the material.
    /// </summary>
    public void AddMesh(Mesh mesh, Material material)
    {
        Meshes.Add(mesh);
        Materials.Add(material);
    }

    public void Draw(Shader shader, IReadOnlyList<TransformData>? blockTransforms = null, Matrix4? baseTransform = null)
    {
        Matrix4 global = baseTransform ?? Matrix4.Identity;
        int count = Meshes.Count;

        for (int i = 0; i < count; i++)
        {
            var material = Materials[i];

            // Apply material uniform values
            material.ApplyToShader(shader);

            // Bind texture if present
            if (material.Texture != null)
                material.Texture.Bind(TextureUnit.Texture0);

            Matrix4 meshMatrix = global;
            if (blockTransforms != null && i < blockTransforms.Count)
            {
                var meshTransform = ToMatrix4(blockTransforms[i]);
                meshMatrix = global * meshTransform;
            }

            shader.SetMatrix4("uModel", ref meshMatrix);
            Meshes[i].Render();
        }
    }

    private static Matrix4 ToMatrix4(TransformData transform)
    {
        var rotation = transform.Rotation;
        float scale = transform.Scale;

        return new Matrix4(
            rotation.M11 * scale, rotation.M12 * scale, rotation.M13 * scale, 0f,
            rotation.M21 * scale, rotation.M22 * scale, rotation.M23 * scale, 0f,
            rotation.M31 * scale, rotation.M32 * scale, rotation.M33 * scale, 0f,
            transform.Translation.X, transform.Translation.Y, transform.Translation.Z, 1f
        );
    }

    public void Dispose()
    {
        foreach (var mesh in Meshes)
            mesh.Dispose();
    }
}
