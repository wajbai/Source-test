/*  Class Name      : frmInsuranceRenewal.cs
 *  Purpose         : To Insert Renewal
 *  Author          : CD
 *  Created on      : 03-june-2015
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;

using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model.Inventory.Asset;
using Bosco.Utility;
using Bosco.DAO.Schema;
using Bosco.Model.UIModel;
using Bosco.Model;
using DevExpress.XtraGrid;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmInsuranceRenewal : frmBaseAdd
    {
        #region VariableDeclaration
        private int ProjectId { get; set; }
        private string Project { get; set; }
        #endregion

        #region Constructor
        public frmInsuranceRenewal()
        {
            InitializeComponent();
        }

        public frmInsuranceRenewal(int renewalId, int itemId, int insId, int prjId, string PJName, DataTable dtRenewal)
            : this()
        {
            RenewalId = renewalId;
            ItemId = itemId;
            InsuranceId = insId;
            ProjectId = prjId;
            Project = PJName;
            dtInsRenewal = dtRenewal;
        }
        #endregion

        #region Properties
        AppSchemaSet appSchema = new AppSchemaSet();
        ResultArgs resultArgs = null;
        private DataTable dtInsRenewal { get; set; }
        public event EventHandler UpdateHeld;
        public int RenewalId { get; set; }
        private int ItemId { get; set; }
        public const string ITEM_ID = "ITEM_ID";
        private const string DUE_DATE = "DUE_DATE";
        private const string RENEWAL_AMOUNT = "RENEWAL_AMOUNT";
        public int InsuranceId { get; set; }
        private DataTable dtActiveAsset { get; set; }
        private DateTime Duedate { get; set; }
        List<int> InsRenewal = new List<int>();
        private int VoucherId { get; set; }

        private int renewalId = 0;
        private int SelectedRenewalId
        {
            get
            {
                renewalId = gvRenewalDetail.GetFocusedRowCellValue(colRenewalId) != null ? this.UtilityMember.NumberSet.ToInteger(gvRenewalDetail.GetFocusedRowCellValue(colRenewalId).ToString()) : 0;
                return renewalId;
            }
            set
            {
                renewalId = value;
            }
        }

        private double AmountSummaryVal
        {
            get { return this.UtilityMember.NumberSet.ToDouble(ColRenAmount.SummaryItem.SummaryValue.ToString()); }
        }

        private void glkpExpenceLedger_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                ACPP.Modules.Master.frmLedgerDetailAdd frmBank = new ACPP.Modules.Master.frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.GN, ProjectId);
                frmBank.ShowDialog();
                LoadCashBankLedger();
            }
        }
        private void glkpLedger_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                ACPP.Modules.Master.frmLedgerDetailAdd frmBank = new ACPP.Modules.Master.frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.BK, ProjectId);
                frmBank.ShowDialog();
                LoadCashBankLedger();
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Load the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmInsuranceRenewal_Load(object sender, EventArgs e)
        {
            Caption();
            SetDefaultDates();
            BindRenewal();
            LoadIns();
            LoadRenewaDetail(InsuranceId, ItemId, ProjectId);
        }

        /// <summary>
        /// Save the Renewal Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValidateRenewal())
            {
                using (InsuranceRenewalSystem insuranceRenewalSystem = new InsuranceRenewalSystem())
                {
                    insuranceRenewalSystem.InsId = InsuranceId;
                    insuranceRenewalSystem.RenewalId = RenewalId;
                    insuranceRenewalSystem.VoucherDate = this.UtilityMember.DateSet.ToDate(dtRenewalDate.Text, false);
                    insuranceRenewalSystem.CashLedId = this.UtilityMember.NumberSet.ToInteger(glkpLedger.EditValue.ToString());
                    insuranceRenewalSystem.ProjectId = ProjectId;
                    insuranceRenewalSystem.VouId = VoucherId;
                    insuranceRenewalSystem.RenewalAmount = this.UtilityMember.NumberSet.ToDecimal(AmountSummaryVal.ToString());
                    insuranceRenewalSystem.NameAddress = txtNameAddress.Text.Trim();
                    insuranceRenewalSystem.Narration = txtNarration.Text.Trim();
                    insuranceRenewalSystem.ExpLedger = this.UtilityMember.NumberSet.ToInteger(glkpExpenceLedger.EditValue.ToString());
                    DataTable dtSource = gcRenewal.DataSource as DataTable;
                    DataTable dtFilteredRows = dtSource.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull)).CopyToDataTable();
                    insuranceRenewalSystem.dtRenewalDetails = dtFilteredRows;
                    insuranceRenewalSystem.InsRenewal = InsRenewal;
                    //if (ValidateDate())
                    //{
                        resultArgs = insuranceRenewalSystem.SaveRenewal();
                        if (resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            ClearControls();
                            dtInsRenewal.Columns.Remove(DUE_DATE);
                            dtInsRenewal.Columns.Add("DUE_DATE", typeof(DateTime));
                            dtInsRenewal.Rows[0][RENEWAL_AMOUNT] = 0;
                            gcRenewal.DataSource = dtInsRenewal;
                            gcRenewal.RefreshDataSource();
                            LoadRenewaDetail(InsuranceId, ItemId, ProjectId);
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                            InsRenewal.Clear();
                        }
                   // }
                }
            }
        }

        /// <summary>
        /// Validate the Asset Name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rglkpItemName_Validating(object sender, CancelEventArgs e)
        {
            int itemID = 0;
            GridLookUpEdit gridLKPEdit = sender as GridLookUpEdit;
            if (gridLKPEdit.EditValue != null)
            {
                DataRowView drv = gridLKPEdit.GetSelectedDataRow() as DataRowView;
                if (drv != null)
                {
                    itemID = this.UtilityMember.NumberSet.ToInteger(drv[ITEM_ID].ToString());
                }
            }
        }

        /// <summary>
        /// Leave the Date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtRenewalDate_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(dtRenewalDate);
        }

        /// <summary>
        /// Leave ledger set Border
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glkpLedger_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpLedger);
        }

        /// <summary>
        /// To Delete the add form row entry record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteTransaction();
        }
        private void glkpExpenceLedger_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpExpenceLedger);
        }

        /// <summary>
        /// Process the commands
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="KeyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData.Equals(Keys.F5))
            {
                ShowProjectWindow();
            }
            else if (KeyData == (Keys.Alt | Keys.D))
            {
                DeleteTransaction();
            }
            else if (KeyData.Equals(Keys.F3))
            {
                dtRenewalDate.Focus();
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        /// <summary>
        /// Delete the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnDelete_Click(object sender, EventArgs e)
        {
            DeleteTransaction();
        }

        /// <summary>
        /// Set the Row events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvRenewal_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            string assetIds = string.Empty;
            string ItemName = string.Empty;
            if (gvRenewal.FocusedColumn == ColAssetID && !string.IsNullOrEmpty(gvRenewal.GetFocusedRowCellValue(ColAssetID).ToString()))
            {
                ItemName = gvRenewal.GetFocusedRowCellValue(ColItemName).ToString();
                assetIds = gvRenewal.GetFocusedRowCellValue(ColAssetID).ToString();
                if (!string.IsNullOrEmpty(assetIds) && !string.IsNullOrEmpty(ItemName))
                {
                    LoadAssetByItemId();
                    gvRenewal.SetRowCellValue(gvRenewal.FocusedRowHandle, ColAssetID, assetIds);
                    gvRenewal.UpdateCurrentRow();
                }
                else
                {
                    LoadActiveAssets();
                }
            }
        }

        /// <summary>
        /// Process grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcRenewal_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control)
                {
                    gvRenewal.PostEditor();
                    gvRenewal.UpdateCurrentRow();

                    if (gvRenewal.FocusedColumn == ColItemName)
                    {
                        if (string.IsNullOrEmpty(gvRenewal.GetFocusedRowCellValue(ColItemName).ToString()))
                        {
                            gvRenewal.CloseEditor();
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                            glkpExpenceLedger.Focus();
                            glkpExpenceLedger.Select();
                        }
                    }
                    if (gvRenewal.FocusedColumn == ColDueDate)
                    {
                         //   ValidateDate();
                    }
                    if (gvRenewal.IsLastRow && (gvRenewal.FocusedColumn == ColRenAmount) &&
                        !string.IsNullOrEmpty(gvRenewal.GetFocusedRowCellValue(ColRenAmount).ToString()))
                    {
                        string AssetName = gvRenewal.GetFocusedRowCellValue(ColItemName) != null ? gvRenewal.GetFocusedRowCellValue(ColItemName).ToString() : string.Empty;
                        string AssetId = gvRenewal.GetFocusedRowCellValue(ColAssetID) != null ? gvRenewal.GetFocusedRowCellValue(ColAssetID).ToString() : string.Empty;
                        string DueDate = gvRenewal.GetFocusedRowCellValue(ColDueDate) != null ? gvRenewal.GetFocusedRowCellValue(ColDueDate).ToString() : string.Empty;
                        if (!string.IsNullOrEmpty(AssetName) && !string.IsNullOrEmpty(AssetId) && !string.IsNullOrEmpty(DueDate))
                        {
                            //gvRenewal.AddNewRow();
                            //gvRenewal.FocusedColumn = gvRenewal.Columns[ColItemName.Name];
                            //LoadActiveAssets();
                            //gvRenewal.ShowEditor();
                            glkpExpenceLedger.Focus();
                            glkpExpenceLedger.Select();
                        }
                        else
                        {
                            gvRenewal.CloseEditor();
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                            glkpExpenceLedger.Focus();
                            glkpExpenceLedger.Select();
                        }
                    }
                }
                else if (gvRenewal.IsFirstRow && gvRenewal.FocusedColumn == ColItemName && e.Shift && e.KeyCode == Keys.Tab)
                {
                    dtRenewalDate.Select();
                    dtRenewalDate.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        /// <summary>
        /// Close the Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Focus the date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiDate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dtRenewalDate.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (SelectedRenewalId > 0)
            {
                RenewalId = SelectedRenewalId;
                Caption();
                AssignRenewalProperties();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteRenewalDetail();
        }

        #endregion

        #region Methods
        /// <summary>
        /// Assign the properties
        /// </summary>
        private void AssignRenewalProperties()
        {
            if (RenewalId > 0)
            {
                using (InsuranceRenewalSystem insuranceRenewalSystem = new InsuranceRenewalSystem(RenewalId))
                {
                    txtVoucherNo.Text = insuranceRenewalSystem.VoucherNo;
                    txtNarration.Text = insuranceRenewalSystem.Narration;
                    dtRenewalDate.DateTime = insuranceRenewalSystem.VoucherDate;
                    txtNameAddress.Text = insuranceRenewalSystem.NameAddress;
                    txtNarration.Text = insuranceRenewalSystem.Narration;
                    ucCaptionPanel1.Caption = Project;
                    glkpLedger.EditValue = insuranceRenewalSystem.CashLedId.ToString();
                    glkpExpenceLedger.EditValue = insuranceRenewalSystem.ExpLedger.ToString();
                    VoucherId = insuranceRenewalSystem.VouId;
                    int ItemId = insuranceRenewalSystem.ItemId;
                    resultArgs = insuranceRenewalSystem.FetchRenewalDetailById(RenewalId.ToString());
                    if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        Duedate=this.UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][appSchema.AppSchema.InsuranceRenewalDetails.DUE_DATEColumn.ColumnName].ToString(),false);
                        gcRenewal.DataSource = resultArgs.DataSource.Table;
                        gcRenewal.RefreshDataSource();
                    }
                }
            }
        }

        /// <summary>
        /// Load the Asset Item
        /// </summary>
        private void LoadAssetItem()
        {
            using (InsuranceRenewalSystem insuranceRenewalSystem = new InsuranceRenewalSystem())
            {
                resultArgs = insuranceRenewalSystem.LoadItems();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpItemName, resultArgs.DataSource.Table, insuranceRenewalSystem.AppSchema.ASSETItem.ASSET_ITEMColumn.ColumnName, insuranceRenewalSystem.AppSchema.ASSETItem.ITEM_IDColumn.ColumnName);
                }
            }
        }

        private void LoadRenewaDetail(int InsuranceId, int ItemId, int ProjectId)
        {
            using (InsuranceRenewalSystem insuranceRenewalSystem = new InsuranceRenewalSystem(InsuranceId, ItemId, ProjectId))
            {
                resultArgs = insuranceRenewalSystem.FetchAllRenewal(RenewalId);
                if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    gcRenewalDetail.DataSource = resultArgs.DataSource.Table;
                    gcRenewalDetail.RefreshDataSource();
                }
                else
                {
                    gcRenewalDetail.DataSource = resultArgs.DataSource.Table;
                    gcRenewalDetail.RefreshDataSource();
                }
            }
        }

        private void LoadIns()
        {
            if (!dtInsRenewal.Columns.Contains("DUE_DATE"))
            {
                dtInsRenewal.Columns.Add("DUE_DATE", typeof(DateTime));
            }
            gcRenewal.DataSource = dtInsRenewal;
            gcRenewal.RefreshDataSource();
        }

        private bool ValidateDate()
        {
            bool isvalid = true;
            string dueDate = gvRenewal.GetFocusedRowCellValue(ColDueDate) != null ? gvRenewal.GetFocusedRowCellValue(ColDueDate).ToString() : string.Empty;
            if (!string.IsNullOrEmpty(dueDate))
            {
                if (RenewalId == 0)
                {
                    using (InsuranceRenewalSystem insuranceRenewalSystem = new InsuranceRenewalSystem())
                    {
                        resultArgs = insuranceRenewalSystem.ValidateDate(ItemId, ProjectId, dueDate);
                        if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            isvalid = false;
                        }
                    }
                }
                else 
                {
                    //if (!this.UtilityMember.DateSet.ValidateDate(Duedate, this.UtilityMember.DateSet.ToDate(dueDate, false)))
                    //{
                    //    isvalid = false;
                    //}
                }
            }
            return isvalid;
        }

        /// <summary>
        /// Load Expence Ledger
        /// </summary>
        private void LoadExpenseLedgers()
        {
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                ledgerSystem.GroupId = (int)Natures.Expenses;
                resultArgs = ledgerSystem.FetchLedgerByNature();

                if (resultArgs != null && resultArgs.Success)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpExpenceLedger, resultArgs.DataSource.Table,
                        ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                }
            }
        }

        /// <summary>
        /// Construct the details
        /// </summary>
        private void ConstructInsuranceRenewal()
        {
            DataTable dtInsuranceRenewal = new DataTable();
            dtInsuranceRenewal.Columns.Add("INS_ID", typeof(UInt32));
            dtInsuranceRenewal.Columns.Add("ITEM_ID", typeof(UInt32));
            dtInsuranceRenewal.Columns.Add("ASSET_ID", typeof(String));
            dtInsuranceRenewal.Columns.Add("DUE_DATE", typeof(DateTime));
            dtInsuranceRenewal.Columns.Add("RENEWAL_AMOUNT", typeof(decimal));
            gcRenewal.DataSource = dtInsuranceRenewal;
            gvRenewal.AddNewRow();
        }

        /// <summary>
        /// Delete the details
        /// </summary>
        private void DeleteTransaction()
        {
            try
            {
                if (gvRenewal.RowCount > 1)
                {
                    if (gvRenewal.FocusedRowHandle != GridControl.NewItemRowHandle)
                    {

                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            gvRenewal.DeleteRow(gvRenewal.FocusedRowHandle);
                            gvRenewal.UpdateCurrentRow();
                            gcRenewal.RefreshDataSource();
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                        }
                    }
                }
                else if (gvRenewal.RowCount == 1)
                {
                    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ConstructInsuranceRenewal();
                        gcRenewal.RefreshDataSource();
                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        private void DeleteRenewalDetail()
        {
            try
            {
                if (gvRenewalDetail.RowCount > 0)
                {
                    if (gvRenewalDetail.FocusedRowHandle != GridControl.NewItemRowHandle)
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            InsRenewal.Add(gvRenewalDetail.GetFocusedRowCellValue(colRenewalId) != null ? this.UtilityMember.NumberSet.ToInteger(gvRenewalDetail.GetFocusedRowCellValue(colRenewalId).ToString()) : 0);
                            gvRenewalDetail.DeleteRow(gvRenewalDetail.FocusedRowHandle);
                            gvRenewalDetail.UpdateCurrentRow();
                            gcRenewalDetail.RefreshDataSource();
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                        }
                    }
                }
                else if (gvRenewalDetail.RowCount == 1)
                {
                    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        InsRenewal.Add(SelectedRenewalId);
                        ConstructInsuranceRenewal();
                        gcRenewalDetail.RefreshDataSource();
                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        /// <summary>
        /// Load the Active Asset
        /// </summary>
        private void LoadActiveAssets()
        {
            try
            {
                using (AssetItemSystem assetItemSystem = new AssetItemSystem())
                {
                    resultArgs = assetItemSystem.FetchActiveAssetItem();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        dtActiveAsset = resultArgs.DataSource.Table;
                        this.UtilityMember.ComboSet.BindRepositoryItemCheckBoxGridLookUpEdit(rccmbAssetId, resultArgs.DataSource.Table, assetItemSystem.AppSchema.ASSETItem.ASSET_IDColumn.ColumnName,
                         assetItemSystem.AppSchema.ASSETItem.ASSET_IDColumn.ColumnName);
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
        /// Set the Default dates
        /// </summary>
        private void SetDefaultDates()
        {
            dtRenewalDate.Properties.MinValue = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtRenewalDate.Properties.MaxValue = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            dtRenewalDate.DateTime = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
        }

        /// <summary>
        /// Set the Caption
        /// </summary>
        private void Caption()
        {
            ucCaptionPanel1.Caption = Project;
            this.Text = RenewalId == 0 ? this.GetMessage(MessageCatalog.Asset.Insurance.INSURANCE_RENEWALADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.Insurance.INSURANCE_RENEWALEDIT_CAPTION);
        }

        /// <summary>
        /// Bind the renewal
        /// </summary>
        private void BindRenewal()
        {
            LoadAssetItem();
            LoadActiveAssets();
            LoadCashBankLedger();
            LoadExpenseLedgers();
            ConstructInsuranceRenewal();
        }

        /// <summary>
        /// Load asset Item by group 
        /// </summary>
        private void LoadAssetByItemId()
        {
            try
            {
                using (AssetItemSystem assetItemSystem = new AssetItemSystem())
                {
                    int ItemId = gvRenewal.GetFocusedRowCellValue(ColItemName) != null ? this.UtilityMember.NumberSet.ToInteger(gvRenewal.GetFocusedRowCellValue(ColItemName).ToString()) : 0;
                    if (dtActiveAsset != null && dtActiveAsset.Rows.Count > 0 && ItemId > 0)
                    {
                        DataView dvAssetItem = new DataView(dtActiveAsset);
                        dvAssetItem.RowFilter = "ITEM_ID=" + ItemId + "";

                        this.UtilityMember.ComboSet.BindRepositoryItemCheckBoxGridLookUpEdit(rccmbAssetId, dvAssetItem.ToTable(), assetItemSystem.AppSchema.ASSETItem.ASSET_IDColumn.ColumnName,
                         assetItemSystem.AppSchema.ASSETItem.ASSET_IDColumn.ColumnName);
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
        /// load cash bank ledger
        /// </summary>
        /// <param name="glkpLedger"></param>
        private void LoadCashBankLedger()
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    ledgerSystem.ProjectId = ProjectId;
                    resultArgs = ledgerSystem.FetchCashBankLedger();
                    glkpLedger.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpLedger, resultArgs.DataSource.Table, ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
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
        /// Clear the controls
        /// </summary>
        private void ClearControls()
        {
            if (RenewalId == 0)
            {
                txtVoucherNo.Text = string.Empty;
                dtRenewalDate.DateTime = DateTime.Now;
                glkpLedger.EditValue = 0;
                glkpExpenceLedger.EditValue = 0;
                txtNameAddress.Text = string.Empty;
                txtNarration.Text = string.Empty;
                LoadAssetItem();
                InsuranceId = 0;
                ConstructInsuranceRenewal();
            }
            else
            {
                if (this.UIAppSetting.UITransClose == "1")
                {
                    this.Close();
                }
            }
        }

        /// <summary>
        /// Validate the Controls
        /// </summary>
        /// <returns></returns>
        private bool IsValidateRenewal()
        {
            bool isRenewal = true;
            try
            {
                if (string.IsNullOrEmpty(dtRenewalDate.Text))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Insurance.RENEWAL_DATE_EMPTY));
                    this.SetBorderColor(dtRenewalDate);
                    isRenewal = false;
                    dtRenewalDate.Focus();
                }
                else if (string.IsNullOrEmpty(glkpExpenceLedger.Text))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AMCVoucher.AMC_EXPENSE_LEDGER_EMPTY));
                    this.SetBorderColorForGridLookUpEdit(glkpExpenceLedger);
                    isRenewal = false;
                    glkpExpenceLedger.Focus();
                }
                else if (string.IsNullOrEmpty(glkpLedger.Text))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.InwardVoucher.PURCHASE_ASSET_CASH_BANK_LEDGER_EMPTY));
                    this.SetBorderColorForGridLookUpEdit(glkpLedger);
                    isRenewal = false;
                    glkpLedger.Focus();
                }
                else if (!IsValidRenewalGrid())
                {
                    isRenewal = false;
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally
            {
            }
            return isRenewal;
        }

        /// <summary>
        /// Validate the Controls
        /// </summary>
        /// <returns></returns>
        private bool IsValidRenewalGrid()
        {

            bool isValid = true;
            try
            {
                DataTable dtRenewal = gcRenewal.DataSource as DataTable;
                int ItemId = 0;
                string AssetId = string.Empty;
                string DueDate = string.Empty;
                int RowPosition = 0;
                double RenAmount = 0;
                DataView dv = new DataView(dtRenewal);
                dv.RowFilter = "(ITEM_ID>0)";
                gvRenewal.FocusedColumn = ColItemName;
                if (dv.Count > 0)
                {
                    foreach (DataRowView drRenewal in dv)
                    {
                        ItemId = this.UtilityMember.NumberSet.ToInteger(drRenewal[appSchema.AppSchema.ASSETItem.ITEM_IDColumn.ColumnName].ToString());
                        AssetId = drRenewal[appSchema.AppSchema.InsuranceRenewalDetails.ASSET_IDColumn.ColumnName].ToString();
                        DueDate = drRenewal[appSchema.AppSchema.InsuranceRenewalDetails.DUE_DATEColumn.ColumnName].ToString();
                        RenAmount = this.UtilityMember.NumberSet.ToDouble(drRenewal[appSchema.AppSchema.InsuranceRenewalDetails.RENEWAL_AMOUNTColumn.ColumnName].ToString());
                        if (ItemId == 0)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Insurance.ASSET_NAME));
                            gvRenewal.FocusedColumn = colItemId;
                            isValid = false;
                        }
                        else if (string.IsNullOrEmpty(AssetId))
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Insurance.ASSET_ID_EMPTY));
                            gvRenewal.FocusedColumn = ColAssetID;
                            isValid = false;
                        }
                        else if (string.IsNullOrEmpty(DueDate))
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Insurance.DUE_DATE_EMPTY));
                            gvRenewal.FocusedColumn = ColDueDate;
                            isValid = false;
                        }
                        else if (RenAmount == 0 || RenAmount < 0)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Insurance.RENEWAL_AMOUNT_EMPTY));
                            gvRenewal.FocusedColumn = ColRenAmount;
                            isValid = false;
                        }
                        else if (!ValidateDate())
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Insurance.VALIDATE_DUE_DATE));
                            gvRenewal.FocusedColumn = ColDueDate;
                            isValid = false;
                        }
                        if (!isValid) break;
                        RowPosition = RowPosition + 1;
                    }
                }
                else
                {
                    isValid = false;
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Insurance.ASSET_NAME));
                    gvRenewal.FocusedColumn = colItemId;
                }
                if (!isValid)
                {
                    gvRenewal.CloseEditor();
                    gvRenewal.FocusedRowHandle = gvRenewal.GetRowHandle(RowPosition);
                    gvRenewal.ShowEditor();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
                isValid = false;
            }
            return isValid;
        }

       
        #endregion

    

        
    }
}