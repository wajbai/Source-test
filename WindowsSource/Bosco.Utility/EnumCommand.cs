/*  Class Name      : EnumCommand.cs
 *  Purpose         : Enum Data type to avoid using scalars/magic numbers in application
 *  Author          : CS
 *  Created on      : 14-Jul-2010
 */

using System.ComponentModel;
namespace Bosco.Utility
{

    public enum AppSettingName
    {
        DatabaseProvider = 1,
        AppConnectionString = 2,
        SQLAdapter = 3,
        APIMode = 4,
        HiGradeURL = 5
    }

    public enum SelectionType
    {
        All = 0,
        Selected = 1,
        Deselected = 2
    }

    public enum Source
    {
        To = 1,
        By = 2,
    }

    public enum NavigationType
    {
        CashBank = 0,
        Cash = 1,
        Bank = 2,
        BRS = 3,
        FD = 4
    }

    public enum TransSource
    {
        Cr = 1,
        Dr = 2
    }

    public enum PrintType
    {
        DT,
        DS
    }

    public enum WaitDialogType
    {
        SuccessDialog,
        WaitDialog
    }

    public enum CashSource
    {
        By,
        To
    }

    public enum ExportMode
    {
        Offline = 0,
        Online = 1
    }

    public enum CashFlag
    {
        Cash,
        Bank
    }
    public enum Mode
    {
        Offline = 0,
        Online = 0
    }
    public enum Sorting
    {
        Ascending,
        Descending,
        None
    }

    public enum TimeMode
    {
        AM = 1,
        PM = 2
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }

    public enum Id
    {
        ProjectCategory,
        Project,
        Donor,
        Ledger,
        LedgerGroup,
        Bank,
        BankAccount,
        Purpose,
        CostCentre,
        CostCategory,
        Country,
        GSTClass,
        Vendor,
        LegalEntity,
        ParentGroup,
        MainGroup,
        Nature,
        State,
        SubLedger,
        BudgetLedger,
        FDAccount,
        StatisticsType

    }

    public enum ID
    {
        ParentGroupID,
        ItemID,
        VendorID,
        CustodianID,
        ManufactureID,
        ClassID,
        ParentClassId,
        UnitID,
        StockGroupID,
        StockItemID,
        LocationID,
        ParentLocationId,
        ProjectId,
        AccountLedgerId,
        BlockID,
    }

    public enum ValueGreaterType
    {
        GreaterorLess = 0
    }

    public enum YesNo
    {
        No = 0,
        Yes = 1
    }

    public enum DonorType
    {
        Institutional = 1,
        Individual = 2
    }

    public enum Types
    {
        Donor = 0,
        Auditor = 1,
        Other = 2
    }

    public enum DonorCategory
    {
        Above = 1,
        Below = 2
    }

    public enum DateDataType
    {
        Date,
        DateTime,
        Time,
        TimeStamp,
        DateNoFormatBegin,
        DateNoFormatEnd,
        DateFormatYMD
    }

    public enum MasterImport
    {
        Donor,
        Group,
        Item,
        Vendor,
        Custudian,
        UnitOfMeasure,
        Manufacture,
        METHOD,
        DEPRECIATION,
        ROLE,
        SYMBOL,
        UNIT,
        PREFIX,
        SUFFIX,
        ADDRESS,
        EMAIL,
        Location,
        Masters,
        Prospects,
        Transaction,
        Voucher
    }

    public enum UserType
    {
        None = 0,
        Admin = 1,
        User = 2,
    }

    public enum ChartViewType
    {
        None,
        Bar,
        Pie,
        Pie3D,
        Line,
        Area,
        SplineArea,
        StepArea,
        //FullStackedBar
    }

    public enum BudgetType
    {
        [Description("Budget Year")]
        BudgetYear = 1,
        [Description("Budget Period")]
        BudgetPeriod = 2,
        [Description("Financial Year")]
        BudgetByAnnualYear = 3,
        [Description("Calendar Year")]
        BudgetByCalendarYear = 4,
        [Description("Month")]
        BudgetMonth = 5,
        [Description("Academic Year")]
        BudgetAcademic = 6
    }

    public enum BudgetLevel
    {
        [Description("Singular")]
        Singular = 1,
        [Description("Composite")]
        Composite = 2
    }

    public enum BudgetAction
    {
        [Description("Created")]
        Created = 0,
        [Description("Recommended")]
        Recommended = 1,
        [Description("Approved")]
        Approved = 2
    }


    public enum BudgetMainGroup
    {
        [Description("Recurring")]
        Recurring = 1,
        [Description("NonRecurring")]
        NonRecurring = 0
    }

    public enum BudgetGroup
    {
        [Description("Default")]
        Default = 0,
        [Description("Recurring Expenses")]
        RecurringExpenses = 1,
        [Description("Non-Recurring Expenses")]
        NonRecurringExpenses = 2,
        [Description("Recurring Income")]
        RecurringIncome = 3,
        [Description("Non-Recurring Income")]
        NonRecurringIncome = 4,
        [Description("Transfer other Sectors")]
        TransferotherSectors = 5,
        [Description("Contribution Province")]
        ContributionProvince = 6,
    }

    public enum BudgetSubGroup
    {
        [Description("Default")]
        Default = 0,
        [Description("Regular Expenses")]
        RegularExpenses = 1,
        [Description("Non-Regular Expenses")]
        NonRegularExpenses = 2,
    }

    public enum LedgerType
    {
        General,
        InKind
    }
    public enum AccountType
    {
        [Description("Savings Account")]
        Savings = 1,
        [Description("Current Account")]
        Current = 2
    }

    public enum ledgerSubType
    {
        CA,
        BK,
        FD,
        IK,
        GN,
        TDS,
        PAY,
        AST
    }

    public enum FDInvestmentType
    {
        [Description("None")]
        None = 0,
        [Description("FD")]
        FD = 1,
        [Description("RD - Post Office")]
        RDPostOffice = 2,
        [Description("RD - Bank")]
        RDBank = 3,
        [Description("Mutual Fund")]
        MutualFund = 4,
    }

    public enum LedgerTable
    {
        MasterLedger,
        HeadOfficeLedger
    }

    public enum FixedLedgerGroup
    {
        BankAccounts = 12,
        Cash = 13,
        FixedDeposit = 14,
        FixedAssets = 2
    }

    public enum FixedDepositStatus
    {
        Deposited = 1,
        Realized = 2
    }

    public enum LedgerSortOrder
    {
        Cash = 1,
        Bank = 2,
        FD = 3,
        IK = 4,
        GN = 255
    }

    public enum Status
    {
        Inactive = 0,
        Active = 1
    }

    public enum ViewDetails
    {
        Donor = 0,
        Auditor = 1,
    }

    public enum IdentityKey
    {
        Donor = 0,
        Auditor = 1,
    }

    public enum AddNewRow
    {
        NewRow = 0
    }
    public enum ActiveStatus
    {
        Active = 1,
        InActive = 0
    }
    public enum FormMode
    {
        Add = 0,
        Edit = 1
    }

    public enum FundTransferList
    {
        [Description("NEFT")]
        NEFT = 1,
        [Description("RTGS")]
        RTGS = 2,
        [Description("IMPS")]
        IMPS = 3,
        [Description("GOOGLE PAY")]
        GooglePay = 4,
        [Description("PHONE PAY")]
        PhonePay = 5,
        [Description("PAYTM")]
        Paytm = 6

    }

    public enum AccessFlag
    {
        Accessable = 0,
        Editable = 1,
        Readonly = 2
    }
    public enum BankAccoutType
    {
        SavingAccount = 1,
        FixedDeposit = 2,
        MutualFund = 3,
        RecurringDeposit = 4,
        Equity = 5
    }

    public enum AssetLedgerType
    {
        Month,
        AccountLedgerId,
        DepreciationLedgerId,
        DisposalLedgerId,
        DepreciationMonth
    }
    public enum AssetInsurance
    {
        Create = 0,
        Renew = 1,
        Update = 2,
        Purchase,
        InKind,
        Opening
    }
    public enum AssetDepre
    {
        Before = 1,
        After = 2
    }
    public enum AssetAmc
    {
        Create = 0,
        Renew = 1,
        Edit = 2,
        AmcRenew
    }
    public enum AssetCondition
    {
        [Description("Working")]
        Working = 0,
        [Description("Alteration Renovation")]
        AlterationRenovation = 1,
        [Description("Maintenance")]
        Maintenance = 2,
        [Description("Under Renovation")]
        UnderRenovation = 3
    }
    public enum StockLedgerType
    {
        //AccountLedger,
        //DisposalLedger
        IncomeLedger,
        ExpenseLedger
    }

    public enum FinanceSetting
    {
        PrintVoucher,
        UIProjSelection,
        CustomizationForm,
        EnableTransMode,
        UITransClose,
        UITransGSTPan,
        UITransMode,
        TransEntryMethod,
        EnableBookingAtPayment,
        EnableVoucherRegenerationInsert,
        EnableVoucherRegenerationDeletion,
        EnableNegativeBalance,
        EnableGST,
        IncludeGSTVendorInvoiceDetails,
        EnableChequePrinting,
        EnableRefWiseRecPayment,
        ExportVouchersBeforeClose,
        DontAlertTakeBackupBeforeClose,
        DuplicateCopyVoucherPrint,
        TwoVouchersInOnePageVoucherPrint,
        EnableFlexiFD,
        ShowBudgetLedgerActualBalance,
        ShowBudgetLedgerSeparateReceiptPaymentActualBalance,
        IncludeBudgetStatistics,
        IncludeIncomeLedgersInBudget,
        MaxCashLedgerAmountInReceiptsPayments,
        ShowCr_DrAmountDrillingLedgerInAbstract,
        EnableSubLedgerVouchers,
        EnableSubLedgerBudget,
        ShowCCOpeningBalanceInReports,
        ShowResetLedgerOpeningBalance,
        MandatoryChequeNumberInVoucherEntry,
        EnableCashBankJournal,
        EnableCCMode,
        AlertHighValuePayment,
        AlertLocalDonations,
        DonotAllowDuplicateChequeNumberInVoucherEntry,
        ShowMonthlySummaryDrillingReport,
        ConsiderBudgetNewProject,
        ContributionFromLedgers,
        ContributionToLedgers,
        InterAccountFromLedgers,
        InterAccountToLedgers,
        NatuersInReceiptVoucherEntry,
        NatuersInPaymentVoucherEntry,
        LCRef1,
        LCRef2,
        LCRef3,
        LCRef4,
        LCRef5,
        LCRef6,
        LCRef7,
        BranchReceiptModuleStatus,
        AllocateCCAmountWithGST,
        CostCeterMapping,
        GeneralateOpeningIEBalance,
        GeneralateOpeningIEBalanceMode,
        EnableCostCentreBudget,
        IncludeBudgetCCStrengthDetails,
        CreateBudgetDevNewProjects,
        ConfirmAuthorizationVoucherEntry,
        EnableFDAdjustmentEntry,
        ShowBudgetApprovedAmountInMonthlyReport,
        ShowCashBankFDDetailLedgerInBudgetProposed,
        AllowZeroValuedCashBankVoucherEntry,
        AttachVoucherFiles,
    }

    public enum AuditorSignNote
    {
        AuditorNote,
        ShowDate,
        Place,
        Sign1,
        Sign2,
        Sign3,
    }
    public enum AssetSetting
    {
        Months,
        AccountLedgerId,
        DepreciationLedgerId,
        DisposalLedgerId,
        ShowAMCRenewalAlert,
        ShowInsuranceAlert,
        ShowDepr,
        ShowOpApplyFrom
    }

    public enum TDSSetting
    {
        TDSEnabled,
        EnableBookingAtPayment,
    }

    public enum AcmeReportSetting
    {
        VoucherPrintSign1Row1,
        VoucherPrintSign1Row2,
        VoucherPrintSign2Row1,
        VoucherPrintSign2Row2,
        VoucherPrintSign3Row1,
        VoucherPrintSign3Row2,
        VoucherPrintCaptionBold,
        VoucherPrintValueBold,
        VoucherPrintShowLogo,
        VoucherPrintProject,
        VoucherPrintIncludeSigns,
        VoucherPrintReportTitleType,
        VoucherPrintReportTitleAddress,
        VoucherPrintLegalEntityDetails,
        VoucherPrintShowCostCentre,
        VoucherPrintHideVoucherReceiptNo
    }

    public enum VoucherPrintReceipt
    {

    }

    public enum VoucherPrintPayment
    {

    }

    public enum VoucherPrintJournal
    {

    }

    public enum Setting
    {
        Country,
        Currency,
        CurrencyName,
        CurrencyPosition,
        CurrencyPositivePattern,
        CurrencyNegativePattern,
        CurrencyNegativeSign,
        CurrencyCode,
        CurrencyCodePosition,
        Months,
        DigitGrouping,
        GroupingSeparator,
        DecimalPlaces,
        DecimalSeparator,
        HighNaturedAmt,
        TransEntryMethod,
        PrintVoucher,
        GSTEnabled,
        TDSEnabled,
        // EnableBookingAtPayment,
        EnableTransMode,
        Location,
        UILanguage,
        UIDateFormat,
        UIDateSeparator,
        UIThemes,
        UIProjSelection,
        UITransClose,
        UITransGSTPan,
        UIForeignBankAccount,
        UITransMode,
        CustomizationForm,
        AccountLedgerId,
        DepreciationLedgerId,
        DisposalLedgerId,
        UITDSEnabled,
        ShowAMCRenewalAlert,
        ShowInsuranceAlert,
        UIEnableBookingAtPayment,
        UIFilterMode,
        UITransType,
        UIDonationVoucherPrint,
        ServerName,
        Port,
        SMTPUsername,
        SMTPPassword,
        ThanksGivingSubject,
        AppealSubject,
        WeddingdaySubject,
        BirthdaySubject,
        SMSUserName,
        SMSPassKey,
        SenderId,
        TDSBooking,
        CreditBalance,
        ProductVersion,  //On 12/06/2017, To store Current Proudct Version
        DBUploadedOn,   //On 12/06/2017, Last date of Branch uploaded database
        DBRestoredOn,   //On 25/11/2024, Last date of Current DB restoed on
        DBRestoredRemarks,   //On 27/11/2024, Restoed details Voucher Mismatching
        UpdaterDownloadBy, //On 24/07/2017, Updater download by FTP or HTTP
        ProxyUse, //On 29/08/2017, to store proxy server details
        ProxyAddress,
        ProxyPort,
        ProxyAuthenticationUse,
        ProxyUserName,
        ProxyPassword,
        ThirdParty,
        ThirdPartyMode,
        ThirdPartyURL,
        PayrollPassword,
        ShowDepr,

        VoucherEnforceGraceMode, //0-Default(Others), 1-No, 2-Yes
        VoucherGraceDays,
        VoucherGraceTmpDateFrom,
        VoucherGraceTmpDateTo,
        VoucherGraceTmpValidUpTo,
    }

    public enum NetworkingSetting
    {
        //ServerName,
        //Port,
        //SMTPUsername,
        //SMTPPassword,
        ThanksGivingSubject,
        AppealSubject,
        WeddingdaySubject,
        BirthdaySubject,
        //SMSUserName,
        //SMSPassKey,
        //SenderId
    }
    public enum LicenseModules
    {
        SocietyName = 0,
        InstitudeName = 1,
        NoOfNodes = 2,
        NoOfModules = 3
    }

    public enum VoucherEntryGraceDaysMode
    {
        None = 0,   //Default - No settings details - fix default number of days
        No = 1,     // settings details available enforced it means dont enforce
        Yes = 2     // settings details available enforced it means enforce for given days
    }

    public enum UserSetting
    {
        //UI Setting for other users
        UILanguage,
        UIDateFormat,
        UIDateSeparator,
        UIThemes,
        UIProjSelection,
        TransEntryMethod,
        UITransClose,
        UIForeignBankAccount,
        UITransMode,
        UITDSEnabled,
        UIDonationVoucherPrint,
        Months,
        AccountLedgerId,
        DepreciationLedgerId,
        DisposalLedgerId,
        ShowAMCRenewalAlert,
        ShowInsuranceAlert,
        PrintVoucher,
        CustomizationForm,
        EnableTransMode,
        EnableBookingAtPayment,
        TDSBooking,
        UpdaterDownloadBy,
        ShowDepr,
        ShowOpApplyFrom

    }

    public enum ChequePrinting
    {
        Width,
        Height,
        DateTop,
        DateLeft,
        DateDigitWidth,
        PartyNameTop,
        PartyNameLeft,
        AmountTop,
        AmountLeft,
        AmountWordsTop,
        AmountWordsLeft
    }

    public enum VoucherEntryMethod
    {
        Single = 1,
        Multi = 2
    }

    public enum Division
    {
        Local = 1,
        Foreign = 2
    }

    //public enum VoucherType
    //{
    //    Receipts = 1,
    //    Payments = 2,
    //    Contra = 3,
    //    Journal = 4
    //}

    //public enum TransType
    //{
    //    Receipt = 0,
    //    Payment = 1,
    //    Contra = 2,
    //    Journal = 3
    //}

    //public enum VoucherTransType
    //{
    //    Receipt = 1,
    //    Payment = 2,
    //    Contra = 3,
    //    Journal = 4
    //}

    public enum DefaultVoucherTypes
    {
        Receipt = 1,
        Payment = 2,
        Contra = 3,
        Journal = 4
    }

    public enum DefaultReceiptPayment
    {
        Receipt = 1,
        Payment = 2
    }

    public enum DefaulContraVoucher
    {
        None = 0,
        CashDeposit = 1,
        CashWithdraw = 2,
        Transfer = 3,
    }

    public enum TransactionVoucherMethod
    {
        Automatic = 1,
        Manual = 2
    }

    public enum CreditBalance
    {
        [Description("Project Wise")]
        ProjectWide = 1,
        [Description("Society Wise")]
        SocietyWide = 2,
        [Description("Application Wise")]
        ApplicationWide = 3,
    }

    public enum Month
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }

    public enum EnumColumns
    {
        Id,
        Name
    }

    public enum CurrencyPosition
    {
        Before,
        After,
        None
    }

    public enum CurrencyPositivePattern
    {
        Before = 2,
        After = 3
    }

    public enum CurrencyNegativePatternBracket
    {
        Before = 14,
        After = 15
    }

    public enum CurrencyNegativePatternMinus
    {
        Before = 9,
        After = 8
    }

    public enum MapForm
    {
        Ledger,
        BankAccount,
        Project,
        CostCentre,
        Donor,
        FDLedger,
        Transaction,
        Bank,
        LedgerGroup,
        Asset
    }
    public enum ProjectVoucher
    {
        MoveIn,
        MoveOut
    }

    public enum MasterRights
    {
        ReadOnly = 0,
        FullRights = 1
    }

    public enum ReceiptType
    {
        First,
        Subsequent
    }

    public enum VoucherSubTypes
    {
        RC,
        PY,
        CN,
        JN,
        TDS,
        AST,
        STK,
        PAY,
        FD,
        GN
    }


    public enum TransactionMode
    {
        CR,
        DR
    }

    public enum TransactionAction
    {
        New,
        EditBeforeSave,
        EditAfterSave,
        Cancel
    }
    public enum NumberFormat
    {
        VoucherNumber = 1,
        ReceiptNumber = 2,
        ContraVoucherNumber = 3,
        JournalVoucherNumber = 4,
        GSTInvoiceNumber = 5
    }
    public enum BankReconciliation
    {
        Uncleared,
        Cleared,
        Unrealized,
        Realized
    }
    public enum FDRenewal
    {
        New = 0,
        Renewal = 1,
        RelizedOn = 2,
        FDOP = 3
    }

    public enum FDDeductionMode
    {
        None = 0,
        FromInterest = 1,
        FromPricipal = 2,
        Both = 3
    }

    public enum TransSelectionType
    {
        Add = 0,
        View = 1
    }

    public enum FDRenewalMode
    {
        Edit,
        Delete
    }
    public enum TransactionLedgerType
    {
        gvTrans = 0,
        gvCashTrans = 1
    }
    public enum FDTransType
    {
        Invest = 0,
        Reinvest = 1,
        Realize = 2,
        Break = 3
    }

    public enum FDInterestTypes
    {
        Renewal,
        Realize
    }
    public enum IsPeriodically
    {
        Yes,
        No
    }

    public enum LedgerTypes
    {
        FD,
        Ledger,
        GN,
        TDS,
        PAY
    }

    public enum FDTypes
    {
        OP,
        IN,
        RN,
        WD,
        POI,
        NONE,
        PWD,
        RIN,
        POC //Post Charge Amount (Part of Partial Widthdrewal)
    }

    public enum FDRenewalTypes
    {
        IRI,
        ACI,
        WDI,
        CLS,
        PWD,
        RIN,
        //POC //Post Charge Amount (Part of Partial Widthdrewal)
    }

    public enum FDScheme
    {
        Normal = 0,
        Flexi = 1
    }

    public enum ProjectSelection
    {
        EnableVoucherSelectionMethod,
        DisableVoucherSelectionMethod
    }

    public enum MigrationType
    {
        None,
        AcMePlus,
        Tally,
        BOSCOPAC
    }

    public enum DownloadBy
    {
        FTP,
        HTTP
    }

    public enum VoucherPrint
    {
        [Description("RPT-024")]
        CASHBANKRECEIPTS,
        [Description("RPT-025")]
        CASHBANKPAYMENTS,
        [Description("RPT-151")]
        CASHBANKCONTRA, //Treat Journal Voucher as Contra Voucher
        [Description("RPT-026")]
        JOURNALVOUCHER,
        [Description("RPT-144")]
        CASHBANKDONORRECEIPTS,
        [Description("RPT-144")]
        AIDDONORRECEIPTS,
        [Description("RPT-190")]
        PURCHASESLIP,
        [Description("RPT-207")]
        GSTINVOICE_JOURNAL,
        [Description("RPT-212")]
        GSTINVOICE_RECEIPT,
        [Description("RPT-233")]
        INVOICE_JOURNAL,
    }

    /// <summary>
    /// 15/02/2021, to Pre-Defined list modes
    /// </summary>
    public enum DrillDownPDLFlag
    {
        FromLedgerMonthSummary
    }

    public enum DrillDownType
    {
        BASE_REPORT,
        [Description("RPT-DDM")]
        GROUP_SUMMARY,
        [Description("RPT-DDM")]
        GROUP_SUMMARY_RECEIPTS,
        [Description("RPT-DDM")]
        GROUP_SUMMARY_PAYMENTS,

        [Description("RPT-012")]
        LEDGER_SUMMARY,
        [Description("RPT-016")]
        LEDGER_CASH,
        [Description("RPT-TPW")]
        TDS_PARTY_WISE,
        [Description("RPT-OLR")]
        TDS_OUTSTANDING_LEDGER,
        [Description("RPT-NOP")]
        TDS_OUTSTANDING_NOP,
        [Description("RPT-017")]
        LEDGER_BANK,
        [Description("RPT-012")]
        LEDGER_SUMMARY_RECEIPTS,
        [Description("RPT-012")]
        LEDGER_SUMMARY_PAYMENTS,

        [Description("RPT-050")]
        CC_LEDGER_SUMMARY,
        [Description("RPT-050")]
        CC_LEDGER_SUMMARY_RECEIPTS,
        [Description("RPT-050")]
        CC_LEDGER_SUMMARY_PAYMENTS,

        [Description("ACPP.Modules.Transaction.frmTransactionMultiAdd,ACPP")]
        LEDGER_VOUCHER,

        [Description("ACPP.Modules.Transaction.frmTransactionMultiAdd,ACPP")]
        LEDGER_CASHBANK_VOUCHER,

        [Description("ACPP.Modules.Transaction.JournalAdd,ACPP")]
        LEDGER_JOURNAL_VOUCHER,

        [Description("ACPP.Modules.Master.frmFDAccount,ACPP")]
        FD_VOUCHER,

        [Description("ACPP.Modules.Master.frmMapProjectLedger,ACPP")]
        DRILL_TO_LEDGER_DEFINE_OPENING_BALANCE,

        [Description("RPT-028")]
        DRILL_TO_IE_REPORT,

        //******Added by sugan--to drilldown FD Statement report to FD History report*********************************************************************************************************
        [Description("RPT-094")]
        FD_ACCOUNT,
        //******Added by sugan--to drilldown FD History report to concern record*********************************************************************************************************
        [Description("ACPP.Modules.Master.frmFDAccount,ACPP")]
        FD_RENEWAL_DRILLDOWN,
        //***************************************************************************************************************

        [Description("RPT-DDM")]
        DRILL_DOWN,

        [Description("RPT-TPW")]
        TDS_PARTY_DRILL_DOWN,

        [Description("RPT-OLR")]
        TDS_LEDGER_DRILL_DOWN,

        [Description("RPT-NOP")]
        TDS_NOP_DRILL_DOWN,

        [Description("RPT-FCR")]
        FC_REPORT,

        [Description("RPT-052")]
        DAYBOOK,

        [Description("RPT-195")]
        AUDITLOG,

        [Description("ACPP.Modules.Master.frmMapLedgersWithGeneralate, ACPP")]
        GENERALATE_MAPPING,
    }

    public enum Menus
    {
        Settings = 1,
        MasterSetting = 2,
        AccountMapping = 5,
        TransactionPeriod = 11,
        LegalEntity = 16,
        Masters = 21,
        ProjectCategory = 22,

        Project = 27,
        LedgerGroup = 32,
        Ledger = 37,
        SundryCreditorsDebtors = 560,
        BankAccounts = 42,
        VoucherNumberDefinition = 47,
        CostCentre = 52,
        Bank = 57,
        Country = 62,
        AuditInfo = 67,
        GoverningMembers = 72,
        Auditor = 77,
        Donor = 82,
        Purpose = 87,
        Finance = 89,
        Receipt = 90,
        Payments = 97,
        Contra = 103,
        Journal = 110,
        BankReconciliation = 115,
        Budget = 121,
        FixedDepositLedger = 126,
        FixedDeposit = 131,
        FixedInvestment = 136,
        FixedDepositRenewal = 141,
        FixedDepositPostInterest = 437,
        FixedDepositReInvestment = 442,
        FDWithdrawal = 146,
        FixedDepositRegister = 149,
        User = 152,
        UserRole = 157,
        UserRightsManagement = 162,
        ManageSecurity = 164,
        Backup = 206,
        Restore = 207,
        RefreshBalance = 208,
        UnusedLedger = 362,
        MoveDeleteMultipleLedgers = 363,
        RegenarateVoucher = 209,
        DataMigration = 210,
        DataExport = 211,
        Dashboard = 213,
        ShowImportMasters = 218,
        ShowExportVouchers = 219,
        ShowMapLedgers = 220,
        MigrationMapping = 212,
        LedgerOptions = 221,
        ImportData = 221,
        ExportData = 222,
        ManageMultiBranch = 223,
        SubBranchList = 224,
        ExportMastertoSubBranch = 225,
        UploadSubBranchVouchers = 226,
        LicenseKey = 227,
        UploadDatabase = 228,
        PortalUpdates = 229,
        AuditLockTypes = 230,
        AuditLockTrans = 235,
        FinanceSettings = 359, // Finnance Setting
        TDSSetting = 360,        // TDS Setting
        AssetSetting = 361,      // Asset Setting
        BudgetAnnual = 242,
        State = 254,
        CostCentreCategory = 260,
        TDSSection = 274,
        TDSCompanyInfo = 273,
        TDSNatureofPayments = 286,
        AuditType = 280,
        TDSDeducteeType = 292,
        TDSPolicy = 298,
        TDSLedger = 302,
        TDSDutyTax = 305,
        TDSDeduction = 309,
        ReceiptView = 323,
        JournalView = 324,

        // Fixed Asset Module 
        AssetClass = 363,
        AssetItem = 371,
        Vendor = 376,
        Manufacturer = 381,
        Custodian = 386,
        Block = 391,
        Location = 396,
        UoM = 401,
        OpeningAsset = 406,
        Purchase = 411,
        ReceiveInKind = 416,
        SalesDisposalDonation = 421,
        Depreciation = 426,
        UpdateAssetDetails = 431,
        FixedAssetRegister = 432,
        MapAssetLedger = 433,
        ImportOpeningAsset = 434,
        Configure = 435,

        // Payroll
        CreateNewPayrollMonth = 448,
        PayrollGroup = 449,
        Staff = 454,
        PayrollComponent = 459,
        Loan = 464,
        IssuesLoan = 469,
        OpenPayrollMonth = 474,
        DeletePayrollMonth = 475,
        componentAllocation = 476,
        ViewPayroll = 477,
        PostPayrollvouchertoFinanceTransaction = 478,

        // Stock User Rights
        StockGroup = 481,
        StockItem = 486,
        StockVendor = 491,
        StockCustodian = 496,
        StockBlockArea = 501,
        StockLocation = 506,
        StockUOM = 511,
        StockOpeningBalance = 516,
        StockImportMaster = 517,
        StockRegister = 518,
        StockPurchase = 519,
        StockReceiveInkind = 524,
        StockSales = 529,
        StockUtilize = 534,
        StockDispose = 539,
        StockItemTransfer = 544,
        StockPurchaseReturn = 549,
        StockReports = 554,

        //Network
        Member = 357, // to be added in the DB
        Prospect = 313,
        MailTemplate = 321,
        SMSTemplate = 327,
        ThanksgivingMail = 332,
        AppealMail = 335,
        NewsletterMail = 338,
        AnniversaryMail = 341,
        FeastMail = 344,
        ThanksgivingSMS = 347,
        AppealSMS = 350,
        AnniversarySMS = 353,
        FeastSMS = 356,
        GST = 400,
    }

    public enum Forms
    {
        UISettings = 3,
        GlobalSettings = 4,
        MapProject = 6,
        MapFCPurpose = 310,
        MapLedger = 7,
        MapCostCentre = 8,
        MapDonor = 9,
        MapVouchers = 10,
        InstituteInfo = 311,
        [Description("ADD")]
        CreateTransaction = 12,
        [Description("EDIT")]
        EditTransaction = 13,
        [Description("DELETE")]
        DeleteTransaction = 14,
        [Description("PRINT")]
        PrintTransaction = 15,
        [Description("ADD")]
        CreateLegalEntity = 17,
        [Description("EDIT")]
        EditLegalEntity = 18,
        [Description("DELETE")]
        DeleteLegalEntity = 19,
        [Description("PRINT")]
        PrintLegalEntity = 20,
        [Description("ADD")]
        CreateProjectCategory = 23,
        [Description("EDIT")]
        EditProjectCategory = 24,
        [Description("DELETE")]
        DeleteProjectCategory = 25,
        [Description("PRINT")]
        PrintProjectCategory = 26,
        [Description("ADD")]
        CreateProject = 28,
        [Description("EDIT")]
        EditProject = 29,
        [Description("DELETE")]
        DeleteProject = 30,
        [Description("PRINT")]
        PrintProject = 31,
        [Description("ADD")]
        CreateLedgerGroup = 33,
        [Description("EDIT")]
        EditLedgerGroup = 34,
        [Description("DELETE")]
        DeleteLedgerGroup = 35,
        [Description("PRINT")]
        PrintLedgerGroup = 36,
        [Description("ADD")]
        CreateLedger = 38,
        [Description("EDIT")]
        EditLedger = 39,
        [Description("DELETE")]
        DeleteLedger = 40,
        [Description("PRINT")]
        PrintLedger = 41,
        [Description("ADD")]
        CreateSundryCreditorsDebtorsLedgers = 561,
        [Description("EDIT")]
        EditSundryCreditorsDebtorsLedgers = 562,
        [Description("DELETE")]
        DeleteSundryCreditorsDebtorsLedgers = 563,
        [Description("PRINT")]
        ViewSundryCreditorsDebtorsLedgers = 564,
        [Description("ADD")]
        CreateBankAccount = 43,
        [Description("EDIT")]
        EditBankAccount = 44,
        [Description("DELETE")]
        DeleteBankAccount = 45,
        [Description("PRINT")]
        PrintBankAccount = 46,
        [Description("ADD")]
        CreateVoucher = 48,
        [Description("EDIT")]
        EditVoucher = 49,
        [Description("DELETE")]
        DeleteVoucher = 50,
        [Description("PRINT")]
        PrintVoucher = 51,
        [Description("ADD")]
        CreateCostCentre = 53,
        [Description("EDIT")]
        EditCostCentre = 54,
        [Description("DELETE")]
        DeleteCostCentre = 55,
        [Description("PRINT")]
        PrintCostCentre = 56,
        [Description("ADD")]
        CreateBank = 58,
        [Description("EDIT")]
        EditBank = 59,
        [Description("DELETE")]
        DeleteBank = 60,
        [Description("PRINT")]
        PrintBank = 61,
        [Description("ADD")]
        CreateCountry = 63,
        [Description("EDIT")]
        EditCountry = 64,
        [Description("DELETE")]
        DeleteCountry = 65,
        [Description("PRINT")]
        PrintCountry = 66,
        [Description("ADD")]
        CreateAuditInfo = 68,
        [Description("EDIT")]
        EditAuditInfo = 69,
        [Description("DELETE")]
        DeleteAuditInfo = 70,
        [Description("PRINT")]
        PrintAuditInfo = 71,
        [Description("ADD")]
        CreateGoverningMember = 73,
        [Description("EDIT")]
        EditGoverningMember = 74,
        [Description("DELETE")]
        DeleteGoverningMember = 75,
        [Description("PRINT")]
        PrintGoverningMember = 76,
        [Description("ADD")]
        CreateAuditor = 78,
        [Description("EDIT")]
        EditAuditor = 79,
        [Description("DELETE")]
        DeleteAuditor = 80,
        [Description("PRINT")]
        PrintAuditor = 81,
        [Description("ADD")]
        CreateDonor = 83,
        [Description("EDIT")]
        EditDonor = 84,
        [Description("DELETE")]
        DeleteDonor = 85,
        [Description("PRINT")]
        PrintDonor = 86,
        [Description("PRINT")]
        PrintPurpose = 88,
        [Description("ADD")]
        CreateReceiptVoucher = 91,
        [Description("EDIT")]
        EditReceiptVoucher = 92,
        [Description("DELETE")]
        DeleteReceiptVoucher = 93,
        [Description("VIEW")]
        ViewReceiptVoucher = 94,

        [Description("INSERT")]
        InsertReceiptVoucher = 266,
        [Description("NAGATIVEBALANCE")]
        ShowReceiptNagativeBalance = 269,
        [Description("PRINT")]
        PrintReceiptVoucher = 95,
        [Description("MOVE")]
        MoveReceiptVoucher = 109,
        [Description("ADD")]
        CreatePaymentVoucher = 98,
        [Description("EDIT")]
        EditPaymentVoucher = 99,
        [Description("DELETE")]
        DeletePaymentVoucher = 100,
        [Description("MOVE")]
        MovePaymentVoucher = 101,
        [Description("PRINT")]
        PrintPaymentVoucher = 102,
        [Description("VIEW")]
        ViewPaymentVoucher = 184,
        [Description("INSERT")]
        InsertPaymentVoucher = 267,
        [Description("NAGATIVEBALANCE")]
        ShowPaymentNagativeBalance = 270,
        [Description("ADD")]
        CreateContraVoucher = 104,
        [Description("EDIT")]
        EditContraVoucher = 105,
        [Description("DELETE")]
        DeleteContraVoucher = 106,
        [Description("MOVE")]
        MoveContraVoucher = 107,
        [Description("PRINT")]
        PrintContraVoucher = 108,
        [Description("VIEW")]
        ViewContraVoucher = 185,
        [Description("INSERT")]
        InsertContraVoucher = 268,
        [Description("NAGATIVEBALANCE")]
        ShowContraNagativeBalance = 271,
        [Description("ADD")]
        CreateJournalVoucher = 111,
        [Description("EDIT")]
        EditJournalVoucher = 112,
        [Description("DELETE")]
        DeleteJournalVoucher = 113,
        [Description("PRINT")]
        PrintJournalVoucher = 114,
        [Description("PRINT")]
        PrintBankReconciliation = 116,
        BankReconciled = 117,
        BankUnReconcilied = 118,
        BankCleared = 119,
        BankUnCleared = 120,
        [Description("ADD")]
        CreateBudget = 122,
        [Description("EDIT")]
        EditBudget = 123,
        [Description("DELETE")]
        DeleteBudget = 124,
        [Description("PRINT")]
        PrintBudget = 125,
        [Description("ADD")]
        CreateFDLedger = 127,
        [Description("EDIT")]
        EditFDLedger = 128,
        [Description("DELETE")]
        DeleteFDLedger = 129,
        [Description("PRINT")]
        PrintFDLedger = 130,
        [Description("ADD")]
        CreateFixedDeposit = 132,
        [Description("EDIT")]
        EditFixedDeposit = 133,
        [Description("DELETE")]
        DeleteFixedDeposit = 134,
        [Description("PRINT")]
        PrintFixedDeposit = 135,
        [Description("ADD")]
        CreateFixedInvestment = 137,
        [Description("EDIT")]
        EditFixedInvestment = 138,
        [Description("DELETE")]
        DeleteFixedInvestment = 139,
        [Description("PRINT")]
        PrintFixedInvestment = 140,
        [Description("ADD")]
        RenewFixedDeposit = 142,
        [Description("EDIT")]
        ModifyFixedDepostRenewal = 143,
        [Description("DELETE")]
        DeleteFixedDepositRenewal = 144,
        [Description("PRINT")]
        PrintFixedDepositRenewal = 145,
        [Description("ADD")]
        PostInterestFixedDeposit = 438,
        [Description("EDIT")]
        ModifyPostInterestFixedDeposit = 439,
        [Description("DELETE")]
        DeletePostInterestFixedDeposit = 440,
        [Description("PRINT")]
        PrintFixedDepositPostInterest = 441,
        [Description("ADD")]
        ReInvestmentFixedDeposit = 443,
        [Description("EDIT")]
        ModifyFixedDepostReInvestment = 444,
        [Description("DELETE")]
        DeleteFixedDepositReInvestment = 445,
        [Description("PRINT")]
        PrintFixedDepositReInvestment = 446,
        [Description("ADD")]
        WithdrawFixedDeposit = 147,
        [Description("PRINT")]
        PrintFixedDepositWithdraw = 148,
        [Description("PRINT")]
        PrintFixedDepositRegister = 150,
        [Description("REFRESH")]
        ViewFixedDepositPostInterest = 1005,
        [Description("ADD")]
        CreateUser = 153,
        [Description("EDIT")]
        EditUser = 154,
        [Description("DELETE")]
        DeleteUser = 155,
        [Description("PRINT")]
        PrintUser = 156,
        [Description("ADD")]
        CreateUserRole = 158,
        [Description("EDIT")]
        EditUserRole = 159,
        [Description("DELETE")]
        DeleteUserRole = 160,
        [Description("PRINT")]
        PrintUserRole = 161,
        [Description("EDIT")]
        ResetPassword = 165,
        [Description("VIEW")]
        ViewUserRole = 166,
        [Description("VIEW")]
        ViewUser = 167,
        [Description("VIEW")]
        ViewTransaction = 168,
        [Description("VIEW")]
        ViewLedgalEntity = 169,
        [Description("VIEW")]
        ViewProjectCategory = 170,
        [Description("VIEW")]
        ViewProject = 171,
        [Description("VIEW")]
        ViewLedgerGroup = 172,
        [Description("VIEW")]
        ViewLedger = 173,
        [Description("VIEW")]
        ViewBankAccounts = 174,
        [Description("VIEW")]
        ViewVoucher = 175,
        [Description("VIEW")]
        ViewCostCentre = 176,
        [Description("VIEW")]
        ViewBank = 177,
        [Description("VIEW")]
        ViewCountry = 178,
        [Description("VIEW")]
        ViewAuditInfo = 179,
        [Description("VIEW")]
        ViewGoverningMembers = 180,
        [Description("VIEW")]
        ViewAuditor = 181,
        [Description("VIEW")]
        ViewDonor = 182,
        [Description("VIEW")]
        ViewPurpose = 183,
        //[Description("VIEW")]
        //ViewPaymentVoucher = 184,
        //[Description("VIEW")]
        //ViewContraVoucher = 185,
        [Description("VIEW")]
        ViewJournalVoucher = 186,
        [Description("VIEW")]
        ViewBankReconciliation = 187,
        [Description("VIEW")]
        ViewBudget = 188,
        [Description("VIEW")]
        ViewFDLedger = 189,
        [Description("VIEW")]
        ViewFixedDeposit = 190,
        [Description("VIEW")]
        ViewFixedInvestment = 191,
        [Description("VIEW")]
        ViewFixedDepositRenewal = 192,
        [Description("VIEW")]
        ViewFixedDepositWithdraw = 193,
        [Description("VIEW")]
        ViewFixedDepositRegister = 194,
        [Description("VIEW")]
        ViewManageSecurity = 195,
        [Description("ADD")]
        CreateLockType = 231,
        [Description("EDIT")]
        EditLockType = 232,
        [Description("DELETE")]
        DeleteLockType = 233,
        [Description("PRINT")]
        PrintLockType = 234,
        [Description("VIEW")]
        ViewLockType = 240,
        [Description("ADD")]
        CreateLockTrans = 236,
        [Description("EDIT")]
        EditLockTrans = 237,
        [Description("DELETE")]
        DeleteLockTrans = 238,
        [Description("PRINT")]
        PrintLockTrans = 239,
        [Description("VIEW")]
        ViewLockTrans = 241,
        [Description("ADD")]
        CreateBudgetAnnual = 243,
        [Description("EDIT")]
        EditBudgetAnnual = 244,
        [Description("DELETE")]
        DeleteBudgetAnnual = 245,
        [Description("PRINT")]
        PrintBudgetAnnual = 246,
        [Description("VIEW")]
        ViewBudgetAnnual = 247,
        [Description("ADD")]
        CreateCostCentreCategory = 249,
        [Description("EDIT")]
        EditCostCentreCategory = 250,
        [Description("DELETE")]
        DeleteCostCentreCategory = 251,
        [Description("PRINT")]
        PrintCostCentreCategory = 252,
        [Description("VIEW")]
        ViewCostCentreCategory = 253,
        [Description("ADD")]
        CreateState = 255,
        [Description("EDIT")]
        EditState = 256,
        [Description("DELETE")]
        DeleteState = 257,
        [Description("PRINT")]
        PrintState = 258,
        [Description("VIEW")]
        ViewState = 259,
        DashBoard = 213,
        ViewReceiptPayments = 214,
        ShowFDAlert = 215,
        ShowBankReconciliation = 216,
        ShowProjectDetails = 217,
        [Description("ADD")]
        CreateAuditType = 281,
        [Description("EDIT")]
        EditAuditType = 282,
        [Description("DELETE")]
        DeleteAuditType = 283,
        [Description("PRINT")]
        PrintAuditType = 284,
        [Description("VIEW")]
        ViewAuditType = 285,
        [Description("ADD")]
        CreateTDSSection = 275,
        [Description("EDIT")]
        EditTDSSection = 276,
        [Description("DELETE")]
        DeleteTDSSection = 277,
        [Description("PRINT")]
        PrintTDSSection = 278,
        [Description("VIEW")]
        ViewTDSSection = 279,
        [Description("ADD")]
        CreateNatureofPayments = 287,
        [Description("EDIT")]
        EditNatureofPayments = 288,
        [Description("DELETE")]
        DeleteNatureofPayments = 289,
        [Description("PRINT")]
        PrintNatureofPayments = 290,
        [Description("VIEW")]
        ViewNatureofPayments = 291,
        [Description("ADD")]
        CreateDeducteeType = 293,
        [Description("EDIT")]
        EditDeducteeType = 294,
        [Description("DELETE")]
        DeleteDeducteeType = 295,
        [Description("PRINT")]
        PrintDeducteeType = 296,
        [Description("VIEW")]
        ViewDeducteeType = 297,
        [Description("EDIT")]
        EditTDSPolicy = 299,
        [Description("DELETE")]
        DeleteTDSPolicy = 300,
        [Description("VIEW")]
        ViewTDSPolicy = 301,
        [Description("PRINT")]
        PrintTDSLedger = 303,
        [Description("VIEW")]
        ViewTDSLedger = 304,
        [Description("EDIT")]
        EditDutyTax = 306,
        [Description("PRINT")]
        PrintDutyTax = 307,
        [Description("VIEW")]
        ViewDutyTax = 308,
        TDSDeduction,

        //Networking
        [Description("ADD")]
        CreateProspect = 314,
        [Description("EDIT")]
        EditProspect = 315,
        [Description("DELETE")]
        DeleteProspect = 316,
        [Description("PRINT")]
        PrintProspect = 317,
        [Description("VIEW")]
        ViewProspect = 318,
        [Description("CONVERT")]
        ConvertProspect = 319,
        [Description("IMPORT")]
        ImportProspect = 320,

        [Description("ADD")]
        CreateThanksgivingMail = 322,
        [Description("ADD")]
        CreateAppealMail = 323,
        [Description("ADD")]
        CreateNewsletterMail = 324,
        [Description("ADD")]
        CreateAnniversaryMail = 325,
        [Description("ADD")]
        CreateFeastDayMail = 326,

        [Description("ADD")]
        CreateThanksgivingSMS = 328,
        [Description("ADD")]
        CreateAppealSMS = 329,
        [Description("ADD")]
        CreateAnniversarySMS = 330,
        [Description("ADD")]
        CreateFeastDaySMS = 331,

        [Description("VIEW")]
        PreviewThanksgivingMail = 333,
        SendThanksgivingMail = 334,

        [Description("VIEW")]
        PreviewAppealMail = 336,
        SendAppealMail = 337,

        [Description("VIEW")]
        PreviewNewsletterMail = 339,
        SendNewsletterMail = 340,

        [Description("VIEW")]
        PreviewAnniversaryMail = 342,
        SendAnniversaryMail = 343,

        [Description("VIEW")]
        PreviewFeastMail = 345,
        SendFeastMail = 346,

        [Description("VIEW")]
        PreviewThanksgivingSMS = 348,
        SendThanksgivingSMS = 349,

        [Description("VIEW")]
        PreviewAppealSMS = 351,
        SendAppealSMS = 352,

        [Description("VIEW")]
        PreviewAnniversarySMS = 354,
        SendAnniversarySMS = 355,

        [Description("VIEW")]
        PreviewFeastSMS = 357,
        SendFeastSMS = 358,

        //Asset
        [Description("ADD")]
        CreateClass = 364,
        [Description("EDIT")]
        EditClass = 365,
        [Description("DELETE")]
        DeleteClass = 366,
        [Description("PRINT")]
        PrintClass = 367,
        [Description("VIEW")]
        ViewClass = 368,

        [Description("ADD")]
        CreateItem = 372,
        [Description("EDIT")]
        EditItem = 373,
        [Description("DELETE")]
        DeleteItem = 374,
        [Description("VIEW")]
        ViewItem = 375,

        [Description("ADD")]
        CreateVendor = 377,
        [Description("EDIT")]
        EditVendor = 378,
        [Description("DELETE")]
        DeleteVendor = 379,
        [Description("VIEW")]
        ViewVendor = 380,

        [Description("ADD")]
        CreateManufacturer = 382,
        [Description("EDIT")]
        EditManufacturer = 383,
        [Description("DELETE")]
        DeleteManufacturer = 384,
        [Description("VIEW")]
        ViewManufacturer = 385,

        [Description("ADD")]
        CreateCustodian = 387,
        [Description("EDIT")]
        EditCustodian = 388,
        [Description("DELETE")]
        DeleteCustodian = 389,
        [Description("VIEW")]
        ViewCustodian = 390,

        [Description("ADD")]
        CreateBlock = 392,
        [Description("EDIT")]
        EditBlock = 393,
        [Description("DELETE")]
        DeleteBlock = 394,
        [Description("VIEW")]
        ViewBlock = 395,

        [Description("ADD")]
        CreateLocation = 397,
        [Description("EDIT")]
        EditLocation = 398,
        [Description("DELETE")]
        DeleteLocation = 399,
        [Description("VIEW")]
        ViewLocation = 400,

        [Description("ADD")]
        CreateUoM = 402,
        [Description("EDIT")]
        EditUoM = 403,
        [Description("DELETE")]
        DeleteUoM = 404,
        [Description("VIEW")]
        ViewUoM = 405,

        [Description("ADD")]
        CreatePurchase = 412,
        [Description("EDIT")]
        EditPurchase = 413,
        [Description("DELETE")]
        DeletePurchase = 414,
        [Description("VIEW")]
        ViewPurchase = 415,

        [Description("ADD")]
        CreateReceiveInKind = 417,
        [Description("EDIT")]
        EditReceiveInKind = 418,
        [Description("DELETE")]
        DeleteReceiveInKind = 419,
        [Description("VIEW")]
        ViewReceiveInKind = 420,

        [Description("ADD")]
        CreateSalesDisposalDonation = 422,
        [Description("EDIT")]
        EditSalesDisposalDonation = 423,
        [Description("DELETE")]
        DeleteSalesDisposalDonation = 424,
        [Description("VIEW")]
        ViewSalesDisposalDonation = 425,

        [Description("ADD")]
        CreateDepreciation = 427,
        [Description("EDIT")]
        EditDepreciation = 428,
        [Description("DELETE")]
        DeleteDepreciation = 429,
        [Description("VIEW")]
        ViewDepreciation = 430,

        // Payroll
        [Description("ADD")]
        CreatePayrollGroup = 450,
        [Description("EDIT")]
        EditPayrollGroup = 451,
        [Description("DELETE")]
        DeletePayrollGroup = 452,
        [Description("VIEW")]
        ViewPayrollGroup = 453,

        [Description("ADD")]
        CreateStaff = 455,
        [Description("EDIT")]
        EditStaff = 456,
        [Description("DELETE")]
        DeleteStaff = 457,
        [Description("VIEW")]
        ViewStaff = 458,

        [Description("ADD")]
        CreatePayrollComponent = 460,
        [Description("EDIT")]
        EditPayrollComponent = 461,
        [Description("DELETE")]
        DeletePayrollComponent = 462,
        [Description("VIEW")]
        ViewPayrollComponent = 463,

        [Description("ADD")]
        CreateLoan = 465,
        [Description("EDIT")]
        EditLoan = 466,
        [Description("DELETE")]
        DeleteLoan = 467,
        [Description("VIEW")]
        ViewLoan = 468,

        [Description("ADD")]
        CreateIssuesLoan = 470,
        [Description("EDIT")]
        EditIssuesLoan = 471,
        [Description("DELETE")]
        DeleteIssuesLoan = 472,
        [Description("VIEW")]
        ViewIssuesLoan = 473,

        // Stock
        [Description("ADD")]
        CreateStockGroup = 481,
        [Description("EDIT")]
        EditStockGroup = 482,
        [Description("DELETE")]
        DeleteStockGroup = 483,
        [Description("VIEW")]
        ViewStockGroup = 484,

        [Description("ADD")]
        CreateStockItem = 487,
        [Description("EDIT")]
        EditStockItem = 488,
        [Description("DELETE")]
        DeleteStockItem = 489,
        [Description("VIEW")]
        ViewStockItem = 490,

        [Description("ADD")]
        CreateStockVendor = 492,
        [Description("EDIT")]
        EditStockVendor = 493,
        [Description("DELETE")]
        DeleteStockVendor = 494,
        [Description("VIEW")]
        ViewStockVendor = 495,

        [Description("ADD")]
        CreateStockCustodian = 497,
        [Description("EDIT")]
        EditStockCustodian = 498,
        [Description("DELETE")]
        DeleteStockCustodian = 499,
        [Description("VIEW")]
        ViewStockCustodian = 500,

        [Description("ADD")]
        CreateStockBlockArea = 501,
        [Description("EDIT")]
        EditStockBlockArea = 502,
        [Description("DELETE")]
        DeleteStockBlockArea = 503,
        [Description("VIEW")]
        ViewStockBlockArea = 504,

        [Description("ADD")]
        CreateStockLocation = 507,
        [Description("EDIT")]
        EditStockLocation = 508,
        [Description("DELETE")]
        DeleteStockLocation = 510,
        [Description("VIEW")]
        ViewStockLocation = 511,

        [Description("ADD")]
        CreateStockUOM = 512,
        [Description("EDIT")]
        EditStockUOM = 513,
        [Description("DELETE")]
        DeleteStockUOM = 514,
        [Description("VIEW")]
        ViewStockUOM = 515,

        [Description("ADD")]
        CreateStockPurchase = 520,
        [Description("EDIT")]
        EditStockPurchase = 521,
        [Description("DELETE")]
        DeleteStockPurchase = 522,
        [Description("VIEW")]
        ViewStockPurchase = 523,

        [Description("ADD")]
        CreateStockReceiveInkind = 525,
        [Description("EDIT")]
        EditStockReceiveInkind = 526,
        [Description("DELETE")]
        DeleteStockReceiveInkind = 527,
        [Description("VIEW")]
        ViewStockReceiveInkind = 528,

        [Description("ADD")]
        CreateStockSales = 530,
        [Description("EDIT")]
        EditStockSales = 531,
        [Description("DELETE")]
        DeleteStockSales = 532,
        [Description("VIEW")]
        ViewStockSales = 533,

        [Description("ADD")]
        CreateStockUtilize = 535,
        [Description("EDIT")]
        EditStockUtilize = 536,
        [Description("DELETE")]
        DeleteStockUtilize = 537,
        [Description("VIEW")]
        ViewStockUtilize = 538,

        [Description("ADD")]
        CreateStockDispose = 540,
        [Description("EDIT")]
        EditStockDispose = 541,
        [Description("DELETE")]
        DeleteStockDispose = 542,
        [Description("VIEW")]
        ViewStockDispose = 543,

        [Description("ADD")]
        CreateStockItemTransfer = 545,
        [Description("EDIT")]
        EditStockItemTransfer = 546,
        [Description("DELETE")]
        DeleteStockItemTransfer = 547,
        [Description("VIEW")]
        ViewStockItemTransfer = 548,

        [Description("ADD")]
        CreateStockPurchaseReturn = 550,
        [Description("EDIT")]
        EditStockPurchaseReturn = 551,
        [Description("DELETE")]
        DeleteStockPurchaseReturn = 552,
        [Description("VIEW")]
        ViewStockPurchaseReturn = 553
    }

    public enum Prospect
    {
        [Description("ADD")]
        CreateProspect = 314,
        [Description("EDIT")]
        EditProspect = 315,
        [Description("DELETE")]
        DeleteProspect = 316,
        [Description("PRINT")]
        PrintProspect = 317,
        [Description("VIEW")]
        ViewProspect = 318,
        [Description("CONVERT")]
        ConvertProspect = 319,
        [Description("IMPORT")]
        ImportProspect = 320,
    }

    public enum MailTemplate
    {
        [Description("ADD")]
        CreateThanksgivingMail = 322,
        [Description("ADD")]
        CreateAppealMail = 323,
        [Description("ADD")]
        CreateNewsletterMail = 324,
        [Description("ADD")]
        CreateAnniversaryMail = 325,
        [Description("ADD")]
        CreateFeastDayMail = 326
    }

    public enum SMSTemplate
    {
        [Description("ADD")]
        CreateThanksgivingSMS = 328,
        [Description("ADD")]
        CreateAppealSMS = 329,
        [Description("ADD")]
        CreateAnniversarySMS = 330,
        [Description("ADD")]
        CreateFeastDaySMS = 331
    }

    public enum ThanksgivingMail
    {
        [Description("VIEW")]
        PreviewThanksgivingMail = 333,
        SendThanksgivingMail = 334
    }

    public enum AppealMail
    {
        [Description("VIEW")]
        PreviewAppealMail = 336,
        SendAppealMail = 337
    }

    public enum NewsletterMail
    {
        [Description("VIEW")]
        PreviewNewsletterMail = 339,
        SendNewsletterMail = 340
    }

    public enum AnniversaryMail
    {
        [Description("VIEW")]
        PreviewAnniversaryMail = 342,
        SendAnniversaryMail = 343
    }

    public enum FeastMail
    {
        [Description("VIEW")]
        PreviewFeastMail = 345,
        SendFeastMail = 346
    }


    public enum ThanksgivingSMS
    {
        [Description("VIEW")]
        PreviewThanksgivingSMS = 348,
        SendThanksgivingSMS = 349
    }

    public enum AppealSMS
    {
        [Description("VIEW")]
        PreviewAppealSMS = 351,
        SendAppealSMS = 352
    }

    public enum AnniversarySMS
    {
        [Description("VIEW")]
        PreviewAnniversarySMS = 354,
        SendAnniversarySMS = 355
    }

    public enum FeastSMS
    {
        [Description("VIEW")]
        PreviewFeastSMS = 357,
        SendFeastSMS = 358
    }

    public enum AssetClass
    {
        [Description("ADD")]
        CreateClass = 364,
        [Description("EDIT")]
        EditClass = 365,
        [Description("DELETE")]
        DeleteClass = 366,
        [Description("PRINT")]
        PrintClass = 367,
        [Description("VIEW")]
        ViewAssetClass = 368,
    }

    public enum AssetItem
    {
        [Description("ADD")]
        CreateItem = 372,
        [Description("EDIT")]
        EditItem = 373,
        [Description("DELETE")]
        DeleteItem = 374,
        [Description("VIEW")]
        ViewItem = 375
    }

    public enum Vendor
    {
        [Description("ADD")]
        CreateVendor = 377,
        [Description("EDIT")]
        EditVendor = 378,
        [Description("DELETE")]
        DeleteVendor = 379,
        [Description("VIEW")]
        ViewVendor = 380
    }

    public enum Manufacturer
    {
        [Description("ADD")]
        CreateManufacturer = 382,
        [Description("EDIT")]
        EditManufacturer = 383,
        [Description("DELETE")]
        DeleteManufacturer = 384,
        [Description("VIEW")]
        ViewManufacturer = 385
    }


    public enum Custodian
    {
        [Description("ADD")]
        CreateCustodian = 387,
        [Description("EDIT")]
        EditCustodian = 388,
        [Description("DELETE")]
        DeleteCustodian = 389,
        [Description("VIEW")]
        ViewCustodian = 390
    }

    public enum Block
    {
        [Description("ADD")]
        CreateBlock = 392,
        [Description("EDIT")]
        EditBlock = 393,
        [Description("DELETE")]
        DeleteBlock = 394,
        [Description("VIEW")]
        ViewBlock = 395
    }

    public enum LocationForm
    {
        [Description("ADD")]
        CreateLocation = 397,
        [Description("EDIT")]
        EditLocation = 398,
        [Description("DELETE")]
        DeleteLocation = 399,
        [Description("VIEW")]
        ViewLocation = 400
    }

    public enum UoM
    {
        [Description("ADD")]
        CreateUoM = 402,
        [Description("EDIT")]
        EditUoM = 403,
        [Description("DELETE")]
        DeleteUoM = 404,
        [Description("VIEW")]
        ViewUoM = 405
    }

    public enum Purchase
    {
        [Description("ADD")]
        CreatePurchase = 412,
        [Description("EDIT")]
        EditPurchase = 413,
        [Description("DELETE")]
        DeletePurchase = 414,
        [Description("VIEW")]
        ViewPurchase = 415
    }

    public enum ReceiveInKind
    {
        [Description("ADD")]
        CreateReceiveInKind = 417,
        [Description("EDIT")]
        EditReceiveInKind = 418,
        [Description("DELETE")]
        DeleteReceiveInKind = 419,
        [Description("VIEW")]
        ViewReceiveInKind = 420
    }

    public enum SalesDisposalDonation
    {
        [Description("ADD")]
        CreateSalesDisposalDonation = 422,
        [Description("EDIT")]
        EditSalesDisposalDonation = 423,
        [Description("DELETE")]
        DeleteSalesDisposalDonation = 424,
        [Description("VIEW")]
        ViewSalesDisposalDonation = 425
    }

    public enum Depreciation
    {
        [Description("ADD")]
        CreateDepreciation = 427,
        [Description("EDIT")]
        EditDepreciation = 428,
        [Description("DELETE")]
        DeleteDepreciation = 429,
        [Description("VIEW")]
        ViewDepreciation = 430
    }

    public enum PayrollGroup
    {
        [Description("ADD")]
        CreatePayrollGroup = 450,
        [Description("EDIT")]
        EditPayrollGroup = 451,
        [Description("DELETE")]
        DeletePayrollGroup = 452,
        [Description("VIEW")]
        ViewPayrollGroup = 453
    }

    public enum Staff
    {
        [Description("ADD")]
        CreateStaff = 455,
        [Description("EDIT")]
        EditStaff = 456,
        [Description("DELETE")]
        DeleteStaff = 457,
        [Description("VIEW")]
        ViewStaff = 458
    }

    public enum Payrollcomponent
    {
        [Description("ADD")]
        CreatePayrollComponent = 460,
        [Description("EDIT")]
        EditPayrollComponent = 461,
        [Description("DELETE")]
        DeletePayrollComponent = 462,
        [Description("VIEW")]
        ViewPayrollComponent = 463
    }

    public enum Loan
    {
        [Description("ADD")]
        CreateLoan = 465,
        [Description("EDIT")]
        EditLoan = 466,
        [Description("DELETE")]
        DeleteLoan = 467,
        [Description("VIEW")]
        ViewLoan = 468
    }
    public enum IssuesLoan
    {
        [Description("ADD")]
        CreateIssuesLoan = 470,
        [Description("EDIT")]
        EditIssuesLoan = 471,
        [Description("DELETE")]
        DeleteIssuesLoan = 472,
        [Description("VIEW")]
        ViewIssuesLoan = 473
    }

    // Stock
    public enum StockGroup
    {
        [Description("ADD")]
        CreateStockGroup = 481,
        [Description("EDIT")]
        EditStockGroup = 482,
        [Description("DELETE")]
        DeleteStockGroup = 483,
        [Description("VIEW")]
        ViewStockGroup = 484,
    }

    public enum StockItem
    {
        [Description("ADD")]
        CreateStockItem = 487,
        [Description("EDIT")]
        EditStockItem = 488,
        [Description("DELETE")]
        DeleteStockItem = 489,
        [Description("VIEW")]
        ViewStockItem = 490,
    }

    public enum StockVendor
    {
        [Description("ADD")]
        CreateStockVendor = 492,
        [Description("EDIT")]
        EditStockVendor = 493,
        [Description("DELETE")]
        DeleteStockVendor = 494,
        [Description("VIEW")]
        ViewStockVendor = 495,
    }

    public enum StockCustodian
    {
        [Description("ADD")]
        CreateStockCustodian = 497,
        [Description("EDIT")]
        EditStockCustodian = 498,
        [Description("DELETE")]
        DeleteStockCustodian = 499,
        [Description("VIEW")]
        ViewStockCustodian = 500,
    }

    public enum StockBlockArea
    {
        [Description("ADD")]
        CreateStockBlockArea = 501,
        [Description("EDIT")]
        EditStockBlockArea = 502,
        [Description("DELETE")]
        DeleteStockBlockArea = 503,
        [Description("VIEW")]
        ViewStockBlockArea = 504,
    }

    public enum StockLocation
    {
        [Description("ADD")]
        CreateStockLocation = 507,
        [Description("EDIT")]
        EditStockLocation = 508,
        [Description("DELETE")]
        DeleteStockLocation = 510,
        [Description("VIEW")]
        ViewStockLocation = 511,
    }

    public enum StockUOM
    {
        [Description("ADD")]
        CreateStockUOM = 512,
        [Description("EDIT")]
        EditStockUOM = 513,
        [Description("DELETE")]
        DeleteStockUOM = 514,
        [Description("VIEW")]
        ViewStockUOM = 515,
    }

    public enum StockPurchase
    {
        [Description("ADD")]
        CreateStockPurchase = 520,
        [Description("EDIT")]
        EditStockPurchase = 521,
        [Description("DELETE")]
        DeleteStockPurchase = 522,
        [Description("VIEW")]
        ViewStockPurchase = 523,
    }

    public enum StockReceiveInkind
    {
        [Description("ADD")]
        CreateStockReceiveInkind = 525,
        [Description("EDIT")]
        EditStockReceiveInkind = 526,
        [Description("DELETE")]
        DeleteStockReceiveInkind = 527,
        [Description("VIEW")]
        ViewStockReceiveInkind = 528,
    }

    public enum StockSales
    {
        [Description("ADD")]
        CreateStockSales = 530,
        [Description("EDIT")]
        EditStockSales = 531,
        [Description("DELETE")]
        DeleteStockSales = 532,
        [Description("VIEW")]
        ViewStockSales = 533,
    }

    public enum StockUtilize
    {
        [Description("ADD")]
        CreateStockUtilize = 535,
        [Description("EDIT")]
        EditStockUtilize = 536,
        [Description("DELETE")]
        DeleteStockUtilize = 537,
        [Description("VIEW")]
        ViewStockUtilize = 538,
    }

    public enum StockDispose
    {
        [Description("ADD")]
        CreateStockDispose = 540,
        [Description("EDIT")]
        EditStockDispose = 541,
        [Description("DELETE")]
        DeleteStockDispose = 542,
        [Description("VIEW")]
        ViewStockDispose = 543,
    }

    public enum StockItemTransfer
    {
        [Description("ADD")]
        CreateStockItemTransfer = 545,
        [Description("EDIT")]
        EditStockItemTransfer = 546,
        [Description("DELETE")]
        DeleteStockItemTransfer = 547,
        [Description("VIEW")]
        ViewStockItemTransfer = 548,
    }

    public enum StockPurchaseReturn
    {
        [Description("ADD")]
        CreateStockPurchaseReturn = 550,
        [Description("EDIT")]
        EditStockPurchaseReturn = 551,
        [Description("DELETE")]
        DeleteStockPurchaseReturn = 552,
        [Description("VIEW")]
        ViewStockPurchaseReturn = 553,
    }

    public enum OperationsList
    {
        ADD,
        EDIT,
        DELETE,
        PRINT,
        MOVE,
        VIEW,
        INSERT,
        NAGATIVEBALANCE,
        IMPORT,
        CONVERT
    }

    public enum Settings
    {
        UISettings = 3,
        GlobalSettings = 4
    }

    public enum AccountMapping
    {
        MapProject = 6,
        MapLedger = 7,
        MapCostCentre = 8,
        MapDonor = 9,
        MapVouchers = 10,
        MapCostCentreCategory = 266,
        MapFCPurpose = 267,
    }

    public enum TransactionPeriods
    {
        [Description("ADD")]
        CreateTransaction = 12,
        [Description("EDIT")]
        EditTransaction = 13,
        [Description("DELETE")]
        DeleteTransaction = 14,
        [Description("PRINT")]
        PrintTransaction = 15,
        [Description("VIEW")]
        ViewTransaction = 168
    }

    public enum LegalEntity
    {
        [Description("ADD")]
        CreateLegalEntity = 17,
        [Description("EDIT")]
        EditLegalEntity = 18,
        [Description("DELETE")]
        DeleteLegalEntity = 19,
        [Description("PRINT")]
        PrintLegalEntity = 20,
        [Description("VIEW")]
        ViewLedgalEntity = 169
    }

    public enum ProjectCategory
    {
        [Description("ADD")]
        CreateProjectCategory = 23,
        [Description("EDIT")]
        EditProjectCategory = 24,
        [Description("DELETE")]
        DeleteProjectCategory = 25,
        [Description("PRINT")]
        PrintProjectCategory = 26,
        [Description("VIEW")]
        ViewProjectCategory = 170
    }

    public enum CostCentreCategory
    {
        [Description("ADD")]
        CreateCostCentreCategory = 261,
        [Description("EDIT")]
        EditCostCentreCategory = 262,
        [Description("DELETE")]
        DeleteCostCentreCategory = 263,
        [Description("PRINT")]
        PrintCostCentreCategory = 264,
        [Description("VIEW")]
        ViewCostCentreCategory = 265
    }

    //public enum TDSSection
    //{
    //    [Description("ADD")]
    //    CreateTDSSection = 900,
    //    [Description("EDIT")]
    //    EditTDSSection = 901,
    //    [Description("DELETE")]
    //    DeleteTDSSection = 902,
    //    [Description("PRINT")]
    //    PrintTDSSection = 903,
    //    [Description("VIEW")]
    //    ViewTDSSection = 904

    //}

    public enum Project
    {
        [Description("ADD")]
        CreateProject = 28,
        [Description("EDIT")]
        EditProject = 29,
        [Description("DELETE")]
        DeleteProject = 30,
        [Description("PRINT")]
        PrintProject = 31,
        [Description("VIEW")]
        ViewProject = 171
    }


    public enum LegerGroup
    {
        [Description("ADD")]
        CreateLedgerGroup = 33,
        [Description("EDIT")]
        EditLedgerGroup = 34,
        [Description("DELETE")]
        DeleteLedgerGroup = 35,
        [Description("PRINT")]
        PrintLedgerGroup = 36,
        [Description("VIEW")]
        ViewLedgerGroup = 172
    }

    public enum Ledger
    {
        [Description("ADD")]
        CreateLedger = 38,
        [Description("EDIT")]
        EditLedger = 39,
        [Description("DELETE")]
        DeleteLedger = 40,
        [Description("PRINT")]
        PrintLedger = 41,
        [Description("VIEW")]
        ViewLedger = 173
    }

    public enum SundryCreditorsDebtorsLedger
    {
        [Description("ADD")]
        CreateSundryCreditorsDebtorsLedgers = 561,
        [Description("EDIT")]
        EditSundryCreditorsDebtorsLedgers = 562,
        [Description("DELETE")]
        DeleteSundryCreditorsDebtorsLedgers = 563,
        [Description("VIEW")]
        ViewSundryCreditorsDebtorsLedgers = 564
    }

    public enum BankAccount
    {
        [Description("ADD")]
        CreateBankAccount = 43,
        [Description("EDIT")]
        EditBankAccount = 44,
        [Description("DELETE")]
        DeleteBankAccount = 45,
        [Description("PRINT")]
        PrintBankAccount = 46,
        [Description("VIEW")]
        ViewBankAccounts = 174
    }

    public enum Voucher
    {
        [Description("ADD")]
        CreateVoucher = 48,
        [Description("EDIT")]
        EditVoucher = 49,
        [Description("DELETE")]
        DeleteVoucher = 50,
        [Description("PRINT")]
        PrintVoucher = 51,
        [Description("VIEW")]
        ViewVoucher = 175
    }

    public enum CostCentre
    {
        [Description("ADD")]
        CreateCostCentre = 53,
        [Description("EDIT")]
        EditCostCentre = 54,
        [Description("DELETE")]
        DeleteCostCentre = 55,
        [Description("PRINT")]
        PrintCostCentre = 56,
        [Description("VIEW")]
        ViewCostCentre = 176
    }

    public enum Bank
    {
        [Description("ADD")]
        CreateBank = 58,
        [Description("EDIT")]
        EditBank = 59,
        [Description("DELETE")]
        DeleteBank = 60,
        [Description("PRINT")]
        PrintBank = 61,
        [Description("VIEW")]
        ViewBank = 177
    }


    public enum GST
    {
        [Description("ADD")]
        CreateGST = 364,
        [Description("EDIT")]
        EditGSt = 365,
        [Description("DELETE")]
        DeleteGST = 366,
        [Description("PRINT")]
        PrintGST = 367,
        [Description("VIEW")]
        ViewGSt
    }

    public enum Conutry
    {
        [Description("ADD")]
        CreateCountry = 63,
        [Description("EDIT")]
        EditCountry = 64,
        [Description("DELETE")]
        DeleteCountry = 65,
        [Description("PRINT")]
        PrintCountry = 66,
        [Description("VIEW")]
        ViewCountry = 178
    }

    public enum AuditInfo
    {
        [Description("ADD")]
        CreateAuditInfo = 68,
        [Description("EDIT")]
        EditAuditInfo = 69,
        [Description("DELETE")]
        DeleteAuditInfo = 70,
        [Description("PRINT")]
        PrintAuditInfo = 71,
        [Description("VIEW")]
        ViewAuditInfo = 179
    }


    public enum GoverningMembers
    {
        [Description("ADD")]
        CreateGoverningMember = 73,
        [Description("EDIT")]
        EditGoverningMember = 74,
        [Description("DELETE")]
        DeleteGoverningMember = 75,
        [Description("PRINT")]
        PrintGoverningMember = 76,
        [Description("VIEW")]
        ViewGoverningMembers = 180
    }

    public enum Auditor
    {
        [Description("ADD")]
        CreateAuditor = 78,
        [Description("EDIT")]
        EditAuditor = 79,
        [Description("DELETE")]
        DeleteAuditor = 80,
        [Description("PRINT")]
        PrintAuditor = 81,
        [Description("VIEW")]
        ViewAuditor = 181
    }

    public enum Donor
    {
        [Description("ADD")]
        CreateDonor = 83,
        [Description("EDIT")]
        EditDonor = 84,
        [Description("DELETE")]
        DeleteDonor = 85,
        [Description("PRINT")]
        PrintDonor = 86,
        [Description("VIEW")]
        ViewDonor = 182
    }

    public enum Purpose
    {
        [Description("PRINT")]
        PrintPurpose = 88,
        [Description("VIEW")]
        ViewPurpose = 183
    }

    public enum Receipt
    {
        [Description("ADD")]
        CreateReceiptVoucher = 91,
        [Description("EDIT")]
        EditReceiptVoucher = 92,
        [Description("DELETE")]
        DeleteReceiptVoucher = 93,
        ViewReceiptVoucher = 94,
        [Description("PRINT")]
        PrintReceiptVoucher = 95,
        [Description("MOVE")]
        MoveReceiptVoucher = 109,
        [Description("INSERT")]
        InsertReceiptVoucher = 266,
        [Description("NAGATIVEBALANCE")]
        ShowReceiptNagativeBalance = 269,

    }


    public enum MoveTransForm
    {
        Transaction,
        Journal
    }

    public enum BulkMoveType
    {
        Delete,
        Move
    }

    public enum MultiMoveTransType
    {
        Single,
        Multiple
    }

    public enum Payment
    {
        [Description("ADD")]
        CreatePaymentVoucher = 98,
        [Description("EDIT")]
        EditPaymentVoucher = 99,
        [Description("DELETE")]
        DeletePaymentVoucher = 100,
        [Description("MOVE")]
        MovePaymentVoucher = 101,
        [Description("PRINT")]
        PrintPaymentVoucher = 102,
        [Description("VIEW")]
        ViewPaymentVoucher = 184,
        [Description("INSERT")]
        InsertPaymentVoucher = 267,
        [Description("NAGATIVEBALANCE")]
        ShowPaymentNagativeBalance = 270,
    }

    public enum Contra
    {
        [Description("ADD")]
        CreateContraVoucher = 104,
        [Description("EDIT")]
        EditContraVoucher = 105,
        [Description("DELETE")]
        DeleteContraVoucher = 106,
        [Description("MOVE")]
        MoveContraVoucher = 107,
        [Description("PRINT")]
        PrintContraVoucher = 108,
        [Description("VIEW")]
        ViewContraVoucher = 185,
        [Description("INSERT")]
        InsertContraVoucher = 268,
        [Description("NAGATIVEBALANCE")]
        ShowContraNagativeBalance = 271,
    }

    public enum Journal
    {
        [Description("ADD")]
        CreateJournalVoucher = 111,
        [Description("EDIT")]
        EditJournalVoucher = 112,
        [Description("DELETE")]
        DeleteJournalVoucher = 113,
        [Description("PRINT")]
        PrintJournalVoucher = 114,
        [Description("VIEW")]
        ViewJournalVoucher = 186
    }

    public enum Reconciliation
    {
        [Description("PRINT")]
        PrintBankReconciliation = 116,
        BankReconciled = 117,
        BankUnReconcilied = 118,
        BankCleared = 119,
        BankUnCleared = 120,
        [Description("VIEW")]
        ViewBankReconciliation = 187
    }

    public enum Budget
    {
        [Description("ADD")]
        CreateBudget = 122,
        [Description("EDIT")]
        EditBudget = 123,
        [Description("DELETE")]
        DeleteBudget = 124,
        [Description("PRINT")]
        PrintBudget = 125,
        [Description("VIEW")]
        ViewBudget = 188
    }

    public enum FDLedger
    {
        [Description("ADD")]
        CreateFDLedger = 127,
        [Description("EDIT")]
        EditFDLedger = 128,
        [Description("DELETE")]
        DeleteFDLedger = 129,
        [Description("PRINT")]
        PrintFDLedger = 130,
        [Description("VIEW")]
        ViewFDLedger = 189
    }

    public enum FDOpening
    {
        [Description("ADD")]
        CreateFixedDeposit = 132,
        [Description("EDIT")]
        EditFixedDeposit = 133,
        [Description("DELETE")]
        DeleteFixedDeposit = 134,
        [Description("PRINT")]
        PrintFixedDeposit = 135,
        [Description("VIEW")]
        ViewFixedDeposit = 190
    }

    public enum FDInvestment
    {
        [Description("ADD")]
        CreateFixedInvestment = 137,
        [Description("EDIT")]
        EditFixedInvestment = 138,
        [Description("DELETE")]
        DeleteFixedInvestment = 139,
        [Description("PRINT")]
        PrintFixedInvestment = 140,
        [Description("VIEW")]
        ViewFixedInvestment = 191
    }

    public enum Renewal
    {
        [Description("ADD")]
        RenewFixedDeposit = 142,
        [Description("EDIT")]
        ModifyFixedDepostRenewal = 143,
        [Description("DELETE")]
        DeleteFixedDepositRenewal = 144,
        [Description("PRINT")]
        PrintFixedDepositRenewal = 145,
        [Description("VIEW")]
        ViewFixedDepositRenewal = 192

    }
    public enum ReInvestment
    {
        [Description("ADD")]
        ReInvestmentFixedDeposit = 443,
        [Description("EDIT")]
        ModifyFixedDepostReInvestment = 444,
        [Description("DELETE")]
        DeleteFixedDepositReInvestment = 445,
        [Description("PRINT")]
        PrintFixedDepositReInvestment = 446,
    }

    public enum FDPostInterest
    {
        [Description("ADD")]
        PostInterestFixedDeposit = 438,
        [Description("EDIT")]
        ModifyPostInterestFixedDeposit = 439,
        [Description("DELETE")]
        DeletePostInterestFixedDeposit = 440,
        [Description("PRINT")]
        PrintFixedDepositPostInterest = 441

    }
    public enum Withdrewal
    {
        [Description("ADD")]
        WithdrawFixedDeposit = 147,
        [Description("PRINT")]
        PrintFixedDepositWithdraw = 148,
        [Description("VIEW")]
        ViewFixedDepositWithdraw = 193
    }

    public enum FDRegister
    {
        [Description("PRINT")]
        PrintFixedDepositRegister = 150,
        [Description("VIEW")]
        ViewFixedDepositRegister = 194
    }

    public enum User
    {
        [Description("ADD")]
        CreateUser = 153,
        [Description("EDIT")]
        EditUser = 154,
        [Description("DELETE")]
        DeleteUser = 155,
        [Description("PRINT")]
        PrintUser = 156,
        [Description("VIEW")]
        ViewUser = 167,
    }

    public enum UserRole
    {
        [Description("ADD")]
        CreateUserRole = 158,
        [Description("EDIT")]
        EditUserRole = 159,
        [Description("DELETE")]
        DeleteUserRole = 160,
        [Description("PRINT")]
        PrintUserRole = 161,
        [Description("VIEW")]
        ViewUserRole = 166,
    }

    public enum ManageSecurity
    {
        [Description("EDIT")]
        ResetPassword = 165,
        [Description("VIEW")]
        ViewManageSecurity = 195
    }

    public enum AuditLockTypes
    {
        [Description("ADD")]
        CreateLockType = 231,
        [Description("EDIT")]
        EditLockType = 232,
        [Description("DELETE")]
        DeleteLockType = 233,
        [Description("PRINT")]
        PrintLockType = 234,
        [Description("VIEW")]
        ViewLockType = 240
    }

    public enum AuditLockTrans
    {
        [Description("ADD")]
        CreateLockTrans = 236,
        [Description("EDIT")]
        EditLockTrans = 237,
        [Description("DELETE")]
        DeleteLockTrans = 238,
        [Description("PRINT")]
        PrintLockTrans = 239,
        [Description("VIEW")]
        ViewLockTrans = 241
    }

    public enum BudgetAnnual
    {
        [Description("ADD")]
        CreateBudgetAnnual = 243,
        [Description("EDIT")]
        EditBudgetAnnual = 244,
        [Description("DELETE")]
        DeleteBudgetAnnual = 245,
        [Description("PRINT")]
        PrintBudgetAnnual = 246,
        [Description("VIEW")]
        ViewBudgetAnnual = 247
    }

    public enum CostCentreCategories
    {
        [Description("ADD")]
        CreateCostCentreCategory = 249,
        [Description("EDIT")]
        EditCostCentreCategory = 250,
        [Description("DELETE")]
        DeleteCostCentreCategory = 251,
        [Description("PRINT")]
        PrintCostCentreCategory = 252,
        [Description("VIEW")]
        ViewCostCentreCategory = 253
    }
    public enum State
    {
        [Description("ADD")]
        CreateState = 255,
        [Description("EDIT")]
        EditState = 256,
        [Description("DELETE")]
        DeleteState = 257,
        [Description("PRINT")]
        PrintState = 258,
        [Description("VIEW")]
        ViewState = 259
    }
    public enum DashBoard
    {
        DashBoard = 213,
        ViewReceiptPayments = 214,
        ShowFDAlert = 215,
        ShowBankReconciliation = 216,
        ShowProjectDetails = 217
    }

    public enum AuditType
    {
        [Description("ADD")]
        CreateAuditType = 281,
        [Description("EDIT")]
        EditAuditType = 282,
        [Description("DELETE")]
        DeleteAuditType = 283,
        [Description("PRINT")]
        PrintAuditType = 284,
        [Description("VIEW")]
        ViewAuditType = 285
    }

    public enum TDSSection
    {
        [Description("ADD")]
        CreateTDSSection = 275,
        [Description("EDIT")]
        EditTDSSection = 276,
        [Description("DELETE")]
        DeleteTDSSection = 277,
        [Description("PRINT")]
        PrintTDSSection = 278,
        [Description("VIEW")]
        ViewTDSSection = 279
    }

    public enum TDSNatureofPayments
    {
        [Description("ADD")]
        CreateNatureofPayments = 287,
        [Description("EDIT")]
        EditNatureofPayments = 288,
        [Description("DELETE")]
        DeleteNatureofPayments = 289,
        [Description("PRINT")]
        PrintNatureofPayments = 290,
        [Description("VIEW")]
        ViewNatureofPayments = 291
    }

    public enum TDSDeducteeType
    {
        [Description("ADD")]
        CreateDeducteeType = 293,
        [Description("EDIT")]
        EditDeducteeType = 294,
        [Description("DELETE")]
        DeleteDeducteeType = 295,
        [Description("PRINT")]
        PrintDeducteeType = 296,
        [Description("VIEW")]
        ViewDeducteeType = 297
    }

    public enum TDSTaxPolicy
    {
        [Description("EDIT")]
        EditTDSPolicy = 299,
        [Description("DELETE")]
        DeleteTDSPolicy = 300,
        [Description("VIEW")]
        ViewTDSPolicy = 301
    }



    public enum TDSLedger
    {
        PrintTDSLedger = 303,
        ViewTDSLedger = 304

    }

    public enum TDSDutyTax
    {
        EditDutyTax = 306,
        PrintDutyTax = 307,
        ViewDutyTax = 308,

    }

    public enum TDSDeduction
    {
        TDSDeduction
    }

    public enum Reports
    {
        Reports = 196
    }

    public enum AdminUserId
    {
        UserId = 1
    }
    public enum TDSResidentialStatus
    {
        Residential = 0,
        Non_Residential = 1
    }
    public enum TDSDeducteeStatus
    {
        Company = 0,
        Non_Company = 1
    }

    public enum TDSPendingType
    {
        PartyPaymentPending,
        DeductTDSPending
    }

    public enum DataSync
    {
        HeadOffice,
        LegalEntity,
        ExecutiveMember,
        ProjectCategory,
        ProjectCategoryITRGroup,
        Project,
        LedgerGroup,
        Ledger,
        HeadOfficeLedger,
        FCPurpose,
        ParentGroup,
        Nature,
        MainGroup,
        Country,
        State,
        BudgetGroup,
        BudgetSubGroup,
        IsLedgerGroupCodeExists,
        IsLedgerGroupExists,
        IsProjectExists,
        IsProjectCodeExists,
        IsFCPurposeExists,
        ISFCPurposeCodeExists,
        isProjectCategoryExist,
        IsLegalEntityExist,
        IsExecutiveMemberExist,
        IsLedgerExists,
        IsHeadOfficeLedgerExists,
        IsHeadOfficeCodeExists,
        IsLedgerCodeExists,
        IsCashExists,
        IsFDExists,
        IsCapFundExists,
        IsBranchOfficeCodeExists,
        IsBranchOfficeExists,
        IsCountryExists,
        IsGeneralateLedgersExists,
        GeneralateLedger,
        GeneralateParent,
        GeneralateMainParent,

        //TDS
        IsTDSSectionExists,
        IsTDSNatureOfPaymentExists,
        IsDeducteeTypeExists,
        IsDutyTaxExists,
        IsTDSPolicyExists,
        TDSSection,
        NatureOfPayment,
        TDSDeducteeType,
        DutyTax,
        TDSPolicy
    }

    public enum DefaultLedgers
    {
        Cash = 1,
        FixedDeposit = 2,
        CapitalFund = 3
    }

    public enum LCBranchModuleStatus
    {
        Disabled = 0,
        Requested = 1,
        Approved = 2
    }

    public enum Moudule
    {
        Home = 0,
        DashBoard = 1,
        Finance = 2,
        Reports = 8,
        FixedAsset = 5,
        Payroll = 6,
        Settings = 9,
        Users = 10,
        Utlities = 12,
        DataSync = 21,
        TDS = 20,
        Stock = 35,
        Networking = 37,
    }

    public enum BranchUploadAction
    {
        BranchReport,
        BranchVouchers,
        BranchDatabase,
        BranchVoucherAttachFiles,
    }

    public enum ProjectTypes
    {
        New = 0,
        Exits = 1
    }

    public enum DataSyncMailType
    {
        Received = 1,
        InProgress = 2,
        Closed = 3,
        Failed = 4
    }

    public enum Natures
    {
        Income = 1,
        Expenses = 2,
        Assert = 3,
        Libilities = 4
    }
    public enum LedgerOptions
    {
        [Description("Enable CostCentre for Ledgers")]
        EnableCostCenter,
        [Description("Enable FD Interest Ledgers")]
        EnableBankFDInterestLedger,
        [Description("Enable FD Penalty Ledgers")]
        EnableBankFDPenaltyLedger,
        [Description("Enable SB Interest Ledgers")]
        EnableBankSBInterestLedger,
        [Description("Enable Bank Commission Ledgers")]
        EnableBankCommissionLedger,
        //[Description("Enable In- Kind Ledger Option")]
        //EnableInkindLedger,
        [Description("Enable Depreciation Ledger Option")]
        EnableDepreciationLedger,
        [Description("Enable Asset Gain Ledger Option")]
        EnableAssetGainLedger,
        [Description("Enable Asset Loss Ledger Option")]
        EnableAssetLossLedger,
        [Description("Enable Asset Disposal Ledger Option")]
        EnableAssetDisposalLedger,
        [Description("Enable Subsidy Ledger Option")]
        EnableSubsidyLedger,
        [Description("Enable GST Classification for Ledgers")]
        EnableGSTLedger,
        [Description("Enable High Value Payments")]
        EnableHighValuePayments,
        [Description("Enable Local Donation")]
        EnableLocalDonations
    }

    public enum TDSLedgerGroup
    {
        ExpensesLedger = 8,
        DutiesAndTax = 24,
        SundryCreditors = 26,
        FixedAsset = 18
    }

    public enum FinacialTransType
    {
        RC,
        PY,
        CN,
        JN
    }

    public enum Check
    {
        Select = 1
    }

    public enum TDSTransTypes
    {
        DeductTDS,
        TDSParty,
        TDSPayments
    }

    public enum UserRights
    {
        Admin = 1,
        Supervisor = 2
    }

    public enum UserVisibleOptions
    {
        DisableRights = 1
    }

    public enum TDSLedgerTypes
    {
        DirectExpense,
        SunderyCreditors,
        DutiesandTaxes
    }

    public enum TDSDefaultLedgers
    {
        DirectExpense = 8,
        SundryDebtors = 17,
        SunderyCreditors = 26,
        DutiesandTaxes = 24

    }

    public enum FDWithdrawalStatus
    {
        Active,
        Closed
    }
    public enum ItemKind
    {
        New = 1,
        Used = 2
    }
    public enum PurposeAct
    {
        CompaniesAct = 1,
        IncomeTaxAct = 2
    }
    public enum AssetAMCVoucher
    {
        Annual = 1,
        Monthly = 2
    }
    public enum FDRenewalCaption
    {
        Renew,
        Modify,
        Withdraw,
        Charge,
        Master,

    }
    public enum FDPostInterestCaption
    {
        [Description("Post Interest")]
        PostInterest,
        Modify,
        Withdraw,
        Master
    }
    public enum TaxPolicyId
    {
        TDSWithPAN = 1,
        TDSWithoutPAN = 2,
        Surcharge = 3,
        EdCess = 4,
        SecEdCess = 5
    }

    public enum BorderStyleCell
    {
        Regular = 0,
        Bold = 1
    }
    public enum TDSRateWithoutPANNo
    {
        TDSRate = 20
    }
    public enum TDSPayTypes
    {
        PartyPayment,
        TDSPayment
    }

    public enum Association
    {
        Cultural = 0,
        Economic = 1,
        Educational = 2,
        Religious = 3,
        Social = 4,
        Others = 5
    }

    public enum SetDefaultValue
    {
        DefaultValue = 1,
        DisableValue = 0
    }

    public enum TDSTransType
    {
        TDSBooking,
        TDSPartyPayment,
        TDSPayment
    }

    public enum TDSLedgerType
    {
        Interest = 1,
        Penalty = 2
    }
    public enum ReportModule
    {
        Finance = 1,
        TDS = 2,
        Payroll = 3,
        FixedAsset = 4,
        Stock = 5,
        NetWorking = 6,
    }

    public enum TDSPartyPayment
    {
        MakeTDSBookingAtPayment
    }
    public enum Result
    {
        Success = 1,
        Failure
    }
    public enum StaffStatus
    {
        InService = 1,
        Outof = 2,
        All = 3
    }

    public enum AuditLockType
    {
        Edit,
        Delete
    }

    public enum BulkTransaction
    {
        Delete = 0,
        Move = 1
    }

    public enum PortalUpdates
    {
        ImportMasters,
        UploadVouchers,
        UpdateLicense,
        ImportTDSMasters
    }

    public enum PortalMessages
    {
        DataSynMessage = 0,
        AmendmentMessage = 1,
        BroadCastMessage = 2,
        Tickets = 3
    }
    public enum LocationType
    {
        HeadOffice = 1,
        BranchOffice = 2
    }

    public enum UnitOfMeasureType
    {
        Simple = 1,
        Compound = 2
    }

    public enum AssetItemManualType
    {
        Automatic = 1,
        Manual = 2
    }

    public enum FinanceModule
    {
        Asset,
        Stock
    }
    public enum VendorManufacture
    {
        Vendor,
        Manufacture,
    }
    public enum AssetCategory
    {
        Primary
    }
    public enum DefaultLocation
    {
        Primary
    }

    public enum DonationVoucherPrint
    {
        Receipt = 1,
        DonorReceipt = 2
    }

    public enum Processtype
    {
        [Description("Salary")]
        SalaryPayable = 0,
        [Description("PF Contribution")]
        PFPayable = 1,
        [Description("ESI Contribution")]
        ESIPayable = 2,
        [Description("Deductions")]
        LoanDeduction = 3

    }

    public enum ImportType
    {
        SplitProject,
        SubBranch,
        HeadOffice,
        SplitHOBranchProject
    }

    public enum CustodiansType
    {
        Owner = 1,
        Custodian = 2
    }

    public enum StockSalesTransType
    {
        Sold = 0,
        Utilized = 1,
        Disposal = 2
    }
    public enum StockPurchaseTransType
    {
        Purchase = 0,
        Receive = 1
    }

    public enum AssetSourceFlag
    {
        OpeningBalance = 1,
        Purchase = 2,
        Sales = 3,
        Receive = 4,
        Disposal = 5,
        Donation = 6
    }

    public enum StockReturnType
    {
        InWards = 0,
        OutWards = 1
    }

    public enum StockType
    {
        Purchase = 0,
        Return = 0,
        Sales = 1,
        Utilized = 1,
        Disposal = 1,
        PurchaseReturns = 2
    }

    public enum PayRoll
    {
        PostVoucher,
        IssueLoan
    }

    public enum PayrollSetting
    {
        LOPMode,
        LOPAutomaticTotalDaysOption,
        LOPAutomaticCustomTotalDays,
        LOPAutomaticTotalDaysComponent,
        LOPAutomaticShowProcessedValues
    }

    public enum PayRollExtraPayInfo
    {
        BASICPAY,
        EARNING1,
        EARNING2,
        EARNING3,
        DEDUCTION1,
        DEDUCTION2,
        PAYING_SALARY_DAYS,
        YOS,
        SCALEOFPAY,
        TOTALDAYSINPAYMONTH,
    }

    public enum PayRollDefaultComponent
    {
        [Description("BASIC")]
        BASIC,
        [Description("DA")]
        DA,
        [Description("HRA")]
        HRA,
        [Description("PF WAGES")]
        PF_WAGES,
        [Description("PF")]
        PF,
        [Description("PT")]
        PT,
        [Description("GROSS WAGES")]
        GROSS_WAGES,
        [Description("DEDUCTIONS")]
        DEDUCTIONS,
        [Description("NETPAY")]
        NETPAY,
        [Description("NAME")]
        NAME,
        [Description("DESIGNATION")]
        DESIGNATION,
        [Description("TOTALDAYS")]
        TOTALDAYS,
        [Description("WORKINGDAYS")]
        WORKINGDAYS,
        [Description("LEAVEDAYS")]
        LEAVEDAYS,
        [Description("LOPDAYS")]
        LOPDAYS,
    }

    public enum PayRollProcessComponent
    {
        None = 0,
        NetPay = 1,
        GrossWages = 2,
        Deductions = 3,
        EPF = 4,
        PT = 5,
        ESI = 6
    }

    public enum DatePickerType
    {
        VoucherDate,
        ChangePeriod
    }

    public enum ModuleActivities
    {
        Abstract = 197,
        CostCentre = 202,
        BookofAccounts = 199,
        Budget = 204,
        ForeignContribution = 201,
        FinalAccounts = 200,
        FinancialRecords = 203,
        AssetReports = 436,
        StockReports = 502,
        PayrollReports = 602,
        Networking = 702
    }

    public enum TicketPriority
    {
        High = 1,
        Medium = 2,
        Low = 3,
    }

    public enum AssetInOut
    {
        OP,
        PU,
        IK,
        SL,
        DS,
        DN,
        AMC,
        GAIN,
        LOSS,
        INS
    }
    public enum FixedAssetDefaultUOM
    {
        Nos = 1
    }
    public enum FixedAssetDefaultLedger
    {
        [Description("Fixed Asset Ledger")]
        FixedAssetLedger = 1
    }

    public enum FixedAssetClass
    {
        [Description("Office Equipments")]
        OfficeEquipments = 9,
        [Description("Primary")]
        Primary = 1
    }

    public enum FixedAssetCustodian
    {
        Unknown = 0
    }
    public enum FixedAssetBlock
    {
        Unknown = 0
    }
    public enum MaritalStatus
    {
        Single = 0,
        Married = 1
    }
    public enum GSTType
    {
        Goods = 0,
        Services = 1
    }

    public enum GSTInvouceStatus
    {
        Cancelled = 0,
        Active = 1,
        Closed = 2
    }

    public enum GSTDefaultClass
    {
        [Description("GST0")]
        GST0,
        [Description("GST05")]
        GST05,
        [Description("GST12")]
        GST12,
        [Description("GST18")]
        GST18,
        [Description("GST28")]
        GST28
    }

    public enum ECSDuration
    {
        [Description("Monthly")]
        Monthly = 1,
        [Description("Quaterly")]
        Quaterly = 2,
        [Description("Half-Yearly")]
        HalfYearly = 3,
        [Description("Yearly")]
        Yearly = 4

    }
    public enum NameTitle
    {
        Mr = 0,
        Ms = 1,
        Mrs = 2,
        Dr = 3,
        Prof = 4,
        Fr = 5,
        Sr = 6,
        Br = 7
    }

    public enum DonorMailTemplate
    {
        Thanksgiving = 1,
        Appeal = 2,
        NewsLetter = 3,
        Anniversary = 4,
        Tasks = 5
    }

    public enum PreviewType
    {
        Print,
        Email
    }

    public enum AnniversaryType
    {
        Birthday = 0,
        Wedding = 1
    }

    public enum MailStatus
    {
        [Description("Not Send")]
        NotSend = 0,
        [Description("Sent")]
        Sent = 1,
        [Description("All")]
        All = 2
    }

    public enum CommunicationMode
    {
        MailDesk = 1,
        ContactDesk = 2
    }

    public enum DepreciationMethods
    {
        [Description("Straight Line Method")]
        SLV = 1,
        [Description("Written Down Method")]
        WDV = 2
    }

    public enum MemberType
    {
        Donor,
        Prospect
    }

    public enum AcmeerpModules
    {
        [Description("Finance")]
        AcmeerpFinance = 1,
        [Description("Statutory")]
        AcmeerpTDS = 2,
        [Description("Fixed Asset")]
        AcmeerpFixedAsset = 3,
        [Description("Payroll")]
        AcmeerpPayroll = 4,
        [Description("Stock")]
        AcmeerpStock = 5,
        [Description("Networking")]
        AcmeerpNetworking = 6,
        [Description("Reports")]
        AcmeerpReports = 7
    }

    public enum AuditAction
    {
        Created,
        Modified,
        Deleted
    }

    public enum ReportCodeType
    {
        Standard = 0,
        Province = 1,
        Generalate = 2
    }



}
