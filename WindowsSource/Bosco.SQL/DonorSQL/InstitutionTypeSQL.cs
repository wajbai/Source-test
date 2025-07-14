using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.DAO.Schema;
using Bosco.DAO.Data;
using Bosco.DAO;

namespace Bosco.SQL
{
    public class InstitutionTypeSQL : IDatabaseQuery
    {
        #region ISQLServerQueryMembers
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;

        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.InstutionType).FullName)
            {
                query = GetProspectSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        public string GetProspectSQL()
        {
            string query = "";
            SQLCommand.InstutionType sqlCommandId = (SQLCommand.InstutionType)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {
                case SQLCommand.InstutionType.Add:
                    {
                        query = "INSERT INTO MASTER_DONAUD_INS_TYPE\n" +
                                "  (INSTITUTIONAL_TYPE)\n" +
                                "VALUES\n" +
                                "  (?INSTITUTIONAL_TYPE)";
                        break;
                    }

                case SQLCommand.InstutionType.Update:
                    {
                        query = "UPDATE MASTER_DONAUD_INS_TYPE\n" +
                                "   SET INSTITUTIONAL_TYPE = ?INSTITUTIONAL_TYPE\n" +
                                " WHERE INSTITUTIONAL_TYPE_ID = ?INSTITUTIONAL_TYPE_ID";

                        break;
                    }

                case SQLCommand.InstutionType.Delete:
                    {
                        query = "DELETE FROM MASTER_DONAUD_INS_TYPE\n" +
                                " WHERE INSTITUTIONAL_TYPE_ID = ?INSTITUTIONAL_TYPE_ID;";

                        break;
                    }

                case SQLCommand.InstutionType.FetchAll:
                    {
                        query = "SELECT INSTITUTIONAL_TYPE_ID, INSTITUTIONAL_TYPE\n" +
                                "  FROM MASTER_DONAUD_INS_TYPE";
                        break;
                    }
                case SQLCommand.InstutionType.FetchById:
                    {
                        query = "SELECT INSTITUTIONAL_TYPE_ID, INSTITUTIONAL_TYPE\n" +
                                "  FROM MASTER_DONAUD_INS_TYPE\n" +
                                " WHERE INSTITUTIONAL_TYPE_ID = ?INSTITUTIONAL_TYPE_ID";
                        break;
                    }
            }
            return query;
        }
    }
}
