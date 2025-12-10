using System;
using System.Collections.Generic;
using System.IO;

namespace NifViewer.Nif;

public class NodeSimple : INifBlock
{
    public NodeSimple(BinaryReader reader)
    {
    }

    public string TypeName { get; set; } = string.Empty;
    public int BlockIndex { get; set; }
}

public class NodeNamed : NodeSimple
{
    public NodeNamed(BinaryReader reader)
        : base(reader)
    {
        Name = reader.ReadStringWithLength();
    }

    public string Name { get; set; }
}

public class NodeGeneralNoFlag : NodeNamed
{
    public NodeGeneralNoFlag(BinaryReader reader)
        : base(reader)
    {
        ExtraData = ReadRefs(reader);
        Controller = reader.ReadInt32();
    }

    public IReadOnlyList<uint> ExtraData { get; }
    public int Controller { get; }

    private static uint[] ReadRefs(BinaryReader reader)
    {
        uint count = reader.ReadUInt32();
        var refs = new uint[count];
        for (int i = 0; i < count; i++)
            refs[i] = reader.ReadUInt32();
        return refs;
    }
}

public class NodeGeneral : NodeNamed
{
    public NodeGeneral(BinaryReader reader)
        : base(reader)
    {
        ExtraData = ReadRefs(reader);
        Controller = reader.ReadInt32();
        Flags = reader.ReadUInt16();
    }

    public IReadOnlyList<uint> ExtraData { get; }
    public int Controller { get; }
    public ushort Flags { get; }

    private static uint[] ReadRefs(BinaryReader reader)
    {
        uint count = reader.ReadUInt32();
        var refs = new uint[count];
        for (int i = 0; i < count; i++)
            refs[i] = reader.ReadUInt32();
        return refs;
    }
}

public class NodeController : NodeSimple
{
    public NodeController(BinaryReader reader)
        : base(reader)
    {
        NextController = reader.ReadUInt32();
        Flags = reader.ReadUInt16();
        Frequency = reader.ReadSingle();
        Phase = reader.ReadSingle();
        StartTime = reader.ReadSingle();
        StopTime = reader.ReadSingle();
        Target = reader.ReadUInt32();
    }

    public uint NextController { get; }
    public ushort Flags { get; }
    public float Frequency { get; }
    public float Phase { get; }
    public float StartTime { get; }
    public float StopTime { get; }
    public uint Target { get; }
}

public class NodeGeometry : NodeGeneral
{
    public NodeGeometry(BinaryReader reader)
        : base(reader)
    {
        Translation = reader.ReadVector3();
        Rotation = reader.ReadMatrix33();
        Scale = reader.ReadSingle();

        uint propertiesCount = reader.ReadUInt32();
        var props = new int[propertiesCount];
        for (int i = 0; i < propertiesCount; i++)
            props[i] = (int)reader.ReadUInt32();
        PropertyIndices = props;

        CollisionObject = reader.ReadInt32();
    }

    public Vector3 Translation { get; }
    public Matrix33 Rotation { get; }
    public float Scale { get; }
    public IReadOnlyList<int> PropertyIndices { get; }
    public int CollisionObject { get; }
}

public class NiNode : NodeGeometry
{
    public NiNode(BinaryReader reader)
        : base(reader)
    {
        Children = ReadRefs(reader);
        Effects = ReadRefs(reader);
    }

    public IReadOnlyList<uint> Children { get; }
    public IReadOnlyList<uint> Effects { get; }

    private static uint[] ReadRefs(BinaryReader reader)
    {
        uint count = reader.ReadUInt32();
        var refs = new uint[count];
        for (int i = 0; i < count; i++)
            refs[i] = reader.ReadUInt32();
        return refs;
    }
}

public class NiTriShape : NodeGeometry
{
    public NiTriShape(BinaryReader reader)
        : base(reader)
    {
        DataIndex = reader.ReadInt32();
        SkinInstance = reader.ReadInt32();
        HasShader = reader.ReadByte();
    }

    public int DataIndex { get; }
    public int SkinInstance { get; }
    public byte HasShader { get; }
}

public class NiTriShapeData : NodeSimple
{
    public NiTriShapeData(BinaryReader reader)
        : base(reader)
    {
        GroupID = reader.ReadInt32();
        VerticesCount = reader.ReadUInt16();
        KeepFlags = reader.ReadByte();
        CompressFlags = reader.ReadByte();
        HasVertices = reader.ReadByte();

        if (HasVertices != 0)
        {
            Vertices = new Vector3[VerticesCount];
            for (int i = 0; i < VerticesCount; i++)
                Vertices[i] = reader.ReadVector3();
        }
        else
        {
            Vertices = Array.Empty<Vector3>();
        }

        DataFlags = reader.ReadUInt16();
        HasNormals = reader.ReadByte();
        if (HasNormals != 0)
        {
            Normals = new Vector3[VerticesCount];
            for (int i = 0; i < VerticesCount; i++)
                Normals[i] = reader.ReadVector3();
        }
        else
        {
            Normals = Array.Empty<Vector3>();
        }

        BoundingSphereCenter = reader.ReadVector3();
        BoundingSphereRadius = reader.ReadSingle();

        HasVertexColors = reader.ReadByte();
        if (HasVertexColors != 0)
        {
            VertexColors = new Color4[VerticesCount];
            for (int i = 0; i < VerticesCount; i++)
                VertexColors[i] = reader.ReadColor4();
        }
        else
        {
            VertexColors = Array.Empty<Color4>();
        }

        UVSets = new TexCoord[VerticesCount];
        for (int i = 0; i < VerticesCount; i++)
            UVSets[i] = reader.ReadTexCoord();

        ConsistencyFlags = reader.ReadUInt16();
        AdditionalData = reader.ReadInt32();

        TrianglesCount = reader.ReadUInt16();
        TrianglePointsCount = reader.ReadUInt32();
        HasTriangles = reader.ReadByte();
        Triangles = new Triangle[TrianglesCount];
        for (int i = 0; i < TrianglesCount; i++)
            Triangles[i] = reader.ReadTriangle();

        MatchGroupsCount = reader.ReadUInt16();
        MatchGroups = new MatchGroup[MatchGroupsCount];
        for (int i = 0; i < MatchGroupsCount; i++)
            MatchGroups[i] = reader.ReadMatchGroup();
    }

    public int GroupID { get; }
    public ushort VerticesCount { get; }
    public byte KeepFlags { get; }
    public byte CompressFlags { get; }
    public byte HasVertices { get; }
    public Vector3[] Vertices { get; }
    public ushort DataFlags { get; }
    public byte HasNormals { get; }
    public Vector3[] Normals { get; }
    public Vector3 BoundingSphereCenter { get; }
    public float BoundingSphereRadius { get; }
    public byte HasVertexColors { get; }
    public Color4[] VertexColors { get; }
    public TexCoord[] UVSets { get; }
    public ushort ConsistencyFlags { get; }
    public int AdditionalData { get; }
    public ushort TrianglesCount { get; }
    public uint TrianglePointsCount { get; }
    public byte HasTriangles { get; }
    public Triangle[] Triangles { get; }
    public ushort MatchGroupsCount { get; }
    public MatchGroup[] MatchGroups { get; }
}

public class NiSourceTexture : NodeGeneralNoFlag
{
    public NiSourceTexture(BinaryReader reader)
        : base(reader)
    {
        UseExternal = reader.ReadByte();
        FilePath = reader.ReadStringWithLength();
        PixelData = reader.ReadInt32();
        PixelLayout = reader.ReadUInt32();
        UseMipmaps = reader.ReadUInt32();
        AlphaFormat = reader.ReadUInt32();
        IsStatic = reader.ReadByte();
        DirectRender = reader.ReadByte();
    }

    public byte UseExternal { get; }
    public string FilePath { get; }
    public int PixelData { get; }
    public uint PixelLayout { get; }
    public uint UseMipmaps { get; }
    public uint AlphaFormat { get; }
    public byte IsStatic { get; }
    public byte DirectRender { get; }
}
