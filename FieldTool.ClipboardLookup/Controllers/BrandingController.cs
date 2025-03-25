using Ionic.Zip;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Configuration;
using System.Web;
using FieldTool.Constants.Logging;
using FieldTool.ClipboardLookup.Models;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace FieldTool.ClipboardLookup.Controllers
{
    [RoutePrefix("api/branding")]
    public class BrandingController : BaseLoggingController
    {
        private readonly string _azureStorageConnectionKey = "ClipboardAzureStorageConnectionString";
        private readonly string _azureStorageContainerKey = "ClipboardAzureStorageContainer";

        private readonly string BRANDING_CONFIGURATION_URI = "dat/ConfigurationParent/ApiBrandingConfiguration.json";
        private readonly string URL_ZIP = "{0}/temp/{1}.zip";
        private readonly string URL_ZIP_EXISTING = "{0}/temp/{1}";
        private readonly string ZIP_TEMP_LOCATION = "~/temp/{0}.zip";
        private readonly string ZIP_FILE_NAME = "{0}_{1}_{2:yyyy_MM_dd_hh_mm_ss}";
        private readonly string TEMP_DIRECTORY = "~/temp/";

        //

        public BrandingController(ILogger logger)
            : base(logger)
        {
        }

        [HttpGet]
        public string Test()
        {
            var str = "";
            return str;
        }

        [HttpGet]
        // GET: CipboardDataFiles/Branding
        public string Index(string parentFolderName, string username, string uriMustContain = "")
        {
            try
            {
                string azureStorageConnectionString = ConfigurationManager.ConnectionStrings[_azureStorageConnectionKey].ConnectionString;
                string azureStorageContainer = ConfigurationManager.AppSettings[_azureStorageContainerKey];
                //string brandingConfigurationUri = string.Format(BRANDING_CONFIGURATION_URI, azureStorageContainer);

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(azureStorageConnectionString);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference(azureStorageContainer);
                List<Uri> uriCollection = new List<Uri>();
                ZipFile zipFile = new ZipFile();
                List<CloudBlockBlob> list = new List<CloudBlockBlob>();
                BrandingConfig brandingConfig = GetBrandingConfig(parentFolderName, container, BRANDING_CONFIGURATION_URI);
                FileInfo brandingZip = GetZipByConfig(HttpContext.Current.Server.MapPath(TEMP_DIRECTORY), brandingConfig);

                if (brandingConfig != default(BrandingConfig) && brandingZip != default(FileInfo) && brandingConfig.CacheBrandingFiles)
                {
                    string urlZip = string.Format(URL_ZIP_EXISTING, Request.RequestUri.GetLeftPart(UriPartial.Authority), brandingZip.Name);
                    return urlZip;
                }
                else
                {
                    string parentFolderNameEncoded = Uri.EscapeUriString(parentFolderName);
                    string uriMustContainEncoded = string.IsNullOrEmpty(uriMustContain) ? "" : Uri.EscapeUriString(uriMustContain);

                    // Search through each file in azure container
                    foreach (IListBlobItem item in container.ListBlobs(null, true))
                    {
                        if (item.GetType() == typeof(CloudBlockBlob))
                        {
                            CloudBlockBlob blob = (CloudBlockBlob)item;

                            // Only grab files that contain the clients id and is in the branding folder
                            if (blob.Uri.AbsolutePath.ToLower().Contains(parentFolderNameEncoded?.ToLower()) && blob.Uri.AbsolutePath.ToLower().Contains(uriMustContainEncoded?.ToLower()))
                            {
                                list.Add(blob);
                            }
                        }
                    }

                    if (list.Count == 0)
                    {
                        return "Empty";
                    }


                    // Create the zip file and return the Url
                    string filename = Download(list, parentFolderName, username);

                    // Format the download url
                    string url = string.Format(URL_ZIP, Request.RequestUri.GetLeftPart(UriPartial.Authority), filename);

                    // Return the url to download the zip file
                    return url;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return $"ERROR: {ex.Message}";
            }
        }


        private BrandingConfig GetBrandingConfig(string parentFolderName, CloudBlobContainer container, string blobReferancePath)
        {
            try
            {
                CloudBlob file = (CloudBlob)container.GetBlobReference(blobReferancePath);
                string json = ConvertCloudBlobToString(file)?.Replace("???", "");
                List<BrandingConfig> branding = JsonConvert.DeserializeObject<List<BrandingConfig>>(json);
                return branding.Where(x=> x.FolderName == parentFolderName).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private string ConvertCloudBlobToString(CloudBlob file)
        {
            string result = "";

            using (Stream fs = file.OpenRead())
            {
                fs.Seek(0, SeekOrigin.Begin);

                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    byte[] bytes = ms.ToArray();
                    result = Encoding.ASCII.GetString(bytes);
                }
            }

            return result;
        }

        public string Download(List<CloudBlockBlob> list, string parentFolderName, string username)
        {
            try
            {
                IEnumerable<CloudBlob> allFiles = list.Cast<CloudBlob>();
                ZipFile zip = new ZipFile();

                foreach (CloudBlob file in allFiles)
                {
                    // Create the filename to add in the zip file
                    string entryName = Clean(file.Uri, parentFolderName);

                    using (Stream fs = file.OpenRead())
                    {
                        fs.Seek(0, SeekOrigin.Begin);

                        using (MemoryStream ms = new MemoryStream())
                        {
                            fs.CopyTo(ms);

                            // Add the file to the zip file
                            zip.AddEntry(entryName, ms.ToArray());
                        }
                    }
                }

                // Delete Files older than 1 day
                DeleteYesterdaysFiles(HttpContext.Current.Server.MapPath(TEMP_DIRECTORY));

                // Formate the zips file name to be username plus formatted datetime
                string fileName = string.Format(ZIP_FILE_NAME, parentFolderName, username, DateTime.Now);

                // Get the root http url and combine it with the filename
                string path = HttpContext.Current.Server.MapPath(string.Format(ZIP_TEMP_LOCATION, fileName));

                // Save the zip file to the server file system
                zip.Save(path);



                // return the filename because it contains the dynamic date time
                return fileName;
            }
            catch (Exception e)
            {
                throw;
            }
            
        }

        private string Clean(Uri uri, string parentFolderName)
        {
            try
            {
                // We need to remove the extra file paths from the uri so the folder structure in the zip file is intact.
                string path = uri.AbsolutePath;
                string parentFolderNameEncoded = string.IsNullOrEmpty(parentFolderName) ? "" : Uri.EscapeUriString(parentFolderName);
                Regex reg = new Regex($"^.*{parentFolderNameEncoded}/");
                string output = reg.Replace(path, "");
                output = output?.Replace("%20", " ");
                return output;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void DeleteYesterdaysFiles(string dirName)
        {
            try
            {
                string[] files = Directory.GetFiles(dirName);

                foreach (string file in files)
                {
                    FileInfo fi = new FileInfo(file);
                    if (fi.CreationTime < DateTime.Now.AddDays(-1))
                    {
                        fi.Delete();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private FileInfo GetZipByConfig(string dirName, BrandingConfig config)
        {
            try
            {
                if (config == default(BrandingConfig))
                {
                    return default(FileInfo);
                }


                string[] files = Directory.GetFiles(dirName);
                List<FileInfo> list = new List<FileInfo>();

                foreach (string file in files)
                {
                    FileInfo fi = new FileInfo(file);
                    list.Add(fi);
                }

                FileInfo brandingFile = list.Where(
                    x => x.Name.Contains(config.FolderName) 
                    && x.CreationTime >= DateTime.Now.AddDays(config.Days).AddHours(config.Hours)
                    ).FirstOrDefault();


                return brandingFile;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
