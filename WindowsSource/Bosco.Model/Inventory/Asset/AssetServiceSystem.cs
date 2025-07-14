using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.Model
{
    public class AssetServiceSystem : SystemBase
    {
        #region Variable Declearation
        ResultArgs resultArgs = null;
        # endregion

        #region Constructor
        public AssetServiceSystem()
        {
        }

        public AssetServiceSystem(int ServiceId)
        {
            this.ServiceId = ServiceId;
            AssignMaintanceService();
        }
        #endregion

        #region properties
        public int ServiceId { get; set; }
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        #endregion

        #region Methods

        public ResultArgs SaveMaintanceService()
        {
            using (DataManager datamanager = new DataManager((ServiceId == 0) ? SQLCommand.AssetService.Add : SQLCommand.AssetService.Update))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSSETerviceDetails.SERVICE_IDColumn, ServiceId);
                datamanager.Parameters.Add(this.AppSchema.ASSSETerviceDetails.SERVICE_CODEColumn, ServiceCode);
                datamanager.Parameters.Add(this.AppSchema.ASSSETerviceDetails.NAMEColumn, ServiceName);
                datamanager.Parameters.Add(this.AppSchema.ASSSETerviceDetails.DESCRIPTIONColumn, Description);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchMaintanceService()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetService.FetchAll))
            {
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteService()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetService.Delete))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSSETerviceDetails.SERVICE_IDColumn, this.ServiceId);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchServiceDetailsById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetService.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSSETerviceDetails.SERVICE_IDColumn, this.ServiceId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public void AssignMaintanceService()
        {
            resultArgs = FetchServiceDetailsById();
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                ServiceCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSSETerviceDetails.SERVICE_CODEColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSSETerviceDetails.SERVICE_CODEColumn.ColumnName].ToString() : string.Empty;
                ServiceName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSSETerviceDetails.NAMEColumn.ColumnName].ToString();
                Description = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSSETerviceDetails.DESCRIPTIONColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSSETerviceDetails.DESCRIPTIONColumn.ColumnName].ToString() : string.Empty;
            }
        }

        #endregion
    }
}
