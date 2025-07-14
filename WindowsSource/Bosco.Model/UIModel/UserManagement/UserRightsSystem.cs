using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;

namespace Bosco.Model.UIModel
{
    public class UserRightsSystem : SystemBase
    {
        #region Variable Decelaration
        ResultArgs resultArgs = null;
        #endregion

        #region constructor
        public UserRightsSystem()
        {
        }
        #endregion

        #region Properties
        public int ActivityId { get; set; }
        public DataTable dtUserRights { get; set; }
        public DataTable dtUserProject { get; set; }
        public int UserRoleId { get; set; }
        #endregion

        #region Methods

        public ResultArgs FetchUserRights()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UserRights.FetchAll))
            {
                dataManager.Parameters.Add(this.AppSchema.UserRights.USER_ROLE_IDColumn, UserRoleId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        public ResultArgs FetchUserDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UserRights.Fetch))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchUserProject()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UserRights.FetchProjectMapped))
            {
                dataManager.Parameters.Add(this.AppSchema.UserRights.USER_ROLE_IDColumn, UserRoleId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveUserRights()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = DeleteUserRights(dataManager);
                if (resultArgs.Success)
                {
                    resultArgs = UpdateUserRigts(dataManager);
                    if (resultArgs.Success)
                    {
                        resultArgs = DeleteUserProject(dataManager);
                        if (resultArgs.Success)
                            resultArgs = UpdateUserProject(dataManager);
                    }

                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs DeleteUserRights(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UserRights.DeleteUserRights))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.UserRights.USER_ROLE_IDColumn, UserRoleId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteUserProject(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UserRights.DeleteUserProject))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.UserRights.USER_ROLE_IDColumn, UserRoleId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs UpdateUserRigts(DataManager dataManagers)
        {
            foreach (DataRow dr in dtUserRights.Rows)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.UserRights.UpdateUserRights))
                {
                    if (NumberSet.ToInteger(dr["HAS_RIGHTS"].ToString()).Equals(1))
                    {
                        dataManager.Database = dataManagers.Database;
                        dataManager.Parameters.Add(this.AppSchema.UserRights.USER_ROLE_IDColumn, UserRoleId);
                        dataManager.Parameters.Add(this.AppSchema.UserRights.ACTIVITY_IDColumn, NumberSet.ToInteger(dr[AppSchema.UserRights.ACTIVITY_IDColumn.ColumnName].ToString()));
                        resultArgs = dataManager.UpdateData();
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs UpdateUserProject(DataManager dataManagers)
        {
            foreach (DataRow dr in dtUserProject.Rows)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.UserRights.UpdateUserProject))
                {
                    if (NumberSet.ToInteger(dr["SELECT_COL"].ToString()).Equals(1))
                    {
                        dataManager.Database = dataManagers.Database;
                        dataManager.Parameters.Add(this.AppSchema.UserRights.USER_ROLE_IDColumn, UserRoleId);
                        dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, NumberSet.ToInteger(dr[AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString()));
                        resultArgs = dataManager.UpdateData();
                    }
                }
            }
            return resultArgs;
        }

        public DataTable FetchUserRightsDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UserRights.FetchUserRightsByRole))
            {
                dataManager.Parameters.Add(this.AppSchema.UserRights.USER_ROLE_IDColumn, UserRoleId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs.DataSource.Table != null ? resultArgs.DataSource.Table : null;

        }

        #endregion
    }
}
