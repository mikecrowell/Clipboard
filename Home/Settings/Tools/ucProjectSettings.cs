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
using FieldTool.BLL.ClipboardConfiguration;
using Clipboard.UI._Helper;
using Clipboard.Helper.Home.Settings;
using DevExpress.XtraEditors;
using FieldTool.Constants.DirectInstall.Projects;
using FieldTool.BLL;

namespace Clipboard.UI.Home.Settings.Tools
{
    public partial class ucProjectSettings : DevExpress.XtraEditors.XtraUserControl
    {

        public List<ToggleSwitch> _groupList = new List<ToggleSwitch>();
        public List<ToggleSwitch> _orderList = new List<ToggleSwitch>();


        public ucProjectSettings()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.LookAndFeel.SkinName = MainFromUserControls.MainForm.SkinName;

            btnSave.Click += btnSave_Click;
            btnDefault.Click += btnDefault_Click;
            InitGroupAndOrder();


            if (SelectedItems.UserClipboardConfig.ProjectUserConfig == null)
            {
                _projectUserConfig = new ProjectUserConfig();
            }
            else
            {
                _projectUserConfig = SelectedItems.UserClipboardConfig.ProjectUserConfig;
            }

            SetConfiguration();
        }


        private void InitGroupAndOrder()
        {
            // Group
            tglGroupParentAccountName.Tag = ProjectTableColumnConstants.ParentAccountName;
            tglGroupCity.Tag = ProjectTableColumnConstants.City;
            tglGroupProgram.Tag = ProjectTableColumnConstants.ProgramName;
            tglGroupStreet.Tag = ProjectTableColumnConstants.Street;
            tglGroupRoute.Tag = ProjectTableColumnConstants.RouteId;
            tgleGroupZipcode.Tag = ProjectTableColumnConstants.Zipcode;
            _groupList.Add(tglGroupParentAccountName);
            _groupList.Add(tglGroupCity);
            _groupList.Add(tglGroupProgram);
            _groupList.Add(tglGroupStreet);
            _groupList.Add(tglGroupRoute);
            _groupList.Add(tgleGroupZipcode);

            // Order
            tglOrderZipcode.Tag = ProjectTableColumnConstants.Zipcode;
            tglOrderCity.Tag = ProjectTableColumnConstants.City;
            _orderList.Add(tglOrderZipcode);
            _orderList.Add(tglOrderCity);

            // Group
            tglGroupCity.Click += GroupColumn_Click;
            tglGroupParentAccountName.Click += GroupColumn_Click;
            tglGroupProgram.Click += GroupColumn_Click;
            tglGroupStreet.Click += GroupColumn_Click;
            tglGroupRoute.Click += GroupColumn_Click;
            tgleGroupZipcode.Click += GroupColumn_Click;

            // ORder
            tglOrderZipcode.Click += OrderColumn_Click;
            tglOrderCity.Click += OrderColumn_Click;
        }

        private void GroupColumn_Click(object sender, EventArgs e)
        {
            try
            {
                ToggleSwitch tgl = (ToggleSwitch)sender;
                foreach (var item in _groupList)
                {
                    if (item.IsOn && item.Tag != tgl.Tag)
                    {
                        item.Toggle();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }

        private void OrderColumn_Click(object sender, EventArgs e)
        {
            try
            {
                ToggleSwitch tgl = (ToggleSwitch)sender;
                foreach (var item in _orderList)
                {
                    if (item.IsOn && item.Tag != tgl.Tag)
                    {
                        item.Toggle();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }

        public ProjectUserConfig _projectUserConfig { get; set; }

        private void SetConfiguration()
        {
            // Hide / Show
            tglDetails.IsOn = _projectUserConfig.ShowDetailColumn;
            tglProgramName.IsOn = _projectUserConfig.ShowProgramNameColumn;
            tglTime.IsOn = _projectUserConfig.ShowTimeColumn;
            tglShowParentAccountName.IsOn = _projectUserConfig.ShowParentAccountNameColumn;
            tglShowCity.IsOn = _projectUserConfig.ShowCityColumn;
            tglShowZipcode.IsOn = _projectUserConfig.ShowZipcodeColumn;
            tgleShowStreet.IsOn = _projectUserConfig.ShowStreet;
            tglShowRouteId.IsOn = _projectUserConfig.ShowRouteId;
            tglShowPhone.IsOn = _projectUserConfig.ShowPhone;
            tglShowAltPhone.IsOn = _projectUserConfig.ShowAltPhone;
            tglBrandingKey.IsOn = _projectUserConfig.ShowBrandingKey;
            tglShowClock.IsOn = _projectUserConfig.ShowStartStopTime;

            // Group
            ToggleSwitch groupTgl = _groupList.Where(x => x.Tag?.ToString() == _projectUserConfig.GroupedColumn).FirstOrDefault();
            if (groupTgl != null) 
            {
                groupTgl.IsOn = true;
            }


            // Order
            ToggleSwitch orderTgl = _orderList.Where(x => x.Tag?.ToString() == _projectUserConfig.OrderByColumn).FirstOrDefault();
            if (orderTgl != null)
            {
                orderTgl.IsOn = true;
            }

            rdoRowHeight.SelectedIndex = UserConfigurationSettingsHelper.SetRowHeightSelected(_projectUserConfig.RowHeight);
            rdoFontSize.SelectedIndex = UserConfigurationSettingsHelper.SetFontSizeSelected(_projectUserConfig.FontSize);

        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                MainFromUserControls.MainForm.PleaseWait = true;
                // Hide Show
                _projectUserConfig.ShowDetailColumn = tglDetails.IsOn;
                _projectUserConfig.ShowProgramNameColumn = tglProgramName.IsOn;
                _projectUserConfig.ShowTimeColumn = tglTime.IsOn;
                _projectUserConfig.ShowCityColumn = tglShowCity.IsOn;
                _projectUserConfig.ShowZipcodeColumn = tglShowZipcode.IsOn;
                _projectUserConfig.ShowParentAccountNameColumn = tglShowParentAccountName.IsOn;
                _projectUserConfig.ShowStreet = tgleShowStreet.IsOn;
                _projectUserConfig.ShowRouteId = tglShowRouteId.IsOn;
                _projectUserConfig.ShowPhone = tglShowPhone.IsOn;
                _projectUserConfig.ShowAltPhone = tglShowAltPhone.IsOn;
                _projectUserConfig.ShowBrandingKey = tglBrandingKey.IsOn;
                _projectUserConfig.ShowStartStopTime = tglShowClock.IsOn;

                // Group
                _projectUserConfig.GroupedColumn = _groupList.Where(x => x.IsOn).FirstOrDefault()?.Tag?.ToString();

                // Order
                _projectUserConfig.OrderByColumn = _orderList.Where(x => x.IsOn).FirstOrDefault()?.Tag?.ToString();
                

                if (rdoFontSize.SelectedIndex > -1)
                {
                    _projectUserConfig.FontSize = Convert.ToInt32(rdoFontSize.EditValue?.ToString());
                }
                if (rdoRowHeight.SelectedIndex > -1)
                {
                    _projectUserConfig.RowHeight = Convert.ToInt32(rdoRowHeight.EditValue?.ToString());
                }
                
                SelectedItems.UserClipboardConfig.ProjectUserConfig = _projectUserConfig;
                SelectedItems.UserClipboardConfiguration.SaveToXml();

                if (MainFromUserControls.UcDirectInstall != null && DirectInstallUserControls.UcProjects != null)
                {
                    string diId = SelectedItems.DI?.DIId;

                    DirectInstallUserControls.UcProjects.SetProjectUserConfiguration(_projectUserConfig);
                    DirectInstallUserControls.UcProjects.SetTableData();

                    if (!string.IsNullOrEmpty(diId))
                    {
                        GridHelper.FocusRowByColumnCellValue(DirectInstallUserControls.UcProjects.GridView, ProjectTableColumnConstants.DIId, diId);
                    }
                }


                if (MainFromUserControls.MainForm.PreviousUserControl?.Name == MainFromUserControls.UcDirectInstall?.Name && MainFromUserControls.UcDirectInstall != default(UserControl))
                {
                    MainFromUserControls.MainForm.SetUserControl(MainFromUserControls.UcDirectInstall);
                }
                else if (MainFromUserControls.MainForm.PreviousUserControl?.Name == MainFromUserControls.UcSettings?.Name && MainFromUserControls.UcSettings != default(UserControl))
                {
                    MainFromUserControls.MainForm.SetUserControl(MainFromUserControls.UcSettings);
                }
                else
                {
                    MainFromUserControls.MainForm.SetUserControl(MainFromUserControls.UcHome);
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
            finally
            {
                MainFromUserControls.MainForm.PleaseWait = false;
                this.Dispose();
            }
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            try
            {
                _projectUserConfig = new ProjectUserConfig();
                SetConfiguration();
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }




    }
}
