/*
 * This class is used to get data from BOSCOPAC application and migrate into Acme.erp for the following information
 * 
 * 1. House         : Branch
 * 2. Trust         : Projects
 * 3. Accounts      : Ledger Group (If grphead = 1)
 * 4. Accounts      : Ledger (If grphead = 0)
 * 5. lmain, lacdet : List of Donors (if FCamount and currency are not empty in voucher, treat as FC entries
 *                    //O-Organizaton and I-Indiviual 
 *                    Consider "paid to" as  donor name.
 *                    If there is donor name is empty but FC details found, we fix donor name is Unknown) 
 * 6. lmain, lacdet : List of Countries 
 * 7. lmain, lacdet : Master Voucher, Voucher Detials and FC details 
 *                   (In BOSCOPAC, FC purpose is not avilable, so we fixed as  "Provision of free clothing / food to the poor, needy and destitute")
 *    
 * Migration only for 2017-2018 (Migration startging from "01/04/2017")
 * 1. In BOSCOPAC, ledgers are defined based on Projects.
 * 2. Skip FD ledger and its vouchers. there are two levels of ledger group (CURRENT ASSET --> ASSET)
 * 3. Change Ledger Group "CASH-ON-HAND" as "Cash in hand" and ledger "CASH-ON-HAND" as "Cash".
 * 3. Consider "paid to" as  donor name in voucher entry, if "paid to" is empty fix donor name as ""
 * 4. Consider "Provision of free clothing / food to the poor, needy and destitute" as defualt purpose
 * 5. If Journal entry does affect Cash/bank ledger, consider as Cash/Bank Voucher (Receipts, Payments, Contra) Voucher
 * 6. If Cash/Bank Voucher (Receipts, Payments, Contra) does not affect cash/bank ledger, consider those vouchers as  Journal Voucer
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.Utility;
using AcMEDSync.Model;
using System.Data;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using System.IO;
using Bosco.Model.UIModel;

namespace Bosco.Model.TallyMigration
{
    public class BOSCOPACMigrationSystem : SystemBase
    {
        
        #region Variables
        private string boscopac_base_path = string.Empty;
        ResultArgs resultArgs = null;
        BalanceSystem balanceSystem = new BalanceSystem();

        //Defaults 
        public Int32 DEF_DIVISIONID = 1; //By default Account division is "Local"
        public string DEF_INVALID_GROUP = "Invalid BOSCOPAC Group";
        //private string DEF_DONORNAME = "Unknown";
        //private string DEF_PURPOSE = "Provision of free clothing / food to the poor, needy and destitute";
        //private string DEF_STATE = "Maharashtra MH";
        private DateTime BOSCOPAC_StartDate = DateTime.Parse("01/04/2017");

        

        //---------------------------------UI Updation---------------------------------
        public event EventHandler InitProgressBar;
        public event EventHandler IncreaseProgressBar;
        
        public bool DeleteUnusedLedger { get; set; }
        #endregion

        #region Properties
        private int BOSCOPAC_HouseId { get; set; }
        private string BOSCOPAC_Selected_Activities { get; set; }
        
        private string BOSCOPAC_ActivityName { get; set; }
        private Int32 BOSCOPAC_ActivityId{ get; set; }
        private int Acmeerp_ProjectId { get; set; }
                
        public DataTable dtHouse { get; set; }
        public DataTable dtActivty{ get; set; }
        public DataTable dtGroup { get; set; }
        public DataTable dtLedger { get; set; }
        public DataTable dtCountry { get; set; }
        public DataTable dtDonor { get; set; }
        public DataTable dtMasterVoucher { get; set; }
        public DataTable dtDetailVoucher { get; set; }

        private int CreatedBy { get; set; }
        private int ModifiedBy { get; set; }
        private string CreatedByName { get; set; }
        private string ModifiedByName { get; set; }
        #endregion

        public BOSCOPACMigrationSystem()
        {
            resultArgs = new ResultArgs();
        }

        public BOSCOPACMigrationSystem(string basepath):base()
        {
            boscopac_base_path = basepath;
        }

        /// <summary>
        /// Check Viusal FOX provider driver in local system
        /// </summary>
        /// <returns></returns>
        public ResultArgs CheckVFPOLEDBDriverInstalled()
        {
            ResultArgs resultarg = new ResultArgs();
            using (BOSCOPACConnector boscoconnectior = new BOSCOPACConnector(boscopac_base_path))
            {
                resultarg  = boscoconnectior.CheckValidBOSCOPACPath();
                if (resultarg.Success)
                {
                    resultarg = boscoconnectior.FetchHouses();
                }
            }
            return resultarg;
        }

        /// <summary>
        /// This method is used to check all the mandatory tables are exists in selected BOSCOPAC
        /// </summary>
        /// <returns></returns>
        public ResultArgs CheckValidBOSCOPACPath()
        {
            ResultArgs resultarg = new ResultArgs();
            using (BOSCOPACConnector boscoconnectior = new BOSCOPACConnector(boscopac_base_path))
            {
                resultarg = boscoconnectior.CheckValidBOSCOPACPath();
            }
            return resultarg;
        }

        /// <summary>
        /// Migrate from BOSCOPAC for the following detials
        /// 1. Load all the data from boscopac
        /// 2. Check Defualt legers are avialble in Acme.erp
        /// 3. Mirgate A/c Year (as of now we fiexd year as 2017-2018)
        /// 4. Mirgate list Project of selected projects
        /// 
        /// In BoscoPAC 
        /// 
        /// </summary>
        /// <param name="boscopachouseid"></param>
        /// <param name="boscopactivityid"></param>
        /// <returns></returns>
        public ResultArgs MigrateBOSCOPAC(int boscopachouseid, string boscopactivityid)
        {
            BOSCOPAC_HouseId = boscopachouseid;
            BOSCOPAC_Selected_Activities = boscopactivityid;
   
            resultArgs = LoadDataFromBOSCOPAC();
            if (resultArgs.Success)
            {
                try
                {
                    //On 26/08/2021, To fix User details by dfault ---------------------------------------------
                    CreatedBy = this.NumberSet.ToInteger(this.LoginUserId.ToString());
                    ModifiedBy = this.NumberSet.ToInteger(this.LoginUserId.ToString());

                    CreatedByName = FirstName;  //LoginUserName.ToString();
                    ModifiedByName = FirstName; //LoginUserName.ToString();
                    //------------------------------------------------------------------------------------------

                    SetDefaultLedgers();
                    MigrateAccountingYears();
                    MigrateProjectInfo();
                    MigrateLedgerGroup();
                    bool refreshop = false;
                    foreach (DataRow dr in dtActivty.Rows)
                    {
                        BOSCOPAC_ActivityName = dr["activity"].ToString().Trim();
                        BOSCOPAC_ActivityName = BOSCOPAC_ActivityName.Length > 100 ? BOSCOPAC_ActivityName.Substring(0, 100) : BOSCOPAC_ActivityName; //Database only accepts 100 char for Project Name
                        BOSCOPAC_ActivityId = NumberSet.ToInteger(dr["tcode"].ToString().Trim());
                        if (!string.IsNullOrEmpty(BOSCOPAC_ActivityName))
                        {
                            Acmeerp_ProjectId = GetProjectId();
                            if (Acmeerp_ProjectId > 0)
                            {
                                //Map Default Cash, FD legers
                                Int32 DefaultCashLedger = (Int32)DefaultLedgers.Cash;
                                Int32 DefaultFDLedger = (Int32)DefaultLedgers.FixedDeposit;
                                Int32 DefaultCaptialFundLedger = (Int32)DefaultLedgers.CapitalFund;
                                MapLedger(DefaultCashLedger, 0);
                                MapLedger(DefaultFDLedger, 0);
                                MapLedger(DefaultCaptialFundLedger, 0);

                                MigrateLedger();
                                MigrateCountry();
                                MigrateDonorInformation();
                                MigrateVouches();
                                refreshop = true;
                            }
                        }
                    }
                    if (refreshop)
                    {
                        ExecuteFlushCommands();
                        UpdateLedgerOpeningBalanceDate();
                    }
                    SetProgressBar("Migration is completed", 0);
                    resultArgs.Message = "Migration is completed";
                    resultArgs.Success = true;
                }
                catch (Exception err)
                {
                    resultArgs.Message = "Problem in Migration " + err.Message;
                }
            }
            else
            {
                resultArgs.Message = "Could not load mandatory data from BOSCOPAC";
            }
            return resultArgs;
        }       
        
       #region PreChecking for Migration
        public bool IsBOSCOPACMigrationMade(string boscopacactivties, Int32  division)
        {
            bool Projectexists = false;
            using (AcMePlusMigrationSystem getProject = new AcMePlusMigrationSystem(MigrationType.BOSCOPAC))
            {
                int ProjectId = getProject.GetProjectId(boscopacactivties, division);
                Projectexists = (ProjectId > 0);
            }
            return Projectexists;
        }

        public void SetDefaultLedgers()
        {
            using (AcMePlusMigrationSystem AcMePlusMigration = new AcMePlusMigrationSystem(MigrationType.BOSCOPAC))
            {
                AcMePlusMigration.SetDefaultLedgers();
            }

            //In BOSCOPAC Group is not avilable but ledger is mapped with that group and made transactions
            //In BoscoPAC, group is deleted from group list, but it has ledgers and vouchers
            //so if group is not found in group list, we create those groups under our default group called "Invalid BOSCOPAC Group"
            InsertLedgerGroup(DEF_INVALID_GROUP, "Expenses");
        }


        /// <summary>
        /// On 24/09/2021, 
        /// </summary>
        /// <returns></returns>
        public bool IsAuditVouchersLockedVoucherDate(string boscopacactivties, Int32 division)
        {
            bool rnt = true;
            ResultArgs result = new ResultArgs();
            AcMELog.WriteLog("Check Audit Voucher Lock Started..");
            DateTime frmDate = BOSCOPAC_StartDate;
            DateTime toDate = frmDate.AddMonths(12).AddDays(-1);

            result.Success = true;
            try
            {
                int projectid = 0;
                string projectname = boscopacactivties + ((division == 2 && boscopacactivties.Trim().ToUpper() != "FOREIGN") ? " (F)" : string.Empty);
                using (AcMePlusMigrationSystem getProject = new AcMePlusMigrationSystem(MigrationType.BOSCOPAC))
                {
                    projectid = getProject.GetProjectId(projectname, division);
                }

                using (DataManager datamanager = new DataManager(SQLCommand.AuditLockTrans.FetchAuditLockDetailByProjectDateRange))
                {
                    datamanager.Parameters.Add(this.AppSchema.Voucher.PROJECT_IDColumn, projectid);
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

        #endregion

       #region Clear Previous Data
        public void RemovePriviousMigration(string boscopacactivties, Int32 division)
        {
            int projectid = 0;
            using (AcMePlusMigrationSystem getProject = new AcMePlusMigrationSystem(MigrationType.BOSCOPAC))
            {
                projectid = getProject.GetProjectId(boscopacactivties, division);
            }

            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.ClearData))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, projectid);
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

       /// <summary>
        /// Get list of houses from boscopac
        /// </summary>
        /// <returns></returns>
       public ResultArgs GetHouses()
        {
            ResultArgs resultarg = new ResultArgs();
            using (BOSCOPACConnector boscoconnectior = new BOSCOPACConnector(boscopac_base_path))
            {
               resultarg =  boscoconnectior.FetchHouses();
            }
            return resultarg;
        }

       /// <summary>
        /// GEt list of Projects from boscopac
        /// </summary>
        /// <returns></returns>
       public ResultArgs GetActivity()
        {
            ResultArgs resultarg = new ResultArgs();
            using (BOSCOPACConnector boscoconnectior = new BOSCOPACConnector(boscopac_base_path))
            {
               resultarg =  boscoconnectior.FetchActivities();
            }
            return resultarg;
        }

       #region Migrate Accounting Years
        private void MigrateAccountingYears()
        {

            DateTime deYearFrom = BOSCOPAC_StartDate.AddYears(-1);
            int YearCount = DateTime.Now.Year - BOSCOPAC_StartDate.Year;
            YearCount = DateTime.Now.Month > 3 ? YearCount + 1 : YearCount;
            SetProgressBar("Migrating Accounting Year(s)", YearCount);
            for (int i = 1; i <= YearCount; i++)
            {
                deYearFrom = deYearFrom.AddYears(1);
                if (IsAcYearNotExists(deYearFrom.ToShortDateString(), deYearFrom.AddMonths(12).AddDays(-1).ToShortDateString()))
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateAcYears))
                    {
                        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        int Status = 0;
                        dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, deYearFrom.ToShortDateString());
                        dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, deYearFrom.AddMonths(12).AddDays(-1));
                        dataManager.Parameters.Add(AppSchema.AccountingPeriod.BOOKS_BEGINNING_FROMColumn, BOSCOPAC_StartDate);
                        dataManager.Parameters.Add(AppSchema.AccountingPeriod.STATUSColumn, Status);
                        dataManager.Parameters.Add(AppSchema.AccountingPeriod.IS_FIRST_ACCOUNTING_YEARColumn, Status);
                        resultArgs = dataManager.UpdateData();
                    }
                }
                ChangeProgressBarStatus();
            }
            SetActiveAccountYear();
        }

        private bool IsAcYearNotExists(string DateFrom, string DatTo)
        {
            int AcYearCount = 0;
            using (AcMePlusMigrationSystem findAcYear = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                AcYearCount = findAcYear.FindAccountingYear(DateFrom, DatTo);
            }
            return AcYearCount == 0;
        }

        private void SetActiveAccountYear()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.SetActiveAccountingYear))
            {
                using (AcMePlusMigrationSystem firstAcYear = new AcMePlusMigrationSystem(MigrationType.Tally))
                {
                    DateTime NewBooksBeginningDate =  BOSCOPAC_StartDate;
                    int firstacyear = firstAcYear.GetFirstAccountingYearId();
                    DateTime deBookBeginningFrom = GetLeastBookBeginningFromDate() ?? BOSCOPAC_StartDate;
                    if (DateTime.Compare(deBookBeginningFrom, BOSCOPAC_StartDate) <= 0)
                        NewBooksBeginningDate = deBookBeginningFrom;

                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.ACC_YEAR_IDColumn, firstacyear);
                    dataManager.Parameters.Add(AppSchema.AccountingPeriod.BOOKS_BEGINNING_FROMColumn, NewBooksBeginningDate.ToShortDateString());
                    resultArgs = dataManager.UpdateData();

                }
            }
        }

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
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, BOSCOPAC_StartDate);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        #endregion

       #region Migrate Project
        private void MigrateProjectInfo()
        {
            //For selected Projects alone
            dtActivty.DefaultView.RowFilter = "code = " + BOSCOPAC_HouseId + " AND tcode in (" + BOSCOPAC_Selected_Activities + ")";
            dtActivty = dtActivty.DefaultView.ToTable();

            SetProgressBar("Migrating Projects", dtActivty.Rows.Count);
            foreach (DataRow drItem in dtActivty.Rows)
            {
                //------------------Get Default Project Category Id if exists else Insert and get the default project category id------------------
                BOSCOPAC_ActivityName = drItem["activity"].ToString().Trim();
                int CategoryId = SetDefaultProjectCategory();
                int DivisionId = DEF_DIVISIONID;
                if (!string.IsNullOrEmpty(BOSCOPAC_ActivityName))
                {
                    if (IsProjectNotExists())
                    {
                        using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.MigrateProject))
                        {
                            dataManager.Parameters.Add(AppSchema.Project.PROJECT_CODEColumn, GetProjectCode(), true);
                            dataManager.Parameters.Add(AppSchema.Project.PROJECTColumn, BOSCOPAC_ActivityName);
                            dataManager.Parameters.Add(AppSchema.Project.DIVISION_IDColumn, DivisionId);
                            dataManager.Parameters.Add(AppSchema.Project.ACCOUNT_DATEColumn, BookBeginFrom);
                            dataManager.Parameters.Add(AppSchema.Project.DATE_STARTEDColumn, BookBeginFrom);
                            dataManager.Parameters.Add(AppSchema.Project.DATE_CLOSEDColumn, null);
                            dataManager.Parameters.Add(AppSchema.Project.DESCRIPTIONColumn, string.Empty);
                            dataManager.Parameters.Add(AppSchema.Project.PROJECT_CATEGORY_IDColumn, CategoryId);
                            resultArgs = dataManager.UpdateData();
                            if (resultArgs != null && resultArgs.Success)
                            {
                                Acmeerp_ProjectId = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                //Map Project Voucher
                                MapProjectVoucher();
                            }
                        }
                    }
                    else
                    {
                        //Getting Existing Project Id for furthur migration
                        Acmeerp_ProjectId = GetProjectId();
                    }
                }
                ChangeProgressBarStatus();
            }
        }

        private void MapProjectVoucher()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.MapProjectVoucher))
            {
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, Acmeerp_ProjectId);
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
                Code = String.Format("{0}{1}", BOSCOPAC_ActivityName.Substring(0, 2), ++Count);
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

        private bool IsProjectNotExists()
        {
            return GetProjectId() == 0;
        }
        #endregion

       #region Ledger Group
        private void MigrateLedgerGroup()
        {
            if (dtGroup != null)
            {
                SetProgressBar("Migrating Ledger Group", dtGroup.Rows.Count);
                foreach (DataRow dritem in dtGroup.Rows)
                {
                    //MigrateLedgerGroupByCategory(dritem);
                    string groupname = dritem["Account"].ToString().Trim();
                    string parentname = dritem["group"].ToString().Trim();
                    if (!string.IsNullOrEmpty(groupname) && !string.IsNullOrEmpty(parentname))
                    {
                        InsertLedgerGroup(groupname, parentname);
                    }
                    ChangeProgressBarStatus();
                }
            }
        }
        
        private Int32 InsertLedgerGroup(string GroupName, string ParentName)
        {
            Int32 InsertedGroupId = 0;
            Int32 AccessFlag = 0;
            //-------------------- Query need to be changed----------------------------------
            //string GroupName = dritem["Account"].ToString().Trim();
            //string ParentName = dritem["group"].ToString().Trim();
            GroupName = GroupName.Length > 100 ? GroupName.Substring(0, 100) : GroupName; //Database only accepts 100 char for Ledger Group Name

            using (AcMePlusMigrationSystem AcMePlusMigration = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                int NatureId = AcMePlusMigration.GetGroupNatureId(ParentName);
                int LedgerGroupId = GetLedgerGroupId(ParentName);
                int GroupIdExists = AcMePlusMigration.GetLedgerGroupId(GroupName);
                //Checking if the parents is already available
                if (AcMePlusMigration.GetLedgerGroupId(GroupName) == 0)
                {
                    string GroupCode = GenerarteLedgerGroupCode();
                    //Checking if the Group is already available
                    if (GroupIdExists == 0)
                    {
                        using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateLedgerGroup))
                        {
                            //To Insert parent Group
                            dataManager.Parameters.Add(AppSchema.LedgerGroup.GROUP_CODEColumn, GroupCode, true);
                            dataManager.Parameters.Add(AppSchema.LedgerGroup.LEDGER_GROUPColumn, GroupName);
                            dataManager.Parameters.Add(AppSchema.LedgerGroup.PARENT_GROUP_IDColumn, LedgerGroupId);
                            dataManager.Parameters.Add(AppSchema.LedgerGroup.NATURE_IDColumn, NatureId);
                            dataManager.Parameters.Add(AppSchema.LedgerGroup.MAIN_GROUP_IDColumn, LedgerGroupId); 
                            dataManager.Parameters.Add(AppSchema.LedgerGroup.ACCESS_FLAGColumn, AccessFlag);
                            resultArgs = dataManager.UpdateData();
                            if (resultArgs != null && resultArgs.Success)
                            {
                                InsertedGroupId = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                            }
                        }
                    }
                }
            }

            return InsertedGroupId;
        }
        
        private string GenerarteLedgerGroupCode()
        {
            string GroupCode = string.Empty;
            using (AcMePlusMigrationSystem AcMePlusMigration = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                int Count = AcMePlusMigration.GenerateLedgerGroupCode();
                GroupCode = String.Format("LG{0}", ++Count);
            }
            return GroupCode;
        }
        #endregion
        
       #region Migrate  Ledger
        private void MigrateLedger()
        {
            if (dtLedger != null)
            {
                //Get ledger only for selected activities from boscopac
                dtLedger.DefaultView.RowFilter = string.Empty;
                dtLedger.DefaultView.RowFilter = "hcode = " + BOSCOPAC_HouseId + " AND tcode = " + BOSCOPAC_ActivityId + " AND group <> 'FIXED DEPOSITS'";
                DataTable dtProjectLedger = dtLedger.DefaultView.ToTable();
                SetProgressBar(BOSCOPAC_ActivityName  + " (Migrating Ledgers)", dtProjectLedger.Rows.Count);

                foreach (DataRow drledger in dtProjectLedger.Rows)
                {
                    string LedgerCodeId = drledger["code"].ToString().Trim();
                    string LedgerName = drledger["account"].ToString().Trim(); //drledger["LedgerName"].ToString();
                    LedgerName = LedgerName.Length > 100 ? LedgerName.Substring(0, 100) : LedgerName; //Database only accepts 100 char for Ledger Name

                    if (LedgerName.ToUpper() == "FIXED DEPOSIT")
                    {

                    }

                    string ParantGroup = drledger["group"].ToString().Trim();
                    double OPBalance = 0;
                    //string PrimaryGroup = drledger["group"].ToString().Trim();
                    string LedgerSubType = ParantGroup.ToUpper().Equals("BANK ACCOUNTS") ? "BK" : "GN";
                    OPBalance = NumberSet.ToDouble(drledger["amount"].ToString());

                    if (drledger["dbcr"].ToString().Trim().ToUpper() == "CR")
                    {
                        OPBalance = -OPBalance; 
                    }
                    
                    //-------------------------------------------------------------------------------------------------------------------
                    if (!ParantGroup.ToUpper().Equals("FIXED DEPOSITS"))
                    {
                        //if (LedgerName == "INC/EXP APPRON A/C1314" || LedgerName == "INC/EXP APPRON A/C1516" || LedgerName == "ARUL'S LOAN A/C")
                        if (LedgerName =="INC/EXP APPRON A/C0607")
                        {

                        }

                        if (IsLedgerNotExists(LedgerName))
                        {
                            bool voucherExists = IsVoucherExistsforLedger(LedgerName);

                            //TRUST/CORPUS FUND
                            if (ParantGroup.ToUpper() != "TRUST/CORPUS FUND" || voucherExists)
                            {
                                string LedgerType = "GN";
                                int IsCostCenter = 0; //NumberSet.ToInteger(drledger["IsCostCentresOn"].ToString());
                                int GroupId = GetLedgerGroupId(ParantGroup);

                                //In BoscoPAC, group is deleted from group list, but it has ledgers and vouchers
                                //so if group is not found in group list, we create those groups under our default group called "Invalid BOSCOPAC Group"
                                if (GroupId == 0)
                                {
                                    if (voucherExists || OPBalance != 0)
                                    {
                                        GroupId = InsertLedgerGroup(ParantGroup, DEF_INVALID_GROUP);
                                    }
                                }

                                if (GroupId > 0)
                                {
                                    string ledgercode = GetLedgerCode();
                                    using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.MigrateLedger))
                                    {
                                        dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_CODEColumn, ledgercode, true);
                                        dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_NAMEColumn, LedgerName);
                                        dataManager.Parameters.Add(AppSchema.Ledger.GROUP_IDColumn, GroupId);
                                        dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_TYPEColumn, LedgerType);
                                        dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_SUB_TYPEColumn, LedgerSubType);
                                        dataManager.Parameters.Add(AppSchema.Ledger.IS_COST_CENTERColumn, IsCostCenter);
                                        resultArgs = dataManager.UpdateData();
                                        if (resultArgs != null && resultArgs.Success)
                                        {
                                            Int32 LedgerId = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                            if (LedgerSubType.Equals("BK"))
                                            {
                                                //(Treat Ledger Name as Account No and Bank Name)
                                                string BankName = drledger["account"].ToString().Trim(); //drledger["LedgerName"].ToString();
                                                string Branch = string.Empty; //drledger["Branch"].ToString().Trim(); //drledger["BankBranchName"].ToString();

                                                //Migrate Branch and Bank Account (Treat Ledger Name as Account No and Bank Name)
                                                MigrateBankAccount(LedgerId, BankName, BankName, BOSCOPAC_StartDate.ToShortDateString(), Branch, ledgercode);
                                            }
                                            //----------------------Mapping the Ledger to the Project with Opening Balance ---------------------
                                            MapLedger(LedgerId, OPBalance);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            MapLedger(GetLedgerId(LedgerName), OPBalance);
                        }
                    }
                    ChangeProgressBarStatus();
                }
            }

        }

        private int GetLedgerId(string LedgerName)
        {
            int TempLedgerId = 0;
            using (AcMePlusMigrationSystem AcMePlusMigration = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                TempLedgerId = AcMePlusMigration.GetLedgerId(LedgerName);
            }
            return TempLedgerId;
        }

        private void MapLedger(int LedgerId, double OPBalance)
        {
            //Mapping Ledger with the Project
            if (IsProjectLedgerNotMapped(LedgerId))
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectLedgerMappingAdd))
                {
                    dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                    dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, Acmeerp_ProjectId);
                    resultArgs = dataManager.UpdateData();
                }
            }

            //Updating Opening Balance if it has Opening Balance
            if (OPBalance != 0)
            {
                using (AcMePlusMigrationSystem AcMePlusMigration = new AcMePlusMigrationSystem(MigrationType.AcMePlus))
                {
                    AcMePlusMigration.ProjectId = Acmeerp_ProjectId;
                    AcMePlusMigration.OPDate = DateSet.ToDate(BookBeginFrom, false).AddDays(-1);
                    AcMePlusMigration.BankLedgerId = LedgerId;
                    AcMePlusMigration.OPBankBalance = OPBalance;
                    AcMePlusMigration.UpdateBankOpeningBalance(MigrationType.AcMePlus);
                }
            }
        }

        private bool IsProjectLedgerNotMapped(int LedgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.IsProjectLedgerMapped))
            {
                dataManager.Parameters.Add(AppSchema.LedgerBalance.PROJECT_IDColumn, Acmeerp_ProjectId);
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

        private int GetBankId(string BankName, string BrachName)
        {
            int ExistingBankId = 0;
            using (AcMePlusMigrationSystem AcMePlusMigration = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                ExistingBankId = AcMePlusMigration.FindBankId(BankName, BrachName);
            }
            return ExistingBankId;
        }

        private int MigrateMasterBank(string BankName, string Branch, string Address, string IFSCode)
        {
            BankName = BankName.Length > 50 ? BankName.Substring(0, 50) : BankName; //Database only accepts 50 char for AccountNo
            Int32 bankid = GetBankId(BankName, Branch);

            if (bankid == 0)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.MigrateMasterBank))
                {
                    dataManager.Parameters.Add(AppSchema.Bank.BANK_CODEColumn, GetBankCode(), true);
                    dataManager.Parameters.Add(AppSchema.Bank.BANKColumn, BankName);
                    dataManager.Parameters.Add(AppSchema.Bank.BRANCHColumn, Branch);
                    dataManager.Parameters.Add(AppSchema.Bank.ADDRESSColumn, Address);
                    dataManager.Parameters.Add(AppSchema.Bank.IFSCCODEColumn, IFSCode);
                    resultArgs = dataManager.UpdateData();
                }
                if (resultArgs.Success)
                {
                    bankid = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                }
            }
            return bankid;
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

        private void MigrateBankAccount(Int32 LedgerId, string BankName, string AccountNo, string DateOpened, string Branch, string ledgercode)
        {
            Int32 BankId = MigrateMasterBank(BankName, Branch, string.Empty, string.Empty);
            AccountNo = AccountNo.Length > 50 ? AccountNo.Substring(0, 50) : AccountNo; //Database only accepts 50 char for AccountNo
            if (BankId > 0)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateBankAccount))
                {
                    dataManager.Parameters.Add(AppSchema.BankAccount.LEDGER_IDColumn, LedgerId);
                    dataManager.Parameters.Add(AppSchema.BankAccount.ACCOUNT_CODEColumn, ledgercode); //GetBankAccountCode()
                    dataManager.Parameters.Add(AppSchema.BankAccount.ACCOUNT_NUMBERColumn, AccountNo);
                    dataManager.Parameters.Add(AppSchema.BankAccount.BANK_IDColumn, BankId);
                    dataManager.Parameters.Add(AppSchema.BankAccount.DATE_OPENEDColumn, DateOpened);
                    dataManager.Parameters.Add(AppSchema.BankAccount.DATE_CLOSEDColumn, null);
                    dataManager.Parameters.Add(AppSchema.BankAccount.ACCOUNT_TYPE_IDColumn, 1);
                    resultArgs = dataManager.UpdateData();
                }
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

        #endregion

       #region Migrate Country
        private void MigrateCountry()
        {
            
            if (dtCountry != null)
            {
                dtCountry.DefaultView.RowFilter = string.Empty;
                dtCountry.DefaultView.RowFilter =  "hcode = " + BOSCOPAC_HouseId + " AND tcode = " + BOSCOPAC_ActivityId;
                DataTable dtProjectCountry  = dtCountry.DefaultView.ToTable();
                SetProgressBar(BOSCOPAC_ActivityName + " (Migrating Countries)", dtProjectCountry.Rows.Count);

                foreach (DataRow drItem in dtProjectCountry.Rows)
                {
                    string CountryName = drItem["Country"].ToString().Trim();
                    string currency = drItem["fcurr"].ToString().Trim();
                    CountryName = CountryName.Length > 50 ? CountryName.Substring(0, 50) : CountryName; //Database only accepts 100 char for CountryName

                    if (IsCountryNotExists(CountryName))
                    {
                        using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateCountry))
                        {
                            dataManager.Parameters.Add(AppSchema.Country.COUNTRYColumn, CountryName);
                            dataManager.Parameters.Add(AppSchema.Country.COUNTRY_CODEColumn, GenerateCountryCode());
                            dataManager.Parameters.Add(AppSchema.Country.CURRENCY_SYMBOLColumn, currency);
                            resultArgs = dataManager.UpdateData();
                        }
                    }
                    ChangeProgressBarStatus();
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
        
        #region Migrate Donor
        private void MigrateDonorInformation()
        {
            if (dtDonor != null)
            {
                dtDonor.DefaultView.RowFilter = string.Empty;
                dtDonor.DefaultView.RowFilter = "hcode = " + BOSCOPAC_HouseId + " AND tcode = " + BOSCOPAC_ActivityId;
                DataTable dtProjectDonor = dtDonor.DefaultView.ToTable();
                SetProgressBar(BOSCOPAC_ActivityName + " (Migrating Donor)", dtProjectDonor.Rows.Count);

                foreach (DataRow drItem in dtProjectDonor.Rows)
                {
                    int CountryId = GetCountryId(drItem["COUNTRY"].ToString().Trim());
                    string DonorName = drItem["PAIDTO"].ToString().Trim();
                    //if (String.IsNullOrEmpty(DonorName ))  DonorName = DEF_DONORNAME;
                    DonorName = DonorName.Length > 150 ? DonorName.Substring(0, 150) : DonorName; //Database only accepts 150 char for donorname
                    double contributionamount = NumberSet.ToDouble(drItem["fcamount"].ToString());

                    if (!string.IsNullOrEmpty(DonorName) && CountryId > 0 && contributionamount > 0)
                    {
                        if (IsDonorNotExists(DonorName, CountryId))
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.MigrateDonorAuditor))
                            {
                                int StateId = 0; //GetStateId(DEF_STATE); //Default State Id (Tamil Nadu);
                                int DonorIdentity = 0;             //0-Donor and 1-Auditor
                                int DonorType = drItem["IOTYPE"].ToString().Equals("O") ? 1 : 2; //1-Instutional  2-Indiviual
                                string Address = drItem["fadd1"].ToString().Trim() + "," + drItem["fadd2"].ToString().Trim() + "," +
                                                 drItem["fadd3"].ToString().Trim() + "," + drItem["fadd4"].ToString().Trim();
                                Address = Address.Length > 100 ? Address.Substring(0, 100) : Address; //Database only accepts 100 char for address
                                Address = Address.TrimEnd(','); 

                                dataManager.Parameters.Add(AppSchema.DonorAuditor.NAMEColumn, DonorName, true);
                                dataManager.Parameters.Add(AppSchema.DonorAuditor.TYPEColumn, DonorType);
                                dataManager.Parameters.Add(AppSchema.State.STATE_IDColumn, StateId); 
                                dataManager.Parameters.Add(AppSchema.DonorAuditor.COUNTRY_IDColumn, CountryId);
                                dataManager.Parameters.Add(AppSchema.DonorAuditor.ADDRESSColumn, Address);
                                dataManager.Parameters.Add(AppSchema.DonorAuditor.PHONEColumn, string.Empty);
                                dataManager.Parameters.Add(AppSchema.DonorAuditor.EMAILColumn, string.Empty);
                                dataManager.Parameters.Add(AppSchema.DonorAuditor.IDENTITYKEYColumn, DonorIdentity);
                                resultArgs = dataManager.UpdateData();
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    int DonorId = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                    //------------------------------Map Project Donor----------------------------------
                                    MapDonorProject(DonorId);
                                }
                            }
                        }
                        else
                        {
                            //------------------------------Map Project Donor----------------------------------
                            MapDonorProject(GetDonorId(DonorName));
                        }
                    }
                    ChangeProgressBarStatus();
                }
            }
        }
        
        private int GetStateId(string StateName)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.GetStateId))
            {
                dataManager.Parameters.Add(AppSchema.State.STATE_NAMEColumn, StateName);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        private void MapDonorProject(int DonorId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MapProjectDonor))
            {
                dataManager.Parameters.Add(AppSchema.VoucherMaster.DONOR_IDColumn, DonorId);
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, Acmeerp_ProjectId);
                resultArgs = dataManager.UpdateData();
            }
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

        #region Migrate Vouchers
        private void MigrateVouches()
        {
            if (dtMasterVoucher != null)
            {
                dtMasterVoucher.DefaultView.RowFilter = string.Empty;
                dtMasterVoucher.DefaultView.Sort= string.Empty;
                dtMasterVoucher.DefaultView.RowFilter = "hcode = " + BOSCOPAC_HouseId + " AND tcode = " + BOSCOPAC_ActivityId;
                dtMasterVoucher.DefaultView.Sort = "vdate, vtype, vno";
                DataTable dtProjectMasterVoucher = dtMasterVoucher.DefaultView.ToTable();
                SetProgressBar(BOSCOPAC_ActivityName + " (Migrating Vouchers)", dtProjectMasterVoucher.Rows.Count);

                foreach (DataRow drItem in dtProjectMasterVoucher.Rows)
                {
                    string VoucherSubType = "GN";
                    string voucherdate = DateSet.ToDate(drItem["vdate"].ToString(),false).ToShortDateString();
                    int genno = NumberSet.ToInteger(drItem["genno"].ToString());
                    int vno = NumberSet.ToInteger(drItem["vno"].ToString());
                    string chequeno = drItem["chequeno"].ToString().Trim();
                    string chqdate = drItem["chqdate"].ToString().Trim();
                    if (DateSet.ToDate(chqdate) == "30/12/1899") chqdate = string.Empty; //30/12/1899 is null or empty date in BOSCOPAC
                    string vtype = drItem["vtype"].ToString().Trim();
                    string paidto = drItem["paidto"].ToString().Trim();
                    string narration = drItem["NARRATION"].ToString().Trim().Contains("\"") ? drItem["NARRATION"].ToString().Trim().Replace('"', '\'') : drItem["NARRATION"].ToString().Trim();
                    narration = narration.Length > 500 ? narration.Substring(0, 500) : narration; //Database only accepts 500 char for narration
                    paidto = paidto.Length > 100 ? paidto.Substring(0, 100) : paidto; //Database only accepts 100 char for paidto

                    //Check Current voucher contains cash/bank ledger, in bosco pac few vouchers (R,P,C) doenst contains cash or bank ledger
                    //so we consider those vouchers as journal vouchers
                    bool isContainsCashBankLedger = IsVoucherContainsCASHBANKLedger(genno, voucherdate, vtype);

                    //If Current Voucher contains (Fixed Deposit ledger) skip those vouchers
                    bool isContainsFD = IsVoucherContainsFD(genno, voucherdate, vtype);

                    //If Current Voucher contains invalid child voucher detials like..does not have child entries..skip those vouchers
                    bool isValidChildVoucherDetail = IsVoucherContainsValidChildVouchers(genno, voucherdate, vtype);

                    if (!isContainsFD && isValidChildVoucherDetail)
                    {
                        string VoucherType = string.Empty;
                        Int32 VoucherDefinitionId = (Int32)DefaultVoucherTypes.Receipt;
                        switch (vtype)
                        {
                            case "CP":
                            case "BP":    //Payments
                                VoucherType = "PY";
                                VoucherDefinitionId = (int)DefaultVoucherTypes.Payment;
                                //If vtype is CP,BP or CR, BR vouchers doesnot contain CASH or BANK Legers, consider those vouchers as JOURNAL ENTRY
                                if (!isContainsCashBankLedger)
                                {
                                    VoucherType = "JN";
                                    VoucherDefinitionId = (int)DefaultVoucherTypes.Journal;
                                }
                                break;
                            case "CR":
                            case "BR":   //"Receipt":
                                VoucherType = "RC";
                                VoucherDefinitionId = (int)DefaultVoucherTypes.Receipt;

                                //If vtype is CP,BP or CR, BR vouchers doesnot contain CASH or BANK Legers, consider those vouchers as JOURNAL ENTRY
                                if (!isContainsCashBankLedger)
                                {
                                    VoucherType = "JN";
                                    VoucherDefinitionId = (int)DefaultVoucherTypes.Journal;
                                }
                                break;
                            case "CC":              //"Contra"
                                VoucherType = "CN";
                                VoucherDefinitionId = (int)DefaultVoucherTypes.Contra;
                                break;
                            case "JV":              //"Journal"
                                VoucherType = "JN";
                                VoucherDefinitionId = (int)DefaultVoucherTypes.Journal;

                                if (isContainsCashBankLedger)
                                {
                                    bool isReceiptVouhcer = IsVoucherReceipt(genno, voucherdate, vtype);
                                    VoucherType = (isReceiptVouhcer ? "RC" : "PY");
                                    VoucherDefinitionId = (isReceiptVouhcer ? (int)DefaultVoucherTypes.Receipt : (int)DefaultVoucherTypes.Payment);
                                }
                                break;
                        }

                        using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.MigrateVoucherMasterWithNameAddress))
                        {
                            dataManager.BeginTransaction();
                            dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_DATEColumn, voucherdate, true);
                            dataManager.Parameters.Add(AppSchema.VoucherMaster.PROJECT_IDColumn, Acmeerp_ProjectId);
                            dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_NOColumn, vno);
                            dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_TYPEColumn, VoucherType);
                            dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_SUB_TYPEColumn, VoucherSubType);
                            dataManager.Parameters.Add(AppSchema.VoucherMaster.NARRATIONColumn, narration);
                            dataManager.Parameters.Add(AppSchema.VoucherMaster.NAME_ADDRESSColumn, paidto);
                            dataManager.Parameters.Add(AppSchema.VoucherMaster.CREATED_BYColumn, CreatedBy);
                            dataManager.Parameters.Add(AppSchema.VoucherMaster.CREATED_BY_NAMEColumn, CreatedByName);
                            //dataManager.Parameters.Add(AppSchema.VoucherMaster.MODIFIED_BYColumn, NumberSet.ToInteger(LoginUserId));
                            dataManager.Parameters.Add(AppSchema.VoucherNumber.VOUCHER_DEFINITION_IDColumn, VoucherDefinitionId);
                            resultArgs = dataManager.UpdateData();
                            if (resultArgs != null && resultArgs.Success)
                            {
                                int NewVoucherId = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());

                                //Making Voucher Transaction details for the Ledgers
                                resultArgs = UpdateVoucherDetails(dataManager, genno, NewVoucherId, voucherdate, vtype, chequeno, chqdate);
                                
                                //Migrate Donor details too
                                if (resultArgs.Success)
                                {
                                    resultArgs = UpdateDonorTransDetails(dataManager, drItem, NewVoucherId);
                                }
                                else
                                {

                                }
                            }
                            dataManager.EndTransaction();
                        }
                    }
                    ChangeProgressBarStatus();
                }
            }
        }

        private ResultArgs UpdateDonorTransDetails(DataManager dmActive, DataRow drMasterVoucherItem, Int32 VoucherId)
        {
            if (drMasterVoucherItem != null && VoucherId > 0)
            {
                string donorname = drMasterVoucherItem["paidto"].ToString().Trim();
                //if (string.IsNullOrEmpty(donorname))   donorname = DEF_DONORNAME;
                Int32 donorid = GetDonorId(donorname);
                double contributionamount = NumberSet.ToDouble(drMasterVoucherItem["fcamount"].ToString());
                int countryid = GetCountryId(drMasterVoucherItem["country"].ToString().Trim());
                
                if (contributionamount > 0)
                {
                    Int32 purposeId = 0;// GetPurposeId(DEF_PURPOSE); No Purpose in BOSCOPAC
                    double actualamount = NumberSet.ToDouble(drMasterVoucherItem["iamount"].ToString()); ;

                    //Update FC voucher details 
                    using (DataManager dataManager = new DataManager(SQLCommand.TallyMigration.UpdateDonorTransaction))
                    {
                        dataManager.Database = dmActive.Database;
                        dataManager.Parameters.Add(AppSchema.VoucherMaster.DONOR_IDColumn, donorid);
                        dataManager.Parameters.Add(AppSchema.VoucherMaster.CONTRIBUTION_AMOUNTColumn, Math.Abs(contributionamount));
                        dataManager.Parameters.Add(AppSchema.VoucherMaster.CALCULATED_AMOUNTColumn, Math.Abs(actualamount));
                        dataManager.Parameters.Add(AppSchema.VoucherMaster.ACTUAL_AMOUNTColumn, Math.Abs(actualamount));
                        dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                        dataManager.Parameters.Add(AppSchema.VoucherMaster.PURPOSE_IDColumn, purposeId);
                        resultArgs = dataManager.UpdateData();
                    }

                    //Mapp Purpose to current project
                    if (purposeId > 0)
                    {
                        if (IsProjectPurposeNotMapped(Acmeerp_ProjectId, purposeId))
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MapPurpose))
                            {
                                dataManager.Parameters.Add(AppSchema.VoucherMaster.PURPOSE_IDColumn, purposeId);
                                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, Acmeerp_ProjectId);
                                dataManager.UpdateData();
                            }
                        }
                    }

                }
            }
            return resultArgs;
        }

        private ResultArgs UpdateVoucherDetails(DataManager dmActive, int Genno, int NewVoucherId, string VoucherDate, string VType, string ChequeNo, string MaterializedOn)
        {
            if (dtDetailVoucher != null)
            {
                dtDetailVoucher.DefaultView.RowFilter = string.Empty;
                DataView dvVoucherTrans = new DataView(dtDetailVoucher);
                dvVoucherTrans.RowFilter = string.Empty;
                dvVoucherTrans.RowFilter = "hcode = " + BOSCOPAC_HouseId+ " AND tcode = " +  BOSCOPAC_ActivityId  + " AND " + 
                                            "genno =" + Genno + " AND vdate= '" + VoucherDate + "' AND vtype='" + VType + "'";
                DataTable dtVoucherTransFiltered = dvVoucherTrans.ToTable();
                if (dtDetailVoucher != null)
                {
                    int VoucherTransSequenceNo = 1;
                    foreach (DataRow drItem in dtVoucherTransFiltered.Rows)
                    {
                        string ledgername = drItem["acchead"].ToString().Trim();
                        int LedgerId = GetLedgerId(MapLedgerName(ledgername));
                        if (LedgerId > 0)
                        {
                            double debit = NumberSet.ToDouble(drItem["debit"].ToString());
                            double credit = NumberSet.ToDouble(drItem["credit"].ToString());
                            double Amount = credit > 0 ? credit : debit;
                            string TransMode = credit > 0 ? "CR" : "DR";

                            using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateVoucherTransWithChequeNo))
                            {
                                dataManager.Database = dmActive.Database;
                                dataManager.Parameters.Add(AppSchema.VoucherTransaction.VOUCHER_IDColumn, NewVoucherId);
                                dataManager.Parameters.Add(AppSchema.VoucherTransaction.SEQUENCE_NOColumn, VoucherTransSequenceNo);
                                dataManager.Parameters.Add(AppSchema.VoucherTransaction.LEDGER_IDColumn, LedgerId);
                                dataManager.Parameters.Add(AppSchema.VoucherTransaction.AMOUNTColumn, Math.Abs(Amount));
                                dataManager.Parameters.Add(AppSchema.VoucherTransaction.TRANS_MODEColumn, TransMode);
                                dataManager.Parameters.Add(AppSchema.VoucherTransaction.CHEQUE_NOColumn, ChequeNo);
                                dataManager.Parameters.Add(AppSchema.VoucherTransaction.CHEQUE_REF_DATEColumn, string.Empty);
                                dataManager.Parameters.Add(AppSchema.VoucherTransaction.CHEQUE_REF_BANKNAMEColumn, string.Empty);
                                dataManager.Parameters.Add(AppSchema.VoucherTransaction.CHEQUE_REF_BRANCHColumn, string.Empty);
                                dataManager.Parameters.Add(AppSchema.VoucherTransaction.MATERIALIZED_ONColumn, MaterializedOn);
                                resultArgs = dataManager.UpdateData();
                            }
                            VoucherTransSequenceNo++;
                        }
                        else
                        {
                            dmActive.TransExecutionMode = ExecutionMode.Fail;
                            resultArgs.Message = "Ledger is not found in Voucher Details";
                            resultArgs.Success = false;
                            break;
                        }
                    }
                }
            }
            return resultArgs;
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
        #endregion
                
       #region UI Updation (Triggering Events)
        private void SetProgressBar(string message, Int32 maxrecord)
        {
            if (InitProgressBar != null)
            {
                EventMigrationProcessbarArgs progressevent = new EventMigrationProcessbarArgs(message, maxrecord);
                InitProgressBar(this, progressevent);
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
      #endregion

       private int GetCountryId(string CountryName)
        {
            int CountryId = 0;
            using (AcMePlusMigrationSystem AcMePlusMigration = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                CountryId = AcMePlusMigration.GetCountryId(CountryName);
            }
            return CountryId;
        }

       /// <summary>
        /// Created by alwar on 02/12/2015
        /// This function is used to get Nature Type (A,L, I, E) for given Ledger from Acme.erp by using its ledger group,
        /// If Leadger Name is not existing in the Acmeerp, It will check Leadger's group nature which is coming from Tally
        /// For Ex this leadger "Bosco Counselling Services" group's might be changed in Acme.erp, 
        /// so when we take update op, check ledgers nature (if ledger is already exists in acmeerp, take its nature or take tally group nature)
        /// </summary>
       private int GetLedgerNature(string ledgername, string primarygroup)
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

       private string MapLedgerName(string LedgerName)
        {
            string Result = string.Empty;
            if (dtLedger != null)
            {
                DataView dvLedger = new DataView(dtLedger);
                dvLedger.RowFilter = String.Format("account='{0}'", LedgerName.Replace("'", "''"));
                if (dvLedger.ToTable() != null)
                {
                    DataTable dtLedgerFiltered = dvLedger.ToTable();
                    if (dtLedgerFiltered != null && dtLedgerFiltered.Rows.Count > 0)
                    {
                        if (dtLedger.Columns.Contains("LEDGER_NAME")) //When Mapping is done
                            Result = string.IsNullOrEmpty(dtLedgerFiltered.Rows[0]["LEDGER_NAME"].ToString()) ? LedgerName : dtLedgerFiltered.Rows[0]["LEDGER_NAME"].ToString();
                        else //When Mapping is  not done
                            Result = string.IsNullOrEmpty(dtLedgerFiltered.Rows[0]["account"].ToString()) ? LedgerName : dtLedgerFiltered.Rows[0]["account"].ToString();
                    }
                }
            }
            return Result.Trim();
        }

       /// <summary>
        /// Get all the mandatory data from BOSCOPAC, this method will be invorked before migration
        /// </summary>
        /// <returns></returns>
       private ResultArgs LoadDataFromBOSCOPAC()
        {
            resultArgs = new ResultArgs();
            using (BOSCOPACConnector boscoconnector = new BOSCOPACConnector(boscopac_base_path))
            {
                resultArgs = boscoconnector.FetchActivities();
                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    dtActivty = resultArgs.DataSource.Table;
                    resultArgs = boscoconnector.FetchLedgerGroup();
                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        dtGroup = resultArgs.DataSource.Table;
                        resultArgs = boscoconnector.FetchLedger();
                        if (resultArgs.Success && resultArgs.DataSource.Table != null)
                        {
                            dtLedger = resultArgs.DataSource.Table;
                            resultArgs = boscoconnector.FetchCountry();
                            if (resultArgs.Success && resultArgs.DataSource.Table != null)
                            {
                                dtCountry = resultArgs.DataSource.Table;
                                resultArgs = boscoconnector.FetchDonor();
                                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                                {
                                    dtDonor = resultArgs.DataSource.Table;
                                    resultArgs = boscoconnector.FetchMasterVouchers();
                                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                                    {
                                        dtMasterVoucher = resultArgs.DataSource.Table;
                                        resultArgs = boscoconnector.FetchDetailVouchers();
                                        if (resultArgs.Success && resultArgs.DataSource.Table != null)
                                        {
                                            dtDetailVoucher = resultArgs.DataSource.Table;
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// This method is used to get projectId from Acme.erp
        /// </summary>
        /// <returns></returns>
       private int GetProjectId()
       {
            int ProjectId = 0;
            using (AcMePlusMigrationSystem getProject = new AcMePlusMigrationSystem(MigrationType.Tally))
            {
                ProjectId = getProject.GetProjectId(BOSCOPAC_ActivityName);
            }
            return ProjectId;
        }
                
        /// <summary>
        /// Execute Flash tables after migration
        /// </summary>
        private void ExecuteFlushCommands()
        {
            using (DataManager dataManger = new DataManager())
            {
                resultArgs = dataManger.UpdateData("FLUSH TABLES;FLUSH HOSTS;");
            }
        }

        /// <summary>
        /// This method is used to check current voucher entry, contains fd leger
        /// </summary>
        /// <returns></returns>
        private bool IsVoucherContainsFD(Int32 Gno, string Vdate, string Vtype)
        {
            bool Rtn = true;
            dtDetailVoucher.DefaultView.RowFilter = string.Empty;
            dtDetailVoucher.DefaultView.RowFilter = "hcode = " + BOSCOPAC_HouseId + " AND tcode = " + BOSCOPAC_ActivityId + " AND " +
                                                        "genno =" + Gno + " AND vdate= '" + Vdate + "' AND vtype='" + Vtype + "' AND group = 'FIXED DEPOSITS'";
            Rtn = (dtDetailVoucher.DefaultView.Count > 0);
            dtDetailVoucher.DefaultView.RowFilter = string.Empty;
            return Rtn;
        }
        
        /// <summary>
        /// This method is used to check current voucher entry, contains proper child details of voucher, somtimes, its has only one child (CR or DR alone) vouchers
        /// </summary>
        /// <returns></returns>
        private bool IsVoucherContainsValidChildVouchers(Int32 Gno, string Vdate, string Vtype)
        {
            bool Rtn = true;
            dtDetailVoucher.DefaultView.RowFilter = string.Empty;
            dtDetailVoucher.DefaultView.RowFilter = "hcode = " + BOSCOPAC_HouseId + " AND tcode = " + BOSCOPAC_ActivityId + " AND " +
                                                        "genno =" + Gno + " AND vdate= '" + Vdate + "' AND vtype='" + Vtype + "'";
            Rtn = (dtDetailVoucher.DefaultView.Count >= 2);
            dtDetailVoucher.DefaultView.RowFilter = string.Empty;
            return Rtn;
        }

        /// <summary>
        /// This method is used to check current voucher entry, contains proper child details of voucher, somtimes, its has only one child (CR or DR alone) vouchers
        /// </summary>
        /// <returns></returns>
        private bool IsVoucherContainsCASHBANKLedger(Int32 Gno, string Vdate, string Vtype)
        {
            bool Rtn = true;
            dtDetailVoucher.DefaultView.RowFilter = string.Empty;
            dtDetailVoucher.DefaultView.RowFilter = "hcode = " + BOSCOPAC_HouseId + " AND tcode = " + BOSCOPAC_ActivityId + " AND " +
                                                        "genno =" + Gno + " AND vdate= '" + Vdate + "' AND vtype='" + Vtype + "' AND group IN ('Cash-in-hand', 'Bank Accounts')";
            Rtn = (dtDetailVoucher.DefaultView.Count > 0);
            dtDetailVoucher.DefaultView.RowFilter = string.Empty;
            return Rtn;
        }

        /// <summary>
        /// This method will be called only if current voucher journal but it affects CASH/BANK ledger
        /// 
        /// This method is used to check current voucher entry, Receipts voucher it means
        /// cash or bank ledger should be debit
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsVoucherReceipt(Int32 Gno, string Vdate, string Vtype)
        {
            bool Rtn = true;
            dtDetailVoucher.DefaultView.RowFilter = string.Empty;
            dtDetailVoucher.DefaultView.RowFilter = "hcode = " + BOSCOPAC_HouseId + " AND tcode = " + BOSCOPAC_ActivityId + " AND " +
                                                    "genno =" + Gno + " AND vdate= '" + Vdate + "' AND vtype='" + Vtype + "' AND " +
                                                    "group IN ('Cash-in-hand', 'Bank Accounts') AND debit > 0";
            Rtn = (dtDetailVoucher.DefaultView.Count > 0);
            dtDetailVoucher.DefaultView.RowFilter = string.Empty;
            return Rtn;
        }

        /// <summary>
        /// This method is used to check vouchers are avilable for given ledger name
        /// </summary>
        /// <returns></returns>
        private bool IsVoucherExistsforLedger(string LedgerName)
        {
            bool Rtn = true;
            dtDetailVoucher.DefaultView.RowFilter = string.Empty;
            dtDetailVoucher.DefaultView.RowFilter = "hcode = " + BOSCOPAC_HouseId + " AND tcode = " + BOSCOPAC_ActivityId + " AND acchead = '" + LedgerName.Replace("'", "''") + "'";
            DataTable dt = dtDetailVoucher.DefaultView.ToTable();
            Rtn = (dtDetailVoucher.DefaultView.Count > 0);
            if (Rtn)
            {

            }
            dtDetailVoucher.DefaultView.RowFilter = string.Empty;
            return Rtn;
        }
    }

    public class EventMigrationProcessbarArgs : EventArgs
    {
        public string ProgressMessage { get; set; }
        public Int32 MaxRecord { get; set; }

        public EventMigrationProcessbarArgs(string progressmessage, Int32 maxrecord)
        {
            ProgressMessage = progressmessage;
            MaxRecord = maxrecord;
        }
    }
}

//private void MigrateBankAccount()
//{
//    using (DataManager dataManager = new DataManager(SQLCommand.AcMePlusMigration.MigrateBankAccount))
//    {
//        dataManager.Parameters.Add(AppSchema.BankAccount.LEDGER_IDColumn, LedgerId);
//        dataManager.Parameters.Add(AppSchema.BankAccount.ACCOUNT_CODEColumn, GetBankAccountCode());
//        dataManager.Parameters.Add(AppSchema.BankAccount.ACCOUNT_NUMBERColumn, AccountNo);
//        dataManager.Parameters.Add(AppSchema.BankAccount.BANK_IDColumn, BankId);
//        dataManager.Parameters.Add(AppSchema.BankAccount.DATE_OPENEDColumn, DateOpened);
//        dataManager.Parameters.Add(AppSchema.BankAccount.DATE_CLOSEDColumn, null);
//        dataManager.Parameters.Add(AppSchema.BankAccount.ACCOUNT_TYPE_IDColumn, 1);
//        resultArgs = dataManager.UpdateData();
//    }
//}

///// <summary>
//        /// 
//        /// </summary>
//        /// <returns></returns>
//        public bool IsFDLegersInVoucher(Int32 hcode, Int32 tcode, string acchead)
//        {
//            bool Rtn = true;
//            try
//            {

//                dtLedger.DefaultView.RowFilter = "hcode = " + hcode + " AND tcode = " + tcode + " AND acchead = '" +  acchead + "'";
//                if (dtLedger.DefaultView.Count > 0)
//                {
//                    string grpname  = dtLedger.DefaultView[0]["group"].ToString().Trim();
//                    Rtn = (grpname.ToUpper().Equals("FIXED DEPOSITS"));
//                }

//            }
//            catch (Exception ex)
//            {
//                Rtn = false;
//            }
//            dtLedger.DefaultView.RowFilter = string.Empty;
//            return Rtn;
//        }
