using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model.UIModel;
using Bosco.Model;
using Bosco.Model.Inventory.Asset;
using Bosco.Utility;
using DevExpress.XtraGrid;
using Bosco.Model.Transaction;
using ACPP.Modules.Master;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmAMCRenewAdd : frmFinanceBaseAdd
    {
        #region VariableDeclaration
        ResultArgs resultArgs = new ResultArgs();
        const string SELECT = "SELECT";
        DataTable dtRetriveData = new DataTable();
        public event EventHandler UpdateHeld;
        AssetAmc AmcData = new AssetAmc();
        private DateTime dtAMCRenewDate = DateTime.Now;
        public DataTable dtAmcRenewal = new DataTable();
        public DialogResult Dialogresult = DialogResult.Cancel;
        #endregion

        #region Property
        private int AMCId = 0;
        //public int Projectid { get; set; }
        private int AmcRenewalId = 0;
        private int ItemDetailId = 0;
        private int Mode = 0;
        private int RowNo = 0;

        private double BankTransSummaryVal
        {
            get { return ucAssetJournal1.BankTransSummaryVal; }
        }

        public int VoucherId { get; set; }
        private Bosco.DAO.Schema.AppSchemaSet.ApplicationSchemaSet appSchema = null;
        private Bosco.DAO.Schema.AppSchemaSet.ApplicationSchemaSet AppSchemas
        {
            get { return appSchema = new Bosco.DAO.Schema.AppSchemaSet().AppSchema; }
        }

        public Dictionary<int, DataTable> AssetAmcCollection = new Dictionary<int, DataTable>();

        public int ProjectId { get; set; }
        private int ExpenseEledgerid { get; set; }
        private int CashBankLedgerId
        {
            get;
            set;
        }
        private int voucherid { get; set; }
        int groupId = 0;
        private int CashBankGroupId
        {
            get
            {
                //DataRowView dr = glkpCashBankLedger.GetSelectedDataRow() as DataRowView;
                //groupId = this.UtilityMember.NumberSet.ToInteger(dr["GROUP_ID"].ToString());
                return groupId;
            }
            set { groupId = value; }
        }
        #endregion

        #region Constructors
        public frmAMCRenewAdd()
        {
            //       ProjectId = ProjectId;
            InitializeComponent();
        }

        public frmAMCRenewAdd(int AmcId, int Mode, int RenewalId)
            : this()
        {
            AMCId = AmcId;
            this.Mode = Mode;
            this.AmcRenewalId = RenewalId;
        }

        public frmAMCRenewAdd(int AmcId, AssetAmc amc, int rowNO)
            : this()
        {
            AMCId = AmcId;
            AmcData = amc;
            RowNo = rowNO;
        }
        #endregion

        #region Events
        /// <summary>
        /// Load the Amc Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAMCRenewAdd_Load(object sender, EventArgs e)
        {
            SetDefaults();
            //SetDateDuration();
            LoadProject();
            //  LoadExpenseLedgers();
            // LoadCashBankLedgers();
            //LoadAssetItemDetails();
            AssignValueToControls();
            LoadDefaults();
            //  ucAssetJournal1.ProjectId = 3;
            //  ucAssetJournal1.Flag = AssetInOut.AMC;
            // ucAssetJournal1.LoadLedger();
            // ucAssetJournal1.NextFocusControl = btnSave;
            //  ucAssetJournal1.EnableCashBankGrid = false;
            glkpProject.EditValue = this.ProjectId;

            FinanceIntegrationDefaults();
            LoadNarrationAutoComplete();
            SetVoucherDate();
            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                ApplyRights();
            }
            else
            {
                glkpProject.Properties.Buttons[1].Visible = true;
            }
        }

        /// <summary>
        /// Save the Amc Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidateAMC())
                {
                    if (!AmcData.Equals(AssetAmc.AmcRenew))
                    {
                        using (AssetAMCRenewalSystem renewalSystem = new AssetAMCRenewalSystem())
                        {
                            renewalSystem.VoucherDate = this.UtilityMember.DateSet.ToDate(deVoucherDate.Text, false);
                            renewalSystem.VoucherNo = lblEVNo.Text;
                            renewalSystem.Provider = txtProvider.Text;
                            renewalSystem.Narration = txtNarration.Text;
                            renewalSystem.ProjectId = ProjectId;
                            renewalSystem.Flag = AssetInOut.AMC.ToString();
                            renewalSystem.VoucherId = VoucherId;
                            resultArgs = renewalSystem.SaveFinanceVoucher(ucAssetJournal1.DtCashBank);

                            renewalSystem.AMCId = AMCId;
                            renewalSystem.AmcRenewalId = AmcRenewalId;
                            renewalSystem.AMCGroup = txtAssetGroupName.Text.Trim();
                            renewalSystem.Provider = txtProvider.Text.Trim();
                            renewalSystem.ProjectId = ProjectId;
                            renewalSystem.PremiumAmount = this.UtilityMember.NumberSet.ToDecimal(txtPremiumAmount.Text.Trim());
                            renewalSystem.AmcFrom = this.UtilityMember.DateSet.ToDate(dtAMCFrom.Text, false);
                            renewalSystem.AmcTo = this.UtilityMember.DateSet.ToDate(dtAMCTo.Text.Trim(), false);
                            renewalSystem.RenewalDate = this.UtilityMember.DateSet.ToDate(deRenewalDate.Text.Trim(), false);
                            renewalSystem.VoucherDate = deVoucherDate.DateTime;
                            dtRetriveData = gcSelectedItemList.DataSource as DataTable;
                            if (dtRetriveData != null && dtRetriveData.Rows.Count > 0)
                            {
                                renewalSystem.dtAmcDetails = dtRetriveData;
                                renewalSystem.Mode = Mode;
                                renewalSystem.ChequeNo = string.Empty;
                                if (voucherid > 0)
                                {
                                    renewalSystem.VoucherId = voucherid;
                                }

                                resultArgs = renewalSystem.SaveAMC();
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                                }

                                ClearControls();
                                AMCId = renewalSystem.AMCId;
                                LoadDefaults();
                                LoadVoucherNo();
                                if (UpdateHeld != null)
                                {
                                    UpdateHeld(this, e);
                                }
                                else
                                {
                                    txtAssetGroupName.Focus();
                                }
                            }
                            else
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AMCVoucher.AMC_NO_MAPPED_ITEMS));
                            }

                        }
                    }
                    if (AmcData.Equals(AssetAmc.Edit))
                    {

                    }
                }
            }
            catch (Exception ex)
            {

                this.ShowMessageBox(ex.ToString());
            }
        }

        /// <summary>
        /// Calculate the Values
        /// </summary>
        private void CalculateFirstRowValue()
        {
            try
            {
                double LedgerAmount = this.UtilityMember.NumberSet.ToDouble(txtPremiumAmount.Text);
                if (LedgerAmount >= 0 && LedgerAmount != BankTransSummaryVal && VoucherId >= 0)
                {
                    //  ucAssetJournal1.EnableCashBankGrid = true;
                    ucAssetJournal1.Focus();
                    double Amount = ucAssetJournal1.BankAmount;//asset.le
                    if (Amount >= 0)
                    {
                        double dAmount = 0.0;
                        if (BankTransSummaryVal <= LedgerAmount)
                        {
                            dAmount = Math.Abs((LedgerAmount - BankTransSummaryVal) + Amount);
                        }
                        else if (BankTransSummaryVal >= LedgerAmount)
                        {
                            dAmount = Math.Abs(Amount - (BankTransSummaryVal - LedgerAmount));
                        }
                        if (dAmount >= 0)
                        {
                            ucAssetJournal1.Flag = AssetInOut.AMC;
                            ucAssetJournal1.ConstructCashTransEmptySournce();
                            ucAssetJournal1.SetCashLedger(LedgerAmount);
                            ucAssetJournal1.SetExpenseLedger(LedgerAmount);
                            ucAssetJournal1.PurchaseTransSummary = LedgerAmount;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        private void FinanceIntegrationDefaults()
        {
            ucAssetJournal1.ProjectId = ProjectId;
            ucAssetJournal1.ShowDeleteColumn = false;
            ucAssetJournal1.Flag = AssetInOut.AMC;
            ucAssetJournal1.LoadLedger();
            ucAssetJournal1.NextFocusControl = txtNarration;

            if (AMCId > 0 && AmcRenewalId != 0 && VoucherId > 0)
            {
                ucAssetJournal1.AssignValues(VoucherId);
                LoadFinanceProperties();
            }
            else
            {
                VoucherId = 0;
                ucAssetJournal1.ConstructCashTransEmptySournce();
                // CalculateFirstRowValue();
            }
        }

        private void LoadNarrationAutoComplete()
        {
            try
            {
                using (VoucherTransactionSystem vouchermastersystem = new VoucherTransactionSystem())
                {
                    resultArgs = vouchermastersystem.AutoFetchNarration();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataView dvNarration = resultArgs.DataSource.Table.AsDataView();
                        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            collection.Add(dr[vouchermastersystem.AppSchema.VoucherMaster.NARRATIONColumn.ColumnName].ToString());
                        }
                        txtNarration.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        txtNarration.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        txtNarration.MaskBox.AutoCompleteCustomSource = collection;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void LoadFinanceProperties()
        {
            try
            {
                using (AssetInwardOutwardSystem inwardoutward = new AssetInwardOutwardSystem())
                {
                    resultArgs = inwardoutward.FetchVoucherDetailsByVoucherId(VoucherId);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        lblEVNo.Text = resultArgs.DataSource.Table.Rows[0]["VOUCHER_NO"].ToString();
                        txtNarration.Text = resultArgs.DataSource.Table.Rows[0]["NARRATION"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void LoadVoucherNo()
        {
            string vType = string.Empty;
            string pId = string.Empty;
            using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
            {
                voucherTransaction.VoucherType = VoucherSubTypes.PY.ToString();
                voucherTransaction.ProjectId = ProjectId;
                voucherTransaction.VoucherDate = this.UtilityMember.DateSet.ToDate(deVoucherDate.Text, false);
                lblEVNo.Text = voucherTransaction.TempVoucherNo();
            }
        }


        /// <summary>
        /// Close the Amc Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Edit the Amc details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ripeEdit_Click(object sender, EventArgs e)
        {
            int AmcEditId = gvRenewalHistory.GetFocusedRowCellValue(colAmcId) != null ? this.UtilityMember.NumberSet.ToInteger(gvRenewalHistory.GetFocusedRowCellValue(colAmcId).ToString()) : 0;
            int AmcRewId = gvRenewalHistory.GetFocusedRowCellValue(gcColAmcRenewalId) != null ? this.UtilityMember.NumberSet.ToInteger(gvRenewalHistory.GetFocusedRowCellValue(gcColAmcRenewalId).ToString()) : 0;
            if (AmcEditId > 0 && AmcRewId > 0)
            {
                AMCId = AmcEditId;
                AmcRenewalId = AmcRewId;
                Mode = (int)AssetAmc.Edit;
                AssignValueToControls();
                SetDefaults();
            }
        }

        /// <summary>
        /// Delete the Amc details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ripeDelete_Click(object sender, EventArgs e)
        {
            try
            {
                using (AssetAMCRenewalSystem renewsystem = new AssetAMCRenewalSystem())
                {
                    int amcId = gvRenewalHistory.GetFocusedRowCellValue(colAmcId) != null ? this.UtilityMember.NumberSet.ToInteger(gvRenewalHistory.GetFocusedRowCellValue(colAmcId).ToString()) : 0;
                    int amcRenewalId = gvRenewalHistory.GetFocusedRowCellValue(gcColAmcRenewalId) != null ? this.UtilityMember.NumberSet.ToInteger(gvRenewalHistory.GetFocusedRowCellValue(gcColAmcRenewalId).ToString()) : 0;
                    voucherid = gvRenewalHistory.GetFocusedRowCellValue(colVoucherId) != null ? this.UtilityMember.NumberSet.ToInteger(gvRenewalHistory.GetFocusedRowCellValue(colVoucherId).ToString()) : 0;
                    DeleteAmcDetails(amcId, amcRenewalId);
                    if (UpdateHeld != null)
                    {
                        UpdateHeld(this, e);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.ToString());
            }
        }

        /// <summary>
        /// Delete the History Details();
        /// </summary>
        private void DeleteAmcDetails(int AmcId, int amcRenewalId)
        {
            try
            {
                using (AssetAMCRenewalSystem RenewSystem = new AssetAMCRenewalSystem())
                {
                    if (gvRenewalHistory.RowCount > 0)
                    {
                        if (gvRenewalHistory.FocusedRowHandle != GridControl.NewItemRowHandle)
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                gvRenewalHistory.DeleteRow(gvRenewalHistory.FocusedRowHandle);
                                gvRenewalHistory.UpdateCurrentRow();
                                RenewSystem.AMCId = AmcId;
                                RenewSystem.AmcRenewalId = amcRenewalId;
                                resultArgs = RenewSystem.DeleteRenewalHistoryByamcRenewalId();
                                LoadAmcRenewalHistory(AMCId);
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        /// <summary>
        /// Set border color for groups
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAssetGroupName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtAssetGroupName);
        }

        /// <summary>
        /// take date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtAMCFrom_EditValueChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// set border color provider
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtProvider_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtProvider);
        }

        /// <summary>
        /// set border color premium amount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPremiumAmount_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtPremiumAmount);
            CalculateFirstRowValue();
        }

        /// <summary>
        /// check the selected Items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void riceSelectItems_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit chked = (CheckEdit)sender;
            if (!chked.Checked)
            {

            }
        }

        /// <summary>
        /// this is to filter the records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkshowSelectedChanged_CheckedChanged(object sender, EventArgs e)
        {
            gvSelectedItemList.OptionsView.ShowAutoFilterRow = chkshowSelectedChanged.Checked;
            if (chkshowSelectedChanged.Checked)
            {
                this.SetFocusRowFilter(gvSelectedItemList, colAssetId);
            }
        }

        /// <summary>
        /// this is to filter the records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAvailableCheckChanged_CheckedChanged(object sender, EventArgs e)
        {
            gvAvailableItemList.OptionsView.ShowAutoFilterRow = chkAvailableCheckChanged.Checked;
            if (chkAvailableCheckChanged.Checked)
            {
                this.SetFocusRowFilter(gvAvailableItemList, colAssetClass);
            }
        }

        /// <summary>
        /// check all the items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSelectAllItems_CheckedChanged(object sender, EventArgs e)
        {
            if ((gcSelectedItemList.DataSource as DataTable) != null && (gcSelectedItemList.DataSource as DataTable).Rows.Count > 0)
                (gcSelectedItemList.DataSource as DataTable).Select().ToList<DataRow>().ForEach(r => { r[SELECT] = chkSelectAllItems.Checked ? 1 : 0; });
        }

        /// <summary>
        /// select the Items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAvailableItems_CheckedChanged(object sender, EventArgs e)
        {
            if ((gcAvailableItemList.DataSource as DataTable) != null && (gcAvailableItemList.DataSource as DataTable).Rows.Count > 0)
                (gcAvailableItemList.DataSource as DataTable).Select().ToList<DataRow>().ForEach(r => { r[SELECT] = chkAvailableItems.Checked ? 1 : 0; });
        }

        /// <summary>
        /// count calculation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvSelectedItemList_RowCountChanged(object sender, EventArgs e)
        {
            lblSelectedRecordCount.Text = gvSelectedItemList.RowCount.ToString();
        }

        /// <summary>
        /// Count Calculation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvAvailableItemList_RowCountChanged(object sender, EventArgs e)
        {
            lblAvailableRowCount.Text = gvAvailableItemList.RowCount.ToString();
        }

        /// <summary>
        /// Map the Items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoveInItems_Click(object sender, EventArgs e)
        {
            DataTable dtAvailableItems = (gcAvailableItemList.DataSource as DataTable);
            if (dtAvailableItems != null)
            {
                EnumerableRowCollection<DataRow> NewSelectedRowItems = dtAvailableItems.AsEnumerable().Where(dr => dr.Field<Int32>(SELECT) == 1);
                EnumerableRowCollection<DataRow> RemoveSelectedItems = dtAvailableItems.AsEnumerable().Where(dr => dr.Field<Int32>(SELECT) == 0);
                if (NewSelectedRowItems.Count() > 0)
                {
                    DataTable dtMappedRecords = NewSelectedRowItems.CopyToDataTable();
                    gcAvailableItemList.DataSource = RemoveSelectedItems.Count() > 0 ? RemoveSelectedItems.CopyToDataTable() : null;
                    if (gvSelectedItemList.RowCount == 0)
                    {
                        dtMappedRecords.Select().ToList<DataRow>().ForEach(dr => dr[SELECT] = 0);
                        gcSelectedItemList.DataSource = dtMappedRecords;
                    }
                    else
                    {
                        // Merge records with already mapped Items or already moved records
                        dtMappedRecords = (gcSelectedItemList.DataSource as DataTable);
                        dtMappedRecords.Merge(NewSelectedRowItems.CopyToDataTable());
                        dtMappedRecords.Select().ToList<DataRow>().ForEach(dr => dr[SELECT] = 0);
                        gcSelectedItemList.DataSource = dtMappedRecords;
                    }
                }
                else
                {
                    //ShowMessageBox("No record selected to move");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AMCVoucher.AMC_NORECORD_SELECT_MOVE));
                }
            }
        }

        /// <summary>
        /// Unmap the Items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoveOutItems_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtSelectedLocation = (gcSelectedItemList.DataSource as DataTable);
                if (dtSelectedLocation != null)
                {
                    EnumerableRowCollection<DataRow> SelectedLocations = dtSelectedLocation.AsEnumerable().Where(dr => dr.Field<Int32>(SELECT) == 1);
                    EnumerableRowCollection<DataRow> UnSelectedLocations = dtSelectedLocation.AsEnumerable().Where(dr => dr.Field<Int32>(SELECT) == 0);
                    if (SelectedLocations.Count() > 0)
                    {
                        DataTable dtUnMappedRecords = SelectedLocations.CopyToDataTable();
                        gcSelectedItemList.DataSource = UnSelectedLocations.Count() > 0 ? UnSelectedLocations.CopyToDataTable() : null;
                        if (gvAvailableItemList.RowCount == 0)
                        {
                            dtUnMappedRecords.Select().ToList<DataRow>().ForEach(dr => dr[SELECT] = 0);
                            gcAvailableItemList.DataSource = dtUnMappedRecords;
                        }
                        else
                        {
                            // Merge records with already mapped location or already moved records
                            dtUnMappedRecords = (gcAvailableItemList.DataSource as DataTable);
                            dtUnMappedRecords.Merge(SelectedLocations.CopyToDataTable());
                            dtUnMappedRecords.Select().ToList<DataRow>().ForEach(dr => dr[SELECT] = 0);
                            gcAvailableItemList.DataSource = dtUnMappedRecords;
                        }
                    }
                    else
                    {
                        //ShowMessageBox("No record selected to move");
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AMCVoucher.AMC_NORECORD_SELECT_MOVE));
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        private void deRenewalDate_Leave(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(deRenewalDate.DateTime.ToString()))
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AMCVoucher.AMCDATE_VALIDATION));
            //    deRenewalDate.Focus();
            //}
            //else
            //{
            //    dtAMCFrom.Properties.MinValue = dtAMCFrom.DateTime = dtAMCTo.Properties.MinValue = deRenewalDate.DateTime.AddDays(1);
            //    dtAMCTo.DateTime = dtAMCTo.Properties.MaxValue = dtAMCFrom.DateTime.AddYears(1);
            //}
        }

        /// <summary>
        /// editvalue datasource
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
            // LoadExpenseLedgers();
            // LoadCashBankLedgers();
            FinanceIntegrationDefaults();
            LoadAssetItemDetails();
        }

        /// <summary>
        /// Load the cash Bank Ledger
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glkpCashBankLedger_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (CashBankGroupId == (int)FixedLedgerGroup.BankAccounts)
                {
                    //layoutControlItem19.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    //layoutControlItem20.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
                else
                {
                    //layoutControlItem19.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    //layoutControlItem20.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            finally { }
        }

        private void gvSelectedItemList_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
                if (AMCId > 0)
                {
                    if (gvSelectedItemList.GetRowCellValue(gvSelectedItemList.FocusedRowHandle, colStatus) != null)
                    {
                        if (gvSelectedItemList.GetRowCellValue(gvSelectedItemList.FocusedRowHandle, colStatus).ToString() != string.Empty)
                        {
                            string Status = (string)gvSelectedItemList.GetRowCellValue(gvSelectedItemList.FocusedRowHandle, colStatus).ToString();
                            //Status 0 = Sold or Donoted or Diposed
                            //Status 1 = Purchase
                            if (Status == "0")// && gvSelectedItemList.FocusedColumn == colDelete)
                            {
                                e.Cancel = true; //Disabling the editing of the cell 
                                //this.ShowMessageBox("This Item has been Sold or Diposed or Donated");
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AMCVoucher.AMC_SALES_DONATE_DISPOSE_SOLD_INFO));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        private void gvSelectedItemList_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (AMCId > 0)
                {
                    if (gvSelectedItemList.GetRowCellValue(e.RowHandle, colStatus) != null)
                    {
                        if (gvSelectedItemList.GetRowCellValue(e.RowHandle, colStatus).ToString() != string.Empty)
                        {
                            string Status = gvSelectedItemList.GetRowCellValue(e.RowHandle, colStatus).ToString();
                            //Status 0 = sold or Donoted or Diposed
                            //Status 1 = Purchase
                            if (Status == "0")
                            {
                                e.Appearance.BackColor = Color.Red;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }
        #endregion

        #region User Rights

        private void ApplyRights()
        {
            bool createprojectrights = (CommonMethod.ApplyUserRights((int)Forms.CreateProject) != 0);
            glkpProject.Properties.Buttons[1].Visible = createprojectrights;
        }

        #endregion

        #region Methods

        /// <summary>
        /// This is to validate the Amc Details
        /// </summary>
        /// <returns></returns>
        private bool IsValidateAMC()
        {
            bool isAmc = true;
            if (string.IsNullOrEmpty(txtAssetGroupName.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AMCVoucher.AMCGROUP_IS_EMPTY));
                isAmc = false;
                txtAssetGroupName.Focus();
            }
            else if (string.IsNullOrEmpty(txtProvider.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AMCVoucher.AMCPROVIDER_IS_EMPYT));
                isAmc = false;
                txtProvider.Focus();
            }
            else if (dtAMCFrom.EditValue == null)
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AMCVoucher.AMC_FROM_EMPTY));
                isAmc = false;
                dtAMCFrom.Focus();
            }
            else if (dtAMCTo.EditValue == null)
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AMCVoucher.AMC_TO_EMPTY));
                isAmc = false;
                dtAMCTo.Focus();
            }
            else if (!this.UtilityMember.DateSet.ValidateDate(dtAMCFrom.DateTime, dtAMCTo.DateTime))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AMCVoucher.AMCDATE_VALIDATION));
                isAmc = false;
                dtAMCTo.Focus();
            }
            else if ((string.IsNullOrEmpty(txtPremiumAmount.Text)) || txtPremiumAmount.Text == "0.00")
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AMCVoucher.AMCPREMIUM_AMOUNT));
                isAmc = false;
                txtPremiumAmount.Focus();
            }
            else if (deRenewalDate.EditValue == null)
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AMCVoucher.RENEWAL_DATE_EMPTY));
                isAmc = false;
                deRenewalDate.Focus();
            }
            else if (!this.UtilityMember.DateSet.ValidateDate(dtAMCFrom.DateTime, dtAMCTo.DateTime))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AMCVoucher.AMC_RENEWAL_DATE_VALIDATION));
                isAmc = false;
                deRenewalDate.Focus();
            }
            return isAmc;
        }

        /// <summary>
        /// Assign value to Controls
        /// </summary>
        private void AssignValueToControls()
        {
            using (AssetAMCRenewalSystem amcSystem = new AssetAMCRenewalSystem(AMCId, AmcRenewalId))
            {
                if (AMCId > 0 && AmcRenewalId != 0)
                {
                    //  AssignPaymentLedgers();
                    amcSystem.AMCId = AMCId;
                    amcSystem.AmcRenewalId = AmcRenewalId;
                    txtPremiumAmount.Text = amcSystem.PremiumAmount.ToString();
                    dtAMCFrom.DateTime = amcSystem.AmcFrom;
                    dtAMCTo.DateTime = amcSystem.AmcTo;
                    deRenewalDate.DateTime = amcSystem.RenewalDate;
                    VoucherId = amcSystem.VoucherId;
                    //  glkpExpenseLedger.EditValue = ExpenseEledgerid;
                    //  glkpCashBankLedger.EditValue = CashBankLedgerId;
                }
                glkpProject.EditValue = amcSystem.ProjectId;
                txtAssetGroupName.Text = amcSystem.AMCGroup;
                txtProvider.Text = amcSystem.Provider;
            }

        }

        /// <summary>
        /// Create the Row Details
        /// </summary>
        /// <param name="dtLedgers"></param>
        /// <returns></returns>
        private DataTable AddColumns(DataTable dtLedgers)
        {
            DataTable dtAddedledger = dtLedgers;
            if (!dtAddedledger.Columns.Contains("SELECT"))
            {
                dtAddedledger.Columns.Add("SELECT", typeof(int));
            }
            return dtAddedledger;
        }

        /// <summary>
        /// Clear controls
        /// </summary>
        private void ClearControls()
        {
            if (Mode == (int)AssetInsurance.Create)
            {
                txtAssetGroupName.Text = txtProvider.Text = txtPremiumAmount.Text = string.Empty;
                dtAMCFrom.DateTime = dtAMCTo.DateTime = this.UtilityMember.DateSet.ToDate(DateTime.Now.ToString(), false);
                this.txtAssetGroupName.Focus();
            }
        }

        /// <summary>
        /// Load AvailableDetails
        /// </summary>
        private void LoadDefaultItems()
        {
            try
            {
                using (AssetAMCRenewalSystem amcSystem = new AssetAMCRenewalSystem())
                {
                    resultArgs = amcSystem.LoadItemDetails();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        gcAvailableItemList.DataSource = resultArgs.DataSource.Table;
                        gcAvailableItemList.RefreshDataSource();
                    }
                    else
                    {
                        ShowMessageBox(resultArgs.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.ToString());
            }
        }

        /// <summary>
        /// Load the Grid
        /// </summary>
        private void LoadAssetItemDetails()
        {
            try
            {
                using (AssetAMCRenewalSystem amcSystem = new AssetAMCRenewalSystem())
                {
                    amcSystem.AMCId = AMCId;
                    amcSystem.ProjectId = this.ProjectId;
                    resultArgs = AMCId != 0 ? amcSystem.FetchAvailableMappedItems() : amcSystem.FetchUnmappedItems();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        gcAvailableItemList.DataSource = AddSelectColumn(resultArgs.DataSource.Table);
                        gcAvailableItemList.RefreshDataSource();
                        resultArgs = amcSystem.FetchSelectedMappedItems();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            gcSelectedItemList.DataSource = AddSelectColumn(resultArgs.DataSource.Table);
                            gcSelectedItemList.RefreshDataSource();
                        }
                        else ShowMessageBox(resultArgs.Message);
                    }
                    else
                    {
                        ShowMessageBox(resultArgs.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.ToString());
            }
        }

        /// <summary>
        /// Create the Select coloums
        /// </summary>
        /// <param name="dtSource"></param>
        /// <returns></returns>
        private DataTable AddSelectColumn(DataTable dtSource)
        {
            if (!dtSource.Columns.Contains(SELECT))
            {
                dtSource.Columns.Add(SELECT, typeof(Int32));
                dtSource.Select().ToList<DataRow>().ForEach(r => { r[SELECT] = 0; });
            }
            return dtSource;
        }

        /// <summary>
        /// load the defaults
        /// </summary>
        private void LoadDefaults()
        {
            if (Mode == (int)AssetAmc.Create)
            {
                HideRenewDetails();
            }
            else if (Mode == (int)AssetAmc.Renew)
            {
                txtAssetGroupName.Enabled = false;
                txtProvider.Enabled = false;
                glkpProject.Enabled = false;
                //this.Text = "AMC(Renew)";
                this.Text = this.GetMessage(MessageCatalog.Asset.AMCVoucher.AMC_RENEW_MODE_CAPTION);
                LoadAmcRenewalHistory(AMCId);
            }
            else if (Mode == (int)AssetAmc.Edit)
            {
                txtAssetGroupName.Enabled = true;
                txtProvider.Enabled = true;
                //this.Text = "Renewal(Edit)";
                this.Text = this.GetMessage(MessageCatalog.Asset.AMCVoucher.AMC_RENEWAL_EDIT_MODE_CAPTION);
                LoadAmcRenewalHistory(AMCId);
            }

        }

        /// <summary>
        /// Load the History details from by Amc Id's
        /// </summary>
        /// <param name="amcId"></param>
        private void LoadAmcRenewalHistory(int amcId)
        {
            try
            {
                using (AssetAMCRenewalSystem assetamcrenewalSystem = new AssetAMCRenewalSystem())
                {
                    assetamcrenewalSystem.AMCId = amcId;
                    resultArgs = assetamcrenewalSystem.FetchAMCRenewalHistory(amcId.ToString());
                    if (resultArgs != null)
                    {
                        if (Mode == (int)AssetAmc.Renew)
                        {
                            if (resultArgs.DataSource.Table.Rows.Count > 0)
                                dtAMCRenewDate = UtilityMember.DateSet.ToDate
                                    (resultArgs.DataSource.Table.Rows[0][AppSchemas.AMCRenewalHistory.AMC_TOColumn.ColumnName].ToString(), false);
                            dtAMCFrom.DateTime = deRenewalDate.DateTime = dtAMCTo.Properties.MinValue = dtAMCFrom.Properties.MinValue = dtAMCRenewDate.AddDays(1);
                            dtAMCTo.DateTime = dtAMCRenewDate.AddYears(1);
                            deRenewalDate.DateTime = dtAMCTo.DateTime;
                        }
                        gcRenewalHistory.DataSource = resultArgs.DataSource.Table;
                        gcRenewalHistory.RefreshDataSource();
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
        /// Hide the History Details while Creating Insurance
        /// </summary>
        private void HideRenewDetails()
        {
            lcpAssetRenewalHistory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.CenterToScreen();

        }

        private void SetDefaults()
        {
            try
            {
                if (AMCId == 0)
                {
                    layoutControlItem12.AllowHtmlStringInCaption = true;
                    //layoutControlItem12.Text = "Date of AMC<color='red'>*";
                    layoutControlItem12.Text =this.GetMessage(MessageCatalog.Asset.AMCVoucher.AMC_DATEOF_AMC_CAPTION)+ "<Color='red'>*";
                    dtAMCFrom.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
                    dtAMCTo.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
                    deRenewalDate.DateTime = dtAMCTo.DateTime;
                }
                else if (AMCId > 0 && Mode == (int)AssetAmc.Renew)
                {
                    using (AssetAMCRenewalSystem amcsystem = new AssetAMCRenewalSystem())
                    {
                        DateTime LastRenewaldate = this.UtilityMember.DateSet.ToDate(amcsystem.FetchLastRenewldateByAMCId(AMCId), false).AddDays(1);
                        if (!string.IsNullOrEmpty(LastRenewaldate.ToString()))
                        {
                            dtAMCFrom.DateTime = LastRenewaldate;
                            dtAMCTo.DateTime = dtAMCFrom.DateTime.AddYears(1);
                            deRenewalDate.DateTime = dtAMCTo.DateTime;
                        }
                    }
                }
                else if (AMCId > 0 && Mode == (int)AssetAmc.Edit)
                {
                    ResultArgs result = new ResultArgs();
                    DataTable dtInsurance = new DataTable();

                    result = FetchPreviousRenewal();
                    if (result != null && result.Success && resultArgs.DataSource.Table != null)
                    {
                        if (resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            dtAMCFrom.Properties.MinValue = this.UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["AMC_TO"].ToString(), false).AddDays(1);
                            dtAMCTo.Properties.MinValue = dtAMCFrom.DateTime.AddDays(1);
                        }
                    }

                    result = FetchNextRenewal();
                    if (result != null && result.Success && resultArgs.DataSource.Table != null)
                    {
                        if (resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            dtAMCTo.Properties.MaxValue = dtAMCFrom.Properties.MaxValue = this.UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["AMC_FROM"].ToString(), false).AddDays(-1);
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

        // Load the Immediate added project in to the Project Combo

        public void LoadProjectCombo()
        {
            if (this.AppSetting.LockMasters == (int)YesNo.No)
            {
                frmProjectAdd frmprojectAdd = new frmProjectAdd();
                frmprojectAdd.ShowDialog();
                if (frmprojectAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                {
                    LoadProject();
                    if (frmprojectAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmprojectAdd.ReturnValue.ToString()) > 0)
                    {
                        glkpProject.EditValue = this.UtilityMember.NumberSet.ToInteger(frmprojectAdd.ReturnValue.ToString());
                    }
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
            }
        }

        private void LoadProject()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    resultArgs = mappingSystem.FetchProjectsLookup();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProject, resultArgs.DataSource.Table, mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                        glkpProject.EditValue = glkpProject.Properties.GetKeyValue(0);
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

        //private void LoadExpenseLedgers()
        //{
        //    try
        //    {
        //        using (LedgerSystem ledgersystem = new LedgerSystem())
        //        {
        //            ledgersystem.ProjectId = ProjectId;
        //            ledgersystem.GroupId = 2;
        //            resultArgs = ledgersystem.FetchAllExpenceLedgers();
        //            if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
        //            {
        //                //this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpExpenseLedger, resultArgs.DataSource.Table, ledgersystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgersystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
        //                //glkpExpenseLedger.EditValue = glkpExpenseLedger.Properties.GetKeyValue(0);
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        MessageRender.ShowMessage(Ex.Message);
        //    }
        //    finally { }
        //}

        //private void LoadCashBankLedgers()
        //{
        //    try
        //    {
        //        using (LedgerSystem ledgersystem = new LedgerSystem())
        //        {
        //            ledgersystem.ProjectId = ProjectId;
        //            resultArgs = ledgersystem.FetchCashBankLedger();
        //            if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
        //            {
        //                //this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpCashBankLedger, resultArgs.DataSource.Table, ledgersystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgersystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
        //                //glkpCashBankLedger.EditValue = glkpCashBankLedger.Properties.GetKeyValue(0);
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        MessageRender.ShowMessage(Ex.Message);
        //    }
        //    finally { }
        //}

        private void SetVoucherDate()
        {
            deVoucherDate.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deVoucherDate.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            deVoucherDate.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
        }

        private void AssignPaymentLedgers()
        {
            using (AssetAMCRenewalSystem renewalsys = new AssetAMCRenewalSystem())
            {

                voucherid = renewalsys.FetchVoucherIdByamcRenewalId(AmcRenewalId);
                renewalsys.VoucherId = voucherid;
                if (voucherid > 0)
                {
                    DataTable dtLedgers = renewalsys.FetchLedgerIdByvoucherId(voucherid).DataSource.Table;
                    if (dtLedgers != null && dtLedgers.Rows.Count > 0)
                    {
                        deVoucherDate.DateTime = this.UtilityMember.DateSet.ToDate(dtLedgers.Rows[0]["VOUCHER_DATE"].ToString(), false);
                        DataView dvTransaction = dtLedgers.AsDataView();
                        dvTransaction.RowFilter = "TRANS_MODE='DR'";
                        DataTable dtTransaction = dvTransaction.ToTable();
                        ExpenseEledgerid = this.UtilityMember.NumberSet.ToInteger(dtTransaction.Rows[0]["LEDGER_ID"].ToString());
                        dvTransaction.RowFilter = "";

                        DataView dvCash = dtLedgers.AsDataView();
                        dvCash.RowFilter = "TRANS_MODE='CR'";
                        DataTable dtCash = dvCash.ToTable();
                        CashBankLedgerId = this.UtilityMember.NumberSet.ToInteger(dtCash.Rows[0]["LEDGER_ID"].ToString());
                        dvCash.RowFilter = "";
                    }
                }
            }
        }

        private void SetDateDuration()
        {
            ResultArgs result = new ResultArgs();
            DataTable dtInsurance = new DataTable();
            if (AMCId > 0 && AmcRenewalId > 0)
            {
                result = FetchPreviousRenewal();
                if (result != null && result.Success && resultArgs.DataSource.Table != null)
                {
                    if (resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        dtAMCFrom.Properties.MinValue = dtAMCTo.Properties.MinValue = this.UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["AMC_TO"].ToString(), false);
                    }
                }

                result = FetchNextRenewal();
                if (result != null && result.Success && resultArgs.DataSource.Table != null)
                {
                    if (resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        dtAMCTo.Properties.MaxValue = dtAMCFrom.Properties.MaxValue = this.UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["AMC_FROM"].ToString(), false);
                    }
                }
            }
        }

        private ResultArgs FetchPreviousRenewal()
        {
            try
            {
                using (AssetAMCRenewalSystem renewalsystem = new AssetAMCRenewalSystem())
                {
                    renewalsystem.AMCId = AMCId;
                    renewalsystem.AmcRenewalId = AmcRenewalId;
                    resultArgs = renewalsystem.FetchPreviousRenewal();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            return resultArgs;
        }

        private ResultArgs FetchNextRenewal()
        {
            try
            {
                using (AssetAMCRenewalSystem renewalsystem = new AssetAMCRenewalSystem())
                {
                    renewalsystem.AMCId = AMCId;
                    renewalsystem.AmcRenewalId = AmcRenewalId;

                    resultArgs = renewalsystem.FetchNextRenewal();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            return resultArgs;
        }
        #endregion

        private void deVoucherDate_EditValueChanged(object sender, EventArgs e)
        {
            LoadProject();
            LoadVoucherNo();
        }

        private void glkpProject_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                LoadProjectCombo();
            }
        }
    }

}