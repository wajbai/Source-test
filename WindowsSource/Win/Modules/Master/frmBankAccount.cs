using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Schema;
using System.Xml;
using ACPP.Modules.UIControls;
using System.Runtime.InteropServices;


namespace ACPP.Modules.Master
{
    public partial class frmBankAccount : frmFinanceBaseAdd
    {
        #region Variables
        ResultArgs resultArgs = null;
        public event EventHandler UpdateHeld;
        private int BankAccountId = 0;
        private int LedgerId = 0;
        DialogResult bankAccountResultSet = DialogResult.Cancel;

        #endregion

        #region Properties
        private int ledgerInsertId = 0;
        private int LedgerInsertId
        {
            set { ledgerInsertId = value; }
            get { return ledgerInsertId; }
        }

        #endregion


        public frmBankAccount()
        {
            InitializeComponent();
        }

        public frmBankAccount(int bankAccountId, int ProjectId = 0)
            : this()
        {
            BankAccountId = bankAccountId;
            using (BankAccountSystem ledgerSystem = new BankAccountSystem())
            {
                LedgerId = UcMapProject.Id = ledgerSystem.FetchLedgerId(BankAccountId.Equals(0) ? -1 : BankAccountId);
            }
            UcMapProject.ProjectId = ProjectId;
            UcMapProject.FormType = MapForm.BankAccount;
            if (BankAccountId == 0) { LoadBank(true); } else { LoadBank(false); }

            AssignLedgerDetails();
        }

        #region Methods
        /// <summary>
        /// To load the Bank Account Type
        /// </summary>
        /// <param name="InitialValue"></param>
        private void LoadBankAccoutType(bool InitialValue = true)
        {
            try
            {
                using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem())
                {
                    resultArgs = ledgerSystem.GetAccountType();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(lkpAccountType, resultArgs.DataSource.Table, ledgerSystem.AppSchema.AccountType.ACCOUNT_TYPEColumn.ToString(), ledgerSystem.AppSchema.AccountType.ACCOUNT_TYPE_IDColumn.ToString());
                        if (InitialValue) { lkpAccountType.EditValue = lkpAccountType.Properties.GetKeyValue(0); }
                    }
                    //else
                    //{
                    //    //XtraMessageBox.Show(resultArgs.Message);
                    //}
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        /// <summary>
        /// To load the Bank
        /// </summary>
        /// <param name="InitialValue"></param>
        private void LoadBank(bool InitialValue = true)
        {
            try
            {
                using (BankSystem bankSystem = new BankSystem())
                {
                    resultArgs = bankSystem.FetchBankDetailsforLookup();
                    lkpAccountType.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(lkpBank, resultArgs.DataSource.Table, bankSystem.AppSchema.Bank.BANKColumn.ColumnName, bankSystem.AppSchema.Bank.BANK_IDColumn.ColumnName);
                        //  if (InitialValue) { lkpBank.EditValue = lkpBank.Properties.GetKeyValue(0); }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }
        ///// <summary>
        ///// Load existingAccountCodes      
        ///// </summary>
        //public void LoadExistingAccountCodes()
        //{
        //    try
        //    {
        //        using (BankAccountSystem bankAccountSystem = new BankAccountSystem())
        //        {
        //            resultArgs = bankAccountSystem.FetchBankAccountCodes();
        //            if (resultArgs.DataSource != null && resultArgs.RowsAffected > 0)
        //            {
        //                this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpExistingAccCodes, resultArgs.DataSource.Table, bankAccountSystem.AppSchema.BankAccount.ACCOUNT_CODEColumn.ColumnName, bankAccountSystem.AppSchema.BankAccount.ACCOUNT_CODEColumn.ColumnName);
        //                glkpExistingAccCodes.EditValue = glkpExistingAccCodes.Properties.GetKeyValue(0);
        //                if (BankAccountId == 0)
        //                    txtAccountCode.Text = CodePredictor(glkpExistingAccCodes.Properties.GetKeyValue(0).ToString(), resultArgs.DataSource.Table);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string exception = ex.ToString();
        //    }
        //}
        #endregion

        private void frmBankAccount_Load(object sender, EventArgs e)
        {
            ucBankAccountCodes.Visible = false;
            SetTitle();
            detClosedDate.Properties.ShowClear = detOpenDate.Properties.ShowClear = true;
            if (BankAccountId == 0) { LoadBank(true); } else { LoadBank(false); }
            if (BankAccountId == 0) { LoadBankAccoutType(true); } else { LoadBankAccoutType(false); }
            if (BankAccountId == 0) { LoadCurrentDate(); }
            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                ApplyRights();
            }
            else
            {
                lkpBank.Properties.Buttons[1].Visible = true;
            }
            // UcAccountMapping RefMapProject = new UcAccountMapping(MapForm.Ledger);
            //UcMapProject

        }
        private void LoadCurrentDate()
        {
            detOpenDate.Text = this.UtilityMember.DateSet.GetDateToday();
        }

        private void ClearControls()
        {
            txtAccountCode.Text = "";
            txtAccountCode.Text = "";
            txtAccountNumber.Text = "";
            txtAccountHolderName.Text = "";
            mtxtNots.Text = "";
            detClosedDate.Text = "";
            txtOperatedBy.Text = "";
            lkpBank.EditValue = null;
            LoadCurrentDate();
        }

        private void AssignLedgerDetails()
        {
            try
            {
                if (BankAccountId != 0)
                {
                    using (BankAccountSystem ledgerSystem = new BankAccountSystem(BankAccountId))
                    {
                        txtAccountCode.Text = ledgerSystem.AccountCode;
                        txtAccountNumber.Text = ledgerSystem.AccountNumber;
                        txtAccountHolderName.Text = ledgerSystem.AccountHolderName;
                        lkpBank.EditValue = ledgerSystem.BankId;
                        LoadBankAccoutType();
                        lkpAccountType.EditValue = ledgerSystem.AccountTypeId;
                        detOpenDate.Text = this.UtilityMember.DateSet.ToDate(ledgerSystem.OpenedDate);
                        detClosedDate.Text = this.UtilityMember.DateSet.ToDate(ledgerSystem.ClosedDate);
                        txtOperatedBy.Text = ledgerSystem.OperatedBy;
                        LedgerId = ledgerSystem.FetchLedgerId(BankAccountId);
                        ledgerSystem.IsCostCentre = ledgerSystem.FetchLedgerCostCenterId(LedgerId);
                        mtxtNots.Text = ledgerSystem.BankAccNotes;
                        if (ledgerSystem.IsFCRAAccount == (int)YesNo.No)
                            chkFCRAAccount.Checked = false;
                        else
                            chkFCRAAccount.Checked = true;
                    }
                }
                //lkpAccountType.Enabled = false;
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateBankAccountDetails())
            {
                bankAccountResultSet = DialogResult.OK;
                // this.ShowWaitDialog();
                try
                {
                    using (LedgerSystem ledgerSystem = new LedgerSystem())
                    {
                        ledgerSystem.AccountCode = txtAccountCode.Text.Trim().ToUpper();
                        ledgerSystem.AccountNumber = txtAccountNumber.Text.Trim();
                        ledgerSystem.AccountHolderName = txtAccountHolderName.Text.Trim();
                        ledgerSystem.AccountTypeId = this.UtilityMember.NumberSet.ToInteger(lkpAccountType.EditValue.ToString());
                        ledgerSystem.BankId = this.UtilityMember.NumberSet.ToInteger(lkpBank.EditValue.ToString());
                        ledgerSystem.OpenedDate = this.UtilityMember.DateSet.ToDate(detOpenDate.Text.ToString(), DateFormatInfo.DateFormatYMD);
                        ledgerSystem.ClosedDate = this.UtilityMember.DateSet.ToDate(detClosedDate.Text.ToString(), DateFormatInfo.DateFormatYMD);
                        ledgerSystem.OperatedBy = txtOperatedBy.Text.Trim();
                        ledgerSystem.BankAccountId = (BankAccountId == (int)AddNewRow.NewRow) ? (int)AddNewRow.NewRow : BankAccountId;
                        ledgerSystem.LedgerCode = txtAccountCode.Text.Trim();
                        ledgerSystem.LedgerName = txtAccountNumber.Text.Trim();
                        ledgerSystem.GroupId = (int)FixedLedgerGroup.BankAccounts;//Bank Account Group Id
                        ledgerSystem.LedgerId = LedgerId;
                        ledgerSystem.LedgerType = ledgerSubType.GN.ToString(); //Bank Account leger
                        ledgerSystem.LedgerSubType = ledgerSubType.BK.ToString(); //Saving Account
                        // ledgerSystem.IsCostCentre = (chkCostCentre.Checked) ? (int)YesNo.Yes : (int)YesNo.No;
                        ledgerSystem.IsFCRAAccount = (chkFCRAAccount.Checked) ? (int)YesNo.Yes : (int)YesNo.No;
                        ledgerSystem.LedgerNotes = mtxtNots.Text.Trim();
                        ledgerSystem.BankAccNotes = mtxtNots.Text.Trim();
                        ledgerSystem.SortId = (int)LedgerSortOrder.Bank;
                        ledgerSystem.BankAccountId = (BankAccountId == (int)AddNewRow.NewRow) ? (int)AddNewRow.NewRow : BankAccountId;

                        if (!string.IsNullOrEmpty(this.AppSetting.BookBeginFrom))
                        {

                            ledgerSystem.dtMappingLedgers = UcMapProject.GetMappingDetails;
                            DataView dvLegalEntity = new DataView(ledgerSystem.dtMappingLedgers);
                            dvLegalEntity.RowFilter = "SELECT=1";
                            DataTable dtFilteredEntity = dvLegalEntity.ToTable();
                            if (dtFilteredEntity != null)
                            {
                                int LegalEntityId = dtFilteredEntity.Rows.Count > 0 ? UtilityMember.NumberSet.ToInteger(dtFilteredEntity.Rows[0]["CUSTOMERID"].ToString()) : 0;
                                DataView dvLegalEntityFilter2 = new DataView(dtFilteredEntity);
                                dvLegalEntityFilter2.RowFilter = String.Format("CUSTOMERID={0}", LegalEntityId);
                                DataTable dtFilteredEntity2 = dvLegalEntityFilter2.ToTable();
                                if (dtFilteredEntity2.Rows.Count == dtFilteredEntity.Rows.Count)
                                {
                                    DataView dvDivisionFilter = new DataView(dtFilteredEntity2);
                                    dvDivisionFilter.RowFilter = "DIVISION_ID=1 AND LEGAL_LEDGER_ID<>0";
                                    DataTable dtDivsionFiltered = dvDivisionFilter.ToTable();
                                    if (dtDivsionFiltered.Rows.Count == 0)
                                    {
                                        ledgerSystem.MapLedgerId = LedgerId;
                                        ledgerSystem.FDLeger = false;
                                        resultArgs = ledgerSystem.SaveBankLedger(false);
                                        if (resultArgs.Success)
                                        {
                                            this.ReturnValue = resultArgs.RowUniqueId;
                                            this.ReturnDialog = System.Windows.Forms.DialogResult.OK;

                                            if (bankAccountResultSet.Equals(DialogResult.Cancel))
                                                this.DialogResult = DialogResult.OK;
                                            LedgerInsertId = ledgerSystem.BankAccountId;
                                            lkpAccountType.Focus();
                                            if (BankAccountId == 0)
                                            {
                                                UcMapProject.GridClear = true;
                                                ClearControls();
                                            }
                                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                                            if (UpdateHeld != null)
                                                UpdateHeld(this, e);
                                        }
                                    }
                                    else
                                    {
                                        ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.LOCAL_PROJECT_RESTRICTED_WITH_BANK_ACCOUNT));
                                    }
                                }
                                else
                                {
                                    ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.DEFERENT_LEGAL_ENTITY_RESTRICTED_WITH_BANK_ACCOUNT));
                                }
                            }
                        }
                        else
                            ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.BOOK_BEGINNING_DATE_EMPTY));
                    }
                }
                catch (Exception Ex)
                {
                    MessageRender.ShowMessage(Ex.Message);
                }
                finally
                {
                    // this.CloseWaitDialog();
                }
            }
        }

        /// <summary>
        /// To Validate Ledger Details
        /// </summary>
        /// <returns></returns>

        private bool ValidateBankAccountDetails()
        {
            bool IsGroudValid = true;
            if (lkpAccountType.EditValue == null)
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_ACCOUNT_TYPE_EMPTY));
                this.SetBorderColor(lkpAccountType);
                IsGroudValid = false;
                lkpAccountType.Focus();
            }
            else if (lkpBank.EditValue == null)
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_BANK_EMPTY));
                this.SetBorderColor(lkpBank);
                IsGroudValid = false;
                lkpBank.Focus();
            }
            else if (string.IsNullOrEmpty(txtAccountNumber.Text.Trim()))
            {

                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_ACCOUNT_NUMBER_EMPTY));
                this.SetBorderColor(txtAccountNumber);
                IsGroudValid = false;
                txtAccountNumber.Focus();
            }
            else if (string.IsNullOrEmpty(detOpenDate.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_DATE_OPEN_EMPTY));
                this.SetBorderColorForDateTimeEdit(detOpenDate);
                IsGroudValid = false;
                detOpenDate.Focus();
            }
            else if (!string.IsNullOrEmpty(detClosedDate.Text.Trim()))
            {
                //if (detClosedDate.DateTime == DateTime.MinValue)
                //{
                if (!this.UtilityMember.DateSet.ValidateDate(detOpenDate.DateTime, detClosedDate.DateTime))
                {
                    //XtraMessageBox.Show("Bank CloseDate should not be less than the Bank Open Date", "AcMe++", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Ledger.BANK_ACCOUNT_CLOSEDATE_VALIDATION));
                    IsGroudValid = false;
                    detOpenDate.Focus();
                }
                //}
            }
            if (BankAccountId != 0 && (detClosedDate.DateTime != DateTime.MinValue))
            {
                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    //On 19/10/2021, TO have common method to to check ledger balance date
                    //resultArgs = ledgersystem.CheckTransactionExistsByDateClosed(LedgerId, detClosedDate.DateTime);
                    resultArgs = ledgersystem.CheckLedgerClosedDate(LedgerId, detClosedDate.DateTime);
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Ledger.BANK_ACCOUNT_CLOSEDATE_TRANSACTION));
                        IsGroudValid = false;
                    }
                }
            }

            return IsGroudValid;
        }

        /// <summary>
        /// To set the form title in runtime
        /// </summary>

        private void SetTitle()
        {
            this.Text = BankAccountId == 0 ? this.GetMessage(MessageCatalog.Master.Ledger.BANK_ACCOUNT_ADD) : this.GetMessage(MessageCatalog.Master.Ledger.BANK_ACCOUNT_EDIT);
        }

        private void lkpAccountType_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(lkpAccountType);
        }

        private void txtAccountHolderName_Leave(object sender, EventArgs e)
        {
            txtAccountHolderName.Text = this.UtilityMember.StringSet.ToSentenceCase(txtAccountHolderName.Text);
        }

        private void txtOperatedBy_Leave(object sender, EventArgs e)
        {
            txtOperatedBy.Text = this.UtilityMember.StringSet.ToSentenceCase(txtOperatedBy.Text);
        }

        private void lkpBank_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(lkpBank);
        }

        private void txtAccountNumber_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtAccountNumber);
        }

        private void lkpBank_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                LoadBankAdd();
            }
        }

        public void LoadBankAdd()
        {
            if (this.AppSetting.LockMasters == (int)YesNo.No)
            {
                frmBankAdd frmAdd = new frmBankAdd();
                frmAdd.ShowDialog();
                if (frmAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                {
                    LoadBank();
                    if (frmAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmAdd.ReturnValue.ToString()) > 0)
                    {
                        lkpBank.EditValue = this.UtilityMember.NumberSet.ToInteger(frmAdd.ReturnValue.ToString());
                    }
                }
            }
        }

        private void detOpenDate_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForDateTimeEdit(detOpenDate);
        }

        private void btnMaptoProject_Click(object sender, EventArgs e)
        {
            frmMapProjectLedger frmMapPro = new frmMapProjectLedger(MapForm.Ledger, (LedgerInsertId == 0) ? LedgerId : LedgerInsertId);
            frmMapPro.ShowDialog();
        }

        //private void btnNew_Click(object sender, EventArgs e)
        //{
        //    ClearControls();
        //    lkpAccountType.Focus();
        //    txtAccountCode.Text = CodePredictor(glkpExistingAccCodes.Properties.GetKeyValue(0).ToString(), resultArgs.DataSource.Table);
        //}

        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.F3))
            {
                detOpenDate.Focus();

            }
            if (KeyData == (Keys.F2))
            {
                LoadBankAdd();
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        private void UcMapProject_ProcessGridKey(object sender, EventArgs e)
        {
            if (UcMapProject.ucGridControl.IsLastRow)
            {
                mtxtNots.Focus();
            }
        }

        private void ApplyRights()
        {
            if (CommonMethod.ApplyUserRights((int)Bank.CreateBank) != 0)
            {
                lkpBank.Properties.Buttons[1].Visible = true;
            }
            else
            {
                lkpBank.Properties.Buttons[1].Visible = false;
            }
        }
    }
}


