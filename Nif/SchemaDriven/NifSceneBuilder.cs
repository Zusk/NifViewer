using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Mathematics;

/// <summary>
/// Simple conversion layer that maps generic NIF block instances into a lightweight
/// scene graph structure. It is intentionally conservative and only understands a
/// handful of common blocks (NiNode, NiTriShape, NiTriShapeData).
/// </summary>
public sealed class NifSceneBuilder
{
    public NifScene BuildScene(NifFile file)
    {
        var scene = new NifScene();
        var blockToNode = new Dictionary<NifBlockInstance, NifSceneNode>();
        var referencedChildren = new HashSet<NifBlockInstance>();

        // First pass: create nodes for NiNode blocks
        foreach (var block in file.Blocks.Where(b => b.TypeName.Equals("NiNode", StringComparison.Ordinal)))
        {
            var node = new NifSceneNode
            {
                Name = block.Fields.TryGetValue("name", out var nameValue) ? nameValue?.ToString() ?? string.Empty : block.TypeName
            };
            blockToNode[block] = node;
        }

        // Second pass: attach children
        foreach (var (block, node) in blockToNode)
        {
            if (block.Fields.TryGetValue("children", out var childObj))
            {
                var children = ExtractReferences(childObj);
                foreach (var child in children)
                {
                    if (child != null && blockToNode.TryGetValue(child, out var childNode))
                    {
                        node.Children.Add(childNode);
                        referencedChildren.Add(child);
                    }
                    else if (child != null && child.TypeName.Equals("NiTriShape", StringComparison.Ordinal))
                    {
                        var mesh = BuildMesh(child);
                        if (mesh != null)
                            node.Meshes.Add(mesh);
                        referencedChildren.Add(child);
                    }
                }
            }
        }

        // Any NiTriShape that isn't attached to a NiNode is treated as a root mesh
        foreach (var block in file.Blocks.Where(b => b.TypeName.Equals("NiTriShape", StringComparison.Ordinal)))
        {
            if (referencedChildren.Contains(block))
                continue;

            var mesh = BuildMesh(block);
            if (mesh != null)
            {
                var rootNode = new NifSceneNode { Name = block.TypeName };
                rootNode.Meshes.Add(mesh);
                scene.Roots.Add(rootNode);
            }
        }

        // Roots are NiNodes that were never referenced as children
        foreach (var (block, node) in blockToNode)
        {
            if (!referencedChildren.Contains(block))
                scene.Roots.Add(node);
        }

        return scene;
    }

    private NifMesh? BuildMesh(NifBlockInstance triShape)
    {
        if (!triShape.Fields.TryGetValue("data", out var dataRef))
            return null;

        var dataBlock = dataRef as NifBlockInstance;
        if (dataBlock == null)
            return null;

        var mesh = new NifMesh();

        if (dataBlock.Fields.TryGetValue("vertices", out var vertsObj))
        {
            foreach (var v in ExtractVector3Array(vertsObj))
                mesh.Vertices.Add(v);
        }

        if (dataBlock.Fields.TryGetValue("triangles", out var triObj))
        {
            foreach (var t in ExtractTriangleIndices(triObj))
            {
                mesh.Indices.AddRange(t);
            }
        }

        return mesh.Vertices.Count > 0 && mesh.Indices.Count > 0 ? mesh : null;
    }

    private static IEnumerable<NifBlockInstance?> ExtractReferences(object? value)
    {
        switch (value)
        {
            case NifBlockInstance single:
                yield return single;
                break;
            case NifBlockInstance?[] arr:
                foreach (var item in arr)
                    yield return item;
                break;
        }
    }

    private static IEnumerable<Vector3> ExtractVector3Array(object? value)
    {
        if (value is object?[] arr)
        {
            foreach (var entry in arr)
            {
                if (TryConvertVector3(entry, out var vec))
                    yield return vec;
            }
        }
    }

    private static bool TryConvertVector3(object? value, out Vector3 vec)
    {
        if (value is Dictionary<string, object?> dict)
        {
            vec = new Vector3(
                ReadFloat(dict, "x"),
                ReadFloat(dict, "y"),
                ReadFloat(dict, "z"));
            return true;
        }

        vec = Vector3.Zero;
        return false;
    }

    private static IEnumerable<uint[]> ExtractTriangleIndices(object? value)
    {
        if (value is object?[] arr)
        {
            foreach (var entry in arr)
            {
                if (entry is Dictionary<string, object?> dict)
                {
                    uint a = (uint)ReadFloat(dict, "v1");
                    uint b = (uint)ReadFloat(dict, "v2");
                    uint c = (uint)ReadFloat(dict, "v3");
                    yield return new[] { a, b, c };
                }
            }
        }
    }

    private static float ReadFloat(Dictionary<string, object?> dict, string key)
    {
        if (dict.TryGetValue(key, out var value))
        {
            return value switch
            {
                float f => f,
                double d => (float)d,
                int i => i,
                uint u => u,
                short s => s,
                ushort us => us,
                _ when float.TryParse(value?.ToString(), out var parsed) => parsed,
                _ => 0f
            };
        }

        return 0f;
    }
}

public sealed class NifScene
{
    public List<NifSceneNode> Roots { get; } = new();
}

public sealed class NifSceneNode
{
    public string Name { get; set; } = string.Empty;
    public List<NifSceneNode> Children { get; } = new();
    public List<NifMesh> Meshes { get; } = new();
}

public sealed class NifMesh
{
    public List<Vector3> Vertices { get; } = new();
    public List<uint> Indices { get; } = new();
}
