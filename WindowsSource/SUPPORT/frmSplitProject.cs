using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Bosco.Model.Dsync;
using Bosco.Utility;
using Bosco.Model.TallyMigration;
using Bosco.Utility.Base;

namespace SUPPORT
{
    public partial class frmSplitProject : frmBase
    {
        #region Variables
        ResultArgs resultArgs = new ResultArgs();
        const string SELECT_COL = "SELECT";
        private const string DATASET_NAME = "Vouchers";
        #endregion

        #region Properties
        private string GetProjectId
        {
            get
            {
                return SeletedProjectId();
            }
        }

        private DataTable dtSelectedProject { set; get; }
        #endregion

        #region Constructor
        public frmSplitProject()
        {
            InitializeComponent();
        }
        #endregion

        #region Event
        private void frmSplitProject_Load(object sender, EventArgs e)
        {
            LoadDefaults();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvSplitProject.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvSplitProject, colProjectName);
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            (gcSplitProject.DataSource as DataTable).Select().ToList<DataRow>().ForEach(r => r[SELECT_COL] = chkSelectAll.Checked);
        }

        private void btnSplitProject_Click(object sender, EventArgs e)
        {
            if (IsValideIput())
            {
                if (chkRemoveProject.Checked)
                {
                    if (ShowConfirmationMessage("Selected Project will be removed permanently,Do you want still want to export the project?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question).Equals(DialogResult.OK))
                    {
                        SplitProject();
                    }
                }
                else
                {
                    SplitProject();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Loading the project details
        /// </summary>
        public void LoadDefaults()
        {
            LoadBranches();
            deDateFrom.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deDateTo.DateTime = deDateFrom.DateTime.AddMonths(1).AddDays(-1);
        }

        private void LoadBranches()
        {
            using (ExportVoucherSystem vouchersystem = new ExportVoucherSystem())
            {
                ResultArgs resultArgs = vouchersystem.FetchHOBranchList();
                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpBranch, resultArgs.DataSource.Table, "BRANCH_OFFICE_NAME", "BRANCH_OFFICE_ID");
                }
            }
        }

        private void LoadProjects()
        {
            if (glkpBranch.EditValue != null)
            {
                using (ExportVoucherSystem vouchersystem = new ExportVoucherSystem())
                {
                    vouchersystem.BranchId = this.UtilityMember.NumberSet.ToInteger(glkpBranch.EditValue.ToString());
                    ResultArgs resultArgs = vouchersystem.FetchHOBranchProjects();
                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        gcSplitProject.DataSource = AddSelectColumn(resultArgs.DataSource.Table);
                    }
                }
            }
        }

        /// <summary>
        /// Creating SELECT column and setting default values 0.
        /// For binding checkbox column
        /// </summary>
        /// <param name="dtProject">Collection of Project without SELECT Column</param>
        /// <returns>Collection of DataTable with SELECT Column with the default values 0</returns>
        private DataTable AddSelectColumn(DataTable dtProject)
        {
            if (dtProject != null)
            {
                if (!dtProject.Columns.Contains(SELECT_COL))
                {
                    dtProject.Columns.Add(SELECT_COL, typeof(Int32));
                    dtProject.Select().ToList<DataRow>().ForEach(r => r[SELECT_COL] = 0);
                }
            }
            return dtProject;
        }

        private bool IsValideIput()
        {
            bool IsValid = true;
            DataView dvSelectedProject = new DataView(gcSplitProject.DataSource as DataTable);
            dvSelectedProject.RowFilter = String.Format("{0}=1", SELECT_COL);
            dtSelectedProject = dvSelectedProject.ToTable();
            if (string.IsNullOrEmpty(glkpBranch.Text))
            {
                this.ShowMessageBoxWarning("Branch Office is not selected");
                IsValid = false;
                glkpBranch.Focus();
            }
            else if (dvSelectedProject.ToTable().Rows.Count == 0)
            {
                ShowMessageBox(this.GetMessage(MessageCatalog.DataSynchronization.Export.DSYNC_SELECT_ONE_PROJECT));
                IsValid = false;
            }
            else if (deDateTo.DateTime < deDateFrom.DateTime)
            {
                ShowMessageBox(this.GetMessage(MessageCatalog.DataSynchronization.Export.DSYNC_VALIDATE_DATE));
                IsValid = false;
            }
            else if (!CheckPrimaryLedgerGroup())
            {
                this.ShowMessageBoxWarning("Few Ledger Groups are created under Primary itself. Kindly move those Ledger Groups to any one of the four Natures and try again.");
                IsValid = false;
            }
            return IsValid;
        }

        private bool CheckPrimaryLedgerGroup()
        {
            bool isPrimary = true;

            using (ExportVoucherSystem exportSystem = new ExportVoucherSystem())
            {
                resultArgs = exportSystem.CheckPrimaryLedgerGroup();
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    isPrimary = false;
                }
            }
            return isPrimary;
        }

        private void SplitProject()
        {
            AcMELog.WriteLog("-----Split Project Started Started----");
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                DataSet dsTransaction = new DataSet(DATASET_NAME);
                using (ExportVoucherSystem vouchersystem = new ExportVoucherSystem())
                {
                    ShowWaitDialog();
                    //Modified by Carmel Raj M
                    vouchersystem.ProjectId = GetProjectId;
                    vouchersystem.BranchId= this.UtilityMember.NumberSet.ToInteger(glkpBranch.EditValue.ToString());
                    vouchersystem.DateFrom = UtilityMember.DateSet.ToDate(deDateFrom.DateTime.ToShortDateString(), false);
                    vouchersystem.DateTo = UtilityMember.DateSet.ToDate(deDateTo.DateTime.ToShortDateString(), false);
                    vouchersystem.VoucherImportType = ImportType.SplitProject;
                    resultArgs = vouchersystem.FillExportVoucherTransaction();
                    CloseWaitDialog();
                    if (resultArgs.Success)
                    {
                        dsTransaction = vouchersystem.dsTransaction;
                        if (dsTransaction.Tables.Count > 0)
                        {
                            resultArgs = ExportXML(dsTransaction);
                            if (resultArgs != null && resultArgs.Success)
                            {
                                if (chkRemoveProject.Checked)
                                {
                                    resultArgs = RemoveProject();
                                }

                                if (resultArgs.Success)
                                {
                                    ShowMessageBox("Project(s) exported Successfully");
                                }
                                else
                                {
                                    ShowMessageBox(resultArgs.Message);
                                }
                            }
                        }
                    }
                    else ShowMessageBox(resultArgs.Message);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Success = false;
                resultArgs.Message = ex.Message;
            }
        }

        private ResultArgs RemoveProject()
        {
            using (TallyMigrationSystem tallyMigrationSystem = new TallyMigrationSystem())
            {
                resultArgs = tallyMigrationSystem.RemoveProject(GetProjectId);
            }
            return resultArgs;
        }

        private string SeletedProjectId()
        {
            return GetCommaSeparatedValue(dtSelectedProject, "SELECT", "PROJECT_ID");
        }

        /// <summary>
        /// Convert  DataTable Row Value to Comma Separated String value
        /// </summary>
        /// <param name="dtValue">Project DataTable </param>
        /// <param name="FilterColumn">Name of  the column from which you want to make filter condition</param>
        /// <param name="OutputColumnName">Name of the column to be returned with Commo separed String Value</param>
        /// <returns>String Value with Commo Seperated Value</returns>
        private string GetCommaSeparatedValue(DataTable dtValue, string FilterColumn, string OutputColumnName)
        {
            string retValue = String.Empty;
            if (dtValue != null && dtValue.Rows.Count > 0)
            {
                var rowVal = dtValue.AsEnumerable();
                retValue = String.Join(",", (from r in rowVal
                                             where r.Field<Int32>(FilterColumn) == 1
                                             select r.Field<UInt32>(OutputColumnName)));
            }
            return retValue;
        }

        private ResultArgs ExportXML(DataSet dsTransaction)
        {
            resultArgs.Success = false;
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Xml Files|.xml";
                saveDialog.Title = "Split Voucher";
                saveDialog.FileName = "AcME_ERP_SplitProject_" + DateTime.Now.Ticks.ToString() + ".xml";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    XMLConverter.WriteToXMLFile(dsTransaction, saveDialog.FileName);
                    resultArgs.Success = true;
                    //  dsTransaction.WriteXml(saveDialog.FileName);
                }
                else
                {
                    resultArgs.Message = "Split Project has been cancelled";
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
                AcMELog.WriteLog(ex.Message);
            }
            return resultArgs;
        }
        #endregion

        private void glkpBranch_EditValueChanged(object sender, EventArgs e)
        {
            LoadProjects();
        }
    }
}