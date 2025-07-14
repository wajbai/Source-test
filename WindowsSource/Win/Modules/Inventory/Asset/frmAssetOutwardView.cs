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
    public partial class frmAssetOutwardView : frmFinanceBase
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
        private int inOutId = 0;
        public int InOutId
        {
            get
            {
                RowIndex = gvOutwardMaster.FocusedRowHandle;
                inOutId = gvOutwardMaster.GetFocusedRowCellValue(colInOutId) != null ? this.UtilityMember.NumberSet.ToInteger(gvOutwardMaster.GetFocusedRowCellValue(colInOutId).ToString()) : 0;
                return inOutId;
            }
            set
            {
                inOutId = value;
            }
        }
        #endregion

        #region Constructor
        public frmAssetOutwardView()
        {
            InitializeComponent();
        }

        public frmAssetOutwardView(int projectId, string voucherDate)
            : this()
        {
            this.ProjectId = projectId;
            this.RecentVoucherDate = voucherDate;
        }
        #endregion

        #region Events

        private void frmSalesView_Load(object sender, EventArgs e)
        {
            chkSales.Checked = true;
            LoadDate();
            LoadProject();
            LoadOutwardDetails();
            SetVisibileShortCuts(true, true, true);
            SetTitle();
            ApplyUserRights();
        }

        private void ucToolBar_EditClicked_(object sender, EventArgs e)
        {
            ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProjects.EditValue.ToString());
            ProjectName = glkpProjects.Text.ToString();
        }

        private void ucToolBar_EditClicked(object sender, EventArgs e)
        {
            ShowAssetOutwardEditForm();
        }

        private void frmAssetOutwardView_EnterClicked(object sender, EventArgs e)
        {
            ShowAssetOutwardEditForm();
        }

        private void ucToolBar_RefreshClicked(object sender, EventArgs e)
        {
            LoadOutwardDetails();
        }

        private void ucToolBar_AddClicked(object sender, EventArgs e)
        {
            ShowAssetOutwardForm((int)AddNewRow.NewRow);
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
            LoadOutwardDetails();
            gvOutwardMaster.FocusedRowHandle = RowIndex;
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvOutwardMaster.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvOutwardMaster, colOutwardDate);
            }
        }

        private void frmSalesView_DoubleClick(object sender, EventArgs e)
        {
            ShowAssetOutwardEditForm();
        }

        private void gcSalesView_DoubleClick(object sender, EventArgs e)
        {
            ShowAssetOutwardEditForm();
        }

        private void gvSalesView_RowCountChanged(object sender, EventArgs e)
        {
            lblCount.Text = gvOutwardMaster.RowCount.ToString();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            LoadOutwardDetails();
        }

        private void ucToolBar_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcSalesView, this.GetMessage(MessageCatalog.Asset.SalesVoucher.SALES_PRINT_CAPTION), PrintType.DS, gvOutwardMaster, true);
        }

        private void gridLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void chkSales_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDispose.Checked)
            {
                if (chkDonate.Checked)
                {
                }
            }
            else
            {
                if (chkDonate.Checked)
                {
                }
                else
                {
                    chkSales.Checked = true;
                }
            }
        }

        private void chkDispose_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSales.Checked)
            {
                if (chkDonate.Checked)
                {
                }
            }
            else
            {
                if (chkDonate.Checked)
                {
                }
                else
                {
                    chkDispose.Checked = true;
                }
            }
        }

        private void chkDonate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSales.Checked)
            {
                if (chkDispose.Checked)
                {
                }
            }
            else if (!chkSales.Checked)
            {
                if (chkDispose.Checked)
                {
                }
                else
                {
                    chkDonate.Checked = true;
                }
            }
            else
            {
                if (chkDonate.Checked)
                {
                }
                else
                {
                    chkDonate.Checked = true;
                }
            }
        }

        #endregion

        #region Methods

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(SalesDisposalDonation.CreateSalesDisposalDonation);
            this.enumUserRights.Add(SalesDisposalDonation.EditSalesDisposalDonation);
            this.enumUserRights.Add(SalesDisposalDonation.DeleteSalesDisposalDonation);
            this.enumUserRights.Add(SalesDisposalDonation.ViewSalesDisposalDonation);
            this.ApplyUserRights(ucToolBar, enumUserRights, (int)Menus.SalesDisposalDonation);
        }

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

        private void LoadDate()
        {
            dtFromDate.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtToDate.DateTime = dtFromDate.DateTime.AddYears(1).AddDays(-1);
        }

        public void SetTitle()
        {
            this.Text = this.GetMessage(MessageCatalog.Asset.SalesVoucher.UPDATE_SALES_DISPOSE_VIEW_CAPTION);
        }

        public void LoadOutwardDetails()  //Sales, Dispose, Donate
        {
            try
            {
                DataSet dsAssetOutward = new DataSet();
                using (AssetInwardOutwardSystem assetOutwardSystem = new AssetInwardOutwardSystem())
                {
                    assetOutwardSystem.ProjectId = ProjectId;
                    assetOutwardSystem.Status = 0;
                    if (dtFromDate.DateTime > dtToDate.DateTime)
                    {
                        DateTime dtTempDate = dtToDate.DateTime;
                        dtToDate.DateTime = dtFromDate.DateTime;
                        dtFromDate.DateTime = dtTempDate;
                    }
                    assetOutwardSystem.DateFrom = this.UtilityMember.DateSet.ToDate(dtFromDate.DateTime.ToString(), false);
                    assetOutwardSystem.DateTo = this.UtilityMember.DateSet.ToDate(dtToDate.DateTime.ToString(), false);
                    string tmpFlag = string.Empty;
                    if (chkSales.Checked)
                        tmpFlag = AssetInOut.SL.ToString() + ",";
                    if (chkDispose.Checked)
                        tmpFlag += AssetInOut.DS.ToString() + ",";
                    if (chkDonate.Checked)
                        tmpFlag += AssetInOut.DN.ToString();

                    assetOutwardSystem.Flag = tmpFlag.TrimEnd(',');
                    dsAssetOutward = assetOutwardSystem.FetchAssetInOutDetailsByFlag();
                    if (dsAssetOutward != null && dsAssetOutward.Tables.Count > 0)
                    {
                        gcSalesView.DataSource = dsAssetOutward;
                        gcSalesView.DataMember = "Master";
                        gcSalesView.RefreshDataSource();
                    }
                    else
                    {
                        gcSalesView.DataSource = null;
                    }
                    gvOutwardMaster.FocusedRowHandle = 0;
                    gvOutwardMaster.FocusRectStyle = DrawFocusRectStyle.RowFocus;
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
                if (gvOutwardMaster.RowCount != 0)
                {
                    if (InOutId != 0)
                    {
                        using (AssetInwardOutwardSystem assetOutwardSystem = new AssetInwardOutwardSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                assetOutwardSystem.InoutId = InOutId;

                                string tmpFlag = string.Empty;
                                if (chkSales.Checked)
                                    tmpFlag = AssetInOut.SL.ToString() + ",";
                                if (chkDispose.Checked)
                                    tmpFlag += AssetInOut.DS.ToString() + ",";
                                if (chkDonate.Checked)
                                    tmpFlag += AssetInOut.DN.ToString();

                                assetOutwardSystem.Flag = tmpFlag.TrimEnd(',');
                                resultArgs = assetOutwardSystem.DeleteAssetInOutward();
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    LoadOutwardDetails();
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

        private void ShowAssetOutwardForm(int InOutId)
        {
            ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProjects.EditValue.ToString());
            ProjectName = glkpProjects.Text.ToString();
            DateTime dtRecentVoucherDate = (!string.IsNullOrEmpty(AppSetting.RecentVoucherDate)) ? this.UtilityMember.DateSet.ToDate(AppSetting.RecentVoucherDate, false) : dtFromDate.DateTime;
            frmAssetOutward SalesVoucherAdd = new frmAssetOutward(projectId, ProjectName, InOutId, dtRecentVoucherDate.ToString());
            SalesVoucherAdd.UpdateHeld += new EventHandler(OnUpdateHeld);
            SalesVoucherAdd.ShowDialog();
        }

        private void ShowAssetOutwardEditForm()
        {
            if (gvOutwardMaster.RowCount > 0)
            {
                if (InOutId > 0)
                {
                    ShowAssetOutwardForm(InOutId);
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

        #endregion

        private void frmAssetOutwardView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }
    }
}
