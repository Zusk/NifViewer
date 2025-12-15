using System;
using System.Collections.Generic;
using OpenTK.Mathematics;

/// <summary>
/// Minimal controller abstractions to construct animation sequences for the skeleton.
/// </summary>
public sealed class NiControllerManager
{
    private readonly List<INiController> _controllers = new();

    public void AddController(INiController controller)
    {
        if (controller == null) throw new ArgumentNullException(nameof(controller));
        _controllers.Add(controller);
    }

    public void Clear() => _controllers.Clear();

    public void Update(float time, Skeleton skeleton)
    {
        skeleton.ResetOverrides();
        foreach (var controller in _controllers)
        {
            controller.Update(time, skeleton);
        }
    }

    public int ControllerCount => _controllers.Count;
}

public interface INiController
{
    void Update(float time, Skeleton skeleton);
}

public sealed class NiControllerSequence : INiController
{
    private readonly List<ControlledBlock> _controlledBlocks = new();

    public string Name { get; set; } = string.Empty;
    public float StartTime { get; set; } = 0f;
    public float StopTime { get; set; } = 1f;

    public IReadOnlyList<ControlledBlock> ControlledBlocks => _controlledBlocks;

    public void AddControlledBlock(ControlledBlock block)
    {
        if (block == null) throw new ArgumentNullException(nameof(block));
        _controlledBlocks.Add(block);
    }

    public void Update(float time, Skeleton skeleton)
    {
        if (StopTime <= StartTime)
            return;

        float range = StopTime - StartTime;
        float local = (time - StartTime) / range;
        local %= 1f;
        if (local < 0f)
            local += 1f;

        foreach (var block in _controlledBlocks)
            block.Apply(local, skeleton);
    }

    public static NiControllerSequence CreateDemoSpin(string nodeName)
    {
        var sequence = new NiControllerSequence
        {
            Name = "DemoSpin",
            StartTime = 0f,
            StopTime = 5f
        };

        sequence.AddControlledBlock(new ControlledBlock(nodeName, (normalizedTime, bone) =>
        {
            float angle = normalizedTime * MathF.PI * 2f;
            var rotation = bone.Node.LocalTransform.Rotation * Matrix3.CreateRotationY(angle);
            return TransformData.FromComponents(bone.Node.LocalTransform.Translation, rotation, bone.Node.LocalTransform.Scale);
        }));

        return sequence;
    }
}

public sealed class ControlledBlock
{
    private readonly Func<float, SkeletonBone, TransformData> _sampler;

    public ControlledBlock(string nodeName, Func<float, SkeletonBone, TransformData> sampler)
    {
        NodeName = nodeName ?? throw new ArgumentNullException(nameof(nodeName));
        _sampler = sampler ?? throw new ArgumentNullException(nameof(sampler));
    }

    public string NodeName { get; }

    public void Apply(float normalizedTime, Skeleton skeleton)
    {
        if (skeleton.TryGetBoneByName(NodeName, out var bone) && bone != null)
        {
            var sample = _sampler(normalizedTime, bone);
            skeleton.SetOverride(bone.Node.BlockIndex, sample);
        }
    }
}
