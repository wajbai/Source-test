using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bosco.DAO.Data;
using Bosco.Model.UIModel;
using Bosco.Model.Transaction;
using Bosco.Utility;
using Bosco.Model;
using Bosco.Model.Inventory.Asset;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Controls;

namespace ACPP.Modules.Asset.Transactions
{
    public partial class frmTransferVoucherAdd : frmBaseAdd
    {
        #region Variable Declearation
        private int VoucherNo = 0;
        private int VoucherId;
        ResultArgs resultArgs = null;
        public event EventHandler UpdateHeld;
        #endregion

        #region Properties
        private int tranferId { get; set; }
        private DataTable dtAssetIdDetails { get; set; }
        private DataTable dtAssetGroupDetails { get; set; }
        private DataTable dtAssetItemDetails { get; set; }
        private int GroupId = 0;
        private int ASSET_GROUP_ID
        {
            get
            {
                GroupId = gvTransferAdd.GetFocusedRowCellValue(colAssetGroup) != null ? this.UtilityMember.NumberSet.ToInteger(gvTransferAdd.GetFocusedRowCellValue(colAssetGroup).ToString()) : 0;
                return GroupId;
            }
            set
            {
                GroupId = value;
            }
        }
        private int ItemId = 0;
        private int ITEM_ID
        {
            get
            {
                ItemId = gvTransferAdd.GetFocusedRowCellValue(colAssetName) != null ? this.UtilityMember.NumberSet.ToInteger(gvTransferAdd.GetFocusedRowCellValue(colAssetName).ToString()) : 0;
                return ItemId;
            }
            set
            {
                ItemId = value;
            }
        }

        #endregion

        #region Constructors
        public frmTransferVoucherAdd()
        {
            InitializeComponent();
        }
        public frmTransferVoucherAdd(int TransferID, int ProjectId, string ProjectName)
            : this()
        {
            ucCaptionPanel1.Caption = ProjectName;
            this.tranferId = TransferID;
        }
        #endregion

        #region Common Methods
        private void ConstructEmptyDataSource()
        {
            try
            {
                DataTable dtTransfer = new DataTable();
                dtTransfer.Columns.Add("ASSET_GROUP_ID", typeof(int));
                dtTransfer.Columns.Add("ITEM_ID", typeof(int));
                dtTransfer.Columns.Add("QUANTITY", typeof(int));
                dtTransfer.Columns.Add("AMOUNT", typeof(decimal));
                dtTransfer.Columns.Add("LOCATION_FROM_ID", typeof(int));
                dtTransfer.Columns.Add("ASSET_ID", typeof(string));
                dtTransfer.Columns.Add("NAME_ADDRESS", typeof(string));
                dtTransfer.Columns.Add("NARRATION", typeof(string));
                dtTransfer.Columns.Add("LOCATION_TO_ID", typeof(int));
                dtTransfer.Columns.Add("TRANSFER_DATE", typeof(DateTime));
                dtTransfer.Columns.Add("VOUCHER_ID", typeof(int));
                dtTransfer.Columns.Add("TRANSFER_ID", typeof(int));
                gcTransferAdd.DataSource = dtTransfer;
                gvTransferAdd.AddNewRow();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void LoadItemDetails()
        {
            try
            {
                using (AssetItemSystem assetItemSystem = new AssetItemSystem())
                {
                    resultArgs = assetItemSystem.FetchAssetItemDetailforTransfer();
                    if (resultArgs.Success && resultArgs != null)
                    {
                        dtAssetItemDetails = resultArgs.DataSource.Table;
                        //  this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpAssetName, resultArgs.DataSource.Table, assetItemSystem.AppSchema.ASSETItem.ASSET_NAMEColumn.ColumnName, assetItemSystem.AppSchema.ASSETItem.ITEM_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
            finally { }
        }

        private void LoadGroupDetails()
        {
            try
            {
                using (TransferVoucherSystem transferSystem = new TransferVoucherSystem())
                {
                    resultArgs = transferSystem.fetchAssetGroupDetails();
                    if (resultArgs.Success && resultArgs != null)
                    {
                        dtAssetGroupDetails = resultArgs.DataSource.Table;
                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpAssetGrop, resultArgs.DataSource.Table, transferSystem.AppSchema.ASSETGroupDetails.GROUP_NAMEColumn.ColumnName, transferSystem.AppSchema.ASSETGroupDetails.GROUP_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
            finally { }
        }

        private void LoadLocationDetails()
        {
            try
            {
                using (LocationSystem LocationSystem = new LocationSystem())
                {
                    resultArgs = LocationSystem.FetchLocationDetails();
                    if (resultArgs.Success && resultArgs != null)
                    {
                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpFromLocation, resultArgs.DataSource.Table, LocationSystem.AppSchema.ASSETLocationDetails.LOCATION_NAMEColumn.ColumnName, LocationSystem.AppSchema.ASSETLocationDetails.LOCATION_FROM_IDColumn.ColumnName);
                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpToLocation, resultArgs.DataSource.Table, LocationSystem.AppSchema.ASSETLocationDetails.LOCATION_NAMEColumn.ColumnName, LocationSystem.AppSchema.ASSETLocationDetails.LOCATION_TO_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
            finally { }
        }

        private void LoadAssetItemDetails()
        {
            try
            {
                using (AssetItemSystem assetSystem = new AssetItemSystem())
                {
                    if (dtAssetIdDetails != null && dtAssetIdDetails.Rows.Count > 0)
                    {
                        DataView dvAssetId = dtAssetIdDetails.DefaultView;
                        dvAssetId.RowFilter = "ITEM_ID=" + this.ITEM_ID + "";
                        DataTable dtAssetId = dvAssetId.ToTable();
                        rcbAssetId.DataSource = dtAssetId;

                        //this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpAssetId, dtAssetId, assetSystem.AppSchema.AssetTransferDetails.ASSET_IDColumn.ColumnName, assetSystem.AppSchema.AssetTransferDetails.ASSET_IDColumn.ColumnName);
                        dvAssetId.RowFilter = "";
                    }
                    assetSystem.ItemId = this.ITEM_ID;
                    resultArgs = assetSystem.FetchAssetItemDetailforTransferByID();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        foreach (DataRow drTransfer in resultArgs.DataSource.Table.Rows)
                        {
                            decimal amount = this.UtilityMember.NumberSet.ToDecimal(drTransfer[assetSystem.AppSchema.AssetTransferDetails.AMOUNTColumn.ColumnName].ToString());
                            //int quantity = this.UtilityMember.NumberSet.ToInteger(drTransfer[assetSystem.AppSchema.AssetTransferDetails.QUANTITYColumn.ColumnName].ToString());
                            int locationFromId = this.UtilityMember.NumberSet.ToInteger(drTransfer[assetSystem.AppSchema.AssetTransferDetails.LOCATION_FROM_IDColumn.ColumnName].ToString());
                            gvTransferAdd.SetRowCellValue(gvTransferAdd.FocusedRowHandle, colAmount, amount);
                            //gvTransferAdd.SetRowCellValue(gvTransferAdd.FocusedRowHandle, colQuantity, quantity);
                            gvTransferAdd.SetRowCellValue(gvTransferAdd.FocusedRowHandle, colFromLocation, locationFromId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
            finally { }
        }

        private void LoadAssetIdDetails()
        {
            using (AssetItemSystem assetSystem = new AssetItemSystem())
            {
                resultArgs = assetSystem.FetchAssetIdDetail(0);
                if (resultArgs != null && resultArgs.Success) //&& resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0
                {
                    dtAssetIdDetails = resultArgs.DataSource.Table;
                    // this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpAssetId, resultArgs.DataSource.Table, assetSystem.AppSchema.AssetTransferDetails.ASSET_IDColumn.ColumnName, assetSystem.AppSchema.AssetTransferDetails.ASSET_IDColumn.ColumnName);
                }
            }
        }

        private void SetTitle()
        {
            this.Text = this.tranferId == 0 ? this.GetMessage(MessageCatalog.Asset.TransferVoucher.TRANSFER_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.TransferVoucher.TRANSFER_EDIT_CAPTION);
        }

        private void ClearControls()
        {
            txtNarration.Text = string.Empty;
            txtNameAddress.Text = string.Empty;
            txtRefrenceNo.Text = string.Empty;
            ConstructEmptyDataSource();
            this.dtTransactionDate.Focus();
        }

        private bool IsValidEntry()
        {
            bool isValid = true;
            DataTable transSource = (DataTable)gcTransferAdd.DataSource;
            if (string.IsNullOrEmpty(dtTransactionDate.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_TRANSACTION_DATE));
                isValid = false;
                this.SetBorderColor(dtTransactionDate);
                dtTransactionDate.Focus();
            }
            else if (string.IsNullOrEmpty(txtRefrenceNo.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.REFRENCE_NUMBER_EMPTY));
                isValid = false;
                this.SetBorderColor(txtRefrenceNo);
                txtRefrenceNo.Focus();
            }
            //else if (!IsValidTransGrid())
            //{
            //    isValid = false;
            //}
            return isValid;
        }

        private bool IsValidTransGrid()
        {
            DataTable dtTrans = gcTransferAdd.DataSource as DataTable;

            int GroupId = 0;
            decimal Amount = 0;
            int AssetId = 0;
            int LocationFromId = 0;
            int LocationToId = 0;
            int RowPosition = 0;
            int ItemId = 0;
            bool isValid = false;

            string validateMessage = "Required Information not filled, Transaction is not filled fully";

            //"((LEDGER_ID=0 OR LEDGER_ID IS NULL)  OR (AMOUNT<=0 OR AMOUNT IS Null)) AND (NOT((LEDGER_ID=0 OR LEDGER_ID IS NULL)  AND (AMOUNT<=0 OR AMOUNT IS Null)))";
            DataView dv = new DataView(dtTrans);
            dv.RowFilter = "(ASSET_GROUP_ID>0 OR AMOUNT>0)";
            gvTransferAdd.FocusedColumn = colAssetGroup;
            if (dv.Count > 0)
            {
                isValid = true;
                foreach (DataRowView drTrans in dv)
                {
                    GroupId = this.UtilityMember.NumberSet.ToInteger(drTrans["ASSET_GROUP_ID"].ToString());
                    Amount = this.UtilityMember.NumberSet.ToDecimal(drTrans["AMOUNT"].ToString());
                    AssetId = this.UtilityMember.NumberSet.ToInteger(drTrans["ASSET_ID"].ToString());
                    LocationFromId = this.UtilityMember.NumberSet.ToInteger(drTrans["LOCATION_FROM_ID"].ToString());
                    LocationToId = this.UtilityMember.NumberSet.ToInteger(drTrans["LOCATION_TO_ID"].ToString());
                    ItemId = this.UtilityMember.NumberSet.ToInteger(drTrans["ITEM_ID"].ToString());


                    if ((GroupId == 0 || Amount == 0 || AssetId == 0 || ItemId == 0 || LocationFromId == 0 || LocationToId == 0)) //&& !(Id == 0 && Amt == 0))
                    {
                        if (GroupId == 0)
                        {
                            validateMessage = "Required Information not filled, Group Name is empty";
                            gvTransferAdd.FocusedColumn = colAssetGroup;
                        }
                        if (LocationFromId == 0)
                        {
                            validateMessage = "Required Information not filled, Location From is empty";
                            gvTransferAdd.FocusedColumn = colAssetName;
                        }
                        if (LocationToId == 0)
                        {
                            validateMessage = "Required Information not filled, Location To is empty";
                            gvTransferAdd.FocusedColumn = colAssetName;
                        }
                        if (ItemId == 0)
                        {
                            validateMessage = "Required Information not filled, Asset Name is empty";
                            gvTransferAdd.FocusedColumn = colAssetName;
                        }
                        //if (AssetId == 0)
                        //{
                        //    validateMessage = "Required Information not filled, Asset Id is empty";
                        //    gvTransferAdd.FocusedColumn = colAssetName;
                        //}
                        if (Amount == 0)
                        {
                            validateMessage = "Required Information not filled, Amount is empty";
                            gvTransferAdd.FocusedColumn = colAmount;
                        }
                        isValid = false;
                        break;
                    }
                    RowPosition = RowPosition + 1;
                }
            }

            if (!isValid)
            {
                this.ShowMessageBox(validateMessage);
                gvTransferAdd.CloseEditor();
                gvTransferAdd.FocusedRowHandle = gvTransferAdd.GetRowHandle(RowPosition);
                gvTransferAdd.ShowEditor();
            }

            return isValid;
        }

        private void AssigntoProperties()
        {
            try
            {
                if (this.tranferId > 0)
                {
                    using (TransferVoucherSystem transferSystem = new TransferVoucherSystem(this.tranferId))
                    {
                        transferSystem.TransferId = this.tranferId;
                        resultArgs = transferSystem.FillTransferProperties();
                        gcTransferAdd.DataSource = resultArgs.DataSource.Table;
                        dtTransactionDate.DateTime = transferSystem.TransfetrDate;
                        txtRefrenceNo.Text = transferSystem.RefrenceId.ToString();
                        txtNameAddress.Text = transferSystem.NameAddress;
                        txtNarration.Text = transferSystem.Narration;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message); ;
            }
            finally { }
        }

        private void DeleteTransaction()
        {
            if (gvTransferAdd.FocusedRowHandle != GridControl.NewItemRowHandle)
            {
                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    gvTransferAdd.DeleteRow(gvTransferAdd.FocusedRowHandle);
                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                }
            }
        }

        private void FilterAssetNameByGroup()
        {
            using (AssetItemSystem assetSystem = new AssetItemSystem())
            {
                // resultArgs = assetSystem.FetchAssetItemDetailforTransfer();
                if (dtAssetItemDetails != null && dtAssetItemDetails.Rows.Count > 0)
                {
                    DataView dvAssetId = dtAssetItemDetails.DefaultView;
                    dvAssetId.RowFilter = "ASSET_GROUP_ID=" + this.ASSET_GROUP_ID + "";
                    DataTable dtAssetId = dvAssetId.ToTable();
                    this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpAssetName, dtAssetId, assetSystem.AppSchema.ASSETItem.ASSET_NAMEColumn.ColumnName, assetSystem.AppSchema.ASSETItem.ITEM_IDColumn.ColumnName);
                    dvAssetId.RowFilter = "";
                }
            }
        }

        #endregion

        #region Events

        private void frmTransfer_Load(object sender, EventArgs e)
        {
            SetTitle();
            ConstructEmptyDataSource();
            LoadLocationDetails();
            LoadAssetIdDetails();
            LoadItemDetails();
            LoadGroupDetails();
            AssigntoProperties();
            //dtTransactionDate.DateTime = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidEntry())
                {
                    using (TransferVoucherSystem transferVoucherSystem = new TransferVoucherSystem())
                    {
                        transferVoucherSystem.TransferId = this.tranferId;
                        transferVoucherSystem.branchId = 1;
                        transferVoucherSystem.TransfetrDate = dtTransactionDate.DateTime;
                        transferVoucherSystem.Narration = txtNameAddress.Text;
                        transferVoucherSystem.NameAddress = txtNarration.Text;
                        transferVoucherSystem.RefrenceId =this.UtilityMember.NumberSet.ToInteger(txtRefrenceNo.Text);

                        transferVoucherSystem.dtTransferDetail = gcTransferAdd.DataSource as DataTable;
                        resultArgs = transferVoucherSystem.SaveAssetTransfer();
                    }
                    if (resultArgs.Success && resultArgs != null)
                    {
                        ClearControls();
                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                        if (UpdateHeld != null)
                        {
                            UpdateHeld(this, e);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
            finally { }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            VoucherId = 0;
            ClearControls();
        }

        private void rglkpAssetGrop_Leave(object sender, EventArgs e)
        {

        }

        private void ribtDelete_Click(object sender, EventArgs e)
        {
            DeleteTransaction();
        }

        private void rglkpAssetName_Leave(object sender, EventArgs e)
        {
            //gvTransferAdd.PostEditor();
            //gvTransferAdd.UpdateCurrentRow();
            //LoadAssetItemDetails();
        }

        private void dtTransactionDate_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(dtTransactionDate);
        }

        private void txtVoucherNo_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtRefrenceNo);
        }

        private void gcTransferAdd_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control)
                {
                    gvTransferAdd.PostEditor();
                    gvTransferAdd.UpdateCurrentRow();

                    if (gvTransferAdd.FocusedColumn == colAssetGroup)
                    {
                        FilterAssetNameByGroup();
                    }
                    else if (gvTransferAdd.FocusedColumn == colAssetName)
                    {
                        LoadAssetItemDetails();
                    }

                    if (gvTransferAdd.IsLastRow && (gvTransferAdd.FocusedColumn == colAmount) && gvTransferAdd.GetFocusedRowCellValue(colAmount) != null)
                    {
                        string AssetGroup = gvTransferAdd.GetFocusedRowCellValue(colAssetGroup) != null ? gvTransferAdd.GetFocusedRowCellValue(colAssetGroup).ToString() : string.Empty;
                        string AssetName = gvTransferAdd.GetFocusedRowCellValue(colAssetName) != null ? gvTransferAdd.GetFocusedRowCellValue(colAssetName).ToString() : string.Empty;
                        string LocationFrom = gvTransferAdd.GetFocusedRowCellValue(colFromLocation) != null ? gvTransferAdd.GetFocusedRowCellValue(colFromLocation).ToString() : string.Empty;
                        string LocationTo = gvTransferAdd.GetFocusedRowCellValue(colToLocation) != null ? gvTransferAdd.GetFocusedRowCellValue(colToLocation).ToString() : string.Empty;
                        string AssetId = gvTransferAdd.GetFocusedRowCellValue(colAssetId) != null ? gvTransferAdd.GetFocusedRowCellValue(colAssetId).ToString() : string.Empty;
                        decimal Amount = gvTransferAdd.GetFocusedRowCellValue(colAmount) != null ? this.UtilityMember.NumberSet.ToDecimal(gvTransferAdd.GetFocusedRowCellValue(colAmount).ToString()) : 0;
                        if (!string.IsNullOrEmpty(AssetGroup) && !string.IsNullOrEmpty(AssetName) && !string.IsNullOrEmpty(LocationFrom) && !string.IsNullOrEmpty(LocationTo) && !string.IsNullOrEmpty(AssetId) && Amount > 0)
                        {
                            gvTransferAdd.AddNewRow();
                        }
                        else
                        {
                            gvTransferAdd.CloseEditor();
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                            txtNameAddress.Focus();
                            //txtNameAddress.Select();
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

        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.F3))
            {
                dtTransactionDate.Focus();
            }
            if (KeyData == Keys.F4)// (Keys.Control | Keys.N)
            {
                dtTransactionDate.DateTime = dtTransactionDate.DateTime.AddDays(1);
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        #endregion

        private void gcTransferAdd_Enter(object sender, EventArgs e)
        {
            gvTransferAdd.OptionsSelection.EnableAppearanceFocusedCell = true;
        }

        private void gcTransferAdd_Leave(object sender, EventArgs e)
        {
            gvTransferAdd.OptionsSelection.EnableAppearanceFocusedCell = false;
        }

    }
}

