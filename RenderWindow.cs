using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
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
    private readonly bool _benchmarkMode;
    private readonly int _benchmarkCount;

    // Scene objects (meshes, debug helpers, etc.) rendered each frame.
    private List<ISceneObject> _sceneObjects = new List<ISceneObject>();

    // Simple free-fly camera
    private Vector3 _cameraPosition = new Vector3(0f, 15f, 40f);
    private readonly Vector3 _cameraUp = Vector3.UnitY;
    private Vector3 _cameraForward = -Vector3.UnitZ;
    private float _movementSpeed = 5f;

    public RenderWindow(GameWindowSettings gws, NativeWindowSettings nws)
        : base(gws, nws)
    {
    }

    public RenderWindow(GameWindowSettings gws, NativeWindowSettings nws, bool forceCube, bool forceModel, string? nifPath = null)
        : this(gws, nws, forceCube, forceModel, false, 10_000, nifPath)
    {
    }

    public RenderWindow(GameWindowSettings gws, NativeWindowSettings nws, bool forceCube, bool forceModel, bool benchmarkMode, int benchmarkCount, string? nifPath = null)
        : base(gws, nws)
    {
        _forceCube = forceCube;
        _forceModel = forceModel;
        _benchmarkMode = benchmarkMode;
        _benchmarkCount = benchmarkCount;
        _nifPath = nifPath;
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
            if (TryAddNifModel(desiredPath))
                return;

            Console.WriteLine("[INFO] Falling back to debug cube because the model could not be loaded.");
        }

        _sceneObjects.Add(new DebugCube());
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);

        // Update scene and draw.
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        Matrix4 view = Matrix4.LookAt(_cameraPosition, _cameraPosition + _cameraForward, _cameraUp);

        Matrix4 proj =
            Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(45f),
                Size.X / (float)Size.Y,
                0.1f, 100f
            );

        // Shared shader state
        _shader!.Use();
        _shader.SetVector3("uLightPos", new Vector3(3f, 3f, 3f));
        _shader.SetVector3("uViewPos", _cameraPosition);
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

    protected override void OnUnload()
    {
        base.OnUnload();
        foreach (var obj in _sceneObjects)
            obj.Dispose();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);

        var keyboard = KeyboardState;
        float delta = (float)args.Time;
        float speed = _movementSpeed * delta * (keyboard.IsKeyDown(Keys.LeftShift) ? 3f : 1f);

        Vector3 forward = new Vector3(_cameraForward.X, 0f, _cameraForward.Z);
        if (forward.LengthSquared > 0.0001f)
            forward.Normalize();
        Vector3 right = Vector3.Normalize(Vector3.Cross(forward, _cameraUp));

        if (keyboard.IsKeyDown(Keys.W)) _cameraPosition += forward * speed;
        if (keyboard.IsKeyDown(Keys.S)) _cameraPosition -= forward * speed;
        if (keyboard.IsKeyDown(Keys.A)) _cameraPosition -= right * speed;
        if (keyboard.IsKeyDown(Keys.D)) _cameraPosition += right * speed;
        if (keyboard.IsKeyDown(Keys.Space)) _cameraPosition += _cameraUp * speed;
        if (keyboard.IsKeyDown(Keys.LeftControl)) _cameraPosition -= _cameraUp * speed;
    }

    private bool TryAddNifModel(string path)
    {
        try
        {
            var loader = new Civ4NifLoader();
            var model = loader.LoadModel(path);
            Console.WriteLine($"[INFO] Loaded NIF model \"{Path.GetFileName(path)}\" ({model.Meshes.Count} mesh(es))");

            if (_benchmarkMode)
            {
                var instanced = InstancedModelSceneObject.CreateCube(model, _benchmarkCount);
                _sceneObjects.Add(instanced);
            }
            else
            {
                _sceneObjects.Add(new NifModelSceneObject(model));
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[WARN] Failed to load NIF model \"{path}\": {ex.Message}");
            return false;
        }
    }
}
