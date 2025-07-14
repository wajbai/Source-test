using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Utility;
using Bosco.Model.UIModel;
using ACPP.Modules.TDS;
using Bosco.Model.Transaction;
using ACPP.Modules;
using Bosco.Model.TDS;

namespace ACPP.Modules.TDS
{
    public partial class frmDutyTaxAdd : frmFinanceBaseAdd
    {
        #region Variable Declaration
        private int DutyTaxId = 0;
        public event EventHandler UpdateHeld;
        #endregion

        #region Constructor
        public frmDutyTaxAdd()
        {
            InitializeComponent();
        }
        public frmDutyTaxAdd(int TaxId)
            : this()
        {
            DutyTaxId = TaxId;
        }
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateControlDetails())
            {
                using (DeducteeTaxSystem tax = new DeducteeTaxSystem())
                {
                    tax.TaxTypeId = DutyTaxId;
                    tax.TaxTypeName = txtDutyTax.Text;
                    tax.IsActive = chkStatus.Checked ? 1 : 0;
                    ResultArgs result = tax.SaveTaxTypeDetails();
                    if (result.Success)
                    {
                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                        if (UpdateHeld != null)
                            UpdateHeld(this, e);
                        if (DutyTaxId.Equals(0))
                        {
                            ClearControls();
                        }

                    }
                }
            }
        }


        #region Properties

        #endregion

        #region Events

        #endregion

        #region Methods
        private void SetTitle()
        {
            this.Text = this.DutyTaxId == 0 ? this.GetMessage(MessageCatalog.TDS.DutyTax.TDS_DUTY_TAX_ADD_CAPTION) : this.GetMessage(MessageCatalog.TDS.DutyTax.TDS_DUTY_TAX_EDIT_CAPTION);
        }
        public bool ValidateControlDetails()
        {
            bool isValue = true;
            if (string.IsNullOrEmpty(txtDutyTax.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.TDS.DutyTax.TDS_DUTY_TAX_NAME_EMPTY));
                this.SetBorderColor(txtDutyTax);
                isValue = false;
                txtDutyTax.Focus();
            }
            return isValue;
        }
        private void AssignDefaults()
        {
            try
            {
                if (DutyTaxId != 0)
                {
                    using (DeducteeTaxSystem deducteeTax = new DeducteeTaxSystem(DutyTaxId))
                    {
                        txtDutyTax.Text = deducteeTax.TaxTypeName;
                        chkStatus.Checked = deducteeTax.IsActive.Equals((int)YesNo.Yes) ? true : false;
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


        private void frmDutyTaxAdd_Load(object sender, EventArgs e)
        {
            SetTitle();
            AssignDefaults();
        }

        private void txtDutyTax_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtDutyTax);
        }
        private void ClearControls()
        {
            txtDutyTax.Text = string.Empty;
            txtDutyTax.Focus();
            chkStatus.Checked = false;
        }
    }
}