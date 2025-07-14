using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model;
using Bosco.Utility;
using Bosco.Model.UIModel;
using Bosco.Model.Inventory.Stock;

namespace ACPP.Modules.Inventory.Stock
{
    public partial class frmStockOpeningBalance : frmFinanceBaseAdd
    {
        #region Decelaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public frmStockOpeningBalance()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public int ProjectId { get; set; }
        public int LocationId { get; set; }
        public int ItemId { get; set; }
        public double Quantity { get; set; }
        public decimal Rate { get; set; }
        private DataTable dtStockOPBalance { get; set; }

        #endregion

        #region Events
        private void frmStockOpeningBalance_Load(object sender, EventArgs e)
        {
            loadLocation();
            loadProjects();
        }

        private void glkpLocation_EditValueChanged(object sender, EventArgs e)
        {
            FetchStockBalance();
        }

        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            FetchStockBalance();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateStockOpening())
                {
                    using (StockBalanceSystem BalanceSystem = new StockBalanceSystem())
                    {
                        BalanceSystem.projectId = glkpProject.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
                        BalanceSystem.locationid = glkpLocation.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpLocation.EditValue.ToString()) : 0;
                        BalanceSystem.dtStockOPBalance = gcStockOPBalance.DataSource as DataTable;
                        resultArgs = BalanceSystem.UpdateStockOpBalance();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            glkpProject.Focus();
                            glkpProject.Select();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gcStockOPBalance_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                //if (glkpLocation.EditValue != null)
                //{
                //    gvStockOPBalance.Focus();
                //}
                //else 
                //if (gvStockOPBalance.IsLastRow && gvStockOPBalance.FocusedColumn == colRate && e.Shift && e.KeyCode == Keys.Tab)
                //{
                //    gvStockOPBalance.CloseEditor();
                //    e.SuppressKeyPress = true;
                //    e.Handled = true;
                //    btnSave.Select();
                //    btnSave.Focus();
                //}

                //if (gvStockOPBalance.IsLastRow)
                //{
                //    gvStockOPBalance.CloseEditor();
                //    e.SuppressKeyPress = true;
                //    e.Handled = true;
                //    btnSave.Select();
                //    btnSave.Focus();
                //}
                //else 
                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control &&
                gvStockOPBalance.FocusedColumn == colRate && gvStockOPBalance.IsLastRow)
                {
                    btnSave.Focus();
                }
                else if (gvStockOPBalance.IsFirstRow && gvStockOPBalance.FocusedColumn == colQuantity && e.Shift && e.KeyCode == Keys.Tab)
                {
                    glkpLocation.Focus();
                    glkpLocation.Select();
                }
                // (gvStockOPBalance.IsLastRow && gvStockOPBalance.FocusedColumn == colRate)
                //{
                //    btnSave.Focus();
                //    btnSave.Select();
                //}
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvStockOPBalance.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvStockOPBalance, colItemName);
            }
        }

        #endregion

        #region Methods

        private bool ValidateStockOpening()
        {
            bool isTrue = true;
            if (this.glkpProject.EditValue == null || string.IsNullOrEmpty(glkpProject.Text))
            {
                this.SetBorderColorForGridLookUpEdit(glkpProject);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Project.PROJECT_NAME_EMPTY));
                isTrue = false;
                glkpProject.Focus();
            }
            else if (glkpLocation.EditValue == null || string.IsNullOrEmpty(glkpLocation.Text))
            {
                this.SetBorderColorForGridLookUpEdit(glkpLocation);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Stock.StockItem.STOCK_LOCATION_EMPTY));
                isTrue = false;
                glkpLocation.Focus();
            }
            else if (gcStockOPBalance.DataSource != null)
            {
                //if (!IsValidGrid())
                //{
                //    isTrue = false;
                //}
                DataTable dt = new DataTable();
                dt = gcStockOPBalance.DataSource as DataTable;
                if (dt.Rows.Count == 0)
                {
                    ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NO_RECORDS_TO_SAVE));
                    isTrue = false;
                }
            }
            return isTrue;
        }

        private bool IsValidGrid()
        {
            DataTable dtTrans = gcStockOPBalance.DataSource as DataTable;

            decimal qty = 0;
            decimal rate = 0;
            bool isValid = false;
            int RowPosition = 0;

            string validateMessage = "Required Information not filled, Stock Openning Details are not filled fully";

            DataView dv = new DataView(dtTrans);
            dv.RowFilter = "(ITEM_ID>0 OR QUANTITY>0 OR RATE>0)";
            gvStockOPBalance.FocusedColumn = colRate;
            if (dv.Count > 0)
            {
                isValid = true;
                foreach (DataRowView drTrans in dv)
                {
                    qty = this.UtilityMember.NumberSet.ToDecimal(drTrans["QUANTITY"].ToString());
                    rate = this.UtilityMember.NumberSet.ToDecimal(drTrans["RATE"].ToString());

                    if ((qty == 0 || rate == 0))
                    {
                        if (qty == 0)
                        {
                            validateMessage = "Required Information not filled, Quantity is empty";
                            gvStockOPBalance.FocusedColumn = colQuantity;
                        }
                        else if (rate == 0)
                        {
                            validateMessage = "Required Information not filled, Rate is empty";
                            gvStockOPBalance.FocusedColumn = colRate;
                        }
                        isValid = false;
                        break;
                    }
                    RowPosition = RowPosition + 1;
                }
            }
            if (!isValid)
            {
                this.ShowMessageBox(validateMessage);
                gvStockOPBalance.CloseEditor();
                gvStockOPBalance.FocusedRowHandle = gvStockOPBalance.GetRowHandle(RowPosition);
                gvStockOPBalance.ShowEditor();
            }
            return isValid;
        }

        private void loadLocation()
        {
            try
            {
                using (LocationSystem locatioSystem = new LocationSystem())
                {
                    resultArgs = locatioSystem.FetchLocationDetails();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpLocation, resultArgs.DataSource.Table, locatioSystem.AppSchema.ASSETLocationDetails.LOCATIONColumn.ColumnName, locatioSystem.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName);
                        glkpLocation.EditValue = glkpLocation.Properties.GetKeyValue(0);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        /// <summary>
        /// Fetch project for Master Project
        /// </summary>
        private void loadProjects()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    resultArgs = mappingSystem.FetchProjectsLookup();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProject, resultArgs.DataSource.Table, mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                        glkpProject.EditValue = glkpProject.Properties.GetKeyValue(0);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void FetchStockBalance()
        {
            using (StockItemSystem stockItemSystem = new StockItemSystem())
            {
                stockItemSystem.LocationId = glkpLocation.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpLocation.EditValue.ToString()) : 0;
                stockItemSystem.ProjectId = glkpProject.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
                if (stockItemSystem.LocationId > 0 && stockItemSystem.ProjectId > 0)
                {
                    resultArgs = stockItemSystem.FetchStockOPBalance();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        gcStockOPBalance.DataSource = resultArgs.DataSource.Table;
                        gcStockOPBalance.RefreshDataSource();
                    }
                }
            }
        }
        #endregion

        private void gcStockOPBalance_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // btnSave.Focus();
        }

        private void gvStockOPBalance_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvStockOPBalance.RowCount.ToString();
        }
    }
}