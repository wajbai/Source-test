using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using Bosco.Utility.ConfigSetting;

namespace Bosco.Model.UIModel
{

    public class SubLedgerSystem : SystemBase
    {
        #region Variable Declaration
        ResultArgs resultArgs = null;
        UISettingProperty setting = new UISettingProperty();
        #endregion

        public SubLedgerSystem()
        {
        }

        public SubLedgerSystem(int subLedgerId)
        {
            FillSubLedgerProperties(subLedgerId);
        }

        public string SubLedgerName { get; set; }
        public int SubLedgerId { get; set; }
        public int ProjectId { get; set; }
        public int LedgerId { get; set; }

        public void FillSubLedgerProperties(int SubLedgerId)
        {
            this.SubLedgerId = SubLedgerId;
            resultArgs = FetchSubLedgerDetailsById(SubLedgerId);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                SubLedgerName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.SubLedger.SUB_LEDGER_NAMEColumn.ColumnName].ToString();
            }
        }

        private ResultArgs FetchSubLedgerDetailsById(int SubLedgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.SubLedger.FetchBySubLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.SubLedger.SUB_LEDGER_IDColumn, SubLedgerId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchSubLedgerExists(int subLedgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.isExistsMonthlyBudget))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, subLedgerId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        public ResultArgs SaveSubLedgerDetails(DataManager dataManager)
        {
            using (DataManager datamanager = new DataManager(SubLedgerId == 0 ? SQLCommand.SubLedger.BudgetSubLedgerAdd : SQLCommand.SubLedger.BudgetSubLedgerEdit))
            {
                datamanager.Database = dataManager.Database;
                datamanager.Parameters.Add(this.AppSchema.SubLedger.SUB_LEDGER_IDColumn, SubLedgerId, true);
                datamanager.Parameters.Add(this.AppSchema.SubLedger.SUB_LEDGER_NAMEColumn, SubLedgerName);
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        public int isExistSubLedgerDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.SubLedger.IsExistSubLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.SubLedger.SUB_LEDGER_NAMEColumn, SubLedgerName);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs MapLedgerwithSubLedger(DataManager datamager)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.SubLedger.MapLedgerwithSubledger))
            {
                datamanager.Database = datamager.Database;
                datamanager.Parameters.Add(this.AppSchema.SubLedger.SUB_LEDGER_IDColumn, SubLedgerId);
                datamanager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteSubLedgerDetails()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.Budget.DeleteBudgetLedgerById))
            {
                datamanager.Parameters.Add(this.AppSchema.Budget.BUDGET_IDColumn, SubLedgerId);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgerDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.BindGrid))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ActiveProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBudgetLedgerByGroup()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchBudgetLedgerGroup))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
    }
}
