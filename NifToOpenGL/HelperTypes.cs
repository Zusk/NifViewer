using System.IO;
using System.Text;
using System.Collections.Generic;
using System;

namespace Civ4NifReader
{
    // Basic 3D Vector (x, y, z)
    public struct Vector3
    {
        public float X, Y, Z;
        
    }

    public struct Triangle
    {
        public UInt16 X, Y, Z;

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
        public UInt16 VerticesCount;
        public UInt16[] VertexIndices;
    }

    public struct Bone
    {
        public Matrix33 SkinRotation;
        public Vector3 SkinTranslation;
        public UInt32 SkinScale;
        public Vector3 BoundingSphereCenter;
        public float BoundingSphereRadius;
        public UInt16 VerticesCount;
        public UInt16[] VertexWeightIndexes;
        public float[] VertexWeights;
    }

    public struct Partition
    {
        public UInt16 VerticesCount;
        public UInt16 TrianglesCount;
        public UInt16 BonesCount;
        public UInt16 StripsCount;
        public UInt16 WeightsPerVertex;
        public UInt16[] Bones;
        public byte HasVertexMap;
        public UInt16[] VartexMap;
        public byte HasVertexWeights;
        public float[,] VertexWeights;
        public UInt16[] StripLengths;
        public UInt16[][] Strips;
        public byte HasFaces;
        public Triangle[] Triangles;
        public byte HasBoneIndices;
        public byte[,] BoneIndices;
    }
}