using System;
using System.IO;

/// <summary>
/// Reads the optional group list that follows the string palette in the header.
/// </summary>
public static class NifGroupList
{
    public static (uint NumGroups, uint[] Groups) Read(BinaryReader br)
    {
        uint numGroups = br.ReadUInt32();
        var groups = new uint[numGroups];

        Console.WriteLine("[NIF] GROUPS");
        Console.WriteLine($"NumGroups = {numGroups}");

        for (int i = 0; i < numGroups; i++)
        {
            groups[i] = br.ReadUInt32();
            Console.WriteLine($"[{i}] {groups[i]}");
        }

        Console.WriteLine();
        return (numGroups, groups);
    }
}
