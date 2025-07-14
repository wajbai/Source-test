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
using DevExpress.XtraLayout.Utils;

namespace ACPP.Modules.Transaction
{
    public partial class frmDatePicker : frmFinanceBase
    {

        #region Properties

        DatePickerType DateType { get; set; }
        public DateTime deProjectMaxDate { private get; set; }

        FDTypes FDType;

        #endregion

        #region Constractor
        public frmDatePicker()
        {
            InitializeComponent();
        }

        public frmDatePicker(DateTime DateFrom, DateTime DateTo, DatePickerType DPType)
            : this()
        {
            try
            {
                AppSetting.VoucherDateFrom = DateFrom;
                AppSetting.VoucherDateTo = DateTo;
                DateType = DPType;
                if (DPType == DatePickerType.ChangePeriod)
                {
                    lygVoucherDate.Visibility = LayoutVisibility.Never;
                    this.Height = 99;
                    this.Width = 188;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex);
            }
        }



        public frmDatePicker(DateTime Date, DatePickerType DPType)
            : this()
        {
            try
            {
                AppSetting.VoucherDate = Date;
                DateType = DPType;
                FDType = FDTypes.NONE;
                if (DPType == DatePickerType.VoucherDate)
                {
                    lygChangePeriod.Visibility = LayoutVisibility.Never;
                    this.Height = 75;
                    this.Width = 150;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex);
            }
        }

        public frmDatePicker(DateTime Date, DatePickerType DPType, FDTypes FixedDepositType)
            : this()
        {
            try
            {
                FDType = FixedDepositType;
                AppSetting.VoucherDate = Date;
                DateType = DPType;
                if (DPType == DatePickerType.VoucherDate)
                {
                    lygChangePeriod.Visibility = LayoutVisibility.Never;
                    this.Height = 75;
                    this.Width = 150;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex);
            }
        }

        #endregion

        #region Events
        private void frmDatePicker_Load(object sender, EventArgs e)
        {
            deProjectMaxDate = this.UtilityMember.DateSet.ToDate(AppSetting.YearTo, false);
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width - 2, Height - 2, 5, 5));
            if (DateType == DatePickerType.VoucherDate)
            {
                //  dteVoucherDate.Properties.MaxValue = this.UtilityMember.DateSet.ToDate(AppSetting.YearTo, false);
                dteVoucherDate.Properties.MaxValue = deProjectMaxDate;
                dteVoucherDate.Properties.MinValue = this.UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false);
                dteVoucherDate.DateTime = AppSetting.VoucherDate;
            }
            else if (DateType == DatePickerType.ChangePeriod)
            {
                dtDateFrom.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
                //  dtDateFrom.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
                dtDateFrom.Properties.MaxValue = deProjectMaxDate;
                dtDateTo.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
                // dtDateTo.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
                dtDateTo.Properties.MaxValue = deProjectMaxDate;

                dtDateFrom.DateTime = AppSetting.VoucherDateFrom;
                // dtDateTo.DateTime = AppSetting.VoucherDateTo;
                dtDateTo.DateTime = deProjectMaxDate;
            }
        }

        private void dteVoucherDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (DateType == DatePickerType.VoucherDate)
                {
                    if (FDType != FDTypes.OP)
                    {
                        // if (dteVoucherDate.DateTime >= UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false) && dteVoucherDate.DateTime <= UtilityMember.DateSet.ToDate(AppSetting.YearTo, false))
                        if (dteVoucherDate.DateTime >= UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false) && dteVoucherDate.DateTime <= deProjectMaxDate)
                        {
                            AppSetting.VoucherDate = dteVoucherDate.DateTime;
                            this.Hide();
                        }
                    }
                    else
                    {
                        if (dteVoucherDate.DateTime < UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false))
                        {
                            AppSetting.VoucherDate = dteVoucherDate.DateTime;
                            this.Hide();
                        }
                    }
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                //AppSetting.VoucherDate = UtilityMember.DateSet.ToDate(AppSetting.RecentVoucherDate, false);
                this.Hide();
            }
        }

        private void dtDateFrom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (DateType == DatePickerType.ChangePeriod)
                    {
                        //if (dtDateFrom.DateTime >= this.UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false) && dtDateFrom.DateTime <= this.UtilityMember.DateSet.ToDate(AppSetting.YearTo, false))
                        if (dtDateFrom.DateTime >= this.UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false) && dtDateFrom.DateTime <= deProjectMaxDate)
                        {
                            if (dtDateFrom.DateTime > dtDateTo.DateTime)
                            {
                                AppSetting.VoucherDateFrom = dtDateFrom.DateTime;
                                AppSetting.VoucherDateTo = dtDateFrom.DateTime;
                                this.Hide();
                            }
                            else
                            {
                                AppSetting.VoucherDateFrom = dtDateFrom.DateTime;
                                AppSetting.VoucherDateTo = dtDateTo.DateTime;
                                this.Hide();
                            }
                        }
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    this.Hide();
                }

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex);
            }
        }

        private void dtDateTo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (DateType == DatePickerType.ChangePeriod)
                    {
                        // if (dtDateTo.DateTime >= this.UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false) && dtDateTo.DateTime <= this.UtilityMember.DateSet.ToDate(AppSetting.YearTo, false))
                        if (dtDateTo.DateTime >= this.UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false) && dtDateTo.DateTime <= deProjectMaxDate)
                        {
                            if (dtDateFrom.DateTime > dtDateTo.DateTime)
                            {
                                AppSetting.VoucherDateFrom = dtDateFrom.DateTime;
                                AppSetting.VoucherDateTo = dtDateFrom.DateTime;
                                this.Hide();
                            }
                            else
                            {
                                AppSetting.VoucherDateFrom = dtDateFrom.DateTime;
                                AppSetting.VoucherDateTo = dtDateTo.DateTime;
                                this.Hide();
                            }
                        }
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex);
            }

        }
        #endregion
        #region Run Time Events and methods
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern System.IntPtr CreateRoundRectRgn
        (
         int nLeftRect, // x-coordinate of upper-left corner
         int nTopRect, // y-coordinate of upper-left corner
         int nRightRect, // x-coordinate of lower-right corner
         int nBottomRect, // y-coordinate of lower-right corner
         int nWidthEllipse, // height of ellipse
         int nHeightEllipse // width of ellipse
        );

        [System.Runtime.InteropServices.DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        private static extern bool DeleteObject(System.IntPtr hObject);
        #endregion




    }
}