using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using ACPP.Modules.Master;
using Bosco.Utility;
using Bosco.Model.UIModel;
using Bosco.Utility.CommonMemberSet;

namespace ACPP.Modules.User_Management
{
    public partial class frmUserView : frmFinanceBase
    {
        #region Variables
        private int RowIndex = 0;

        #endregion

        #region Property
        private int _UserId = 0;
        private int UserId
        {
            get
            {
                RowIndex = gvUserView.FocusedRowHandle;
                if (gvUserView.RowCount != 0)
                {
                    _UserId = gvUserView.GetFocusedRowCellValue(colUserId) != null ? this.UtilityMember.NumberSet.ToInteger(gvUserView.GetFocusedRowCellValue(colUserId).ToString()) : 0;
                }
                return _UserId;
            }
        }

        #endregion

        #region Constructors
        public frmUserView()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void frmUserView_Load(object sender, EventArgs e)
        {
            //Added by Carmel Raj
            SetVisibileShortCuts(false, false);
            LoadUserDetails();
            ApplyUserRights();
        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(User.CreateUser);
            this.enumUserRights.Add(User.EditUser);
            this.enumUserRights.Add(User.DeleteUser);
            this.enumUserRights.Add(User.PrintUser);
            this.enumUserRights.Add(User.ViewUser);
            this.ApplyUserRights(ucToolBarUserMenu, this.enumUserRights, (int)Menus.User);
        }

        private void ucToolBarUserMenu_AddClicked(object sender, EventArgs e)
        {
            ShowUserForm();
        }

        private void ucToolBarUserMenu_EditClicked(object sender, EventArgs e)
        {
            EditUser();
        }

        private void gcUserView_DoubleClick(object sender, EventArgs e)
        {
            EditUser();
        }

        private void ucToolBarUserMenu_DeleteClicked(object sender, EventArgs e)
        {
            DeleteUser();
        }

        private void ucToolBarUserMenu_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcUserView, this.GetMessage(MessageCatalog.User.USER_PRINT_CAPTION), PrintType.DT, gvUserView, true);
        }

        private void gvUserView_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvUserView.RowCount.ToString();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvUserView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvUserView, colUserName);
            }
        }

        private void ucToolBarUserMenu_CloseClicked(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Methods

        private void LoadUserDetails()
        {
            try
            {
                using (UserSystem userSystem = new UserSystem())
                {
                    ResultArgs resultArgs = userSystem.FetchUserDetail();
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        if (!this.LoginUser.IsFullRightsReservedUser)
                        {
                            resultArgs.DataSource.Table.DefaultView.RowFilter = "FIRSTNAME ='" + this.LoginUser.LoginUserName + "'";
                        }
                        gcUserView.DataSource = resultArgs.DataSource.Table;
                        gcUserView.RefreshDataSource();
                    }
                }
            }
            catch (Exception e)
            {
                MessageRender.ShowMessage(e.Message, true);
            }
            finally { }
        }

        private void ShowUserForm(int UserId = 0)
        {
            try
            {
                using (frmAddNewUser frmAddUser = new frmAddNewUser(UserId))
                {
                    frmAddUser.UpdateHeld += new EventHandler(this.OnUpdateHeld);
                    frmAddUser.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }

        private void DeleteUser()
        {
            try
            {
                if (UserId != 0)
                {
                    if (!UserId.Equals((int)UserRights.Admin) && !UserId.Equals(AppSetting.DefaultAuditorUserId))
                    {
                        using (UserSystem userSystem = new UserSystem())
                        {
                            if (gvUserView.RowCount != 0)
                            {
                                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    userSystem.UserId = UserId;
                                    ResultArgs resultArgs = userSystem.DeleteUserDetails();
                                    if (resultArgs.Success)
                                    {
                                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                        LoadUserDetails();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //this.ShowMessageBox("You have no rights to delete this user");
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.UserManagementForms.NO_RIGHTS_TO_DELETE_USER));
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                }
            }
            catch (Exception ee)
            {
                MessageRender.ShowMessage(ee.Message, true);
            }
            finally { }
        }

        private void EditUser()
        {
            if (this.isEditable)
            {
                if (UserId != 0)
                {
                    if (gvUserView.RowCount != 0)
                    {
                        if (!UserId.Equals((int)UserRights.Admin) && !UserId.Equals(AppSetting.DefaultAuditorUserId))
                        {
                            ShowUserForm(UserId);
                        }
                        else
                        {
                            //this.ShowMessageBox("You have no access to edit this user");
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.UserManagementForms.NO_ACCESS_EDIT_USER));
                        }
                    }
                }
                else
                {
                    if (!chkShowFilter.Checked)
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                    }
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_USER_RIGHTS));
            }
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadUserDetails();
            gvUserView.FocusedRowHandle = RowIndex;
        }
        #endregion

        private void frmUserView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void frmUserView_EnterClicked(object sender, EventArgs e)
        {
            EditUser();
        }

        private void frmUserView_Activated(object sender, EventArgs e)
        {
            SetVisibileShortCuts(false, true);
        }
    }
}