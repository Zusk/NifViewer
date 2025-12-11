using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
// This build does not include a NIF/model loader. The app will print a
// diagnostic line and render a debug cube by default. The loader and
// dynamic model-rendering code may be added back in future revisions.

class CubeWindow : GameWindow
{
    private Model? _model;
    private Shader? _shader;
    private bool _forceCube = false;
    private bool _forceModel = false;

    private float _time = 0f;

    // Cube vertex layout: position (3), normal (3), uv (2)
    private float[] _vertices =
    {
        // FRONT (+Z)
        -0.5f,-0.5f, 0.5f,   0f,0f,1f,   0f,0f,
         0.5f,-0.5f, 0.5f,   0f,0f,1f,   1f,0f,
         0.5f, 0.5f, 0.5f,   0f,0f,1f,   1f,1f,
        -0.5f, 0.5f, 0.5f,   0f,0f,1f,   0f,1f,

        // BACK (-Z)
        -0.5f,-0.5f,-0.5f,   0f,0f,-1f,  1f,0f,
         0.5f,-0.5f,-0.5f,   0f,0f,-1f,  0f,0f,
         0.5f, 0.5f,-0.5f,   0f,0f,-1f,  0f,1f,
        -0.5f, 0.5f,-0.5f,   0f,0f,-1f,  1f,1f,

        // LEFT (-X)
        -0.5f,-0.5f,-0.5f,  -1f,0f,0f,   0f,0f,
        -0.5f,-0.5f, 0.5f,  -1f,0f,0f,   1f,0f,
        -0.5f, 0.5f, 0.5f,  -1f,0f,0f,   1f,1f,
        -0.5f, 0.5f,-0.5f,  -1f,0f,0f,   0f,1f,

        // RIGHT (+X)
         0.5f,-0.5f,-0.5f,   1f,0f,0f,   1f,0f,
         0.5f,-0.5f, 0.5f,   1f,0f,0f,   0f,0f,
         0.5f, 0.5f, 0.5f,   1f,0f,0f,   0f,1f,
         0.5f, 0.5f,-0.5f,   1f,0f,0f,   1f,1f,

        // BOTTOM (-Y)
        -0.5f,-0.5f,-0.5f,   0f,-1f,0f,  0f,1f,
         0.5f,-0.5f,-0.5f,   0f,-1f,0f,  1f,1f,
         0.5f,-0.5f, 0.5f,   0f,-1f,0f,  1f,0f,
        -0.5f,-0.5f, 0.5f,   0f,-1f,0f,  0f,0f,

        // TOP (+Y)
        -0.5f, 0.5f,-0.5f,   0f,1f,0f,   0f,0f,
         0.5f, 0.5f,-0.5f,   0f,1f,0f,   1f,0f,
         0.5f, 0.5f, 0.5f,   0f,1f,0f,   1f,1f,
        -0.5f, 0.5f, 0.5f,   0f,1f,0f,   0f,1f,
    };

    private uint[] _indices =
    {
        0,1,2,  2,3,0,
        4,5,6,  6,7,4,
        8,9,10, 10,11,8,
        12,13,14,14,15,12,
        16,17,18,18,19,16,
        20,21,22,22,23,20
    };

    public CubeWindow(GameWindowSettings gws, NativeWindowSettings nws)
        : base(gws, nws)
    {
    }

    // New constructor: allow caller to force rendering mode
    public CubeWindow(GameWindowSettings gws, NativeWindowSettings nws, bool forceCube, bool forceModel)
        : base(gws, nws)
    {
        _forceCube = forceCube;
        _forceModel = forceModel;
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        GL.Enable(EnableCap.DepthTest);
        GL.ClearColor(0.1f, 0.1f, 0.1f, 1f);

    // No model loader available: print diagnostic line and use the
    // debug cube as the rendering fallback.
    Console.WriteLine("[DEBUG] Model loader not present: showing debug cube.");

        // Honor explicit flags to the extent possible. If the user requested
        // model loading via --model, we don't have a loader here; warn and
        // fall back to the cube.
        if (_forceModel && !_forceCube)
        {
            Console.WriteLine("[INFO] --model requested but model loading is currently disabled. Showing debug cube instead.");
            _model = PrimitiveFactory.CreateTestCubeModel();
        }
        else
        {
            // Default path: render the debug cube
            _model = PrimitiveFactory.CreateTestCubeModel();
        }

        // ------------------------------------------------------
        // Build Phong shader
        // ------------------------------------------------------
        _shader = new Shader(VertexShaderCode, FragmentShaderCode);
            _shader!.Use();
            _shader!.SetInt("uTexture", 0);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);

        _time += (float)args.Time;

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        Matrix4 model =
            Matrix4.CreateRotationY(_time) *
            Matrix4.CreateRotationX(_time * 0.7f);

        Matrix4 view =
            Matrix4.CreateTranslation(0f, 0f, -3f);

        Matrix4 proj =
            Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(45f),
                Size.X / (float)Size.Y,
                0.1f, 100f
            );

    _shader!.Use();
        _shader.SetMatrix4("uModel", ref model);
        _shader.SetMatrix4("uView", ref view);
        _shader.SetMatrix4("uProj", ref proj);

        _shader.SetVector3("uLightPos", new Vector3(3f, 3f, 3f));
        _shader.SetVector3("uViewPos", new Vector3(0f, 0f, 3f));
        _shader.SetVector3("uAmbientColor", new Vector3(0.2f));
        _shader.SetVector3("uDiffuseColor", new Vector3(0.8f));
        _shader.SetVector3("uSpecularColor", new Vector3(1f));
        _shader.SetFloat("uShininess", 32f);

        if (_model != null && _shader != null)
            _model.Draw(_shader);

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
        if (_model != null)
            _model.Dispose();
    }


    // -----------------------------
    // SHADERS
    // -----------------------------

    private const string VertexShaderCode = @"
#version 330 core
layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec3 aNormal;
layout(location = 2) in vec2 aUV;

uniform mat4 uModel;
uniform mat4 uView;
uniform mat4 uProj;

out vec3 vFragPos;
out vec3 vNormal;
out vec2 vUV;

void main()
{
    vFragPos = vec3(uModel * vec4(aPosition, 1.0));
    vNormal = mat3(transpose(inverse(uModel))) * aNormal;
    vUV = aUV;
    gl_Position = uProj * uView * vec4(vFragPos, 1.0);
}
";

    private const string FragmentShaderCode = @"
#version 330 core
out vec4 FragColor;

in vec3 vFragPos;
in vec3 vNormal;
in vec2 vUV;

uniform vec3 uLightPos;
uniform vec3 uViewPos;

uniform vec3 uAmbientColor;
uniform vec3 uDiffuseColor;
uniform vec3 uSpecularColor;
uniform float uShininess;

uniform sampler2D uTexture;

void main()
{
    vec3 N = normalize(vNormal);
    vec3 L = normalize(uLightPos - vFragPos);

    float diff = max(dot(N,L),0.0);
    vec3 diffuse = diff * uDiffuseColor;

    vec3 ambient = uAmbientColor;

    vec3 V = normalize(uViewPos - vFragPos);
    vec3 R = reflect(-L, N);
    float spec = pow(max(dot(V,R),0.0), uShininess);
    vec3 specular = spec * uSpecularColor;

    vec4 texColor = texture(uTexture, vUV);

    vec3 final = (ambient + diffuse + specular) * texColor.rgb;

    FragColor = vec4(final, texColor.a);
}
";
}
