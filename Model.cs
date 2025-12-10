using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

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

    public void Draw(Shader shader)
    {
        for (int i = 0; i < Meshes.Count; i++)
        {
            var material = Materials[i];

            // Apply material uniform values
            material.ApplyToShader(shader);

            // Bind texture if present
            if (material.Texture != null)
                material.Texture.Bind(TextureUnit.Texture0);

            Meshes[i].Render();
        }
    }

    public void Dispose()
    {
        foreach (var mesh in Meshes)
            mesh.Dispose();
    }
}
