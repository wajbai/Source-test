using System;
using System.Windows.Forms;

using Bosco.Utility;
using Bosco.Model;
using Bosco.Model.UIModel;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System.Data;

namespace ACPP.Modules.Asset
{
    public partial class frmPurchaseView : frmBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        private int purchaseId = 0;
        private int RowIndex = 0;

        #endregion

        #region Properties
        public int PurchaseId
        {
            get
            {
                RowIndex = gvPurchaseView.FocusedRowHandle;
                purchaseId = gvPurchaseView.GetFocusedRowCellValue(colPurchaseId) != null ? this.UtilityMember.NumberSet.ToInteger(gvPurchaseView.GetFocusedRowCellValue(colPurchaseId).ToString()) : 0;
                return purchaseId;
            }
            set
            {
                purchaseId = value;
            }
        }

        public int ProjectId
        {
            get;
            set;
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
        #endregion

        #region Constructors

        public frmPurchaseView()
        {
            InitializeComponent();
        }
        public frmPurchaseView(string recentVoucherDate, int recentProjectId, string recentProject)
            : this()
        {
            ProjectId = recentProjectId;
            RecentVoucherDate = recentVoucherDate;
        }
        #endregion

        #region Events

        private void frmPurchaseView_Load(object sender, EventArgs e)
        {
            SetVisibileShortCuts(true, true, true);
            LoadDefaults();
            LoadProject();
            FetchAssetPurchaseDetails();
        }

        private void LoadDefaults()
        {
            deFromDate.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deFromDate.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            deToDate.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deToDate.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            deFromDate.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deFromDate.DateTime = UtilityMember.DateSet.ToDate(RecentVoucherDate.ToString(), false);
            DateTime DateFrom = new DateTime(deFromDate.DateTime.Year, deFromDate.DateTime.Month, 1);
            deFromDate.DateTime = DateFrom;
            deToDate.DateTime = deFromDate.DateTime.AddMonths(1).AddDays(-1);
        }

        private void ucPurchaseVoucherView_AddClicked(object sender, EventArgs e)
        {
            ShowPurchase((int)AddNewRow.NewRow);
        }

        private void ucPurchaseVoucherView_EditClicked(object sender, EventArgs e)
        {
            ShowEditAssetPurchase();
        }

        private void gvPurchaseView_DoubleClick(object sender, EventArgs e)
        {
            ShowEditAssetPurchase();
        }
        private void gvAssetPurchaseDetails_DoubleClick(object sender, EventArgs e)
        {
            ShowEditAssetPurchase();
        }

        private void ucPurchaseVoucherView_DeleteClicked(object sender, EventArgs e)
        {
            DeleteAssetPurchase();
        }

        private void ucPurchaseVoucherView_PrintClicked(object sender, EventArgs e)
        {
                PrintGridViewDetails(gcPurchaseView, this.GetMessage(MessageCatalog.Asset.PurchaseVoucher.PURCHASE_PRINT_CAPTION), PrintType.DS, gvPurchaseView, true);
        }

        private void ucPurchaseVoucherView_RefreshClicked(object sender, EventArgs e)
        {
            LoadProject();
            ProjectId = glkpProject.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
            FetchAssetPurchaseDetails();

        }

        private void ucPurchaseVoucherView_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvPurchaseView_RowCountChanged(object sender, EventArgs e)
        {
            lblRowCount.Text = gvPurchaseView.RowCount.ToString();
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchAssetPurchaseDetails();
            gvPurchaseView.FocusedRowHandle = RowIndex;
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvPurchaseView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvPurchaseView, colVendorName);
            }
        }

        private void frmPurchaseView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (deFromDate.DateTime > deToDate.DateTime)
            {
                DateTime dateTo = deToDate.DateTime;
                deToDate.DateTime = deFromDate.DateTime;
                deFromDate.DateTime = dateTo.Date;
            }
            FetchAssetPurchaseDetails();
        }

        #endregion

        #region Methods
        /// <summary>
        /// show the form
        /// </summary>
        /// <param name="VendorID"></param>
        private void ShowPurchase(int purchaseId)
        {
            try
            {
                int ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
                string ProjectName = glkpProject.Text.ToString();
                frmInwardVoucherAdd purchaseAdd = new frmInwardVoucherAdd(RecentVoucherDate, ProjectId, ProjectName, purchaseId);
                purchaseAdd.UpdateHeld += new EventHandler(OnUpdateHeld);
                purchaseAdd.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            { }
        }

        public void FetchAssetPurchaseDetails()
        {
            try
            {
                DataSet dsAssetPurchase = new DataSet();
                using (AssetPurchaseVoucherSystem purchaseSystem = new AssetPurchaseVoucherSystem())
                {
                    purchaseSystem.FromDate = deFromDate.DateTime;
                    purchaseSystem.ToDate = deToDate.DateTime;
                    purchaseSystem.ProjectId = ProjectId;
                    dsAssetPurchase = purchaseSystem.FetchAssetPurchaseDetails();
                    if (dsAssetPurchase != null && dsAssetPurchase.Tables.Count > 0)
                    {
                        gcPurchaseView.DataSource = dsAssetPurchase;
                        gcPurchaseView.DataMember = "Master";
                        gcPurchaseView.RefreshDataSource();
                    }
                    else
                    {
                        gcPurchaseView.DataSource = null;
                        gcPurchaseView.RefreshDataSource();
                    }
                    gvPurchaseView.FocusedRowHandle = 0;
                    gvPurchaseView.FocusRectStyle = DrawFocusRectStyle.RowFocus;
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
        }

        private void ShowEditAssetPurchase()
        {
            if (gvPurchaseView.RowCount > 0)
            {
                if (PurchaseId > 0)
                {
                    ShowPurchase(PurchaseId);
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

        private void DeleteAssetPurchase()
        {
            try
            {
                ResultArgs resultArgs = null;
                if (gvPurchaseView.RowCount > 0)
                {
                    if (PurchaseId > 0)
                    {
                        using (AssetPurchaseVoucherSystem purchaseSystem = new AssetPurchaseVoucherSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                resultArgs = purchaseSystem.DeleteAssetPurchase(PurchaseId);
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    FetchAssetPurchaseDetails();
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
    }
}
