using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.Model.Transaction;
using Bosco.Model.UIModel.Master;
using Bosco.Report.Base;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using System.Threading;

namespace ACPP.Modules.Transaction
{
    public partial class frmTransactionJournalView : frmFinanceBase
    {
        #region Properties
        private int projectId = 0;
        private int ProjectId
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

        private int voucherId = 0;
        private int VoucherId
        {
            get
            {
                RowIndex = gvJournal.FocusedRowHandle;
                if (gvJournal.GetFocusedRowCellValue(colVoucherId) != null) { voucherId = this.UtilityMember.NumberSet.ToInteger(gvJournal.GetFocusedRowCellValue(colVoucherId).ToString()); }
                return voucherId;
            }
            set
            {
                voucherId = value;
            }
        }

        private int ledgerId;
        private int LedgerId
        {
            set
            {
                ledgerId = value;
            }
            get
            {
                ledgerId = gvJournalLedger.GetFocusedRowCellValue(colLedgerId) != null ? this.UtilityMember.NumberSet.ToInteger(gvJournalLedger.GetFocusedRowCellValue(colLedgerId).ToString()) : 0;
                return ledgerId;
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

        private string voucherSubTypes = string.Empty;
        private string VoucherSubTypes
        {
            get
            {
                voucherSubTypes = gvJournal.GetFocusedRowCellValue(colVoucherSubType) != null ? gvJournal.GetFocusedRowCellValue(colVoucherSubType).ToString() : string.Empty;
                return voucherSubTypes;
            }
            set
            {
                voucherSubTypes = value;
            }
        }

        private int fdStatus = 0;
        private int FDStaus
        {
            get
            {
                fdStatus = gvJournal.GetFocusedRowCellValue(colFDStatus) != null ? this.UtilityMember.NumberSet.ToInteger(gvJournal.GetFocusedRowCellValue(colFDStatus).ToString()) : 0;
                return fdStatus;
            }
            set
            {
                fdStatus = value;
            }
        }

        private int fdAccountID = 0;
        private int FDAccountID
        {
            get
            {
                fdAccountID = gvJournal.GetFocusedRowCellValue(colFDAccountId) != null ? this.UtilityMember.NumberSet.ToInteger(gvJournal.GetFocusedRowCellValue(colFDAccountId).ToString()) : 0;
                return fdAccountID;
            }
            set
            {
                fdAccountID = value;
            }
        }

        private int bookingId = 0;
        private int TDSBookingId
        {
            get { return gvJournal.GetFocusedRowCellValue(colBookingId) != null ? this.UtilityMember.NumberSet.ToInteger(gvJournal.GetFocusedRowCellValue(colBookingId).ToString()) : 0; }
            set { bookingId = value; }
        }

        private int expLedgerId = 0;
        private int ExpenseLedgerId
        {
            get { return gvJournal.GetFocusedRowCellValue(colExpLedgerId) != null ? this.UtilityMember.NumberSet.ToInteger(gvJournal.GetFocusedRowCellValue(colExpLedgerId).ToString()) : 0; }
            set { expLedgerId = value; }
        }

        private int deducteeTypeId = 0;
        private int DeducteeId
        {
            get { return gvJournal.GetFocusedRowCellValue(colDeducteeTypeId) != null ? this.UtilityMember.NumberSet.ToInteger(gvJournal.GetFocusedRowCellValue(colDeducteeTypeId).ToString()) : 0; }
            set { deducteeTypeId = value; }
        }

        private int partyLedgerId = 0;
        private int PartyLedgerId
        {
            get { return gvJournal.GetFocusedRowCellValue(colPartyLedgerId) != null ? this.UtilityMember.NumberSet.ToInteger(gvJournal.GetFocusedRowCellValue(colPartyLedgerId).ToString()) : 0; }
            set { partyLedgerId = value; }
        }

        //private DateTime SelectedVoucherDate;
        private DateTime dtSelectedVoucherDate
        {
            get
            {
                return gvJournal.GetFocusedRowCellValue(colDate) != null ? this.UtilityMember.DateSet.ToDate(gvJournal.GetFocusedRowCellValue(colDate).ToString(), false) : DateTime.MaxValue;
            }
        }

        //private DateTime dtTransLockDateFrom { get; set; }
        //private DateTime dtTransLockDateTo { get; set; }
        private DataTable dtAuditLockDetails = new DataTable();

        private int voucherdefinitionid = 0;
        private int VoucherDefinitionId
        {
            get { return gvJournal.GetFocusedRowCellValue(colVoucherDefinitionId) != null ? this.UtilityMember.NumberSet.ToInteger(gvJournal.GetFocusedRowCellValue(colVoucherDefinitionId).ToString()) : 0; }
            set { voucherdefinitionid = value; }
        }
        #endregion

        #region Variables
        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        public bool IsDateLoaded = false;
        #endregion

        public frmTransactionJournalView()
        {
            InitializeComponent();

            this.ribDuplicateVoucher.Buttons[0].ToolTip ="To make Replicate Voucher (Alt+U)";//To make Duplicate Voucher (Alt+U)";
          
        }

        public frmTransactionJournalView(string recVoucherDate, int proId, string pro, int frmTransactionIndex, int SelectionType)
            : this()
        {
            ProjectId = proId;
            ProjectName = pro;
            VoucherIndex = frmTransactionIndex;
            TransSelectionType = SelectionType;
            RecentVoucherDate = recVoucherDate;
        }

        private void ucJournal_AddClicked(object sender, EventArgs e)
        {
            ShowJournalTransactionForm((int)AddNewRow.NewRow);
        }

        private void frmTransactionJournalView_Load(object sender, EventArgs e)
        {
            
            //On 14/08/2024, Apply Project Currency Setting
            /*this.ApplyProjectCurrencySetting(ProjectId);
            rtxtAmount.Mask.Culture = System.Threading.Thread.CurrentThread.CurrentUICulture;
            rtxtAmount.Mask.UseMaskAsDisplayFormat = true;
            txtCredit.Mask.Culture = System.Threading.Thread.CurrentThread.CurrentUICulture;
            txtCredit.Mask.UseMaskAsDisplayFormat = true;
            txtDebit.Mask.Culture = System.Threading.Thread.CurrentThread.CurrentUICulture;
            txtDebit.Mask.UseMaskAsDisplayFormat = true;*/

            SetVisibileShortCuts(true, true, true);
            deTo.DateTime = deFrom.DateTime.AddMonths(1).AddDays(-1);
            //deTo.Properties.MaxValue = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            LoadProject();
            Setdefaults();
            glkpProject.EditValue = ProjectId;
            ApplyUserRights();
            LoadVoucherDetails();
            FetchDateDuration();
            this.AttachGridContextMenu(gcJournal);

            //On 09/09/2024, To show currency 
            colCurrencyName.Visible = colExchangeRate.Visible = colLiveExchangeRate.Visible =  false;
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                colCurrencyName.VisibleIndex = colLedgerName.VisibleIndex + 1;
                colExchangeRate.VisibleIndex = colCurrencyName.VisibleIndex + 1;
                colLiveExchangeRate.VisibleIndex = colExchangeRate.VisibleIndex + 1;
            }

            // if (TransSelectionType == 0) { ShowJournalTransactionForm((int)AddNewRow.NewRow); }
            //On 13/08/2024, Reset to Global Currency Setting
            //this.ApplyGlobalSetting();
        }

        private void deTo_Leave(object sender, EventArgs e)
        {
            if (deFrom.DateTime > deTo.DateTime)
            {
                //DateTime dateTo = deTo.DateTime;
                //deTo.DateTime = deFrom.DateTime;
                //deFrom.DateTime = dateTo.Date;
                deTo.DateTime = deFrom.DateTime;
            }
        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(Journal.CreateJournalVoucher);
            this.enumUserRights.Add(Journal.EditJournalVoucher);
            this.enumUserRights.Add(Journal.DeleteJournalVoucher);
            this.enumUserRights.Add(Journal.PrintJournalVoucher);
            this.enumUserRights.Add(Journal.ViewJournalVoucher);
            this.ApplyUserRights(ucJournalToolbar, this.enumUserRights, (int)Menus.Journal);
        }

        private void MoveTransaction()
        {
            try
            {
                if (!IsLockedTransaction(dtSelectedVoucherDate))
                {
                    int projectId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
                    frmMoveTransaction frmVoucherTrans = new frmMoveTransaction(projectId, VoucherId, LedgerId, MoveTransForm.Journal, VoucherDefinitionId);
                    frmVoucherTrans.UpdateHeld += new EventHandler(OnUpdateHeld);
                    frmVoucherTrans.ShowDialog();
                }
                else
                {
                    //this.ShowMessageBox("Voucher is locked.Cannot move voucher for the project '" + glkpProject.Text + "'");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Transaction.TRANS_JOURNAL_VIEW_TRANS_LOCKED_INFO) + glkpProject.Text + "'");
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void LoadVoucherDetails()
        {
            DataSet dsTransVoucherDetails = new DataSet();
            using (VoucherTransactionSystem voucherTransactionSystem = new VoucherTransactionSystem())
            {
                dsTransVoucherDetails = voucherTransactionSystem.LoadJournalVoucherDetails(ProjectId, deFrom.DateTime, deTo.DateTime);
                if (dsTransVoucherDetails.Tables.Count != 0)
                {
                    gcJournal.DataSource = dsTransVoucherDetails;
                    gcJournal.DataMember = "Master";
                    gcJournal.RefreshDataSource();
                }
                else
                {
                    gcJournal.DataSource = null;
                    gcJournal.RefreshDataSource();
                }
            }

            ShowHideColumnsBasedOnSetting();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvJournal.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvJournal, colDate);
            }
        }

        private void LoadProject()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    mappingSystem.ProjectClosedDate = deFrom.Text;
                    resultArgs = mappingSystem.FetchProjectsLookup();
                    glkpProject.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProject, resultArgs.DataSource.Table, mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                        // if (glkpProject.EditValue == null) { glkpProject.EditValue = glkpProject.Properties.GetKeyValue(0); }
                        //glkpProject.EditValue = (ProjectId != 0) ? ProjectId : glkpProject.Properties.GetKeyValue(0);

                        glkpProject.EditValue = (glkpProject.Properties.GetDisplayValueByKeyValue(ProjectId) != null ? ProjectId : glkpProject.Properties.GetKeyValue(0));                        

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

        private void DeleteVoucherDetails()
        {
            try
            {
                if (VoucherId != 0)
                {
                    using (VoucherTransactionSystem voucherTransactionSystem = new VoucherTransactionSystem())
                    {
                        if (gvJournal.RowCount != 0)
                        {
                            if (VoucherSubTypes != ledgerSubType.FD.ToString())
                            {
                                voucherTransactionSystem.VoucherId = VoucherId;
                                voucherTransactionSystem.tdsTransType = TDSTransType.TDSBooking;

                                // Commented by Praveen : 14 10 2016 : To Implement Delete option for TDS

                                //  int isExists = voucherTransactionSystem.CheckTDSBooking();
                                //  if (isExists == 0 || isExists > 0)
                                //  {
                                if (!IsLockedTransaction(dtSelectedVoucherDate))
                                {
                                    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        if (VoucherSubTypes != ledgerSubType.AST.ToString())
                                        {
                                            int IsExists = voucherTransactionSystem.IsExistVoucherJournalRefTrans();
                                            if (IsExists > 0)
                                            {
                                                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Master.Transaction.TRANS_JOURNAL_VIEW_DELETE_REFERERED_VOUCHER), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                                {
                                                    //resultArgs = voucherTransactionSystem.DeleteRefererdVoucher();
                                                    resultArgs = voucherTransactionSystem.DeleteRefererdVouchersByJournalVoucher();
                                                }
                                            }
                                            if (resultArgs.Success)
                                            {
                                                resultArgs = voucherTransactionSystem.DeleteVoucherTrans();
                                                if (resultArgs.Success)
                                                {
                                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                                    LoadVoucherDetails();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //this.ShowMessageBox("Entry can be deleted in the Fixed Asset Module.");
                                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Transaction.TRANS_JOURNAL_ENTRY_CAN_DELETED_FIXED_ASSET_MODULE));
                                        }
                                    }
                                }
                                else
                                {
                                    //this.ShowMessageBox("Voucher is locked.Cannot delete this voucher for the project '" + glkpProject.Text + "'.");
                                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Transaction.TRANS_JOURNAL_VOUCHER_LOCKED_INFO) + glkpProject.Text + "'");
                                        //" during the period " + this.UtilityMember.DateSet.ToDate(dtTransLockDateFrom.ToShortDateString()) +
                                        //" - " + this.UtilityMember.DateSet.ToDate(dtTransLockDateTo.ToShortDateString()));
                                }
                                //    }
                                //   else
                                //   {
                                //this.ShowMessageBox("TDS Payment is done for this Voucher.");
                                //  this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Transaction.TRANS_JOURNAL_TDS_JOURNAL_DONE_VOUCHER));
                                //   }
                            }
                            else
                            {
                                bool CanbeModified = false;
                                //bool rnt = false;
                                ////On 17/10/2022, to lock to FD Vouchers--------------------------------------------------------------------------------------
                                //this.ShowMessageBox("Entry can be deleted in the Fixed Deposit Module.");
                                ////---------------------------------------------------------------------------------------------------------------------------

                                //On 24/01/2023, To check FD voucher entry can be modified or not -----------------------------
                                using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                                {
                                    ResultArgs result = fdAccountSystem.IsAllowToModifyFDVoucherEntry(0, 0, VoucherId);
                                    CanbeModified = result.Success;
                                    if (!result.Success)
                                    {
                                        this.ShowMessageBoxWarning(result.Message);
                                    }
                                }
                                //---------------------------------------------------------------------------------------------

                                if (CanbeModified)
                                {
                                    if (!IsLockedTransaction(dtSelectedVoucherDate))
                                    {
                                        using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                                        {
                                            //fdAccountSystem.VoucherId = VoucherId;
                                            fdAccountSystem.VoucherId = fdAccountSystem.FDVoucherId = voucherId;
                                            resultArgs = fdAccountSystem.FetchFDAccountId();
                                            if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0)
                                            {
                                                if (this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_STATUSColumn.ColumnName].ToString()) != 0 && resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName].ToString() == FDRenewalTypes.ACI.ToString())
                                                {
                                                    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                                    {
                                                        voucherTransactionSystem.VoucherId = VoucherId;
                                                        resultArgs = voucherTransactionSystem.DeleteVoucherTrans();
                                                        if (resultArgs.Success)
                                                        {
                                                            resultArgs = fdAccountSystem.DeleteFDRenewalsByVoucherId();
                                                            if (resultArgs.Success)
                                                            {
                                                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                                                LoadVoucherDetails();
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.JOURNAL_CANNOT_DELETE));
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Transaction.TRANS_JOURNAL_VOUCHER_LOCKED_INFO) + glkpProject.Text + "'");
                                        //" during the period " + this.UtilityMember.DateSet.ToDate(dtTransLockDateFrom.ToShortDateString()) +
                                        //" - " + this.UtilityMember.DateSet.ToDate(dtTransLockDateTo.ToShortDateString()));
                                    }
                                }
                            }
                        }
                        else
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
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

        private void ShowJournalTransactionForm(int vouId, bool DuplicateVoucher=false)
        {
            try
            {
                DateTime dtyearfrom = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
                DateTime dtbookbeginfrom = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false);
                DateTime dtRecentVoucher = UtilityMember.DateSet.ToDate(this.AppSetting.RecentVoucherDate, false);
                DateTime dtRecentVoucherDate = (!string.IsNullOrEmpty(this.AppSetting.RecentVoucherDate)) ? this.UtilityMember.DateSet.ToDate(this.AppSetting.RecentVoucherDate, false) : dtbookbeginfrom > dtyearfrom ? dtbookbeginfrom : dtyearfrom;
                DateTime dtTransDate = vouId == 0 ? deFrom.DateTime : dtSelectedVoucherDate;
                
                //01/03/2018, since recent date is taken from db by defualt, if that date is locked, we can show voucher etnry form
                //so we open voucher form here evern date is locked, it will be validated in entry form
                bool voucherlocked = IsLockedTransaction(dtTransDate);
                if (vouId == 0 || !voucherlocked)
                {
                    int ProId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
                    string Pro = glkpProject.Text.ToString();

                    //On 04/02/2019, show list of voucher types ---------------------------------------------------------------------------------------------
                    //this list will be shown only when more than one voucher type exists except base vouchers for selected project
                    Int32 voucherdefinitionid = (Int32)DefaultVoucherTypes.Journal; //by default 

                    if (vouId == 0)
                    {
                        string basevouchers = ((Int32)DefaultVoucherTypes.Journal).ToString();
                        ResultArgs result = this.ShowVoucherTypeSelection(ProId, basevouchers, voucherdefinitionid);
                        if (result.Success && result.ReturnValue != null)
                        {
                            string[] VoucherTypeSelected = result.ReturnValue as string[];
                            voucherdefinitionid = UtilityMember.NumberSet.ToInteger(VoucherTypeSelected[0]);
                            Int32 baseVoucherType = UtilityMember.NumberSet.ToInteger(VoucherTypeSelected[1]);
                        }
                    }
                    else
                        voucherdefinitionid = VoucherDefinitionId;
                    //----------------------------------------------------------------------------------------------------------------------------------------------

                    JournalAdd frmTransaction = new JournalAdd(dtRecentVoucherDate.ToString(), ProId, Pro, vouId, VoucherIndex, voucherdefinitionid, DuplicateVoucher);
                    frmTransaction.BookingId = vouId > 0 ? TDSBookingId : vouId;
                    // frmTransaction.ExpenseLedgerId = vouId > 0 ? ExpenseLedgerId : vouId;
                    frmTransaction.DeducteeTypeLedgerId = vouId > 0 ? DeducteeId : vouId;
                    frmTransaction.TDSPartyLedgerId = vouId > 0 ? PartyLedgerId : vouId;
                    frmTransaction.UpdateHeld += new EventHandler(OnUpdateHeld);
                    frmTransaction.ShowDialog();
                }
                else if (voucherlocked)
                {
                    //this.ShowMessageBox("Voucher is locked.Cannot make voucher entry for the project '" + glkpProject.Text + "'");
                    if (vouId == 0)
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED) + "'" + glkpProject.Text + "'");
                                //" during the period " + this.UtilityMember.DateSet.ToDate(dtTransLockDateFrom.ToShortDateString()) +
                                //" - " + this.UtilityMember.DateSet.ToDate(dtTransLockDateTo.ToShortDateString()));
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED_CANNOT_EDIT) + "'" + glkpProject.Text + "'");
                                //" during the period " + this.UtilityMember.DateSet.ToDate(dtTransLockDateFrom.ToShortDateString()) +
                                //" - " + this.UtilityMember.DateSet.ToDate(dtTransLockDateTo.ToShortDateString()));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            //On 14/08/2024, Apply Project Currency Setting
            /*.ApplyProjectCurrencySetting(ProjectId);
            Int32  num = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalDigits;
            colAmount.DisplayFormat.FormatType = FormatType.Numeric;
            colAmount.DisplayFormat.FormatString = "n";*/
                        
            ProjectName = (glkpProject.EditValue != null ? glkpProject.Text : string.Empty);
            LoadVoucherDetails();
            FetchDateDuration();

            //On 13/08/2024, Reset to Global Currency Setting
            //this.ApplyGlobalSetting();
        }

        private void ucJournal_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadVoucherDetails();
            gvJournal.FocusedRowHandle = RowIndex;
        }

        private void ucJournalToolbar_AddClicked(object sender, EventArgs e)
        {
            ShowJournalTransactionForm((int)AddNewRow.NewRow);
        }

        private void ucJournalToolbar_EditClicked(object sender, EventArgs e)
        {
            try
            {
                if (this.isEditable)
                {
                    if (VoucherId != 0)
                    {
                        if (gvJournal.RowCount != 0)
                        {
                            if (VoucherSubTypes == ledgerSubType.GN.ToString() || VoucherSubTypes == ledgerSubType.PAY.ToString() || VoucherSubTypes == ledgerSubType.AST.ToString()) // Payroll Vouchers
                            {
                                if (VoucherSubTypes != ledgerSubType.AST.ToString())
                                {
                                    ShowJournalTransactionForm(VoucherId);
                                }
                                else
                                {   

                                    //Dinesh 03/07/2025 
                                    //this.ShowMessageBox("Entry can be edited in the Fixed Asset Module.");

                                    //Implemenation
                                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Transaction.ENRTY_CAN_BE_EDITED_IN_THE_FIXED_ASSTE_MODULE));
                                }
                            }
                            else
                            {
                                //using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                                //{
                                //    fdAccountSystem.VoucherId = voucherId;
                                //    resultArgs = fdAccountSystem.FetchFDAccountId();
                                //    if (this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_STATUSColumn.ColumnName].ToString()) != 0 &&
                                //        resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName].ToString() == FDRenewalTypes.ACI.ToString())
                                ShowFDAccountForm();
                                //    else
                                //        this.ShowMessageBox("Cannot edit,This is closed Journal Voucher Entry");
                                //}
                            }
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

        private void ucJournalToolbar_PrintClicked(object sender, EventArgs e)
        {
            //PrintGridViewDetails(gcJournal, "Transaction Journal", PrintType.DT, gvJournal,true);
            PrintGridViewDetails(gcJournal, "Transaction Journal", PrintType.DS, gvJournal, true);
        }

        private void ucJournalToolbar_RefreshClicked(object sender, EventArgs e)
        {
            LoadProject();
            LoadVoucherDetails();
            FetchDateDuration();
        }

        private void Setdefaults()
        {
            deFrom.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deFrom.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deFrom.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            deTo.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deTo.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            deFrom.DateTime = (!string.IsNullOrEmpty(RecentVoucherDate)) ? UtilityMember.DateSet.ToDate(RecentVoucherDate, false) : UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deTo.DateTime = deFrom.DateTime.AddMonths(1).AddDays(-1);
        }

        private void ucJournalToolbar_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucJournalToolbar_DeleteClicked(object sender, EventArgs e)
        {
            //if (VoucherSubTypes == ledgerSubType.FD.ToString())
            //{
            //    this.ShowMessageBox("This is Fixed Deposit Ledger,No provision to delete this voucher");
            //}
            //else
            //{
            DeleteVoucherDetails();
            // }
        }

        private void gvJournal_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (this.isEditable)
                {
                    if (VoucherId != 0)
                    {
                        if (gvJournal.RowCount != 0)
                        {
                            if (VoucherSubTypes == ledgerSubType.GN.ToString() || VoucherSubTypes == ledgerSubType.PAY.ToString() || VoucherSubTypes == ledgerSubType.AST.ToString())
                            {
                                if (VoucherSubTypes != ledgerSubType.AST.ToString())
                                {
                                    ShowJournalTransactionForm(VoucherId);
                                }
                                else
                                {
                                    //Dinesh 03/07/2025 
                                   // this.ShowMessageBox("Entry can be edited in the Fixed Asset Module.");

                                    //Implemation 
                                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Transaction.ENRTY_CAN_BE_EDITED_IN_THE_FIXED_ASSTE_MODULE));
                                }
                            }
                            else
                            {
                                //using (FDAccountSystem fdAccount = new FDAccountSystem())
                                //{
                                //    fdAccount.VoucherId = VoucherId;
                                //    resultArgs = fdAccount.FetchFDAccountId();
                                //    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                                //    {
                                //        if (this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][fdAccount.AppSchema.FDAccount.FD_STATUSColumn.ColumnName].ToString()) != 0 &&
                                //        resultArgs.DataSource.Table.Rows[0][fdAccount.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName].ToString() == FDRenewalTypes.ACI.ToString())
                                ShowFDAccountForm();
                                //    else
                                //        this.ShowMessageBox("Cannot edit,This is closed Journal Voucher Entry");
                                //}
                                // }
                            }
                        }
                        else
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

        private void gvJournalLedger_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (VoucherId != 0)
                {
                    if (gvJournal.RowCount != 0)
                    {
                        if (VoucherSubTypes == ledgerSubType.GN.ToString() || VoucherSubTypes == ledgerSubType.PAY.ToString())
                        {

                            if (VoucherSubTypes != ledgerSubType.AST.ToString())
                            {
                                ShowJournalTransactionForm(VoucherId);
                            }
                            else
                            {   
                                //Dinesh 03/07/2025
                                //this.ShowMessageBox("Entry can be edited in the Fixed Asset Module.");

                                //Implemenation
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Transaction.ENRTY_CAN_BE_EDITED_IN_THE_FIXED_ASSTE_MODULE));
                            }
                        }
                        else
                        {
                            ShowFDAccountForm();
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
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }

        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            //ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
            //LoadVoucherDetails();
            ProjectId = (glkpProject.EditValue != null) ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
        }

        private void gvJournal_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvJournal.RowCount.ToString();
        }

        private void deFrom_Leave(object sender, EventArgs e)
        {
            if (IsDateLoaded)
            {
                deTo.DateTime = deFrom.DateTime.AddMonths(1).AddDays(-1);
                IsDateLoaded = true;
            }
            if (deFrom.DateTime > deTo.DateTime)
            {
                //DateTime dateTo = deTo.DateTime;
                //deTo.DateTime = deFrom.DateTime;
                //deFrom.DateTime = dateTo.Date;
                deTo.DateTime = deFrom.DateTime;
            }
        }

        private void rbtnPrint_Click(object sender, EventArgs e)
        {
            if (gvJournal != null)
                PrintVoucher(VoucherId);
        }

        private void PrintVoucher(int vid)
        {
            if (!IsLockedTransaction(dtSelectedVoucherDate))
            {
                //if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.CONFIRM_PRINT_VOUCHER), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes && vid != 0)
                //{
                ProjectName = (glkpProject.EditValue != null ? glkpProject.Text : string.Empty);
                Bosco.Report.Base.IReport report = new Bosco.Report.Base.ReportEntry(this.MdiParent);
                using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
                {
                    string jounralrpt = UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.JOURNALVOUCHER).ToString();
                    resultArgs = voucherSystem.FetchReportSetting(jounralrpt);
                    if (resultArgs != null && resultArgs.Success)
                    {
                        ReportProperty.Current.VoucherPrintSettingInfo = resultArgs.DataSource.TableView;
                        ReportProperty.Current.CashBankVoucherDateFrom = ReportProperty.Current.CashBankVoucherDateTo = dtSelectedVoucherDate;
                        report.VoucherPrint(vid.ToString(), UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.JOURNALVOUCHER), ProjectName, ProjectId);
                    }
                    else
                    {
                        this.ShowMessageBoxError(resultArgs.Message);
                    }
                }
            }
            else
            {   //Dinesh 03/07/2025 
                 //this.ShowMessageBox("Voucher is locked.Cannot print voucher for the project '" + glkpProject.Text + "'");

                //Implemantaion 
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Transaction.VOUCHER_IS_LOCKED_CANNOT_PRINT_VOUCHER_FOR_THE_PROJECT) + "'" + glkpProject.Text + "'");
              
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.F3))
            {
                //  deFrom.Focus();

                frmDatePicker datePicker = new frmDatePicker(deFrom.DateTime, deTo.DateTime, DatePickerType.ChangePeriod);
                datePicker.ShowDialog();
                deFrom.DateTime = AppSetting.VoucherDateFrom;
                deTo.DateTime = AppSetting.VoucherDateTo;
            }
            if (KeyData == (Keys.Control | Keys.P))
            {
                if (gvJournal != null)
                    PrintVoucher(VoucherId);
            }
            else if (KeyData == (Keys.Alt | Keys.U))
            {
                MakeDuplicationVoucher();
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        private void frmTransactionJournalView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void ShowFDAccountForm()
        {
            try
            {
                bool CanbeModified = false;
                //bool rnt = false;
                ////On 17/10/2022, to lock to FD Vouchers--------------------------------------------------------------------------------------
                //this.ShowMessageBox("Entry can be edited in the Fixed Deposit Module.");
                ////---------------------------------------------------------------------------------------------------------------------------

                //On 24/01/2023, To check FD voucher entry can be modified or not -----------------------------
                using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                {
                    ResultArgs result = fdAccountSystem.IsAllowToModifyFDVoucherEntry(0, 0, VoucherId);
                    CanbeModified = result.Success;
                    if (!result.Success)
                    {
                        this.ShowMessageBoxWarning(result.Message);
                    }
                }
                //---------------------------------------------------------------------------------------------


                if (CanbeModified)
                {
                    bool voucherlocked = IsLockedTransaction(dtSelectedVoucherDate);
                    if (!voucherlocked)
                    {
                        using (FDAccountSystem fdAccount = new FDAccountSystem())
                        {
                            fdAccount.VoucherId = VoucherId;
                            string fdtype = string.Empty;
                            resultArgs = fdAccount.FetchFDAccountId();
                            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                            {
                                if (this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][fdAccount.AppSchema.FDAccount.FD_STATUSColumn.ColumnName].ToString()) != 0 &&
                                resultArgs.DataSource.Table.Rows[0][fdAccount.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName].ToString() == FDRenewalTypes.ACI.ToString())
                                {
                                    fdtype = resultArgs.DataSource.Table.Rows[0][fdAccount.AppSchema.FDRenewal.FD_TYPEColumn.ColumnName].ToString();
                                    if (fdtype == FDTypes.POI.ToString())
                                    {
                                        ACPP.Modules.Master.frmFDAccount frmAccount = new Master.frmFDAccount(FDAccountID, VoucherId, FDTypes.POI);
                                        frmAccount.PostInterestCreatedDate = dtSelectedVoucherDate;
                                        frmAccount.ProjectId = ProjectId;
                                        frmAccount.ShowDialog();
                                    }
                                    else
                                    {
                                        ACPP.Modules.Master.frmFDAccount frmAccount = new Master.frmFDAccount(FDAccountID, VoucherId, FDTypes.RN);
                                        frmAccount.ProjectId = ProjectId;
                                        frmAccount.ShowDialog();
                                    }
                                    LoadVoucherDetails();
                                }
                                else
                                {
                                    if (!chkShowFilter.Checked)
                                    {
                                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.JOURNAL_CANNOT_EDIT));
                                    }
                                }
                            }
                        }

                        //if (FDStaus != 0)
                        //{
                        //    ACPP.Modules.Master.frmFDAccount frmAccount = new Master.frmFDAccount(FDAccountID, VoucherId, FDTypes.RN);
                        //    frmAccount.ProjectId = ProjectId;
                        //    frmAccount.ShowDialog();
                        //}
                        //else
                        //{
                        //    this.ShowMessageBox("This is closed Fixed Deposit voucher ,No provision to edit / delete this voucher ");
                        //}
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED_CANNOT_EDIT) + "'" + glkpProject.Text + "'");
                        //" during the period " + this.UtilityMember.DateSet.ToDate(dtTransLockDateFrom.ToShortDateString()) +
                        //" - " + this.UtilityMember.DateSet.ToDate(dtTransLockDateTo.ToShortDateString()));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void frmTransactionJournalView_EnterClicked(object sender, EventArgs e)
        {
            try
            {
                if (VoucherId != 0)
                {
                    if (gvJournal.RowCount != 0)
                    {
                        if (VoucherSubTypes == ledgerSubType.GN.ToString() || VoucherSubTypes == ledgerSubType.PAY.ToString())
                        {
                            if (VoucherSubTypes != ledgerSubType.AST.ToString())
                            {
                                ShowJournalTransactionForm(VoucherId);
                            }
                            else
                            {


                                //Dinesh 03/07/2025
                               // this.ShowMessageBox("Entry can be edited in the Fixed Asset Module.");

                                // Implementation
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Transaction.ENRTY_CAN_BE_EDITED_IN_THE_FIXED_ASSTE_MODULE));

                            }
                        }
                        else
                        {
                            ShowFDAccountForm();
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
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }

        private void ucJournalToolbar_MoveTransaction(object sender, EventArgs e)
        {
            MoveTransaction();
        }

        private void gvJournal_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (gvJournal.RowCount > 0)
                {
                    //if (dtTransLockDateFrom != null && dtTransLockDateTo != null && (!dtTransLockDateFrom.Equals(DateTime.MinValue)) && (!dtTransLockDateTo.Equals(DateTime.MinValue)))
                    if (dtAuditLockDetails != null && dtAuditLockDetails.Rows.Count > 0 && colLockTrans.Visible)
                    {
                        DateTime dtTransDate = gvJournal.GetRowCellValue(e.RowHandle, colDate) != null ? this.UtilityMember.DateSet.ToDate(gvJournal.GetRowCellValue(e.RowHandle, colDate).ToString(), false) : DateTime.MinValue;
                        if (!dtTransDate.Equals(DateTime.MinValue))
                        {
                            //Check temporary relaxation
                            bool isEnforceTmpRelaxation = this.AppSetting.IsTemporaryGraceLockRelaxDate(dtTransDate);
                            
                            //dtAuditLockDetails.DefaultView.RowFilter = "('" + UtilityMember.DateSet.ToDate(dtTransDate.ToShortDateString()) + "'>= DATE_FROM) AND " +
                            //                                              "('" + UtilityMember.DateSet.ToDate(dtTransDate.ToShortDateString()) + "'<= DATE_TO)";
                            dtAuditLockDetails.DefaultView.RowFilter =  "('" + UtilityMember.DateSet.ToDate(dtTransDate.ToShortDateString()) + "'>= DATE_FROM) AND " +
                                                                          "('" + UtilityMember.DateSet.ToDate(dtTransDate.ToShortDateString()) + "'< DATE_TO)";

                            if (dtAuditLockDetails.DefaultView.Count > 0 && !isEnforceTmpRelaxation)
                            {
                                rbtnTransLock.Buttons[0].Image = imgJournalView.Images[0];
                                e.Handled = false;
                            }
                            else
                            {
                                //20/09/2021, to disable unlock cell
                                rbtnTransLock.Buttons[0].Image = null; //imgJournalView.Images[4];
                            }
                            //colLockTrans.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
                        }
                        colNarration.Resize(colNarration.Width);
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
                    AuditSystem.DateFrom = deFrom.DateTime;
                    AuditSystem.DateTo = deTo.DateTime;
                    resultArgs = AuditSystem.FetchAuditDetailByProjectDateRange();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        dtAuditLockDetails = resultArgs.DataSource.Table;
                        colLockTrans.Visible = true;
                        //dtTransLockDateFrom = this.UtilityMember.DateSet.ToDate(dtAuditLockDetails.Rows[0][AuditSystem.AppSchema.AuditLockTransType.DATE_FROMColumn.ColumnName].ToString(), false);
                        //dtTransLockDateTo = this.UtilityMember.DateSet.ToDate(dtAuditLockDetails.Rows[0][AuditSystem.AppSchema.AuditLockTransType.DATE_TOColumn.ColumnName].ToString(), false);
                    }
                    else
                    {
                        dtAuditLockDetails = null;
                        colLockTrans.Visible = false;
                        //dtTransLockDateFrom = dtTransLockDateTo = DateTime.MinValue;
                    }


                    //On 07/02/2024, For SDBINM, Lock Voucehrs before grace period
                    if (dtAuditLockDetails == null && this.AppSetting.IS_SDB_INM && this.AppSetting.VoucherGraceDays > 0)
                    {
                        dtAuditLockDetails = AuditSystem.AppSchema.AuditLockTransType.DefaultView.ToTable();
                        DataRow dr = dtAuditLockDetails.NewRow();
                        dr[AuditSystem.AppSchema.AuditLockTransType.DATE_FROMColumn.ColumnName] = this.AppSetting.GraceLockDateFrom;
                        dr[AuditSystem.AppSchema.AuditLockTransType.DATE_TOColumn.ColumnName] = this.AppSetting.GraceLockDateTo;
                        dtAuditLockDetails.Rows.Add(dr);

                        //bool b = (startTime1 <= endTime2 && startTime2 <= endTime1;);
                        if ((deFrom.DateTime <= this.AppSetting.GraceLockDateTo || deTo.DateTime <= this.AppSetting.GraceLockDateFrom))
                        {
                            colLockTrans.Visible = true;
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

        private bool IsLockedTransaction(DateTime dtVoucherDate)
        {
            bool isSuccess = false;
            try
            {
                //if (dtTransLockDateFrom != DateTime.MinValue && dtTransLockDateTo != DateTime.MinValue)
                if (dtAuditLockDetails != null && dtAuditLockDetails.Rows.Count > 0)
                {
                    //Check temporary relaxation
                    bool isEnforceTmpRelaxation = this.AppSetting.IsTemporaryGraceLockRelaxDate(dtVoucherDate);

                    //dtAuditLockDetails.DefaultView.RowFilter = "('" + UtilityMember.DateSet.ToDate(dtVoucherDate.ToShortDateString()) + "'>= DATE_FROM) AND " +
                    //                    "('" + UtilityMember.DateSet.ToDate(dtVoucherDate.ToShortDateString()) + "'<= DATE_TO)";
                    dtAuditLockDetails.DefaultView.RowFilter = "('" + UtilityMember.DateSet.ToDate(dtVoucherDate.ToShortDateString()) + "'>= DATE_FROM) AND " +
                                        "('" + UtilityMember.DateSet.ToDate(dtVoucherDate.ToShortDateString()) + "'< DATE_TO)";
                                        
                    if (dtAuditLockDetails.DefaultView.Count > 0 && !isEnforceTmpRelaxation)
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

        private void deFrom_EditValueChanged(object sender, EventArgs e)
        {
            //On 12/07/2018, For closed Projects----
            LoadProject();
            //--------------------------------------
        }

        private void ribDuplicateVoucher_Click(object sender, EventArgs e)
        {
            MakeDuplicationVoucher();
        }

        private void MakeDuplicationVoucher()
        {
            if (gvJournal != null)
            {
                if (VoucherSubTypes == ledgerSubType.GN.ToString())
                {
                    if (this.ShowConfirmationMessage("Do you want to Replicate Voucher Entry ?",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        ShowJournalTransactionForm(VoucherId, true);
                    }
                }
                else
                    //Dinesh 03/07/2025
                    //this.ShowMessageBox("Selected Voucher is Fixed Deposit Voucher, Only Journal Voucher alone can be duplicated");

                    //Implementaion
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Transaction.SELECTED_VOUCHER_IS_FIXED_DEPOSIT_VOUCHER_ONLY_JOURNAL_VOUCHER_ALONE_CAN_BE_DUPLICATED));
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

        private void rbtnGSTInvoicePrint_Click(object sender, EventArgs e)
        {
            PrintGSTInvoice(VoucherId);
        }

        private void PrintGSTInvoice(int vid)
        {
            //On 01/02/2018, to show contra voucher also, Treat Journal Voucher as Contra Voucher
            //Treat Journal Voucher as Contra Voucher
            if (this.LoginUser.IsFullRightsReservedUser)
            {
                if (!IsLockedTransaction(dtSelectedVoucherDate))
                {
                    string vendorgst = gvJournal.GetFocusedRowCellValue(colVendorGSTInvoice) != null ? gvJournal.GetFocusedRowCellValue(colVendorGSTInvoice).ToString() : string.Empty;

                    if (!string.IsNullOrEmpty(vendorgst))
                    {
                        using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
                        {
                            string rptVoucher = string.Empty;
                            Bosco.Report.Base.IReport report = new Bosco.Report.Base.ReportEntry(this.MdiParent);
                            rptVoucher = UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.JOURNALVOUCHER);
                            resultArgs = voucherSystem.FetchReportSetting(rptVoucher);
                            if (resultArgs != null && resultArgs.Success)
                            {
                                ReportProperty.Current.VoucherPrintSettingInfo = resultArgs.DataSource.TableView;
                                ReportProperty.Current.CashBankVoucherDateFrom = ReportProperty.Current.CashBankVoucherDateTo = dtSelectedVoucherDate;
                                if (this.AppSetting.AllowMultiCurrency==1 || this.AppSetting.IsCountryOtherThanIndia)
                                    report.VoucherPrint(vid.ToString(), UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.INVOICE_JOURNAL), ProjectName, ProjectId);
                                else
                                    report.VoucherPrint(vid.ToString(), UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.GSTINVOICE_JOURNAL), ProjectName, ProjectId);
                            }
                            else
                            {
                                this.ShowMessageBoxError(resultArgs.Message);
                            }
                        }
                    }
                    else
                    {

                        //Dinesh 03/07/2025

                        //this.ShowMessageBox("GST Invoice details is not available");
                        //Implemenation
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.GST.GST_INVOICE_DETAILS_IS_NOT_AVAILABLE));

                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED_CANNOT_PRINT) + glkpProject.Text + "'");
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.NO_RIGHTS_TO_TAKE_PRINTOUT));
            }

        }

        private void ShowHideColumnsBasedOnSetting()
        {
            GridColumn lastVisibleColumn = null;

            //To find last visible column except those are not button coulmns and customize columns (setting based columns)
            for (Int32 i = gvJournal.VisibleColumns.Count - 1; i >= 0; i--)
            {
                lastVisibleColumn = gvJournal.VisibleColumns[i];

                if (lastVisibleColumn.ColumnEdit == null && 
                    lastVisibleColumn != colAuthorization && lastVisibleColumn != colVendorGSTInvoice)
                {
                    break;
                }
            }

            if (lastVisibleColumn != null)
            {
                //14/11/2022, to show print GST invoice details 
                colVendorGSTInvoice.Visible = colPrintGSTInvoice.Visible = colAuthorization.Visible = false;

                //For Vendor GST details column
                if ( (this.AppSetting.EnableGST == "1" || (this.AppSetting.IsCountryOtherThanIndia || this.AppSetting.AllowMultiCurrency == 1))
                    && this.AppSetting.IncludeGSTVendorInvoiceDetails == "2")
                {
                    colVendorGSTInvoice.Visible = true;
                    colVendorGSTInvoice.VisibleIndex = lastVisibleColumn.VisibleIndex + 1;//colNameAddress.VisibleIndex + 1;
                }
                
                //For Authorization Status column
                if (this.AppSetting.ConfirmAuthorizationVoucherEntry == 1)
                {
                    colAuthorization.Visible = true;
                    if (colVendorGSTInvoice.Visible)
                        colAuthorization.VisibleIndex = colVendorGSTInvoice.VisibleIndex + 1;
                    else
                        colAuthorization.VisibleIndex = lastVisibleColumn.VisibleIndex + 1; // colNameAddress.VisibleIndex + 1;
                }

                //For Print GST Invoice button
                if ( (this.AppSetting.EnableGST == "1" || (this.AppSetting.IsCountryOtherThanIndia || this.AppSetting.AllowMultiCurrency == 1))
                    && this.AppSetting.IncludeGSTVendorInvoiceDetails == "2")
                {
                    colPrintGSTInvoice.Visible = true;
                    if (colAuthorization.Visible)
                        colPrintGSTInvoice.VisibleIndex = colAuthorization.VisibleIndex + 1;
                    else if (colVendorGSTInvoice.Visible)
                        colPrintGSTInvoice.VisibleIndex = colVendorGSTInvoice.VisibleIndex + 1;
                    else
                        colPrintGSTInvoice.VisibleIndex = lastVisibleColumn.VisibleIndex + 1;// colNameAddress.VisibleIndex + 1;
                }
                colLockTrans.VisibleIndex = colDuplicateVoucher.VisibleIndex + 1;
            }
            
        }

        private void gcJournal_Click(object sender, EventArgs e)
        {

        }
    }
}