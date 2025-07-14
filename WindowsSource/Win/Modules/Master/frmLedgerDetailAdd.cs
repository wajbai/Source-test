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
using Bosco.Model.TDS;
using Bosco.Model.UIModel.Master;

using DevExpress.XtraEditors.Controls;
using AcMEDSync.Model;
using Bosco.Model.Dsync;
using Bosco.Model.Transaction;

namespace ACPP.Modules.Master
{
    public partial class frmLedgerDetailAdd : frmFinanceBaseAdd
    {
        #region Variables
        ResultArgs resultArgs = null;
        public event EventHandler UpdateHeld;
        private int LedgerAccId = 0;
        DialogResult mappingDialogResult = DialogResult.Cancel;
        ledgerSubType ledType;
        int ProjectId = 0;
        private string NATURE_OF_PAYMENT = string.Empty;// "Nature of Payments";
        private string DEDUCTEE_TYPE = string.Empty;// "Deductee Type";
        private bool IsSuntryDebtorsCreditor = false;
        private bool IsCashLedger = false;
        private DataTable dtAllProjectLedgerApplicable { get; set; }

        #endregion

        #region Properties

        private int ledgerInsertId = 0;
        private int LedgerInsertId
        {
            set { ledgerInsertId = value; }
            get { return ledgerInsertId; }
        }
        private int DeducteeTypeId { get; set; }
        private int CountryId { get; set; }
        private int NatureId { get; set; }
        private string NatureName { get; set; }
        private int PrevNatureId { get; set; }
        private int GroupId { get; set; }
        private DataTable dtLedgerGroups { get; set; }
        private DataTable dtBudgetGroups { get; set; }
        private DataTable dtBudgetSubGroups { get; set; }
        private DataTable dtLedgerProfile { get; set; }
        private TDSLedgerTypes tdsLedgersTypes { get; set; }
        private int CreditorsProfileId { get; set; }
        private int LedgerGroupId { get; set; }
        private int BankAccountId { get; set; }

        #endregion

        #region Constructor
        public frmLedgerDetailAdd()
        {
            InitializeComponent();

            //On 02/07/2019, Map HO ledger---------
            LoadHeadOfficeLedgers();
            //-------------------------------------

            //On 22/08/22024, To Show currency details
            lcgCurrencyDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                LoadCurrency();
            }
        }

        public frmLedgerDetailAdd(int LedgerId, ledgerSubType ledgerType, int ProjetId = 0, int BankAccountId = 0, bool isSuntryDebtorsCreditor = false, bool isCashLedger = false)
            : this()
        {
            ucAccountMapping1.ProjectId = ProjetId;
            ucAccountMapping1.Id = LedgerAccId = LedgerId;
            IsSuntryDebtorsCreditor = isSuntryDebtorsCreditor;
            IsCashLedger = isCashLedger;
            ledType = ledgerType;
            ProjectId = ProjetId;
            this.BankAccountId = BankAccountId;
            ucAccountMapping1.FormType = ledType != ledgerSubType.FD ? MapForm.Ledger : MapForm.FDLedger;
            LedgerAccessFlag(LedgerId);
            LoadBudgetGroup();
            LoadBudgetSubGroup();
            AssignLedgerDetails();
        }

        public frmLedgerDetailAdd(ledgerSubType ledgerType, int ProjetId = 0, bool isSuntryDebtorsCreditor = false, bool isCashLedger = false)
            : this()
        {
            ucAccountMapping1.ProjectId = ProjetId;
            IsSuntryDebtorsCreditor = isSuntryDebtorsCreditor;
            IsCashLedger = isCashLedger;
            ledType = ledgerType;
            ProjectId = ProjetId;

            ucAccountMapping1.FormType = ledType != ledgerSubType.FD ? MapForm.Ledger : MapForm.FDLedger;
        }
        #endregion

        #region User Rights
        private void ApplyRights()
        {
            bool createbranchrights = (CommonMethod.ApplyUserRights((int)Bank.CreateBank) != 0);
            //glkpState.Properties.Buttons[1].Visible = createbranchrights;  // No Index 1 available for state Property
            //glkpBranch.Properties.Buttons.Count

            //EditorButton cbonewbutton = new EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus);
            //EditorButton cbonewbutton1 = new EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo);

            //if (glkpBranch.Properties.Buttons.IndexOf(cbonewbutton) > 0)
            //{
            glkpBranch.Properties.Buttons[1].Visible = createbranchrights;
            // }
        }
        #endregion

        #region Methods

        /// <summary>
        /// To load the ledger Group
        /// </summary>
        /// <param name="InitialValue"></param>
        private void LoadLedgerGroup(bool InitialValue = true)
        {
            try
            {
                using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem())
                {
                    resultArgs = ledgerSystem.LoadLedgerGroupforLedgerLoodkup(ledType);

                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        dtLedgerGroups = resultArgs.DataSource.Table;
                        if (ledType == ledgerSubType.TDS)
                        {
                            DataView dvLedgers = resultArgs.DataSource.Table.DefaultView;
                            dvLedgers.RowFilter = "GROUP_ID IN(" + (int)TDSDefaultLedgers.DirectExpense + "," + (int)TDSDefaultLedgers.DutiesandTaxes + "," + (int)TDSDefaultLedgers.SunderyCreditors + ") OR NATURE_ID IN(" + (int)Natures.Expenses + ") OR NATURE_ID IN(" + (int)Natures.Expenses + ")";
                            this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpLedgerGroup, dvLedgers.ToTable(), ledgerSystem.AppSchema.LedgerGroup.LEDGER_GROUPColumn.ToString(), ledgerSystem.AppSchema.LedgerGroup.GROUP_IDColumn.ToString());
                            dvLedgers.RowFilter = "";
                        }
                        else if (IsSuntryDebtorsCreditor)
                        {
                            DataView dvLedgers = resultArgs.DataSource.Table.DefaultView;
                            dvLedgers.RowFilter = "GROUP_ID IN (" + (int)TDSDefaultLedgers.SunderyCreditors + "," + (int)TDSDefaultLedgers.SundryDebtors + ")";
                            this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpLedgerGroup, dvLedgers.ToTable(), ledgerSystem.AppSchema.LedgerGroup.LEDGER_GROUPColumn.ToString(), ledgerSystem.AppSchema.LedgerGroup.GROUP_IDColumn.ToString());
                            dvLedgers.RowFilter = "";
                        }
                        else if (IsCashLedger)
                        {
                            DataView dvLedgers = resultArgs.DataSource.Table.DefaultView;
                            dvLedgers.RowFilter = "GROUP_ID IN (" + (int)FixedLedgerGroup.Cash + ")";
                            this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpLedgerGroup, dvLedgers.ToTable(), ledgerSystem.AppSchema.LedgerGroup.LEDGER_GROUPColumn.ToString(), ledgerSystem.AppSchema.LedgerGroup.GROUP_IDColumn.ToString());
                            dvLedgers.RowFilter = "";

                            if (ledType.Equals(ledgerSubType.CA))
                            {
                                glkpLedgerGroup.EditValue = glkpLedgerGroup.Properties.GetKeyValue(0);
                            }
                        }
                        else
                        {
                            DataView dvLedgers = resultArgs.DataSource.Table.DefaultView;
                            //On 26/10/2024, If general mode Ledgers - To lock individual groups ( Bank Accounts Ledgers, FD Ledgers, Cash Ledgers, Sundry cred/deb Ledgers)
                            if (ledType.Equals(ledgerSubType.GN))
                            {
                                string filter = "GROUP_ID NOT IN (" + (int)FixedLedgerGroup.BankAccounts + "," + (int)FixedLedgerGroup.Cash + "," +
                                    (int)FixedLedgerGroup.FixedDeposit + "," + (int)TDSDefaultLedgers.SunderyCreditors + "," + (int)TDSDefaultLedgers.SundryDebtors + ")";
                                dvLedgers.RowFilter = filter;
                            }
                            this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpLedgerGroup, dvLedgers.ToTable(), ledgerSystem.AppSchema.LedgerGroup.LEDGER_GROUPColumn.ToString(), ledgerSystem.AppSchema.LedgerGroup.GROUP_IDColumn.ToString());
                            dvLedgers.RowFilter = "";

                            if (ledType.Equals(ledgerSubType.FD))
                            {
                                glkpLedgerGroup.EditValue = glkpLedgerGroup.Properties.GetKeyValue(0);
                            }
                        }
                        if (InitialValue)
                        {
                            //  lkpGroup.EditValue = lkpGroup.Properties.GetDataSourceValue(lkpGroup.Properties.ValueMember, 0);
                        }
                        //glkpLedgerGroup.Enabled = ledType == ledgerSubType.GN ? true : ledType == ledgerSubType.TDS ? true : false;
                    }
                }

            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message + System.Environment.NewLine + Ex.Source);
            }
            finally { }
        }

        /// <summary>
        /// To load the Budget Group
        /// </summary>
        /// <param name="InitialValue"></param>
        private void LoadBudgetGroup()
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    resultArgs = ledgerSystem.FetchBudgetGroupLookup();

                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        dtBudgetGroups = resultArgs.DataSource.Table;
                        this.UtilityMember.ComboSet.BindGridLookUpComboEmptyItem(glkpBudgetGroup, resultArgs.DataSource.Table, ledgerSystem.AppSchema.BudgetGroup.BUDGET_GROUPColumn.ToString(), ledgerSystem.AppSchema.BudgetGroup.BUDGET_GROUP_IDColumn.ToString(), true, "");
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message + System.Environment.NewLine + Ex.Source);
            }
            finally { }
        }

        /// <summary>
        /// To load the Budget Sub Group
        /// </summary>
        /// <param name="InitialValue"></param>
        private void LoadBudgetSubGroup()
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    resultArgs = ledgerSystem.FetchBudgetSubGroupLookup();

                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        dtBudgetSubGroups = resultArgs.DataSource.Table;
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpBudgetSubGroup, resultArgs.DataSource.Table, ledgerSystem.AppSchema.BudgetSubGroup.BUDGET_SUB_GROUPColumn.ToString(), ledgerSystem.AppSchema.BudgetSubGroup.BUDGET_SUB_GROUP_IDColumn.ToString());
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message + System.Environment.NewLine + Ex.Source);
            }
            finally { }
        }

        /// <summary>
        /// To assign the details in the clrts
        /// </summary>
        private void AssignLedgerDetails()
        {
            string msg = "As Ledger is closed by Head Office, you can't modify its closed date.";

            try
            {
                if (LedgerAccId > 0)
                {
                    using (LedgerSystem ledgerSystem = new LedgerSystem(LedgerAccId))
                    {
                        LoadLedgerGroup(true);
                        txtCode.Text = ledgerSystem.LedgerCode;
                        txtLedgerName.Text = ledgerSystem.LedgerName;
                        LedgerGroupId = ledgerSystem.GroupId;
                        PrevNatureId =  ledgerSystem.NatureId;
                        glkpLedgerGroup.EditValue = ledgerSystem.GroupId.ToString();
                        meNotes.Text = ledgerSystem.LedgerNotes;
                        glkpLedgerType.EditValue = (ledgerSystem.LedgerType == ledgerSubType.GN.ToString()) ? (int)LedgerType.General : (int)LedgerType.InKind;

                        // 21/11/2024
                        if (ledgerSystem.TypeId > 0)
                        {
                            LoadAccountType();
                            glkpNewAccountType.EditValue = (ledgerSystem.TypeId == 1) ? AccountType.Savings.ToString() : (ledgerSystem.TypeId == 2) ? (AccountType.Current.ToString()) : string.Empty;
                        }
                        else
                        {
                            LoadAccountType();
                        }


                        loadLedgerType(false);
                        if (ledgerSystem.IsCostCentre == (int)YesNo.No)
                            chkCostCentre.Checked = false;
                        else
                            chkCostCentre.Checked = true;
                        if (ledgerSystem.IsBankInterestLedger == (int)YesNo.No)
                            chkBankFDInsLedger.Checked = false;
                        else
                            chkBankFDInsLedger.Checked = true;

                        chkInkindLedger.Checked = ledgerSystem.IsInKindLedger == (int)YesNo.No ? false : true;   //0; // (int)YesNo.No ? false : true;

                        // checkEdit1= Asset Loss Ledger
                        // checkEdit2= Asset Gain Ledger

                        chkAssetLossLedger.Checked = ledgerSystem.IsAssetLossLedger == (int)YesNo.No ? false : true;
                        chkAssetGainLedger.Checked = ledgerSystem.IsAssetGainLedger == (int)YesNo.No ? false : true;
                        chkDisposalLedger.Checked = ledgerSystem.IsAssetDisposalLedger == (int)YesNo.No ? false : true;

                        chkDepriciationLedger.Checked = ledgerSystem.IsDepriciationLedger == (int)YesNo.No ? false : true;
                        chkSubsidyLedger.Checked = ledgerSystem.IsSubsidyLedger == (int)YesNo.No ? false : true;

                        //18/07/2022,
                        chkBankSBInterest.Checked = ledgerSystem.IsBankSBInterestLedger == (int)YesNo.No ? false : true;
                        chkBankCommissionLedger.Checked = ledgerSystem.IsBankCommissionLedger == (int)YesNo.No ? false : true;

                        //08/09/2022, for fd penalty ledgers
                        chkBankFDPenaltyLedger.Checked = ledgerSystem.IsBankFDPenaltyLedger == (int)YesNo.No ? false : true;


                        if (ledgerSystem.IsTDSLedger.Equals((int)YesNo.Yes))
                        {
                            chkIsTDSApplicable.Checked = true;
                            chkIsTDSApplicable.Visible = true;
                        }
                        else
                        {
                            chkIsTDSApplicable.Checked = false;
                        }
                        if (ledgerSystem.IsGSTLedger.Equals((int)YesNo.Yes))
                        {
                            chkIsGSTApplicable.Checked = true;
                            chkIsGSTApplicable.Visible = true;
                        }
                        else
                        {
                            chkIsGSTApplicable.Checked = false;
                        }
                        if (LedgerAccessRights == 1)
                        {
                            txtName.Enabled = false;
                        }
                        else
                        {
                            txtName.Enabled = true;
                        }

                        if (ledgerSystem.NatureofPaymentsId > 0)
                        {
                            FetchDefaultNatureofPayments();
                            glkpNop.EditValue = ledgerSystem.NatureofPaymentsId;
                        }
                        else if (ledgerSystem.DeducteeTypeId > 0)
                        {
                            DeducteeTypeId = ledgerSystem.DeducteeTypeId;
                            glkpNop.Text = this.GetMessage(MessageCatalog.Master.Ledger.DEDUCTEE_TYPE);
                            FetchDeducteeTypes();
                        }
                        else
                        {
                            FetchDefaultNatureofPayments();
                        }

                        if (ledgerSystem.GStsId > 0)
                        {
                            FetchGstList();
                            FetchGSTType();
                            glkpGSt.EditValue = ledgerSystem.GStsId;
                            glkpGSTType.EditValue = ledgerSystem.GStServiceType == 0 ? GSTType.Goods : GSTType.Services;
                        }
                        else
                        {
                            FetchGstList();
                            FetchGSTType();
                        }

                        txtName.Text = ledgerSystem.FavouringName;
                        meAddress.Text = ledgerSystem.Address;
                        txtEmailAddress.Text = ledgerSystem.Email;
                        txtMobileNumber.Text = ledgerSystem.MobileNumber;
                        txtPANNo.Text = ledgerSystem.PANNo;
                        txtPinCode.Text = ledgerSystem.PinCode;
                        txtGSTIN.Text = ledgerSystem.GSTNo;
                        glkpCountry.EditValue = ledgerSystem.Country;
                        CountryId = ledgerSystem.Country;
                        glkpState.EditValue = ledgerSystem.State;

                        //On 02/07/2019, to map HO ledger
                        glkpHOLedger.EditValue = ledgerSystem.HeadofficeLedgerId;

                        // budget group 
                        glkpBudgetGroup.EditValue = ledgerSystem.BudgetGroupId;

                        // budget sub group
                        glkpBudgetSubGroup.EditValue = ledgerSystem.BudgetGroupId != 2 ? ledgerSystem.BudgetSubGroupId : 0;

                        //19/10/2021, To update Ledger Closed Date
                        if (lcLedgerClosedDate.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                        {
                            //deLedgerDateClosed.Text = ledgerSystem.LedgerDateClosed != DateTime.MinValue ? ledgerSystem.LedgerDateClosed.ToShortDateString() : "";
                            if (ledgerSystem.LedgerDateClosed != DateTime.MinValue)
                            {
                                deLedgerDateClosed.DateTime = ledgerSystem.LedgerDateClosed;

                                //On 04/07/2023, Lock to remove ledger closed date if it is closed by HO
                                deLedgerDateClosed.Enabled = (ledgerSystem.LedgerClosedBy == 0);
                                deLedgerDateClosed.ToolTip = (ledgerSystem.LedgerClosedBy == 1 ? msg : "");
                                lcLedgerClosedDate.OptionsToolTip.ToolTip = deLedgerDateClosed.ToolTip;
                            }
                        }

                        if (glkpLedgerGroup.EditValue != null && !string.IsNullOrEmpty(glkpLedgerGroup.Text) && this.UtilityMember.NumberSet.ToInteger(glkpLedgerGroup.EditValue.ToString()).Equals((int)FixedLedgerGroup.BankAccounts))
                        {
                            ShowBankAccountDetails();
                            glkpBranch.EditValue = ledgerSystem.BankId;
                            txtAccountHolder.Text = ledgerSystem.AccountHolderName;
                            deDateOpened.DateTime = ledgerSystem.dtDateOpened;
                            //deDateClosed.Text =  ledgerSystem.dtDateClosed != DateTime.MinValue ? ledgerSystem.dtDateClosed.ToShortDateString() : "";
                            if (ledgerSystem.dtDateClosed != DateTime.MinValue)
                            {
                                deDateClosed.DateTime = ledgerSystem.dtDateClosed;// ledgerSystem.dtDateClosed != DateTime.MinValue ? ledgerSystem.dtDateClosed.ToShortDateString() : "";

                                //On 04/07/2023, Lock to remove ledger closed date if it is closed by HO
                                deDateClosed.Enabled = (ledgerSystem.LedgerClosedBy == 0);
                                deDateClosed.ToolTip = (ledgerSystem.LedgerClosedBy == 1 ? msg : "");
                                lblDateClosed.OptionsToolTip.ToolTip = deDateClosed.ToolTip;
                            }

                            meOperatedBy.Text = ledgerSystem.OperatedBy;
                            ShowCurrencyDetails();
                        }
                        else
                        {
                            ShowLedgerGroupDetails();
                        }

                        txtHSNCode.Text = txtSACCode.Text = string.Empty;
                        if (chkIsGSTApplicable.Checked)
                        {
                            if (lcHSNCode.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                            {
                                txtHSNCode.Text = ledgerSystem.GST_HSN_SAC_CODE;
                            }
                            else
                            {
                                txtSACCode.Text = ledgerSystem.GST_HSN_SAC_CODE;
                            }
                        }

                        glkpCurrencyCountry.EditValue = null;
                        if (this.AppSetting.AllowMultiCurrency == 1)
                        {
                            glkpCurrencyCountry.EditValue = ledgerSystem.LedgerCurrencyCountryId;

                            glkpCurrencyCountry.Enabled = true;
                            using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                            {
                                voucherTransaction.LedgerId = LedgerAccId;
                                resultArgs = voucherTransaction.MadeTransactionForLedger();
                                if (resultArgs.DataSource.Table.Rows.Count != 0)
                                {
                                    glkpCurrencyCountry.Enabled = false;
                                }
                            }

                            txtOPExchangeRate.Text = ledgerSystem.LedgerCurrencyOPExchangeRate.ToString();
                            txtOPExchangeRate.Tag = txtOPExchangeRate.Text;
                        }
                    }
                }
                FetchProjectLedgerApplicable();
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        /// <summary>
        /// On 23/09/2023, To get Project Ledger's Applicable 
        /// </summary>
        private void FetchProjectLedgerApplicable()
        {
            using (MappingSystem mappingSystem = new MappingSystem())
            {
                dtAllProjectLedgerApplicable = null;

                resultArgs = mappingSystem.FetchProjectLedgerApplicableByLedgerId(LedgerAccId);
                if (resultArgs != null && resultArgs.Success)
                {
                    dtAllProjectLedgerApplicable = resultArgs.DataSource.Table;
                    dtAllProjectLedgerApplicable.DefaultView.Sort = mappingSystem.AppSchema.Ledger.APPLICABLE_FROMColumn.ColumnName;
                    dtAllProjectLedgerApplicable = dtAllProjectLedgerApplicable.DefaultView.ToTable();
                }
            }
        }

        /// <summary>
        /// To Validate Ledger Details
        /// </summary>
        /// <returns></returns>
        private bool ValidateLedgerDetails()
        {
            bool IsGroudValid = true;
            if (glkpLedgerGroup.EditValue == null || string.IsNullOrEmpty(glkpLedgerGroup.Text) || this.UtilityMember.NumberSet.ToInteger(glkpLedgerGroup.EditValue.ToString()).Equals(0))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_GROUP_EMPTY));
                this.SetBorderColor(glkpLedgerGroup);
                IsGroudValid = false;
                glkpLedgerGroup.Focus();
                return IsGroudValid;
            }
            else if (glkpLedgerType.EditValue == null || string.IsNullOrEmpty(glkpLedgerType.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_TYPE_EMPTY));
                this.SetBorderColor(glkpLedgerType);
                IsGroudValid = false;
                glkpLedgerType.Focus();
                return IsGroudValid;
            }
            else if (string.IsNullOrEmpty(txtLedgerName.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_NAME_EMPTY));
                this.SetBorderColor(txtLedgerName);
                IsGroudValid = false;
                txtLedgerName.Focus();
                return IsGroudValid;
            }
            else if (!string.IsNullOrEmpty(txtEmailAddress.Text.Trim()))
            {
                if (!this.IsValidEmail(txtEmailAddress.Text.Trim()))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_EMAIL_INVALID));
                    IsGroudValid = false;
                    txtEmailAddress.Focus();
                    return IsGroudValid;
                }
            }
            else if (LedgerAccId > 0 && NatureId != PrevNatureId &&
                (AppSetting.NatuersInReceiptVoucherEntry != "0" || AppSetting.NatuersInPaymentVoucherEntry != "0")) //On 28/11/2024 - Check nature for general vouchers
            {
                using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                {
                    voucherTransaction.LedgerId = LedgerAccId;
                    resultArgs = voucherTransaction.MadeTransactionForLedger();
                    //if row count is zero than no transaction is made
                    if (resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.ShowMessageBox("As Ledger(s) Natures are fixed in Finance Setting for Vocuher Entry, You can't change Ledger's Nature when voucher(s) exists.");
                        IsGroudValid = false;
                    }
                }
            }
            else if (!ValidateTDSLedger())
            {
                IsGroudValid = false;
            }
            else if (!ValidateGSTLedgers())
            {
                IsGroudValid = false;
            }
            else if (!ValidateSundryCreditorsDebtorsLedger())
            {
                IsGroudValid = false;
            }
            else if (!ValidateFDLedgers())
            {
                IsGroudValid = false;
            }
            else
            {
                if (lcgHOLedger.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always &&
                     this.AppSetting.EnablePortal == 1 && this.AppSetting.MapHeadOfficeLedger == 1) //28/11/2019, to check mapping ledger is mandatory
                {
                    int HOMappedLedgerId = glkpHOLedger.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpHOLedger.EditValue.ToString()) : 0;
                    if (HOMappedLedgerId == 0)
                    {
                        this.ShowMessageBox("Map Branch Ledger with Head Office Ledger");
                        IsGroudValid = false;
                        glkpHOLedger.Focus();
                        return IsGroudValid;
                    }
                }
            }

            if (IsGroudValid && this.AppSetting.AllowMultiCurrency == 1 && lcgCurrencyDetails.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                int CountryCurrencyId = (glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString()));
                double exchangerate = UtilityMember.NumberSet.ToDouble(txtOPExchangeRate.Text);
                if (CountryCurrencyId == 0 || exchangerate == 0)
                {
                    MessageRender.ShowMessage("As Multi Currency option is enabled, Currecny details should be filled.");
                    IsGroudValid = false;
                }
            }

            if (IsGroudValid && lcLedgerClosedDate.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always && !string.IsNullOrEmpty(deLedgerDateClosed.Text.Trim()))
            {
                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    resultArgs = ledgersystem.CheckLedgerClosedDate(LedgerAccId, deLedgerDateClosed.DateTime);
                    if (!resultArgs.Success)
                    {
                        this.ShowMessageBox(resultArgs.Message);
                        IsGroudValid = false;
                    }
                }
            }

            if (glkpLedgerGroup.EditValue != null && this.UtilityMember.NumberSet.ToInteger(glkpLedgerGroup.EditValue.ToString()) > 0)
            {
                if (this.UtilityMember.NumberSet.ToInteger(glkpLedgerGroup.EditValue.ToString()).Equals((int)FixedLedgerGroup.BankAccounts))
                {
                    if (glkpBranch.EditValue == null || string.IsNullOrEmpty(glkpBranch.Text) || this.UtilityMember.NumberSet.ToInteger(glkpBranch.EditValue.ToString()).Equals(0))
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_BANK_EMPTY));
                        this.SetBorderColorForGridLookUpEdit(glkpBranch);
                        IsGroudValid = false;
                        glkpBranch.Focus();
                        return IsGroudValid;
                    }
                    else if (deDateOpened.DateTime == DateTime.MinValue || string.IsNullOrEmpty(deDateOpened.Text))
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_DATE_OPEN_EMPTY));
                        this.SetBorderColorForDateTimeEdit(deDateOpened);
                        IsGroudValid = false;
                        deDateOpened.Focus();
                        return IsGroudValid;
                    }
                    else if (!string.IsNullOrEmpty(deDateClosed.Text.Trim()) && !this.UtilityMember.DateSet.ValidateDate(deDateOpened.DateTime, deDateClosed.DateTime))
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Ledger.BANK_ACCOUNT_CLOSEDATE_VALIDATION));
                        IsGroudValid = false;
                        deDateOpened.Focus();
                        return IsGroudValid;
                    }
                    else if (dtAllProjectLedgerApplicable != null && dtAllProjectLedgerApplicable.Rows.Count > 0)
                    {
                        DataTable dtvalidation = dtAllProjectLedgerApplicable.DefaultView.ToTable();
                        string msg = "Ledger Applicable date range is mismatching with Ledger Opened/Ledger Closed Date";
                        //Check Opened Date
                        if (deDateOpened.DateTime != DateTime.MinValue)
                        {
                            string fld = "(APPLICABLE_FROM <'" + deDateOpened.DateTime + "') OR (APPLICABLE_TO <> '" + DateTime.MinValue + "' AND APPLICABLE_TO <'" + deDateOpened.DateTime + "')";
                            dtvalidation.DefaultView.RowFilter = fld;
                            if (dtvalidation.DefaultView.Count > 0)
                            {
                                msg = "Ledger Applicable date range is mismatching with Ledger Opened Date";
                                IsGroudValid = false;
                            }
                        }
                        //Check Closed Date
                        if (IsGroudValid && deDateClosed.DateTime != DateTime.MinValue)
                        {
                            string fld = "(APPLICABLE_TO >'" + deDateClosed.DateTime + "') OR (APPLICABLE_TO <> '" + DateTime.MinValue + "' AND APPLICABLE_TO >'" + deDateClosed.DateTime + "')";
                            dtvalidation.DefaultView.RowFilter = fld;
                            if (dtvalidation.DefaultView.Count > 0)
                            {
                                msg = "Ledger Applicable date range is mismatching with Ledger Closed Date";
                                IsGroudValid = false;
                            }
                        }


                        if (!IsGroudValid)
                        {
                            this.ShowMessageBoxWarning(msg);
                            IsGroudValid = false;
                        }
                    }

                    // Validate while closing the Bank Accounts if there is Transaction Exists or not if exists make it false in order to do the Transaction (Chinna)
                    if (LedgerGroupId >= 0 && (deDateClosed.DateTime != DateTime.MinValue))
                    {
                        using (LedgerSystem ledgersystem = new LedgerSystem())
                        {
                            // resultArgs = ledgersystem.CheckTransactionExistsByDateClosed(LedgerGroupId, deDateClosed.DateTime);
                            //On 19/10/2021, TO have common method to to check ledger balance date
                            //resultArgs = ledgersystem.CheckTransactionExistsByDateClosed(LedgerAccId, deDateClosed.DateTime);
                            //resultArgs = ledgersystem.CheckTransactionExistsByDateClosed(LedgerId, detClosedDate.DateTime);
                            resultArgs = ledgersystem.CheckLedgerClosedDate(LedgerAccId, deDateClosed.DateTime);
                            if (!resultArgs.Success)
                            {
                                //this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Ledger.BANK_ACCOUNT_CLOSEDATE_TRANSACTION));
                                this.ShowMessageBox(resultArgs.Message);
                                IsGroudValid = false;
                            }

                            /*
                            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Ledger.BANK_ACCOUNT_CLOSEDATE_TRANSACTION));
                                IsGroudValid = false;
                            }
                            else
                            {
                                //On 30/06/2018, If bankaccount ledger has closing balance, alert message, we can't give clsoed date
                                using (BalanceSystem balancesystem = new BalanceSystem())
                                {
                                    string projectid = string.Empty;
                                    DataTable dtProjects = ucAccountMapping1.GetLedgerMappingDetails;
                                    foreach (DataRow dr in dtProjects.Rows)
                                    {
                                        projectid += dr[balancesystem.AppSchema.Project.PROJECT_IDColumn.ColumnName] + ",";
                                    }
                                    projectid = projectid.TrimEnd(',');

                                    if (!string.IsNullOrEmpty(projectid))
                                    {
                                        BalanceProperty ledgerclosingbalance = balancesystem.GetBalance(projectid, LedgerAccId, this.UtilityMember.DateSet.ToDate(deDateClosed.DateTime.ToShortDateString()), BalanceSystem.BalanceType.ClosingBalance);
                                        if (ledgerclosingbalance.Amount > 0)
                                        {
                                            this.ShowMessageBox("Bank Account ledger has closing balance, It can't be closed on " + this.UtilityMember.DateSet.ToDate(deDateClosed.DateTime.ToShortDateString()));
                                            deDateClosed.Select();
                                            deDateClosed.Focus();
                                            IsGroudValid = false;
                                        }
                                    }
                                }
                            }
                             */
                        }
                    }
                }
            }
            return IsGroudValid;
        }

        private bool ValidateTDSLedger()
        {
            bool isValid = true;
            int LedgerGroupId = 0;
            if (chkIsTDSApplicable.Checked)
            {
                LedgerGroupId = this.UtilityMember.NumberSet.ToInteger(glkpLedgerGroup.EditValue.ToString());
                if (LedgerGroupId > 0)
                {
                    if (LedgerGroupId == (int)TDSLedgerGroup.SundryCreditors && string.IsNullOrEmpty(glkpNop.Text))
                    {
                        isValid = false;
                        XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.Ledger.DEDUCTEE_TYPE_IS_REQUIRED), "AcME ERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (LedgerGroupId == (int)TDSLedgerGroup.ExpensesLedger && string.IsNullOrEmpty(glkpNop.Text))
                    {
                        isValid = false;
                        XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.Ledger.DEDUCTEE_TYPE_IS_REQUIRED), "AcME ERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    //else if (LedgerGroupId == (int)TDSLedgerGroup.DutiesAndTax && string.IsNullOrEmpty(glkpNop.Text))
                    //{
                    //    isValid = false;
                    //    XtraMessageBox.Show("Nature of Payment is required for the Duties and Tax Ledger, If TDS applicable.", "AcME ERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}
                }
            }
            return isValid;
        }

        private bool ValidateGSTLedgers()
        {
            bool isValid = true;
            int LedgerGroupId = 0;
            if (chkIsGSTApplicable.Checked)
            {
                LedgerGroupId = this.UtilityMember.NumberSet.ToInteger(glkpLedgerGroup.EditValue.ToString());
                if (LedgerGroupId > 0)
                {
                    if (string.IsNullOrEmpty(glkpGSt.Text))
                    {
                        isValid = false;
                        XtraMessageBox.Show("GST is required", "AcME ERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            return isValid;
        }

        private bool ValidateFDLedgers()
        {
            bool isValid = true;
            if (LedgerAccId > 0)
            {
                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    //For FD Interest 
                    if (!chkBankFDInsLedger.Checked)
                    {
                        ResultArgs result = ledgersystem.CheckFDInterestLedgerVoucherExists(LedgerAccId);
                        if ((result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0))
                        {
                            isValid = false;
                            this.ShowMessageBoxWarning("As Fixed Depoist Interest is available, you can't disable FD interest option");
                        }
                    }

                    //For FD Penalty
                    if (isValid && !chkBankFDPenaltyLedger.Checked)
                    {
                        ResultArgs result = ledgersystem.CheckFDPenaltyLedgerVoucherExists(LedgerAccId);
                        if ((result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0))
                        {
                            isValid = false;
                            this.ShowMessageBoxWarning("As Fixed Depoist Penalty is available, you can't disable FD Penalty option");
                        }
                    }
                }
            }

            return isValid;
        }

        private bool ValidateSundryCreditorsDebtorsLedger()
        {
            bool isValid = true;
            int LedgerId = 0;

            LedgerId = LedgerAccId;

            using (LedgerSystem Ledgersystem = new LedgerSystem())
            {
                resultArgs = Ledgersystem.FetchReferedByLedgerId(LedgerId);
                if (resultArgs.Success && this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Data.ToString()) > 0)
                {
                    if (this.ShowConfirmationMessage("This Ledger is Booked in Journal Voucher if you change the Ledger Group Reference Number and Referered Transaction will be Cleared. Do you proceed?.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        resultArgs = Ledgersystem.DeleteLedgerReferenceDetails(LedgerId);

                        if (!resultArgs.Success)
                        {
                            isValid = false;
                        }
                    }
                    else
                    {
                        glkpLedgerGroup.Focus();
                        isValid = false;
                    }
                }
            }

            return isValid;
        }

        /// <summary>
        /// To set the form title in runtime
        /// </summary>

        private void SetTitle()
        {
            lcLedgerClosedDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (ledType == ledgerSubType.GN)
            {
                this.Text = LedgerAccId == 0 ? this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_ADD_CAPTION) : this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_EDIT_CAPTION);
                lcLedgerClosedDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else if (ledType == ledgerSubType.FD)
            {
                this.Text = LedgerAccId == 0 ? this.GetMessage(MessageCatalog.Master.FDLedger.FD_LEDGER_ADD) : this.GetMessage(MessageCatalog.Master.FDLedger.FD_LEDGER_EDIT);
            }
            lcgBankAccountDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        /// <summary>
        /// To clear the control Values
        /// </summary>
        private void ClearControls()
        {
            foreach (Control ctrol in this.Controls[0].Controls)
            {
                if (ctrol is TextEdit)
                {
                    ctrol.Text = string.Empty;
                }
                else if (ctrol is GridLookUpEdit)
                {
                    ctrol.Text = string.Empty;
                }
                else if (ctrol is CheckEdit)
                {
                    chkBankFDInsLedger.Checked = chkCostCentre.Checked = chkIsTDSApplicable.Checked = chkIsGSTApplicable.Checked = false;
                    chkAssetLossLedger.Checked = chkAssetGainLedger.Checked = chkDepriciationLedger.Checked = false;
                    chkSubsidyLedger.Checked = false;
                    chkBankSBInterest.Checked = chkBankCommissionLedger.Checked = false;
                }
            }
            deLedgerDateClosed.Enabled = deDateClosed.Enabled = true;

            glkpState.EditValue = null;
            if (ledType == ledgerSubType.BK || LedgerGroupId.Equals((int)FixedLedgerGroup.BankAccounts))
            {
                glkpLedgerGroup.EditValue = (int)FixedLedgerGroup.BankAccounts;
                glkpLedgerGroup.Enabled = false;
                txtCode.Select();
            }
            else if (ledType == ledgerSubType.CA || LedgerGroupId.Equals((int)FixedLedgerGroup.Cash))
            {
                glkpLedgerGroup.EditValue = (int)FixedLedgerGroup.Cash;
                glkpLedgerGroup.Enabled = false;
                txtCode.Select();
            }
            else
            {
                glkpLedgerGroup.Enabled = true;
            }
            txtHSNCode.Text = txtSACCode.Text = string.Empty;
            ShowHSNSAC_CodePanel();
            PrevNatureId = 0;
        }

        private void ShowHSNSAC_CodePanel()
        {
            lcHSNCode.Visibility = lcSACCode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (lciGST.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always && chkIsGSTApplicable.Checked)
            {

                if (glkpGSTType.EditValue != null && glkpGSTType.EditValue.ToString().Equals(GSTType.Goods.ToString()))
                {
                    lcHSNCode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    txtHSNCode.Select();
                    txtHSNCode.Focus();
                }
                else if (glkpGSTType.EditValue != null && glkpGSTType.EditValue.ToString().Equals(GSTType.Services.ToString()))
                {
                    lcSACCode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    txtSACCode.Select();
                    txtSACCode.Focus();
                }
            }
        }

        private void ShowCurrencyDetails()
        {
            int CountryId = (glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString()));
            lblCurrencyName.Text = string.Empty;
            lblCurrencySymbol.Text = string.Empty;
            //lblOpeningAvgExchangeRate.Text = "0.00";
            txtOPExchangeRate.Text = "1";
            try
            {
                if (CountryId != 0)
                {
                    using (CountrySystem countrySystem = new CountrySystem())
                    {
                        ResultArgs result = countrySystem.FetchCountryCurrencyExchangeRateByCountryDate(CountryId, AppSetting.FirstFYDateFrom.Date);
                        if (result.Success)
                        {
                            lblCurrencySymbol.Text = countrySystem.CurrencySymbol;
                            lblCurrencyName.Text = countrySystem.CurrencyName;
                            //lblOpeningAvgExchangeRate.Text = UtilityMember.NumberSet.ToNumber(countrySystem.ExchangeRate);
                            if (txtOPExchangeRate.Tag == null) txtOPExchangeRate.Text = UtilityMember.NumberSet.ToNumber(countrySystem.ExchangeRate);
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

        private void ClearLedgerInfoDetails()
        {
            txtName.Text = txtEmailAddress.Text = txtMobileNumber.Text = txtPANNo.Text = txtGSTIN.Text = txtPinCode.Text = meAddress.Text = string.Empty;
            glkpCountry.EditValue = glkpState.EditValue = null;
        }

        private void loadLedgerType(bool initialValue)
        {
            LedgerType ledgerType = new LedgerType();
            DataView dvReceiptType = this.UtilityMember.EnumSet.GetEnumDataSource(ledgerType, Sorting.Ascending);
            glkpLedgerType.Properties.DataSource = dvReceiptType.ToTable();
            glkpLedgerType.Properties.DisplayMember = "Name";
            glkpLedgerType.Properties.ValueMember = "Id";
            glkpLedgerType.EditValue = 0;

            glkpLedgerType.Properties.ImmediatePopup = true;
            glkpLedgerType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            glkpLedgerType.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;

        }

        private void LoadAccountType()
        {
            //AccountType accounttype = new AccountType();
            //DataView dvaccountType = this.UtilityMember.EnumSet.GetEnumDataSource(accounttype, Sorting.None);
            //glkpNewAccountType.Properties.DataSource = dvaccountType.ToTable();
            //glkpNewAccountType.Properties.DisplayMember = "Name";
            //glkpNewAccountType.Properties.ValueMember = "Id";
            ////glkpNewAccountType.EditValue = 0;

            //glkpNewAccountType.Properties.ImmediatePopup = true;
            //glkpNewAccountType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            //glkpNewAccountType.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;

            try
            {
                Bosco.Utility.CommonMemberSet.EnumSetMember enumSetMembers = new Bosco.Utility.CommonMemberSet.EnumSetMember();
                AccountType accounttype = new AccountType();
                this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpNewAccountType, enumSetMembers.GetEnumDataSource(accounttype, Sorting.None).ToTable(),
                    EnumColumns.Name.ToString(),
                    EnumColumns.Name.ToString());
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        private string GetLedgerType(int groupId)
        {
            string ledgerType = "";
            //if (groupId.Equals((int)FixedLedgerGroup.BankAccounts))
            //    ledgerType = ledgerSubType.BK.ToString();
            if (this.UtilityMember.NumberSet.ToInteger(glkpLedgerType.EditValue.ToString()) == (int)LedgerType.General)
                ledgerType = ledgerSubType.GN.ToString();
            else
                ledgerType = ledgerSubType.IK.ToString();
            return ledgerType;
        }

        private string GetSubType(int groupId)
        {
            string ledgerSubtype = "";
            if (groupId.Equals((int)FixedLedgerGroup.BankAccounts))
                ledgerSubtype = ledgerSubType.BK.ToString();
            else if (this.UtilityMember.NumberSet.ToInteger(glkpLedgerType.EditValue.ToString()) == (int)LedgerType.General)
                ledgerSubtype = (groupId == (int)FixedLedgerGroup.Cash) ? ledgerSubType.CA.ToString() : ledgerSubType.GN.ToString();
            else
                ledgerSubtype = ledgerSubType.IK.ToString();
            return ledgerSubtype;
        }

        private int GetSortOrder(int groupId)
        {
            int sortId = 0;
            if (this.UtilityMember.NumberSet.ToInteger(glkpLedgerType.EditValue.ToString()) == (int)LedgerType.General)
                sortId = (groupId == (int)FixedLedgerGroup.Cash) ? (int)LedgerSortOrder.Cash : (int)LedgerSortOrder.GN;
            else
                sortId = (int)LedgerSortOrder.IK;
            return sortId;
        }

        private bool EnableTDS()
        {
            bool isTDSEnable = false;
            NatureName = glkpLedgerGroup.Text;
            GroupId = glkpLedgerGroup.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpLedgerGroup.EditValue.ToString()) : 0;
            if (GroupId > 0 && dtLedgerGroups != null
                && dtLedgerGroups.Rows.Count > 0)
            {
                DataView dvLedgerGroup = dtLedgerGroups.DefaultView;
                dvLedgerGroup.RowFilter = "GROUP_ID =" + GroupId + "";
                if (dvLedgerGroup != null && dvLedgerGroup.Count > 0)
                {
                    NatureId = dvLedgerGroup.ToTable().Rows[0]["NATURE_ID"] != null ? this.UtilityMember.NumberSet.ToInteger(dvLedgerGroup.ToTable().Rows[0]["NATURE_ID"].ToString()) : 0;
                }
                if (NatureId.Equals((int)Natures.Expenses) || NatureName.Equals("Duties and Taxes") || NatureName.Equals("Sundry Creditors") || GroupId.Equals((int)TDSDefaultLedgers.DutiesandTaxes) || GroupId.Equals((int)TDSDefaultLedgers.SunderyCreditors))
                {
                    if (GroupId.Equals((int)TDSDefaultLedgers.SunderyCreditors))
                    {
                        lblNoP.Text = this.GetMessage(MessageCatalog.Master.Ledger.DEDUCTEE_TYPE);
                        FetchDeducteeTypes();
                    }
                    else
                    {
                        lblNoP.Text = this.GetMessage(MessageCatalog.Master.Ledger.NATURE_OF_PAYMENT);
                    }
                }
                isTDSEnable = true;
            }
            return isTDSEnable;
        }

        private void FetchDefaultNatureofPayments()
        {
            using (DeducteeTaxSystem deducteeSystem = new DeducteeTaxSystem())
            {
                colNOPCode.Visible = true;
                this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpNop, deducteeSystem.NOP(), deducteeSystem.AppSchema.DeducteeTypes.NAMEColumn.ColumnName, "NATURE_PAY_ID");
            }
        }


        /// <summary>
        /// On 02/07/2019, load head office ledgers
        /// </summary>
        private void LoadHeadOfficeLedgers()
        {
            try
            {
                if (AppSetting.EnablePortal == 1)
                {
                    bool IsBankInterestLedger = chkBankFDInsLedger.Checked;
                    using (ExportVoucherSystem exportVoucher = new ExportVoucherSystem())
                    {
                        resultArgs = exportVoucher.HeadOfficeLedgers();
                        if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            DataTable dtHoLedgers = resultArgs.DataSource.Table;
                            //On 28/11/2019, to skip HO FD ledger for generl ledger mapping
                            dtHoLedgers.DefaultView.RowFilter = "GROUP_ID <>" + (int)FixedLedgerGroup.FixedDeposit;
                            dtHoLedgers = dtHoLedgers.DefaultView.ToTable();

                            if (IsBankInterestLedger)
                            {
                                dtHoLedgers.DefaultView.RowFilter = "IS_BANK_INTEREST_LEDGER=1";
                                dtHoLedgers = dtHoLedgers.DefaultView.ToTable();
                            }

                            //this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpHOLedger, dtHoLedgers, "HEADOFFICELEDGER", "HEADOFFICE_LEDGER_ID");
                            this.UtilityMember.ComboSet.BindGridLookUpComboEmptyItem(glkpHOLedger, dtHoLedgers, "HEADOFFICELEDGER", "HEADOFFICE_LEDGER_ID", true, "");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
        }

        private void LoadCurrency()
        {
            try
            {
                using (CountrySystem countrySystem = new CountrySystem())
                {
                    resultArgs = countrySystem.FetchCountryCurrencyDetails(AppSetting.FirstFYDateFrom.Date);
                    if (resultArgs.Success)
                    {
                        DataTable dtCurrencies = resultArgs.DataSource.Table;
                        dtCurrencies.DefaultView.RowFilter = countrySystem.AppSchema.Country.EXCHANGE_RATEColumn.ColumnName + "> 0 ";
                        dtCurrencies = dtCurrencies.DefaultView.ToTable();

                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpCurrencyCountry, dtCurrencies,
                            countrySystem.AppSchema.Country.CURRENCYColumn.ToString(), countrySystem.AppSchema.Country.COUNTRY_IDColumn.ToString());

                        object findcountry = glkpCurrencyCountry.Properties.GetDisplayValueByKeyValue(this.AppSetting.Country);
                        if (findcountry != null)
                        {
                            glkpCurrencyCountry.EditValue = this.AppSetting.Country;
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

        private void FetchDeducteeTypes()
        {
            using (DeducteeTypeSystem deducteeSystem = new DeducteeTypeSystem())
            {
                resultArgs = deducteeSystem.FetchActiveDeductTypes();
                if (resultArgs.DataSource.Table.Columns[deducteeSystem.AppSchema.DeducteeTypes.DEDUCTEE_TYPE_IDColumn.ColumnName].ColumnName.Equals(deducteeSystem.AppSchema.DeducteeTypes.DEDUCTEE_TYPE_IDColumn.ColumnName) && resultArgs.DataSource.Table.Columns[deducteeSystem.AppSchema.DeducteeTypes.NAMEColumn.ColumnName].ColumnName.Equals(deducteeSystem.AppSchema.DeducteeTypes.NAMEColumn.ColumnName))
                {
                    resultArgs.DataSource.Table.Columns[deducteeSystem.AppSchema.DeducteeTypes.DEDUCTEE_TYPE_IDColumn.ColumnName].ColumnName = "NATURE_PAY_ID";
                }
                colNOPCode.Visible = false;
                this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpNop, resultArgs.DataSource.Table, deducteeSystem.AppSchema.DeducteeTypes.NAMEColumn.ColumnName, "NATURE_PAY_ID");
                if (LedgerAccId > 0)
                {
                    glkpNop.EditValue = DeducteeTypeId;
                }
            }
        }

        private void FetchGstList()
        {
            using (GSTClassSystem GstClass = new GSTClassSystem())
            {
                glkpGSt.EditValue = null;
                this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpGSt, GstClass.FetchGSTList(), "GST_NAME", "GST_Id");
            }
        }

        private void FetchGSTType()
        {
            try
            {
                GSTType gstTypes = new GSTType();
                DataView dvGSTTypes = this.UtilityMember.EnumSet.GetEnumDataSource(gstTypes, Sorting.None);
                this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpGSTType, dvGSTTypes.ToTable(), "Name", "Name");

                ShowHSNSAC_CodePanel();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
        private void EnableControls(bool isEnableControls, bool isEnableTabFoucs, bool isEnterMoveFocus)
        {
            txtName.Properties.ReadOnly = meAddress.Properties.ReadOnly = txtEmailAddress.Properties.ReadOnly =
                txtMobileNumber.Properties.ReadOnly = txtPANNo.Properties.ReadOnly = txtGSTIN.Properties.ReadOnly = txtPinCode.Properties.ReadOnly = glkpCountry.Properties.ReadOnly =
                glkpState.Properties.ReadOnly = isEnableControls;

            txtName.TabStop = meAddress.TabStop = txtEmailAddress.TabStop =
                txtMobileNumber.TabStop = txtPANNo.TabStop = txtGSTIN.TabStop = txtPinCode.TabStop = glkpCountry.TabStop =
                glkpState.TabStop = isEnableTabFoucs;

            txtName.EnterMoveNextControl = meAddress.EnterMoveNextControl = txtEmailAddress.EnterMoveNextControl =
                txtMobileNumber.EnterMoveNextControl = txtPANNo.EnterMoveNextControl = txtGSTIN.EnterMoveNextControl = txtPinCode.EnterMoveNextControl = glkpCountry.EnterMoveNextControl =
                glkpState.EnterMoveNextControl = isEnterMoveFocus;
        }

        private void LoadCountryDetails(bool initialValue)
        {
            try
            {
                using (CountrySystem countrySystem = new CountrySystem())
                {
                    resultArgs = countrySystem.FetchCountryListDetails();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpCountry, resultArgs.DataSource.Table, countrySystem.AppSchema.Country.COUNTRYColumn.ColumnName, countrySystem.AppSchema.Country.COUNTRY_IDColumn.ColumnName);
                        if (!initialValue)
                        {
                            glkpCountry.EditValue = CountryId;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void SetFocusGrid()
        {
            ucAccountMapping1.Focus();
            ucAccountMapping1.ucGridControl.Focus();
            ucAccountMapping1.ucGridControl.FocusedColumn = ucAccountMapping1.ucGridControl.Columns[0];
            ucAccountMapping1.ucGridControl.MoveFirst();
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
                    glkpBranch.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpBranch, resultArgs.DataSource.Table, bankSystem.AppSchema.Bank.BANKColumn.ColumnName, bankSystem.AppSchema.Bank.BANK_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        /// <summary>
        /// On 02/07/2019 To show option to Map with Head office Ledger
        /// </summary>
        private void ShowMapHOLedger()
        {
            Int32 ledgergroupid = glkpLedgerGroup.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkpLedgerGroup.EditValue.ToString()) : 0;
            lcgHOLedger.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (AppSetting.EnablePortal == 1)
            {
                if (ledgergroupid != (int)FixedLedgerGroup.Cash && ledgergroupid != (int)FixedLedgerGroup.BankAccounts && ledgergroupid != (int)FixedLedgerGroup.FixedDeposit)
                {
                    lcgHOLedger.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
            }
        }
        #endregion

        #region Events

        private void frmLedgerDetailAdd_Load(object sender, EventArgs e)
        {
            ucLedgerDetailCodes.Visible = false;

            NATURE_OF_PAYMENT = this.GetMessage(MessageCatalog.Master.Ledger.NATURE_OF_PAYMENT);
            DEDUCTEE_TYPE = this.GetMessage(MessageCatalog.Master.Ledger.DEDUCTEE_TYPE);
            SetTitle();

            // 21/11/2024, chinna

            if (LedgerAccId == 0)
                LoadAccountType();

            if (LedgerAccId == 0) { loadLedgerType(true); } else { loadLedgerType(false); }
            if (LedgerAccId == 0) { LoadLedgerGroup(true); } else { LoadLedgerGroup(false); }
            if (LedgerAccId == 0) { LoadCountryDetails(true); }
            else
            {
                LoadCountryDetails(false);
            }

            ucAccountMapping1.SelectFixedWidth = true;

            if (LedgerAccId == 0)
                LoadBudgetGroup();
            if (LedgerAccId == 0)
                LoadBudgetSubGroup();

            if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.TDSEnabled).Equals((int)YesNo.Yes))
            {
                lcTDSApplicable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                GroupId = glkpLedgerGroup.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpLedgerGroup.EditValue.ToString()) : 0;
                if (!GroupId.Equals(14) && LedgerAccId == 0)
                    //layoutControlItem3.Visibility = emptySpaceItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;          
                    // EnableControls(false, true, true);
                    if (LedgerAccId == 0)
                    {
                        FetchDefaultNatureofPayments();
                        FetchGstList();
                        FetchGSTType();
                    }
            }
            glkpLedgerGroup.Properties.NullText = glkpLedgerType.Properties.NullText = glkpNop.Properties.NullText = glkpUserCode.Properties.NullText =
                glkpCountry.Properties.NullText = glkpState.Properties.NullText = string.Empty;
            layoutControlItem7.Text = "Email";
            layoutControlItem9.Text = "Contact No";
            txtEmailAddress.CausesValidation = false;

            ucAccountMapping1.ucGridControl.Columns[2].OptionsColumn.AllowFocus = false;
            ucAccountMapping1.ucGridControl.Columns[7].OptionsColumn.AllowFocus = false;

            if (LedgerGroupId == 0)
            {
                chkIsTDSApplicable.Enabled = true;
                chkIsGSTApplicable.Enabled = true;
            }

            //emptySpaceItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            emptySpaceItem10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            if (LedgerGroupId.Equals((int)FixedLedgerGroup.BankAccounts))
            {
                ShowBankAccountDetails();
            }
            else
            {
                ShowLedgerGroupDetails();
            }
            if (ledType == ledgerSubType.BK || LedgerGroupId.Equals((int)FixedLedgerGroup.BankAccounts))
            {
                glkpLedgerGroup.EditValue = (int)FixedLedgerGroup.BankAccounts;
                glkpLedgerGroup.Enabled = false;
            }
            else if (ledType == ledgerSubType.CA || LedgerGroupId.Equals((int)FixedLedgerGroup.BankAccounts))
            {
                glkpLedgerGroup.EditValue = (int)FixedLedgerGroup.Cash;
                glkpLedgerGroup.Enabled = false;
            }
            else
            {
                glkpLedgerGroup.Enabled = true;
            }
            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                ApplyRights();
            }
            else
            {
                glkpBranch.Properties.Buttons[1].Visible = true;
            }

            //On 02/07/2019, Map HO ledger---------
            ShowMapHOLedger();
            //-------------------------------------
            ShowHSNSAC_CodePanel();

            //10/07/2024, If other than india country
            if (this.AppSetting.IsCountryOtherThanIndia)
            {
                //lcPanNo.Visibility = lcPanEmptySpace.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                lcLedgerDetail.HideItem(lcPanNo);
                lcLedgerDetail.HideItem(lcPanEmptySpace);
            }

            lcOpeningAvgExchangeRateCaption.Text = (lcOpeningAvgExchangeRateCaption.Text + " " + AppSetting.FirstFYDateFrom.ToShortDateString()) + " : ";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateLedgerDetails())
            {
                try
                {
                    using (LedgerSystem ledgerSystem = new LedgerSystem())
                    {
                        ledgerSystem.LedgerCode = txtCode.Text.Trim();  //txtCode.Text.Trim().ToUpper();
                        ledgerSystem.LedgerName = txtLedgerName.Text.Trim();
                        ledgerSystem.GroupId = this.UtilityMember.NumberSet.ToInteger(glkpLedgerGroup.EditValue.ToString());
                        ledgerSystem.IsCostCentre = (chkCostCentre.Checked) ? (int)YesNo.Yes : (int)YesNo.No;
                        ledgerSystem.IsBankInterestLedger = (chkBankFDInsLedger.Checked) ? (int)YesNo.Yes : (int)YesNo.No;

                        // checkEdit1= Asset Loss Ledger
                        // checkEdit2= Asset Gain Ledger
                        ledgerSystem.IsAssetGainLedger = (chkAssetGainLedger.Checked) ? (int)YesNo.Yes : (int)YesNo.No;
                        ledgerSystem.IsAssetLossLedger = (chkAssetLossLedger.Checked) ? (int)YesNo.Yes : (int)YesNo.No;
                        ledgerSystem.IsAssetDisposalLedger = (chkDisposalLedger.Checked) ? (int)YesNo.Yes : (int)YesNo.No;

                        // Added by Praveen for Asset starts
                        ledgerSystem.IsInKindLedger = (chkInkindLedger.Checked) ? (int)YesNo.Yes : (int)YesNo.No; //(int)YesNo.No;
                        ledgerSystem.IsDepriciationLedger = (chkDepriciationLedger.Checked) ? (int)YesNo.Yes : (int)YesNo.No;
                        ledgerSystem.IsSubsidyLedger = (chkSubsidyLedger.Checked) ? (int)YesNo.Yes : (int)YesNo.No;
                        // Added by Praveen for Asset ends
                        ledgerSystem.LedgerType = GetLedgerType(ledgerSystem.GroupId);// CA-Cash Leger, GN -General Ledger
                        //ledgerSystem.LedgerSubType = ledType == ledgerSubType.FD ? LedgerTypes.FD.ToString() : GetSubType(ledgerSystem.GroupId);
                        ledgerSystem.LedgerSubType = ledgerSystem.GroupId.Equals((int)FixedLedgerGroup.FixedDeposit) ? LedgerTypes.FD.ToString() : GetSubType(ledgerSystem.GroupId);
                        ledgerSystem.LedgerId = (LedgerAccId == (int)AddNewRow.NewRow) ? (int)AddNewRow.NewRow : LedgerAccId;
                        ledgerSystem.LedgerNotes = meNotes.Text.Trim();
                        ledgerSystem.SortId = ledType == ledgerSubType.FD ? (int)LedgerSortOrder.FD : GetSortOrder(ledgerSystem.GroupId);// (int)LedgerSortOrder.Bank;
                        ledgerSystem.FDType = ledType == ledgerSubType.FD ? ledgerSubType.FD.ToString() : ledgerSubType.GN.ToString();
                        ledgerSystem.ledgerTypes = ledType;
                        ledgerSystem.AccountTypeId = (ledgerSystem.GroupId.Equals((int)FixedLedgerGroup.BankAccounts)) ? 1 : 0;



                        ledgerSystem.IsTDSLedger = chkIsTDSApplicable.Checked ? 1 : 0;
                        ledgerSystem.IsGSTLedger = chkIsGSTApplicable.Checked ? 1 : 0;
                        ledgerSystem.FavouringName = txtName.Text;
                        ledgerSystem.Address = meAddress.Text;
                        ledgerSystem.Email = txtEmailAddress.Text;
                        ledgerSystem.PANNo = txtPANNo.Text;
                        ledgerSystem.PinCode = txtPinCode.Text;
                        ledgerSystem.MobileNumber = txtMobileNumber.Text;
                        ledgerSystem.NatureofPaymentsId = glkpNop.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpNop.EditValue.ToString()) : 0;

                        //On 18/07/2022, Bank SB and Commission Ledgers
                        ledgerSystem.IsBankSBInterestLedger = (chkBankSBInterest.Checked) ? (int)YesNo.Yes : (int)YesNo.No;
                        ledgerSystem.IsBankCommissionLedger = (chkBankCommissionLedger.Checked) ? (int)YesNo.Yes : (int)YesNo.No;

                        //08/09/2022, for FD Penalty Ledgers--------------------------------------------
                        ledgerSystem.IsBankFDPenaltyLedger = (chkBankFDPenaltyLedger.Checked) ? (int)YesNo.Yes : (int)YesNo.No;
                        //-------------------------------------------------------------------------------

                        //On 19/10/2021 ------------------ Ledger Closed Date
                        if (!string.IsNullOrEmpty(deLedgerDateClosed.Text.Trim()))
                        {
                            ledgerSystem.LedgerDateClosed = deLedgerDateClosed.DateTime;
                        }
                        //--------------------------------

                        //On 02/07/2019, Map Ledger Office Ledger -------------------------------------------------------------------------------------------------------
                        ledgerSystem.HeadofficeLedgerId = 0;
                        if (AppSetting.EnablePortal == 1)
                        {
                            if (ledgerSystem.GroupId != (int)FixedLedgerGroup.Cash &&
                                ledgerSystem.GroupId != (int)FixedLedgerGroup.BankAccounts && ledgerSystem.GroupId != (int)FixedLedgerGroup.FixedDeposit)
                            {
                                ledgerSystem.HeadofficeLedgerId = glkpHOLedger.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpHOLedger.EditValue.ToString()) : 0;
                            }
                        }
                        //------------------------------------------------------------------------------------------------------------------------------------------------

                        if (glkpGSTType.EditValue != null)
                        {
                            ledgerSystem.GStServiceType = (glkpGSTType.EditValue.ToString().Equals(GSTType.Goods.ToString()) ? (int)GSTType.Goods : (int)GSTType.Services);
                        }
                        ledgerSystem.GSTNo = txtGSTIN.Text;
                        if (glkpGSt.EditValue != null)
                        {
                            ledgerSystem.GStsId = glkpGSt.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpGSt.EditValue.ToString()) : 0;
                        }
                        ledgerSystem.State = glkpState.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpState.EditValue.ToString()) : 0;
                        ledgerSystem.Country = glkpCountry.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpCountry.EditValue.ToString()) : 0;
                        ledgerSystem.tdsLedgerTypes = this.UtilityMember.NumberSet.ToInteger(glkpLedgerGroup.EditValue.ToString()).Equals((int)TDSLedgerGroup.ExpensesLedger) || NatureId.Equals((int)Natures.Expenses) ? TDSLedgerTypes.DirectExpense :
                            this.UtilityMember.NumberSet.ToInteger(glkpLedgerGroup.EditValue.ToString()).Equals((int)TDSLedgerGroup.DutiesAndTax) ? TDSLedgerTypes.DutiesandTaxes : this.UtilityMember.NumberSet.ToInteger(glkpLedgerGroup.EditValue.ToString()).Equals((int)TDSLedgerGroup.SundryCreditors) ? TDSLedgerTypes.SunderyCreditors : TDSLedgerTypes.DirectExpense;
                        ledgerSystem.CreditorsProfileId = CreditorsProfileId;

                        ledgerSystem.BudgetGroupId = glkpBudgetGroup.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpBudgetGroup.EditValue.ToString()) : 0;

                        ledgerSystem.BudgetSubGroupId = glkpBudgetSubGroup.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpBudgetSubGroup.EditValue.ToString()) : 0;

                        if (ledgerSystem.GroupId.Equals((int)FixedLedgerGroup.BankAccounts))
                        {
                            ledgerSystem.BankId = glkpBranch.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpBranch.EditValue.ToString()) : 0;
                            ledgerSystem.AccountHolderName = txtAccountHolder.Text;
                            ledgerSystem.TypeId = glkpNewAccountType.EditValue != null ? glkpNewAccountType.EditValue.ToString().Equals(AccountType.Savings.ToString()) ? 1 : glkpNewAccountType.EditValue.ToString().Equals(AccountType.Current.ToString()) ? 2 : 0 : 0;
                            ledgerSystem.dtDateClosed = deDateClosed.DateTime;
                            ledgerSystem.dtDateOpened = deDateOpened.DateTime;
                            ledgerSystem.OperatedBy = meOperatedBy.Text;
                            ledgerSystem.BankAccountId = this.BankAccountId;

                            //On 19/10/2021 ------------------ Ledger Closed Date when update bank account closed date
                            ledgerSystem.LedgerDateClosed = deDateClosed.DateTime;
                            ledgerSystem.LedgerClosedBy = deLedgerDateClosed.Enabled ? 0 : 1; //0 - Closed By BO, 1- Closed by HO
                            //-----------------------------------------------------------------------------------------
                        }

                        //On 02/11/2022 GST HSN/SAC Code
                        ledgerSystem.GST_HSN_SAC_CODE = string.Empty;
                        if (chkIsGSTApplicable.Checked)
                        {
                            ledgerSystem.GST_HSN_SAC_CODE = (lcHSNCode.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always) ? txtHSNCode.Text : txtSACCode.Text;
                        }

                        //On 22/08/2024, To get Currency details
                        ledgerSystem.LedgerCurrencyCountryId = 0;
                        ledgerSystem.LedgerCurrencyOPExchangeRate = 0;
                        if (this.AppSetting.AllowMultiCurrency == 1 && this.UtilityMember.NumberSet.ToInteger(glkpLedgerGroup.EditValue.ToString()) == (int)FixedLedgerGroup.Cash ||
                            this.UtilityMember.NumberSet.ToInteger(glkpLedgerGroup.EditValue.ToString()) == (int)FixedLedgerGroup.BankAccounts)
                        {
                            ledgerSystem.LedgerCurrencyCountryId = glkpCurrencyCountry.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString()) : 0;
                            ledgerSystem.LedgerCurrencyOPExchangeRate = UtilityMember.NumberSet.ToDouble(txtOPExchangeRate.Text);
                        }

                        if (!string.IsNullOrEmpty(this.AppSetting.BookBeginFrom))
                        {

                            ledgerSystem.dtMappingLedgers = ucAccountMapping1.GetLedgerMappingDetails;
                            ledgerSystem.dtProjectAmountMadeZero = ucAccountMapping1.GetProjectAmountMadeZero;
                            ledgerSystem.MapLedgerId = ledgerSystem.LedgerId;
                            ledgerSystem.FDLeger = false;
                            ledgerSystem.isTDSApplicable = chkIsTDSApplicable.Checked;
                            ledgerSystem.isGSTApplicable = chkIsGSTApplicable.Checked;
                            ledgerSystem.dtAllProjectLedgerApplicable = dtAllProjectLedgerApplicable;
                            if (ledgerSystem.GroupId.Equals((int)FixedLedgerGroup.BankAccounts))
                            {
                                resultArgs = ledgerSystem.SaveBankLedger(false);
                            }
                            else
                            {
                                resultArgs = ledgerSystem.SaveLedger();
                            }
                            if (resultArgs != null && resultArgs.Success)
                            {
                                this.ReturnValue = resultArgs.RowUniqueId;
                                this.ReturnDialog = System.Windows.Forms.DialogResult.OK;

                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                                LedgerInsertId = this.UtilityMember.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                if (LedgerAccId == 0)
                                {
                                    ucAccountMapping1.GridClear = true;

                                    ClearControls();
                                    FetchDefaultNatureofPayments();
                                    FetchGstList();
                                    FetchGSTType();
                                    LoadLedgerGroup();
                                    glkpLedgerGroup.Focus();
                                    glkpLedgerType.EditValue = 0;
                                    glkpNewAccountType.EditValue = 0;
                                    glkpHOLedger.EditValue = 0;
                                }
                                if (UpdateHeld != null)
                                    UpdateHeld(this, e);
                            }
                            FetchProjectLedgerApplicable();
                        }
                        else
                            ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.BOOK_BEGINNING_DATE_EMPTY));
                    }
                }
                catch (Exception Ex)
                {
                    MessageRender.ShowMessage(Ex.Message + System.Environment.NewLine + Ex.Source);
                }
                finally { }
            }
        }

        private void chkCostCentre_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit chked = (CheckEdit)sender;
            if (!chked.Checked)
            {
                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    resultArgs = ledgersystem.MadeTransactionByLedger(LedgerAccId.ToString());
                    //if row count is more zero than  transaction is made
                    if (resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        chked.Checked = true;
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_TRANSACTION_MADE));
                    }
                }
            }
        }

        private void glkpLedgerGroup_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpLedgerGroup);
        }

        private void glkpLedgerType_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpLedgerType);
        }

        private void txtLedgerName_Leave(object sender, EventArgs e)
        {
            int GroupId = glkpLedgerGroup.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpLedgerGroup.EditValue.ToString()) : 0;
            if (GroupId.Equals(14))
            {
                EnableControls(true, false, true);
                SetFocusGrid();
            }
            this.SetBorderColor(txtLedgerName);
        }

        private void chkBankInsLedger_Leave(object sender, EventArgs e)
        {
            //GetCheckCountForIncome(chkBankInsLedger);
            //if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.TDSEnabled).Equals(AdminUserId.UserId))
            //{
            //    chkIsTDSApplicable.Select();
            //    chkIsTDSApplicable.Focus();
            //}
            //else
            //{
            //    SetFocusGrid();
            //}
        }

        private void glkpCountry_EditValueChanged(object sender, EventArgs e)
        {
            using (StateSystem stateSystem = new StateSystem())
            {
                if (glkpCountry.EditValue != null)
                {
                    stateSystem.CountryId = glkpCountry.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpCountry.EditValue.ToString()) : 0;
                    if (stateSystem.CountryId > 0)
                    {
                        resultArgs = stateSystem.FetchStateListDetails();
                        if (resultArgs.Success)
                        {
                            this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpState, resultArgs.DataSource.Table, stateSystem.AppSchema.State.STATE_NAMEColumn.ColumnName, stateSystem.AppSchema.State.STATE_IDColumn.ColumnName);
                            // glkpState.EditValue = glkpState.Properties.GetKeyValue(0);
                            if (LedgerAccId > 0)
                            {
                                glkpCountry.EditValue = CountryId;
                            }
                        }
                    }
                }
            }
        }

        private void ucAccountMapping1_ProcessGridKey(object sender, EventArgs e)
        {
            if (ucAccountMapping1.ucGridControl.IsLastRow && ucAccountMapping1.ucGridControl.FocusedColumn == ucAccountMapping1.ucGridControl.Columns[4])
            {
                meNotes.Select();
                meNotes.Focus();
            }
        }

        private void txtPinCode_Leave(object sender, EventArgs e)
        {
            SetFocusGrid();
        }

        private void chkIsTDSApplicable_Leave(object sender, EventArgs e)
        {
            if (chkIsTDSApplicable.Checked)
            {
                if (NatureId.Equals((int)Natures.Assert) || NatureId.Equals((int)Natures.Libilities))
                {
                    lcgTDSInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    glkpNop.Focus();
                    EnableControls(false, true, true);
                }
            }
            else
            {
                if (NatureId.Equals((int)Natures.Assert) || NatureId.Equals((int)Natures.Libilities))
                {
                    lcgTDSInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    EnableControls(false, false, true);
                    SetFocusGrid();
                }
            }
        }

        private void glkpLedgerGroup_EditValueChanged(object sender, EventArgs e)
        {
            ucAccountMapping1.ShowApplicableOption = false;
            if (glkpLedgerGroup.EditValue != null)
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    mappingSystem.LedgerGroupId = glkpLedgerGroup.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkpLedgerGroup.EditValue.ToString()) : 0;
                    mappingSystem.LedgerId = LedgerAccId;
                    resultArgs = mappingSystem.LoadProjectMappingGrid();
                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        DataTable dtProject = resultArgs.DataSource.Table;
                        dtProject.Columns.Add("SELECT", typeof(Int32));
                        foreach (DataRow dr in dtProject.Rows)
                        {
                            dr["SELECT"] = dr["SELECT_TEMP"];
                        }

                        if (ProjectId != 0)
                        {
                            foreach (DataRow Irow in dtProject.Rows)
                            {
                                if (Irow["PROJECT_ID"].ToString() == ProjectId.ToString())
                                {
                                    Irow["SELECT"] = 1;
                                }
                            }
                        }

                        DataView dv = new DataView(dtProject);
                        dv.Sort = "SELECT" + " DESC";
                        dtProject = dv.ToTable();
                        ucAccountMapping1.GetMappingDetails = dtProject;
                    }
                    if (!mappingSystem.LedgerGroupId.Equals((int)FixedLedgerGroup.BankAccounts) &&
                        !mappingSystem.LedgerGroupId.Equals((int)FixedLedgerGroup.Cash))
                    {
                        ShowLedgerGroupDetails();
                        if (dtLedgerGroups != null && dtLedgerGroups.Rows.Count > 0)
                        {
                            DataView dvLedgerGroup = dtLedgerGroups.DefaultView;
                            dvLedgerGroup.RowFilter = "GROUP_ID=" + mappingSystem.LedgerGroupId + "";
                            if (dvLedgerGroup != null && dvLedgerGroup.Count > 0)
                            {
                                NatureId = this.UtilityMember.NumberSet.ToInteger(dvLedgerGroup.ToTable().Rows[0]["NATURE_ID"].ToString());
                                if (NatureId.Equals((int)Natures.Expenses) || NatureId.Equals((int)Natures.Income))
                                {
                                    if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.TDSEnabled).Equals((int)YesNo.Yes))
                                    {
                                        lcTDSApplicable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                    }
                                    FetchDefaultNatureofPayments();
                                    FetchGstList();
                                    FetchGSTType();
                                    EnableControls(true, false, false);
                                    emptySpaceItem1.Visibility = layoutControlItem1.Visibility =
                                        lcBankFDInsLedger.Visibility = lcBankSBInterest.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                    ClearLedgerInfoDetails();
                                }
                                else if (NatureId.Equals((int)Natures.Libilities) || (NatureId.Equals((int)Natures.Assert) || mappingSystem.LedgerGroupId.Equals((int)TDSDefaultLedgers.SunderyCreditors) || mappingSystem.LedgerGroupId.Equals((int)TDSDefaultLedgers.DutiesandTaxes)))//NatureId.Equals((int)Natures.Assert) ||
                                {
                                    chkIsTDSApplicable.Visible = true;
                                    EnableControls(false, true, true);
                                    emptySpaceItem1.Visibility = layoutControlItem1.Visibility =
                                        lcBankFDInsLedger.Visibility = lcBankSBInterest.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                    if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.TDSEnabled).Equals((int)YesNo.Yes))
                                    {
                                        lcTDSApplicable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                    }
                                    if (mappingSystem.LedgerGroupId.Equals(14))
                                    {
                                        layoutControlItem1.Visibility =
                                           lcBankFDInsLedger.Visibility = lcBankSBInterest.Visibility = lcTDSApplicable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                        EnableControls(true, true, true);
                                    }
                                }
                                else if (NatureId.Equals((int)Natures.Income) || (NatureId.Equals((int)Natures.Expenses) || (NatureId.Equals((int)Natures.Assert) || (NatureId.Equals((int)Natures.Libilities)))))
                                {
                                    chkIsGSTApplicable.Visible = true;
                                    EnableControls(false, true, true);
                                    emptySpaceItem1.Visibility = layoutControlItem1.Visibility =
                                        lcBankFDInsLedger.Visibility = lcBankSBInterest.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                    if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableGST).Equals((int)YesNo.Yes))
                                    {
                                        layoutControlItem20.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                    }
                                    else
                                    {
                                        layoutControlItem20.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    }
                                }
                                else if (mappingSystem.LedgerGroupId.Equals(14))
                                {
                                    layoutControlItem1.Visibility =
                                        lcBankFDInsLedger.Visibility = lcBankSBInterest.Visibility = lcTDSApplicable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    emptySpaceItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                                    lcTDSApplicable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                }
                                else
                                {
                                    emptySpaceItem1.Visibility = layoutControlItem1.Visibility =
                                        lcBankFDInsLedger.Visibility = lcBankSBInterest.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                    if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.TDSEnabled).Equals((int)YesNo.Yes))
                                    {
                                        lcTDSApplicable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                    }
                                    FetchDefaultNatureofPayments();
                                    FetchGstList();
                                    FetchGSTType();
                                }

                                lcgIncome.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                lcgExpense.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                                // Gain / Loss Ledger Options
                                if (NatureId.Equals((int)Natures.Income))
                                {
                                    chkAssetGainLedger.Enabled = true;
                                    chkAssetLossLedger.Enabled = false;
                                    chkAssetGainLedger.Checked = chkAssetLossLedger.Checked = false;
                                    lcgExpense.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    lcgIncome.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                }
                                else if (NatureId.Equals((int)Natures.Expenses))
                                {
                                    chkAssetGainLedger.Enabled = false;
                                    chkAssetLossLedger.Enabled = true;
                                    chkAssetGainLedger.Checked = chkAssetLossLedger.Checked = false;
                                    lcgIncome.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    lcgExpense.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                }
                                else
                                {
                                    chkAssetGainLedger.Enabled = chkAssetLossLedger.Enabled = false;
                                    chkAssetGainLedger.Checked = chkAssetLossLedger.Checked = false;
                                }

                                dvLedgerGroup.RowFilter = "";
                            }
                        }
                        if (mappingSystem.LedgerGroupId.Equals((int)TDSLedgerGroup.SundryCreditors))
                        {
                            lblNoP.Text = this.GetMessage(MessageCatalog.Master.Ledger.DEDUCTEE_TYPE);
                            FetchDeducteeTypes();
                        }
                        else
                        {
                            lblNoP.Text = this.GetMessage(MessageCatalog.Master.Ledger.NATURE_OF_PAYMENT);
                        }
                    }
                    else if (mappingSystem.LedgerGroupId.Equals((int)FixedLedgerGroup.Cash))
                    {
                        NatureId = (Int32)Natures.Assert;
                        ShowCashAccountDetails();
                    }
                    else
                    {
                        NatureId = (Int32)Natures.Assert;
                        ShowBankAccountDetails();
                    }
                }

                //On 02/07/2019, Map HO ledger---------
                ShowMapHOLedger();
                //-------------------------------------
                ShowHSNSAC_CodePanel();

                lcgCurrencyDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //On 22/08/2024, show currency details for cash and bank  ---------------
                if (this.AppSetting.AllowMultiCurrency == 1 && (this.UtilityMember.NumberSet.ToInteger(glkpLedgerGroup.EditValue.ToString()) == (int)FixedLedgerGroup.Cash ||
                                this.UtilityMember.NumberSet.ToInteger(glkpLedgerGroup.EditValue.ToString()) == (int)FixedLedgerGroup.BankAccounts))
                {
                    lcgCurrencyDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
                //-----------------------------------------------------------------------
            }
        }

        private int GetCheckCountForIncome()
        {
            int count = 0;
            if (chkBankFDInsLedger.Checked)
                count++;
            if (chkInkindLedger.Checked)
                count++;
            if (chkAssetGainLedger.Checked)
                count++;
            return count;
        }

        private int GetCheckCountForExpense()
        {
            int count = 0;
            if (chkDisposalLedger.Checked)
                count++;
            if (chkDepriciationLedger.Checked)
                count++;
            if (chkAssetLossLedger.Checked)
                count++;
            if (chkSubsidyLedger.Checked)
                count++;
            return count;
        }

        private void ShowLedgerGroupDetails()
        {
            lcgBankAccountDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcgLedgerProfile.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            lblLedgerName.Text = this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_NAME) + " <color=red>*";
            chkIsTDSApplicable.Enabled = true;
            if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.TDSEnabled).Equals((int)YesNo.Yes))
            {
                lcTDSApplicable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            emptySpaceItem1.Visibility = layoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            lcBankFDInsLedger.Visibility = lcBankSBInterest.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

            if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableGST).Equals((int)YesNo.Yes))
            {
                layoutControlItem20.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                layoutControlItem20.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            // if gst is enabled
            if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableGST).Equals((int)YesNo.Yes))
            {
                chkIsGSTApplicable.Visible = true;
            }
            else
            {
                chkIsGSTApplicable.Visible = false;
            }

            if (chkIsTDSApplicable.Checked)
            {
                lcgTDSInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                lcgTDSInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            //layoutControlItem20.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            if (chkIsGSTApplicable.Checked)
            {
                lcgGSt.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                lcgGSt.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private void ShowBankAccountDetails()
        {
            lcgLedgerProfile.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcgBankAccountDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            lblLedgerName.Text = this.GetMessage(MessageCatalog.Master.Ledger.ACCOUNT_NUMBER) + " <color=red>*";
            lcgTDSInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            chkIsTDSApplicable.Enabled = false;
            lcTDSApplicable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcBankFDInsLedger.Visibility = lcBankSBInterest.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // emptySpaceItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            emptySpaceItem10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            chkIsGSTApplicable.Enabled = false;
            layoutControlItem20.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            if (chkIsGSTApplicable.Checked)
            {
                lcgGSt.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                lcgGSt.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            LoadBank(false);
            ucAccountMapping1.ShowApplicableOption = true;
            ucAccountMapping1.ApplicableDateButtonClick -= new EventHandler(ucAccountMapping1_ApplicableDateButtonClick);
            ucAccountMapping1.ApplicableDateButtonClick += new EventHandler(ucAccountMapping1_ApplicableDateButtonClick);

            //On 14/10/2024, To lock few features for bank accounts
            lcgIncome.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcgExpense.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcLedgerClosedDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

        }

        private void ShowCashAccountDetails()
        {
            lcgLedgerProfile.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcgBankAccountDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //lblLedgerName.Text = this.GetMessage(MessageCatalog.Master.Ledger.ACCOUNT_NUMBER) + " <color=red>*";
            lblLedgerName.Text = this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_NAME) + " <color=red>*";
            lcgTDSInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            chkIsTDSApplicable.Enabled = false;
            lcTDSApplicable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcBankFDInsLedger.Visibility = lcBankSBInterest.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // emptySpaceItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            emptySpaceItem10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            chkIsGSTApplicable.Enabled = false;
            layoutControlItem20.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            if (chkIsGSTApplicable.Checked)
            {
                lcgGSt.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                lcgGSt.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            ucAccountMapping1.ShowApplicableOption = true;
            ucAccountMapping1.ApplicableDateButtonClick -= new EventHandler(ucAccountMapping1_ApplicableDateButtonClick);
            ucAccountMapping1.ApplicableDateButtonClick += new EventHandler(ucAccountMapping1_ApplicableDateButtonClick);

            //On 14/10/2024, To lock few features for bank accounts
            lcgIncome.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcgExpense.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcLedgerClosedDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            lcLedgerClosedDate.Padding = new DevExpress.XtraLayout.Utils.Padding(0, lcLedgerClosedDate.Padding.Right, lcLedgerClosedDate.Padding.Top, lcLedgerClosedDate.Padding.Bottom);
            lcLedgerClosedDate.Width = glkpBudgetGroup.Width;
        }

        private void ucAccountMapping1_ApplicableDateButtonClick(object sender, EventArgs e)
        {
            using (LedgerSystem ledgersystem = new LedgerSystem())
            {
                if (!string.IsNullOrEmpty(txtLedgerName.Text.Trim()) && deDateOpened.DateTime != DateTime.MinValue)
                {
                    if (ucAccountMapping1.ActiveMappId > 0 && !string.IsNullOrEmpty(ucAccountMapping1.ActiveMappName))
                    {
                        frmProjectLedgerApplicableDate frmProjectLedgerapplicabledate = new frmProjectLedgerApplicableDate(MapForm.Project, LedgerAccId, ucAccountMapping1.ActiveMappId,
                            ucAccountMapping1.ActiveMappName, txtLedgerName.Text, dtAllProjectLedgerApplicable, deDateOpened.DateTime.ToShortDateString(), deDateClosed.DateTime.ToShortDateString());
                        DialogResult dialogresult = frmProjectLedgerapplicabledate.ShowDialog();

                        if (dialogresult == System.Windows.Forms.DialogResult.OK)
                        {
                            if (frmProjectLedgerapplicabledate.ReturnValue != null)
                            {
                                dtAllProjectLedgerApplicable = frmProjectLedgerapplicabledate.ReturnValue as DataTable;
                            }
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox("Ledger Name/Opened date is empty");
                    txtLedgerName.Select();
                    txtLedgerName.Focus();
                }
            }
        }

        private void chkIsTDSApplicable_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsTDSApplicable.Checked)
            {
                lcgTDSInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                GroupId = glkpLedgerGroup.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpLedgerGroup.EditValue.ToString()) : 0;
                if (GroupId.Equals((int)TDSDefaultLedgers.SunderyCreditors))
                {
                    FetchDeducteeTypes();
                    glkpNop.Text = this.GetMessage(MessageCatalog.Master.Ledger.DEDUCTEE_TYPE);
                }
                else //if (GroupId.Equals((int)TDSDefaultLedgers.DirectExpense))
                {
                    glkpNop.Properties.ImmediatePopup = true;
                    glkpNop.Properties.PopupFilterMode = PopupFilterMode.Contains;
                    FetchDefaultNatureofPayments();
                    glkpNop.Text = this.GetMessage(MessageCatalog.Master.Ledger.NATURE_OF_PAYMENT);
                }
                glkpNop.Focus();
            }
            else
            {
                lcgTDSInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                SetFocusGrid();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            meAddress.Focus();
        }

        private void ucLedgerDetailCodes_Iconclicked(object sender, EventArgs e)
        {
            ucLedgerDetailCodes.FetchCodes(MapForm.Ledger);
        }

        private void glkpBranch_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpBranch);
        }

        private void deDateOpened_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForDateTimeEdit(deDateOpened);
        }
        #endregion

        private void glkpBranch_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                frmBankAdd frmAdd = new frmBankAdd();
                frmAdd.ShowDialog();
                if (frmAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                {
                    LoadBank();
                    if (frmAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmAdd.ReturnValue.ToString()) > 0)
                    {
                        glkpBranch.EditValue = this.UtilityMember.NumberSet.ToInteger(frmAdd.ReturnValue.ToString());
                    }
                }

                //frmBankAdd frmAdd = new frmBankAdd();
                //frmAdd.ShowDialog();
                //LoadBank();
            }
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit chked = (CheckEdit)sender; // Event for Asset Gain Ledger
            int count = GetCheckCountForIncome();
            if (count > 1)
            {
                chkAssetGainLedger.Checked = false;
                this.ShowMessageBox("Already one option is selected from this group.");
            }
            //if (!chked.Checked)
            //{
            //    using (LedgerSystem ledgersystem = new LedgerSystem())
            //    {
            //        resultArgs = ledgersystem.MadeTransactionByAssetGainLedger(LedgerAccId.ToString());
            //        //if row count is more zero than  transaction is made
            //        if (resultArgs.DataSource.Table.Rows.Count > 0)
            //        {
            //            chked.Checked = true;
            //            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_TRANSACTION_MADE));
            //        }
            //    }
            //}
            //else
            //{
            //    if (checkEdit1.Checked)
            //    {
            //        checkEdit2.Checked = false;
            //        this.ShowMessageBox("Already this ledger set as Loss Ledger.");
            //    }
            //}
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit chked = (CheckEdit)sender; // Event for Asset Loss Ledger
            int count = GetCheckCountForExpense();
            if (count > 1)
            {
                chkAssetLossLedger.Checked = false;
                this.ShowMessageBox("Already one option is selected from this group.");
            }
            //if (!chked.Checked)
            //{
            //    using (LedgerSystem ledgersystem = new LedgerSystem())
            //    {
            //        resultArgs = ledgersystem.MadeTransactionByAssetLossLedger(LedgerAccId.ToString());
            //        //if row count is more zero than  transaction is made
            //        if (resultArgs.DataSource.Table.Rows.Count > 0)
            //        {
            //            chked.Checked = true;
            //            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_TRANSACTION_MADE));
            //        }
            //    }
            //}
            //else
            //{
            //    if (checkEdit2.Checked)
            //    {
            //        checkEdit1.Checked = false;
            //        this.ShowMessageBox("Already this ledger set as Gain Ledger.");
            //    }
            //}
        }

        private void checkEdit2_Leave(object sender, EventArgs e)
        {

        }

        private void checkEdit1_Leave(object sender, EventArgs e)
        {

        }

        private void chkDisposalLedger_CheckedChanged(object sender, EventArgs e)
        {
            int count = GetCheckCountForExpense();
            if (count > 1)
            {
                chkDisposalLedger.Checked = false;
                this.ShowMessageBox("Already one option is selected from this group.");
            }

            //CheckEdit chked = (CheckEdit)sender;
            //if (!chked.Checked)
            //{
            //    //using (LedgerSystem ledgersystem = new LedgerSystem())
            //    //{
            //    //    resultArgs = ledgersystem.MadeTransactionByAssetDisposalLedger(LedgerAccId.ToString());
            //    //    //if row count is more zero than  transaction is made
            //    //    if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0)
            //    //    {
            //    //        chked.Checked = true;
            //    //        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_TRANSACTION_MADE));
            //    //    }
            //    //}
            //}
        }

        private void chkDisposalLedger_Leave(object sender, EventArgs e)
        {

        }

        private void chkBankInsLedger_CheckedChanged(object sender, EventArgs e)
        {
            int count = GetCheckCountForIncome();
            if (count > 1)
            {
                chkBankFDInsLedger.Checked = false;
                this.ShowMessageBox("Already one option is selected from this group.");
            }
            if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.TDSEnabled).Equals(AdminUserId.UserId))
            {
                chkIsTDSApplicable.Select();
                chkIsTDSApplicable.Focus();
            }
            else
            {
                SetFocusGrid();
            }
            //On 02/07/2019, Map HO ledger---------
            LoadHeadOfficeLedgers();
            //-------------------------------------
        }

        private void chkInkindLedger_CheckedChanged(object sender, EventArgs e)
        {
            int count = GetCheckCountForIncome();
            if (count > 1)
            {
                chkInkindLedger.Checked = false;
                this.ShowMessageBox("Already one option is selected from this group.");
            }
        }

        private void chkDepriciationLedger_CheckedChanged(object sender, EventArgs e)
        {
            int count = GetCheckCountForExpense();
            if (count > 1)
            {
                chkDepriciationLedger.Checked = false;
                this.ShowMessageBox("Already one option is selected from this group.");
            }
        }

        private void chkSubsidyLedger_CheckedChanged(object sender, EventArgs e)
        {
            int count = GetCheckCountForExpense();
            if (count > 1)
            {
                chkSubsidyLedger.Checked = false;
                this.ShowMessageBox("Already one option is selected from this group.");
            }
        }

        private void chkSubsidyLedger_Leave(object sender, EventArgs e)
        {

        }

        private void chkIsGSTApplicable_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsGSTApplicable.Checked)
            {
                lcgGSt.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                FetchGstList();
                FetchGSTType();
                GroupId = glkpLedgerGroup.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpLedgerGroup.EditValue.ToString()) : 0;
                glkpGSTType.Focus();
            }
            else
            {
                lcgGSt.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                SetFocusGrid();
            }
            ShowHSNSAC_CodePanel();
        }

        private void chkIsGSTApplicable_Leave(object sender, EventArgs e)
        {
            if (chkIsGSTApplicable.Checked)
            {
                lcgGSt.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                glkpGSTType.Focus();
                //EnableControls(false, true, true);
            }
            else
            {
                lcgGSt.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                SetFocusGrid();
            }
        }

        private void glkpGSTType_EditValueChanged(object sender, EventArgs e)
        {
            ShowHSNSAC_CodePanel();
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void glkpCurrencyCountry_EditValueChanged(object sender, EventArgs e)
        {
            //On 09/01/2025 - In edit mode, if they change country, we can show exchange rate
            if (this.LedgerAccId > 0) txtOPExchangeRate.Tag = null;
            ShowCurrencyDetails();
        }

        private void glkpBudgetGroup_EditValueChanged(object sender, EventArgs e)
        {
            int BudgetGroupId = glkpBudgetGroup.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpBudgetGroup.EditValue.ToString()) : 0;
            if (BudgetGroupId == 2)   // Non Recurring 
            {
                glkpBudgetSubGroup.EditValue = glkpBudgetSubGroup.Properties.GetKeyValue(0);
                glkpBudgetSubGroup.Enabled = false;
            }
            else
            {
                glkpBudgetSubGroup.EditValue = null;
                glkpBudgetSubGroup.Enabled = true;
            }
        }


    }
}