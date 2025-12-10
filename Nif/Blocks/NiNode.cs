using System.IO;

/// <summary>
/// Standard scene graph node.
/// </summary>
public sealed class NiNode : NiAVObject
{
    public int NumChildren { get; private set; }
    public int[] Children { get; private set; } = Array.Empty<int>();

    public int NumEffects { get; private set; }
    public int[] Effects { get; private set; } = Array.Empty<int>();

    public override void Read(BinaryReader br, NifContext ctx)
    {
        base.Read(br, ctx);

        NumChildren = br.ReadUInt16();
        Children = new int[NumChildren];
        for (int i = 0; i < NumChildren; i++)
            Children[i] = br.ReadInt32();

        NumEffects = br.ReadUInt16();
        Effects = new int[NumEffects];
        for (int i = 0; i < NumEffects; i++)
            Effects[i] = br.ReadInt32();
    }
}

