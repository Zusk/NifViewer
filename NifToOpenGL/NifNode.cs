using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Civ4NifReader
{
    public class NifHeader
    {
        string HeaderString;
        string version;
        byte endian;
        UInt32 userVersion;
        public UInt32 BlockCount;
        public UInt16 BlockTypeCount;
        public string[] BlockTypes; //list of all available block types
        public string[] Blocks; //type of each block
        UInt32 GroupCount;

        public void Read(ref BinaryReader reader)
        {
            HeaderString = reader.ReadNullTerminatedString();
            version = "";
            for (int i = 0; i < 4; i++)
            {
                byte bVersion = reader.ReadByte();
                version += bVersion.ToString();
                if (i < 3)
                    version += ".";
            }

            endian = reader.ReadByte();
            userVersion = reader.ReadUInt32();
            BlockCount = reader.ReadUInt32();
            BlockTypeCount = reader.ReadUInt16();
            BlockTypes = new string[BlockTypeCount];
            for (int i = 0; i < BlockTypeCount; i++)
            {
                BlockTypes[i] = reader.ReadStringWithLength();
            }
            Blocks = new string[BlockCount];
            for (int i = 0; i < BlockCount; i++)
            {
                Blocks[i] = BlockTypes[reader.ReadUInt16()];
            }
            GroupCount = reader.ReadUInt32();
            if (GroupCount > 0)
            {
                MessageBox.Show($"Reading groups not implemented yet. If you need it, ask kaczkar128 on discord");
            }
        }
    }

    #region Node common properties
    public class NodeSimple
    {
        public NodeSimple(ref BinaryReader reader)
        {

        }
    }
    public class NodeNamed:NodeSimple
    {
        public string Name;
        public NodeNamed(ref BinaryReader reader)
            :base(ref reader)
        {
            Name = reader.ReadStringWithLength();
        }
    }
    public class NodeGeneralNoFlag : NodeNamed
    {
        UInt32 ExtraDataListCount;
        UInt32[] ExtraData;
        Int32 Controller; // -1 -> None
        public NodeGeneralNoFlag(ref BinaryReader reader)
            : base(ref reader)
        {
            ExtraDataListCount = reader.ReadUInt32();
            ExtraData = new UInt32[ExtraDataListCount];
            for (int i = 0; i < ExtraDataListCount; i++)
            {
                ExtraData[i] = reader.ReadUInt32();
            }
            Controller = reader.ReadInt32();
        }

    }
    public class NodeGeneral : NodeNamed
    {
        UInt32 ExtraDataListCount;
        UInt32[] ExtraData;
        Int32 Controller; // -1 -> None
        UInt16 Flags;
        public NodeGeneral(ref BinaryReader reader)
            : base(ref reader)
        {
            ExtraDataListCount = reader.ReadUInt32();
            ExtraData = new UInt32[ExtraDataListCount];
            for (int i = 0; i < ExtraDataListCount; i++)
            {
                ExtraData[i] = reader.ReadUInt32();
            }
            Controller = reader.ReadInt32();
            Flags = reader.ReadUInt16();
        }

    }
    public class NodeController : NodeSimple
    {
        UInt32 NextController;
        UInt16 Flags;
        float Frequency;
        float Phase;
        float StartTime;
        float StopTime;
        UInt32 Target;
        public NodeController(ref BinaryReader reader)
            : base(ref reader)
        {
            NextController = reader.ReadUInt32();
            Flags = reader.ReadUInt16();
            Frequency = reader.ReadSingle();
            Phase = reader.ReadSingle();
            StartTime = reader.ReadSingle();
            StopTime = reader.ReadSingle();
            Target = reader.ReadUInt32();
        }
    }
    public class NodeGeometry : NodeGeneral
    {

        Vector3 Translation;
        Matrix33 Rotation;
        float Scale;
        UInt32 PropertiesCount;
        UInt32[] Properties;
        Int32 CollisionObject; //-1 -> None
        public NodeGeometry(ref BinaryReader reader)
            : base(ref reader)
        {
            Translation = reader.ReadVector3();
            Rotation = reader.ReadMatrix33();
            Scale = reader.ReadSingle();
            PropertiesCount = reader.ReadUInt32();
            Properties = new UInt32[PropertiesCount];
            for (int i = 0; i < PropertiesCount; i++)
            {
                Properties[i] = reader.ReadUInt32();
            }
            CollisionObject = reader.ReadInt32();
        }
    }
    #endregion
    #region Nodes

    public class NiNode : NodeGeometry
    {
        UInt32 ChildrenCount;
        UInt32[] Children;
        UInt32 EffectsCount;
        UInt32[] Effects;
        public NiNode(ref BinaryReader reader)
            : base(ref reader)
        {
            ChildrenCount = reader.ReadUInt32();
            Children = new UInt32[ChildrenCount];
            for (int i = 0; i < ChildrenCount; i++)
            {
                Children[i] = reader.ReadUInt32();
            }
            EffectsCount = reader.ReadUInt32();
            Effects = new UInt32[EffectsCount];
            for (int i = 0; i < EffectsCount; i++)
            {
                Effects[i] = reader.ReadUInt32();
            }
        }
    }

    public class NiTriShape : NodeGeometry
    {
        UInt32 Data;
        Int32 SkinInstance;
        byte HasShader;
        public NiTriShape(ref BinaryReader reader)
            : base(ref reader)
        {
            Data = reader.ReadUInt32();
            SkinInstance = reader.ReadInt32();
            HasShader = reader.ReadByte();
        }
    }
    public class NiZBufferProperty : NodeGeneral
    {
        UInt32 functionID;
        public NiZBufferProperty(ref BinaryReader reader)
            :base(ref reader)
        {
            functionID = reader.ReadUInt32();
        }

    }

    public class NiMultiTargetTransformController:NodeController
    {
        UInt16 ExtraTargetsCount;
        UInt32[] ExtraTargets;
        public NiMultiTargetTransformController(ref BinaryReader reader)
            :base(ref reader)
        {
            ExtraTargetsCount = reader.ReadUInt16();
            ExtraTargets = new UInt32[ExtraTargetsCount];
            for(int i=0;i<ExtraTargetsCount;i++)
            {
                ExtraTargets[i] = reader.ReadUInt32();
            }
        }
    }
    public class NiTransformController : NodeController
    {
        UInt32 Interpolator;
        public NiTransformController(ref BinaryReader reader)
            : base(ref reader)
        {
            Interpolator = reader.ReadUInt32();
        }
    }
    public class NiVertexColorProperty:NodeGeneral
    {
        UInt32 VertexMode;
        UInt32 LightingMode;
        public NiVertexColorProperty(ref BinaryReader reader)
            :base(ref reader)
        {
            VertexMode = reader.ReadUInt32();
            LightingMode = reader.ReadUInt32();
        }
    }

    public class NiStringExtraData:NodeNamed
    {
        string StringData;
        public NiStringExtraData(ref BinaryReader reader)
            :base(ref reader)
        {
            StringData = reader.ReadStringWithLength();
        }
    }
    public class NiTexturingProperty : NodeGeneralNoFlag
    {
        UInt32 ApplyMode;
        UInt32 TextureCount;
        byte[] HasXTexture; //Base, Dark, etc. see: nifscope UI
        UInt32[] Sources;
        UInt32[] ClampModes;
        UInt32[] FilterModes;
        UInt32[] UVSets;
        byte[] HasTextureTransform;
        UInt32 ShaderTexturesCount;
        UInt32[] ShaderTextures;

        public NiTexturingProperty(ref BinaryReader reader)
            : base(ref reader)
        {
            ApplyMode = reader.ReadUInt32();
            TextureCount = reader.ReadUInt32();
            HasXTexture = new byte[TextureCount];
            Sources = new UInt32[TextureCount];
            ClampModes = new UInt32[TextureCount];
            FilterModes = new UInt32[TextureCount];
            UVSets = new UInt32[TextureCount];
            HasTextureTransform = new byte[TextureCount];
            for (int i = 0;i<TextureCount;i++)
            {
                HasXTexture[i] = reader.ReadByte();
                if(HasXTexture[i] != 0)
                {
                    Sources[i] = reader.ReadUInt32();
                    ClampModes[i] = reader.ReadUInt32();
                    FilterModes[i] = reader.ReadUInt32();
                    UVSets[i] = reader.ReadUInt32();
                    HasTextureTransform[i] = reader.ReadByte();
                }
            }
            ShaderTexturesCount = reader.ReadUInt32();
            ShaderTextures = new UInt32[TextureCount];
            if (ShaderTexturesCount > 1)
                MessageBox.Show("Shader textures > 1. Tell me if it worked properly.");
            for (int i = 0;i<ShaderTexturesCount;i++)
            {
                ShaderTextures[i] = reader.ReadUInt32();
            }
        }
    }

    public class NiSourceTexture : NodeGeneralNoFlag
    {
        byte UseExternal;
        string FilePath;
        Int32 PixelData;
        UInt32 PixelLayout;
        UInt32 UseMipmaps;
        UInt32 AlphaFormat;
        byte IsStatic;
        byte DirectRender;
        public NiSourceTexture(ref BinaryReader reader)
            : base(ref reader)
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
    }

    public class NiAlphaProperty:NodeGeneral
    {
        byte Threshold;
        public NiAlphaProperty(ref BinaryReader reader)
            :base(ref reader)
        {
            Threshold = reader.ReadByte();
        }
    }

    public class NiMaterialProperty:NodeGeneralNoFlag
    {
        Color3 AmbientColor;
        Color3 DiffuseColor;
        Color3 SpecularColor;
        Color3 EmissiveColor;
        float Glossiness;
        float Alpha;
        public NiMaterialProperty(ref BinaryReader reader)
            : base(ref reader)
        {
            AmbientColor = reader.ReadColor3();
            DiffuseColor = reader.ReadColor3();
            SpecularColor = reader.ReadColor3();
            EmissiveColor = reader.ReadColor3();
            Glossiness = reader.ReadSingle();
            Alpha = reader.ReadSingle();
        }
    }

    public class NiAlphaController:NodeController
    {
        Int32 Interpolator;
        public NiAlphaController(ref BinaryReader reader)
            :base(ref reader)
        {
            Interpolator = reader.ReadInt32();
        }
    }

    public class NiTriShapeData : NodeSimple
    {
        Int32 GroupID;
        UInt16 VerticesCount;
        byte KeepFlags;
        byte CompressFlags;
        byte HasVertices;
        Vector3[] Vertices;
        UInt16 DataFlags;
        byte HasNormals;
        Vector3[] Normals;
        Vector3 BoundingSphereCenter;
        float BoundingSphereRadius;
        byte HasVertexColors;
        Color4[] VertexColors;
        TexCoord[] UVSets;
        UInt16 ConsistencyFlags;
        Int32 AdditionalData;
        UInt16 TrianglesCount;
        UInt32 TrianglePointsCount;
        Triangle[] Triangles;
        UInt16 MatchGroupsCount;
        MatchGroup[] MatchGroups;

        byte HasTriangles;

        public NiTriShapeData(ref BinaryReader reader)
            : base(ref reader)
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
                {
                    Vertices[i] = reader.ReadVector3();
                }
            }
            DataFlags = reader.ReadUInt16();
            HasNormals = reader.ReadByte();
            if (HasNormals != 0)
            {
                Normals = new Vector3[VerticesCount];
                for (int i = 0; i < VerticesCount; i++)
                {
                    Normals[i] = reader.ReadVector3();
                }
            }
            BoundingSphereCenter = reader.ReadVector3();
            BoundingSphereRadius = reader.ReadSingle();
            HasVertexColors = reader.ReadByte();
            if (HasVertexColors != 0)
            {
                VertexColors = new Color4[VerticesCount];
                for (int i = 0; i < VerticesCount; i++)
                {
                    VertexColors[i] = reader.ReadColor4();
                }
            }
            UVSets = new TexCoord[VerticesCount];
            for (int i = 0; i < VerticesCount; i++)
            {
                UVSets[i] = reader.ReadTexCoord();
            }
            ConsistencyFlags = reader.ReadUInt16();
            AdditionalData = reader.ReadInt32();
            TrianglesCount = reader.ReadUInt16();
            TrianglePointsCount = reader.ReadUInt32();
            HasTriangles = reader.ReadByte();
            Triangles = new Triangle[TrianglesCount];
            for(int i = 0; i<TrianglesCount;i++)
            {
                Triangles[i] = reader.ReadTriangle();
            }
            MatchGroupsCount = reader.ReadUInt16();
            MatchGroups = new MatchGroup[MatchGroupsCount];
            for (int i = 0; i < MatchGroupsCount; i++)
            {
                MatchGroups[i] = reader.ReadMatchGroup();
            }
        }
    }

    public class NiVisController : NiAlphaController
    {
        public NiVisController(ref BinaryReader reader)
            : base(ref reader)
        {
        }
    }

    public class NiBillboardNode : NiNode
    {
        UInt16 BillboardMode;
        public NiBillboardNode(ref BinaryReader reader)
            : base(ref reader)
        {
            BillboardMode = reader.ReadUInt16();
        }
    }

    public class NiSkinInstance : NodeSimple
    {
        UInt32 Data;
        UInt32 SkinPartition;
        UInt32 SkeletonRoot;
        UInt32 BonesCount;
        UInt32[] Bones;
        public NiSkinInstance(ref BinaryReader reader)
            : base(ref reader)
        {
            Data = reader.ReadUInt32();
            SkinPartition = reader.ReadUInt32();
            SkeletonRoot = reader.ReadUInt32();
            BonesCount = reader.ReadUInt32();
            Bones = new UInt32[BonesCount];
            for(int i = 0; i<BonesCount;i++)
            {
                Bones[i] = reader.ReadUInt32();
            }
        }
    }

    public class NiSkinData : NodeSimple
    {
        Matrix33 SkinRotation;
        Vector3 SkinTranslation;
        UInt32 SkinScale;
        UInt32 BonesCount;
        byte HasVertexWeights;
        Bone[] Bones;
        public NiSkinData(ref BinaryReader reader)
            : base(ref reader)
        {
            SkinRotation = reader.ReadMatrix33();
            SkinTranslation = reader.ReadVector3();
            SkinScale = reader.ReadUInt32();
            BonesCount = reader.ReadUInt32();
            HasVertexWeights = reader.ReadByte();
            Bones = new Bone[BonesCount];
            for (int i = 0; i < BonesCount; i++)
            {
                Bones[i] = reader.ReadBone(HasVertexWeights);
            }
        }
    }
    public class NiSkinPartition : NodeSimple
    {
        UInt32 PartitionsCount;
        Partition[] Partitions;
        public NiSkinPartition(ref BinaryReader reader)
            : base(ref reader)
        {
            PartitionsCount = reader.ReadUInt32();
            Partitions = new Partition[PartitionsCount];
            for (int i = 0; i < PartitionsCount; i++)
            {
                Partitions[i] = reader.ReadPartition();
            }
        }
    }
    #endregion
    public class NifFile
    {
        public string path;
        public NifHeader header;
        NodeSimple[] nodes;
        public void Read(BinaryReader reader)
        {
            header = new NifHeader();
            header.Read(ref reader);
            nodes = new NodeSimple[header.BlockCount];
            for (int i = 0; i < header.BlockCount; i++)
            {
                Type nodeType = Type.GetType("Civ4NifReader." + header.Blocks[i]);
                if (nodeType == null)
                {
                    MessageBox.Show($"Node type " + header.Blocks[i] + " not supported.");
                    return;
                }
                Type[] paramTypes = new Type[] { typeof(BinaryReader).MakeByRefType() };

                // 2. Find the specific constructor
                ConstructorInfo ctor = nodeType.GetConstructor(paramTypes);

                if (ctor == null)
                {
                    throw new Exception("Constructor with 'ref BinaryReader' for " + header.Blocks[i] + " not found (or I forgot to make it public).");
                }

                // 3. Prepare the arguments in an array
                object[] args = new object[] { reader };

                // 4. Invoke the constructor
                object o = ctor.Invoke(args);

                //object o = Activator.CreateInstance(nodeType, ref reader);
                if (o is NodeSimple)
                    nodes[i] = o as NodeSimple;
                else
                {
                    MessageBox.Show($"Type " + header.Blocks[i] + "is not a node.");
                    return;
                }
            }
        }
    }
}
