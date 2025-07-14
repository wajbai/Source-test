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
using DevExpress.XtraTreeList.Nodes;
using Bosco.DAO.Schema;
using Bosco.Model.Asset;
using Bosco.Model;

namespace ACPP.Modules.Asset.Masters
{
    public partial class frmAssetCategoryView : frmBase
    {
        #region VariableDeclaration
        TreeListNode Selectednode = null;
        AppSchemaSet appSchema = new AppSchemaSet();
        ResultArgs resultArgs = null;
        private FinanceModule financeModule { get; set; }
        private Bosco.Model.AssetStockProduct.ICategory Category = null;
        #endregion

        #region Constructor
        public frmAssetCategoryView()
        {
            InitializeComponent();
        }

        public frmAssetCategoryView(FinanceModule module)
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
                return this.UtilityMember.NumberSet.ToInteger(trlCategories.FocusedNode.GetValue(trlCategories.KeyFieldName).ToString());
            }
            set
            {
                CategoryId = value;
            }
        }
        #endregion

        #region Events
        private void ucAssetCategories_AddClicked(object sender, EventArgs e)
        {
            showAssetCategoryForm((int)AddNewRow.NewRow);
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
            PrintGridViewDetails(gcAssetItems, this.GetMessage(MessageCatalog.Asset.Location.LOCATION_PRINT_CAPTION), PrintType.DT, gvAssetItems, true);
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
                Category.GroupIds = GroupIds;
                resultArgs = Category.FetchSelectedCategoryDetails();
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    DataTable dtCategory = resultArgs.DataSource.Table;
                    gcAssetItems.DataSource = dtCategory;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }
            finally { }
        }
        #endregion

        #region Methods

        public void DeleteCategoryDetails()
        {
            try
            {
                if (trlCategories.Nodes.Count > 0)
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
                        showAssetCategoryForm(CategoryId);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CategoryId"></param>
        public void showAssetCategoryForm(int CategoryId)
        {
            try
            {
                frmAssetCategoriesAdd frmCategory = new frmAssetCategoriesAdd(CategoryId, financeModule);
                frmCategory.updateHeld += new EventHandler(OnUpdateHeld);
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
                int ParentId = 0;
                Selectednode = (TreeListNode)trlCategories.FocusedNode;
                LoadAssetCategories();
                if (Selectednode.ParentNode == null)
                {
                    ParentId = Selectednode.Id;
                }
                else
                {
                    ParentId = (Selectednode.ParentNode.Id > trlCategories.Nodes.Count) ? Selectednode.ParentNode.ParentNode.Id : Selectednode.ParentNode.Id;
                }
                trlCategories.FocusedNode = trlCategories.Nodes[ParentId];
                trlCategories.FocusedNode.ExpandAll();
            }
            catch (Exception ex)
            {
            }
            finally { }
        }

        public void SetTitle()
        {
            if (Category.Equals(FinanceModule.Asset))
            {
                this.Text = this.GetMessage(MessageCatalog.Asset.AssetCategory.ASSETCATEGORY_VIEW_CAPTION);
            }
            else
            {
                this.Text = this.GetMessage(MessageCatalog.Stock.StockCategory.STOCKCATEGORY_VIEW_CAPTION);
            }
        }
        #endregion
    }
}
