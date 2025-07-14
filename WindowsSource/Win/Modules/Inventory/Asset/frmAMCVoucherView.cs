using System;
using System.Data;
using System.Windows.Forms;
using Bosco.Model.UIModel;

using Bosco.Utility;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using Bosco.Model.Inventory.Asset;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmAMCVoucherView : frmFinanceBase
    {
        #region Constructor
        public frmAMCVoucherView()
        {
            InitializeComponent();
        }

        public frmAMCVoucherView(int projectId, string dateFrom)
            : this()
        {
            this.ProjectId = projectId;
            this.VoucherDate = dateFrom;
            RecentVoucherDate = dateFrom;
        }
        #endregion

        #region Variable Decelaration
        private int RowIndex = 0;
        #endregion

        #region Properties
        ResultArgs resultArgs = null;
        private string projectName = null;
        //private int projectId = 0;
        private int GroupId = 0;
        private string VoucherDate { get; set; }
        public int ProjectId { get; set; }
        private int Amcid = 0;
        public int AmcId
        {
            get
            {
                RowIndex = gvAMCView.FocusedRowHandle;
                Amcid = gvAMCView.GetFocusedRowCellValue(colAmcId) != null ? this.UtilityMember.NumberSet.ToInteger(gvAMCView.GetFocusedRowCellValue(colAmcId).ToString()) : 0;
                return Amcid;
            }
            set
            {
                Amcid = value;
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
        #endregion

        #region Events
        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvAMCView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvAMCView, colAmcDate);
            }
        }

        private void frmAMCVoucherView_Load(object sender, EventArgs e)
        {
            LoadProject();
            LoadProjectDate();
            //LoadAMCVoucherDetails();
            LoadAMCVoucherDetailsByProjectID();
            SetTittle();
        }

        private void frmAMCVoucherView_Activated(object sender, EventArgs e)
        {
            SetVisibileShortCuts(false, true);
        }

        private void ucToolBar1_AddClicked(object sender, EventArgs e)
        {
            ShowAmcVoucherForm((int)AddNewRow.NewRow);
        }

        private void ucToolBar1_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }


        protected virtual void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadAMCVoucherDetails();
        }

        private void ucToolBar1_DeleteClicked(object sender, EventArgs e)
        {
            DeletAmcVoucherDetails();
        }

        private void ucToolBar1_EditClicked(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void frmAMCVoucherView_EnterClicked(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void ucToolBar1_RefreshClicked(object sender, EventArgs e)
        {
            LoadAMCVoucherDetails();
        }

        private void glkpProjects_EditValueChanged(object sender, EventArgs e)
        {
            ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProjects.EditValue.ToString());
            projectName = glkpProjects.Text.ToString();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (dtFromDate.DateTime > dtToDate.DateTime)
            {
                DateTime dateTo = dtToDate.DateTime;
                dtToDate.DateTime = dtFromDate.DateTime;
                dtFromDate.DateTime = dateTo.Date;
            }
            LoadAMCVoucherDetailsByProjectID();
        }

        private void gvAMCView_RowCountChanged(object sender, EventArgs e)
        {
            lblCount.Text = gvAMCView.RowCount.ToString();
        }

        private void gcAMCView_DoubleClick(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void ucToolBar1_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcAMCView, this.GetMessage(MessageCatalog.Asset.AMCVoucher.AMCVOUCHER_PRINT_CAPTION), PrintType.DS, gvAMCView, true);
        }
        #endregion

        #region Methods
        private void LoadProject()
        {
            try
            {
                using (MappingSystem MappingSystem = new MappingSystem())
                {
                    resultArgs = MappingSystem.FetchProjectsLookup();
                    glkpProjects.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProjects, resultArgs.DataSource.Table, MappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, MappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
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

        /// <summary>
        /// Date 
        /// </summary>
        private void LoadProjectDate()
        {
            dtFromDate.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtToDate.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            //dtToDate.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            //dtToDate.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            //dtFromDate.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            //// deFromDate.DateTime = UtilityMember.DateSet.ToDate(RecentVoucherDate.ToString(), false);
            //DateTime DateFrom = new DateTime(dtFromDate.DateTime.Year, dtFromDate.DateTime.Month, 1);
            //dtFromDate.DateTime = DateFrom;
            //dtToDate.DateTime = dtFromDate.DateTime.AddMonths(1).AddDays(-1);
        }

        private void LoadAMCVoucherDetails()
        {
            try
            {
                DataSet dsAmcVoucher = new DataSet();
                using (AMCVoucherSystem AmcvoucherSystem = new AMCVoucherSystem())
                {
                    AmcvoucherSystem.DateFrom = dtFromDate.DateTime;
                    AmcvoucherSystem.DateTo = dtToDate.DateTime;
                    AmcvoucherSystem.ProjectId = ProjectId;
                    dsAmcVoucher = AmcvoucherSystem.FetchAssetAMCVoucherDetails();
                    if (dsAmcVoucher != null && dsAmcVoucher.Tables.Count > 0)
                    {
                        gcAMCView.DataSource = dsAmcVoucher;
                        gcAMCView.DataMember = "Master";
                        gcAMCView.RefreshDataSource();
                    }
                    else
                    {
                        gcAMCView.DataSource = null;
                        gcAMCView.RefreshDataSource();
                    }
                    gvAmcDetails.FocusedRowHandle = 0;
                    gvAmcDetails.FocusRectStyle = DrawFocusRectStyle.RowFocus;
                }
            }

            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally
            { }
        }

        private void DeletAmcVoucherDetails()
        {
            try
            {
                ResultArgs resultArgs = null;
                if (gvAMCView.RowCount != 0)
                {
                    if (AmcId != 0)
                    {
                        using (AMCVoucherSystem AmcVoucherSystem = new AMCVoucherSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                resultArgs = AmcVoucherSystem.DeleteAmcVoucherDetails(AmcId);
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    LoadAMCVoucherDetails();
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

        private void ShowAmcVoucherForm(int AmcId)
        {
            ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProjects.EditValue.ToString());
            projectName = glkpProjects.Text.ToString();
            frmAmcVoucherAdd objamcvoucher = new frmAmcVoucherAdd(ProjectId, projectName, AmcId);
            objamcvoucher.UpdateHeld += new EventHandler(OnUpdateHeld);
            objamcvoucher.Show();
        }

        private void ShowForm()
        {
            if (gvAMCView.RowCount > 0)
            {
                if (AmcId > 0)
                {
                    ShowAmcVoucherForm(AmcId);
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

        public void SetTittle()
        {
            this.Text = GetMessage(MessageCatalog.Asset.AMCVoucher.AMC_VIEW_CAPTION);
        }

        private void LoadAMCVoucherDetailsByProjectID()
        {
            try
            {
                DataSet dsAmcVoucher = new DataSet();
                using (AMCVoucherSystem AmcvoucherSystem = new AMCVoucherSystem())
                {
                    AmcvoucherSystem.ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProjects.EditValue.ToString());
                    AmcvoucherSystem.DateFrom = this.UtilityMember.DateSet.ToDate(dtFromDate.DateTime.ToString(), false);
                    AmcvoucherSystem.DateTo = this.UtilityMember.DateSet.ToDate(dtToDate.DateTime.ToString(), false);
                    dsAmcVoucher = AmcvoucherSystem.FetchAssetAMCVoucherDetails();
                    if (dsAmcVoucher != null && dsAmcVoucher.Tables.Count > 0)
                    {
                        gcAMCView.DataSource = dsAmcVoucher;
                        gcAMCView.DataMember = "Master";
                        gcAMCView.RefreshDataSource();
                    }
                    else
                    {
                        gcAMCView.DataSource = null;
                    }
                }
            }

            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally
            { }
        }
        #endregion

    }
}