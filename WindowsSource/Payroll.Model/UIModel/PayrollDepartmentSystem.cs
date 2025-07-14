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
    public class PayrollDepartmentSystem : SystemBase
    {
        #region Declaration
        private ResultArgs resultArgs = null;
        public Int32 DepartmentId = 0;
        public string Department = string.Empty;
        #endregion

        public PayrollDepartmentSystem()
        {
            resultArgs = new ResultArgs();
        }

        public PayrollDepartmentSystem(Int32 DeptId)
            : this()
        {
            DepartmentId = DeptId;
            FetchPayrollDepartmentById();
        }

        public ResultArgs FetchPayrollDepartments()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.PayrollDepartment.FetchPayrollDepartment,SQLAdapterType.PayrollSQL))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                resultArgs.DataSource.Table.DefaultView.Sort = this.AppSchema.PayrollDepartment.DEPARTMENTColumn.ColumnName;
            }
            return resultArgs;
        }

        public ResultArgs FetchPayrollDepartmentById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.PayrollDepartment.FetchPayrollDepartmentById, SQLAdapterType.PayrollSQL))
            {
                dataManager.Parameters.Add(this.AppSchema.PayrollDepartment.DEPARTMENT_IDColumn, DepartmentId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            Department= string.Empty;
            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                Department = resultArgs.DataSource.Table.Rows[0][this.AppSchema.PayrollDepartment.DEPARTMENTColumn.ColumnName].ToString().Trim();
            }
            return resultArgs;
        }
        
        public ResultArgs SavePayrollDepartments()
        {
            using (DataManager dataManager = new DataManager(DepartmentId == 0 ? SQLCommand.PayrollDepartment.InsertPayrollDepartment : 
                SQLCommand.PayrollDepartment.UpdatePayrollDepartment, SQLAdapterType.PayrollSQL))
            {
                dataManager.Parameters.Add(this.AppSchema.PayrollDepartment.DEPARTMENT_IDColumn, DepartmentId);
                dataManager.Parameters.Add(this.AppSchema.PayrollDepartment.DEPARTMENTColumn, Department.Trim());
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeletePayrollDepartments()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.PayrollDepartment.DeletePayrollDepartment, SQLAdapterType.PayrollSQL))
            {
                dataManager.Parameters.Add(this.AppSchema.PayrollDepartment.DEPARTMENT_IDColumn, DepartmentId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

    }
}
