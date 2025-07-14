using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Model.UIModel.Master;
using Bosco.Utility;

namespace ACPP.Modules.Master
{
    public partial class frmResetAuditLock : frmFinanceBaseAdd
    {
        #region Variables
        SimpleEncrypt.SimpleEncDec objSimpleEncrypt = new SimpleEncrypt.SimpleEncDec();
        bool isResetPassword = false;
        int HintCount = 0;
        #endregion

        #region Constructor
        public frmResetAuditLock()
        {
            InitializeComponent();
        }
        public frmResetAuditLock(int LockTransId, bool isResetPassword = false)
            : this()
        {
            this.LockTransactionId = LockTransId;
            this.isResetPassword = isResetPassword;
        }
        #endregion

        #region Properties
        private int LockTransactionId { get; set; }
        public bool isValidPassword { get; set; }
        #endregion

        #region Events
        private void frmResetAuditLock_Load(object sender, EventArgs e)
        {
            if (isResetPassword)
            {
                this.Text = this.GetMessage(MessageCatalog.Master.AuditLockType.RESET_PASSWORD);
                lblHint.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                lblHint.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.Height = 100;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateDetails())
                {
                    if (!isResetPassword)
                    {
                        if (ValidatePassword())
                        {
                            isValidPassword = true;
                            this.Close();
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(txtPassword.Text))
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.AuditLockType.PASSWORD_INVALID));
                                txtPassword.Text = string.Empty;
                                txtPassword.Focus();
                            }
                        }
                    }
                    else
                    {
                        if (ValidatePasswordHint())
                        {
                            if (ResetPassword())
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.AuditLockType.PASSWORD_RESET));
                                this.Close();
                            }
                        }
                        else
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.AuditLockType.HINT_INVALID));
                            txtPasswordHint.Text = string.Empty;
                            txtPasswordHint.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtPassword);
        }

        private void txtPasswordHint_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtPasswordHint);
        }

        #endregion

        #region Methods

        private bool ValidatePassword()
        {
            bool isValidPassword = true;
            if (!string.IsNullOrEmpty(txtPassword.Text))
            {
                using (AuditLockTransSystem AuditSystem = new AuditLockTransSystem())
                {
                    AuditSystem.Password = objSimpleEncrypt.EncryptString(txtPassword.Text);
                    AuditSystem.LockTransId = LockTransactionId;
                    isValidPassword = AuditSystem.IsValidPassword();
                }
            }
            return isValidPassword;
        }

        private bool ValidateDetails()
        {
            bool IsSuccess = true;
            if (!isResetPassword)
            {
                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.AuditLockType.PASSWORDEMPTY));
                    this.SetBorderColor(txtPassword);
                    txtPassword.Focus();
                    IsSuccess = false;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtPasswordHint.Text))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.AuditLockType.PASSWORD_HINT_EMPTY));
                    this.SetBorderColor(txtPasswordHint);
                    txtPasswordHint.Focus();
                    IsSuccess = false;
                }
                else if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.AuditLockType.PASSWORDEMPTY));
                    this.SetBorderColor(txtPassword);
                    txtPassword.Focus();
                    IsSuccess = false;
                }
            }
            return IsSuccess;

        }

        private bool ResetPassword()
        {
            bool isResetPassword = false;
            if (!string.IsNullOrEmpty(txtPassword.Text))
            {
                using (AuditLockTransSystem AuditSystem = new AuditLockTransSystem())
                {
                    AuditSystem.Password = objSimpleEncrypt.EncryptString(txtPassword.Text);
                    AuditSystem.LockTransId = LockTransactionId;
                    isResetPassword = AuditSystem.ResetPassword();
                }
            }
            return isResetPassword;
        }

        private bool ValidatePasswordHint()
        {
            bool isResetPassword = false;
            if (!string.IsNullOrEmpty(txtPasswordHint.Text))
            {
                using (AuditLockTransSystem AuditSystem = new AuditLockTransSystem())
                {
                    AuditSystem.PasswordHint = objSimpleEncrypt.EncryptString(txtPasswordHint.Text);
                    AuditSystem.LockTransId = LockTransactionId;
                    HintCount = AuditSystem.ValidatePasswordHint();
                    if (HintCount > 0)
                    {
                        isResetPassword = true;
                    }
                }
            }
            return isResetPassword;
        }

        #endregion



    }
}