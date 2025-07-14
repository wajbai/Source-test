using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

using Bosco.Utility;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using System.Data;
using Bosco.Model.Business;
using Bosco.Model.UIModel;
using System.IO;
using AcMEDSync.Model;

namespace Bosco.Model.TallyMigration
{
    public class ImportMasterDetailSystem : SystemBase
    {
        #region Common Properties
        ResultArgs resultArgs = null;
        public string FilePath { get; set; }
        public int ProjectType { get; set; }
        int LegalEntityId = 0;
        public DateTime dtTransactionPeriod = new DateTime();
        public DateTime dtTransLeastDate = new DateTime();
        bool isRefreshBalance = true;
        private int BankId { get; set; }
        private int IsTDSLedger = 0;
        #endregion

        #region Project Properties
        public int ProjectId { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectCategory { get; set; }
        public string Project { get; set; }
        public int Division { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CloseDate { get; set; }
        public string Description { get; set; }
        public bool isProjectSuccess = false;
        public DataTable dtProject { get; set; }
        #endregion

        #region Ledger
        public DataTable dtLedger { get; set; }
        private string LedgerName { get; set; }
        private string LedgerCode { get; set; }
        private int LedgerId { get; set; }
        private int FetchLastLedgerId { get; set; }
        private const string LEDGERCODE = "LEDGER_CODE";
        private const string LEDGERNAME = "LEDGER_NAME";
        private const string LEDGERGROUPNAME = "LEDGER_GROUP";
        private const string MAINGROUP = "MAIN_GROUP";
        private double LedgerAmount { get; set; }
        private int isCostCentre = 0;
        private int isBankInterest = 0;
        private int isBankAccount = 0;
        private int SortId = 4;

        #endregion

        #region Ledger Group
        private const string LEDGERGROUPCODE = "GROUP_CODE";
        private const string LEGERGROUPNAME = "LEDGER_GROUP";
        private const string PARENTGROUP = "PARENT_GROUP";
        private const string NATURE = "NATURE";
        private int ImageId = 0;
        public DataTable dtLedgerGroup { get; set; }
        private string LedgerGroupCode { get; set; }
        private string LedgerGroupName { get; set; }
        private string ParentGroup { get; set; }
        private string Nature { get; set; }
        private int GroupId { get; set; }
        private string LedgerType { get; set; }
        private string LedgerSubType { get; set; }
        private int FetchLastLedgerGroupId { get; set; }
        #endregion

        #region Cost Centres
        public DataTable dtCostCentre { get; set; }
        private string CostCentreCode { get; set; }
        private string CostCentreName { get; set; }
        private int CostCentreId { get; set; }
        private double CostCentreAmount { get; set; }
        private string TransMode { get; set; }
        private int FetchLastCostCentreId { get; set; }
        private const string ABBRIVATION = "ABBREVATION";
        private const string COSTCENTRENAME = "COST_CENTRE_NAME";
        private const string OPBALANCE = "AMOUNT";
        private const string TRANSMODE = "TRANS_MODE";
        #endregion

        #region Voucher Transaction
        public DataTable dtErrorTransaction;
        public DataTable dtVoucherDetails { get; set; }
        private string VoucherNo { get; set; }
        int CostCenterId { get; set; }
        string VoucherType { get; set; }
        string TranMode { get; set; }
        string Narration { get; set; }
        string Particulars { get; set; }
        decimal DebitAmount { get; set; }
        decimal CreditAmount { get; set; }
        string CheaqueNo { get; set; }
        string MaterializedOn { get; set; }
        DateTime deVoucherDate { get; set; }
        int VoucherId { get; set; }
        decimal Amount = 0;
        bool IsCostCenter = false;
        string Query = string.Empty;
        string FieldValue = string.Empty;

        private const string PARTICULARS = "Particulars";
        private const string DEBITAMOUNT = "Debit Amount";
        private const string CREDITAMOUNT = "Credit Amount";
        private const string DATE = "Date";
        private const string CREDIT = "Credit";
        private const string DEBIT = "Debit";
        private const string VOUCHERNUMBER = "Vch No.";
        private const string VOUCHERTYPE = "Vch Type";
        private const string NO_OF_TRANS = "NO_OF_TRANS";

        #endregion

        #region Public Methods
        /// <summary>
        /// To establish the connection to import excel file
        /// </summary>
        /// <returns></returns>
        public OleDbConnection ConfigurationHandler()
        {
            OleDbConnection conn = null;
            try
            {
                string connstr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                conn = new OleDbConnection(connstr);
                if (!string.IsNullOrEmpty(conn.ConnectionString) && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return conn;
        }

        /// <summary>
        /// To import the excel file as a datatable
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="tablename"></param>
        /// <param name="connectionstring"></param>
        /// <returns></returns>
        public DataTable ExcelToDataTable(string sqlQuery)
        {
            DataTable dtTable = new DataTable();
            try
            {
                OleDbConnection oldDBConnecton = ConfigurationHandler();
                OleDbDataAdapter oleDataAdapter = new OleDbDataAdapter(sqlQuery, oldDBConnecton);
                oleDataAdapter.Fill(dtTable);
                oldDBConnecton.Close();
                foreach (var column in dtTable.Columns.Cast<DataColumn>().ToArray())
                {
                    if (dtTable.AsEnumerable().All(drr => drr.IsNull(column)))
                        dtTable.Columns.Remove(column);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return dtTable;
        }

        /// <summary>
        /// Import Tally ERP master and voucher to AcME ERP
        /// </summary>
        /// <returns></returns>
        public ResultArgs ImportTallyToAcMEERP()
        {
            try
            {
                GeneralLogger.TallyMigration.WriteLog("Tally Migration :Started ImportMaster method(Library) Successfully ");
                using (DataManager dataManager = new DataManager())
                {
                    GeneralLogger.TallyMigration.WriteLog("Tally Migration :Transaction begins Successfully ");
                    dataManager.BeginTransaction();
                    resultArgs = ImportProjects(dataManager);
                    if (resultArgs != null && resultArgs.Success)
                    {
                        resultArgs = ImportLedgerGroup(dataManager);
                        if (resultArgs != null && resultArgs.Success)
                        {
                            resultArgs = ImportLedger(dataManager);
                            if (resultArgs != null && resultArgs.Success)
                            {
                                resultArgs = ImportCostCentre(dataManager);
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    resultArgs = ImportVoucherTrans(dataManager);
                                    isRefreshBalance = resultArgs.Success;
                                }
                            }
                        }
                    }
                    dataManager.EndTransaction();
                    if (resultArgs != null && resultArgs.Success && isRefreshBalance)
                    {
                        resultArgs = RefreshBalance();
                    }
                    GeneralLogger.TallyMigration.WriteLog("Tally Migration :Transaction ends Successfully ");
                }
                GeneralLogger.TallyMigration.WriteLog("Tally Migration :Ended ImportMaster method(Library) Successfully ");
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// Get Naute id by Ledger Grop
        /// </summary>
        /// <param name="LedgerGroup"></param>
        /// <returns></returns>
        public int GetNatureId(string LedgerGroup)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.GetNatureIdByLedgerGroup))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.LEDGER_GROUPColumn, LedgerGroup);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Import project details
        /// </summary>
        /// <param name="dataManagerProject"></param>
        /// <returns></returns>
        private ResultArgs ImportProjects(DataManager dataManagerProject)
        {
            try
            {
                GeneralLogger.TallyMigration.WriteLog("Tally Migration :Import Project begins Successfully ");
                foreach (DataRow drProject in dtProject.Rows)
                {
                    using (DataManager dataManager = new DataManager())
                    {
                        dataManager.Database = dataManagerProject.Database;
                        ProjectCode = drProject[this.AppSchema.Project.PROJECT_CODEColumn.ColumnName].ToString();
                        Project = drProject[this.AppSchema.Project.PROJECTColumn.ColumnName].ToString();

                        if (ProjectId == (int)ProjectTypes.New)
                        {
                            if (isProjectCodeExist(dataManager) == 0 && isProjectNameExist(dataManager) == 0)
                            {
                                SaveProject(dataManager);
                            }
                            else
                            {
                                resultArgs.Success = false;
                            }
                        }
                        else
                        {
                            SaveProject(dataManager);
                        }
                    }
                }
                GeneralLogger.TallyMigration.WriteLog("Tally Migration :Import Project ends Successfully ");
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
                resultArgs.Success = false;
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// Import ledger group details
        /// </summary>
        /// <param name="dataManagerLedgerGroup"></param>
        /// <returns></returns>
        private ResultArgs ImportLedgerGroup(DataManager dataManagerLedgerGroup)
        {
            try
            {
                GeneralLogger.TallyMigration.WriteLog("Tally Migration :Import Ledge Group begins Successfully ");
                if (dtLedgerGroup.Columns.Contains(LEDGERGROUPCODE) && dtLedgerGroup.Columns.Contains(LEGERGROUPNAME) && dtLedgerGroup.Columns.Contains(PARENTGROUP) && dtLedgerGroup.Columns.Contains(NATURE))
                {
                    foreach (DataRow drLedgerGroup in dtLedgerGroup.Rows)
                    {
                        using (DataManager dataManager = new DataManager())
                        {
                            dataManager.Database = dataManagerLedgerGroup.Database;
                            LedgerGroupCode = drLedgerGroup[LEDGERGROUPCODE] != DBNull.Value ? drLedgerGroup[LEDGERGROUPCODE].ToString() : string.Empty;
                            LedgerGroupName = drLedgerGroup[LEGERGROUPNAME] != DBNull.Value ? drLedgerGroup[LEGERGROUPNAME].ToString() : string.Empty;
                            ParentGroup = drLedgerGroup[PARENTGROUP] != DBNull.Value ? drLedgerGroup[PARENTGROUP].ToString() : string.Empty;
                            Nature = drLedgerGroup[NATURE] != DBNull.Value ? drLedgerGroup[NATURE].ToString() : string.Empty;
                            if (!(string.IsNullOrEmpty(LedgerGroupCode) && string.IsNullOrEmpty(LedgerGroupName) && string.IsNullOrEmpty(ParentGroup) && string.IsNullOrEmpty(Nature)))
                            {
                                if (isLedgerGroupCodeExists(dataManager) == 0 && isLedgerGroupNameExists(dataManager) == 0)
                                {
                                    resultArgs = SaveLedgerGroup(dataManager);
                                    if (!resultArgs.Success)
                                    {
                                        GeneralLogger.TallyMigration.WriteLog(String.Format("Tally Migration :{0}", resultArgs.Message + Environment.NewLine + " Ledger Group Name  " + LedgerGroupName));
                                        break;
                                    }
                                }
                                else if (isLedgerGroupCodeExists(dataManager) != 0 && isLedgerGroupNameExists(dataManager) == 0)
                                {
                                    LedgerGroupCode = FetchLedgerGroupCode(dataManager).ToString();
                                    resultArgs = SaveLedgerGroup(dataManager);
                                    if (!resultArgs.Success)
                                    {
                                        GeneralLogger.TallyMigration.WriteLog(String.Format("Tally Migration :{0}", resultArgs.Message + Environment.NewLine + " Ledger Group Name  " + LedgerGroupName));
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                GeneralLogger.TallyMigration.WriteLog("Tally Migration :Import Ledge Group ends Successfully ");
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// Import ledger details
        /// </summary>
        /// <param name="dataManagerLedger"></param>
        /// <returns></returns>
        private ResultArgs ImportLedger(DataManager dataManagerLedger)
        {
            try
            {
                GeneralLogger.TallyMigration.WriteLog("Tally Migration :Import Ledger begins Successfully ");
                if (dtLedger != null && dtLedger.Rows.Count != 0)
                {
                    if (dtLedger.Columns.Contains(LEDGERCODE) && dtLedger.Columns.Contains(LEDGERNAME) && dtLedger.Columns.Contains(LEDGERGROUPNAME))
                    {
                        foreach (DataRow drLedger in dtLedger.Rows)
                        {
                            using (DataManager dataManager = new DataManager())
                            {
                                dataManager.Database = dataManagerLedger.Database;
                                LedgerCode = drLedger[LEDGERCODE] != DBNull.Value ? drLedger[LEDGERCODE].ToString() : string.Empty;
                                LedgerName = drLedger[LEDGERNAME] != DBNull.Value ? drLedger[LEDGERNAME].ToString() : string.Empty;
                                if (!(string.IsNullOrEmpty(LedgerCode) && string.IsNullOrEmpty(LedgerName)))
                                {
                                    LedgerAmount = drLedger[OPBALANCE] != DBNull.Value ? this.NumberSet.ToDouble(drLedger[OPBALANCE].ToString()) : 0;
                                    TransMode = drLedger[TRANSMODE] != DBNull.Value ? drLedger[TRANSMODE].ToString() : string.Empty;
                                    //LedgerGroupName = drLedger[LEDGERGROUPNAME] != DBNull.Value ? drLedger[LEDGERGROUPNAME].ToString() : string.Empty;
                                    LedgerGroupName = drLedger[MAINGROUP] != DBNull.Value ? drLedger[MAINGROUP].ToString() : string.Empty;
                                    if (!string.IsNullOrEmpty(LedgerGroupName))
                                    {
                                        GroupId = GetLedgerGroupID(dataManager); //Get Ledger id based on ledger name
                                        LedgerType = ledgerSubType.GN.ToString();
                                        LedgerSubType = GroupId == (int)FixedLedgerGroup.BankAccounts ? ledgerSubType.BK.ToString() : GroupId == (int)FixedLedgerGroup.FixedDeposit ? ledgerSubType.FD.ToString() : ledgerSubType.GN.ToString();
                                    }
                                    if (isLedgerCodeExists(dataManager) == 0 && isLedgerNameExists(dataManager) == 0) //Insert when ledger code and ledger name does not exit
                                    {
                                        resultArgs = SaveLedgers(dataManager);
                                        if (!resultArgs.Success)
                                        {
                                            GeneralLogger.TallyMigration.WriteLog(String.Format("Tally Migration :{0}", resultArgs.Message + Environment.NewLine + "Ledger Code " + LedgerCode + Environment.NewLine + " Ledger Group Name  " + LedgerGroupName + Environment.NewLine + "Ledger Name " + LedgerName));
                                            break;
                                        }
                                    }
                                    else if (isLedgerCodeExists(dataManager) != 0 && isLedgerNameExists(dataManager) == 0) //Insert when ledger code exits and ledger name does not exists
                                    {
                                        LedgerCode = FetchLedgerCode(dataManager);
                                        resultArgs = SaveLedgers(dataManager);
                                        if (!resultArgs.Success)
                                        {
                                            GeneralLogger.TallyMigration.WriteLog(String.Format("Tally Migration :{0}", resultArgs.Message + Environment.NewLine + "Ledger Code " + LedgerCode + Environment.NewLine + " Ledger Group Name  " + LedgerGroupName + Environment.NewLine + "Ledger Name " + LedgerName));
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        LedgerId = FetchLedgerId(dataManager); //Get Ledger  id based on ledger name
                                        if (LedgerId != 0)
                                        {
                                            if (ProjectType == (int)ProjectTypes.New)
                                            {
                                                resultArgs = DeleteMappedProjectLedger(dataManager);
                                                if (resultArgs != null && resultArgs.Success)
                                                {
                                                    resultArgs = MapProjectWithLedgers(dataManager);
                                                    if (resultArgs != null && resultArgs.Success)
                                                    {
                                                        if (LedgerAmount > 0)
                                                        {
                                                            resultArgs = UpdateLedgerBalance(dataManager); //update ledger balance based on the project id and ledger id when amount is greater then zero
                                                            if (resultArgs != null && !resultArgs.Success)
                                                            {
                                                                GeneralLogger.TallyMigration.WriteLog(String.Format("Tally Migration :{0}", resultArgs.Message + Environment.NewLine + "Ledger Code " + LedgerCode + Environment.NewLine + " Ledger Group Name  " + LedgerGroupName + Environment.NewLine + "Ledger Name " + LedgerName));
                                                                break;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        GeneralLogger.TallyMigration.WriteLog(String.Format("Tally Migration :{0}", resultArgs.Message + Environment.NewLine + "Ledger Code " + LedgerCode + Environment.NewLine + " Ledger Group Name  " + LedgerGroupName + Environment.NewLine + "Ledger Name " + LedgerName));
                                                        break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                resultArgs = DeleteMappedProjectLedger(dataManager);
                                                if (resultArgs != null && resultArgs.Success)
                                                {
                                                    resultArgs = MapProjectWithLedgers(dataManager);
                                                    if (resultArgs != null && resultArgs.Success)
                                                    {
                                                        if (LedgerAmount > 0)
                                                        {
                                                            resultArgs = UpdateLedgerBalance(dataManager); //update ledger balance based on the project id and ledger id when amount is greater then zero
                                                            if (!resultArgs.Success)
                                                            {
                                                                GeneralLogger.TallyMigration.WriteLog(String.Format("Tally Migration :{0}", resultArgs.Message + Environment.NewLine + "Ledger Code " + LedgerCode + Environment.NewLine + " Ledger Group Name  " + LedgerGroupName + Environment.NewLine + "Ledger Name " + LedgerName));
                                                                break;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        GeneralLogger.TallyMigration.WriteLog(String.Format("Tally Migration :{0}", resultArgs.Message + Environment.NewLine + "Ledger Code " + LedgerCode + Environment.NewLine + " Ledger Group Name  " + LedgerGroupName + Environment.NewLine + "Ledger Name " + LedgerName));
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
                GeneralLogger.TallyMigration.WriteLog("Tally Migration :Import Ledger ends Successfully ");
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// Import cost centre details
        /// </summary>
        /// <param name="dataManagerCostCentre"></param>
        /// <returns></returns>
        private ResultArgs ImportCostCentre(DataManager dataManagerCostCentre)
        {
            try
            {
                if (dtCostCentre != null && dtCostCentre.Rows.Count != 0)
                {
                    if (dtCostCentre.Columns.Contains(ABBRIVATION) && dtCostCentre.Columns.Contains(COSTCENTRENAME))
                    {
                        foreach (DataRow drCostCentre in dtCostCentre.Rows)
                        {
                            using (DataManager datamanager = new DataManager(SQLCommand.CostCentre.Add))
                            {
                                datamanager.Database = dataManagerCostCentre.Database;
                                CostCentreCode = drCostCentre[ABBRIVATION] != DBNull.Value ? drCostCentre[ABBRIVATION].ToString() : string.Empty;
                                CostCentreName = drCostCentre[COSTCENTRENAME] != DBNull.Value ? drCostCentre[COSTCENTRENAME].ToString() : string.Empty;
                                CostCentreAmount = drCostCentre[OPBALANCE] != DBNull.Value ? this.NumberSet.ToDouble(drCostCentre[OPBALANCE].ToString()) : 0;
                                TransMode = drCostCentre[TRANSMODE] != DBNull.Value ? drCostCentre[TRANSMODE].ToString() : string.Empty;
                                if (!string.IsNullOrEmpty(CostCentreCode) && !string.IsNullOrEmpty(CostCentreName))
                                {
                                    if (!string.IsNullOrEmpty(CostCentreCode) && (!string.IsNullOrEmpty(CostCentreName)))
                                    {
                                        if (isCostCentreCodeExist(datamanager) == 0 && isCostCentreNameExist(datamanager) == 0) //when cost centre code and cost centre does not exit than insert
                                        {
                                            resultArgs = SaveCostCentre(datamanager);
                                            if (!resultArgs.Success)
                                            {
                                                GeneralLogger.TallyMigration.WriteLog(String.Format("Tally Migration :{0}", resultArgs.Message));
                                                break;
                                            }
                                        }
                                        //when cost centre code exits and cost centre does not exit than insert cost centre
                                        else if (isCostCentreCodeExist(datamanager) != 0 && isCostCentreNameExist(datamanager) == 0)
                                        {
                                            CostCentreCode = FetchCostCentreCode(datamanager).ToString(); //Generate cost centre code automatically
                                            resultArgs = SaveCostCentre(datamanager);
                                            if (!resultArgs.Success)
                                            {
                                                GeneralLogger.TallyMigration.WriteLog(String.Format("Tally Migration :{0}", resultArgs.Message));
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            CostCentreId = FetchCostCentreId(datamanager); // Fetch cost centre id based on cost centre name
                                            if (CostCentreId != 0)
                                            {
                                                resultArgs = DeleteCostCentre(datamanager);
                                                if (resultArgs != null && resultArgs.Success)
                                                {
                                                    if (CostCentreAmount > 0)
                                                    {
                                                        resultArgs = MapProjectWithCostCentre(datamanager);
                                                        if (!resultArgs.Success)
                                                        {
                                                            GeneralLogger.TallyMigration.WriteLog(String.Format("Tally Migration :{0}", resultArgs.Message));
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
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return resultArgs;
        }

        private ResultArgs ImportVoucherTrans(DataManager dataManagerVoucher)
        {
            try
            {
                GeneralLogger.TallyMigration.WriteLog("Tally Migration :Import Transaction begins Successfully ");
                using (DataManager dataManager = new DataManager())
                {
                    DataTable dtNoOfTransaction;
                    dataManager.Database = dataManagerVoucher.Database;
                    if (dtVoucherDetails != null && dtVoucherDetails.Rows.Count > 0)
                    {
                        GetStructuredVoucherDetails(out dtNoOfTransaction, out dtErrorTransaction);
                        GeneralLogger.TallyMigration.WriteLog("Tally Migration :Import Transaction begins dtNoOfTransaction looping Successfully ");
                        foreach (DataRow drVoucherTrans in dtNoOfTransaction.Rows)
                        {
                            bool IsBank = false;
                            int Count = 0;
                            VoucherId = 0;
                            int TransCount = 0;
                            int LedgerId = 0;
                            GeneralLogger.TallyMigration.WriteLog("Tally Migration :Getting number of rows based on NO_OF_TRANS begins Successfully.");
                            DataRow[] NoOfTras = dtVoucherDetails.Select(String.Format(NO_OF_TRANS + "={0}", drVoucherTrans[NO_OF_TRANS].ToString()));
                            GeneralLogger.TallyMigration.WriteLog("Tally Migration :Getting number of rows based on NO_OF_TRANS ends Successfully.");
                            int TotalRowsCount = NoOfTras.Count();
                            foreach (DataRow drTrans in NoOfTras)
                            {
                                using (Bosco.Model.Transaction.VoucherTransactionSystem trans = new Transaction.VoucherTransactionSystem())
                                {
                                    trans.VoucherType = string.Empty;
                                    DebitAmount = 0;
                                    CreditAmount = 0;
                                    MaterializedOn = "0001-01-01 00:00:00";
                                    string BankDetails = string.Empty;
                                    Narration = string.Empty;
                                    if (!IsBank) //This is to avoid bank details be executed because bank details would have been executed in the previous loop itseft
                                    {
                                        Particulars = drTrans[PARTICULARS].ToString();// Particulars name can be a Ledger Name or a Cost Centre Name or a Narration.
                                        if (TotalRowsCount != TransCount + 1) // This condition is for avoiding error while executing last row
                                            BankDetails = NoOfTras[TransCount + 1][1].ToString(); //Getting bank details

                                        if (string.IsNullOrEmpty(BankDetails))
                                        {
                                            IsBank = true;  //For skipping the next loop when BankDetails is not empty
                                            if (TotalRowsCount != TransCount + 1)// Check for the next row 
                                            {
                                                //For bank details coulum name is generated randomlily so use index to get cheque number and materialized on details
                                                trans.ChequeNo = NoOfTras[TransCount + 1][3].ToString();  //If it bank column 3 equal to cheque number (column 3=Cheque Number)
                                                if (NoOfTras[TransCount + 1][4].ToString() != string.Empty) //If it bank column 4 equal to Materialized on (column 4=Materialized on)
                                                    trans.MaterializedOn = Convert.ToDateTime(NoOfTras[TransCount + 1][4].ToString()).ToString("yyyy-MM-dd");
                                                else
                                                    trans.MaterializedOn = "0001-01-01 00:00:00";
                                            }
                                        }
                                        if (dtVoucherDetails.Columns.Contains(DEBITAMOUNT)) //Debit Column  may be "Debit Amount" or just "Debit"
                                        {
                                            if (drTrans[DEBITAMOUNT].ToString() != string.Empty)
                                            {
                                                string[] Value = drTrans[DEBITAMOUNT].ToString().Split(' '); //To sperate amount from Trans Mode (EX:1000 DR)
                                                DebitAmount = NumberSet.ToDecimal(Value[0].ToString());
                                            }
                                        }
                                        else if (!string.IsNullOrEmpty(drTrans[DEBIT].ToString()))
                                        {
                                            string[] Value = drTrans[DEBIT].ToString().Split(' '); //To sperate amount from Trans Mode (EX:1000 DR)
                                            DebitAmount = NumberSet.ToDecimal(Value[0].ToString());
                                        }
                                        else
                                        {
                                            DebitAmount = 0;
                                        }
                                        if (dtVoucherDetails.Columns.Contains(CREDITAMOUNT)) //Credit Column  may be "Credit Amount" or just "Credit"
                                        {
                                            if (drTrans[CREDITAMOUNT].ToString() != string.Empty && drTrans[CREDITAMOUNT] != DBNull.Value)
                                            {
                                                string[] Value = drTrans[CREDITAMOUNT].ToString().Split(' ');//To sperate amount from Trans Mode (EX:1000 CR)
                                                CreditAmount = NumberSet.ToDecimal(Value[0].ToString());
                                            }
                                        }
                                        else if (!string.IsNullOrEmpty(drTrans[CREDIT].ToString()))
                                        {
                                            string[] Value = drTrans[CREDIT].ToString().Split(' ');//To sperate amount from Trans Mode (EX:1000 CR)
                                            CreditAmount = NumberSet.ToDecimal(Value[0].ToString());
                                        }
                                        else
                                        {
                                            CreditAmount = 0;
                                        }

                                        if (DebitAmount > 0)
                                        {
                                            trans.Amount = DebitAmount;
                                            trans.TransMode = TransactionMode.DR.ToString();
                                        }
                                        else if (CreditAmount > 0)
                                        {
                                            trans.Amount = CreditAmount;
                                            trans.TransMode = TransactionMode.CR.ToString();
                                        }
                                        else
                                        {
                                            if (string.IsNullOrEmpty(drTrans[2].ToString()))//If it bank column 2 equal to Narration (column 2=Narration)
                                                Narration = Particulars;
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(Particulars))
                                                {
                                                    //For cost center details
                                                    GeneralLogger.TallyMigration.WriteLog("Tally Migration :Getting Cost Centre amount and Trans Mode begins successfully");
                                                    trans.TransMode = GetCostCentreAmount(drTrans, ref Amount);
                                                    GeneralLogger.TallyMigration.WriteLog("Tally Migration :Getting Cost Centre amount and Trans Mode ends successfully");
                                                    trans.CostCentreAmount = Amount;
                                                    CostCentreName = Particulars;
                                                    GeneralLogger.TallyMigration.WriteLog("Tally Migration :Fetch Cost centre Id Based on Cost centre Name begins successfully");
                                                    trans.CostCenterId = FetchCostCentreId(dataManager);
                                                    GeneralLogger.TallyMigration.WriteLog("Tally Migration :Fetch Cost centre Id Based on Cost centre Name ends successfully");
                                                    if (trans.CostCenterId <= 0)
                                                    {
                                                        GeneralLogger.TallyMigration.WriteLog("Tally Migration: Invalid Cost Centre id");
                                                        break;
                                                    }
                                                    IsCostCenter = true;
                                                }
                                            }
                                        }
                                        if (string.IsNullOrEmpty(Narration))
                                        {
                                            if (drTrans[VOUCHERTYPE].ToString() != string.Empty)
                                            {
                                                VoucherType = drTrans[VOUCHERTYPE].ToString();
                                                if (VoucherType.Equals(DefaultVoucherTypes.Payment.ToString()))
                                                {
                                                    trans.VoucherType = VoucherSubTypes.PY.ToString();
                                                }
                                                else if (VoucherType.Equals(DefaultVoucherTypes.Receipt.ToString()))
                                                {
                                                    trans.VoucherType = VoucherSubTypes.RC.ToString();
                                                }
                                                else if (VoucherType.Equals(DefaultVoucherTypes.Contra.ToString()))
                                                {
                                                    trans.VoucherType = VoucherSubTypes.CN.ToString();
                                                }
                                                else if (VoucherType.Equals(DefaultVoucherTypes.Journal.ToString()))
                                                {
                                                    trans.VoucherType = VoucherSubTypes.JN.ToString();
                                                }
                                                if (!string.IsNullOrEmpty(drTrans[VOUCHERNUMBER].ToString()))
                                                {
                                                    trans.VoucherNo = drTrans[VOUCHERNUMBER].ToString();
                                                }
                                                if (drTrans[0].ToString() != string.Empty)
                                                {
                                                    trans.VoucherDate = DateSet.ToDate(drTrans[0].ToString(), false);
                                                }
                                                if (!string.IsNullOrEmpty(trans.VoucherType))
                                                {
                                                    LedgerName = Particulars;
                                                    GeneralLogger.TallyMigration.WriteLog("Tally Migration :Fetch Ledger Id Based on Ledge Name Begins successfully");
                                                    int ReplaceLedgerId = FetchLedgerId(dataManager);
                                                    GeneralLogger.TallyMigration.WriteLog("Tally Migration :Fetch Ledger Id Based on Ledge Name ends successfully");
                                                    if (ReplaceLedgerId > 0)
                                                    {
                                                        trans.LedgerId = LedgerId = ReplaceLedgerId;
                                                        trans.ProjectId = ProjectId;
                                                        trans.Narration = string.IsNullOrEmpty(Narration) ? string.Empty : Narration;
                                                        trans.ModifiedBy = trans.CreatedBy = NumberSet.ToInteger(LoginUserId);
                                                        //Saving the Voucher Master Records
                                                        GeneralLogger.TallyMigration.WriteLog("Tally Migration :Save Voucher Master Transaction Details begins Successfully");
                                                        resultArgs = trans.SaveTallyVoucherMasterDetails(dataManager);
                                                        if (resultArgs.Success)
                                                        {
                                                            GeneralLogger.TallyMigration.WriteLog("Tally Migration :Save Voucher Master Transaction Details ends Successfully");
                                                            VoucherId = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                                            trans.VoucherId = VoucherId;
                                                            if (VoucherId == 0)
                                                            {
                                                                GeneralLogger.TallyMigration.WriteLog("Tally Migration :Invalid Voucher Id");
                                                                dtErrorTransaction = WriteErrorTransactionToDataTable(NoOfTras);
                                                                break; //Skipping the current transaction
                                                            }
                                                            else
                                                            {
                                                                trans.SequenceNo = ++Count;
                                                                GeneralLogger.TallyMigration.WriteLog("Tally Migration :Save Voucher Transaction Details begins Successfully");
                                                                resultArgs = trans.SaveVoucherTransactionDetails(dataManager);
                                                                GeneralLogger.TallyMigration.WriteLog("Tally Migration :Save Voucher Transaction Details ends Successfully");
                                                                if (!resultArgs.Success)
                                                                {
                                                                    GeneralLogger.TallyMigration.WriteLog(String.Format("Tally Migration :{0}", resultArgs.Message));
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        dtErrorTransaction = WriteErrorTransactionToDataTable(NoOfTras);
                                                        GeneralLogger.TallyMigration.WriteLog("Tally Migration :Invaid Ledger Id ");
                                                        break; //This is to skip the current transaction
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                LedgerName = Particulars;
                                                GeneralLogger.TallyMigration.WriteLog("Tally Migration :Fetch Ledger Id Based on Ledge Name Begins successfully");
                                                int ReplaceLedgerId = 0;
                                                if (!IsCostCenter)
                                                {
                                                    //New Ledger Id for the transaction
                                                    ReplaceLedgerId = FetchLedgerId(dataManager);
                                                }
                                                else
                                                {
                                                    //To get cost center Ledger id for the transaction from the previous tranastion transaction ledger Id
                                                    ReplaceLedgerId = LedgerId;
                                                }
                                                GeneralLogger.TallyMigration.WriteLog("Tally Migration :Fetch Ledger Id Based on Ledge Name ends successfully");
                                                if (VoucherId > 0)
                                                {
                                                    if (ReplaceLedgerId > 0 || IsCostCenter)
                                                    {
                                                        trans.LedgerId = LedgerId = ReplaceLedgerId;
                                                        if (IsCostCenter)
                                                        {
                                                            trans.LedgerId = LedgerId;
                                                            GeneralLogger.TallyMigration.WriteLog("Tally Migration :Save Cost Center Transaction Details begins Successfully");
                                                            trans.SequenceNo = ++Count;
                                                            trans.VoucherId = VoucherId;
                                                            resultArgs = trans.SaveVoucherCostCentre(dataManager);
                                                            if (!resultArgs.Success) GeneralLogger.TallyMigration.WriteLog(String.Format("Tally Migration :{0}", resultArgs.Message));
                                                            else GeneralLogger.TallyMigration.WriteLog("Tally Migration :Save Cost Center Transaction Details ends Successfully");
                                                            IsCostCenter = false;
                                                        }
                                                        else
                                                        {
                                                            trans.SequenceNo = ++Count;
                                                            trans.VoucherId = VoucherId;
                                                            GeneralLogger.TallyMigration.WriteLog("Tally Migration :Save Voucher Transaction Details begins Successfully");
                                                            resultArgs = trans.SaveVoucherTransactionDetails(dataManager);
                                                            if (!resultArgs.Success) GeneralLogger.TallyMigration.WriteLog(String.Format("Tally Migration :{0}", resultArgs.Message));
                                                            else GeneralLogger.TallyMigration.WriteLog("Tally Migration :Save Voucher Transaction Details ends Successfully");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        dtErrorTransaction = WriteErrorTransactionToDataTable(NoOfTras);
                                                        GeneralLogger.TallyMigration.WriteLog("Tally Migration :Invaid Ledger Id ");
                                                        break; //This is to skip the current transaction
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (VoucherId > 0)
                                            {
                                                //Updating Narration
                                                trans.Narration = Narration;
                                                trans.VoucherId = VoucherId;
                                                resultArgs = trans.UpdateVoucherTransNarration(dataManager);
                                                if (!resultArgs.Success) GeneralLogger.TallyMigration.WriteLog(String.Format("Tally Migration# Updating Narration Faild :{0}", resultArgs.Message));
                                            }
                                        }
                                    }
                                    else IsBank = false;
                                    TransCount++;
                                }
                            }
                        }
                    }
                    GeneralLogger.TallyMigration.WriteLog("Tally Migration :Import Transaction ends dtNoOfTransaction looping Successfully ");
                }
                GeneralLogger.TallyMigration.WriteLog("Tally Migration :Import Transaction ends Successfully ");
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
                resultArgs.Success = false;
            }
            finally { }
            return resultArgs;
        }

        private DataTable WriteErrorTransactionToDataTable(DataRow[] No_Of_Rows)
        {
            foreach (DataRow dr in No_Of_Rows)
            {
                dtErrorTransaction.ImportRow(dr);
            }
            return dtErrorTransaction;
        }

        private void GetStructuredVoucherDetails(out DataTable dtNoOfTransaction, out DataTable dtErrorTransaction)
        {
            GeneralLogger.TallyMigration.WriteLog("Tally Migration :Import Transaction Structring the datatable begins Successfully ");
            for (int i = 0; i < dtVoucherDetails.Rows.Count; i++)
            {
                if (dtVoucherDetails.Rows[0][0].ToString() == DATE && dtVoucherDetails.Rows[0][1].ToString() == PARTICULARS)
                {
                    break;
                }
                else
                {
                    dtVoucherDetails.Rows.RemoveAt(0);
                    dtVoucherDetails = dtVoucherDetails;
                }
            }
            dtVoucherDetails = RenameColumns(dtVoucherDetails);
            dtVoucherDetails.Rows.RemoveAt(0);
            dtVoucherDetails.Rows.RemoveAt(0);
            dtNoOfTransaction = TrasactionByGroup(dtVoucherDetails);
            if (dtNoOfTransaction != null && dtNoOfTransaction.Rows.Count > 0)
            {
                GeneralLogger.TallyMigration.WriteLog("Tally Migration :Import Transaction Grouping by number of transaction(NO_OF_TRANS) begins successfully");
                dtNoOfTransaction = dtNoOfTransaction.AsEnumerable().GroupBy(r => r.Field<Int32>(NO_OF_TRANS)).Select(g => g.First()).CopyToDataTable();
                GeneralLogger.TallyMigration.WriteLog("Tally Migration :Import Transaction Grouping by number of transaction(NO_OF_TRANS) ends successfully");
            }
            dtErrorTransaction = dtVoucherDetails.Clone();
            GeneralLogger.TallyMigration.WriteLog("Tally Migration :Import Transaction Structring the datatable ends Successfully ");
        }

        private ResultArgs SaveBank(DataManager dmBank)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Bank.Add))
            {
                dataManager.Database = dmBank.Database;
                dataManager.Parameters.Add(this.AppSchema.Bank.BANK_CODEColumn, GenerateBankCode(dataManager));
                dataManager.Parameters.Add(this.AppSchema.Bank.BANKColumn, LedgerName);
                dataManager.Parameters.Add(this.AppSchema.Bank.BRANCHColumn, LedgerName);
                dataManager.Parameters.Add(this.AppSchema.Bank.ADDRESSColumn, string.Empty);
                dataManager.Parameters.Add(this.AppSchema.Bank.IFSCCODEColumn, string.Empty);
                dataManager.Parameters.Add(this.AppSchema.Bank.MICRCODEColumn, string.Empty);
                dataManager.Parameters.Add(this.AppSchema.Bank.CONTACTNUMBERColumn, string.Empty);
                dataManager.Parameters.Add(this.AppSchema.Bank.SWIFTCODEColumn, string.Empty);
                dataManager.Parameters.Add(this.AppSchema.Bank.BANK_IDColumn, BankId);
                dataManager.Parameters.Add(this.AppSchema.Bank.NOTESColumn, string.Empty);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private string GenerateBankCode(DataManager dataManager)
        {
            string BankCode = string.Empty;
            using (DataManager dataManagerBank = new DataManager(SQLCommand.Bank.FetchBankCount))
            {
                dataManagerBank.Database = dataManager.Database;
                resultArgs = dataManagerBank.FetchData(DataSource.Scalar);
                if (resultArgs != null && resultArgs.Success)
                {
                    BankCode = "BT" + resultArgs.DataSource.Sclar.ToInteger.ToString();
                }
            }
            return BankCode;
        }
        public int PeriodYr { get; set; }
        public int PeriodMth { get; set; }
        public int PeriodDay { get; set; }
        public decimal InterestRate { get; set; }
        private ResultArgs SaveBankAccount(DataManager dmBankAccount)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.BankAccountAdd))
            {
                dataManager.Database = dmBankAccount.Database;
                dataManager.Parameters.Add(this.AppSchema.BankAccount.ACCOUNT_CODEColumn, GenerateBankCode(dataManager));
                dataManager.Parameters.Add(this.AppSchema.BankAccount.LEDGER_IDColumn, LedgerId.ToString());
                dataManager.Parameters.Add(this.AppSchema.BankAccount.ACCOUNT_NUMBERColumn, LedgerName);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.ACCOUNT_TYPE_IDColumn, 1);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.DATE_OPENEDColumn, dtTransactionPeriod);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.BANK_IDColumn, BankId);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.ACCOUNT_HOLDER_NAMEColumn, "");
                dataManager.Parameters.Add(this.AppSchema.BankAccount.DATE_CLOSEDColumn, null);
                dataManager.Parameters.Add(AppSchema.BankAccount.OPERATED_BYColumn, "");
                dataManager.Parameters.Add(AppSchema.BankAccount.PERIOD_YEARColumn, PeriodYr);
                dataManager.Parameters.Add(AppSchema.BankAccount.PERIOD_MTHColumn, PeriodMth);
                dataManager.Parameters.Add(AppSchema.BankAccount.PERIOD_DAYColumn, PeriodDay);
                dataManager.Parameters.Add(AppSchema.BankAccount.INTEREST_RATEColumn, InterestRate);
                dataManager.Parameters.Add(AppSchema.BankAccount.AMOUNTColumn, Amount);
                dataManager.Parameters.Add(AppSchema.BankAccount.MATURITY_DATEColumn, null);
                dataManager.Parameters.Add(AppSchema.BankAccount.NOTESColumn, "");
                dataManager.Parameters.Add(AppSchema.BankAccount.IS_FCRA_ACCOUNTColumn, PeriodYr);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// check project category exists or not
        /// </summary>
        /// <param name="dataProjectCategory"></param>
        /// <returns></returns>
        private int isProjectCategoryExist(DataManager dataProjectCategory)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ProjectCatogory.IsProjectCategory))
                {
                    dataManager.Database = dataProjectCategory.Database;
                    dataManager.Parameters.Add(this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn, ProjectCategory);
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        /// <summary>
        /// Insert project category
        /// </summary>
        /// <param name="dataManagerProjectCategory"></param>
        /// <returns></returns>
        private int InsertProjectCategory(DataManager dataManagerProjectCategory)
        {
            int ProjectCategoryId = 0;
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ProjectCatogory.Add))
                {
                    dataManager.Database = dataManagerProjectCategory.Database;
                    dataManager.Parameters.Add(this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn, ProjectCategory);
                    resultArgs = dataManager.UpdateData();
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        ProjectCategoryId = FetchProjectCategoryId(dataManager);
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            return ProjectCategoryId;
        }

        /// <summary>
        /// Fetch project category id by project category name
        /// </summary>
        /// <param name="dm"></param>
        /// <returns></returns>
        private int FetchProjectCategoryId(DataManager dm)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ProjectCatogory.FetchProjectCategoryId))
                {
                    dataManager.Database = dm.Database;
                    dataManager.Parameters.Add(this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn, ProjectCategory);
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        /// <summary>
        /// check ledger code exists or not
        /// </summary>
        /// <param name="dataManagerLedgerCode"></param>
        /// <returns></returns>
        private int isLedgerCodeExists(DataManager dataManagerLedgerCode)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.IsLedgerCodeExists))
                {
                    dataManager.Database = dataManagerLedgerCode.Database;
                    dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_CODEColumn, LedgerCode);
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        /// <summary>
        /// Check ledger group code exists or not
        /// </summary>
        /// <param name="dataLedgerGroupCode"></param>
        /// <returns></returns>
        private int isLedgerGroupCodeExists(DataManager dataLedgerGroupCode)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.IsLedgerGroupCode))
                {
                    dataManager.Database = dataLedgerGroupCode.Database;
                    dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_CODEColumn, LedgerGroupCode);
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        /// <summary>
        /// Check ledger group name exists or not
        /// </summary>
        /// <param name="dataLedgerGroupName"></param>
        /// <returns></returns>
        private int isLedgerGroupNameExists(DataManager dataLedgerGroupName)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.IsLedgerGroupName))
                {
                    dataManager.Database = dataLedgerGroupName.Database;
                    dataManager.Parameters.Add(this.AppSchema.LedgerGroup.LEDGER_GROUPColumn, LedgerGroupName);
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        /// <summary>
        /// Check ledger name is exist or not.
        /// </summary>
        /// <param name="dataManagerLedgerName"></param>
        /// <returns></returns>
        private int isLedgerNameExists(DataManager dataManagerLedgerName)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.IsLedgerNameExists))
                {
                    dataManager.Database = dataManagerLedgerName.Database;
                    dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, LedgerName);
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        /// <summary>
        /// Check cost centre code is exits or not.
        /// </summary>
        /// <param name="dataManagerCostCentre"></param>
        /// <returns></returns>
        private int isCostCentreCodeExist(DataManager dataManagerCostCentre)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.CostCentre.IsCostCentreExist))
                {
                    dataManager.Database = dataManagerCostCentre.Database;
                    dataManager.Parameters.Add(this.AppSchema.CostCentre.ABBREVATIONColumn, CostCentreCode);
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        /// <summary>
        /// Check cost name is exists or not
        /// </summary>
        /// <param name="dataManagerCostCentreName"></param>
        /// <returns></returns>
        private int isCostCentreNameExist(DataManager dataManagerCostCentreName)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.CostCentre.IsCostCentreNameExist))
                {
                    dataManager.Database = dataManagerCostCentreName.Database;
                    dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_NAMEColumn, CostCentreName);
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        /// <summary>
        /// Check project code is exist or not
        /// </summary>
        /// <param name="dataManagerProjectcode"></param>
        /// <returns></returns>
        private int isProjectCodeExist(DataManager dataManagerProjectcode)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Project.IsProjectCodeExists))
                {
                    dataManager.Database = dataManagerProjectcode.Database;
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_CODEColumn, ProjectCode);
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }

            return resultArgs.DataSource.Sclar.ToInteger;
        }

        /// <summary>
        ///  Check project name is exist or not.
        /// </summary>
        /// <param name="dataManagerProject"></param>
        /// <returns></returns>
        private int isProjectNameExist(DataManager dataManagerProject)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Project.IsProjectNameExists))
                {
                    dataManager.Database = dataManagerProject.Database;
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECTColumn, Project);
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        /// <summary>
        /// Get Ledge group id by ledgergroup name
        /// </summary>
        /// <param name="dataManagerLedgerGroupName"></param>
        /// <returns></returns>
        private int GetLedgerGroupID(DataManager dataManagerLedgerGroupName)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.GetLedgerGroupId))
                {
                    dataManager.Database = dataManagerLedgerGroupName.Database;
                    dataManager.Parameters.Add(this.AppSchema.LedgerGroup.LEDGER_GROUPColumn, LedgerGroupName);
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        /// <summary>
        /// Get ledger group parent id
        /// </summary>
        /// <param name="dataManagerParent"></param>
        /// <returns></returns>
        private int GetParentId(DataManager dataManagerParent)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.GetParentId))
                {
                    dataManager.Database = dataManagerParent.Database;
                    dataManager.Parameters.Add(this.AppSchema.LedgerGroup.LEDGER_GROUPColumn, ParentGroup);
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        /// <summary>
        /// Get ledger group nature id
        /// </summary>
        /// <param name="dataManagerNatureId"></param>
        /// <returns></returns>
        private int GetNatureID(DataManager dataManagerNatureId)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.GetNatureId))
                {
                    dataManager.Database = dataManagerNatureId.Database;
                    dataManager.Parameters.Add(this.AppSchema.LedgerGroup.NATUREColumn, Nature);
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        /// <summary>
        /// Get main group parent id
        /// </summary>
        /// <param name="dataManagerMainParentId"></param>
        /// <returns></returns>
        private int GetMainParentId(DataManager dataManagerMainParentId)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.GetParentId))
                {
                    dataManager.Database = dataManagerMainParentId.Database;
                    dataManager.Parameters.Add(this.AppSchema.LedgerGroup.LEDGER_GROUPColumn, Nature);
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        /// <summary>
        /// Get project id by project name
        /// </summary>
        /// <param name="dataManagerProject"></param>
        /// <returns></returns>
        private int GetProjectId(DataManager dataManagerProject)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchProjectId))
                {
                    dataManager.Database = dataManagerProject.Database;
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECTColumn, Project);
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        /// <summary>
        /// Fetch ledger id by ledger name
        /// </summary>
        /// <param name="dataManagerLedger"></param>
        /// <returns></returns>
        private int FetchLedgerId(DataManager dataManagerLedger)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerId))
                {
                    dataManager.Database = dataManagerLedger.Database;
                    dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, LedgerName);
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        /// <summary>
        /// Fetch cost centre id by cost centre name
        /// </summary>
        /// <param name="dataManagerCostCentre"></param>
        /// <returns></returns>
        private int FetchCostCentreId(DataManager dataManagerCostCentre)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.CostCentre.FetchCostCentreId))
                {
                    dataManager.Database = dataManagerCostCentre.Database;
                    dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_NAMEColumn, CostCentreName);
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        /// <summary>
        /// Map ledger with  projects.
        /// </summary>
        /// <param name="dataManagerLedgerMapping"></param>
        /// <returns></returns>
        private ResultArgs MapProjectWithLedgers(DataManager dataManagerLedgerMapping)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectLedgerMappingAdd))
                {
                    dataManager.Database = dataManagerLedgerMapping.Database;
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// Map Project with cost centres
        /// </summary>
        /// <param name="dataManagerCostCentreMapping"></param>
        /// <returns></returns>
        private ResultArgs MapProjectWithCostCentre(DataManager dataManagerCostCentreMapping)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectCostCentreMappingAdd))
                {
                    dataManager.Database = dataManagerCostCentreMapping.Database;
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, CostCentreId);
                    dataManager.Parameters.Add(this.AppSchema.LedgerBalance.AMOUNTColumn, CostCentreAmount);
                    dataManager.Parameters.Add(this.AppSchema.LedgerBalance.TRANS_MODEColumn, TransMode);
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// Map Default vouchers with projects
        /// </summary>
        /// <param name="dataManagerVouchers"></param>
        /// <returns></returns>
        private ResultArgs MapDefaultVouchersWithProject(DataManager dataManagerVouchers)
        {
            List<int> voucherTypes = new List<int>();
            voucherTypes.Add((int)DefaultVoucherTypes.Receipt);
            voucherTypes.Add((int)DefaultVoucherTypes.Payment);
            voucherTypes.Add((int)DefaultVoucherTypes.Contra);
            voucherTypes.Add((int)DefaultVoucherTypes.Journal);
            try
            {
                for (int i = 1; i <= voucherTypes.Count; i++)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Project.AddProjectVouchers))
                    {
                        dataManager.Database = dataManagerVouchers.Database;
                        dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                        dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_IDColumn, i);
                        resultArgs = dataManager.UpdateData();
                        if (!resultArgs.Success)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// Map default cash,Capital fund and fixed deposit ledger with projects
        /// </summary>
        /// <param name="dataManagerDefaultLedgerMapping"></param>
        /// <returns></returns>
        private ResultArgs MapDefaultLedgerwithProjects(DataManager dataManagerDefaultLedgerMapping)
        {
            try
            {
                DataTable dtLedger = FetchDefaultLedgers();
                foreach (DataRow drLedger in dtLedger.Rows)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectLedgerMappingAdd))
                    {
                        dataManager.Database = dataManagerDefaultLedgerMapping.Database;
                        dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, drLedger[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName] != DBNull.Value ?
                        this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()) : 0);
                        resultArgs = dataManager.UpdateData();
                        if (!resultArgs.Success)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// Fetch default ledger for mapping with projects.
        /// </summary>
        /// <returns></returns>
        private DataTable FetchDefaultLedgers()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchDefaultLedgers))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return resultArgs.DataSource.Table;
        }

        /// <summary>
        /// Update ledger balance
        /// </summary>
        /// <param name="dataLedgerBalance"></param>
        /// <returns></returns>
        private ResultArgs UpdateLedgerBalance(DataManager dataLedgerBalance)
        {
            try
            {
                if (!string.IsNullOrEmpty(dtTransactionPeriod.ToShortDateString()))
                {
                    using (DataManager dataManager = new DataManager())
                    {
                        dataManager.Database = dataLedgerBalance.Database;
                        using (BalanceSystem balanceSystem = new BalanceSystem())
                        {
                            if (dtTransactionPeriod > dtTransLeastDate)
                            {
                                dtTransactionPeriod = dtTransLeastDate;
                            }

                            if (ProjectType == (int)ProjectTypes.New)
                            {
                                resultArgs = balanceSystem.UpdateOpBalance(dtTransactionPeriod.ToShortDateString(), ProjectId, LedgerId, LedgerAmount, TransMode, TransactionAction.New);
                            }
                            else
                            {
                                resultArgs = balanceSystem.UpdateOpBalance(dtTransactionPeriod.ToShortDateString(), ProjectId, LedgerId, LedgerAmount, TransMode, TransactionAction.EditAfterSave);
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    resultArgs = balanceSystem.UpdateOpBalance(dtTransactionPeriod.ToShortDateString(), ProjectId, LedgerId, LedgerAmount, TransMode, TransactionAction.New);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// Fetch active transaction  period for ledger balance
        /// </summary>
        /// <param name="dataManagerTransaction"></param>
        public DateTime FetchActiveTransactionPeriod(DataManager dataManagerTransaction)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.FetchActiveTransactionperiod))
                {
                    dataManager.Database = dataManagerTransaction.Database;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        dtTransactionPeriod = this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AccountingPeriod.BOOKS_BEGINNING_FROMColumn.ColumnName].ToString(), false);
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return dtTransactionPeriod;
        }

        /// <summary>
        /// Save project details
        /// </summary>
        /// <param name="dataManagerProject"></param>
        /// <returns></returns>
        private ResultArgs SaveProject(DataManager dataManagerProject)
        {
            try
            {
                foreach (DataRow drProject in dtProject.Rows)
                {
                    using (DataManager dataManager = new DataManager(ProjectId == 0 ? SQLCommand.Project.Add : SQLCommand.Project.Update))
                    {
                        dataManager.Database = dataManagerProject.Database;
                        dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                        dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_CODEColumn, drProject[this.AppSchema.Project.PROJECT_CODEColumn.ColumnName].ToString());
                        ProjectCategory = drProject[this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn.ColumnName] != DBNull.Value ?
                        drProject[this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn.ColumnName].ToString() : string.Empty;
                        if (isProjectCategoryExist(dataManager) != 0)
                        {
                            dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_CATEGORY_IDColumn, FetchProjectCategoryId(dataManager));
                        }
                        else
                        {
                            dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_CATEGORY_IDColumn, InsertProjectCategory(dataManager));
                        }
                        dataManager.Parameters.Add(this.AppSchema.Project.ACCOUNT_DATEColumn, null);

                        dataManager.Parameters.Add(this.AppSchema.Project.PROJECTColumn, drProject[this.AppSchema.Project.PROJECTColumn.ColumnName].ToString());
                        dataManager.Parameters.Add(this.AppSchema.Project.DIVISION_IDColumn, drProject[this.AppSchema.Project.DIVISION_IDColumn.ColumnName].ToString());
                        dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, drProject[this.AppSchema.Project.DATE_STARTEDColumn.ColumnName].ToString());
                        dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, drProject[this.AppSchema.Project.DATE_CLOSEDColumn.ColumnName] != DBNull.Value ?
                        drProject[this.AppSchema.Project.DATE_CLOSEDColumn.ColumnName] : null);
                        dataManager.Parameters.Add(this.AppSchema.Project.DESCRIPTIONColumn, drProject[this.AppSchema.Project.DESCRIPTIONColumn.ColumnName].ToString());
                        dataManager.Parameters.Add(this.AppSchema.Project.CUSTOMERIDColumn, LegalEntityId);
                        dataManager.Parameters.Add(this.AppSchema.Project.NOTESColumn, string.Empty);
                        resultArgs = dataManager.UpdateData();
                        if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected != 0)
                        {
                            if (ProjectType == (int)ProjectTypes.New)
                            {
                                ProjectId = GetProjectId(dataManager);
                                if (ProjectId != 0)
                                {
                                    resultArgs = MapDefaultVouchersWithProject(dataManager);
                                    if (resultArgs != null && resultArgs.Success)
                                    {
                                        resultArgs = MapDefaultLedgerwithProjects(dataManager);
                                        if (resultArgs != null && resultArgs.Success)
                                        {
                                            isProjectSuccess = resultArgs.Success;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                isProjectSuccess = resultArgs.Success;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
                resultArgs.Success = false;
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        ///  Save Ledger details
        /// </summary>
        /// <param name="dataManagerLedger"></param>
        /// <returns></returns>
        private ResultArgs SaveLedgers(DataManager dataManagerLedger)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.Add))
                {
                    dataManager.Database = dataManagerLedger.Database;
                    dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_CODEColumn, LedgerCode);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, LedgerName);
                    if (!string.IsNullOrEmpty(LedgerGroupName))
                    {
                        dataManager.Parameters.Add(this.AppSchema.Ledger.GROUP_IDColumn, GroupId);
                        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_TYPEColumn, LedgerType);
                        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_SUB_TYPEColumn, LedgerSubType);
                    }
                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_COST_CENTERColumn, isCostCentre);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_BANK_INTEREST_LEDGERColumn, isBankInterest);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.BANK_ACCOUNT_IDColumn, isBankAccount);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.NOTESColumn, null);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.SORT_IDColumn, SortId + 1);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_TDS_LEDGERColumn, IsTDSLedger);
                    resultArgs = dataManager.UpdateData();
                    if (resultArgs.Success)
                    {
                        LedgerId = FetchLedgerId(dataManager);
                        if (LedgerSubType.Equals(ledgerSubType.BK.ToString()))
                        {
                            resultArgs = SaveBank(dataManager);
                            if (resultArgs != null && resultArgs.Success)
                            {
                                BankId = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                resultArgs = SaveBankAccount(dataManager);
                            }
                        }
                        if (LedgerId != 0)
                        {
                            if (ProjectType == (int)ProjectTypes.New)
                            {
                                resultArgs = MapProjectWithLedgers(dataManager);
                                if (resultArgs.Success)
                                {
                                    if (LedgerAmount > 0)
                                    {
                                        resultArgs = UpdateLedgerBalance(dataManager);
                                    }
                                }
                            }
                            else
                            {
                                if (LedgerAmount > 0)
                                {
                                    resultArgs = UpdateLedgerBalance(dataManager);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// Fetch ledge code from ledger table in decending order to generate auto ledger code for ledgers.
        /// </summary>
        /// <param name="dataManagerLedgecode"></param>
        /// <returns></returns>
        private string FetchLedgerCode(DataManager dataManagerLedgecode)
        {
            try
            {
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Database = dataManagerLedgecode.Database;
                    using (Bosco.Model.UIModel.LedgerSystem ledgerSystem = new UIModel.LedgerSystem())
                    {
                        resultArgs = ledgerSystem.FetchMaxLedgerCode();
                        if (resultArgs != null && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                        {
                            FetchLastLedgerId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return "LE" + FetchLastLedgerId;
        }

        /// <summary>
        /// Save Cost centre
        /// </summary>
        /// <param name="dm"></param>
        /// <returns></returns>
        private ResultArgs SaveCostCentre(DataManager dm)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.CostCentre.Add))
                {
                    dataManager.Database = dm.Database;
                    dataManager.Parameters.Add(this.AppSchema.CostCentre.ABBREVATIONColumn, CostCentreCode);
                    dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_NAMEColumn, CostCentreName);
                    dataManager.Parameters.Add(this.AppSchema.CostCentre.NOTESColumn, string.Empty);
                    resultArgs = dataManager.UpdateData();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        CostCentreId = FetchCostCentreId(dataManager);
                        if (CostCentreId != 0)
                        {
                            resultArgs = DeleteCostCentre(dataManager);
                            if (resultArgs != null && resultArgs.Success)
                            {
                                if (CostCentreAmount > 0)
                                {
                                    resultArgs = MapProjectWithCostCentre(dataManager);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// Fetch Cost centre code from cost centre table for generating auto cost centre code for cost centre.
        /// </summary>
        /// <param name="dataManagerCostCentre"></param>
        /// <returns></returns>
        private int FetchCostCentreCode(DataManager dataManagerCostCentre)
        {
            try
            {
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Database = dataManagerCostCentre.Database;
                    using (Bosco.Model.UIModel.CostCentreSystem costCentreSystem = new UIModel.CostCentreSystem())
                    {
                        resultArgs = costCentreSystem.FetchCostCentreCodes();
                        if (resultArgs != null && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                        {
                            FetchLastCostCentreId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return FetchLastCostCentreId + 1;
        }

        /// <summary>
        /// Save Ledger group details
        /// </summary>
        /// <param name="dataManagerLedgerGroup"></param>
        /// <returns></returns>
        private ResultArgs SaveLedgerGroup(DataManager dataManagerLedgerGroup)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.Add))
                {
                    dataManager.Database = dataManagerLedgerGroup.Database;
                    dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_CODEColumn, LedgerGroupCode);
                    dataManager.Parameters.Add(this.AppSchema.LedgerGroup.LEDGER_GROUPColumn, LedgerGroupName);
                    dataManager.Parameters.Add(this.AppSchema.LedgerGroup.PARENT_GROUP_IDColumn, (!string.IsNullOrEmpty(ParentGroup)) ? GetParentId(dataManager) : 0);
                    dataManager.Parameters.Add(this.AppSchema.LedgerGroup.NATURE_IDColumn, (!string.IsNullOrEmpty(Nature)) ? GetNatureID(dataManager) : 0);
                    dataManager.Parameters.Add(this.AppSchema.LedgerGroup.MAIN_GROUP_IDColumn, (!string.IsNullOrEmpty(LedgerGroupName)) ? GetMainParentId(dataManager) : 0);
                    dataManager.Parameters.Add(this.AppSchema.LedgerGroup.IMAGE_IDColumn, ImageId);
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// Fetch ledger group id for generating auto ledger group code for ledger group.
        /// </summary>
        /// <param name="dataManagerLedgerGroup"></param>
        /// <returns></returns>
        private int FetchLedgerGroupCode(DataManager dataManagerLedgerGroup)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchLedgerGroupId))
                {
                    dataManager.Database = dataManagerLedgerGroup.Database;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        FetchLastLedgerGroupId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerGroup.GROUP_IDColumn.ColumnName].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return FetchLastLedgerGroupId + 1;
        }

        /// <summary>
        /// Rename columns dynamically
        /// </summary>
        /// <param name="dtTable"></param>
        /// <returns></returns>
        private DataTable RenameColumns(DataTable dtTable)
        {
            GeneralLogger.TallyMigration.WriteLog("Tally Migration :Import Transaction Renaming datatable column begins successfully");
            for (int col = 0; col < dtTable.Columns.Count; col++)
            {
                dtTable.Columns[col].ColumnName = dtTable.Rows[0][col].ToString().Equals(string.Empty) ? "COL" + col : dtTable.Rows[0][col].ToString();
            }
            GeneralLogger.TallyMigration.WriteLog("Tally Migration :Import Transaction Renaming datatable column ends successfully");
            return dtTable;
        }

        /// <summary>
        /// To add a column to the DataTable for separating the Transaction 
        /// </summary>
        /// <param name="dtTrasction"></param>
        /// <returns></returns>
        private DataTable TrasactionByGroup(DataTable dtTrasction)
        {
            GeneralLogger.TallyMigration.WriteLog("Tally Migration :Import Transaction adding number of transaction columns (NO_OF_TRANS) begins successfully");
            int Count = 0;
            dtTrasction.Columns.Add(NO_OF_TRANS, typeof(System.Int32));
            foreach (DataRow dr in dtTrasction.Rows)
            {
                if (dr[0].ToString() != string.Empty)
                {
                    Count++;
                    dr[NO_OF_TRANS] = Count;
                }
                else
                {
                    dr[NO_OF_TRANS] = Count;
                }
            }
            GeneralLogger.TallyMigration.WriteLog("Tally Migration :Import Transaction adding number of transaction columns (NO_OF_TRANS) ends successfully");
            return dtTrasction;
        }

        /// <summary>
        /// Fetch cost centre amount 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="Amount"></param>
        /// <returns></returns>
        private string GetCostCentreAmount(DataRow dr, ref decimal Amount)
        {
            // Cost Centre Column is not fixed so use index based for cost center amount and trans mode.
            Amount = NumberSet.ToDecimal(dr[2].ToString()); //  2= Cost centre Amount
            return dr[3].ToString(); // 3= Trans Mode for Cost Centre
        }

        /// <summary>
        /// Refresh entrire balance 
        /// </summary>
        /// <returns></returns>
        private ResultArgs RefreshBalance()
        {
            GeneralLogger.TallyMigration.WriteLog("Tally Migration :Refresh begins successfully");
            using (DataManager dataManager = new DataManager())
            {
                using (BalanceSystem balanceSystem = new BalanceSystem())
                {
                    balanceSystem.ProjectId = ProjectId;
                    resultArgs = balanceSystem.UpdateBulkTransBalance();
                }
            }
            GeneralLogger.TallyMigration.WriteLog("Tally Migration :Refresh ends successfully " + resultArgs.Message);
            return resultArgs;
        }

        /// <summary>
        /// Fetch project details
        /// </summary>
        /// <returns></returns>
        public DataTable FetchProject()
        {
            GeneralLogger.TallyMigration.WriteLog("Tally Migration :Fetch project  begins successfully");
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchProjectforLookup))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            GeneralLogger.TallyMigration.WriteLog("Tally Migration :Fetch project ends successfully");
            return resultArgs.DataSource.Table;
        }

        /// <summary>
        /// Fetch project based on the project id
        /// </summary>
        /// <returns></returns>
        public DataTable FetchProjectDetailsById()
        {
            GeneralLogger.TallyMigration.WriteLog("Tally Migration :Fetch project information based on the  project id begins successfully");
            using (DataManager dataManager = new DataManager(SQLCommand.Project.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            GeneralLogger.TallyMigration.WriteLog("Tally Migration :Fetch project information based on the  project id ends successfully");
            return resultArgs.DataSource.Table;
        }

        /// <summary>
        /// Delete mapped ledger based on the project id
        /// </summary>
        /// <param name="dataManagerProjectLedger"></param>
        /// <returns></returns>
        private ResultArgs DeleteMappedProjectLedger(DataManager dataManagerProjectLedger)
        {
            GeneralLogger.TallyMigration.WriteLog("Tally Migration :Delete Ledger based on project id ,ledger is begins successfully");
            using (DataManager dataManager = new DataManager(SQLCommand.Project.DeleteProjectMappedLedgers))
            {
                dataManager.Database = dataManagerProjectLedger.Database;
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.UpdateData();
            }
            GeneralLogger.TallyMigration.WriteLog("Tally Migration :Delete Ledger based on project id ,ledger is ends successfully");
            return resultArgs;
        }

        /// <summary>
        /// Delete cost centre based on the project id in project_costcentre Table
        /// </summary>
        /// <param name="dataManagerCostCentre"></param>
        /// <returns></returns>
        private ResultArgs DeleteCostCentre(DataManager dataManagerCostCentre)
        {
            GeneralLogger.TallyMigration.WriteLog("Tally Migration :Delete cost centre based on project id begins successfully");
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.DeleteProjectCostCenterMapping))
            {
                dataManager.Database = dataManagerCostCentre.Database;
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.UpdateData();
            }
            GeneralLogger.TallyMigration.WriteLog("Tally Migration :Delete cost centre based on project id ends successfully");
            return resultArgs;
        }

        /// <summary>
        /// Fetch last transaction date
        /// </summary>
        /// <returns></returns>
        private DateTime FetchTransactionLeastDate()
        {
            GeneralLogger.TallyMigration.WriteLog("Tally Migration :Fetch Last Transactions date begins successfully");
            using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.FetchLeastDate))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            GeneralLogger.TallyMigration.WriteLog("Tally Migration :Fetch Last Transactions date ends successfully");
            return dtTransLeastDate = this.DateSet.ToDate(resultArgs.DataSource.Data.ToString(), false);
        }

        /// <summary>
        /// Delete transaction based on the Project if it is overwritting.
        /// </summary>
        /// <param name="dm"></param>
        /// <returns></returns>
        public ResultArgs DeleteTransactions()
        {
            GeneralLogger.TallyMigration.WriteLog("Tally Migration :Delete number of transaction based on the selected project begins successfully");
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.DeleteVouchers))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.UpdateData();
            }
            GeneralLogger.TallyMigration.WriteLog("Tally Migration :Delete number of transaction based on the selected project ends successfully");
            return resultArgs;
        }

        public int CheckTransExists()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.CheckTransExists))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        #endregion
    }
}
