using System;
using System.Collections.Generic;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Payroll.DAO.Schema;

namespace Payroll.SQL
{
    public class UserSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.User).FullName)
            {
                query = GetUserSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script

        private string GetUserSQL()
        {
            string query = "";
            SQLCommand.User sqlCommandId = (SQLCommand.User)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.User.Add:
                    {
                        query = "INSERT INTO USER_INFO " +
                                 "(USER_NAME,PASSWORD,NAME,ADDRESS,CONTACT_NO,EMAIL_ID,ROLE_ID)" +
                               " VALUES(?USER_NAME,?PASSWORD,?USER_NAME,?ADDRESS,?MOBILE_NO,?EMAIL_ID,?USER_TYPE);";
                        break;
                    }
                case SQLCommand.User.Update:
                    {
                        query = "UPDATE USER_INFO " +
                                "SET " +
                                       "USER_NAME=?USER_NAME," +
                                       "PASSWORD=?PASSWORD," +
                                       "NAME=?USER_NAME," +
                                       "ADDRESS=?ADDRESS," +
                                       "CONTACT_NO=?MOBILE_NO," +
                                       "EMAIL_ID=?EMAIL_ID," +
                                       "ROLE_ID=?USER_TYPE " +
                                 "WHERE USER_ID=?USER_ID;";
                        break;
                    }
                case SQLCommand.User.Delete:
                    {
                        query = "DELETE FROM USER_INFO WHERE USER_ID=?USER_ID;";
                        break;
                    }
                case SQLCommand.User.Fetch:
                    {
                        query = "SELECT USER_ID,USER_NAME, NAME, PASSWORD,ADDRESS,CONTACT_NO AS MOBILE_NO,EMAIL_ID,ROLE_ID AS USER_TYPE" +
                                " FROM USER_INFO " +
                                "WHERE USER_ID=?USER_ID;";
                        break;
                    }
                case SQLCommand.User.FetchAll:
                    {
                        query = "SELECT USER_ID,USER_NAME,PASSWORD,NAME,ADDRESS,CONTACT_NO,EMAIL_ID," +
                                 "(SELECT USERROLE FROM USER_ROLE WHERE USERROLE_ID= ROLE_ID) AS USERROLE" +
                                " FROM USER_INFO ORDER BY USER_NAME;";
                        break;
                    }
                case SQLCommand.User.Authenticate:
                    {
                        query = "SELECT USER_ID, USER_NAME,FULL_NAME, PASSWORD, " +
                                "ROLE_ID FROM MAS_USER " +
                                "WHERE USER_NAME = ?USER_NAME " +
                                "AND PASSWORD = ?PASSWORD AND STATUS = ?STATUS";
                        break;
                    }
            }

            return query;
        }
        #endregion User SQL
    }
}
