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

namespace ACPP.Modules.Transaction
{
    public partial class frmBulkTransaction : frmFinanceBaseAdd
    {
        #region VariableDeclaration

        ResultArgs resultArgs = new ResultArgs();
        int[] MultipleVoucherId { get; set; }

        #endregion

        #region Property

        private string VoucherSubType { get; set; }
        private int ProjectId { get; set; }

        #endregion

        #region Constructor

        public frmBulkTransaction()
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
            //For few features permentaly will be locked for SDBINM -----------------
            // This is option to Lock  the Receipt Module features for major purpose -- 13/02/2025
            EnforceReceiptModule(new object[] { chkReceipt });
            //-----------------------------------------------------------------------

            LoadDefaults();
            LoadProject();
            LoadVoucherDetails();
            LoadMapProject();

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
        /// Activate Controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmBulkTransaction_Activated(object sender, EventArgs e)
        {
            //  LoadProject();
            //  LoadVoucherDetails();
            //  LoadMapProject();
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadDefaults()
        {
            dtDateFrom.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtDateFrom.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtDateFrom.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            dtDateto.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtDateto.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            dtDateto.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            if (chkReceipt.Enabled)
            {
                chkReceipt.Checked = true;
            }
            else
            {
                chkPayments.Checked = true;
            }
        }

        /// <summary>
        /// Change the Text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rgBulkTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowHideControls();
        }

        /// <summary>
        /// Show and Hide the Controls when it s necessary
        /// </summary>
        private void ShowHideControls()
        {
            if (rgBulkTransactionType.SelectedIndex == 0)
            {
                btnDeleteTransaction.Text = BulkMoveType.Delete.ToString();
                lciMoveProject.Visibility = LayoutVisibility.Never;
                //For few features permentaly will be locked for SDBINM -----------------
                EnforceReceiptModule(new object[] { chkReceipt });
                //-----------------------------------------------------------------------
            }
            else
            {
                btnDeleteTransaction.Text = BulkMoveType.Move.ToString();
                lciMoveProject.Visibility = LayoutVisibility.Always;

                //For few features permentaly will be locked for SDBINM -----------------
                EnforceReceiptModule(new object[] { chkReceipt }, true);

                if (this.AppSetting.IS_SDB_INM && chkReceipt.Checked && !chkReceipt.Enabled)
                {
                    this.ShowMessageBoxWarning(MessageCatalog.Common.COMMON_RECEIPT_DISABLED_MESSAGE);
                    chkPayments.Checked = true;
                    chkReceipt.Checked = false;
                    LoadVoucherDetails();
                }
                //-----------------------------------------------------------------------
            }
        }

        /// <summary>
        /// Delete the Transaction Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteTransaction_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvTransaction.RowCount > 0)
                {
                    resultArgs = GetCheckedDatasource();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        if (btnDeleteTransaction.Text.Equals(BulkMoveType.Delete.ToString()))
                        {
                            resultArgs = DeleteVoucherTrans(resultArgs.DataSource.Table);
                        }
                        else
                        {
                            if (glkpMapMoveProject.EditValue != null)
                            {
                                resultArgs = MoveTransactions(resultArgs.DataSource.Table);
                            }
                            else
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.TRANSACTION_MOVE_PROJECT_NOT_SELECTED));
                            }
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.TRANSACTION_MOVE_NOT_SELECTED));
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.TRANSACTION_MOVE_GRID_EMPTY));
                }
            }
            catch (Exception ex)
            {
                this.CloseWaitDialog();
                MessageRender.ShowMessage(ex.ToString());
            }
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
                this.SetFocusRowFilter(gvTransaction, colCAmount);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            LoadVoucherDetails();
            LoadMapProject();
        }

        /// <summary>
        /// Receipts Checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Payments Checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Contra Checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkContra_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReceipt.Checked)
            {
                if (chkPayments.Checked)
                {
                }
            }
            if (chkPayments.Checked)
            {
                if (chkPayments.Checked)
                {
                }
                //else
                //{
                //    chkContra.Checked = true;
                //}
            }
            else
            {
                if ((chkPayments.Checked) || (chkReceipt.Checked))
                {
                }
                else
                {
                    chkContra.Checked = true;
                }
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
                DataTable dtVoucher = new DataTable();
                using (VoucherTransactionSystem voucherTransactionSystem = new VoucherTransactionSystem())
                {
                    ProjectId = glkpProject.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
                    string VoucherType = GetSelectedTransactions();
                    dtVoucher = voucherTransactionSystem.LoadVoucherMasterDetails(ProjectId, VoucherType, dtDateFrom.DateTime, dtDateto.DateTime);
                    if (dtVoucher.Rows.Count != 0)
                    {
                        DataView dvBulkTrans = dtVoucher.DefaultView;
                        //string VsubType = "FD";
                        //dvBulkTrans.RowFilter = "VOUCHER_SUB_TYPE<>'" + VsubType + "'";
                        string VsubType = "GN";
                        dvBulkTrans.RowFilter = "VOUCHER_SUB_TYPE='" + VsubType + "'";
                        dtVoucher = dvBulkTrans.ToTable();

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
                        //DataView dvpro = resultArgs.DataSource.Table.AsDataView();
                        //dvpro.RowFilter = "PROJECT_ID=" + ProjectId + "";
                        //bool isProjectavail = false;
                        //if (dvpro.ToTable().Rows.Count > 0)
                        //{
                        //    isProjectavail = true;
                        //}
                        glkpProject.EditValue = this.AppSetting.UserProjectId;
                        this.glkpProject.EditValueChanged += new System.EventHandler(this.glkpProject_EditValueChanged);
                        ShowHideControls();
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
        /// Load Map Project
        /// </summary>
        private void LoadMapProject()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    resultArgs = mappingSystem.FetchProjectsLookup();
                    glkpMapMoveProject.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataView dvpro = resultArgs.DataSource.Table.AsDataView();
                        dvpro.RowFilter = "PROJECT_ID<>" + ProjectId + "";
                        if (dvpro.ToTable().Rows.Count > 0)
                        {
                            this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpMapMoveProject, dvpro.ToTable(), mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                            glkpMapMoveProject.EditValue = glkpMapMoveProject.Properties.GetKeyValue(0);
                            // this.glkpProject.EditValueChanged += new System.EventHandler(this.glkpProject_EditValueChanged);


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
        /// Get the selected Voucher Type
        /// </summary>
        /// <returns></returns>
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
        /// Bulk Delete transaction details
        /// </summary>
        /// <param name="VoucherId"></param>
        private ResultArgs DeleteVoucherTrans(DataTable dtDeleteTransaction)
        {
            try
            {
                if (gvTransaction.RowCount > 0)
                {
                    if (dtDeleteTransaction != null && dtDeleteTransaction.Rows.Count > 0)
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            this.ShowWaitDialog();
                            using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                            {
                                voucherTransaction.dtBulkTransactions = dtDeleteTransaction;
                                resultArgs = voucherTransaction.DeleteMultipleVoucherTrans();
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    LoadVoucherDetails();
                                }
                                else
                                {
                                    this.ShowMessageBox(resultArgs.Message);
                                }
                            }
                            this.CloseWaitDialog();
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
                this.CloseWaitDialog();
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }

            return resultArgs;
        }

        /// <summary>
        /// Move Transactions
        /// </summary>
        /// <param name="dtMoveTransactions"></param>
        /// <returns></returns>
        public ResultArgs MoveTransactions(DataTable dtMoveTransactions)
        {
            try
            {
                if (gvTransaction.RowCount > 0)
                {
                    if (dtMoveTransactions != null && dtMoveTransactions.Rows.Count > 0)
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.TRANSACTION_MOVE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            this.ShowWaitDialog();
                            using (VoucherTransactionSystem TransactionSystem = new VoucherTransactionSystem())
                            {
                                TransactionSystem.ProjectId = glkpMapMoveProject.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpMapMoveProject.EditValue.ToString()) : 0;
                                TransactionSystem.dtBulkTransactions = dtMoveTransactions;
                                TransactionSystem.MoveTransactionType = MultiMoveTransType.Multiple.ToString();
                                resultArgs = TransactionSystem.MoveBulkTransactions();
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.TRANSACTION_MOVE_SUCCESS_NOT_BANK_ACCOUNT));
                                    LoadVoucherDetails();
                                }
                            }
                            this.CloseWaitDialog();
                        }
                    }
                    if (!resultArgs.Success)
                    {
                        this.CloseWaitDialog();
                        if (dtMoveTransactions.Rows.Count > 1)
                        {
                            //resultArgs.ShowMessage("Selected Voucher(s) are moved." + System.Environment.NewLine  +"But few Voucher(s) are not moved because " + resultArgs.Message);
                            resultArgs.ShowMessage("Few Voucher(s) are not moved because " + resultArgs.Message);
                            LoadVoucherDetails();
                        }
                        else
                        {
                            resultArgs.ShowMessage(resultArgs.Message);
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
                this.CloseWaitDialog();
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }

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

        private void glkpProject_QueryPopUp(object sender, CancelEventArgs e)
        {
            //19/07/2021, To set Popup widow size
            if (sender != null)
            {
                GridLookUpEdit editor = (GridLookUpEdit)sender;
                SetGridLookPopupWindowSize(editor);
            }
        }
    }
}
