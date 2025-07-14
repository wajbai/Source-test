using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Bosco.Utility;
using Bosco.Model;
using Bosco.Model.Inventory.Asset;
using ACPP.Modules.Transaction;
using Bosco.Model.UIModel;
using Bosco.DAO.Schema;
using DevExpress.XtraEditors;

namespace ACPP.Modules.Asset.Transactions
{
    public partial class frmTransfer : frmFinanceBaseAdd
    {
        #region Variable Declearation
        private int VoucherNo = 0;
        private int VoucherId;
        ResultArgs resultArgs = null;
        public event EventHandler UpdateHeld;
        const string SELECT_COL = "SELECT";
        private string ASSET_GROUP_ID = "ASSET_GROUP_ID";
        private string RecentVoucherDate { get; set; }
        string AssetIDCollection = string.Empty;
        private AppSchemaSet.ApplicationSchemaSet appSchema = new AppSchemaSet.ApplicationSchemaSet();
        bool isIdChecked = true;
        #endregion

        #region Properties
        private int ITEM_ID { get; set; }
        private int tranferId { get; set; }
        private DataTable dtLocationDetails { get; set; }
        private DataTable dtAssetIdDetails { get; set; }
        private DataTable dtAssetItemDetails { get; set; }
        private DataTable dtCheckedItems { get; set; }

        private int GroupId = 0;

        #endregion

        #region Constructors
        public frmTransfer()
        {
            InitializeComponent();
        }
        public frmTransfer(int TransferID, int ProjectId, string ProjectName)
            : this()
        {
            this.tranferId = TransferID;
        }
        #endregion

        #region Events
        private void frmTransfer_Load(object sender, EventArgs e)
        {
            SetTitle();
            LoadDefaults();
           // LoadLocationDetails();
            LoadItemDetails();
            AssignToControls();
            //  LoardProjectDetails();
            LoardProjectDetail();
        }


        private void glkpAssetName_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataRowView drv = glkpAssetName.GetSelectedDataRow() as DataRowView;
                using (AssetItemSystem assetItemSystem = new AssetItemSystem())
                {
                    DataTable dtAssetitem = new DataTable();
                    assetItemSystem.ItemId = this.UtilityMember.NumberSet.ToInteger(glkpAssetName.EditValue.ToString());
                    resultArgs = assetItemSystem.FetchProjectByAssetItem();
                    this.ITEM_ID = this.UtilityMember.NumberSet.ToInteger(glkpAssetName.EditValue.ToString());
                    if (resultArgs.Success && resultArgs != null)
                    {
                        dtAssetitem = resultArgs.DataSource.Table;
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpFromProject, dtAssetitem, assetItemSystem.AppSchema.Project.PROJECTColumn.ColumnName, assetItemSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                        // glkpFromProject.EditValue = glkpFromProject.Properties.GetKeyValue(0);
                        //int projectID =(dtAssetitem.Rows[0]["PROJECT_ID"].ToString()) !=null ? this.UtilityMember.NumberSet.ToInteger(dtAssetitem.Rows[0]["PROJECT_ID"].ToString()):0;
                        //if (projectID == 0)
                        glkpFromLocation.EditValue = 0;
                        gcFromLocationAssets.DataSource = null;
                    }
                }
                //if (drv != null)
                //{
                //    GroupId = this.UtilityMember.NumberSet.ToInteger(drv[ASSET_GROUP_ID].ToString());
                //}
                //using (LocationSystem locationSystem = new LocationSystem())
                //{
                //    resultArgs = locationSystem.FetchLocationDetails();
                //    if (resultArgs.Success && resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0)
                //    {
                //        dtLocationDetails = resultArgs.DataSource.Table;
                //        
                //        DataView dvLocationDetails = dtLocationDetails.DefaultView;
                //        dvLocationDetails.RowFilter = "ITEM_ID=" + ITEM_ID + "";
                //        DataTable dtLocation = dvLocationDetails.ToTable();
                //        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpFromLocation, dtLocation, locationSystem.AppSchema.ASSETLocationDetails.LOCATION_NAMEColumn.ColumnName, locationSystem.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName);
                //        gcFromLocationAssets.DataSource = null;
                //    }
                //}
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
            finally { }
        }

        private void glkpToLocation_EditValueChanged(object sender, EventArgs e)
        {
            if (glkpFromLocation.EditValue != null)
            {
                if (glkpFromLocation.EditValue != glkpToLocation.EditValue)
                {
                    if (glkpToLocation.EditValue != null)
                    {
                        LoadToLocationGrid();
                    }
                    else
                    {
                        gcToLocationAssets.DataSource = null;
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.TransferVoucher.TRANSFER_FROM_TO_SAME));
                    glkpToLocation.Text = string.Empty;
                    glkpToLocation.Focus();
                }
                btnMoveIn.Focus();
            }
        }

        private void glkpFromLocation_EditValueChanged(object sender, EventArgs e)
        {
            LoadFromLocationGrid();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SaveAssetTransferDetails(e);
        }

        private void glkpAssetName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpAssetName);
        }

        private void glkpFromLocation_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpFromLocation);
        }

        private void glkpToLocation_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpToLocation);
            if (tranferId == 0)
            {
                int FromLocationId = glkpFromLocation.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpFromLocation.EditValue.ToString()) : 0;
                int ToLocationId = glkpToLocation.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpToLocation.EditValue.ToString()) : 0;
                if (FromLocationId > 0 && ToLocationId > 0)
                {
                    if (FromLocationId.Equals(ToLocationId)&&glkpFromProject.Equals(glkpToProject))
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.TransferVoucher.TRANSFER_FROM_TO_SAME));
                        glkpToLocation.EditValue = null;
                        glkpToLocation.Select();
                        glkpToLocation.Focus();
                    }

                }
            }
        }

        private void btnMoveIn_Click(object sender, EventArgs e)
        {
            if (gcFromLocationAssets.DataSource != null)
            {
                if (!string.IsNullOrEmpty(glkpToLocation.Text))
                {
                    MoveInAsstes();
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.TransferVoucher.TRANSFER_TOLOCATION_EMPTY));
                    this.SetBorderColor(glkpToLocation);
                    glkpToLocation.Focus();
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.TransferVoucher.NO_ASSET_SELECTED));
            }
        }

        private void chkFromLocationSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dtFromLocationAsset = (DataTable)gcFromLocationAssets.DataSource;
            if (dtFromLocationAsset != null && dtFromLocationAsset.Rows.Count > 0)
            {
                foreach (DataRow dr in dtFromLocationAsset.Rows)
                {
                    dr[SELECT_COL] = chkFromLocationSelectAll.Checked;
                }
                gcFromLocationAssets.DataSource = dtFromLocationAsset;
            }
        }

        private void chkToLocationSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtToLocationAsset = (DataTable)gcToLocationAssets.DataSource;
                if (dtToLocationAsset != null && dtToLocationAsset.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtToLocationAsset.Rows)
                    {
                        dr[SELECT_COL] = chkToLocationSelectAll.Checked;
                    }
                    gcToLocationAssets.DataSource = dtToLocationAsset;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Source + Environment.NewLine + ex.Message);
            }
        }

        private void btnMoveOut_Click(object sender, EventArgs e)
        {
            if (gcToLocationAssets.DataSource != null)
            {
                if (glkpFromLocation.Text != string.Empty)
                {
                    MoveOutAssets();
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.TransferVoucher.TRANSFER_FROMLOCATION_EMPTY));
                    this.SetBorderColor(glkpFromLocation);
                    glkpFromLocation.Focus();
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.TransferVoucher.NO_ASSET_SELECTED));
            }
        }

        private void gvFromLocationAssets_RowCountChanged(object sender, EventArgs e)
        {
            lblFromCount.Text = gvFromLocationAssets.RowCount.ToString();
        }

        private void gvToLocationAssets_RowCountChanged(object sender, EventArgs e)
        {
            lblToCount.Text = gvToLocationAssets.RowCount.ToString();
        }

        private void chkFromShowFilte_CheckedChanged(object sender, EventArgs e)
        {
            gvFromLocationAssets.OptionsView.ShowAutoFilterRow = chkFromShowFilte.Checked;
            if (chkFromShowFilte.Checked)
            {
                this.SetFocusRowFilter(gvFromLocationAssets, colAssetId);
            }
        }

        private void chkToShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvToLocationAssets.OptionsView.ShowAutoFilterRow = chkToShowFilter.Checked;
            if (chkToShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvToLocationAssets, colAssetId);
            }
        }

        private void chkFromShowFilte_Click(object sender, EventArgs e)
        {
            chkFromShowFilte.Checked = (chkFromShowFilte.Checked) ? false : true;
        }

        private void chkToShowFilter_Click(object sender, EventArgs e)
        {
            chkToShowFilter.Checked = (chkToShowFilter.Checked) ? false : true;
        }

        private void gcFromLocationAssets_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (gvFromLocationAssets.IsLastRow)
                {
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    btnMoveIn.Select();
                    btnMoveIn.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void gcToLocationAssets_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (gvToLocationAssets.IsLastRow)
                {
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    meNarration.Select();
                    meNarration.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void txtRefrenceId_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtRefrenceId);
        }

        #endregion

        #region Methods

        private void SaveAssetTransferDetails(EventArgs e)
        {
            try
            {
                if (IsValidTransfer())
                {
                    using (TransferVoucherSystem transferSystem = new TransferVoucherSystem())
                    {
                        transferSystem.TransferId = this.tranferId;
                        transferSystem.GroupId = GroupId;
                        transferSystem.RefrenceId = this.UtilityMember.NumberSet.ToInteger(txtRefrenceId.Text.ToString());
                        transferSystem.ItemId = this.UtilityMember.NumberSet.ToInteger(glkpAssetName.EditValue.ToString());
                        transferSystem.TransfetrDate = this.UtilityMember.DateSet.ToDate(deTransferDate.Text, false);
                        transferSystem.ToLocationId = this.UtilityMember.NumberSet.ToInteger(glkpToLocation.EditValue.ToString());
                        transferSystem.FromLocationId = this.UtilityMember.NumberSet.ToInteger(glkpFromLocation.EditValue.ToString());
                        transferSystem.Narration = meNarration.Text;
                        DataTable dtAssetTransfer = gcToLocationAssets.DataSource as DataTable;
                        transferSystem.dtTransferDetail = dtAssetTransfer;
                        DataTable dtAssetTransfered = gcFromLocationAssets.DataSource as DataTable;
                        transferSystem.dtTransferedDetail = dtAssetTransfered;
                        resultArgs = transferSystem.SaveAssetTransfer();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            ClearControls();
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Source + Environment.NewLine + ex.Message);
            }
            finally { }
        }

        private void LoadAssetIdDetails()
        {
            using (AssetItemSystem assetSystem = new AssetItemSystem())
            {
                resultArgs = assetSystem.FetchAssetIdDetail(0);
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    dtAssetIdDetails = resultArgs.DataSource.Table;
                }
            }
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
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpAssetName, resultArgs.DataSource.Table, assetItemSystem.AppSchema.ASSETItem.ASSET_ITEMColumn.ColumnName, assetItemSystem.AppSchema.ASSETItem.ITEM_IDColumn.ColumnName);
                        // glkpAssetName.EditValue = glkpAssetName.Properties.GetKeyValue(0);
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
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpToLocation, resultArgs.DataSource.Table, LocationSystem.AppSchema.ASSETLocationDetails.TO_LOCATION_NAMEColumn.ColumnName, LocationSystem.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
            finally { }
        }

        private bool IsValidTransfer()
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(txtRefrenceId.Text))
            {
                this.SetBorderColor(txtRefrenceId);
                //this.ShowMessageBox("Refrence No is empty.");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.FixedAssetTransfer.FIXED_ASSET_TRANS_REGNO_EMTPY));
                txtRefrenceId.Focus();
                isValid = false;
            }
            else if (string.IsNullOrEmpty(glkpAssetName.Text))
            {
                this.SetBorderColor(glkpAssetName);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.TransferVoucher.TRANSFER_ASSETITEM_EMPTY));
                glkpAssetName.Focus();
                isValid = false;
            }
            else if (string.IsNullOrEmpty(glkpFromLocation.Text))
            {
                this.SetBorderColor(glkpFromLocation);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.TransferVoucher.TRANSFER_FROMLOCATION_EMPTY));
                glkpFromLocation.Focus();
                isValid = false;
            }
            else if (string.IsNullOrEmpty(glkpToLocation.Text))
            {
                this.SetBorderColor(glkpToLocation);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.TransferVoucher.TRANSFER_TOLOCATION_EMPTY));
                glkpToLocation.Focus();
                isValid = false;
            }
            else if (gcFromLocationAssets.DataSource == null)
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.TransferVoucher.TRANSFER_FROMLOCATION_EMPTY));
                isValid = false;
            }
            else if (gcToLocationAssets.DataSource == null)
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.TransferVoucher.TRANSFER_TOLOCATION_GRID_EMPTY));
                isValid = false;
            }
            else if (deTransferDate.DateTime > DateTime.Now)
            {
                this.SetBorderColor(glkpToLocation);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.TransferVoucher.TRANSFER_DATE_FUTURE));
                deTransferDate.Focus();
                isValid = false;
            }
            return isValid;
        }

        private void LoadToLocationGrid()
        {
            try
            {
                AssetIDCollection = string.Empty;
                gcToLocationAssets.DataSource = null;
                int LocationId = 0;
                DataTable dtAssetId = new DataTable();
                DataTable dtTransfered = new DataTable();
                LocationId = glkpToLocation.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpToLocation.EditValue.ToString()) : 0;

                using (AssetItemSystem assetSystem = new AssetItemSystem())
                {
                    dtTransfered = gcToLocationAssets.DataSource as DataTable;
                    resultArgs = assetSystem.FetchAssetIdDetail(LocationId);
                    if (resultArgs != null && resultArgs.Success)
                    {
                        DataTable dtAssetIdDetails = resultArgs.DataSource.Table;
                        //DataView dvAssetId = dtAssetIdDetails.DefaultView;
                        //dvAssetId.RowFilter = "ITEM_ID=" + this.ITEM_ID + "";
                        //dtAssetId = dvAssetId.ToTable();
                        //dtAssetIdDetails = AddColumns(dtAssetId);
                        if (dtAssetIdDetails != null && dtAssetIdDetails.Rows.Count > 0)
                        {
                            if (dtTransfered != null && dtTransfered.Rows.Count > 0)
                            {
                                dtAssetIdDetails.Merge(dtTransfered);
                            }
                            gcToLocationAssets.DataSource = dtAssetIdDetails;
                            AssetIDCollection = string.Empty;
                            foreach (DataRow dr in dtAssetIdDetails.Rows)
                            {
                                AssetIDCollection += dr[this.appSchema.ASSETItem.ASSET_IDColumn.ColumnName] + ",";
                            }
                            AssetIDCollection = AssetIDCollection.Trim(',');
                            LoadFromLocationGrid();
                            //layoutControlGroup2.Text = glkpToLocation.Text + " " + "(Asset)";
                            layoutControlGroup2.Text = glkpToLocation.Text + " " + this.GetMessage(MessageCatalog.Asset.FixedAssetTransfer.FIXED_ASSET_TITLE);

                        }
                    }
                }
                gcFromLocationAssets.Focus();
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
            finally
            { }
        }

        private void LoadFromLocationGrid()
        {
            try
            {
                DataTable dtAssetId = new DataTable();
                int LocationId = 0;
                LocationId = glkpFromLocation.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpFromLocation.EditValue.ToString()) : 0;
                using (AssetItemSystem assetSystem = new AssetItemSystem())
                {
                    resultArgs = assetSystem.FetchAssetIdDetail(LocationId);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        dtAssetIdDetails = resultArgs.DataSource.Table;
                        DataView dvAssetId = dtAssetIdDetails.DefaultView;
                        dvAssetId.RowFilter = "ITEM_ID=" + this.ITEM_ID + "";
                        dtAssetId = dvAssetId.ToTable();
                        dtAssetIdDetails = AddColumns(dtAssetId);
                        gcFromLocationAssets.DataSource = dtAssetIdDetails;
                        //layoutControlGroup3.Text = glkpFromLocation.Text + " " + "(Asset)";
                        layoutControlGroup3.Text = glkpFromLocation.Text + " " + this.GetMessage(MessageCatalog.Asset.FixedAssetTransfer.FIXED_ASSET_TITLE);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
                throw;
            }
        }

        //private void LoadFromProject()
        //{
        //    try
        //    {
        //        using (AssetItemSystem assetItemSystem = new AssetItemSystem())
        //        {
        //            int LocationID = this.UtilityMember.NumberSet.ToInteger(glkpFromLocation.EditValue.ToString());
        //            int ProjectID;
        //            resultArgs = assetItemSystem.FetchAssetItemDetailAll();

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
        //        throw;
        //    }
        //}


        private void ClearControls()
        {
            if (this.tranferId == 0)
            {
                glkpAssetName.Text = glkpFromLocation.Text = glkpToLocation.Text = string.Empty;
                gcFromLocationAssets.DataSource = gcToLocationAssets.DataSource = null;
                glkpAssetName.EditValue = glkpAssetName.Properties.GetKeyValue(0);
            }
            else
            {
                this.Close();
            }
        }

        private void MoveInAsstes()
        {
            try
            {
                DataTable dtTransferAssets = gcToLocationAssets.DataSource as DataTable;
                DataTable dtAvailableAssets = gcFromLocationAssets.DataSource as DataTable;
                if (dtAvailableAssets != null)
                {
                    //To get all the checked items from Available Assets
                    var CheckedAssets = (from ledgers in dtAvailableAssets.AsEnumerable()
                                         where ((ledgers.Field<Int32?>(SELECT_COL) == 1))
                                         select ledgers);
                    //To get all the Unchecked item of Available Assets
                    var UnCheckedAssets = (from ledgers in dtAvailableAssets.AsEnumerable()
                                           where ((ledgers.Field<Int32?>(SELECT_COL) != 1))
                                           select ledgers);
                    if (UnCheckedAssets.Count() > 0)
                        dtAvailableAssets = UnCheckedAssets.CopyToDataTable();
                    else
                        dtAvailableAssets = (gcFromLocationAssets.DataSource as DataTable).Clone();
                    if (CheckedAssets.Count() > 0)
                    {
                        if (dtTransferAssets != null)
                            dtTransferAssets.Merge(CheckedAssets.CopyToDataTable());
                        else
                            dtTransferAssets = CheckedAssets.CopyToDataTable();
                    }
                    else
                    {
                        if (dtTransferAssets == null)
                            dtTransferAssets = (gcToLocationAssets.DataSource as DataTable).Clone();
                    }
                    gcToLocationAssets.DataSource = MakeSelectColumnZero(dtTransferAssets);
                    gcFromLocationAssets.DataSource = dtAvailableAssets;
                }
                chkFromLocationSelectAll.Checked = false;
                gcToLocationAssets.Focus();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Source + Environment.NewLine + ex.Message);
            }
        }

        private DataTable MakeSelectColumnZero(DataTable dtCheckedColumnSource)
        {
            if (dtCheckedColumnSource != null)
            {
                int i = 0;
                if (dtCheckedColumnSource.Columns.Contains(SELECT_COL))
                {
                    foreach (DataRow dr in dtCheckedColumnSource.Rows)
                    {
                        dtCheckedColumnSource.Rows[i++][SELECT_COL] = 0;
                    }
                }
                else
                    dtCheckedColumnSource = AddColumns(dtCheckedColumnSource);
            }
            return dtCheckedColumnSource;
        }

        private DataTable AddColumns(DataTable NewColumns)
        {
            if (!NewColumns.Columns.Contains(SELECT_COL))
                NewColumns.Columns.Add(SELECT_COL, typeof(Int32));
            return NewColumns;
        }

        private void MoveOutAssets()
        {
            try
            {
                string CheckedItemID = string.Empty;
                DataTable dtTransferAssets = gcToLocationAssets.DataSource as DataTable;
                DataTable dtAvailableAssets = gcFromLocationAssets.DataSource as DataTable;
                if (dtTransferAssets != null)
                {
                    var CheckedItems = (from Checkeditems in dtTransferAssets.AsEnumerable()
                                        where (Checkeditems.Field<Int32?>(SELECT_COL) == 1)
                                        select Checkeditems);

                    var UnCheckedItems = (from Uncheckeditems in dtTransferAssets.AsEnumerable()
                                          where (Uncheckeditems.Field<Int32?>(SELECT_COL) != 1)
                                          select Uncheckeditems);
                    if (CheckedItems.Count() > 0)
                    {
                        dtCheckedItems = CheckedItems.CopyToDataTable();
                        if (dtTransferAssets != null)
                        {
                            if (dtAssetIdDetails.Rows.Count > 0)
                            {
                                dtAvailableAssets.Merge(dtCheckedItems);
                            }
                        }
                        else
                        {
                            dtAvailableAssets = dtCheckedItems;
                        }
                        if (UnCheckedItems.Count() > 0)
                        {
                            dtTransferAssets = UnCheckedItems.CopyToDataTable();
                        }
                        else
                        {
                            dtTransferAssets = dtAvailableAssets.Clone();
                        }
                    }
                    //else
                    //{
                    //    ShowMessageBox("Can't not unmaped this asset ID");
                    //}
                }
                if (dtTransferAssets != null)
                {
                    gcFromLocationAssets.DataSource = dtAvailableAssets;
                    gcToLocationAssets.DataSource = MakeSelectColumnZero(dtTransferAssets);
                }
                chkToLocationSelectAll.Checked = false;
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Source + Environment.NewLine + ex.Message);
            }
        }

        private void AssignToControls()
        {
            try
            {
                if (tranferId > 0)
                {
                    using (TransferVoucherSystem transferSystem = new TransferVoucherSystem())
                    {
                        transferSystem.TransferId = this.tranferId;
                        resultArgs = transferSystem.FillTransferProperties();
                        DataTable dtTransfer = resultArgs.DataSource.Table;
                        if (resultArgs != null && resultArgs.Success && dtTransfer != null && dtTransfer.Rows.Count > 0)
                        {
                            txtRefrenceId.Text = dtTransfer.Rows[0][transferSystem.AppSchema.AssetTransferDetails.REFRENCE_IDColumn.ColumnName].ToString();
                            meNarration.Text = dtTransfer.Rows[0][transferSystem.AppSchema.AssetTransferDetails.NARRATIONColumn.ColumnName].ToString();
                            deTransferDate.DateTime = this.UtilityMember.DateSet.ToDate(dtTransfer.Rows[0][transferSystem.AppSchema.AssetTransferDetails.TRANSFER_DATEColumn.ColumnName].ToString(), false);
                            glkpAssetName.EditValue = dtTransfer.Rows[0][transferSystem.AppSchema.AssetTransferDetails.ITEM_IDColumn.ColumnName].ToString();
                            glkpFromLocation.EditValue = dtTransfer.Rows[0][transferSystem.AppSchema.AssetTransferDetails.LOCATION_FROM_IDColumn.ColumnName].ToString();
                            glkpToLocation.EditValue = dtTransfer.Rows[0][transferSystem.AppSchema.AssetTransferDetails.LOCATION_TO_IDColumn.ColumnName].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Source + Environment.NewLine + ex.Message);
            }
            finally
            { }
        }

        private void SetTitle()
        {
            this.Text = tranferId == 0 ? this.GetMessage(MessageCatalog.Asset.TransferVoucher.TRANSFER_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.TransferVoucher.TRANSFER_EDIT_CAPTION);
        }
        private void LoadDefaults()
        {
            deTransferDate.DateTime = DateTime.Now;
            deTransferDate.Focus();
            deTransferDate.DateTime = UtilityMember.DateSet.ToDate(RecentVoucherDate, false);
            deTransferDate.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deTransferDate.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            deTransferDate.DateTime = (!string.IsNullOrEmpty(RecentVoucherDate)) ? UtilityMember.DateSet.ToDate(RecentVoucherDate, false) : UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deTransferDate.DateTime = deTransferDate.DateTime.AddMonths(1).AddDays(-1);
        }

        #endregion

        private void glkpFromProject_EditValueChanged(object sender, EventArgs e)
        {
            LoadFromProjects();
        }

        private void LoadFromProjects()
        {
            DataRowView drv = glkpAssetName.GetSelectedDataRow() as DataRowView;
            using (LocationSystem assetLocationSystem = new LocationSystem())
            {
                DataTable dtAssetitem = new DataTable();
                assetLocationSystem.ProjectId = (glkpFromProject.EditValue) != null ? this.UtilityMember.NumberSet.ToInteger(glkpFromProject.EditValue.ToString()) : 0;
                resultArgs = assetLocationSystem.FetchLocationByProject();
                if (resultArgs.Success && resultArgs != null)
                {
                    dtAssetitem = resultArgs.DataSource.Table;
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpFromLocation, dtAssetitem, assetLocationSystem.AppSchema.ASSETLocationDetails.LOCATIONColumn.ColumnName, assetLocationSystem.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName);
                    // glkpFromLocation.EditValue = glkpFromLocation.Properties.GetKeyValue(0);
                    //  LoadFromLocationGrid();
                }
            }
            //LoadFromLocationGrid();
        }

        private void gcToLocationAssets_Click(object sender, EventArgs e)
        {

        }

        private void LoardProjectDetails()
        {
            using (ProjectSystem projectSystem = new ProjectSystem())
            {
                resultArgs = projectSystem.FetchProjects();
                if (resultArgs.Success && resultArgs != null)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpFromProject, resultArgs.DataSource.Table, projectSystem.AppSchema.Project.PROJECTColumn.ColumnName, projectSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                    glkpFromProject.EditValue = glkpFromProject.Properties.GetKeyValue(0);
                }
            }
        }
        private void LoardProjectDetail()
        {
            using (ProjectSystem projectSystem = new ProjectSystem())
            {
                resultArgs = projectSystem.FetchProjects();
                if (resultArgs.Success && resultArgs != null)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpToProject, resultArgs.DataSource.Table, projectSystem.AppSchema.Project.PROJECTColumn.ColumnName, projectSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                    // glkpToProject.EditValue = glkpToProject.Properties.GetKeyValue(0);
                }
            }
        }

        private void glkpToProject_EditValueChanged(object sender, EventArgs e)
        {
            //  DataRowView drv = glkpAssetName.GetSelectedDataRow() as DataRowView;
            using (LocationSystem assetLocationSystem = new LocationSystem())
            {
                DataTable dtAssetitem = new DataTable();
                assetLocationSystem.ProjectId = (glkpToProject.EditValue) != null ? this.UtilityMember.NumberSet.ToInteger(glkpToProject.EditValue.ToString()) : 0;
                resultArgs = assetLocationSystem.FetchLocationByProject();
                if (resultArgs.Success && resultArgs != null)
                {
                    dtAssetitem = resultArgs.DataSource.Table;
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpToLocation, dtAssetitem, assetLocationSystem.AppSchema.ASSETLocationDetails.LOCATIONColumn.ColumnName, assetLocationSystem.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName);
                    // glkpFromLocation.EditValue = glkpFromLocation.Properties.GetKeyValue(0);
                    //  LoadFromLocationGrid();
                    gcToLocationAssets.DataSource = null;
                }
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rchkToLocation_CheckedChanged(object sender, EventArgs e)
        {
            string assetid = string.Empty;
            CheckEdit chkasset = (CheckEdit)sender;
            assetid = gvToLocationAssets.GetFocusedRowCellValue(colToAssetName) != null ?
              gvToLocationAssets.GetFocusedRowCellValue(colToAssetName).ToString() : string.Empty;

            if (AssetIDCollection.Contains(assetid.ToString()))
            {
                gvToLocationAssets.SetFocusedRowCellValue(colToSelect, 0);
                this.ShowMessageBox("Default Assets cannot be moved");
            }
        }
    }
}
