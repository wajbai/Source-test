/*  Class Name      : ImportVoucherSystem.cs
 *  Purpose         : Synchronize Branch Office Data to Portal.
 *  Author          : Salamon Raj M
 *  Created on      : 20-June-2014
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Utility;
using Bosco.Utility.ConfigSetting;
using System.Text.RegularExpressions;

namespace AcMEDSync.Model
{
    public class ImportVoucherSystem : DsyncSystemBase
    {
        #region Decelaration
        ResultArgs resultArgs = null;
        public string Caption = string.Empty;
        public event EventHandler SynchronizeHeld;
        CommonMethod common = new CommonMethod();
        //Added by Carmel Raj on August-18-2015
        Dictionary<int, int> FDVoucherDetails;
        Dictionary<int, int> FDAccountIdDetails;
        private const string PROJECT_TABLE_NAME = "Project";
        private const string VOUCHER_MASTER_TABLE_NAME = "VoucherMasters";
        private const string VOUCHER_TRANS_TABLE_NAME = "VoucherTransaction";
        private const string VOUCHER_COSTCENTRE_TABLE_NAME = "VoucherCostCentre";
        private const string DONOR_TABLE_NAME = "Donors";
        private const string COUNTRY_TABLE_NAME = "Country";
        private const string STATE_TABLE_NAME = "State";
        private const string LEDGER_GROUP_TABLE_NAME = "LedgerGroup";
        private const string LEDGER_TABLE_NAME = "Ledger";
        private const string LEDGER_PROFILE_TABLE_NAME = "LedgerProfile";
        private const string BANKACCOUNT_TABLE_NAME = "BankDetails";
        private const string LEDGER_BALANCE_TABLE_NAME = "LedgerBalance";
        private const string HEADER_TABLE_NAME = "Header";
        private const string FD_BANK_TABLE_NAME = "FD_Bank_Details";
        private const string FD_BANK_ACCOUNT_TABLE_NAME = "FD_Bank_Account_Details";
        private const string FD_ACCOUNT_TABLE_NAME = "FD_Investment_Account";
        private const string FD_RENEWAL_TABLE_NAME = "FD_Renewal";
        private const string FD_VOUCHER_MASTER_TRANS_TABLE_NAME = "FD_Voucher_Master_Trans";
        private const string FD_VOUCHER_TRANS_TABLE_NAME = "FD_Voucher_Trans";
        //   private const string MERGE_PROJECT_TABLE_NAME = "MergeProject";
        private const string MERGE_PROJECT_ID = "MERGE_PROJECT_ID";
        private const string LEGAL_ENTITY_TABLE_NAME = "LegalEntity";
        private const string PROJECT_LEDGERS = "Project_Ledgers";
        private const string PROJECT_COST_CENTRES = "Project_CostCentres";
        private const string PROJECT_DONORS = "Project_Donors";
        public const string ASSET_STOCK_VENDORS = "Asset_Stock_Vendor";
        private const string MASTER_GST_CLASS = "Master_GST_Class";

        //On 15/06/2021, Import Budget Module 
        public const string BUDGET_MASTER = "Budget_Master";
        public const string BUDGET_PROJECT = "Budget_Project";
        public const string BUDGET_LEDGER = "Budget_Ledger";
        public const string BUDGET_STATISTICS_DETAILS = "Budget_Statistics_Details";
        public const string BUDGET_PROJECT_LEDGER = "Budget_Project_Ledger";
        public const string BUDGET_STATISTICS_TYPE = "Budget_Statistics_Type";
        public const string BUDGET_TYPE = "Budget_Type";
        public const string BUDGET_LEVEL = "Budget_Level";

        //On 05/02/2024, For GST Module
        public const string GST_INVOICE_MASTER = "GST_Invoice_Master";
        public const string GST_INVOICE_MASTER_LEDGER_DETAIL = "GST_Invoice_Master_Ledger_Detail";
        public const string GST_INVOICE_VOUCHER = "GST_Invoice_Voucher";


        public const string FINANCE_SETTING = "Finance_Setting";
        #endregion

        #region Constructor
        public ImportVoucherSystem()
        {

        }
        #endregion

        #region Properties
        private DataTable dtLegalEntity { get; set; }
        private DataTable dtProject { get; set; }
        private DataTable dtHeader { get; set; }
        private DataTable dtVoucherMaster { get; set; }
        private DataTable dtVoucherTrans { get; set; }
        private DataTable dtVoucherCostCentre { get; set; }
        private DataTable dtDonor { get; set; }
        private DataTable dtCountry { get; set; }
        private DataTable dtState { get; set; }
        private DataTable dtLedgerGroup { get; set; }
        private DataTable dtLedger { get; set; }
        private DataTable dtBankAccount { get; set; }
        private DataTable dtLedgerBalance { get; set; }
        private DataTable dtLedgerProfile { get; set; }
        private DataTable dtFDBank { get; set; }
        private DataTable dtFDBankAccount { get; set; }
        private DataTable dtFDAccount { get; set; }
        private DataTable dtFDRenewal { get; set; }
        private DataTable dtFDVoucherMasterTrans { get; set; }
        private DataTable dtFDVoucherTrans { get; set; }
        private DataTable dtVoucherSubLedger { get; set; }
        private DataTable dtProjectLedgers { get; set; }
        private DataTable dtProjectCostCentres { get; set; }
        private DataTable dtProjectDonors { get; set; }
        private DataTable dtSubLedger { get; set; }
        private DataTable dtGSTClass { get; set; }
        private DataTable dtAssetStockVendors { get; set; }


        //On 15/06/2021, Budget Module 
        private DataTable dtBudgetMaster { get; set; }
        private DataTable dtBudgetLedger { get; set; }
        private DataTable dtBudgetProject { get; set; }
        private DataTable dtBudgetStatisticsDetails { get; set; }
        private DataTable dtBudgetProjectLedger { get; set; }
        private DataTable dtBudgetStatisticsType { get; set; }
        private DataTable dtBudgetLevel { get; set; }
        private DataTable dtBudgetType { get; set; }

        //On 05/02/2024, GST Invoice dtails
        private DataTable dtGSTInvoiceMaster { get; set; }
        private DataTable dtGSTInvoiceMasterLedgerDetail { get; set; }
        private DataTable dtGSTInvoiceVoucher { get; set; }
        private Int32 GSTMasterBillingCountryId { get; set; }
        private Int32 GSTMasterBillingStateId { get; set; }
        private Int32 GSTMasterShippingCountryId { get; set; }
        private Int32 GSTMasterShippingStateId { get; set; }

        public DataTable dtFinanceSettings { get; set; }

        public bool CanOverride { get; set; }
        public int MergerProjectId { get; set; }
        public bool ImportHeadOfficeBranchData = false;
        public string MergeProject { get; set; }
        private DateTime DateFrom { get; set; }
        private DateTime DateTo { get; set; }
        public ImportType VoucherImportType = ImportType.HeadOffice;
        public String HeadOfficeCodeSplitProject { get; set; }

        //01/07/2020, Project split is just between date range or FYs split
        public bool IsFYSplit = false;
        public DateTime FYDateFrom;
        public DateTime FYDateTo;
        public DateTime FYYearFrom;
        public string FYProjectName = string.Empty;
        public Int32 FYProjectId = 0;
        public bool FYProjectVoucherExistsInDB = false;
        public bool FYProjectFDVoucherExistsInDB = false;
        public bool FYProjectFDVoucherExistsInXML = false;
        public DateTime FYProjectFDVoucherFirstDateInXML;
        public bool FYProjectBudgetExistsInXML = false;
        public bool FYProjectGSTExistsInXML = false;
        public DateTime FYProjectFirstVoucherDateInDB;
        public DateTime FYProjectLastVoucherDateInDB;
        public bool MismatchBudgetProjectsWithDBProjects = false;
        public int NoOfBudgetProjects = 0;
        public bool IsGSTEnabled = false;
        public bool IsGSTVendorDetailsEnabled = false;
        public bool Include_AL_LedgerOpeningBalance = false;
        public bool With_FD_Vouchers = false;
        public bool With_Budget = false;

        //03/01/2022, to show ledger mapping
        public DataTable Ledgers = null;
        public DataTable MappedLedgers = null;
        public bool IsShowMappingLedgers = false;

        public Int32 CostCentreMapping = 0;

        /// <summary>
        /// 22/02/2018, When import voucher/import project, if error occured, we need to show which data occurs error,
        /// for exmaple, name of the project, name of the master, date of voucher etc.
        /// This details will be added into the mail when voucher is exporting to portal
        /// </summary>
        public string ProblemDataDetails { get; set; }

        //Project Property 
        public string ProjectCode { get; set; }
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public DateTime AccountDate { get; set; }
        public DateTime StartedOn { get; set; }
        //Modified by Carmel Raj on 7 th October
        //Purpose : To hold null valu when project closing date is empty
        public DateTime? Closed_On { get; set; }
        public int ProjectClosedBy { get; set; }
        public string ClosedOn { get; set; }
        public string Description { get; set; }
        private string ProjectName { get; set; }
        private string ProjectCategoryName { get; set; }
        private string ProjectIds { get; set; }
        public string Notes { get; set; }
        public int ProjectCategroyId { get; set; }
        public int LegalEntityId { get; set; }

        //Legal Entity Property
        private string SocietyName { get; set; } //12/04/2017, for check ledgal entity exists

        //Purpose Property
        private string PurposeCode { get; set; }
        private string PurposeName { get; set; }

        //Bank Properties
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string BankCode { get; set; }
        public string Branch { get; set; }
        public string Address { get; set; }
        public string IFSCCode { get; set; }
        public string MICRCode { get; set; }
        public string ContactNumber { get; set; }
        public string SWIFTCode { get; set; }
        public DateTime DateOpened { get; set; }
        public DateTime DateClosed { get; set; }

        //Bank Account Properties
        private int BankAccountId { get; set; }
        private string AccountCode { get; set; }
        private string AccountNumber { get; set; }
        private int Is_FCRA_Account { get; set; }
        private string AccountHolderName { get; set; }

        //Donor Properties
        public string DonorName { get; set; }
        public string Place { get; set; }
        public int DonorType { get; set; }
        public string CompanyName { get; set; }
        public string Pincode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public int IdentityKey { get; set; }
        public string URL { get; set; }
        public string DonorStateName { get; set; }
        public string DonorAddress { get; set; }
        public string PAN { get; set; }

        //Ledger Group Property
        private string Abbrevation { get; set; }
        private string GroupCode { get; set; }
        private string GroupName { get; set; }
        private int ParentGroupId { get; set; }
        private int NatureId { get; set; }
        private int MainGroupId { get; set; }
        private string ParentGroup { get; set; }
        private string Nature { get; set; }
        private string MainGroup { get; set; }
        private int SortOrder { get; set; }
        private int ImageId { get; set; }

        private bool IsTargetDBNew = false;
        //private bool VoucherAutoNumberUpdated = false;


        //Ledger Property
        string lname = string.Empty;
        private string LedgerName
        {
            get
            {
                return lname;
            }
            set
            {
                lname = value;
                //On 04/01/2022, to get mapped project imported ledger
                if (IsShowMappingLedgers)
                {
                    lname = ReplaceMappedLedgerName(lname);
                }
            }
        }


        private int AccountType { get; set; }
        private string LedgerCode { get; set; }
        private string LedgerGroup { get; set; }
        private string LedType { get; set; }
        private string LedSubType { get; set; }
        private int GroupId { get; set; }
        private int SortId { get; set; }
        public int IsBranchLedger { get; set; }
        private string LedgerNotes { get; set; }
        public string FDSubTypes { get; set; }
        public string MFFolioNo { get; set; }
        public string MFSchemeName { get; set; }
        public double MFNAVperUnit { get; set; }
        public double MFNAVUnits { get; set; }
        public int MFModeOfHodling { get; set; }

        //Added by Carmel Raj on August-18-2015
        public int IsCostCentre { get; set; }
        public int IsBankInterestLedger { get; set; }
        public int IsTDSLedger { get; set; }
        public int IsInKindLedger { get; set; }
        public int IsDepreciationLedger { get; set; }
        public int IsAssetGainLedger { get; set; }
        public int IsAssetLossLedger { get; set; }
        public int IsDisposalLedger { get; set; }
        public int IsSubSidyLedger { get; set; }
        public int IsGSTLedger { get; set; }
        public int GSTServiceTyper { get; set; }
        public int BudgetGroupId { get; set; }
        public int BudgetSubGroupId { get; set; }
        public DateTime LedgerDateClosed { get; set; }
        public int LedgerClosedBy { get; set; }
        public int FDInvestmentTypeId { get; set; }

        public int IsBankSBInterestLedger { get; set; }
        public int IsBankCommissionInterestLedger { get; set; }
        public int IsBankPenaltyInterestLedger { get; set; }
        public string GST_HSN_SAC_CODE { get; set; }

        //Country
        private string CountryName { get; set; }
        private string CurrencyCountry { get; set; }
        private string ExchangeCountry { get; set; }
        public int CountryId { get; set; }
        public string CountryCode { get; set; }
        public new string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
        public string CurrencyName { get; set; }

        //State
        public int StateId { get; set; }
        public string StateCode { get; set; }
        private string StateName { get; set; }
        public int StateCountryId { get; set; }

        //Voucher Master Properties
        public int VoucherId { get; set; }
        public int LastVoucherId { get; set; }
        public DateTime VoucherDate { get; set; }
        public int ProjectId { get; set; }
        public int TransVoucherMethod { get; set; }
        public string VoucherNo { get; set; }
        public string VoucherType { get; set; }
        public string VoucherSubType { get; set; }
        public Int32 VoucherDefinitionId { get; set; }
        public int DonorId { get; set; }
        public int PurposeId { get; set; }
        public string ContributionType { get; set; }
        public decimal ContributionAmount { get; set; }
        public int CurrencyCountryId { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal CalculatedAmount { get; set; }
        public decimal ActualAmount { get; set; }
        public string ClientCode { get; set; }
        public int ClientRefId { get; set; }
        public int ExchageCountryId { get; set; }
        public string Narration { get; set; }
        public string NameAddress { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public string CreatedByName { get; set; }
        public string ModifiedByName { get; set; }
        public int IsCashBankstatus { get; set; }
        public Nullable<int> GSTVendorId { get; set; }
        public string GSTVendorInvoiceNo { get; set; }
        public int GSTVendorInvoiceType { get; set; }
        public Nullable<DateTime> GSTVendorInvoiceDate { get; set; }


        //Voucher Trans Properties
        public int LedgerId { get; set; }
        public int SequenceNo { get; set; }
        public decimal Amount { get; set; }
        public string TransMode { get; set; }
        public string ChequeNo { get; set; }
        public string MaterializedOn { get; set; }
        public string ChequeRefDate { get; set; }
        public string ChequeRefBankName { get; set; }
        public string ChequeRefBankBranch { get; set; }
        public string ChequeRefFundTransferTypeName { get; set; }
        public string LedgerNarration { get; set; }
        public Nullable<int> LedgerGSTClassId { get; set; }

        //Voucher Cost Centre Properties
        private string CostCentreName { get; set; }
        private string CostCategory { get; set; }
        public int CostCenterId { get; set; }
        public int ContributionId { get; set; }
        public int CostCategoryId { get; set; }
        public decimal CostCentreAmount { get; set; }
        public int CostCentreSequenceNo { get; set; }
        public int LedgerCostCentreSequenceNo { get; set; }

        //17/02/2020 Voucher Sub Ledger Properties
        public decimal VoucherSubLedgerAmount { get; set; }
        public int VoucherSubLedgerSequenceNo { get; set; }
        public string VoucherSubLedgerTransMode { get; set; }

        //Ledger Balance Properties
        private DateTime BalanceDate { get; set; }
        private Double BalanceAmount { get; set; }
        private string BalanceTransMode { get; set; }
        private string BalanceTransFlag { get; set; }


        //17/02/2020 Sub Ledger Properties 
        private int SubLedgerId { get; set; }
        private string SubLedgerName { get; set; }

        //DataSync Status
        private int BranchOfficeId { get; set; }
        private string BranchEmailId { get; set; }
        private string BranchOfficeName { get; set; }
        private int HeadOfficeId { get; set; }
        private string XmlFileName { get; set; }
        private string Remarks { get; set; }

        //Head Office Portal
        private int BranchId { get; set; }
        private string Location { get; set; }
        private int LocationId { get; set; }
        private string HODBConnection { get; set; }
        private string HeadOfficeBranchCode { get; set; }
        private string HeadOfficeCode { get; set; }

        //FD Account
        public int FDAccountId { get; set; }
        public string FDAccountNumber { get; set; }
        public int FDSchemeType { get; set; }
        public string FDAccountHolderName { get; set; }
        public double FdAmount { get; set; }
        public double FDInterestAmount { get; set; }
        public double FDInterestRate { get; set; }
        public double FDPrinicipalAmount { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime FDMaturityDate { get; set; }
        public DateTime FDClosedDate { get; set; }
        public string FDTransType { get; set; }
        public string FDTransMode { get; set; }
        public string FDProjectName { get; set; }
        public string FDLedgerCurBalance { get; set; }
        public int CashBankId { get; set; }
        public string ReceiptNo { get; set; }
        public string FDVoucherNo { get; set; }
        public string FDOPInvestmentDate { get; set; }
        public DataTable dtFDAcountDetails { get; set; }
        public DataTable dtCashBankLedger { get; set; }
        public DataTable dtFDLedger { get; set; }
        public string CashBankLedgerGroup { get; set; }
        public string CashBankLedgerAmt { get; set; }
        public string FDLedgerAmt { get; set; }
        public int PrevProjectId { get; set; }
        public int PrevLedgerId { get; set; }
        public int VoucherIntrestMode { get; set; }
        public int FDStatus { get; set; }
        public int InterestType { get; set; }

        //On 20/05/2022, to Assign Pentaly details
        public double TDSAmount { get; set; }
        public Int32 ChargeMode { get; set; } //0 - None 1 - Deduct form Principal, 2- Deduct form Interest
        public double ChargeAmount { get; set; }
        public Int32 ChargeLedgerId { get; set; }


        //FD Renewal
        public double IntrestAmount { get; set; }
        public double WithdrawAmount { get; set; }
        public double ReInvestedAmount { get; set; }
        public int IntrestLedgerId { get; set; }
        public int BankLedgerId { get; set; }
        public int FDIntrestVoucherId { get; set; }
        public DateTime RenewedDate { get; set; }
        public DateTime WithdrawDate { get; set; }
        public string RenewalType { get; set; }
        public double IntrestRate { get; set; }
        public int FDRenewalStatus { get; set; }
        public int FDLedgerId { get; set; }
        public string FDRenewalType { get; set; }
        public double PrinicipalAmount { get; set; }
        public double FDInterstCalAmount { get; set; }
        public double FDCashBankWithdrawAmount { get; set; }
        public int FDRenewalInterestId { get; set; }
        public double PrinicipalInsAmount { get; set; }
        public int FDRenewalId { get; set; }
        public string FDType { get; set; }
        public string FDRenewalTransMode { get; set; }

        //Budget Properties
        string BudgetName { get; set; }
        private DateTime BudgetDateFrom { get; set; }
        private DateTime BudgetDateTo { get; set; }
        private string BudgetProjectIds { get; set; }
        private int BudgetTypeId { get; set; }
        private int StatisticsTypeId { get; set; }
        private string StatisticsTypeName { get; set; }
        public bool IsMappingMandatory = false; //Mapping mandatory or not

        //GST Master Class
        private int GSTId { get; set; }
        private string Slab { get; set; }
        private double GST { get; set; }
        private double CGST { get; set; }
        private double SGST { get; set; }
        private double IGST { get; set; }
        private DateTime GSTClassApplicableFrom { get; set; }
        private int GSTClassStatus { get; set; }
        private int GSTClassSortOrder { get; set; }

        //Ledger Profile details
        private int LedgerProfileNatureofPaymentid { get; set; }
        private int LedgerProfileNatureofPayment { get; set; }
        private int LedgerProfileDeduteeTypeId { get; set; }

        //On 09/07/2021, Asset Stock Vendor Details
        private int VendorId { get; set; }
        private string Vendor { get; set; }
        private string VendorAddress { get; set; }
        private string VendorPanNo { get; set; }
        private string VendorGSTNO { get; set; }
        private string VendorContactNo { get; set; }
        private string VendorEmailId { get; set; }
        private int VendorBranchId { get; set; }

        private int LedgerProfileGSTID { get; set; }
        private string LedgerProfileDeduteeType { get; set; }
        private string LedgerProfileName { get; set; }
        private string LedgerProfileAddress { get; set; }
        private string LedgerProfilePincode { get; set; }
        private string LedgerProfileContactPerson { get; set; }
        private string LedgerProfileContactNumber { get; set; }
        private string LedgerProfileEmail { get; set; }
        private string LedgerProfilePAN { get; set; }
        private string LedgerProfileNameOnPAN { get; set; }
        private string LedgerProfileGSTNO { get; set; }

        #endregion

        #region Methods
        #region Public Method
        /// <summary>
        /// Entry Point of Data Synchronization. This will be called by data sync service 
        /// This is run in ACMEERP portal server or multi location branch admin system
        /// This is Main method to do all synchronization and calls corrsponding the methods
        /// </summary>
        /// <param name="VoucherXml">Branch Office XML File</param>
        /// <returns>ResultArgs with its properties</returns>
        public ResultArgs ImportVouchers(string VoucherXml)
        {
            bool isBeginTransaction = false;
            ProblemDataDetails = string.Empty;
            using (DataManager dataManager = new DataManager())
            {
                try
                {
                    resultArgs = new ResultArgs();
                    resultArgs = InitializeVoucherSync(VoucherXml);
                    if (resultArgs.Success)
                    {
                        //On 08/02/2024, To update autoincrement from source branch db
                        //for SDBINM alone
                        /*if (VoucherImportType == ImportType.SplitProject && IS_SDB_INM && IsFYSplit)
                        {
                            resultArgs = SetSourceBranchVoucherAutoincrement();
                        }*/

                        if (resultArgs.Success)
                        {
                            dataManager.BeginTransaction();
                            isBeginTransaction = true;
                            resultArgs = ImportTransactionMasters();
                            if (resultArgs.Success)
                            {
                                //Added by Carmel Raj M on September-10-2015
                                //if (VoucherImportType == ImportType.SplitProject)
                                //{
                                //    if (CanOverride) resultArgs = DeleteOpeningBalance();
                                //}

                                if (VoucherImportType == ImportType.SplitProject)
                                {
                                    //Attach op balance for Split FY / general import export with include op bal
                                    resultArgs = GetProjectIds();
                                    if (resultArgs.Success && (resultArgs.ReturnValue != null || VoucherImportType == ImportType.SplitProject))
                                    {
                                        ProjectIds = resultArgs.ReturnValue != null ? resultArgs.ReturnValue.ToString() : "0";
                                    }

                                    if (Include_AL_LedgerOpeningBalance)
                                    {
                                        resultArgs = UpdateLedgeOPBalance();
                                    }
                                }
                                else
                                {
                                    resultArgs = UpdateLedgeOPBalance();
                                }

                                if (resultArgs.Success)
                                {
                                    //Added by Carmel Raj M on September-10-2015
                                    if (VoucherImportType == ImportType.SplitProject)
                                    {
                                        if (CanOverride) resultArgs = DeleteVouchers();
                                    }
                                    else resultArgs = DeleteVouchers();

                                    if (resultArgs.Success)
                                    {
                                        resultArgs = ImportVoucherDetails();
                                        if (resultArgs.Success)
                                        {
                                            if (VoucherImportType == ImportType.SplitProject)
                                            {
                                                //On 03/05/2021, With FD Vouchers for General Project/Import-Export 
                                                if (IsFYSplit || With_FD_Vouchers)
                                                {
                                                    resultArgs = ImportFixedDeposit();
                                                }
                                            }
                                            else
                                            {
                                                resultArgs = ImportFixedDeposit();
                                            }


                                            //if (resultArgs.Success)
                                            //{
                                            //    resultArgs = RefreshTransBalance();
                                            //}
                                            //if (resultArgs.Success && IsFYSplit) //On 10/04.2021

                                            if (resultArgs.Success && VoucherImportType == ImportType.SplitProject) //On 30/04/2021
                                            {
                                                resultArgs = ImportMAPProjectLedgers();

                                                if (resultArgs.Success)
                                                {
                                                    resultArgs = ImportBudgetModule();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    resultArgs.Message = ex.Message;
                }
                finally
                {
                    if (!resultArgs.Success)
                    {
                        dataManager.TransExecutionMode = ExecutionMode.Fail;
                    }

                    if (isBeginTransaction)
                    {
                        dataManager.EndTransaction();
                    }

                    if (VoucherImportType == ImportType.HeadOffice)
                    {
                        if (resultArgs.Success)
                        {
                            AcMEDataSynLog.WriteLog("Data Synchronized Successfully.");
                            UpdateDataSynStatus(DataSyncMailType.Closed, "Data Synchronized Successfully.");
                            SendMail(DataSyncMailType.Closed, "Success");
                        }

                        else
                        {
                            AcMEDataSynLog.WriteLog(resultArgs.Message);
                            UpdateDataSynStatus(DataSyncMailType.Failed, "Problem in Importing Vouchers. " + resultArgs.Message);
                            SendMail(DataSyncMailType.Failed, resultArgs.Message);
                        }
                    }
                    else if (VoucherImportType == ImportType.SplitProject)
                    {
                        //On 10/02/2024, for SDBINM, if target db is new and auto number is updated from source branch xml files, but import is not sucess
                        //roll back previous auto number in target databse
                        /*if (IS_SDB_INM && IsTargetDBNew && IsFYSplit && !VoucherAutoNumberUpdated)
                        {
                            AltertVoucherTableWithNewAutoNumber(1);
                        }*/
                    }
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// this is common method in datasync for branch as well as for portal
        /// This method is used to get budget from branch office or Portal based on branch id
        /// if branchid =0, branch db or portal head office db
        /// if this method is called from branch office, branch_id will be 0, else real branch_id from portal
        /// 
        /// 
        /// </summary>
        /// <param name="branchId"></param>
        /// <param name="dsBudgetDetails"></param>
        /// <returns></returns>
        public ResultArgs UpdateBudgetDetails(Int32 branchId, DataSet dsBudgetDetails)
        {
            bool isBeginTransaction = false;
            using (DataManager dataManager = new DataManager())
            {
                try
                {
                    AcMEDataSynLog.WriteLog("UpdateBudget Started..");
                    resultArgs = new ResultArgs();
                    BranchId = branchId;
                    if (dsBudgetDetails.Tables.Count >= 3 && dsBudgetDetails.Tables.Contains("BudgetMaster") && dsBudgetDetails.Tables.Contains("BudgetProject") && dsBudgetDetails.Tables.Contains("BudgetLedger"))
                    {
                        DataTable dtBudgetMaster = dsBudgetDetails.Tables["BudgetMaster"];
                        DataTable dtBudgetProject = dsBudgetDetails.Tables["BudgetProject"];
                        DataTable dtBudgetLedger = dsBudgetDetails.Tables["BudgetLedger"];
                        DataTable dtBudgetSubLedger = dsBudgetDetails.Tables["BudgetSubLedger"];
                        if (dtBudgetMaster.Rows.Count > 0 && dtBudgetProject.Rows.Count > 0 && dtBudgetLedger.Rows.Count > 0)
                        {
                            //1. Get budget master
                            dataManager.BeginTransaction();
                            isBeginTransaction = true;
                            foreach (DataRow drBudget in dtBudgetMaster.Rows)
                            {
                                int budgetidsource = NumberSet.ToInteger(drBudget[this.AppSchema.Budget.BUDGET_IDColumn.ColumnName].ToString());
                                BudgetName = drBudget[this.AppSchema.Budget.BUDGET_NAMEColumn.ColumnName].ToString();
                                BudgetDateFrom = DateSet.ToDate(drBudget[this.AppSchema.Budget.DATE_FROMColumn.ColumnName].ToString(), false);
                                BudgetDateTo = DateSet.ToDate(drBudget[this.AppSchema.Budget.DATE_TOColumn.ColumnName].ToString(), false);
                                BudgetTypeId = NumberSet.ToInteger(drBudget[this.AppSchema.Budget.BUDGET_TYPE_IDColumn.ColumnName].ToString());

                                //Get projects (from brach db or portal db based on project name)
                                //1. Get projects ids (from brach db or portal db based on project name)
                                dtBudgetProject.DefaultView.RowFilter = this.AppSchema.Budget.BUDGET_IDColumn.ColumnName + "=" + budgetidsource;
                                dtProject = dtBudgetProject.DefaultView.ToTable();
                                dtBudgetProject.DefaultView.RowFilter = string.Empty;
                                resultArgs = GetProjectIds(); //get actual projectid (brach/portal db) by giving project name 
                                if (resultArgs.Success && resultArgs.ReturnValue != null)
                                {
                                    ProjectIds = resultArgs.ReturnValue.ToString();
                                    //using (StreamWriter writer = new StreamWriter(@"D:\testb\t.txt"))
                                    //{
                                    //    writer.WriteLine(ProjectIds);
                                    //}

                                    //2. Insert/Update Budget master in branch or portal db based on update master calling (Branch/Portal)
                                    resultArgs = UpdateBudgetMaster(drBudget, branchId);
                                    if (resultArgs.Success)
                                    {
                                        Int32 newbudgetid = NumberSet.ToInteger(resultArgs.ReturnValue.ToString());
                                        dtBudgetProject.DefaultView.RowFilter = this.AppSchema.Budget.BUDGET_IDColumn.ColumnName + "=" + budgetidsource;
                                        //3. Insert Budget projects in branch or portal db based on update master calling (Branch/Portal)
                                        resultArgs = UpdateBudgetProject(dtBudgetProject.DefaultView.ToTable(), newbudgetid);
                                        dtBudgetProject.DefaultView.RowFilter = string.Empty;
                                        if (resultArgs.Success)
                                        {
                                            //4. Insert Budget Ledgers in branch or portal db based on update master calling (Branch/Portal)
                                            dtBudgetLedger.DefaultView.RowFilter = this.AppSchema.Budget.BUDGET_IDColumn.ColumnName + "=" + budgetidsource;
                                            resultArgs = UpdateBudgetLedger(dtBudgetLedger.DefaultView.ToTable(), newbudgetid);
                                            dtBudgetLedger.DefaultView.RowFilter = string.Empty;
                                            if (resultArgs.Success && dtBudgetSubLedger != null && dtBudgetSubLedger.Rows.Count > 0)
                                            {
                                                //5. Insert Budget sub ledger in branch or portal db based on update master calling (Branch/Portal)
                                                dtBudgetSubLedger.DefaultView.RowFilter = this.AppSchema.Budget.BUDGET_IDColumn.ColumnName + "=" + budgetidsource;
                                                resultArgs = UpdateBudgetSubLedger(dtBudgetSubLedger.DefaultView.ToTable(), newbudgetid);
                                                dtBudgetSubLedger.DefaultView.RowFilter = string.Empty;
                                            }
                                        }
                                    }
                                }
                                else
                                    resultArgs.Message = "Budget Project(s) are not available in current Branch" + "- " + resultArgs.Message;

                                if (!resultArgs.Success)
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            resultArgs.Message = "Budget details are not found";
                        }
                    }
                    else
                    {
                        resultArgs.Message = "Budget details are not found";
                    }

                    if (resultArgs.Success)
                    {
                        AcMEDataSynLog.WriteLog("UpdateBudget Ended..");
                    }
                    else
                    {
                        AcMEDataSynLog.WriteLog("Error in UpdateBudget" + resultArgs.Message);
                    }
                }
                catch (Exception ex)
                {
                    resultArgs.Message = ex.Message;
                }
                finally
                {
                    if (!resultArgs.Success)
                    {
                        dataManager.TransExecutionMode = ExecutionMode.Fail;
                    }

                    if (isBeginTransaction)
                    {
                        dataManager.EndTransaction();
                    }
                }
            }

            return resultArgs;
        }
        #endregion

        #region Import Transaction Masters
        /// <summary>
        /// Importing Raw transactions Masters without Fixed Deposit
        /// </summary>
        /// <returns></returns>
        private ResultArgs ImportTransactionMasters()
        {
            if (SynchronizeHeld != null)
            {
                Caption = "ImportTransaction Masters Progress..";
                SynchronizeHeld(this, new EventArgs());
            }
            AcMEDataSynLog.WriteLog("ImportTransactionMasters Started..");
            try
            {
                resultArgs = ImportProjectCategory();  //Project Category Details
                if (resultArgs.Success)
                {
                    //Added by Carmel Raj M on August-18-2015
                    //Purpose :Add Legal Entity information
                    if (VoucherImportType == ImportType.SplitProject)
                    {
                        resultArgs = ImportLegalEntity();
                    }

                    if (resultArgs.Success)
                    {
                        resultArgs = ImportProject();   //Project Details
                        if (resultArgs.Success)
                        {
                            //if (VoucherImportType == ImportType.SplitProject)
                            //{
                            //    resultArgs = ImportLedgerGroup();
                            //}
                            //else
                            //{
                            resultArgs = ImportMasterLedgerGroup();
                            //}

                            if (resultArgs.Success)
                            {
                                resultArgs = ImportGSTClass();
                                if (resultArgs.Success)
                                {
                                    resultArgs = ImportLedger();  //Head Office Ledger Details
                                    if (resultArgs.Success)
                                    {
                                        resultArgs = ImportPurpose();  //Purpose Details    
                                        if (resultArgs.Success)
                                        {
                                            resultArgs = ImportCountry();  //Country Details
                                            if (resultArgs.Success)
                                            {
                                                resultArgs = ImportState();  //State Details
                                                if (resultArgs.Success)
                                                {
                                                    resultArgs = ImportDonorInfo(); //Donor Details
                                                    if (resultArgs.Success)
                                                    {
                                                        resultArgs = ImportCostCentre();  //Cost Centre Details (import from master or import from voucher cc)
                                                        if (resultArgs.Success)
                                                        {
                                                            resultArgs = ImportBank();   //Bank Details
                                                            if (resultArgs.Success)
                                                            {
                                                                resultArgs = ImportLedgerBankAccount();  //Bank Account Details In Ledger
                                                                if (resultArgs.Success)
                                                                {
                                                                    resultArgs = ImportLedgerProfile();
                                                                    if (resultArgs.Success)
                                                                    {
                                                                        resultArgs = ImportAssetStockVendor();
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in ImportTransactionMasters: " + ex.ToString();
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ImportTransactionMasters Ended.");
            }
            return resultArgs;
        }

        /// <summary>
        /// Checks whether Branch Project Category is available in Head Office
        /// If Project Category not available in Head Offcie, The details will be added in Head Office.
        /// </summary>
        /// <returns>ResultArgs with its properties</returns>
        private ResultArgs ImportProjectCategory()
        {
            AcMEDataSynLog.WriteLog("ImportProjectCategory Started..");
            try
            {
                bool isProjectCategoryExists = true;
                using (DataManager dataManager = new DataManager())
                {
                    if (dtProject != null && dtProject.Rows.Count > 0)
                    {
                        foreach (DataRow drProject in dtProject.Rows)
                        {
                            ProjectCategoryName = drProject[this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn.ColumnName].ToString();

                            if (!string.IsNullOrEmpty(ProjectCategoryName))
                            {
                                resultArgs = IsExists(SQLCommand.ImportVoucher.IsProjectCategoryExists);
                                if (resultArgs.Success)
                                {
                                    isProjectCategoryExists = resultArgs.DataSource.Sclar.ToInteger > 0;
                                    if (!isProjectCategoryExists)
                                    {
                                        resultArgs = SaveProjectCatogoryDetails();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("Import Project Category Ended.");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Problem in ImportProjectCategory : " + resultArgs.Message);
            }

            //On 22/02/2018, attach name of the project and voucher detailis in the error log
            if (!resultArgs.Success && string.IsNullOrEmpty(ProblemDataDetails))
            {
                ProblemDataDetails = "Project Category : " + ProjectCategoryName + "<br>" +
                                      "Problem in updating Master (Project Category)";
            }
            return resultArgs;
        }

        /// <summary>
        /// Checks whether Branch project is available in Head Office
        /// If there is any conflict of Project Name between the Branch and Head Office, The Synchronization process will be stopped.
        /// </summary>
        /// <returns>ResultArgs with its properties</returns>
        private ResultArgs ImportProject()
        {
            AcMEDataSynLog.WriteLog("ImportProject Started..");
            try
            {
                DataTable dtTempProject = dtProject.Copy();
                bool isProjectExists = true;
                if (MergerProjectId > 0)
                {
                    //Fetch only the Merge project
                    resultArgs = FetchMergeProject();
                    if (resultArgs != null && resultArgs.Success)
                        dtProject = resultArgs.DataSource.Table;
                }

                using (DataManager dataManager = new DataManager())
                {
                    if (dtProject != null && dtProject.Rows.Count > 0)
                    {
                        foreach (DataRow drProject in dtProject.Rows)
                        {
                            ProjectName = drProject[this.AppSchema.Project.PROJECTColumn.ColumnName].ToString();

                            ProjectClosedBy = 0;
                            if (!string.IsNullOrEmpty(drProject[this.AppSchema.Project.DATE_CLOSEDColumn.ColumnName].ToString()) &&
                                 dtProject.Columns.Contains(this.AppSchema.Project.CLOSED_BYColumn.ColumnName))
                            {
                                ProjectClosedBy = this.NumberSet.ToInteger(drProject[this.AppSchema.Project.CLOSED_BYColumn.ColumnName].ToString()); ;
                            }

                            if (!string.IsNullOrEmpty(ProjectName))
                            {
                                resultArgs = IsExists(SQLCommand.ImportVoucher.IsProjectExists);
                                if (resultArgs.Success)
                                {
                                    isProjectExists = resultArgs.DataSource.Sclar.ToInteger > 0;
                                    if (isProjectExists)
                                    {
                                        if (VoucherImportType == ImportType.HeadOffice || VoucherImportType == ImportType.SubBranch)
                                        {
                                            ProjectId = MergerProjectId > 0 ? MergerProjectId : GetId(Id.Project);//Modified by Carmel Raj M on July-08-2015
                                            if (ProjectId > 0)
                                            {
                                                if (!VoucherImportType.Equals(ImportType.SplitProject))
                                                {
                                                    resultArgs = IsProjectMappedWithBranch();
                                                    if (resultArgs.Success && resultArgs.DataSource.Sclar.ToInteger == 0)
                                                    {
                                                        resultArgs.Message = "Project used in Branch Office is not mapped to this Branch Office in Portal. " +
                                                                             " The Project Might have been unmapped in the Head Office. Map Project(s) to the Branch Office and Try again. " +
                                                                             "<br/> The Required Project(s) in Portal are :<br/><b> " + ProjectName + "</b>";
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // resultArgs.Message = "Project used in Branch Office is not available in Head office. " +
                                        //                      " The Project Name might have been changed or Removed by the Branch. " +
                                        //                      " <br/>The Required Project(s) in Portal are :<br/><b> " + ProjectName + "</b>";
                                        // break;

                                        ProjectCode = drProject[this.AppSchema.Project.PROJECT_CODEColumn.ColumnName].ToString();
                                        ProjectName = drProject[this.AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                                        AccountDate = this.DateSet.ToDate(drProject[this.AppSchema.Project.ACCOUNT_DATEColumn.ColumnName].ToString(), false);
                                        Description = drProject[this.AppSchema.Project.DESCRIPTIONColumn.ColumnName].ToString();
                                        StartedOn = this.DateSet.ToDate(drProject[this.AppSchema.Project.DATE_STARTEDColumn.ColumnName].ToString(), false);

                                        //Modified by Carmel Raj on October-07-2015
                                        //Modified field : Closed_On
                                        //Purpose : To hold null valu when project closing date is empty
                                        Closed_On = string.IsNullOrEmpty(drProject[this.AppSchema.Project.DATE_CLOSEDColumn.ColumnName].ToString()) ? null : (DateTime?)this.DateSet.ToDate(drProject[this.AppSchema.Project.DATE_CLOSEDColumn.ColumnName].ToString(), false);
                                        DivisionId = this.NumberSet.ToInteger(drProject[this.AppSchema.Project.DIVISION_IDColumn.ColumnName].ToString());
                                        Notes = drProject[this.AppSchema.Project.NOTESColumn.ColumnName].ToString();
                                        ProjectCategoryName = drProject[this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn.ColumnName].ToString();
                                        ProjectCategroyId = GetId(Id.ProjectCategory);
                                        SocietyName = drProject[this.AppSchema.LicenseDataTable.SocietyNameColumn.ColumnName].ToString();
                                        LegalEntityId = GetId(Id.LegalEntity);

                                        if (string.IsNullOrEmpty(ProjectCategoryName))
                                        {
                                            resultArgs.Message = "Project Category is empty for '" + ProjectName + "'";
                                        }
                                        else
                                        {
                                            resultArgs = SaveProjectDetails();
                                            if (VoucherImportType.Equals(ImportType.SplitProject))
                                            {
                                                ProjectId = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                                if (resultArgs.Success)
                                                    resultArgs = MapDefaultVoucher();
                                            }
                                        }
                                    }
                                }
                            }

                            if (!resultArgs.Success)
                            {
                                break;
                            }
                        }
                    }
                }
                //if (MergerProjectId > 0)
                //{
                //    //Replacing the dtProject DataTable with original Data
                //    dtProject = dtTempProject;
                //}
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ImportProject Ended.");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Problem in ImportProject :" + resultArgs.Message);
            }

            //On 22/02/2018, attach name of the project and voucher detailis in the error log
            if (!resultArgs.Success && string.IsNullOrEmpty(ProblemDataDetails))
            {
                ProblemDataDetails = "Project : " + ProjectName + "<br>" +
                                      "Problem in updating Master (Project)";
            }
            return resultArgs;
        }

        private ResultArgs FetchMergeProject()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.FetchMergeProject, DataBaseTypeName, SQLAdapterType.HOSQL))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, MergerProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// Checks whether the Purpose at Branch XML is available in Head Office.
        /// If the Pupose mismatches in Head Office, The Synchronization process will be stopped.
        /// </summary>
        /// <returns></returns>
        private ResultArgs ImportPurpose()
        {
            AcMEDataSynLog.WriteLog("ImportPurpose Started..");
            try
            {
                bool isPurposeExists = true;
                using (DataManager dataManager = new DataManager())
                {
                    if (dtVoucherMaster != null && dtVoucherMaster.Rows.Count > 0)
                    {
                        foreach (DataRow drProject in dtVoucherMaster.Rows)
                        {
                            PurposeName = drProject[this.AppSchema.Purposes.FC_PURPOSEColumn.ColumnName].ToString();

                            if (!string.IsNullOrEmpty(PurposeName))
                            {
                                resultArgs = IsExists(SQLCommand.ImportVoucher.IsPurposeExists);
                                if (resultArgs.Success)
                                {
                                    isPurposeExists = resultArgs.DataSource.Sclar.ToInteger > 0;
                                    if (!isPurposeExists)
                                    {
                                        //Added by Carmel Raj on 14-May-2015
                                        if (VoucherImportType == ImportType.SplitProject)
                                        {
                                            PurposeCode = "";
                                            ImportNewPurpose();
                                        }
                                        else
                                        {
                                            resultArgs.Message = "Purpose is not available in Head Office : " + PurposeName;
                                            break;
                                        }
                                    }
                                }

                                if (!resultArgs.Success)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally
            {
            }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ImportPurpose Ended.");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in Importing Purpose: " + resultArgs.Message);
            }

            //On 22/02/2018, attach name of the project and voucher detailis in the error log
            if (!resultArgs.Success && string.IsNullOrEmpty(ProblemDataDetails))
            {
                ProblemDataDetails = "Purpose :" + PurposeName + "<br>" +
                                      "Problem in updating Master (Purpose)";
            }
            return resultArgs;
        }

        private void ImportNewPurpose()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.MigrateFCPurpose))
            {
                dataManager.Parameters.Add(AppSchema.Purposes.CODEColumn, PurposeCode, true);
                dataManager.Parameters.Add(AppSchema.Purposes.FC_PURPOSEColumn, PurposeName);
                resultArgs = dataManager.UpdateData();
            }
        }

        /// <summary>
        /// Insert Master Ledger Group Details in the Branch Office 
        /// </summary>
        /// <param name="dataGroupmanager"></param>
        /// <param name="dtGroup"></param>
        /// <returns></returns>
        private ResultArgs ImportMasterLedgerGroup()
        {
            try
            {
                AcMELog.WriteLog("InsertMasterLedgerGroup Started");
                if (dtLedgerGroup != null && dtLedgerGroup.Rows.Count > 0)
                {
                    foreach (DataRow drRowLedgerGroup in dtLedgerGroup.Rows)
                    {
                        LedgerGroup = drRowLedgerGroup[this.AppSchema.LedgerGroup.LEDGER_GROUPColumn.ColumnName].ToString();
                        if (!string.IsNullOrEmpty(LedgerGroup))
                        {
                            //If the mapping is mandatory, Ledger group is not inserted. Portal ledgers should be available
                            if (VoucherImportType == ImportType.HeadOffice)
                            {
                                if (IsMappingMandatory)
                                {
                                    break;
                                }
                            }

                            Abbrevation = drRowLedgerGroup[this.AppSchema.LedgerGroup.GROUP_CODEColumn.ColumnName].ToString();
                            ParentGroup = drRowLedgerGroup[this.AppSchema.LedgerGroup.ParentGroupColumn.ColumnName].ToString();
                            ParentGroupId = GetId(Id.ParentGroup);
                            Nature = drRowLedgerGroup[this.AppSchema.LedgerGroup.NATUREColumn.ColumnName].ToString();
                            NatureId = GetId(Id.Nature);
                            MainGroup = drRowLedgerGroup[this.AppSchema.LedgerGroup.MainGroupColumn.ColumnName].ToString();
                            MainGroupId = GetId(Id.MainGroup);
                            SortOrder = this.NumberSet.ToInteger(drRowLedgerGroup[this.AppSchema.LedgerGroup.SORT_ORDERColumn.ColumnName].ToString());

                            //Ledger Group Validations
                            if (ParentGroupId == 0)
                            {
                                ParentGroupId = NatureId;
                            }

                            if (MainGroupId == 0)
                            {
                                MainGroupId = NatureId;
                            }

                            if (NatureId == 0)
                            {
                                resultArgs.Message = "Nature '" + Nature + "' does not exists in Portal.";
                            }
                            else
                            {
                                resultArgs = IsExists(SQLCommand.ImportVoucher.IsLedgerGroupExists);
                                if (resultArgs.Success)
                                {
                                    GroupId = 0;
                                    if (resultArgs.DataSource.Sclar.ToInteger > 0)
                                    {
                                        GroupId = GetId(Id.LedgerGroup);
                                    }
                                    resultArgs = SaveLedgerGroupDetails();
                                }
                            }
                        }

                        if (!resultArgs.Success)
                        {
                            resultArgs.Message = "Problem in InsertMasterLedgerGroup: " + resultArgs.Message;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Exception in importing Master Ledger Group. " + ex.ToString();
            }
            finally { }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("InsertMasterLedgerGroup Ended");
            }

            //On 22/02/2018, attach name of the project and voucher detailis in the error log
            if (!resultArgs.Success && string.IsNullOrEmpty(ProblemDataDetails))
            {
                ProblemDataDetails = "Ledger Group :" + LedgerGroup + "<br>" +
                                      "Problem in updating Master (Ledger Group)";
            }
            return resultArgs;
        }

        /// <summary>
        /// Insert Ledger Group if the Ledger group is not available in the database
        /// </summary>
        /// <returns></returns>
        private ResultArgs ImportLedgerGroup()
        {
            AcMEDataSynLog.WriteLog("ImportLedgerGroup Started..");
            try
            {
                if (dtLedgerGroup != null)
                {
                    int MainGroupId = 0;
                    int AccessFlag = 0;
                    foreach (DataRow drItem in dtLedgerGroup.Rows)
                    {
                        GroupName = drItem[AppSchema.LedgerGroup.LEDGER_GROUPColumn.ColumnName].ToString();
                        if (IsLedgerGroupNotExists())
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateLedgerGroup))
                            {
                                dataManager.Parameters.Add(AppSchema.LedgerGroup.GROUP_CODEColumn, drItem[AppSchema.LedgerGroup.GROUP_CODEColumn.ColumnName].ToString());
                                dataManager.Parameters.Add(AppSchema.LedgerGroup.LEDGER_GROUPColumn, GroupName);
                                dataManager.Parameters.Add(AppSchema.LedgerGroup.PARENT_GROUP_IDColumn, NumberSet.ToInteger(drItem[AppSchema.LedgerGroup.PARENT_GROUP_IDColumn.ColumnName].ToString()));
                                dataManager.Parameters.Add(AppSchema.LedgerGroup.NATURE_IDColumn, NumberSet.ToInteger(drItem[AppSchema.LedgerGroup.NATURE_IDColumn.ColumnName].ToString()));
                                dataManager.Parameters.Add(AppSchema.LedgerGroup.MAIN_GROUP_IDColumn, MainGroupId);
                                dataManager.Parameters.Add(AppSchema.LedgerGroup.ACCESS_FLAGColumn, AccessFlag);

                                resultArgs = dataManager.UpdateData();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Importing Ledger Group: " + ex.ToString();
            }
            finally { }
            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ImportLedgerGroup Ended.");
            }
            return resultArgs;
        }

        private ResultArgs ImportLegalEntity()
        {
            AcMEDataSynLog.WriteLog("Import Legal entity Started..");
            bool isLegalEnityExists = true;

            try
            {
                if (dtLegalEntity != null)
                {
                    foreach (DataRow drItem in dtLegalEntity.Rows)
                    {
                        //12/04/2017, to check legal entity exists in db..for project import and export
                        SocietyName = drItem[AppSchema.LegalEntity.SOCIETYNAMEColumn.ColumnName].ToString().Trim();
                        resultArgs = IsExists(SQLCommand.ImportVoucher.IsLegalEntityExists);
                        if (resultArgs.Success)
                        {
                            isLegalEnityExists = resultArgs.DataSource.Sclar.ToInteger > 0;
                            if (!isLegalEnityExists)
                            {
                                using (DataManager dataManager = new DataManager(SQLCommand.LegalEntity.Add))
                                {
                                    dataManager.Parameters.Add(AppSchema.LegalEntity.SOCIETYNAMEColumn, drItem[AppSchema.LegalEntity.SOCIETYNAMEColumn.ColumnName]);
                                    dataManager.Parameters.Add(AppSchema.LegalEntity.CONTACTPERSONColumn, drItem[AppSchema.LegalEntity.CONTACTPERSONColumn.ColumnName]);
                                    dataManager.Parameters.Add(AppSchema.LegalEntity.ADDRESSColumn, drItem[AppSchema.LegalEntity.ADDRESSColumn.ColumnName]);
                                    dataManager.Parameters.Add(AppSchema.LegalEntity.PLACEColumn, drItem[AppSchema.LegalEntity.PLACEColumn.ColumnName]);
                                    dataManager.Parameters.Add(AppSchema.LegalEntity.PHONEColumn, drItem[AppSchema.LegalEntity.PHONEColumn.ColumnName]);
                                    dataManager.Parameters.Add(AppSchema.LegalEntity.FAXColumn, drItem[AppSchema.LegalEntity.FAXColumn.ColumnName]);
                                    CountryName = drItem[AppSchema.LegalEntity.COUNTRYColumn.ColumnName].ToString();
                                    dataManager.Parameters.Add(AppSchema.LegalEntity.COUNTRY_IDColumn, GetId(Id.Country));
                                    State = drItem[AppSchema.LegalEntity.STATEColumn.ColumnName].ToString();
                                    dataManager.Parameters.Add(AppSchema.LegalEntity.STATE_IDColumn, GetId(Id.State));
                                    dataManager.Parameters.Add(AppSchema.LegalEntity.A12NOColumn, drItem[AppSchema.LegalEntity.A12NOColumn.ColumnName]);
                                    dataManager.Parameters.Add(AppSchema.LegalEntity.GIRNOColumn, drItem[AppSchema.LegalEntity.GIRNOColumn.ColumnName]);
                                    dataManager.Parameters.Add(AppSchema.LegalEntity.TANNOColumn, drItem[AppSchema.LegalEntity.TANNOColumn.ColumnName]);
                                    dataManager.Parameters.Add(AppSchema.LegalEntity.PANNOColumn, drItem[AppSchema.LegalEntity.PANNOColumn.ColumnName]);
                                    dataManager.Parameters.Add(AppSchema.LegalEntity.EMAILColumn, drItem[AppSchema.LegalEntity.EMAILColumn.ColumnName]);
                                    dataManager.Parameters.Add(AppSchema.LegalEntity.PINCODEColumn, drItem[AppSchema.LegalEntity.PINCODEColumn.ColumnName]);
                                    dataManager.Parameters.Add(AppSchema.LegalEntity.URLColumn, drItem[AppSchema.LegalEntity.URLColumn.ColumnName]);
                                    dataManager.Parameters.Add(AppSchema.LegalEntity.REGNOColumn, drItem[AppSchema.LegalEntity.REGNOColumn.ColumnName]);
                                    dataManager.Parameters.Add(AppSchema.LegalEntity.PERMISSIONNOColumn, drItem[AppSchema.LegalEntity.PERMISSIONNOColumn.ColumnName]);

                                    if (!string.IsNullOrEmpty(drItem[AppSchema.LegalEntity.REGDATEColumn.ColumnName].ToString()))
                                    {
                                        dataManager.Parameters.Add(AppSchema.LegalEntity.REGDATEColumn, this.DateSet.ToDate(drItem[AppSchema.LegalEntity.REGDATEColumn.ColumnName].ToString(), DateFormatInfo.MySQLFormat.DateAdd.ToString()));
                                    }
                                    else
                                    {
                                        dataManager.Parameters.Add(AppSchema.LegalEntity.REGDATEColumn, null);
                                    }

                                    if (!string.IsNullOrEmpty(drItem[AppSchema.LegalEntity.PERMISSIONDATEColumn.ColumnName].ToString()))
                                    {
                                        dataManager.Parameters.Add(AppSchema.LegalEntity.PERMISSIONDATEColumn, this.DateSet.ToDate(drItem[AppSchema.LegalEntity.PERMISSIONDATEColumn.ColumnName].ToString(), DateFormatInfo.MySQLFormat.DateAdd.ToString()));
                                    }
                                    else
                                    {
                                        dataManager.Parameters.Add(AppSchema.LegalEntity.PERMISSIONDATEColumn, null);
                                    }

                                    dataManager.Parameters.Add(AppSchema.LegalEntity.ASSOCIATIONNATUREColumn, drItem[AppSchema.LegalEntity.OTHER_ASSOCIATION_NATUREColumn.ColumnName]);
                                    dataManager.Parameters.Add(AppSchema.LegalEntity.DENOMINATIONColumn, drItem[AppSchema.LegalEntity.DENOMINATIONColumn.ColumnName]);
                                    dataManager.Parameters.Add(AppSchema.LegalEntity.OTHER_ASSOCIATION_NATUREColumn, drItem[AppSchema.LegalEntity.OTHER_ASSOCIATION_NATUREColumn.ColumnName]);
                                    dataManager.Parameters.Add(AppSchema.LegalEntity.OTHER_DENOMINATIONColumn, drItem[AppSchema.LegalEntity.OTHER_ASSOCIATION_NATUREColumn.ColumnName]);
                                    dataManager.Parameters.Add(AppSchema.LegalEntity.FCRINOColumn, drItem[AppSchema.LegalEntity.FCRINOColumn.ColumnName]);

                                    if (!string.IsNullOrEmpty(drItem[AppSchema.LegalEntity.FCRIREGDATEColumn.ColumnName].ToString()))
                                    {
                                        dataManager.Parameters.Add(AppSchema.LegalEntity.FCRIREGDATEColumn, this.DateSet.ToDate(drItem[AppSchema.LegalEntity.FCRIREGDATEColumn.ColumnName].ToString(), DateFormatInfo.MySQLFormat.DateAdd.ToString()));
                                    }
                                    else
                                    {
                                        dataManager.Parameters.Add(AppSchema.LegalEntity.FCRIREGDATEColumn, null);
                                    }

                                    dataManager.Parameters.Add(AppSchema.LegalEntity.EIGHTYGNOColumn, drItem[AppSchema.LegalEntity.EIGHTYGNOColumn.ColumnName]);

                                    //On 09/09/2022 80G registration Date
                                    string G80Regdate = string.Empty;
                                    if (dtLegalEntity.Columns.Contains(AppSchema.LegalEntity.EIGHTY_GNO_REG_DATEColumn.ColumnName))
                                    {
                                        G80Regdate = drItem[AppSchema.LegalEntity.EIGHTY_GNO_REG_DATEColumn.ColumnName].ToString();
                                    }

                                    if (!string.IsNullOrEmpty(G80Regdate))
                                    {
                                        dataManager.Parameters.Add(AppSchema.LegalEntity.EIGHTY_GNO_REG_DATEColumn, this.DateSet.ToDate(G80Regdate, DateFormatInfo.MySQLFormat.DateAdd.ToString()));
                                    }
                                    else
                                    {
                                        dataManager.Parameters.Add(AppSchema.LegalEntity.EIGHTY_GNO_REG_DATEColumn, null);
                                    }

                                    dataManager.Parameters.Add(AppSchema.LegalEntity.GST_NOColumn, drItem[AppSchema.LegalEntity.GST_NOColumn.ColumnName]);
                                    dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_IDColumn, drItem[AppSchema.Ledger.LEDGER_IDColumn.ColumnName]);
                                    resultArgs = dataManager.UpdateData();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Importing Legal Entity: " + ex.ToString();
            }
            finally { }
            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("Importing Legal Entity Ended.");
            }

            //On 22/02/2018, attach name of the project and voucher detailis in the error log
            if (!resultArgs.Success && string.IsNullOrEmpty(ProblemDataDetails))
            {
                ProblemDataDetails = "Legal Entity : " + SocietyName + "<br>" +
                                      "Problem in updating Master (Legal Entity)";
            }
            return resultArgs;
        }


        private bool IsLedgerGroupNotExists()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GetLedgerGroupId))
            {
                dataManager.Parameters.Add(AppSchema.LedgerGroup.LEDGER_GROUPColumn, GroupName);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger == 0;
        }



        /// <summary>
        /// Check whether the Ledger is available in Head Office, If not Particular Ledger is FD or Cash will be added in Head Office.
        /// If the General ledgers are mismatching between the Branch and Head Office, The Data Synchronization process will be stopeed.
        /// Every ledger from the Branch should be mapped with any one of the Head Office Ledger. 
        /// If ledger is not mapped with Head Office Ledger, The Data Sync Process will be stopped.
        /// </summary>
        /// <returns></returns>
        private ResultArgs ImportLedger()
        {
            AcMEDataSynLog.WriteLog("ImportLedger Started..");
            try
            {
                bool isLedgerExists = true;
                string MismatchedLedgers = string.Empty;
                // bool isLedgersMatched = true;

                using (DataManager dataManager = new DataManager())
                {
                    if (dtLedger != null && dtLedger.Rows.Count > 0)
                    {
                        foreach (DataRow drLedger in dtLedger.Rows)
                        {
                            LedgerName = drLedger[this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                            //If and if only sample ledgers (show mapping enabled)
                            string actualledgername = drLedger[this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();

                            if (!string.IsNullOrEmpty(LedgerName))
                            {
                                resultArgs = IsExists(SQLCommand.ImportVoucher.IsLedgerExists);
                                if (resultArgs.Success)
                                {
                                    isLedgerExists = resultArgs.DataSource.Sclar.ToInteger > 0;
                                    if (!isLedgerExists)
                                    {
                                        GroupId = this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.GROUP_IDColumn.ColumnName].ToString());
                                        AcMEDataSynLog.WriteLog("Ledger Name: " + LedgerName + " Group Id: " + GroupId + "Mapping Mandatory: " + IsMappingMandatory);

                                        if (VoucherImportType == ImportType.HeadOffice &&
                                                IsMappingMandatory && GroupId != (int)FixedLedgerGroup.Cash && GroupId != (int)FixedLedgerGroup.BankAccounts)
                                        {
                                            resultArgs.Message = "Branch Ledger (" + LedgerName + ")  is not available in Head Office." + Environment.NewLine
                                                + "Mapping is made mandatory that all the branch ledgers should be mapped with Head office ledgers." + Environment.NewLine
                                                + "Ledgers would have been rearranged in Head office. Import masters and try again.";
                                            break;
                                        }

                                        if (GroupId != (int)FixedLedgerGroup.BankAccounts)
                                        {
                                            LedgerGroup = drLedger[this.AppSchema.LedgerGroup.LEDGER_GROUPColumn.ColumnName].ToString();
                                            GroupId = GetId(Id.LedgerGroup);
                                            LedgerCode = drLedger[this.AppSchema.Ledger.LEDGER_CODEColumn.ColumnName].ToString();
                                            LedgerName = drLedger[this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                                            LedType = drLedger[this.AppSchema.Ledger.LEDGER_TYPEColumn.ColumnName].ToString();
                                            LedSubType = drLedger[this.AppSchema.Ledger.LEDGER_SUB_TYPEColumn.ColumnName].ToString();
                                            SortId = this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.SORT_IDColumn.ColumnName].ToString());
                                            IsBranchLedger = this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_BRANCH_LEDGERColumn.ColumnName].ToString());
                                            //Added by Carmel Raj on August-18-2015
                                            IsCostCentre = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_COST_CENTERColumn.ColumnName].ToString());
                                            IsBankInterestLedger = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_BANK_INTEREST_LEDGERColumn.ColumnName].ToString());
                                            IsTDSLedger = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_TDS_LEDGERColumn.ColumnName].ToString());
                                            IsInKindLedger = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_INKIND_LEDGERColumn.ColumnName].ToString());
                                            IsDepreciationLedger = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_DEPRECIATION_LEDGERColumn.ColumnName].ToString());
                                            IsAssetGainLedger = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_ASSET_GAIN_LEDGERColumn.ColumnName].ToString());
                                            IsAssetLossLedger = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_ASSET_LOSS_LEDGERColumn.ColumnName].ToString());
                                            IsDisposalLedger = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_DISPOSAL_LEDGERColumn.ColumnName].ToString());
                                            IsSubSidyLedger = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_SUBSIDY_LEDGERColumn.ColumnName].ToString());
                                            IsGSTLedger = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_GST_LEDGERSColumn.ColumnName].ToString());
                                            GSTServiceTyper = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.GST_SERVICE_TYPEColumn.ColumnName].ToString());
                                            BudgetGroupId = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.BUDGET_GROUP_IDColumn.ColumnName].ToString());
                                            BudgetSubGroupId = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.BUDGET_SUB_GROUP_IDColumn.ColumnName].ToString());
                                            //On 09/09/2022 ----------------------------------------------------------------------------------------
                                            IsBankCommissionInterestLedger = IsBankSBInterestLedger = IsBankPenaltyInterestLedger = 0;
                                            if (dtLedger.Columns.Contains(this.AppSchema.Ledger.IS_BANK_COMMISSION_LEDGERColumn.ColumnName))
                                            {
                                                IsBankCommissionInterestLedger = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_BANK_COMMISSION_LEDGERColumn.ColumnName].ToString());
                                            }

                                            if (dtLedger.Columns.Contains(this.AppSchema.Ledger.IS_BANK_SB_INTEREST_LEDGERColumn.ColumnName))
                                            {
                                                IsBankSBInterestLedger = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_BANK_SB_INTEREST_LEDGERColumn.ColumnName].ToString());
                                            }

                                            if (dtLedger.Columns.Contains(this.AppSchema.Ledger.IS_BANK_FD_PENALTY_LEDGERColumn.ColumnName))
                                            {
                                                IsBankPenaltyInterestLedger = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_BANK_FD_PENALTY_LEDGERColumn.ColumnName].ToString());
                                            }
                                            //-----------------------------------------------------------------------------------------------------

                                            //On 09/11/2022, GST HSN, GST SAC Code -----------------------------------------------------------------
                                            GST_HSN_SAC_CODE = string.Empty;
                                            if (IsGSTLedger == 1 && dtLedger.Columns.Contains(this.AppSchema.Ledger.GST_HSN_SAC_CODEColumn.ColumnName))
                                            {
                                                GST_HSN_SAC_CODE = drLedger[this.AppSchema.Ledger.GST_HSN_SAC_CODEColumn.ColumnName].ToString().Trim();
                                            }

                                            //------------------------------------------------------------------------------------------------------

                                            //IsBankInterestLedger
                                            //---------------------------------------------------------------
                                            //On 06/01/2022, For Ledger Closed Date -------------------------
                                            LedgerDateClosed = DateTime.MinValue;
                                            if (dtLedger.Columns.Contains(this.AppSchema.Ledger.DATE_CLOSEDColumn.ColumnName))
                                            {
                                                if (drLedger[this.AppSchema.Ledger.DATE_CLOSEDColumn.ColumnName] != null)
                                                {
                                                    if (actualledgername == LedgerName) //If and if only sample ledgers (show mapping enabled)
                                                    {
                                                        string ledgerclosedate = drLedger[this.AppSchema.Ledger.DATE_CLOSEDColumn.ColumnName].ToString();
                                                        if (!string.IsNullOrEmpty(ledgerclosedate) && DateSet.ToDate(ledgerclosedate, false) != DateTime.MinValue)
                                                        {
                                                            LedgerDateClosed = DateSet.ToDate(drLedger[this.AppSchema.Ledger.DATE_CLOSEDColumn.ColumnName].ToString(), false);
                                                        }
                                                    }
                                                }
                                            }

                                            LedgerClosedBy = 0;
                                            if (LedgerDateClosed != DateTime.MinValue && dtLedger.Columns.Contains(this.AppSchema.Ledger.CLOSED_BYColumn.ColumnName))
                                            {
                                                LedgerClosedBy = this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.CLOSED_BYColumn.ColumnName].ToString()); ;
                                            }

                                            //--------------------------------------------------------------------

                                            //On 15/05/2024, To Update FD Ledger Investment Type -----------------
                                            FDInvestmentTypeId = 0; //For all Ledgers
                                            if (LedSubType.ToUpper() == ledgerSubType.FD.ToString().ToUpper())
                                            {
                                                FDInvestmentTypeId = (Int32)FDInvestmentType.FD;
                                                if (drLedger.Table.Columns.Contains(this.AppSchema.Ledger.FD_INVESTMENT_TYPE_IDColumn.ColumnName))
                                                {
                                                    //Assign portal branch investment type id
                                                    FDInvestmentTypeId = this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.FD_INVESTMENT_TYPE_IDColumn.ColumnName].ToString());
                                                }
                                            }
                                            //--------------------------------------------------------------------

                                            //Ledger Details Validation.
                                            if (string.IsNullOrEmpty(LedgerName))
                                            {
                                                resultArgs.Message = "Ledger Name is Empty in Branch Voucher file while updating Ledger Details..";
                                            }

                                            else if (VoucherImportType == ImportType.HeadOffice &&
                                            IsMappingMandatory && GroupId == 0)
                                            {
                                                resultArgs.Message = "Ledger Group is not available in Head Office for the ' " + LedgerName + "'. The required Ledger Group in Portal is '" + LedgerGroup + "'";
                                            }
                                            else if (GroupId == 0)
                                            {
                                                //Modifed by Carmel Raj On July-12-2015
                                                //Purpose : Changes the Message when Import type is SplitProject
                                                resultArgs.Message = VoucherImportType.Equals(ImportType.SplitProject) ? String.Format("Ledger Group is not available {0}", LedgerGroup) :
                                                    "Ledger Group is not available in Head Office for the ' " + LedgerName + "'. The required Ledger Group in Portal is '" + LedgerGroup + "'";
                                            }
                                            else
                                            {
                                                resultArgs = SaveLedgerDetails();
                                            }
                                        }
                                        //else
                                        //{
                                        //    MismatchedLedgers += LedgerName;
                                        //    MismatchedLedgers += ",<br/>";
                                        //    isLedgersMatched = false;
                                        //}
                                    }
                                    else if (isLedgerExists && VoucherImportType == ImportType.SplitProject)
                                    {
                                        //On 24/06/2021, to update ledger options
                                        LedSubType = drLedger[this.AppSchema.Ledger.LEDGER_SUB_TYPEColumn.ColumnName].ToString();
                                        IsCostCentre = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_COST_CENTERColumn.ColumnName].ToString());
                                        IsBankInterestLedger = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_BANK_INTEREST_LEDGERColumn.ColumnName].ToString());
                                        IsTDSLedger = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_TDS_LEDGERColumn.ColumnName].ToString());
                                        IsInKindLedger = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_INKIND_LEDGERColumn.ColumnName].ToString());
                                        IsDepreciationLedger = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_DEPRECIATION_LEDGERColumn.ColumnName].ToString());
                                        IsAssetGainLedger = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_ASSET_GAIN_LEDGERColumn.ColumnName].ToString());
                                        IsAssetLossLedger = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_ASSET_LOSS_LEDGERColumn.ColumnName].ToString());
                                        IsDisposalLedger = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_DISPOSAL_LEDGERColumn.ColumnName].ToString());
                                        IsSubSidyLedger = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_SUBSIDY_LEDGERColumn.ColumnName].ToString());
                                        IsGSTLedger = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_GST_LEDGERSColumn.ColumnName].ToString());
                                        GSTServiceTyper = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.GST_SERVICE_TYPEColumn.ColumnName].ToString());
                                        BudgetGroupId = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.BUDGET_GROUP_IDColumn.ColumnName].ToString());
                                        BudgetSubGroupId = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.BUDGET_SUB_GROUP_IDColumn.ColumnName].ToString());

                                        //On 09/09/2022 ----------------------------------------------------------------------------------------
                                        IsBankCommissionInterestLedger = IsBankSBInterestLedger = IsBankPenaltyInterestLedger = 0;
                                        if (dtLedger.Columns.Contains(this.AppSchema.Ledger.IS_BANK_COMMISSION_LEDGERColumn.ColumnName))
                                        {
                                            IsBankCommissionInterestLedger = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_BANK_COMMISSION_LEDGERColumn.ColumnName].ToString());
                                        }

                                        if (dtLedger.Columns.Contains(this.AppSchema.Ledger.IS_BANK_SB_INTEREST_LEDGERColumn.ColumnName))
                                        {
                                            IsBankSBInterestLedger = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_BANK_SB_INTEREST_LEDGERColumn.ColumnName].ToString());
                                        }

                                        if (dtLedger.Columns.Contains(this.AppSchema.Ledger.IS_BANK_FD_PENALTY_LEDGERColumn.ColumnName))
                                        {
                                            IsBankPenaltyInterestLedger = NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_BANK_FD_PENALTY_LEDGERColumn.ColumnName].ToString());
                                        }
                                        //-----------------------------------------------------------------------------------------------------

                                        //On 09/11/2022, GST HSN, GST SAC Code -----------------------------------------------------------------
                                        GST_HSN_SAC_CODE = string.Empty;
                                        if (IsGSTLedger == 1 && dtLedger.Columns.Contains(this.AppSchema.Ledger.GST_HSN_SAC_CODEColumn.ColumnName))
                                        {
                                            GST_HSN_SAC_CODE = drLedger[this.AppSchema.Ledger.GST_HSN_SAC_CODEColumn.ColumnName].ToString().Trim();
                                        }

                                        //------------------------------------------------------------------------------------------------------

                                        Int32 lid = GetId(Id.Ledger);

                                        //On 06/01/2022, For Ledger Closed Date -------------------------
                                        LedgerDateClosed = DateTime.MinValue;
                                        if (dtLedger.Columns.Contains(this.AppSchema.Ledger.DATE_CLOSEDColumn.ColumnName))
                                        {
                                            if (drLedger[this.AppSchema.Ledger.DATE_CLOSEDColumn.ColumnName] != null)
                                            {
                                                if (actualledgername == LedgerName) //If and if only sample ledgers (show mapping enabled)
                                                {
                                                    string ledgerclosedate = drLedger[this.AppSchema.Ledger.DATE_CLOSEDColumn.ColumnName].ToString();
                                                    if (!string.IsNullOrEmpty(ledgerclosedate) && DateSet.ToDate(ledgerclosedate, false) != DateTime.MinValue)
                                                    {
                                                        LedgerDateClosed = DateSet.ToDate(drLedger[this.AppSchema.Ledger.DATE_CLOSEDColumn.ColumnName].ToString(), false);
                                                    }

                                                    ResultArgs result = CheckLedgerClosedDate(lid, LedgerDateClosed, GroupId);
                                                    if (!result.Success)
                                                    {
                                                        LedgerDateClosed = DateTime.MinValue;
                                                    }
                                                }
                                            }
                                        }

                                        LedgerClosedBy = 0;
                                        if (LedgerDateClosed != DateTime.MinValue && dtLedger.Columns.Contains(this.AppSchema.Ledger.CLOSED_BYColumn.ColumnName))
                                        {
                                            LedgerClosedBy = this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.CLOSED_BYColumn.ColumnName].ToString()); ;
                                        }
                                        //--------------------------------------------------------------------

                                        //On 15/05/2024, To Update FD Ledger Investment Type -----------------
                                        FDInvestmentTypeId = 0; //For all Ledgers
                                        if (LedSubType.ToUpper() == ledgerSubType.FD.ToString().ToUpper())
                                        {
                                            FDInvestmentTypeId = (Int32)FDInvestmentType.FD;
                                            if (drLedger.Table.Columns.Contains(this.AppSchema.Ledger.FD_INVESTMENT_TYPE_IDColumn.ColumnName))
                                            {
                                                FDInvestmentTypeId = this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.FD_INVESTMENT_TYPE_IDColumn.ColumnName].ToString());
                                            }
                                        }
                                        //--------------------------------------------------------------------

                                        resultArgs = EnableLedgerPropertiesDetails(lid);
                                    }
                                }
                            }

                            if (!resultArgs.Success)
                            {
                                break;
                            }
                        }
                        //if (!isLedgersMatched)
                        //{
                        //    resultArgs.Message = "Ledgers used in Branch Office is not available in Head Office. " +
                        //                         "Ledgers might have been newly created in Branch Office or removed in Head Office. " +
                        //                         "<br/> The Required Ledger(s) in Portal are:<br/><b> " + MismatchedLedgers.TrimEnd(',') + "</b>";
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally
            {

            }


            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ImportLedger Ended.");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Problem Importing Ledger: " + resultArgs.Message);
            }

            //On 22/02/2018, attach name of the project and voucher detailis in the error log
            if (!resultArgs.Success && string.IsNullOrEmpty(ProblemDataDetails))
            {
                ProblemDataDetails = "Ledger : " + LedgerName + "<br>" +
                                      "Problem in updating Master(Ledger)";
            }
            return resultArgs;
        }

        /// <summary>
        /// On 07/01/2022, To update Closed date for ledger after checking its vouchers and balances
        /// #1. Check Vouchers are available for Clsoed Date and for al the Proejcts
        /// #2. Check closing balances for Clsoed Date and for all the Proejcts
        /// </summary>
        /// <param name="LedgerId"></param>
        /// <param name="DateClosed"></param>
        /// <returns></returns>
        private ResultArgs CheckLedgerClosedDate(Int32 LId, DateTime DateClosed, Int32 LGroupId)
        {
            ResultArgs result = new ResultArgs();
            // Validate while closing the Bank Accounts if there is Transaction Exists or not if exists make it false in order to do the Transaction (Chinna)
            if (LId > 0 && DateClosed != DateTime.MinValue)
            {
                //#1. Check Vouchers are available for clsoed date
                result = CheckTransactionExistsByDateClosed(LId, DateClosed);
                if (result.Success)
                {
                    if (result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                    {
                        result.Message = "Transaction is made for this Closed Date (" + DateSet.ToDate(DateClosed.ToShortDateString()) + "), Ledger can not be closed.";
                    }
                    else
                    {
                        //On 07/01/2022, To check Ledger Closing balance only for Bank and FD Ledgers alone
                        //#2. Check Closing Balances are available for all the projects
                        if (LGroupId == (int)FixedLedgerGroup.BankAccounts || LGroupId == (int)FixedLedgerGroup.FixedDeposit)
                        {
                            using (BalanceSystem balancesystem = new BalanceSystem())
                            {
                                BalanceProperty ledgerclosingbalance = balancesystem.HasBalance(DateSet.ToDate(DateClosed.ToShortDateString()), 0, 0, LId, BalanceSystem.BalanceType.ClosingBalance);
                                if (ledgerclosingbalance.Amount > 0)
                                {
                                    result.Message = "Ledger has closing balance, It can't be closed on " + DateSet.ToDate(DateClosed.ToShortDateString());
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                result.Success = true;
            }

            return result;
        }

        /// <summary>
        /// On 07/01/2022, to check vouchers as available for given date and ledger
        /// </summary>
        /// <param name="CLedgerId"></param>
        /// <param name="DateClosed"></param>
        /// <returns></returns>
        private ResultArgs CheckTransactionExistsByDateClosed(int CLedgerId, DateTime DateClosed)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.CheckTransactionExistsByDateClose, SQLAdapterType.HOSQL))
            {
                dataManager.Parameters.Add(this.AppSchema.BankAccount.DATE_CLOSEDColumn, DateClosed);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.LEDGER_IDColumn, CLedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 15/05/2024, to check FD Account exists for given FD Ledger
        /// </summary>
        /// <returns></returns>
        private ResultArgs CheckFDAccountsExistsByLedger(int fdledgerid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.CheckFDAccountsExistsByLedger, SQLAdapterType.HOSQL))
            {
                dataManager.Parameters.Add(this.AppSchema.BankAccount.LEDGER_IDColumn, fdledgerid);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 05/07/2021, to import master gst class
        /// </summary>
        /// <returns></returns>
        private ResultArgs ImportGSTClass()
        {
            AcMEDataSynLog.WriteLog("ImportGSTClass Started.");
            try
            {
                bool isGSTClassExists = true;
                using (DataManager dataManager = new DataManager())
                {
                    if (dtGSTClass != null && dtGSTClass.Rows.Count > 0)
                    {
                        foreach (DataRow drGSTClass in dtGSTClass.Rows)
                        {
                            Slab = drGSTClass[this.AppSchema.MasterGSTClass.SLABColumn.ColumnName].ToString();

                            if (!string.IsNullOrEmpty(Slab))
                            {
                                resultArgs = IsExists(SQLCommand.ImportVoucher.IsGSTClassExists);
                                if (resultArgs.Success)
                                {
                                    isGSTClassExists = resultArgs.DataSource.Sclar.ToInteger > 0;
                                    if (!isGSTClassExists)
                                    {
                                        //Insert GST Class Details Details
                                        Slab = drGSTClass[this.AppSchema.MasterGSTClass.SLABColumn.ColumnName].ToString();
                                        GST = NumberSet.ToDouble(drGSTClass[this.AppSchema.MasterGSTClass.GSTColumn.ColumnName].ToString());
                                        CGST = NumberSet.ToDouble(drGSTClass[this.AppSchema.MasterGSTClass.CGSTColumn.ColumnName].ToString());
                                        SGST = NumberSet.ToDouble(drGSTClass[this.AppSchema.MasterGSTClass.SGSTColumn.ColumnName].ToString());
                                        //IGST = NumberSet.ToDouble(drGSTClass[this.AppSchema.MasterGSTClass.IGSTColumn].ToString());
                                        GSTClassApplicableFrom = DateSet.ToDate(drGSTClass[this.AppSchema.MasterGSTClass.APPLICABLE_FROMColumn.ColumnName].ToString(), false);
                                        GSTClassStatus = NumberSet.ToInteger(drGSTClass[this.AppSchema.MasterGSTClass.STATUSColumn.ColumnName].ToString());
                                        GSTClassSortOrder = NumberSet.ToInteger(drGSTClass[this.AppSchema.MasterGSTClass.SGSTColumn.ColumnName].ToString());
                                        resultArgs = SaveGSTClassDetails();
                                    }
                                }
                            }
                            else
                            {
                                resultArgs.Message = "GST Slab is empty in Branch Office.";
                            }

                            if (!resultArgs.Success)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ImportGSTClass Ended.");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in Importing GSTClass: " + resultArgs.Message);
            }

            //On 22/02/2018, attach name of the project and voucher detailis in the error log
            if (!resultArgs.Success && string.IsNullOrEmpty(ProblemDataDetails))
            {
                ProblemDataDetails = "GST Slab : " + Slab + "<br>" +
                                      "Problem in GST class";
            }
            return resultArgs;
        }

        /// <summary>
        /// On 09/07/2021, to import Asset & Stock Vendor details
        /// </summary>
        /// <returns></returns>
        private ResultArgs ImportAssetStockVendor()
        {
            AcMEDataSynLog.WriteLog("ImportAssetStockVendor Started.");
            try
            {
                bool isVendorExists = true;
                using (DataManager dataManager = new DataManager())
                {
                    if (dtAssetStockVendors != null && dtAssetStockVendors.Rows.Count > 0)
                    {
                        foreach (DataRow drAssetStockVendors in dtAssetStockVendors.Rows)
                        {
                            Vendor = drAssetStockVendors[this.AppSchema.Vendors.VENDORColumn.ColumnName].ToString();

                            if (!string.IsNullOrEmpty(Vendor))
                            {
                                resultArgs = IsExists(SQLCommand.ImportVoucher.IsVendorExists);
                                if (resultArgs.Success)
                                {
                                    isVendorExists = resultArgs.DataSource.Sclar.ToInteger > 0;
                                    if (!isVendorExists)
                                    {
                                        Vendor = drAssetStockVendors[this.AppSchema.Vendors.VENDORColumn.ColumnName].ToString();
                                        VendorAddress = drAssetStockVendors[this.AppSchema.Vendors.ADDRESSColumn.ColumnName].ToString();
                                        VendorPanNo = drAssetStockVendors[this.AppSchema.Vendors.PAN_NOColumn.ColumnName].ToString();
                                        VendorGSTNO = drAssetStockVendors[this.AppSchema.Vendors.GST_NOColumn.ColumnName].ToString();
                                        VendorContactNo = drAssetStockVendors[this.AppSchema.Vendors.CONTACT_NOColumn.ColumnName].ToString();
                                        VendorEmailId = drAssetStockVendors[this.AppSchema.Vendors.CONTACT_NOColumn.ColumnName].ToString();
                                        VendorBranchId = NumberSet.ToInteger(drAssetStockVendors[this.AppSchema.Vendors.BRANCH_IDColumn.ColumnName].ToString());

                                        CountryName = drAssetStockVendors[this.AppSchema.Country.COUNTRYColumn.ColumnName].ToString();
                                        CountryId = GetId(Id.Country);

                                        StateName = drAssetStockVendors[this.AppSchema.State.STATE_NAMEColumn.ColumnName].ToString();
                                        StateId = GetId(Id.State);

                                        resultArgs = SaveAssetStockVendor();
                                    }
                                }
                            }
                            else
                            {
                                resultArgs.Message = "Vendor name is empty in Vendor Master.";
                            }

                            if (!resultArgs.Success)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ImportAssetStockVendor Ended.");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in Importing Asset Stock Vendor: " + resultArgs.Message);
            }

            //On 22/02/2018, attach name of the project and voucher detailis in the error log
            if (!resultArgs.Success && string.IsNullOrEmpty(ProblemDataDetails))
            {
                ProblemDataDetails = "Asset Stock Vendor : " + Vendor + "<br>" +
                                      "Problem in Asset Stock Vendor";
            }
            return resultArgs;
        }

        /// <summary>
        /// On 06/07/2021, to Import Ledger Profile
        /// </summary>
        /// <returns></returns>
        private ResultArgs ImportLedgerProfile()
        {
            AcMEDataSynLog.WriteLog("ImportLedgerProfile Started.");
            try
            {
                bool isLedgerProfileExists = true;
                using (DataManager dataManager = new DataManager())
                {
                    if (dtLedgerProfile != null && dtLedgerProfile.Rows.Count > 0)
                    {
                        foreach (DataRow drLedgerProfile in dtLedgerProfile.Rows)
                        {
                            LedgerName = drLedgerProfile[this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();

                            if (!string.IsNullOrEmpty(LedgerName))
                            {
                                GetId(Id.Ledger);
                                LedgerId = GetId(Id.Ledger);
                                if (LedgerId > 0)
                                {
                                    resultArgs = IsExists(SQLCommand.ImportVoucher.IsLedgerProfileExists);
                                    if (resultArgs.Success)
                                    {
                                        isLedgerProfileExists = resultArgs.DataSource.Sclar.ToInteger > 0;
                                        LedgerProfileNatureofPaymentid = 0; // NumberSet.ToInteger(drLedgerProfile[this.AppSchema.LedgerProfileData.NATURE_OF_PAYMENT_IDColumn.ColumnName].ToString());
                                        LedgerProfileDeduteeTypeId = 0;     // NumberSet.ToInteger(drLedgerProfile[this.AppSchema.LedgerProfileData.DEDUTEE_TYPE_IDColumn.ColumnName].ToString());
                                        LedgerProfileName = drLedgerProfile[this.AppSchema.LedgerProfileData.NAMEColumn.ColumnName].ToString();
                                        LedgerProfileAddress = drLedgerProfile[this.AppSchema.LedgerProfileData.ADDRESSColumn.ColumnName].ToString();
                                        StateName = drLedgerProfile[this.AppSchema.State.STATE_NAMEColumn.ColumnName].ToString();
                                        CountryName = drLedgerProfile[this.AppSchema.Country.COUNTRYColumn.ColumnName].ToString();
                                        LedgerProfilePincode = drLedgerProfile[this.AppSchema.LedgerProfileData.PIN_CODEColumn.ColumnName].ToString();
                                        LedgerProfileContactNumber = drLedgerProfile[this.AppSchema.LedgerProfileData.CONTACT_NUMBERColumn.ColumnName].ToString();
                                        LedgerProfileContactPerson = drLedgerProfile[this.AppSchema.LedgerProfileData.CONTACT_PERSONColumn.ColumnName].ToString();
                                        LedgerProfileEmail = drLedgerProfile[this.AppSchema.LedgerProfileData.EMAILColumn.ColumnName].ToString();
                                        LedgerProfileGSTNO = drLedgerProfile[this.AppSchema.LedgerProfileData.GST_NOColumn.ColumnName].ToString();
                                        LedgerProfilePAN = drLedgerProfile[this.AppSchema.LedgerProfileData.PAN_NUMBERColumn.ColumnName].ToString();
                                        LedgerProfileNameOnPAN = drLedgerProfile[this.AppSchema.LedgerProfileData.PAN_IT_HOLDER_NAMEColumn.ColumnName].ToString();
                                        StateName = drLedgerProfile[this.AppSchema.State.STATE_NAMEColumn.ColumnName].ToString().Trim();
                                        CountryName = drLedgerProfile[this.AppSchema.Country.COUNTRYColumn.ColumnName].ToString().Trim();
                                        Slab = drLedgerProfile[this.AppSchema.MasterGSTClass.SLABColumn.ColumnName].ToString().Trim();
                                        StateId = GetId(Id.State);
                                        CountryId = GetId(Id.Country);
                                        LedgerProfileGSTID = GetId(Id.GSTClass);

                                        if (!string.IsNullOrEmpty(StateName) && StateId == 0)
                                        {
                                            resultArgs.Message = "State is not available '" + StateName + "'.";
                                        }
                                        else if (!string.IsNullOrEmpty(CountryName) && CountryId == 0)
                                        {
                                            resultArgs.Message = "Country is not available '" + CountryName + "'.";
                                        }
                                        else if (!string.IsNullOrEmpty(Slab) && LedgerProfileGSTID == 0)
                                        {
                                            resultArgs.Message = "GST Class is not available '" + Slab + "'.";
                                        }
                                        else
                                        {
                                            resultArgs = SaveLedgerProfile(isLedgerProfileExists);
                                        }
                                    }
                                }
                                else
                                {
                                    resultArgs.Message = "Ledger is not available '" + LedgerName + "'.";
                                }
                            }
                            else
                            {
                                resultArgs.Message = "Ledger is empty in Ledger Profile.";
                            }
                            if (!resultArgs.Success)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ImportLedgerProfile Ended.");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in Importing tLedger Profile: " + resultArgs.Message);
            }

            //On 22/02/2018, attach name of the project and voucher detailis in the error log
            if (!resultArgs.Success && string.IsNullOrEmpty(ProblemDataDetails))
            {
                ProblemDataDetails = "Ledger Name : " + LedgerName + "<br>" +
                                      "Problem in Ledger Profile";
            }
            return resultArgs;
        }



        /// <summary>
        /// Checks whether the Particular Country exists in Head Office, If not  country details will be added in Head office.
        /// </summary>
        /// <returns></returns>
        private ResultArgs ImportCountry()
        {
            AcMEDataSynLog.WriteLog("ImportCountry Started.");
            try
            {
                bool isCountryExists = true;
                using (DataManager dataManager = new DataManager())
                {
                    if (dtCountry != null && dtCountry.Rows.Count > 0)
                    {
                        foreach (DataRow drCountry in dtCountry.Rows)
                        {
                            CountryName = drCountry[this.AppSchema.Country.COUNTRYColumn.ColumnName].ToString();

                            if (!string.IsNullOrEmpty(CountryName))
                            {
                                resultArgs = IsExists(SQLCommand.ImportVoucher.IsCountryExists);
                                if (resultArgs.Success)
                                {
                                    isCountryExists = resultArgs.DataSource.Sclar.ToInteger > 0;
                                    if (!isCountryExists)
                                    {
                                        //Insert Currency Country Details
                                        CountryName = drCountry[this.AppSchema.Country.COUNTRYColumn.ColumnName].ToString();
                                        CountryCode = drCountry[this.AppSchema.Country.COUNTRY_CODEColumn.ColumnName].ToString();
                                        CurrencyCode = drCountry[this.AppSchema.Country.CURRENCY_CODEColumn.ColumnName].ToString();
                                        CurrencySymbol = drCountry[this.AppSchema.Country.CURRENCY_SYMBOLColumn.ColumnName].ToString();
                                        CurrencyName = drCountry[this.AppSchema.Country.CURRENCY_NAMEColumn.ColumnName].ToString();

                                        resultArgs = SaveCountryDetails();
                                    }
                                }
                            }
                            else
                            {
                                resultArgs.Message = "Country is empty in Branch Office.";
                            }

                            if (!resultArgs.Success)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ImportCountry Ended.");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in Importing Country: " + resultArgs.Message);
            }

            //On 22/02/2018, attach name of the project and voucher detailis in the error log
            if (!resultArgs.Success && string.IsNullOrEmpty(ProblemDataDetails))
            {
                ProblemDataDetails = "Country : " + CountryName + "<br>" +
                                      "Problem in updating Master (Country)";
            }
            return resultArgs;
        }

        /// <summary>
        /// Checks whether the Particular State exists in Head Office, If not state details will be added in Head office.
        /// </summary>
        /// <returns></returns>
        private ResultArgs ImportState()
        {
            AcMEDataSynLog.WriteLog("ImportState Started.");
            try
            {
                bool isStateExists = true;
                using (DataManager dataManager = new DataManager())
                {
                    if (dtState != null && dtState.Rows.Count > 0)
                    {
                        foreach (DataRow drState in dtState.Rows)
                        {
                            StateName = drState[this.AppSchema.State.STATE_NAMEColumn.ColumnName].ToString();
                            StateCode = string.Empty;
                            if (dtState.Columns.Contains(this.AppSchema.State.STATE_CODEColumn.ColumnName))
                            {
                                StateCode = drState[this.AppSchema.State.STATE_CODEColumn.ColumnName].ToString();
                            }

                            if (!string.IsNullOrEmpty(StateName))
                            {
                                resultArgs = IsExists(SQLCommand.ImportVoucher.IsStateExists);
                                if (resultArgs.Success)
                                {
                                    isStateExists = resultArgs.DataSource.Sclar.ToInteger > 0;
                                    if (!isStateExists)
                                    {
                                        //Insert State Details
                                        StateName = drState[this.AppSchema.State.STATE_NAMEColumn.ColumnName].ToString();
                                        CountryName = drState[this.AppSchema.Country.COUNTRYColumn.ColumnName].ToString();
                                        StateCountryId = GetId(Id.Country);
                                        if (StateCountryId > 0)
                                        {
                                            resultArgs = SaveStateDetails();
                                        }
                                        else
                                        {
                                            resultArgs.Message = "State's country is empty";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                resultArgs.Message = "State is empty.";
                            }

                            if (!resultArgs.Success)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ImportState Ended.");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in Importing State: " + resultArgs.Message);
            }

            //On 22/02/2018, attach name of the project and voucher detailis in the error log
            if (!resultArgs.Success && string.IsNullOrEmpty(ProblemDataDetails))
            {
                ProblemDataDetails = "State : " + StateName + "<br>" +
                                      "Problem in updating Master (State)";
            }
            return resultArgs;
        }

        /// <summary>
        /// Import donor from project mapping donor or from voucher donor
        /// </summary>
        /// <returns></returns>
        private ResultArgs ImportDonorInfo()
        {
            try
            {
                resultArgs = ImportDonorInfo(true);
                if (resultArgs.Success)
                {
                    resultArgs = ImportDonorInfo(false);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally { }
            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ImportDonorInfo Ended.");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Problem in Import Donor :" + resultArgs.Message);
            }
            return resultArgs;
        }

        /// <summary>
        /// Checks whether the Donor is available in Head Office. If not the donor details will be added in Head Office.
        /// fromProjectMapped : Import from Master Poject Donors or Import from Voucher donor
        /// By mistakenly, if donor has voucher without mapping
        /// <returns></returns>
        /// 
        /// <summary>
        private ResultArgs ImportDonorInfo(bool fromProjectMapped)
        {
            AcMEDataSynLog.WriteLog("ImportDonorInfo Started..");
            try
            {
                bool isDonorExists = true;
                using (DataManager dataManager = new DataManager())
                {
                    DataTable dtDonorInfo = dtDonor; //From Voucher donor
                    if (fromProjectMapped)
                    {
                        dtDonorInfo = dtProjectDonors; //From Master donor
                    }

                    if (dtDonorInfo != null && dtDonorInfo.Rows.Count > 0)
                    {
                        foreach (DataRow drDonor in dtDonorInfo.Rows)
                        {
                            DonorName = drDonor[this.AppSchema.DonorAuditor.NAMEColumn.ColumnName].ToString();

                            if (!string.IsNullOrEmpty(DonorName))
                            {
                                resultArgs = IsExists(SQLCommand.ImportVoucher.IsDonorExists);
                                if (resultArgs.Success)
                                {
                                    isDonorExists = resultArgs.DataSource.Sclar.ToInteger > 0;
                                    if (!isDonorExists)
                                    {
                                        //Insert Donor Info
                                        DonorName = drDonor[this.AppSchema.DonorAuditor.NAMEColumn.ColumnName].ToString();
                                        Place = drDonor[this.AppSchema.DonorAuditor.PLACEColumn.ColumnName].ToString();
                                        DonorType = this.NumberSet.ToInteger(drDonor[this.AppSchema.DonorAuditor.TYPEColumn.ColumnName].ToString());
                                        CompanyName = drDonor[this.AppSchema.DonorAuditor.COMPANY_NAMEColumn.ColumnName].ToString();
                                        CountryName = drDonor[this.AppSchema.Country.COUNTRYColumn.ColumnName].ToString();
                                        CountryId = GetId(Id.Country);
                                        Pincode = drDonor[this.AppSchema.DonorAuditor.PINCODEColumn.ColumnName].ToString();
                                        Phone = drDonor[this.AppSchema.DonorAuditor.PHONEColumn.ColumnName].ToString();
                                        Email = drDonor[this.AppSchema.DonorAuditor.EMAILColumn.ColumnName].ToString();
                                        Fax = drDonor[this.AppSchema.DonorAuditor.FAXColumn.ColumnName].ToString();
                                        URL = drDonor[this.AppSchema.DonorAuditor.URLColumn.ColumnName].ToString();
                                        DonorStateName = drDonor[this.AppSchema.DonorAuditor.STATEColumn.ColumnName].ToString();
                                        StateId = 0;
                                        if (CountryId > 0)
                                        {
                                            StateName = drDonor[this.AppSchema.State.STATE_NAMEColumn.ColumnName].ToString();
                                            StateId = GetId(Id.State);
                                        }
                                        DonorAddress = drDonor[this.AppSchema.DonorAuditor.ADDRESSColumn.ColumnName].ToString();
                                        PAN = drDonor[this.AppSchema.DonorAuditor.PANColumn.ColumnName].ToString();
                                        IdentityKey = 0;  // 0 - Donor , 1 - Auditor

                                        //Branch Office Donor Details Validation.
                                        if (string.IsNullOrEmpty(DonorName))
                                        {
                                            resultArgs.Message = "Donor Name is Empty in Brach Office Voucher file while updating Donor Details.";
                                        }
                                        //else if (string.IsNullOrEmpty(CountryName))
                                        //{
                                        //    resultArgs.Message = "Donor country is Empty in Brach Office Voucher file while updating Donor Details..";
                                        //}
                                        //else if (CountryId == 0)
                                        //{
                                        //    resultArgs.Message = "Donor country doesn't exists in Head Office while updating Donor Details.";
                                        //}
                                        else
                                        {
                                            resultArgs = SaveDonorDetails();
                                        }
                                    }
                                }
                            }

                            if (!resultArgs.Success)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ImportDonorInfo Ended.");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in Importing Donor Details: " + resultArgs.Message);
            }

            //On 22/02/2018, attach name of the project and voucher detailis in the error log
            if (!resultArgs.Success && string.IsNullOrEmpty(ProblemDataDetails))
            {
                ProblemDataDetails = "Donor : " + DonorName + "<br>" +
                                      "Problem in updating Master (Donor)";
            }
            return resultArgs;
        }

        /// <summary>
        /// Import CC from project cc or from voucher cc
        /// </summary>
        /// <returns></returns>
        private ResultArgs ImportCostCentre()
        {
            try
            {
                resultArgs = ImportCostCentre(true);
                if (resultArgs.Success)
                {
                    resultArgs = ImportCostCentre(false);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally { }
            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ImportCostCentre Ended.");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Problem in Import Cost Center :" + resultArgs.Message);
            }
            return resultArgs;
        }

        /// <summary>
        /// Checks whether the particular Cost Centre is available in Head Office. If not Cost centre details will be added in Head Office.
        /// fromCCProjectMapped : Import from Master Poject Costcentre or Import from Voucher CC
        /// By mistakenly, if CC has voucher without mapping
        /// </summary>
        /// <returns></returns>
        private ResultArgs ImportCostCentre(bool fromCCProjectMapped)
        {
            AcMEDataSynLog.WriteLog("ImportCostCentre Started...Import from " + (fromCCProjectMapped ? "From Master" : "From CC Voucher"));
            try
            {
                bool isCostCentreExists = true;

                using (DataManager dataManager = new DataManager())
                {
                    DataTable dtCC = dtVoucherCostCentre; //From Voucher CC
                    if (fromCCProjectMapped)
                    {
                        dtCC = dtProjectCostCentres; //From Master CC
                    }

                    if (dtCC != null && dtCC.Rows.Count > 0)
                    {
                        //On 01/02/2024, Get unique CCs
                        string[] CCNames = new string[] { this.AppSchema.CostCentre.COST_CENTRE_NAMEColumn.ColumnName , 
                                            this.AppSchema.CostCentre.COST_CENTRE_CATEGORY_NAMEColumn.ColumnName};
                        DataTable dtMasterCC = dtCC.DefaultView.ToTable(true, CCNames);


                        foreach (DataRow drCostCentre in dtMasterCC.Rows)
                        {
                            CostCentreName = drCostCentre[this.AppSchema.CostCentre.COST_CENTRE_NAMEColumn.ColumnName].ToString();
                            CostCategory = drCostCentre[this.AppSchema.CostCentre.COST_CENTRE_CATEGORY_NAMEColumn.ColumnName].ToString();

                            if (!string.IsNullOrEmpty(CostCentreName))
                            {
                                resultArgs = IsExists(SQLCommand.ImportVoucher.IsCostCentreExists);
                                if (resultArgs.Success)
                                {
                                    isCostCentreExists = resultArgs.DataSource.Sclar.ToInteger > 0;
                                    if (!isCostCentreExists)
                                    {
                                        //Save Cost Centre Details.
                                        resultArgs = SaveCostCentreDetails();
                                    }

                                    //Import Cost Category
                                    if (resultArgs.Success)
                                    {
                                        resultArgs = ImportCostCentegory();
                                    }

                                    //Map Cost Category with Cost Centre
                                    if (resultArgs.Success)
                                    {
                                        CostCenterId = GetId(Id.CostCentre);
                                        CostCategoryId = GetId(Id.CostCategory);

                                        //Validation
                                        if (CostCenterId > 0 && CostCategoryId > 0)
                                        {
                                            resultArgs = MapProject(SQLCommand.ImportVoucher.MapCostCategory);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                resultArgs.Message = "Cost Centre Name is Empty in Branch Office.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally { }
            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ImportCostCentre Ended...Import from " + (fromCCProjectMapped ? "From Master" : "From CC Voucher"));
            }
            else
            {
                AcMEDataSynLog.WriteLog("Problem in Import Cost Center Import from " + (fromCCProjectMapped ? "From Master" : "From CC Voucher") + " :" + resultArgs.Message);
            }

            //On 22/02/2018, attach name of the project and voucher detailis in the error log
            if (!resultArgs.Success && string.IsNullOrEmpty(ProblemDataDetails))
            {
                ProblemDataDetails = "Cost Center : " + CostCentreName + " Import From " + (fromCCProjectMapped ? "From Master" : "From CC Voucher") + "<br>" +
                                      "Problem in updating Master (Cost Center Vouchers)";
            }
            return resultArgs;
        }


        /// <summary>
        /// Checks whether the given  Cost Category available in Head Office. If not the Cost Category details will be inserted in Head Office.
        /// </summary>
        /// <returns></returns>
        private ResultArgs ImportCostCentegory()
        {
            try
            {
                bool isCostCategoryExists = true;
                if (!string.IsNullOrEmpty(CostCategory))
                {
                    resultArgs = IsExists(SQLCommand.ImportVoucher.IsCostCategoryExists);
                    if (resultArgs.Success)
                    {
                        isCostCategoryExists = resultArgs.DataSource.Sclar.ToInteger > 0;
                        if (!isCostCategoryExists)
                        {
                            resultArgs = SaveCostCategoryDetails();
                        }
                    }
                }
                else
                {
                    resultArgs.Message = "Cost Center Category is Empty in Branch Office.";
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }

            if (!resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("Problem in ImportCostCategory :" + resultArgs.Message);
            }


            return resultArgs;
        }

        /// <summary>
        /// Checks whether the given Bank available in Head Office. If not the Bank details will be inserted in Head Office.
        /// </summary>
        /// <returns></returns>
        private ResultArgs ImportBank()
        {
            AcMEDataSynLog.WriteLog("ImportBank Started..");
            try
            {
                bool isBankExists = true;
                using (DataManager dataManager = new DataManager())
                {
                    if (dtBankAccount != null && dtBankAccount.Rows.Count > 0)
                    {
                        foreach (DataRow drBank in dtBankAccount.Rows)
                        {
                            BankName = drBank[this.AppSchema.Bank.BANKColumn.ColumnName].ToString();
                            Branch = drBank[this.AppSchema.Bank.BRANCHColumn.ColumnName].ToString();

                            if (!string.IsNullOrEmpty(BankName))
                            {
                                resultArgs = IsExists(SQLCommand.ImportVoucher.IsBankExists);
                                if (resultArgs.Success)
                                {
                                    isBankExists = resultArgs.DataSource.Sclar.ToInteger > 0;
                                    if (!isBankExists)
                                    {
                                        //Insert Bank Details
                                        BankCode = drBank[this.AppSchema.Bank.BANK_CODEColumn.ColumnName].ToString();
                                        BankName = drBank[this.AppSchema.Bank.BANKColumn.ColumnName].ToString();
                                        Address = drBank[this.AppSchema.Bank.ADDRESSColumn.ColumnName].ToString();
                                        IFSCCode = drBank[this.AppSchema.Bank.IFSCCODEColumn.ColumnName].ToString();
                                        MICRCode = drBank[this.AppSchema.Bank.MICRCODEColumn.ColumnName].ToString();
                                        ContactNumber = drBank[this.AppSchema.Bank.CONTACTNUMBERColumn.ColumnName].ToString();
                                        SWIFTCode = drBank[this.AppSchema.Bank.SWIFTCODEColumn.ColumnName].ToString();

                                        if (string.IsNullOrEmpty(BankName))
                                        {
                                            resultArgs.Message = "Bank Name is Empty in Branch Office";
                                        }
                                        else if (string.IsNullOrEmpty(Branch))
                                        {
                                            resultArgs.Message = "Branch is Empty in Branch Office";
                                        }
                                        else
                                        {
                                            resultArgs = SaveBankDetails();
                                        }
                                    }
                                }
                            }

                            if (!resultArgs.Success)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Importing Bank and Branch Details: " + ex.ToString();
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ImportBank Ended.");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in Importing Bank and Branch Details:" + resultArgs.Message);
            }
            return resultArgs;
        }

        /// <summary>
        /// This method will check for the Concern bank account details have entries in Ledger table. 
        /// If not the Bank account details will be insertd in Ledger Table.
        /// ImportBankAccount will be called if sucuessfully imported all ledger bank account
        /// </summary>
        /// <returns></returns>
        private ResultArgs ImportLedgerBankAccount()
        {
            AcMEDataSynLog.WriteLog("ImportLedgerBankAccount Started..");
            try
            {
                bool isLedgerExists = true;
                using (DataManager dataManager = new DataManager())
                {
                    if (dtBankAccount != null && dtBankAccount.Rows.Count > 0)
                    {
                        AcMEDataSynLog.WriteLog("ImportLedgerBankAccount Started..");
                        foreach (DataRow drBank in dtBankAccount.Rows)
                        {
                            AccountNumber = drBank[this.AppSchema.BankAccount.ACCOUNT_NUMBERColumn.ColumnName].ToString();
                            if (!string.IsNullOrEmpty(AccountNumber))
                            {
                                resultArgs = IsExists(SQLCommand.ImportVoucher.IsLedgerBankExists);
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    isLedgerExists = resultArgs.DataSource.Sclar.ToInteger > 0;
                                    if (!isLedgerExists)
                                    {
                                        LedgerCode = drBank[this.AppSchema.BankAccount.ACCOUNT_CODEColumn.ColumnName].ToString();
                                        LedgerName = drBank[this.AppSchema.BankAccount.ACCOUNT_NUMBERColumn.ColumnName].ToString();
                                        GroupId = (int)FixedLedgerGroup.BankAccounts;
                                        IsBranchLedger = 1;

                                        if (string.IsNullOrEmpty(LedgerName))
                                        {
                                            resultArgs.Message = "Bank Account Ledger Name is Empty in Branch Office";
                                        }
                                        else
                                        {
                                            resultArgs = SaveLedgerBankAccount();
                                        }
                                    }
                                }
                            }

                            if (!resultArgs.Success)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ImportLedgerBankAccount Ended.");
                resultArgs = ImportBankAccount();  //Bank Account Details
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in Importing Bank Account details in Ledger: " + resultArgs.Message);
            }
            return resultArgs;
        }

        /// <summary>
        /// This method will check for Bank account details available. If not the Bank Account details will be Inserted.
        /// </summary>
        /// <returns></returns>
        private ResultArgs ImportBankAccount()
        {
            AcMEDataSynLog.WriteLog("ImportBankAccount Started..");
            try
            {
                bool isBankAccountExits = true;
                using (DataManager dataManager = new DataManager())
                {
                    if (dtBankAccount != null && dtBankAccount.Rows.Count > 0)
                    {
                        foreach (DataRow drBankAccount in dtBankAccount.Rows)
                        {
                            AccountNumber = LedgerName = drBankAccount[this.AppSchema.BankAccount.ACCOUNT_NUMBERColumn.ColumnName].ToString();
                            if (!string.IsNullOrEmpty(AccountNumber))
                            {
                                //Insert Bank Account Info
                                AccountCode = drBankAccount[this.AppSchema.BankAccount.ACCOUNT_CODEColumn.ColumnName].ToString();
                                AccountHolderName = drBankAccount[this.AppSchema.BankAccount.ACCOUNT_HOLDER_NAMEColumn.ColumnName].ToString();
                                LedgerId = GetId(Id.Ledger);    // Get the Id of Bank Account in Ledger Table
                                BankName = drBankAccount[this.AppSchema.Bank.BANKColumn.ColumnName].ToString();
                                Branch = drBankAccount[this.AppSchema.Bank.BRANCHColumn.ColumnName].ToString();
                                BankId = GetId(Id.Bank);     //Bank Id
                                Is_FCRA_Account = this.NumberSet.ToInteger(drBankAccount[this.AppSchema.BankAccount.IS_FCRA_ACCOUNTColumn.ColumnName].ToString());
                                AccountType = this.NumberSet.ToInteger(drBankAccount[this.AppSchema.BankAccount.ACCOUNT_TYPE_IDColumn.ColumnName].ToString());
                                DateOpened = this.DateSet.ToDate(drBankAccount[this.AppSchema.BankAccount.DATE_OPENEDColumn.ColumnName].ToString(), false);
                                DateClosed = DateTime.MinValue;

                                if (dtBankAccount.Columns.Contains(this.AppSchema.BankAccount.DATE_CLOSEDColumn.ColumnName) &&
                                    !string.IsNullOrEmpty(drBankAccount[this.AppSchema.BankAccount.DATE_CLOSEDColumn.ColumnName].ToString()))
                                {
                                    DateClosed = this.DateSet.ToDate(drBankAccount[this.AppSchema.BankAccount.DATE_CLOSEDColumn.ColumnName].ToString(), false);
                                }

                                //Bank Account details validation
                                if (LedgerId == 0)
                                {
                                    resultArgs.Message = "Bank account is not available in portal : " + AccountNumber;
                                }
                                else if (BankId == 0)
                                {
                                    resultArgs.Message = "Bank is not available in Portal : " + BankName;
                                }
                                else
                                {
                                    BankAccountId = 0;
                                    resultArgs = IsExists(SQLCommand.ImportVoucher.IsBankAccountExists);
                                    if (resultArgs.Success)
                                    {
                                        isBankAccountExits = resultArgs.DataSource.Sclar.ToInteger > 0;
                                        if (isBankAccountExits)
                                        {
                                            BankAccountId = GetId(Id.BankAccount);
                                        }
                                        resultArgs = SaveBankAccountDetails();
                                    }
                                }
                            }

                            if (!resultArgs.Success)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ImportBankAccount Ended.");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in Importing Bank Account Details: " + resultArgs.Message);
            }
            return resultArgs;
        }

        /// <summary>
        /// 17/02/2020, This method will check Sub Ledgers. If not available Sub Ledger, it will be Inserted.
        /// </summary>
        /// <returns></returns>
        public ResultArgs ImportSubLedger(DataTable dtTmpSubLedgers)
        {
            dtSubLedger = dtTmpSubLedgers;
            AcMEDataSynLog.WriteLog("ImportSubLedger Started..");
            try
            {
                using (DataManager dataManager = new DataManager())
                {
                    if (dtSubLedger != null && dtSubLedger.Rows.Count > 0)
                    {
                        foreach (DataRow drSubLedger in dtSubLedger.Rows)
                        {
                            LedgerName = drSubLedger[this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                            LedgerId = GetId(Id.Ledger);    // Get the Ledger Id

                            //Check Main ledger is available in poral
                            if (LedgerId > 0)
                            {
                                SubLedgerName = drSubLedger[this.AppSchema.SubLedger.SUB_LEDGER_NAMEColumn.ColumnName].ToString();
                                SubLedgerId = GetId(Id.SubLedger);    // Get the Sub Ledger Id

                                //Check Sub Ledger is available in portal, if not exists, create new sub ledger in poral
                                if (SubLedgerId == 0)
                                {
                                    resultArgs = SaveSubLedgerDetails();
                                    if (resultArgs.Success)
                                    {
                                        SubLedgerId = this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                    }
                                }

                                //Clear and Map SubLeger with Ledger
                                if (LedgerId > 0 && SubLedgerId > 0)
                                {
                                    resultArgs = MapSubLedger();
                                }
                            }
                            else
                            {
                                resultArgs.Message = "Sub Ledger's Branch Ledger (" + LedgerName + ")  is not available in Head Office." + Environment.NewLine
                                                + "Ledgers would have been rearranged in Head office. Import masters and try again.";
                            }

                            if (!resultArgs.Success)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ImportSubLedger Ended.");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in Importing Sub Ledgers Details: " + resultArgs.Message);
            }
            return resultArgs;
        }

        private ResultArgs IsProjectMappedWithBranch()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.IsProjectBranchMapped, DataBaseTypeName, SQLAdapterType.HOSQL))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_IDColumn, BranchId);

                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        /// <summary>
        /// Save Project Category Details
        /// </summary>
        /// <returns></returns>
        public ResultArgs SaveProjectCatogoryDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.InsertProjectCategory, DataBaseTypeName, SQLAdapterType.HOSQL))
            {
                dataManager.Parameters.Add(this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn, ProjectCategoryName);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// Save Project Details.
        /// </summary>
        /// <returns></returns>
        private ResultArgs SaveProjectDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.InsertProject, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_CODEColumn, ProjectCode);
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECTColumn, ProjectName);
                    dataManager.Parameters.Add(this.AppSchema.Project.CUSTOMERIDColumn, LegalEntityId);
                    dataManager.Parameters.Add(this.AppSchema.Project.ACCOUNT_DATEColumn, AccountDate);
                    dataManager.Parameters.Add(this.AppSchema.Project.DESCRIPTIONColumn, Description);
                    dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, StartedOn);
                    dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, Closed_On);
                    dataManager.Parameters.Add(this.AppSchema.Project.DIVISION_IDColumn, DivisionId);
                    dataManager.Parameters.Add(this.AppSchema.Project.NOTESColumn, Notes);
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_CATEGORY_IDColumn, ProjectCategroyId);

                    //On 04/07/2023, Closed by 0 - Branch Office 1- Head Office
                    dataManager.Parameters.Add(this.AppSchema.Project.CLOSED_BYColumn, ProjectClosedBy);

                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Save Project Details." + ex.Message;
            }
            return resultArgs;
        }

        private ResultArgs MapDefaultVoucher()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.MapDefaultVoucher, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in mapping default voucher Details." + ex.Message;
            }
            return resultArgs;
        }

        public ResultArgs SaveLedgerGroupDetails()
        {
            using (DataManager dataManager = new DataManager((GroupId == 0) ? SQLCommand.ImportVoucher.InsertLedgerGroup : SQLCommand.ImportVoucher.UpdateLedgerGroup, DataBaseTypeName, SQLAdapterType.HOSQL))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_CODEColumn, Abbrevation);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.LEDGER_GROUPColumn, LedgerGroup);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.PARENT_GROUP_IDColumn, ParentGroupId);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.NATURE_IDColumn, NatureId);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.MAIN_GROUP_IDColumn, MainGroupId);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, GroupId);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.IMAGE_IDColumn, ImageId);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.SORT_ORDERColumn, SortOrder);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// Save the General Ledger Details
        /// </summary>
        /// <returns></returns>
        private ResultArgs SaveLedgerDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.InsertLedger, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_CODEColumn, LedgerCode.ToUpper());
                    dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, LedgerName);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.GROUP_IDColumn, GroupId);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_TYPEColumn, LedType);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_SUB_TYPEColumn, LedSubType);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.SORT_IDColumn, SortId);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_BRANCH_LEDGERColumn, IsBranchLedger);
                    //Added by Carmel Raj on August-18-2015
                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_COST_CENTERColumn, IsCostCentre);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_BANK_INTEREST_LEDGERColumn, IsBankInterestLedger);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_TDS_LEDGERColumn, IsTDSLedger);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_INKIND_LEDGERColumn, IsInKindLedger);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_DEPRECIATION_LEDGERColumn, IsDepreciationLedger);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_ASSET_GAIN_LEDGERColumn, IsAssetGainLedger);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_ASSET_LOSS_LEDGERColumn, IsAssetLossLedger);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_DISPOSAL_LEDGERColumn, IsDisposalLedger);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_SUBSIDY_LEDGERColumn, IsSubSidyLedger);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_GST_LEDGERSColumn, IsGSTLedger);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.GST_SERVICE_TYPEColumn, GSTServiceTyper);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.BUDGET_GROUP_IDColumn, BudgetGroupId);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.BUDGET_SUB_GROUP_IDColumn, BudgetSubGroupId);

                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_BANK_SB_INTEREST_LEDGERColumn, IsBankSBInterestLedger);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_BANK_COMMISSION_LEDGERColumn, IsBankCommissionInterestLedger);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_BANK_FD_PENALTY_LEDGERColumn, IsBankPenaltyInterestLedger);

                    dataManager.Parameters.Add(this.AppSchema.Ledger.GST_HSN_SAC_CODEColumn, GST_HSN_SAC_CODE);

                    dataManager.Parameters.Add(this.AppSchema.Ledger.FD_INVESTMENT_TYPE_IDColumn, FDInvestmentTypeId);

                    if (LedgerDateClosed != null && LedgerDateClosed == DateTime.MinValue)
                    {
                        dataManager.Parameters.Add(this.AppSchema.Ledger.DATE_CLOSEDColumn, null);
                    }
                    else
                    {
                        dataManager.Parameters.Add(this.AppSchema.Ledger.DATE_CLOSEDColumn, LedgerDateClosed);
                    }

                    //On 04/07/2023, Closed by 0 - Branch Office 1- Head Office
                    dataManager.Parameters.Add(this.AppSchema.Ledger.CLOSED_BYColumn, LedgerClosedBy);

                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Saving Ledger Details: " + ex.ToString();
            }
            return resultArgs;
        }


        /// <summary>
        /// On 24/06/2021, Update General Ledger Details
        /// </summary>
        /// <returns></returns>
        private ResultArgs EnableLedgerPropertiesDetails(Int32 lid)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.EnableLedgerPropertiesDetails, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, lid);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_COST_CENTERColumn.ColumnName, IsCostCentre);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_BANK_INTEREST_LEDGERColumn, IsBankInterestLedger);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_TDS_LEDGERColumn, IsTDSLedger);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_INKIND_LEDGERColumn, IsInKindLedger);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_DEPRECIATION_LEDGERColumn, IsDepreciationLedger);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_ASSET_GAIN_LEDGERColumn, IsAssetGainLedger);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_ASSET_LOSS_LEDGERColumn, IsAssetLossLedger);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_DISPOSAL_LEDGERColumn, IsDisposalLedger);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_SUBSIDY_LEDGERColumn, IsSubSidyLedger);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_GST_LEDGERSColumn, IsGSTLedger);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.GST_SERVICE_TYPEColumn, GSTServiceTyper);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.BUDGET_GROUP_IDColumn, BudgetGroupId);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.BUDGET_SUB_GROUP_IDColumn, BudgetSubGroupId);

                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_BANK_SB_INTEREST_LEDGERColumn, IsBankSBInterestLedger);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_BANK_COMMISSION_LEDGERColumn, IsBankCommissionInterestLedger);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_BANK_FD_PENALTY_LEDGERColumn, IsBankPenaltyInterestLedger);

                    dataManager.Parameters.Add(this.AppSchema.Ledger.GST_HSN_SAC_CODEColumn, GST_HSN_SAC_CODE);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.FD_INVESTMENT_TYPE_IDColumn, FDInvestmentTypeId);

                    //dataManager.Parameters.Add(this.AppSchema.Ledger.DATE_CLOSEDColumn, LedgerDateClosed);
                    if (LedgerDateClosed != null && LedgerDateClosed == DateTime.MinValue)
                    {
                        dataManager.Parameters.Add(this.AppSchema.Ledger.DATE_CLOSEDColumn, null);
                    }
                    else
                    {
                        dataManager.Parameters.Add(this.AppSchema.Ledger.DATE_CLOSEDColumn, LedgerDateClosed);
                    }
                    dataManager.Parameters.Add(this.AppSchema.Ledger.CLOSED_BYColumn, LedgerClosedBy);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Saving Ledger Details: " + ex.ToString();
            }
            return resultArgs;
        }

        /// </summary>
        /// 05/07/2021, To save GST master gst class details
        /// <returns></returns>
        private ResultArgs SaveGSTClassDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.InsertGSTClass, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(AppSchema.MasterGSTClass.SLABColumn, Slab);
                    dataManager.Parameters.Add(AppSchema.MasterGSTClass.GSTColumn, GST);
                    dataManager.Parameters.Add(AppSchema.MasterGSTClass.CGSTColumn, CGST);
                    dataManager.Parameters.Add(AppSchema.MasterGSTClass.SGSTColumn, SGST);
                    dataManager.Parameters.Add(AppSchema.MasterGSTClass.IGSTColumn, IGST);
                    dataManager.Parameters.Add(AppSchema.MasterGSTClass.APPLICABLE_FROMColumn, GSTClassApplicableFrom);
                    dataManager.Parameters.Add(AppSchema.MasterGSTClass.SORT_ORDERColumn, GSTClassSortOrder);
                    dataManager.Parameters.Add(AppSchema.MasterGSTClass.STATUSColumn, GSTClassStatus);

                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Saving GST Class Details: " + ex.ToString();
            }
            finally { }

            return resultArgs;
        }

        /// </summary>
        /// 09/07/2021, To save GST master gst class details
        /// <returns></returns>
        private ResultArgs SaveAssetStockVendor()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.InsertAssetStockVendor, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(AppSchema.Vendors.VENDORColumn, Vendor);
                    dataManager.Parameters.Add(AppSchema.Vendors.ADDRESSColumn, VendorAddress);
                    dataManager.Parameters.Add(AppSchema.Vendors.STATE_IDColumn, StateId);
                    dataManager.Parameters.Add(AppSchema.Vendors.COUNTRY_IDColumn, CountryId);
                    dataManager.Parameters.Add(AppSchema.Vendors.PAN_NOColumn, VendorPanNo);
                    dataManager.Parameters.Add(AppSchema.Vendors.GST_NOColumn, VendorGSTNO);
                    dataManager.Parameters.Add(AppSchema.Vendors.CONTACT_NOColumn, VendorContactNo);
                    dataManager.Parameters.Add(AppSchema.Vendors.EMAIL_IDColumn, VendorEmailId);
                    dataManager.Parameters.Add(AppSchema.Vendors.BRANCH_IDColumn, VendorBranchId);

                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Saving Vendor : " + ex.ToString();
            }
            finally { }

            return resultArgs;
        }



        /// </summary>
        /// 05/07/2021, To save GST master gst class details
        /// <returns></returns>
        private ResultArgs SaveLedgerProfile(bool isexists)
        {
            try
            {
                using (DataManager dataManager = new DataManager((!isexists ? SQLCommand.ImportVoucher.InsertLedgerProfile : SQLCommand.ImportVoucher.UpdateLedgerProfile), DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.LEDGER_IDColumn, LedgerId);
                    dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.DEDUTEE_TYPE_IDColumn, LedgerProfileNatureofPaymentid);
                    dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.NATURE_OF_PAYMENT_IDColumn, LedgerProfileDeduteeTypeId);
                    dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.NAMEColumn, LedgerProfileName);
                    dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.ADDRESSColumn, LedgerProfileAddress);
                    dataManager.Parameters.Add(this.AppSchema.State.STATE_IDColumn, StateId);
                    dataManager.Parameters.Add(this.AppSchema.Country.COUNTRY_IDColumn, CountryId);
                    dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.GST_NOColumn, LedgerProfileGSTNO);
                    dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.PAN_NUMBERColumn, LedgerProfilePAN);
                    dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.PAN_IT_HOLDER_NAMEColumn, LedgerProfileNameOnPAN);
                    dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.PIN_CODEColumn, LedgerProfilePincode);
                    dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.CONTACT_PERSONColumn, LedgerProfileContactPerson);
                    dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.CONTACT_NUMBERColumn, LedgerProfileContactNumber);
                    dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.GST_IdColumn, LedgerProfileGSTID);
                    dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.EMAILColumn, Email);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = false;
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Saving Country Details: " + ex.ToString();
            }
            finally { }

            return resultArgs;
        }



        /// <summary>
        /// Save the Country Details
        /// </summary>
        /// <returns></returns>
        private ResultArgs SaveCountryDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.InsertCountry, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(AppSchema.Country.COUNTRYColumn, CountryName);
                    dataManager.Parameters.Add(AppSchema.Country.COUNTRY_CODEColumn, CountryCode);
                    dataManager.Parameters.Add(AppSchema.Country.CURRENCY_CODEColumn, CurrencyCode);
                    dataManager.Parameters.Add(AppSchema.Country.CURRENCY_SYMBOLColumn, CurrencySymbol);
                    dataManager.Parameters.Add(AppSchema.Country.CURRENCY_NAMEColumn, CurrencyName);

                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Saving Country Details: " + ex.ToString();
            }
            finally { }

            return resultArgs;
        }

        private ResultArgs SaveStateDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.InsertState, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(AppSchema.State.STATE_CODEColumn, StateCode);
                    dataManager.Parameters.Add(AppSchema.State.STATE_NAMEColumn, StateName);
                    dataManager.Parameters.Add(AppSchema.State.COUNTRY_IDColumn, StateCountryId);
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Saving Country Details: " + ex.ToString();
            }
            finally { }

            return resultArgs;
        }

        /// <summary>
        /// Save Donor Details
        /// </summary>
        /// <returns></returns>
        private ResultArgs SaveDonorDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.InsertDonor, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.DonorAuditor.NAMEColumn, DonorName);
                    dataManager.Parameters.Add(this.AppSchema.DonorAuditor.PLACEColumn, Place);
                    dataManager.Parameters.Add(this.AppSchema.DonorAuditor.TYPEColumn, DonorType);
                    dataManager.Parameters.Add(this.AppSchema.DonorAuditor.COMPANY_NAMEColumn, CompanyName);
                    dataManager.Parameters.Add(this.AppSchema.DonorAuditor.COUNTRY_IDColumn, CountryId);
                    dataManager.Parameters.Add(this.AppSchema.DonorAuditor.PINCODEColumn, Pincode);
                    dataManager.Parameters.Add(this.AppSchema.DonorAuditor.PHONEColumn, Phone);
                    dataManager.Parameters.Add(this.AppSchema.DonorAuditor.FAXColumn, Fax);
                    dataManager.Parameters.Add(this.AppSchema.DonorAuditor.EMAILColumn, Email);
                    dataManager.Parameters.Add(this.AppSchema.DonorAuditor.URLColumn, URL);
                    dataManager.Parameters.Add(this.AppSchema.DonorAuditor.STATEColumn, DonorStateName);
                    dataManager.Parameters.Add(this.AppSchema.State.STATE_IDColumn, StateId);
                    dataManager.Parameters.Add(this.AppSchema.DonorAuditor.ADDRESSColumn, DonorAddress);
                    dataManager.Parameters.Add(this.AppSchema.DonorAuditor.PANColumn, PAN);
                    dataManager.Parameters.Add(this.AppSchema.DonorAuditor.IDENTITYKEYColumn, IdentityKey);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_IDColumn, BranchId);

                    resultArgs = dataManager.UpdateData();

                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        DonorId = GetId(Id.Donor);
                        if (DonorId > 0)
                        {
                            resultArgs = MapProject(SQLCommand.ImportVoucher.MapProjectDonor);//Mapping Project Donor
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Saving Donor Details: " + ex.ToString();
            }
            finally { }

            return resultArgs;
        }

        /// <summary>
        /// Save Cost Centre Details
        /// </summary>
        /// <returns></returns>
        private ResultArgs SaveCostCentreDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.InsertCostCentre, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_NAMEColumn, CostCentreName);
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Saving Cost Centre Details: " + ex.ToString();
            }

            return resultArgs;
        }

        /// <summary>
        /// Save Cost Category Details
        /// </summary>
        /// <returns></returns>
        private ResultArgs SaveCostCategoryDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.InsertCostCategory, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_CATEGORY_NAMEColumn, CostCategory);
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Saving Cost Category Details: " + ex.ToString();
            }

            //On 22/02/2018, attach name of the project and voucher detailis in the error log
            if (!resultArgs.Success && string.IsNullOrEmpty(ProblemDataDetails))
            {
                ProblemDataDetails = "Cost Category : " + CostCategory + "<br>" +
                                      "Problem in updating Master (Cost Category)";
            }

            return resultArgs;
        }

        /// <summary>
        /// Save Master Bank Details
        /// </summary>
        /// <returns></returns>
        private ResultArgs SaveBankDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.InsertBank, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.Bank.BANK_CODEColumn, BankCode);
                    dataManager.Parameters.Add(this.AppSchema.Bank.BANKColumn, BankName);
                    dataManager.Parameters.Add(this.AppSchema.Bank.BRANCHColumn, Branch);
                    dataManager.Parameters.Add(this.AppSchema.Bank.ADDRESSColumn, Address);
                    dataManager.Parameters.Add(this.AppSchema.Bank.IFSCCODEColumn, IFSCCode);
                    dataManager.Parameters.Add(this.AppSchema.Bank.MICRCODEColumn, MICRCode);
                    dataManager.Parameters.Add(this.AppSchema.Bank.CONTACTNUMBERColumn, ContactNumber);
                    dataManager.Parameters.Add(this.AppSchema.Bank.SWIFTCODEColumn, SWIFTCode);

                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Saving Bank Details: " + ex.ToString();
            }
            return resultArgs;
        }

        /// <summary>
        /// Save Bank  Account details
        /// </summary>
        /// <returns></returns>
        private ResultArgs SaveBankAccountDetails()   //0 - Add  >0 - Update
        {
            try
            {
                using (DataManager dataManager = new DataManager(BankAccountId == 0 ? SQLCommand.ImportVoucher.InsertBankAccount : SQLCommand.ImportVoucher.UpdateBankAccount, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.BankAccount.BANK_ACCOUNT_IDColumn, BankAccountId);
                    dataManager.Parameters.Add(this.AppSchema.BankAccount.ACCOUNT_CODEColumn, AccountCode);
                    dataManager.Parameters.Add(this.AppSchema.BankAccount.ACCOUNT_NUMBERColumn, AccountNumber);
                    dataManager.Parameters.Add(this.AppSchema.BankAccount.ACCOUNT_HOLDER_NAMEColumn, AccountHolderName);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                    dataManager.Parameters.Add(this.AppSchema.BankAccount.BANK_IDColumn, BankId);
                    dataManager.Parameters.Add(this.AppSchema.BankAccount.IS_FCRA_ACCOUNTColumn, Is_FCRA_Account);
                    dataManager.Parameters.Add(this.AppSchema.BankAccount.ACCOUNT_TYPE_IDColumn, AccountType);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_IDColumn, BranchId);
                    dataManager.Parameters.Add(this.AppSchema.BankAccount.DATE_OPENEDColumn, DateOpened);

                    if (DateClosed == DateTime.MinValue)
                    {
                        dataManager.Parameters.Add(this.AppSchema.BankAccount.DATE_CLOSEDColumn, null);
                    }
                    else
                    {
                        dataManager.Parameters.Add(this.AppSchema.BankAccount.DATE_CLOSEDColumn, DateClosed);
                    }

                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Saving Bank Account Details: " + ex.ToString();
            }
            return resultArgs;
        }


        /// <summary>
        /// Save Bank Account details in Ledger Table.
        /// </summary>
        /// <returns></returns>
        private ResultArgs SaveLedgerBankAccount()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.InsertLedgerBank, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_CODEColumn, LedgerCode);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, LedgerName);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.GROUP_IDColumn, GroupId);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_BRANCH_LEDGERColumn, IsBranchLedger);

                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Saving Ledger Bank Account: " + ex.ToString();
            }
            return resultArgs;
        }

        /// <summary>
        /// 17/02/2020, Save the Sub Ledger Details
        /// </summary>
        /// <returns></returns>
        private ResultArgs SaveSubLedgerDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.InsertSubLedger, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.SubLedger.SUB_LEDGER_IDColumn, SubLedgerId, true);
                    dataManager.Parameters.Add(this.AppSchema.SubLedger.SUB_LEDGER_NAMEColumn, SubLedgerName);
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Saving Sub Ledger Details: " + ex.ToString();
            }
            return resultArgs;
        }
        #endregion

        #region Voucher Transaction


        /// <summary>
        /// On 08/02/2024, For SDBINM, they wanted to have same head office SDBIM database but they need to split Local community database
        /// Existing portal db logic, voucher id should be unique for branch (Later in portal, It should be changed like to have VOUCHER_ID and REF_VOUCHER_ID)
        /// To continue voucherid from source branch Database (auto voucherid), we update target database voucher id auto increment number from source branch
        /// 
        /// If and If only Target database has no vouchers
        /// </summary>
        /// <returns></returns>
        private ResultArgs SetSourceBranchVoucherAutoincrement()
        {
            try
            {
                //Int32 soucebranchlastvoucherid = 0;
                //VoucherAutoNumberUpdated = false;
                IsTargetDBNew = false;
                if (IS_SDB_INM && IsFYSplit)
                {
                    resultArgs = IsExists(SQLCommand.ImportVoucher.IsVoucherExists);
                    if (resultArgs.Success)
                    {
                        IsTargetDBNew = resultArgs.DataSource.Sclar.ToInteger == 0;

                        if (IsTargetDBNew)
                        {
                            /*if (dtHeader.Columns.Contains("LAST_VOUCHER_ID"))
                            {
                                soucebranchlastvoucherid = NumberSet.ToInteger(dtHeader.Rows[0]["LAST_VOUCHER_ID"].ToString());
                            }
                            soucebranchlastvoucherid = soucebranchlastvoucherid + 1;
                            resultArgs = AltertVoucherTableWithNewAutoNumber(soucebranchlastvoucherid);
                            if (resultArgs.Success)
                            {
                                VoucherAutoNumberUpdated = true;
                            }*/
                        }
                    }
                }
                else
                {
                    resultArgs.Success = true;
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in SetVoucherAutoincrement: " + ex.ToString();
            }
            finally { }

            return resultArgs;
        }

        private ResultArgs AltertVoucherTableWithNewAutoNumber(Int32 newAutoNubmer)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.UpdateAutoIncrementNumber, DataBaseTypeName, SQLAdapterType.HOSQL))
            {
                dataManager.Parameters.Add("LAST_VOUCHER_ID", newAutoNubmer, DataType.Int32);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// Save Voucher Master transaction, Voucher transaction & Voucher Cost Centre.
        /// </summary>
        /// <returns></returns>
        private ResultArgs ImportVoucherDetails()
        {
            if (SynchronizeHeld != null)
            {
                Caption = "Importing Vouchers Progress..";
                SynchronizeHeld(this, new EventArgs());
            }

            AcMEDataSynLog.WriteLog("ImportVoucherDetails Started..");
            try
            {
                if (dtVoucherMaster != null && dtVoucherMaster.Rows.Count > 0)
                {
                    foreach (DataRow drMaster in dtVoucherMaster.Rows)
                    {
                        VoucherId = this.NumberSet.ToInteger(drMaster[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString());
                        VoucherNo = drMaster[this.AppSchema.VoucherMaster.VOUCHER_NOColumn.ColumnName].ToString();
                        VoucherType = drMaster[this.AppSchema.VoucherMaster.VOUCHER_TYPEColumn.ColumnName].ToString();
                        VoucherSubType = drMaster[this.AppSchema.VoucherMaster.VOUCHER_SUB_TYPEColumn.ColumnName].ToString();
                        ContributionType = drMaster[this.AppSchema.VoucherMaster.CONTRIBUTION_TYPEColumn.ColumnName].ToString();
                        ContributionAmount = this.NumberSet.ToDecimal(drMaster[this.AppSchema.VoucherMaster.CONTRIBUTION_AMOUNTColumn.ColumnName].ToString());
                        ExchangeRate = this.NumberSet.ToDecimal(drMaster[this.AppSchema.VoucherMaster.EXCHANGE_RATEColumn.ColumnName].ToString());
                        CalculatedAmount = this.NumberSet.ToDecimal(drMaster[this.AppSchema.VoucherMaster.CALCULATED_AMOUNTColumn.ColumnName].ToString());
                        ActualAmount = this.NumberSet.ToDecimal(drMaster[this.AppSchema.VoucherMaster.ACTUAL_AMOUNTColumn.ColumnName].ToString());
                        ClientCode = drMaster[this.AppSchema.VoucherMaster.CLIENT_CODEColumn.ColumnName].ToString();
                        ClientRefId = this.NumberSet.ToInteger(drMaster[this.AppSchema.VoucherMaster.CLIENT_REFERENCE_IDColumn.ColumnName].ToString());
                        Narration = drMaster[this.AppSchema.VoucherMaster.NARRATIONColumn.ColumnName].ToString();
                        NameAddress = drMaster[this.AppSchema.VoucherMaster.NAME_ADDRESSColumn.ColumnName].ToString();
                        VoucherDate = this.DateSet.ToDate(drMaster[this.AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName].ToString(), false);
                        ProjectName = drMaster[this.AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                        ProjectId = MergerProjectId > 0 ? MergerProjectId : GetId(Id.Project); //Modified by Carmel Raj M on July-08-2015
                        DonorName = drMaster[this.AppSchema.DonorAuditor.NAMEColumn.ColumnName].ToString();
                        DonorId = GetId(Id.Donor);
                        PurposeName = drMaster[this.AppSchema.Purposes.FC_PURPOSEColumn.ColumnName].ToString();
                        PurposeId = GetId(Id.Purpose);
                        CountryName = drMaster[this.AppSchema.Country.CURRENCY_COUNTRYColumn.ColumnName].ToString();
                        CurrencyCountryId = GetId(Id.Country);
                        CountryName = drMaster[this.AppSchema.Country.EXCHANGE_COUNTRYColumn.ColumnName].ToString();
                        ExchageCountryId = GetId(Id.Country);
                        CreatedOn = this.DateSet.ToDate(drMaster[this.AppSchema.VoucherMaster.CREATED_ONColumn.ColumnName].ToString(), true);
                        CreatedBy = NumberSet.ToInteger(drMaster[this.AppSchema.VoucherMaster.CREATED_BYColumn.ColumnName].ToString());
                        CreatedByName = drMaster[this.AppSchema.VoucherMaster.CREATED_BY_NAMEColumn.ColumnName].ToString();
                        ModifiedOn = this.DateSet.ToDate(drMaster[this.AppSchema.VoucherMaster.MODIFIED_ONColumn.ColumnName].ToString(), true);
                        ModifiedBy = NumberSet.ToInteger(drMaster[this.AppSchema.VoucherMaster.CREATED_BYColumn.ColumnName].ToString());
                        ModifiedByName = drMaster[this.AppSchema.VoucherMaster.MODIFIED_BYColumn.ColumnName].ToString();

                        IsCashBankstatus = this.NumberSet.ToInteger(drMaster[this.AppSchema.VoucherMaster.IS_CASH_BANK_STATUSColumn.ColumnName].ToString());


                        //On 13/02/2019, for multi voucher type defintinid
                        VoucherDefinitionId = (Int32)DefaultVoucherTypes.Receipt;
                        if (VoucherType == "RC") VoucherDefinitionId = (Int32)DefaultVoucherTypes.Receipt;
                        else if (VoucherType == "PY") VoucherDefinitionId = (Int32)DefaultVoucherTypes.Payment;
                        else if (VoucherType == "CN") VoucherDefinitionId = (Int32)DefaultVoucherTypes.Contra;
                        else if (VoucherType == "JN") VoucherDefinitionId = (Int32)DefaultVoucherTypes.Journal;

                        //On 09/07/2021, to update vendor invoice details-----------------
                        GSTVendorId = null;
                        GSTVendorInvoiceNo = string.Empty;
                        GSTVendorInvoiceType = 0;
                        GSTVendorInvoiceDate = null;
                        Vendor = drMaster[this.AppSchema.Vendors.VENDORColumn.ColumnName].ToString();
                        int gstVendorId = NumberSet.ToInteger(drMaster[this.AppSchema.VoucherMaster.GST_VENDOR_IDColumn.ColumnName].ToString());
                        if (!string.IsNullOrEmpty(Vendor) && gstVendorId > 0)
                        {
                            GSTVendorId = GetId(Id.Vendor);
                            GSTVendorInvoiceNo = drMaster[this.AppSchema.VoucherMaster.GST_VENDOR_INVOICE_NOColumn.ColumnName].ToString();
                            GSTVendorInvoiceType = NumberSet.ToInteger(drMaster[this.AppSchema.VoucherMaster.GST_VENDOR_INVOICE_TYPEColumn.ColumnName].ToString());
                            if (drMaster[this.AppSchema.VoucherMaster.GST_VENDOR_INVOICE_TYPEColumn.ColumnName] != null)
                            {
                                GSTVendorInvoiceDate = DateSet.ToDate(drMaster[this.AppSchema.VoucherMaster.GST_VENDOR_INVOICE_DATEColumn.ColumnName].ToString(), false);
                            }
                        }
                        //---------------------------------------------------------------------


                        // Voucher Master Details Validation.
                        if (ProjectId == 0)
                        {
                            //Modified by Carmel Raj M on July-12-2015
                            //Purpose : Message differentiation between Split Project and data Synchronization
                            resultArgs.Message = VoucherImportType.Equals(ImportType.SplitProject) ? String.Format("Project is not available while updating Voucher Master Details.The Required Project is {0}", ProjectName) :
                                "Project is not available in Head Office while updating Voucher Master Details. <br/> The Required Project in Portal is: <br/><b>" + ProjectName + "</b>";
                        }
                        else if (!string.IsNullOrEmpty(DonorName) && DonorId == 0)
                        {
                            //Modified by Carmel Raj M on July-12-2015
                            //Purpose : Message differentiation between Split Project and data Synchronization
                            resultArgs.Message = VoucherImportType.Equals(ImportType.SplitProject) ? String.Format("Donor Name is not available while updating Voucher Master Details.The Required Donor is {0}", DonorName) :
                                "Donor Name is not available in Head Office while updating Voucher Master Details.<br/> The Required Donor in Portal is: <br/><b>" + DonorName + "</b>";
                        }
                        else if (DonorId > 0 && PurposeId == 0)
                        {
                            resultArgs.Message = "Purpose is Empty but Donor Receipt entry is made. <br/> Project Name : "
                                + ProjectName + " <br/> Voucher Date :" + VoucherDate + "<br/> Voucher No : " + VoucherNo;
                        }
                        //else if (DonorId > 0 && CurrencyCountryId == 0)
                        //{
                        //    resultArgs.Message = "Currency Country is not available but Donor Receipt entry is made" + CountryName;
                        //}
                        else
                        {
                            if (DonorId > 0)
                            {
                                resultArgs = MapProject(SQLCommand.ImportVoucher.MapProjectDonor);//Mapping Project Donor
                            }

                            if (resultArgs.Success)
                            {
                                resultArgs = SaveVoucherMasters();  //Inserting Voucher Master Details.
                                if (resultArgs.Success)
                                {
                                    LastVoucherId = (VoucherImportType == ImportType.HeadOffice) ? VoucherId : this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                    resultArgs = ImportVoucherTrans();  //Inserting Voucher Transaction Details.
                                    if (resultArgs.Success)
                                    {
                                        resultArgs = ImportVoucherCostCentre();  //Inserting Voucher Cost Centre Details.
                                        if (resultArgs.Success)
                                        {
                                            //On 05/02/2024, To Import GST Invoice Details
                                            if (VoucherImportType == ImportType.SplitProject)
                                            {
                                                resultArgs = SaveGSTInvoiceMaster();
                                            }
                                            if (resultArgs.Success)
                                            {
                                                using (BalanceSystem balance = new BalanceSystem())
                                                {
                                                    resultArgs = balance.UpdateTransBalance(BranchId, LocationId, LastVoucherId, TransactionAction.New);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (!resultArgs.Success)
                        {
                            AcMEDataSynLog.WriteLog("Voucher Id: " + VoucherId + ", BranchId: " + BranchId + ", Voucher Date :" + VoucherDate.ToShortDateString() + ", Project Id :" + ProjectId);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ImportVoucherDetails Ended.");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Error in Importing Voucher Details. " + resultArgs.Message);
            }
            return resultArgs;
        }

        /// <summary>
        /// This method filters the Concern Voucher Transaction and Inserts in the Voucher Transaction.
        /// </summary>
        /// <returns></returns>
        private ResultArgs ImportVoucherTrans()
        {
            try
            {
                if (VoucherId > 0)
                {
                    if (dtVoucherTrans != null && dtVoucherTrans.Rows.Count > 0)
                    {
                        DataView dvTrans = dtVoucherTrans.DefaultView;
                        dvTrans.RowFilter = "VOUCHER_ID=" + VoucherId;
                        if (dvTrans.Count > 1)
                        {
                            foreach (DataRowView dr in dvTrans)
                            {
                                SequenceNo = this.NumberSet.ToInteger(dr[this.AppSchema.VoucherTransaction.SEQUENCE_NOColumn.ColumnName].ToString());
                                LedgerName = dr[this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                                LedgerId = GetId(Id.Ledger);
                                Amount = this.NumberSet.ToDecimal(dr[this.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString());
                                TransMode = dr[this.AppSchema.VoucherTransaction.TRANS_MODEColumn.ColumnName].ToString();
                                ChequeNo = dr[this.AppSchema.VoucherTransaction.CHEQUE_NOColumn.ColumnName].ToString();
                                MaterializedOn = dr[this.AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName].ToString() != null ?
                                    dr[this.AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName].ToString() : string.Empty;

                                //On 22/11/2017, to get Cheque reference details (Cheque date, cheque bank name, cheque branch) 

                                ChequeRefDate = dr[this.AppSchema.VoucherTransaction.CHEQUE_REF_DATEColumn.ColumnName].ToString() != null ?
                                    dr[this.AppSchema.VoucherTransaction.CHEQUE_REF_DATEColumn.ColumnName].ToString() : string.Empty;

                                ChequeRefBankName = dr[this.AppSchema.VoucherTransaction.CHEQUE_REF_BANKNAMEColumn.ColumnName].ToString() != null ?
                                    dr[this.AppSchema.VoucherTransaction.CHEQUE_REF_BANKNAMEColumn.ColumnName].ToString() : string.Empty;

                                ChequeRefBankBranch = dr[this.AppSchema.VoucherTransaction.CHEQUE_REF_BRANCHColumn.ColumnName].ToString() != null ?
                                    dr[this.AppSchema.VoucherTransaction.CHEQUE_REF_BRANCHColumn.ColumnName].ToString() : string.Empty;

                                //31/01/2024
                                ChequeRefFundTransferTypeName = dr[this.AppSchema.VoucherTransaction.FUND_TRANSFER_TYPE_NAMEColumn.ColumnName].ToString() != null ?
                                    dr[this.AppSchema.VoucherTransaction.FUND_TRANSFER_TYPE_NAMEColumn.ColumnName].ToString() : string.Empty;

                                LedgerNarration = dr[this.AppSchema.VoucherMaster.NARRATIONColumn.ColumnName].ToString() != null ?
                                    dr[this.AppSchema.VoucherMaster.NARRATIONColumn.ColumnName].ToString() : string.Empty;

                                //On 09/07/2021, to include GST details and Vendor details
                                Slab = dr[this.AppSchema.VoucherTransaction.SLABColumn.ColumnName].ToString();
                                int gstClassId = NumberSet.ToInteger(dr[this.AppSchema.VoucherTransaction.LEDGER_GST_CLASS_IDColumn.ColumnName].ToString());
                                LedgerGSTClassId = 0;
                                GST = CGST = SGST = IGST = 0;
                                if (!string.IsNullOrEmpty(Slab) && gstClassId > 0)
                                {
                                    LedgerGSTClassId = GetId(Id.GSTClass);
                                    if (LedgerGSTClassId > 0)
                                    {
                                        GST = this.NumberSet.ToDouble(dr[this.AppSchema.VoucherTransaction.GSTColumn.ColumnName].ToString());
                                        CGST = this.NumberSet.ToDouble(dr[this.AppSchema.VoucherTransaction.CGSTColumn.ColumnName].ToString());
                                        SGST = this.NumberSet.ToDouble(dr[this.AppSchema.VoucherTransaction.SGSTColumn.ColumnName].ToString());
                                        IGST = this.NumberSet.ToDouble(dr[this.AppSchema.VoucherTransaction.IGSTColumn.ColumnName].ToString());
                                    }
                                }

                                //--------------------------------------------------------------------------------------------------------------

                                //Voucher Transaction Validation
                                if (LedgerId == 0)
                                {
                                    //Modified by Carmel Raj M on July-12-2015
                                    //Purpose : Message differentiation between Split Project and data Synchronization
                                    resultArgs.Message = VoucherImportType.Equals(ImportType.SplitProject) ? String.Format("Ledger Name is Empty while updating Voucher Transaction Details.The Required Ledger is {0}", LedgerName) :
                                        "Ledger Name is Empty in Head Office while updating Voucher Transaction Details.<br/> The Required Ledger in Portal is:<br/><b>" + LedgerName + "</b>";
                                }
                                else
                                {
                                    resultArgs = MapProject(SQLCommand.ImportVoucher.MapProjectLedger);  //Mapping Project Ledger
                                    if (resultArgs.Success)
                                    {
                                        resultArgs = SaveVoucherTrans();  //Inserting Voucher Transaction Detaiils
                                    }
                                }

                                if (!resultArgs.Success)
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            AcMEDataSynLog.WriteLog("Transaction count is 1:" + VoucherId);
                            resultArgs.Message = "The transaction is not valid. Only one Ledger transaction is available. \n Cash/Bank transaction is not available : " + VoucherId;
                        }
                        dvTrans.RowFilter = "";
                    }
                }
                else
                {
                    AcMEDataSynLog.WriteLog("VOUCHER_ID in Voucher Trans is 0 : " + VoucherId);
                }
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog("Error in ImportVoucherTrans: " + ex.ToString());
                resultArgs.Message = ex.Message;
            }
            finally { }

            //On 22/02/2018, attach name of the project and voucher detailis in the error log
            if (!resultArgs.Success && string.IsNullOrEmpty(ProblemDataDetails))
            {
                ProblemDataDetails = "Project Name  : " + ProjectName + "<br>" +
                                      "Voucher Date : " + VoucherDate + "<br>" +
                                      "Voucher Type : " + VoucherType + "<br>" +
                                      "Voucher No   : " + VoucherNo + "<br>" +
                                      "Problem in updating Voucher Ledger Transaction";
            }
            return resultArgs;
        }



        /// <summary>
        /// On 10/04/2021
        /// This method used to map project ledger
        /// for FY split enabled, export project ledgers too as when project imports maps transactions ledgers alone.
        /// </summary>
        /// <returns></returns>
        private ResultArgs ImportMAPProjectLedgers()
        {
            try
            {
                if (dtProjectLedgers != null && dtProjectLedgers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtProjectLedgers.Rows)
                    {
                        /*ProjectName = dr[this.AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                        ProjectId = GetId(Id.Project);*/

                        LedgerName = dr[this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                        LedgerId = GetId(Id.Ledger);

                        //Voucher Transaction Validation
                        if (ProjectId == 0 || LedgerId == 0)
                        {
                            resultArgs.Message = VoucherImportType.Equals(ImportType.SplitProject) ? String.Format("Ledger Name/Project Name is Empty while mapping Project Ledger.The Required Ledger is {0}", LedgerName) :
                                "Ledger Name is Empty in Head Office while mapping Project Ledger.<br/> The Required Ledger in Portal is:<br/><b>" + LedgerName + "</b>";
                        }
                        else
                        {
                            resultArgs = MapProject(SQLCommand.ImportVoucher.MapProjectLedger);  //Mapping Project Ledger
                        }

                        if (!resultArgs.Success)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog("Error in ImportMAPProjectLedgers: " + ex.ToString());
                resultArgs.Message = ex.Message;
            }
            finally { }

            if (!resultArgs.Success && string.IsNullOrEmpty(ProblemDataDetails))
            {
                ProblemDataDetails = "Project Name  : " + ProjectName + "<br>" +
                                      "Ledger name : " + LedgerName + "<br>" +
                                      "Problem in Mapping Project Ledger";
            }
            return resultArgs;
        }

        /// <summary>
        /// This Method filter the Concern Voucher Cost Centers and inserts the Cost Centre Details.
        /// </summary>
        /// <returns></returns>
        private ResultArgs ImportVoucherCostCentre()
        {
            try
            {
                CostCentreSequenceNo = 0;
                if (VoucherId > 0)
                {
                    if (dtVoucherCostCentre != null && dtVoucherCostCentre.Rows.Count > 0)
                    {
                        DataView dvCostCentre = dtVoucherCostCentre.DefaultView;
                        dvCostCentre.RowFilter = "VOUCHER_ID=" + VoucherId;
                        if (dvCostCentre.Count > 0)
                        {
                            foreach (DataRowView dr in dvCostCentre)
                            {
                                CostCentreName = dr[this.AppSchema.CostCentre.COST_CENTRE_NAMEColumn.ColumnName].ToString();
                                CostCenterId = GetId(Id.CostCentre);
                                LedgerName = dr[this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                                LedgerId = GetId(Id.Ledger);
                                CostCentreAmount = this.NumberSet.ToDecimal(dr[this.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString());
                                CostCentreSequenceNo = CostCentreSequenceNo + 1;  //this.NumberSet.ToInteger(dr[this.AppSchema.VoucherTransaction.SEQUENCE_NOColumn.ColumnName].ToString());
                                LedgerCostCentreSequenceNo = this.NumberSet.ToInteger(dr[this.AppSchema.VoucherCostCentre.LEDGER_SEQUENCE_NOColumn.ColumnName].ToString());
                                // Check the ledgerid is not cash/bank/fd ledger
                                resultArgs = IsGeneralLedger(LedgerId);
                                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                                {
                                    //Voucher Cost Centre Validation
                                    if (CostCenterId == 0)
                                    {
                                        AcMEDataSynLog.WriteLog("Cost Centre Name is not available while updating cost Centre Details.<br> The required CostCentreName in Portal is:<br/><b> " + CostCentreName + "</b>");
                                        continue;
                                    }
                                    else if (LedgerId == 0)
                                    {
                                        resultArgs.Message = "Ledger Name is not available in Head Office while updating Cost Centre Details.<br/> The required LedgerName in Portal is: <br/><b>" + LedgerName + "</b>";
                                    }
                                    else
                                    {
                                        resultArgs = MapProject(SQLCommand.ImportVoucher.MapProjectCostcentre); //Mapping Project Cost Centre
                                        if (resultArgs.Success)
                                        {
                                            resultArgs = SaveVoucherCostCentre();   //Inserting Voucher Cost Centre Details
                                        }
                                    }

                                    if (!resultArgs.Success)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        dvCostCentre.RowFilter = "";
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally
            {
                if (!resultArgs.Success)
                {
                    AcMEDataSynLog.WriteLog("Problem in Importing Voucher cost Center Detail." + resultArgs.Message);
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// 17/02/2020, This Method filter the Concern Voucher SubLedger Vouchers and inserts Sub Ledger Vouchers Details.
        /// </summary>
        /// <returns></returns>
        public ResultArgs ImportVoucherSubLedgers(DataTable dtTmpSubLedgersVouchers, Int32 branchid, Int32 locationid)
        {
            try
            {
                BranchId = branchid;
                LocationId = locationid;

                dtVoucherSubLedger = dtTmpSubLedgersVouchers;
                VoucherSubLedgerSequenceNo = 0;
                //if (VoucherId > 0)
                //{
                if (dtVoucherSubLedger != null && dtVoucherSubLedger.Rows.Count > 0)
                {
                    DataTable dtVoucherIds = dtVoucherSubLedger.DefaultView.ToTable(true, "VOUCHER_ID");
                    foreach (DataRow drVoucherRow in dtVoucherIds.Rows)
                    {
                        LastVoucherId = NumberSet.ToInteger(drVoucherRow["VOUCHER_ID"].ToString());
                        //Clear Existing Sub Ledger Vouchers for vcoucher id
                        resultArgs = DeleteVoucherSubLedgers(LastVoucherId);
                        if (resultArgs.Success)
                        {
                            dtVoucherSubLedger.DefaultView.RowFilter = "VOUCHER_ID=" + LastVoucherId;
                            if (dtVoucherSubLedger.DefaultView.Count > 0)
                            {
                                foreach (DataRowView dr in dtVoucherSubLedger.DefaultView)
                                {
                                    ProjectName = dr[this.AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                                    ProjectId = GetId(Id.Project);
                                    SubLedgerName = dr[this.AppSchema.VoucherSubLedger.SUB_LEDGER_NAMEColumn.ColumnName].ToString();
                                    SubLedgerId = GetId(Id.SubLedger);
                                    LedgerName = dr[this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                                    LedgerId = GetId(Id.Ledger);
                                    VoucherSubLedgerAmount = this.NumberSet.ToDecimal(dr[this.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString());
                                    VoucherSubLedgerTransMode = dr[this.AppSchema.VoucherTransaction.TRANS_MODEColumn.ColumnName].ToString();
                                    VoucherSubLedgerSequenceNo = VoucherSubLedgerSequenceNo + 1;

                                    // Check the ledgerid is not cash/bank/fd ledger
                                    resultArgs = IsGeneralLedger(LedgerId);
                                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                                    {
                                        //Voucher Sub Ledger Validation
                                        if (SubLedgerId == 0)
                                        {
                                            resultArgs.Message = "Sub Ledger Name is not available in Head Office while updating Voucher Sub Ledger Details.<br/> The required Sub LedgerName in Portal is: <br/><b>" + SubLedgerName + "</b>";
                                        }
                                        else if (LedgerId == 0)
                                        {
                                            resultArgs.Message = "Ledger Name is not available in Head Office while updating Cost Centre Details.<br/> The required LedgerName in Portal is: <br/><b>" + LedgerName + "</b>";
                                        }
                                        else
                                        {
                                            resultArgs = SaveVoucherSubLedger();
                                        }

                                        //Clear and Map SubLeger with Ledger
                                        if (resultArgs.Success && ProjectId > 0 && LedgerId > 0)
                                        {
                                            resultArgs = MapProject(SQLCommand.ImportVoucher.MapProjectBudgetLedger);
                                        }
                                        else if (!resultArgs.Success)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            dtVoucherSubLedger.DefaultView.RowFilter = "";
                        }
                        else
                        {
                            if (!resultArgs.Success)
                            {
                                break;
                            }
                        }
                    }
                }
                //}
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally
            {
                if (!resultArgs.Success)
                {
                    AcMEDataSynLog.WriteLog("Problem in Importing Voucher cost Center Detail." + resultArgs.Message);
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// Save Voucher Master Transaction Details
        /// </summary>
        /// <returns></returns>
        private ResultArgs SaveVoucherMasters()
        {
            try
            {
                //On 19/08/2021, To fix User details by dfault ---------------------------------------------
                if (VoucherImportType != ImportType.HeadOffice)
                {
                    CreatedBy = this.NumberSet.ToInteger(this.LoginUserId.ToString());
                    ModifiedBy = this.NumberSet.ToInteger(this.LoginUserId.ToString());

                    CreatedByName = FirstName;  //LoginUserName.ToString();
                    ModifiedByName = FirstName; //LoginUserName.ToString();
                }
                //------------------------------------------------------------------------------------------

                using (DataManager dataManager = new DataManager((VoucherImportType == ImportType.HeadOffice) ? SQLCommand.ImportVoucher.InsertVoucherMaster : SQLCommand.ImportVoucher.InsertVoucherMasterBranch, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId, true);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_DATEColumn, VoucherDate);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_NOColumn, VoucherNo);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_TYPEColumn, VoucherType);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_SUB_TYPEColumn, VoucherSubType);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.DONOR_IDColumn, DonorId);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PURPOSE_IDColumn, PurposeId);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CONTRIBUTION_TYPEColumn, (string.IsNullOrEmpty(ContributionType) ? "N" : ContributionType));
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CONTRIBUTION_AMOUNTColumn, ContributionAmount);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CURRENCY_COUNTRY_IDColumn, CurrencyCountryId);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.EXCHANGE_RATEColumn, ExchangeRate);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CALCULATED_AMOUNTColumn, CalculatedAmount);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.ACTUAL_AMOUNTColumn, ActualAmount);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CLIENT_CODEColumn, ClientCode);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CLIENT_REFERENCE_IDColumn, ClientRefId);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.EXCHANGE_COUNTRY_IDColumn, ExchageCountryId);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.NARRATIONColumn, Narration);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.NAME_ADDRESSColumn, NameAddress);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_IDColumn, BranchId);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.LOCATION_IDColumn, LocationId);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CREATED_ONColumn, CreatedOn);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CREATED_BYColumn, CreatedBy);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CREATED_BY_NAMEColumn, CreatedByName);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.MODIFIED_ONColumn, ModifiedOn);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.MODIFIED_BYColumn, ModifiedBy);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.MODIFIED_BY_NAMEColumn, ModifiedByName);
                    //On 09/07/2021, to update vendor invoice details-----------------------
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.GST_VENDOR_IDColumn, GSTVendorId);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.GST_VENDOR_INVOICE_NOColumn, GSTVendorInvoiceNo);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.GST_VENDOR_INVOICE_TYPEColumn, GSTVendorInvoiceType);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.GST_VENDOR_INVOICE_DATEColumn, GSTVendorInvoiceDate);
                    //----------------------------------------------------------------------

                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.IS_CASH_BANK_STATUSColumn, IsCashBankstatus);

                    dataManager.Parameters.Add(this.AppSchema.VoucherNumber.VOUCHER_DEFINITION_IDColumn, VoucherDefinitionId);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = false;
                    resultArgs = dataManager.UpdateData();
                }
                if (!resultArgs.Success)
                {
                    resultArgs.Message = "Problem in updating Voucher Master details, " + resultArgs.Message;
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Problem in updating Voucher Master details, " + ex.ToString();
            }
            finally { }

            //On 22/02/2018, attach name of the project and voucher detailis in the error log
            if (!resultArgs.Success && string.IsNullOrEmpty(ProblemDataDetails))
            {
                ProblemDataDetails = "Project Name  : " + ProjectName + "<br>" +
                                      "Voucher Date : " + VoucherDate + "<br>" +
                                      "Voucher Tyoe : " + VoucherType + "<br>" +
                                      "Voucher No   : " + VoucherNo + "<br>" +
                                      "Problem in updating Voucher Master details";
            }

            return resultArgs;
        }

        /// <summary>
        /// Save Voucher Transaction Details
        /// </summary>
        /// <returns></returns>
        private ResultArgs SaveVoucherTrans()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.InsertVoucherTrans, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, LastVoucherId);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.SEQUENCE_NOColumn, SequenceNo);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.LEDGER_IDColumn, LedgerId);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.AMOUNTColumn, Amount);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.TRANS_MODEColumn, TransMode);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.CHEQUE_NOColumn, ChequeNo);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.MATERIALIZED_ONColumn, MaterializedOn);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.CHEQUE_REF_DATEColumn, ChequeRefDate);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.CHEQUE_REF_BANKNAMEColumn, ChequeRefBankName);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.CHEQUE_REF_BRANCHColumn, ChequeRefBankBranch);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.FUND_TRANSFER_TYPE_NAMEColumn, ChequeRefFundTransferTypeName);

                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.NARRATIONColumn, LedgerNarration);

                    //On 09/07/2021, to include GST details
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.GSTColumn, GST);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.SGSTColumn, SGST);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.CGSTColumn, CGST);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.IGSTColumn, IGST);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.LEDGER_GST_CLASS_IDColumn, (LedgerGSTClassId == 0 ? null : LedgerGSTClassId));

                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_IDColumn, BranchId);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.LOCATION_IDColumn, LocationId);

                    //dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                    resultArgs = dataManager.UpdateData();

                    if (!resultArgs.Success)
                    {
                        AcMEDataSynLog.WriteLog("Error in Saving Voucher Transactions. " + resultArgs.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Saving Voucher Transaction: " + ex.ToString();
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// Save Voucher cost Centre Detials.
        /// </summary>
        /// <returns></returns>
        private ResultArgs SaveVoucherCostCentre()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.InsertVoucherCostCentre, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    string CostCentreTable = "0LDR" + LedgerId;
                    dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.VOUCHER_IDColumn, LastVoucherId);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.SEQUENCE_NOColumn, CostCentreSequenceNo);
                    dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.LEDGER_IDColumn, LedgerId);
                    dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.COST_CENTRE_IDColumn, CostCenterId);
                    dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.AMOUNTColumn, CostCentreAmount);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_IDColumn, BranchId);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.LOCATION_IDColumn, LocationId);
                    //Added by Carmel Raj M on August-18-2015

                    dataManager.Parameters.Add(AppSchema.VoucherCostCentre.COST_CENTRE_TABLEColumn, CostCentreTable);
                    dataManager.Parameters.Add(AppSchema.VoucherCostCentre.LEDGER_SEQUENCE_NOColumn, LedgerCostCentreSequenceNo);

                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Saving Voucher Cost Centre: " + ex.ToString();
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// 17/02/2020, Save Voucher Sub Ledger Detials.
        /// </summary>
        /// <returns></returns>
        private ResultArgs SaveVoucherSubLedger()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.InsertVoucherSubLedger, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.VOUCHER_IDColumn, LastVoucherId);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.SEQUENCE_NOColumn, VoucherSubLedgerSequenceNo);
                    dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.LEDGER_IDColumn, LedgerId);
                    dataManager.Parameters.Add(this.AppSchema.VoucherSubLedger.SUB_LEDGER_IDColumn, SubLedgerId);
                    dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.AMOUNTColumn, VoucherSubLedgerAmount);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.TRANS_MODEColumn, VoucherSubLedgerTransMode);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_IDColumn, BranchId);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.LOCATION_IDColumn, LocationId);

                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Saving Voucher Cost Centre: " + ex.ToString();
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// This method is to get the Project Id's in the XML that is passed.
        /// These Id's will be used to fetch transaction of those projects and used for deletion.
        /// </summary>
        /// <returns></returns>
        private ResultArgs GetProjectIds()
        {
            AcMEDataSynLog.WriteLog("GetProjectIds Started.");
            try
            {
                string ProjectIds = string.Empty;
                if (dtProject != null && dtProject.Rows.Count > 0)
                {
                    foreach (DataRow drProject in dtProject.Rows)
                    {
                        ProjectName = drProject[this.AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                        ProjectId = this.GetId(Id.Project);

                        if (ProjectId > 0)
                        {
                            ProjectIds += ProjectId.ToString();
                            ProjectIds += ',';
                        }
                        //Modified by Carmel Raj M on September-10-2015
                        //Purpose:For Split Project empty project validation is not need
                        else if (VoucherImportType != ImportType.SplitProject)
                        {
                            resultArgs.Message = "Problem in Fetching Project Id from Head Office. Project Id is 0";
                        }
                    }

                    if (!string.IsNullOrEmpty(ProjectIds))
                    {
                        resultArgs.ReturnValue = ProjectIds.TrimEnd(',');
                    }
                    //Modified by Carmel Raj M on September-10-2015
                    //Purpose:For Split Project empty project validation is not need
                    else if (VoucherImportType != ImportType.SplitProject)
                    {
                        resultArgs.Message = "No Projects found in Branch Master XML";
                    }
                }
                //else
                //{
                //    resultArgs.Message = "No Projects found in Branch Voucher XML";
                //}
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("GetProjectIds Ended.");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Problem in Getting Project ID:" + resultArgs.Message);
            }
            return resultArgs;
        }

        public bool ValidateBudgetProjectsWtihTargetDB()
        {
            bool Rnt = false;



            return Rnt;
        }

        private ResultArgs GetBudgetProjectIds()
        {
            AcMEDataSynLog.WriteLog("GetBudgetProjectIds Started.");
            try
            {
                string ProjectIds = string.Empty;
                if (dtBudgetProject != null && dtBudgetProject.Rows.Count > 0)
                {
                    foreach (DataRow drProject in dtBudgetProject.Rows)
                    {
                        ProjectName = drProject[this.AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                        ProjectId = this.GetId(Id.Project);

                        if (ProjectId > 0)
                        {
                            ProjectIds += ProjectId.ToString();
                            ProjectIds += ',';
                        }
                        //Modified by Carmel Raj M on September-10-2015
                        //Purpose:For Split Project empty project validation is not need
                        else if (VoucherImportType != ImportType.SplitProject)
                        {
                            resultArgs.Message = "Problem in Fetching Project Id from Head Office. Project Id is 0";
                        }
                    }

                    if (!string.IsNullOrEmpty(ProjectIds))
                    {
                        resultArgs.ReturnValue = ProjectIds.TrimEnd(',');
                    }
                    //Modified by Carmel Raj M on September-10-2015
                    //Purpose:For Split Project empty project validation is not need
                    else if (VoucherImportType != ImportType.SplitProject)
                    {
                        resultArgs.Message = "No Projects found in Branch Master XML";
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("GetProjectIds Ended.");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Problem in Getting Project ID:" + resultArgs.Message);
            }
            return resultArgs;
        }

        /// <summary>
        /// This Method fetches the Transactions between the date range based on the Projects and Branch.
        /// </summary>
        /// <returns></returns>
        private ResultArgs DeleteVouchers()
        {
            if (SynchronizeHeld != null)
            {
                Caption = "Delete Vouchers Progress..";
                SynchronizeHeld(this, new EventArgs());
            }

            AcMEDataSynLog.WriteLog("DeleteVouchers Started..");
            try
            {
                int VoucherId = 0;
                DataTable dtTrans = new DataTable();
                resultArgs = GetProjectIds();
                //Modified by Carmel Raj M on September-10-2015
                //Purpose : When import Type is SplitProject null value should not be validated
                if (resultArgs.Success && (resultArgs.ReturnValue != null || VoucherImportType == ImportType.SplitProject))
                {
                    ProjectIds = resultArgs.ReturnValue != null ? resultArgs.ReturnValue.ToString() : "0";
                    if (!string.IsNullOrEmpty(ProjectIds))
                    {
                        resultArgs = FetchTransactions();
                        if (resultArgs.Success)
                        {
                            dtTrans = resultArgs.DataSource.Table;
                            if (dtTrans != null && dtTrans.Rows.Count > 0)
                            {
                                foreach (DataRow drTrans in dtTrans.Rows)
                                {
                                    VoucherId = this.NumberSet.ToInteger(drTrans[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString());

                                    if (VoucherId > 0)
                                    {
                                        using (BalanceSystem balance = new BalanceSystem())
                                        {
                                            resultArgs = balance.UpdateTransBalance(BranchId, LocationId, VoucherId, TransactionAction.Cancel);
                                        }

                                        if (resultArgs.Success)
                                        {
                                            resultArgs = DeleteVoucherTransaction(VoucherId);
                                            if (resultArgs.Success)
                                            {
                                                resultArgs = DeleteVoucherCostCentre(VoucherId);
                                                if (resultArgs.Success)
                                                {
                                                    resultArgs = DeleteVoucherMasterTransaction(VoucherId);
                                                }
                                            }
                                        }
                                    }

                                    if (!resultArgs.Success)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally
            {
                if (resultArgs.Success)
                {
                    AcMEDataSynLog.WriteLog("DeleteVouchers Ended.");
                }
                else
                {
                    AcMEDataSynLog.WriteLog("Problem in Deleting Vouchers. " + resultArgs.Message);
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetch the Transaction between the Date range.
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchTransactions()
        {
            AcMEDataSynLog.WriteLog("FetchTransactions Started..");
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.FetchTransactions, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectIds ?? "0");
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_IDColumn, BranchId);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.LOCATION_IDColumn, LocationId);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in FetchTransactions: " + ex.ToString();
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("FetchTransactions Ended.");
            }
            return resultArgs;
        }

        /// <summary>
        /// Delete the Fixed Deposit Account Details.
        /// </summary>
        /// <param name="FDAccountId"></param>
        /// <returns></returns>
        private ResultArgs DeleteFDAccountDetails()  //int FDAccountId)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.DeleteFDAccount, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    //dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectIds ?? "0");
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_IDColumn, BranchId);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.LOCATION_IDColumn, LocationId);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.UpdateData();
                    if (!resultArgs.Success)
                    {
                        resultArgs.Message = "Error in Deleting Fixed Deposit Account Details. " + resultArgs.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Deleting Fixed Deposit Account Details: " + ex.ToString();
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// Delete the Entire renewal details of the Fixed Deposit Account.
        /// </summary>
        /// <param name="FDAccountId"></param>
        /// <returns></returns>
        private ResultArgs DeleteFDRenewalDetails()   //int FDAccountId)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.DeleteFDRenewal, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    //dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectIds ?? "0"); //Modified by Carmel Raj M on September-10-2015
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_IDColumn, BranchId);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.LOCATION_IDColumn, LocationId);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                    resultArgs = dataManager.UpdateData();
                    if (!resultArgs.Success)
                    {
                        AcMEDataSynLog.WriteLog("Error in Deleting Fixed Deposit Renewal Details. " + resultArgs.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Deleting Fixed Deposit Renewal Details: " + ex.ToString();
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// Delete the voucher Master Transaction based on the Voucher Id
        /// </summary>
        /// <param name="VoucherId"></param>
        /// <returns></returns>
        private ResultArgs DeleteVoucherMasterTransaction(int VoucherId)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.DeleteVoucherMasterTrans, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_IDColumn, BranchId);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.LOCATION_IDColumn, LocationId);

                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Deleting Voucher Master Transaction: " + ex.ToString();
            }
            finally { }

            if (!resultArgs.Success)
            {
                resultArgs.Message = "Error in Deleting Voucher Master Transaction. " + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// Delete the voucher Master Transaction based on the Voucher Id
        /// </summary>
        /// <param name="VoucherId"></param>
        /// <returns></returns>
        private ResultArgs DeleteFDVoucherMasterTransaction()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.DeleteFDVoucherMasterTrans, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectIds ?? "0");//Modified by Carmel Raj M on September-10-2015
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_IDColumn, BranchId);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.LOCATION_IDColumn, LocationId);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog("Error in Deleting Voucher Master Transaction: " + ex.ToString());
                resultArgs.Message = ex.Message;
            }
            finally { }

            if (!resultArgs.Success)
            {
                resultArgs.Message = "Error in Deleting Voucher Master Transaction. " + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// Delete the Voucher Transaction based on the voucher Id
        /// </summary>
        /// <param name="VoucherId">Voucher Id</param>
        /// <returns>resultargs whether success or failure</returns>
        private ResultArgs DeleteVoucherTransaction(int VoucherId)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.DeleteVoucherTrans, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_IDColumn, BranchId);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.LOCATION_IDColumn, LocationId);

                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Deleting Voucher Transaction: " + ex.ToString();
            }
            finally { }

            if (!resultArgs.Success)
            {
                resultArgs.Message = "Error in Deleting Voucher Transaction." + resultArgs.Message;
            }
            return resultArgs;
        }

        private ResultArgs FetchFDTransactions()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.FetchFDTransaction, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectIds ?? "0");//Modified by Carmel Raj M on September-10-2015
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_IDColumn, BranchId);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.LOCATION_IDColumn, LocationId);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally { }

            if (!resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("Error in Deleting Voucher Transaction." + resultArgs.Message);
            }
            return resultArgs;
        }

        /// <summary>
        /// Delete the Voucher Transaction based on the voucher Id
        /// </summary>
        /// <param name="VoucherId">Voucher Id</param>
        /// <returns>resultargs whether success or failure</returns>
        private ResultArgs DeleteFDVoucherTransaction()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.DeleteFDVoucherTrans, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectIds ?? "0");//Modified by Carmel Raj M on September-10-2015
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_IDColumn, BranchId);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.LOCATION_IDColumn, LocationId);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Deleting Voucher Transaction: " + ex.ToString();
            }
            finally { }

            if (!resultArgs.Success)
            {
                resultArgs.Message = "Error in Deleting Voucher Transaction." + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// Delete the Voucher Cost Centre based on the Voucher Id
        /// </summary>
        /// <param name="VoucherId">Voucher Id</param>
        /// <returns></returns>
        private ResultArgs DeleteVoucherCostCentre(int VoucherId)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.DeleteVoucherCostCentre, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_IDColumn, BranchId);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.LOCATION_IDColumn, LocationId);

                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Deleting Voucher Cost Centre: " + ex.ToString();
            }
            finally { }

            if (!resultArgs.Success)
            {
                resultArgs.Message = "Error in Deleting Voucher Cost Centre. " + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// 17/02/2020, Delete the Voucher Sub Ledgers based on the Voucher Id
        /// </summary>
        /// <param name="VoucherId">Voucher Id</param>
        /// <returns></returns>
        private ResultArgs DeleteVoucherSubLedgers(int VoucherId)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.DeleteVoucherSubLedger, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_IDColumn, BranchId);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.LOCATION_IDColumn, LocationId);

                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Deleting Voucher Sub Ledger: " + ex.ToString();
            }
            finally { }

            if (!resultArgs.Success)
            {
                resultArgs.Message = "Error in Deleting Voucher Sub Ledger. " + resultArgs.Message;
            }
            return resultArgs;
        }
        #endregion

        #region Ledger Balance

        /// <summary>
        /// Deleting Opening Balance 
        /// </summary>
        /// <returns></returns>
        private ResultArgs DeleteOpeningBalance()
        {
            try
            {
                if (MergerProjectId > 0)
                {
                    ProjectId = MergerProjectId;
                    resultArgs = DeleteOpBalance();
                }
                else
                {
                    if (dtLedgerBalance != null && dtLedgerBalance.Rows.Count > 0)
                    {
                        foreach (DataRow drBalance in dtLedgerBalance.Rows)
                        {
                            ProjectName = drBalance[this.AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                            ProjectId = GetId(Id.Project);
                            resultArgs = DeleteOpBalance();
                            if (!resultArgs.Success) break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("Deleting Opening Balance Ended.");
                // UpdateLedgerOPBalanceDate();
                UpdateOPBalanceDate();
            }
            else
            {
                AcMEDataSynLog.WriteLog("Problem in Deleting Opening Balance: " + resultArgs.Message);
            }
            return resultArgs;
        }


        private ResultArgs DeleteOpBalance()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.DeleteProjectOpBalance))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// Updating Branch wise Ledger opening balance
        /// </summary>
        /// <returns></returns>
        private ResultArgs UpdateLedgeOPBalance()
        {
            if (SynchronizeHeld != null)
            {
                Caption = "UpdateLedgerOPBalance Progress..";
                SynchronizeHeld(this, new EventArgs());
            }

            AcMEDataSynLog.WriteLog("UpdateLedgerOPBalance Started..");
            try
            {
                if (dtLedgerBalance != null && dtLedgerBalance.Rows.Count > 0)
                {

                    foreach (DataRow drBalance in dtLedgerBalance.Rows)
                    {
                        ProjectName = drBalance[this.AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                        ProjectId = MergerProjectId > 0 ? MergerProjectId : GetId(Id.Project);//Modified by Carmel Raj M on July-08-2015
                        LedgerName = drBalance[this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();

                        // Added by chinna
                        if (LedgerName.Equals("OPD General"))
                        {
                            string mess = "";
                        }

                        LedgerId = GetId(Id.Ledger);

                        //On 03/05/2021, to have proper ledger opening balance date
                        //BalanceDate = this.DateSet.ToDate(drBalance[this.AppSchema.LedgerBalance.BALANCE_DATEColumn.ColumnName].ToString(), false);
                        if (VoucherImportType == ImportType.SplitProject)
                        {
                            BalanceDate = DateSet.ToDate(this.BookBeginFrom, false);
                        }
                        else
                        {
                            BalanceDate = this.DateSet.ToDate(drBalance[this.AppSchema.LedgerBalance.BALANCE_DATEColumn.ColumnName].ToString(), false);
                        }
                        //Modified by Carmel Raj M on July-06-2015
                        //Purpose : Updating OP Balance for the same project with the Ledger ledger name from different project only if MergerProjectId >0
                        //BalanceAmount = MergerProjectId > 0 ? this.NumberSet.ToDouble(drBalance[this.AppSchema.LedgerBalance.AMOUNTColumn.ColumnName].ToString()) + GetPreviousOPBalance()
                        //     : this.NumberSet.ToDouble(drBalance[this.AppSchema.LedgerBalance.AMOUNTColumn.ColumnName].ToString());
                        BalanceAmount = this.NumberSet.ToDouble(drBalance[this.AppSchema.LedgerBalance.AMOUNTColumn.ColumnName].ToString());
                        BalanceTransMode = drBalance[this.AppSchema.LedgerBalance.TRANS_MODEColumn.ColumnName].ToString();
                        BalanceTransFlag = drBalance[this.AppSchema.LedgerBalance.TRANS_FLAGColumn.ColumnName].ToString();

                        //Ledger Balance details validation
                        if (LedgerId == 0)
                        {
                            //Modified by Carmel Raj M on July-12-2015
                            //Purpose : Message differentiation between Split Project and data Synchronization
                            resultArgs.Message = VoucherImportType.Equals(ImportType.SplitProject) ? String.Format("No Ledger is found while updating OP Balance.Missing Ledger is: {0}", LedgerName) : "Problem in Fetching Ledger Id from Head Office. Ledger Id is 0.<br/> The Required Ledger:<br/><b> " + LedgerName + "</b>";
                        }
                        else if (ProjectId == 0)
                        {
                            //Modified by Carmel Raj M on July-12-2015
                            //Purpose : Message differentiation between Split Project and data Synchronization
                            resultArgs.Message = VoucherImportType.Equals(ImportType.SplitProject) ? String.Format("No Project is found while updating OP Balance.The Missing Project  is: {0}", ProjectName) :
                                "Problem in Fetching Project Id from Head Office. Project Id is 0.<br/> The Required Project:<br/><b> " + ProjectName + "</b>";
                        }
                        else if (string.IsNullOrEmpty(BalanceTransMode))
                        {
                            resultArgs.Message = "Trans Mode is empty in Branch Voucher File while updating Ledger Opening Balance.";
                        }
                        else if (string.IsNullOrEmpty(BalanceTransFlag))
                        {
                            resultArgs.Message = "Trans Flag is empty in Branch Voucher File while updating Ledger Opening Balance.";
                        }
                        else
                        {
                            resultArgs = MapProject(SQLCommand.ImportVoucher.MapProjectLedger);
                            if (resultArgs.Success)
                            {
                                resultArgs = UpdateOpeningBalance();
                            }
                            else
                            {

                            }
                        }

                        if (!resultArgs.Success)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("UpdateLedgerOPBalance Ended.");
                // UpdateLedgerOPBalanceDate();
                UpdateOPBalanceDate();
            }
            else
            {
                AcMEDataSynLog.WriteLog("Problem in Updating Ledger Opening Balance: " + resultArgs.Message);
            }

            //On 22/02/2018, attach name of the project and voucher detailis in the error log
            if (!resultArgs.Success && string.IsNullOrEmpty(ProblemDataDetails))
            {
                ProblemDataDetails = "Project Name : " + ProjectName + "<br>" +
                                      "Ledger Name : " + LedgerName + "<br>" +
                                      "Problem in updating opening balance in Ledger";
            }
            return resultArgs;
        }

        private double GetPreviousOPBalance()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.GetPreviousOPBalance, DataBaseTypeName, SQLAdapterType.HOSQL))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return NumberSet.ToDouble(resultArgs.DataSource.Sclar.ToString);
        }


        /// <summary>
        /// This method Checks whether the balance is available already for the particular Ledger
        /// If the does not contain balance It will be inserted.
        /// </summary>
        /// <returns></returns>
        private ResultArgs UpdateOpeningBalance()
        {
            try
            {
                using (BalanceSystem balance = new BalanceSystem())
                {
                    if (balance.HasBalance((IsClientBranch) ? 0 : BranchId, ProjectId, LedgerId))
                    {
                        resultArgs = balance.UpdateOpBalance(BalanceDate.ToString(), (IsClientBranch) ? 0 : BranchId, ProjectId, LedgerId, BalanceAmount, BalanceTransMode, TransactionAction.Cancel);
                        resultArgs = balance.UpdateOpBalance(BalanceDate.ToString(), (IsClientBranch) ? 0 : BranchId, ProjectId, LedgerId, BalanceAmount, BalanceTransMode, TransactionAction.New);
                    }
                    else
                    {
                        resultArgs = balance.UpdateOpBalance(BalanceDate.ToString(), (IsClientBranch) ? 0 : BranchId, ProjectId, LedgerId, BalanceAmount, BalanceTransMode, TransactionAction.New);
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Problem in updating Opening Balance: " + ex.ToString();
            }
            finally { }

            //On 22/02/2018, attach name of the project and voucher detailis in the error log
            if (!resultArgs.Success && string.IsNullOrEmpty(ProblemDataDetails))
            {
                ProblemDataDetails = "Project Name : " + ProjectName + "<br>" +
                                        "Ledger Name : " + LedgerName + "<br>" +
                                        "Problem in updating Opening Balance in Ledger";
            }

            return resultArgs;
        }

        private ResultArgs FetchOPBalanceDate()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.FetchOPBalanceDate, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in FetchOPBalanceDate: " + ex.ToString();
            }
            finally { }
            return resultArgs;
        }

        private ResultArgs UpdateOPBalanceDate() //DateTime BalanceDate)
        {
            try
            {
                //On 13/04/2021, to delete ledger opening balances which are more than one records for a project
                DeleteOPBalanceDateMoreThanOne();

                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.UpdateOPBalanceDate, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    //dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BALANCE_DATEColumn, BalanceDate);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in FetchOPBalanceDate: " + ex.ToString();
            }
            finally { }

            //On 22/02/2018, attach name of the project and voucher detailis in the error log
            if (!resultArgs.Success && string.IsNullOrEmpty(ProblemDataDetails))
            {
                ProblemDataDetails = "Project Name : " + ProjectName + "<br>" +
                                        "Problem in updating Opening Date in Ledger balance";
            }
            return resultArgs;
        }

        /// <summary>
        /// On 13/04/2021, to delete ledger opening balances which are more than one records for a project
        /// </summary>
        /// <returns></returns>
        private ResultArgs DeleteOPBalanceDateMoreThanOne(DataManager dmanager = null)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.DeleteOPBalanceDateMoreThanOne, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    if (dmanager != null)
                    {
                        dataManager.Database = dmanager.Database;
                    }
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in DeleteOPBalanceDateMoreThanOne: " + ex.ToString();
            }
            finally { }

            //On 22/02/2018, attach name of the project and voucher detailis in the error log
            if (!resultArgs.Success && string.IsNullOrEmpty(ProblemDataDetails))
            {
                ProblemDataDetails = "Project Name : " + ProjectName + "<br>" +
                                        "Problem in deleting ledger opening balance which are more than one records";
            }
            return resultArgs;
        }

        /// <summary>
        /// On 01/06/2021, To update ledger opening balance for given date
        /// 1. delete incorrect ledger opening balance date (some branch have two opening balance for particularu ledger)
        /// 2. updtae all exisig ledger op date to given date
        /// 4. update FD opening balance date
        /// </summary>
        /// <param name="BalanceDate"></param>
        /// <returns></returns>
        public ResultArgs UpdateLedgerOpBalanceDate(string BalanceDate, DataManager dmanager = null)
        {
            try
            {
                //1. to delete ledger opening balances which are more than one records for a project
                ResultArgs result = DeleteOPBalanceDateMoreThanOne(dmanager);
                if (result != null && result.Success)
                {

                    //2. Update all opening balances
                    using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.UpdateOPBalanceDateByDate, DataBaseTypeName, SQLAdapterType.HOSQL))
                    {
                        if (dmanager != null)
                        {
                            dataManager.Database = dmanager.Database;
                        }
                        dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BALANCE_DATEColumn, BalanceDate);
                        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        resultArgs = dataManager.UpdateData();
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in UpdateLedgerOpBalanceDate : " + ex.ToString();
            }
            finally { }

            return resultArgs;
        }
        #endregion

        #region Fixed Deposit
        private ResultArgs ImportFixedDeposit()
        {
            if (SynchronizeHeld != null)
            {
                Caption = "Import Fixed Deposit Progress..";
                SynchronizeHeld(this, new EventArgs());
            }

            AcMEDataSynLog.WriteLog("ImportFixedDeposit Started..");   //Temporary. Bank Account Details has been brought in the Transaction Masters
            try
            {
                //resultArgs = ImportFDBank();
                //if (resultArgs.Success)
                //{
                //    resultArgs = ImportFDLedger();
                //    if (resultArgs.Success)
                //    {
                //        resultArgs = ImportFDBankAccount();
                //        if (resultArgs.Success)
                //        {
                resultArgs = DeleteFDVouchers();
                if (resultArgs.Success)
                {
                    resultArgs = DeleteFDAccount();
                    if (resultArgs.Success)
                    {
                        resultArgs = ImportFDVoucherDetails();
                        if (resultArgs.Success)
                        {
                            resultArgs = ImportFDAccount();
                            if (resultArgs.Success)
                            {
                                resultArgs = ImportFDRenewalDetails();
                            }

                            //On 03/07/2020 to update FD option balacne for Project Split Import/Export ---------------------------
                            if (resultArgs.Success && VoucherImportType == ImportType.SplitProject && IsFYSplit)
                            {
                                UpdateFDOpeningLedgerBalanceByProject();
                            }
                            else if (resultArgs.Success && VoucherImportType == ImportType.SplitProject && !IsFYSplit)
                            { //#30/04/2021, delete imported project's fd ledger opening balances and update it.
                                DeleteFDOpeningLedgerBalanceByProject();
                                UpdateFDOpeningLedgerBalanceByProject();
                            }
                            //-------------------------------------------------------------------------------------------------
                        }
                    }
                }
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in ImportFixedDeposit: " + ex.ToString();
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ImportFixedDeposit Ended.");
            }
            return resultArgs;
        }

        /// <summary>
        /// This method is to import the FD account details.
        /// </summary>
        /// <returns></returns>
        private ResultArgs ImportFDAccount()
        {
            string FDLedgerName = string.Empty;
            Int32 FDLedgerId = 0;
            AcMEDataSynLog.WriteLog("ImportFDAccount Started..");
            try
            {
                if (dtFDAccount != null && dtFDAccount.Rows.Count > 0)
                {
                    DataView dvFDAccount = dtFDAccount.DefaultView;
                    dvFDAccount.RowFilter = "STATUS<>0";
                    if (dvFDAccount != null && dvFDAccount.Count > 0)
                    {
                        foreach (DataRowView drFD in dvFDAccount)
                        {
                            FDAccountId = this.NumberSet.ToInteger(drFD[this.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn.ColumnName].ToString());
                            FDAccountNumber = drFD[this.AppSchema.FDAccount.FD_ACCOUNT_NUMBERColumn.ColumnName].ToString();
                            FDSchemeType = this.NumberSet.ToInteger(drFD[this.AppSchema.FDAccount.FD_SCHEMEColumn.ColumnName].ToString());
                            ProjectName = drFD[this.AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                            ProjectId = MergerProjectId > 0 ? MergerProjectId : GetId(Id.Project); //Modified by Carmel Raj M on July-08-2015
                            LedgerName = drFD[this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                            LedgerId = GetId(Id.Ledger);
                            FDLedgerName = LedgerName;
                            FDLedgerId = LedgerId;
                            BankName = drFD[this.AppSchema.Bank.BANKColumn.ColumnName].ToString();
                            Branch = "";
                            BankId = GetId(Id.Bank);
                            //On 11/12/2018, for investment, it takes same ledger name (Fixed Deposit) ledger insed of Bank ledger
                            //LedgerName = drFD[this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                            LedgerName = drFD["BANK_LEDGER"].ToString();
                            BankLedgerId = GetId(Id.Ledger);
                            VoucherId = NumberSet.ToInteger(drFD[this.AppSchema.FDAccount.FD_VOUCHER_IDColumn.ColumnName].ToString());
                            FdAmount = NumberSet.ToDouble(drFD[this.AppSchema.FDAccount.AMOUNTColumn.ColumnName].ToString());
                            FDTransType = drFD[this.AppSchema.FDAccount.TRANS_TYPEColumn.ColumnName].ToString();
                            ReceiptNo = drFD[this.AppSchema.FDAccount.RECEIPT_NOColumn.ColumnName].ToString();
                            FDAccountHolderName = drFD[this.AppSchema.FDAccount.ACCOUNT_HOLDERColumn.ColumnName].ToString();
                            CreatedOn = DateSet.ToDate(drFD[this.AppSchema.FDAccount.INVESTMENT_DATEColumn.ColumnName].ToString(), false);
                            FDTransMode = drFD[this.AppSchema.FDAccount.TRANS_MODEColumn.ColumnName].ToString();
                            InterestType = NumberSet.ToInteger(drFD[this.AppSchema.FDAccount.INTEREST_TYPEColumn.ColumnName].ToString());
                            FDMaturityDate = DateSet.ToDate(drFD[this.AppSchema.FDAccount.MATURED_ONColumn.ColumnName].ToString(), false);
                            FDInterestRate = NumberSet.ToDouble(drFD[this.AppSchema.FDAccount.INTEREST_RATEColumn.ColumnName].ToString());
                            FDInterestAmount = NumberSet.ToDouble(drFD[this.AppSchema.FDAccount.INTEREST_AMOUNTColumn.ColumnName].ToString());
                            LedgerNotes = drFD[this.AppSchema.FDAccount.NOTESColumn.ColumnName].ToString();
                            FDSubTypes = drFD[this.AppSchema.FDAccount.FD_SUB_TYPESColumn.ColumnName].ToString();

                            //On 15/05/2024, For Mutual Fund properties
                            MFFolioNo = drFD[this.AppSchema.FDAccount.MF_FOLIO_NOColumn.ColumnName].ToString();
                            MFSchemeName = drFD[this.AppSchema.FDAccount.MF_SCHEME_NAMEColumn.ColumnName].ToString();
                            MFNAVperUnit = NumberSet.ToDouble(drFD[this.AppSchema.FDAccount.MF_NAV_PER_UNITColumn.ColumnName].ToString());
                            MFNAVUnits = NumberSet.ToDouble(drFD[this.AppSchema.FDAccount.MF_NO_OF_UNITSColumn.ColumnName].ToString());
                            MFModeOfHodling = NumberSet.ToInteger(drFD[this.AppSchema.FDAccount.MF_MODE_OF_HOLDINGColumn.ColumnName].ToString());

                            FDStatus = this.NumberSet.ToInteger(drFD[this.AppSchema.FDAccount.FD_STATUSColumn.ColumnName].ToString());
                            //Add by  Carmel Raj M on August-18-2015
                            //Purpose : To get lasted inserted value of the master fd voucher
                            if (VoucherImportType.Equals(ImportType.SplitProject))
                            {
                                if (FDVoucherDetails != null)
                                {
                                    VoucherId = FDVoucherDetails.FirstOrDefault(x => x.Key == VoucherId).Value;
                                }
                            }


                            //FD Account details validation
                            if (ProjectId == 0)
                            {
                                //Modified by Carmel Raj M on July-12-2015
                                //Purpose : Message differentiation between Split Project and data Synchronization
                                resultArgs.Message = VoucherImportType.Equals(ImportType.SplitProject) ? String.Format("Project is not available in the Master table but it has Fixed Deposit Account Details. The Missing Project is {0}", ProjectName) :
                                    "Project Name is not available in Head office while updating Fixed Deposit Account Details.<br/> The Required Project is:<br/><b> " + ProjectName + "</b>";
                            }
                            else if (BankId == 0)
                            {
                                //Modified by Carmel Raj M on July-12-2015
                                //Purpose : Message differentiation between Split Project and data Synchronization
                                resultArgs.Message = VoucherImportType.Equals(ImportType.SplitProject) ? String.Format("Bank Name is missing in the master table but it has Fixed Deposit Account Details. The Missing Bank is {0}", BankName) :
                                    "Bank Name is not available in Head Office while updating Fixed Deposit Account Details. <br/> The Required Bank is:<br/><b> " + BankName + "</b>";
                            }
                            else if ((!string.IsNullOrEmpty(FDLedgerName) && FDLedgerId == 0))
                            {   //On 11/06/2021, Check Fixed Deposit ledger name
                                resultArgs.Message = VoucherImportType.Equals(ImportType.SplitProject) ? String.Format("Fixed Deposit Ledger is missing in the master table but it has Fixed Deposit Account Details. The Missing Fixed Deposit Ledger is '{0}'", FDLedgerName) :
                                    "Fixed Deposite Ledger is not available in Head Office while updating Fixed Deposit Account Details. <br/> The Required Fixed Deposit Ledger is:<br/><b> " + FDLedgerName + "</b>";
                            }
                            else if ((!string.IsNullOrEmpty(LedgerName) && BankLedgerId == 0))
                            {   //On 11/06/2021, Check Bank ledger name
                                resultArgs.Message = VoucherImportType.Equals(ImportType.SplitProject) ? String.Format("FD Bank Ledger is missing in the master table but it has Fixed Deposit Account Details. The Missing Fixed Ledger is '{0}'", LedgerName) :
                                    "FD Bank Ledger is not available in Head Office while updating Fixed Deposit Account Details. <br/> The Required FD Bank Ledger is:<br/><b> " + LedgerName + "</b>";
                            }
                            else
                            {
                                resultArgs = SaveFDAccountDetails();
                            }

                            if (!resultArgs.Success)
                            {
                                break;
                            }
                            dvFDAccount.RowFilter = " ";
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ImportFDAccount Ended.");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Problem in Importing Fixed Deposit Account Details. " + resultArgs.Message);
            }
            return resultArgs;
        }

        /// <summary>
        /// This method is to insert the Fixed Deposit Renewal details.
        /// </summary>
        /// <returns></returns>
        private ResultArgs ImportFDRenewalDetails()
        {
            AcMEDataSynLog.WriteLog("ImportFDRenewalDetails Started.");
            try
            {
                if (dtFDRenewal != null && dtFDRenewal.Rows.Count > 0)
                {
                    DataView dvFDRenewal = dtFDRenewal.DefaultView;
                    dvFDRenewal.RowFilter = "STATUS<>0";
                    if (dvFDRenewal != null && dvFDRenewal.Count > 0)
                    {
                        foreach (DataRowView drFDRenewal in dvFDRenewal)
                        {
                            FDAccountId = this.NumberSet.ToInteger(drFDRenewal[this.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn.ColumnName].ToString());
                            LedgerName = drFDRenewal[this.AppSchema.FDRenewal.INTEREST_LEDGERColumn.ColumnName].ToString();
                            string IntrestLedgerName = LedgerName;
                            IntrestLedgerId = GetId(Id.Ledger);
                            LedgerName = drFDRenewal[this.AppSchema.FDRenewal.BANK_LEDGERColumn.ColumnName].ToString();
                            BankLedgerId = GetId(Id.Ledger);
                            FDIntrestVoucherId = this.NumberSet.ToInteger(drFDRenewal[this.AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn.ColumnName].ToString());
                            VoucherId = this.NumberSet.ToInteger(drFDRenewal[this.AppSchema.FDRenewal.FD_VOUCHER_IDColumn.ColumnName].ToString());
                            IntrestAmount = this.NumberSet.ToDouble(drFDRenewal[this.AppSchema.FDRenewal.INTEREST_AMOUNTColumn.ColumnName].ToString());
                            WithdrawAmount = this.NumberSet.ToDouble(drFDRenewal[this.AppSchema.FDRenewal.WITHDRAWAL_AMOUNTColumn.ColumnName].ToString());
                            ReInvestedAmount = this.NumberSet.ToDouble(drFDRenewal[this.AppSchema.FDRenewal.REINVESTED_AMOUNTColumn.ColumnName].ToString());
                            RenewedDate = this.DateSet.ToDate(drFDRenewal[this.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName].ToString(), false);
                            FDMaturityDate = this.DateSet.ToDate(drFDRenewal[this.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName].ToString(), false);
                            //On 07/04/2021, FD Closed Date
                            if (dvFDRenewal.Table.Columns.Contains(this.AppSchema.FDRenewal.CLOSED_DATEColumn.ColumnName))
                            {
                                if (!String.IsNullOrEmpty(drFDRenewal[this.AppSchema.FDRenewal.CLOSED_DATEColumn.ColumnName].ToString()))
                                {
                                    FDClosedDate = this.DateSet.ToDate(drFDRenewal[this.AppSchema.FDRenewal.CLOSED_DATEColumn.ColumnName].ToString(), false);
                                }
                            }

                            //On 08/06/2022, For Penalty Amount and its ledtgers
                            if (dvFDRenewal.Table.Columns.Contains(this.AppSchema.FDRenewal.CHARGE_MODEColumn.ColumnName))
                            {
                                TDSAmount = this.NumberSet.ToDouble(drFDRenewal[this.AppSchema.FDRenewal.TDS_AMOUNTColumn.ColumnName].ToString());
                                ChargeMode = this.NumberSet.ToInteger(drFDRenewal[this.AppSchema.FDRenewal.CHARGE_MODEColumn.ColumnName].ToString());
                                ChargeAmount = this.NumberSet.ToDouble(drFDRenewal[this.AppSchema.FDRenewal.CHARGE_AMOUNTColumn.ColumnName].ToString());
                                ChargeLedgerId = this.NumberSet.ToInteger(drFDRenewal[this.AppSchema.FDRenewal.CHARGE_LEDGER_IDColumn.ColumnName].ToString());
                                //If deduction mode is none, make charge and charge ledger zero
                                if (ChargeMode == 0)
                                {
                                    ChargeAmount = ChargeLedgerId = 0;
                                }
                            }

                            FDInterestRate = this.NumberSet.ToDouble(drFDRenewal[this.AppSchema.FDRenewal.INTEREST_RATEColumn.ColumnName].ToString());
                            InterestType = this.NumberSet.ToInteger(drFDRenewal[this.AppSchema.FDRenewal.INTEREST_TYPEColumn.ColumnName].ToString());
                            ReceiptNo = drFDRenewal[this.AppSchema.FDRenewal.RECEIPT_NOColumn.ColumnName].ToString();
                            RenewalType = drFDRenewal[this.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName].ToString();
                            FDStatus = this.NumberSet.ToInteger(drFDRenewal[this.AppSchema.FDRenewal.STATUSColumn.ColumnName].ToString());
                            FDType = drFDRenewal[AppSchema.FDRenewal.FD_TYPEColumn.ColumnName].ToString();

                            //On 15/12/2023, For FD Trans Mode for FD Adustment Entries - Accumulated Interst
                            FDRenewalTransMode = TransactionMode.DR.ToString();
                            if (dvFDRenewal.Table.Columns.Contains(this.AppSchema.FDRenewal.FD_TRANS_MODEColumn.ColumnName) &&
                                RenewalType.ToUpper() == FDRenewalTypes.ACI.ToString().ToUpper())
                            {
                                FDRenewalTransMode = drFDRenewal[this.AppSchema.FDRenewal.FD_TRANS_MODEColumn.ColumnName].ToString();
                            }


                            //Add by  Carmel Raj M on August-18-2015
                            //Purpose : To get lasted inserted value of the master fd voucher
                            if (VoucherImportType.Equals(ImportType.SplitProject))
                            {
                                if (FDVoucherDetails != null)
                                {
                                    FDIntrestVoucherId = FDVoucherDetails.FirstOrDefault(x => x.Key == FDIntrestVoucherId).Value;
                                }
                                if (FDVoucherDetails != null)
                                {
                                    VoucherId = FDVoucherDetails.FirstOrDefault(x => x.Key == VoucherId).Value;
                                }
                            }
                            //FD Renewal details validation
                            if ((!string.IsNullOrEmpty(IntrestLedgerName) && IntrestLedgerId == 0))
                            {   //On 11/06/2021, Check Fixed Deposit ledger name
                                resultArgs.Message = VoucherImportType.Equals(ImportType.SplitProject) ? String.Format("Interest Ledger is missing in the master table but it has Fixed Deposit Renewal Details. The Missing Fixed Deposit Ledger is '{0}'", IntrestLedgerName) :
                                    "Fixed Deposite Interest Ledger is not available in Head Office while updating Fixed Deposit Renewal Details. <br/> The Required Fixed Deposit Ledger is:<br/><b> " + IntrestLedgerName + "</b>";
                            }
                            else if ((!string.IsNullOrEmpty(LedgerName) && BankLedgerId == 0))
                            {   //On 11/06/2021, Check Bank ledger name
                                resultArgs.Message = VoucherImportType.Equals(ImportType.SplitProject) ? String.Format("FD Bank Ledger is missing in the master table but it has Fixed Deposit Account Details. The Missing Fixed Ledger is '{0}'", LedgerName) :
                                    "FD Bank Ledger is not available in Head Office while updating Fixed Deposit Account Details. <br/> The Required FD Bank Ledger is:<br/><b> " + LedgerName + "</b>";
                            }
                            else
                            {
                                resultArgs = SaveFDRenewalDetails();
                            }
                            if (!resultArgs.Success)
                            {
                                break;
                            }

                        }
                    }
                    dvFDRenewal.RowFilter = " ";
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ImportFDRenewalDetails Ended.");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Problem in Importing FD Renewal Detail : " + resultArgs.Message);
            }
            return resultArgs;
        }

        /// <summary>
        /// This method is to Insert the FD related transactions.(Contra, Receipt, Journal)
        /// </summary>
        /// <returns></returns>
        private ResultArgs ImportFDVoucherDetails()
        {
            AcMEDataSynLog.WriteLog("ImportFDVoucherDetails Started.");
            try
            {
                if (dtFDVoucherMasterTrans != null && dtFDVoucherMasterTrans.Rows.Count > 0)
                {
                    DataView dvFDVoucherMasterTrans = dtFDVoucherMasterTrans.DefaultView;
                    dvFDVoucherMasterTrans.RowFilter = "STATUS<>0";
                    if (dvFDVoucherMasterTrans != null && dvFDVoucherMasterTrans.Count > 0)
                    {
                        foreach (DataRowView drMaster in dvFDVoucherMasterTrans)
                        {
                            VoucherId = this.NumberSet.ToInteger(drMaster[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString());
                            VoucherNo = drMaster[this.AppSchema.VoucherMaster.VOUCHER_NOColumn.ColumnName].ToString();
                            VoucherType = drMaster[this.AppSchema.VoucherMaster.VOUCHER_TYPEColumn.ColumnName].ToString();
                            VoucherSubType = drMaster[this.AppSchema.VoucherMaster.VOUCHER_SUB_TYPEColumn.ColumnName].ToString();
                            ContributionType = drMaster[this.AppSchema.VoucherMaster.CONTRIBUTION_TYPEColumn.ColumnName].ToString();
                            ContributionAmount = this.NumberSet.ToDecimal(drMaster[this.AppSchema.VoucherMaster.CONTRIBUTION_AMOUNTColumn.ColumnName].ToString());
                            ExchangeRate = this.NumberSet.ToDecimal(drMaster[this.AppSchema.VoucherMaster.EXCHANGE_RATEColumn.ColumnName].ToString());
                            CalculatedAmount = this.NumberSet.ToDecimal(drMaster[this.AppSchema.VoucherMaster.CALCULATED_AMOUNTColumn.ColumnName].ToString());
                            ActualAmount = this.NumberSet.ToDecimal(drMaster[this.AppSchema.VoucherMaster.ACTUAL_AMOUNTColumn.ColumnName].ToString());
                            Narration = drMaster[this.AppSchema.VoucherMaster.NARRATIONColumn.ColumnName].ToString();
                            NameAddress = drMaster[this.AppSchema.VoucherMaster.NAME_ADDRESSColumn.ColumnName].ToString();
                            VoucherDate = this.DateSet.ToDate(drMaster[this.AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName].ToString(), false);
                            ProjectName = drMaster[this.AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                            ProjectId = MergerProjectId > 0 ? MergerProjectId : GetId(Id.Project);//Modified by Carmel Raj M on July-08-2015
                            // DonorName = drMaster[this.AppSchema.DonorAuditor.NAMEColumn.ColumnName].ToString();
                            DonorId = this.NumberSet.ToInteger(drMaster[this.AppSchema.VoucherMaster.DONOR_IDColumn.ColumnName].ToString());
                            //   PurposeName = drMaster[this.AppSchema.Purposes.FC_PURPOSEColumn.ColumnName].ToString();
                            PurposeId = this.NumberSet.ToInteger(drMaster[this.AppSchema.VoucherMaster.PURPOSE_IDColumn.ColumnName].ToString());
                            //  CountryName = drMaster[this.AppSchema.Country.CURRENCY_COUNTRYColumn.ColumnName].ToString();
                            CurrencyCountryId = this.NumberSet.ToInteger(drMaster[this.AppSchema.VoucherMaster.CURRENCY_COUNTRY_IDColumn.ColumnName].ToString());
                            //  CountryName = drMaster[this.AppSchema.Country.EXCHANGE_COUNTRYColumn.ColumnName].ToString();
                            ExchageCountryId = this.NumberSet.ToInteger(drMaster[this.AppSchema.VoucherMaster.EXCHANGE_COUNTRY_IDColumn.ColumnName].ToString());

                            //On 13/02/2019, for multi voucher type defintinid
                            VoucherDefinitionId = (Int32)DefaultVoucherTypes.Contra;
                            if (VoucherType == "RC") VoucherDefinitionId = (Int32)DefaultVoucherTypes.Receipt;
                            else if (VoucherType == "PY") VoucherDefinitionId = (Int32)DefaultVoucherTypes.Payment;
                            else if (VoucherType == "CN") VoucherDefinitionId = (Int32)DefaultVoucherTypes.Contra;
                            else if (VoucherType == "JN") VoucherDefinitionId = (Int32)DefaultVoucherTypes.Journal;

                            // Voucher Master Details Validation.
                            if (ProjectId == 0)
                            {
                                //Modified by Carmel Raj M on July-12-2015
                                //Purpose : Message differentiation between Split Project and data Synchronization
                                resultArgs.Message = VoucherImportType.Equals(ImportType.SplitProject) ? String.Format("Project is not available while updating Fixed Deposit Details.The Required Project is {0}", ProjectName) :
                                    "Project is not available in Head Office while updating Fixed Deposit Details. <br/> It has been modified either in Branch Office or Head Office. <br/> The Required Project is:<br/><b>" + ProjectName + "</b>";
                            }
                            else
                            {
                                resultArgs = SaveVoucherMasters();  //Inserting Voucher Master Details
                                if (resultArgs.Success)
                                {
                                    LastVoucherId = (VoucherImportType == ImportType.HeadOffice) ? VoucherId : this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                    //Added by Carmel Raj M on August-18-2015
                                    //Purpose : To maintain new voucher id for all the old voucher id
                                    if (FDVoucherDetails == null)
                                        FDVoucherDetails = new Dictionary<int, int>();
                                    FDVoucherDetails.Add(VoucherId, LastVoucherId);
                                    resultArgs = ImportFDVoucherTrans();
                                    if (resultArgs.Success)
                                    {
                                        using (BalanceSystem balance = new BalanceSystem())
                                        {
                                            resultArgs = balance.UpdateTransBalance(BranchId, LocationId, LastVoucherId, TransactionAction.New);
                                        }
                                    }
                                }
                            }
                            if (!resultArgs.Success) { break; }
                        }
                        dvFDVoucherMasterTrans.RowFilter = "";
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally { }
            if (!resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("Problem in Importing FD vouchers" + resultArgs.Message);
            }
            return resultArgs;
        }

        /// <summary>
        /// Fixed Deposit Voucher Transaction.
        /// </summary>
        /// <returns></returns>
        private ResultArgs ImportFDVoucherTrans()
        {
            string RtnMessage = string.Empty;
            try
            {
                if (VoucherId > 0)
                {
                    if (dtFDVoucherTrans != null && dtFDVoucherTrans.Rows.Count > 0)
                    {
                        DataView dvTrans = dtFDVoucherTrans.DefaultView;
                        dvTrans.RowFilter = "VOUCHER_ID=" + VoucherId;
                        if (dvTrans.Count > 1)
                        {
                            foreach (DataRowView dr in dvTrans)
                            {
                                SequenceNo = this.NumberSet.ToInteger(dr[this.AppSchema.VoucherTransaction.SEQUENCE_NOColumn.ColumnName].ToString());
                                LedgerName = dr[this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                                LedgerId = GetId(Id.Ledger);
                                Amount = this.NumberSet.ToDecimal(dr[this.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString());
                                TransMode = dr[this.AppSchema.VoucherTransaction.TRANS_MODEColumn.ColumnName].ToString();
                                ChequeNo = dr[this.AppSchema.VoucherTransaction.CHEQUE_NOColumn.ColumnName].ToString();
                                MaterializedOn = dr[this.AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName].ToString() != null ?
                                    dr[this.AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName].ToString() : string.Empty;

                                //Voucher Transaction Validation
                                if (LedgerId == 0)
                                {
                                    //Modified by Carmel Raj M on July-12-2015
                                    //Purpose : Message differentiation between Split Project and data Synchronization
                                    resultArgs.Message = VoucherImportType.Equals(ImportType.SplitProject) ? String.Format("Master Ledger is not available in master table but it has transaction.The Missing Ledger is {0}", LedgerName) :
                                        "Problem in Fetching LedgerId from Head Office, DataSyn The Required Ledger is. " + LedgerName + "</b>";
                                }
                                else if (TransMode == string.Empty)
                                {
                                    //Modified by Carmel Raj M on July-12-2015
                                    //Purpose : Message differentiation between Split Project and data Synchronization
                                    resultArgs.Message = VoucherImportType.Equals(ImportType.SplitProject) ? String.Format("Trans mode is empty for the Ledger {0} for the Voucher Id {1}", LedgerName, VoucherId) :
                                        "FD Trans Mode is Empty in Branch Office Voucher XML";
                                }
                                else
                                {
                                    resultArgs = MapProject(SQLCommand.ImportVoucher.MapProjectLedger);  //Mapping Project Ledger
                                    if (resultArgs.Success)
                                    {
                                        resultArgs = SaveVoucherTrans();  //Inserting Voucher Transaction Detaiils
                                    }
                                }

                                if (!resultArgs.Success)
                                {
                                    resultArgs.Message = "Error in Importing Fixed Deposit Voucher Transaction. " + resultArgs.Message;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            resultArgs.Message = "The transaction is not valid. Only one transaction is available. Cash/Bank transaction is not available " + VoucherId;
                        }
                        dvTrans.RowFilter = "";
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Importing Fixed Deposit Voucher Transaction: " + ex.ToString();
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ImportFDVoucherTrans Ended.");
            }
            return resultArgs;
        }

        /// <summary>
        /// On 03/07/2020, to update sum of FD opening accounts to ledger balance as opening balance for books begin
        /// </summary>
        /// <returns></returns>
        private ResultArgs UpdateFDOpeningLedgerBalanceByProject()
        {
            try
            {
                if (VoucherImportType == ImportType.SplitProject)
                {
                    string openingdate = DateSet.ToDate(this.BookBeginFrom, false).AddDays(-1).ToShortDateString();
                    using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.UpdateFDLedgerOpeningBalanceByProject, DataBaseTypeName, SQLAdapterType.HOSQL))
                    {
                        dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BALANCE_DATEColumn, openingdate);
                        dataManager.Parameters.Add(this.AppSchema.LedgerBalance.PROJECT_IDColumn, ProjectId);
                        resultArgs = dataManager.UpdateData();
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in updating opening fd account's ledger balance : " + ex.ToString();
            }
            finally { }

            if (!resultArgs.Success)
            {
                resultArgs.Message = "Error in updating opening fd account's ledger balance. " + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// 30/04/2021, to delete selected projects all FD ledger's opening balance
        /// </summary>
        /// <returns></returns>
        private ResultArgs DeleteFDOpeningLedgerBalanceByProject()
        {
            try
            {
                if (VoucherImportType == ImportType.SplitProject)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.DeleteFDLedgerOpeningBalanceByProject, DataBaseTypeName, SQLAdapterType.HOSQL))
                    {
                        dataManager.Parameters.Add(this.AppSchema.LedgerBalance.PROJECT_IDColumn, ProjectId);
                        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        resultArgs = dataManager.UpdateData();
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in deleting opening fd account's ledger balance : " + ex.ToString();
            }
            finally { }

            if (!resultArgs.Success)
            {
                resultArgs.Message = "Error in deletingopening fd account's ledger balance. " + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// This method is used to Check whether the Bank is available in Head Office.
        /// If the Bank is not available in Head Office then the Bank Details will be Inserted in Head Office.
        /// </summary>
        /// <returns></returns>
        private ResultArgs ImportFDBank()
        {
            AcMEDataSynLog.WriteLog("ImportFDBank Started..");
            try
            {
                bool isBankExists = true;
                using (DataManager dataManager = new DataManager())
                {
                    if (dtFDBank != null && dtFDBank.Rows.Count > 0)
                    {
                        foreach (DataRow drFDBank in dtFDBank.Rows)
                        {
                            BankName = drFDBank[this.AppSchema.Bank.BANKColumn.ColumnName].ToString();

                            resultArgs = IsExists(SQLCommand.ImportVoucher.IsBankExists);
                            if (resultArgs.Success)
                            {
                                isBankExists = resultArgs.DataSource.Sclar.ToInteger > 0;
                                if (!isBankExists)
                                {
                                    //Insert Bank Details
                                    BankCode = drFDBank[this.AppSchema.Bank.BANK_CODEColumn.ColumnName].ToString();
                                    BankName = drFDBank[this.AppSchema.Bank.BANKColumn.ColumnName].ToString();
                                    Branch = drFDBank[this.AppSchema.Bank.BRANCHColumn.ColumnName].ToString();
                                    Address = drFDBank[this.AppSchema.Bank.ADDRESSColumn.ColumnName].ToString();
                                    IFSCCode = drFDBank[this.AppSchema.Bank.IFSCCODEColumn.ColumnName].ToString();
                                    MICRCode = drFDBank[this.AppSchema.Bank.MICRCODEColumn.ColumnName].ToString();
                                    ContactNumber = drFDBank[this.AppSchema.Bank.CONTACTNUMBERColumn.ColumnName].ToString();
                                    SWIFTCode = drFDBank[this.AppSchema.Bank.SWIFTCODEColumn.ColumnName].ToString();

                                    if (string.IsNullOrEmpty(BankName))
                                    {
                                        resultArgs.Message = "Bank Name is Empty in Branch Office Voucher XML.";
                                    }
                                    else if (string.IsNullOrEmpty(Branch))
                                    {
                                        resultArgs.Message = "Branch is Empty in Branch Office Voucher XML.";
                                    }
                                    else
                                    {
                                        resultArgs = SaveBankDetails();
                                    }
                                }
                            }

                            if (!resultArgs.Success)
                            {
                                resultArgs.Message = "Error in ImportFDBank. " + resultArgs.Message;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in ImportFDBank: " + ex.ToString();
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ImportFDBank Ended.");
            }

            //On 22/02/2018, attach name of the project and voucher detailis in the error log
            if (!resultArgs.Success && string.IsNullOrEmpty(ProblemDataDetails))
            {
                ProblemDataDetails = "FD Bank : " + BankName + "<br>" +
                                      "Problem in updating Master (FD Bank)";
            }
            return resultArgs;
        }

        /// <summary>
        /// This method Checks for the Fixed Deposit available in Head Office.
        /// If the Ledger is not available then the FD Ledger will be inserted in Head Office.
        /// </summary>
        /// <returns></returns>
        private ResultArgs ImportFDLedger()
        {
            AcMEDataSynLog.WriteLog("ImportFDLedger Started..");
            try
            {
                bool isLedgerExists = true;
                using (DataManager dataManager = new DataManager())
                {
                    if (dtFDBankAccount != null && dtFDBankAccount.Rows.Count > 0)
                    {
                        foreach (DataRow drFD in dtFDBankAccount.Rows)
                        {
                            AccountNumber = drFD[this.AppSchema.BankAccount.ACCOUNT_NUMBERColumn.ColumnName].ToString();

                            resultArgs = IsExists(SQLCommand.ImportVoucher.IsLedgerBankExists);
                            if (resultArgs.Success)
                            {
                                isLedgerExists = resultArgs.DataSource.Sclar.ToInteger > 0;
                                if (!isLedgerExists)
                                {
                                    LedgerCode = drFD[this.AppSchema.BankAccount.ACCOUNT_CODEColumn.ColumnName].ToString();
                                    LedgerName = drFD[this.AppSchema.BankAccount.ACCOUNT_NUMBERColumn.ColumnName].ToString();
                                    GroupId = (int)FixedLedgerGroup.FixedDeposit;

                                    if (string.IsNullOrEmpty(LedgerName))
                                    {
                                        resultArgs.Message = "Fixed Deposit Ledger Name is Empty in Branch Office Voucher XML.";
                                    }
                                    else if (string.IsNullOrEmpty(LedgerCode))
                                    {
                                        resultArgs.Message = "Fixed Deposit Ledger Code is Empty in Branch Office Voucher XML.";
                                    }
                                    else
                                    {
                                        resultArgs = SaveLedgerBankAccount();
                                    }
                                }
                            }

                            if (!resultArgs.Success)
                            {
                                resultArgs.Message = "Error in ImportFDLedger. " + resultArgs.Message;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in ImportFDLedger: " + ex.ToString();
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ImportFDLedger Ended.");
            }

            //On 22/02/2018, attach name of the project and voucher detailis in the error log
            if (!resultArgs.Success && string.IsNullOrEmpty(ProblemDataDetails))
            {
                ProblemDataDetails = "FD Ledger : " + LedgerName + "<br>" +
                                      "Problem in updating Master (FD Ledger)";
            }
            return resultArgs;
        }

        /// <summary>
        /// This Method will check for the Bank Account available in Head Office.
        /// If the Bank Account is not present, Bank Account details will be inserted in Head Office.
        /// </summary>
        /// <returns></returns>
        private ResultArgs ImportFDBankAccount()
        {
            AcMEDataSynLog.WriteLog("ImportFDBankAccount Started..");
            try
            {
                bool isBankAccountExists = true;
                using (DataManager dataManager = new DataManager())
                {
                    if (dtFDBankAccount != null && dtFDBankAccount.Rows.Count > 0)
                    {
                        foreach (DataRow drFD in dtFDBankAccount.Rows)
                        {
                            AccountNumber = drFD[this.AppSchema.BankAccount.ACCOUNT_NUMBERColumn.ColumnName].ToString();

                            resultArgs = IsExists(SQLCommand.ImportVoucher.IsBankAccountExists);
                            if (resultArgs.Success)
                            {
                                isBankAccountExists = resultArgs.DataSource.Sclar.ToInteger > 0;
                                if (!isBankAccountExists)
                                {
                                    //Insert Bank Account Info
                                    AccountCode = drFD[this.AppSchema.BankAccount.ACCOUNT_CODEColumn.ColumnName].ToString();
                                    AccountHolderName = drFD[this.AppSchema.BankAccount.ACCOUNT_HOLDER_NAMEColumn.ColumnName].ToString();
                                    LedgerId = GetId(Id.Ledger);
                                    BankName = drFD[this.AppSchema.Bank.BANKColumn.ColumnName].ToString();
                                    BankId = GetId(Id.Bank);
                                    Is_FCRA_Account = this.NumberSet.ToInteger(drFD[this.AppSchema.BankAccount.IS_FCRA_ACCOUNTColumn.ColumnName].ToString());

                                    //Bank Account details validation
                                    if (LedgerId == 0)
                                    {
                                        resultArgs.Message = "Problem in Fetching FD LedgerId from Head Office.The required FD Ledger is. " + LedgerName;
                                    }
                                    else if (BankId == 0)
                                    {
                                        resultArgs.Message = "Problem in Fetching FD BankId from Head Office. The required FD Bank is. " + BankName;
                                    }
                                    else
                                    {
                                        resultArgs = SaveBankAccountDetails();
                                    }
                                }
                            }

                            if (!resultArgs.Success)
                            {
                                resultArgs.Message = "Error in ImportFDBankAccount. " + resultArgs.Message;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in ImportFDBankAccount: " + ex.ToString();
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ImportFDBankAccount Ended.");
            }

            //On 22/02/2018, attach name of the project and voucher detailis in the error log
            if (!resultArgs.Success && string.IsNullOrEmpty(ProblemDataDetails))
            {
                ProblemDataDetails = "Bank Account : " + AccountNumber + "<br>" +
                                      "Problem in updating Master (Bank Account)";
            }
            return resultArgs;
        }

        /// <summary>
        /// This Method is to Remove all the Fixed Deposit related vouchers.
        /// </summary>
        /// <returns></returns>
        private ResultArgs DeleteFDVouchers()
        {
            AcMEDataSynLog.WriteLog("DeleteFDVouchers Started..");
            try
            {
                DataTable dtTrans = new DataTable();
                if (!string.IsNullOrEmpty(ProjectIds))
                {
                    resultArgs = FetchFDTransactions();
                    if (resultArgs.Success)
                    {
                        dtTrans = resultArgs.DataSource.Table;
                        if (dtTrans != null && dtTrans.Rows.Count > 0)
                        {
                            foreach (DataRow drTrans in dtTrans.Rows)
                            {
                                VoucherId = this.NumberSet.ToInteger(drTrans[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString());

                                if (VoucherId > 0)
                                {
                                    using (BalanceSystem balance = new BalanceSystem())
                                    {
                                        resultArgs = balance.UpdateTransBalance(BranchId, LocationId, VoucherId, TransactionAction.Cancel);
                                    }

                                    if (resultArgs.Success)
                                    {
                                        resultArgs = DeleteVoucherTransaction(VoucherId);
                                        if (resultArgs.Success)
                                        {
                                            resultArgs = DeleteVoucherMasterTransaction(VoucherId);
                                        }
                                    }
                                }

                                if (!resultArgs.Success)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }

                //resultArgs = DeleteFDVoucherTransaction();
                //if (resultArgs.Success)
                //{
                //    resultArgs = DeleteFDVoucherMasterTransaction();
                //}
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("DeleteFDVouchers Ended.");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Problem in Deleting Fixed Deposit Vouchers. " + resultArgs.Message);
            }
            return resultArgs;
        }

        /// <summary>
        /// This method is used to Delete the Fixed Deposit Account details
        /// </summary>
        /// <returns></returns>
        private ResultArgs DeleteFDAccount()
        {
            AcMEDataSynLog.WriteLog("DeleteFDAccountDetails Started.");
            try
            {
                // if (dtFDAccount != null && dtFDAccount.Rows.Count > 0)
                // {
                //  foreach (DataRow drFD in dtFDAccount.Rows)
                //  {
                //      int FDAccountId = this.NumberSet.ToInteger(drFD[this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName].ToString());
                //      if (FDAccountId > 0)
                //     {
                resultArgs = DeleteFDRenewalDetails(); //FDAccountId);
                if (resultArgs.Success)
                {
                    resultArgs = DeleteFDAccountDetails();  //FDAccountId);
                }
                //     }

                //     if (!resultArgs.Success)
                //     {
                //         break;
                //     }
                //  }
                // }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("Deleting Fixed Deposit Account Details Ended.");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Problem in Deleting Fixed Deposit Account Details. " + resultArgs.Message);
            }
            return resultArgs;
        }

        /// <summary>
        /// This method is used to Save Fixed Deposit Account Details.
        /// 
        /// On 02/07/2020, For Splitting Project (Project Import and Project Export), For FD Accoutns, we have some changes for the following cases
        /// 
        /// CASE 1: If Project Splitting for FY (we have to rearrange FD Vouchers like few FDs have to be changed as FD opening, Few renewals have to be removed.
        /// CASE 2: If Project spllit just between date range. we have to take all investments and renewals alone, it should be linked with already avilable FD 
        /// 
        /// </summary>
        /// <returns></returns>
        private ResultArgs SaveFDAccountDetails()
        {
            bool IsFDAccountExists = false; //02/07/2020, for local split project
            Int32 xmlFDAccountid = 0;
            try
            {
                using (DataManager dataManager = new DataManager(VoucherImportType.Equals(ImportType.SplitProject) ? SQLCommand.ImportVoucher.InsertFDAcountForSplitProject : SQLCommand.ImportVoucher.InsertFDAccount, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    if (!VoucherImportType.Equals(ImportType.SplitProject))
                        dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                    else
                    {
                        //02/07/2020, for local split project. Check FD is already avilalbe
                        if (!IsFYSplit)
                        {
                            xmlFDAccountid = GetId(Id.FDAccount);
                            IsFDAccountExists = (xmlFDAccountid > 0);
                        }
                    }

                    dataManager.Parameters.Add(this.AppSchema.FDAccount.PROJECT_IDColumn, ProjectId, true);
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.LEDGER_IDColumn, LedgerId);
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.BANK_IDColumn, BankId);
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.BANK_LEDGER_IDColumn, BankLedgerId);
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_VOUCHER_IDColumn, VoucherId);
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_NUMBERColumn, FDAccountNumber);
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_SCHEMEColumn, FDSchemeType);
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.ACCOUNT_HOLDERColumn, FDAccountHolderName);
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.INVESTMENT_DATEColumn, CreatedOn);
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.AMOUNTColumn, FdAmount);
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.INTEREST_RATEColumn, FDInterestRate);
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.INTEREST_AMOUNTColumn, FDInterestAmount);
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.INTEREST_TYPEColumn, InterestType);
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.MATURED_ONColumn, FDMaturityDate);
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.TRANS_TYPEColumn, FDTransType);
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.TRANS_MODEColumn, FDTransMode);
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.NOTESColumn, LedgerNotes);
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.RECEIPT_NOColumn, ReceiptNo);
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_SUB_TYPESColumn, FDSubTypes);

                    dataManager.Parameters.Add(this.AppSchema.FDAccount.MF_FOLIO_NOColumn, MFFolioNo);
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.MF_SCHEME_NAMEColumn, MFSchemeName);
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.MF_NAV_PER_UNITColumn, MFNAVperUnit);
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.MF_NO_OF_UNITSColumn, MFNAVUnits);
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.MF_MODE_OF_HOLDINGColumn, MFModeOfHodling);

                    dataManager.Parameters.Add(this.AppSchema.FDAccount.STATUSColumn, FDStatus);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_IDColumn, BranchId);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.LOCATION_IDColumn, LocationId);

                    //  dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                    //On 03/07/2020 for Project split (Import and Export Data)
                    //resultArgs = dataManager.UpdateData();
                    if (VoucherImportType.Equals(ImportType.SplitProject) && IsFYSplit == false)
                    {
                        //On 02/07/2020, For Project Split for date range (Local), if already fd exists and import renewals
                        if (IsFDAccountExists)
                        {
                            resultArgs.Success = true;
                            resultArgs.RowUniqueId = xmlFDAccountid;
                        }
                        else
                        {
                            resultArgs = dataManager.UpdateData();
                        }
                    }
                    else
                    {
                        resultArgs = dataManager.UpdateData();
                    }

                    //Added by Carmel Raj M on September-03-2015
                    //Purpose :To maintain the new AccountId for all the old Account Id
                    if (VoucherImportType.Equals(ImportType.SplitProject))
                    {
                        if (FDAccountIdDetails == null)
                            FDAccountIdDetails = new Dictionary<int, int>();
                        FDAccountIdDetails.Add(FDAccountId, this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()));

                        //On 31/03/2021, If there is no vouchers but if FD opening is alone existings, let us map those ledgters --------------------
                        Int32 tmepLedgerId = LedgerId;
                        if (BankLedgerId > 0)
                        {
                            LedgerId = BankLedgerId;
                            MapProject(SQLCommand.ImportVoucher.MapProjectLedger);  //Mapping Project Ledger
                        }

                        LedgerId = tmepLedgerId;
                        MapProject(SQLCommand.ImportVoucher.MapProjectLedger);  //Mapping Project Ledger
                        //---------------------------------------------------------------------------------------------------------------------------
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Saving Fixed Deposit Account Details: " + ex.ToString();
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// This method is used to Save Fixed Deposit Renewal details of the particular FD Account.
        /// </summary>
        /// <returns></returns>
        private ResultArgs SaveFDRenewalDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.InsertFDRenewal, DataBaseTypeName, SQLAdapterType.HOSQL))
                {

                    // dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_RENEWAL_IDColumn, FDRenewalId);
                    //Modified by Carmel Raj on September-03-2015
                    //Purpose : When the import type is SplitProject FDAccountId should be latest Id
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn, VoucherImportType.Equals(ImportType.SplitProject) ? FDAccountIdDetails.FirstOrDefault(x => x.Key == FDAccountId).Value : FDAccountId);
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.INTEREST_LEDGER_IDColumn, IntrestLedgerId);
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.BANK_LEDGER_IDColumn, BankLedgerId);
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn, FDIntrestVoucherId);
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_VOUCHER_IDColumn, VoucherId);
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.RENEWAL_DATEColumn, RenewedDate);
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.MATURITY_DATEColumn, FDMaturityDate);
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.CLOSED_DATEColumn, FDClosedDate); //On 07/04/2021
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.INTEREST_AMOUNTColumn, IntrestAmount);
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.WITHDRAWAL_AMOUNTColumn, WithdrawAmount);
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.REINVESTED_AMOUNTColumn, ReInvestedAmount);
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.INTEREST_RATEColumn, FDInterestRate);
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.INTEREST_TYPEColumn, InterestType);
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.RECEIPT_NOColumn, ReceiptNo);
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.PRINCIPAL_AMOUNTColumn, PrinicipalAmount);
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.RENEWAL_TYPEColumn, RenewalType);
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.STATUSColumn, FDStatus);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_IDColumn, BranchId);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.LOCATION_IDColumn, LocationId);
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_TYPEColumn, FDType);

                    //On 08/06/2022
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.TDS_AMOUNTColumn, TDSAmount);
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.CHARGE_MODEColumn, ChargeMode);
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.CHARGE_AMOUNTColumn, ChargeAmount);
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.CHARGE_LEDGER_IDColumn, ChargeLedgerId);

                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_TRANS_MODEColumn, FDRenewalTransMode);


                    resultArgs = dataManager.UpdateData();
                }

                //11/06/2021, Update LEdger as Interest Ledger--------
                if (IntrestLedgerId > 0)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.UpdateLedgerInterestLedger, DataBaseTypeName, SQLAdapterType.HOSQL))
                    {
                        dataManager.Parameters.Add(this.AppSchema.FDRenewal.INTEREST_LEDGER_IDColumn, IntrestLedgerId);
                        resultArgs = dataManager.UpdateData();
                    }
                }
                //-----------------------------------------------------
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Saving Fixed Deposit Renewal Details: " + ex.ToString();
            }
            finally { }

            return resultArgs;
        }


        /// <summary>
        /// On 30/04/2021, To rearrange FDs based on Date Range
        /// For Import :
        ///  1. Re-arrange "Date From" will be first FY "Date From"
        ///  2. Re-arrange "Date To" will be maximum date between Last FY "Date To" and XML "Date To"
        /// Based on these date range, all FDs in xml will be re arragned which includes 
        /// FD Investments, FD opening, All Renewals (Post, Renewal), Partital Withdraw and Withdrwas
        ///  
        /// For Export with Split FY: Its fixed XML Date range 
        ///  1. Re-arrange "Date From" will be XML "Date from"
        ///  2. Re-arrange "Date To" will be XML "Date To"
        /// Based on these date range, all FDs in xml will be re arragned which includes 
        /// FD Investments, FD opening, All Renewals (Post, Renewal), Partital Withdraw and Withdrwas
        /// </summary>
        /// <param name="dsSplitedProjectsVouchers"></param>
        /// <param name="dtSplitFromDate"></param>
        /// <param name="dtSplitToDate"></param>
        /// <returns></returns>
        private ResultArgs ReArrangeFDsBasedOnDateRangeForImport(DataSet dsSplitedProjectsVouchers)
        {
            ResultArgs result = new ResultArgs();
            result.Success = true;
            try
            {
                //For General Project Import, To regarange FD Vouchers based on settings
                result = AssignXMLProjectSettingDetails(dsSplitedProjectsVouchers);
                if (result.Success)
                {
                    //On 17/03/2022, To skip rearrange FD vouchers when import vouchers from first fy
                    result = SetDateDuration(dsSplitedProjectsVouchers);
                    if (result.Success && With_FD_Vouchers && VoucherImportType == ImportType.SplitProject && !IsFYSplit && FirstFYDateFrom != FYDateFrom)
                    {
                        DateTime datefrm = GetFDRearrangeDateForImportProject(); //FirstFYDateFrom; //DateFrom;
                        DateTime dateto = DateTo; //(LastFYDateTo > DateTo) ? LastFYDateTo : DateTo;

                        if (datefrm != DateTime.MinValue)
                        {
                            if (With_FD_Vouchers)
                            { //For With FD, Rearrange FD Vouchers
                                result = SplitFDAccountsBasedOnDateRange(dsSplitedProjectsVouchers, datefrm, dateto);
                            }
                            else if (With_FD_Vouchers == false)
                            { //For Without FD, Clear All FD Vouchers
                                if (dsSplitedProjectsVouchers != null && dsSplitedProjectsVouchers.Tables[FD_ACCOUNT_TABLE_NAME] != null &&
                                    dsSplitedProjectsVouchers.Tables[FD_RENEWAL_TABLE_NAME] != null && dsSplitedProjectsVouchers.Tables[FD_VOUCHER_MASTER_TRANS_TABLE_NAME] != null)
                                {
                                    //6. If all FD Accounts are skipped, update in Dataset
                                    dsSplitedProjectsVouchers.Tables[FD_ACCOUNT_TABLE_NAME].Rows.Clear();
                                    dsSplitedProjectsVouchers.Tables[FD_RENEWAL_TABLE_NAME].Rows.Clear();
                                    dsSplitedProjectsVouchers.Tables[FD_VOUCHER_MASTER_TRANS_TABLE_NAME].Rows.Clear();
                                    dsSplitedProjectsVouchers.Tables[FD_VOUCHER_TRANS_TABLE_NAME].Rows.Clear();
                                }
                            }
                        }
                        else
                        {
                            result.Message = "Select Project Data XML is old file. Could not Rearrange FD Vouchers";
                        }
                    }
                }
            }
            catch (Exception err)
            {
                result.Message = err.Message;
                AcMEDataSynLog.WriteLog("Error in FD ReArrangeFDsBasedOnDateRangeForImport: " + err.ToString());
            }
            return result;
        }

        /// <summary>
        /// On 29/06/2020,
        /// While Project Import/Expot - Existing logic, we have taken all FDs without date from splitting condition.
        /// so this method is called after export or split project.
        /// This mehod is used to check all Exported FDs and renewals are with in splitted Date range
        /// 
        /// This method will be called only if date from is year from
        /// 
        /// 1. Check all Renewals and update status=0 if renewal date less than splitted date from
        /// 2. If all renewals which are deleted, change fd status is inactive
        /// 3. FD type is invested before splitted date from and If few FD renewals are based on splitted date from
        ///  Change FD type and flag as Opening, remove fd voucherid, real balance and maturity date

        /// </summary>
        /// <param name="dsSplitedProjectsVouchers"></param>
        public ResultArgs SplitFDAccountsBasedOnDateRange(DataSet dsSplitedProjectsVouchers, DateTime dtSplitFromDate, DateTime dtSplitToDate)
        {
            ResultArgs result = new ResultArgs();
            result.Success = true;
            try
            {

                if (dsSplitedProjectsVouchers != null && dsSplitedProjectsVouchers.Tables[FD_ACCOUNT_TABLE_NAME] != null &&
                   dsSplitedProjectsVouchers.Tables[FD_RENEWAL_TABLE_NAME] != null && dsSplitedProjectsVouchers.Tables[FD_VOUCHER_MASTER_TRANS_TABLE_NAME] != null)
                {
                    DataTable dtFDAccount = dsSplitedProjectsVouchers.Tables[FD_ACCOUNT_TABLE_NAME];
                    DataTable dtFDRenewals = dsSplitedProjectsVouchers.Tables[FD_RENEWAL_TABLE_NAME];
                    DataTable dtFDVoucherMaster = dsSplitedProjectsVouchers.Tables[FD_VOUCHER_MASTER_TRANS_TABLE_NAME];
                    DataTable dtFDRenewalsMaturityDate = dtFDRenewals.DefaultView.ToTable();


                    //31/03/2021, to check *************************************
                    // take FDs only invested before splited period date to
                    dtFDAccount.DefaultView.RowFilter = "(" + AppSchema.FDAccount.TRANS_TYPEColumn.ColumnName + "= '" + FDTypes.OP.ToString() + "') OR " +
                                                        "(" + AppSchema.FDAccount.TRANS_TYPEColumn.ColumnName + " = '" + FDTypes.IN.ToString() + "' AND " +
                                                        AppSchema.FDAccount.INVESTMENT_DATEColumn.ColumnName + " <= '" + dtSplitToDate + "')";
                    dtFDAccount = dtFDAccount.DefaultView.ToTable();
                    //**********************************************************
                    result = CheckClosedFDsBeforeSplittedDateFrom(ref dtFDAccount, ref dtFDRenewals, dtSplitFromDate, dtSplitToDate);
                    if (result.Success)
                    {
                        //Take all FD Invested alone with in date range and its renewals 
                        string statusexpression = AppSchema.FDAccount.STATUSColumn.ColumnName + "=" + (Int32)Status.Active;

                        dtFDAccount.DefaultView.RowFilter = statusexpression;
                        dtFDAccount = dtFDAccount.DefaultView.ToTable();

                        if (dtFDAccount.Rows.Count > 0)
                        {
                            statusexpression = "STATUS=1";
                            dtFDVoucherMaster.DefaultView.RowFilter = statusexpression;
                            dtFDVoucherMaster = dtFDVoucherMaster.DefaultView.ToTable();

                            //Change FD_Voucher_Master_Trans Status is inactive for all voucher which are less thatn splited date from
                            statusexpression = "IIF(" + AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName + "< '" + dtSplitFromDate + "' OR " +
                                                               AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName + "> '" + dtSplitToDate + "', 0, 1)";
                            DataColumn dcVoucherStatus = new DataColumn("STATUS1", typeof(System.Int32), statusexpression);
                            dcVoucherStatus.DefaultValue = 1; //by default Active 
                            dtFDVoucherMaster.Columns.Add(dcVoucherStatus);

                            dtFDRenewals.DefaultView.RowFilter = "STATUS=1";
                            dtFDRenewals = dtFDRenewals.DefaultView.ToTable();
                            //1. Check all Renewals and update status=0 if renewal date less than splitted date from
                            statusexpression = "IIF(" + AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName + "< '" + dtSplitFromDate + "', 0, 1)";
                            DataColumn dcFDStatus = new DataColumn("STATUS1", typeof(System.Int32), statusexpression);
                            dcFDStatus.DefaultValue = 1; //by default Active 
                            dtFDRenewals.Columns.Add(dcFDStatus);

                            foreach (DataRow drFDRow in dtFDAccount.Rows)
                            {
                                //Get FD Account Details 
                                Int32 FDAccountId = NumberSet.ToInteger(drFDRow[AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName].ToString());
                                string FDType = drFDRow[AppSchema.FDAccount.TRANS_TYPEColumn.ColumnName].ToString();
                                Int32 FDVoucherId = NumberSet.ToInteger(drFDRow[AppSchema.FDAccount.FD_VOUCHER_IDColumn.ColumnName].ToString());
                                DateTime FDInvestedDate = DateSet.ToDate(drFDRow[AppSchema.FDAccount.INVESTMENT_DATEColumn.ColumnName].ToString(), false);
                                DateTime FDRecentMaturityDate = DateSet.ToDate(drFDRow[AppSchema.FDAccount.MATURED_ONColumn.ColumnName].ToString(), false);
                                double FDInvestmentAmount = NumberSet.ToDouble(drFDRow[AppSchema.FDAccount.AMOUNTColumn.ColumnName].ToString());
                                DateTime FDRecentRenewedDate = FDInvestedDate;

                                double TotalFDRecentBalance = FDInvestmentAmount;


                                //Get FD Renewal Details for selected FD Account
                                dtFDRenewals.DefaultView.RowFilter = AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName + " = " + FDAccountId;
                                Int32 NumberofRenewals = dtFDRenewals.DefaultView.Count;
                                bool IsRenewalsDeleted = false;
                                bool AllRenewalsDeleted = false;

                                if (NumberofRenewals > 0)
                                {
                                    //Get Recent valid Renewal Date Date
                                    dtFDRenewals.DefaultView.RowFilter = AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName + " = " + FDAccountId + " AND STATUS1 = 1";
                                    dtFDRenewals.DefaultView.Sort = AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName;
                                    AllRenewalsDeleted = (dtFDRenewals.DefaultView.Count == 0);
                                    IsRenewalsDeleted = (NumberofRenewals != dtFDRenewals.DefaultView.Count);

                                    //Get Current FD Account balance as on splitted date from
                                    //Sum of Accumulated interest amount with all renewals date less than date splitted date from
                                    //Sum of paritical withdrwal amount and withdrwalamount with all renewals date less than date splitted date from
                                    //Sum of Re invested amount with all renewals date less than date splitted date from
                                    dtFDRenewals.DefaultView.RowFilter = string.Empty;
                                    string commoncriteria = AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName + " = " + FDAccountId + " AND STATUS1 = 0";
                                    double AccInterestAmount = NumberSet.ToDouble(dtFDRenewals.DefaultView.Table.Compute("SUM(" + AppSchema.FDRenewal.INTEREST_AMOUNTColumn.ColumnName + ")",
                                                        commoncriteria + " AND " + AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName + "= '" + FDRenewalTypes.ACI.ToString() + "'").ToString());
                                    double WithdrwalAmount = NumberSet.ToDouble(dtFDRenewals.DefaultView.Table.Compute("SUM(" + AppSchema.FDRenewal.WITHDRAWAL_AMOUNTColumn.ColumnName + ")",
                                                        commoncriteria + " AND " + AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName + " IN ('" + FDRenewalTypes.WDI.ToString() + "', '" + FDRenewalTypes.PWD.ToString() + "')").ToString());

                                    double ReInvested = NumberSet.ToDouble(dtFDRenewals.DefaultView.Table.Compute("SUM(" + AppSchema.FDRenewal.REINVESTED_AMOUNTColumn.ColumnName + ")",
                                                        commoncriteria + " AND " + AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName + " IN ('" + FDRenewalTypes.RIN.ToString() + "')").ToString());

                                    TotalFDRecentBalance = (FDInvestmentAmount + AccInterestAmount + ReInvested) - WithdrwalAmount;

                                    //To get Recent Renewal date and maturity date, If renewals skipped
                                    if (IsRenewalsDeleted)
                                    //if (dtFDRenewals.DefaultView.Count > 0)
                                    {
                                        dtFDRenewals.DefaultView.RowFilter = commoncriteria;
                                        dtFDRenewals.DefaultView.Sort = AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName + " DESC";
                                        if (dtFDRenewals.DefaultView.Count > 0)
                                        {
                                            FDRecentRenewedDate = DateSet.ToDate(dtFDRenewals.DefaultView[0][AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName].ToString(), false);
                                            FDRecentMaturityDate = DateSet.ToDate(dtFDRenewals.DefaultView[0][AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName].ToString(), false);

                                            //On 23/02/2024, to have proper maturity date
                                            string fdfilter = AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName + " = " + FDAccountId + " AND " +
                                                    AppSchema.FDAccount.STATUSColumn.ColumnName + " = 1";
                                            DateTime realMaturityDate = DateSet.ToDate(dtFDRenewalsMaturityDate.Compute("MAX(" + AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName + ")", fdfilter).ToString(), false);
                                            if (realMaturityDate > FDRecentMaturityDate)
                                            {
                                                FDRecentMaturityDate = realMaturityDate;
                                            }
                                        }
                                    }
                                }
                                dtFDRenewals.DefaultView.RowFilter = string.Empty;
                                dtFDRenewals.DefaultView.Sort = string.Empty;

                                if (FDType.ToUpper() == FDTypes.IN.ToString() && IsRenewalsDeleted && (FDInvestedDate < FDRecentRenewedDate || FDInvestedDate < dtSplitFromDate))
                                {
                                    //3. FD type is invested before splitted date from and If few FD renewals are based on splitted date from
                                    //Change FD type and flag as Opening, remove fd voucherid, real balance and maturity date
                                    drFDRow[AppSchema.FDAccount.INVESTMENT_DATEColumn.ColumnName] = FDRecentRenewedDate;
                                    drFDRow[AppSchema.FDAccount.TRANS_TYPEColumn.ColumnName] = FDTypes.OP.ToString().ToUpper();
                                    drFDRow[AppSchema.FDAccount.FD_SUB_TYPESColumn.ColumnName] = "FD-O";
                                    drFDRow[AppSchema.FDAccount.FD_VOUCHER_IDColumn.ColumnName] = 0;
                                    drFDRow["BANK_LEDGER"] = string.Empty; // On 05/02/2024, for FD opening let us empty bank ledger
                                    drFDRow[AppSchema.FDAccount.MATURED_ONColumn.ColumnName] = FDRecentMaturityDate;
                                    drFDRow[AppSchema.FDAccount.AMOUNTColumn.ColumnName] = TotalFDRecentBalance;
                                    dtFDAccount.AcceptChanges();
                                }
                                else if (FDType.ToUpper() == FDTypes.IN.ToString() && !IsRenewalsDeleted && FDInvestedDate < dtSplitFromDate)
                                {
                                    //4. FD type is opening If few FD renewals deleted based on splitted date from
                                    //change real balance and maturity date
                                    drFDRow[AppSchema.FDAccount.INVESTMENT_DATEColumn.ColumnName] = FDRecentRenewedDate;
                                    drFDRow[AppSchema.FDAccount.TRANS_TYPEColumn.ColumnName] = FDTypes.OP.ToString().ToUpper();
                                    drFDRow[AppSchema.FDAccount.FD_SUB_TYPESColumn.ColumnName] = "FD-O";
                                    drFDRow[AppSchema.FDAccount.FD_VOUCHER_IDColumn.ColumnName] = 0;
                                    drFDRow["BANK_LEDGER"] = string.Empty; // On 05/02/2024, for FD opening let us empty bank ledger
                                    drFDRow[AppSchema.FDAccount.MATURED_ONColumn.ColumnName] = FDRecentMaturityDate;
                                    drFDRow[AppSchema.FDAccount.AMOUNTColumn.ColumnName] = TotalFDRecentBalance;
                                    dtFDAccount.AcceptChanges();
                                }
                                else if (FDType.ToUpper() == FDTypes.OP.ToString()) //&& IsRenewalsDeleted
                                {
                                    //5. FD type is opening If few FD renewals deleted based on splitted date from
                                    //change real balance and maturity date
                                    drFDRow[AppSchema.FDAccount.AMOUNTColumn.ColumnName] = TotalFDRecentBalance;
                                    drFDRow[AppSchema.FDAccount.MATURED_ONColumn.ColumnName] = FDRecentMaturityDate;
                                    dtFDAccount.AcceptChanges();
                                }
                            }

                            //For FD Account
                            dtFDAccount.DefaultView.RowFilter = "STATUS=1";
                            dtFDAccount = dtFDAccount.DefaultView.ToTable();
                            dsSplitedProjectsVouchers.Tables.Remove(FD_ACCOUNT_TABLE_NAME);
                            dsSplitedProjectsVouchers.Tables.Add(dtFDAccount);

                            //For FD Voucher Master Trans
                            dtFDVoucherMaster.Columns.Remove("STATUS");
                            dtFDVoucherMaster.Columns["STATUS1"].ColumnName = "STATUS";
                            dtFDVoucherMaster.DefaultView.RowFilter = "STATUS=1";
                            dtFDVoucherMaster = dtFDVoucherMaster.DefaultView.ToTable();
                            dsSplitedProjectsVouchers.Tables.Remove(FD_VOUCHER_MASTER_TRANS_TABLE_NAME);
                            dsSplitedProjectsVouchers.Tables.Add(dtFDVoucherMaster);

                            //For FD Renewals Status
                            dtFDRenewals.Columns.Remove("STATUS");
                            dtFDRenewals.Columns["STATUS1"].ColumnName = "STATUS";
                            //Attach Renewals which are less that date to
                            dtFDRenewals.DefaultView.RowFilter = "STATUS=1 AND " + AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName + "<= '" + dtSplitToDate + "'";
                            dtFDRenewals = dtFDRenewals.DefaultView.ToTable();
                            dsSplitedProjectsVouchers.Tables.Remove(FD_RENEWAL_TABLE_NAME);
                            dsSplitedProjectsVouchers.Tables.Add(dtFDRenewals);
                            result.Success = true;
                        }
                        else
                        {
                            //6. If all FD Accounts are skipped, update in Dataset
                            dsSplitedProjectsVouchers.Tables[FD_ACCOUNT_TABLE_NAME].Rows.Clear();
                            dsSplitedProjectsVouchers.Tables[FD_RENEWAL_TABLE_NAME].Rows.Clear();
                            dsSplitedProjectsVouchers.Tables[FD_VOUCHER_MASTER_TRANS_TABLE_NAME].Rows.Clear();
                            dsSplitedProjectsVouchers.Tables[FD_VOUCHER_TRANS_TABLE_NAME].Rows.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        /// <param name="dtFDAccounts"></param>
        /// <param name="dtFDAllRenewals"></param>
        private ResultArgs CheckClosedFDsBeforeSplittedDateFrom(ref DataTable dtFDAccounts, ref DataTable dtFDAllRenewals, DateTime dtSplitFromDate, DateTime dtSplitToDate)
        {
            ResultArgs result = new ResultArgs();
            try
            {
                //dtFDAccounts.DefaultView.RowFilter = vouchersystem.AppSchema.FDAccount.STATUSColumn.ColumnName + " = 1";
                //dtFDAccounts = dtFDAccounts.DefaultView.ToTable();

                //dtFDAllRenewals.DefaultView.RowFilter = vouchersystem.AppSchema.FDAccount.STATUSColumn.ColumnName + " = 1";
                //dtFDAllRenewals = dtFDAllRenewals.DefaultView.ToTable();

                foreach (DataRow drFDRow in dtFDAccounts.Rows)
                {
                    Int32 FDAccountId = NumberSet.ToInteger(drFDRow[AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName].ToString());
                    string fdnumber = drFDRow[AppSchema.FDAccount.FD_ACCOUNT_NUMBERColumn.ColumnName].ToString();
                    string FDType = drFDRow[AppSchema.FDAccount.TRANS_TYPEColumn.ColumnName].ToString();
                    Int32 FDVoucherId = NumberSet.ToInteger(drFDRow[AppSchema.FDAccount.FD_VOUCHER_IDColumn.ColumnName].ToString());
                    DateTime FDInvestedDate = DateSet.ToDate(drFDRow[AppSchema.FDAccount.INVESTMENT_DATEColumn.ColumnName].ToString(), false);
                    double FDInvestmentAmount = NumberSet.ToDouble(drFDRow[AppSchema.FDAccount.AMOUNTColumn.ColumnName].ToString());
                    DateTime FDRecentRenewedDate = FDInvestedDate;

                    //1. Check current FDs had withdrwan fully
                    dtFDAllRenewals.DefaultView.RowFilter = AppSchema.FDAccount.STATUSColumn.ColumnName + "=" + (Int32)Status.Active +
                                                          " AND " + AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName + "=" + FDAccountId +
                                                          " AND " + AppSchema.FDRenewal.FD_TYPEColumn.ColumnName + "= '" + FDTypes.WD + "'" +
                                                          " AND " + AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName + "= '" + FDRenewalTypes.WDI + "'" +
                                                          " AND " + AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName + "< '" + dtSplitFromDate.ToShortDateString() + "'";

                    //2. If Current FD withdrwan fully, make that FD is in active
                    if (dtFDAllRenewals.DefaultView.Count > 0)
                    {
                        drFDRow.BeginEdit();
                        drFDRow[AppSchema.FDAccount.STATUSColumn.ColumnName] = (Int32)Status.Inactive;
                        drFDRow.EndEdit();
                        dtFDAccounts.AcceptChanges();

                        //On 06/05/2021, To avoid invalid FDs
                        //If Particual FD is in active, let all its renewals too inactive -----------------------
                        dtFDAllRenewals.DefaultView.RowFilter = string.Empty;
                        dtFDAllRenewals.DefaultView.RowFilter = AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName + "=" + FDAccountId;
                        foreach (DataRowView drvFDRenewal in dtFDAllRenewals.DefaultView)
                        {
                            drvFDRenewal.BeginEdit();
                            drvFDRenewal[AppSchema.FDAccount.STATUSColumn.ColumnName] = (Int32)Status.Inactive;
                            drvFDRenewal.EndEdit();
                        }
                        dtFDAllRenewals.AcceptChanges();
                        //-----------------------------------------------------------------------------------------
                    }
                    dtFDAllRenewals.DefaultView.RowFilter = string.Empty;
                }
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            finally
            {
                dtFDAccounts.DefaultView.RowFilter = string.Empty;
                dtFDAllRenewals.DefaultView.RowFilter = string.Empty;
            }

            return result;
        }

        /// <summary>
        /// On 14/05/2021, This method is used to get FD rearrange date for project import
        /// 
        ///Finding Rearrange date for FDs
        ///If the Project does not exist, 
        /// 1. FDs will be rearranged based on XML FY "Date from" of FY Year From. FD opening ledger balance will be on Books Begin on Target Database.
        ///If the Project exist,  
        /// 2. If FDs or Vouchers available for project, rearrange FDs based on first voucher date of FY "Year From". 
        ///    FD opening ledger balance will be on Books Begin on Target Database.
        /// </summary>
        /// <returns></returns>
        private DateTime GetFDRearrangeDateForImportProject()
        {
            DateTime rtn = FirstFYDateFrom;

            try
            {
                //1. If there Project is not available, FDs will be rearranged based on XML FY "Date from" of FY Year From. 
                //FD opening ledger balance will be on Books Begin on Target Database.
                if (FYProjectId == 0 || !FYProjectVoucherExistsInDB)
                {
                    rtn = FYYearFrom;
                }
                else if (With_FD_Vouchers)
                {
                    //If Vouchers availalbe in target database, get first voucher date's FY Year from
                    DateTime FYYearFromVoucherDateExisting = DateSet.GetFinancialYearByDate(FYProjectFirstVoucherDateInDB, this.YearFrom);

                    /// 2. If FDs or Vouchers available for project, rearrange FDs based on first voucher date of FY "Year From". 
                    ///    FD opening ledger balance will be on Books Begin on Target Database.
                    if (FYProjectVoucherExistsInDB || FYProjectFDVoucherExistsInDB)
                    {
                        rtn = FYYearFromVoucherDateExisting;
                    }
                }
            }
            catch (Exception err)
            {
                rtn = FirstFYDateFrom;
                string msg = err.Message;
                MessageRender.ShowMessage(msg);
            }
            return rtn;
        }
        #endregion

        #region Budget
        private ResultArgs ImportBudgetModule()
        {
            if (With_Budget)
            {
                resultArgs = ImportStatisticsType(); //Import Statistics Types
                if (resultArgs.Success)
                {
                    resultArgs = ImportBudgetProjectLedger(); //Mapping Project Ledger and Mapping Budget Project Ledger
                    if (resultArgs.Success)
                    {
                        resultArgs = DeleteBudgetModule();
                        if (resultArgs.Success)
                        {
                            foreach (DataRow drBudget in dtBudgetMaster.Rows)
                            {
                                int budgetidsource = NumberSet.ToInteger(drBudget[this.AppSchema.Budget.BUDGET_IDColumn.ColumnName].ToString());
                                BudgetName = drBudget[this.AppSchema.Budget.BUDGET_NAMEColumn.ColumnName].ToString();
                                BudgetDateFrom = DateSet.ToDate(drBudget[this.AppSchema.Budget.DATE_FROMColumn.ColumnName].ToString(), false);
                                BudgetDateTo = DateSet.ToDate(drBudget[this.AppSchema.Budget.DATE_TOColumn.ColumnName].ToString(), false);
                                BudgetTypeId = NumberSet.ToInteger(drBudget[this.AppSchema.Budget.BUDGET_TYPE_IDColumn.ColumnName].ToString());

                                //Get projects (from brach db or portal db based on project name)
                                //1. Get projects ids (from brach db or portal db based on project name)
                                dtBudgetProject.DefaultView.RowFilter = this.AppSchema.Budget.BUDGET_IDColumn.ColumnName + "=" + budgetidsource;
                                dtProject = dtBudgetProject.DefaultView.ToTable();
                                dtBudgetProject.DefaultView.RowFilter = string.Empty;
                                resultArgs = GetBudgetProjectIds(); //get actual projectid (brach/portal db) by giving project name 
                                if (resultArgs.Success && resultArgs.ReturnValue != null)
                                {
                                    ProjectIds = resultArgs.ReturnValue.ToString();

                                    //2. Insert/Update Budget master in branch or portal db based on update master calling (Branch/Portal)
                                    resultArgs = UpdateBudgetMaster(drBudget, 0);
                                    if (resultArgs.Success)
                                    {
                                        Int32 newbudgetid = NumberSet.ToInteger(resultArgs.ReturnValue.ToString());
                                        dtBudgetProject.DefaultView.RowFilter = this.AppSchema.Budget.BUDGET_IDColumn.ColumnName + "=" + budgetidsource;
                                        //3. Insert Budget projects in branch or portal db based on update master calling (Branch/Portal)
                                        resultArgs = UpdateBudgetProject(dtBudgetProject.DefaultView.ToTable(), newbudgetid);
                                        dtBudgetProject.DefaultView.RowFilter = string.Empty;
                                        if (resultArgs.Success)
                                        {
                                            //4. Insert Budget Ledgers in branch or portal db based on update master calling (Branch/Portal)
                                            dtBudgetLedger.DefaultView.RowFilter = this.AppSchema.Budget.BUDGET_IDColumn.ColumnName + "=" + budgetidsource;
                                            resultArgs = UpdateBudgetLedger(dtBudgetLedger.DefaultView.ToTable(), newbudgetid);
                                            dtBudgetLedger.DefaultView.RowFilter = string.Empty;

                                            //5. Budget Statistics Details
                                            if (resultArgs.Success)
                                            {
                                                dtBudgetStatisticsDetails.DefaultView.RowFilter = this.AppSchema.Budget.BUDGET_IDColumn.ColumnName + "=" + budgetidsource;
                                                resultArgs = UpdateBudgetStatisticalsDetails(dtBudgetStatisticsDetails.DefaultView.ToTable(), newbudgetid);
                                                dtBudgetStatisticsDetails.DefaultView.RowFilter = string.Empty;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    resultArgs.Message = "Budget Project(s) are not available in current Branch";
                                }

                                if (!resultArgs.Success)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs DeleteBudgetModule()
        {
            try
            {
                string ExistingBudgets = string.Empty;

                resultArgs = GetBudgetIdForSplitProject();
                if (resultArgs.Success)
                {
                    DataTable dtBudgetIds = resultArgs.DataSource.Table;
                    if (dtBudgetIds.Rows.Count > 0)
                    {
                        ExistingBudgets = (dtBudgetIds.Rows[0]["BUDGET_IDs"] != null ? dtBudgetIds.Rows[0]["BUDGET_IDs"].ToString() : string.Empty);
                        if (!string.IsNullOrEmpty(ExistingBudgets))
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.DeleteBudgetForSplitProject, DataBaseTypeName, SQLAdapterType.HOSQL))
                            {
                                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, ExistingBudgets);
                                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                                resultArgs = dataManager.UpdateData();
                                if (!resultArgs.Success)
                                {
                                    AcMEDataSynLog.WriteLog("Error in Deleting DeleteBudgetModule Details. " + resultArgs.Message);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Deleting Budget Module : " + ex.ToString();
            }
            finally { }
            return resultArgs;
        }

        private ResultArgs GetBudgetIdForSplitProject()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.GetBudgetIdForSplitProject, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.Budget.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(AppSchema.Budget.DATE_FROMColumn, YearFrom);
                    dataManager.Parameters.Add(AppSchema.Budget.DATE_TOColumn, YearTo);
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, new DateTime(DateSet.ToDate(YearFrom, false).Year, 1, 1));
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, new DateTime(DateSet.ToDate(YearTo, false).Year, 12, 31));
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (!resultArgs.Success)
                    {
                        AcMEDataSynLog.WriteLog("Error in Fetch Budget Module Details. " + resultArgs.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Deleting Fixed Deposit Renewal Details: " + ex.ToString();
            }
            finally { }
            return resultArgs;
        }

        private ResultArgs ImportStatisticsType()
        {
            try
            {
                if (dtBudgetStatisticsType != null && dtBudgetStatisticsType.Rows.Count > 0)
                {
                    foreach (DataRow drBudgetStatisticsType in dtBudgetStatisticsType.Rows)
                    {
                        StatisticsTypeName = drBudgetStatisticsType[this.AppSchema.StatisticsType.STATISTICS_TYPEColumn.ColumnName].ToString();
                        StatisticsTypeId = GetId(Id.StatisticsType);    // Get the Statistics Type Id

                        //Check Statistics Type is not found, insert into db
                        if (StatisticsTypeId == 0)
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.InsertStatisticsType, DataBaseTypeName, SQLAdapterType.HOSQL))
                            {
                                //dataManager.Parameters.Add(this.AppSchema.StatisticsType.STATISTICS_TYPE_IDColumn, StatisticsTypeId, true);
                                dataManager.Parameters.Add(this.AppSchema.StatisticsType.STATISTICS_TYPEColumn, StatisticsTypeName);
                                resultArgs = dataManager.UpdateData();
                            }
                        }

                        if (!resultArgs.Success)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Saving ImportStatisticsType : " + ex.ToString();
            }
            return resultArgs;
        }

        private ResultArgs ImportBudgetProjectLedger()
        {
            try
            {
                if (dtBudgetProjectLedger != null && dtBudgetProjectLedger.Rows.Count > 0)
                {
                    foreach (DataRow drBudgetProjectLedger in dtBudgetProjectLedger.Rows)
                    {
                        LedgerName = drBudgetProjectLedger[this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                        LedgerId = GetId(Id.Ledger);    // Get Ledger Id

                        //Check Leger Id is available not not
                        if (LedgerId == 0)
                        {
                            //resultArgs.Message = VoucherImportType.Equals(ImportType.SplitProject) ? String.Format("Ledger Name is Empty while mapping Project Ledger/Budget Project Ledger. The Required Ledger is '{0}'", LedgerName) :
                            //            "Ledger Name is Empty in Head Office while mapping Project Ledger/Budget Project Ledger.<br/> The Required Ledger in Portal is:<br/><b>" + LedgerName + "</b>";
                        }
                        else
                        {
                            resultArgs = MapProject(SQLCommand.ImportVoucher.MapProjectLedger);
                            if (resultArgs.Success)
                            {
                                resultArgs = MapProject(SQLCommand.ImportVoucher.MapProjectBudgetLedger);
                            }
                        }

                        if (!resultArgs.Success)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Saving ImportStatisticsType : " + ex.ToString();
            }
            return resultArgs;
        }

        #endregion

        #region GST Invoice
        private ResultArgs ImportGSTInvoiceModule()
        {
            if (FYProjectGSTExistsInXML)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.DeleteGSTInvoicesByProjectSplit, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.ProjectImportExport.DATE_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(this.AppSchema.ProjectImportExport.DATE_TOColumn, DateTo);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                    resultArgs = dataManager.UpdateData();
                    if (resultArgs.Success)
                    {
                        resultArgs = SaveGSTInvoiceMaster();
                    }
                    else
                    {
                        AcMEDataSynLog.WriteLog("Error in Deleting GST Invoice Details Details. " + resultArgs.Message);
                    }
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// On 05/02/2023, To update GST Invoice details
        /// </summary>
        /// <param name="dm"></param>
        /// <returns></returns>
        private ResultArgs SaveGSTInvoiceMaster()
        {
            Int32 GSTInvoiceId = 0;
            Int32 NewGSTInvoiceId = 0;
            double totalamount = 0;
            double cgst = 0;
            double sgst = 0;
            double igst = 0;

            try
            {
                if (resultArgs.Success)
                {
                    if (dtGSTInvoiceMaster != null && dtGSTInvoiceMaster.Rows.Count > 0)
                    {
                        DataTable dtgstinvoicemaster = dtGSTInvoiceMaster.DefaultView.ToTable();
                        dtgstinvoicemaster.DefaultView.RowFilter = this.AppSchema.GSTInvoiceMaster.BOOKING_VOUCHER_IDColumn.ColumnName + " = " + VoucherId;

                        if (dtgstinvoicemaster.DefaultView.Count > 0)
                        {
                            NewGSTInvoiceId = 0;
                            DataRow drGSTInvoiceMaster = dtgstinvoicemaster.DefaultView[0].Row;
                            GSTInvoiceId = this.NumberSet.ToInteger(drGSTInvoiceMaster[this.AppSchema.GSTInvoiceMaster.GST_INVOICE_IDColumn.ColumnName].ToString());

                            Vendor = drGSTInvoiceMaster[this.AppSchema.Vendors.VENDORColumn.ColumnName].ToString();
                            VendorId = GetId(Id.Vendor);
                            if (VendorId == 0)
                            {
                                resultArgs.Message = String.Format("GST Vendor is Empty while GST Invoice Details.The Required Vendor is {0}'" + Vendor + "'");
                            }
                            if (resultArgs.Success)
                            {
                                totalamount = this.NumberSet.ToDouble(drGSTInvoiceMaster[this.AppSchema.GSTInvoiceMaster.TOTAL_AMOUNTColumn.ColumnName].ToString());
                                cgst = this.NumberSet.ToDouble(drGSTInvoiceMaster[this.AppSchema.GSTInvoiceMaster.TOTAL_CGST_AMOUNTColumn.ColumnName].ToString());
                                sgst = this.NumberSet.ToDouble(drGSTInvoiceMaster[this.AppSchema.GSTInvoiceMaster.TOTAL_SGST_AMOUNTColumn.ColumnName].ToString());
                                igst = this.NumberSet.ToDouble(drGSTInvoiceMaster[this.AppSchema.GSTInvoiceMaster.TOTAL_IGST_AMOUNTColumn.ColumnName].ToString());
                                GSTMasterBillingCountryId = GSTMasterBillingStateId = GSTMasterShippingCountryId = GSTMasterShippingStateId = 0;
                                CountryName = StateName = string.Empty;
                                CountryName = drGSTInvoiceMaster[AppSchema.GSTInvoiceMaster.BILLING_COUNTRYColumn.ColumnName].ToString();
                                GSTMasterBillingCountryId = GetId(Id.Country);
                                if (GSTMasterBillingCountryId > 0)
                                {
                                    StateName = drGSTInvoiceMaster[AppSchema.GSTInvoiceMaster.BILLING_STATE_NAMEColumn.ColumnName].ToString();
                                    GSTMasterBillingStateId = GetId(Id.State);
                                }

                                CountryName = StateName = string.Empty;
                                CountryName = drGSTInvoiceMaster[AppSchema.GSTInvoiceMaster.SHIPPING_COUNTRYColumn.ColumnName].ToString();
                                GSTMasterShippingCountryId = GetId(Id.Country);
                                if (GSTMasterShippingCountryId > 0)
                                {
                                    StateName = drGSTInvoiceMaster[AppSchema.GSTInvoiceMaster.SHIPPING_STATE_NAMEColumn.ColumnName].ToString();
                                    GSTMasterShippingStateId = GetId(Id.State);
                                }

                                //using (DataManager dataManager = new DataManager((gstinvoiceid == 0) ? SQLCommand.VoucherMaster.SaveGSTInvoiceMasterDetails : SQLCommand.VoucherMaster.UpdateGSTInvoiceMasterDetails))
                                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.InsertGSTInvoiceMaster, DataBaseTypeName, SQLAdapterType.HOSQL))
                                {
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.GST_INVOICE_IDColumn, GSTInvoiceId, true);
                                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.GST_VENDOR_IDColumn, VendorId);
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.GST_VENDOR_INVOICE_NOColumn, drGSTInvoiceMaster[AppSchema.GSTInvoiceMaster.GST_VENDOR_INVOICE_NOColumn.ColumnName].ToString());
                                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.GST_VENDOR_INVOICE_DATEColumn, drGSTInvoiceMaster[AppSchema.GSTInvoiceMaster.GST_VENDOR_INVOICE_DATEColumn.ColumnName].ToString(), false);
                                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.GST_VENDOR_INVOICE_TYPEColumn, NumberSet.ToInteger(drGSTInvoiceMaster[AppSchema.GSTInvoiceMaster.GST_VENDOR_INVOICE_TYPEColumn.ColumnName].ToString()));
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.TRANSPORT_MODEColumn, drGSTInvoiceMaster[AppSchema.GSTInvoiceMaster.TRANSPORT_MODEColumn.ColumnName].ToString());
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.VEHICLE_NUMBERColumn, drGSTInvoiceMaster[AppSchema.GSTInvoiceMaster.VEHICLE_NUMBERColumn.ColumnName].ToString());
                                    if (string.IsNullOrEmpty(drGSTInvoiceMaster[AppSchema.GSTInvoiceMaster.SUPPLY_DATEColumn.ColumnName].ToString())
                                        || DateSet.ToDate(drGSTInvoiceMaster[AppSchema.GSTInvoiceMaster.SUPPLY_DATEColumn.ColumnName].ToString(), false) == DateTime.MinValue)
                                        dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.SUPPLY_DATEColumn, DBNull.Value);
                                    else
                                        dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.SUPPLY_DATEColumn, drGSTInvoiceMaster[AppSchema.GSTInvoiceMaster.SUPPLY_DATEColumn.ColumnName].ToString());

                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.SUPPLY_PLACEColumn, drGSTInvoiceMaster[AppSchema.GSTInvoiceMaster.SUPPLY_PLACEColumn.ColumnName].ToString());
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.BILLING_NAMEColumn, drGSTInvoiceMaster[AppSchema.GSTInvoiceMaster.BILLING_NAMEColumn.ColumnName].ToString());
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.BILLING_GST_NOColumn, drGSTInvoiceMaster[AppSchema.GSTInvoiceMaster.BILLING_GST_NOColumn.ColumnName].ToString());
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.BILLING_ADDRESSColumn, drGSTInvoiceMaster[AppSchema.GSTInvoiceMaster.BILLING_ADDRESSColumn.ColumnName].ToString());
                                    //dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.BILLING_STATE_IDColumn, NumberSet.ToInteger(drGSTInvoiceMaster[AppSchema.GSTInvoiceMaster.BILLING_STATE_IDColumn.ColumnName].ToString()));
                                    //dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.BILLING_COUNTRY_IDColumn, NumberSet.ToInteger(drGSTInvoiceMaster[AppSchema.GSTInvoiceMaster.BILLING_COUNTRY_IDColumn.ColumnName].ToString()));
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.BILLING_STATE_IDColumn, GSTMasterBillingStateId);
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.BILLING_COUNTRY_IDColumn, GSTMasterBillingCountryId);
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.SHIPPING_NAMEColumn, drGSTInvoiceMaster[AppSchema.GSTInvoiceMaster.SHIPPING_NAMEColumn.ColumnName].ToString());
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.SHIPPING_GST_NOColumn, drGSTInvoiceMaster[AppSchema.GSTInvoiceMaster.SHIPPING_GST_NOColumn.ColumnName].ToString());
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.SHIPPING_ADDRESSColumn, drGSTInvoiceMaster[AppSchema.GSTInvoiceMaster.SHIPPING_ADDRESSColumn.ColumnName].ToString());
                                    //dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.SHIPPING_STATE_IDColumn, NumberSet.ToInteger(drGSTInvoiceMaster[AppSchema.GSTInvoiceMaster.SHIPPING_STATE_IDColumn.ColumnName].ToString()));
                                    //dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.SHIPPING_COUNTRY_IDColumn, NumberSet.ToInteger(drGSTInvoiceMaster[AppSchema.GSTInvoiceMaster.SHIPPING_COUNTRY_IDColumn.ColumnName].ToString()));
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.SHIPPING_STATE_IDColumn, GSTMasterShippingStateId);
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.SHIPPING_COUNTRY_IDColumn, GSTMasterShippingCountryId);

                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.CHEQUE_IN_FAVOURColumn, drGSTInvoiceMaster[AppSchema.GSTInvoiceMaster.CHEQUE_IN_FAVOURColumn.ColumnName].ToString());
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.TOTAL_AMOUNTColumn, totalamount);
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.TOTAL_CGST_AMOUNTColumn, cgst);
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.TOTAL_SGST_AMOUNTColumn, sgst);
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.TOTAL_IGST_AMOUNTColumn, igst);
                                    if (NumberSet.ToInteger(drGSTInvoiceMaster[AppSchema.GSTInvoiceMaster.IS_REVERSE_CHARGEColumn.ColumnName].ToString()) == ((int)SetDefaultValue.DefaultValue))
                                    {
                                        dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.IS_REVERSE_CHARGEColumn, ((int)SetDefaultValue.DefaultValue).ToString());
                                        dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.REVERSE_CHARGE_AMOUNTColumn, NumberSet.ToDecimal(drGSTInvoiceMaster[AppSchema.GSTInvoiceMaster.REVERSE_CHARGE_AMOUNTColumn.ColumnName].ToString()));
                                    }
                                    else
                                    {
                                        dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.IS_REVERSE_CHARGEColumn, ((int)SetDefaultValue.DisableValue).ToString());
                                        dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.REVERSE_CHARGE_AMOUNTColumn, ((int)SetDefaultValue.DisableValue).ToString());
                                    }

                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.STATUSColumn, NumberSet.ToInteger(drGSTInvoiceMaster[AppSchema.GSTInvoiceMaster.STATUSColumn.ColumnName].ToString()));

                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.BOOKING_VOUCHER_IDColumn.ColumnName, LastVoucherId);
                                    if (VoucherType == DefaultVoucherTypes.Receipt.ToString() || VoucherType == VoucherSubTypes.RC.ToString())
                                    {
                                        dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.BOOKING_VOUCHER_TYPEColumn, VoucherSubTypes.RC.ToString());
                                    }
                                    else if (VoucherType == DefaultVoucherTypes.Payment.ToString() || VoucherType == VoucherSubTypes.PY.ToString())
                                    {
                                        dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.BOOKING_VOUCHER_TYPEColumn, VoucherSubTypes.PY.ToString());
                                    }
                                    else
                                    {
                                        dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.BOOKING_VOUCHER_TYPEColumn, VoucherSubTypes.JN.ToString());
                                    }

                                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                                    resultArgs = dataManager.UpdateData();
                                }

                                if (resultArgs.Success && resultArgs.RowUniqueId != null)
                                {
                                    if (NewGSTInvoiceId == 0) NewGSTInvoiceId = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());

                                    if (resultArgs.Success)
                                    {
                                        resultArgs = SaveGSTInvoiceMasterLedgerDetails(GSTInvoiceId, NewGSTInvoiceId);
                                        if (resultArgs.Success)
                                        {
                                            SaveGSTInvoiceVoucher(GSTInvoiceId, NewGSTInvoiceId, totalamount);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        /// <summary>
        /// On 05/02/2024, To update gst invoice ledger details
        /// </summary>
        /// <param name="dm"></param>
        /// <returns></returns>
        private ResultArgs SaveGSTInvoiceMasterLedgerDetails(Int32 GSTInvoiceId, Int32 NewGSTInvoiceId)
        {
            try
            {
                if (resultArgs.Success)
                {
                    if (dtGSTInvoiceMasterLedgerDetail != null && dtGSTInvoiceMasterLedgerDetail.Rows.Count > 0 &&
                        dtGSTInvoiceMasterLedgerDetail != null && dtGSTInvoiceMasterLedgerDetail.Rows.Count > 0)
                    {
                        DataTable dtgstinvoicemasterledgerdetails = dtGSTInvoiceMasterLedgerDetail.DefaultView.ToTable();
                        dtgstinvoicemasterledgerdetails.DefaultView.RowFilter = this.AppSchema.GSTInvoiceMaster.GST_INVOICE_IDColumn.ColumnName + " = " + GSTInvoiceId;
                        dtgstinvoicemasterledgerdetails = dtgstinvoicemasterledgerdetails.DefaultView.ToTable();

                        foreach (DataRow dr in dtgstinvoicemasterledgerdetails.Rows)
                        {
                            LedgerName = dr[this.AppSchema.GSTInvoiceMasterLedgerDetails.LEDGER_NAMEColumn.ColumnName].ToString();
                            LedgerId = GetId(Id.Ledger);
                            Slab = dr[this.AppSchema.VoucherTransaction.SLABColumn.ColumnName].ToString();
                            int gstClassId = NumberSet.ToInteger(dr[this.AppSchema.VoucherTransaction.LEDGER_GST_CLASS_IDColumn.ColumnName].ToString());
                            int ledgerGSTClassId = 0;
                            if (!string.IsNullOrEmpty(Slab) && gstClassId > 0)
                            {
                                ledgerGSTClassId = GetId(Id.GSTClass);
                            }
                            if (LedgerId == 0)
                            {
                                resultArgs.Message = VoucherImportType.Equals(ImportType.SplitProject) ? String.Format("Ledger Name is Empty while GST Invoice Details.The Required Ledger is {0}", LedgerName) :
                                    "Ledger Name is Empty in Head Office while updating GST Invocie Details.<br/> The Required Ledger in Portal is:<br/><b>" + LedgerName + "</b>";
                            }
                            else if (ledgerGSTClassId == 0)
                            {
                                resultArgs.Message = VoucherImportType.Equals(ImportType.SplitProject) ? String.Format("GST Slab is Empty while updating GST Invoice Details.The Required GST Slab is {0}", Slab) :
                                    "GST Slab is Empty in Head Office while updating GST Invoice Details.<br/> The Required GST Slab in Portal is:<br/><b>" + Slab + "</b>";
                            }
                            else
                            {
                                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.InsertGSTInvoiceMasterLedgerDetail, DataBaseTypeName, SQLAdapterType.HOSQL))
                                {
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.GST_INVOICE_IDColumn, NewGSTInvoiceId);
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.LEDGER_IDColumn, LedgerId);
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.LEDGER_GST_CLASS_IDColumn, ledgerGSTClassId);
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.AMOUNTColumn, NumberSet.ToDouble(dr[this.AppSchema.GSTInvoiceMasterLedgerDetails.AMOUNTColumn.ColumnName].ToString()));
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.TRANS_MODEColumn, dr[this.AppSchema.GSTInvoiceMasterLedgerDetails.TRANS_MODEColumn.ColumnName].ToString());
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.GST_HSN_SAC_CODEColumn, dr[this.AppSchema.GSTInvoiceMasterLedgerDetails.GST_HSN_SAC_CODEColumn.ColumnName].ToString());

                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.QUANTITYColumn, NumberSet.ToDouble(dr[this.AppSchema.GSTInvoiceMasterLedgerDetails.QUANTITYColumn.ColumnName].ToString()));
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.UNIT_AMOUNTColumn, NumberSet.ToDouble(dr[this.AppSchema.GSTInvoiceMasterLedgerDetails.UNIT_AMOUNTColumn.ColumnName].ToString()));
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.UNIT_MEASUREMENTColumn, dr[this.AppSchema.GSTInvoiceMasterLedgerDetails.UNIT_MEASUREMENTColumn.ColumnName].ToString());
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.DISCOUNTColumn, NumberSet.ToDouble(dr[this.AppSchema.GSTInvoiceMasterLedgerDetails.DISCOUNTColumn.ColumnName].ToString()));

                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.CGSTColumn, NumberSet.ToDouble(dr[this.AppSchema.GSTInvoiceMasterLedgerDetails.CGSTColumn.ColumnName].ToString()));
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.SGSTColumn, NumberSet.ToDouble(dr[this.AppSchema.GSTInvoiceMasterLedgerDetails.SGSTColumn.ColumnName].ToString()));
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.IGSTColumn, NumberSet.ToDouble(dr[this.AppSchema.GSTInvoiceMasterLedgerDetails.IGSTColumn.ColumnName].ToString()));
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.BRANCH_IDColumn, "0");
                                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                                    resultArgs = dataManager.UpdateData();
                                }
                            }

                            if (!resultArgs.Success)
                            {
                                break;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        /// <summary>
        /// On 05/02/2024, To update GST vouchers for the following 
        /// 1. GST direct invoice bookig in Receipt and Payment
        /// 2. Making Vouuches against Journal Booking Invoice
        /// </summary>
        /// <param name="dm"></param>
        /// <returns></returns>
        private ResultArgs SaveGSTInvoiceVoucher(Int32 GSTInvoiceId, Int32 NewGSTInvoiceId, double amt)
        {
            try
            {
                if (resultArgs.Success)
                {
                    //GST Invoice - Voucher details
                    //using (DataManager dataManager = new DataManager((resultArgs.DataSource.Table.Rows.Count == 0) ? SQLCommand.VoucherMaster.SaveGSTInvoiceVocuhers : SQLCommand.VoucherMaster.UpdateGSTInvoiceVocuhers))
                    using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.InsertGSTInvoiceVoucher, DataBaseTypeName, SQLAdapterType.HOSQL))
                    {
                        dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.VOUCHER_IDColumn, LastVoucherId);
                        dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.GST_INVOICE_IDColumn, NewGSTInvoiceId);
                        dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.AMOUNTColumn, amt);
                        resultArgs = dataManager.UpdateData();
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }
        #endregion
        #region Head Office Portal Methods
        /// <summary>
        /// This method validates the given Branch voucher XML file and assign mandatory properties for data sync.
        /// (BranchCode, HeadOfficeCode, DateFrom, DateTo and Connection String)
        /// 0. Validates valid voucher xml (checking all voucher datatables in xml)
        /// 1. Validates the Branch Code with Head Office Branch Code of (Webserver or multi location)
        /// 2. Validates the Head Office Code with Head Office of (Webserver or multi location)
        /// 3. Constructs the Connection string for the Head Office and assigns in the static variable. 
        ///    and this will be used for entire sync process for this file
        /// 4. Fetches the Branch Id from the Concern Head Office Database.
        /// 5. Fetches the Head Office ID from the Concern Head Office Database.
        /// 6. DB cconnection
        /// 7. Updates the status to STARTED once the validation is succeeded.
        /// </summary>
        /// <param name="VoucherXml">Branch Voucher XML file</param>
        /// <returns></returns>
        private ResultArgs InitializeVoucherSync(string VoucherXml)
        {
            if (SynchronizeHeld != null)
            {
                Caption = "Initialize Voucher Sync Progress..";
                SynchronizeHeld(this, new EventArgs());
            }
            AcMEDataSynLog.WriteLog("InitializeVoucherSync started");
            try
            {
                XmlFileName = Path.GetFileName(VoucherXml);
                resultArgs = XMLConverter.ConvertXMLToDataSetWithResultArgs(VoucherXml);
                if (resultArgs.Success)
                {
                    DataSet dsVouchers = resultArgs.DataSource.TableSet;
                    resultArgs = RemoveTimezoneForDataSet(dsVouchers);
                    if (resultArgs.Success)
                    {
                        resultArgs = ValidateVoucherXMLFile(dsVouchers);
                        if (resultArgs.Success)
                        {
                            resultArgs = SetImportType(dsVouchers);
                            if (resultArgs.Success)
                            {
                                if (!ImportHeadOfficeBranchData)
                                {
                                    resultArgs = (VoucherImportType == ImportType.HeadOffice) ? ValidateBranchCode(dsVouchers) :
                                        (VoucherImportType == ImportType.SubBranch) ? ValidateSubBranchCode(dsVouchers) : (new CommonMethod()).SplitProjectValidateLicenseBranchCode(dsVouchers);
                                }

                                if (resultArgs.Success)
                                {
                                    if (VoucherImportType == ImportType.HeadOffice) { resultArgs = ValidateHeadOfficeCode(dsVouchers); }
                                    if (resultArgs.Success)
                                    {
                                        resultArgs = SetDateDuration(dsVouchers);
                                        if (resultArgs.Success)
                                        {
                                            if (VoucherImportType == ImportType.HeadOffice) { resultArgs = ValidateLicenseKey(); }
                                            if (resultArgs.Success)
                                            {
                                                if (VoucherImportType == ImportType.HeadOffice) { resultArgs = FetchHeadOfficeDataBase(); }
                                                if (resultArgs.Success)
                                                {
                                                    if (VoucherImportType == ImportType.HeadOffice) { resultArgs = UpdateDataSynStatus(DataSyncMailType.InProgress, "Started.."); }
                                                    if (resultArgs.Success)
                                                    {
                                                        if ((VoucherImportType == ImportType.HeadOffice || VoucherImportType == ImportType.SubBranch))
                                                        {
                                                            resultArgs = FetchBranchOfficeId();
                                                        }

                                                        if (resultArgs.Success)
                                                        {
                                                            if (VoucherImportType == ImportType.HeadOffice)
                                                            {
                                                                resultArgs = SetLocation(dsVouchers);
                                                            }
                                                            //Added by Carmel Raj on September-03-2015
                                                            else
                                                            {
                                                                //On 14/09/2021, to have proper modification date
                                                                //resultArgs = UpdateModifiedOn(); 
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog("Error in InitializeVoucherSync: " + ex.ToString());
                resultArgs.Message = ex.Message;
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("InitializeVoucherSync ended");
            }
            return resultArgs;
        }

        private ResultArgs UpdateModifiedOn()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.UpdateModifiedOn, DataBaseTypeName, SQLAdapterType.HOSQL))
            {
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs RemoveTimezoneForDataSet(DataSet ds)
        {
            try
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataTable dt in ds.Tables)
                    {
                        foreach (DataColumn dc in dt.Columns)
                        {
                            if (dc.DataType == typeof(DateTime))
                            {
                                dc.DateTimeMode = DataSetDateTime.Unspecified;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
                resultArgs.Success = false;
            }
            return resultArgs;
        }

        /// <summary>
        /// Validates the Given XMl file
        /// 1. Validates the mandatory tables for synchronization.
        /// 2. If valid tables exists, It will be assigned to datatables.
        /// </summary>
        /// <param name="dsVouchers"></param>
        /// <returns></returns>
        private ResultArgs ValidateVoucherXMLFile(DataSet dsVouchers)
        {
            try
            {
                //# 30/04/2021, to set import type
                resultArgs = SetImportType(dsVouchers);
                if (resultArgs.Success)
                {
                    if (dsVouchers != null && dsVouchers.Tables.Count > 0)
                    {
                        if (!ImportHeadOfficeBranchData)
                        {
                            if (dsVouchers.Tables.Contains(HEADER_TABLE_NAME) && dsVouchers.Tables.Contains(LEDGER_BALANCE_TABLE_NAME))
                            {
                                if (VoucherImportType == ImportType.SplitProject)
                                {
                                    if (dsVouchers.Tables[HEADER_TABLE_NAME] != null && dsVouchers.Tables[HEADER_TABLE_NAME].Rows.Count > 0)
                                    {
                                        string ImportTypeName = dsVouchers.Tables[HEADER_TABLE_NAME].Rows[0]["IMPORT_TYPE"].ToString();
                                        if (ImportTypeName == ImportType.SplitProject.ToString())
                                        {
                                            //On 30/04/2021, To re-arrange FDs based on Date range in Target DB
                                            resultArgs = ReArrangeFDsBasedOnDateRangeForImport(dsVouchers);
                                            if (resultArgs.Success)
                                            {
                                                resultArgs = AssignDataTable(dsVouchers);
                                            }

                                            ////08/06/2021, To skip closed banks-----------
                                            /*if (dtBankAccount != null && dtBankAccount.Rows.Count > 0)
                                            {
                                                dtBankAccount.DefaultView.RowFilter = AppSchema.BankAccount.DATE_CLOSEDColumn.ColumnName + " => '" + FYDateFrom + "' OR " +
                                                     AppSchema.BankAccount.DATE_CLOSEDColumn.ColumnName + " IS NULL";
                                                dtBankAccount = dtBankAccount.DefaultView.ToTable();
                                            }*/
                                            //--------------------------------------------
                                        }
                                        else
                                        {
                                            resultArgs.Message = "Invalid XML file.";
                                        }
                                    }
                                }
                                else
                                    resultArgs = AssignDataTable(dsVouchers);
                            }
                            else
                            {
                                resultArgs.Message = "Voucher file is Invalid. It does not contain Voucher details.";
                            }
                        }
                        else
                        {
                            resultArgs = AssignDataTable(dsVouchers);
                        }
                    }
                    else
                    {
                        resultArgs.Message = "Voucher file is Empty.";
                    }
                }
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog("Problem in ValidateVoucherXMLFile : " + ex.Message);
                resultArgs.Message = ex.Message;
            }

            return resultArgs;
        }

        /// <summary>
        /// This method Reads the Branch Code from XML file and validates against the Head Office.
        /// 1. Gets the Decrypted Branch Office Code from the Voucher XML
        /// 2. Validates the XML Branch Code with the Portal Branch Code.
        /// 3. If the Branch Code is Valid, Branch Office Id will be assigned to the global variable.
        ///     That brach Id will be used for the entire data sync process.
        /// </summary>
        /// <param name="VoucherXml">Vouchers</param>
        /// <returns>ResultArgs with its properties</returns>
        private ResultArgs ValidateBranchCode(DataSet dsVouchers)
        {
            AcMEDataSynLog.WriteLog("ValidateBranchCode Started..");
            try
            {
                resultArgs = common.GetBranchOfficeCode(dsVouchers);
                if (resultArgs.Success)
                {
                    if (!string.IsNullOrEmpty(resultArgs.ReturnValue.ToString()) && resultArgs.ReturnValue != null)
                    {
                        string BranchCode = resultArgs.ReturnValue.ToString();
                        AcMEDataSynLog.WriteLog("Branch Office Code is: " + BranchCode);
                        using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.AuthenticateBranchCode, DataBaseType.Portal, SQLAdapterType.HOSQL))
                        {
                            dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_CODEColumn, BranchCode);
                            resultArgs = dataManager.FetchData(DataSource.DataTable);
                            if (resultArgs.Success)
                            {
                                DataView dvBranchCode = resultArgs.DataSource.Table.DefaultView;
                                if (dvBranchCode != null && dvBranchCode.Count == 1)
                                {
                                    HeadOfficeBranchCode = BranchCode;
                                    BranchOfficeId = this.NumberSet.ToInteger(dvBranchCode[0][this.AppSchema.DataSyncStatus.BRANCH_OFFICE_IDColumn.ColumnName].ToString());
                                    BranchOfficeName = dvBranchCode[0]["BRANCH_OFFICE_NAME"].ToString();
                                    BranchEmailId = dvBranchCode[0]["BRANCH_EMAIL_ID"].ToString();
                                    AcMEDataSynLog.WriteLog("Branch Email ID :" + BranchEmailId);
                                }
                                else
                                {
                                    resultArgs.Message = "Branch is not available in Portal. Please contact Portal administrator.";
                                }
                            }
                        }
                    }
                    else
                    {
                        resultArgs.Message = "Branch Office Code is empty";
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Validating Branch Office Code: " + ex.ToString();
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ValidateBranchCode Ended.");
            }
            return resultArgs;
        }

        /// <summary>
        /// This method Reads the Sub Branch Code from XML file and validates against the Branch Office Sub Branch Code.
        /// 1. Gets the Decrypted Sub Branch Office Code from the Voucher XML
        /// 2. Validates the XML Branch Code with the Main Branch Code.
        /// 3. If the Sub Branch Code is Valid, Sub Branch Office Id will be assigned to the global variable.
        ///     That brach Id will be used for the entire data sync process.
        /// </summary>
        /// <param name="VoucherXml">Vouchers</param>
        /// <returns>ResultArgs with its properties</returns>
        private ResultArgs ValidateSubBranchCode(DataSet dsVouchers)
        {
            AcMEDataSynLog.WriteLog("ValidateSubBranchCode Started..");
            try
            {
                resultArgs = common.GetBranchOfficeCode(dsVouchers);
                if (resultArgs.Success)
                {
                    if (!string.IsNullOrEmpty(resultArgs.ReturnValue.ToString()) && resultArgs.ReturnValue != null)
                    {
                        string SubBranchCode = resultArgs.ReturnValue.ToString();
                        using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.AuthenticateBranchCode, DataBaseTypeName, SQLAdapterType.HOSQL))
                        {
                            dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_CODEColumn, SubBranchCode);
                            resultArgs = dataManager.FetchData(DataSource.DataTable);

                            if (resultArgs.Success)
                            {
                                DataView dvBranchCode = resultArgs.DataSource.Table.DefaultView;
                                if (dvBranchCode != null && dvBranchCode.Count == 1)
                                {
                                    HeadOfficeBranchCode = SubBranchCode;
                                    BranchOfficeId = this.NumberSet.ToInteger(dvBranchCode[0][this.AppSchema.DataSyncStatus.BRANCH_OFFICE_IDColumn.ColumnName].ToString());
                                    AcMEDataSynLog.WriteLog("Branch Office Code is: " + HeadofficeCode);
                                }
                                else
                                {
                                    resultArgs.Message = "Sub-Branch Office Code not found in Branch Office.";
                                }
                            }
                        }
                    }
                    else
                    {
                        resultArgs.Message = "Sub Branch Office Code is empty";
                    }
                }
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog("Error in Validating Sub Branch Office Code: " + ex.ToString());
                resultArgs.Message = ex.Message;
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ValidateBranchCode Ended.");
            }
            return resultArgs;
        }

        /// <summary>
        /// This method reads the Head Office Code from the Voucher XML and validates against the Head Office.
        /// 1. Gets the Decrypted Head Office Office Code from the Voucher XML
        /// 2. Validates the XML Head Office Code with the Portal Head Office Code.
        /// 3. If the Head Office Code is Valid, Head Office Id will be assigned to the global variable.
        ///     That Head Office Id will be used for the entire data sync process wherever necessary.
        /// </summary>
        /// <param name="VoucherXml">Vouchers</param>
        /// <returns>ResultArgs with its properties</returns>
        private ResultArgs ValidateHeadOfficeCode(DataSet dsVouchers)
        {
            AcMEDataSynLog.WriteLog("ValidateHeadOfficeCode Started..");
            try
            {
                resultArgs = common.GetHeadOfficeCode(dsVouchers);
                if (resultArgs.Success)
                {
                    if (resultArgs.ReturnValue != null && !string.IsNullOrEmpty(resultArgs.ReturnValue.ToString()))
                    {
                        string HOCode = resultArgs.ReturnValue.ToString();
                        AcMEDataSynLog.WriteLog("Head Office Code is: " + HOCode);
                        using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.AuthenticateHeadOfficeCode, DataBaseType.Portal, SQLAdapterType.HOSQL))
                        {
                            dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.HEAD_OFFICE_CODEColumn, HOCode);
                            resultArgs = dataManager.FetchData(DataSource.DataTable);
                            if (resultArgs.Success)
                            {
                                DataView dvHeadOfficeCode = resultArgs.DataSource.Table.DefaultView;
                                if (dvHeadOfficeCode != null && dvHeadOfficeCode.Count == 1)
                                {
                                    HeadOfficeCode = HOCode;
                                    HeadOfficeId = this.NumberSet.ToInteger(dvHeadOfficeCode[0][this.AppSchema.DataSyncStatus.HEAD_OFFICE_IDColumn.ColumnName].ToString());
                                }
                                else
                                {
                                    resultArgs.Message = "Your Head Office is not available in Portal. Contact Portal administrator.";
                                }
                            }
                        }
                    }
                    else
                    {
                        resultArgs.Message = "Head Office Code is empty";
                    }
                }
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog("Error in Validating Head Office Code: " + ex.ToString());
                resultArgs.Message = ex.Message;
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ValidateHeadOfficeCode Ended..");
            }
            return resultArgs;
        }

        /// <summary>
        /// This method constructs the Connection string from Portal Database and assigns to the static variable to change the connection
        /// to the concern Head Office Database..
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchHeadOfficeDataBase()
        {
            AcMEDataSynLog.WriteLog("FetchHeadOfficeDataBase Started.");
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.FetchDataBase, DataBaseType.Portal, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.HEAD_OFFICE_CODEColumn, HeadOfficeCode);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                    if (resultArgs.Success)
                    {
                        DataTable dtHeadOfficeConnection = resultArgs.DataSource.Table;
                        if (dtHeadOfficeConnection != null && dtHeadOfficeConnection.Rows.Count == 1)
                        {
                            SettingProperty.HOBConnectionString = dtHeadOfficeConnection.Rows[0][this.AppSchema.LicenseDataTable.DB_CONNECTIONColumn.ColumnName].ToString();
                        }
                        else
                        {
                            resultArgs.Message = "Head Office connection string is empty.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog("Error in Fetching Head Office Database Connection: " + ex.Message);
                resultArgs.Message = ex.Message;
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("FetchHeadOfficeDataBase Ended.");
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetches Branch Office Id for inserting records in the concern Head Office database.
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchBranchOfficeId()
        {
            AcMEDataSynLog.WriteLog("FetchBranchOfficeId Started..");
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.FetchBranchOfficeId, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_CODEColumn, HeadOfficeBranchCode);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                    if (resultArgs.Success)
                    {
                        DataTable dtBranchId = resultArgs.DataSource.Table;
                        if (dtBranchId != null && dtBranchId.Rows.Count == 1)
                        {
                            BranchId = this.NumberSet.ToInteger(dtBranchId.Rows[0][this.AppSchema.LicenseDataTable.BRANCH_OFFICE_IDColumn.ColumnName].ToString());
                            AcMEDataSynLog.WriteLog("BRANCH_ID is :" + BranchId);
                        }
                        else
                        {
                            resultArgs.Message = "Branch is not available in Head Office. Contact Head Office Administrator.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Fetch Branch Office Id from Head Office: " + ex.ToString();
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("FetchBranchOfficeId Ended.");
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetches Location Id for inserting records in the concern Head Office database.
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchLocationId()
        {
            AcMEDataSynLog.WriteLog("FetchLocationId Started..");
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.FetchLocationId, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.LOCATIONColumn, Location);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_IDColumn, BranchId);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                    if (resultArgs.Success)
                    {
                        DataTable dtLocation = resultArgs.DataSource.Table;
                        if (dtLocation != null && dtLocation.Rows.Count == 1)
                        {
                            LocationId = this.NumberSet.ToInteger(dtLocation.Rows[0][this.AppSchema.LicenseDataTable.LOCATION_IDColumn.ColumnName].ToString());
                            AcMEDataSynLog.WriteLog("LOCATION ID is :" + LocationId);
                        }
                        else
                        {
                            resultArgs.Message = "Location is not found for this Branch in Portal. Contact Head Office Administrator.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog("Error in Fetch Location Id from Head Office: " + ex.ToString());
                resultArgs.Message = ex.Message;
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("FetchLocationId Ended.");
            }
            return resultArgs;
        }

        public void InitializeLicenceDetails(string VoucherXml)
        {
            CommonMethod common = new CommonMethod();
            resultArgs = XMLConverter.ConvertXMLToDataSetWithResultArgs(VoucherXml);
            if (resultArgs.Success)
            {
                DataSet dsVouchers = resultArgs.DataSource.TableSet;
                //Assign Branch office Code and Master Code to static variables
                common.ValidateLicenseInformation(dsVouchers);

                DataTable dtHeader = dsVouchers.Tables["Header"];
                if (dtHeader != null && dtHeader.Rows.Count > 0)
                {
                    //For Local Project Split, we introduced isFYSplit colunmn
                    if (resultArgs.Success && dtHeader != null && dtHeader.Rows.Count > 0)
                    {
                        //On 14/04/2021, For Branch Information ---------------------------------------------
                        string simportType = dtHeader.Rows[0]["IMPORT_TYPE"].ToString();
                        ImportType voucherImportType = (simportType == ImportType.HeadOffice.ToString() ? ImportType.HeadOffice : simportType == ImportType.SubBranch.ToString() ? ImportType.SubBranch : ImportType.SplitProject);

                        if (voucherImportType == ImportType.SplitProject)
                        {
                            ResultArgs result = common.GetHeadOfficeCode(dsVouchers);
                            if (result.Success && result.ReturnValue != null)
                            {
                                CommonMethod.HeadOfficeCode = result.ReturnValue.ToString();
                            }

                            result = common.GetBranchOfficeCode(dsVouchers);
                            if (result.Success && result.ReturnValue != null)
                            {
                                CommonMethod.BranchOfficeCode = result.ReturnValue.ToString();
                            }
                        }
                        //-----------------------------------------------------------------------------------

                        AssignXMLProjectSettingDetails(dsVouchers);


                    }
                }
            }
        }

        public ResultArgs AssignXMLProjectSettingDetails(DataSet dsVouchers)
        {
            ResultArgs result = new ResultArgs();
            result.Success = true;
            try
            {
                if (dsVouchers != null)
                {
                    DataTable dtHeader = dsVouchers.Tables["Header"];
                    if (dtHeader != null && dtHeader.Rows.Count > 0)
                    {
                        //For Local Project Split, we introduced isFYSplit colunmn
                        if (resultArgs.Success && dtHeader != null && dtHeader.Rows.Count > 0)
                        {
                            //On 14/04/2021, For Branch Information ---------------------------------------------
                            string simportType = dtHeader.Rows[0]["IMPORT_TYPE"].ToString();
                            ImportType voucherImportType = (simportType == ImportType.HeadOffice.ToString() ? ImportType.HeadOffice : simportType == ImportType.SubBranch.ToString() ? ImportType.SubBranch : ImportType.SplitProject);
                            //-----------------------------------------------------------------------------------

                            if (dtHeader.Columns.Contains("IsFYSplit"))
                            {
                                //IsFYSplit = Boolean.TryParse(dtHeader.Rows[0]["IsFYSplit"].ToString().Trim(), out IsFYSplit);
                                bool check = Boolean.TryParse(dtHeader.Rows[0]["IsFYSplit"].ToString().Trim(), out IsFYSplit);
                            }

                            //For Date Range
                            if (dtHeader.Columns.Contains("DATE_FROM") && dtHeader.Columns.Contains("DATE_TO"))
                            {
                                FYDateFrom = this.DateSet.ToDate(dtHeader.Rows[0]["DATE_FROM"].ToString(), false);
                                FYDateTo = this.DateSet.ToDate(dtHeader.Rows[0]["DATE_TO"].ToString(), false);
                            }

                            if (dtHeader.Columns.Contains(AppSchema.AccountingPeriod.YEAR_FROMColumn.ColumnName))
                            {
                                FYYearFrom = this.DateSet.ToDate(dtHeader.Rows[0][AppSchema.AccountingPeriod.YEAR_FROMColumn.ColumnName].ToString(), false);
                            }

                            if (dtHeader.Columns.Contains(this.AppSchema.ProjectImportExport.SHOW_MAP_LEDGERSColumn.ColumnName))
                            {
                                bool check = Boolean.TryParse(dtHeader.Rows[0][this.AppSchema.ProjectImportExport.SHOW_MAP_LEDGERSColumn.ColumnName].ToString().Trim(), out IsShowMappingLedgers);
                            }


                            //For Project
                            DataTable dtProject = dsVouchers.Tables["Project"];
                            if (dtProject != null && dtProject.Rows.Count > 0)
                            {
                                FYProjectName = dtProject.Rows[0]["PROJECT"].ToString().Trim();
                                ProjectName = FYProjectName;
                                FYProjectId = GetId(Id.Project);

                                result = AssignImportedProjectVoucherDetails(dsVouchers);

                                if (result.Success)
                                {
                                    result = IsAuditVouchersLocked(FYProjectId, FYDateFrom, FYDateFrom);
                                }
                            }

                            //03/01/2022, For Ledgers Mapping
                            if (dsVouchers.Tables["Ledger"] != null)
                            {
                                Ledgers = dsVouchers.Tables["Ledger"].DefaultView.ToTable();
                            }
                        }
                    }
                }

                //If Year from is not exits in XML, prompt message
                if (FYYearFrom == DateTime.MinValue)
                {
                    result.Message = "Selected Project Data XML is old file. Export Project Data again and Import.";
                }

            }
            catch (Exception err)
            {
                result.Message = err.Message;
                AcMEDataSynLog.WriteLog("Error in FD AssignXMLProjectSettingDetails: " + err.ToString());
            }
            return result;
        }

        /// <summary>
        /// On 18/05/2021, If Import Project is available
        /// Get its Voucher details like 
        /// # Fifrst voucher date of the Project
        /// # Last voucher date of the Project
        /// # Check FD Vouchers are available during imported xml date range
        /// </summary>
        private ResultArgs AssignImportedProjectVoucherDetails(DataSet dsVouchers)
        {
            ResultArgs result = new ResultArgs();
            result.Success = true;
            try
            {
                result = SetImportType(dsVouchers);
                if (result.Success)
                {
                    FYProjectVoucherExistsInDB = false;
                    FYProjectFDVoucherExistsInDB = false;
                    FYProjectFDVoucherExistsInDB = false;
                    FYProjectFDVoucherExistsInXML = false;
                    FYProjectBudgetExistsInXML = false;
                    FYProjectGSTExistsInXML = false;
                    MismatchBudgetProjectsWithDBProjects = false;
                    NoOfBudgetProjects = 0;
                    IsGSTEnabled = false;
                    IsGSTVendorDetailsEnabled = false;

                    //Check FD Vouchers available in Project XML Data
                    FYProjectFDVoucherFirstDateInXML = FYDateFrom;
                    if ((dsVouchers.Tables[FD_ACCOUNT_TABLE_NAME] != null && dsVouchers.Tables[FD_ACCOUNT_TABLE_NAME].Rows.Count > 0)
                        || (dsVouchers.Tables[FD_RENEWAL_TABLE_NAME] != null && dsVouchers.Tables[FD_RENEWAL_TABLE_NAME].Rows.Count > 0))
                    {
                        FYProjectFDVoucherExistsInXML = true;

                        //On 16/09/2022, To get minimum fd voucher date
                        if (dsVouchers.Tables[FD_VOUCHER_MASTER_TRANS_TABLE_NAME] != null && dsVouchers.Tables[FD_VOUCHER_MASTER_TRANS_TABLE_NAME].Rows.Count > 0)
                        {
                            DataTable dtFDVouchers = dsVouchers.Tables[FD_VOUCHER_MASTER_TRANS_TABLE_NAME];
                            string FDfirstVoucherDATE = dtFDVouchers.Compute("MIN(VOUCHER_DATE)", string.Empty).ToString();
                            if (!string.IsNullOrEmpty(FDfirstVoucherDATE))
                            {
                                FYProjectFDVoucherFirstDateInXML = DateSet.ToDate(FDfirstVoucherDATE, false);
                            }
                        }
                    }

                    //Check Budget available in Project XML Data
                    if ((dsVouchers.Tables[BUDGET_MASTER] != null && dsVouchers.Tables[BUDGET_MASTER].Rows.Count > 0)
                        && (dsVouchers.Tables[BUDGET_LEDGER] != null && dsVouchers.Tables[BUDGET_LEDGER].Rows.Count > 0))
                    {
                        FYProjectBudgetExistsInXML = true;
                    }

                    //Check GST available in Project XML Data
                    if ((dsVouchers.Tables[GST_INVOICE_MASTER] != null && dsVouchers.Tables[GST_INVOICE_MASTER].Rows.Count > 0)
                        && (dsVouchers.Tables[GST_INVOICE_MASTER_LEDGER_DETAIL] != null && dsVouchers.Tables[GST_INVOICE_MASTER_LEDGER_DETAIL].Rows.Count > 0))
                    {
                        FYProjectGSTExistsInXML = true;
                    }

                    if (FYProjectId > 0 || MergerProjectId > 0)
                    {
                        //On 25/05/2021, Imported Project is not available in Target database but merge with existing Proejct
                        //Change project details
                        Int32 proid = (FYProjectId == 0 && MergerProjectId > 0) ? MergerProjectId : FYProjectId;
                        if (FYProjectId > 0 && MergerProjectId > 0)
                        {
                            proid = MergerProjectId;
                        }

                        //Get Vouchers details which are available for Proejcts
                        using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.CheckFirstVoucherDateByProject))
                        {
                            dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, proid);
                            result = dataManager.FetchData(DataSource.DataTable);
                        }
                        if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                        {
                            FYProjectVoucherExistsInDB = true;
                            string vdate = result.DataSource.Table.Rows[0][AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName].ToString();
                            FYProjectFirstVoucherDateInDB = DateSet.ToDate(vdate, false);

                            //Get Last Voucher Date
                            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.CheckLastVoucherDateByProject))
                            {
                                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, proid);
                                result = dataManager.FetchData(DataSource.DataTable);
                            }
                            if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                            {
                                FYProjectVoucherExistsInDB = true;
                                vdate = result.DataSource.Table.Rows[0][AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName].ToString();
                                FYProjectLastVoucherDateInDB = DateSet.ToDate(vdate, false);
                            }
                        }

                        //Check FD Vouchers are available for Proejcts
                        using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.CheckFDVoucheExistsByProject))
                        {
                            dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, proid);
                            //dataManager.Parameters.Add(this.AppSchema.FDRegisters.DATE_FROMColumn, FYDateFrom);
                            //dataManager.Parameters.Add(this.AppSchema.FDRegisters.DATE_TOColumn, FYDateTo);
                            dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                            result = dataManager.FetchData(DataSource.DataTable);
                        }
                        if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                        {
                            Int32 noofFDVouchers = NumberSet.ToInteger(result.DataSource.Table.Rows[0]["COUNT"].ToString());
                            FYProjectFDVoucherExistsInDB = (noofFDVouchers > 0);
                        }
                    }

                    //18/06/2021, for Budget Module
                    if (dsVouchers.Tables[BUDGET_PROJECT] != null && dsVouchers.Tables[BUDGET_PROJECT].Rows.Count > 0)
                    {
                        dtBudgetProject = dsVouchers.Tables[BUDGET_PROJECT];
                        result = GetBudgetProjectIds();
                        NoOfBudgetProjects = dtBudgetProject.Rows.Count;
                        if (result.Success && result.ReturnValue != null)
                        {
                            string BudgetProjectsIdsInDB = result.ReturnValue.ToString();
                            if (BudgetProjectsIdsInDB.Split(',').Length != dtBudgetProject.Rows.Count) //For combined budget projects
                            {
                                MismatchBudgetProjectsWithDBProjects = true;
                            }
                        }
                        //else if (result.Success && result.ReturnValue == null && dsVouchers.Tables[BUDGET_PROJECT].Rows.Count > 1)
                        else if (result.Success && result.ReturnValue == null) //If only one Budget Project 
                        {
                            MismatchBudgetProjectsWithDBProjects = true;
                        }
                    }

                    //10/07/2021, To assign GST enabled ledgers and GST Vendor details for Finance Settings
                    if (dsVouchers.Tables[LEDGER_TABLE_NAME] != null && dsVouchers.Tables[VOUCHER_TRANS_TABLE_NAME] != null)
                    {
                        //GST Enabled in Finance Settings
                        DataTable dtGSTLedgers = dsVouchers.Tables[LEDGER_TABLE_NAME];
                        dtGSTLedgers.DefaultView.RowFilter = AppSchema.Ledger.IS_GST_LEDGERSColumn.ColumnName + " = 1";
                        IsGSTEnabled = (dtGSTLedgers.DefaultView.Count > 0);
                        dtGSTLedgers.DefaultView.RowFilter = string.Empty;

                        //GST Vendor Enabled
                        DataTable dtGSTVouchersLedgers = dsVouchers.Tables[VOUCHER_TRANS_TABLE_NAME];
                        dtGSTVouchersLedgers.DefaultView.RowFilter = AppSchema.VoucherTransaction.LEDGER_GST_CLASS_IDColumn.ColumnName + " > 0";
                        IsGSTVendorDetailsEnabled = (dtGSTVouchersLedgers.DefaultView.Count > 0);
                        dtGSTVouchersLedgers.DefaultView.RowFilter = string.Empty;
                    }
                }
            }
            catch (Exception err)
            {
                result.Message = err.Message;
                AcMEDataSynLog.WriteLog("Error in FD AssignImportedProjectVoucherDetails: " + err.ToString());
            }
            return result;
        }
        #endregion

        #region Update Budget Details

        /// <summary>
        /// This method is used to Insert or update budget detials in brach db or portal db (Budget import /Budget Export)
        /// 
        /// this is common method in datasync for branch as well as for portal
        /// This method is used to get budget from branch office or Portal based on branch id
        /// if branchid =0, branch db or portal head office db
        /// if this method is called from branch office, branch_id will be 0, else real branch_id from portal
        /// 
        /// 1. By giving DateFrom, DateTo, Project and budget type to find budget is exists or not
        /// 2. based on budget exists, insert/update budget in brach/port db (Budget import /Budget Export)
        /// 3. budget action will be approved if updates in branch db or will be recommanded (in Portal db) (Budget import /Budget Export)
        /// </summary>
        /// <returns></returns>
        private ResultArgs UpdateBudgetMaster(DataRow drBudgetRow, Int32 branchId)
        {
            try
            {
                //Budget Properties
                int IsMonthwiseDistribution = NumberSet.ToInteger(drBudgetRow[this.AppSchema.Budget.IS_MONTH_WISEColumn.ColumnName].ToString());
                int BudgetLevelId = NumberSet.ToInteger(drBudgetRow[this.AppSchema.Budget.BUDGET_LEVEL_IDColumn.ColumnName].ToString());
                string Remarks = drBudgetRow[this.AppSchema.Budget.REMARKSColumn.ColumnName].ToString();
                //1. Get real budget id from brach or porta db (Budget import /Budget Export)
                resultArgs = GetBudgetId(branchId, BudgetDateFrom, BudgetDateTo, ProjectIds, BudgetTypeId);
                if (resultArgs.Success)
                {
                    Int32 budgetid = NumberSet.ToInteger(resultArgs.ReturnValue.ToString());
                    string proid = ProjectIds.Split(',').GetValue(0).ToString(); //for temp project
                    using (DataManager dataManager = new DataManager(budgetid == 0 ? SQLCommand.ImportVoucher.InsertBudgetMaster : SQLCommand.ImportVoucher.UpdateBudgetMaster,
                                                                                DataBaseTypeName, SQLAdapterType.HOSQL))
                    {
                        dataManager.Parameters.Add(AppSchema.Budget.BUDGET_IDColumn, budgetid, true);
                        dataManager.Parameters.Add(AppSchema.Budget.BRANCH_IDColumn, branchId);
                        dataManager.Parameters.Add(AppSchema.Budget.PROJECT_IDColumn, proid);
                        dataManager.Parameters.Add(AppSchema.Budget.BUDGET_NAMEColumn, BudgetName);
                        dataManager.Parameters.Add(AppSchema.Budget.BUDGET_TYPE_IDColumn, BudgetTypeId);
                        dataManager.Parameters.Add(AppSchema.Budget.DATE_FROMColumn, BudgetDateFrom);
                        dataManager.Parameters.Add(AppSchema.Budget.DATE_TOColumn, BudgetDateTo);
                        dataManager.Parameters.Add(AppSchema.Budget.IS_MONTH_WISEColumn, IsMonthwiseDistribution);
                        dataManager.Parameters.Add(AppSchema.Budget.BUDGET_LEVEL_IDColumn, BudgetLevelId);
                        dataManager.Parameters.Add(AppSchema.Budget.REMARKSColumn, Remarks);
                        dataManager.Parameters.Add(AppSchema.Budget.IS_ACTIVEColumn, (int)ActiveStatus.Active);
                        dataManager.Parameters.Add(AppSchema.Budget.BUDGET_ACTIONColumn, (branchId == 0 ? (int)BudgetAction.Approved : (int)BudgetAction.Recommended));
                        dataManager.DataCommandArgs.IsDirectReplaceParameter = false;
                        resultArgs = dataManager.UpdateData();
                    }
                    if (resultArgs.Success)
                    {
                        if (budgetid.Equals(0))
                            budgetid = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());

                        resultArgs.ReturnValue = budgetid;
                    }

                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Budget Master Details: " + ex.ToString();
            }
            finally { }

            return resultArgs;
        }

        /// <summary>
        /// 1. Clear slected budget ledger 
        /// 2. Insert all budget ledgers with propsoed and approved amount
        /// based on budget exists, insert/update budget in brach/port db (Budget import /Budget Export)
        /// </summary>
        /// <returns></returns>
        private ResultArgs UpdateBudgetLedger(DataTable dtBudgetLedger, Int32 BudgetId)
        {
            try
            {
                if (dtBudgetLedger.Rows.Count > 0)
                {
                    //1. Clear budget ledgers
                    using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.DeleteBudgetLedger, DataBaseTypeName, SQLAdapterType.HOSQL))
                    {
                        dataManager.Parameters.Add(AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                        resultArgs = dataManager.UpdateData();
                    }

                    //2. insert all budget ledgers
                    if (resultArgs.Success)
                    {
                        foreach (DataRow drBudgetLedger in dtBudgetLedger.Rows)
                        {
                            //Int32 ledgerid = this.NumberSet.ToInteger(drBudgetLedger[this.AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString());
                            LedgerName = drBudgetLedger[this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                            Int32 ledgeridNew = GetId(Id.Ledger);
                            //ledgeridNew = this.NumberSet.ToInteger(drBudgetLedger[this.AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString());
                            if (ledgeridNew > 0)
                            {
                                double proposedamount = this.NumberSet.ToDouble(drBudgetLedger[this.AppSchema.Budget.PROPOSED_AMOUNTColumn.ColumnName].ToString());
                                double approvedamount = this.NumberSet.ToDouble(drBudgetLedger[this.AppSchema.Budget.APPROVED_AMOUNTColumn.ColumnName].ToString());
                                string transmode = drBudgetLedger[this.AppSchema.Budget.TRANS_MODEColumn.ColumnName].ToString();
                                string narration = drBudgetLedger[this.AppSchema.Budget.NARRATIONColumn.ColumnName].ToString();
                                string honrration = drBudgetLedger[this.AppSchema.Budget.HO_NARRATIONColumn.ColumnName].ToString();


                                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.InsertBudgetLedger, DataBaseTypeName, SQLAdapterType.HOSQL))
                                {
                                    dataManager.Parameters.Add(AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                                    dataManager.Parameters.Add(AppSchema.Budget.LEDGER_IDColumn, ledgeridNew);
                                    dataManager.Parameters.Add(AppSchema.Budget.PROPOSED_AMOUNTColumn, proposedamount);
                                    dataManager.Parameters.Add(AppSchema.Budget.APPROVED_AMOUNTColumn, approvedamount);
                                    dataManager.Parameters.Add(AppSchema.Budget.TRANS_MODEColumn, transmode);
                                    dataManager.Parameters.Add(AppSchema.Budget.NARRATIONColumn, narration);
                                    dataManager.Parameters.Add(AppSchema.Budget.HO_NARRATIONColumn, honrration);
                                    resultArgs = dataManager.UpdateData();
                                }

                                //Map budget ledger with project
                                if (resultArgs.Success)
                                {
                                    foreach (string pid in ProjectIds.Split(','))
                                    {
                                        ProjectId = this.NumberSet.ToInteger(pid);
                                        LedgerId = ledgeridNew;
                                        //Map Project Ledger
                                        resultArgs = MapProject(SQLCommand.ImportVoucher.MapProjectLedger);

                                        if (resultArgs.Success)
                                        {
                                            //Map Budget Project Ledger
                                            resultArgs = MapProject(SQLCommand.ImportVoucher.MapProjectBudgetLedger);
                                            if (!resultArgs.Success)
                                            {
                                                resultArgs.Message = "Problem in mapping budget ledger with project'" + LedgerName + "', " + resultArgs.Message;
                                                break;
                                            }
                                        }
                                        else if (!resultArgs.Success)
                                        {
                                            resultArgs.Message = "Problem in mapping ledger with project'" + LedgerName + "', " + resultArgs.Message;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    resultArgs.Message = "Problem in updating Budget ledger '" + LedgerName + "', " + resultArgs.Message;
                                }
                            }
                            else
                            {
                                resultArgs.Message = "Budget ledger is not exists '" + LedgerName + "'";
                            }

                            if (!resultArgs.Success)
                            {
                                break;
                            }
                        }
                    }
                }
                else
                {
                    resultArgs.Message = "Budget ledger are not available";
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Budget Ledger Details: " + ex.ToString();
            }
            finally { }

            return resultArgs;
        }



        /// <summary>
        /// 1. Clear slected budget statistical details
        /// 2. Insert all budget statistical details
        /// based on budget exists, insert/update budget in brach/port db (Budget import /Budget Export)
        /// </summary>
        /// <returns></returns>
        private ResultArgs UpdateBudgetStatisticalsDetails(DataTable BudgetStatisticsDetails, Int32 BudgetId)
        {
            try
            {
                if (BudgetStatisticsDetails.Rows.Count > 0)
                {
                    //1. Clear budget Statistics details
                    using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.DeleteBudgetStatisticsDetails, DataBaseTypeName, SQLAdapterType.HOSQL))
                    {
                        dataManager.Parameters.Add(AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                        resultArgs = dataManager.UpdateData();
                    }

                    //2. insert all Statistics details
                    if (resultArgs.Success)
                    {
                        foreach (DataRow drBudgetStatisticsDetail in BudgetStatisticsDetails.Rows)
                        {
                            //Int32 ledgerid = this.NumberSet.ToInteger(drBudgetLedger[this.AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString());
                            StatisticsTypeName = drBudgetStatisticsDetail[this.AppSchema.StatisticsType.STATISTICS_TYPEColumn.ColumnName].ToString();
                            Int32 StatisticsTypeIdNewId = GetId(Id.StatisticsType);

                            if (StatisticsTypeIdNewId > 0)
                            {
                                Int32 TotalCount = this.NumberSet.ToInteger(drBudgetStatisticsDetail[this.AppSchema.BudgetStatistics.TOTAL_COUNTColumn.ColumnName].ToString());

                                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.InsertBudgetStatisticsDetails, DataBaseTypeName, SQLAdapterType.HOSQL))
                                {
                                    dataManager.Parameters.Add(AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                                    dataManager.Parameters.Add(AppSchema.BudgetStatistics.STATISTICS_TYPE_IDColumn, StatisticsTypeIdNewId);
                                    dataManager.Parameters.Add(AppSchema.BudgetStatistics.TOTAL_COUNTColumn, TotalCount);
                                    resultArgs = dataManager.UpdateData();
                                }
                            }
                            else
                            {
                                resultArgs.Message = "Budget Statistics Type is not exists '" + StatisticsTypeName + "'";
                            }

                            if (!resultArgs.Success)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Budget Ledger Details: " + ex.ToString();
            }
            finally { }

            return resultArgs;
        }

        /// <summary>
        /// 1. Clear slected budget sub ledger 
        /// 2. Insert all budget sub ledgers with propsoed and approved amount
        /// based on budget exists, insert/update budget in brach/port db (Budget import /Budget Export)
        /// </summary>
        /// <returns></returns>
        private ResultArgs UpdateBudgetSubLedger(DataTable dtBudgetSubLedger, Int32 BudgetId)
        {
            try
            {
                if (dtBudgetSubLedger.Rows.Count > 0)
                {
                    //1. Clear budget ledgers
                    using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.DeleteBudgetSubLedger, DataBaseTypeName, SQLAdapterType.HOSQL))
                    {
                        dataManager.Parameters.Add(AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                        resultArgs = dataManager.UpdateData();
                    }

                    //2. insert all budget sub ledgers
                    if (resultArgs.Success)
                    {
                        //Check and Insert all Sub Ledgers, before insert budget sub ledger----------------------------------------------
                        DataTable dtSubLedgers = dtBudgetSubLedger.DefaultView.ToTable(true, new string[] { this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, 
                                                                        this.AppSchema.VoucherSubLedger.SUB_LEDGER_NAMEColumn.ColumnName });
                        if (dtSubLedgers != null && dtSubLedgers.Rows.Count > 0)
                        {
                            //Insert/Update Sub Ledgers
                            resultArgs = ImportSubLedger(dtSubLedgers);
                        }
                        //-----------------------------------------------------------------------------------------------------------------------

                        if (resultArgs.Success)
                        {
                            foreach (DataRow drBudgetSubLedger in dtBudgetSubLedger.Rows)
                            {
                                //Int32 ledgerid = this.NumberSet.ToInteger(drBudgetLedger[this.AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString());
                                LedgerName = drBudgetSubLedger[this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                                Int32 ledgeridNew = GetId(Id.Ledger);
                                LedgerId = ledgeridNew;

                                SubLedgerName = drBudgetSubLedger[this.AppSchema.SubLedger.SUB_LEDGER_NAMEColumn.ColumnName].ToString();
                                Int32 subledgeridNew = GetId(Id.SubLedger);

                                //ledgeridNew = this.NumberSet.ToInteger(drBudgetLedger[this.AppSchema.Budget.LEDGER_IDColumn.ColumnName].ToString());
                                if (ledgeridNew > 0 && subledgeridNew > 0)
                                {
                                    double proposedamount = this.NumberSet.ToDouble(drBudgetSubLedger[this.AppSchema.Budget.PROPOSED_AMOUNTColumn.ColumnName].ToString());
                                    double approvedamount = this.NumberSet.ToDouble(drBudgetSubLedger[this.AppSchema.Budget.APPROVED_AMOUNTColumn.ColumnName].ToString());
                                    string transmode = drBudgetSubLedger[this.AppSchema.Budget.TRANS_MODEColumn.ColumnName].ToString();
                                    string narration = drBudgetSubLedger[this.AppSchema.Budget.NARRATIONColumn.ColumnName].ToString();
                                    string honrration = drBudgetSubLedger[this.AppSchema.Budget.HO_NARRATIONColumn.ColumnName].ToString();


                                    using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.InsertBudgetSubLedger, DataBaseTypeName, SQLAdapterType.HOSQL))
                                    {
                                        dataManager.Parameters.Add(AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                                        dataManager.Parameters.Add(AppSchema.Budget.LEDGER_IDColumn, ledgeridNew);
                                        dataManager.Parameters.Add(AppSchema.SubLedger.SUB_LEDGER_IDColumn, subledgeridNew);
                                        dataManager.Parameters.Add(AppSchema.Budget.PROPOSED_AMOUNTColumn, proposedamount);
                                        dataManager.Parameters.Add(AppSchema.Budget.APPROVED_AMOUNTColumn, approvedamount);
                                        dataManager.Parameters.Add(AppSchema.Budget.TRANS_MODEColumn, transmode);
                                        dataManager.Parameters.Add(AppSchema.Budget.NARRATIONColumn, narration);
                                        dataManager.Parameters.Add(AppSchema.Budget.HO_NARRATIONColumn, honrration);
                                        resultArgs = dataManager.UpdateData();
                                    }

                                    //Map budget ledger with project
                                    if (resultArgs.Success)
                                    {
                                        foreach (string pid in ProjectIds.Split(','))
                                        {
                                            ProjectId = this.NumberSet.ToInteger(pid);

                                            //Map Project Ledger
                                            resultArgs = MapProject(SQLCommand.ImportVoucher.MapProjectBudgetLedger);

                                            if (resultArgs.Success)
                                            {
                                                //Map Budget Ledger with Project
                                                resultArgs = MapProject(SQLCommand.ImportVoucher.MapProjectLedger);
                                                if (!resultArgs.Success)
                                                {
                                                    resultArgs.Message = "Problem in mapping ledger with project'" + LedgerName + "', " + resultArgs.Message;
                                                    break;
                                                }
                                            }
                                            else if (!resultArgs.Success)
                                            {
                                                resultArgs.Message = "Problem in mapping budget ledger with project '" + LedgerName + "'" + SubLedgerName + "', " + resultArgs.Message;
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        resultArgs.Message = "Problem in updating Budget sub ledger '" + LedgerName + "'" + SubLedgerName + "', " + resultArgs.Message;
                                    }
                                }
                                else
                                {
                                    resultArgs.Message = "Budget sub ledger is not exists '" + LedgerName + "'" + SubLedgerName + "'";
                                }

                                if (!resultArgs.Success)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    resultArgs.Message = "Budget sub ledger are not available";
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in budget sub ledger : " + ex.ToString();
            }
            finally { }

            return resultArgs;
        }

        /// <summary>
        /// 1. Clear slected budget ledger 
        /// 2. Insert all budget ledgers with propsoed and approved amount
        /// based on budget exists, insert/update budget in brach/port db (Budget import /Budget Export)
        /// </summary>
        /// <returns></returns>
        private ResultArgs UpdateBudgetProject(DataTable dtBudgetProject, Int32 BudgetId)
        {
            try
            {
                if (dtBudgetProject.Rows.Count > 0)
                {
                    //1. Clear budget projects
                    using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.DeleteBudgetProject, DataBaseTypeName, SQLAdapterType.HOSQL))
                    {
                        dataManager.Parameters.Add(AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                        resultArgs = dataManager.UpdateData();
                    }

                    //2. insert all budget projects
                    if (resultArgs.Success)
                    {
                        //foreach (DataRow drBudgetLedger in dtBudgetProject.Rows)
                        foreach (string pid in ProjectIds.Split(','))
                        {
                            Int32 projectid = this.NumberSet.ToInteger(pid);

                            using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.InsertBudgetProject, DataBaseTypeName, SQLAdapterType.HOSQL))
                            {
                                dataManager.Parameters.Add(AppSchema.Budget.BUDGET_IDColumn, BudgetId);
                                dataManager.Parameters.Add(AppSchema.Budget.PROJECT_IDColumn, projectid);
                                resultArgs = dataManager.UpdateData();

                                if (!resultArgs.Success)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    resultArgs.Message = "Budget ledger are not available";
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Saving Country Details: " + ex.ToString();
            }
            finally { }

            return resultArgs;
        }

        /// <summary>
        /// This method is used to get budget id from brach or port db (Import/Export)
        /// </summary>
        /// <param name="branchid"></param>
        /// <param name="budgetfromdate"></param>
        /// <param name="budgettodate"></param>
        /// <param name="budgetprojectid"></param>
        /// <returns></returns>#endregion
        private ResultArgs GetBudgetId(int branchid, DateTime budgetfromdate, DateTime budgettodate, string budgetprojectid, Int32 budgettypeid)
        {
            AcMEDataSynLog.WriteLog("GetBudgetId Started..");
            try
            {
                using (DataManager dataManager = new DataManager(DataBaseTypeName))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.HOSQL;
                    dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.GetBudgetId;
                    dataManager.Parameters.Add(this.AppSchema.Budget.BRANCH_IDColumn, branchid);
                    dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, budgetfromdate);
                    dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, budgettodate);
                    dataManager.Parameters.Add(this.AppSchema.Budget.PROJECT_IDColumn, budgetprojectid); //this.AppSchema.Project.PROJECT_IDColumn
                    dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_TYPE_IDColumn, budgettypeid);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                    {
                        if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            resultArgs.ReturnValue = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Budget.BUDGET_IDColumn.ColumnName].ToString();
                        }
                        else
                            resultArgs.ReturnValue = "0";

                        AcMEDataSynLog.WriteLog("GetBudgetId Ended..");
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in GetBudgetId:" + ex.ToString();
            }
            finally { }
            return resultArgs;
        }
        #endregion

        #region Common Methods
        /// <summary>
        /// This methods get the dataset and assigns to the concern datatable.
        /// </summary>
        /// <param name="dsReadXML"></param>
        private ResultArgs AssignDataTable(DataSet dsReadXML)
        {
            AcMEDataSynLog.WriteLog("AssignDataTable Started..");
            try
            {
                var query = dsReadXML.Tables.OfType<DataTable>().Select(dt => dt.TableName);
                foreach (var item in query)
                {
                    switch (item)
                    {
                        case PROJECT_TABLE_NAME:
                            {
                                dtProject = dsReadXML.Tables[PROJECT_TABLE_NAME];
                                break;
                            }
                        case HEADER_TABLE_NAME:    //Branch Code, Head Office Code, Transaction Date From and Date To Details
                            {
                                dtHeader = dsReadXML.Tables[HEADER_TABLE_NAME];
                                break;
                            }
                        case VOUCHER_MASTER_TABLE_NAME:  //Raw voucher Master Transaction Details
                            {
                                dtVoucherMaster = dsReadXML.Tables[VOUCHER_MASTER_TABLE_NAME];
                                break;
                            }
                        case VOUCHER_TRANS_TABLE_NAME: //Raw voucher transaction details.
                            {
                                dtVoucherTrans = dsReadXML.Tables[VOUCHER_TRANS_TABLE_NAME];
                                break;
                            }
                        case VOUCHER_COSTCENTRE_TABLE_NAME: //Voucher Cost Centre Details.
                            {
                                dtVoucherCostCentre = dsReadXML.Tables[VOUCHER_COSTCENTRE_TABLE_NAME];
                                break;
                            }
                        case DONOR_TABLE_NAME: //Donor Details
                            {
                                dtDonor = dsReadXML.Tables[DONOR_TABLE_NAME];
                                break;
                            }
                        case COUNTRY_TABLE_NAME: //Country Details
                            {
                                dtCountry = dsReadXML.Tables[COUNTRY_TABLE_NAME];
                                break;
                            }
                        case STATE_TABLE_NAME: //State Details
                            {
                                dtState = dsReadXML.Tables[STATE_TABLE_NAME];
                                break;
                            }
                        case LEDGER_GROUP_TABLE_NAME:
                            {
                                dtLedgerGroup = dsReadXML.Tables[LEDGER_GROUP_TABLE_NAME];
                                break;
                            }
                        case LEDGER_TABLE_NAME: //General Ledger Details
                            {
                                dtLedger = dsReadXML.Tables[LEDGER_TABLE_NAME];
                                break;
                            }
                        case BANKACCOUNT_TABLE_NAME: //Bank Account Details
                            {
                                dtBankAccount = dsReadXML.Tables[BANKACCOUNT_TABLE_NAME];
                                break;
                            }
                        case LEDGER_BALANCE_TABLE_NAME: //Ledger Opening Balance Details
                            {
                                dtLedgerBalance = dsReadXML.Tables[LEDGER_BALANCE_TABLE_NAME];
                                break;
                            }
                        case FD_BANK_TABLE_NAME:   //Fixed Deposit Bank Details
                            {
                                dtFDBank = dsReadXML.Tables[FD_BANK_TABLE_NAME];
                                break;
                            }
                        case FD_BANK_ACCOUNT_TABLE_NAME: //Fixed Deposit Bank Account Details.
                            {
                                dtFDBankAccount = dsReadXML.Tables[FD_BANK_ACCOUNT_TABLE_NAME];
                                break;
                            }
                        case FD_VOUCHER_MASTER_TRANS_TABLE_NAME: //Fixed Deposit Voucher Master Transaction.
                            {
                                dtFDVoucherMasterTrans = dsReadXML.Tables[FD_VOUCHER_MASTER_TRANS_TABLE_NAME];
                                break;
                            }
                        case FD_VOUCHER_TRANS_TABLE_NAME:   //Fixed Deposit Voucher Transactions
                            {
                                dtFDVoucherTrans = dsReadXML.Tables[FD_VOUCHER_TRANS_TABLE_NAME];
                                break;
                            }
                        case FD_ACCOUNT_TABLE_NAME:   //Fixed Deposit Account Details.
                            {
                                dtFDAccount = dsReadXML.Tables[FD_ACCOUNT_TABLE_NAME];
                                break;
                            }
                        case FD_RENEWAL_TABLE_NAME:  //Fixed Deposit Renewal Details
                            {
                                dtFDRenewal = dsReadXML.Tables[FD_RENEWAL_TABLE_NAME];
                                break;
                            }
                        case LEGAL_ENTITY_TABLE_NAME:
                            {
                                dtLegalEntity = dsReadXML.Tables[LEGAL_ENTITY_TABLE_NAME];
                                break;
                            }
                        case PROJECT_LEDGERS: //On 10/04/2021
                            {
                                dtProjectLedgers = dsReadXML.Tables[PROJECT_LEDGERS];
                                break;
                            }
                        case PROJECT_COST_CENTRES:
                            {
                                dtProjectCostCentres = dsReadXML.Tables[PROJECT_COST_CENTRES];
                                break;
                            }
                        case PROJECT_DONORS:
                            {
                                dtProjectDonors = dsReadXML.Tables[PROJECT_DONORS];
                                break;
                            }
                        case BUDGET_MASTER: //On 17/06/2021
                            {
                                dtBudgetMaster = dsReadXML.Tables[BUDGET_MASTER];
                                break;
                            }
                        case BUDGET_PROJECT:
                            {
                                dtBudgetProject = dsReadXML.Tables[BUDGET_PROJECT];
                                break;
                            }
                        case BUDGET_LEDGER:
                            {
                                dtBudgetLedger = dsReadXML.Tables[BUDGET_LEDGER];
                                break;
                            }
                        case LEDGER_PROFILE_TABLE_NAME:
                            {
                                dtLedgerProfile = dsReadXML.Tables[LEDGER_PROFILE_TABLE_NAME];
                                break;
                            }
                        case BUDGET_STATISTICS_DETAILS:
                            {
                                dtBudgetStatisticsDetails = dsReadXML.Tables[BUDGET_STATISTICS_DETAILS];
                                break;
                            }
                        case BUDGET_PROJECT_LEDGER:
                            {
                                dtBudgetProjectLedger = dsReadXML.Tables[BUDGET_PROJECT_LEDGER];
                                break;
                            }
                        case BUDGET_STATISTICS_TYPE:
                            {
                                dtBudgetStatisticsType = dsReadXML.Tables[BUDGET_STATISTICS_TYPE];
                                break;
                            }
                        case BUDGET_TYPE:
                            {
                                dtBudgetType = dsReadXML.Tables[BUDGET_TYPE];
                                break;
                            }
                        case BUDGET_LEVEL:
                            {
                                dtBudgetLevel = dsReadXML.Tables[BUDGET_LEVEL];
                                break;
                            }
                        case MASTER_GST_CLASS:
                            {
                                dtGSTClass = dsReadXML.Tables[MASTER_GST_CLASS];
                                break;
                            }
                        case ASSET_STOCK_VENDORS:
                            {
                                dtAssetStockVendors = dsReadXML.Tables[ASSET_STOCK_VENDORS];
                                break;
                            }
                        case GST_INVOICE_MASTER:
                            {
                                dtGSTInvoiceMaster = dsReadXML.Tables[GST_INVOICE_MASTER];
                                break;
                            }
                        case GST_INVOICE_MASTER_LEDGER_DETAIL:
                            {
                                dtGSTInvoiceMasterLedgerDetail = dsReadXML.Tables[GST_INVOICE_MASTER_LEDGER_DETAIL];
                                break;
                            }
                        case GST_INVOICE_VOUCHER:
                            {
                                dtGSTInvoiceVoucher = dsReadXML.Tables[GST_INVOICE_VOUCHER];
                                break;
                            }
                        case FINANCE_SETTING:
                            {
                                dtFinanceSettings = dsReadXML.Tables[FINANCE_SETTING];
                                break;
                            }
                        //case MERGE_PROJECT_TABLE_NAME:
                        //    {
                        //        if (dsReadXML.Tables[MERGE_PROJECT_TABLE_NAME] != null && dsReadXML.Tables[MERGE_PROJECT_TABLE_NAME].Rows.Count > 0)
                        //        {
                        //            MergerProjectId = NumberSet.ToInteger(dsReadXML.Tables[MERGE_PROJECT_TABLE_NAME].Rows[0][MERGE_PROJECT_ID].ToString());
                        //        }
                        //        break;
                        //    }
                    }
                }
                AcMEDataSynLog.WriteLog("AssignDataTable Ended.");
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in AssignDataTable:" + ex.ToString();
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// This is to check whether ther requested Details available in Head Office
        /// </summary>
        /// <param name="queryId"></param>
        /// <returns></returns>
        private ResultArgs IsExists(SQLCommand.ImportVoucher queryId)
        {
            string TypeofExists = string.Empty;

            try
            {
                using (DataManager dataManager = new DataManager(DataBaseTypeName))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.HOSQL;
                    switch (queryId)
                    {
                        case SQLCommand.ImportVoucher.IsProjectCategoryExists:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.IsProjectCategoryExists;
                                dataManager.Parameters.Add(this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn, ProjectCategoryName);
                                TypeofExists = Id.Purpose.ToString() + " (" + ProjectCategoryName + ")";
                                break;
                            }
                        case SQLCommand.ImportVoucher.IsProjectExists:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.IsProjectExists;
                                dataManager.Parameters.Add(this.AppSchema.Project.PROJECTColumn, ProjectName);
                                TypeofExists = Id.Project.ToString() + " (" + ProjectName + ")";
                                break;
                            }
                        case SQLCommand.ImportVoucher.IsDonorExists:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.IsDonorExists;
                                dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_IDColumn, BranchId);
                                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.NAMEColumn, DonorName);
                                TypeofExists = Id.Donor.ToString() + " (" + DonorName + ")";
                                break;
                            }
                        case SQLCommand.ImportVoucher.IsBankAccountExists:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.IsBankAccountExists;
                                dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_IDColumn, BranchId);
                                dataManager.Parameters.Add(this.AppSchema.BankAccount.ACCOUNT_NUMBERColumn, AccountNumber);
                                TypeofExists = Id.BankAccount.ToString() + " (" + AccountNumber + ")";
                                break;
                            }
                        //case SQLCommand.ImportVoucher.IsBankAccountCodeExits:
                        //    {
                        //        dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.IsBankAccountCodeExits;
                        //        dataManager.Parameters.Add(this.AppSchema.BankAccount.ACCOUNT_CODEColumn, AccountCode);
                        //        break;
                        //    }
                        case SQLCommand.ImportVoucher.IsCostCategoryExists:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.IsCostCategoryExists;
                                dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_CATEGORY_NAMEColumn, CostCategory);
                                TypeofExists = Id.CostCategory.ToString() + " (" + CostCategory + ")";
                                break;
                            }
                        case SQLCommand.ImportVoucher.IsBankExists:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.IsBankExists;
                                dataManager.Parameters.Add(this.AppSchema.Bank.BANKColumn, BankName);
                                dataManager.Parameters.Add(this.AppSchema.Bank.BRANCHColumn, Branch);
                                TypeofExists = Id.Bank.ToString() + " (" + BankName + "," + Branch + ")";
                                break;
                            }
                        //case SQLCommand.ImportVoucher.IsBankCodeExists:
                        //    {
                        //        dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.IsBankCodeExists;
                        //        dataManager.Parameters.Add(this.AppSchema.Bank.BANK_CODEColumn, BankCode);
                        //        break;
                        //    }
                        case SQLCommand.ImportVoucher.IsPurposeExists:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.IsPurposeExists;
                                dataManager.Parameters.Add(this.AppSchema.Purposes.FC_PURPOSEColumn, PurposeName);
                                TypeofExists = Id.Purpose.ToString() + " (" + PurposeName + ")";
                                break;
                            }
                        case SQLCommand.ImportVoucher.IsCountryExists:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.IsCountryExists;
                                dataManager.Parameters.Add(this.AppSchema.Country.COUNTRYColumn, CountryName);
                                TypeofExists = Id.Country.ToString() + " (" + CountryName + ")";
                                break;
                            }
                        //case SQLCommand.ImportVoucher.IsLedgerCodeExists:
                        //    {
                        //        dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.IsLedgerCodeExists;
                        //        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_CODEColumn, LedgerCode);
                        //        break;
                        //    }
                        case SQLCommand.ImportVoucher.IsLedgerExists:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.IsLedgerExists;
                                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, LedgerName);

                                TypeofExists = Id.Ledger.ToString() + " (" + LedgerName + ")";
                                break;
                            }
                        case SQLCommand.ImportVoucher.IsLedgerBankExists:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.IsLedgerBankExists;
                                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, AccountNumber);
                                TypeofExists = "Ledger Bank (" + AccountNumber + ")";
                                break;
                            }
                        case SQLCommand.ImportVoucher.IsCostCentreExists:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.IsCostCentreExists;
                                dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_NAMEColumn, CostCentreName);
                                TypeofExists = Id.CostCentre.ToString() + " (" + CostCentreName + ")";
                                break;
                            }
                        case SQLCommand.ImportVoucher.IsLedgerGroupExists:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.IsLedgerGroupExists;
                                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.LEDGER_GROUPColumn, LedgerGroup);
                                TypeofExists = Id.LedgerGroup.ToString() + " (" + LedgerGroup + ")";
                                break;
                            }
                        case SQLCommand.ImportVoucher.IsLegalEntityExists: //12/04/2017, to check legal entity exists in db..for project import and export
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.IsLegalEntityExists;
                                dataManager.Parameters.Add(this.AppSchema.LegalEntity.SOCIETYNAMEColumn, SocietyName);
                                TypeofExists = Id.LegalEntity.ToString() + " (" + SocietyName + ")";
                                break;
                            }
                        case SQLCommand.ImportVoucher.IsGSTClassExists:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.IsGSTClassExists;
                                dataManager.Parameters.Add(this.AppSchema.MasterGSTClass.SLABColumn, Slab);
                                TypeofExists = Id.GSTClass.ToString() + " (" + Slab + ")";
                                break;
                            }
                        case SQLCommand.ImportVoucher.IsVendorExists:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.IsVendorExists;
                                dataManager.Parameters.Add(this.AppSchema.Vendors.VENDORColumn, Vendor);
                                TypeofExists = Id.Vendor.ToString() + " (" + Vendor + ")";
                                break;
                            }
                        case SQLCommand.ImportVoucher.IsStateExists:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.IsStateExists;
                                dataManager.Parameters.Add(this.AppSchema.State.STATE_NAMEColumn, StateName);
                                TypeofExists = Id.State.ToString() + " (" + StateName + ")";
                                break;
                            }
                        case SQLCommand.ImportVoucher.IsLedgerProfileExists:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.IsLedgerProfileExists;
                                dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.LEDGER_IDColumn, LedgerId);
                                TypeofExists = "Ledger Profile (" + LedgerName + ")";
                                break;
                            }
                        case SQLCommand.ImportVoucher.IsVoucherExists:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.IsVoucherExists;

                                TypeofExists = "Voucher details ";
                                break;
                            }
                    }
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in IsExists: " + ex.ToString();
            }
            finally { }

            //On 22/02/2018, attach name of the project and voucher detailis in the error log
            if (!resultArgs.Success && string.IsNullOrEmpty(ProblemDataDetails))
            {
                ProblemDataDetails = "Problem in checking Master exists (" + TypeofExists + ")";
            }

            return resultArgs; ;
        }

        /// <summary>
        /// Mapping (Project Ledger Mapping, Project Donor Mapping, Project Cost Centre Mapping)
        /// </summary>
        /// <param name="queryId"></param>
        /// <returns></returns>
        public ResultArgs MapProject(SQLCommand.ImportVoucher queryId)
        {
            string TypeofMapping = string.Empty;

            try
            {
                using (DataManager dataManager = new DataManager(DataBaseTypeName))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.HOSQL;
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    switch (queryId)
                    {
                        case SQLCommand.ImportVoucher.MapProjectLedger:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.MapProjectLedger;
                                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                                TypeofMapping = LedgerName + " (" + Id.Ledger.ToString() + ")";
                                break;
                            }
                        case SQLCommand.ImportVoucher.MapProjectDonor:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.MapProjectDonor;
                                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.DONAUD_IDColumn, DonorId);
                                TypeofMapping = DonorName + " (" + Id.Donor.ToString() + ")";
                                break;
                            }
                        case SQLCommand.ImportVoucher.MapProjectCostcentre:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.MapProjectCostcentre;
                                dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, CostCenterId);
                                //On 30/11/2022, Set Project-wise/Ledger-wise Cost Centre Mapping
                                if (CostCentreMapping == 1)
                                    dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId, DataType.Int32);
                                else
                                    dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, 0, DataType.Int32);

                                TypeofMapping = CostCentreName + " (" + Id.CostCentre.ToString() + ")";
                                break;
                            }
                        case SQLCommand.ImportVoucher.MapCostCategory:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.MapCostCategory;
                                dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRECATEGORY_IDColumn, CostCategoryId);
                                dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, CostCenterId);
                                TypeofMapping = CostCategory + " (" + Id.CostCategory.ToString() + ")";
                                break;
                            }
                        case SQLCommand.ImportVoucher.MapProjectPurpose:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.MapProjectPurpose;
                                dataManager.Parameters.Add(this.AppSchema.Purposes.CONTRIBUTION_IDColumn, ContributionId);
                                TypeofMapping = PurposeName + " (" + Id.Purpose.ToString() + ")";
                                break;
                            }
                        case SQLCommand.ImportVoucher.MapProjectBudgetLedger:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.MapProjectBudgetLedger;
                                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                                TypeofMapping = LedgerName + " (Budget Ledger)";
                                break;
                            }
                    }
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in MapProject: " + ex.ToString();
            }
            finally { }

            if (!resultArgs.Success)
            {
                //On 22/02/2018, attach name of the project and voucher detailis in the error log
                ProblemDataDetails = "Project Name :" + ProjectName + "<br>" +
                                        "Mapping with " + TypeofMapping + "<br>" +
                                        "Problem in mapping Project";
                resultArgs.Message = "Problem in mapping Project (" + resultArgs.Message + ")";
            }
            return resultArgs;
        }

        /// <summary>
        /// On 27/02/2020 Mapping Sub Ledger with Main Ledger
        /// </summary>
        /// <returns></returns>
        public ResultArgs MapSubLedger()
        {
            string TypeofMapping = string.Empty;

            try
            {
                TypeofMapping = LedgerName + "-" + SubLedgerName + " (Sub Ledger)";
                using (DataManager dataManager = new DataManager(DataBaseTypeName))
                {
                    dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.MapSubLedger;
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.HOSQL;
                    dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                    dataManager.Parameters.Add(this.AppSchema.SubLedger.SUB_LEDGER_IDColumn, SubLedgerId);
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in MapSubLedger: " + ex.ToString();
            }
            finally { }

            if (!resultArgs.Success)
            {
                //On 22/02/2018, attach name of the project and voucher detailis in the error log
                ProblemDataDetails = "Project Name :" + ProjectName + "<br>" +
                                        "Mapping with " + TypeofMapping + "<br>" +
                                        "Problem in mapping Project";
                resultArgs.Message = "Problem in mapping Sub Ledger with Ledger (" + resultArgs.Message + ")";
            }
            return resultArgs;
        }

        public string GetProjectVoucherDetailsByDataSycErrorMessage(string headOfficeCode, string branchOfficeCode, string datasyncerrormessage)
        {
            string rtn = datasyncerrormessage.Trim();

            try
            {

                string pattern = @"The Record is Available '\d+-\d+-\d+'";
                Match m = Regex.Match(datasyncerrormessage, pattern, RegexOptions.IgnoreCase);
                if (m.Success && !string.IsNullOrEmpty(m.Value))
                {
                    string[] temp = Regex.Split(m.Value, @"'");
                    if (temp.Length == 3)
                    {
                        string[] digits = Regex.Split(temp.GetValue(1).ToString(), @"-");
                        if (digits.Length == 3)
                        {
                            Int32 branchid = 0; //NumberSet.ToInteger(digits.GetValue(0).ToString());
                            Int32 voucherid = NumberSet.ToInteger(digits.GetValue(1).ToString());
                            Int32 locationid = 0; //NumberSet.ToInteger(digits.GetValue(2).ToString());

                            ResultArgs result = FetchMasterByBranchLocationVoucherId(branchid, locationid, voucherid);
                            if (result != null && result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                            {
                                DataTable dtVoucher = result.DataSource.Table;

                                string vtype = dtVoucher.Rows[0][AppSchema.VoucherMaster.VOUCHER_TYPEColumn.ColumnName].ToString();
                                string remarkindetails = System.Environment.NewLine + "Local Branch Voucher " + System.Environment.NewLine;
                                remarkindetails += "Project           : " + dtVoucher.Rows[0][AppSchema.Project.PROJECTColumn.ColumnName].ToString() + System.Environment.NewLine;
                                remarkindetails += "Voucher Date  : " + DateSet.ToDate(dtVoucher.Rows[0][AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName].ToString(), false).ToShortDateString();
                                remarkindetails += "   Voucher No : " + dtVoucher.Rows[0][AppSchema.VoucherMaster.VOUCHER_NOColumn.ColumnName].ToString();
                                remarkindetails += "   Voucher Type : " + (vtype == "RC" ? "Receipts" : vtype == "PY" ? "Payments" :
                                                                    vtype == "CN" ? "Contra" : "Journal") + System.Environment.NewLine;
                                rtn += System.Environment.NewLine + remarkindetails;

                            }
                        }
                    }

                }
            }
            catch (Exception err)
            {

            }
            return rtn;
        }

        public ResultArgs FetchMasterByBranchLocationVoucherId(int BranchId, int LocationId, int VoucherID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.FetchMasterByBranchLocationVoucherId, SQLAdapterType.HOSQL))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.BRANCH_IDColumn, BranchId);
                dataManager.Parameters.Add("LOCATION_ID", LocationId, DataType.Int32);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherID);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// This is to get the Id of the requestd. (Project,Purpose,Ledger,Donor,Cost Centre,Bank,Country)
        /// </summary>
        /// <param name="queryId"></param>
        /// <returns></returns>
        private int GetId(Id queryId)
        {
            try
            {
                using (DataManager dataManager = new DataManager(DataBaseTypeName))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.HOSQL;
                    switch (queryId)
                    {
                        case Id.ProjectCategory:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.GetProjectCategoryId;
                                dataManager.Parameters.Add(this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn, ProjectCategoryName);
                                break;
                            }
                        case Id.Project:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.GetProjectId;
                                dataManager.Parameters.Add(this.AppSchema.Project.PROJECTColumn, ProjectName);
                                break;
                            }
                        case Id.Purpose:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.GetPurposeId;
                                dataManager.Parameters.Add(this.AppSchema.Purposes.FC_PURPOSEColumn, PurposeName);
                                break;
                            }
                        case Id.Ledger:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.GetLedgerId;
                                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, LedgerName);
                                break;
                            }
                        case Id.LedgerGroup:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.GetLedgerGroupId;
                                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.LEDGER_GROUPColumn, LedgerGroup);
                                break;
                            }
                        case Id.Donor:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.GetDonorId;
                                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.NAMEColumn, DonorName);
                                dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_IDColumn, BranchId);
                                break;
                            }
                        case Id.CostCentre:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.GetCostCentreId;
                                dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_NAMEColumn, CostCentreName);
                                break;
                            }
                        case Id.CostCategory:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.GetCostCategoryId;
                                dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_CATEGORY_NAMEColumn, CostCategory);
                                break;
                            }
                        case Id.Bank:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.GetBankId;
                                dataManager.Parameters.Add(this.AppSchema.Bank.BANKColumn, BankName);
                                if (!string.IsNullOrEmpty(Branch))
                                {
                                    dataManager.Parameters.Add(this.AppSchema.Bank.BRANCHColumn, Branch);
                                }
                                break;
                            }
                        case Id.BankAccount:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.GetBankAccountId;
                                dataManager.Parameters.Add(this.AppSchema.BankAccount.ACCOUNT_NUMBERColumn, AccountNumber);
                                dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_IDColumn, BranchId);
                                break;
                            }
                        case Id.Country:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.GetCountryId;
                                dataManager.Parameters.Add(this.AppSchema.Country.COUNTRYColumn, CountryName);
                                break;
                            }
                        case Id.LegalEntity:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.GetLegalEntityId;
                                dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.SocietyNameColumn, SocietyName);
                                break;
                            }
                        case Id.ParentGroup:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.GetParentGroupId;
                                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.LEDGER_GROUPColumn, ParentGroup);
                                break;
                            }
                        case Id.MainGroup:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.GetMainParentId;
                                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.LEDGER_GROUPColumn, MainGroup);
                                break;
                            }
                        case Id.Nature:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.GetNatureId;
                                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.NATUREColumn, Nature);
                                break;
                            }
                        case Id.State:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.GetStateId;
                                dataManager.Parameters.Add(this.AppSchema.State.STATE_NAMEColumn, StateName);
                                break;
                            }
                        case Id.SubLedger:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.GetSubLedgerId;
                                dataManager.Parameters.Add(this.AppSchema.SubLedger.SUB_LEDGER_NAMEColumn, SubLedgerName);
                                break;
                            }
                        case Id.FDAccount:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.GetFDAccountId;
                                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_NUMBERColumn, FDAccountNumber);
                                break;
                            }
                        case Id.StatisticsType:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.GetStatisticsTypeId;
                                dataManager.Parameters.Add(this.AppSchema.StatisticsType.STATISTICS_TYPEColumn, StatisticsTypeName);
                                break;
                            }
                        case Id.GSTClass:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.GetGSTSlabClassId;
                                dataManager.Parameters.Add(this.AppSchema.MasterGSTClass.SLABColumn, Slab);
                                break;
                            }
                        case Id.Vendor:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportVoucher.GetVendorId;
                                dataManager.Parameters.Add(this.AppSchema.Vendors.VENDORColumn, Vendor);
                                break;
                            }
                    }
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
                if (!resultArgs.Success)
                {
                    AcMEDataSynLog.WriteLog("Error in GetId: " + resultArgs.Message);
                }
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog(ex.Message);
            }
            finally { }

            return resultArgs.DataSource.Sclar.ToInteger != 0 ? resultArgs.DataSource.Sclar.ToInteger : 0;
        }

        private ResultArgs ValidateLicenseKey()
        {
            AcMEDataSynLog.WriteLog("ValidateLicenseKey Started.");
            int MapLedgers = 0;
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.FetchLatestLicense, DataBaseType.Portal, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_CODEColumn, HeadOfficeBranchCode);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                    if (resultArgs.Success)
                    {
                        DataTable dtLicenseInfo = resultArgs.DataSource.Table;
                        if (dtLicenseInfo != null && dtLicenseInfo.Rows.Count == 1)
                        {
                            MapLedgers = this.NumberSet.ToInteger(dtLicenseInfo.Rows[0]["MAP_LEDGER"].ToString());
                            IsMappingMandatory = (MapLedgers == 1);
                            if (MapLedgers > 0)
                            {
                                if (dtLedger != null && dtLedger.Rows.Count > 0)
                                {
                                    DataView dvLedger = dtLedger.DefaultView;
                                    dvLedger.RowFilter = "IS_BRANCH_LEDGER=1 AND GROUP_ID NOT IN (12,13)";  // To filter Branch Ledgers except Cash & Bank accounts
                                    if (dvLedger != null && dvLedger.Count > 0)
                                    {
                                        resultArgs.Message = "Branch office ledgers are not mapped with Head Office Ledgers" + Environment.NewLine +
                                            "It is made mandatory for your Branch that All Branch Ledgers should be mapped with Head Office Ledgers. " + Environment.NewLine +
                                            "Update the latest license key and try exporting vouchers to Portal.";
                                    }
                                }
                            }
                        }
                        else
                        {
                            resultArgs.Message = "License is not created in Portal.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog("Error in Validating License key: " + ex.Message);
                resultArgs.Message = ex.Message;
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ValidateLicenseKey Ended.");
            }
            return resultArgs;
        }

        /// <summary>
        /// This is to get the Transaction Date From and Date to from the Brnach office XMLfile
        /// This date from and to will be used to fetch the transaction in the date range for deletion.
        /// </summary>
        /// <returns></returns>
        private ResultArgs SetDateDuration(DataSet dsXML)
        {
            AcMEDataSynLog.WriteLog("SetDateDuration Started..");
            try
            {
                if (dsXML != null && dsXML.Tables.Count > 0)
                {
                    DataTable dtHeader = dsXML.Tables["Header"];
                    if (dtHeader != null && dtHeader.Rows.Count > 0)
                    {
                        DataRow drHeader = dtHeader.Rows[0];
                        if ((!string.IsNullOrEmpty(drHeader["DATE_FROM"].ToString())) && (!string.IsNullOrEmpty(drHeader["DATE_TO"].ToString())))
                        {
                            DateFrom = this.DateSet.ToDate(drHeader["DATE_FROM"].ToString(), false);
                            DateTo = this.DateSet.ToDate(drHeader["DATE_TO"].ToString(), false);
                            AcMEDataSynLog.WriteLog("Date Duration: " + DateFrom.ToShortDateString() + " to " + DateTo.ToShortDateString());
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(DateFrom.ToString()))
                            {
                                resultArgs.Message = "Transaction Date From is Empty";
                            }
                            else if (string.IsNullOrEmpty(DateTo.ToString()))
                            {
                                resultArgs.Message = "Transaction Date To is Empty";
                            }
                        }

                        //For Local Project Split, we introduced isFYSplit colunmn
                        if (resultArgs.Success && dtHeader != null)
                        {
                            if (dtHeader.Columns.Contains("IsFYSplit"))
                            {
                                //IsFYSplit = Boolean.TryParse(drHeader["IsFYSplit"].ToString().Trim(), out IsFYSplit);
                                bool check = Boolean.TryParse(dtHeader.Rows[0]["IsFYSplit"].ToString().Trim(), out IsFYSplit);
                            }
                        }
                    }
                    else
                    {
                        resultArgs.Message = "Transaction Date from and Date to is empty.";
                    }
                }
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog("Problem in SetDateDuration :" + ex.Message);
                resultArgs.Message = ex.Message;
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("SetDateDuration Ended.");
            }
            return resultArgs;
        }

        /// <summary>
        /// This is to get the Location from the Brnach office XMLfile
        /// </summary>
        /// <returns></returns>
        private ResultArgs SetLocation(DataSet dsXML)
        {
            AcMEDataSynLog.WriteLog("SetLocation Started..");
            try
            {
                if (dsXML != null && dsXML.Tables.Count > 0)
                {
                    DataTable dtHeader = dsXML.Tables["Header"];
                    if (dtHeader != null && dtHeader.Rows.Count > 0)
                    {
                        DataRow drHeader = dtHeader.Rows[0];
                        if (dtHeader.Columns.Contains("LOCATION"))
                        {
                            Location = drHeader["LOCATION"].ToString();
                            resultArgs = CommonMethod.DecreptWithResultArg(Location);
                            if (resultArgs.Success && resultArgs.ReturnValue != null)
                            {
                                Location = resultArgs.ReturnValue.ToString();
                                if (Location != DefaultLocation.Primary.ToString())
                                {
                                    resultArgs = FetchLocationId();
                                }
                            }
                        }
                    }
                    else
                    {
                        resultArgs.Message = "Branch Information (Branch Code, Head Office Code, Date From, Date To) is Empty.";
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("SetLocation Ended.");
            }
            return resultArgs;
        }

        /// <summary>
        /// This is to get the Import Type from the Brnach office XMLfile
        /// </summary>
        /// <returns></returns>
        private ResultArgs SetImportType(DataSet dsXML)
        {
            AcMEDataSynLog.WriteLog("SetImportType Started..");
            try
            {
                if (dsXML != null && dsXML.Tables.Count > 0)
                {
                    DataTable dtHeader = dsXML.Tables["Header"];
                    if (dtHeader != null && dtHeader.Rows.Count > 0)
                    {
                        DataRow drHeader = dtHeader.Rows[0];
                        if (dtHeader.Columns.Contains("IMPORT_TYPE"))
                        {
                            string sImportType = drHeader["IMPORT_TYPE"].ToString();
                            VoucherImportType = (sImportType == ImportType.HeadOffice.ToString() ? ImportType.HeadOffice : sImportType == ImportType.SubBranch.ToString() ? ImportType.SubBranch : ImportType.SplitProject);
                        }
                        else
                        {
                            resultArgs.Message = "Import Type is empty. Update latest version of Acme.erp in Branch Office and try again.";
                        }
                    }
                    else
                    {
                        resultArgs.Message = "Branch Information (Branch Code, Head Office Code, Date From, Date To) is Empty.";
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("SetImportType Ended.");
            }
            return resultArgs;
        }

        /// <summary>
        /// This method is to update the Synchronization status in the Portal Database.
        /// </summary>
        /// <param name="status">1. InProgress</param>
        /// <param name="status">2. Closed</param>
        /// <param name="status">1. Failed</param>
        /// <param name="Remark">Remarks(Actual problem could be)</param>
        /// <returns>ResultArgs with its properties</returns>
        private ResultArgs UpdateDataSynStatus(DataSyncMailType status, string Remark)
        {
            string StatusRemark = Remark.Replace("<br/>", string.Empty).Replace("<b>", string.Empty).Replace("</b>", string.Empty);
            ResultArgs result = new ResultArgs();
            AcMEDataSynLog.WriteLog("UpdateDataSyncStatus Started: " + status.ToString());
            //On 22/02/2018, when probelm occured, update real error and probelm data also lilke project name and voucher details
            Remark += ProblemDataDetails.Replace("<br>", ".");
            try
            {
                using (DataManager dataManager = new DataManager(IsClientBranch ? SQLCommand.ImportVoucher.UpdateSubBranchDsyncStatus : SQLCommand.ImportVoucher.UpdateDataSynStatus,
                    DataBaseType.Portal, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.DataSyncStatus.HEAD_OFFICE_IDColumn, HeadOfficeId);
                    dataManager.Parameters.Add(this.AppSchema.DataSyncStatus.BRANCH_OFFICE_IDColumn, BranchOfficeId);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);
                    dataManager.Parameters.Add(this.AppSchema.DataSyncStatus.XML_FILENAMEColumn, XmlFileName);
                    dataManager.Parameters.Add(this.AppSchema.DataSyncStatus.STATUSColumn, (int)status);
                    dataManager.Parameters.Add(this.AppSchema.DataSyncStatus.REMARKSColumn, StatusRemark);
                    //dataManager.DataCommandArgs.IsDirectReplaceParameter = false;
                    result = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog("Error in Updating Data Synchronization Status: " + ex.ToString());
                resultArgs.Message = ex.Message;
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("UpdateDataSynStatus Ended.");
            }
            return resultArgs;
        }

        private ResultArgs SendMail(DataSyncMailType Status, string Message)
        {
            ResultArgs result = new ResultArgs();
            string Header = string.Empty;
            string MainContent = string.Empty;

            string Name = "Branch Admin";
            if (Status == DataSyncMailType.Closed)
            {
                Header = "Your Branch Office Data has been updated in Acmeerp Portal Successfully." +
                         " <br /> You can login to your Branch Office and view your Branch vouchers in Portal. <br />";
            }
            else
            {
                //On 22/02/2018, attach problem data details in failure mail
                Header = "Your Branch Office Data has not been updated in Acmeerp Portal due to the following reason.<br/>" +
                          "<b>" + ProblemDataDetails + "<br> Error Message : " + Message + "</b>" +
                         "<br> Please Contact ACME ERP Portal Administrator for further assistance.<br/>";
            }


            MainContent = "<b>Your Voucher File details are as follows:</b><br/><br/>" +
                                 "Head Office Code   : <b>" + HeadOfficeCode + "</b><br/>" +
                                 "Branch Office Name : <b>" + BranchOfficeName + " (" + HeadOfficeBranchCode + " )</b><br/>" +
                                 "Vouchers Duration  : <b>" + DateFrom.ToString("dd-MMM-yyyy") + "</b> To <b>" + DateTo.ToString("dd-MMM-yyyy") + "</b><br/>";

            if (Status == DataSyncMailType.Closed)
            {
                //Send Project Details if Datasync success
                MainContent += "<b><h3>Project Details</h3></b><br/>" +
                  GetUploadedProjectDataStatus() + "<br/>";
            }


            string content = Common.GetMailTemplate(Header, MainContent, Name);

            if (!string.IsNullOrEmpty(BranchEmailId))
            {
                result = Common.SendEmail(Common.GetFirstValue(BranchEmailId), Common.RemoveFirstValue(BranchEmailId),
                    (Status == DataSyncMailType.Closed) ? "Branch Office Data Updation Success." : "Branch Office Data Updation Failure", content, true);
                if (result.Success)
                {
                    AcMEDataSynLog.WriteLog("Mail has been communicated to the concerned Person");
                }
                else
                {
                    AcMEDataSynLog.WriteLog("Problem in Sending mail." + result.Message);
                }
            }
            else
            {
                AcMEDataSynLog.WriteLog("Branch Office Email Address is Empty. Could not send email.");
            }

            return result;
        }


        public ResultArgs IsGeneralLedger(int LedgerId)
        {
            ResultArgs result = new ResultArgs();
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.IsGeneralLedger, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in checking Is General Ledger : " + ex.ToString();
            }
            finally { }

            if (!resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("Problem in checking Is General Ledger " + result.Message);
            }
            return resultArgs;
        }

        private ResultArgs RefreshTransBalance()
        {
            try
            {
                using (BalanceSystem balanceSystem = new BalanceSystem())
                {
                    balanceSystem.VoucherDate = DateFrom.ToString();
                    resultArgs = balanceSystem.UpdateBulkTransBalance(BranchId, true);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally
            {
                if (!resultArgs.Success)
                {
                    AcMEDataSynLog.WriteLog("Problem in Refreshing Balance");
                }
            }
            return resultArgs;
        }

        private ResultArgs FetchBranchProjects()
        {
            ResultArgs result = new ResultArgs();
            using (DataManager datamanager = new DataManager(SQLCommand.ImportVoucher.FetchBranchProjects, DataBaseTypeName, SQLAdapterType.HOSQL))
            {
                datamanager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);

                result = datamanager.FetchData(DataSource.DataTable);
            }
            return result;
        }

        private ResultArgs FetchProjects()
        {
            ResultArgs result = new ResultArgs();
            using (DataManager datamanager = new DataManager(SQLCommand.ImportVoucher.FetchProjects, DataBaseTypeName, SQLAdapterType.HOSQL))
            {
                result = datamanager.FetchData(DataSource.DataTable);
            }
            return result;
        }

        private string GetUploadedProjectDataStatus()
        {
            StringBuilder html = new StringBuilder();
            DataTable dtProjects = new DataTable();
            ResultArgs result = FetchBranchProjects();
            if (result != null && result.Success && result.RowsAffected > 0)
            {
                dtProjects = result.DataSource.Table;
                if (dtProjects != null)
                {
                    foreach (DataRow drProject in dtProject.Rows)
                    {

                        for (int i = 0; i < dtProjects.Rows.Count; i++)
                        {
                            if (dtProjects.Rows[i][this.AppSchema.Project.PROJECTColumn.ColumnName].ToString().Trim().Equals
                                (drProject[this.AppSchema.Project.PROJECTColumn.ColumnName].ToString().Trim()))
                            {
                                dtProjects.Rows[i]["STATUS"] = "1";
                            }
                            dtProjects.AcceptChanges();
                        }

                    }
                    //Table start.
                    html.Append("<table border = '1'>");

                    //Building the Header row.
                    html.Append("<tr>");
                    foreach (DataColumn column in dtProjects.Columns)
                    {
                        html.Append("<th>");
                        html.Append(column.ColumnName);
                        html.Append("</th>");
                    }
                    html.Append("</tr>");

                    //Building the Data rows.
                    foreach (DataRow row in dtProjects.Rows)
                    {
                        html.Append("<tr>");
                        foreach (DataColumn column in dtProjects.Columns)
                        {
                            if (column.ColumnName.Equals("STATUS"))
                            {
                                if (row[column.ColumnName].ToString().Equals("1"))
                                {
                                    //Set Bgcolor to green
                                    //html.Append("<td bgcolor='#008000'>");
                                    html.Append("<td align='center'>");
                                    html.Append("<font color='#008000'>&#10004;</font>");
                                }
                                else
                                {
                                    //html.Append("<td bgcolor='#FF0000'>");
                                    html.Append("<td>"); html.Append(" ");
                                }
                                //html.Append(row[column.ColumnName]);
                                html.Append("</td>");
                            }
                            else
                            {
                                html.Append("<td>");
                                html.Append(row[column.ColumnName]);
                                html.Append("</td>");
                            }
                        }
                        html.Append("</tr>");
                    }

                    //Table end.
                    html.Append("</table>");
                }
            }
            return html.ToString();
        }

        /// <summary>
        /// On 24/09/2021, 
        /// </summary>
        /// <returns></returns>
        private ResultArgs IsAuditVouchersLocked(Int32 pId, DateTime frmDate, DateTime toDate)
        {
            ResultArgs result = new ResultArgs();
            result.Success = true;
            AcMEDataSynLog.WriteLog("Import Audit Voucher Started..");
            try
            {
                using (DataManager datamanager = new DataManager(SQLCommand.ImportVoucher.IsAuditVouchersLocked, DataBaseTypeName, SQLAdapterType.HOSQL))
                {
                    datamanager.Parameters.Add(this.AppSchema.Voucher.PROJECT_IDColumn, pId);
                    datamanager.Parameters.Add(this.AppSchema.FDRegisters.DATE_FROMColumn, frmDate);
                    datamanager.Parameters.Add(this.AppSchema.FDRegisters.DATE_TOColumn, toDate);
                    result = datamanager.FetchData(DataSource.DataTable);

                    if (result != null && result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                    {
                        string lockedmessage = "Not able to Import Vouchers, Voucher is locked for '" + ProjectName + "'" +
                                           " during the period of " + DateSet.ToDate(frmDate.ToShortDateString()) + " - " + DateSet.ToDate(toDate.ToShortDateString());
                        result.Message = lockedmessage;
                        result.Success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Message = "Not able to check for Audit Lock Vouchers " + ex.Message;
            }
            finally { }

            if (result.Success)
            {
                AcMEDataSynLog.WriteLog("Import Audit Voucher Ended.");
            }
            else
            {
                AcMEDataSynLog.WriteLog("Problem in IsAuditVouchersLocked : " + result.Message);
            }
            return result;
        }


        /// <summary>
        /// on 04/01/2022, For Project Import and Export, if it is mapping ledgers is enabled
        /// To check Project imported ledger is mapped with existing ledger in target database, If so
        /// instead of taking ledger name from XML, take mapped existing ledger name.
        /// </summary>
        private string ReplaceMappedLedgerName(string xmlledgername)
        {
            string rtn = xmlledgername;
            if (IsShowMappingLedgers && MappedLedgers != null && MappedLedgers.Rows.Count > 0)
            {
                var varResult = (from drmappedrow in MappedLedgers.AsEnumerable()
                                 where drmappedrow["LEDGER_NAME"].ToString() == xmlledgername
                                 select drmappedrow);
                if (varResult != null && varResult.Count() > 0)
                {
                    DataTable dtResult = varResult.CopyToDataTable();
                    rtn = dtResult.Rows[0]["MERGE_LEDGER_NAME"].ToString().Trim();
                }
            }
            //----------------------------------------------------------------------------------------
            return rtn;
        }
        #endregion

        #endregion
    }
}
