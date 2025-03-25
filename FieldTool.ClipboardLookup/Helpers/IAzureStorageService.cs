using FieldTool.Constants.Logging;
using System;

namespace FieldTool.ClipboardLookup.Helpers
{
    public interface IAzureStorageService
    {
        ILogger Logger { get; set; }

        Uri SaveReportFile(string type, string programId, string accountId, string projectId, string filename, System.IO.Stream pdfStream);

        Uri SaveUploadBackupXmlFile(string type, string programId, string accountId, string projectId, System.IO.Stream xmlStream);
    }
}