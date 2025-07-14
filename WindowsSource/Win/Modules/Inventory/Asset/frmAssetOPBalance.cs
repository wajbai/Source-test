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
using Bosco.Model;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using AcMEDSync.Model;
using Bosco.Model.UIModel;
using Bosco.Model.Inventory;
using Bosco.Utility.ConfigSetting;
using ACPP.Modules.Transaction;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using Bosco.DAO.Schema;
using DevExpress.XtraGrid;
using ACPP.Modules.Master;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmAssetOPBalance : frmFinanceBaseAdd
    {
        #region Properties
        private DataTable dtAssetItem { get; set; }
        public event EventHandler UpdateHeld;
        AppSchemaSet.ApplicationSchemaSet appSchema = new AppSchemaSet.ApplicationSchemaSet();
        ResultArgs resultArgs = new ResultArgs();
        int InwardId = 0;
        int cashLedgerId = 0;
        private const string LEDGER_ID = "LEDGER_ID";
        private const string ITEM_ID = "ITEM_ID";
        private const string QUANTITY = "QUANTITY";
        private const string AMOUNT = "AMOUNT";
        private const string CUSTODIANS_ID = "CUSTODIANS_ID";
        bool isMouseClicked = false;
        public int tmpQuantity { get; set; }
        public int AssetTempQuantity { get; set; }
        public bool isvalidQty = true;
        private int PrvQty { get; set; }
        private int AvailQty { get; set; }

        private int AccountLedgerId
        {
            get
            {
                int ledgerId = 0;
                if (ItemId > 0)
                {
                    DataRowView dv = rglkpAssetName.GetRowByKeyValue(ItemId) as DataRowView;
                    if (dv != null)
                        ledgerId = this.UtilityMember.NumberSet.ToInteger(dv.Row["ACCOUNT_LEDGER_ID"].ToString());
                }
                return ledgerId;
            }
        }

        public int ItemId
        {
            get { return gvOPBalance.GetFocusedRowCellValue(colAssItemID) != null ? this.UtilityMember.NumberSet.ToInteger(gvOPBalance.GetFocusedRowCellValue(colAssItemID).ToString()) : 0; }
        }

        public int LedgerId
        {
            get
            {
                int id = 0;
                id = gvOPBalance.GetRowCellValue(gvOPBalance.FocusedRowHandle, colAccountLedgerId) != null ? this.UtilityMember.NumberSet.ToInteger(gvOPBalance.GetRowCellValue(gvOPBalance.FocusedRowHandle, colAccountLedgerId).ToString()) : 0;
                return id;
            }
        }

        public int Quantity
        {
            get { return gvOPBalance.GetFocusedRowCellValue(colQuantity) != null ? this.UtilityMember.NumberSet.ToInteger(gvOPBalance.GetFocusedRowCellValue(colQuantity).ToString()) : 0; }
        }
        public int AssetName
        {
            get { return gvOPBalance.GetFocusedRowCellValue(colAssetName) != null ? this.UtilityMember.NumberSet.ToInteger(gvOPBalance.GetFocusedRowCellValue(colQuantity).ToString()) : 0; }
        }
        public int InOutDetailId
        {
            get { return gvOPBalance.GetFocusedRowCellValue(colInoutDetailId) != null ? this.UtilityMember.NumberSet.ToInteger(gvOPBalance.GetFocusedRowCellValue(colInoutDetailId).ToString()) : 0; }
        }
        private int projectId = 0;
        public int ProjectId
        {
            get
            {
                projectId = glkpProject.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
                return projectId;
            }
            set
            {
                projectId = value;
            }
        }

        #endregion

        #region Constructor
        public frmAssetOPBalance()
        {
            InitializeComponent();
            RealColumnEditQuantity();
        }
        #endregion

        #region Events
        private void frmAssetOPBalance_Load(object sender, EventArgs e)
        {
            // "Opening Balance as on : " +
            //lcgOPeningBalance.Text = "Opening Balance ( As On ) : " +
            lcgOPeningBalance.Text = this.GetMessage(MessageCatalog.Asset.AssetOpenningBalance.ASSET_OPBAL_ASON_INFO) +
            UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false).ToString("d");
            LoadProject();
            ConstructPurchaseVoucherDetail();
            LoadAssetItem();
            AssignValues();
            colAvailableQuantity.Visible = false;
            gcOPBalance.Focus();
            gvOPBalance.MoveFirst();
            //10/12/2024 -- chinna

            gvOPBalance.FocusedColumn = colBalanceDate;
            //gvOPBalance.FocusedColumn = colAssetName;
            calculateOPBalance();
            calculateDifference();
            SetTitle();

            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                ApplyRights();
            }
            else
            {
                glkpProject.Properties.Buttons[1].Visible = true;
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataTable dtFilteredRows = new DataTable();
            try
            {
                if (ValidOPBalanceDetails())
                {
                    //DialogResult result = this.ShowConfirmationMessage("Ledger Opening Balance will be updated with the New Asset Opening Balance",
                    DialogResult result = this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Asset.AssetOpenningBalance.ASSET_LEDGER_OPENNING_BAL_INFO),
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (result == DialogResult.OK)
                    {
                        // This is to update the Ledger Balance
                        resultArgs = UpdateLedgerOPBalance();
                        if (resultArgs.Success)
                        {
                            using (AssetInwardOutwardSystem inwardvouchersystem = new AssetInwardOutwardSystem())
                            {
                                inwardvouchersystem.InoutId = InwardId;
                                inwardvouchersystem.ProjectId = ProjectId;
                                inwardvouchersystem.ItemDetailId = ItemId;
                                inwardvouchersystem.InOutDate = this.UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false);
                                inwardvouchersystem.Flag = AssetInOut.OP.ToString();
                                DataTable dtSource = gcOPBalance.DataSource as DataTable;
                                dtSource.AcceptChanges(); // To verify the Null values 
                                dtFilteredRows = dtSource.Rows.Count > 1 && dtSource != null ? dtSource.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull)).CopyToDataTable() : null;
                                inwardvouchersystem.TotalAmount = UtilityMember.NumberSet.ToDouble(dtSource.Compute("SUM(AMOUNT)", string.Empty).ToString());
                                inwardvouchersystem.TotalDepreciationAmount = UtilityMember.NumberSet.ToDouble(dtSource.Compute("SUM(DEPRECIATION_AMOUNT)", string.Empty).ToString());
                                inwardvouchersystem.dtinoutword = dtFilteredRows;
                                //   inwardvouchersystem.dtinoutword = dtSource;

                                resultArgs = inwardvouchersystem.SaveAssetInwardOutward();
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                                    ClearAssetCommonProperties();
                                    gvOPBalance.FocusedColumn = colAssetName;
                                    //   gvOPBalance.FocusedColumn = gvOPBalance.Columns["0"];

                                    //List<Form> openForms = new List<Form>();

                                    //foreach (Form f in Application.OpenForms)
                                    //    openForms.Add(f);

                                    //foreach (Form f in openForms)
                                    //{ 
                                    //    if (f.Name != "frmAssetOPBalance")
                                    //        f.Close();
                                    //}
                                    //if (result == DialogResult.OK)
                                    //// if (colQuantity.FieldName==string.Empty.ToString())
                                    //    if(frmAssetOPBalance.ActiveForm=Close)
                                    // {
                                    // this.Close();

                                    //  }

                                    ///  LoadAssetOpeningBalance();
                                    // this.Close();
                                    //ClearControls();
                                }
                                else
                                {
                                    MessageRender.ShowMessage(resultArgs.Message);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message + System.Environment.NewLine + Ex.Source);
            }
            finally
            {
            }
        }

        private void LoadAssetOpeningBalance()
        {
            frmAssetOPBalance objOPBalance = new frmAssetOPBalance();
            objOPBalance.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            ClearAssetCommonProperties();
            AssignValues();
            gcOPBalance.Focus();
            gvOPBalance.MoveFirst();
            // 10/12/2024 -- chinna
            gvOPBalance.FocusedColumn = colBalanceDate;
            //gvOPBalance.FocusedColumn = colAssetName;
            calculateOPBalance();
            calculateDifference();
        }

        private void gcOPBalance_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                string Amount = string.Empty;
                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control
                && (gvOPBalance.FocusedColumn == colQuantity))
                {
                    gvOPBalance.PostEditor();
                    gvOPBalance.UpdateCurrentRow();
                    gvOPBalance.SetFocusedRowCellValue(colAccountLedgerId, this.AccountLedgerId);
                    Amount = gvOPBalance.GetFocusedRowCellValue(colAmount) != null ? gvOPBalance.GetFocusedRowCellValue(colAmount).ToString() : string.Empty;
                    int item = this.UtilityMember.NumberSet.ToInteger(gvOPBalance.GetFocusedRowCellValue(colAssItemID) != null ? gvOPBalance.GetFocusedRowCellValue(colAssItemID).ToString() : string.Empty);
                    //  string salvagevalue = gvOPBalance.GetFocusedRowCellValue(colSalvageValue) != null ? gvOPBalance.GetFocusedRowCellValue(colSalvageValue).ToString() : string.Empty;

                    //if (item > 0)
                    //{
                    if (gvOPBalance.FocusedColumn == colAssetName)
                    {
                        gvOPBalance.FocusedColumn = gvOPBalance.Columns.ColumnByName(colAssetName.Name);
                        gvOPBalance.ShowEditor();
                    }
                    AssignLedgerDetails(ItemId);
                    //  tmpQuantity = 0;

                    if (gvOPBalance.FocusedColumn == colQuantity)
                    {
                        AssetGenerationList();
                    }
                    if (item > 0 && this.Quantity > 0)
                    {
                        //if (salvagevalue == string.Empty)
                        //{
                        //    gvOPBalance.FocusedColumn = colSalvageValue;
                        //}
                        //else
                        if (gvOPBalance.IsLastRow)
                        {
                            DataTable dtCashTransaction = gcOPBalance.DataSource as DataTable;
                            dtCashTransaction.Rows.Add(dtCashTransaction.NewRow());
                            gcOPBalance.DataSource = dtCashTransaction;
                            // 10/12/2024 -- Chinna
                            gvOPBalance.FocusedColumn = colBalanceDate;
                            //gvOPBalance.FocusedColumn = colAssName;
                            //  gvOPBalance.MoveNext();
                            gvOPBalance.ShowEditor();
                        }
                        else
                        {
                            //gvOPBalance.MoveNext();
                            //gvOPBalance.ShowEditor();
                            DataTable dtCashTransaction = gcOPBalance.DataSource as DataTable;
                            gcOPBalance.DataSource = dtCashTransaction;

                            // 10/12/2024 -- Chinna
                            gvOPBalance.FocusedColumn = colBalanceDate;
                            //gvOPBalance.FocusedColumn = colAssetName;

                            gvOPBalance.MoveNext();
                        }

                        //gvOPBalance.UpdateCurrentRow();
                        // gvOPBalance.FocusedColumn = colSalvageValue;

                        // 10/12/2024 -- Chinna
                        gvOPBalance.FocusedColumn = colBalanceDate;
                        //gvOPBalance.FocusedColumn = colAssetName;

                        gvOPBalance.MoveNext();
                        gvOPBalance.FocusedColumn = gvOPBalance.Columns["0"];
                        gvOPBalance.ShowEditor();
                        calculateDifference();
                    }
                }
                // 10/12/2024 - Chinna
                //else if (gvOPBalance.IsFirstRow && gvOPBalance.FocusedColumn == colAssetName && e.Shift && e.KeyCode == Keys.Tab)
                //{
                //    gvOPBalance.CloseEditor();
                //    e.Handled = true;
                //    e.SuppressKeyPress = true;
                //    // btnSave.Focus();
                //    glkpProject.Focus();
                //}
                else if (gvOPBalance.IsFirstRow && gvOPBalance.FocusedColumn == colBalanceDate && e.Shift && e.KeyCode == Keys.Tab)
                {
                    gvOPBalance.CloseEditor();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    // btnSave.Focus();
                    glkpProject.Focus();
                }
                //calculateOPBalance();
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void AssetGenerationList()
        {
            int calculateQty = 0;
            //gvOPBalance.FocusedColumn = gvOPBalance.Columns.ColumnByName(colQuantity.Name);
            gvOPBalance.ShowEditor();
            int rowid = gvOPBalance.GetFocusedDataSourceRowIndex();
            int is_insurance = 0;
            int is_amc = 0;
            DataRow dr;
            DataTable dtFilterdAsset = new DataTable();
            DataView dvAssetItem = new DataView(dtAssetItem);
            string AssetName = gvOPBalance.GetFocusedRowCellValue(colAssItemID) != null ? gvOPBalance.GetFocusedRowCellValue(colAssetName).ToString() : string.Empty;
            string Amount = gvOPBalance.GetFocusedRowCellValue(colAmount) != null ? gvOPBalance.GetFocusedRowCellValue(colAmount).ToString() : string.Empty;

            if (!string.IsNullOrEmpty(gvOPBalance.GetFocusedRowCellValue(colAssItemID).ToString()))
            //&& !string.IsNullOrEmpty(gvOPBalance.GetFocusedRowCellValue(colQuantity).ToString()))
            {
                gvOPBalance.CloseEditor();
                //e.Handled = true;
                //e.SuppressKeyPress = true;
                dvAssetItem.RowFilter = "ITEM_ID=" + ItemId;
                dtFilterdAsset = dvAssetItem.ToTable();
                if (dtFilterdAsset != null && dtFilterdAsset.Rows.Count > 0)
                {
                    dr = dtFilterdAsset.Rows[0];
                    if (Quantity > 0)
                    {
                        frmAssetItemList AssetItemList = new frmAssetItemList(ItemId, this.Quantity, rowid, InOutDetailId, AssetInOut.OP, ProjectId, glkpProject.Text);
                        AssetItemList.ShowDialog();
                        if (AssetItemList.Dialogresult == DialogResult.OK)
                        {
                            if (AssetItemList.Quantity > 0)
                            {
                                gvOPBalance.SetFocusedRowCellValue(colQuantity, AssetItemList.Quantity);
                                tmpQuantity = AssetItemList.Quantity;

                                int PrvQty = gvOPBalance.GetRowCellValue(gvOPBalance.FocusedRowHandle, colQuantity) != null ? this.UtilityMember.NumberSet.ToInteger(gvOPBalance.GetRowCellValue(gvOPBalance.FocusedRowHandle, colQuantity).ToString()) : 0;

                            }
                            else if (AssetItemList.Quantity == 0)
                            {
                                gvOPBalance.SetFocusedRowCellValue(colQuantity, AssetItemList.Quantity);
                                gvOPBalance.SetFocusedRowCellValue(colAmount, AssetItemList.Amount);
                                // New 
                                gvOPBalance.SetFocusedRowCellValue(colDepreciationAmount, AssetItemList.DepreciationAmount);
                            }

                            decimal amount = AssetItemList.Amount;
                            decimal depreciationAmount = AssetItemList.DepreciationAmount;
                            if (amount > 0 || depreciationAmount > 0)
                            {
                                gvOPBalance.SetFocusedRowCellValue(colAmount, amount.ToString());
                                gvOPBalance.SetFocusedRowCellValue(colDepreciationAmount, depreciationAmount.ToString());
                                gvOPBalance.UpdateTotalSummary();
                            }
                        }
                        else
                        {
                            if (InwardId == 0)
                            {
                                gvOPBalance.SetFocusedRowCellValue(colQuantity, tmpQuantity);
                                gvOPBalance.SetFocusedRowCellValue(colAvailableQuantity, BindAvailQty());
                            }
                            else
                            {
                                gvOPBalance.SetFocusedRowCellValue(colQuantity, tmpQuantity);
                                //gvOPBalance.SetFocusedRowCellValue(colAvailableQty, AvailQty);
                            }
                            if (AssetItemList.Amount == 0 && tmpQuantity == 0 && SettingProperty.AssetListCollection.ContainsKey(rowid))
                                SettingProperty.AssetListCollection.Remove(rowid);
                        }
                    }
                    //else
                    //{
                    //    //gvOPBalance.SetFocusedRowCellValue(colQuantity, 0);
                    //    //gvOPBalance.FocusedColumn = colQuantity;
                    //}
                }
                gvOPBalance.UpdateCurrentRow();
                //   gvOPBalance.FocusedColumn = colAssItemID;


                if (gvOPBalance.FocusedColumn != colQuantity)
                {
                    gvOPBalance.FocusedColumn = gvOPBalance.Columns.ColumnByName(colAssetName.Name);
                    gvOPBalance.ShowEditor();

                }
                //  gvOPBalance.MoveNext();
                //  gvOPBalance.FocusedColumn = gvOPBalance.Columns["0"];

            }
            else if (gvOPBalance.IsLastRow)
            {
                btnSave.Focus();
            }

        }

        private void AssignLedgerDetails(int itmID)
        {
            using (AssetInwardOutwardSystem inwardoutward = new AssetInwardOutwardSystem())
            {
                resultArgs = inwardoutward.FetchAccountLedgerDetailsByItem(itmID, ProjectId);
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    string LedgerName = resultArgs.DataSource.Table.Rows[0]["LEDGER_NAME"].ToString();
                    string Amount = resultArgs.DataSource.Table.Rows[0]["LED_AMOUNT"].ToString();
                    string LedId = resultArgs.DataSource.Table.Rows[0]["LEDGER_ID"].ToString();

                    gvOPBalance.SetFocusedRowCellValue(colLedgername, LedgerName);
                    gvOPBalance.SetFocusedRowCellValue(colLedgerOPAmount, Amount);
                    gvOPBalance.SetFocusedRowCellValue(colLedgerId, UtilityMember.NumberSet.ToInteger(LedId));

                    gvOPBalance.UpdateCurrentRow();

                    calculateOPBalance();
                }

            }
        }

        private void calculateDifference()
        {
            gvOPBalance.UpdateCurrentRow();
            DataTable dtDetails = gcOPBalance.DataSource as DataTable;
            if (dtDetails != null && dtDetails.Rows.Count > 0)
            {
                dtDetails.AsDataView().Sort = "LEDGER_NAME ASC";
                string PrvName = string.Empty;
                double LedAmount = 0;

                foreach (DataRow dr in dtDetails.Rows)
                {
                    if (dr.RowState != DataRowState.Deleted)
                    {
                        if (PrvName != dr["LEDGER_NAME"].ToString())
                        {
                            LedAmount += UtilityMember.NumberSet.ToDouble(dr["LED_AMOUNT"].ToString());
                        }
                        PrvName = dr["LEDGER_NAME"].ToString();
                    }
                }
                double tempOPAmt = this.UtilityMember.NumberSet.ToDouble(dtDetails.Compute("SUM(AMOUNT)", string.Empty).ToString());
                colLedgerOPAmount.SummaryItem.DisplayFormat = this.UtilityMember.NumberSet.ToCurrency(tempOPAmt).ToString();

                string calcitmAmount = dtDetails.Compute("SUM(AMOUNT)", string.Empty).ToString();
                double ItmAmount = UtilityMember.NumberSet.ToDouble(calcitmAmount);

                if (ItmAmount > LedAmount)
                {
                    lblDiffBalance.Text = "<b><color=Red>" + UtilityMember.NumberSet.ToNumber(ItmAmount - LedAmount).ToString() + "</color></b>";
                }
                else
                {
                    lblDiffBalance.Text = "<b><color=Green>" + UtilityMember.NumberSet.ToNumber(LedAmount - ItmAmount).ToString() + "</color></b>";
                }
            }
        }

        private void calculateOPBalance()
        {
            gvOPBalance.UpdateCurrentRow();
            DataTable dtDetails = gcOPBalance.DataSource as DataTable;
            if (dtDetails != null && dtDetails.Rows.Count > 0)
            {
                dtDetails.AsDataView().Sort = "LEDGER_NAME ASC";
                string PrvName = string.Empty;
                double LedAmount = 0;

                foreach (DataRow dr in dtDetails.Rows)
                {
                    if (dr.RowState != DataRowState.Deleted)
                    {
                        if (PrvName != dr["LEDGER_NAME"].ToString())
                        {
                            LedAmount += UtilityMember.NumberSet.ToDouble(dr["LED_AMOUNT"].ToString());
                        }
                        PrvName = dr["LEDGER_NAME"].ToString();
                    }
                }
                colLedgerOPAmount.SummaryItem.DisplayFormat = this.UtilityMember.NumberSet.ToCurrency(LedAmount).ToString();
            }
        }

        public ResultArgs UpdateLedgerOPBalance()
        {
            gvOPBalance.UpdateCurrentRow();
            DataTable dtDetails = gcOPBalance.DataSource as DataTable;
            if (dtDetails != null && dtDetails.Rows.Count > 0)
            {
                dtDetails.AsDataView().Sort = "LEDGER_NAME ASC";
                string PrvName = string.Empty;
                string LedID = string.Empty;
                double ItmAmount = 0;

                DataView dvDetails = dtDetails.AsDataView();
                foreach (DataRow dr in dtDetails.Rows)
                {
                    if (dr.RowState != DataRowState.Deleted)
                    {
                        if (!string.IsNullOrEmpty(dr["LEDGER_NAME"].ToString()) && !string.IsNullOrEmpty(dr["AMOUNT"].ToString()))
                        {
                            if (PrvName != dr["LEDGER_NAME"].ToString())
                            {
                                LedID = dr["LEDGER_ID"].ToString();
                                string amt = dtDetails.Compute("SUM(AMOUNT)", "LEDGER_ID=" + LedID).ToString();
                                ItmAmount = UtilityMember.NumberSet.ToDouble(amt);
                                resultArgs = UpdateOpBalance(LedID, ItmAmount);
                                if (!resultArgs.Success)
                                    break;
                            }
                            PrvName = dr["LEDGER_NAME"].ToString();
                        }
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs UpdateOpBalance(string LedId, double Amount)
        {
            if (LedId != "0")
            {
                using (MappingSystem mapsystem = new MappingSystem())
                {
                    mapsystem.ProjectId = ProjectId;
                    mapsystem.LedgerId = UtilityMember.NumberSet.ToInteger(LedId);
                    resultArgs = mapsystem.MapProjectLedger();

                    if (resultArgs != null && resultArgs.Success)
                    {
                        using (BalanceSystem balanceSystem = new BalanceSystem())
                        {
                            if (balanceSystem.HasBalance(ProjectId, UtilityMember.NumberSet.ToInteger(LedId)))
                            {
                                resultArgs = balanceSystem.UpdateOpBalance(this.AppSetting.BookBeginFrom, ProjectId,
                                       UtilityMember.NumberSet.ToInteger(LedId), Amount, TransactionMode.DR.ToString(), TransactionAction.EditBeforeSave);
                            }
                            else
                            {
                                resultArgs = balanceSystem.UpdateOpBalance(this.AppSetting.BookBeginFrom, ProjectId,
                                       UtilityMember.NumberSet.ToInteger(LedId), Amount, TransactionMode.DR.ToString(), TransactionAction.New);
                            }

                        }
                    }
                }

            }
            return resultArgs;
        }

        private void rglkpAssetName_Validating(object sender, CancelEventArgs e)
        {
            int itemId = 0;
            GridLookUpEdit gridLKPEdit = sender as GridLookUpEdit;
            if (gridLKPEdit.EditValue != null)
            {
                DataRowView drv = gridLKPEdit.GetSelectedDataRow() as DataRowView;

                if (drv != null)
                {
                    itemId = this.UtilityMember.NumberSet.ToInteger(drv[ITEM_ID].ToString());
                    gvOPBalance.SetFocusedRowCellValue(colAssItemID, itemId);
                    gvOPBalance.UpdateCurrentRow();
                }
            }
        }

        private void rglkpAssetName_EditValueChanged(object sender, EventArgs e)
        {
            if (isMouseClicked)
            {
                SendKeys.Send("{tab}");
                isMouseClicked = false;
            }
        }

        private void rglkpAssetName_MouseDown(object sender, MouseEventArgs e)
        {
            //To Identify the Mouse Click Event
            if (e.Button == MouseButtons.Left)
                isMouseClicked = true;
        }

        private void rtxtQuantity_Enter(object sender, EventArgs e)
        {
            // if (tmpQuantity == 0)
            //   AssignLedgerDetails(ItemId);
            tmpQuantity = Quantity;
        }

        private void frmAssetOPBalance_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClearAssetCommonProperties();
        }

        private void ucAssetVoucherShortcuts1_AssetItemClicked(object sender, EventArgs e)
        {
            frmAssetItemAdd itemAdd = new frmAssetItemAdd();
            itemAdd.ShowDialog();
        }

        private void ucAssetVoucherShortcuts1_LocationMappingClicked(object sender, EventArgs e)
        {
            frmMapLocation maplocation = new frmMapLocation();
            maplocation.ShowDialog();
        }

        private void ucAssetVoucherShortcuts1_ProjectClicked(object sender, EventArgs e)
        {
            frmProjectSelection projectSelection = new frmProjectSelection(Bosco.Utility.ProjectSelection.DisableVoucherSelectionMethod);
            projectSelection.ShowDialog();
        }

        private void frmAssetOPBalance_KeyDown(object sender, KeyEventArgs e)
        {
            ProcessShortcutKeys(e);
        }

        private void ucAssetVoucherShortcuts1_ManufacturerClicked(object sender, EventArgs e)
        {
            frmVendorInfoAdd Manufacturer = new frmVendorInfoAdd(0, VendorManufacture.Manufacture);
            Manufacturer.ShowDialog();
        }

        private void ucAssetVoucherShortcuts1_CustodianClicked_1(object sender, EventArgs e)
        {
            frmCustodiansAdd custodianAdd = new frmCustodiansAdd();
            custodianAdd.ShowDialog();
        }

        private void ucAssetVoucherShortcuts1_LocationClicked_1(object sender, EventArgs e)
        {
            frmLocationsAdd locationAdd = new frmLocationsAdd();
            locationAdd.ShowDialog();
        }

        private void ucAssetVoucherShortcuts1_ConfigureClicked_1(object sender, EventArgs e)
        {
            frmAssetSettings assetsetting = new frmAssetSettings();
            assetsetting.ShowDialog();
        }

        private void gvOPBalance_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "ITEM_ID" && IsDuplicatedValue((sender as GridView), e.Column, e.Value))
            {
                (sender as GridView).CancelUpdateCurrentRow();
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

        private void LoadProject()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    resultArgs = mappingSystem.FetchProjectsLookup();
                    if (resultArgs.Success)
                    {
                        using (CommonMethod GetMethod = new CommonMethod())
                        {
                            this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProject, resultArgs.DataSource.Table, mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                            glkpProject.EditValue = this.AppSetting.UserProjectId;

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

        private int AvailableQuantity()
        {
            int availQty = 0;
            using (AssetInwardOutwardSystem inoutSystem = new AssetInwardOutwardSystem())
            {
                inoutSystem.ProjectId = ProjectId;
                inoutSystem.ItemId = ItemId;
                //    inoutSystem.LocationId = LocationId;
                availQty = inoutSystem.FetchAvailableQty();

            }
            return availQty;
        }

        private void LoadAssetItem()
        {
            try
            {
                using (AssetItemSystem assetItemSystem = new AssetItemSystem())
                {
                    resultArgs = assetItemSystem.FetchAssetItemDetails();
                    dtAssetItem = resultArgs.DataSource.Table;
                    if (dtAssetItem != null && dtAssetItem.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpAssetName, dtAssetItem, assetItemSystem.AppSchema.ASSETItem.ASSET_ITEMColumn.ColumnName, assetItemSystem.AppSchema.ASSETItem.ITEM_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void AssignValues()
        {
            using (AssetInwardOutwardSystem inwardsystem = new AssetInwardOutwardSystem())
            {
                inwardsystem.ProjectId = ProjectId;
                resultArgs = inwardsystem.FetchAssetOPDetailssByProjectID();
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    // InwardId = UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["IN_OUT_ID"].ToString());
                    gcOPBalance.DataSource = resultArgs.DataSource.Table;
                    gvOPBalance.AddNewRow();

                    DataTable dtList = resultArgs.DataSource.Table;
                    int itmID = 0;
                    int projID = 0;
                    int Qunty = 0;
                    int InOutDetID = 0;
                    int RNo = 0;
                    foreach (DataRow dritem in dtList.Rows)
                    {
                        itmID = UtilityMember.NumberSet.ToInteger(dritem["ITEM_ID"].ToString());
                        Qunty = UtilityMember.NumberSet.ToInteger(dritem["QUANTITY"].ToString());
                        InOutDetID = UtilityMember.NumberSet.ToInteger(dritem["IN_OUT_DETAIL_ID"].ToString());

                        if (Qunty > 0)
                        {
                            frmAssetItemList frmlist = new frmAssetItemList(itmID, Qunty, RNo, InOutDetID,
                                AssetInOut.OP, ProjectId);
                            frmlist.AssignItemDetails();
                        }
                        RNo++;
                    }

                }
                else
                {
                    ConstructPurchaseVoucherDetail();
                    LoadAssetItem();
                    // 10/12/2024 -- Chinna
                    //  gvOPBalance.FocusedColumn = colAssetName;
                    gvOPBalance.FocusedColumn = colBalanceDate;
                    gvOPBalance.ShowEditor();
                }
            }
        }

        public void SetTitle()
        {
            this.Text = "Opening Asset"; this.GetMessage(MessageCatalog.Asset.AssetOpenningBalance.ASSET_OPENNING_BALANCE_TITLE);
        }

        private void ClearControls()
        {
            ConstructPurchaseVoucherDetail();
            LoadAssetItem();
        }

        private bool ValidOPBalanceDetails()
        {
            bool isOPBalanceTrue = true;
            if (string.IsNullOrEmpty(glkpProject.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_TRANSACTION_DATE));
                isOPBalanceTrue = false;
                this.SetBorderColor(glkpProject);
                glkpProject.Focus();
            }
            else if (!IsValidTransGrid())
            {
                isOPBalanceTrue = false;
            }
            else if (!IsQuantitymatch())
            {
                //this.ShowMessageBox("Quantity Mismatch with the Asset Item List.");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetOpenningBalance.ASSET_AMOUNT_MISMATCH_INFO));
                isOPBalanceTrue = false;
            }
            return isOPBalanceTrue;
        }

        private bool IsValidTransGrid()
        {
            bool isValid = true;
            int RowPosition = 0;
            // int ItemId = 0;
            try
            {
                DataTable dtOPBalance = gcOPBalance.DataSource as DataTable;
                string AssetId = string.Empty;
                decimal Quantity = 0;
                decimal Amount = 0;
                decimal SalavageValue = 0;
                int ItemId = 0;
                DataView dvOPBalance = new DataView(dtOPBalance);
                dvOPBalance.RowFilter = "(ITEM_ID>0 OR QUANTITY>0)";
                gvOPBalance.FocusedColumn = colAssetName; // To remove the count and added the coloumn null values and grid empty
                //if (dvOPBalance.Count > 0)
                //{
                foreach (DataRowView drOPBalance in dvOPBalance)
                {
                    ItemId = drOPBalance[this.appSchema.ASSETItem.ITEM_IDColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(drOPBalance[this.appSchema.ASSETItem.ITEM_IDColumn.ColumnName].ToString()) : 0;
                    Quantity = drOPBalance[this.appSchema.AssetInOut.QUANTITYColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(drOPBalance[this.appSchema.AssetInOut.QUANTITYColumn.ColumnName].ToString()) : 0;
                    Amount = drOPBalance[this.appSchema.AssetInOut.AMOUNTColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToDecimal(drOPBalance[this.appSchema.AssetInOut.AMOUNTColumn.ColumnName].ToString()) : 0;
                    SalavageValue = drOPBalance[this.appSchema.AssetInOut.SALVAGE_VALUEColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToDecimal(drOPBalance[this.appSchema.AssetInOut.SALVAGE_VALUEColumn.ColumnName].ToString()) : 0;

                    if (ItemId == 0)
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.SalesVoucher.SALES_ASSET_NAME_EMPTY));
                        gvOPBalance.FocusedColumn = colAssetName;
                        isValid = false;
                    }
                    else if (Quantity == 0)
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.SalesVoucher.SALES_QUANTITY_EMPTY));
                        gvOPBalance.FocusedColumn = colQuantity;
                        isValid = false;
                    }
                    else if (Amount == 0)
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AMCVoucher.AMCVOUCHER_AMOUNT_EMPTY));
                        gvOPBalance.FocusedColumn = colAmount;
                        isValid = false;
                    }
                    //else if (SalavageValue == 0)
                    //{
                    //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.InwardVoucher.SALAVAGE_VALUE_EMPTY));
                    //    gvOPBalance.FocusedColumn = colSalvageValue;
                    //    isValid = false;
                    //}
                    if (!isValid) break;
                    RowPosition = RowPosition + 1;
                }
                //  }
                //else
                //{
                //    isValid = false;
                //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.SalesVoucher.SALES_ASSET_NAME_EMPTY));
                //    gvOPBalance.FocusedColumn = colAssetName;
                //}
                if (!isValid)
                {
                    gvOPBalance.CloseEditor();
                    gvOPBalance.FocusedRowHandle = gvOPBalance.GetRowHandle(RowPosition);
                    gvOPBalance.ShowEditor();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
                isValid = false;
            }
            finally { }
            return isValid;
        }

        private void RealColumnEditQuantity()
        {
            colQuantity.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditQuantity_EditValueChanged);
            this.gvOPBalance.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvOPBalance.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colAssetName)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvOPBalance.ShowEditorByMouse();
                    }));
                }
                else
                {
                    //  if(gvOPBalance.FocusedColumn == colQuantity.)
                    //    gvOPBalance.MoveFirst();
                    //   if (hitInfo.Column == null && hitInfo.Column == colQuantity)

                    //  gvOPBalance.MoveFirst();
                    //  gvOPBalance.FocusedColumn = gvOPBalance.Columns["0"];
                }
            };
        }

        void RealColumnEditQuantity_EditValueChanged(object sender, System.EventArgs e)
        {
            int tmpcalculateQty = 0;
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvOPBalance.PostEditor();
            gvOPBalance.UpdateCurrentRow();
            if (gvOPBalance.ActiveEditor == null)
            {
                gvOPBalance.ShowEditor();
            }
            if (InwardId == 0)
            {
                AvailQty = tmpcalculateQty = BindAvailQty();
                if (AvailQty == 0)
                {
                    // gvOPBalance.SetFocusedRowCellValue(colAvailableQuantity, Quantity);
                }
                else
                {
                    AvailQty = AvailQty - tmpQuantity;
                    AvailQty = AvailQty + Quantity;
                    gvOPBalance.SetFocusedRowCellValue(colAvailableQuantity, AvailQty == 0 ? tmpcalculateQty : AvailQty);
                }
            }
            //else
            //{
            //    AvailQty = BindAvailQty();
            //    int TotalQuantity = AvailQty - tmpQuantity;
            //    AvailQty = TotalQuantity + Quantity;
            //    gvOPBalance.SetFocusedRowCellValue(colAvailableQuantity, AvailQty);
            //}
            //if (rgVoucherType.SelectedIndex == 0)
            //    CalculateFirstRowValue();
        }

        private int BindAvailQty()
        {
            using (AssetInwardOutwardSystem inwardSystem = new AssetInwardOutwardSystem())
            {
                //this.ItemId=gvPurchase.GetFocusedRowCellValue(colAssItemID);
                inwardSystem.LocationId = 0;
                inwardSystem.ItemId = this.ItemId;
                inwardSystem.ProjectId = this.ProjectId;


                inwardSystem.InOutDate = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom.ToString(), false);
                AvailQty = inwardSystem.FetchAvailableQty();
                gvOPBalance.SetFocusedRowCellValue(colAvailableQuantity, AvailQty);
            }
            return AvailQty;
        }

        private void ProcessShortcutKeys(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode.Equals(Keys.F12))
            {
                frmAssetSettings setting = new frmAssetSettings();
                setting.ShowDialog();
            }
            else if (e.KeyData == (Keys.Alt | Keys.E))
            {
                frmAssetItemAdd itemadd = new frmAssetItemAdd();
                itemadd.ShowDialog();
                LoadAssetItem();
            }
            else if (e.KeyData == (Keys.Alt | Keys.F))
            {
                frmVendorInfoAdd Manufacturer = new frmVendorInfoAdd(0, VendorManufacture.Manufacture);
                Manufacturer.ShowDialog();
            }

            else if (e.KeyData == (Keys.Alt | Keys.L))
            {
                frmLocationsAdd locationAdd = new frmLocationsAdd();
                locationAdd.ShowDialog();
            }
            else if (e.KeyData == (Keys.Alt | Keys.O))
            {
                frmLedgerOptions ledgerOption = new frmLedgerOptions();
                ledgerOption.ShowDialog();
            }
            else if (e.KeyData == (Keys.Alt | Keys.U))
            {
                frmCustodiansAdd custodianAdd = new frmCustodiansAdd();
                custodianAdd.ShowDialog();
            }
            else if (e.KeyData == (Keys.Alt | Keys.R))
            {
                frmLedgerOptions LedgerDetailAdd = new frmLedgerOptions();
                LedgerDetailAdd.ShowDialog();
            }
            else if (e.KeyData == (Keys.Alt | Keys.T))
            {
                frmMapLocation maplocation = new frmMapLocation();
                maplocation.ShowDialog();
            }
            else if (e.KeyData == (Keys.Alt | Keys.A))
            {
                frmAssetItemAdd AssetItem = new frmAssetItemAdd();
                AssetItem.ShowDialog();
            }
            else if (e.KeyData == (Keys.Control | Keys.D))
            {
                DeleteOPTransaction();
            }
            else if (e.KeyData == (Keys.Control | Keys.L))
            {
                AssetGenerationList();
            }
            else if (e.KeyData.Equals(Keys.F5))
            {
                frmProjectSelection projectSelection = new frmProjectSelection(Bosco.Utility.ProjectSelection.DisableVoucherSelectionMethod);
                projectSelection.ShowDialog();

                if (projectSelection.ProjectName != string.Empty)
                {
                    ProjectId = projectSelection.ProjectId;
                    //projcetname = projectSelection.ProjectName;
                    //SetLoadDefault();
                    // Setdefaults();
                    LoadProject();
                    glkpProject.EditValue = ProjectId;
                }
            }
        }

        private void ConstructPurchaseVoucherDetail()
        {
            DataTable dtPurchaseVouhcerDetail = new DataTable();
            dtPurchaseVouhcerDetail.Columns.Add("BALANCE_OP_DATE", typeof(DateTime));
            dtPurchaseVouhcerDetail.Columns.Add("ITEM_ID", typeof(string));
            dtPurchaseVouhcerDetail.Columns.Add("QUANTITY", typeof(int));
            dtPurchaseVouhcerDetail.Columns.Add("AVAILABLE_QUANTITY", typeof(Int32));
            dtPurchaseVouhcerDetail.Columns.Add("RATE", typeof(decimal));
            dtPurchaseVouhcerDetail.Columns.Add("DEPRECIATION_AMOUNT", typeof(decimal));
            dtPurchaseVouhcerDetail.Columns.Add("AMOUNT", typeof(decimal));
            dtPurchaseVouhcerDetail.Columns.Add("IN_OUT_DETAIL_ID", typeof(int));
            dtPurchaseVouhcerDetail.Columns.Add("LEDGER_ID", typeof(int));
            dtPurchaseVouhcerDetail.Columns.Add("ACCOUNT_LEDGER_ID", typeof(int));
            dtPurchaseVouhcerDetail.Columns.Add("CHEQUE_NO", typeof(string));
            dtPurchaseVouhcerDetail.Columns.Add("LEDGER_NAME", typeof(string));
            dtPurchaseVouhcerDetail.Columns.Add("LED_AMOUNT", typeof(decimal));
            dtPurchaseVouhcerDetail.Columns.Add("MATERIALIZED_ON", typeof(DateTime));
            dtPurchaseVouhcerDetail.Columns.Add("SALVAGE_VALUE", typeof(decimal));
            dtPurchaseVouhcerDetail.Rows.Add(dtPurchaseVouhcerDetail.NewRow());
            gcOPBalance.DataSource = dtPurchaseVouhcerDetail;
        }

        bool IsDuplicatedValue(GridView currentView, GridColumn currentColumn, object someValue)
        {
            bool isexist = true;
            for (int i = 0; i < currentView.DataRowCount; i++)
            {
                if (currentView.GetRowCellValue(currentView.GetRowHandle(i), currentColumn).ToString() == someValue.ToString())
                {
                    gvOPBalance.FocusedRowHandle = i;

                    gvOPBalance.FocusedColumn = colQuantity;
                    tmpQuantity = this.UtilityMember.NumberSet.ToInteger(gvOPBalance.GetFocusedRowCellValue(colQuantity) != null ? gvOPBalance.GetFocusedRowCellValue(colQuantity).ToString() : string.Empty);

                    return true;
                }
            }
            return false;
        }

        #endregion

        private void rglkpAssetName_Enter(object sender, EventArgs e)
        {
            AssetTempQuantity = this.UtilityMember.NumberSet.ToInteger(gvOPBalance.GetFocusedRowCellValue(colQuantity) != null ? gvOPBalance.GetFocusedRowCellValue(colQuantity).ToString() : string.Empty);
            //  get { return gvOPBalance.GetFocusedRowCellValue(colQuantity) != null ? this.UtilityMember.NumberSet.ToInteger(gvOPBalance.GetFocusedRowCellValue(colQuantity).ToString()) : 0; }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            AssignValues();
            gcOPBalance.Focus();
            gvOPBalance.MoveFirst();
            // 10/12/2024 -- Chinna
            //gvOPBalance.FocusedColumn = colAssetName;
            gvOPBalance.FocusedColumn = colBalanceDate;
        }

        private void rbtnViewDetails_Click(object sender, EventArgs e)
        {
            //DataView dvAssetItem = new DataView(dtAssetItem);
            //DataTable dtFilterdAsset = new DataTable();
            //DataRow dr;
            //int rowid = gvOPBalance.GetVisibleIndex(gvOPBalance.FocusedRowHandle);
            //dtFilterdAsset = dvAssetItem.ToTable();

            //if (dtFilterdAsset != null && dtFilterdAsset.Rows.Count > 0)
            //{
            //    dr = dtFilterdAsset.Rows[0];
            //    if (Quantity > 0)
            //    {
            //        frmAssetItemList AssetItemList = new frmAssetItemList(ItemId, this.Quantity, rowid, InOutDetailId, AssetInOut.OP, ProjectId);
            //        AssetItemList.ShowDialog();
            //        if (AssetItemList.Dialogresult == DialogResult.OK)
            //        {
            //            if (AssetItemList.Quantity > 0)
            //            {
            //                gvOPBalance.SetFocusedRowCellValue(colQuantity, AssetItemList.Quantity);
            //                tmpQuantity = AssetItemList.Quantity;

            //                int PrvQty = gvOPBalance.GetRowCellValue(gvOPBalance.FocusedRowHandle, colQuantity) != null ? this.UtilityMember.NumberSet.ToInteger(gvOPBalance.GetRowCellValue(gvOPBalance.FocusedRowHandle, colQuantity).ToString()) : 0;
            //            }
            //            else if (AssetItemList.Quantity == 0)
            //            {
            //                gvOPBalance.SetFocusedRowCellValue(colQuantity, AssetItemList.Quantity);
            //                gvOPBalance.SetFocusedRowCellValue(colAmount, AssetItemList.Amount);
            //            }

            //            decimal amount = AssetItemList.Amount;
            //            if (amount > 0)
            //            {
            //                gvOPBalance.SetFocusedRowCellValue(colAmount, amount.ToString());
            //                gvOPBalance.UpdateTotalSummary();
            //            }
            //        }
            //        else
            //        {
            //            if (InwardId == 0)
            //            {
            //                gvOPBalance.SetFocusedRowCellValue(colQuantity, AssetTempQuantity);
            //                gvOPBalance.SetFocusedRowCellValue(colAvailableQuantity, BindAvailQty());
            //            }
            //            else
            //            {
            //                gvOPBalance.SetFocusedRowCellValue(colQuantity, tmpQuantity);
            //            }
            //        }
            //        if (gvOPBalance.IsLastRow)
            //        {
            //            gvOPBalance.AddNewRow();
            //        }
            //        else
            //        {
            //            gvOPBalance.MoveNext();
            //        }

            //        gvOPBalance.UpdateCurrentRow();
            //        gvOPBalance.FocusedColumn = colAssetName;
            //        gvOPBalance.ShowEditor();
            //    }
            //    else
            //    {
            //        gvOPBalance.SetFocusedRowCellValue(colQuantity, 0);
            //    }
            //}
            AssetGenerationList();
            gvOPBalance.MoveNext();
            //  gvOPBalance.FocusedColumn = gvOPBalance.Columns["0"];
            gvOPBalance.FocusedColumn = gvOPBalance.Columns["0"];
        }

        private void gvOPBalance_CellValueChanging_1(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "ITEM_ID" && IsDuplicatedValue((sender as GridView), e.Column, e.Value))
            {
                (sender as GridView).CancelUpdateCurrentRow();
            }
        }

        private void rbtnDeleteTrans_Click(object sender, EventArgs e)
        {
            DeleteOPTransaction();
        }

        private void DeleteOPTransaction()
        {
            try // it has to set as a order
            {
                if (!string.IsNullOrEmpty(gvOPBalance.GetFocusedRowCellValue(colAssItemID).ToString()))
                {
                    if (gvOPBalance.RowCount > 1)
                    {
                        //if (gvOPBalance.FocusedRowHandle == 0)
                        //{
                        int rowNo = 0;
                        frmAssetItemList AssetItemList;
                        int soldItemcount = 0;
                        if (gvOPBalance.FocusedRowHandle != GridControl.NewItemRowHandle)
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                using (AssetInwardOutwardSystem inoutwardSystem = new AssetInwardOutwardSystem())
                                {
                                    bool InsuranceMade = false;
                                    bool SalesMade = false;
                                    int insurance = inoutwardSystem.CheckInsuranceByItemID(InwardId, ProjectId, InOutDetailId, ItemId);
                                    soldItemcount = inoutwardSystem.CheckSoldAssetIdByItemID(InwardId, InOutDetailId, ItemId);
                                    if (insurance > 0)
                                    {
                                        //if (this.ShowConfirmationMessage("Insurance is made for this Entry. Do you want to delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Asset.AssetOpenningBalance.ASSET_INS_MADE_ENTRY_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            InsuranceMade = true;
                                            if (soldItemcount > 0)
                                            {
                                                //if (this.ShowConfirmationMessage("Sold Item can not be deleted.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Asset.AssetOpenningBalance.ASSET_SOLD_ITEM_CANNOT_DELETE_INFO), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                                {
                                                    SalesMade = true;
                                                    AssetItemList = new frmAssetItemList(ItemId, this.Quantity, rowNo, InOutDetailId, AssetInOut.OP, ProjectId);
                                                    AssetItemList.ShowDialog();
                                                }
                                            }
                                        }
                                    }
                                    else if (soldItemcount > 0)
                                    {
                                        //if (this.ShowConfirmationMessage("Sold Item can not be deleted.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Asset.AssetOpenningBalance.ASSET_SOLD_ITEM_CANNOT_DELETE_INFO), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            SalesMade = true;
                                            AssetItemList = new frmAssetItemList(ItemId, this.Quantity, rowNo, InOutDetailId, AssetInOut.OP, ProjectId);
                                            AssetItemList.ShowDialog();
                                        }
                                    }

                                    if (InsuranceMade)// || !InsuranceMade && !SalesMade)
                                    {
                                        DeleteRowTransaction();
                                    }
                                    else if (insurance == 0 && soldItemcount == 0)
                                    {
                                        DeleteRowTransaction();
                                    }
                                    calculateOPBalance();
                                }
                            }
                        }
                        //}
                        //else
                        //{
                        //    this.ShowMessageBox("dddd ");
                        //}
                    }
                    else if (gvOPBalance.RowCount == 1)
                    {
                        if (LedgerId > 0 || Quantity > 0)
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                gvOPBalance.DeleteRow(gvOPBalance.FocusedRowHandle);
                                ConstructPurchaseVoucherDetail();

                                //10/12/2024 -- Chinna
                                gvOPBalance.FocusedColumn = colBalanceDate;
                                // gvOPBalance.FocusedColumn = colAssetName;
                            }
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Bank.BANK_DELETE));

                    //10/12/2024
                    gvOPBalance.FocusedColumn = colBalanceDate;
                    //gvOPBalance.FocusedColumn = colAssetName;
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
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


        private frmAssetItemList DeleteRowTransaction()
        {
            frmAssetItemList AssetItemList;
            int rowNo = gvOPBalance.FocusedRowHandle;
            AssetItemList = new frmAssetItemList(ItemId, Quantity, rowNo, InOutDetailId, AssetInOut.OP, ProjectId);
            AssetItemList.DeleteAssetList();

            gvOPBalance.DeleteRow(gvOPBalance.FocusedRowHandle);
            gvOPBalance.UpdateCurrentRow();
            gcOPBalance.RefreshDataSource();
            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
            //10/12/2024 -- Chinna
            gvOPBalance.FocusedColumn = colBalanceDate;
            //gvOPBalance.FocusedColumn = colAssetName;
            return AssetItemList;
        }

        private void bbiDeleteTransaction_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteOPTransaction();
        }

        private void bbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bbiAssetGenerationList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AssetGenerationList();
        }

        private void rtxtQuantity_Leave(object sender, EventArgs e)
        {
            isvalidQty = true;
            int rowid = gvOPBalance.GetVisibleIndex(gvOPBalance.FocusedRowHandle);
            if (SettingProperty.AssetListCollection.ContainsKey(rowid))
            {
                int ListCount = SettingProperty.AssetListCollection[rowid].AsEnumerable().Count(r => r.RowState != DataRowState.Deleted ?
                            UtilityMember.NumberSet.ToInteger(r["SELECT"].ToString()) == 1 : false);
                string DCount = ListCount.ToString();

                if (Quantity.ToString() != DCount)
                {
                    //   this.ShowMessageBox("Quantity Mismatch with the Asset Item List.");
                    isvalidQty = false;
                }
            }
            gvOPBalance.SetRowCellValue(gvOPBalance.FocusedRowHandle, colQuantity, Quantity);
            //  SendKeys.Send("{ENTER}");
        }

        private bool IsQuantitymatch()
        {
            isvalidQty = true;
            DataTable dtItem = (DataTable)gcOPBalance.DataSource;
            if (dtItem != null && dtItem.Rows.Count > 0)
            {
                int RId = 0;
                foreach (DataRow drItem in dtItem.Rows)
                {
                    if (drItem.RowState != DataRowState.Deleted)
                    {
                        if (SettingProperty.AssetListCollection.ContainsKey(RId))
                        {
                            int StatusCount = SettingProperty.AssetListCollection[RId].AsEnumerable().Count(r => r.RowState != DataRowState.Deleted ?
                                UtilityMember.NumberSet.ToInteger(r["STATUS"].ToString()) != 1 : false);
                            string DCount = StatusCount.ToString();

                            int SelectCount = SettingProperty.AssetListCollection[RId].AsEnumerable().Count(r => r.RowState != DataRowState.Deleted ?
                                UtilityMember.NumberSet.ToInteger(r["SELECT"].ToString()) != 1 : false);
                            string sCount = SelectCount.ToString();

                            string ActualCount = (StatusCount + SelectCount).ToString();
                            DataTable dtItems = SettingProperty.AssetListCollection[RId];
                            if (drItem["QUANTITY"].ToString() != ActualCount.ToString())
                            {
                                // gvOPBalance.SetRowCellValue(RId, colQuantity, drItem["QUANTITY"].ToString() != string.Empty ? drItem["QUANTITY"].ToString() : string.Empty);

                                //gvOPBalance.SetRowCellValue(RId, colQuantity, drItem["QUANTITY"].ToString());
                                //isvalidQty = false;
                                //break;
                            }
                        }
                        RId++;
                    }
                }
            }
            return isvalidQty;
        }

        private void gvOPBalance_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {

        }

        private void gvOPBalance_RowCountChanged(object sender, EventArgs e)
        {
            //colQuantity.SummaryItem.DisplayFormat = "Quantity " + ;
        }

        private void ucAssetVoucherShortcuts1_LedgerOptionClicked(object sender, EventArgs e)
        {
            frmLedgerOptions ledgerOption = new frmLedgerOptions();
            ledgerOption.ShowDialog();
        }

        public void ClearAssetCommonProperties()
        {
            SettingProperty.AssetListCollection.Clear();
            SettingProperty.AssetInsuranceCollection.Clear();
            SettingProperty.AssetMultiInsuranceCollection.Clear();
            SettingProperty.AssetDeletedInoutIds = string.Empty;
            SettingProperty.AssetDeletedItemDetailIds = string.Empty;
        }

        private void glkpProject_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                LoadProjectCombo();
            }
        }

        private void gcOPBalance_Click(object sender, EventArgs e)
        {


        }
    }
}