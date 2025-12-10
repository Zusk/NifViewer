using System;
using System.IO;

/// <summary>
/// Extends NiObject with: name index, extra data array, controller index.
/// </summary>
public abstract class NiObjectNET : NiObject
{
    public string Name { get; protected set; } = string.Empty;
    public int[] ExtraDataIndices { get; private set; } = Array.Empty<int>();
    public int ControllerIndex { get; private set; } = -1;

    public override void Read(BinaryReader br, NifContext ctx)
    {
        base.Read(br, ctx);

        // ---- Name ----
        // niflib reads the name as a string palette index for 10.0+ files
        // (including Civ4 20.0.0.4). Keep the same behaviour here. When the
        // palette is missing (older versions), fall back to an inline sized string
        // to avoid desyncing the stream.
        if (ctx.Strings.Count > 0)
        {
            int nameIndex = br.ReadInt32();
            Name = ctx.GetString(nameIndex);
        }
        else
        {
            Name = NifString.ReadSizedString(br);
        }

        // ---- Extra Data ----
        ushort extraCount = br.ReadUInt16();
        ExtraDataIndices = new int[extraCount];
        for (int i = 0; i < extraCount; i++)
            ExtraDataIndices[i] = br.ReadInt32();

        // ---- Controller ----
        ControllerIndex = br.ReadInt32();
    }
}

