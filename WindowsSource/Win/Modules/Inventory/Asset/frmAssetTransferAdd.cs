using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bosco.Utility;
using Bosco.Model.Inventory.Asset;
using Bosco.Model;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmAssetTransferAdd : frmBaseAdd
    {
        #region Variable Declearation
        int VoucherId = 0;
        int TransferID = 0;
        ResultArgs resultArgs = null;
        public event EventHandler UpdateHeld;
        #endregion

        #region Constructors
        public frmAssetTransferAdd()
        {
            InitializeComponent();
        }

        public frmAssetTransferAdd(int TransferId)
            : this()
        {
            TransferID = TransferId;
        }
        #endregion

        #region Events
        private void frmAssetTransferAdd_Load(object sender, EventArgs e)
        {
            SetTitle();
            LoadLocationDetails();
            LoadGroupDetails();
            AssigntoProperties();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidTransfer())
                {
                    using (TransferVoucherSystem objAssetTransferSystem = new TransferVoucherSystem())
                    {
                        objAssetTransferSystem.TransferId = TransferID;
                        objAssetTransferSystem.GroupId = this.UtilityMember.NumberSet.ToInteger(gklpGroup.EditValue.ToString());
                        objAssetTransferSystem.ItemId = this.UtilityMember.NumberSet.ToInteger(gklpAssetItem.EditValue.ToString());
                        objAssetTransferSystem.AssetID = txtAssetId.Text.ToString();
                        objAssetTransferSystem.ToLocationId = this.UtilityMember.NumberSet.ToInteger(gklpTolocation.EditValue.ToString());
                        objAssetTransferSystem.FromLocationId = gklpFromLocation.EditValue!=null?this.UtilityMember.NumberSet.ToInteger(gklpFromLocation.EditValue.ToString()):0;
                        objAssetTransferSystem.TransfetrDate = this.UtilityMember.DateSet.ToDate(dtDate.EditValue.ToString(), false);
                        objAssetTransferSystem.Amount = this.UtilityMember.NumberSet.ToDecimal(txtAmount.Text);
                        objAssetTransferSystem.Narration = this.meRemarks.Text;
                        //objAssetTransferSystem.RefrenceId = this.UtilityMember.NumberSet(;
                        resultArgs = null;// objAssetTransferSystem.SaveTransferDetails();

                        if (resultArgs != null && resultArgs.Success)
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
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Source + Environment.NewLine + ex.Message);
            }
        }

        private void gklpGroup_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(gklpGroup);
        }

        private void gklpGroup_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                using (TransferVoucherSystem objTransferSystem = new TransferVoucherSystem())
                {
                    int GroupId = this.UtilityMember.NumberSet.ToInteger(gklpGroup.EditValue.ToString()) != 0 ? this.UtilityMember.NumberSet.ToInteger(gklpGroup.EditValue.ToString()) : 0;
                    objTransferSystem.GroupId = GroupId;
                    resultArgs = objTransferSystem.fetchAssetDetailsById();
                    if (resultArgs.Success && resultArgs != null)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(gklpAssetItem, resultArgs.DataSource.Table, objTransferSystem.AppSchema.ASSETItem.ASSET_NAMEColumn.ColumnName, objTransferSystem.AppSchema.ASSETItem.ITEM_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
            finally { }
        }

        private void gklpAssetItem_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                resultArgs = null;
                int ItemId = this.UtilityMember.NumberSet.ToInteger(gklpAssetItem.EditValue.ToString()) != 0 ? this.UtilityMember.NumberSet.ToInteger(gklpAssetItem.EditValue.ToString()) : 0;
                using (AssetItemSystem itemSystem = new AssetItemSystem())
                {
                    itemSystem.ItemId = ItemId;
                    //resultArgs = itemSystem.FetchItemLocationDetailsById();
                    //if (resultArgs.Success && resultArgs != null)
                    //{
                    //    txtAssetId.Text = resultArgs.DataSource.Table.Rows[0]["ITEM_ID"].ToString();
                    //    gklpFromLocation.EditValue = resultArgs.DataSource.Table.Rows[0]["LOCATION_ID"].ToString();
                    //}
                }
            }
            catch (Exception ex)
            {
            }
            finally { }
        }

        private void gklpAssetItem_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(gklpAssetItem);
        }

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtAmount);
        }

        private void gklpTolocation_Leave_1(object sender, EventArgs e)
        {
            this.SetBorderColor(gklpTolocation);
        }
        #endregion

        #region Methods

        private void ClearControls()
        {
            if (VoucherId == 0)
            {
                txtAmount.Text = txtAssetId.Text = gklpAssetItem.Text = meRemarks.Text = gklpGroup.Text = txtAmount.Text = gklpFromLocation.Text = gklpTolocation.Text = string.Empty;
                this.dtDate.Focus();
            }
            else
            {
                this.Close();
            }
        }

        private bool IsValidTransfer()
        {
            bool isvalidTransfer = true;
            if (string.IsNullOrEmpty(dtDate.Text))
            {
                this.SetBorderColor(dtDate);
                this.dtDate.Focus();
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.TransferVoucher.TRANSFER_DATE_EMPTY));
                isvalidTransfer = false;
            }
            else if (string.IsNullOrEmpty(gklpGroup.Text))
            {
                this.SetBorderColor(gklpGroup);
                this.gklpGroup.Focus();
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.TransferVoucher.TRANSFER_ASSETITEM_EMPTY));
                isvalidTransfer = false;
            }
            else if (string.IsNullOrEmpty(gklpAssetItem.Text))
            {
                this.SetBorderColor(gklpAssetItem);
                this.gklpAssetItem.Focus();
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.TransferVoucher.TRANSFER_GROUP_EMPTY));
                isvalidTransfer = false;
            }
            else if (string.IsNullOrEmpty(txtAmount.Text))
            {
                this.SetBorderColor(txtAmount);
                this.txtAmount.Focus();
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.TransferVoucher.TRANSFER_AMOUNT_EMPTY));
                isvalidTransfer = false;
            }
            else if (string.IsNullOrEmpty(gklpTolocation.Text))
            {
                this.SetBorderColor(gklpTolocation);
                this.gklpTolocation.Focus();
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.TransferVoucher.TRANSFER_TOLOCATION_EMPTY));
                isvalidTransfer = false;
            }
            return isvalidTransfer;
        }

        private void SetTitle()
        {
            this.Text = TransferID == 0 ? this.GetMessage(MessageCatalog.Asset.TransferVoucher.TRANSFER_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.TransferVoucher.TRANSFER_EDIT_CAPTION);
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
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(gklpFromLocation, resultArgs.DataSource.Table, LocationSystem.AppSchema.ASSETLocationDetails.LOCATION_NAMEColumn.ColumnName, LocationSystem.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName);
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(gklpTolocation, resultArgs.DataSource.Table, LocationSystem.AppSchema.ASSETLocationDetails.LOCATION_NAMEColumn.ColumnName, LocationSystem.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName);
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
                using (AssetGroupSystem objAssetGroupSystem = new AssetGroupSystem())
                {
                    resultArgs = objAssetGroupSystem.FetchGroupDetails();
                    if (resultArgs.Success && resultArgs != null)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(gklpGroup, resultArgs.DataSource.Table, objAssetGroupSystem.AppSchema.ASSETGroupDetails.GROUP_NAMEColumn.ColumnName, objAssetGroupSystem.AppSchema.ASSETGroupDetails.GROUP_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
            finally { }
        }

        private void AssigntoProperties()
        {
            try
            {
                if (TransferID > 0)
                {
                    using (TransferVoucherSystem transferSystem = new TransferVoucherSystem(TransferID))
                    {
                        gklpGroup.EditValue = transferSystem.GroupId;
                        gklpTolocation.EditValue = transferSystem.ToLocationId;
                        gklpAssetItem.EditValue = transferSystem.AssetID;
                        txtAssetId.Text = transferSystem.AssetID.ToString();
                        txtAmount.Text = transferSystem.Amount.ToString();
                        gklpFromLocation.Text = transferSystem.FromLocationId.ToString();
                        meRemarks.Text = transferSystem.Narration;
                        dtDate.Text = this.UtilityMember.DateSet.ToDate(transferSystem.TransfetrDate.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message); ;
            }
            finally { }
        }
        #endregion
    }
}

