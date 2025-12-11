using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

class Program
{
    public static void Main(string[] args)
    {
        // Schema-driven test mode: use nif.xml to parse everything dynamically
        //if (args.Length > 0 && args[0] == "--schema-load")
        //{
        //    string path = args.Length > 1 ? args[1] : "Content/Svart_Monk.nif";
        //    string schemaPath = args.Length > 2 ? args[2] : Path.Combine(AppContext.BaseDirectory, "Content", "nif.xml");
        //    Console.WriteLine($"[INFO] Schema-driven load requested: {path} (schema={schemaPath})");
        //    Console.WriteLine("[WARN] Model/NIF loader is not included in this build. Re-enable or implement the loader to use this feature.");
        //    return;
        //}

        // Quick test mode: `--test-load [path]` will load a NIF and print a summary
        //if (args.Length > 0 && args[0] == "--test-load")
        //{
        //    string path = args.Length > 1 ? args[1] : "Content/Svart_Monk.nif";
        //    Console.WriteLine($"[INFO] Test-load requested: {path}");
        //    Console.WriteLine("[WARN] Model/NIF loader is not included in this build. Re-enable or implement the loader to use this feature.");
        //    return;
        //}
        bool forceCube = true;
        bool forceModel = false;
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--cube") forceCube = true;
            if (args[i] == "--model") forceModel = true;
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

        using var window = new RenderWindow(gws, nws, forceCube, forceModel);
        window.Run();
    }
}
