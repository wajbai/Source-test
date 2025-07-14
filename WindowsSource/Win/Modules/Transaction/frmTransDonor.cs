using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Utility;
using ACPP.Modules.Master;
using Bosco.Model.Business;
using Bosco.Model.Transaction;
using Bosco.Model.UIModel;


namespace ACPP.Modules.Transaction
{
    public partial class frmTransDonor : frmBaseAdd
    {
        #region Constructor
        public frmTransDonor()
        {
            InitializeComponent();
        }
        #endregion
        #region events
        /// <summary>
        /// To save donor's information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            DataTable dt=CreateDataTable();
                    
            using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
            {
                dt.Rows.Add(
                   voucherTransaction.DonorId = glkpDonor.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpDonor.EditValue.ToString()),
                 voucherTransaction.PurposeId = glkpPurpose.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpPurpose.EditValue.ToString()),
                 voucherTransaction.ContributionType = glkpReceiptType.EditValue == null ? "N" : this.UtilityMember.NumberSet.ToInteger(glkpReceiptType.EditValue.ToString()) == (int)ReceiptType.First ? "F" : "S",
                 voucherTransaction.ContributionAmount = this.UtilityMember.NumberSet.ToDecimal(txtAmount.Text),
                 voucherTransaction.CurrencyCountryId = lkpCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(lkpCountry.EditValue.ToString()),
                 voucherTransaction.ExchangeRate = this.UtilityMember.NumberSet.ToDecimal(txtExchangeRate.Text),
                 voucherTransaction.CalculatedAmount = this.UtilityMember.NumberSet.ToDecimal(lblCalculatedAmount.Text),
                 voucherTransaction.ActualAmount = this.UtilityMember.NumberSet.ToDecimal(txtActualAmount.Text));
                
            }
           // return dt;
        }

        /// <summary>
        /// Fired when glkpDonor value is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glkpDonor_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(glkpDonor.Text))
                {
                    EnableDonorFields();
                    DataRow dr = (glkpDonor.Properties.GetRowByKeyValue(glkpDonor.EditValue) as DataRowView).Row;

                    if (dr != null)
                    {
                        lkpCountry.EditValue = this.UtilityMember.NumberSet.ToInteger(dr["COUNTRY_ID"].ToString());
                        this.glkpReceiptType.EditValueChanged -= new System.EventHandler(this.glkpReceiptType_EditValueChanged);
                        glkpReceiptType.EditValue = glkpReceiptType.Properties.GetKeyValue(0);
                        glkpPurpose.EditValue = glkpPurpose.Properties.GetKeyValue(0);
                        this.glkpReceiptType.EditValueChanged += new System.EventHandler(this.glkpReceiptType_EditValueChanged);
                    }
                }
                else { DisableDonorFields(); }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }
        /// <summary>
        /// Fired when glkpReceiptType value is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glkpReceiptType_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(glkpReceiptType.Text))
                {
                    if (glkpReceiptType.Text == "First")
                    {
                        glkpReceiptType.EditValue = glkpReceiptType.Properties.GetKeyValue(0);
                    }
                    else
                    {
                        glkpReceiptType.EditValue = glkpReceiptType.Properties.GetKeyValue(1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.Source);
            }
            finally { }
        }
        /// <summary>
        /// Fired when the cursor leaves the txtExchangeRate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtExchangeRate_Leave(object sender, EventArgs e)
        {
            if (this.UtilityMember.NumberSet.ToDouble(txtExchangeRate.Text) >= 0)
            {
                CalculateExchangeRate();
            }
            else
            {
                //this.ShowMessageBox("Amount can not be less then Zero");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_AMOUNT_LESS_THAN_ZERO));
                txtExchangeRate.Text = "0";
                CalculateExchangeRate();
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Method to enable donor fields
        /// </summary>
        private void EnableDonorFields()
        {
            glkpPurpose.Enabled = true;
            glkpReceiptType.Enabled = true;
            txtActualAmount.Enabled = true;
            txtAmount.Enabled = true;
            txtExchangeRate.Enabled = true;
            lkpCountry.Enabled = true;
        }
        /// <summary>
        ///  Method to disable donor fields
        /// </summary>
        private void DisableDonorFields()
        {
            glkpPurpose.Enabled = false;
            glkpReceiptType.Enabled = false;
            txtActualAmount.Enabled = false;
            txtAmount.Enabled = false;
            txtExchangeRate.Enabled = false;
            lkpCountry.Enabled = false;
        }
        /// <summary>
        /// To calculate exchagerate
        /// </summary>
        private void CalculateExchangeRate()
        {
            try
            {
                Double ActualAmt = this.UtilityMember.NumberSet.ToDouble(txtAmount.Text) * this.UtilityMember.NumberSet.ToDouble(txtExchangeRate.Text);
                lblCalculatedAmount.Text = ActualAmt.ToString();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
        private DataTable CreateDataTable()
        {
            DataTable dtTransDonor = new DataTable();
            dtTransDonor.Columns.Add("DonorId", typeof(int));
            dtTransDonor.Columns.Add("PurposeId", typeof(int));
            dtTransDonor.Columns.Add("ContributionType", typeof(int));
            dtTransDonor.Columns.Add("ContributionAmt", typeof(decimal));
            dtTransDonor.Columns.Add("ContryId", typeof(int));
            dtTransDonor.Columns.Add("ExchangeRate", typeof(decimal));
            dtTransDonor.Columns.Add("CalculateAmount", typeof(decimal));
            dtTransDonor.Columns.Add("ActualAmount", typeof(decimal));
            return dtTransDonor;
        }
        #endregion                      
    }
}