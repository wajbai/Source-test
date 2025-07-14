/*  Class Name      : EnumActivityDataCommand.cs
 *  Purpose         : Enum Data type for Indetifying SQL Statement from UI request
 *  Author          : CS
 *  Created on      : 02-Aug-2010
 */

namespace Bosco.Report.SQL
{
    public class ReportSQLCommand
    {
        public enum Report
        {
            AccountYear,
            ACYearSignDetails,
            MonthlyAbstract,
            MultiAbstract,
            MultiAbstractQuaterly,
            MultiAbstractQuaterlyWithCC,
            MultiAbstractYear,
            MultiAbstractYearWithCC,
            MultiAbstractProject,
            MultiAbstractCurrency,
            MultiAbstractProjectCashBank,
            MonitorMultiAbstractProjectCashBank,
            MonitorInternalAudit,
            DrillDownReport,
            DrillDownReportBySummary,
            Ledger,
            LedgerwiseCashBankCollection,
            FetchLedgerName,
            FetchGroupName,
            LedgerOpBalance,
            MonthlyAbstractReceiptJournal,
            FetchACIBalance,
            FetchACIBalanceMulti,
            CCEnabledLedgerList,
            ApprovedBudgetDetails
        }

        public enum BankReport
        {
            ChequeCleared,
            ChequeUncleared,
            ChequeRealiszed,
            ChequeUnrealized,
            ChequeIssuedRegister,
            ChequeCollectedRegister,
            CashBankBook,
            CashBankBookDouble,
            CashBankBookManual,
            CashBankBookManual1,
            CashBankBookManualReceipts,
            CashBankBookManualPayments,
            CashJournal,
            CashFlow,
            BankFlow,
            BankJournal,
            BankReconcilationStatement,
            BankReconcilationStatementByConsolidated,
            BankReconcilationStatementCleared,
            BankBalanceStatement,
            BankActualStatement,
            DayBook,
            ReferenceNo,
            FinancialPosition,
            NegativeCashBankBalance,
            MultiColumnCashbank,
            GStPaymentChellan,
            GSTInsPreference,
            ChequeRegisterDetails,

            FixedDepositStatement,
            CashBankReceipts,
            CashBankPayments,
            ReceiptPaymentWithPrevious,
            BankCurrentClosingBalance,
            FetchFDRegisterDetails,
            FetchMutualFundRegisterDetails,
            FetchFDInterestRegister,
            FetchFDInterestQuarterwiseRegister,
            FetchFDInterestAmountByRenewalDate,
            FetchFDHistoryDetails,
            FetchFDClosingBalanceByFDId,
            FetchFDDetailByVoucherId, //21/03/2017, To get FD_Account_Id, FD_TYPE from (FD_Account or FD_RENEWAL Tables) execpts fd opening
            CheckInValidRenewalDetailByFDId, //16/03/2017, Check Renewal's (both fd_voucher_id, fd_interest_voucher_id should be 0)
            FDInvestmentRegisterbyDateRange, //17/01/2018, to show FD investment register for givne date range
            TotalReceiptsPayments,
            BankRegister,
            GSTReturn,
            GSTExemptionInvoices,
            FetchFDMasterByFDAccountId,
            FetchFDRenewalsByFDAccountId,
            FetchFDAccountIDByVoucherId,
            FetchFDAccounts,
            JournalInvoiceStatus,
        }

        public enum FinalAccounts
        {
            ReceiptsPayments,
            Receipts,
            ReceiptsPreviousYear,
            Payments,
            PaymentsPreviousYear,
            TrialBalaceCurrent,
            TrialBalanceList,
            TrialBalanceListVerification,
            TrialBalanceExcessDifference,
            BalanceSheetExcessDifference,
            BalanceSheetExcessDifferenceForMultiCurrency,
            BalanceSheetExcessOpeningPeriod,
            Expenditure,
            Income,
            IncomeExpenditure,
            FinalIncomeExpenditure,
            GetProjectCategoryName,
            GetProjectlist,
            IEReceitpsAmt,
            IEPaymentsAmt,
            IsFirstFinancialYear,
            IsNatures,
            IsAccountingYearFrom,
            BalanceSheet,
            BalanceSheetOpeningAmt,
            BalanceSheetGroups,
            BalanceSchedules,
            BalanceScheduleIncome,
            BalanceScheduleExpence,
            BalanceCapital,
            FinalReceiptJournal,
            FinalReceiptJournalPrevious,
            ReceiptJournal,
            TransactionDetails,
            StatisticalReport,
            StatisticalSubReport,
            FetchTDSOnFDInterest,
            FetchTDSOnFDInterestPrevious,
            FetchSignDetails,
            GeneralateReportStatementAccounts,
            GeneralateVerification,
            ProfitLossBasedOnBudgetGroup,
            ForexSplitDetails,
            MismatchedStatistics
            //GeneralateReportAnnualStatementAccounts
        }

        public enum CostCentre
        {
            CostCenterCashJournal,
            CostCenterBankJournal,
            CostCenterCashBankBook,
            CostCenterLedger,
            CostCenterOpeningBalance,
            MonthlyAbstract,
            MultiAbstract,
            CostCentreReceipts,
            CostCentrePayments,
            CostCentreIncome,
            CostCentreExpenditure,
            CostCentreConsolidatedStatement,
            CCDayBook,
            CostCentreJournalTransaction,
            CostCentreBalanceStatement,
            CostCentreDisbursementList,
            ccSurplusandDeficitStatement,
            CostCentreDistributionList,
            CCDetail,
            CCDetailMonthlyAbstract,
            CCDetailReceiptsPayments,
            CCDetailVouchers,
            CCCashBankOpeningBalance
        }

        public enum CashBankVoucher
        {
            CashBankVoucherReceipts,
            CashBankVoucherPayments,
            CashBankVoucherContra,
            CashBankVoucher,
            JournalVoucher,
            CashBankTransactions,
            JournalTransactions,
            FetchcashBankByVoucher,
            FetchPurchaseslipVoucher,
            FetchPurchaseStockSlip,
            FetchJournalByVoucher,
            FetchJournalContraByVoucher,
            FetchCashBankReceiptPaymentDetails,
            FetchCashBankStockDetails,
            FetchcashBankContraByVoucher,
            FetchCashBankContraDetails,
            DonorReceipts,
            AidReceipts,
            FetchChequePrintingSetting,
            FetchChequePrinting,
            CashBankReceiptsPayments,
            FetchGSTInvoiceByJournalVoucher,
            FetchGSTInvoiceByRPVoucher,
            FetchJournalInvoice,
        }

        public enum ForeginContribution
        {
            FCCountry,
            FCPurpose,
            FCDonorInstitutional,
            FCDonorIndividual,
            ExecutiveMembers,
            BankInfoDetails,
            FC6,
            FC6Purpose,
            FC6PurposeCashBank,
            FCBank,
            FCInstPreference,
            FCContribution,
            FC6Donor,
            FC6DonorAmount,
            FC6BankAccount,
            FC6BankInterestAmount,
            FC6DesignatedBankAmount,
            FC6DesignatedBankAmountCashBank,
            FC6FixedDeposit,
            FCDrillDownReport,
            FCPurposeWiseContribution,
            FCPurposeCumulative,
            FCPurposeDistribution,
            FCPurposeCumulativeSummary,
            DonorDetailMonthlyAbstract,
            DonorDetailReceiptsPayments,
        }

        public enum ReportCriteria
        {
            DF,
            DT,
            DA,
            AT,
            BL,
            BG,
            DB,
            IK,
            IJ,
            GT,
            AG,
            AC,
            MT,
            AD,
            CD,
            AB,
            PJ,
            BK,
            LG,
            GP,
            CC,
            NN
        }

        public enum FinacialTransType
        {
            RC,
            PY,
            JN
        }

        public enum FianacialMode
        {
            Add,
            Edit
        }

        public enum BudgetVariance
        {
            BudgetVarianceReport,
            BudgetMonthDistribution,
            BudgetDetails,
            BudgetDetailsByProject,
            BudgetVaiacneReportByMonth,
            BudgetSummary,
            FetchBudgetNames,
            BudgetOpBalance,
            BudgetVarianceCostCentre,
            BudgetStatistics,
            BudgetLedgers,
            cmfBudgetLedgers,
            BudgetInfo,
            PreviousBudgetInfoByProjects,
            BudgetProjectsByDate,
            BudgetProjectsByDateProjects,
            BudgetApprovalByMonth,
            BudgetAnnualSummary,
            BudgetRealization,
            BudgetAnnualApproved,
            BudgetAnnualBudgetBalanceSheet,
            BudgetAnnualYearComparision,
            BudgetAnnualYearUserDefined,
            FetchuserDefinedBudgetBalance,
            BudgetQuaterlyRealization,
            MultiAbstractBudget,
            MultiAbstractBudgetConsolidated,
            BudgetDevelopmentalProjectDetailsByBudget,
            BudgetCCLedgerwise,
            BudgetCCRealization,
            BudgetCCRealizationUnderCC,
            BudgetCCVariation,
            BudgetAnnualCCApproved
        }

        public enum TDS
        {
            TDSPartyWise,
            TDSPaid,
            TDSComputationPayable,
            TDSComputationPayableDrillDown,
            TDSOutstandingPayable,
            TDSLedgerWise,
            TDSOutstandingLedgerDrillDown,
            TDSNatureOfPayments,
            TDSOutstandingNatureOfPaymentsDrillDown,
            TDSChallan,
            TDSForm26Q,
            TDSForm27Q,
            TDSPrintForm16A,
            TDSForm26,
            TDSForm27,
            TDSPrintForm16AQuarter
        }
        public enum Asset
        {
            AssetAMCRegister,
            AssetInsuranceRegister,
            FixedAssetSummary,
            SalesDisposeDonateRegister,
            ChartofAssets,
            PurchaseRegister,
            AssetInKindRegister,
            FixedAssetRegister,
            FixedAssetItemRegister,
            DepreciationRegister
        }
        public enum NetWorking
        {
            ProspectInstitutional,
            ProspectIndividual,
            TrackingSheet,
            DonationStatistics,
            Thanksgiving,
            Appeal,
            AnniversariesMail,
            AnniversariesSMS,
            FeastDay,
            LabelPrint

        }
        public enum ReportProperty
        {

        }

        public enum UserRights
        {
            Reports = 196,
            Abstract = 197,
            BankActivities = 198,
            BookofAccounts = 199,
            FinalAccounts = 200,
            ForeginContribution = 201,
            CostCentre = 202,
            FinancialRecords = 203,
            Budget = 204,
            Audit = 205
        }
        public enum AssociationNature
        {
            Cultural = 0,
            Economic = 1,
            Educational = 2,
            Religious = 3,
            Social = 4
        }
        public enum DenominationNature
        {
            Hindu = 0,
            Sikh = 1,
            Muslim = 2,
            Christian = 3,
            Buddhist = 4,
            Others = 5
        }

        #region Stock
        public enum Stock
        {
            StockSummaryItem,
            StockRegister,
            StockLocationSummary,
            StockTransferredItem,
            StockPurchase,
            StockSales,
            StockReceive,
            stockUtilizeRegister,
            ChartofStock,
            stockGroupLocationwise
        }
        #endregion

        public enum AuditReports
        {
            VoucherStatistics,
            VoucherAuditLog,
            AuditedVouchers,
            AuditedVouchersTrackChanges
        }

        public enum GeneralateReports
        {
            GeneralatePatrimonial,
            GeneralateActivityIncomeExpense,
            GeneralateActivityGSTLedgerIncomeExpense,
            GeneralateActivityIncomeExpenseFA,
        }

    }
}