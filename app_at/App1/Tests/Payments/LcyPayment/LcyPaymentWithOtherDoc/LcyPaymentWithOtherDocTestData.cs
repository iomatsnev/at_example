using App1.BusinessObjects;
using App1.Services;
using NUnit.Framework;
using System;
using System.Collections;

namespace App1.Tests.Payments
{
    /// <summary>
    /// TestData for LcyPayment with OtherDocuments Test
    /// </summary>
    class LcyPaymentWithOtherDocTestData
    {
        public static IEnumerable TestData
        {
            get
            {
                yield return GetValidDataObjectsMsk();
                yield return GetValidDataObjectsSpb();
            }
        }

        public static (SubscriberTxnRurApp1 LcyPayment,
                       FlexBccasLog BccasPayment,
                       ContractDossiers ContractDossiers,
                       CoverXml CoverXml,
                       OtherXml OtherXml,
                       BccasValidationErrors BccasValidationLcyTable,
                       Partners Partners)
            GenerateBaseDataObjects()
        {
            int txnId = AutoIncrement.GetNextLcyId();
            SubscriberTxnRurApp1 lcyPayment = new SubscriberTxnRurApp1
            (
               txnId: txnId
            )
            {
                CustRef = "xxx",
                BaseNo = 700007,
                Branch = Branch.Msk,
                Pdmsdn = "xxx",
                RcvDate = DateTime.Now,
                PoReceivedDate = DateTime.Now.Date,
                DebitDate = null,
                Number = "xxx",
                PoDate = DateTime.Now.Date,
                Delivery = "xxx",
                Fld101 = null,
                AmountInWords = "xxx",
                RemInn = "xxx",
                RemKpp = "xxx",
                RemName = "xxx",
                RemAcc = 702515001,
                RemAcc20 = decimal.Parse("xxx"),
                Amount = 100m + txnId + 1.00m,
                RemBankName = "xxx",
                RemBic = "xxx",
                RemBankCorrAcct = 0,
                BeneBankName = "xxx",
                BeneBic = "044525101",
                BeneBankCorrAcct = 0m,
                BeneInn = "xxx",
                BeneKpp = "xxx",
                BeneName = "x",
                BeneAcc = 0m,
                Vop = "01",
                SrokPlat = null,
                Priority = "x",
                Fld104 = null,
                Fld105 = null,
                Fld106 = null,
                Fld107 = null,
                Fld108 = null,
                Fld109 = null,
                Fld110 = null,
                Details = "x",
                ImportedDate = DateTime.Now,
                Suspicious = null,
                VoCode = 99999,
                SpecialCutOff = 'x',
                PirsResponseCode = "xxx",
                PirsResponseDesc = "xxxx",
                TokenNumber = AutoIncrement.GetNextCdToken(),
                SubscriberSystem = "xxx",
                LoadedBy = null,
                LoadedDt = null,
                LoadingErrorReason = null,
                Uip = null,
                UipCheck = null,
                BudgetCheck = null,
            };

            FlexBccasLog bccasPayment = new FlexBccasLog
            (
                tflexRefNo: $"XX{(short)lcyPayment.Branch + 1}XX{DateTime.Now.Ticks.ToString().Substring(18 - 9)}",
                branchNo: (short)lcyPayment.Branch,
                direction: '2',
                postingDate: lcyPayment.RcvDate
            )
            {
                ResidentBaseNumber = (int)lcyPayment.BaseNo,
                SchetNumber = lcyPayment.RemAcc20.ToString(),
                Kodop = lcyPayment.Details.Substring(3, 5),
                Valp = "xxx",
                Sump = lcyPayment.Amount,
                Kods = 643,
                PlatDate = DateTime.Now,
                ReferDate = DateTime.Now,
                Prim = lcyPayment.Details,
                Namep = lcyPayment.RemName,
                Nameb = lcyPayment.BeneBankName,
                Bik = lcyPayment.BeneBic,
                Innp = "xx",
                Rc = -1
            };

            ContractDossiers contractDossiers = new ContractDossiers
            (
               contractNumber: "LCY" + $"{DateTime.Now.Ticks}",
               contractAmount: double.Parse(lcyPayment.Amount.ToString()),
               contractCurrency: 643,
               contractDate: DateTime.Now.Date,
               contractTypeName: "4",
               baseNumber: lcyPayment.BaseNo.ToString()
            );

            CoverXml coverXml = new CoverXml
            (
                msgTyp: "XXX",
                tokenNumber: lcyPayment.TokenNumber,
                submissionDate: DateTime.Now.Date,
                docFlag: 'Y',
                serviceReferenceNumber: "XXX",
                amended: 1,
                branch: Branch.Msk.GetDbValue(),
                baseNo: (int)lcyPayment.BaseNo,
                clientName: "XXX",
                ctcDate: DateTime.Now.Date,
                correctionFlag: '*',
                residentAccountNo: lcyPayment.RemAcc20.ToString(),
                reference: "XXX",
                date: DateTime.Now.Date,
                direction: 'X',
                voCode: "XXXXX",
                currencyCodePayment: bccasPayment.Kods,
                sumPayment: lcyPayment.Amount,
                passportOfDeal: contractDossiers.ContractNumber,
                currencyCodeContract: bccasPayment.Kods,
                sumContract: lcyPayment.Amount,
                termOfDelivery: DateTime.Now.Date,
                comments: "XXX"
                );

            OtherXml otherXml = new OtherXml
            (
                msgTyp: "OTHER_DOC",
                tokenNumber: lcyPayment.TokenNumber,
                submissionDate: DateTime.Now.Date,
                amended: 0,
                branch: Branch.Msk.GetDbValue(),
                baseNo: lcyPayment.BaseNo.ToString(),
                docType: "XXX",
                docDescription: $"XXX {contractDossiers.ContractNumber}"
            );

            BccasValidationErrors bccasValidationLcyErrors = new BccasValidationErrors();
            bccasValidationLcyErrors.AddError(BccasValidationPaymentsError.Error215);
            bccasValidationLcyErrors.AddError(BccasValidationPaymentsError.Error440);
            bccasValidationLcyErrors.AddError(BccasValidationPaymentsError.Error560);


            Partners partner = new Partners(
                nameP: "XXX",
                kods: "643"
              ) { };

            (SubscriberTxnRurApp1 LcyPayment,
                FlexBccasLog BccasPayment,
                ContractDossiers ContractDossiers,
                CoverXml CoverXml,
                OtherXml OtherXml,
                BccasValidationErrors BccasValidationLcyTable,
                Partners Partners
            ) tuple = (
                LcyPayment: lcyPayment,
                BccasPayment: bccasPayment,
                ContractDossiers: contractDossiers,
                CoverXml: coverXml,
                OtherXml: otherXml,
                BccasValidationLcyTable: bccasValidationLcyErrors,
                Partners: partner
            );
            return tuple;
        }

        public static TestCaseData GetValidDataObjectsMsk()
        {
            var tuple = GenerateBaseDataObjects();
            return new TestCaseData(
                          tuple.LcyPayment,
                          tuple.BccasPayment,
                          tuple.ContractDossiers,
                          tuple.CoverXml,
                          tuple.OtherXml,
                          tuple.BccasValidationLcyTable,
                          tuple.Partners
                      );
        }

        public static TestCaseData GetValidDataObjectsSpb()
        {
            var tuple = GenerateBaseDataObjects();

            tuple.LcyPayment.CustRef = "XXXXX";
            tuple.LcyPayment.BaseNo = 0000;
            tuple.LcyPayment.Branch = Branch.Spb;
            tuple.LcyPayment.Number = "XXXX";
            tuple.LcyPayment.RemInn = "XXX";
            tuple.LcyPayment.RemKpp = "XXX";
            tuple.LcyPayment.RemName = "XXX";
            tuple.LcyPayment.RemAcc = 790074009;
            tuple.LcyPayment.RemAcc20 = decimal.Parse("XXX");
            tuple.LcyPayment.RemBankName = "XXX";
            tuple.LcyPayment.RemBic = "XXX";

            BccasValidationErrors bccasValidationLcyErrors = new BccasValidationErrors();
            bccasValidationLcyErrors.AddError(BccasValidationPaymentsError.Error215);
            bccasValidationLcyErrors.AddError(BccasValidationPaymentsError.Error440);
            bccasValidationLcyErrors.AddError(BccasValidationPaymentsError.Error560);
            tuple.BccasValidationLcyTable = bccasValidationLcyErrors;

            return new TestCaseData(
                          tuple.LcyPayment,
                          tuple.BccasPayment,
                          tuple.ContractDossiers,
                          tuple.CoverXml,
                          tuple.OtherXml,
                          tuple.BccasValidationLcyTable,
                          tuple.Partners
                      );
        }
    }
}
