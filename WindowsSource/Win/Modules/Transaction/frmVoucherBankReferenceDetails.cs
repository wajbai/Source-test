/*
 * This form is used to get Cheque/DD/NEFT/REGTS details like Date on when it was given, its Bank Name and Bank Branhc
 * all these details will be reutnred to parent form (this. returnvalue)
 * */

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

namespace ACPP.Modules.Transaction
{
    public partial class frmVoucherBankReferenceDetails : frmFinanceBaseAdd
    {
        private string ChequeReferecneNumber;
        private string ReferecneDate;
        private string BankName;
        private string Branch;
        private string FundTransfer;
        private int FundTransferId;

        private Bosco.Utility.ResultArgs resultArgs = new Bosco.Utility.ResultArgs();

        public frmVoucherBankReferenceDetails(string chequereferecnenumber, DateTime voucherdate, string referecnedate, string bankname, string branch, string fundtransfer)
        {
            InitializeComponent();
            ChequeReferecneNumber = chequereferecnenumber;
            ReferecneDate = referecnedate;
            BankName = bankname;
            Branch = branch;
            FundTransfer = fundtransfer;

            //Assign values to control
            lcGrpBankReferenceNo.Text = " Bank Allocation for " + chequereferecnenumber;     //  chequereferecnenumber + " Bank Reference Number details";
            if (!string.IsNullOrEmpty(referecnedate) && DateTime.MinValue.Date.ToShortDateString() != UtilityMember.DateSet.ToDate(referecnedate))
                dateDate.DateTime = this.UtilityMember.DateSet.ToDate(referecnedate, false);
            else
                dateDate.DateTime = voucherdate;
            txtBankName.Text = BankName;
            txtBranch.Text = Branch;

            //FundTransferId = GetFundTransferIds(FundTransfer);

            //LoadFundTransfer();

            glkpFundTransfer.EditValue = FundTransfer; //FundTransferId == 0 ? glkpFundTransfer.Properties.GetKeyValue(0) : glkpFundTransfer.Properties.GetKeyValue(FundTransferId);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //This will return given value like Date, BankName, Branch
            string[] bankreferencenumberdetails = new string[] { dateDate.DateTime.ToShortDateString(), txtBankName.Text.Trim(), txtBranch.Text.Trim(), glkpFundTransfer.Text };
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ReturnValue = bankreferencenumberdetails;
        }

        //private int GetFundTransferIds(string fundname)
        //{
        //    int fundid = 0;
        //    if (fundname.Equals(FundTransferList.NEFT.ToString()))
        //    {
        //        fundid = (int)FundTransferList.NEFT;
        //    }
        //    else if (fundname.Equals(FundTransferList.RTGS.ToString()))
        //    {
        //        fundid = (int)FundTransferList.RTGS;
        //    }
        //    else if (fundname.Equals(FundTransferList.IMPS.ToString()))
        //    {
        //        fundid = (int)FundTransferList.IMPS;
        //    }
        //    else if (fundname.Equals("GOOGLE PAY"))
        //    {
        //        fundid = (int)FundTransferList.GooglePay;
        //    }
        //    else if (fundname.Equals("PHONE PAY"))
        //    {
        //        fundid = (int)FundTransferList.PhonePay;
        //    }
        //    else if (fundname.Equals(FundTransferList.Paytm.ToString()))
        //    {
        //        fundid = (int)FundTransferList.Paytm;
        //    }

        //    return fundid;
        //}

        private void LoadFundTransfer()
        {
            try
            {
                using (Bosco.Model.Transaction.VoucherTransactionSystem voucherTransaction = new Bosco.Model.Transaction.VoucherTransactionSystem())
                {
                    using (Bosco.Utility.CommonMethod commonmethod = new Bosco.Utility.CommonMethod())
                    {

                        resultArgs = voucherTransaction.FetchFundTransferList();
                        if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            // DataTable dtFundTransfer = commonmethod.AddHeaderColumn(resultArgs.DataSource.Table, "FUND_TRANSFER_TYPE_ID", "FUND_TRANSFER_TYPE_NAME", "Select");
                            resultArgs.DataSource.Table.DefaultView.Sort = "FUND_TRANSFER_TYPE_NAME";
                            this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpFundTransfer, resultArgs.DataSource.Table, "FUND_TRANSFER_TYPE_NAME", "FUND_TRANSFER_TYPE_NAME");

                            // glkpFundTransfer.EditValue = FundTransferId == 0 ? glkpFundTransfer.Properties.GetKeyValue(0) : glkpFundTransfer.Properties.GetKeyValue(FundTransferId);
                            glkpFundTransfer.EditValue = string.IsNullOrEmpty(FundTransfer) ? "" : FundTransfer;
                            //  glkpFundTransfer.EditValue = string.IsNullOrEmpty(FundTransfer) ? glkpFundTransfer.Properties.GetKeyValue(0) : glkpFundTransfer.Properties.GetKeyValue(1); ; // (glkpFundTransfer.Properties.GetDisplayValueByKeyValue(1)); //!= null ? FundTransfer : glkpFundTransfer.Properties.GetKeyValue(0));
                            //glkpFundTransfer.EditValue = (glkpFundTransfer.Properties.GetDisplayValueByKeyValue(1)); //!= null ? FundTransfer : glkpFundTransfer.Properties.GetKeyValue(0));
                        }
                        else
                        {
                            XtraMessageBox.Show(resultArgs.Message);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Bosco.Utility.MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void frmVoucherBankReferenceDetails_Load(object sender, EventArgs e)
        {
            LoadFundTransfer();
        }

    }
}