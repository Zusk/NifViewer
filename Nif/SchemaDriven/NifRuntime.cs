using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Parsed NIF file using the data-driven schema.
/// </summary>
public sealed class NifFile
{
    public required NifHeader Header { get; init; }
    public List<NifBlockInstance> Blocks { get; } = new();
}

/// <summary>
/// Generic runtime representation of a NIF block.
/// </summary>
public sealed class NifBlockInstance
{
    public int Index { get; set; }
    public string TypeName { get; set; } = string.Empty;
    public required NifBlockDef Definition { get; init; }
    public Dictionary<string, object?> Fields { get; } = new(StringComparer.Ordinal);
}

/// <summary>
/// Context used while reading a block to resolve expressions and field values.
/// </summary>
public sealed class NifReadContext
{
    private readonly Dictionary<string, object?> _values = new(StringComparer.Ordinal);

    public NifReadContext(NifHeader header)
    {
        Header = header;
        _values[nameof(header.Version).ToLowerInvariant()] = header.Version;
        _values[nameof(header.UserVersion).ToLowerInvariant()] = header.UserVersion;
        _values["userversion2"] = 0; // placeholder for alternate user version fields
    }

    private NifReadContext(NifReadContext parent)
    {
        Header = parent.Header;
        Parent = parent;
    }

    public NifHeader Header { get; }
    public NifReadContext? Parent { get; }

    public void SetValue(string name, object? value)
    {
        _values[name] = value;
    }

    public bool TryGetValue(string name, out object? value)
    {
        if (_values.TryGetValue(name, out value))
            return true;

        if (Parent != null)
            return Parent.TryGetValue(name, out value);

        value = null;
        return false;
    }

    public NifReadContext CreateChild()
    {
        return new NifReadContext(this);
    }
}

public static class NifDefinitionHelpers
{
    /// <summary>
    /// Returns the fields for a block including any inherited fields.
    /// </summary>
    public static IEnumerable<NifFieldDef> EnumerateWithBases(NifSchema schema, NifBlockDef def)
    {
        var stack = new Stack<NifBlockDef>();
        var current = def;
        while (current != null)
        {
            stack.Push(current);
            if (current.BaseName != null && schema.Blocks.TryGetValue(current.BaseName, out var baseDef))
                current = baseDef;
            else
                break;
        }

        foreach (var d in stack.Reverse())
        {
            foreach (var f in d.Fields)
                yield return f;
        }
    }
}
