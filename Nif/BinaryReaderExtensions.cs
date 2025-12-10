using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NifViewer.Nif;

/// <summary>
/// Helpers used by the Civ4 NIF reader to pull structured data out of a BinaryReader.
/// </summary>
public static class BinaryReaderExtensions
{
    public static string ReadNullTerminatedString(this BinaryReader reader)
    {
        var bytes = new List<byte>();
        while (reader.BaseStream.Position != reader.BaseStream.Length)
        {
            byte b = reader.ReadByte();
            if (b == '\n')
                break;

            bytes.Add(b);
        }

        return Encoding.UTF8.GetString(bytes.ToArray());
    }

    public static string ReadStringWithLength(this BinaryReader reader)
    {
        uint length = reader.ReadUInt32();
        var bytes = new byte[length];
        if (length > 0)
            bytes = reader.ReadBytes((int)length);
        return Encoding.UTF8.GetString(bytes);
    }

    public static Vector3 ReadVector3(this BinaryReader reader)
    {
        return new Vector3
        {
            X = reader.ReadSingle(),
            Y = reader.ReadSingle(),
            Z = reader.ReadSingle()
        };
    }

    public static Matrix33 ReadMatrix33(this BinaryReader reader)
    {
        return new Matrix33
        {
            m11 = reader.ReadSingle(),
            m12 = reader.ReadSingle(),
            m13 = reader.ReadSingle(),
            m21 = reader.ReadSingle(),
            m22 = reader.ReadSingle(),
            m23 = reader.ReadSingle(),
            m31 = reader.ReadSingle(),
            m32 = reader.ReadSingle(),
            m33 = reader.ReadSingle(),
        };
    }

    public static Color3 ReadColor3(this BinaryReader reader)
    {
        return new Color3 { R = reader.ReadSingle(), G = reader.ReadSingle(), B = reader.ReadSingle() };
    }

    public static Color4 ReadColor4(this BinaryReader reader)
    {
        return new Color4 { R = reader.ReadSingle(), G = reader.ReadSingle(), B = reader.ReadSingle(), A = reader.ReadSingle() };
    }

    public static TexCoord ReadTexCoord(this BinaryReader reader)
    {
        return new TexCoord { X = reader.ReadSingle(), Y = reader.ReadSingle() };
    }

    public static Triangle ReadTriangle(this BinaryReader reader)
    {
        return new Triangle { X = reader.ReadUInt16(), Y = reader.ReadUInt16(), Z = reader.ReadUInt16() };
    }

    public static MatchGroup ReadMatchGroup(this BinaryReader reader)
    {
        var group = new MatchGroup
        {
            VerticesCount = reader.ReadUInt16(),
        };

        group.VertexIndices = new ushort[group.VerticesCount];
        for (int i = 0; i < group.VerticesCount; i++)
            group.VertexIndices[i] = reader.ReadUInt16();

        return group;
    }
}
