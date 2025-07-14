using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.DAO;

namespace Bosco.SQL
{
    public class DonorRegistrationTypeSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.DonorRegistrationType).FullName)
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
            SQLCommand.DonorRegistrationType sqlCommandId = (SQLCommand.DonorRegistrationType)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {
                case SQLCommand.DonorRegistrationType.Add:
                    {
                        query = "INSERT INTO MASTER_DONAUD_REG_TYPE ( " +
                           "REGISTRATION_TYPE) VALUES( " +
                           "?REGISTRATION_TYPE)";
                        break;
                    }
                case SQLCommand.DonorRegistrationType.Update:
                    {
                        query = "UPDATE MASTER_DONAUD_REG_TYPE SET " +
                            "REGISTRATION_TYPE =?REGISTRATION_TYPE " +
                            "WHERE REGISTRATION_TYPE_ID = ?REGISTRATION_TYPE_ID";
                        break;
                    }
                case SQLCommand.DonorRegistrationType.Delete:
                    {
                        query = "DELETE FROM MASTER_DONAUD_REG_TYPE WHERE REGISTRATION_TYPE_ID =?REGISTRATION_TYPE_ID";
                        break;
                    }
                case SQLCommand.DonorRegistrationType.FetchAll:
                    {
                        query = "SELECT " +
                            "REGISTRATION_TYPE_ID, " +
                            "REGISTRATION_TYPE " +
                            "FROM " +
                            "MASTER_DONAUD_REG_TYPE ORDER BY REGISTRATION_TYPE ASC ";
                        break;
                    }
                case SQLCommand.DonorRegistrationType.FetchRegistrationTypeByID:
                    {
                        query = "SELECT " +
                            "REGISTRATION_TYPE_ID, " +
                            "REGISTRATION_TYPE " +
                            "FROM " +
                            "MASTER_DONAUD_REG_TYPE WHERE REGISTRATION_TYPE_ID=?REGISTRATION_TYPE_ID ";
                        break;
                    }
            }
            return query;
        }
    }
}