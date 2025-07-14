using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Utility;
using Bosco.Model;
using ACPP.Modules.Data_Utility;

namespace ACPP.Modules.Inventory.Stock
{
    public partial class frmStockItemView : frmFinanceBase
    {
        #region VariableDeclearation
        ResultArgs resultArgs = null;
        private int StockItem_Id = 0;
        private int RowIndex = 0;
        public FinanceModule module { get; set; }
        #endregion

        #region Properties
        public int StockItemId
        {
            get
            {
                RowIndex = gvStockView.FocusedRowHandle;
                StockItem_Id = gvStockView.GetFocusedRowCellValue(colItemId) != null ? this.UtilityMember.NumberSet.ToInteger(gvStockView.GetFocusedRowCellValue(colItemId).ToString()) : 0;
                return StockItem_Id;
            }
            set
            {
                StockItem_Id = value;
            }
        }
        #endregion

        #region Constructor
        public frmStockItemView()
        {
            InitializeComponent();
        }

        public frmStockItemView(FinanceModule module)
            : this()
        {
            this.module = module;
        }

        #endregion

        #region Events
        /// <summary>
        /// To load the Stock Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmStockItemView_Load(object sender, EventArgs e)
        {
            SetVisibileShortCuts(true, true);
            LoadStockItemDetails();
            ApplyUserRights();
        }

        /// <summary>
        /// To add the Stock Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucStockView_AddClicked(object sender, EventArgs e)
        {
            ShowStock((int)AddNewRow.NewRow);
        }

        /// <summary>
        /// To Edit the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucStockView_EditClicked(object sender, EventArgs e)
        {
            ShowStockItemEdit();
        }

        /// <summary>
        /// Filter the Records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvStockView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvStockView, colName);
            }
        }

        /// <summary>
        /// Print the Item Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucStockView_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcStockView, this.GetMessage(MessageCatalog.Stock.StockItem.STOCK_PRINT_CAPTION), PrintType.DT, gvStockView, true);
        }

        /// <summary>
        /// Refresh the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucStockView_RefreshClicked(object sender, EventArgs e)
        {
            LoadStockItemDetails();
        }

        /// <summary>
        /// To Edit the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvStockView_DoubleClick(object sender, EventArgs e)
        {
            ShowStockItemEdit();
        }

        /// <summary>
        /// View the Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmStockItemView_EnterClicked(object sender, EventArgs e)
        {
            ShowStockItemEdit();
        }

        /// <summary>
        /// To close the Stock Item form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucStockView_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Count the Stock item Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvStockView_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvStockView.RowCount.ToString();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Load the Stock Details
        /// </summary>
        public void LoadStockItemDetails()
        {
            using (StockItemSystem stockItemSystem = new StockItemSystem())
            {
                resultArgs = stockItemSystem.FetchStockItemDetails();
                if (resultArgs.Success && resultArgs != null)
                {
                    gcStockView.DataSource = resultArgs.DataSource.Table;
                    gcStockView.RefreshDataSource();
                }
            }
        }


        /// <summary>
        /// Stock Views
        /// </summary>
        private void ApplyUserRights()
        {
            this.enumUserRights.Add(StockItem.CreateStockItem);
            this.enumUserRights.Add(StockItem.EditStockItem);
            this.enumUserRights.Add(StockItem.DeleteStockItem);
            this.enumUserRights.Add(StockItem.ViewStockItem);
            this.ApplyUserRights(ucStockView, enumUserRights, (int)Menus.StockItem);
        }

        /// <summary>
        /// Delete the Item Details
        /// </summary>
        private void StockItemDelete()
        {
            try
            {
                ResultArgs resultArgs = null;
                if (gvStockView.RowCount != 0)
                {
                    if (StockItemId != 0)
                    {
                        using (StockItemSystem stockItemSystem = new StockItemSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                resultArgs = stockItemSystem.DeleteStockItem(StockItemId);
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    LoadStockItemDetails();
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
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally
            { }
        }

        /// <summary>
        /// Delete the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucStockView_DeleteClicked(object sender, EventArgs e)
        {
            StockItemDelete();
        }

        /// <summary>
        /// Edit the Stock Details
        /// </summary>
        private void ShowStockItemEdit()
        {

            if (gvStockView.RowCount != 0)
            {
                if (StockItemId != 0)
                {
                    ShowStock(StockItemId);
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

        /// <summary>
        /// Show the form
        /// </summary>
        /// <param name="StockItemId"></param>
        private void ShowStock(int StockItemId)
        {
            try
            {
                frmStockItemAdd StockItemAdd = new frmStockItemAdd(StockItemId);
                StockItemAdd.UpdateHeld += new EventHandler(OnUpdateHeld);
                StockItemAdd.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            { }
        }

        /// <summary>
        /// refresh the Stock Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadStockItemDetails();
            gvStockView.FocusedRowHandle = RowIndex;
        }
        #endregion

        private void ucStockView_DownloadExcel(object sender, EventArgs e)
        {
            using (frmExcelSupport excelsupport = new frmExcelSupport("Item", MasterImport.Item, module))
            {
                excelsupport.UpdateHeld += new EventHandler(OnUpdateHeld);
                excelsupport.ShowDialog();
            }
        }

        private void frmStockItemView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void frmStockItemView_Activated(object sender, EventArgs e)
        {
            SetVisibileShortCuts(true, true);
            LoadStockItemDetails();
        }
    }
}