using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Utility;
using Bosco.Model.Inventory.Asset;
using Bosco.Model.UIModel;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmInsuranceRenewalView : frmBase
    {
        #region Constructor
        public frmInsuranceRenewalView()
        {
            InitializeComponent();
        }

        public frmInsuranceRenewalView(int projectId, string voucherDate)
            : this()
        {
            this.ProjectId = projectId;
            this.VoucherDate = voucherDate;
        }
        #endregion

        #region Properties
        private int RowIndex = 0;
        private string ProjectName { get; set; }
        ResultArgs resultArgs = null;
        private int insuranceRenewalId = 0;
        public int InsuranceRenewalId
        {
            get
            {
                RowIndex = gvRenewal.FocusedRowHandle;
                insuranceRenewalId = gvRenewal.GetFocusedRowCellValue(colRenewalId) != null ? this.UtilityMember.NumberSet.ToInteger(gvRenewal.GetFocusedRowCellValue(colRenewalId).ToString()) : 0;
                return insuranceRenewalId;
            }
            set
            {
                insuranceRenewalId = value;
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

        private int itemId = 0;
        private int ItemId
        {
            get
            {
                RowIndex = gvRenewal.FocusedRowHandle;
                itemId = gvRenewal.GetFocusedRowCellValue(colInsId) != null ? this.UtilityMember.NumberSet.ToInteger(gvRenewal.GetFocusedRowCellValue(colInsId).ToString()) : 0;
                return itemId;
            }
            set
            {
                itemId = value;
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

        public string VoucherDate { get; set; }

        #endregion

        #region Events
        /// <summary>
        /// Load the Renewal form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmInsuranceRenewalView_Load(object sender, EventArgs e)
        {
            dtDateFrom.DateTime = this.UtilityMember.DateSet.ToDate(this.VoucherDate, false);
            dtDateTo.DateTime = this.UtilityMember.DateSet.ToDate(VoucherDate, false).AddMonths(1).AddDays(-1);
            LoadRenewalDetails();
            LoadProject();
            LoadDefaults();
        }

        private void LoadDefaults()
        {
            dtDateFrom.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtDateFrom.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            dtDateTo.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtDateTo.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            dtDateFrom.DateTime = (!string.IsNullOrEmpty(RecentVoucherDate)) ? UtilityMember.DateSet.ToDate(RecentVoucherDate, false) : UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtDateFrom.DateTime = UtilityMember.DateSet.ToDate(RecentVoucherDate.ToString(), false);
            dtDateTo.DateTime = dtDateFrom.DateTime.AddMonths(1).AddDays(-1);
        }
        /// <summary>
        /// Add the voucher form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar_AddClicked(object sender, EventArgs e)
        {
            ShowInsVoucherAdd((int)AddNewRow.NewRow);
        }

        /// <summary>
        /// Apply the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            if (dtDateFrom.DateTime > dtDateTo.DateTime)
            {
                DateTime dateTo = dtDateTo.DateTime;
                dtDateTo.DateTime = dtDateFrom.DateTime;
                dtDateFrom.DateTime = dateTo.Date;
            }
            LoadRenewalDetailsByProject();
        }

        /// <summary>
        /// Edit the voucher form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar_EditClicked(object sender, EventArgs e)
        {
            ShowEditInsVoucher();
        }

        /// <summary>
        /// Delete the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar_DeleteClicked(object sender, EventArgs e)
        {
            DeleteRenewalDetails();
        }

        /// <summary>
        /// Double click the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvInsrenewal_DoubleClick(object sender, EventArgs e)
        {
            ShowEditInsVoucher();
        }

        /// <summary>
        /// Double click the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvRenewal_DoubleClick(object sender, EventArgs e)
        {
            ShowEditInsVoucher();
        }

        /// <summary>
        /// Close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// row count the data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvRenewal_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvRenewal.RowCount.ToString();
        }

        /// <summary>
        /// Refresh the data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadRenewalDetails();
        }

        /// <summary>
        /// Print the details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar_PrintClicked(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// check checked the data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvRenewal.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvRenewal, colVouNo);
            }
        }

        /// <summary>
        /// Refresh the Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar_RefreshClicked(object sender, EventArgs e)
        {
            LoadRenewalDetails();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Load the Details
        /// </summary>
        /// <param name="InsuranceRenewalId"></param>
        private void ShowInsVoucherAdd(int InsuranceRenewalId)
        {
            try
            {
                //ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
                //ProjectName = glkpProject.Text.ToString();
                //frmInsuranceRenewal insuranceRenewal = new frmInsuranceRenewal(InsuranceRenewalId, ProjectId, ProjectName);
                //insuranceRenewal.UpdateHeld += new EventHandler(OnUpdateHeld);
                //insuranceRenewal.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            { }
        }

        /// <summary>
        /// Show the Edit the Voucher
        /// </summary>
        private void ShowEditInsVoucher()
        {
            if (gvRenewal.RowCount != 0)
            {
                if (InsuranceRenewalId != 0)
                {

                    ShowInsVoucherAdd(InsuranceRenewalId);
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

        /// <summary>
        /// Load the renewal details
        /// </summary>
        private void LoadRenewalDetails()
        {
            try
            {
                DataSet dsVoucher = new DataSet();
                using (InsuranceRenewalSystem InsrenSystem = new InsuranceRenewalSystem())
                {
                    //     dsVoucher = InsrenSystem.FetchInsurenceRenewalDetails();
                    if (dsVoucher != null && dsVoucher.Tables.Count > 0)
                    {
                        gcRenewal.DataSource = dsVoucher;
                        gcRenewal.DataMember = "Master";
                        gcRenewal.RefreshDataSource();
                    }
                    else
                    {
                        gcRenewal.DataSource = null;
                        gcRenewal.RefreshDataSource();
                    }
                    gvRenewal.FocusedRowHandle = 0;
                    gvRenewal.FocusRectStyle = DrawFocusRectStyle.RowFocus;
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        /// <summary>
        /// Load renewal by selecting Master;
        /// </summary>
        private void LoadRenewalDetailsByProject()
        {
            try
            {
                DataSet dsRenewal = new DataSet();
                using (InsuranceRenewalSystem InsRenewalSystem = new InsuranceRenewalSystem())
                {
                    InsRenewalSystem.ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
                    InsRenewalSystem.DateFrom = this.UtilityMember.DateSet.ToDate(dtDateFrom.DateTime.ToString(), false);
                    InsRenewalSystem.DateTo = this.UtilityMember.DateSet.ToDate(dtDateTo.DateTime.ToString(), false);
                    //   dsRenewal = InsRenewalSystem.FetchDetailsByProject();
                    if (dsRenewal != null && dsRenewal.Tables.Count > 0)
                    {
                        gcRenewal.DataSource = dsRenewal;
                        gcRenewal.DataMember = "Master";
                        gcRenewal.RefreshDataSource();
                    }
                    else
                    {
                        gcRenewal.DataSource = null;
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
        /// <summary>
        /// Load the details
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
                        glkpProject.EditValue = (ProjectId != 0) ? ProjectId : glkpProject.Properties.GetKeyValue(0);
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
        /// Delete the details
        /// </summary>
        private void DeleteRenewalDetails()
        {
            try
            {
                ResultArgs resultArgs = null;
                if (gvRenewal.RowCount != 0)
                {
                    if (InsuranceRenewalId != 0)
                    {
                        using (InsuranceRenewalSystem renewalSystem = new InsuranceRenewalSystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                resultArgs = renewalSystem.DeleteRenewal(InsuranceRenewalId);
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    LoadRenewalDetails();
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


    }
        #endregion
}