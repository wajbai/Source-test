/*  Class Name      : frmDeleteVouchers.cs
 *  Purpose         : Physical Deletion of Transaction Details
 *  Author          : Chinna M
 *  Created on      : 20-02-2015
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

using ACPP.Modules.Master;
using ACPP.Modules.UIControls;
using Bosco.Utility;
using Bosco.Model.UIModel.Master;
using Bosco.Model.Transaction;
using Bosco.Utility.CommonMemberSet;
using DevExpress.XtraPrinting;
using Bosco.DAO.Schema;
using Bosco.Model.UIModel;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Bosco.Model.Business;
using DevExpress.XtraBars;
using AcMEDSync.Model;


namespace ACPP.Modules.Transaction
{
    public partial class frmDeleteVouchers : frmFinanceBaseAdd
    {
        #region Variable Decelaration
        public event EventHandler EditHeld;
        int[] VoucherIdData { get; set; }
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
        private string ProjectName
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
        private int VoucherIndex
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
        private string Vouchertype
        {
            get
            {
                vouchertype = gvTransaction.GetFocusedRowCellValue(colVoucherMode) != null ? gvTransaction.GetFocusedRowCellValue(colVoucherMode).ToString() : "";
                return vouchertype;
            }
        }
        #endregion

        #region Constructor
        public frmDeleteVouchers()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties
        private int VoucherMasterId
        {
            get
            {
                RowIndex = gvTransaction.FocusedRowHandle;
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
                RowIndex = gvVoucher.FocusedRowHandle;
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

        private void frmDeleteVouchers_Load(object sender, EventArgs e)
        {
            deDateFrom.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deDateFrom.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deDateFrom.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            deDateTo.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deDateTo.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            deDateTo.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            LoadProject();
            LoadVoucherDetails();
            gvTransaction.FocusedRowHandle = 0;
            gvVoucher.OptionsView.ShowFooter = true;
            glkpProject.EditValue = ProjectId;
        }

        /// <summary>
        /// To delete selected Deleted Records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucTrans_DeleteClicked(object sender, EventArgs e)
        {
            VoucherSubType = gvTransaction.GetFocusedRowCellValue(colVoucherSubType) != null ? gvTransaction.GetFocusedRowCellValue(colVoucherSubType).ToString() : string.Empty;
            VoucherIdData = GetCheckedProjects();
            DeleteVoucherDetails(VoucherIdData);
        }

        private void ucTrans_RestoreVoucherClicked(object sender, EventArgs e)
        {
            RestoreVouchers();
        }

        /// <summary>
        /// Show filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDeleteVouchers_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        /// <summary>
        /// Data To Criteria
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deDateTo_Leave(object sender, EventArgs e)
        {
            if (deDateFrom.DateTime > deDateTo.DateTime)
            {
                DateTime dateTo = deDateTo.DateTime;
                deDateTo.DateTime = deDateFrom.DateTime;
                deDateFrom.DateTime = dateTo.Date;
            }
        }

        /// <summary>
        /// Data From Criteria
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deDateFrom_Leave(object sender, EventArgs e)
        {
            if (IsDateLoaded)
            {
                deDateTo.DateTime = deDateFrom.DateTime.AddMonths(1).AddDays(-1);
                IsDateLoaded = true;
            }
            if (deDateFrom.DateTime > deDateTo.DateTime)
            {
                deDateTo.DateTime = deDateFrom.DateTime;
            }
        }

        /// <summary>
        /// Filter the Records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvTransaction.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvTransaction, colTopLedgerName);
            }
        }


        /// <summary>
        /// Apply the Records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            if (ValidateVoucher())
            {
                if (deDateFrom.DateTime > deDateTo.DateTime)
                {
                    DateTime dateTo = deDateTo.DateTime;
                    deDateTo.DateTime = deDateFrom.DateTime;
                    deDateFrom.DateTime = dateTo.Date;
                }
            }
            LoadVoucherDetails();

        }

        private void ucTrans_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// validate the Deleting Vouchers 
        /// </summary>
        /// <returns></returns>
        public bool ValidateVoucher()
        {
            bool Id = true;
            if (string.IsNullOrEmpty(glkpProject.Text.Trim()))
            {
                //this.ShowMessageBox("Project is empty");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Voucher.VOUCHER_PROJECT_EMPTY));
                Id = false;
                glkpProject.Focus();
            }
            return Id;
        }

        /// <summary>
        /// count the Records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvTransaction_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvTransaction.RowCount.ToString();
        }

        /// <summary>
        /// Validate Receipts check Changed
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
            else if (chkJournal.Checked)
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
        /// Validate Receipts check Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkJournal_CheckedChanged(object sender, EventArgs e)
        {
            if (chkContra.Checked)
            {
                if (chkContra.Checked)
                {
                }
            }
            if (chkReceipt.Checked)
            {
                if (chkContra.Checked)
                {
                }
            }
            else
            {
                if ((chkPayments.Checked) || (chkContra.Checked))
                {
                }
                else
                {
                    chkJournal.Checked = true;
                }
            }
        }

        /// <summary>
        /// Validate Receipts check Changed
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
            else if (chkJournal.Checked)
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
        /// Validate Receipts check Changed
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
            if (chkJournal.Checked)
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

        /// <summary>
        /// Value to be checked in renewal 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvVoucher_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            int value = gvVoucher.FocusedRowHandle;
        }

        /// <summary>
        /// Project fill
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            if (glkpProject.EditValue != null)
            {
                ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
            }
        }

        /// <summary>
        /// to close the form
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="KeyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.Escape))
            {
                this.Close();
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        #endregion

        #region Methods
        /// <summary>
        /// Get selected Projects
        /// </summary>
        /// <returns></returns>
        private int[] GetCheckedProjects()
        {
            int[] SelectedIds = gvTransaction.GetSelectedRows();
            int[] sCheckedProjects = new int[SelectedIds.Count()];
            int ArrayIndex = 0;
            if (SelectedIds.Count() > 0)
            {
                foreach (int RowIndex in SelectedIds)
                {
                    DataRow drProject = gvTransaction.GetDataRow(RowIndex);
                    if (drProject != null)
                    {
                        sCheckedProjects[ArrayIndex] = UtilityMember.NumberSet.ToInteger(drProject["VOUCHER_ID"].ToString());
                        ArrayIndex++;
                    }
                }
            }
            return sCheckedProjects;
        }

        /// <summary>
        /// Load the Projects
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
                        this.glkpProject.EditValueChanged -= new System.EventHandler(this.glkpProject_EditValueChanged);
                        glkpProject.EditValue = (ProjectId != 0) ? ProjectId : glkpProject.Properties.GetKeyValue(0);

                        this.glkpProject.EditValueChanged += new System.EventHandler(this.glkpProject_EditValueChanged);
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
        /// Load the Voucher Details
        /// </summary>
        private void LoadVoucherDetails()
        {
            DataSet dsVoucher = new DataSet();
            using (VoucherTransactionSystem voucherTransactionSystem = new VoucherTransactionSystem())
            {
                string VoucherType = GetSelectedTransactions();
                dsVoucher = voucherTransactionSystem.loadDeleteVoucherDetails(ProjectId, VoucherType, deDateFrom.DateTime, deDateTo.DateTime);
                if (dsVoucher.Tables.Count != 0)
                {
                    gcTransaction.DataSource = dsVoucher;
                    gcTransaction.DataMember = "Master";
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

        /// <summary>
        /// Selected Voucher Type
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


        private ResultArgs RestoreVouchers()
        {
            ResultArgs result = new ResultArgs();
            try
            {
                ResultArgs resultArgs = new ResultArgs();
                if (gvTransaction.RowCount != 0)
                {
                    int[] SelectedVoucherIds = GetCheckedProjects();
                    if (SelectedVoucherIds.Count() > 0)
                    {
                        if (this.ShowConfirmationMessage("General Vouchers alone will be restored, Are you sure to proceed ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
                            {
                                result = voucherSystem.RevertCancelledVouchers(SelectedVoucherIds);
                                if (result.Success)
                                {
                                    //this.ShowSuccessMessage("Cancelled Voucher are reverted successfully.");
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Master.Voucher.VOUCHER_CANCELLED_REVERT_SUCCESS));
                                    LoadVoucherDetails();
                                }
                                else
                                {
                                    this.ShowMessageBoxError(result.Message);
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
                result.Message = ex.ToString();
            }
            return result;
        }

        /// <summary>
        /// Delete the voucher details
        /// </summary>
        /// <param name="DeleteVoucherId"></param>
        private void DeleteVoucherDetails(int[] DeletedVoucherId)
        {
            try
            {
                string iDeleteVoucherType = gvTransaction.GetFocusedRowCellValue(colVoucherMode) != null ? gvTransaction.GetFocusedRowCellValue(colVoucherMode).ToString() : string.Empty;
                ResultArgs resultArgs = new ResultArgs();
                if (gvTransaction.RowCount != 0)
                {
                    using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                    {
                        //if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION) + " " , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        if (this.ShowConfirmationMessage("Are you sure to delete selected Vouchers permanently, those Vouchers will be available in Acmeerp", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            foreach (int GetDeletedVoucherId in DeletedVoucherId)
                            {
                                voucherTransaction.VoucherId = GetDeletedVoucherId;
                                if (VoucherSubType == ledgerSubType.FD.ToString())
                                {
                                    using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                                    {
                                        fdAccountSystem.VoucherId = GetDeletedVoucherId;
                                        resultArgs = fdAccountSystem.FetchPhysicalFDAccountId();
                                        if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                                        {
                                            fdAccountSystem.FDVoucherId = GetDeletedVoucherId;
                                            fdAccountSystem.FDAccountId = fdAccountSystem.FetchFDAId();
                                            if (fdAccountSystem.FDAccountId != 0)
                                            {
                                                if (fdAccountSystem.CheckPhysicalFDAccountExists() != 0)
                                                {
                                                    resultArgs = voucherTransaction.DeletePhysicalVoucherTrans();
                                                    if (resultArgs.Success)
                                                    {
                                                        resultArgs = fdAccountSystem.DeleteFDPhysicalRenewalDetails();
                                                        if (resultArgs.Success)
                                                        {
                                                            resultArgs = fdAccountSystem.DeleteFDPhysicalAccountDetails();
                                                            if (resultArgs.Success)
                                                            {
                                                                //this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                                                //LoadVoucherDetails();
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    resultArgs = fdAccountSystem.DeleteFDPhysicalAccountDetails();
                                                    if (resultArgs.Success)
                                                    {
                                                        //this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                                        //LoadVoucherDetails();
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                string RenewalType = string.Empty;
                                                string VoucherId = string.Empty;
                                                string VoucherId1 = string.Empty;
                                                string VoucherId2 = string.Empty;
                                                DataTable dtRenewalType = fdAccountSystem.FetchPhysicalFDRenewalType();
                                                foreach (DataRow dr in dtRenewalType.Rows)
                                                {
                                                    RenewalType = dr[fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName] != null ? dr[fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName].ToString() : string.Empty;
                                                    fdAccountSystem.FDAccountId = dr[fdAccountSystem.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(dr[fdAccountSystem.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn.ColumnName].ToString()) : 0;
                                                    VoucherId = dr[fdAccountSystem.AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn.ColumnName] != null ? dr[fdAccountSystem.AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn.ColumnName].ToString() : string.Empty;
                                                    VoucherId1 = dr[fdAccountSystem.AppSchema.FDRenewal.FD_VOUCHER_IDColumn.ColumnName] != null ? dr[fdAccountSystem.AppSchema.FDRenewal.FD_VOUCHER_IDColumn.ColumnName].ToString() : string.Empty;
                                                    VoucherId2 = VoucherId1 + "," + VoucherId;
                                                    if (RenewalType == FDRenewalTypes.WDI.ToString())
                                                    {
                                                        string[] voucherId = VoucherId2.Split(',');
                                                        foreach (string sValue in voucherId)
                                                        {
                                                            voucherTransaction.VoucherId = this.UtilityMember.NumberSet.ToInteger(sValue);
                                                            resultArgs = voucherTransaction.DeletePhysicalVoucherTrans();
                                                        }
                                                        if (resultArgs.Success)
                                                        {
                                                            resultArgs = fdAccountSystem.DeleteFDPhysicalRenewalsByVoucherId();
                                                            if (resultArgs.Success)
                                                            {
                                                                //this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                                                //LoadVoucherDetails();
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //if (fdAccountSystem.CheckFDPhysicalRenewalClosed() == 0)
                                                        //{
                                                        resultArgs = voucherTransaction.DeletePhysicalVoucherTrans();
                                                        if (resultArgs.Success)
                                                        {
                                                            resultArgs = fdAccountSystem.DeleteFDPhysicalRenewalsByVoucherId();
                                                            if (resultArgs.Success)
                                                            {
                                                                //this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                                                //LoadVoucherDetails();
                                                            }
                                                        }

                                                        //}
                                                        //else
                                                        //{
                                                        //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_CANNOT_DELETE_FD_RECEIPT_ENTRY));
                                                        //}
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            fdAccountSystem.FDVoucherId = GetDeletedVoucherId;
                                            resultArgs = fdAccountSystem.DeleteFDPhysicalAccountDetails();
                                            if (resultArgs.Success)
                                            {
                                                resultArgs = voucherTransaction.DeletePhysicalVoucherTrans();
                                                if (resultArgs.Success)
                                                {
                                                    //this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                                    //LoadVoucherDetails();
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (!this.LoginUser.IsFullRightsReservedUser)
                                    {
                                        if (iDeleteVoucherType == DefaultVoucherTypes.Receipt.ToString())
                                        {
                                            if (CommonMethod.ApplyUserRightsForTransaction((int)Receipt.DeleteReceiptVoucher) != 0)
                                            {
                                                resultArgs = DeleteVoucherTrans(GetDeletedVoucherId);

                                            }
                                            else
                                            {
                                                //this.ShowMessageBox("No rights to delete this Receipt Vouchers");
                                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Voucher.VOUCHER_RECEIPT_NORIGHTS_DELETE));
                                            }
                                        }
                                        else if (iDeleteVoucherType == DefaultVoucherTypes.Payment.ToString())
                                        {
                                            if (CommonMethod.ApplyUserRightsForTransaction((int)Payment.DeletePaymentVoucher) != 0)
                                            {
                                                resultArgs = DeleteVoucherTrans(GetDeletedVoucherId);

                                            }
                                            else
                                            {
                                                //this.ShowMessageBox("No rights to delete this Payment Vouchers");
                                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Voucher.VOUCHER_PAYMENT_NORIGHTS_DELETE));
                                            }
                                        }
                                        else
                                        {
                                            if (CommonMethod.ApplyUserRightsForTransaction((int)Contra.DeleteContraVoucher) != 0)
                                            {
                                                resultArgs = DeleteVoucherTrans(GetDeletedVoucherId);
                                            }
                                            else
                                            {
                                                //this.ShowMessageBox("No rights to delete this Contra Vouchers");
                                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Voucher.VOUCHER_CONTRA_NORIGHTS_DELETE));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        resultArgs = DeleteVoucherTrans(GetDeletedVoucherId);
                                    }
                                }
                            }
                        }
                        if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                            LoadVoucherDetails();
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
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// Delete the voucher trans physically from Database
        /// </summary>
        /// <param name="VoucherId"></param>
        private ResultArgs DeleteVoucherTrans(int VoucherId)
        {
            try
            {
                using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                {
                    voucherTransaction.VoucherId = VoucherId;
                    voucherTransaction.tdsTransType = TDSTransType.TDSPartyPayment;
                    resultArgs = voucherTransaction.DeletePhysicalVoucherTrans();
                    if (resultArgs.Success)
                    {
                        //this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                        //LoadVoucherDetails();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
            return resultArgs;
        }
        #endregion

        private void ucTrans_PrintClicked(object sender, EventArgs e)
        {
            string reportTitle = "Deleted Vouchers - " + glkpProject.Text  + " (" + deDateFrom.DateTime.ToShortDateString() + " " + deDateTo.DateTime.ToShortDateString() + ")";
            
            PrintGridViewDetails(gcTransaction, reportTitle, PrintType.DS, gvTransaction, true);
        }

        


    }
}