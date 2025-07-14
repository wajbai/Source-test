using System;
using System.Windows.Forms;

using Bosco.Utility;
using Bosco.Model.UIModel;
using Bosco.Model.Transaction;

namespace ACPP.Modules.Master
{
    public partial class frmBankAccountView : frmFinanceBase
    {
        #region Variable Decelartion

        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        #endregion

        #region Properties
        private int bankAccountId = 0;
        private int BankAccountId
        {
            get
            {

                RowIndex = gvLedgerView.FocusedRowHandle;
                bankAccountId = gvLedgerView.GetFocusedRowCellValue(colBankAccountId) != null ? this.UtilityMember.NumberSet.ToInteger(gvLedgerView.GetFocusedRowCellValue(colBankAccountId).ToString()) : 0;
                return bankAccountId;
            }
            set
            {
                bankAccountId = value;
            }
        }
        private int ledgerid = 0;
        private int LedgerId
        {
            get
            {

                RowIndex = gvLedgerView.FocusedRowHandle;
                bankAccountId = gvLedgerView.GetFocusedRowCellValue(colLEdgerId) != null ? this.UtilityMember.NumberSet.ToInteger(gvLedgerView.GetFocusedRowCellValue(colLEdgerId).ToString()) : 0;
                return bankAccountId;
            }
            set
            {
                bankAccountId = value;
            }
        }
        #endregion

        #region Construtor
        public frmBankAccountView()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void frmLedgerView_Load(object sender, EventArgs e)
        {
            ApplyUserRights();
        }

        private void frmBankAccountView_Activated(object sender, EventArgs e)
        {
            //Added by Carmel Raj
            SetVisibileShortCuts(false, true);
            LoadBankAccountDetails();

            colCurrencyName.Visible = colOpExchangeRate.Visible = false;
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                colCurrencyName.Visible = colOpExchangeRate.Visible = true;
                colOpExchangeRate.VisibleIndex = colCurrencyName.VisibleIndex + 1;
            }

        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(BankAccount.CreateBankAccount);
            this.enumUserRights.Add(BankAccount.EditBankAccount);
            this.enumUserRights.Add(BankAccount.DeleteBankAccount);
            this.enumUserRights.Add(BankAccount.PrintBankAccount);
            this.enumUserRights.Add(BankAccount.ViewBankAccounts);
            this.ApplyUserRights(ucToolBarBankView, enumUserRights, (int)Menus.BankAccounts);
        }

        private void ucToolBarBankView_AddClicked(object sender, EventArgs e)
        {
            ShowLedgerForm((int)AddNewRow.NewRow);
        }

        private void ucToolBarBankView_EditClicked(object sender, EventArgs e)
        {
            ShowEditLedgerForm();
        }

        private void ucToolBarBankView_DeleteClicked(object sender, EventArgs e)
        {
            DeleteLedgerDetails();
        }

        private void ucToolBarBankView_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcLedgerView, this.GetMessage(MessageCatalog.Master.Ledger.BANK_ACCOUNT_PRINT_CAPTION), PrintType.DT, gvLedgerView, true);
        }

        private void ucToolBarBankView_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvLedgerView_DoubleClick(object sender, EventArgs e)
        {
            ShowEditLedgerForm();
        }

        private void gvLedgerView_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvLedgerView.RowCount.ToString();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvLedgerView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvLedgerView, colAccountNumber);
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Load Ledger details.
        /// </summary>

        private void LoadBankAccountDetails()
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    resultArgs = ledgerSystem.FetchBankAccountDetails();
                    gcLedgerView.DataSource = resultArgs.DataSource.Table;
                    gcLedgerView.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// Show Ledger Form
        /// </summary>

        private void ShowLedgerForm(int BkAccountId)
        {
            try
            {
                //frmBankAccount frmBankAccountAdd = new frmBankAccount(BankAccountId);
                //frmBankAccountAdd.UpdateHeld += new EventHandler(OnUpdateHeld);
                //frmBankAccountAdd.ShowDialog();
                frmLedgerDetailAdd frmbankaccountadd = new frmLedgerDetailAdd(BkAccountId == 0 ? 0 : LedgerId, ledgerSubType.BK, 0, BkAccountId);
                frmbankaccountadd.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmbankaccountadd.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }

        }

        /// <summary>
        /// Delete Ledger Details
        /// </summary>

        private void DeleteLedgerDetails()
        {
            try
            {
                //if (BankAccountId != 0)
                //{
                //    using (LedgerSystem ledgerSystem = new LedgerSystem())
                //    {
                if (gvLedgerView.RowCount != 0)
                {
                    if (BankAccountId != 0)
                    {
                        using (LedgerSystem ledgerSystem = new LedgerSystem())
                        {
                            using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                            {
                                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    voucherTransaction.LedgerId = LedgerId;
                                    resultArgs = voucherTransaction.MadeTransactionForLedger();
                                    //if row count is zero than no transaction is made
                                    if (resultArgs.DataSource.Table.Rows.Count == 0)
                                    {
                                        resultArgs = voucherTransaction.MappedLedger();
                                        if (resultArgs.DataSource.Table.Rows.Count == 0)
                                        {
                                             resultArgs = ledgerSystem.DeleteCategoryLedgerbyLedgerID(LedgerId);
                                             if (resultArgs.Success)
                                             {
                                                 ledgerSystem.BankAccountId = BankAccountId;
                                                 resultArgs = ledgerSystem.DeleteBankAccount();
                                                 if (resultArgs.Success)
                                                 {
                                                     this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                                     LoadBankAccountDetails();
                                                 }
                                                 if (resultArgs.Success && resultArgs.RowsAffected > 0)
                                                 {
                                                     this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                                 }
                                             }
                                        }
                                        else ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.LEDGER_MAPPED));
                                    }
                                    else ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.TRANSACTION_MADE_ALREADY_FOR_LEDGER));
                                }
                            }
                            //if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            //{

                            //    ledgerSystem.BankAccountId = BankAccountId;
                            //    resultArgs = ledgerSystem.DeleteBankAccount();
                            //    if (resultArgs.Success)
                            //    {
                            //        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                            //        LoadBankAccountDetails();
                            //    }
                            //    //else
                            //    //{
                            //    //    XtraMessageBox.Show(resultArgs.Message, this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    //}
                            //}
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NOSELECTION_FOR_DELETE));
                    }

                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        //private int CheckBankLedger()
        //{
        //    using (LedgerSystem LedgerSystem = new LedgerSystem())
        //    {
        //       // LedgerSystem.LedgerId = LedgerId;
        //       return LedgerSystem.CheckBankLedger();
        //    }
        //}

        public void ShowEditLedgerForm()
        {
            if (this.isEditable)
            {
                if (gvLedgerView.RowCount != 0)
                {
                    if (BankAccountId != 0)
                    {
                        ShowLedgerForm(BankAccountId);
                    }
                    else
                    {
                        if (!chkShowFilter.Checked)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NOSELECTION_FOR_EDIT));
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_USER_RIGHTS));
            }
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadBankAccountDetails();
            gvLedgerView.FocusedRowHandle = RowIndex;
        }

        #endregion

        private void frmBankAccountView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void frmBankAccountView_EnterClicked(object sender, EventArgs e)
        {
            ShowEditLedgerForm();
        }

        private void ucToolBarBankView_RefreshClicked(object sender, EventArgs e)
        {
            LoadBankAccountDetails();
        }

    }
}