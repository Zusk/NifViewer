using System.Collections.Generic;
using OpenTK.Mathematics;

/// <summary>
/// Summarizes a skin instance so animation systems can build weighted skeletons without needing private block classes.
/// </summary>
public sealed class SkinInstanceInfo
{
    public SkinInstanceInfo(
        int blockIndex,
        int dataBlockIndex,
        int skinPartitionRef,
        int skeletonRootRef,
        IReadOnlyList<int> bones,
        IReadOnlyList<SkinBoneInfo> skinBones)
    {
        BlockIndex = blockIndex;
        DataBlockIndex = dataBlockIndex;
        SkinPartitionRef = skinPartitionRef;
        SkeletonRootRef = skeletonRootRef;
        Bones = bones;
        SkinBones = skinBones;
    }

    public int BlockIndex { get; }
    public int DataBlockIndex { get; }
    public int SkinPartitionRef { get; }
    public int SkeletonRootRef { get; }
    public IReadOnlyList<int> Bones { get; }
    public IReadOnlyList<SkinBoneInfo> SkinBones { get; }
}

public sealed class SkinBoneInfo
{
    public SkinBoneInfo(TransformData bindTransform, Vector3 boundingCenter, float boundingRadius, IReadOnlyList<VertexWeightInfo> weights)
    {
        BindTransform = bindTransform;
        BoundingCenter = boundingCenter;
        BoundingRadius = boundingRadius;
        VertexWeights = weights;
    }

    public TransformData BindTransform { get; }
    public Vector3 BoundingCenter { get; }
    public float BoundingRadius { get; }
    public IReadOnlyList<VertexWeightInfo> VertexWeights { get; }
}

public sealed class VertexWeightInfo
{
    public VertexWeightInfo(int index, float weight)
    {
        Index = index;
        Weight = weight;
    }

    public int Index { get; }
    public float Weight { get; }
}
