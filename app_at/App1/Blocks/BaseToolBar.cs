using Common.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Block
{
    /// <summary>
    /// Base Tool Bar
    /// </summary>
    public class BaseToolBar : BaseBlock
    {
        private static By _blockBy = By.Name("GridBar");

        public BaseToolBar() : base(_blockBy)
        {
        }

        private AppiumWebElement RefreshButton => Block.FindElementByName("Refresh");

        private AppiumWebElement ToExcelButton => Block.FindElementByName("To Excel");

        private AppiumWebElement ViewButton => Block.FindElementByName("View");

        private AppiumWebElement SplitMergeButton => Block.FindElementByName("Split/Merge");

        public void RefreshGrid() => RefreshButton.Click();

        public void ExportToExcel() => ToExcelButton.Click();

        public void View() => ViewButton.Click();

        public void SplitMerge() => SplitMergeButton.Click();
    }
}
