using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.XPath;

namespace KFM_Utility
{
    public class XMLData : Singleton<XMLData>
    {
        /*
         * <KFM>
         *  <Header />
         *  <NIF />
         *  <Master />
         *  <Unknown />
         *  <Animations>
         *   <Animation>
         *    <Event />
         *    <Variation />
         *    <File />
         *   </Animation>
         *  </Animations>
         * </KFM>
         */
        public void Load(string path)
        {
            XPathDocument reader = new XPathDocument(path);
            XPathNavigator nav = reader.CreateNavigator();

            KFMData kfmData = KFMData.Instance;
            AnimCodes animcodes = AnimCodes.Instance;

            kfmData.Header = GetValue("/KFM/Header",nav);
            kfmData.NifFile = GetValue("/KFM/NIF", nav);
            kfmData.Master = GetValue("/KFM/Master", nav);
            kfmData.UnknownData = GetValue("/KFM/Unknown", nav);

            XPathExpression expr = nav.Compile("/KFM/Animations/Animation");
            XPathNodeIterator iterator = nav.Select(expr);
            while (iterator.MoveNext())
            {
                XPathNavigator nav2 = iterator.Current.Clone();
                Animation anim = new Animation();
                //anim.EventCode = Convert.ToInt32(GetValue("Event", nav2));
                string ecd = GetValue("Event", nav2);
                short EvntCde;
                if (animcodes.GetByName(ecd) != -1)
                {
                    EvntCde = animcodes.GetByName(ecd);
                }
                else
                {
                    EvntCde = Convert.ToInt16(ecd);
                }
                anim.EventCode = EvntCde;
                anim.AnimFile = GetValue("File", nav2);
                anim.VariationCode = Convert.ToInt16(GetValue("Variation", nav2));
                kfmData.AddAnimation(anim);
            }
        }
        private string GetValue(string pth,XPathNavigator nav)
        {
            XPathExpression expr;
            XPathNodeIterator iterator;

            expr = nav.Compile(pth);
            iterator = nav.Select(expr);
            while (iterator.MoveNext())
            {
                XPathNavigator nav2 = iterator.Current.Clone();
                return nav2.Value;
            }
            return "";
        }

        public void Save(string path)
        {
            if (File.Exists(path))
            {
                File.Copy(path, path + ".tmp", true);
                File.Delete(path);
            }

            XmlTextWriter writer = new XmlTextWriter(path, Encoding.Default);
            KFMData kfmData = KFMData.Instance;
            AnimCodes animcodes = AnimCodes.Instance;

            string data = "";

            data += "<KFM>";
            data += "\n\t<Header>" + kfmData.Header + "</Header>";
            data += "\n\t<NIF>" + kfmData.NifFile + "</NIF>";
            data += "\n\t<Master>" + kfmData.Master + "</Master>";
            data += "\n\t<Unknown>" + kfmData.UnknownData + "</Unknown>";
            data += "\n\t<Animations>";
            foreach(Animation anim in kfmData.Animations)
            {
                data += "\n\t\t<Animation>";
                data += "\n\t\t\t<Event>";
                if (animcodes.GetByVal(anim.EventCode) != null)
                {
                    data += animcodes.GetByVal(anim.EventCode);
                }
                else
                {
                    data += anim.EventCode;
                }
                data += "</Event>";
                data += "\n\t\t\t<Variation>" + anim.VariationCode + "</Variation>";
                data += "\n\t\t\t<File>"+anim.AnimFile+"</File>";
                data += "\n\t\t</Animation>";
            }
            data += "\n\t</Animations>";
            data += "\n</KFM>";

            writer.WriteRaw(data);
            writer.Close();

            if (File.Exists(path + ".tmp"))
            {
                File.Delete(path + ".tmp");
            }
        }
        protected override void Instantiate()
        { }
    }
}
