using App1.Pages;
using NUnit.Framework;

namespace App1.Tests.Payments.Lcy
{
    /// <summary>
    /// XXX
    /// </summary>
    [TestFixtureSource(typeof(LcyPaymentWithOtherDocTestData), nameof(LcyPaymentWithOtherDocTestData.TestData))]
    [Category("LcyTests")]
    [Category("Payments")]
    public class LcyPaymentWithOtherDocTest : App1BaseTest
    {
        private readonly SubscriberTxnRurApp1 _expectedLcyPayment;
        private readonly FlexBccasLog _expectedBccasPayment;
        private readonly ContractDossiers _expectedContractDossiers;
        private readonly CoverXml _expectedCoverXml;
        private readonly OtherXml _expectedOtherXml;
        private readonly BccasValidationErrors _expectedBccasValidationErrors;
        private readonly Partners _expectedPartners;

        public LcyPaymentWithOtherDocTest(TestCaseData testCaseData)
        {
            _expectedLcyPayment = (SubscriberTxnRurApp1)testCaseData.Arguments[0];
            _expectedBccasPayment = (FlexBccasLog)testCaseData.Arguments[1];
            _expectedContractDossiers = (ContractDossiers)testCaseData.Arguments[2];
            _expectedCoverXml = (CoverXml)testCaseData.Arguments[3];
            _expectedOtherXml = (OtherXml)testCaseData.Arguments[4];
            _expectedBccasValidationErrors = (BccasValidationErrors)testCaseData.Arguments[5];
            _expectedPartners = (Partners)testCaseData.Arguments[6];
        }

        [Test]
        public void LcyPaymentWithOtherDocTestSteps()
        {
            //
            // Precondition steps
            //
            Logger.Info("Precondition steps: Write LCY payment To Payment Db");
            _expectedLcyPayment.WriteToPaymentDb();

            Logger.Info("Precondition steps: Write BCASS payment To Dwh database");
            _expectedBccasPayment.WriteToDwhDb();

            Logger.Info("Precondition steps: Export Payment To BCCAS");
            PaymentExporter.ExportPaymentToBccas(_expectedBccasPayment.PostingDate);

            Logger.Info("Precondition steps: Add CoverXml to App1");
            Services.Documents.AddDocumentToApp1WithSignature(_expectedCoverXml.GetXml());
            Logger.Debug($"Precondition steps: CoverXml {_expectedCoverXml.GetXmlString()}");

            Payment expectedPayment = new Payment(_expectedBccasPayment, _expectedCoverXml);
            OtherContract expectedContract = new OtherContract(_expectedOtherXml);

            Logger.Info("Precondition steps: Add OtherDocument to APP");
            DownloadDocumentGreenTrade.AddDocumentToGreenTrade(_expectedCoverXml.TokenNumber);
            Services.Documents.AddDocumentToApp1WithSignature(_expectedOtherXml.GetXml());

            //
            // Steps
            //
            Logger.Info("Step: Compliance Check and Approve the contract to send it to All contract queue");
            OtherProcessingPage otherProcessingPage = new App1MainPage(InstanceManager.GetInstance(InstanceType.Maker))
                .OpenOtherProcessingPage();

            string barCodeOtherDoc = otherProcessingPage.GetBarCodeValue(_expectedOtherXml.TokenNumber);
            otherProcessingPage
                .OpenOtherContractByTokenValue(_expectedOtherXml.TokenNumber)
                .ComplianceCheckWithRadioButtonValue()
                .Close();

            Logger.Info("Step: Attach Cover , Validate, Approve payment and sent it to XXX queue");
            BccasValidationErrors actualBccasValidationErrors;
            new App1MainPage(InstanceManager.GetInstance(InstanceType.Maker)).OpenCcuProcessingLcyPage()
                .OpenLcyPayment(_expectedLcyPayment.TokenNumber)
                .VerificationCover(_expectedCoverXml.TokenNumber)
                .AssingDossiers()
                .OpenFindBccasPage()
                .AttachDossier(_expectedContractDossiers.ContractNumber)
                .OpenOtherTab()
                .AttachOtherDoc(barCodeOtherDoc)
                .CloseAssignLcyWindow()
                .ValidateLcy(out actualBccasValidationErrors)
                .VerificationCover(_expectedLcyPayment.TokenNumber)
                .CheckCriticalSeverity()
                .ApproveLcy();


            Logger.Info("Step: Open Ccu-Ift -> Reporting -> LCY ->  LCY payment by CDToken");
            new App1MainPage(InstanceManager.GetInstance(InstanceType.Checker)).OpenCcuReportingLcyPage()
                .ValidateAndUploadToBccas(_expectedLcyPayment.TokenNumber);

            string barCodeCover = Services.Documents.GetBarCodeValueByToken(_expectedCoverXml.TokenNumber);
            Logger.Info("Step: Open unique contract queue by Bar code value -> Open document in UC tab");
            LcyPaymentPage lcyInUniqueContract = new App1MainPage(InstanceManager.GetInstance(InstanceType.Checker))
                    .OpenContractPage()
                    .FindDocAndOpenIt(barCodeCover)
                    .OpenOther(_expectedContractDossiers.ContractNumber)
                    .OpenLcy(_expectedCoverXml.TokenNumber);

            Logger.Info("Step: Read Fcy Payment from UI");
            Payment actualUiPayment = lcyInUniqueContract.ReadFromUi(ContractTypes.Contract.ToString());

            Logger.Info("Step: Read actual Database payment");
            Payment actualDbPayment = Service.Database.Oplata.ReadFromDb(_expectedBccasPayment.TflexRefNo, _expectedLcyPayment.Branch);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(DataTableUtils.TablesAreTheSame(actualBccasValidationErrors, _expectedBccasValidationErrors),
                   "Actual BCCAS Validation error list is not equal to Expected list");
                Assert.AreEqual(expectedPayment, actualUiPayment, "Actual UI Payment is not equal to Expected payment");
                Assert.AreEqual(expectedPayment, actualDbPayment, "Actual Db Payment is not equal to Expected payment");
            });
        }
    }
}