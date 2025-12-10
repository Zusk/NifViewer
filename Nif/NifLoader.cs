using System;
using System.IO;
using NifViewer.Nif;
using NifViewer.Nif.ModelBuilder;

public static class NIFLoader
{
    public static bool Debug = true;

    /// <summary>
    /// Entry point for the Civ4-style loader. Returns a NifFile whose Blocks array
    /// mirrors the order of the data stored in the binary stream.
    /// </summary>
    public static NifFile Load(string path)
    {
        Console.WriteLine($"[NIF] Loading {path}");
        return NifFile.Load(path);
    }

    /// <summary>
    /// Schema-driven loader left intact for experimentation; it still uses the legacy
    /// reflection-based reader found under SchemaDriven/.
    /// </summary>
    public static global::NifFile LoadWithSchema(string path, string? schemaPath = null)
    {
        schemaPath ??= Path.Combine(AppContext.BaseDirectory, "Content", "nif.xml");
        var schema = NifSchema.Load(schemaPath);
        var reader = new SchemaDrivenNifReader(schema);
        return reader.Read(path);
    }

    /// <summary>
    /// Converts Civ4 node objects into a renderer-friendly Model by passing them through
    /// MeshBuilder, then wrapping each mesh with a basic material.
    /// </summary>
    public static Model BuildModelFromNIF(NifFile file)
    {
        Model model = new Model();
        var meshes = MeshBuilder.Build(file);

        foreach (var mesh in meshes)
        {
            var material = new Material
            {
                Texture = Texture.Load("Content/texture.png")
            };
            model.AddMesh(mesh, material);
        }

        return model;
    }
}
