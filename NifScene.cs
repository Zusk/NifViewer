using System.Collections.Generic;

public sealed class NifScene
{
    public NifScene(
        Model model,
        NifContext context,
        IReadOnlyDictionary<int, TransformData> worldTransforms,
        IReadOnlyList<GeometryBlockInfo> geometryBlocks,
        IReadOnlyList<NodeInfo> nodes,
        IReadOnlyList<SkinInstanceInfo> skinInstances)
    {
        Model = model;
        Context = context;
        WorldTransforms = worldTransforms;
        GeometryBlocks = geometryBlocks;
        Nodes = nodes;
        SkinInstances = skinInstances;
    }

    public Model Model { get; }
    public NifContext Context { get; }
    public IReadOnlyDictionary<int, TransformData> WorldTransforms { get; }
    public IReadOnlyList<GeometryBlockInfo> GeometryBlocks { get; }
    public IReadOnlyList<NodeInfo> Nodes { get; }
    public IReadOnlyList<SkinInstanceInfo> SkinInstances { get; }
}
