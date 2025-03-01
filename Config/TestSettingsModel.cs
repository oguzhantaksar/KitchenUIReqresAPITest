namespace EasternPeakAutomation.Config
{
    /// <summary>
    /// Represents the test settings loaded from configuration.
    /// </summary>
    public class TestSettingsModel
    {
        public int ApiTimeout { get; set; }
        public string Browser { get; set; }
        public int ImplicitWaitSeconds { get; set; }
        public string Environment { get; set; } 
        public string BaseUrlApi { get; set; }
        public string BaseUrlUi { get; set; }
    }
}