using Clipboard.UI._Configuration;
using Clipboard.UI._Helper;
using Clipboard.UI.DirectInstall;
using Clipboard.UI.DirectInstall.Projects;
using Clipboard.UI.DirectInstall.Settings;
using Clipboard.UI.Error;
using Clipboard.UI.Home;
using Clipboard.UI.Home.Download;
using Clipboard.UI.Home.Help;
using Clipboard.UI.LogIn;
using Clipboard.UI.Menu;
using Clipboard.UI.Theme;
using Clipboard.Helper;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using FieldTool.BLL;
using FieldTool.Bsi.Helpers;
using FieldTool.Constants;
using FieldTool.Constants.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FieldTool.Constants.Logging;
using FieldTool.Bsi.Models;
using Clipboard.UI.PleaseWait;
using System.Threading;
using FieldTool.BLL.Utilities;
using Clipboard.Helper.Bsi;
using System.IO;
using DevExpress.XtraSpellChecker;
using System.Globalization;
using Clipboard.UI._Shared.CustomMessageBox;

namespace Clipboard.UI
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        public MainForm()
        {
            try
            {
                InitializeComponent();
                MainFromUserControls.MainForm = this;

                // Events
                this.Shown += MainForm_Shown;
                this.ResizeEnd += MainForm_ResizeEnd;
                this.FormClosing += MainForm_FormClosing;

                // Title
                _originalTitle = this.Text + ApplicationHelper.GetVersion();
                this.Title = "";

                // Spell Checker
                MainFormHelper.SetSpellChecker(spellChecker1);

                // Init
                MainFormHelper.CreateBrandingFolder();
                UserSettingsHelper.LoadUserSettings(false);
                MainFormHelper.GetApiUser();
                CreateAndLoadUserControls();

               

                if (!IsDebug)
                {
                    this.WindowState = FormWindowState.Maximized;
                }
            }
            catch (Exception e)
            {
               ErrorHelper.PresentError(e);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (e.CloseReason == CloseReason.UserClosing && !IsDebug)
                {
                    DialogResult dialogResult = MessageBox.Show("Do you really want to exit Efficiency Clipboard?", "Exit", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        // YES
                        DataStore.SaveData();
                        Program.IsOnlyApplicationRunning.Dispose();
                    }
                    else
                    {
                        // NO
                        e.Cancel = true;
                    }
                }

                if (e.Cancel == false)
                {
                    Program.IsOnlyApplicationRunning.Dispose();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    

        #region Properties

        public string Enviorment
        {
            get
            {
                return ConfigurationManager.AppSettings[AppConstants.AppKeys.ENV];
            }
        }

        
        public  bool IsRelease
        {
            get
            {
                return (Enviorment == AppConstants.Enviorment.RELEASE);
            }
        }

        public bool IsDebug
        {
            get
            {
                return (Enviorment == AppConstants.Enviorment.DEBUG);
            }
        }

        private IBsiService _bsiService = default(IBsiService);
        public IBsiService BsiService
        {
            get
            {
                if (this._bsiService == default(IBsiService))
                {
                    this._bsiService = new BsiService(ConfigurationManager.AppSettings);
                }
                return this._bsiService;
            }
            set
            {
                this._bsiService = value;
            }
        }

        public bool IsTaEnv = false;
        private ApiUser _apiUser { get; set; }
        public ApiUser ApiUser
        {
            get
            {
                if (_apiUser == default(ApiUser))
                {
                    _apiUser = MainFormHelper.GetApiUser();
                }
                return _apiUser;
            }
        }

        private UserSettings _user = default(UserSettings);
        public UserSettings User
        {
            get
            {
                if (_user == default(UserSettings))
                {
                    _user = UserSettings.GetUserSettings();
                }
                return _user;
            }
        }

        public string SkinName
        {
            get
            {
                return this.LookAndFeel.SkinName;
            }
            set
            {
                this.LookAndFeel.SkinName = value;
                MainFormHelper.SkinApplication();
            }
        }

        public UserControl PreviousUserControl { get; set; }

        public ClipboardConfiguration ClipboardConfiguration { get; set; }
        public UserClipboardConfiguration UserClipboardConfiguration { get; set; }


        private string _originalTitle { get; set; }
        public string Title
        {
            get
            {
                return this.Text;
            }set
            {
                if (SelectedItems.DIandProgramBothNotNull)
                {
                    this.Text = $"{_originalTitle} - {SelectedItems.DI?.Accounts?[0]?.EnergyTypeClassLabel} - {value}";
                }
                else
                {
                    this.Text = $"{_originalTitle} - {value}";
                }
            }
        }

        public bool PleaseWait
        {
            set
            {
                if (value)
                {

                    MainFormHelper.TogglePleaseWait(show:true);

                }
                else
                {
                    
                    MainFormHelper.TogglePleaseWait(show: false);
                }
            }
        }

        public bool CurrentControlEnabled
        {
            set
            {
                var uc = Controls.OfType<UserControl>().Where(x => x.Tag?.ToString() != "PleaseWait" &&  x.Name != "ucCustomMessageBox").FirstOrDefault();
                if (uc != null)
                {
                    uc.Enabled = value;
                }
            }
        }

        public bool DownloadInProgress { get; internal set; }
        public bool RunDownloadAfterShow = false;
        public bool HomeHasBeenEntered = false;
        #endregion


        #region Init

        private void CreateAndLoadUserControls()
        {
            try
            {
                Lg.Title("CreateAndLoadUserControls()");

                // Create
                Lg.Info("Newing Up Controls");
                MainFromUserControls.MainForm = this;
                MainFromUserControls.UcHome = new ucHome();
                MainFromUserControls.UcLogIn = new ucLogIn();
                MainFromUserControls.UcDownload = new ucDownload();
                MainFromUserControls.UcHelp = new ucHelp();
                MainFromUserControls.UcError = new ucError();
                MainFromUserControls.UcSettings = new Clipboard.UI.Home.Settings.ucSettings();
                MainFromUserControls.UcPleaseWait = new ucPleaseWait("");

                // Init
                Lg.Info("Initializing Ucs");
                MainFromUserControls.UcLogIn.Init();
                MainFromUserControls.UcDownload.Init(BsiService, User.Email);
                MainFromUserControls.UcHelp.Init();

                // Theme
                SkinName = this.LookAndFeel.SkinName;

                // Display First Page
                MainFormHelper.DecideWhichPageToDisplayFirst();
            }
            catch (Exception e)
            {
                ErrorHelper.PresentError(e);
            }
        }
        

        
        

        #endregion


        #region Set UserControl

        public void SetUserControl(UserControl userControl)
        {
            try
            {
                Lg.Info($"SetUserControl: {userControl?.Name}");

               

                PreviousUserControl = Controls.OfType<UserControl>().Where(x => x.Tag?.ToString() != "PleaseWait").FirstOrDefault();
                OnEnteringUserControl(userControl);
                this.Controls.Clear();
                MainFromUserControls.UcPleaseWait.Visible = false;
                this.Controls.Add(MainFromUserControls.UcPleaseWait);

                if (userControl != default(UserControl) && !userControl.IsDisposed)
                {
                    userControl.AutoScaleMode = AutoScaleMode.None;
                    this.Controls.Add(userControl);
                }
                else
                {
                    // This is here because we don't want to allow TA's to by pass log in just because they got an error.
                    // Also if for some reason where they are trying to go is null we dont want to display and empty page
                    if (HomeHasBeenEntered || !IsTaEnv && MainFromUserControls.UcHome != default(UserControl))
                    {
                        this.Controls.Add(MainFromUserControls.UcHome);
                    }
                    else
                    {
                        // This would display an empty page
                        this.Controls.Add(userControl);
                    }
                }
            }
            catch (Exception e)
            {
                ErrorHelper.PresentError(e);
            }
        }


        private void OnEnteringUserControl(UserControl uc)
        {
            if (uc != default(UserControl))
            {
                if (uc.Name == "ucHome")
                {
                    HomeHasBeenEntered = true;
                }
            }
        }


        public void CustomMessageBox(string title, string msg, int height = 0)
        {
            try
            {
                


                var uc = new ucCustomMessageBox(this.Height, title, msg, height);
                uc.Location = new Point((this.Width - uc.Width) / 2, 10);
                this.Controls.Add(uc);
                uc.BringToFront();
                CurrentControlEnabled = false;
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }

        public void CustomMessageBoxDispose(UserControl uc)
        {
            uc.Dispose();
            CurrentControlEnabled = true;
        }

        #endregion


        


        #region Events

        private void MainForm_Shown(object sender, EventArgs e)
        {

            try
            {
                DataStore.LoadInstance();
            }
            catch (Exception)
            {
                MainFromUserControls.MainForm.SetUserControl(new ucDataFileRecovery());
                return;
            }

            try
            {
                MainFromUserControls.UcLogIn.CenterControls();

                if (RunDownloadAfterShow)
                {
                    DownloadInProgress = true;
                    MainFromUserControls.UcDownload.BeginDownload(autoTransferToNextPage: true);
                }

                if (SelectedItems.ClipboardConfig == null && !DownloadInProgress)
                {
                    Lg.Info("1 ---> MainFormHelper.SetClipboardConfiguration();");
                    MainFormHelper.SetClipboardConfiguration();
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            try
            {
                if (MainFromUserControls.UcLogIn != default(ucLogIn))
                {
                    MainFromUserControls.UcLogIn.CenterControls();
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }

        #endregion






    }
}
