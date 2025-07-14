using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Utility;
using Bosco.Model.Transaction;

namespace ACPP.Modules.Transaction
{
    public partial class frmFDRenewalAdd : frmBaseAdd
    {
        #region Event Handler
        public event EventHandler UpdateHeld;
        #endregion

        #region Decelaration
        private int BankAccountId = 0;
        private string FDRenewalDate = string.Empty;
        private string AccNumber = string.Empty;
        ResultArgs resultArgs = null;
        private const string TRANS_MODE = "TR";
        private const string FDTitle = "FD Renewal list on";
        private int LedgerId = 0;
        private int ProjectId = 0;
        private string transMode = string.Empty;
        private string FDNo = string.Empty;
        private string MaturedOn = string.Empty;
        #endregion]

        #region Constructor
        public frmFDRenewalAdd()
        {
            InitializeComponent();
        }
        public frmFDRenewalAdd(int BankId, string Date, string AccountNumber, int LedgerID, int ProjectId,string ProjectName,string TransMode,string MaturedOn,string FdNo)
            : this()
        {
            BankAccountId = BankId;
            FDRenewalDate = Date;
            AccNumber = AccountNumber;
            this.LedgerId = LedgerID;
            this.ProjectId = ProjectId;
            this.transMode = TransMode;
            this.MaturedOn = MaturedOn;
            this.FDNo = FdNo;
          //  lblProject.Text = ProjectName;
        }
        #endregion

        #region Events
        private void frmFDRenewalAdd_Load(object sender, EventArgs e)
        {
            SetTitle();
            AssignDate();
            FetchFDRenewal();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateFDRenewal())
                {
                    using (FDRenewalSystem fdRenewalSystem = new FDRenewalSystem())
                    {
                        fdRenewalSystem.FDAccountNumber = AccNumber;
                        fdRenewalSystem.FDNumber = txtFDNo.Text;
                        fdRenewalSystem.DepostiedOn = RenewedDate.DateTime;
                        fdRenewalSystem.MaturedOn = MaturedDate.DateTime;
                        fdRenewalSystem.FDAmount = this.UtilityMember.NumberSet.ToDecimal(txtAmount.Text);
                        fdRenewalSystem.InterestRate = this.UtilityMember.NumberSet.ToDecimal(txtInterestRate.Text);
                        fdRenewalSystem.InterestAmount = this.UtilityMember.NumberSet.ToDecimal(lblInterestTotalAmt.Text);
                        fdRenewalSystem.BankAccountId = this.BankAccountId;
                        fdRenewalSystem.Status = (int)FDRenewal.Renewal;
                        fdRenewalSystem.TransMode = TRANS_MODE;
                        fdRenewalSystem.LedgerId = this.LedgerId;
                        fdRenewalSystem.ProjectId = this.ProjectId;
                        resultArgs = fdRenewalSystem.SaveFDRenewal();
                        if (resultArgs.Success)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            ClearFDRenewal();
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }
        private void txtInterestRate_Leave(object sender, EventArgs e)
        {
            try
            {
                this.SetBorderColor(txtInterestRate);
                double amount = this.UtilityMember.NumberSet.ToDouble(txtAmount.Text);
                double interestRate = this.UtilityMember.NumberSet.ToDouble(txtInterestRate.Text);
                double calculateAmount = interestRate != 0 ? (amount / 100) * interestRate : amount;
                lblInterestTotalAmt.Text = calculateAmount.ToString();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }

        }
        private void txtFDNo_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtFDNo);
        }

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtAmount);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Methods

        private void FetchFDRenewal()
        {
            try
            {
                if (BankAccountId != 0)
                {
                    using (FDRenewalSystem fdRenewalSystem = new FDRenewalSystem(BankAccountId,this.MaturedOn,this.FDNo,this.transMode))
                    {
                        FDRenewedDate.DateTime = fdRenewalSystem.DepostiedOn;
                        FDMaturedDate.DateTime = fdRenewalSystem.MaturedOn;
                        txtFdNumber.Text = fdRenewalSystem.FDNumber;
                        txtFDAmount.Text = fdRenewalSystem.FDAmount.ToString();
                        txtInterestAmount.Text = fdRenewalSystem.InterestAmount.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }

        private void SetTitle()
        {
            this.Text = FDTitle + " " + FDRenewalDate;
        }

        private void AssignDate()
        {
            RenewedDate.DateTime = this.UtilityMember.DateSet.ToDate(FDRenewalDate, false);
            MaturedDate.DateTime = this.UtilityMember.DateSet.ToDate(FDRenewalDate, false);
        }

        public bool ValidateFDRenewal()
        {
            bool isFDRenewal = true;
            if (string.IsNullOrEmpty(txtFDNo.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FixedDeposit.FD_NUMBER_EMPTY));
                this.SetBorderColor(txtFDNo);
                isFDRenewal = false;
                txtFDNo.Focus();
            }
            else if (string.IsNullOrEmpty(txtAmount.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FixedDeposit.FD_AMOUNT_EMPTY));
                this.SetBorderColor(txtAmount);
                isFDRenewal = false;
                txtAmount.Focus();
            }
            else if (string.IsNullOrEmpty(txtInterestRate.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FixedDeposit.FD_INTEREST_RATE_EMPTY));
                this.SetBorderColor(txtInterestRate);
                isFDRenewal = false;
                txtInterestRate.Focus();
            }
            else if (!this.UtilityMember.DateSet.ValidateDate(RenewedDate.DateTime, MaturedDate.DateTime))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FixedDeposit.FD_MATURITY_DATE_EMPTY));
                MaturedDate.Focus();
                isFDRenewal = false;
            }
            else
            {
                btnSave.Focus();
            }
            return isFDRenewal;
        }

        private void ClearFDRenewal()
        {
            lblInterestTotalAmt.Text = txtFDNo.Text = txtAmount.Text = txtInterestRate.Text = string.Empty;

        }
        #endregion
    }
}