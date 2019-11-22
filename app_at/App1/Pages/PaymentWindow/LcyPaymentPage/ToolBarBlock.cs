using Common.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace App1.Pages
{
    /// <summary>
    /// LCY {Base number}
    /// xxx
    /// </summary>
    public class ToolBarBlock : BaseBlock
    {
        private static By _blockBy = By.XPath("//ToolBar[@Name='Available actions']");

        public ToolBarBlock() : base(_blockBy)
        {
        }

        private AppiumWebElement ValidateButton => Block.FindElementByName("Validate");

        private AppiumWebElement ApproveButton => Block.FindElementByName("Approve");

        private AppiumWebElement OnHoldButton => Block.FindElementByName("On-Hold");

        private AppiumWebElement SaveButton => Block.FindElementByName("Save");

        private AppiumWebElement PrintButton => Block.FindElementByName("Print");

        private AppiumWebElement AutoMatchButton => Block.FindElementByName("Auto match");

        private AppiumWebElement DossiersButton => Block.FindElementByName("Dossiers");

        private AppiumWebElement VbkButton => Block.FindElementByName("VBK");

        private AppiumWebElement HistoryDocumentButton => Block.FindElementByName("History of document");
    }
}
