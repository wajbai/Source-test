using System;
using System.Collections.Generic;

using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Payroll.DAO.Schema;

namespace Payroll.SQL
{
    public class PayrollDepartmentSQL : IDatabaseQuery
    {
        #region ISQLServerQuery Members

        DataCommandArguments dataCommandArgs;
        SQLType sqlType;

        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.PayrollDepartment).FullName)
            {
                query = GetPayrollDepartmentSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region Payroll DepartmentSQL

        private string GetPayrollDepartmentSQL()
        {
            string query = "";
            SQLCommand.PayrollDepartment sqlCommandId = (SQLCommand.PayrollDepartment)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.PayrollDepartment.FetchPayrollDepartment:
                    {
                        query = "SELECT DEPARTMENT_ID, DEPARTMENT FROM PR_DEPARTMENT";
                        break;
                    }
                case SQLCommand.PayrollDepartment.FetchPayrollDepartmentById:
                    {
                        query = "SELECT DEPARTMENT_ID, DEPARTMENT FROM PR_DEPARTMENT WHERE DEPARTMENT_ID=?DEPARTMENT_ID";
                        break;
                    }
                case SQLCommand.PayrollDepartment.InsertPayrollDepartment:
                    {
                        query = "INSERT INTO PR_DEPARTMENT (DEPARTMENT_ID, DEPARTMENT) VALUES (?DEPARTMENT_ID, ?DEPARTMENT)";
                        break;
                    }
                case SQLCommand.PayrollDepartment.UpdatePayrollDepartment:
                    {
                        query = "UPDATE PR_DEPARTMENT SET DEPARTMENT_ID=?DEPARTMENT_ID WHERE DEPARTMENT_ID=?DEPARTMENT_ID";
                        break;
                    }
                case SQLCommand.PayrollDepartment.DeletePayrollDepartment:
                    {
                        query = "DELETE FROM PR_DEPARTMENT WHERE DEPARTMENT_ID=?DEPARTMENT_ID;";
                        break;
                    }
            }

            return query;
        }
        #endregion Payroll Department SQL
    }
}
