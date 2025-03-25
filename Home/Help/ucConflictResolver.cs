using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clipboard.UI._Helper;
using Clipboard.Helper.Help;
using FieldTool.BLL;

namespace Clipboard.UI.Home.Help
{
    public partial class ucConflictResolver : DevExpress.XtraEditors.XtraUserControl
    {
        public ucConflictResolver()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.LookAndFeel.SkinName = MainFromUserControls.MainForm.SkinName;

            btnResolveConflicts.Enabled = false;

            btnResolveConflicts.ItemClick += BtnResolveConflicts_ItemClick;
            btnSearchForConflicts.ItemClick += BtnSearchForConflicts_ItemClick;
        }

        private void BtnSearchForConflicts_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            try
            {
                if (ConflictResolverHelper.RunAll(txtStatus, updateData: false, runMinor: true))
                {
                    btnResolveConflicts.Enabled = true;
                    MessageBox.Show($"Conflicts Found{Environment.NewLine}Please click the Resolve Conflicts button at the top");
                }

                
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }

        private void BtnResolveConflicts_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show($"Are you sure? {Environment.NewLine} Resolving conflicts may result in the loss of data...", "Update Data Files", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    DataStore.SaveData(true);
                    ConflictResolverHelper.RunAll(txtStatus, updateData: true, runMinor: true);
                    MainFromUserControls.UcDirectInstall = null;
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            MainFromUserControls.MainForm.SetUserControl(MainFromUserControls.UcHome);
            this.Dispose();
        }
    }
}
