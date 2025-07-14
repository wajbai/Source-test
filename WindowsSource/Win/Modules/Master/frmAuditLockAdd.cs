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
    public partial class frmAuditLockAdd : frmFinanceBaseAdd
    {
        #region Event Decelaration
        public event EventHandler UpdateHeld;
        #endregion

        #region Variables
        ResultArgs resultArgs = null;
        private int LockTypeId = 0;
        #endregion

        #region Constructor
        public frmAuditLockAdd()
        {
            InitializeComponent();
        }
        public frmAuditLockAdd(int LockTypeId)
            : this()
        {
            this.LockTypeId = LockTypeId;
            AssignLockType();
        }
        #endregion

        #region Events
        private void frmAuditLockAdd_Load(object sender, EventArgs e)
        {
            SetDefaults();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateAuditType())
                {
                    using (AuditLockTransSystem AuditTypeSystem = new AuditLockTransSystem())
                    {
                        AuditTypeSystem.LockTypeId = LockTypeId;
                        AuditTypeSystem.LockType = this.UtilityMember.StringSet.ToSentenceCase(txtLockType.Text.Trim());
                        resultArgs = AuditTypeSystem.SaveAuditType();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            this.ReturnValue = resultArgs.RowUniqueId;
                            this.ReturnDialog = System.Windows.Forms.DialogResult.OK;

                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                            if (LockTypeId == 0)
                            {
                                txtLockType.Text = string.Empty;
                            }
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtLockType_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtLockType);
            txtLockType.Text = this.UtilityMember.StringSet.ToSentenceCase(txtLockType.Text.Trim());
        }
        #endregion

        #region Methods

        private bool ValidateAuditType()
        {
            bool isSucess = true;
            if (string.IsNullOrEmpty(txtLockType.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.AuditLockType.AUDITLOCKTYPEEMPTY));
                this.SetBorderColor(txtLockType);
                txtLockType.Focus();
                isSucess = false;
            }
            //if (!string.IsNullOrEmpty(txtLockType.Text) && isAuditTypeExist())
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.AuditLockType.AUDITLOCKTYPEEMPTY));
            //    this.SetBorderColor(txtLockType);
            //    txtLockType.Focus();
            //    isSucess = false;
            //}
            return isSucess;
        }

        private void AssignLockType()
        {
            try
            {
                if (this.LockTypeId > 0)
                {
                    using (AuditLockTransSystem AuditSystem = new AuditLockTransSystem())
                    {
                        AuditSystem.LockTypeId = LockTypeId;
                        resultArgs = AuditSystem.FetchAuditType();
                        if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                            {
                                LockTypeId = this.UtilityMember.NumberSet.ToInteger(dr[AuditSystem.AppSchema.AuditLockType.LOCK_TYPE_IDColumn.ColumnName].ToString());
                                txtLockType.Text = dr[AuditSystem.AppSchema.AuditLockType.LOCK_TYPEColumn.ColumnName].ToString();
                            }
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

        private void SetDefaults()
        {
            this.Text = LockTypeId == 0 ? this.GetMessage(MessageCatalog.Master.AuditLockType.LOCK_TYPE_ADD_CAPTION) : this.GetMessage(MessageCatalog.Master.AuditLockType.LOCK_TYPE_EDIT_CAPTION);
        }

        //private bool isAuditTypeExist()
        //{
        //    bool isAuditTypeExist = false;
        //    using (AuditLockTransSystem auditSystem = new AuditLockTransSystem())
        //    {
        //        auditSystem.LockType = txtLockType.Text.Trim();
        //        isAuditTypeExist = auditSystem.isAuditTypeExists();
        //    }
        //    return isAuditTypeExist;
        //}
        #endregion
    }
}