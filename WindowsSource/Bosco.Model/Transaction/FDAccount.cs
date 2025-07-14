using Bosco.DAO.Schema;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using System.Collections;
using System;
using Bosco.Model.UIModel;


namespace Bosco.Model.Transaction
{
    public class FDAccountSystem : SystemBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        #endregion
       
        #region Ledger Properties
        public int LedgerId { get; set; }
        public string LedgerCode { get; set; }
        public string LedgerName { get; set; }
        public int GroupId { get; set; }
        public int IsCostCentre { get; set; }
        public int IsBankInterestLedger { get; set; }
        public string LedgerType { get; set; }
        public string LedgerSubType { get; set; }
        public int BankAccountId { get; set; }
        public string AccountCode { get; set; }
        public string AccountNumber { get; set; }
        public string AccountHolderName { get; set; }
        public string AccountDate { get; set; }
        public int AccountTypeId { get; set; }
        public int BankId { get; set; }
        public string OpenedDate { get; set; }
        public string ClosedDate { get; set; }
        public string OperatedBy { get; set; }
        public string LedgerNotes { get; set; }
        public string BankAccNotes { get; set; }
        public int PeriodYr { get; set; }
        public int PeriodMth { get; set; }
        public int PeriodDay { get; set; }
        public decimal InterestRate { get; set; }
        public decimal Amount { get; set; }
        public string MaturityDate { get; set; }
        public int ProjectId { get; set; }
        public int SortId { get; set; }
        public int FDLedgerBankAccountId { get; set; }
        public DataTable dtMappingLedgers { get; set; }
        public bool FDLeger { get; set; }

        #endregion

        #region Methods
        public ResultArgs FetchLedger()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.FDAccount.FetchLedgers))
            {
                resultArgs = dataMember.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchProjectByLedger()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.FDAccount.FetchProjectByLedger))
            {
                resultArgs = dataMember.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs SaveFdLedger()
        {
            using (DataManager dataManager = new DataManager((LedgerId == 0) ? SQLCommand.LedgerBank.Add : SQLCommand.LedgerBank.Update))
            {
                dataManager.Database = ledgerManager.Database;
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId, true);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_CODEColumn, LedgerCode.ToUpper());
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, LedgerName);
                dataManager.Parameters.Add(this.AppSchema.Ledger.GROUP_IDColumn, GroupId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_TYPEColumn, LedgerType);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_SUB_TYPEColumn, LedgerSubType);
                dataManager.Parameters.Add(this.AppSchema.Ledger.BANK_ACCOUNT_IDColumn, BankAccountId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_COST_CENTERColumn, IsCostCentre);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_BANK_INTEREST_LEDGERColumn, IsBankInterestLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.NOTESColumn, LedgerNotes);
                dataManager.Parameters.Add(this.AppSchema.Ledger.SORT_IDColumn, SortId);
                resultArgs = dataManager.UpdateData();

                // dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public ResultArgs SaveProjectLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectLedgerMappingAdd))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        #endregion
    }
}