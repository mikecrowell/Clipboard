using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clipboard.MultiMedia.DataRecovery;

namespace Clipboard.UI.Home.Help
{
    public partial class ucDataFileRecovery : DevExpress.XtraEditors.XtraUserControl, ISelfDataFileRecovery
    {
        public ucDataFileRecovery()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.LookAndFeel.SkinName = MainFromUserControls.MainForm.SkinName;

            MainFromUserControls.MainForm.PleaseWait = true;

            var uc = new ucSelfDataFileRecovery(this, this.LookAndFeel.SkinName);
            this.Controls.Add(uc);
        }


        public void BackClick()
        {
            MainFromUserControls.MainForm.Close();
        }

        public void Loading(bool show) {
            MainFromUserControls.MainForm.PleaseWait = show;
        }






    }
}
