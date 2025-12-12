using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

class Program
{
    public static void Main(string[] args)
    {
        bool forceCube = false;
        bool forceModel = true;
        string? nifPath = null;

        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "--cube":
                    forceCube = true;
                    forceModel = false;
                    break;
                case "--model":
                    forceModel = true;
                    forceCube = false;
                    break;
                case "--nif":
                    if (i + 1 < args.Length)
                    {
                        nifPath = args[++i];
                        forceModel = true;
                        forceCube = false;
                    }
                    break;
            }
        }

        var gws = GameWindowSettings.Default;

        string title = forceCube ? "NIF Viewer — Debug Cube" : "NIF Viewer — Model View";
        var nws = new NativeWindowSettings
        {
            ClientSize = new Vector2i(800, 600),
            Title = title,
            APIVersion = new Version(3, 3),
            Profile = ContextProfile.Core,
            Flags = ContextFlags.ForwardCompatible
        };

        using var window = new RenderWindow(gws, nws, forceCube, forceModel, nifPath);
        window.Run();
    }
}
