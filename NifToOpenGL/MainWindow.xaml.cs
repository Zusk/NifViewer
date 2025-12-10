using Microsoft.Win32; // Required for OpenFileDialog
using System;
using System.IO;
using System.Windows;

namespace Civ4NifReader
{
    public partial class MainWindow : Window
    {
        // Keep a reference to the loaded file
        NifFile nif_file;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Optional: Filter for specific extensions
            openFileDialog.Filter = "Binary files (*.nif)|*.nif|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                ProcessBinaryFile(filePath);
            }
        }

        private void ProcessBinaryFile(string path)
        {
            nif_file = new NifFile();
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    nif_file.Read(reader);
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Error reading file: {ex.Message}");
            }
        }
    }
}