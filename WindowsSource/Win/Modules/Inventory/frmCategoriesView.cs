using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;

using Bosco.Utility;
using Bosco.DAO.Schema;
using Bosco.Model;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;

namespace ACPP.Modules.Inventory
{
    public partial class frmCategoriesView : frmFinanceBase
    {
        #region VariableDeclaration
        TreeListNode Selectednode = null;
        AppSchemaSet appSchema = new AppSchemaSet();
        ResultArgs resultArgs = null;
        FormMode FrmMode;
        private FinanceModule financeModule { get; set; }
        private AssetStockProduct.ICategory Category = null;
        public event EventHandler UpdateHeld;
        private int TempCategoryId { get; set; }
        #endregion

        #region Constructor
        public frmCategoriesView()
        {
            InitializeComponent();
        }

        public frmCategoriesView(FinanceModule module)
            : this()
        {
            financeModule = module;
            Category = AssetStockFactory.GetCategoryInstance(financeModule);
        }
        #endregion

        #region Properties

        private int CategoryId
        {
            get
            {
                return (trlCategories.FocusedNode != null) ? this.UtilityMember.NumberSet.ToInteger(trlCategories.FocusedNode.GetValue(trlCategories.KeyFieldName).ToString()) : 0;
            }
        }
        #endregion

        #region Events
        private void ucAssetCategories_AddClicked(object sender, EventArgs e)
        {
            showAssetCategoryForm(CategoryId, FormMode.Add);
        }

        private void ucAssetCategories_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucAssetCategories_DeleteClicked(object sender, EventArgs e)
        {
            DeleteCategoryDetails();
        }

        private void ucAssetCategories_EditClicked(object sender, EventArgs e)
        {
            UpdateCategoryDetails();
        }

        private void ucAssetCategories_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcAssetItems, this.GetMessage(MessageCatalog.Asset.AssetCategory.ASSETCATEGORY_PRINT_CAPTION), PrintType.DT, gvAssetItems, true);
        }

        private void ucAssetCategories_RefreshClicked(object sender, EventArgs e)
        {
            LoadAssetCategories();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvAssetItems.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvAssetItems, colName);
            }
        }

        private void gvAssetItems_RowCountChanged(object sender, EventArgs e)
        {
            lblCountRecord.Text = gvAssetItems.RowCount.ToString();
        }

        private void frmAssetCategoriesView_Load(object sender, EventArgs e)
        {
            LoadAssetCategories();
            SetTitle();
        }

        private void trlCategories_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            string nodeList = string.Empty;
            string nodeCategoryList = string.Empty;
            if (trlCategories.FocusedNode.Nodes.Count > 0)
            {
                int tmpparent = 0;
                foreach (TreeListNode child in trlCategories.FocusedNode.Nodes)
                {
                    nodeList += child.GetValue(appSchema.AppSchema.ASSETCategory.CATEGORY_IDColumn.ToString()) + ",";
                    nodeCategoryList += child.GetValue(appSchema.AppSchema.ASSETCategory.CATEGORY_IDColumn.ToString()) + ",";
                    if (tmpparent == 0)
                    {
                        nodeCategoryList += child.GetValue(appSchema.AppSchema.ASSETCategory.PARENT_CATEGORY_IDColumn.ToString()) + ",";
                        tmpparent++;
                    }
                }
            }
            else
            {
                nodeList = trlCategories.FocusedNode.GetValue(appSchema.AppSchema.ASSETCategory.CATEGORY_IDColumn.ToString()).ToString();
                nodeCategoryList = trlCategories.FocusedNode.GetValue(appSchema.AppSchema.ASSETCategory.CATEGORY_IDColumn.ToString()).ToString();
            }
            if (!string.IsNullOrEmpty(nodeList))
            {
                LoadCategoryList(nodeCategoryList.TrimEnd(','));
            }
        }

        private void LoadCategoryList(string GroupIds)
        {
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                gcAssetItems.DataSource = null;
                Category.CategoryId = CategoryId;
                resultArgs = Category.FetchSelectedCategoryDetails();
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    gcAssetItems.DataSource = resultArgs.DataSource.Table;
                }

                //DataSet dsVoucher = new DataSet();
                //dsVoucher = Category.FetchAssetItemMasterDetail();
                //if (dsVoucher.Tables.Count != 0)
                //{
                //    gcAssetItems.DataSource = dsVoucher;
                //    gcAssetItems.DataMember = "Master";
                //    gcAssetItems.RefreshDataSource();
                //}
                //else
                //{
                //    gcAssetItems.DataSource = null;
                //    gcAssetItems.RefreshDataSource();
                //}
                //gvAssetItems.FocusedRowHandle = 0;
                //gvAssetItems.FocusRectStyle = DrawFocusRectStyle.RowFocus;
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }
            finally { }
        }

        private void frmAssetCategoryView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        #endregion

        #region Methods

        public void DeleteCategoryDetails()
        {
            try
            {
                if (trlCategories.Nodes.Count > 0)
                {
                    if (trlCategories.FocusedNode.Nodes.Count == 0)
                    {
                        if (this.CategoryId > 0)
                        {

                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                Category.CategoryId = CategoryId;
                                resultArgs = Category.DeleteCategoryDetails();
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    LoadAssetCategories();
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
                        XtraMessageBox.Show(this.GetMessage(MessageCatalog.Asset.AssetCategory.CATEGORY_CAN_DELETE), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
            finally
            { }
        }

        private void frmAssetCategoryView_EnterClicked(object sender, EventArgs e)
        {
            UpdateCategoryDetails();
        }

        private void trlCategories_DoubleClick(object sender, EventArgs e)
        {
            UpdateCategoryDetails();
        }


        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            RefreshTreeView();
        }

        public void UpdateCategoryDetails()
        {
            try
            {
                if (trlCategories.Nodes.Count > 0)
                {
                    if (CategoryId > 0)
                    {
                        showAssetCategoryForm(CategoryId, FormMode.Edit);
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
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
            finally
            { }
        }

        public void showAssetCategoryForm(int categoryId, FormMode FrmMode)
        {
            try
            {
                frmCategoriesAdd frmCategory = new frmCategoriesAdd(CategoryId, financeModule, FrmMode);
                frmCategory.UpdateHeld += new EventHandler(OnUpdateHeld);
                TempCategoryId = frmCategory.categoryID;
                frmCategory.ShowDialog();
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
            finally
            { }
        }

        private void LoadAssetCategories()
        {
            try
            {
                resultArgs = Category.FetchCategoryDetails();
                if (resultArgs != null && resultArgs.Success)
                {
                    DataTable dtSource = resultArgs.DataSource.Table;
                    trlCategories.DataSource = dtSource;
                    gcAssetItems.RefreshDataSource();
                }

            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void RefreshTreeView()
        {
            try
            {
                //int ParentId = 0;
                //Selectednode = (TreeListNode)trlCategories.FocusedNode;
                //LoadAssetCategories();
                //if (Selectednode.ParentNode == null)
                //{
                //    ParentId = Selectednode.Id;
                //}
                //else
                //{
                //    ParentId = (Selectednode.ParentNode.Id > trlCategories.Nodes.Count) ? Selectednode.ParentNode.ParentNode.Id : Selectednode.ParentNode.Id;
                //}
                //trlCategories.FocusedNode = trlCategories.Nodes[ParentId];
                //trlCategories.FocusedNode.ExpandAll();
                LoadAssetCategories();
                if (TempCategoryId > 0)
                {
                    trlCategories.FocusedColumn = colCategoryName;
                    trlCategories.CollapseAll();
                    TreelistIterator treeListIterator = new TreelistIterator(TempCategoryId.ToString(), "CATEGORY_ID");
                    trlCategories.NodesIterator.DoOperation(treeListIterator);
                    trlCategories.FocusedNode = treeListIterator.MyNode;
                    if (trlCategories.FocusedNode != null)
                        trlCategories.FocusedNode.Expanded = true;
                }
            }
            catch (Exception ex)
            {
            }
            finally { }
        }

        public void SetTitle()
        {
            if (financeModule.Equals(FinanceModule.Asset))
            {
                this.Text = this.GetMessage(MessageCatalog.Asset.AssetCategory.ASSETCATEGORY_VIEW_CAPTION);
            }
            else
            {
                this.Text = this.GetMessage(MessageCatalog.Stock.StockCategory.STOCKCATEGORY_VIEW_CAPTION);
            }
        }

        #endregion

        private void frmCategoriesView_Activated(object sender, EventArgs e)
        {
            LoadAssetCategories();
            SetTitle();
        }



    }
}
