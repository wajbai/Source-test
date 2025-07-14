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
    public class UserRoleSystem : SystemBase
    {
        #region Variable Decelaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public UserRoleSystem()
        {

        }

        public UserRoleSystem(int userRoleId)
        {
            FillSaveUserRole(userRoleId);
        }

        #endregion

        #region Properties
        public int UserRoleId { get; set; }
        public string UserRoleName { get; set; }
        #endregion

        #region Methods
        public ResultArgs FetchUserRoleDetails()
        {
            using (DataManager dataManager = new DataManager(this.NumberSet.ToInteger(this.LoginUserId) != (int)UserRights.Admin ? SQLCommand.UserRole.FetchAll : SQLCommand.UserRole.FetchUserRole))
            {
                if (!IsFullRightsReservedUser)
                {
                    dataManager.Parameters.Add(this.AppSchema.User.USER_IDColumn, DefaultAdminUserId);
                    dataManager.Parameters.Add(this.AppSchema.UserRole.USERROLE_IDColumn, DefaultAuditorRoleId);
                }
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveUserRoles()
        {
            using (DataManager dataManager = new DataManager(UserRoleId == 0 ? SQLCommand.UserRole.Add : SQLCommand.UserRole.Edit))
            {
                dataManager.Parameters.Add(this.AppSchema.UserRole.USERROLE_IDColumn, UserRoleId,true);
                dataManager.Parameters.Add(this.AppSchema.UserRole.USERROLEColumn, UserRoleName);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FillSaveUserRole(int UserRoleId)
        {
            resultArgs = FetchUserDetailsbyId(UserRoleId);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                UserRoleName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.UserRole.USERROLEColumn.ColumnName].ToString();
            }
            return resultArgs;
        }

        public ResultArgs DeleteUserRole(int UserRoleID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UserRole.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.UserRole.USERROLE_IDColumn, UserRoleID);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        public ResultArgs FetchUserDetailsbyId(int UserRoleId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UserRole.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.UserRole.USERROLE_IDColumn, UserRoleId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;

        }

        /// <summary>
        /// On 27/08/2021, to get Auditor User Id
        /// </summary>
        /// <param name="UserRoleId"></param>
        /// <returns></returns>
        public ResultArgs FetchAuditorUserDetailsByName(string username)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UserRole.FetchAuditorUserDetailsByName))
            {
                dataManager.Parameters.Add(this.AppSchema.User.USER_NAMEColumn, username);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }

        #endregion
    }
}
