using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
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
    private readonly bool _bakeTransforms = true;

    // Scene objects (meshes, debug helpers, etc.) rendered each frame.
    private List<ISceneObject> _sceneObjects = new List<ISceneObject>();

    public RenderWindow(GameWindowSettings gws, NativeWindowSettings nws)
        : base(gws, nws)
    {
    }

    public RenderWindow(GameWindowSettings gws, NativeWindowSettings nws, bool forceCube, bool forceModel, bool bakeTransforms, string? nifPath = null)
        : base(gws, nws)
    {
        _forceCube = forceCube;
        _forceModel = forceModel;
        _nifPath = nifPath;
        _bakeTransforms = bakeTransforms;
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

        Matrix4 view =
            Matrix4.CreateTranslation(0f, 0f, -3f);

        Matrix4 proj =
            Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(45f),
                Size.X / (float)Size.Y,
                0.1f, 100f
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

    protected override void OnUnload()
    {
        base.OnUnload();
        foreach (var obj in _sceneObjects)
            obj.Dispose();
    }

    private bool TryAddNifModel(string path)
    {
        try
        {
            var obj = NifModelSceneObject.Load(path, _bakeTransforms);
            _sceneObjects.Add(obj);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[WARN] Failed to load NIF model \"{path}\": {ex.Message}");
            return false;
        }
    }
}
