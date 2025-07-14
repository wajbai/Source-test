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
using Bosco.Model.UIModel;



namespace ACPP.Modules.Inventory
{
    public partial class frmAssetClassView : frmFinanceBase
    {
        #region Varialbe Decelaration
        int RowIndex = 0;
        TreeListNode Selectednode = null;
        AppSchemaSet appSchema = new AppSchemaSet();
        public FinanceModule module { get; set; }
        // AssetStockProduct.IGroup Igroup;

        #endregion

        #region Event Decelaration
        public event EventHandler UpdateHeld;
        private int TempAsseClassId { get; set; }
        #endregion

        #region Properties
        public int AssetClassId
        {
            get
            {
                return (trlAssetClass.FocusedNode != null) ? this.UtilityMember.NumberSet.ToInteger(trlAssetClass.FocusedNode.GetValue(trlAssetClass.KeyFieldName).ToString()) : 0;
            }
        }
        #endregion

        #region Costurctor
        public frmAssetClassView(FinanceModule mod)
        {
            module = mod;
            // Igroup = AssetStockFactory.GetGroupInstance(mod);
            InitializeComponent();
        }
        #endregion

        #region Events

        private void ucAssetGroups_AddClicked(object sender, EventArgs e)
        {
            ShowAssetClassDetails(AssetClassId, FormMode.Add);
        }

        private void ucAssetGroups_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucAssetGroups_EditClicked(object sender, EventArgs e)
        {
            if (ValidateEditAccessFlag())
            {
                ShowAssetClassEdit();
            }
            else
            {
                XtraMessageBox.Show("Fixed Asset Class can't be Edited", this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ucAssetGroups_DeleteClicked(object sender, EventArgs e)
        {
            DeleteAssetClassDetails();
        }

        private void frmGroupView_Load(object sender, EventArgs e)
        {
            //SetVisibileShortCuts(false, true);
            LoadAssetClassDetails();
            SetTitle();
            //ApplyUserRights();

            //Set Visible to Add/Edit/Delete
            //LockMasters(ucAssetClass);

            ShowHide();
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
                    PrintGridViewDetails(gcAssetClassView, this.GetMessage(MessageCatalog.Asset.AssetClass.ASSETCLASS_PRINT_CAPTION), PrintType.DT, gvAssetClassView, true);
                }
            }
        }

        private void ucAssetGroups_RefreshClicked(object sender, EventArgs e)
        {
            RefreshTreeView();
        }

        private void gcAssetGroup_DoubleClick(object sender, EventArgs e)
        {
            ShowAssetClassEdit();
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
                this.SetFocusRowFilter(gvAssetClassView, colAssetClass);
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
                using (AssetClassSystem assetClassSystem = new AssetClassSystem())
                {
                    assetClassSystem.AssetClassId = AssetClassId;
                    assetClassSystem.ClassId = AssetClassId.ToString();
                    resultArgs = assetClassSystem.FetchClassDetailsById();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        SelectedGroup = resultArgs.DataSource.Table.Rows[0]["ASSET_CLASS"].ToString();
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
            if (ValidateEditAccessFlag())
            {
                ShowAssetClassEdit();
            }
            else
            {
                XtraMessageBox.Show("Fixed Asset Class can't be Edited", this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

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

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void trlAssetClass_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            string nodeList = string.Empty;
            string nodeAssetClassList = string.Empty;
            if (trlAssetClass.FocusedNode.Nodes.Count > 0)
            {
                int tmpparent = 0;
                foreach (TreeListNode child in trlAssetClass.FocusedNode.Nodes)
                {
                    nodeList += child.GetValue(this.appSchema.AppSchema.ASSETClassDetails.ASSET_CLASS_IDColumn.ToString()) + ",";
                    nodeAssetClassList += child.GetValue(this.appSchema.AppSchema.ASSETClassDetails.ASSET_CLASS_IDColumn.ToString()) + ",";
                    if (tmpparent == 0)
                    {
                        nodeAssetClassList += child.GetValue(this.appSchema.AppSchema.ASSETClassDetails.PARENT_CLASS_IDColumn.ToString()) + ",";
                        tmpparent++;
                    }
                }
            }
            else
            {
                nodeList = trlAssetClass.FocusedNode.GetValue(this.appSchema.AppSchema.ASSETClassDetails.ASSET_CLASS_IDColumn.ToString()).ToString();
                nodeAssetClassList = trlAssetClass.FocusedNode.GetValue(this.appSchema.AppSchema.ASSETClassDetails.ASSET_CLASS_IDColumn.ToString()).ToString();
            }
            if (!string.IsNullOrEmpty(nodeList))
            {
                LoadAssetClassList(nodeAssetClassList.TrimEnd(','));
            }
            lblSelectedGroup.Text = FetchSelectedClass().Contains("&") ? FetchSelectedClass().Replace("&", "&&") + " - Asset Item" : FetchSelectedClass() + " - Asset Item";
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
            ShowAssetClassEdit();
        }

        private void frmGroupView_Activated(object sender, EventArgs e)
        {
            SetVisibileShortCuts(false, true);
            LoadAssetClassDetails();
        }
        #endregion

        #region Methods

        private void LoadAssetClassDetails()
        {
            try
            {
                ResultArgs resultArgs = null;
                //trlGroup.DataSource = null;
                using (AssetClassSystem assetClassSystem = new AssetClassSystem())
                {
                    resultArgs = assetClassSystem.FetchClassDetails();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        DataTable dtLedgerAssetClass = resultArgs.DataSource.Table;
                        trlAssetClass.DataSource = dtLedgerAssetClass;
                    }
                    else
                    {
                        trlAssetClass.DataSource = null;
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

        private void DeleteAssetClassDetails()
        {
            try
            {
                ResultArgs resultArgs = null;

                if (ValidateAccessFlag())
                {
                    if (trlAssetClass.Nodes.Count > 0)
                    {
                        if (trlAssetClass.FocusedNode.Nodes.Count == 0)
                        {
                            if (AssetClassId > 0)
                            {
                                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                                {
                                    using (AssetClassSystem assetClassSystem = new AssetClassSystem())
                                    {
                                        assetClassSystem.AssetClassId = AssetClassId;
                                        assetClassSystem.ClassId = AssetClassId.ToString(); ;
                                        resultArgs = assetClassSystem.DeleteClassDetails();

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
                            XtraMessageBox.Show(this.GetMessage(MessageCatalog.Asset.AssetClass.ASSETCLASS_CANNOT_DELETE), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                    }
                }
                else
                {
                    XtraMessageBox.Show("Fixed Asset Class can't be deleted", this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally
            { }
        }

        private void ShowAssetClassEdit()
        {
            if (this.AppSetting.LockMasters == (int)YesNo.No)
            {
                if (ucAssetClass.VisibleEditButton == BarItemVisibility.Always)
                {
                    if (trlAssetClass.Nodes.Count > 0)
                    {
                        if (AssetClassId > 0)
                        {
                            ShowAssetClassDetails(AssetClassId, FormMode.Edit);
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

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(AssetClass.CreateClass);
            this.enumUserRights.Add(AssetClass.EditClass);
            this.enumUserRights.Add(AssetClass.DeleteClass);
            this.enumUserRights.Add(AssetClass.PrintClass);
            this.enumUserRights.Add(AssetClass.ViewAssetClass);
            this.ApplyUserRights(ucAssetClass, enumUserRights, (int)Menus.AssetClass);
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            RefreshTreeView();
        }

        private void ShowAssetClassDetails(int ClassId, FormMode FrmMode)
        {
            try
            {
                frmAssetClassAdd assetClassItem = new frmAssetClassAdd(ClassId, FrmMode);
                assetClassItem.UpdateHeld += new EventHandler(OnUpdateHeld);
                TempAsseClassId = assetClassItem.ClassId;
                assetClassItem.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            { }
        }

        private void LoadAssetClassList(string ClassId)
        {
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                using (AssetClassSystem assetClassSystem = new AssetClassSystem())
                {
                    gcAssetClassView.DataSource = null;
                    assetClassSystem.ClassId = ClassId;
                    resultArgs = assetClassSystem.FetchSelectedClassDetails();
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
                LoadAssetClassDetails();
                if (TempAsseClassId > 0)
                {
                    trlAssetClass.FocusedColumn = trlcolName;
                    trlAssetClass.CollapseAll();
                    TreelistIterator treeListIterator = new TreelistIterator(TempAsseClassId.ToString(), "ASSET_CLASS_ID");
                    trlAssetClass.NodesIterator.DoOperation(treeListIterator);
                    trlAssetClass.FocusedNode = treeListIterator.MyNode;
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

        private void ShowHide()
        {
            ucAssetClass.VisibleMoveTrans = BarItemVisibility.Never;
            ucAssetClass.VisiblePrintButton = ucAssetClass.VisibleAddButton = ucAssetClass.VisibleDeleteButton = ucAssetClass.VisibleEditButton = ucAssetClass.VisibleRefresh = BarItemVisibility.Always;

            //ucAssetClass.VisibleAddButton = ucAssetClass.VisibleDeleteButton = ucAssetClass.VisibleEditButton = ucAssetClass.VisibleMoveTrans = BarItemVisibility.Never;
            //ucAssetClass.VisiblePrintButton = BarItemVisibility.Always;
        }

        private bool ValidateAccessFlag()
        {
            bool isValid = true;
            using (AssetClassSystem assetclasssystem = new AssetClassSystem())
            {
                assetclasssystem.AssetClassId = AssetClassId;
                int AccessFlag = assetclasssystem.FetchAccessClassId();
                if (AccessFlag == (int)Bosco.Utility.AccessFlag.Readonly || AccessFlag == (int)Bosco.Utility.AccessFlag.Editable)
                {
                    isValid = false;
                }
            }
            return isValid;
        }

        private bool ValidateEditAccessFlag()
        {
            bool isValid = true;
            using (AssetClassSystem assetclasssystem = new AssetClassSystem())
            {
                assetclasssystem.AssetClassId = AssetClassId;
                int AccessFlag = assetclasssystem.FetchAccessClassId();
                if (AccessFlag == (int)Bosco.Utility.AccessFlag.Readonly)
                {
                    isValid = false;
                }
            }
            return isValid;
        }

        #endregion

    }
}
