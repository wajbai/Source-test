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

namespace ACPP.Modules.Inventory.Stock
{
    public partial class frmStockUpdation : frmFinanceBaseAdd
    {
        #region Declaration
        public event EventHandler UpdateHeld;
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Constructor
        public frmStockUpdation()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void frmStockUpdation_Load(object sender, EventArgs e)
        {
            LoadItems();
            LoadLocation();
            LoadProject();
            LoadStockType();
            LoadDate();
        }

        public void LoadStockType()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Type", typeof(string));

            dt.Rows.Add(0, "Inward");
            dt.Rows.Add(1, "Outward");

            this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpStockType, dt, "Type", "Id");
        }

        private void LoadDate()
        {
            deDate.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deDate.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            deDate.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (IsValidEntry())
            {
                using (StockBalanceSystem balancesystem = new StockBalanceSystem())
                {
                    resultArgs = balancesystem.UpdateStockDetails(
                        this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()),
                        deDate.DateTime,
                        this.UtilityMember.NumberSet.ToInteger(glkpLocation.EditValue.ToString()),
                        this.UtilityMember.NumberSet.ToInteger(glkItem.EditValue.ToString()),
                        this.UtilityMember.NumberSet.ToDouble(txtQuantity.Text),0,
                        this.UtilityMember.NumberSet.ToInteger(glkpStockType.EditValue.ToString()));

                    if (resultArgs.Success)
                    {
                        this.ShowMessageBox("Stock Updated.");
                        FetchStockDetails();
                    }
                }
            }
        }
        #endregion

        #region Methods

        private void LoadLocation()
        {
            try
            {
                using (StockPurchaseSalesSystem salesSystem = new StockPurchaseSalesSystem())
                {
                    resultArgs = salesSystem.FetchStockItemLocationDetails();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpLocation, resultArgs.DataSource.Table,
                            "LOCATION_NAME", "LOCATION_ID");
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

        private void LoadItems()
        {
            try
            {
                using (StockPurchaseSalesSystem salesSystem = new StockPurchaseSalesSystem())
                {
                    resultArgs = salesSystem.FetchStockItemDetails();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
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

        private void LoadProject()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    resultArgs = mappingSystem.FetchProjectsLookup();
                    glkpProject.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProject, resultArgs.DataSource.Table, mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                        glkpProject.EditValue = glkpProject.Properties.GetKeyValue(0);
                    }
                    else
                    {
                        XtraMessageBox.Show(resultArgs.Message);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        public bool IsValidEntry()
        {
            bool Isvalid = true;

            if (string.IsNullOrEmpty(deDate.Text))
            {
                this.ShowMessageBox("Date is Empty");
                Isvalid = false;
                deDate.Focus();
            }
            else if (glkpLocation.EditValue == null)
            {
                this.ShowMessageBox("Location is Empty");
                Isvalid = false;
                glkpLocation.Focus();
            }
            else if (glkItem.EditValue == null)
            {
                this.ShowMessageBox("Item is Empty");
                Isvalid = false;
                glkItem.Focus();
            }
            else if (string.IsNullOrEmpty(txtQuantity.Text) || this.UtilityMember.NumberSet.ToInteger(txtQuantity.Text) <= 0)
            {
                this.ShowMessageBox("Quantity is Empty");
                Isvalid = false;
                txtQuantity.Focus();
            }
            return Isvalid;
        }

        public void FetchStockDetails()
        {
            using (StockBalanceSystem balancesystem = new StockBalanceSystem())
            {
                if (glkpProject.EditValue != null && glkItem.EditValue != null && glkpLocation.EditValue != null)
                {
                    resultArgs = balancesystem.GetOPBalance(this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()),
                        this.UtilityMember.NumberSet.ToInteger(glkItem.EditValue.ToString()),
                        this.UtilityMember.NumberSet.ToInteger(glkpLocation.EditValue.ToString()),
                        deDate.Text);

                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        lblOPBalance.Text = resultArgs.DataSource.Table.Rows[0]["QUANTITY"].ToString();
                    }
                    else
                    {
                        lblOPBalance.Text = "0";
                    }

                    resultArgs = balancesystem.GetClosingBalance(this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()),
                        this.UtilityMember.NumberSet.ToInteger(glkItem.EditValue.ToString()),
                        this.UtilityMember.NumberSet.ToInteger(glkpLocation.EditValue.ToString()),
                        deDate.Text);
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            lblClosingBalance.Text = resultArgs.DataSource.Table.Rows[0]["QUANTITY"].ToString();
                        }
                        else
                        {
                            lblClosingBalance.Text = "0";
                        }

                    }
                    resultArgs = balancesystem.GetCurrentBalance(this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()),
                        this.UtilityMember.NumberSet.ToInteger(glkItem.EditValue.ToString()),
                        this.UtilityMember.NumberSet.ToInteger(glkpLocation.EditValue.ToString()));
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            lblCurrentBalance.Text = resultArgs.DataSource.Table.Rows[0]["QUANTITY"].ToString();
                        }
                        else
                        {
                            lblCurrentBalance.Text = "0";
                        }
                    }
                }
            }
        }
        #endregion

        private void deDate_Leave(object sender, EventArgs e)
        {
            FetchStockDetails();
        }

        private void glkpLocation_Leave(object sender, EventArgs e)
        {
            FetchStockDetails();
        }

        private void glkItem_Leave(object sender, EventArgs e)
        {
            FetchStockDetails();
        }

        private void glkpStockType_Leave(object sender, EventArgs e)
        {
            FetchStockDetails();
        }
    }
}