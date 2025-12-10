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
        // For 10.0+ files, names are always stored as indices into the
        // string palette, even when the palette is empty (value is usually
        // -1). Both pyffi and nifparse follow this rule. Older formats store
        // an inline sized string instead. Using the palette path for modern
        // files avoids desyncing the stream when NumStrings == 0.
        bool usesStringPalette = ctx.Version >= 0x0A000000; // 10.0.0.0
        if (usesStringPalette)
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

