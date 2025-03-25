using Clipboard.MultiMedia.SendDataFiles;
using DevExpress.LookAndFeel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FieldTool.UI
{
    public partial class frmSendDataFiles : DevExpress.XtraEditors.XtraForm, ISendDataFiles
    {
        public frmSendDataFiles(string userName, string title)
        {
            InitializeComponent();
            this.LookAndFeel.SkinName = "Office 2013";
            var uc = new Clipboard.MultiMedia.SendDataFiles.ucSendDataFiles(this, false, userName, title, "Office 2013", "");
            this.Controls.Add(uc);
            LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
        }


        public void BackClick()
        {
            this.Close();
        }

        public void Loading(bool show)
        {
            if (show)
            {
                Cursor.Current = Cursors.WaitCursor;
            }
            else
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public void Error(Exception ex)
        {
            throw ex;
        }







    }
}
