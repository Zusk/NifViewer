using System;
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

    /// <summary>All instantiated NiObject blocks, same order as in file.</summary>
    public NiObject[] Blocks { get; set; } = Array.Empty<NiObject>();


    /// <summary>Helper: resolve a block by index with type safety.</summary>
    public T? GetBlock<T>(int index) where T : NiObject
    {
        if (index < 0 || index >= Blocks.Length) return null;
        return Blocks[index] as T;
    }
}
