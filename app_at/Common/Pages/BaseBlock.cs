using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace Common.Pages
{
    /// <summary>
    /// Base Block
    /// </summary>
    public class BaseBlock : BasePage
    {
        private By _blockBy;
        private AppiumWebElement _block;

        protected AppiumWebElement Block
        {
            get => _block;
            set => _block = value;
        }

        public BaseBlock(By blockBy)
        {
            _blockBy = blockBy;
            _block = PageDriver.FindElement(_blockBy);
        }
    }
}
