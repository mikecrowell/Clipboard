using System;
using System.Collections.Generic;

namespace FieldTool.ClipboardLookup.Models
{
    public class UploadXmlBackupDTO
    {
        public Uri Url { get; set; }
        public List<string> Errors { get; set; }

        public UploadXmlBackupDTO()
        {
        }
    }
}