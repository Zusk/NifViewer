using System;
using System.IO;

/// <summary>
/// Reads the block type index table for Civ4 NIFs.
/// For each block, a 16-bit index into the type dictionary.
/// No heuristics.
/// </summary>
public static class NifBlockIndexTable
{
    public static short[] ReadBlockTypeIndices(BinaryReader br, NifHeader header, string[] blockTypes)
    {
        uint numBlocks = header.NumBlocks;
        short[] indices = new short[numBlocks];

        Console.WriteLine("[NIF] BLOCK TYPE INDEX TABLE");
        Console.WriteLine($"NumBlocks = {numBlocks}");

        for (int i = 0; i < numBlocks; i++)
        {
            ushort raw = br.ReadUInt16();
            short idx = (short)raw;
            indices[i] = idx;

            string typeName = (idx >= 0 && idx < blockTypes.Length)
                ? blockTypes[idx]
                : "<OUT OF RANGE>";

            Console.WriteLine($"Block {i}: typeIndex={idx} ({typeName})");
        }

        Console.WriteLine();
        return indices;
    }
}

