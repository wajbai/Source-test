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
using DevExpress.XtraGrid.Views.Grid;
using ACPP.Modules.Data_Utility;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmAssetItemView : frmFinanceBase
    {
        #region Variable Decelaration
        public FinanceModule module { get; set; }
        #endregion

        #region Constructor
        public frmAssetItemView()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        private int Rowindex = 0;
        private int itemId = 0;
        private int ItemId
        {
            get
            {
                Rowindex = gvAssetItems.FocusedRowHandle;
                itemId = gvAssetItems.GetFocusedRowCellValue(colId) != null ? this.UtilityMember.NumberSet.ToInteger(gvAssetItems.GetFocusedRowCellValue(colId).ToString()) : 0;
                return itemId;
            }
            set
            {
                ItemId = value;
            }
        }

        ResultArgs resultArgs = null;
        #endregion

        #region Events

        private void ucAssetItemsView_AddClicked(object sender, EventArgs e)
        {
            ShowAssetItemForm((int)AddNewRow.NewRow);
        }

        private void ucAssetItemsView_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucAssetItemsView_DeleteClicked(object sender, EventArgs e)
        {
            DeleteAssetItemDetails();
        }

        private void ucAssetItemsView_EditClicked(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void gvAssetItems_DoubleClick(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void frmAssetItemView_Load(object sender, EventArgs e)
        {
            //  SetVisibileShortCuts(true, true, true);
            LoadAssetItemDetails();
            SetTitle();
            ApplyUserRights();

            // this.LockMasters(ucAssetItemsView);
        }

        private void gvAssetItems_RowCountChanged(object sender, EventArgs e)
        {
            lblCountNumber.Text = gvAssetItems.RowCount.ToString();
        }

        private void ucAssetItemsView_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcAssetItems, this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETITEM_PRINT_CAPTION), PrintType.DS, gvAssetItems, true);
        }

        private void ucAssetItemsView_RefreshClicked(object sender, EventArgs e)
        {
            LoadAssetItemDetails();
        }
        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvAssetItems.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvAssetItems, colAssetItem);
            }
        }

        private void frmAssetItemView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Load asset item details in the grid to view.
        /// </summary>
        private void LoadAssetItemDetails()
        {
            DataSet dsVoucher = new DataSet();
            using (AssetItemSystem assetItemSystem = new AssetItemSystem())
            {
                dsVoucher = assetItemSystem.FetchAssetItemMasterDetail();
                if (dsVoucher.Tables.Count != 0)
                {
                    gcAssetItems.DataSource = dsVoucher;
                    gcAssetItems.DataMember = "Master";
                    gcAssetItems.RefreshDataSource();
                }
                else
                {
                    gcAssetItems.DataSource = null;
                    gcAssetItems.RefreshDataSource();
                }
                gvAssetItems.FocusedRowHandle = 0;
                gvAssetItems.FocusRectStyle = DrawFocusRectStyle.RowFocus;
            }
        }
        /// <summary>
        /// Open edit form based on the ItemId.
        /// </summary>
        private void ShowForm()
        {
            if (this.isEditable)
            {
                // if (this.AppSetting.LockMasters == (int)YesNo.No)
                // {
                if (gvAssetItems.RowCount > 0)
                {
                    if (ItemId > 0)
                    {
                        ShowAssetItemForm(ItemId);
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NOSELECTION_FOR_EDIT));
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                }
                // }
                // else
                //  {
                //   this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
                //  }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_USER_RIGHTS));
            }
        }
        /// <summary>
        /// Item Id is sent to the AssetItemAdd constructor to do the Edit option.
        /// </summary>
        /// <param name="ItemId"></param>
        private void ShowAssetItemForm(int ItemId)
        {
            try
            {
                frmAssetItemAdd frmAssetItemAdd = new frmAssetItemAdd(ItemId);
                frmAssetItemAdd.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmAssetItemAdd.ShowDialog();
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
            finally { }
        }
        /// <summary>
        /// After saving an asset item records are refreshed in the view form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadAssetItemDetails();
        }
        /// <summary>
        /// Deleter asset item details by the ItemId.
        /// </summary>
        public void DeleteAssetItemDetails()
        {
            if (gvAssetItems.RowCount > 0)
            {
                if (ItemId > 0)
                {
                    using (AssetItemSystem AssetItemSystem = new AssetItemSystem())
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            AssetItemSystem.ItemId = ItemId;
                            resultArgs = AssetItemSystem.DeleteAssetItem();
                            if (resultArgs.Success && resultArgs != null)
                            {
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                LoadAssetItemDetails();
                            }
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NOSELECTION_FOR_DELETE));
                }

            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
            }
        }

        #endregion

        private void frmAssetItemView_Activated(object sender, EventArgs e)
        {
            SetVisibileShortCuts(false, true);
            LoadAssetItemDetails();
        }

        private void ucAssetItemsView_DownloadExcel(object sender, EventArgs e)
        {
            //using (frmExcelSupport excelSupport = new frmExcelSupport("Item", MasterImport.Item, module))
            using (frmExcelSupport excelSupport = new frmExcelSupport(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSET_ITEM_CAPTION), MasterImport.Item, module))
            {
                excelSupport.UpdateHeld += new EventHandler(OnUpdateHeld);
                excelSupport.ShowDialog();
            }
        }

        private void SetTitle()
        {
            //this.Text = "Asset Item";
            this.GetMessage(MessageCatalog.Asset.AssetItem.ASSET_ITEM_VIEW);
        }

        private void frmAssetItemView_EnterClicked(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(AssetItem.CreateItem);
            this.enumUserRights.Add(AssetItem.EditItem);
            this.enumUserRights.Add(AssetItem.DeleteItem);
            this.enumUserRights.Add(AssetItem.ViewItem);
            this.ApplyUserRights(ucAssetItemsView, enumUserRights, (int)Menus.AssetItem);

        }
    }
}
