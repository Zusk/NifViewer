/// <summary>
/// Describes a geometry block (tri shape or strips) within a loaded NIF scene.
/// </summary>
public enum GeometryType
{
    NiTriShape,
    NiTriStrips
}

public sealed class GeometryBlockInfo
{
    public GeometryBlockInfo(
        int blockIndex,
        GeometryType geometryType,
        int dataBlockIndex,
        TransformData localTransform,
        TransformData worldTransform,
        int skinInstanceRef,
        int controllerRef)
    {
        BlockIndex = blockIndex;
        GeometryType = geometryType;
        DataBlockIndex = dataBlockIndex;
        LocalTransform = localTransform;
        WorldTransform = worldTransform;
        SkinInstanceRef = skinInstanceRef;
        ControllerRef = controllerRef;
    }

    public int BlockIndex { get; }
    public GeometryType GeometryType { get; }
    public int DataBlockIndex { get; }
    public TransformData LocalTransform { get; }
    public TransformData WorldTransform { get; }
    public int SkinInstanceRef { get; }
    public int ControllerRef { get; }

    public bool HasSkin => SkinInstanceRef >= 0;
    public bool HasController => ControllerRef >= 0;
}
