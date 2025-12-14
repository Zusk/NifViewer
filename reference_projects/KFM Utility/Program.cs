/**
 * <Author>Esemjay</Author>
 * <Version>1.0</Version>
 * <Usage>Converts KFM <---> XML</Usage>
 */

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KFM_Utility
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}