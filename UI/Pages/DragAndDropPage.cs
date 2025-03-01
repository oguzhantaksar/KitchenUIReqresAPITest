using EasternPeakAutomation.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Serilog;
using System;

namespace EasternPeakAutomation.UI.Pages
{
    /// <summary>
    /// Page class for the Drag & Drop functionality.
    /// </summary>
    public class DragAndDropPage : BasePage
    {
        public DragAndDropPage(IWebDriver driver) : base(driver) { }

        // Locator for the order ticket area where items are dropped.
        public IWebElement OrderTicketPlate => Driver.FindElement(By.Id("plate-items"));
        
        // Locator for the first element inside the order ticket area.
        public IWebElement OrderTicketElement => Driver.FindElement(By.CssSelector("#plate-items li"));

        /// <summary>
        /// Gets the text of the menu item element.
        /// </summary>
        public string GetMenuElementText(string menuItem)
        {
            Log.Information($"Locating menu item '{menuItem}'.");
            return Driver.FindElement(By.Id("menu-" + menuItem)).Text.Trim();
        }

        /// <summary>
        /// Retrieves the text from the order ticket element.
        /// </summary>
        public string GetOrderTicketElementText()
        {
            return OrderTicketElement.Text.Trim();
        }

        /// <summary>
        /// Performs the drag and drop action for a given menu item.
        /// </summary>
        public void PerformDragAndDrop(string menuItem, IWebElement target)
        {
            IWebElement source = Driver.FindElement(By.Id("menu-" + menuItem));

            try
            {
                Log.Information("Attempting drag & drop using Actions.");
                Actions actions = new Actions(Driver);
                actions.DragAndDrop(source, target).Build().Perform();
                Log.Information("Drag & drop via Actions succeeded.");
            }
            catch(Exception ex)
            {
                Log.Error("Actions drag & drop failed: " + ex.Message);
                // Fallback to JavaScript drag and drop.
                DragAndDropUsingJS(source, target);
            }
            
            string orderTicketText = GetOrderTicketElementText();
            if (orderTicketText.Equals("Drag an item from the menu to start your order!"))
            {
                Log.Warning("Order ticket text not updated. Falling back to JavaScript drag & drop.");
                DragAndDropUsingJS(source, target);
            }
            else
            {
                Log.Information("Order ticket updated with text: {OrderTicketText}", orderTicketText);
            }
        }

        /// <summary>
        /// Fallback method: Performs drag and drop using JavaScript if native actions fail.
        /// </summary>
        private void DragAndDropUsingJS(IWebElement source, IWebElement target)
        {
            Log.Information("Performing drag & drop using JavaScript.");
            string script = @"
                function createEvent(typeOfEvent) {
                    var event = document.createEvent('CustomEvent');
                    event.initCustomEvent(typeOfEvent, true, true, null);
                    event.dataTransfer = {
                        data: {},
                        setData: function(key, value) {
                            this.data[key] = value;
                        },
                        getData: function(key) {
                            return this.data[key];
                        }
                    };
                    return event;
                }
                function dispatchEvent(element, event, transferData) {
                    if (transferData !== undefined) {
                        event.dataTransfer = transferData;
                    }
                    if (element.dispatchEvent) {
                        element.dispatchEvent(event);
                    } else if (element.fireEvent) {
                        element.fireEvent('on' + event.type, event);
                    }
                }
                function simulateHTML5DragAndDrop(element, target) {
                    var dragStartEvent = createEvent('dragstart');
                    dispatchEvent(element, dragStartEvent);
                    var dropEvent = createEvent('drop');
                    dispatchEvent(target, dropEvent, dragStartEvent.dataTransfer);
                    var dragEndEvent = createEvent('dragend');
                    dispatchEvent(element, dragEndEvent, dropEvent.dataTransfer);
                }
                simulateHTML5DragAndDrop(arguments[0], arguments[1]);";
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)Driver;
            jsExecutor.ExecuteScript(script, source, target);
            Log.Information("JavaScript drag & drop executed successfully.");
        }
    }
}
