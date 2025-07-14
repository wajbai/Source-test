using System;
using System.Collections.Generic;

using System.Text;
using System.Data;

using Payroll.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;

namespace Payroll.Model.UIModel
{
    public class PayrollWorkLocationSystem : SystemBase
    {
        #region Declaration
        private ResultArgs resultArgs = null;
        public Int32 PayrollWorkLocationId = 0;
        public string PayrollWorkLocation = string.Empty;
        #endregion

        public PayrollWorkLocationSystem()
        {
            resultArgs = new ResultArgs();
        }

        public PayrollWorkLocationSystem(Int32 worklocationid)
            : this()
        {
            PayrollWorkLocationId = worklocationid;
            FetchPayrollWorkLocation();
        }

        public ResultArgs FetchPayrollWorkLocation()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.PayrollWorkLocation.FetchPayrollWorkLocation,SQLAdapterType.PayrollSQL))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);

                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    resultArgs.DataSource.Table.DefaultView.Sort = this.AppSchema.PayrollWorkLocation.WORK_LOCATIONColumn.ColumnName;
                }
            }
            return resultArgs;
        }

        public ResultArgs FetchPayrollWorkLocationById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.PayrollWorkLocation.FetchPayrollWorkLocationById, SQLAdapterType.PayrollSQL))
            {
                dataManager.Parameters.Add(this.AppSchema.PayrollWorkLocation.WORK_LOCATION_IDColumn, PayrollWorkLocationId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            PayrollWorkLocation= string.Empty;
            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                PayrollWorkLocation = resultArgs.DataSource.Table.Rows[0][this.AppSchema.PayrollWorkLocation.WORK_LOCATIONColumn.ColumnName].ToString().Trim();
            }
            return resultArgs;
        }
        
        public ResultArgs SavePayrollWorkLocation()
        {
            using (DataManager dataManager = new DataManager(PayrollWorkLocationId == 0 ? SQLCommand.PayrollWorkLocation.InsertPayrollWorkLocation :
                SQLCommand.PayrollWorkLocation.UpdatePayrollWorkLocation, SQLAdapterType.PayrollSQL))
            {
                dataManager.Parameters.Add(this.AppSchema.PayrollWorkLocation.WORK_LOCATION_IDColumn, PayrollWorkLocationId);
                dataManager.Parameters.Add(this.AppSchema.PayrollWorkLocation.WORK_LOCATIONColumn, PayrollWorkLocation.Trim());
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeletePayrollWorkLocation()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.PayrollWorkLocation.DeletePayrollWorkLocation, SQLAdapterType.PayrollSQL))
            {
                dataManager.Parameters.Add(this.AppSchema.PayrollWorkLocation.WORK_LOCATION_IDColumn, PayrollWorkLocationId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        

    }
}
