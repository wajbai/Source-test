using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Utility;
using Bosco.Model;
using DevExpress.XtraTreeList.Nodes;
using Bosco.DAO.Schema;
using ACPP.Modules.Data_Utility;
using DevExpress.XtraBars;



namespace ACPP.Modules.Inventory
{
    public partial class frmStockGroupView : frmFinanceBase
    {
        #region Varialbe Decelaration
        int RowIndex = 0;
        TreeListNode Selectednode = null;
        AppSchemaSet appSchema = new AppSchemaSet();
        //public FinanceModule module { get; set; }
        // AssetStockProduct.IGroup Igroup;

        #endregion

        #region Event Decelaration
        public event EventHandler UpdateHeld;
        private int TempAsseClassId { get; set; }
        #endregion

        #region Properties
        public int StockGroupId
        {
            get
            {
                return (trlStockGroup.FocusedNode != null) ? this.UtilityMember.NumberSet.ToInteger(trlStockGroup.FocusedNode.GetValue(trlStockGroup.KeyFieldName).ToString()) : 0;
            }
        }
        #endregion

        #region Costurctor
        public frmStockGroupView()
        {
            //module = mod;
            // Igroup = AssetStockFactory.GetGroupInstance(mod);
            InitializeComponent();
        }
        #endregion

        #region Events

        private void ucAssetGroups_AddClicked(object sender, EventArgs e)
        {
            ShowStockGroupDetails(StockGroupId, FormMode.Add);
        }

        private void ucAssetGroups_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucAssetGroups_EditClicked(object sender, EventArgs e)
        {
            ShowStockGroupEdit();
        }

        private void ucAssetGroups_DeleteClicked(object sender, EventArgs e)
        {
            DeleteStockGroupDetails();
        }

        private void frmGroupView_Load(object sender, EventArgs e)
        {
            SetVisibileShortCuts(false, true);
            LoadStockGroupDetails();
            SetTitle();

            //Set Visible to Add/Edit/Delete
            LockMasters(ucAssetClass);
        }

        private void ucAssetGroups_PrintClicked(object sender, EventArgs e)
        {
            //trlGroup.OptionsPrint.PrintAllNodes = true;
            //trlGroup.OptionsPrint.PrintPageHeader = true;
            //trlGroup.OptionsPrint.PrintReportFooter = false;
            //trlGroup.OptionsPrint.PrintImages = false;
            //trlGroup.OptionsPrint.PrintTree = true;
            //trlGroup.OptionsPrint.PrintHorzLines = false;
            //trlGroup.OptionsPrint.PrintVertLines = false;
            // trlGroup.ShowRibbonPrintPreview();
            ResultArgs resultArgs = null;
            using (AssetItemSystem assetItemSystem = new AssetItemSystem())
            {
                resultArgs = assetItemSystem.FetchAllAssetItems();
                if (resultArgs != null && resultArgs.Success)
                {
                    gcAssetClassView.DataSource = resultArgs.DataSource.Table;
                    gvAssetClassView.RefreshData();
                    PrintGridViewDetails(gcAssetClassView, "Stock Group Details", PrintType.DT, gvAssetClassView, true);
                }
            }
        }

        private void ucAssetGroups_RefreshClicked(object sender, EventArgs e)
        {
            RefreshTreeView();
        }

        private void gcAssetGroup_DoubleClick(object sender, EventArgs e)
        {
            ShowStockGroupEdit();
        }

        private void gvGroupView_RowCountChanged(object sender, EventArgs e)
        {
            lblCountNumber.Text = gvAssetClassView.RowCount.ToString();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvAssetClassView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvAssetClassView, colStockGroup);
            }
        }

        //private void trlGroup_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        //{
        //    string nodeList = string.Empty;
        //    string nodeGroupList = string.Empty;
        //    if (trlAssetClass.FocusedNode.Nodes.Count > 0)
        //    {
        //        int tmpparent = 0;
        //        foreach (TreeListNode child in trlAssetClass.FocusedNode.Nodes)
        //        {
        //            nodeList += child.GetValue(this.appSchema.AppSchema.ASSETClassDetails.ASSET_CLASS_IDColumn.ToString()) + ",";
        //            nodeGroupList += child.GetValue(this.appSchema.AppSchema.ASSETClassDetails.ASSET_CLASS_IDColumn.ToString()) + ",";
        //            if (tmpparent == 0)
        //            {
        //                nodeGroupList += child.GetValue(this.appSchema.AppSchema.ASSETClassDetails.PARENT_CLASS_IDColumn.ToString()) + ",";
        //                tmpparent++;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        nodeList = trlAssetClass.FocusedNode.GetValue(this.appSchema.AppSchema.ASSETClassDetails.ASSET_CLASS_IDColumn.ToString()).ToString();
        //        nodeGroupList = trlAssetClass.FocusedNode.GetValue(this.appSchema.AppSchema.ASSETClassDetails.ASSET_CLASS_IDColumn.ToString()).ToString();
        //    }
        //    if (!string.IsNullOrEmpty(nodeList))
        //    {
        //        LoadGroupList(nodeGroupList.TrimEnd(','));
        //    }
        //    lblSelectedGroup.Text=FetchSelectedClass() + "-Asset Items";
        //}

        private string FetchSelectedClass()
        {
            ResultArgs resultArgs = new ResultArgs();
            string SelectedGroup = string.Empty;
            try
            {
                using (StockGroupSystem stockgroupSystem = new StockGroupSystem())
                {
                    stockgroupSystem.StockGroupId = StockGroupId;
                    resultArgs = stockgroupSystem.FetchGroupDetailsById();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        SelectedGroup = resultArgs.DataSource.Table.Rows[0]["GROUP_NAME"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
            return SelectedGroup;
        }

        private void trlAssetClass_DoubleClick(object sender, EventArgs e)
        {
            ShowStockGroupEdit();
        }

        private void frmGroupView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void ucAssetGroups_DownloadExcel(object sender, EventArgs e)
        {
            using (frmExcelSupport excelSupport = new frmExcelSupport("Group", MasterImport.Group))
            {
                excelSupport.UpdateHeld += new EventHandler(OnUpdateHeld);
                excelSupport.ShowDialog();
            }
        }

        private void frmGroupView_Activated(object sender, EventArgs e)
        {
            SetVisibileShortCuts(true, true);
            LoadStockGroupDetails();
        }
        #endregion

        #region Methods

        private void LoadStockGroupDetails()
        {
            try
            {
                ResultArgs resultArgs = null;
                using (StockGroupSystem stockgroupSystem = new StockGroupSystem())
                {
                    resultArgs = stockgroupSystem.FetchStockGroupDetails();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        DataTable dtStockGroup = resultArgs.DataSource.Table;
                        trlStockGroup.DataSource = dtStockGroup;
                    }
                    else
                    {
                        trlStockGroup.DataSource = null;
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

        private void DeleteStockGroupDetails()
        {
            try
            {
                ResultArgs resultArgs = null;
                if (trlStockGroup.Nodes.Count > 0)
                {
                    if (trlStockGroup.FocusedNode.Nodes.Count == 0)
                    {
                        if (StockGroupId > 0)
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                using (StockGroupSystem StockGroupsystem = new StockGroupSystem())
                                {
                                    StockGroupsystem.StockGroupId = StockGroupId;
                                    resultArgs = StockGroupsystem.DeleteGroupDetails();
                                    if (resultArgs.Success)
                                    {
                                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                        RefreshTreeView();
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
                        XtraMessageBox.Show(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_FAILURE), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally
            { }
        }

        private void ShowStockGroupEdit()
        {
            if (this.AppSetting.LockMasters == (int)YesNo.No)
            {
                if (ucAssetClass.VisibleEditButton == BarItemVisibility.Always)
                {
                    if (trlStockGroup.Nodes.Count > 0)
                    {
                        if (StockGroupId > 0)
                        {
                            ShowStockGroupDetails(StockGroupId, FormMode.Edit);
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
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
            }
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            RefreshTreeView();
        }

        private void ShowStockGroupDetails(int GroupId, FormMode FrmMode)
        {
            try
            {
                frmStockGroupsAdd StockGroup = new frmStockGroupsAdd(GroupId, FrmMode);
                StockGroup.UpdateHeld += new EventHandler(OnUpdateHeld);
                TempAsseClassId = StockGroup.GroupId;
                StockGroup.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            { }
        }

        private void LoadStockGroupList(string GroupId)
        {
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                using (StockGroupSystem StockGroupSystem = new StockGroupSystem())
                {
                    gcAssetClassView.DataSource = null;
                    StockGroupSystem.GroupId = GroupId;
                    resultArgs = StockGroupSystem.FetchSelectedGroupDetails();
                    if (resultArgs.Success && resultArgs != null)
                    {
                        gcAssetClassView.DataSource = resultArgs.DataSource.Table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }
            finally { }
        }

        private void RefreshTreeView()
        {
            try
            {
                //int ParentId = 0;
                //Selectednode = (TreeListNode)trlGroup.FocusedNode;
                //LoadAssetGroupDetails();
                //if (Selectednode.ParentNode == null)
                //{
                //    ParentId = Selectednode.Id;
                //}
                //else
                //{
                //    ParentId = (Selectednode.ParentNode.Id > trlGroup.Nodes.Count) ? Selectednode.ParentNode.ParentNode.Id : Selectednode.ParentNode.Id;
                //}
                //trlGroup.FocusedNode = trlGroup.Nodes[AssetGroupId];
                //trlGroup.FocusedNode.ExpandAll();
                LoadStockGroupDetails();
                if (TempAsseClassId > 0)
                {
                    trlStockGroup.FocusedColumn = trlcolName;
                    trlStockGroup.CollapseAll();
                    TreelistIterator treeListIterator = new TreelistIterator(TempAsseClassId.ToString(), "ASSET_CLASS_ID");
                    trlStockGroup.NodesIterator.DoOperation(treeListIterator);
                    trlStockGroup.FocusedNode = treeListIterator.MyNode;
                    //if (trlGroup.FocusedNode != null)
                    //    trlGroup.FocusedNode.Expanded = true;
                }
            }
            catch (Exception ex)
            {
                ShowMessageBoxError(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        public void SetTitle()
        {
            this.Text = this.GetMessage(MessageCatalog.Stock.StockGroup.STOCKGROUP_VIEW_CAPTION);
        }

        //Set Visible to Add/Edit/Delete

        private void Hide()
        {
            ucAssetClass.VisibleAddButton = ucAssetClass.VisibleDeleteButton = ucAssetClass.VisibleEditButton = ucAssetClass.VisibleMoveTrans = BarItemVisibility.Never;
            ucAssetClass.VisiblePrintButton = BarItemVisibility.Always;
        }

        #endregion

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void trlAssetClass_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            string nodeList = string.Empty;
            string nodeStockGroupList = string.Empty;
            if (trlStockGroup.FocusedNode.Nodes.Count > 0)
            {
                int tmpparent = 0;
                foreach (TreeListNode child in trlStockGroup.FocusedNode.Nodes)
                {
                    nodeList += child.GetValue(this.appSchema.AppSchema.StockGroup.GROUP_IDColumn.ToString()) + ",";
                    nodeStockGroupList += child.GetValue(this.appSchema.AppSchema.StockGroup.GROUP_IDColumn.ToString()) + ",";
                    if (tmpparent == 0)
                    {
                        nodeStockGroupList += child.GetValue(this.appSchema.AppSchema.StockGroup.PARENT_GROUP_IDColumn.ToString()) + ",";
                        tmpparent++;
                    }
                }
            }
            else
            {
                nodeList = trlStockGroup.FocusedNode.GetValue(this.appSchema.AppSchema.StockGroup.GROUP_IDColumn.ToString()).ToString();
                nodeStockGroupList = trlStockGroup.FocusedNode.GetValue(this.appSchema.AppSchema.StockGroup.GROUP_IDColumn.ToString()).ToString();
            }
            if (!string.IsNullOrEmpty(nodeList))
            {
                LoadStockGroupList(nodeStockGroupList.TrimEnd(','));
            }
            lblSelectedGroup.Text = FetchSelectedClass().Contains("&") ? FetchSelectedClass().Replace("&", "&&") + " - Stock Item" : FetchSelectedClass() + " - Stock Item";
        }

        private void gvAssetClassView_RowCountChanged(object sender, EventArgs e)
        {
            lblCountNumber.Text = gvAssetClassView.RowCount.ToString();
        }

        private void gvAssetClassView_DoubleClick(object sender, EventArgs e)
        {
            //ShowAssetClassEdit();
        }

        private void frmAssetClassView_EnterClicked(object sender, EventArgs e)
        {
            ShowStockGroupEdit();
        }

        /// <summary>
        /// Stock Views
        /// </summary>
        private void ApplyUserRights()
        {
            this.enumUserRights.Add(StockGroup.CreateStockGroup);
            this.enumUserRights.Add(StockGroup.EditStockGroup);
            this.enumUserRights.Add(StockGroup.DeleteStockGroup);
            this.enumUserRights.Add(StockGroup.ViewStockGroup);
            this.ApplyUserRights(ucAssetClass, enumUserRights, (int)Menus.StockGroup);
        }
    }
}
