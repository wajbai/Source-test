using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model.Donor;
using Bosco.Utility;

namespace ACPP.Modules.ProspectsDonor
{
    public partial class frmInstitutionType : frmFinanceBaseAdd
    {
        private int InstitutionTypeId { get; set; }
        ResultArgs resultArgs = null;
        public event EventHandler UpdateHeld;

        public frmInstitutionType()
        {
            InitializeComponent();
        }
        public frmInstitutionType(int Id)
            : this()
        {
            this.InstitutionTypeId = Id;
        }

        private void frmInstitutionType_Load(object sender, EventArgs e)
        {
            SetTitle();
            AssignValues();
        }

        private void txtInstitutionType_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtInstitutionType);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateControls())
                {
                    using (InstitutionType institutionType = new InstitutionType())
                    {
                        institutionType.InstutionTypeId = this.InstitutionTypeId;
                        institutionType.InstutionType = txtInstitutionType.Text;
                        resultArgs = institutionType.Save();
                        if (resultArgs.Success)
                        {
                            this.ReturnValue = resultArgs.RowUniqueId;
                            this.ReturnDialog = System.Windows.Forms.DialogResult.OK;

                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                            txtInstitutionType.Text = string.Empty;
                            txtInstitutionType.Focus();
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

        private void SetTitle()
        {
            this.Text = InstitutionTypeId == 0 ? GetMessage(MessageCatalog.NetworkingSettings.INSTITUTIONAL_TYPE_ADD) : GetMessage(MessageCatalog.NetworkingSettings.INSTITUTIONAL_TYPE_EDIT);
        }

        private bool ValidateControls()
        {
            bool isTrue = true;
            if (string.IsNullOrEmpty(txtInstitutionType.Text))
            {
                isTrue = false;
                this.SetBorderColor(txtInstitutionType);
                txtInstitutionType.Focus();
                this.ShowMessageBox(GetMessage(MessageCatalog.NetworkingSettings.INSTITUTIONAL_TYPE_EMPTY));
            }
            return isTrue;
        }

        private void AssignValues()
        {
            if (this.InstitutionTypeId > 0)
            {
                using (InstitutionType institutionType = new InstitutionType())
                {
                    institutionType.InstutionTypeId = this.InstitutionTypeId;
                    institutionType.AssignValues();
                    InstitutionTypeId = institutionType.InstutionTypeId;
                    txtInstitutionType.Text = institutionType.InstutionType;
                }
            }
        }
    }
}