using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;

namespace Bosco.Model
{
    public class StockLedgerMapping : SystemBase
    {
        #region Variables
        ResultArgs resultArgs = new ResultArgs();
        private static DataView dvSetting = null;
        private const string SettingNameField = "NAME";
        private const string SettingValueField = "LEDGER_ID";
        private int EXPLedgerid = 0;
        private int INCLedgerid = 0;
        public int AccountLedgerId { get; set; }
        public int DepLedgerId { get; set; }
        public int DisposalLedgerId { get; set; }
        #endregion

        #region Constructor
        public StockLedgerMapping()
        {
            AssignStockLedgers();
        }
        #endregion

        #region Methods
        private int GetStockLedgerId(string name)
        {
            int val = 0;
            try
            {
                if (dvSetting != null && dvSetting.Count > 0)
                {
                    for (int i = 0; i < dvSetting.Count; i++)
                    {
                        string record = dvSetting[i][SettingNameField].ToString();

                        if (name == record)
                        {
                            val = this.NumberSet.ToInteger(dvSetting[i][SettingValueField].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }

            return val;
        }

        private void AssignStockLedgers()
        {
            resultArgs = FetchStockLedgersAll();
            if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
            {
                dvSetting = resultArgs.DataSource.Table.DefaultView;
                AccountLedgerId = GetStockLedgerId(StockLedgerType.IncomeLedger.ToString());
                DisposalLedgerId = GetStockLedgerId(StockLedgerType.ExpenseLedger.ToString());
            }
        }

        public ResultArgs FetchStockLedgersAll()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockLedgerMapping.FetchStockLedgers))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveStockLedger(DataTable dtStockLedger)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.StockLedgerMapping.SaveStockMappedLedgers))
                {
                    dataManager.BeginTransaction();

                    string SettingName = "";
                    int Value = 0;

                    DataTable dtSet = dtStockLedger;

                    if (dtSet != null)
                    {
                        EXPLedgerid = NumberSet.ToInteger(dtSet.Rows[0][2].ToString());
                        INCLedgerid = NumberSet.ToInteger(dtSet.Rows[1][2].ToString());
                        foreach (DataRow drSetting in dtSet.Rows)
                        {
                            SettingName = drSetting[this.AppSchema.Setting.NameColumn.ColumnName].ToString();
                            Value = this.NumberSet.ToInteger(drSetting[this.AppSchema.Setting.ValueColumn.ColumnName].ToString());

                            dataManager.Parameters.Add(this.AppSchema.Setting.NameColumn, SettingName);
                            dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, Value);

                            resultArgs = dataManager.UpdateData();
                            if (resultArgs.Success)
                            {
                                dataManager.Parameters.Clear();
                            }
                            else
                            {
                                break;
                            }
                        }
                        resultArgs = MapLedgerToAllItems();
                        if (!resultArgs.Success)
                        {
                            resultArgs.Message = "Ledgers are not maped to all the items.";
                        }
                    }
                    dataManager.EndTransaction();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Source + Environment.NewLine + ex.Message);
            }
            return resultArgs;
        }

        private ResultArgs MapLedgerToAllItems()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockLedgerMapping.MapLedgerToAllItems))
            {
                dataManager.Parameters.Add(this.AppSchema.StockItem.INCOME_LEDGER_IDColumn, INCLedgerid);
                dataManager.Parameters.Add(this.AppSchema.StockItem.EXPENSE_LEDGER_IDColumn, EXPLedgerid);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        #endregion
    }
}
