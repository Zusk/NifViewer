using System;
using System.IO;

/// <summary>
/// Reads the block type index table for Civ4 NIFs.
/// For each block, a 16-bit index into the type dictionary.
/// No heuristics.
/// </summary>
public static class NifBlockIndexTable
{
    public static int[] ReadBlockTypeIndices(BinaryReader br, NifHeader header, string[] blockTypes)
    {
        uint numBlocks = header.NumBlocks;
        int[] indices = new int[numBlocks];

        Console.WriteLine("[NIF] BLOCK TYPE INDEX TABLE");
        Console.WriteLine($"NumBlocks = {numBlocks}");

        for (int i = 0; i < numBlocks; i++)
        {
            // The NIF header stores the indices as 16-bit values that point
            // into the type dictionary. Niflib follows the same layout.
            int idx = br.ReadUInt16();
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

