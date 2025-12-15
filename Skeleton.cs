using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Runtime skeleton representation built from nodes and skin instances.
/// </summary>
public sealed class Skeleton
{
    private readonly Dictionary<int, NodeInfo> _nodesByIndex = new();
    private readonly Dictionary<string, NodeInfo> _nodesByName = new();
    private readonly List<NodeInfo> _nodeList = new();
    private readonly Dictionary<string, SkeletonBone> _bonesByName = new();
    private readonly Dictionary<int, TransformData> _localOverrides = new();
    private readonly List<SkeletonBone> _bones = new();

    public Skeleton(NifScene scene)
    {
        foreach (var node in scene.Nodes)
        {
            _nodesByIndex[node.BlockIndex] = node;
            string? nodeName = node.Name;
            if (!string.IsNullOrEmpty(nodeName))
                _nodesByName[nodeName] = node;
            _nodeList.Add(node);
        }

        foreach (var skin in scene.SkinInstances)
        {
            for (int boneIndex = 0; boneIndex < skin.Bones.Count; boneIndex++)
            {
                int nodeIndex = skin.Bones[boneIndex];
                if (!_nodesByIndex.TryGetValue(nodeIndex, out var node))
                    continue;

                var bindInfo = boneIndex < skin.SkinBones.Count
                    ? skin.SkinBones[boneIndex]
                    : new SkinBoneInfo(TransformData.Identity, default, 0f, Array.Empty<VertexWeightInfo>());

                var bone = new SkeletonBone(node, bindInfo, boneIndex, skin);
                _bones.Add(bone);
                string? nodeName = node.Name;
                if (!string.IsNullOrEmpty(nodeName))
                    _bonesByName[nodeName] = bone;
            }
        }
    }

    public IReadOnlyList<NodeInfo> Nodes => _nodeList;
    public IReadOnlyList<SkeletonBone> Bones => _bones;
    public IReadOnlyDictionary<string, SkeletonBone> BonesByName => _bonesByName;

    public void ResetOverrides() => _localOverrides.Clear();

    public void SetOverride(int nodeIndex, TransformData transform) =>
        _localOverrides[nodeIndex] = transform;

    public TransformData GetLocalTransform(int nodeIndex)
    {
        if (_localOverrides.TryGetValue(nodeIndex, out var overrideTransform))
            return overrideTransform;
        if (_nodesByIndex.TryGetValue(nodeIndex, out var node))
            return node.LocalTransform;
        return TransformData.Identity;
    }

    public bool TryGetBoneByName(string name, out SkeletonBone? bone) =>
        _bonesByName.TryGetValue(name, out bone);
}

public sealed class SkeletonBone
{
    public SkeletonBone(NodeInfo node, SkinBoneInfo bindInfo, int boneIndex, SkinInstanceInfo owner)
    {
        Node = node;
        BindInfo = bindInfo;
        BoneIndex = boneIndex;
        Owner = owner;
    }

    public NodeInfo Node { get; }
    public SkinBoneInfo BindInfo { get; }
    public int BoneIndex { get; }
    public SkinInstanceInfo Owner { get; }
}
