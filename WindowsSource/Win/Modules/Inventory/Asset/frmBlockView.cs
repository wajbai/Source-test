using System;
using System.Windows.Forms;

using Bosco.Model.UIModel;
using Bosco.Model;
using Bosco.Utility;

namespace ACPP.Modules.Inventory
{
    public partial class frmBlockView : frmFinanceBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        FinanceModule FrmModule;
        private int EditBlockId = 0;
        #endregion

        #region Constructor
        public frmBlockView()
        {
            InitializeComponent();
        }

        public frmBlockView(FinanceModule module)
            : this()
        {
            FrmModule = module;
        }
        #endregion

        #region Property
        private int blockid = 0;
        public int BlockId
        {
            get
            {
                RowIndex = gvBlock.FocusedRowHandle;
                blockid = gvBlock.GetFocusedRowCellValue(colBlockId) != null ? this.UtilityMember.NumberSet.ToInteger(gvBlock.GetFocusedRowCellValue(colBlockId).ToString()) : 0;
                return blockid;
            }
            set
            {
                blockid = value;
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// To load the Block
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmBlockView_Load(object sender, EventArgs e)
        {
            GetBlockDetails();
            SetTitle();
            if (FrmModule.ToString().Equals("Asset"))
                ApplyUserRights();
            else
                ApplyStockUserRights();
        }

        /// <summary>
        /// load the block details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmBlockView_Activated(object sender, EventArgs e)
        {
            SetVisibileShortCuts(false, true);
            GetBlockDetails();
        }

        /// <summary>
        /// To Show the Add form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarBlock_AddClicked(object sender, EventArgs e)
        {
            ShowForm((int)AddNewRow.NewRow);
        }

        /// <summary>
        /// To Edit the block
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarBlock_EditClicked(object sender, EventArgs e)
        {
            EditBlock();
        }

        /// <summary>
        /// To Edit the block while press the Enter Key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmBlockView_EnterClicked(object sender, EventArgs e)
        {
            EditBlock();
        }
        /// <summary>
        /// To Edit the block
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvBlock_DoubleClick(object sender, EventArgs e)
        {
            EditBlock();
        }

        /// <summary>
        /// To Delete block System
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarBlock_DeleteClicked(object sender, EventArgs e)
        {
            try
            {

                if (gvBlock.RowCount != 0)
                {
                    if (BlockId != 0)
                    {
                        using (BlockSystem blocksystem = new BlockSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                resultArgs = blocksystem.DeleteBlockDetails(BlockId);
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    GetBlockDetails();
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
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// To Print the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarBlock_PrintClicked(object sender, EventArgs e)
        {
            //PrintGridViewDetails(gcBlock, "Block Details", PrintType.DT, gvBlock, true);
            PrintGridViewDetails(gcBlock, this.GetMessage(MessageCatalog.Asset.Block.ASSETBLOCK_PRINT_CAPTION), PrintType.DT, gvBlock, true);
        }

        /// <summary>
        /// To View the Number of Record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvProjectCategory_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvBlock.RowCount.ToString();
        }

        /// <summary>
        /// To Enable the AutoFilter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvBlock.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvBlock, colBlock);
            }
        }

        /// <summary>
        /// To close the View Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarBlock_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// To refresh the Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarBlock_RefreshClicked(object sender, EventArgs e)
        {
            GetBlockDetails();
        }
        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        private void ApplyUserRights()
        {
            this.enumUserRights.Add(Block.CreateBlock);
            this.enumUserRights.Add(Block.EditBlock);
            this.enumUserRights.Add(Block.DeleteBlock);
            this.enumUserRights.Add(Block.ViewBlock);
            this.ApplyUserRights(ucToolBarBlock, enumUserRights, (int)Menus.Block);
        }

        /// <summary>
        /// 
        /// </summary>
        private void ApplyStockUserRights()
        {
            this.enumUserRights.Add(StockBlockArea.CreateStockBlockArea);
            this.enumUserRights.Add(StockBlockArea.EditStockBlockArea);
            this.enumUserRights.Add(StockBlockArea.DeleteStockBlockArea);
            this.enumUserRights.Add(StockBlockArea.ViewStockBlockArea);
            this.ApplyUserRights(ucToolBarBlock, enumUserRights, (int)Menus.StockBlockArea);
        }

        /// <summary>
        /// To Get the BlockDetails
        /// </summary>
        public void GetBlockDetails()
        {
            try
            {
                using (BlockSystem blocksystem = new BlockSystem())
                {
                    resultArgs = blocksystem.FetchBlockDetails();
                    gcBlock.DataSource = resultArgs.DataSource.Table;
                    gcBlock.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// To Show form based on the Selection
        /// </summary>
        /// <param name="projectCategoryId"></param>
        public void ShowForm(int blockId)
        {
            try
            {
                frmBlockAdd frmBlock = new frmBlockAdd(blockId);
                frmBlock.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmBlock.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        /// <summary>
        /// To refresh the Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            GetBlockDetails();
            gvBlock.FocusedRowHandle = RowIndex;
            ApplyUserRights();
        }

        /// <summary>
        /// To set the Id of Block
        /// </summary>
        public void EditBlock()
        {
            try
            {
                if (this.isEditable)
                {
                    if (gvBlock.RowCount != 0)
                    {
                        if (BlockId != 0)
                        {
                            ShowForm(BlockId);
                        }
                        else
                        {
                            if (!chkShowFilter.Checked)
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NOSELECTION_FOR_EDIT));
                            }
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_USER_RIGHTS));
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void SetTitle()
        {
            //this.Text = "Block";
            this.Text = this.GetMessage(MessageCatalog.Asset.Block.ASSETBLOCK_VIEW_CAPTION);
        }
        #endregion

    }
}