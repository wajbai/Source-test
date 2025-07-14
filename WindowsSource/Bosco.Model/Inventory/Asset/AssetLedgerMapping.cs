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
    public class AssetLedgerMapping : SystemBase
    {
        ResultArgs resultArgs = new ResultArgs();
        private static DataView dvSetting = null;
        private const string SettingNameField = "NAME";
        private const string SettingValueField = "LEDGER_ID";
        public int AccountLedgerId { get; set; }
        public int DepLedgerId { get; set; }
        public int DisposalLedgerId { get; set; }
        //public string Months { get; set; }

        public AssetLedgerMapping()
        {
            AssignAssetLedgers();
        }

        private int GetAssetLedgerId(string name)
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

        private void AssignAssetLedgers()
        {
            resultArgs = FetchAssetLedgersAll();
            if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
            {
                dvSetting = resultArgs.DataSource.Table.DefaultView;
                AccountLedgerId = GetAssetLedgerId(AssetLedgerType.AccountLedgerId.ToString());
                DepLedgerId = GetAssetLedgerId(AssetLedgerType.DepreciationLedgerId.ToString());
                DisposalLedgerId = GetAssetLedgerId(AssetLedgerType.DisposalLedgerId.ToString());
            }
        }

        //private void AssignAssetLedgers()
        //{
        //    resultArgs = FetchAssetLedgersAll();
        //    if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
        //    {
        //        dvSetting = resultArgs.DataSource.Table.DefaultView;
        //        Months = resultArgs.DataSource.Table.Rows[0]["MONTH"].ToString();
        //        AccountLedgerId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["ACCOUNT_LEDGER"].ToString());
        //        DepLedgerId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["DEPRECIATION_LEDGER"].ToString());
        //        DisposalLedgerId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["DISPOSAL_LEDGER"].ToString());
        //    }
        //}

        public ResultArgs FetchAssetLedgersAll()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetLedgerMapping.FetchAssetLedgerAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        //public ResultArgs SaveAssetLedger()
        //{
        //    using (DataManager dataManager = new DataManager(SQLCommand.AssetLedgerMapping.SaveAssetLedgers))
        //    {
        //        dataManager.Parameters.Add(this.AppSchema.Ledger.MONTHColumn, Months);
        //        dataManager.Parameters.Add(this.AppSchema.Ledger.ACCOUNT_LEDGERColumn, AccountLedgerId);
        //        dataManager.Parameters.Add(this.AppSchema.Ledger.DISPOSAL_LEDGERColumn, DisposalLedgerId);
        //        dataManager.Parameters.Add(this.AppSchema.Ledger.DEPRECIATION_LEDGERColumn, DepLedgerId);
        //        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
        //        resultArgs = dataManager.UpdateData();
        //    }
        //    return resultArgs;
        //}

        //public ResultArgs DeleteMappedLedger()
        //{
        //    using (DataManager dataManager = new DataManager(SQLCommand.AssetLedgerMapping.DeleteAssetMappedLedger))
        //    {
        //        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
        //        resultArgs = dataManager.UpdateData();
        //    }
        //    return resultArgs;
        //}

        public ResultArgs SaveAssetLedger(DataTable dtAssetLedger)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetLedgerMapping.SaveAssetLedgers))
            {
                dataManager.BeginTransaction();

                string SettingName = "";
                int Value = 0;

                DataTable dtSet = dtAssetLedger;

                if (dtSet != null)
                {
                    foreach (DataRow drSetting in dtSet.Rows)
                    {
                        SettingName = drSetting[this.AppSchema.Setting.NameColumn.ColumnName].ToString();
                        Value = this.NumberSet.ToInteger(drSetting[this.AppSchema.Setting.ValueColumn.ColumnName].ToString());
                        dataManager.Parameters.Add(this.AppSchema.Setting.NameColumn, SettingName);
                        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, Value);

                        resultArgs = dataManager.UpdateData();
                        if (!resultArgs.Success) { break; }
                        else
                            dataManager.Parameters.Clear();
                    }
                    dataManager.Parameters.Add(this.AppSchema.Ledger.MONTHColumn, Months);

                    resultArgs = dataManager.UpdateData();
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }
    }
}
