using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using OpenTK.Mathematics;

/// <summary>
/// Scene object that builds a runtime skeleton/controller stack and keeps the shared model alive.
/// </summary>
public sealed class AnimatedNifSceneObject : ISceneObject
{
    private readonly NifScene _scene;
    private readonly Skeleton _skeleton;
    private readonly NiControllerManager _controllerManager = new();
    private readonly float _autoScale;
    private readonly string _modelDirectory;
    private readonly Dictionary<int, NodeInfo> _nodeLookup;
    private readonly Dictionary<int, GeometryBlockInfo> _geometryLookup;
    private readonly Dictionary<int, int> _nodeParents;
    private readonly bool _verboseLogging;
    private readonly Model _debugMarker;
    private float _elapsedTime;
    private float _verboseTimer;
    private Dictionary<int, TransformData>? _debugRuntimeTransforms;
    private const float VerboseIntervalSeconds = 1f;
    private const float MarkerScale = 0.2f;

    private AnimatedNifSceneObject(NifScene scene, string modelDirectory, bool verboseLogging)
    {
        _scene = scene;
        _skeleton = new Skeleton(scene);
        _autoScale = ComputeAutoScale(scene.Model);
        _modelDirectory = modelDirectory;
        _nodeLookup = scene.Nodes.ToDictionary(node => node.BlockIndex);
        _geometryLookup = scene.GeometryBlocks.ToDictionary(block => block.BlockIndex);
        _nodeParents = BuildNodeParentMap(scene.Nodes);
        _verboseLogging = verboseLogging;
        _debugMarker = PrimitiveFactory.CreateMarkerCubeModel();

        // Add a demo controller while .kf loading is still pending.
        var spinBone = _skeleton.Bones.FirstOrDefault(b => !string.IsNullOrEmpty(b.Node.Name));
        if (spinBone != null)
        {
            string nodeName = spinBone.Node.Name ?? string.Empty;
            _controllerManager.AddController(NiControllerSequence.CreateDemoSpin(nodeName));
            Console.WriteLine($"[INFO] Attached demo controller to \"{spinBone.Node.Name}\" (skeleton bones: {_skeleton.Bones.Count}).");
        }
        else
        {
            Console.WriteLine("[INFO] No skeleton bones found; animation controller skipped.");
        }
    }

    public static AnimatedNifSceneObject Load(string nifPath, bool bakeTransforms, string? animationPath = null, bool verboseLogging = false)
    {
        string resolvedPath = ResolvePath(nifPath);
        var loader = new Civ4NifLoader();
        var scene = loader.LoadModel(resolvedPath, bakeTransforms: bakeTransforms);
        Console.WriteLine($"[INFO] Loaded animated NIF model \"{Path.GetFileName(resolvedPath)}\" ({scene.Model.Meshes.Count} mesh(es), {scene.SkinInstances.Count} skin instance(s))");
        var obj = new AnimatedNifSceneObject(scene, Path.GetDirectoryName(resolvedPath) ?? string.Empty, verboseLogging);
        obj.TryAutoLoadAnimations(animationPath);
        return obj;
    }

    public void Update(float deltaTime)
    {
        _elapsedTime += deltaTime;
        _controllerManager.Update(_elapsedTime, _skeleton);
        if (_verboseLogging)
            _verboseTimer += deltaTime;
    }

    public void Render(Shader shader, Matrix4 view, Matrix4 proj)
    {
        Matrix4 baseModel =
            Matrix4.CreateRotationY(_elapsedTime * 0.2f) *
            Matrix4.CreateScale(_autoScale);

        shader.Use();
        shader.SetMatrix4("uView", ref view);
        shader.SetMatrix4("uProj", ref proj);

        var blockTransforms = ComputeGeometryTransforms();
        var (centeredTransforms, geometryCenter) = CenterGeometryTransforms(blockTransforms);
        if (_verboseLogging && _verboseTimer >= VerboseIntervalSeconds)
        {
            LogVerbose(blockTransforms, geometryCenter);
            _verboseTimer -= VerboseIntervalSeconds;
            if (_verboseTimer < 0f)
                _verboseTimer = 0f;
        }
        _scene.Model.Draw(shader, centeredTransforms, baseModel);
        DrawDebugMarkers(shader, blockTransforms, geometryCenter, baseModel);
    }

    public void Dispose()
    {
        _debugMarker.Dispose();
        _scene.Model.Dispose();
    }

    public void LoadKfSequence(string path)
    {
        try
        {
            var loader = new AnimationSequenceLoader();
            string resolved = ResolvePath(path);
            var sequences = loader.LoadSequences(resolved);
            Console.WriteLine($"[INFO] {sequences.Count} sequence(s) parsed from \"{resolved}\".");

            if (sequences.Count == 0)
                return;

            _controllerManager.Clear();
            foreach (var sequence in sequences)
            {
                if (sequence.Tracks.Count == 0)
                {
                    Console.WriteLine($"[WARN] Sequence \"{sequence.Name}\" did not declare any tracks.");
                    continue;
                }

                var controller = CreateController(sequence);
                _controllerManager.AddController(controller);
                Console.WriteLine($"[INFO] Added controller \"{controller.Name}\" with {sequence.Tracks.Count} track(s).");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[WARN] Failed to load animation sequence \"{path}\": {ex.Message}");
        }
    }

    private NiControllerSequence CreateController(AnimationSequence sequence)
    {
        var controller = new NiControllerSequence
        {
            Name = sequence.Name
        };

        if (sequence.StopTime <= sequence.StartTime || sequence.StopTime - sequence.StartTime < float.Epsilon)
        {
            controller.StartTime = 0f;
            controller.StopTime = 1f;
        }
        else
        {
            controller.StartTime = sequence.StartTime;
            controller.StopTime = sequence.StopTime;
        }

        foreach (var track in sequence.Tracks)
        {
            controller.AddControlledBlock(new ControlledBlock(track.NodeName, (normalizedTime, bone) =>
            {
                return track.Sample(normalizedTime);
            }));
        }

        return controller;
    }

    private void TryAutoLoadAnimations(string? explicitPath)
    {
        if (!string.IsNullOrEmpty(explicitPath) && TryLoadAnimationFile(explicitPath))
            return;

        if (string.IsNullOrEmpty(_modelDirectory) || !Directory.Exists(_modelDirectory))
            return;

        foreach (var catalog in Directory.GetFiles(_modelDirectory, "*.kfm"))
        {
            if (TryLoadCatalog(catalog))
                return;
        }

        foreach (var candidate in Directory.GetFiles(_modelDirectory, "*.kf"))
        {
            LoadKfSequence(candidate);
            return;
        }
    }

    private bool TryLoadCatalog(string catalogPath)
    {
        try
        {
            var candidates = ExtractKfNamesFromCatalog(catalogPath);
            if (candidates.Count == 0)
                return false;

            string directory = Path.GetDirectoryName(catalogPath) ?? string.Empty;
            foreach (var entry in candidates)
            {
                string candidatePath = Path.Combine(directory, entry);
                if (File.Exists(candidatePath))
                {
                    Console.WriteLine($"[INFO] Loading animation from catalog entry \"{entry}\".");
                    LoadKfSequence(candidatePath);
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[WARN] Failed to parse animation catalog \"{catalogPath}\": {ex.Message}");
        }

        return false;
    }

    private bool TryLoadAnimationFile(string animationPath)
    {
        if (!File.Exists(animationPath))
        {
            Console.WriteLine($"[WARN] Animation file \"{animationPath}\" was not found.");
            return false;
        }

        LoadKfSequence(animationPath);
        return true;
    }

    private static IReadOnlyList<string> ExtractKfNamesFromCatalog(string catalogPath)
    {
        var names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var buffer = new StringBuilder();
        byte[] data = File.ReadAllBytes(catalogPath);

        foreach (byte value in data)
        {
            if (value >= 0x20 && value < 0x7F)
            {
                buffer.Append((char)value);
                continue;
            }

            if (buffer.Length > 0)
            {
                AddCandidate(buffer, names);
                buffer.Clear();
            }
        }

        if (buffer.Length > 0)
            AddCandidate(buffer, names);

        return new List<string>(names);
    }

    private static void AddCandidate(StringBuilder buffer, HashSet<string> names)
    {
        string value = buffer.ToString();
        if (value.EndsWith(".kf", StringComparison.OrdinalIgnoreCase))
            names.Add(value);
    }

    private static string ResolvePath(string path)
    {
        if (File.Exists(path))
            return path;

        string candidate = Path.Combine(AppContext.BaseDirectory, path);
        if (File.Exists(candidate))
            return candidate;

        candidate = Path.Combine(Directory.GetCurrentDirectory(), path);
        if (File.Exists(candidate))
            return candidate;

        throw new FileNotFoundException("NIF file could not be located for animated scene.", path);
    }

    private static Dictionary<int, int> BuildNodeParentMap(IReadOnlyList<NodeInfo> nodes)
    {
        var parentMap = new Dictionary<int, int>();
        var lookup = nodes.ToDictionary(node => node.BlockIndex);

        foreach (var node in nodes)
        {
            foreach (int child in node.Children)
            {
                if (lookup.ContainsKey(child))
                    parentMap[child] = node.BlockIndex;
            }
        }

        return parentMap;
    }

    private void LogVerbose(IReadOnlyList<TransformData> geometryTransforms, Vector3 geometryCenter)
    {
        int runtimeNodeCount = _debugRuntimeTransforms?.Count ?? 0;
        float minNodeScale = float.MaxValue;
        float maxNodeScale = float.MinValue;
        float sumNodeScale = 0f;
        int zeroNodeCount = 0;

        if (_debugRuntimeTransforms != null && runtimeNodeCount > 0)
        {
            foreach (var transform in _debugRuntimeTransforms.Values)
            {
                float scale = transform.Scale;
                minNodeScale = Math.Min(minNodeScale, scale);
                maxNodeScale = Math.Max(maxNodeScale, scale);
                sumNodeScale += scale;
                if (scale <= 0f)
                    zeroNodeCount++;
            }
        }

        if (runtimeNodeCount == 0)
        {
            minNodeScale = 0f;
            maxNodeScale = 0f;
        }

        float avgNodeScale = runtimeNodeCount > 0 ? sumNodeScale / runtimeNodeCount : 0f;
        int zeroScaleBlocks = geometryTransforms.Count(t => t.Scale <= 0f);
        int totalBlocks = geometryTransforms.Count;

        Console.WriteLine($"[VERBOSE] Nodes: {runtimeNodeCount}, Controllers: {_controllerManager.ControllerCount}, Meshes: {_scene.Model.Meshes.Count}, Skin instances: {_scene.SkinInstances.Count}");
        Console.WriteLine($"[VERBOSE] Node scale range {minNodeScale:F3}..{maxNodeScale:F3} (avg {avgNodeScale:F3}), zero-scale nodes: {zeroNodeCount}");
        Console.WriteLine($"[VERBOSE] Geometry blocks: {totalBlocks}, zero-scale blocks: {zeroScaleBlocks}");
        Console.WriteLine($"[VERBOSE] Geometry center ≈ {FormatVector3(geometryCenter)}");

        var meshDetails = geometryTransforms
            .Select((transform, idx) =>
            {
                var block = _scene.GeometryBlocks[idx];
                string type = block.HasSkin ? "skinned" : "static";
                var translation = transform.Translation;
                float distance = translation.Length;
                return $"{block.BlockIndex}:scale={transform.Scale:F3},dist={distance:F3},{type}";
            })
            .Take(8);
        Console.WriteLine($"[VERBOSE] Mesh detail heads (block:scale/dist/type) ≈ {string.Join(", ", meshDetails)}");
        var farBlocks = geometryTransforms
            .Select((transform, idx) => new { idx, transform })
            .OrderByDescending(entry => entry.transform.Translation.LengthSquared)
            .Take(4)
            .Select(entry =>
            {
                var block = _scene.GeometryBlocks[entry.idx];
                string translation = FormatVector3(entry.transform.Translation);
                float distance = entry.transform.Translation.Length;
                return $"{block.BlockIndex}:{translation},dist={distance:F2}";
            })
            .ToList();
        if (farBlocks.Count > 0)
            Console.WriteLine($"[VERBOSE] Far block translations ≈ {string.Join(", ", farBlocks)}");
    }

    private IReadOnlyList<TransformData> ComputeGeometryTransforms()
    {
        var runtimeTransforms = new Dictionary<int, TransformData>();
        var visited = new HashSet<int>();

        foreach (var nodeIndex in _nodeLookup.Keys)
        {
            if (_nodeParents.ContainsKey(nodeIndex))
                continue;

            TraverseNode(nodeIndex, TransformData.Identity, runtimeTransforms, visited);
        }

        _debugRuntimeTransforms = runtimeTransforms;
        var transforms = new List<TransformData>(_scene.GeometryBlocks.Count);
        foreach (var block in _scene.GeometryBlocks)
        {
            if (runtimeTransforms.TryGetValue(block.BlockIndex, out var world))
            {
                transforms.Add(world);
            }
            else
            {
                transforms.Add(block.WorldTransform);
            }
        }

        return transforms;
    }

    private static (List<TransformData> centered, Vector3 center) CenterGeometryTransforms(IReadOnlyList<TransformData> transforms)
    {
        var centered = new List<TransformData>(transforms.Count);
        if (transforms.Count == 0)
            return (centered, Vector3.Zero);

        Vector3 accumulated = Vector3.Zero;
        foreach (var transform in transforms)
            accumulated += transform.Translation;

        Vector3 center = accumulated / transforms.Count;
        foreach (var transform in transforms)
        {
            var adjusted = new TransformData(transform.Translation - center, transform.Rotation, transform.Scale);
            centered.Add(adjusted);
        }

        return (centered, center);
    }

    private void DrawDebugMarkers(Shader shader, IReadOnlyList<TransformData> geometryTransforms, Vector3 center, Matrix4 baseModel)
    {
        if (geometryTransforms.Count == 0)
            return;

        foreach (var transform in geometryTransforms)
        {
            Vector3 offset = transform.Translation - center;
            Matrix4 placement = Matrix4.CreateTranslation(offset) * Matrix4.CreateScale(MarkerScale);
            Matrix4 markerTransform = baseModel * placement;
            _debugMarker.Draw(shader, null, markerTransform);
        }
    }

    private static string FormatVector3(Vector3 vector)
    {
        return $"({vector.X:F2},{vector.Y:F2},{vector.Z:F2})";
    }

    private void TraverseNode(int nodeIndex, TransformData parentWorld, Dictionary<int, TransformData> output, HashSet<int> visited)
    {
        if (!visited.Add(nodeIndex))
            return;

        TransformData local = _skeleton.GetLocalTransform(nodeIndex);
        TransformData world = TransformData.Combine(parentWorld, local);
        output[nodeIndex] = world;

        if (!_nodeLookup.TryGetValue(nodeIndex, out var node))
            return;

        foreach (int child in node.Children)
        {
            if (child < 0)
                continue;

            if (_nodeLookup.ContainsKey(child))
            {
                TraverseNode(child, world, output, visited);
                continue;
            }

            if (_geometryLookup.TryGetValue(child, out var geometry))
            {
                var geometryWorld = TransformData.Combine(world, geometry.LocalTransform);
                output[child] = geometryWorld;
            }
        }
    }

    private static float ComputeAutoScale(Model model)
    {
        float maxAbs = 0f;
        foreach (var mesh in model.Meshes)
        {
            var positions = mesh.Positions;
            for (int i = 0; i < positions.Length; i++)
            {
                float value = MathF.Abs(positions[i]);
                if (value > maxAbs)
                    maxAbs = value;
            }
        }

        if (maxAbs <= float.Epsilon)
            return 1f;

        return 1f / maxAbs;
    }
}
