using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ACPP.Modules.Master;
using Bosco.Model.Business;
using Bosco.Model.Transaction;
using Bosco.Model;
using Bosco.Model.UIModel;
using Bosco.Utility;

using DevExpress.Utils.Frames;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using Bosco.Model.UIModel.Master;

namespace ACPP.Modules.Transaction
{
    public partial class frmAuthorizeVoucher : frmFinanceBaseAdd
    {
        #region VariableDeclaration

        ResultArgs resultArgs = new ResultArgs();
        int[] MultipleVoucherId { get; set; }

        #endregion

        #region Property

        private int ProjectId { 
            get
            {
                return glkpProject.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
            }
        }

        private string SelectedVoucherType
        {
            get
            {
                string selectedvtypes = string.Empty;
                chkListVoucherTypes.RefreshEditValue();
                List<object> selecteditems = chkListVoucherTypes.Properties.Items.GetCheckedValues();

                foreach (object item in selecteditems)
                {
                    selectedvtypes += item.ToString() + ",";
                }
                selectedvtypes = selectedvtypes.TrimEnd(',');
                return selectedvtypes;
            }
        }

        #endregion

        #region Constructor

        public frmAuthorizeVoucher()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        /// <summary>
        /// Load the Bulk Transaction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmTransactionBulkVoucherView_Load(object sender, EventArgs e)
        {
            LoadDefaults();
            LoadProject();
            LoadVoucherDetails();
        }

        /// <summary>
        /// Apply the Transaction Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            LoadVoucherDetails();
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadDefaults()
        {
            using (VoucherSystem vsystem = new VoucherSystem())
            {
                ResultArgs result = vsystem.FetchVoucerDetail();
                chkListVoucherTypes.Properties.DataSource = null;
                if (result.Success)
                {
                    DataTable dtVoucherTypes = result.DataSource.Table;
                    //dtVoucherTypes.DefaultView.RowFilter = vsystem.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName + " <=4";
                    //dtVoucherTypes = dtVoucherTypes.DefaultView.ToTable();
                    chkListVoucherTypes.Properties.DataSource = result.DataSource.Table;
                    chkListVoucherTypes.Properties.DisplayMember = vsystem.AppSchema.Voucher.VOUCHER_NAMEColumn.ColumnName;
                    chkListVoucherTypes.Properties.ValueMember = vsystem.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName;

                    chkListVoucherTypes.EditValue = (int)DefaultVoucherTypes.Receipt; //Select Receitps by Default
                }
            }

            dtDateFrom.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtDateFrom.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtDateFrom.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            dtDateto.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtDateto.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            dtDateto.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
        }
        
        /// <summary>
        /// close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// filter the records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvTransaction.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvTransaction, colDate);
            }
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

        private void btnAuthorize_Click(object sender, EventArgs e)
        {
            ResultArgs result = new ResultArgs();
            try
            {
                if (gvTransaction.RowCount > 0)
                {
                    int[] SelectedIds = gvTransaction.GetSelectedRows();

                    if (SelectedIds.Length > 0)
                    {
                        if (this.ShowConfirmationMessage("Are you sure to authorize the selected Voucher(s) ?", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            this.ShowWaitDialog("Updating authorizing Vouchers");
                            foreach (int vidIndex in SelectedIds)
                            {
                                DataRow dr = gvTransaction.GetDataRow(vidIndex);
                                int vid = UtilityMember.NumberSet.ToInteger(dr[colVid.FieldName].ToString());
                                using (VoucherTransactionSystem vouchertranssys = new VoucherTransactionSystem())
                                {
                                    result = vouchertranssys.AuthorizeVouchers(vid);
                                }
                                if (!result.Success)
                                {
                                    break;
                                }
                            }

                            if (result.Success)
                            {
                                this.ShowMessageBox("Selected Voucher(s) are authorized");
                            }
                            else
                            {
                                this.ShowMessageBox(result.Message);
                            }
                            LoadVoucherDetails();
                            this.CloseWaitDialog();
                        }
                    }
                    else
                    {
                        this.ShowMessageBox("Select Voucher(s) to be authorized");
                    }
                }
            }
            catch (Exception ex)
            {
                this.CloseWaitDialog();
                MessageRender.ShowMessage(ex.ToString());
            }
        }

        #endregion

        #region Method

        /// <summary>
        /// Load the Transaction Details
        /// </summary>
        public void LoadVoucherDetails()
        {
            try
            {
                if (string.IsNullOrEmpty(SelectedVoucherType))
                {
                    this.ShowMessageBox("Select Voucher Type");
                }
                else
                {
                    using (VoucherTransactionSystem voucherTransactionSystem = new VoucherTransactionSystem())
                    {
                        ResultArgs result = voucherTransactionSystem.FetchVouchersForAuthorization(ProjectId, SelectedVoucherType, dtDateFrom.DateTime, dtDateto.DateTime);
                        if (result.Success && result.DataSource.Table != null)
                        {
                            DataTable dtVoucher = result.DataSource.Table;
                            gcTransaction.DataSource = dtVoucher;
                            gcTransaction.RefreshDataSource();
                        }
                        else
                        {
                            gcTransaction.DataSource = null;
                            gcTransaction.RefreshDataSource();
                        }
                        gvTransaction.FocusedRowHandle = 0;
                        gvTransaction.FocusRectStyle = DrawFocusRectStyle.RowFocus;
                    }
                }
            }
            catch (Exception err)
            {
                XtraMessageBox.Show(err.Message);
            }
            finally
            {
                { };
            }
        }

        /// <summary>
        /// Load Project
        /// </summary>
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
                        if (!string.IsNullOrEmpty(this.AppSetting.UserProjectId))
                        {
                            glkpProject.EditValue = this.AppSetting.UserProjectId;
                        }
                        else
                        {
                            glkpProject.EditValue = UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString());
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private ResultArgs GetCheckedDatasource()
        {
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                int[] SelectedIds = gvTransaction.GetSelectedRows();
                if (SelectedIds.Length != 0)
                {

                    DataTable dtBulkTransactionSource = gcTransaction.DataSource as DataTable;
                    DataTable dtMultipleSelectedSource = new DataTable();
                    dtMultipleSelectedSource = dtBulkTransactionSource.Clone();
                    if (SelectedIds.Count() > 0)
                    {
                        foreach (int RowIndex in SelectedIds)
                        {
                            DataRow drTransactions = gvTransaction.GetDataRow(RowIndex);
                            if (drTransactions != null)
                            {
                                dtMultipleSelectedSource.ImportRow(drTransactions);
                            }
                        }
                        if (dtMultipleSelectedSource != null)
                        {
                            resultArgs.DataSource.Data = dtMultipleSelectedSource;
                            resultArgs.Success = true;
                        }
                    }

                    // chinna 

                    //DataSet dsBulkTransactionSource = gcTransaction.DataSource as DataSet;
                    //DataTable dtBulkTransactionSource = dsBulkTransactionSource.Tables[0];
                    //DataTable dtMultipleSelectedSource = new DataTable();
                    //dtMultipleSelectedSource = dtBulkTransactionSource.Clone();
                    //if (SelectedIds.Count() > 0)
                    //{
                    //    foreach (int RowIndex in SelectedIds)
                    //    {
                    //        DataRow drTransactions = gvTransaction.GetDataRow(RowIndex);
                    //        if (drTransactions != null)
                    //        {
                    //            dtMultipleSelectedSource.ImportRow(drTransactions);
                    //        }
                    //    }
                    //    if (dtMultipleSelectedSource != null)
                    //    {
                    //        resultArgs.DataSource.Data = dtMultipleSelectedSource;
                    //        resultArgs.Success = true;
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            return resultArgs;
        }
                
        /// <summary>
        /// row count
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvTransaction_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvTransaction.RowCount.ToString();
        }
        #endregion

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string title = glkpProject.Text;
            string title1 = "List of Unauthorized Voucher(s)";
            PrintGridViewDetails(gvTransaction, title, null, title1);
        }
    }
}
