using System;

/// <summary>
/// Base type for all NIF blocks.
/// </summary>
public abstract class NiObject
{
    public int BlockIndex { get; internal set; }
    public string TypeName { get; internal set; } = string.Empty;

    public virtual void Read(BinaryReader br, NifContext ctx)
    {
        // Many Civ4 blocks have nothing at the pure NiObject level.
    }
}

/// <summary>
/// Fallback block for unsupported or unknown types.
/// </summary>
public sealed class NiUnknown : NiObject
{
    public override void Read(BinaryReader br, NifContext ctx)
    {
        Console.WriteLine(
            $"[WARN] Unsupported NIF block: {TypeName} at index {BlockIndex}");
    }
}

