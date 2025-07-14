/*  Class Name      : frmDepreciationVoucherView.cs
 *  Purpose         : To Show Asset Depreciation
 *  Author          : CD
 *  Created on      : 15-May-2015
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Bosco.Utility;
using Bosco.Model;
using Bosco.Model.UIModel;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System.Data;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmDepriciationVoucherView : frmFinanceBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = new ResultArgs();
        string ProjectName = string.Empty;
        private int RowIndex = 0;
        #endregion

        #region Property
        private int Depriciationid = 0;
        public int DepriciationId
        {
            get
            {
                RowIndex = gvDepriciationView.FocusedRowHandle;
                Depriciationid = gvDepriciationView.GetFocusedRowCellValue(colDepriciaitonId) != null ? this.UtilityMember.NumberSet.ToInteger(gvDepriciationView.GetFocusedRowCellValue(colDepriciaitonId).ToString()) : 0;
                return Depriciationid;
            }
            set
            {
                Depriciationid = value;
            }

        }

        //private int projectId = 0;
        public int ProjectId { get; set; }

        //private string recentVoucherDate = string.Empty;
        private string RecentVoucherDate { get; set; }
        //{
        //    set
        //    {
        //        recentVoucherDate = value;
        //    }
        //    get
        //    {
        //        return recentVoucherDate;
        //    }
        //}

        #endregion

        #region Constructor
        public frmDepriciationVoucherView()
        {
            InitializeComponent();
        }

        public frmDepriciationVoucherView(int projectId, string dateFrom)
            : this()
        {
            this.ProjectId = projectId;
            this.RecentVoucherDate = dateFrom;
        }
        #endregion

        #region Events
        /// <summary>
        /// This is to load the Records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDepriciationVoucherView_Load(object sender, EventArgs e)
        {
            //ValidateDateControls();
            LoadProject();
            deFrom.DateTime = this.UtilityMember.DateSet.ToDate(RecentVoucherDate, false);
            deTo.DateTime = this.UtilityMember.DateSet.ToDate(RecentVoucherDate, false).AddMonths(1).AddDays(-1);
            FetchDepreciationDetails();
            ApplyUserRights();

        }

        /// <summary>
        /// This is to add the Records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar1_AddClicked(object sender, EventArgs e)
        {
            ShowDepriciation((int)AddNewRow.NewRow);
        }

        /// <summary>
        /// This is to Edit the records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar1_EditClicked(object sender, EventArgs e)
        {
            ShowEditDepreciation();
        }

        /// <summary>
        /// This is to delete the records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar1_DeleteClicked(object sender, EventArgs e)
        {
            DeleteDepreciation();
        }

        /// <summary>
        /// Edit the Records while double Clicking
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvDetails_DoubleClick(object sender, EventArgs e)
        {
            ShowEditDepreciation();
        }

        /// <summary>
        /// This is to print the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar1_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcDepriciationVoucher, this.GetMessage(MessageCatalog.Asset.DepreciationVoucher.DEPRECIATIONVOUCHER_PRINT_CAPTION), PrintType.DS, gvDepriciationView, true);
        }

        /// <summary>
        /// This is to get the Project Name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            ProjectName = glkpProject.SelectedText.ToString();
            ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
        }

        /// <summary>
        /// This is to refresh the records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar1_RefreshClicked(object sender, EventArgs e)
        {
            FetchDepreciationDetails();
        }

        /// <summary>
        /// This is to close the records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar1_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// This is to apply the records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            FetchDepreciationDetails();
        }

        /// <summary>
        /// This is to filter the Records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvDepriciationView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvDepriciationView, colAssetName);
            }
        }

        /// <summary>
        /// This is to Edit the forms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvDepriciationView_DoubleClick(object sender, EventArgs e)
        {
            ShowEditDepreciation();
        }

        /// <summary>
        /// This is to get the Count
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvDepriciationView_RowCountChanged(object sender, EventArgs e)
        {
            lblCount.Text = gvDepriciationView.RowCount.ToString();
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
            this.ApplyUserRights(ucToolBar1, enumUserRights, (int)Menus.Depreciation);
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

        /// <summary>
        /// This is to show the forms
        /// </summary>
        /// <param name="Depriciation"></param>
        private void ShowDepriciation(int Depriciation)
        {
            try
            {
                ProjectName = glkpProject.Text.ToString();
                ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
                frmDepreciationVoucherAdd frmDepricationAdd = new frmDepreciationVoucherAdd(ProjectId, ProjectName, Depriciation);
                frmDepricationAdd.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmDepricationAdd.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            { }
        }

        /// <summary>
        /// This is to refresh the Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchDepreciationDetails();
            gvDepriciationView.FocusedRowHandle = RowIndex;
        }

        /// <summary>
        /// This is to show the Edit forms
        /// </summary>
        private void ShowEditDepreciation()
        {
            if (gvDepriciationView.RowCount > 0)
            {
                if (DepriciationId > 0)
                {
                    ShowDepriciation(DepriciationId);
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

        /// <summary>
        /// This is to Fetch the Depreciation Details
        /// </summary>
        public void FetchDepreciationDetails()
        {
            DataSet dsDepreciation = new DataSet();
            using (DepriciationVoucherSystem DepVoucherSystem = new DepriciationVoucherSystem())
            {
                DepVoucherSystem.FromDate = deFrom.DateTime;
                DepVoucherSystem.ToDate = deTo.DateTime;
                DepVoucherSystem.ProjectId = ProjectId;
                dsDepreciation = DepVoucherSystem.FetchAssetDepreciationDetails();
                if (dsDepreciation != null && dsDepreciation.Tables.Count > 0)
                {
                    gcDepriciationVoucher.DataSource = dsDepreciation;
                    gcDepriciationVoucher.DataMember = "Master";
                    gcDepriciationVoucher.RefreshDataSource();
                }
                else
                {
                    gcDepriciationVoucher.DataSource = null;
                    gcDepriciationVoucher.RefreshDataSource();
                }
                gvDepriciationView.FocusedRowHandle = 0;
                gvDepriciationView.FocusRectStyle = DrawFocusRectStyle.RowFocus;
            }
        }

        /// <summary>
        /// This is to delete the Depreciation Details
        /// </summary>
        private void DeleteDepreciation()
        {
            try
            {
                ResultArgs resultArgs = null;
                if (gvDepriciationView.RowCount > 0)
                {
                    if (DepriciationId > 0)
                    {
                        using (DepriciationVoucherSystem Depreciation = new DepriciationVoucherSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                resultArgs = Depreciation.DeleteDepreciation(DepriciationId);
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    FetchDepreciationDetails();
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

        /// <summary>
        /// This is validating the Date controls
        /// </summary>
        private void ValidateDateControls()
        {
            deFrom.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deFrom.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            deTo.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deTo.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            deFrom.DateTime = (!string.IsNullOrEmpty(RecentVoucherDate)) ? UtilityMember.DateSet.ToDate(RecentVoucherDate, false) : UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deFrom.DateTime = UtilityMember.DateSet.ToDate(RecentVoucherDate.ToString(), false);
            deTo.DateTime = deFrom.DateTime.AddMonths(1).AddDays(-1);
        }
        #endregion
    }
}
