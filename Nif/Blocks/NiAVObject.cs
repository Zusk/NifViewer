using System.IO;
using OpenTK.Mathematics;

/// <summary>
/// Most visible scene objects derive from NiAVObject.
/// Contains transform, flags, and bounds.
/// </summary>
public abstract class NiAVObject : NiObjectNET
{
    public ushort Flags { get; private set; }

    public Vector3 Translation { get; private set; }
    public Matrix3 Rotation { get; private set; }
    public float Scale { get; private set; } = 1f;

    public bool HasBounds { get; private set; }
    public Vector3 BoundMin { get; private set; }
    public Vector3 BoundMax { get; private set; }

    public override void Read(BinaryReader br, NifContext ctx)
    {
        base.Read(br, ctx);

        // Flags (Civ4 uses 2 bytes flags + 2 bytes padding)
        Flags = br.ReadUInt16();
        br.ReadUInt16(); // padding

        // Transform: translation (3 floats)
        Translation = new Vector3(
            br.ReadSingle(), br.ReadSingle(), br.ReadSingle());

        // Rotation matrix (3x3)
        Rotation = new Matrix3(
            br.ReadSingle(), br.ReadSingle(), br.ReadSingle(),
            br.ReadSingle(), br.ReadSingle(), br.ReadSingle(),
            br.ReadSingle(), br.ReadSingle(), br.ReadSingle());

        // Scale
        Scale = br.ReadSingle();

        // Bounds
        HasBounds = br.ReadBoolean();
        if (HasBounds)
        {
            BoundMin = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
            BoundMax = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
        }
    }
}

