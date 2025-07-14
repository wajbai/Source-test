using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using Bosco.Model.UIModel;

namespace Bosco.Model.Dsync
{
    public class ExportVoucherSystem : SystemBase
    {
        #region Decelaration
        ResultArgs resultArgs = null;
        CommonMethod UtilityMethod = new CommonMethod();
        public DataSet dsTransaction = new DataSet("Vouchers");
        //Transactions Variables
        private const string PROJECT_TABLE_NAME = "Project";
        private const string MERGE_PROJECT_NAME = "MergeProject";
        //private const string MERGE_PROJECT_TABLE_NAME = "MergeProject";
        private const string LEGAL_ENTITY_TABLE_NAME = "LegalEntity";
        private const string MERGE_PROJECT_ID = "MERGE_PROJECT_ID";
        private const string MASTER_TABLE_NAME = "VoucherMasters";
        private const string TRANSACTION_TABLE_NAME = "VoucherTransaction";
        private const string COSTCENTRE_TABLE_NAME = "VoucherCostCentre";
        private const string DONOR_TABLE_NAME = "Donors";
        private const string BANKDETAILS_TABLE_NAME = "BankDetails";
        private const string LEDGERBALANCE_TABLE_NAME = "LedgerBalance";
        private const string COUNTRY_TABLE_NAME = "Country";
        private const string STATE_TABLE_NAME = "State";
        private const string HEADOFFICE_LEDGER_TABLE_NAME = "Ledger";
        private const string LEDGER_PROFILE_TABLE_NAME = "LedgerProfile";
        private const string LEDGER_GROUP_TABLE_NAME = "LedgerGroup";
        private const string VOUCHERS_SUB_LEDGER_TABLE_NAME = "VoucherSubLedger";
        private const string PROJECT_COST_CENTRES = "Project_CostCentres";
        private const string PROJECT_DONORS = "Project_Donors";

        //FD Transaction Variables
        public const string FD_ACCOUNT_TABLE_NAME = "FD_Investment_Account";
        public const string FD_RENEWAL_TABLE_NAME = "FD_Renewal";
        public const string FD_VOUCHER_MASTER_TRANS_TABLE_NAME = "FD_Voucher_Master_Trans";
        public const string FD_VOUCHER_TRANS_TABLE_NAME = "FD_Voucher_Trans";
        public const string PROJECT_LEDGERS = "Project_Ledgers";
        public const string MASTER_GST_CLASS = "Master_GST_Class";
        public const string ASSET_STOCK_VENDORS = "Asset_Stock_Vendor";

        // private const string FD_BANK_ACCOUNT_DETAILS_TABLE_NAME = "FD_Bank_Account_Details";
        // private const string FD_BANK_DETAILS_TABLE_NAME = "FD_Bank_Details";

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

        #region Properties
        public int BranchId { get; set; }
        public string ProjectId { get; set; }
        //   public int MergeProjectId { get; set; }
        //  public string HeadOfficeCode { get; set; }
        // public string BranchOfficeCode { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int HeadOfficeId { get; set; }
        public int BranchOfficeId { get; set; }
        public ImportType VoucherImportType = ImportType.HeadOffice;

        //01/07/2020, Project split is just between date range or FYs split
        public bool IsFYSplit = false;

        //On 20/05/2020, to have option for master settings when we export
        public bool IsExportMasterAlone = false;
        public bool IsExportCasBankFDAlone = false;

        //04/01/2022, to show or not Ledger Mapping
        public bool IsShowMappingLedgers = false;
        #endregion

        #region Methods
        public ResultArgs CheckPrimaryLedgerGroup()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.CheckPrimaryLedgerGroup))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchProjectsLookup()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchProjects))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchHOBranchList()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchHOBranchList))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchHOBranchProjects()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchHOBranchProjects))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs HeadOfficeLedgers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchHeadOfficebyLedger))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs CheckHOLedgerExists()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.CheckHeadofficeLedgerExists))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs MapBranchHeadOfficeLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.MapBrachHeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, BranchOfficeId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.HEADOFFICE_LEDGER_IDColumn, HeadOfficeId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        //On 15/07/2017, commented by alawr (unused)
        //public ResultArgs FetchActiveTransactionPeriod()
        //{
        //    using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchActiveTransactionperiod))
        //    {
        //        resultArgs = dataManager.FetchData(DataSource.DataTable);
        //    }
        //    return resultArgs;
        //}

        public ResultArgs ValidateTransactions()
        {
            try
            {
                resultArgs = ValidateVoucherMaster();
                DataTable dtVoucherMaster = resultArgs.DataSource.Table;
                resultArgs = ValidateFDVoucherMasterTrans();
                DataTable dtFDVoucherMaster = resultArgs.DataSource.Table;

                //On 10/04/2021, for project splitting, if FY option is enabled, allow evern there is no vocuher entires
                //It will help to get projects and its ledgders
                if (VoucherImportType != ImportType.SplitHOBranchProject && !IsFYSplit)
                {
                    if ((dtVoucherMaster == null || dtVoucherMaster.Rows.Count == 0)
                        && (dtFDVoucherMaster == null || dtFDVoucherMaster.Rows.Count == 0))
                    {
                        resultArgs.Message = "Vouchers are not available for the duration.";
                    }
                }

            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("Validating Transaction Ended");
            }
            return resultArgs;
        }

        public ResultArgs FillExportVoucherTransaction()
        {
            try
            {
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.BeginTransaction();

                    // This is to enabled while coming from support Project to export
                    //this.HeadofficeCode = "sccgsb";
                    //this.BranchOfficeCode = "socpr0007";

                    resultArgs = (VoucherImportType == ImportType.SplitHOBranchProject) ? ConstructHOBranchHeaderData() : ConstructHeaderData(); // Construct Header
                    if (resultArgs.Success)
                    {
                        resultArgs = FetchProjects();
                        if (resultArgs.Success)
                        {
                            ////Added by Carmel Raj M on July-08-2015
                            //resultArgs = AddMergeProjectDetails(); //Construct Merge Project Details 
                            if (VoucherImportType.Equals(ImportType.SplitProject))
                            {
                                if (resultArgs.Success)
                                {
                                    //Added by Carmel Raj M on August-18-2015
                                    //Purpose : Exporting Ledgel Entity details
                                    resultArgs = FetchLegalEntityDetails();
                                }
                            }
                            if (resultArgs.Success)
                            {
                                resultArgs = ValidateTransactions(); // Validate Raw & FD Voucher Transaction
                                if (resultArgs.Success)
                                {
                                    resultArgs = FetchVoucherMaster();   //Raw Voucher Master Transaction
                                    if (resultArgs.Success)
                                    {
                                        resultArgs = (VoucherImportType == ImportType.SplitProject || VoucherImportType == ImportType.SplitHOBranchProject) ? FetchHOBranchVoucherTransactions() : FetchVoucherTransactions();   //Raw Voucher Transactions
                                        if (resultArgs.Success)
                                        {
                                            resultArgs = (VoucherImportType == ImportType.SplitProject || VoucherImportType == ImportType.SplitHOBranchProject) ? FetchHOBranchVoucherCostCentres() : FetchVoucherCostCentres();   //Raw Voucher Cost Centre
                                            if (resultArgs.Success)
                                            {
                                                resultArgs = FetchDonors(); //Donor Details
                                                if (resultArgs.Success)
                                                {
                                                    //On 16/06/2021, to have bank accounts even it does not have opening or transactions
                                                    //resultArgs = FetchBankDetails(); //Bank Details
                                                    resultArgs = (VoucherImportType == ImportType.SplitProject || VoucherImportType == ImportType.SplitHOBranchProject) ? FetchBankDetailsForSplitProject() : FetchBankDetails();
                                                    if (resultArgs.Success)
                                                    {
                                                        resultArgs = (VoucherImportType == ImportType.SplitProject || VoucherImportType == ImportType.SplitHOBranchProject) ? FetchSplitProjectLedgerBalance() : FetchLedgerBalance(); // Ledger Balance Details
                                                        if (resultArgs.Success)
                                                        {
                                                            resultArgs = FetchCountry(); // Country Details
                                                            if (resultArgs.Success)
                                                            {
                                                                resultArgs = FetchLedgerGroup();
                                                                if (resultArgs.Success)
                                                                {
                                                                    resultArgs = (VoucherImportType == ImportType.SplitProject || VoucherImportType == ImportType.SplitHOBranchProject) ? FetchHOBranchLedger() : FetchHeadOfficeLedger(); // Head office Ledger Details
                                                                    if (resultArgs.Success)
                                                                    {
                                                                        resultArgs = (VoucherImportType == ImportType.SplitProject || VoucherImportType == ImportType.SplitHOBranchProject) ? FetchHOBranchFDAccountInvestments() : FDAccountInvestments(); // FD Account Details
                                                                        if (resultArgs.Success)
                                                                        {
                                                                            resultArgs = (VoucherImportType == ImportType.SplitProject || VoucherImportType == ImportType.SplitHOBranchProject) ? FetchHOBranchFDRenewals() : FDRenewals(); // FD Renewals
                                                                            if (resultArgs.Success)
                                                                            {
                                                                                resultArgs = FDVoucherMasterTrans(); // FD Vocher Master Transactions
                                                                                if (resultArgs.Success)
                                                                                {
                                                                                    resultArgs = (VoucherImportType == ImportType.SplitProject || VoucherImportType == ImportType.SplitHOBranchProject) ? FetchHOBranchFDVoucherTrans() : FDVoucherTrans();

                                                                                    //On 10/04/2021, for FY split enabled, export project ledgers too as when project imports
                                                                                    //maps transactions ledgers alone.
                                                                                    //if (resultArgs.Success && VoucherImportType == ImportType.SplitProject && IsFYSplit)
                                                                                    if (resultArgs.Success && VoucherImportType == ImportType.SplitProject)
                                                                                    {
                                                                                        resultArgs = FetchProjectLedgers();

                                                                                        //16/06/2021
                                                                                        if (resultArgs.Success && VoucherImportType == ImportType.SplitProject)
                                                                                        {
                                                                                            //06/02/2024, For tmep (Training Program)
                                                                                            if (!IS_SDB_INM) resultArgs = FetchBudgetModuleDetails();

                                                                                            if (resultArgs.Success)
                                                                                            {
                                                                                                //On 09/07/2021, to get asset stock vendor details
                                                                                                resultArgs = FetchAssetStockVendors();
                                                                                            }
                                                                                        }

                                                                                        //On 01/02/2024, To get project mapped costcentres also
                                                                                        if (resultArgs.Success && VoucherImportType == ImportType.SplitProject &&
                                                                                            IsFYSplit)
                                                                                        {
                                                                                            //On 05/02/3034, To GST details
                                                                                            if (resultArgs.Success)
                                                                                            {
                                                                                                resultArgs = FetchGSTModuleDetails();
                                                                                                if (resultArgs.Success)
                                                                                                {
                                                                                                    resultArgs = FetchProjectCostCentres();
                                                                                                    //On 01/02/2024, To get project mapped Donors also
                                                                                                    if (resultArgs.Success)
                                                                                                    {
                                                                                                        resultArgs = FetchProjectDonors();
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
                                        }
                                    }
                                }
                            }
                        }
                    }
                    dataManager.EndTransaction();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                //On 11/01/2022, If show map ledger is enabled, export only ledgers has vouchers or budget
                if (VoucherImportType == ImportType.SplitProject && IsShowMappingLedgers && !IsExportMasterAlone)
                {
                    RemoveUnusedLedgersForShowMapping();
                }
                else if (VoucherImportType == ImportType.SplitProject && IsFYSplit)
                {
                    //AllowClosedLedgersBasedOnUsage(false); //For General Ledgers
                    //AllowClosedLedgersBasedOnUsage(true); //For Bank Ledgers
                }
                AcMELog.WriteLog("Exporting Vouchers Ended.");
            }
            else
            {
                resultArgs.Message = "Error in Export Vouchers. " + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// Construct Header data
        /// </summary>
        /// <returns></returns>
        private ResultArgs ConstructHOBranchHeaderData()
        {
            ResultArgs resultArgs = new ResultArgs();
            if (dsTransaction.Tables.Count == 0)
            {
                AcMELog.WriteLog("Constructs Header data Started...");
                DataTable dtBranch = new DataTable("Header");
                try
                {
                    DataColumn dcolDateFrom = new DataColumn("DATE_FROM", typeof(DateTime));
                    DataColumn dcolDateTo = new DataColumn("DATE_TO", typeof(DateTime));
                    DataColumn dcolUploadedBy = new DataColumn("UPLOADED_BY", typeof(string));
                    DataColumn dcolImportType = new DataColumn("IMPORT_TYPE", typeof(string));
                    DataColumn dcolLocation = new DataColumn("LOCATION", typeof(string));
                    DataColumn dcolKeyGeneratedDate = new DataColumn("KEY_GENERATED_DATE", typeof(string));

                    dtBranch.Columns.Add(dcolDateFrom);
                    dtBranch.Columns.Add(dcolDateTo);
                    dtBranch.Columns.Add(dcolUploadedBy);
                    dtBranch.Columns.Add(dcolImportType);
                    dtBranch.Columns.Add(dcolLocation);
                    dtBranch.Columns.Add(dcolKeyGeneratedDate);

                    dtBranch.Rows.Add(this.DateSet.ToDate(DateFrom.ToShortDateString(), false), this.DateSet.ToDate(DateTo.ToShortDateString(), false),
                        this.LoginUserName, (VoucherImportType == ImportType.HeadOffice ? ImportType.HeadOffice.ToString() : VoucherImportType == ImportType.SubBranch ? ImportType.SubBranch.ToString() : ImportType.SplitProject.ToString()),
                        CommonMethod.Encrept(this.Location), this.LicenseKeyGeneratedDate);
                    dsTransaction.Tables.Add(dtBranch);
                    resultArgs.Success = true;
                    AcMELog.WriteLog("Construct Header data Ended....");

                }
                catch (Exception ex)
                {
                    resultArgs.Message = ex.ToString();
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// Construct Header data
        /// </summary>
        /// <returns></returns>
        private ResultArgs ConstructHeaderData()
        {
            ResultArgs resultArgs = new ResultArgs();
            if (dsTransaction.Tables.Count == 0)
            {
                AcMELog.WriteLog("Constructs Header data Started...");
                DataTable dtBranch = new DataTable("Header");
                try
                {
                    DataColumn dcolHeadOfficeCode = new DataColumn("HEAD_OFFICE_CODE", typeof(string));
                    DataColumn dcolBranchOfficeCode = new DataColumn("BRANCH_OFFICE_CODE", typeof(string));
                    DataColumn dcolDateFrom = new DataColumn("DATE_FROM", typeof(DateTime));
                    DataColumn dcolDateTo = new DataColumn("DATE_TO", typeof(DateTime));
                    DataColumn dcolUploadedBy = new DataColumn("UPLOADED_BY", typeof(string));
                    DataColumn dcolImportType = new DataColumn("IMPORT_TYPE", typeof(string));
                    DataColumn dcolLocation = new DataColumn("LOCATION", typeof(string));
                    DataColumn dcolKeyGeneratedDate = new DataColumn("KEY_GENERATED_DATE", typeof(string));
                    DataColumn dcolIsMultiCurrency = new DataColumn("IS_MULTI_CURRENCY", typeof(Int32));

                    //01/07/2020, Project split is just between date range or FYs split
                    DataColumn dcolIsFYSplit = new DataColumn("ISFYSPLIT", typeof(bool));

                    //23/05/2021, To have Current FY from of selected Datefrom in XML file (To validate in Target Branch DB)
                    //It will used to for FD Vouchers
                    DataColumn dcolYearFrom = new DataColumn(AppSchema.AccountingPeriod.YEAR_FROMColumn.ColumnName, typeof(DateTime));
                    DateTime FYfrom = DateSet.GetFinancialYearByDate(DateSet.ToDate(this.YearFrom, false), this.YearFrom);

                    //04/01/2022, To show Project Ledger Mapping
                    DataColumn dcolIsShowLedgerMapping = new DataColumn(this.AppSchema.ProjectImportExport.SHOW_MAP_LEDGERSColumn.ColumnName, typeof(bool));

                    //08/02/2024, To show last voucher id 
                    /*DataColumn dcolLastVoucherId  = new DataColumn("LAST_VOUCHER_ID", typeof(Int32));
                    dcolLastVoucherId.DefaultValue = 0; 
                    Int32 LastVoucherId  = 0;
                    if (IsFYSplit)
                    {
                        LastVoucherId = FetchBranchLastVoucherId();
                    }*/

                    dtBranch.Columns.Add(dcolHeadOfficeCode);
                    dtBranch.Columns.Add(dcolBranchOfficeCode);
                    dtBranch.Columns.Add(dcolDateFrom);
                    dtBranch.Columns.Add(dcolDateTo);
                    dtBranch.Columns.Add(dcolUploadedBy);
                    dtBranch.Columns.Add(dcolImportType);
                    dtBranch.Columns.Add(dcolLocation);
                    dtBranch.Columns.Add(dcolKeyGeneratedDate);
                    dtBranch.Columns.Add(dcolIsMultiCurrency);
                    dtBranch.Columns.Add(dcolIsFYSplit);
                    dtBranch.Columns.Add(dcolYearFrom);
                    dtBranch.Columns.Add(dcolIsShowLedgerMapping);
                    //dtBranch.Columns.Add(dcolLastVoucherId);

                    // resultArgs = UtilityMethod.GetLicenseHeadofficeCode();
                    // if (resultArgs.Success && resultArgs.ReturnValue != null)
                    //  {
                    // HeadOfficeCode = this.BranchOfficeCode resultArgs.ReturnValue.ToString();
                    // resultArgs = UtilityMethod.GetLicenseBranchCode();
                    // if (resultArgs.Success && resultArgs.ReturnValue != null)
                    // {
                    //  BranchOfficeCode = resultArgs.ReturnValue.ToString();
                    if (!string.IsNullOrEmpty(this.HeadofficeCode) && !string.IsNullOrEmpty(this.BranchOfficeCode))
                    {
                        dtBranch.Rows.Add(CommonMethod.Encrept(this.HeadofficeCode), CommonMethod.Encrept(this.BranchOfficeCode), this.DateSet.ToDate(DateFrom.ToShortDateString(), false), this.DateSet.ToDate(DateTo.ToShortDateString(), false),
                            this.LoginUserName, (VoucherImportType == ImportType.HeadOffice ? ImportType.HeadOffice.ToString() : VoucherImportType == ImportType.SubBranch ? ImportType.SubBranch.ToString() : ImportType.SplitProject.ToString()),
                            CommonMethod.Encrept(this.Location), this.LicenseKeyGeneratedDate, this.AllowMultiCurrency, this.IsFYSplit, FYfrom, IsShowMappingLedgers);
                        dsTransaction.Tables.Add(dtBranch);
                        resultArgs.Success = true;
                        AcMELog.WriteLog("Construct Header data Ended....");

                        //On 06/02/2024, To export Finance Setting
                        if (resultArgs.Success)
                        {
                            resultArgs = AttachFinanceSetting();
                        }
                    }
                    else
                    {
                        resultArgs.Message = "HeadOffice Code or BranchOffice Code is Empty. ";
                    }
                    // }
                    // }
                }
                catch (Exception ex)
                {
                    resultArgs.Message = ex.ToString();
                }
            }
            return resultArgs;
        }


        /// <summary>
        /// 06/02/2024, Export Finance Settings which are enabled
        /// </summary>
        /// <returns></returns>
        private ResultArgs AttachFinanceSetting()
        {
            ResultArgs resultArgs = new ResultArgs();

            AcMELog.WriteLog("Constructs Attach Finance Setting data Started...");
            DataTable dtFinanceSetting = AppSchema.Setting.DefaultView.ToTable();
            dtFinanceSetting.TableName = FINANCE_SETTING;
            if (dtFinanceSetting.Columns[0].ColumnName == AppSchema.Setting.IdColumn.ColumnName)
            {
                dtFinanceSetting.Columns.Remove(dtFinanceSetting.Columns[0]);
            }
            try
            {
                //For Enable Options                
                if (this.UIProjSelection == "1") AttachFinanceSetting(dtFinanceSetting, FinanceSetting.UIProjSelection, this.UIProjSelection);
                if (this.EnableFlexiFD == "1") AttachFinanceSetting(dtFinanceSetting, FinanceSetting.EnableFlexiFD, this.EnableFlexiFD);
                if (this.EnableGST == "1") AttachFinanceSetting(dtFinanceSetting, FinanceSetting.EnableGST, this.EnableGST);
                if (this.IncludeGSTVendorInvoiceDetails != "0") AttachFinanceSetting(dtFinanceSetting, FinanceSetting.IncludeGSTVendorInvoiceDetails, this.IncludeGSTVendorInvoiceDetails);
                if (this.AllocateCCAmountWithGST == 1) AttachFinanceSetting(dtFinanceSetting, FinanceSetting.AllocateCCAmountWithGST, this.AllocateCCAmountWithGST.ToString());

                //For Entry Setting
                if (this.TransClose == "1") AttachFinanceSetting(dtFinanceSetting, FinanceSetting.UITransClose, this.TransClose);
                if (this.TransMode == "2") AttachFinanceSetting(dtFinanceSetting, FinanceSetting.EnableTransMode, this.TransMode); //Enable Dobule Entry
                if (this.TransEntryMethod == "2") AttachFinanceSetting(dtFinanceSetting, FinanceSetting.TransEntryMethod, this.TransEntryMethod);
                if (this.EnableTransMode == "1") AttachFinanceSetting(dtFinanceSetting, FinanceSetting.EnableTransMode, this.EnableTransMode);
                if (this.MaxCashLedgerAmountInReceiptsPayments > 0) AttachFinanceSetting(dtFinanceSetting, FinanceSetting.MaxCashLedgerAmountInReceiptsPayments, this.MaxCashLedgerAmountInReceiptsPayments.ToString());

                //For Budget Setting
                if (this.IncludeIncomeLedgersInBudget == "1") AttachFinanceSetting(dtFinanceSetting, FinanceSetting.IncludeIncomeLedgersInBudget, this.IncludeIncomeLedgersInBudget);
                if (this.IncludeBudgetStatistics == "1") AttachFinanceSetting(dtFinanceSetting, FinanceSetting.IncludeBudgetStatistics, this.IncludeBudgetStatistics);
                if (this.IncludeBudgetCCStrengthDetails == 1) AttachFinanceSetting(dtFinanceSetting, FinanceSetting.IncludeBudgetCCStrengthDetails, this.IncludeBudgetCCStrengthDetails.ToString());
                if (this.EnableCostCentreBudget == 1) AttachFinanceSetting(dtFinanceSetting, FinanceSetting.EnableCostCentreBudget, this.EnableCostCentreBudget.ToString());
                AttachFinanceSetting(dtFinanceSetting, FinanceSetting.ShowBudgetLedgerActualBalance, this.ShowBudgetLedgerActualBalance);

                //For Reports Setting
                if (this.CustomizationForm == "1") AttachFinanceSetting(dtFinanceSetting, FinanceSetting.CustomizationForm, this.CustomizationForm);
                if (this.ShowCr_DrAmountDrillingLedgerInAbstract == 1) AttachFinanceSetting(dtFinanceSetting, FinanceSetting.ShowCr_DrAmountDrillingLedgerInAbstract, this.ShowCr_DrAmountDrillingLedgerInAbstract.ToString());
                if (this.UIVoucherPrint == "1") AttachFinanceSetting(dtFinanceSetting, FinanceSetting.PrintVoucher, this.UIVoucherPrint);
                //if (this.VoucherPrint == "1") AttachFinanceSetting(dtFinanceSetting, FinanceSetting.PrintVoucher, this.VoucherPrint);
                AttachFinanceSetting(dtFinanceSetting, FinanceSetting.DuplicateCopyVoucherPrint, this.DuplicateCopyVoucherPrint);

                dsTransaction.Tables.Add(dtFinanceSetting);
                resultArgs.Success = true;
                AcMELog.WriteLog("Construct Attach Finance Setting data Ended....");
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            return resultArgs;
        }

        private void AttachFinanceSetting(DataTable dt, FinanceSetting financesetting, string value)
        {
            string[] objsettingRow = { financesetting.ToString(), value };
            dt.Rows.Add(objsettingRow);
        }

        /// <summary>
        /// Validate Raw Vocuher Master Transaction
        /// </summary>
        /// <returns></returns>
        private ResultArgs ValidateVoucherMaster()
        {
            try
            {
                AcMELog.WriteLog("ValidateVoucherTransactions Started");
                using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchMasterVouchers))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom.ToString("yyyy-MM-dd"));
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo.ToString("yyyy-MM-dd"));
                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                AcMELog.WriteLog("ValidateVoucherTransactions Ended.");
                resultArgs.DataSource.Table.TableName = MASTER_TABLE_NAME;
            }
            else
            {
                resultArgs.Message = "Error in ValidateVoucherTransactions. " + resultArgs.Message;
            }
            return resultArgs;
        }
        /// <summary>
        /// Validate Fixed Deposit Voucher Master Transaction 
        /// </summary>
        /// <returns></returns>
        private ResultArgs ValidateFDVoucherMasterTrans()
        {
            try
            {
                AcMELog.WriteLog("Validate FetchFDVoucherMasterTransactions Started");
                using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FDVoucherMasterTrans))
                {
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("Validate FetchFDVoucherMasterTransactions Ended.");
                resultArgs.DataSource.Table.TableName = FD_VOUCHER_MASTER_TRANS_TABLE_NAME;
            }
            else
            {
                resultArgs.Message = "Error in Validate FetchFDVoucherMasterTransactions: " + resultArgs.Message;
            }

            return resultArgs;
        }
        /// <summary>
        /// Fetch Raw Vocuher Master Transaction
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchVoucherMaster()
        {
            try
            {
                AcMELog.WriteLog("FetchVoucherTransactions Started");
                using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchMasterVouchers))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);
                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchVoucherTransactions Ended.");
                resultArgs.DataSource.Table.TableName = MASTER_TABLE_NAME;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
            }
            else
            {
                resultArgs.Message = "Error in FetchVoucherMasterTransactions. " + resultArgs.Message;
            }
            return resultArgs;
        }
        /// <summary>
        /// Fetch Vocuher Transaction Details
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchVoucherTransactions()
        {
            try
            {
                AcMELog.WriteLog("FetchVoucherTransactions Started");
                using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchVoucherTransactions))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchVoucherTransactions Ended.");
                resultArgs.DataSource.Table.TableName = TRANSACTION_TABLE_NAME;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
            }
            else
            {
                resultArgs.Message = "Error in FetchVoucherTransactions. " + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetch Vocuher Transaction Details
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchHOBranchVoucherTransactions()
        {
            try
            {
                AcMELog.WriteLog("FetchVoucherTransactions Started");
                using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchHOBranchVoucherTransactions))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);
                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchVoucherTransactions Ended.");
                resultArgs.DataSource.Table.TableName = TRANSACTION_TABLE_NAME;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
            }
            else
            {
                resultArgs.Message = "Error in FetchVoucherTransactions. " + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetch Voucher Cost Centre Details
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchVoucherCostCentres()
        {
            try
            {
                AcMELog.WriteLog("FetchVoucherCostCentres Started.");
                using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchVoucherCostCentres))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchVoucherCostCentres Ended.");
                resultArgs.DataSource.Table.TableName = COSTCENTRE_TABLE_NAME;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
            }
            else
            {
                resultArgs.Message = "Error in FetchVoucherCostCentres: " + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// 18/02/2020, Fetch Voucher Sub Ledgers
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchVoucherSubLedgers()
        {
            try
            {
                AcMELog.WriteLog("FetchVoucherSubLedgers Started.");
                using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchVoucherSubLedgers))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchVoucherSubLedgers Ended.");
                resultArgs.DataSource.Table.TableName = VOUCHERS_SUB_LEDGER_TABLE_NAME;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
            }
            else
            {
                resultArgs.Message = "Error in FetchVoucherSubLedgers: " + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetch Voucher Cost Centre Details
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchHOBranchVoucherCostCentres()
        {
            try
            {
                AcMELog.WriteLog("FetchVoucherCostCentres Started.");
                using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchHOBranchVoucherCostCenter))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);
                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchVoucherCostCentres Ended.");
                resultArgs.DataSource.Table.TableName = COSTCENTRE_TABLE_NAME;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
            }
            else
            {
                resultArgs.Message = "Error in FetchVoucherCostCentres: " + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetch  Donor Details
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchDonors()
        {
            try
            {
                AcMELog.WriteLog("FetchDonors Started.");
                using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchDonors))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchDonors Ended.");
                resultArgs.DataSource.Table.TableName = DONOR_TABLE_NAME;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
            }
            else
            {
                resultArgs.Message = "Error in FetchDonors: " + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetch  Donor Details
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchProjects()
        {
            try
            {
                AcMELog.WriteLog("FetchProjects Started.");
                using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchTransProjects))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    // dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                    // dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("Fetch Projects Ended.");
                resultArgs.DataSource.Table.TableName = PROJECT_TABLE_NAME;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
            }
            else
            {
                resultArgs.Message = "Error in FetchDonors: " + resultArgs.Message;
            }
            return resultArgs;
        }

        //private ResultArgs AddMergeProjectDetails()
        //{
        //    DataTable dtMergerProject = new DataTable(MERGE_PROJECT_TABLE_NAME);
        //    try
        //    {
        //        AcMELog.WriteLog("Fetch Merge Projects Started.");
        //        dtMergerProject.Columns.Add(MERGE_PROJECT_ID, typeof(System.Int32));
        //        DataRow dr = dtMergerProject.NewRow();
        //        dtMergerProject.Rows.Add(dr[MERGE_PROJECT_ID] = MergeProjectId);
        //    }
        //    catch (Exception ex)
        //    {
        //        resultArgs.Message = ex.ToString();
        //    }
        //    if (resultArgs.Success)
        //    {
        //        AcMELog.WriteLog("Fetch Merge Projects Ended.");
        //        dsTransaction.Tables.Add(dtMergerProject);
        //    }
        //    else
        //    {
        //        resultArgs.Message = "Error in Adding Merge Project: " + resultArgs.Message;
        //    }
        //    return resultArgs;
        //}

        /// <summary>
        /// Fetching Legal entity details
        /// </summary>
        /// <returns></returns>

        //Added by Carmel Raj M on August-18-2015
        private ResultArgs FetchLegalEntityDetails()
        {
            try
            {
                AcMELog.WriteLog("Fetch legal Entity Started.");
                using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchLegalEntity))
                {
                    dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("Fetch legal Entity Ended.");
                resultArgs.DataSource.Table.TableName = LEGAL_ENTITY_TABLE_NAME;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
            }
            else
            {
                resultArgs.Message = "Error in fetching legal Entity: " + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetch Bank Account and concern Bank Details
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchBankDetails()
        {
            try
            {
                AcMELog.WriteLog("FetchBankDetails Started.");
                using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchBankDetails))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                    // dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                    // dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchBankDetails Ended.");
                resultArgs.DataSource.Table.TableName = BANKDETAILS_TABLE_NAME;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
            }
            else
            {
                resultArgs.Message = "Error in FetchBankDetails: " + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// On 16/06/2021, to have bank accounts even it does not have opening or transactions
        /// Fetch Bank Account and concern Bank Details
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchBankDetailsForSplitProject()
        {
            try
            {
                AcMELog.WriteLog("FetchSplitProjectBankDetails Started.");
                using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchBankDetailsForSplitProject))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchSplitProjectBankDetails Ended.");
                resultArgs.DataSource.Table.TableName = BANKDETAILS_TABLE_NAME;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
            }
            else
            {
                resultArgs.Message = "Error in FetchSplitProjectBankDetails: " + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetch Ledger Opening Balance
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchLedgerBalance()
        {
            try
            {
                AcMELog.WriteLog("FetchLedgerBalance Started.");
                using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchLedgerBalance))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchLedgerBalance Ended.");
                resultArgs.DataSource.Table.TableName = LEDGERBALANCE_TABLE_NAME;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
            }
            else
            {
                resultArgs.Message = "Error in FetchLedgerBalance: " + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetch Ledger Opening Balance
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchSplitProjectLedgerBalance()
        {
            try
            {
                AcMELog.WriteLog("FetchLedgerBalance Started.");

                //On 30/03/2021, For split FY option is enabled, get only Cash/Bank Opening balance alone for given date from 
                SQLCommand.ExportVouchers sqlcmd = SQLCommand.ExportVouchers.FetchSplitProjectLedgerBalance;
                //if (IsFYSplit)
                if (VoucherImportType == ImportType.SplitProject)
                {
                    sqlcmd = SQLCommand.ExportVouchers.FetchSplitProjectLedgerBalanceForSplitFY;
                }

                using (DataManager dataManager = new DataManager(sqlcmd))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchLedgerBalance Ended.");
                resultArgs.DataSource.Table.TableName = LEDGERBALANCE_TABLE_NAME;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
            }
            else
            {
                resultArgs.Message = "Error in FetchLedgerBalance: " + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// On 05/07/20201, To get GST classs
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchMasterGSTClass()
        {
            try
            {
                AcMELog.WriteLog("FetchMasterGSTClassStarted.");

                //05/07/2021, To have GST Master Class
                if (resultArgs.Success)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchMasterGSTClass))
                    {
                        resultArgs = dataManager.FetchData(DataSource.DataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchMasterGSTClassEnded.");
                resultArgs.DataSource.Table.TableName = MASTER_GST_CLASS;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
            }
            else
            {
                resultArgs.Message = "Error in FetchMasterGSTClass: " + resultArgs.Message;
            }
            return resultArgs;
        }


        /// <summary>
        /// On 09/07/20201, To Asset & Stock Vendors
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchAssetStockVendors()
        {
            try
            {
                AcMELog.WriteLog("FetchAssetStockVendor.");

                //05/07/2021, To have Asset & Stock Vendors
                if (resultArgs.Success)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchAssetStockVendors))
                    {
                        resultArgs = dataManager.FetchData(DataSource.DataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchAssetStockVendor.");
                resultArgs.DataSource.Table.TableName = ASSET_STOCK_VENDORS;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
            }
            else
            {
                resultArgs.Message = "Error in FetchAssetStockVendor: " + resultArgs.Message;
            }
            return resultArgs;
        }


        /// <summary>
        /// Fetch Country Details
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchCountry()
        {
            try
            {
                AcMELog.WriteLog("FetchCountry Started.");
                using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchCountry))
                {
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchCountry Ended.");
                resultArgs.DataSource.Table.TableName = COUNTRY_TABLE_NAME;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);

                //As on 06/07/2021
                resultArgs = FetchState();
            }
            else
            {
                resultArgs.Message = "Error in FetchCountry: " + resultArgs.Message;
            }
            return resultArgs;
        }


        /// <summary>
        /// Fetch State Details
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchState()
        {
            try
            {
                AcMELog.WriteLog("FetchState Started.");
                using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchState))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchState Ended.");
                resultArgs.DataSource.Table.TableName = STATE_TABLE_NAME;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
            }
            else
            {
                resultArgs.Message = "Error in FetchState: " + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetch Branch  Office Ledgers which are mapped with Head Office Ledgers
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchHeadOfficeLedger()
        {
            try
            {
                AcMELog.WriteLog("FetchHeadOfficeLedger Started.");
                using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchHeadOfficeLedger))
                {
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchHeadOfficeLedger Ended.");
                resultArgs.DataSource.Table.TableName = HEADOFFICE_LEDGER_TABLE_NAME;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
            }
            else
            {
                resultArgs.Message = "Error in FetchHeadOfficeLedger: " + resultArgs.Message;
            }
            return resultArgs;
        }

        private ResultArgs FetchHOBranchLedger()
        {
            try
            {
                AcMELog.WriteLog("FetchHOBranchLedger Started.");

                //On 12/04/2021, To get all Ledgers for Split FY enabled
                SQLCommand.ExportVouchers sqlcmd = SQLCommand.ExportVouchers.FetchHOBranchLedger;
                //if (IsFYSplit) as on 30/04/2021
                if (VoucherImportType == ImportType.SplitProject) //To get all Master Ledgers
                {
                    sqlcmd = SQLCommand.ExportVouchers.FetchHOBranchLedgerSplitFY;
                }

                using (DataManager dataManager = new DataManager(sqlcmd))
                {
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchHOBranchLedger Ended.");
                resultArgs.DataSource.Table.TableName = HEADOFFICE_LEDGER_TABLE_NAME;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);

                //05/07/2021, To have GST Master Class and Ledger Profile

                if (VoucherImportType == ImportType.SplitProject) //To get all Master Ledgers
                {
                    if (resultArgs.Success)
                    {
                        resultArgs = FetchMasterGSTClass();
                        if (resultArgs.Success)
                        {
                            resultArgs = FetchLedgerProfileDetails();
                        }
                    }
                }
            }
            else
            {
                resultArgs.Message = "Error in FetchHOBranchLedger: " + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// On 06/07/2021, Ledger Profile Details
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchLedgerProfileDetails()
        {
            try
            {
                AcMELog.WriteLog("FetchLedgerProfileDetails Started.");

                using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchLedgerProfile))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchLedgerProfileDetails Ended.");
                resultArgs.DataSource.Table.TableName = LEDGER_PROFILE_TABLE_NAME;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
            }
            else
            {
                resultArgs.Message = "Error in FetchLedgerProfileDetails: " + resultArgs.Message;
            }
            return resultArgs;
        }

        private ResultArgs FetchLedgerGroup()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchLedgerGroup))
                {
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("Fetch Ledger Group Ended.");
                resultArgs.DataSource.Table.TableName = LEDGER_GROUP_TABLE_NAME;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
            }
            else
            {
                resultArgs.Message = "Error in Fetch Ledger Group: " + resultArgs.Message;
            }

            return resultArgs;
        }

        /// <summary>
        /// to get Mapped and UnMappedLedgers
        /// </summary>
        public ResultArgs FetchMapLedger()
        {
            AcMELog.WriteLog("Mapping Started.");
            using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.AllUnMappedMappedLedgers))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetch Fixed Deposit Account Investments
        /// </summary>
        /// <returns></returns>
        private ResultArgs FDAccountInvestments()
        {
            try
            {
                AcMELog.WriteLog("FetchFDAccountInvestment Started.");
                using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FDAccounts))
                {
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchFDAccountInvestment Ended.");
                resultArgs.DataSource.Table.TableName = FD_ACCOUNT_TABLE_NAME;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
            }
            else
            {
                resultArgs.Message = "Error in FetchFDAccountInvestment: " + resultArgs.Message;
            }
            return resultArgs;
        }
        /// <summary>
        /// Fixed Deposit Renewals and Withdrawal
        /// </summary>
        /// <returns></returns>
        private ResultArgs FDRenewals()
        {
            try
            {
                AcMELog.WriteLog("FetchFDRenewals Started.");
                using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FDRenewals))
                {
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchFDRenewals Ended.");
                resultArgs.DataSource.Table.TableName = FD_RENEWAL_TABLE_NAME;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
            }
            else
            {
                resultArgs.Message = "Error in FetchFDRenewals: " + resultArgs.Message;
            }
            return resultArgs;
        }



        /// <summary>
        /// Fetch Fixed Deposit Account Investments
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchHOBranchFDAccountInvestments()
        {
            try
            {
                AcMELog.WriteLog("FetchFDAccountInvestment Started.");
                using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchHOBranchFDAccounts))
                {
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchFDAccountInvestment Ended.");
                resultArgs.DataSource.Table.TableName = FD_ACCOUNT_TABLE_NAME;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
            }
            else
            {
                resultArgs.Message = "Error in FetchFDAccountInvestment: " + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// Fixed Deposit Renewals and Withdrawal
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchHOBranchFDRenewals()
        {
            try
            {
                AcMELog.WriteLog("FetchFDRenewals Started.");
                using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchHOBranchFDRenewals))
                {
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchFDRenewals Ended.");
                resultArgs.DataSource.Table.TableName = FD_RENEWAL_TABLE_NAME;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
            }
            else
            {
                resultArgs.Message = "Error in FetchFDRenewals: " + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetch Fixed Deposit Voucher Master Transaction 
        /// </summary>
        /// <returns></returns>
        private ResultArgs FDVoucherMasterTrans()
        {
            try
            {
                AcMELog.WriteLog("FetchFDVoucherMasterTransactions Started");
                using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FDVoucherMasterTrans))
                {
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchFDVoucherMasterTransactions Ended.");
                resultArgs.DataSource.Table.TableName = FD_VOUCHER_MASTER_TRANS_TABLE_NAME;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
            }
            else
            {
                resultArgs.Message = "Error in FetchFDVoucherMasterTransactions: " + resultArgs.Message;
            }

            return resultArgs;
        }

        /// <summary>
        /// On 08/02/2024, To get last voucher id, it will be used to set auto increment number when import project data in Target DB
        /// to have the same sequence voucher id in the target db too if and if only empty target db
        /// </summary>
        /// <returns></returns>
        private Int32 FetchBranchLastVoucherId()
        {
            Int32 rtn = 0;
            if (IsFYSplit)
            {
                ResultArgs result = new ResultArgs();
                try
                {
                    AcMELog.WriteLog("FetchBranchLastVoucherId Started");
                    using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchBranchLastVoucherId))
                    {
                        result = dataManager.FetchData(DataSource.DataTable);
                    }
                }
                catch (Exception ex)
                {
                    result.Message = ex.ToString();
                }
                if (result.Success && result.DataSource.Table.Rows.Count > 0)
                {
                    DataTable dtLastVoucherId = result.DataSource.Table;
                    rtn = NumberSet.ToInteger(dtLastVoucherId.Rows[0]["LAST_VOUCHER_ID"].ToString());
                    AcMELog.WriteLog("FetchBranchLastVoucherId Ended.");
                }
                else
                {
                    result.Message = "Error in FetchBranchLastVoucherId: " + resultArgs.Message;
                }
            }
            return rtn;
        }

        /// <summary>
        /// Fetch Fixed Deposit Vocuher Transactions
        /// </summary>
        /// <returns></returns>
        private ResultArgs FDVoucherTrans()
        {
            try
            {
                AcMELog.WriteLog("FetchFDVoucherTransactions Started");
                using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FDVoucherTrans))
                {
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchFDVoucherTransactions Ended.");
                resultArgs.DataSource.Table.TableName = FD_VOUCHER_TRANS_TABLE_NAME;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
            }
            else
            {
                resultArgs.Message = "Error in FetchFDVoucherTransactions: " + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetch Fixed Deposit Vocuher Transactions
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchHOBranchFDVoucherTrans()
        {
            try
            {
                AcMELog.WriteLog("FetchFDVoucherTransactions Started");
                using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchHOBranchFDVoucherTrans))
                {
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchFDVoucherTransactions Ended.");
                resultArgs.DataSource.Table.TableName = FD_VOUCHER_TRANS_TABLE_NAME;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
            }
            else
            {
                resultArgs.Message = "Error in FetchFDVoucherTransactions: " + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// Fixed Deposit Bank Account Details
        /// </summary>
        /// <returns></returns>
        private ResultArgs FDBankAccountDetails()
        {
            try
            {
                AcMELog.WriteLog("FetchFDBankAccountDetails Started.");
                using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FDBankAccountDetails))
                {
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
                throw;
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchFDBankAccountDetails Ended.");
                // resultArgs.DataSource.Table.TableName = FD_BANK_ACCOUNT_DETAILS_TABLE_NAME;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
            }
            else
            {
                resultArgs.Message = "Error in FetchFDBankAccountDetails: " + resultArgs.Message;
            }
            return resultArgs;
        }
        /// <summary>
        /// Fetch Fixed Deposit Bank Details
        /// </summary>
        /// <returns></returns>
        private ResultArgs FDBankDetails()
        {
            try
            {
                AcMELog.WriteLog("FetchFDBankDetails Started.");
                using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FDBankDetails))
                {
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchFDBankDetails Ended.");
                // resultArgs.DataSource.Table.TableName = FD_BANK_DETAILS_TABLE_NAME;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
            }
            else
            {
                resultArgs.Message = "Error in FetchFDBankDetails: " + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// This method used to check FD ledgers 
        ///  1. Check all BO FD ledgers are correctly mapped with HO FD ledger
        ///  2. Check HO FD ledger should not mapped with other BO Ledger
        /// 
        /// </summary>
        /// <returns></returns>
        public ResultArgs CheckInvalidMappedFDLedgers()
        {
            //1. Get list of FD legers from BO OR mapped FD HO ledgers
            using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.CheckMappedFDLedgers))
            {
                resultArgs = dataManager.FetchData(DataSource.DataView);
            }
            if (resultArgs != null && resultArgs.Success)
            {
                //2. Check Invalid mapped FD ledgers
                DataView dvMappedFDLedgers = resultArgs.DataSource.TableView;
                dvMappedFDLedgers.RowFilter = "GROUP_ID <> HO_GROUP_ID";
                if (dvMappedFDLedgers.Count > 0)
                {
                    resultArgs.Message = "Fixed Deposit ledgers are not mapped correctly.";
                }
            }
            return resultArgs;
        }


        /// <summary>
        /// On 10/04/2021, To get Mapped Ledgers for given Project
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchProjectLedgers()
        {
            try
            {
                AcMELog.WriteLog("FetchProjectLedgers: Started");
                using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchProjectLedgers))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchProjectLedgers Ended.");
                resultArgs.DataSource.Table.TableName = PROJECT_LEDGERS;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
            }
            else
            {
                resultArgs.Message = "Error in FetchProjectLedgers : " + resultArgs.Message;
            }
            return resultArgs;
        }


        /// <summary>
        /// On 01/02/2024, To get Mapped costcentes for given Project
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchProjectCostCentres()
        {
            try
            {
                AcMELog.WriteLog("FetchProjectCostCentres: Started");
                using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchProjectCostCenters))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    //dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchProjectCostCentres Ended.");
                resultArgs.DataSource.Table.TableName = PROJECT_COST_CENTRES;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
            }
            else
            {
                resultArgs.Message = "Error in FetchProjectCostCentres : " + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// On 01/02/2024, To get Mapped donors for given Project
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchProjectDonors()
        {
            try
            {
                AcMELog.WriteLog("FetchProjectDonors: Started");
                using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchProjectDonors))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchProjectDonors Ended.");
                resultArgs.DataSource.Table.TableName = PROJECT_DONORS;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
            }
            else
            {
                resultArgs.Message = "Error in FetchProjectDonors : " + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// On 20/05/2021, To apply export master alone and Cash/Bannk/FD Ledgers in already prepared dataset
        /// </summary>
        /// <returns></returns>
        public ResultArgs EnforceMasterExportSettings()
        {
            try
            {
                if (VoucherImportType == ImportType.SplitProject && IsExportMasterAlone && dsTransaction != null && resultArgs.Success && dsTransaction.Tables.Count > 0 && IsFYSplit)
                {
                    //On 20/05/2020, If export only masters, Clear all Voucher's Tables
                    if (dsTransaction.Tables[MASTER_TABLE_NAME] != null) dsTransaction.Tables[MASTER_TABLE_NAME].Rows.Clear();
                    if (dsTransaction.Tables[TRANSACTION_TABLE_NAME] != null) dsTransaction.Tables[TRANSACTION_TABLE_NAME].Rows.Clear();
                    if (dsTransaction.Tables[COSTCENTRE_TABLE_NAME] != null) dsTransaction.Tables[COSTCENTRE_TABLE_NAME].Rows.Clear();

                    //For FDs, have only Openings alone
                    if (dsTransaction.Tables[FD_ACCOUNT_TABLE_NAME] != null)
                    {
                        DataTable dtFDs = dsTransaction.Tables[FD_ACCOUNT_TABLE_NAME];
                        dtFDs.DefaultView.RowFilter = AppSchema.FDAccount.TRANS_TYPEColumn.ColumnName + " = '" + FDTypes.OP.ToString() + "'";
                        dsTransaction.Tables.Remove(FD_ACCOUNT_TABLE_NAME);
                        dsTransaction.Tables.Add(dtFDs.DefaultView.ToTable());
                    }
                    if (dsTransaction.Tables[FD_RENEWAL_TABLE_NAME] != null) dsTransaction.Tables[FD_RENEWAL_TABLE_NAME].Rows.Clear();
                    if (dsTransaction.Tables[FD_VOUCHER_MASTER_TRANS_TABLE_NAME] != null) dsTransaction.Tables[FD_VOUCHER_MASTER_TRANS_TABLE_NAME].Rows.Clear();
                    if (dsTransaction.Tables[FD_VOUCHER_TRANS_TABLE_NAME] != null) dsTransaction.Tables[FD_VOUCHER_TRANS_TABLE_NAME].Rows.Clear();

                    //On 20/05/2020, If export only masters and cash/Bank/FD ledges
                    if (IsExportCasBankFDAlone)
                    {
                        string CashBankFDfilter = (int)FixedLedgerGroup.Cash + "," + (int)FixedLedgerGroup.BankAccounts + "," + (int)FixedLedgerGroup.FixedDeposit;
                        CashBankFDfilter = AppSchema.Ledger.GROUP_IDColumn.ColumnName + " IN (" + CashBankFDfilter + ")";
                        CashBankFDfilter += " OR " + AppSchema.LedgerGroup.ACCESS_FLAGColumn.ColumnName + " = 2 OR " +
                                                     AppSchema.LedgerGroup.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.FixedDeposit;

                        //For Ledgers
                        if (dsTransaction.Tables[HEADOFFICE_LEDGER_TABLE_NAME] != null && dsTransaction.Tables[HEADOFFICE_LEDGER_TABLE_NAME].Rows.Count > 0)
                        {
                            DataTable dtLedgers = dsTransaction.Tables[HEADOFFICE_LEDGER_TABLE_NAME];
                            dtLedgers.DefaultView.RowFilter = CashBankFDfilter;
                            dsTransaction.Tables.Remove(HEADOFFICE_LEDGER_TABLE_NAME);
                            dsTransaction.Tables.Add(dtLedgers.DefaultView.ToTable());

                            //For Ledger Profile
                            if (dsTransaction.Tables[LEDGER_PROFILE_TABLE_NAME] != null) dsTransaction.Tables[LEDGER_PROFILE_TABLE_NAME].Rows.Clear();
                        }

                        //For Ledgers Balance
                        if (dsTransaction.Tables[LEDGERBALANCE_TABLE_NAME] != null && dsTransaction.Tables[LEDGERBALANCE_TABLE_NAME].Rows.Count > 0)
                        {
                            DataTable dtLedgers = dsTransaction.Tables[LEDGERBALANCE_TABLE_NAME];
                            dtLedgers.DefaultView.RowFilter = AppSchema.Ledger.GROUP_IDColumn.ColumnName + " IN (" + (int)FixedLedgerGroup.Cash + "," + (int)FixedLedgerGroup.BankAccounts + ")";
                            dsTransaction.Tables.Remove(LEDGERBALANCE_TABLE_NAME);
                            dsTransaction.Tables.Add(dtLedgers.DefaultView.ToTable());
                        }

                        //For Project Ledger
                        if (dsTransaction.Tables[PROJECT_LEDGERS] != null && dsTransaction.Tables[PROJECT_LEDGERS].Rows.Count > 0)
                        {
                            DataTable dtLedgers = dsTransaction.Tables[PROJECT_LEDGERS];
                            dtLedgers.DefaultView.RowFilter = CashBankFDfilter;
                            dsTransaction.Tables.Remove(PROJECT_LEDGERS);
                            dsTransaction.Tables.Add(dtLedgers.DefaultView.ToTable());
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
        /// On 02/02/2024, To check Closed Ledgers are used in Vouchers 
        /// 1. Voucher Transactions
        /// 2. FD Investments
        /// 3. FD Renewal Voucher
        /// 4. Budget Ledger
        /// 5. Budget Project Ledger        
        /// </summary>
        /// <returns></returns>
        public ResultArgs IsClosedLedgersInVouchers()
        {
            string message = string.Empty;
            DataTable dtClosedLedgers = new DataTable();
            //Checking Ledgers in the following Tables
            DataTable dtproject = dsTransaction.Tables[PROJECT_TABLE_NAME];
            DataTable dtVoucherLedger = dsTransaction.Tables[TRANSACTION_TABLE_NAME];
            DataTable dtFDInvestments = dsTransaction.Tables[FD_ACCOUNT_TABLE_NAME];
            DataTable dtFDRenewals = dsTransaction.Tables[FD_RENEWAL_TABLE_NAME];
            DataTable dtFDRenewalVoucherLedger = dsTransaction.Tables[FD_VOUCHER_TRANS_TABLE_NAME];
            DataTable dtBudgetProejctLedger = dsTransaction.Tables[BUDGET_PROJECT_LEDGER];
            DataTable dtBudgetVoucherLedger = dsTransaction.Tables[BUDGET_LEDGER];

            try
            {
                if (VoucherImportType == ImportType.SplitProject)
                {
                    using (LedgerSystem ledgersystem = new LedgerSystem())
                    {
                        resultArgs = ledgersystem.FetchClosedLedgersByDate(DateFrom);
                        if (resultArgs.Success)
                        {
                            dtClosedLedgers = resultArgs.DataSource.Table;
                        }
                    }
                    if (resultArgs.Success && dtClosedLedgers != null && dtClosedLedgers.Rows.Count > 0)
                    {
                        foreach (DataRow drClosedLedger in dtClosedLedgers.Rows)
                        {
                            string lname = drClosedLedger[AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                            Int32 grpid = NumberSet.ToInteger(drClosedLedger[AppSchema.Ledger.GROUP_IDColumn.ColumnName].ToString());

                            //1. Check Ledger has General Vouchers
                            ResultArgs resultFind = UtilityMethod.CheckValueCotainsInDataTable(dtVoucherLedger, AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, lname);
                            if (resultFind.DataSource.Data == null && resultFind.Success)
                            {
                                //2. Check Ledger has FD Investments - FD Ledger
                                resultFind = UtilityMethod.CheckValueCotainsInDataTable(dtFDInvestments, AppSchema.ProjectImportExport.LEDGER_NAMEColumn.ColumnName, lname);
                                if (resultFind.DataSource.Data == null && resultFind.Success)
                                {
                                    //3. Check Ledger has FD Investments - Bank Ledger
                                    resultFind = UtilityMethod.CheckValueCotainsInDataTable(dtFDInvestments, "BANK_LEDGER", lname);
                                    if (resultFind.DataSource.Data == null && resultFind.Success)
                                    {
                                        //4. Check Ledger has FD Renewals - Bank Ledger
                                        resultFind = UtilityMethod.CheckValueCotainsInDataTable(dtFDRenewals, "BANK_LEDGER", lname);
                                        if (resultFind.DataSource.Data == null && resultFind.Success)
                                        {
                                            //5. Check Ledger has FD Renewals - Bank Ledger
                                            resultFind = UtilityMethod.CheckValueCotainsInDataTable(dtFDRenewals, "INTEREST_LEDGER", lname);
                                            if (resultFind.DataSource.Data == null && resultFind.Success)
                                            {
                                                ////5. Check Ledger has Budget Ledger
                                                //resultFind = UtilityMethod.CheckValueCotainsInDataTable(dtBudgetVoucherLedger, AppSchema.ProjectImportExport.LEDGER_NAMEColumn.ColumnName, lname);
                                                //if (resultFind.DataSource.Data == null && resultFind.Success)
                                                //{
                                                //    resultArgs.Success = true;
                                                //}
                                                //else if (dtBudgetVoucherLedger!=null && dtBudgetVoucherLedger.Rows.Count>0)
                                                //{
                                                //    message = "Ledger : '" + lname + "' is closed but it has relation in Budget";
                                                //}
                                            }
                                            else if (dtFDRenewals != null && dtFDRenewals.Rows.Count > 0)
                                            {
                                                message = "Ledger : '" + lname + "' is closed but it has relation in FD Renewals (Interest Ledger)";
                                            }
                                        }
                                        else if (dtFDRenewals != null && dtFDRenewals.Rows.Count > 0)
                                        {
                                            message = "Ledger : '" + lname + "' is closed but it has relation in FD Renewals (Bank Ledger)";
                                        }
                                    }
                                    else if (dtFDInvestments != null && dtFDInvestments.Rows.Count > 0)
                                    {
                                        message = "Ledger : '" + lname + "' is closed but it has relation in FD Investment (Bank Ledger)";
                                    }
                                }
                                else if (dtFDInvestments != null && dtFDInvestments.Rows.Count > 0)
                                {
                                    message = "Ledger : '" + lname + "' is closed but it has relation in FD Investment (FD Ledger)";
                                }
                            }
                            else if (dtVoucherLedger != null && dtVoucherLedger.Rows.Count > 0)
                            {
                                message = "Ledger : '" + lname + "' is closed but it has relation in General Vouchers";
                            }

                            if (!string.IsNullOrEmpty(message))
                            {
                                string projectname = string.Empty;
                                if (dtproject != null && dtproject.Rows.Count > 0)
                                {
                                    projectname = dtproject.Rows[0][AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                                }
                                resultArgs.Message = "Project : " + projectname + System.Environment.NewLine + message;

                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                string msg = err.Message;
                resultArgs.Message = msg;
            }
            return resultArgs;
        }

        /// <summary>
        /// On 15/06/2021, this method is used to export Budget moulde details and all affected datatables will be attached
        /// 1. Budget Master
        /// 2. Budget Ledger
        /// 3. Budget Statistiscs Details
        /// 4. Budget Statistiscs Types
        /// 5. Budget Type
        /// 5. Budget Level
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchBudgetModuleDetails()
        {
            try
            {
                AcMELog.WriteLog("FetchBudgetModuleDetails: Started");

                //1. Budget Master Details
                resultArgs = FetchBudgetData(BUDGET_MASTER);
                if (resultArgs.Success)
                {
                    resultArgs = FetchBudgetData(BUDGET_PROJECT);
                    if (resultArgs.Success)
                    {
                        resultArgs = FetchBudgetData(BUDGET_LEDGER);
                        if (resultArgs.Success)
                        {
                            resultArgs = FetchBudgetData(BUDGET_STATISTICS_DETAILS);
                            if (resultArgs.Success)
                            {
                                resultArgs = FetchBudgetData(BUDGET_PROJECT_LEDGER);
                                if (resultArgs.Success)
                                {
                                    resultArgs = FetchBudgetData(BUDGET_STATISTICS_TYPE);
                                    if (resultArgs.Success)
                                    {
                                        resultArgs = FetchBudgetData(BUDGET_LEVEL);
                                        if (resultArgs.Success)
                                        {
                                            resultArgs = FetchBudgetData(BUDGET_TYPE);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (resultArgs.Success)
                {
                    AcMELog.WriteLog("FetchBudgetModuleDetails Ended.");
                }
                else
                {
                    resultArgs.Message = "Error in FetchBudgetModuleDetails : " + resultArgs.Message;
                }

            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            return resultArgs;
        }

        /// <summary>
        /// On 16/06/2021, To get Budget Details
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchBudgetData(string BudgetSubModules)
        {
            try
            {
                AcMELog.WriteLog("FetchBudgetData: Started");
                SQLCommand.ExportVouchers sqlcmd = SQLCommand.ExportVouchers.FetchBudgetMaster;
                if (BudgetSubModules == BUDGET_MASTER)
                {
                    sqlcmd = SQLCommand.ExportVouchers.FetchBudgetMaster;
                }
                else if (BudgetSubModules == BUDGET_PROJECT)
                {
                    sqlcmd = SQLCommand.ExportVouchers.FetchBudgetProject;
                }
                else if (BudgetSubModules == BUDGET_LEDGER)
                {
                    sqlcmd = SQLCommand.ExportVouchers.FetchBudgetLedger;
                }
                else if (BudgetSubModules == BUDGET_STATISTICS_DETAILS)
                {
                    sqlcmd = SQLCommand.ExportVouchers.FetchBudgetStatisticsDetails;
                }
                else if (BudgetSubModules == BUDGET_PROJECT_LEDGER)
                {
                    sqlcmd = SQLCommand.ExportVouchers.FetchBudgetProjectLedger;
                }
                else if (BudgetSubModules == BUDGET_STATISTICS_TYPE)
                {
                    sqlcmd = SQLCommand.ExportVouchers.FetchBudgetStatisticsDetails;
                }
                else if (BudgetSubModules == BUDGET_TYPE)
                {
                    sqlcmd = SQLCommand.ExportVouchers.FetchBudgetType;
                }
                else if (BudgetSubModules == BUDGET_LEVEL)
                {
                    sqlcmd = SQLCommand.ExportVouchers.FetchBudgetLevel;
                }
                using (DataManager dataManager = new DataManager(sqlcmd))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(AppSchema.Budget.DATE_FROMColumn, YearFrom);
                    dataManager.Parameters.Add(AppSchema.Budget.DATE_TOColumn, YearTo);
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, new DateTime(DateSet.ToDate(YearFrom, false).Year, 1, 1));
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, new DateTime(DateSet.ToDate(YearTo, false).Year, 12, 31));

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchBudgetData Ended.");
                resultArgs.DataSource.Table.TableName = BudgetSubModules;

                if (BudgetSubModules == BUDGET_STATISTICS_TYPE)
                {
                    //For Budget Satistics Type, get affected or used statistics types alone
                    DataTable dtStatisticsType = resultArgs.DataSource.Table;
                    string[] types = new string[] { this.AppSchema.StatisticsType.STATISTICS_TYPE_IDColumn.ColumnName, this.AppSchema.StatisticsType.STATISTICS_TYPEColumn.ColumnName };
                    dtStatisticsType = dtStatisticsType.DefaultView.ToTable(true, types);
                    dtStatisticsType.TableName = BudgetSubModules;
                    dsTransaction.Tables.Add(dtStatisticsType);
                }
                else
                {
                    dsTransaction.Tables.Add(resultArgs.DataSource.Table);
                }
            }
            else
            {
                resultArgs.Message = "Error in FetchBudgetData : " + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// On 05/02/2024, this method is used to export GST Invoice moulde details and all affected datatables will be attached
        /// 1. GST Master
        /// 2. GST Master Details
        /// 3. GST Voucher
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchGSTModuleDetails()
        {
            try
            {
                AcMELog.WriteLog("FetchGSTModuleDetails: Started");

                //1. Budget Master Details
                resultArgs = FetchGSTInvoiceData(GST_INVOICE_MASTER);
                if (resultArgs.Success)
                {
                    resultArgs = FetchGSTInvoiceData(GST_INVOICE_MASTER_LEDGER_DETAIL);
                    if (resultArgs.Success)
                    {
                        resultArgs = FetchGSTInvoiceData(GST_INVOICE_VOUCHER);
                    }
                }

                if (resultArgs.Success)
                {
                    AcMELog.WriteLog("FetchGSTModuleDetails Ended.");
                }
                else
                {
                    resultArgs.Message = "Error in FetchGSTModuleDetails : " + resultArgs.Message;
                }

            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            return resultArgs;
        }

        /// <summary>
        /// On 05/02/2024, To get GST Invoice Details
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchGSTInvoiceData(string GSTSubModules)
        {
            try
            {
                AcMELog.WriteLog("FetchGSTInvoiceData: Started");
                SQLCommand.ExportVouchers sqlcmd = SQLCommand.ExportVouchers.FetchGSTInvoiceMaster;
                if (GSTSubModules == GST_INVOICE_MASTER)
                {
                    sqlcmd = SQLCommand.ExportVouchers.FetchGSTInvoiceMaster;
                }
                if (GSTSubModules == GST_INVOICE_MASTER_LEDGER_DETAIL)
                {
                    sqlcmd = SQLCommand.ExportVouchers.FetchGSTInvoiceMasterLedgerDetail;
                }
                if (GSTSubModules == GST_INVOICE_VOUCHER)
                {
                    sqlcmd = SQLCommand.ExportVouchers.FetchGSTInvoiceVoucher;
                }

                using (DataManager dataManager = new DataManager(sqlcmd))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(AppSchema.Budget.DATE_FROMColumn, YearFrom);
                    dataManager.Parameters.Add(AppSchema.Budget.DATE_TOColumn, YearTo);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("FetchGSTInvoiceData Ended.");
                resultArgs.DataSource.Table.TableName = GSTSubModules;
                dsTransaction.Tables.Add(resultArgs.DataSource.Table);

            }
            else
            {
                resultArgs.Message = "Error in FetchGSTInvoiceData : " + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// on 11/01/2022, To remove unused ledgers (Check in voucher trans & not in budget)
        /// Remove from master ledger
        /// Remove from project ledger
        /// Remove from budget project ledger
        /// </summary>
        private void RemoveUnusedLedgersForShowMapping()
        {
            if (VoucherImportType == ImportType.SplitProject && IsShowMappingLedgers && !IsExportMasterAlone)
            {
                DataTable dtMasterBOLedger = dsTransaction.Tables[HEADOFFICE_LEDGER_TABLE_NAME];
                DataTable dtProjectLedger = dsTransaction.Tables[PROJECT_LEDGERS];
                DataTable dtBudgetProjectLedger = dsTransaction.Tables[BUDGET_PROJECT_LEDGER];

                DataTable dtVoucherLedger = dsTransaction.Tables[TRANSACTION_TABLE_NAME];
                DataTable dtFDVoucherLedger = dsTransaction.Tables[FD_RENEWAL_TABLE_NAME];
                DataTable dtBudgetVoucherLedger = dsTransaction.Tables[BUDGET_LEDGER];

                foreach (DataRow drLedger in dtMasterBOLedger.Rows)
                {
                    string lname = drLedger[AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                    Int32 grpid = NumberSet.ToInteger(drLedger[AppSchema.Ledger.GROUP_IDColumn.ColumnName].ToString());
                    if (grpid != (Int32)FixedLedgerGroup.Cash && grpid != (Int32)FixedLedgerGroup.BankAccounts && grpid != (Int32)FixedLedgerGroup.FixedDeposit)
                    {
                        //1. Check Ledger has vouchers
                        ResultArgs resultFind = UtilityMethod.CheckValueCotainsInDataTable(dtVoucherLedger, AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, lname);
                        if (resultFind.DataSource.Data == null && resultFind.Success)
                        {
                            //2. FD Renewals
                            resultFind = UtilityMethod.CheckValueCotainsInDataTable(dtFDVoucherLedger, AppSchema.FDRenewal.INTEREST_LEDGERColumn.ColumnName, lname);
                            if (resultFind.DataSource.Data == null && resultFind.Success)
                            {
                                //3. Check Ledger has Budget
                                resultFind = UtilityMethod.CheckValueCotainsInDataTable(dtBudgetVoucherLedger, AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, lname);
                                if (resultFind.DataSource.Data == null && resultFind.Success)
                                {
                                    drLedger.Delete();
                                }
                            }
                        }
                    }
                }
                dtMasterBOLedger.AcceptChanges();

                //Remove from Project mapping ledgers
                foreach (DataRow drMapProjectLedger in dtProjectLedger.Rows)
                {
                    string lname = drMapProjectLedger[AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                    Int32 grpid = NumberSet.ToInteger(drMapProjectLedger[AppSchema.Ledger.GROUP_IDColumn.ColumnName].ToString());
                    if (grpid != (Int32)FixedLedgerGroup.Cash && grpid != (Int32)FixedLedgerGroup.BankAccounts && grpid != (Int32)FixedLedgerGroup.FixedDeposit)
                    {
                        //1. Check Ledger in Master Ledger 
                        ResultArgs resultFind = UtilityMethod.CheckValueCotainsInDataTable(dtMasterBOLedger, AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, lname);

                        if (resultFind.DataSource.Data == null && resultFind.Success)
                        {
                            drMapProjectLedger.Delete();
                        }
                    }
                }
                dtProjectLedger.AcceptChanges();

                //Remove from Budget Project mapping ledgers
                foreach (DataRow drMapBudgetProjectLedger in dtBudgetProjectLedger.Rows)
                {
                    string lname = drMapBudgetProjectLedger[AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                    //1. Check Ledger in Master Ledger 
                    ResultArgs resultFind = UtilityMethod.CheckValueCotainsInDataTable(dtMasterBOLedger, AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, lname);

                    if (resultFind.DataSource.Data == null && resultFind.Success)
                    {
                        drMapBudgetProjectLedger.Delete();
                    }
                }
                dtBudgetProjectLedger.AcceptChanges();


            }
        }

        /// <summary>
        /// On 31/01/2024, Skip closed Ledgters for given DateFrom after checking used in transactions
        /// Checking Closed ledger in the following tables
        /// 1. Voucher Transactions
        /// 2. FD Renewal Voucher
        /// 3. Budget Ledger
        /// 
        /// Based above condition, skip Ledgers in the following tables
        /// 1. Ledger
        /// 2. Project Ledger
        /// 3. Budget Project
        /// 4. Ledger Balance
        /// </summary>
        /// <returns></returns>
        private void AllowClosedLedgersBasedOnUsage(bool isBankLedger)
        {
            //Remove Ledgers in the following Tables
            string CheckLedgerName = AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName;
            DataTable dtMasterBOLedger = dsTransaction.Tables[HEADOFFICE_LEDGER_TABLE_NAME];
            if (isBankLedger)
            {
                CheckLedgerName = AppSchema.BankAccount.ACCOUNT_NUMBERColumn.ColumnName;
                dtMasterBOLedger = dsTransaction.Tables[BANKDETAILS_TABLE_NAME];
            }
            DataTable dtProjectLedger = dsTransaction.Tables[PROJECT_LEDGERS];
            DataTable dtBudgetProjectLedger = dsTransaction.Tables[BUDGET_PROJECT_LEDGER];
            DataTable dtLedgerBalance = dsTransaction.Tables[LEDGERBALANCE_TABLE_NAME];

            //Checking Ledgers in the following Tables
            DataTable dtVoucherLedger = dsTransaction.Tables[TRANSACTION_TABLE_NAME];
            DataTable dtFDRenewalVoucherLedger = dsTransaction.Tables[FD_VOUCHER_TRANS_TABLE_NAME]; //FD_RENEWAL_TABLE_NAME
            DataTable dtBudgetVoucherLedger = dsTransaction.Tables[BUDGET_LEDGER];

            try
            {
                if (VoucherImportType == ImportType.SplitProject)
                {
                    dtMasterBOLedger.DefaultView.RowFilter = "";
                    dtMasterBOLedger.DefaultView.RowFilter = AppSchema.Ledger.DATE_CLOSEDColumn.ColumnName + "< '" + DateFrom + "'";
                    DataTable dt = dtMasterBOLedger.DefaultView.ToTable();

                    foreach (DataRowView drvLedger in dtMasterBOLedger.DefaultView)
                    {
                        string lname = drvLedger[CheckLedgerName].ToString();
                        Int32 grpid = isBankLedger ? (int)FixedLedgerGroup.BankAccounts : NumberSet.ToInteger(drvLedger[AppSchema.Ledger.GROUP_IDColumn.ColumnName].ToString());
                        if (grpid != (Int32)FixedLedgerGroup.FixedDeposit)
                        {
                            //1. Check Ledger has General Vouchers
                            ResultArgs resultFind = UtilityMethod.CheckValueCotainsInDataTable(dtVoucherLedger, AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, lname);
                            if (resultFind.DataSource.Data == null && resultFind.Success)
                            {
                                //2. Check Ledger has FD Vouchers
                                resultFind = UtilityMethod.CheckValueCotainsInDataTable(dtFDRenewalVoucherLedger, AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, lname); //FDRenewal.INTEREST_LEDGERColumn
                                if (resultFind.DataSource.Data == null && resultFind.Success)
                                {
                                    //3. Check Ledger has Budget
                                    resultFind = UtilityMethod.CheckValueCotainsInDataTable(dtBudgetVoucherLedger, AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, lname);
                                    if (resultFind.DataSource.Data == null && resultFind.Success)
                                    {
                                        drvLedger.Delete();
                                    }
                                }
                            }
                        }
                    }
                    dtMasterBOLedger.AcceptChanges();
                    dtMasterBOLedger.DefaultView.RowFilter = "";

                    //Remove from Project mapping ledgers
                    foreach (DataRow drMapProjectLedger in dtProjectLedger.Rows)
                    {
                        string lname = drMapProjectLedger[AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                        Int32 grpid = NumberSet.ToInteger(drMapProjectLedger[AppSchema.Ledger.GROUP_IDColumn.ColumnName].ToString());
                        if ((!isBankLedger && grpid != (Int32)FixedLedgerGroup.Cash && grpid != (Int32)FixedLedgerGroup.BankAccounts && grpid != (Int32)FixedLedgerGroup.FixedDeposit) ||
                            (isBankLedger && grpid == (int)FixedLedgerGroup.BankAccounts))
                        {
                            //1. Check Ledger in Master Ledger 
                            ResultArgs resultFind = UtilityMethod.CheckValueCotainsInDataTable(dtMasterBOLedger, CheckLedgerName, lname);

                            if (resultFind.DataSource.Data == null && resultFind.Success)
                            {
                                drMapProjectLedger.Delete();
                            }
                        }
                    }
                    dtProjectLedger.AcceptChanges();

                    //Remove Ledger Balance
                    foreach (DataRow drLedgerBalance in dtLedgerBalance.Rows)
                    {
                        string lname = drLedgerBalance[AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                        Int32 grpid = NumberSet.ToInteger(drLedgerBalance[AppSchema.Ledger.GROUP_IDColumn.ColumnName].ToString());
                        if ((!isBankLedger && grpid != (Int32)FixedLedgerGroup.Cash && grpid != (Int32)FixedLedgerGroup.BankAccounts && grpid != (Int32)FixedLedgerGroup.FixedDeposit) ||
                            (isBankLedger && grpid == (int)FixedLedgerGroup.BankAccounts))
                        {
                            //1. Check Ledger in Master Ledger 
                            ResultArgs resultFind = UtilityMethod.CheckValueCotainsInDataTable(dtMasterBOLedger, CheckLedgerName, lname);

                            if (resultFind.DataSource.Data == null && resultFind.Success)
                            {
                                drLedgerBalance.Delete();
                            }
                        }
                    }
                    dtLedgerBalance.AcceptChanges();


                    //Remove from Budget Project mapping ledgers
                    if (!isBankLedger)
                    {
                        foreach (DataRow drMapBudgetProjectLedger in dtBudgetProjectLedger.Rows)
                        {
                            string lname = drMapBudgetProjectLedger[AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                            //1. Check Ledger in Master Ledger 
                            ResultArgs resultFind = UtilityMethod.CheckValueCotainsInDataTable(dtMasterBOLedger, AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, lname);

                            if (resultFind.DataSource.Data == null && resultFind.Success)
                            {
                                drMapBudgetProjectLedger.Delete();
                            }
                        }
                    }
                    dtBudgetProjectLedger.AcceptChanges();
                }
            }
            catch (Exception err)
            {
                dtMasterBOLedger.DefaultView.RowFilter = "";
            }
        }

        /// <summary>
        /// This method is used to check exported voucher(s) are already available in portal for differnet project or different date range
        /// This issue will occur "Record is Available" issue in data sync
        /// </summary>
        /// <param name="dsExportedVouchers"></param>
        /// <returns></returns>
        /// </summary>
        /// <param name="dsExportedVouchers"></param>
        /// <param name="dtPortalVoucherOthersDate"></param>
        /// <param name="datefrom"></param>
        /// <param name="dateto"></param>
        /// <returns></returns>
        public ResultArgs CheckVouchersInPortalOtherProjectsOrDates(DataSet dsExportedVouchers, DataTable dtPortalVoucherOthersDate, DateTime datefrom, DateTime dateto)
        {
            resultArgs = new ResultArgs();

            try
            {
                DataTable dtProject = dsExportedVouchers.Tables["PROJECT"];
                DataTable dtVoucherMaster = dsExportedVouchers.Tables["VoucherMasters"];
                DataTable dtExistsVouchers = new DataTable();
                DataTable dtAllExistsVouchers = new DataTable();

                dtExistsVouchers.Columns.Add("LB_VOUCHER_MODE", AppSchema.VoucherMaster.VOUCHER_SUB_TYPEColumn.DataType);
                dtExistsVouchers.Columns.Add("LB_" + AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName, AppSchema.VoucherMaster.VOUCHER_IDColumn.DataType);
                dtExistsVouchers.Columns.Add("LB_" + AppSchema.VoucherMaster.PROJECTColumn.ColumnName, AppSchema.VoucherMaster.PROJECTColumn.DataType);
                dtExistsVouchers.Columns.Add("LB_" + AppSchema.VoucherMaster.VOUCHER_NOColumn.ColumnName, AppSchema.VoucherMaster.VOUCHER_NOColumn.DataType);
                dtExistsVouchers.Columns.Add("LB_" + AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName, AppSchema.VoucherMaster.VOUCHER_DATEColumn.DataType);
                dtExistsVouchers.Columns.Add("LB_" + AppSchema.VoucherMaster.VOUCHER_TYPEColumn.ColumnName, AppSchema.VoucherMaster.VOUCHER_TYPEColumn.DataType);
                dtExistsVouchers.Columns.Add("LB_" + AppSchema.VoucherMaster.VOUCHER_SUB_TYPEColumn.ColumnName, AppSchema.VoucherMaster.VOUCHER_SUB_TYPEColumn.DataType);

                dtExistsVouchers.Columns.Add("HO_VOUCHER_MODE", AppSchema.VoucherMaster.VOUCHER_SUB_TYPEColumn.DataType);
                dtExistsVouchers.Columns.Add("HO_" + AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName, AppSchema.VoucherMaster.VOUCHER_IDColumn.DataType);
                dtExistsVouchers.Columns.Add("HO_" + AppSchema.VoucherMaster.PROJECTColumn.ColumnName, AppSchema.VoucherMaster.PROJECTColumn.DataType);
                dtExistsVouchers.Columns.Add("HO_" + AppSchema.VoucherMaster.VOUCHER_NOColumn.ColumnName, AppSchema.VoucherMaster.VOUCHER_NOColumn.DataType);
                dtExistsVouchers.Columns.Add("HO_" + AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName, AppSchema.VoucherMaster.VOUCHER_DATEColumn.DataType);
                dtExistsVouchers.Columns.Add("HO_" + AppSchema.VoucherMaster.VOUCHER_TYPEColumn.ColumnName, AppSchema.VoucherMaster.VOUCHER_TYPEColumn.DataType);
                dtExistsVouchers.Columns.Add("HO_" + AppSchema.VoucherMaster.VOUCHER_SUB_TYPEColumn.ColumnName, AppSchema.VoucherMaster.VOUCHER_SUB_TYPEColumn.DataType);


                if (dtVoucherMaster != null && dtPortalVoucherOthersDate != null)
                {
                    DataTable dtExportProject = dtVoucherMaster.DefaultView.ToTable(true, new string[] { "PROJECT" });


                    foreach (DataRow dr in dtExportProject.Rows)
                    {
                        string pname = dr["PROJECT"].ToString();

                        ResultArgs resultFind = UtilityMethod.CheckValueCotainsInDataTable(dtVoucherMaster, "PROJECT", pname);
                        DataTable dtLBProjectVouchers = null;
                        if (resultFind.DataSource.Data != null && resultFind.Success)
                        {
                            dtLBProjectVouchers = resultFind.DataSource.Table;
                        }

                        if (dtPortalVoucherOthersDate != null && dtLBProjectVouchers != null &&
                            dtPortalVoucherOthersDate.Rows.Count > 0 && dtLBProjectVouchers.Rows.Count > 0)
                        {
                            var resultResult = from drLBVouchers in dtLBProjectVouchers.AsEnumerable()
                                               join drPOVouchers in dtPortalVoucherOthersDate.AsEnumerable()
                                                   on drLBVouchers.Field<UInt32>("VOUCHER_ID") equals drPOVouchers.Field<UInt32>("VOUCHER_ID")
                                               where (!drPOVouchers["PROJECT"].Equals(pname) ||
                                                     (drPOVouchers["PROJECT"].Equals(pname) && drPOVouchers.Field<System.DateTime>("VOUCHER_DATE") < datefrom
                                                        || drPOVouchers.Field<System.DateTime>("VOUCHER_DATE") > dateto))
                                               select dtExistsVouchers.LoadDataRow(new object[] 
                                                                {
                                                                    "Local Branch",
                                                                    (drLBVouchers["VOUCHER_ID"] == null ? 0 : drLBVouchers.Field<UInt32>("VOUCHER_ID")),
                                                                    (drLBVouchers["PROJECT"]==null? string.Empty : drLBVouchers.Field<System.String>("PROJECT") ),
                                                                    (drLBVouchers["VOUCHER_NO"]==null ? string.Empty : drLBVouchers.Field<System.String>("VOUCHER_NO") ),
                                                                    (drLBVouchers["VOUCHER_DATE"] == null ? DateTime.MinValue : drLBVouchers.Field<System.DateTime>("VOUCHER_DATE")),
                                                                    (drLBVouchers["VOUCHER_TYPE"]==null ? string.Empty : 
                                                                        (drLBVouchers.Field<System.String>("VOUCHER_TYPE") == VoucherSubTypes.RC.ToString() ? DefaultVoucherTypes.Receipt.ToString() : 
                                                                        drLBVouchers.Field<System.String>("VOUCHER_TYPE") == VoucherSubTypes.PY.ToString() ? DefaultVoucherTypes.Payment.ToString() : 
                                                                        drLBVouchers.Field<System.String>("VOUCHER_TYPE") == VoucherSubTypes.CN.ToString() ? DefaultVoucherTypes.Contra.ToString() : DefaultVoucherTypes.Journal.ToString())),
                                                                    (drLBVouchers["VOUCHER_SUB_TYPE"]==null? string.Empty : drLBVouchers.Field<System.String>("VOUCHER_SUB_TYPE")),

                                                                    "Head Office Branch",
                                                                    (drPOVouchers["VOUCHER_ID"] == null ? 0 : drPOVouchers.Field<UInt32>("VOUCHER_ID")),
                                                                    (drPOVouchers["PROJECT"]==null? string.Empty : drPOVouchers.Field<System.String>("PROJECT") ),
                                                                    (drPOVouchers["VOUCHER_NO"]==null ? string.Empty : drPOVouchers.Field<System.String>("VOUCHER_NO") ),
                                                                    (drPOVouchers["VOUCHER_DATE"] == null ? DateTime.MinValue : drPOVouchers.Field<System.DateTime>("VOUCHER_DATE")),
                                                                    (drPOVouchers["VOUCHER_TYPE"]==null ? string.Empty : 
                                                                        (drPOVouchers.Field<System.String>("VOUCHER_TYPE") == VoucherSubTypes.RC.ToString() ? DefaultVoucherTypes.Receipt.ToString() : 
                                                                        drPOVouchers.Field<System.String>("VOUCHER_TYPE") == VoucherSubTypes.PY.ToString() ? DefaultVoucherTypes.Payment.ToString() : 
                                                                        drPOVouchers.Field<System.String>("VOUCHER_TYPE") == VoucherSubTypes.CN.ToString() ? DefaultVoucherTypes.Contra.ToString() : DefaultVoucherTypes.Journal.ToString())),
                                                                    (drPOVouchers["VOUCHER_SUB_TYPE"]==null? string.Empty : drPOVouchers.Field<System.String>("VOUCHER_SUB_TYPE"))
                                                                    
                                                                }, false);

                            if (resultResult.Count() > 0)
                            {
                                dtExistsVouchers = resultResult.CopyToDataTable();
                                dtAllExistsVouchers.Merge(dtExistsVouchers);
                            }
                        }
                    }
                }

                if (dtAllExistsVouchers != null && dtAllExistsVouchers.Rows.Count > 0)
                {
                    string rtn = "IIF(LB_VOUCHER_DATE <> HO_VOUCHER_DATE, 'Voucher Date is changed  ', '') + " +
                                    "IIF(LB_PROJECT <> HO_PROJECT, 'Project is changed', '') ";

                    /*DataColumn dcSNo = new DataColumn("SERIAL_NO", typeof(System.Int32));
                    dcSNo.AutoIncrement = true;
                    dcSNo.AutoIncrementSeed = 1;
                    dcSNo.AutoIncrementStep = 1;*/
                    DataColumn dcEmpty = new DataColumn("EMPTY", typeof(System.String));
                    dcEmpty.DefaultValue = "-----------------------------------------------------------------------------------------------------------------------------";
                    dtAllExistsVouchers.Columns.Add(dcEmpty);
                    dtAllExistsVouchers.Columns.Add("REMARK", typeof(System.String), rtn);
                    //dtAllExistsVouchers.Columns.Add(dc); 
                    resultArgs.Message = "Few Project(s) Vouchers are already available in Portal for different Project(s) or Date range.";

                    resultArgs.DataSource.Data = dtAllExistsVouchers;
                }
                else
                {
                    resultArgs.Success = true;
                }
            }
            catch (Exception err)
            {
                resultArgs.Message = err.Message;
            }
            return resultArgs;
        }
        #endregion
    }
}
