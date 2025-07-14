using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model;
using Bosco.Utility;
using Bosco.Model.Setting;
using Bosco.Model.UIModel;
using Bosco.Model.Inventory.Stock;
using Bosco.Report.Base;
using Bosco.Model.Transaction;

namespace ACPP.Modules.Inventory.Stock
{
    public partial class frmStockPurchaseView : frmFinanceBase
    {
        #region Constructor
        public frmStockPurchaseView()
        {
            InitializeComponent();
        }

        public frmStockPurchaseView(int projectId, StockPurchaseTransType stockpurchasetype)
            : this()
        {
            this.AssignProject = projectId;
            this.PurchaseType = (int)stockpurchasetype;
        }
        #endregion

        #region Variables
        ResultArgs resultArgs = null;
        int vouchertype = 0;
        private int RowIndex = 0;
        #endregion

        #region Properties

        private int purchasetype = 0;
        private int PurchaseType
        {
            set { purchasetype = value; }
            get { return purchasetype; }
        }

        private int AssignProject { get; set; }

        private int projectId;
        public int ProjectId
        {
            get { return glkpProject.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0; }
        }

        private string projectName;
        public string ProjectName
        {
            get { return !string.IsNullOrEmpty(glkpProject.Text) ? glkpProject.Text : string.Empty; ; }
        }

        private int voucherId = 0;
        public int VoucherId
        {
            get
            {
                RowIndex = gvStockPurchase.FocusedRowHandle;
                voucherId = gvStockPurchase.GetFocusedRowCellValue(colVoucherId) != null ? this.UtilityMember.NumberSet.ToInteger(gvStockPurchase.GetFocusedRowCellValue(colVoucherId).ToString()) : 0;
                return voucherId;
            }
            set
            {
                voucherId = value;
            }
        }

        public int PurchaseId { get { return gvStockPurchase.GetFocusedRowCellValue(colPurchaseId) != null ? this.UtilityMember.NumberSet.ToInteger(gvStockPurchase.GetFocusedRowCellValue(colPurchaseId).ToString()) : 0; RowIndex = gvStockPurchase.FocusedRowHandle; } }

        private DateTime SelectedVoucherDate;
        public DateTime dtSelectedVoucherDate
        {
            get
            {
                return gvStockPurchase.GetFocusedRowCellValue(colPurchaseDate) != null ? this.UtilityMember.DateSet.ToDate(gvStockPurchase.GetFocusedRowCellValue(colPurchaseDate).ToString(), false) : DateTime.MinValue;
            }
        }

        #endregion

        #region Events
        private void frmStockPurchaseView_Load(object sender, EventArgs e)
        {
            SetVisibileShortCuts(true, true);
            loadProjects();
            setDefaults();
            setTitle();
            fetchStockPurchaseDetails();
            ApplyUserRights();
        }

        private void ucStockPurchase_AddClicked(object sender, EventArgs e)
        {
            ShowStockPurchaseForm((int)AddNewRow.NewRow);
        }

        private void ucStockPurchase_EditClicked(object sender, EventArgs e)
        {
            ShowPurchaseForm();
        }

        private void ucStockPurchase_DeleteClicked(object sender, EventArgs e)
        {
            DeleteStockPurchase();
        }

        private void ucStockPurchase_RefreshClicked(object sender, EventArgs e)
        {
            fetchStockPurchaseDetails();
        }

        private void ucStockPurchase_PrintClicked(object sender, EventArgs e)
        {
            this.PrintGridViewDetails(gcStockPurchase, this.GetMessage(MessageCatalog.Stock.StockMasterPurchase.PRINT_CAPTION), PrintType.DS, gvStockPurchase, true);
        }

        private void ucStockPurchase_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvStockPurchase_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = "# " + gvStockPurchase.RowCount.ToString();
        }

        private void btnFetch_Click(object sender, EventArgs e)
        {
            fetchStockPurchaseDetails();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvStockPurchase.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvStockPurchase, colVendorName);
            }
        }

        private void gvStockPurchase_DoubleClick(object sender, EventArgs e)
        {
            ShowPurchaseForm();
        }

        private void gvStockPurchaseDetail_DoubleClick(object sender, EventArgs e)
        {
            ShowPurchaseForm();
        }
        #endregion

        #region Methods

        /// <summary>
        /// This is to call Purchase Transaction form while at Adding/Editing.
        /// </summary>
        /// <param name="PurchaseId"></param>
        private void ShowStockPurchaseForm(int PurchaseId)
        {
            try
            {
                frmPurchaseStockAdd frmPurchaseAdd = new frmPurchaseStockAdd(PurchaseId, ProjectId, ProjectName, dteDateFrom.DateTime, PurchaseType);
                frmPurchaseAdd.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmPurchaseAdd.ShowDialog();
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            fetchStockPurchaseDetails();
            gvStockPurchase.FocusedRowHandle = RowIndex;
        }

        private void ShowPurchaseForm()
        {
            try
            {
                if (gvStockPurchase.RowCount > 0)
                {
                    ShowStockPurchaseForm(this.PurchaseId);
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        /// <summary>
        /// Fetch project for Master Project
        /// </summary>
        private void loadProjects()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    resultArgs = mappingSystem.FetchProjectsLookup();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProject, resultArgs.DataSource.Table, mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                        glkpProject.EditValue = AssignProject;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void setDefaults()
        {
            dteDateFrom.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dteDateTo.DateTime = dteDateFrom.DateTime.AddYears(1).AddDays(-1);
            //commond by sudhakar
            //dteDateFrom.Properties.MinValue = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            //dteDateFrom.Properties.MaxValue = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            //dteDateTo.Properties.MinValue = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            //dteDateTo.Properties.MaxValue = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            //dteDateFrom.DateTime = this.UtilityMember.DateSet.ToDate(this.AppSetting.RecentVoucherDate, false);
            //dteDateTo.DateTime = dteDateFrom.DateTime.AddYears(1).AddDays(-1);
        }

        private void fetchStockPurchaseDetails()
        {
            DataSet dsStockPurchase = new DataSet();
            try
            {
                using (StockPurchaseDetail stockPurchase = new StockPurchaseDetail())
                {
                    stockPurchase.ProjectId = ProjectId;
                    stockPurchase.DateFrom = dteDateFrom.DateTime;
                    stockPurchase.DateTo = dteDateTo.DateTime;
                    stockPurchase.PurchaseFlag = PurchaseType;
                    dsStockPurchase = stockPurchase.FetchStockDetail();
                    if (dsStockPurchase != null && dsStockPurchase.Tables.Count > 0)
                    {
                        gcStockPurchase.DataSource = dsStockPurchase;
                        gcStockPurchase.DataMember = "StockMasters";
                        gcStockPurchase.RefreshDataSource();
                    }
                    else
                    {
                        gcStockPurchase.DataSource = null;
                        gcStockPurchase.RefreshDataSource();
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        /// <summary>
        /// Delete Stock Purchase Maters and Details
        /// </summary>
        private void DeleteStockPurchase()
        {
            try
            {
                if (gvStockPurchase.RowCount > 0)
                {
                    using (StockPurchaseDetail stockPurchase = new StockPurchaseDetail())
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            stockPurchase.PurchaseId = PurchaseId;
                            resultArgs = stockPurchase.DeleteStock();
                            if (resultArgs != null && resultArgs.Success)
                            {
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                fetchStockPurchaseDetails();
                                if (gvStockPurchase.RowCount.Equals(0))
                                {
                                    gvStockPurchase.OptionsView.ShowAutoFilterRow = false;
                                    chkShowFilter.Checked = false;
                                }
                            }
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void setTitle()
        {
            this.Text = (PurchaseType == 0) ? this.GetMessage(MessageCatalog.Stock.StockMasterPurchase.PURCHASE_VIEW_CAPTION) : this.GetMessage(MessageCatalog.Stock.StockMasterPurchase.RECEIVE_VIEW_CAPTION);
            this.rbtnStockPrintPurchaseView.Buttons[0].ToolTip = "To Print Voucher (Ctl+P)";
            colPrint.ToolTip = "To Print Voucher (Ctl+P)";
        }
        #endregion

        private void frmStockPurchaseView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void frmStockPurchaseView_Activated(object sender, EventArgs e)
        {
            SetVisibileShortCuts(true, true);
            loadProjects();
            setDefaults();
            setTitle();
            fetchStockPurchaseDetails();
        }

        private void rbtnStockPrintPurchaseView_Click(object sender, EventArgs e)
        {
            LoadPrintVoucher();
        }

        private void LoadPrintVoucher()
        {
            //On 01/02/2018, to show contra voucher also and Treat Journal Voucher as Contra Voucher
            //if (Vouchertype != DefaultVoucherTypes.Contra.ToString() && Vouchertype != string.Empty && gcTransaction != null)
            //    PrintVoucher(VoucherMasterId);
            //   if (Vouchertype != string.Empty && gcTransaction != null)
            PrintVoucher(VoucherId);
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
                            // rptVoucher = vouchertype == DefaultVoucherTypes.Receipt.ToString() ? UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKDONORRECEIPTS) : Vouchertype == DefaultVoucherTypes.Payment.ToString() ? UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKPAYMENTS) : UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKCONTRA);
                        }
                        else
                        {
                            // setting can be set for the Payment voucher details for the payments. VoucherPrint.PURCHASESLIP
                            rptVoucher = UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.PURCHASESLIP);      // Vouchertype == DefaultVoucherTypes.Receipt.ToString() ? UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKRECEIPTS) : Vouchertype == DefaultVoucherTypes.Payment.ToString() ? UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKPAYMENTS) : UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKCONTRA);
                        }

                        string payment = UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKPAYMENTS);
                        resultArgs = voucherSystem.FetchReportSetting(payment);
                        if (resultArgs != null && resultArgs.Success)
                        {
                            ReportProperty.Current.VoucherPrintSettingInfo = resultArgs.DataSource.TableView;
                            DataTable dt = resultArgs.DataSource.TableView.ToTable();
                            report.VoucherPrint(vid.ToString(), rptVoucher, ProjectName, ProjectId, PurchaseId, "Stock");
                        }
                        else
                        {
                            this.ShowMessageBoxError(resultArgs.Message);
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED_CANNOT_PRINT) + glkpProject.Text + "'");
                }
            }
            else
            {
                PrintVouchers(PurchaseId);

                //if (Vouchertype == DefaultVoucherTypes.Receipt.ToString())
                //{
                //    if (CommonMethod.ApplyUserRightsForTransaction((int)Receipt.PrintReceiptVoucher) != 0)
                //    {
                //        PrintVouchers(vid);
                //    }
                //    else
                //    {
                //        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.NO_RIGHTS_TO_TAKE_PRINTOUT));
                //    }
                //}
                //else if (Vouchertype == DefaultVoucherTypes.Payment.ToString())
                //{
                //    if (CommonMethod.ApplyUserRightsForTransaction((int)Payment.PrintPaymentVoucher) != 0)
                //    {
                //        PrintVouchers(vid);
                //    }
                //    else
                //    {
                //        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.NO_RIGHTS_TO_TAKE_PRINTOUT_PAYMENT));
                //    }
                //}
            }
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
                            string rptVoucher = UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKRECEIPTS);       // Vouchertype == DefaultVoucherTypes.Receipt.ToString() ? UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKRECEIPTS) : Vouchertype == DefaultVoucherTypes.Payment.ToString() ? UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKPAYMENTS) : string.Empty;

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

        public bool IsLockedTransaction(DateTime dtVoucherDate)
        {
            bool isSuccess = false;
            try
            {
                //if (dtLockDateFrom != DateTime.MinValue && dtLockDateTo != DateTime.MinValue)
                //{
                //    if (dtVoucherDate >= dtLockDateFrom && dtVoucherDate <= dtLockDateTo)
                //    {
                isSuccess = true;
                isSuccess = false;
                //    }
                //}
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
            return isSuccess;
        }

        /// <summary>
        /// Stock Views
        /// </summary>
        private void ApplyUserRights()
        {
            this.enumUserRights.Add(StockPurchase.CreateStockPurchase);
            this.enumUserRights.Add(StockPurchase.CreateStockPurchase);
            this.enumUserRights.Add(StockPurchase.CreateStockPurchase);
            this.enumUserRights.Add(StockPurchase.CreateStockPurchase);
            this.ApplyUserRights(ucStockPurchase, enumUserRights, (int)Menus.StockPurchase);
        }
    }
}