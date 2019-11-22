using Common.Elements;
using Common.Pages;
using App1.BusinessObjects;
using OpenQA.Selenium.Appium;
using System;
using System.Collections.ObjectModel;

namespace App1.Pages
{
    /// <summary>
    /// LCY {Base number}
    /// xxx
    /// </summary>
    public class LcyPaymentPage : BasePage
    {
        private ToolBarBlock ToolBar => new ToolBarBlock();
        private LcyBlock LcyPaymentBlock => new LcyBlock();
        private CoverBlock CoverPaymentBlock => new CoverBlock();
        private GridBarBlock GridBar => new GridBarBlock();

        #region  Windows
        private AppiumWebElement LcyWindow => PageDriver.FindElementByAccessibilityId("MatchingForm_Lcy");
        private AppiumWebElement LcyWindowInDossiers => PageDriver.FindElementByAccessibilityId("_DossierQueuesViewer");
        private AppiumWebElement ApproveWindow => LcyWindow.FindElementByXPath("//Window[@Name = 'Approve']");
        private AppiumWebElement CoverPane => LcyWindow.FindElementByAccessibilityId("ctcView");
        private AppiumWebElement BccasPane => LcyWindow.FindElementByName("BCCAS Validation");
        private AppiumWebElement BccasRainbowPane => LcyRainbowWindow.FindElementByName("BCCAS Validation");
        private AppiumWebElement LcyRainbowWindow => PageDriver.FindElementByAccessibilityId("MatchingForm_RainbowLcy");
        private AppiumWebElement OnHoldWindow => LcyWindow.FindElementByAccessibilityId("RejectionForm");
        #endregion

        #region  Elements
        private AppiumWebElement CoversTable => LcyWindow.FindElementByAccessibilityId("grid");
        private Table Table => new Table(CoversTable);
        private AppiumWebElement SetTab => LcyWindowInDossiers.FindElementByXPath("//Group[starts-with(@Name, 'Set')]");
        private AppiumWebElement MetadataTable => LcyWindowInDossiers.FindElementByAccessibilityId("VerticalGrid");
        private AppiumWebElement MessageGroup => LcyWindowInDossiers.FindElementByAccessibilityId("memoMessage");
        private AppiumWebElement CoverCdTokenInput => CoverPane.FindElementByAccessibilityId("txtCdToken");
        private AppiumWebElement BccasTable => BccasPane.FindElementByAccessibilityId("grid");
        private Table BccasValidationTable => new Table(BccasTable);
        private AppiumWebElement BccasRainbowTable => BccasRainbowPane.FindElementByAccessibilityId("grid");
        private Table BccasRainbowValidationTable => new Table(BccasRainbowTable);
        private AppiumWebElement ReasonsOnHoldTable => OnHoldWindow.FindElementByAccessibilityId("GR_RejectionReasons");
        private ReadOnlyCollection<AppiumWebElement> OtherReasons
            => ReasonsOnHoldTable.FindElementsByXPath("//DataItem[starts-with(@Name, 'Reason row')]");
        #endregion

        #region  Buttons
        private AppiumWebElement ValidateButton => LcyWindow.FindElementByName("Validate");
        private AppiumWebElement ApproveButton => LcyWindow.FindElementByName("Approve");
        private AppiumWebElement OnHoldButton => LcyWindow.FindElementByName("On-Hold");
        private AppiumWebElement ValidateRainbowButton => LcyRainbowWindow.FindElementByName("Validate");
        private AppiumWebElement ApproveRainbowButton => LcyRainbowWindow.FindElementByName("Approve");
        private AppiumWebElement DossiersButton => LcyWindow.FindElementByName("Dossiers");
        private AppiumWebElement CloseDossierButton => LcyWindowInDossiers.FindElementByName("Close");
        private AppiumWebElement CloseLcyButton => LcyWindow.FindElementByName("Close");
        private AppiumWebElement OnHoldSaveButton => OnHoldWindow.FindElementByName("Save");
        #endregion

        #region Fields
        #region FirstTab
        private AppiumWebElement BaseNo => LcyWindowInDossiers.FindElementByAccessibilityId("txtCustomerID");
        private AppiumWebElement ClientName => LcyWindowInDossiers.FindElementByAccessibilityId("txtCustomerName");
        private AppiumWebElement SchetNumber => LcyWindowInDossiers.FindElementByAccessibilityId("txtRemAcc20");
        private AppiumWebElement PlatDate => LcyWindowInDossiers.FindElementByAccessibilityId("dtPoDate");
        private AppiumWebElement Prim => LcyWindowInDossiers.FindElementByAccessibilityId("txtOtherDetails");
        private AppiumWebElement NameB => LcyWindowInDossiers.FindElementByAccessibilityId("txtBeneBank");
        private AppiumWebElement Bik => LcyWindowInDossiers.FindElementByAccessibilityId("txtBeneBic");
        #endregion

        #region SecondTab
        private AppiumWebElement Direction => MetadataTable.FindElementByName("Character of the payment Record 0");
        private AppiumWebElement CtcDate => MetadataTable.FindElementByName("Cover date Record 0");
        private AppiumWebElement VoCode => MetadataTable.FindElementByName("VO Code Record 0");
        private AppiumWebElement CurrencyCodePayment => MetadataTable.FindElementByName("Payment CCY Record 0");
        private AppiumWebElement SumPayment => MetadataTable.FindElementByName("Payment Amount Record 0");
        private AppiumWebElement PassportOfDealContract => MetadataTable.FindElementByName("Contract number Record 0");
        private AppiumWebElement PassportOfDeal => MetadataTable.FindElementByName("Contract unique number Record 0");
        private AppiumWebElement CurrencyCodeContract => MetadataTable.FindElementByName("Contract CCY Record 0");
        private AppiumWebElement SumContract => MetadataTable.FindElementByName("Contract Amount Record 0");
        private AppiumWebElement TermOfDelivery => MetadataTable.FindElementByName("Term of Delivery Record 0");
        private AppiumWebElement ReferDate => MetadataTable.FindElementByName("Reference Date Record 0");
        private AppiumWebElement ReferNumber => MetadataTable.FindElementByName("Reference No. Record 0");
        #endregion
        #endregion

        public AssignToDossierPage AssingDossiers()
        {
            DossiersButton.Click();
            return new AssignToDossierPage();
        }

        public LcyPaymentPage VerificationCover(string cdToken)
        {
            if (CoverCdTokenInput.Text != cdToken)
            {
                throw new InvalidOperationException("Cover not attached");
            }
            return this;
        }

        public LcyPaymentPage CloseLcyPage()
        {
            CloseLcyButton.Click();
            return this;
        }

        public LcyPaymentPage ValidateLcy()
        {
            ValidateButton.Click();
            return this;
        }

        public LcyPaymentPage ValidateLcy(out BccasValidationErrors bccasValidationErrors)
        {
            ValidateButton.Click();
            bccasValidationErrors = new BccasValidationErrors(BccasValidationTable.ReadTableToSet());
            return this;
        }

        public LcyPaymentPage ApproveLcy()
        {
            ApproveButton.Click();
            return this;
        }

        public LcyPaymentPage SendToOnHoldQueue()
        {
            OnHoldButton.Click();

            Table table = new Table(ReasonsOnHoldTable);
            table.ClickOnCell("Is Selected", numberOfCell);
            table.SetCellValue("Details", numberOfCell, "Тестовая причина");
            OnHoldSaveButton.Click();
            return new LcyPaymentPage();
        }

        public Payment ReadPayment(string type)
        {
            Payment payment = new Payment();

            payment.SchetNumber = SchetNumber.Text;
            payment.PlatDate = DateTime.Parse(PlatDate.Text);
            payment.Prim = Prim.Text;
            payment.Namep = ClientName.Text;
            payment.Nameb = NameB.Text;
            payment.Bik = Bik.Text;
            payment.Kods = short.Parse(SchetNumber.Text.Substring(5, 3));
            payment.CtcDate = DateTime.Parse(CtcDate.Text);
            payment.BaseNo = int.Parse(BaseNo.Text);
            payment.ClientName = ClientName.Text;
            payment.VoCode = VoCode.Text;
            payment.CurrencyCodePayment = int.Parse(CurrencyCodePayment.Text);
            payment.SumPayment = decimal.Parse(SumPayment.Text);
            payment.PassportOfDeal = PassportOfDeal.Text;
            payment.CurrencyCodeContract = short.Parse(CurrencyCodeContract.Text);
            payment.TermOfDelivery = DateTime.Parse(TermOfDelivery.Text);
            payment.SumContract = decimal.Parse(SumContract.Text);
            payment.ReferDate = DateTime.Parse(ReferDate.Text);
            payment.Direction = char.Parse(Direction.Text);

            return payment;
        }
    }
}
