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
using Bosco.Model.Inventory.Stock;
using Bosco.Utility;
using DevExpress.XtraGrid.Views.Grid;
using ACPP.Modules.Transaction;

namespace ACPP.Modules.Inventory.Stock
{
    public partial class frmPurchaseReturnsView : frmFinanceBase
    {
        #region Declaration

        ResultArgs resultArgs;
        private int RowIndex = 0;
        #endregion

        #region Properties
        private int returnID = 0;
        private int ReturnId
        {
            get
            {
                RowIndex = gvPurchaseReturns.FocusedRowHandle;
                returnID = gvPurchaseReturns.GetFocusedRowCellValue(colRetrunId) != null ? this.UtilityMember.NumberSet.ToInteger(gvPurchaseReturns.GetFocusedRowCellValue(colRetrunId).ToString()) : 0;
                return returnID;
            }
            set
            {
                returnID = value;
            }
        }


        private int projectId = 0;
        private int ProjectId
        {
            set { projectId = value; }
            get
            {
                projectId = glkpProject.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
                return projectId;
            }
        }

        private int RecentProjectId { get; set; }
        private string RecentDate { get; set; }

        #endregion

        #region Constractors
        public frmPurchaseReturnsView()
        {
            InitializeComponent();
        }
        public frmPurchaseReturnsView(int Pid, string Rdate)
            : this()
        {
            RecentProjectId = Pid;
            RecentDate = Rdate;
        }
        #endregion

        #region Events

        private void frmPurchaseReturnsView_Load(object sender, EventArgs e)
        {
            SetVisibileShortCuts(true, true);
            SetDefaults();
            LoadProject();
            LoadPurchaseReturnsDetails();
            LoadDate();
            ApplyUserRights();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            LoadPurchaseReturnsDetails();
        }

        private void chkFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvPurchaseReturns.OptionsView.ShowAutoFilterRow = chkFilter.Checked;
            if (chkFilter.Checked)
            {
                this.SetFocusRowFilter(gvPurchaseReturns, colItem);
            }
        }

        private void gvPurchaseReturns_RowCountChanged(object sender, EventArgs e)
        {
            lblRowCount.Text = gvPurchaseReturns.RowCount.ToString();
        }

        private void ucToolbar_AddClicked(object sender, EventArgs e)
        {
            ShowForm((int)AddNewRow.NewRow);
        }

        private void ucToolbar_EditClicked(object sender, EventArgs e)
        {
            ShowForm(ReturnId);
        }

        private void ucToolbar_DeleteClicked(object sender, EventArgs e)
        {
            try
            {
                if (ReturnId > 0)
                {
                    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using (StockPurcahseReturnSystem PurchaseReturnSystem = new StockPurcahseReturnSystem())
                        {
                            PurchaseReturnSystem.ReturnId = ReturnId;
                            PurchaseReturnSystem.ProjectId = ProjectId;
                            PurchaseReturnSystem.ReturnDate = gvPurchaseReturns.GetFocusedRowCellValue(colDate) != null ? this.UtilityMember.DateSet.ToDate(gvPurchaseReturns.GetFocusedRowCellValue(colDate).ToString(), false) : DateTime.Now;
                            DataSet ds = gcPurchaseReturns.DataSource as DataSet;
                            DataView dv = ds.Tables[1].DefaultView;
                            dv.RowFilter = "RETURN_ID='" + ReturnId + "'";
                            PurchaseReturnSystem.dtItems = dv.ToTable();
                            resultArgs = PurchaseReturnSystem.DeletePurchaseReturn();
                            if (resultArgs.Success && resultArgs.RowsAffected > 0)
                            {
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                LoadPurchaseReturnsDetails();
                            }
                            else
                            {
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_FAILURE));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }
        }

        private void ucToolbar_RefreshClicked(object sender, EventArgs e)
        {
            LoadPurchaseReturnsDetails();
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadPurchaseReturnsDetails();
            gvPurchaseReturns.FocusedRowHandle = RowIndex;
        }

        private void gvPurchaseReturns_DoubleClick(object sender, EventArgs e)
        {
            ShowForm(ReturnId);
        }

        private void ucToolbar_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcPurchaseReturns, this.GetMessage(MessageCatalog.Stock.StockPurcahseReturns.STOCK_RETURN_PRINT_CAPTION), PrintType.DS, gvPurchaseReturnsDetails, true);
        }

        private void ucToolbar_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Methods


        private void ShowForm(int Id)
        {
            frmGoodsReturnAdd GoodReturn = new frmGoodsReturnAdd(Id, ProjectId, glkpProject.Text, this.UtilityMember.DateSet.ToDate(RecentDate, false));
            GoodReturn.UpdateHeld += new EventHandler(OnUpdateHeld);
            GoodReturn.ShowDialog();
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
                        glkpProject.EditValue = RecentProjectId == 0 ? glkpProject.Properties.GetKeyValue(0) : RecentProjectId;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void SetDefaults()
        {
            dtDateFrom.DateTime = this.UtilityMember.DateSet.ToDate(RecentDate, false);
            dtDateTo.DateTime = dtDateFrom.DateTime.AddYears(1).AddDays(-1);

            //dtDateTo.DateTime = this.UtilityMember.DateSet.ToDate(RecentDate, false).AddMonths(1).AddDays(-1);
        }

        private void LoadPurchaseReturnsDetails()
        {
            try
            {
                DataSet dsPurchaseReturns = new DataSet();
                using (StockPurcahseReturnSystem stockPurchaseReturnDetails = new StockPurcahseReturnSystem())
                {
                    stockPurchaseReturnDetails.ProjectId = ProjectId;
                    stockPurchaseReturnDetails.StartDate = dtDateFrom.DateTime;
                    stockPurchaseReturnDetails.ToDate = dtDateTo.DateTime;
                    dsPurchaseReturns = stockPurchaseReturnDetails.FetchPurchaseReturns();
                    if (dsPurchaseReturns.Tables.Count != 0)
                    {
                        gcPurchaseReturns.DataSource = dsPurchaseReturns;
                        gcPurchaseReturns.DataMember = "Master";
                        gcPurchaseReturns.RefreshDataSource();
                    }
                    else
                    {
                        gcPurchaseReturns.DataSource = null;
                        gcPurchaseReturns.RefreshDataSource();
                    }
                    gvPurchaseReturns.FocusedRowHandle = 0;
                    gvPurchaseReturns.FocusRectStyle = DrawFocusRectStyle.RowFocus;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source);
            }


        }

        private void LoadDate()
        {
            dtDateFrom.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtDateTo.DateTime = dtDateFrom.DateTime.AddYears(1).AddDays(-1);
            //commond by sudhakar
            //dtDateFrom.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            //dtDateFrom.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            //dtDateTo.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            //dtDateTo.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            //dtDateFrom.DateTime = !string.IsNullOrEmpty(RecentDate) ? UtilityMember.DateSet.ToDate(RecentDate, false) : UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            //dtDateTo.DateTime = dtDateFrom.DateTime.AddMonths(1).AddDays(-1);
        }

        #endregion

        private void frmPurchaseReturnsView_Activated(object sender, EventArgs e)
        {
            SetVisibileShortCuts(true, true);
            SetDefaults();
            LoadProject();
            LoadPurchaseReturnsDetails();
            LoadDate();
        }

        /// <summary>
        /// Stock Views
        /// </summary>
        private void ApplyUserRights()
        {
            this.enumUserRights.Add(StockPurchaseReturn.CreateStockPurchaseReturn);
            this.enumUserRights.Add(StockPurchaseReturn.EditStockPurchaseReturn);
            this.enumUserRights.Add(StockPurchaseReturn.DeleteStockPurchaseReturn);
            this.enumUserRights.Add(StockPurchaseReturn.ViewStockPurchaseReturn);
            this.ApplyUserRights(ucToolbar, enumUserRights, (int)Menus.StockPurchaseReturn);
        }
    }
}