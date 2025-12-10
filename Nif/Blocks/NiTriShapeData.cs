using System.IO;
using OpenTK.Mathematics;

/// <summary>
/// Raw triangle mesh geometry.
/// Civ4 NiTriShapeData is simple: vertices, normals, UV, triangle list.
/// </summary>
public sealed class NiTriShapeData : NiObject
{
    public ushort NumVertices;
    public bool HasVertices;
    public Vector3[] Vertices = Array.Empty<Vector3>();

    public bool HasNormals;
    public Vector3[] Normals = Array.Empty<Vector3>();

    public Vector3 Center;
    public float Radius;

    public bool HasVertexColors;
    public Vector4[] VertexColors = Array.Empty<Vector4>();

    public byte NumUVSets;
    public bool HasUV;
    public Vector2[][] UVSets = Array.Empty<Vector2[]>();

    public ushort NumTriangles;
    public ushort NumTrianglePoints;
    public bool HasTriangles;
    public ushort[] Indices = Array.Empty<ushort>();

    public Vector2[] GetPrimaryUVs() =>
        (HasUV && NumUVSets > 0) ? UVSets[0] : Array.Empty<Vector2>();

    public override void Read(BinaryReader br, NifContext ctx)
    {
        base.Read(br, ctx);

        NumVertices = br.ReadUInt16();
        HasVertices = br.ReadBoolean();

        if (HasVertices)
        {
            Vertices = new Vector3[NumVertices];
            for (int i = 0; i < NumVertices; i++)
                Vertices[i] = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
        }

        HasNormals = br.ReadBoolean();
        if (HasNormals)
        {
            Normals = new Vector3[NumVertices];
            for (int i = 0; i < NumVertices; i++)
                Normals[i] = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
        }

        Center = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
        Radius = br.ReadSingle();

        HasVertexColors = br.ReadBoolean();
        if (HasVertexColors)
        {
            VertexColors = new Vector4[NumVertices];
            for (int i = 0; i < NumVertices; i++)
                VertexColors[i] =
                    new Vector4(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
        }

        NumUVSets = br.ReadByte();
        HasUV = br.ReadBoolean();

        if (HasUV && NumUVSets > 0)
        {
            UVSets = new Vector2[NumUVSets][];
            for (int set = 0; set < NumUVSets; set++)
            {
                UVSets[set] = new Vector2[NumVertices];
                for (int i = 0; i < NumVertices; i++)
                    UVSets[set][i] =
                        new Vector2(br.ReadSingle(), br.ReadSingle());
            }
        }

        NumTriangles = br.ReadUInt16();
        NumTrianglePoints = br.ReadUInt16();
        HasTriangles = br.ReadBoolean();

        if (HasTriangles)
        {
            Indices = new ushort[NumTriangles * 3];
            for (int i = 0; i < NumTriangles; i++)
            {
                int k = i * 3;
                Indices[k]     = br.ReadUInt16();
                Indices[k + 1] = br.ReadUInt16();
                Indices[k + 2] = br.ReadUInt16();
            }
        }
    }
}

