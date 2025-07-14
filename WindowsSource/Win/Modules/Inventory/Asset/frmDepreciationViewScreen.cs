using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using ACPP.Modules.Transaction;
using Bosco.Model.Transaction;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraGrid.Views.Base;
using AcMEDSync.Model;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using ACPP.Modules.Master;
using Bosco.Utility;
using Bosco.Model.UIModel;
using Bosco.Model;
using Bosco.DAO.Schema;
using Bosco.Model.Inventory.Asset;
using Bosco.Utility.CommonMemberSet;
using Bosco.Utility.ConfigSetting;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmDepreciationViewScreen : frmFinanceBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = new ResultArgs();
        private int RowIndex;
        #endregion

        #region Property
        private int projectId { get; set; }
        private DataSet dsDepreciation { get; set; }
        private DataTable dtDepMaster { get; set; }
        private DataTable dtDepDetail { get; set; }

        private int depreciationID;
        private int DepreciationId
        {
            get
            {
                RowIndex = gvDepreciation.FocusedRowHandle;
                depreciationID = gvDepreciation.GetFocusedRowCellValue(colDepreciationId) != null ?
                    this.UtilityMember.NumberSet.ToInteger(gvDepreciation.GetFocusedRowCellValue(colDepreciationId).ToString()) : 0;
                return depreciationID;
            }
            set
            {
                depreciationID = value;
            }
        }

        private int voucherid;
        private int VoucherID
        {
            get
            {
                RowIndex = gvDepreciation.FocusedRowHandle;
                voucherid = gvDepreciation.GetFocusedRowCellValue(colDepreciationId) != null ?
                    this.UtilityMember.NumberSet.ToInteger(gvDepreciation.GetFocusedRowCellValue(colVoucherID).ToString()) : 0;
                return voucherid;
            }
            set
            {
                voucherid = value;
            }
        }

        private string depreciationperiodfrom;
        private string DepreciationPeriodFrom
        {
            get
            {
                RowIndex = gvDepreciation.FocusedRowHandle;
                depreciationperiodfrom = gvDepreciation.GetFocusedRowCellValue(colDeprecationFrom) != null ?
                    this.UtilityMember.DateSet.ToDate(gvDepreciation.GetFocusedRowCellValue(colDeprecationFrom).ToString(), false).ToShortDateString() : string.Empty;
                return depreciationperiodfrom;
            }
            set
            {
                depreciationperiodfrom = value;
            }
        }

        private string depreciationperiodto;
        private string DepreciationPeriodTo
        {
            get
            {
                RowIndex = gvDepreciation.FocusedRowHandle;
                depreciationperiodto = gvDepreciation.GetFocusedRowCellValue(colDepreciationTo) != null ?
                    this.UtilityMember.DateSet.ToDate(gvDepreciation.GetFocusedRowCellValue(colDepreciationTo).ToString(), false).ToShortDateString() : string.Empty;
                return depreciationperiodto;
            }
            set
            {
                depreciationperiodto = value;
            }
        }

        #endregion

        #region Constructor

        public frmDepreciationViewScreen()
        {
            InitializeComponent();
        }

        public frmDepreciationViewScreen(int value, int recentProjectId, string recentProject)
            : this()
        {
            projectId = recentProjectId;
        }
        #endregion

        #region Events
        private void DepreciationView_Load(object sender, EventArgs e)
        {
            SetDate();
            LoadProject();
            LoadDepreciationMasterDetails();
            ApplyUserRights();
        }

        private void SetDate()
        {
            //dePeriodFrom.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            //dePeriodFrom.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            //dePeriodTo.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            //dePeriodTo.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            dePeriodFrom.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dePeriodTo.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDepreciationMasterDetails();
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void ucToolBarDepreciation_AddClicked(object sender, EventArgs e)
        {
            ShowForm((int)AddNewRow.NewRow);
        }
        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        private void ApplyUserRights()
        {
            this.enumUserRights.Add(Depreciation.CreateDepreciation);
            this.enumUserRights.Add(Depreciation.EditDepreciation);
            this.enumUserRights.Add(Depreciation.DeleteDepreciation);
            this.enumUserRights.Add(Depreciation.ViewDepreciation);
            this.ApplyUserRights(ucToolBarDepreciation, enumUserRights, (int)Menus.Depreciation);
        }

        /// <summary>
        /// This is to Load the Projects 
        /// </summary>
        private void LoadProject()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {

                    resultArgs = mappingSystem.FetchProjectsLookup();
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProject, resultArgs.DataSource.Table, mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                        glkpProject.EditValue = (projectId != 0) ? projectId : glkpProject.Properties.GetKeyValue(0);
                    }
                }
            }
            catch (Exception Ex)
            {
                this.ShowMessageBox(Ex.Message + Environment.NewLine + Ex.Source);
            }
            finally { }
        }
        private void LoadDepreciationMasterDetails()
        {
            try
            {
                using (AssetDepreciationSystem assetDepSys = new AssetDepreciationSystem())
                {
                    assetDepSys.ProjectId = projectId;
                    assetDepSys.DepFrom = dePeriodFrom.DateTime;
                    assetDepSys.DepTo = dePeriodTo.DateTime;
                    dsDepreciation = assetDepSys.LoadDepreciationDetails();
                    if (dsDepreciation.Tables.Count > (int)YesNo.Yes)
                    {
                        dtDepMaster = dsDepreciation.Tables[0];
                        dtDepDetail = dsDepreciation.Tables[1];
                    }
                    if (dsDepreciation.Tables.Count != (int)YesNo.No)
                    {
                        gcDepreciation.DataSource = dsDepreciation;
                        gcDepreciation.DataMember = FDRenewalCaption.Master.ToString();
                        gcDepreciation.RefreshDataSource();
                    }
                    if (dsDepreciation.Tables.Count == 0)
                    {
                        gcDepreciation.DataSource = null;
                        gcDepreciation.RefreshDataSource();
                    }
                }
            }
            catch (Exception Ex)
            {
                this.ShowMessageBox(Ex.Message + Environment.NewLine + Ex.Source);
            }
            finally { }
        }
        private void ShowForm(int DepreciationId)
        {
            try
            {
                if (IsValidDepreication(DepreciationId))
                {
                    frmDepreciation frmDepreciationAdd = new frmDepreciation(DepreciationId, projectId, glkpProject.Text.Trim());
                    frmDepreciationAdd.DepPeriodFrom = DepreciationPeriodFrom;
                    frmDepreciationAdd.UpdateHeld += new EventHandler(OnUpdateHeld);
                    frmDepreciationAdd.ShowDialog();
                }
                else
                {
                    this.ShowMessageBox("Depreciation Applied for the Current Transaction Period.");
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        public bool IsValidDepreication(int DeprID)
        {
            bool istrue = true;

            if (DeprID.Equals(0))
            {
                resultArgs = FetchMaxPeriod();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    if (resultArgs.DataSource.Table.Rows[0]["PERIOD_TO"].ToString() != DBNull.Value.ToString())
                    {
                        if (UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["PERIOD_TO"].ToString(), false)
                            .Equals(UtilityMember.DateSet.ToDate(AppSetting.YearTo, false)))
                        {
                            istrue = false;
                        }
                    }
                }
            }
            return istrue;

        }
        private ResultArgs FetchMaxPeriod()
        {
            try
            {
                using (AssetDepreciation assetdepreciation = new AssetDepreciation())
                {
                    assetdepreciation.ProjectId = projectId;
                    resultArgs = assetdepreciation.FetchMaxRenewal();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            return resultArgs;
        }

        private void ShowEditDepreciationForm()
        {
            if (gvDepreciation.RowCount != 0)
            {
                if (DepreciationId != 0)
                {
                    ShowForm(DepreciationId);
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
        private void DeleteDepreciationDetails()
        {
            try
            {
                if (gvDepreciation.RowCount != 0)
                {
                    if (DepreciationId != 0)
                    {
                        using (AssetDepreciationSystem assetDepSys = new AssetDepreciationSystem())
                        {
                            // DepreciationId = (gcDepreciation.FocusedView as GridView).GetFocusedRowCellValue(colDepreciationId) != null ? this.UtilityMember.NumberSet.ToInteger((gcDepreciation.FocusedView as GridView).GetFocusedRowCellValue(colDepreciationId).ToString()) : 0;
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                assetDepSys.VoucherID = VoucherID;
                                assetDepSys.DepreciationId = DepreciationId;
                                resultArgs = assetDepSys.DeleteDepreciationDetails();
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    LoadDepreciationMasterDetails();
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
        #endregion

        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            projectId = (glkpProject.EditValue != null) ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
        }

        private void ucToolBarDepreciation_EditClicked(object sender, EventArgs e)
        {
            try
            {
                ShowEditDepreciationForm();
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void ucToolBarDepreciation_DeleteClicked(object sender, EventArgs e)
        {
            try
            {
                DeleteDepreciationDetails();
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void gvDepreciation_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            //DevExpress.XtraGrid.Views.Grid.GridView gridView = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            //DepreciationId = gridView.GetFocusedRowCellValue(colDepreciationId) != null ? this.UtilityMember.NumberSet.ToInteger(gridView.GetFocusedRowCellValue(colDepreciationId).ToString()) : 0;
        }

        private void gvDetailDepreciation_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView gridView = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            DepreciationId = gridView.GetFocusedRowCellValue(colDetailDepreciationId) != null ?
                this.UtilityMember.NumberSet.ToInteger(gridView.GetFocusedRowCellValue(colDetailDepreciationId).ToString()) : 0;
        }

        private void frmDepreciationViewScreen_EnterClicked(object sender, EventArgs e)
        {
            ShowEditDepreciationForm();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvDepreciation.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            gvDetailDepreciation.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvDepreciation, colDepreciationAppliedOn);
            }
        }
        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadDepreciationMasterDetails();
            gvDepreciation.FocusedRowHandle = RowIndex;
        }

        private void ucToolBarDepreciation_RefreshClicked(object sender, EventArgs e)
        {
            try
            {
                LoadDepreciationMasterDetails();
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void ucToolBarDepreciation_PrintClicked(object sender, EventArgs e)
        {
            // PrintGridViewDetails(gcDepreciation, this.GetMessage(MessageCatalog.Asset.Depreciation.DEPRECIATION_VIEW_SCREEN), PrintType.DS, gvDepreciation, true);
            if (gvDepreciation.RowCount != 0)
            {
                Bosco.Report.Base.IReport report = new Bosco.Report.Base.ReportEntry(this.MdiParent);
                SettingProperty.ReportModuleId = (int)ReportModule.FixedAsset;

                int DepreId = DepreciationId;
                DateTime dtFrom = Convert.ToDateTime(DepreciationPeriodFrom);
                DateTime dtTo = Convert.ToDateTime(DepreciationPeriodTo);
                int ProId = projectId;

                MessageRender.ShowMessage("Depreciation details will be Printed / Exported");
                // report.ShowBudgetView1(BudgetId, BudgetName, gvBudget);
                report.ShowDepreciationCalculation(DepreId, dtFrom, dtTo, ProId, this);

            }
        }

        private void ucToolBarDepreciation_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvDepreciation_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                ShowEditDepreciationForm();
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void gcDepreciation_Enter(object sender, EventArgs e)
        {
            //try
            //{
            //    ShowEditDepreciationForm();
            //}
            //catch (Exception ex)
            //{
            //    this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            //}
            //finally { }
        }
    }
}