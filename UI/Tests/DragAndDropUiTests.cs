using EasternPeakAutomation.UI.Helper;
using EasternPeakAutomation.UI.Pages;
using Serilog;
using OpenQA.Selenium;
using EasternPeakAutomation.Utils;

namespace EasternPeakAutomation.UI.Tests
{
    /// <summary> 
    /// UI tests for verifying the Drag & Drop functionality on the Kitchen site.
    /// </summary>
    [TestFixture]
    public class DragAndDropUiTests : BaseUITest
    {
        [Test]
        [TestCase("fried-chicken")]
        [TestCase("hamburger")]
        [TestCase("ice-cream")]
        public void VerifyDragAndDropFunctionality(string menuItem)
        {
            Log.Information($"Starting Drag & Drop test for menu item: '{menuItem}'.");
            
            // Navigate to the Drag & Drop section
            DragAndDropPage dndPage = new KitchenHomePage(Driver).Navigate().GoToDragAndDropSection();
            dndPage.PerformDragAndDrop(menuItem, dndPage.OrderTicketPlate);
            string orderTicketText = dndPage.GetOrderTicketElementText();
            
            Log.Information($"Order Ticket content after drop: {orderTicketText}");
            Assert.Multiple(() =>
            {
                Assert.That(orderTicketText, Is.EqualTo(dndPage.GetMenuElementText(menuItem)),
                    $"Expected text for '{menuItem}' not found. Actual: '{orderTicketText}'");
            });

            
            var (baselinePath, currentPath) = ImageComparisonHelper.GetScreenshotPaths(menuItem);

            // Capture screenshot for visual verification.
            Screenshot elementScreenshot = ((ITakesScreenshot)dndPage.OrderTicketPlate).GetScreenshot();
            elementScreenshot.SaveAsFile(currentPath);
            Log.Information($"Screenshot of Order Ticket Plate saved at: {currentPath}");

            
            bool imagesMatch = ImageComparisonHelper.CompareImages(baselinePath, currentPath);
            Assert.That(imagesMatch,Is.True,"The visual differences exceed the tolerance.");
            
            Log.Information($"Drag & Drop test for '{menuItem}' completed successfully.");
        }
    }
}