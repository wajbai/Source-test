using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model.TDS;
using Bosco.Utility;
using DevExpress.XtraGrid.Views.Base;

namespace ACPP.Modules.TDS
{
    public partial class frmDeducteeTaxAdd : frmFinanceBaseAdd
    {
        #region Declaration
        public event EventHandler UpdateHeld;
        private int TaxId = 0;
        ResultArgs resultArgs = null;
        public DataView dvDeducteeType = null;
        #endregion

        #region Constructor
        public frmDeducteeTaxAdd()
        {
            InitializeComponent();
        }

        public frmDeducteeTaxAdd(int taxId)
            : this()
        {
            TaxId = taxId;
        }
        #endregion

        #region Events
        private void frmDeducteeTaxAdd_Load(object sender, EventArgs e)
        {
            SetDefaults();
            LoadDeducteeTypes();
            LoadNatureOfPayment();
            LoadTaxTypeDetails();
            AssignTaxDetails();
        }

        private void glkpDeducteeType_EditValueChanged(object sender, EventArgs e)
        {
            if (glkpDeducteeType.EditValue != null)
            {
                if (dvDeducteeType.Count > 0)
                {
                    dvDeducteeType.RowFilter = "DEDUCTEE_TYPE_ID=" + glkpDeducteeType.EditValue.ToString();

                    if (dvDeducteeType.Count > 0)
                    {
                        lblResidentStatus.Text = dvDeducteeType[0]["RESIDENTIAL_STATUS"].ToString();
                        lblDeducteeStatus.Text = dvDeducteeType[0]["DEDUCTEE_TYPE"].ToString();
                        lblStatus.Text = dvDeducteeType[0]["STATUS"].ToString();
                    }
                    dvDeducteeType.RowFilter = "";
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValidInput())
            {
                DataTable dtTaxType = null;
                using (DeducteeTaxSystem tax = new DeducteeTaxSystem())
                {
                    dtTaxType = gcDeducteeTax.DataSource as DataTable;
                    if (dtTaxType != null && dtTaxType.Rows.Count > 0)
                    {
                        tax.TaxPolicyId = TaxId;
                        tax.DeducteeTypeId = glkpDeducteeType.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpDeducteeType.EditValue.ToString()) : 0;
                        tax.NaturePaymentId = glkpNatureOfPayment.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpNatureOfPayment.EditValue.ToString()) : 0;
                        tax.ApplicableFrom = dtDateFrom.DateTime;

                        tax.dtTaxDetails = dtTaxType;

                        resultArgs = tax.SaveDutyTaxDetails();
                    }

                    if (resultArgs.Success)
                    {
                        //this.ShowSuccessMessage("Deductee Tax Types details saved successfully");
                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.TDS.NatureofPayments.TDS_DEDUCTEE_TYPE_SUCCESS));
                        ClearControls();
                        if (UpdateHeld != null)
                        {
                            UpdateHeld(this, e);
                        }
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gcDeducteeTax_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control &&
                (gvDeducteeTax.FocusedColumn == colExemptionLimit) && gvDeducteeTax.IsLastRow)
            {
                btnSave.Select();
                btnSave.Focus();
            }
            else if (gvDeducteeTax.IsFirstRow && gvDeducteeTax.FocusedColumn == colRate && e.Shift && e.KeyCode == Keys.Tab)
            {
                txtExemptionLimit.Select();
                txtExemptionLimit.Focus();
            }
        }

        private void gvDeducteeTax_GotFocus(object sender, EventArgs e)
        {
            BaseView view = sender as BaseView;
            if (view == null)
                return;

            if (MouseButtons == System.Windows.Forms.MouseButtons.Left)
                return;
            view.ShowEditor();
            TextEdit editor = view.ActiveEditor as TextEdit;
            if (editor != null)
            {
                editor.SelectAll();
                editor.Focus();
            }
        }
        #endregion

        #region Methods
        private void SetDefaults()
        {
            this.dtDateFrom.DateTime = DateTime.Today;
        }

        private void LoadNatureOfPayment()
        {
            using (NatureofPaymentsSystem paymentNature = new NatureofPaymentsSystem())
            {
                resultArgs = paymentNature.FetchNatureofPayments();
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpNatureOfPayment, resultArgs.DataSource.Table, "NAME", "NATURE_PAY_ID");
                    glkpNatureOfPayment.EditValue = glkpNatureOfPayment.Properties.GetKeyValue(0);
                }
            }
        }

        private void LoadTaxTypeDetails()
        {
            using (DeducteeTaxSystem tax = new DeducteeTaxSystem())
            {
                resultArgs = tax.FetchDutyTaxTypes();

                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    gcDeducteeTax.DataSource = resultArgs.DataSource.Table;
                }
            }
        }

        private void LoadDeducteeTypes()
        {
            using (DeducteeTypeSystem deductee = new DeducteeTypeSystem())
            {
                resultArgs = deductee.FetchDeducteeTypes();
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    dvDeducteeType = resultArgs.DataSource.Table.DefaultView;
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpDeducteeType, resultArgs.DataSource.Table, "NAME", "DEDUCTEE_TYPE_ID");
                    glkpDeducteeType.EditValue = glkpDeducteeType.Properties.GetKeyValue(0);
                }
            }
        }

        private bool IsValidInput()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(glkpDeducteeType.EditValue.ToString()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.TDS.DeducteeTypes.TDS_DEDUCTEE_TYPE_EMPTY));
                isValid = false;
            }
            else if (string.IsNullOrEmpty(glkpNatureOfPayment.EditValue.ToString()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.TDS.NatureofPayments.TDS_NATURE_OF_PAYMENT_EMPTY));
                isValid = false;
            }
            return isValid;
        }


        public void AssignTaxDetails()
        {
            try
            {
                if (TaxId != 0)
                {
                    using (DeducteeTaxSystem tax = new DeducteeTaxSystem())
                    {
                        tax.TaxPolicyId = TaxId;
                        resultArgs = tax.FillDutyTaxProperties();

                        if (resultArgs.Success)
                        {
                            glkpDeducteeType.EditValue = tax.DeducteeTypeId;
                            glkpNatureOfPayment.EditValue = tax.NaturePaymentId;
                            dtDateFrom.DateTime = tax.ApplicableFrom;

                            resultArgs = tax.FetchTaxRateById();
                            if (resultArgs.Success && resultArgs.RowsAffected > 0)
                            {
                                gcDeducteeTax.DataSource = resultArgs.DataSource.Table;
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

        private void ClearControls()
        {
            LoadTaxTypeDetails();
            txtExemptionLimit.Text = txtRate.Text = string.Empty;
        }
        #endregion
    }
}