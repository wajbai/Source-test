using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;

namespace Bosco.Model.UIModel
{
    public class UserSystem : SystemBase
    {
        #region Declaration
        ApplicationSchema.UserDataTable dtUser = null;
        ResultArgs resultArgs = null;
        #endregion

        #region Properties
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public int Gender { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public byte[] UserPhoto { get; set; }
        public byte[] ReportLogo { get; set; }
        public string Notes { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        #endregion

        #region Constructor
        public UserSystem()
        {
            dtUser = this.AppSchema.User;
        }

        public UserSystem(int UserId)
        {
            this.UserId = UserId;
            FillBankProperties();
        }
        #endregion

        #region Methods
        public ResultArgs AuthenticateUser(string userName, string password)
        {
            ResultArgs resultArgs = null;

            using (DataManager dataManager = new DataManager(SQLCommand.User.Authenticate))
            {
                dataManager.Parameters.Add(dtUser.USER_NAMEColumn, userName);
                dataManager.Parameters.Add(dtUser.PASSWORDColumn, password);
                dataManager.Parameters.Add(dtUser.STATUSColumn, (int)Status.Active);
                resultArgs = dataManager.FetchData(DataSource.DataView);

                if (resultArgs.Success)
                {
                    DataView dvUser = resultArgs.DataSource.TableView;
                    resultArgs.Success = (dvUser != null && dvUser.Count == 1);
                    //if (!resultArgs.Success) { resultArgs.Message = MessageCatalog.User.USER_INVALID; }

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
                resultArgs = dataManager.FetchData(DataSource.DataView);
            }

            return resultArgs;
        }
        public ResultArgs ResetPassword(string uid, string password)
        {
            ResultArgs resultArgs = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.User.ResetPassword))
            {
                dataManager.Parameters.Add(dtUser.USER_IDColumn, uid);
                dataManager.Parameters.Add(dtUser.PASSWORDColumn, password);
                resultArgs = dataManager.FetchData(DataSource.DataView);
            }

            return resultArgs;
        }

        public ResultArgs UpdatePassword(string uid, string password)
        {
            ResultArgs resultArgs = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.User.ResetPassword))
            {
                dataManager.Parameters.Add(dtUser.USER_IDColumn, uid);
                dataManager.Parameters.Add(dtUser.PASSWORDColumn, password);
                resultArgs = dataManager.UpdateData();
            }

            return resultArgs;
        }

        public ResultArgs CheckCurrentPassword(string uid, string password)
        {
            ResultArgs resultArgs = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.User.CheckOldPassword))
            {
                dataManager.Parameters.Add(dtUser.USER_IDColumn, uid);
                dataManager.Parameters.Add(dtUser.PASSWORDColumn, password);
                resultArgs = dataManager.FetchData(DataSource.DataView);
            }

            return resultArgs;
        }
        public ResultArgs FetchUserId(string uname)
        {
            ResultArgs resultArgs = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.User.FetchUserId))
            {
                dataManager.Parameters.Add(dtUser.USER_NAMEColumn, uname);
                resultArgs = dataManager.FetchData(DataSource.DataView);
            }

            return resultArgs;
        }
        public ResultArgs GetUserSource(int userId)
        {
            ResultArgs resultArgs = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.User.Fetch))
            {
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
                dataManager.Parameters.Add(dtUser.USER_IDColumn, userId);
                resultArgs = dataManager.UpdateData();
            }

            return resultArgs;
        }

        public ResultArgs SaveUser()
        {
            using (DataManager dataManager = new DataManager(UserId.Equals(0) ? SQLCommand.User.Add : SQLCommand.User.Update))
            {
                dataManager.Parameters.Add(AppSchema.User.USER_IDColumn, UserId);
                dataManager.Parameters.Add(AppSchema.User.FIRSTNAMEColumn, FirstName);
                dataManager.Parameters.Add(AppSchema.User.LASTNAMEColumn, LastName);
                dataManager.Parameters.Add(AppSchema.User.USER_NAMEColumn, UserName);
                dataManager.Parameters.Add(AppSchema.User.GENDERColumn, Gender);
                dataManager.Parameters.Add(AppSchema.User.PASSWORDColumn, Password);
                dataManager.Parameters.Add(AppSchema.User.ADDRESSColumn, Address);
                dataManager.Parameters.Add(AppSchema.User.MOBILE_NOColumn, MobileNo);
                dataManager.Parameters.Add(AppSchema.User.EMAIL_IDColumn, Email);
                dataManager.Parameters.Add(AppSchema.User.USER_TYPEColumn, RoleId);
                dataManager.Parameters.Add(AppSchema.User.USER_PHOTOColumn, UserPhoto);
                dataManager.Parameters.Add(AppSchema.User.NOTESColumn, Notes);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveLogo()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.User.AddLogo))
            {
                dataManager.Parameters.Add(AppSchema.User.LOGOColumn, ReportLogo);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteLogo()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.User.DeleteLogo))
            {
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchLogo()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.User.FetchLogo))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                
            }
            return resultArgs;
        }

        public ResultArgs DeleteUserDetails()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.User.Delete))
            {
                dataMember.Parameters.Add(this.AppSchema.User.USER_IDColumn, UserId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchUserDetail()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.User.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private void FillBankProperties()
        {
            resultArgs = UserDetailsById();
            DataTable dtUserEdit = resultArgs.DataSource.Table;
            if (dtUserEdit != null && dtUserEdit.Rows.Count > 0)
            {
                FirstName = dtUserEdit.Rows[0][AppSchema.User.FIRSTNAMEColumn.ColumnName].ToString();
                LastName = dtUserEdit.Rows[0][AppSchema.User.LASTNAMEColumn.ColumnName].ToString();
                this.UserId = NumberSet.ToInteger(dtUserEdit.Rows[0][AppSchema.User.USER_IDColumn.ColumnName].ToString());
                UserName = dtUserEdit.Rows[0][AppSchema.User.USER_NAMEColumn.ColumnName].ToString();
                this.Gender = NumberSet.ToInteger(dtUserEdit.Rows[0][AppSchema.User.GENDERColumn.ColumnName].ToString());
                Password = dtUserEdit.Rows[0][AppSchema.User.PASSWORDColumn.ColumnName].ToString();
                RoleId = NumberSet.ToInteger(dtUserEdit.Rows[0][AppSchema.User.USER_TYPEColumn.ColumnName].ToString());
                Address = dtUserEdit.Rows[0][AppSchema.User.ADDRESSColumn.ColumnName].ToString();
                MobileNo = dtUserEdit.Rows[0][AppSchema.User.MOBILE_NOColumn.ColumnName].ToString();
                Email = dtUserEdit.Rows[0][AppSchema.User.EMAIL_IDColumn.ColumnName].ToString();
                UserPhoto = dtUserEdit.Rows[0][AppSchema.User.USER_PHOTOColumn.ColumnName].ToString().Equals(string.Empty) ? UserPhoto : (byte[])dtUserEdit.Rows[0][AppSchema.User.USER_PHOTOColumn.ColumnName];
                Notes = dtUserEdit.Rows[0][AppSchema.User.NOTESColumn.ColumnName].ToString();
            }
        }

        public ResultArgs UserDetailsById()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.User.Fetch))
            {
                dataMember.Parameters.Add(this.AppSchema.User.USER_IDColumn, this.UserId);
                resultArgs = dataMember.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchUserProfile()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.User.FetchUserProfile))
            {
                dataMember.Parameters.Add(this.AppSchema.User.USER_IDColumn, this.LoginUserId);
                resultArgs = dataMember.FetchData(DataSource.DataTable);
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    this.UserInfo = resultArgs.DataSource.Table.DefaultView;
                }
            }
            return resultArgs;
        }

        public ResultArgs FetchAllShortcuts()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.User.FetchAllShortcuts))
            {
                resultArgs = dataMember.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchChequeSetting(Int32 bankid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Setting.FetchChequePrintingSetting))
            {
                dataManager.Parameters.Add(this.AppSchema.ChequePrinting.BANK_IDColumn, bankid);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }

        public ResultArgs DeleteChequeSetting(Int32 bankid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Setting.DeleteChequePrintingSetting))
            {
                dataManager.Parameters.Add(this.AppSchema.ChequePrinting.BANK_IDColumn, bankid);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }

        public ResultArgs SaveChequeSetting(Int32 bankid, string setting, string value)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Setting.InsertUpdateChequePrintingSetting))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(this.AppSchema.ChequePrinting.BANK_IDColumn, bankid);
                dataManager.Parameters.Add(this.AppSchema.ChequePrinting.SETTING_NAMEColumn, setting);
                dataManager.Parameters.Add(this.AppSchema.ChequePrinting.SETTING_VALUEColumn, value);

                resultArgs = dataManager.UpdateData();
            }

            return resultArgs;
        }

        #endregion
    }
}
