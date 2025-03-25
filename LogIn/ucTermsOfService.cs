using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Clipboard.UI.LogIn
{
    public partial class ucTermsOfService : DevExpress.XtraEditors.XtraUserControl
    {
        public ucTermsOfService()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.LookAndFeel.SkinName = MainFromUserControls.MainForm.SkinName;
            pdfViewer1.DetachStreamAfterLoadComplete = true;
            LoadPDF();
        }

        private void LoadPDF()
        {
            string pdfFilePath = System.Environment.CurrentDirectory + @"\LogIn\assets\termsOfService.pdf";

            if (File.Exists(pdfFilePath))
            {
                using (FileStream stream = new FileStream(pdfFilePath, FileMode.Open))
                {
                    pdfViewer1.LoadDocument(stream);
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            MainFromUserControls.MainForm.SetUserControl(MainFromUserControls.UcLogIn);
        }
    }
}
