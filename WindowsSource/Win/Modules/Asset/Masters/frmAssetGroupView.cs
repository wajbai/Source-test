using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Utility;
using Bosco.Model;
using Bosco.Model.Asset;
using DevExpress.XtraTreeList.Nodes;
using Bosco.DAO.Schema;

namespace ACPP.Modules.Asset.Masters
{
    public partial class frmAssetGroupView : frmBase
    {

        #region Varialbe Decelaration
        int RowIndex = 0;
        int AssetGroupId = 0;
        TreeListNode Selectednode = null;
        AppSchemaSet appSchema = new AppSchemaSet();
        public FinanceModule module { get; set; }
        AssetStockProduct.IGroup Igroup;

        #endregion

        #region Event Decelaration
        public event EventHandler UpdateHeld;

        #endregion

        #region Properties
        public int assetGroupId
        {
            get
            {
                return this.UtilityMember.NumberSet.ToInteger(trlGroup.FocusedNode.GetValue(trlGroup.KeyFieldName).ToString());

            }
            set
            {
                AssetGroupId = value;
            }
        }
        #endregion

        #region Costurctor
        public frmAssetGroupView(FinanceModule mod)
        {
            module = mod;
            Igroup = AssetStockFactory.GetGroupInstance(mod);
            InitializeComponent();
        }
        #endregion

        #region Events

        private void ucAssetGroups_AddClicked(object sender, EventArgs e)
        {
            ShowAssetGroupDetails((int)AddNewRow.NewRow);
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

        private void frmAssetGroupView_Load(object sender, EventArgs e)
        {
            LoadAssetGroupDetails();
            SetTitle();
        }

        private void ucAssetGroups_PrintClicked(object sender, EventArgs e)
        {
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
                this.SetFocusRowFilter(gvGroupView, colNAME);
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
                    nodeList += child.GetValue(this.appSchema.AppSchema.ASSETGroupDetails.GROUP_IDColumn.ToString()) + ",";
                    nodeGroupList += child.GetValue(this.appSchema.AppSchema.ASSETGroupDetails.GROUP_IDColumn.ToString()) + ",";
                    if (tmpparent == 0)
                    {
                        nodeGroupList += child.GetValue(this.appSchema.AppSchema.ASSETGroupDetails.PARENT_GROUP_IDColumn.ToString()) + ",";
                        tmpparent++;
                    }
                }
            }
            else
            {
                nodeList = trlGroup.FocusedNode.GetValue(this.appSchema.AppSchema.ASSETGroupDetails.GROUP_IDColumn.ToString()).ToString();
                nodeGroupList = trlGroup.FocusedNode.GetValue(this.appSchema.AppSchema.ASSETGroupDetails.GROUP_IDColumn.ToString()).ToString();
            }
            if (!string.IsNullOrEmpty(nodeList))
            {
                LoadGroupList(nodeGroupList.TrimEnd(','));
            }
        }
        #endregion

        #region Methods

        private void LoadAssetGroupDetails()
        {
            try
            {
                ResultArgs resultArgs = null;
                trlGroup.DataSource = null;
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
                    if (assetGroupId > 0)
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            Igroup.GroupId = assetGroupId;
                            resultArgs = Igroup.DeleteGroupDetails();
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
            if (trlGroup.Nodes.Count != 0)
            {
                if (assetGroupId != 0)
                {
                    ShowAssetGroupDetails(assetGroupId);
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

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            RefreshTreeView();
        }

        private void ShowAssetGroupDetails(int AssetGroupId)
        {
            try
            {
                frmAssetGroupAdd assetGroupItem = new frmAssetGroupAdd(AssetGroupId, module);
                assetGroupItem.UpdateHeld += new EventHandler(OnUpdateHeld);
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
                int ParentId = 0;
                Selectednode = (TreeListNode)trlGroup.FocusedNode;
                LoadAssetGroupDetails();
                if (Selectednode.ParentNode == null)
                {
                    ParentId = Selectednode.Id;
                }
                else
                {
                    ParentId = (Selectednode.ParentNode.Id > trlGroup.Nodes.Count) ? Selectednode.ParentNode.ParentNode.Id : Selectednode.ParentNode.Id;
                }
                trlGroup.FocusedNode = trlGroup.Nodes[ParentId];
                trlGroup.FocusedNode.ExpandAll();
            }
            catch (Exception ex)
            {

            }
            finally { }
        }

        public void SetTitle()
        {
            if (module.Equals(FinanceModule.Asset))
            {
                this.Text = this.GetMessage(MessageCatalog.Asset.AssetGroup.ASSETGROUP_VIEW_CAPTION);
            }
            else
            {
                this.Text = this.GetMessage(MessageCatalog.Stock.StockGroup.STOCKGROUP_VIEW_CAPTION);
            }
        }

        #endregion
    }
}
