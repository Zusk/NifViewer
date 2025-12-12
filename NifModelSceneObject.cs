using System;
using System.IO;
using OpenTK.Mathematics;

/// <summary>
/// Scene object that renders a model loaded from a Civ4 NIF file.
/// </summary>
public sealed class NifModelSceneObject : ISceneObject
{
    private readonly Model _model;
    private readonly float _autoScale;
    private float _time;

    private NifModelSceneObject(Model model)
    {
        _model = model;
        _autoScale = ComputeAutoScale(model);
    }

    public static NifModelSceneObject Load(string nifPath)
    {
        string resolvedPath = ResolvePath(nifPath);
        var loader = new Civ4NifLoader();
        var model = loader.LoadModel(resolvedPath);
        Console.WriteLine($"[INFO] Loaded NIF model \"{Path.GetFileName(resolvedPath)}\" ({model.Meshes.Count} mesh(es))");
        return new NifModelSceneObject(model);
    }

    public void Update(float deltaTime)
    {
        _time += deltaTime;
    }

    public void Render(Shader shader, Matrix4 view, Matrix4 proj)
    {
        Matrix4 modelMatrix =
            Matrix4.CreateRotationY(_time * 0.2f) *
            Matrix4.CreateScale(_autoScale);

        shader.Use();
        shader.SetMatrix4("uModel", ref modelMatrix);
        shader.SetMatrix4("uView", ref view);
        shader.SetMatrix4("uProj", ref proj);

        _model.Draw(shader);
    }

    public void Dispose()
    {
        _model.Dispose();
    }

    private static string ResolvePath(string path)
    {
        if (File.Exists(path))
            return path;

        string candidate = Path.Combine(AppContext.BaseDirectory, path);
        if (File.Exists(candidate))
            return candidate;

        candidate = Path.Combine(Directory.GetCurrentDirectory(), path);
        if (File.Exists(candidate))
            return candidate;

        throw new FileNotFoundException("NIF file could not be located.", path);
    }

    private static float ComputeAutoScale(Model model)
    {
        float maxAbs = 0f;
        foreach (var mesh in model.Meshes)
        {
            var positions = mesh.Positions;
            for (int i = 0; i < positions.Length; i++)
            {
                float value = MathF.Abs(positions[i]);
                if (value > maxAbs)
                    maxAbs = value;
            }
        }

        if (maxAbs <= float.Epsilon)
            return 1f;

        return 1f / maxAbs;
    }
}
