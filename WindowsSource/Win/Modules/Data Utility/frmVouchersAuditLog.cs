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

namespace ACPP.Modules.Data_Utility
{
    public partial class frmVouchersAuditLog : frmFinanceBase
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
        public DataTable dtVoucherAuditorLogHistoryDetails = new DataTable("VoucherAuditorLogHistoryDetails");
        public DataTable dtVoucherCostcentreDetails = new DataTable("Costcentre");
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

        //private int costcentreledgerId;
        //private int CostCentreLedgerId
        //{
        //    set
        //    {
        //        costcentreledgerId = value;
        //    }
        //    get
        //    {
        //        costcentreledgerId = gvLedgerDetails.GetFocusedRowCellValue(ggcolLedgerId) != null ?
        //            this.UtilityMember.NumberSet.ToInteger(gvLedgerDetails.GetFocusedRowCellValue(ggcolLedgerId).ToString()) : 0;
        //        return costcentreledgerId;
        //    }
        //}

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

        private DateTime SelectedVoucherDate;
        public DateTime dtSelectedVoucherDate
        {
            get
            {
                return gvTransaction.GetFocusedRowCellValue(colVoucherDate) != null ? this.UtilityMember.DateSet.ToDate(gvTransaction.GetFocusedRowCellValue(colVoucherDate).ToString(), false) : DateTime.MinValue;
            }
        }

        private DateTime dtLockDateFrom { get; set; }
        private DateTime dtLockDateTo { get; set; }
        #endregion

        #region Constructor
        public frmVouchersAuditLog()
        {
            InitializeComponent();
        }

        public frmVouchersAuditLog(string recVoucherDate, int proId, string pro, int frmTransactionIndex, int transSelection)
            : this()
        {
            ProjectId = proId;
            ProjectName = pro;
            VoucherIndex = frmTransactionIndex;
            TransSelectionType = transSelection;
            RecentVoucherDate = recVoucherDate;
           // this.Text = "Vouchers - Audit Trail Log";
            if (frmTransactionIndex == 0)
            {
                //this.Text = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_TRANSACTION_RECEIPT);
                chkReceipt.Checked = true;
                this.enumUserRights.Add(Receipt.PrintReceiptVoucher);
                this.enumUserRights.Add(Receipt.ViewReceiptVoucher);
                this.ApplyUserRights(ucTrans, this.enumUserRights, (int)Menus.Receipt);
                ucTrans.VisibleAddButton = ucTrans.VisibleDeleteButton = ucTrans.VisibleNegativeBalance=
                        ucTrans.VisibleMoveTrans = ucTrans.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;

                if (!LoginUser.IsFullRightsReservedUser)
                {
                    //chkPayments.Enabled = chkContra.Enabled = false;
                    EnableCheckBox();
                }
            }
            else if (frmTransactionIndex == 1)
            {
                //this.Text = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_TRANSACTION_PAYMENT);
                chkPayments.Checked = true;
                
                this.enumUserRights.Add(Payment.PrintPaymentVoucher);
                this.enumUserRights.Add(Payment.ViewPaymentVoucher);
                this.ApplyUserRights(ucTrans, this.enumUserRights, (int)Menus.Payments);
                ucTrans.VisibleAddButton = ucTrans.VisibleDeleteButton = ucTrans.VisibleNegativeBalance =
                        ucTrans.VisibleMoveTrans = ucTrans.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
                if (!LoginUser.IsFullRightsReservedUser)
                {
                    EnableCheckBox();
                }
            }
            else
            {
                //this.Text = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_TRANSACTION_CONTRA);
                chkContra.Checked = true;
                this.enumUserRights.Add(Contra.PrintContraVoucher);
                this.enumUserRights.Add(Contra.ViewContraVoucher);
                this.ApplyUserRights(ucTrans, this.enumUserRights, (int)Menus.Contra);
                ucTrans.VisibleAddButton = ucTrans.VisibleDeleteButton = ucTrans.VisibleNegativeBalance =
                        ucTrans.VisibleMoveTrans = ucTrans.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
                if (!LoginUser.IsFullRightsReservedUser)
                {
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
            deDateFrom.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deDateFrom.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            deDateTo.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deDateTo.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            deDateFrom.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deDateFrom.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);// UtilityMember.DateSet.ToDate(RecentVoucherDate.ToString(), false);
            DateTime DateFrom = new DateTime(deDateFrom.DateTime.Year, deDateFrom.DateTime.Month, 1);
            deDateFrom.DateTime = DateFrom;
            deDateTo.DateTime = deDateFrom.DateTime.AddMonths(1).AddDays(-1);
        }

        private void LoadDefaultValues()
        {
            SetVisibileShortCuts(true, true, true);

            LoadProject();
            LoadVoucherDetails();
            gvTransaction.FocusedRowHandle = 0;
            gvVoucher.OptionsView.ShowFooter = true;
            glkpProject.EditValue = ProjectId;
            // command by sala
            //   if (TransSelectionType == 0) { ShowTransactionForm(); }
            //ucTrans.VisibleNegativeBalance = BarItemVisibility.Always;
            FetchDateDuration();
        }

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
            this.ShowCustomFilter(gcTransaction);
            //ShowVoucherMasterForm();
        }

        private void ucTrans_PrintClicked(object sender, EventArgs e)
        {
            string reportTitle = this.Text;
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
            //ucTransViewOpeningBalDetails.ShowOpNegative();
            //ucTransviewClosingBalance.ShowClosingBalNegative();
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
                    //ucTransViewOpeningBalDetails.ProjectId = ProjectId;
                    LoadVoucherDetails();
                    //ucTransViewOpeningBalDetails.ShowOpNegative();
                    //ucTransviewClosingBalance.ShowClosingBalNegative();
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
                CloseWaitDialog();
            }

        }

        private void gvTransaction_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvTransaction.RowCount.ToString();
        }


        private void chkReceipt_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit chk = sender as CheckEdit;
            KeepCheckedAtleast(chk);
        }

        private void chkPayments_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit chk = sender as CheckEdit;
            KeepCheckedAtleast(chk);
        }

        private void chkContra_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit chk = sender as CheckEdit;
            KeepCheckedAtleast(chk);
        }

        private void chkJournal_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit chk = sender as CheckEdit;
            KeepCheckedAtleast(chk);
        }


        private void gvVoucher_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            int value = gvVoucher.FocusedRowHandle;
        }

        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            ProjectId = (glkpProject.EditValue != null) ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
            //ucTransViewOpeningBalDetails.ProjectId = ProjectId;
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
            //ucTransViewOpeningBalDetails.ShowOpNegative();
            //ucTransviewClosingBalance.ShowClosingBalNegative();
        }

        private void ucTrans_MoveTransaction(object sender, EventArgs e)
        {
            //if (gvTransaction.RowCount > 0)
            //{
            //    if (VoucherMasterId > 0)
            //    {
            //        if (this.LoginUser.LoginUserId != "1")
            //        {
            //            if (Vouchertype == DefaultVoucherTypes.Receipt.ToString())
            //            {
            //                if (CommonMethod.ApplyUserRightsForTransaction((int)Receipt.MoveReceiptVoucher) != 0)
            //                {
            //                    MoveTransaction();
            //                }
            //                else
            //                {
            //                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.NO_RIGHTS_TO_MOVE_RECEIPT_VOUCHER));
            //                }
            //            }
            //            else if (Vouchertype == DefaultVoucherTypes.Payment.ToString())
            //            {
            //                if (CommonMethod.ApplyUserRightsForTransaction((int)Payment.MovePaymentVoucher) != 0)
            //                {
            //                    MoveTransaction();
            //                }
            //                else
            //                {
            //                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.NO_RIGHTS_TO_MOVE_PAYMENT_VOUCHER));
            //                }
            //            }
            //            else
            //            {
            //                if (CommonMethod.ApplyUserRightsForTransaction((int)Contra.MoveContraVoucher) != 0)
            //                {
            //                    MoveTransaction();
            //                }
            //                else
            //                {
            //                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.NO_RIGHTS_TO_MOVE_CONTRA_VOUCHER));
            //                }
            //            }
            //        }
            //        else
            //        {
            //            if (!IsLockedTransaction(dtSelectedVoucherDate))
            //            {
            //                int projectId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
            //                frmMoveTransaction frmVoucherTrans = new frmMoveTransaction(projectId, VoucherMasterId, LedgerId, MoveTransForm.Transaction, VoucherDefinitionId);
            //                frmVoucherTrans.UpdateHeld += new EventHandler(OnUpdateHeld);
            //                frmVoucherTrans.ShowDialog();
            //            }
            //            else
            //            {
            //                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED_CANNOT_MOVE) + glkpProject.Text + "'," +
            //                    " during the period " + this.UtilityMember.DateSet.ToDate(dtLockDateFrom.ToShortDateString()) +
            //                    " - " + this.UtilityMember.DateSet.ToDate(dtLockDateTo.ToShortDateString()));
            //            }
            //        }
            //    }
            //    else
            //    {
            //        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_MOVE_TRANSACTION_SELECTION_EMPTY));
            //    }
            //}
            //else
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_MOVE_TRANSACTION_SELECTION_EMPTY));
            //}
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
            //try
            //{
            //    if (!IsLockedTransaction(dtSelectedVoucherDate))
            //    {
            //        int projectId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
            //        frmMoveTransaction frmVoucherTrans = new frmMoveTransaction(projectId, VoucherMasterId, LedgerId, MoveTransForm.Transaction, VoucherDefinitionId);
            //        frmVoucherTrans.UpdateHeld += new EventHandler(OnUpdateHeld);
            //        frmVoucherTrans.ShowDialog();
            //    }
            //    else
            //    {
            //        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED_CANNOT_MOVE) + glkpProject.Text + "'");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            //}
            //finally { }
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
            LoadVoucherAuditHistoryDetails();
            //LoadVoucherTransDetails();
            //LoadVouchercostcentre();

        }


        public void LoadVoucherMaster()
        {
            try
            {
                DataTable dtVoucher = new DataTable();
                using (VoucherTransactionSystem voucherTransactionSystem = new VoucherTransactionSystem())
                {
                    string VoucherType = GetSelectedTransactions();
                    dtVoucher = voucherTransactionSystem.FetchMasterAuditLog(ProjectId, VoucherType, deDateFrom.DateTime, deDateTo.DateTime);
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
            //try
            //{
            //    DataTable dtVoucher = new DataTable();
            //    using (VoucherTransactionSystem voucherTransactionSystem = new VoucherTransactionSystem())
            //    {
            //        string VoucherType = GetSelectedTransactions();
            //        dtVoucher = voucherTransactionSystem.LoadVoucherCCDetails(ProjectId, VoucherType, deDateFrom.DateTime, deDateTo.DateTime);
            //        if (dtVoucher.Rows.Count != 0)
            //        {
            //            dtVoucherCostcentreDetails = dtVoucher;
            //            LoadCCDetailsByVoucherId();
            //            gcTransaction.RefreshDataSource();
            //        }
            //        else
            //        {
            //            gcCostCentreDetails.DataSource = null;
            //            gcCostCentreDetails.RefreshDataSource();
            //        }
            //        gvCostCentreDetails.FocusedRowHandle = 0;
            //        gvCostCentreDetails.FocusRectStyle = DrawFocusRectStyle.RowFocus;
            //        LoadUserControlInputData();
            //    }
            //}
            //catch (Exception err)
            //{
            //    MessageRender.ShowMessage(err.Message);
            //}
            //finally
            //{

            //}
        }

        public void LoadVoucherAuditHistoryDetails()
        {
            try
            {
                using (VoucherTransactionSystem voucherTransactionSystem = new VoucherTransactionSystem())
                {
                    string VoucherType = GetSelectedTransactions();
                    dtVoucherAuditorLogHistoryDetails = voucherTransactionSystem.LoadVoucherAuditHistoryDetails(ProjectId, VoucherType, deDateFrom.DateTime, deDateTo.DateTime);
                    if (dtVoucherAuditorLogHistoryDetails.Rows.Count != 0)
                    {
                        LoadAuditorLogHistoryByVoucherId();
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


        private void LoadUserControlInputData()
        {
            //ucTransViewOpeningBalDetails.ProjectId = projectId;
            //ucTransViewOpeningBalDetails.OpeningDateFrom = deDateFrom.Text;
            //ucTransViewOpeningBalDetails.OpeningDateTo = deDateTo.Text;
            //ucTransviewClosingBalance.ProjectId = ProjectId;
            //ucTransviewClosingBalance.ClosingDateFrom = deDateFrom.Text;
            //ucTransviewClosingBalance.ClosingDateTo = deDateTo.Text;
            //ucTransViewOpeningBalDetails.BankClosedDate = deDateFrom.Text; //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
            //ucTransViewOpeningBalDetails.GetOpBalance();
            //ucTransViewOpeningBalDetails.ShowOpNegative();
            //ucTransviewClosingBalance.BankClosedDate = deDateFrom.Text; //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
            //ucTransviewClosingBalance.GetClosingBalance();
            //ucTransviewClosingBalance.ShowClosingBalNegative();

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
                Transaction += "CN" + ",";
            }
            else
            {
                Transaction += "" + ",";
            }
            if (chkJournal.Checked)
            {
                Transaction += "JN";
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
            //try
            //{
            //    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        if (gvTransaction.RowCount != 0)
            //        {
            //            //Added by Carmel Raj on October-14-2015
            //            //Purpose :Route the voucher to delete the concern voucher and its child records
            //            using (RouteVoucher routeVoucher = new RouteVoucher())
            //            {
            //                //if (VoucherSubType != "AST" && VoucherSubType != VoucherSubTypes.CRI.ToString())
            //                if (VoucherSubType != "AST")
            //                {
            //                    routeVoucher.RouteVoucherDelete(this, VoucherMasterId, (VoucherSubTypes)Enum.Parse(typeof(VoucherSubTypes), VoucherSubType));
            //                }
            //                else
            //                {
            //                    if (VoucherSubType == VoucherSubTypes.AST.ToString())
            //                    {
            //                        //this.ShowMessageBox("Entry can be deleted in Fixed Asset Module.");
            //                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Transaction.TRANS_JOURNAL_ENTRY_CAN_DELETED_FIXED_ASSET_MODULE));
            //                    }
            //                }

            //            }
            //            LoadVoucherDetails();
            //        }
            //        else
            //        {
            //            ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageRender.ShowMessage(ex.ToString(), true);
            //}
            //finally { }
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
            //try
            //{
            //    RowIndex = gvTransaction.FocusedRowHandle;
            //    int ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
            //    string VoucherSubType = gvTransaction.GetFocusedRowCellValue(colVoucherSubType) != null ? gvTransaction.GetFocusedRowCellValue(colVoucherSubType).ToString() : string.Empty;
            //    // done by sala
            //    ProjectName = glkpProject.Text;
            //    if (VoucherID > 0)
            //    {
            //        if (Vouchertype == DefaultVoucherTypes.Receipt.ToString()) { VoucherIndex = 0; } else if (Vouchertype == DefaultVoucherTypes.Payment.ToString()) { VoucherIndex = 1; } else { VoucherIndex = 2; }
            //    }
            //    //Added by Carmel Raj on October-01-2015
            //    //Purpose :When Voucher Sub Type is PayRoll ClienReferenceId is the VoucherId according to the table design

            //    //VoucherID = (VoucherSubTypes)Enum.Parse(typeof(VoucherSubTypes), VoucherSubType) == VoucherSubTypes.PAY ?
            //    //    gvTransaction.GetFocusedRowCellValue(colPayRollClientId) != null ? UtilityMember.NumberSet.ToInteger(gvTransaction.GetFocusedRowCellValue(colPayRollClientId).ToString()) : 0 : VoucherID;

            //    //Added by Carmel Raj on October-01-2015
            //    //Purpose :Voucher Router for all the modules wherever voucher entries made
            //    using (RouteVoucher routeVoucher = new RouteVoucher())
            //    {
            //        //if (VoucherSubType != "AST" && VoucherSubType != VoucherSubTypes.CRI.ToString())
            //        if (VoucherSubType != "AST")
            //        {
            //            routeVoucher.RouteVoucherEdit(this, VoucherID, (VoucherSubTypes)Enum.Parse(typeof(VoucherSubTypes), VoucherSubType), MakeDuplicate);
            //        }
            //        else
            //        {
            //            if (VoucherSubType == VoucherSubTypes.AST.ToString())
            //            {
            //                //this.ShowMessageBox("Entry can be edited in the Fixed Asset Module.");
            //                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Transaction.TRANS_VIEW_ENTRY_CAN_EDIT_ASSET_MODULE));
            //            }
            //        }

            //    }
            //    //Referesh the voucher details
            //    LoadVoucherDetails();
            //    gvTransaction.FocusedRowHandle = RowIndex;
            //}
            //catch (Exception ex)
            //{
            //    MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            //}
            //finally { }
        }

        private void ShowTransactionForm()
        {
            //try
            //{
            //    DateTime dtyearfrom = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            //    DateTime dtbookbeginfrom = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false);
            //    DateTime dtRecentVoucher = UtilityMember.DateSet.ToDate(this.AppSetting.RecentVoucherDate, false);
            //    int ProId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
            //    ApplyRecentPrjectDetails(ProId);
            //    //DateTime dtRecentVoucherDate = (!string.IsNullOrEmpty(this.AppSetting.RecentVoucherDate)) ? this.UtilityMember.DateSet.ToDate(this.AppSetting.RecentVoucherDate, false) : dtbookbeginfrom > dtyearfrom ? dtbookbeginfrom : dtyearfrom;
            //    DateTime dtRecentVoucherDate = (!string.IsNullOrEmpty(AppSetting.RecentVoucherDate)) ? this.UtilityMember.DateSet.ToDate(AppSetting.RecentVoucherDate, false) : deDateFrom.DateTime;

            //    //01/03/2018, since recent date is taken from db by defualt, if that date is locked, we can show voucher etnry form
            //    //so we open voucher form here evern date is locked, it will be validated in entry form
            //    string Pro = glkpProject.Text.ToString();
                
            //    //On 28/01/2019, show list of voucher types ---------------------------------------------------------------------------------------------
            //    //this list will be shown only when more than one voucher type exists except base vouchers for selected project
            //    Int32 voucherdefinitionid = voucherIndex == 0 ? (Int32)DefaultVoucherTypes.Receipt : voucherIndex == 1 ? (Int32)DefaultVoucherTypes.Payment : (Int32)DefaultVoucherTypes.Contra; //by default 
            //    string basevouchers = voucherIndex == 0 ? ((Int32)DefaultVoucherTypes.Receipt).ToString() : voucherIndex == 1 ? ((Int32)DefaultVoucherTypes.Payment).ToString() : ((Int32)DefaultVoucherTypes.Contra).ToString();
            //    ResultArgs result = this.ShowVoucherTypeSelection(ProId, basevouchers, voucherdefinitionid);
            //    if (result.Success && result.ReturnValue != null)
            //    {
            //        string[] VoucherTypeSelected = result.ReturnValue as string[];
            //        voucherdefinitionid = UtilityMember.NumberSet.ToInteger(VoucherTypeSelected[0]);

            //        Int32 baseVoucherType = UtilityMember.NumberSet.ToInteger(VoucherTypeSelected[1]);
            //        if (baseVoucherType == (Int32)DefaultVoucherTypes.Receipt) { VoucherIndex = 0; }
            //        else if (baseVoucherType == (Int32)DefaultVoucherTypes.Payment) { VoucherIndex = 1; }
            //        else { VoucherIndex = 2; }
            //    }
            //    //----------------------------------------------------------------------------------------------------------------------------------------------

            //    frmTransactionMultiAdd frmTransaction = new frmTransactionMultiAdd(dtRecentVoucherDate.ToString(), ProId, Pro, (int)AddNewRow.NewRow, VoucherIndex,false,voucherdefinitionid);
            //    frmTransaction.UpdateHeld += new EventHandler(OnUpdateHeld);
            //    frmTransaction.ShowDialog();

            //    /*if (!IsLockedTransaction(dtRecentVoucherDate))
            //    {
            //        string Pro = glkpProject.Text.ToString();
            //        frmTransactionMultiAdd frmTransaction = new frmTransactionMultiAdd(dtRecentVoucherDate.ToString(), ProId, Pro, (int)AddNewRow.NewRow, VoucherIndex);
            //        frmTransaction.UpdateHeld += new EventHandler(OnUpdateHeld);
            //        frmTransaction.ShowDialog();
            //    }
            //    else
            //    {
            //        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED) + glkpProject.Text + "'");
            //    }*/

            //    //28/11/2018, refresh finance setting
            //    if (this.MdiParent != null)
            //    {
            //        (this.MdiParent as frmMain).ReAssignSetting();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageRender.ShowMessage(ex.ToString(), true);
            //}
            //finally { }
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
                            rptVoucher = vouchertype == DefaultVoucherTypes.Receipt.ToString() ? UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKDONORRECEIPTS) : Vouchertype == DefaultVoucherTypes.Payment.ToString() ? UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKPAYMENTS) : UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKCONTRA);
                        }
                        else
                        {
                            rptVoucher = Vouchertype == DefaultVoucherTypes.Receipt.ToString() ? UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKRECEIPTS) : Vouchertype == DefaultVoucherTypes.Payment.ToString() ? UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKPAYMENTS) : UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKCONTRA);
                        }

                        resultArgs = voucherSystem.FetchReportSetting(rptVoucher);
                        if (resultArgs != null && resultArgs.Success)
                        {
                            ReportProperty.Current.VoucherPrintSettingInfo = resultArgs.DataSource.TableView;
                            report.VoucherPrint(vid.ToString(), rptVoucher, ProjectName, ProjectId);
                        }
                        else
                        {
                            this.ShowMessageBoxError(resultArgs.Message);
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

        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.F3))
            {
                ////deDateFrom.Focus();
                //frmDatePicker datePicker = new frmDatePicker(deDateFrom.DateTime, deDateTo.DateTime, DatePickerType.ChangePeriod);
                //datePicker.ShowDialog();
                //deDateFrom.DateTime = AppSetting.VoucherDateFrom;
                //deDateTo.DateTime = AppSetting.VoucherDateTo;
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
            //frmNegativeBalanceHistory negativebalance = new frmNegativeBalanceHistory(ProjectId, deDateFrom.DateTime);
            //negativebalance.ShowDialog();
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
            //try
            //{
            //    if (VoucherMasterId != 0)
            //    {
            //        if (gvTransaction.RowCount != 0)
            //        {
            //            string VoucherNo = string.Empty;
            //            string RunningDigit = string.Empty;
            //            int ProId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
            //            string Pro = glkpProject.Text.ToString();
            //            string CurrentVoucherDate = gvTransaction.GetFocusedRowCellValue(colVoucherDate) != null ? gvTransaction.GetFocusedRowCellValue(colVoucherDate).ToString() : string.Empty;
            //            if (Vouchertype == DefaultVoucherTypes.Receipt.ToString()) { VoucherIndex = 0; } else if (Vouchertype == DefaultVoucherTypes.Payment.ToString()) { VoucherIndex = 1; } else { VoucherIndex = 2; }
            //            if (!IsLockedTransaction(this.UtilityMember.DateSet.ToDate(CurrentVoucherDate, false)))
            //            {
            //                DataRow drVoucher = gvTransaction.GetDataRow(gvTransaction.FocusedRowHandle); // gvTransaction.GetFocusedRowCellValue(colVoucherNo) != null ? gvTransaction.GetFocusedRowCellValue(colVoucherNo).ToString() : string.Empty;
            //                if (drVoucher != null)
            //                {
            //                    VoucherNo = drVoucher["VOUCHER_NO"].ToString();
            //                    resultArgs = SplitRunningDigit(VoucherNo);
            //                    if (resultArgs.Success)
            //                    {
            //                        if (resultArgs.ReturnValue != null)
            //                        {
            //                            RunningDigit = resultArgs.ReturnValue.ToString();
            //                            frmTransactionMultiAdd frmTransaction = new frmTransactionMultiAdd(CurrentVoucherDate, ProId, Pro, (int)AddNewRow.NewRow, VoucherIndex, VoucherNo, RunningDigit, VoucherDefinitionId);
            //                            frmTransaction.UpdateHeld += new EventHandler(OnUpdateHeld);
            //                            frmTransaction.ShowDialog();
            //                        }
            //                    }
            //                    else
            //                    {
            //                        this.ShowMessageBoxWarning(resultArgs.Message);
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED) + glkpProject.Text + "'," +
            //                     " during the period " + this.UtilityMember.DateSet.ToDate(dtLockDateFrom.ToShortDateString()) +
            //                    " - " + this.UtilityMember.DateSet.ToDate(dtLockDateTo.ToShortDateString()));
            //            }
            //        }
            //        else
            //        {
            //            ShowTransactionForm();
            //        }
            //    }
            //    else
            //    {
            //        ShowTransactionForm();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageRender.ShowMessage(ex.ToString(), true);
            //}
            //finally { }
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
                using (AuditLockTransSystem AuditSystem = new AuditLockTransSystem())
                {
                    AuditSystem.ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
                    AuditSystem.DateFrom = deDateFrom.DateTime;
                    AuditSystem.DateTo = deDateTo.DateTime;
                    resultArgs = AuditSystem.FetchAuditLockDetailsForProjectAndDate();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        colTranLocked.Visible = true;
                        dtLockDateFrom = this.UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][AuditSystem.AppSchema.AuditLockTransType.DATE_FROMColumn.ColumnName].ToString(), false);
                        dtLockDateTo = this.UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][AuditSystem.AppSchema.AuditLockTransType.DATE_TOColumn.ColumnName].ToString(), false);
                    }
                    else
                    {
                        colTranLocked.Visible = false;
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

        private void gvTransaction_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (gvTransaction.RowCount > 0)
                {
                    if (dtLockDateFrom != null && dtLockDateTo != null && (!dtLockDateFrom.Equals(DateTime.MinValue)) && (!dtLockDateTo.Equals(DateTime.MinValue)))
                    {
                        colTranLocked.Visible = true;
                        DateTime dtTransDate = gvTransaction.GetRowCellValue(e.RowHandle, colVoucherDate) != null ? this.UtilityMember.DateSet.ToDate(gvTransaction.GetRowCellValue(e.RowHandle, colVoucherDate).ToString(), false) : DateTime.MinValue;
                        if (!dtTransDate.Equals(DateTime.MinValue))
                        {
                            if (dtTransDate >= dtLockDateFrom && dtTransDate <= dtLockDateTo)
                            {
                                rbtnLockTrans.Buttons[0].Image = imgTransView.Images[0];
                                e.Handled = false;
                            }
                            else
                            {
                                rbtnLockTrans.Buttons[0].Image = imgTransView.Images[4];
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
                if (dtLockDateFrom != DateTime.MinValue && dtLockDateTo != DateTime.MinValue)
                {
                    if (dtVoucherDate >= dtLockDateFrom && dtVoucherDate <= dtLockDateTo)
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
            //if (chkColumnChooser.Checked)
            //{
            //    gvTransaction.ColumnsCustomization();
            //}
            //else
            //{
            //    gvTransaction.DestroyCustomization();
            //}
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
            LoadCCDetailsByVoucherId();
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

        private void LoadAuditorLogHistoryByVoucherId()
        {
            if (dtVoucherAuditorLogHistoryDetails != null && dtVoucherAuditorLogHistoryDetails.Rows.Count > 0)
            {
                DataView dvFilter = dtVoucherAuditorLogHistoryDetails.AsDataView();
                dvFilter.RowFilter = "VOUCHER_ID =" + VoucherMasterId;
                dvFilter.Sort = "IS_AUDITOR_MODIFIED DESC";
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
            //if (dtVoucherCostcentreDetails != null && dtVoucherCostcentreDetails.Rows.Count > 0)
            //{
            //    DataView dvFilter = dtVoucherCostcentreDetails.AsDataView();
            //    dvFilter.RowFilter = "LEDGER_ID = " + CostCentreLedgerId + " AND  VOUCHER_ID = " + VoucherMasterId;
            //    gcCostCentreDetails.DataSource = null;

            //    if (dvFilter != null)
            //    {
            //        gcCostCentreDetails.DataSource = dvFilter.ToTable();
            //    }
            //}
        }

        private void gvTransaction_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //LoadTransDetailsByVoucherId();
            LoadAuditorLogHistoryByVoucherId();
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
            if (e.Column == colTrackPreviousAmount || e.Column == colTrackAmount)
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
                    if ((CommonMethod.ApplyUserRightsForTransaction((int)Receipt.EditReceiptVoucher) != 0) || 
                    (CommonMethod.ApplyUserRightsForTransaction((int)Contra.EditContraVoucher) != 0) ||
                    (CommonMethod.ApplyUserRightsForTransaction((int)Payment.EditPaymentVoucher) != 0) || this.LoginUser.IsFullRightsReservedUser)
                    {
                        if (this.ShowConfirmationMessage("Do you want to Replicate Voucher Entry ?",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            ShowVoucherMasterForm(true);
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

        private void pnlProject_Paint(object sender, PaintEventArgs e)
        {

        }
                
        private void KeepCheckedAtleast(CheckEdit chk)
        {
            bool uncheckedall = (chkReceipt.Checked == false && chkPayments.Checked == false && chkContra.Checked == false && chkJournal.Checked == false);
            if (uncheckedall)
            {
                chk.Checked = true;
            }

        }

      

      
    }

    
}