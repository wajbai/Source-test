using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using System.Data;
using AcMEDSync.Model;
using Bosco.Model.UIModel;
using System.Collections;

namespace Bosco.Model.TallyMigration
{
    public class TallyMigrationSystem : SystemBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        BalanceSystem balanceSystem = new BalanceSystem();

        public string GRP_FD = "Fixed Deposits";
        public string GRP_FD_TALLY = "Fixed Deposits (Tally)";
        public string LDR_FD = "Fixed Deposit";
        public string LDR_FD_TALLY = "Fixed Deposit (Tally)";

        public string LDR_PROFIT_LOSS = "Profit & Loss A/c";
        public string LDR_PROFIT_LOSS_TALLY = "Profit & Loss A/c (Tally)";

        //---------------------------------UI Updation---------------------------------
        public event EventHandler UpdateMessage;
        public event EventHandler InitProgressBar;
        public event EventHandler IncreaseProgressBar;
        public event EventHandler UpdateGroupProcessedCount;
        public event EventHandler UpdateGroupPendingCountEve;
        public event EventHandler UpdateLedgerProcessedCount;
        public event EventHandler UpdateLedgerPendingCountEve;
        public event EventHandler UpdateCostCentreCategoryProcessedCount;
        public event EventHandler UpdateCostCentreCategoryPendingCount;
        public event EventHandler UpdateCostCentreProcessedCount;
        public event EventHandler UpdateCostCentrePendingCount;
        public event EventHandler UpdateDonorProcessedCount;
        public event EventHandler UpdateDonorPendingCount;
        public event EventHandler UpdateVoucherTransProcessedCount;
        public event EventHandler UpdatevoucherTransPendingCount;

        //For Export
        public event EventHandler UpdateExportProgressStatusMessage;
        public event EventHandler IncreaseExportProgressBar;

        public bool IsMigrateionCompleted = false;
        public int ProgressMaxCount { set; get; }
        public string StatusMessage { set; get; }
        //public bool DeleteUnusedLedger { get; set; }
        //-----------------------------Ledger Group UI Updation-------------------------
        public int GroupProcessedCount { set; get; }
        public int GroupPendingCount { set; get; }

        //-----------------------------Ledger  UI Updation-------------------------
        public int LedgerProcessedCount { set; get; }
        public int LedgerPendingCount { set; get; }

        //-----------------------------Cost Centre Category  UI Updation-------------------------
        public int CCCategoryProcessedCount { set; get; }
        public int CCCategoryPendingCount { set; get; }

        //-----------------------------Cost Centre Category  UI Updation-------------------------
        public int CostCentrerocessedCount { set; get; }
        public int CostCentrePendingCount { set; get; }

        //-----------------------------Cost Centre Category  UI Updation-------------------------
        public int DonorProcessedCount { set; get; }
        public int DonorPendingCount { set; get; }

        //-----------------------------Cost Centre Category  UI Updation-------------------------
        public int VoucherTransProcessedCount { set; get; }
        public int VoucherTransPendingCount { set; get; }

        #endregion

        #region Properties
        bool isDonorModuleAvailable = false;
        public bool IsDonorModuleEnabled
        {
            set { isDonorModuleAvailable = value; }
            get { return isDonorModuleAvailable; }
        }

        
        /// <summary>
        /// On 12/02/2021, option to migrate one than one voucher types
        /// </summary>
        public bool IncludeMultipleVoucherTypes
        {
             get; set; 
        }

        // On 08/12/2017, whether to updte opening balance or not 
        public bool IsUpdateLedgerOpeningBalance { get; set; }
        public int ProjectId { get; set; }
        private string Narration { get; set; }
        public string CurrentCompanyName { get; set; }

        public DateTime StartingDate { get; set; }
        public DateTime BooksBeginningDate { get; set; }
        public DateTime MigrationDateFrom { get; set; }
        public DateTime MigrationDateTo { get; set; }

        public DataTable dtTallyLedger = null;

        public DataTable dtTallyCompany { get; set; }
        public DataTable dtTallyGroup { get; set; }
        public DataTable dtTallyVoucherType { get; set; }
        public DataTable dtTallyCostCategory { get; set; }
        public DataTable dtTallyCostCentre { get; set; }
        public DataTable dtTallyCountry { get; set; }
        public DataTable dtTallyState { get; set; }
        public DataTable dtTallyDonor { get; set; }
        public DataTable dtTallyPurpose { get; set; }
        public DataSet dsTallyVoucher { get; set; }

        //-----------------------Master Bank---------------------------------------------
        private string BankName { set; get; }
        private string Branch { set; get; }
        private string Address { set; get; }
        private string IFSCode { set; get; }

        //--------------------------Master Bank Account-----------------------------------
        private int LedgerId { set; get; }
        private int BankId { set; get; }
        private string AccountNo { set; get; }
        private string DateOpened { set; get; }


        #endregion

        private int CreatedBy { get; set; }
        private int ModifiedBy { get; set; }
        private string CreatedByName { get; set; }
        private string ModifiedByName { get; set; }

        public void ProcessTallyMigration()
        {
            SetDefaultLedgers();
            MigrateAccountingYears();
            MigrateProjectInfo();

            //On 26/08/2021, To fix User details by dfault ---------------------------------------------
            CreatedBy = this.NumberSet.ToInteger(this.LoginUserId.ToString());
            ModifiedBy = this.NumberSet.ToInteger(this.LoginUserId.ToString());

            CreatedByName = FirstName;  //LoginUserName.ToString();
            ModifiedByName = FirstName; //LoginUserName.ToString();
            //------------------------------------------------------------------------------------------

            //On 01/02/2019, if license is ABE, migrate multi voucher types otherwise (only for Receipts/Payments)
            //all vouchers will be migrated into Receipts/Payments/Contra
            //if (this.IS_ABEBEN_DIOCESE)
            if (IncludeMultipleVoucherTypes)
            {
                MirgrateVoucherType();
            }
            MigrateLedgerGroup();
            MigrateLedger();
            MigrateCostCategory();
            MigrateCostCentre();
            MigrateCountry();
            MigrateState();
            MigrateDonorInformation();
            MigrateMasterPurpose();
            MigrateTransaction();
            UpdateLedgerOpeningBalanceDate();
            UpdateVoucherMasterTrans();
            UpdateTransactionCountry();
            EnableCostCentreForLedger();
            ExecuteFlushCommands();

            //Delete Unused ledgers only after refreshing balance
            //if (DeleteUnusedLedger)
            //    DeleteUnusedLedgersFromAcMeERP();
        }

        #region PreChecking for Migration
        public bool IsTallyMigrationMade()
        {
            bool Status = false;
            using (AcMePlusMigrationSystem getProject = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                int ProjectCount = getProject.GetProjectId(CurrentCompanyName);
                if (ProjectCount > 0)
                    Status = true;
            }
            return Status;
        }

        public void SetDefaultLedgers()
        {
            using (AcMePlusMigrationSystem AcMePlusMigration = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                AcMePlusMigration.SetDefaultLedgers();
            }
        }

        public bool IsAuditVouchersLockedVoucherDate(string tallycompanyname, DateTime frmDate, DateTime toDate)
        {
            bool rnt = true;
            ResultArgs result = new ResultArgs();
            AcMELog.WriteLog("Check Audit Voucher Lock Started..");
            result.Success = true;
            try
            {
                CurrentCompanyName = tallycompanyname;
                Int32 pId = GetProjectId();
                using (DataManager datamanager = new DataManager(SQLCommand.AuditLockTrans.FetchAuditLockDetailByProjectDateRange))
                {
                    datamanager.Parameters.Add(this.AppSchema.Voucher.PROJECT_IDColumn, pId);
                    datamanager.Parameters.Add(this.AppSchema.FDRegisters.DATE_FROMColumn, frmDate);
                    datamanager.Parameters.Add(this.AppSchema.FDRegisters.DATE_TOColumn, toDate);
                    result = datamanager.FetchData(DataSource.DataTable);

                    if (result != null && result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                    {
                        string lockedmessage = "Unable to Delete/Modify Vouchers." + System.Environment.NewLine + "Voucher is locked for '" + tallycompanyname + "'" +
                            " during the period of " + DateSet.ToDate(frmDate.ToShortDateString()) + " - " + DateSet.ToDate(toDate.ToShortDateString());
                        MessageRender.ShowMessage(lockedmessage);
                        rnt = true;
                    }
                    else
                    {
                        rnt = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Message = "Not able to check for Audit Lock Vouchers " + ex.Message;
                rnt = true;
            }
            finally { }

            if (result.Success)
            {
                AcMELog.WriteLog("Check Audit Voucher Lock Ended.");
            }
            else
            {
                AcMELog.WriteLog("Problem in IsAuditVouchersLocked : " + result.Message);
            }

           
            return rnt;
        }
        #endregion

        #region Clear Previous Data
        public void RemovePriviousMigration()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.ClearData))
            {
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, GetProjectId());
                resultArgs = dataManager.UpdateData();
            }
        }

        public void RemovePriviousMigrationByDateRange(DateTime dtfrom, DateTime dtto)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.ClearDataByDateRange))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, GetProjectId());
                dataManager.Parameters.Add(AppSchema.AuditLockTransType.DATE_FROMColumn, dtfrom.ToShortDateString());
                dataManager.Parameters.Add(AppSchema.AuditLockTransType.DATE_TOColumn, dtto.ToShortDateString());
                resultArgs = dataManager.UpdateData();
            }
        }


        public ResultArgs RemoveProject(string ProjectId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.ClearData))
            {
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        #endregion

        #region Migrate Project
        private void MigrateProjectInfo()
        {
            SetProgressBar();
            SetExportStatusMessage("Project Info", 2);
            UpdateProgessStatus();
            if (IsProjectNotExists())
            {
                using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.MigrateProject))
                {
                    //------------------Get Default Project Category Id if exists else Insert and get the default project category id------------------
                    int CategoryId = SetDefaultProjectCategory();
                    string ProjectDivision = "";
                    if (CurrentCompanyName.Contains("-"))
                        ProjectDivision = CurrentCompanyName.Substring(CurrentCompanyName.IndexOf('-'));
                    int DivisionId = ProjectDivision.Contains("Local") ? 1 : ProjectDivision.Contains("Foreign") ? 2 : 1;
                    string DateClosed = null;

                    dataManager.Parameters.Add(AppSchema.Project.PROJECT_CODEColumn, GetProjectCode(), true);
                    dataManager.Parameters.Add(AppSchema.Project.PROJECTColumn, CurrentCompanyName);
                    dataManager.Parameters.Add(AppSchema.Project.DIVISION_IDColumn, DivisionId);
                    dataManager.Parameters.Add(AppSchema.Project.ACCOUNT_DATEColumn, StartingDate.ToShortDateString());
                    dataManager.Parameters.Add(AppSchema.Project.DATE_STARTEDColumn, StartingDate.ToShortDateString());
                    dataManager.Parameters.Add(AppSchema.Project.DATE_CLOSEDColumn, DateClosed);
                    dataManager.Parameters.Add(AppSchema.Project.DESCRIPTIONColumn, Narration);
                    dataManager.Parameters.Add(AppSchema.Project.PROJECT_CATEGORY_IDColumn, CategoryId);
                    resultArgs = dataManager.UpdateData();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        ProjectId = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                        //Map Project Voucher
                        MapProjectVoucher();
                    }
                    UpdateProgessStatus();
                }

            }
            else
            {
                //Getting Existing Project Id for furthur migration
                ProjectId = GetProjectId();
            }
        }

        private void MapProjectVoucher()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.MapProjectVoucher))
            {
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.UpdateData();
            }
        }

        public bool IsDefaultVoucherExists()
        {
            using (DataManager dataManger = new DataManager(SQLCommand.TallyMigration.IsDefaultVoucherExists))
            {
                resultArgs = dataManger.FetchData(DataSource.Scalar);
            }
            // 1 -Receipts
            // 2 -Payments
            // 3 -Contra
            // 4 -Journal. The no of count must be 4
            return resultArgs.DataSource.Sclar.ToInteger == 4;
        }

        private string GetProjectCode()
        {
            string Code = string.Empty;
            using (AcMePlusMigrationSystem AcMeplusMigration = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                int Count = AcMeplusMigration.GetProjectCount();
                Code = String.Format("{0}{1}", CurrentCompanyName.Substring(0, 2), ++Count);
            }
            return Code;
        }

        private int SetDefaultProjectCategory()
        {
            int CategoryId = 0;
            using (AcMePlusMigrationSystem AcMePlusMigration = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                CategoryId = AcMePlusMigration.GetProjectCategoryId();
                if (!(CategoryId > 0))
                    CategoryId = AcMePlusMigration.MigrateDefaultProjectCategory();
            }
            return CategoryId;
        }

        private int GetProjectId()
        {
            int ProjectId = 0;
            using (AcMePlusMigrationSystem getProject = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                ProjectId = getProject.GetProjectId(CurrentCompanyName);
            }
            return ProjectId;
        }

        private bool IsProjectNotExists()
        {
            return GetProjectId() == 0;
        }
        #endregion

        #region Migrate Accounting Years
        private void MigrateAccountingYears()
        {
            SetProgressBar();
            SetExportStatusMessage("Accounting Years", 2);
            UpdateProgessStatus();
            
            //On 07/07/2023, Don't update Books Begin (As we validated pre-condition to migrate greater than first fiance of Acme.erp)
            DateTime deYearFrom = this.FirstFYDateFrom;
            DateTime deMigratedFYYearFrom = this.DateSet.GetFinancialYearByDate(MigrationDateTo, this.FirstFYDateFrom.ToShortDateString());
            DateTime deMigratedFYYearTo = deMigratedFYYearFrom.AddMonths(12).AddDays(-1);
            int YearCount = (deMigratedFYYearTo.Year - FirstFYDateFrom.Year) + 1;
            
            for (int i = 1; i < YearCount; i++)
            {
                deYearFrom = deYearFrom.AddYears(1); //On 05/09/2018, to skip creating many Financial year from BooksBegin
                //deYearFrom = DateSet.ToDate(DateSet.GetMySQLDateTime(deYearFrom.ToShortDateString(), DateDataType.DateFormatYMD), false);
                if (deYearFrom < deMigratedFYYearTo)
                {
                    if (IsAcYearNotExists(deYearFrom.ToShortDateString(), deYearFrom.AddMonths(12).AddDays(-1).ToShortDateString()))
                    {
                        using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateAcYears))
                        {
                            dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                            int Status = 0;
                            dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, deYearFrom.ToShortDateString());
                            dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, deYearFrom.AddMonths(12).AddDays(-1));
                            dataManager.Parameters.Add(AppSchema.AccountingPeriod.BOOKS_BEGINNING_FROMColumn, null);//StartingDate
                            dataManager.Parameters.Add(AppSchema.AccountingPeriod.STATUSColumn, Status);
                            dataManager.Parameters.Add(AppSchema.AccountingPeriod.IS_FIRST_ACCOUNTING_YEARColumn, Status);
                            resultArgs = dataManager.UpdateData();
                        }
                        UpdateProgessStatus();
                    }
                }
            }

            //On 07/07/2023, Don't update Books Begin (As we validated pre-condition to migrate greater than first fiance of Acme.erp)
            //SetActiveAccountYear();
        }

        private bool IsAcYearNotExists(string DateFrom, string DatTo)
        {
            int AcYearCount = 0;
            using (AcMePlusMigrationSystem findAcYear = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                AcYearCount = findAcYear.FindAccountingYearForTally(DateFrom, DatTo);
                //AcYearCount = findAcYear.FindAccountingYear(DateFrom, DatTo);
                // AcYearCount = findAcYear.FindAccountingYear(StartingDate.ToShortDateString(), StartingDate.AddMonths(12).ToShortDateString());
            }
            return AcYearCount == 0;
        }

        //private void SetActiveAccountYear()
        //{
        //    using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.SetActiveAccountingYear))
        //    {
        //        using (AcMePlusMigrationSystem firstAcYear = new AcMePlusMigrationSystem(MigrationType.Tally))
        //        {
        //            dataManager.Parameters.Add(AppSchema.AccountingPeriod.ACC_YEAR_IDColumn, firstAcYear.GetFirstAccountingYearId());
        //            DateTime deBookBeginningFrom = GetLeastBookBeginningFromDate() ?? BooksBeginningDate;
        //            if (DateTime.Compare(deBookBeginningFrom, BooksBeginningDate) <= 0)
        //                BooksBeginningDate = deBookBeginningFrom;
        //            dataManager.Parameters.Add(AppSchema.AccountingPeriod.BOOKS_BEGINNING_FROMColumn, BooksBeginningDate.ToShortDateString());
        //            resultArgs = dataManager.UpdateData();

        //        }
        //    }
        //    // SetCurrentMigrationYear();
        //}

        private DateTime? GetLeastBookBeginningFromDate()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GetLeastBookBeginningYear))
            {
                using (AcMePlusMigrationSystem firstAcYear = new AcMePlusMigrationSystem(MigrationType.Tally))
                {
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            return string.IsNullOrEmpty(resultArgs.DataSource.Sclar.ToString) ? null : (DateTime?)DateSet.ToDate(resultArgs.DataSource.Sclar.ToString, false);
        }

        private void SetCurrentMigrationYear()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.SetCurrentMigrationYear))
            {
                using (AcMePlusMigrationSystem firstAcYear = new AcMePlusMigrationSystem(MigrationType.Tally))
                {
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.ACC_YEAR_IDColumn, GetCurrentMigrationYear());
                    resultArgs = dataManager.UpdateData();

                }
            }
        }

        private int GetCurrentMigrationYear()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.GetCurrentMigrationYear))
            {
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, StartingDate);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        #endregion

        #region Master Voucher Type

        /// <summary>
        /// Migrate only Receipts/Payments/Contra/Journal voucher types
        /// 
        /// for ABE migrate Multi Receipts and Payments
        /// </summary>
        private void MirgrateVoucherType()
        {
            //On 01/02/2019, if license is ABE, migrate multi voucher types otherwise (only for Receipts/Payments/Contra/Journal)
            //all vouchers will be migrated into Receipts/Payments/Contra/Journal
            //if (this.IS_ABEBEN_DIOCESE)
            if (IncludeMultipleVoucherTypes)
            {
                SetProgressBar();
                if (dtTallyVoucherType != null)
                {
                    SetExportStatusMessage("Master Voucher Type", dtTallyVoucherType.Rows.Count);
                    foreach (DataRow drItem in dtTallyVoucherType.Rows)
                    {
                        UpdateProgessStatus();
                        string TallyVoucherTypeName = drItem["NAME"].ToString(); //VOUCHERTYPE
                        string TallyBaseVoucherTypeName = drItem["PARENT"].ToString(); //BASEVOUCHERTYPE
                        int AcmeerpVoucherType = 0;
                        string AcmeerpVoucherTypeName = string.Empty;
                        switch (TallyBaseVoucherTypeName.ToUpper())
                        {
                            case "RECEIPT":
                            case "CASH RECEIPT":
                                AcmeerpVoucherType = 1;
                                AcmeerpVoucherTypeName = DefaultVoucherTypes.Receipt.ToString() + "s";
                                //To Allow Multi Receipts for ABE
                                if (TallyVoucherTypeName.ToUpper() != "RECEIPT")
                                {
                                    AcmeerpVoucherTypeName = TallyVoucherTypeName;
                                }
                                break;
                            case "PAYMENT":
                            case "CASH PAYMENT":
                                AcmeerpVoucherType = 2;
                                AcmeerpVoucherTypeName = DefaultVoucherTypes.Payment.ToString() + "s";
                                //To Allow Multi Payments for ABE
                                if (TallyVoucherTypeName.ToUpper() != "PAYMENT")
                                {
                                    AcmeerpVoucherTypeName = TallyVoucherTypeName;
                                }
                                break;
                            case "CONTRA":
                                AcmeerpVoucherType = 3;
                                AcmeerpVoucherTypeName = DefaultVoucherTypes.Contra.ToString();
                                break;
                            case "JOURNAL":
                                AcmeerpVoucherType = 4;
                                AcmeerpVoucherTypeName = DefaultVoucherTypes.Journal.ToString();
                                //To Allow Multi Journal for ABE
                                if (TallyVoucherTypeName.ToUpper() != "JOURNAL")
                                {
                                    AcmeerpVoucherTypeName = TallyVoucherTypeName;
                                }
                                break;
                        }
                                               

                        if (IsVoucherTypeNotExists(AcmeerpVoucherTypeName) && !string.IsNullOrEmpty(AcmeerpVoucherTypeName))
                        {
                            //On 12/02/2021, Allow to migrate Voucher Type(s) If and if only Voucher type should have Voouchers
                            //except default Voucher Types (RECEIPTS, PAYMENTS, CONTRA and JOURNAL
                            bool migrate = true;
                            if (dsTallyVoucher != null && dsTallyVoucher.Tables["MASTER VOUCHER"] != null)
                            {
                                DataTable dtVoucherMaster = dsTallyVoucher.Tables["MASTER VOUCHER"];
                                dtVoucherMaster.DefaultView.RowFilter = string.Empty;
                                dtVoucherMaster.DefaultView.RowFilter = "VOUCHERTYPENAME='" + TallyVoucherTypeName + "'";
                                migrate = (dtVoucherMaster.DefaultView.Count > 0);
                                dtVoucherMaster.DefaultView.RowFilter = string.Empty;
                            }

                            if (migrate)
                            {
                                Int32 VoucherTypeId = 0;
                                using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.InsertMasterVocher))
                                {
                                    dataManager.Parameters.Add(AppSchema.Voucher.VOUCHER_IDColumn, VoucherTypeId, true);
                                    dataManager.Parameters.Add(AppSchema.Voucher.VOUCHER_NAMEColumn, AcmeerpVoucherTypeName);
                                    dataManager.Parameters.Add(AppSchema.Voucher.VOUCHER_TYPEColumn, AcmeerpVoucherType);
                                    dataManager.Parameters.Add(AppSchema.Voucher.VOUCHER_METHODColumn, 1); //drItem["NUMBERINGMETHOD"].ToString().Equals("Automatic") ? 1 : 2
                                    dataManager.Parameters.Add(AppSchema.Voucher.PREFIX_CHARColumn, string.Empty); //drItem["PREFIX"].ToString()
                                    dataManager.Parameters.Add(AppSchema.Voucher.SUFFIX_CHARColumn, string.Empty); //drItem["PREFIX"].ToString()
                                    dataManager.Parameters.Add(AppSchema.Voucher.STARTING_NUMBERColumn, NumberSet.ToInteger(drItem["BEGINNINGNUMBER"].ToString()));
                                    dataManager.Parameters.Add(AppSchema.Voucher.NUMBERICAL_WITHColumn, NumberSet.ToInteger(drItem["WIDTHOFNUMBER"].ToString()));
                                    dataManager.Parameters.Add(AppSchema.Voucher.PREFIX_WITH_ZEROColumn, "0"); //NumberSet.ToInteger(drItem["PREFILLZERO"].ToString())
                                    dataManager.Parameters.Add(AppSchema.Voucher.MONTHColumn, "3"); //by default april
                                    dataManager.Parameters.Add(AppSchema.Voucher.DURATIONColumn, "12"); //by default reset one year
                                    dataManager.Parameters.Add(AppSchema.Voucher.NOTEColumn, ""); //by default reset one year
                                    resultArgs = dataManager.UpdateData();
                                }

                                if (resultArgs.Success)
                                {
                                    VoucherTypeId = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                    if (VoucherTypeId > 0)
                                    {
                                        using (ProjectSystem projectsystem = new ProjectSystem())
                                        {
                                            projectsystem.MapProjectVoucher(ProjectId, VoucherTypeId);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool IsVoucherTypeNotExists(string VoucherName)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.IsMasterVoucherExists))
            {
                dataManager.Parameters.Add(AppSchema.Voucher.VOUCHER_NAMEColumn, VoucherName);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger == 0;
        }

        private Int32 GetVoucherTypeId(string VoucherName, DefaultVoucherTypes basevouchertype)
        {
            Int32 Rtn = (Int32)basevouchertype;
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.IsMasterVoucherExists))
            {
                dataManager.Parameters.Add(AppSchema.Voucher.VOUCHER_NAMEColumn, VoucherName);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            if (resultArgs.Success && resultArgs.DataSource.Sclar.ToInteger > 0)
            {
                Rtn = resultArgs.DataSource.Sclar.ToInteger;
            }
            return Rtn;
        }
        #endregion

        #region Ledger Group
        private void MigrateLedgerGroup()
        {
            SetProgressBar();
            if (dtTallyGroup != null)
            {
                GroupPendingCount = dtTallyGroup.Rows.Count;
                UpdateGroupPendingCount();
                CallGroupProcessedCount(0);
                SetMigrationStatusMessage("Master Ledger Group", GroupPendingCount);
                DataView dvGroup = new DataView(dtTallyGroup);
                dvGroup.RowFilter = "[Parent]=' Primary'";
                DataTable dtFilteredGroup = dvGroup.ToTable();
                int ProcessedCount = 0;
                //For Primary Group only
                foreach (DataRow dritem in dtFilteredGroup.Rows)
                {
                    MigrateLedgerGroupByCategory(dritem, ref ProcessedCount);
                }

                dvGroup = new DataView(dtTallyGroup);
                dvGroup.RowFilter = "[Parent]<>' Primary'";
                dtFilteredGroup = dvGroup.ToTable();
                foreach (DataRow dritem in dtFilteredGroup.Rows)
                {
                    MigrateLedgerGroupByCategory(dritem, ref ProcessedCount);

                }
            }
        }

        private bool ValidateGroupLevel(int GroupId)
        {
            bool IsGroupLevel = true;
            using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem())
            {
                resultArgs = ledgerSystem.ValidateGroupId(GroupId);
                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    if (resultArgs.DataSource.Table.Rows[0][ledgerSystem.AppSchema.LedgerGroup.PARENT_GROUP_IDColumn.ColumnName].ToString() != resultArgs.DataSource.Table.Rows[0][ledgerSystem.AppSchema.LedgerGroup.NATURE_IDColumn.ColumnName].ToString())
                    {
                        IsGroupLevel = false;
                    }
                }
            }
            return IsGroupLevel;
        }

        private void MigrateLedgerGroupByCategory(DataRow dritem, ref int ProcessedCount)
        {
            int AccessFlag = 0;

            //12/12/2017, In Acmeerp "Fixed Deposit" is separate module, so we make it "Fixed Deposit" as "Fixed Deposit (Tally)" ------------------------------------
            if (dritem["Group"].ToString().ToUpper() == GRP_FD.ToUpper())
            {
                dritem["Group"] = GRP_FD_TALLY;
            }
            if (dritem["Parent"].ToString().ToUpper() == GRP_FD.ToUpper())
            {
                dritem["Parent"] = GRP_FD_TALLY;
            }
            //--------------------------------------------------------------------------------------------------

            //-------------------- Query need to be changed----------------------------------
            string ParentName = dritem["Parent"].ToString();
            string Nature = dritem["Nature"].ToString();
            string GroupName = dritem["GROUP"].ToString();
            int ParentId = 0;
            int NatureId = 0;
            ParentName = ParentName.Equals(" Primary") ? Nature : ParentName;
            string GroupCode = GenerarteLedgerGroupCode();
            using (AcMePlusMigrationSystem AcMePlusMigration = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                NatureId = String.IsNullOrEmpty(Nature) ? AcMePlusMigration.GetGroupNatureId(ParentName) : AcMePlusMigration.GetGroupNatureId(Nature);
                NatureId = NatureId == 0 ? (Int32)Natures.Income : NatureId; //Fixing the Nature id if it has no group
                ParentId = AcMePlusMigration.GetLedgerGroupId(ParentName);
                int GroupIdExists = AcMePlusMigration.GetLedgerGroupId(GroupName);
                if (ParentId == 0) //If Parent Id is not exists, map to its nature group
                {
                    ParentId = NatureId;
                }
                if (ParentId > 0 && GroupIdExists > 0)
                {
                    ParentId = ValidateGroupLevel(AcMePlusMigration.GetLedgerGroupId(GroupName)) ? ParentId : AcMePlusMigration.GetLedgerGroupId(dritem["PRIMARY_GROUP"].ToString());
                }
                else if (!dritem["Parent"].ToString().Equals(" Primary"))
                {
                    ParentId = ValidateGroupLevel(AcMePlusMigration.GetLedgerGroupId(ParentName)) ? ParentId : AcMePlusMigration.GetLedgerGroupId(dritem["PRIMARY_GROUP"].ToString());
                }
                //Checking if the parents is already available
                if (AcMePlusMigration.GetLedgerGroupId(GroupName) == 0)
                {
                    //if (ParentId == 0 && ParentName != " Primary")
                    //{
                    //    using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateLedgerGroup))
                    //    {
                    //        //To Insert parent
                    //        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    //        dataManager.Parameters.Add(AppSchema.LedgerGroup.GROUP_CODEColumn, GroupCode, true);
                    //        dataManager.Parameters.Add(AppSchema.LedgerGroup.LEDGER_GROUPColumn, ParentName);
                    //        dataManager.Parameters.Add(AppSchema.LedgerGroup.PARENT_GROUP_IDColumn, 3);
                    //        dataManager.Parameters.Add(AppSchema.LedgerGroup.NATURE_IDColumn, 3);
                    //        dataManager.Parameters.Add(AppSchema.LedgerGroup.MAIN_GROUP_IDColumn, ParentId);
                    //        dataManager.Parameters.Add(AppSchema.LedgerGroup.ACCESS_FLAGColumn, AccessFlag);
                    //        resultArgs = dataManager.UpdateData();
                    //        --GroupPendingCount;
                    //        UpdateGroupPendingCount();
                    //        CallGroupProcessedCount(++ProcessedCount);
                    //    }
                    //}

                    GroupCode = GenerarteLedgerGroupCode();
                    string Parent = dritem["Group"].ToString();
                    int GroupId = AcMePlusMigration.GetLedgerGroupId(Parent);

                    //Checking if the Group is already available
                    if (GroupId == 0)
                    {
                        using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateLedgerGroup))
                        {
                            //To Insert parent Group
                            dataManager.DataCommandArgs.IsDirectReplaceParameter = false;
                            dataManager.Parameters.Add(AppSchema.LedgerGroup.GROUP_CODEColumn, GroupCode, true);
                            dataManager.Parameters.Add(AppSchema.LedgerGroup.LEDGER_GROUPColumn, Parent);
                            dataManager.Parameters.Add(AppSchema.LedgerGroup.PARENT_GROUP_IDColumn, ParentId);
                            dataManager.Parameters.Add(AppSchema.LedgerGroup.NATURE_IDColumn, NatureId);
                            dataManager.Parameters.Add(AppSchema.LedgerGroup.MAIN_GROUP_IDColumn, ParentId); //Modified by Carmel Raj M on 02-November-2015
                            dataManager.Parameters.Add(AppSchema.LedgerGroup.ACCESS_FLAGColumn, AccessFlag);
                            resultArgs = dataManager.UpdateData();
                            --GroupPendingCount;
                            UpdateGroupPendingCount();
                            CallGroupProcessedCount(++ProcessedCount);
                        }
                    }
                }
            }
            UpdateProgessStatus();
        }

        private string GenerarteLedgerGroupCode()
        {
            string GroupCode = string.Empty;
            using (AcMePlusMigrationSystem AcMePlusMigration = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                int Count = AcMePlusMigration.GenerateLedgerGroupCode();
                GroupCode = String.Format("LG{0}", ++Count);
                //if (!string.IsNullOrEmpty(AcMePlusMigration.GetLedgerGroupCode(GroupCode)))
                //{
                //    GroupCode = String.Format("LG{0}", ++Count);
                //}
            }
            return GroupCode;
        }
        #endregion

        #region Migrate  Ledger
        private void MigrateLedger()
        {
            SetProgressBar();
            if (dtTallyLedger != null)
            {
                LedgerPendingCount = dtTallyLedger.Rows.Count;
                SetMigrationStatusMessage("Master Ledger", LedgerPendingCount);
                int ProcessedCount = 0;
                CallLedgerPendingCount();
                CallLedgerProcessedCount(0);
                foreach (DataRow drledger in dtTallyLedger.Rows)
                {
                    string LedgerName = drledger["LedgerName"].ToString();

                    //12/12/2017, In Acmeerp "Fixed Deposit" is separate module, so treat "Fixed Deposit" as  "Fixed Deposit (Tally)" 
                    LedgerName = (LedgerName.ToUpper() == LDR_FD.ToUpper() ? LDR_FD_TALLY : LedgerName);

                    //16/02/2017, In Acmeerp No "Profit Loss A/c" ledger, so we create it as "Profit Loss A/c" Tally ledger
                    LedgerName = (LedgerName.ToUpper() == LDR_PROFIT_LOSS.ToUpper() ? LDR_PROFIT_LOSS_TALLY : LedgerName);


                    //Modified by alwar 01/12/2015 -----------------------------------------------------------------------------------------
                    //If Starting Date is equal to BooksBeginningDate, Take all nature's leadger's opening balance
                    //Else take opening balance only for ASSET and LIABILITY nature leadgers
                    //Earlier it was taking all nature's leadgers opening balances 

                    /*double OPBalance = NumberSet.ToDouble(drledger["CLOSINGBALANCE"].ToString());
                    string PrimaryGroup = drledger["PrimaryGroup"].ToString();
                    string LedgerSubType = PrimaryGroup.Equals("Bank Accounts") ? "BK" : "GN";*/
                    double OPBalance = 0;
                    string PrimaryGroup = drledger["PrimaryGroup"].ToString();
                    string LedgerSubType = PrimaryGroup.ToUpper().Equals("BANK ACCOUNTS") ? "BK" : "GN";

                    //On 28/10/2017, for getting opening balance for given date range
                    //if (BooksBeginningDate == StartingDate)
                    //{
                    //    OPBalance = NumberSet.ToDouble(drledger["CLOSINGBALANCE"].ToString());
                    //}
                    //else if (StartingDate == MigrationDateFrom)
                    //{
                    //    //Get Leadger's Group Nature, IF ASSET, Libilities, take opening balance
                    //    int natureid = GetLedgerNature(LedgerName, PrimaryGroup);
                    //    if (natureid == (int)Natures.Assert || natureid == (int)Natures.Libilities)
                    //    {
                    //        OPBalance = NumberSet.ToDouble(drledger["CLOSINGBALANCE"].ToString());
                    //    }
                    //}

                    //if (StartingDate == MigrationDateFrom)
                    //{
                    //    OPBalance = NumberSet.ToDouble(drledger["DATEOPENINGBALANCE"].ToString()); //OPENINGBALANCE
                    //}
                    OPBalance = NumberSet.ToDouble(drledger["DATEOPENINGBALANCE"].ToString()); //OPENINGBALANCE
                    //-------------------------------------------------------------------------------------------------------------------

                    if (IsLedgerNotExists(LedgerName))
                    {
                        string ParantGroup = drledger["Parent"].ToString();
                        string LedgerType = "GN";
                        int IsCostCenter = NumberSet.ToInteger(drledger["IsCostCentresOn"].ToString());
                        // On 28/10/2017, support inbeween ledgers are added in tally, when we add into acme.erp 
                        //if migration is not from begning, we will take base opening balance
                        //if (StartingDate != MigrationDateFrom)
                        //{
                        //    OPBalance = NumberSet.ToDouble(drledger["OPENINGBALANCE"].ToString());
                        //}

                        OPBalance = NumberSet.ToDouble(drledger["DATEOPENINGBALANCE"].ToString());

                        //12/12/2017, In Acmeerp "Fixed Deposit" is separate module, so treat "Fixed Deposit" as  "Fixed Deposit (Tally)" 
                        ParantGroup = (ParantGroup.ToUpper() == GRP_FD.ToUpper() ? GRP_FD_TALLY : ParantGroup);

                        if (IsLedgerMapped(drledger))
                        {
                            int GroupId = GetLedgerGroupId(ParantGroup);
                            if (GroupId > 0)
                            {
                                using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.MigrateLedger))
                                {
                                    dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_CODEColumn, GetLedgerCode(), true);
                                    dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_NAMEColumn, LedgerName);
                                    dataManager.Parameters.Add(AppSchema.Ledger.GROUP_IDColumn, GroupId);
                                    dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_TYPEColumn, LedgerType);
                                    dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_SUB_TYPEColumn, LedgerSubType);
                                    dataManager.Parameters.Add(AppSchema.Ledger.IS_COST_CENTERColumn, IsCostCenter);
                                    resultArgs = dataManager.UpdateData();
                                    if (resultArgs != null && resultArgs.Success)
                                    {
                                        LedgerId = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                        if (LedgerSubType.Equals("BK"))
                                        {
                                            BankName = drledger["LedgerName"].ToString();

                                            //On 27/09/2017, If branch name is empty, take bank name
                                            //Branch = drledger["BankBranchName"].ToString();
                                            Branch = drledger["BankBranchName"].ToString();
                                            Branch = (String.IsNullOrEmpty(Branch) ? BankName : Branch);

                                            string BankAccountNo = GetBankAccountNo(BankName);
                                            if (string.IsNullOrEmpty(BankAccountNo))
                                            {
                                                Address = Branch;
                                                IFSCode = drledger["IFSCode"].ToString();
                                                BankId = GetExistingBankId(BankName, Branch);
                                                BankId = BankId > 0 ? BankId : MigrateMasterBank();

                                                //----------------------Make new Bank Account Entry-----------------------------------------------
                                                AccountNo = drledger["BankDetails"].ToString();
                                                DateOpened = StartingDate.ToShortDateString();
                                                //----------------------Migrate Bank Account Details ---------------------------------------------
                                                //  AccountNo = string.IsNullOrEmpty(AccountNo) ? BankName : AccountNo;

                                                //Bank Account No should be same as the Ledger Name
                                                AccountNo = BankName;
                                                MigrateBankAccount();
                                            }
                                        }
                                    }
                                    //----------------------Mapping the Ledger to the Project with Opening Balance ---------------------
                                    MapLedger(LedgerId, OPBalance);
                                    --LedgerPendingCount;
                                    CallLedgerPendingCount();
                                    CallLedgerProcessedCount(++ProcessedCount);
                                }
                            }
                        }
                        else
                        {
                            string BankAccountNo = GetBankAccountNo(LedgerName);
                            if (string.IsNullOrEmpty(BankAccountNo))
                            {
                                //Updating Opening Balance and migrating Bank Account
                                LedgerId = GetLedgerId(drledger["LedgerName"].ToString());
                                if (LedgerSubType.Equals("BK"))
                                {
                                    BankName = drledger["LedgerName"].ToString();
                                    Branch = drledger["BankBranchName"].ToString();
                                    Address = Branch;
                                    IFSCode = drledger["IFSCode"].ToString();
                                    BankId = GetExistingBankId(BankName, Branch);
                                    BankId = BankId > 0 ? BankId : MigrateMasterBank();

                                    //----------------------Make new Bank Account Entry-----------------------------------------------
                                    AccountNo = drledger["BankDetails"].ToString();
                                    DateOpened = StartingDate.ToShortDateString();
                                    //----------------------Migrate Bank Account Details ---------------------------------------------
                                    // AccountNo = string.IsNullOrEmpty(AccountNo) ? BankName : AccountNo;
                                    //Bank Account No should be same as the Ledger Name
                                    AccountNo = BankName;
                                    MigrateBankAccount();
                                }
                            }
                            //----------------------Mapping the Ledger to the Project with Opening Balance ---------------------
                            MapLedger(LedgerId, OPBalance);
                            --LedgerPendingCount;
                            CallLedgerPendingCount();
                            CallLedgerProcessedCount(++ProcessedCount);
                        }
                    }
                    else
                    {
                        string BankAccountNo = GetBankAccountNo(LedgerName);
                        if (string.IsNullOrEmpty(BankAccountNo))
                        {
                            //Updating Opening Balance and migrating Bank Account
                            LedgerId = GetLedgerId(drledger["LedgerName"].ToString());
                            if (LedgerSubType.Equals("BK"))
                            {
                                BankName = drledger["LedgerName"].ToString();
                                Branch = drledger["BankBranchName"].ToString();
                                Address = Branch;
                                IFSCode = drledger["IFSCode"].ToString();
                                BankId = GetExistingBankId(BankName, Branch);
                                BankId = BankId > 0 ? BankId : MigrateMasterBank();
                                DateOpened = StartingDate.ToShortDateString();

                                //----------------------Make new Bank Account Entry-----------------------------------------------
                                //  AccountNo = drledger["BankDetails"].ToString();

                                //----------------------Migrate Bank Account Details ---------------------------------------------
                                //AccountNo = string.IsNullOrEmpty(AccountNo) ? BankName : AccountNo;
                                //Bank Account No should be same as the Ledger Name
                                AccountNo = BankName;

                                MigrateBankAccount();
                            }
                        }

                        string LName = string.Empty;
                        if (dtTallyLedger.Columns.Contains("LEDGER_NAME"))
                        {
                            LName = drledger["LEDGER_NAME"].ToString() == null ? LedgerName : drledger["LEDGER_NAME"].ToString();
                        }
                        else LName = drledger["LedgerName"].ToString();
                        MapLedger(GetLedgerId(LedgerName), OPBalance);
                    }
                    UpdateProgessStatus();
                }
            }

        }

        private bool IsLedgerMapped(DataRow drLedgers)
        {
            bool IsValid = false;
            if (dtTallyLedger.Columns.Contains("Ledger_Id"))
            {
                if (NumberSet.ToInteger(drLedgers["Ledger_Id"].ToString()) == 0)
                {
                    IsValid = true;
                }
            }
            else //if Mapping is not done Ledger_Id column will not be available so retuns true (Note: it applicable for the first time migration)
            {
                IsValid = true;
            }
            return IsValid;
        }

        private int GetLedgerId(string LedgerName)
        {
            int TempLedgerId = 0;
            //12/12/2017, In Acmeerp "Fixed Deposit" is separate module, so treat "Fixed Deposit" as  "Fixed Deposit (Tally)" 
            LedgerName = (LedgerName.ToUpper() == LDR_FD.ToUpper() ? LDR_FD_TALLY : LedgerName);

            //16/02/2017, In Acmeerp No "Profit Loss A/c" ledger, so we create it as "Profit Loss A/c" Tally ledger
            LedgerName = (LedgerName.ToUpper() == LDR_PROFIT_LOSS.ToUpper() ? LDR_PROFIT_LOSS_TALLY : LedgerName);

            using (AcMePlusMigrationSystem AcMePlusMigration = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                TempLedgerId = AcMePlusMigration.GetLedgerId(LedgerName);
            }
            return TempLedgerId;
        }

        private void MapLedger(int LedgerId, double OPBalance)
        {
            if (IsProjectLedgerNotMapped(LedgerId))
            {
                //Mapping Ledger with the Project
                using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectLedgerMappingAdd))
                {
                    dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                    dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    resultArgs = dataManager.UpdateData();
                }
            }

            // On 08/12/2017, whether to updte opening balance or not 
            //if (OPBalance != 0 || IsUpdateLedgerOpeningBalance == true)
            
            // On 22/06/2018, whether to updte opening balance or not 
            if (OPBalance != 0 && IsUpdateLedgerOpeningBalance == true)
            {
                //Updating Opening Balance if it has Opening Balance
                using (AcMePlusMigrationSystem AcMePlusMigration = new AcMePlusMigrationSystem(MigrationType.Tally))
                {
                    AcMePlusMigration.ProjectId = ProjectId;
                    AcMePlusMigration.OPDate = BooksBeginningDate.AddDays(-1);
                    AcMePlusMigration.BankLedgerId = LedgerId;
                    AcMePlusMigration.OPBankBalance = OPBalance;
                    AcMePlusMigration.UpdateBankOpeningBalance(MigrationType.Tally);
                }
            }
        }

        private bool IsProjectLedgerNotMapped(int LedgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.IsProjectLedgerMapped))
            {
                dataManager.Parameters.Add(AppSchema.LedgerBalance.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(AppSchema.LedgerBalance.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger == 0;
        }

        private bool IsLedgerNotExists(string LedgerName)
        {
            int Count = 0;
            using (AcMePlusMigrationSystem AcMePlusMigration = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                Count = AcMePlusMigration.GetLedgerId(LedgerName);
            }
            return Count == 0;
        }

        private int GetLedgerGroupId(string ParentGroup)
        {
            int ParentGroupId = 0;
            using (AcMePlusMigrationSystem AcMePlusMigraion = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                ParentGroupId = AcMePlusMigraion.GetLedgerGroupId(ParentGroup);
            }
            return ParentGroupId;
        }

        private string GetLedgerCode()
        {
            string Code = string.Empty;
            using (AcMePlusMigrationSystem AcMePlusMigration = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                int Count = AcMePlusMigration.GenerateLedgerCode();
                Code = String.Format("LE{0}", ++Count);
            }
            return Code;
        }

        private int GetExistingBankId(string BankName, string BrachName)
        {
            int ExistingBankId = 0;
            using (AcMePlusMigrationSystem AcMePlusMigration = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                ExistingBankId = AcMePlusMigration.FindBankId(BankName, BrachName);
            }
            return ExistingBankId;
        }

        private int MigrateMasterBank()
        {
            //Fix length Bank(50), Branch(50), Address(100)
            BankName = (BankName.Length > 50 ? BankName.Substring(0, 50) : BankName);
            Branch = (Branch.Length > 50 ? Branch.Substring(0, 50) : Branch);
            Address = (Address.Length > 100 ? Address.Substring(0, 100) : Address);

            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.MigrateMasterBank))
            {
                dataManager.Parameters.Add(AppSchema.Bank.BANK_CODEColumn, GetBankCode(), true);
                dataManager.Parameters.Add(AppSchema.Bank.BANKColumn, BankName);
                dataManager.Parameters.Add(AppSchema.Bank.BRANCHColumn, Branch);
                dataManager.Parameters.Add(AppSchema.Bank.ADDRESSColumn, Address);
                dataManager.Parameters.Add(AppSchema.Bank.IFSCCODEColumn, IFSCode);
                resultArgs = dataManager.UpdateData();
            }
            return NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
        }

        private string GetBankCode()
        {
            string Code = string.Empty;
            using (AcMePlusMigrationSystem AcMePlusMigration = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                int Count = AcMePlusMigration.GenerateBankCode();
                Code = String.Format("BK{0}", ++Count);
                if (!string.IsNullOrEmpty(AcMePlusMigration.GetBankCode(Code)))
                {
                    Code = String.Format("BK{0}", ++Count);
                }
            }
            return Code;
        }

        private string GetBankAccountNo(string BankName)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GetBankAccountNo))
            {
                dataManager.Parameters.Add(AppSchema.BankAccount.ACCOUNT_NUMBERColumn, BankName);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToString;
        }

        private void MigrateBankAccount()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateBankAccount))
            {
                dataManager.Parameters.Add(AppSchema.BankAccount.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(AppSchema.BankAccount.ACCOUNT_CODEColumn, GetBankAccountCode());
                dataManager.Parameters.Add(AppSchema.BankAccount.ACCOUNT_NUMBERColumn, AccountNo);
                dataManager.Parameters.Add(AppSchema.BankAccount.BANK_IDColumn, BankId);
                dataManager.Parameters.Add(AppSchema.BankAccount.DATE_OPENEDColumn, DateOpened);
                dataManager.Parameters.Add(AppSchema.BankAccount.DATE_CLOSEDColumn, null);
                dataManager.Parameters.Add(AppSchema.BankAccount.ACCOUNT_TYPE_IDColumn, 1);
                resultArgs = dataManager.UpdateData();
            }
        }

        private string GetBankAccountCode()
        {
            string Code = string.Empty;
            using (AcMePlusMigrationSystem AcMePlusMigration = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                int Count = AcMePlusMigration.GenerateBankAccountCount();
                Code = String.Format("BKA{0}", ++Count);
            }
            return Code;
        }

        /// <summary>
        /// This mehtod is used to check given ledger is bank account ledger
        /// </summary>
        /// <param name="CashBankLedgerId"></param>
        /// <returns></returns>
        private bool IsBankAccountLedger(Int32 CashBankLedgerId)
        {
            bool Rtn = false;
            using (LedgerSystem ledgersystem = new LedgerSystem())
            {
                ledgersystem.LedgerId = CashBankLedgerId;
                int bankledger = ledgersystem.FetchLedgerGroupById();
                Rtn = (bankledger == (Int32)FixedLedgerGroup.BankAccounts);
            }
            return Rtn;
        }

        #endregion

        #region Cost Centre and Cost Category

        private void MigrateCostCategory()
        {
            SetProgressBar();
            if (dtTallyCostCategory != null)
            {
                CCCategoryPendingCount = dtTallyCostCategory.Rows.Count;
                int ProcessedCount = 0;
                CallCostCentreCategroyProcessedCount(0);
                CallCostCentreCategoryPending();
                SetMigrationStatusMessage("Cost Category", CCCategoryPendingCount);
                foreach (DataRow drCostCategory in dtTallyCostCategory.Rows)
                {
                    string Name = drCostCategory["Name"].ToString();
                    if (IsCostCentreCategoryNotExists(Name))
                    {
                        using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.InsertCostCentreCategory))
                        {
                            dataManager.Parameters.Add(AppSchema.CostCentreCategory.COST_CENTRE_CATEGORY_NAMEColumn, Name);
                            resultArgs = dataManager.UpdateData();
                        }
                        CallCostCentreCategroyProcessedCount(++ProcessedCount);
                        --CCCategoryPendingCount;
                        CallCostCentreCategoryPending();
                    }
                    UpdateProgessStatus();
                }
            }

        }

        private bool IsCostCentreCategoryNotExists(string Name)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.IsCostCentreCategoryExists))
            {
                dataManager.Parameters.Add(AppSchema.CostCentreCategory.COST_CENTRE_CATEGORY_NAMEColumn, Name);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger == 0;
        }
        #endregion

        #region Migrate Cost Centre

        private void MigrateCostCentre()
        {
            SetProgressBar();
            if (dtTallyCostCentre != null)
            {
                CostCentrePendingCount = dtTallyCostCentre.Rows.Count;
                int ProcessedCount = 0;
                SetMigrationStatusMessage("Cost Centre", CostCentrePendingCount);
                CallCostCentreProcessedCount(0);
                CallCostCentrePendingCount();
                foreach (DataRow drItem in dtTallyCostCentre.Rows)
                {
                    string Name = drItem["Name"].ToString();
                    if (IsCostCentreNotExists(Name))
                    {
                        using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateCostCentre))
                        {
                            dataManager.Parameters.Add(AppSchema.CostCentre.ABBREVATIONColumn, GenerateCostCentreCode(), true);
                            dataManager.Parameters.Add(AppSchema.CostCentre.COST_CENTRE_NAMEColumn, Name);
                            resultArgs = dataManager.UpdateData();
                            if (resultArgs != null && resultArgs.Success)
                            {
                                int CostCentreId = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                int CostCategoryId = GetCostCategoryId(drItem["Category"].ToString());

                                //--------------Mapping Cost Centre to Cost Category--------------------------------
                                MapCostCentreToCostCategory(CostCategoryId, CostCentreId);

                                //-----------------Mapping Cost Centre to Current Project---------------------------

                                //On 30/11/202, To Map CC Project-wise/Ledger-wise
                                //MapCostCentreToProject(CostCentreId);
                                CallCostCentreProcessedCount(++CostCentrerocessedCount);
                                --CostCentrePendingCount;
                                CallCostCentrePendingCount();
                            }
                        }
                    }
                    else
                    {
                        //-----------------Mapping Cost Centre to Current Project---------------------------
                        //On 30/11/202, To Map CC Project-wise/Ledger-wise
                        //MapCostCentreToProject(GetCostCentreId(Name));
                    }
                    UpdateProgessStatus();
                }

            }
        }

        private void MapCostCentreToProject(int CostCentreId, int LId)
        {
            using (AcMePlusMigrationSystem AcMePlusMigration = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                AcMePlusMigration.ProjectId = ProjectId;
                AcMePlusMigration.LedgerId = LId;
                AcMePlusMigration.VoucherCostCentreId = CostCentreId;
                AcMePlusMigration.TransModeCC = "DR";
                AcMePlusMigration.MapCostCentre();
            }
        }

        private void MapCostCentreToCostCategory(int CostCategoryId, int CostCentreId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.MapCostCentreToCostCategory))
            {
                dataManager.Parameters.Add(AppSchema.CostCentreCategory.COST_CENTRECATEGORY_IDColumn, CostCategoryId);
                dataManager.Parameters.Add(AppSchema.CostCentre.COST_CENTRE_IDColumn, CostCentreId);
                resultArgs = dataManager.UpdateData();
            }
        }

        private int GetCostCategoryId(string Name)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.GetCostCentreCategoryId))
            {
                dataManager.Parameters.Add(AppSchema.CostCentreCategory.COST_CENTRE_CATEGORY_NAMEColumn, Name);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        private bool IsCostCentreNotExists(string Name)
        {
            int Count = 0;
            using (AcMePlusMigrationSystem AcMePlusMigration = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                Count = AcMePlusMigration.GetCostCentreId(Name);
            }
            return Count == 0;
        }

        private int GetCostCentreId(string CostCentreName)
        {
            int CCId = 0;
            using (AcMePlusMigrationSystem AcMePlusMigration = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                CCId = AcMePlusMigration.GetCostCentreId(CostCentreName);
            }
            return CCId;
        }

        private string GenerateCostCentreCode()
        {
            string GeneratedCode = string.Empty;
            using (AcMePlusMigrationSystem AcMePlusMigration = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                int Count = AcMePlusMigration.GenerateCostCentreCode();
                GeneratedCode = String.Format("CC{0}", ++Count);
                if (!string.IsNullOrEmpty(AcMePlusMigration.GetCostCentreCode(GeneratedCode)))
                {
                    GeneratedCode = String.Format("CC{0}", ++Count);
                }
            }
            return GeneratedCode;
        }
        #endregion

        #region Migrate Country
        private void MigrateCountry()
        {
            SetProgressBar();
            if (dtTallyCountry != null)
            {
                foreach (DataRow drItem in dtTallyCountry.Rows)
                {
                    SetMigrationStatusMessage("Country Information", dtTallyCountry.Rows.Count);
                    string Name = drItem["NAME"].ToString();
                    if (IsCountryNotExists(Name))
                    {
                        using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateCountry))
                        {
                            dataManager.Parameters.Add(AppSchema.Country.COUNTRYColumn, Name);
                            dataManager.Parameters.Add(AppSchema.Country.COUNTRY_CODEColumn, GenerateCountryCode());
                            dataManager.Parameters.Add(AppSchema.Country.CURRENCY_SYMBOLColumn, "$");
                            resultArgs = dataManager.UpdateData();
                        }
                    }
                    UpdateProgessStatus();
                }
            }
        }

        private string GenerateCountryCode()
        {
            string Code = string.Empty;
            using (AcMePlusMigrationSystem AcMePlusMigration = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                int Count = AcMePlusMigration.GetCountryCount();
                Code = String.Format("C{0}", ++Count);
                if (IsCountryCodeExists(Code))
                {
                    Code = String.Format("C{0}", ++Count);
                }
            }
            return Code;
        }

        private bool IsCountryCodeExists(string Code)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.GetCountryId))
            {
                dataManager.Parameters.Add(AppSchema.Country.COUNTRY_CODEColumn, Code);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger > 0;
        }

        private bool IsCountryNotExists(string Name)
        {
            int Count = 0;
            using (AcMePlusMigrationSystem AcMePlusMigration = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                Count = AcMePlusMigration.GetCountryId(Name);
            }
            return Count == 0;
        }
        #endregion

        #region Migrate States
        private void MigrateState()
        {
            SetProgressBar();
            if (dtTallyState != null)
            {
                SetMigrationStatusMessage("State Information", dtTallyState.Rows.Count);
                foreach (DataRow drItem in dtTallyState.Rows)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.InsertState))
                    {
                        string Name = drItem["STATE"].ToString();
                        int CountryId = GetCountryId(drItem["COUNTRY"].ToString());
                        if (IsStateNotExists(Name))
                        {
                            dataManager.Parameters.Add(AppSchema.State.STATE_NAMEColumn, Name);
                            dataManager.Parameters.Add(AppSchema.State.COUNTRY_IDColumn, CountryId);
                            resultArgs = dataManager.UpdateData();
                        }
                    }
                    UpdateProgessStatus();
                }
            }
        }

        private bool IsStateNotExists(string StateName)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.IsSateExists))
            {
                dataManager.Parameters.Add(AppSchema.State.STATE_NAMEColumn, StateName);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger == 0;
        }

        private int GetCountryId(string CountryName)
        {
            int CountryId = 0;
            using (AcMePlusMigrationSystem AcMePlusMigration = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                CountryId = AcMePlusMigration.GetCountryId(CountryName);
            }
            return CountryId;
        }
        #endregion

        #region Migrate Donor
        private void MigrateDonorInformation()
        {
            SetProgressBar();
            if (dtTallyDonor != null)
            {
                DonorPendingCount = dtTallyDonor.Rows.Count;
                int ProcessedCount = 0;
                SetMigrationStatusMessage("Donor Information", DonorPendingCount);
                CallDonorProcessedCount(0);
                CallDonorPendingCount();
                foreach (DataRow drItem in dtTallyDonor.Rows)
                {
                    int CountryId = GetCountryId(drItem["COUNTRY"].ToString());
                    string DonorName = drItem["NAME"].ToString();
                    if (CountryId > 0)
                    {
                        if (IsDonorNotExists(DonorName, CountryId))
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.MigrateDonorAuditor))
                            {
                                int Identity = 0;//0-Donor and 1-Auditor (Zero by default in Tally Migration)
                                int StateId = GetStateId(drItem["STATE"].ToString());
                                string Address = drItem["ADDRESS"].ToString();
                                Address = Address.Length > 100 ? Address.Substring(0, 100) : Address; //Database only accepts 100 char for address

                                dataManager.Parameters.Add(AppSchema.DonorAuditor.NAMEColumn, DonorName, true);
                                dataManager.Parameters.Add(AppSchema.DonorAuditor.TYPEColumn, drItem["DONORTYPE"].ToString().Equals("Institutional donors") ? 1 : 2);
                                dataManager.Parameters.Add(AppSchema.State.STATE_IDColumn, StateId == 0 ? GetDefaultStateId() : StateId); //Default State Id (Tamil Nadu)
                                dataManager.Parameters.Add(AppSchema.DonorAuditor.COUNTRY_IDColumn, CountryId);
                                dataManager.Parameters.Add(AppSchema.DonorAuditor.ADDRESSColumn, Address);
                                dataManager.Parameters.Add(AppSchema.DonorAuditor.PHONEColumn, drItem["MOBILENUMBER"].ToString());
                                dataManager.Parameters.Add(AppSchema.DonorAuditor.EMAILColumn, drItem["EMAILID"].ToString());
                                dataManager.Parameters.Add(AppSchema.DonorAuditor.IDENTITYKEYColumn, Identity);
                                resultArgs = dataManager.UpdateData();
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    int DonorId = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                    //------------------------------Map Project Donor----------------------------------
                                    MapDonorProject(DonorId);
                                }
                            }
                            CallDonorProcessedCount(++ProcessedCount);
                            --DonorPendingCount;
                            CallDonorPendingCount();
                        }
                        else
                        {
                            //------------------------------Map Project Donor----------------------------------
                            MapDonorProject(GetDonorId(DonorName));
                        }
                    }
                    UpdateProgessStatus();
                }
            }
        }

        private int GetDefaultStateId()
        {
            int StateId = 0;
            using (AcMePlusMigrationSystem AcMePlusMigration = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                StateId = AcMePlusMigration.GetDefaultStateId();
            }
            return StateId;
        }

        private void MapDonorProject(int DonorId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MapProjectDonor))
            {
                dataManager.Parameters.Add(AppSchema.VoucherMaster.DONOR_IDColumn, DonorId);
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.UpdateData();
            }
        }

        private int GetStateId(string Name)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.IsSateExists))
            {
                dataManager.Parameters.Add(AppSchema.State.STATE_NAMEColumn, Name);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        private int GetDonorId(string DonorName)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.GetDonorAuditorId))
            {
                dataManager.Parameters.Add(AppSchema.DonorAuditor.NAMEColumn, DonorName);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        private bool IsDonorNotExists(string DonorName, int CountryId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.IsDonorExists))
            {
                dataManager.Parameters.Add(AppSchema.DonorAuditor.NAMEColumn, DonorName);
                dataManager.Parameters.Add(AppSchema.DonorAuditor.COUNTRY_IDColumn, CountryId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger == 0;
        }
        #endregion

        #region Migrate Purpose
        private void MigrateMasterPurpose()
        {
            SetProgressBar();
            if (dtTallyPurpose != null)
            {
                SetMigrationStatusMessage("Purpose List", dtTallyPurpose.Rows.Count);
                foreach (DataRow drItem in dtTallyPurpose.Rows)
                {
                    string Name = drItem["NAME"].ToString();
                    if (IsPurposeNotExists(Name))
                    {
                        using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.MigrateFCPurpose))
                        {
                            dataManager.Parameters.Add(AppSchema.Purposes.CODEColumn, GetPurposeCode(), true);
                            dataManager.Parameters.Add(AppSchema.Purposes.FC_PURPOSEColumn, Name);
                            resultArgs = dataManager.UpdateData();
                            if (resultArgs.Success)
                                MapPurpose(NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()));
                        }
                    }
                    else
                    {
                        //Mapping Purpose
                        MapPurpose(GetPurposeId(Name));
                    }
                    UpdateProgessStatus();
                }
            }
        }

        private string GetPurposeCode()
        {
            string Code = string.Empty;
            using (AcMePlusMigrationSystem AcMePlusMigration = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                int Count = AcMePlusMigration.GeneratePurposeCode();
                Code = String.Format("PU{0}", ++Count);
                if (IsPurposeCodeNotExists(Code))
                {
                    Code = String.Format("PU{0}", ++Count);
                }
            }
            return Code;
        }

        private bool IsPurposeNotExists(string Name)
        {
            int Count = 0;
            using (AcMePlusMigrationSystem AcMePlusMigration = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                Count = AcMePlusMigration.GetCFCPurposeCode(Name);
            }
            return Count <= 0;
        }

        private bool IsPurposeCodeNotExists(string Code)
        {
            string Count = string.Empty;
            using (AcMePlusMigrationSystem AcMePlusMigration = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                Count = AcMePlusMigration.GetPurposeCode(Code);
            }
            return (!string.IsNullOrEmpty(Count));
        }

        private int GetPurposeId(string PurposeName)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.GetPurposeId))
            {
                dataManager.Parameters.Add(AppSchema.Purposes.FC_PURPOSEColumn, PurposeName);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        private void MapPurpose(int PurposeId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.MapDonor))
            {
                dataManager.Parameters.Add(AppSchema.Purposes.CONTRIBUTION_IDColumn, PurposeId);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.FDRegisters.TRANS_MODEColumn, "CR");
                resultArgs = dataManager.UpdateData();
            }
        }
        #endregion

        #region Migrate Voucher Transaction
        private void CallVoucherTransPendingCount()
        {
            if (UpdatevoucherTransPendingCount != null)
            {
                UpdatevoucherTransPendingCount(this, new EventArgs());
            }
        }

        private void CallVoucherTransProcessedCount(int Count)
        {
            if (UpdateVoucherTransProcessedCount != null)
            {
                VoucherTransProcessedCount = Count;
                UpdateVoucherTransProcessedCount(this, new EventArgs());
            }
        }

        private void MigrateTransaction()
        {
            if (dsTallyVoucher != null)
            {
                //DataSet dsVoucher = (DataSet)resultVoucher.DataSource.Data;
                DataTable dtVoucherMaster = dsTallyVoucher.Tables["MASTER VOUCHER"];
                DataTable dtVoucherTrans = dsTallyVoucher.Tables["VOUCHER DETAILS"];
                DataTable dtVoucherCCTrans = dsTallyVoucher.Tables["CCVoucher"];
                DataTable dtDonorVoucherEntry = dsTallyVoucher.Tables["DonorVoucher"];
                if (dtVoucherMaster != null)
                {
                    VoucherTransPendingCount = dtVoucherMaster.Rows.Count;
                    SetProgressBar();
                    CallVoucherTransProcessedCount(0);
                    CallVoucherTransPendingCount();
                    VoucherMasterTrans(dtVoucherMaster, dtVoucherTrans, dtVoucherCCTrans, dtDonorVoucherEntry);
                }
                else
                {
                    CallVoucherTransProcessedCount(0);
                    VoucherTransPendingCount = 0;
                    CallVoucherTransPendingCount();
                }
            }
        }

        private void VoucherMasterTrans(DataTable dtMasterVoucher, DataTable dtVoucherTrans, DataTable dtVoucherCCTrans, DataTable dtDonorVoucherEntry)
        {
            int ProcessedCount = 0;
            if (dtMasterVoucher != null)
            {
                SetMigrationStatusMessage("Voucher Details", dtMasterVoucher.Rows.Count);
                foreach (DataRow drItem in dtMasterVoucher.Rows)
                {
                    UpdateProgessStatus();
                    string VoucherDate = DateSet.ToDate(drItem["DATE"].ToString());
                    int VoucherId = NumberSet.ToInteger(drItem["VOUCHER_ID"].ToString());
                    int VoucherNo = NumberSet.ToInteger(drItem["VOUCHERNUMBER"].ToString());
                    string TallyBaseVoucherTypeName = drItem["BASEVOUCHERTYPE"].ToString().ToUpper();
                    string TallyVoucherTypeName = drItem["VOUCHERTYPENAME"].ToString().ToUpper();
                    string AcmeerpVoucherType = string.Empty;
                    Int32 VoucherDefinitionId = (Int32)DefaultVoucherTypes.Receipt;
                    switch (TallyBaseVoucherTypeName)
                    {
                        case "PAYMENT":
                        case "CASH PAYMENT": //'Cash Receipt', 'Cash Payment' (For Tally 8.0 converted database)
                            AcmeerpVoucherType = "PY";
                            VoucherDefinitionId = (Int32)DefaultVoucherTypes.Payment;

                            //To Allow Multi Payments for ABE
                            if (TallyVoucherTypeName.ToUpper() != "PAYMENT" && IncludeMultipleVoucherTypes) //this.IS_ABEBEN_DIOCESE
                            {
                                VoucherDefinitionId = GetVoucherTypeId(TallyVoucherTypeName, DefaultVoucherTypes.Payment);
                            }
                            break;
                        case "RECEIPT":
                        case "CASH RECEIPT":  //'Cash Receipt', 'Cash Payment' (For Tally 8.0 converted database)
                            AcmeerpVoucherType = "RC";
                            VoucherDefinitionId = (Int32)DefaultVoucherTypes.Receipt;
                            
                            //To Allow Multi Receipts for ABE
                            if (TallyVoucherTypeName.ToUpper() != "RECEIPT" && IncludeMultipleVoucherTypes) //this.IS_ABEBEN_DIOCESE
                            {
                                VoucherDefinitionId = GetVoucherTypeId(TallyVoucherTypeName, DefaultVoucherTypes.Receipt);
                            }
                            break;
                        case "CONTRA":
                            AcmeerpVoucherType = "CN";
                            VoucherDefinitionId = (Int32)DefaultVoucherTypes.Contra;
                            break;
                        case "JOURNAL":
                            AcmeerpVoucherType = "JN";
                            VoucherDefinitionId = (Int32)DefaultVoucherTypes.Journal;

                            //To Allow Multi Payments for ABE
                            if (TallyVoucherTypeName.ToUpper() != "JOURNAL" && IncludeMultipleVoucherTypes ) //this.IS_ABEBEN_DIOCESE
                            {
                                VoucherDefinitionId = GetVoucherTypeId(TallyVoucherTypeName, DefaultVoucherTypes.Journal);
                            }
                            break;
                    }
                    string VoucherSubType = "GN";
                    string Narration = drItem["NARRATION"].ToString().Contains("\"") ? drItem["NARRATION"].ToString().Replace('"', '\'') : drItem["NARRATION"].ToString();
                                       

                    using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.MigrateVoucherMaster))
                    {
                        dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_DATEColumn, VoucherDate, true);
                        dataManager.Parameters.Add(AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectId);
                        dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_NOColumn, VoucherNo);
                        dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_TYPEColumn, AcmeerpVoucherType);
                        dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_SUB_TYPEColumn, VoucherSubType);
                        dataManager.Parameters.Add(AppSchema.VoucherMaster.NARRATIONColumn, Narration);
                        dataManager.Parameters.Add(AppSchema.VoucherMaster.CREATED_BYColumn, CreatedBy);
                        dataManager.Parameters.Add(AppSchema.VoucherMaster.CREATED_BY_NAMEColumn, CreatedByName);
                        //dataManager.Parameters.Add(AppSchema.VoucherMaster.MODIFIED_BYColumn, NumberSet.ToInteger(LoginUserId));
                        dataManager.Parameters.Add(AppSchema.VoucherNumber.VOUCHER_DEFINITION_IDColumn, VoucherDefinitionId);
                        resultArgs = dataManager.UpdateData();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            int NewVoucherId = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                            //-------------------------Making Voucher Transaction for the Ledgers----------------------------------------
                            VoucherTrans(dtVoucherTrans, VoucherId, NewVoucherId, dtVoucherCCTrans, dtDonorVoucherEntry);
                        }
                    }
                    CallVoucherTransProcessedCount(++ProcessedCount);
                    --VoucherTransPendingCount;
                    CallVoucherTransPendingCount();
                }
            }
        }

        private string MapLedgerName(string LedgerName)
        {
            string Result = string.Empty;
            if (dtTallyLedger != null)
            {
                DataView dvLedger = new DataView(dtTallyLedger);
                dvLedger.RowFilter = String.Format("LedgerName='{0}'", LedgerName.Replace("'", "''"));
                if (dvLedger.ToTable() != null)
                {
                    DataTable dtLedgerFiltered = dvLedger.ToTable();
                    if (dtLedgerFiltered != null && dtLedgerFiltered.Rows.Count > 0)
                    {
                        if (dtTallyLedger.Columns.Contains("LEDGER_NAME")) //When Mapping is done
                            Result = string.IsNullOrEmpty(dtLedgerFiltered.Rows[0]["LEDGER_NAME"].ToString()) ? LedgerName : dtLedgerFiltered.Rows[0]["LEDGER_NAME"].ToString();
                        else //When Mapping is  not done
                            Result = string.IsNullOrEmpty(dtLedgerFiltered.Rows[0]["LedgerName"].ToString()) ? LedgerName : dtLedgerFiltered.Rows[0]["LedgerName"].ToString();
                    }
                }
            }
            return Result;
        }

        private void VoucherTrans(DataTable dtVoucherTrans, int OldVoucherId, int NewVoucherId, DataTable dtVoucherCCTrans, DataTable dtDonorVoucherEntry)
        {
            if (dtVoucherTrans != null)
            {
                DataView dvVoucherTrans = new DataView(dtVoucherTrans);
                dvVoucherTrans.RowFilter = String.Format("VOUCHER_ID={0}", OldVoucherId);
                DataTable dtVoucherTransFiltered = dvVoucherTrans.ToTable();
                if (dtVoucherTrans != null)
                {
                    int VoucherTransSequenceNo = 1;
                    int SequenceNo = 1;
                    foreach (DataRow drItem in dtVoucherTransFiltered.Rows)
                    {
                        string ChequeNo = string.Empty;
                        string Cheque_Ref_Date = string.Empty;
                        string Cheque_Ref_BankName = string.Empty;
                        string Cheque_Ref_BankBranch = string.Empty;
                        string MaterializedOn = string.Empty;

                        //int LedgerId = GetLedgerId(drItem["LEDGERNAME"].ToString());
                        string ledgername = drItem["LEDGERNAME"].ToString();

                        int LedgerId = GetLedgerId(MapLedgerName(ledgername));
                        if (LedgerId > 0)
                        {
                            using (AcMePlusMigrationSystem AcMePlusMigration = new AcMePlusMigrationSystem(MigrationType.Tally))
                            {
                                double Amount = NumberSet.ToDouble(drItem["AMOUNT"].ToString());
                                //In Tally Migration All Negative Amount should be 'DR'
                                //And Positive Amount should be 'CR'
                                string TransMode = Amount > 0 ? "CR" : "DR";// GetTransMode(RefAccessTally, LedgerId, Amount);
                                //If ledger is bankaccount ledger, update bank reference and its allocation details
                                if (IsBankAccountLedger(LedgerId))
                                {
                                    ChequeNo = drItem["INSTRUMENTNUMBER"].ToString().Trim();
                                    Cheque_Ref_Date = string.IsNullOrEmpty(drItem["INSTRUMENTDATE"].ToString()) ? string.Empty : drItem["INSTRUMENTDATE"].ToString();
                                    MaterializedOn = string.IsNullOrEmpty(drItem["BANKERSDATE"].ToString()) ? string.Empty : drItem["BANKERSDATE"].ToString();
                                    Cheque_Ref_BankName = drItem["BANKNAME"].ToString();
                                    Cheque_Ref_BankBranch = drItem["BANKBRANCHNAME"].ToString();
                                    ChequeNo = ChequeNo.Length > 25 ? ChequeNo.Substring(0, 25) : ChequeNo; //Acme.erp Database only accepts 25 char for cheqno
                                    Cheque_Ref_BankName = Cheque_Ref_BankName.Length > 50 ? Cheque_Ref_BankName.Substring(0, 50) : Cheque_Ref_BankName; //Acme.erp Database only accepts 50 chars for Bank name
                                    Cheque_Ref_BankBranch = Cheque_Ref_BankBranch.Length > 50 ? Cheque_Ref_BankBranch.Substring(0, 50) : Cheque_Ref_BankBranch; //Acme.erp Database only accepts 50 chars for Bank branch
                                }

                                int FindCCId = NumberSet.ToInteger(drItem["ALLLEDGERENTRIES.LIST_Id"].ToString());

                                AcMePlusMigration.VoucherId = NewVoucherId;
                                AcMePlusMigration.SequenceNo = VoucherTransSequenceNo;
                                AcMePlusMigration.LedgerId = LedgerId;
                                AcMePlusMigration.TransAmount = Math.Abs(Amount);
                                AcMePlusMigration.TranMode = TransMode;
                                AcMePlusMigration.ChequeNo = ChequeNo;
                                AcMePlusMigration.Cheque_Ref_Date = Cheque_Ref_Date;
                                AcMePlusMigration.Cheque_Ref_Bank = Cheque_Ref_BankName;
                                AcMePlusMigration.Cheque_Ref_BankBranch = Cheque_Ref_BankBranch;
                                AcMePlusMigration.deMaterializedOn = MaterializedOn;
                                AcMePlusMigration.MigrateVoucherTransWithChequeNo();

                                VoucherCostCentreTrans(dtVoucherCCTrans, FindCCId, NewVoucherId, LedgerId, ref SequenceNo, dtDonorVoucherEntry, Amount, VoucherTransSequenceNo);
                                VoucherTransSequenceNo++;
                            }
                        }
                    }
                }
            }
        }

        private void VoucherCostCentreTrans(DataTable dtVoucherCCTrans, int FindCCId, int VoucherId, int LedgerId, ref int SequenceNo, DataTable dtDonorVoucherEntry, double DonorActualAmount, int LedgerSequenceNo)
        {
            DataView dvCCEntry = new DataView(dtVoucherCCTrans);
            //dvCCEntry.RowFilter = String.Format("COSTCENTREALLOCATIONS.LIST_ID={0}", FindCCId);
            dvCCEntry.RowFilter = String.Format("ALLLEDGERENTRIES.LIST_Id={0}", FindCCId);
            DataTable dtCCTransaction = dvCCEntry.ToTable();
            if (dtCCTransaction != null)
            {
                foreach (DataRow drItem in dtCCTransaction.Rows)
                {
                    int CostCenterAllocationId = NumberSet.ToInteger(drItem["COSTCENTREALLOCATIONS.LIST_ID"].ToString());
                    double Amount = Math.Abs(NumberSet.ToDouble(drItem["AMOUNT"].ToString()));
                    if (IsDonorModuleEnabled)
                    {
                        if (drItem["CATEGORY"].ToString() == "Purposes")  //if (IsDonorTransactionMade(RefAccessTally, dtDonorVoucherEntry, CostCenterAllocationId))
                        {
                            int PurposeId = GetPurposeId(drItem["NAME"].ToString());
                            MigrateDonorTransactionDetails(dtDonorVoucherEntry, CostCenterAllocationId, VoucherId, Amount, DonorActualAmount, PurposeId);
                        }
                        else
                        {
                            MigrateCostCentreTrans(drItem["NAME"].ToString(), VoucherId, LedgerId, Amount, SequenceNo, LedgerSequenceNo);
                            SequenceNo++;

                        }
                    }
                    else
                    {
                        MigrateCostCentreTrans(drItem["NAME"].ToString(), VoucherId, LedgerId, Amount, SequenceNo, LedgerSequenceNo);
                        SequenceNo++;
                    }
                }
            }
        }

        private void MigrateCostCentreTrans(string Name, int VoucherId, int LedgerId, double Amount, int SequenceNo, int LedgerSequenceNo)
        {
            int CostCentreId = GetCostCentreId(Name);

            if (CostCentreId > 0)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.MigrateCostCentreTransaction))
                {
                    string CostCentreTable = "0LDR" + LedgerId;
                    dataManager.Parameters.Add(AppSchema.VoucherCostCentre.VOUCHER_IDColumn, VoucherId);
                    dataManager.Parameters.Add(AppSchema.VoucherCostCentre.LEDGER_IDColumn, LedgerId);
                    dataManager.Parameters.Add(AppSchema.VoucherCostCentre.COST_CENTRE_IDColumn, CostCentreId);
                    dataManager.Parameters.Add(AppSchema.VoucherCostCentre.COST_CENTRE_TABLEColumn, CostCentreTable);
                    dataManager.Parameters.Add(AppSchema.VoucherCostCentre.AMOUNTColumn, Amount);
                    dataManager.Parameters.Add(AppSchema.VoucherTransaction.SEQUENCE_NOColumn, SequenceNo);
                    dataManager.Parameters.Add(AppSchema.VoucherCostCentre.LEDGER_SEQUENCE_NOColumn, LedgerSequenceNo);
                    resultArgs = dataManager.UpdateData();
                }

                //On Map Cost Center based on Transactions
                MapCostCentreToProject(CostCentreId, LedgerId);
            }

        }

        private void MigrateDonorTransactionDetails(DataTable dtDonorDetails, int CostCentreAllocationId, int VoucherId, double CalculatedAmount, double ActualAmount, int PurposeId)
        {
            DataTable dtFilteredDonorInfo = FilterDonorDetails(dtDonorDetails, CostCentreAllocationId);
            if (dtFilteredDonorInfo != null)
            {
                //Modified by carmel Raj M on September-04-2015
                //Purpose :To show Purpose for the payment voucher also. It means that payment voucher may not have Donor but can have Purpose
                if (dtFilteredDonorInfo.Rows.Count == 0 && PurposeId > 0)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.UpdateDonorTransaction))
                    {
                        int DonorId = 0; double ContributionAmount = 0;
                        dataManager.Parameters.Add(AppSchema.VoucherMaster.DONOR_IDColumn, DonorId);
                        dataManager.Parameters.Add(AppSchema.VoucherMaster.CONTRIBUTION_AMOUNTColumn, Math.Abs(ContributionAmount));
                        dataManager.Parameters.Add(AppSchema.VoucherMaster.CALCULATED_AMOUNTColumn, Math.Abs(CalculatedAmount));
                        dataManager.Parameters.Add(AppSchema.VoucherMaster.ACTUAL_AMOUNTColumn, Math.Abs(ActualAmount));
                        dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                        dataManager.Parameters.Add(AppSchema.VoucherMaster.PURPOSE_IDColumn, PurposeId);
                        resultArgs = dataManager.UpdateData();
                    }
                }
                else
                {
                    foreach (DataRow drItem in dtFilteredDonorInfo.Rows)
                    {
                        double ContributionAmount = NumberSet.ToDouble(drItem["DONATIONAMOUNT"].ToString());
                        int DonorId = GetDonorId(drItem["DONOR"].ToString());

                        if (DonorId > 0 && PurposeId > 0)
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.UpdateDonorTransaction))
                            {
                                dataManager.Parameters.Add(AppSchema.VoucherMaster.DONOR_IDColumn, DonorId);
                                dataManager.Parameters.Add(AppSchema.VoucherMaster.CONTRIBUTION_AMOUNTColumn, Math.Abs(ContributionAmount));
                                dataManager.Parameters.Add(AppSchema.VoucherMaster.CALCULATED_AMOUNTColumn, Math.Abs(CalculatedAmount));
                                dataManager.Parameters.Add(AppSchema.VoucherMaster.ACTUAL_AMOUNTColumn, Math.Abs(ActualAmount));
                                dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                                dataManager.Parameters.Add(AppSchema.VoucherMaster.PURPOSE_IDColumn, PurposeId);
                                resultArgs = dataManager.UpdateData();
                            }
                        }
                        else //Just for Testing only (Can be Removed later)
                        {
                        }
                    }
                }
            }
        }

        public ResultArgs EnableCostCentreForLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.EnableCostCentreLedger))
            {
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private DataTable FilterDonorDetails(DataTable dtDonorDetails, int CostCentreAllocationId)
        {
            DataTable dtFilteredDonorDetails = null;
            if (dtDonorDetails != null)
            {
                DataView dvDonorDetails = new DataView(dtDonorDetails);
                dvDonorDetails.RowFilter = String.Format("COSTCENTREALLOCATIONS.LIST_ID={0}", CostCentreAllocationId);
                dtFilteredDonorDetails = dvDonorDetails.ToTable();
            }
            return dtFilteredDonorDetails;
        }
        #endregion

        #region Update Openning Balance Date
        private void UpdateLedgerOpeningBalanceDate()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.UpdateLedgerOpBalaceDate))
            {
                string Date = GetLeastBookBeginningFromDateFromAcMeERP();
                dataManager.Parameters.Add(AppSchema.LedgerBalance.BALANCE_DATEColumn, DateSet.ToDate(Date, false).AddDays(-1));
                resultArgs = dataManager.UpdateData();
            }
        }

        private string GetLeastBookBeginningFromDateFromAcMeERP()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.GetBookBeginningDate))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToString;
        }

        public void UpdateVoucherMasterTrans()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.UpdateVoucherMasterTransTableDates))
            {
                resultArgs = dataManager.UpdateData();
            }
        }

        private void UpdateTransactionCountry()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.UpdateVoucherCountry))
            {
                resultArgs = dataManager.UpdateData();
            }
        }
        #endregion

        #region Refresh Balance

        private void ExecuteFlushCommands()
        {
            using (DataManager dataManger = new DataManager())
            {
                resultArgs = dataManger.UpdateData("FLUSH TABLES;FLUSH HOSTS;");
            }
        }

        public ResultArgs RefreshBalance()
        {
            SetProgressBar();
            balanceSystem.VoucherDate = BookBeginFrom;
            balanceSystem.RefreshBalanceSetMaxValue += new EventHandler(BalanceSystem_RefreshBalanceSetMaxValue);
            balanceSystem.RefreshBalanceUpdateProgressBar += new EventHandler(BalanceSystem_RefreshBalanceUpdateProgressBar);
            ResultArgs result = balanceSystem.UpdateBulkTransBalance();
            return resultArgs;
        }

        #endregion

        #region UI Updation (Triggering Events)
        private void SetProgressBar()
        {
            if (InitProgressBar != null)
            {
                InitProgressBar(this, new EventArgs());
            }
        }

        private void SetMigrationStatusMessage(string Message, int MaxCount, bool ChangesDefaultMessage = false)
        {
            if (UpdateMessage != null)
            {
                ProgressMaxCount = MaxCount;
                StatusMessage = Message;
                UpdateMessage(this, new EventArgs());
            }
        }

        private void UpdateProgessStatus()
        {
            if (IncreaseProgressBar != null)
            {
                IncreaseProgressBar(this, new EventArgs());
            }
        }

        private void BalanceSystem_RefreshBalanceSetMaxValue(object sender, EventArgs e)
        {
            if (UpdateMessage != null)
            {
                SetMigrationStatusMessage("Refreshing Balance", balanceSystem.RefreshProgressBarMaxCount, true);
            }
        }

        private void BalanceSystem_RefreshBalanceUpdateProgressBar(object sender, EventArgs e)
        {
            UpdateProgessStatus();
        }

        #region Statics Count
        private void UpdateGroupPendingCount()
        {
            if (UpdateGroupPendingCountEve != null)
            {
                UpdateGroupPendingCountEve(this, new EventArgs());
            }
        }

        private void CallGroupProcessedCount(int Count)
        {
            if (UpdateGroupProcessedCount != null)
            {
                GroupProcessedCount = Count;
                UpdateGroupProcessedCount(this, new EventArgs());
            }
        }

        private void CallLedgerProcessedCount(int Count)
        {
            if (UpdateLedgerProcessedCount != null)
            {
                LedgerProcessedCount = Count;
                UpdateLedgerProcessedCount(this, new EventArgs());
            }
        }

        private void CallLedgerPendingCount()
        {
            if (UpdateLedgerPendingCountEve != null)
            {
                UpdateLedgerPendingCountEve(this, new EventArgs());
            }
        }

        private void CallCostCentreCategoryPending()
        {
            if (UpdateCostCentreCategoryPendingCount != null)
            {
                UpdateCostCentreCategoryPendingCount(this, new EventArgs());
            }
        }

        private void CallCostCentreCategroyProcessedCount(int Count)
        {
            if (UpdateCostCentreCategoryProcessedCount != null)
            {
                CCCategoryProcessedCount = Count;
                UpdateCostCentreCategoryProcessedCount(this, new EventArgs());
            }
        }

        private void CallCostCentreProcessedCount(int Count)
        {
            if (UpdateCostCentreProcessedCount != null)
            {
                UpdateCostCentreProcessedCount(this, new EventArgs());
            }
        }

        private void CallCostCentrePendingCount()
        {
            if (UpdateCostCentrePendingCount != null)
            {
                UpdateCostCentrePendingCount(this, new EventArgs());
            }
        }

        private void CallDonorPendingCount()
        {
            if (UpdateDonorPendingCount != null)
            {
                UpdateDonorPendingCount(this, new EventArgs());
            }
        }

        private void CallDonorProcessedCount(int Count)
        {
            if (UpdateDonorProcessedCount != null)
            {
                DonorProcessedCount = Count;
                UpdateDonorProcessedCount(this, new EventArgs());

            }
        }
        #endregion


        #endregion

        #region Delete Migration
        public DataTable FetchAllOpeningBalance()
        {
            DataTable dtOpBal = null;
            using (DataManager dataMager = new DataManager(SQLCommand.TallyMigration.FetchAllOpeningBalace))
            {
                resultArgs = dataMager.FetchData(DataSource.DataTable);
                if (resultArgs != null && resultArgs.Success)
                {
                    dtOpBal = resultArgs.DataSource.Table;
                }
            }
            return dtOpBal;
        }

        public ResultArgs UpdateOpBalance(string BalanceDate, int ProjectId, int LedgerId, double Amount)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.UpdateDeleteOPBalance))
            {
                dataManager.Parameters.Add(AppSchema.LedgerBalance.BALANCE_DATEColumn, BalanceDate);
                dataManager.Parameters.Add(AppSchema.LedgerBalance.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(AppSchema.LedgerBalance.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(AppSchema.LedgerBalance.AMOUNTColumn, Amount);
                dataManager.Parameters.Add(AppSchema.LedgerBalance.TRANS_MODEColumn, Amount > 0 ? "DR" : "CR");
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteTrans(string DateBefore)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.DeleteTransaction))
            {
                dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_DATEColumn, DateBefore);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs UpdateOpBalanceDate(string OPDate)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.UpdateOPDate))
            {
                dataManager.Parameters.Add(AppSchema.LedgerBalance.BALANCE_DATEColumn, OPDate);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteOpBalanceDate()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.DeleteOPBalance))
            {
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        //public void DeleteUnusedLedgersFromAcMeERP()
        //{
        //    using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.DeleteUnusedLedgers))
        //    {
        //        resultArgs = dataManager.UpdateData();
        //    }
        //}

        /// <summary>
        /// Created by alwar on 02/12/2015
        /// This function is used to get Nature Type (A,L, I, E) for given Ledger from Acme.erp by using its ledger group,
        /// If Leadger Name is not existing in the Acmeerp, It will check Leadger's group nature which is coming from Tally
        /// For Ex this leadger "Bosco Counselling Services" group's might be changed in Acme.erp, 
        /// so when we take update op, check ledgers nature (if ledger is already exists in acmeerp, take its nature or take tally group nature)
        /// </summary>
        public int GetLedgerNature(string ledgername, string primarygroup)
        {
            int nature = 0;

            //If tally ledger is not exists in Acmeerp, check its group's nature in Acmeerp.
            if (IsLedgerNotExists(ledgername))
            {
                //Get Leadger's Group Nature, IF ASSET, Libilities, take opening balance
                using (AcMePlusMigrationSystem AcMePlusMigration = new AcMePlusMigrationSystem(MigrationType.Tally))
                {
                    nature = AcMePlusMigration.GetGroupNatureId(primarygroup);
                }
            }
            else //If tally ledger is avilable in Acmerp, get its ledger group from acme.erp and take its nature
            {
                int ledgerId = GetLedgerId(ledgername);
                using (LedgerSystem ledgersys = new LedgerSystem(ledgerId))
                {
                    nature = ledgersys.FetchLedgerNature();
                }
            }

            return nature;
        }

        #endregion

        #region Export to Tally

        /// <summary>
        /// This method is used to export slected Acme.erp project's masters and its vouchers for given time
        /// 1. Ledger Group
        /// 2. Ledgers
        /// 3. Upadte Company Feature (Enable cc, Enable cc category)
        /// 4. Cost Category
        /// 5. Cost Center
        /// 6. Vouchers
        /// </summary>
        /// <param name="AcmeerpProejctId"></param>
        /// <param name="AcmeerpProject"></param>
        /// <param name="TallyCompanyName"></param>
        /// <param name="dateFromm"></param>
        /// <param name="dateTo"></param>
        /// <param name="dateTallyBooksBegin"></param>
        /// <returns></returns>
        public ResultArgs ExportToTally(Int32 AcmeerpProejctId, string AcmeerpProject, string TallyCompanyName,
                        DateTime dateFrom, DateTime dateTo, DateTime dateTallyBooksBegin, bool IsIncludeOPBalance, bool overwrite,
                        bool includeAssetOpBalance, bool includeExpenseOpBalance, bool includeIncomeOpBalance, bool includeLiabilitiesOpBalance)
        {
            ResultArgs resultargs = new ResultArgs();
            try
            {
                //11. Export Ledger Group
                resultargs = ExportVoucherType();
                if (resultargs.Success)
                {
                    //1. Export Ledger Group
                    resultargs = ExportLedgerGroup();
                    if (resultargs.Success)
                    {
                        //2. Export Ledger
                        resultargs = ExportLedger(AcmeerpProejctId, dateFrom, IsIncludeOPBalance, includeAssetOpBalance, includeExpenseOpBalance, includeIncomeOpBalance, includeLiabilitiesOpBalance);
                        if (resultargs.Success)
                        {
                            //3. Export Cost Center, Cost Category and enable Costcenter feature
                            resultargs = ExportCostCenter(AcmeerpProejctId, TallyCompanyName);
                            if (resultargs.Success)
                            {
                                //3. Export Vouchers and its details
                                resultargs = ExportVouchers(AcmeerpProejctId, dateFrom, dateTo, overwrite);
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                resultargs.Message = "Could not export to Tally, " + err.Message;
            }
            return resultargs;
        }

        /// <summary>
        /// Export Ledger group to tally in two cases
        /// 1. First lever groups, which is under Nature
        /// 2. Second Level groups
        /// </summary>
        /// <returns></returns>
        private ResultArgs ExportLedgerGroup()
        {
            string ProgressMessage = "Exporting Ledger Group";
            ResultArgs resultargs = new ResultArgs();
            SetExportStatusMessage(ProgressMessage, 1);
            try
            {
                using (TallyConnector tallyConnector = new TallyConnector())
                {
                    resultargs = GetData(SQLCommand.TallyExport.FetchLedgerGroup, 0, DateTime.Now, DateTime.Now);
                    if (resultargs.Success && resultargs.DataSource.Table != null)
                    {
                        DataTable dtLedgerGroup = resultargs.DataSource.Table;
                        //1. Export first leverl ledger group(parent group = Incomes, Expenses,  
                        resultargs = ExportLedgerGroup(dtLedgerGroup, true);

                        //2. Export second level Ledgers groups
                        if (resultargs.Success)
                        {
                            resultargs = ExportLedgerGroup(dtLedgerGroup, false);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                resultargs.Message = "Could not export Ledger Group to Tally, " + err.Message;
            }
            return resultargs;
        }

        /// <summary>
        /// Export Ledger group to tally in two cases
        /// 1. First lever groups, which is under Nature
        /// 2. Second Level groups
        /// </summary>
        /// <param name="dtLedgerGrp"></param>
        /// <param name="firstlevel"></param>
        /// <returns></returns>
        private ResultArgs ExportLedgerGroup(DataTable dtLedgerGrp, bool firstlevel)
        {
            string ProgressMessage = "Exporting Ledger Group";
            ResultArgs resultargs = new ResultArgs();
            using (TallyConnector tallyConnector = new TallyConnector())
            {
                try
                {
                    string levelfilter = string.Empty;
                    if (firstlevel)
                    {
                        ProgressMessage = "Exporting Primary Ledger Group";
                        levelfilter = AppSchema.LedgerGroup.ParentGroupColumn + "=" + AppSchema.LedgerGroup.NATUREColumn;
                    }
                    else
                    {
                        ProgressMessage = "Exporting Ledger Group";
                        levelfilter = AppSchema.LedgerGroup.ParentGroupColumn + "<>" + AppSchema.LedgerGroup.NATUREColumn;
                    }
                    dtLedgerGrp.DefaultView.RowFilter = string.Empty;
                    dtLedgerGrp.DefaultView.RowFilter = levelfilter;

                    SetExportStatusMessage("Exporting Ledger Group", dtLedgerGrp.DefaultView.Count);
                    int rows = 0;
                    foreach (DataRowView drv in dtLedgerGrp.DefaultView)
                    {
                        string ledgergroup = drv[AppSchema.LedgerGroup.LEDGER_GROUPColumn.ColumnName].ToString();
                        string ledgergroupParent = drv[AppSchema.LedgerGroup.ParentGroupColumn.ColumnName].ToString();
                        string nature = drv[AppSchema.LedgerGroup.NATUREColumn.ColumnName].ToString();

                        nature = (nature == "Assets" ? "Assert" : (nature == "Liabilities" ? "Libilities" : nature));
                        Natures enumNature = (Natures)EnumSet.GetEnumItemType(typeof(Natures), nature);
                        resultargs = tallyConnector.InsertLedgerGroup(ledgergroup, ledgergroupParent, enumNature, firstlevel);
                        rows++;
                        UpdateExportProgessStatus(ProgressMessage + " (" + rows.ToString() + "/" + dtLedgerGrp.DefaultView.Count + ")");
                        if (!resultargs.Success)
                        {
                            break;
                        }
                    }
                    dtLedgerGrp.DefaultView.RowFilter = string.Empty;
                }
                catch (Exception err)
                {
                    dtLedgerGrp.DefaultView.RowFilter = string.Empty;
                    resultargs.Message = "Could not export Ledger Group to Tally, " + err.Message;
                }
            }
            return resultargs;
        }

        /// <summary>
        /// Export Ledger to tally
        /// </summary>
        /// <returns></returns>
        private ResultArgs ExportLedger(Int32 ProjectId, DateTime OpBalanceDate, bool IncludeOpBalance, 
            bool includeAssetOpBalance, bool includeExpenseOpBalance, bool includeIncomeOpBalance, bool includeLiabilitiesOpBalance)
        {
            ResultArgs resultargs = new ResultArgs();
            string ProgressMessage = "Exporting Ledger";
            SetExportStatusMessage(ProgressMessage, 1);
            try
            {
                using (TallyConnector tallyConnector = new TallyConnector())
                {
                    resultargs = GetData(SQLCommand.TallyExport.FetchLedger, ProjectId, OpBalanceDate, DateTime.Now);
                    if (resultargs.Success && resultargs.DataSource.Table != null)
                    {
                        DataTable dtLedger = resultargs.DataSource.Table;
                        SetExportStatusMessage(ProgressMessage, dtLedger.Rows.Count);
                        Int32 rows = 0;
                        foreach (DataRow dr in dtLedger.Rows)
                        {
                            Int32 natureid = NumberSet.ToInteger(dr[AppSchema.LedgerGroup.NATURE_IDColumn.ColumnName].ToString());
                            string ledgername = dr[AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                            string ledgergroup = dr[AppSchema.LedgerGroup.LEDGER_GROUPColumn.ColumnName].ToString();
                            string Is_CC = dr[AppSchema.Ledger.IS_COST_CENTERColumn.ColumnName].ToString();
                            bool iscc = (Is_CC == "0" ? false : true);
                            string bank = dr[AppSchema.Bank.BANKColumn.ColumnName].ToString();
                            string branch = dr[AppSchema.Bank.BRANCHColumn.ColumnName].ToString();
                            string accountholdernmae = dr[AppSchema.BankAccount.ACCOUNT_HOLDER_NAMEColumn.ColumnName].ToString();
                            string name = dr[AppSchema.LedgerProfileData.NAMEColumn.ColumnName].ToString();
                            string address = dr[AppSchema.LedgerProfileData.ADDRESSColumn.ColumnName].ToString();
                            string pannumber = dr[AppSchema.LedgerProfileData.PAN_NUMBERColumn.ColumnName].ToString();
                            string pincode = dr[AppSchema.LedgerProfileData.PIN_CODEColumn.ColumnName].ToString();
                            string OpAmount = dr[AppSchema.LedgerBalance.AMOUNTColumn.ColumnName].ToString();

                            //# 29/05/2020, Ledger Opening balance based on user input
                            //(Asset, Expense, Income and Liabilities) ledger opening balances based on user selection or input
                            bool updateOpbalance = IncludeOpBalance;
                            if (updateOpbalance)
                            {
                                if (natureid == (int)Natures.Assert)
                                    updateOpbalance = (natureid == (int)Natures.Assert && includeAssetOpBalance == true);
                                else if (natureid == (int)Natures.Expenses)
                                    updateOpbalance = (natureid == (int)Natures.Expenses && includeExpenseOpBalance == true);
                                else if (natureid == (int)Natures.Income)
                                    updateOpbalance = (natureid == (int)Natures.Income && includeIncomeOpBalance == true);
                                else if (natureid == (int)Natures.Libilities)
                                    updateOpbalance = (natureid == (int)Natures.Libilities && includeLiabilitiesOpBalance == true);
                            }

                            resultargs = tallyConnector.InsertLedger(ledgername, ledgergroup, iscc, name, address, string.Empty, pincode,
                                            bank, branch, accountholdernmae, pannumber, updateOpbalance, OpAmount);

                            //resultargs.Success = true;
                            rows++;
                            UpdateExportProgessStatus(ProgressMessage + " (" + rows.ToString() + "/" + dtLedger.Rows.Count + ")");
                            if (!resultargs.Success)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                resultargs.Message = "Could not export Ledger to Tally, " + err.Message;
            }
            return resultargs;
        }

        /// <summary>
        /// This method is used to export costcenter and costcategory and update costcenter feature in company
        /// 
        /// Check if cost center avilable, 
        /// 1. Enable Cost center feature in tally
        /// 2. Export CostCategory 
        /// 3. Export Cost center
        /// </summary>
        /// <returns></returns>
        private ResultArgs ExportCostCenter(Int32 ProjectId, string TallyCompanyName)
        {
            ResultArgs resultargs = new ResultArgs();
            string ProgressMessage = "Exporting CostCenter";
            SetExportStatusMessage(ProgressMessage, 1);
            try
            {
                using (TallyConnector tallyConnector = new TallyConnector())
                {
                    resultargs = GetData(SQLCommand.TallyExport.FetchCostCenter, ProjectId, DateTime.Now, DateTime.Now);
                    if (resultargs.Success && resultargs.DataSource.Table != null)
                    {
                        DataTable dtCostCenter = resultargs.DataSource.Table;
                        SetExportStatusMessage(ProgressMessage, dtCostCenter.Rows.Count);

                        //1. If CostCenter is avilable, Export all cost category and enable its feature in tally
                        if (dtCostCenter.Rows.Count > 0)
                        {
                            //2. enable Costcenter and Costcategory feature in Tally
                            resultargs = tallyConnector.UpdateCompanyFeatures(TallyCompanyName, true);

                            //In Tally Voucher Entry, Multiple CC category is not allowed, it accepts only for particular CC category, so we fix all CC are under "Acmeerp Cost Category"
                            ////3. Export Costcategory 
                            //if (resultargs.Success)
                            //{
                            //    resultargs = ExportCostCategory();
                            //}
                            if (resultargs.Success)
                            {
                                resultargs = tallyConnector.InsertCostCategory(tallyConnector.Def_CC_Category);
                            }
                        }

                        //4. Export CostCenter
                        if (resultargs.Success)
                        {
                            ProgressMessage = "Exporting CostCenter";
                            Int32 rows = 0;
                            foreach (DataRow dr in dtCostCenter.Rows)
                            {
                                string costcentername = dr[AppSchema.CostCentre.COST_CENTRE_NAMEColumn.ColumnName].ToString();
                                string costcategory = dr[AppSchema.CostCentreCategory.COST_CENTRE_CATEGORY_NAMEColumn.ColumnName].ToString();

                                resultargs = tallyConnector.InsertCostCenter(costcentername, costcategory);
                                rows++;
                                UpdateExportProgessStatus(ProgressMessage + " (" + rows.ToString() + "/" + dtCostCenter.Rows.Count + ")");
                                if (!resultargs.Success)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                resultargs.Message = "Could not export CostCenter to Tally, " + err.Message;
            }
            return resultargs;
        }

        /// <summary>
        /// This method is used to export vouchertype
        /// This will be called only for ABE alone
        /// </summary>
        /// <returns></returns>
        private ResultArgs ExportVoucherType()
        {
            ResultArgs resultargs = new ResultArgs();
            string ProgressMessage = "Exporting Voucher Type";
            SetExportStatusMessage(ProgressMessage, 1);
            try
            {
                //On 13/02/2019, expoft of multi voucher type is only for ABE
                //if (this.IS_ABEBEN_DIOCESE)
                if (IncludeMultipleVoucherTypes)
                {
                    string MissedVoucherTypes = string.Empty;
                    using (TallyConnector tallyConnector = new TallyConnector())
                    {
                        resultargs = GetData(SQLCommand.TallyExport.FetchVoucherType, ProjectId, DateTime.Now, DateTime.Now);
                        if (resultargs.Success && resultargs.DataSource.Table != null)
                        {
                            DataTable dtVoucherTypes = resultargs.DataSource.Table;
                            SetExportStatusMessage(ProgressMessage, dtVoucherTypes.Rows.Count);

                            //1. Export  Voucher types (other than default voucher types (Receipt/Payment/contra/journal) is avilable, Export all voucher types
                            if (resultargs.Success)
                            {
                                ProgressMessage = "Exporting Voucher Types";
                                Int32 rows = 0;
                                foreach (DataRow dr in dtVoucherTypes.Rows)
                                {
                                    string vouchertypename = dr[AppSchema.Voucher.VOUCHER_NAMEColumn.ColumnName].ToString();
                                    string basevouchertypename = dr[AppSchema.Voucher.VOUCHER_TYPEColumn.ColumnName].ToString();
                                    Int32 beginingnumber = NumberSet.ToInteger(dr[AppSchema.Voucher.STARTING_NUMBERColumn.ColumnName].ToString());
                                    bool isPrefixZero = (NumberSet.ToInteger(dr[AppSchema.Voucher.PREFIX_WITH_ZEROColumn.ColumnName].ToString()) == 0);
                                    Int32 numberwith = NumberSet.ToInteger(dr[AppSchema.Voucher.PREFIX_WITH_ZEROColumn.ColumnName].ToString());

                                    resultargs = tallyConnector.InsertVoucherType(vouchertypename, basevouchertypename, beginingnumber, isPrefixZero, numberwith);
                                    rows++;
                                    UpdateExportProgessStatus(ProgressMessage + " (" + rows.ToString() + "/" + dtVoucherTypes.Rows.Count + ")");
                                    //if (!resultargs.Success)
                                    //{
                                    //    break;
                                    //}
                                    if (!resultargs.Success)
                                    {
                                        if (!String.IsNullOrEmpty(MissedVoucherTypes)) MissedVoucherTypes += Environment.NewLine;
                                        MissedVoucherTypes += vouchertypename + " (" + basevouchertypename + ")";
                                    }
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(MissedVoucherTypes))
                    {
                        resultargs.Success = false;
                        resultargs.Message = "The following Voucher Type(s) are missing in TALLY, Create these Voucher Types in TALLY" + Environment.NewLine + Environment.NewLine + MissedVoucherTypes;
                    }
                }
                else
                {
                    resultargs.Success = true;
                }
            }
            catch (Exception err)
            {
                resultargs.Message = "Could not export VoucherType to Tally, " + err.Message;
            }
            return resultargs;
        }

        /// <summary>
        /// This method is used to export costcategory
        /// </summary>
        /// <returns></returns>
        private ResultArgs ExportCostCategory()
        {
            ResultArgs resultargs = new ResultArgs();
            string ProgressMessage = "Exporting CostCategory";
            SetExportStatusMessage(ProgressMessage, 1);
            try
            {
                using (TallyConnector tallyConnector = new TallyConnector())
                {
                    resultargs = GetData(SQLCommand.TallyExport.FetchCostCategory, 0, DateTime.Now, DateTime.Now);
                    if (resultargs.Success && resultargs.DataSource.Table != null)
                    {
                        DataTable dtCostCategory = resultargs.DataSource.Table;
                        SetExportStatusMessage(ProgressMessage, dtCostCategory.Rows.Count);

                        Int32 rows = 0;
                        foreach (DataRow dr in dtCostCategory.Rows)
                        {
                            string costcategoryname = dr[AppSchema.CostCentreCategory.COST_CENTRE_CATEGORY_NAMEColumn.ColumnName].ToString();

                            resultargs = tallyConnector.InsertCostCategory(costcategoryname);
                            rows++;
                            UpdateExportProgessStatus(ProgressMessage + " (" + rows.ToString() + "/" + dtCostCategory.Rows.Count + ")");
                            if (!resultargs.Success)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                resultargs.Message = "Could not export CostCenter to Tally, " + err.Message;
            }
            return resultargs;
        }

        /// <summary>
        /// Export Vouchers
        /// 1. If vouchers is already exists, ask confirmation to orverwite or append
        /// 2. if overwrite, delete all vouchers for given date range in tally
        /// 3. Fetch Vocuers master details
        /// 4. Fetch Vocuers details
        /// 5. Fetch CC Vocuers details
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="DateFrom"></param>
        /// <param name="DateTo"></param>
        /// <returns></returns>
        private ResultArgs ExportVouchers(Int32 ProjectId, DateTime DateFrom, DateTime DateTo, bool Overwrite)
        {
            string ProgressMessage = "Exporting Vouchers";
            ResultArgs resultargs = new ResultArgs();

            try
            {
                using (TallyConnector tallyConnector = new TallyConnector())
                {
                    DataTable dtCRs = tallyConnector.LedgerEntriesStructure;
                    DataTable dtDRs = tallyConnector.LedgerEntriesStructure;

                    //1. If vouchers is already exists, ask confirmation to orverwite or append
                    if (Overwrite)
                    {
                        ProgressMessage += " (Clearing Vouchers for given date range)";
                        SetExportStatusMessage(ProgressMessage, 1);
                        //2. if overwrite, delete all vouchers for given date range in tally
                        resultargs = tallyConnector.DeleteVouchers(DateFrom, DateTo);
                    }
                    else
                    {
                        SetExportStatusMessage(ProgressMessage, 1);
                        resultargs.Success = true; //Proceed if no overwrite
                    }

                    if (resultargs.Success)
                    {
                        ProgressMessage = "Exporting Vouchers";
                        //3. Fetch Vocuers master details
                        resultargs = GetData(SQLCommand.TallyExport.FetchMasterVoucher, ProjectId, DateFrom, DateTo);
                        if (resultargs.Success && resultargs.DataSource.Table != null)
                        {
                            DataTable dtMasterVouchers = resultargs.DataSource.Table;
                            SetExportStatusMessage(ProgressMessage, dtMasterVouchers.Rows.Count);

                            //4. Fetch Vocuers details
                            resultargs = GetData(SQLCommand.TallyExport.FetchVoucherDetails, ProjectId, DateFrom, DateTo);
                            if (resultargs.Success && resultargs.DataSource.Table != null)
                            {
                                DataTable dtVouchersDetails = resultargs.DataSource.Table;
                                //5. Fetch CC Vocuers details
                                resultargs = GetData(SQLCommand.TallyExport.FetchCCVoucherDetails, ProjectId, DateFrom, DateTo);
                                if (resultargs.Success && resultargs.DataSource.Table != null)
                                {
                                    DataTable dtCCVouchersDetails = resultargs.DataSource.Table;
                                    int rows = 0;
                                    foreach (DataRow dr in dtMasterVouchers.Rows)
                                    {
                                        Int32 voucherid = NumberSet.ToInteger(dr[AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString());
                                        DateTime voucherdate = DateSet.ToDate(dr[AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName].ToString(), false);
                                        string vouchertype = dr[AppSchema.VoucherMaster.VOUCHER_TYPEColumn.ColumnName].ToString();
                                        string subtype = dr[AppSchema.VoucherMaster.VOUCHER_SUB_TYPEColumn.ColumnName].ToString();
                                        string vouchernumber = dr[AppSchema.VoucherMaster.VOUCHER_NOColumn.ColumnName].ToString();
                                        string narration = dr[AppSchema.VoucherMaster.NARRATIONColumn.ColumnName].ToString();
                                        string nameaddress = dr[AppSchema.VoucherMaster.NAME_ADDRESSColumn.ColumnName].ToString();
                                        Int32 VouhcerDefinitionId = NumberSet.ToInteger(dr[AppSchema.VoucherNumber.VOUCHER_DEFINITION_IDColumn.ColumnName].ToString());
                                        
                                        //On 13/02/2019, to send multi voucher types,
                                        //MultiVoucher type will be sent only for ABE and Voucher defintion greater than 4 (Receipts/Payments/Contra/Journal)
                                        string voucherdefinitionname = string.Empty;
                                        if (VouhcerDefinitionId > 4 && IncludeMultipleVoucherTypes) //this.IS_ABEBEN_DIOCESE
                                        {
                                            voucherdefinitionname = dr[AppSchema.Voucher.VOUCHER_NAMEColumn.ColumnName].ToString();
                                        }
                                        //-------------------------------------------------------------------------------------------------------------

                                        DefaultVoucherTypes vtype = DefaultVoucherTypes.Receipt;
                                        VoucherSubTypes vsubtype = (VoucherSubTypes)EnumSet.GetEnumItemType(typeof(VoucherSubTypes), subtype);
                                        if (vouchertype == VoucherSubTypes.RC.ToString())
                                        {
                                            vtype = DefaultVoucherTypes.Receipt;
                                        }
                                        else if (vouchertype == VoucherSubTypes.PY.ToString())
                                        {
                                            vtype = DefaultVoucherTypes.Payment;
                                        }
                                        else if (vouchertype == VoucherSubTypes.CN.ToString())
                                        {
                                            vtype = DefaultVoucherTypes.Contra;
                                            //If vooucher type contra and sub type ='FD', it means FD moulde Investment, Widhtdrwal, in Tally there is no separate module, 
                                            //we will consider "investment as Payment Voucher" , "Widthdrwal as Receipt Voucher"
                                            if (vsubtype == VoucherSubTypes.FD)
                                            {
                                                dtVouchersDetails.DefaultView.RowFilter = string.Empty;
                                                dtVouchersDetails.DefaultView.RowFilter = AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName + "=" + voucherid.ToString() +
                                                                            " AND " + AppSchema.LedgerGroup.GROUP_IDColumn + "=" + (Int32)FixedLedgerGroup.FixedDeposit +
                                                                            " AND " + AppSchema.VoucherTransaction.TRANS_MODEColumn.ColumnName + "='" + TransactionMode.DR + "'";
                                                //If Investment -- Change as Payment, If Widthdrwal -- Change as Receipt
                                                vtype = (dtVouchersDetails.DefaultView.Count > 0 ? DefaultVoucherTypes.Payment : DefaultVoucherTypes.Receipt);
                                                dtVouchersDetails.DefaultView.RowFilter = string.Empty;
                                            }
                                        }
                                        else if (vouchertype == VoucherSubTypes.JN.ToString())
                                        {
                                            vtype = DefaultVoucherTypes.Journal;
                                        }

                                        //Prepare Voucher Child details
                                        dtVouchersDetails.DefaultView.RowFilter = string.Empty;
                                        dtVouchersDetails.DefaultView.RowFilter = AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName + "=" + voucherid.ToString();
                                        DataTable dtChildDetails = dtVouchersDetails.DefaultView.ToTable();

                                        //Prepare CC Voucher details
                                        dtCCVouchersDetails.DefaultView.RowFilter = string.Empty;
                                        dtCCVouchersDetails.DefaultView.RowFilter = AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName + "=" + voucherid.ToString();
                                        DataTable dtCCChildDetails = dtCCVouchersDetails.DefaultView.ToTable();

                                        // Voucher details should have at least 2 entries, if it is less than 2, it means invlid voucher
                                        if (dtChildDetails.Rows.Count > 1)
                                        {
                                            //All CRs Ledgers and its Amount
                                            dtCRs = GetLedgerEntries(dtChildDetails, dtCRs, TransSource.Cr);

                                            //All DRs Ledgers and its Amount
                                            dtDRs = GetLedgerEntries(dtChildDetails, dtDRs, TransSource.Dr);

                                            //Voucher details should have at least 1CR and 2Dr entries, otherwise it means invlid voucher
                                            if (dtCRs.Rows.Count > 0 && dtDRs.Rows.Count > 0)
                                            {
                                                resultargs = tallyConnector.InsertVoucher(voucherdate, vtype, vouchernumber, narration, nameaddress, dtCRs, dtDRs, dtCCChildDetails, voucherdefinitionname);
                                                rows++;
                                                UpdateExportProgessStatus(ProgressMessage + " (" + rows.ToString() + "/" + dtMasterVouchers.Rows.Count + ")");
                                                if (!resultargs.Success)
                                                {
                                                    break;
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
            catch (Exception err)
            {
                resultargs.Message = "Could not export vouchers to Tally, " + err.Message;
            }
            return resultargs;
        }

        /// <summary>
        /// This method is used to prpeare ledgers of CRs and DRs 
        /// </summary>
        /// <param name="dtVoucherTransaction"></param>
        /// <param name="dtLedgerEntries"></param>
        /// <param name="transsource"></param>
        /// <returns></returns>
        private DataTable GetLedgerEntries(DataTable dtVoucherTransaction, DataTable dtLedgerEntries, TransSource transsource)
        {
            dtLedgerEntries.Clear();
            dtVoucherTransaction.DefaultView.RowFilter = string.Empty;
            if (transsource == TransSource.Cr)
            {
                dtVoucherTransaction.DefaultView.RowFilter = AppSchema.VoucherTransaction.TRANS_MODEColumn.ColumnName + "= '" + TransSource.Cr.ToString() + "'";
            }
            else
            {
                dtVoucherTransaction.DefaultView.RowFilter = AppSchema.VoucherTransaction.TRANS_MODEColumn.ColumnName + "= '" + TransSource.Dr.ToString() + "'";
            }

            foreach (DataRowView drv in dtVoucherTransaction.DefaultView)
            {
                DataRow dr = dtLedgerEntries.NewRow();
                dr[AppSchema.Ledger.LEDGER_IDColumn.ColumnName] = NumberSet.ToInteger(drv[AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString());
                dr[AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName] = drv[AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                dr[AppSchema.LedgerGroup.LEDGER_GROUPColumn.ColumnName] = drv[AppSchema.LedgerGroup.LEDGER_GROUPColumn.ColumnName].ToString();
                dr[AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName] = drv[AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString();
                dr[AppSchema.VoucherTransaction.CHEQUE_NOColumn.ColumnName] = drv[AppSchema.VoucherTransaction.CHEQUE_NOColumn.ColumnName].ToString();
                dr[AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName] = drv[AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName].ToString();
                dr[AppSchema.VoucherTransaction.CHEQUE_REF_DATEColumn.ColumnName] = drv[AppSchema.VoucherTransaction.CHEQUE_REF_DATEColumn.ColumnName].ToString();
                dr[AppSchema.VoucherTransaction.CHEQUE_REF_BANKNAMEColumn.ColumnName] = drv[AppSchema.VoucherTransaction.CHEQUE_REF_BANKNAMEColumn.ColumnName].ToString();
                dr[AppSchema.VoucherTransaction.CHEQUE_REF_BRANCHColumn.ColumnName] = drv[AppSchema.VoucherTransaction.CHEQUE_REF_BRANCHColumn.ColumnName].ToString();
                dr[AppSchema.VoucherTransaction.SEQUENCE_NOColumn.ColumnName] = drv[AppSchema.VoucherTransaction.SEQUENCE_NOColumn.ColumnName].ToString();
                dtLedgerEntries.Rows.Add(dr);
            }
            return dtLedgerEntries;
        }

        /// <summary>
        /// This method is used to get data from DB based on the command
        /// </summary>
        /// <param name="sqlmode"></param>
        /// <param name="ProjectId"></param>
        /// <param name="DateFrom"></param>
        /// <param name="DateTo"></param>
        /// <returns></returns>
        private ResultArgs GetData(SQLCommand.TallyExport sqlmode, Int32 ProjectId, DateTime DateFrom, DateTime DateTo)
        {
            ResultArgs resultargs = new ResultArgs();
            try
            {
                switch (sqlmode)
                {
                    case SQLCommand.TallyExport.FetchVoucherType:
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.TallyExport.FetchVoucherType))
                            {
                                resultargs = dataManager.FetchData(DataSource.DataTable);
                            }
                            break;
                        }
                    case SQLCommand.TallyExport.FetchLedgerGroup:
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.TallyExport.FetchLedgerGroup))
                            {
                                resultargs = dataManager.FetchData(DataSource.DataTable);
                            }
                            break;
                        }
                    case SQLCommand.TallyExport.FetchLedger:
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.TallyExport.FetchLedger))
                            {
                                dataManager.Parameters.Add(AppSchema.LedgerBalance.PROJECT_IDColumn, ProjectId);
                                dataManager.Parameters.Add(AppSchema.LedgerBalance.BALANCE_DATEColumn, DateFrom);
                                resultargs = dataManager.FetchData(DataSource.DataTable);
                            }
                            break;
                        }
                    case SQLCommand.TallyExport.FetchCostCenter:
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.TallyExport.FetchCostCenter))
                            {
                                dataManager.Parameters.Add(AppSchema.LedgerBalance.PROJECT_IDColumn, ProjectId);
                                resultargs = dataManager.FetchData(DataSource.DataTable);
                            }
                            break;
                        }
                    case SQLCommand.TallyExport.FetchCostCategory:
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.TallyExport.FetchCostCategory))
                            {
                                resultargs = dataManager.FetchData(DataSource.DataTable);
                            }
                            break;
                        }
                    case SQLCommand.TallyExport.FetchMasterVoucher:
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.TallyExport.FetchMasterVoucher))
                            {
                                dataManager.Parameters.Add(AppSchema.LedgerBalance.PROJECT_IDColumn, ProjectId);
                                dataManager.Parameters.Add(AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                                dataManager.Parameters.Add(AppSchema.ExportVouchers.DATE_TOColumn, DateTo);
                                resultargs = dataManager.FetchData(DataSource.DataTable);
                            }
                            break;
                        }
                    case SQLCommand.TallyExport.FetchVoucherDetails:
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.TallyExport.FetchVoucherDetails))
                            {
                                dataManager.Parameters.Add(AppSchema.LedgerBalance.PROJECT_IDColumn, ProjectId);
                                dataManager.Parameters.Add(AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                                dataManager.Parameters.Add(AppSchema.ExportVouchers.DATE_TOColumn, DateTo);
                                resultargs = dataManager.FetchData(DataSource.DataTable);
                            }
                            break;
                        }
                    case SQLCommand.TallyExport.FetchCCVoucherDetails:
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.TallyExport.FetchCCVoucherDetails))
                            {
                                dataManager.Parameters.Add(AppSchema.LedgerBalance.PROJECT_IDColumn, ProjectId);
                                dataManager.Parameters.Add(AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                                dataManager.Parameters.Add(AppSchema.ExportVouchers.DATE_TOColumn, DateTo);
                                resultargs = dataManager.FetchData(DataSource.DataTable);
                            }
                            break;
                        }
                }
            }
            catch (Exception err)
            {
                resultargs.Message = "Could not get data, " + err.Message;
            }
            return resultargs;
        }

        /// <summary>
        /// Raise Event when expot process is started
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="MaxCount"></param>
        private void SetExportStatusMessage(string Message, int MaxCount)
        {
            if (UpdateExportProgressStatusMessage != null)
            {
                ArrayList list = new ArrayList();
                list.Add(Message);
                list.Add(MaxCount);
                UpdateExportProgressStatusMessage(list, new EventArgs());
            }
        }

        /// <summary>
        /// Raise Event when export process is going on
        /// </summary>
        private void UpdateExportProgessStatus(string RecordCount)
        {
            if (IncreaseExportProgressBar != null)
            {
                IncreaseExportProgressBar(RecordCount, new EventArgs());
            }
        }
        #endregion
    }
}
