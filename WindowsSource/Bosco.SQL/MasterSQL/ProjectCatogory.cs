using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Schema;
using Bosco.DAO.Data;

namespace Bosco.SQL
{
    public class ProjectCatogory : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.ProjectCatogory).FullName)
            {
                query = GetProjectCatogorySQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        #region SQL Script
        private string GetProjectCatogorySQL()
        {
            string query = "";
            SQLCommand.ProjectCatogory sqlCommandId = (SQLCommand.ProjectCatogory)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {
                case SQLCommand.ProjectCatogory.Add:
                    {
                        query = "INSERT INTO MASTER_PROJECT_CATOGORY ( " +
                            "PROJECT_CATOGORY_NAME,PROJECT_CATOGORY_ITRGROUP_ID) VALUES( " +
                            "?PROJECT_CATOGORY_NAME,?PROJECT_CATOGORY_ITRGROUP_ID)";
                        break;
                    }
                case SQLCommand.ProjectCatogory.Update:
                    {
                        query = "UPDATE MASTER_PROJECT_CATOGORY SET " +
                            "PROJECT_CATOGORY_NAME =?PROJECT_CATOGORY_NAME, PROJECT_CATOGORY_ITRGROUP_ID=?PROJECT_CATOGORY_ITRGROUP_ID " +
                            "WHERE PROJECT_CATOGORY_ID = ?PROJECT_CATOGORY_ID";

                        break;
                    }
                case SQLCommand.ProjectCatogory.Delete:
                    {
                        query = "DELETE FROM MASTER_PROJECT_CATOGORY WHERE PROJECT_CATOGORY_ID =?PROJECT_CATOGORY_ID";
                        break;
                    }
                case SQLCommand.ProjectCatogory.Fetch:
                    {
                        query = "SELECT " +
                        "PROJECT_CATOGORY_ID, " +
                        "PROJECT_CATOGORY_NAME " +
                        "FROM " +
                        "MASTER_PROJECT_CATOGORY WHERE PROJECT_CATOGORY_ID=?PROJECT_CATOGORY_ID";
                        break;
                    }
                case SQLCommand.ProjectCatogory.FetchAll:
                    {
                        query = "SELECT " +
                            "PROJECT_CATOGORY_ID, " +
                            "PROJECT_CATOGORY_NAME,PROJECT_CATOGORY_ITRGROUP " +
                            "FROM " +
                            "MASTER_PROJECT_CATOGORY MPC " +
                            " LEFT JOIN master_project_catogory_ITRGroup MPCITR ON MPCITR.PROJECT_CATOGORY_ITRGROUP_ID =MPC.PROJECT_CATOGORY_ITRGROUP_ID " +
                             " ORDER BY PROJECT_CATOGORY_NAME ASC";
                        break;
                    }
                case SQLCommand.ProjectCatogory.IsProjectCategory:
                    {
                        query = "SELECT COUNT(*) FROM MASTER_PROJECT_CATOGORY WHERE PROJECT_CATOGORY_NAME=?PROJECT_CATOGORY_NAME";
                        break;
                    }
                case SQLCommand.ProjectCatogory.FetchProjectCategoryId:
                    {
                        query = "SELECT PROJECT_CATOGORY_ID FROM MASTER_PROJECT_CATOGORY WHERE PROJECT_CATOGORY_NAME=?PROJECT_CATOGORY_NAME";
                        break;
                    }
            }
            return query;

        }
        #endregion

    }
}
