using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL 
{
    public class ReportCustomizationSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.CustomReport).FullName)
            {
                query = GetCustomizationQuery();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the Report Customization Details
        /// </summary>
        /// <returns></returns>
        private string GetCustomizationQuery()
        {
            string Query = string.Empty;
            SQLCommand.CustomReport sqlCommandId = (SQLCommand.CustomReport)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.CustomReport.FetchCustomReportByName:
                    {
                        Query = "SELECT SERIALIZED_REPORT FROM CUSTOM_REPORT WHERE REPORT_NAME=?REPORT_NAME;";
                        break;
                    }
                case SQLCommand.CustomReport.SaveCustomReport:
                    {
                        Query = "INSERT INTO CUSTOM_REPORT(SERIALIZED_REPORT,REPORT_NAME) VALUES(?SERIALIZED_REPORT,?REPORT_NAME);";
                        break;
                    }
                case SQLCommand.CustomReport.DeletePreviousReport:
                    {
                        Query = "DELETE FROM CUSTOM_REPORT WHERE REPORT_NAME=?REPORT_NAME;";
                        break;
                    }
            }
            return Query;
        }
        #endregion
       
    }
}
