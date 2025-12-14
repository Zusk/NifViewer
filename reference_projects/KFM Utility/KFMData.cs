using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace KFM_Utility
{
    public class Animation
    {
        private short iEventCode;
        public short EventCode
        {
            get { return iEventCode; }
            set { iEventCode = value; }
        }

        private short iVariationCode;
        public short VariationCode
        {
            get { return iVariationCode; }
            set { iVariationCode = value; }
        }
        
        private string sAnimFile;
        public string AnimFile
        {
            get { return sAnimFile; }
            set { sAnimFile = value; }
        }
    }

    public class KFMData : Singleton<KFMData>
    {
        public void Save(string filename)
        {
            fileStream = File.OpenWrite(filename+".tmp");
            kfmWriter = new BinaryWriter(fileStream);

            // Header
            SetUnsizedString(Header);

            // NIF File
            SetSizedString(NifFile);

            // Master
            SetSizedString(Master);

            // Always the same.
            // 01 00 00 00 00 00 00 00 CD CC CC 3D CD CC CC 3D
            kfmWriter.Write((byte)0x01);
            kfmWriter.Write((byte)0x00);
            kfmWriter.Write((byte)0x00);
            kfmWriter.Write((byte)0x00);
            kfmWriter.Write((byte)0x00);
            kfmWriter.Write((byte)0x00);
            kfmWriter.Write((byte)0x00);
            kfmWriter.Write((byte)0x00);
            kfmWriter.Write((byte)0xCD);
            kfmWriter.Write((byte)0xCC);
            kfmWriter.Write((byte)0xCC);
            kfmWriter.Write((byte)0x3D);
            kfmWriter.Write((byte)0xCD);
            kfmWriter.Write((byte)0xCC);
            kfmWriter.Write((byte)0xCC);
            kfmWriter.Write((byte)0x3D);
            
            // Number of Animations.
            int numAnimations = Animations.Count;
            kfmWriter.Write(numAnimations);

            // Animations Loop
            for (int i = 0; i < numAnimations; i++)
            {
                // Current Animation
                Animation anim = Animations[i];
                
                // Write the Type and File.
                kfmWriter.Write((short)anim.EventCode);
                kfmWriter.Write((short)anim.VariationCode);
                //int AnimationNumber = 0;
                
                SetSizedString(anim.AnimFile);

                kfmWriter.Write((int)0);
                kfmWriter.Write((int)(numAnimations-1));
                for (int j = 0; j < numAnimations; j++)
                {
                    if (i != j)
                    {
                        int animcode = Animations[j].EventCode;
                        kfmWriter.Write((int)animcode);
                        kfmWriter.Write((int)5);
                    }
                }
            }
            kfmWriter.Write((int)0);
            kfmWriter.Close();

            File.Delete(filename);
            File.Copy(filename + ".tmp", filename);
            File.Delete(filename + ".tmp");
        }

        private void SetSizedString(string text)
        {
            kfmWriter.Write((int)text.Length);
            byteArray(text);
        }
        private void byteArray(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                kfmWriter.Write(text[i]);
            }
        }
        private void SetUnsizedString(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                kfmWriter.Write(text[i]);
            }
            kfmWriter.Write((char)0x01);
        }
        BinaryWriter kfmWriter;
        //-----------------------------------------------------------------------------------

        public void Load(string filename)
        {
            fileStream = File.OpenRead(filename);
            kfmReader = new BinaryReader(fileStream);
            //Form1.Message(filename);

            // ;Gamebryo etc.
            Header = GetUnsizedString();
            //Form1.Message(Header);

            // NIF File
            NifFile = GetSizedString();
            //Form1.Message(NifFile);

            // "MD"
            Master = GetSizedString();
            kfmReader.ReadByte();

            // Always the same.
            // 00 00 00 00 00 00 00 CD CC CC 3D CD CC CC 3D
            kfmReader.ReadBytes(15);
            
            // Number of Animations
            int numAnimations = kfmReader.ReadInt32();
            //Form1.Message("Animations = " + numAnimations);

            // Foreach animation in animations
            for (int i = 0; i < numAnimations; i++)
            {
                // Get the Animation Type
                //int AnimType = kfmReader.ReadInt32();
                short AnimType = kfmReader.ReadInt16();
                short VariType = kfmReader.ReadInt16();
                //Form1.Message("AnimType = " + AnimType);

                // The Animation file.
                string AnimFile = GetSizedString();
                //Form1.Message("AnimFile = " + AnimFile);

                Animation animation = new Animation();
                animation.AnimFile = AnimFile;
                animation.EventCode = AnimType;
                animation.VariationCode = VariType;

                AddAnimation(animation);

                // Skip all the data. It seems to be irrelevant.
                List<int> ArrayData = ReadArray();
                //foreach (int integer in ArrayData)
                //{
                //    Form1.Message("ArrayData = " + integer);
                //}
            }
            kfmReader.Close();
        }
        private List<int> ReadArray()
        {
            List<int> ret = new List<int>();
            kfmReader.ReadBytes(4);
            int size = kfmReader.ReadInt32();
            //Form1.Message(size.ToString());
            for (int i = 0; i < size; i++)
            {
                ret.Add(kfmReader.ReadInt32());
                kfmReader.ReadInt32();
            }
            return ret;
        }
        private string ReadChars(int size)
        {
            string ret = "";
            for (int i = 0; i < size; i++)
            {
                ret += kfmReader.ReadChar();
            }
            return ret;
        }
        private string GetUnsizedString()
        {
            string header = "";
            while (kfmReader.PeekChar() != (char)0x01)
            {
                header += kfmReader.ReadChar();
            }
            kfmReader.ReadChar();
            return header;
        }
        private string GetSizedString()
        {
            //int size = (int)kfmReader.ReadByte();
            string ret = "";
            //kfmReader.ReadChars(3);
            int size = kfmReader.ReadInt32();
            for (int i = 0; i < size; i++)
            {
                ret += kfmReader.ReadChar();
            }
            return ret;
        }

        private FileStream fileStream;
        private BinaryReader kfmReader;

        //--------------------------------------------------------------------------------
        private string sHeader;
        public string Header
        {
            get { return sHeader; }
            set { sHeader = value; }
        }

        private string sNifFileName;
        public string NifFile
        {
            get { return sNifFileName; }
            set { sNifFileName = value; }
        }

        private string sMaster;
        public string Master
        {
            get { return sMaster; }
            set { sMaster = value; }
        }

        private string sUnknownData;
        public string UnknownData
        {
            get { return sUnknownData; }
            set { sUnknownData = value; }
        }

        private List<Animation> lAnimations;
        public List<Animation> Animations
        {
            get { return lAnimations; }
            set { lAnimations = value; }
        }
        public void AddAnimation(Animation anim)
        {
            if (lAnimations == null)
            {
                lAnimations = new List<Animation>();
            }

            for (int i = 0; i < lAnimations.Count; i++)
            {
                if (lAnimations[i].AnimFile == anim.AnimFile)
                    return;
                if (lAnimations[i].EventCode == anim.EventCode)
                    return;
            }
            lAnimations.Add(anim);
        }
        protected override void Instantiate()
        {
            sHeader = "";
            sNifFileName = "";
            sMaster = "";
            sUnknownData = "";
            lAnimations = new List<Animation>();
        }
    }
}
