///On 09/02/2018 DuplicateVoucher Ref_01 : To make duplication voucher (repeat selected voucher one more time), Logic is show selected voucher details edit mode, 
/// it will display existing voucher detail and make voucherid to 0 or change add mode...(Remove VoucherId column in Transaction grid and Cash/Bank Grid), it was desinged voucher id 
/// should not be there in Add/Mode

using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.Serialization;
using System.Linq;
using ACPP.Modules.Master;
using Bosco.Model.Business;
using Bosco.Model.Transaction;
using Bosco.Model.UIModel;
using Bosco.Utility;

using DevExpress.Utils.Frames;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Base;
using AcMEDSync.Model;
using ACPP.Modules.TDS;
using Bosco.Model.TDS;
using DevExpress.XtraEditors.Mask;
using Bosco.Model.UIModel.Master;
using Bosco.DAO.Schema;
using Bosco.Report.Base;
using DevExpress.XtraGrid.Views.Grid;
using Bosco.Utility.ConfigSetting;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ACPP.Modules.Transaction
{
    public partial class frmTransactionMultiAdd : frmFinanceBaseAdd
    {
        #region Declaration
        public event EventHandler UpdateHeld;
        public event EventHandler EditHeld;
        ResultArgs resultArgs = null;
        private DataSet dsCostCentre = new DataSet();
        private DataTable dtReferenceVoucherNumber = new DataTable();
        private DataTable dtReferenceValidation = new DataTable();
        private DataTable dtSubLedgerVouchers = new DataTable(); //07/02/2020, Sub Ledger Vouchers
        private DataSet dsDenomination = new DataSet();
        private string SINGLE_ENTRY = string.Empty; //"<b>Press <color=blue>F11</color> to Single Entry</b>";
        private string DOUBLE_ENTRY = string.Empty; //"<b>Press <color=blue>F11</color> to Multiple Entry</b>";
        DirtyControls clsDirty = null;
        private bool isInsertVoucher = false;
        private string PreviousVoucherNo = string.Empty;
        private string PreviousRunningDigit = string.Empty;
        private string InsertVoucherDate = "";
        private int EditVoucherIndex = 0;
        private int InsertVoucherIndex = 0;
        private DateTime EditVoucherDate = DateTime.Now;
        bool isMouseClicked = false;
        int BudgetTypeId = 0;
        int BudgetMonthDistribution = 0;
        private bool EnableMultiRow = false;
        public string CurrentLedgerTransMode { get; set; }

        //On 24/07/2023, Voucher Authorization details
        public int AuthorizedStatus { get; set; }

        //On 29/07/2024, Voucher Attached Images
        private DataTable dtVoucherImages = new DataTable();
        private enum AdditionButttons
        {
            VendorGSTInvoiceDetails,
            SubLedgerDetails
        }

        #endregion

        #region Property
        private DateTime deMaxDate { get; set; }

        //01/03/2018, keep date value (To show alert message if selected date is locked for the selected project)
        private DateTime dePreviousVoucherDate { get; set; }

        //On 09/02/2018, To make duplication entry or not(Ref_01)
        public bool DuplicateVoucher { get; set; }

        private int voucherId = 0;
        private int VoucherId
        {
            get { return voucherId; }
            set { voucherId = value; }
        }

        private int projectId = 0;
        private int ProjectId
        {
            set { projectId = value; }
            get { return projectId; }
        }

        private int divisionId = 0;
        private int DivisionId
        {
            set { divisionId = value; }
            get { return divisionId; }
        }

        private int contributionId = 0;
        private int ContributionId
        {
            set { contributionId = value; }
            get { return contributionId; }
        }

        private int budgetid = 0;
        private int BudgetId
        {
            set { budgetid = value; }
            get { return budgetid; }
        }

        private DateTime budgetDateFrom;
        private DateTime BudgetPeriodDateFrom
        {
            set { budgetDateFrom = value; }
            get { return budgetDateFrom; }
        }

        private string budgetprojectids = string.Empty;
        private string BudgetProjectIds
        {
            set { budgetprojectids = value; }
            get { return budgetprojectids; }
        }

        private DateTime budgetDateTo;
        private DateTime BudgetPeriodDateTo
        {
            set { budgetDateTo = value; }
            get { return budgetDateTo; }
        }

        private string projectName = string.Empty;
        private string ProjectName
        {
            set { projectName = value; }
            get { return projectName; }
        }

        private double CashTransSummaryVal
        {
            get { return this.UtilityMember.NumberSet.ToDouble(colCashAmount.SummaryItem.SummaryValue.ToString()); }
        }

        private double TransSummaryVal
        {
            get { return this.UtilityMember.NumberSet.ToDouble(colAmount.SummaryItem.SummaryValue.ToString()); }
        }

        /// <summary>
        /// This is to sum the GST Amount with Transaction (09.05.2019)
        /// </summary>
        private double TransGStSummaryVal
        {
            get { return this.UtilityMember.NumberSet.ToDouble(colAmount.SummaryItem.SummaryValue.ToString()) + UtilityMember.NumberSet.ToDouble(colGStAmt.SummaryItem.SummaryValue.ToString()); }
        }

        DefaultVoucherTypes voucherType = DefaultVoucherTypes.Receipt;
        private DefaultVoucherTypes VoucherType
        {
            set { voucherType = value; }
            get { return voucherType; }
        }

        private int DenominationLedgerId = 0;
        private int DenominationLedgerID
        {
            set { DenominationLedgerId = value; }
            get { return DenominationLedgerId; }
        }
        private int LedgerId
        {
            get
            {
                int ledgerId = 0;
                ledgerId = gvTransaction.GetFocusedRowCellValue(colLedger) != null ? this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetRowCellValue(gvTransaction.FocusedRowHandle, colLedger).ToString()) : 0;

                //02/12/2019, To set GST properties for selected Ledger for Receipts and Payments----
                IsgstEnabledLedgers = false;
                ledgerMappedDefaultGSTID = 0;
                if (ledgerId > 0)
                {
                    DataRowView rowSelectedLedger = rglkpLedger.GetRowByKeyValue(ledgerId) as DataRowView;
                    if (rowSelectedLedger != null)
                    {
                        if (rowSelectedLedger.Row.Table.Columns.Contains("IS_GST_LEDGERS") && rowSelectedLedger.Row.Table.Columns.Contains("GST_ID"))
                        {
                            if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableGST).Equals((int)YesNo.Yes)) //28/12/2019, to check applicable from 
                            {
                                if (dtTransactionDate.DateTime >= this.AppSetting.GSTStartDate)
                                {
                                    Int32 gstclassid = UtilityMember.NumberSet.ToInteger(rowSelectedLedger["GST_ID"].ToString());
                                    DateTime gstapplicablefrom = GetGSTApplicableFrom(gstclassid);
                                    IsgstEnabledLedgers = (UtilityMember.NumberSet.ToInteger(rowSelectedLedger["IS_GST_LEDGERS"].ToString()) == 1);
                                    if (dtTransactionDate.DateTime >= gstapplicablefrom)
                                    {
                                        ledgerMappedDefaultGSTID = gstclassid;
                                    }
                                    else
                                    {
                                        ledgerMappedDefaultGSTID = this.AppSetting.GSTZeroClassId;
                                    }
                                }
                            }
                        }
                    }
                }
                //-----------------------------------------------------------------------------------
                return ledgerId;
            }
        }

        //02/12/2019, 
        bool IsgstEnabledLedgers;
        private bool IsGSTEnabledLedgers
        {
            get
            {
                if (LedgerId == 0)
                {
                    IsgstEnabledLedgers = false;
                }
                return IsgstEnabledLedgers;
            }
        }

        //02/12/2019, 
        int ledgerMappedDefaultGSTID;
        private int LedgerMappedDefaultGSTID
        {
            get
            {
                if (LedgerId == 0)
                {
                    ledgerMappedDefaultGSTID = 0;
                }
                return ledgerMappedDefaultGSTID;
            }
        }

        //29/11/2019, previous ledger id
        int previousLedgerId = 0;
        private int PreviousLedgerId
        {
            get
            {
                return previousLedgerId;
            }
            set
            {
                previousLedgerId = value;
            }
        }

        //29/11/2019, To set Ledger GST Ledger Class
        private int LedgerGSTClassId
        {
            get
            {
                int ledgerGSTclassId = 0;
                ledgerGSTclassId = gvTransaction.GetFocusedRowCellValue(colGSTLedgerClass) != null ? this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetRowCellValue(gvTransaction.FocusedRowHandle, colGSTLedgerClass).ToString()) : 0;
                return ledgerGSTclassId;
            }
            set
            {
                gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colGSTLedgerClass, value);
            }
        }

        //28/12/2019, To set Ledger GST Ledger Class
        private DateTime LedgerGSTClassApplicable
        {
            get
            {
                DateTime ledgerGSTclassapplicable = this.AppSetting.GSTStartDate;
                DataRowView drv = rglkpLedgerGST.GetRowByKeyValue(LedgerGSTClassId) as DataRowView;
                if (drv != null)
                {
                    ledgerGSTclassapplicable = UtilityMember.DateSet.ToDate(drv["APPLICABLE_FROM"].ToString(), false);
                }
                return ledgerGSTclassapplicable;
            }
        }

        //07/02/2020, Check Sub Ledger Exists for selected Ledger
        private bool IsSubLedgerExists
        {
            get
            {
                bool rtn = false;
                dtSubLedgerVouchers.DefaultView.RowFilter = "";

                if (LedgerId > 0 && dtSubLedgerVouchers != null && dtSubLedgerVouchers.Rows.Count > 0)
                {
                    dtSubLedgerVouchers.DefaultView.RowFilter = "LEDGER_ID=" + LedgerId;
                    rtn = (dtSubLedgerVouchers.DefaultView.Count > 0);
                }

                dtSubLedgerVouchers.DefaultView.RowFilter = "";
                return rtn;
            }
        }

        //On 28/02/2020, to selected ledger's get sub ledger amont
        private double SubLedgerAmount
        {
            get
            {
                double dsubledgeramount = 0;
                if (dtSubLedgerVouchers != null && dtSubLedgerVouchers.Rows.Count > 0)
                {
                    dsubledgeramount = UtilityMember.NumberSet.ToDouble(dtSubLedgerVouchers.Compute("SUM(AMOUNT)", "LEDGER_ID=" + LedgerId).ToString());
                }
                return dsubledgeramount;
            }
        }

        private int TransModeId
        {
            get
            {
                int SourceId = 0;
                SourceId = gvTransaction.GetFocusedRowCellValue(colSource) != null ? this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetRowCellValue(gvTransaction.FocusedRowHandle, colSource).ToString()) : 0;
                return SourceId;
            }
        }

        private double LedgerAmount
        {
            get
            {
                double ledgerAmount;
                ledgerAmount = gvTransaction.GetFocusedRowCellValue(colAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvTransaction.GetFocusedRowCellValue(colAmount).ToString()) : 0;
                return ledgerAmount;
            }
        }

        private int CashLedgerId
        {
            get
            {
                int cashLedgerId = 0;
                cashLedgerId = gvBank.GetRowCellValue(gvBank.FocusedRowHandle, colCashLedgerId) != null ? this.UtilityMember.NumberSet.ToInteger(gvBank.GetRowCellValue(gvBank.FocusedRowHandle, colCashLedgerId).ToString()) : 0;
                return cashLedgerId;
            }
        }

        private int transVoucherMethod = 0;
        private int TransVoucherMethod
        {
            set
            {
                transVoucherMethod = value;
            }
            get
            {
                return transVoucherMethod;
            }
        }

        private VoucherEntryMethod transEntryMethod = VoucherEntryMethod.Single;
        private VoucherEntryMethod TransEntryMethod
        {
            set
            {
                DataTable dtTrans = gcTransaction.DataSource as DataTable;
                DataTable dtCashTrans = gcBank.DataSource as DataTable;
                if (value == VoucherEntryMethod.Multi)
                {
                    ucAdditionalInfo.EntryCaption = SINGLE_ENTRY;
                    transEntryMethod = value;
                    EntryForm = value;
                }
                else
                {
                    bool result = true;
                    if (dtTrans.Rows.Count > 1 || dtCashTrans.Rows.Count > 1)
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.ENTRIES_WILL_BE_CLEARED), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            result = false;
                        }
                    }

                    if (result)
                    {
                        object[] objFirstTrans = null;
                        object[] objCashTransFirst = null;

                        //switch over from muti to single
                        if (dtTrans.Rows.Count > 0 || dtCashTrans.Rows.Count > 0)
                        {
                            if (dtTrans.Rows.Count > 1)
                            {
                                objFirstTrans = dtTrans.Rows[0].ItemArray;
                                dtTrans.Clear();
                            }
                            if (dtCashTrans.Rows.Count > 1)
                            {
                                objCashTransFirst = dtCashTrans.Rows[0].ItemArray;
                                dtCashTrans.Clear();
                            }
                        }

                        if (objFirstTrans != null) dtTrans.Rows.Add(objFirstTrans);
                        gcTransaction.DataSource = dtTrans;

                        if (objCashTransFirst != null) dtCashTrans.Rows.Add(objCashTransFirst);
                        gcBank.DataSource = dtCashTrans;

                        ucAdditionalInfo.EntryCaption = DOUBLE_ENTRY;
                        transEntryMethod = value;
                        TransacationGridNewItem = transEntryMethod;
                        CashBankTransGridNewItem = transEntryMethod;
                        EntryForm = VoucherEntryMethod.Single;
                    }
                }
            }
            get
            {
                return transEntryMethod;
            }
        }

        private VoucherEntryMethod EntryForm
        {
            set
            {
                if (value == VoucherEntryMethod.Single)
                {
                    emptysizing.Visibility = LayoutVisibility.Always;
                    gvTransaction.OptionsView.ShowFooter = false;
                    gvBank.OptionsView.ShowFooter = false;
                    lciTrans.Height = 58;
                    lciBank.Height = 47;
                }
                else
                {
                    emptysizing.Visibility = LayoutVisibility.Never;
                    gvTransaction.OptionsView.ShowFooter = true;
                    gvBank.OptionsView.ShowFooter = true;
                }
                if (TransVoucherMethod == 1) { FocusTransactionGrid(); }
                else { txtVoucher.Select(); txtVoucher.Focus(); }
            }
        }

        private VoucherEntryMethod TransacationGridNewItem
        {
            set
            {
                if (value == VoucherEntryMethod.Multi)
                {
                    DataTable dtTransaction = gcTransaction.DataSource as DataTable;

                    dtTransaction.Rows.Add(dtTransaction.NewRow());

                    gcTransaction.DataSource = dtTransaction;
                    int sourceId = (rgTransactionType.SelectedIndex == 0 || rgTransactionType.SelectedIndex == 2) ? (int)Source.To : (int)Source.By;
                    gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colSource, sourceId);
                    gvTransaction.FocusedColumn = (this.AppSetting.EnableTransMode == "1") ? colSource : colLedger;
                    gvTransaction.ShowEditor();
                }
            }
        }

        private VoucherEntryMethod CashBankTransGridNewItem
        {
            set
            {
                if (value == VoucherEntryMethod.Multi)
                {
                    DataTable dtCashTransaction = gcBank.DataSource as DataTable;
                    dtCashTransaction.Rows.Add(dtCashTransaction.NewRow());
                    gcBank.DataSource = dtCashTransaction;
                    gvBank.MoveNext();
                    int sourceId = (rgTransactionType.SelectedIndex == 0 || rgTransactionType.SelectedIndex == 2) ? (int)Source.By : (int)Source.To;
                    gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCashSource, sourceId);
                    gvBank.FocusedColumn = colCashLedger;
                    gvBank.ShowEditor();
                }
            }
        }

        private double CashLedgerAmount
        {
            get
            {
                double cashLedgerAmount;
                cashLedgerAmount = gvBank.GetFocusedRowCellValue(colCashAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvBank.GetFocusedRowCellValue(colCashAmount).ToString()) : 0;
                return cashLedgerAmount;
            }
        }

        int groupId = 0;
        private int GroupId
        {
            get { return groupId; }
            set { groupId = value; }
        }

        private int TDSGroupId { get; set; }

        private int LedgerGroupId
        {
            get
            {
                int GroupId = 0;
                if (LedgerId > 0)
                {
                    DataRowView dv = rglkpLedger.GetRowByKeyValue(LedgerId) as DataRowView;
                    if (dv != null)
                        GroupId = this.UtilityMember.NumberSet.ToInteger(dv.Row["Group_ID"].ToString());
                }
                return GroupId;
            }
        }

        private int CashBankGroupId
        {
            get
            {
                int GroupId = 0;
                if (CashLedgerId > 0)
                {
                    DataRowView dv = rglkpCashLedger.GetRowByKeyValue(CashLedgerId) as DataRowView;
                    if (dv != null)
                        GroupId = this.UtilityMember.NumberSet.ToInteger(dv.Row["Group_ID"].ToString());
                }
                return GroupId;
            }
        }

        //Confirmation while changing the transaction type in edit mode
        private int voucherTypeId = 0;
        private int VoucherTypeId
        {
            get { return voucherTypeId; }
            set { voucherTypeId = value; }
        }

        private string recentVoucherDate = string.Empty;
        private string RecentVoucherDate
        {
            set
            {
                recentVoucherDate = value;
            }
            get
            {
                return recentVoucherDate;
            }
        }

        private int EditVoucherDefinitionid = 0;

        //private Int32 voucherdefinitionid = 0;
        private bool EnableLedgerNarration = false;
        private Int32 VoucherDefinitionId
        {
            set
            {
                Int32 voucherdefinitionid = value;

                //On 28/01/2019, Get name of the voucher type and assign into option button (except default vouchers)
                //if (voucherdefinitionid > 4)
                //{
                using (VoucherSystem vouchersystem = new VoucherSystem())
                {
                    vouchersystem.VoucherId = voucherdefinitionid;
                    ResultArgs result = vouchersystem.VoucherDetailsById();
                    if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtVoucherTypeDetail = result.DataSource.Table;
                        Int32 basevouchertype = vouchersystem.NumberSet.ToInteger(dtVoucherTypeDetail.Rows[0][vouchersystem.AppSchema.Voucher.VOUCHER_TYPEColumn.ColumnName].ToString());

                        rgTransactionType.SelectedIndex = (basevouchertype == (int)DefaultVoucherTypes.Receipt ? 0 : basevouchertype == (int)DefaultVoucherTypes.Payment ? 1 : 2);
                        rgTransactionType.Properties.Items[rgTransactionType.SelectedIndex].Description = dtVoucherTypeDetail.Rows[0][vouchersystem.AppSchema.Voucher.VOUCHER_NAMEColumn.ColumnName].ToString();
                        int VoucherEnabled = vouchersystem.NumberSet.ToInteger(dtVoucherTypeDetail.Rows[0][vouchersystem.AppSchema.Voucher.IS_NARRATION_ENABLEDColumn.ColumnName].ToString());
                        EnableLedgerNarration = VoucherEnabled == 0 ? false : true;
                        rgTransactionType.Properties.Items[rgTransactionType.SelectedIndex].Value = voucherdefinitionid;
                    }
                }
                //}
            }
            get
            {
                Int32 voucherdefinitionid = rgTransactionType.SelectedIndex + 1;
                if (rgTransactionType.Properties.Items[rgTransactionType.SelectedIndex].Value != null)
                {
                    voucherdefinitionid = this.UtilityMember.NumberSet.ToInteger(rgTransactionType.Properties.Items[rgTransactionType.SelectedIndex].Value.ToString());
                }
                return voucherdefinitionid;
            }
        }


        /// <summary>
        /// On 24/07/2023, based on the setting, alert and get the confirmation
        /// </summary>
        private int ConfirmVoucherAuthorization
        {
            get
            {
                int rnt = AuthorizedStatus;
                if (VoucherId == 0 && AppSetting.ConfirmAuthorizationVoucherEntry == 1)
                {
                    string msg = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.IS_AUTHORIZED);  // "Is this Voucher Authorized ?";
                    if (this.ShowConfirmationMessage(msg, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        rnt = (int)Status.Active;
                    }
                }

                return rnt;
            }
        }

        //On 01/09/2023, To assign booked gst invoices id
        private int BookingGSTInvoiceId
        {
            get
            {
                int bookingGSTInvoiceId = 0;
                if (lgGSTInvoce.Visibility == LayoutVisibility.Always)
                {
                    bookingGSTInvoiceId = lkpGSTInvoices.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(lkpGSTInvoices.EditValue.ToString()) : 0;
                }
                return bookingGSTInvoiceId;
            }
            set
            {
                lkpGSTInvoices.EditValue = value;
            }
        }

        /// <summary>
        /// 19/06/2024, To allow cash and bank zero valued only for Receipt and Payment
        /// </summary>
        private bool AllowZeroValuedCashBankLedger
        {
            get
            {
                bool allowzerovaluedledger = (this.AppSetting.AllowZeroValuedCashBankVoucherEntry &&
                                    (rgTransactionType.SelectedIndex == 0 || rgTransactionType.SelectedIndex == 1));
                return allowzerovaluedledger;
            }
        }


        /// <summary>
        /// Onm 19/08/2024, To check currency based voucher details
        /// </summary>
        private bool IsCurrencyEnabledVoucher
        {
            get
            {
                Int32 currencycountry = VoucherCurrencyCountryId;
                double currencyamt = UtilityMember.NumberSet.ToDouble(txtCurrencyAmount.Text);
                double exchagnerate = UtilityMember.NumberSet.ToDouble(txtExchangeRate.Text);
                double liveexchangerate = UtilityMember.NumberSet.ToDouble(lblLiveExchangeRate.Text);
                double actalamount = UtilityMember.NumberSet.ToDouble(txtActualAmount.Text);

                return (currencycountry > 0 && currencyamt > 0 && exchagnerate > 0 && actalamount > 0 && liveexchangerate > 0);
            }
        }

        /// <summary>
        /// Onm 11/11/2024, To Voucher Currency Country
        /// </summary>
        private Int32 VoucherCurrencyCountryId
        {
            get
            {
                Int32 currencycountry = glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString());
                return currencycountry;
            }
        }

        private bool IsGeneralInvolice
        {
            get
            {
                bool rtn = (this.AppSetting.IsCountryOtherThanIndia || this.AppSetting.AllowMultiCurrency == 1);
                if (rtn)
                {
                    if (this.AppSetting.AllowMultiCurrency == 1)
                        rtn = (VoucherCurrencyCountryId == UtilityMember.NumberSet.ToInteger(this.AppSetting.Country));
                    else if (this.AppSetting.IsCountryOtherThanIndia)
                        rtn = true;
                }

                //for temp - to local against invoice amount
                //rtn = false;
                return rtn;
            }
        }

        #endregion

        #region GstProperty
        public double gstCalcAmount = 0.0;
        public double cgstCalcAmount = 0.0;
        public double sgstCalcAmount = 0.0;
        public double igstCalcAmount = 0.0;

        public double UpdateGST = 0.0;

        private Int32 GSTInvoiceId { get; set; }
        private string GSTVendorInvoiceNo { get; set; }
        private string GSTVendorInvoiceDate { get; set; }
        private Int32 GSTVendorInvoiceType { get; set; }
        private Int32 GSTVendorId { get; set; }
        private DataTable DtGSTInvoiceMasterDetails = null;
        private DataTable DtGSTInvoiceMasterLedgerDetails = null;

        private bool CanShowVendorGST
        {
            get
            {
                bool rtn = false;
                using (VoucherTransactionSystem vsystem = new VoucherTransactionSystem())
                {
                    if (colGStAmt.Visible && this.AppSetting.IncludeGSTVendorInvoiceDetails == "1")
                    {
                        DataTable dtVoucherTrans = gcTransaction.DataSource as DataTable;
                        if (dtVoucherTrans != null)
                        {
                            DataTable dtGSTLedgers = dtVoucherTrans.DefaultView.ToTable();
                            //string gstledgers = vsystem.AppSchema.VoucherTransaction.LEDGER_GST_CLASS_IDColumn.ColumnName + ">0 AND " +
                            //                    vsystem.AppSchema.Ledger.IS_GST_LEDGERSColumn.ColumnName + " = 1";

                            string gstledgers = vsystem.AppSchema.Ledger.IS_GST_LEDGERSColumn.ColumnName + " = 1";
                            dtGSTLedgers.DefaultView.RowFilter = gstledgers;
                            rtn = (dtGSTLedgers.DefaultView.Count > 0);

                            /*//On 20/10/2023 Allow GST Invoice always without GST Amount
                            double cgst = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(CGST)", "").ToString());
                            double sgst = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(SGST)", "").ToString());
                            double igst = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(IGST)", "").ToString());
                            rtn = (cgst > 0 || sgst > 0 || igst > 0); */


                        }
                    }
                }
                return rtn;
            }
        }
        #endregion

        #region TDS Properties
        private int CashBankId { get; set; }
        private int TDSPaymentId { get; set; }
        private int PaymentBookingVoucherId { get; set; }
        private string ClientCode { get; set; }
        private DataTable dtTDSPaymentInfo { get; set; }
        private int InterestLedgerId { get; set; }
        private int PenaltyLedgerId { get; set; }
        private double InterestAmount { get; set; }
        private double PenaltyAmount { get; set; }
        private double TDSPaymentAmount { get; set; }
        private double TDSPrevTaxAmount { get; set; }
        #endregion

        #region TDS Party Payment Properties
        private int TDSPartyBankId { get; set; }
        private int TDSPartyPaymentId { get; set; }
        #endregion

        #region Active Donor Properties
        private int DonorId { get; set; }
        #endregion

        #region Voucher Lock
        private DateTime dtLockDateFrom { get; set; }
        private DateTime dtLockDateTo { get; set; }
        #endregion

        #region ReceiptModule Enable/Track Properties
        private Int32 TrackVoucherId { get; set; }
        private DateTime TrackVoucherDate { get; set; }
        private string TrackVoucherNo { get; set; }
        private Int32 TrackVoucherProjectId { get; set; }
        private DefaultVoucherTypes TrackVoucherType { get; set; }
        private string TrackVoucherSubType { get; set; }
        private double TrackVoucherAmount { get; set; }
        #endregion

        #region Constructor
        public frmTransactionMultiAdd()
        {
            InitializeComponent();

            this.Location = new Point(0, 0);
            this.Size = new System.Drawing.Size(Screen.PrimaryScreen.WorkingArea.Width,
                            Screen.PrimaryScreen.WorkingArea.Height);
            gvTransaction.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            gvBank.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            RealColumnEditTransAmount();
            RealColumnEditCashTransAmount();
        }

        public frmTransactionMultiAdd(int voucherId)
            : this()
        {
            using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem(voucherId))
            {
                ProjectId = voucherSystem.ProjectId;
                VoucherId = voucherSystem.VoucherId;
                ProjectName = voucherSystem.ProjectName;

                if (!this.LoginUser.IsFullRightsReservedUser)
                {
                    if (CommonMethod.ApplyUserRights((int)Receipt.EditReceiptVoucher) != 0 && voucherSystem.VoucherType == "RC")
                    {
                        rgTransactionType.SelectedIndex = VoucherTypeId = 0;
                    }
                    else if (CommonMethod.ApplyUserRights((int)Payment.EditPaymentVoucher) != 0 && voucherSystem.VoucherType == "PY")
                    {
                        rgTransactionType.SelectedIndex = VoucherTypeId = 1;
                    }
                    else if (CommonMethod.ApplyUserRights((int)Contra.EditContraVoucher) != 0 && voucherSystem.VoucherType == "CN")
                    {
                        rgTransactionType.SelectedIndex = VoucherTypeId = 2;
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    rgTransactionType.SelectedIndex = VoucherTypeId = (voucherSystem.VoucherType == "RC") ? 0 : (voucherSystem.VoucherType == "PY") ? 1 : 2;
                }
            }
        }

        public frmTransactionMultiAdd(string RecVoucherDate, int ProjId, string PrjName, int voucherId, int voucherIndex, bool duplicatevoucher = false, Int32 voucherdefinitionid = 0)
            : this()
        {
            RecentVoucherDate = RecVoucherDate;
            ProjectId = ProjId;
            ProjectName = PrjName;
            VoucherId = voucherId;
            VoucherDefinitionId = voucherdefinitionid;
            rgTransactionType.SelectedIndex = VoucherTypeId = voucherIndex;


            //To make duplication voucher entry(Ref_01)
            if (duplicatevoucher)
            {
                DuplicateVoucher = duplicatevoucher;
            }
        }

        public frmTransactionMultiAdd(string insertVoucherDate, int ProjId, string PrjName, int voucherId, int voucherIndex, string Vno, string runningDigit, Int32 voucherdefinitionid = 0)
            : this()
        {
            isInsertVoucher = true;
            RecentVoucherDate = InsertVoucherDate = insertVoucherDate;
            ProjectId = ProjId;
            ProjectName = PrjName;
            VoucherId = voucherId;
            VoucherDefinitionId = voucherdefinitionid;
            rgTransactionType.SelectedIndex = VoucherTypeId = InsertVoucherIndex = voucherIndex;
            PreviousVoucherNo = Vno;
            PreviousRunningDigit = runningDigit;
        }

        public frmTransactionMultiAdd(int ProjId, string PrjName, int voucherId, int voucherIndex, int ledgerId, int fdAccountId)
            : this()
        {
            ProjectId = ProjId;
            ProjectName = PrjName;
            VoucherId = voucherId;
            rgTransactionType.SelectedIndex = voucherIndex;
            rgTransactionType.Properties.Items[0].Enabled = rgTransactionType.Properties.Items[1].Enabled = false;
        }
        #endregion

        #region Events

        #region Form Events
        void RealColumnEditTransAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            BalanceProperty BudgetAmount;
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvTransaction.PostEditor();
            gvTransaction.UpdateCurrentRow();
            if (gvTransaction.ActiveEditor == null)
            {
                gvTransaction.ShowEditor();
            }

            TextEdit txtTransAmount = edit as TextEdit;
            int grpCounts = (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.DecimalPlaces) + 1);
            if (txtTransAmount.Text.Length > grpCounts && txtTransAmount.SelectionLength == txtTransAmount.Text.Length)
                txtTransAmount.Select(txtTransAmount.Text.Length - grpCounts, 0);
            if (LedgerId > 0)
            {
                int NatureId = GetNatureId();

                if (!this.AppSetting.IS_CMF_SLA || ((NatureId == (int)Natures.Assert || NatureId == (int)Natures.Libilities))) // Chinna  03.06.2021
                {
                    DataTable dtTrans = gcTransaction.DataSource as DataTable;
                    string Balance = GetLedgerBalanceValues(dtTrans, LedgerId); //ShowLedgerBalance(LedgerId, dtTrans, true);
                    if (Balance != string.Empty) { gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colLedgerBal, Balance); }
                }
                else
                {
                    //  if (BudgetId > 0 && LedgerId > 0)// && BudgetAmount.Amount > 0) // 07.06.2021
                    if (LedgerId > 0)// && BudgetAmount.Amount > 0)
                    {
                        //DataTable dtTrans = gcTransaction.DataSource as DataTable;
                        //string Balance = CalculateBudget(LedgerId); //ShowLedgerBalance(LedgerId, dtTrans, true);
                        //string[] BalanceData = Balance.Split('/');
                        //string Balances = BalanceData[0].ToString();

                        //BalanceProperty Balance = FetchBudgetLedgerBalance(LedgerId);
                        //double BalanceValues = Balance.Amount;

                        string Bal = GetCurrentBalanceYearIEValues(LedgerId);

                        if (Bal != string.Empty) { gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colLedgerBal, Bal); }
                    }
                }

            }
            // By Aldrin
            BudgetAmount = FetchBudgetAmount(LedgerId);
            if (BudgetId > 0 && LedgerId > 0)  // && BudgetAmount.Amount > 0)
            {
                string Balance = string.Empty;
                //if (this.AppSetting.IS_CMF_SLA) // Chinna  03.06.2021
                //{
                DataTable dtTrans = gcTransaction.DataSource as DataTable;
                Balance = CalculateBudget(LedgerId); //ShowLedgerBalance(LedgerId, dtTrans, true);
                //}
                //else
                //{
                //    Balance = GetBudgetBalanceYearIEValues(LedgerId); // chinna 04.06.2021
                //}
                if (Balance != string.Empty) { gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colBudgetAmount, Balance); }
            }
            // Included Gst updating value while changes Trans Amount (09.05.2019)
            //AssignGSTAmount(LedgerId)
            AssignGSTAmount(LedgerGSTClassId); //29/11/2019, To set Ledger GST Ledger Class
            CalculateFirstRowValue();

        }

        void RealColumnEditCashTransAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null) return;
            gvBank.PostEditor();
            gvBank.UpdateCurrentRow();
            if (gvBank.ActiveEditor == null)
                gvBank.ShowEditor();

            TextEdit txtTransAmount = edit as TextEdit;
            int grpCounts = (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.DecimalPlaces) + 1);
            if (txtTransAmount.Text.Length > grpCounts && txtTransAmount.SelectionLength == txtTransAmount.Text.Length)
                txtTransAmount.Select(txtTransAmount.Text.Length - grpCounts, 0);

            if (CashLedgerId > 0)
            {
                DataTable dtCashTrans = gcBank.DataSource as DataTable;
                string Balance = GetLedgerBalanceValues(dtCashTrans, CashLedgerId); //ShowLedgerBalance(CashLedgerId, dtCashTrans, false);
                if (Balance != string.Empty)
                {

                    gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colLedgerBalance, Balance);
                }
            }
        }

        private void frmTransactionMultiAdd_Load(object sender, EventArgs e)
        {
            //On 13/08/2024, Apply Project Currency Setting
            /*this.ApplyProjectCurrencySetting(ProjectId);
            rtxtAmount.Mask.Culture = System.Threading.Thread.CurrentThread.CurrentUICulture;
            rtxtAmount.Mask.UseMaskAsDisplayFormat = true;
            rtxtCashAmount.Mask.Culture = System.Threading.Thread.CurrentThread.CurrentUICulture;
            rtxtCashAmount.Mask.UseMaskAsDisplayFormat = true;*/

            SINGLE_ENTRY = "<b>" + this.GetMessage(MessageCatalog.Transaction.VocherTransaction.PRESS) + "<color=blue>F11</color>" + this.GetMessage(MessageCatalog.Transaction.VocherTransaction.TO_SINGLE_ENTRY) + "</b>";
            DOUBLE_ENTRY = "<b>" + this.GetMessage(MessageCatalog.Transaction.VocherTransaction.PRESS) + " <color=blue>F11</color> " + this.GetMessage(MessageCatalog.Transaction.VocherTransaction.TO_DOUBLE_ENTRY) + "</b>";
            if (EditHeld != null)
            {
                EditHeld(this, e);
            }

            LoadTransBasic();
            if (VoucherId != 0)
            {
                ucRightShortcut.DisableProject = BarItemVisibility.Never;
            }

            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                ApplyRights();
                ApplyRightsForShortCutKeys();
                ApplyUserRightsForDeletion();
            }

            //On 02/08/2024, To attach voucher images
            ucAdditionalInfo.DiableVoucherBills = BarItemVisibility.Never;
            lcAttachVocuherImages.Visibility = LayoutVisibility.Never;
            if (this.AppSetting.AttachVoucherFiles == 1)
            {
                ucAdditionalInfo.DiableVoucherBills = BarItemVisibility.Never;
                lcAttachVocuherImages.Visibility = LayoutVisibility.Always;
                btnAttachVocuherImages.TabIndex = 15;
            }

            //ucRightShortcut.DisableLedgerAdd = BarItemVisibility.Never;
            //clsDirty = new DirtyControls(this);
            //clsDirty.MarkAsClean();

            //04/04/2022, To Enable/Disable
            EnforceReceiptModule();

            //On 10/01/2023, For SDBINB, they wanted to lock few fields when GST invoice vendor details exists        
            EnforceLockingforGSTInvoiceVouchers();
            //ShowDonorAdditionalInfo(); //On 07/09/2023, to fix bank height when include GST invoice list

            enforceCurrencyDetails(false);
            if (this.AppSetting.AllowMultiCurrency == 1 || lciDonorInfo.Visibility == LayoutVisibility.Always)
            {
                colLedgerBalance.Width = 250;
                enforceCurrencyDetails(true);
            }

            //On 15/11/2024
            colLedgerBal.Visible = false;
            colLedgerBalance.Visible = false;
        }

        private void enforceCurrencyDetails(bool visible)
        {
            //On 12/09/2024, To enable currency details
            lcCurrency.Visibility = lcCurrencyAmount.Visibility = lblDonorCurrency.Visibility = (this.AppSetting.AllowMultiCurrency == 1 || visible ? LayoutVisibility.Always : LayoutVisibility.Never);
            lcExchangeRate.Visibility = lblCalculatedAmtCaption.Visibility = lblCalculatedAmt.Visibility = (this.AppSetting.AllowMultiCurrency == 1 || visible ? LayoutVisibility.Always : LayoutVisibility.Never);
            lcActualAmount.Visibility = (this.AppSetting.AllowMultiCurrency == 1 || visible ? LayoutVisibility.Always : LayoutVisibility.Never);
            lcLiveExchangeRate.Visibility = (this.AppSetting.AllowMultiCurrency == 1 || visible ? LayoutVisibility.Always : LayoutVisibility.Never);
            if (this.AppSetting.AllowMultiCurrency == 0)
            {
                lcCurrencyEmptySpace.Height = 25;
            }
        }

        private void LoadVoucherNo()
        {
            string vType = string.Empty;
            string pId = string.Empty;

            //On 03/04/2024, To show inserted voucher number as proposed number
            if (isInsertVoucher)
            {
                txtVoucher.Text = PreviousVoucherNo;
            }
            else
            {
                using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                {
                    voucherTransaction.VoucherType = rgTransactionType.SelectedIndex == 0 ? VoucherSubTypes.RC.ToString() : rgTransactionType.SelectedIndex == 1 ? VoucherSubTypes.PY.ToString() : VoucherSubTypes.CN.ToString();
                    voucherTransaction.ProjectId = ProjectId;
                    voucherTransaction.VoucherDate = this.UtilityMember.DateSet.ToDate(dtTransactionDate.Text, false);
                    voucherTransaction.VoucherDefinitionId = VoucherDefinitionId;
                    txtVoucher.Text = voucherTransaction.TempVoucherNo();
                }
            }
        }

        private void ApplyUserRightsForDeletion()
        {
            if (VoucherTypeId == 0)
            {
                bbiDeleteTrans.Visibility = bbiDeleteCashBank.Visibility = CommonMethod.ApplyUserRightsForTransaction((int)Receipt.DeleteReceiptVoucher) != 0 ? BarItemVisibility.Always : BarItemVisibility.Never;
                bbiNew.Visibility = CommonMethod.ApplyUserRights((int)Forms.CreateReceiptVoucher) != 0 ? BarItemVisibility.Always : BarItemVisibility.Never;
            }
            else if (VoucherTypeId == 1)
            {
                bbiDeleteTrans.Visibility = bbiDeleteCashBank.Visibility = CommonMethod.ApplyUserRightsForTransaction((int)Payment.DeletePaymentVoucher) != 0 ? BarItemVisibility.Always : BarItemVisibility.Never;
                bbiNew.Visibility = CommonMethod.ApplyUserRights((int)Forms.CreatePaymentVoucher) != 0 ? BarItemVisibility.Always : BarItemVisibility.Never;
            }
            else
            {
                bbiDeleteTrans.Visibility = bbiDeleteCashBank.Visibility = CommonMethod.ApplyUserRightsForTransaction((int)Contra.DeleteContraVoucher) != 0 ? BarItemVisibility.Always : BarItemVisibility.Never;
                bbiNew.Visibility = CommonMethod.ApplyUserRights((int)Forms.CreateContraVoucher) != 0 ? BarItemVisibility.Always : BarItemVisibility.Never;
            }
        }

        private void frmTransactionMultiAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            //On 13/08/2024, Reset to Global Currency Setting
            //this.ApplyGlobalSetting();
            ApplyRecentPrjectDetails();
        }

        private void frmTransactionMultiAdd_KeyDown(object sender, KeyEventArgs e)
        {
            ProcessShortcutKeys(e);
        }

        private void frmTransactionMultiAdd_Shown(object sender, EventArgs e)
        {
            /*lcGSTInvoices.Visibility = lcGSTInvoiceTotalAmountCaption.Visibility = lcGSTInvoiceTotalAmount.Visibility = LayoutVisibility.Never;
            lcGSTInvoiceTotalAmount.Visibility = lcGSTInvoiceBalance.Visibility = LayoutVisibility.Never;
            lgGSTInvoce.Visibility = LayoutVisibility.Never;*/

            dtTransactionDate.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //12/11/2024 To apply voucher currency and exchange rate to ledger
                //For Receipt/Payment - Voucher currnecy and exchange rate to all the legers
                //For contra - Cash/Bank part alone can be different
                if (this.AppSetting.AllowMultiCurrency == 1)
                {
                    ApplyVoucherCurrencyToLedgers();
                }

                if (IsValidEntry())
                {
                    ResultArgs resultArgs = null;
                    using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                    {
                        voucherTransaction.VoucherId = VoucherId;
                        voucherTransaction.ProjectId = ProjectId;
                        voucherTransaction.VoucherDate = this.UtilityMember.DateSet.ToDate(dtTransactionDate.Text, false);
                        voucherTransaction.VoucherType = VoucherType.ToString();
                        voucherTransaction.VoucherSubType = LedgerTypes.GN.ToString();
                        voucherTransaction.VoucherDefinitionId = VoucherDefinitionId;

                        //On 24/07/2023, To alert to authorize voucher based on setting in Finance ----------------------------------
                        voucherTransaction.AuthorizedStatus = ConfirmVoucherAuthorization;
                        //-----------------------------------------------------------------------------------------------------------

                        DataTable dt = gcBank.DataSource as DataTable;
                        DataRow drFirstRow = gvTransaction.GetDataRow(0);
                        if (drFirstRow != null)
                        {
                            voucherTransaction.LedgerId = this.UtilityMember.NumberSet.ToInteger(drFirstRow[voucherTransaction.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString());
                            resultArgs = voucherTransaction.FetchIsTDSLedger();
                            if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                            {
                                int isTDSLedger = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][voucherTransaction.AppSchema.Ledger.IS_TDS_LEDGERColumn.ColumnName].ToString());
                                int TDSGroupId = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][voucherTransaction.AppSchema.Ledger.GROUP_IDColumn.ColumnName].ToString());

                                if ((TDSGroupId == (int)TDSDefaultLedgers.SunderyCreditors && isTDSLedger == (int)YesNo.Yes) || (TDSGroupId == (int)TDSDefaultLedgers.DutiesandTaxes && isTDSLedger == (int)YesNo.Yes))
                                {
                                    voucherTransaction.VoucherSubType = LedgerTypes.TDS.ToString();
                                }
                            }
                        }

                        if (TransVoucherMethod == (int)TransactionVoucherMethod.Manual)
                        {
                            voucherTransaction.VoucherNo = txtVoucher.Text;
                        }
                        else
                        {
                            voucherTransaction.TransVoucherMethod = TransVoucherMethod;
                        }

                        if (VoucherId > 0) { voucherTransaction.VoucherNo = txtVoucher.Text; }

                        //On 09/10/2017, Earlier insert voucher and generate vouhcer number logic was with in same date
                        //like (Inserted voucher date and transaction voucher date should be same, vno will not be generated, if date is changd in voucher add screen)

                        //Now changed to any date. (Voucher No will be regenerate in insert mode if voucher type is same)
                        //if (isInsertVoucher && ((this.UtilityMember.DateSet.ToDate(InsertVoucherDate, false) != dtTransactionDate.DateTime) || (InsertVoucherIndex != rgTransactionType.SelectedIndex)))
                        if (isInsertVoucher && ((InsertVoucherIndex != rgTransactionType.SelectedIndex)))
                        {
                            voucherTransaction.isInsertVoucher = false;
                        }
                        else
                        {
                            if (isInsertVoucher)
                            {
                                voucherTransaction.PreviousVoucherNo = PreviousVoucherNo;
                                voucherTransaction.PreviousRunningDigit = PreviousRunningDigit;
                                voucherTransaction.isInsertVoucher = true;
                            }
                        }

                        if (VoucherId > 0 && EditVoucherIndex != rgTransactionType.SelectedIndex)
                        {
                            voucherTransaction.EditVoucherTypeChanged = true;
                            voucherTransaction.EditVoucherIndex = EditVoucherIndex;
                            voucherTransaction.EditVoucherDate = EditVoucherDate;
                        }

                        voucherTransaction.DonorId = glkpDonor.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpDonor.EditValue.ToString());
                        voucherTransaction.PurposeId = glkpPurpose.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpPurpose.EditValue.ToString());
                        voucherTransaction.ContributionType = glkpReceiptType.EditValue == null ? "N" : this.UtilityMember.NumberSet.ToInteger(glkpReceiptType.EditValue.ToString()) == (int)ReceiptType.First ? "F" : "S";
                        voucherTransaction.ContributionAmount = this.UtilityMember.NumberSet.ToDecimal(txtCurrencyAmount.Text);
                        voucherTransaction.CurrencyCountryId = glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString());
                        voucherTransaction.ExchangeRate = this.UtilityMember.NumberSet.ToDecimal(txtExchangeRate.Text);
                        voucherTransaction.CalculatedAmount = this.UtilityMember.NumberSet.ToDecimal(lblCalculatedAmt.Text);
                        voucherTransaction.ActualAmount = this.UtilityMember.NumberSet.ToDecimal(txtActualAmount.Text);
                        voucherTransaction.ExchageCountryId = this.glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString());
                        voucherTransaction.Narration = txtNarration.Text;
                        voucherTransaction.Status = 1;
                        voucherTransaction.FDGroupId = GroupId;
                        voucherTransaction.CreatedBy = this.UtilityMember.NumberSet.ToInteger(this.LoginUser.LoginUserId.ToString());
                        voucherTransaction.ModifiedBy = this.UtilityMember.NumberSet.ToInteger(this.LoginUser.LoginUserId.ToString());
                        voucherTransaction.NameAddress = txtNameAddress.Text;
                        voucherTransaction.PanNumber = txtPanNo.Text;
                        voucherTransaction.GSTNumber = txtGSTNo.Text;
                        voucherTransaction.FixedDepositInterestInfo = null;
                        if (PaymentBookingVoucherId > 0)
                        {
                            voucherTransaction.ClientCode = VoucherSubTypes.TDS.ToString();
                            voucherTransaction.ClientReferenceId = PaymentBookingVoucherId.ToString();
                        }

                        //TDS Payment Properties
                        voucherTransaction.TDSPaymentId = TDSPaymentId;
                        voucherTransaction.TDSCashBankId = CashBankId;

                        //TDS Party Payment Properties
                        voucherTransaction.TDSPartyPaymentId = TDSPartyPaymentId;
                        voucherTransaction.TDSPartyPaymentCashBankId = TDSPartyBankId;

                        //Voucher Trans Details
                        DataTable dtTrans = gcTransaction.DataSource as DataTable;
                        DataView dvTrans = new DataView(dtTrans);
                        dvTrans.RowFilter = "LEDGER_ID>0 AND AMOUNT>0";
                        this.Transaction.TransInfo = dvTrans;

                        DataTable dtCashTrans = gcBank.DataSource as DataTable;
                        DataView dvCashTrans = new DataView(dtCashTrans);

                        //19/06/2024, To allow cash and bank zero valued only for Receipt and Payment
                        //dvCashTrans.RowFilter = "LEDGER_ID>0 AND AMOUNT>0";
                        dvCashTrans.RowFilter = (AllowZeroValuedCashBankLedger ? string.Empty : "LEDGER_ID>0 AND AMOUNT>0");
                        this.Transaction.CashTransInfo = dvCashTrans;

                        //Apply
                        ApplyGST(dtTrans, dtCashTrans, true);

                        //26/04/2019, for Vendor GST Invoice details
                        if (!String.IsNullOrEmpty(GSTVendorInvoiceDate))
                        {
                            if (this.UtilityMember.DateSet.ToDate(GSTVendorInvoiceDate, false) > dtTransactionDate.DateTime)
                            {
                                GSTVendorInvoiceDate = dtTransactionDate.DateTime.ToShortDateString();
                            }
                        }
                        //On 04//11/2022, to assing gst invoice details ----------------------------------------------
                        voucherTransaction.GST_INVOICE_ID = GSTInvoiceId;
                        voucherTransaction.dtGSTInvoiceMasterDetails = DtGSTInvoiceMasterDetails;
                        voucherTransaction.dtGSTInvoiceMasterLedgerDetails = DtGSTInvoiceMasterLedgerDetails;
                        voucherTransaction.GST_VENDOR_INVOICE_NO = GSTVendorInvoiceNo; // STVendorInvoiceNo.Trim();
                        voucherTransaction.GST_VENDOR_INVOICE_DATE = GSTVendorInvoiceDate;
                        voucherTransaction.GST_VENDOR_INVOICE_TYPE = GSTVendorInvoiceType;
                        voucherTransaction.GST_VENDOR_ID = GSTVendorId;


                        voucherTransaction.BookingGSTInvoiceId = BookingGSTInvoiceId;
                        //--------------------------------------------------------------------------------------------

                        //Cost Centre Details
                        this.Transaction.CostCenterInfo = dsCostCentre;

                        this.Transaction.ReferenceNumberInfo = dtReferenceVoucherNumber;

                        this.Transaction.LedgerSubLedgerVouchers = dtSubLedgerVouchers;

                        //Denomination Details
                        this.Transaction.DenominationInfo = dsDenomination;
                        voucherTransaction.DenominationLedgerID = DenominationLedgerID;
                        //if (dsDenomination != null)
                        //{
                        //    voucherTransaction.ClientCode = VoucherSubTypes.CN.ToString();
                        //}

                        //Attach Voucher Images 
                        voucherTransaction.dtVoucherFiles = dtVoucherImages;
                        if (dtVoucherImages != null && dtVoucherImages.Rows.Count > 0)
                        {
                            //Dinesh 02/07/2025
                            //this.ShowWaitDialog("Attaching Voucher File(s)....");

                            //Implementaion
                            this.ShowWaitDialog(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.ADD_VOUCHER_FILE));

                        }
                        resultArgs = voucherTransaction.SaveTransactions();

                        if (dtVoucherImages != null && dtVoucherImages.Rows.Count > 0)
                        {
                            this.CloseWaitDialog();
                        }

                        if (resultArgs.Success)
                        {
                            //On 01/02/2018, to show contra voucher also
                            if (this.UIAppSetting.UIVoucherPrint == "1")
                            {
                                PrintVoucher(voucherTransaction.VoucherId);
                            }
                            else
                            {
                                GetUserControlInput();
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_SAVE));
                            }

                            //on 20/09/2017, Print cheque
                            PrintCheque(voucherTransaction.VoucherId, dtTrans, dtCashTrans);

                            //On 01/02/2018, to show contra voucher also
                            //if (rgTransactionType.SelectedIndex != -2)
                            //{
                            //    if (this.UIAppSetting.UIVoucherPrint == "1")
                            //    {
                            //        PrintVoucher(voucherTransaction.VoucherId);
                            //    }
                            //    else
                            //    {
                            //        GetUserControlInput();
                            //        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_SAVE));
                            //    }

                            //    //on 20/09/2017, Print cheque
                            //    PrintCheque(voucherTransaction.VoucherId);
                            //}
                            //else
                            //{
                            //    GetUserControlInput();
                            //    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_SAVE));
                            //}
                            PaymentBookingVoucherId = 0;
                            ClearControls();
                            LoadNarrationAutoComplete();
                            LoadNameAddressAutoComplete();
                            if (TransVoucherMethod == (int)TransactionVoucherMethod.Automatic && VoucherId == 0)
                            {
                                LoadVoucherNo();
                            }
                            else if (TransVoucherMethod == (int)TransactionVoucherMethod.Manual && VoucherId == 0)
                            {
                                txtVoucher.Text = string.Empty;
                            }
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                        }
                        else
                        {
                            this.ShowMessageBoxError(resultArgs.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            {
                this.CloseWaitDialog();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            VoucherId = 0;
            ClearControls();
            if (TransVoucherMethod == (int)TransactionVoucherMethod.Automatic && VoucherId == 0)
            {
                LoadVoucherNo();
            }
            else if (TransVoucherMethod == (int)TransactionVoucherMethod.Manual && VoucherId == 0)
            {
                txtVoucher.Text = string.Empty;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChequePrint_Click(object sender, EventArgs e)
        {
            Bosco.Report.Base.IReport report = new Bosco.Report.Base.ReportEntry(this.MdiParent);
            report.ShowChequePrint(VoucherId);
        }

        private void btnVendor_Click(object sender, EventArgs e)
        {
            ShowVendorGSTInvoiceDetails();
        }

        #endregion

        #region Donor & Common Events
        private void dtTransactionDate_EditValueChanged(object sender, EventArgs e)
        {
            rdtMaterializedOn.MinValue = rdeMaterializedOn.MinValue = dtTransactionDate.DateTime;
            SetBudgetInfo();
            GetUserControlInput();
            if (TransVoucherMethod == (int)TransactionVoucherMethod.Automatic && VoucherId == 0)
            {
                LoadVoucherNo();
            }
            else if (TransVoucherMethod == (int)TransactionVoucherMethod.Manual && VoucherId == 0)
            {
                txtVoucher.Text = string.Empty;
            }
            LoadSelectedLedger();
            FetchDateDuration();
            FetchSubLedgerVouchers();

            LoadPendingGSTInvoices();

            //26/08/2024, Load currency based on voucher date
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                LoadCountry();

            }
        }

        private void rgTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Temp 28/02/2020
            rtxtAmt.Tag = string.Empty;

            FetchVoucherMethod();
            ChangeTransType();

            if ((this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableGST).Equals((int)YesNo.Yes)))
            {
                if (rgTransactionType.SelectedIndex != 2)
                {
                    colGStAmt.VisibleIndex = 7;
                    colValueDate.Visible = false;
                    colCheque.Visible = false;
                    colGSTLedgerClass.Visible = true;
                }
                else
                {
                    colGStAmt.Visible = false;
                    colGSTLedgerClass.Visible = false;
                }

            }


            if ((this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableRefWiseReceiptANDPayment).Equals((int)YesNo.Yes)))
            {
                if (rgTransactionType.SelectedIndex != 2 || rgTransactionType.SelectedIndex != 0)
                {
                    colReferenceNumber.VisibleIndex = 3;
                }
                else
                {
                    colReferenceNumber.Visible = false;
                }
            }

            // To hide the Reference Number 
            ShowTransReference();

            //On 15/10/2024, Don't clear donor and currency details in the edit mode-------------------
            ShowHideDonor();
            //if (rgTransactionType.SelectedIndex == 2)
            //{
            //    ShowHideDonor();
            //}
            //------------------------------------------------------------------------------------------

            if (this.AppSetting.ShowGSTPan == "1")
            {
                if (rgTransactionType.SelectedIndex != 2)
                {
                    lciGroupPanGST.Visibility = LayoutVisibility.Always;
                    lciGroupPanGST.TextVisible = false;
                }
                else
                {
                    lciGroupPanGST.Visibility = LayoutVisibility.Never;
                }
            }

            if (TransVoucherMethod == (int)TransactionVoucherMethod.Automatic && VoucherId == 0)
            {
                LoadVoucherNo();
            }
            else if (TransVoucherMethod == (int)TransactionVoucherMethod.Manual && VoucherId == 0)
            {
                txtVoucher.Text = string.Empty;
            }

            if ((int)Division.Foreign == DivisionId)
            {
                ShowDonorAdditionalInfoForeignProjects();
            }
            else
            {
                //On 15/10/2024, Don't clear donor and currency details in the edit mode
                //ClearDonorDetails();
                DisableDonorFields();
            }

            //On 10/09/2024, To focuse to control
            dtTransactionDate.Focus();
        }

        private void glkpDonor_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                ResultArgs result = FetchProjectDetails();
                DataView dvResult = result.DataSource.Table.DefaultView;
                dvResult.RowFilter = "PROJECT_ID=" + ProjectId;
                DataTable dtResult = dvResult.ToTable();
                ContributionId = this.UtilityMember.NumberSet.ToInteger(dtResult.Rows[0]["CONTRIBUTION_ID"].ToString());
                DivisionId = this.UtilityMember.NumberSet.ToInteger(dtResult.Rows[0]["DIVISION_ID"].ToString());
                dvResult.RowFilter = "";
                if (!string.IsNullOrEmpty(glkpDonor.Text))
                {
                    EnableDonorFields();
                    DataRow dr = (glkpDonor.Properties.GetRowByKeyValue(glkpDonor.EditValue) as DataRowView).Row;

                    if (dr != null)
                    {
                        glkpCurrencyCountry.EditValue = this.UtilityMember.NumberSet.ToInteger(dr["COUNTRY_ID"].ToString());
                        this.glkpReceiptType.EditValueChanged -= new System.EventHandler(this.glkpReceiptType_EditValueChanged);
                        glkpReceiptType.EditValue = glkpReceiptType.Properties.GetKeyValue(0);
                        if (DivisionId == (int)Division.Foreign && ContributionId != 0)
                        {
                            glkpPurpose.EditValue = glkpPurpose.Properties.GetKeyValue(ContributionId - 1);
                            // glkpPurpose.Enabled = false;
                        }
                        else if (DivisionId == (int)Division.Local)
                        {
                            if (ContributionId != 0)
                            {
                                glkpPurpose.EditValue = glkpPurpose.Properties.GetKeyValue(ContributionId - 1);
                                //  glkpPurpose.Enabled = false;
                            }
                        }
                        else
                        {
                            glkpPurpose.EditValue = glkpPurpose.Properties.GetKeyValue(0);
                            // glkpPurpose.Enabled = true;
                        }
                        this.glkpReceiptType.EditValueChanged += new System.EventHandler(this.glkpReceiptType_EditValueChanged);
                        //txtNameAddress.Text = dr["FIRSTLASTNAME"].ToString();
                        txtNameAddress.Text = dr["ADDRESS"].ToString();
                        AssignDonationLedger();
                    }

                }
                else { DisableDonorFields(); }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        private void glkpDonor_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Close)
            {
                DisableDonorFields();
                ClearDonorDetails();
                txtNameAddress.Text = string.Empty;
                glkpDonor.Properties.ImmediatePopup = true;
            }
        }

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

        private void lkpCountry_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                //colAmount.SummaryItem.DisplayFormat = "{0:C}";
                //colCashAmount.SummaryItem.DisplayFormat = "{0:C}";

                DevExpress.XtraEditors.LookUpEdit editor = (sender as DevExpress.XtraEditors.LookUpEdit);
                DataRowView row = editor.Properties.GetDataSourceRowByKeyValue(editor.EditValue) as DataRowView;
                txtExchangeRate.Text = "1";
                if (row != null)
                {
                    lblDonorCurrency.Text = row["CUR"].ToString();

                    //On 22/08/2024, To Multi Currency property
                    if (this.AppSetting.AllowMultiCurrency == 1)
                    {
                        txtExchangeRate.Text = row["EXCHANGE_RATE"].ToString();
                        if (rgTransactionType.SelectedIndex == 0 || rgTransactionType.SelectedIndex == 1)
                        {
                            LoadCashBankLedger(rglkpCashLedger);
                        }
                        else
                        {
                            txtCashAmount.NullText = string.Empty;
                            LoadCashBankLedger(rglkpLedger);
                            LoadCashBankLedger(rglkpCashLedger);
                        }

                        //txtExchangeRate.Enabled = false;

                        //colAmount.SummaryItem.DisplayFormat = lblDonorCurrency.Text.Replace("(", "").Replace(")", "") + " {0:n}";
                        //colCashAmount.SummaryItem.DisplayFormat = lblDonorCurrency.Text.Replace("(", "").Replace(")", "") + " {0:n}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        private void txtAmount_EditValueChanged(object sender, EventArgs e)
        {
            if (this.UtilityMember.NumberSet.ToDouble(txtCurrencyAmount.Text) >= 0)
            {
                CalculateExchangeRate();
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_AMOUNT_LESS_THAN_ZERO));
                txtCurrencyAmount.Text = "0";
                CalculateExchangeRate();
            }
        }

        private void txtExchangeRate_EditValueChanged(object sender, EventArgs e)
        {
            if (this.UtilityMember.NumberSet.ToDouble(txtExchangeRate.Text) >= 0)
            {
                CalculateExchangeRate();
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_AMOUNT_LESS_THAN_ZERO));
                txtExchangeRate.Text = "1";
                CalculateExchangeRate();
            }

            //12/11/2024
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                ApplyVoucherCurrencyToLedgers();
            }
        }

        private void RemoveColor(TextEdit txtEdit)
        {
            txtEdit.BackColor = Color.Empty;
        }

        private void txtVoucher_Leave(object sender, EventArgs e)
        {
            RemoveColor(txtVoucher);
        }

        private void txtVoucher_Enter(object sender, EventArgs e)
        {
            SetTextEditBackColor(txtVoucher);
        }

        private void txtAmount_Enter(object sender, EventArgs e)
        {
            txtCurrencyAmount.Properties.Mask.MaskType = MaskType.RegEx;
            //On 29/07/2017, When we change decimial point seprator in global settings, we could not enter , as decimal point separtor in the txtbox
            //123.345.567,12
            //txtAmount.Properties.Mask.EditMask = @"\d*\.?\d*";
            txtCurrencyAmount.Properties.Mask.EditMask = @"\d*\" + AppSetting.DecimalSeparator + @"?\d*";
        }

        private void txtAmount_Validating(object sender, CancelEventArgs e)
        {
            txtCurrencyAmount.Properties.Mask.MaskType = MaskType.Numeric;
            txtCurrencyAmount.Properties.Mask.EditMask = "n";
            txtCurrencyAmount.Properties.Mask.UseMaskAsDisplayFormat = true;
        }

        private void txtExchangeRate_Enter(object sender, EventArgs e)
        {
            txtExchangeRate.Properties.Mask.MaskType = MaskType.RegEx;

            //On 29/07/2017, When we change decimial point seprator in global settings, we could not enter , as decimal point separtor in the txtbox
            //123.345.567,12
            //txtExchangeRate.Properties.Mask.EditMask = @"\d*\.?\d*";
            txtExchangeRate.Properties.Mask.EditMask = @"\d*\" + AppSetting.DecimalSeparator + @"?\d*";
        }

        private void txtExchangeRate_Validating(object sender, CancelEventArgs e)
        {
            txtExchangeRate.Properties.Mask.MaskType = MaskType.Numeric;
            txtExchangeRate.Properties.Mask.EditMask = "n";
            txtExchangeRate.Properties.Mask.UseMaskAsDisplayFormat = true;
        }

        private void txtActualAmount_Enter(object sender, EventArgs e)
        {
            txtActualAmount.Properties.Mask.MaskType = MaskType.RegEx;
            //txtActualAmount.Properties.Mask.EditMask = @"\d*\.?\d*";
            //On 29/07/2017, When we change decimial point seprator in global settings, we could not enter , as decimal point separtor in the txtbox
            //123.345.567,12
            txtActualAmount.Properties.Mask.EditMask = @"\d*\" + AppSetting.DecimalSeparator + @"?\d*";
            txtActualAmount.Properties.Mask.UseMaskAsDisplayFormat = true;
        }

        private void txtActualAmount_Validating(object sender, CancelEventArgs e)
        {
            txtActualAmount.Properties.Mask.MaskType = MaskType.Numeric;
            txtActualAmount.Properties.Mask.EditMask = "n";
            //txtActualAmount.Properties.Mask.UseMaskAsDisplayFormat = true;
        }
        #endregion

        #region Transaction Grid Events
        private void gvTransaction_GotFocus(object sender, EventArgs e)
        {
            BaseView view = sender as BaseView;
            if (view == null)
                return;

            if (MouseButtons == System.Windows.Forms.MouseButtons.Left)
                return;
            view.ShowEditor();
            TextEdit editor = view.ActiveEditor as TextEdit;
            if (editor != null)
            {
                editor.SelectAll();
                editor.Focus();
            }
        }

        private void rglkpLedger_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                BalanceProperty budgetAmount = FetchBudgetAmount(LedgerId);
                int LedgerID = 0;
                int NatureId = 0;
                int Group = 0;
                int IsGSTLedgers = 0;

                int SourceId = 0;

                GridLookUpEdit gridLKPEdit = sender as GridLookUpEdit;
                if (gridLKPEdit.EditValue != null)
                {
                    DataRowView drv = gridLKPEdit.GetSelectedDataRow() as DataRowView;

                    if (drv != null)
                    {
                        LedgerID = this.UtilityMember.NumberSet.ToInteger(drv["LEDGER_ID"].ToString());
                        NatureId = this.UtilityMember.NumberSet.ToInteger(drv["NATURE_ID"].ToString());
                        Group = this.UtilityMember.NumberSet.ToInteger(drv["GROUP_ID"].ToString());
                        SourceId = this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetRowCellValue(gvTransaction.FocusedRowHandle, colSource).ToString());
                        IsGSTLedgers = this.UtilityMember.NumberSet.ToInteger(drv["IS_GST_LEDGERS"].ToString());

                        if (!NotifyTransactionLedger(NatureId, SourceId))
                        {
                            e.Cancel = true;
                        }
                        //else
                        //{

                        //    DataTable dtTrans = gcTransaction.DataSource as DataTable;
                        //    if (dtTrans != null && dtTrans.Rows.Count > 0)
                        //    {
                        //        DataTable dtFirst = dtTrans.AsEnumerable().Take(1).CopyToDataTable();
                        //        if (dtFirst != null && dtFirst.Rows.Count > 0)
                        //        {
                        //            int LedgerId = this.UtilityMember.NumberSet.ToInteger(dtFirst.Rows[0]["LEDGER_ID"].ToString());
                        //        }
                        //    }
                        //}

                        if (!e.Cancel)
                        {
                            gvTransaction.SetFocusedRowCellValue(colLedger, LedgerID);
                            gvTransaction.SetFocusedRowCellValue(colIsGSTLedger, IsGSTLedgers);
                            // if (ActualAmt > 0 && (TransSummaryVal <= ActualAmt) && TransSummaryVal < 1)
                            // {



                            //if (ActualAmt > 0)
                            //{
                            //    double Amount = ActualAmt - TransSummaryVal;
                            //    gvTransaction.SetFocusedRowCellValue(colAmount, Amount.ToString());
                            //    gvTransaction.PostEditor();
                            //    gvTransaction.UpdateCurrentRow();
                            //}
                            //  }
                            // By Aldrin
                            if (BudgetId > 0 && budgetAmount.Amount > 0)
                            {
                                string BudgetAmt = CalculateBudget(LedgerId); //FetchBudgetAmount(LedgerID);
                                if (BudgetAmt != string.Empty)
                                {
                                    gvTransaction.SetFocusedRowCellValue(colBudgetAmount, BudgetAmt);
                                }
                            }
                            else
                            {
                                gvTransaction.SetFocusedRowCellValue(colBudgetAmount, string.Empty);
                            }
                            EnableBankFields();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        private void CalculateDonorFirstRowValue()
        {
            double ActualAmt = 0;

            //On 26/08/2024, If multi currency enabled, let us validate ledger amount with currency amount other wise  converted acutal amount
            ActualAmt = this.UtilityMember.NumberSet.ToDouble(txtActualAmount.Text);
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                ActualAmt = this.UtilityMember.NumberSet.ToDouble(txtCurrencyAmount.Text);
            }

            if (ActualAmt > 0)
            {
                int TransSrc = gvTransaction.GetRowCellValue(gvTransaction.FocusedRowHandle, colSource) != null ?
                            this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetRowCellValue(gvTransaction.FocusedRowHandle, colSource).ToString()) : 0;

                //16/08/2024, For all type of (Receipt, Payment, Contra) ---------------------------------------------------------------------------------------
                int EntryTransSrc = (int)Source.To;
                if (rgTransactionType.SelectedIndex == 0) EntryTransSrc = (int)Source.To;
                else if (rgTransactionType.SelectedIndex == 1) EntryTransSrc = (int)Source.By;
                //----------------------------------------------------------------------------------------------------------------------------------------------

                if (TransSrc == EntryTransSrc) //(int)Source.To
                {
                    double TransSummaryValue = 0;
                    DataTable dtTrans = gcTransaction.DataSource as DataTable;
                    if (dtTrans != null && dtTrans.Rows.Count > 0)
                    {
                        TransSummaryValue = this.UtilityMember.NumberSet.ToDouble(dtTrans.Compute("SUM(AMOUNT)", "SOURCE=" + EntryTransSrc).ToString()); //(int)Source.To
                    }

                    double Amount = gvTransaction.GetRowCellValue(gvTransaction.FocusedRowHandle, colCashAmount) != null ?
                        this.UtilityMember.NumberSet.ToDouble(gvTransaction.GetRowCellValue(gvTransaction.FocusedRowHandle, colAmount).ToString()) : 0;
                    double dAmount = 0.0;
                    if (ActualAmt <= TransSummaryValue)
                    {
                        dAmount = (ActualAmt - TransSummaryValue) + Amount;
                    }
                    else if (ActualAmt >= TransSummaryValue)
                    {
                        dAmount = Amount - (TransSummaryValue - ActualAmt);
                    }

                    if (dAmount >= 0)
                    {
                        gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colAmount, dAmount);
                    }
                }
            }
        }

        private void rglkpLedger_Leave(object sender, EventArgs e)
        {
            if (LedgerId > 0)
            {
                //int NatureId = 0;
                //GridLookUpEdit grdLedgerLookup = sender as GridLookUpEdit;
                //if (grdLedgerLookup.EditValue != null)
                //{
                //    DataRowView drv = grdLedgerLookup.GetSelectedDataRow() as DataRowView;

                //    if (drv != null)
                //    {
                //        NatureId = this.UtilityMember.NumberSet.ToInteger(drv["NATURE_ID"].ToString());
                //    }
                //}

                // To Get the Nature Id's 
                int NatureId = GetNatureId();

                if (VoucherId == 0)
                {
                    gvTransaction.PostEditor();
                    gvTransaction.UpdateCurrentRow();
                    DataTable dtTrans = gcTransaction.DataSource as DataTable;
                    string Balance = string.Empty;
                    Balance = GetLedgerBalanceValues(dtTrans, LedgerId);
                    SetBudgetInfo(); // by alex to set the budget amount while leaving the ledger field.
                    BalanceProperty budgetAmt = FetchBudgetAmount(LedgerId);
                    if (Balance != string.Empty)
                    {
                        if (rgTransactionType.SelectedIndex.Equals(1))
                        {
                            int IsTDSLedger = CheckIsTDSLedger(LedgerId);
                            if (IsTDSLedger == 1 && TDSGroupId.Equals((int)TDSLedgerGroup.DutiesAndTax))
                            {
                                //using (DeducteeTaxSystem DeducteeTax = new DeducteeTaxSystem())
                                //{
                                // DeducteeTax.TaxLedgerId = LedgerId;
                                // double TDSAmount = DeducteeTax.GetTaxAmount();

                                BalanceProperty balanceProperty = FetchCurrentBalance(LedgerId);
                                double BalanceAmount = balanceProperty.Amount;
                                string[] LedgerBalance = Balance.Split(' ');
                                //  double BalanceAmount = this.UtilityMember.NumberSet.ToDouble(LedgerBalance[1].ToString());
                                // if (BalanceAmount != 0)
                                // {
                                // BalanceAmount = TDSAmount.Equals(BalanceAmount) ? TDSAmount : TDSAmount > BalanceAmount ? TDSAmount - BalanceAmount : TDSAmount < BalanceAmount ? TDSAmount : BalanceAmount;
                                // }
                                //TDSPaymentAmount = TDSPaymentAmount > 0 && BalanceAmount < TDSPaymentAmount ? TDSPaymentAmount - TDSPrevTaxAmount : BalanceAmount;

                                gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colAmount, BalanceAmount);
                                gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colLedgerBal, Balance);
                                gvTransaction.UpdateCurrentRow();
                                gvTransaction.Focus();
                                gvTransaction.ShowEditor();
                                //  BalanceAmount = TDSAmount = 0;
                                // }
                            }
                            else
                            {
                                if (this.AppSetting.IS_CMF_SLA && (BudgetId > 0) && (!(NatureId == (int)Natures.Assert || NatureId == (int)Natures.Libilities))) // Chinna 03/06/2021
                                {
                                    string BudgetBal = string.Empty;
                                    string LedgerBal = string.Empty;

                                    //BudgetBal = GetBudgetBalanceYearIEValues(LedgerId);

                                    BudgetBal = CalculateBudget(LedgerId);
                                    LedgerBal = GetCurrentBalanceYearIEValues(LedgerId);

                                    if (BudgetBal != string.Empty)
                                        gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colBudgetAmount, BudgetBal);

                                    gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colLedgerBal, LedgerBal);
                                }
                                else
                                {
                                    if (this.AppSetting.IS_CMF_SLA && (!(NatureId == (int)Natures.Assert || NatureId == (int)Natures.Libilities)))
                                    {
                                        string LedgerBal = string.Empty;
                                        LedgerBal = GetCurrentBalanceYearIEValues(LedgerId);

                                        gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colLedgerBal, LedgerBal);
                                    }
                                    else
                                    {
                                        gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colLedgerBal, Balance);

                                    }
                                }
                            }
                        }
                        else
                        {
                            if (this.AppSetting.IS_CMF_SLA && (!(NatureId == (int)Natures.Assert || NatureId == (int)Natures.Libilities))) // Chinna 03/06/2021
                            {
                                string LedgerBalance = string.Empty;
                                string BudgetBal = string.Empty;

                                LedgerBalance = GetCurrentBalanceYearIEValues(LedgerId);
                                //BudgetBal = GetBudgetBalanceYearIEValues(LedgerId);
                                BudgetBal = CalculateBudget(LedgerId);

                                if (BudgetBal != string.Empty)
                                    gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colBudgetAmount, BudgetBal);

                                gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colLedgerBal, LedgerBalance);
                            }
                            else
                            {
                                gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colLedgerBal, Balance);
                            }
                        }
                    }
                    // By Aldrin
                    if (BudgetId > 0) //&& budgetAmt.Amount > 0)
                    {
                        string BudgetBal = CalculateBudget(LedgerId);
                        if (BudgetBal != string.Empty)
                            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colBudgetAmount, BudgetBal);

                        // chinna 03.06.2021
                        //string BudgetBal = string.Empty;
                        //if (this.AppSetting.IS_CMF_SLA) // Chinna 03/06/2021
                        //{
                        //    //BudgetBal = GetBudgetBalanceYearIEValues(LedgerId);
                        //    BudgetBal = CalculateBudget(LedgerId);
                        //    if (BudgetBal != string.Empty)

                        //        if (BudgetBal != string.Empty)
                        //            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colBudgetAmount, BudgetBal);
                        //}
                        //else
                        //{
                        //    //  BudgetBal = GetBudgetBalanceYearIEValues(LedgerId);
                        //    // if (BudgetBal != string.Empty)

                        //    gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colBudgetAmount, BudgetBal);
                        //}
                    }
                    else
                    {
                        gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colBudgetAmount, string.Empty);
                    }
                }
                // if (LedgerId != ReferenceLedgerId)
                // {
                // gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, gccolRefLedgerId, LedgerId);
                //  }
                CalculateDonorFirstRowValue();

                // Calculate the GST Values while Change the Ledger  (09.05.2019)
                //AssignGSTAmount(LedgerId);

                //29/11/2019, To set Ledger GST Ledger Class
                if ((PreviousLedgerId != LedgerId && PreviousLedgerId != 0) || LedgerGSTClassId == 0)
                {
                    //As on 30/10/2023, To retain zero based GST Ledger when voucer is edited
                    //AssignGSTAmount(LedgerMappedDefaultGSTID);
                    Int32 tmplegerid = gvTransaction.GetRowCellValue(gvTransaction.FocusedRowHandle, colTmpLedgerId) == null ? 0 : UtilityMember.NumberSet.ToInteger(gvTransaction.GetRowCellValue(gvTransaction.FocusedRowHandle, colTmpLedgerId).ToString());
                    if (VoucherId > 0 && LedgerGSTClassId == 0 && tmplegerid == LedgerId)
                    {
                        AssignGSTAmount(this.AppSetting.GSTZeroClassId);
                    }
                    else
                    {
                        AssignGSTAmount(LedgerMappedDefaultGSTID);
                    }
                }
                else
                {
                    AssignGSTAmount(LedgerGSTClassId);
                }

                /*if (VoucherId == 0)
                {
                    int ledgerMappedDefaultGSTID = FetchGSTLedger(LedgerId);
                    AssignGSTAmount(ledgerMappedDefaultGSTID);
                }
                else
                {
                    bool isGSTEnabledLedger = (IsGSTLedgers(LedgerId) == 1);
                    if (isGSTEnabledLedger && LedgerGSTClassId==0)
                    {
                        int ledgerMappedDefaultGSTID = FetchGSTLedger(LedgerId);
                        AssignGSTAmount(ledgerMappedDefaultGSTID);
                    }
                    else if (!isGSTEnabledLedger)
                    {
                        AssignGSTAmount(0);
                    }
                    else
                    {
                        AssignGSTAmount(LedgerGSTClassId);
                    }
                }*/
                CalculateFirstRowValue();

                //On 11/11/2024 to set exchange and currency id
                if (this.AppSetting.AllowMultiCurrency == 1)
                {
                    Int32 currencycountry = glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString());
                    gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colExchangeAmount, txtExchangeRate.Text);
                    gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colExchangeAmount, lblLiveExchangeRate.Text);
                    gvTransaction.UpdateCurrentRow();
                }

            }
            else
            {
                //gvTransaction.UpdateCurrentRow();
            }
            PreviousLedgerId = LedgerId;
        }

        /// <summary>
        /// Calculate I & E Balance (chinna 04.06.2021) (Ledger Balance)
        /// </summary>
        /// <param name="LedgerId"></param>
        /// <returns></returns>
        private string GetCurrentBalanceYearIEValues(int LedgerId)
        {
            string Mode = string.Empty;
            double OldValue = 0;
            double NewValue = 0;
            double CalOldNewValue = 0;
            double CurBal = 0.00;
            CurrentLedgerTransMode = "";

            double dCalculateCurBal = 0.00;

            BalanceProperty Balance = FetchBudgetLedgerBalance(LedgerId);
            dCalculateCurBal = Balance.Amount;

            DataTable dtTemp = gcTransaction.DataSource as DataTable;
            string NewValueMode = string.Empty;

            if (dtTemp != null)
            {
                NewValue = GetCalculatedAmount(LedgerId, dtTemp);
                OldValue = GetCalculatedTempAmount(LedgerId, dtTemp);
                CalOldNewValue = (OldValue - NewValue);
            }

            if (Balance.TransMode == TransactionMode.CR.ToString())
            {
                CurBal = -(Balance.Amount);
            }
            else
            {
                CurBal = Balance.Amount;
            }


            dCalculateCurBal = CurBal - (OldValue) + NewValue;

            if (dCalculateCurBal < 0)
            {
                Mode = TransactionMode.CR.ToString();
            }
            else
            {
                Mode = TransactionMode.DR.ToString();
            }

            CurrentLedgerTransMode = Mode; // To be used in Budget Calculation

            return this.UtilityMember.NumberSet.ToCurrency(Math.Abs(dCalculateCurBal)) + " " + Mode;

            //if (calIECurrentBal < 0)
            //{
            //    Mode = TransactionMode.CR.ToString();
            //}
            //else
            //{
            //    Mode = TransactionMode.DR.ToString();
            //}

            //string ActualAmount = string.Empty;
            //int tmpNatureID = gvTransaction.GetFocusedRowCellValue(colSource) != null ?
            //                this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetFocusedRowCellValue(colSource).ToString()) : 0;

            //if (rgTransactionType.SelectedIndex == 0)
            //{

            //    if (CurrentLedgerTransMode == "CR")
            //    {
            //        Total = Total + CalOldNewValue;
            //    }
            //}
            //else
            //{
            //    if (CurrentLedgerTransMode == "CR" && Total != LedgerAmount)
            //    {
            //        Total = Total > LedgerAmount ? Total - CalOldNewValue : CalOldNewValue - Total;
            //    }
            //    else if (CurrentLedgerTransMode == "DR" && Total == LedgerAmount)
            //    {
            //        Total = Total > LedgerAmount ? Total - CalOldNewValue : CalOldNewValue - Total;
            //    }
            //    else if (CurrentLedgerTransMode == "DR" && Total != LedgerAmount)
            //    {
            //        if (VoucherId == 0)
            //        {
            //            Total = Total = Total + LedgerAmount;
            //        }
            //        else
            //        {
            //            Total = Total = Total + (OldValue + NewValue);
            //        }
            //    }
            //    else
            //    {
            //        Total = Total > LedgerAmount ? Total - CalOldNewValue : CalOldNewValue - Total;
            //    }
            //}

            //if (Total != 0)
            //{
            //    ActualAmount = Math.Abs(this.UtilityMember.NumberSet.ToDouble(Total.ToString())).ToString();
            //}
            //else
            //{
            //    // ActualAmount = "0.00" + " " + CurrentLedgerTransMode;
            //    ActualAmount = "0.00";
            //}

            //return this.UtilityMember.NumberSet.ToCurrency(Math.Abs(calIECurrentBal)) + " " + Mode;
            // return this.UtilityMember.NumberSet.ToCurrency(Math.Abs(calIECurrentBal)) + " " + Mode;
            //return this.UtilityMember.NumberSet.ToNumber(Math.Abs(this.UtilityMember.NumberSet.ToDouble(ActualAmount.ToString())));
        }

        /// <summary>
        /// Calculate I & E Balance (chinna 04.06.2021) ( Budget Balance)
        /// </summary>
        /// <param name="LedgerId"></param>
        /// <returns></returns>
        private string GetBudgetBalanceYearIEValues(int LedgerId)
        {
            double calBudgetAmt = 0.00;
            BalanceProperty Balance = FetchBudgetAmount(LedgerId);
            calBudgetAmt = Balance.Amount;


            return this.UtilityMember.NumberSet.ToCurrency(Math.Abs(calBudgetAmt)) + " " + Balance.TransMode;
        }

        private int GetNatureId()
        {
            int NatureId = 0;
            using (LedgerGroupSystem ledgergroupsystem = new LedgerGroupSystem())
            {
                NatureId = ledgergroupsystem.GetNatureId(LedgerGroupId);
            }
            return NatureId;
        }

        private int CheckIsTDSLedger(int LedgerID)
        {
            int isTDSLedger = 0;
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    ledgerSystem.LedgerId = LedgerID;
                    // isTDSLedger = ledgerSystem.IsTDSLedgerExits();
                    resultArgs = ledgerSystem.FetchTDSLedgers();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        isTDSLedger = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][ledgerSystem.AppSchema.Ledger.IS_TDS_LEDGERColumn.ColumnName].ToString());
                        TDSGroupId = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][ledgerSystem.AppSchema.Ledger.GROUP_IDColumn.ColumnName].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
            return isTDSLedger;
        }

        private void rglkpSource_Leave(object sender, EventArgs e)
        {
            if (LedgerId > 0)
            {
                if (VoucherId == 0)
                {
                    gvTransaction.PostEditor();
                    gvTransaction.UpdateCurrentRow();
                    DataTable dtTrans = gcTransaction.DataSource as DataTable;
                    string Balance = GetLedgerBalanceValues(dtTrans, LedgerId);//ShowLedgerBalance(LedgerId, dtTrans, true);
                    if (Balance != string.Empty)
                    {
                        gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colLedgerBal, Balance);
                    }
                }
            }
        }

        private void rtxtAmount_Leave(object sender, EventArgs e)
        {
            if (IsValidaTransactionRow())
            {
                BalanceProperty dAmt = FetchBudgetAmount(LedgerId);
                if (dAmt.Amount > 0)
                {
                    BalanceProperty Balance = FetchCurrentBalance(LedgerId);
                    if ((Balance.TransMode == TransactionMode.CR.ToString() && rgTransactionType.SelectedIndex == 0) ||
                        (Balance.TransMode == TransactionMode.DR.ToString() && rgTransactionType.SelectedIndex == 1))
                    {
                        if (dAmt.Amount < (Balance.Amount))
                        {   
                            //Dinesh 02-07-2025
                            //this.ShowMessageBox("The Transaction exceeds the Approved Budget");

                            //Implementation 
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.TRANSACTION_AMOUNT_EXCEEDS));
                            
                        }
                        //if (dAmt.Amount < (Balance.Amount + LedgerAmount))
                        //{
                        //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.TRANSACTION_AMOUNT_EXCEEDS));
                        //}
                    }
                }
            }

        }

        private void gcTransaction_ProcessGridKey(object sender, KeyEventArgs e)
        {
            bool canFoucsCashTrnasaction = false;
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control &&
                (gvTransaction.FocusedColumn == colAmount || gvTransaction.FocusedColumn == colValueDate || gvTransaction.FocusedColumn == colCheque ||
                 gvTransaction.FocusedColumn == colReferenceNumber || gvTransaction.FocusedColumn == colLedgerNarration || gvTransaction.FocusedColumn == colGSTLedgerClass ||
                 (AppSetting.IS_DIOMYS_DIOCESE && AppSetting.EnableSubLedgerVouchers == "1" && gvTransaction.FocusedColumn == colLedger)))
            {
                gvTransaction.PostEditor();
                gvTransaction.UpdateCurrentRow();

                //On 21/10/2017 To show Cheque refernumber detials only for Contra (No Need for contra voucher on 15/02/2018)
                //On 21/09/2023 To show Cheque refernumber detials only for Contra - chinna
                if (gvTransaction.FocusedColumn == colCheque && LedgerAmount > 0 && LedgerId > 0)
                {
                    if (rgTransactionType.SelectedIndex == 2) //Only for Contra
                    {
                        ShowBankChequeDDReferenceNumberDetails(false); // To show, 21/09/2023
                        // ShowBankChequeDDReferenceNumberDetails(true);
                    }
                }

                // To show cost centre form.
                if (gvTransaction.FocusedColumn == colAmount && LedgerAmount > 0 && LedgerId > 0)
                {
                    if (rgTransactionType.SelectedIndex == 1)
                    {
                        ShowTDSForm(this.UtilityMember.NumberSet.ToDouble(LedgerAmount.ToString()));
                    }

                    if (rgTransactionType.SelectedIndex != 2)
                    {
                        ShowCostCentre(this.UtilityMember.NumberSet.ToDouble(LedgerAmount.ToString()));
                    }
                }
                // To show the Denomination form
                if (gvTransaction.FocusedColumn == colAmount && LedgerAmount > 0 && LedgerId > 0)
                {
                    int s = rgTransactionType.SelectedIndex;
                    if (s.Equals((int)VoucherSubTypes.CN))
                    {
                        // ShowDenominationForm();
                    }
                }

                // to Show Reference Forms
                if (gvTransaction.FocusedColumn == colReferenceNumber && LedgerAmount > 0 && LedgerId > 0)
                {
                    if (rgTransactionType.SelectedIndex == 1 && LedgerGroupId == (int)TDSDefaultLedgers.SunderyCreditors || LedgerGroupId == (int)TDSDefaultLedgers.SundryDebtors)
                    {
                        ShowReferenceNo();
                    }
                }

                //#10/02/2020, To show Sub Ledgers Vouchers
                if (gvTransaction.FocusedColumn == colLedger && LedgerId > 0)
                {
                    ShowSubLedgersVouchers();
                }

                // Single Entry
                if (TransEntryMethod == VoucherEntryMethod.Single)
                {
                    if (rgTransactionType.SelectedIndex != 2)
                    {
                        //if (gvTransaction.FocusedColumn == colAmount && (!colLedgerNarration.Visible)) //On 29/11/2019
                        if (gvTransaction.FocusedColumn == colAmount && (!colLedgerNarration.Visible && !colGSTLedgerClass.Visible))
                        {
                            canFoucsCashTrnasaction = true;
                        }
                        else if (gvTransaction.FocusedColumn == colLedgerNarration)
                        {
                            canFoucsCashTrnasaction = true;
                        }
                        else if (gvTransaction.FocusedColumn == colReferenceNumber)
                        {
                            canFoucsCashTrnasaction = true;
                        }
                        else if (gvTransaction.FocusedColumn == colGSTLedgerClass) //On 29/11/2019
                        {
                            canFoucsCashTrnasaction = true;
                        }
                    }
                    else if (rgTransactionType.SelectedIndex == 2)
                    {
                        if ((gvTransaction.FocusedColumn == colValueDate && LedgerGroupId == (int)FixedLedgerGroup.BankAccounts) //On 29/11/2019
                        || (gvTransaction.FocusedColumn == colAmount && LedgerGroupId == (int)FixedLedgerGroup.Cash && colLedgerNarration.Visible == false && colGSTLedgerClass.Visible == false))
                        {
                            canFoucsCashTrnasaction = true;
                        }
                    }
                }
                else // Multi Entry Mode
                {
                    if (LedgerId == 0 && LedgerAmount == 0)
                    {
                        if (IsValidSource()) { canFoucsCashTrnasaction = true; }
                        else { FocusTransactionGrid(); }
                    }

                    if (rgTransactionType.SelectedIndex == 2)
                    {
                        if ((gvTransaction.FocusedColumn == colValueDate && LedgerGroupId == (int)FixedLedgerGroup.BankAccounts)
                            || (gvTransaction.FocusedColumn == colAmount && LedgerGroupId == (int)FixedLedgerGroup.Cash && colLedgerNarration.Visible == false)
                            || (gvTransaction.FocusedColumn == colLedgerNarration && LedgerGroupId == (int)FixedLedgerGroup.Cash))
                        {
                            if (gvTransaction.IsLastRow && !EnableMultiRow)
                            {
                                TransacationGridNewItem = TransEntryMethod;
                            }
                            else { gvTransaction.MoveNext(); gvTransaction.FocusedColumn = colLedger; }
                            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                            {
                                e.SuppressKeyPress = true;
                            }
                        }
                    }
                }

                if (canFoucsCashTrnasaction)
                {
                    gvTransaction.CloseEditor();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    FocusCashTransactionGrid();
                }

                //This will assign new row, active LedgerId and LedgerAmount will be cleared

                if ((LedgerId > 0 && LedgerAmount > 0)
                    && (TransEntryMethod == VoucherEntryMethod.Multi) && rgTransactionType.SelectedIndex != 2 && gvTransaction.IsLastRow && !EnableMultiRow
                    && (!colReferenceNumber.Visible || gvTransaction.FocusedColumn == colReferenceNumber)
                    && (!colLedgerNarration.Visible || gvTransaction.FocusedColumn == colLedgerNarration)
                    && (!colGSTLedgerClass.Visible || colLedgerNarration.Visible || gvTransaction.FocusedColumn == colGSTLedgerClass)) //On 29/11/2019
                {
                    TransacationGridNewItem = TransEntryMethod;
                    if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                    {
                        e.SuppressKeyPress = true;
                    }
                }
            }
            else if (gvTransaction.IsFirstRow && gvTransaction.FocusedColumn == colLedger && e.Shift && e.KeyCode == Keys.Tab)
            {
                if (lciDonorInfo.Visibility == LayoutVisibility.Never)
                {
                    if (txtVoucher.Enabled)
                    {
                        txtVoucher.Select();
                        txtVoucher.Focus();
                    }
                    else
                    {
                        dtTransactionDate.Focus();
                    }
                    e.SuppressKeyPress = true;
                }
                else
                {
                    if (txtActualAmount.Enabled)
                    {
                        txtActualAmount.Select();
                        txtActualAmount.Focus();
                        e.SuppressKeyPress = true;
                    }
                    else if (glkpPurpose.Enabled)
                    {
                        glkpPurpose.Select();
                        glkpPurpose.Focus();
                        e.SuppressKeyPress = true;
                    }
                    else if (glkpDonor.Enabled)
                    {
                        glkpDonor.Select();
                        glkpDonor.Focus();
                        e.SuppressKeyPress = true;
                    }
                    else
                    {
                        dtTransactionDate.Focus();
                        e.SuppressKeyPress = true;
                    }
                }
            }

            // This is to assign the Value while entering or changing GST to local and local to GSt Ledger (09.05.2019)
            //29/11/2019, To set Ledger GST Ledger Class
            AssignGSTAmount(LedgerGSTClassId);
        }

        private void gvTransaction_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
                if (LedgerId > 0 && rgTransactionType.SelectedIndex == 2)
                {
                    if ((LedgerGroupId == (int)FixedLedgerGroup.Cash)
                        && (gvTransaction.FocusedColumn == colCheque || gvTransaction.FocusedColumn == colValueDate))
                    {
                        e.Cancel = true;
                    }
                }

                if (gvTransaction.IsFirstRow && gvTransaction.FocusedColumn == colSource)
                {
                    e.Cancel = true;
                }

                if (gvTransaction.FocusedColumn == colSource && rgTransactionType.SelectedIndex == 2)
                {
                    e.Cancel = true;
                }
                if (gvTransaction.FocusedColumn == colCostCentre) // Added By praveen to restrict the costcentre form to be shown
                {
                    if (LedgerId > 0)
                    {
                        if (!CheckCostcentreEnabled(LedgerId))
                            e.Cancel = true;
                    }
                }

                if (rgTransactionType.SelectedIndex == 2)
                {
                    colGStAmt.Visible = false;
                    colBudgetAmount.Visible = false;
                    colCostCentre.Visible = false;
                    colReferenceNumber.Visible = false;
                    // VisibleCashBankAdditionalFields(false);
                }
                else
                {
                    //Temp 28/02/2020
                    AlertBudgetMessage();
                }

                //02/12/2019, to lock GST column, if selected ledger is not GST ledger
                if (LedgerId > 0 && colGSTLedgerClass.Visible)
                {
                    if (gvTransaction.FocusedColumn == colGSTLedgerClass && LedgerGSTClassId == 0)
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            finally { }
        }

        private void gvTransaction_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (gvTransaction.GetRowCellValue(e.RowHandle, colLedger) != null && rgTransactionType.SelectedIndex == 2)
                {
                    using (LedgerSystem ledgerSystem = new LedgerSystem())
                    {
                        int GroupId = 0;
                        int LedgerId = this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetRowCellValue(e.RowHandle, colLedger).ToString());
                        if (LedgerId > 0)
                        {
                            DataRowView drvLedger = rglkpLedger.GetRowByKeyValue(LedgerId) as DataRowView;
                            if (drvLedger != null)
                            {
                                GroupId = this.UtilityMember.NumberSet.ToInteger(drvLedger["GROUP_ID"].ToString());
                            }

                            if ((e.Column == colCheque || e.Column == colValueDate) &&
                            (GroupId == (int)FixedLedgerGroup.Cash))
                            {
                                e.Appearance.BackColor = Color.LightGray;
                            }
                        }
                    }
                }
                else if (gvTransaction.GetRowCellValue(e.RowHandle, colLedger) != null) // Added By praveen to highlight the row the ledgers that are costcentre enabled
                {
                    int LedgerId = this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetRowCellValue(e.RowHandle, colLedger).ToString());
                    if (LedgerId > 0)
                    {
                        if (e.Column == colCostCentre)
                        {
                            if (CheckCostcentreEnabled(LedgerId))
                                e.Appearance.BackColor = Color.DarkOrange;
                            else
                                e.Appearance.BackColor = Color.Empty;
                        }
                        else if (colGSTLedgerClass.Visible && e.Column == colGSTLedgerClass) //02/12/2019, to lock GST column, if selected ledger is not GST ledger
                        {
                            //if (LedgerId > 0 && ledgerMappedDefaultGSTID == 0)
                            //{
                            //    e.Appearance.BackColor = Color.LightGray;
                            //}
                            //else
                            //{
                            //    e.Appearance.BackColor = Color.Empty;
                            //}
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            finally { }
        }

        private void rbtnCostCentreView_Click(object sender, EventArgs e)
        {
            if (rgTransactionType.SelectedIndex != 2 && LedgerAmount > 0 && LedgerId > 0)
            {
                ShowCostCentre(this.UtilityMember.NumberSet.ToDouble(LedgerAmount.ToString()));
            }
        }

        private void rbtnDeleteTransaction_Click(object sender, EventArgs e)
        {
            DeleteTransaction();
        }

        private void rtxtAmount_Enter(object sender, EventArgs e)
        {
            //rtxtAmount.Mask.MaskType = MaskType.RegEx;
            //rtxtAmount.Mask.EditMask = @"\d*\.?\d*";
        }

        private void rtxtAmount_Validating(object sender, CancelEventArgs e)
        {
            //rtxtAmount.Mask.MaskType = MaskType.Numeric;
            //rtxtAmount.Mask.EditMask = "c";
        }

        private void rtxtAmt_Leave(object sender, EventArgs e)
        {
            if (this.BudgetId != 0)
            {
                if (IsValidaTransactionRow())
                {
                    BalanceProperty dAmt = FetchBudgetAmount(LedgerId);
                    if (dAmt.Amount > 0 && dAmt.Amount != 0)
                    {
                        BalanceProperty Balance = FetchBudgetLedgerBalance(LedgerId);
                        double amt = Balance.Amount;

                        double OldValue = 0;
                        double NewValue = 0;
                        double finalCurrentBalance = 0;
                        double BothOldNewValue = 0;
                        DataTable dtTrans = gcTransaction.DataSource as DataTable;

                        if (dtTrans != null)
                        {
                            NewValue = GetCalculatedAmount(LedgerId, dtTrans);
                            OldValue = GetCalculatedTempAmount(LedgerId, dtTrans);
                            finalCurrentBalance = FetchLedgerBalanceData(LedgerId, dtTrans);
                            double CurrentBudgetBalance = Math.Abs(OldValue - NewValue);

                            // commanded on Jan 2019
                            //BothOldNewValue = Math.Abs(amt + OldValue - NewValue);

                            // commanded on Mar 2019
                            // BothOldNewValue = Math.Abs(amt + CurrentBudgetBalance);

                            BothOldNewValue = finalCurrentBalance;
                        }

                        // commanded by chinna Mar 2019
                        //if (dAmt.Amount < BothOldNewValue)
                        if (dAmt.Amount < BothOldNewValue)
                        {
                            //this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.TRANSACTION_AMOUNT_EXCEEDS));
                            //this.ShowMessageBox("The Transaction exceeds the Approved Budget");
                            //gvTransaction.FocusedColumn = colLedger;
                            //Temp 28/02/2020
                            rtxtAmt.Tag = "BudgetAlert";
                        }
                    }
                }
            }

            // it removed here while leave the Amount and added into Real Columns while Edit the Transaction (09.05.2019)
            AssignGSTAmount(LedgerGSTClassId); //29/11/2019, To set Ledger GST Ledger Class
        }

        private void gvTransaction_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            //On 13/08/2019, to set focused column border color -----------------------------------------------------------------------------
            if (gvTransaction.IsEditing && gvTransaction.FocusedColumn == e.Column && gvTransaction.FocusedRowHandle == e.RowHandle)
            {
                Rectangle rect = e.Bounds;
                rect.Inflate(new Size(1, 1));

                SolidBrush blueBrush = new SolidBrush(Color.Black);

                e.Graphics.FillRectangle(Brushes.Black, rect);
                e.Handled = true;
            }
            //---------------------------------------------------------------------------------------------------------------------------------


        }

        private void gvBank_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            //On 13/08/2019, to set focused column border color -----------------------------------------------------------------------------
            if (gvBank.IsEditing && gvBank.FocusedColumn == e.Column && gvBank.FocusedRowHandle == e.RowHandle)
            {
                Rectangle rect = e.Bounds;
                rect.Inflate(new Size(1, 1));
                e.Graphics.FillRectangle(Brushes.Black, rect);
                e.Handled = true;
            }
            //---------------------------------------------------------------------------------------------------------------------------------
        }
        #endregion

        #region Cash/Bank Grid Events
        private void gvBank_GotFocus(object sender, EventArgs e)
        {
            BaseView view = sender as BaseView;
            if (view == null)
                return;

            if (MouseButtons == System.Windows.Forms.MouseButtons.Left)
                return;
            view.ShowEditor();
            TextEdit editor = view.ActiveEditor as TextEdit;
            if (editor != null)
            {
                editor.SelectAll();
                editor.Focus();
            }
        }

        private void rglkpCashLedger_Validating(object sender, CancelEventArgs e)
        {
            GridLookUpEdit gridLKPEdit = sender as GridLookUpEdit;
            int Group = 0;
            int CashBankCurrencyId = VoucherCurrencyCountryId;

            if (gridLKPEdit.EditValue != null)
            {
                int LedgerID = this.UtilityMember.NumberSet.ToInteger(gridLKPEdit.EditValue.ToString());

                DataRowView drvLedger = rglkpCashLedger.GetRowByKeyValue(LedgerID) as DataRowView;
                if (drvLedger != null)
                {
                    Group = this.UtilityMember.NumberSet.ToInteger(drvLedger["GROUP_ID"].ToString());  //CashBankGroupId;//ledgerSystem.FetchLedgerGroupById();
                    CashBankCurrencyId = this.UtilityMember.NumberSet.ToInteger(drvLedger["CUR_COUNTRY_ID"].ToString());
                }

                gvBank.SetFocusedRowCellValue(colCashLedger, LedgerID);

                if (TransSummaryVal > 0 && CashTransSummaryVal <= TransSummaryVal && CashLedgerAmount < 1)
                {
                    double Amt = TransSummaryVal - CashTransSummaryVal;
                    //On 03/12/2024, To propse amount when same currency contra
                    if (this.AppSetting.AllowMultiCurrency == 0 || CashBankCurrencyId == VoucherCurrencyCountryId)
                    {
                        gvBank.SetFocusedRowCellValue(colCashAmount, Amt.ToString());
                    }
                    gvBank.SetFocusedRowCellValue(colCashLedger, LedgerID);
                    gvBank.PostEditor();
                    gvBank.UpdateCurrentRow();
                }

                //On 11/11/2024 to set exchange and currency id
                if (this.AppSetting.AllowMultiCurrency == 1)
                {
                    //Int32 currencycountry = glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString());
                    gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCashBankExchangeAmount, txtExchangeRate.Text);
                    gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colLiveExchangeAmount, txtExchangeRate.Text);

                    //For Contra 
                    if (rgTransactionType.SelectedIndex == 2)
                    {
                        using (CountrySystem countrySystem = new CountrySystem())
                        {
                            ResultArgs result = countrySystem.FetchCountryCurrencyExchangeRateByCountryDate(CashBankCurrencyId, this.UtilityMember.DateSet.ToDate(dtTransactionDate.Text, false));
                            if (result.Success)
                            {
                                gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCashBankExchangeAmount, UtilityMember.NumberSet.ToNumber(countrySystem.ExchangeRate));
                                gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCashBankLiveExchangeAmount, UtilityMember.NumberSet.ToNumber(countrySystem.ExchangeRate));
                            }
                        }
                    }
                    gvBank.UpdateCurrentRow();
                }

                EnableCashBankFields();

            }
        }

        private void rglkpCashLedger_Leave(object sender, EventArgs e)
        {
            if (gvBank.IsFirstRow)
            {
                CalculateFirstRowValue();
            }

            if (CashLedgerId > 0)
            {
                if (VoucherId == 0)
                {
                    gvBank.PostEditor();
                    gvBank.UpdateCurrentRow();
                    DataTable dtCashTrans = gcBank.DataSource as DataTable;
                    string Balance = GetLedgerBalanceValues(dtCashTrans, CashLedgerId); //ShowLedgerBalance(CashLedgerId, dtCashTrans, false);
                    if (Balance != string.Empty)
                    {
                        gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colLedgerBalance, Balance);
                    }
                }
            }
            else { gvBank.UpdateCurrentRow(); }

        }

        private void gcBank_ProcessGridKey(object sender, KeyEventArgs e)
        {
            bool canFocusNameAddress = false;

            // To show Bank transaction reference number details..
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control &&
                            (gvBank.FocusedColumn == colCashCheque))
            {
                gvBank.PostEditor();
                gvBank.UpdateCurrentRow();
                ShowBankChequeDDReferenceNumberDetails(true);
            }

            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab)
                && !e.Shift && !e.Alt && !e.Control
                && (gvBank.FocusedColumn == colCashAmount || gvBank.FocusedColumn == colMaterializedOn))//&& (gvBank.IsLastRow))
            {
                if (TransEntryMethod == VoucherEntryMethod.Single) //single Entry
                {
                    if ((gvBank.FocusedColumn == colMaterializedOn && CashBankGroupId == (int)FixedLedgerGroup.BankAccounts)
                        || (gvBank.FocusedColumn == colCashAmount && CashBankGroupId == (int)FixedLedgerGroup.Cash || CashBankGroupId == 0) && gvBank.IsLastRow)
                    {
                        canFocusNameAddress = true;
                    }
                }
                else  //Multi Entry
                {
                    if (CashLedgerId == 0 && CashLedgerAmount == 0) { canFocusNameAddress = true; }
                    if (CashTransSummaryVal == TransSummaryVal)
                    {
                        if ((gvBank.FocusedColumn == colMaterializedOn && CashBankGroupId == (int)FixedLedgerGroup.BankAccounts)
                            || (gvBank.FocusedColumn == colCashAmount && CashBankGroupId == (int)FixedLedgerGroup.Cash))
                        {
                            if (gvBank.IsLastRow) { canFocusNameAddress = true; }
                            else { gvBank.MoveNext(); gvBank.FocusedColumn = colCashLedger; }
                        }
                    }
                    else if ((gvBank.FocusedColumn == colMaterializedOn && CashBankGroupId == (int)FixedLedgerGroup.BankAccounts)
                            || (gvBank.FocusedColumn == colCashAmount && CashBankGroupId == (int)FixedLedgerGroup.Cash))
                    {
                        if (gvBank.IsLastRow) { CashBankTransGridNewItem = TransEntryMethod; }
                        else { gvBank.MoveNext(); gvBank.FocusedColumn = colCashLedger; }
                        if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                        {
                            e.SuppressKeyPress = true;
                        }
                    }
                }

                if (canFocusNameAddress)
                {
                    gvBank.CloseEditor();
                    e.Handled = true;
                    e.SuppressKeyPress = true;

                    txtNarration.Select();
                    txtNarration.Focus();
                }

                if (CashTransSummaryVal != TransSummaryVal &&
                TransEntryMethod == VoucherEntryMethod.Multi &&
                (gvBank.FocusedColumn == colMaterializedOn && CashBankGroupId == (int)FixedLedgerGroup.BankAccounts) ||
                (gvBank.FocusedColumn == colCashAmount && CashBankGroupId == (int)FixedLedgerGroup.Cash) && gvBank.IsLastRow && !canFocusNameAddress)
                {
                    CashBankTransGridNewItem = TransEntryMethod;
                }

            }
            else if (gvBank.IsFirstRow && gvBank.FocusedColumn == colCashLedger && e.Shift && e.KeyCode == Keys.Tab)
            {
                FocusTransactionGrid();
            }
        }

        private void gvBank_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (gvBank.GetRowCellValue(e.RowHandle, colCashLedger) != null)
                {
                    int GroupId = 0;
                    int LedgerId = this.UtilityMember.NumberSet.ToInteger(gvBank.GetRowCellValue(e.RowHandle, colCashLedgerId).ToString());
                    if (LedgerId > 0)
                    {
                        DataRowView drvLedger = rglkpCashLedger.GetRowByKeyValue(LedgerId) as DataRowView;
                        if (drvLedger != null)
                        {
                            GroupId = this.UtilityMember.NumberSet.ToInteger(drvLedger["GROUP_ID"].ToString());  //CashBankGroupId;//ledgerSystem.FetchLedgerGroupById();
                        }

                        if ((e.Column == colCashCheque || e.Column == colMaterializedOn) &&
                        (GroupId == (int)FixedLedgerGroup.Cash))
                        {
                            e.Appearance.BackColor = Color.LightGray;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            finally { }
        }

        private void gvBank_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
                if (CashLedgerId > 0)
                {
                    if ((CashBankGroupId == (int)FixedLedgerGroup.Cash)
                        && (gvBank.FocusedColumn == colCashCheque || gvBank.FocusedColumn == colMaterializedOn))
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            finally { }
        }

        #endregion

        #region Right ShortCut Events
        private void ucRightShortcut_BankAccountClicked(object sender, EventArgs e)
        {
            ShowBankAccountForm();
        }

        private void ucRightShortcut_ConfigureClicked(object sender, EventArgs e)
        {
            ShowDonorForm();
        }

        private void ucRightShortcut_ContraClicked(object sender, EventArgs e)
        {
            ShowVoucherType(DefaultVoucherTypes.Contra);
        }

        private void ucRightShortcut_CostCentreClicked(object sender, EventArgs e)
        {
            ShowCostCentreForm();
        }

        private void ucRightShortcut_DateClicked(object sender, EventArgs e)
        {
            dtTransactionDate.Focus();
        }

        private void ucRightShortcut_DonorClicked(object sender, EventArgs e)
        {
            frmFinanceSetting setting = new frmFinanceSetting();
            setting.ShowDialog();
            if (setting.DialogResult == DialogResult.OK)
            {
                GridRefreshAfterFinanceSettingChanged();
            }
        }

        private void ucRightShortcut_JournalClicked(object sender, EventArgs e)
        {
            LoadJournal();
        }

        private void ucRightShortcut_LedgerClicked(object sender, EventArgs e)
        {
            ShowLedgerForm();
        }

        protected override void OnEditHeld(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucRightShortcut_MappingClicked(object sender, EventArgs e)
        {
            frmMapProjectLedger obj = new frmMapProjectLedger();
            obj.ShowDialog();
            if (obj.DialogResult == DialogResult.OK)
            {
                LoadSelectedLedger();
                LoadPurposeDetails();
                LoadDonorDetails();
            }
        }

        private void ucRightShortcut_PaymentClicked(object sender, EventArgs e)
        {
            ShowVoucherType(DefaultVoucherTypes.Payment);
        }

        private void ucRightShortcut_ProjectClicked(object sender, EventArgs e)
        {
            ShowProjectSelectionWindow();
            if (TransVoucherMethod == (int)TransactionVoucherMethod.Automatic && VoucherId == 0)
            {
                LoadVoucherNo();
            }
            else if (TransVoucherMethod == (int)TransactionVoucherMethod.Manual && VoucherId == 0)
            {
                txtVoucher.Text = string.Empty;
            }
        }

        private void ucRightShortcut_ReceiptsClicked(object sender, EventArgs e)
        {
            ShowVoucherType(DefaultVoucherTypes.Receipt);
        }

        private void ucRightShortcut_TransactionVoucherViewClicked(object sender, EventArgs e)
        {
            frmTransactionVoucherView voucview = new frmTransactionVoucherView(projectName, ProjectId, dtTransactionDate.DateTime, false);
            voucview.VoucherEditHeld += new EventHandler(OnEditHeld);
            voucview.ShowDialog();
        }

        private void ucRightShortcut_LedgerAddClicked(object sender, EventArgs e)
        {
            ShowLedgerForm();
        }

        private void ucRightShortcut_LedgerOptionsClicked(object sender, EventArgs e)
        {
            ShowLedgerOptionsForm();
        }
        #endregion

        #region Bottom Shortcut Events
        private void bbiDeleteTrans_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteTransaction();
        }

        private void bbiDeleteCashBank_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteCashBankTransaction();
        }

        private void bbiMoveToTrans_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FocusTransactionGrid();
        }

        private void bbiMoveToCashBank_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FocusCashTransactionGrid();
        }

        private void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void bbiNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            VoucherId = 0;
            ClearControls();
        }

        private void bbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Extra Info ShortCut
        private void ucAdditionalInfo_EntryMethodClicked(object sender, EventArgs e)
        {
            ShowEntryMethod();
        }

        private void ucAdditionalInfo_DonorClicked(object sender, EventArgs e)
        {
            ShowDonorAdditionalInfo();
        }
        #endregion

        #region  ShortCut
        private void glkpDonor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
                if (txtVoucher.Enabled)
                {
                    txtVoucher.Select();
                    txtVoucher.Focus();
                }
                else
                    dtTransactionDate.Focus();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                if (glkpReceiptType.Enabled)
                {
                    glkpReceiptType.Select();
                    glkpReceiptType.Focus();
                }
                else
                {
                    FocusTransactionGrid();
                }
                e.SuppressKeyPress = true;
            }
            else
                ProcessShortcutKeys(e);
        }

        private void glkpReceiptType_KeyDown(object sender, KeyEventArgs e)
        {
            ProcessShortcutKeys(e);
        }

        private void glkpPurpose_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
                if (glkpReceiptType.Enabled)
                {
                    glkpReceiptType.Focus();
                }
                else
                    dtTransactionDate.Focus();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                if (glkpCurrencyCountry.Enabled)
                {
                    glkpCurrencyCountry.Focus();
                }
                else
                {
                    FocusTransactionGrid();
                }
                e.SuppressKeyPress = true;
            }
            else
                ProcessShortcutKeys(e);
        }

        private void lkpCountry_KeyDown(object sender, KeyEventArgs e)
        {
            ProcessShortcutKeys(e);

            //if (AppSetting.AllowMultiCurrency==1 && e.Shift && e.KeyCode == Keys.Tab)
            //{
            //    this.dtTransactionDate.Select();
            //    this.dtTransactionDate.Focus();
            //}
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            ProcessShortcutKeys(e);
        }

        private void txtExchangeRate_KeyDown(object sender, KeyEventArgs e)
        {
            ProcessShortcutKeys(e);
        }

        private void txtActualAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
                txtExchangeRate.Select();
                txtExchangeRate.Focus();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                FocusTransactionGrid();
                e.SuppressKeyPress = true;
            }
            else
                ProcessShortcutKeys(e);
        }

        private void dtTransactionDate_KeyDown(object sender, KeyEventArgs e)
        {
            //  if (e.Shift && e.KeyCode == Keys.Tab) { FocusCashTransactionGrid(); e.SuppressKeyPress = true; }
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                if (txtVoucher.Enabled)
                {
                    txtVoucher.Focus();
                }
                else if (lciDonorInfo.Visibility == LayoutVisibility.Always)
                {
                    if (rgTransactionType.SelectedIndex == 0)
                    {
                        glkpDonor.Focus();
                    }
                    else
                    {
                        glkpPurpose.Focus();
                    }
                }
                else if (this.AppSetting.AllowMultiCurrency == 1)
                {
                    glkpCurrencyCountry.Select();
                    glkpCurrencyCountry.Focus();
                }
                else
                {
                    FocusTransactionGrid();
                }
                e.SuppressKeyPress = true;
            }
        }

        private void txtVoucher_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
                txtExchangeRate.Select();
                txtExchangeRate.Focus();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                if (lciDonorInfo.Visibility != LayoutVisibility.Always)
                {
                    FocusTransactionGrid();
                    e.SuppressKeyPress = true;
                }
                else
                {
                    glkpDonor.Select();
                    glkpDonor.Focus();
                    e.SuppressKeyPress = true;
                }
            }
        }

        private void glkpDonor_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;

        }
        #endregion

        #region Trans Events
        private void gvTransaction_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            double dAmt = GetTransSummaryAmount();
            if (dAmt >= 0)
            {
                e.TotalValue = dAmt;
            }
            else
            {
                e.TotalValue = 0;
            }
        }

        private void rbtnDeleteTransaction_DoubleClick(object sender, EventArgs e)
        {
            DeleteTransaction();
        }

        private void ucAdditionalInfo_DeleteVoucherClicked(object sender, EventArgs e)
        {
            DeleteVoucherById(e);
        }

        public void DeleteVoucherById(EventArgs e)
        {
            try
            {
                using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                {
                    if (!IsLockedTransaction(dtTransactionDate.DateTime) && ucAdditionalInfo.LockDeleteVocuher)
                    {
                        voucherTransaction.VoucherId = VoucherId;
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            resultArgs = voucherTransaction.DeleteVoucherTrans();
                            if (resultArgs.Success)
                            {
                                if (UpdateHeld != null)
                                {
                                    UpdateHeld(this, e);
                                }
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                this.Close();
                            }
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED_CANNOT_DELETE) + "'" + ProjectName + "'" +
                            //  " during the period " + this.UtilityMember.DateSet.ToDate(dtLockDateFrom.ToShortDateString()) +
                            this.GetMessage(MessageCatalog.Transaction.VocherTransaction.DURING_PERIOD) + this.UtilityMember.DateSet.ToDate(dtLockDateFrom.ToShortDateString()) +
                                " - " + this.UtilityMember.DateSet.ToDate(dtLockDateTo.ToShortDateString())
                                    );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void rbtnDeleteBank_Click(object sender, EventArgs e)
        {
            DeleteCashBankTransaction();
        }

        private void ucRightShortcut_NextVoucherDateClicked(object sender, EventArgs e)
        {
            DateTime dtYearTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            dtTransactionDate.DateTime = (dtTransactionDate.DateTime < dtYearTo) ? dtTransactionDate.DateTime.AddDays(1) : dtYearTo;
        }

        private void SetTextEditBackColor(TextEdit txtEdit)
        {
            txtEdit.BackColor = Color.Thistle;
        }

        private void gcTransaction_Enter(object sender, EventArgs e)
        {
            //gvTransaction.Appearance.FocusedCell.BackColor = Color.LightBlue;
            //gvTransaction.OptionsSelection.EnableAppearanceFocusedCell = true;
        }

        private void gcTransaction_Leave(object sender, EventArgs e)
        {
            // gvTransaction.Appearance.FocusedCell.BackColor = Color.Empty;
            //gvTransaction.OptionsSelection.EnableAppearanceFocusedCell = false;
        }

        private void gcBank_Enter(object sender, EventArgs e)
        {
            //gvBank.Appearance.FocusedCell.BackColor = Color.LightBlue;
        }

        private void gcBank_Leave(object sender, EventArgs e)
        {
            //gvBank.Appearance.FocusedCell.BackColor = Color.Empty;
            //gvBank.OptionsSelection.EnableAppearanceFocusedCell = false;
        }

        private void rglkpLedger_EditValueChanged(object sender, EventArgs e)
        {
            //To select the Ledger Using Mouse Click
            if (isMouseClicked)
            {
                SendKeys.Send("{tab}"); isMouseClicked = false;
            }
        }

        private void rglkpCashLedger_EditValueChanged(object sender, EventArgs e)
        {
            //To retain the ledger in  the Cash/Bank Grid
            if (isMouseClicked)
            {
                SendKeys.Send("{tab}"); isMouseClicked = false;
            }
            //BaseEdit bs = sender as BaseEdit;
            //BankLedgerName = bs.Text;
        }

        private void rglkpLedger_MouseDown(object sender, MouseEventArgs e)
        {
            //To Identify the Mouse Click Event
            if (e.Button == MouseButtons.Left)
                isMouseClicked = true;
        }

        private void rglkpCashLedger_MouseDown(object sender, MouseEventArgs e)
        {
            //To Identify the Mouse Click Event
            if (e.Button == MouseButtons.Left)
                isMouseClicked = true;
        }

        private void dtTransactionDate_Enter(object sender, EventArgs e)
        {
            //01/03/2018, keep date value (To show alert message if selected date is locked for the selected project)
            dePreviousVoucherDate = dtTransactionDate.DateTime;
        }

        private void dtTransactionDate_Validating(object sender, CancelEventArgs e)
        {
            //01/03/2018, To show alert message if selected date is locked for the selected project
            //if (dePreviousVoucherDate != dtTransactionDate.DateTime)
            //{
            if (IsLockedTransaction(dtTransactionDate.DateTime))
            {
                if (VoucherId == 0)
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED) + "'" + ProjectName + "'," +
                            " during the period " + this.UtilityMember.DateSet.ToDate(dtLockDateFrom.ToShortDateString()) +
                            " - " + this.UtilityMember.DateSet.ToDate(dtLockDateTo.ToShortDateString()));
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED_CANNOT_EDIT) + "'" + ProjectName + "'," +
                            " during the period " + this.UtilityMember.DateSet.ToDate(dtLockDateFrom.ToShortDateString()) +
                            " - " + this.UtilityMember.DateSet.ToDate(dtLockDateTo.ToShortDateString()));
                }
            }
            //}

            // On 12/12/2024 - get Live exchange rate, if we received live exchange rate, let us have live exchange rate
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                AssignLiveExchangeRate();
            }
        }

        #endregion


        private void rglkpLedgerGST_EditValueChanged(object sender, EventArgs e)
        {
            //29/11/2019, To set Ledger GST Ledger Class
            GridLookUpEdit edit = sender as GridLookUpEdit;
            if (edit != null)
            {
                LedgerGSTClassId = this.UtilityMember.NumberSet.ToInteger(edit.EditValue.ToString());
                AssignGSTAmount(LedgerGSTClassId);
                CalculateFirstRowValue();
            }
        }

        private void gcTransaction_Validated(object sender, EventArgs e)
        {
            //AlertBudgetMessage();
        }

        private void gcTransaction_Validating(object sender, CancelEventArgs e)
        {
            //For Tmep 28/08/2020
            AlertBudgetMessage();
        }

        private void rglkpLedger_QueryPopUp(object sender, CancelEventArgs e)
        {
            //19/07/2021, To set Popup widow size
            if (sender != null)
            {
                GridLookUpEdit editor = (GridLookUpEdit)sender;
                SetGridLookPopupWindowSize(editor);
            }
        }

        private void rglkpCashLedger_QueryPopUp(object sender, CancelEventArgs e)
        {
            //19/07/2021, To set Popup widow size
            if (sender != null)
            {
                GridLookUpEdit editor = (GridLookUpEdit)sender;
                SetGridLookPopupWindowSize(editor);
            }
        }

        private void glkpPurpose_QueryPopUp(object sender, CancelEventArgs e)
        {
            //19/07/2021, To set Popup widow size
            if (sender != null)
            {
                GridLookUpEdit editor = (GridLookUpEdit)sender;
                SetGridLookPopupWindowSize(editor);
            }
        }

        #endregion

        #region Methods
        private void RealColumnEditTransAmount()
        {
            colAmount.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditTransAmount_EditValueChanged);
            this.gvTransaction.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvTransaction.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colAmount)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvTransaction.ShowEditorByMouse();
                    }));
                }
            };
        }

        private void RealColumnEditCashTransAmount()
        {
            colCashAmount.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditCashTransAmount_EditValueChanged);
            this.gvBank.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvBank.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colCashAmount)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvBank.ShowEditorByMouse();
                    }));
                }
            };
        }

        private void LoadTransBasic()
        {
            SetTitle();
            SetDefaults();
            LoadNarrationAutoComplete();
            LoadNameAddressAutoComplete();
            LoadDonorDetails();
            if ((int)Division.Foreign == DivisionId)
            {
                ShowDonorAdditionalInfoForeignProjects();
            }
            LoadPurposeDetails();
            LoadCountry();
            ConstructEmptyDataSource();
            if (this.AppSetting.TransMode == "2") { LoadTransSource(); }
            else { LoadTransactionMode(); }
            // LoadSelectedLedger();
            LoadReceiptType();
            FetchVoucherMethod();
            // ShowHideDonor();
            SetBudgetInfo();

            // Set Zero values while opening another new transaction (chinna 04.09.2017)
            gstCalcAmount = 0.0;
            cgstCalcAmount = 0.0;
            sgstCalcAmount = 0.0;
            igstCalcAmount = 0.0;

            LoadPendingGSTInvoices();

            AssignValues();

            ShowTransGST();

            ShowCashBankGST();

            ShowTransReference();

            if (VoucherId == 0)
            {
                TransEntryMethod = this.AppSetting.TransEntryMethod == "1" ? VoucherEntryMethod.Single : VoucherEntryMethod.Multi;
            }
            else
            {
                TransEntryMethod = (this.AppSetting.TransEntryMethod == "2" || gvTransaction.RowCount > 1) ? VoucherEntryMethod.Multi : VoucherEntryMethod.Single;
            }
            GetUserControlInput();
            ClearTableValues();
            FetchDateDuration();
            ShowHideGSTPAN();
            //FetchUpdateReferenceNo();
        }

        private void ShowHideGSTPAN()
        {
            if (this.AppSetting.ShowGSTPan == "1")
            {
                lciGroupPanGST.Visibility = LayoutVisibility.Always;
                lciGroupPanGST.TextVisible = false;
            }
            else
            {
                lciGroupPanGST.Visibility = LayoutVisibility.Never;
            }
        }
        private void ClearTableValues()
        {
            //this.AppSetting.CashTransInfo.ToTable() = null;
            //this.AppSetting.TransInfo.ToTable().Clear();
            //this.AppSetting.TDSPaymentBank.Clear();
            //this.AppSetting.TDSPayment.Clear();
            //this.AppSetting.TDSPartyPayment.Clear();
            //this.AppSetting.TDSPartyPaymentBank.Clear();
        }

        private void ConstructEmptyDataSource()
        {
            ConstructTransEmptySource();
            ConstructCashTransEmptySournce();
        }

        private void ConstructTransEmptySource()
        {
            DataTable dtTransaction = new DataTable();
            dtTransaction.Columns.Add("SOURCE", typeof(string));
            dtTransaction.Columns.Add("LEDGER_ID", typeof(Int32));
            dtTransaction.Columns.Add("LIVE_EXCHANGE_RATE", typeof(decimal));
            dtTransaction.Columns.Add("EXCHANGE_RATE", typeof(decimal));
            dtTransaction.Columns.Add("AMOUNT", typeof(decimal));
            dtTransaction.Columns.Add("LEDGER_GST_CLASS_ID", typeof(Int32));
            dtTransaction.Columns.Add("CHEQUE_NO", typeof(string));
            dtTransaction.Columns.Add("MATERIALIZED_ON", typeof(DateTime));
            dtTransaction.Columns.Add("LEDGER_BALANCE", typeof(string));
            dtTransaction.Columns.Add("NARRATION", typeof(string));
            dtTransaction.Columns.Add("BUDGET_AMOUNT", typeof(string));
            dtTransaction.Columns.Add("GST_AMOUNT", typeof(string));
            dtTransaction.Columns.Add("GST", typeof(decimal));
            dtTransaction.Columns.Add("CGST", typeof(decimal));
            dtTransaction.Columns.Add("SGST", typeof(decimal));
            dtTransaction.Columns.Add("IGST", typeof(decimal));
            dtTransaction.Columns.Add("TEMP_AMOUNT", typeof(decimal));
            dtTransaction.Columns.Add("REFERENCE_NUMBER", typeof(string));
            // dtTransaction.Columns.Add("REF_AMOUNT", typeof(decimal));
            // dtTransaction.Columns.Add("REF_LEDGER_ID", typeof(Int32));
            dtTransaction.Columns.Add("CHEQUE_REF_DATE", typeof(DateTime));
            dtTransaction.Columns.Add("CHEQUE_REF_BANKNAME", typeof(string));
            dtTransaction.Columns.Add("CHEQUE_REF_BRANCH", typeof(string));
            dtTransaction.Columns.Add("FUND_TRANSFER_TYPE_NAME", typeof(string));
            dtTransaction.Columns.Add("IS_GST_LEDGERS", typeof(Int32));
            dtTransaction.Rows.Add(dtTransaction.NewRow());

            gcTransaction.DataSource = dtTransaction;
            gvTransaction.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;

            int sourceId = (rgTransactionType.SelectedIndex == 0 || rgTransactionType.SelectedIndex == 2) ? (int)Source.To : (int)Source.By;
            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colSource, sourceId);
        }

        private void ConstructCashTransEmptySournce()
        {
            DataTable dtCashTransaction = new DataTable();
            dtCashTransaction.Columns.Add("SOURCE", typeof(string));
            dtCashTransaction.Columns.Add("LEDGER_FLAG", typeof(string));
            dtCashTransaction.Columns.Add("LEDGER_ID", typeof(Int32));
            dtCashTransaction.Columns.Add("LIVE_EXCHANGE_RATE", typeof(decimal));
            dtCashTransaction.Columns.Add("EXCHANGE_RATE", typeof(decimal));
            dtCashTransaction.Columns.Add("AMOUNT", typeof(decimal));
            dtCashTransaction.Columns.Add("LEDGER_GST_CLASS_ID", typeof(Int32));
            dtCashTransaction.Columns.Add("CHEQUE_NO", typeof(string));
            dtCashTransaction.Columns.Add("MATERIALIZED_ON", typeof(DateTime));
            dtCashTransaction.Columns.Add("LEDGER_BALANCE", typeof(string));
            dtCashTransaction.Columns.Add("BUDGET_AMOUNT", typeof(string));
            dtCashTransaction.Columns.Add("GST_AMOUNT", typeof(string));
            dtCashTransaction.Columns.Add("GST_TOTAL", typeof(string));
            dtCashTransaction.Columns.Add("GST", typeof(decimal));
            dtCashTransaction.Columns.Add("CGST", typeof(decimal));
            dtCashTransaction.Columns.Add("SGST", typeof(decimal));
            dtCashTransaction.Columns.Add("IGST", typeof(decimal));
            dtCashTransaction.Columns.Add("TEMP_AMOUNT", typeof(decimal));
            dtCashTransaction.Columns.Add("REFERENCE_NUMBER", typeof(string));
            dtCashTransaction.Columns.Add("CHEQUE_REF_DATE", typeof(DateTime));
            dtCashTransaction.Columns.Add("CHEQUE_REF_BANKNAME", typeof(string));
            dtCashTransaction.Columns.Add("CHEQUE_REF_BRANCH", typeof(string));
            dtCashTransaction.Columns.Add("FUND_TRANSFER_TYPE_NAME", typeof(string));

            dtCashTransaction.Rows.Add(dtCashTransaction.NewRow());

            gcBank.DataSource = dtCashTransaction;
            gvBank.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            int sourceId = (rgTransactionType.SelectedIndex == 0 || rgTransactionType.SelectedIndex == 2) ? (int)Source.By : (int)Source.To;
            gvBank.MoveFirst();
            gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCashSource, sourceId);
        }

        private void SetDefaults()
        {
            DateTime dtProjectFrom;
            DateTime dtProjectTo;
            DateTime dtyearfrom;
            DateTime dtbookbeginfrom;
            DateTime dtYearTo;
            DateTime dtRecentVoucherDate;


            // rglkpLedger.PopupFormSize = new Size(colLedger.Width, rglkpLedger.PopupFormSize.Height);
            ucCaptionPanel1.Caption = ProjectName;
            lblDonorCurrency.Text = this.AppSetting.Currency;
            colSource.OptionsColumn.AllowEdit = (this.AppSetting.EnableTransMode == "1") ? true : false;
            colSource.OptionsColumn.AllowFocus = (this.AppSetting.EnableTransMode == "1") ? true : false;
            //ucRightShortcut.DiableLedger = BarItemVisibility.Never;
            if (VoucherId != 0)
            {
                ucAdditionalInfo.DisableDeleteVocuher = BarItemVisibility.Always;
                ucAdditionalInfo.DisablePrintVoucher = BarItemVisibility.Always;
            }
            else
            {
                ucAdditionalInfo.DisableDeleteVocuher = BarItemVisibility.Never;
                ucAdditionalInfo.DisablePrintVoucher = BarItemVisibility.Never;
            }
            dtyearfrom = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtbookbeginfrom = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false);
            dtYearTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            dtRecentVoucherDate = UtilityMember.DateSet.ToDate(RecentVoucherDate, false);

            ResultArgs result = FetchProjectDetails();

            DataView dvResult = result.DataSource.Table.DefaultView;
            dvResult.RowFilter = "PROJECT_ID=" + ProjectId;
            DataTable dtResult = dvResult.ToTable();
            DivisionId = this.UtilityMember.NumberSet.ToInteger(dtResult.Rows[0]["DIVISION_ID"].ToString());
            dvResult.RowFilter = "";
            if (dtResult.Rows.Count > 0)
            {
                DataRow drProject = dtResult.Rows[0];

                string sProjectFrom = drProject["DATE_STARTED"].ToString();
                string sProjectTo = drProject["DATE_CLOSED"].ToString();

                dtProjectFrom = (!string.IsNullOrEmpty(sProjectFrom)) ? this.UtilityMember.DateSet.ToDate(sProjectFrom, false) : dtyearfrom;

                if (!string.IsNullOrEmpty(sProjectTo))
                {
                    dtProjectTo = this.UtilityMember.DateSet.ToDate(sProjectTo, false);
                    //Added by Carmel Raj M on 20-Oct-2015
                    //Purpose: Setting Project closing date as max value
                    deMaxDate = dtProjectTo;
                }
                else
                {
                    dtProjectTo = dtProjectFrom > dtYearTo ? dtProjectFrom : dtYearTo;
                    //Added by Carmel Raj M on 20-Oct-2015
                    //Setting year to as max value
                    deMaxDate = dtProjectTo;
                }
                if ((dtProjectFrom < dtyearfrom && dtProjectTo < dtyearfrom) || (dtProjectFrom > dtYearTo && dtProjectTo > dtYearTo))
                {
                    dtTransactionDate.Properties.MinValue = dtbookbeginfrom > dtyearfrom ? dtbookbeginfrom : dtyearfrom;
                    dtTransactionDate.Properties.MaxValue = dtYearTo;
                    dtTransactionDate.DateTime = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom.ToString(), false);
                    //this.ShowMessageBox("Project Start Date and Closed Date does not fall between the Accounting Period");
                    //this.Close();
                }
                else
                {
                    //To set transaction minimum
                    if (!string.IsNullOrEmpty(sProjectFrom))
                    {
                        if (dtbookbeginfrom > dtyearfrom)
                        {
                            dtTransactionDate.Properties.MinValue = (dtProjectFrom > dtbookbeginfrom) ? dtProjectFrom : dtbookbeginfrom;
                        }
                        else
                        {
                            dtTransactionDate.Properties.MinValue = (dtProjectFrom > dtyearfrom) ? dtProjectFrom : dtyearfrom;
                        }
                    }
                    else
                    {
                        dtTransactionDate.Properties.MinValue = dtTransactionDate.DateTime = dtbookbeginfrom > dtyearfrom ? dtbookbeginfrom : dtyearfrom;
                    }

                    //To set transaction maximum
                    if (!string.IsNullOrEmpty(sProjectTo))
                    {
                        dtTransactionDate.Properties.MaxValue = dtProjectTo < dtYearTo ? dtProjectTo : dtYearTo;
                    }
                    else
                    {
                        dtTransactionDate.Properties.MaxValue = dtYearTo;
                    }

                    dtTransactionDate.DateTime = (dtRecentVoucherDate >= dtTransactionDate.Properties.MinValue && dtRecentVoucherDate <=
                        dtTransactionDate.Properties.MaxValue) ? dtRecentVoucherDate : dtTransactionDate.Properties.MinValue;
                    rdeMaterializedOn.MinValue = dtTransactionDate.DateTime;
                    rdtMaterializedOn.MinValue = dtTransactionDate.DateTime;
                }
            }
        }

        private ResultArgs FetchProjectDetails()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    resultArgs = mappingSystem.FetchProjectsLookup();
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
            return resultArgs;
        }

        private BalanceProperty FetchBudgetAmount(int LedgerId)
        {
            BalanceProperty balance = new BalanceProperty();
            ResultArgs result = new ResultArgs();
            using (VoucherTransactionSystem voucher = new VoucherTransactionSystem())
            {
                voucher.ProjectId = ProjectId;
                voucher.VoucherDate = dtTransactionDate.DateTime;
                voucher.LedgerId = LedgerId;
                voucher.BudgetTypeId = BudgetTypeId;
                voucher.BudgetMonthDistribution = BudgetMonthDistribution;
                voucher.TransMode = GetTransMode();

                // Chinna 02/05/2024 for having multiple
                // Assign for fetch Active Date from and date to, it used in the Fetch Budget Amount

                voucher.BudgetDateFrom = budgetDateFrom.ToShortDateString();
                voucher.BudgetDateTo = budgetDateTo.ToShortDateString();

                BalanceProperty budgetAmount = voucher.FetchBudgetAmount();

                // In order to multify the Realized Amount for every Month ( Annual Year )
                if (this.AppSetting.IS_CMF_CONGREGATION)
                {
                    balance.Amount = Math.Round((budgetAmount.Amount / 12) * dtTransactionDate.DateTime.Month);
                    balance.TransMode = budgetAmount.TransMode;
                    balance.Result = budgetAmount.Result;
                }
                else
                {
                    balance.Amount = budgetAmount.Amount;
                    balance.TransMode = budgetAmount.TransMode;
                    balance.Result = budgetAmount.Result;
                }
            }
            return balance;
        }

        private void SetTitle()
        {
            if (VoucherId == 0)
            {
                if (rgTransactionType.SelectedIndex == 0)
                {
                    this.Text = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_TRANSACTION_RECEIPT);
                }
                else if (rgTransactionType.SelectedIndex == 1)
                {
                    this.Text = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_TRANSACTION_PAYMENT);
                }
                else
                {
                    this.Text = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_TRANSACTION_CONTRA);
                }
            }
            else
            {
                if (!DuplicateVoucher)
                {
                    this.Text = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_EDIT_CAPTION);
                }
                else
                {
                    this.Text = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOCHER_ENTRY); // "Voucher (Replicate Entry)";
                }

            }
            //this.Text = VoucherId == 0 ? this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_ADD_CAPTION) : this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_EDIT_CAPTION);

            //On 08/04/2022
            if (this.AppSetting.FINAL_RECEIPT_MODULE_STATUS != LCBranchModuleStatus.Approved && rgTransactionType.SelectedIndex == 0)
            {
                this.Text += " - " + MessageCatalog.Common.COMMON_RECEIPT_DISABLED_MESSAGE;
            }
        }

        private void VisibleCashBankAdditionalFields(bool Visible)
        {
            colCashSource.VisibleIndex = 0;
            colCashLedger.VisibleIndex = 1;
            colCashAmount.VisibleIndex = 2;
            colCashCheque.VisibleIndex = 3;
            colMaterializedOn.VisibleIndex = 4;
            colLedgerBalance.VisibleIndex = 5;
            colCBGSTAmount.VisibleIndex = 6;

            if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableGST).Equals((int)YesNo.Yes) && rgTransactionType.SelectedIndex == 0 && rgTransactionType.SelectedIndex == 1)
            {
                colCBGSTAmount.Visible = true;
            }
            else
            {
                colCBGSTAmount.Visible = false;
            }

            ShowTransReference();

            // colBudgetAmt.VisibleIndex = 6;
            colDeleteCashBank.VisibleIndex = 7;

            if (Visible)
            {
                colCashCheque.Visible = true;
                colMaterializedOn.Visible = true;
                if (BudgetId != 0)
                    colBudgetAmount.Visible = true;
            }
            else
            {
                colCashCheque.Visible = false;
                colMaterializedOn.Visible = false;
            }
            colAction.VisibleIndex = 8;

            //On 15/11/2024
            colLedgerBal.Visible = false;
            colLedgerBalance.Visible = false;

            //On 11/11/2024 - To show Currency Exchange and Currency Details
            ShowExchangeRateColumns();
        }

        private void VisibleTransBankAdditionalFields(bool Visible)
        {
            colLedger.VisibleIndex = 1;
            colAmount.VisibleIndex = 2;

            //GSt 28.11.2019
            if (colGStAmt.Visible)
            {
                if (rgTransactionType.SelectedIndex != 2) //02/12/2019
                {
                    colGSTLedgerClass.VisibleIndex = 3;
                }
                else
                {
                    colGSTLedgerClass.Visible = false;
                }
            }
            // Ledger Narration - 27.08.2019 

            if (EnableLedgerNarration)
                colLedgerNarration.VisibleIndex = 4;

            colCheque.VisibleIndex = 5;
            colValueDate.VisibleIndex = 6;

            // reference No
            //if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableRefWiseReceiptANDPayment).Equals((int)YesNo.Yes) && rgTransactionType.SelectedIndex == 0 || rgTransactionType.SelectedIndex == 1)
            if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableRefWiseReceiptANDPayment).Equals((int)YesNo.Yes) && rgTransactionType.SelectedIndex == 1)
            {
                colReferenceNumber.VisibleIndex = 5;
            }
            else
            {
                colReferenceNumber.Visible = false;
            }

            colLedgerBal.VisibleIndex = 7;
            if (BudgetId != 0 && rgTransactionType.SelectedIndex != 2) colBudgetAmount.VisibleIndex = 8;
            else
                colBudgetAmount.Visible = false;
            if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableGST).Equals((int)YesNo.Yes) && rgTransactionType.SelectedIndex == 0 || rgTransactionType.SelectedIndex == 1)
            {
                colGStAmt.VisibleIndex = 9;
            }
            else
            {
                colGStAmt.Visible = false;
                colGStAmt.VisibleIndex = -1;
            }
            colAction.VisibleIndex = 10;
            colCostCentre.VisibleIndex = 11;

            if (Visible)
            {
                colCheque.Visible = true;
                colValueDate.Visible = true;
            }
            else
            {
                colCheque.Visible = false;
                colValueDate.Visible = false;
            }

            ShowTransGST();
            ShowCashBankVisibleGST();
            ShowTransReference();

            //On 15/11/2024
            colLedgerBal.Visible = false;
            colLedgerBalance.Visible = false;
        }

        private void LoadTransSource()
        {
            Source transSource = new Source();
            DataView dvtransSource = this.UtilityMember.EnumSet.GetEnumDataSource(transSource, Sorting.None);
            rglkpSource.DataSource = dvtransSource.ToTable();
            rglkpSource.DisplayMember = "Name";
            rglkpSource.ValueMember = "Id";

            rglkpCashSource.DataSource = dvtransSource.ToTable();
            rglkpCashSource.DisplayMember = "Name";
            rglkpCashSource.ValueMember = "Id";
        }

        private void LoadTransactionMode()
        {
            TransSource trans = new TransSource();
            DataView dvtransSource = this.UtilityMember.EnumSet.GetEnumDataSource(trans, Sorting.None);
            rglkpSource.DataSource = dvtransSource.ToTable();
            rglkpSource.DisplayMember = "Name";
            rglkpSource.ValueMember = "Id";

            rglkpCashSource.DataSource = dvtransSource.ToTable();
            rglkpCashSource.DisplayMember = "Name";
            rglkpCashSource.ValueMember = "Id";
        }

        private void LoadSelectedLedger()
        {
            if (rgTransactionType.SelectedIndex == 0 || rgTransactionType.SelectedIndex == 1)
            {
                LoadLedger(rglkpLedger);
                LoadCashBankLedger(rglkpCashLedger);

                //Ledgers 27.11.2019
                LoadGSTLedgerClass();
            }
            else
            {
                txtCashAmount.NullText = string.Empty;
                LoadCashBankLedger(rglkpLedger);
                LoadCashBankLedger(rglkpCashLedger);
                // LoadCashBankFDLedger(rglkpLedger, (int)YesNo.Yes);
                //  LoadCashBankFDLedger(rglkpCashLedger, (int)YesNo.No);
            }
        }

        private bool IsValidTransactionDate()
        {
            bool isValid = true;
            DateTime dtProjectFrom;
            DateTime dtProjectTo;
            DateTime dtyearfrom;
            DateTime dtbookbeginfrom;
            DateTime dtYearTo;
            DateTime dtRecentVoucherDate;
            dtyearfrom = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtbookbeginfrom = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false);
            dtYearTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            dtRecentVoucherDate = UtilityMember.DateSet.ToDate(RecentVoucherDate, false);

            ResultArgs result = FetchProjectDetails();

            DataView dvResult = result.DataSource.Table.DefaultView;
            dvResult.RowFilter = "PROJECT_ID=" + ProjectId;
            DataTable dtResult = dvResult.ToTable();
            DivisionId = this.UtilityMember.NumberSet.ToInteger(dtResult.Rows[0]["DIVISION_ID"].ToString());
            dvResult.RowFilter = "";
            if (dtResult.Rows.Count > 0)
            {
                DataRow drProject = dtResult.Rows[0];

                string sProjectFrom = drProject["DATE_STARTED"].ToString();
                string sProjectTo = drProject["DATE_CLOSED"].ToString();

                dtProjectFrom = (!string.IsNullOrEmpty(sProjectFrom)) ? this.UtilityMember.DateSet.ToDate(sProjectFrom, false) : dtyearfrom;

                if (!string.IsNullOrEmpty(sProjectTo))
                {
                    dtProjectTo = this.UtilityMember.DateSet.ToDate(sProjectTo, false);
                }
                else
                {
                    dtProjectTo = dtProjectFrom > dtYearTo ? dtProjectFrom : dtYearTo;
                }

                if ((dtProjectFrom < dtyearfrom && dtProjectTo < dtyearfrom) || (dtProjectFrom > dtYearTo && dtProjectTo > dtYearTo))
                {
                    this.ShowMessageBoxWarning(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.DOES_NOT_FALL_BETWEEN_TRANSACTION_PERIOD) + Environment.NewLine + this.GetMessage(MessageCatalog.Transaction.VocherTransaction.CHANGE_THE_PROJECT_DATE));
                    isValid = false;
                    //this.Close();
                }
            }
            return isValid;
        }

        private bool IsRecordDeleted()
        {
            bool isValid = true;
            if (VoucherId > 0)
            {
                using (VoucherTransactionSystem voucher = new VoucherTransactionSystem())
                {
                    string UserName = string.Empty;
                    voucher.VoucherId = VoucherId;
                    resultArgs = voucher.FetchDeletedVoucher();
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        DataTable dtResult = resultArgs.DataSource.Table;
                        DataRow dr = dtResult.Rows[0];
                        UserName = dr["USER_NAME"].ToString();

                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.RECORD_HAS_BEEN_DELETED) + " " + this.LoginUser.FirstName + " (" + UserName + ")");
                        isValid = false;
                        this.Close();
                    }
                }
            }
            return isValid;
        }

        private bool IsValidEntry()
        {
            bool isValid = true;

            DataTable transSource = (DataTable)gcTransaction.DataSource;

            if (string.IsNullOrEmpty(dtTransactionDate.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_TRANSACTION_DATE));
                isValid = false;
                dtTransactionDate.Focus();
            }
            else if (!IsValidTransactionDate())
            {
                dtTransactionDate.DateTime = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom.ToString(), false);
                isValid = false;
            }
            else if (TransVoucherMethod == (int)TransactionVoucherMethod.Manual && string.IsNullOrEmpty(txtVoucher.Text))
            {
                //On 10/05/2022, for sdbinm, no manual receipt number
                if (this.AppSetting.IS_SDB_INM && VoucherType == DefaultVoucherTypes.Receipt &&
                               UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false) >= UtilityMember.DateSet.ToDate(SettingProperty.Enforce_Receipt_Module_FY, false))
                {
                    this.ShowMessageBox(MessageCatalog.Common.COMMON_RECEIPT_DISABLED_CHANGE_RECEIPT_METHOD);
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_NUMBER_EMPTY));
                }
                isValid = false;
                txtVoucher.Focus();
            }
            else if (!IsRecordDeleted())
            {
                isValid = false;
            }
            else if (!IsValidSource())
            {
                isValid = false;
            }
            else if (!ValidateReceiptModuleRights())
            { //11/04/2022 to validate Receipt Module rights
                isValid = false;
            }
            else if (this.AppSetting.AllowMultiCurrency == 1 && !IsCurrencyEnabledVoucher)
            { //On 26/08/2024, If multi currency enabled, all the currency details must be filled
                MessageRender.ShowMessage("As Multi Currency option is enabled, All the Currecny details should be filled.");
                glkpCurrencyCountry.Select();
                glkpCurrencyCountry.Focus();
                isValid = false;
            }
            else if (!ValidateDonorActualAmount())
            {
                isValid = false;
            }
            else if (!ValidateChequeNumberBasedOnSetting()) //08/06/2020, Validate cheque number
            {
                isValid = false;
            }
            else if (!IsValidTransGrid())
            {
                isValid = false;
            }
            else if (!IsValidCashTransGrid())
            {
                isValid = false;
            }
            else if (!IsValidCashTransGridNegative())
            {
                isValid = false;
            }
            else if (!ValidateReferenceDetails())
            {
                isValid = false;
            }
            else if (!IsValidateTransCashEqualAmount())  // This is Check the GST Values with Trans Amount (09.05.2019)
            {
                isValid = false;
            }
            else if (!IsValidateTransCashEqualAmountGST()) // This is to check the Trans Amount with Cash\Bank Amount (09.05.2019)
            {
                isValid = false;
            }
            //else if (TransSummaryVal != CashTransSummaryVal)
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_AMOUNT_MISMATCH));
            //    isValid = false;
            //    gcBank.Select();
            //    gcBank.Focus();
            //}
            else if (!IsDonorEntryRequired())
            {
                isValid = false;
            }
            else if (!ValidateCCAmoutWithLedgerAmount())
            {
                isValid = false;
                gcTransaction.Select();
                gcTransaction.Focus();
            }
            else if (!ValidateGSTInvoiceDetails())
            {
                isValid = false;
                gcTransaction.Select();
                gcTransaction.Focus();
            }
            else if (!isExistInvoiceNo())
            {
                isValid = false;
                MessageRender.ShowMessage("'" + GSTVendorInvoiceNo + "'" + this.GetMessage(MessageCatalog.Transaction.VocherTransaction.IS_GST_PERCENTAGE));    // GST Invoice No is already exists");
                gcTransaction.Select();
                gcTransaction.Focus();
            }
            else if (lgGSTInvoce.Visibility == LayoutVisibility.Always && BookingGSTInvoiceId > 0 &&
                   UtilityMember.NumberSet.ToDouble(colGStAmt.SummaryItem.SummaryValue.ToString()) > 0)
            {   //On 04/09/2023, To validate if Journal GST Invocies selected and GST Selected, altert message
                isValid = false;
                //   MessageRender.ShowMessage("Since GST Invoice '" + lkpGSTInvoices.Text + "' is selected you can't pass GST percentage for this Voucher");
                MessageRender.ShowMessage(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.GST_INVOICE) + "'" + lkpGSTInvoices.Text + "'" + this.GetMessage(MessageCatalog.Transaction.VocherTransaction.IS_GST_PERCENTAGE));
                gcTransaction.Select();
                gcTransaction.Focus();
            }
            else if (!ValidateRPLedgerAmountWithGSTInvoice())
            {
                isValid = false;
            }

            if (isValid && DuplicateVoucher)
            {
                //   if (this.ShowConfirmationMessage("Are you sure to Replicate Voucher Entry ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.ARE_REP_ENTRY), MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                == System.Windows.Forms.DialogResult.No)
                {
                    isValid = false;
                }
            }

            //On 20/10/2023, Alert message if no GST with GST Invoice
            if (isValid && UpdateGST == 0 && DtGSTInvoiceMasterDetails != null &&
                    DtGSTInvoiceMasterLedgerDetails.Rows.Count > 0 && !string.IsNullOrEmpty(GSTVendorInvoiceNo))
            {
                if (this.ShowConfirmationMessage("Are you sure to update Voucher GST Invoice without GST Amount ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                == System.Windows.Forms.DialogResult.No)
                {
                    isValid = false;
                }
            }

            //On 10/04/2023, Alert to save gst invoice details
            /*if (isValid)
            {
                if (colGStAmt.Visible && dtTransactionDate.DateTime >= this.AppSetting.GSTStartDate && this.AppSetting.CGSTLedgerId > 0 &&
                this.AppSetting.SGSTLedgerId > 0 && AppSetting.EnableGST == "1" && rgTransactionType.SelectedIndex == 0 || rgTransactionType.SelectedIndex == 1 && gstCalcAmount > 0)
                {
                    if (CanShowVendorGST)
                    {
                        if (string.IsNullOrEmpty(GSTVendorInvoiceNo))
                        {
                            if (this.ShowConfirmationMessage("Do you want to update Vendor GST Invoice Details ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                            {
                                ShowVendorGSTInvoiceDetails();
                                isValid = true;
                            }
                            else
                            {
                                //26/04/2019, clear vendor gst invoice
                                GSTVendorInvoiceNo = string.Empty;
                                GSTVendorInvoiceDate = string.Empty;
                                GSTVendorInvoiceType = 0;
                                GSTVendorId = 0;
                                DtGSTInvoiceMasterDetails = null;
                                DtGSTInvoiceMasterLedgerDetails = null;
                                isValid = true;
                            }
                        }
                    }
                }
            }*/

            if (IsLockedTransaction(dtTransactionDate.DateTime))
            {
                /*if (VoucherId == 0)
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED) + ProjectName + "'");
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED_CANNOT_EDIT) + ProjectName + "'");
                }*/
                if (VoucherId == 0)
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED) + "'" + ProjectName + "'," +
                            " during the period " + this.UtilityMember.DateSet.ToDate(dtLockDateFrom.ToShortDateString()) +
                            " - " + this.UtilityMember.DateSet.ToDate(dtLockDateTo.ToShortDateString()));
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED_CANNOT_EDIT) + "'" + ProjectName + "'," +
                            " during the period " + this.UtilityMember.DateSet.ToDate(dtLockDateFrom.ToShortDateString()) +
                            " - " + this.UtilityMember.DateSet.ToDate(dtLockDateTo.ToShortDateString()));
                }
                isValid = false;
                dtTransactionDate.Focus();
            }
            return isValid;
        }

        private bool IsDonorEntryRequired()
        {
            int DivisionId = 0;
            bool isValid = true;
            using (ProjectSystem projectSystem = new ProjectSystem(ProjectId))
            {
                DivisionId = projectSystem.DivisionId;
                if (DivisionId == (int)Division.Foreign)
                {
                    if (glkpDonor.Text == string.Empty && rgTransactionType.SelectedIndex == 0)
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.SAVE_WITHOUT_DONOR), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            isValid = false;
                        }
                    }
                }
            }
            return isValid;
        }

        /// <summary>
        /// On 08/06/2020, Validate cheque number based on setting
        /// 
        /// Receipts & Payment: As usual 
        /// Contra Cash Deposit : Don't check mandatory condition (not needed cheque number) even though if they give cheque number, it should be validated duplicate
        /// Contra Withdrawal : As usual 
        /// Contra Transfer      : It will validate both conditions for Cr-Bank. For Dr Bank (Don't check mandatory condition (not needed cheque number) 
        /// even though if they give cheque number, it should be validated duplicate)
        /// </summary>
        /// <returns></returns>
        private bool ValidateChequeNumberBasedOnSetting()
        {
            bool isValid = true;
            DefaulContraVoucher contravoucher = GetContaVoucherType();
            DataTable dtLedgerVouchers = gcTransaction.DataSource as DataTable;
            DataTable dtCashBankTrans = gcBank.DataSource as DataTable;
            if (dtCashBankTrans != null && dtCashBankTrans.Rows.Count > 0)
            {
                DataTable dtCashBank = dtCashBankTrans.DefaultView.ToTable();
                if (rgTransactionType.SelectedIndex != 2)
                {
                    isValid = IsValidChequeNumber(dtCashBank, DefaulContraVoucher.None);
                    if (!isValid)
                    {
                        FocusCashTransactionGrid();
                    }
                }
                else if (rgTransactionType.SelectedIndex == 2)
                {
                    if (contravoucher == DefaulContraVoucher.CashDeposit)
                    {
                        isValid = IsValidChequeNumber(dtCashBank, contravoucher);
                        if (!isValid)
                        {
                            FocusCashTransactionGrid();
                        }
                    }
                    else if (contravoucher == DefaulContraVoucher.CashWithdraw)
                    {
                        isValid = IsValidChequeNumber(dtLedgerVouchers, contravoucher);

                        if (!isValid)
                        {
                            FocusTransactionGrid();
                        }
                    }
                    else if (contravoucher == DefaulContraVoucher.Transfer)
                    {
                        isValid = IsValidChequeNumber(dtLedgerVouchers, contravoucher);
                        if (isValid)
                        {
                            isValid = IsValidChequeNumber(dtCashBank, DefaulContraVoucher.CashDeposit);
                            if (!isValid)
                            {
                                FocusCashTransactionGrid();
                            }
                        }
                        else
                        {
                            FocusTransactionGrid();
                        }

                    }
                }
            }

            return isValid;
        }

        private bool IsValidChequeNumber(DataTable dtLedgers, DefaulContraVoucher contravoucher)
        {
            bool isValid = true;
            if ((AppSetting.MandatoryChequeNumberInVoucherEntry == 1 || AppSetting.DonotAllowDuplicateChequeNumberInVoucherEntry == 1))
            {
                using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem(VoucherId))
                {
                    foreach (DataRowView drv in dtLedgers.DefaultView)
                    {
                        Int32 ledgerid = UtilityMember.NumberSet.ToInteger(drv[voucherSystem.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
                        Int32 ledgergroupid = GetLedgerGroupId(ledgerid);
                        string chequenumber = drv[voucherSystem.AppSchema.VoucherTransaction.CHEQUE_NOColumn.ColumnName].ToString().Trim();

                        //#. Check Mandatory for Cheque Number
                        if (ledgergroupid == (Int32)FixedLedgerGroup.BankAccounts && AppSetting.MandatoryChequeNumberInVoucherEntry == 1 && String.IsNullOrEmpty(chequenumber)
                            && contravoucher != DefaulContraVoucher.CashDeposit)
                        {
                            //this.ShowMessageBox("Cheque Number is missing, Fill Cheque Number for Bank Account.");
                            //if (this.ShowConfirmationMessage("Cheque Number is missing for Bank Account, Do you want to proceed without Cheque Number?",
                            string msg = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.BANK_CHEQUE_NUMBER);
                            MessageBoxButtons msgbtn = MessageBoxButtons.YesNo;
                            if (this.AppSetting.AllowMultiCurrency == 1)
                            {
                                msg = "Cheque/DD/Reference Number is missing for Bank Account.";
                                msgbtn = MessageBoxButtons.OK;
                            }
                            if (this.ShowConfirmationMessage(msg, msgbtn, MessageBoxIcon.Question) == DialogResult.No ||
                                this.AppSetting.AllowMultiCurrency == 1)
                            {
                                isValid = false;
                            }
                            break;
                        }

                        //#. Allow Duplicate Cheque Number
                        if (ledgergroupid == (Int32)FixedLedgerGroup.BankAccounts && AppSetting.DonotAllowDuplicateChequeNumberInVoucherEntry == 1 && !String.IsNullOrEmpty(chequenumber))
                        {
                            ResultArgs result = voucherSystem.CheckChequeNumberExists(chequenumber, VoucherId);
                            if (result.Success && result.ReturnValue != null && AppSetting.NumberSet.ToInteger(result.ReturnValue.ToString()) > 0)
                            {
                                //this.ShowMessageBox("Cheque Number '" + chequenumber + "' should not be duplicated in Voucher Entry, Cross check or change Cheque Number for Bank Account.");
                                //if (this.ShowConfirmationMessage("Cheque Number '" + chequenumber + "' should not be duplicated for Bank Account. " +
                                //        "It has been used for someother Voucher. Do you want to proceed with duplicate Cheque Number?",
                                //        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.CHEQUE_NUMBER) + "'" + chequenumber + "'" + this.GetMessage(MessageCatalog.Transaction.VocherTransaction.SHOULD_DUPLICATED) +
                                    this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_DUPLICATE_CHEQUE),
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                {
                                    isValid = false;
                                }
                                break;
                            }
                        }
                    }
                }
            }

            return isValid;
        }

        /// <summary>
        /// On 10/06/2020, Check Contra-Cash Deposite Voucher
        /// </summary>  
        /// <returns></returns>
        private DefaulContraVoucher GetContaVoucherType()
        {
            DefaulContraVoucher contravoucher = DefaulContraVoucher.CashDeposit;
            Int32 ledgerid = (Int32)DefaultLedgers.Cash;
            Int32 ledgergroupid = (Int32)FixedLedgerGroup.Cash;

            Int32 CBledgerid = (Int32)DefaultLedgers.Cash;
            Int32 CBledgergroupid = (Int32)FixedLedgerGroup.BankAccounts;

            if (rgTransactionType.SelectedIndex == 2)
            {
                using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem(VoucherId))
                {
                    if (AppSetting.MandatoryChequeNumberInVoucherEntry == 1 || AppSetting.DonotAllowDuplicateChequeNumberInVoucherEntry == 1)
                    {
                        DataTable dtLedgerVouchers = gcTransaction.DataSource as DataTable;
                        DataTable dtCashBankTrans = gcBank.DataSource as DataTable;

                        //General Ledger
                        if (dtLedgerVouchers != null && dtLedgerVouchers.Rows.Count > 0)
                        {
                            ledgerid = UtilityMember.NumberSet.ToInteger(dtLedgerVouchers.Rows[0][voucherSystem.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
                            ledgergroupid = GetLedgerGroupId(ledgerid);
                        }
                        //Cash/Bank Ledger
                        if (dtCashBankTrans != null && dtCashBankTrans.Rows.Count > 0)
                        {
                            CBledgerid = UtilityMember.NumberSet.ToInteger(dtCashBankTrans.Rows[0][voucherSystem.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
                            CBledgergroupid = GetLedgerGroupId(CBledgerid);
                        }

                        if (ledgergroupid == (Int32)FixedLedgerGroup.Cash && CBledgergroupid == (Int32)FixedLedgerGroup.BankAccounts)
                        {
                            contravoucher = DefaulContraVoucher.CashDeposit;
                        }
                        else if (ledgergroupid == (Int32)FixedLedgerGroup.BankAccounts && CBledgergroupid == (Int32)FixedLedgerGroup.Cash)
                        {
                            contravoucher = DefaulContraVoucher.CashWithdraw;
                        }
                        else if (ledgergroupid == (Int32)FixedLedgerGroup.BankAccounts && CBledgergroupid == (Int32)FixedLedgerGroup.BankAccounts)
                        {
                            contravoucher = DefaulContraVoucher.Transfer;
                        }

                    }
                }
            }

            return contravoucher;
        }

        private bool ValidateDonorActualAmount()
        {
            bool isValid = true;
            if (glkpDonor.Text != string.Empty && (rgTransactionType.SelectedIndex == 0)
                && (glkpPurpose.Text == string.Empty || txtCurrencyAmount.Text == string.Empty || txtActualAmount.Text == string.Empty || txtActualAmount.Text != string.Empty))
            {
                if (string.IsNullOrEmpty(glkpPurpose.Text))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.DONOR_PUPOSE_EMPTY));
                    isValid = false;
                    glkpPurpose.Select();
                    glkpPurpose.Focus();
                }
                else if (this.UtilityMember.NumberSet.ToDouble(txtCurrencyAmount.Text) == 0)
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.DONOR_CONTRIBUTION_AMOUNT_EMPTY));
                    isValid = false;
                    txtCurrencyAmount.Focus();
                }
                else if (this.UtilityMember.NumberSet.ToDouble(txtActualAmount.Text) == 0)
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.DONOR_CONTRIBUTION_ACTUAL_AMOUNT_EMPTY));
                    isValid = false;
                    txtActualAmount.Focus();
                }
            }

            if (isValid)
            {
                //On 22/08/2024, To check Currenty actual amount with transaction amount
                //On 26/08/2024, If multi currency enabled, let us validate ledger amount with currency amount other wise  converted acutal amount
                if ((this.AppSetting.AllowMultiCurrency == 1 && IsCurrencyEnabledVoucher) || this.AppSetting.AllowMultiCurrency == 0)
                {
                    if ((this.UtilityMember.NumberSet.ToDouble(txtCurrencyAmount.Text) > 0) || (this.UtilityMember.NumberSet.ToDouble(txtActualAmount.Text) > 0))
                    {
                        double TransAmount = 0;
                        double CurrencyAmount = this.UtilityMember.NumberSet.ToDouble(txtActualAmount.Text);
                        if (this.AppSetting.AllowMultiCurrency == 1)
                        {
                            CurrencyAmount = this.UtilityMember.NumberSet.ToDouble(txtCurrencyAmount.Text);
                        }

                        //16/08/2024, For all type of (Receipt, Payment, Contra) ---------------------------------------------------------------------------------------
                        int EntryTransSrc = (int)Source.To;
                        if (rgTransactionType.SelectedIndex == 0) EntryTransSrc = (int)Source.To;
                        else if (rgTransactionType.SelectedIndex == 1) EntryTransSrc = (int)Source.By;
                        //----------------------------------------------------------------------------------------------------------------------------------------------

                        DataTable dtTrans = gcTransaction.DataSource as DataTable;
                        if (dtTrans != null && dtTrans.Rows.Count > 0)
                        {
                            TransAmount = this.UtilityMember.NumberSet.ToDouble(dtTrans.Compute("SUM(AMOUNT)", "SOURCE=" + EntryTransSrc).ToString()); //(int)Source.To
                        }

                        // if (TransSummaryVal != this.UtilityMember.NumberSet.ToDouble(txtActualAmount.Text))
                        if (TransAmount != CurrencyAmount) //this.UtilityMember.NumberSet.ToDouble(txtActualAmount.Text)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.TRANSACTION_AMOUNT_NOT_EQUAL_ACTUAL_AMOUNT));
                            FocusTransactionGrid();
                            isValid = false;
                        }
                    }
                }
            }
            return isValid;
        }

        private void AssignValues()
        {
            try
            {
                if (VoucherId > 0)
                {
                    using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem(VoucherId))
                    {
                        dtTransactionDate.DateTime = EditVoucherDate = this.UtilityMember.DateSet.ToDate(voucherSystem.VoucherDate.ToShortDateString(), false);
                        ucCaptionPanel1.Caption = ProjectName;
                        txtVoucher.Text = voucherSystem.VoucherNo;
                        VoucherDefinitionId = voucherSystem.VoucherDefinitionId;
                        EditVoucherDefinitionid = VoucherDefinitionId;

                        AuthorizedStatus = voucherSystem.AuthorizedStatus;
                        if (TransVoucherMethod == 1)
                        {
                            txtVoucher.Enabled = false;
                        }
                        else
                        {
                            //On 10/05/2022, for sdbinm, no manual receipt number
                            //txtVoucher.Enabled = true;
                            if (this.AppSetting.IS_SDB_INM && VoucherType == DefaultVoucherTypes.Receipt &&
                               UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false) >= UtilityMember.DateSet.ToDate(SettingProperty.Enforce_Receipt_Module_FY, false))
                            {
                                txtVoucher.Enabled = false;
                            }
                            else
                            {
                                txtVoucher.Enabled = true;
                            }
                        }
                        rgTransactionType.SelectedIndex = EditVoucherIndex = VoucherTypeId = (voucherSystem.VoucherType == "RC") ? 0 : (voucherSystem.VoucherType == "PY") ? 1 : 2;
                        if (voucherSystem.DonorId > 0)
                        {
                            if ((int)Division.Local == DivisionId) { ShowDonorAdditionalInfo(); }
                        }

                        glkpDonor.EditValue = voucherSystem.DonorId;
                        glkpPurpose.EditValue = voucherSystem.PurposeId;
                        glkpReceiptType.EditValue = voucherSystem.ContributionType == "N" ? 0 : voucherSystem.ContributionType == "F" ? 0 : 1;

                        //glkpCurrencyCountry.Properties.ForceInitialize();
                        glkpCurrencyCountry.EditValue = voucherSystem.CurrencyCountryId;
                        object findcountry = glkpCurrencyCountry.Properties.GetDisplayValueByKeyValue(voucherSystem.CurrencyCountryId);
                        if (findcountry == null) glkpCurrencyCountry.EditValue = null;
                        ShowCurrencyDetails();

                        txtExchangeRate.Text = voucherSystem.ExchangeRate.ToString();
                        txtNarration.Text = voucherSystem.Narration;
                        voucherSystem.VoucherId = VoucherId;
                        txtCurrencyAmount.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(voucherSystem.ContributionAmount.ToString())).ToString();
                        lblCalculatedAmt.Text = voucherSystem.CalculatedAmount.ToString();
                        txtActualAmount.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(voucherSystem.ActualAmount.ToString())).ToString();
                        txtNameAddress.Text = voucherSystem.NameAddress;

                        txtPanNo.Text = voucherSystem.PanNumber;
                        txtGSTNo.Text = voucherSystem.GSTNumber;

                        //26/04/2019, for Vendor GST Invoice details
                        GSTVendorInvoiceNo = (string.IsNullOrEmpty(voucherSystem.GST_VENDOR_INVOICE_NO) ? string.Empty : voucherSystem.GST_VENDOR_INVOICE_NO.Trim());
                        GSTVendorInvoiceDate = voucherSystem.GST_VENDOR_INVOICE_DATE;
                        GSTVendorInvoiceType = voucherSystem.GST_VENDOR_INVOICE_TYPE;
                        GSTVendorId = voucherSystem.GST_VENDOR_ID;
                        BookingGSTInvoiceId = voucherSystem.BookingGSTInvoiceId;

                        resultArgs = voucherSystem.FetchTransDetails();
                        if (resultArgs.Success)
                        {
                            DataTable dtTrans = BindCurrentBalance(resultArgs.DataSource.Table); // GetCurrentBalance(resultArgs.DataSource.Table, true);

                            //On 09/02/2018, To make duplication voucher (Ref_01), remove voucher id and make it add mode after getting selected voucher details
                            if (DuplicateVoucher)
                            {
                                dtTrans.Columns.Remove(voucherSystem.AppSchema.VoucherTransaction.VOUCHER_IDColumn.ColumnName);
                            }

                            gstCalcAmount = this.UtilityMember.NumberSet.ToDouble(dtTrans.Compute("SUM(GST)", "").ToString());
                            cgstCalcAmount = this.UtilityMember.NumberSet.ToDouble(dtTrans.Compute("SUM(CGST)", "").ToString());
                            sgstCalcAmount = this.UtilityMember.NumberSet.ToDouble(dtTrans.Compute("SUM(SGST)", "").ToString());
                            igstCalcAmount = this.UtilityMember.NumberSet.ToDouble(dtTrans.Compute("SUM(IGST)", "").ToString());

                            ApplyGST(dtTrans, null, false);

                            gcTransaction.DataSource = dtTrans.DefaultView.ToTable(); // resultArgs.DataSource.Table;
                            EnableBankFields();

                            DataTable dt = dtTrans;  //resultArgs.DataSource.Table;
                            dsCostCentre.Clear();

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                int LedId = this.UtilityMember.NumberSet.ToInteger(dt.Rows[i][voucherSystem.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
                                voucherSystem.LedgerId = LedId;
                                voucherSystem.CostCenterTable = i + "LDR" + LedId;
                                resultArgs = voucherSystem.GetCostCentreDetails();
                                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                                {
                                    DataTable CostCentreInfo = resultArgs.DataSource.Table;

                                    //On 09/02/2018, To make duplication voucher (Ref_01), remove voucher id and make it add mode after getting selected voucher details
                                    if (DuplicateVoucher)
                                    {
                                        CostCentreInfo.Columns.Remove(voucherSystem.AppSchema.VoucherTransaction.VOUCHER_IDColumn.ColumnName);
                                    }
                                    CostCentreInfo.TableName = i + "LDR" + LedId;
                                    if (CostCentreInfo != null)
                                    {
                                        if (!dsCostCentre.Tables.Contains(CostCentreInfo.TableName))
                                        {
                                            dsCostCentre.Tables.Add(CostCentreInfo);
                                        }
                                    }
                                }
                            }

                            ///
                            /// this is to fetch the Voucher Ledger Reference Details to assign to Controls
                            /// 
                            //resultArgs = BindVoucherLedgerReferenceDetails();
                            //if (resultArgs.Success)
                            //{
                            //    dtReferenceVoucherNumber = resultArgs.DataSource.Table;
                            //    dtReferenceVoucherNumber.AcceptChanges();
                            //}

                            resultArgs = voucherSystem.FetchCashBankDetails();
                            if (resultArgs.Success)
                            {
                                DataTable dtBank = BindCurrentBalance(resultArgs.DataSource.Table);

                                //On 09/02/2018, To make duplication voucher (Ref_01), remove voucher id and make it add mode after getting selected voucher details
                                if (DuplicateVoucher)
                                {
                                    dtBank.Columns.Remove(voucherSystem.AppSchema.VoucherTransaction.VOUCHER_IDColumn.ColumnName);
                                }
                                ApplyGST(dtTrans, dtBank, false);

                                gcBank.DataSource = dtBank.DefaultView.ToTable();
                                EnableCashBankFields();
                            }
                        }

                        //08/04/2022-------------------------------------------------------------------------------------------
                        TrackVoucherType = rgTransactionType.SelectedIndex == 0 ? DefaultVoucherTypes.Receipt : rgTransactionType.SelectedIndex == 1 ? DefaultVoucherTypes.Payment : DefaultVoucherTypes.Contra;
                        TrackVoucherId = VoucherId;
                        TrackVoucherDate = dtTransactionDate.DateTime;
                        TrackVoucherNo = voucherSystem.VoucherNo;
                        TrackVoucherProjectId = voucherSystem.ProjectId;
                        TrackVoucherSubType = voucherSystem.VoucherSubType;
                        TrackVoucherAmount = CashTransSummaryVal;
                        //-----------------------------------------------------------------------------------------------------

                        //04/11/2022, to get GST vouchers
                        DtGSTInvoiceMasterDetails = voucherSystem.dtGSTInvoiceMasterDetails;
                        DtGSTInvoiceMasterLedgerDetails = voucherSystem.dtGSTInvoiceMasterLedgerDetails;
                        GSTInvoiceId = voucherSystem.GST_INVOICE_ID;

                        dtVoucherImages = voucherSystem.dtVoucherFiles;
                    }
                }

                FetchUpdateReferenceNo();

                FetchSubLedgerVouchers();


                //On 09/02/2018, If duplication entry, clear voucher Id and make it add mode (Ref_01)
                //On 07/02/2020, If duplication entry, clear voucher Id and make it add mode
                if (DuplicateVoucher)
                {
                    //Reset Voucher Id for duplciated voucher in Referenfce Number
                    if (dtReferenceVoucherNumber.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtReferenceVoucherNumber.Rows)
                        {
                            dr["REC_PAY_VOUCHER_ID"] = 0;
                        }
                        dtReferenceVoucherNumber.AcceptChanges();
                    }

                    //Reset Voucher Id for duplciated voucher in Sub Ledgers Vouchers
                    //On 07/02/2020, If duplication entry, clear voucher Id and make it add mode
                    if (dtSubLedgerVouchers.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtSubLedgerVouchers.Rows)
                        {
                            dr["VOUCHER_ID"] = 0;
                        }
                        dtReferenceVoucherNumber.AcceptChanges();
                    }

                    VoucherId = 0;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void GetUserControlInput()
        {
            ucTransViewOpeningBalDetails.ProjectId = ProjectId;
            ucTransViewOpeningBalDetails.OpeningDateFrom = dtTransactionDate.Text;
            ucTransViewOpeningBalDetails.OpeningDateTo = dtTransactionDate.Text;
            ucTransviewClosingBalance.ProjectId = ProjectId;
            ucTransviewClosingBalance.ClosingDateFrom = dtTransactionDate.Text;
            ucTransviewClosingBalance.ClosingDateTo = dtTransactionDate.Text;
            ucTransViewOpeningBalDetails.BankClosedDate = dtTransactionDate.Text; //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
            ucTransViewOpeningBalDetails.GetOpBalance();
            ucTransViewOpeningBalDetails.ShowOpNegative();
            ucTransviewClosingBalance.BankClosedDate = dtTransactionDate.Text; //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
            ucTransviewClosingBalance.GetClosingBalance();
            ucTransviewClosingBalance.ShowClosingBalNegative();
        }

        private DataTable BindCurrentBalance(DataTable dtTrans)
        {
            int ledgerId = 0;
            int NatureId = 0;
            foreach (DataRow dr in dtTrans.Rows)
            {
                ledgerId = this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                NatureId = dr["NATURE_ID"] != null ? this.UtilityMember.NumberSet.ToInteger(dr["NATURE_ID"].ToString()) : 0;

                // chinna 04.06.2021, 07.06.2021
                if (!this.AppSetting.IS_CMF_SLA || (NatureId == (int)Natures.Assert || NatureId == (int)Natures.Libilities))
                {
                    BalanceProperty balance = FetchCurrentBalance(ledgerId);
                    string rtn = this.UtilityMember.NumberSet.ToCurrency(balance.Amount) + " " + balance.TransMode;

                    //On 26/08/2024, If multi currency enabled, let show both currency balance 
                    if (this.AppSetting.AllowMultiCurrency == 1)
                    {
                        if (balance.GroupId == (Int32)FixedLedgerGroup.Cash || balance.GroupId == (Int32)FixedLedgerGroup.BankAccounts)
                        {
                            rtn = balance.CurrencySymbol + " " + this.UtilityMember.NumberSet.ToNumber(Math.Abs(balance.AmountFC)) + " " + balance.TransMode + "  "
                                        + this.UtilityMember.NumberSet.ToCurrency(Math.Abs(balance.Amount)) + " " + balance.TransMode;
                        }
                    }
                    //--------------------------------------------------------------------------------------------------------------------------------------

                    dr["LEDGER_BALANCE"] = rtn;
                }
                else
                {
                    BalanceProperty balance = FetchBudgetLedgerBalance(ledgerId);
                    string rtn = this.UtilityMember.NumberSet.ToCurrency(balance.Amount) + " " + balance.TransMode;
                    dr["LEDGER_BALANCE"] = rtn;
                }

                // By Aldrin
                BalanceProperty BudgetBal = FetchBudgetAmount(ledgerId);
                if (BudgetId > 0)// && BudgetBal.Amount > 0)
                {
                    string BudgetAmount = CalculateBudget(ledgerId); //FetchBudgetAmount(LedgerID);
                    if (BudgetAmount != string.Empty)
                    {
                        dr["BUDGET_AMOUNT"] = BudgetAmount;
                    }
                }
            }

            return dtTrans;
        }

        // This is hide for showing unwanted ( 17.01.2022)
        //private void ShowCostCentrePopUp()
        //{
        //    if (LedgerId > 0 && dsCostCentre.Tables.Count > 0)
        //    {
        //        if (dsCostCentre.Tables.Contains(LedgerId.ToString()))
        //        {
        //            frmTransactionCostCenter frmCostCentre = new frmTransactionCostCenter(ProjectId, dsCostCentre.Tables[dsCostCentre.Tables.IndexOf(LedgerId.ToString())].DefaultView, LedgerId, LedgerAmount, "Check");
        //            frmCostCentre.ShowDialog();
        //        }
        //    }
        //}

        private void GetSelectedRow()
        {
            int TransHandle = gvTransaction.FocusedRowHandle;
            int CashBankHandle = gvBank.FocusedRowHandle;
        }

        private bool IsValidaTransactionRow()
        {
            bool IsLedgerValid = false;
            try
            {
                IsLedgerValid = (LedgerId > 0 && LedgerAmount > 0);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            return IsLedgerValid;
        }

        private bool NotifyTransactionLedger(int NatureId, int TransMode)
        {
            bool IsValid = true;
            try
            {
                if (rgTransactionType.SelectedIndex == 0 && NatureId == (int)DefaultVoucherTypes.Payment && TransMode == (int)Source.To)
                {
                    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_MULTY_RECEIPT_CONFIRM), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        IsValid = false;
                    }
                }
                else if (rgTransactionType.SelectedIndex == 1 && NatureId == (int)DefaultVoucherTypes.Receipt && TransMode == (int)Source.By)
                {
                    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_MULTY_PAYMENT_CONFIRM), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        IsValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
            return IsValid;
        }

        private void LoadLedger(RepositoryItemGridLookUpEdit glkpLedger)
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    ledgerSystem.ProjectId = ProjectId;
                    resultArgs = ledgerSystem.FetchLedgerByGroup();
                    //glkpLedger.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        glkpLedger.DisplayMember = "LEDGER_NAME";
                        glkpLedger.ValueMember = "LEDGER_ID";

                        //On 25/03/2021, To skip SDBINM Auditors mentioend Ledges
                        DataTable dtLedgers = resultArgs.DataSource.Table;
                        if (AppSetting.IS_SDB_INM)
                        {
                            dtLedgers = this.ForSDBINMSkipLedgers(dtTransactionDate.Text, dtLedgers);
                        }

                        //On 20/10/2021, to skip Closed Ledgers
                        DataTable dtBaseLedger = FilterLedgerByDateClosed(dtLedgers, false, dtTransactionDate.DateTime);

                        /*//28/03/2023, Enforce lodger locking in Voucher entry screen --------------------------------------------------------------------
                        ResultArgs result = EnforceLockMastersInVoucherEntry(Id.Ledger, dtBaseLedger.DefaultView.ToTable(), dtTransactionDate.DateTime);
                        if (result.Success)
                        {
                            dtBaseLedger = result.DataSource.Table;
                        }
                        //-------------------------------------------------------------------------------------------------------------------------------
                         */

                        //On 21/11/2024 - Enforce Nature based Ledgers
                        if (rgTransactionType.SelectedIndex != 2 &&
                             (!string.IsNullOrEmpty(this.AppSetting.NatuersInReceiptVoucherEntry) || !string.IsNullOrEmpty(this.AppSetting.NatuersInPaymentVoucherEntry)) &&
                             (this.AppSetting.NatuersInReceiptVoucherEntry != "0" || this.AppSetting.NatuersInPaymentVoucherEntry != "0"))
                        {
                            string natureid = rgTransactionType.SelectedIndex == 0 ? this.AppSetting.NatuersInReceiptVoucherEntry : this.AppSetting.NatuersInPaymentVoucherEntry;

                            if (natureid != "0")
                            {
                                dtBaseLedger.DefaultView.RowFilter = (string.IsNullOrEmpty(dtBaseLedger.DefaultView.RowFilter) ? "" : " AND ") +
                                                                        ledgerSystem.AppSchema.LedgerGroup.NATURE_IDColumn.ColumnName + " IN (" + natureid + ")";
                            }
                            dtBaseLedger = dtBaseLedger.DefaultView.ToTable();
                        }

                        glkpLedger.DataSource = dtBaseLedger;
                    }
                    //else
                    //{
                    //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_LEDGER_MAPPING_TO_PROJECT) + " ' " + ProjectName + " '");
                    //}
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private DataTable IncludeTempAmount(DataTable dtTrans)
        {
            //dtTrans.Columns["Temp_Amount"].Expression = "0";
            dtTrans.Columns["Temp_Amount"].Expression = "IIF(LEDGER_ID=TEMP_LEDGER_ID,BASE_AMOUNT,0)";
            return dtTrans;
        }

        private void ChangeTransType()
        {
            DataView dvCashBankTrans = gvBank.DataSource as DataView;
            DataView dvTrans = gvTransaction.DataSource as DataView;

            if (dvTrans != null && dvCashBankTrans != null)
            {
                // dvTrans.RowFilter = "(LEDGER_ID>0 AND AMOUNT>0)";
                // dvCashBankTrans.RowFilter = "(LEDGER_ID>0 AND AMOUNT>0)";

                if (dvTrans.Count > 0 || dvCashBankTrans.Count > 0)
                {
                    DataTable dt = dvTrans.ToTable();
                    //dt.Columns["SOURCE"].Expression = (rgTransactionType.SelectedIndex == 1) ? "2" : "1";
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["SOURCE"] = (rgTransactionType.SelectedIndex == 1) ? 2 : 1;
                    }
                    dvTrans = dt.DefaultView;

                    DataTable dtBank = dvCashBankTrans.ToTable();
                    foreach (DataRow drBank in dtBank.Rows)
                    {
                        drBank["SOURCE"] = (rgTransactionType.SelectedIndex == 1) ? 1 : 2;
                    }
                    dvCashBankTrans = dtBank.DefaultView;

                    //if (dvTrans.Count == 0) { ConstructTransEmptySource(); }
                    //if (dvCashBankTrans.Count == 0) { ConstructCashTransEmptySournce(); }

                    if (VoucherTypeId != rgTransactionType.SelectedIndex)
                    {
                        //if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_TRANSACTION_CHANGE_TYPE.ToString()),
                        //    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        //{
                        if (rgTransactionType.SelectedIndex == 0 || rgTransactionType.SelectedIndex == 1)
                        {
                            if (VoucherTypeId != 2)
                            {
                                BindDataSource(dvTrans, dvCashBankTrans);
                            }
                            else
                            {
                                ConstructEmptyDataSource();
                            }
                        }
                        else if (rgTransactionType.SelectedIndex == 2)
                        {
                            ConstructEmptyDataSource();
                        }

                        //}
                        //else
                        //{
                        //    rgTransactionType.SelectedIndex = VoucherTypeId;
                        //}
                    }
                    VoucherTypeId = rgTransactionType.SelectedIndex;
                    CalculateFirstRowValue();
                }
            }

            //07/02/2020, Visible Show SubLedger Based on Voucher Type and Settings
            ShowAdditionButtons(AdditionButttons.SubLedgerDetails, true);

            LoadPendingGSTInvoices();
        }

        private void BindDataSource(DataView dvTrans, DataView dvCashTrans)
        {
            DataTable dtTransTemp = new DataTable();
            DataTable dtCashBankTemp = new DataTable();

            if (VoucherId > 0)
            {
                if (dvTrans.Count > 0)
                {
                    dtTransTemp = GetCurrentBalance(dvTrans.ToTable());
                }
                if (dvCashTrans.Count > 0)
                {
                    dtCashBankTemp = GetCurrentBalance(dvCashTrans.ToTable());
                }
            }
            else
            {
                if (dvTrans.Count > 0)
                {
                    dtTransTemp = GetCurrentBalance(dvTrans.ToTable());
                }
                if (dvCashTrans.Count > 0)
                {
                    dtCashBankTemp = GetCurrentBalance(dvCashTrans.ToTable());
                }
            }

            if (dtTransTemp != null && dtTransTemp.Rows.Count > 0)
            {
                gcTransaction.DataSource = dtTransTemp;
                gvBank.RefreshData();
            }
            if (dtCashBankTemp != null && dtCashBankTemp.Rows.Count > 0)
            {
                gcBank.DataSource = dtCashBankTemp;
                gcBank.RefreshDataSource();
            }

            // if payment mode selected unvisible the columns
            ShowTransGST();

            // Visible the reference No columns visible
            ShowTransReference();

        }

        private void FetchVoucherMethod()
        {
            using (ProjectSystem projectSystem = new ProjectSystem())
            {
                string VoucherMethod = rgTransactionType.SelectedIndex == 0 ? "1" : rgTransactionType.SelectedIndex == 1 ? "2" : "3";
                //int VoucherDefinitionId = rgTransactionType.SelectedIndex == 0 ? 1 : rgTransactionType.SelectedIndex == 1 ? 2 : 3;
                resultArgs = projectSystem.FetchVoucherByProjectId(ProjectId, VoucherMethod, VoucherDefinitionId);
                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    TransVoucherMethod = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][projectSystem.AppSchema.Voucher.VOUCHER_METHODColumn.ColumnName].ToString());
                    int VoucherEnabled = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][projectSystem.AppSchema.Voucher.IS_NARRATION_ENABLEDColumn.ColumnName].ToString());
                    EnableLedgerNarration = VoucherEnabled == 0 ? false : true;
                    if (TransVoucherMethod == 1)
                    {
                        txtVoucher.Enabled = false;
                        FocusTransactionGrid();
                    }
                    else
                    {
                        //On 10/05/2022, for sdbinm, no manual receipt number
                        //txtVoucher.Enabled = true;
                        //txtVoucher.Select();
                        //txtVoucher.Focus();
                        if (this.AppSetting.IS_SDB_INM && VoucherType == DefaultVoucherTypes.Receipt &&
                                UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false) >= UtilityMember.DateSet.ToDate(SettingProperty.Enforce_Receipt_Module_FY, false))
                        {
                            txtVoucher.Enabled = false;
                            FocusTransactionGrid();
                        }
                        else
                        {
                            txtVoucher.Enabled = true;
                            txtVoucher.Select();
                            txtVoucher.Focus();
                        }
                    }

                    if (EnableLedgerNarration)
                    {
                        colLedgerNarration.Visible = true;
                    }
                    else
                    {
                        colLedgerNarration.Visible = false;
                    }
                }
                else
                {
                    TransVoucherMethod = 2;
                }


            }
        }

        private void LoadCashBank(RepositoryItemGridLookUpEdit glkpLedger)
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    ledgerSystem.ProjectId = ProjectId;
                    resultArgs = ledgerSystem.FetchCashBankFDLedger();
                    glkpLedger.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataView dvLedger = (DataView)resultArgs.DataSource.Table.DefaultView;
                        dvLedger.RowFilter = "LEDGER_ID <> " + LedgerId;
                        glkpLedger.DataSource = dvLedger.ToTable();
                        glkpLedger.DisplayMember = "LEDGER_NAME";
                        glkpLedger.ValueMember = "LEDGER_ID";
                        dvLedger.RowFilter = "";
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_CASHBANK_MAPPING_TO_PROJECT) + ProjectName + "");
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private bool IsValidaCashBankTransactionRow()
        {
            bool IsCashLedgerValid = false;
            try
            {
                IsCashLedgerValid = (CashLedgerId > 0 && CashLedgerAmount > 0);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            return IsCashLedgerValid;
        }

        private void LoadCashBankLedger(RepositoryItemGridLookUpEdit glkpLedger)
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    ledgerSystem.ProjectId = ProjectId;
                    ledgerSystem.LedgerClosedDateForFilter = dtTransactionDate.DateTime.ToShortDateString();
                    resultArgs = ledgerSystem.FetchCashBankLedger();
                    //glkpLedger.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        glkpLedger.ValueMember = "LEDGER_ID";
                        glkpLedger.DisplayMember = "LEDGER_NAME";   // This is Disable the Closed Bank....
                        DataTable dtCashBankLedger = FilterLedgerByDateClosed(resultArgs.DataSource.Table, true, dtTransactionDate.DateTime); // resultArgs.DataSource.Table;

                        //21/08/2024, To set Bank Ledger currency mode
                        //On 26/08/2024, If multi currency enabled, let us load cash and bank ledgers only for selected currency
                        //On 11/11//2024, For contra, voucher can be done between currency
                        if (this.AppSetting.AllowMultiCurrency == 1 && (rgTransactionType.SelectedIndex != 2 || glkpLedger == rglkpLedger))
                        {
                            Int32 currencycountry = glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString());
                            dtCashBankLedger.DefaultView.RowFilter = ledgerSystem.AppSchema.Ledger.CUR_COUNTRY_IDColumn.ColumnName + " = " + currencycountry;
                            dtCashBankLedger = dtCashBankLedger.DefaultView.ToTable();
                        }
                        Int32 PrevLedgerID = 0;
                        if (glkpLedger == rglkpCashLedger)
                        {
                            PrevLedgerID = gvBank.GetFocusedRowCellValue(colCashLedger) == null ? 0 :
                                                     this.UtilityMember.NumberSet.ToInteger(gvBank.GetFocusedRowCellValue(colCashLedger).ToString());
                        }
                        else
                        {
                            PrevLedgerID = gvTransaction.GetFocusedRowCellValue(colCashLedger) == null ? 0 :
                                                     this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetFocusedRowCellValue(colCashLedger).ToString());
                        }

                        glkpLedger.DataSource = dtCashBankLedger;

                        //After selecting all the values and change the currency, let us clear cash/bank ledgers
                        if (this.AppSetting.AllowMultiCurrency == 1)
                        {
                            if (glkpLedger == rglkpCashLedger)
                            {
                                if (glkpLedger.GetDisplayValueByKeyValue(PrevLedgerID) != null)
                                    gvBank.SetFocusedRowCellValue(colCashLedger, PrevLedgerID);
                                else
                                    gvBank.SetFocusedRowCellValue(colCashLedger, null);
                                gvBank.PostEditor();
                                gvBank.UpdateCurrentRow();
                            }
                            else
                            {
                                if (glkpLedger.GetDisplayValueByKeyValue(PrevLedgerID) != null)
                                    gvTransaction.SetFocusedRowCellValue(colCashLedger, PrevLedgerID);
                                else
                                    gvTransaction.SetFocusedRowCellValue(colCashLedger, null);
                                gvTransaction.PostEditor();
                                gvTransaction.UpdateCurrentRow();
                            }
                        }

                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_CASHBANK_MAPPING_TO_PROJECT) + ProjectName + "");
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void LoadJournal()
        {
            //JournalAdd jadd = new JournalAdd("", ProjectId, ProjectName, VoucherId, (int)DefaultVoucherTypes.Journal - 1); On 05/08/2019
            JournalAdd jadd = new JournalAdd(recentVoucherDate, ProjectId, ProjectName, 0, 0, (int)DefaultVoucherTypes.Journal);
            jadd.ShowDialog();
        }

        private void LoadReceiptType()
        {
            ReceiptType receiptType = new ReceiptType();
            DataView dvReceiptType = this.UtilityMember.EnumSet.GetEnumDataSource(receiptType, Sorting.Ascending);
            glkpReceiptType.Properties.DataSource = dvReceiptType.ToTable();
            glkpReceiptType.Properties.DisplayMember = "Name";
            glkpReceiptType.Properties.ValueMember = "Id";
        }

        private void LoadCountry()
        {
            try
            {
                using (CountrySystem countrySystem = new CountrySystem())
                {
                    //On 26/08/2024, If multi currency enabled, let us load only currencies with have exchange rate for voucher date
                    if (this.AppSetting.AllowMultiCurrency == 1)
                        resultArgs = countrySystem.FetchCountryCurrencyDetails(this.UtilityMember.DateSet.ToDate(dtTransactionDate.Text, false));
                    else
                        resultArgs = countrySystem.FetchCountryDetails();

                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        DataTable dtCurencyCountry = resultArgs.DataSource.Table;
                        dtCurencyCountry.DefaultView.RowFilter = "";

                        //26/08/2024, Load Currecny which have exchange rate
                        if (this.AppSetting.AllowMultiCurrency == 1)
                        {
                            dtCurencyCountry.DefaultView.RowFilter = countrySystem.AppSchema.Country.EXCHANGE_RATEColumn.ColumnName + " >0"; ;
                            dtCurencyCountry = dtCurencyCountry.DefaultView.ToTable();
                        }
                        //this.UtilityMember.ComboSet.BindLookUpEditCombo(glkpCurrencyCountry, resultArgs.DataSource.Table, countrySystem.AppSchema.Country.CURRENCYColumn.ToString(), countrySystem.AppSchema.Country.COUNTRY_IDColumn.ToString());
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpCurrencyCountry, resultArgs.DataSource.Table, countrySystem.AppSchema.Country.CURRENCYColumn.ToString(), countrySystem.AppSchema.Country.COUNTRY_IDColumn.ToString());

                        //26/08/2024, For new voucher, set default currecny (global setting)
                        if (this.AppSetting.AllowMultiCurrency == 1 && VoucherId == 0 && DuplicateVoucher == false)
                        {
                            glkpCurrencyCountry.EditValue = string.IsNullOrEmpty(this.AppSetting.Country) ? 0 : UtilityMember.NumberSet.ToInteger(this.AppSetting.Country);

                            object findcountry = glkpCurrencyCountry.Properties.GetDisplayValueByKeyValue(this.AppSetting.Country);
                            if (findcountry == null) glkpCurrencyCountry.EditValue = null;
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

        private void LoadNarrationAutoComplete()
        {
            try
            {
                using (VoucherTransactionSystem vouchermastersystem = new VoucherTransactionSystem())
                {
                    resultArgs = vouchermastersystem.AutoFetchNarration();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataView dvNarration = resultArgs.DataSource.Table.AsDataView();
                        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            collection.Add(dr[vouchermastersystem.AppSchema.VoucherMaster.NARRATIONColumn.ColumnName].ToString());
                        }
                        txtNarration.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        txtNarration.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        txtNarration.MaskBox.AutoCompleteCustomSource = collection;


                        //dvNarration.RowFilter = ("VOUCHER_ID=" + dvNarration.ToTable().Compute("max(voucher_id)", string.Empty)).ToString();
                        //txtNarration.Text = dvNarration.ToTable().Rows[0]["NARRATION"].ToString();
                        //dvNarration.RowFilter = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void LoadNameAddressAutoComplete()
        {
            try
            {
                using (VoucherTransactionSystem vouchermastersystem = new VoucherTransactionSystem())
                {
                    resultArgs = vouchermastersystem.FetchAutoFetchNames();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            collection.Add(dr[vouchermastersystem.AppSchema.VoucherMaster.NAME_ADDRESSColumn.ColumnName].ToString());
                        }
                        txtNameAddress.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        txtNameAddress.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        txtNameAddress.MaskBox.AutoCompleteCustomSource = collection;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void ShowHideDonor(bool dontloadagain = false)
        {
            colSource.VisibleIndex = 0;
            colLedger.VisibleIndex = 1;
            colAmount.VisibleIndex = 2;

            //GSt 28.11.2019
            if (colGStAmt.Visible)
                colGSTLedgerClass.VisibleIndex = 3;
            else
                colGSTLedgerClass.Visible = false;

            // Ledger Narration - 27.08.2019 
            if (EnableLedgerNarration)
                colLedgerNarration.VisibleIndex = 4;

            // reference
            //colReferenceNumber.VisibleIndex = 3;

            colLedgerBal.VisibleIndex = 5;
            if (BudgetId != 0 && rgTransactionType.SelectedIndex != 2)
            {
                colBudgetAmount.VisibleIndex = 6;
                colValueDate.Visible = false;
                colValueDate.VisibleIndex = -1;
            }
            else
            {
                colBudgetAmount.Visible = false;
                colValueDate.Visible = false;
                colCheque.Visible = false;
            }

            if (colGStAmt.Visible) colGStAmt.VisibleIndex = 7;
            else
                colGStAmt.Visible = false;
            colAction.VisibleIndex = 8;
            colCostCentre.VisibleIndex = 9;

            // colCheque.Visible = false;
            // colMaterializedOn.Visible = false;

            if (rgTransactionType.SelectedIndex == 0)
            {
                colCostCentre.Visible = true;
                this.Text = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_TRANSACTION_RECEIPT);
                lblEntry.Text = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.RECEIPT);
                gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colSource, (int)Source.To);
                gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCashSource, (int)Source.By);
                VoucherType = DefaultVoucherTypes.Receipt;
                // rglkpCashSource.NullText = (this.AppSetting.TransMode == "2") ? CashSource.By.ToString() : "Dr";
                rglkpCashFlag.NullText = CashFlag.Cash.ToString();
                lblEntry.Appearance.BackColor = rgTransactionType.BackColor = gvTransaction.Appearance.Row.BackColor =
                    gvBank.Appearance.Row.BackColor = repositoryItemGridLookUpEdit1View.Appearance.FocusedRow.BackColor =
                    gvTransaction.Appearance.FocusedRow.BackColor = gvBank.Appearance.FocusedRow.BackColor =
                    gvCashLedger.Appearance.FocusedRow.BackColor = Color.LightSteelBlue;
                ucAdditionalInfo.DiableDonor = BarItemVisibility.Always;
                //if ((int)Division.Foreign == DivisionId)
                //{
                //    ClearDonorDetails();
                //    glkpPurpose.Enabled = false;
                //    glkpDonor.Enabled = true;
                //    lciDonorInfo.Visibility = LayoutVisibility.Always;
                //    flyoutPanel1.ShowPopup();
                //    glkpDonor.Select();
                //    glkpDonor.Focus();
                //}
                //else
                //{
                // DisableDonorInfo();
                //}

            }
            else if (rgTransactionType.SelectedIndex == 1)
            {
                colCostCentre.Visible = true;
                this.Text = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_TRANSACTION_PAYMENT);
                lblEntry.Text = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.PAYMENT);
                VoucherType = DefaultVoucherTypes.Payment;
                // rglkpSource.NullText = (this.AppSetting.TransMode == "1") ? "Dr" : Source.To.ToString();
                gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colSource, (int)Source.By);
                gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCashSource, (int)Source.To);
                //  rglkpCashSource.NullText = (this.AppSetting.TransMode == "2") ? CashSource.To.ToString() : "Cr";
                rglkpCashFlag.NullText = CashFlag.Cash.ToString();
                lblEntry.Appearance.BackColor = rgTransactionType.BackColor = gvTransaction.Appearance.Row.BackColor =
                    gvBank.Appearance.Row.BackColor = repositoryItemGridLookUpEdit1View.Appearance.FocusedRow.BackColor =
                    gvTransaction.Appearance.FocusedRow.BackColor = gvBank.Appearance.FocusedRow.BackColor =
                    gvCashLedger.Appearance.FocusedRow.BackColor = Color.Wheat;
                ucAdditionalInfo.DiableDonor = BarItemVisibility.Always;
                //  colLedgerBal.Width = colLedgerBal.Width - colCostCentre.Width;
                //if ((int)Division.Foreign == DivisionId)
                //{
                //    glkpDonor.Properties.ImmediatePopup = false;
                //    ClearDonorDetails();
                //    glkpDonor.Enabled = false;
                //    glkpPurpose.Enabled = true;
                //    lciDonorInfo.Visibility = LayoutVisibility.Always;
                //    flyoutPanel1.ShowPopup();
                //}
                //else
                //{
                //DisableDonorInfo();
                // }
            }
            else
            {
                colCostCentre.Visible = false;
                this.Text = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_TRANSACTION_CONTRA);
                lblEntry.Text = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.CONTRA);
                VoucherType = DefaultVoucherTypes.Contra;
                // rglkpSource.NullText = (this.AppSetting.TransMode == "1") ? "Cr" : Source.By.ToString();
                gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colSource, (int)Source.To);
                gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCashSource, (int)Source.By);
                // rglkpCashSource.NullText = (this.AppSetting.TransMode == "2") ? CashSource.By.ToString() : "Dr";
                rglkpCashFlag.NullText = CashFlag.Cash.ToString();
                lblEntry.Appearance.BackColor = rgTransactionType.BackColor = gvTransaction.Appearance.Row.BackColor =
                    gvBank.Appearance.Row.BackColor = repositoryItemGridLookUpEdit1View.Appearance.FocusedRow.BackColor =
                    gvTransaction.Appearance.FocusedRow.BackColor = gvBank.Appearance.FocusedRow.BackColor =
                    gvCashLedger.Appearance.FocusedRow.BackColor = Color.LightYellow;
                ucAdditionalInfo.DiableDonor = BarItemVisibility.Never;
                // colLedgerBal.Width = colLedgerBal.Width+ colCostCentre.Width;
                // DisableDonorInfo();
            }

            // if (!dontloadagain) On 23/10/2024 to clear grid values when we change voucher type (Clear for contr alone)
            LoadSelectedLedger();
            ShowExchangeRateColumns();

            //On 15/11/2024
            colLedgerBal.Visible = false;
            colLedgerBalance.Visible = false;
        }

        private void DisableDonorInfo()
        {
            DisableDonorFields();
            ClearDonorDetails();
            //flypnlDonorInfo.HidePopup();
            lciDonorInfo.Visibility = LayoutVisibility.Never;
        }

        private void CalculateExchangeRate()
        {
            try
            {
                Double ActualAmt = this.UtilityMember.NumberSet.ToDouble(txtCurrencyAmount.Text) * this.UtilityMember.NumberSet.ToDouble(txtExchangeRate.Text);

                lblCalculatedAmt.Text = ActualAmt.ToString();
                lblCalculatedAmt.Width = 275;
                //lcActualAmount.Location = new Point((lblCalculatedAmt.Location.X + lblCalculatedAmt.Width), lcActualAmount.Location .Y);
                txtActualAmount.Text = ActualAmt.ToString();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// 12/11/2024, To set voucher currency 
        /// </summary>
        private void ApplyVoucherCurrencyToLedgers()
        {
            try
            {
                //For General Ledgers
                for (int i = 0; i < gvTransaction.DataRowCount; i++)
                {
                    int handle = gvTransaction.GetRowHandle(i);
                    Int32 lid = UtilityMember.NumberSet.ToInteger(gvTransaction.GetRowCellValue(handle, colLedger).ToString());
                    if (lid > 0)
                    {
                        gvTransaction.SetRowCellValue(handle, colExchangeAmount, UtilityMember.NumberSet.ToDecimal(txtExchangeRate.Text));
                        gvTransaction.SetRowCellValue(handle, colLiveExchangeAmount, UtilityMember.NumberSet.ToDecimal(lblLiveExchangeRate.Text));
                        gvTransaction.UpdateCurrentRow();
                    }
                }

                //For Cash and Bank Ledgers
                for (int i = 0; i < gvBank.DataRowCount; i++)
                {
                    int handle = gvBank.GetRowHandle(i);
                    Int32 lid = UtilityMember.NumberSet.ToInteger(gvBank.GetRowCellValue(handle, colCashLedger).ToString());
                    if (lid > 0)
                    {
                        //For Contra
                        if (rgTransactionType.SelectedIndex == 2)
                        {
                            double cashbankexchange = gvBank.GetRowCellValue(handle, colCashBankExchangeAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvBank.GetRowCellValue(handle, colCashBankExchangeAmount).ToString()) : 0;
                            double livecashbankexchange = gvBank.GetRowCellValue(handle, colCashBankLiveExchangeAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvBank.GetRowCellValue(handle, colCashBankLiveExchangeAmount).ToString()) : 0;
                            if (livecashbankexchange == 0) gvBank.SetRowCellValue(handle, colCashBankLiveExchangeAmount, cashbankexchange);

                        }
                        else
                        {
                            gvBank.SetRowCellValue(handle, colCashBankExchangeAmount, UtilityMember.NumberSet.ToDecimal(txtExchangeRate.Text));
                            gvBank.SetRowCellValue(handle, colCashBankLiveExchangeAmount, UtilityMember.NumberSet.ToDecimal(lblLiveExchangeRate.Text));
                        }
                        gvBank.UpdateCurrentRow();
                    }
                }
                gvBank.FocusedColumn = colCashLedger;
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        // On 12/12/2024 - get Live exchange rate, if we received live exchange rate, let us have live exchange rate
        /// </summary>
        private void AssignLiveExchangeRate()
        {
            lblLiveExchangeRate.ForeColor = Color.Black;
            lcLiveExchangeRate.AppearanceItemCaption.ForeColor = Color.Black;
            lblLiveExchangeRate.Font = new System.Drawing.Font(lblLiveExchangeRate.Font.FontFamily, lblLiveExchangeRate.Font.Size, FontStyle.Regular);
            lcLiveExchangeRate.AppearanceItemCaption.Font = new System.Drawing.Font(lcLiveExchangeRate.AppearanceItemCaption.Font.FontFamily,
                    lcLiveExchangeRate.AppearanceItemCaption.Font.Size, FontStyle.Regular);

            int CountryId = (glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString()));
            using (CountrySystem countrySystem = new CountrySystem())
            {
                ResultArgs result = countrySystem.FetchCountryCurrencyExchangeRateByCountryDate(CountryId, UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false));
                if (result.Success)
                {
                    lblLiveExchangeRate.Text = UtilityMember.NumberSet.ToNumber(countrySystem.ExchangeRate);
                    this.ShowWaitDialog("Fetching Live Exchange Rate");
                    double liveExchangeAmount = this.AppSetting.CurrencyLiveExchangeRate(dtTransactionDate.DateTime.Date, countrySystem.CurrencyCode, AppSetting.CurrencyCode);
                    this.CloseWaitDialog();
                    if (liveExchangeAmount > 0)
                    {
                        lblLiveExchangeRate.Text = UtilityMember.NumberSet.ToNumber(liveExchangeAmount);

                        lblLiveExchangeRate.ForeColor = Color.Green;
                        lcLiveExchangeRate.AppearanceItemCaption.ForeColor = Color.Green;
                        lblLiveExchangeRate.Font = new System.Drawing.Font(lblLiveExchangeRate.Font.FontFamily, lblLiveExchangeRate.Font.Size, (FontStyle.Bold | FontStyle.Underline));
                        lcLiveExchangeRate.AppearanceItemCaption.Font = new System.Drawing.Font(lcLiveExchangeRate.AppearanceItemCaption.Font.FontFamily,
                                lcLiveExchangeRate.AppearanceItemCaption.Font.Size, (FontStyle.Bold | FontStyle.Underline));
                    }
                }
            }
        }

        public void LoadPurposeDetails()
        {
            try
            {
                using (PurposeSystem purposeSystem = new PurposeSystem())
                {
                    purposeSystem.ProjectId = ProjectId;
                    resultArgs = purposeSystem.FetchPurposeMappedDetails();
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpPurpose, resultArgs.DataSource.Table, purposeSystem.AppSchema.Purposes.FC_PURPOSEColumn.ColumnName, purposeSystem.AppSchema.Purposes.CONTRIBUTION_IDColumn.ColumnName);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void LoadDonorDetails()
        {
            try
            {
                using (MappingSystem ms = new MappingSystem())
                {
                    ms.ProjectId = ProjectId;
                    //resultArgs = ms.FetchMappedDonor();
                    AssignInactiveDonors();
                    int Status = ms.CheckDonorStatus(DonorId); //0--Inactive Donor,1---Active Donor
                    if (VoucherId > 0 && DonorId > 0 && Status == 0)
                    {
                        ms.DonorId = DonorId;
                    }
                    resultArgs = ms.FetchMappedActiveDonors();
                    if (resultArgs.Success)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpDonor, resultArgs.DataSource.Table, ms.AppSchema.DonorAuditor.NAMEColumn.Caption, ms.AppSchema.DonorAuditor.DONAUD_IDColumn.Caption);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            {
            }
        }

        private void LoadPendingGSTInvoices()
        {
            try
            {

                lgGSTInvoce.Visibility = LayoutVisibility.Never;
                lkpGSTInvoices.Properties.DataSource = null;
                lblGSTInvoiceTotalAmount.Text = "0.00";
                lblGSTInvoiceBalance.Text = "0.00";
                lgGSTInvoce.Update();

                if (this.AppSetting.IncludeGSTVendorInvoiceDetails == "2" && (rgTransactionType.SelectedIndex == 0) && IsGeneralInvolice) //|| rgTransactionType.SelectedIndex == 1
                {
                    DefaultVoucherTypes vtype = (rgTransactionType.SelectedIndex == 1 ? DefaultVoucherTypes.Payment : DefaultVoucherTypes.Receipt);
                    using (VoucherTransactionSystem voucherTransSys = new VoucherTransactionSystem())
                    {
                        Int32 prevInvoiceId = (lkpGSTInvoices.EditValue == null) ? 0 : BookingGSTInvoiceId;
                        resultArgs = voucherTransSys.FetchGSTPenindgInvoices(VoucherId, ProjectId, vtype, dtTransactionDate.DateTime);
                        if (resultArgs.Success)
                        {
                            this.UtilityMember.ComboSet.BindGridLookUpComboEmptyItem(lkpGSTInvoices, resultArgs.DataSource.Table, "VENDOR_GST_INVOICE",
                                voucherTransSys.AppSchema.GSTInvoiceMaster.GST_INVOICE_IDColumn.Caption, true, "- Select Invoice -");
                            //|| rgTransactionType.SelectedIndex == 1
                            if ((rgTransactionType.SelectedIndex == 0) && resultArgs.DataSource.Table.Rows.Count > 1) //"first item is select Invoice"
                            {
                                lgGSTInvoce.Visibility = LayoutVisibility.Always;
                            }
                            lkpGSTInvoices.EditValue = prevInvoiceId;
                            this.SetGridLookPopupWindowSize(lkpGSTInvoices);
                            //lkpGSTInvoices.Properties.PopupFormSize = new System.Drawing.Size(lkpGSTInvoices.Width, lkpGSTInvoices.Properties.PopupFormSize.Height);
                            colGSTParty.Caption = VoucherType == DefaultVoucherTypes.Payment ? "Creditors" : "Debtors";
                        }
                    }
                }
                else
                {
                    lkpGSTInvoices.EditValue = null;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            {

            }
        }

        private void ClearControls()
        {
            if (VoucherId == 0)
            {
                glkpDonor.EditValue = null;
                glkpPurpose.EditValue = null;
                glkpReceiptType.EditValue = null;
                glkpCurrencyCountry.EditValue = null;

                //On 26/08/2024, If multi currency enabled, let us set default currency from global setting
                if (this.AppSetting.AllowMultiCurrency == 1)
                {
                    glkpCurrencyCountry.EditValue = this.AppSetting.Currency;

                    object findcountry = glkpCurrencyCountry.Properties.GetDisplayValueByKeyValue(this.AppSetting.Country);
                    if (findcountry == null) glkpCurrencyCountry.EditValue = null;
                }

                txtActualAmount.Text = string.Empty;
                txtCurrencyAmount.Text = string.Empty;
                txtExchangeRate.Text = string.Empty;
                lblLiveExchangeRate.Text = "0.0";
                txtNameAddress.Text = string.Empty;
                //txtPanNumber.Text = string.Empty;
                //txtGSTNumber.Text = string.Empty;
                txtNarration.Text = string.Empty;
                txtVoucher.Text = string.Empty;
                txtPanNo.Text = string.Empty;
                txtGSTNo.Text = string.Empty;
                dsCostCentre.Clear();
                dtReferenceVoucherNumber.Clear();
                LoadPendingGSTInvoices();
                //07/02/2020, Load Voucher Sub Ledger Vouchers
                //dtSubLedgerVouchers.Rows.Clear();
                FetchSubLedgerVouchers();

                lblCalculatedAmt.Text = "0.00";

                //lblShowGSTAmt.Text = " ";

                DisableDonorFields();
                VoucherTypeId = rgTransactionType.SelectedIndex;
                ucAdditionalInfo.DisableDeleteVocuher = BarItemVisibility.Never;
                ucAdditionalInfo.DisablePrintVoucher = BarItemVisibility.Never;
                ConstructEmptyDataSource();
                VisibleCashBankAdditionalFields(false);
                VisibleTransBankAdditionalFields(false);
                SetBudgetInfo();
                if ((int)Division.Foreign != DivisionId) { DisableDonorInfo(); }
                if (rgTransactionType.SelectedIndex == 2) { LoadSelectedLedger(); }
                ShowHideDonor();
                //this.dtTransactionDate.Select();

                //o7/04/2017, to foucus on day on date control after saving voucher..it goes to year
                if (!string.IsNullOrEmpty(dtTransactionDate.Text))
                {
                    dtTransactionDate.SelectionStart = 0;
                }
                this.dtTransactionDate.Focus();
                DuplicateVoucher = false;//After savining duplication entry, change as default enty mode

                //26/04/2019, to clear vendor gst invoice details
                GSTInvoiceId = 0;
                GSTVendorInvoiceNo = string.Empty;
                GSTVendorInvoiceDate = string.Empty;
                GSTVendorInvoiceType = 0;
                GSTVendorId = 0;
                DtGSTInvoiceMasterDetails = null;
                DtGSTInvoiceMasterLedgerDetails = null;

                if (lgGSTInvoce.Visibility == LayoutVisibility.Always)
                {
                    lkpGSTInvoices.EditValue = 0;
                }

                dtVoucherImages.Rows.Clear();
            }
            else
            {
                if (this.UIAppSetting.UITransClose == "1")
                { this.Close(); }
            }
        }

        private void ShowTDSForm(double LedgerAmount)
        {
            try
            {
                int isTDSLedger = 0;
                string PartyLedgerName = string.Empty;
                int TDSGroupId = 0;
                using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
                {
                    voucherSystem.LedgerId = LedgerId;
                    resultArgs = voucherSystem.FetchIsTDSLedger();
                    if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        PartyLedgerName = resultArgs.DataSource.Table.Rows[0][voucherSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                        isTDSLedger = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][voucherSystem.AppSchema.Ledger.IS_TDS_LEDGERColumn.ColumnName].ToString());
                        TDSGroupId = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][voucherSystem.AppSchema.Ledger.GROUP_IDColumn.ColumnName].ToString());

                        if (TDSGroupId == (int)TDSDefaultLedgers.SunderyCreditors && this.AppSetting.TDSEnabled.Equals("1") && isTDSLedger == (int)YesNo.Yes && this.AppSetting.EnableBookingAtPayment.Equals("1"))
                        {
                            ShowTDSBookingForm(PartyLedgerName, LedgerAmount, LedgerId, isTDSLedger);
                        }
                        else if (VoucherId > 0 && IsPartyVoucher() > 0)
                        {
                            isTDSLedger = 1;
                            ShowTDSBookingForm(PartyLedgerName, LedgerAmount, LedgerId, isTDSLedger);
                        }


                        if (TDSGroupId == (int)TDSDefaultLedgers.DutiesandTaxes && isTDSLedger == (int)YesNo.Yes)
                        {
                            ShowTDSPaymentForm(PartyLedgerName, LedgerAmount);
                            // EnableMultiRow = true;
                        }
                        else if (VoucherId > 0 && IsTDSPaymentVoucher() > 0)
                        {
                            ShowTDSPaymentForm(PartyLedgerName, LedgerAmount, isTDSLedger > 0 ? true : false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
        }

        private void ShowPartyPaymentForm(string partyLedgerName, double LedgerAmt)
        {
            if (VoucherId > 0)
            {
                FetchPartyPaymentId();
            }
            frmPaymentsParty frmPartyPayment = new frmPaymentsParty(VoucherId > 0 ? TDSPartyPaymentId : (int)AddNewRow.NewRow, ProjectId, LedgerId, dtTransactionDate.DateTime, partyLedgerName, LedgerAmt);
            frmPartyPayment.ProjectName = ProjectName;
            frmPartyPayment.VoucherId = VoucherId;
            frmPartyPayment.CashBankLedgerId = TDSPartyBankId;
            frmPartyPayment.ShowDialog();
            if (frmPartyPayment.DialogResult == DialogResult.OK)
            {
                if (this.AppSetting.TDSPartyPayment != null && this.AppSetting.TDSPartyPayment.Rows.Count > 0)
                {
                    DataTable dtTemp = this.AppSetting.TDSPartyPayment;
                    double DebitAmount = this.UtilityMember.NumberSet.ToDouble(dtTemp.Compute("SUM(AMOUNT)", "TRANS_MODE='DR'").ToString());
                    double CreditAmount = this.UtilityMember.NumberSet.ToDouble(dtTemp.Compute("SUM(AMOUNT)", "TRANS_MODE='CR'").ToString());
                    object NetAmount = DebitAmount - CreditAmount;
                    DataTable dtFirst = dtTemp.AsEnumerable().Take(1).CopyToDataTable();
                    if (dtFirst != null && dtFirst.Rows.Count > 0)
                    {
                        dtFirst.Rows[0]["AMOUNT"] = NetAmount.ToString();
                        gcTransaction.DataSource = dtFirst;
                        gcTransaction.RefreshDataSource();
                    }

                    if (this.AppSetting.TDSPartyPaymentBank != null && this.AppSetting.TDSPartyPaymentBank.Rows.Count > 0)
                    {
                        gcBank.DataSource = this.AppSetting.TDSPartyPaymentBank;
                        gcBank.RefreshDataSource();
                        EnableCashBankFields();
                    }
                }
            }
        }

        private void ShowTDSBookingForm(string partyLedgerName, double LedgerAmt, int LedgerId, int IsTDSLedger = 0)
        {
            if (VoucherId > 0)
            {
                FetchTDSBookingID();
            }

            if (VoucherId > 0 && (ClientCode.Equals(ledgerSubType.TDS.ToString()) || IsTDSLedger > 0))
            {
                JournalAdd frmJournalAdd = new JournalAdd(dtTransactionDate.DateTime.ToShortDateString(), ProjectId, ProjectName, 0, LedgerId, LedgerAmt, 1);
                frmJournalAdd.VoucherId = PaymentBookingVoucherId;
                frmJournalAdd.ShowDialog();
                if (frmJournalAdd.DialogResult == DialogResult.OK)
                {
                    PaymentBookingVoucherId = frmJournalAdd.VoucherId;
                    if (LedgerId.Equals(frmJournalAdd.TDSPartyLedgerId))
                    {
                        gvTransaction.SetFocusedRowCellValue(colAmount, frmJournalAdd.PartyLedgerAmount);
                        string LedgerBalance = GetLedgerBalanceValues(gcTransaction.DataSource as DataTable, LedgerId);
                        gvTransaction.SetFocusedRowCellValue(colLedgerBalance, LedgerBalance);

                        //Assign Cash Bank Balance while at tds party payment
                        int FocusedColumn = gvBank.FocusedRowHandle;
                        if (FocusedColumn >= 0)
                        {
                            int CashBankId = gvBank.GetFocusedRowCellValue(colCashLedger) != null ? this.UtilityMember.NumberSet.ToInteger(gvBank.GetFocusedRowCellValue(colCashLedger).ToString()) : 0;
                            gvBank.SetRowCellValue(FocusedColumn, colCashAmount, frmJournalAdd.PartyLedgerAmount);
                            string CashBankBalance = GetLedgerBalanceValues(gcBank.DataSource as DataTable, CashBankId);
                            gvBank.SetRowCellValue(FocusedColumn, colLedgerBalance, CashBankBalance);
                        }
                    }
                }
            }
            else if (VoucherId == 0)
            {
                JournalAdd frmJournalAdd = new JournalAdd(dtTransactionDate.DateTime.ToShortDateString(), ProjectId, ProjectName, 0, LedgerId, LedgerAmt, 1);
                frmJournalAdd.VoucherId = PaymentBookingVoucherId;
                frmJournalAdd.ShowDialog();
                if (frmJournalAdd.DialogResult == DialogResult.OK)
                {
                    PaymentBookingVoucherId = frmJournalAdd.VoucherId;
                    gvTransaction.SetFocusedRowCellValue(colAmount, frmJournalAdd.PartyLedgerAmount);
                    string LedgerBalance = GetLedgerBalanceValues(gcTransaction.DataSource as DataTable, frmJournalAdd.TDSPartyLedgerId);
                    gvTransaction.SetFocusedRowCellValue(colLedgerBalance, LedgerBalance);
                    gvTransaction.SetFocusedRowCellValue(colLedger, frmJournalAdd.TDSPartyLedgerId);
                    //Assign Cash Bank Balance whilke at tds party payment
                    int FocusedColumn = gvBank.FocusedRowHandle;
                    if (FocusedColumn >= 0)
                    {
                        int CashBankId = gvBank.GetFocusedRowCellValue(colCashLedger) != null ? this.UtilityMember.NumberSet.ToInteger(gvBank.GetFocusedRowCellValue(colCashLedger).ToString()) : 0;
                        gvBank.SetRowCellValue(FocusedColumn, colCashAmount, frmJournalAdd.PartyLedgerAmount);
                        //  gvBank.SetRowCellValue(FocusedColumn,colCashAmount
                        string CashBankBalance = GetLedgerBalanceValues(gcBank.DataSource as DataTable, CashBankId);
                        gvBank.SetRowCellValue(FocusedColumn, colLedgerBalance, CashBankBalance);
                        gvBank.UpdateCurrentRow();
                    }
                }
            }
        }

        private void ShowTDSPaymentForm(string partyLedgerName, double LedgerAmt, bool IsTDSOptionEnabled = false)
        {
            try
            {
                int FocusedRow = gvTransaction.FocusedRowHandle;
                if (VoucherId > 0)
                {
                    FetchPaymentId();
                }
                frmPaymentsTDS frmTDSPayment = new frmPaymentsTDS(VoucherId > 0 ? TDSPaymentId : (int)AddNewRow.NewRow, ProjectId, LedgerId, dtTransactionDate.DateTime, partyLedgerName, LedgerAmt);
                frmTDSPayment.CashBankVoucherId = CashBankId;
                frmTDSPayment.ProjectName = ProjectName;
                frmTDSPayment.dtTDSPaymentInfo = dtTDSPaymentInfo;
                frmTDSPayment.PrevInsAmount = InterestAmount;
                frmTDSPayment.PrevPenAmount = PenaltyAmount;
                frmTDSPayment.InsLedgerId = InterestLedgerId;
                frmTDSPayment.PenLedgerId = PenaltyLedgerId;
                frmTDSPayment.IsTDSOptionEnabled = IsTDSOptionEnabled;
                frmTDSPayment.ShowDialog();
                if (frmTDSPayment.DialogResult == DialogResult.OK)
                {
                    if (this.AppSetting.TDSPayment != null && this.AppSetting.TDSPayment.Rows.Count > 0)
                    {
                        DataTable dtTemp = this.AppSetting.TDSPayment;
                        InterestLedgerId = frmTDSPayment.InsLedgerId;
                        PenaltyLedgerId = frmTDSPayment.PenLedgerId;
                        InterestAmount = frmTDSPayment.PrevInsAmount;
                        PenaltyAmount = frmTDSPayment.PrevPenAmount;
                        object TempAmount = null;
                        object InsAmount = null;
                        int TDSLedgerId = LedgerId;
                        object NetAmount = dtTemp.Compute("SUM(AMOUNT)", "LEDGER_ID=" + LedgerId + "");
                        TDSPrevTaxAmount = this.UtilityMember.NumberSet.ToDouble(dtTemp.Compute("SUM(AMOUNT)", "LEDGER_ID=" + LedgerId + "").ToString());

                        if (InterestLedgerId.Equals(PenaltyLedgerId))
                        {
                            InsAmount = dtTemp.Compute("SUM(AMOUNT)", "LEDGER_ID=" + InterestLedgerId + "");
                        }

                        TempAmount = NetAmount;
                        dtTDSPaymentInfo = this.AppSetting.TDSPayment;
                        DataTable dtLedgerInfo = dtTemp.AsEnumerable().GroupBy(r => r.Field<UInt32?>("LEDGER_ID")).Select(g => g.First()).CopyToDataTable();
                        if (dtLedgerInfo != null && dtLedgerInfo.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtLedgerInfo.Rows)
                            {
                                if (this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString()) == TDSLedgerId)
                                    dr.SetField("AMOUNT", TempAmount);
                                else if (InterestLedgerId.Equals(PenaltyLedgerId) && this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString()) == PenaltyLedgerId)
                                    dr.SetField("AMOUNT", InsAmount);
                            }
                        }

                        gvTransaction.DeleteRow(gvTransaction.FocusedRowHandle);
                        ConstructEmptyDataSource();

                        foreach (DataRow dr in dtLedgerInfo.Rows)
                        {
                            gvTransaction.AddNewRow();
                            int SourceID = dr["SOURCE"] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dr["SOURCE"].ToString()) : 0;
                            gvTransaction.SetRowCellValue(FocusedRow, colSource, SourceID);
                            int Id = dr["LEDGER_ID"] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString()) : 0;
                            gvTransaction.SetRowCellValue(FocusedRow, colLedger, Id);
                            double LedgerAmount = dr["AMOUNT"] != DBNull.Value ? this.UtilityMember.NumberSet.ToDouble(dr["AMOUNT"].ToString()) : 0;
                            gvTransaction.SetRowCellValue(FocusedRow, colAmount, LedgerAmount);
                            string Balance = GetLedgerBalanceValues(gcTransaction.DataSource as DataTable, Id);
                            gvTransaction.SetRowCellValue(FocusedRow, colLedgerBalance, Balance);
                            FocusedRow++;
                        }

                        if (this.AppSetting.TDSPaymentBank != null && this.AppSetting.TDSPaymentBank.Rows.Count > 0)
                        {
                            CashBankId = frmTDSPayment.CashBankVoucherId;
                            gcBank.DataSource = this.AppSetting.TDSPaymentBank;
                            gcBank.RefreshDataSource();
                            gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colLedgerBalance, GetLedgerBalanceValues(gcBank.DataSource as DataTable, CashBankId));
                            EnableCashBankFields();
                        }
                        gvTransaction.MoveLast();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.ERROR_SHOWING_TDS) + ex.ToString());
            }
        }

        private void FetchPaymentId()
        {
            using (Bosco.Model.TDS.TDSPaymentSystem tdsPaymentSystem = new Bosco.Model.TDS.TDSPaymentSystem())
            {
                tdsPaymentSystem.VoucherId = VoucherId;
                resultArgs = tdsPaymentSystem.FetchTDSPaymentId();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    TDSPaymentId = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][tdsPaymentSystem.AppSchema.TDSPayment.TDS_PAYMENT_IDColumn.ColumnName].ToString());
                    CashBankId = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][tdsPaymentSystem.AppSchema.TDSPayment.PAYMENT_LEDGER_IDColumn.ColumnName].ToString());
                }
            }
        }

        private void FetchPartyPaymentId()
        {
            using (Bosco.Model.TDS.PartyPaymentSystem tdsPaymentSystem = new Bosco.Model.TDS.PartyPaymentSystem())
            {
                tdsPaymentSystem.VoucherId = VoucherId;
                resultArgs = tdsPaymentSystem.FetchPartyPaymentId();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    TDSPartyPaymentId = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][tdsPaymentSystem.AppSchema.TDSPartyPaymentDetail.PARTY_PAYMENT_IDColumn.ColumnName].ToString());
                    TDSPartyBankId = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][tdsPaymentSystem.AppSchema.TDSPartyPayment.PAYMENT_LEDGER_IDColumn.ColumnName].ToString());
                }
            }
        }

        private void FetchTDSBookingID()
        {
            using (VoucherTransactionSystem voucherTransSystem = new VoucherTransactionSystem())
            {
                voucherTransSystem.VoucherId = VoucherId;
                resultArgs = voucherTransSystem.FetchBookingIdByVoucher();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    ClientCode = resultArgs.DataSource.Table.Rows[0][voucherTransSystem.AppSchema.VoucherMaster.CLIENT_CODEColumn.ColumnName].ToString();
                    if (ClientCode.Equals(VoucherSubTypes.TDS.ToString()))
                        PaymentBookingVoucherId = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][voucherTransSystem.AppSchema.VoucherMaster.CLIENT_REFERENCE_IDColumn.ColumnName].ToString());
                }
            }
        }

        public int IsPartyVoucher()
        {
            int Isvalid = 0;
            using (PartyPaymentSystem partysystem = new PartyPaymentSystem())
            {
                partysystem.VoucherId = VoucherId;
                Isvalid = partysystem.IsPartyVoucher();
            }
            return Isvalid;
        }

        public int IsTDSPaymentVoucher()
        {
            int Isvalid = 0;
            using (PartyPaymentSystem partysystem = new PartyPaymentSystem())
            {
                partysystem.VoucherId = VoucherId;
                Isvalid = partysystem.IsTDSPaymentVoucher();
            }
            return Isvalid;
        }

        private void ShowCostCentre(double LedgerAmount)
        {
            try
            {
                int CostCentre = 0;
                string LedgerName = string.Empty;
                DataView dvCostCentre = null;
                using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
                {
                    voucherSystem.LedgerId = LedgerId;
                    resultArgs = voucherSystem.FetchCostCentreLedger();
                    if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        LedgerName = resultArgs.DataSource.Table.Rows[0][voucherSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                        CostCentre = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][voucherSystem.AppSchema.Ledger.IS_COST_CENTERColumn.ColumnName].ToString());
                        //getGSTLedger()
                        if (CostCentre != 0 && !string.IsNullOrEmpty(LedgerName))
                        {
                            int RowIndex = this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetDataSourceRowIndex(gvTransaction.FocusedRowHandle).ToString());

                            if (dsCostCentre.Tables.Contains(RowIndex + "LDR" + LedgerId))
                            {
                                dvCostCentre = dsCostCentre.Tables[RowIndex + "LDR" + LedgerId].DefaultView;
                            }

                            // This is to Remove GST Amount from the Ledger Transaction Amount ( Mentione below code 17.01.2022
                            //frmTransactionCostCenter frmCostCentre = new frmTransactionCostCenter(ProjectId, dvCostCentre, LedgerId, LedgerAmount + getGSTLedger(LedgerId), LedgerName);    

                            //On 19/01/2022, To allocate cc amount with GST or withour GST based on Finnace setting
                            double ledgergstamount = 0;
                            if (this.AppSetting.AllocateCCAmountWithGST == 1)
                            {
                                ledgergstamount = getGSTLedger(LedgerId);
                            }

                            frmTransactionCostCenter frmCostCentre = new frmTransactionCostCenter(ProjectId, dvCostCentre, LedgerId, LedgerAmount + ledgergstamount, LedgerName);
                            frmCostCentre.ShowDialog();


                            if (frmCostCentre.DialogResult == DialogResult.OK)
                            {
                                DataTable dtValues = frmCostCentre.dtRecord;
                                if (dtValues != null)
                                {
                                    dtValues.TableName = RowIndex + "LDR" + LedgerId;
                                    if (dsCostCentre.Tables.Contains(dtValues.TableName))
                                    {
                                        dsCostCentre.Tables.Remove(dtValues.TableName);
                                    }
                                    dsCostCentre.Tables.Add(dtValues);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            finally { }
        }

        private bool IsFixedDepositLedger()
        {
            bool IsFixedDepositLedger = false;
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                ledgerSystem.LedgerId = LedgerId;
                GroupId = ledgerSystem.FetchLedgerGroupById();
                int BankAccountId = new LedgerSystem().FetchBankAccountById(CashLedgerId);
                if (GroupId == (int)FixedLedgerGroup.FixedDeposit)
                {
                    IsFixedDepositLedger = true;
                }
            }
            return IsFixedDepositLedger;
        }



        private void DeleteCashBankTransaction()
        {
            if (gvBank.RowCount > 1)
            {
                if (VoucherId > 0)
                {
                    if (HasTDSVoucher() == 0)
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            gvBank.DeleteRow(gvBank.FocusedRowHandle);
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.ASSOCICATION_WITH_TDS_PAYMENT));
                    }
                }
                else
                {
                    if (this.dtTDSPaymentInfo == null)
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            gvBank.DeleteRow(gvBank.FocusedRowHandle);
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.ASSOCICATION_WITH_TDS_PAYMENT));
                    }
                }
            }
            else if (gvBank.RowCount == 1)
            {
                if (CashLedgerId > 0 || CashLedgerAmount > 0)
                {
                    if (VoucherId > 0)
                    {
                        if (HasTDSVoucher() == 0)
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                ConstructCashTransEmptySournce();
                            }
                        }
                        else
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.ASSOCICATION_WITH_TDS_PAYMENT));
                        }
                    }
                    else
                    {
                        if (this.dtTDSPaymentInfo == null)
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                ConstructCashTransEmptySournce();
                            }
                        }
                        else
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.ASSOCICATION_WITH_TDS_PAYMENT));
                        }
                    }
                }
            }
            CalculateFirstRowValue();
        }

        private void DeleteTransaction()
        {
            try
            {
                if (gvTransaction.RowCount > 1)
                {
                    if (VoucherId > 0)
                    {
                        if (HasTDSLedger() == 0)
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                RemoveCostCentre(LedgerId);
                                gvTransaction.DeleteRow(gvTransaction.FocusedRowHandle);
                            }
                        }
                        else
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.ASSOCICATION_WITH_TDS_PAYMENT));
                        }
                    }
                    else
                    {
                        if (this.dtTDSPaymentInfo == null)
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                gvTransaction.DeleteRow(gvTransaction.FocusedRowHandle);
                                //ConstructEmptyDataSource();;
                            }
                        }
                        else
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.ASSOCICATION_WITH_TDS_PAYMENT));
                        }
                    }
                }
                else if (gvTransaction.RowCount == 1)
                {
                    if (LedgerId > 0 || LedgerAmount > 0)
                    {
                        if (VoucherId > 0)
                        {
                            if (HasTDSLedger() == 0)
                            {
                                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    ConstructTransEmptySource();
                                    int sourceId = (rgTransactionType.SelectedIndex == 0 || rgTransactionType.SelectedIndex == 2) ? (int)Source.To : (int)Source.By;
                                    gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colSource, sourceId);
                                }
                            }
                        }
                        else
                        {
                            if (this.dtTDSPaymentInfo == null)
                            {
                                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    gvTransaction.DeleteRow(gvTransaction.FocusedRowHandle);
                                    ConstructEmptyDataSource();
                                }
                            }
                            else
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.ASSOCICATION_WITH_TDS_PAYMENT));
                            }
                        }
                    }
                }
                CalculateFirstRowValue();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void RemoveCostCentre(int LedgerId)
        {
            if (LedgerId > 0)
            {
                if (dsCostCentre != null && dsCostCentre.Tables.Count > 0)
                {
                    //dsCostCentre.Tables.Remove(dsCostCentre.Tables[dsCostCentre.Tables.IndexOf(LedgerId.ToString())]);
                }
            }
        }

        private void ClearDonorDetails()
        {
            glkpPurpose.Text = glkpReceiptType.Text = txtActualAmount.Text = txtCurrencyAmount.Text = string.Empty;
            lblCalculatedAmt.Text = "0.00";
            txtExchangeRate.Text = "1.00";
            glkpCurrencyCountry.EditValue = null;
            glkpDonor.EditValue = null;
        }

        private DataTable GetCurrentBalance(DataTable dtVoucher)//, bool isCurrentBalance)
        {
            int LedgerId = 0;
            foreach (DataRow dr in dtVoucher.Rows)
            {
                LedgerId = this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                if (LedgerId > 0)
                {
                    string Balance = GetLedgerBalanceValues(dtVoucher, LedgerId); //ShowLedgerBalance(LedgerId, dtVoucher, isTransGrid);
                    if (Balance != string.Empty)
                    {
                        dr["LEDGER_BALANCE"] = Balance;
                    }
                }
            }
            return dtVoucher;
        }

        private void ShowProjectSelectionWindow()
        {
            frmProjectSelection projectSelection = new frmProjectSelection(Bosco.Utility.ProjectSelection.DisableVoucherSelectionMethod);
            projectSelection.ShowDialog();
            if (projectSelection.DialogResult == DialogResult.OK)
            {
                if (projectSelection.ProjectName != string.Empty)
                {
                    //28/01/2019, to reset voucher definition type when project selection
                    if (ProjectId != projectSelection.ProjectId)
                    {
                        VoucherDefinitionId = rgTransactionType.SelectedIndex + 1;
                    }
                    ProjectId = projectSelection.ProjectId;
                    ProjectName = projectSelection.ProjectName;
                    LoadTransBasic();
                    LoadSelectedLedger();

                    //On 13/08/2024, Apply Project Currency Setting
                    /*this.ApplyProjectCurrencySetting(ProjectId);
                    rtxtAmount.Mask.Culture = System.Threading.Thread.CurrentThread.CurrentUICulture;
                    rtxtAmount.Mask.UseMaskAsDisplayFormat = true;
                    rtxtCashAmount.Mask.Culture = System.Threading.Thread.CurrentThread.CurrentUICulture;
                    rtxtCashAmount.Mask.UseMaskAsDisplayFormat = true;*/
                }
            }
        }

        private void ShowVoucherType(DefaultVoucherTypes transType)
        {
            if (rgTransactionType.Enabled)
            {
                rgTransactionType.SelectedIndex = (int)transType - 1;

                //On 28/01/2019, show list of voucher types ---------------------------------------------------------------------------------------------
                //this list will be shown only when more than one voucher type exists except base vouchers for selected project
                //VoucherDefinitionId = (int)transType; //by default 
                ResultArgs result = this.ShowVoucherTypeSelection(projectId, ((int)transType).ToString(), (voucherId > 0 ? EditVoucherDefinitionid : VoucherDefinitionId));
                if (result.Success && result.ReturnValue != null)
                {
                    string[] VoucherTypeSelected = result.ReturnValue as string[];
                    VoucherDefinitionId = UtilityMember.NumberSet.ToInteger(VoucherTypeSelected[0]);
                    Int32 baseVoucherType = UtilityMember.NumberSet.ToInteger(VoucherTypeSelected[1]);
                    rgTransactionType.SelectedIndex = baseVoucherType - 1;
                }
                else if (EditVoucherIndex > 0 && EditVoucherIndex == (int)transType - 1)
                {
                    VoucherDefinitionId = EditVoucherDefinitionid;
                }
                else if (rgTransactionType.SelectedIndex != (int)transType - 1)
                {
                    VoucherDefinitionId = (int)transType;
                    rgTransactionType.SelectedIndex = (int)transType - 1;
                }
                rgTransactionType_SelectedIndexChanged(rgTransactionType, null);
                //----------------------------------------------------------------------------------------------------------------------------------------------
            }
        }

        private void ShowLedgerForm()
        {
            //frmLedgerBankAccountAdd frmLedger = new frmLedgerBankAccountAdd((int)AddNewRow.NewRow, ledgerSubType.GN, ProjectId);
            //frmLedger.ShowDialog();
            if (this.AppSetting.LockMasters == (int)YesNo.No)
            {
                frmLedgerDetailAdd frmLedger = new frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.GN, ProjectId);
                frmLedger.ShowDialog();
                //if (frmLedger.DialogResult == DialogResult.OK)
                //{
                //    LoadSelectedLedger();
                //}
                LoadSelectedLedger();
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
            }
        }

        private void ShowCostCentreForm()
        {
            frmCostCentreAdd frmCostCentre = new frmCostCentreAdd((int)AddNewRow.NewRow, ProjectId);
            frmCostCentre.ShowDialog();
            if (frmCostCentre.DialogResult == DialogResult.OK)
            {

            }
        }

        private void ShowDonorForm()
        {
            frmDonorAdd frmDonor = new frmDonorAdd(ViewDetails.Donor, (int)AddNewRow.NewRow, ProjectId);
            frmDonor.ShowDialog();
            //if (frmDonor.DialogResult == DialogResult.OK)
            //{
            //    LoadDonorDetails();
            //}
            LoadDonorDetails();
        }

        private void ShowLedgerOptionsForm()
        {
            frmLedgerOptions frmledgeroptions = new frmLedgerOptions();
            frmledgeroptions.ShowDialog();
            //if (frmledgeroptions.DialogResult == DialogResult.OK)
            //{
            //    LoadTransBasic();
            //}
            // Commanded after closing the Ledger Option. it has to be checked later. 12.07.2019
            // AssignValues();
        }

        private void ShowBankAccountForm()
        {
            frmLedgerDetailAdd frmbankaccount = new frmLedgerDetailAdd(ledgerSubType.BK, ProjectId);
            frmbankaccount.ShowDialog();
            LoadSelectedLedger();
        }

        private void EnableDonorFields()
        {
            glkpPurpose.Enabled = true;
            glkpReceiptType.Enabled = true;


            /*
            txtActualAmount.Enabled = true;
            txtCurrencyAmount.Enabled = true;
            txtExchangeRate.Enabled = true;
            lkpCountry.Enabled = true;*/

        }

        private void DisableDonorFields()
        {
            glkpPurpose.Enabled = false;
            glkpReceiptType.Enabled = false;

            /*txtActualAmount.Enabled = false;
            txtCurrencyAmount.Enabled = false;
            txtExchangeRate.Enabled = false;
            lkpCountry.Enabled = false;*/

            if (rgTransactionType.SelectedIndex == 1)
            {
                glkpDonor.Enabled = false;
                glkpPurpose.Enabled = true;
                glkpPurpose.Focus();
            }
            else
            {
                glkpDonor.Enabled = true;
                glkpDonor.Focus();
            }
        }

        private void ClearDonorFileds()
        {
            glkpDonor.Text = string.Empty;
            glkpReceiptType.Text = string.Empty;
            txtActualAmount.Text = string.Empty;
            txtCurrencyAmount.Text = string.Empty;
            txtExchangeRate.Text = "1";
            glkpCurrencyCountry.Text = string.Empty;
        }

        private void FocusTransactionGrid()
        {
            gcTransaction.Focus();
            gvTransaction.MoveFirst(); //DevExpress.XtraGrid.GridControl.NewItemRowHandle;
            gvTransaction.FocusedColumn = gvTransaction.Columns.ColumnByName(colLedger.Name);
            gvTransaction.ShowEditor();
        }

        private void FocusCashTransactionGrid()
        {
            gcBank.Select();
            gvBank.MoveFirst();
            gvBank.FocusedColumn = colCashLedger;
            gvBank.ShowEditor();
        }

        private void ShowDonorAdditionalInfo()
        {
            if (rgTransactionType.SelectedIndex == 0 || rgTransactionType.SelectedIndex == 1)
            {
                if (lciDonorInfo.Visibility == LayoutVisibility.Always)
                {
                    //flypnlDonorInfo.HidePopup();
                    lciDonorInfo.Visibility = LayoutVisibility.Never;
                    glkpDonor.Enabled = false;
                }
                else
                {
                    //flypnlDonorInfo.ShowPopup();
                    lciDonorInfo.Visibility = LayoutVisibility.Always;

                    if (rgTransactionType.SelectedIndex == 0)
                    {
                        glkpDonor.Enabled = true;
                        // glkpDonor.Select();
                        glkpDonor.Focus();
                    }
                    else
                    {
                        glkpDonor.Enabled = false;
                        glkpPurpose.Enabled = true;
                        // glkpPurpose.Select();
                        glkpPurpose.Focus();
                    }
                }
                if (TransEntryMethod == VoucherEntryMethod.Single)
                {
                    lciTrans.Height = 52;
                    lciBank.Height = 47;
                }
                else
                {   //On 07/09/2023, to fix bank height when include GST invoice list
                    lciBank.Height = 125;
                }

                //if (this.AppSetting.ShowGSTPan == "1")
                //{
                //    txtPanNo.Enabled = txtGSTNo.Enabled = true;
                //    lciPanNumber.Visibility = lciGSTNumber.Visibility = LayoutVisibility.Always;
                //}
                //else
                //{
                //    txtPanNo.Enabled = txtGSTNo.Enabled = false;
                //    lciPanNumber.Visibility = lciGSTNumber.Visibility = LayoutVisibility.Never;
                //}
            }

            //On 12/09/2024, To Show Currency details 
            if (lciDonorInfo.Visibility == LayoutVisibility.Always || this.AppSetting.AllowMultiCurrency == 1)
            {
                enforceCurrencyDetails(true);
            }
            else
            {
                enforceCurrencyDetails(false);
            }
        }

        private void ShowDonorAdditionalInfoForeignProjects()
        {
            if (rgTransactionType.SelectedIndex == 0 || rgTransactionType.SelectedIndex == 1)
            {
                ClearDonorDetails();
                DisableDonorFields();
                if (lciDonorInfo.Visibility != LayoutVisibility.Always)
                {
                    //flypnlDonorInfo.ShowPopup();
                    lciDonorInfo.Visibility = LayoutVisibility.Always;
                }

                if (TransEntryMethod == VoucherEntryMethod.Single)
                {
                    lciTrans.Height = 52;
                    lciBank.Height = 47;
                }
            }
            else
            {
                if (lciDonorInfo.Visibility == LayoutVisibility.Always)
                {
                    //flypnlDonorInfo.HidePopup();
                    lciDonorInfo.Visibility = LayoutVisibility.Never;
                    glkpDonor.Enabled = false;
                }
            }
        }

        /// <summary>
        /// On 13/10/2017, This method is used to show and reassign bank reference number details
        /// This form will be shown only for bank ledger only
        /// </summary>
        private void ShowBankChequeDDReferenceNumberDetails(bool cashbankgrid)
        {
            string ChequeDDReferenceNo = string.Empty;
            string referencedate = string.Empty;
            string bankname = string.Empty;
            string branch = string.Empty;
            string fundtansfer = string.Empty;
            Int32 LedgerGroupId = 0;

            //On 15/02/2018, Only for Receipt Voucher
            //if ((voucherType == DefaultVoucherTypes.Receipt || voucherType == DefaultVoucherTypes.Payment) && cashbankgrid) //For cash bank ledgers in Receipts and Payments
            if (voucherType == DefaultVoucherTypes.Receipt && cashbankgrid) //For cash bank ledgers in Receipts
            {
                LedgerGroupId = GetLedgerGroupId(CashLedgerId);
                ChequeDDReferenceNo = gvBank.GetFocusedRowCellValue(colCashCheque) != null ? gvBank.GetFocusedRowCellValue(colCashCheque).ToString() : string.Empty;
                referencedate = gvBank.GetFocusedRowCellValue(colChequeRefDate) != null ? gvBank.GetFocusedRowCellValue(colChequeRefDate).ToString() : string.Empty; ;
                bankname = gvBank.GetFocusedRowCellValue(colChequeRefBankName) != null ? gvBank.GetFocusedRowCellValue(colChequeRefBankName).ToString() : string.Empty;
                branch = gvBank.GetFocusedRowCellValue(colChequeRefBranch) != null ? gvBank.GetFocusedRowCellValue(colChequeRefBranch).ToString() : string.Empty;
                fundtansfer = gvBank.GetFocusedRowCellValue(colChequeRefFundTransfer) != null ? gvBank.GetFocusedRowCellValue(colChequeRefFundTransfer).ToString() : string.Empty;
            }
            else if ((voucherType == DefaultVoucherTypes.Payment || voucherType == DefaultVoucherTypes.Contra) && cashbankgrid) // Chinna 21/09/2023 to show ref available transfer mode
            {
                LedgerGroupId = GetLedgerGroupId(CashLedgerId);
                ChequeDDReferenceNo = gvBank.GetFocusedRowCellValue(colCashCheque) != null ? gvBank.GetFocusedRowCellValue(colCashCheque).ToString() : string.Empty;
                referencedate = gvBank.GetFocusedRowCellValue(colChequeRefDate) != null ? gvBank.GetFocusedRowCellValue(colChequeRefDate).ToString() : string.Empty;

                bankname = gvBank.GetFocusedRowCellValue(colChequeRefBankName) != null ? gvBank.GetFocusedRowCellValue(colChequeRefBankName).ToString() : string.Empty;
                branch = gvBank.GetFocusedRowCellValue(colChequeRefBranch) != null ? gvBank.GetFocusedRowCellValue(colChequeRefBranch).ToString() : string.Empty;

                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    ledgersystem.LedgerId = CashLedgerId;
                    resultArgs = ledgersystem.FetchBankBranchById();

                    if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        bankname = resultArgs.DataSource.Table.Rows[0]["BANK"] != null ? resultArgs.DataSource.Table.Rows[0]["BANK"].ToString() : string.Empty;
                        branch = resultArgs.DataSource.Table.Rows[0]["BRANCH"] != null ? resultArgs.DataSource.Table.Rows[0]["BRANCH"].ToString() : string.Empty;
                    }
                }

                fundtansfer = gvBank.GetFocusedRowCellValue(colChequeRefFundTransfer) != null ? gvBank.GetFocusedRowCellValue(colChequeRefFundTransfer).ToString() : string.Empty;
            }
            else if (rgTransactionType.SelectedIndex == 2 && cashbankgrid == false) //For contra // it was commanded, i removed 21/09/2023 - chinna
            {
                LedgerGroupId = GetLedgerGroupId(LedgerId);
                ChequeDDReferenceNo = gvTransaction.GetFocusedRowCellValue(colCheque) != null ? gvTransaction.GetFocusedRowCellValue(colCheque).ToString() : string.Empty;
                referencedate = gvTransaction.GetFocusedRowCellValue(colTransChequeRefDate) != null ? gvTransaction.GetFocusedRowCellValue(colTransChequeRefDate).ToString() : string.Empty; ;
                bankname = gvTransaction.GetFocusedRowCellValue(colTransChequeRefBankName) != null ? gvTransaction.GetFocusedRowCellValue(colTransChequeRefBankName).ToString() : string.Empty;
                branch = gvTransaction.GetFocusedRowCellValue(colTransChequeRefBranch) != null ? gvTransaction.GetFocusedRowCellValue(colTransChequeRefBranch).ToString() : string.Empty;

                // 
                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    ledgersystem.LedgerId = LedgerId;  // CashLedgerId; 29/09/2023
                    resultArgs = ledgersystem.FetchBankBranchById();

                    if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        bankname = resultArgs.DataSource.Table.Rows[0]["BANK"] != null ? resultArgs.DataSource.Table.Rows[0]["BANK"].ToString() : string.Empty;
                        branch = resultArgs.DataSource.Table.Rows[0]["BRANCH"] != null ? resultArgs.DataSource.Table.Rows[0]["BRANCH"].ToString() : string.Empty;
                    }
                }

                fundtansfer = gvBank.GetFocusedRowCellValue(colChequeRefFundTransfer) != null ? gvBank.GetFocusedRowCellValue(colChequeRefFundTransfer).ToString() : string.Empty;
            }

            if (cashbankgrid)
            {
                gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colChequeRefDate, string.Empty);
                gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colChequeRefBankName, string.Empty);
                gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colChequeRefBranch, string.Empty);
                gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colChequeRefFundTransfer, string.Empty);
            }
            else
            {
                gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colTransChequeRefDate, string.Empty);
                gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colTransChequeRefBankName, string.Empty);
                gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colTransChequeRefBranch, string.Empty);
                gvTransaction.SetRowCellValue(gvBank.FocusedRowHandle, colTransChequeRefBranch, string.Empty);
            }

            if (!string.IsNullOrEmpty(ChequeDDReferenceNo) && LedgerGroupId == (int)FixedLedgerGroup.BankAccounts)
            {
                string[] bankreferencedetails = new string[4];
                DateTime vdate = this.UtilityMember.DateSet.ToDate(dtTransactionDate.Text, false);

                frmVoucherBankReferenceDetails voucherbankreferencedetails =
                        new frmVoucherBankReferenceDetails(ChequeDDReferenceNo, vdate, referencedate, bankname, branch, fundtansfer);
                if (voucherbankreferencedetails.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    bankreferencedetails = voucherbankreferencedetails.ReturnValue as string[];
                    if (bankreferencedetails.GetUpperBound(0) > 0)
                    {
                        string chequedate = bankreferencedetails.GetValue(0).ToString().Trim();
                        if (cashbankgrid)
                        {
                            gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colChequeRefDate, (string.IsNullOrEmpty(chequedate) ? null : chequedate));
                            gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colChequeRefBankName, bankreferencedetails.GetValue(1).ToString().Trim());
                            gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colChequeRefBranch, bankreferencedetails.GetValue(2).ToString().Trim());
                            gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colChequeRefFundTransfer, bankreferencedetails.GetValue(3).ToString().Trim());
                        }
                        else
                        {
                            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colTransChequeRefDate, (string.IsNullOrEmpty(chequedate) ? null : chequedate));
                            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colTransChequeRefBankName, bankreferencedetails.GetValue(1).ToString().Trim());
                            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colTransChequeRefBranch, bankreferencedetails.GetValue(2).ToString().Trim());
                            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colTransChequeRefFundTransfer, bankreferencedetails.GetValue(3).ToString().Trim());
                        }
                    }
                }
            }

            ShowExchangeRateColumns();
        }



        private void PrintVoucher(int vid)
        {
            if (XtraMessageBox.Show(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.SAVED_PRINT_VOUCHER), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                Application.DoEvents();
                using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
                {
                    Bosco.Report.Base.IReport report = new Bosco.Report.Base.ReportEntry(this.MdiParent);
                    int rid = rgTransactionType.SelectedIndex;
                    string rptVoucher = rid == 0 ? UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKRECEIPTS) : rid == 1 ? UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKPAYMENTS) : UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKCONTRA);
                    resultArgs = voucherSystem.FetchReportSetting(rptVoucher);
                    if (resultArgs != null && resultArgs.Success)
                    {
                        ReportProperty.Current.VoucherPrintSettingInfo = resultArgs.DataSource.TableView;
                        ReportProperty.Current.CashBankVoucherDateFrom = ReportProperty.Current.CashBankVoucherDateTo = this.UtilityMember.DateSet.ToDate(dtTransactionDate.Text, false);
                        report.VoucherPrint(vid.ToString(), rptVoucher, ProjectName, ProjectId);
                    }
                    else
                    {
                        this.ShowMessageBoxError(resultArgs.Message);
                    }
                }
            }
        }



        /// <summary>
        /// This method is used to print cheque
        ///  Based on user confirmation and payment voucher only
        /// </summary>
        /// <param name="vid"></param>
        private void PrintCheque(int vid, DataTable dtVoucherTrans, DataTable dtCashBankVoucherTrans)
        {
            try
            {
                //Cheque print for Payment, Contra vouchers (Withdrwan and Transfer)
                if ((rgTransactionType.SelectedIndex == 1 || rgTransactionType.SelectedIndex == 2) && this.AppSetting.EnableChequePrinting == "1")
                {
                    //Only for bank payment voucher and contra voucher (withdrawn, Transfer)
                    /// 1. For all Bank Payment Voucher
                    /// 2. For all Contra Voucher (Withdraw and Transfer)
                    if (dtVoucherTrans.Rows.Count > 0 && dtCashBankVoucherTrans.Rows.Count > 0)
                    {
                        Int32 firstTransLedgerGrpId = GetLedgerGroupId(UtilityMember.NumberSet.ToInteger(dtVoucherTrans.Rows[0]["LEDGER_ID"].ToString()));
                        Int32 firstCashBankTransLedgerGrpId = GetLedgerGroupId(UtilityMember.NumberSet.ToInteger(dtCashBankVoucherTrans.Rows[0]["LEDGER_ID"].ToString()));
                        if ((rgTransactionType.SelectedIndex == 1 && firstCashBankTransLedgerGrpId == 12) || (rgTransactionType.SelectedIndex == 2 && firstTransLedgerGrpId == 12))
                        {
                            //if (this.ShowConfirmationMessage("Do you want to Print Cheque?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.PRINT_CHQUE), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                            {
                                Bosco.Report.Base.IReport report = new Bosco.Report.Base.ReportEntry(this.MdiParent);
                                report.ShowChequePrint(vid);
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                this.ShowMessageBox(err.Message);
            }
        }

        private string GetCurBalance(int LedId, double OldValue, double NewValue)
        {
            bool isCashBank = false;
            string Mode = string.Empty;
            double CurBal = 0.00;
            double dCalculateCurBal = 0.00;

            CurrentLedgerTransMode = "";

            BalanceProperty Balance = FetchCurrentBalance(LedId);
            isCashBank = (Balance.GroupId == (Int32)FixedLedgerGroup.Cash || Balance.GroupId == (Int32)FixedLedgerGroup.BankAccounts);

            if (!isCashBank && this.AppSetting.AllowMultiCurrency == 1)
            {
                NewValue = NewValue * UtilityMember.NumberSet.ToDouble(txtExchangeRate.Text);
                OldValue = OldValue * UtilityMember.NumberSet.ToDouble(txtExchangeRate.Text);
            }

            if (Balance.TransMode == TransactionMode.CR.ToString())
            {
                CurBal = -((isCashBank ? Balance.AmountFC : Balance.Amount));
            }
            else
            {
                CurBal = ((isCashBank ? Balance.AmountFC : Balance.Amount)); ;
            }

            dCalculateCurBal = CurBal - ((OldValue) + NewValue);

            if (dCalculateCurBal < 0)
            {
                Mode = TransactionMode.CR.ToString();
            }
            else
            {
                Mode = TransactionMode.DR.ToString();
            }

            CurrentLedgerTransMode = Mode; // To be used in Budget Calculation
            string rtn = this.UtilityMember.NumberSet.ToCurrency(Math.Abs(dCalculateCurBal)) + " " + Mode;

            //On 24/08/2024, To check Cash and Bank Ledger -------------------------------------------------------------------------------------
            //On 26/08/2024, If multi currency enabled, let show both currency balance 
            if (this.AppSetting.AllowMultiCurrency == 1 && isCashBank)
            {
                rtn = Balance.CurrencySymbol + " " + this.UtilityMember.NumberSet.ToNumber(Math.Abs(dCalculateCurBal)) + " " + Mode;

                /*double amountinlc = dCalculateCurBal * UtilityMember.NumberSet.ToDouble(txtExchangeRate.Text);
                string bankcashcurrencysymbol = "$";
                rtn = bankcashcurrencysymbol + " " + this.UtilityMember.NumberSet.ToNumber(Math.Abs(dCalculateCurBal)) + " " + Balance.TransMode + "  "
                            + this.UtilityMember.NumberSet.ToCurrency(Math.Abs(amountinlc)) + " " + Balance.TransMode;*/
            }
            //--------------------------------------------------------------------------------------------------------------------------------------

            return rtn;
        }

        private BalanceProperty FetchCurrentBalance(int LedgerId)
        {
            BalanceProperty balProperty;
            using (BalanceSystem balancesystem = new BalanceSystem())
            {
                balProperty = balancesystem.GetBalance(ProjectId, LedgerId, "", BalanceSystem.BalanceType.CurrentBalance);
            }
            return balProperty;
        }


        /// <summary>
        /// By Aldrin to fetch budget ledger balance from the budget period.
        /// </summary>
        /// <param name="LedgerId"></param>
        /// <returns></returns>
        private BalanceProperty FetchBudgetLedgerBalance(int LedgerId)
        {
            BalanceProperty balProperty;
            using (BalanceSystem balancesystem = new BalanceSystem())
            {
                //Command by chinna
                //  balProperty = balancesystem.GetBudgetLedgereBalance(BudgetProjectIds, LedgerId, budgetDateFrom, budgetDateTo);
                string TransMode = GetTransMode();

                // Chinna to filter the Actual Balance in the month itself (17.12.2019)
                if (BudgetMonthDistribution == 1)
                {
                    DateTime Date = Convert.ToDateTime(dtTransactionDate.DateTime);

                    budgetDateFrom = new DateTime(this.UtilityMember.DateSet.ToDate(Date.ToString(), false).Year, this.UtilityMember.DateSet.ToDate(Date.ToString(), false).Month, 01);
                    budgetDateTo = budgetDateFrom.AddMonths(1).AddDays(-1);
                }

                if (!this.AppSetting.IS_CMF_SLA)

                    balProperty = balancesystem.GetBudgetLedgereBalanceByTransMode(BudgetProjectIds, LedgerId, budgetDateFrom, budgetDateTo, TransMode);
                else

                    //balProperty = balancesystem.GetBudgetLedgereBalanceByTransMode(ProjectId.ToString(), LedgerId, UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false), UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false), TransMode);
                    balProperty = balancesystem.GetBudgetLedgereBalance(ProjectId.ToString(), LedgerId, UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false), UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false));    //checked in Updater (10.06.2021)
                // balProperty = balancesystem.GetBudgetLedgereBalanceByTransMode(ProjectId.ToString(), LedgerId, UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false), UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false), TransMode);


            }
            return balProperty;
        }

        private BalanceProperty FetchLedgerOPBalance(int LedgerId)
        {
            BalanceProperty balProperty;
            using (BalanceSystem balancesystem = new BalanceSystem())
            {
                balProperty = balancesystem.GetBalance(ProjectId, LedgerId, BudgetPeriodDateFrom.ToShortDateString(), BalanceSystem.BalanceType.OpeningBalance);
            }
            return balProperty;
        }

        private BalanceProperty FetchClosingBalance(int LedgerId)
        {
            BalanceProperty balancePropery;
            using (BalanceSystem balanceSystem = new BalanceSystem())
            {
                balancePropery = balanceSystem.GetBalance(ProjectId, LedgerId, dtTransactionDate.Text, BalanceSystem.BalanceType.ClosingBalance);
            }
            return balancePropery;
        }


        private void EnableBankFields()
        {
            int iLedgerId = 0;
            int Group = 0;
            DataTable dtTrans = gcTransaction.DataSource as DataTable;

            foreach (DataRow dr in dtTrans.Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    iLedgerId = this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                    using (LedgerSystem ledger = new LedgerSystem())
                    {
                        ledger.LedgerId = iLedgerId;
                        Group = ledger.FetchLedgerGroupById();
                        if (Group == (int)FixedLedgerGroup.BankAccounts)
                        {
                            VisibleTransBankAdditionalFields(true);
                            break;
                        }
                        else
                        {
                            VisibleTransBankAdditionalFields(false);
                            gvTransaction.SetRowCellValue(gvBank.FocusedRowHandle, colCheque, string.Empty);
                            gvTransaction.SetRowCellValue(gvBank.FocusedRowHandle, colValueDate, null);
                            gvTransaction.SetRowCellValue(gvBank.FocusedRowHandle, colTransChequeRefDate, null);
                            gvTransaction.SetRowCellValue(gvBank.FocusedRowHandle, colTransChequeRefBankName, string.Empty);
                            gvTransaction.SetRowCellValue(gvBank.FocusedRowHandle, colTransChequeRefBranch, string.Empty);
                        }
                    }
                }
            }
        }

        private void EnableCashBankFields()
        {
            int iLedgerId = 0;
            int Group = 0;
            DataTable dtTrans = gcBank.DataSource as DataTable;

            foreach (DataRow dr in dtTrans.Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    iLedgerId = this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                    using (LedgerSystem ledger = new LedgerSystem())
                    {
                        ledger.LedgerId = iLedgerId;
                        Group = ledger.FetchLedgerGroupById();
                        if (Group == (int)FixedLedgerGroup.BankAccounts)
                        {
                            VisibleCashBankAdditionalFields(true);
                            break;
                        }
                        else
                        {
                            VisibleCashBankAdditionalFields(false);
                            //On 17/10/2017, when bank ledger is changed to cash ledger clear (chequeno and its details)
                            gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCashCheque, string.Empty);
                            gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colMaterializedOn, null);
                            gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colChequeRefDate, null);
                            gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colChequeRefBankName, string.Empty);
                            gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colChequeRefBranch, string.Empty);
                            gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colChequeRefFundTransfer, string.Empty);
                        }
                    }
                }
            }
        }

        private bool IsValidTransGrid()
        {
            DataTable dtTrans = gcTransaction.DataSource as DataTable;
            int GroupId = 0;
            int Id = 0;
            decimal Amt = 0;
            decimal ExchangeAmt = 0;
            decimal LiveExchangeAmt = 0;
            int Source = 0;
            int RowPosition = 0;
            bool isValid = false;
            DateTime dtClosedDate = DateTime.MinValue;
            string validateMessage = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.REQUIRED_INFORMATION_NOT_FILLED);

            //"((LEDGER_ID=0 OR LEDGER_ID IS NULL)  OR (AMOUNT<=0 OR AMOUNT IS Null)) AND (NOT((LEDGER_ID=0 OR LEDGER_ID IS NULL)  AND (AMOUNT<=0 OR AMOUNT IS Null)))";
            DataView dv = new DataView(dtTrans);
            dv.RowFilter = "(LEDGER_ID>0 OR AMOUNT>0)";
            gvTransaction.FocusedColumn = colLedger;
            if (dv.Count > 0)
            {
                isValid = true;
                foreach (DataRowView drTrans in dv)
                {
                    Id = this.UtilityMember.NumberSet.ToInteger(drTrans["LEDGER_ID"].ToString());
                    Amt = this.UtilityMember.NumberSet.ToDecimal(drTrans["AMOUNT"].ToString());
                    ExchangeAmt = this.UtilityMember.NumberSet.ToDecimal(drTrans["EXCHANGE_RATE"].ToString());
                    LiveExchangeAmt = this.UtilityMember.NumberSet.ToDecimal(drTrans["LIVE_EXCHANGE_RATE"].ToString());
                    Source = this.UtilityMember.NumberSet.ToInteger(drTrans["SOURCE"].ToString());
                    GroupId = GetLedgerGroupId(Id);

                    //Check Ledger Closed Date
                    dtClosedDate = DateTime.MinValue;
                    if (GroupId == (Int32)FixedLedgerGroup.BankAccounts)
                    {
                        dtClosedDate = GetBankLedgerClosedDateById(Id);
                    }
                    else
                    {
                        dtClosedDate = GetLedgerClosedDateById(Id);
                    }

                    //if ((Id == 0 || Amt == 0 || Source == 0)) //&& !(Id == 0 && Amt == 0))
                    if ((Id == 0 || Amt == 0) || (!(dtClosedDate >= dtTransactionDate.DateTime)) && (!(dtClosedDate == DateTime.MinValue)))
                    {
                        if (Id == 0)
                        {
                            validateMessage = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.REQUIRED_INFORMATION_LEDGER_EMPTY);
                            gvTransaction.FocusedColumn = colLedger;
                        }
                        if (Amt == 0)
                        {
                            validateMessage = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.REQUIRED_INFORMATION_AMOUNT_EMPTY);
                            gvTransaction.FocusedColumn = colAmount;
                        }
                        if (Source == 0)
                        {
                            validateMessage = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.REQUIRED_INFORMATION_TRANSMODE_EMPTY);
                            gvTransaction.FocusedColumn = colSource;
                        }
                        isValid = false;
                        break;
                    }

                    //On 12/11/2024, to check exchange rate
                    if (isValid && this.AppSetting.AllowMultiCurrency == 1)
                    {
                        if (ExchangeAmt != UtilityMember.NumberSet.ToDecimal(txtExchangeRate.Text))
                        {
                            validateMessage = "Mismatching Voucher Exchange Rate with Ledger Exchange Rate.";
                            gvTransaction.FocusedColumn = colAmount;
                            isValid = false;
                            break;
                        }
                    }

                    RowPosition = RowPosition + 1;
                }
            }

            if (!isValid)
            {
                this.ShowMessageBox(validateMessage);
                gvTransaction.CloseEditor();
                gvTransaction.FocusedRowHandle = gvTransaction.GetRowHandle(RowPosition);
                gvTransaction.ShowEditor();
            }

            return isValid;
        }

        private bool IsValidSource()
        {
            bool isValid = true;
            if (rgTransactionType.SelectedIndex != 2)
            {
                DataTable dtTrans = gcTransaction.DataSource as DataTable;
                DataView dv = new DataView(dtTrans);
                dv.RowFilter = "(LEDGER_ID>0 OR AMOUNT>0)";

                if (dv.Count > 0)
                {
                    double dAmt = GetTransSummaryAmount();
                    //On if general ledger has only one ledger that too zero values, let us altert message
                    //if (dAmt <= 0) 
                    if (dAmt <= 0 && (!AllowZeroValuedCashBankLedger || dtTrans.Rows.Count <= 1))
                    {
                        if (rgTransactionType.SelectedIndex == 0)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.CASHBANK_MUST_DEBITED));
                        }
                        else
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.CASHBANK_MUST_CREDITED));
                        }
                        isValid = false;
                    }
                }
            }

            if (!isValid) { FocusTransactionGrid(); }

            return isValid;
        }

        /// <summary>
        /// On 11/04/2022, to validate and prompt proper message for sdbinm or locking receipt module 
        /// </summary>
        /// <returns></returns>
        private bool ValidateReceiptModuleRights()
        {
            bool rtn = true;
            string msg = string.Empty;
            try
            {
                if (this.AppSetting.IS_SDB_INM && (UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false) >= UtilityMember.DateSet.ToDate(SettingProperty.Enforce_Receipt_Module_FY, false)) &&
                    dtTransactionDate.DateTime >= UtilityMember.DateSet.ToDate(SettingProperty.Enforce_Receipt_Module_FY, false))
                {
                    if (VoucherType == DefaultVoucherTypes.Receipt && !AppSetting.ENABLE_TRACK_RECEIPT_MODULE)
                    {
                        rtn = false;
                        //As per the Province regulation, Receipt Module is locked, You can't make Receipt Voucher.";
                        msg = MessageCatalog.Common.COMMON_RECEIPT_DISABLED_VOUCHER_ENTRY;
                    }
                    else if (VoucherType == DefaultVoucherTypes.Receipt && AppSetting.ENABLE_TRACK_RECEIPT_MODULE && VoucherId > 0)
                    {
                        //If We locked date and project for receipt module for sdbinm, even though if it is changed, let us prompt message
                        //bool primepropertyChanged = transEntryMethod;
                        if (TrackVoucherAmount != CashTransSummaryVal)
                        {
                            //"As per the Province regulation, Receipt Module is being tracked, You can't change Receipt Voucher Amount";
                            msg = MessageCatalog.Common.COMMON_RECEIPT_DISABLED_VOUCHER_AMOUNT;
                            rtn = false;
                        }
                        else if (TrackVoucherType != VoucherType && TrackVoucherDate != dtTransactionDate.DateTime &&
                            TrackVoucherProjectId != ProjectId && TrackVoucherSubType != VoucherSubTypes.GN.ToString())
                        {
                            //"As per the Province regulation, Receipt Module is being tracked, You can't change Voucher Date, Project in Receipt Voucher";
                            msg = MessageCatalog.Common.COMMON_RECEIPT_DISABLED_CHANGE_DATEPROJECT;
                            rtn = false;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                this.ShowMessageBox(err.Message);
                rtn = false;
            }
            finally
            {
                if (this.AppSetting.IS_SDB_INM && !rtn && !string.IsNullOrEmpty(msg))
                {
                    this.ShowMessageBox(msg);
                }
                else
                {
                    rtn = true;
                }
            }

            return rtn;
        }

        /// <summary>
        /// Check the Gst Calculated Amount should be equal to Trans Amt with GST Calculation (09.05.2019)
        /// </summary>
        /// <returns></returns>
        private bool IsValidateTransCashEqualAmountGST()
        {
            bool isValid = true;
            try
            {
                DataTable dtTransaction = gcTransaction.DataSource as DataTable;
                dtTransaction.AcceptChanges();
                if (dtTransaction != null && dtTransaction.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtTransaction.Rows)
                    {

                        int rownumber = dtTransaction.Rows.IndexOf(dr);
                        Int32 ledgerid = dr["LEDGER_ID"].ToString().Equals(string.Empty) ? 0 : UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                        //1. Take leger details in transaction grid
                        if (ledgerid > 0)
                        {
                            //int IsGSTExistLedger = IsGSTLedgers(ledgerid); //02/12/2019

                            if (IsgstEnabledLedgers && colGStAmt.Visible) //02/12/2019
                            {
                                if ((TransGStSummaryVal != CashTransSummaryVal))
                                {
                                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_AMOUNT_MISMATCH));
                                    isValid = false;
                                    gcBank.Select();
                                    gcBank.Focus();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                isValid = false;
                MessageRender.ShowMessage(err.Message);
            }
            return isValid;
        }

        /// <summary>
        /// Validate Trans Grid Amount equal to Cash\Bank Amount without GST (09.05.2019)
        /// </summary>
        /// <returns></returns>
        private bool IsValidateTransCashEqualAmount()
        {
            bool isValid = true;
            try
            {
                double GSTcolValue = UtilityMember.NumberSet.ToDouble(colGStAmt.SummaryItem.SummaryValue.ToString());
                //GST not Enabled (General)
                //GST Enabled and no GST for ledger
                if (!colGStAmt.Visible || GSTcolValue == 0)
                {
                    //On 23/11/2024, for multi currency contra, let us allow different amount for differnt currency ledgers
                    //TransSummaryVal != CashTransSummaryVal
                    if ((TransSummaryVal != CashTransSummaryVal) && (this.AppSetting.AllowMultiCurrency == 0 || rgTransactionType.SelectedIndex != 2))
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_AMOUNT_MISMATCH));
                        isValid = false;
                        gcBank.Select();
                        gcBank.Focus();
                        //GetGStCalcAmt();
                    }
                }
            }
            catch (Exception err)
            {
                isValid = false;
                MessageRender.ShowMessage(err.Message);
            }
            return isValid;
        }

        /// <summary>
        /// Included Bank Closed Details and Validating Transaction Date is 
        /// Less than the Closed Date. 
        /// </summary>
        /// <returns></returns>
        private bool IsValidCashTransGrid()
        {
            DataTable dtCashTrans = gcBank.DataSource as DataTable;
            int GroupId = 0;
            int Id = 0;
            decimal Amt = 0;
            decimal ExchangeAmt = 0;
            double TransCashAmount = 0;
            int RowPosition = 0;
            bool isValid = false;
            DateTime dtClosedDate = DateTime.MinValue;
            string validateMessage = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.REQUIRED_INFORMATION_NOT_FILLED);

            DataView dv = new DataView(dtCashTrans);
            dv.RowFilter = "(LEDGER_ID>0 OR AMOUNT>0)";
            gvBank.FocusedColumn = colCashLedger;

            if (dv.Count > 0)
            {
                isValid = true;
                foreach (DataRowView drTrans in dv)
                {
                    Id = this.UtilityMember.NumberSet.ToInteger(drTrans["LEDGER_ID"].ToString());
                    Amt = this.UtilityMember.NumberSet.ToDecimal(drTrans["AMOUNT"].ToString());
                    if (this.AppSetting.AllowMultiCurrency == 1) ExchangeAmt = this.UtilityMember.NumberSet.ToDecimal(drTrans["EXCHANGE_RATE"].ToString());
                    GroupId = GetLedgerGroupId(Id);

                    //On 23/03/2019, To check and alter it Cash maximum amount exceeds based on setting
                    if (GroupId == (Int32)FixedLedgerGroup.Cash)
                    {
                        TransCashAmount += this.UtilityMember.NumberSet.ToDouble(Amt.ToString());
                    }

                    dtClosedDate = DateTime.MinValue;
                    if (GroupId == (Int32)FixedLedgerGroup.BankAccounts)
                    {
                        dtClosedDate = GetBankLedgerClosedDateById(Id);
                    }
                    else
                    {
                        dtClosedDate = GetLedgerClosedDateById(Id);
                    }

                    if ((Id == 0 || (Amt == 0 && !AllowZeroValuedCashBankLedger)) || (!(dtClosedDate >= dtTransactionDate.DateTime)) && (!(dtClosedDate == DateTime.MinValue))) // && !(Id == 0 && Amt == 0)) // && !(Id == 0 && Amt == 0))
                    {
                        if (Id == 0)
                        {
                            validateMessage = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.CASHBANK_LEDGER_EMPTY);
                            gvBank.FocusedColumn = colCashLedger;
                        }
                        if (Amt == 0)
                        {
                            validateMessage = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.CASHBANK_AMOUNT_EMPTY);
                            gvBank.FocusedColumn = colCashAmount;
                        }
                        if (!(dtClosedDate >= dtTransactionDate.DateTime) && (!(dtClosedDate == DateTime.MinValue)))
                        {
                            validateMessage = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.CASHBANK_LEDGER_EMPTY);
                            gvBank.FocusedColumn = colCashLedger;
                        }
                        isValid = false;
                        break;
                    }

                    //On 12/11/2024, to check exchange rate
                    if (isValid && this.AppSetting.AllowMultiCurrency == 1)
                    {
                        //On 23/11/2024, for multi currency contra, let us allow different exchange rate for differnt currency ledgers
                        if ((ExchangeAmt != UtilityMember.NumberSet.ToDecimal(txtExchangeRate.Text))
                            && (this.AppSetting.AllowMultiCurrency == 0 || rgTransactionType.SelectedIndex != 2))
                        {
                            validateMessage = "Mismatching Voucher Exchange Rate with Ledger Exchange Rate.";
                            gvBank.FocusedColumn = colCashAmount;
                            isValid = false;
                            break;
                        }
                    }
                    RowPosition = RowPosition + 1;
                }

                //On 23/03/2019, To check and alter it Cash maximum amount exceeds based on setting (only for receipts and contra)
                if (isValid && this.AppSetting.MaxCashLedgerAmountInReceiptsPayments > 0 && TransCashAmount > 0 && (voucherType == DefaultVoucherTypes.Receipt || voucherType == DefaultVoucherTypes.Payment))
                {
                    if (TransCashAmount > this.AppSetting.MaxCashLedgerAmountInReceiptsPayments)
                    {
                        //if (this.ShowConfirmationMessage("Cash Ledger amount exceeds with defined amount in Setting, Do you want to proceed ?",
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.CASH_EXCEEDS),
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            RowPosition = 0;
                            validateMessage = string.Empty;
                            isValid = false;
                        }
                    }
                }
            }

            if (!isValid)
            {
                if (!string.IsNullOrEmpty(validateMessage))
                {
                    this.ShowMessageBox(validateMessage);
                }
                gvBank.CloseEditor();
                gvBank.FocusedRowHandle = gvBank.GetRowHandle(RowPosition);
                gvBank.ShowEditor();
            }
            return isValid;
        }

        /// <summary>
        /// This is to Retern the Bank Closed Date.... 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        private DateTime GetBankLedgerClosedDateById(int Id)
        {
            DateTime DtClosedDate = DateTime.MinValue;
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                DtClosedDate = ledgerSystem.GetBankLedgerClosedDate(Id);
            }
            return DtClosedDate;
        }

        /// <summary>
        /// This is to Retern the Ledger Closed Date.... 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        private DateTime GetLedgerClosedDateById(int Id)
        {
            DateTime DtClosedDate = DateTime.MinValue;
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                DtClosedDate = ledgerSystem.GetLedgerClosedDate(Id);
            }
            return DtClosedDate;
        }


        private bool IsValidCashTransGridNegative()
        {
            bool isValid = true;
            if (AppSetting.EnableNegativeBalance == "1" && rgTransactionType.SelectedIndex == 1)
            {
                DataTable dtCashTrans = gcBank.DataSource as DataTable;
                int RowPosition = 0;
                isValid = false;
                string LedgerBalance = string.Empty;

                DataView dv = new DataView(dtCashTrans);
                dv.RowFilter = "(LEDGER_ID>0 OR AMOUNT>0)";
                gvBank.FocusedColumn = colCashLedger;

                if (dv.Count > 0)
                {
                    isValid = true;
                    foreach (DataRowView drTrans in dv)
                    {
                        LedgerBalance = drTrans["LEDGER_BALANCE"].ToString();
                        if (LedgerBalance.Contains("CR"))
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.CASHBANK_NEGATIVE), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                gvBank.FocusedColumn = colAmount;
                                isValid = false;
                                break;
                            }
                        }

                        RowPosition = RowPosition + 1;
                    }
                }
                if (!isValid)
                {
                    gvBank.CloseEditor();
                    gvBank.FocusedRowHandle = gvBank.GetRowHandle(RowPosition);
                    gvBank.ShowEditor();
                }
            }
            return isValid;
        }

        private string GetTransMode()
        {
            return (rgTransactionType.SelectedIndex == 0
                    || rgTransactionType.SelectedIndex == 2) ? TransactionMode.CR.ToString() : TransactionMode.DR.ToString();
        }

        private string GetCashTransMode()
        {
            return (rgTransactionType.SelectedIndex == 0 ||
              rgTransactionType.SelectedIndex == 2) ? TransactionMode.DR.ToString() : TransactionMode.CR.ToString();
        }

        private string GetLedgerBalanceValues(DataTable dtTrans, int LedgerId)
        {
            string LedgerBalance = string.Empty;
            double OldValue = 0;
            double NewValue = 0;
            string NewValueMode = string.Empty;

            if (dtTrans != null)
            {
                NewValue = GetCalculatedAmount(LedgerId, dtTrans);
                OldValue = GetCalculatedTempAmount(LedgerId, dtTrans);

                LedgerBalance = GetCurBalance(LedgerId, OldValue, NewValue);
            }

            return LedgerBalance;
        }

        private void ShowEntryMethod()
        {
            FocusTransactionGrid();
            TransEntryMethod = (TransEntryMethod == VoucherEntryMethod.Single) ? VoucherEntryMethod.Multi : VoucherEntryMethod.Single;
        }

        private void ProcessShortcutKeys(KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
                if (e.KeyCode == Keys.F3 && dtTransactionDate.Enabled)
                {
                    // dtTransactionDate.Focus();

                    // This Code is designed by Amal
                    frmDatePicker datePicker = new frmDatePicker(dtTransactionDate.DateTime, DatePickerType.VoucherDate);
                    //Added by Carmel Raj on 20-October-2015
                    //Purpose : Set max date property of frmDatePicker Object variable
                    datePicker.deProjectMaxDate = deMaxDate;
                    datePicker.ShowDialog();
                    dtTransactionDate.DateTime = AppSetting.VoucherDate;
                    gcTransaction.Focus();

                    LoadPendingGSTInvoices();
                }
                if (e.KeyCode == Keys.F4 && dtTransactionDate.Enabled)// (Keys.Control | Keys.N)
                {
                    DateTime dtYearTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
                    dtTransactionDate.DateTime = (dtTransactionDate.DateTime < dtYearTo) ? dtTransactionDate.DateTime.AddDays(1) : dtYearTo;
                }
                if (e.KeyCode == Keys.F5 && AppSetting.ENABLE_TRACK_RECEIPT_MODULE)
                {
                    if (VoucherId == 0)
                    {
                        ShowProjectSelectionWindow();
                        if (TransVoucherMethod == (int)TransactionVoucherMethod.Automatic)
                        {
                            LoadVoucherNo();
                        }
                        else if (TransVoucherMethod == (int)TransactionVoucherMethod.Manual)
                        {
                            txtVoucher.Text = string.Empty;
                        }
                    }
                }
                if (e.KeyCode == Keys.F6 && AppSetting.ENABLE_TRACK_RECEIPT_MODULE)
                {
                    if (!this.LoginUser.IsFullRightsReservedUser)
                    {
                        if (VoucherId == 0)
                        {
                            if (CommonMethod.ApplyUserRights((int)Receipt.CreateReceiptVoucher) > 0)
                            {
                                ShowVoucherType(DefaultVoucherTypes.Receipt);
                            }
                        }
                        else
                        {
                            if (CommonMethod.ApplyUserRights((int)Receipt.EditReceiptVoucher) > 0)
                            {
                                ShowVoucherType(DefaultVoucherTypes.Receipt);
                            }
                        }
                    }
                    else
                    {
                        ShowVoucherType(DefaultVoucherTypes.Receipt);
                    }
                }
                if (e.KeyCode == Keys.F7 && AppSetting.ENABLE_TRACK_RECEIPT_MODULE)
                {
                    if (!this.LoginUser.IsFullRightsReservedUser)
                    {
                        if (VoucherId == 0)
                        {
                            if (CommonMethod.ApplyUserRights((int)Payment.CreatePaymentVoucher) > 0)
                            {
                                ShowVoucherType(DefaultVoucherTypes.Payment);
                            }
                        }
                        else
                        {
                            if (CommonMethod.ApplyUserRights((int)Payment.EditPaymentVoucher) > 0)
                            {
                                ShowVoucherType(DefaultVoucherTypes.Payment);
                            }
                        }
                    }
                    else
                    {
                        ShowVoucherType(DefaultVoucherTypes.Payment);
                    }
                }
                if (e.KeyCode == Keys.F8 && AppSetting.ENABLE_TRACK_RECEIPT_MODULE)
                {
                    if (!this.LoginUser.IsFullRightsReservedUser)
                    {
                        if (VoucherId == 0)
                        {
                            if (CommonMethod.ApplyUserRights((int)Contra.CreateContraVoucher) > 0)
                            {
                                ShowVoucherType(DefaultVoucherTypes.Contra);
                            }
                        }
                        else
                        {
                            if (CommonMethod.ApplyUserRights((int)Contra.EditContraVoucher) > 0)
                            {
                                ShowVoucherType(DefaultVoucherTypes.Contra);
                            }
                        }
                    }
                    else
                    {
                        ShowVoucherType(DefaultVoucherTypes.Contra);
                    }
                }
                if (e.KeyCode == Keys.F9)
                {
                    if (!this.LoginUser.IsFullRightsReservedUser)
                    {
                        if (CommonMethod.ApplyUserRights((int)Journal.CreateJournalVoucher) > 0)
                        {
                            LoadJournal();
                        }
                    }
                    else
                    {
                        LoadJournal();
                    }
                }
                if (e.KeyCode == Keys.F10)
                {
                    if (!this.LoginUser.IsFullRightsReservedUser)
                    {
                        if (CommonMethod.ApplyUserRights((int)Ledger.CreateLedger) > 0)
                        {
                            ShowLedgerForm();
                        }
                    }
                    else
                    {
                        ShowLedgerForm();
                    }
                }
                if (e.KeyCode == Keys.F11)
                {
                    ShowEntryMethod();
                }

                if (e.KeyCode == Keys.F12)
                {
                    if (!this.LoginUser.IsFullRightsReservedUser)
                    {
                        if (CommonMethod.ApplyUserRightsForTransaction((int)Menus.MasterSetting) != 0)
                        {
                            frmFinanceSetting setting = new frmFinanceSetting();
                            setting.ShowDialog();
                        }
                    }
                    else
                    {
                        frmFinanceSetting setting = new frmFinanceSetting();
                        setting.ShowDialog();
                    }
                }
                if (e.KeyData == (Keys.Alt | Keys.T))
                {
                    ShowCostCentreForm();
                }
                if (e.KeyData == (Keys.Alt | Keys.R))
                {
                    ShowDonorAdditionalInfo();
                }
                if (e.KeyData == (Keys.Alt | Keys.U))
                {
                    DeleteVoucherById(e);
                }
                if (e.KeyData == (Keys.Alt | Keys.B))
                {
                    ShowBankAccountForm();
                }
                if (e.KeyData == (Keys.Alt | Keys.M))
                {
                    frmMapProjectLedger objMap = new frmMapProjectLedger();
                    objMap.ShowDialog();
                    if (objMap.DialogResult == DialogResult.OK)
                    {
                        LoadSelectedLedger();
                        LoadPurposeDetails();
                        LoadDonorDetails();
                    }
                }
                if (e.KeyData == (Keys.Alt | Keys.O))
                {
                    ShowDonorForm();
                }
                if (e.KeyData == (Keys.Control | Keys.D))
                {
                    DeleteTransaction();
                }
                if (e.KeyData == (Keys.Alt | Keys.D))
                {
                    DeleteCashBankTransaction();
                }
                if (e.KeyData == (Keys.Control | Keys.I))
                {
                    FocusTransactionGrid();
                }
                if (e.KeyData == (Keys.Alt | Keys.I))
                {
                    FocusCashTransactionGrid();
                }
                if (e.KeyData == (Keys.Alt | Keys.N))
                {
                    if (VoucherTypeId == 0 && CommonMethod.ApplyUserRights((int)Receipt.CreateReceiptVoucher) != 0)
                    {

                    }
                    else if (VoucherTypeId == 1)
                    {

                    }
                    else if (VoucherTypeId == 2)
                    {

                    }
                    VoucherId = 0;
                    ClearControls();
                }
                if (e.KeyData == (Keys.Alt | Keys.C))
                {
                    this.Close();
                }
                if (e.KeyData == (Keys.Alt | Keys.V))
                {
                    frmTransactionVoucherView voucview = new frmTransactionVoucherView(projectName, ProjectId, dtTransactionDate.DateTime, false);
                    voucview.ShowDialog();
                }
                if (e.KeyData == (Keys.Alt | Keys.P))
                {
                    ShowLedgerOptionsForm();
                }
                if (e.KeyData == (Keys.Alt | Keys.S))
                {
                    btnSave.Focus();
                    btnSave_Click(null, null);
                }
                if (e.KeyData == (Keys.Control | Keys.R))
                {
                    ShowSubLedgersVouchers();
                }

                if (e.KeyData == (Keys.Alt | Keys.B))
                {
                    //ShowVoucherBills();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        private void AssignDonationLedger()
        {
            if (glkpDonor.EditValue != null && this.UtilityMember.NumberSet.ToInteger(glkpDonor.EditValue.ToString()) > 0)
            {
                gvTransaction.MoveFirst();

                using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
                {
                    voucherSystem.ProjectId = ProjectId;
                    resultArgs = voucherSystem.FetchInKindLedger();

                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Sclar.ToInteger > 0)
                    {
                        if (LedgerId == 0)
                        {
                            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colLedger, resultArgs.DataSource.Sclar.ToInteger);
                        }

                        gvTransaction.PostEditor();
                        gvTransaction.UpdateCurrentRow();
                        DataTable dtTemp = gcTransaction.DataSource as DataTable;
                        if (LedgerId > 0)
                        {
                            string Balance = GetLedgerBalanceValues(dtTemp, LedgerId);
                            if (Balance != string.Empty) { gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colLedgerBal, Balance); }
                        }
                    }
                }
            }
        }

        private void CalculateFirstRowValue()
        {
            if (LedgerAmount >= 0 && CashTransSummaryVal != TransSummaryVal && VoucherId >= 0)
            {
                gvBank.MoveFirst();
                double Amount = gvBank.GetRowCellValue(gvBank.FocusedRowHandle, colCashAmount) != null ?
                    this.UtilityMember.NumberSet.ToDouble(gvBank.GetRowCellValue(gvBank.FocusedRowHandle, colCashAmount).ToString()) : 0;
                if (Amount >= 0)
                {
                    double dAmount = 0.0;
                    double updateGStAmount = 0.0;
                    if (!this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableGST).Equals((int)YesNo.Yes))
                    {
                        if (CashTransSummaryVal <= TransSummaryVal)
                        {
                            dAmount = (TransSummaryVal - CashTransSummaryVal) + Amount;
                        }
                        else if (CashTransSummaryVal >= TransSummaryVal)
                        {
                            dAmount = Amount - (CashTransSummaryVal - TransSummaryVal);
                        }
                    }
                    else
                    {
                        // This is to changes Trans amount eqaul to Cash/Bank Amount (09.05.2019)
                        if (CashLedgerId > 0)
                        {
                            updateGStAmount = UtilityMember.NumberSet.ToDouble(colGStAmt.SummaryItem.SummaryValue.ToString());
                        }
                        dAmount = Amount - (CashTransSummaryVal - TransSummaryVal) + UpdateGST;
                    }

                    if (dAmount >= 0)
                    {
                        //On 23/11/2024 - For Multi Currency Contra, Amount can be mismtached (Ledger Actual Amunt must be same)
                        if (this.AppSetting.AllowMultiCurrency == 0 || rgTransactionType.SelectedIndex != 2)
                        {
                            gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCashAmount, dAmount);
                        }
                        if (this.AppSetting.AllowMultiCurrency == 0 && CashLedgerId == 0 && isCashLedgerExists())
                        {
                            gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCashLedger, 1);
                        }
                        gvBank.PostEditor();
                        gvBank.UpdateCurrentRow();
                        DataTable dtTemp = gcBank.DataSource as DataTable;
                        if (CashLedgerId > 0)
                        {
                            string Balance = GetLedgerBalanceValues(dtTemp, CashLedgerId); //ShowLedgerBalance(CashLedgerId, dtTemp, false);
                            if (Balance != string.Empty) { gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colLedgerBalance, Balance); }
                        }
                    }
                }
            }  // This is Created newly in order to Assign the Changed gst amount with Trans Amount (09.05.2019)
            else if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableGST).Equals((int)YesNo.Yes) && colGStAmt.Visible)
            {
                gvBank.MoveFirst();
                double Amount = gvBank.GetRowCellValue(gvBank.FocusedRowHandle, colCashAmount) != null ?
                    this.UtilityMember.NumberSet.ToDouble(gvBank.GetRowCellValue(gvBank.FocusedRowHandle, colCashAmount).ToString()) : 0;
                if (Amount >= 0)
                {
                    double dAmount = 0.0;
                    double updateGStAmount = 0.0;
                    if (CashLedgerId > 0)
                    {
                        updateGStAmount = UtilityMember.NumberSet.ToDouble(colGStAmt.SummaryItem.SummaryValue.ToString());
                    }
                    dAmount = Amount - (CashTransSummaryVal - TransSummaryVal) + UpdateGST;

                    if (dAmount >= 0)
                    {
                        gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCashAmount, dAmount);
                        if (CashLedgerId == 0 && isCashLedgerExists())
                        {
                            gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCashLedger, 1);
                        }
                        gvBank.PostEditor();
                        gvBank.UpdateCurrentRow();
                        DataTable dtTemp = gcBank.DataSource as DataTable;
                        if (CashLedgerId > 0)
                        {
                            string Balance = GetLedgerBalanceValues(dtTemp, CashLedgerId); //ShowLedgerBalance(CashLedgerId, dtTemp, false);
                            if (Balance != string.Empty) { gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colLedgerBalance, Balance); }
                        }
                    }
                }
            }

            //gvTransaction.UpdateSummary(); //28/12/2019, Refresh gst column summary item
        }

        /// <summary>
        /// // this is to enable add the Cost Centre Amount 12.07.2019
        /// </summary>
        private double getGSTLedger(int LedgerId)
        {
            double LedgerGST = 0.0;
            if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableGST).Equals((int)YesNo.Yes))
            {
                if (dtTransactionDate.DateTime >= this.AppSetting.GSTStartDate)
                {
                    // if (IsValidaTransactionRow())
                    //{
                    int IsGSTExistLedger = IsGSTLedgers(LedgerId);

                    if (IsGSTExistLedger > 0 && LedgerId > 0)
                    {
                        string GSTBalance = CalculateGST();

                        if (GSTBalance != string.Empty)
                        {
                            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colGStAmt, GSTBalance);

                            string SpiltValues = GSTBalance;
                            decimal GSt = 0;
                            decimal SGSt = 0;
                            decimal CGSt = 0;
                            decimal IGSt = 0;
                            if (!string.IsNullOrEmpty(SpiltValues))
                            {
                                string[] values = SpiltValues.Split('(');  // get values[0]
                                GSt = this.UtilityMember.NumberSet.ToDecimal(values[0].ToString());

                                string[] gstPercentages = values[1].Split(')'); // get values1[1]
                                string[] gstPercentagesValues = gstPercentages[0].Split('+');
                                SGSt = this.UtilityMember.NumberSet.ToDecimal(gstPercentagesValues[0].ToString());
                                string[] gstPercentagesCGST = gstPercentagesValues[1].Split('/');
                                CGSt = this.UtilityMember.NumberSet.ToDecimal(gstPercentagesValues[0].ToString());
                            }
                            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colGST, GSt);
                            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colCGST, CGSt);
                            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colSGST, SGSt);
                            if (SGSt == 0 && CGSt == 0)
                                gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colIGST, GSt);

                            // Cash Transaction
                            gvBank.PostEditor();
                            gvBank.UpdateCurrentRow();
                            DataTable dtTemp = gcBank.DataSource as DataTable;
                            if (CashLedgerId > 0)
                            {
                                DataTable dtValue = gcTransaction.DataSource as DataTable;
                                string Balance = GSTTotalAmount(dtValue);
                                if (Balance != string.Empty) { gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCBGSTAmount, Balance); }
                                if (Balance != string.Empty)
                                    LedgerGST = this.UtilityMember.NumberSet.ToDouble(GSt.ToString());
                            }
                        }
                    }
                    else
                    {
                        gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colGStAmt, "  ");
                        gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colGST, 0);
                        gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colCGST, 0);
                        gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colSGST, 0);
                        gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colIGST, 0);

                        //25/04/2019, to reset gst amount for non gst ledgers
                        DataTable dtValue = gcTransaction.DataSource as DataTable;
                        string Balance = GSTTotalAmount(dtValue);
                    }
                    // }
                }
                else
                {
                    DataTable dtVouchers = gcTransaction.DataSource as DataTable;
                    ClearGSTValues(dtVouchers);
                }
            }
            return LedgerGST;
        }

        /// <summary>
        /// GST Option for 10.01.2019
        /// Assign GST values from calculation and assign concern columns
        /// </summary>
        private void AssignGSTAmount(int selectedGSTClass)
        {
            if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableGST).Equals((int)YesNo.Yes))
            {
                if (dtTransactionDate.DateTime >= this.AppSetting.GSTStartDate)
                {
                    //int IsGSTExistLedger = IsGSTLedgers(LedgerId); //02/12/2019
                    LedgerGSTClassId = selectedGSTClass;//FetchGSTLedger(LedgerId); //29/11/2019, To set Ledger GST Ledger Class
                    if (IsValidaTransactionRow())
                    {
                        //02/12/2019
                        //28/12/2019, to check ledger GST applicable date
                        if (LedgerId > 0 && IsgstEnabledLedgers && dtTransactionDate.DateTime >= LedgerGSTClassApplicable)
                        {
                            string GSTBalance = CalculateGST();

                            if (GSTBalance != string.Empty)
                            {
                                gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colGStAmt, GSTBalance);

                                string SpiltValues = GSTBalance;
                                decimal GSt = 0;
                                decimal SGSt = 0;
                                decimal CGSt = 0;
                                decimal IGSt = 0;
                                if (!string.IsNullOrEmpty(SpiltValues))
                                {
                                    string[] values = SpiltValues.Split('(');  // get values[0]
                                    GSt = this.UtilityMember.NumberSet.ToDecimal(values[0].ToString());

                                    string[] gstPercentages = values[1].Split(')'); // get values1[1]
                                    string[] gstPercentagesValues = gstPercentages[0].Split('+');
                                    SGSt = this.UtilityMember.NumberSet.ToDecimal(gstPercentagesValues[0].ToString());
                                    string[] gstPercentagesCGST = gstPercentagesValues[1].Split('/');
                                    CGSt = this.UtilityMember.NumberSet.ToDecimal(gstPercentagesValues[0].ToString());
                                }
                                gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colGST, GSt);
                                gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colCGST, CGSt);
                                gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colSGST, SGSt);
                                if (SGSt == 0 && CGSt == 0)
                                    gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colIGST, GSt);

                                // Cash Transaction
                                gvBank.PostEditor();
                                gvBank.UpdateCurrentRow();
                                DataTable dtTemp = gcBank.DataSource as DataTable;
                                if (CashLedgerId > 0)
                                {
                                    DataTable dtValue = gcTransaction.DataSource as DataTable;
                                    string Balance = GSTTotalAmount(dtValue);
                                    if (Balance != string.Empty) { gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCBGSTAmount, Balance); }
                                }
                            }
                        }
                        else
                        {
                            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colGStAmt, "  ");
                            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colGST, 0);
                            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colCGST, 0);
                            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colSGST, 0);
                            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colIGST, 0);

                            //# 28/12/2019, Reset Ledger GST Class Id
                            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colGSTLedgerClass, this.AppSetting.GSTZeroClassId);


                            //25/04/2019, to reset gst amount for non gst ledgers
                            DataTable dtValue = gcTransaction.DataSource as DataTable;
                            string Balance = GSTTotalAmount(dtValue);
                        }
                    }
                }
                else
                {
                    DataTable dtVouchers = gcTransaction.DataSource as DataTable;
                    ClearGSTValues(dtVouchers);
                }
            }
        }

        private bool isCashLedgerExists()
        {
            bool isValid = true;
            int LedId = 0;
            using (LedgerSystem ledger = new LedgerSystem())
            {
                LedId = ledger.IsCashLedgerExists();
                if (LedId != 1)
                {
                    isValid = false;
                }
            }
            return isValid;
        }

        private double GetTransSummaryAmount()
        {
            double dAmount = 0;
            DataTable dtTrans = gcTransaction.DataSource as DataTable;
            if (dtTrans.Rows.Count > 0)
            {
                double CrAmt = 0;
                double DrAmt = 0;

                CrAmt = this.UtilityMember.NumberSet.ToDouble(dtTrans.Compute("SUM(AMOUNT)", "SOURCE=" + (int)Source.To).ToString());
                DrAmt = this.UtilityMember.NumberSet.ToDouble(dtTrans.Compute("SUM(AMOUNT)", "SOURCE=" + (int)Source.By).ToString());

                if ((rgTransactionType.SelectedIndex == 0) || (rgTransactionType.SelectedIndex == 2))
                {
                    dAmount = CrAmt - DrAmt;
                }
                else if (rgTransactionType.SelectedIndex == 1)
                {
                    dAmount = DrAmt - CrAmt;
                }
            }
            return dAmount;
        }

        private double GetCalculatedAmount(int LedId, DataTable dtVoucher)
        {
            double dAmount = 0;
            if (dtVoucher.Rows.Count > 0)
            {
                double CrAmt = 0;
                double DrAmt = 0;

                CrAmt = this.UtilityMember.NumberSet.ToDouble(dtVoucher.Compute("SUM(AMOUNT)", "SOURCE=" + (int)Source.To + " AND LEDGER_ID=" + LedId).ToString());
                DrAmt = this.UtilityMember.NumberSet.ToDouble(dtVoucher.Compute("SUM(AMOUNT)", "SOURCE=" + (int)Source.By + " AND LEDGER_ID=" + LedId).ToString());

                if ((rgTransactionType.SelectedIndex == 0) || (rgTransactionType.SelectedIndex == 2))
                {
                    dAmount = CrAmt - DrAmt;
                    if (dAmount > 0)
                    {
                        dAmount = -(dAmount);
                    }
                    else
                    {
                        dAmount = Math.Abs(dAmount);
                    }
                }
                else if (rgTransactionType.SelectedIndex == 1)
                {
                    dAmount = DrAmt - CrAmt;
                    if (dAmount > 0)
                    {
                        dAmount = +(dAmount);
                    }
                    else
                    {
                        dAmount = -(Math.Abs(dAmount));
                    }
                }
            }
            return dAmount;
        }

        private double FetchLedgerBalanceData(int LedId, DataTable dtVoucher)
        {
            double dAmount = 0;
            string strvalue = dtVoucher.Rows[0]["BUDGET_AMOUNT"].ToString();
            string[] strdoubleValue = strvalue.Split('/');
            double value = this.UtilityMember.NumberSet.ToDouble(strdoubleValue[0]);
            dAmount = value;
            return dAmount;
        }

        private double GetCalculatedTempAmount(int LedId, DataTable dtVoucher)
        {
            double dAmount = 0;
            if (dtVoucher.Rows.Count > 0)
            {
                double CrAmt = 0;
                double DrAmt = 0;

                CrAmt = this.UtilityMember.NumberSet.ToDouble(dtVoucher.Compute("SUM(TEMP_AMOUNT)", "SOURCE=" + (int)Source.To + " AND LEDGER_ID=" + LedId).ToString());
                DrAmt = this.UtilityMember.NumberSet.ToDouble(dtVoucher.Compute("SUM(TEMP_AMOUNT)", "SOURCE=" + (int)Source.By + " AND LEDGER_ID=" + LedId).ToString());

                if ((rgTransactionType.SelectedIndex == 0) || (rgTransactionType.SelectedIndex == 2))
                {
                    dAmount = CrAmt - DrAmt;
                    dAmount = Math.Abs(dAmount);
                    if (rgTransactionType.SelectedIndex == 0 || rgTransactionType.SelectedIndex == 2)
                    {
                        dAmount = -dAmount;
                    }
                    else
                    {
                        dAmount = +(dAmount);
                    }
                }
                else if (rgTransactionType.SelectedIndex == 1)
                {
                    dAmount = DrAmt - CrAmt;
                    if (rgTransactionType.SelectedIndex == 0 || rgTransactionType.SelectedIndex == 2)
                    {
                        dAmount = +(dAmount);
                    }
                    else
                    {
                        dAmount = -(dAmount);
                    }
                }
            }
            return dAmount;
        }

        private void SetBudgetInfo()
        {
            BalanceProperty BudgetAmt;
            BudgetAmt = FetchBudgetAmount(LedgerId);
            colLedger.VisibleIndex = 1;
            colAmount.VisibleIndex = 2;

            if (colGStAmt.Visible)
                colGSTLedgerClass.VisibleIndex = 3;
            else
                colGSTLedgerClass.Visible = false;

            // Ledger Narration - 27.08.2019
            if (EnableLedgerNarration)
                colLedgerNarration.VisibleIndex = 4;

            colReferenceNumber.VisibleIndex = 5;

            if (rgTransactionType.SelectedIndex == 2)
            {
                colCheque.Visible = true;
                colCheque.VisibleIndex = 6;

                colValueDate.Visible = true;
                colValueDate.VisibleIndex = 7;

                //colBudgetAmount.VisibleIndex = -1;
                //colGStAmt.VisibleIndex = -1;

                colReferenceNumber.Visible = false;
            }
            else
            {
                colCheque.Visible = false;
                colCheque.VisibleIndex = -1;

                colValueDate.Visible = false;
                colValueDate.VisibleIndex = -1;

                colBudgetAmount.VisibleIndex = 9;
                colGStAmt.VisibleIndex = 10;
            }

            colLedgerBal.VisibleIndex = 7;

            colAction.VisibleIndex = 11;
            if (rgTransactionType.SelectedIndex != 2)
                colCostCentre.VisibleIndex = 12;

            using (BudgetSystem budgetSystem = new BudgetSystem())
            {
                budgetSystem.ProjectId = ProjectId;
                budgetSystem.VoucherDate = Convert.ToDateTime(dtTransactionDate.DateTime);

                ShowTransGST();

                // Show reference No
                ShowTransReference();

                resultArgs = budgetSystem.CheckBudgetByDate();

                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    this.BudgetId = UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][budgetSystem.AppSchema.Budget.BUDGET_IDColumn.ColumnName].ToString());
                    this.BudgetPeriodDateFrom = UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][budgetSystem.AppSchema.Budget.DATE_FROMColumn.ColumnName].ToString(), false);
                    this.BudgetPeriodDateTo = UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][budgetSystem.AppSchema.Budget.DATE_TOColumn.ColumnName].ToString(), false);
                    this.BudgetTypeId = UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][budgetSystem.AppSchema.Budget.BUDGET_TYPE_IDColumn.ColumnName].ToString());
                    this.BudgetMonthDistribution = UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][budgetSystem.AppSchema.Budget.IS_MONTH_WISEColumn.ColumnName].ToString());
                    if (rgTransactionType.SelectedIndex != 2) colBudgetAmount.Visible = true;

                    if (resultArgs.Success)
                    {
                        budgetSystem.BudgetId = this.BudgetId;
                        budgetSystem.VoucherDate = Convert.ToDateTime(dtTransactionDate.DateTime);
                        resultArgs = budgetSystem.FetchActiveBudgetProjects();

                        if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            this.BudgetProjectIds = resultArgs.DataSource.Table.Rows[0][budgetSystem.AppSchema.Budget.PROJECT_IDColumn.ColumnName].ToString();
                        }

                    }
                    if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableGST).Equals((int)YesNo.Yes) && rgTransactionType.SelectedIndex != 2)
                    {
                        colGStAmt.Visible = true;
                    }
                    else
                    {
                        colGStAmt.Visible = false;
                    }

                    // reference No
                    if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableRefWiseReceiptANDPayment).Equals((int)YesNo.Yes) && rgTransactionType.SelectedIndex != 2)
                    {
                        colReferenceNumber.Visible = true;
                    }
                    else
                    {
                        colReferenceNumber.Visible = false;
                    }

                    if (EnableLedgerNarration)
                    {
                        colLedgerNarration.Visible = true;
                    }
                    else
                    {
                        colLedgerNarration.Visible = false;

                    }
                    // By Aldrin
                    if (BudgetId > 0 && BudgetAmt.Amount > 0)
                    {
                        string BudgetAmount = CalculateBudget(LedgerId); //FetchBudgetAmount(LedgerID);
                        if (BudgetAmount != string.Empty)
                        {
                            gvTransaction.SetFocusedRowCellValue(colBudgetAmount, BudgetAmount);
                        }
                    }
                    else
                    {
                        gvTransaction.SetFocusedRowCellValue(colBudgetAmount, string.Empty);
                    }
                }
                else
                {
                    this.BudgetId = 0;
                    colBudgetAmount.Visible = false;
                    if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableGST).Equals((int)YesNo.Yes))
                    {
                        colGStAmt.Visible = true;
                    }
                    else
                    {
                        colGStAmt.Visible = false;
                    }


                    // Show Reference No Visible 
                    ShowTransReference();
                }

            }

            //On 11/11/2024 - To show Currency Exchange and Currency Details
            ShowExchangeRateColumns();

            //On 15/11/2024
            colLedgerBal.Visible = false;
            colLedgerBalance.Visible = false;
        }

        private void ShowTransGST()
        {
            //17/11/2023
            ShowAdditionButtons(AdditionButttons.VendorGSTInvoiceDetails, false);
            if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableGST).Equals((int)YesNo.Yes))
            {
                colGSTLedgerClass.Visible = true;
                colGStAmt.Visible = true;

                //17/11/2023
                if (colGStAmt.Visible && this.AppSetting.IncludeGSTVendorInvoiceDetails == "1" && CanShowVendorGST)
                {
                    ShowAdditionButtons(AdditionButttons.VendorGSTInvoiceDetails, true);
                }
            }
            else
            {
                colGSTLedgerClass.Visible = false;
                colGStAmt.Visible = false;
            }
        }

        private void ShowCashBankVisibleGST()
        {
            if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableGST).Equals((int)YesNo.Yes))
            {
                colCBGSTAmount.Visible = false;
            }
            else
            {
                colCBGSTAmount.Visible = false;
            }
        }

        private void ShowTransReference()
        {
            if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableRefWiseReceiptANDPayment).Equals((int)YesNo.Yes))
            {
                //if (rgTransactionType.SelectedIndex == 0 || rgTransactionType.SelectedIndex == 1)
                if (rgTransactionType.SelectedIndex == 1)
                {
                    colReferenceNumber.Visible = true;
                }
                else
                {
                    colReferenceNumber.Visible = false;
                }
            }
            else
            {
                colReferenceNumber.Visible = false;
            }
        }

        /// <summary>
        /// On 11/11/2024 - To show Currency Exchange and Currency Details
        /// </summary>
        private void ShowExchangeRateColumns()
        {
            colExchangeAmount.Visible = colCashBankExchangeAmount.Visible = false;
            colLiveExchangeAmount.Visible = colCashBankLiveExchangeAmount.Visible = false;
            if (rgTransactionType.SelectedIndex == 2)
            {
                if (this.AppSetting.AllowMultiCurrency == 1)
                {
                    colCashBankExchangeAmount.Visible = true;
                    colCashBankExchangeAmount.VisibleIndex = colCashLedger.VisibleIndex + 1;

                    colCashBankLiveExchangeAmount.Visible = true;

                    if (CashBankGroupId == (Int32)FixedLedgerGroup.BankAccounts)
                        colCashBankLiveExchangeAmount.VisibleIndex = colMaterializedOn.VisibleIndex + 1;
                    else
                        colCashBankLiveExchangeAmount.VisibleIndex = colCashAmount.VisibleIndex + 1;
                }
            }
        }

        private void ShowCashBankGST()
        {
            EnableCashBankFields();
        }

        private void SetGSTInfo()
        {
            colLedger.VisibleIndex = 1;
            colAmount.VisibleIndex = 2;

            // Class GST Ledger 27.11.2019
            if (colGStAmt.Visible)
                colGSTLedgerClass.VisibleIndex = 3;
            else
                colGSTLedgerClass.Visible = false;

            // Ledger Narration - 27.08.2019
            if (EnableLedgerNarration)
                colLedgerNarration.VisibleIndex = 4;

            //  colCheque.VisibleIndex = 3;
            // colValueDate.VisibleIndex = 4;
            colReferenceNumber.VisibleIndex = 5;
            colLedgerBal.VisibleIndex = 6;
            colBudgetAmount.VisibleIndex = 7;
            colGStAmt.VisibleIndex = 8;
            colAction.VisibleIndex = 9;
            colCostCentre.VisibleIndex = 10;

            //On 15/11/2024
            colLedgerBal.Visible = false;
            colLedgerBalance.Visible = false;

        }

        private void LoadGSTLedgerClass()
        {
            using (GSTClassSystem GstClass = new GSTClassSystem())
            {
                resultArgs = GstClass.FetchGSTClassList();
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    rglkpLedgerGST.DisplayMember = "GST_NAME";
                    rglkpLedgerGST.ValueMember = "GST_Id";

                    DataTable dtGSTClass = resultArgs.DataSource.Table;
                    dtGSTClass.DefaultView.RowFilter = GstClass.AppSchema.MasterGSTClass.APPLICABLE_FROMColumn.ColumnName + "<'" + UtilityMember.DateSet.ToDate(dtTransactionDate.DateTime.ToShortDateString()) + "'";
                    dtGSTClass = dtGSTClass.DefaultView.ToTable();

                    rglkpLedgerGST.DataSource = dtGSTClass;
                }
            }
        }
        private void ApplyRecentPrjectDetails()
        {
            try
            {
                DateTime dtyearfrom = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
                DateTime dtbookbeginfrom = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false);
                DateTime dtYearTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
                using (AccouingPeriodSystem accountingSystem = new AccouingPeriodSystem())
                {
                    accountingSystem.YearFrom = this.AppSetting.YearFrom;
                    accountingSystem.YearTo = this.AppSetting.YearTo;
                    resultArgs = accountingSystem.FetchRecentProjectDetails(this.LoginUser.LoginUserId);
                    if (resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.AppSetting.UserProjectInfor = resultArgs.DataSource.Table.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        /// <summary>
        /// This is to Filter the Bank Closed Details and Bind it
        /// 
        /// also filter Bank Opened and bind it
        /// </summary>
        /// <param name="dtCashBankLedger"></param>
        /// <returns></returns>
        ////private DataTable FilterLedgerByDateClosed(DataTable dtBaseLedger, bool isCashBankLedgers)
        ////{
        ////    DataView dvledger = dtBaseLedger.AsDataView();
        ////    //dvledger.RowFilter = "DATE_CLOSED >='" + dtTransactionDate.DateTime + "' OR DATE_CLOSED IS NULL";

        ////    //On 20/10/2021
        ////    //dvledger.RowFilter = "(DATE_CLOSED >='" + UtilityMember.DateSet.ToDate(dtTransactionDate.DateTime.ToShortDateString()) + "' OR DATE_CLOSED IS NULL) AND " +
        ////    //                     "(DATE_OPENED <='" + UtilityMember.DateSet.ToDate(dtTransactionDate.DateTime.ToShortDateString()) + "' OR DATE_OPENED IS NULL)";

        ////    //On 13/07/2018, fitler Bank Openend date also
        ////    string datecondition = "(DATE_CLOSED >='" + UtilityMember.DateSet.ToDate(dtTransactionDate.DateTime.ToShortDateString()) + "' OR DATE_CLOSED IS NULL)";
        ////    if (isCashBankLedgers)
        ////    {
        ////        datecondition += " AND " + "(DATE_OPENED <='" + UtilityMember.DateSet.ToDate(dtTransactionDate.DateTime.ToShortDateString()) + "' OR DATE_OPENED IS NULL)";
        ////    }
        ////    dvledger.RowFilter = datecondition;
        ////    DataTable dtActiveLedgers = dvledger.ToTable();

        ////    return dtActiveLedgers;
        ////}

        /// <summary>
        /// On 28/12/2019, to get applicable from 
        /// </summary>
        /// <param name="gstclassid"></param>
        /// <returns></returns>
        private DateTime GetGSTApplicableFrom(Int32 gstclassid)
        {
            DateTime gstcalssapplyfrom = this.AppSetting.GSTStartDate;
            using (GSTClassSystem gstclass = new GSTClassSystem(gstclassid))
            {
                if (!string.IsNullOrEmpty(UtilityMember.DateSet.ToDate(gstclass.ApplicableFrom.ToShortDateString())))
                {
                    gstcalssapplyfrom = gstclass.ApplicableFrom;
                }
            }

            return gstcalssapplyfrom;
        }

        /// <summary>
        /// For Temp
        /// On 29/08/2020, To alert budget alert message if and if only Trans Amount exceeds Budget Approved Amount
        /// </summary>
        private void AlertBudgetMessage()
        {
            if (rtxtAmt.Tag != null && rtxtAmt.Tag.ToString() == "BudgetAlert")
            {
                rtxtAmt.Tag = string.Empty;
                // this.ShowMessageBox("The Transaction exceeds the Approved Budget");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.TRANSACTIONS_EXCEEDS));
                rtxtAmt.Tag = string.Empty;
            }
        }
        #endregion

        #region Active Donor Methods
        private void AssignInactiveDonors()
        {
            try
            {
                if (voucherId > 0)
                {
                    using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem(VoucherId))
                    {

                        DonorId = voucherSystem.DonorId;
                    }
                }

            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }
        #endregion

        #region User Rights
        private void ApplyRights()
        {
            try
            {
                if (rgTransactionType.SelectedIndex == 0)
                {
                    if (VoucherId == 0)
                    {
                        rgTransactionType.Properties.Items[1].Enabled = CommonMethod.ApplyUserRightsForTransaction((int)Payment.CreatePaymentVoucher) != 0 ? true : false;
                        rgTransactionType.Properties.Items[2].Enabled = CommonMethod.ApplyUserRightsForTransaction((int)Contra.CreateContraVoucher) != 0 ? true : false;
                    }
                    else
                    {
                        rgTransactionType.Properties.Items[1].Enabled = CommonMethod.ApplyUserRightsForTransaction((int)Payment.EditPaymentVoucher) != 0 ? true : false;
                        rgTransactionType.Properties.Items[2].Enabled = CommonMethod.ApplyUserRightsForTransaction((int)Contra.EditContraVoucher) != 0 ? true : false;
                    }
                }
                else if (rgTransactionType.SelectedIndex == 1)
                {
                    if (VoucherId == 0)
                    {
                        rgTransactionType.Properties.Items[0].Enabled = CommonMethod.ApplyUserRightsForTransaction((int)Receipt.CreateReceiptVoucher) != 0 ? true : false;
                        rgTransactionType.Properties.Items[2].Enabled = CommonMethod.ApplyUserRightsForTransaction((int)Contra.CreateContraVoucher) != 0 ? true : false;
                    }
                    else
                    {
                        rgTransactionType.Properties.Items[0].Enabled = CommonMethod.ApplyUserRightsForTransaction((int)Receipt.EditReceiptVoucher) != 0 ? true : false;
                        rgTransactionType.Properties.Items[2].Enabled = CommonMethod.ApplyUserRightsForTransaction((int)Contra.EditContraVoucher) != 0 ? true : false;
                    }
                }
                else
                {
                    if (VoucherId == 0)
                    {
                        rgTransactionType.Properties.Items[0].Enabled = CommonMethod.ApplyUserRightsForTransaction((int)Receipt.EditReceiptVoucher) != 0 ? true : false;
                        rgTransactionType.Properties.Items[1].Enabled = CommonMethod.ApplyUserRightsForTransaction((int)Payment.EditPaymentVoucher) != 0 ? true : false;
                    }
                    else
                    {
                        rgTransactionType.Properties.Items[0].Enabled = CommonMethod.ApplyUserRightsForTransaction((int)Receipt.EditReceiptVoucher) != 0 ? true : false;
                        rgTransactionType.Properties.Items[1].Enabled = CommonMethod.ApplyUserRightsForTransaction((int)Payment.EditPaymentVoucher) != 0 ? true : false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void ApplyRightsForShortCutKeys()
        {
            try
            {
                //ucRightShortcut.DiableLedger = CommonMethod.ApplyUserRights((int)Ledger.CreateLedger) != 0 ? BarItemVisibility.Always : BarItemVisibility.Never;
                ucRightShortcut.DisableBankAccount = CommonMethod.ApplyUserRights((int)BankAccount.CreateBankAccount) != 0 ? BarItemVisibility.Always : BarItemVisibility.Never;
                ucRightShortcut.DisableConfigure = CommonMethod.ApplyUserRights((int)Donor.CreateDonor) != 0 ? BarItemVisibility.Always : BarItemVisibility.Never;
                ucRightShortcut.DisableCostCentre = CommonMethod.ApplyUserRights((int)CostCentre.CreateCostCentre) != 0 ? BarItemVisibility.Always : BarItemVisibility.Never;
                ucRightShortcut.DisableDonor = CommonMethod.ApplyUserRightsForTransaction((int)Menus.MasterSetting) != 0 ? BarItemVisibility.Always : BarItemVisibility.Never;
                ucRightShortcut.DisableMapping = CommonMethod.ApplyUserRightsForTransaction((int)Menus.AccountMapping) != 0 ? BarItemVisibility.Always : BarItemVisibility.Never;

                ucRightShortcut.DisableReceipt = CommonMethod.ApplyUserRightsForTransaction((int)Menus.Receipt) != 0 ? BarItemVisibility.Always : BarItemVisibility.Never;
                ucRightShortcut.DisablePayment = CommonMethod.ApplyUserRightsForTransaction((int)Menus.Payments) != 0 ? BarItemVisibility.Always : BarItemVisibility.Never;
                ucRightShortcut.DisableContra = CommonMethod.ApplyUserRightsForTransaction((int)Menus.Contra) != 0 ? BarItemVisibility.Always : BarItemVisibility.Never;
                ucRightShortcut.DisableJournal = CommonMethod.ApplyUserRightsForTransaction((int)Menus.Journal) != 0 ? BarItemVisibility.Always : BarItemVisibility.Never;
                ucRightShortcut.DisableLedgerAdd = CommonMethod.ApplyUserRightsForTransaction((int)Menus.Ledger) != 0 ? BarItemVisibility.Always : BarItemVisibility.Never;

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        /// <summary>
        /// On 04/04/2022, This method should be called end of the form load event
        /// To enforce Enable and Track Receipt Module
        /// # to disable Change Date, Change Project Always for Modify Voucher
        /// </summary>
        private void EnforceReceiptModule()
        {
            if (AppSetting.IS_SDB_INM && (UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false) >= UtilityMember.DateSet.ToDate(SettingProperty.Enforce_Receipt_Module_FY, false)) &&
                dtTransactionDate.DateTime >= UtilityMember.DateSet.ToDate(SettingProperty.Enforce_Receipt_Module_FY, false))
            {
                bool modify = (VoucherId > 0);
                if (!AppSetting.ENABLE_TRACK_RECEIPT_MODULE) //&& VoucherType == DefaultVoucherTypes.Receipt
                {
                    //EnforceReceiptModule(new object[] { ucRightShortcut, rgTransactionType.Properties.Items[0], ucAdditionalInfo, }, modify);
                    EnforceReceiptModule(new object[] { ucRightShortcut, rgTransactionType.Properties.Items[0], }, modify);

                    if (VoucherType == DefaultVoucherTypes.Receipt)
                    {
                        EnforceReceiptModule(new object[] { ucAdditionalInfo, }, modify);
                    }
                }


                //As per the policy, they should not change date, project and amount while modifying receipt voucher
                //to track thier modification for receipt voucher, if needed, they have to delete receipt voucher and reenter
                if (VoucherId > 0 && VoucherType == DefaultVoucherTypes.Receipt)
                {
                    EnforceReceiptModule(new object[] { dtTransactionDate, ucRightShortcut, rgTransactionType }, true);
                    ucRightShortcut.LockPayment = false;
                    ucRightShortcut.LockContra = false;
                }
                else if (VoucherId > 0)
                {
                    rgTransactionType.Properties.Items[0].Enabled = false;
                    ucRightShortcut.LockReceipt = false;
                }
            }
        }

        /// <summary>
        /// On 10/01/2023, For SDBINB, they wanted to lock few fields when GST invoice vendor details exists
        /// </summary>
        private void EnforceLockingforGSTInvoiceVouchers()
        {
            if (this.AppSetting.HeadofficeCode.ToUpper() == "SDBINB" && VoucherId > 0 && GSTVendorId > 0)
            {
                string msg = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.PROVINCE_REGULATIONS); // "As per the Province regulation, You can't change Voucher details except Invoice details for GST Invoice enabled Vouchers";
                rgTransactionType.ToolTip = dtTransactionDate.ToolTip = msg;
                rgTransactionType.Enabled = dtTransactionDate.Enabled = false;
                ucRightShortcut.LockReceipt = false;
                ucRightShortcut.LockPayment = false;
                ucRightShortcut.LockContra = false;
                ucRightShortcut.LockDate = ucRightShortcut.LockNextVoucherDate = false;
                ucAdditionalInfo.DisableEntryMethod = BarItemVisibility.Never;

                //gcTransaction.Enabled = gcBank.Enabled = false;
                gvTransaction.OptionsBehavior.ReadOnly = true;
                gvBank.OptionsBehavior.ReadOnly = true;

                this.Text += " - " + msg;
            }
        }
        #endregion

        #region TDS Payment Methods
        private int HasTDSVoucher()
        {
            using (Bosco.Model.TDS.TDSPaymentSystem tdsPaymentSystem = new Bosco.Model.TDS.TDSPaymentSystem())
            {
                tdsPaymentSystem.VoucherId = VoucherId;
                return tdsPaymentSystem.HasTDSVoucher();
            }
        }

        private int HasTDSLedger()
        {
            using (Bosco.Model.TDS.TDSPaymentSystem tdsPaymentSystem = new Bosco.Model.TDS.TDSPaymentSystem())
            {
                tdsPaymentSystem.VoucherId = VoucherId;
                tdsPaymentSystem.LedgerId = LedgerId;
                return tdsPaymentSystem.HasTDSLedger();
            }
        }
        #endregion

        #region Voucher Lock
        private void FetchDateDuration()
        {
            try
            {
                resultArgs = base.GetAuditVoucherLockedDetails(ProjectId, dtTransactionDate.DateTime);
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    DataTable dtAuditLockDetails = resultArgs.DataSource.Table;
                    using (AuditLockTransSystem AuditSystem = new AuditLockTransSystem())
                    {
                        dtLockDateFrom = this.UtilityMember.DateSet.ToDate(dtAuditLockDetails.Rows[0][AuditSystem.AppSchema.AuditLockTransType.DATE_FROMColumn.ColumnName].ToString(), false);
                        dtLockDateTo = this.UtilityMember.DateSet.ToDate(dtAuditLockDetails.Rows[0][AuditSystem.AppSchema.AuditLockTransType.DATE_TOColumn.ColumnName].ToString(), false);
                    }
                }
                else
                {
                    //On 07/02/2024, For SDBINM, Lock Voucehrs before grace period
                    if (this.AppSetting.IS_SDB_INM && this.AppSetting.VoucherGraceDays > 0)
                    {

                        dtLockDateFrom = this.AppSetting.GraceLockDateFrom;
                        dtLockDateTo = this.AppSetting.GraceLockDateTo;
                    }
                    else
                    {
                        dtLockDateFrom = dtLockDateTo = DateTime.MinValue;
                    }
                }

            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private bool IsLockedTransaction(DateTime dtVoucherDate)
        {
            bool isSuccess = false;
            try
            {
                //Check temporary relaxation
                bool isEnforceTmpRelaxation = this.AppSetting.IsTemporaryGraceLockRelaxDate(dtVoucherDate);

                if (dtLockDateFrom != DateTime.MinValue && dtLockDateTo != DateTime.MinValue)
                {
                    if ((dtVoucherDate >= dtLockDateFrom && dtVoucherDate <= dtLockDateTo) && !isEnforceTmpRelaxation)
                    {
                        isSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
            return isSuccess;
        }
        #endregion

        #region CommonFunctions
        private void dtTransactionDate_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        private void txtNameAddress_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // e.IsInputKey = true;
        }

        private void txtVoucher_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        private void glkpPurpose_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        private void txtActualAmount_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        private void gvTransaction_LostFocus(object sender, EventArgs e)
        {
        }

        private void txtNarration_Enter(object sender, EventArgs e)
        {
            SetTextEditBackColor(txtNarration);
        }

        private void txtNarration_Leave(object sender, EventArgs e)
        {
            RemoveColor(txtNarration);
            txtNarration.Text = UtilityMember.StringSet.ToSentenceCase(txtNarration.Text);
        }

        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.KeyCode == Keys.Tab) { FocusCashTransactionGrid(); e.SuppressKeyPress = true; }
            else if (e.KeyCode == Keys.Tab) { txtNameAddress.Select(); txtNameAddress.Focus(); e.SuppressKeyPress = true; }
        }

        public bool CheckCostcentreEnabled(int LedgerId)
        {
            int CostCentre = 0;
            bool IsExists = false;
            using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
            {
                voucherSystem.LedgerId = LedgerId;
                resultArgs = voucherSystem.FetchCostCentreLedger();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    CostCentre = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][voucherSystem.AppSchema.Ledger.IS_COST_CENTERColumn.ColumnName].ToString());
                    IsExists = CostCentre > 0 ? true : false;
                }
            }
            return IsExists;
        }

        private void ShowDenominationForm()
        {
            try
            {
                DataView dvDenomination = new DataView();
                string BankLedgerName = string.Empty;
                gvBank.UpdateCurrentRow();
                gvTransaction.UpdateCurrentRow();
                DenominationLedgerId = this.UtilityMember.NumberSet.ToInteger(gvBank.GetFocusedRowCellValue(colLedgerId).ToString());
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    ledgerSystem.ProjectId = ProjectId;
                    ledgerSystem.LedgerId = DenominationLedgerId;
                    resultArgs = ledgerSystem.FetchCashBankLedgerById();
                    if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        BankLedgerName = resultArgs.DataSource.Table.Rows[0][ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                    }
                    if (LedgerGroupId.Equals(13) && CashLedgerAmount != 0 && CashBankGroupId.Equals(12))
                    {
                        int RowIndex = this.UtilityMember.NumberSet.ToInteger(gvBank.GetDataSourceRowIndex(gvBank.FocusedRowHandle).ToString());
                        if (dsDenomination.Tables.Contains(RowIndex + "LDR" + DenominationLedgerId))
                        {
                            dvDenomination = dsDenomination.Tables[RowIndex + "LDR" + DenominationLedgerId].DefaultView;
                        }
                        frmDenomination frmDenomination = new frmDenomination(VoucherId, CashLedgerAmount, BankLedgerName, DenominationLedgerId, dvDenomination);
                        frmDenomination.ShowDialog();
                        if (frmDenomination.DialogResult == DialogResult.OK)
                        {
                            DataTable dtValues = frmDenomination.dtDenomination;
                            if (dtValues != null)
                            {
                                dtValues.TableName = RowIndex + "LDR" + DenominationLedgerId;
                                if (dsDenomination.Tables.Contains(dtValues.TableName))
                                {
                                    dsDenomination.Tables.Remove(dtValues.TableName);
                                }
                                dsDenomination.Tables.Add(dtValues);
                            }
                        }
                        else
                        {
                            frmDenomination.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            finally { }
        }

        private void rtxtCashAmount_Leave(object sender, EventArgs e)
        {
            int s = rgTransactionType.SelectedIndex;
            if (s.Equals((int)VoucherSubTypes.CN))
            {
                // ShowDenominationForm();
            }
        }

        private void ucAdditionalInfo_PrintVoucherClicked(object sender, EventArgs e)
        {
            if (rgTransactionType.SelectedIndex != 2)
            {
                using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
                {
                    Bosco.Report.Base.IReport report = new Bosco.Report.Base.ReportEntry(this.MdiParent);
                    int rid = rgTransactionType.SelectedIndex;
                    string rptVoucher = rid == 0 ? UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKRECEIPTS) : rid == 1 ? UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKPAYMENTS) : string.Empty;
                    resultArgs = voucherSystem.FetchReportSetting(rptVoucher);
                    if (resultArgs != null && resultArgs.Success)
                    {
                        ReportProperty.Current.VoucherPrintSettingInfo = resultArgs.DataSource.TableView;
                        ReportProperty.Current.CashBankVoucherDateFrom = ReportProperty.Current.CashBankVoucherDateTo = this.UtilityMember.DateSet.ToDate(dtTransactionDate.Text, false);
                        report.VoucherPrint(this.VoucherId.ToString(), rptVoucher, ProjectName, ProjectId);
                    }
                    else
                    {
                        this.ShowMessageBoxError(resultArgs.Message);
                    }
                }
            }
        }

        private string CalculateBudget(int ledgerId)
        {
            ResultArgs result = new ResultArgs();
            string tmpMode = string.Empty;
            string value = string.Empty;
            string BalanceCurrentTransaction = string.Empty;

            double OldValue = 0;
            double NewValue = 0;
            double CalOldNewValue = 0;

            BalanceProperty CurrentAmount;
            // BalanceProperty OPAmount; // done by Mr Alex Aldrin
            BalanceProperty BudgetAmount;
            BudgetAmount = FetchBudgetAmount(ledgerId);
            CurrentAmount = FetchBudgetLedgerBalance(ledgerId);

            DataTable dtTemp = gcTransaction.DataSource as DataTable;
            string NewValueMode = string.Empty;

            if (dtTemp != null)
            {
                NewValue = GetCalculatedAmount(LedgerId, dtTemp);
                OldValue = GetCalculatedTempAmount(LedgerId, dtTemp);
                CalOldNewValue = (OldValue - NewValue);
                //LedgerBalance = GetCurBalance(LedgerId, OldValue, NewValue);
            }

            //OPAmount = FetchLedgerOPBalance(ledgerId);
            double Total = CurrentAmount.Amount; // != 0 ? Math.Abs(CurrentAmount.Amount - OPAmount.Amount) : CurrentAmount.Amount;

            // ((BudgetAmount) + (Temp - New Amount))

            string ActualAmount = string.Empty;
            int tmpNatureID = gvTransaction.GetFocusedRowCellValue(colSource) != null ?
                            this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetFocusedRowCellValue(colSource).ToString()) : 0;

            if (rgTransactionType.SelectedIndex == 0)
            {

                if (CurrentLedgerTransMode == "CR")
                {
                    Total = Total + CalOldNewValue;              //Total + LedgerAmount;
                }

                // chinna 17.04.2018
                //if (CurrentLedgerTransMode == "DR")
                //{
                //    Total = Total > LedgerAmount ? Total - LedgerAmount : LedgerAmount - Total;
                //}
                //else
                //{
                //    Total = Total + LedgerAmount;
                //}
            }
            else
            {
                if (CurrentLedgerTransMode == "CR" && Total != LedgerAmount)
                {
                    // Total = Total > LedgerAmount ? Total - LedgerAmount : LedgerAmount - Total;
                    Total = Total > LedgerAmount ? Total - CalOldNewValue : CalOldNewValue - Total;
                }
                else if (CurrentLedgerTransMode == "DR" && Total == LedgerAmount)
                {
                    //  Total = Total > LedgerAmount ? Total - LedgerAmount : LedgerAmount - Total;
                    Total = Total > LedgerAmount ? Total - CalOldNewValue : CalOldNewValue - Total;
                }
                else if (CurrentLedgerTransMode == "DR" && Total != LedgerAmount)
                {
                    //Total = Total + LedgerAmount;
                    //  VoucherId == 0 ? 0 : 0;
                    if (VoucherId == 0)
                    {
                        Total = Total = Total + LedgerAmount;
                    }
                    else
                    {
                        Total = Total = Total + (OldValue + NewValue);
                    }
                }
                else
                {
                    //Total = Total > LedgerAmount ? Total - LedgerAmount : LedgerAmount - Total;
                    Total = Total > LedgerAmount ? Total - CalOldNewValue : CalOldNewValue - Total;
                }



                //if (CurrentLedgerTransMode == "CR" && Total != LedgerAmount)
                //{
                //    Total = Total > LedgerAmount ? Total - LedgerAmount : LedgerAmount - Total;
                //}
                //else if (CurrentLedgerTransMode == "DR" && Total == LedgerAmount)
                //{
                //    Total = Total > LedgerAmount ? Total - LedgerAmount : LedgerAmount - Total;
                //}
                //else
                //{
                //    Total = Total > LedgerAmount ? Total - LedgerAmount : LedgerAmount - Total;
                //}
            }

            if (Total != 0)
            {
                // done by chinna 18.04.2018
                // ActualAmount = UtilityMember.NumberSet.ToCurrency(Total).ToString() + " " + CurrentLedgerTransMode;
                ActualAmount = Math.Abs(this.UtilityMember.NumberSet.ToDouble(Total.ToString())).ToString();
            }
            else
            {
                //done by chinna 18.04.2018
                // ActualAmount = "0.00" + " " + CurrentLedgerTransMode;
                ActualAmount = "0.00";
            }

            value = this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(ActualAmount.ToString())) + " / " + UtilityMember.NumberSet.ToNumber(BudgetAmount.Amount) + " " + BudgetAmount.TransMode;
            // value = UtilityMember.NumberSet.ToNumber(BudgetAmount.Amount) + " " + BudgetAmount.TransMode + " / " + this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(ActualAmount.ToString()));

            return value;
        }

        private int IsGSTLedgers(int LedgerId)
        {
            int GStId = 0;
            using (VoucherTransactionSystem voucherTrans = new VoucherTransactionSystem())
            {
                voucherTrans.LedgerId = LedgerId;
                resultArgs = voucherTrans.FetchIsGStLedger();
                if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    GStId = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][voucherTrans.AppSchema.Ledger.IS_GST_LEDGERSColumn.ColumnName].ToString());
                }
            }
            return GStId;
        }

        private int IsIGSTLedgers(int LedgerId)
        {
            int isGST = 0;
            using (VoucherTransactionSystem VoucherTrans = new VoucherTransactionSystem())
            {
                VoucherTrans.LedgerId = LedgerId;
                VoucherTrans.State = this.AppSetting.State;
                resultArgs = VoucherTrans.FetchIsIGSTApplicable();
                if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    isGST = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][VoucherTrans.AppSchema.Ledger.IS_GST_LEDGERSColumn.ColumnName].ToString());
                }
            }
            return isGST;
        }

        private int FetchGSTLedger(int LegerId)
        {
            int gstId = 0;
            using (LedgerSystem ledgersystem = new LedgerSystem())
            {
                // This is to set the Ledger id which is send  12.07.2019

                // ledgersystem.LedgerId = LedgerId; 
                ledgersystem.LedgerId = LegerId;
                gstId = ledgersystem.FetchGStId();
            }
            return gstId;
        }

        private ResultArgs FetchGSTPercentages(int GStId)
        {
            using (GSTClassSystem gstclass = new GSTClassSystem())
            {
                resultArgs = gstclass.FetchGSt(GStId);
            }
            return resultArgs;
        }


        /// <summary>
        /// Calculate GST values for given GST class Id
        /// </summary>
        /// <returns></returns>
        private string CalculateGST()
        {
            string value = "";
            //int GSTId = FetchGSTLedger(LedgerId);
            if (LedgerGSTClassId > 0)
            {
                int IsIGSTExistLedger = IsIGSTLedgers(LedgerId);
                if (IsIGSTExistLedger == 0)
                {
                    resultArgs = FetchGSTPercentages(LedgerGSTClassId); //FetchGSTPercentages(GSTId);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        double gst = this.UtilityMember.NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0]["GST"].ToString());
                        double cgst = this.UtilityMember.NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0]["CGST"].ToString());
                        double sgst = this.UtilityMember.NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0]["SGST"].ToString());

                        //On 10/05/2021, to have real GST values without rounding value---------------------------------
                        /*double gstCalcAmt = LedgerAmount * gst / 100;
                        double cgstCalcAmt = LedgerAmount * cgst / 100;
                        double sgstCalcAmt = LedgerAmount * sgst / 100;*/
                        //---------------------------------------------------------------------------------------------

                        double gstCalcAmt = LedgerAmount * gst / 100;
                        double cgstCalcAmt = UtilityMember.NumberSet.TruncateDoubleByPrecision((LedgerAmount * cgst / 100), 2);
                        double sgstCalcAmt = UtilityMember.NumberSet.TruncateDoubleByPrecision((LedgerAmount * sgst / 100), 2);
                        gstCalcAmt = cgstCalcAmt + sgstCalcAmt;

                        //value = UtilityMember.NumberSet.ToNumber(gstCalcAmt) + "  (" + UtilityMember.NumberSet.ToNumber(cgstCalcAmt) + " + " + UtilityMember.NumberSet.ToNumber(sgstCalcAmt) + ") ";
                        value = UtilityMember.NumberSet.ToNumber(gstCalcAmt) + "  (" + UtilityMember.NumberSet.ToNumber(cgstCalcAmt) + " + " + UtilityMember.NumberSet.ToNumber(sgstCalcAmt) + " / " + UtilityMember.NumberSet.ToNumber(0.00) + ") ";
                    }
                }
                else
                {
                    resultArgs = FetchGSTPercentages(LedgerGSTClassId); //FetchGSTPercentages(GSTId);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        double gst = this.UtilityMember.NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0]["GST"].ToString());
                        double igst = this.UtilityMember.NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0]["GST"].ToString());

                        //On 10/05/2021, to have real GST values without rounding value---------------------------------
                        /*double gstCalcAmt = LedgerAmount * gst / 100;
                        double igstCalcAmt = LedgerAmount * igst / 100;*/
                        //----------------------------------------------------------------------------------------------
                        double gstCalcAmt = UtilityMember.NumberSet.TruncateDoubleByPrecision((LedgerAmount * gst / 100), 2);
                        double igstCalcAmt = UtilityMember.NumberSet.TruncateDoubleByPrecision((LedgerAmount * igst / 100), 2);

                        value = UtilityMember.NumberSet.ToNumber(igstCalcAmt) + "  (" + UtilityMember.NumberSet.ToNumber(0.00) + " + " + UtilityMember.NumberSet.ToNumber(0.00) + " / " + UtilityMember.NumberSet.ToNumber(igstCalcAmt) + ") ";
                    }
                }
            }
            return value;
        }

        /// <summary>
        /// This method is used to attach GST legers in Receipt voucher entry (CR grid) automatically and add total GST amount into (Cash/Bank)
        /// 
        /// This method should be called only for 
        ///     Receipts Vouchers, When User Clicks Save button after confimation (Y/N)
        ///     1. GST should be enabled
        ///     2. GST amoutn should be > 0  
        /// </summary>
        private void ApplyGST(DataTable dtVoucherTrans, DataTable dtCashBankTrans, bool isAttach)
        {
            if (colGStAmt.Visible)  // 09.05.2109
            {
                if (dtTransactionDate.DateTime >= this.AppSetting.GSTStartDate)
                {
                    gstCalcAmount = UpdateGST = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(GST)", "").ToString());
                    cgstCalcAmount = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(CGST)", "").ToString());
                    sgstCalcAmount = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(SGST)", "").ToString());
                    igstCalcAmount = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(IGST)", "").ToString());
                    double GSTAmount = igstCalcAmount == 0 ? (cgstCalcAmount + sgstCalcAmount) : igstCalcAmount;

                    //0. Check GST Ledges avialbe
                    if (this.AppSetting.CGSTLedgerId > 0 && this.AppSetting.SGSTLedgerId > 0)
                    {
                        //1. Check GST Enabgled 
                        if (AppSetting.EnableGST == "1" && rgTransactionType.SelectedIndex == 0 || rgTransactionType.SelectedIndex == 1)
                        {
                            //2. Check GST amount, If GST amout available, ask confirmationh from user to attach GST leges in Voucher entry
                            if (GSTAmount > 0)
                            {
                                if (isAttach)
                                {
                                    //if (this.ShowConfirmationMessage("Do you want to apply GST for this Voucher?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                                    //{ //On 29/11/2019
                                    //3. Attach CGST
                                    if (cgstCalcAmount > 0 && this.AppSetting.CGSTLedgerId > 0)
                                    {
                                        DataRow drCGST = dtVoucherTrans.NewRow();
                                        drCGST["LEDGER_ID"] = this.AppSetting.CGSTLedgerId;
                                        drCGST["AMOUNT"] = cgstCalcAmount;
                                        if (rgTransactionType.SelectedIndex == 0)
                                        {
                                            drCGST["SOURCE"] = 1;
                                        }
                                        else { drCGST["SOURCE"] = 2; }
                                        dtVoucherTrans.Rows.Add(drCGST);
                                    }

                                    //4. Attach SGST
                                    if (sgstCalcAmount > 0 && this.AppSetting.SGSTLedgerId > 0)
                                    {
                                        DataRow drSGST = dtVoucherTrans.NewRow();
                                        drSGST["LEDGER_ID"] = this.AppSetting.SGSTLedgerId;
                                        drSGST["AMOUNT"] = sgstCalcAmount;
                                        if (rgTransactionType.SelectedIndex == 0)
                                        {
                                            drSGST["SOURCE"] = 1;
                                        }
                                        else { drSGST["SOURCE"] = 2; }
                                        dtVoucherTrans.Rows.Add(drSGST);
                                    }

                                    //5. Attach IGST 
                                    if (igstCalcAmount > 0 && this.AppSetting.IGSTLedgerId > 0)
                                    {
                                        DataRow drIGST = dtVoucherTrans.NewRow();
                                        drIGST["LEDGER_ID"] = this.AppSetting.IGSTLedgerId;
                                        drIGST["AMOUNT"] = igstCalcAmount;
                                        if (rgTransactionType.SelectedIndex == 0)
                                        {
                                            drIGST["SOURCE"] = 1;
                                        }
                                        else { drIGST["SOURCE"] = 2; }
                                        dtVoucherTrans.Rows.Add(drIGST);
                                    }

                                    //6. Add Total GST amount into Cash/Bank ledgers
                                    if (dtCashBankTrans.Rows.Count > 0)
                                    {
                                        double cashBankamount = this.UtilityMember.NumberSet.ToDouble(dtCashBankTrans.Rows[0]["Amount"].ToString());
                                        //dtCashBankTrans.Rows[0]["Amount"] = cashBankamount + gstCalcAmount;
                                        dtCashBankTrans.Rows[0]["Amount"] = cashBankamount;
                                        UpdateGST = gstCalcAmount - UpdateGST;
                                        dtCashBankTrans.Rows[0]["GST_TOTAL"] = this.UtilityMember.NumberSet.ToNumber(gstCalcAmount);
                                        dtCashBankTrans.AcceptChanges();
                                    }

                                    //26/04/2019, to ask confirmation to update gst vendor invoice detials
                                    dtVoucherTrans.DefaultView.RowFilter = "LEDGER_ID>0 AND AMOUNT>0";
                                    // dtVoucherTrans.DefaultView.RowFilter = "LEDGER_ID NOT IN(" + this.AppSetting.CGSTLedgerId + ") AND LEDGER_ID<>" + this.AppSetting.SGSTLedgerId + " AND LEDGER_ID<>" + this.AppSetting.IGSTLedgerId;
                                    // dtVoucherTrans.AcceptChanges();
                                    this.Transaction.TransInfo = dtVoucherTrans.DefaultView;
                                    //On 10/04/2023, move this logic to alert to fill gst invoice detals in validation screen
                                    /*if (CanShowVendorGST)
                                    {
                                        if (string.IsNullOrEmpty(GSTVendorInvoiceNo))
                                        {
                                            if (this.ShowConfirmationMessage("Do you want to update Vendor GST Invoice Details ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                                            {
                                                ShowVendorGSTInvoiceDetails();
                                            }
                                            else
                                            {
                                                //26/04/2019, clear vendor gst invoice
                                                GSTVendorInvoiceNo = string.Empty;
                                                GSTVendorInvoiceDate = string.Empty;
                                                GSTVendorInvoiceType = 0;
                                                GSTVendorId = 0;
                                                DtGSTInvoiceMasterDetails = null;
                                                DtGSTInvoiceMasterLedgerDetails = null;
                                            }
                                        }
                                    }*/
                                    //}
                                    //else
                                    //{
                                    //    ClearGSTValues(dtVoucherTrans);
                                    //    dtVoucherTrans.DefaultView.RowFilter = "LEDGER_ID NOT IN(" + this.AppSetting.CGSTLedgerId + ") AND LEDGER_ID<>" + this.AppSetting.SGSTLedgerId + " AND LEDGER_ID<>" + this.AppSetting.IGSTLedgerId;
                                    //}
                                }
                                else
                                {
                                    if (dtVoucherTrans.Rows.Count > 0)
                                    {
                                        gstCalcAmount = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(GST)", "").ToString());
                                        cgstCalcAmount = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(CGST)", "").ToString());
                                        sgstCalcAmount = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(SGST)", "").ToString());
                                        igstCalcAmount = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(IGST)", "").ToString());

                                        dtVoucherTrans.DefaultView.RowFilter = "LEDGER_ID NOT IN(" + this.AppSetting.CGSTLedgerId + ") AND LEDGER_ID<>" + this.AppSetting.SGSTLedgerId + " AND LEDGER_ID<>" + this.AppSetting.IGSTLedgerId;
                                    }
                                    if (dtCashBankTrans != null && dtCashBankTrans.Rows.Count > 0)
                                    {
                                        // Show Actual Cash\Bank Balance without dedect the GST
                                        double cashBankamount = this.UtilityMember.NumberSet.ToDouble(dtCashBankTrans.Rows[0]["Amount"].ToString());
                                        dtCashBankTrans.Rows[0]["Amount"] = cashBankamount;
                                        dtCashBankTrans.Rows[0]["GST_TOTAL"] = this.UtilityMember.NumberSet.ToNumber(gstCalcAmount);
                                        dtCashBankTrans.Rows[0]["TEMP_AMOUNT"] = cashBankamount;
                                        dtCashBankTrans.Rows[0]["BASE_AMOUNT"] = cashBankamount;
                                        dtCashBankTrans.AcceptChanges();

                                        //double cashBankamount = this.UtilityMember.NumberSet.ToDouble(dtCashBankTrans.Rows[0]["Amount"].ToString());
                                        //dtCashBankTrans.Rows[0]["Amount"] = cashBankamount - gstCalcAmount;
                                        //dtCashBankTrans.Rows[0]["GST_TOTAL"] = this.UtilityMember.NumberSet.ToNumber(gstCalcAmount);
                                        //dtCashBankTrans.Rows[0]["TEMP_AMOUNT"] = cashBankamount - gstCalcAmount;
                                        //dtCashBankTrans.Rows[0]["BASE_AMOUNT"] = cashBankamount - gstCalcAmount;
                                        //dtCashBankTrans.AcceptChanges();
                                    }
                                }
                            }
                        }
                        else
                        {
                            ClearGSTValues(dtVoucherTrans);
                            dtVoucherTrans.DefaultView.RowFilter = "LEDGER_ID NOT IN(" + this.AppSetting.CGSTLedgerId + ") AND LEDGER_ID<>" + this.AppSetting.SGSTLedgerId + " AND LEDGER_ID<>" + this.AppSetting.IGSTLedgerId;
                        }
                    }
                    else
                    {
                        // MessageRender.ShowMessage("GST ledgers are not avialble");
                        MessageRender.ShowMessage(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.GST_LEDGER_AVAI));
                    }
                }
                else
                {
                    DataTable dtVoucher = gcTransaction.DataSource as DataTable;
                    ClearGSTValues(dtVoucher);
                }
            }
            else
            {
                DataTable dtVoucher = gcTransaction.DataSource as DataTable;
                ClearGSTValues(dtVoucher);
            }

            if (!CanShowVendorGST && dtVoucherTrans != null && dtCashBankTrans != null)
            {
                GSTVendorInvoiceNo = GSTVendorInvoiceDate = string.Empty;
                GSTVendorId = GSTVendorInvoiceType = 0;
                DtGSTInvoiceMasterDetails = null;
                DtGSTInvoiceMasterLedgerDetails = null;
            }
            else if (!string.IsNullOrEmpty(GSTVendorInvoiceNo) && DtGSTInvoiceMasterDetails != null && DtGSTInvoiceMasterDetails.Rows.Count > 0)
            {
                // On 15/11/2022, after attached GST invoice details, if voucher and and percentage changed, update into GST invoice master
                using (LedgerSystem ledgerSys = new LedgerSystem())
                {
                    DataTable dtSumVoucherTrans = gcTransaction.DataSource as DataTable;
                    if (dtVoucherTrans != null)
                    {
                        double cgst = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(CGST)", "").ToString());
                        double sgst = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(SGST)", "").ToString());
                        double igst = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(IGST)", "").ToString());

                        DateTime transdate = this.UtilityMember.DateSet.ToDate(dtTransactionDate.Text, false);
                        DtGSTInvoiceMasterDetails.Rows[0][ledgerSys.AppSchema.GSTInvoiceMaster.GST_VENDOR_INVOICE_DATEColumn.ColumnName] = transdate;
                        DtGSTInvoiceMasterDetails.Rows[0][ledgerSys.AppSchema.GSTInvoiceMaster.TOTAL_AMOUNTColumn.ColumnName] = TransSummaryVal;
                        DtGSTInvoiceMasterDetails.Rows[0][ledgerSys.AppSchema.GSTInvoiceMaster.TOTAL_CGST_AMOUNTColumn.ColumnName] = cgst;
                        DtGSTInvoiceMasterDetails.Rows[0][ledgerSys.AppSchema.GSTInvoiceMaster.TOTAL_SGST_AMOUNTColumn.ColumnName] = sgst;
                        DtGSTInvoiceMasterDetails.Rows[0][ledgerSys.AppSchema.GSTInvoiceMaster.TOTAL_IGST_AMOUNTColumn.ColumnName] = igst;
                        DtGSTInvoiceMasterDetails.AcceptChanges();
                    }
                }
            }

        }

        private string GSTTotalAmount(DataTable dtVoucherTrans)
        {
            string GStValues = string.Empty;
            if (dtTransactionDate.DateTime >= this.AppSetting.GSTStartDate)
            {
                if (dtTransactionDate.DateTime >= this.AppSetting.GSTStartDate)
                {
                    UpdateGST = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(GST)", "").ToString());
                    GStValues = this.UtilityMember.NumberSet.ToNumber(UpdateGST);
                }

            }
            return GStValues;
        }

        private void ClearGSTValues(DataTable dtVouchers)
        {
            if (VoucherId > 0)
            {
                foreach (DataRow dr in dtVouchers.Rows)
                {
                    dr["GST_AMOUNT"] = string.Empty;
                    dr["GST"] = 0;
                    dr["CGST"] = 0;
                    dr["SGST"] = 0;
                    dr["IGST"] = 0;
                    dtVouchers.AcceptChanges();
                }
            }

            //26/04/2019, clear vendor gst invoice
            GSTVendorInvoiceNo = string.Empty;
            GSTVendorInvoiceDate = string.Empty;
            GSTVendorInvoiceType = 0;
            GSTVendorId = 0;
            DtGSTInvoiceMasterDetails = null;
            DtGSTInvoiceMasterLedgerDetails = null;
        }

        private Int32 GetLedgerGroupId(Int32 ledgerId)
        {
            Int32 LedgerGroupId = 0;
            try
            {
                using (LedgerSystem ledger = new LedgerSystem())
                {
                    ledger.LedgerId = ledgerId;
                    LedgerGroupId = ledger.FetchLedgerGroupById();
                }
            }
            catch (Exception Err)
            {
                MessageRender.ShowMessage("Could not get Ledger Group " + Err.Message);
            }
            return LedgerGroupId;
        }

        private void gvBank_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableGST).Equals((int)YesNo.Yes))
            {
                if (e.Column == colCashLedger)
                {
                    //17/11/2023
                    //ShowAdditionButtons(AdditionButttons.VendorGSTInvoiceDetails, false);

                    DataTable dtTrans = gcTransaction.DataSource as DataTable;
                    DataView dvTrans = new DataView(dtTrans);
                    dvTrans.RowFilter = "LEDGER_ID>0 AND AMOUNT>0";
                    dtTrans = dvTrans.ToTable(); ;

                    //25/04/2019, to reset gst amount for non gst ledgers
                    //if (dtTrans != null && dtTrans.Rows.Count >)
                    if (dtTrans != null && dtTrans.Rows.Count > 0) //&& UpdateGST > 0 //On 20/10/2023 Allow GST Invoice always without GST Amount
                    {
                        e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                        // double AmtGSTTotal = UtilityMember.NumberSet.ToDouble(colCashAmount.SummaryItem.SummaryValue.ToString()) + UpdateGST;
                        double AmtGSTTotal = UtilityMember.NumberSet.ToDouble(colCashAmount.SummaryItem.SummaryValue.ToString());
                        e.Info.Value = "Total Amount with GST: " + this.UtilityMember.NumberSet.ToCurrency(AmtGSTTotal);
                        e.Info.DisplayText = e.Info.Value.ToString();

                        //17/11/2023
                        if (colGStAmt.Visible && this.AppSetting.IncludeGSTVendorInvoiceDetails == "1" && CanShowVendorGST)
                        {
                            ShowAdditionButtons(AdditionButttons.VendorGSTInvoiceDetails, true);
                        }
                    }
                    else
                    {
                        //colCashLedger.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.None;
                    }
                }
            }
            else
            {
                colCashLedger.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.None;
            }
        }


        /// <summary>
        /// 25/04/2019, based on the condiditons, show additional buttons
        /// </summary>
        private void ShowAdditionButtons(AdditionButttons additionbuttions, bool show)
        {
            //1. For Vendor GST Invoice details
            if (additionbuttions == AdditionButttons.VendorGSTInvoiceDetails && this.AppSetting.IncludeGSTVendorInvoiceDetails == "1")
            {
                btnVendor.Visible = show;
                lcVendor.Visibility = show ? LayoutVisibility.Always : LayoutVisibility.Never;

                lcRemoveVendorGSTInvoice.Visibility = LayoutVisibility.Never;
                //btnRemoveVendorGSTInvoce.Visible = btnPrintVendorGSTInvoce.Visible = false;
                if (show && ((VoucherId > 0 && GSTInvoiceId > 0) || (DtGSTInvoiceMasterDetails != null && DtGSTInvoiceMasterDetails.Rows.Count > 0)))
                {
                    lcRemoveVendorGSTInvoice.Visibility = LayoutVisibility.Always;

                    //lcPrintVendorGSTInvoce.Visibility = LayoutVisibility.Never;
                    //btnRemoveVendorGSTInvoce.Visible = btnPrintVendorGSTInvoce.Visible = true;

                    /* if (VoucherId > 0 && GSTInvoiceId > 0)
                     {
                         btnPrintVendorGSTInvoce.Visible = true;
                     }
                     else if (DtGSTInvoiceMasterDetails != null && DtGSTInvoiceMasterDetails.Rows.Count > 0)
                     {
                         lcPrintVendorGSTInvoce.Visibility = LayoutVisibility.Always;
                     }*/
                }
            }

            //2. For Enable Sub Ledgers
            //07/02/2020, to enable sub ledger vouchers based on settings
            if (additionbuttions == AdditionButttons.SubLedgerDetails && AppSetting.IS_DIOMYS_DIOCESE && this.AppSetting.EnableSubLedgerVouchers == "1")
            {
                if (rgTransactionType.SelectedIndex != 0 && rgTransactionType.SelectedIndex != 1 && show)
                {
                    show = false; //Hide for contra and journal
                }
                //   btnSubLedgers.Visible = show;
                //  lcSubLedgers.Visibility = show ? LayoutVisibility.Always : LayoutVisibility.Never;
            }
        }

        /// <summary>
        /// On 25/04/2015, To show/Get Vendor GST Invoice details
        /// </summary>
        private void ShowVendorGSTInvoiceDetails()
        {
            bool showvendor = false;
            if (CanShowVendorGST)
            {
                DataTable dtVoucherTrans = gcTransaction.DataSource as DataTable;
                if (dtVoucherTrans != null)
                {
                    using (VoucherTransactionSystem sysvoucher = new VoucherTransactionSystem())
                    {
                        double cgst = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(" + sysvoucher.AppSchema.VoucherTransaction.CGSTColumn.ColumnName + ")", "").ToString());
                        double sgst = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(" + sysvoucher.AppSchema.VoucherTransaction.SGSTColumn.ColumnName + ")", "").ToString());
                        double igst = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(" + sysvoucher.AppSchema.VoucherTransaction.IGSTColumn.ColumnName + ")", "").ToString());

                        //if (cgst > 0 || sgst > 0 || igst > 0) //On 20/10/2023 Allow GST Invoice always without GST Amount
                        //{
                        ResultArgs result = sysvoucher.AttachVoucherLedgetsToGSTInvoiceLedgerDetails(true, VoucherType.ToString(), DtGSTInvoiceMasterLedgerDetails, dtVoucherTrans);
                        if (result.Success)
                        {
                            //AttachGSTInvoiceLedgerDetails();
                            DtGSTInvoiceMasterLedgerDetails = result.DataSource.Table;
                            showvendor = true;
                            DateTime transdate = this.UtilityMember.DateSet.ToDate(dtTransactionDate.Text, false);
                            frmVoucherVendorGSTInvoiceDetails frmVendorGST = new frmVoucherVendorGSTInvoiceDetails(voucherType, ProjectId, GSTInvoiceId, DtGSTInvoiceMasterDetails, DtGSTInvoiceMasterLedgerDetails, VoucherId,
                                                    transdate, GSTVendorId, GSTVendorInvoiceNo, GSTVendorInvoiceType, GSTVendorInvoiceDate, TransSummaryVal, cgst, sgst, igst, txtNarration.Text);
                            frmVendorGST.DtGSTInvoiceMasterDetails = DtGSTInvoiceMasterDetails;
                            frmVendorGST.ShowDialog();
                            if (frmVendorGST.DialogResult == System.Windows.Forms.DialogResult.OK)
                            {
                                GSTVendorInvoiceNo = frmVendorGST.InvoiceNo.Trim();
                                GSTVendorInvoiceDate = frmVendorGST.InvoiceDate;
                                GSTVendorInvoiceType = frmVendorGST.InvoiceType;
                                GSTVendorId = frmVendorGST.VendorId;
                                GSTInvoiceId = frmVendorGST.GSTInvoiceId;
                                DtGSTInvoiceMasterDetails = frmVendorGST.DtGSTInvoiceMasterDetails;
                                ShowAdditionButtons(AdditionButttons.VendorGSTInvoiceDetails, true);
                            }
                            else
                            {
                                //Temp 03/11/2022
                                /*GSTVendorInvoiceNo = string.Empty;
                                GSTVendorInvoiceDate = string.Empty;
                                GSTVendorInvoiceType = 0;
                                GSTVendorId = 0;
                                GSTInvoiceId = 0;*/
                            }
                        }
                        else
                        {
                            this.ShowMessageBox(result.Message);
                        }
                        //}
                    }
                }
            }

            if (!showvendor)
            {
                // MessageRender.ShowMessage("GST Amount is not available");
                MessageRender.ShowMessage(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.GST_AMOUNT_AVAI));
            }
        }

        /// <summary>
        /// On 06/02/2020, To show/Get Vendor GST Invoice details
        /// </summary>
        private void ShowSubLedgersVouchers()
        {
            if (AppSetting.IS_DIOMYS_DIOCESE && AppSetting.EnableSubLedgerVouchers == "1" && IsSubLedgerExists)
            {
                DateTime transdate = this.UtilityMember.DateSet.ToDate(dtTransactionDate.Text, false);
                string ledgername = gvTransaction.GetRowCellDisplayText(gvTransaction.FocusedRowHandle, colLedger).ToString();
                TransactionMode trandmode = (TransModeId == (int)Source.To ? TransactionMode.CR : TransactionMode.DR);

                if (dtSubLedgerVouchers.Rows.Count > 0 && LedgerId > 0)
                {
                    frmTransactionSubLedger frmSubLedgerVouchers = new frmTransactionSubLedger(VoucherType, ProjectId, VoucherId, LedgerId, trandmode,
                                                      transdate, LedgerAmount, ledgername, dtSubLedgerVouchers);
                    frmSubLedgerVouchers.ShowDialog();
                    if (frmSubLedgerVouchers.DialogResult == System.Windows.Forms.DialogResult.OK &&
                        frmSubLedgerVouchers.ReturnValue != null)
                    {
                        dtSubLedgerVouchers = frmSubLedgerVouchers.ReturnValue as DataTable;

                        //On 28/02/2020, to set sum of sub ledger amount to main ledger amount
                        gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colAmount, SubLedgerAmount);
                    }
                }
                else
                {
                    if (String.IsNullOrEmpty(ledgername))
                    {
                        //   MessageRender.ShowMessage("Sub Ledger(s) are not available for selected Ledger");
                        MessageRender.ShowMessage(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.SUB_LEDGER));
                    }
                    else
                    {
                        //  MessageRender.ShowMessage("Sub Ledger(s) are not avilable for selected Ledger '" + ledgername + "'");
                        MessageRender.ShowMessage("Sub Ledger(s) are not avilable for selected Ledger '" + ledgername + "'");
                    }
                }

                //FocusTransactionGrid();
            }
        }

        private void ShowVoucherFiles()
        {
            if (this.AppSetting.AttachVoucherFiles == 1)
            {
                frmAttachVoucherFiles frmimage = new frmAttachVoucherFiles(VoucherId, dtVoucherImages);
                if (frmimage.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (frmimage.ReturnValue != null)
                    {
                        dtVoucherImages = frmimage.ReturnValue as DataTable;
                    }
                }
            }
        }

        private void ShowCurrencyDetails()
        {
            int CountryId = (glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString()));
            lblDonorCurrency.Text = " ";  //string.Empty;
            txtExchangeRate.Text = "1.00";
            lblLiveExchangeRate.Text = "1.00";
            try
            {
                if (CountryId != 0)
                {
                    using (CountrySystem countrySystem = new CountrySystem())
                    {
                        ResultArgs result = countrySystem.FetchCountryCurrencyExchangeRateByCountryDate(CountryId, UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false));
                        if (result.Success)
                        {
                            lblDonorCurrency.Text = countrySystem.CurrencySymbol;

                            //On 22/08/2024, To Multi Currency property
                            if (this.AppSetting.AllowMultiCurrency == 1)
                            {
                                txtExchangeRate.Text = UtilityMember.NumberSet.ToNumber(countrySystem.ExchangeRate);
                                lblLiveExchangeRate.Text = UtilityMember.NumberSet.ToNumber(countrySystem.ExchangeRate);

                                // On 12/12/2024 - get Live exchange rate, if we received live exchange rate, let us have live exchange rate
                                AssignLiveExchangeRate();
                                if (rgTransactionType.SelectedIndex == 0 || rgTransactionType.SelectedIndex == 1)
                                {
                                    LoadCashBankLedger(rglkpCashLedger);
                                }
                                else
                                {
                                    txtCashAmount.NullText = string.Empty;
                                    LoadCashBankLedger(rglkpLedger);
                                    LoadCashBankLedger(rglkpCashLedger);
                                }

                                //txtExchangeRate.Enabled = false;
                                //colAmount.SummaryItem.DisplayFormat = lblDonorCurrency.Text.Replace("(", "").Replace(")", "") + " {0:n}";
                                //colCashAmount.SummaryItem.DisplayFormat = lblDonorCurrency.Text.Replace("(", "").Replace(")", "") + " {0:n}";
                            }
                        }
                    }

                    LoadPendingGSTInvoices();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// This method is used to bind leger reference details
        /// </summary>
        private ResultArgs BindVoucherLedgerReferenceDetails()
        {
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
                {
                    resultArgs = vouchersystem.FetchVoucherLedgerReferenceDetails(VoucherId);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }

            return resultArgs;
        }

        private void ShowReferenceNo()
        {
            DateTime vdate = this.UtilityMember.DateSet.ToDate(dtTransactionDate.Text, false);
            string ledname = gvTransaction.GetRowCellDisplayText(gvTransaction.FocusedRowHandle, colLedger).ToString();
            frmTransactionReference frmReference = new frmTransactionReference(ProjectId, VoucherId, LedgerId, vdate, LedgerAmount, ledname, dtReferenceVoucherNumber);
            frmReference.ShowDialog();
            if (frmReference.DialogResult == DialogResult.OK)
            {
                dtReferenceVoucherNumber = frmReference.VoucherLedgerReferenceDetails;
                double TotalReferenceAmount = frmReference.TotalAmount;

                //gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colREFAmount, TotalReferenceAmount);
                //if (dtReferenceVoucherNumber != null && dtReferenceVoucherNumber.Rows.Count > 0)
                //{

                //    DataView dvRowFilter = new DataView(dtReferenceVoucherNumber);
                //    dtReferenceVoucherNumber.DefaultView.ToTable(true,""
                //    dvRowFilter.RowFilter = "LEDGER_ID=" + LedgerId;
                //}

            }
            return;
        }

        private void FetchUpdateReferenceNo()
        {
            ResultArgs resultarg = BindVoucherLedgerReferenceDetails();
            if (resultarg.Success)
            {
                dtReferenceVoucherNumber = resultarg.DataSource.Table;
            }
        }

        //On 07/02/2020, to get sub Ledger Voucher for current voucher
        private void FetchSubLedgerVouchers()
        {
            if (AppSetting.IS_DIOMYS_DIOCESE && AppSetting.EnableSubLedgerVouchers == "1")
            {
                dtSubLedgerVouchers.Rows.Clear();
                ResultArgs resultArgs = new ResultArgs();
                try
                {
                    using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
                    {
                        if (AppSetting.IS_DIOMYS_DIOCESE && AppSetting.EnableSubLedgerBudget == "1")
                        {
                            DateTime vdate = this.UtilityMember.DateSet.ToDate(dtTransactionDate.Text, false);
                            string transmode = ""; //GetTransMode(); for Temp to take budget for CR/DR all ledger

                            resultArgs = vouchersystem.FetchVoucherLedgerSubLedgerVouchers(ProjectId, VoucherId, vdate, transmode);
                        }
                        else
                        {
                            resultArgs = vouchersystem.FetchVoucherLedgerSubLedgerVouchers(ProjectId, VoucherId);
                        }

                        if (resultArgs.Success && resultArgs.DataSource.Table != null)
                        {
                            dtSubLedgerVouchers = resultArgs.DataSource.Table;

                            //On 11/02/2020, for myssore, show only budgeted sub ledgers alone
                            if (AppSetting.IS_DIOMYS_DIOCESE && AppSetting.EnableSubLedgerBudget == "1")
                            {
                                dtSubLedgerVouchers.DefaultView.RowFilter = "IS_BUDGET IN (1)";
                                dtSubLedgerVouchers = dtSubLedgerVouchers.DefaultView.ToTable();
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
        }

        /// <summary>
        /// This method used to validate ref amout and its ledgers are correct in transaction table in grid
        /// 1. Check Ref_Date Validation with transaction date
        /// 2. Check Ledger changed and its amount are correct in transaction table in grid
        /// </summary>
        /// <returns></returns>
        private bool ValidateReferenceDetails()
        {
            bool Rtn = true;
            DataTable dtRefLedgers = dtReferenceVoucherNumber.DefaultView.ToTable(true, new string[] { "LEDGER_ID" });
            DataTable dtRefMinmumnDate = dtReferenceVoucherNumber.DefaultView.ToTable(true, new string[] { "REF_VOUCHER_DATE" });
            dtRefMinmumnDate.DefaultView.Sort = "REF_VOUCHER_DATE ASC";
            DataTable dtTransaction = gcTransaction.DataSource as DataTable;
            try
            {
                //1. Check Ref_Date Validation with transaction date
                //DataTable dtRefMinmumnDate = dtReferenceVoucherNumber.DefaultView.ToTable(true, new string[] { "REF_DATE" });
                //dtRefMinmumnDate.DefaultView.Sort = "REF_DATE ASC";

                //2. Check Ledger changed and its amount are correct in transaction table in grid
                foreach (DataRow drRefledger in dtRefLedgers.Rows)
                {
                    Int32 ref_LedgerId = this.UtilityMember.NumberSet.ToInteger(drRefledger["LEDGER_ID"].ToString());
                    double ref_Amount = this.UtilityMember.NumberSet.ToDouble(dtReferenceVoucherNumber.Compute("SUM(AMOUNT)", "LEDGER_ID=" + ref_LedgerId).ToString());
                    dtTransaction.DefaultView.RowFilter = "LEDGER_ID = " + ref_LedgerId;
                    Int32 LegId = this.UtilityMember.NumberSet.ToInteger(dtTransaction.Rows[0]["LEDGER_ID"].ToString());
                    DateTime dtRefDate = this.UtilityMember.DateSet.ToDate(dtReferenceVoucherNumber.Compute("MIN(REF_VOUCHER_DATE)", "LEDGER_ID=" + ref_LedgerId).ToString(), false);

                    if (dtTransaction.DefaultView.Count > 0)
                    {
                        double trans_Amount = this.UtilityMember.NumberSet.ToDouble(dtTransaction.DefaultView[0]["AMOUNT"].ToString());
                        if (trans_Amount != ref_Amount)
                        {
                            // this.ShowMessageBox("Transaction Amount is mismatched with Reference Amount, Check/Review payment reference details");
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.TRANSACTIONS_REF));
                            gvTransaction.FocusedColumn = colAmount;
                            gvTransaction.ShowEditor();
                            Rtn = false;
                            break;
                        }
                        else
                        {
                            //On 15/02/2018, Check reference amount with its balance amount 
                            if (!CheckReferenceAmountWithBalance(ref_LedgerId, dtRefDate, ref_Amount))
                            {
                                Rtn = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (VoucherId > 0)
                        {
                            // if (this.ShowConfirmationMessage("if you change the Ledger that referered Amount will be cleared. Do you want to proceed?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            if (this.ShowConfirmationMessage("if you change '" + GetLedgerName(ref_LedgerId) + "'  To  '" + GetLedgerName(LegId) + "'  Ledger that Referered Amount will be cleared. Do you want to proceed?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                //1. Remove existing ledger referce details
                                DataRow[] ledgerdetails = dtReferenceVoucherNumber.Select("LEDGER_ID" + " = " + ref_LedgerId);
                                if (ledgerdetails.Length > 0)
                                {
                                    foreach (DataRow dr in ledgerdetails)
                                    {
                                        dtReferenceVoucherNumber.Rows.Remove(dr);
                                    }
                                }
                            }
                            else
                            {
                                gvTransaction.FocusedColumn = colLedger;
                                Rtn = false;
                                break;
                            }
                        }
                        else
                        {
                            //1. Remove existing ledger referce details
                            DataRow[] ledgerdetails = dtReferenceVoucherNumber.Select("LEDGER_ID" + " = " + ref_LedgerId);
                            if (ledgerdetails.Length > 0)
                            {
                                foreach (DataRow dr in ledgerdetails)
                                {
                                    dtReferenceVoucherNumber.Rows.Remove(dr);
                                }
                            }
                        }
                    }

                    if (!(dtRefDate <= dtTransactionDate.DateTime) && (!(dtRefDate == DateTime.MinValue)))
                    {
                        if (this.ShowConfirmationMessage("Payment is made for Journal Reference Voucher, if you are tring to reduce the Date, it will be Clear. Do you Proceed?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            DataRow[] ledgerdetails = dtReferenceVoucherNumber.Select("LEDGER_ID" + " = " + ref_LedgerId);
                            if (ledgerdetails.Length > 0)
                            {
                                foreach (DataRow dr in ledgerdetails)
                                {
                                    dtReferenceVoucherNumber.Rows.Remove(dr);
                                }
                            }
                        }
                        else
                        {
                            dtTransactionDate.Focus();
                            Rtn = false;
                            break;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Rtn = false;
                MessageRender.ShowMessage(err.Message);
            }
            finally
            {
                dtTransaction.DefaultView.RowFilter = string.Empty;
            }
            return Rtn;
        }

        private string GetLedgerName(int LedId)
        {
            string LedgerName = string.Empty;
            using (LedgerSystem LedgerSystem = new LedgerSystem())
            {
                LedgerSystem.LedgerId = LedId;
                resultArgs = LedgerSystem.FetchLedgersByLedgerId();
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    LedgerName = resultArgs.DataSource.Table.Rows[0]["LEDGER"].ToString();
                }
            }

            return LedgerName;
        }

        /// <summary>
        /// On 15/02/2018, Check reference amount with its balance amount 
        /// </summary>
        /// <param name="Ref_Ledger_Id"></param>
        /// <param name="drRefDate"></param>
        /// <param name="ref_Amount"></param>
        /// <returns></returns>
        private bool CheckReferenceAmountWithBalance(Int32 Ref_Ledger_Id, DateTime dRefDate, double ref_Amount)
        {
            bool Rtn = false;
            using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
            {
                ResultArgs resultarg = vouchersystem.FetchReferenceBalances(ProjectId, VoucherId, Ref_Ledger_Id, dRefDate);
                if (resultarg.Success)
                {
                    DataTable dtRefBalances = resultarg.DataSource.Table;
                    if (dtRefBalances.Rows.Count > 0)
                    {
                        double refBalanceAmount = this.UtilityMember.NumberSet.ToDouble(dtRefBalances.Rows[0]["BALANCE"].ToString());
                        if (refBalanceAmount != ref_Amount)
                        {
                            this.ShowMessageBox("Reference Amount is mismatched with its Balance Amount, Check/Review payment reference details.");
                            gvTransaction.FocusedColumn = colAmount;
                            gvTransaction.ShowEditor();
                            Rtn = false;
                        }
                        else
                        { Rtn = true; }

                    }
                    else  // done on 14.03.2017
                    {
                        Rtn = true;
                    }

                }
                else
                {
                    this.ShowMessageBox(resultarg.Message);
                    Rtn = false;
                }
            }
            return Rtn;
        }

        /// <summary>
        /// On 20/11/2018, 
        /// </summary>
        /// <returns></returns>
        private bool ValidateCCAmoutWithLedgerAmount()
        {
            bool Rtn = true;

            try
            {
                DataTable dtTransaction = gcTransaction.DataSource as DataTable;
                if (dsCostCentre != null && dsCostCentre.Tables.Count > 0)
                {
                    if (dtTransaction != null && dtTransaction.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtTransaction.Rows)
                        {
                            int rownumber = dtTransaction.Rows.IndexOf(dr);
                            Int32 ledgerid = UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                            //1. Take leger details in transaction grid
                            if (ledgerid > 0)
                            {
                                string ledgername = GetLedgerName(ledgerid);
                                double ledgeramount = UtilityMember.NumberSet.ToDouble(dr["AMOUNT"].ToString());
                                string CCTableName = rownumber + "LDR" + ledgerid;

                                //2. Compare ledger amount with sum of cc amount for given ledger
                                if (dsCostCentre.Tables.Contains(CCTableName))
                                {
                                    DataTable dtLedgerCC = dsCostCentre.Tables[CCTableName];
                                    if (dtLedgerCC != null && dtLedgerCC.Rows.Count > 0)
                                    {
                                        dtLedgerCC.DefaultView.RowFilter = "COST_CENTRE_ID >0";
                                        if (dtLedgerCC.DefaultView.Count > 0)
                                        {
                                            dtLedgerCC.DefaultView.RowFilter = "";
                                            double CCAmount = UtilityMember.NumberSet.ToDouble(dtLedgerCC.Compute("SUM(AMOUNT)", string.Empty).ToString());

                                            // this is to enable add the Cost Centre Amount 12.07.2019
                                            //double CCamountGSt = CCAmount - ledgeramount;
                                            //On 19/01/2022, To allocate cc amount with GST or withour GST based on Finnace setting
                                            double ledgerGSTamount = 0;
                                            if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableGST).Equals((int)YesNo.Yes))
                                            {
                                                if (dtTransactionDate.DateTime >= this.AppSetting.GSTStartDate)
                                                {
                                                    if (AppSetting.AllocateCCAmountWithGST == 1 && dtTransaction.Columns.Contains("GST"))
                                                    {
                                                        ledgerGSTamount = UtilityMember.NumberSet.ToDouble(dr["GST"].ToString()); ;
                                                    }
                                                }
                                            }
                                            double CCamountGST = CCAmount - (ledgeramount + ledgerGSTamount);

                                            //On 09/11/2021, To check cc amount mis matching
                                            //if (CCAmount != (ledgeramount + CCamountGSt))
                                            if (CCamountGST != 0)
                                            {
                                                // MessageRender.ShowMessage("'" + ledgername + "' ledger amount is mismatching with Cost Centre allocation amount, Check Cost Centre allocation details.");
                                                MessageRender.ShowMessage("'" + ledgername + "'" + this.GetMessage(MessageCatalog.Transaction.VocherTransaction.LEDGER_CC_ALLOCATIONS));
                                                Rtn = false;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception err)
            {
                Rtn = false;
                // MessageRender.ShowMessage("Could not check Cost center amount with ledger amount, " + err.Message);
                MessageRender.ShowMessage(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.CC_LEDGER_AMOUNT) + err.Message);
            }

            return Rtn;
        }


        /// <summary>
        /// On 13/12/2022, To check GST Invoice Master and GST Master Ledger details
        /// </summary>
        /// <returns></returns>
        private bool ValidateGSTInvoiceDetails()
        {
            bool Rtn = true;
            string msg = string.Empty;
            try
            {
                DataTable dtTransaction = gcTransaction.DataSource as DataTable;
                using (VoucherTransactionSystem sysvoucher = new VoucherTransactionSystem())
                {
                    if (!string.IsNullOrEmpty(GSTVendorInvoiceNo))
                    {
                        if (DtGSTInvoiceMasterLedgerDetails == null) DtGSTInvoiceMasterLedgerDetails = sysvoucher.AppSchema.GSTInvoiceMasterLedgerDetails.DefaultView.ToTable();
                        DataTable dtGSTLegersTrans = dtTransaction.DefaultView.ToTable();

                        //On 20/10/2023 Allow GST Invoice always without GST Amount
                        //dtGSTLegersTrans.DefaultView.RowFilter = sysvoucher.AppSchema.VoucherTransaction.CGSTColumn.ColumnName + " > 0";
                        dtGSTLegersTrans.DefaultView.RowFilter = sysvoucher.AppSchema.VoucherTransaction.LEDGER_GST_CLASS_IDColumn.ColumnName + " > 0 AND " +
                                                                sysvoucher.AppSchema.Ledger.IS_GST_LEDGERSColumn.ColumnName + " = 1";
                        dtGSTLegersTrans = dtGSTLegersTrans.DefaultView.ToTable();
                        if (DtGSTInvoiceMasterDetails != null && DtGSTInvoiceMasterLedgerDetails.Rows.Count > 0 && !string.IsNullOrEmpty(GSTVendorInvoiceNo))
                        {
                            foreach (DataRow dr in dtGSTLegersTrans.Rows)
                            {
                                int rownumber = dtTransaction.Rows.IndexOf(dr);
                                Int32 ledgerid = UtilityMember.NumberSet.ToInteger(dr[sysvoucher.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString());
                                string ledgername = GetLedgerName(ledgerid);
                                DtGSTInvoiceMasterLedgerDetails.DefaultView.RowFilter = sysvoucher.AppSchema.Ledger.LEDGER_IDColumn.ColumnName + " = " + ledgerid;
                                if (DtGSTInvoiceMasterLedgerDetails.DefaultView.Count == 0)
                                {
                                    // msg = "'" + ledgername + "' is not available in the GST Invoice details, Click \"Attach Vendor GST Invoice\" and Check details.";
                                    msg = "'" + ledgername + "'" + this.GetMessage(MessageCatalog.Transaction.VocherTransaction.GST_ATTACH);
                                    Rtn = false;
                                    break;
                                }
                            }
                        }

                        if (lcVendor.Visibility == LayoutVisibility.Always)
                        {
                            if (Rtn && DtGSTInvoiceMasterDetails == null && dtGSTLegersTrans.Rows.Count > 0)
                            {
                                // msg = "Ledger(s) are mismatching in the GST Invoice details, Click on \"Attach Vendor GST Invoice\" and Check details.";
                                msg = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.LEDGER_GST_INV);
                                Rtn = false;
                            }
                            if (Rtn && DtGSTInvoiceMasterDetails != null && DtGSTInvoiceMasterLedgerDetails.Rows.Count != dtGSTLegersTrans.Rows.Count)
                            {
                                //If Voucher GST Ledger is removed from Voucher grid list
                                // msg = "Ledger(s) are mismatching in the GST Invoice details, Click on \"Attach Vendor GST Invoice\" and Check details.";
                                msg = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.LEDGER_GST_INV);
                                Rtn = false;
                            }
                            else if (Rtn && DtGSTInvoiceMasterDetails != null && DtGSTInvoiceMasterLedgerDetails.Rows.Count > 0)
                            {
                                //On 30/10/2023, Check GST mismatching amount vouhcer trans and gst invoice
                                string fitlergstamtinvoice = "SUM(" + sysvoucher.AppSchema.GSTInvoiceMaster.TOTAL_CGST_AMOUNTColumn.ColumnName + ")";
                                double cgstamtinvoice = UtilityMember.NumberSet.ToDouble(DtGSTInvoiceMasterDetails.Compute(fitlergstamtinvoice, string.Empty).ToString());
                                fitlergstamtinvoice = "SUM(" + sysvoucher.AppSchema.GSTInvoiceMaster.TOTAL_SGST_AMOUNTColumn.ColumnName + ")";
                                double sgstamtinvoice = UtilityMember.NumberSet.ToDouble(DtGSTInvoiceMasterDetails.Compute(fitlergstamtinvoice, string.Empty).ToString());
                                fitlergstamtinvoice = "SUM(" + sysvoucher.AppSchema.GSTInvoiceMaster.TOTAL_IGST_AMOUNTColumn.ColumnName + ")";
                                double igstamtinvoice = UtilityMember.NumberSet.ToDouble(DtGSTInvoiceMasterDetails.Compute(fitlergstamtinvoice, string.Empty).ToString());
                                double gstamtinvoice = (cgstamtinvoice + sgstamtinvoice + igstamtinvoice);

                                if (UpdateGST != gstamtinvoice)
                                {
                                    msg = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.LEDGER_GST_INV);
                                    Rtn = false;
                                }
                            }
                        }

                        /*if (Rtn && DtGSTInvoiceMasterDetails != null )
                        {
                            string[] rtn = new string[] { sysvoucher.AppSchema.Ledger.LEDGER_IDColumn.ColumnName, 
                                                    sysvoucher.AppSchema.VoucherTransaction.LEDGER_GST_CLASS_IDColumn.ColumnName };
                            DataTable dt= dtGSTLegersTrans.DefaultView.ToTable(true, rtn);
                            if (DtGSTInvoiceMasterLedgerDetails.Rows.Count != dt.Rows.Count)
                            {
                                //If Voucher GST Ledger is removed from Voucher grid list
                                msg = "Ledger(s) are mismatching in the GST Invoice details, Click on \"Attach Vendor GST Invoice\" and Check details.";
                                Rtn = false;
                            }
                        }*/
                    }
                }
                if (!Rtn)
                {
                    MessageRender.ShowMessage(msg);
                }
            }
            catch (Exception err)
            {
                Rtn = false;
                MessageRender.ShowMessage("Could validate GST Invoice details, " + err.Message);
            }

            return Rtn;
        }

        private bool ValidateRPLedgerAmountWithGSTInvoice()
        {
            bool rtn = true;
            try
            {
                DefaultVoucherTypes vtype = (rgTransactionType.SelectedIndex == 1 ? DefaultVoucherTypes.Payment : DefaultVoucherTypes.Receipt);
                DataTable dtTransaction = gcTransaction.DataSource as DataTable;
                using (VoucherTransactionSystem transSystem = new VoucherTransactionSystem())
                {
                    ResultArgs result = transSystem.ValidateGSTInvoiceLedgers(vtype, VoucherId, ProjectId, BookingGSTInvoiceId, dtTransaction);
                    if (!result.Success)
                    {
                        rtn = false;
                        this.ShowMessageBox(result.Message);
                    }
                    else
                    {
                        rtn = true;
                    }
                }
            }
            catch (Exception err)
            {
                rtn = false;
                this.ShowMessageBox(err.Message);
            }
            return rtn;
        }

        private bool isExistInvoiceNo()
        {
            int counts = 0;
            if (!string.IsNullOrEmpty(GSTVendorInvoiceNo))
            {
                using (VoucherTransactionSystem transSystem = new VoucherTransactionSystem())
                {
                    counts = transSystem.IsExistsGSTInvoceNno(GSTVendorInvoiceNo.Trim(), VoucherId);
                }
            }
            return (counts == 0);
        }
        #endregion

        #region GridRefresh
        private void GridRefreshAfterFinanceSettingChanged()
        {
            SINGLE_ENTRY = "<b>" + this.GetMessage(MessageCatalog.Transaction.VocherTransaction.PRESS) + "<color=blue>F11</color>" + this.GetMessage(MessageCatalog.Transaction.VocherTransaction.TO_SINGLE_ENTRY) + "</b>";
            DOUBLE_ENTRY = "<b>" + this.GetMessage(MessageCatalog.Transaction.VocherTransaction.PRESS) + " <color=blue>F11</color> " + this.GetMessage(MessageCatalog.Transaction.VocherTransaction.TO_DOUBLE_ENTRY) + "</b>";

            VoucherId = 0;
            ClearControls();
            if (TransVoucherMethod == (int)TransactionVoucherMethod.Automatic && VoucherId == 0)
            {
                LoadVoucherNo();
            }
            else if (TransVoucherMethod == (int)TransactionVoucherMethod.Manual && VoucherId == 0)
            {
                txtVoucher.Text = string.Empty;
            }

            SetDefaults();

            if (VoucherId == 0)
            {
                TransEntryMethod = this.AppSetting.TransEntryMethod == "1" ? VoucherEntryMethod.Single : VoucherEntryMethod.Multi;
            }
            else
            {
                TransEntryMethod = (this.AppSetting.TransEntryMethod == "2" || gvTransaction.RowCount > 1) ? VoucherEntryMethod.Multi : VoucherEntryMethod.Single;
            }
            GetUserControlInput();
            ClearTableValues();
            FetchDateDuration();
            if (VoucherId != 0)
            {
                ucRightShortcut.DisableProject = BarItemVisibility.Never;
            }
            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                ApplyRights();
                ApplyRightsForShortCutKeys();
                ApplyUserRightsForDeletion();
            }
        }
        #endregion

        private void btnRemoveVendorGSTInvoce_Click(object sender, EventArgs e)
        {
            //if (this.ShowConfirmationMessage("Are you sure to Remove Vendor GST Invoice details ?", MessageBoxButtons.OKCancel,
            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.REM_GST_INVOICE), MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
                {
                    ResultArgs result = vouchersystem.RemoveGSTVendorInvoiceDetailsById(VoucherId, GSTInvoiceId);
                    if (result.Success)
                    {
                        GSTVendorInvoiceNo = string.Empty;
                        GSTVendorInvoiceDate = string.Empty;
                        GSTVendorInvoiceType = 0;
                        GSTVendorId = 0;
                        GSTInvoiceId = 0;
                        DtGSTInvoiceMasterDetails = null;
                        DtGSTInvoiceMasterLedgerDetails = null;
                        ShowAdditionButtons(AdditionButttons.VendorGSTInvoiceDetails, true);
                        //  this.ShowMessageBox("GST Invoice details is removed");
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.GST_INVOICE_REM));
                    }
                }
            }
        }

        private void lkpGSTInvoices_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Close)
            {
                lkpGSTInvoices.EditValue = 0;
                lkpGSTInvoices.Properties.ImmediatePopup = true;
            }
        }

        private void lkpGSTInvoices_EditValueChanged(object sender, EventArgs e)
        {
            lblGSTInvoiceTotalAmount.Text = "0.00";
            lblGSTInvoiceBalance.Text = "0.00";
            if (lkpGSTInvoices.GetSelectedDataRow() != null)
            {
                using (VoucherSystem vsystem = new VoucherSystem())
                {
                    DataRowView drv = lkpGSTInvoices.GetSelectedDataRow() as DataRowView;
                    lblGSTInvoiceTotalAmount.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(drv[vsystem.AppSchema.GSTInvoiceMaster.AMOUNTColumn.ColumnName].ToString()));
                    lblGSTInvoiceBalance.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(drv[vsystem.AppSchema.GSTInvoiceMaster.BALANCEColumn.ColumnName].ToString()));
                }
            }
        }

        private void ucAdditionalInfo_VoucherBillsClicked(object sender, EventArgs e)
        {
            //ShowVoucherImages();
        }

        private void btnAttachVocuherImages_Click(object sender, EventArgs e)
        {
            ShowVoucherFiles();
        }

        private void lgGSTInvoce_Click(object sender, EventArgs e)
        {

        }

        private void flypnlDonorInfo_Showing(object sender, DevExpress.Utils.FlyoutPanelEventArgs e)
        {
            //(sender as DevExpress.Utils.FlyoutPanel).Margin.Left = 10;
        }

        private void glkpCurrencyCountry_EditValueChanged(object sender, EventArgs e)
        {
            ShowCurrencyDetails();

            //12/11/2024
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                ApplyVoucherCurrencyToLedgers();
            }
        }

        //private void txtNameAddress_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Shift && e.KeyCode == Keys.Tab) { FocusCashTransactionGrid(); e.SuppressKeyPress = true; }
        //    else if (e.KeyCode == Keys.Tab) { e.SuppressKeyPress = true; }  //{ txtPanNumber.Select(); txtPanNumber.Focus(); e.SuppressKeyPress = true; }
        //}

        //private void txtPanNumber_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Shift && e.KeyCode == Keys.Tab) { FocusCashTransactionGrid(); e.SuppressKeyPress = true; }
        //    else if (e.KeyCode == Keys.Tab) { e.SuppressKeyPress = true; }  // txtGSTNumber.Select(); txtGSTNumber.Focus(); 
        //}
    }
}
