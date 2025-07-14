using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bosco.Utility;
using Bosco.Model.ASSET;

namespace ACPP.Modules.Asset.Masters
{
    public partial class frmAssetItemView : frmBase
    {
        public frmAssetItemView()
        {
            InitializeComponent();
        }

        #region Properties

        private int Rowindex = 0;
        private int itemId = 0;
        private int Item_Id
        {
            get
            {
                Rowindex = gvAssetItems.FocusedRowHandle;
                itemId = gvAssetItems.GetFocusedRowCellValue(colId) != null ? this.UtilityMember.NumberSet.ToInteger(gvAssetItems.GetFocusedRowCellValue(colId).ToString()) : 0;
                return itemId;
            }
            set
            {
                Item_Id = value;
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

        private void gcAssetItems_Click(object sender, EventArgs e)
        {

        }

        private void frmAssetItemView_Load(object sender, EventArgs e)
        {
            LoadAssetItemDetails();

        }

        private void gvAssetItems_RowCountChanged(object sender, EventArgs e)
        {
            lblCountNumber.Text = gvAssetItems.RowCount.ToString();
        }

        #endregion

        #region Methods
        /// <summary>
        /// Load asset item details in the grid to view.
        /// </summary>
        private void LoadAssetItemDetails()
        {
            using (AssetItemSystem assetItemSystem = new AssetItemSystem())
            {
                resultArgs = assetItemSystem.FetchAssetItemDetails();
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    gcAssetItems.DataSource = resultArgs.DataSource.Table;
                }
            }
        }
        /// <summary>
        /// Open edit form based on the ItemId.
        /// </summary>
        private void ShowForm()
        {
            if (gvAssetItems.RowCount > 0)
            {
                if (Item_Id > 0)
                {
                    ShowAssetItemForm(Item_Id);
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
                if (Item_Id > 0)
                {
                    using (AssetItemSystem assetItemSystem = new AssetItemSystem())
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            assetItemSystem.ItemId = Item_Id;
                            resultArgs = assetItemSystem.DeleteAssetItem(Item_Id);
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
    }
}
