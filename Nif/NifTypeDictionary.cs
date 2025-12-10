using System;
using System.IO;
using System.Text;

/// <summary>
/// Reads the block type dictionary for Civ4-style NIFs.
/// </summary>
public static class NifTypeDictionary
{
    /// <summary>
    /// Layout (Civ4):
    ///   First type name: raw bytes of length header.FirstTypeNameLength (no length prefix)
    ///   Remaining type names: uint32 length + that many bytes (ASCII)
    /// After this, the stream is aligned to a 4-byte boundary before the block type index table.
    /// </summary>
    public static string[] ReadTypeNames(BinaryReader br, NifHeader header)
    {
        int numTypes = header.NumTypes;
        string[] types = new string[numTypes];

        Console.WriteLine("[NIF] TYPE DICTIONARY");
        Console.WriteLine($"NumTypes = {numTypes}");

        // First type name: fixed length from header
        byte[] firstBytes = br.ReadBytes(header.FirstTypeNameLength);
        types[0] = Encoding.ASCII.GetString(firstBytes);
        Console.WriteLine($"[0] {types[0]}");

        // Remaining type names: sized strings
        for (int i = 1; i < numTypes; i++)
        {
            uint len = br.ReadUInt32();
            string name = len > 0
                ? Encoding.ASCII.GetString(br.ReadBytes((int)len))
                : string.Empty;

            types[i] = name;
            Console.WriteLine($"[{i}] {name}");
        }

        Console.WriteLine();

        return types;
    }

    /// <summary>
    /// Aligns the stream to the next 4-byte boundary (if not already aligned).
    /// Civ4 places the block type index table on a 4-byte boundary.
    /// </summary>
    public static void AlignTo4Bytes(BinaryReader br)
    {
        long pos = br.BaseStream.Position;
        long pad = (4 - (pos % 4)) % 4;
        if (pad > 0)
        {
            br.ReadBytes((int)pad);
        }
    }
}

