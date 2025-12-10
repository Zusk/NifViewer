using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

class Program
{
    public static void Main(string[] args)
    {
        // Schema-driven test mode: use nif.xml to parse everything dynamically
        if (args.Length > 0 && args[0] == "--schema-load")
        {
            string path = args.Length > 1 ? args[1] : "Content/Svart_Monk.nif";
            string schemaPath = args.Length > 2 ? args[2] : Path.Combine(AppContext.BaseDirectory, "Content", "nif.xml");
            Console.WriteLine($"Schema-driven load: {path} (schema={schemaPath})");
            try
            {
                var file = NIFLoader.LoadWithSchema(path, schemaPath);
                Console.WriteLine($"Loaded {file.Blocks.Count} blocks via schema-driven reader.");
                for (int i = 0; i < Math.Min(10, file.Blocks.Count); i++)
                {
                    var block = file.Blocks[i];
                    Console.WriteLine($"[{i}] {block.TypeName} ({block.Fields.Count} fields)");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during schema-driven load: {ex}");
            }

            return;
        }

        // Quick test mode: `--test-load [path]` will load a NIF and print a summary
        if (args.Length > 0 && args[0] == "--test-load")
        {
            string path = args.Length > 1 ? args[1] : "Content/Svart_Monk.nif";
            Console.WriteLine($"Test loading: {path}");
            try
            {
                var blocks = NIFLoader.Load(path);
                Console.WriteLine($"Loaded {blocks.Count} blocks.");
                for (int i = 0; i < Math.Min(10, blocks.Count); i++)
                    Console.WriteLine($"[{i}] {blocks[i].TypeName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during load: {ex}");
            }

            return;
        }

        // Rendering mode: default to model view. Use --cube to force the
        // debug cube renderer, or --model to force model rendering.
        bool forceCube = false;
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

        using var window = new CubeWindow(gws, nws, forceCube, forceModel);
        window.Run();
    }
}
