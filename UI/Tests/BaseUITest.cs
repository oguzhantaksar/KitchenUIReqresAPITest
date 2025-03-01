using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using Serilog;
using EasternPeakAutomation.Config;
using System;
using OpenQA.Selenium.Safari;

namespace EasternPeakAutomation.UI.Tests
{
    /// <summary>
    /// Base class for UI tests; handles WebDriver initialization and teardown.
    /// </summary>
    public abstract class BaseUITest
    {
        protected IWebDriver Driver;

        [SetUp]
        public void SetUp()
        {
            Log.Information("Initializing WebDriver for UI tests.");
            string browser = TestConfig.Settings.Browser.ToLower();

            // Only Chrome implemented for now. Additional browsers can be added here.
            if (browser.Contains("chrome"))
            {
                var chromeOptions = new ChromeOptions();
                if (browser.Contains("headless"))
                {
                    chromeOptions.AddArgument("headless");
                }
                Driver = new ChromeDriver(chromeOptions);
            }
            else
            {
                throw new Exception($"Browser not supported: {TestConfig.Settings.Browser}");
            }
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TestConfig.Settings.ImplicitWaitSeconds);
            Log.Information("WebDriver initialized successfully.");
        }

        [TearDown]
        public void TearDown()
        {
            
            if (Driver != null)
            {
                Log.Information("Closing and disposing WebDriver.");
                Driver.Quit();
                Driver.Dispose();
                Driver = null;
            }
        }
    }
}
