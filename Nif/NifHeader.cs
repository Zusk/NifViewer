using System;
using System.IO;

namespace NifViewer.Nif;

/// <summary>
/// Lightweight Civ4 NIF header reader. It captures the block list and type dictionary
/// exactly as stored in the file without attempting to interpret additional metadata.
/// </summary>
public sealed class NifHeader
{
    public string HeaderString { get; private set; } = string.Empty;
    public string VersionString { get; private set; } = string.Empty;
    public uint Version { get; private set; }
    public byte Endian { get; private set; }
    public uint UserVersion { get; private set; }
    public uint BlockCount { get; private set; }
    public ushort BlockTypeCount { get; private set; }
    public string[] BlockTypes { get; private set; } = Array.Empty<string>();
    public string[] Blocks { get; private set; } = Array.Empty<string>();

    // Backwards-compatibility members for legacy readers.
    public byte EndianType => Endian;
    public uint NumBlocks => BlockCount;
    public ushort NumTypes => BlockTypeCount;

    public void Read(BinaryReader reader)
    {
        HeaderString = reader.ReadNullTerminatedString();

        // Civ4 stores the version as 4 separate bytes (20.0.0.4)
        byte b0 = reader.ReadByte();
        byte b1 = reader.ReadByte();
        byte b2 = reader.ReadByte();
        byte b3 = reader.ReadByte();

        VersionString = string.Join('.', b0, b1, b2, b3);
        Version = (uint)((b0 << 24) | (b1 << 16) | (b2 << 8) | b3);

        Endian = reader.ReadByte();
        UserVersion = reader.ReadUInt32();
        BlockCount = reader.ReadUInt32();
        BlockTypeCount = reader.ReadUInt16();

        BlockTypes = new string[BlockTypeCount];
        for (int i = 0; i < BlockTypeCount; i++)
            BlockTypes[i] = reader.ReadStringWithLength();

        Blocks = new string[BlockCount];
        for (int i = 0; i < BlockCount; i++)
        {
            var typeIndex = reader.ReadUInt16();
            Blocks[i] = typeIndex < BlockTypes.Length ? BlockTypes[typeIndex] : "NiUnknown";
        }

        // Group metadata appears here in Civ4 files; it is rarely present so we skip it.
        uint groupCount = reader.ReadUInt32();
        if (groupCount > 0)
        {
            // Skip group indices to keep the reader aligned for the block stream.
            reader.ReadBytes((int)(groupCount * sizeof(uint)));
        }
    }

    public static NifHeader Read(BinaryReader reader)
    {
        var header = new NifHeader();
        header.Read(reader);
        return header;
    }
}
