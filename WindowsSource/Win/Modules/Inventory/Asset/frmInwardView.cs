using System;
using System.Windows.Forms;

using Bosco.Utility;
using Bosco.Model;
using Bosco.Model.UIModel;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System.Data;
using ACPP.Modules.Inventory.Asset;
using Bosco.Model.Transaction;
using Bosco.Model.Inventory;
using Bosco.Report.Base;

namespace ACPP.Modules.Asset
{
    public partial class frmInwardView : frmFinanceBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        private int inOutId = 0;
        private int voucherId = 0;
        private int RowIndex = 0;
        #endregion

        #region Properties
        public int InOutId
        {
            get
            {
                RowIndex = gvPurchaseView.FocusedRowHandle;
                inOutId = gvPurchaseView.GetFocusedRowCellValue(colInOutId) != null ? this.UtilityMember.NumberSet.ToInteger(gvPurchaseView.GetFocusedRowCellValue(colInOutId).ToString()) : 0;
                return inOutId;
            }
            set
            {
                inOutId = value;
            }
        }

        public int VoucherId
        {
            get
            {
                RowIndex = gvPurchaseView.FocusedRowHandle;
                voucherId = gvPurchaseView.GetFocusedRowCellValue(colVoucherId) != null ? this.UtilityMember.NumberSet.ToInteger(gvPurchaseView.GetFocusedRowCellValue(colVoucherId).ToString()) : 0;
                return voucherId;
            }
            set
            {
                voucherId = value;
            }
        }

        public int ProjectId
        {
            get;
            set;
        }

        public string ProjectName
        {
            get;
            set;

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

        private DateTime SelectedVoucherDate;
        public DateTime dtSelectedVoucherDate
        {
            get
            {
                return gvPurchaseView.GetFocusedRowCellValue(colPurchaseDate) != null ? this.UtilityMember.DateSet.ToDate(gvPurchaseView.GetFocusedRowCellValue(colPurchaseDate).ToString(), false) : DateTime.MinValue;
            }
        }


        public AssetInOut Flag { get; set; }

        #endregion

        #region Constructors

        public frmInwardView()
        {
            InitializeComponent();
        }
        public frmInwardView(string recentVoucherDate, int recentProjectId, string recentProject, AssetInOut flag)
            : this()
        {
            ProjectId = recentProjectId;
            RecentVoucherDate = recentVoucherDate;
            this.Flag = flag;
            chkInkind.Checked = true;
        }
        #endregion

        #region Events

        private void LoadDefaults()
        {
            DateTime dtTime;
            deFromDate.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deToDate.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            dtTime = deFromDate.DateTime.AddYears(1);
            deToDate.DateTime = dtTime.AddDays(-1);
            SetTitle();
        }

        private void ucPurchaseVoucherView_AddClicked(object sender, EventArgs e)
        {
            ShowPurchase((int)AddNewRow.NewRow);
        }

        private void ucPurchaseVoucherView_EditClicked(object sender, EventArgs e)
        {
            ShowEditAssetPurchase();
        }

        private void gvPurchaseView_DoubleClick(object sender, EventArgs e)
        {
            ShowEditAssetPurchase();
        }
        private void gvAssetPurchaseDetails_DoubleClick(object sender, EventArgs e)
        {
            ShowEditAssetPurchase();
        }

        private void ucPurchaseVoucherView_DeleteClicked(object sender, EventArgs e)
        {
            DeleteAssetInward();
        }

        private void ucPurchaseVoucherView_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcPurchaseView, this.GetMessage(MessageCatalog.Asset.InwardVoucher.PURCHASE_PRINT_CAPTION), PrintType.DS, gvPurchaseView, true);
        }

        private void ucPurchaseVoucherView_RefreshClicked(object sender, EventArgs e)
        {
            LoadProject();
            ProjectId = glkpProject.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
            FetchAssetPurchaseDetails();
        }

        private void ucPurchaseVoucherView_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvPurchaseView_RowCountChanged(object sender, EventArgs e)
        {
            lblRowCount.Text = gvPurchaseView.RowCount.ToString();
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchAssetPurchaseDetails();
            gvPurchaseView.FocusedRowHandle = RowIndex;
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvPurchaseView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvPurchaseView, colVendor);
            }
        }

        private void frmPurchaseView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            FetchAssetPurchaseDetails();
        }

        private void frmInwardView_Load(object sender, EventArgs e)
        {
            //SetVisibileShortCuts(true, true, true);
            LoadDefaults();
            LoadProject();
            SetTitle();
            FetchAssetPurchaseDetails();
            chkPurchase.Checked = (Flag == AssetInOut.PU) ? true : false;
            chkInkind.Checked = chkPurchase.Checked ? false : true;

            if (Flag.Equals(AssetInOut.PU))
            {
                ApplyUserRightsPurchase();
            }
            else
            {
                ApplyUserRightsInKind();
            }
        }

        private void frmInwardView_Activated(object sender, EventArgs e)
        {
            SetVisibileShortCuts(true, true, true);
            LoadDefaults();
            LoadProject();
            FetchAssetPurchaseDetails();
        }
        #endregion

        #region Methods
        /// <summary>
        /// show the form
        /// </summary>
        /// <param name="VendorID"></param>
        private void ShowPurchase(int purchaseId)
        {
            try
            {
                int ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
                string ProjectName = glkpProject.Text.ToString();
                ApplyRecentPrjectDetails(ProjectId);
                DateTime dtRecentVoucherDate = (!string.IsNullOrEmpty(AppSetting.RecentVoucherDate)) ? this.UtilityMember.DateSet.ToDate(AppSetting.RecentVoucherDate, false) : deFromDate.DateTime;
                frmInwardVoucherAdd purchaseAdd = new frmInwardVoucherAdd(dtRecentVoucherDate.ToString(), ProjectId, ProjectName, purchaseId, this.Flag);
                purchaseAdd.UpdateHeld += new EventHandler(OnUpdateHeld);
                purchaseAdd.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            { }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ApplyUserRightsPurchase()
        {
            this.enumUserRights.Add(Purchase.CreatePurchase);
            this.enumUserRights.Add(Purchase.EditPurchase);
            this.enumUserRights.Add(Purchase.DeletePurchase);
            this.enumUserRights.Add(Purchase.ViewPurchase);
            this.ApplyUserRights(ucPurchaseVoucherView, enumUserRights, (int)Menus.Purchase);
        }

        /// <summary>
        /// 
        /// </summary>
        private void ApplyUserRightsInKind()
        {
            this.enumUserRights.Add(ReceiveInKind.CreateReceiveInKind);
            this.enumUserRights.Add(ReceiveInKind.EditReceiveInKind);
            this.enumUserRights.Add(ReceiveInKind.DeleteReceiveInKind);
            this.enumUserRights.Add(ReceiveInKind.ViewReceiveInKind);
            this.ApplyUserRights(ucPurchaseVoucherView, enumUserRights, (int)Menus.ReceiveInKind);
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

        public void FetchAssetPurchaseDetails()
        {
            try
            {
                DataSet dsAssetInwardOutward = new DataSet();
                using (AssetInwardOutwardSystem InwardOutwardVoucherSystem = new AssetInwardOutwardSystem())
                {
                    DateTime dtTime;
                    if (deToDate.DateTime < deFromDate.DateTime)
                    {
                        deToDate.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
                        dtTime = deFromDate.DateTime.AddYears(1);
                        deToDate.DateTime = dtTime.AddDays(-1);
                        InwardOutwardVoucherSystem.DateFrom = this.UtilityMember.DateSet.ToDate(deFromDate.DateTime.ToString(), false);
                        InwardOutwardVoucherSystem.DateTo = this.UtilityMember.DateSet.ToDate(deToDate.DateTime.ToString(), false);
                    }
                    else
                    {
                        InwardOutwardVoucherSystem.DateFrom = this.UtilityMember.DateSet.ToDate(deFromDate.DateTime.ToString(), false);
                        InwardOutwardVoucherSystem.DateTo = this.UtilityMember.DateSet.ToDate(deToDate.DateTime.ToString(), false);
                    }
                    InwardOutwardVoucherSystem.ProjectId = ProjectId;
                    InwardOutwardVoucherSystem.Status = 1;

                    //string tmpFlag = string.Empty;
                    //if (chkPurchase.Checked)
                    //    tmpFlag = AssetInOut.PU.ToString() + ",";
                    //if (chkInkind.Checked)
                    //    tmpFlag += AssetInOut.IK.ToString() + ",";

                    InwardOutwardVoucherSystem.Flag = this.Flag.ToString();
                    dsAssetInwardOutward = InwardOutwardVoucherSystem.FetchAssetInOutDetailsByFlag();
                    if (dsAssetInwardOutward != null && dsAssetInwardOutward.Tables.Count > 0)
                    {
                        gcPurchaseView.DataSource = dsAssetInwardOutward;
                        gcPurchaseView.DataMember = "Master";
                        gcPurchaseView.RefreshDataSource();
                    }
                    else
                    {
                        gcPurchaseView.DataSource = null;
                        gcPurchaseView.RefreshDataSource();
                    }
                    gvPurchaseView.FocusedRowHandle = 0;
                    gvPurchaseView.FocusRectStyle = DrawFocusRectStyle.RowFocus;
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
        }

        private void ShowEditAssetPurchase()
        {
            if (gvPurchaseView.RowCount > 0)
            {
                if (InOutId > 0)
                {
                    ShowPurchase(InOutId);
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

        private void DeleteAssetInward()
        {
            try
            {
                ResultArgs resultArgs = null;
                if (gvPurchaseView.RowCount > 0)
                {
                    if (InOutId > 0)
                    {
                        using (frmAssetItemList ItemList = new frmAssetItemList(InOutId, 0, 0, 1, Flag == AssetInOut.PU ? AssetInOut.PU : AssetInOut.IK))
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                //resultArgs = 
                                ItemList.DeleteAssetList();
                                using (AssetInwardOutwardSystem InwardOutwardSystem = new AssetInwardOutwardSystem())
                                {
                                    InwardOutwardSystem.InoutId = this.InOutId;
                                    InwardOutwardSystem.Flag = AssetInOut.PU.ToString();
                                    resultArgs = InwardOutwardSystem.DeleteAssetInOutward();
                                    if (!resultArgs.Success)
                                    {
                                        this.ShowMessageBox(resultArgs.Message);
                                    }
                                    //    //else
                                    //    //{
                                    //    //    this.ShowMessageBox(resultArgs.Message);
                                    //    //}
                                }
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    FetchAssetPurchaseDetails();
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
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally
            { }
        }

        private void LoadProject()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    resultArgs = mappingSystem.FetchProjectsLookup();
                    glkpProject.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProject, resultArgs.DataSource.Table, mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                        this.glkpProject.EditValueChanged -= new System.EventHandler(this.glkpProject_EditValueChanged);
                        glkpProject.EditValue = (ProjectId != 0) ? ProjectId : glkpProject.Properties.GetKeyValue(0);
                        this.glkpProject.EditValueChanged += new System.EventHandler(this.glkpProject_EditValueChanged);
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

        private void SetTitle()
        {
            if (this.Flag == AssetInOut.PU)
            {
                this.Text = this.GetMessage(MessageCatalog.Asset.InwardVoucher.PURCHASE_VIEW);
            }
            else
            {
                this.Text = this.GetMessage(MessageCatalog.Asset.InwardVoucher.RECEIVE_VIEW);
            }

            this.rbtnInwardPrintViewScreen.Buttons[0].ToolTip = "To Print Voucher (Ctl+P)";
            colPrint.ToolTip = "To Print Voucher (Ctl+P)";
        }
        #endregion

        private void chkPurchase_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkInkind.Checked)
                chkPurchase.Checked = true;
        }

        private void chkInkind_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkPurchase.Checked)
                chkInkind.Checked = true;
        }

        private void frmInwardView_EnterClicked(object sender, EventArgs e)
        {
            ShowEditAssetPurchase();
        }

        private void rbtnInwardPrintViewScreen_Click(object sender, EventArgs e)
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

                        // Chinna Commanded on 22.09.2022 at 3 PM
                        //if (Bosco.Utility.ConfigSetting.SettingProperty.EnableNetworking == true)
                        //{
                        //    // rptVoucher = vouchertype == DefaultVoucherTypes.Receipt.ToString() ? UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKDONORRECEIPTS) : Vouchertype == DefaultVoucherTypes.Payment.ToString() ? UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKPAYMENTS) : UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKCONTRA);
                        //}
                        //else
                        //{
                        //    // setting can be set for the Payment voucher details for the payments. VoucherPrint.PURCHASESLIP
                        //    rptVoucher = UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.PURCHASESLIP);      // Vouchertype == DefaultVoucherTypes.Receipt.ToString() ? UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKRECEIPTS) : Vouchertype == DefaultVoucherTypes.Payment.ToString() ? UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKPAYMENTS) : UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKCONTRA);
                        //}

                        if (Bosco.Utility.ConfigSetting.SettingProperty.EnableAsset == true)
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
                            report.VoucherPrint(vid.ToString(), rptVoucher, ProjectName, ProjectId, InOutId, "Asset");
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
                PrintVouchers(VoucherId);

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
    }
}
