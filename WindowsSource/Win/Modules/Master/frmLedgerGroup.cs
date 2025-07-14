using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Bosco.Model.UIModel;
using Bosco.Utility;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Bosco.DAO.Schema;
//using DevExpress.CodeRush.StructuralParser;
using System.Collections;
using DevExpress.XtraBars;
using DevExpress.XtraTreeList.Internal;
using System.Reflection;

namespace ACPP.Modules.Master
{
    public partial class frmLedgerGroup : frmFinanceBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        //int LedgerGroupId = 0;
        TreeListNode Selectednode = null;
        AppSchemaSet appSchema = new AppSchemaSet();
        #endregion

        #region Properties

        private int LedgerGroupId
        {
            get
            {
                Int32 ledgerGroupId = 0;
                if (trlLedgerGroup.FocusedNode != null)
                {
                    ledgerGroupId = trlLedgerGroup.FocusedNode.GetValue(trlLedgerGroup.KeyFieldName) != null ? this.UtilityMember.NumberSet.ToInteger(trlLedgerGroup.FocusedNode.GetValue(trlLedgerGroup.KeyFieldName).ToString()) : 0;
                }
                return ledgerGroupId;
            }
            set
            {
                LedgerGroupId = value;
            }
        }

        //private int SubLedgerGroupId
        //{
        //    get
        //    {
        //        return trlChild.FocusedNode.GetValue(trlChild.KeyFieldName) != null ? this.UtilityMember.NumberSet.ToInteger(trlChild.FocusedNode.GetValue(trlChild.KeyFieldName).ToString()) : 0;
        //    }
        //    set
        //    {
        //        LedgerGroupId = value;
        //    }
        //}

        #endregion

        #region Constructor
        public frmLedgerGroup()
        {
            InitializeComponent();
            trlLedgerGroup.NodeCellStyle += new GetCustomNodeCellStyleEventHandler(trlLedgerGroup_NodeCellStyle);
            
        }

        private void trlLedgerGroup_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if ((helper != null) && (!ReferenceEquals(helper.FilterCriteria, null)) && (IsNodeMatchFilter(e.Node)))
            {
                trlLedgerGroup.ExpandAll();
                e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Bold);
            }
        }

        TreeListFilterHelper helper = null;
        private TreeListFilterHelper GetHelper(TreeList inp)
        {
            var propInfo = typeof(TreeList).GetProperty("FilterHelper", BindingFlags.NonPublic | BindingFlags.Instance);
            TreeListFilterHelper res = (TreeListFilterHelper)propInfo.GetValue(inp, new object[] { });
            return res;
        }

        private bool IsNodeMatchFilter(TreeListNode node)
        {
            if (helper == null) helper = GetHelper(trlLedgerGroup);
            if (ReferenceEquals(helper.FilterCriteria, null)) return true;
            return (helper.Fit(node));
        }

        private bool IsNodeOrParentMatchFilter(TreeListNode node)
        {
            if (helper == null) helper = GetHelper(trlLedgerGroup);
            if (ReferenceEquals(helper.FilterCriteria, null)) return true;
            if (helper.Fit(node)) return true;
            foreach (TreeListNode n in node.Nodes)
            {
                if (IsNodeOrParentMatchFilter(n)) return true;
            }
            return false;
        }
           
        private void OnFilterNode(object sender, FilterNodeEventArgs e)
        {
            e.Handled = true;
            e.Node.Visible = IsNodeOrParentMatchFilter(e.Node);
        }
        #endregion

        #region UserControl Events

        /// <summary>
        /// Add new ledger group details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucToolBar1_AddClicked_1(object sender, EventArgs e)
        {
            ShowForm(FormMode.Add);
        }

        /// <summary>
        /// Edit the ledger group details based on the selected id.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucToolBar1_EditClicked_1(object sender, EventArgs e)
        {
            //if (!(trlChild.Focused))
            //{
            if (ValidateGroupEdit())
            {
                ShowForm(FormMode.Edit);
            }
            else
            {
                if (!chkShowFilter.Checked)
                {
                    XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.Group.GROUP_FIXED_EDIT), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            //}
        }

        /// <summary>
        /// Delete the ledger groud details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucToolBar1_DeleteClicked_1(object sender, EventArgs e)
        {
            try
            {
                //if (!(trlChild.Focused))
                //{
                if ((ValidateGroupDelete()))
                {
                    if (trlLedgerGroup.FocusedNode.Nodes.Count == 0)
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            using (LedgerGroupSystem LedgerSystem = new LedgerGroupSystem())
                            {
                                resultArgs = LedgerSystem.DeleteLedgerGroup(LedgerGroupId);
                                if (resultArgs.RowsAffected > 0)
                                {
                                    RefreshTreeView();
                                    // XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.Group.GROUP_DELETE_SUCCESS), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));


                                }
                            }
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.Group.GROUP_CAN_DELETE), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.Group.GROUP_NATURE_DELETE), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //}
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        /// <summary>
        /// Close the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucToolBar1_CloseClicked_1(object sender, EventArgs e)
        {
            this.Close();
        }


        #endregion

        #region Page Events

        /// <summary>
        /// Lead ledger groud details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLedgerGroup_Load(object sender, EventArgs e)
        {
            //Added by Carmel Raj
            SetVisibileShortCuts(false, true);
            FetchLedgerGroup();
            ucToolBar1.VisiblePrintButton = BarItemVisibility.Never;
            // SetRights();
            ApplyUserRights();

            // Set Visible false to Add/Edit/Delete
            this.LockMasters(ucToolBar1);
        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(LegerGroup.CreateLedgerGroup);
            this.enumUserRights.Add(LegerGroup.EditLedgerGroup);
            this.enumUserRights.Add(LegerGroup.DeleteLedgerGroup);
            this.enumUserRights.Add(LegerGroup.PrintLedgerGroup);
            this.enumUserRights.Add(LegerGroup.ViewLedgerGroup);
            this.ApplyUserRights(ucToolBar1, enumUserRights, (int)Menus.LedgerGroup);
        }

        /// <summary>
        /// To Show the group list for Selected Parent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trlLedgerGroup_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            string nodeList = string.Empty;
            string nodeLedgerList = string.Empty;
            if (trlLedgerGroup.FocusedNode.Nodes != null)
            {
                if (trlLedgerGroup.FocusedNode.Nodes.Count > 0)
                {
                    int tmpparent = 0;
                    foreach (TreeListNode child in trlLedgerGroup.FocusedNode.Nodes)
                    {
                        nodeList += child.GetValue(appSchema.AppSchema.LedgerGroup.GROUP_IDColumn.ToString()) + ",";
                        nodeLedgerList += child.GetValue(appSchema.AppSchema.LedgerGroup.GROUP_IDColumn.ToString()) + ",";
                        if (tmpparent == 0)
                        {
                            nodeLedgerList += child.GetValue(appSchema.AppSchema.LedgerGroup.PARENT_GROUP_IDColumn.ToString()) + ",";
                            tmpparent++;
                        }
                    }
                }
                else
                {
                    nodeList = trlLedgerGroup.FocusedNode.GetValue(appSchema.AppSchema.LedgerGroup.GROUP_IDColumn.ToString()).ToString();
                    nodeLedgerList = trlLedgerGroup.FocusedNode.GetValue(appSchema.AppSchema.LedgerGroup.GROUP_IDColumn.ToString()).ToString();
                }
                if (!string.IsNullOrEmpty(nodeList))
                {
                    LoadLedgerGroupList(nodeList.TrimEnd(','));
                    LoadLedgerList(nodeLedgerList.TrimEnd(','));
                }

                //else
                //    trlChild.DataSource = null;
                //lblSelectedGroup.Text = FetchSelectedLedgerGroup() + " - Ledgers";
                lblSelectedGroup.Text = FetchSelectedLedgerGroup() + " - " + this.GetMessage(MessageCatalog.Master.Ledger.LEDGERS_CAPTION);
            }
        }

        /// <summary>
        /// To Edit the Double click Node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trlLedgerGroup_DoubleClick(object sender, EventArgs e)
        {
           TreeList tree = sender as TreeList;
           TreeListHitInfo hi = tree.CalcHitInfo(tree.PointToClient(Control.MousePosition));
           if (hi.HitInfoType == HitInfoType.Cell)
           {
               if (ValidateGroupEdit())
               {
                   if (this.isEditable)
                   {
                       ShowForm(FormMode.Edit);
                   }
                   else
                   {
                       this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_USER_RIGHTS));
                   }
               }
               else
               {
                   XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.Group.GROUP_FIXED_EDIT), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
               }
           }
        }

        /// <summary>
        ///  To Edit the Double click Node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trlChild_DoubleClick(object sender, EventArgs e)
        {
            //if (ValidateGroupEdit())
            //{
            //    ShowForm(FormMode.Edit);
            //}
            //else
            //{
            //    XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.Group.GROUP_FIXED_EDIT), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        /// <summary>
        /// To Show the ledger list for Selected Parent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trlChild_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            //string nodeList = string.Empty;
            //string nodeLedgerList = string.Empty;
            //if (trlChild.FocusedNode.Nodes.Count > 0)
            //{
            //    int tmpparent = 0;
            //    foreach (TreeListNode child in trlChild.FocusedNode.Nodes)
            //    {
            //        nodeList += child.GetValue(appSchema.AppSchema.LedgerGroup.GROUP_IDColumn.ToString()) + ",";
            //        nodeLedgerList += child.GetValue(appSchema.AppSchema.LedgerGroup.GROUP_IDColumn.ToString()) + ",";
            //        if (tmpparent == 0)
            //        {
            //            nodeLedgerList += child.GetValue(appSchema.AppSchema.LedgerGroup.PARENT_GROUP_IDColumn.ToString()) + ",";
            //            tmpparent++;
            //        }
            //    }
            //}
            //else
            //{
            //    nodeList = trlChild.FocusedNode.GetValue(appSchema.AppSchema.LedgerGroup.GROUP_IDColumn.ToString()).ToString();
            //    nodeLedgerList = trlChild.FocusedNode.GetValue(appSchema.AppSchema.LedgerGroup.GROUP_IDColumn.ToString()).ToString();
            //}
            //if (!string.IsNullOrEmpty(nodeList))
            //{
            //    //LoadLedgerGroupList(nodeList.TrimEnd(','));
            //    LoadLedgerList(nodeLedgerList.TrimEnd(','));
            //}
            //else
            //    trlChild.DataSource = null;
            //lblSelectedGroup.Text = FetchSelectedLedgerGroup() + " - Ledgers";
        }

        #endregion

        #region Methods

        /// <summary>
        /// To load the group in the treeview list
        /// </summary>

        private void FetchLedgerGroup()
        {
            try
            {
                using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem())
                {
                    resultArgs = ledgerSystem.GetLedgerGroupSource();
                    if (resultArgs.Success)
                    {
                        DataTable dtLedgerGroup = resultArgs.DataSource.Table;
                        trlLedgerGroup.DataSource = dtLedgerGroup;
                    }

                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        /// <summary>
        /// To show the Group list in the treeview list for the Selectednode
        /// </summary>
        /// <param name="GroupIds"></param>

        private void LoadLedgerGroupList(string GroupIds)
        {
            try
            {
                using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem())
                {
                    //trlChild.DataSource = null;
                    //ledgerSystem.GroupIds = GroupIds;
                    //resultArgs = ledgerSystem.GetLedgerGroupByIdList();
                    //if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    //{
                    //    DataTable dtLedgerGroup = resultArgs.DataSource.Table;
                    //    trlChild.DataSource = dtLedgerGroup;
                    //    gcLedgerList.Focus();
                    //    trlChild.ExpandAll();
                    //}

                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        /// <summary>
        /// To Show the Group Add form 
        /// </summary>
        /// <param name="frmMode"></param>

        private void ShowForm(FormMode frmMode)
        {
            try
            {
                if (this.AppSetting.LockMasters == (int)YesNo.No)
                {
                    if (frmMode == FormMode.Edit && LedgerGroupId == 0)
                    {
                        MessageRender.ShowMessage("Select Ledger Group");
                    }
                    else
                    {
                        frmLedgerGroupAdd frmLedger = new frmLedgerGroupAdd(LedgerGroupId, frmMode);
                        frmLedger.UpdateHeld += new EventHandler(frmLedger_UpdateHeld);
                        frmLedger.ShowDialog();
                    }
                }
                
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        /// <summary>
        /// To refresh the treeview list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        void frmLedger_UpdateHeld(object sender, EventArgs e)
        {
            RefreshTreeView();
        }

        private void RefreshTreeView()
        {
            try
            {
                if (trlLedgerGroup.FocusedNode != null)
                {
                    int ParentId = 0;
                    Selectednode = (TreeListNode)trlLedgerGroup.FocusedNode;
                    FetchLedgerGroup();
                    if (Selectednode.ParentNode == null)
                    {
                        if (Selectednode.GetType() != typeof(TreeListAutoFilterNode))
                        {
                            ParentId = Selectednode.Id;
                            if (trlLedgerGroup.FindNodeByID(ParentId) != null)
                            {
                                trlLedgerGroup.FindNodeByID(ParentId).Selected = true;
                            }
                        }
                        else
                        {
                            if (trlLedgerGroup.FindNodeByID(trlLedgerGroup.Nodes.FirstNode.Id) != null)
                            {
                                trlLedgerGroup.FindNodeByID(trlLedgerGroup.Nodes.FirstNode.Id).Selected = true;
                            }
                        }
                    }
                    else
                    {
                        //ParentId = (Selectednode.ParentNode.Id > trlLedgerGroup.Nodes.Count) ? Selectednode.ParentNode.ParentNode.Id : Selectednode.ParentNode.Id;
                        if (trlLedgerGroup.FindNodeByID(Selectednode.Id) != null)
                        {
                            trlLedgerGroup.FindNodeByID(Selectednode.Id).Selected = true;
                        }
                    }
                    
                    if (trlLedgerGroup.FocusedNode != null)
                    {
                        trlLedgerGroup.FocusedNode.ExpandAll();
                    }
                    else
                    {
                        trlLedgerGroup.ExpandAll();
                    }
                }
                trlLedgerGroup.AddFilter(trlLedgerGroup.ActiveFilterString);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }
            finally { }
        }

        private bool ValidateGroupDelete()
        {
            bool IsGroupVaild = true;
            using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem())
            {
                int AcFlag = ledgerSystem.GetAccessFlag(LedgerGroupId);
                if (AcFlag == (int)AccessFlag.Readonly || AcFlag == (int)AccessFlag.Editable)
                {
                    IsGroupVaild = false;
                }
            }
            return IsGroupVaild;
        }

        private bool ValidateGroupEdit()
        {
            bool IsGroupVaild = true;
            using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem())
            {
                int AcFlag = ledgerSystem.GetAccessFlag(LedgerGroupId);
                if (AcFlag == (int)AccessFlag.Readonly)
                {
                    IsGroupVaild = false;
                }
            }
            return IsGroupVaild;
        }

        /// <summary>
        /// To load Ledger in the Grid
        /// </summary>
        /// <param name="GroupIds"></param>

        private void LoadLedgerList(string GroupIds)
        {
            try
            {
                using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem())
                {
                    gcLedgerList.DataSource = null;
                    ledgerSystem.GroupIds = GroupIds;
                    resultArgs = ledgerSystem.GetLedgerList();
                    if (resultArgs.Success && resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        gcLedgerList.DataSource = resultArgs.DataSource.Table;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        //private void SetRights()
        //{
        //    try
        //    {
        //        //if (ledgerSubType == ledgerSubType.GN)
        //        //{
        //        using (MasterRightsSystem masterRights = new MasterRightsSystem())
        //        {
        //            masterRights.MasterName = this.Text;
        //            if (masterRights.MasterRights() == (int)MasterRights.ReadOnly)
        //            {
        //                ucToolBar1.VisibleAddButton = ucToolBar1.VisibleDeleteButton = ucToolBar1.VisibleEditButton = BarItemVisibility.Never;
        //            }
        //        }
        //        // }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
        //    }
        //    finally { }
        //}

        #endregion

        private void ucToolBar1_RefreshClicked(object sender, EventArgs e)
        {
            RefreshTreeView();
        }

        private void frmLedgerGroup_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvLedger.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvLedger, colLedgerName);
            }
        }

        private void gvLedger_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvLedger.RowCount.ToString();
        }

        private void SetRights()
        {
            try
            {
                using (MasterRightsSystem masterRights = new MasterRightsSystem())
                {
                    masterRights.MasterName = this.Text;
                    if (masterRights.MasterRights() == (int)MasterRights.ReadOnly)
                    {
                        ucToolBar1.VisibleAddButton = ucToolBar1.VisibleDeleteButton = ucToolBar1.VisibleEditButton = BarItemVisibility.Never;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void ucToolBar1_PrintClicked(object sender, EventArgs e)
        {
            //PrintGridViewDetails(gcLedgerList, this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_PRINT_CAPTION), PrintType.DT, gvLedger, true);
            trlLedgerGroup.OptionsPrint.PrintAllNodes = true;
            trlLedgerGroup.OptionsPrint.PrintFilledTreeIndent = true;
            trlLedgerGroup.OptionsPrint.PrintImages = false;
            trlLedgerGroup.OptionsPrint.PrintFilledTreeIndent = true;
            trlLedgerGroup.OptionsPrint.PrintHorzLines = false;
            trlLedgerGroup.OptionsPrint.PrintVertLines = false;
            trlLedgerGroup.OptionsPrint.PrintTreeButtons = true;
            trlLedgerGroup.OptionsPrint.PrintPageHeader = true;
            trlLedgerGroup.OptionsPrint.PrintReportFooter = false;
            trlLedgerGroup.ShowPrintPreview();
        }

        private void frmLedgerGroup_EnterClicked(object sender, EventArgs e)
        {

            if (trlLedgerGroup.FocusedNode.GetType() != typeof(TreeListAutoFilterNode))
            {
                if (ValidateGroupEdit())
                {
                    ShowForm(FormMode.Edit);
                }
                else
                {
                    XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.Group.GROUP_FIXED_EDIT), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                TreeListNode node = trlLedgerGroup.Nodes.AutoFilterNode;
                trlLedgerGroup.FocusedNode = node;
                trlLedgerGroup.ShowEditor();
            }
        }
        public string FetchSelectedLedgerGroup()
        {
            string SelectedGroup = string.Empty;
            try
            {
                using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem())
                {
                    ledgerSystem.GroupIds = LedgerGroupId.ToString();
                    resultArgs = ledgerSystem.GetLedgerGroupByIdList();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        SelectedGroup = resultArgs.DataSource.Table.Rows[0]["Ledger Sub Group"].ToString();
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

        private void chkLedgerGroupFilter_CheckedChanged(object sender, EventArgs e)
        {
            trlLedgerGroup.OptionsView.ShowAutoFilterRow = chkLedgerGroupFilter.Checked;
            if (chkLedgerGroupFilter.Checked)
            {
                trlLedgerGroup.ExpandAll();
                TreeListNode node = trlLedgerGroup.Nodes.AutoFilterNode;
                trlLedgerGroup.FocusedNode = node;
                trlLedgerGroup.ShowEditor();
            }
        }
    }
}
