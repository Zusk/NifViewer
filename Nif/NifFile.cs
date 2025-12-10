using System;
using System.IO;
using System.Reflection;

namespace NifViewer.Nif;

/// <summary>
/// Parses a binary NIF into strongly-typed Civ4 node objects.
/// The blocks array preserves on-disk ordering so lookups can be resolved by index.
/// </summary>
public sealed class NifFile
{
    public string Path { get; private set; } = string.Empty;
    public NifHeader Header { get; private set; } = new();
    public INifBlock[] Blocks { get; private set; } = Array.Empty<INifBlock>();

    public static NifFile Load(string path)
    {
        using var stream = File.OpenRead(path);
        using var reader = new BinaryReader(stream);

        var file = new NifFile { Path = path };
        file.Read(reader);
        return file;
    }

    public void Read(BinaryReader reader)
    {
        Header = new NifHeader();
        Header.Read(reader);

        Blocks = new INifBlock[Header.BlockCount];

        for (int i = 0; i < Header.BlockCount; i++)
        {
            string typeName = Header.Blocks[i];
            var block = CreateBlock(typeName, i, reader);
            Blocks[i] = block;
        }
    }

    private static INifBlock CreateBlock(string typeName, int index, BinaryReader reader)
    {
        string qualifiedName = $"NifViewer.Nif.{typeName}";
        var type = Type.GetType(qualifiedName);

        if (type == null || !typeof(INifBlock).IsAssignableFrom(type))
        {
            return new UnknownBlock(typeName, index);
        }

        var ctor = type.GetConstructor(new[] { typeof(BinaryReader) });
        if (ctor == null)
        {
            return new UnknownBlock(typeName, index);
        }

        var instance = (INifBlock)ctor.Invoke(new object[] { reader });
        instance.BlockIndex = index;
        instance.TypeName = typeName;
        return instance;
    }
}

/// <summary>
/// Minimal interface shared by all Civ4 node objects.
/// </summary>
public interface INifBlock
{
    string TypeName { get; set; }
    int BlockIndex { get; set; }
}

public sealed class UnknownBlock : INifBlock
{
    public UnknownBlock(string typeName, int blockIndex)
    {
        TypeName = typeName;
        BlockIndex = blockIndex;
    }

    public string TypeName { get; set; }
    public int BlockIndex { get; set; }
}
