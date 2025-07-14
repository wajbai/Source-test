using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;

using Bosco.Model;
using Bosco.Utility;
using DevExpress.XtraGrid.Views.Grid;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmAssetSearch : frmFinanceBase
    {
        #region Varialbe Decelaration
        TreeListNode Selectednode = null;
        FormMode FrmMode;
        private DataTable dtAssetItemDetail = new DataTable();
        #endregion

        private int LocationId
        {
            get
            {
                int locationId = 0;
                locationId = gvAssetItems.GetFocusedRowCellValue(colLocationID) != null ? this.UtilityMember.NumberSet.ToInteger(gvAssetItems.GetRowCellValue(gvAssetItems.FocusedRowHandle, colLocationID).ToString()) : 0;
                return locationId;
            }
        }

        #region Constructor
        public frmAssetSearch()
        {
            InitializeComponent();
        }
        #endregion

        #region Events

        private void frmLocationsView_Load(object sender, EventArgs e)
        {
            LoadAssetItemDetails();
            LoadAssetLocationDetails();
        }

        private void uctbLocationView_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void uctbLocationView_PrintClicked(object sender, EventArgs e)
        {
             PrintGridViewDetails(gcAsset, this.GetMessage(MessageCatalog.Asset.AssetItem.ASSET_PRINT_CAPTION), PrintType.DT, gvAssetItemDetail, true);
        }

        private void uctbLocationView_RefreshClicked(object sender, EventArgs e)
        {
            LoadAssetItemDetails();
            LoadAssetLocationDetails();
        }

        private void trlLocation_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            using (LocationSystem LocationSystem = new LocationSystem())
            {
                string nodeList = string.Empty;
                string nodeLocationList = string.Empty;
                if (trlLocation.FocusedNode.Nodes.Count > 0)
                {
                    int tmpparent = 0;
                    foreach (TreeListNode child in trlLocation.FocusedNode.Nodes)
                    {
                        nodeList += child.GetValue(LocationSystem.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ToString()) + ",";
                        nodeLocationList += child.GetValue(LocationSystem.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ToString()) + ",";
                        if (tmpparent == 0)
                        {
                            //nodeLocationList += child.GetValue(LocationSystem.AppSchema.ASSETLocationDetails.PARENT_LOCATION_IDColumn.ToString()) + ",";
                            tmpparent++;
                        }
                    }
                }
                else
                {
                    nodeList = trlLocation.FocusedNode.GetValue(LocationSystem.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ToString()).ToString();
                    nodeLocationList = trlLocation.FocusedNode.GetValue(LocationSystem.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ToString()).ToString();
                }
                if (!string.IsNullOrEmpty(nodeList))
                {
                    LoadLocationList(nodeLocationList.TrimEnd(','));
                }
            }
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            //lvAssetItems.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            // if (chkShowFilter.Checked)
            // {
            //     this.SetFocusRowFilter(gvAssetItems, gccolName);
            // }
        }

        private void gvItemView_RowCountChanged(object sender, EventArgs e)
        {
            // lblCount.Text = gvAssetItems.DataRowCount.ToString();
        }
        #endregion

        #region Methods
        //private void LoadGroupList(string Locationid)
        //{
        //    ResultArgs resultArgs = new ResultArgs();
        //    try
        //    {
        //        using (LocationSystem LocationSystem = new LocationSystem())
        //        {
        //            gcAssetItems.DataSource = null;
        //            LocationSystem.LocationId = LocationID;
        //            if (resultArgs.Success && resultArgs != null)
        //            {
        //                gcAssetItems.DataSource = resultArgs.DataSource.Table;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageRender.ShowMessage(ex.Message, true);
        //    }
        //    finally { }
        //}

        private void RefreshTreeView()
        {
            int ParentId = 0;
            Selectednode = (TreeListNode)trlLocation.FocusedNode;
            LoadAssetItemDetails();
            if (Selectednode.ParentNode == null)
            {
                ParentId = Selectednode.Id;
            }
            else
            {
                ParentId = (Selectednode.ParentNode.Id > trlLocation.Nodes.Count) ? Selectednode.ParentNode.ParentNode.Id : Selectednode.ParentNode.Id;
            }
            trlLocation.FocusedNode = trlLocation.Nodes[ParentId];
            trlLocation.FocusedNode.Expanded = true;
            //trlLocation.FocusedNode.ExpandAll();
        }

        private void LoadAssetLocationDetails()
        {
            try
            {
                using (LocationSystem LocationSystem = new LocationSystem())
                {
                    ResultArgs resultArgs = null;
                    trlLocation.DataSource = null;
                    resultArgs = LocationSystem.FetchLocationDetails();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        DataTable dtLedgerGroup = resultArgs.DataSource.Table;
                        trlLocation.DataSource = dtLedgerGroup;
                        //this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpLocation, dtLedgerGroup, LocationSystem.AppSchema.ASSETLocationDetails.LOCATIONColumn.ColumnName,
                        //    LocationSystem.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName, true);
                    }
                    else
                    {
                        trlLocation.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally
            { }
        }

        private void LoadAssetItemDetails()
        {
            DataSet dsVoucher = new DataSet();
            using (AssetItemSystem assetItemSystem = new AssetItemSystem())
            {
                ResultArgs result = assetItemSystem.FetchAssetItemDetailAll();
                if (result != null && result.Success)
                {
                    gcAsset.DataSource=dtAssetItemDetail = result.DataSource.Table;
                    gcAsset.RefreshDataSource();
                }
            }
        }

        private void LoadLocationList(string Locationid)
        {
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                using (AssetItemSystem assetItemSystem = new AssetItemSystem())
                {
                  //  gcAsset.DataSource = null;
                   // resultArgs = assetItemSystem.FetchAssetItemDetailByLocation(Locationid);
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtLocation = resultArgs.DataSource.Table;
                       //   gcAsset.DataSource = dtLocation;
                       //   gcAsset.RefreshDataSource();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }

        #endregion

        private void glkpLocation_EditValueChanged(object sender, EventArgs e)
        {
            //if (glkpLocation.EditValue != null)
            //{
            //    trlLocation.FocusedColumn = colNAME;
            //    //trlLocation.FocusedNode = trlLocation.Nodes[glkpLocation.];
            //    trlLocation.FocusedNode.Expanded = true;
            //   //trlLocation.FocusedNode = trlLocation.
            //   // trlLocation.foc
            //   // TreeListNode y=new TreeListNode();
            //    //trlLocation.focused = this.UtilityMember.NumberSet.ToInteger(glkpLocation.EditValue.ToString());
            //    //trlLocation.FocusedNode.Expanded = true;
            //}
        }

        private void gvAssetItems_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (LocationId > 0)
            {
                trlLocation.FocusedColumn = colNAME;
                trlLocation.CollapseAll();
                TreelistIterator treeListIterator = new TreelistIterator(LocationId.ToString(),"LOCATION_ID");
                trlLocation.NodesIterator.DoOperation(treeListIterator);
                trlLocation.FocusedNode = treeListIterator.MyNode;
                if (trlLocation.FocusedNode != null)
                {
                    trlLocation.FocusedNode.Expanded = true;
                    trlLocation.Focus();
                    trlLocation.Appearance.FocusedRow.ForeColor = Color.Black;
                    trlLocation.OptionsSelection.EnableAppearanceFocusedCell = true;
                    trlLocation.OptionsSelection.EnableAppearanceFocusedRow = true;
                    trlLocation.OptionsView.ShowFocusedFrame = false;
                }
            }
        }

        private void gvAssetItems_RowCountChanged(object sender, EventArgs e)
        {
              lblCount.Text = gvAssetItems.RowCount.ToString();
        }
    }
}
