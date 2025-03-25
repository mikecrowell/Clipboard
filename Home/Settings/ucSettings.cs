using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clipboard.UI.DirectInstall;
using Clipboard.UI._Helper;
using Clipboard.UI.Home.Settings.Tools;

namespace Clipboard.UI.Home.Settings
{
    public partial class ucSettings : DevExpress.XtraEditors.XtraUserControl
    {
        public ucSettings()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            btnProjects.ItemClick += BtnProjects_ItemClick;
            btnRetrofits.ItemClick += BtnRetrofits_ItemClick;
            btnQA.ItemClick += BtnQA_ItemClick;
            btnSavingData.ItemClick += BtnSavingData_ItemClick;
            //btnTesting.ItemClick += BtnTesting_ItemClick;

            pnlTesting.Visible = !MainFromUserControls.MainForm.IsRelease;

        }

        private void BtnQA_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            try
            {
                MainFromUserControls.MainForm.SetUserControl(new ucQA());
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }

        private void BtnTesting_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            try
            {
                MainFromUserControls.MainForm.SetUserControl(new ucTesting());
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }

        public void SetSkin()
        {
            this.LookAndFeel.SkinName = MainFromUserControls.MainForm.SkinName;
        }


        


        // Projects
        private void BtnProjects_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            try
            {
                MainFromUserControls.MainForm.SetUserControl(new ucProjectSettings());
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }

        // Split Grid
        private void btnSplitGrid_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            try
            {
                MainFromUserControls.MainForm.SetUserControl(new ucSplitGrid());
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }

        // Retrofits
        private void BtnRetrofits_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            try
            {
                MainFromUserControls.MainForm.SetUserControl(new ucRetrofitSettings());
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }

        // Retrofits
        private void BtnSavingData_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            try
            {
                MainFromUserControls.MainForm.SetUserControl(new ucSavingData());
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }


        #region Bottom Buttons

        private void btnBack_Click(object sender, EventArgs e)
        {
            MainFromUserControls.MainForm.SetUserControl(MainFromUserControls.UcHome);
        }



        #endregion

       
    }
}
