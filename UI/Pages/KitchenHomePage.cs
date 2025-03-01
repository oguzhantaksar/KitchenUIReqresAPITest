using EasternPeakAutomation.Config;
using Serilog;
using OpenQA.Selenium;

namespace EasternPeakAutomation.UI.Pages
{
    /// <summary>
    /// Page class for the Kitchen homepage.
    /// </summary>
    public class KitchenHomePage : BasePage
    {
        // Locator for navigating to the Drag & Drop section.
        private readonly By dragAndDropLinkLocator = By.CssSelector("[href='/ingredients/drag-and-drop']");

        public KitchenHomePage(IWebDriver driver) : base(driver) { }
        
        /// <summary>
        /// Navigates to the Kitchen homepage.
        /// </summary>
        public KitchenHomePage Navigate()
        {
            Log.Information("Navigating to Kitchen homepage.");
            try 
            {
                Driver.Navigate().GoToUrl(TestConfig.Settings.BaseUrlUi);
                Log.Information("Navigation to Kitchen homepage successful.");
            }
            catch(Exception ex)
            {
                Log.Error("Error during navigation: " + ex.Message);
                throw;
            }
            return this;
        }

        /// <summary>
        /// Navigates to the Drag & Drop section from the homepage.
        /// </summary
        public DragAndDropPage GoToDragAndDropSection()
        {
            Log.Information("Navigating to Drag & Drop section.");
            Click(dragAndDropLinkLocator);
            return new DragAndDropPage(Driver);
        }
    }
}