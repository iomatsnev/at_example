using Common.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System.Collections.ObjectModel;

namespace App1.Pages
{
    /// <summary>
    /// LCY {Base number}
    /// </summary>
    public class GridBarBlock : BaseBlock
    {
        private static By _blockBy = By.XPath("//Window[@AutomationId = 'MatchingForm_Lcy']//ToolBar[@Name='GridBar']");

        public GridBarBlock() : base(_blockBy)
        {
        }

        private AppiumWebElement RefreshButton => Block.FindElementByName("Refresh");

        private AppiumWebElement ToExcelButton => Block.FindElementByName("To Excel");

        private AppiumWebElement PrintSetButton => Block.FindElementByName("Print Set");

        private AppiumWebElement PrintPreviewButton => Block.FindElementByName("Print Preview");

        private AppiumWebElement MatchButton => Block.FindElementByName("Match");

        private AppiumWebElement BreakButton => Block.FindElementByName("Break");

        private AppiumWebElement ChangeTypeButton => Block.FindElementByName("Change type");

        private AppiumWebElement SendPirsButton => Block.FindElementByName("Send back to PIRS");

        private AppiumWebElement ChangeBaseNumberButton => Block.FindElementByName("Change Base Number");

        private AppiumWebElement CreateNewCoverButton => Block.FindElementByName("Create New Cover");

        private AppiumWebElement DeleteCoverButton => Block.FindElementByName("Delete Cover");

        private AppiumWebElement HeaderPanel => Block.FindElementByXPath("//Pane[@AutomationId='matchingCtcGrid']//Custom[@Name = 'Header Panel']");

        private ReadOnlyCollection<AppiumWebElement> AllSeverityItems => Block.FindElementsByXPath("//Custom[@Name='Data Panel']" +
            "/Custom[starts-with(@Name,'Row')]/DataItem[starts-with(@Name, 'Severity row')]");
    }
}
