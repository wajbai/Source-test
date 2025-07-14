using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.Model;
using Bosco.Model.Inventory.Stock;

namespace ACPP.Modules.Inventory.Stock
{
    public partial class frmSoldUtilizedView : frmFinanceBase
    {
        #region Declaration
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Constructor
        public frmSoldUtilizedView()
        {
            InitializeComponent();
        }

        public frmSoldUtilizedView(string recentVDate, int ProjID, StockSalesTransType stktype)
            : this()
        {
            ProjectId = ProjID;
            RecentDate = recentVDate;
            StockType = (int)stktype;
        }

        #endregion

        #region Properties
        private int salesid;
        private int SalesId
        {
            set
            {
                salesid = value;
            }
            get
            {
                salesid = gvSoldUtilized.GetFocusedRowCellValue(colSalesId) != null ?
                    this.UtilityMember.NumberSet.ToInteger(gvSoldUtilized.GetFocusedRowCellValue(colSalesId).ToString()) : 0;
                return salesid;
            }
        }

        private int projectId = 0;
        public int ProjectId
        {
            get
            {
                return projectId;
            }
            set
            {
                projectId = value;
            }
        }
        private string projectName = string.Empty;
        private string ProjectName
        {
            set
            {
                projectName = glkpProject.Text;
            }
            get
            {
                return projectName;
            }
        }
        private string recentdate = string.Empty;
        private string RecentDate
        {
            set
            {
                recentdate = value;
            }
            get
            {
                return recentdate;
            }
        }

        private int stocktype = 0;
        public int StockType
        {
            get
            {
                return stocktype;
            }
            set
            {
                stocktype = value;
            }
        }
        #endregion

        private void frmSoldUtilizedView_Load(object sender, EventArgs e)
        {
            SetVisibileShortCuts(true, true);
            LoadDefaults();
        }


        #region Methods
        public void LoadDefaults()
        {
            setTitle();
            LoadDate();
            LoadProject();
            LoadStockSalesDetails();
        }

        private void LoadDate()
        {
            deDateFrom.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deDateTo.DateTime = deDateFrom.DateTime.AddYears(1).AddDays(-1);

            //commond by sudhakar
           // deDateFrom.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
           // deDateFrom.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
           // deDateTo.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
           // deDateTo.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
           //// deDateFrom.DateTime = !string.IsNullOrEmpty(RecentDate) ? UtilityMember.DateSet.ToDate(RecentDate, false) : UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
           // deDateTo.DateTime = deDateFrom.DateTime.AddYears(1).AddDays(-1);
           // //  deDateTo.DateTime = deDateFrom.DateTime.AddMonths(1).AddDays(-1);
        }

        private void setTitle()
        {
            this.Text = (StockType == 0) ? this.GetMessage(MessageCatalog.Stock.StockSales.SALES_CAPTION) : (StockType == 1) ? this.GetMessage(MessageCatalog.Stock.StockSales.UTILIZE_CAPTION) : this.GetMessage(MessageCatalog.Stock.StockSales.DISPOSAL_CAPTION);
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
                        this.glkpProject.EditValueChanged -= new System.EventHandler(this.glkpProject_EditValueChanged);
                        glkpProject.EditValue = (ProjectId != 0) ? ProjectId : glkpProject.Properties.GetKeyValue(0);
                        if (ProjectId == 0)
                        {
                            ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
                        }
                        this.glkpProject.EditValueChanged += new System.EventHandler(this.glkpProject_EditValueChanged);
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

        #endregion

        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
        }

        private void ucSoldUtilized_AddClicked(object sender, EventArgs e)
        {
            showSoldUtilizeForm((int)AddNewRow.NewRow);
        }

        public void showSoldUtilizeForm(int salesID)
        {
            try
            {
                frmUtiliseOrSoldItems utlizesold = new frmUtiliseOrSoldItems(salesID, deDateFrom.DateTime.ToString(), ProjectId, glkpProject.Text, StockType);
                utlizesold.UpdateHeld += new EventHandler(OnUpdateHeld);
                utlizesold.ShowDialog();
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
            finally
            { }
        }

        /// <summary>
        /// Refresh the grid after adding and editing the values. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadStockSalesDetails();
        }

        private void LoadStockSalesDetails()
        {
            DataSet dsVoucher = new DataSet();
            try
            {
                using (StockSalesSystem salesSystem = new StockSalesSystem())
                {
                    salesSystem.ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
                    salesSystem.DateFrom = deDateFrom.DateTime;
                    salesSystem.DateTo = deDateTo.DateTime;
                    salesSystem.SalesFlag = StockType;
                    dsVoucher = salesSystem.FetchSalesDetails();
                    gcSoldUtilized.DataSource = null;                 
                    if (dsVoucher.Tables.Count != 0)
                    {
                        gcSoldUtilized.DataSource = dsVoucher;
                        gcSoldUtilized.DataMember = "Master";
                        gcSoldUtilized.RefreshDataSource();
                    }
                    else
                    {
                        gcSoldUtilized.DataSource = null;
                        gcSoldUtilized.RefreshDataSource();
                    }
                    gvSoldUtilized.FocusedRowHandle = 0;
                }
              
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void ucSoldUtilized_EditClicked(object sender, EventArgs e)
        {
            showSoldUtilizeForm(SalesId);
        }

        private void ucSoldUtilized_RefreshClicked(object sender, EventArgs e)
        {
            LoadStockSalesDetails();
        }

        private void gvSoldUtilized_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvSoldUtilized.RowCount.ToString();
        }

        private void ucSoldUtilized_DeleteClicked(object sender, EventArgs e)
        {
            DeletesoldutilisedDetails();
        }

        public void DeletesoldutilisedDetails()
        {
            try
            {
                if (gvSoldUtilized.RowCount != 0)
                {
                    if (SalesId != 0)
                    {
                        using (StockSalesSystem salessystem = new StockSalesSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                salessystem.SalesId = SalesId;
                                resultArgs = salessystem.DeleteSoldUtlized();

                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    LoadStockSalesDetails();
                                    if (chkShowFilter.Checked && gvItemDetails.RowCount == 0)
                                        chkShowFilter.Checked = false;
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

        private void gcSoldUtilized_DoubleClick(object sender, EventArgs e)
        {
            showSoldUtilizeForm(SalesId);
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvSoldUtilized.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvSoldUtilized, colCustomerName);
            }
        }

        private void frmSoldUtilizedView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            LoadStockSalesDetails();
        }

        private void ucSoldUtilized_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSoldUtilizedView_Activated(object sender, EventArgs e)
        {
            SetVisibileShortCuts(true, true);
            LoadDefaults();
        }

        private void ucSoldUtilized_PrintClicked(object sender, EventArgs e)
        {
            this.PrintGridViewDetails(gcSoldUtilized, this.GetMessage(MessageCatalog.Stock.StockMasterPurchase.PRINT_CAPTION), PrintType.DS, gvSoldUtilized, true);
        }

        /// <summary>
        /// Stock Views
        /// </summary>
        private void ApplyUserRights()
        {
            this.enumUserRights.Add(StockSales.CreateStockSales);
            this.enumUserRights.Add(StockSales.EditStockSales);
            this.enumUserRights.Add(StockSales.DeleteStockSales);
            this.enumUserRights.Add(StockSales.ViewStockSales);
            this.ApplyUserRights(ucSoldUtilized, enumUserRights, (int)Menus.StockSales);
        }
    }
}