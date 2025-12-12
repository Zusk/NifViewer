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
    public struct Quaternion
    {
        public float A, X, Y, Z;
    }

    public struct Triangle
    {
        public UInt16 X, Y, Z;

    }

    public struct Matrix22
    {
        public float m11, m12, m21, m22;
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

    public struct Texture
    {
        public UInt32 Source;
        public UInt32 ClampMode;
        public UInt32 FilterMode;
        public UInt32 UVSet;
        public Byte HasTextureTransform;
        public TexCoord Translation;
        public TexCoord Scale;
        public float Rotation;
        public UInt32 TransformMethod;
        public TexCoord Center;
    }

    public struct floatKey
    {
        public UInt32 KeysCount;
        public UInt32 Interpolation;
        public float[] KeysTime;
        public float[] KeysValue;
        public float[] KeysForward;
        public float[] KeysBackward;
        public Vector3[] KeysTBC;
    }

    public struct Vector3Key
    {
        public UInt32 KeysCount;
        public UInt32 Interpolation;
        public float[] KeysTime;
        public Vector3[] KeysValue;
        public float[] KeysForward;
        public float[] KeysBackward;
        public Vector3[] KeysTBC;
    }

    public struct Shader
    {
        public byte HasMap;
        public UInt32 MapSource;
        public UInt32 MAPClampMode;
        public UInt32 MapFilterMode;
        public UInt32 MapUVSet;
        public byte MapHasTextureTransform;
        public TexCoord MapTranslation;
        public TexCoord MapScale;
        public float MapRotation;
        public UInt32 MapTransformMethod;
        public TexCoord MaptransformCenter;
        public UInt32 MapID;
    }

    public struct QuaternionKey
    {
        public float Time;
        public Quaternion Value;
        public Vector3 TBC;
    }
}