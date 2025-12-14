using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace KFM_Utility
{
    public class AnimCodes : Singleton<AnimCodes>
    {
        /*
         * <AnimCodes>
         *  <Animation>
         *   <Code />
         *   <Name />
         *  </Animation>
         * </AnimCodes>
         */
        public void Load()
        {
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            string xmlFile = path+"\\AnimCodes.xml";

            XPathDocument reader = new XPathDocument(xmlFile);
            XPathNavigator nav = reader.CreateNavigator();

            XPathExpression expr = nav.Compile("/AnimCodes/Animation");
            XPathNodeIterator iterator = nav.Select(expr);
            while (iterator.MoveNext())
            {
                XPathNavigator nav2 = iterator.Current.Clone();
                string scnt = GetValue("Code", nav2);
                short code = Convert.ToInt16(scnt);
                string name = GetValue("Name", nav2);
                Add(name, code);
            }
        }
        private string GetValue(string pth, XPathNavigator nav)
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
        //------------------------------------------------------------------------------
        private Dictionary<string, short> string_int;
        private Dictionary<short, string> int_string;

        protected override void Instantiate()
        {
            string_int = new Dictionary<string, short>();
            int_string = new Dictionary<short, string>();
        }

        public void Add(string name, short code)
        {
            //if (string_int == null) string_int = new Dictionary<string, int>();
            //if (int_string == null) int_string = new Dictionary<int, string>();
            if (string_int.ContainsKey(name))
            {
                string_int[name] = code;
                int_string[code] = name;
            }
            else
            {
                string_int.Add(name, code);
                int_string.Add(code, name);
            }
        }
        public string GetByVal(short i)
        {
            if (!int_string.ContainsKey(i)) return null;
            return int_string[i];
        }
        public short GetByName(string s)
        {
            if (!string_int.ContainsKey(s)) return -1;
            return string_int[s];
        }
    }
}
