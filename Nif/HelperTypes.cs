namespace NifViewer.Nif;

public struct Vector3
{
    public float X, Y, Z;
}

public struct Triangle
{
    public ushort X, Y, Z;
}

public struct Matrix33
{
    public float m11, m12, m13, m21, m22, m23, m31, m32, m33;
}

public struct Color3
{
    public float R, G, B;
}

public struct Color4
{
    public float R, G, B, A;
}

public struct TexCoord
{
    public float X, Y;
}

public struct MatchGroup
{
    public ushort VerticesCount;
    public ushort[] VertexIndices;
}
