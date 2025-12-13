using System;
using System.IO;
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
        bool loadOnly = false;
        bool bakeTransforms = true;

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
                case "--load-only":
                    loadOnly = true;
                    forceModel = true;
                    forceCube = false;
                    break;
                case "--no-transform-bake":
                    bakeTransforms = false;
                    break;
            }
        }

        if (loadOnly)
        {
            string desiredPath = nifPath ?? Path.Combine(AppContext.BaseDirectory, "Content", "Svart_Monk.nif");
            RunLoadOnly(desiredPath, bakeTransforms);
            return;
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

        using var window = new RenderWindow(gws, nws, forceCube, forceModel, bakeTransforms, nifPath);
        window.Run();
    }

    private static void RunLoadOnly(string desiredPath, bool bakeTransforms)
    {
        var loader = new Civ4NifLoader();
        Console.WriteLine($"[INFO] Running load-only mode against \"{Path.GetFileName(desiredPath)}\"...");
        try
        {
            loader.LoadModel(desiredPath, createGpuMeshes: false, bakeTransforms: bakeTransforms);
            Console.WriteLine($"[INFO] Successfully loaded \"{Path.GetFileName(desiredPath)}\" (load-only mode).");
            Environment.ExitCode = 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] Load-only mode failed for \"{desiredPath}\": {ex}");
            Environment.ExitCode = 1;
        }
    }
}
