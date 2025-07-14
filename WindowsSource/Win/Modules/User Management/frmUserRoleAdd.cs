using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.Model.UIModel;
using System.Xml;

namespace ACPP.Modules.User_Management
{
    public partial class frmUserRoleAdd : frmFinanceBaseAdd
    {
        #region EventsDeclaration
        public event EventHandler UpdateHeld;
        #endregion

        #region VariableDeclaration
        private int UserRoleId = 0;
        #endregion

        #region Constructor
        public frmUserRoleAdd()
        {
            InitializeComponent();
        }
        public frmUserRoleAdd(int userRoleId)
            :this()
        {
            UserRoleId = userRoleId;
        }
        #endregion

        #region Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUserRoleAdd_Load(object sender, EventArgs e)
        {
            SetTitle();
            AssignUserRole();
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateUserRoleDetails())
                {
                    ResultArgs resultArgs = null;
                    using (UserRoleSystem userRoleSystem = new UserRoleSystem())
                    {
                        userRoleSystem.UserRoleId = UserRoleId == 0 ? (int)AddNewRow.NewRow : UserRoleId;
                        userRoleSystem.UserRoleName = txtUserRole.Text.Trim();
                        resultArgs = userRoleSystem.SaveUserRoles();
                        if (resultArgs.Success)
                        {
                            this.ReturnValue = resultArgs.RowUniqueId;
                            this.ReturnDialog = System.Windows.Forms.DialogResult.OK;

                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                            ClearControl();
                            txtUserRole.Focus();
                        }
                    }
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
        private void txtUserRole_Leave(object sender, EventArgs e)
        {
            SetBorderColor(txtUserRole);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ValidateUserRoleDetails()
        {
            bool isValue = true;
            if (string.IsNullOrEmpty(txtUserRole.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.UserRole.USERROLE_EMPTY));
                this.SetBorderColor(txtUserRole);
                isValue = false;
                txtUserRole.Focus();
            }
            else
            {
                btnSave.Focus();
            }
            return isValue;
        }

        /// <summary>
        /// 
        /// </summary>
        private void ClearControl()
        {
            if (UserRoleId == 0)
            {
                txtUserRole.Text = string.Empty;
            }
            txtUserRole.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetTitle()
        {
            this.Text = UserRoleId == 0 ? this.GetMessage(MessageCatalog.UserRole.USER_ROLE_ADD_CAPTION) : this.GetMessage(MessageCatalog.UserRole.USER_ROLE_EDIT_CAPTION);
            txtUserRole.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        public void AssignUserRole()
        {
            try
            {
                if (UserRoleId != 0)
                {
                    using (UserRoleSystem userRoleSystem = new UserRoleSystem(UserRoleId))
                    {
                        txtUserRole.Text = userRoleSystem.UserRoleName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
        #endregion

        
    }
}