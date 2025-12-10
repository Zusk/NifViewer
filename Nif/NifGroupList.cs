using System;
using System.IO;

/// <summary>
/// Reads the optional group list that follows the string palette in the header.
/// </summary>
public static class NifGroupList
{
    public static (uint NumGroups, uint[] Groups, bool Consumed) Read(BinaryReader br)
    {
        long startPos = br.BaseStream.Position;
        long bytesRemaining = br.BaseStream.Length - startPos;

        if (bytesRemaining < sizeof(uint))
        {
            Console.WriteLine($"[NIF] GROUPS not present at 0x{startPos:X}; no bytes left for count. Defaulting to zero groups.");
            return (0, Array.Empty<uint>(), false);
        }

        uint numGroups = br.ReadUInt32();
        long bytesAfterCount = br.BaseStream.Length - br.BaseStream.Position;
        long bytesRequired = (long)numGroups * sizeof(uint);

        if (numGroups > int.MaxValue || bytesRequired > bytesAfterCount)
        {
            Console.WriteLine($"[NIF] GROUPS count {numGroups} is invalid for remaining {bytesAfterCount} bytes. Rewinding and skipping groups.");
            br.BaseStream.Position = startPos;
            return (0, Array.Empty<uint>(), false);
        }

        var groups = new uint[numGroups];

        Console.WriteLine("[NIF] GROUPS");
        Console.WriteLine($"NumGroups = {numGroups}");

        for (int i = 0; i < numGroups; i++)
        {
            groups[i] = br.ReadUInt32();
            Console.WriteLine($"[{i}] {groups[i]}");
        }

        Console.WriteLine();
        return (numGroups, groups, true);
    }
}
