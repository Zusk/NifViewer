using System;
using System.Collections.Generic;
public sealed class NifContext
{
    public string HeaderString { get; set; } = string.Empty;
    public uint Version { get; set; }
    public uint UserVersion { get; set; }
    public byte EndianType { get; set; }

    public uint NumBlocks { get; set; }

    /// <summary>Index → type name (e.g. 0 → "NiNode").</summary>
    public string[] BlockTypes { get; set; } = Array.Empty<string>();

    /// <summary>Block index → type index into BlockTypes.</summary>
    public int[] BlockTypeIndex { get; set; } = Array.Empty<int>();

    /// <summary>Global string palette read from the file (matches niflib behaviour).</summary>
    public List<string> Strings { get; set; } = new();

    /// <summary>All instantiated NiObject blocks, same order as in file.</summary>
    public NiObject[] Blocks { get; set; } = Array.Empty<NiObject>();


    /// <summary>Helper: resolve a block by index with type safety.</summary>
    public T? GetBlock<T>(int index) where T : NiObject
    {
        if (index < 0 || index >= Blocks.Length) return null;
        return Blocks[index] as T;
    }

    /// <summary>
    /// Returns a string from the palette or an empty string if the index is out of range.
    /// </summary>
    public string GetString(int index)
    {
        if (index < 0 || index >= Strings.Count)
            return string.Empty;

        return Strings[index];
    }
}
