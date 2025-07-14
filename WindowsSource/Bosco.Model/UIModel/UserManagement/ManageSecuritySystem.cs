using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;

namespace Bosco.Model.UIModel
{
    public class ManageSecuritySystem : SystemBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        #endregion
        #region Properties
        public int UserId { get; set; }
        public int UserRoleId { get; set; }
        #endregion

        #region Methods
        public ResultArgs FetchUserRoles(bool includeReserveUsersRoles=false)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ManageSecurity.FetchUserRole))
            {
                //if (this.NumberSet.ToInteger(this.LoginUserId) != (int)UserRights.Admin)
                //{
                if (!includeReserveUsersRoles)
                {
                    dataManager.Parameters.Add(this.AppSchema.UserRole.USERROLE_IDColumn, "1," + DefaultAuditorRoleId);
                }
                //}
                //else
                //{
                //    dataManager.Parameters.Add(this.AppSchema.UserRole.USERROLE_IDColumn, 1);
                //}
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        //public ResultArgs FetchUserRolesForUsers()
        //{
        //    using (DataManager dataManager = new DataManager(SQLCommand.ManageSecurity.FetchUserRole))
        //    {
        //        //if (this.NumberSet.ToInteger(this.LoginUserId) != (int)UserRights.Admin)
        //        //{
        //        dataManager.Parameters.Add(this.AppSchema.UserRole.USERROLE_IDColumn, 1);
        //        //}
        //        resultArgs = dataManager.FetchData(DataSource.DataTable);
        //    }
        //    return resultArgs;
        //}
        public ResultArgs ManageSecurity()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ManageSecurity.Fetch))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs SaveManageSecurity()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ManageSecurity.Edit))
            {
                dataManager.Parameters.Add(this.AppSchema.User.USER_TYPEColumn, UserRoleId);
                dataManager.Parameters.Add(this.AppSchema.User.USER_IDColumn, UserId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        #endregion
    }
}
