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
using Bosco.Model.TDS;
using ACPP.Modules.TDS;
using ACPP.Modules;


namespace ACPP.Modules.TDS
{
    public partial class frmNatureofPayments : frmFinanceBaseAdd
    {
        #region Variable Declaration
        private int NaturePaymentId = 0;
        public event EventHandler UpdateHeld;
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public frmNatureofPayments()
        {
            InitializeComponent();
        }
        public frmNatureofPayments(int Paymentid)
            : this()
        {
            NaturePaymentId = Paymentid;
        }
        #endregion

        #region Properties

        #endregion

        #region Events
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateControlDetails())
                {
                    ResultArgs resultArgs = null;
                    using (NatureofPaymentsSystem natureofpaymentssystem = new NatureofPaymentsSystem())
                    {
                        natureofpaymentssystem.NatureofPaymentId = NaturePaymentId == 0 ? (int)AddNewRow.NewRow : NaturePaymentId;
                        natureofpaymentssystem.PaymentName = memName.Text.Trim();
                        natureofpaymentssystem.PaymentCode = txtPaymentCode.Text.Trim();
                        natureofpaymentssystem.TdsSectionID = this.UtilityMember.NumberSet.ToInteger(glkpAvailableSectionCodes.EditValue.ToString());
                        natureofpaymentssystem.Notes = memNotes.Text;
                        natureofpaymentssystem.IsActive = chkStatus.Checked ? 1 : 0;
                        resultArgs = natureofpaymentssystem.SavePaymentDetails();
                        if (resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                            if (NaturePaymentId.Equals(0))
                            {
                                ClearControl();
                            }
                            memName.Focus();
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

        private void memName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(memName);
        }

        private void txtPaymentCode_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtPaymentCode);
        }

        private void frmNatureofPayments_Load(object sender, EventArgs e)
        {
            SetTitle();
            LoadSectionCodes();
            AssignDefaults();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void glkpAvailableSectionCodes_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpAvailableSectionCodes);
        }

        #endregion

        #region Methods

        /// <summary>
        /// To Validate the Nature of Payments Details
        /// </summary>
        /// <returns></returns>
        public bool ValidateControlDetails()
        {
            bool isValue = true;
            if (string.IsNullOrEmpty(memName.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.TDS.NatureofPayments.TDS_NATUREPAYMENT_PAYMENT_NAME_EMPTY));
                this.SetBorderColor(memName);
                isValue = false;
                memName.Focus();
            }
            else if (string.IsNullOrEmpty(txtPaymentCode.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.TDS.NatureofPayments.TDS_NATUREPAYMENT_PAYMENT_CODE_EMPTY));
                this.SetBorderColor(txtPaymentCode);
                isValue = false;
                txtPaymentCode.Focus();
            }
            else if (string.IsNullOrEmpty(glkpAvailableSectionCodes.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.TDS.NatureofPayments.TDS_NATUREPAYMENT_SECTION_CODE_EMPTY));
                this.SetBorderColor(glkpAvailableSectionCodes);
                isValue = false;
                glkpAvailableSectionCodes.Focus();
            }
            if (this.NaturePaymentId > 0)
            {
                if (IsActiveNatureNOP() > 0 && chkStatus.Checked == false)
                {
                    //this.ShowMessageBox("Cannot set this inactive.Voucher is done for this Nature of Payment.");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.TDS.NatureofPayments.TDS_CANNOT_SET_INACTIVE_INFO));
                    chkStatus.Checked = true;
                    isValue = false;
                }
            }
            return isValue;
        }

        private int IsActiveNatureNOP()
        {
            using (NatureofPaymentsSystem NaturePayments = new NatureofPaymentsSystem())
            {
                NaturePayments.NatureofPaymentId = NaturePaymentId;
                return NaturePayments.IsActiveNOP();
            }
        }

        private void ClearControl()
        {
            memName.Text = txtPaymentCode.Text = memNotes.Text = string.Empty;
            memName.Focus();
            chkStatus.Checked = false;
            glkpAvailableSectionCodes.EditValue = null;
        }

        private void LoadSectionCodes()
        {
            using (NatureofPaymentsSystem NaturepaymentSystem = new NatureofPaymentsSystem())
            {
                resultArgs = NaturepaymentSystem.FetchSectionCodes();
                if (resultArgs.DataSource != null && resultArgs.RowsAffected > 0)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpAvailableSectionCodes, resultArgs.DataSource.Table, "SECTION_NAME", NaturepaymentSystem.AppSchema.NatureofPayment.TDS_SECTION_IDColumn.ColumnName);
                    //  glkpAvailableSectionCodes.EditValue = glkpAvailableSectionCodes.Properties.GetKeyValue(0);
                    //if (NaturePaymentId == 0)
                    //    txtCode.Text = CodePredictor(glkpAvailableCostCentreCodes.Properties.GetKeyValue(0).ToString(), resultArgs.DataSource.Table);
                }
            }
        }

        private void AssignDefaults()
        {
            try
            {
                if (NaturePaymentId != 0)
                {
                    using (NatureofPaymentsSystem NaturePaymentsystem = new NatureofPaymentsSystem(NaturePaymentId))
                    {
                        memName.Text = NaturePaymentsystem.PaymentName;
                        txtPaymentCode.Text = NaturePaymentsystem.PaymentCode;
                        glkpAvailableSectionCodes.EditValue = NaturePaymentsystem.TdsSectionID;
                        memNotes.Text = NaturePaymentsystem.Notes;
                        chkStatus.Checked = NaturePaymentsystem.IsActive.Equals((int)YesNo.Yes) ? true : false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString() + Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void SetTitle()
        {
            this.Text = this.NaturePaymentId == 0 ? this.GetMessage(MessageCatalog.TDS.NatureofPayments.TDS_NATUREPAYMENT_ADD_CAPTION) : this.GetMessage(MessageCatalog.TDS.NatureofPayments.TDS_NATUREPAYMENT_EDIT_CAPTION);
        }
        #endregion
    }
}