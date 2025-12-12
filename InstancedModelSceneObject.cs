using System;
using System.Collections.Generic;
using OpenTK.Mathematics;

/// <summary>
/// Renders many copies of the same model arranged in a cubic grid for benchmarking.
/// </summary>
public sealed class InstancedModelSceneObject : ISceneObject
{
    private readonly Model _model;
    private readonly Vector3[] _positions;
    private readonly float _autoScale;
    private readonly float _rotationSpeed;
    private float _time;

    private InstancedModelSceneObject(Model model, Vector3[] positions, float autoScale, float rotationSpeed)
    {
        _model = model;
        _positions = positions;
        _autoScale = autoScale;
        _rotationSpeed = rotationSpeed;
    }

    public static InstancedModelSceneObject CreateCube(Model model, int instanceCount, float spacing = 100f, float rotationSpeed = 0.2f)
    {
        if (instanceCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(instanceCount));

        float autoScale = NifModelSceneObject.ComputeAutoScale(model);

        int perAxis = (int)Math.Ceiling(Math.Pow(instanceCount, 1f / 3f));
        float half = (perAxis - 1) * spacing * 0.5f;
        var positions = new List<Vector3>(instanceCount);

        for (int x = 0; x < perAxis && positions.Count < instanceCount; x++)
        {
            for (int y = 0; y < perAxis && positions.Count < instanceCount; y++)
            {
                for (int z = 0; z < perAxis && positions.Count < instanceCount; z++)
                {
                    var pos = new Vector3(
                        x * spacing - half,
                        y * spacing - half,
                        z * spacing - half);
                    positions.Add(pos);
                    if (positions.Count >= instanceCount)
                        break;
                }
            }
        }

        return new InstancedModelSceneObject(model, positions.ToArray(), autoScale, rotationSpeed);
    }

    public void Update(float deltaTime)
    {
        _time += deltaTime;
    }

    public void Render(Shader shader, Matrix4 view, Matrix4 proj)
    {
        shader.Use();
        shader.SetMatrix4("uView", ref view);
        shader.SetMatrix4("uProj", ref proj);

        Matrix4 baseRotationX = Matrix4.CreateRotationX(-MathF.PI / 2f);
        Matrix4 rotationY = Matrix4.CreateRotationY(_time * _rotationSpeed);
        Matrix4 scale = Matrix4.CreateScale(_autoScale);
        Matrix4 baseModel = baseRotationX * rotationY * scale;

        foreach (var pos in _positions)
        {
            Matrix4 modelMatrix = Matrix4.CreateTranslation(pos) * baseModel;
            shader.SetMatrix4("uModel", ref modelMatrix);
            _model.Draw(shader);
        }
    }

    public void Dispose()
    {
        _model.Dispose();
    }
}
