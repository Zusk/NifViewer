using System.IO;
using System.Text;
using System.Collections.Generic;
using System;

namespace Civ4NifReader
{
    public static class BinaryReaderExtensions
    {
        public static string ReadNullTerminatedString(this BinaryReader reader)
        {
            List<byte> bytes = new List<byte>();

            // Loop until we hit the end of the stream
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                byte b = reader.ReadByte();

                // Check for the null terminator (0x00)
                if (b == '\n')
                {
                    // Stop reading
                    break;
                }

                bytes.Add(b);
            }

            // Convert the collected bytes to a string (usually UTF8 or ASCII)
            return System.Text.Encoding.UTF8.GetString(bytes.ToArray());
        }

        public static string ReadStringWithLength(this BinaryReader reader)
        {
            List<byte> bytes = new List<byte>();
            UInt32 length = reader.ReadUInt32();

            // Loop until we hit the end of the stream
            for (uint i = 0; i<length && reader.BaseStream.Position != reader.BaseStream.Length; i++ )
            {
                byte b = reader.ReadByte();

                bytes.Add(b);
            }

            // Convert the collected bytes to a string (usually UTF8 or ASCII)
            return System.Text.Encoding.UTF8.GetString(bytes.ToArray());
        }

        public static Vector3 ReadVector3(this BinaryReader reader)
        {
            Vector3 v = new Vector3 { X = reader.ReadSingle(), Y = reader.ReadSingle(), Z = reader.ReadSingle() }; ;
            return v;
        }
        public static Matrix33 ReadMatrix33(this BinaryReader reader)
        {
            var m = new Matrix33();
            m.m11 = reader.ReadSingle(); m.m12 = reader.ReadSingle(); m.m13 = reader.ReadSingle();
            m.m21 = reader.ReadSingle(); m.m22 = reader.ReadSingle(); m.m23 = reader.ReadSingle();
            m.m31 = reader.ReadSingle(); m.m32 = reader.ReadSingle(); m.m33 = reader.ReadSingle();
            return m;
        }

        public static Color3 ReadColor3(this BinaryReader reader)
        {
            Color3 v = new Color3 { R = reader.ReadSingle(), G = reader.ReadSingle(), B = reader.ReadSingle() }; ;
            return v;
        }

        public static Color4 ReadColor4(this BinaryReader reader)
        {
            Color4 v = new Color4 { R = reader.ReadSingle(), G = reader.ReadSingle(), B = reader.ReadSingle(), A = reader.ReadSingle() }; ;
            return v;
        }

        public static TexCoord ReadTexCoord(this BinaryReader reader)
        {
            TexCoord v = new TexCoord { X = reader.ReadSingle(), Y = reader.ReadSingle() }; ;
            return v;
        }

        public static Triangle ReadTriangle(this BinaryReader reader)
        {
            Triangle v = new Triangle { X = reader.ReadUInt16(), Y = reader.ReadUInt16(), Z = reader.ReadUInt16() }; ;
            return v;
        }
        public static MatchGroup ReadMatchGroup(this BinaryReader reader)
        {
            MatchGroup m;
            m.VerticesCount = reader.ReadUInt16();
            m.VertexIndices = new UInt16[m.VerticesCount];
            for(int i = 0;i<m.VerticesCount;i++)
            {
                m.VertexIndices[i] = reader.ReadUInt16();
            }
            return m;
        }
        public static Bone ReadBone(this BinaryReader reader, byte HasVertexWeights)
        {
            Bone m = new Bone();
            m.SkinRotation = reader.ReadMatrix33();
            m.SkinTranslation = reader.ReadVector3();
            m.SkinScale = reader.ReadUInt32();
            m.BoundingSphereCenter = reader.ReadVector3();
            m.BoundingSphereRadius = reader.ReadSingle();

            if (HasVertexWeights != 0)
            {
                m.VerticesCount = reader.ReadUInt16();
                m.VertexWeightIndexes = new UInt16[m.VerticesCount];
                m.VertexWeights = new float[m.VerticesCount];
                for (int i = 0; i < m.VerticesCount; i++)
                {
                    m.VertexWeightIndexes[i] = reader.ReadUInt16();
                    m.VertexWeights[i] = reader.ReadSingle();
                }
            }
            return m;
        }
        public static Partition ReadPartition(this BinaryReader reader)
        {
            Partition p = new Partition();
            p.VerticesCount = reader.ReadUInt16();
            p.TrianglesCount = reader.ReadUInt16();
            p.BonesCount = reader.ReadUInt16();
            p.StripsCount = reader.ReadUInt16();
            p.WeightsPerVertex = reader.ReadUInt16();
            p.Bones = new UInt16[p.BonesCount];
            for (int i = 0; i < p.BonesCount; i++)
                p.Bones[i] = reader.ReadUInt16();
            p.HasVertexMap = reader.ReadByte();
            if (p.HasVertexMap != 0)
            {
                p.VartexMap = new UInt16[p.VerticesCount];
                for (int i = 0; i < p.VerticesCount; i++)
                    p.VartexMap[i] = reader.ReadUInt16();
            }
            p.HasVertexWeights = reader.ReadByte();
            if (p.HasVertexWeights != 0)
            {
                p.VertexWeights = new float[p.VerticesCount, p.WeightsPerVertex];
                for (int i = 0; i < p.VerticesCount; i++)
                    for(int j = 0;  j<p.WeightsPerVertex; j++)
                        p.VertexWeights[i,j] = reader.ReadSingle();
            }
            p.StripLengths = new UInt16[p.StripsCount];
            p.Strips = new UInt16[p.StripsCount][];
            p.HasFaces = reader.ReadByte();
            for (int i = 0; i < p.StripsCount; i++)
            {
                p.Strips[i] = new UInt16[p.StripLengths[i]];
                for(int j=0;j< p.StripLengths[i];j++)
                {
                    p.Strips[i][j] = reader.ReadUInt16();
                }
            }
            p.Triangles = new Triangle[p.TrianglesCount];
            for (int i = 0; i < p.TrianglesCount; i++)
            {
                p.Triangles[i] = reader.ReadTriangle();
            }
            p.HasBoneIndices = reader.ReadByte();
            if(p.HasBoneIndices!=0)
            {
                p.BoneIndices = new byte[p.VerticesCount, p.BonesCount];
                for (int i = 0; i < p.VerticesCount; i++)
                {
                    for (int j = 0; j < p.BonesCount; j++)
                    {
                        p.BoneIndices[i, j] = reader.ReadByte();
                    }
                }
            }
            return p;
        }
    }
}