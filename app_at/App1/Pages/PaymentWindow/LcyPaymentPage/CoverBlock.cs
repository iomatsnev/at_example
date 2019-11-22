using Common.Elements;
using Common.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System.Collections.ObjectModel;

namespace App1.Pages
{
    /// <summary>
    /// LCY {Base number}
    /// xxx
    /// </summary>
    public class CoverBlock : BaseBlock
    {
        private static By _blockBy = By.XPath("//Window[@AutomationId = 'MatchingForm_Lcy']//Pane[@AutomationId='ctcView']");

        public CoverBlock() : base(_blockBy)
        {
        }

        private AppiumWebElement SenderIdInput => Block.FindElementByXPath("//Edit[@AutomationId = 'txtCustomerID']");

        private AppiumWebElement SenderNameInput => Block.FindElementByXPath("//Edit[@AutomationId = 'txtCustomerName']");

        private AppiumWebElement BranchInput => Block.FindElementByXPath("//Edit[@AutomationId = 'txtBranchName']");

        private AppiumWebElement ReferencesInput => Block.FindElementByXPath("//Edit[@AutomationId = 'txtCustRef']");

        private AppiumWebElement CdTokenInput => Block.FindElementByXPath("//Edit[@AutomationId='txtCdToken']");

        private AppiumWebElement QueueNameInput => Block.FindElementByXPath("//Edit[@AutomationId='txtQueueName']");

        private AppiumWebElement ReceivedInput => Block.FindElementByXPath("//Pane[@AutomationId='dtRcvDate']");

        private AppiumWebElement GroupNameInput => Block.FindElementByXPath("//Edit[@AutomationId='txtGroupName']");

        private AppiumWebElement ClientNameInput => Block.FindElementByXPath("//Edit[@AutomationId='txtClientName']");

        private AppiumWebElement PassNameInput => Block.FindElementByXPath("//Edit[@AutomationId='txtPassportName']");

        private AppiumWebElement CpNameInput => Block.FindElementByXPath("//Edit[@AutomationId='txtCpName']");

        private AppiumWebElement CpNameButton => Block.FindElementByXPath("//Edit[@AutomationId='txtCpName']/Button[@Name='Open']");

        private AppiumWebElement ContractNoInput => Block.FindElementByXPath("//Edit[@AutomationId='contractNoEditValue']");

        private AppiumWebElement ContractDateInput => Block.FindElementByXPath("//Pane[@AutomationId='contractDateEditValue']");

        private AppiumWebElement ContractCodeInput => Block.FindElementByXPath("//Edit[@AutomationId='cpCountryCodeEditValue']");

        private AppiumWebElement AmountInput => Block.FindElementByXPath("//Edit[@AutomationId='contractAmountEditValue']");

        private AppiumWebElement ContractCcuInput => Block.FindElementByXPath("//Edit[@AutomationId='contractCcyEditValue']");

        private AppiumWebElement CcuClauseInput => Block.FindElementByXPath("//Edit[@AutomationId='ccyReservationEditValue']");

        private AppiumWebElement CountryCodeInput => Block.FindElementByXPath("//Edit[@AutomationId='cpCountryCodeEditValue']");

        private AppiumWebElement DebtsInput => Block.FindElementByXPath("//Edit[@AutomationId='debtsAmountEditValue']");

        private AppiumWebElement PodSignedInput => Block.FindElementByXPath("//Pane[@AutomationId='podDateEditValue']");

        private AppiumWebElement PodCloseInput => Block.FindElementByXPath("//Pane[@AutomationId='podClosedEditValue']");

        private AppiumWebElement CommentsInput => Block.FindElementByXPath("//Edit[@AutomationId='txtPrim']");

        private AppiumWebElement MessageTextInput => Block.FindElementByXPath("//Pane[@AutomationId='msgPanel']" +
            "/Document[@AutomationId = 'memoMessage']");

        public ReadOnlyCollection<AppiumWebElement> HeaderPanel => Block.FindElementsByXPath("//Pane[@AutomationId='ctcView']" +
            "//Table[@AutomationId='VerticalGrid']/Custom[@Name = 'Header Panel']");

        public Table Table => new Table(By.XPath("//Pane[@AutomationId='ctcView']//Table[@AutomationId='VerticalGrid']"));
    }
}
