using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Utility.ConfigSetting;
using Bosco.DAO.Schema;
using Bosco.DAO.Data;
using Bosco.Report.Base;
using Bosco.Utility;

namespace Bosco.Report.View
{
    public partial class frmTDSChallan : Bosco.Utility.Base.frmBase
    {
        #region Decelaration
        SettingProperty setttings = new SettingProperty();
        private DataTable dtVouchers { get; set; }
        private DataTable dtProjectDetails { get; set; }
        private DataTable dtTDSChallanFilter { get; set; }
        private AppSchemaSet.ApplicationSchemaSet appSchema = new AppSchemaSet.ApplicationSchemaSet();
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public frmTDSChallan()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void frmTDSChallan_Load(object sender, EventArgs e)
        {
            setDefaults();
            FetchProjects();
            LoadByProject();
        }

        private void btnFetchTDS_Click(object sender, EventArgs e)
        {
            FetchTDSChallanReconciliation();
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            getSelectedRows();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            ReportProperty.Current.CashBankProjectId = glkpProject.EditValue != null ? ReportProperty.Current.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
        }
        #endregion

        #region Methods

        private void LoadByProject()
        {
            try
            {
                if (ReportProperty.Current.CashBankProjectId != 0 && ReportProperty.Current.DateAsOn != string.Empty && ReportProperty.Current.dtTDSChallan != null && ReportProperty.Current.dtTDSChallan.Rows.Count > 0)
                {
                    FetchTDSChallanReconciliation();
                    glkpProject.EditValue = ReportProperty.Current.CashBankProjectId;
                    dteDateAson.DateTime = ReportProperty.Current.DateSet.ToDate(ReportProperty.Current.DateAsOn, false);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString() + Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void FetchProjects()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchProjectforLookup))
                {
                    if (!this.LoginUser.IsFullRightsReservedUser)
                    {
                        dataManager.Parameters.Add(this.appSchema.UserRole.USERROLE_IDColumn, setttings.LoginUserId);
                    }
                    ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                    {
                        if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                        {
                            dtProjectDetails = resultArgs.DataSource.Table;
                            ReportProperty.Current.ComboSet.BindGridLookUpCombo(glkpProject, resultArgs.DataSource.Table, "PROJECT", "PROJECT_ID");
                            glkpProject.EditValue = ReportProperty.Current.NumberSet.ToInteger(setttings.UserProjectId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void setDefaults()
        {
            if (!string.IsNullOrEmpty(ReportProperty.Current.DateAsOn) && !string.IsNullOrEmpty(ReportProperty.Current.DateTo))
            {
                dteDateAson.DateTime = ReportProperty.Current.DateSet.ToDate(ReportProperty.Current.DateAsOn, false);
                dteDateTo.DateTime = ReportProperty.Current.DateSet.ToDate(ReportProperty.Current.DateTo, false);
            }
            else
            {
                dteDateAson.DateTime = ReportProperty.Current.DateSet.ToDate(setttings.YearFrom, false);
                dteDateTo.DateTime = ReportProperty.Current.DateSet.ToDate(setttings.YearFrom, false).AddMonths(1).AddDays(-1);
            }
        }

        public void FetchTDSChallanReconciliation()
        {
            int ProjectId = 0;
            string DateFrom = string.Empty;
            string DateTo = string.Empty;

            try
            {
                ProjectId = ReportProperty.Current.NumberSet.ToInteger(glkpProject.EditValue != null ? glkpProject.EditValue.ToString() : string.Empty);
                DateFrom = dteDateAson.DateTime.ToShortDateString();
                DateTo = dteDateTo.DateTime.ToShortDateString();
                using (DataManager dataManager = new DataManager(SQLCommand.TDSPayment.TDSChallanReport))
                {
                    dataManager.Parameters.Add(this.appSchema.LedgerBalance.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.appSchema.Project.DATE_STARTEDColumn, ReportProperty.Current.DateSet.ToDate(DateFrom, false));
                    dataManager.Parameters.Add(this.appSchema.Project.DATE_CLOSEDColumn, ReportProperty.Current.DateSet.ToDate(DateTo, false));
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
                    if (resultArgs != null && resultArgs.Success)
                    {
                        dtTDSChallanFilter = resultArgs.DataSource.Table;
                        gcTDSChallan.DataSource = resultArgs.DataSource.Table;
                        gcTDSChallan.RefreshDataSource();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString() + Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void getSelectedRows()
        {
            string VoucherId = string.Empty;
            try
            {
                if ((gcTDSChallan.DataSource as DataTable) != null && (gcTDSChallan.DataSource as DataTable).Rows.Count > 0)
                {
                    foreach (int i in gvTDSChallan.GetSelectedRows())
                    {
                        DataRow row = gvTDSChallan.GetDataRow(i);
                        VoucherId += row["VOUCHER_ID"].ToString() + ",";
                    }
                    VoucherId = VoucherId.TrimEnd(',');

                    if (!string.IsNullOrEmpty(VoucherId))
                    {
                        ReportProperty.Current.TDSVoucherId = VoucherId;
                        ReportProperty.Current.DateAsOn = dteDateAson.DateTime.ToShortDateString();
                        ReportProperty.Current.DateTo = dteDateTo.DateTime.ToShortDateString();
                        ReportProperty.Current.Project = glkpProject.EditValue != null ? glkpProject.EditValue.ToString() : "0";
                        ReportProperty.Current.ProjectTitle = glkpProject.Text;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        XtraMessageBox.Show("Select TDS Payment to print TDS Challan.", "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString() + Environment.NewLine + ex.Source, true);
            }
            finally { }
        }
        #endregion

    }
}