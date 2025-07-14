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
    public partial class frmGroupView : frmBase
    {
        #region Varialbe Decelaration
        int RowIndex = 0;
        TreeListNode Selectednode = null;
        AppSchemaSet appSchema = new AppSchemaSet();
        public FinanceModule module { get; set; }
        AssetStockProduct.IGroup Igroup;

        #endregion

        #region Event Decelaration
        public event EventHandler UpdateHeld;
        private int TempAsseGroupId { get; set; }
        #endregion

        #region Properties
        public int AssetGroupId
        {
            get
            {
                return (trlGroup.FocusedNode != null) ? this.UtilityMember.NumberSet.ToInteger(trlGroup.FocusedNode.GetValue(trlGroup.KeyFieldName).ToString()) : 0;
            }
        }
        #endregion

        #region Costurctor
        public frmGroupView(FinanceModule mod)
        {
            module = mod;
            Igroup = AssetStockFactory.GetGroupInstance(mod);
            InitializeComponent();
        }
        #endregion

        #region Events

        private void ucAssetGroups_AddClicked(object sender, EventArgs e)
        {
            ShowAssetGroupDetails(AssetGroupId, FormMode.Add);
        }

        private void ucAssetGroups_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucAssetGroups_EditClicked(object sender, EventArgs e)
        {
            ShowAssetGroupEdit();
        }

        private void ucAssetGroups_DeleteClicked(object sender, EventArgs e)
        {
            DeleteAssetGroupDetails();
        }

        private void frmGroupView_Load(object sender, EventArgs e)
        {
            SetVisibileShortCuts(false, true);
            LoadAssetGroupDetails();
            SetTitle();

            //Set Visible to Add/Edit/Delete
            LockMasters(ucAssetGroups);
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
            PrintGridViewDetails(gcGroupView, this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_PRINT_CAPTION), PrintType.DT, gvGroupView, true);
        }

        private void ucAssetGroups_RefreshClicked(object sender, EventArgs e)
        {
            RefreshTreeView();
        }

        private void gcAssetGroup_DoubleClick(object sender, EventArgs e)
        {
            ShowAssetGroupEdit();
        }

        private void gvGroupView_RowCountChanged(object sender, EventArgs e)
        {
            lblCountNumber.Text = gvGroupView.RowCount.ToString();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvGroupView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvGroupView,colgroup);
            }
        }

        private void trlGroup_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            string nodeList = string.Empty;
            string nodeGroupList = string.Empty;
            if (trlGroup.FocusedNode.Nodes.Count > 0)
            {
                int tmpparent = 0;
                foreach (TreeListNode child in trlGroup.FocusedNode.Nodes)
                {
                    nodeList += child.GetValue(this.appSchema.AppSchema.ASSETClassDetails.ASSET_CLASS_IDColumn.ToString()) + ",";
                    nodeGroupList += child.GetValue(this.appSchema.AppSchema.ASSETClassDetails.ASSET_CLASS_IDColumn.ToString()) + ",";
                    if (tmpparent == 0)
                    {
                        nodeGroupList += child.GetValue(this.appSchema.AppSchema.ASSETClassDetails.PARENT_CLASS_IDColumn.ToString()) + ",";
                        tmpparent++;
                    }
                }
            }
            else
            {
                nodeList = trlGroup.FocusedNode.GetValue(this.appSchema.AppSchema.ASSETClassDetails.ASSET_CLASS_IDColumn.ToString()).ToString();
                nodeGroupList = trlGroup.FocusedNode.GetValue(this.appSchema.AppSchema.ASSETClassDetails.ASSET_CLASS_IDColumn.ToString()).ToString();
            }
            if (!string.IsNullOrEmpty(nodeList))
            {
                LoadGroupList(nodeGroupList.TrimEnd(','));
            }
            lblSelectedGroup.Text=FetchSelectedGroup() + "-Asset Items";
        }
        private string FetchSelectedGroup()
        {
            ResultArgs resultArgs = new ResultArgs();
            string SelectedGroup = string.Empty;
            try
            {
                using (AssetClassSystem groupSystem = new AssetClassSystem())
                {
                    groupSystem.AssetClassId = AssetGroupId;
                    resultArgs = groupSystem.FetchClassDetailsById();
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
        private void trlGroup_DoubleClick(object sender, EventArgs e)
        {
            ShowAssetGroupEdit();
        }
        private void frmGroupView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void ucAssetGroups_DownloadExcel(object sender, EventArgs e)
        {
            using (frmExcelSupport excelSupport = new frmExcelSupport("Group", MasterImport.Group, module))
            {
                excelSupport.UpdateHeld += new EventHandler(OnUpdateHeld);
                excelSupport.ShowDialog();
            }
        }

        private void frmGroupView_Activated(object sender, EventArgs e)
        {
            SetVisibileShortCuts(true, true);
            LoadAssetGroupDetails();
        }
        #endregion

        #region Methods

        private void LoadAssetGroupDetails()
        {
            try
            {
                ResultArgs resultArgs = null;
                //trlGroup.DataSource = null;
                resultArgs = Igroup.FetchGroupDetails();
                if (resultArgs != null && resultArgs.Success)
                {
                    DataTable dtLedgerGroup = resultArgs.DataSource.Table;
                    trlGroup.DataSource = dtLedgerGroup;
                }
                else
                {
                    trlGroup.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally
            { }
        }

        private void DeleteAssetGroupDetails()
        {
            try
            {
                ResultArgs resultArgs = null;
                if (trlGroup.Nodes.Count > 0)
                {
                    if (trlGroup.FocusedNode.Nodes.Count == 0)
                    {
                        if (AssetGroupId > 0)
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                Igroup.AssetClassId = AssetGroupId;
                                resultArgs = Igroup.DeleteClassDetails();
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    RefreshTreeView();
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
                        XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.Group.GROUP_CAN_DELETE), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void ShowAssetGroupEdit()
        {
            if (this.isEditable)
            {
                if (ucAssetGroups.VisibleEditButton == BarItemVisibility.Always)
                {
                    if (trlGroup.Nodes.Count > 0)
                    {
                        if (AssetGroupId > 0)
                        {
                            ShowAssetGroupDetails(AssetGroupId, FormMode.Edit);
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
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_USER_RIGHTS));
            }
        }
        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            RefreshTreeView();
        }

        private void ShowAssetGroupDetails(int groupId, FormMode frmMode)
        {
            try
            {
                frmGroupAdd assetGroupItem = new frmGroupAdd(groupId, module, frmMode);
                assetGroupItem.UpdateHeld += new EventHandler(OnUpdateHeld);
                TempAsseGroupId = assetGroupItem.ClassId;
                assetGroupItem.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            { }
        }

        private void LoadGroupList(string Groupids)
        {
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                gcGroupView.DataSource = null;
                Igroup.GroupIds = Groupids;
                resultArgs = Igroup.FetchSelectedGroupDetails();
                if (resultArgs.Success && resultArgs != null)
                {
                    gcGroupView.DataSource = resultArgs.DataSource.Table;
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
                LoadAssetGroupDetails();
                if (TempAsseGroupId > 0)
                {
                    trlGroup.FocusedColumn = trlcolName;
                    trlGroup.CollapseAll();
                    TreelistIterator treeListIterator = new TreelistIterator(TempAsseGroupId.ToString(), "GROUP_ID");
                    trlGroup.NodesIterator.DoOperation(treeListIterator);
                    trlGroup.FocusedNode = treeListIterator.MyNode;
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
            if (module.Equals(FinanceModule.Asset))
            {
                this.Text = this.GetMessage(MessageCatalog.Asset.AssetClass.ASSETCLASS_VIEW_CAPTION);
            }
            else
            {
                this.Text = this.GetMessage(MessageCatalog.Stock.StockGroup.STOCKGROUP_VIEW_CAPTION);
            }
        }

        //Set Visible to Add/Edit/Delete

        private void Hide()
        {
            ucAssetGroups.VisibleAddButton = ucAssetGroups.VisibleDeleteButton = ucAssetGroups.VisibleEditButton = ucAssetGroups.VisibleMoveTrans = BarItemVisibility.Never;
            ucAssetGroups.VisiblePrintButton = BarItemVisibility.Always;
        }

        #endregion

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
