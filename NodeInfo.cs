using System.Collections.Generic;

/// <summary>
/// Metadata collected from a NiNode block so the animation subsystem can build skeletons.
/// </summary>
public sealed class NodeInfo
{
    public NodeInfo(int blockIndex, string? name, TransformData localTransform, TransformData worldTransform, IReadOnlyList<int> children, int controllerRef)
    {
        BlockIndex = blockIndex;
        Name = name;
        LocalTransform = localTransform;
        WorldTransform = worldTransform;
        Children = children;
        ControllerRef = controllerRef;
    }

    public int BlockIndex { get; }
    public string? Name { get; }
    public TransformData LocalTransform { get; }
    public TransformData WorldTransform { get; }
    public IReadOnlyList<int> Children { get; }
    public int ControllerRef { get; }
}
