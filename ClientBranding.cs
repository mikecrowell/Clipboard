namespace FieldTool.UI
{
    public class ClientBranding
    {
        public ClientBranding()
        {
            this.HasValidRGB = false;
        }

        public string ClientAccountId { get; set; }
        public string ClientAccountName { get; set; }
        public string SystemPath { get; set; }
        public string FolderName { get; set; }
        public int ColorValueRed { get; set; }
        public int ColorValueGreen { get; set; }
        public int ColorValueBlue { get; set; }
        public string BaseColor { get; set; }
        public string ImageFileName { get; set; }
        public bool HasValidRGB { get; set; }
    }
}