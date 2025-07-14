using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Bosco.Utility;

namespace Bosco.Utility.ConfigSetting
{
    public class UISettingProperty : TransProperty
    {
        private static DataView dvUISetting = null;
        private const string SettingNameField = "Name";
        private const string SettingValueField = "Value";

        private static DataTable dtSignatureDetails = null;

        private string GetUISettingInfo(string name)
        {
            string val = "";
            try
            {
                if (dvUISetting != null && dvUISetting.Count > 0)
                {
                    for (int i = 0; i < dvUISetting.Count; i++)
                    {
                        string record = dvUISetting[i][SettingNameField].ToString();
                        if (name == record)
                        {
                            val = dvUISetting[i][SettingValueField].ToString();
                            break; //25/01/2025 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            return val;
        }

        public DataView UISettingInfo
        {
            set
            {
                UISettingProperty.dvUISetting = value;
            }
            get
            {
                return dvUISetting;
            }
        }

        public DataTable SignatureDetails
        {
            set
            {
                UISettingProperty.dtSignatureDetails = value;
            }
            get
            {
                return dtSignatureDetails;
            }
        }

        public string UILanguage
        {
            get
            {
                return GetUISettingInfo(UserSetting.UILanguage.ToString());
            }
        }

        public string UIDateFormat
        {
            get
            {
                return GetUISettingInfo(UserSetting.UIDateFormat.ToString());
            }
        }

        public string UIDateSeparator
        {
            get
            {
                return GetUISettingInfo(UserSetting.UIDateSeparator.ToString());
            }
        }

        public string UIThemes
        {
            get
            {
                return GetUISettingInfo(UserSetting.UIThemes.ToString());
            }
        }

        public string UITransClose
        {
            get
            {
                return GetUISettingInfo(UserSetting.UITransClose.ToString());
            }
        }

        public string UIVoucherPrint
        {
            get
            {
                return GetUISettingInfo(Setting.PrintVoucher.ToString());
            }
        }

        public string TransEntryMethod
        {
            get
            {
                return GetUISettingInfo(UserSetting.TransEntryMethod.ToString());
            }
        }

        public string TransMode
        {
            get
            {
                return GetUISettingInfo(UserSetting.UITransMode.ToString());
            }
        }
        public string UICustomizationForm
        {
            get
            {
                return GetUISettingInfo(UserSetting.CustomizationForm.ToString());
            }
        }
        public string UIDonationVoucherPrint
        {
            get
            {
                return GetUISettingInfo(UserSetting.UIDonationVoucherPrint.ToString());
            }
        }

        public string CustomizationForm
        {
            get
            {
                return GetUISettingInfo(Setting.CustomizationForm.ToString());
            }
        }
        public string ForeignBankAccount
        {
            get
            {
                return GetUISettingInfo(Setting.UIForeignBankAccount.ToString());
            }
        }
        public string UIProjSelection
        {
            get
            {
                return GetUISettingInfo(UserSetting.UIProjSelection.ToString());
            }
        }
        public string TDSEnabled
        {
            get
            {
                //On 28/02/2024, Hide TDS related features
                //return GetUISettingInfo(Setting.TDSEnabled.ToString());
                return SettingProperty.EnableTDS ? "1" : "0";
            }
        }

        public string EnableBookingAtPayment
        {
            get
            {
                return GetUISettingInfo(FinanceSetting.EnableBookingAtPayment.ToString());
            }
        }


        public string UIEnableBookingAtPayment
        {
            get
            {
                return GetUISettingInfo(UserSetting.EnableBookingAtPayment.ToString());
            }
        }

        public string EnableTransMode
        {
            get
            {
                return GetUISettingInfo(Setting.EnableTransMode.ToString());
            }
        }

        public string EnableVoucherRegenerationInsert
        {
            get
            {
                return GetUISettingInfo(FinanceSetting.EnableVoucherRegenerationInsert.ToString());
            }
        }

        public string EnableVoucherRegenerationDeletion
        {
            get
            {
                return GetUISettingInfo(FinanceSetting.EnableVoucherRegenerationDeletion.ToString());
            }
        }

        public string EnableNegativeBalance
        {
            get
            {
                return GetUISettingInfo(FinanceSetting.EnableNegativeBalance.ToString());
            }
        }

        public string UIEnableTransMode
        {
            get
            {
                return GetUISettingInfo(UserSetting.EnableTransMode.ToString());
            }
        }

        public string TDSBooking
        {
            get
            {
                return GetUISettingInfo(Setting.TDSBooking.ToString());
            }
        }
        public string UITDSBooking
        {
            get
            {
                return GetUISettingInfo(UserSetting.TDSBooking.ToString());
            }
        }
        public string VoucherPrint
        {
            get
            {
                return GetUISettingInfo(UserSetting.PrintVoucher.ToString());
            }
        }
        public string EnableGST
        {
            get
            {
                return GetUISettingInfo(FinanceSetting.EnableGST.ToString());
            }
        }
        public string IncludeGSTVendorInvoiceDetails
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.IncludeGSTVendorInvoiceDetails.ToString());
                value = (string.IsNullOrEmpty(value) ? "0" : value);
                return value;
            }
        }
        public string EnableChequePrinting
        {
            get
            {
                return GetUISettingInfo(FinanceSetting.EnableChequePrinting.ToString());
            }
        }
        public string EnableRefWiseReceiptANDPayment
        {
            get
            {
                return GetUISettingInfo(FinanceSetting.EnableRefWiseRecPayment.ToString());
            }
        }
        public string ExportVouchersBeforeClose
        {
            get
            {
                return GetUISettingInfo(FinanceSetting.ExportVouchersBeforeClose.ToString());
            }
        }

        public string DontAlertTakeBackupBeforeClose
        {
            get
            {
                return GetUISettingInfo(FinanceSetting.DontAlertTakeBackupBeforeClose.ToString());
            }
        }

        public string DuplicateCopyVoucherPrint
        {
            get
            {
                return GetUISettingInfo(FinanceSetting.DuplicateCopyVoucherPrint.ToString());
            }
        }

        public string TwoVouchersInOnePageVoucherPrint
        {
            get
            {
                return GetUISettingInfo(FinanceSetting.TwoVouchersInOnePageVoucherPrint.ToString());
            }
        }

        public string EnableFlexiFD
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.EnableFlexiFD.ToString());
                value = (string.IsNullOrEmpty(value) ? "0" : value);
                return value;
            }
        }

        public string ShowBudgetLedgerActualBalance
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.ShowBudgetLedgerActualBalance.ToString());
                value = (string.IsNullOrEmpty(value) ? "0" : value);
                return value;
            }
        }

        /// <summary>
        ///// On 31/07/2020, Not yet used, it will be used later to show ledger receipts balance, payment balance
        ///// </summary>
        //public string ShowBudgetLedgerSeparateReceiptPaymentActualBalance
        //{
        //    get
        //    {
        //        string value = GetUISettingInfo(FinanceSetting.ShowBudgetLedgerSeparateReceiptPaymentActualBalance.ToString());
        //        value = (string.IsNullOrEmpty(value) ? "0" : value);

        //        //Fix for CMF congregation
        //        if (new Bosco.Utility.ConfigSetting.SettingProperty().IS_CMF_CONGREGATION)
        //        {
        //            value = "1";
        //        }
        //        return value;
        //    }
        //}

        public string IncludeBudgetStatistics
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.IncludeBudgetStatistics.ToString());
                value = (string.IsNullOrEmpty(value) ? "0" : value);
                return value;
            }
        }

        public string IncludeIncomeLedgersInBudget
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.IncludeIncomeLedgersInBudget.ToString());
                value = (string.IsNullOrEmpty(value) ? "0" : value);
                return value;
            }
        }

        public string EnableSubLedgerBudget
        {
            get
            {
                string value = "1"; //GetUISettingInfo(FinanceSetting.EnableSubLedgerBudget.ToString());
                value = (string.IsNullOrEmpty(value) ? "0" : value);
                return value;
            }
        }

        public string EnableSubLedgerVouchers
        {
            get
            {
                string value = "1"; //GetUISettingInfo(FinanceSetting.EnableSubLedgerVouchers.ToString());
                value = (string.IsNullOrEmpty(value) ? "0" : value);
                return value;
            }
        }

        public double MaxCashLedgerAmountInReceiptsPayments
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.MaxCashLedgerAmountInReceiptsPayments.ToString());
                value = (string.IsNullOrEmpty(value) ? "0" : value);
                return NumberSet.ToDouble(value);
            }
        }

        public Int32 ShowCr_DrAmountDrillingLedgerInAbstract
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.ShowCr_DrAmountDrillingLedgerInAbstract.ToString());
                return NumberSet.ToInteger((string.IsNullOrEmpty(value) ? "0" : value));
            }
        }

        public Int32 ShowCCOpeningBalanceInReports
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.ShowCCOpeningBalanceInReports.ToString());
                return NumberSet.ToInteger((string.IsNullOrEmpty(value) ? "0" : value));
            }
        }

        /// <summary>
        /// # 11/05/2020, to enable or show reset opening balance feature
        /// </summary>
        public Int32 ShowResetLedgerOpeningBalance
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.ShowResetLedgerOpeningBalance.ToString());
                return NumberSet.ToInteger((string.IsNullOrEmpty(value) ? "0" : value));
            }
        }

        /// <summary>
        /// # 08/06/2020, to enable or disable - Madatory Cheque Number in Voucher entry screen
        /// </summary>
        public Int32 MandatoryChequeNumberInVoucherEntry
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.MandatoryChequeNumberInVoucherEntry.ToString());
                return NumberSet.ToInteger((string.IsNullOrEmpty(value) ? "0" : value));
            }
        }


        // Alert High Value Payment Entry if crossed above 50L
        public Int32 EnableCashBankJournal
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.EnableCashBankJournal.ToString());
                return NumberSet.ToInteger(value);
            }
        }

        // Alert High Value Payment Entry if crossed above 50L
        public Int32 EnableCCModeReports
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.EnableCCMode.ToString());
                return NumberSet.ToInteger(value);
            }
        }

        // Alert High Value Payment Entry if crossed above 50L
        public Int32 AlertHighValuePaymentEntry
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.AlertHighValuePayment.ToString());
                return NumberSet.ToInteger(value);
            }
        }

        public Int32 AlertLocalDonation
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.AlertLocalDonations.ToString());
                return NumberSet.ToInteger(value);
            }
        }

        /// <summary>
        /// # 08/06/2020, to enable or disable - Allow Duplicate Cheque Number in Voucher entry screen
        /// </summary>
        public Int32 DonotAllowDuplicateChequeNumberInVoucherEntry
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.DonotAllowDuplicateChequeNumberInVoucherEntry.ToString());
                return NumberSet.ToInteger((string.IsNullOrEmpty(value) ? "0" : value));
            }
        }

        /// <summary>
        /// On 15/02/2021, to show monthly ledger summary while drilling ledger amount from all Reports
        // For all the report, When we do ledger drilling from any report, we need to show Ledger monthly summary report bsaed on finance setting
        // If we drill monthly summary from the same report, it will show as usual ledger report, If we drill back, again it will show Monthly summary 
        /// </summary>
        public Int32 ShowMonthlySummaryDrillingReport
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.ShowMonthlySummaryDrillingReport.ToString());
                return NumberSet.ToInteger((string.IsNullOrEmpty(value) ? "0" : value));
            }
        }

        /// <summary>
        /// On 19/01/2022, Allocate cost center amount with GST for Ledger, 
        /// </summary>
        public Int32 AllocateCCAmountWithGST
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.AllocateCCAmountWithGST.ToString());
                return NumberSet.ToInteger((string.IsNullOrEmpty(value) ? "0" : value));
            }
        }

        /// <summary>
        /// 18/11/2022, To set CC Mapping Project Based or LedgerBased
        /// 0 - Project-wise(Default)                  1- Ledger-wise
        /// </summary>
        public Int32 CostCeterMapping
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.CostCeterMapping.ToString());
                return NumberSet.ToInteger((string.IsNullOrEmpty(value) ? "0" : value));
            }
        }



        //public LCBranchModuleStatus BranchReceiptModuleStatus
        //{
        //    get
        //    {
        //        Int32 value = NumberSet.ToInteger(GetUISettingInfo(FinanceSetting.BranchReceiptModuleStatus.ToString()));
        //        return (LCBranchModuleStatus)value;
        //    }
        //}



        /// <summary>
        /// On 03/03/2021, In Budget Annual Summary - New Project, 
        /// Few Clients asks us to name it as New Budget with income, expenditure and Province Help, but 
        /// Manfort asks us to name it as Development Projects and wanted few more extra details like local fund and govt fund
        /// </summary>
        public Int32 ConsiderBudgetNewProject
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.ConsiderBudgetNewProject.ToString());
                return NumberSet.ToInteger((string.IsNullOrEmpty(value) ? "0" : value));
            }
        }

        /// <summary>
        /// For FMA Congregation Reports
        /// On 19/11/2021, To Keep list of contribution from ledgers
        /// </summary>
        public string ContributionFromLedgers
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.ContributionFromLedgers.ToString());
                return (string.IsNullOrEmpty(value) ? "0" : value);
            }
        }

        /// <summary>
        /// For FMA Congregation Reports
        /// On 19/11/2021, To Keep list of contribution to ledgers
        /// </summary>
        public string ContributionToLedgers
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.ContributionToLedgers.ToString());
                return (string.IsNullOrEmpty(value) ? "0" : value);
            }
        }

        /// <summary>
        /// On 10/12/2021, To Keep list of Inter Account from ledgers
        /// </summary>
        public string InterAccountFromLedgers
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.InterAccountFromLedgers.ToString());
                return (string.IsNullOrEmpty(value) ? "0" : value);
            }
        }

        /// <summary>
        /// On 10/12/2021, To Keep list of Inter Account from ledgers
        /// </summary>
        public string InterAccountToLedgers
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.InterAccountToLedgers.ToString());
                return (string.IsNullOrEmpty(value) ? "0" : value);
            }
        }

        /// <summary>
        /// Receipt Voucher entry Natures
        /// On 21/11/2024, To Keep Natures of ledgers to be loaded in Receipt Voucher Etnry
        /// 0 - All Natures
        /// </summary>
        public string NatuersInReceiptVoucherEntry
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.NatuersInReceiptVoucherEntry.ToString());
                return (string.IsNullOrEmpty(value) ? "0" : value);
            }
        }

        /// <summary>
        /// Payment Voucher entry Natures
        /// On 21/11/2024, To Keep Natures of ledgers to be loaded in Receipt Voucher Etnry
        /// 0 - All Natures
        /// </summary>
        public string NatuersInPaymentVoucherEntry
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.NatuersInPaymentVoucherEntry.ToString());
                return (string.IsNullOrEmpty(value) ? "0" : value);
            }
        }

        //On 02/03/2023, For FMA Generalate Reports
        public double GeneralateOpeningIEBalance
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.GeneralateOpeningIEBalance.ToString());
                return (NumberSet.ToDouble(string.IsNullOrEmpty(value) ? "0" : value));
            }
        }

        //On 02/03/2023, For FMA Generalate Reports
        //0 - Operating Profit, 1 - Operating Loss
        public Int32 GeneralateOpeningIEBalanceMode
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.GeneralateOpeningIEBalanceMode.ToString());
                return NumberSet.ToInteger((string.IsNullOrEmpty(value) ? "0" : value));
            }
        }

        public Int32 EnableCostCentreBudget
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.EnableCostCentreBudget.ToString());
                return NumberSet.ToInteger((string.IsNullOrEmpty(value) ? "0" : value));
            }
        }

        public Int32 IncludeBudgetCCStrengthDetails
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.IncludeBudgetCCStrengthDetails.ToString());
                return NumberSet.ToInteger((string.IsNullOrEmpty(value) ? "0" : value));
            }
        }

        /// <summary>
        /// Attach Development Projects/New Projects with Report or Main Budget
        /// </summary>
        public Int32 CreateBudgetDevNewProjects
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.CreateBudgetDevNewProjects.ToString());
                return NumberSet.ToInteger((string.IsNullOrEmpty(value) ? "0" : value));
            }
        }

        public Int32 ConfirmAuthorizationVoucherEntry
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.ConfirmAuthorizationVoucherEntry.ToString());
                return NumberSet.ToInteger((string.IsNullOrEmpty(value) ? "0" : value));
            }
        }

        public Int32 EnableFDAdjustmentEntry
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.EnableFDAdjustmentEntry.ToString());
                return NumberSet.ToInteger((string.IsNullOrEmpty(value) ? "0" : value));
            }
        }

        public Int32 ShowBudgetApprovedAmountInMonthlyReport
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.ShowBudgetApprovedAmountInMonthlyReport.ToString());
                return NumberSet.ToInteger((string.IsNullOrEmpty(value) ? "0" : value));
            }
        }

        public Int32 ShowCashBankFDDetailLedgerInBudgetProposed
        {
            get
            {
                string value = GetUISettingInfo(FinanceSetting.ShowCashBankFDDetailLedgerInBudgetProposed.ToString());
                return NumberSet.ToInteger((string.IsNullOrEmpty(value) ? "0" : value));
            }
        }

        //public Int32 AllowMultiCurrency
        //{
        //    get
        //    {
        //        string value = GetUISettingInfo(FinanceSetting.AllowMultiCurrency.ToString());
        //        return NumberSet.ToInteger((string.IsNullOrEmpty(value) ? "0" : value));
        //    }
        //}

        /// <summary>
        /// On 18/06/2024, To Allow Cash and Bank zero value amount in Voucher Entry.
        /// Allow Zero valued cash bank voucher only for Double Entry 
        /// This Option will be enabled only for Double Entry option is enabled
        /// </summary>
        public bool AllowZeroValuedCashBankVoucherEntry
        {
            get
            {
                bool rtn = false;
                //Allow Zero valued cash bank voucher only for Double Entry  
                if (this.EnableTransMode == "1")
                {
                    string value = GetUISettingInfo(FinanceSetting.AllowZeroValuedCashBankVoucherEntry.ToString());
                    if (!string.IsNullOrEmpty(value) && value == "1")
                    {
                        rtn = true;
                    }
                }
                return rtn;
            }
        }

        ///// <summary>
        ///// On 02/08/2024, To attach Vocuher Files in Voucher Entry
        ///// </summary>
        //public Int32 AttachVoucherFiles
        //{
        //    get
        //    {
        //        string value = GetUISettingInfo(FinanceSetting.AttachVoucherFiles.ToString());
        //        return NumberSet.ToInteger((string.IsNullOrEmpty(value) ? "0" : value));
        //    }
        //}

        ////On 02/03/2023, For FMA Generalate Reports
        //public double GeneralateOpeningDepreciationBalance
        //{
        //    get
        //    {
        //        string value = GetUISettingInfo(FinanceSetting.GeneralateOpeningDepreciationBalance.ToString());
        //        return (NumberSet.ToDouble(string.IsNullOrEmpty(value) ? "0" : value));
        //    }
        //}


        public string AccountLedgerId
        {
            get
            {
                //  return GetUISettingInfo(AssetSetting.AccountLedgerId.ToString());
                return GetUISettingInfo(UserSetting.AccountLedgerId.ToString());
            }
        }

        public string DepreciationLedgerId
        {
            get
            {
                //  return GetUISettingInfo(AssetSetting.DepreciationLedgerId.ToString());
                return GetUISettingInfo(UserSetting.DepreciationLedgerId.ToString());
            }
        }

        public string Months
        {
            get
            {
                return GetUISettingInfo(UserSetting.Months.ToString());
            }
        }

        public string ProjSelection
        {
            get
            {
                return GetUISettingInfo(UserSetting.UIProjSelection.ToString());
            }
        }
        public string TransClose
        {
            get
            {
                return GetUISettingInfo(UserSetting.UITransClose.ToString());
            }
        }


        public string DisposalLedgerId
        {
            get
            {
                //  return GetUISettingInfo(AssetSetting.DisposalLedgerId.ToString());
                return GetUISettingInfo(UserSetting.DisposalLedgerId.ToString());
            }
        }

        // Show AMC Alert in Asset Configure
        public string ShowAMCRenewalAlert
        {
            get
            {
                //  return GetUISettingInfo(AssetSetting.ShowAMCRenewalAlert.ToString());
                return GetUISettingInfo(UserSetting.ShowAMCRenewalAlert.ToString());
            }
        }

        public string ShowDepr
        {
            get
            {
                //  return GetUISettingInfo(AssetSetting.ShowDepr.ToString());
                return GetUISettingInfo(UserSetting.ShowDepr.ToString());
            }
        }

        public string ShowInsuranceAlert
        {
            get
            {
                //   return GetUISettingInfo(AssetSetting.ShowInsuranceAlert.ToString());
                return GetUISettingInfo(UserSetting.ShowInsuranceAlert.ToString());
            }
        }

        public string ShowOpApplyFrom
        {
            get
            {
                return GetUISettingInfo(UserSetting.ShowOpApplyFrom.ToString());
            }
        }

        public string RoleName(Int32 signorder, Int32 ProjectId)
        {
            string rtn = string.Empty;
            if (dtSignatureDetails != null && dtSignatureDetails.Rows.Count > 0)
            {
                dtSignatureDetails.DefaultView.RowFilter = "";
                dtSignatureDetails.DefaultView.RowFilter = "SIGN_ORDER = " + signorder + " AND PROJECT_ID = " + ProjectId;
                if (dtSignatureDetails.DefaultView.Count > 0)
                {
                    rtn = dtSignatureDetails.DefaultView[0]["ROLE_NAME"].ToString();
                }
                dtSignatureDetails.DefaultView.RowFilter = "";
            }
            return rtn;
        }

        public string Role(Int32 signorder, Int32 ProjectId)
        {
            string rtn = string.Empty;
            if (dtSignatureDetails != null && dtSignatureDetails.Rows.Count > 0)
            {
                dtSignatureDetails.DefaultView.RowFilter = "";
                dtSignatureDetails.DefaultView.RowFilter = "SIGN_ORDER = " + signorder + " AND PROJECT_ID = " + ProjectId;
                if (dtSignatureDetails.DefaultView.Count > 0)
                {
                    rtn = dtSignatureDetails.DefaultView[0]["ROLE"].ToString();
                }
                dtSignatureDetails.DefaultView.RowFilter = "";
            }
            return rtn;
        }

        public byte[] SignImage(Int32 signorder, Int32 ProjectId)
        {
            byte[] SignImage = null;
            dtSignatureDetails.DefaultView.RowFilter = "";
            dtSignatureDetails.DefaultView.RowFilter = "SIGN_ORDER = " + signorder + " AND PROJECT_ID = " + ProjectId;
            if (dtSignatureDetails.DefaultView.Count > 0)
            {
                SignImage = (byte[])dtSignatureDetails.DefaultView[0]["SIGN_IMAGE"];
            }
            dtSignatureDetails.DefaultView.RowFilter = "";
            return SignImage;
        }
        // end

        #region IDisposable Members

        public override void Dispose()
        {
            //GC.Collect();
        }

        #endregion
    }
}
