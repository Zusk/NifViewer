using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace KFM_Utility
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AnimCodes animcodes = AnimCodes.Instance;
            animcodes.Load();
        }

        private void KFM_Open_Click(object sender, EventArgs e)
        {
            OpenClick(KFMFileDialog, KFMBox, DecodeButton);
        }

        private void OpenClick(FileDialog dialog, TextBox box, Button button)
        {
            dialog.ShowDialog();
            if (!dialog.FileName.Equals(""))
            {
                box.Text = dialog.FileName;
            }
        }

        private void XML_Open_Click(object sender, EventArgs e)
        {
            OpenClick(XMLFileDialog, XMLBox, EncodeButton);
        }

        public static void Message(string text)
        {
            MessageBox.Show(text);
        }
        public void Message2(string text)
        {
            Status.Text = text;
        }


        private void EncodeButton_Click(object sender, EventArgs e)
        {
            Message2("");
            if (File.Exists(XMLBox.Text))
            {

                // Load the Xml File.
                XMLData xmlData = XMLData.Instance;
                xmlData.Load(XMLBox.Text);

                // Convert the data.
                KFMData kfmData = KFMData.Instance;
                kfmData.Save(KFMBox.Text);

                xmlData.Clear();
                kfmData.Clear();

                Message2("Encoding Complete.");
            }
            else
            {
                Message2("Error Loading XML File - Does not exist.");
            }
        }
        private void DecodeButton_Click(object sender, EventArgs e)
        {
            Message2("");
            if (File.Exists(KFMBox.Text))
            {
                // Decode the XML.
                XMLData xmlData = XMLData.Instance;
                KFMData kfmData = KFMData.Instance;
                
                kfmData.Load(KFMBox.Text);
                xmlData.Save(XMLBox.Text);

                xmlData.Clear();
                kfmData.Clear();

                Message2("Decoding Complete.");
            }
            else
            {
                Message2("Error Loading KFM File - Does not exist.");
            }
        }
    }
}