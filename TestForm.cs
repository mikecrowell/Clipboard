using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Flurl.Http;
using Ionic.Zip;
using System.IO;
using System.Net;

namespace FieldTool.UI
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private string _clientId = "001C000000vzTSmIAM";
        private string _userName = "kgrittner";
        private string _placementExtractedDirectory = @"C:\Users\kgrittner\Pictures\tst\{0}";
        private string _placementPath = @"C:\Users\kgrittner\Pictures\tst\{0}.zip";
        private string _copyBrandingToDirectory = @"C:\Users\kgrittner\Pictures\tst\Branding";
        private string _endpoint = "http://localhost:54690/CipboardDataFiles/Branding";
        private string _finaleFilePath = "";

        private async void button1_Click(object sender, EventArgs e)
        {
            _finaleFilePath = "";
            _finaleFilePath = string.Format(_placementPath, _clientId);

            await HttpLogin(_endpoint, _clientId, _userName, _finaleFilePath);
        }


        private async Task<bool> HttpLogin(string endpoint, string clientId, string userName, string filePlacementPath)
        {
            string brandingZipUrl = await endpoint
                 .PostUrlEncodedAsync(
                 new
                 {
                     clientId = clientId,
                     username = userName
                 })
                    .ReceiveString();

            DownloadAndCopyFileToPath(brandingZipUrl, clientId, filePlacementPath);
            return true;
        }

        public WebClient webClient = new WebClient();

        private void DownloadAndCopyFileToPath(string brandingZipUrl, string clientId, string filePlacementPath)
        {
            webClient = new WebClient();                                                                    // Creates a webclient
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);                   // Uses the Event Handler to check whether the download is complete
            webClient.DownloadFileAsync(new Uri(brandingZipUrl), filePlacementPath);                        // Defines the URL and destination directory for the downloaded file
        }

        public int count = 0;
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            label1.Text = count.ToString();
            count++;
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            webClient.Dispose();
            MessageBox.Show("Completed");

        }

        private void btnUnpack_Click(object sender, EventArgs e)
        {
            if (File.Exists(_finaleFilePath))
            {

                string directory = string.Format(_placementExtractedDirectory, _clientId);

                ZipFile zipFile = new ZipFile(_finaleFilePath);
                zipFile.ExtractAll(directory, ExtractExistingFileAction.OverwriteSilently);

                
                

                if (Directory.Exists(directory))
                {
                    DirectoryCopy.CopyDirectory(directory, _copyBrandingToDirectory, copySubDirs:true, overWriteFiles:true);
                }




            }
        }











    }











    public static class DirectoryCopy
    {
        public static void CopyDirectory(string sourceDirName, string destDirName, bool copySubDirs, bool overWriteFiles)
        {
            // Copy from the current directory, include subdirectories.
            Copy(sourceDirName, destDirName, copySubDirs, overWriteFiles);
        }

        private static void Copy(string sourceDirName, string destDirName, bool copySubDirs, bool overWriteFiles)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, overWriteFiles);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    Copy(subdir.FullName, temppath, copySubDirs, overWriteFiles);
                }
            }
        }
    }




}
