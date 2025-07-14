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
using Bosco.Model.UIModel;
using Bosco.Utility;

using DevExpress.Utils.Frames;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace ACPP.Modules.Transaction
{
    public partial class frmTransactionVoucherView : frmFinanceBaseAdd
    {
        #region Variables
        public event EventHandler VoucherEditHeld;
        public string ProjectName = string.Empty;
        public int ProId = 0;
        DateTime LastVoucheDate;
        bool JournalEntyAlone = false;
        #endregion

        #region Constructor

        public frmTransactionVoucherView()
        {
            InitializeComponent();
        }
        public frmTransactionVoucherView(string ProName, int proid, DateTime dtfrom, bool journalentyalone=false)
            : this()
        {
            ProjectName = ProName;
            ProId = proid;
            LastVoucheDate = dtfrom;
            JournalEntyAlone = journalentyalone;
        }
        #endregion

        #region Properties
        private int VoucherMasterId
        {
            get
            {
                int VoucherId = 0;
                VoucherId = gvTransaction.GetFocusedRowCellValue(colVoucherID) != null ? this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetFocusedRowCellValue(colVoucherID).ToString()) : 0;
                return VoucherId;
            }
        }

        private int ProjectId
        {
            get
            {
                int projectId = 0;
                projectId = gvTransaction.GetFocusedRowCellValue(colProjectId) != null ? this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetFocusedRowCellValue(colProjectId).ToString()) : 0;
                return projectId;
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

        private Int32 voucherdefinitiond = 0;
        private Int32 VoucherDefinitionId
        {
            get
            {
                voucherdefinitiond = gvTransaction.GetFocusedRowCellValue(colVoucherDefinitionId) != null ? this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetFocusedRowCellValue(colVoucherDefinitionId).ToString()) : 0;
                return voucherdefinitiond;
            }
        }

       
        private string project = "";
        private string Project
        {
            get
            {
                project = gvTransaction.GetFocusedRowCellValue(colProject) != null ? gvTransaction.GetFocusedRowCellValue(colProject).ToString() : "";
                return project;
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
        #endregion

        #region Events

        private void frmTransactionVoucherView_Load(object sender, EventArgs e)
        {
            SetDefaults();
            LoadVoucherDetails();
            this.isEditable = true;
            this.ribDuplicateVoucher.Buttons[0].ToolTip = "To make Replicate Voucher (Alt+U)";
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnApply_Click(object sender, EventArgs e)
        {
            LoadVoucherDetails();
        }

        #endregion

        #region Methods

        private void LoadVoucherDetails()
        {
            if (deDateFrom.DateTime > deDateTo.DateTime)
            {
                DateEdit dttemp = new DateEdit();
                dttemp.DateTime = deDateTo.DateTime;
                deDateTo.DateTime = deDateFrom.DateTime;
                deDateFrom.DateTime = dttemp.DateTime;
            }

            DataSet dsVoucher = new DataSet();
            using (VoucherTransactionSystem voucherTransactionSystem = new VoucherTransactionSystem())
            {
                string VoucherTypes = string.Empty;
                if (!this.LoginUser.IsFullRightsReservedUser)
                {
                    if (!JournalEntyAlone)
                    {
                        if (CommonMethod.ApplyUserRights((int)Forms.CreateReceiptVoucher) > 0 || CommonMethod.ApplyUserRights((int)Forms.EditReceiptVoucher) > 0)
                        {
                            VoucherTypes = "RC,";
                        }
                        if (CommonMethod.ApplyUserRights((int)Forms.CreatePaymentVoucher) > 0 || CommonMethod.ApplyUserRights((int)Forms.EditPaymentVoucher) > 0)
                        {
                            VoucherTypes += "PY,";
                        }
                        if (CommonMethod.ApplyUserRights((int)Forms.CreateContraVoucher) > 0 || CommonMethod.ApplyUserRights((int)Forms.EditContraVoucher) > 0)
                        {
                            VoucherTypes += "CN,";
                        }
                        if (CommonMethod.ApplyUserRights((int)Forms.CreateJournalVoucher) > 0 || CommonMethod.ApplyUserRights((int)Forms.EditJournalVoucher) > 0)
                        {
                            VoucherTypes += "JN";
                        }
                    }
                    else
                    {
                        VoucherTypes = "JN";
                    }
                    VoucherTypes = VoucherTypes.TrimEnd(',');
                }
                else
                {
                    if (!JournalEntyAlone)
                    {
                        VoucherTypes = "RC,PY,CN";
                    }
                    else
                    {
                        VoucherTypes = "JN";
                    }
                }

                if (VoucherTypes.Equals("JN"))
                {
                    gridColumn10.Visible = gridColumn9.Visible = gridColumn8.Visible = false;
                    gcDonorName.Visible = gcolVoucherType.Visible = gcLedgerName.Visible = gcCashBank.Visible = colNameAddress.Visible = false;
                    dsVoucher = voucherTransactionSystem.LoadJournalVoucherDetails(ProId, deDateFrom.DateTime, deDateTo.DateTime);
                }
                else
                {
                    dsVoucher = voucherTransactionSystem.LoadVoucherDetails(ProId, VoucherTypes, deDateFrom.DateTime, deDateTo.DateTime);
                }

                if (dsVoucher.Tables.Count > 0)
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
            }
        }

        private void SetDefaults()
        {
            ucCaptionPanel1.Caption = ProjectName;
            ucCaptionPanel1.CaptionSize = 12;

            deDateFrom.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deDateFrom.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);

            deDateTo.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deDateTo.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);

            if (LastVoucheDate.ToString() != string.Empty)
            {
                deDateTo.DateTime = LastVoucheDate.Date;
                deDateFrom.DateTime = (new DateTime(deDateTo.DateTime.Year, deDateTo.DateTime.Month, 1) < deDateFrom.Properties.MinValue) ? UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false) : new DateTime(deDateTo.DateTime.Year, deDateTo.DateTime.Month, 1);
            }
            else
            {
                deDateFrom.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
                deDateTo.DateTime = (deDateTo.DateTime > deDateTo.Properties.MaxValue) ? UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false) : deDateFrom.DateTime.AddMonths(1).AddDays(-1);
            }
        }

        #endregion

        private void deDateFrom_Leave(object sender, EventArgs e)
        {
            deDateTo.DateTime = (deDateTo.DateTime > deDateTo.Properties.MaxValue) ? UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false) : deDateFrom.DateTime.AddMonths(1).AddDays(-1);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.F3))
            {
                deDateFrom.Focus();
            }
            else if (KeyData == (Keys.Alt | Keys.U))
            {
                MakeDuplicationEntry();
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        private void frmTransactionVoucherView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }


        private void gvTransaction_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvTransaction.RowCount.ToString();
        }

        private void ShowForm(int VoucherID, bool duplicate = false)
        {
            int fdAccountId = 0;
            int FdVoucherID = 0;
            int ProjId = 0;
            int FDStatus = 0;
            int FDCount = 0;
            string TempFdType = string.Empty;
            FDTypes fdTypes;
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                string VoucherSubType = gvTransaction.GetFocusedRowCellValue(colVoucherSubType) != null ? gvTransaction.GetFocusedRowCellValue(colVoucherSubType).ToString() : string.Empty;

                if (VoucherID > 0)
                {
                    if (Vouchertype == DefaultVoucherTypes.Receipt.ToString()) { VoucherIndex = 0; } else if (Vouchertype == DefaultVoucherTypes.Payment.ToString()) { VoucherIndex = 1; } else { VoucherIndex = 2; }
                }
                if (VoucherSubType != ledgerSubType.FD.ToString())
                {
                    if (!this.LoginUser.IsFullRightsReservedUser)
                    {
                        if (Vouchertype == DefaultVoucherTypes.Receipt.ToString())
                        {
                            if (CommonMethod.ApplyUserRightsForTransaction((int)Receipt.EditReceiptVoucher) != 0)
                            {
                                CloseAlreadyOpenedEntryScreen();
                                frmTransactionMultiAdd frmTrans = new frmTransactionMultiAdd(deDateFrom.Text, ProjectId, Project, VoucherID, VoucherIndex, duplicate);
                                frmTrans.UpdateHeld += new EventHandler(OnUpdateHeld);
                                frmTrans.EditHeld += new EventHandler(frmTrans_EditHeld);
                                frmTrans.ShowDialog();
                            }
                            else
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.NO_RIGHTS_TO_EDIT_RECEIPT));
                            }
                        }
                        else if (Vouchertype == DefaultVoucherTypes.Payment.ToString())
                        {
                            if (CommonMethod.ApplyUserRightsForTransaction((int)Payment.EditPaymentVoucher) != 0)
                            {
                                CloseAlreadyOpenedEntryScreen();
                                frmTransactionMultiAdd frmTrans = new frmTransactionMultiAdd(deDateFrom.Text, ProjectId, Project, VoucherID, VoucherIndex, duplicate);
                                frmTrans.UpdateHeld += new EventHandler(OnUpdateHeld);
                                frmTrans.EditHeld += new EventHandler(frmTrans_EditHeld);
                                frmTrans.ShowDialog();
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
                                CloseAlreadyOpenedEntryScreen();
                                frmTransactionMultiAdd frmTrans = new frmTransactionMultiAdd(deDateFrom.Text, ProjectId, Project, VoucherID, VoucherIndex, duplicate);
                                frmTrans.UpdateHeld += new EventHandler(OnUpdateHeld);
                                frmTrans.EditHeld += new EventHandler(frmTrans_EditHeld);
                                frmTrans.ShowDialog();
                            }
                            else
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.NO_RIGHTS_TO_EDIT_CONTRA));
                            }
                        }
                    }
                    else
                    {
                        if (Vouchertype != "JN")
                        {
                            CloseAlreadyOpenedEntryScreen();
                            frmTransactionMultiAdd frmTrans = new frmTransactionMultiAdd(deDateFrom.Text, ProjectId, Project, VoucherID, VoucherIndex, duplicate, VoucherDefinitionId);
                            frmTrans.UpdateHeld += new EventHandler(OnUpdateHeld);
                            frmTrans.EditHeld += new EventHandler(frmTrans_EditHeld);
                            frmTrans.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            JournalAdd frmJournalAdd = new JournalAdd("JN", ProjectId, ProjectName, VoucherMasterId, 0);
                            frmJournalAdd.UpdateHeld += new EventHandler(OnUpdateHeld);
                            frmJournalAdd.ShowDialog();
                            this.Close();

                        }

                    }
                }
                else
                {
                    fdAccountId = gvTransaction.GetFocusedRowCellValue(colFDVoucherId) != null ? this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetFocusedRowCellValue(colFDVoucherId).ToString()) : 0;
                    FdVoucherID = VoucherMasterId;
                    using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                    {
                        fdAccountSystem.FDAccountId = fdAccountId;
                        FDCount = fdAccountSystem.CountRenewalDetails();
                        fdAccountSystem.VoucherId = VoucherMasterId;
                        resultArgs = fdAccountSystem.FetchFDAccountId();
                        if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                        {
                            fdAccountSystem.FDAccountId = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName].ToString());
                            if (fdAccountSystem.CheckFDRenewalClosed() == 0)
                            {
                                fdAccountId = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName].ToString()) : fdAccountId;
                                ProjId = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.PROJECT_IDColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.PROJECT_IDColumn.ColumnName].ToString()) : 0;
                                FDStatus = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_STATUSColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_STATUSColumn.ColumnName].ToString()) : 0;
                                TempFdType = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName] != null ? resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName].ToString() : string.Empty;
                            }
                            else
                            {
                                if (Vouchertype == DefaultVoucherTypes.Receipt.ToString())
                                {
                                    if (fdAccountSystem.CheckRenewalTypeByVoucherId() == FDRenewalTypes.IRI.ToString())
                                    {
                                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_CANNOT_EDIT_FD_RECEIPT_ENTRY));
                                        return;
                                    }
                                    else
                                    {
                                        fdAccountId = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName].ToString()) : fdAccountId;
                                        ProjId = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.PROJECT_IDColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.PROJECT_IDColumn.ColumnName].ToString()) : 0;
                                        FDStatus = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_STATUSColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_STATUSColumn.ColumnName].ToString()) : 0;
                                        TempFdType = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName] != null ? resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName].ToString() : string.Empty;
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
                                        ProjId = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.PROJECT_IDColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.PROJECT_IDColumn.ColumnName].ToString()) : 0;
                                        FDStatus = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_STATUSColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_STATUSColumn.ColumnName].ToString()) : 0;
                                        TempFdType = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName] != null ? resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName].ToString() : string.Empty;
                                    }
                                }
                            }
                        }
                    }

                    if (Vouchertype == DefaultVoucherTypes.Contra.ToString())
                    {
                        if (TempFdType == FDRenewalTypes.WDI.ToString())
                            fdTypes = FDTypes.WD;
                        else
                            fdTypes = FDTypes.IN;
                    }
                    else
                    {
                        if (TempFdType == FDRenewalTypes.WDI.ToString())
                            fdTypes = FDTypes.WD;
                        else
                            fdTypes = FDTypes.RN;
                    }
                    if (FDStatus != 0 || TempFdType != FDRenewalTypes.IRI.ToString())
                    {
                        ACPP.Modules.Master.frmFDAccount frmAccount = new frmFDAccount(fdAccountId, FdVoucherID, fdTypes);
                        frmAccount.ProjectId = ProjId;
                        frmAccount.FDRenewalCount = FDCount;
                        frmAccount.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }//LoadVoucherDetails(); }
        }

        void frmTrans_EditHeld(object sender, EventArgs e)
        {
            this.Close();
        }

        //protected override void OnEditHeld(object sender, EventArgs e)
        //{
        //    this.Close();
        //}

        private void gcTransaction_DoubleClick(object sender, EventArgs e)
        {
            ShowVoucherEntry(false);
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvTransaction.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvTransaction, colDate);
            }
        }


        /// <summary>
        /// On 09/02/2018, Before show entry screen, check if already opended, close it and reopen
        /// </summary>
        private void CloseAlreadyOpenedEntryScreen()
        {
            foreach (Form frmVoucherEntry in Application.OpenForms)
            {
                if (frmVoucherEntry.Name == typeof(frmTransactionMultiAdd).Name)
                {
                    frmVoucherEntry.Close();
                    frmVoucherEntry.Dispose();
                    break;
                }
            }
        }

        /// <summary>
        /// On 09/02/2018, Show Voucher Entry Screen
        /// </summary>
        private void ShowVoucherEntry(bool duplicate =false)
        {
            try
            {
                if (this.isEditable)
                {
                    if (VoucherMasterId != 0)
                    {
                        if (Vouchertype == DefaultVoucherTypes.Receipt.ToString() && !this.AppSetting.ENABLE_TRACK_RECEIPT_MODULE)
                        {
                            this.ShowMessageBox(MessageCatalog.Common.COMMON_RECEIPT_DISABLED_MESSAGE);
                        }
                        else
                        {
                            if (gvTransaction.RowCount != 0)
                            {
                                //if (VoucherEditHeld != null && e != null)
                                //{
                                //    VoucherEditHeld(this, e);
                                //}

                                ShowForm(VoucherMasterId, duplicate);
                            }
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
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

        private void ribDuplicateVoucher_Click(object sender, EventArgs e)
        {
            MakeDuplicationEntry();
        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            
        }

        private void MakeDuplicationEntry()
        {
            string VoucherSubType = gvTransaction.GetFocusedRowCellValue(colVoucherSubType) != null ? gvTransaction.GetFocusedRowCellValue(colVoucherSubType).ToString() : string.Empty;
            if (!string.IsNullOrEmpty(Vouchertype))
            {
                if ((Vouchertype == DefaultVoucherTypes.Receipt.ToString() || Vouchertype == DefaultVoucherTypes.Payment.ToString() || Vouchertype == DefaultVoucherTypes.Contra.ToString()) 
                    && (VoucherSubType == ledgerSubType.GN.ToString()))
                {
                    if (this.ShowConfirmationMessage("Do you want to Replicate Voucher Entry ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        ShowVoucherEntry(true);
                    }
                }
                else
                {
                    this.ShowMessageBox("Receipts/Payments/Contra Vouchers alone can be Replicated");
                }
            }
        }

        //private void btnDuplicateVoucher_Click(object sender, EventArgs e)
        //{
        //    string VoucherSubType = gvTransaction.GetFocusedRowCellValue(colVoucherSubType) != null ? gvTransaction.GetFocusedRowCellValue(colVoucherSubType).ToString() : string.Empty;
        //    if (!string.IsNullOrEmpty(Vouchertype))
        //    {
        //        if ((Vouchertype == "Receipt" || Vouchertype == "Payment") && (VoucherSubType == ledgerSubType.GN.ToString()))
        //        {
        //            if (this.ShowConfirmationMessage("Do you want to duplicate Voucher Entry ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
        //            {
        //                ShowVoucherEntry(e, true);
        //            }
        //        }
        //        else
        //        {
        //            this.ShowMessageBox("Receipts/Payments Vouchers alone can be duplicated");
        //        }
        //    }
        //}
    }
}
