using Clipboard.Helper.Bsi;
using Clipboard.Helper.Configuration;
using Clipboard.UI._Configuration;
using Clipboard.UI.DirectInstall;
using DevExpress.LookAndFeel;
using DevExpress.XtraSpellChecker;
using FieldTool.BLL;
using FieldTool.BLL.Utilities;
using FieldTool.Bsi.Models;
using FieldTool.Constants.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clipboard.UI
{
    public static class MainFormHelper
    {


        public static void CreateBrandingFolder()
        {
            if (!Directory.Exists(ConfigurationHelper.BrandingFolderPath))
            {
                Directory.CreateDirectory(ConfigurationHelper.BrandingFolderPath);
            }
        }

        public static int GetBrandingDirectoryCount()
        {
            CreateBrandingFolder();
            int directoryCount = System.IO.Directory.GetDirectories(ConfigurationHelper.BrandingFolderPath).Length;
            return directoryCount;
        }

        public static void SkinApplication()
        {
            Lg.Info("SkinApplication()");
            if (MainFromUserControls.UcDirectInstall != default(ucDirectInstall))
            {
                MainFromUserControls.UcDirectInstall.SetSkin();
            }
            MainFromUserControls.UcHome.SetSkin();
            MainFromUserControls.UcLogIn.SetSkin();
            MainFromUserControls.UcDownload.SetSkin();
            MainFromUserControls.UcError.SetSkin();
            MainFromUserControls.UcSettings.SetSkin();
            LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
        }

        public static void SetClipboardConfiguration()
        {
            Lg.Info("SetClipboardConfiguration()");
            var programs = DataStore.GetPrograms();

            if (
                MainFromUserControls.UcDirectInstall != null
                && DirectInstallUserControls.UcProjects != null
                && SelectedItems.Program != default(ApiProgramMetadata)
                && SelectedItems.DI != default(DI)
                && File.Exists(ConfigurationHelper.ApiBrandingConfigurationFilePath)
                )
            {
                MainFromUserControls.MainForm.ClipboardConfiguration = new ClipboardConfiguration(SelectedItems.Program.program.ClientName, SelectedItems.Program.program.ProgramCode, SelectedItems.DI);
                
            }
            else if (programs.Count > 0 && File.Exists(ConfigurationHelper.ApiBrandingConfigurationFilePath))
            {
                var program = programs.OrderByDescending(x => x.program.ProgramCode).ToList().FirstOrDefault();
                MainFromUserControls.MainForm.ClipboardConfiguration = new ClipboardConfiguration(program.program.ClientName, program.program.ProgramCode, SelectedItems.DI);
                
            }

            if (MainFromUserControls.UcHome != null)
            {
                if (!string.IsNullOrEmpty(MainFromUserControls.MainForm.ApiUser?.Email))
                {
                    MainFromUserControls.UcHome.LogOffEnabled = false;
                }
                else
                {
                    MainFromUserControls.UcHome.LogOffEnabled = true;
                }
            }

            string skinName = SelectedItems.UserClipboardConfig.Theme?.SkinName;
            if (!string.IsNullOrEmpty(skinName) && MainFromUserControls.MainForm.SkinName != skinName)
            {
                MainFromUserControls.MainForm.SkinName = SelectedItems.UserClipboardConfig.Theme.SkinName;
            }
        }

        

        public static void SetUserClipboardConfiguration()
        {
            MainFromUserControls.MainForm.UserClipboardConfiguration = new UserClipboardConfiguration();
        }

        public static void TogglePleaseWait(bool show)
        {
            if (show)
            {
                MainFromUserControls.MainForm.Enabled = false;
                Application.DoEvents();
                Cursor.Current = Cursors.WaitCursor;
                MainFromUserControls.UcPleaseWait.Visible = true;
                MainFromUserControls.UcPleaseWait.BringToFront();
                MainFromUserControls.UcPleaseWait.CenterUc();
                Application.DoEvents();
                Cursor.Current = Cursors.WaitCursor;
            }
            else
            {
                Cursor.Current = Cursors.Default;
                MainFromUserControls.UcPleaseWait.Visible = false;
                MainFromUserControls.UcPleaseWait.SendToBack();
                MainFromUserControls.MainForm.Enabled = true;
            }
        }

        public static bool AutoDownloadPrograms
        {
            get
            {
                var programList = DataStore.GetPrograms();
                int brandingDirectoryCount = MainFormHelper.GetBrandingDirectoryCount();


                try
                {
                    // This section if for QA to skip the auto download in UAT
                    if (SelectedItems.UserClipboardConfig.QAUserConfig.SkipProgramDownload)
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                }

                if (
                    programList.Count == 0 
                    || !MainFromUserControls.MainForm.IsDebug 
                    || brandingDirectoryCount <= 1 
                    || !File.Exists(DataStore.XmlMetaDataFile) 
                    || !File.Exists(ConfigurationHelper.ApiBrandingConfigurationFilePath)
                    )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static ApiUser GetApiUser(bool deleteFile = false)
        {
            Lg.Info("GetApiUser()");
            ApiUser apiUser = new ApiUser();

            if (deleteFile && File.Exists(DataStore.JsonApiUserFile))
            {
                File.Delete(DataStore.JsonApiUserFile);
            }


            if (File.Exists(DataStore.JsonApiUserFile))
            {
                Lg.Info("ApiUser.json file Exists");
                apiUser = ApiUserHelper.ReadApiUser();
            }
            else
            {
                if (FieldTool.Constants.Utilities.IsNetworkAvailable())
                {
                    var downloadHelper = new DownloadHelper(ConfigurationManager.AppSettings);
                    downloadHelper.bsiService = MainFromUserControls.MainForm.BsiService;
                    apiUser = downloadHelper.GetApiUser(SelectedItems.User.Email, "");
                    ApiUserHelper.WriteApiUserToXml(apiUser);
                }
            }

            if (string.IsNullOrEmpty(apiUser?.Email))
            {
                MainFromUserControls.MainForm.IsTaEnv = true;
            }

            return apiUser;
        }

        public static void DecideWhichPageToDisplayFirst()
        {
            Lg.Info("DecideWhichPageToDisplayFirst()");
            if (MainFromUserControls.MainForm.IsTaEnv)
            {
                MainFromUserControls.MainForm.SetUserControl(MainFromUserControls.UcLogIn);
            }
            else
            {
                if (MainFormHelper.AutoDownloadPrograms )
                {
                    MainFromUserControls.MainForm.SetUserControl(MainFromUserControls.UcDownload);
                    MainFromUserControls.MainForm.RunDownloadAfterShow = true;
                    //MainFromUserControls.UcDownload.BeginDownload();
                }
                else
                {
                    MainFromUserControls.MainForm.SetUserControl(MainFromUserControls.UcHome);
                }
            }
        }


        public static void SetSpellChecker(SpellChecker spellChecker1)
        {
            SpellCheckerOpenOfficeDictionary dic_en_US = new SpellCheckerOpenOfficeDictionary();
            dic_en_US.DictionaryPath = System.Environment.CurrentDirectory + @"\dat\Dictionary\american.xlg";
            dic_en_US.GrammarPath = System.Environment.CurrentDirectory + @"\dat\Dictionary\en_US.aff"; ;
            dic_en_US.AlphabetPath = System.Environment.CurrentDirectory + @"\dat\Dictionary\en_US.dic"; ;
            dic_en_US.Culture = new CultureInfo("en-US");
            spellChecker1.Dictionaries.Clear();
            spellChecker1.Dictionaries.Add(dic_en_US);
        }


    }
}
