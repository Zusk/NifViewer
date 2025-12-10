using System;
using System.IO;

/// <summary>
/// Extends NiObject with: name index, extra data array, controller index.
/// </summary>
public abstract class NiObjectNET : NiObject
{
    public int NameIndex { get; protected set; } = -1;
    public int[] ExtraDataIndices { get; private set; } = Array.Empty<int>();
    public int ControllerIndex { get; private set; } = -1;

    public override void Read(BinaryReader br, NifContext ctx)
    {
        base.Read(br, ctx);

        // ---- Name Index ----
        NameIndex = br.ReadInt32();

        // ---- Extra Data ----
        ushort extraCount = br.ReadUInt16();
        ExtraDataIndices = new int[extraCount];
        for (int i = 0; i < extraCount; i++)
            ExtraDataIndices[i] = br.ReadInt32();

        // ---- Controller ----
        ControllerIndex = br.ReadInt32();
    }
}

