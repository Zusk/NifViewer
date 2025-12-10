using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

/// <summary>
/// Reads the NIF string palette (count, max length, and strings).
/// Keeping this in sync with the file layout prevents block parsing
/// from starting at the wrong offset.
/// </summary>
public static class NifStringPalette
{
    public static List<string> ReadStrings(BinaryReader br)
    {
        uint numStrings = br.ReadUInt32();
        uint maxStringLen = br.ReadUInt32();

        Console.WriteLine("[NIF] STRING PALETTE");
        Console.WriteLine($"NumStrings = {numStrings}");
        Console.WriteLine($"MaxStringLength = {maxStringLen}");

        var strings = new List<string>((int)numStrings);

        for (int i = 0; i < numStrings; i++)
        {
            uint len = br.ReadUInt32();

            // The Civ4 NIFs use sized strings with ASCII encoding.
            string s = len == 0xFFFFFFFF
                ? string.Empty
                : Encoding.ASCII.GetString(br.ReadBytes((int)len));

            strings.Add(s);
            Console.WriteLine($"[{i}] {s}");
        }

        Console.WriteLine();
        return strings;
    }
}
