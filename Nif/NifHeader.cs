using System;
using System.IO;
using System.Text;

/// <summary>
/// Parsed header for a Civ4-style Gamebryo 20.0.0.4 NIF.
/// No heuristics, strictly matches Civ4 layout.
/// </summary>
public sealed class NifHeader
{
    public string HeaderString { get; }
    public uint Version { get; }
    public uint UserVersion { get; }
    public byte EndianType { get; }
    public uint NumBlocks { get; }
    public ushort NumTypes { get; }

    /// <summary>Length in bytes of the first type name in the type dictionary.</summary>
    public ushort FirstTypeNameLength { get; }

    private NifHeader(
        string headerString,
        uint version,
        uint userVersion,
        byte endianType,
        uint numBlocks,
        ushort numTypes,
        ushort firstTypeNameLength)
    {
        HeaderString = headerString;
        Version = version;
        UserVersion = userVersion;
        EndianType = endianType;
        NumBlocks = numBlocks;
        NumTypes = numTypes;
        FirstTypeNameLength = firstTypeNameLength;
    }

    /// <summary>
    /// Reads a Civ4-style NIF header.
    /// Layout:
    ///   char[] headerString (C-string terminated by '\n' or '\0')
    ///   uint32 version
    ///   uint32 userVersion
    ///   uint8  endianType
    ///   uint32 numBlocks
    ///   uint16 numTypes
    ///   uint16 firstTypeNameLength
    ///   uint16 padding (0)
    /// </summary>
    public static NifHeader Read(BinaryReader br)
    {
        string header = ReadHeaderString(br);

        uint version = br.ReadUInt32();
        uint userVersion = br.ReadUInt32();

        // Civ4 NIFs always include an endian byte (0 or 1).
        byte endianType = br.ReadByte();

        uint numBlocks = br.ReadUInt32();
        ushort numTypes = br.ReadUInt16();
        ushort firstTypeLen = br.ReadUInt16();
        ushort padding = br.ReadUInt16(); // should be 0

        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("[NIF] HEADER");
        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine(header);
        Console.WriteLine($"Version:          0x{version:X8}");
        Console.WriteLine($"UserVersion:      {userVersion}");
        Console.WriteLine($"EndianType:       {endianType}");
        Console.WriteLine($"NumBlocks:        {numBlocks}");
        Console.WriteLine($"NumTypes:         {numTypes}");
        Console.WriteLine($"FirstTypeNameLen: {firstTypeLen}");
        Console.WriteLine($"Padding:          0x{padding:X4}");
        Console.WriteLine();

        return new NifHeader(
            header,
            version,
            userVersion,
            endianType,
            numBlocks,
            numTypes,
            firstTypeLen);
    }

    private static string ReadHeaderString(BinaryReader br)
    {
        var bytes = new System.Collections.Generic.List<byte>();
        while (true)
        {
            byte b = br.ReadByte();
            if (b == 0x0A || b == 0x00) // newline or null terminator
                break;
            bytes.Add(b);
        }

        // Trim trailing CR if present
        if (bytes.Count > 0 && bytes[^1] == 0x0D)
            bytes.RemoveAt(bytes.Count - 1);

        return Encoding.ASCII.GetString(bytes.ToArray());
    }
}

