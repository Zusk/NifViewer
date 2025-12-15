using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.IO;

// General rendering window — owns shader, camera/projection, and scene objects.
class RenderWindow : GameWindow
{
    private Shader? _shader;
    private bool _forceCube = false;
    private bool _forceModel = false;
    private readonly string? _nifPath;
    private readonly string? _animationPath;
    private readonly bool _bakeTransforms = true;
    private float _cameraYaw = MathF.PI / 4f;
    private float _cameraPitch = -0.2f;
    private float _cameraDistance = 20f;
    private const float MinCameraDistance = 0.5f;
    private const float MaxCameraDistance = 200f;
    private const float CameraRotateSpeed = 1.3f;
    private const float CameraZoomSpeed = 10.0f;
    private readonly bool _verboseLogging = false;

    // Scene objects (meshes, debug helpers, etc.) rendered each frame.
    private List<ISceneObject> _sceneObjects = new List<ISceneObject>();

    public RenderWindow(GameWindowSettings gws, NativeWindowSettings nws)
        : base(gws, nws)
    {
    }

    public RenderWindow(GameWindowSettings gws, NativeWindowSettings nws, bool forceCube, bool forceModel, bool bakeTransforms, string? nifPath = null, string? animationPath = null, bool verboseLogging = false)
        : base(gws, nws)
    {
        _forceCube = forceCube;
        _forceModel = forceModel;
        _nifPath = nifPath;
        _animationPath = animationPath;
        _bakeTransforms = bakeTransforms;
        _verboseLogging = verboseLogging;
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        GL.Enable(EnableCap.DepthTest);
        GL.ClearColor(0.1f, 0.1f, 0.1f, 1f);

        Console.WriteLine("[DEBUG] Initializing renderer.");

        // Load shader sources from files (copied to output by the project file).
        string vertPath = Path.Combine(AppContext.BaseDirectory, "Shaders", "phong.vert");
        string fragPath = Path.Combine(AppContext.BaseDirectory, "Shaders", "phong.frag");

        string vertSrc;
        string fragSrc;

        if (File.Exists(vertPath) && File.Exists(fragPath))
        {
            vertSrc = File.ReadAllText(vertPath);
            fragSrc = File.ReadAllText(fragPath);
        }
        else
        {
            throw new Exception($"Shader files not found. Expected at: {vertPath} and {fragPath}");
        }

        // Build Phong shader
        _shader = new Shader(vertSrc, fragSrc);
        _shader!.Use();
        _shader!.SetInt("uTexture", 0);

        // Add scene objects according to flags.
        if (_forceCube && !_forceModel)
        {
            _sceneObjects.Add(new DebugCube());
            return;
        }

        if (_forceModel)
        {
            string desiredPath = _nifPath ?? Path.Combine(AppContext.BaseDirectory, "Content", "Svart_Monk.nif");
            if (TryAddNifModel(desiredPath, _animationPath))
                return;

            Console.WriteLine("[INFO] Falling back to debug cube because the model could not be loaded.");
        }

        _sceneObjects.Add(new DebugCube());
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);

        float delta = (float)args.Time;
        if (delta <= 0f)
            return;

        var keyboard = KeyboardState;
        if (keyboard.IsKeyDown(Keys.Left))
            _cameraYaw -= CameraRotateSpeed * delta;
        if (keyboard.IsKeyDown(Keys.Right))
            _cameraYaw += CameraRotateSpeed * delta;
        if (keyboard.IsKeyDown(Keys.Up))
            _cameraPitch = Clamp(_cameraPitch + CameraRotateSpeed * delta, -MathF.PI / 2f + 0.1f, MathF.PI / 2f - 0.1f);
        if (keyboard.IsKeyDown(Keys.Down))
            _cameraPitch = Clamp(_cameraPitch - CameraRotateSpeed * delta, -MathF.PI / 2f + 0.1f, MathF.PI / 2f - 0.1f);
        if (keyboard.IsKeyDown(Keys.PageUp))
            _cameraDistance = Clamp(_cameraDistance - CameraZoomSpeed * delta, MinCameraDistance, MaxCameraDistance);
        if (keyboard.IsKeyDown(Keys.PageDown))
            _cameraDistance = Clamp(_cameraDistance + CameraZoomSpeed * delta, MinCameraDistance, MaxCameraDistance);
        if (keyboard.IsKeyPressed(Keys.R))
        {
            _cameraYaw = MathF.PI / 4f;
            _cameraPitch = -0.2f;
            _cameraDistance = 5f;
        }
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);

        // Update scene and draw.
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        Matrix4 view = BuildCameraView();

        Matrix4 proj =
            Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(45f),
                Size.X / (float)Size.Y,
                0.01f, 400f
            );

        // Shared shader state
        _shader!.Use();
        _shader.SetVector3("uLightPos", new Vector3(3f, 3f, 3f));
        _shader.SetVector3("uViewPos", new Vector3(0f, 0f, 3f));
        _shader.SetVector3("uAmbientColor", new Vector3(0.2f));
        _shader.SetVector3("uDiffuseColor", new Vector3(0.8f));
        _shader.SetVector3("uSpecularColor", new Vector3(1f));
        _shader.SetFloat("uShininess", 32f);

        // Update & render each scene object
        foreach (var obj in _sceneObjects)
        {
            obj.Update((float)args.Time);
            obj.Render(_shader, view, proj);
        }

        SwapBuffers();
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);
        GL.Viewport(0, 0, Size.X, Size.Y);
    }

    protected override void OnMouseWheel(MouseWheelEventArgs e)
    {
        base.OnMouseWheel(e);
        _cameraDistance = Clamp(_cameraDistance - e.OffsetY * CameraZoomSpeed, MinCameraDistance, MaxCameraDistance);
    }

    protected override void OnUnload()
    {
        base.OnUnload();
        foreach (var obj in _sceneObjects)
            obj.Dispose();
    }

    private bool TryAddNifModel(string path, string? animationPath)
    {
        try
        {
            var obj = AnimatedNifSceneObject.Load(path, _bakeTransforms, animationPath, _verboseLogging);
            _sceneObjects.Add(obj);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[WARN] Failed to load animated NIF model \"{path}\": {ex.Message}");
            return false;
        }
    }

    private Matrix4 BuildCameraView()
    {
        float cosPitch = MathF.Cos(_cameraPitch);
        float sinPitch = MathF.Sin(_cameraPitch);
        float sinYaw = MathF.Sin(_cameraYaw);
        float cosYaw = MathF.Cos(_cameraYaw);

        var cameraPosition = new Vector3(
            _cameraDistance * cosPitch * sinYaw,
            _cameraDistance * sinPitch,
            _cameraDistance * cosPitch * cosYaw);

        return Matrix4.LookAt(cameraPosition, Vector3.Zero, Vector3.UnitY);
    }

    private static float Clamp(float value, float min, float max)
    {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }
}
