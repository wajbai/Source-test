using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.Serialization;
using Bosco.Model.Business;
using Bosco.Model.Transaction;
using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.Utility.Common;
using Bosco.Utility.CommonMemberSet;

using DevExpress.XtraEditors;

using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Base;
using Payroll.Model.UIModel;
using Payroll.DAO;
using DevExpress.XtraEditors.Mask;
using Bosco.Utility.ConfigSetting;
using DevExpress.XtraGrid.Views.Grid;
using Payroll.DAO.Schema;

namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmPostVoucherview : frmPayrollBase
    {
        #region Declaration
        private int RowIndex = 0;
        ResultArgs resultArgs = null;
        clsPayrollBase payrollbase = new clsPayrollBase();
        DataView dvPayRollVoucherDetails = null;
        ApplicationSchema.PAYROLL_FINANCEDataTable dtpayrollFinance = new ApplicationSchema.PAYROLL_FINANCEDataTable();
        #endregion

        #region Constructor
        public frmPostVoucherview()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        private Int32 VoucherId
        {
            get
            {
                Int32 voucherid = gvPostVoucherView.GetFocusedRowCellValue(colVoucherId) != null ? this.UtilityMember.NumberSet.ToInteger(gvPostVoucherView.GetFocusedRowCellValue(colVoucherId).ToString()) : 0;
                return voucherid;
            }
        }

        private double PostAmount
        {
            get
            {
                double postamount = gvPostVoucherView.GetFocusedRowCellValue(colAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvPostVoucherView.GetFocusedRowCellValue(colAmount).ToString()) : 0.00;
                return postamount;
            }
        }

        
        #endregion

        private void SetTitle()
        {
            this.Text = "Payroll Payment Vouchers";
        }
        
        #region Methods
        /// <summary>
        /// To load the post payment details in grid 
        /// </summary>
        private void LoadPostpaymentDetails()
        {
            try
            {
                RowIndex = gvPostVoucherView.FocusedRowHandle;
                using (clsPrGateWay ObjGateway = new clsPrGateWay())
                {
                    ResultArgs result = ObjGateway.FetchPayrollPostPaymentVouhcerMaster(clsGeneral.PAYROLL_ID);
                    if (result.Success)
                    {
                        DataTable dtPayRollVoucherMaster = result.DataSource.Table;
                        result = ObjGateway.FetchPayrollPostPaymentVouhcerDetails();
                        if (result.Success && result.DataSource.Table!=null)
                        {
                            dvPayRollVoucherDetails = result.DataSource.Table.DefaultView;
                        }
                        gcPostVOucherview.DataSource = dtPayRollVoucherMaster;
                        RowIndex = gvPostVoucherView.FocusedRowHandle;
                        LoadTransDetails();
                    }
                    else
                    {
                        MessageRender.ShowMessage(result);
                    }
                    
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message, true);
            }
            finally { }
        }

        private void LoadTransDetails()
        {
            if (dvPayRollVoucherDetails != null)
            {
                dvPayRollVoucherDetails.RowFilter = dtpayrollFinance.VOUCHER_IDColumn.ColumnName + "=" + VoucherId;
                gcLedgerDetails.DataSource = null;

                gcLedgerDetails.DataSource = dvPayRollVoucherDetails.ToTable();
                gcLedgerDetails.RefreshDataSource();
                gvLedgerDetails.FocusedRowHandle = 0;
                gvLedgerDetails.FocusRectStyle = DrawFocusRectStyle.RowFocus;
            }
        }

        /// <summary>
        /// To load post payment add form when user clicks edit option
        /// </summary>
        private void ShowPostVoucher(bool isPostVoucher)
        {
            try
            {
                if (gvPostVoucherView.RowCount != 0 || isPostVoucher)
                {
                    frmPostPaymentVoucher Postpayment = new frmPostPaymentVoucher((isPostVoucher ? 0 : VoucherId));
                    Postpayment.UpdateHeld += new EventHandler(OnUpdateHeld);
                    Postpayment.ShowDialog();
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_EDIT_GRID_EMTPY));
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message, true);
            }
            finally { }
        }

        /// <summary>
        ///To focus row when showfilter checked changed event.
        /// </summary>
        /// <param name="gridview"></param>
        /// <param name="colGridColumn"></param>
        protected virtual void SetFocusRowFilter(DevExpress.XtraGrid.Views.Grid.GridView gridview, DevExpress.XtraGrid.Columns.GridColumn colGridColumn)
        {
            gridview.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
            gridview.FocusedColumn = colGridColumn;
            gridview.OptionsFind.AllowFindPanel = false;
            gridview.ShowEditor();
        }
        #endregion

        #region Events
        private void ucPostPayment_AddClicked(object sender, EventArgs e)
        {
            ShowPostVoucher(true);
        }

        private void frmPostVoucherview_Load(object sender, EventArgs e)
        {
            LoadPostpaymentDetails();
            SetTitle();
        }

        private void ucPostPayment_EditClicked(object sender, EventArgs e)
        {
            ShowPostVoucher(false);
        }

        private void ucPostPayment_DeleteClicked(object sender, EventArgs e)
        {
            try
            {
                if (gvPostVoucherView.RowCount != 0)
                {
                    using (clsPrGateWay objPrgateway = new clsPrGateWay())
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (CommonMethod.ApplyUserRightsForTransaction((int)Payment.DeletePaymentVoucher) != 0 || this.LoginUser.IsFullRightsReservedUser)
                            {
                                //if (!BaseObject.IsLockedTransaction(BaseObject.dtSelectedVoucherDate))
                                //{
                                //    resultArgs = objPrgateway.DeletePayrollPostPaymentVouchers(VoucherId);
                                //}
                                //else
                                //{
                                //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED_CANNOT_DELETE) + BaseObject.ProjectName + "'");
                                //}
                                resultArgs = objPrgateway.DeletePayrollPostPaymentVouchers(VoucherId);
                            }
                            else
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.NO_RIGHTS_TO_DELETE_PAYMENT));
                            }
                                                        
                            if (resultArgs.Success)
                            {
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                LoadPostpaymentDetails();
                            }
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message, true);
            }
            finally { }
        }

        public void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadPostpaymentDetails();
        }

        private void gvPostVoucherView_DoubleClick(object sender, EventArgs e)
        {
            ShowPostVoucher(false);
        }
        private void ucPostPayment_RefreshClicked(object sender, EventArgs e)
        {
            LoadPostpaymentDetails();
        }
        private void ucPostPayment_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }
        private void gvPostVoucherView_RowCountChanged(object sender, EventArgs e)
        {
            lblRowCount.Text = gvPostVoucherView.RowCount.ToString();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvPostVoucherView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvPostVoucherView, colDate);
            }
        }

        private void frmPostVoucherview_EnterClicked(object sender, EventArgs e)
        {
            ShowPostVoucher(false);
        }

        private void frmPostVoucherview_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void ucPostPayment_PrintClicked(object sender, EventArgs e)
        {
            payrollbase.PrintGridView(gcPostVOucherview, this.GetMessage(MessageCatalog.Payroll.PostPayment.POST_PRINT_CAPTION), PrintType.DT, gvPostVoucherView, false);
        }

        private void gvPostVoucherView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            LoadTransDetails();
        }
        #endregion

        private void frmPostVoucherview_Activated(object sender, EventArgs e)
        {
            //LoadPostpaymentDetails();
        }
    }
}