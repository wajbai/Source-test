using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model.UIModel;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.Model.UIModel;
using System.Xml;
using DevExpress.XtraLayout.Utils;

namespace ACPP.Modules.User_Management
{
    public partial class frmChangePassword : frmFinanceBaseAdd
    {
        #region Variables
        private string userName = string.Empty;
        private int roleId = 0;
        private bool isValidUsers = false;
        #endregion

        #region Constructor
        public frmChangePassword()
        {
            InitializeComponent();
        }
        public frmChangePassword(string UserName, int RoleId, bool isValidUser = false)
            : this()
        {
            userName = UserName;
            roleId = RoleId;
            isValidUsers = isValidUser;
        }
        #endregion

        #region Events
        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            txtUsername.Text = userName;
            if (roleId == 1)
            {
                //  txtCurrentpassword.Focus();
                layoutControlItem1.Visibility = LayoutVisibility.Never;
                this.Height = 160;
            }
            else
            {
                txtCurrentpassword.Focus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validatedetails())
                {
                    ResultArgs resultArgs = null;

                    using (UserSystem usersystem = new UserSystem())
                    {
                        resultArgs = usersystem.FetchUserId(txtUsername.Text);
                        if (resultArgs.DataSource.TableView.Table != null)
                        {
                            string UID = resultArgs.DataSource.TableView.Table.Rows[0]["USER_ID"].ToString();
                            if (resultArgs.Success && resultArgs.RowsAffected > 0)
                            {
                                SimpleEncrypt.SimpleEncDec objReset = new SimpleEncrypt.SimpleEncDec();
                                string currpassword = txtCurrentpassword.Text;
                                currpassword = objReset.EncryptString(currpassword);
                                resultArgs = usersystem.CheckCurrentPassword(UID, currpassword);
                                if (resultArgs.Success && resultArgs.RowsAffected > 0 || roleId == 1)
                                {
                                    if (!(string.IsNullOrEmpty(txtConfirmPassword.Text)) && (!string.IsNullOrEmpty(txtNewPassword.Text)))
                                    {
                                        if (!txtNewPassword.Text.Equals(txtConfirmPassword.Text.Trim()))
                                        {
                                            this.ShowMessageBox(this.GetMessage(MessageCatalog.User.USER_NEW_PASSWORD_MISMATCH));
                                            this.SetBorderColor(txtNewPassword);
                                            txtNewPassword.Focus();
                                            txtConfirmPassword.Text = txtNewPassword.Text = "";
                                        }

                                        else if (this.AppSetting.LoginUserId.Equals("1") && this.txtUsername.Text.Equals("admin") && this.txtConfirmPassword.Text.Equals("admin"))
                                        {
                                            //this.ShowMessageBox("Username and password cannot be same for the admin user.");
                                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.UserManagementForms.USERNAME_PASSWORD_CANNOT_SAME_FOR_ADMIN_USER));
                                            this.SetBorderColor(txtNewPassword);
                                            txtNewPassword.Focus();
                                            txtConfirmPassword.Text = txtNewPassword.Text = "";
                                        }
                                        else
                                        {
                                          //  txtNewPassword.Text = objReset.EncryptString(txtNewPassword.Text.Trim());
                                            resultArgs = usersystem.UpdatePassword(UID, objReset.EncryptString(txtNewPassword.Text.Trim()));
                                            if (resultArgs.Success)
                                            {
                                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.User.USER_RESET_PASSWORD_SUCCESS));
                                                Clearcontrols();
                                                if (isValidUsers)
                                                {
                                                    this.DialogResult = DialogResult.OK;
                                                    this.Close();
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    this.ShowMessageBox(this.GetMessage(MessageCatalog.User.USER_CURRENT_PASSWORD_FAIL));
                                    this.SetBorderColor(txtCurrentpassword);
                                    txtCurrentpassword.Focus();
                                    txtCurrentpassword.Text = string.Empty;
                                }
                            }
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void Clearcontrols()
        {
            txtCurrentpassword.Text = txtNewPassword.Text = txtConfirmPassword.Text = string.Empty;
        }
        public bool Validatedetails()
        {
            bool isValue = true;

            if (roleId != 1)
            {
                if (string.IsNullOrEmpty(txtCurrentpassword.Text))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.User.USER_CURRENT_PASSWORD_EMPTY));
                    this.SetBorderColor(txtCurrentpassword);
                    isValue = false;
                    txtCurrentpassword.Focus();
                }
            }
            if (isValue)
            {
                if (string.IsNullOrEmpty(txtNewPassword.Text))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.User.USER_NEW_PASSWORD_EMPTY));
                    this.SetBorderColor(txtNewPassword);
                    isValue = false;
                    txtNewPassword.Focus();
                }
                else if (string.IsNullOrEmpty(txtConfirmPassword.Text))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.User.USER_CONFIRM_PASSWORD_EMPTY));
                    this.SetBorderColor(txtConfirmPassword);
                    isValue = false;
                    txtConfirmPassword.Focus();
                }
                else
                {
                    btnSave.Focus();
                }
            }
            return isValue;
        }
        #endregion

        private void txtCurrentpassword_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtCurrentpassword);
        }

        private void txtNewPassword_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtNewPassword);
        }

        private void txtConfirmPassword_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtConfirmPassword);
        }
    }
}