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
using Bosco.Model.Inventory.Stock;
using Bosco.Model.UIModel;
using DevExpress.XtraGrid.Views.Grid;

namespace ACPP.Modules.Inventory.Stock
{
    public partial class frmStockHomeView : frmFinanceBase
    {
        #region Variables
        public event EventHandler UpdateHeld;
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Constructor
        public frmStockHomeView()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public int ProjectId
        {
            get
            {
                return glkpProject.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : this.UtilityMember.NumberSet.ToInteger(this.AppSetting.UserProjectId);
            }
        }

        public int LocationId
        {
            get
            {
                return glkLocation.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkLocation.EditValue.ToString()) : 0;
            }
        }
        public int ItemId
        {
            get
            {
                return glkItem.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkItem.EditValue.ToString()) : 0;
            }
        }
        public int GroupId
        {
            get
            {
                return glkGroup.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkGroup.EditValue.ToString()) : 0;
            }
        }
        public int CategoryId
        {
            get
            {
                return glkCategory.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkCategory.EditValue.ToString()) : 0;
            }
        }
        #endregion

        #region Events

        private void frmStockHomeView_Load(object sender, EventArgs e)
        {
            SetVisibileShortCuts(true, true);
            LoadProject();
            LoadGroup();
            //LoadStockCategory();
            LoadLocation();
            LoadItems();
            LoadDate();
            FetchStockDetails();
            gvStockDetails.ExpandAllGroups();
            FetchChartStockDetails();
            FetchReorderLevel();

        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            FetchStockDetails();
            FetchChartStockDetails();
            FetchReorderLevel();
            gvStockDetails.ExpandAllGroups();
        }

        private void ucStockView_RefreshClicked(object sender, EventArgs e)
        {
            FetchStockDetails();
            FetchChartStockDetails();
            FetchReorderLevel();
            gvStockDetails.ExpandAllGroups();
            LoadProject();
            LoadItems();
            LoadGroup();
            LoadLocation();
        }

        private void gvStockDetails_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                double qntuy = this.UtilityMember.NumberSet.ToDouble(view.GetRowCellDisplayText(e.RowHandle, view.Columns["QUANTITY"]));
                if (qntuy < 0)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvStockDetails.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvStockDetails, colStockDate);
            }
        }

        private void frmStockHomeView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void ucStockView_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvStockDetails_RowCountChanged(object sender, EventArgs e)
        {
            //lblRecordCount.Text = gvStockDetails.RowCount.ToString();
        }

        private void ucStockView_PrintClicked(object sender, EventArgs e)
        {
            this.PrintGridViewDetails(gcStockDetails, MessageCatalog.Stock.StockItem.STOCK_REGISTER, PrintType.DT, gvStockDetails, true);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.F3))
            {
                deStockDate.Focus();
            }
            if (KeyData == (Keys.F5))
            {
                glkpProject.Focus();
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        #endregion

        #region Methods
        private void LoadLocation()
        {
            try
            {
                using (LocationSystem locationSystem = new LocationSystem())
                {
                    resultArgs = locationSystem.FetchLocationDetails();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpComboEmptyItem(glkLocation, resultArgs.DataSource.Table, "LOCATION", "LOCATION_ID", true, "<--All-->");
                        glkLocation.EditValue = glkLocation.Properties.GetKeyValue(0);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void LoadItems()
        {
            try
            {
                using (StockPurchaseSalesSystem salesSystem = new StockPurchaseSalesSystem())
                {
                    resultArgs = salesSystem.FetchStockItemDetails();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpComboEmptyItem(glkItem, resultArgs.DataSource.Table, "ITEM_NAME", "ITEM_ID", true, "<--All-->");
                        glkItem.EditValue = this.glkItem.Properties.GetKeyValue(0);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void LoadProject()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    resultArgs = mappingSystem.FetchProjectsLookup();
                    glkpProject.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProject, resultArgs.DataSource.Table, mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                        glkpProject.EditValue = this.UtilityMember.NumberSet.ToInteger(this.AppSetting.UserProjectId);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void LoadGroup()
        {
            try
            {
                using (StockGroupSystem stockGroupSystem = new StockGroupSystem())
                {
                    resultArgs = stockGroupSystem.FetchStockGroupDetails();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpComboEmptyItem(glkGroup, resultArgs.DataSource.Table, stockGroupSystem.AppSchema.StockGroup.GROUP_NAMEColumn.ColumnName, stockGroupSystem.AppSchema.StockGroup.GROUP_IDColumn.ColumnName, true, "<--All-->");
                        glkGroup.EditValue = this.glkGroup.Properties.GetKeyValue(0);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowSuccessMessage(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        private void LoadDate()
        {
            deStockDate.Properties.MinValue = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deStockDate.Properties.MaxValue = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            deStockDate.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.RecentVoucherDate, false);
        }

        //private void LoadStockCategory()
        //{
        //    try
        //    {
        //        using (StockCategorySystem CategorySystem = new StockCategorySystem())
        //        {
        //            resultArgs = CategorySystem.FetchCategoryDetails();
        //            if (resultArgs.Success && resultArgs != null && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
        //            {
        //                this.UtilityMember.ComboSet.BindGridLookUpComboEmptyItem(glkCategory, resultArgs.DataSource.Table, CategorySystem.AppSchema.ASSETCategory.NAMEColumn.ColumnName, CategorySystem.AppSchema.ASSETCategory.CATEGORY_IDColumn.ColumnName, true, "");
        //                glkCategory.EditValue = glkCategory.Properties.GetKeyValue(0);
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        public void FetchStockDetails()
        {
            using (StockBalanceSystem balancesystem = new StockBalanceSystem())
            {
                resultArgs = balancesystem.FetchStockDetails(ProjectId, deStockDate.Text, LocationId, ItemId, GroupId, CategoryId);
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    gcStockDetails.DataSource = resultArgs.DataSource.Table;
                    lblRecordCount.Text = resultArgs.DataSource.Table.Rows.Count.ToString();
                }
                else
                {
                    gcStockDetails.DataSource = null;
                }
            }
        }

        public void FetchChartStockDetails()
        {
            using (StockPurchaseSalesSystem stockpurchasesystem = new StockPurchaseSalesSystem())
            {
                resultArgs = stockpurchasesystem.FetchstockDashboardDetails(ProjectId, LocationId, ItemId, deStockDate.DateTime.ToShortDateString());
                if (resultArgs.Success)
                {
                    LoadQuantity(resultArgs.DataSource.Table);
                }
            }
        }

        public void FetchReorderLevel()
        {
            using (StockPurchaseSalesSystem stockpurchasesystem = new StockPurchaseSalesSystem())
            {
                gcReorderLevel.DataSource = null;
                resultArgs = stockpurchasesystem.FetchReorderLevel(ProjectId);
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    gcReorderLevel.DataSource = resultArgs.DataSource.Table;

                }
            }
        }

        public void LoadQuantity(DataTable dtquantity)
        {
            using (StockItemSystem item = new StockItemSystem())
            {
                chtStock.Series[0].DataSource = dtquantity;
                chtStock.Series[0].ArgumentDataMember = item.AppSchema.DashBoard.MONTH_NAMEColumn.ToString();
                chtStock.Series[0].ValueDataMembers.AddRange(new string[] { item.AppSchema.StockItem.QUANTITYColumn.ToString() });
            }
        }

        #endregion

        private void frmStockHomeView_Activated(object sender, EventArgs e)
        {
            SetVisibileShortCuts(true, true);
            LoadProject();
            LoadGroup();
            //LoadStockCategory();
            LoadLocation();
            LoadItems();
            LoadDate();
            FetchStockDetails();
            gvStockDetails.ExpandAllGroups();
            FetchChartStockDetails();
            FetchReorderLevel();
        }
    }
}