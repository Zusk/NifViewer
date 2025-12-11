using OpenTK.Mathematics;
using System;

public class DebugCube : ISceneObject
{
    private Model _model;
    private float _time = 0f;

    public DebugCube()
    {
        _model = PrimitiveFactory.CreateTestCubeModel();
    }

    public void Update(float deltaTime)
    {
        _time += deltaTime;
    }

    public void Render(Shader shader, Matrix4 view, Matrix4 proj)
    {
        // Compute object-local model transform
        Matrix4 model =
            Matrix4.CreateRotationY(_time) *
            Matrix4.CreateRotationX(_time * 0.7f);

        shader.Use();
        shader.SetMatrix4("uModel", ref model);
        shader.SetMatrix4("uView", ref view);
        shader.SetMatrix4("uProj", ref proj);

        _model.Draw(shader);
    }

    public void Dispose()
    {
        _model.Dispose();
    }
}
