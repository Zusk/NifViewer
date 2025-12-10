using System;
using System.IO;

/// <summary>
/// Reads the block offset table for Civ4 NIFs.
/// For each block, a uint32 absolute file offset where that block begins.
/// </summary>
public static class NifBlockOffsets
{
    public static uint[] ReadBlockOffsets(BinaryReader br, NifHeader header)
    {
        uint numBlocks = header.NumBlocks;
        uint[] offsets = new uint[numBlocks];

        Console.WriteLine("[NIF] BLOCK OFFSETS TABLE");
        Console.WriteLine($"NumBlocks = {numBlocks}");

        long start = br.BaseStream.Position;

        for (int i = 0; i < numBlocks; i++)
        {
            offsets[i] = br.ReadUInt32();
        }

        for (int i = 0; i < numBlocks; i++)
        {
            Console.WriteLine($"Block {i}: offset={offsets[i]}");
        }

        Console.WriteLine();
        return offsets;
    }
}

