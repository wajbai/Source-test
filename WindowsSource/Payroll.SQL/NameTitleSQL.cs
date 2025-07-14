using System;
using System.Collections.Generic;

using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Payroll.DAO.Schema;

namespace Payroll.SQL
{
    public class NameTitleSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.NameTitle).FullName)
            {
                query = GetNameTitleSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region Payroll Staff NameTitle SQL
        private string GetNameTitleSQL()
        {
            string query = "";
            SQLCommand.NameTitle sqlCommandId = (SQLCommand.NameTitle)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.NameTitle.FetchNameTitle:
                    {
                        query = "SELECT NAME_TITLE_ID, NAME_TITLE FROM PR_NAME_TITLE";
                        break;
                    }
                case SQLCommand.NameTitle.FetchNameTitleById:
                    {
                        query = "SELECT NAME_TITLE_ID, NAME_TITLE FROM PR_NAME_TITLE WHERE NAME_TITLE_ID=?NAME_TITLE_ID";
                        break;
                    }
                case SQLCommand.NameTitle.InsertNameTitle:
                    {
                        query = "INSERT INTO PR_NAME_TITLE (NAME_TITLE_ID, NAME_TITLE) VALUES (?NAME_TITLE_ID, ?NAME_TITLE)";
                        break;
                    }
                case SQLCommand.NameTitle.UpdateNameTitle:
                    {
                        query = "UPDATE PR_NAME_TITLE SET NAME_TITLE=?NAME_TITLE WHERE NAME_TITLE_ID = ?NAME_TITLE_ID";
                        break;
                    }
                case SQLCommand.NameTitle.DeleteNameTitle:
                    {
                        query = "DELETE FROM PR_NAME_TITLE WHERE NAME_TITLE_ID=?NAME_TITLE_ID;";
                        break;
                    }
            }

            return query;
        }
        #endregion Payroll Staff NameTitle SQL
    }
}
