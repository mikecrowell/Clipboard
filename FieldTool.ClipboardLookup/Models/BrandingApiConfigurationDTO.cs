using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FieldTool.ClipboardLookup.Models
{
    public class BrandingConfig
    {
        public string FolderName { get; set; }
        public List<string> ClientName { get; set; }
        public bool CacheBrandingFiles { get; set; }
        public int Days { get; set; }
        public int Hours { get; set; }
    }
    

}