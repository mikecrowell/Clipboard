﻿using System;
using System.Windows.Forms;
using VideoCapture;

namespace VS2013VideoCapture {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMultimedia());
        }
    }
}
