using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using ACPP;
using Bosco.Utility;
using Bosco.Model.UIModel;
using ACPP.Modules.Master;
using ACPP.Modules;
using System.Text.RegularExpressions;
using Bosco.Model.UIModel.Master;
using Bosco.Utility.CommonMemberSet;

namespace ACPP.Modules.Master
{
    public partial class frmAuditTypeAdd : frmFinanceBaseAdd
    {
        #region Variable Decelaration
        private int auditId = 0;
        private ResultArgs resultArgs = null;

        #endregion

        #region Event Declaration
        public event EventHandler UpdateHeld;
        #endregion

        #region Constructors
        public frmAuditTypeAdd()
        {
            InitializeComponent();
        }

        public frmAuditTypeAdd(int AuditId)
            : this()
        {
            auditId = AuditId;
        }


        #endregion

        private void frmAuditTypeAdd_Load(object sender, EventArgs e)
        {
            AssignAuditDetails();
            SetTitle();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateAuditDetails())
                {
                    ResultArgs resultArgs = null;
                    using (AuditSystem auditSystem = new AuditSystem())
                    {
                        auditSystem.AuditId = auditId == 0 ? this.UtilityMember.NumberSet.ToInteger(AddNewRow.NewRow.ToString()) : auditId;
                        auditSystem.AuditType = txtAuditType.Text.Trim();
                        resultArgs = auditSystem.SaveAuditDetails();

                        if (resultArgs.Success)
                        {
                            this.ReturnValue = resultArgs.RowUniqueId;
                            this.ReturnDialog = System.Windows.Forms.DialogResult.OK;

                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            ClearControls();
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                        }
                        else
                        {
                            this.ShowMessageBoxError("" + resultArgs.Message);
                            txtAuditType.Focus();
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

        public bool ValidateAuditDetails()
        {
            bool isAuditTrue = true;
            if (string.IsNullOrEmpty(txtAuditType.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Audit.AUDIT_NAME_EMPTY));
                this.SetBorderColor(txtAuditType);
                isAuditTrue = false;
                txtAuditType.Focus();
            }

            return isAuditTrue;

        }

        public void AssignAuditDetails()
        {
            try
            {
                if (auditId != 0)
                {
                    using (AuditSystem auditSystem = new AuditSystem(auditId))
                    {
                        txtAuditType.Text = auditSystem.AuditType;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void ClearControls()
        {
            if (auditId == 0)
            {
                txtAuditType.Text = string.Empty;
            }
            txtAuditType.Focus();
        }

        private void SetTitle()
        {
            this.Text = auditId == 0 ? this.GetMessage(MessageCatalog.Master.Audit.AUDIT_ADD_CAPTION) : this.GetMessage(MessageCatalog.Master.Audit.AUDIT_EDIT_CAPTION);
            txtAuditType.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtAuditType_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtAuditType);
            txtAuditType.Text = this.UtilityMember.StringSet.ToSentenceCase(txtAuditType.Text);
        }
    }
}
