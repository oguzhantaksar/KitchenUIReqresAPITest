using Serilog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace EasternPeakAutomation.UI.Pages;

    /// <summary>
    /// Base page class containing common functionality for all UI pages.
    /// </summary>
    public abstract class BasePage
    {
        protected readonly IWebDriver Driver;

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
            Log.Information($"BasePage initialized for driver: {driver.GetType().Name}");
        }
        
        /// <summary>
        /// Waits for an element to be present in the DOM. It can be used for dynamic elements in the future.
        /// </summary>
        protected IWebElement WaitForElement(By locator, TimeSpan timeout)
        {
            WebDriverWait wait = new WebDriverWait(Driver, timeout);
            return wait.Until(driver => driver.FindElement(locator));
        }
        
        /// <summary>
        /// Waits for an element to be clickable.
        /// </summary>
        protected IWebElement WaitForElementClickable(By locator, TimeSpan timeout)
        {
            WebDriverWait wait = new WebDriverWait(Driver, timeout);
            return wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        /// <summary>
        /// Clicks on the element located by the given locator.
        /// </summary>
        public void Click(By locator)
        {
            try 
            {
                var element = WaitForElementClickable(locator, TimeSpan.FromSeconds(10));
                element.Click();
                Log.Information($"Clicked element: {locator}");
            }
            catch(Exception ex)
            {
                Log.Error($"Error clicking element {locator}: {ex.Message}");
                throw;
            }
        }
    }
