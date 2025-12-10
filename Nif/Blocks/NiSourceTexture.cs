using System.IO;
using System.Text;

/// <summary>
/// External texture reference.
/// </summary>
public sealed class NiSourceTexture : NiObject
{
    public string FileName { get; private set; } = string.Empty;

    public override void Read(BinaryReader br, NifContext ctx)
    {
        base.Read(br, ctx);

        uint external = br.ReadUInt32(); // usually 1
        uint len = br.ReadUInt32();

        if (len > 0)
        {
            byte[] bytes = br.ReadBytes((int)len);
            FileName = Encoding.ASCII.GetString(bytes).TrimEnd('\0');
        }

        // Skip 3 ints (Civ4 layout)
        br.ReadUInt32();
        br.ReadUInt32();
        br.ReadUInt32();
    }
}

