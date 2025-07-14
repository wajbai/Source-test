using System;
using System.Data;
using System.Windows.Forms;

using Bosco.Utility;
using Bosco.Model.UIModel;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using Bosco.Model;

namespace ACPP.Modules.Asset.Transactions
{
    public partial class frmSalesView : frmBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = new ResultArgs();
        private string ProjectName = null;
        private int RowIndex = 0;
        #endregion

        #region Property
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
        private string recentVoucherDate = string.Empty;
        private string RecentVoucherDate
        {
            set
            {
                recentVoucherDate = value;
            }
            get
            {
                return recentVoucherDate;
            }
        }
        private int Salesid = 0;
        public int SalesId
        {
            get
            {
                RowIndex = gvSalesView.FocusedRowHandle;
                Salesid = gvSalesView.GetFocusedRowCellValue(colSalesId) != null ? this.UtilityMember.NumberSet.ToInteger(gvSalesView.GetFocusedRowCellValue(colSalesId).ToString()) : 0;
                return Salesid;
            }
            set
            {
                Salesid = value;
            }
        }
        #endregion

        #region Constructor
        public frmSalesView()
        {
            InitializeComponent();
        }

        public frmSalesView(int projectId, string voucherDate)
            : this()
        {
            this.ProjectId = projectId;
            this.RecentVoucherDate = voucherDate;
        }
        #endregion

        #region Events

        private void ucToolBar_EditClicked_(object sender, EventArgs e)
        {
            ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProjects.EditValue.ToString());
            ProjectName = glkpProjects.Text.ToString();
        }

        private void ucToolBar_EditClicked(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void ucToolBar_RefreshClicked(object sender, EventArgs e)
        {
            LoadSalesVoucherDetails();
        }

        private void ucToolBar_AddClicked(object sender, EventArgs e)
        {
            ShowSalesVoucherForm((int)AddNewRow.NewRow);
        }

        private void ucToolBar_DeleteClicked(object sender, EventArgs e)
        {
            DeletSalesVoucherDetails();
        }

        private void ucToolBar_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void glkpProjects_EditValueChanged(object sender, EventArgs e)
        {
            ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProjects.EditValue.ToString());
            ProjectName = glkpProjects.Text.ToString();
            //LoadSalesDetailsByDate();
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadSalesVoucherDetails();
            gvSalesView.FocusedRowHandle = RowIndex;
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvSalesView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvSalesView, colSalesDate);
            }
        }

        private void frmSalesView_Load(object sender, EventArgs e)
        {
            SetVisibileShortCuts(true, true, true);
            LoadProject();
            LoadSalesVoucherDetails();
            LoadProjectDate();
            LoadSalesDetailsByDate();
            LoadDefaults();
        }

        private void LoadDefaults()
        {
            dtFromDate.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtFromDate.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            dtToDate.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtToDate.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            dtFromDate.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtFromDate.DateTime = UtilityMember.DateSet.ToDate(RecentVoucherDate.ToString(), false);
            DateTime DateFrom = new DateTime(dtFromDate.DateTime.Year, dtFromDate.DateTime.Month, 1);
            dtFromDate.DateTime = DateFrom;
            dtToDate.DateTime = dtFromDate.DateTime.AddMonths(1).AddDays(-1);
        }
        private void frmSalesView_DoubleClick(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void gcSalesView_DoubleClick(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void gvSalesView_RowCountChanged(object sender, EventArgs e)
        {
            lblCount.Text = gvSalesView.RowCount.ToString();
        }

        #endregion

        #region Methods
        /// <summary>
        /// To load the Project
        /// </summary>`
        /// 
        private void LoadProject()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    resultArgs = mappingSystem.FetchProjectsLookup();
                    glkpProjects.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProjects, resultArgs.DataSource.Table, mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                        this.glkpProjects.EditValueChanged -= new System.EventHandler(this.glkpProjects_EditValueChanged);
                        glkpProjects.EditValue = (ProjectId != 0) ? ProjectId : glkpProjects.Properties.GetKeyValue(0);
                        this.glkpProjects.EditValueChanged += new System.EventHandler(this.glkpProjects_EditValueChanged);
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

        private void LoadProjectDate()
        {
            dtFromDate.DateTime = this.UtilityMember.DateSet.ToDate(RecentVoucherDate, false);
            dtToDate.DateTime = this.UtilityMember.DateSet.ToDate(RecentVoucherDate, false).AddMonths(1).AddDays(-1);
        }

        public void LoadSalesVoucherDetails()
        {
            try
            {
                DataSet dsAssetSales = new DataSet();
                using (AssetSalesSystem salesSystem = new AssetSalesSystem())
                {
                    salesSystem.ProjectId = ProjectId;
                    salesSystem.DateFrom = this.UtilityMember.DateSet.ToDate(dtFromDate.DateTime.ToString(), false);
                    salesSystem.DateTo = this.UtilityMember.DateSet.ToDate(dtToDate.DateTime.ToString(), false);
                    dsAssetSales = salesSystem.FetchAssetSalesDetails(AssetFormMode.Sales);
                    if (dsAssetSales != null && dsAssetSales.Tables.Count > 0)
                    {
                        gcSalesView.DataSource = dsAssetSales;
                        gcSalesView.DataMember = "Master";
                        gcSalesView.RefreshDataSource();
                    }
                    else
                    {
                        gcSalesView.DataSource = null;
                    }

                    gvSalesView.FocusedRowHandle = 0;
                    gvSalesView.FocusRectStyle = DrawFocusRectStyle.RowFocus;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }

        }

        private void DeletSalesVoucherDetails()
        {
            try
            {
                ResultArgs resultArgs = null;
                if (gvSalesView.RowCount != 0)
                {
                    if (SalesId != 0)
                    {
                        using (AssetSalesSystem salesSystem = new AssetSalesSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                salesSystem.SalesId = SalesId;
                                resultArgs = salesSystem.DeleteAssetSales();
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    LoadSalesVoucherDetails();
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

        private void ShowSalesVoucherForm(int SalesId)
        {
            ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProjects.EditValue.ToString());
            ProjectName = glkpProjects.Text.ToString();
            frmAssetOutward SalesVoucherAdd = new frmAssetOutward(projectId, ProjectName, SalesId,RecentVoucherDate);
            SalesVoucherAdd.UpdateHeld += new EventHandler(OnUpdateHeld);
            SalesVoucherAdd.ShowDialog();
        }

        private void ShowForm()
        {
            if (gvSalesView.RowCount > 0)
            {
                if (SalesId > 0)
                {
                    ShowSalesVoucherForm(Salesid);
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
        private void LoadSalesDetailsByDate()
        {
            if (dtFromDate.DateTime > dtToDate.DateTime)
            {
                DateTime dtdate = dtToDate.DateTime;
                dtToDate.DateTime = dtFromDate.DateTime;
                dtFromDate.DateTime = dtdate;
            }
            LoadSalesVoucherDetails();
        }
        #endregion

        private void btnApply_Click(object sender, EventArgs e)
        {
            LoadSalesDetailsByDate();
        }

        private void ucToolBar_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcSalesView, this.GetMessage(MessageCatalog.Asset.SalesVoucher.SALES_PRINT_CAPTION), PrintType.DS, gvSalesView, true);
        }

        private void gridLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

    }
}
