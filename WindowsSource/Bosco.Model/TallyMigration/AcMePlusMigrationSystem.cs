using System;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.DAO.Schema;
using System.Data;
using System.Data.OleDb;
using Bosco.DAO;
using AcMEDSync.Model;

namespace Bosco.Model.TallyMigration
{
    public class AcMePlusMigrationSystem : SystemBase
    {
        #region Variables
        public event EventHandler ProgressBarEvent;
        public event EventHandler InitProgressBar;
        public event EventHandler IncreaseProgressBar;
        public event EventHandler ProRefreshBalance;
        public event EventHandler MySQLExecutionCount;


        OleDbConnection OleDbConn = null;
        DataAccess OleDbAccess = new DataAccess();
        BalanceSystem balanceSystem = new BalanceSystem();
        ResultArgs resultArgs = null;
        bool ForeignMulti = false;
        int ImportedRowCount = 0;
        public int MasterCount = 0;
        public int MappingCount = 0;
        public int ProjectCount = 0;
        public int TransactionCount = 0;
        int ZeroRow = 0;
        int CashId = 1;//Cash ID is always 1
        int MasterProjectCount = 0;
        public int iMySQLExecutionCount = 0;
        #endregion

        #region Properties
        public int ProgressBarMaxCount { get; set; }
        public string MigrationStatusMessage { get; set; }
        public int TotalYearCount { get; set; }
        public int MigratedYearCount { get; set; }
        public bool DeleteUnusedLedger { get; set; }
        bool MigrateTrans = false;
        public bool MigratingTransaction
        {
            set
            {
                MigrateTrans = value;
            }
            get
            {
                return MigrateTrans;
            }
        }
        public bool TransactionEnds { get; set; }
        bool MigrationCompeleted = false;
        public bool IsMigrateionCompleted
        {
            set
            {
                MigrationCompeleted = value;
            }
            get
            {
                return MigrationCompeleted;
            }
        }
        public bool MigrateByYear { get; set; }
        public DateTime deDateFrom { get; set; }
        public DateTime deDateTo { get; set; }
        public DateTime OPDate { get; set; }
        public DateTime YearFrom { get; set; }
        private DateTime deVoucherDate { get; set; }
        public string deMaterializedOn { get; set; }
        public string ChequeNo { get; set; }
        public string Cheque_Ref_Date { get; set; }
        public string Cheque_Ref_Bank { get; set; }
        public string Cheque_Ref_BankBranch { get; set; }
        private string VoucherNo { get; set; }
        private string VoucherType { get; set; }
        private string Narration { get; set; }
        private string Narration_Individual { get; set; }
        public string TranMode { get; set; }
        public double TransAmount { get; set; }
        public int SequenceNo { get; set; }
        public int LedgerId { get; set; }
        public int VoucherId { get; set; }
        private int CreatedBy { get; set; }
        private int ModifiedBy { get; set; }
        private string CreatedByName { get; set; }
        private string ModifiedByName { get; set; }
        private int VoucherDefinitionId { get; set; }
        public int VoucherCostCentreId { get; set; }
        public string TransModeCC { get; set; }
        private int VoucherProjectId { get; set; }
        public int BankLedgerId { get; set; }
        private int AccountTypeId { get; set; }
        private int GroupId { get; set; }
        public int ProjectId { get; set; }
        private int DivisionId { get; set; }
        public double OPBankBalance { get; set; }
        private string BankAccountCode { get; set; }
        private string BankAccounts { get; set; }
        private string LedgerType { get; set; }

        public DataTable dtMappedLedgers { get; set; }


        #endregion

        #region AcMePlus const Variables

        #region Users
        const string USERID = "UserId";
        const string USERNAME = "UserName";
        const string PASSWORD = "PassWord";
        #endregion


        #endregion

        #region Constructor
        public AcMePlusMigrationSystem(MigrationType type = MigrationType.AcMePlus)
        {
            if (type == MigrationType.AcMePlus)
                this.OleDbConn = OleDbAccess.OpenOleDbConnection();
        }
        #endregion

        #region Methods

        public ResultArgs MigrateAcMePlusToAcMeERP()
        {
            //On 26/08/2021, To fix User details by dfault ---------------------------------------------
            CreatedBy = this.NumberSet.ToInteger(this.LoginUserId.ToString());
            ModifiedBy = this.NumberSet.ToInteger(this.LoginUserId.ToString());

            CreatedByName = FirstName; //LoginUserName.ToString();
            ModifiedByName = FirstName;//  LoginUserName.ToString();
            //------------------------------------------------------------------------------------------

            SetDefaultLedgers();
            MigrateAccountingYears();
            MigrateMasterBank();
            MigrateCountry();
            MigrateMasterProject();
            MigrateLedgerGroup();
            MigrateMasterLedger();
            MasterBankAccount();
            MigrateDonorAuditor();
            MigrateMasterCostCentreCategory();
            MigrateMasterCostCentre();
            ExecutiveCommittee();
            MigrateFCPurpose();
            MigrateFCPurposeOPeningBalance();
            MappingProjectLedger();
            UpdateOpeningBalance();
            MigrateJournalVouchers();
            MigrateVoucherTransaction();
            //Include FD Migration here

            UpdateVoucherDate();
            EnableCostCentreForLedger();
            //  resultArgs = RefreshBalance();
            //if (DeleteUnusedLedger)
            //    resultArgs = DeleteUnusedLedgersFromAcMeERP();

            return resultArgs;
        }

        private void SetExecutionCount()
        {
            if (MySQLExecutionCount != null)
            {
                iMySQLExecutionCount++;
                MySQLExecutionCount(this, new EventArgs());
            }
        }

        #region Clear Old Migaration
        public void RemovePriviousMigration()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.ClearData))
            {
                resultArgs = dataManager.UpdateData();
                SetExecutionCount();
            }
        }
        #endregion

        #region Set Default Ledgers
        public void SetDefaultLedgers()
        {
            try
            {
                using (DataManager dataManager = new DataManager())
                {
                    int CashLedgerId = GetDefaultCashLedgerId();
                    if (!(CashLedgerId > 0))
                    {
                        InsertDefaultCashLedger();
                    }
                    int FixedDepositeLedgerId = GetDefaultFixedDepositeLedgerId();
                    if (!(FixedDepositeLedgerId > 0))
                    {
                        InsertDefaultFixedDepositeLedger();
                    }
                    int CapitalFundLedgerId = GetDefaultCapitalFundId();
                    if (!(CapitalFundLedgerId > 0))
                    {
                        InsertDefaultCapitalFund();
                    }
                    if (!(GetDefaultStateId() > 0))
                    {
                        InsertDefultState();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
        }

        private int GetDefaultCashLedgerId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.CashDefaultLedger))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            SetExecutionCount();
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int GetDefaultStateId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.DefaultSateId))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            SetExecutionCount();
            return resultArgs.DataSource.Sclar.ToInteger;
        }


        private int GetDefaultFixedDepositeLedgerId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.FixedDepositeDefaultLedger))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            SetExecutionCount();
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        private int GetDefaultCapitalFundId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.CapitalFundDefaultLedger))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            SetExecutionCount();
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        private void InsertDefaultCashLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.InsertCashDefaultLedger))
            {
                resultArgs = dataManager.UpdateData();
                SetExecutionCount();
                if (resultArgs != null)
                    MasterCount += resultArgs.RowsAffected;
                SetExecutionCount();
            }
        }

        private void InsertDefaultFixedDepositeLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.InsertFixedDepositeDefaultLedger))
            {
                resultArgs = dataManager.UpdateData();
                SetExecutionCount();
                if (resultArgs != null)
                    MasterCount += resultArgs.RowsAffected;
                SetExecutionCount();
            }
        }

        private void InsertDefaultCapitalFund()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.InsertCapitalFundDefaultLedger))
            {
                resultArgs = dataManager.UpdateData();
                SetExecutionCount();
                if (resultArgs != null)
                    MasterCount += resultArgs.RowsAffected;
                SetExecutionCount();
            }
        }

        private void InsertDefultState()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.InsertDefaultState))
            {
                resultArgs = dataManager.UpdateData();
                SetExecutionCount();
                if (resultArgs != null)
                    MasterCount += resultArgs.RowsAffected;
                SetExecutionCount();
            }
        }
        #endregion

        #region Migrate User
        public void MigrateUsers()
        {
            DataTable dtUsers = GetUserListFromAcMePlus();
            if (dtUsers != null)
            {
                foreach (DataRow drUsers in dtUsers.Rows)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateUsers))
                    {
                        if (!NumberSet.ToInteger(drUsers[USERID].ToString()).Equals(0))
                        {
                            string UserName = drUsers[USERNAME].ToString();
                            string Password = drUsers[PASSWORD].ToString();
                            if (Password.Equals(string.Empty))
                                Password = UserName;
                            dataManager.Parameters.Add(AppSchema.User.USER_NAMEColumn, UserName);
                            dataManager.Parameters.Add(AppSchema.User.PASSWORDColumn, Password);
                            resultArgs = dataManager.UpdateData();
                            SetExecutionCount();
                            if (resultArgs != null)
                                MasterCount += resultArgs.RowsAffected;
                        }
                    }
                }
            }
        }
        #endregion

        #region Migrate Accounting Years

        private void MigrateAccountingYears()
        {
            SetProgressBar();
            DataTable dtAcYear = GetAllAccountingYearFromAcMePlus();
            int BookBeginningFrom = 0;
            if (dtAcYear != null)
            {
                SetMigrationStatusMessage("Accounting Years", dtAcYear.Rows.Count);
                foreach (DataRow drAcYears in dtAcYear.Rows)
                {
                    ChangeProgressBarStatus();
                    if (MigrateByYear)
                    {
                        int Year = DateSet.ToDate(drAcYears["DateFrom"].ToString(), false).Year;
                        if (Year >= deDateFrom.Year)
                        {
                            ImportAccountingYear(BookBeginningFrom, drAcYears);
                        }
                    }
                    else
                    {
                        ImportAccountingYear(BookBeginningFrom, drAcYears);
                    }
                }
                SetActiveAccountingYear();
            }
        }


        public bool IsAuditVouchersLocked(bool isAllYears, Int32 FYId)
        {
            bool rtn = false;
            
            try
            {
                DataTable dtFYYears = GetAllAccountingYearFromAcMePlus();
                DataTable dtProjects = GetAllProjectListFromAcMePlus();

                if (dtFYYears != null && dtProjects != null)
                {
                    if (!isAllYears)
                    {
                        dtFYYears.DefaultView.RowFilter = "ACORDER = " + FYId;
                        dtFYYears = dtFYYears.DefaultView.ToTable();
                    }

                    foreach (DataRow drFY in dtFYYears.Rows)
                    {
                        DateTime FyFrom = DateSet.ToDate(drFY["DateFrom"].ToString(), false);
                        DateTime FyTo = DateSet.ToDate(drFY["DateTo"].ToString(), false);

                        if (dtProjects != null)
                        {
                            foreach (DataRow drProject in dtProjects.Rows)
                            {
                                string acmeplusProjectName = drProject["PROJECT"].ToString().Trim();
                                string acmeerpprojectname = acmeplusProjectName + ((DivisionId == 2 && acmeplusProjectName.Trim().ToUpper() != "FOREIGN") ? " (F)" : string.Empty);
                                int DId = NumberSet.ToInteger(drProject["DIVISIONID"].ToString());

                                if (!string.IsNullOrEmpty(acmeplusProjectName))
                                {
                                    int projectid = GetProjectId(acmeplusProjectName, DId);

                                    rtn = this.IsAuditVouchersLockedVoucherDate(projectid, acmeerpprojectname, FyFrom, FyTo);
                                    if (rtn)
                                    {
                                        break;
                                    }
                                }
                            }

                            if (rtn)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
                rtn = true;
            }
            return rtn;
        }

        /// <summary>
        /// On 24/09/2021, 
        /// </summary>
        /// <returns></returns>
        private bool IsAuditVouchersLockedVoucherDate(Int32 pId, string projectname, DateTime frmDate, DateTime toDate)
        {
            bool rnt = true;
            ResultArgs result = new ResultArgs();
            AcMELog.WriteLog("Check Audit Voucher Lock Started..");
            result.Success = true;
            try
            {
                using (DataManager datamanager = new DataManager(SQLCommand.AuditLockTrans.FetchAuditLockDetailByProjectDateRange))
                {
                    datamanager.Parameters.Add(this.AppSchema.Voucher.PROJECT_IDColumn, pId);
                    datamanager.Parameters.Add(this.AppSchema.FDRegisters.DATE_FROMColumn, frmDate);
                    datamanager.Parameters.Add(this.AppSchema.FDRegisters.DATE_TOColumn, toDate);
                    result = datamanager.FetchData(DataSource.DataTable);

                    if (result != null && result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                    {
                        string lockedmessage = "Unable to Delete/Modify Vouchers." + System.Environment.NewLine + "Voucher is locked for '" + projectname + "'" +
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
        /// <summary>
        /// On 24/02/2021, To check Acmeerp FY and Acmeplus FY (Apr - Mar and Jan-Dec)
        /// If both differs, prompt message and alert the message
        /// </summary>
        /// <returns></returns>
        public bool CheckValidFYPeriod()
        {
            bool rtn = false;
            try
            {
                DataTable dtAmcePlusAcYear = GetAllAccountingYearFromAcMePlus();
                if (dtAmcePlusAcYear != null && dtAmcePlusAcYear.Rows.Count > 0)
                {
                    int month = DateSet.ToDate(dtAmcePlusAcYear.Rows[0]["DateFrom"].ToString(), false).Month;
                    rtn = (month == DateSet.ToDate(base.YearFrom, false).Month);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            return rtn;
        }

        private int ImportAccountingYear(int BookBeginningFrom, DataRow dr)
        {
            int Status = 0;
            string DateFrom = dr["DateFrom"].ToString();
            string DateTo = dr["DateTo"].ToString();
            DateFrom = DateFrom.Equals(string.Empty) ? "0001-01-01" : DateFrom;
            DateTo = DateTo.Equals(string.Empty) ? "0001-01-01" : DateTo;

            int AcYear = FindAccountingYear(DateFrom, DateTo);
            if (!(AcYear > 0))
            {
                using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateAcYears))
                {
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, DateTo);
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.BOOKS_BEGINNING_FROMColumn, BookBeginningFrom.Equals(0) ? DateFrom : "0001-01-01");
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.STATUSColumn, Status);
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.IS_FIRST_ACCOUNTING_YEARColumn, Status.Equals(1) ? 1 : 0);
                    resultArgs = dataManager.UpdateData();
                    SetExecutionCount();
                    if (resultArgs != null)
                        MasterCount += resultArgs.RowsAffected;
                }
            }
            return BookBeginningFrom;
        }

        public int FindAccountingYear(string DateFrom, string DateTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.FindAccountingYear))
            {
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, DateFrom);
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, DateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                SetExecutionCount();
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int FindAccountingYearForTally(string DateFrom, string DateTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.FindAccountingYearForTALLY))
            {
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, DateFrom);
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, DateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                SetExecutionCount();
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        private void SetActiveAccountingYear()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.SetActiveAccountingYear))
            {
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.ACC_YEAR_IDColumn, GetFirstAccountingYearId());
                resultArgs = dataManager.UpdateData();
                SetExecutionCount();
                if (resultArgs != null)
                    MasterCount += resultArgs.RowsAffected;
            }
        }

        private void SetActiveBookBeginningFromYear()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.SetActiveAccountingYear))
            {
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.ACC_YEAR_IDColumn, GetFirstAccountingYearId());
                resultArgs = dataManager.UpdateData();
                SetExecutionCount();
                if (resultArgs != null)
                    MasterCount += resultArgs.RowsAffected;
            }
        }

        public int GetFirstAccountingYearId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GetActiveAccountingYearId))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            SetExecutionCount();
            return resultArgs.DataSource.Sclar.ToInteger;
        }



        private DateTime GetLeastAccountingDate()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GetLeastAccountingDate))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            SetExecutionCount();
            return DateSet.ToDate(resultArgs.DataSource.Sclar.ToString, false);
        }

        #endregion

        #region Migrate Master Bank

        private void MigrateMasterBank()
        {
            SetProgressBar();
            DataTable dtMasterBank = GetMasterBankFromAcMePlus();
            if (dtMasterBank != null)
            {
                SetMigrationStatusMessage("Master Bank", dtMasterBank.Rows.Count);
                foreach (DataRow drMasterBank in dtMasterBank.Rows)
                {
                    ChangeProgressBarStatus();
                    if (!NumberSet.ToInteger(drMasterBank["BankId"].ToString()).Equals(0))
                    {
                        string Bank = drMasterBank["Bank"].ToString();
                        string Branch = drMasterBank["Place"].ToString();
                        if (FindBankId(Bank, Branch) == 0)
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateMasterBank))
                            {
                                string Code = drMasterBank["Abbreviation"].ToString();
                                string BankAddress = drMasterBank["Address"].ToString();

                                string BKCode = string.Empty;
                                //string BCode = GetBankCode(Code);
                                //if (!string.IsNullOrEmpty(BCode))
                                //    Code = String.Format("{0}-{1}", Code, GenerateBankCode()); //Generating new Bank Code;

                                dataManager.Parameters.Add(AppSchema.Bank.BANK_CODEColumn, Code);
                                dataManager.Parameters.Add(AppSchema.Bank.BANKColumn, Bank);
                                dataManager.Parameters.Add(AppSchema.Bank.BRANCHColumn, Branch);
                                dataManager.Parameters.Add(AppSchema.Bank.ADDRESSColumn, BankAddress);
                                resultArgs = dataManager.UpdateData();
                                SetExecutionCount();
                                if (resultArgs != null)
                                    MasterCount += resultArgs.RowsAffected;
                            }
                        }
                    }
                }
            }
        }

        public int FindBankId(string BankName, string Branch)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GetBankId))
            {
                dataManager.Parameters.Add(AppSchema.Bank.BANKColumn, BankName);
                dataManager.Parameters.Add(AppSchema.Bank.BRANCHColumn, Branch);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                SetExecutionCount();
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public string GetBankCode(string Code)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GetBankCode))
            {
                dataManager.Parameters.Add(AppSchema.Bank.BANK_CODEColumn, Code);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                SetExecutionCount();
            }
            return resultArgs.DataSource.Sclar.ToString;
        }

        public int GenerateBankCode()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GenerateBankCode))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                SetExecutionCount();
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        #endregion

        #region Migrate Country
        private void MigrateCountry()
        {
            SetProgressBar();
            DataTable dtCountry = GetCountryListFromAcMePlus();
            if (dtCountry != null)
            {
                SetMigrationStatusMessage("Master Country", dtCountry.Rows.Count);
                foreach (DataRow drCountry in dtCountry.Rows)
                {
                    ChangeProgressBarStatus();
                    using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateCountry))
                    {
                        if (!NumberSet.ToInteger(drCountry["CountryId"].ToString()).Equals(0))
                        {
                            string CountryName = drCountry["Country"].ToString();
                            int CountryId = GetCountryId(CountryName);
                            if (CountryId == 0)
                            {
                                string CountryCode = GenerateCountryCode(); //Generating new Country Code
                                string CurrencySymbol = drCountry["CurrencySymbol"].ToString();
                                dataManager.Parameters.Add(AppSchema.Country.COUNTRYColumn, CountryName);
                                dataManager.Parameters.Add(AppSchema.Country.COUNTRY_CODEColumn, CountryCode);
                                dataManager.Parameters.Add(AppSchema.Country.CURRENCY_SYMBOLColumn, CurrencySymbol);
                                resultArgs = dataManager.UpdateData();
                                SetExecutionCount();
                                if (resultArgs != null)
                                    MasterCount += resultArgs.RowsAffected;
                            }
                        }
                    }
                }
            }
        }

        public int GetCountryId(string CountryName)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GetCountryId))
            {
                dataManager.Parameters.Add(AppSchema.Country.COUNTRYColumn, CountryName);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                SetExecutionCount();
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        private string GenerateCountryCode()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GenereateCountryCode))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                SetExecutionCount();
            }
            string NewCode = String.Format("{0}", resultArgs.DataSource.Sclar.ToString);
            return NewCode;
        }

        public int GetCountryCount()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GenereateCountryCode))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        private int GetStateId(string StateName)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GetStateId))
            {
                dataManager.Parameters.Add(AppSchema.State.STATE_NAMEColumn, StateName);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                SetExecutionCount();
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        #endregion

        #region Migrate Project
        private void MigrateMasterProject()
        {
            SetProgressBar();
            DataTable dtProject = GetAllProjectListFromAcMePlus();
            if (dtProject != null)
            {
                SetMigrationStatusMessage("Master Project", dtProject.Rows.Count);
                ProjectCount = GetProjectCount();
                foreach (DataRow drProject in dtProject.Rows)
                {
                    ChangeProgressBarStatus();
                    using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateProject))
                    {
                        if (NumberSet.ToInteger(drProject["PROJECTid"].ToString()) > 0)
                        {
                            int CategoryId = GetProjectCategoryId();
                            if (!(CategoryId > 0))
                                CategoryId = MigrateDefaultProjectCategory();
                            DateTime AcDate = DateSet.ToDate(drProject["ACDATE"].ToString(), false);
                            DateTime DateStarted = string.IsNullOrEmpty(drProject["DATESTARTED"].ToString()) ? GetLeastAccountingDate() : DateSet.ToDate(drProject["DATESTARTED"].ToString(), false);
                            DateTime? DateClosed = string.IsNullOrEmpty(drProject["DATECLOSED"].ToString()) ? null : (DateTime?)DateSet.ToDate(drProject["DATECLOSED"].ToString(), false);

                            int DivisionId = NumberSet.ToInteger(drProject["DIVISIONID"].ToString());

                            if (DivisionId == 0)
                            {
                                DivisionId = 1;//If DivisionId is 0 in Acmeplus, this is invalid project, so we fix dvision as local (1)
                            }

                            string ProjectName = drProject["PROJECT"].ToString();
                            string ProjectCode = drProject["ABBREVIATION"].ToString();
                            string Description = drProject["DESCRIPTION"].ToString();
                            //On 29/06/2017, Other then "Foreign" project, if division =2, change project name as "PROJECT NAME (F)";
                            ProjectName = ProjectName + ((DivisionId == 2 && ProjectName.Trim().ToUpper() != "FOREIGN") ? " (F)" : string.Empty);
                            ProjectId = GetProjectId(ProjectName);

                            if (!(ProjectId > 0))
                            {
                                dataManager.Parameters.Add(AppSchema.Project.PROJECT_CODEColumn, ProjectCode, true);

                                dataManager.Parameters.Add(AppSchema.Project.PROJECTColumn, ProjectName);
                                dataManager.Parameters.Add(AppSchema.Project.DIVISION_IDColumn, DivisionId);
                                dataManager.Parameters.Add(AppSchema.Project.ACCOUNT_DATEColumn, AcDate);
                                dataManager.Parameters.Add(AppSchema.Project.DATE_STARTEDColumn, DateStarted);
                                dataManager.Parameters.Add(AppSchema.Project.DATE_CLOSEDColumn, DateClosed);
                                dataManager.Parameters.Add(AppSchema.Project.DESCRIPTIONColumn, Description);
                                dataManager.Parameters.Add(AppSchema.Project.PROJECT_CATEGORY_IDColumn, CategoryId);
                                resultArgs = dataManager.UpdateData();
                                SetExecutionCount();
                                if (resultArgs != null)
                                {
                                    MasterProjectCount += resultArgs.RowsAffected;
                                    MasterCount += resultArgs.RowsAffected;
                                    ProjectId = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                    MapVouchers(NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()));
                                }
                            }
                        }
                    }
                }
            }
        }

        public int GetProjectCategoryId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GetProjetCategoryId))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                SetExecutionCount();
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int MigrateDefaultProjectCategory()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateProjectCategory))
            {
                string DefaultCategory = "Primary";
                dataManager.Parameters.Add(AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn, DefaultCategory, true);
                resultArgs = dataManager.UpdateData();
                SetExecutionCount();
            }
            return NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
        }

        public int GetProjectId(string ProjectName, int DivisionId)
        {
            //On 29/06/2017, Other then "Foreign" project, if division =2, change project name as "PROJECT NAME (F)";
            ProjectName = ProjectName + ((DivisionId == 2 && ProjectName.Trim().ToUpper() != "FOREIGN") ? " (F)" : string.Empty);
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GetProjectId))
            {
                dataManager.Parameters.Add(AppSchema.Project.PROJECTColumn, ProjectName);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                SetExecutionCount();
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int GetProjectId(string ProjectName)
        {
            //(Its for Acmeplus), On 29/06/2017, Other then "Foreign" project, if division =2, change project name as "PROJECT NAME (F)";
            return GetProjectId(ProjectName, 1);
        }

        public string GetProjectCode(string Code)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GetProjectCode))
            {
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_CODEColumn, Code);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                SetExecutionCount();
            }
            return resultArgs.DataSource.Sclar.ToString;
        }

        public int GetProjectCount()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GenerateProjectCode))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                SetExecutionCount();
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        private string GenerateNewProject()
        {
            string NewProject = String.Format("PRO({0})", ProjectCount);
            return NewProject;
        }

        private void MapVouchers(int ProjectId)
        {
            DataTable dtVoucher = GetAllMasterVouchers();
            if (dtVoucher != null)
            {
                foreach (DataRow drVoucher in dtVoucher.Rows)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MapProjectVoucher))
                    {
                        dataManager.Parameters.Add(AppSchema.Voucher.VOUCHER_IDColumn, drVoucher["VOUCHER_ID"].ToString());
                        dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                        resultArgs = dataManager.UpdateData();
                        SetExecutionCount();
                        if (resultArgs != null)
                            MappingCount += resultArgs.RowsAffected;
                    }
                }
            }
        }

        private DataTable GetAllMasterVouchers()
        {
            DataTable dtVouchers = null;
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GetAllMasterVouchers))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                SetExecutionCount();
                if (resultArgs != null)
                {
                    dtVouchers = resultArgs.DataSource.Table;
                }
            }
            return dtVouchers;
        }
        #endregion

        #region Migrate Ledger Group
        private void MigrateLedgerGroup()
        {
            SetProgressBar();
            DataTable dtLedgerGroup = GetMasterLedgerGroupFromAcMePlus();
            if (dtLedgerGroup != null)
            {
                SetMigrationStatusMessage("Master Ledger Group", dtLedgerGroup.Rows.Count);
                foreach (DataRow drLedgerGroup in dtLedgerGroup.Rows)
                {
                    ChangeProgressBarStatus();
                    int GroupId = NumberSet.ToInteger(drLedgerGroup["GroupId"].ToString());
                    string LedgerGroup = drLedgerGroup["Group"].ToString();
                    //----------------------GroupId =4 =>Liabilities
                    //----------------------GroupId =13 =>Cash-in-hand
                    //----------------------GroupId =14 =>Deposits (Asset)
                    //----------------------GroupId =21 =>Capital Fund
                    if (!(GroupId == 0 || GroupId == 4 || GroupId == 13 || (GroupId == 14 && LedgerGroup == "Fixed Deposits") || GroupId == 21))
                    {
                        //Check for the Group name and get group id if exist
                        int LedgerGroupId = GetLedgerGroupId(LedgerGroup);
                        if (LedgerGroupId == 0)
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateLedgerGroup))
                            {
                                string GroupCode = drLedgerGroup["Abbreviation"].ToString();
                                // string Code = GetLedgerGroupCode(GroupCode);
                                int ParentGroupId = NumberSet.ToInteger(drLedgerGroup["ParentGroupId"].ToString());
                                int NatureId = NumberSet.ToInteger(drLedgerGroup["NatureId"].ToString());
                                int MainGroupId = NumberSet.ToInteger(drLedgerGroup["MainGroupId"].ToString());

                                //if (!string.IsNullOrEmpty(Code))
                                //    GroupCode = String.Format("{0}-{1}", GroupCode, GenerateLedgerGroupCode()); //Generating new Ledger Group Code
                                int AccessFlag = 0;
                                if (LedgerGroupId == 1 || LedgerGroupId == 2 || LedgerGroupId == 3 || LedgerGroupId == 11 || LedgerGroupId == 12)
                                    AccessFlag = 2;

                                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                                dataManager.Parameters.Add(AppSchema.LedgerGroup.GROUP_CODEColumn, GroupCode);
                                dataManager.Parameters.Add(AppSchema.LedgerGroup.LEDGER_GROUPColumn, LedgerGroup);
                                dataManager.Parameters.Add(AppSchema.LedgerGroup.PARENT_GROUP_IDColumn, ParentGroupId);
                                dataManager.Parameters.Add(AppSchema.LedgerGroup.NATURE_IDColumn, NatureId);
                                dataManager.Parameters.Add(AppSchema.LedgerGroup.MAIN_GROUP_IDColumn, MainGroupId);
                                dataManager.Parameters.Add(AppSchema.LedgerGroup.ACCESS_FLAGColumn, AccessFlag);
                                SetExecutionCount();
                                resultArgs = dataManager.UpdateData();
                                if (resultArgs != null)
                                    MasterCount += resultArgs.RowsAffected;
                            }
                        }
                    }
                }


            }
        }

        public int GetLedgerGroupId(string LedgeGroup)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GetLedgerGroupId))
            {
                dataManager.Parameters.Add(AppSchema.LedgerGroup.LEDGER_GROUPColumn, LedgeGroup);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                SetExecutionCount();
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int GetGroupNatureId(string LedgeGroup)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GetNatureId))
            {
                dataManager.Parameters.Add(AppSchema.LedgerGroup.LEDGER_GROUPColumn, LedgeGroup);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                SetExecutionCount();
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        //public string GetLedgerGroupCode(string GroupCode)
        //{
        //    using (DataManager dataManger = new DataManager(SQLCommand.AcMePlusMigration.GetLedgerGroupCode))
        //    {
        //        dataManger.Parameters.Add(AppSchema.LedgerGroup.GROUP_CODEColumn, GroupCode);
        //        resultArgs = dataManger.FetchData(DataSource.Scalar);
        //        SetExecutionCount();
        //    }
        //    return resultArgs.DataSource.Sclar.ToString;
        //}

        public int GenerateLedgerGroupCode()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GenerateGroupCode))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                SetExecutionCount();
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        #endregion

        #region Migrate Master Ledger

        private void MigrateMasterLedger()
        {
            SetProgressBar();
            // DataTable dtLedgers = GetAllLedgersFromAcMePlus(); //dtMappedLedgers
            DataTable dtLedgers = dtMappedLedgers;
            if (dtLedgers != null)
            {
                SetMigrationStatusMessage("Master Ledger", dtLedgers.Rows.Count);
                foreach (DataRow drLedgers in dtLedgers.Rows)
                {
                    ChangeProgressBarStatus();
                    int LedgerId = NumberSet.ToInteger(drLedgers["LedgerId"].ToString());
                    string LedgerName = drLedgers["LedgerName"].ToString();
                    //----------------------LedgerId =1 =>Cash Deposit----------------------------------------------
                    //----------------------LedgerId =2 =>Cash Withdrawal-------------------------------------------
                    //----------------------LedgerId =3 =>Capital Fund----------------------------------------------
                    //----------------------LedgerId =5 =>Fixed Deposit Invested------------------------------------
                    //----------------------LedgerId =6 =>Fixed Deposit Realised------------------------------------
                    //----------------------LedgerId =8 =>A/c Transfer----------------------------------------------

                    if (LedgerId != 0 && LedgerId != 1 && LedgerId != 2 && LedgerId != 3 && LedgerId != 5 && LedgerId != 6 && LedgerId != 8)
                    {
                        if (IsMappingLedgerValidation(drLedgers)) //if No mapping is done for the ledger migrate the ledger if not skip the ledger
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateLedger))
                            {
                                int ExistingLedgerId = GetLedgerId(LedgerName);

                                if (!(ExistingLedgerId > 0))
                                {
                                    int GroupId = GetLedgerGroupId(GetGroupIdFromAcMePlus(NumberSet.ToInteger(drLedgers["GROUPID"].ToString())));

                                    //On 15/07/2017, In Acmeplus some users, they created ledgers in Cash-In-Hand, Bank Accounts and Fixed Deposits Group
                                    //When we migrate from Acmeplyus, those ledgers affects in Cash, Bank and FD balance
                                    //so if any ledgers and its group falls in (12 Bank, 13 cash, 14 -FD), we fixed those ledges's group as "Direct Incomes" 
                                    //If "Direct Incomes"  is not avilable in Acme.erp, fixed as Incomes ledgers
                                    if (GroupId == 12 || GroupId == 13 || GroupId == 14)
                                    {
                                        GroupId = GetLedgerGroupId("Direct Incomes");
                                        if (GroupId == 0)
                                        {
                                            GroupId = (int)Natures.Income;
                                        }
                                    }
                                    string LedgerType = NumberSet.ToInteger(drLedgers["IKHEAD"].ToString()).Equals(0) ? "GN" : "IK";
                                    string LedgerCode = drLedgers["LedgerCode"].ToString();
                                    dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_CODEColumn, LedgerCode);
                                    dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_NAMEColumn, LedgerName);
                                    dataManager.Parameters.Add(AppSchema.Ledger.GROUP_IDColumn, GroupId);
                                    dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_TYPEColumn, "GN");
                                    dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_SUB_TYPEColumn, LedgerType);
                                    resultArgs = dataManager.UpdateData();
                                    SetExecutionCount();
                                    if (resultArgs != null && resultArgs.Success)
                                        MasterCount += resultArgs.RowsAffected;
                                    else
                                    { 

                                    }

                                }
                            }
                        }
                    }
                }
                MapAndUpdateBalaneCash();
            }
        }

        private bool IsMappingLedgerValidation(DataRow drLedgers)
        {
            bool IsValid = false;
            if (dtMappedLedgers.Columns.Contains("Ledger_Id"))
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

        public int GetLedgerId(string Ledger)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GetLedgerId))
            {
                dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_NAMEColumn, Ledger);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                SetExecutionCount();
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        //public string GetLedgerCode(string Code)
        //{
        //    using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GetLedgerCode))
        //    {
        //        dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_CODEColumn, Code);
        //        resultArgs = dataManager.FetchData(DataSource.Scalar);
        //        SetExecutionCount();
        //    }
        //    return resultArgs.DataSource.Sclar.ToString;
        //}

        public int GenerateLedgerCode()
        {
            using (DataManager dataManger = new DataManager(SQLCommand.AcMePlusMigration.GenerateLedgerCode))
            {
                resultArgs = dataManger.FetchData(DataSource.Scalar);
                SetExecutionCount();
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        #endregion

        #region Mapping and Updating OpeningBalance
        private void MappingProjectLedger()
        {
            SetProgressBar();
            DataTable dtProjectLedgerMapping = GetProjectLedgerMappingFromAcMePlus();
            if (dtProjectLedgerMapping != null)
            {
                SetMigrationStatusMessage("Mapping Project Ledger", dtProjectLedgerMapping.Rows.Count, true);
                foreach (DataRow drProjectLedger in dtProjectLedgerMapping.Rows)
                {
                    ChangeProgressBarStatus();
                    int LedgerId = NumberSet.ToInteger(drProjectLedger["HEADID"].ToString());
                    //----------------------LedgerId =1 =>Cash Deposit----------------------------------------------
                    //----------------------LedgerId =2 =>Cash Withdrawal-------------------------------------------
                    //----------------------LedgerId =5 =>Fixed Deposit Invested------------------------------------
                    //----------------------LedgerId =6 =>Fixed Deposit Realised------------------------------------
                    //----------------------LedgerId =8 =>A/c Transfer----------------------------------------------
                    if (LedgerId != 1 && LedgerId != 2 && LedgerId != 5 && LedgerId != 6 && LedgerId != 8)
                    {
                        if (!NumberSet.ToInteger(drProjectLedger["PROJECTID"].ToString()).Equals(0))
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectLedgerMappingAdd))
                            {
                                string ProjectName = drProjectLedger["PROJECT"].ToString();
                                //if (NumberSet.ToInteger(drProjectLedger["DIVISIONID"].ToString()) == 2)
                                //    ProjectName = ProjectName + "(F)";
                                int ProjectId = GetProjectId(ProjectName, NumberSet.ToInteger(drProjectLedger["DIVISIONID"].ToString()));
                                //int ProjectId = GetProjectId(ProjectName + "-" + GenerateNewProject());
                                if (!(ProjectId > 0))
                                    ProjectId = GetProjectId(ProjectName, NumberSet.ToInteger(drProjectLedger["DIVISIONID"].ToString()));

                                // int NewLedgerId = GetLedgerId(GetLedgerNameByIdFromAcMePlus(LedgerId));
                                int NewLedgerId = GetMappedLedgerId(LedgerId);

                                if (IsProjectLedgerNotMapped(ProjectId, NewLedgerId))
                                {
                                    dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_IDColumn, NewLedgerId);
                                    dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                                    resultArgs = dataManager.UpdateData();
                                    SetExecutionCount();
                                    if (resultArgs != null && resultArgs.Success)
                                    {
                                        MappingCount += resultArgs.RowsAffected;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public int GetMappedLedgerId(int SourceLedgerId)
        {
            int LedgerId = 0;
            if (dtMappedLedgers != null)
            {
                DataView dvLedgerId = new DataView(dtMappedLedgers);
                dvLedgerId.RowFilter = String.Format("LedgerId={0}", SourceLedgerId);
                DataTable dtLedger = dvLedgerId.ToTable();
                if (dtLedger.Columns.Contains("LEDGER_ID"))
                {
                    if (dtLedger != null && dtLedger.Rows.Count > 0)
                    {
                        LedgerId = NumberSet.ToInteger(dtLedger.Rows[0]["LEDGER_ID"].ToString()) > 0 ? NumberSet.ToInteger(dtLedger.Rows[0]["LEDGER_ID"].ToString()) :
                            GetLedgerId(dtLedger.Rows[0]["LEDGERNAME"].ToString());
                    }
                }
                else
                {
                    LedgerId = GetLedgerId(dtLedger.Rows[0]["LEDGERNAME"].ToString());
                }
            }
            else
            {
                //Get AcMePlus Ledger name by its Id when mapping is not done
                LedgerId = GetLedgerId(GetLedgerNameByIdFromAcMePlus(SourceLedgerId));
            }
            return LedgerId;
        }

        private bool IsProjectLedgerNotMapped(int ProjectId, int LedgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.IsProjectLedgerMapped))
            {
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger == 0;
        }

        private bool IsProjectDonorNotMapped(int ProjectId, int DonorId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.IsProjectDonorMapped))
            {
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(AppSchema.DonorAuditor.DONAUD_IDColumn, DonorId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger == 0;
        }

        private bool IsProjectPurposeNotMapped(int ProjectId, int PurposeId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.IsProjectPurposeMapped))
            {
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(AppSchema.DonorAuditor.DONAUD_IDColumn, PurposeId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger == 0;
        }

        private void UpdateOpeningBalance()
        {
            SetProgressBar();
            DataTable dtOPBalance = GetOpeningBalanceFromAcMePlus();
            if (dtOPBalance != null)
            {
                SetMigrationStatusMessage("Updating Opening Balance", dtOPBalance.Rows.Count, true);
                foreach (DataRow drOPBalance in dtOPBalance.Rows)
                {
                    ChangeProgressBarStatus();
                    int AcMeProjectId = NumberSet.ToInteger(drOPBalance["PROJECTID"].ToString());
                    double OPBalance = NumberSet.ToInteger(drOPBalance["OPBALANCE"].ToString());
                    if (AcMeProjectId > 0 && OPBalance != 0)
                    {
                        OPBankBalance = OPBalance;
                        OPDate = GetLeastAccountingDate().AddDays(-1);
                        int DivisionId = NumberSet.ToInteger(drOPBalance["DIVISIONID"].ToString());
                        string ProjectName = GetProjectNameByIdDivisionFromAcMePlus(AcMeProjectId, DivisionId);
                        int ProId = GetProjectId(ProjectName, DivisionId);
                        if (!(ProId > 0))
                            ProjectId = GetProjectId(ProjectName, DivisionId);
                        else
                            ProjectId = ProId;
                        // BankLedgerId = GetLedgerId(GetLedgerNameByIdFromAcMePlus(NumberSet.ToInteger(drOPBalance["HEADID"].ToString())));
                        BankLedgerId = GetMappedLedgerId(NumberSet.ToInteger(drOPBalance["HEADID"].ToString()));
                        UpdateBankOpeningBalance();
                    }
                }
            }
        }

        private void MapAndUpdateBalaneCash()
        {
            SetProgressBar();
            DataTable dtProjectCash = GetProjectListForMapping();
            if (dtProjectCash != null)
            {
                SetMigrationStatusMessage("Updating Cash Openning Balance", dtProjectCash.Rows.Count);
                foreach (DataRow drProjectCash in dtProjectCash.Rows)
                {
                    ChangeProgressBarStatus();
                    int ProjectId = NumberSet.ToInteger(drProjectCash["PROJECT_ID"].ToString());
                    using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectLedgerMappingAdd))
                    {
                        dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_IDColumn, CashId);
                        dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                        resultArgs = dataManager.UpdateData();
                        SetExecutionCount();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            MappingCount += resultArgs.RowsAffected;
                        }
                    }
                }
                if (MigrateByYear)
                {
                    UpdateCashOpeningBalanceByPeriod();
                }
                else
                {
                    //Updating OP balance for cash (Project)
                    DataTable dtOledDbProjectIdCollection = GetMappingFromAcMeplus();
                    if (dtOledDbProjectIdCollection != null)
                    {
                        foreach (DataRow drCashOPBalance in dtOledDbProjectIdCollection.Rows)
                        {
                            int ProjectId = NumberSet.ToInteger(drCashOPBalance["PROJECTID"].ToString());
                            if (ProjectId > 0)
                            {
                                int DivId = NumberSet.ToInteger(drCashOPBalance["DivisionId"].ToString());

                                DataTable dtMappingData = GetOpBanalceProjectFromAcMePlus();
                                DateTime BalDate = GetLeastAccountingDate().AddDays(-1);
                                if (BalDate == null)
                                    BalDate = DateSet.ToDate(dtOledDbProjectIdCollection.Rows[0][0].ToString(), false).AddDays(-1);
                                if (dtMappingData != null)
                                {
                                    DataTable dtOPBalance = GetOPBalanceByProjectDivision(ProjectId, DivId);
                                    if (dtOPBalance != null)
                                    {
                                        foreach (DataRow drOpBalance in dtOPBalance.Rows)
                                        {
                                            double Amount = NumberSet.ToDouble(drOpBalance["OPBALANCE"].ToString());
                                            if (Amount != 0)
                                            {
                                                using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.UpdateOpBalance))
                                                {
                                                    string ValueField = string.Empty;
                                                    string CashOpQuery = string.Empty;
                                                    string ProjectName = string.Empty;
                                                    string TransMode = Amount > 0 ? "DR" : "CR";
                                                    double Balance = Amount > 0 ? Amount : Math.Abs(Amount);
                                                    ProjectName = drOpBalance["PROJECT"].ToString();

                                                    int ProId = GetProjectId(ProjectName, DivId);
                                                    //On 10/10/2018, to update same op balance
                                                    /*dataManager.Parameters.Add(AppSchema.LedgerBalance.BALANCE_DATEColumn, BalDate);
                                                    dataManager.Parameters.Add(AppSchema.LedgerBalance.PROJECT_IDColumn, ProId);
                                                    dataManager.Parameters.Add(AppSchema.LedgerBalance.LEDGER_IDColumn, CashId);
                                                    dataManager.Parameters.Add(AppSchema.LedgerBalance.AMOUNTColumn, Amount);
                                                    dataManager.Parameters.Add(AppSchema.LedgerBalance.TRANS_MODEColumn, TransMode);
                                                    resultArgs = dataManager.UpdateData();*/
                                                    CheckAndUpdateLedgerOpeningBalance(BalDate, ProId, CashId, MigrationType.AcMePlus);
                                                    SetExecutionCount();
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

        private DataTable GetProjectListForMapping()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MapProjectCash))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                SetExecutionCount();
            }
            return resultArgs.DataSource.Table;
        }

        private void UpdateCashOpeningBalanceByPeriod()
        {
            DataTable dtProject = GetMappingProjectByPeriodFromAcMePlus();
            if (dtProject != null)
            {
                foreach (DataRow drProject in dtProject.Rows)
                {
                    DataManager dataManager;
                    int ProId = NumberSet.ToInteger(drProject["PROJECTID"].ToString());
                    if (ProId > 0)
                    {
                        int ProjectDivisionId = NumberSet.ToInteger(drProject["DIVISIONID"].ToString());
                        object oDate = GetCashOpBalanceDate(ProId, ProjectDivisionId);
                        {
                            DateTime deDate = DateSet.ToDate(oDate.ToString(), false);
                            DataTable dtCashOpeningBalance = GetCashOpeningBalance(deDate, ProId, ProjectDivisionId);
                            if (dtCashOpeningBalance != null && dtCashOpeningBalance.Rows.Count > 0)
                            {
                                string ValueField = string.Empty;
                                string CashOpQuery = string.Empty;
                                string ProjectName = string.Empty;
                                DateTime BalanceDate = GetLeastAccountingDate().AddDays(-1);
                                double Amount = NumberSet.ToDouble(dtCashOpeningBalance.Rows[0]["AMOUNT"].ToString());
                                if (Amount != 0)
                                {
                                    string TransMode = Amount > 0 ? "DR" : "CR";
                                    double Balance = Amount > 0 ? Amount : Math.Abs(Amount);
                                    string AcMePlusProjectName = ProjectNameByIdFromAcMePlus(ProId);

                                    ProjectName = AcMePlusProjectName;

                                    ProjectId = GetProjectId(ProjectName, ProjectDivisionId);
                                    BankLedgerId = CashId;
                                    OPDate = BalanceDate;
                                    OPBankBalance = Amount;
                                    UpdateBankOpeningBalance();

                                    //double AvaiAmount = IsBalanceAvailable(BalanceDate, ProjectId, CashId);
                                    //if (AvaiAmount == 0)
                                    //{
                                    //    dataManager = new DataManager(SQLCommand.AcMePlusMigration.UpdateOpBalance);
                                    //}
                                    //else
                                    //{
                                    //    Amount += AvaiAmount; //Sum the previous Cash Op balance with Project
                                    //    dataManager = new DataManager(SQLCommand.AcMePlusMigration.SumUpdateOPBalance);
                                    //}

                                    //dataManager.Parameters.Add(AppSchema.LedgerBalance.BALANCE_DATEColumn, BalanceDate);
                                    //dataManager.Parameters.Add(AppSchema.LedgerBalance.PROJECT_IDColumn, ProjectId);
                                    //dataManager.Parameters.Add(AppSchema.LedgerBalance.LEDGER_IDColumn, CashId);
                                    //dataManager.Parameters.Add(AppSchema.LedgerBalance.AMOUNTColumn, Amount);
                                    //dataManager.Parameters.Add(AppSchema.LedgerBalance.TRANS_MODEColumn, TransMode);
                                    //resultArgs = dataManager.UpdateData();
                                    //SetExecutionCount();
                                }
                            }
                        }
                    }
                }
            }
        }

        private double GetOPBalance(DateTime BalanceDate, int ProjectId, int LedgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.CheckOPBalUpdate))
            {
                dataManager.Parameters.Add(AppSchema.LedgerBalance.BALANCE_DATEColumn, BalanceDate);
                dataManager.Parameters.Add(AppSchema.LedgerBalance.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(AppSchema.LedgerBalance.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Data == null ? 0 : NumberSet.ToDouble(resultArgs.DataSource.Data.ToString());
        }

        private void UpdateBankOpeningBalanceByPeriod()
        {
            DataTable BankProject = GetBankAccountMappedProject();
            if (BankProject != null)
            {
                string BankOpDate = GetBankOpeningBalanceDate();
                int DivId = 0;
                DataTable dtBankOPBalance;
                foreach (DataRow drBankProject in BankProject.Rows)
                {
                    int AcMePlusProjectId = NumberSet.ToInteger(drBankProject["PROJECTID"].ToString());
                    if (AcMePlusProjectId > 0)
                    {
                        DivId = NumberSet.ToInteger(drBankProject["DIVISIONID"].ToString());
                        int AccountId = NumberSet.ToInteger(drBankProject["ACCOUNTID"].ToString());
                        dtBankOPBalance = GetBankOpeningBalanceFromAcMePlus(AcMePlusProjectId, DivId, AccountId, BankOpDate);

                        if (dtBankOPBalance != null && dtBankOPBalance.Rows.Count > 0)
                        {
                            OPBankBalance = NumberSet.ToDouble(dtBankOPBalance.Rows[0]["AMOUNT"].ToString());
                            if (OPBankBalance != 0)
                            {
                                BankLedgerId = GetBankLedgerId(String.Format("{0}-{1}", dtBankOPBalance.Rows[0]["BANKACCOUNT"].ToString(), dtBankOPBalance.Rows[0]["ABBREVIATION"].ToString()));
                                string ProjectName = GetProjectNameFromAcMePlus(AcMePlusProjectId);
                                //if (DivId == 2)
                                //{
                                //    ProjectName = String.Format("{0}(F)-{1}", ProjectName, GenerateNewProject());
                                //    ProjectName = ProjectName + "(F)";
                                //}

                                //Updating Bank Opening Balance
                                ProjectId = GetProjectId(ProjectName, DivId);
                                OPDate = GetLeastAccountingDate().AddDays(-1);
                                UpdateBankOpeningBalance();
                            }
                        }
                    }
                }

            }
        }

        public void UpdateBankOpeningBalance(MigrationType migrationType = MigrationType.AcMePlus)
        {
            DataManager dataManager;
            //On 10/10/2018, to update same op balance
            if (migrationType == MigrationType.AcMePlus)
            {
                OPDate = GetLeastAccountingDate().AddDays(-1);
            }

            double AvaiAmount = GetOPBalance(OPDate, ProjectId, BankLedgerId);
            bool updateop = true;

            using (DataManager dataManager1 = new DataManager(SQLCommand.AcMePlusMigration.CheckOPBalUpdate))
            {
                dataManager1.Parameters.Add(AppSchema.LedgerBalance.BALANCE_DATEColumn, OPDate);
                dataManager1.Parameters.Add(AppSchema.LedgerBalance.PROJECT_IDColumn, ProjectId);
                dataManager1.Parameters.Add(AppSchema.LedgerBalance.LEDGER_IDColumn, BankLedgerId);
                resultArgs = dataManager1.FetchData(DataSource.DataTable);
            }

            if (resultArgs.Success == true && resultArgs.DataSource.Table != null)
            {
                if (resultArgs.DataSource.Table.Rows.Count == 0)
                {
                    updateop = false;
                }
            }

            if (updateop == false)
            {
                dataManager = new DataManager(SQLCommand.AcMePlusMigration.UpdateOpBalance);
            }
            else
            {
                double Amount = Math.Abs(OPBankBalance);
                Amount += AvaiAmount; //Sum the previous Cash Op balance with Project
                dataManager = new DataManager(SQLCommand.AcMePlusMigration.SumUpdateOPBalance);
            }

            dataManager.Parameters.Add(AppSchema.LedgerBalance.BALANCE_DATEColumn, OPDate);
            dataManager.Parameters.Add(AppSchema.LedgerBalance.PROJECT_IDColumn, ProjectId);
            dataManager.Parameters.Add(AppSchema.LedgerBalance.LEDGER_IDColumn, BankLedgerId);
            dataManager.Parameters.Add(AppSchema.LedgerBalance.AMOUNTColumn, Math.Abs(OPBankBalance));
            if (migrationType == MigrationType.AcMePlus)
                dataManager.Parameters.Add(AppSchema.LedgerBalance.TRANS_MODEColumn, OPBankBalance > 0 ? "DR" : "CR");
            else
            {
                //In Tally Migration All Negative Amount should be 'DR'
                //And Positive Amount should be 'CR'
                dataManager.Parameters.Add(AppSchema.LedgerBalance.TRANS_MODEColumn, OPBankBalance > 0 ? "CR" : "DR");
            }
            resultArgs = dataManager.UpdateData();
            SetExecutionCount();

            //using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.UpdateOpBalance))
            //{
            //    dataManager.Parameters.Add(AppSchema.LedgerBalance.BALANCE_DATEColumn, OPDate);
            //    dataManager.Parameters.Add(AppSchema.LedgerBalance.PROJECT_IDColumn, ProjectId);
            //    dataManager.Parameters.Add(AppSchema.LedgerBalance.LEDGER_IDColumn, BankLedgerId);
            //    dataManager.Parameters.Add(AppSchema.LedgerBalance.AMOUNTColumn, Math.Abs(OPBankBalance));
            //    dataManager.Parameters.Add(AppSchema.LedgerBalance.TRANS_MODEColumn, OPBankBalance > 0 ? "DR" : "CR");
            //    resultArgs = dataManager.UpdateData();
            //    SetExecutionCount();
            //}
        }

        public void CheckAndUpdateLedgerOpeningBalance(DateTime opdate, Int32 projectid, Int32 ledgerid, MigrationType migrationType = MigrationType.AcMePlus)
        {
            DataManager dataManager;
            double AvaiAmount = GetOPBalance(opdate, projectid, ledgerid);
            bool updateop = true;

            using (DataManager dataManager1 = new DataManager(SQLCommand.AcMePlusMigration.CheckOPBalUpdate))
            {
                dataManager1.Parameters.Add(AppSchema.LedgerBalance.BALANCE_DATEColumn, opdate);
                dataManager1.Parameters.Add(AppSchema.LedgerBalance.PROJECT_IDColumn, projectid);
                dataManager1.Parameters.Add(AppSchema.LedgerBalance.LEDGER_IDColumn, ledgerid);
                resultArgs = dataManager1.FetchData(DataSource.DataTable);
            }

            if (resultArgs.Success == true && resultArgs.DataSource.Table != null)
            {
                if (resultArgs.DataSource.Table.Rows.Count == 0)
                {
                    updateop = false;
                }
            }

            if (updateop == false)
            {
                dataManager = new DataManager(SQLCommand.AcMePlusMigration.UpdateOpBalance);
            }
            else
            {
                double Amount = Math.Abs(OPBankBalance);
                Amount += AvaiAmount; //Sum the previous Cash Op balance with Project
                dataManager = new DataManager(SQLCommand.AcMePlusMigration.SumUpdateOPBalance);
            }

            dataManager.Parameters.Add(AppSchema.LedgerBalance.BALANCE_DATEColumn, opdate);
            dataManager.Parameters.Add(AppSchema.LedgerBalance.PROJECT_IDColumn, projectid);
            dataManager.Parameters.Add(AppSchema.LedgerBalance.LEDGER_IDColumn, ledgerid);
            dataManager.Parameters.Add(AppSchema.LedgerBalance.AMOUNTColumn, Math.Abs(OPBankBalance));
            if (migrationType == MigrationType.AcMePlus)
                dataManager.Parameters.Add(AppSchema.LedgerBalance.TRANS_MODEColumn, OPBankBalance > 0 ? "DR" : "CR");
            else
            {
                //In Tally Migration All Negative Amount should be 'DR'
                //And Positive Amount should be 'CR'
                dataManager.Parameters.Add(AppSchema.LedgerBalance.TRANS_MODEColumn, OPBankBalance > 0 ? "CR" : "DR");
            }
            resultArgs = dataManager.UpdateData();

        }

        #endregion

        #region Migrate Master Bank Account
        private void MasterBankAccount()
        {
            SetProgressBar();
            DataTable dtBankAccounts = GetAllBankAccountsFromAcMePlus();
            if (dtBankAccounts != null)
            {
                SetMigrationStatusMessage("Master Bank Account", dtBankAccounts.Rows.Count);
                foreach (DataRow drBankAccounts in dtBankAccounts.Rows)
                {
                    ChangeProgressBarStatus();
                    int AccountId = NumberSet.ToInteger(drBankAccounts["ACCOUNTID"].ToString());
                    if (AccountId > 0)
                    {
                        string NewBank = string.Empty;
                        bool IsFD = Convert.ToBoolean(drBankAccounts["FDAC"]);
                        LedgerType = IsFD ? "FD" : "BK";
                        if (LedgerType != "FD")
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateBankAccount))
                            {
                                BankAccounts = drBankAccounts["BANKACCOUNT"].ToString();
                                BankAccountCode = drBankAccounts["ABBREVIATION"].ToString();
                                ProjectId = NumberSet.ToInteger(drBankAccounts["PROJECTID"].ToString());
                                DivisionId = NumberSet.ToInteger(drBankAccounts["DIVISIONID"].ToString());
                                OPBankBalance = NumberSet.ToDouble(drBankAccounts["OPBALANCE"].ToString());
                                OPDate = DateSet.ToDate(drBankAccounts["ACDATE"].ToString(), false).AddDays(-1);

                                GroupId = LedgerType.Equals("BK") ? 12 : 14;                        //12 -Bank Accounts, 14 - Deposits (Asset)
                                bool IsFDBank = Convert.ToBoolean(drBankAccounts["FDAC"]);
                                AccountTypeId = IsFDBank ? 2 : 1;                                    //ACCOUNT_TYPE_ID=2 Fixed Deposit,ACCOUNT_TYPE_ID=1 Saving Account
                                //Check for the Bank Account Number and get Account Number id if exist
                                string BankAccountNo = GetBankAccountNo(BankAccounts + "-" + BankAccountCode);
                                if (string.IsNullOrEmpty(BankAccountNo))
                                {
                                    //string ExistingBankAccountCode = GetBankAccountCode(BankAccountCode);
                                    //string GeneratedBankAccountCode = string.Empty;
                                    //if (string.IsNullOrEmpty(ExistingBankAccountCode))
                                    //    GeneratedBankAccountCode = String.Format("{0}-{1}", BankAccountCode, GenerateBankAccountCode()); //Generating new Bank Account Code

                                    int BankLedgerId = MigrateMasterBankAccountAsLedger(drBankAccounts, BankAccountCode);

                                    int BankId = GetBankId(NumberSet.ToInteger(drBankAccounts["BANKID"].ToString()));
                                    DateTime? deDateOpened = string.IsNullOrEmpty(drBankAccounts["DATEOPENED"].ToString()) ? GetLeastAccountingDate() : (DateTime?)DateSet.ToDate(drBankAccounts["DATEOPENED"].ToString(), false);
                                    DateTime? deDateClosed = string.IsNullOrEmpty(drBankAccounts["DATECLOSED"].ToString()) ? null : (DateTime?)DateSet.ToDate(drBankAccounts["DATECLOSED"].ToString(), false);

                                    dataManager.Parameters.Add(AppSchema.BankAccount.LEDGER_IDColumn, BankLedgerId);
                                    dataManager.Parameters.Add(AppSchema.BankAccount.ACCOUNT_CODEColumn, BankAccountCode);
                                    dataManager.Parameters.Add(AppSchema.BankAccount.ACCOUNT_NUMBERColumn, String.Format("{0}-{1}", BankAccounts, drBankAccounts["ABBREVIATION"].ToString()));
                                    dataManager.Parameters.Add(AppSchema.BankAccount.BANK_IDColumn, BankId);
                                    dataManager.Parameters.Add(AppSchema.BankAccount.DATE_OPENEDColumn, deDateOpened);
                                    dataManager.Parameters.Add(AppSchema.BankAccount.DATE_CLOSEDColumn, deDateClosed);
                                    dataManager.Parameters.Add(AppSchema.BankAccount.ACCOUNT_TYPE_IDColumn, AccountTypeId);
                                    resultArgs = dataManager.UpdateData();
                                    SetExecutionCount();
                                    if (resultArgs != null)
                                        MasterCount += resultArgs.RowsAffected;

                                }
                            }
                        }
                    }
                }
                if (MigrateByYear)
                {
                    UpdateBankOpeningBalanceByPeriod();
                }
            }
        }

        private string GetBankAccountNo(string BankName)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GetBankAccountNo))
            {
                dataManager.Parameters.Add(AppSchema.BankAccount.ACCOUNT_NUMBERColumn, BankName);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                SetExecutionCount();
            }
            return resultArgs.DataSource.Sclar.ToString;
        }

        //public string GetBankAccountCode(string Code)
        //{
        //    using (DataManager dataManger = new DataManager(SQLCommand.AcMePlusMigration.GetBankAccountCode))
        //    {
        //        dataManger.Parameters.Add(AppSchema.BankAccount.ACCOUNT_CODEColumn, Code);
        //        resultArgs = dataManger.FetchData(DataSource.Scalar);
        //        SetExecutionCount();
        //    }
        //    return resultArgs.DataSource.Sclar.ToString;
        //}

        private int MigrateMasterBankAccountAsLedger(DataRow drBankAccountLedger, string GeneratedBankAccountCode)
        {
            BankLedgerId = 0;
            int LedgerId = 0;
            string LedgerName = drBankAccountLedger.ItemArray[5].ToString() + "-" + drBankAccountLedger.ItemArray[4].ToString();
            if (!string.IsNullOrEmpty(LedgerName))
            {
                LedgerId = GetLedgerId(LedgerName);
                if (LedgerId > 0)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectLedgerMappingAdd))
                    {
                        //For Mapping
                        BankLedgerId = LedgerId;
                        string ProjectName = GetProjectNameByIdDivisionFromAcMePlus(ProjectId, DivisionId);
                        ProjectId = GetProjectId(ProjectName, DivisionId);
                        dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_IDColumn, BankLedgerId);
                        dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                        resultArgs = dataManager.UpdateData();
                        SetExecutionCount();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            MappingCount += resultArgs.RowsAffected;
                        }
                    }
                }
                else
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateLedger))
                    {
                        dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_CODEColumn, GeneratedBankAccountCode + LedgerType, true);
                        dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_NAMEColumn, BankAccounts + "-" + GeneratedBankAccountCode);
                        dataManager.Parameters.Add(AppSchema.Ledger.GROUP_IDColumn, GroupId);
                        dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_TYPEColumn, "GN");
                        dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_SUB_TYPEColumn, LedgerType);
                        resultArgs = dataManager.UpdateData();
                        SetExecutionCount();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            MasterCount += resultArgs.RowsAffected;
                            BankLedgerId = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                        }
                    }
                }
            }
            else
            {
                LedgerId = GetLedgerId(LedgerName);
                if (LedgerId > 0)
                {
                    BankLedgerId = LedgerId;
                }
                else
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateLedger))
                    {
                        dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_CODEColumn, BankAccountCode + LedgerType, true);
                        dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_NAMEColumn, BankAccounts + "-" + BankAccountCode);
                        dataManager.Parameters.Add(AppSchema.Ledger.GROUP_IDColumn, GroupId);
                        dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_TYPEColumn, "GN");
                        dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_SUB_TYPEColumn, LedgerType);
                        resultArgs = dataManager.UpdateData();
                        SetExecutionCount();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            MasterCount += resultArgs.RowsAffected;
                            BankLedgerId = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                        }
                    }
                }
            }
            if (BankLedgerId > 0)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectLedgerMappingAdd))
                {
                    //For Mapping
                    string ProjectName = GetProjectNameByIdDivisionFromAcMePlus(ProjectId, DivisionId);
                    ProjectId = GetProjectId(ProjectName, DivisionId);
                    dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_IDColumn, BankLedgerId);
                    dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    resultArgs = dataManager.UpdateData();
                    SetExecutionCount();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        MappingCount += resultArgs.RowsAffected;
                    }

                    if (!MigrateByYear) //Update bank openingbalance when migration type is all
                    {
                        UpdateBankOpeningBalance();
                    }
                }
            }
            return BankLedgerId;
        }

        private int GetBankId(int AcMePlusBankId)
        {
            int NewId = -1;
            DataTable dtBank = GetMasterBankByIdFromAcMePlus(AcMePlusBankId);
            if (dtBank != null)
            {
                foreach (DataRow drBank in dtBank.Rows)
                {
                    string BankName = drBank["BANK"].ToString();
                    string BranchName = drBank["PLACE"].ToString();
                    if (!BankName.Trim().Equals(string.Empty))
                    {
                        NewId = FindBankId(BankName, BranchName);
                    }
                }
            }
            return NewId;
        }

        //private string GenerateBankAccountCode()
        //{
        //    using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GenerateBankAccountCode))
        //    {
        //        resultArgs = dataManager.FetchData(DataSource.Scalar);
        //    }
        //    SetExecutionCount();
        //    return resultArgs.DataSource.Sclar.ToString;
        //}

        public int GenerateBankAccountCount()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GenerateBankAccountCode))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            SetExecutionCount();
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        private int GetBankLedgerId(int BankAccountId)
        {
            int BankLedgerId = -1;
            DataTable dtBankAccount = GetBankAccontsInfoFromAcMePlus(BankAccountId);
            if (dtBankAccount != null)
            {
                foreach (DataRow drBankAccount in dtBankAccount.Rows)
                {
                    BankLedgerId = GetBankLedgerId(drBankAccount["BANKACCOUNT"].ToString() + "-" + drBankAccount["ABBREVIATION"].ToString());
                }
            }
            return BankLedgerId;
        }

        private int GetBankLedgerId(string AccountNo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GetBankAccountLedgerId))
            {
                dataManager.Parameters.Add(AppSchema.BankAccount.ACCOUNT_NUMBERColumn, AccountNo);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                SetExecutionCount();
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        #endregion

        #region Migrate Donor Auditor
        private void MigrateDonorAuditor()
        {
            SetProgressBar();
            DataTable dtDonorAuditor = GetDonorAuditorDetailFromAcMePlus();
            if (dtDonorAuditor != null)
            {
                SetMigrationStatusMessage("Donor Auditor Info", dtDonorAuditor.Rows.Count);
                foreach (DataRow drDonorAuditor in dtDonorAuditor.Rows)
                {
                    ChangeProgressBarStatus();
                    int DonAudId = NumberSet.ToInteger(drDonorAuditor["DonAudId"].ToString());
                    string DonorName = !string.IsNullOrEmpty(drDonorAuditor["Donor"].ToString()) ? drDonorAuditor["Donor"].ToString() : "India";
                    int CountryId = NumberSet.ToInteger(drDonorAuditor["CountryId"].ToString());
                    if (DonAudId > 0)
                    {
                        if (!(GetDonorAuditorId(DonorName, CountryId) > 0))
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateDonorAuditor))
                            {
                                CountryId = GetCountryIdFromAcMePlus(CountryId);
                                int DonorType = drDonorAuditor["Type"].ToString().ToUpper().Equals("I") ? 0 : 1;
                                int IdentiyKey = drDonorAuditor["IdentiyKey"].ToString().ToUpper().Equals("D") ? 0 : 1;
                                string Place = drDonorAuditor["Place"].ToString();
                                int StateId = GetStateId(drDonorAuditor["State"].ToString());
                                string Address = drDonorAuditor["Address"].ToString();
                                string PinCode = drDonorAuditor["PinCode"].ToString();
                                string Phone = drDonorAuditor["Phone"].ToString();
                                string Fax = drDonorAuditor["Fax"].ToString();
                                string Email = drDonorAuditor["Email"].ToString();
                                string URL = drDonorAuditor["URL"].ToString();
                                int FcDonor = NumberSet.ToInteger(drDonorAuditor["FcDonor"].ToString());

                                dataManager.Parameters.Add(AppSchema.DonorAuditor.NAMEColumn, DonorName);
                                dataManager.Parameters.Add(AppSchema.DonorAuditor.TYPEColumn, DonorType);
                                dataManager.Parameters.Add(AppSchema.DonorAuditor.PLACEColumn, Place);
                                dataManager.Parameters.Add(AppSchema.State.STATE_IDColumn, StateId == 0 ? GetDefaultStateId() : StateId); //Default State Id (Tamil Nadu)
                                dataManager.Parameters.Add(AppSchema.DonorAuditor.COUNTRY_IDColumn, CountryId);
                                dataManager.Parameters.Add(AppSchema.DonorAuditor.ADDRESSColumn, Address);
                                dataManager.Parameters.Add(AppSchema.DonorAuditor.PINCODEColumn, PinCode);
                                dataManager.Parameters.Add(AppSchema.DonorAuditor.PHONEColumn, Phone);
                                dataManager.Parameters.Add(AppSchema.DonorAuditor.FAXColumn, Fax);
                                dataManager.Parameters.Add(AppSchema.DonorAuditor.EMAILColumn, Email);
                                dataManager.Parameters.Add(AppSchema.DonorAuditor.IDENTITYKEYColumn, IdentiyKey);
                                dataManager.Parameters.Add(AppSchema.DonorAuditor.URLColumn, URL);
                                dataManager.Parameters.Add(AppSchema.DonorAuditor.AUDIT_TYPE_IDColumn, FcDonor);
                                resultArgs = dataManager.UpdateData();
                                SetExecutionCount();
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    MasterCount += resultArgs.RowsAffected;
                                }
                            }
                        }
                    }
                }
            }
        }

        private int GetDonorAuditorId(string DonorAudName, int CountryId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GetDonorAuditorId))
            {
                dataManager.Parameters.Add(AppSchema.DonorAuditor.NAMEColumn, DonorAudName);
                dataManager.Parameters.Add(AppSchema.DonorAuditor.COUNTRY_IDColumn, Country);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                SetExecutionCount();
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        private int GetDonorAuditorId(int AcMePlusDonorId)
        {
            int NewDonAudId = -1;
            DataTable dtDonAud = GetDonorIdFromAcMeplus(AcMePlusDonorId);
            if (dtDonAud != null)
            {
                if (dtDonAud != null && dtDonAud.Rows.Count > 0)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.NewDonorId))
                    {
                        dataManager.Parameters.Add(AppSchema.DonorAuditor.NAMEColumn, dtDonAud.Rows[0]["Donor"].ToString());
                        dataManager.Parameters.Add(AppSchema.DonorAuditor.PLACEColumn, dtDonAud.Rows[0]["Place"].ToString());
                        resultArgs = dataManager.FetchData(DataSource.Scalar);
                        SetExecutionCount();
                        NewDonAudId = resultArgs.DataSource.Sclar.ToInteger;
                    }
                    //NewDonAudId = NumberSet.ToInteger(OleDbAccess.ExecuteScalarValue(String.Format("SELECT DONAUD_ID FROM master_donaud WHERE NAME=\"{0}\"AND PLACE=\"{1}\"",
                    //    dtDonAud.Rows[0]["Donor"].ToString(), dtDonAud.Rows[0]["Place"].ToString())).ToString());
                }
            }
            return NewDonAudId;
        }
        #endregion

        #region Migrate Master Cost Centre and Cost Centre Category
        private void MigrateMasterCostCentre()
        {
            SetProgressBar();
            DataTable dtCostCentre = GetCostCentreFromAcMePlus();
            if (dtCostCentre != null)
            {
                SetMigrationStatusMessage("Maste Cost Centre", dtCostCentre.Rows.Count);
                foreach (DataRow drCostCentre in dtCostCentre.Rows)
                {
                    ChangeProgressBarStatus();
                    int CostCentreId = NumberSet.ToInteger(drCostCentre["COSTCENTREID"].ToString());
                    if (CostCentreId > 0)
                    {
                        string CostCentreName = drCostCentre["CostCentre"].ToString();
                        CostCentreId = GetCostCentreId(CostCentreName);
                        if (!(CostCentreId > 0))
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateCostCentre))
                            {
                                string CostCenteCode = drCostCentre["ABBREVIATION"].ToString();
                                dataManager.Parameters.Add(AppSchema.CostCentre.ABBREVATIONColumn, CostCenteCode, true);
                                dataManager.Parameters.Add(AppSchema.CostCentre.COST_CENTRE_NAMEColumn, CostCentreName);
                                resultArgs = dataManager.UpdateData();

                                SetExecutionCount();
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    MasterCount += resultArgs.RowsAffected;
                                    int AcMeERPCostCentreId = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                    int CostCategoryId = GetDefaultCostCategoryid();
                                    MapCostCentreCategory(AcMeERPCostCentreId, CostCategoryId);
                                }
                            }
                        }
                    }
                }
            }
        }

        public int GetCostCentreId(string CostCentreName)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GetCostCentreId))
            {
                dataManager.Parameters.Add(AppSchema.CostCentre.COST_CENTRE_NAMEColumn, CostCentreName);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                SetExecutionCount();
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public string GetCostCentreCode(string Code)
        {
            using (DataManager dataManger = new DataManager(SQLCommand.AcMePlusMigration.GetCostCentreCode))
            {
                dataManger.Parameters.Add(AppSchema.CostCentre.ABBREVATIONColumn, Code);
                resultArgs = dataManger.FetchData(DataSource.Scalar);
                SetExecutionCount();
            }
            return resultArgs.DataSource.Sclar.ToString;
        }

        public int GenerateCostCentreCode()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GenerateCostCentreCode))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                SetExecutionCount();
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        private void MigrateMasterCostCentreCategory()
        {
            if (GetDefaultCostCategoryid() == 0)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.AddDefaultCostCategory))
                {
                    resultArgs = dataManager.UpdateData();
                }
            }
        }

        private int GetDefaultCostCategoryid()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.ICostCategoryExists))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        private void MapCostCentreCategory(int CostCentreId, int CostCentreCategoryId)
        {
            if (IsCostCategoryNotMapped(CostCentreId, CostCentreCategoryId))
            {
                using (DataManager dataManger = new DataManager(SQLCommand.AcMePlusMigration.MapCostCentreCategory))
                {
                    dataManger.Parameters.Add(AppSchema.CostCentreCategory.COST_CENTRECATEGORY_IDColumn, CostCentreCategoryId);
                    dataManger.Parameters.Add(AppSchema.CostCentre.COST_CENTRE_IDColumn, CostCentreId);
                    resultArgs = dataManger.UpdateData();
                }
            }
        }

        private bool IsCostCategoryNotMapped(int CostCentreId, int CostCentreCategoryId)
        {
            using (DataManager dataManagerMap = new DataManager(SQLCommand.AcMePlusMigration.IsCostCategoryMapped))
            {
                dataManagerMap.Parameters.Add(AppSchema.CostCentreCategory.COST_CENTRECATEGORY_IDColumn, CostCentreCategoryId);
                dataManagerMap.Parameters.Add(AppSchema.CostCentre.COST_CENTRE_IDColumn, CostCentreId);
                resultArgs = dataManagerMap.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger == 0;
        }
        #endregion

        #region Migrate Master Executive Committee
        private void ExecutiveCommittee()
        {
            SetProgressBar();
            DataTable dtExecutiveComittee = GetExecutiveCommitteeFromAcMePlus();
            if (dtExecutiveComittee != null)
            {
                SetMigrationStatusMessage("Executive Members", dtExecutiveComittee.Rows.Count);
                foreach (DataRow drExecutiveComitte in dtExecutiveComittee.Rows)
                {
                    ChangeProgressBarStatus();
                    if (NumberSet.ToInteger(drExecutiveComitte["ExecutiveId"].ToString()) > 0)
                    {
                        string Executive = drExecutiveComitte["Executive"].ToString();
                        if (!(GetExecutiveCommitteeId(Executive, NumberSet.ToInteger(drExecutiveComitte["CountryId"].ToString())) > 0))
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateExecutiveCommittee))
                            {
                                int StateId = GetStateId(drExecutiveComitte["State"].ToString());
                                StateId = StateId == 0 ? GetDefaultStateId() : StateId;
                                dataManager.Parameters.Add(AppSchema.ExecutiveMembers.EXECUTIVEColumn, drExecutiveComitte["Executive"].ToString());
                                dataManager.Parameters.Add(AppSchema.ExecutiveMembers.NAMEColumn, drExecutiveComitte["Name"].ToString());
                                dataManager.Parameters.Add(AppSchema.ExecutiveMembers.NATIONALITYColumn, drExecutiveComitte["Nationality"].ToString());
                                dataManager.Parameters.Add(AppSchema.ExecutiveMembers.OCCUPATIONColumn, drExecutiveComitte["Occupation"].ToString());
                                dataManager.Parameters.Add(AppSchema.ExecutiveMembers.ASSOCIATIONColumn, drExecutiveComitte["Association"].ToString());
                                dataManager.Parameters.Add(AppSchema.ExecutiveMembers.OFFICE_BEARERColumn, drExecutiveComitte["OfficeBarrier"].ToString());
                                dataManager.Parameters.Add(AppSchema.ExecutiveMembers.PLACEColumn, drExecutiveComitte["Place"].ToString());
                                dataManager.Parameters.Add(AppSchema.ExecutiveMembers.STATE_IDColumn, StateId);
                                dataManager.Parameters.Add(AppSchema.ExecutiveMembers.COUNTRY_IDColumn, GetCountryIdFromAcMePlus(NumberSet.ToInteger(drExecutiveComitte["CountryId"].ToString())));
                                dataManager.Parameters.Add(AppSchema.ExecutiveMembers.PIN_CODEColumn, drExecutiveComitte["PinCode"].ToString());
                                dataManager.Parameters.Add(AppSchema.ExecutiveMembers.PHONEColumn, drExecutiveComitte["Phone"].ToString());
                                dataManager.Parameters.Add(AppSchema.ExecutiveMembers.FAXColumn, drExecutiveComitte["Fax"].ToString());
                                dataManager.Parameters.Add(AppSchema.ExecutiveMembers.EMAILColumn, drExecutiveComitte["Email"].ToString());
                                dataManager.Parameters.Add(AppSchema.ExecutiveMembers.URLColumn, drExecutiveComitte["URL"].ToString());
                                resultArgs = dataManager.UpdateData();
                                SetExecutionCount();
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    MasterCount += resultArgs.RowsAffected;
                                }
                            }
                        }

                    }
                }
            }
        }

        private int GetExecutiveCommitteeId(string ExecutiveName, int CountryId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GetExecutiveCommitteeId))
            {
                dataManager.Parameters.Add(AppSchema.ExecutiveMembers.NAMEColumn, ExecutiveName);
                dataManager.Parameters.Add(AppSchema.ExecutiveMembers.COUNTRY_IDColumn, CountryId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                SetExecutionCount();
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        #endregion

        #region Migrate Fc Purpose
        private void MigrateFCPurpose()
        {
            SetProgressBar();
            DataTable dtFCPurpose = GetFCPurposeFromAcMePlus();
            if (dtFCPurpose != null)
            {
                SetMigrationStatusMessage("Master FC Purpose", dtFCPurpose.Rows.Count);
                foreach (DataRow drFCPurpose in dtFCPurpose.Rows)
                {
                    ChangeProgressBarStatus();
                    int ConHeadId = NumberSet.ToInteger(drFCPurpose["ConHeadId"].ToString());
                    if (ConHeadId > 0)
                    {
                        int PurposeId = GetPurposeIdByPurpose(drFCPurpose["Head"].ToString());
                        if (!(PurposeId > 0))
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateFCPurpose))
                            {
                                string Code = String.Format("P{0}", GeneratePurposeCode()); //Generating new FC Code
                                //dataManager.Parameters.Add(AppSchema.Purposes.CONTRIBUTION_IDColumn, PurposeId);
                                dataManager.Parameters.Add(AppSchema.Purposes.CODEColumn, Code);
                                dataManager.Parameters.Add(AppSchema.Purposes.FC_PURPOSEColumn, drFCPurpose["Head"].ToString());
                                resultArgs = dataManager.UpdateData();
                                SetExecutionCount();
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    MasterCount += resultArgs.RowsAffected;
                                }
                            }

                        }
                    }
                }
            }
        }


        private DateTime GetFirstMigrationYear()
        {
            DateTime dtPrDateFrom = new DateTime();
            DataTable dtAcYear = GetAllAccountingYearFromAcMePlus();
            if (dtAcYear != null)
            {
                foreach (DataRow drAcYears in dtAcYear.Rows)
                {
                    if (MigrateByYear)
                    {
                        int Year = DateSet.ToDate(drAcYears["DateFrom"].ToString(), false).Year;
                        if (Year >= deDateFrom.Year)
                        {
                            dtPrDateFrom = DateSet.ToDate(drAcYears["DateFrom"].ToString(), false);
                            break;
                        }
                    }
                    else
                    {
                        dtPrDateFrom = DateSet.ToDate(drAcYears["DateFrom"].ToString(), false);
                        break;
                    }
                }
            }
            return dtPrDateFrom;
        }

        private void MigrateFCPurposeOPeningBalance()
        {
            string Project = string.Empty;
            string Purpose = string.Empty;
            double Amount = 0;
            string TransMode = string.Empty;
            int ProjectId = 0;
            int PurposeId = 0;

            SetProgressBar();
            //DataTable dtFirstAccYear = GetFirstAccYearFromAcmePlus();
            //if (dtFirstAccYear != null && dtFirstAccYear.Rows.Count > 0)
            //{
            //DateTime dtAccYear = this.DateSet.ToDate(dtFirstAccYear.Rows[0]["DATEFROM"].ToString(), false);
            //if (dtAccYear != DateTime.MinValue)
            //{
            DataTable dtFCPurpose = GetFCPurposeOpeningBalance(MigrateByYear == true ? deDateFrom.AddDays(-1) : GetFirstMigrationYear().AddDays(-1)); // deDateFrom.AddDays(-1));
            if (dtFCPurpose != null)
            {
                SetMigrationStatusMessage("FC Purpose Opening Balance", dtFCPurpose.Rows.Count);
                foreach (DataRow drFCPurpose in dtFCPurpose.Rows)
                {
                    ChangeProgressBarStatus();

                    Project = drFCPurpose["PROJECT"].ToString();
                    ProjectId = GetProjectId(Project, NumberSet.ToInteger(drFCPurpose["DIVISIONID"].ToString()));

                    Purpose = drFCPurpose["HEAD"].ToString();
                    PurposeId = GetPurposeIdByPurpose(Purpose);

                    Amount = this.NumberSet.ToDouble(drFCPurpose["CONAMOUNT"].ToString());

                    TransMode = (Amount <= 0) ? TransactionMode.CR.ToString() : TransactionMode.DR.ToString();

                    if (ProjectId > 0 && PurposeId > 0 && Amount > 0)
                    {
                        using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateFCPurposeOpening))
                        {
                            dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                            dataManager.Parameters.Add(AppSchema.Purposes.CONTRIBUTION_IDColumn, PurposeId);
                            dataManager.Parameters.Add(AppSchema.VoucherTransaction.AMOUNTColumn, Math.Abs(Amount));
                            dataManager.Parameters.Add(AppSchema.LedgerBalance.TRANS_MODEColumn, TransMode);

                            resultArgs = dataManager.UpdateData();
                            SetExecutionCount();
                            if (resultArgs != null && resultArgs.Success)
                            {
                                MasterCount += resultArgs.RowsAffected;
                            }

                        }
                    }
                }
            }
            // }
            //}
        }

        public int GetCFCPurposeCode(string Purpose)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GetPurposeId))
            {
                dataManager.Parameters.Add(AppSchema.Purposes.FC_PURPOSEColumn, Purpose);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                SetExecutionCount();
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int GetPurposeIdByPurpose(string Purpose)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GetPurposeIdByPurpose))
            {
                dataManager.Parameters.Add(AppSchema.Purposes.FC_PURPOSEColumn, Purpose);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                SetExecutionCount();
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public string GetPurposeCode(string Code)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GetPurposeCode))
            {
                dataManager.Parameters.Add(AppSchema.Purposes.CODEColumn, Code);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                SetExecutionCount();
            }
            return resultArgs.DataSource.Sclar.ToString;
        }

        public int GeneratePurposeCode()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GeneratePurposeCode))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                SetExecutionCount();
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        #endregion

        #region Migrate Journal Vouchers

        /// <summary>
        /// To migrate journal vouchers from acmeplus 
        /// 
        /// 19/06/2017, fix master and child vouchers
        /// Master and child vouchers are updated in JournalTrans itself 
        /// JRegister=1 :: Master voucher, group by SerialNumber its child vouchers includes master
        /// 
        /// </summary>
        private void MigrateJournalVouchers()
        {
            SetProgressBar();
            DataTable dtSourceJournalVoucher = GetJournalVouchers();
            if (dtSourceJournalVoucher != null)
            {
                Narration_Individual = string.Empty;
                
                //On 24/04/2024, To get only date ranges only 
                //Filter master vouchers
                //dtSourceJournalVoucher.DefaultView.RowFilter = "JRegister=1";
                //DataTable dtMasterJounralVouchers = dtSourceJournalVoucher.DefaultView.ToTable();

                string filtercondition = "JRegister=1";
                if (MigrateByYear)
                {
                    filtercondition += " AND JDATE >= '" + YearFrom.ToShortDateString() + "'";
                }
                dtSourceJournalVoucher.DefaultView.RowFilter = filtercondition;
                DataTable dtMasterJounralVouchers = dtSourceJournalVoucher.DefaultView.ToTable();

                SetMigrationStatusMessage("Journal Transaction", dtMasterJounralVouchers.Rows.Count);

                
                foreach (DataRow drJournal in dtMasterJounralVouchers.Rows)
                {
                    ChangeProgressBarStatus();
                    int ProjectId = NumberSet.ToInteger(drJournal["PROJECTID"].ToString());
                    if (ProjectId > 0)
                    {
                        int DivisionId = NumberSet.ToInteger(drJournal["DivisionId"].ToString());
                        string ProjectName = GetProjectNameByIdDivisionFromAcMePlus(ProjectId, DivisionId);
                        VoucherProjectId = GetProjectId(ProjectName, DivisionId);
                        if (!(VoucherProjectId > 0))
                            VoucherProjectId = GetProjectId(ProjectName, DivisionId);
                        deVoucherDate = DateSet.ToDate(drJournal["JDATE"].ToString(), false);
                        VoucherNo = drJournal["VOUCHERNUMBER"].ToString();
                        VoucherType = "JN";
                        VoucherDefinitionId = (int)DefaultVoucherTypes.Journal;

                        Narration = string.Empty;
                        //CreatedBy = 1;
                        //ModifiedBy = 1;
                        resultArgs = VoucherMasterTrans();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            VoucherId = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                            if (VoucherId > 0)
                            {
                                Int32 SerialNumber = NumberSet.ToInteger(drJournal["SerialNumber"].ToString());
                                SequenceNo = 1;

                                //Filter its Child vouchers
                                dtSourceJournalVoucher.DefaultView.RowFilter = "SerialNumber=" + SerialNumber;
                                dtSourceJournalVoucher.DefaultView.Sort = "JTransId";
                                DataTable dtChildVouchers = dtSourceJournalVoucher.DefaultView.ToTable();
                                foreach (DataRow drChildVouchers in dtChildVouchers.Rows)
                                {
                                    TranMode = "CR";
                                    TransAmount = 0;
                                    //  LedgerId = GetLedgerId(GetLedgerNameByIdFromAcMePlus(NumberSet.ToInteger(drJournal["HEADID"].ToString())));
                                    LedgerId = GetMappedLedgerId(NumberSet.ToInteger(drChildVouchers["HEADID"].ToString()));
                                    Narration_Individual = drChildVouchers["NARRATION"].ToString();
                                    //SequenceNo = NumberSet.ToInteger(drJournal["SERIALNUMBER"].ToString());
                                    double DebitAmount = NumberSet.ToInteger(drChildVouchers["DEBIT"].ToString());
                                    double CreditAmount = NumberSet.ToInteger(drChildVouchers["CREDIT"].ToString());
                                    if (DebitAmount > 0)
                                    {
                                        TranMode = "DR";
                                        TransAmount = DebitAmount > 0 ? DebitAmount : Math.Abs(DebitAmount);
                                    }
                                    else
                                    {
                                        TransAmount = CreditAmount > 0 ? CreditAmount : Math.Abs(CreditAmount);
                                    }
                                    VoucherTrans();
                                    SequenceNo++;
                                }
                            }
                        }
                    }
                }
            }
            Narration_Individual = string.Empty;
        }

        private ResultArgs VoucherMasterTrans()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateVoucherMaster))
            {
                dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_DATEColumn, deVoucherDate, true);
                dataManager.Parameters.Add(AppSchema.VoucherMaster.PROJECT_IDColumn, VoucherProjectId);
                dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_NOColumn, VoucherNo);
                dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_TYPEColumn, VoucherType);
                dataManager.Parameters.Add(AppSchema.VoucherMaster.NARRATIONColumn, Narration);
                dataManager.Parameters.Add(AppSchema.VoucherMaster.CREATED_BYColumn, CreatedBy);
                dataManager.Parameters.Add(AppSchema.VoucherMaster.CREATED_BY_NAMEColumn, CreatedByName);
                //dataManager.Parameters.Add(AppSchema.VoucherMaster.MODIFIED_BYColumn, ModifiedBy);
                dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_DEFINITION_IDColumn, VoucherDefinitionId);
                resultArgs = dataManager.UpdateData();
                SetExecutionCount();
                if (resultArgs != null && resultArgs.Success)
                {
                    TransactionCount += resultArgs.RowsAffected;
                }
            }
            return resultArgs;
        }

        private ResultArgs VoucherTrans()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateVoucherTransJournal))
            {
                dataManager.Parameters.Add(AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherId);
                dataManager.Parameters.Add(AppSchema.VoucherTransaction.SEQUENCE_NOColumn, SequenceNo);
                dataManager.Parameters.Add(AppSchema.VoucherTransaction.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(AppSchema.VoucherTransaction.AMOUNTColumn, TransAmount);
                dataManager.Parameters.Add(AppSchema.VoucherTransaction.TRANS_MODEColumn, TranMode);
                dataManager.Parameters.Add(AppSchema.VoucherMaster.NARRATIONColumn, Narration_Individual);
                resultArgs = dataManager.UpdateData();
                SetExecutionCount();
                if (resultArgs != null && resultArgs.Success)
                {
                    TransactionCount += resultArgs.RowsAffected;
                }
            }
            return resultArgs;
        }

        #endregion

        #region Migrate Voucher Transaction
        private void MigrateVoucherTransaction()
        {
            DataTable dtYear = GetAllAcYearFromAcMePlus();
            if (dtYear != null)
            {
                if (MigrateByYear)
                {
                    DataView dvAcYear = new DataView(dtYear);
                    dvAcYear.RowFilter = String.Format("DATEFROM>={0}", YearFrom.Year);
                    TotalYearCount = dvAcYear.Count;
                    dtYear = dvAcYear.ToTable();
                }
                else
                {
                    TotalYearCount = dtYear.Rows.Count;
                }
                foreach (DataRow drYear in dtYear.Rows)
                {
                    SetProgressBar(true);
                    MigrateVoucher(drYear);
                    MigratedYearCount++;
                }
            }
            SetProgressBar(true, true);
        }

        private void MigrateVoucher(DataRow drYear)
        {
            DataTable dtVoucheDetails = GetVoucherTransactionFromAcMePlus(drYear["DATEFROM"].ToString());
            if (dtVoucheDetails != null)
            {
                SetMigrationStatusMessage("Transaction details", dtVoucheDetails.Rows.Count);
                foreach (DataRow drVouchers in dtVoucheDetails.Rows)
                {
                    ChangeProgressBarStatus();
                    DateTime deValidateDate = DateSet.ToDate(drVouchers["CBDate"].ToString(), false);

                    if (MigrateByYear)
                    {
                        if (deValidateDate.CompareTo(deDateFrom) >= 0 && deValidateDate <= deDateTo)
                        {
                            MigrateVoucherTransaction(drVouchers);
                        }
                    }
                    else
                    {
                        MigrateVoucherTransaction(drVouchers);
                    }
                }

            }
        }

        private void MigrateVoucherTransaction(DataRow drVouchers)
        {
            LedgerId = -1;
            int HeadId = NumberSet.ToInteger(drVouchers["HEADID"].ToString());
            if (HeadId > 0)
            {
                // LedgerId = GetLedgerId(GetLedgerNameByIdFromAcMePlus(HeadId));
                LedgerId = GetMappedLedgerId(HeadId);
                string NarrationTemp = drVouchers["Narration"].ToString().Replace('"', '\'');
                int ProjectId = NumberSet.ToInteger(drVouchers["ProjectId"].ToString());
                string ProjectName = drVouchers["PROJECT"].ToString();
                int DivisionId = NumberSet.ToInteger(drVouchers["DIVISIONID"].ToString());
                if (ProjectId > 0)
                {
                    int acmeerpcountryid = 0;
                    TranMode = drVouchers["TransMode"].ToString();
                    string TransactionMode = string.Empty;
                    switch (TranMode.ToUpper())
                    {
                        case "CN":
                            TransactionMode = "CN";
                            VoucherDefinitionId = (int)DefaultVoucherTypes.Contra;
                            break;
                        case "CP":
                            TransactionMode = "PY";
                            VoucherDefinitionId = (int)DefaultVoucherTypes.Payment;
                            break;
                        case "CR":
                            TransactionMode = "RC";
                            VoucherDefinitionId = (int)DefaultVoucherTypes.Receipt;
                            break;
                    }


                    int ProjectIdTemp = GetProjectId(ProjectName, DivisionId);
                    if (!(ProjectIdTemp > 0))
                        ProjectId = GetProjectId(ProjectName, DivisionId);
                    else
                        ProjectId = ProjectIdTemp;

                    deVoucherDate = DateSet.ToDate(drVouchers["CBDate"].ToString(), false);
                    VoucherProjectId = ProjectId;
                    VoucherNo = drVouchers["VoucherNumber"].ToString();
                    VoucherType = TransactionMode;
                    Narration = NarrationTemp;
                    //CreatedBy = ModifiedBy = 1; //1-admin by default

                    //Mapping of Donor
                    int ConDetailId = NumberSet.ToInteger(drVouchers["ConDetailId"].ToString());
                    if (ConDetailId == 0)
                    {
                        DataTable dtDonorTrans = GetDonorTransactionDetailsFromAcMePlus(ConDetailId);
                        if (dtDonorTrans != null && dtDonorTrans.Rows.Count > 0)
                        {
                            int DonAudId = NumberSet.ToInteger(dtDonorTrans.Rows[0]["DonAudId"].ToString());
                            int DonorId = 0;
                            if (DonAudId > 0)
                            {
                                DonorId = GetDonorAuditorId(DonAudId);
                                if (DonorId > 0)
                                {
                                    string donorcountryname = GetDonorCountryIdFromAcMeplus(DonAudId);
                                    acmeerpcountryid = GetCountryId(donorcountryname);

                                    if (IsProjectDonorNotMapped(ProjectId, DonorId))
                                    {
                                        //Mapping Donor details
                                        using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MapProjectDonor))
                                        {
                                            dataManager.Parameters.Add(AppSchema.VoucherMaster.DONOR_IDColumn, DonorId);
                                            dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                                            resultArgs = dataManager.UpdateData();
                                            SetExecutionCount();
                                        }
                                    }
                                }
                            }

                            //Mapping the Purpose details
                            int ConHeadId = NumberSet.ToInteger(dtDonorTrans.Rows[0]["ConHeadId"].ToString());
                            int PurposeId = 0;
                            if (ConHeadId > 0)
                            {
                                DataTable dtConHead = GetContributionHeadFromAcmePlus(ConHeadId);
                                if (dtConHead != null && dtConHead.Rows.Count > 0)
                                {
                                    PurposeId = GetPurposeIdByPurpose(dtConHead.Rows[0]["Head"].ToString());
                                    if (PurposeId > 0)
                                    {
                                        if (IsProjectPurposeNotMapped(ProjectId, PurposeId))
                                        {
                                            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MapPurpose))
                                            {
                                                dataManager.Parameters.Add(AppSchema.VoucherMaster.PURPOSE_IDColumn, PurposeId);
                                                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                                                resultArgs = dataManager.UpdateData();
                                                SetExecutionCount();
                                            }
                                        }
                                    }
                                }
                            }

                            //Master Transaction with Donor Details
                            //int DonorCountryId = GetDonorCountryIdFromAcMeplus(DonAudId);
                            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateVoucherMasterDonor))
                            {
                                dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_DATEColumn, deVoucherDate, true);
                                dataManager.Parameters.Add(AppSchema.VoucherMaster.PROJECT_IDColumn, VoucherProjectId);
                                dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_NOColumn, VoucherNo);
                                dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_TYPEColumn, VoucherType);
                                dataManager.Parameters.Add(AppSchema.VoucherMaster.NARRATIONColumn, Narration);
                                dataManager.Parameters.Add(AppSchema.DonorAuditor.DONAUD_IDColumn, DonorId);
                                dataManager.Parameters.Add(AppSchema.VoucherMaster.PURPOSE_IDColumn, PurposeId);
                                dataManager.Parameters.Add(AppSchema.VoucherMaster.CONTRIBUTION_TYPEColumn, dtDonorTrans.Rows[0]["FirstSubs"].ToString());
                                dataManager.Parameters.Add(AppSchema.VoucherMaster.CONTRIBUTION_AMOUNTColumn, NumberSet.ToDouble(dtDonorTrans.Rows[0]["Amount"].ToString()));
                                dataManager.Parameters.Add(AppSchema.VoucherMaster.CURRENCY_COUNTRY_IDColumn, acmeerpcountryid); //DonorCountryId
                                dataManager.Parameters.Add(AppSchema.VoucherMaster.EXCHANGE_RATEColumn, NumberSet.ToDouble(dtDonorTrans.Rows[0]["ExchangeRate"].ToString()));
                                dataManager.Parameters.Add(AppSchema.VoucherMaster.EXCHANGE_COUNTRY_IDColumn, acmeerpcountryid); //DonorCountryId
                                dataManager.Parameters.Add(AppSchema.VoucherMaster.CALCULATED_AMOUNTColumn, (NumberSet.ToDouble(dtDonorTrans.Rows[0]["ExchangeRate"].ToString()) * NumberSet.ToDouble(dtDonorTrans.Rows[0]["ConAmount"].ToString())));
                                dataManager.Parameters.Add(AppSchema.VoucherMaster.ACTUAL_AMOUNTColumn, NumberSet.ToDouble(dtDonorTrans.Rows[0]["ConAmount"].ToString()));

                                dataManager.Parameters.Add(AppSchema.VoucherMaster.CREATED_BYColumn, CreatedBy);
                                dataManager.Parameters.Add(AppSchema.VoucherMaster.CREATED_BY_NAMEColumn, CreatedByName);

                                dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_DEFINITION_IDColumn, VoucherDefinitionId);
                                resultArgs = dataManager.UpdateData();
                                SetExecutionCount();
                                if (resultArgs != null)
                                {
                                    VoucherId = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                    TransactionCount += resultArgs.RowsAffected;

                                }
                            }
                        }
                        else
                        {
                            //Master Transaction without Donor
                            resultArgs = VoucherMasterTrans();
                            if (resultArgs != null)
                            {
                                VoucherId = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                TransactionCount += resultArgs.RowsAffected;
                            }
                        }
                    }
                    else
                    {
                        //Master Transaction without Donor
                        resultArgs = VoucherMasterTrans();
                        if (resultArgs != null)
                        {
                            VoucherId = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                            TransactionCount += resultArgs.RowsAffected;
                        }
                    }

                    if (VoucherId > 0)
                    {
                        TransAmount = 0;
                        TransModeCC = string.Empty;
                        double DebitAmount = NumberSet.ToDouble(drVouchers["Debit"].ToString());
                        double CreditAmount = NumberSet.ToDouble(drVouchers["Credit"].ToString());
                        if (DebitAmount > 0)
                        {
                            TransModeCC = "DR";
                            TransAmount = DebitAmount > 0 ? DebitAmount : Math.Abs(DebitAmount);
                        }
                        else
                        {
                            TransModeCC = "CR";
                            TransAmount = CreditAmount > 0 ? CreditAmount : Math.Abs(CreditAmount);
                        }
                        //Mappping Cost Cetre
                        if (NumberSet.ToInteger(drVouchers["CostCentreID"].ToString()) > 0)
                        {
                            this.ProjectId = ProjectId;
                            VoucherCostCentreId = GetCostCentreId(GetCostCentreNameFromAcMePlus(NumberSet.ToInteger(drVouchers["CostCentreID"].ToString())));
                            MapCostCentre();
                            //Migrate Cost Centre Transaction
                            MigrateCostCentreTransaction();
                        }
                        //CashId 1 by default in Acme.erp
                        if (CashId > 0)
                        {
                            ChequeNo = drVouchers["ChequeNo"].ToString();
                            deMaterializedOn = (string.IsNullOrEmpty(drVouchers["ReconcilDate"].ToString())) ? drVouchers["ReconcilDate"].ToString() : string.Empty;

                            switch (TranMode.ToUpper())
                            {
                                case "CR":
                                    {
                                        //---------------------------------------------Receipt Voucher Entry One---------------------------------
                                        SequenceNo = 1;
                                        TranMode = "CR";
                                        MigrateVoucherTransWithChequeNo();
                                        //---------------------------------------------Receipt Voucher Entry Two---------------------------------
                                        SequenceNo = 2;
                                        TranMode = "DR";
                                        if (drVouchers["CashFlag"].ToString().Equals("C"))
                                        {
                                            LedgerId = CashId;
                                        }
                                        else
                                        {
                                            LedgerId = GetBankLedgerId(NumberSet.ToInteger(drVouchers["AccountId"].ToString()));
                                        }
                                        MigrateVoucherTransWithChequeNo();
                                        break;
                                    }
                                case "CP":
                                    {
                                        //----------------------------------------------Payment Voucher Entry One-----------------------------------
                                        SequenceNo = 1;
                                        TranMode = "DR";
                                        MigrateVoucherTransWithChequeNo();

                                        //----------------------------------------------Payment Voucher Entry Two------------------------------------
                                        SequenceNo = 2;
                                        TranMode = "CR";
                                        if (drVouchers["CashFlag"].ToString().Equals("C"))
                                        {
                                            LedgerId = CashId;
                                        }
                                        else
                                        {
                                            LedgerId = GetBankLedgerId(NumberSet.ToInteger(drVouchers["AccountId"].ToString()));
                                        }
                                        MigrateVoucherTransWithChequeNo();
                                        break;
                                    }
                                case "CN":
                                    {
                                        switch (HeadId)
                                        {
                                            case 1://Cash Deposit C-B
                                                {
                                                    //------------------------------------Cash Deposit C-B Entry One-----------------------------------
                                                    SequenceNo = 1;
                                                    TranMode = "CR";
                                                    LedgerId = CashId;
                                                    MigrateVoucherTransWithChequeNo();

                                                    //------------------------------------Cash Deposit C-B Entry Two-----------------------------------
                                                    SequenceNo = 2;
                                                    TranMode = "DR";
                                                    LedgerId = GetBankLedgerId(NumberSet.ToInteger(drVouchers["AccountId"].ToString()));
                                                    MigrateVoucherTransWithChequeNo();
                                                    break;
                                                }
                                            case 2: //Cash Withdrawal
                                                {
                                                    //-------------------------------------Cash Withdrawal (Bank-Cash) Entry One-----------------------------------
                                                    SequenceNo = 1;
                                                    LedgerId = GetBankLedgerId(NumberSet.ToInteger(drVouchers["AccountId"].ToString()));
                                                    TranMode = "CR";
                                                    MigrateVoucherTransWithChequeNo();

                                                    //--------------------------------------Cash Withdrawal Entry Two-----------------------------------
                                                    SequenceNo = 2;
                                                    TranMode = "DR";
                                                    LedgerId = CashId;
                                                    MigrateVoucherTransWithChequeNo();
                                                    break;
                                                }
                                            case 8://A/c Transfer
                                                {
                                                    //----------------------------------------A/c Transfer (Bank - Bank) Entry One---------------------------------------
                                                    SequenceNo = 1;
                                                    LedgerId = GetBankLedgerId(NumberSet.ToInteger(drVouchers["TRANSFERACID"].ToString()));
                                                    TranMode = "CR";
                                                    MigrateVoucherTransWithChequeNo();

                                                    //----------------------------------------A/c Transfer Entry Two---------------------------------------
                                                    SequenceNo = 2;
                                                    TranMode = "DR";
                                                    LedgerId = GetBankLedgerId(NumberSet.ToInteger(drVouchers["AccountId"].ToString())); ;
                                                    MigrateVoucherTransWithChequeNo();
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                            }
                        }
                    }
                }
            }
        }

        public void MapCostCentre()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MapVoucherCostCentre))
            {
                double Amount = 0;
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);

                if (this.CostCeterMapping==1)
                    dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_IDColumn.ColumnName, LedgerId,  DataType.Int32);
                else
                    dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_IDColumn.ColumnName, 0, DataType.Int32);

                dataManager.Parameters.Add(AppSchema.CostCentre.COST_CENTRE_IDColumn, VoucherCostCentreId);
                dataManager.Parameters.Add(AppSchema.VoucherTransaction.AMOUNTColumn, Amount); //By default Cost Centre Amount is zero (0) while Migration
                dataManager.Parameters.Add(AppSchema.VoucherTransaction.TRANS_MODEColumn, TransModeCC);
                resultArgs = dataManager.UpdateData();
                SetExecutionCount();
            }
        }

        private void MigrateCostCentreTransaction()
        {
            using (DataManager dataManger = new DataManager(SQLCommand.AcMePlusMigration.MigrateCostCentreTransaction))
            {
                string CostCentreTable = "0LDR" + LedgerId;
                dataManger.Parameters.Add(AppSchema.VoucherCostCentre.VOUCHER_IDColumn, VoucherId);
                dataManger.Parameters.Add(AppSchema.VoucherCostCentre.LEDGER_IDColumn, LedgerId);
                dataManger.Parameters.Add(AppSchema.VoucherCostCentre.COST_CENTRE_IDColumn, VoucherCostCentreId);
                dataManger.Parameters.Add(AppSchema.VoucherCostCentre.COST_CENTRE_TABLEColumn, CostCentreTable);
                dataManger.Parameters.Add(AppSchema.VoucherCostCentre.AMOUNTColumn, TransAmount);
                dataManger.Parameters.Add(AppSchema.VoucherTransaction.SEQUENCE_NOColumn, 1);
                dataManger.Parameters.Add(AppSchema.VoucherCostCentre.LEDGER_SEQUENCE_NOColumn, 1);

                resultArgs = dataManger.UpdateData();
                SetExecutionCount();
                if (resultArgs != null)
                {
                    TransactionCount += resultArgs.RowsAffected;
                }
            }
        }

        public void MigrateVoucherTransWithChequeNo()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateVoucherTransWithChequeNo))
            {
                dataManager.Parameters.Add(AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherId);
                dataManager.Parameters.Add(AppSchema.VoucherTransaction.SEQUENCE_NOColumn, SequenceNo);
                dataManager.Parameters.Add(AppSchema.VoucherTransaction.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(AppSchema.VoucherTransaction.AMOUNTColumn, TransAmount);
                dataManager.Parameters.Add(AppSchema.VoucherTransaction.TRANS_MODEColumn, TranMode);
                dataManager.Parameters.Add(AppSchema.VoucherTransaction.CHEQUE_NOColumn, ChequeNo);
                dataManager.Parameters.Add(AppSchema.VoucherTransaction.CHEQUE_REF_DATEColumn, Cheque_Ref_Date);
                dataManager.Parameters.Add(AppSchema.VoucherTransaction.CHEQUE_REF_BANKNAMEColumn, Cheque_Ref_Bank);
                dataManager.Parameters.Add(AppSchema.VoucherTransaction.CHEQUE_REF_BRANCHColumn, Cheque_Ref_BankBranch);
                dataManager.Parameters.Add(AppSchema.VoucherTransaction.MATERIALIZED_ONColumn, deMaterializedOn);
                resultArgs = dataManager.UpdateData();
                SetExecutionCount();
                SequenceNo = 0;
            }
        }

        public ResultArgs MigrateVoucherTransResultArg()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateVoucherTransWithChequeNo))
            {
                dataManager.Parameters.Add(AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherId);
                dataManager.Parameters.Add(AppSchema.VoucherTransaction.SEQUENCE_NOColumn, SequenceNo);
                dataManager.Parameters.Add(AppSchema.VoucherTransaction.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(AppSchema.VoucherTransaction.AMOUNTColumn, TransAmount);
                dataManager.Parameters.Add(AppSchema.VoucherTransaction.TRANS_MODEColumn, TranMode);
                dataManager.Parameters.Add(AppSchema.VoucherTransaction.CHEQUE_NOColumn, ChequeNo);
                dataManager.Parameters.Add(AppSchema.VoucherTransaction.MATERIALIZED_ONColumn, deMaterializedOn);
                resultArgs = dataManager.UpdateData();
                SetExecutionCount();
                SequenceNo = 0;
            }
            return resultArgs;
        }

        private void UpdateVoucherDate()
        {
            using (TallyMigrationSystem tallyMigration = new TallyMigrationSystem())
            {
                tallyMigration.UpdateVoucherMasterTrans();
            }
        }

        private void EnableCostCentreForLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.UpdateLedgerCostCentre))
            {
                resultArgs = dataManager.UpdateData();
            }
        }

        #endregion

        #region Refresh Balance
        private ResultArgs RefreshBalance()
        {
            SetProgressBar();
            balanceSystem.VoucherDate = BookBeginFrom;
            balanceSystem.RefreshBalanceSetMaxValue += new EventHandler(BalanceSystem_RefreshBalanceSetMaxValue);
            balanceSystem.RefreshBalanceUpdateProgressBar += new EventHandler(BalanceSystem_RefreshBalanceUpdateProgressBar);
            ResultArgs result = balanceSystem.UpdateBulkTransBalance();
            return resultArgs;
        }


        private void BalanceSystem_RefreshBalanceSetMaxValue(object sender, EventArgs e)
        {
            if (ProgressBarEvent != null)
            {
                IsMigrateionCompleted = true;
                MigrationStatusMessage = "Refreshing Balance";
                ProgressBarMaxCount = balanceSystem.RefreshProgressBarMaxCount;
                ProgressBarEvent(this, new EventArgs());
            }
        }

        private void BalanceSystem_RefreshBalanceUpdateProgressBar(object sender, EventArgs e)
        {
            ChangeProgressBarStatus();
        }

        #endregion

        #region UI Updation (Triggering Events)
        private void SetProgressBar(bool TransMigration = false, bool IsTransactionEnds = false)
        {
            if (InitProgressBar != null)
            {
                MigratingTransaction = TransMigration;
                TransactionEnds = TransactionEnds;
                InitProgressBar(this, new EventArgs());
            }
        }

        private void SetMigrationStatusMessage(string Message, int MaxCount, bool IsMigrateionComp = false)
        {
            if (ProgressBarEvent != null)
            {
                IsMigrateionCompleted = IsMigrateionComp;
                MigrationStatusMessage = Message;
                ProgressBarMaxCount = MaxCount;
                ProgressBarEvent(this, new EventArgs());
            }
        }

        private void ChangeProgressBarStatus()
        {
            if (IncreaseProgressBar != null)
            {
                IncreaseProgressBar(this, new EventArgs());
            }
        }
        #endregion

        #endregion

        #region OLEDB Operations
        #region User List

        private DataTable GetUserListFromAcMePlus()
        {
            DataTable dtUserList = OleDbAccess.ExecuteOleDbQuery("SELECT UserId,UserName,PassWord FROM UserRight", OleDbConn);
            return dtUserList;
        }
        #endregion

        #region Accounting Year
        private DataTable GetAllAccountingYearFromAcMePlus()
        {
            DataTable dtAcYear = OleDbAccess.ExecuteOleDbQuery("SELECT * FROM ACYEAR ORDER BY ACORDER ASC;", OleDbConn);
            return dtAcYear;
        }
        #endregion

        #region Master Bank
        private DataTable GetMasterBankFromAcMePlus()
        {
            return OleDbAccess.ExecuteOleDbQuery("SELECT * FROM BANK", OleDbConn);
        }

        private DataTable GetMasterBankByIdFromAcMePlus(int AcMePlusBankId)
        {
            return OleDbAccess.ExecuteOleDbQuery(String.Format("SELECT BANK,PLACE FROM BANK WHERE bankid= {0}", AcMePlusBankId), OleDbConn);
        }

        #endregion

        #region Country
        private DataTable GetCountryListFromAcMePlus()
        {
            return OleDbAccess.ExecuteOleDbQuery("SELECT * FROM COUNTRY", OleDbConn);
        }

        private int GetCountryIdFromAcMePlus(int OldCountryId)
        {
            string Query = String.Format("SELECT COUNTRY FROM COUNTRY WHERE COUNTRYID={0}", OldCountryId);
            string Name = OleDbAccess.ExecuteOleDbScalarValue(Query, OleDbConn).ToString();
            return GetCountryId(Name);
        }

        #endregion

        #region Project
        private DataTable GetAllProjectListFromAcMePlus()
        {
            return OleDbAccess.ExecuteOleDbQuery("SELECT DIVISIONID,P.PROJECTID,ABBREVIATION,PROJECT,ACDATE,DATESTARTED,DATECLOSED,DESCRIPTION" +
                                                             " FROM PROJECT P LEFT JOIN PROJECTDIVISION PD ON PD.PROJECTID=P.PROJECTID ORDER BY DIVISIONID;", OleDbConn);
        }

        private string GetProjectNameByIdDivisionFromAcMePlus(int ProjectId, int DivisionId)
        {
            //            string Query = String.Format(@"SELECT PROJECT FROM PROJECTDIVISION PD
            //                                           LEFT JOIN PROJECT P ON P.PROJECTID=PD.PROJECTID 
            //                                            WHERE PD.PROJECTID={0} AND PD.DIVISIONID={1};", ProjectId, DivisionId);

            string Query = String.Format(@"SELECT PROJECT FROM PROJECTDIVISION PD
                                           INNER JOIN PROJECT P ON P.PROJECTID=PD.PROJECTID 
                                            WHERE PD.PROJECTID={0} AND PD.DIVISIONID={1};", ProjectId, DivisionId);
            return OleDbAccess.ExecuteOleDbScalarValue(Query, OleDbConn).ToString();
        }

        private DataTable GetBankAccountMappedProject()
        {
            return OleDbAccess.ExecuteOleDbQuery("SELECT ACCOUNTID,PROJECTID,DIVISIONID FROM BANKACCOUNT ORDER BY PROJECTID", OleDbConn);
        }

        private string GetProjectNameFromAcMePlus(int ProjectId)
        {
            return OleDbAccess.ExecuteOleDbScalarValue(String.Format("SELECT PROJECT FROM PROJECT WHERE PROJECTID={0}", ProjectId), OleDbConn).ToString();
        }
        #endregion

        #region Master Ledger Group
        private DataTable GetMasterLedgerGroupFromAcMePlus()
        {
            return OleDbAccess.ExecuteOleDbQuery("SELECT * FROM ACGROUP", OleDbConn);
        }

        private string GetGroupIdFromAcMePlus(int GroupId)
        {
            return OleDbAccess.ExecuteOleDbScalarValue(String.Format("SELECT GROUP FROM ACGROUP WHERE GROUPID={0}", GroupId), OleDbConn).ToString();
        }

        #endregion

        #region Ledger
        public DataTable GetAllLedgersFromAcMePlus()
        {
            //            string Query = @"SELECT HEADID AS LedgerId,H.ABBREVIATION AS LedgerCode,HEAD AS LedgerName,H.GROUPID,GROUP AS Parent,IKHEAD,
            //                                   '' as PrimaryGroup,
            //                                   0.0 as OpeningBalance,
            //                                   0.0 as ClosingBalance,
            //                                   ''  as IsCostCentersOn,
            //                                   '' as BankHolderName,
            //                                   '' as BankDetails,
            //                                   '' as BankBranchName,
            //                                   '' as BankType,
            //                                   '' as IFSCode,
            //                                   '' as Address,
            //                                   '' as [PAN/IT],
            //                                   '' as NameOnPan,
            //                                   '' as TDSDedecteeType,
            //                                   0 as ParentId
            //                                FROM ACHEAD H
            //                                LEFT JOIN AcGroup G
            //                                ON H.GROUPID = G.GROUPID";
            string Query = @"SELECT HEADID AS LedgerId,H.ABBREVIATION AS LedgerCode,HEAD AS LedgerName,H.GROUPID,GROUP AS Parent,IKHEAD
                                FROM ACHEAD H
                                INNER JOIN AcGroup G
                                ON H.GROUPID = G.GROUPID";
            DataTable dtLedger = OleDbAccess.ExecuteOleDbQuery(Query, OleDbConn);
            if (dtLedger != null)
            {
                //----------------------LedgerId =1 =>Cash Deposit----------------------------------------------
                //----------------------LedgerId =2 =>Cash Withdrawal-------------------------------------------
                //----------------------LedgerId =3 =>Capital Fund----------------------------------------------
                //----------------------LedgerId =5 =>Fixed Deposit Invested------------------------------------
                //----------------------LedgerId =6 =>Fixed Deposit Realised------------------------------------
                //----------------------LedgerId =8 =>A/c Transfer----------------------------------------------
                DataView dvLedger = new DataView(dtLedger);
                //The above Ledgerid should not be migrated because its no longer is used in AcMeERP
                dvLedger.RowFilter = "LedgerId<>0";
                if (dvLedger.ToTable() != null)
                    dtLedger = dvLedger.ToTable();
                else
                    dtLedger = null;
            }
            return dtLedger;
        }

        private string GetLedgerNameByIdFromAcMePlus(int AcMePlusLedgerId)
        {
            return OleDbAccess.ExecuteOleDbScalarValue(String.Format("SELECT HEAD FROM ACHEAD WHERE HEADID={0}", AcMePlusLedgerId), OleDbConn).ToString();
        }
        #endregion

        #region Mapping
        private DataTable GetMappingFromAcMeplus()
        {
            return OleDbAccess.ExecuteOleDbQuery("SELECT ProjectId,DivisionId FROM PROJECTDIVISION", OleDbConn);
        }

        private DataTable GetOpBanalceProjectFromAcMePlus()
        {
            return OleDbAccess.ExecuteOleDbQuery("SELECT ACDATE FROM PROJECTHEAD", OleDbConn);
        }

        private DataTable GetOPBalanceByProjectDivision(int ProjectId, int DivisionId)
        {
            string OPQuery = "SELECT PROJECT,OPBALANCE FROM PROJECT P INNER JOIN PROJECTDIVISION PD ON P.PROJECTID=PD.PROJECTID WHERE PD.PROJECTID=" + ProjectId + " AND PD.DIVISIONID=" + DivisionId + ";";
            return OleDbAccess.ExecuteOleDbQuery(OPQuery, OleDbConn);
        }

        private DataTable GetMappingProjectByPeriodFromAcMePlus()
        {
            string Query = @"SELECT P.PROJECTID,DIVISIONID FROM PROJECT AS  P
                                    LEFT  JOIN PROJECTDIVISION  AS PD ON P.PROJECTID=PD.PROJECTID";
            return OleDbAccess.ExecuteOleDbQuery(Query, OleDbConn);
        }

        private object GetCashOpBalanceDate(int ProjectId, int ProjectDivisionId)
        {
            string DateQuery = String.Format("SELECT TOP 1 CASHDATE FROM DAILYCASHBAL WHERE CASHDATE< DATEVALUE('{0}') AND PROJECTID={1} AND DIVISIONID={2} ORDER BY CASHDATE DESC",
                            deDateFrom, ProjectId, ProjectDivisionId);
            return OleDbAccess.ExecuteOleDbScalarValue(DateQuery, OleDbConn);
        }

        private DataTable GetCashOpeningBalance(DateTime Date, int ProjectId, int ProjectDivisionId)
        {
            string Query = String.Format("SELECT TOP 1 CASHDATE,PROJECTID,DIVISIONID,AMOUNT FROM DAILYCASHBAL WHERE CASHDATE<=DATEVALUE('{0}') AND PROJECTID={1} AND DIVISIONID={2} ORDER BY CASHDATE DESC",
                                          Date.ToString(), ProjectId, ProjectDivisionId);
            return OleDbAccess.ExecuteOleDbQuery(Query, OleDbConn);
        }

        private string ProjectNameByIdFromAcMePlus(int ProjectId)
        {
            return OleDbAccess.ExecuteOleDbScalarValue(String.Format("SELECT PROJECT FROM PROJECT WHERE PROJECTID={0}", ProjectId), OleDbConn).ToString();
        }

        private DataTable GetProjectLedgerMappingFromAcMePlus()
        {
            //            return OleDbAccess.ExecuteOleDbQuery(@"SELECT PM.PROJECTID,PROJECT,HEADID,DIVISIONID FROM PROJECTHEAD PM
            //                                                        RIGHT JOIN PROJECT P ON P.PROJECTID=PM.PROJECTID;", OleDbConn);

            return OleDbAccess.ExecuteOleDbQuery(@"SELECT PM.PROJECTID,P.PROJECT,PM.HEADID,PM.DIVISIONID FROM (((PROJECTHEAD PM
                                                        INNER JOIN PROJECT P ON P.PROJECTID=PM.PROJECTID)
                                                        INNER JOIN ACHEAD H ON H.HeadID=PM.HEADID)
                                                        INNER JOIN ACGROUP G ON G.GROUPID=H.GROUPID);", OleDbConn);
        }


        #endregion

        #region Master Bank Account
        private DataTable GetAllBankAccountsFromAcMePlus()
        {
            string OleQuery = @"SELECT PROJECT, ACCOUNTID,BA.BANKID,BA.ACDATE,BA.ABBREVIATION,BANKACCOUNT,DATEOPENED,BA.DATECLOSED,
                                FDAC,BA.PROJECTID,DIVISIONID,OPBALANCE,OPBALANCEFD,FDAC FROM (BANKACCOUNT BA
                                INNER JOIN PROJECT P ON P.PROJECTID=BA.PROJECTID)
                                INNER JOIN BANK B ON B.BANKID = BA.BANKID";
            return OleDbAccess.ExecuteOleDbQuery(OleQuery, OleDbConn);
        }

        private DataTable GetBankAccontsInfoFromAcMePlus(int AccountId)
        {
            return OleDbAccess.ExecuteOleDbQuery(String.Format("SELECT BANKACCOUNT,ABBREVIATION FROM BANKACCOUNT WHERE ACCOUNTID={0}", AccountId), OleDbConn);
        }

        #endregion

        #region Donor Auditor
        private DataTable GetDonorAuditorDetailFromAcMePlus()
        {
            return OleDbAccess.ExecuteOleDbQuery("SELECT * FROM DonAud", OleDbConn);
        }

        private DataTable GetDonorIdFromAcMeplus(int AcMePlusDonorId)
        {
            return OleDbAccess.ExecuteOleDbQuery(String.Format("SELECT * FROM DONAUD WHERE DONAUDID={0}", AcMePlusDonorId), OleDbConn);
        }
        #endregion

        #region Migrate Cost Centre
        private DataTable GetCostCentreFromAcMePlus()
        {
            return OleDbAccess.ExecuteOleDbQuery("SELECT COSTCENTREID,ABBREVIATION,COSTCENTRE FROM COSTCENTRE", OleDbConn);
        }

        private string GetCostCentreNameFromAcMePlus(int CostCenterId)
        {
            return OleDbAccess.ExecuteOleDbScalarValue(String.Format("SELECT COSTCENTRE FROM COSTCENTRE WHERE COSTCENTREID={0}", CostCenterId), OleDbConn).ToString();
        }
        #endregion

        #region Executive Committee
        private DataTable GetExecutiveCommitteeFromAcMePlus()
        {
            return OleDbAccess.ExecuteOleDbQuery("SELECT ExecutiveId,Executive,Name,Nationality,Occupation,Association," +
                        "OfficeBarrier,Place,State,CountryId,PinCode,Phone,Fax,Email,URL FROM ExeCommittee", OleDbConn);
        }
        #endregion

        #region FC Purpose
        private DataTable GetFCPurposeFromAcMePlus()
        {
            return OleDbAccess.ExecuteOleDbQuery("select * from ContributionHead", OleDbConn);
        }

        private DataTable GetFCPurposeOpeningBalance(DateTime dtOPDate)
        {
            //On 21/08/2017, Add DivisionId in the sql for getting Purpose O/p
            string Query = string.Format(@"SELECT P.PROJECT, CD.DivisionId, CH.HEAD,SUM( IIf(TRANSMODE='CR', CD.CONAMOUNT , -CD.CONAMOUNT ) ) AS CONAMOUNT FROM ((CONDETAIL CD
                            INNER JOIN PROJECT P ON P.PROJECTID=CD.PROJECTID)
                            INNER JOIN CONTRIBUTIONHEAD CH ON CH.CONHEADID=CD.CONHEADID)
                            WHERE CD.CONDATE<=DATEVALUE('{0}') GROUP BY P.PROJECT, CD.DivisionId, CH.HEAD", dtOPDate);
            return OleDbAccess.ExecuteOleDbQuery(Query, OleDbConn);
        }

        //private DataTable GetFirstAccYearFromAcmePlus()
        //{
        //    return OleDbAccess.ExecuteOleDbQuery("SELECT TOP 1 DATEFROM,DATETO FROM ACYEAR ORDER BY ACORDER ASC", OleDbConn);
        //}
        #endregion

        #region Opening Balance
        private DataTable GetOpeningBalanceFromAcMePlus()
        {
            //return OleDbAccess.ExecuteOleDbQuery("SELECT ACDATE,PROJECTID,DIVISIONID,HEADID,OPBALANCE FROM PROJECTHEAD", OleDbConn);

            return OleDbAccess.ExecuteOleDbQuery(@"SELECT PM.ACDATE, PM.PROJECTID, PM.DIVISIONID, PM.HEADID,PM.OPBALANCE FROM (((PROJECTHEAD PM
                                                  INNER JOIN PROJECT P ON P.PROJECTID=PM.PROJECTID)
                                                  INNER JOIN ACHEAD H ON H.HeadID=PM.HEADID)
                                                  INNER JOIN ACGROUP G ON G.GROUPID=H.GROUPID)", OleDbConn);
        }

        private string GetBankOpeningBalanceDate()
        {
            string OpDate = BookBeginFrom;
            string DateQuery = String.Format("SELECT TOP 1 CASHDATE FROM DAILYCASHBAL WHERE CASHDATE<= DATEVALUE('{0}') ORDER BY CASHDATE DESC", deDateFrom.AddDays(-1));
            object oDate = OleDbAccess.ExecuteOleDbScalarValue(DateQuery, OleDbConn);
            if (oDate != null)
            {
                OpDate = oDate.ToString();
            }
            return OpDate;
        }

        private DataTable GetBankOpeningBalanceFromAcMePlus(int ProjectId, int DivId, int AccountId, string DateBankOpBal)
        {
            string Query = String.Format(@"SELECT TOP 1 BANKDATE, PROJECTID, DIVISIONID, AMOUNT, P.ACCOUNTID, BANKACCOUNT, ABBREVIATION
                                                    FROM BANKACCOUNT AS BA LEFT JOIN DAILYBANKBAL AS P ON P.ACCOUNTID=BA.ACCOUNTID
                                                    WHERE PROJECTID={0} AND DIVISIONID={1} AND  P.ACCOUNTID={2} AND BANKDATE<=DATEVALUE('{3}')
                                                    ORDER BY BANKDATE DESC", ProjectId, DivId, AccountId, DateBankOpBal);
            return OleDbAccess.ExecuteOleDbQuery(Query, OleDbConn);
        }


        #endregion

        #region Journal Voucher
        private DataTable GetJournalVouchers()
        {
            string JournalQuery = string.Empty;
            if (MigrateByYear)
            {
                //                JournalQuery = String.Format(@"SELECT JT.PROJECTID, P.PROJECT,DIVISIONID,HEADID,JTRANSID ,JDATE,DEBIT,CREDIT,VOUCHERID,VOUCHERNUMBER,SERIALNUMBER,NARRATION
                //                                    FROM JOURNALTRANS JT LEFT JOIN PROJECT P ON  JT.PROJECTID=P.PROJECTID WHERE  YEAR(JDATE)>={0}", deDateFrom.ToShortDateString());

                JournalQuery = String.Format(@"SELECT JT.PROJECTID, P.PROJECT,JT.DIVISIONID,JT.HEADID,JT.JTRANSID ,JT.JDATE,JT.DEBIT,JT.CREDIT,JT.VOUCHERID,VOUCHERNUMBER,SERIALNUMBER,NARRATION,JRegister
                                    FROM (((JOURNALTRANS JT 
                                    INNER JOIN PROJECT P ON  JT.PROJECTID=P.PROJECTID)  
                                    INNER JOIN ACHEAD H ON H.HeadID=JT.HEADID)
                                    INNER JOIN ACGROUP G ON G.GROUPID=H.GROUPID)
                                    WHERE  YEAR(JDATE)>={0}", deDateFrom.ToShortDateString());
            }
            else
            {
                JournalQuery = @"SELECT JT.PROJECTID, P.PROJECT,JT.DIVISIONID,JT.HEADID,JTRANSID ,JDATE,DEBIT,CREDIT,VOUCHERID,VOUCHERNUMBER,SERIALNUMBER,NARRATION,JRegister
                                    FROM (((JOURNALTRANS JT 
                                    INNER JOIN PROJECT P ON  JT.PROJECTID=P.PROJECTID)  
                                    INNER JOIN ACHEAD H ON H.HeadID=JT.HEADID)
                                    INNER JOIN ACGROUP G ON G.GROUPID=H.GROUPID)";
            }
            return OleDbAccess.ExecuteOleDbQuery(JournalQuery, OleDbConn);
        }
        #endregion


        #region Voucher Transaction
        private DataTable GetAllAcYearFromAcMePlus()
        {
            return OleDbAccess.ExecuteOleDbQuery("SELECT YEAR(DATEFROM)  AS DATEFROM FROM ACYEAR", OleDbConn);
        }

        private DataTable GetVoucherTransactionFromAcMePlus(string YearFrom)
        {
            //            string CBTransQuery = String.Format(@"SELECT CC.COSTCENTRE,P.PROJECT,LED.HEAD, CBTRANSID,CBDATE,TRANS.PROJECTID,DIVISIONID,TRANS.HEADID,TRANS.COSTCENTREID,TRANSMODE,CASHFLAG,NARRATION,DEBIT,CREDIT,
            //                                                CONDETAILID,SERIALNUMBER,ACCOUNTID,CHEQUENO,RECONCILDATE,
            //                                                USERID,AUTHORIZER,VOUCHERID,VOUCHERNUMBER,ADDRESS,FAID,FDID,IKID,LOCKED,TRANSFERACID
            //                                                    FROM ((((CBTrans{0} AS TRANS)
            //                                                LEFT JOIN COSTCENTRE AS CC ON CC.COSTCENTREID=TRANS.COSTCENTREID )
            //                                                LEFT JOIN PROJECT AS P ON P.PROJECTID=TRANS.PROJECTID)
            //                                                LEFT JOIN ACHEAD AS LED ON LED.HEADID=TRANS.HEADID);", YearFrom);

            string CBTransQuery = String.Format(@"SELECT CC.COSTCENTRE,P.PROJECT,LED.HEAD, CBTRANSID,CBDATE,TRANS.PROJECTID,TRANS.DIVISIONID,TRANS.HEADID,TRANS.COSTCENTREID,TRANSMODE,CASHFLAG,NARRATION,DEBIT,CREDIT,
                                                CONDETAILID,SERIALNUMBER,TRANS.ACCOUNTID,CHEQUENO,RECONCILDATE,
                                                USERID,AUTHORIZER,VOUCHERID,VOUCHERNUMBER,TRANS.ADDRESS,FAID,FDID,IKID,LOCKED,TRANSFERACID
                                                    FROM ((((((CBTrans{0} AS TRANS)
                                                INNER JOIN COSTCENTRE AS CC ON CC.COSTCENTREID=TRANS.COSTCENTREID )
                                                INNER JOIN PROJECT AS P ON P.PROJECTID=TRANS.PROJECTID)
                                                INNER JOIN ACHEAD AS LED ON LED.HEADID=TRANS.HEADID)
                                                INNER JOIN ACGROUP G ON G.GROUPID=LED.GROUPID)
                                                INNER JOIN BankAccount BA ON BA.ACCOUNTID = TRANS.ACCOUNTID) 
                                                INNER JOIN BANK B ON B.BANKID=BA.BANKID;", YearFrom);

            return OleDbAccess.ExecuteOleDbQuery(CBTransQuery, OleDbConn);
        }

        private DataTable GetDonorTransactionDetailsFromAcMePlus(int ConDetailId)
        {
            return OleDbAccess.ExecuteOleDbQuery(String.Format("SELECT * FROM  ConDetail WHERE ConDetailId={0}", ConDetailId), OleDbConn);
        }

        private DataTable GetContributionHeadFromAcmePlus(int ConHeadId)
        {
            return OleDbAccess.ExecuteOleDbQuery(String.Format("SELECT * FROM  ContributionHead WHERE ConHeadId={0}", ConHeadId), OleDbConn);
        }

        private string GetDonorCountryIdFromAcMeplus(int DonAudId)
        {
            //string Name = OleDbAccess.ExecuteOleDbScalarValue(String.Format("SELECT DONOR FROM DonAud WHERE DonAudId={0}", DonAudId), OleDbConn).ToString();
            //string DonorTranQuery = String.Format("SELECT CountryId FROM DonAud WHERE [DONOR]=\'{0}\'", Name);
            //return NumberSet.ToInteger(OleDbAccess.ExecuteOleDbScalarValue(DonorTranQuery, OleDbConn).ToString());

            string CountryName = OleDbAccess.ExecuteOleDbScalarValue(String.Format(@"SELECT C.COUNTRY FROM DonAud D
                                                                INNER JOIN COUNTRY C
                                                                ON C.COUNTRYID = D.COUNTRYID 
                                                                WHERE DonAudId={0}", DonAudId), OleDbConn).ToString();
            return CountryName;
        }


        #endregion

        private ResultArgs DeleteUnusedLedgersFromAcMeERP()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.DeleteUnusedLedgers))
            {
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        #endregion

        
    }
}

