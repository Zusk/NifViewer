using System.IO;

/// <summary>
/// Civ4-style texturing property: contains a base texture index.
/// </summary>
public sealed class NiTexturingProperty : NiObject
{
    public int BaseTextureIndex { get; private set; } = -1;

    public override void Read(BinaryReader br, NifContext ctx)
    {
        base.Read(br, ctx);

        ushort flags = br.ReadUInt16();
        byte hasBase = br.ReadByte();

        if (hasBase != 0)
        {
            br.ReadByte(); // unknown
            br.ReadByte(); // unknown
            BaseTextureIndex = br.ReadInt32();
        }
    }
}

