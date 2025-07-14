using System;
using System.Collections.Generic;

using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Payroll.DAO.Schema;

namespace Payroll.SQL
{
    public class PayrollWorkLocationSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.PayrollWorkLocation).FullName)
            {
                query = GetPayrollWorkLocationSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region Payroll DepartmentSQL

        private string GetPayrollWorkLocationSQL()
        {
            string query = "";
            SQLCommand.PayrollWorkLocation sqlCommandId = (SQLCommand.PayrollWorkLocation)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.PayrollWorkLocation.FetchPayrollWorkLocation:
                    {
                        query = "SELECT WORK_LOCATION_ID, WORK_LOCATION FROM PR_WORK_LOCATION";
                        break;
                    }
                case SQLCommand.PayrollWorkLocation.FetchPayrollWorkLocationById:
                    {
                        query = "SELECT WORK_LOCATION_ID, WORK_LOCATION FROM PR_WORK_LOCATION WHERE WORK_LOCATION_ID=?WORK_LOCATION_ID";
                        break;
                    }
                case SQLCommand.PayrollWorkLocation.InsertPayrollWorkLocation:
                    {
                        query = "INSERT INTO PR_WORK_LOCATION (WORK_LOCATION_ID, WORK_LOCATION) VALUES (?WORK_LOCATION_ID, ?WORK_LOCATION)";
                        break;
                    }
                case SQLCommand.PayrollWorkLocation.UpdatePayrollWorkLocation:
                    {
                        query = "UPDATE PR_WORK_LOCATION SET WORK_LOCATION=?WORK_LOCATION WHERE WORK_LOCATION_ID=?WORK_LOCATION_ID";
                        break;
                    }
                case SQLCommand.PayrollWorkLocation.DeletePayrollWorkLocation:
                    {
                        query = "DELETE FROM PR_WORK_LOCATION WHERE WORK_LOCATION_ID=?WORK_LOCATION_ID;";
                        break;
                    }
            }

            return query;
        }
        #endregion Payroll Department SQL
    }
}
