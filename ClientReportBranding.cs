namespace FieldTool.UI
{
    public class ClientReportBranding
    {
        public ClientReportBranding()
        {
            this.IsConsumersEnergy = false;
            this.IsFirstEnergyOH = false;
        }

        public bool IsConsumersEnergy { get; set; }
        public bool IsFirstEnergyOH { get; set; }
        public string BrandingFilePath { get; set; }
        public string DIIconFilePath { get; set; }
        public string SignOffDisclaimerText { get; set; }
    }
}