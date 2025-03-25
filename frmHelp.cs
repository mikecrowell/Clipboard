using FieldTool.BLL;
using FieldTool.BLL.BusinessObjects;
using FieldTool.Constants.Config;
using FieldTool.Constants.Helpers;
using FieldTool.Constants.Logging;
using FieldTool.Constants.Paths;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Deployment.Application;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace FieldTool.UI
{
    public partial class frmHelp : DevComponents.DotNetBar.Metro.MetroForm
    {
        #region Inner classes and structs

        private class SendResult
        {
            #region Private member variables

            private string _caption;
            private string _message;

            #endregion Private member variables

            #region Constructors

            internal SendResult()
            {
                this.Message = "";
                this.Caption = "Efficiency Clipboard";
                this.MessageBoxButtons = MessageBoxButtons.OK;
                this.MessageBoxIcon = MessageBoxIcon.Information;
                this.IsError = false;
            }

            internal SendResult(string message, string caption, MessageBoxButtons messageBoxButtons, MessageBoxIcon messageBoxIcon, bool isError)
            {
                this.Message = message;
                this.Caption = caption;
                this.MessageBoxButtons = messageBoxButtons;
                this.MessageBoxIcon = messageBoxIcon;
                this.IsError = isError;
            }

            #endregion Constructors

            #region Properties

            internal string Caption
            {
                get
                {
                    return this._caption;
                }
                set
                {
                    this._caption = (value ?? "").Trim();
                }
            }

            internal bool HasResult
            {
                get
                {
                    if (String.IsNullOrWhiteSpace(this.Message))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            internal bool IsError { get; set; }

            internal string Message
            {
                get
                {
                    return this._message;
                }
                set
                {
                    this._message = (value ?? "").Trim();
                }
            }

            internal MessageBoxButtons MessageBoxButtons { get; set; }

            internal MessageBoxIcon MessageBoxIcon { get; set; }

            #endregion Properties

            #region Exposed methods

            internal void ShowDialog()
            {
                MessageBox.Show(this.Message, this.Caption, this.MessageBoxButtons, this.MessageBoxIcon);
            }

            #endregion Exposed methods
        }

        private class FilePaths
        {
            private List<string> _errors = null;

            public FilePaths(string zipFileName)
            {
                this.AppConfigFile = DataStore.AppConfigFile;
                this.CustomRecommendationsFile = DataStore.XmlCustomRecommendationsFile;
                this.DataFile = DataStore.XmlDataFile;
                this.DebugInfoFile = this.CreateDebugInfoFile();
                this.DiReportFile = DataStore.XmlDIReportFile;
                this.ProgramsFile = DataStore.XmlProgramsFile;
                this.ReportFile = DataStore.XmlReportFile;
                this.SettingsFile = UserSettings.SettingsFilePath;
                this.ZipFile = Path.Combine(Path.GetDirectoryName(DataStore.XmlDataDirectory), zipFileName);
            }

            private string _appConfigFile = "";

            public string AppConfigFile
            {
                get
                {
                    return this._appConfigFile;
                }
                private set
                {
                    this._appConfigFile = Constants.Utilities.ScrubConfigFile(value);
                }
            }

            public string CustomRecommendationsFile { get; private set; }
            public string DataFile { get; private set; }
            public string DebugInfoFile { get; private set; }
            public string DiReportFile { get; private set; }
            public string ProgramsFile { get; private set; }
            public string ReportFile { get; private set; }
            public string SettingsFile { get; private set; }
            public string ZipFile { get; private set; }

            private List<string> Errors
            {
                get
                {
                    if (this._errors == null)
                    {
                        this._errors = new List<string>();
                    }
                    return _errors;
                }
            }

            //public bool IsValid {
            //    get {
            //        Lg.Info("Entering IsValid");

            //        bool result = true;

            //        this.ClearErrors();

            //        if (!File.Exists(this.AppConfigFile)) {
            //            Lg.Info("The application configuration file does not exist: " + this.AppConfigFile);
            //            this.Errors.Add("The application configuration file does not exist: " + this.AppConfigFile);
            //            result = false;
            //        }

            //        if (!File.Exists(this.CustomRecommendationsFile)) {
            //            Lg.Info("The custom recommendations file does not exist: " + this.CustomRecommendationsFile);
            //            this.Errors.Add("The custom recommendations file does not exist: " + this.CustomRecommendationsFile);
            //            result = false;
            //        }

            //        if (!File.Exists(this.DataFile)) {
            //            Lg.Info("The data file does not exist: " + this.DataFile);
            //            this.Errors.Add("The data file does not exist: " + this.DataFile);
            //            result = false;
            //        }

            //        if (!File.Exists(this.DebugInfoFile)) {
            //            Lg.Info("The debug information file does not exist: " + this.DebugInfoFile);
            //            this.Errors.Add("The debug information file does not exist: " + this.DebugInfoFile);
            //            result = false;
            //        }

            //        if (!File.Exists(this.DiReportFile)) {
            //            Lg.Info("The DI report file does not exist: " + this.DiReportFile);
            //            this.Errors.Add("The DI report file does not exist: " + this.DiReportFile);
            //            result = false;
            //        }

            //        if (!File.Exists(this.ProgramsFile)) {
            //            Lg.Info("The programs file does not exist: " + this.ProgramsFile);
            //            this.Errors.Add("The programs file does not exist: " + this.ProgramsFile);
            //            result = false;
            //        }

            //        if (!File.Exists(this.ReportFile)) {
            //            Lg.Info("The report file does not exist: " + this.ReportFile);
            //            this.Errors.Add("The report file does not exist: " + this.ReportFile);
            //            result = false;
            //        }

            //        if (!File.Exists(this.SettingsFile)) {
            //            Lg.Info("The settings file file does not exist: " + this.SettingsFile);
            //            this.Errors.Add("The settings file does not exist: " + this.SettingsFile);
            //            result = false;
            //        }

            //        return result;
            //    }
            //}

            private string GetFileInfo(string filePath)
            {
                string result = "";
                FileInfo fi = new FileInfo(filePath);

                if (!fi.Exists)
                {
                    result = fi.Name + "\n\tFILE NOT FOUND!\n\texpected file location: " + filePath;
                }
                else
                {
                    long kbSize = fi.Length / 1024;
                    result = fi.Name + "\n\tfile location: " + fi.DirectoryName + "\n\tappx. size: " + kbSize + "KB (" + String.Format("{0:n0}", fi.Length) + " bytes)\n\tlast modified: " + fi.LastWriteTime.ToString();
                }

                return result;
            }

            private string CreateDebugInfoFile()
            {
                string result = DataStore.XmlDebugInfoFile;
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("Install directory: " + Constants.Utilities.GetInstallPath());
                sb.AppendLine("Data directory: " + DataStore.XmlDataDirectory);
                sb.AppendLine();
                sb.AppendLine("Meta data files");
                sb.AppendLine("---------------");
                sb.AppendLine();
                sb.AppendLine("Meta data files directory: " + Path.Combine(Constants.Utilities.GetInstallPath(), "dat"));
                sb.AppendLine();
                sb.AppendLine(this.GetFileInfo(DataStore.XmlBuildingTypeSpaces));
                sb.AppendLine(this.GetFileInfo(DataStore.XmlBuildingDetailsFile));
                sb.AppendLine(this.GetFileInfo(DataStore.XmlMetaDataFile));
                sb.AppendLine(this.GetFileInfo(DataStore.XmlRebatesFile));
                sb.AppendLine(this.GetFileInfo(DataStore.XmlRecommendationLibraryComponentFile));
                sb.AppendLine(this.GetFileInfo(DataStore.XmlRecommendationLibraryParameterFile));
                sb.AppendLine(this.GetFileInfo(DataStore.XmlRecommendationEngineReplacementAdapterFile));
                sb.AppendLine(this.GetFileInfo(DataStore.XmlRecommendationEngineParametersFile));
                sb.AppendLine(this.GetFileInfo(DataStore.XmlReportDiIconsFile));
                sb.AppendLine(this.GetFileInfo(DataStore.XmlRetrofitFiltersDataFile));
                sb.AppendLine(this.GetFileInfo(DataStore.XmlZipZoneFile));
                //sb.AppendLine();
                //sb.AppendLine("Image files");
                //sb.AppendLine("-----------");
                //sb.AppendLine();
                //sb.AppendLine(this.GetFileInfo(DataStore.DiIconsDirectoryPath))
                //sb.AppendLine();

                // Creates a new file each time.
                using (StreamWriter f = new StreamWriter(result, false))
                {
                    f.Write(sb.ToString());
                }

                return result;
            }

            public void ClearErrors()
            {
                Lg.Info("ClearErrors() called");
                this.Errors.Clear();
            }
        }

        #endregion Inner classes and structs

        #region Enumerations and constants

        private const string EFFICIENCY_CLIPBOARD_NETWORK_HELP_FOLDER = "\\\\newberlin01\\newberlin\\EfficiencyClipboardTroubleshoot";
        private const bool DELETE_ZIP_FILE_AFTER_SEND = true;
        private const string DATA_BACKUP_FOLDER = "backup";
        private const string DATA_BACKUP_FILE_FORMAT = "data-*-*.xml";
        private const int NUMBER_OF_LOG_FILES_TO_SEND = 2;

        #endregion Enumerations and constants

        #region Private member variables

        private bool _isLoading = false;
        private frmMain _parent { get; set; }
        #endregion Private member variables

        #region Constructors

        public frmHelp(frmMain parent)
        {
            Lg.Title("frmHelp Init()");
            Lg.Info("User has entered the Help area.");
            InitializeComponent();
            _parent = parent;
        }

        #endregion Constructors

        #region Properties

        private RestoreObject SelectedRestoreBackup
        {
            get
            {
                RestoreObject result = null;
                if (this.lstRestoreList.SelectedItems != null && this.lstRestoreList.SelectedItems.Count > 0)
                {
                    ListViewItem li = this.lstRestoreList.SelectedItems[0];

                    result = li.Tag as RestoreObject;
                }
                return result;
            }
        }

        private string XmlFileNameToSend
        {
            get
            {
                return String.Format("{0}_{1}{2}",
                    Path.GetFileNameWithoutExtension(DataStore.XmlDataFile),
                    DataStore.UserName,
                    Path.GetExtension(DataStore.XmlDataFile));
            }
        }

        private string ZipFileName
        {
            get
            {
                return String.Format("{0}_{1}.zip",
                    Path.GetFileNameWithoutExtension(DataStore.XmlDataFile),
                    DataStore.UserName);
            }
        }

        #endregion Properties

        #region Events

        private void btnDebugClear_Click(object sender, EventArgs e)
        {
            if (!this._isLoading)
            {
                Cursor.Current = Cursors.WaitCursor;

                this.txtDebugData.Clear();

                Cursor.Current = Cursors.Default;
            }
        }

        private void btnDebugLoad_Click(object sender, EventArgs e)
        {
            if (!this._isLoading)
            {
                Cursor.Current = Cursors.WaitCursor;

                this.LoadDebugInfo();

                Cursor.Current = Cursors.Default;
            }
        }

        private void btnRestoreSelect_Click(object sender, EventArgs e)
        {
            if (!this._isLoading)
            {
                Cursor.Current = Cursors.WaitCursor;

                this.RestoreBackup();

                Cursor.Current = Cursors.Default;
            }
        }

        private void btnSendDataFilePaths_Click(object sender, EventArgs e)
        {
            string s = DataStore.XmlDataDirectory;
            Constants.Utilities.SetClipboardText(s);

            try
            {
                System.Diagnostics.Process.Start("explorer.exe", s);
            }
            catch (Exception ex)
            {
                var cause = ExceptionHelper.Innermost(ex);
                Lg.Error(cause.Message);
                Lg.Error(cause.StackTrace.ToString());
            }
        }

        private void btnSendDataInstallFolder_Click(object sender, EventArgs e)
        {
            string s = this.GetInstallPath();

            Constants.Utilities.SetClipboardText(s);

            //MessageBox.Show(s, "Installation Directory", MessageBoxButtons.OK, MessageBoxIcon.Information);
            try
            {
                System.Diagnostics.Process.Start("explorer.exe", s);
            }
            catch (Exception ex)
            {
                var cause = ExceptionHelper.Innermost(ex);
                Lg.Error(cause.Message);
                Lg.Error(cause.StackTrace.ToString());
            }
        }

        private void btnSendDataRestoreDataBackup_Click(object sender, EventArgs e)
        {
            if (!this._isLoading)
            {
                Cursor.Current = Cursors.WaitCursor;

                this.RestoreBackup();

                Cursor.Current = Cursors.Default;
            }
        }

        private void btnSendDataSend_Click(object sender, EventArgs e)
        {
            if (!this._isLoading)
            {
                Cursor.Current = Cursors.WaitCursor;

                this.SetDataSendFile(DELETE_ZIP_FILE_AFTER_SEND);

                Cursor.Current = Cursors.Default;
            }
        }

        private void btnViewDocument_Click(object sender, EventArgs e)
        {
            if (File.Exists(cboDocuments.SelectedValue.ToString()))
            {
                System.Diagnostics.Process.Start(cboDocuments.SelectedValue.ToString());
            }
        }

        private void cboRestoreUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this._isLoading)
            {
                Cursor.Current = Cursors.WaitCursor;

                this.LoadRestoreItems();
                this.SetRestoreNote();

                Cursor.Current = Cursors.Default;
            }
        }

        private void cboSendDataUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this._isLoading)
            {
                Cursor.Current = Cursors.WaitCursor;

                this.SetSendDataNote();

                Cursor.Current = Cursors.Default;
            }
        }

        private void cboSendDataVia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this._isLoading)
            {
                Cursor.Current = Cursors.WaitCursor;

                if (this.cboSendDataVia.SelectedItem != null)
                {
                    switch (this.cboSendDataVia.SelectedItem.ToString())
                    {
                        case "Local":
                            this.lblSendDataInternetMessage.Visible = false;
                            this.txtSendDataLocalPath.Visible = true;

                            break;

                        default:
                            this.lblSendDataInternetMessage.Visible = true;
                            this.txtSendDataLocalPath.Visible = false;

                            break;
                    }
                }

                this.SetSendDataSaveButton();
                this.SetSendDataNote();

                Cursor.Current = Cursors.Default;
            }
        }

        private void chbLogTrace_CheckedChanged(object sender, EventArgs e)
        {
            Lg.DoTrace = this.chbLogTrace.Checked;
            UserSettings.SetLogTrace(Lg.DoTrace);
        }

        private void chbRestoreBackupCurrent_CheckedChanged(object sender, EventArgs e)
        {
            if (!this._isLoading)
            {
                Cursor.Current = Cursors.WaitCursor;

                this.SetRestoreNote();

                Cursor.Current = Cursors.Default;
            }
        }

        private void chbSendDataCompress_CheckedChanged(object sender, EventArgs e)
        {
            if (!this._isLoading)
            {
                Cursor.Current = Cursors.WaitCursor;

                this.SetSendDataNote();

                Cursor.Current = Cursors.Default;
            }
        }

        private void frmHelp_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.InitializeSendDataPanel();
            this.InitializeRestorePanel();
            this.InitializeDocuments();

            Cursor.Current = Cursors.Default;
        }

        private void lblSendDataFile_MouseDown(object sender, MouseEventArgs e)
        {
            if (!this._isLoading)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    this.DecryptConfigFilePrompt();

                    Cursor.Current = Cursors.Default;
                }
            }
        }

        public void DecryptConfigFilePrompt()
        {
            if (Constants.Utilities.IsNetworkAvailable() && DataStore.IsUserDev)
            {
                using (OpenFileDialog dlg = new OpenFileDialog())
                {
                    dlg.CheckFileExists = true;
                    //dlg.DefaultExt = "encrypt";
                    // dlg.FileName = "FieldTool.UI.exe.config.encrypt";
                    dlg.Filter = "Encrypted Config Files (.encrypt)|*.encrypt";
                    dlg.InitialDirectory = FieldTool.KnownFolderPaths.KnownFolders.GetPath(FieldTool.KnownFolderPaths.KnownFolder.Downloads);
                    dlg.Multiselect = false;
                    dlg.Title = "Select Encrypted Configuration File";

                    DialogResult result = dlg.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        Constants.Utilities.ScrubConfigFile(dlg.FileName);
                    }
                }
            }
        }

        private void lstRestoreList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this._isLoading)
            {
                Cursor.Current = Cursors.WaitCursor;

                this.SetRestoreNote();
                this.btnRestoreSelect.Enabled = this.SelectedRestoreBackup != null;

                Cursor.Current = Cursors.Default;
            }
        }

        private void txtSendDataLocalPath_ButtonCustomClick(object sender, EventArgs e)
        {
            if (!this._isLoading)
            {
                Cursor.Current = Cursors.WaitCursor;

                using (FolderBrowserDialog dlg = new FolderBrowserDialog())
                {
                    dlg.Description = "Select a folder on this device that will be used to hold the data file.  The file will not be sent to the " +
                        "System Administrator until you are on a Franklin Energy network and manually transfer it, via email or the network folder.  " +
                        "The file will be overwritten if it already exists.";
                    dlg.RootFolder = Environment.SpecialFolder.MyDocuments;
                    dlg.ShowNewFolderButton = true;

                    DialogResult result = dlg.ShowDialog();

                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        this.txtSendDataLocalPath.Text = dlg.SelectedPath;
                    }
                    else
                    {
                        this.txtSendDataLocalPath.Clear();
                    }
                }

                this.SetSendDataNote();
                this.SetSendDataSaveButton();

                Cursor.Current = Cursors.Default;
            }
        }

        private void txtSendDataLocalPath_VisibleChanged(object sender, EventArgs e)
        {
            if (!this._isLoading)
            {
                Cursor.Current = Cursors.WaitCursor;

                if (!this.txtSendDataLocalPath.Visible)
                {
                    this.txtSendDataLocalPath.Clear();
                }

                Cursor.Current = Cursors.Default;
            }
        }

        #endregion Events

        #region Private helper members

        private string BuildRestoreNote()
        {
            string indent = "   ";
            string result = "This option will:" + Environment.NewLine + Environment.NewLine;

            //string userPlural = "";
            //if (this.cboRestoreUser.Text.EndsWith("s", true))
            result += (this.chbRestoreBackupCurrent.Checked ? indent + "- Make a new backup of " + this.cboRestoreUser.Text + "'s current data file" + Environment.NewLine : "");

            RestoreObject currentItem = this.SelectedRestoreBackup;
            if (currentItem != null)
            {
                result += indent + "- Restore " + this.cboRestoreUser.Text + "'s backup file from " + currentItem.ToString("M") + Environment.NewLine;
                result += indent + "- Load this new data into the Efficiency Clipboard application" + Environment.NewLine + Environment.NewLine;
                //result += indent + "Your existing data file will be replaced and all data in it will be permanently erased! It is highly suggested that you choose the Make a backup option!";
            }

            return result;
        }

        private string BuildSendDataNote()
        {
            string indent = "   ";
            string result = "This option will:" + Environment.NewLine + Environment.NewLine +
                indent + "- Make a copy of all Efficiency Clipboard data assigned to '" + this.cboSendDataUser.Text + "'" + Environment.NewLine;

            result += (this.chbSendDataCompress.Checked ? indent + "- Compress the copied data into a zip file" + Environment.NewLine : "");

            switch (this.cboSendDataVia.SelectedItem.ToString())
            {
                case "Email":
                    result += indent + "- Open Outlook and prepare a new outgoing email to the System Administrator" + Environment.NewLine +
                        indent + "- Attach this file to the new email" + Environment.NewLine + Environment.NewLine +
                        "Please review this email and send it per your approval.";
                    break;

                case "Local":
                    string path = "Please select a local folder";
                    if (!String.IsNullOrWhiteSpace(this.txtSendDataLocalPath.Text))
                    {
                        path = this.txtSendDataLocalPath.Text;
                    }
                    result += indent + "- Copy this file to the local directory: " + path;
                    break;

                case "Network":
                    result += indent + "- Copy this file to a Franklin Energy network directory for the System Administrator to retrieve";
                    break;
            }

            return result;
        }

        private string CompressDataFile()
        {
            string zipFile = "";
            string sourceFilePath = DataStore.XmlDataFile;

            try
            {
                if (File.Exists(sourceFilePath))
                {
                    zipFile = Path.Combine(Path.GetDirectoryName(sourceFilePath), this.ZipFileName);
                    using (FileStream zip = new FileStream(zipFile, FileMode.Create))
                    {
                        using (ZipArchive archive = new ZipArchive(zip, ZipArchiveMode.Create))
                        {
                            archive.CreateEntryFromFile(sourceFilePath, Path.GetFileName(sourceFilePath), CompressionLevel.Optimal);
                            //CreateLogFileZipEntries(archive, NUMBER_OF_LOG_FILES_TO_SEND);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var cause = ExceptionHelper.Innermost(ex);
                Lg.Error(cause.Message);
                Lg.Error(cause.StackTrace.ToString());
            }
            return (File.Exists(zipFile) ? zipFile : "");
        }

        private string CompressAllFiles()
        {
            //Lg.Trace("CompressAllFiles", 0);

            FilePaths paths = new FilePaths(this.ZipFileName);
            //Lg.Info("this.ZipFileName: " + this.ZipFileName);

            //if (paths.IsValid) {
            //Lg.Info("path.IsValid = true");

            try
            {
                using (FileStream zip = new FileStream(paths.ZipFile, FileMode.Create))
                {
                    using (ZipArchive archive = new ZipArchive(zip, ZipArchiveMode.Create))
                    {
                        if (File.Exists(paths.AppConfigFile))
                        {
                            Lg.Info("Archiving AppConfigFile");
                            archive.CreateEntryFromFile(paths.AppConfigFile, Path.GetFileName(paths.AppConfigFile), CompressionLevel.Optimal);
                        }

                        if (File.Exists(paths.CustomRecommendationsFile))
                        {
                            Lg.Info("Archiving CustomRecommendationsFile");
                            archive.CreateEntryFromFile(paths.CustomRecommendationsFile, Path.GetFileName(paths.CustomRecommendationsFile), CompressionLevel.Optimal);
                        }

                        if (File.Exists(paths.DataFile))
                        {
                            Lg.Info("Archiving DataFile");
                            archive.CreateEntryFromFile(paths.DataFile, Path.GetFileName(paths.DataFile), CompressionLevel.Optimal);
                        }

                        if (File.Exists(paths.DebugInfoFile))
                        {
                            Lg.Info("Archiving DebugInfoFile");
                            archive.CreateEntryFromFile(paths.DebugInfoFile, Path.GetFileName(paths.DebugInfoFile), CompressionLevel.Optimal);
                        }

                        if (File.Exists(paths.DiReportFile))
                        {
                            Lg.Info("Archiving DiReportFile");
                            archive.CreateEntryFromFile(paths.DiReportFile, Path.GetFileName(paths.DiReportFile), CompressionLevel.Optimal);
                        }

                        if (File.Exists(paths.ProgramsFile))
                        {
                            Lg.Info("Archiving ProgramsFile");
                            archive.CreateEntryFromFile(paths.ProgramsFile, Path.GetFileName(paths.ProgramsFile), CompressionLevel.Optimal);
                        }

                        if (File.Exists(paths.ReportFile))
                        {
                            Lg.Info("Archiving ReportFile");
                            archive.CreateEntryFromFile(paths.ReportFile, Path.GetFileName(paths.ReportFile), CompressionLevel.Optimal);
                        }

                        if (File.Exists(paths.SettingsFile))
                        {
                            Lg.Info("Archiving SettingsFile");
                            archive.CreateEntryFromFile(paths.SettingsFile, Path.GetFileName(paths.SettingsFile), CompressionLevel.Optimal);
                        }

                        Lg.Info("Zipping");
                        this.CreateLogFileZipEntries(archive, NUMBER_OF_LOG_FILES_TO_SEND);
                    }
                }
            }
            catch (Exception ex)
            {
                var cause = ExceptionHelper.Innermost(ex);
                Lg.Error(cause.Message);
                Lg.Error(cause.StackTrace.ToString());
            }

            //}

            //if (File.Exists(paths.ZipFile)) {
            //FileInfo f = new FileInfo(paths.ZipFile);

            //string s = ".DirectoryName: " + f.DirectoryName + ".FullName: " + f.FullName + ".Length: " + f.Length + ".Name: " + f.Name;
            //Lg.Info("paths.ZipFile: " + s);

            //}
            //else {
            //    Lg.Info("paths.ZipFile Not Found");
            //}

            return paths.ZipFile;
        }

        private List<RestoreObject> GetBackupFiles()
        {
            List<RestoreObject> result = new List<RestoreObject>();

            try
            {
                string backupFolder = Path.Combine(DataStore.XmlDataDirectory, DATA_BACKUP_FOLDER);

                if (Directory.Exists(backupFolder))
                {
                    foreach (string file in Directory.GetFiles(backupFolder, DATA_BACKUP_FILE_FORMAT, SearchOption.TopDirectoryOnly))
                    {
                        result.Add(new RestoreObject(file));
                    }
                }
            }
            catch (Exception ex)
            {
                var cause = ExceptionHelper.Innermost(ex);
                Lg.Error(cause.Message);
                Lg.Error(cause.StackTrace.ToString());
            }
            return result;
        }

        private string GetInstallPath()
        {
            string result = "Undetermined";

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                // Running from installation.

                try
                {
                    result = ApplicationDeployment.CurrentDeployment.DataDirectory;
                }
                catch (Exception ex)
                {
                    var cause = ExceptionHelper.Innermost(ex);
                    Lg.Error(cause.Message);
                    Lg.Error(cause.StackTrace.ToString());
                }
            }
            else
            {
                // Not running from installation.

                result = System.Environment.CurrentDirectory;
            }

            return result;
        }

        private List<string> GetSendMediums()
        {
            List<string> result = new List<string>();

            if (Constants.Utilities.IsNetworkAvailable())
            {
                result.Add("Email");
                result.Add("Network");
            }

            result.Add("Local");

            return result;
        }

        private List<string> GetUsers()
        {
            List<string> result = new List<string>();
            DirectoryInfo rootDirectory = new DirectoryInfo(DataStore.XmlDataDirectoryRoot);

            foreach (DirectoryInfo userDirectory in rootDirectory.GetDirectories())
            {
                result.Add(userDirectory.Name);
            }

            return result;
        }

        private void InitializeRestorePanel()
        {
            this._isLoading = true;

            this.cboRestoreUser.DataSource = this.GetUsers();
            this.cboRestoreUser.Text = DataStore.UserName;

            this.LoadRestoreItems();

            this.chbRestoreBackupCurrent.Checked = true;

            this.SetRestoreNote();

            this.btnRestoreSelect.Enabled = false;

            // Always create a backup.
            this.chbRestoreBackupCurrent.Enabled = false;
            // User can't change user drowdown.
            this.cboRestoreUser.Enabled = false;

            this._isLoading = false;
        }

        private void InitializeSendDataPanel()
        {
            this._isLoading = true;

            this.cboSendDataUser.DataSource = this.GetUsers();
            this.cboSendDataUser.Text = DataStore.UserName;

            this.chbSendDataCompress.Checked = true;

            this.cboSendDataVia.DataSource = this.GetSendMediums();
            this.cboSendDataVia.SelectedIndex = 0;

            this.lblSendDataInternetMessage.Visible = true;

            this.txtSendDataLocalPath.Clear();
            this.txtSendDataLocalPath.Visible = false;

            this.SetSendDataNote();

            this.SetSendDataSaveButton();

            // User can't change user drowdown.
            this.cboSendDataUser.Enabled = false;

            this.chbLogTrace.Checked = Lg.DoTrace;

            this._isLoading = false;
        }

        private void InitializeDocuments()
        {
            this._isLoading = true;

            this.cboDocuments.DataSource = UserDocs.GetDocs();
            cboDocuments.DisplayMember = "DocName";
            cboDocuments.ValueMember = "FileNamePath";
            if (this.cboDocuments.Items.Count > 0)
            {
                this.cboDocuments.SelectedIndex = 0;
                btnViewDocument.Enabled = true;
            }
            else
            {
                btnViewDocument.Enabled = false;
            }

            this._isLoading = false;
        }

        private void LoadDebugInfo()
        {
            string eol = Environment.NewLine;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}: {1}{2}", AppConstants.AppKeys.ENV, ConfigurationManager.AppSettings[AppConstants.AppKeys.ENV], eol);
            sb.AppendFormat("{0}: {1}{2}", CPConstants.AppKeys.BSI_AUTH_ENDPOINT, ConfigurationManager.AppSettings[CPConstants.AppKeys.BSI_AUTH_ENDPOINT], eol);
            sb.AppendFormat("{0}: {1}{2}", CPConstants.AppKeys.BSI_USERNAME, ConfigurationManager.AppSettings[CPConstants.AppKeys.BSI_USERNAME], eol);

            var psw = ConfigurationManager.AppSettings[CPConstants.AppKeys.BSI_PASSWORD];
            sb.AppendFormat("{0}: ...{1}{2}", CPConstants.AppKeys.BSI_PASSWORD, Tail(psw, 2), eol);

            var token = ConfigurationManager.AppSettings[CPConstants.AppKeys.BSI_TOKEN];
            sb.AppendFormat("{0}: ...{1}{2}", CPConstants.AppKeys.BSI_TOKEN, Tail(token, 2), eol);

            var k = ConfigurationManager.AppSettings[CPConstants.AppKeys.BSI_CLIENT_KEY];
            sb.AppendFormat("{0}: ...{1}{2}", CPConstants.AppKeys.BSI_CLIENT_KEY, Tail(k, 2), eol);

            var secret = ConfigurationManager.AppSettings[CPConstants.AppKeys.BSI_CLIENT_SECRET];
            sb.AppendFormat("{0}: ...{1}{2}", CPConstants.AppKeys.BSI_CLIENT_SECRET, Tail(secret, 2), eol);

            foreach (ConnectionStringSettings conn in ConfigurationManager.ConnectionStrings)
            {
                sb.AppendFormat("{0}CONNECTION: {1}{0}{0}", eol, conn.Name);
                var map = conn.ConnectionString.Split(';').ToList().Select(x => x.Split('=')).ToDictionary(x => x[0].ToLower(), x => x[1]);
                // now use it to pull only specific parts
                if (map.Keys.Contains(AppConstants.ConnKeys.DATA_SOURCE))
                {
                    sb.AppendFormat("{0}: {1}{2}", AppConstants.ConnKeys.DATA_SOURCE, map[AppConstants.ConnKeys.DATA_SOURCE], eol);
                }
                if (map.Keys.Contains(AppConstants.ConnKeys.INITIAL_CATALOG))
                {
                    sb.AppendFormat("{0}: {1}{2}", AppConstants.ConnKeys.INITIAL_CATALOG, map[AppConstants.ConnKeys.INITIAL_CATALOG], eol);
                }

                if (map.Keys.Contains(AppConstants.ConnKeys.APPLICATION_NAME))
                {
                    sb.AppendFormat("{0}: {1}{2}", AppConstants.ConnKeys.APPLICATION_NAME, map[AppConstants.ConnKeys.APPLICATION_NAME], eol);
                }
                if (map.Keys.Contains(AppConstants.ConnKeys.INTEGRATED_SECURITY))
                {
                    sb.AppendFormat("{0}: {1}{2}", AppConstants.ConnKeys.INTEGRATED_SECURITY, map[AppConstants.ConnKeys.INTEGRATED_SECURITY], eol);
                }
                if (map.Keys.Contains(AppConstants.ConnKeys.PERSIST_SECURITY_INFO))
                {
                    sb.AppendFormat("{0}: {1}{2}", AppConstants.ConnKeys.PERSIST_SECURITY_INFO, map[AppConstants.ConnKeys.PERSIST_SECURITY_INFO], eol);
                }
                if (map.Keys.Contains(AppConstants.ConnKeys.USER_ID))
                {
                    sb.AppendFormat("{0}: {1}{2}", AppConstants.ConnKeys.USER_ID, map[AppConstants.ConnKeys.USER_ID], eol);
                }

                if (map.Keys.Contains(AppConstants.ConnKeys.PASSWORD))
                {
                    var connPwd = map[AppConstants.ConnKeys.PASSWORD];
                    sb.AppendFormat("{0}: ...{1}{2}", AppConstants.ConnKeys.PASSWORD, Tail(connPwd, 2), eol);
                }
            }

            if (this.chbDebugCopyToClipboard.Checked)
            {
                Constants.Utilities.SetClipboardText(sb.ToString());
            }

            this.txtDebugData.Text = sb.ToString();
        }

        private void LoadRestoreItems()
        {
            List<RestoreObject> files = this.GetBackupFiles();
            files.Sort(new RestoreObjectComparer_DESC());

            this.lstRestoreList.Items.Clear();

            if (files != null)
            {
                DayOfWeek currentDay = default(DayOfWeek);
                DayOfWeek lastDay = default(DayOfWeek);
                ListViewGroup grp = null;

                foreach (RestoreObject restore in files)
                {
                    currentDay = restore.BackupDay;

                    if (grp == null || currentDay != lastDay)
                    {
                        grp = new ListViewGroup(currentDay.ToString());
                        this.lstRestoreList.Groups.Add(grp);
                    }

                    ListViewItem li = new ListViewItem(restore.ToString("LI"), grp);
                    li.Tag = restore;
                    this.lstRestoreList.Items.Add(li);

                    lastDay = currentDay;
                }

                this.colRestoreItems.Width = this.lstRestoreList.Width - 20;
            }
        }

        private void RestoreBackup()
        {
            RestoreObject restore = this.SelectedRestoreBackup;

            if (restore != null)
            {
                if (this.chbRestoreBackupCurrent.Checked)
                {
                }
            }
        }

        private void SetRestoreNote()
        {
            this.lblRestoreNote.Text = this.BuildRestoreNote();
        }

        private void SetSendDataNote()
        {
            this.lblSendDataNote.Text = this.BuildSendDataNote();
        }

        private void SetSendDataSaveButton()
        {
            bool result = true;

            if (this.cboSendDataVia.SelectedItem != null && this.cboSendDataVia.SelectedItem.ToString() == "Local")
            {
                result = (!String.IsNullOrWhiteSpace(this.txtSendDataLocalPath.Text));
            }

            this.btnSendDataSend.Enabled = result;
        }

        private void SetDataSendFile(bool deleteCompressedFileAfterSend)
        {
            Lg.Info("SetDataSendFile()");

            DialogResult prompt = MessageBox.Show("Are you sure you want to send your Efficiency Clipboard data files?",
                "Send Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (prompt == System.Windows.Forms.DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;

                SendResult result = null;
                string compressedDataFilePath = "";
                string dataFileToSend = "";
                string newFileName = "";

                if (this.chbSendDataCompress.Checked)
                {
                    Lg.Info("Compressing.");

                    compressedDataFilePath = this.CompressAllFiles();
                    Lg.Info("compressedDataFilePath: " + compressedDataFilePath);
                    Lg.Info("compressedDataFilePath Exists: " + File.Exists(compressedDataFilePath));

                    newFileName = Path.GetFileName(compressedDataFilePath);
                    Lg.Info("newFileName: " + newFileName);
                }
                else
                {
                    Lg.Info("Not Compressing");
                    newFileName = this.XmlFileNameToSend;
                    Lg.Info("newFileName: " + newFileName);
                }

                dataFileToSend = (String.IsNullOrWhiteSpace(compressedDataFilePath) ? DataStore.XmlDataFile : compressedDataFilePath);

                Lg.Info("dataFileToSend: " + dataFileToSend);
                Lg.Info("compressedDataFilePath (2): " + compressedDataFilePath);
                Lg.Info("DataStore.XmlDataFile: " + DataStore.XmlDataFile);

                if (this.cboSendDataVia.SelectedItem != null)
                {
                    switch (this.cboSendDataVia.SelectedItem.ToString())
                    {
                        case "Email":
                            result = this.SendDataSendFileEmail(dataFileToSend);
                            break;

                        case "Local":
                            result = this.SendDataSendFileLocal(dataFileToSend, newFileName);
                            break;

                        case "Network":
                            result = this.SendDataSendFileNetwork(dataFileToSend, newFileName);
                            break;
                    }
                }

                if (deleteCompressedFileAfterSend && File.Exists(compressedDataFilePath))
                {
                    File.Delete(compressedDataFilePath);
                }

                if (result != null && result.HasResult)
                {
                    result.ShowDialog();
                }

                Cursor.Current = Cursors.Default;
            }
        }

        private SendResult SendDataSendFileEmail(string dataFileAttachmentPath)
        {
            SendResult result = null;

            if (Constants.Utilities.IsNetworkAvailable())
            {
                Outlook.Application outlookApp = null;
                Outlook.MailItem message = null;
                Outlook.Recipient receiver = null;

                try
                {
                    outlookApp = new Outlook.Application();
                    message = (Outlook.MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);

                    string[] recipients = DataStore.EmailDataTo.Split(',');
                    foreach (string recipient in recipients)
                    {
                        receiver = (Outlook.Recipient)message.Recipients.Add(recipient);
                    }

                    receiver.Resolve();

                    message.Subject = "Efficiency Clipboard Data File";

                    if (File.Exists(dataFileAttachmentPath))
                    {
                        message.Body = "The attached data files were sent so that you can troubleshoot a problem with Efficiency Clipboard.";

                        int position = (int)message.Body.Length + 1;
                        int attachmentType = (int)Outlook.OlAttachmentType.olByValue;

                        message.Attachments.Add(dataFileAttachmentPath, attachmentType, position, "Data File");

                        if (!chbSendDataCompress.Checked)
                        {
                            // add log files
                            foreach (string logFilePath in GetLogFiles(NUMBER_OF_LOG_FILES_TO_SEND))
                            {
                                message.Attachments.Add(logFilePath, attachmentType, position, Path.GetFileNameWithoutExtension(logFilePath));
                            }
                        }
                    }
                    else
                    {
                        message.Body = "The data file could not be located: " + dataFileAttachmentPath;
                        Lg.Info(message.Body);
                    }

                    //message.Display();
                    message.Save();
                    ((Outlook.MailItem)message).Send();

                    result = new SendResult("Your Efficiency Clipboard data files have been successfully emailed.", "Send Data Files ", MessageBoxButtons.OK, MessageBoxIcon.Information, false);
                }
                catch (Exception ex)
                {
                    var cause = ExceptionHelper.Innermost(ex);
                    Lg.Error(cause.Message);
                    Lg.Error(cause.StackTrace.ToString());
                }
                finally
                {
                    receiver = null;
                    message = null;
                    outlookApp = null;
                }
            }
            else
            {
                result = new SendResult("You must have a network connection to send data via email.", "No Network Connection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, true);
            }

            return result;
        }

        private SendResult SendDataSendFileLocal(string dataFilePath, string newFileName)
        {
            SendResult result = null;
            string directoryPath = this.txtSendDataLocalPath.Text;
            string newPath = "";

            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    DialogResult dlg = MessageBox.Show("The directory '" + directoryPath + "' does not exist.  Would you like to create it?",
                        "Send Data Files", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dlg == System.Windows.Forms.DialogResult.Yes)
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                }

                newPath = Path.Combine(directoryPath, newFileName);
                File.Copy(dataFilePath, newPath, true);

                if (!chbSendDataCompress.Checked)
                {
                    // send log files
                    foreach (string logFilePath in GetLogFiles(NUMBER_OF_LOG_FILES_TO_SEND))
                    {
                        File.Copy(logFilePath, Path.Combine(Path.GetDirectoryName(newPath), Path.GetFileName(logFilePath)), true);
                    }
                }
            }
            catch (Exception ex)
            {
                var cause = ExceptionHelper.Innermost(ex);
                Lg.Error(cause.Message);
                Lg.Error(cause.StackTrace.ToString());
            }

            result = new SendResult("Your Efficiency Clipboard data files have been successfully copied as: '" + newPath + "'.",
                "Send Data Files ", MessageBoxButtons.OK, MessageBoxIcon.Information, false);

            return result;
        }

        private SendResult SendDataSendFileNetwork(string dataFilePath, string newFileName)
        {
            SendResult result = null;

            if (Constants.Utilities.IsNetworkAvailable())
            {
                try
                {
                    if (File.Exists(dataFilePath))
                    {
                        try
                        {
                            File.Copy(dataFilePath, Path.Combine(EFFICIENCY_CLIPBOARD_NETWORK_HELP_FOLDER, newFileName), true);

                            if (!chbSendDataCompress.Checked)
                            {
                                // send log files
                                foreach (string logFilePath in GetLogFiles(NUMBER_OF_LOG_FILES_TO_SEND))
                                {
                                    File.Copy(logFilePath, Path.Combine(EFFICIENCY_CLIPBOARD_NETWORK_HELP_FOLDER, Path.GetFileName(logFilePath)), true);
                                }
                            }

                            result = new SendResult("Your Efficiency Clipboard data files have been successfully sent to the network.",
                                "Send Data Files ", MessageBoxButtons.OK, MessageBoxIcon.Information, false);
                        }
                        catch (Exception ex)
                        {
                            result = new SendResult(ex.ToString(), "Send Data Files", MessageBoxButtons.OK, MessageBoxIcon.Error, true);
                        }
                    }
                    else
                    {
                        result = new SendResult("Could not locate your data file.  If the problem persists, please submit a bug report.",
                            "Send Data Files", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, true);
                    }
                }
                catch (Exception)
                {
                    result = new SendResult("You must have a Franklin Energy network connection to send data using this method.",
                        "No Network Connection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, true);
                }
            }
            else
            {
                result = new SendResult("You must have a network connection to send data using this method of data transmission.",
                    "No Network Connection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, true);
            }

            return result;
        }

        private string Tail(string val, int tailToShow)
        {
            return (!String.IsNullOrEmpty(val) && val.Length > tailToShow) ? val.Substring(val.Length - tailToShow, tailToShow) : val;
        }

        private void CreateLogFileZipEntries(ZipArchive archive, int numberOfFiles)
        {
            try
            {
                Lg.Info(String.Format("Zipping {0} log files.", numberOfFiles.ToString()));
                foreach (string logFileName in GetLogFiles(numberOfFiles))
                {
                    try
                    {
                        archive.CreateEntryFromFile(logFileName, Path.GetFileName(logFileName), CompressionLevel.Optimal);
                    }
                    catch (Exception ex)
                    {
                        Lg.Error(ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                var cause = ExceptionHelper.Innermost(ex);
                Lg.Error(cause.Message);
                Lg.Error(cause.StackTrace.ToString());
            }
        }

        private List<string> GetLogFiles(int numberOfFiles)
        {
            List<string> logFilePaths = new List<string>();

            try
            {
                var filePath = string.Format(PathConstant.xmlDataDirectory, System.Environment.UserName);
                filePath = Path.Combine(filePath, "Logging");

                if (Directory.Exists(filePath))
                {
                    List<string> files = Directory.GetFiles(filePath, "Log*.txt").ToList();
                    numberOfFiles = Math.Min(files.Count, numberOfFiles);
                    logFilePaths.AddRange(files.Skip(files.Count - numberOfFiles).Take(numberOfFiles));
                }
            }
            catch (Exception ex)
            {
                var cause = ExceptionHelper.Innermost(ex);
                Lg.Error(cause.Message);
                Lg.Error(cause.StackTrace.ToString());
            }

            return logFilePaths;
        }

        #endregion Private helper members

        private void btnDataFileRecovery_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new frmDataFileRecovery(_parent);
                frm.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    internal class RestoreObject : IComparable, IComparable<RestoreObject>, IEquatable<RestoreObject>, IFormattable
    {
        #region Private member variables

        private string _fileName;

        #endregion Private member variables

        #region Constructors

        internal RestoreObject(string fileName)
        {
            this.FileName = fileName;
        }

        #endregion Constructors

        #region Properties

        internal DayOfWeek BackupDay { get; set; }

        internal int BackupHour { get; set; }

        internal string BackupPeriod { get; private set; }

        internal DateTime CreatedDate { get; set; }

        internal DateTime LastModifiedDate { get; set; }

        internal int MilitaryBackupHour { get; set; }

        internal string FileName
        {
            get
            {
                return this._fileName;
            }
            set
            {
                this._fileName = (value ?? "").Trim();
                this.ParseFileName();
            }
        }

        internal long FileSize { get; set; }

        #endregion Properties

        #region Overridden base methods

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            int result = base.GetHashCode();

            unchecked
            {
                result += 7 * this.BackupDay.GetHashCode();
                result += 7 * this.BackupHour.GetHashCode();
                result += 7 * this.CreatedDate.GetHashCode();
                result += 7 * this.LastModifiedDate.GetHashCode();
                result += 7 * this.FileName.GetHashCode();
                result += 7 * this.FileSize.GetHashCode();
            }

            return result;
        }

        public override string ToString()
        {
            return this.ToString("", null);
        }

        #endregion Overridden base methods

        #region IComparable Members

        public int CompareTo(object obj)
        {
            int result = 1;

            if (obj != null)
            {
                RestoreObject r = obj as RestoreObject;

                if (r != null)
                {
                    result = this.BackupDay.CompareTo(r.BackupDay);

                    if (result == 0)
                    {
                        result = this.BackupHour.CompareTo(r.BackupHour);

                        if (result == 0)
                        {
                            result = this.LastModifiedDate.CompareTo(r.LastModifiedDate);
                        }
                    }
                }
            }

            return result;
        }

        #endregion IComparable Members

        #region IComparable<RestoreObject> Members

        public int CompareTo(RestoreObject other)
        {
            int result = 1;

            if (other != null)
            {
                result = this.BackupDay.CompareTo(other.BackupDay);

                if (result == 0)
                {
                    result = this.BackupHour.CompareTo(other.BackupHour);

                    if (result == 0)
                    {
                        result = this.LastModifiedDate.CompareTo(other.LastModifiedDate);
                    }
                }
            }

            return result;
        }

        #endregion IComparable<RestoreObject> Members

        #region IEquatable<RestoreObject> Members

        public bool Equals(RestoreObject other)
        {
            bool result = false;

            if (other != null)
            {
                if (Object.ReferenceEquals((object)this, (object)other))
                {
                    result = true;
                }
                else
                {
                    result = this.FileName.Equals(other.FileName);
                }
            }

            return result;
        }

        #endregion IEquatable<RestoreObject> Members

        #region Private helper methods

        private DayOfWeek GetRestoreBackupDay()
        {
            DayOfWeek result = default(DayOfWeek);

            string fileName = Path.GetFileName(this.FileName);
            int pos1 = fileName.IndexOf('-');
            int pos2 = fileName.IndexOf('-', pos1 + 1);

            if (pos2 > pos1)
            {
                string dayName = fileName.Substring(pos1 + 1, pos2 - pos1 - 1);

                switch (dayName.ToUpper())
                {
                    case "SUN":
                        result = DayOfWeek.Sunday;
                        break;

                    case "MON":
                        result = DayOfWeek.Monday;
                        break;

                    case "TUE":
                        result = DayOfWeek.Tuesday;
                        break;

                    case "WED":
                        result = DayOfWeek.Wednesday;
                        break;

                    case "THU":
                        result = DayOfWeek.Thursday;
                        break;

                    case "FRI":
                        result = DayOfWeek.Friday;
                        break;

                    case "SAT":
                        result = DayOfWeek.Saturday;
                        break;
                }
            }

            return result;
        }

        private int GetRestoreBackupHour()
        {
            int result = default(int);

            string fileName = Path.GetFileNameWithoutExtension(this.FileName);
            int pos = fileName.LastIndexOf('-');
            string hourText = fileName.Substring(pos + 1);
            int militaryHour = 0;

            if (int.TryParse(hourText, out militaryHour))
            {
                if (militaryHour >= 0 && militaryHour <= 23)
                {
                    this.MilitaryBackupHour = militaryHour;

                    TimeSpan ts = new TimeSpan(militaryHour, 0, 0);
                    DateTime time = DateTime.Today.Add(ts);
                    int hour = 0;

                    if (int.TryParse(time.ToString("hh"), out hour))
                    {
                        result = hour;
                        this.BackupPeriod = time.ToString("tt");
                    }
                }
            }

            return result;
        }

        private void SetFileInformation()
        {
            FileInfo file = new FileInfo(this.FileName);

            if (file != null)
            {
                this.CreatedDate = file.CreationTime;
                this.FileSize = file.Length;
                this.LastModifiedDate = file.LastWriteTime;
            }
        }

        private void ParseFileName()
        {
            if (!String.IsNullOrWhiteSpace(this.FileName) && File.Exists(this.FileName))
            {
                this.BackupDay = this.GetRestoreBackupDay();
                this.BackupHour = this.GetRestoreBackupHour();
                this.SetFileInformation();
            }
        }

        #endregion Private helper methods

        #region IFormattable Members

        public string ToString(string format)
        {
            return this.ToString(format, null);
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return this.ToString("", formatProvider);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            string result = base.ToString();

            if (String.IsNullOrWhiteSpace(format))
            {
                format = "G";
            }

            if (formatProvider != null)
            {
                ICustomFormatter formatter = formatProvider.GetFormat(this.GetType()) as ICustomFormatter;

                if (formatter != null)
                {
                    result = formatter.Format(format, this, formatProvider);
                }
            }
            else
            {
                switch (format.Trim().ToUpper())
                {
                    case "L":
                    case "LI":
                    case "LISTITEM":
                        result = String.Format("Backup from {0}:00 {1}", this.BackupHour, this.BackupPeriod);
                        break;

                    case "M":
                    case "MSG":
                    case "MESSAGE":
                        result = String.Format("{0} at {1}:00 {2}", this.BackupDay, this.BackupHour, this.BackupPeriod);
                        break;

                    default:
                        result = String.Format("BackupDay: {0}, BackupHour: {1}, BackupPeriod: {2}, CreatedDate: {3}, FileName: {4}, FileSize: {5}, LastModifiedDate: {6}",
                            this.BackupDay,
                            this.BackupHour,
                            this.BackupPeriod,
                            this.CreatedDate.ToString(),
                            this.FileName,
                            this.FileSize,
                            this.LastModifiedDate.ToString()
                        );

                        break;
                }
            }

            return result;
        }

        #endregion IFormattable Members
    }

    internal class RestoreObjectComparer_ASC : IComparer<RestoreObject>
    {
        #region IComparer<RestoreObject> Members

        public int Compare(RestoreObject x, RestoreObject y)
        {
            int i = x.BackupDay.CompareTo(y.BackupDay);

            if (i != 0)
            {
                return i;
            }
            else
            {
                i = x.MilitaryBackupHour.CompareTo(y.MilitaryBackupHour);

                if (i != 0)
                {
                    return i;
                }
                else
                {
                    return x.LastModifiedDate.CompareTo(y.LastModifiedDate);
                }
            }
        }

        #endregion IComparer<RestoreObject> Members
    }

    internal class RestoreObjectComparer_DESC : IComparer<RestoreObject>
    {
        #region IComparer<RestoreObject> Members

        public int Compare(RestoreObject x, RestoreObject y)
        {
            int i = y.BackupDay.CompareTo(x.BackupDay);

            if (i != 0)
            {
                return i;
            }
            else
            {
                i = y.MilitaryBackupHour.CompareTo(x.MilitaryBackupHour);

                if (i != 0)
                {
                    return i;
                }
                else
                {
                    return y.LastModifiedDate.CompareTo(x.LastModifiedDate);
                }
            }
        }

        #endregion IComparer<RestoreObject> Members
    }
}