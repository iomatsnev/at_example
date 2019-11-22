using Common.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace App1.Pages
{
    /// <summary>
    /// LCY {Base number}
    /// xxx
    /// </summary>
    public class LcyBlock : BaseBlock
    {
        private static By _blockBy = By.XPath("//Window[@AutomationId = 'MatchingForm_Lcy']//Pane[@AutomationId='scMainPayment']");

        public LcyBlock() : base(_blockBy)
        {
        }

        private AppiumWebElement SenderIdInput => Block.FindElementByXPath("//Edit[@AutomationId = 'txtCustomerID']");

        private AppiumWebElement SenderNameInput => Block.FindElementByXPath("//Edit[@AutomationId = 'txtCustomerName']");

        private AppiumWebElement BranchInput => Block.FindElementByXPath("//Edit[@AutomationId = 'txtBranchName']");

        private AppiumWebElement ReferencesInput => Block.FindElementByXPath("//Edit[@AutomationId = 'txtCustRef']");

        private AppiumWebElement CdTokenInput => Block.FindElementByXPath("//Edit[@AutomationId='txtCdToken']");

        private AppiumWebElement PdmInput => Block.FindElementByXPath("//Edit[@AutomationId='txtPDM']");

        private AppiumWebElement ReceivedInput => Block.FindElementByXPath("//Pane[@AutomationId='dtRcvDate']");

        private AppiumWebElement SuspiciousInput => Block.FindElementByXPath("//Edit[@AutomationId = 'txtSuspicious']");

        private AppiumWebElement PaymentNumberInput => Block.FindElementByXPath("//Edit[@AutomationId = 'txtPaymentNumber']");

        private AppiumWebElement PoDateInput => Block.FindElementByXPath("//Pane[@AutomationId='dtPoDate']");

        private AppiumWebElement NoDocumentsCheckBox => Block.FindElementByXPath("//CheckBox[@AutomationId='chkNoDocuments']");

        private AppiumWebElement RemNameInput => Block.FindElementByXPath("//CheckBox[@AutomationId='chkNoDocuments']");

        private AppiumWebElement AmountInput => Block.FindElementByXPath("//Edit[@AutomationId = 'txtAmount']");

        private AppiumWebElement RemAccInput => Block.FindElementByXPath("//Edit[starts-with(@AutomationId,'txtRemAcc')]");

        private AppiumWebElement BeneBankInput => Block.FindElementByXPath("//Edit[@AutomationId='txtBeneBank']");

        private AppiumWebElement BeneBicInput => Block.FindElementByXPath("//Edit[@AutomationId='txtBeneBic']");

        private AppiumWebElement BeneBankCorrAccInput => Block.FindElementByXPath("//Edit[@AutomationId='txtBeneBankCorrAcc']");

        private AppiumWebElement BeneNameInput => Block.FindElementByXPath("//Edit[@AutomationId='txtBeneName']");

        private AppiumWebElement BeneAccInput => Block.FindElementByXPath("//Edit[@AutomationId='txtBeneAcc']");

        private AppiumWebElement VoCodeInput => Block.FindElementByXPath("//Edit[@AutomationId='txtVoCode']");

        private AppiumWebElement OtherDetailsInput => Block.FindElementByXPath("//Edit[@AutomationId='txtOtherDetails']");

        private AppiumWebElement PaymentMetadataTable => Block.FindElementByXPath("//Table[@AutomationId = 'VerticalGrid']" +
            "/Custom[@Name = 'Header Panel']");
    }
}


