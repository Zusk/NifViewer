using System;
using System.Collections.Generic;
public sealed class NifContext
{
    public string HeaderString { get; set; } = string.Empty;
    public uint Version { get; set; }
    public uint UserVersion { get; set; }
    public byte EndianType { get; set; }

    public uint NumBlocks { get; set; }

    public uint NumGroups { get; set; }

    /// <summary>Index → type name (e.g. 0 → "NiNode").</summary>
    public string[] BlockTypes { get; set; } = Array.Empty<string>();

    /// <summary>Block index → type index into BlockTypes.</summary>
    public int[] BlockTypeIndex { get; set; } = Array.Empty<int>();

    /// <summary>Global string palette read from the file (matches niflib behaviour).</summary>
    public List<string> Strings { get; set; } = new();

    /// <summary>Additional grouping information stored in the header.</summary>
    public uint[] Groups { get; set; } = Array.Empty<uint>();

    /// <summary>All instantiated blocks (no NiObject type in this codebase; store as plain objects).</summary>
    public object[] Blocks { get; set; } = Array.Empty<object>();


    /// <summary>Helper: resolve a block by index with type safety.</summary>
    public T? GetBlock<T>(int index) where T : class
    {
        if (index < 0 || index >= Blocks.Length) return null;
        return Blocks[index] as T;
    }

    /// <summary>
    /// Returns a string from the palette or an empty string if the index is out of range.
    /// </summary>
    // NOTE: The project currently does not implement a full NIF loader or
    // string-palette parser. The `Strings` list is retained so a future
    // loader can populate and use it. An unused GetString helper was
    // removed to avoid leaving orphaned functions in the codebase.
}
