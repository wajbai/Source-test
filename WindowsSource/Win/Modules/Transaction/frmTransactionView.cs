using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AcMEDSync.Model;
using ACPP.Modules.Master;
using ACPP.Modules.UIControls;
using Bosco.DAO.Schema;
using Bosco.Model.Business;
using Bosco.Model.Transaction;
using Bosco.Model.UIModel;
using Bosco.Model.UIModel.Master;
using Bosco.Utility;
using Bosco.Utility.CommonMemberSet;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using PAYROLL.Modules;
using ACPP.Modules.Inventory.Stock;
using System.Linq;

using ACPP.Modules.Asset;
using ACPP.Modules.Asset.Transactions;
using PAYROLL.Modules.Payroll_app;
using Bosco.Model.Inventory.Stock;
using Bosco.Model;
using ACPP.Modules.Data_Utility;
using Bosco.Report.Base;
using DevExpress.XtraGrid.Views.Grid.Customization;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Popup;
using DevExpress.Utils.Win;
using DevExpress.Utils;



namespace ACPP.Modules.Transaction
{
    public partial class frmTransactionView : frmFinanceBase
    {
        #region Variable Decelaration

        public event EventHandler EditHeld;
        ResultArgs resultArgs = null;
        Timer timer = new Timer();
        private int VoucherMaster = 0;
        private int VoucherTrans = 0;
        private int RowIndex = 0;
        private string CashTransMode = "";
        private string BankTransMode = "";
        private string CashTransModeOpen = "";
        private string BankTransModeOpen = "";
        public bool IsDateLoaded = false;
        private bool frmloaded = false;
        
        public DataTable dtVoucherMasterDetails = new DataTable("Master");
        public DataTable dtVoucherDetails = new DataTable("Details");
        public DataTable dtVoucherCostcentreDetails = new DataTable("Costcentre");

        //public const string TRANSACTION_RECEIPTS = ;
        //private const string TRANSACTION_PAYMENTS = "Transaction - Payment";
        //private const string TRANSACTION_CONTRA = "Transaction - Contra";

        private List<Int32> zoomsize = new List<Int32> { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 72 };
        private Single defaultFontSize;
        #endregion

        #region Properties
        private int ledgerId;
        private int LedgerId
        {
            set
            {
                ledgerId = value;
            }
            get
            {
                ledgerId = gvVoucher.GetFocusedRowCellValue(colLedgerId) != null ? this.UtilityMember.NumberSet.ToInteger(gvVoucher.GetFocusedRowCellValue(colLedgerId).ToString()) : 0;
                return ledgerId;
            }
        }

        private int costcentreledgerId;
        private int CostCentreLedgerId
        {
            set
            {
                costcentreledgerId = value;
            }
            get
            {
                costcentreledgerId = gvLedgerDetails.GetFocusedRowCellValue(ggcolLedgerId) != null ?
                    this.UtilityMember.NumberSet.ToInteger(gvLedgerDetails.GetFocusedRowCellValue(ggcolLedgerId).ToString()) : 0;
                return costcentreledgerId;
            }
        }

        private int ledgersequenceNo;
        private int LedgerSequenceNo
        {
            get
            {
                ledgersequenceNo = gvLedgerDetails.GetFocusedRowCellValue(ggcolLedgerSequenceNo) != null ?
                    this.UtilityMember.NumberSet.ToInteger(gvLedgerDetails.GetFocusedRowCellValue(ggcolLedgerSequenceNo).ToString()) : 0;
                return ledgersequenceNo;
            }
        }

        private int projectId = 0;
        public int ProjectId
        {
            get
            {
                return projectId;
            }
            set
            {
                projectId = value;
            }
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

        private string projectName = string.Empty;
        public string ProjectName
        {
            set
            {
                projectName = value;
            }
            get
            {
                return projectName;
            }
        }

        private int voucherIndex = 0;
        public int VoucherIndex
        {
            set
            {
                voucherIndex = value;
            }
            get
            {
                return voucherIndex;
            }
        }

        private int transSelectionType = 0;
        private int TransSelectionType
        {
            set
            {
                transSelectionType = value;
            }
            get
            {
                return transSelectionType;
            }
        }

        private string vouchertype = "";
        public string Vouchertype
        {
            get
            {
                vouchertype = gvTransaction.GetFocusedRowCellValue(colVoucherMode) != null ? gvTransaction.GetFocusedRowCellValue(colVoucherMode).ToString() : "";
                return vouchertype;
            }
        }

        private Int32 voucherdefinitionid = 0;
        public Int32 VoucherDefinitionId
        {
            get
            {
                voucherdefinitionid = this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetFocusedRowCellValue(colVoucherDefinitionId) != null ? gvTransaction.GetFocusedRowCellValue(colVoucherDefinitionId).ToString() : "0");
                return voucherdefinitionid;
            }
        }

        //public DevExpress.XtraEditors.PopupContainerControl AssignFDRealization
        //{
        //    set { popupceFDRealization.Properties.PopupControl = value; }
        //}

        //private DateTime SelectedVoucherDate;
        public DateTime dtSelectedVoucherDate
        {
            get
            {
                return gvTransaction.GetFocusedRowCellValue(colVoucherDate) != null ? this.UtilityMember.DateSet.ToDate(gvTransaction.GetFocusedRowCellValue(colVoucherDate).ToString(), false) : DateTime.MinValue;
            }
        }

        //private DateTime dtLockDateFrom { get; set; }
        //private DateTime dtLockDateTo { get; set; }
        private DataTable dtAuditLockDetails = new DataTable();

        #endregion

        #region Constructor
        public frmTransactionView()
        {
            InitializeComponent();
        }

        public frmTransactionView(string recVoucherDate, int proId, string pro, int frmTransactionIndex, int transSelection)
            : this()
        {
            ProjectId = proId;
            ProjectName = pro;
            VoucherIndex = frmTransactionIndex;
            TransSelectionType = transSelection;
            RecentVoucherDate = recVoucherDate;
            if (frmTransactionIndex == 0)
            {
                this.Text = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_TRANSACTION_RECEIPT);
                chkReceipt.Checked = true;
                this.enumUserRights.Add(Receipt.CreateReceiptVoucher);
                this.enumUserRights.Add(Receipt.EditReceiptVoucher);
                this.enumUserRights.Add(Receipt.DeleteReceiptVoucher);
                this.enumUserRights.Add(Receipt.MoveReceiptVoucher);
                this.enumUserRights.Add(Receipt.PrintReceiptVoucher);
                this.enumUserRights.Add(Receipt.ViewReceiptVoucher);
                this.enumUserRights.Add(Receipt.InsertReceiptVoucher);
                this.enumUserRights.Add(Receipt.ShowReceiptNagativeBalance);
                this.ApplyUserRights(ucTrans, this.enumUserRights, (int)Menus.Receipt);
                if (!this.LoginUser.IsFullRightsReservedUser)
                {
                    //chkPayments.Enabled = chkContra.Enabled = false;
                    EnableCheckBox();
                }
            }
            else if (frmTransactionIndex == 1)
            {
                this.Text = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_TRANSACTION_PAYMENT);
                chkPayments.Checked = true;
                this.enumUserRights.Add(Payment.CreatePaymentVoucher);
                this.enumUserRights.Add(Payment.EditPaymentVoucher);
                this.enumUserRights.Add(Payment.DeletePaymentVoucher);
                this.enumUserRights.Add(Payment.MovePaymentVoucher);
                this.enumUserRights.Add(Payment.PrintPaymentVoucher);
                this.enumUserRights.Add(Payment.ViewPaymentVoucher);
                this.enumUserRights.Add(Payment.InsertPaymentVoucher);
                this.enumUserRights.Add(Payment.ShowPaymentNagativeBalance);
                this.ApplyUserRights(ucTrans, this.enumUserRights, (int)Menus.Payments);
                if (!this.LoginUser.IsFullRightsReservedUser)
                {
                    // chkReceipt.Enabled = chkContra.Enabled = false;
                    EnableCheckBox();
                }
            }
            else
            {
                this.Text = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_TRANSACTION_CONTRA);
                chkContra.Checked = true;
                this.enumUserRights.Add(Contra.CreateContraVoucher);
                this.enumUserRights.Add(Contra.EditContraVoucher);
                this.enumUserRights.Add(Contra.DeleteContraVoucher);
                this.enumUserRights.Add(Contra.MoveContraVoucher);
                this.enumUserRights.Add(Contra.PrintContraVoucher);
                this.enumUserRights.Add(Contra.ViewContraVoucher);
                this.enumUserRights.Add(Contra.InsertContraVoucher);
                this.enumUserRights.Add(Contra.ShowContraNagativeBalance);
                this.ApplyUserRights(ucTrans, this.enumUserRights, (int)Menus.Contra);
                if (!this.LoginUser.IsFullRightsReservedUser)
                {
                    //chkPayments.Enabled = chkReceipt.Enabled = false;
                    EnableCheckBox();
                }
            }
        }


        #endregion

        #region Properties
        public int SetFDAccountId { get { return gvTransaction.GetFocusedRowCellValue(colFDVoucherId) != null ? this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetFocusedRowCellValue(colFDVoucherId).ToString()) : 0; } }

        public int VoucherMasterId
        {
            get
            {
                // RowIndex = gvTransaction.FocusedRowHandle;
                VoucherMaster = gvTransaction.GetFocusedRowCellValue(colVoucherID) != null ? this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetFocusedRowCellValue(colVoucherID).ToString()) : 0;
                return VoucherMaster;
            }
            set
            {
                VoucherMaster = value;
            }
        }

        private int VoucherTransId
        {
            get
            {
                //RowIndex = gvVoucher.FocusedRowHandle;
                VoucherTrans = this.UtilityMember.NumberSet.ToInteger(gvVoucher.GetFocusedRowCellValue(colVoucherTransID).ToString());
                return VoucherTrans;
            }
            set
            {
                VoucherTrans = value;
            }
        }

        private string VoucherSubType { get; set; }
        #endregion

        #region Events
        private void frmTransactionView_Load(object sender, EventArgs e)
        {
            
            // StartTimer();

            //DateTime dtyearfrom = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            //DateTime dtbookbeginingfrom = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false);
            //if (dtyearfrom > dtbookbeginingfrom)
            //{
            //    deDateFrom.DateTime = dtyearfrom;
            //}
            //else
            //{
            //    deDateFrom.DateTime = dtbookbeginingfrom;
            //}
            //this.SetFilterMode(glkpProject);
            //  deDateFrom.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom,false);

            //Added by Carmel Raj
            //LoadDefaultValues();
            // Check_negativeBalance();
            //ShowNegative();

            deDateFrom.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deDateFrom.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            deDateTo.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deDateTo.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            deDateFrom.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deDateFrom.DateTime = UtilityMember.DateSet.ToDate(RecentVoucherDate.ToString(), false);
            DateTime DateFrom = new DateTime(deDateFrom.DateTime.Year, deDateFrom.DateTime.Month, 1);
            deDateFrom.DateTime = DateFrom;
            deDateTo.DateTime = deDateFrom.DateTime.AddMonths(1).AddDays(-1);

            //if Networking is enabled, Visible the coloumns for Bosconet _Delhi Client_......chinna
            if (Bosco.Utility.ConfigSetting.SettingProperty.EnableNetworking == true)
            {
                colRegNO.Visible = true;
                colRegistrationType.Visible = true;
                colRefferredStaff.Visible = true;
            }
            else
            {
                colRegNO.Visible = false;
                colRegistrationType.Visible = false;
                colRefferredStaff.Visible = false;
            }
            this.rbtnDuplicateVoucher.Buttons[0].ToolTip = "To make Replicate Voucher (Alt+U)";
            this.rbtnPrintVoucher.Buttons[0].ToolTip = "To Print Voucher (Ctl+P)";
            this.rbtnGSTInvoicePrint.Buttons[0].ToolTip = "To Print GST Invoice";

            colPrint.ToolTip = "To Print Voucher (Ctl+P)";
            colDuplicate.ToolTip = "To make Replicate Voucher (Alt+U)";
            colPrintGSTInvoice.ToolTip = "To Print GST Invoice";

            ShowHideColumnsBasedOnSetting();
            this.AttachGridContextMenu(gcTransaction);
            this.AttachGridContextMenu(gcLedgerDetails);
            //SetToggleColor(togShowLedgerCCDetails.IsOn);
            //SetToggleColor(togShowOPBalance.IsOn); 
            //SetToggleColor(togShowCLBalance.IsOn); 

            if (this.AppSetting.AttachVoucherFiles == 1)
            {
                //GridViewFooterExtender extender = new GridViewFooterExtender(gvTransaction);
                //extender.addButtonToFooter("View Voucher Files", new Size(100, 25), null, ImageLocation.MiddleLeft, btnViewVoucherFiles_Click);
                SuperToolTip sTooltip1 = new SuperToolTip();
                ToolTipTitleItem titleItem1 = new ToolTipTitleItem();
                titleItem1.Text = "Click to view attached Voucher file(s)";
                sTooltip1.Items.Add(titleItem1);

                ucTrans.ChangePostInterestCaption = "View Voucher Files";
                ucTrans.VisiblePostInterest = BarItemVisibility.Always;
                ucTrans.ChangePostInterestSuperToolTip = sTooltip1;
            }

            zoomctl.EditValue = 8;// zoomsize.IndexOf(Convert.ToInt32(defaultFontSize));
        }

        private void LoadDefaultValues()
        {
            //On 13/08/2024, Apply Project Currency Setting
            /*this.ApplyProjectCurrencySetting(ProjectId);
            rtxtCredit.Mask.Culture = System.Threading.Thread.CurrentThread.CurrentUICulture;
            rtxtDebit.Mask.Culture = System.Threading.Thread.CurrentThread.CurrentUICulture;*/

            SetVisibileShortCuts(true, true, true);

            LoadProject();
            LoadVoucherDetails();
            gvTransaction.FocusedRowHandle = 0;
            gvVoucher.OptionsView.ShowFooter = true;
            glkpProject.EditValue = ProjectId;
            // command by sala
            //   if (TransSelectionType == 0) { ShowTransactionForm(); }
            ucTrans.VisibleNegativeBalance = BarItemVisibility.Always;
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                ucTrans.VisibleNegativeBalance = BarItemVisibility.Never;
            }
            FetchDateDuration();

            //On 13/08/2024, Reset to Global Currency Setting
            //this.ApplyGlobalSetting();
        }

        //private void Check_negativeBalance()
        //{
        //    if (CashTransMode == TransactionMode.CR.ToString() || BankTransMode == TransactionMode.CR.ToString())
        //        timer.Tick += new EventHandler(timer_Tick);
        //    else
        //        timer.Tick -= timer_Tick;
        //    ucTrans.lblNegativeBalance.Visible = false;
        //}

        //private void StartTimer()
        //{
        //    timer.Interval = 500;
        //    timer.Enabled = false;
        //    timer.Start();
        //}

        //void timer_Tick(object sender, EventArgs e)
        //{
        //    //ucTransaction.lblNegativeBalance.Visible = true;
        //    //ucTrans.lblNegativeBalance.Visible = true;
        //    //if (CashTransMode == TransactionMode.CR.ToString() && BankTransMode == TransactionMode.CR.ToString())
        //    //{
        //    //    //ucTrans.lblNegativeBalance.Text = "Negative Balance in Cash and Bank";
        //    //    ucTrans.lblNegativeBalance.Text = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_NEGATIVE_BALANCE_CASHBANK);
        //    //}
        //    //else if (CashTransMode == TransactionMode.CR.ToString())
        //    //    ucTrans.lblNegativeBalance.Text = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_NEGATIVE_BALANCE_CASH);
        //    //else
        //    //    ucTrans.lblNegativeBalance.Text = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_NEGATIVE_BALANCE_BANK);
        //    if (CashTransMode == TransactionMode.CR.ToString())
        //    {
        //        if ((lblCashClosingAmt.AppearanceItemCaption != null))
        //        {
        //            if (lblCashClosingAmt.AppearanceItemCaption.ForeColor == Color.Blue)
        //                lblCashClosingAmt.AppearanceItemCaption.ForeColor = Color.Red;
        //            else
        //                lblCashClosingAmt.AppearanceItemCaption.ForeColor = Color.Blue;
        //        }
        //    }
        //    if (BankTransMode == TransactionMode.CR.ToString())
        //    {
        //        if (lblBankClosingAmt.AppearanceItemCaption != null)
        //        {
        //            if (lblBankClosingAmt.AppearanceItemCaption.ForeColor == Color.Blue)
        //                lblBankClosingAmt.AppearanceItemCaption.ForeColor = Color.Red;
        //            else
        //                lblBankClosingAmt.AppearanceItemCaption.ForeColor = Color.Blue;
        //        }
        //    }
        //}



        private void ucTrans_AddClicked(object sender, EventArgs e)
        {
            ShowTransactionForm();
        }

        private void ucTrans_DeleteClicked(object sender, EventArgs e)
        {
            VoucherSubType = gvTransaction.GetFocusedRowCellValue(colVoucherSubType) != null ? gvTransaction.GetFocusedRowCellValue(colVoucherSubType).ToString() : string.Empty;
            DeleteVoucherDetails();
        }

        private void ucTrans_EditClicked(object sender, EventArgs e)
        {
            ShowVoucherMasterForm();
        }

        private void ucTrans_PrintClicked(object sender, EventArgs e)
        {
            string reportTitle = "Transaction";
            PrintGridViewDetails(gcTransaction, reportTitle, PrintType.DS, gvTransaction, true);
        }

        private void ucTrans_RefreshClicked(object sender, EventArgs e)
        {
            //StartTimer();
            LoadProject();
            ProjectId = glkpProject.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
            // glkpProject.EditValue = glkpProject.Properties.GetKeyValue(ProjectId);
            LoadVoucherDetails();
            // ShowNegative();
            ucTransViewOpeningBalDetails.ShowOpNegative();
            ucTransviewClosingBalance.ShowClosingBalNegative();
            FetchDateDuration();
            //Check_negativeBalance();
        }

        private void ucTrans_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvTransaction.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvTransaction, colVoucherDate);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            //On 13/08/2024, Apply Project Currency Setting
            /*this.ApplyProjectCurrencySetting(ProjectId);
            rtxtCredit.Mask.Culture = System.Threading.Thread.CurrentThread.CurrentUICulture;
            rtxtDebit.Mask.Culture = System.Threading.Thread.CurrentThread.CurrentUICulture;*/
            
            dtVoucherCostcentreDetails = dtVoucherDetails = dtVoucherMasterDetails = null;
            this.ShowWaitDialog(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.LOADING_VOUCHERS));
            try
            {
                ProjectName = (glkpProject.EditValue != null ? glkpProject.Text : string.Empty);

                if (deDateFrom.DateTime > deDateTo.DateTime)
                {
                    DateTime dateTo = deDateTo.DateTime;
                    deDateTo.DateTime = deDateFrom.DateTime;
                    deDateFrom.DateTime = dateTo.Date;
                }
                if (glkpProject.EditValue != null)
                {
                    ucTransViewOpeningBalDetails.ProjectId = ProjectId;
                    LoadVoucherDetails();
                    ucTransViewOpeningBalDetails.ShowOpNegative();
                    ucTransviewClosingBalance.ShowClosingBalNegative();
                    FetchDateDuration();
                }

            }
            catch (Exception ex)
            {
                CloseWaitDialog();
                MessageRender.ShowMessage(ex.Message);
            }
            finally
            {
                //On 13/08/2024, Reset to Global Currency Setting
                //this.ApplyGlobalSetting();
                CloseWaitDialog();
            }

        }

        private void gvTransaction_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvTransaction.RowCount.ToString();
        }

        private void chkReceipt_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPayments.Checked)
            {
                if (chkContra.Checked)
                {
                }
            }
            else
            {
                if (chkContra.Checked)
                {
                }
                else
                {
                    chkReceipt.Checked = true;
                }
            }
        }

        private void chkPayments_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReceipt.Checked)
            {
                if (chkContra.Checked)
                {
                }
            }
            else
            {
                if (chkContra.Checked)
                {
                }
                else
                {
                    chkPayments.Checked = true;
                }
            }
        }

        private void chkContra_CheckedChanged(object sender, EventArgs e)
        {

            if (chkReceipt.Checked)
            {
                if (chkPayments.Checked)
                {
                }
            }
            else if (!chkReceipt.Checked)
            {
                if (chkPayments.Checked)
                {
                }
                else
                {
                    chkContra.Checked = true;
                }
            }
            else
            {
                if (chkContra.Checked)
                {
                }
                else
                {
                    chkContra.Checked = true;
                }
            }
        }

        private void gvVoucher_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            int value = gvVoucher.FocusedRowHandle;
        }

        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            ProjectId = (glkpProject.EditValue != null) ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
            ucTransViewOpeningBalDetails.ProjectId = ProjectId;
        }

        private void gvVoucher_DoubleClick(object sender, EventArgs e)
        {
            ShowVoucherTransForm();
        }

        private void gcTransaction_DoubleClick(object sender, EventArgs e)
        {
            ShowVoucherMasterForm();
        }

        private void pceBankBalance_MouseHover(object sender, EventArgs e)
        {
            //pceBankBalance.Properties.PopupControl = popupContainerControl1;
            //gcTransBalance.DataSource = null;
            //resultArgs = FetchTransBalance((int)FixedLedgerGroup.BankAccounts);
            //if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
            //{
            //    gcTransBalance.DataSource = resultArgs.DataSource.Table;
            //    gcTransBalance.RefreshDataSource();
            //}
        }

        private void gvVoucher_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GridView activeGridView = gcTransaction.FocusedView as GridView;

            ShowCurrentLedgerBalance(activeGridView);
            //GridView activeGridView = gcTransaction.Views[1] as GridView;
            //string sledgerid = activeGridView.GetFocusedDataRow()[colLedgerId.Name].ToString();
            ////string sledgerid = activeGridView.GetFocusedRowCellValue(colLedgerId).ToString();
        }

        private void gvVoucher_RowClick(object sender, RowClickEventArgs e)
        {
            //if (gvVoucher.GetRowCellValue(e.FocusedRowHandle, colLedgerId)!=null)
            //{
            //    using (BalanceSystem balancesystem = new BalanceSystem())
            //    {
            //        BalanceProperty balProperty = balancesystem.GetBalance(ProjectId, this.UtilityMember.NumberSet.ToInteger(gvVoucher.GetRowCellValue(e.FocusedRowHandle, colLedgerId).ToString()), "", BalanceSystem.BalanceType.CurrentBalance);

            //string s = gvVoucher.GetFocusedRowCellValue(colLedgerId).ToString();
            //colLedgerName.SummaryItem.DisplayFormat = string.Format("Ledger Current Balance :53,78,000.00"); //this.UtilityMember.NumberSet.ToCurrency(balProperty.Amount).ToString()+" "+balProperty.TransMode;
            ////    }
            ////}
        }

        private void gcTransaction_ViewRegistered(object sender, ViewOperationEventArgs e)
        {
            GridView activeGridView = e.View as GridView;
            ShowCurrentLedgerBalance(activeGridView);
        }

        private void ShowCurrentLedgerBalance(GridView activeGridView)
        {
            if (activeGridView.Name == gvVoucher.Name)
            {
                string sLedgerId = activeGridView.GetFocusedRowCellValue("LEDGER_ID").ToString();

                using (BalanceSystem balancesystem = new BalanceSystem())
                {
                    BalanceProperty balProperty = balancesystem.GetBalance(ProjectId, this.UtilityMember.NumberSet.ToInteger(sLedgerId), "", BalanceSystem.BalanceType.CurrentBalance);
                    activeGridView.Columns[1].SummaryItem.DisplayFormat = "Ledger Balance: " + this.UtilityMember.NumberSet.ToCurrency(balProperty.Amount).ToString() + " " + balProperty.TransMode;
                }
            }
        }

        /// <summary>
        /// Refresh the grid after adding and editing the values. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadVoucherDetails();
            //gvTransaction.FocusedRowHandle = RowIndex;
            ucTransViewOpeningBalDetails.ShowOpNegative();
            ucTransviewClosingBalance.ShowClosingBalNegative();
        }

        private void ucTrans_MoveTransaction(object sender, EventArgs e)
        {
            if (gvTransaction.RowCount > 0)
            {
                if (VoucherMasterId > 0)
                {
                    if (!this.LoginUser.IsFullRightsReservedUser)
                    {
                        if (Vouchertype == DefaultVoucherTypes.Receipt.ToString())
                        {
                            if (CommonMethod.ApplyUserRightsForTransaction((int)Receipt.MoveReceiptVoucher) != 0)
                            {
                                MoveTransaction();
                            }
                            else
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.NO_RIGHTS_TO_MOVE_RECEIPT_VOUCHER));
                            }
                        }
                        else if (Vouchertype == DefaultVoucherTypes.Payment.ToString())
                        {
                            if (CommonMethod.ApplyUserRightsForTransaction((int)Payment.MovePaymentVoucher) != 0)
                            {
                                MoveTransaction();
                            }
                            else
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.NO_RIGHTS_TO_MOVE_PAYMENT_VOUCHER));
                            }
                        }
                        else
                        {
                            if (CommonMethod.ApplyUserRightsForTransaction((int)Contra.MoveContraVoucher) != 0)
                            {
                                MoveTransaction();
                            }
                            else
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.NO_RIGHTS_TO_MOVE_CONTRA_VOUCHER));
                            }
                        }
                    }
                    else
                    {
                        if (!IsLockedTransaction(dtSelectedVoucherDate))
                        {
                            int projectId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
                            frmMoveTransaction frmVoucherTrans = new frmMoveTransaction(projectId, VoucherMasterId, LedgerId, MoveTransForm.Transaction, VoucherDefinitionId);
                            frmVoucherTrans.UpdateHeld += new EventHandler(OnUpdateHeld);
                            frmVoucherTrans.ShowDialog();
                        }
                        else
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED_CANNOT_MOVE) + glkpProject.Text + "'");
                            //" during the period " + this.UtilityMember.DateSet.ToDate(dtLockDateFrom.ToShortDateString()) +
                            //" - " + this.UtilityMember.DateSet.ToDate(dtLockDateTo.ToShortDateString()));
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_MOVE_TRANSACTION_SELECTION_EMPTY));
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_MOVE_TRANSACTION_SELECTION_EMPTY));
            }
        }
        #endregion

        #region Methods
        private void EnableCheckBox()
        {
            if (VoucherIndex == 0)
            {
                if (CommonMethod.ApplyUserRightsForTransaction((int)Payment.CreatePaymentVoucher) != 0 || CommonMethod.ApplyUserRightsForTransaction((int)Payment.EditPaymentVoucher) != 0 ||
                    CommonMethod.ApplyUserRightsForTransaction((int)Payment.DeletePaymentVoucher) != 0 || CommonMethod.ApplyUserRightsForTransaction((int)Payment.ViewPaymentVoucher) != 0
                    || CommonMethod.ApplyUserRightsForTransaction((int)Payment.MovePaymentVoucher) != 0 || CommonMethod.ApplyUserRightsForTransaction((int)Payment.InsertPaymentVoucher) != 0)
                {
                    chkPayments.Enabled = true;
                }
                else
                {
                    chkPayments.Enabled = false;
                }
                if (CommonMethod.ApplyUserRightsForTransaction((int)Contra.CreateContraVoucher) != 0 || CommonMethod.ApplyUserRightsForTransaction((int)Contra.EditContraVoucher) != 0 ||
                    CommonMethod.ApplyUserRightsForTransaction((int)Contra.DeleteContraVoucher) != 0 || CommonMethod.ApplyUserRightsForTransaction((int)Contra.ViewContraVoucher) != 0 ||
                    CommonMethod.ApplyUserRightsForTransaction((int)Contra.MoveContraVoucher) != 0 || CommonMethod.ApplyUserRightsForTransaction((int)Contra.InsertContraVoucher) != 0)
                {
                    chkContra.Enabled = true;
                }
                else
                {
                    chkContra.Enabled = false;
                }
            }
            else if (VoucherIndex == 1)
            {
                if (CommonMethod.ApplyUserRightsForTransaction((int)Receipt.CreateReceiptVoucher) != 0 || CommonMethod.ApplyUserRightsForTransaction((int)Receipt.EditReceiptVoucher) != 0 ||
                    CommonMethod.ApplyUserRightsForTransaction((int)Receipt.DeleteReceiptVoucher) != 0 || CommonMethod.ApplyUserRightsForTransaction((int)Receipt.ViewReceiptVoucher) != 0 ||
                    CommonMethod.ApplyUserRightsForTransaction((int)Receipt.MoveReceiptVoucher) != 0 || CommonMethod.ApplyUserRightsForTransaction((int)Receipt.InsertReceiptVoucher) != 0)
                {
                    chkReceipt.Enabled = true;
                }
                else
                {
                    chkReceipt.Enabled = false;
                }
                if (CommonMethod.ApplyUserRightsForTransaction((int)Contra.CreateContraVoucher) != 0 || CommonMethod.ApplyUserRightsForTransaction((int)Contra.EditContraVoucher) != 0 ||
                    CommonMethod.ApplyUserRightsForTransaction((int)Contra.DeleteContraVoucher) != 0 || CommonMethod.ApplyUserRightsForTransaction((int)Contra.ViewContraVoucher) != 0 ||
                    CommonMethod.ApplyUserRightsForTransaction((int)Contra.MoveContraVoucher) != 0 || CommonMethod.ApplyUserRightsForTransaction((int)Contra.InsertContraVoucher) != 0)
                {
                    chkContra.Enabled = true;
                }
                else
                {
                    chkContra.Enabled = false;
                }
            }
            else if (VoucherIndex == 2)
            {
                if (CommonMethod.ApplyUserRightsForTransaction((int)Receipt.CreateReceiptVoucher) != 0 || CommonMethod.ApplyUserRightsForTransaction((int)Receipt.EditReceiptVoucher) != 0 ||
                   CommonMethod.ApplyUserRightsForTransaction((int)Receipt.DeleteReceiptVoucher) != 0 || CommonMethod.ApplyUserRightsForTransaction((int)Receipt.ViewReceiptVoucher) != 0 ||
                   CommonMethod.ApplyUserRightsForTransaction((int)Receipt.MoveReceiptVoucher) != 0 || CommonMethod.ApplyUserRightsForTransaction((int)Receipt.InsertReceiptVoucher) != 0)
                {
                    chkReceipt.Enabled = true;
                }
                else
                {
                    chkReceipt.Enabled = false;
                }
                if (CommonMethod.ApplyUserRightsForTransaction((int)Payment.CreatePaymentVoucher) != 0 || CommonMethod.ApplyUserRightsForTransaction((int)Payment.EditPaymentVoucher) != 0 ||
                   CommonMethod.ApplyUserRightsForTransaction((int)Payment.DeletePaymentVoucher) != 0 || CommonMethod.ApplyUserRightsForTransaction((int)Payment.ViewPaymentVoucher) != 0 ||
                   CommonMethod.ApplyUserRightsForTransaction((int)Payment.MovePaymentVoucher) != 0 || CommonMethod.ApplyUserRightsForTransaction((int)Payment.InsertPaymentVoucher) != 0)
                {
                    chkPayments.Enabled = true;
                }
                else
                {
                    chkPayments.Enabled = false;
                }
            }
        }

        private void MoveTransaction()
        {
            try
            {
                if (!IsLockedTransaction(dtSelectedVoucherDate))
                {
                    int projectId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
                    frmMoveTransaction frmVoucherTrans = new frmMoveTransaction(projectId, VoucherMasterId, LedgerId, MoveTransForm.Transaction, VoucherDefinitionId);
                    frmVoucherTrans.UpdateHeld += new EventHandler(OnUpdateHeld);
                    frmVoucherTrans.ShowDialog();
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED_CANNOT_MOVE) + glkpProject.Text + "'");
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void LoadProject()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    mappingSystem.ProjectClosedDate = deDateFrom.Text;
                    resultArgs = mappingSystem.FetchProjectsLookup();
                    glkpProject.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProject, resultArgs.DataSource.Table, mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                        //  this.glkpProject.EditValueChanged -= new System.EventHandler(this.glkpProject_EditValueChanged);
                        /*DataView dvpro = resultArgs.DataSource.Table.AsDataView();
                        dvpro.RowFilter = "PROJECT_ID=" + ProjectId + "";
                        bool isProjectavail = false;
                        if (dvpro.ToTable().Rows.Count > 0)
                        {
                            isProjectavail = true;
                        }
                        glkpProject.EditValue = (ProjectId != 0 && isProjectavail) ? ProjectId : glkpProject.Properties.GetKeyValue(0);*/

                        glkpProject.EditValue = (glkpProject.Properties.GetDisplayValueByKeyValue(ProjectId) != null ? ProjectId : glkpProject.Properties.GetKeyValue(0));
                        // this.glkpProject.EditValueChanged += new System.EventHandler(this.glkpProject_EditValueChanged);
                    }
                    else
                    {
                        XtraMessageBox.Show(resultArgs.Message);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        public void LoadVoucherDetails()
        {
            //this.ShowWaitDialog(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.LOADING_VOUCHERS));
            //try
            //{
            //    DataSet dsVoucher = new DataSet();
            //    using (VoucherTransactionSystem voucherTransactionSystem = new VoucherTransactionSystem())
            //    {
            //        string VoucherType = GetSelectedTransactions();
            //        dsVoucher = voucherTransactionSystem.LoadVoucherDetails(ProjectId, VoucherType, deDateFrom.DateTime, deDateTo.DateTime);
            //        if (dsVoucher.Tables.Count != 0)
            //        {
            //            gcTransaction.DataSource = dsVoucher;
            //            gcTransaction.DataMember = "Master";
            //            gcTransaction.RefreshDataSource();
            //        }
            //        else
            //        {
            //            gcTransaction.DataSource = null;
            //            gcTransaction.RefreshDataSource();
            //        }
            //        gvTransaction.FocusedRowHandle = 0;
            //        gvTransaction.FocusRectStyle = DrawFocusRectStyle.RowFocus;
            //        LoadUserControlInputData();
            //    }
            //}
            //catch (Exception err)
            //{
            //    //CloseWaitDialog();
            //}
            //finally
            //{
            //    //CloseWaitDialog();
            //}

            LoadVoucherMaster();
            LoadVoucherTransDetails();
            LoadVouchercostcentre();

            ShowHideColumnsBasedOnSetting();
            zoomctl.EditValue = gvTransaction.Appearance.Row.Font.Size;
        }

        private void ShowHideColumnsBasedOnSetting()
        {
            
            GridColumn lastVisibleColumn = null;

            //To find last visible column except those are not button coulmns and customize columns (setting based columns)
            for (Int32 i = gvTransaction.VisibleColumns.Count-1; i>=  0; i--) 
            {
                lastVisibleColumn = gvTransaction.VisibleColumns[i];

                if (lastVisibleColumn.ColumnEdit == null && lastVisibleColumn != colThirdpartyClientId &&
                    lastVisibleColumn != colAuthorizationStatus && lastVisibleColumn != colVendorGSTInvoice)
                {
                    break;
                }
            }

            if (lastVisibleColumn != null)
            {
                //14/11/2022, to show print GST invoice details 
                colVendorGSTInvoice.Visible = colPrintGSTInvoice.Visible = colAuthorizationStatus.Visible = colThirdpartyClientId.Visible = false;

                colCurrencyName.Visible =  colLedgerExchangeRate.Visible = colLedgerLiveExchangeRate.Visible = false;
                if (this.AppSetting.AllowMultiCurrency == 1)
                {
                    colCurrencyName.Visible = colLedgerExchangeRate.Visible = colLedgerLiveExchangeRate.Visible = true;
                    colLedgerExchangeRate.VisibleIndex = colLedgerName.VisibleIndex + 1;
                    colLedgerLiveExchangeRate.VisibleIndex = colLedgerExchangeRate.VisibleIndex + 1;
                }

                //For Vendor GST details column
                if (this.AppSetting.EnableGST == "1" && this.AppSetting.IncludeGSTVendorInvoiceDetails == "1")
                {
                    colVendorGSTInvoice.Visible = true;
                    colVendorGSTInvoice.VisibleIndex = lastVisibleColumn.VisibleIndex + 1;//colNameAddress.VisibleIndex + 1;
                }

                //For Third Party column
                if (!string.IsNullOrEmpty(this.AppSetting.ManagementCode))
                {
                    colThirdpartyClientId.Visible = true;
                    colThirdpartyClientId.VisibleIndex = (colVendorGSTInvoice.Visible ? colVendorGSTInvoice.VisibleIndex + 1 : lastVisibleColumn.VisibleIndex + 1); //colNameAddress.VisibleIndex+1
                }

                //For Authorization Status column
                if (this.AppSetting.ConfirmAuthorizationVoucherEntry == 1)
                {
                    colAuthorizationStatus.Visible = true;
                    if (colThirdpartyClientId.Visible)
                        colAuthorizationStatus.VisibleIndex = colThirdpartyClientId.VisibleIndex + 1;
                    else if (colVendorGSTInvoice.Visible)
                        colAuthorizationStatus.VisibleIndex = colVendorGSTInvoice.VisibleIndex + 1;
                    else
                        colAuthorizationStatus.VisibleIndex = lastVisibleColumn.VisibleIndex + 1; // colNameAddress.VisibleIndex + 1;
                }

                //For Print GST Invoice button
                if (this.AppSetting.EnableGST == "1" && this.AppSetting.IncludeGSTVendorInvoiceDetails == "1")
                {
                    colPrintGSTInvoice.Visible = true;
                    if (colAuthorizationStatus.Visible)
                        colPrintGSTInvoice.VisibleIndex = colAuthorizationStatus.VisibleIndex + 1;
                    else if (colThirdpartyClientId.Visible)
                        colPrintGSTInvoice.VisibleIndex = colThirdpartyClientId.VisibleIndex + 1;
                    else if (colVendorGSTInvoice.Visible)
                        colPrintGSTInvoice.VisibleIndex = colVendorGSTInvoice.VisibleIndex + 1;
                    else
                        colPrintGSTInvoice.VisibleIndex = lastVisibleColumn.VisibleIndex + 1;// colNameAddress.VisibleIndex + 1;
                }
            }
        }

        public void LoadVoucherMaster()
        {
            try
            {
                DataTable dtVoucher = new DataTable();
                using (VoucherTransactionSystem voucherTransactionSystem = new VoucherTransactionSystem())
                {
                    string VoucherType = GetSelectedTransactions();
                    dtVoucher = voucherTransactionSystem.LoadVoucherMasterDetails(ProjectId, VoucherType, deDateFrom.DateTime, deDateTo.DateTime);
                    if (dtVoucher.Rows.Count != 0)
                    {
                        gcTransaction.DataSource = dtVoucher;
                        gcTransaction.RefreshDataSource();
                        dtVoucherMasterDetails = dtVoucher;
                    }
                    else
                    {
                        gcTransaction.DataSource = null;
                        gcTransaction.RefreshDataSource();
                    }
                    gvTransaction.FocusedRowHandle = 0;
                    gvTransaction.FocusRectStyle = DrawFocusRectStyle.RowFocus;
                    LoadUserControlInputData();
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
            finally
            {

            }
        }

        public void LoadVoucherTransDetails()
        {
            try
            {
                DataTable dtVoucher = new DataTable();
                using (VoucherTransactionSystem voucherTransactionSystem = new VoucherTransactionSystem())
                {
                    string VoucherType = GetSelectedTransactions();
                    dtVoucher = voucherTransactionSystem.LoadVoucherTranDetails(ProjectId, VoucherType, deDateFrom.DateTime, deDateTo.DateTime);
                    if (dtVoucher.Rows.Count != 0)
                    {
                        dtVoucherDetails = dtVoucher;
                        LoadTransDetailsByVoucherId();
                        gcLedgerDetails.RefreshDataSource();
                    }
                    else
                    {
                        gcLedgerDetails.DataSource = null;
                        gcLedgerDetails.RefreshDataSource();
                    }
                    gvLedgerDetails.FocusedRowHandle = 0;
                    gvLedgerDetails.FocusRectStyle = DrawFocusRectStyle.RowFocus;
                    LoadUserControlInputData();
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
        }

        public void LoadVouchercostcentre()
        {
            try
            {
                DataTable dtVoucher = new DataTable();
                using (VoucherTransactionSystem voucherTransactionSystem = new VoucherTransactionSystem())
                {
                    string VoucherType = GetSelectedTransactions();
                    dtVoucher = voucherTransactionSystem.LoadVoucherCCDetails(ProjectId, VoucherType, deDateFrom.DateTime, deDateTo.DateTime);
                    if (dtVoucher.Rows.Count != 0)
                    {
                        dtVoucherCostcentreDetails = dtVoucher;
                        LoadCCDetailsByVoucherId();
                        gcTransaction.RefreshDataSource();
                    }
                    else
                    {
                        gcCostCentreDetails.DataSource = null;
                        gcCostCentreDetails.RefreshDataSource();
                    }
                    gvCostCentreDetails.FocusedRowHandle = 0;
                    gvCostCentreDetails.FocusRectStyle = DrawFocusRectStyle.RowFocus;
                    LoadUserControlInputData();
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
            finally
            {

            }
        }


        private void LoadUserControlInputData()
        {
            ucTransViewOpeningBalDetails.ProjectId = projectId;
            ucTransViewOpeningBalDetails.OpeningDateFrom = deDateFrom.Text;
            ucTransViewOpeningBalDetails.OpeningDateTo = deDateTo.Text;
            ucTransviewClosingBalance.ProjectId = ProjectId;
            ucTransviewClosingBalance.ClosingDateFrom = deDateFrom.Text;
            ucTransviewClosingBalance.ClosingDateTo = deDateTo.Text;
            ucTransViewOpeningBalDetails.BankClosedDate = deDateFrom.Text; //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
            ucTransViewOpeningBalDetails.GetOpBalance();
            ucTransViewOpeningBalDetails.ShowOpNegative();
            ucTransviewClosingBalance.BankClosedDate = deDateFrom.Text; //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
            ucTransviewClosingBalance.GetClosingBalance();
            ucTransviewClosingBalance.ShowClosingBalNegative();

        }

        public string GetSelectedTransactions()
        {
            string Transaction = string.Empty;
            if (chkReceipt.Checked)
            {
                Transaction = "RC" + ",";
            }
            else
            {
                Transaction = "" + ",";
            }
            if (chkPayments.Checked)
            {
                Transaction += "PY" + ",";
            }
            else
            {
                Transaction += "" + ",";
            }
            if (chkContra.Checked)
            {
                Transaction += "CN";
            }
            else
            {
                Transaction += "";
            }
            Transaction = Transaction.TrimEnd(',');
            return Transaction;
        }

        private void DeleteVoucherDetails()
        {
            try
            {
                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (gvTransaction.RowCount != 0)
                    {
                        string ThirdPartyId = gvTransaction.GetFocusedRowCellValue(colThirdpartyClientId) != null ? gvTransaction.GetFocusedRowCellValue(colThirdpartyClientId).ToString() : string.Empty;
                        //Added by Carmel Raj on October-14-2015
                        //Purpose :Route the voucher to delete the concern voucher and its child records
                        using (RouteVoucher routeVoucher = new RouteVoucher())
                        {
                            //if (VoucherSubType != "AST" && VoucherSubType != VoucherSubTypes.CRI.ToString())
                            //if (VoucherSubType != "AST" && ThirdPartyId == "")
                            if (VoucherSubType != "AST")
                            {
                                if (this.AppSetting.IS_SDB_INM)
                                {
                                    if (string.IsNullOrEmpty(ThirdPartyId))
                                        routeVoucher.RouteVoucherDelete(this, VoucherMasterId, (VoucherSubTypes)Enum.Parse(typeof(VoucherSubTypes), VoucherSubType));
                                    else
                                        this.ShowMessageBox("This Voucher is posted by Third Party application, can not be deleted or modified." + " (" + Bosco.Utility.ConfigSetting.SettingProperty.ManagementCodeIntegration + ")");
                                }
                                else
                                {
                                    routeVoucher.RouteVoucherDelete(this, VoucherMasterId, (VoucherSubTypes)Enum.Parse(typeof(VoucherSubTypes), VoucherSubType));
                                }
                            }
                            else
                            {
                                if (VoucherSubType == VoucherSubTypes.AST.ToString())
                                {
                                    //this.ShowMessageBox("Entry can be deleted in Fixed Asset Module.");
                                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Transaction.TRANS_JOURNAL_ENTRY_CAN_DELETED_FIXED_ASSET_MODULE));
                                }
                            }

                        }
                        LoadVoucherDetails();
                    }
                    else
                    {
                        ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        public void DeleteVoucherTrans(int VoucherId)
        {
            try
            {
                using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                {
                    voucherTransaction.VoucherId = VoucherId;
                    //Modified by Carmel Raj on 20-Oct-2015
                    //Purpose :Avoiding Message replication,since Common confirmation message is given in voucher routing itseft,

                    //if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    voucherTransaction.tdsTransType = TDSTransType.TDSPartyPayment;
                    resultArgs = voucherTransaction.DeleteVoucherTrans();
                    if (resultArgs.Success)
                    {
                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                        // LoadVoucherDetails();
                    }
                    else
                    {
                        this.ShowMessageBox(resultArgs.Message);
                    }
                    // }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void ShowVoucherMasterForm(bool MakeDuplicate = false)
        {
            try
            {
                if (this.isEditable)
                {
                    if (VoucherMasterId != 0)
                    {
                        if (gvTransaction.RowCount != 0)
                        {
                            ShowForm(VoucherMasterId, MakeDuplicate);
                        }
                    }
                    else
                    {
                        if (!chkShowFilter.Checked)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_USER_RIGHTS));
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }

        private void ShowVoucherTransForm()
        {
            try
            {
                if (gvVoucher.RowCount != 0)
                {
                    ShowForm(VoucherTransId);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }

        private void ShowForm(int VoucherID, bool MakeDuplicate = false)
        {
            try
            {
                RowIndex = gvTransaction.FocusedRowHandle;
                int ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
                string VoucherSubType = gvTransaction.GetFocusedRowCellValue(colVoucherSubType) != null ? gvTransaction.GetFocusedRowCellValue(colVoucherSubType).ToString() : string.Empty;
                string ThirdPartyId = gvTransaction.GetFocusedRowCellValue(colThirdpartyClientId) != null ? gvTransaction.GetFocusedRowCellValue(colThirdpartyClientId).ToString() : string.Empty;
                // done by sala
                ProjectName = glkpProject.Text;
                if (VoucherID > 0)
                {
                    if (Vouchertype == DefaultVoucherTypes.Receipt.ToString()) { VoucherIndex = 0; } else if (Vouchertype == DefaultVoucherTypes.Payment.ToString()) { VoucherIndex = 1; } else { VoucherIndex = 2; }
                }
                //Added by Carmel Raj on October-01-2015
                //Purpose :When Voucher Sub Type is PayRoll ClienReferenceId is the VoucherId according to the table design

                //VoucherID = (VoucherSubTypes)Enum.Parse(typeof(VoucherSubTypes), VoucherSubType) == VoucherSubTypes.PAY ?
                //    gvTransaction.GetFocusedRowCellValue(colPayRollClientId) != null ? UtilityMember.NumberSet.ToInteger(gvTransaction.GetFocusedRowCellValue(colPayRollClientId).ToString()) : 0 : VoucherID;

                //Added by Carmel Raj on October-01-2015
                //Purpose :Voucher Router for all the modules wherever voucher entries made
                using (RouteVoucher routeVoucher = new RouteVoucher())
                {
                    //if (VoucherSubType != "AST" && VoucherSubType != VoucherSubTypes.CRI.ToString())

                    // && ThirdPartyId == "0" - it is added by chinna not to edit the Third Party Application
                    // private string ManagementCode = SettingProperty.ManagementCodeIntegration;
                    // if (VoucherSubType != "AST" && ThirdPartyId == "")
                    //   if (VoucherSubType != "AST" && string.IsNullOrEmpty(ThirdPartyId))
                    if (VoucherSubType != "AST")
                    {
                        if (this.AppSetting.IS_SDB_INM)
                        {
                            if (string.IsNullOrEmpty(ThirdPartyId))
                                routeVoucher.RouteVoucherEdit(this, VoucherID, (VoucherSubTypes)Enum.Parse(typeof(VoucherSubTypes), VoucherSubType), MakeDuplicate);
                            else
                                this.ShowMessageBox("This Voucher is posted by Third Party application, can not be deleted or modified." + " (" + Bosco.Utility.ConfigSetting.SettingProperty.ManagementCodeIntegration + ")");
                        }
                        else
                        {
                            routeVoucher.RouteVoucherEdit(this, VoucherID, (VoucherSubTypes)Enum.Parse(typeof(VoucherSubTypes), VoucherSubType), MakeDuplicate);
                        }
                    }
                    else
                    {
                        if (VoucherSubType == VoucherSubTypes.AST.ToString())
                        {
                            //this.ShowMessageBox("Entry can be edited in the Fixed Asset Module.");
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Transaction.TRANS_VIEW_ENTRY_CAN_EDIT_ASSET_MODULE));
                        }

                    }
                }
                //Referesh the voucher details
                LoadVoucherDetails();
                gvTransaction.FocusedRowHandle = RowIndex;
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void ShowTransactionForm()
        {
            try
            {
                DateTime dtyearfrom = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
                DateTime dtbookbeginfrom = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false);
                DateTime dtRecentVoucher = UtilityMember.DateSet.ToDate(this.AppSetting.RecentVoucherDate, false);
                int ProId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
                ApplyRecentPrjectDetails(ProId);
                //DateTime dtRecentVoucherDate = (!string.IsNullOrEmpty(this.AppSetting.RecentVoucherDate)) ? this.UtilityMember.DateSet.ToDate(this.AppSetting.RecentVoucherDate, false) : dtbookbeginfrom > dtyearfrom ? dtbookbeginfrom : dtyearfrom;
                DateTime dtRecentVoucherDate = (!string.IsNullOrEmpty(AppSetting.RecentVoucherDate)) ? this.UtilityMember.DateSet.ToDate(AppSetting.RecentVoucherDate, false) : deDateFrom.DateTime;

                //01/03/2018, since recent date is taken from db by defualt, if that date is locked, we can show voucher etnry form
                //so we open voucher form here evern date is locked, it will be validated in entry form
                string Pro = glkpProject.Text.ToString();

                //On 28/01/2019, show list of voucher types ---------------------------------------------------------------------------------------------
                //this list will be shown only when more than one voucher type exists except base vouchers for selected project
                Int32 voucherdefinitionid = voucherIndex == 0 ? (Int32)DefaultVoucherTypes.Receipt : voucherIndex == 1 ? (Int32)DefaultVoucherTypes.Payment : (Int32)DefaultVoucherTypes.Contra; //by default 
                string basevouchers = voucherIndex == 0 ? ((Int32)DefaultVoucherTypes.Receipt).ToString() : voucherIndex == 1 ? ((Int32)DefaultVoucherTypes.Payment).ToString() : ((Int32)DefaultVoucherTypes.Contra).ToString();
                ResultArgs result = this.ShowVoucherTypeSelection(ProId, basevouchers, voucherdefinitionid);
                if (result.Success && result.ReturnValue != null)
                {
                    string[] VoucherTypeSelected = result.ReturnValue as string[];
                    voucherdefinitionid = UtilityMember.NumberSet.ToInteger(VoucherTypeSelected[0]);

                    Int32 baseVoucherType = UtilityMember.NumberSet.ToInteger(VoucherTypeSelected[1]);
                    if (baseVoucherType == (Int32)DefaultVoucherTypes.Receipt) { VoucherIndex = 0; }
                    else if (baseVoucherType == (Int32)DefaultVoucherTypes.Payment) { VoucherIndex = 1; }
                    else { VoucherIndex = 2; }
                }
                //----------------------------------------------------------------------------------------------------------------------------------------------

                frmTransactionMultiAdd frmTransaction = new frmTransactionMultiAdd(dtRecentVoucherDate.ToString(), ProId, Pro, (int)AddNewRow.NewRow, VoucherIndex, false, voucherdefinitionid);
                frmTransaction.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmTransaction.ShowDialog();

                /*if (!IsLockedTransaction(dtRecentVoucherDate))
                {
                    string Pro = glkpProject.Text.ToString();
                    frmTransactionMultiAdd frmTransaction = new frmTransactionMultiAdd(dtRecentVoucherDate.ToString(), ProId, Pro, (int)AddNewRow.NewRow, VoucherIndex);
                    frmTransaction.UpdateHeld += new EventHandler(OnUpdateHeld);
                    frmTransaction.ShowDialog();
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED) + glkpProject.Text + "'");
                }*/

                //28/11/2018, refresh finance setting
                if (this.MdiParent != null)
                {
                    (this.MdiParent as frmMain).ReAssignSetting();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private ResultArgs FetchTransBalance(int GroupId)
        {
            using (VoucherTransactionSystem voucherTransSystem = new VoucherTransactionSystem())
            {
                voucherTransSystem.ProjectId = ProjectId;
                voucherTransSystem.GroupId = GroupId;
                resultArgs = voucherTransSystem.FetchTransBalance();
            }
            return resultArgs;
        }

        private ResultArgs FetchFDOPBalance(int GroupId)
        {
            using (VoucherTransactionSystem voucherTransSystem = new VoucherTransactionSystem())
            {
                voucherTransSystem.ProjectId = ProjectId;
                voucherTransSystem.GroupId = GroupId;
                resultArgs = voucherTransSystem.FetchFDOpBalance();
            }
            return resultArgs;
        }

        #endregion

        private void rbtnPrintVoucher_Click(object sender, EventArgs e)
        {
            LoadPrintVoucher();
        }

        private void LoadPrintVoucher()
        {
            //On 01/02/2018, to show contra voucher also and Treat Journal Voucher as Contra Voucher
            //if (Vouchertype != DefaultVoucherTypes.Contra.ToString() && Vouchertype != string.Empty && gcTransaction != null)
            //    PrintVoucher(VoucherMasterId);
            if (Vouchertype != string.Empty && gcTransaction != null)
                PrintVoucher(VoucherMasterId);
        }

        private void PrintVoucher(int vid)
        {
            //On 01/02/2018, to show contra voucher also, Treat Journal Voucher as Contra Voucher
            //Treat Journal Voucher as Contra Voucher
            if (this.LoginUser.IsFullRightsReservedUser)
            {
                if (!IsLockedTransaction(dtSelectedVoucherDate))
                {
                    if (VoucherDefinitionId == (Int32)DefaultVoucherTypes.Receipt && !AppSetting.ENABLE_TRACK_RECEIPT_MODULE)
                    {
                        this.ShowMessageBox(MessageCatalog.Common.COMMON_RECEIPT_DISABLED_MESSAGE);
                    }
                    else
                    {
                        //if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.CONFIRM_PRINT_VOUCHER), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes && vid != 0)
                        //{
                        using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
                        {
                            string rptVoucher = string.Empty;
                            Bosco.Report.Base.IReport report = new Bosco.Report.Base.ReportEntry(this.MdiParent);
                            //rptVoucher = Vouchertype == DefaultVoucherTypes.Receipt.ToString() ? UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKRECEIPTS) : Vouchertype == DefaultVoucherTypes.Payment.ToString() ? UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKPAYMENTS) : string.Empty;

                            // if BoscoNet Receipts Template Enabled in the Voucher View Screen _Delhi Client_..........Chinna 
                            if (Bosco.Utility.ConfigSetting.SettingProperty.EnableNetworking == true)
                            {
                                //On 27/04/2022, to have common, print (discuss with mr. chinna)
                                //rptVoucher = vouchertype == DefaultVoucherTypes.Receipt.ToString() ? UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKDONORRECEIPTS) : Vouchertype == DefaultVoucherTypes.Payment.ToString() ? UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKPAYMENTS) : UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKCONTRA);
                                rptVoucher = Vouchertype == DefaultVoucherTypes.Receipt.ToString() ? UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKRECEIPTS) : Vouchertype == DefaultVoucherTypes.Payment.ToString() ? UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKPAYMENTS) : UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKCONTRA);
                            }
                            else
                            {
                                rptVoucher = Vouchertype == DefaultVoucherTypes.Receipt.ToString() ? UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKRECEIPTS) : Vouchertype == DefaultVoucherTypes.Payment.ToString() ? UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKPAYMENTS) : UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKCONTRA);
                            }

                            resultArgs = voucherSystem.FetchReportSetting(rptVoucher);
                            if (resultArgs != null && resultArgs.Success)
                            {
                                ReportProperty.Current.VoucherPrintSettingInfo = resultArgs.DataSource.TableView;
                                ReportProperty.Current.CashBankVoucherDateFrom = ReportProperty.Current.CashBankVoucherDateTo = dtSelectedVoucherDate;
                                report.VoucherPrint(vid.ToString(), rptVoucher, ProjectName, ProjectId);
                            }
                            else
                            {
                                this.ShowMessageBoxError(resultArgs.Message);
                            }
                        }
                    }
                    //}
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED_CANNOT_PRINT) + glkpProject.Text + "'");
                }
            }
            else
            {
                if (Vouchertype == DefaultVoucherTypes.Receipt.ToString())
                {
                    if (CommonMethod.ApplyUserRightsForTransaction((int)Receipt.PrintReceiptVoucher) != 0)
                    {
                        PrintVouchers(vid);
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.NO_RIGHTS_TO_TAKE_PRINTOUT));
                    }
                }
                else if (Vouchertype == DefaultVoucherTypes.Payment.ToString())
                {
                    if (CommonMethod.ApplyUserRightsForTransaction((int)Payment.PrintPaymentVoucher) != 0)
                    {
                        PrintVouchers(vid);
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.NO_RIGHTS_TO_TAKE_PRINTOUT_PAYMENT));
                    }
                }
            }

        }

        private void PrintGSTInvoice(int vid)
        {
            //On 01/02/2018, to show contra voucher also, Treat Journal Voucher as Contra Voucher
            //Treat Journal Voucher as Contra Voucher
            if (this.LoginUser.IsFullRightsReservedUser)
            {
                if (!IsLockedTransaction(dtSelectedVoucherDate))
                {
                    string vendorgst = gvTransaction.GetFocusedRowCellValue(colVendorGSTInvoice) != null ? gvTransaction.GetFocusedRowCellValue(colVendorGSTInvoice).ToString() : string.Empty;

                    if (!string.IsNullOrEmpty(vendorgst))
                    {
                        if (VoucherDefinitionId == (Int32)DefaultVoucherTypes.Receipt && !AppSetting.ENABLE_TRACK_RECEIPT_MODULE)
                        {
                            this.ShowMessageBox(MessageCatalog.Common.COMMON_RECEIPT_DISABLED_MESSAGE);
                        }
                        else if (VoucherDefinitionId == (Int32)DefaultVoucherTypes.Receipt)
                        {
                            using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
                            {
                                string rptVoucher = string.Empty;
                                Bosco.Report.Base.IReport report = new Bosco.Report.Base.ReportEntry(this.MdiParent);
                                rptVoucher = Vouchertype == DefaultVoucherTypes.Receipt.ToString() ? UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKRECEIPTS) : Vouchertype == DefaultVoucherTypes.Payment.ToString() ? UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKPAYMENTS) : UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKCONTRA);
                                resultArgs = voucherSystem.FetchReportSetting(rptVoucher);
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    ReportProperty.Current.VoucherPrintSettingInfo = resultArgs.DataSource.TableView;
                                    ReportProperty.Current.CashBankVoucherDateFrom = ReportProperty.Current.CashBankVoucherDateTo = dtSelectedVoucherDate;
                                    report.VoucherPrint(vid.ToString(), UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.GSTINVOICE_RECEIPT), ProjectName, ProjectId);
                                }
                                else
                                {
                                    this.ShowMessageBoxError(resultArgs.Message);
                                }
                            }
                        }
                        else if (VoucherDefinitionId == (Int32)DefaultVoucherTypes.Payment)
                        {
                            this.ShowMessageBox("No option to Print GST Invoice details for Payment Voucher");
                        }
                    }
                    else
                    {
                        this.ShowMessageBox("GST Invoice details is not available");
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED_CANNOT_PRINT) + glkpProject.Text + "'");
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.NO_RIGHTS_TO_TAKE_PRINTOUT));
            }

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.F3))
            {
                //deDateFrom.Focus();
                frmDatePicker datePicker = new frmDatePicker(deDateFrom.DateTime, deDateTo.DateTime, DatePickerType.ChangePeriod);
                datePicker.ShowDialog();
                deDateFrom.DateTime = AppSetting.VoucherDateFrom;
                deDateTo.DateTime = AppSetting.VoucherDateTo;
            }
            if (KeyData == (Keys.Control | Keys.P))
            {
                LoadPrintVoucher();
            }
            if (KeyData == (Keys.Alt | Keys.U))
            {
                ShowVoucherMasterForm(true);
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        private void frmTransactionView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void PrintVouchers(int vid)
        {
            try
            {
                if (!IsLockedTransaction(dtSelectedVoucherDate))
                {
                    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.CONFIRM_PRINT_VOUCHER), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes && vid != 0)
                    {
                        using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
                        {
                            Bosco.Report.Base.IReport report = new Bosco.Report.Base.ReportEntry(this.MdiParent);
                            string rptVoucher = Vouchertype == DefaultVoucherTypes.Receipt.ToString() ? UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKRECEIPTS) : Vouchertype == DefaultVoucherTypes.Payment.ToString() ? UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKPAYMENTS) : string.Empty;

                            resultArgs = voucherSystem.FetchReportSetting(rptVoucher);
                            if (resultArgs != null && resultArgs.Success)
                            {
                                ReportProperty.Current.VoucherPrintSettingInfo = resultArgs.DataSource.TableView;
                                ReportProperty.Current.CashBankVoucherDateFrom = ReportProperty.Current.CashBankVoucherDateTo = dtSelectedVoucherDate;
                                report.VoucherPrint(vid.ToString(), rptVoucher, ProjectName, ProjectId);
                            }
                            else
                            {
                                this.ShowMessageBoxError(resultArgs.Message);
                            }
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED_CANNOT_PRINT) + glkpProject.Text + "'");
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void deDateTo_Leave(object sender, EventArgs e)
        {
            if (deDateFrom.DateTime > deDateTo.DateTime)
            {
                deDateTo.DateTime = deDateFrom.DateTime;
            }
        }

        private void frmTransactionView_EnterClicked(object sender, EventArgs e)
        {
            //ShowVoucherMasterForm();
        }

        private void deDateFrom_Leave(object sender, EventArgs e)
        {
            if (IsDateLoaded)
            {
                deDateTo.DateTime = deDateFrom.DateTime.AddMonths(1).AddDays(-1);
                IsDateLoaded = true;
            }
            if (deDateFrom.DateTime > deDateTo.DateTime)
            {
                //DateTime dateTo = deDateTo.DateTime;
                //deDateTo.DateTime = deDateFrom.DateTime;
                //deDateFrom.DateTime = dateTo.Date;
                deDateTo.DateTime = deDateFrom.DateTime;
            }
        }

        //private void popupceFDRealization_Click(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        ucFDRealization ucFdrealization = new ucFDRealization();
        //        AssignFDRealization = ucFdrealization.PopupContainerControl;
        //        ucFdrealization.LoadFDRealisation(deDateFrom.Text, deDateTo.Text, ProjectId);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageRender.ShowMessage(ex.ToString(), true);
        //    }
        //    finally { }
        //    ////AssingCloisngFD = ucTrans.PopupContainerControl;
        //    ////ucTrans.gcData.DataSource = null;
        //    ////resultArgs = FetchTransFDClosingBalance((int)FixedLedgerGroup.FixedDeposit, FDTypes.IN);
        //    //// ucTrans.LedgerName = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.TRANSACTION_FD_LEDGER);
        //    //ucTrans.LedgerName = "FD Ledger";
        //    //if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
        //    //{
        //    //    FDRealization.DataSource = resultArgs.DataSource.Table;
        //    //    ucTrans.gcData.RefreshDataSource();
        //    //}

        //}

        private void ucTrans_NegativeBalanceClicked(object sender, EventArgs e)
        {
            frmNegativeBalanceHistory negativebalance = new frmNegativeBalanceHistory(ProjectId, deDateFrom.DateTime);
            negativebalance.ShowDialog();
        }

        private void ucTrans_InsertVoucher(object sender, EventArgs e)
        {
            ShowInsertVoucherForm();
        }

        private bool IsValidNumberFormat(string VoucherNo, string Prefix, string Sufix)
        {
            bool isValid = true;
            if (!string.IsNullOrEmpty(Prefix) && !VoucherNo.Contains(Prefix))
            {
                isValid = false;
            }
            else if (!string.IsNullOrEmpty(Sufix) && !VoucherNo.Contains(Sufix))
            {
                isValid = false;
            }
            return isValid;
        }

        private ResultArgs SplitRunningDigit(string PreviousVoucherNo)
        {
            try
            {
                string numbFormat = "";
                string Prifix = "";
                string Sufix = "";
                int StartingNumber = 0;
                int Prefilwitzero = 0;

                if (!string.IsNullOrEmpty(PreviousVoucherNo))
                {
                    using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                    {
                        voucherTransaction.SetRegenerateMethod(Vouchertype, VoucherDefinitionId);
                        int ProjId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
                        voucherTransaction.ProjectId = ProjId;
                        voucherTransaction.VoucherType = Vouchertype;
                        resultArgs = voucherTransaction.FetchVoucherNumberDefinition();
                        if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            int TransVoucherMethod = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][voucherTransaction.AppSchema.Voucher.VOUCHER_METHODColumn.ColumnName].ToString());
                            if (TransVoucherMethod == (int)TransactionVoucherMethod.Automatic)
                            {
                                Prifix = resultArgs.DataSource.Table.Rows[0][voucherTransaction.AppSchema.Voucher.PREFIX_CHARColumn.ColumnName].ToString();
                                Sufix = resultArgs.DataSource.Table.Rows[0][voucherTransaction.AppSchema.Voucher.SUFFIX_CHARColumn.ColumnName].ToString();
                                StartingNumber = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][voucherTransaction.AppSchema.Voucher.STARTING_NUMBERColumn.ColumnName].ToString());
                                Prefilwitzero = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][voucherTransaction.AppSchema.Voucher.PREFIX_WITH_ZEROColumn.ColumnName].ToString());
                                numbFormat = (Prifix + Sufix).ToString();

                                if (IsValidNumberFormat(PreviousVoucherNo, Prifix, Sufix))
                                {
                                    string strVoucherNo = PreviousVoucherNo.Remove(0, Prifix.Length);
                                    if (!string.IsNullOrEmpty(Sufix))
                                    {
                                        strVoucherNo = strVoucherNo.Remove(strVoucherNo.Length - Sufix.Length);
                                    }

                                    if (!string.IsNullOrEmpty(strVoucherNo))
                                    {
                                        resultArgs.ReturnValue = this.UtilityMember.NumberSet.ToInteger(strVoucherNo);
                                    }
                                }
                                else
                                {
                                    resultArgs.Message = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.REGENERATE_VOUCHERS);
                                }
                            }
                            else
                            {
                                resultArgs.Message = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_NUMBER_FORMAT_IS_MANUAL) + Vouchertype + this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        private void ShowInsertVoucherForm()
        {
            try
            {
                if (VoucherMasterId != 0)
                {
                    if (gvTransaction.RowCount != 0)
                    {
                        string VoucherNo = string.Empty;
                        string RunningDigit = string.Empty;
                        int ProId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
                        string Pro = glkpProject.Text.ToString();
                        string CurrentVoucherDate = gvTransaction.GetFocusedRowCellValue(colVoucherDate) != null ? gvTransaction.GetFocusedRowCellValue(colVoucherDate).ToString() : string.Empty;
                        if (Vouchertype == DefaultVoucherTypes.Receipt.ToString()) { VoucherIndex = 0; } else if (Vouchertype == DefaultVoucherTypes.Payment.ToString()) { VoucherIndex = 1; } else { VoucherIndex = 2; }
                        if (!IsLockedTransaction(this.UtilityMember.DateSet.ToDate(CurrentVoucherDate, false)))
                        {
                            DataRow drVoucher = gvTransaction.GetDataRow(gvTransaction.FocusedRowHandle); // gvTransaction.GetFocusedRowCellValue(colVoucherNo) != null ? gvTransaction.GetFocusedRowCellValue(colVoucherNo).ToString() : string.Empty;
                            if (drVoucher != null)
                            {
                                VoucherNo = drVoucher["VOUCHER_NO"].ToString();
                                resultArgs = SplitRunningDigit(VoucherNo);
                                if (resultArgs.Success)
                                {
                                    if (resultArgs.ReturnValue != null)
                                    {
                                        RunningDigit = resultArgs.ReturnValue.ToString();
                                        frmTransactionMultiAdd frmTransaction = new frmTransactionMultiAdd(CurrentVoucherDate, ProId, Pro, (int)AddNewRow.NewRow, VoucherIndex, VoucherNo, RunningDigit, VoucherDefinitionId);
                                        frmTransaction.UpdateHeld += new EventHandler(OnUpdateHeld);
                                        frmTransaction.ShowDialog();
                                    }
                                }
                                else
                                {
                                    this.ShowMessageBoxWarning(resultArgs.Message);
                                }
                            }
                        }
                        else
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED) + glkpProject.Text + "'");
                            //" during the period " + this.UtilityMember.DateSet.ToDate(dtLockDateFrom.ToShortDateString()) +
                            //" - " + this.UtilityMember.DateSet.ToDate(dtLockDateTo.ToShortDateString()));
                        }
                    }
                    else
                    {
                        ShowTransactionForm();
                    }
                }
                else
                {
                    ShowTransactionForm();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void rbtnLockTrans_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)
                {
                    DateTime dtTransDate = gvTransaction.GetFocusedRowCellValue(colVoucherDate) != null ? this.UtilityMember.DateSet.ToDate(gvTransaction.GetFocusedRowCellValue(colVoucherDate).ToString(), false) : DateTime.MinValue;
                    if (!dtTransDate.Equals(DateTime.MinValue))
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void FetchDateDuration()
        {
            try
            {
                using (AuditLockTransSystem Auditsys = new AuditLockTransSystem())
                {
                    Auditsys.ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
                    Auditsys.DateFrom = deDateFrom.DateTime;
                    Auditsys.DateTo = deDateTo.DateTime;
                    resultArgs = Auditsys.FetchAuditDetailByProjectDateRange();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        dtAuditLockDetails = resultArgs.DataSource.Table;
                        colTranLocked.Visible = true;
                        //dtLockDateFrom = this.UtilityMember.DateSet.ToDate(dtAuditLockDetails.Rows[0][AuditSystem.AppSchema.AuditLockTransType.DATE_FROMColumn.ColumnName].ToString(), false);
                        //dtLockDateTo = this.UtilityMember.DateSet.ToDate(dtAuditLockDetails.Rows[0][AuditSystem.AppSchema.AuditLockTransType.DATE_TOColumn.ColumnName].ToString(), false);
                    }
                    else
                    {
                        colTranLocked.Visible = false;
                        dtAuditLockDetails = null;
                        //dtLockDateFrom = dtLockDateTo = DateTime.MinValue;
                    }

                    //On 07/02/2024, For SDBINM, Lock Voucehrs before grace period
                    if (dtAuditLockDetails == null && this.AppSetting.IS_SDB_INM && this.AppSetting.VoucherGraceDays> 0)
                    {
                        dtAuditLockDetails = Auditsys.AppSchema.AuditLockTransType.DefaultView.ToTable();
                        DataRow dr = dtAuditLockDetails.NewRow();
                        dr[Auditsys.AppSchema.AuditLockTransType.DATE_FROMColumn.ColumnName] = this.AppSetting.GraceLockDateFrom;
                        dr[Auditsys.AppSchema.AuditLockTransType.DATE_TOColumn.ColumnName] = this.AppSetting.GraceLockDateTo;
                        dtAuditLockDetails.Rows.Add(dr);

                        //bool b = (startTime1 <= endTime2 && startTime2 <= endTime1;);
                        if ((deDateFrom.DateTime <= this.AppSetting.GraceLockDateTo  || deDateTo.DateTime <= this.AppSetting.GraceLockDateFrom))  
                        {
                            colTranLocked.Visible = true;
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

        private void gvTransaction_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (gvTransaction.RowCount > 0)
                {
                    //if (dtLockDateFrom != null && dtLockDateTo != null && (!dtLockDateFrom.Equals(DateTime.MinValue)) && (!dtLockDateTo.Equals(DateTime.MinValue)))
                    if (dtAuditLockDetails != null && dtAuditLockDetails.Rows.Count > 0 && colTranLocked.Visible)
                    {
                        DateTime dtTransDate = gvTransaction.GetRowCellValue(e.RowHandle, colVoucherDate) != null ? this.UtilityMember.DateSet.ToDate(gvTransaction.GetRowCellValue(e.RowHandle, colVoucherDate).ToString(), false) : DateTime.MinValue;
                        if (!dtTransDate.Equals(DateTime.MinValue))
                        {
                            //if (dtTransDate >= dtLockDateFrom && dtTransDate <= dtLockDateTo)
                            //dtAuditLockDetails.DefaultView.RowFilter = "('" + UtilityMember.DateSet.ToDate(dtTransDate.ToShortDateString()) + "'>= DATE_FROM) AND " +
                            //                                              "('" + UtilityMember.DateSet.ToDate(dtTransDate.ToShortDateString()) + "'<= DATE_TO)";
                            dtAuditLockDetails.DefaultView.RowFilter = "('" + UtilityMember.DateSet.ToDate(dtTransDate.ToShortDateString()) + "'>= DATE_FROM) AND " +
                                                                          "('" + UtilityMember.DateSet.ToDate(dtTransDate.ToShortDateString()) + "'< DATE_TO)";
                            //Check temporary relaxation
                            bool isEnforceTmpRelaxation = this.AppSetting.IsTemporaryGraceLockRelaxDate(dtTransDate);
                            
                            if (dtAuditLockDetails.DefaultView.Count > 0 && !isEnforceTmpRelaxation)
                            {
                                rbtnLockTrans.Buttons[0].Image = imgTransView.Images[0];
                                e.Handled = false;
                            }
                            else
                            {
                                //20/09/2021, to disable unlock cell
                                rbtnLockTrans.Buttons[0].Image = null; //imgTransView.Images[4];
                            }
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

        public bool IsLockedTransaction(DateTime dtVoucherDate)
        {
            bool isSuccess = false;
            try
            {
                //if (dtLockDateFrom != DateTime.MinValue && dtLockDateTo != DateTime.MinValue)
                if (dtAuditLockDetails != null && dtAuditLockDetails.Rows.Count > 0)
                {
                    //Check temporary relaxation
                    bool isEnforceTmpRelaxation = this.AppSetting.IsTemporaryGraceLockRelaxDate(dtVoucherDate);

                    //dtAuditLockDetails.DefaultView.RowFilter = "('" + UtilityMember.DateSet.ToDate(dtVoucherDate.ToShortDateString()) + "'>= DATE_FROM) AND " +
                    //                    "('" + UtilityMember.DateSet.ToDate(dtVoucherDate.ToShortDateString()) + "'<= DATE_TO)";

                    dtAuditLockDetails.DefaultView.RowFilter = "('" + UtilityMember.DateSet.ToDate(dtVoucherDate.ToShortDateString()) + "'>= DATE_FROM) AND " +
                                        "('" + UtilityMember.DateSet.ToDate(dtVoucherDate.ToShortDateString()) + "'< DATE_TO)";

                    if (dtAuditLockDetails.DefaultView.Count > 0 && !isEnforceTmpRelaxation)
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

        private void ApplyRecentPrjectDetails(int proid)
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
                    resultArgs = accountingSystem.FetchRecentVoucherDate(proid);
                    if (resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtProject = resultArgs.DataSource.Table;
                        foreach (DataRow dr in dtProject.Rows)
                        {
                            if (string.IsNullOrEmpty(dr[accountingSystem.AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName].ToString()))
                            {
                                dr[accountingSystem.AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName] = dtbookbeginfrom > dtyearfrom ? dtbookbeginfrom : dtyearfrom;
                            }
                        }
                        this.AppSetting.UserProjectInfor = resultArgs.DataSource.Table.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        private void chkColumnChooser_CheckedChanged(object sender, EventArgs e)
        {
            if (chkColumnChooser.Checked)
            {
                gvTransaction.ColumnsCustomization();
            }
            else
            {
                gvTransaction.DestroyCustomization();
            }
        }

        private void frmTransactionView_Activated(object sender, EventArgs e)
        {
            /*SetVisibileShortCuts(true, true, true);
            deDateFrom.DateTime = deDateFrom.DateTime;
            deDateTo.DateTime = deDateFrom.DateTime.AddMonths(1).AddDays(-1);
            LoadProject();
            LoadVoucherDetails();
            gvTransaction.FocusedRowHandle = 0;
            gvVoucher.OptionsView.ShowFooter = true;
            glkpProject.EditValue = ProjectId;
            if (TransSelectionType == 0) { ShowTransactionForm(); }
            ucTrans.VisibleNegativeBalance = BarItemVisibility.Always;
            FetchDateDuration();*/

            LoadDefaultValues();
        }

        private void gcTransaction_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (gcTransaction.IsFocused)
                {
                    ShowVoucherMasterForm();
                }
            }
        }

        private void ucTrans_DownloadExcel(object sender, EventArgs e)
        {
            using (frmExcelSupport excelSupport = new frmExcelSupport(MasterImport.Transaction.ToString(), MasterImport.Transaction))
            {
                excelSupport.UpdateHeld += new EventHandler(OnUpdateHeld);
                excelSupport.ShowDialog();
            }
        }

        private void gvLedgerDetails_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            /*this.ApplyProjectCurrencySetting(ProjectId);
            rtxtCCAmount.Mask.Culture = System.Threading.Thread.CurrentThread.CurrentUICulture;
            rtxtCCAmount.Mask.UseMaskAsDisplayFormat = true;*/
            
            LoadCCDetailsByVoucherId();
            //this.ApplyGlobalSetting();
        }

        private void LoadTransDetailsByVoucherId()
        {
            if (dtVoucherDetails != null && dtVoucherDetails.Rows.Count > 0)
            {
                DataView dvFilter = dtVoucherDetails.AsDataView();
                dvFilter.RowFilter = "VOUCHER_ID =" + VoucherMasterId;
                gcLedgerDetails.DataSource = null;

                if (dvFilter != null)
                {
                    gcLedgerDetails.DataSource = dvFilter.ToTable();
                    gcLedgerDetails.RefreshDataSource();
                    gvLedgerDetails.FocusedRowHandle = 0;
                    gvLedgerDetails.FocusRectStyle = DrawFocusRectStyle.RowFocus;
                }
            }
        }

        private void LoadCCDetailsByVoucherId()
        {
            if (dtVoucherCostcentreDetails != null && dtVoucherCostcentreDetails.Rows.Count > 0)
            {
                DataView dvFilter = dtVoucherCostcentreDetails.AsDataView();
                dvFilter.RowFilter = "LEDGER_ID = " + CostCentreLedgerId + " AND LEDGER_SEQUENCE_NO = " + LedgerSequenceNo + " AND  VOUCHER_ID = " + VoucherMasterId;
                gcCostCentreDetails.DataSource = null;

                if (dvFilter != null)
                {
                    gcCostCentreDetails.DataSource = dvFilter.ToTable();
                }
            }
        }

        private void gvTransaction_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            /*//On 13/08/2024, Apply Project Currency Setting
            this.ApplyProjectCurrencySetting(ProjectId);
            rtxtLedCredit.Mask.Culture = System.Threading.Thread.CurrentThread.CurrentUICulture;
            rtxtLedCredit.Mask.UseMaskAsDisplayFormat = true;
            rtxtLedDebit.Mask.Culture = System.Threading.Thread.CurrentThread.CurrentUICulture;
            rtxtLedDebit.Mask.UseMaskAsDisplayFormat = true;*/

            LoadTransDetailsByVoucherId();

            //For few features permentaly will be locked for SDBINM -----------------
            colPrint.ToolTip = "To Print Voucher (Ctl+P)";
            colDuplicate.ToolTip = "To make Replicate Voucher (Alt+U)";
            ucTrans.DisableEditButton = ucTrans.DisableDeleteButton = ucTrans.DisableInsertVoucher = ucTrans.DisableMoveTransaction = true;
            if (VoucherDefinitionId == (Int32)DefaultVoucherTypes.Receipt)
            {
                EnforceReceiptModule(new object[] { ucTrans }, true);

                if (this.AppSetting.IS_SDB_INM)
                {
                    colPrint.ToolTip = MessageCatalog.Common.COMMON_RECEIPT_DISABLED_MESSAGE;
                    colDuplicate.ToolTip = MessageCatalog.Common.COMMON_RECEIPT_DISABLED_MESSAGE;
                }
            }
            //-----------------------------------------------------------------------
            //On 13/08/2024, Reset to Global Currency Setting
            //this.ApplyGlobalSetting();
        }

        private void gvTransaction_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //On 05/06/2017, hide zero value in the grid
            if (e.Column == colDebit || e.Column == colDebitAmount || e.Column == colCredit)
                if (e.Value != null && UtilityMember.NumberSet.ToDecimal(e.Value.ToString()) == 0) e.DisplayText = "";
        }

        private void gvLedgerDetails_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //On 05/06/2017, hide zero value in the grid
            if (e.Column == ggcolVoucherTransCredit || e.Column == ggcolVoucherTransDebit)
                if (e.Value != null && UtilityMember.NumberSet.ToDecimal(e.Value.ToString()) == 0) e.DisplayText = "";
        }

        private void rbtnDuplicateVoucher_Click(object sender, EventArgs e)
        {
            //On 13/02/2018, To make duplucate voucher
            string VoucherSubType = gvTransaction.GetFocusedRowCellValue(colVoucherSubType) != null ? gvTransaction.GetFocusedRowCellValue(colVoucherSubType).ToString() : string.Empty;
            if (!string.IsNullOrEmpty(Vouchertype))
            {
                //13/05/2019, to allow Contra vouchers also can be replicated
                //if ((Vouchertype == DefaultVoucherTypes.Receipt.ToString() || Vouchertype == DefaultVoucherTypes.Payment.ToString())
                //    && (VoucherSubType == ledgerSubType.GN.ToString()))
                if ((Vouchertype == DefaultVoucherTypes.Receipt.ToString() || Vouchertype == DefaultVoucherTypes.Payment.ToString() || Vouchertype == DefaultVoucherTypes.Contra.ToString())
                    && (VoucherSubType == ledgerSubType.GN.ToString()))
                {
                    if (((CommonMethod.ApplyUserRightsForTransaction((int)Receipt.EditReceiptVoucher) != 0)) ||
                    (CommonMethod.ApplyUserRightsForTransaction((int)Contra.EditContraVoucher) != 0) ||
                    (CommonMethod.ApplyUserRightsForTransaction((int)Payment.EditPaymentVoucher) != 0) || this.LoginUser.IsFullRightsReservedUser)
                    {
                        if (VoucherDefinitionId == (Int32)DefaultVoucherTypes.Receipt && !AppSetting.ENABLE_TRACK_RECEIPT_MODULE)
                        {
                            this.ShowMessageBox(MessageCatalog.Common.COMMON_RECEIPT_DISABLED_MESSAGE);
                        }
                        else
                        {
                            if (this.ShowConfirmationMessage("Do you want to Replicate Voucher Entry ?",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                            {
                                ShowVoucherMasterForm(true);
                            }
                        }
                    }
                    else
                    {
                        if (Vouchertype == DefaultVoucherTypes.Receipt.ToString())
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.NO_RIGHTS_TO_EDIT_RECEIPT));
                        }
                        else if (Vouchertype == DefaultVoucherTypes.Payment.ToString())
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.NO_RIGHTS_TO_EDIT_PAYMENT));
                        }
                        else
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.NO_RIGHTS_TO_EDIT_CONTRA));
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox("Receipts/Payments/Contra Vouchers alone can be Replicated");
                }
            }
        }

        private void deDateFrom_EditValueChanged(object sender, EventArgs e)
        {
            //On 12/07/2018, For closed Projects----
            LoadProject();
            //--------------------------------------
        }

        private void glkpProject_QueryPopUp(object sender, CancelEventArgs e)
        {
            //19/07/2021, To set Popup widow size
            if (sender != null)
            {
                GridLookUpEdit editor = (GridLookUpEdit)sender;
                SetGridLookPopupWindowSize(editor);
            }
        }

        private void gvTransaction_HideCustomizationForm(object sender, EventArgs e)
        {
            chkColumnChooser.Checked = false;
        }

        private void gvTransaction_ShowCustomizationForm(object sender, EventArgs e)
        {
            if (gvTransaction.CustomizationForm != null)
            {
                CustomizationForm customizationForm = gvTransaction.CustomizationForm;
                if (customizationForm.ActiveListBox != null)
                {
                    customizationForm.ActiveListBox.DoubleClick -= new EventHandler(ActiveListBox_DoubleClick);
                    customizationForm.ActiveListBox.DoubleClick += new EventHandler(ActiveListBox_DoubleClick);
                }
            }  
        }

        private void ActiveListBox_DoubleClick(object sender, EventArgs e)
        {
            ColumnCustomizationListBox columnCustomizationListBox = sender as ColumnCustomizationListBox;
            if (columnCustomizationListBox != null)
            {
                CustomizationForm customizationForm = columnCustomizationListBox.CustomizationForm;
                if (customizationForm != null)
                {
                    GridColumn pressedItem = customizationForm.PressedItem as GridColumn;
                    if (pressedItem != null)
                    {
                        pressedItem.VisibleIndex = colCredit.VisibleIndex + 1;
                        gvTransaction.GetVisibleColumn(pressedItem.VisibleIndex).VisibleIndex = -1;
                    }
                }
            }  
        }

        private void glkpProject_Properties_Popup(object sender, EventArgs e)
        {
            GridLookUpEdit edit = sender as GridLookUpEdit;
            if (edit != null)
            {
                PopupGridLookUpEditForm f = (edit as IPopupControl).PopupWindow as PopupGridLookUpEditForm;
                f.Width = glkpProject.Width;
            }
        }

        private void rbtnGSTInvoicePrint_Click(object sender, EventArgs e)
        {
            PrintGSTInvoice(VoucherMasterId);
        }

        private void gvTransaction_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {

            e.DropInfo.Valid = (e.DropInfo.Index != -100);
            
        }

        private void togShowLedgerCCDetails_Toggled(object sender, EventArgs e)
        {
            lcgLedgerDetails.Visibility = lcgCCDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            if (!togShowLedgerCCDetails.IsOn)
            {
                lcgLedgerDetails.Visibility = lcgCCDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private void togShowOPBalance_Toggled(object sender, EventArgs e)
        {
            pnlProject.Visible = true;
            if (!togShowOPBalance.IsOn)
            {
                pnlProject.Visible = false;
            }
        }

        private void togShowCLBalance_Toggled(object sender, EventArgs e)
        {
            pnlClosingBal.Visible = true;
            if (!togShowCLBalance.IsOn)
            {
                pnlClosingBal.Visible = false;
            }
        }

         private Image GetImage()
        {
            DevExpress.Skins.Skin currentSkin = DevExpress.Skins.EditorsSkins.GetSkin(defaultLookAndFeel1.LookAndFeel);
            string elementName = DevExpress.Skins.EditorsSkins.SkinToggleSwitch;
            DevExpress.Skins.SkinElement element = currentSkin[elementName];
            Image image = element.Image.Image;
            return image;
        }

        private void SetToggleColor(bool state)
        {
            /*Image image = GetImage();

            Brush currentBrush = (state) ? Brushes.DarkBlue : Brushes.DarkRed;

            using (var graphics = Graphics.FromImage(image))
            {
                graphics.FillRectangle(currentBrush, new Rectangle(0, 0, image.Width, image.Height));
            }*/
        }

        private void togShowLedgerCCDetails_EditValueChanged(object sender, EventArgs e)
        {
            Bosco.Utility.Controls.MyToggleSwitch mytoggle = sender as Bosco.Utility.Controls.MyToggleSwitch;
            SetToggleColor(mytoggle.IsOn);           
        }

        private void togShowOPBalance_EditValueChanged(object sender, EventArgs e)
        {
            Bosco.Utility.Controls.MyToggleSwitch mytoggle = sender as Bosco.Utility.Controls.MyToggleSwitch;
            SetToggleColor(mytoggle.IsOn);       
        }
                
        private void zoomctl_EditValueChanged(object sender, EventArgs e)
        {
            Int32 val = Convert.ToInt32(zoomctl.EditValue);

            //Font fnt = new Font(gvTransaction.Appearance.Row.Font.Name, val);
            //Font focusedfnt = new Font(gvTransaction.Appearance.SelectedRow.Font.Name, val);

            float fontSize = defaultFontSize;
            fontSize += Convert.ToInt32(zoomctl.EditValue);
            if (fontSize > 0)
            {
                Font fnt = new Font(gvTransaction.Appearance.Row.Font.Name, fontSize);
                Font focusedfnt = new Font(gvTransaction.Appearance.SelectedRow.Font.Name, fontSize);

                gvTransaction.Appearance.HeaderPanel.Font = fnt;
                gvTransaction.Appearance.Row.Font = fnt;
                gvTransaction.Appearance.SelectedRow.Font = gvTransaction.Appearance.FocusedRow.Font = focusedfnt;
                Int32 zoomvalue = UtilityMember.NumberSet.ToInteger(zoomctl.EditValue.ToString());
                Int32 zoommiddlevalue =  zoomctl.Properties.Middle;

                var value = ((double)zoomvalue / zoommiddlevalue) * 100;
                var percentage = Convert.ToInt32(Math.Round(value, 0));
                lblZoomText.Text = percentage.ToString() +"%";
            }
        }

        private void ucTrans_PostInterestClicked(object sender, EventArgs e)
        {
            ShowVoucherFiles();
        }
        
        private void ShowVoucherFiles()
        {
            if (this.AppSetting.AttachVoucherFiles == 1)
            {
                frmAttachVoucherFiles frmimage = new frmAttachVoucherFiles(VoucherMasterId);
                frmimage.ShowDialog();
            }
        }

        private void pnlFooter_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    // Author    : Carmel Raj M
    //Created On : 30-September-2015
    //Purpose    : A Common class for routing of Edit and delete for all the modules wherever its applicable

    internal class RouteVoucher : frmFinanceBase
    {
        #region Variables
        ResultArgs resultArgs;
        #endregion

        #region Constructor
        public RouteVoucher()
        {

        }
        #endregion

        #region Methods
        #region Voucher Router Edit
        /// <summary>
        /// This methods routes to the corresponding form in Edit Mode having the form object, VoucherId and VoucherSubType as parameter
        /// </summary>
        /// <param name="BaseObject">Objcet of frmTransactionView</param>
        /// <param name="VoucherId">Voucher Id</param>
        /// <param name="voucherSubType">Voucher SubType</param>
        public void RouteVoucherEdit(frmTransactionView BaseObject, int VoucherId, VoucherSubTypes voucherSubType, bool MakeDuplicate = false)
        {
            switch (voucherSubType)
            {
                //-------This part of  Logic has been taken from the already available one  -------------
                case VoucherSubTypes.GN:
                case VoucherSubTypes.TDS:
                    {
                        if (!this.LoginUser.IsFullRightsReservedUser)
                        {
                            if (BaseObject.Vouchertype == DefaultVoucherTypes.Receipt.ToString())
                            {
                                if (CommonMethod.ApplyUserRightsForTransaction((int)Receipt.EditReceiptVoucher) != 0)
                                {
                                    if (!BaseObject.IsLockedTransaction(BaseObject.dtSelectedVoucherDate))
                                    {
                                        if (BaseObject.Vouchertype == DefaultVoucherTypes.Receipt.ToString() && !this.AppSetting.ENABLE_TRACK_RECEIPT_MODULE)
                                        {
                                            this.ShowMessageBox(MessageCatalog.Common.COMMON_RECEIPT_DISABLED_MESSAGE);
                                        }
                                        else
                                        {
                                            frmTransactionMultiAdd frmTrans = new frmTransactionMultiAdd(BaseObject.deDateFrom.Text, BaseObject.ProjectId, BaseObject.ProjectName, VoucherId, BaseObject.VoucherIndex, MakeDuplicate);
                                            frmTrans.UpdateHeld += new EventHandler(OnUpdateHeld);
                                            frmTrans.ShowDialog();
                                        }
                                    }
                                    else
                                    {
                                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED_CANNOT_EDIT) + BaseObject.ProjectName + "'");
                                    }
                                }
                                else
                                {
                                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.NO_RIGHTS_TO_EDIT_RECEIPT));
                                }
                            }
                            else if (BaseObject.Vouchertype == DefaultVoucherTypes.Payment.ToString())
                            {
                                if (CommonMethod.ApplyUserRightsForTransaction((int)Payment.EditPaymentVoucher) != 0)
                                {
                                    if (!BaseObject.IsLockedTransaction(BaseObject.dtSelectedVoucherDate))
                                    {
                                        frmTransactionMultiAdd frmTrans = new frmTransactionMultiAdd(BaseObject.deDateFrom.Text, BaseObject.ProjectId, BaseObject.ProjectName, VoucherId, BaseObject.VoucherIndex, MakeDuplicate);
                                        frmTrans.UpdateHeld += new EventHandler(OnUpdateHeld);
                                        frmTrans.ShowDialog();
                                    }
                                    else
                                    {
                                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED) + BaseObject.ProjectName + "'");
                                    }
                                }
                                else
                                {
                                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.NO_RIGHTS_TO_EDIT_PAYMENT));
                                }
                            }
                            else
                            {
                                if (CommonMethod.ApplyUserRightsForTransaction((int)Contra.EditContraVoucher) != 0)
                                {
                                    if (!BaseObject.IsLockedTransaction(BaseObject.dtSelectedVoucherDate))
                                    {
                                        frmTransactionMultiAdd frmTrans = new frmTransactionMultiAdd(BaseObject.deDateFrom.Text, BaseObject.ProjectId, BaseObject.ProjectName, VoucherId, BaseObject.VoucherIndex, MakeDuplicate);
                                        frmTrans.UpdateHeld += new EventHandler(OnUpdateHeld);
                                        frmTrans.ShowDialog();
                                    }
                                    else
                                    {
                                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED_CANNOT_EDIT) + BaseObject.ProjectName + "'");
                                    }
                                }
                                else
                                {
                                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.NO_RIGHTS_TO_EDIT_CONTRA));
                                }
                            }
                        }
                        else
                        {
                            if (!BaseObject.IsLockedTransaction(BaseObject.dtSelectedVoucherDate))
                            {
                                if (BaseObject.Vouchertype == DefaultVoucherTypes.Receipt.ToString() && !this.AppSetting.ENABLE_TRACK_RECEIPT_MODULE)
                                {
                                    this.ShowMessageBox(MessageCatalog.Common.COMMON_RECEIPT_DISABLED_MESSAGE);
                                }
                                else
                                {
                                    frmTransactionMultiAdd frmTrans = new frmTransactionMultiAdd(BaseObject.deDateFrom.Text, BaseObject.ProjectId, BaseObject.ProjectName, VoucherId, BaseObject.VoucherIndex, MakeDuplicate, BaseObject.VoucherDefinitionId);
                                    frmTrans.UpdateHeld += new EventHandler(OnUpdateHeld);
                                    frmTrans.ShowDialog();
                                }
                            }
                            else
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED_CANNOT_EDIT) + "'" + BaseObject.ProjectName + "'");
                            }
                        }
                        //----------------------------------------General voucher logic ends here ---------------------------------------------
                        break;
                    }
                case VoucherSubTypes.FD: //When Voucher Sub Type is FD the logic goes here
                    {
                        bool CanbeModified = false;
                        //bool rnt = true;
                        //On 17/10/2022, to lock to FD Vouchers--------------------------------------------------------------------------------------
                        //this.ShowMessageBox("Entry can be edited in the Fixed Deposit Module.");
                        //---------------------------------------------------------------------------------------------------------------------------
                        

                        //-------This FD Logic has been taken from the already available one and some modification is made to use the logic -------------
                        int fdAccountId = 0;
                        int FdVoucherID = 0;
                        int ProjId = 0;
                        int FDStatus = 0;
                        int FDCount = 0;
                        int FDRenewalId = 0;
                        string TempFdType = string.Empty;
                        string fdtype = string.Empty;
                        string fdBaseType = string.Empty;
                        FDTypes fdTypes;

                        fdAccountId = BaseObject.SetFDAccountId;
                        FdVoucherID = BaseObject.VoucherMasterId;
                        using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                        {
                            fdAccountSystem.FDAccountId = fdAccountId;
                            FDCount = fdAccountSystem.CountRenewalDetails();
                            fdAccountSystem.VoucherId = FdVoucherID;
                            resultArgs = fdAccountSystem.FetchFDAccountId();
                            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                            {
                                fdAccountSystem.FDAccountId = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName].ToString());
                                if (fdAccountSystem.CheckFDRenewalClosed() == 0)
                                {
                                    fdAccountId = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName].ToString()) : fdAccountId;
                                    //as on 03/11/2021
                                    FDRenewalId = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_RENEWAL_IDColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_RENEWAL_IDColumn.ColumnName].ToString()) : 0;
                                    ProjId = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.PROJECT_IDColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.PROJECT_IDColumn.ColumnName].ToString()) : 0;
                                    FDStatus = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_STATUSColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_STATUSColumn.ColumnName].ToString()) : 0;
                                    TempFdType = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName] != null ? resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName].ToString() : string.Empty;
                                    fdtype = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_TYPEColumn.ColumnName] != null ? resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_TYPEColumn.ColumnName].ToString() : string.Empty;
                                    fdBaseType = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.TRANS_TYPEColumn.ColumnName] != null ? resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.TRANS_TYPEColumn.ColumnName].ToString() : string.Empty;
                                }
                                else
                                {
                                    if (BaseObject.Vouchertype == DefaultVoucherTypes.Receipt.ToString())
                                    {
                                        if (fdAccountSystem.CheckRenewalTypeByVoucherId() == FDRenewalTypes.IRI.ToString())
                                        {
                                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_CANNOT_EDIT_FD_RECEIPT_ENTRY));
                                            return;
                                        }
                                        else
                                        {
                                            fdAccountId = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName].ToString()) : fdAccountId;
                                            //as on 03/11/2021
                                            FDRenewalId = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_RENEWAL_IDColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_RENEWAL_IDColumn.ColumnName].ToString()) : 0;
                                            ProjId = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.PROJECT_IDColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.PROJECT_IDColumn.ColumnName].ToString()) : 0;
                                            FDStatus = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_STATUSColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_STATUSColumn.ColumnName].ToString()) : 0;
                                            TempFdType = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName] != null ? resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName].ToString() : string.Empty;
                                            fdtype = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_TYPEColumn.ColumnName] != null ? resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_TYPEColumn.ColumnName].ToString() : string.Empty;
                                        }
                                    }
                                    else
                                    {
                                        if (fdAccountSystem.CheckRenewalTypeByVoucherId() != FDRenewalTypes.WDI.ToString())
                                        {
                                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_CANNOT_EDIT_FD_CONTRA_ENTRY));
                                            return;
                                        }
                                        else
                                        {
                                            fdAccountId = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName].ToString()) : fdAccountId;
                                            //as on 03/11/2021
                                            FDRenewalId = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_RENEWAL_IDColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_RENEWAL_IDColumn.ColumnName].ToString()) : 0;
                                            ProjId = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.PROJECT_IDColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.PROJECT_IDColumn.ColumnName].ToString()) : 0;
                                            FDStatus = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_STATUSColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_STATUSColumn.ColumnName].ToString()) : 0;
                                            TempFdType = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName] != null ? resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName].ToString() : string.Empty;
                                            fdtype = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_TYPEColumn.ColumnName] != null ? resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_TYPEColumn.ColumnName].ToString() : string.Empty;
                                        }
                                    }
                                }
                            }

                            //On 24/01/2023, To check FD voucher entry can be modified or not -----------------------------
                            CanbeModified = false;
                            ResultArgs result =  fdAccountSystem.IsAllowToModifyFDVoucherEntry(fdAccountId, FDRenewalId);
                            CanbeModified = result.Success;
                            if (!result.Success)
                            {
                                this.ShowMessageBoxWarning(result.Message);
                            }
                            //---------------------------------------------------------------------------------------------
                        }
                        if (BaseObject.Vouchertype == DefaultVoucherTypes.Contra.ToString())
                        {
                            if (fdtype == FDTypes.WD.ToString())
                                fdTypes = FDTypes.WD;
                            else if (fdtype == FDTypes.PWD.ToString())  // by aldrin when the type is PWD show the width drwal screen.
                                fdTypes = FDTypes.PWD;
                            else if (fdtype == FDTypes.RIN.ToString() && fdBaseType == FDTypes.RIN.ToString())
                                fdTypes = FDTypes.RIN;
                            else
                                fdTypes = FDTypes.IN;
                        }
                        else
                        {
                            if (fdtype == FDTypes.WD.ToString())
                                fdTypes = FDTypes.WD;
                            else if (fdtype == FDTypes.PWD.ToString()) // by aldrin when the type is PWD show the width drwal screen.
                                fdTypes = FDTypes.PWD;
                            else if (fdtype == FDTypes.POI.ToString())
                                fdTypes = FDTypes.POI;
                            else
                                fdTypes = FDTypes.RN;
                        }
                        if (CanbeModified)
                        {
                            if (FDStatus != 0 || TempFdType != FDRenewalTypes.IRI.ToString())
                            {
                                ACPP.Modules.Master.frmFDAccount frmAccount = new frmFDAccount(fdAccountId, FdVoucherID, fdTypes);
                                //**************added by suagn---to route the post interest record***********************************************************************************
                                if (fdTypes == FDTypes.POI)
                                {
                                    frmAccount.PostInterestCreatedDate = BaseObject.dtSelectedVoucherDate;
                                }
                                //*************************************************************************************************
                                frmAccount.ProjectId = ProjId;
                                frmAccount.FDRenewalCount = FDCount;
                                frmAccount.ShowDialog();
                                //  BaseObject.LoadVoucherDetails();
                            }
                        }
                        //----------------------------------------FD Logic ends here ---------------------------------------------

                        break;
                    }
                case VoucherSubTypes.AST://When the voucher sub type is Asset than the logic here
                    {
                        //--------------------------------------Asset logic starts here--------------------------------------------
                        //Added by Carmel Raj M
                        int PrimaryId;
                        switch (AnalyzeAssetType(VoucherId, out PrimaryId))
                        {
                            case AssetSourceFlag.Purchase:
                                {
                                    frmInwardVoucherAdd AssetPurchase = new frmInwardVoucherAdd(BaseObject.deDateFrom.DateTime.ToString(), BaseObject.ProjectId, BaseObject.ProjectName, PrimaryId, AssetInOut.IK);
                                    AssetPurchase.ShowDialog();
                                    break;
                                }
                            case AssetSourceFlag.Sales:
                                {
                                    frmAssetOutward AssetsSales = new frmAssetOutward(BaseObject.ProjectId, BaseObject.ProjectName, PrimaryId, BaseObject.deDateFrom.DateTime.ToString());
                                    AssetsSales.ShowDialog();
                                    break;
                                }
                        }
                        break;
                        //--------------------------------------Asset logic ends here--------------------------------------------
                    }
                case VoucherSubTypes.STK: //When the voucher sub type is Stock then the logic here
                    {
                        //--------------------------------------Stock logic starts here--------------------------------------------
                        //Added by Carmel Raj M
                        int PrimaryId;
                        switch (AnalyzeStockType(VoucherId, out PrimaryId)) //Analyzes the Stock Type
                        {
                            case StockType.Purchase:
                                {
                                    //0= Purchase and 1= Receive PurchaseType is fixed with 1 because only Purchase will have vouhcer Entry 
                                    // Purchase receive will not have voucher entry
                                    int PurchaseType = 0;
                                    frmPurchaseStockAdd StockPurchase = new frmPurchaseStockAdd(PrimaryId, BaseObject.ProjectId, BaseObject.ProjectName, BaseObject.deDateFrom.DateTime, PurchaseType);
                                    StockPurchase.ShowDialog();
                                    break;
                                }
                            case StockType.Sales:
                                {
                                    int stockType = 0;
                                    frmUtiliseOrSoldItems StockSales = new frmUtiliseOrSoldItems(PrimaryId, BaseObject.deDateFrom.DateTime.ToString(), BaseObject.ProjectId, BaseObject.ProjectName, stockType);
                                    StockSales.ShowDialog();
                                    break;
                                }
                            case StockType.PurchaseReturns:
                                {
                                    frmGoodsReturnAdd PurchaseReturn = new frmGoodsReturnAdd(PrimaryId, BaseObject.ProjectId, BaseObject.ProjectName, this.UtilityMember.DateSet.ToDate(BaseObject.deDateFrom.DateTime.ToString(), false));
                                    PurchaseReturn.ShowDialog();
                                    break;
                                }
                        }
                        break;
                        //--------------------------------------Stock logic ends here--------------------------------------------
                    }
                case VoucherSubTypes.PAY:
                    {
                        //--------------------------------------Payroll logic starts here--------------------------------------------
                        //Added by Carmel Raj M
                        //On 18/06/2019, to remove existing------------------------------
                        //int PrimaryId;
                        //switch (AnalyzePayRollType(VoucherId, out PrimaryId))
                        //{
                        //    case PayRoll.IssueLoan:
                        //        {
                        //            frmAddLoanManagement PayRoleIssueLoan = new frmAddLoanManagement(PrimaryId);
                        //            PayRoleIssueLoan.ShowDialog();
                        //            break;
                        //        }
                        //    case PayRoll.PostVoucher:
                        //        {
                        //            frmPostPayment PayRolePostVoucher = new frmPostPayment(PrimaryId);
                        //            PayRolePostVoucher.ShowDialog();
                        //            break;
                        //        }
                        //}
                        frmPostPaymentVoucher frmpostvoucher = new frmPostPaymentVoucher(VoucherId);
                        frmpostvoucher.ShowDialog();
                        //-----------------------------------------------------------------------
                        break;
                        //--------------------------------------Payroll logic ends here--------------------------------------------
                    }
            }
        }
        #endregion

        #region Voucher Router Delete
        /// <summary>
        /// This mehtod deletes voucher entry for All the voucher based on voucher Id and VoucherSubTypes
        /// </summary>
        /// <param name="BaseObject">Objcet of frmTransactionView</param>
        /// <param name="VoucherId">Voucher Id</param>
        /// <param name="voucherSubType">Voucher SubType</param>
        public void RouteVoucherDelete(frmTransactionView BaseObject, int VoucherId, VoucherSubTypes voucherSubType)
        {
            switch (voucherSubType)
            {
                //-------This part of  Logic has been taken from the already available one  -------------
                case VoucherSubTypes.GN:
                case VoucherSubTypes.TDS:
                    {
                        if (!this.LoginUser.IsFullRightsReservedUser)
                        {
                            if (BaseObject.Vouchertype == DefaultVoucherTypes.Receipt.ToString())
                            {
                                if (CommonMethod.ApplyUserRightsForTransaction((int)Receipt.DeleteReceiptVoucher) != 0)
                                {
                                    if (!BaseObject.IsLockedTransaction(BaseObject.dtSelectedVoucherDate))
                                    {
                                        BaseObject.DeleteVoucherTrans(VoucherId);
                                    }
                                    else
                                    {
                                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED_CANNOT_DELETE) + BaseObject.ProjectName + "'");
                                    }

                                }
                                else
                                {
                                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.NO_RIGHTS_TO_DELETE_RECEIPT));
                                }
                            }
                            else if (BaseObject.Vouchertype == DefaultVoucherTypes.Payment.ToString())
                            {
                                if (CommonMethod.ApplyUserRightsForTransaction((int)Payment.DeletePaymentVoucher) != 0)
                                {
                                    if (!BaseObject.IsLockedTransaction(BaseObject.dtSelectedVoucherDate))
                                    {
                                        BaseObject.DeleteVoucherTrans(VoucherId);
                                    }
                                    else
                                    {
                                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED_CANNOT_DELETE) + BaseObject.ProjectName + "'");
                                    }
                                }
                                else
                                {
                                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.NO_RIGHTS_TO_DELETE_PAYMENT));
                                }
                            }
                            else
                            {
                                if (CommonMethod.ApplyUserRightsForTransaction((int)Contra.DeleteContraVoucher) != 0)
                                {
                                    if (!BaseObject.IsLockedTransaction(BaseObject.dtSelectedVoucherDate))
                                    {
                                        BaseObject.DeleteVoucherTrans(VoucherId);
                                    }
                                    else
                                    {
                                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED_CANNOT_DELETE) + BaseObject.ProjectName + "'");
                                    }
                                }
                                else
                                {
                                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.NO_RIGHTS_TO_DELETE_CONTRA));
                                }
                            }
                        }
                        else
                        {
                            if (!BaseObject.IsLockedTransaction(BaseObject.dtSelectedVoucherDate))
                            {
                                BaseObject.DeleteVoucherTrans(VoucherId);
                            }
                            else
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED_CANNOT_DELETE) + "'" + BaseObject.ProjectName + "'");
                            }
                        }
                        //----------------------------------------General voucher logic ends here ---------------------------------------------
                        break;
                    }
                case VoucherSubTypes.FD: //When Voucher Sub Type is FD the logic goes here
                    {
                        bool CanbeModified = false;
                        ////On 17/10/2022, to lock to FD Vouchers--------------------------------------------------------------------------------------
                        //this.ShowMessageBox("Entry can be deleted in the Fixed Deposit Module.");

                        //On 24/01/2023, To check FD voucher entry can be modified or not -----------------------------
                        using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                        {
                            ResultArgs result = fdAccountSystem.IsAllowToModifyFDVoucherEntry(0, 0, VoucherId);
                            CanbeModified = result.Success;
                            if (!result.Success)
                            {
                                this.ShowMessageBoxWarning(result.Message);
                            }
                        }
                        //---------------------------------------------------------------------------------------------
                        ////---------------------------------------------------------------------------------------------------------------------------
                        
                        //-------This FD Logic has been taken from the already available one and some modification is made ----------------------------
                        if (CanbeModified)
                        {
                            using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                            {
                                voucherTransaction.VoucherId = VoucherId;
                                using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                                {
                                    fdAccountSystem.VoucherId = VoucherId;
                                    resultArgs = fdAccountSystem.FetchFDAccountId();
                                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                                    {
                                        fdAccountSystem.FDVoucherId = VoucherId;
                                        fdAccountSystem.FDAccountId = fdAccountSystem.FetchFDAId();
                                        if (fdAccountSystem.FDAccountId != 0)
                                        {
                                            if (fdAccountSystem.CheckFDAccountExists() == 0)
                                            {
                                                //Modified by Carmel Raj on 20-Oct-2015
                                                //Purpose :Avoiding Delete Confirmation Message replication,since the Confirmation message is given in voucher routing itseft,

                                                //if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                                //{
                                                resultArgs = voucherTransaction.DeleteVoucherTrans();
                                                if (resultArgs.Success)
                                                {
                                                    resultArgs = fdAccountSystem.DeleteFDAccountDetails();
                                                    if (resultArgs.Success)
                                                    {
                                                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                                    }
                                                    // }
                                                }
                                            }
                                            else
                                            {
                                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_CANNOT_DELETE_ASSOCIATION_OCCURS));
                                            }
                                        }
                                        else
                                        {
                                            string RenewalType = string.Empty;
                                            string VoucherIdfd = string.Empty;
                                            string VoucherId1 = string.Empty;
                                            string VoucherId2 = string.Empty;
                                            DataTable dtRenewalType = fdAccountSystem.FetchFDRenewalType();
                                            foreach (DataRow dr in dtRenewalType.Rows)
                                            {
                                                RenewalType = dr[fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName] != null ? dr[fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName].ToString() : string.Empty;
                                                fdAccountSystem.FDAccountId = dr[fdAccountSystem.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(dr[fdAccountSystem.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn.ColumnName].ToString()) : 0;
                                                VoucherIdfd = dr[fdAccountSystem.AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn.ColumnName] != null ? dr[fdAccountSystem.AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn.ColumnName].ToString() : string.Empty;
                                                VoucherId1 = dr[fdAccountSystem.AppSchema.FDRenewal.FD_VOUCHER_IDColumn.ColumnName] != null ? dr[fdAccountSystem.AppSchema.FDRenewal.FD_VOUCHER_IDColumn.ColumnName].ToString() : string.Empty;
                                                VoucherId2 = VoucherId1 + "," + VoucherIdfd;
                                                if (RenewalType == FDRenewalTypes.WDI.ToString() || RenewalType == FDRenewalTypes.PWD.ToString()) //RenewalType == FDRenewalTypes.WDI.ToString() On 11/10/2022, for partial withdrwal too
                                                {
                                                    //Modified by Carmel Raj on 20-Oct-2015
                                                    //Purpose :Avoiding Delete Confirmation Message replication,since the Confirmation message is given in voucher routing itseft,

                                                    //if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                                    //{
                                                    string msg = this.GetMessage(MessageCatalog.Master.FixedDeposit.FD_CONFIRMATION_DELETE);
                                                    if (RenewalType == FDRenewalTypes.PWD.ToString())
                                                    {
                                                        msg = "This is Partially Withdraw Fixed Deposit Voucher. Do you want to proceed ?";
                                                    }

                                                    if (this.ShowConfirmationMessage(msg, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                                    {
                                                        string[] voucherId = VoucherId2.Split(',');
                                                        foreach (string sValue in voucherId)
                                                        {
                                                            voucherTransaction.VoucherId = this.UtilityMember.NumberSet.ToInteger(sValue);
                                                            resultArgs = voucherTransaction.DeleteVoucherTrans();
                                                        }
                                                        if (resultArgs.Success)
                                                        {
                                                            resultArgs = fdAccountSystem.DeleteFDRenewalsByVoucherId();
                                                            if (resultArgs.Success)
                                                            {
                                                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                                            }
                                                        }
                                                        //  }
                                                    }
                                                }
                                                else
                                                {
                                                    if (fdAccountSystem.CheckFDRenewalClosed() == 0)
                                                    {
                                                        string msg = "This is Fixed Deposit related Voucher. Do you want to proceed ?";
                                                        if (this.ShowConfirmationMessage(msg, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                                        {
                                                            resultArgs = voucherTransaction.DeleteVoucherTrans();
                                                            if (resultArgs.Success)
                                                            {
                                                                resultArgs = fdAccountSystem.DeleteFDRenewalsByVoucherId();
                                                                if (resultArgs.Success)
                                                                {
                                                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));

                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_CANNOT_DELETE_FD_RECEIPT_ENTRY));
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            fdAccountSystem.FDVoucherId = VoucherId;
                                            resultArgs = fdAccountSystem.DeleteFDAccountByVoucherId();
                                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                        }
                                    }
                                }
                            }
                        }

                        //----------------------------------------FD Logic ends here -------------------------------------------------------------
                        break;
                    }
                case VoucherSubTypes.AST://When the voucher sub type is Asset then the logic here
                    {
                        //---------------------------------------Asset logic starts here-----------------------------------------------------------
                        //Added by Carmel Raj M
                        int PrimaryId;
                        switch (AnalyzeAssetType(VoucherId, out PrimaryId))
                        {
                            case AssetSourceFlag.Purchase:
                                {
                                    using (AssetInwardVoucherSystem AssetInwardDelete = new AssetInwardVoucherSystem())
                                    {
                                        resultArgs = AssetInwardDelete.DeleteAssetPurchase(AssetInwardDelete.GetPurchaseId(VoucherId));
                                        if (resultArgs != null && resultArgs.Success)
                                        {
                                            ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                        }
                                        else ShowMessageBox(resultArgs.Message);
                                    }

                                    break;
                                }
                            case AssetSourceFlag.Sales:
                                {
                                    //using (AssetSalesSystem AssetSales = new AssetSalesSystem())
                                    //{
                                    //    AssetSales.SalesId = AssetSales.GetSalesId(VoucherId);
                                    //    resultArgs = AssetSales.DeleteAssetSales();
                                    //    if (resultArgs != null && resultArgs.Success)
                                    //    {
                                    //        ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    //    }
                                    //    else ShowMessageBox(resultArgs.Message);
                                    //}
                                    break;
                                }
                        }
                        break;
                        //---------------------------------------Asset logic ends here-----------------------------------------------------------
                    }
                case VoucherSubTypes.STK: //When the voucher sub type is Stock then the logic here
                    {
                        //-------------------------------------Stock logic starts here-----------------------------------------------------------
                        //Added by Carmel Raj M
                        int PrimaryId;
                        switch (AnalyzeStockType(VoucherId, out PrimaryId)) //Analyzes the Stock Type
                        {
                            case StockType.Purchase:
                                {
                                    using (StockPurchaseDetail DeleteStock = new StockPurchaseDetail())
                                    {
                                        DeleteStock.PurchaseId = DeleteStock.GetPurchaseId(VoucherId);
                                        resultArgs = DeleteStock.DeleteStock();
                                        if (resultArgs != null && resultArgs.Success)
                                        {
                                            ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                        }
                                        else ShowMessageBox(resultArgs.Message);
                                    }
                                    break;
                                }
                            case StockType.Sales:
                                {
                                    using (StockSalesSystem SalesDelete = new StockSalesSystem())
                                    {
                                        SalesDelete.SalesId = SalesDelete.GetSalesId(VoucherId);
                                        resultArgs = SalesDelete.DeleteSoldUtlized();
                                        if (resultArgs != null && resultArgs.Success)
                                        {
                                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                        }
                                        else ShowMessageBox(resultArgs.Message);
                                    }
                                    break;
                                }
                            case StockType.PurchaseReturns:
                                {
                                    using (StockPurcahseReturnSystem PurchaseReturnDelete = new StockPurcahseReturnSystem())
                                    {
                                        PurchaseReturnDelete.ReturnId = PurchaseReturnDelete.GetPurchaseReturnId(VoucherId);
                                        PurchaseReturnDelete.ProjectId = BaseObject.ProjectId;
                                        resultArgs = PurchaseReturnDelete.DeletePurchaseReturn();
                                        if (resultArgs != null && resultArgs.Success)
                                        {
                                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                        }
                                        else ShowMessageBox(resultArgs.Message);
                                    }

                                    break;
                                }
                        }
                        break;
                        //-------------------------------------Stock logic ends here-----------------------------------------------------------
                    }
                case VoucherSubTypes.PAY:
                    {
                        //-------This part of  Logic has been taken from the already available one  -------------
                        using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                        {
                            voucherTransaction.VoucherId = VoucherId;
                            //int PayrollPostId = voucherTransaction.FetchPostIdByVoucherId();
                            if (VoucherId != 0)
                            {
                                using (clsPayrollBase objPayrollbase = new clsPayrollBase())
                                {
                                    //Modified by Carmel Raj on 20-Oct-2015
                                    //Purpose :Avoiding Delete Confirmation Message replication,since the Confirmation message is given in voucher routing itseft,

                                    //On 13/06/2019, to remove payroll voucers physically (Vouchers and Payroll Tables)
                                    //resultArgs = objPayrollbase.DeletePayRollPostPayment(VoucherId);
                                    //if (resultArgs.Success)
                                    //{
                                    //    resultArgs = voucherTransaction.DeleteVoucherTrans();
                                    //    if (resultArgs.Success)
                                    //    {
                                    //        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    //    }

                                    if (CommonMethod.ApplyUserRightsForTransaction((int)Payment.DeletePaymentVoucher) != 0 || this.LoginUser.IsFullRightsReservedUser)
                                    {
                                        if (!BaseObject.IsLockedTransaction(BaseObject.dtSelectedVoucherDate))
                                        {
                                            resultArgs = objPayrollbase.DeletePayrollPostPaymentVouchers(VoucherId);

                                            if (resultArgs.Success)
                                            {
                                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                            }
                                        }
                                        else
                                        {
                                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED_CANNOT_DELETE) + BaseObject.ProjectName + "'");
                                        }
                                    }
                                    else
                                    {
                                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.NO_RIGHTS_TO_DELETE_PAYMENT));
                                    }
                                }
                            }
                        }
                        break;
                        //---------------------------------------- Payroll delete logic ends here ---------------------------------------------
                    }
            }
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Returns one of the following StockTypes Purchase,Purchase Return and Sales for Stock Module
        /// </summary>
        /// <param name="VouhcerId">Voucher Id</param>
        /// <param name="PrimaryId">Primary Id of concern StockType(Purchase,Purchase Return and Sales)</param>
        /// <returns>Nullable type of StockType</returns>
        private StockType? AnalyzeStockType(int VouhcerId, out int PrimaryId)
        {
            PrimaryId = 0;
            StockType? ReturnStockType = null;
            try
            {
                using (VoucherTransactionSystem StockAnalyzer = new VoucherTransactionSystem())
                {
                    StockAnalyzer.VoucherId = VouhcerId;
                    resultArgs = StockAnalyzer.VocherRouterAnalyzerForStock();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        DataTable dtResult = resultArgs.DataSource.Table;
                        if (dtResult.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(dtResult.Rows[0][StockAnalyzer.AppSchema.StockMasterPurchase.PURCHASE_IDColumn.ColumnName].ToString()))
                            {
                                PrimaryId = UtilityMember.NumberSet.ToInteger(dtResult.Rows[0][StockAnalyzer.AppSchema.StockMasterPurchase.PURCHASE_IDColumn.ColumnName].ToString());
                                ReturnStockType = StockType.Purchase;
                            }
                            else if (!string.IsNullOrEmpty(dtResult.Rows[0][StockAnalyzer.AppSchema.StockMasterPurchaseReturns.RETURN_IDColumn.ColumnName].ToString()))
                            {
                                PrimaryId = UtilityMember.NumberSet.ToInteger(dtResult.Rows[0][StockAnalyzer.AppSchema.StockMasterPurchaseReturns.RETURN_IDColumn.ColumnName].ToString());
                                ReturnStockType = StockType.PurchaseReturns;
                            }
                            else if (!string.IsNullOrEmpty(dtResult.Rows[0][StockAnalyzer.AppSchema.StockMasterSales.SALES_IDColumn.ColumnName].ToString()))
                            {
                                PrimaryId = UtilityMember.NumberSet.ToInteger(dtResult.Rows[0][StockAnalyzer.AppSchema.StockMasterSales.SALES_IDColumn.ColumnName].ToString());
                                ReturnStockType = StockType.Sales;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
            return ReturnStockType;
        }

        /// <summary>
        /// Returns one of the following StockTypes Purchase,and Sales for Asset Module
        /// </summary>
        /// <param name="VouhcerId">Voucher Id</param>
        /// <param name="PrimaryId">Primary Id of concern AssetType(Purchase and Sales)</param>
        /// <returns>Nullable type of AssetType</returns>
        private AssetSourceFlag? AnalyzeAssetType(int VouhcerId, out int PrimaryId)
        {

            PrimaryId = 0;
            AssetSourceFlag? ReturnStockType = null;
            try
            {
                using (VoucherTransactionSystem AssetAnalyzer = new VoucherTransactionSystem())
                {
                    AssetAnalyzer.VoucherId = VouhcerId;
                    resultArgs = AssetAnalyzer.VoucherRouterAnalyzerForAsset();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        DataTable dtResult = resultArgs.DataSource.Table;
                        if (dtResult.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(dtResult.Rows[0][AssetAnalyzer.AppSchema.AssetPurchaseMaster.PURCHASE_IDColumn.ColumnName].ToString()))
                            {
                                PrimaryId = UtilityMember.NumberSet.ToInteger(dtResult.Rows[0][AssetAnalyzer.AppSchema.AssetPurchaseMaster.PURCHASE_IDColumn.ColumnName].ToString());
                                ReturnStockType = AssetSourceFlag.Purchase;
                            }
                            else if (!string.IsNullOrEmpty(dtResult.Rows[0][AssetAnalyzer.AppSchema.StockMasterSales.SALES_IDColumn.ColumnName].ToString()))
                            {
                                PrimaryId = UtilityMember.NumberSet.ToInteger(dtResult.Rows[0][AssetAnalyzer.AppSchema.StockMasterSales.SALES_IDColumn.ColumnName].ToString());
                                ReturnStockType = AssetSourceFlag.Sales;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
            return ReturnStockType;
        }

        /// <summary>
        /// Return the one of the following Payroll types  IssueLoan,PostVoucher
        /// </summary>
        /// <param name="ClinetRefId">ClinetRefId</param>
        /// <param name="PrimaryId">Primary Id of concern Payroll</param>
        /// <returns>Nullable type of PayRoll</returns>
        private PayRoll? AnalyzePayRollType(int ClinetRefId, out int PrimaryId)
        {
            PrimaryId = 0;
            PayRoll? ReturnPayRollType = null;
            try
            {
                using (VoucherTransactionSystem PayRoleAnalyzer = new VoucherTransactionSystem())
                {
                    PayRoleAnalyzer.VoucherId = ClinetRefId;
                    resultArgs = PayRoleAnalyzer.VoucherRouterAnalyzerForPayRole();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        DataTable dtResult = resultArgs.DataSource.Table;
                        if (dtResult.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(dtResult.Rows[0]["ISSUE_LOAN"].ToString()))
                            {
                                PrimaryId = UtilityMember.NumberSet.ToInteger(dtResult.Rows[0]["ISSUE_LOAN"].ToString());
                                ReturnPayRollType = PayRoll.IssueLoan;
                            }
                            else if (!string.IsNullOrEmpty(dtResult.Rows[0]["POST_VOUCHER"].ToString()))
                            {
                                PrimaryId = UtilityMember.NumberSet.ToInteger(dtResult.Rows[0]["POST_VOUCHER"].ToString());
                                ReturnPayRollType = PayRoll.PostVoucher;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }

            return ReturnPayRollType;
        }
        #endregion

        #endregion
    }
}