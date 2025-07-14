using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using System.Data;

namespace Bosco.Model.Transaction
{
    public class DenominationSystem : SystemBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        #endregion

        #region Properties
        public int DenominaitonID { get; set; }
        public int VoucherID { get; set; }
        public int DenominationLedgerID { get; set; }
        public int VoucherNo { get; set; }
        public DataTable dtDenomination { get; set; }
        #endregion

        #region Method


        public ResultArgs SaveDenominationDetails()
        {
            try
            {
                int SequenceNo = 1;
                if (dtDenomination.Rows.Count > 0 && dtDenomination != null)
                {
                    foreach (DataRow drDenomination in dtDenomination.Rows)
                    {
                        int count = this.NumberSet.ToInteger(drDenomination["COUNT"].ToString());
                        if (count == 0 || count == null)
                        {
                            break;
                        }
                        using (DataManager dataManager = new DataManager(SQLCommand.Denomination.Add))
                        {
                            dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_IDColumn, VoucherID);
                            dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, DenominationLedgerID);
                            dataManager.Parameters.Add(this.AppSchema.Denomination.SEQUENCE_IDColumn, SequenceNo);
                            dataManager.Parameters.Add(this.AppSchema.Denomination.DENOMINATION_IDColumn, drDenomination[this.AppSchema.Denomination.DENOMINATION_IDColumn.ColumnName]);
                            dataManager.Parameters.Add(this.AppSchema.Denomination.COUNTColumn, drDenomination[this.AppSchema.Denomination.COUNTColumn.ColumnName]);
                            dataManager.Parameters.Add(this.AppSchema.Denomination.AMOUNTColumn, drDenomination[this.AppSchema.Denomination.AMOUNTColumn.ColumnName]);
                            resultArgs = dataManager.UpdateData();
                            SequenceNo++;
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

        public ResultArgs FetchDenomination()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.Denomination.FetchDenomination))
            {
                resultArgs = dataMember.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchDenominationByID()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Denomination.FetchDenominationByID))
            {
                dataManager.Parameters.Add(AppSchema.Voucher.VOUCHER_IDColumn,VoucherID);
                dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_IDColumn, DenominationLedgerID);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion
    }
}
