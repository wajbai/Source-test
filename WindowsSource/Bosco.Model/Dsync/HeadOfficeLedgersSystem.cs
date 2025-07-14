using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.DAO.Schema;
using System.Data;

namespace Bosco.Model.Dsync
{
    public class HeadOfficeLedgersSystem : SystemBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        ImportMasterSystem import = new ImportMasterSystem();
        #endregion

        #region Properties

        private int AssignGroupId = 0;
        public int LedgerId { get; set; }
        public string LedgerCode { get; set; }
        public string LedgerName { get; set; }
        public int GroupId { get; set; }
        public int IsCostCentre { get; set; }
        public int IsBankInterestLedger { get; set; }
        public int IsFCRAAccount { get; set; }
        public string LedgerType { get; set; }
        public string LedgerSubType { get; set; }
        public int BankAccountId { get; set; }
        public string LedgerNotes { get; set; }
        public int SortId { get; set; }
        public int MasterLedgerId { get; set; }
        public int HeadOfficeLedgerId { get; set; }
        public string LedgerGroupName { get; set; }
        public int InsertGroupId { get; set; }
        public string GroupCode { get; set; }
        public string LedgerGroup { get; set; }
        public int ParentGroupId { get; set; }
        public int NatureId { get; set; }
        public int MainGroupId { get; set; }
        #endregion

        #region Constructor
        public HeadOfficeLedgersSystem()
        {
        }
        #endregion

        #region Methods
        private bool ShowHeadOfficeLedgerMapping(DataTable dtMisMatchedLedgers, DataTable dtModifiedLedgers)
        {
            bool isValid = true;
            if (dtMisMatchedLedgers != null && dtMisMatchedLedgers.Rows.Count > 0)
            {
                dtMisMatchedLedgers.Columns.Add("LEDGER_NAME", typeof(string));
               // frmMapHeadOfficeLedgers obj = new frmMapHeadOfficeLedgers(dtMisMatchedLedgers, dtModifiedLedgers);
               // obj.ShowDialog();
               // if (obj.DialogResult == DialogResult.Cancel)
              //  {
             //       isValid = false;
             //   }
            }
            return isValid;
        }
        public ResultArgs ImportLedgerDetails(DataManager dm, DataTable dtHeadOfficeLedger)
        {
            AcMELog.WriteLog("InsertLedger Started");
            try
            {
                DataTable dtBranchHeadOfficeLedgers = null;
                DataTable dtHeadOfficeLedgers = dtHeadOfficeLedger;

                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Database = dm.Database;
                    if (dtHeadOfficeLedgers.Rows.Count > 0)
                    {
                        resultArgs = FetchBrachHeadOfficeLedgers();
                        if (resultArgs.Success)
                        {
                            dtBranchHeadOfficeLedgers = resultArgs.DataSource.Table;

                            if (dtBranchHeadOfficeLedgers != null && dtBranchHeadOfficeLedgers.Rows.Count > 0)
                            {
                                DataTable dtMisMatchedLedgers = FetchMisMatchedLedgers(dtHeadOfficeLedgers, dtBranchHeadOfficeLedgers);
                                DataTable dtModifiedLedgers = FetchModifiedLedgers(dtHeadOfficeLedgers, dtBranchHeadOfficeLedgers);
                                resultArgs.Success = ShowHeadOfficeLedgerMapping(dtMisMatchedLedgers, dtModifiedLedgers);
                            }
                            else
                            {
                                resultArgs = SaveMasterHeadOfficeLedgers(dtHeadOfficeLedgers);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            finally { }
            AcMELog.WriteLog("InsertLedger Ended");
            return resultArgs;
        }


        public DataTable FetchMisMatchedLedgers(DataTable dtHeadOfficeLedgers, DataTable dtBrachHeadOfficeLedgers)
        {
            DataTable dtLedgers = new DataTable();

            var matched = from table1 in dtBrachHeadOfficeLedgers.AsEnumerable()
                          join table2 in dtHeadOfficeLedgers.AsEnumerable() on table1.Field<string>("HEADOFFICE_LEDGER_NAME") equals table2.Field<string>("LEDGER_NAME")
                          // where table1.Field<string>("LEDGER_GROUP") != table2.Field<string>("LEDGER_GROUP") //|| table1.Field<int>("ColumnB") != table2.Field<int>("ColumnB")
                          select table1;


            var missing = from table1 in dtBrachHeadOfficeLedgers.AsEnumerable()
                          where !matched.Contains(table1)
                          select table1;

            if (missing.Count() > 0)
            {
                dtLedgers = missing.CopyToDataTable();
            }
            return dtLedgers;
        }

        public DataTable FetchModifiedLedgers(DataTable dtHeadOfficeLedgers, DataTable dtBrachHeadOfficeLedgers)
        {
            DataTable dtLedgers = null;

            var matched = from table1 in dtHeadOfficeLedgers.AsEnumerable()
                          join table2 in dtBrachHeadOfficeLedgers.AsEnumerable() on table1.Field<string>("LEDGER_NAME") equals table2.Field<string>("HEADOFFICE_LEDGER_NAME")
                          //where table1.Field<string>("LEDGER_GROUP") != table2.Field<string>("LEDGER_GROUP")
                          select table1;


            var missing = from table1 in dtHeadOfficeLedgers.AsEnumerable()
                          where !matched.Contains(table1)
                          select table1;

            if (missing.Count() > 0)
            {
                dtLedgers = missing.CopyToDataTable();
            }
            return dtLedgers;
        }

        public bool HasLedgerEntries(int LedgerId)
        {
            bool isValid = true;
            int TransCount = FetchTransactionCount(LedgerId);
            double BalanceAmt = FetchLedgerBalance(LedgerId);
            double BudgetAmt = FetchBudgetLedgerAmount(LedgerId);

            if (TransCount > 0)
            {
                isValid = false;
            }
            else if (BalanceAmt > 0)
            {
                isValid = false;
            }
            else if (BudgetAmt > 0)
            {
                isValid = false;
            }
            return isValid;
        }

        private ResultArgs FetchBrachHeadOfficeLedgers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportLedger.FetchHeadOfficeLedgers))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int FetchTransactionCount(int LedgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportLedger.CheckTransactionCount))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.HEADOFFICE_LEDGER_IDColumn, LedgerId);

                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public double FetchLedgerBalance(int LedgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportLedger.CheckLedgerBalance))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);

                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return this.NumberSet.ToDouble(resultArgs.DataSource.Sclar.ToString());
        }

        public double FetchBudgetLedgerAmount(int LedgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportLedger.CheckBudgetLedgerAmount))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);

                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return this.NumberSet.ToDouble(resultArgs.DataSource.Sclar.ToString());
        }

        public ResultArgs SaveMasterHeadOfficeLedgers(DataTable dtLedger)
        {
            if (dtLedger != null && dtLedger.Rows.Count > 0)
            {
                foreach (DataRow drLedger in dtLedger.Rows)
                {
                    LedgerCode = drLedger[this.AppSchema.Ledger.LEDGER_CODEColumn.ColumnName].ToString();
                    LedgerName = drLedger[this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                    if (drLedger[this.AppSchema.LedgerGroup.LEDGER_GROUPColumn.ColumnName] != DBNull.Value)
                    {
                        LedgerGroupName = drLedger[this.AppSchema.LedgerGroup.LEDGER_GROUPColumn.ColumnName].ToString();
                    }
                    resultArgs = GetGroupId();
                    if (resultArgs.Success)
                    {
                        GroupId = AssignGroupId;
                        LedgerType = drLedger[this.AppSchema.Ledger.LEDGER_TYPEColumn.ColumnName].ToString();
                        LedgerSubType = drLedger[this.AppSchema.Ledger.LEDGER_SUB_TYPEColumn.ColumnName].ToString();
                        IsCostCentre = this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_COST_CENTERColumn.ColumnName].ToString());
                        IsBankInterestLedger = this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.IS_BANK_INTEREST_LEDGERColumn.ColumnName].ToString());
                        LedgerNotes = drLedger[this.AppSchema.Ledger.NOTESColumn.ColumnName].ToString();
                        SortId = this.NumberSet.ToInteger(drLedger[this.AppSchema.Ledger.SORT_IDColumn.ColumnName].ToString());
                        IsFCRAAccount = 0; //this.NumberSet.ToInteger(drLedger[this.AppSchema.BankAccount.IS_FCRA_ACCOUNTColumn.ColumnName].ToString());
                        resultArgs = SaveLedgerDetails(LedgerTable.MasterLedger);
                        if (resultArgs.Success)
                        {
                            LedgerId = this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                            resultArgs = SaveLedgerDetails(LedgerTable.HeadOfficeLedger);
                            if (resultArgs.Success)
                            {
                                HeadOfficeLedgerId = this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                resultArgs = MapHeadOfficeLedger();
                            }
                        }
                    }
                    if (!resultArgs.Success) { break; }
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// Insert Master Ledger Group Details in the Branch Office 
        /// </summary>
        /// <param name="dm"></param>
        /// <param name="dtGroup"></param>
        /// <returns></returns>
        public ResultArgs InsertMasterLedgerGroup(DataManager dm, DataTable dtGroup)
        {
            try
            {
                AcMELog.WriteLog("InsertMasterLedgerGroup Started");
                bool isLedgerGroupCodeExist = true;
                bool isLedgerGroup = true;
                if (dtGroup != null && dtGroup.Rows.Count > 0)
                {
                    foreach (DataRow drRowLedgerGroup in dtGroup.Rows)
                    {
                        using (DataManager dataManager = new DataManager(SQLCommand.ImportLedger.InsertMasterLedgerGroup))
                        {
                            dataManager.Database = dm.Database;
                            using (ImportMasterSystem importSystem = new ImportMasterSystem())
                            {
                                importSystem.ParentGroup = drRowLedgerGroup[this.AppSchema.LedgerGroup.ParentGroupColumn.ColumnName].ToString();
                                importSystem.Nature = drRowLedgerGroup[this.AppSchema.LedgerGroup.NATUREColumn.ColumnName].ToString();
                                importSystem.MainGroup = drRowLedgerGroup[this.AppSchema.LedgerGroup.MainGroupColumn.ColumnName].ToString();
                                GroupCode = drRowLedgerGroup[this.AppSchema.LedgerGroup.GROUP_CODEColumn.ColumnName].ToString();
                                LedgerGroupName = drRowLedgerGroup[this.AppSchema.LedgerGroup.LEDGER_GROUPColumn.ColumnName].ToString();
                                import.GroupCode = GroupCode;
                                import.LedgerGroup = LedgerGroupName;
                                if (!string.IsNullOrEmpty(GroupCode))
                                {
                                    resultArgs = import.IsExist(Bosco.Utility.DataSync.IsLedgerGroupCodeExists);
                                    if (resultArgs.Success)
                                    {
                                        isLedgerGroupCodeExist = resultArgs.DataSource.Sclar.ToInteger > 0;
                                        if (!isLedgerGroupCodeExist)
                                        {
                                            if (!string.IsNullOrEmpty(LedgerGroupName))
                                            {
                                                resultArgs = import.IsExist(Bosco.Utility.DataSync.IsLedgerGroupExists);
                                                if (resultArgs.Success)
                                                {
                                                    isLedgerGroup = resultArgs.DataSource.Sclar.ToInteger > 0;
                                                    if (!isLedgerGroup)
                                                    {
                                                        dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_CODEColumn, GroupCode);
                                                        dataManager.Parameters.Add(this.AppSchema.LedgerGroup.LEDGER_GROUPColumn, LedgerGroupName);
                                                        ParentGroupId = importSystem.GetMasterId(DataSync.ParentGroup);
                                                        NatureId = importSystem.GetMasterId(DataSync.Nature);
                                                        dataManager.Parameters.Add(this.AppSchema.LedgerGroup.PARENT_GROUP_IDColumn, ParentGroupId);
                                                        dataManager.Parameters.Add(this.AppSchema.LedgerGroup.NATURE_IDColumn, NatureId);
                                                        MainGroupId = importSystem.GetMasterId(DataSync.MainGroup);
                                                        dataManager.Parameters.Add(this.AppSchema.LedgerGroup.MAIN_GROUP_IDColumn, MainGroupId);
                                                        resultArgs = dataManager.UpdateData();
                                                        if (!resultArgs.Success)
                                                        {
                                                            resultArgs.Message = "Problem in InsertMasterLedgerGroup " + resultArgs.Message;
                                                            break;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    resultArgs.Message = "Error in LedgerGroup" + resultArgs.Message;
                                                }
                                            }
                                            else
                                            {
                                                resultArgs.Message = "Error in LedgerGroup Exisits " + resultArgs.Message;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        resultArgs.Message = "Error in Groupcode Exisits " + resultArgs.Message;
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
            finally { }
            AcMELog.WriteLog("InsertMasterLedgerGroup Ended");
            return resultArgs;
        }

        private ResultArgs GetGroupId()
        {
            using (ImportMasterSystem importMasterSystem = new ImportMasterSystem())
            {
                importMasterSystem.LedgerGroup = LedgerGroupName;
                resultArgs = import.IsExist(DataSync.IsLedgerGroupExists);
                if (resultArgs.Success)
                {
                    bool isLedgerGroup = resultArgs.DataSource.Sclar.ToInteger > 0;
                    if (!isLedgerGroup)
                    {
                        AssignGroupId = importMasterSystem.GetMasterId(DataSync.LedgerGroup);
                    }
                    else
                    {
                        resultArgs.Success = false;
                        resultArgs.Message = "There is no LedgerGroup in the Existing DataBase";
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs MapHeadOfficeLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportLedger.MapHeadOfficeLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.HEADOFFICE_LEDGER_IDColumn, HeadOfficeLedgerId);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs SaveLedgerDetails(LedgerTable ledgerTable)
        {
            using (DataManager dataManager = new DataManager((ledgerTable == LedgerTable.MasterLedger) ? SQLCommand.ImportLedger.InsertMasterLedger : SQLCommand.ImportLedger.InsertHeadOfficeLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId, true);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_CODEColumn, LedgerCode);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, LedgerName);
                dataManager.Parameters.Add(this.AppSchema.Ledger.GROUP_IDColumn, GroupId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_TYPEColumn, LedgerType);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_SUB_TYPEColumn, LedgerSubType);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_COST_CENTERColumn, IsCostCentre);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_BANK_INTEREST_LEDGERColumn, IsBankInterestLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.NOTESColumn, LedgerNotes);
                dataManager.Parameters.Add(this.AppSchema.Ledger.SORT_IDColumn, SortId);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.IS_FCRA_ACCOUNTColumn, IsFCRAAccount);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs FetchMappedLedgersById(int LedgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportLedger.FetchMappedLedgers))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.HEADOFFICE_LEDGER_IDColumn, LedgerId);

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        private ResultArgs DeleteMasterLedger(DataTable dtBranchLedger)
        {
            DataTable dtBranchLedgers = resultArgs.DataSource.Table;
            if (dtBranchLedgers.Rows.Count > 0)
            {
                foreach (DataRow dr in dtBranchLedger.Rows)
                {
                    LedgerId = this.NumberSet.ToInteger(dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString());

                    resultArgs = DeleteLedger(SQLCommand.ImportLedger.DeleteProjectMappedLedger);
                    if (resultArgs.Success)
                    {
                        resultArgs = DeleteLedger(SQLCommand.ImportLedger.DeleteLedgerBalance);
                        if (resultArgs.Success)
                        {
                            resultArgs = DeleteLedger(SQLCommand.ImportLedger.DeleteBudgetLedger);
                            if (resultArgs.Success)
                            {
                                resultArgs = DeleteLedger(SQLCommand.ImportLedger.DeleteHeadOfficeMappedLedger);
                                if (resultArgs.Success)
                                {
                                    resultArgs = DeleteLedger(SQLCommand.ImportLedger.DeleteMasterLedger);
                                }
                            }
                        }
                    }
                    if (!resultArgs.Success) { break; }
                }
            }
            return resultArgs;
        }

        private ResultArgs DeleteLedger(int LedgId)
        {
            resultArgs = FetchMappedLedgersById(LedgId);
            if (resultArgs.Success)
            {
                resultArgs = DeleteMasterLedger(resultArgs.DataSource.Table);
                if (resultArgs.Success)
                {
                    LedgerId = LedgId;
                    resultArgs = DeleteLedger(SQLCommand.ImportLedger.DeleteHeadOfficeLedger);
                }
            }
            return resultArgs;
        }

        private ResultArgs DeleteLedger(SQLCommand.ImportLedger Ledger)
        {
            string sQuery = string.Empty;
            using (DataManager dataManager = new DataManager())
            {
                switch (Ledger)
                {
                    case SQLCommand.ImportLedger.DeleteHeadOfficeLedger:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportLedger.DeleteHeadOfficeLedger;
                            break;
                        }
                    case SQLCommand.ImportLedger.DeleteHeadOfficeMappedLedger:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportLedger.DeleteHeadOfficeMappedLedger;
                            break;
                        }
                    case SQLCommand.ImportLedger.DeleteMasterLedger:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportLedger.DeleteMasterLedger;
                            break;
                        }
                    case SQLCommand.ImportLedger.DeleteProjectMappedLedger:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportLedger.DeleteProjectMappedLedger;
                            break;
                        }
                    case SQLCommand.ImportLedger.DeleteLedgerBalance:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportLedger.DeleteLedgerBalance;
                            break;
                        }
                    case SQLCommand.ImportLedger.DeleteBudgetLedger:
                        {
                            dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ImportLedger.DeleteBudgetLedger;
                            break;
                        }
                }
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs UpdateHeadOfficeLedgers(DataTable dtLedgers)
        {
            int HeadOfficeLedgerId = 0;
            string HeadOfficeLedgerName = string.Empty;
            string BranchHeadOfficeLedgerName = string.Empty;

            foreach (DataRow dr in dtLedgers.Rows)
            {
                HeadOfficeLedgerId = this.NumberSet.ToInteger(dr["HEADOFFICE_LEDGER_ID"].ToString());
                BranchHeadOfficeLedgerName = dr["HEADOFFICE_LEDGER_NAME"].ToString();
                HeadOfficeLedgerName = dr["LEDGER_NAME"].ToString();

                if (HeadOfficeLedgerName == "---Delete Ledger---")
                {
                    resultArgs = DeleteLedger(HeadOfficeLedgerId);
                }
                else
                {
                    resultArgs = UpdateMisMatchedLedgers(HeadOfficeLedgerName, BranchHeadOfficeLedgerName);
                }
                if (!resultArgs.Success) { break; }
            }
            return resultArgs;
        }

        private ResultArgs UpdateMisMatchedLedgers(string HeadOfficeLedgerName, string BranchHeadOfficeLedgerName)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportLedger.UpdateHeadOfficeLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, HeadOfficeLedgerName);
                dataManager.Parameters.Add(this.AppSchema.Ledger.HEADOFFICE_LEDGER_NAMEColumn, BranchHeadOfficeLedgerName);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        #endregion
    }
}
