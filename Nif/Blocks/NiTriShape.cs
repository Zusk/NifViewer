using System.IO;
using System.Collections.Generic;

/// <summary>
/// A renderable mesh shape referencing NiTriShapeData.
/// </summary>
public sealed class NiTriShape : NiAVObject
{
    public int DataIndex { get; private set; } = -1;
    public int SkinInstanceIndex { get; private set; } = -1;

    public List<int> PropertyIndices { get; } = new();

    public override void Read(BinaryReader br, NifContext ctx)
    {
        base.Read(br, ctx);

        DataIndex = br.ReadInt32();
        SkinInstanceIndex = br.ReadInt32();

        ushort numProps = br.ReadUInt16();
        PropertyIndices.Clear();
        for (int i = 0; i < numProps; i++)
            PropertyIndices.Add(br.ReadInt32());
    }
}

