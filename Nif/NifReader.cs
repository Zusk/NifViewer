using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Low-level Civ4 NIF reader. Reads header, type dict, block index,
/// offsets, and all blocks into a NifContext.
/// </summary>
public sealed class NifReader
{
    private readonly BinaryReader _br;
    private readonly NifHeader _header;
    private readonly NifContext _ctx;

    private readonly string[] _blockTypes;
    private readonly int[] _blockTypeIndex;
    private readonly List<string> _strings;

    public NifReader(BinaryReader br)
    {
        _br = br;

        // 1) Header
        _header = NifHeader.Read(_br);

        // 2) Type dictionary
        _blockTypes = NifTypeDictionary.ReadTypeNames(_br, _header);

        // 3) Align to 4-byte boundary
        NifTypeDictionary.AlignTo4Bytes(_br);

        // 4) Block type index table
        _blockTypeIndex = NifBlockIndexTable.ReadBlockTypeIndices(_br, _header, _blockTypes);

        // 5) String palette
        _strings = NifStringPalette.ReadStrings(_br);

        // 6) Build context
        _ctx = new NifContext
        {
            HeaderString = _header.HeaderString,
            Version = _header.Version,
            UserVersion = _header.UserVersion,
            EndianType = _header.EndianType,
            NumBlocks = _header.NumBlocks,
            BlockTypes = _blockTypes,
            BlockTypeIndex = _blockTypeIndex,
            Blocks = new NiObject[_header.NumBlocks],
            Strings = _strings
        };
    }

    /// <summary>
    /// Reads all blocks into the context and returns them as a list.
    /// </summary>
    public List<NiObject> ReadAllBlocks()
    {
        uint numBlocks = _header.NumBlocks;
        var blocks = new List<NiObject>((int)numBlocks);

        for (int i = 0; i < numBlocks; i++)
        {
            int typeIndex = _blockTypeIndex[i];

            if (typeIndex < 0 || typeIndex >= _blockTypes.Length)
            {
                Console.WriteLine($"[WARN] Block {i} typeIndex={typeIndex} out of range. Treating as NiUnknown.");
            }

            string typeName = (typeIndex >= 0 && typeIndex < _blockTypes.Length)
                ? _blockTypes[typeIndex]
                : "NiUnknown";

            long blockPos = _br.BaseStream.Position;
            if (blockPos >= _br.BaseStream.Length)
            {
                Console.WriteLine($"[WARN] Block {i} position {blockPos} is past EOF; creating empty {typeName}.");
                var empty = NifRegistry.Create(typeName, i);
                _ctx.Blocks[i] = empty;
                blocks.Add(empty);
                continue;
            }

            Console.WriteLine($"[NIF] Reading block {i}: {typeName} @ {blockPos}");

            var obj = NifRegistry.Create(typeName, i);
            obj.Read(_br, _ctx);

            _ctx.Blocks[i] = obj;
            blocks.Add(obj);
        }

        return blocks;
    }

    public NifContext Context => _ctx;
}

