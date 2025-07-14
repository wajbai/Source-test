/// On 03/03/2021, In Budget Annual Summary - New Project, 
/// Few Clients asks us to name it as New Budget with income, expenditure and Province Help, but 
/// Manfort asks us to name it as Development Projects and wanted few more extra details like local fund and govt fund


using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Bosco.Utility;
using Bosco.Model.Setting;
using ACPP.Modules.Transaction;
using Bosco.Model.Transaction;
using Bosco.Model.UIModel;
using ACPP.Modules.Data_Utility;
using Bosco.Utility.ConfigSetting;
using System.Windows.Forms;
using ACPP.Modules.Master;


namespace ACPP.Modules
{
    public partial class frmFinanceSetting : frmFinanceBaseAdd
    {
        #region Declaration
        ResultArgs resultArgs = null;
        DataTable dtSetting = new DataTable();
        //DataTable dtUISetting = null;
        DataView dtSettings = new DataView();
        private DataTable dtCompareSetting = new DataTable();
        private bool CCMappingChanged = false;


        #endregion

        #region Property


        /// <summary>
        /// Get and Assign Contribution From ledgers
        /// </summary>
        private string ContributionFromLedgers
        {
            get
            {
                string fromledgers = string.Empty;
                chkListContributionFromLedgers.RefreshEditValue();
                List<object> selecteditems = chkListContributionFromLedgers.Properties.Items.GetCheckedValues();

                foreach (object item in selecteditems)
                {
                    fromledgers += item.ToString() + ",";
                }
                fromledgers = fromledgers.TrimEnd(',');
                return fromledgers;
            }
            set
            {
                chkListContributionFromLedgers.SetEditValue(value);
            }
        }

        /// <summary>
        /// Get and Assign Contribution To ledgers
        /// </summary>
        private string ContributionToLedgers
        {
            get
            {
                string toledgers = string.Empty;
                chkListContributionToLedgers.RefreshEditValue();
                List<object> selecteditems = chkListContributionToLedgers.Properties.Items.GetCheckedValues();

                foreach (object item in selecteditems)
                {
                    toledgers += item.ToString() + ",";
                }
                toledgers = toledgers.TrimEnd(',');
                return toledgers;
            }
            set
            {
                chkListContributionToLedgers.SetEditValue(value);
            }
        }

        /// <summary>
        /// On 10/12/2021, Get and Assign Inter Account From ledgers
        /// </summary>
        private string InterAccountFromLedgers
        {
            get
            {
                string fromledgers = string.Empty;
                chkListInterAcFromLedgers.RefreshEditValue();
                List<object> selecteditems = chkListInterAcFromLedgers.Properties.Items.GetCheckedValues();

                foreach (object item in selecteditems)
                {
                    fromledgers += item.ToString() + ",";
                }
                fromledgers = fromledgers.TrimEnd(',');
                return fromledgers;
            }
            set
            {
                chkListInterAcFromLedgers.SetEditValue(value);
            }
        }


        /// <summary>
        /// On 10/12/2021, Get and Assign Inter Account To ledgers
        /// </summary>
        private string InterAccountToLedgers
        {
            get
            {
                string fromledgers = string.Empty;
                chkListInterAcToLedgers.RefreshEditValue();
                List<object> selecteditems = chkListInterAcToLedgers.Properties.Items.GetCheckedValues();

                foreach (object item in selecteditems)
                {
                    fromledgers += item.ToString() + ",";
                }
                fromledgers = fromledgers.TrimEnd(',');
                return fromledgers;
            }
            set
            {
                chkListInterAcToLedgers.SetEditValue(value);
            }
        }

        /// <summary>
        /// Get and Assign Receipt Entry Natures
        /// </summary>
        private string NaturesInReceiptVoucher
        {
            get
            {
                string receiptnatures = string.Empty;
                chkListReceiptVoucherNatures.RefreshEditValue();
                List<object> selecteditems = chkListReceiptVoucherNatures.Properties.Items.GetCheckedValues();

                if (chkListReceiptVoucherNatures.Properties.Items.GetCheckedValues().Count != chkListReceiptVoucherNatures.Properties.Items.Count)
                {
                    foreach (object item in selecteditems)
                    {
                        receiptnatures += item.ToString() + ",";
                    }
                    receiptnatures = receiptnatures.TrimEnd(',');
                }


                return string.IsNullOrEmpty(receiptnatures) ? "0" : receiptnatures;
            }
            set
            {
                string txtvalue = string.IsNullOrEmpty(value) ? "0" : value;

                if (txtvalue == "0") chkListReceiptVoucherNatures.CheckAll();
                else chkListReceiptVoucherNatures.SetEditValue(value);
            }
        }

        /// <summary>
        /// Get and Assign Payment Entry Natures
        /// </summary>
        private string NaturesInPaymentVoucher
        {
            get
            {
                string paymentnatures = string.Empty;
                chkListPaymentVoucherNatures.RefreshEditValue();
                List<object> selecteditems = chkListPaymentVoucherNatures.Properties.Items.GetCheckedValues();
                if (chkListPaymentVoucherNatures.Properties.Items.GetCheckedValues().Count != chkListPaymentVoucherNatures.Properties.Items.Count)
                {
                    foreach (object item in selecteditems)
                    {
                        paymentnatures += item.ToString() + ",";
                    }
                    paymentnatures = paymentnatures.TrimEnd(',');
                }
                return string.IsNullOrEmpty(paymentnatures) ? "0" : paymentnatures;
            }
            set
            {
                string txtvalue = string.IsNullOrEmpty(value) ? "0" : value;

                if (txtvalue == "0") chkListPaymentVoucherNatures.CheckAll();
                else chkListPaymentVoucherNatures.SetEditValue(value);
            }
        }

        #endregion

        #region Constructors
        public frmFinanceSetting()
        {
            InitializeComponent();

            //On 21/07/2020, Disable of Regeneration of Voucher Number
            //lcRegenerateVoucher.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }
        #endregion

        #region Methods
        private void BindValues()
        {
            CCMappingChanged = false;
            this.chkProjectSelection.Checked = this.AppSetting.ProjSelection.ToString() == "1" ? true : false;
            this.chkTransclose.Checked = this.AppSetting.TransClose.ToString() == "1" ? true : false;
            this.chkShowPanGST.Checked = this.AppSetting.ShowGSTPan.ToString() == "1" ? true : false;
            this.rgbTransEntryMode.SelectedIndex = this.AppSetting.TransEntryMethod == "1" ? 0 : 1;
            this.cboTransMode.SelectedIndex = this.AppSetting.TransMode == "1" ? 0 : 1;
            this.chkCustomizationform.Checked = this.AppSetting.CustomizationForm == "1" ? true : false;
            this.chkEnableTransMode.Checked = this.AppSetting.EnableTransMode == "1" ? true : false;
            //this.chkTDSBooking.Checked = this.AppSetting.EnableBookingAtPayment == "1" ? true : false;
            this.chkRegenerateInsert.Checked = this.AppSetting.EnableVoucherRegenerationInsert == "1" ? true : false;
            this.chkRegenerateDelete.Checked = this.AppSetting.EnableVoucherRegenerationDeletion == "1" ? true : false;
            this.chkNegativeBalance.Checked = this.AppSetting.EnableNegativeBalance == "1" ? true : false;
            this.chkVoucherPrint.Checked = this.AppSetting.VoucherPrint == "1" ? true : false;
            this.chkEnableGST.Checked = this.AppSetting.EnableGST == "1" ? true : false;
            //this.chkGSTVendorInvoceDetials.Checked = this.AppSetting.IncludeGSTVendorInvoiceDetails== "1" ? true : false;
            cbIncludeGSTInvoice.SelectedIndex = UtilityMember.NumberSet.ToInteger(this.AppSetting.IncludeGSTVendorInvoiceDetails);
            this.chkChequePrint.Checked = this.AppSetting.EnableChequePrinting == "1" ? true : false;
            this.chkRefwiseRecPayments.Checked = this.AppSetting.EnableRefWiseReceiptANDPayment == "1" ? true : false;
            this.chkExportVouchers.Checked = this.AppSetting.ExportVouchersBeforeClose == "1" ? true : false;
            this.chkDontAlertBackup.Checked = this.AppSetting.DontAlertTakeBackupBeforeClose == "1" ? true : false;
            //this.chkDuplicateVoucherPrint.Checked = this.AppSetting.DuplicateCopyVoucherPrint== "1" ? true : false;
            this.cboVoucherPrint.SelectedIndex = UtilityMember.NumberSet.ToInteger(this.AppSetting.DuplicateCopyVoucherPrint);

            this.chkFlexiFD.Checked = this.AppSetting.EnableFlexiFD == "1" ? true : false;
            //On 28/07/2021
            //this.chkShowBudgetLedgerActualBalance.Checked = this.AppSetting.ShowBudgetLedgerActualBalance== "1" ? true : false;
            rgbShowBudgetLedgerActualBalance.SelectedIndex = UtilityMember.NumberSet.ToInteger(this.AppSetting.ShowBudgetLedgerActualBalance);

            this.chkIncludeBudgetStatistics.Checked = this.AppSetting.IncludeBudgetStatistics == "1" ? true : false;
            this.chkIncludeIncomeLedgersBudget.Checked = this.AppSetting.IncludeIncomeLedgersInBudget == "1" ? true : false;
            this.chkCrDrAmountDrilling.Checked = this.AppSetting.ShowCr_DrAmountDrillingLedgerInAbstract == 1 ? true : false;
            txtMaxCashLedgerAmount.Text = (this.AppSetting.MaxCashLedgerAmountInReceiptsPayments > 0 ? this.AppSetting.MaxCashLedgerAmountInReceiptsPayments.ToString() : string.Empty);
            //On 24/03/2020, to keep showing opening balanes
            this.chkShowCCOpeningBalanceInReports.Checked = this.AppSetting.ShowCCOpeningBalanceInReports == 1 ? true : false;
            //On 11/05/2020, to show reset opening balance 
            this.chkShowRestLedgerOpeningBalance.Checked = this.AppSetting.ShowResetLedgerOpeningBalance == 1 ? true : false;

            //On 08/06/2020, to enable or disable - Madatory Cheque Number in Voucher entry screen
            this.chkMandatoryChequeNo.Checked = this.AppSetting.MandatoryChequeNumberInVoucherEntry == 1 ? true : false;

            chkEnableCashBankEntry.Checked = this.AppSetting.EnableCashBankJournal == 1 ? true : false;
            chkCostCentreMode.Checked = this.AppSetting.EnableCCModeReports == 1 ? true : false;

            chkAlertHighValuePayment.Checked = this.AppSetting.AlertHighValuePaymentEntry == 1 ? true : false;

            chkAlertLocalDonation.Checked = this.AppSetting.AlertLocalDonation == 1 ? true : false;

            //On 08/06/2020, to enable or disable - Allow Duplicate ChequeNumber in Voucher entry screen
            this.chkDonotAllowDuplicateChequeNo.Checked = this.AppSetting.DonotAllowDuplicateChequeNumberInVoucherEntry == 1 ? true : false;

            //On 15/02/2021, to show monthly ledger summary while drilling ledger amount from all Reports
            this.chkShowMonthlySummaryInDrilling.Checked = this.AppSetting.ShowMonthlySummaryDrillingReport == 1 ? true : false;

            //On 03/03/2021, to consider budget new project as New Project or Development Project
            this.radioBudgetNewProjects.SelectedIndex = this.AppSetting.ConsiderBudgetNewProject;

            //On 10/12/2021 to have contribution and inter account ledgers
            ContributionFromLedgers = this.AppSetting.ContributionFromLedgers;
            ContributionToLedgers = this.AppSetting.ContributionToLedgers;
            InterAccountFromLedgers = this.AppSetting.InterAccountFromLedgers;
            InterAccountToLedgers = this.AppSetting.InterAccountToLedgers;

            //21/11/2024,  To have Natures in Receipt  entry, Payment Voucher entry
            NaturesInReceiptVoucher = this.AppSetting.NatuersInReceiptVoucherEntry;
            NaturesInPaymentVoucher = this.AppSetting.NatuersInPaymentVoucherEntry;

            //On 19/01/2022, Allocate cost center amount with GST for Ledger
            this.chkCCwithGSTAmount.Checked = this.AppSetting.AllocateCCAmountWithGST == 1 ? true : false;

            //On 21/11/2022, Mapp CC Project-wise/Ledger-wise
            this.rdCCOpeningBalance.SelectedIndex = this.AppSetting.CostCeterMapping;

            this.chkEnableCCBudget.Checked = this.AppSetting.EnableCostCentreBudget == 1 ? true : false;
            this.chkIncludeBudgetStrengthDetails.Checked = this.AppSetting.IncludeBudgetCCStrengthDetails == 1 ? true : false;
            this.radioCreationBudgetNew_Dev_Projects.SelectedIndex = this.AppSetting.CreateBudgetDevNewProjects;

            this.chkConfirmAuthorizationVoucher.Checked = this.AppSetting.ConfirmAuthorizationVoucherEntry == 1 ? true : false;
            this.chkEnableFDAdjustmentVoucher.Checked = this.AppSetting.EnableFDAdjustmentEntry == 1 ? true : false;
            this.chkShowBudgetInAbstractReport.Checked = this.AppSetting.ShowBudgetApprovedAmountInMonthlyReport == 1 ? true : false;
            this.chkShowCashBankFDDetailInBudgetProposal.Checked = this.AppSetting.ShowCashBankFDDetailLedgerInBudgetProposed == 1 ? true : false;
            this.chkAllowZeroValueCashBank.Checked = this.AppSetting.AllowZeroValuedCashBankVoucherEntry;
            //this.chkAttachVoucherFiles.Checked = this.AppSetting.AttachVoucherFiles == 1 ? true : false;

            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                this.chkProjectSelection.Checked = this.UIAppSetting.ProjSelection.ToString() == "1" ? true : false;
                this.chkTransclose.Checked = this.UIAppSetting.TransClose.ToString() == "1" ? true : false;
                this.rgbTransEntryMode.SelectedIndex = this.UIAppSetting.TransEntryMethod == "1" ? 0 : 1;
                this.cboTransMode.SelectedIndex = this.UIAppSetting.TransMode == "1" ? 0 : 1;
                this.chkCustomizationform.Checked = this.UIAppSetting.UICustomizationForm == "1" ? true : false;
                this.chkEnableTransMode.Checked = this.UIAppSetting.UIEnableTransMode == "1" ? true : false;
                //this.chkTDSBooking.Checked = this.UIAppSetting.UIEnableBookingAtPayment == "1" ? true : false;
                this.chkVoucherPrint.Checked = this.UIAppSetting.VoucherPrint == "1" ? true : false;
            }


            //On 09/10/2024 -  To lock Create/Development projects based on currency FY 
            using (BudgetSystem budgetsystem = new BudgetSystem())
            {
                DataTable dtnewprojectsFY = null;
                ResultArgs result = budgetsystem.FetchNewDevelopmentProjectsByFY();
                if (result.Success)
                {
                    dtnewprojectsFY = result.DataSource.Table;

                    if (dtnewprojectsFY != null && dtnewprojectsFY.Rows.Count > 0)
                    {
                        dtnewprojectsFY.DefaultView.RowFilter = budgetsystem.AppSchema.Budget.BUDGET_IDColumn.ColumnName + " > 0";

                        if (dtnewprojectsFY.DefaultView.Count > 0) //IF With main Budget exists lock to move to Reports
                        {
                            radioCreationBudgetNew_Dev_Projects.SelectedIndex = 1;
                            radioCreationBudgetNew_Dev_Projects.Properties.Items[0].Enabled = false;
                        }
                        else //IF With report exists lock to move to Reports
                        {
                            radioCreationBudgetNew_Dev_Projects.SelectedIndex = 0;
                            radioCreationBudgetNew_Dev_Projects.Properties.Items[1].Enabled = false;
                        }
                    }
                }
            }

        }

        private void SaveGlobalSetting()
        {
            FinanceSetting setting = new FinanceSetting();
            DataView dvSetting = null;
            dvSetting = this.UtilityMember.EnumSet.GetEnumDataSource(setting, Sorting.Ascending);
            dtSetting = dvSetting.ToTable();
            if (dtSetting != null)
            {

                dtSetting.Columns.Add("Value", typeof(string));
                dtSetting.Columns.Add("UserId", typeof(string));
                for (int i = 0; i < dtSetting.Rows.Count; i++)
                {
                    FinanceSetting SettingName = (FinanceSetting)Enum.Parse(typeof(FinanceSetting), dtSetting.Rows[i][1].ToString());
                    string Value = "";
                    switch (SettingName)
                    {

                        case FinanceSetting.UIProjSelection:
                            {
                                Value = chkProjectSelection.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.UITransClose:
                            {
                                Value = chkTransclose.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.UITransGSTPan:
                            {
                                Value = chkShowPanGST.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.TransEntryMethod:
                            {
                                //Value = rgbTransEntryMode.Properties.Items[rgbTransEntryMode.SelectedIndex].Value.ToString();
                                Value = rgbTransEntryMode.Properties.Items[rgbTransEntryMode.SelectedIndex].Value.ToString();
                                //Value=  rgbTransEntryMode.SelectedIndex == 0 ? "0" : "1"; 
                                break;
                            }
                        case FinanceSetting.UITransMode:
                            {
                                Value = cboTransMode.SelectedIndex == 0 ? "1" : "2";
                                //Value = cboTransMode.SelectedIndex == 0 ? "0" : "1"; //
                                break;
                            }
                        case FinanceSetting.EnableBookingAtPayment:
                            {
                                Value = "0";  //chkTDSBooking.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.PrintVoucher:
                            {
                                Value = chkVoucherPrint.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.CustomizationForm:
                            {
                                Value = chkCustomizationform.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.EnableTransMode:
                            {
                                Value = chkEnableTransMode.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.EnableVoucherRegenerationInsert:
                            {
                                Value = chkRegenerateInsert.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.EnableVoucherRegenerationDeletion:
                            {
                                Value = chkRegenerateDelete.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.EnableNegativeBalance:
                            {
                                Value = chkNegativeBalance.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.EnableGST:
                            {
                                Value = chkEnableGST.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.IncludeGSTVendorInvoiceDetails:
                            {
                                //Value = chkGSTVendorInvoceDetials.Checked == true ? "1" : "0";

                                //On 26/02/2025, hide "Attach wtih Receipt/Payment Voucher" for multi currency and other than india
                                if (this.AppSetting.IsCountryOtherThanIndia || this.AppSetting.AllowMultiCurrency == 1)
                                {
                                    //For general license 3 options are there, but multi currency and other than india (only two options)
                                    Value = (cbIncludeGSTInvoice.SelectedIndex == 1 ? "2" : "0");
                                }
                                else
                                    Value = cbIncludeGSTInvoice.SelectedIndex.ToString();

                                break;
                            }
                        case FinanceSetting.EnableChequePrinting:
                            {
                                Value = chkChequePrint.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.EnableRefWiseRecPayment:
                            {
                                Value = chkRefwiseRecPayments.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.ExportVouchersBeforeClose:
                            {
                                Value = chkExportVouchers.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.DontAlertTakeBackupBeforeClose:
                            {
                                Value = chkDontAlertBackup.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.DuplicateCopyVoucherPrint:
                            {
                                //Value = this.chkDuplicateVoucherPrint.Checked == true ? "1" : "0";

                                Value = cboVoucherPrint.SelectedIndex.ToString();
                                break;
                            }
                        case FinanceSetting.EnableFlexiFD:
                            {
                                Value = this.chkFlexiFD.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.ShowBudgetLedgerActualBalance:
                            {
                                //0 - Ledger Closing Balance (without JV)
                                //2 - Ledger Closing Balance (with JV)
                                //3 - R & P separately
                                Value = "0";
                                //Value = this.chkShowBudgetLedgerActualBalance.Checked == true ? "1" : "0";
                                Value = rgbShowBudgetLedgerActualBalance.SelectedIndex.ToString();
                                break;
                            }
                        case FinanceSetting.IncludeBudgetStatistics:
                            {
                                Value = this.chkIncludeBudgetStatistics.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.IncludeIncomeLedgersInBudget:
                            {
                                Value = this.chkIncludeIncomeLedgersBudget.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.MaxCashLedgerAmountInReceiptsPayments:
                            {
                                double maxcashlegeramt = UtilityMember.NumberSet.ToDouble(txtMaxCashLedgerAmount.Text);
                                Value = (maxcashlegeramt > 0 ? maxcashlegeramt.ToString() : string.Empty);
                                break;
                            }
                        case FinanceSetting.ShowCr_DrAmountDrillingLedgerInAbstract:
                            {
                                Value = this.chkCrDrAmountDrilling.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.ShowCCOpeningBalanceInReports: //On 24/03/2020, to keep showing opening balanes
                            {
                                Value = this.chkShowCCOpeningBalanceInReports.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.ShowResetLedgerOpeningBalance: //On 11/05/2020, to show reset ledger opening balance option in account map
                            {
                                Value = this.chkShowRestLedgerOpeningBalance.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.MandatoryChequeNumberInVoucherEntry:  //On 08/06/2020, to enable or disable - Madatory Cheque Number in Voucher entry screen
                            {
                                Value = this.chkMandatoryChequeNo.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.EnableCashBankJournal:
                            {
                                Value = this.chkEnableCashBankEntry.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.EnableCCMode:
                            {
                                Value = this.chkCostCentreMode.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.AlertHighValuePayment:
                            {
                                Value = this.chkAlertHighValuePayment.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.AlertLocalDonations:
                            {
                                Value = this.chkAlertLocalDonation.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.DonotAllowDuplicateChequeNumberInVoucherEntry://On 08/06/2020, to enable or disable - Allow Duplicate ChequeNumber in Voucher entry screen
                            {
                                Value = this.chkDonotAllowDuplicateChequeNo.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.ShowMonthlySummaryDrillingReport:
                            //On 15/02/2021, to show monthly ledger summary while drilling ledger amount from all Reports
                            {
                                Value = this.chkShowMonthlySummaryInDrilling.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.ConsiderBudgetNewProject:
                            //On 03/03/2021, to consider budget new project as New Project or Development Project
                            {
                                Value = this.radioBudgetNewProjects.SelectedIndex.ToString();
                                break;
                            }
                        case FinanceSetting.ContributionFromLedgers:
                            { //On 10/12/2021, to have contribution from ledgers
                                Value = ContributionFromLedgers;
                                break;
                            }
                        case FinanceSetting.ContributionToLedgers:
                            { //On 10/12/2021, to have contribution to ledgers
                                Value = ContributionToLedgers;
                                break;
                            }
                        case FinanceSetting.InterAccountFromLedgers:
                            { //On 10/12/2021, to have inter account from ledgers
                                Value = InterAccountFromLedgers;
                                break;
                            }
                        case FinanceSetting.InterAccountToLedgers:
                            { //On 10/12/2021, to have inter account to ledgers
                                Value = InterAccountToLedgers;
                                break;
                            }
                        case FinanceSetting.NatuersInReceiptVoucherEntry:
                            { //On 21/11/2024, to have Natures in Receipt Voucher entry
                                Value = NaturesInReceiptVoucher;
                                break;
                            }
                        case FinanceSetting.NatuersInPaymentVoucherEntry:
                            { //On 21/11/2024, to have Natures in Payment Voucher entry
                                Value = NaturesInPaymentVoucher;
                                break;
                            }
                        case FinanceSetting.AllocateCCAmountWithGST:
                            { //On 19/01/2022, Allocate cost center amount with GST for Ledger
                                Value = this.chkCCwithGSTAmount.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.LCRef1:
                            { //On 19/01/2022, Allocate cost center amount with GST for Ledger
                                Value = this.chkCCwithGSTAmount.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.CostCeterMapping:
                            { //On 21/11/2022, Mapp CC Project-wise/Ledger-wise
                                Value = this.rdCCOpeningBalance.SelectedIndex.ToString();
                                break;
                            }
                        case FinanceSetting.GeneralateOpeningIEBalance:
                            { //On 02/03/2023, Mapp CC Project-wise/Ledger-wise
                                Value = AppSetting.GeneralateOpeningIEBalance.ToString();
                                break;
                            }
                        case FinanceSetting.GeneralateOpeningIEBalanceMode:
                            { //On 02/03/2023, Mapp CC Project-wise/Ledger-wise
                                Value = AppSetting.GeneralateOpeningIEBalanceMode.ToString();
                                break;
                            }
                        case FinanceSetting.EnableCostCentreBudget:
                            {
                                Value = chkEnableCCBudget.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.IncludeBudgetCCStrengthDetails:
                            {
                                Value = chkIncludeBudgetStrengthDetails.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.CreateBudgetDevNewProjects:
                            {
                                Value = this.radioCreationBudgetNew_Dev_Projects.SelectedIndex.ToString();
                                break;
                            }
                        case FinanceSetting.ConfirmAuthorizationVoucherEntry:
                            {
                                Value = chkConfirmAuthorizationVoucher.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.EnableFDAdjustmentEntry:
                            {
                                Value = chkEnableFDAdjustmentVoucher.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.ShowBudgetApprovedAmountInMonthlyReport:
                            {
                                Value = chkShowBudgetInAbstractReport.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.ShowCashBankFDDetailLedgerInBudgetProposed:
                            {
                                Value = chkShowCashBankFDDetailInBudgetProposal.Checked == true ? "1" : "0";
                                break;
                            }
                        case FinanceSetting.AllowZeroValuedCashBankVoucherEntry:
                            {
                                Value = chkAllowZeroValueCashBank.Checked == true ? "1" : "0";
                                break;
                            }
                        //case FinanceSetting.AttachVoucherFiles:
                        //    {
                        //        Value = chkAttachVoucherFiles.Checked == true ? "1" : "0";
                        //        break;
                        //    }
                    }
                    dtSetting.Rows[i][2] = Value;
                    dtSetting.Rows[i][3] = this.LoginUser.LoginUserId;
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
            }
            FetchDataStatus();
        }

        public void FetchDataStatus()
        {
            DataTable dtAssignedSource = dtSetting;
            DataTable dtOldAssigneSource = dtCompareSetting; // dtCompareSetting;

            var matched = from table1 in dtAssignedSource.AsEnumerable()
                          join table2 in dtOldAssigneSource.AsEnumerable() on table1.Field<string>("Value") equals table2.Field<string>("Value")
                          select table1;

            var missing = from table1 in dtAssignedSource.AsEnumerable()
                          where !matched.Contains(table1)
                          select table1;

            if (missing.Count() > 0)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }

        private void ApplySetting()
        {
            ISetting isetting;
            isetting = new GlobalSetting();
            ResultArgs resultArg = isetting.FetchSettingDetails(this.UtilityMember.NumberSet.ToInteger(this.LoginUser.LoginUserId.ToString()));
            if (resultArg.Success && resultArg.DataSource.TableView != null && resultArg.DataSource.TableView.Count != 0)
            {
                dtSettings = resultArg.DataSource.TableView;
                DataTable dt = dtCompareSetting = dtSetting.AsDataView().ToTable();
                this.UIAppSetting.UISettingInfo = resultArg.DataSource.TableView;
            }
        }


        /// <summary>
        /// Check inputs and alert and lock updating to DB
        /// </summary>
        /// <returns></returns>
        private bool CheckValidation()
        {
            bool isValid = false;
            //1. Check FD Reinvestment exists
            if (HasFDReinvestment())
            {
                isValid = false;
            }
            else if (HasFDAdjustmentEntry())
            {
                isValid = false;
            }
            else if (HasBudgetStatistics())
            {
                isValid = false;
            }
            else if (HasBudgetIncomeLedgers())
            {
                isValid = false;
            }
            else if (HasGSTVouchers())
            {
                isValid = false;
            }
            else if (HasCCMapping())
            {
                isValid = false;
            }
            else if (isCashBankEnabledJournal())
            {
                isValid = false;
            }
            else if (CheckContributionLedgers())
            {
                isValid = false;
            }
            else if (CheckAllowZeroValuedCashBank())
            {
                isValid = false;
            }
            else if (IsReceiptPaymentVoucherMadeForOtherNatures())
            {
                isValid = false;
            }
            //else if (IsVoucherFilesExists())
            //{
            //    isValid = false;
            //}
            else
            {
                isValid = true;
            }

            return isValid;
        }


        /// <summary>
        /// This method is used to check any FDs are re-invested, if so, disable to uncheck Flexi FD feature
        /// </summary>
        /// <returns></returns>
        private bool HasFDReinvestment()
        {
            bool rtn = false;

            if (!chkFlexiFD.Checked)
            {
                using (FDAccountSystem fdaccount = new FDAccountSystem())
                {
                    bool reinvestedexists = fdaccount.HasFlxiFD();

                    if (reinvestedexists)
                    {
                        this.ShowMessageBoxWarning("Flexi FDs are available, you can't disable Flexi FD feature.");
                        rtn = true;
                    }
                }

            }
            return rtn;
        }

        private bool HasFDAdjustmentEntry()
        {
            bool rtn = false;

            if (!chkEnableFDAdjustmentVoucher.Checked)
            {
                using (FDAccountSystem fdaccount = new FDAccountSystem())
                {
                    bool reinvestedexists = fdaccount.HasFDAdjustmentEntries();

                    if (reinvestedexists)
                    {
                        this.ShowMessageBoxWarning("Fixed Deposit Adjustment Voucher(s) are available, you can't disable Fixed Deposit Adjustment feature.");
                        rtn = true;
                    }
                }

            }
            return rtn;
        }

        /// <summary>
        /// This method is used to check Budget Statistics, if so, disable to uncheck Budget Statistics
        /// </summary>
        /// <returns></returns>
        private bool HasBudgetStatistics()
        {
            bool rtn = false;

            if (!chkIncludeBudgetStatistics.Checked)
            {
                using (BudgetSystem budgetsystem = new BudgetSystem())
                {
                    bool budgetstatisticsexists = budgetsystem.HasBudgetStatistics();

                    if (budgetstatisticsexists)
                    {
                        this.ShowMessageBoxWarning("Budget Statistic(s) are available, you can't disable Budget Statistics feature.");
                        rtn = true;
                    }
                }

            }
            return rtn;
        }

        /// <summary>
        /// This method is used to check Budget Income Ledger, if so, disable to uncheck Budget Income Ledgers
        /// </summary>
        /// <returns></returns>
        private bool HasBudgetIncomeLedgers()
        {
            bool rtn = false;

            if (!chkIncludeIncomeLedgersBudget.Checked)
            {
                using (BudgetSystem budgetsystem = new BudgetSystem())
                {
                    bool budgetstatisticsexists = budgetsystem.HasBudgetIncomeLedger();

                    if (budgetstatisticsexists)
                    {
                        this.ShowMessageBoxWarning("Budget(s) are made for Income Ledgers, you can't disable Budget Income Ledgers feature.");
                        rtn = true;
                    }
                }

            }
            return rtn;
        }

        /// <summary>
        /// This method is used to check Gst vouchers , if so, altert message alone
        /// </summary>
        /// <returns></returns>
        private bool HasGSTVouchers()
        {
            bool rtn = false;

            if (!chkEnableGST.Checked && (!this.AppSetting.IsCountryOtherThanIndia && this.AppSetting.MultiLocation == 0))
            {
                using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
                {
                    bool gstvouchersexists = vouchersystem.IsExistsGSTVouchers();

                    if (gstvouchersexists)
                    {
                        if (this.ShowConfirmationMessage("GST enabled Vouchers are already made. GST option has been disabled,  are you sure to proceed ?", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            rtn = false; //altert message alone
                        }
                        else
                        {
                            rtn = true;
                        }
                    }
                }
            }

            //if (!chkGSTVendorInvoceDetials.Checked && !rtn)
            if (cbIncludeGSTInvoice.SelectedIndex == 0 && !rtn)
            {
                using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
                {
                    bool gstvouchervendorsexists = vouchersystem.IsExistsGSTVendorDetails();

                    if (gstvouchervendorsexists)
                    {
                        string msg = "Vendor Invoice details are already made. Attach Vendor Invoice has been disabled,  are you sure to proceed ?";

                        if (this.ShowConfirmationMessage(msg, System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            rtn = false; //altert message alone
                        }
                        else
                        {
                            rtn = true; //altert message alone
                        }
                    }
                }
            }

            return rtn;
        }

        private bool IsCCMappingAvailable(bool isProjectBased)
        {
            bool rtn = false;
            using (MappingSystem mappingsystem = new MappingSystem())
            {
                resultArgs = mappingsystem.CheckCostCentreMappingBySetting(isProjectBased);

                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    rtn = true;
                }
            }
            return rtn;
        }

        private bool CheckContributionLedgers()
        {
            bool rtn = false;
            string contributionToLedgers = ContributionFromLedgers;
            string contributionFromLedgers = ContributionToLedgers;
            string interaccountFromLedgers = InterAccountFromLedgers;
            string interaccountToLedgers = InterAccountToLedgers;
            if (contributionToLedgers.Length > 100 || contributionFromLedgers.Length > 100 ||
                interaccountFromLedgers.Length > 100 || interaccountToLedgers.Length > 100)
            {
                this.ShowMessageBoxWarning("Number of Contribution From/To Ledger(s) Or Inter Accounts From/To Ledger(s) are exceeds");
                rtn = true;
            }

            return rtn;
        }

        private bool CheckAllowZeroValuedCashBank()
        {
            bool rtn = false;

            if (!chkAllowZeroValueCashBank.Checked)
            {
                using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
                {
                    rtn = vouchersystem.IsZeroValuedCashBankExistsInVouchers();
                    if (rtn)
                    {
                        MessageRender.ShowMessage("Zero valued Cash/Bank Voucher(s) are available, Can't disable \"Allow Zero valued Cash/Bank\"");
                    }
                }
            }

            if (!rtn && chkAllowZeroValueCashBank.Checked && !chkEnableTransMode.Checked)
            {
                MessageRender.ShowMessage("As \"Allow Zero valued Cash/Bank\" option is enabled\", \"Double Entry\" is must be enabled");
                chkEnableTransMode.Checked = true;
            }

            return rtn;
        }

        /// <summary>
        /// Check not seleted Natures in Vocuher entry for Receipt/Payment
        /// </summary>
        /// <returns></returns>
        private bool IsReceiptPaymentVoucherMadeForOtherNatures()
        {
            bool rtn = false;
            //Receipt Natuers
            using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
            {
                if (NaturesInReceiptVoucher != "0")
                {
                    rtn = vouchersystem.IsReceiptPaymentVoucherMadeForOtherNatures(VoucherSubTypes.RC.ToString(), NaturesInReceiptVoucher);
                    if (rtn)
                    {
                        MessageRender.ShowMessage("Receipt Voucher(s) have other than selected Nature(s).");
                    }
                }

                if (!rtn && NaturesInPaymentVoucher != "0")
                {
                    rtn = vouchersystem.IsReceiptPaymentVoucherMadeForOtherNatures(VoucherSubTypes.PY.ToString(), NaturesInPaymentVoucher);
                    if (rtn)
                    {
                        MessageRender.ShowMessage("Payment Voucher(s) have other than selected Nature(s).");
                    }
                }
            }

            return rtn;
        }

        private bool IsVouchersExists()
        {
            bool rtn = true;

            using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
            {
                rtn = vouchersystem.IsVouchersExists();
            }

            return rtn;
        }

        private bool isCashBankEnabledJournal()
        {
            bool rtn = false;

            if (this.AppSetting.IsCountryOtherThanIndia || this.AppSetting.AllowMultiCurrency == 1)
            {
                if (!chkEnableCashBankEntry.Checked)
                {
                    using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
                    {

                        rtn = vouchersystem.isCashBankEnabledJournal();
                        if (rtn)
                        {
                            this.ShowMessageBoxWarning("You cannot change the 'Cash and Bank Enabled' option as there are existing journal entries using cash and bank.");
                            rtn = true;
                        }
                    }
                }
            }

            return rtn;
        }

        /// <summary>
        /// This method is used to check Ledger-wise CC is available, if so alert message
        /// </summary>
        /// <returns></returns>
        private bool HasCCMapping()
        {
            bool rtn = false;

            if (this.rdCCOpeningBalance.SelectedIndex == 1)
            {
                if (IsCCMappingAvailable(true))
                {
                    this.ShowMessageBoxWarning("Project-wise (General) Cost Centre mapping is available, Can't set Ledger-wise Cost Centre Mapping");
                    rtn = true;
                }
            }
            else
            {
                if (IsCCMappingAvailable(false))
                {
                    this.ShowMessageBoxWarning("Ledger-wise Cost Centre mapping is available, Can't set Project-wise Cost Centre Mapping");
                    rtn = true;
                }
            }

            return rtn;
        }

        //private bool IsVoucherFilesExists()
        //{
        //    ResultArgs resultarg = new ResultArgs();
        //    bool rtn = false;
        //    try
        //    {
        //        if (!chkAttachVoucherFiles.Checked)
        //        {
        //            using (VoucherTransactionSystem vtranssystem = new VoucherTransactionSystem())
        //            {
        //                resultarg = vtranssystem.IsExistsVoucherFiles();
        //                if (resultarg != null && resultarg.Success)
        //                {
        //                    this.ShowMessageBoxWarning("Attached Voucher File(s) are available, Can't disable \"Attach Voucher Files\"");
        //                    rtn = true;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        resultarg.Message = ex.ToString();
        //    }

        //    return rtn;
        //}

        /// <summary>
        /// Bind Contribution From/To Ledgers
        /// </summary>
        private void BindContributionLedgers()
        {
            using (LedgerSystem ledsystem = new LedgerSystem())
            {
                resultArgs = ledsystem.FetchLedgerDetails();

                if (resultArgs != null && resultArgs.Success)
                {
                    DataTable dtContributionList = resultArgs.DataSource.Table;

                    //1. For Contribution From ( Income Ledgers)
                    dtContributionList.DefaultView.RowFilter = ledsystem.AppSchema.LedgerGroup.NATURE_IDColumn + "=" + (int)Natures.Income;
                    DataTable dtContributionFrom = dtContributionList.DefaultView.ToTable();
                    string condition = ledsystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName + " IN (" + AppSetting.ContributionFromLedgers + ")";
                    dtContributionFrom.Columns.Add("SELECT", typeof(System.Int32), condition);
                    dtContributionFrom.DefaultView.Sort = "SELECT DESC, " + ledsystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName;
                    chkListContributionFromLedgers.Properties.DataSource = dtContributionFrom;
                    chkListContributionFromLedgers.Properties.ValueMember = ledsystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName;
                    chkListContributionFromLedgers.Properties.DisplayMember = ledsystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName;
                    ContributionFromLedgers = AppSetting.ContributionFromLedgers;

                    //2. For Contribution To ( Expense Ledgers)
                    dtContributionList.DefaultView.RowFilter = ledsystem.AppSchema.LedgerGroup.NATURE_IDColumn + "=" + (int)Natures.Expenses;
                    DataTable dtContributionTo = dtContributionList.DefaultView.ToTable();
                    condition = ledsystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName + " IN (" + AppSetting.ContributionToLedgers + ")";
                    dtContributionTo.Columns.Add("SELECT", typeof(System.Int32), condition);
                    dtContributionTo.DefaultView.Sort = "SELECT DESC, " + ledsystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName;
                    chkListContributionToLedgers.Properties.DataSource = dtContributionTo;
                    chkListContributionToLedgers.Properties.ValueMember = ledsystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName;
                    chkListContributionToLedgers.Properties.DisplayMember = ledsystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName;
                    ContributionToLedgers = AppSetting.ContributionToLedgers;

                    //3. For Inter Account From (All Ledgers)
                    string skipCashBankFD = (int)FixedLedgerGroup.Cash + "," + (int)FixedLedgerGroup.BankAccounts + "," + (int)FixedLedgerGroup.FixedAssets;
                    dtContributionList.DefaultView.RowFilter = ledsystem.AppSchema.LedgerGroup.GROUP_IDColumn + " NOT IN (" + skipCashBankFD + ")";
                    DataTable dtInterAccountFromLedgers = dtContributionList.DefaultView.ToTable();
                    condition = ledsystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName + " IN (" + AppSetting.InterAccountFromLedgers + ")";
                    dtInterAccountFromLedgers.Columns.Add("SELECT", typeof(System.Int32), condition);
                    dtInterAccountFromLedgers.DefaultView.Sort = "SELECT DESC, " + ledsystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName;
                    chkListInterAcFromLedgers.Properties.DataSource = dtInterAccountFromLedgers;
                    chkListInterAcFromLedgers.Properties.ValueMember = ledsystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName;
                    chkListInterAcFromLedgers.Properties.DisplayMember = ledsystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName;
                    InterAccountFromLedgers = AppSetting.InterAccountFromLedgers;

                    //4. For Inter Account To (All Ledgers)
                    dtContributionList.DefaultView.RowFilter = ledsystem.AppSchema.LedgerGroup.GROUP_IDColumn + " NOT IN (" + skipCashBankFD + ")";
                    DataTable dtInterAccountToLedgers = dtContributionList.DefaultView.ToTable();
                    condition = ledsystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName + " IN (" + AppSetting.InterAccountToLedgers + ")";
                    dtInterAccountToLedgers.Columns.Add("SELECT", typeof(System.Int32), condition);
                    dtInterAccountToLedgers.DefaultView.Sort = "SELECT DESC, " + ledsystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName;
                    chkListInterAcToLedgers.Properties.DataSource = dtInterAccountFromLedgers;
                    chkListInterAcToLedgers.Properties.ValueMember = ledsystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName;
                    chkListInterAcToLedgers.Properties.DisplayMember = ledsystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName;
                    InterAccountToLedgers = AppSetting.InterAccountToLedgers;
                }
            }

        }

        /// <summary>
        /// Bind Natures
        /// </summary>
        private void BindNatures()
        {
            using (LedgerGroupSystem ledgerGrp = new LedgerGroupSystem())
            {
                resultArgs = ledgerGrp.FetchLedgerGroupNature();

                if (resultArgs != null && resultArgs.Success)
                {
                    DataTable dtNatures = resultArgs.DataSource.Table;

                    //1. For Receipt Voucher Natures (Income Ledgers)
                    chkListReceiptVoucherNatures.Properties.DataSource = dtNatures;
                    chkListReceiptVoucherNatures.Properties.ValueMember = ledgerGrp.AppSchema.LedgerGroup.NATURE_IDColumn.ColumnName;
                    chkListReceiptVoucherNatures.Properties.DisplayMember = ledgerGrp.AppSchema.LedgerGroup.NATUREColumn.ColumnName;
                    NaturesInReceiptVoucher = AppSetting.NatuersInReceiptVoucherEntry;

                    //2. For Payment Voucher Nature (Expense Ledgers)
                    chkListPaymentVoucherNatures.Properties.DataSource = dtNatures;
                    chkListPaymentVoucherNatures.Properties.ValueMember = ledgerGrp.AppSchema.LedgerGroup.NATURE_IDColumn.ColumnName;
                    chkListPaymentVoucherNatures.Properties.DisplayMember = ledgerGrp.AppSchema.LedgerGroup.NATUREColumn.ColumnName;
                    NaturesInPaymentVoucher = AppSetting.NatuersInPaymentVoucherEntry;
                }
            }

        }
        #endregion

        #region Events
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckValidation())
                {
                    ISetting isetting;
                    SaveGlobalSetting();
                    if (this.UtilityMember.NumberSet.ToInteger(LoginUser.LoginUserId) == 1)
                    {
                        isetting = new GlobalSetting();
                        resultArgs = isetting.SaveSetting(dtSetting);
                    }
                    else
                    {
                        isetting = new UISetting();
                        resultArgs = isetting.SaveSetting(dtSetting);
                    }
                    if (resultArgs.Success)
                    {
                        CCMappingChanged = false;
                        ApplySetting();
                        //this.ShowSuccessMessage("Saved");
                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_FINANCE_SETTING_SUCCESS_INFO));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void frmFinanceSetting_Load(object sender, EventArgs e)
        {
            //On 03/05/2022, To map province contribution and inter account ledgers
            MapContributionLedgersByDefaultForSDBINM();

            //On 23/11/2021, for FMA congregation 
            lcCongregationSetting.Visibility = (this.AppSetting.IS_FMA_CONGREGATION ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always :
                                                DevExpress.XtraLayout.Utils.LayoutVisibility.Never);

            BindContributionLedgers();
            BindNatures();

            ApplySetting();
            BindValues();

            /* Commented by Praveen : To show this option always  not to be dependent on the General Setting TDS Option
            if (this.AppSetting.TDSEnabled == "1")
                layoutControlItem15.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            else
            {
                layoutControlItem15.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.Height = this.Height - layoutControlItem15.Height;
            } */
            //lcAllowMultiCurrency.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            if (IsCCMappingAvailable(true) || IsCCMappingAvailable(false))
            {
                this.rdCCOpeningBalance.Enabled = false;
            }
            lcCCMapChange.Visibility = rdCCOpeningBalance.Enabled ? DevExpress.XtraLayout.Utils.LayoutVisibility.Never : DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

            //On 28/02/2024, TDS related featues
            if (!SettingProperty.EnableTDS)
            {
                //lcMakeTDSBookingInPartyPayment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            this.EnforceReceiptModule(new object[] { chkRegenerateInsert, chkRegenerateDelete }, true);

            //10/07/2024, If other than india country let us lock GST
            if (this.AppSetting.IsCountryOtherThanIndia || this.AppSetting.AllowMultiCurrency == 1)
            {
                //On 26/02/2025, hide "Attach wtih Receipt/Payment Voucher" for multi currency and other than india
                cbIncludeGSTInvoice.Properties.Items.RemoveAt(1);
                //cbIncludeGSTInvoice.SelectedIndex = 0; // By default attach with journal always fixed
                //cbIncludeGSTInvoice.Enabled = false;

                //lcGST.Visibility = lcGSTVendorInvoceDetails.Visibility = lcCCwithGSTAmount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;   
                layoutControl1.HideItem(lcGST);
                //layoutControl1.HideItem(lcGSTVendorInvoceDetails);
                layoutControl1.HideItem(lcCCwithGSTAmount);

                //For temp
                //layoutControl1.HideItem(lcEnableFlexiFD);
                layoutControl1.HideItem(lcFDAdjustmentEntry);
                layoutControl1.HideItem(lcEnableReferencewisePayment);
                //layoutControl1.HideItem(lcIncludeCCBudget);
                layoutControl1.HideItem(lcIncludeBudgetCCStrengthDetails);
                layoutControl1.HideItem(lcCashMaxAmount);
                layoutControl1.HideItem(lblTransMode);
                layoutControl1.HideItem(lciLocalDonationItem14);
                layoutControl1.HideItem(lciAlertHigvaluePayment15);


                if (this.AppSetting.AllowMultiCurrency == 1)
                {
                    layoutControl1.HideItem(lcCCOpeningBalance);
                    layoutControl1.HideItem(lcCCMapChange);
                }

                lcAuditorNote.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lciEnableCashBankEntries.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                lciPanGST.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lciMapActualBalance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                lcGrpOption.Height = 500;
                // this.Height = 605; 14/04/2025, *Chinna  
                this.Height = 621;  // 550;//600;// 625; //625;// 600;
            }
            else
            {
                this.Height = 650; //625;// 600;
            }


            //lcAttachVoucherFiles.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.CenterToScreen();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnVoucherPrintSetting_Click(object sender, EventArgs e)
        {
            frmVoucherPrintSetting frmVprintsetting = new frmVoucherPrintSetting();
            frmVprintsetting.ShowDialog();
        }



        private void chkEnableGST_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkEnableGST.Checked)
            {
                //chkGSTVendorInvoceDetials.Checked = false;
                cbIncludeGSTInvoice.SelectedIndex = 0;
            }
        }

        private void btnSignDetails_Click(object sender, EventArgs e)
        {
            frmRolesWithSignSetting frmrolesigndetails = new frmRolesWithSignSetting(this);
            frmrolesigndetails.ShowDialog();
        }

        private void btnCCMapChange_Click(object sender, EventArgs e)
        {
            try
            {
                bool isProjectBased = (rdCCOpeningBalance.SelectedIndex == 0);
                string from = rdCCOpeningBalance.Properties.Items[rdCCOpeningBalance.SelectedIndex].Description;
                string to = isProjectBased ? rdCCOpeningBalance.Properties.Items[1].Description : rdCCOpeningBalance.Properties.Items[0].Description;
                string msg = "Are you sure to change Cost Centre Mapping from '" + from + "' to '" + to + "' ?" + System.Environment.NewLine + System.Environment.NewLine +
                             "Yes : Existing Cost Centre Mapping and Its Opening Balance will be reset" + System.Environment.NewLine +
                             "No  : Keep existing  Cost Centre Mapping";
                if (this.ShowConfirmationMessage(msg, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    this.ShowWaitDialog("Changing Cost Centre Mapping...");
                    using (MappingSystem mappingSys = new MappingSystem())
                    {
                        ResultArgs result = mappingSys.ChangeAllCostCentreMapping(!isProjectBased);
                        if (result.Success)
                        {
                            rdCCOpeningBalance.SelectedIndex = isProjectBased ? 1 : 0;
                            rdCCOpeningBalance.Enabled = (result.RowsAffected == 0);
                            CCMappingChanged = true;
                            this.ShowMessageBox("Sucessfully changed Cost Centre Mapping, Save Finance Setting");
                        }
                    }

                    this.CloseWaitDialog();
                }
            }
            catch (Exception err)
            {
                this.ShowMessageBox(err.Message);
            }
            finally
            {
                lcCCMapChange.Visibility = rdCCOpeningBalance.Enabled ? DevExpress.XtraLayout.Utils.LayoutVisibility.Never : DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.CloseWaitDialog();
            }
        }

        private void btnFMASCongregationSetting_Click(object sender, EventArgs e)
        {
            frmMapLedgersWithGeneralate frmcongregationsetting = new frmMapLedgersWithGeneralate();
            frmcongregationsetting.ShowDialog();
        }

        /// <summary>
        /// For SDBINM
        /// On 03/05/2022, To map province contribution and inter account ledgers
        /// </summary>
        private void MapContributionLedgersByDefaultForSDBINM()
        {
            Int32 lid = 0;
            string ledgername = string.Empty;
            string settingname = string.Empty;
            bool reloadsetting = false;

            if (this.AppSetting.IS_SDB_INM)
            {
                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    ResultArgs result = ledgersystem.FetchLedgerDetails();
                    if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtLedgers = result.DataSource.Table;
                        dtLedgers.DefaultView.RowFilter = ledgersystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName + " IN ('Contribution from Province', 'Contribution to Province', " +
                                                         "'Inter Account Transfer', 'Inter Account Transfer')";

                        foreach (DataRowView drv in dtLedgers.DefaultView)
                        {
                            using (UISetting uisetting = new UISetting())
                            {
                                ledgername = drv[ledgersystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString().Trim();
                                lid = UtilityMember.NumberSet.ToInteger(drv[ledgersystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString());
                                settingname = string.Empty;
                                if (ledgername.ToUpper() == "CONTRIBUTION FROM PROVINCE" && (string.IsNullOrEmpty(AppSetting.ContributionFromLedgers) || AppSetting.ContributionFromLedgers == "0"))
                                {
                                    settingname = FinanceSetting.ContributionFromLedgers.ToString();
                                    uisetting.SaveSettingDetails(settingname, lid.ToString(), UtilityMember.NumberSet.ToInteger(this.LoginUser.LoginUserId));
                                    reloadsetting = true;
                                }
                                else if (ledgername.ToUpper() == "CONTRIBUTION TO PROVINCE" && (string.IsNullOrEmpty(AppSetting.ContributionToLedgers) || AppSetting.ContributionToLedgers == "0"))
                                {
                                    settingname = FinanceSetting.ContributionToLedgers.ToString();
                                    uisetting.SaveSettingDetails(settingname, lid.ToString(), UtilityMember.NumberSet.ToInteger(this.LoginUser.LoginUserId));
                                    reloadsetting = true;
                                }
                                else if (ledgername.ToUpper() == "INTER ACCOUNT TRANSFER")
                                {
                                    if (string.IsNullOrEmpty(AppSetting.InterAccountFromLedgers) || AppSetting.InterAccountFromLedgers == "0")
                                    {
                                        settingname = FinanceSetting.InterAccountFromLedgers.ToString();
                                        uisetting.SaveSettingDetails(settingname, lid.ToString(), UtilityMember.NumberSet.ToInteger(this.LoginUser.LoginUserId));
                                        reloadsetting = true;
                                    }

                                    if (string.IsNullOrEmpty(AppSetting.InterAccountToLedgers) || AppSetting.InterAccountToLedgers == "0")
                                    {
                                        settingname = FinanceSetting.InterAccountToLedgers.ToString();
                                        uisetting.SaveSettingDetails(settingname, lid.ToString(), UtilityMember.NumberSet.ToInteger(this.LoginUser.LoginUserId));
                                        reloadsetting = true;
                                    }
                                }
                            }
                        }
                    }
                }

                if (reloadsetting)
                {
                    ApplySetting();
                }
            }
        }

        private void frmFinanceSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CCMappingChanged)
            {
                this.ShowMessageBox("Cost Centre Mapping is changed, Save Finance Setting.");
                e.Cancel = true;
            }
        }

        private void cbIncludeGSTInvoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbIncludeGSTInvoice.SelectedIndex > 0)
            {
                if (!chkEnableGST.Checked && (!this.AppSetting.IsCountryOtherThanIndia && this.AppSetting.AllowMultiCurrency == 0))
                {
                    MessageRender.ShowMessage("This Feature will be used only Enable GST is applicable");
                    //chkGSTVendorInvoceDetials.Checked = false;
                    cbIncludeGSTInvoice.SelectedIndex = 0;
                }
            }
        }
        #endregion


        private void btnAuditorNote_Click(object sender, EventArgs e)
        {
            frmAuditorSignNoteDetails frmauditornote = new frmAuditorSignNoteDetails(this);
            frmauditornote.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMapBudgetActual_Click(object sender, EventArgs e)
        {
            ResultArgs result = new ResultArgs();
            DataTable dtBudgetLedger = new DataTable();
            try
            {
                using (MappingSystem mapsytem = new MappingSystem())
                {
                    result = FetchProjectList();
                    if (result.Success && result.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtProject = resultArgs.DataSource.Table;

                        var projectIds = dtProject.AsEnumerable()
                          .Select(row => Convert.ToInt32(row["PROJECT_ID"]))
                          .Distinct()
                          .ToList();

                        foreach (int projectid in projectIds)
                        {
                            result = FetchBudgetActualLedgersDetails();
                            if (result.Success && result.DataSource.Table.Rows.Count > 0)
                            {
                                DataView dtView = new DataView(result.DataSource.Table);
                                dtView.RowFilter = "PROJECT_ID=" + projectid;

                                mapsytem.ProjectId = projectid;
                                // mapsytem.OpeningBalanceDate = this.AppSetting.BookBeginFrom;

                                dtBudgetLedger = dtView.ToTable();
                                mapsytem.dtBudgetActualLedger = dtBudgetLedger;

                                result = mapsytem.MapBudgetActualLedgers();

                            }
                        }

                        if (result.Success)
                        {
                            this.ShowMessageBox("Mapped Successfully");
                        }
                        else
                        {
                            this.ShowMessageBox("Nothing affected");
                        }
                    }
                    else
                    {
                        this.ShowMessageBox("No Project is exists");
                    }
                }
            }
            catch (Exception ex)
            {
                AcMELog.WriteLog(ex.Message);
            }
        }

        public ResultArgs FetchBudgetActualLedgersDetails()
        {
            using (MappingSystem mapsystem = new MappingSystem())
            {
                resultArgs = mapsystem.FetchActualBalanceforBudget();

            }
            return resultArgs;
        }

        public ResultArgs FetchProjectList()
        {
            using (MappingSystem mapsystem = new MappingSystem())
            {
                resultArgs = mapsystem.FetchProjectList();
            }
            return resultArgs;
        }
    }
}