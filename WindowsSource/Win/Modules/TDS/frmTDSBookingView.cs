using System;
using System.Data;
using System.Windows.Forms;

using Bosco.Utility;
using Bosco.Model.TDS;
using Bosco.Model.UIModel;
using ACPP.Modules.Transaction;

namespace ACPP.Modules.TDS
{
    public partial class frmTDSBookingView : frmFinanceBase
    {
        #region Decelaration
        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        #endregion

        #region Constructor
        public frmTDSBookingView()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        private int _VoucherId = 0;
        private int VoucherId
        {
            get
            {
                RowIndex = gvBookingView.FocusedRowHandle;
                return _VoucherId = gvBookingView.GetFocusedRowCellValue(colVoucherId) != null ? this.UtilityMember.NumberSet.ToInteger(gvBookingView.GetFocusedRowCellValue(colVoucherId).ToString()) : (int)YesNo.No;
            }
            set
            {
                _VoucherId = value;
            }
        }

        private int _BookingId = 0;
        private int BookingId
        {
            get { return _BookingId = gvBookingView.GetFocusedRowCellValue(colBookingID) != null ? this.UtilityMember.NumberSet.ToInteger(gvBookingView.GetFocusedRowCellValue(colBookingID).ToString()) : (int)YesNo.No; }
            set { _BookingId = value; }
        }

        private int _ExpenseLedgerId = 0;
        private int ExpenseLedgerId
        {
            get { return _ExpenseLedgerId = gvTDSBooking.GetFocusedRowCellValue(colExpenseLedgerId) != null ? this.UtilityMember.NumberSet.ToInteger(gvTDSBooking.GetFocusedRowCellValue(colExpenseLedgerId).ToString()) : (int)YesNo.No; }
            set { _ExpenseLedgerId = value; }
        }

        private int _DeductTypeId = 0;
        private int DeductTypeId
        {
            get { return _DeductTypeId = gvTDSBooking.GetFocusedRowCellValue(colDeductTypeId) != null ? this.UtilityMember.NumberSet.ToInteger(gvTDSBooking.GetFocusedRowCellValue(colDeductTypeId).ToString()) : (int)YesNo.No; }
            set { _DeductTypeId = value; }
        }

        private int _PartyLedgerId = 0;
        private int PartyLedgerId
        {
            get { return _PartyLedgerId = gvTDSBooking.GetFocusedRowCellValue(colPartyLedgerId) != null ? this.UtilityMember.NumberSet.ToInteger(gvTDSBooking.GetFocusedRowCellValue(colPartyLedgerId).ToString()) : (int)YesNo.No; }
            set { _PartyLedgerId = value; }
        }

        private decimal _ExpenseAmount = 0;
        private decimal ExpenseAmount
        {
            get { return _ExpenseAmount = gvTDSBooking.GetFocusedRowCellValue(colAmount) != null ? this.UtilityMember.NumberSet.ToDecimal(gvTDSBooking.GetFocusedRowCellValue(colAmount).ToString()) : (int)YesNo.No; }
            set { _ExpenseAmount = value; }
        }

        private int deducteeTypeId = 0;
        private int DeducteeTypeId
        {
            get { return gvBookingView.GetFocusedRowCellValue(colDeducteeTypeId) != null ? this.UtilityMember.NumberSet.ToInteger(gvBookingView.GetFocusedRowCellValue(colDeducteeTypeId).ToString()) : 0; }
            set { deducteeTypeId = value; }
        }

        private int ProjectId
        {
            get { return glkpProject.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0; }
        }
        #endregion

        #region Events

        private void frmTDSBookingView_Load(object sender, EventArgs e)
        {

        }

        private void frmTDSBookingView_Activated(object sender, EventArgs e)
        {
            LoadDefaults();
            FetchTDSBooking();
        }

        /// <summary>
        /// Add a new Booking
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarBooking_AddClicked(object sender, EventArgs e)
        {
            ShowForm((int)AddNewRow.NewRow, (int)AddNewRow.NewRow);
        }

        /// <summary>
        /// This is to modify the selected record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarBooking_EditClicked(object sender, EventArgs e)
        {
            ShowForm(BookingId, VoucherId);
        }

        /// <summary>
        /// Edit the focused row by double clicking on the row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvBookingView_DoubleClick(object sender, EventArgs e)
        {
            ShowForm(BookingId, VoucherId);
        }

        private void gvTDSBooking_DoubleClick(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView gridView = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            int BookingId = gridView.GetFocusedRowCellValue(colChildBookingId) != null ? this.UtilityMember.NumberSet.ToInteger(gridView.GetFocusedRowCellValue(colChildBookingId).ToString()) : 0;
            int VoucherId = gridView.GetFocusedRowCellValue(colChildVoucherId) != null ? this.UtilityMember.NumberSet.ToInteger(gridView.GetFocusedRowCellValue(colChildVoucherId).ToString()) : 0;
            DeducteeTypeId = gridView.GetFocusedRowCellValue(colDeductId) != null ? this.UtilityMember.NumberSet.ToInteger(gridView.GetFocusedRowCellValue(colDeductId).ToString()) : 0;
            ShowForm(BookingId, VoucherId);
        }

        /// <summary>
        /// Delete TDS Booking Based on the Voucher Id and Booking id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarBooking_DeleteClicked(object sender, EventArgs e)
        {
            DeleteTDSBooking();
        }

        private void ucToolBarBooking_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Print the record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarBooking_PrintClicked(object sender, EventArgs e)
        {
            this.PrintGridViewDetails(gcBookingView, this.GetMessage(MessageCatalog.TDS.TDSBooking.TDS_PRINT_CAPTION), PrintType.DS, gvBookingView, true);
        }

        private void ucToolBarBooking_RefreshClicked(object sender, EventArgs e)
        {
            FetchTDSBooking();
            gvBookingView.FocusedRowHandle = RowIndex;
        }

        private void gvBookingView_RowCountChanged(object sender, EventArgs e)
        {
            lblRowCount.Text = "#" + gvBookingView.RowCount.ToString();
        }
        /// <summary>
        /// Refresh the grie after adding and editing the values. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchTDSBooking();
            gvBookingView.FocusedRowHandle = RowIndex;
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvBookingView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvBookingView, colVoucherDate);
            }
        }

        private void frmTDSBookingView_EnterClicked(object sender, EventArgs e)
        {
            ShowForm(BookingId, VoucherId);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            FetchTDSBooking();
        }

        private void frmTDSBookingView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F3)
                {
                    frmDatePicker datePicker = new frmDatePicker(deDateFrom.DateTime, deDateTo.DateTime, DatePickerType.ChangePeriod);
                    datePicker.ShowDialog();
                    deDateFrom.DateTime = AppSetting.VoucherDateFrom;
                    deDateTo.DateTime = AppSetting.VoucherDateTo;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex);
            }
        }

        private void deDateTo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (deDateTo.DateTime > deDateTo.DateTime)
                {
                    deDateTo.DateTime = deDateTo.DateTime;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex);
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Fetch only the TDS Booking which is deducted.
        /// </summary>
        private void FetchTDSBooking()
        {
            try
            {
                using (TDSBookingSystem tdsBookingSystem = new TDSBookingSystem())
                {
                    tdsBookingSystem.ProjectId = ProjectId;
                    tdsBookingSystem.DateFrom = deDateFrom.DateTime;
                    tdsBookingSystem.DateTo = deDateTo.DateTime;
                    DataSet dsTDSVoucher = tdsBookingSystem.FetchBooking();
                    if (dsTDSVoucher != null && dsTDSVoucher.Tables.Count > 0)
                    {
                        gcBookingView.DataSource = dsTDSVoucher;
                        gcBookingView.DataMember = "Master";
                    }
                    else
                    {
                        gcBookingView.DataSource = null;
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
        /// Delete TDS Booking Based on the Voucher Id 
        /// Delete the Booking in the following Tables (Voucher Master Trans,Voucher Trans,TDS Booking,TDS Booking Detail) 
        /// If this is deducted TDS Ledger Then Delete (TDS Deduction,TDS Deduction Detail) Table.
        /// </summary>
        private void DeleteTDSBooking()
        {
            try
            {
                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (TDSBookingSystem tdsBookingSystem = new TDSBookingSystem())
                    {
                        tdsBookingSystem.VoucherId = VoucherId;
                        tdsBookingSystem.BookingId = BookingId;
                        resultArgs = tdsBookingSystem.DeleteTDSBooking();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                            FetchTDSBooking();
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

        /// <summary>
        /// Show frmTDSTransaction Booking Table based on the Id
        /// </summary>
        private void ShowForm(int bookingId, int voucherId)
        {
            try
            {
                frmTDSTransaction frmTrans = new frmTDSTransaction(bookingId, voucherId);
                frmTrans.DeducteeTypeId = DeducteeTypeId;
                frmTrans.ShowDialog();
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        private void LoadDefaults()
        {
            deDateFrom.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deDateFrom.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            deDateFrom.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deDateTo.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deDateTo.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            deDateTo.DateTime = deDateFrom.DateTime.AddMonths(1).AddDays(-1);
            LoadProject();
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
                        glkpProject.EditValue = glkpProject.Properties.GetKeyValue(0);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message + Environment.NewLine + Ex.Source);
            }
            finally { }
        }
        #endregion

     

       
    }
}