using Common.Instances;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace Common.Elements
{
    /// <summary>
    /// Base Element
    /// </summary>
    public class BaseElement
    {
        protected WindowsDriver<AppiumWebElement> ElementDriver { get; set; }

        protected BaseElement()
        {
            ElementDriver = InstanceManager.CurrentInstance.CurrentSession;
        }
    }
}
