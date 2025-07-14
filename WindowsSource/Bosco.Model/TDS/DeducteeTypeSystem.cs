using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using Bosco.Model.UIModel;
using Bosco.Model.Business;

namespace Bosco.Model.TDS
{
    public class DeducteeTypeSystem : SystemBase
    {
        #region Declaration
        ResultArgs resultArgs = null;

        #endregion

        #region Constructor
        public DeducteeTypeSystem()
        {
        }
        public DeducteeTypeSystem(int DutyTypeId)
        {
            this.DeducteeTypeid = DutyTypeId;
            FillDeducteeTypeProperties();
        }
        #endregion

        #region Properties
        public int DeducteeTypeid { get; set; }
        public int ResidentialStatus { get; set; }
        public int DeducteeStatus { get; set; }
        public int status { get; set; }
        public string DeducteeName { get; set; }
        public int PartyLedgerId { get; set; }

        #endregion

        #region Methods
        private void FillDeducteeTypeProperties()
        {
            resultArgs = FetchDeducteeTypeById();
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                DeducteeTypeid = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DeducteeTypes.DEDUCTEE_TYPE_IDColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DeducteeTypes.DEDUCTEE_TYPE_IDColumn.ColumnName].ToString()) : 0;
                ResidentialStatus = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DeducteeTypes.RESIDENTIAL_STATUSColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DeducteeTypes.RESIDENTIAL_STATUSColumn.ColumnName].ToString()) : 0;
                DeducteeStatus = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DeducteeTypes.DEDUCTEE_TYPEColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DeducteeTypes.DEDUCTEE_TYPEColumn.ColumnName].ToString()) : 0;
                status = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DeducteeTypes.STATUSColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DeducteeTypes.STATUSColumn.ColumnName].ToString()) : 0;
                DeducteeName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DeducteeTypes.NAMEColumn.ColumnName].ToString();
            }
        }

        private ResultArgs FetchDeducteeTypeById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DeducteeType.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.DeducteeTypes.DEDUCTEE_TYPE_IDColumn, DeducteeTypeid);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchDeducteeTypes()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DeducteeType.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchActiveDeductTypes()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DeducteeType.FetchActiveDeductTypes))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchDeductTypesByPartyLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DeducteeType.FetchDeductType))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.LEDGER_IDColumn, PartyLedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveDeducteeDetails()
        {
            using (DataManager dataManager = new DataManager(DeducteeTypeid == 0 ? SQLCommand.DeducteeType.Add : SQLCommand.DeducteeType.Update))
            {
                dataManager.Parameters.Add(this.AppSchema.DeducteeTypes.DEDUCTEE_TYPE_IDColumn, DeducteeTypeid);
                dataManager.Parameters.Add(this.AppSchema.DeducteeTypes.RESIDENTIAL_STATUSColumn, ResidentialStatus);
                dataManager.Parameters.Add(this.AppSchema.DeducteeTypes.DEDUCTEE_TYPEColumn, DeducteeStatus);
                dataManager.Parameters.Add(this.AppSchema.DeducteeTypes.STATUSColumn, status);
                dataManager.Parameters.Add(this.AppSchema.DeducteeTypes.NAMEColumn, DeducteeName);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteDeducteeTypeDetails()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.DeducteeType.Delete))
            {
                dataMember.Parameters.Add(this.AppSchema.DeducteeTypes.DEDUCTEE_TYPE_IDColumn, DeducteeTypeid);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public int ValidateDeducteeType()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.DeducteeType.CheckTransDeducteeType))
            {
                dataMember.Parameters.Add(this.AppSchema.DeducteeTypes.DEDUCTEE_TYPE_IDColumn, DeducteeTypeid);
                resultArgs = dataMember.FetchData(DataSource.Scalar);

            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        #endregion
    }
}
