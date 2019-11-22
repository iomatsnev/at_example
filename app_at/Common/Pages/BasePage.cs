using Common.Drivers;
using Common.Instances;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using System.Linq;
using System.Threading;

namespace Common.Pages
{
    /// <summary>
    /// Base Page
    /// </summary>
    public class BasePage
    {
        protected DriverSessions PageSessions { get; }
        protected WindowsDriver<AppiumWebElement> PageDriver { get; set; }

        protected BasePage()
        {
            PageSessions = InstanceManager.CurrentInstance.Sessions;
            PageDriver = InstanceManager.CurrentInstance.Sessions.Current;
        }

        protected BasePage(Instance currentInstance)
        {
            PageSessions = currentInstance.Sessions;
            PageDriver = currentInstance.Sessions.Current;
        }

        public string GetPageTitle(AppiumWebElement Title)
        {
            return Title.GetAttribute("Name");
        }
    }
}
