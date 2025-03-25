using Clipboard.MultiMedia.DataRecovery;
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
    public partial class frmDataFileRecovery : Form, ISelfDataFileRecovery
    {
        public frmMain _frmMainParent { get; set; }

        public frmDataFileRecovery(frmMain frmMainParent)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            _frmMainParent = frmMainParent;
            var uc = new ucSelfDataFileRecovery(this);
            this.Controls.Add(uc);
        }

        public void BackClick()
        {
            this.Close();
            _frmMainParent.Close();
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

    }
}
