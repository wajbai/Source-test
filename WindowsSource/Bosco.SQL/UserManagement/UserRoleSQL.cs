using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class UserRoleSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.UserRole).FullName)
            {
                query = GetUserRole();
            }

            sqlType = this.sqlType;
            return query;
        }



        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the inkind article details.
        /// </summary>
        /// <returns></returns>
        private string GetUserRole()
        {
            string query = "";
            SQLCommand.UserRole sqlCommandId = (SQLCommand.UserRole)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.UserRole.Add:
                    {
                        query = " INSERT INTO USER_ROLE ( " +
                                " USERROLE)" +
                                " VALUES(?USERROLE)";
                        break;
                    }
                case SQLCommand.UserRole.Edit:
                    {
                        query = " UPDATE USER_ROLE SET " +
                                " USERROLE=?USERROLE" +
                                " WHERE USERROLE_ID=?USERROLE_ID";
                        break;
                    }

                case SQLCommand.UserRole.Delete:
                    {
                        query = "DELETE FROM USER_ROLE WHERE USERROLE_ID=?USERROLE_ID";
                        break;
                    }

                case SQLCommand.UserRole.Fetch:
                    {
                        query = " SELECT " +
                                " USERROLE_ID, " +
                                " USERROLE " +
                                " FROM " +
                                " USER_ROLE" +
                                " WHERE USERROLE_ID=?USERROLE_ID ";

                        break;
                    }
                case SQLCommand.UserRole.FetchAll:
                    {
                        //query = " SELECT " +
                        //        " USERROLE_ID, " +
                        //        " USERROLE " +
                        //        " FROM " +
                        //        " USER_ROLE" +
                        //        " WHERE USERROLE IS NOT NULL " +
                        //        " ORDER BY USERROLE_ID ASC";
                        query = " SELECT " +
                               " USERROLE_ID, " +
                               " USERROLE " +
                               " FROM " +
                               " USER_ROLE AS UR LEFT  JOIN USER_INFO AS UI ON UR.USERROLE_ID=UI.ROLE_ID " +
                               " WHERE USERROLE IS NOT NULL {AND UI.USER_ID NOT IN (?USER_ID)} {AND USERROLE_ID NOT IN (?USERROLE_ID)}" +
                               " ORDER BY USERROLE_ID ASC";
                        break;
                    }
                case SQLCommand.UserRole.FetchUserRole:
                    {
                        query = " SELECT " +
                                " USERROLE_ID, " +
                                " USERROLE " +
                                " FROM " +
                                " USER_ROLE" +
                                " WHERE USERROLE IS NOT NULL " +
                                " ORDER BY USERROLE_ID ASC";
                        break;
                    }
                case SQLCommand.UserRole.FetchAuditorUserDetailsByName:
                    {
                        query ="SELECT UI.USER_ID, UI.ROLE_ID\n" +
                                "FROM USER_INFO UI\n" +
                                "INNER JOIN USER_ROLE UR ON UR.USERROLE_ID = UI.ROLE_ID\n" +
                                "WHERE USER_NAME = ?USER_NAME";
                        break;
                    }
            }

            return query;
        }
        #endregion Bank SQL
    }
}
