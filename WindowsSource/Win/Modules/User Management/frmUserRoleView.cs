using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Model.UIModel;
using Bosco.Utility;

namespace ACPP.Modules.User_Management
{
    public partial class frmUserRoleView : frmFinanceBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        #endregion

        #region Constructor
        public frmUserRoleView()
        {
            InitializeComponent();
        }
        #endregion

        #region Property
        private int userRoleId = 0;
        public int UserRoleId
        {
            get
            {
                RowIndex = gvUserRole.FocusedRowHandle;
                userRoleId = gvUserRole.GetFocusedRowCellValue(colUserRoleId) != null ? this.UtilityMember.NumberSet.ToInteger(gvUserRole.GetFocusedRowCellValue(colUserRoleId).ToString()) : 0;
                return userRoleId;
            }
            set
            {
                userRoleId = value;
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUserRoleView_Load(object sender, EventArgs e)
        {
            //Added by Carmel Raj
            SetVisibileShortCuts(false, false);
            LoadUserRoles();
            ApplyUserRights();
        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(UserRole.CreateUserRole);
            this.enumUserRights.Add(UserRole.EditUserRole);
            this.enumUserRights.Add(UserRole.DeleteUserRole);
            this.enumUserRights.Add(UserRole.PrintUserRole);
            this.enumUserRights.Add(UserRole.ViewUserRole);
            this.ApplyUserRights(ucToolBarUserRoleView, this.enumUserRights, (int)Menus.UserRole);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarUserRoleView_AddClicked(object sender, EventArgs e)
        {
            ShowForm((int)AddNewRow.NewRow);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarUserRoleView_EditClicked(object sender, EventArgs e)
        {
            EditUserRole();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarUserRoleView_DeleteClicked(object sender, EventArgs e)
        {
            try
            {
                if (UserRoleId != 0)
                {
                    if (UserRoleId != (int)UserRights.Admin && UserRoleId != (int)UserRights.Supervisor && UserRoleId != this.LoginUser.DefaultAuditorRoleId)
                    {
                        using (UserRoleSystem userRoleSystem = new UserRoleSystem())
                        {
                            if (gvUserRole.RowCount != 0)
                            {
                                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    resultArgs = userRoleSystem.DeleteUserRole(UserRoleId);
                                    if (resultArgs.Success)
                                    {
                                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                        LoadUserRoles();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.UserRole.USER_ROLE_DELETE_FAILS));
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvUserRole.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvUserRole, colUserRoleName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarUserRoleView_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcUserRole, this.GetMessage(MessageCatalog.UserRole.USER_ROLE_PRINT_CAPTION), PrintType.DT, gvUserRole);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvUserRole_DoubleClick(object sender, EventArgs e)
        {
            EditUserRole();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvUserRole_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvUserRole.RowCount.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarUserRoleView_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        private void LoadUserRoles()
        {
            using (UserRoleSystem userRoleSystem = new UserRoleSystem())
            {
                resultArgs = userRoleSystem.FetchUserRoleDetails();
                if (resultArgs.Success)
                {
                    gcUserRole.DataSource = resultArgs.DataSource.Table;
                    gcUserRole.RefreshDataSource();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectCategoryId"></param>
        public void ShowForm(int UserRoleid)
        {
            try
            {
                frmUserRoleAdd frmuserRole = new frmUserRoleAdd(UserRoleid);
                frmuserRole.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmuserRole.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadUserRoles();
            gvUserRole.FocusedRowHandle = RowIndex;
        }

        /// <summary>
        /// 
        /// </summary>
        public void EditUserRole()
        {
            try
            {
                if (this.isEditable)
                {
                    if (UserRoleId != 0)
                    {
                        if (gvUserRole.RowCount != 0)
                        {
                            if (!UserRoleId.Equals((int)UserRights.Admin) && !UserRoleId.Equals(this.LoginUser.DefaultAuditorRoleId))
                            {
                                ShowForm(UserRoleId);
                            }
                            else
                            {
                                //this.ShowMessageBox("You have no access to edit this user role");
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.UserManagementForms.NO_ACCESS_EDIT_USER_ROLE));
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
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
        #endregion

        private void frmUserRoleView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void frmUserRoleView_EnterClicked(object sender, EventArgs e)
        {
            EditUserRole();
        }

        private void frmUserRoleView_Activated(object sender, EventArgs e)
        {
            SetVisibileShortCuts(false, true);
        }
    }
}