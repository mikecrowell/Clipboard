using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FieldTool.BLL.ClipboardConfiguration;
using Clipboard.UI.DirectInstall;
using Clipboard.UI._Helper;
using Clipboard.Helper.Home.Settings;

namespace Clipboard.UI.Home.Settings.Tools
{
    public partial class ucSplitGrid : DevExpress.XtraEditors.XtraUserControl
    {
        public ucSplitGrid()
        {
            InitializeComponent();
            this.LookAndFeel.SkinName = MainFromUserControls.MainForm.SkinName;
            this.Dock = DockStyle.Fill;
            tglTextBoxQuestion.Visible = false;

            if (SelectedItems.UserClipboardConfig.SplitGrid == null)
            {
                _splitGrid = new SplitGrid();
            }
            else
            {
                _splitGrid = SelectedItems.UserClipboardConfig.SplitGrid;
            }

            SetConfiguration();
        }

        

        private SplitGrid _splitGrid { get; set; }

        private void SetConfiguration()
        {
            toggleQuestionOnLeft.IsOn = _splitGrid.Swaped;
            toggleHorizontal.IsOn = _splitGrid.Vertical;
            tglSettingsButton.IsOn = _splitGrid.ShowSettingsButton;
            tglTextBoxQuestion.IsOn = _splitGrid.HideQuestionTextBox;
            tglShowOrder.IsOn = _splitGrid.ShowOrder;
            SetSplitterPercentRadioSelected();


            rdoQuestionFontSize.SelectedIndex = UserConfigurationSettingsHelper.SetFontSizeSelected(_splitGrid.QuestionFontSize);
            rdoAnswerFontSize.SelectedIndex = UserConfigurationSettingsHelper.SetFontSizeSelected(_splitGrid.AnswerFontSize);
            rdoQuestionRowHeight.SelectedIndex = UserConfigurationSettingsHelper.SetRowHeightSelected(_splitGrid.QuestionRowHeight);
            rdoAnswerRowHeight.SelectedIndex = UserConfigurationSettingsHelper.SetRowHeightSelected(_splitGrid.AnswerRowHeight);

        
        }


       

        private void SetSplitterPercentRadioSelected()
        {
            switch ((int)_splitGrid.QuestionTableWidthPercent)
            {
                case 30:
                    rdoTableSize.SelectedIndex = 0;
                    break;
                case 40:
                    rdoTableSize.SelectedIndex = 1;
                    break;
                case 50:
                    rdoTableSize.SelectedIndex = 2;
                    break;
                case 60:
                    rdoTableSize.SelectedIndex = 3;
                    break;
                case 70:
                    rdoTableSize.SelectedIndex = 4;
                    break;
                case 80:
                    rdoTableSize.SelectedIndex = 5;
                    break;
                default:
                    rdoTableSize.SelectedIndex = 2;
                    break;
            }
        }






        #region Bottom Buttons

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                MainFromUserControls.MainForm.PleaseWait = true;
                _splitGrid.Swaped = toggleQuestionOnLeft.IsOn;
                _splitGrid.Vertical = toggleHorizontal.IsOn;
                _splitGrid.ShowSettingsButton = tglSettingsButton.IsOn;
                _splitGrid.HideQuestionTextBox = tglTextBoxQuestion.IsOn;
                _splitGrid.ShowOrder = tglShowOrder.IsOn;

                if (rdoTableSize.SelectedIndex > -1)
                {
                    _splitGrid.QuestionTableWidthPercent = Convert.ToDouble(rdoTableSize.EditValue?.ToString());
                }

                if (rdoQuestionFontSize.SelectedIndex > -1)
                {
                    _splitGrid.QuestionFontSize = Convert.ToInt32(rdoQuestionFontSize.EditValue?.ToString());
                }

                if (rdoAnswerFontSize.SelectedIndex > -1)
                {
                    _splitGrid.AnswerFontSize = Convert.ToInt32(rdoAnswerFontSize.EditValue?.ToString());
                }

                if (rdoQuestionRowHeight.SelectedIndex > -1)
                {
                    _splitGrid.QuestionRowHeight = Convert.ToInt32(rdoQuestionRowHeight.EditValue?.ToString());
                }

                if (rdoAnswerRowHeight.SelectedIndex > -1)
                {
                    _splitGrid.AnswerRowHeight = Convert.ToInt32(rdoAnswerRowHeight.EditValue?.ToString());
                }

                SelectedItems.UserClipboardConfig.SplitGrid = _splitGrid;
                SelectedItems.UserClipboardConfiguration.SaveToXml();

                if (MainFromUserControls.UcDirectInstall != null 
                    && DirectInstallUserControls.UcSurvey != null 
                    && DirectInstallUserControls.UcReferrals != null 
                    && DirectInstallUserControls.UcRecommendations != null)
                {
                    DirectInstallUserControls.UcSurvey.SetConfiguration(_splitGrid);
                    DirectInstallUserControls.UcReferrals.SetConfiguration(_splitGrid);
                    DirectInstallUserControls.UcRecommendations.SetConfiguration(_splitGrid);
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
                _splitGrid = new SplitGrid();
                SetConfiguration();
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }

        #endregion


    }
}
