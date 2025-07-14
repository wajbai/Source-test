using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

using Bosco.Utility;
using Bosco.Model.UIModel;
using ACPP.Modules.Master;
using ACPP.Modules;
using System.Runtime.InteropServices;

namespace ACPP.Modules.User_Management
{
    public partial class frmManageSecurity : frmFinanceBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        public string selectedLang = string.Empty;
        #endregion

        #region Constructor
        public frmManageSecurity()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        private string userName = string.Empty;
        private string UserName
        {
            get
            {
                userName = gvManageSecurity.GetFocusedRowCellValue(colUserName) != null ? gvManageSecurity.GetFocusedRowCellValue(colUserName).ToString() : string.Empty;
                return userName;
            }
            set
            {
                userName = value;
            }
        }
        #endregion

        #region Events
        private void frmManageSecurity_Load(object sender, EventArgs e)
        {
            selectedLang = this.AppSetting.LanguageId;
            if (selectedLang == "pt-PT")
            {
                this.Width = 900
                    ;
            }
            else if (selectedLang == "id-ID")
            {
                this.Width = 750;
            }
            else
            {
                this.Width = 621;
            }
            LoadLookUp();
            LoadUserDetails();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (ManageSecuritySystem manageSecurity = new ManageSecuritySystem())
            {
                DataView dvManageSecurity = ((DataTable)gcManageSecurity.DataSource).DefaultView;
                dvManageSecurity.RowStateFilter = DataViewRowState.ModifiedCurrent | DataViewRowState.Added;
                if (dvManageSecurity != null && dvManageSecurity.Count != 0)
                {
                    for (int i = 0; i < dvManageSecurity.Count; i++)
                    {
                        manageSecurity.UserId = this.UtilityMember.NumberSet.ToInteger(dvManageSecurity[i][manageSecurity.AppSchema.User.USER_IDColumn.ColumnName].ToString());
                        manageSecurity.UserRoleId = this.UtilityMember.NumberSet.ToInteger(dvManageSecurity[i][manageSecurity.AppSchema.UserRole.USERROLEColumn.ColumnName].ToString());
                        resultArgs = manageSecurity.SaveManageSecurity();
                        if (resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            LoadLookUp();
                            LoadUserDetails();
                        }
                    }
                    dvManageSecurity.RowStateFilter = DataViewRowState.None;
                    LoadLookUp();
                    LoadUserDetails();
                }
                else
                {
                    LoadLookUp();
                    LoadUserDetails();
                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                }
            }
           
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ribtnResetPassword_Click_1(object sender, EventArgs e)
        {
            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                if (CommonMethod.ApplyUserRights((int)ManageSecurity.ResetPassword) != 0)
                {
                    frmChangePassword frmResetPassword = new frmChangePassword(UserName, LoginUser.LoginUserRoleId);
                    frmResetPassword.ShowDialog();
                }
            }
            else
            {
                frmChangePassword frmResetPassword = new frmChangePassword(UserName, LoginUser.LoginUserRoleId);
                frmResetPassword.ShowDialog();
            }
        }
        #endregion

        #region Methods
        private void LoadUserDetails()
        {
            try
            {
                using (ManageSecuritySystem manageSecuritySystem = new ManageSecuritySystem())
                {
                    resultArgs = manageSecuritySystem.ManageSecurity();
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        if (!this.LoginUser.IsLoginUserDefaultAdminUser)
                        {
                            resultArgs.DataSource.Table.DefaultView.RowFilter = "USER_NAME ='" + this.LoginUser.LoginUserName + "'";
                        }
                        else //By default dont show auditor user to anyone
                        {
                            resultArgs.DataSource.Table.DefaultView.RowFilter = "USERROLE <> '" + this.AppSetting.DefaultAuditorRoleName + "'";
                        }

                        gcManageSecurity.DataSource = resultArgs.DataSource.Table;
                        gcManageSecurity.RefreshDataSource();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void LoadLookUp()
        {
            using (ManageSecuritySystem manageSecurityManagement = new ManageSecuritySystem())
            {
                resultArgs = manageSecurityManagement.FetchUserRoles();
                if (resultArgs.Success)
                {
                    glkpUserRole.DataSource = resultArgs.DataSource.Table;
                    glkpUserRole.DisplayMember = manageSecurityManagement.AppSchema.UserRole.USERROLEColumn.ColumnName.ToString();
                    glkpUserRole.ValueMember = manageSecurityManagement.AppSchema.UserRole.USERROLE_IDColumn.ColumnName.ToString();
                }
            }
        }
        #endregion




    }
}