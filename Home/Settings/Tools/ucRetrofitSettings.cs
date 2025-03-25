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
using FieldTool.Constants.DirectInstall.Retrofits;

namespace Clipboard.UI.Home.Settings.Tools
{
    public partial class ucRetrofitSettings : DevExpress.XtraEditors.XtraUserControl
    {
        public RetrofitUserConfig _retrofitUserConfig { get; set; }

        public List<ToggleSwitch> _orderList = new List<ToggleSwitch>();

        public ucRetrofitSettings()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.LookAndFeel.SkinName = MainFromUserControls.MainForm.SkinName;

            btnDefault.Click += btnDefault_Click;

            InitGroupAndOrder();

            if (SelectedItems.UserClipboardConfig.RetrofitUserConfig == null)
            {
                _retrofitUserConfig = new RetrofitUserConfig();
            }
            else
            {
                _retrofitUserConfig = SelectedItems.UserClipboardConfig.RetrofitUserConfig;
            }

            SetConfiguration();
        }


        private void InitGroupAndOrder()
        {
            // Order
            tglOrder_Order.Tag = RetrofitTableColumnConstants.Order;
            _orderList.Add(tglOrder_Order);
            tglOrder_Order.Click += OrderColumn_Click;
        }

        private void SetConfiguration()
        {
            tglDetails.IsOn = _retrofitUserConfig.ShowDetailColumn;
            tglDescription.IsOn = _retrofitUserConfig.ShowDescription;
            tglComponentName.IsOn = _retrofitUserConfig.ShowComponentName;
            tglNonTechName.IsOn = _retrofitUserConfig.ShowNonTechName;
            tglSpace.IsOn = _retrofitUserConfig.ShowSpace;
            tglCopayAmount.IsOn = _retrofitUserConfig.ShowCopayAmount;
            tglCopayTotal.IsOn = _retrofitUserConfig.ShowCopayTotal;
            tglCopy.IsOn = _retrofitUserConfig.ShowCopy;
            tglOrder.IsOn = _retrofitUserConfig.ShowOrder;
            tglFuelType.IsOn = _retrofitUserConfig.ShowFuelType;


            chkLineItem1.Checked = _retrofitUserConfig.ShowLineItem1;
            chkLineItem2.Checked = _retrofitUserConfig.ShowLineItem2;
            chkLineItem3.Checked = _retrofitUserConfig.ShowLineItem3;
            chkLineItem4.Checked = _retrofitUserConfig.ShowLineItem4;
            chkLineItem5.Checked = _retrofitUserConfig.ShowLineItem5;
            chkLineItem6.Checked = _retrofitUserConfig.ShowLineItem6;
            chkLineItem7.Checked = _retrofitUserConfig.ShowLineItem7;
            chkLineItem8.Checked = _retrofitUserConfig.ShowLineItem8;
            chkLineItem9.Checked = _retrofitUserConfig.ShowLineItem9;
            chkLineItem10.Checked = _retrofitUserConfig.ShowLineItem10;
            chkLineItem11.Checked = _retrofitUserConfig.ShowLineItem11;
            chkLineItem12.Checked = _retrofitUserConfig.ShowLineItem12;
            chkLineItem13.Checked = _retrofitUserConfig.ShowLineItem13;
            chkLineItem14.Checked = _retrofitUserConfig.ShowLineItem14;
            chkLineItem15.Checked = _retrofitUserConfig.ShowLineItem15;
            chkLineItem16.Checked = _retrofitUserConfig.ShowLineItem16;
            chkLineItem17.Checked = _retrofitUserConfig.ShowLineItem17;
            chkLineItem18.Checked = _retrofitUserConfig.ShowLineItem18;
            chkLineItem19.Checked = _retrofitUserConfig.ShowLineItem19;
            chkLineItem20.Checked = _retrofitUserConfig.ShowLineItem20;



            

            rdoRowHeight.SelectedIndex = UserConfigurationSettingsHelper.SetRowHeightSelected(_retrofitUserConfig.RowHeight);
            rdoFontSize.SelectedIndex = UserConfigurationSettingsHelper.SetFontSizeSelected(_retrofitUserConfig.FontSize);

            // Order
            ToggleSwitch orderTgl = _orderList.Where(x => x.Tag?.ToString() == _retrofitUserConfig.OrderByColumn).FirstOrDefault();
            if (orderTgl != null)
            {
                orderTgl.IsOn = true;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                MainFromUserControls.MainForm.PleaseWait = true;
                _retrofitUserConfig.ShowDetailColumn = tglDetails.IsOn;
                _retrofitUserConfig.ShowDescription = tglDescription.IsOn;
                _retrofitUserConfig.ShowComponentName = tglComponentName.IsOn;
                _retrofitUserConfig.ShowNonTechName = tglNonTechName.IsOn;
                _retrofitUserConfig.ShowSpace = tglSpace.IsOn;
                _retrofitUserConfig.ShowLineItem1 = chkLineItem1.Checked;
                _retrofitUserConfig.ShowLineItem2 = chkLineItem2.Checked;
                _retrofitUserConfig.ShowLineItem3 = chkLineItem3.Checked;
                _retrofitUserConfig.ShowLineItem4 = chkLineItem4.Checked;
                _retrofitUserConfig.ShowLineItem5 = chkLineItem5.Checked;
                _retrofitUserConfig.ShowLineItem6 = chkLineItem6.Checked;
                _retrofitUserConfig.ShowLineItem7 = chkLineItem7.Checked;
                _retrofitUserConfig.ShowLineItem8 = chkLineItem8.Checked;
                _retrofitUserConfig.ShowLineItem9 = chkLineItem9.Checked;
                _retrofitUserConfig.ShowLineItem10 = chkLineItem10.Checked;
                _retrofitUserConfig.ShowLineItem11 = chkLineItem11.Checked;
                _retrofitUserConfig.ShowLineItem12 = chkLineItem12.Checked;
                _retrofitUserConfig.ShowLineItem13 = chkLineItem13.Checked;
                _retrofitUserConfig.ShowLineItem14 = chkLineItem14.Checked;
                _retrofitUserConfig.ShowLineItem15 = chkLineItem15.Checked;
                _retrofitUserConfig.ShowLineItem16 = chkLineItem16.Checked;
                _retrofitUserConfig.ShowLineItem17 = chkLineItem17.Checked;
                _retrofitUserConfig.ShowLineItem18 = chkLineItem18.Checked;
                _retrofitUserConfig.ShowLineItem19 = chkLineItem19.Checked;
                _retrofitUserConfig.ShowLineItem20 = chkLineItem20.Checked;

                _retrofitUserConfig.ShowCopayAmount = tglCopayAmount.IsOn;
                _retrofitUserConfig.ShowCopayTotal = tglCopayTotal.IsOn;
                _retrofitUserConfig.ShowCopy = tglCopy.IsOn;
                _retrofitUserConfig.ShowOrder = tglOrder.IsOn;
                _retrofitUserConfig.ShowFuelType = tglFuelType.IsOn;

                if (rdoFontSize.SelectedIndex > -1)
                {
                    _retrofitUserConfig.FontSize = Convert.ToInt32(rdoFontSize.EditValue?.ToString());
                }
                if (rdoRowHeight.SelectedIndex > -1)
                {
                    _retrofitUserConfig.RowHeight = Convert.ToInt32(rdoRowHeight.EditValue?.ToString());
                }

                SelectedItems.UserClipboardConfig.RetrofitUserConfig = _retrofitUserConfig;
                SelectedItems.UserClipboardConfiguration.SaveToXml();

                if (MainFromUserControls.UcDirectInstall != null && DirectInstallUserControls.UcProjects != null)
                {
                    DirectInstallUserControls.UcRetrofits.SetUserConfiguration(_retrofitUserConfig);
                }

                MainFromUserControls.MainForm.SetUserControl(MainFromUserControls.MainForm.PreviousUserControl);
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
                _retrofitUserConfig = new RetrofitUserConfig();
                SetConfiguration();
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }





    }
}
