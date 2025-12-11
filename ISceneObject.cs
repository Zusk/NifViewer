using OpenTK.Mathematics;

public interface ISceneObject : System.IDisposable
{
    /// <summary>
    /// Advance per-frame time for the scene object.
    /// </summary>
    void Update(float deltaTime);

    /// <summary>
    /// Render the object. The caller provides the shared shader and camera matrices.
    /// The implementation should set its own model matrix into the shader before drawing.
    /// </summary>
    void Render(Shader shader, Matrix4 view, Matrix4 proj);
}
