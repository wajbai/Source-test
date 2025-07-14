using System;
using System.Data;
using System.Drawing;
using Bosco.Model;
using Bosco.Utility;
using DevExpress.XtraTreeList.Nodes;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmAssetDashboard : frmFinanceBase
    {
        #region Constructor
        public frmAssetDashboard()
        {
            InitializeComponent();
        }
        #endregion

        #region VariableDeclaration
        private DataTable dtAssetItemDetail = new DataTable();
        TreeListNode Selectednode = null;
        #endregion

        #region Properties
        public int LocationID
        {
            get
            {
                int locationId = 0;
                locationId = gvAssetDashboard.GetFocusedRowCellValue(colLocationId) != null ? this.UtilityMember.NumberSet.ToInteger(gvAssetDashboard.GetRowCellValue(gvAssetDashboard.FocusedRowHandle, colLocationId).ToString()) : 0;
                return locationId;
            }
        }
        #endregion

        #region Events

        private void frmAssetDashboard_Load(object sender, EventArgs e)
        {
            LoadAssetItemDetails();
            LoadAssetLocationDetails();
        }

        private void gvAssetDashboard_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (LocationID > 0)
            {
                trlLocation.FocusedColumn = colLocation;
                trlLocation.CollapseAll();
                TreelistIterator treeListIterator = new TreelistIterator(LocationID.ToString(), "LOCATION_ID");
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

        private void gvAssetDashboard_RowCountChanged(object sender, EventArgs e)
        {
            lblCount.Text = gvAssetDashboard.RowCount.ToString();
        }

        private void ucToolBar1_RefreshClicked(object sender, EventArgs e)
        {
            LoadAssetItemDetails();
            LoadAssetLocationDetails();
        }

        private void ucToolBar1_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
        
        #region Methods

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
                        //this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpLocation, dtLedgerGroup, LocationSystem.AppSchema.ASSETLocationDetails.LOCATION_NAMEColumn.ColumnName,
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

        private void ucToolBar1_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcAssetDashboard, this.GetMessage(MessageCatalog.Asset.AssetItem.ASSET_PRINT_CAPTION), PrintType.DT, gvAssetDashboard, true);
        }

        private void gvAssetItems_RowCountChanged(object sender, EventArgs e)
        {
            lblCount.Text = gvAssetDashboard.RowCount.ToString();
        }

        private void LoadLocationList(string Locationid)
        {
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                using (AssetItemSystem assetItemSystem = new AssetItemSystem())
                {
                      gcAssetDashboard.DataSource = null;
                   // resultArgs = assetItemSystem.FetchAssetItemDetailByLocation(Locationid);
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtLocation = resultArgs.DataSource.Table;
                        gcAssetDashboard.DataSource = dtLocation;
                        gcAssetDashboard.RefreshDataSource();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }

        private void LoadAssetItemDetails()
        {
            DataSet dsVoucher = new DataSet();
            using (AssetItemSystem assetItemSystem = new AssetItemSystem())
            {
                ResultArgs result = assetItemSystem.FetchAssetItemDetailAll();
                if (result != null && result.Success)
                {
                    gcAssetDashboard.DataSource = dtAssetItemDetail = result.DataSource.Table;
                    gcAssetDashboard.RefreshDataSource();
                }
            }
        }

         #endregion
    }
}