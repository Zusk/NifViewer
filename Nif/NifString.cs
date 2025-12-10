using System.IO;
using System.Text;

/// <summary>
/// Helper utilities for reading NIF strings in the same way as niflib.
/// Every string is stored as a uint32 length followed by that many bytes
/// (ASCII), except for the very first header string which is newline-terminated.
/// </summary>
public static class NifString
{
    public static string ReadSizedString(BinaryReader br)
    {
        uint length = br.ReadUInt32();

        if (length == 0)
            return string.Empty;

        byte[] data = br.ReadBytes((int)length);
        return Encoding.ASCII.GetString(data).TrimEnd('\0');
    }
}

