using System.IO;

namespace FieldTool.ClipboardLookup.Helpers
{
    public class EmailMessage
    {
        public EmailMessage(string emailSubject, string emailBody, string emailTo, string attachmentFileName = "", string emailFrom = "", string emailFromDisplayName = "")
        {
            this.emailSubject = emailSubject;
            this.emailBody = emailBody;
            this.emailTo = emailTo;
            this.attachmentFileName = attachmentFileName;
            this.emailFrom = emailFrom;
            this.emailFromDisplayName = emailFromDisplayName;
        }

        public string emailSubject { get; set; }
        public string emailBody { get; set; }
        public string emailTo { get; set; }
        public string attachmentFileName { get; set; }
        public string attachmentFilePath { get; set; }
        public string emailFrom { get; set; }
        public string emailFromDisplayName { get; set; }

        public bool AttachmentExists
        {
            get { return File.Exists(this.attachmentFilePath); }
        }
    }
}