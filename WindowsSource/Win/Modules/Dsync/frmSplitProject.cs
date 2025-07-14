using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Bosco.Model.Dsync;
using Bosco.Utility;
using Bosco.Model.TallyMigration;
using DevExpress.XtraEditors;
using AcMEDSync.Model;
using System.IO;
using System.Text.RegularExpressions;
using Bosco.Model.UIModel;

namespace ACPP.Modules.Dsync
{
    public partial class frmSplitProject : frmFinanceBaseAdd
    {
        #region Variables
        
        const string SELECT_COL = "SELECT";
        private const string DATASET_NAME = "Vouchers";
        private bool isBulkImport = false;
        private string FYSplitFolderPath = string.Empty;
        string alreadyselectedprojectis = string.Empty;
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
        public frmSplitProject(bool isbulkImport)
        {
            InitializeComponent();
            isBulkImport = isbulkImport;
           
        }
        #endregion

        #region Event
        private void frmSplitProject_Load(object sender, EventArgs e)
        {
            LoadDefaults();

            //25/03/2021, For Temp
            //chkSplitFY.Checked = false;
            //lcSplitFY.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
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
            ResultArgs resultArgs = new ResultArgs();

            if (IsValideIput())
            {

                /*if (chkRemoveProject.Checked)
                {
                    //if (ShowConfirmationMessage("Selected Project will be removed permanently,Do you want still want to export the project?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question).Equals(DialogResult.OK))
                    if (ShowConfirmationMessage(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.REMOVE_PROJECT_SELECT_PROJECT_CONFIRMATION), MessageBoxButtons.OKCancel, MessageBoxIcon.Question).Equals(DialogResult.OK))
                    {
                        SplitProject();
                    }
                }
                else
                {
                    SplitProject();
                }*/

                if (isBulkImport)
                {
                    AssignFYSplitFolderPath();
                    if (!string.IsNullOrEmpty(FYSplitFolderPath))
                    {
                        bool rtn = false;
                        string[] files = Directory.GetFiles(FYSplitFolderPath);
                        if (files.Length > 0)
                        {
                            if (this.ShowConfirmationMessage("Few file(s) are already available in the selected path, Are you sure to proceed ? ",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                            {
                                rtn = true;
                            }
                        }
                        else
                        {
                            rtn = true;
                        }

                        if (rtn)
                        {
                            string[] selectedprojectIds = GetProjectId.Split(',');
                            foreach (string pid in selectedprojectIds)
                            {
                                resultArgs = SplitProject(pid);
                                if (!resultArgs.Success)
                                {
                                    break;
                                }
                            }

                            if (resultArgs.Success)
                            {
                                ShowMessageBox(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.PROJECT_EXPORT_SUCCESS));
                            }
                        }
                    }
                }
                else
                {
                    SplitProject(GetProjectId);
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
            //  this.ShowWaitDialog();

            deDateFrom.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deDateTo.DateTime = deDateFrom.DateTime.AddMonths(1).AddDays(-1);

            /*using (ExportVoucherSystem vouchersystem = new ExportVoucherSystem())
            {
                ResultArgs resultArgs = vouchersystem.FetchProjectsLookup();
                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    DateTime dtFrom = UtilityMember.DateSet.ToDate(deDateFrom.DateTime.ToShortDateString(), false);
                    DateTime dtTo = UtilityMember.DateSet.ToDate(deDateTo.DateTime.ToShortDateString(), false);
                    DataTable dtProjects = resultArgs.DataSource.Table;

                    //30/08/2019, to skip closing projects
                    //----------------------------------------------------------------------------------------------------------------------------------------
                    string filter = vouchersystem.AppSchema.Project.DATE_CLOSEDColumn.ColumnName + " IS NULL OR " +
                                    "(" + vouchersystem.AppSchema.Project.DATE_CLOSEDColumn.ColumnName + " >='" + dtFrom + "' )"; //AND DATE_CLOSED >='" + dtTo + "'
                    dtProjects.DefaultView.RowFilter = filter;
                    dtProjects = dtProjects.DefaultView.ToTable();
                    //------------------------------------------------------------------------------------------------------------------------------------------

                    gcSplitProject.DataSource = AddSelectColumn(dtProjects);
                }
            }*/
            LoadProjects();

            lcOnlyMasterAlone.Visibility = lcOnlyCashBankFDLedgers.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            chkOnlyMasterAlone.Checked = chkOnlyCashBankFDLedgers.Checked = false;
            chkOnlyCashBankFDLedgers.Enabled = false;

            if (isBulkImport)
            {
                this.Text = this.Text + " - Split Finance Year";
                repCheckProject.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Standard;
                chkSplitFY.Checked = true;
                chkSplitFY.Enabled = false;
                this.deDateFrom.Enabled = false;
                this.deDateTo.DateTime = UtilityMember.DateSet.ToDate(AppSetting.YearTo, false);
                lcOnlyMasterAlone.Visibility = lcOnlyCashBankFDLedgers.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }

            // this.CloseWaitDialog();
        }

        /// <summary>
        /// Loading the project details
        /// </summary>
        public void LoadProjects()
        {
            alreadyselectedprojectis = string.Empty;
            using (MappingSystem vouchersystem = new MappingSystem())
            {
                vouchersystem.ProjectClosedDate = deDateFrom.DateTime.ToShortDateString();
                ResultArgs resultArgs = vouchersystem.FetchProjectsLookup(deDateTo.DateTime.ToShortDateString());
                if (resultArgs.Success && resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    DateTime dtFrom = UtilityMember.DateSet.ToDate(deDateFrom.DateTime.ToShortDateString(), false);
                    DateTime dtTo = UtilityMember.DateSet.ToDate(deDateTo.DateTime.ToShortDateString(), false);
                    DataTable dtProjects = resultArgs.DataSource.Table;
                    
                    if (gcSplitProject.DataSource!=null)
                    {
                        DataTable dtselprojects = gcSplitProject.DataSource as DataTable;
                        alreadyselectedprojectis = GetCommaSeparatedValue(dtselprojects, "SELECT", "PROJECT_ID");
                    }

                    //30/08/2019, to skip closing projects
                    //----------------------------------------------------------------------------------------------------------------------------------------
                    string filter = vouchersystem.AppSchema.Project.DATE_CLOSEDColumn.ColumnName + " IS NULL OR " +
                                    "(" + vouchersystem.AppSchema.Project.DATE_CLOSEDColumn.ColumnName + " >='" + dtFrom + "' )"; //AND DATE_CLOSED >='" + dtTo + "'
                    dtProjects.DefaultView.RowFilter = filter;
                    dtProjects = dtProjects.DefaultView.ToTable();
                    //------------------------------------------------------------------------------------------------------------------------------------------

                    gcSplitProject.DataSource = AddSelectColumn(dtProjects);

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
                    if (isBulkImport)
                    {
                        dtProject.Select().ToList<DataRow>().ForEach(r => r[SELECT_COL] = 1);
                    }
                    else
                    {
                        //dtProject.Select().ToList<DataRow>().ForEach(r => r[SELECT_COL] = 0);
                        dtProject.Select().ToList<DataRow>().ForEach(r => { if (r["PROJECT_ID"].ToString() == alreadyselectedprojectis)  r[SELECT_COL] = 1; else  r[SELECT_COL] = 0; });
                    }
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
            if (dvSelectedProject.ToTable().Rows.Count == 0)
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
                //this.ShowMessageBoxWarning("Few Ledger Groups are created under Primary itself. Kindly move those Ledger Groups to any one of the four Natures and try again.");
                this.ShowMessageBoxWarning(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.LEDGER_GROUP_CREATE_UNDER_PRIMARY_GROUP));
                IsValid = false;
            }

            //01/07/2020, Project split is just between date range or FYs split
            if (IsValid )
            {
                if (chkSplitFY.Checked)
                {
                    if (this.ShowConfirmationMessage("Split Finance Year option is enabled, This XML file should be imported to new Database." + System.Environment.NewLine + System.Environment.NewLine +
                                    "    Fixed Deposit Vouchers (Investments, Openings and Renewals) will be rearranged based on splitting the Date From", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
                    {
                        IsValid = false;
                    }
                }
            }
            return IsValid;
        }

        private bool CheckPrimaryLedgerGroup()
        {
            bool isPrimary = true;
            ResultArgs resultArgs = new ResultArgs();

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


        private ResultArgs SplitProject(string Pid)
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
                    vouchersystem.ProjectId = Pid; // GetProjectId;
                    vouchersystem.DateFrom = UtilityMember.DateSet.ToDate(deDateFrom.DateTime.ToShortDateString(), false);
                    vouchersystem.DateTo = UtilityMember.DateSet.ToDate(deDateTo.DateTime.ToShortDateString(), false);
                    vouchersystem.VoucherImportType = ImportType.SplitProject;
                    vouchersystem.IsFYSplit = chkSplitFY.Checked;
                    vouchersystem.IsExportMasterAlone = chkOnlyMasterAlone.Checked;
                    vouchersystem.IsExportCasBankFDAlone = chkOnlyCashBankFDLedgers.Checked;
                    vouchersystem.IsShowMappingLedgers = chkShowLedgerMapping.Checked;

                    //01/07/2020, Project split is just between date range or FYs split
                    resultArgs = vouchersystem.FillExportVoucherTransaction();
                    CloseWaitDialog();
                    if (resultArgs.Success)
                    {
                        dsTransaction = vouchersystem.dsTransaction;
                        if (dsTransaction.Tables.Count > 0)
                        {
                            //On 04/05/2021 to have common splting rearrange FDs
                            //resultArgs = SplitFDAccountsBasedOnDateRange(dsTransaction);
                            if (chkSplitFY.Checked) //Only for Split FY (FD rearragne)
                            {
                                using (ImportVoucherSystem importsysterm = new ImportVoucherSystem())
                                {
                                    resultArgs = importsysterm.SplitFDAccountsBasedOnDateRange(dsTransaction, vouchersystem.DateFrom, vouchersystem.DateTo);

                                    if (resultArgs.Success)
                                    {
                                        if (chkSplitFY.Checked && chkOnlyMasterAlone.Checked)
                                        {
                                            resultArgs = vouchersystem.EnforceMasterExportSettings();
                                        }
                                    }
                                }
                            }

                            //02/02/2024, To check Closed Ledgers are used in Vouchers
                            if (resultArgs.Success)
                            {
                                resultArgs = vouchersystem.IsClosedLedgersInVouchers();
                            }

                            if (resultArgs.Success)
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
                                        //ShowMessageBox("Project(s) exported Successfully");
                                        if (!isBulkImport)
                                        {
                                            ShowMessageBox(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.PROJECT_EXPORT_SUCCESS));
                                        }
                                    }
                                    else
                                    {
                                        ShowMessageBox(resultArgs.Message);
                                    }
                                }
                            }
                            else
                            {
                                ShowMessageBox(resultArgs.Message);
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

            return resultArgs;
        }

       
       
        private ResultArgs RemoveProject()
        {
            ResultArgs resultArgs = new ResultArgs();

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

        private string CleanInvalidFileName(string fileName)
        {
            return Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty));
        }

        private ResultArgs ExportXML(DataSet dsTransaction)
        {
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                string BOCode = this.AppSetting.PartBranchOfficeCode;
                string Location = this.AppSetting.Location;
                if (dsTransaction.Tables.Count > 0)
                {
                    string projectname = dsTransaction.Tables["PROJECT"].Rows[0]["PROJECT"].ToString();
                    string xmlexportFileName = (isBulkImport ? ("_SplitFY_") : ("_SplitProject_")) + projectname  + "_" + Location + ".xml";
                    xmlexportFileName = BOCode +  xmlexportFileName;
                    ////replace special characters which are not valid for file names
                    //xmlexportFileName = xmlexportFileName.Replace("/", "").Replace("*", "").Replace(":", "");
                    ////--------------------------------------------------------------------------------------
                    xmlexportFileName = CleanInvalidFileName(xmlexportFileName);
                    xmlexportFileName = Path.Combine(FYSplitFolderPath, xmlexportFileName);
                                                            
                    if (isBulkImport)
                    {
                       resultArgs =  XMLConverter.WriteToXMLFile(dsTransaction, xmlexportFileName);
                    }
                    else
                    {
                        SaveFileDialog saveDialog = new SaveFileDialog();
                        saveDialog.Filter = "Xml Files|.xml";
                        saveDialog.Title = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.SPLIT_VOUCHER);
                        saveDialog.FileName = xmlexportFileName; //Acmeerp_SplitProject_" + DateTime.Now.Ticks.ToString() + ".xml";

                        if (saveDialog.ShowDialog() == DialogResult.OK)
                        {
                            resultArgs = XMLConverter.WriteToXMLFile(dsTransaction, saveDialog.FileName);
                            //resultArgs.Success = true;
                        }
                        else
                        {
                            resultArgs.Message = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.SPLIT_PROJECT_HAS_BEEN_CANCELLED);
                        }
                    }
                }
                else
                {
                    resultArgs.Message = "No Data available";
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
                AcMELog.WriteLog(ex.Message);
            }
            return resultArgs;
        }

        private void AssignFYSplitFolderPath()
        {
            FolderBrowserDialog folderPath = new FolderBrowserDialog();
            //Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FYSplitExport"));
            //folderPath.SelectedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FYSplitExport");
            folderPath.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;

            FYSplitFolderPath = string.Empty;
            if (folderPath.ShowDialog() == DialogResult.OK)
            {
                FYSplitFolderPath = folderPath.SelectedPath;
            }
        }
        #endregion

        private void repCheckProject_CheckedChanged(object sender, EventArgs e)
        {
            if (!isBulkImport)
            {
                DataTable dtSource = gcSplitProject.DataSource as DataTable;
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    foreach (DataRow drSource in dtSource.Rows)
                    {
                        drSource["SELECT"] = 0;
                    }
                    gcSplitProject.DataSource = dtSource;
                    gvSplitProject.SetFocusedRowCellValue(colSelect, true);

                    //int projectId = gvSplitProject.GetFocusedRowCellValue(colProjectId) != null ? this.UtilityMember.NumberSet.ToInteger(gvSplitProject.GetFocusedRowCellValue(colProjectId).ToString()) : 0;
                    //CheckEdit chkEdit = sender as CheckEdit;
                    //int status = Convert.ToInt32(chkEdit.CheckState);
                }
            }
        }

        private void chkSplitFY_CheckedChanged(object sender, EventArgs e)
        {
            deDateFrom.Enabled =  true;
            lcOnlyMasterAlone.Visibility = lcOnlyCashBankFDLedgers.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            chkOnlyMasterAlone.Checked = chkOnlyCashBankFDLedgers.Checked = false;
            chkOnlyCashBankFDLedgers.Enabled = false;
            if (chkSplitFY.Checked && !isBulkImport)
            {
                this.ShowMessageBoxWarning("Finance Year splitting option is enabled, \"Date From\" should be equal to Transaction Period \"Year From\".");
                deDateFrom.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
                deDateTo.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
                deDateFrom.Enabled = false;
                lcOnlyMasterAlone.Visibility = lcOnlyCashBankFDLedgers.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }


        }

        private void chkOnlyMasterAlone_CheckedChanged(object sender, EventArgs e)
        {
            chkOnlyCashBankFDLedgers.Enabled = (chkOnlyMasterAlone.Checked);
            if (!chkOnlyCashBankFDLedgers.Enabled)
            {
                chkOnlyCashBankFDLedgers.Checked = false;
            }
        }

        private void deDateFrom_EditValueChanged(object sender, EventArgs e)
        {
            LoadProjects();
        }

        private void deDateTo_EditValueChanged(object sender, EventArgs e)
        {
            LoadProjects();
        }

        private void gvSplitProject_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvSplitProject.RowCount.ToString();
        }
    }
}

/*
        /// <summary>
        /// On 26/03/2021, To check all FDs which are closed and make it as In Active 
        /// </summary>
        /// <param name="dtFDAccounts"></param>
        /// <param name="dtFDAllRenewals"></param>
        private void CheckClosedFDsBeforeSplittedFY(ref DataTable dtFDAccounts, ref DataTable dtFDAllRenewals)
        {
            try
            {
                DateTime dtSplitFromDate = deDateFrom.DateTime;
                DateTime dtSplitToDate = deDateTo.DateTime;

                using (ExportVoucherSystem vouchersystem = new ExportVoucherSystem())
                {
                    //dtFDAccounts.DefaultView.RowFilter = vouchersystem.AppSchema.FDAccount.STATUSColumn.ColumnName + " = 1";
                    //dtFDAccounts = dtFDAccounts.DefaultView.ToTable();

                    //dtFDAllRenewals.DefaultView.RowFilter = vouchersystem.AppSchema.FDAccount.STATUSColumn.ColumnName + " = 1";
                    //dtFDAllRenewals = dtFDAllRenewals.DefaultView.ToTable();

                    foreach (DataRow drFDRow in dtFDAccounts.Rows)
                    {
                        Int32 FDAccountId = UtilityMember.NumberSet.ToInteger(drFDRow[vouchersystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName].ToString());
                        string fdnumber = drFDRow[vouchersystem.AppSchema.FDAccount.FD_ACCOUNT_NUMBERColumn.ColumnName].ToString();
                        string FDType = drFDRow[vouchersystem.AppSchema.FDAccount.TRANS_TYPEColumn.ColumnName].ToString();
                        Int32 FDVoucherId = UtilityMember.NumberSet.ToInteger(drFDRow[vouchersystem.AppSchema.FDAccount.FD_VOUCHER_IDColumn.ColumnName].ToString());
                        DateTime FDInvestedDate = UtilityMember.DateSet.ToDate(drFDRow[vouchersystem.AppSchema.FDAccount.INVESTMENT_DATEColumn.ColumnName].ToString(), false);
                        double FDInvestmentAmount = UtilityMember.NumberSet.ToDouble(drFDRow[vouchersystem.AppSchema.FDAccount.AMOUNTColumn.ColumnName].ToString());
                        DateTime FDRecentRenewedDate = FDInvestedDate;

                        //1. Check current FDs had withdrwan fully
                        dtFDAllRenewals.DefaultView.RowFilter = vouchersystem.AppSchema.FDAccount.STATUSColumn.ColumnName + "=" + (Int32)Status.Active +
                                                              " AND " + vouchersystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName + "=" + FDAccountId +
                                                              " AND " + vouchersystem.AppSchema.FDRenewal.FD_TYPEColumn.ColumnName + "= '" + FDTypes.WD + "'" +
                                                              " AND " + vouchersystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName + "= '" + FDRenewalTypes.WDI + "'" +
                                                              " AND " + vouchersystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName + "< '" + dtSplitFromDate.ToShortDateString() + "'";

                        //2. If Current FD withdrwan fully, make that FD is in active
                        if (dtFDAllRenewals.DefaultView.Count > 0)
                        {
                            drFDRow.BeginEdit();
                            drFDRow[vouchersystem.AppSchema.FDAccount.STATUSColumn.ColumnName] = (Int32)Status.Inactive;
                            drFDRow.EndEdit();
                            dtFDAccounts.AcceptChanges();
                        }

                        dtFDAllRenewals.DefaultView.RowFilter = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBoxError(ex.Message);
            }
            finally
            {
                dtFDAccounts.DefaultView.RowFilter = string.Empty;
                dtFDAllRenewals.DefaultView.RowFilter = string.Empty;
            }
        }

        /// <summary>
        /// On 29/06/2020,
        /// While Project Import/Expot - Existing logic, we have taken all FDs without date from splitting condition.
        /// so this method is called after export or split project.
        /// This mehod is used to check all Exported FDs and renewals are with in splitted Date range
        /// 
        /// This method will be called only if date from is year from
        /// 
        /// 1. Check all Renewals and update status=0 if renewal date less than splitted date from
        /// 2. If all renewals which are deleted, change fd status is inactive
        /// 3. FD type is invested before splitted date from and If few FD renewals are based on splitted date from
        ///  Change FD type and flag as Opening, remove fd voucherid, real balance and maturity date

        /// </summary>
        /// <param name="dsSplitedProjectsVouchers"></param>
        private ResultArgs SplitFDAccountsBasedOnDateRange(DataSet dsSplitedProjectsVouchers)
        {
            ResultArgs result = new ResultArgs();
            result.Success = true;
            try
            {
                if (chkSplitFY.Checked)
                {
                    using (ExportVoucherSystem vouchersystem = new ExportVoucherSystem())
                    {
                        if (dsSplitedProjectsVouchers != null && dsSplitedProjectsVouchers.Tables[ExportVoucherSystem.FD_ACCOUNT_TABLE_NAME] != null &&
                           dsSplitedProjectsVouchers.Tables[ExportVoucherSystem.FD_RENEWAL_TABLE_NAME] != null && dsSplitedProjectsVouchers.Tables[ExportVoucherSystem.FD_VOUCHER_MASTER_TRANS_TABLE_NAME] != null)
                        {
                            DataTable dtFDAccount = dsSplitedProjectsVouchers.Tables[ExportVoucherSystem.FD_ACCOUNT_TABLE_NAME];
                            DataTable dtFDRenewals = dsSplitedProjectsVouchers.Tables[ExportVoucherSystem.FD_RENEWAL_TABLE_NAME];
                            DataTable dtFDVoucherMaster = dsSplitedProjectsVouchers.Tables[ExportVoucherSystem.FD_VOUCHER_MASTER_TRANS_TABLE_NAME];
                            DateTime dtSplitFromDate = deDateFrom.DateTime;
                            DateTime dtSplitToDate = deDateTo.DateTime;

                            //31/03/2021, to check *************************************
                            // take FDs only invested before splited period date to
                            dtFDAccount.DefaultView.RowFilter = "(" + vouchersystem.AppSchema.FDAccount.TRANS_TYPEColumn.ColumnName + "= '"+ FDTypes.OP.ToString() +"') OR "  +
                                                                "(" + vouchersystem.AppSchema.FDAccount.TRANS_TYPEColumn.ColumnName + " = '" + FDTypes.IN.ToString() + "' AND " +
                                                                vouchersystem.AppSchema.FDAccount.INVESTMENT_DATEColumn.ColumnName + " <= '" + dtSplitToDate + "')";
                            dtFDAccount = dtFDAccount.DefaultView.ToTable();
                            //**********************************************************
                            CheckClosedFDsBeforeSplittedFY(ref dtFDAccount, ref dtFDRenewals);

                            //Take all FD Invested alone with in date range and its renewals 
                            string statusexpression = vouchersystem.AppSchema.FDAccount.STATUSColumn.ColumnName + "=" + (Int32)Status.Active; 

                            dtFDAccount.DefaultView.RowFilter = statusexpression;
                            dtFDAccount = dtFDAccount.DefaultView.ToTable();

                            if (dtFDAccount.Rows.Count > 0)
                            {
                                statusexpression = "STATUS=1";
                                dtFDVoucherMaster.DefaultView.RowFilter = statusexpression;
                                dtFDVoucherMaster = dtFDVoucherMaster.DefaultView.ToTable();

                                //Change FD_Voucher_Master_Trans Status is inactive for all voucher which are less thatn splited date from
                                statusexpression = "IIF(" + vouchersystem.AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName + "< '" + dtSplitFromDate + "' OR " +
                                                                   vouchersystem.AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName + "> '" + dtSplitToDate + "', 0, 1)";
                                DataColumn dcVoucherStatus = new DataColumn("STATUS1", typeof(System.Int32), statusexpression);
                                dcVoucherStatus.DefaultValue = 1; //by default Active 
                                dtFDVoucherMaster.Columns.Add(dcVoucherStatus);

                                dtFDRenewals.DefaultView.RowFilter = "STATUS=1";
                                dtFDRenewals = dtFDRenewals.DefaultView.ToTable();
                                //1. Check all Renewals and update status=0 if renewal date less than splitted date from
                                statusexpression = "IIF(" + vouchersystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName + "< '" + dtSplitFromDate + "', 0, 1)";
                                DataColumn dcFDStatus = new DataColumn("STATUS1", typeof(System.Int32), statusexpression);
                                dcFDStatus.DefaultValue = 1; //by default Active 
                                dtFDRenewals.Columns.Add(dcFDStatus);

                                foreach (DataRow drFDRow in dtFDAccount.Rows)
                                {
                                    //Get FD Account Details 
                                    Int32 FDAccountId = UtilityMember.NumberSet.ToInteger(drFDRow[vouchersystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName].ToString());
                                    string FDType = drFDRow[vouchersystem.AppSchema.FDAccount.TRANS_TYPEColumn.ColumnName].ToString();
                                    Int32 FDVoucherId = UtilityMember.NumberSet.ToInteger(drFDRow[vouchersystem.AppSchema.FDAccount.FD_VOUCHER_IDColumn.ColumnName].ToString());
                                    DateTime FDInvestedDate = UtilityMember.DateSet.ToDate(drFDRow[vouchersystem.AppSchema.FDAccount.INVESTMENT_DATEColumn.ColumnName].ToString(), false);
                                    DateTime FDRecentMaturityDate = UtilityMember.DateSet.ToDate(drFDRow[vouchersystem.AppSchema.FDAccount.MATURED_ONColumn.ColumnName].ToString(), false);
                                    double FDInvestmentAmount = UtilityMember.NumberSet.ToDouble(drFDRow[vouchersystem.AppSchema.FDAccount.AMOUNTColumn.ColumnName].ToString());
                                    DateTime FDRecentRenewedDate = FDInvestedDate;

                                    double TotalFDRecentBalance = FDInvestmentAmount;

                                    //Get FD Renewal Details for selected FD Account
                                    dtFDRenewals.DefaultView.RowFilter = vouchersystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName + " = " + FDAccountId;
                                    Int32 NumberofRenewals = dtFDRenewals.DefaultView.Count;
                                    bool IsRenewalsDeleted = false;
                                    bool AllRenewalsDeleted = false;

                                    if (NumberofRenewals > 0)
                                    {
                                        //Get Recent valid Renewal Date Date
                                        dtFDRenewals.DefaultView.RowFilter = vouchersystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName + " = " + FDAccountId + " AND STATUS1 = 1";
                                        dtFDRenewals.DefaultView.Sort = vouchersystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName;
                                        AllRenewalsDeleted = (dtFDRenewals.DefaultView.Count == 0);
                                        IsRenewalsDeleted = (NumberofRenewals != dtFDRenewals.DefaultView.Count);

                                        //Get Current FD Account balance as on splitted date from
                                        //Sum of Accumulated interest amount with all renewals date less than date splitted date from
                                        //Sum of paritical withdrwal amount and withdrwalamount with all renewals date less than date splitted date from
                                        //Sum of Re invested amount with all renewals date less than date splitted date from
                                        dtFDRenewals.DefaultView.RowFilter = string.Empty;
                                        string commoncriteria = vouchersystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName + " = " + FDAccountId + " AND STATUS1 = 0";
                                        double AccInterestAmount = UtilityMember.NumberSet.ToDouble(dtFDRenewals.DefaultView.Table.Compute("SUM(" + vouchersystem.AppSchema.FDRenewal.INTEREST_AMOUNTColumn.ColumnName + ")",
                                                            commoncriteria + " AND " + vouchersystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName + "= '" + FDRenewalTypes.ACI.ToString() + "'").ToString());
                                        double WithdrwalAmount = UtilityMember.NumberSet.ToDouble(dtFDRenewals.DefaultView.Table.Compute("SUM(" + vouchersystem.AppSchema.FDRenewal.WITHDRAWAL_AMOUNTColumn.ColumnName + ")",
                                                            commoncriteria + " AND " + vouchersystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName + " IN ('" + FDRenewalTypes.WDI.ToString() + "', '" + FDRenewalTypes.PWD.ToString() + "')").ToString());

                                        double ReInvested = UtilityMember.NumberSet.ToDouble(dtFDRenewals.DefaultView.Table.Compute("SUM(" + vouchersystem.AppSchema.FDRenewal.REINVESTED_AMOUNTColumn.ColumnName + ")",
                                                            commoncriteria + " AND " + vouchersystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName + " IN ('" + FDRenewalTypes.RIN.ToString() + "')").ToString());

                                        TotalFDRecentBalance = (FDInvestmentAmount + AccInterestAmount + ReInvested) - WithdrwalAmount;

                                        //To get Recent Renewal date and maturity date, If renewals skipped
                                        if (IsRenewalsDeleted)
                                        //if (dtFDRenewals.DefaultView.Count > 0)
                                        {
                                            dtFDRenewals.DefaultView.RowFilter = commoncriteria;
                                            dtFDRenewals.DefaultView.Sort = vouchersystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName + " DESC";
                                            if (dtFDRenewals.DefaultView.Count > 0)
                                            {
                                                FDRecentRenewedDate = UtilityMember.DateSet.ToDate(dtFDRenewals.DefaultView[0][vouchersystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName].ToString(), false);
                                                FDRecentMaturityDate = UtilityMember.DateSet.ToDate(dtFDRenewals.DefaultView[0][vouchersystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName].ToString(), false);
                                            }
                                        }
                                    }
                                    dtFDRenewals.DefaultView.RowFilter = string.Empty;
                                    dtFDRenewals.DefaultView.Sort = string.Empty;

                                    if (FDType.ToUpper() == FDTypes.IN.ToString() && IsRenewalsDeleted && (FDInvestedDate < FDRecentRenewedDate || FDInvestedDate < dtSplitFromDate))
                                    {
                                        //3. FD type is invested before splitted date from and If few FD renewals are based on splitted date from
                                        //Change FD type and flag as Opening, remove fd voucherid, real balance and maturity date
                                        drFDRow[vouchersystem.AppSchema.FDAccount.INVESTMENT_DATEColumn.ColumnName] = FDRecentRenewedDate;
                                        drFDRow[vouchersystem.AppSchema.FDAccount.TRANS_TYPEColumn.ColumnName] = FDTypes.OP.ToString().ToUpper();
                                        drFDRow[vouchersystem.AppSchema.FDAccount.FD_SUB_TYPESColumn.ColumnName] = "FD-O";
                                        drFDRow[vouchersystem.AppSchema.FDAccount.FD_VOUCHER_IDColumn.ColumnName] = 0;
                                        drFDRow[vouchersystem.AppSchema.FDAccount.MATURED_ONColumn.ColumnName] = FDRecentMaturityDate;
                                        drFDRow[vouchersystem.AppSchema.FDAccount.AMOUNTColumn.ColumnName] = TotalFDRecentBalance;
                                        dtFDAccount.AcceptChanges();
                                    }
                                    else if (FDType.ToUpper() == FDTypes.IN.ToString() && !IsRenewalsDeleted && FDInvestedDate < dtSplitFromDate)
                                    {
                                        //4. FD type is opening If few FD renewals deleted based on splitted date from
                                        //change real balance and maturity date
                                        drFDRow[vouchersystem.AppSchema.FDAccount.INVESTMENT_DATEColumn.ColumnName] = FDRecentRenewedDate;
                                        drFDRow[vouchersystem.AppSchema.FDAccount.TRANS_TYPEColumn.ColumnName] = FDTypes.OP.ToString().ToUpper();
                                        drFDRow[vouchersystem.AppSchema.FDAccount.FD_SUB_TYPESColumn.ColumnName] = "FD-O";
                                        drFDRow[vouchersystem.AppSchema.FDAccount.FD_VOUCHER_IDColumn.ColumnName] = 0;
                                        drFDRow[vouchersystem.AppSchema.FDAccount.MATURED_ONColumn.ColumnName] = FDRecentMaturityDate;
                                        drFDRow[vouchersystem.AppSchema.FDAccount.AMOUNTColumn.ColumnName] = TotalFDRecentBalance;
                                        dtFDAccount.AcceptChanges();
                                    }
                                    else if (FDType.ToUpper() == FDTypes.OP.ToString()) //&& IsRenewalsDeleted
                                    {
                                        //5. FD type is opening If few FD renewals deleted based on splitted date from
                                        //change real balance and maturity date
                                        drFDRow[vouchersystem.AppSchema.FDAccount.AMOUNTColumn.ColumnName] = TotalFDRecentBalance;
                                        drFDRow[vouchersystem.AppSchema.FDAccount.MATURED_ONColumn.ColumnName] = FDRecentMaturityDate;
                                        dtFDAccount.AcceptChanges();
                                    }
                                }
                                
                                //For FD Account
                                dtFDAccount.DefaultView.RowFilter = "STATUS=1";
                                dtFDAccount = dtFDAccount.DefaultView.ToTable();
                                dsSplitedProjectsVouchers.Tables.Remove(ExportVoucherSystem.FD_ACCOUNT_TABLE_NAME);
                                dsSplitedProjectsVouchers.Tables.Add(dtFDAccount);

                                //For FD Voucher Master Trans
                                dtFDVoucherMaster.Columns.Remove("STATUS");
                                dtFDVoucherMaster.Columns["STATUS1"].ColumnName = "STATUS";
                                dtFDVoucherMaster.DefaultView.RowFilter = "STATUS=1";
                                dtFDVoucherMaster = dtFDVoucherMaster.DefaultView.ToTable();
                                dsSplitedProjectsVouchers.Tables.Remove(ExportVoucherSystem.FD_VOUCHER_MASTER_TRANS_TABLE_NAME);
                                dsSplitedProjectsVouchers.Tables.Add(dtFDVoucherMaster);

                                //For FD Renewals Status
                                dtFDRenewals.Columns.Remove("STATUS");
                                dtFDRenewals.Columns["STATUS1"].ColumnName = "STATUS";
                                //Attach Renewals which are less that date to
                                dtFDRenewals.DefaultView.RowFilter = "STATUS=1 AND " + vouchersystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName + "< '" + dtSplitToDate + "'";
                                dtFDRenewals = dtFDRenewals.DefaultView.ToTable();
                                dsSplitedProjectsVouchers.Tables.Remove(ExportVoucherSystem.FD_RENEWAL_TABLE_NAME);
                                dsSplitedProjectsVouchers.Tables.Add(dtFDRenewals);
                                result.Success = true;
                            }
                            else
                            {
                                //6. If all FD Accounts are skipped, update in Dataset
                                dsSplitedProjectsVouchers.Tables[ExportVoucherSystem.FD_ACCOUNT_TABLE_NAME].Rows.Clear();
                                dsSplitedProjectsVouchers.Tables[ExportVoucherSystem.FD_RENEWAL_TABLE_NAME].Rows.Clear();
                                dsSplitedProjectsVouchers.Tables[ExportVoucherSystem.FD_VOUCHER_MASTER_TRANS_TABLE_NAME].Rows.Clear();
                                dsSplitedProjectsVouchers.Tables[ExportVoucherSystem.FD_VOUCHER_TRANS_TABLE_NAME].Rows.Clear();

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
*/