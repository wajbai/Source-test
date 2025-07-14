using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class TDSSectionSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.TDSSection).FullName)
            {
                query = GetTDSSectionSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script

        private string GetTDSSectionSQL()
        {
            string query = "";
            SQLCommand.TDSSection sqlCommandId = (SQLCommand.TDSSection)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.TDSSection.Add:
                    {
                        query = "INSERT INTO TDS_SECTION(CODE, SECTION_NAME,STATUS)VALUES(?CODE,?SECTION_NAME,?STATUS)";
                        break;
                    }

                case SQLCommand.TDSSection.Update:
                    {
                        query = "UPDATE TDS_SECTION SET CODE=?CODE,SECTION_NAME=?SECTION_NAME,STATUS=?STATUS WHERE TDS_SECTION_ID=?TDS_SECTION_ID";
                        break;
                    }
                case SQLCommand.TDSSection.Delete:
                    {
                        query = "DELETE FROM TDS_SECTION WHERE TDS_SECTION_ID=?TDS_SECTION_ID";
                        break;
                    }
                case SQLCommand.TDSSection.FetchAll:
                    {
                        query = "SELECT TDS_SECTION_ID,\n" +
                        "       CODE,\n" +
                        "       SECTION_NAME,\n" +
                        "       CASE\n" +
                        "         WHEN STATUS = 1 THEN\n" +
                        "          'Active'\n" +
                        "         ELSE\n" +
                        "          'Inactive'\n" +
                        "       END AS STATUS\n" +
                        "  FROM TDS_SECTION ORDER BY CODE ASC";
                        break;
                    }
                case SQLCommand.TDSSection.Fetch:
                    {
                        query = "SELECT CODE,SECTION_NAME,STATUS FROM TDS_SECTION WHERE TDS_SECTION_ID=?TDS_SECTION_ID";
                        break;
                    }
                case SQLCommand.TDSSection.TDSSection:
                    {
                        query = "SELECT COUNT(*) AS TDS_SECTION_COUNT FROM TDS_NATURE_PAYMENT WHERE TDS_SECTION_ID =?TDS_SECTION_ID";
                        break;
                    }
            }

            return query;
        }
        #endregion TDS SQL
    }
}
