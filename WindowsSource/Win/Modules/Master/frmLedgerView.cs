/*Added By:Michael R
 *Added On:07/08/2013
 *Purpose : To have the details of the bank.
 *Modified On: 
 *Modified Purpose:
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

using ACPP.Modules.Master;
using Bosco.Utility;
using Bosco.Model.UIModel;
using Bosco.Utility.CommonMemberSet;
using DevExpress.XtraPrinting;
using DevExpress.XtraSplashScreen;
using Bosco.Model.Transaction;
using DevExpress.XtraBars;
using Bosco.Utility.ConfigSetting;



namespace ACPP.Modules.Master
{
    public partial class frmLedgerView : frmFinanceBase
    {
        #region Variable Decelartion

        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        private int BankAccountId = 0;
        private ledgerSubType ledgerSubType;
        private bool IsSuntryDebtorsCreditor = false;
        private bool IsCashLedgers = false;
        DataTable dtLedgerTypes = new DataTable();
        #endregion

        #region Properties
        private int ledgerId = 0;
        private int LedgerId
        {
            get
            {

                RowIndex = gvLedgerView.FocusedRowHandle;
                ledgerId = gvLedgerView.GetFocusedRowCellValue(colLedgerId) != null ? this.UtilityMember.NumberSet.ToInteger(gvLedgerView.GetFocusedRowCellValue(colLedgerId).ToString()) : 0;
                //if(gvLedgerView.GetFocusedRowCellValue(colLedgerId) !=null)
                //    ledgerId = this.UtilityMember.NumberSet.ToInteger(gvLedgerView.GetFocusedRowCellValue(colLedgerId).ToString());
                return ledgerId;
            }
            set
            {
                ledgerId = value;
            }
        }

        private string ledgerSub = "";
        private string LedgerSub
        {
            get
            {

                RowIndex = gvLedgerView.FocusedRowHandle;
                ledgerSub = gvLedgerView.GetFocusedRowCellValue(colLedgerSubType) != null ? gvLedgerView.GetFocusedRowCellValue(colLedgerSubType).ToString() : string.Empty;
                // ledgerSub = gvLedgerView.GetFocusedRowCellValue(colLedgerSubType).ToString();
                return ledgerSub;
            }
            set
            {
                ledgerSub = value;
            }
        }

        private int BankAccountLedgerId = 0;
        private int BankAccId
        {
            get
            {
                BankAccountLedgerId = this.UtilityMember.NumberSet.ToInteger(gvLedgerView.GetFocusedRowCellValue(colBankAccountId).ToString());
                return BankAccountLedgerId;
            }
            set
            {
                BankAccountLedgerId = value;
            }
        }

        private string HeadOfficeLedgerName
        {
            get
            {
                string rtn = string.Empty;
                if (gvLedgerView.DataSource != null)
                {
                    if (gvLedgerView.FocusedRowHandle > 0 && gvLedgerView.GetDataRow(gvLedgerView.FocusedRowHandle) != null)
                    {
                        using (LedgerSystem ledgerSystem = new LedgerSystem())
                        {
                            DataRow dr = gvLedgerView.GetDataRow(gvLedgerView.FocusedRowHandle) as DataRow;
                            rtn = dr[ledgerSystem.AppSchema.Ledger.HEADOFFICE_LEDGER_NAMEColumn.ColumnName].ToString();
                        }
                    }
                }
                return rtn;
            }

        }


        #endregion

        #region Construtor
        public frmLedgerView()
        {
            InitializeComponent();
        }
        public frmLedgerView(ledgerSubType ledgerType)
            : this()
        {
            ledgerSubType = ledgerType;
        }

        public frmLedgerView(ledgerSubType ledgerType, bool isSuntryDebtorsCreditor = false, bool isCashLedgers = false)
            : this()
        {
            ledgerSubType = ledgerType;
            IsSuntryDebtorsCreditor = isSuntryDebtorsCreditor;
            IsCashLedgers = isCashLedgers;
        }
        #endregion

        #region Events

        private void ucToolBarBankView_AddClicked(object sender, EventArgs e)
        {
            if (ledgerSubType.GN == ledgerSubType || ledgerSubType.CA == ledgerSubType || ledgerSubType.FD == ledgerSubType)
            {
                ShowLedgerForm((int)AddNewRow.NewRow);
            }
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
            if (ledgerSubType == ledgerSubType.GN)
            {
                PrintGridViewDetails(gcLedgerView, this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_PRINT_CAPTION), PrintType.DT, gvLedgerView);
            }
            else
            {
                PrintGridViewDetails(gcLedgerView, this.GetMessage(MessageCatalog.Master.Ledger.FD_LEDGER_PRINT_CAPTION), PrintType.DT, gvLedgerView);
            }
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
                this.SetFocusRowFilter(gvLedgerView, colLedgerName);
            }
            pnlLedgerDetails.Visible = false;
        }
        private bool ValidateLedgerDelete()
        {
            bool IsGroupVaild = true;
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                int AcFlag = ledgerSystem.GetAccessFlag(LedgerId);
                if (AcFlag == (int)AccessFlag.Readonly || AcFlag == (int)AccessFlag.Editable)
                {
                    IsGroupVaild = false;
                }
            }
            return IsGroupVaild;
        }

        private bool ValidateLedgerEdit()
        {
            bool IsGroupVaild = true;
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                int AcFlag = ledgerSystem.GetAccessFlag(LedgerId);
                if (AcFlag == (int)AccessFlag.Readonly)
                {
                    IsGroupVaild = false;
                }
            }
            return IsGroupVaild;
        }

        private void frmLedgerView_Load(object sender, EventArgs e)
        {
            this.Text = (ledgerSubType == ledgerSubType.GN || ledgerSubType == ledgerSubType.CA) ? this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_TITLE) : ledgerSubType == ledgerSubType.FD ? this.GetMessage(MessageCatalog.Master.FDLedger.FD_LEDGER) : "TDS Ledger";
            colFDInvestmentType.Visible = false;
            if (IsSuntryDebtorsCreditor)
            {
                this.Text += " - Sundry Creditors & Debtors";
                ApplyUserRights();
            }
            else if (IsCashLedgers)
            {
                this.Text += " - Cash Ledgers";
                ApplyUserRights();
            }
            else
            {
                if (ledgerSubType.GN == ledgerSubType || ledgerSubType.FD == ledgerSubType)
                {
                    ApplyUserRights();
                }
                else
                {
                    ucToolBarBankView.VisibleAddButton = ucToolBarBankView.VisibleEditButton = ucToolBarBankView.VisibleDeleteButton = BarItemVisibility.Never;
                    ucToolBarBankView.VisiblePrintButton = BarItemVisibility.Always;
                }
                // Set Visible false to Add/Edit/Delete
                if (ledgerSubType.FD != ledgerSubType)
                {
                    this.LockMasters(ucToolBarBankView);
                }

                //On 12/10/2020, Enable edit option to modify projectg closed on even if lock master is enabled 
                //On 22/09/2023, for sdb mumbai, lock ledger editing feature
                if (AppSetting.LockMasters == 1 && AppSetting.HeadofficeCode.ToUpper() != "SDBINB")
                {
                    ucToolBarBankView.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
                }

                if (AppSetting.HeadofficeCode.ToUpper() == "SDBINB")
                {
                    this.isEditable = false;
                }
                //----------------------------------------------
            }

            lblLedgerNameValue.Text = string.Empty;
            detLedgerDateClosed.Text = string.Empty;
        }

        private void frmLedgerView_Activated(object sender, EventArgs e)
        {
            //Added by Carmel Raj
            SetVisibileShortCuts(false, true);
            FetchLedgerDetails();

            colCurrencyName.Visible = colOpExchangeRate.Visible = false;
            if (this.AppSetting.AllowMultiCurrency == 1 && (ledgerSubType == Bosco.Utility.ledgerSubType.BK ||
                ledgerSubType == Bosco.Utility.ledgerSubType.CA || ledgerSubType == Bosco.Utility.ledgerSubType.FD))
            {
                colCurrencyName.Visible = colOpExchangeRate.Visible = true;
                Int32 index = colNature.VisibleIndex;

                colCurrencyName.VisibleIndex = (index + 1);
                colOpExchangeRate.VisibleIndex = colCurrencyName.VisibleIndex + 1;
            }
        }

        private void setLedgerRights()
        {
            ucToolBarBankView.VisibleAddButton = ucToolBarBankView.VisibleEditButton = ucToolBarBankView.VisibleDeleteButton = BarItemVisibility.Never;
        }

        private void ApplyUserRights()
        {
            if (ledgerSubType == ledgerSubType.GN && IsSuntryDebtorsCreditor != true)
            {
                //if (SettingProperty.EnableLedgers)
                //{
                this.enumUserRights.Add(Ledger.CreateLedger);
                this.enumUserRights.Add(Ledger.EditLedger);
                this.enumUserRights.Add(Ledger.DeleteLedger);
                this.enumUserRights.Add(Ledger.PrintLedger);
                this.enumUserRights.Add(Ledger.ViewLedger);
                this.ApplyUserRights(ucToolBarBankView, this.enumUserRights, (int)Menus.Ledger);
                //}
                //else
                //{
                //    setLedgerRights();
                //    this.isEditable = false;
                //}
            }
            else if (ledgerSubType == ledgerSubType.FD)
            {
                this.enumUserRights.Add(FDLedger.CreateFDLedger);
                this.enumUserRights.Add(FDLedger.EditFDLedger);
                this.enumUserRights.Add(FDLedger.DeleteFDLedger);
                this.enumUserRights.Add(FDLedger.PrintFDLedger);
                this.enumUserRights.Add(FDLedger.ViewFDLedger);
                this.ApplyUserRights(ucToolBarBankView, this.enumUserRights, (int)Menus.FixedDepositLedger);
                //this.isEditable = true;
            }
            else if (IsCashLedgers) //  (IsSuntryDebtorsCreditor || IsCashLedgers)  //29/04/2025, *Chinna
            {
                this.enumUserRights.Add(BankAccount.CreateBankAccount);
                this.enumUserRights.Add(BankAccount.EditBankAccount);
                this.enumUserRights.Add(BankAccount.DeleteBankAccount);
                this.enumUserRights.Add(BankAccount.PrintBankAccount);
                this.enumUserRights.Add(BankAccount.ViewBankAccounts);
                this.ApplyUserRights(ucToolBarBankView, enumUserRights, (int)Menus.BankAccounts);
            }
            else if (IsSuntryDebtorsCreditor)
            {
                // This is add and Edit Rights for SundryCreditors and Debtors
                this.enumUserRights.Add(SundryCreditorsDebtorsLedger.CreateSundryCreditorsDebtorsLedgers);
                this.enumUserRights.Add(SundryCreditorsDebtorsLedger.EditSundryCreditorsDebtorsLedgers);
                this.enumUserRights.Add(SundryCreditorsDebtorsLedger.DeleteSundryCreditorsDebtorsLedgers);
                this.enumUserRights.Add(SundryCreditorsDebtorsLedger.ViewSundryCreditorsDebtorsLedgers);
                this.ApplyUserRights(ucToolBarBankView, this.enumUserRights, (int)Menus.SundryCreditorsDebtors);
            }
            else
            {
                this.enumUserRights.Add(FDLedger.CreateFDLedger);
                this.enumUserRights.Add(FDLedger.EditFDLedger);
                this.enumUserRights.Add(FDLedger.DeleteFDLedger);
                this.enumUserRights.Add(FDLedger.PrintFDLedger);
                this.enumUserRights.Add(FDLedger.ViewFDLedger);
                this.ApplyUserRights(ucToolBarBankView, this.enumUserRights, (int)Menus.FixedDepositLedger);
            }
        }

        private void ucToolBarBankView_RefreshClicked(object sender, EventArgs e)
        {
            FetchLedgerDetails();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Load Ledger details.
        /// </summary>

        private void FetchLedgerDetails()
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    resultArgs = ledgerSystem.FetchLedgerDetails();
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        string fitler = string.Empty;
                        DataView dvFDLedgers = resultArgs.DataSource.Table.DefaultView;

                        if (ledgerSubType == ledgerSubType.FD)
                        {
                            fitler = ledgerSystem.AppSchema.Ledger.GROUP_IDColumn.ColumnName + " IN (" + (int)FixedLedgerGroup.FixedDeposit + ")"; //To filter the FD Ledger from the general Ledger
                            colGroup.Visible = colIsBankInterest.Visible = colIsCC.Visible = false;
                            colIsGST.Visible = colGSTClass.Visible = colGSTType.Visible = false;
                            colFDInvestmentType.Visible = true;
                        }
                        else if (ledgerSubType == ledgerSubType.TDS)
                        {
                            fitler = ledgerSystem.AppSchema.Ledger.IS_TDS_LEDGERColumn.ColumnName + " IN (" + (int)YesNo.Yes + ")"; //To filter t he FD Ledger from the general Ledger
                        }
                        else if (IsSuntryDebtorsCreditor)
                        {
                            fitler += (!string.IsNullOrEmpty(fitler) ? " AND " : "") +
                                ledgerSystem.AppSchema.Ledger.GROUP_IDColumn.ColumnName + " IN (" + (int)TDSDefaultLedgers.SunderyCreditors + "," + (int)TDSDefaultLedgers.SundryDebtors + ")";
                        }
                        else if (IsCashLedgers)
                        {
                            colGroup.Visible = colIsBankInterest.Visible = colIsCC.Visible = false;
                            colIsGST.Visible = colGSTClass.Visible = colGSTType.Visible = false;
                            colFDInvestmentType.Visible = false;

                            fitler += (!string.IsNullOrEmpty(fitler) ? " AND " : "") +
                                ledgerSystem.AppSchema.Ledger.GROUP_IDColumn.ColumnName + " IN (" + (int)FixedLedgerGroup.Cash + ")";
                        }
                        else
                        {
                            // Don't show the FD ledgers, BAnk Account, Sundry Creditors & Debtors Ledgers in the ledger view.
                            fitler = ledgerSystem.AppSchema.Ledger.GROUP_IDColumn.ColumnName + " NOT IN (" + (int)FixedLedgerGroup.Cash + "," + (int)FixedLedgerGroup.FixedDeposit + "," + (int)FixedLedgerGroup.BankAccounts + "," +
                                                                (int)TDSDefaultLedgers.SunderyCreditors + "," + (int)TDSDefaultLedgers.SundryDebtors + ")";
                        }

                        dvFDLedgers.RowFilter = fitler;
                        gcLedgerView.DataSource = dvFDLedgers.ToTable();
                        gcLedgerView.RefreshDataSource();
                        dvFDLedgers.RowFilter = "";
                    }

                    //Hide GST Column

                    if (ledgerSubType != ledgerSubType.FD)
                        colGSTType.Visible = colGSTClass.Visible = colIsGST.Visible = (this.AppSetting.EnableGST == "1");
                    else
                    {
                        colFDInvestmentType.VisibleIndex = 4; //3
                    }


                }
                pnlLedgerDetails.Visible = false;
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// Show Ledger Form (We have command this for ledger creation should be in  one place not in all the place .It was told  by Mr.Chandhu)
        /// </summary>

        private void ShowLedgerForm(int LedgerId, int BankAccountId = 0)
        {
            try
            {
                if (this.AppSetting.LockMasters == (int)YesNo.No || ledgerSubType.FD == ledgerSubType || IsSuntryDebtorsCreditor || IsCashLedgers)
                {
                    if (ledgerSubType.FD.Equals(ledgerSubType))
                    {
                        frmLedgerBankAccountAdd frmLedgerAdd = new frmLedgerBankAccountAdd(LedgerId, ledgerSubType);
                        frmLedgerAdd.UpdateHeld += new EventHandler(OnUpdateHeld);
                        frmLedgerAdd.ShowDialog();
                    }
                    else
                    {
                        frmLedgerDetailAdd frmLedgerDetailAdd = new frmLedgerDetailAdd(LedgerId, ledgerSubType, 0, BankAccountId, IsSuntryDebtorsCreditor, IsCashLedgers);
                        frmLedgerDetailAdd.UpdateHeld += new EventHandler(OnUpdateHeld);
                        frmLedgerDetailAdd.ShowDialog();
                    }
                }
                else if (this.AppSetting.LockMasters == (int)YesNo.Yes && BankAccId == 0)
                {
                    //On 06/02/2023, Enable edit option to modify ledger closed on even if lock master is enabled 
                    ShowEditLedgerDetails();
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }
        /// <summary>
        /// Show bank Account Form
        /// </summary>
        /// <param name="BankAccountId"></param>
        private void ShowBankAccountForm(int BankAccountId)
        {
            try
            {
                frmBankAccount frmBankAccAdd = new frmBankAccount(BankAccountId);
                frmBankAccAdd.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmBankAccAdd.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }

        }

        /// <summary>
        /// Show Fixed Deposit  Form
        /// </summary>
        /// <param name="BankAccountId"></param>
        //private void ShowFixedDepositForm(int BankAccountId)
        //{
        //    try
        //    {
        //        frmFixedDepositAdd frmFixedDepositAdd = new frmFixedDepositAdd(BankAccountId);
        //        frmFixedDepositAdd.UpdateHeld += new EventHandler(OnUpdateHeld);
        //        frmFixedDepositAdd.ShowDialog();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
        //    }
        //    finally { }
        //}

        /// <summary>
        /// Delete Ledger Details
        /// </summary>

        private void DeleteLedgerDetails()
        {
            try
            {
                if ((ValidateLedgerDelete()))
                {
                    if (gvLedgerView.RowCount != 0)
                    {
                        if (LedgerId != 0)
                        {
                            LedgerAccessFlag(LedgerId);
                            if (LedgerAccessRights == 2 || LedgerAccessRights == 1 || LedgerAccessRights == 3)
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_FAILURE));
                            }
                            else
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
                                                //Check ledger is used in budget
                                                resultArgs = ledgerSystem.IsBudgetedLedger(LedgerId);
                                                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count == 0)
                                                {
                                                    resultArgs = voucherTransaction.MappedLedger();
                                                    if (resultArgs.DataSource.Table.Rows.Count == 0)
                                                    {
                                                        BankAccountId = CheckBankLedger();
                                                        if (BankAccountId != 0)
                                                        {
                                                            ledgerSystem.BankAccountId = BankAccountId;
                                                            resultArgs = ledgerSystem.DeleteCategoryLedgerbyLedgerID(LedgerId);
                                                            if (resultArgs.Success)
                                                            {
                                                                resultArgs = ledgerSystem.DeleteBankAccount();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            ledgerSystem.LedgerId = LedgerId;
                                                            if (ledgerSystem.IsTDSLedgerExits() > 0)
                                                            {
                                                                resultArgs = ledgerSystem.DeleteCategoryLedgerbyLedgerID(LedgerId);
                                                                if (resultArgs.Success)
                                                                {
                                                                    using (Bosco.Model.TDS.LedgerProfileSystem ledgerProfileSystem = new Bosco.Model.TDS.LedgerProfileSystem())
                                                                    {
                                                                        ledgerProfileSystem.LedgerID = LedgerId;
                                                                        resultArgs = ledgerProfileSystem.DeleteLedgerProfile();
                                                                        if (resultArgs != null && resultArgs.Success)
                                                                        {
                                                                            resultArgs = ledgerSystem.DeleteLedgerDetails(LedgerId);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                resultArgs = ledgerSystem.DeleteCategoryLedgerbyLedgerID(LedgerId);
                                                                if (resultArgs.Success)
                                                                {
                                                                    using (Bosco.Model.TDS.LedgerProfileSystem ledgerProfileSystem = new Bosco.Model.TDS.LedgerProfileSystem())
                                                                    {
                                                                        ledgerProfileSystem.LedgerID = LedgerId;
                                                                        resultArgs = ledgerProfileSystem.DeleteLedgerProfile();
                                                                        if (resultArgs != null && resultArgs.Success)
                                                                        {
                                                                            resultArgs = ledgerSystem.DeleteLedgerDetails(LedgerId);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        if (resultArgs.Success && resultArgs.RowsAffected > 0)
                                                        {
                                                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                                            FetchLedgerDetails();
                                                        }
                                                    }
                                                    else ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.LEDGER_MAPPED));
                                                }
                                                else
                                                {
                                                    ShowMessageBox("Budget is made for this ledger, can not be deleted.");
                                                }
                                            }
                                            else ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.TRANSACTION_MADE_ALREADY_FOR_LEDGER));
                                        }
                                    }
                                }
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
                else
                {
                    XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.Ledger.FIXED_LEDGER_DELETE), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private int CheckBankLedger()
        {
            using (LedgerSystem LedgerSystem = new LedgerSystem())
            {
                LedgerSystem.LedgerId = LedgerId;
                return LedgerSystem.CheckBankLedger();
            }
        }

        public void ShowEditLedgerForm()
        {
            if (ledgerSubType == ledgerSubType.FD)
            {
                if (ledgerSubType.FD == ledgerSubType) //(this.isEditable)
                {
                    ShowEditFrom();
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_USER_RIGHTS));
                }
            }
            else
            {
                if (ledgerSubType.GN == ledgerSubType || ledgerSubType.CA == ledgerSubType)
                {
                    if (this.isEditable)
                    {
                        ShowEditFrom();
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_USER_RIGHTS));
                    }
                }
            }
        }

        private void ShowEditFrom()
        {
            if (ValidateLedgerEdit())
            {
                if (gvLedgerView.RowCount > 0)
                {
                    if (LedgerId > 0)
                    {
                        //if (LedgerSub == ledgerSubType.BK.ToString())
                        //{
                        //    ShowBankAccountForm(BankAccId);
                        //}
                        //else
                        //{
                        ShowLedgerForm(LedgerId, BankAccId);
                        //}
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
                XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.Ledger.FIXED_LEDGER_EDIT), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SetRights()
        {
            try
            {
                if (ledgerSubType == ledgerSubType.GN)
                {
                    using (MasterRightsSystem masterRights = new MasterRightsSystem())
                    {
                        masterRights.MasterName = this.Text;
                        if (masterRights.MasterRights() == (int)MasterRights.ReadOnly)
                        {
                            ucToolBarBankView.VisibleAddButton = ucToolBarBankView.VisibleDeleteButton = ucToolBarBankView.VisibleEditButton = BarItemVisibility.Never;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchLedgerDetails();
            gvLedgerView.FocusedRowHandle = RowIndex;
        }

        /// <summary>
        /// This method is used to modify project closed on, If License master is locked
        /// </summary>
        private void ShowEditLedgerDetails()
        {
            Int32 closedby = 0;
            string msg = "As Ledger is closed by Head Office, you can't modify its closed date.";
            string holedgername = HeadOfficeLedgerName;
            if (string.IsNullOrEmpty(holedgername))
            {
                pnlLedgerDetails.Visible = true;
                lblLedgerNameValue.Text = string.Empty;
                detLedgerDateClosed.Text = string.Empty;
                if (pnlLedgerDetails.Visible && gvLedgerView.DataSource != null && gvLedgerView.RowCount > 0)
                {
                    if (gvLedgerView.GetRowCellValue(gvLedgerView.FocusedRowHandle, colLedgerName.FieldName) != null)
                    {
                        lblLedgerNameValue.Text = gvLedgerView.GetRowCellValue(gvLedgerView.FocusedRowHandle, colLedgerName.FieldName).ToString();
                        string closedon = gvLedgerView.GetRowCellValue(gvLedgerView.FocusedRowHandle, colCloseDate.FieldName).ToString();
                        closedby = UtilityMember.NumberSet.ToInteger(gvLedgerView.GetRowCellValue(gvLedgerView.FocusedRowHandle, colClosedBy.FieldName) == null ? "0" : gvLedgerView.GetRowCellValue(gvLedgerView.FocusedRowHandle, colClosedBy.FieldName).ToString());

                        if (!String.IsNullOrEmpty(closedon))
                        {
                            DateTime dtClosedOn = UtilityMember.DateSet.ToDate(closedon, false);
                            if (dtClosedOn == DateTime.MinValue)
                            {
                                detLedgerDateClosed.Text = string.Empty;
                            }
                            else
                            {
                                detLedgerDateClosed.DateTime = dtClosedOn;
                            }
                        }
                    }

                    //On 04/07/2023, Lock to remove ledger closed date if it is closed by HO
                    detLedgerDateClosed.Enabled = (closedby == 0);
                    detLedgerDateClosed.ToolTip = (closedby == 1 ? msg : "");

                    if (closedby == 0)
                    {
                        detLedgerDateClosed.Select();
                        detLedgerDateClosed.Focus();
                    }
                    else
                    {
                        pnlLedgerDetails.Visible = false;
                        this.ShowMessageBox(msg);
                    }
                }
            }
            else
            {
                pnlLedgerDetails.Visible = false;
                MessageRender.ShowMessage("Head Office Ledger can't be closed");
            }
        }

        #endregion

        private void frmLedgerView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void frmLedgerView_EnterClicked(object sender, EventArgs e)
        {
            ShowEditLedgerForm();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ResultArgs resultArgs = new ResultArgs();
            if (pnlLedgerDetails.Visible)
            {
                if (gvLedgerView.DataSource != null && gvLedgerView.RowCount > 0)
                {
                    if (gvLedgerView.GetRowCellValue(gvLedgerView.FocusedRowHandle, colLedgerName.FieldName) != null)
                    {
                        if (this.ShowConfirmationMessage("Are you sure to update Ledger closed Date ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            //gvLedgerView.GetRowCellValue(gvLedgerView.FocusedRowHandle, colStartedOn.FieldName).ToString();
                            string startedon = AppSetting.FirstFYDateFrom.ToShortDateString();
                            string closedon = detLedgerDateClosed.DateTime.ToShortDateString();
                            if (!String.IsNullOrEmpty(startedon))
                            {
                                using (LedgerSystem ledgersystem = new LedgerSystem())
                                {
                                    ledgersystem.LedgerClosedBy = detLedgerDateClosed.Enabled ? 0 : 1; //0 - Closed By BO, 1- Closed by HO
                                    resultArgs = ledgersystem.UpdateLedgerClosedDate(LedgerId, detLedgerDateClosed.DateTime);
                                }
                            }
                        }
                        else
                        {
                            resultArgs.Success = true;
                        }
                    }
                }
            }

            if (resultArgs != null && !resultArgs.Success)
            {
                this.ShowMessageBoxWarning(resultArgs.Message);
                detLedgerDateClosed.Select();
                detLedgerDateClosed.Focus();
            }
            else
            {
                pnlLedgerDetails.Visible = false;
                FetchLedgerDetails();
                gvLedgerView.FocusedRowHandle = RowIndex;


            }
        }

        private void lblProjectClosedDate_Click(object sender, EventArgs e)
        {

        }

    }
}