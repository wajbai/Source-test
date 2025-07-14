using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class AuditTypeSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.Audit).FullName)
            {
                query = GetAuditSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region ScriptSQL

        private string GetAuditSQL()
        {
            string query = "";
            SQLCommand.Audit sqlCommandId = (SQLCommand.Audit)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.Audit.Add:
                    {
                        query = "INSERT INTO MASTER_AUDIT_TYPE(AUDIT_TYPE)VALUES(?AUDIT_TYPE);";
                        break;
                    }

                case SQLCommand.Audit.Update:
                    {
                        query = "UPDATE MASTER_AUDIT_TYPE SET AUDIT_TYPE=?AUDIT_TYPE WHERE AUDIT_TYPE_ID=?AUDIT_TYPE_ID;";
                        break;
                    }

                case SQLCommand.Audit.Delete:
                    {
                        query = "DELETE FROM MASTER_AUDIT_TYPE WHERE AUDIT_TYPE_ID=?AUDIT_TYPE_ID;";
                        break;
                    }

                case SQLCommand.Audit.Fetch:
                    {
                        query = "SELECT AUDIT_TYPE_ID,AUDIT_TYPE FROM MASTER_AUDIT_TYPE WHERE AUDIT_TYPE_ID=?AUDIT_TYPE_ID;";
                        break;
                    }

                case SQLCommand.Audit.FetchAll:
                    {
                        query = "SELECT AUDIT_TYPE_ID,AUDIT_TYPE FROM MASTER_AUDIT_TYPE;";
                        break;
                    }
            }
            return query;


        }
        #endregion

    }
}

