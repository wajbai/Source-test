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
    public partial class frmStockDashboard : frmFinanceBase
    {

        #region Variables
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Constructor
        public frmStockDashboard()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public int ProjectId
        {
            get
            {
                return lstProjectName.SelectedIndex >= 0 ?
                    this.UtilityMember.NumberSet.ToInteger(lstProjectName.GetItemValue(lstProjectName.SelectedIndex).ToString()) : 0;
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

        #endregion

        #region Events
        private void frmStockDashboard_Load(object sender, EventArgs e)
        {
            LoadProject();
            LoadLocation();
            LoadItems();
            FetchStockDetails();
            FetchReorderLevel();

        }
        #endregion

        #region Methods
        private void LoadProject()
        {
            using (MappingSystem mappingSystem = new MappingSystem())
            {
                resultArgs = mappingSystem.FetchProjectsLookup();
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    lstProjectName.DataSource = resultArgs.DataSource.Table;
                    lstProjectName.ValueMember = mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName;
                    lstProjectName.DisplayMember = mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName;

                    lstProjectName.SelectedValue = (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.UserProjectId) > 0) ? this.UtilityMember.NumberSet.ToInteger(this.AppSetting.UserProjectId) : ProjectId;
                }
            }
        }

        public void FetchStockDetails()
        {
            //using (StockPurchaseSalesSystem stockpurchasesystem = new StockPurchaseSalesSystem())
            //{
            //    resultArgs = stockpurchasesystem.FetchstockDashboardDetails(ProjectId, LocationId, ItemId);
            //    if (resultArgs.Success)
            //    {
            //        LoadQuantity(resultArgs.DataSource.Table);
            //    }
            //}
        }


        public void FetchReorderLevel()
        {
            using (StockPurchaseSalesSystem stockpurchasesystem = new StockPurchaseSalesSystem())
            {
                gcReorderLevel.DataSource = null;
                resultArgs = stockpurchasesystem.FetchReorderLevel(ProjectId);
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    gcReorderLevel.DataSource = resultArgs.DataSource.Table;
                }
            }
        }

        public void LoadQuantity(DataTable dtquantity)
        {
            chtStock.Series[0].DataSource = dtquantity;
            chtStock.Series[0].ArgumentDataMember = "MONTH_NAME";
            chtStock.Series[0].ValueDataMembers.AddRange(new string[] { "QUANTITY" });
        }

        private void LoadLocation()
        {
            try
            {
                using (StockPurchaseSalesSystem salesSystem = new StockPurchaseSalesSystem())
                {
                    resultArgs = salesSystem.FetchStockItemLocationDetails();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        //this.UtilityMember.ComboSet.BindGridLookUpComboEmptyItem(glkLocation, resultArgs.DataSource.Table, "LOCATION_NAME", "LOCATION_ID", true, "");
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkLocation, resultArgs.DataSource.Table, "LOCATION_NAME", "LOCATION_ID");
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
                        //this.UtilityMember.ComboSet.BindGridLookUpComboEmptyItem(glkItem, resultArgs.DataSource.Table, "ITEM_NAME", "ITEM_ID", true, " ");
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkItem, resultArgs.DataSource.Table, "ITEM_NAME", "ITEM_ID");
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

        #endregion

        private void lstProjectName_DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {
            if (lstProjectName.SelectedIndex == e.Index)
            {
                e.Appearance.Font = new Font(lstProjectName.Font, FontStyle.Bold);
            }
        }

        private void lstProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            FetchStockDetails();
            FetchReorderLevel();
        }

        private void peRefresh_Click(object sender, EventArgs e)
        {
            LoadProject();
            LoadLocation();
            LoadItems();
            FetchStockDetails();
            FetchReorderLevel();
        }
        private void glkLocation_Leave(object sender, EventArgs e)
        {
            FetchStockDetails();
        }

        private void glkItem_Leave(object sender, EventArgs e)
        {
            FetchStockDetails();
        }
    }
}