using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

using Payroll.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;

namespace Payroll.Model.UIModel
{
    public class UserSystem : SystemBase
    {
        #region Declaration
        ApplicationSchema.UserDataTable dtUser = null;
        ResultArgs resultArgs = null;
        #endregion

        #region Properties
        private int userId;
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        private string roleId;
        public string RoleId
        {
            get { return roleId; }
            set { roleId = value; }
        }
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        private string mobileNo;
        public string MobileNo
        {
            get { return mobileNo; }
            set { mobileNo = value; }
        }
        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        #endregion

        #region Constructor
        public UserSystem()
        {
            dtUser = this.AppSchema.User;
        }

        public UserSystem(int UserId)
        {
            this.UserId = UserId;
          //  FillBankProperties();
        }
        #endregion

        #region Methods
        public ResultArgs AuthenticateUser(string userName, string password)
        {
            ResultArgs resultArgs = null;

            using (DataManager dataManager = new DataManager(SQLCommand.User.Authenticate))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtUser.USER_NAMEColumn, userName);
                dataManager.Parameters.Add(dtUser.PASSWORDColumn, password);
                dataManager.Parameters.Add(dtUser.STATUSColumn, (int)Status.Active);
                resultArgs = dataManager.FetchData(DataSource.DataView);

                if (resultArgs.Success)
                {
                    DataView dvUser = resultArgs.DataSource.TableView;
                    resultArgs.Success = (dvUser != null && dvUser.Count == 1);
                    if (!resultArgs.Success) { resultArgs.Message = MessageCatalog.User.USER_INVALID; }

                    if (resultArgs.Success)
                    {
                        //Keep logged in user info into session
                        this.UserInfo = dvUser;
                    }
                    else
                    {
                        this.UserInfo = null;
                        //resultArgs.Message = MessageCatalog.Message.Invalid_User;
                    }
                }
            }

            return resultArgs;
        }

        public ResultArgs GetUserSource()
        {
            ResultArgs resultArgs = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.User.FetchAll))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataView);
            }

            return resultArgs;
        }

        public ResultArgs GetUserSource(int userId)
        {
            ResultArgs resultArgs = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.User.Fetch))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtUser.USER_IDColumn, userId);
                resultArgs = dataManager.FetchData(DataSource.DataView);
            }

            return resultArgs;
        }

        public ResultArgs DeleteUser(int userId)
        {
            ResultArgs resultArgs = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.User.Delete))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtUser.USER_IDColumn, userId);
                resultArgs = dataManager.UpdateData();
            }

            return resultArgs;
        }

        //public ResultArgs SaveUser()
        //{
        //    using (DataManager dataManager = new DataManager(UserId.Equals(0) ? SQLCommand.User.Add : SQLCommand.User.Update))
        //    {
        //        dataManager.Parameters.Add(AppSchema.User.USER_IDColumn, UserId);
        //        dataManager.Parameters.Add(AppSchema.User.USER_NAMEColumn, UserName);
        //        dataManager.Parameters.Add(AppSchema.User.PASSWORDColumn, Password);
        //        dataManager.Parameters.Add(AppSchema.User.ADDRESSColumn, Address);
        //        dataManager.Parameters.Add(AppSchema.User.MOBILE_NOColumn, MobileNo);
        //        dataManager.Parameters.Add(AppSchema.User.EMAIL_IDColumn, Email);
        //        dataManager.Parameters.Add(AppSchema.User.USER_TYPEColumn, RoleId);
        //        resultArgs = dataManager.UpdateData();
        //    }
        //    return resultArgs;
        //}

        public ResultArgs DeleteUserDetails()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.User.Delete))
            {
                dataMember.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataMember.Parameters.Add(this.AppSchema.User.USER_IDColumn, UserId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchUserDetail()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.User.FetchAll))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        //private void FillBankProperties()
        //{
        //    resultArgs = UserDetailsById();
        //    DataTable dtUserEdit = resultArgs.DataSource.Table;
        //    if (dtUserEdit != null && dtUserEdit.Rows.Count > 0)
        //    {
        //        this.UserId = NumberSet.ToInteger(dtUserEdit.Rows[0][AppSchema.User.USER_IDColumn.ColumnName].ToString());
        //        UserName = dtUserEdit.Rows[0][AppSchema.User.USER_NAMEColumn.ColumnName].ToString();
        //        Password = dtUserEdit.Rows[0][AppSchema.User.PASSWORDColumn.ColumnName].ToString();
        //        RoleId = dtUserEdit.Rows[0][AppSchema.User.USER_TYPEColumn.ColumnName].ToString();
        //        Address = dtUserEdit.Rows[0][AppSchema.User.ADDRESSColumn.ColumnName].ToString();
        //        MobileNo = dtUserEdit.Rows[0][AppSchema.User.MOBILE_NOColumn.ColumnName].ToString();
        //        Email = dtUserEdit.Rows[0][AppSchema.User.EMAIL_IDColumn.ColumnName].ToString();
        //    }
        //}

        public ResultArgs UserDetailsById()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.User.Fetch))
            {
                dataMember.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataMember.Parameters.Add(this.AppSchema.User.USER_IDColumn, this.UserId);
                resultArgs = dataMember.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        #endregion
    }
}
