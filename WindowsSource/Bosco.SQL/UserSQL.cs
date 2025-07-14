using System;
using System.Collections.Generic;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
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
                                 "(FIRSTNAME,LASTNAME, USER_NAME,PASSWORD,NAME,GENDER,ADDRESS,CONTACT_NO,EMAIL_ID,ROLE_ID,USER_PHOTO,NOTES)" +
                               " VALUES(?FIRSTNAME,?LASTNAME,?USER_NAME,?PASSWORD,?USER_NAME,?GENDER,?ADDRESS,?MOBILE_NO,?EMAIL_ID,?USER_TYPE,?USER_PHOTO,?NOTES);";
                        break;
                    }

                case SQLCommand.User.AddLogo:
                    {
                        query = "INSERT INTO ACMEERP_LOGO(LOGO) VALUES (?LOGO);";
                        break;
                    }
                case SQLCommand.User.DeleteLogo:
                    {
                        query = "DELETE FROM ACMEERP_LOGO;";
                        break;
                    }
                case SQLCommand.User.FetchLogo:
                    {
                        query = "SELECT LOGO FROM ACMEERP_LOGO;";
                        break;
                    }

                case SQLCommand.User.Update:
                    {
                        query = "UPDATE USER_INFO " +
                                "SET " +
                                        "FIRSTNAME=?FIRSTNAME," +
                                        "LASTNAME=?LASTNAME," +
                                        "USER_NAME=?USER_NAME," +
                                        "PASSWORD=?PASSWORD," +
                                        "NAME=?USER_NAME," +
                                        "GENDER=?GENDER," +
                                        "ADDRESS=?ADDRESS," +
                                        "CONTACT_NO=?MOBILE_NO," +
                                        "EMAIL_ID=?EMAIL_ID," +
                                        "ROLE_ID=?USER_TYPE," +
                                        "USER_PHOTO=?USER_PHOTO," +
                                        "NOTES=?NOTES " +
                                        "WHERE USER_ID=?USER_ID";
                        break;
                    }
                case SQLCommand.User.Delete:
                    {
                        query = "DELETE FROM USER_INFO WHERE USER_ID=?USER_ID;";
                        break;
                    }
                case SQLCommand.User.Fetch:
                    {
                        query = "SELECT USER_ID,FIRSTNAME,LASTNAME, USER_NAME,GENDER, NAME, PASSWORD,ADDRESS,CONTACT_NO AS MOBILE_NO,EMAIL_ID,ROLE_ID AS USER_TYPE,USER_PHOTO,NOTES" +
                                " FROM USER_INFO " +
                                "WHERE USER_ID=?USER_ID;";
                        break;
                    }
                case SQLCommand.User.FetchAll:
                    {
                        query = "SELECT USER_ID,FIRSTNAME,LASTNAME,USER_NAME,GENDER,PASSWORD,NAME,ADDRESS,CONTACT_NO,EMAIL_ID,USER_PHOTO," +
                                 "(SELECT USERROLE FROM USER_ROLE WHERE USERROLE_ID= ROLE_ID) AS USERROLE" +
                                " FROM USER_INFO ORDER BY USER_NAME ASC;";
                        break;
                    }
                case SQLCommand.User.CheckOldPassword:
                    {
                        query = "SELECT USER_ID FROM USER_INFO WHERE PASSWORD=?PASSWORD AND  USER_ID=?USER_ID";
                        break;
                    }
                case SQLCommand.User.FetchUserId:
                    {
                        query = "SELECT USER_ID FROM USER_INFO WHERE USER_NAME=?USER_NAME";
                        break;
                    }
                case SQLCommand.User.ResetPassword:
                    {
                        query = "UPDATE  USER_INFO SET PASSWORD=?PASSWORD WHERE USER_ID=?USER_ID";
                        break;
                    }
                case SQLCommand.User.Authenticate:
                    {
                        query = @"SELECT USER_ID,USERROLE, USER_NAME, NAME, PASSWORD,USER_PHOTO,FIRSTNAME, LASTNAME,UI.ROLE_ID
                                        FROM USER_INFO UI
                                        LEFT JOIN user_role UR ON UI.ROLE_ID=UR.USERROLE_ID
                                             WHERE USER_NAME =?USER_NAME
                                             AND PASSWORD =?PASSWORD AND STATUS = ?STATUS;";
                        break;
                    }

                case SQLCommand.User.FetchUserProfile:
                    {
                        query = "SELECT USER_ID,\n" +
                        "       USER_NAME,\n" +
                        "       NAME,\n" +
                        "       UR.USERROLE,\n" +
                        "       FIRSTNAME,\n" +
                        "       LASTNAME,\n" +
                        "       ADDRESS,\n" +
                        "       ROLE_ID,\n" +
                        "       CONTACT_NO,\n" +
                        "       EMAIL_ID,\n" +
                        "       USER_PHOTO\n" +
                        "  FROM USER_INFO AS UI\n" +
                        " INNER JOIN USER_ROLE AS UR\n" +
                        "    ON UI.ROLE_ID = UR.USERROLE_ID\n" +
                        " WHERE UI.USER_ID = ?USER_ID";

                        break;
                    }
                case SQLCommand.User.FetchAllShortcuts:
                    {
                        query = "SELECT ID,\n" +
                                "       SHORTCUT,\n" +
                                "       DESCRIPTION,\n" +
                                //"       MODULE_ID,\n" +
                                "       CASE\n" +
                                "         WHEN MODULE_ID = 0 THEN\n" +
                                "          \"COMMON\"\n" +
                                "         WHEN MODULE_ID = 1 THEN\n" +
                                "          \"FINANCE\"\n" +
                                "         WHEN MODULE_ID = 2 THEN\n" +
                                "          \"ASSET\"\n" +
                                "         WHEN MODULE_ID = 3 THEN\n" +
                                "          \"STOCK\"\n" +
                                "       END AS MODULE_ID\n" +
                                "  FROM SHORTCUTS";

                        break;
                    }
                case SQLCommand.User.FetchVoucherUsers:
                    {
                        query = "SELECT *  FROM (\n" +
                                "SELECT UN.USER_ID, UN.FIRSTNAME AS FIRSTNAME, UN.USER_NAME FROM USER_INFO AS UN\n" + 
                                "UNION\n" +
                                "SELECT VM.CREATED_BY AS USER_ID, VM.CREATED_BY_NAME AS FIRSTNAME, IFNULL(UC.USER_NAME, '') AS USER_NAME\n" + 
                                "FROM VOUCHER_MASTER_TRANS VM\n" + 
                                "LEFT JOIN USER_INFO UC ON UC.USER_ID = VM.CREATED_BY\n" +
                                "UNION SELECT VM.MODIFIED_BY AS USER_ID, VM.MODIFIED_BY_NAME AS FIRSTNAME, IFNULL(UM.USER_NAME, '') AS USER_NAME\n" + 
                                "FROM VOUCHER_MASTER_TRANS VM\n" +
                                "LEFT JOIN USER_INFO UM ON UM.USER_ID = VM.CREATED_BY) AS T GROUP BY T.FIRSTNAME\n" +
                                "ORDER BY If(USER_ID=1, 0, 1), FIRSTNAME";
                        break;
                    }

            }

            return query;
        }
        #endregion User SQL
    }
}
