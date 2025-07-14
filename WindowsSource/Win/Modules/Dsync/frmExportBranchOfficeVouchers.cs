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
using Bosco.Model;
using System.IO;
using System.Threading;
using DevExpress.XtraSplashScreen;
using System.Reflection;
using Bosco.Model.Dsync;
using System.ServiceModel;
using System.Net;
using System.Configuration;
using Bosco.Model.UIModel;

namespace ACPP.Modules.Dsync
{
    public partial class frmExportBranchOfficeVouchers : frmFinanceBaseAdd
    {
        #region Variables
        ResultArgs resultArgs = new ResultArgs();
        CommonMember UtilityMember = new CommonMember();
        CommonMethod UtilityMethod = new CommonMethod();
        private const string DATASET_NAME = "Vouchers";
        public const string DATASYNC_FOLDER = "AcMEERP_Vouchers";
        public bool IsDateLoaded = false;
        DataSyncService.DataSynchronizerClient dataSyncExportVouchers = new DataSyncService.DataSynchronizerClient();
        //  DsyncSystemBase AcMELogSQL = new DsyncSystemBase();

        public string UploadStatus = string.Empty;
        public long UploadFileLength = 0;
        public string BytesSent = string.Empty;

        private bool ForceToExport = false;
        private bool ExportTriedOnce = false;

        private Int32 MaxUploadFileSize = 20; // 20MB
        #endregion

        #region Properties
        ExportMode exportMode = ExportMode.Offline;
        public ExportMode VoucherExportMode
        {
            get { return exportMode; }
            set { exportMode = value; }
        }

        public string projectid
        {
            get
            {
                return GetProjectsIds();
            }
            set
            {
                //22/04/2020, To retain already selected projects (when we change date range)
                //projectid = value;
                string selectprojects = value;
                if (!String.IsNullOrEmpty(selectprojects))
                {
                    string[] ProjectIds = selectprojects.Split(',');
                    Int32 selectedIndex = 0;
                    for (int index = 0; index < chklstProjects.ItemCount; index++)
                    {
                        DataRowView drv = chklstProjects.GetItem(index) as DataRowView;
                        if (drv != null)
                        {
                            selectedIndex = Array.IndexOf(ProjectIds, drv[0].ToString());
                            if (selectedIndex >= 0)
                            {
                                chklstProjects.SetItemChecked(index, true);
                            }
                        }
                    }
                }
            }
        }

        private DateTime dtDateFrom { get; set; }
        private DateTime dtDateTo { get; set; }
        private string HeadOfficeCode { get; set; }
        private string BranchOfficeCode { get; set; }

        #endregion

        #region Events
        public frmExportBranchOfficeVouchers()
        {
            InitializeComponent();
        }

        public frmExportBranchOfficeVouchers(ExportMode exportMethod, bool forcetoexport = false)
            : this()
        {
            VoucherExportMode = exportMethod;
            lblUploadStatus.Visibility = lciProgressbar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            ForceToExport = forcetoexport;
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void frmExportBranchOfficeVouchers_Load(object sender, EventArgs e)
        {
            LoadDefaults();
        }

        private void deDateTo_Leave(object sender, EventArgs e)
        {
            //if (deDateFrom.DateTime > deDateTo.DateTime)
            //{
            //    DateTime dateTo = deDateTo.DateTime;
            //    deDateTo.DateTime = deDateFrom.DateTime;
            //    deDateFrom.DateTime = dateTo.Date;
            //}
        }


        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidInput())
                {
                    ExportTriedOnce = true;
                    ExportVoucherDetails();
                }

                //1. 18/01/2018 return sucess if only export option clicked (exculde validation message)---------------------------
                if (ForceToExport && ExportTriedOnce)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                //---------------------------------------------------------------------------------------------------------------
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            finally
            {
                AcMELog.WriteLog("-----Exporting Branch office Voucher to Head Office Ended ----");
            }
        }

        private void deDateFrom_Leave(object sender, EventArgs e)
        {
            if (deDateFrom.DateTime > deDateTo.DateTime)
            {
                deDateTo.DateTime = deDateFrom.DateTime;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.F3))
            {
                deDateFrom.Focus();
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.Checked)
            {
                chklstProjects.CheckAll();
            }
            else
            {
                chklstProjects.UnCheckAll();
            }
        }

        private void frmExportBranchOfficeVouchers_FormClosing(object sender, FormClosingEventArgs e)
        {
            //1. 18/01/2018 alert export voucher message if setting is enbaled and not exported ---------------------------
            if (ForceToExport && this.DialogResult == System.Windows.Forms.DialogResult.Cancel)
            {
                this.ShowMessageBox("Export Vouchers before closing Acmeerp");
            }
            //-----------------------------------------------------------------------------------------------------------
        }
        #endregion

        #region Methods

        /// <summary>
        /// Validating the User Input
        /// </summary>
        /// <returns></returns>
        public bool IsValidInput()
        {
            bool isValid = true;
            if (chklstProjects.CheckedItems.Count == 0)
            {
                XtraMessageBox.Show(this.GetMessage(MessageCatalog.DataSynchronization.Export.DSYNC_SELECT_ONE_PROJECT), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                isValid = false;
            }
            else if (deDateTo.DateTime < deDateFrom.DateTime)
            {
                XtraMessageBox.Show(this.GetMessage(MessageCatalog.DataSynchronization.Export.DSYNC_VALIDATE_DATE), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                isValid = false;
            }
            else if (!CheckPrimaryLedgerGroup())
            {
                //this.ShowMessageBoxWarning("Few Ledger Groups are created under Primary itself. Kindly move those Ledger Groups to any one of the four Natures and try again.");
                this.ShowMessageBoxWarning(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_VOUCHER_LEDGER_MOVE_BASED_NATURE));
                isValid = false;
                ExportTriedOnce = true;
            }
            else if (!isHeadOfficeLedgersExists())
            {
                //XtraMessageBox.Show("Head Office Ledgers are not available to export vouchers. Kindly download the Head Office masters and try again.", "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_HO_LEDGER_NOT_AVAIL_INFO));
                isValid = false;
                ExportTriedOnce = true;
            }
            else if (!IsBranchLedgerMapped())
            {
                isValid = false;
                ExportTriedOnce = true;
            }
            else if (!CheckMappedHeadofficeFDLdger()) //On 06/07/2017, Check FD ledger mapping correctly
            {
                ExportTriedOnce = true;
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        /// Loading the project details
        /// </summary>
        public void LoadProjects()
        {
            //22/04/2020, To retain already selected projects (when we change date range)
            string AlreadySelectedProjects = GetProjectsIds();

            chkSelectAll.Checked = false;
            using (MappingSystem vouchersystem = new MappingSystem())
            {
                ResultArgs resultArgs = vouchersystem.FetchProjectsLookup();
                if (resultArgs.Success && resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count != 0)
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

                    chklstProjects.DisplayMember = "PROJECT";
                    chklstProjects.ValueMember = "PROJECT_ID";
                    chklstProjects.DataSource = dtProjects;

                    //22/04/2020, To retain already selected projects (when we change date range)
                    this.projectid = AlreadySelectedProjects;
                }
            }

        }

        private bool CheckPrimaryLedgerGroup()
        {
            bool isPrimary = true;

            using (ExportVoucherSystem exportSystem = new ExportVoucherSystem())
            {
                resultArgs = exportSystem.CheckPrimaryLedgerGroup();
                if (!resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    isPrimary = false;
                }
            }
            return isPrimary;
        }

        private bool isHeadOfficeLedgersExists()
        {
            bool isExists = true;

            if (this.AppSetting.MapHeadOfficeLedger == (int)YesNo.Yes)
            {
                using (ImportMasterSystem masterSystem = new ImportMasterSystem())
                {
                    resultArgs = masterSystem.FetchBrachHeadOfficeLedgers();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        isExists = true;
                    }
                    else
                    {
                        isExists = false;
                    }
                }
            }
            return isExists;
        }

        private bool IsBranchLedgerMapped()
        {
            bool isMapped = true;
            if (this.AppSetting.MapHeadOfficeLedger == (int)YesNo.Yes)
            {
                resultArgs = CheckHeadofficeLdgerExists();

                if (!resultArgs.Success)
                {
                    isMapped = false;
                }
            }
            return isMapped;
        }


        /// <summary>
        /// This method is used to check FD ledger mapped with HO fd ledger correctly
        /// </summary>
        /// <returns></returns>
        public bool CheckMappedHeadofficeFDLdger()
        {
            AcMELog.WriteLog("CheckMappedHeadofficeFDLdger: Check Branch office FD Ledger Mapped with Head Office FD Ledgers Started");
            try
            {
                using (ExportVoucherSystem vouchersystem = new ExportVoucherSystem())
                {
                    resultArgs = vouchersystem.CheckInvalidMappedFDLedgers();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("CheckMappedHeadofficeFDLdger:Ended");
            }
            else
            {
                this.ShowMessageBox(resultArgs.Message);
                AcMELog.WriteLog("Some FD Ledgers not mapped correctly with HO FD ledger");
            }
            return resultArgs.Success;
        }

        /// <summary>
        /// Check wherther Headoffice Ledger Exists 
        /// </summary>
        public ResultArgs CheckHeadofficeLdgerExists()
        {
            AcMELog.WriteLog("Check Branch Office Ledger with Head Office Ledger : Check Branch office Ledger Mapped with Head Office Ledgers Started");
            try
            {
                // string UnmappedLedgerNames = string.Empty;
                // int NoofUmmap = 0;
                using (ExportVoucherSystem vouchersystem = new ExportVoucherSystem())
                {
                    vouchersystem.ProjectId = projectid;
                    vouchersystem.DateFrom = UtilityMember.DateSet.ToDate(deDateFrom.DateTime.ToShortDateString(), false);// dtDateFrom;
                    vouchersystem.DateTo = UtilityMember.DateSet.ToDate(deDateTo.DateTime.ToShortDateString(), false);// dtDateTo;
                    resultArgs = vouchersystem.CheckHOLedgerExists();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        resultArgs.Success = false;
                        //resultArgs.Message = "Branch Office Ledgers are not mapped with the Head Office Ledgers.Exporting Vouchers terminated.";
                        resultArgs.Message = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_BO_NOT_MAPPED_HO_LEDGER_INFO);
                        //foreach (DataRow item in resultArgs.DataSource.Table.Rows)
                        //{
                        //    UnmappedLedgerNames += item["Ledger_Name"].ToString();
                        //    AcMELog.WriteLog("Un Mapped Ledger:" + UnmappedLedgerNames);
                        //    UnmappedLedgerNames += Environment.NewLine;
                        //    NoofUmmap++;
                        //}
                        frmUnMappedLedgers unmappedledgers = new frmUnMappedLedgers(resultArgs.DataSource.Table, true);
                        unmappedledgers.ShowDialog();
                        if (unmappedledgers.DialogResult == System.Windows.Forms.DialogResult.OK)
                        {
                            resultArgs.Success = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            if (resultArgs.Success)
            {
                AcMELog.WriteLog("Check Branch Office Ledgers with Head Office Ledgers Ended");
            }
            else
            {
                AcMELog.WriteLog("Some Ledgers are not Mapped with Head Office Ledgers");
            }
            return resultArgs;
        }

        /// <summary>
        /// Loading the Default details
        /// </summary>
        public void LoadDefaults()
        {
            if (VoucherExportMode == ExportMode.Offline)
            {
                rgSynMode.Properties.Items[1].Enabled = false;
                rgSynMode.SelectedIndex = 0;
            }
            else
            {
                rgSynMode.Properties.Items[0].Enabled = false;
                rgSynMode.SelectedIndex = 1;
            }

            deDateFrom.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deDateTo.DateTime = deDateFrom.DateTime.AddMonths(1).AddDays(-1);
            LoadProjects();

            //  SetActiveTransactionPeriod();
        }

        //private ResultArgs SetActiveTransactionPeriod()
        //{
        //    DataTable dtPeriod = null;
        //    using (ExportVoucherSystem exportSystem = new ExportVoucherSystem())
        //    {
        //        resultArgs = exportSystem.FetchActiveTransactionPeriod();
        //        if (resultArgs.Success)
        //        {
        //            dtPeriod = resultArgs.DataSource.Table;
        //            if (dtPeriod != null && dtPeriod.Rows.Count >= 0)
        //            {
        //                deDateFrom.DateTime = exportSystem.DateSet.ToDate(dtPeriod.Rows[0][exportSystem.AppSchema.AccountingPeriod.YEAR_FROMColumn.ColumnName].ToString(), false);
        //                //deDateTo.DateTime = exportSystem.DateSet.ToDate(dtPeriod.Rows[0][exportSystem.AppSchema.AccountingPeriod.YEAR_TOColumn.ColumnName].ToString(), false);
        //                deDateTo.DateTime = deDateFrom.DateTime.AddMonths(1);
        //                deDateTo.DateTime = deDateTo.DateTime.AddDays(-1);
        //            }
        //            else
        //            {
        //                deDateFrom.DateTime = DateTime.Now;
        //                deDateTo.DateTime = DateTime.Now.AddMonths(1);
        //            }
        //        }
        //    }
        //    return resultArgs;
        //}

        public void ExportVoucherDetails()
        {

            ResultArgs resultArgs = new ResultArgs();
            try
            {
                AcMELog.WriteLog("-----Exporting Branch office Voucher to Head Office Started----");
                DataSet dsTransaction = new DataSet(DATASET_NAME);
                using (ExportVoucherSystem vouchersystem = new ExportVoucherSystem())
                {
                    vouchersystem.ProjectId = projectid;
                    vouchersystem.DateFrom = UtilityMember.DateSet.ToDate(deDateFrom.DateTime.ToShortDateString(), false);// dtDateFrom;
                    vouchersystem.DateTo = UtilityMember.DateSet.ToDate(deDateTo.DateTime.ToShortDateString(), false);// dtDateTo;
                    vouchersystem.VoucherImportType = ImportType.HeadOffice;
                    //this.ShowWaitDialog("Exporting Branch Office Vouchers...");
                    this.ShowWaitDialog(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_BO_LEDGERS_INFO));
                    resultArgs = vouchersystem.FillExportVoucherTransaction();
                    this.CloseWaitDialog();
                    if (resultArgs.Success)
                    {
                        dsTransaction = vouchersystem.dsTransaction;
                        if (dsTransaction.Tables.Count > 0)
                        {
                            if (rgSynMode.SelectedIndex == (int)Bosco.Utility.Mode.Offline)
                            {
                                resultArgs = ExportXmlOffline(dsTransaction);
                            }
                            else
                            {
                                //Check latest license available in Branch and Notify to update the latest license to import masters
                                //if (this.CheckForInternetConnection())
                                bool uploadbyhttp = (AppSetting.UpdaterDownloadBy == "1");
                                uploadbyhttp = false;
                                if ( (!uploadbyhttp && this.CheckForInternetConnection()) ||
                                     (uploadbyhttp && this.CheckForInternetConnectionhttp()) )   
                                {
                                    resultArgs = diagnosisExportedVouchersInPortal(dsTransaction);
                                    if (resultArgs.Success)
                                    {
                                        DataSyncService.DataSynchronizerClient objDsyncService = new DataSyncService.DataSynchronizerClient();
                                        if (objDsyncService.IsLatestLicenseAvailable(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode, this.UtilityMember.DateSet.ToDate(this.AppSetting.LicenseKeyGeneratedDate, false)))
                                        {
                                            resultArgs = ExportXmlOnline(dsTransaction);

                                            //27/02/2020 (Mysore), For Temp Purpose, Export Sub Ledger Vouchers--------------------------------
                                            if (resultArgs.Success && this.AppSetting.IS_DIOMYS_DIOCESE && this.UIAppSetting.EnableSubLedgerBudget == "1")
                                            {
                                                resultArgs = ExportSubLedgetVouchersForTempPurpose();
                                            }
                                            //----------------------------------------------------------------------------------------
                                        }
                                    }
                                }
                                else
                                {
                                    resultArgs.Message = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_UNABLE_REACH_PORTAL_CHECK_FTP_INFO);
                                }
                            }
                        }
                    }
                }

            }
            catch (FaultException<DataSyncService.AcMeServiceException> ex)
            {
                // Modified by Mr Alwar 
                CloseWaitDialog();
                AcMELog.WriteLog(ex.Message);
                resultArgs.Message = ex.Detail.Message;
                //if (ex.Detail.Message.Contains(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_LICENSEKEY_NOT_UPTO_DATE_INFO)))
                //{
                //    AcMELog.WriteLog(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_LICENSEKEY_NOT_UPTO_DATE_INFO));
                //    //if (DialogResult.OK == XtraMessageBox.Show(ex.Detail.Message, "Update License", MessageBoxButtons.OK))
                //    if (DialogResult.OK == XtraMessageBox.Show(ex.Detail.Message, this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_UPDATE_LICENSE_INFO), MessageBoxButtons.OK))
                //    {
                //        new frmPortalUpdates(PortalUpdates.UpdateLicense).ShowDialog();
                //    }
                //}
            }
            catch (CommunicationException ex)
            {
                CloseWaitDialog();
                resultArgs.Message = ex.Message;
                AcMELog.WriteLog(ex.Message);
            }
            catch (Exception ex)
            {
                CloseWaitDialog();
                resultArgs.Message = ex.Message;
                AcMELog.WriteLog(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                CloseWaitDialog();
            }

            if (!resultArgs.Success)
            {
                CloseWaitDialog();
                //XtraMessageBox.Show(resultArgs.Message, "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                XtraMessageBox.Show(resultArgs.Message, this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                AcMELog.WriteLog(resultArgs.Message);
            }
            else
            {
                lblUploadStatus.Visibility = lciProgressbar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                if (exportMode == ExportMode.Online)
                {
                    XtraMessageBox.Show(this.GetMessage(MessageCatalog.DataSynchronization.Export.DSYNC_EXPORT_VOUCHERS_SUCCESS), MessageCatalog.Common.COMMON_MESSAGE_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //this.ShowMessageBox("Vouchers exported successfully.");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_VOUCHER_EXPORT_SUCCESS_INFO));
                }
                AcMELog.WriteLog(this.GetMessage(MessageCatalog.DataSynchronization.Export.DSYNC_EXPORT_VOUCHERS_SUCCESS));
                this.Close();
            }
        }

        public string GetProjectsIds()
        {
            string SelectedProjectId = string.Empty;
            try
            {
                string pid = string.Empty;
                AcMELog.WriteLog("Get Selected Project Id's");
                foreach (DataRowView drProject in chklstProjects.CheckedItems)
                {
                    pid += drProject[0].ToString();
                    pid += ',';
                }
                SelectedProjectId = pid.TrimEnd(',');
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }


            //if (resultArgs.Success)
            //{
            //    AcMELog.WriteLog("Get Selected Project Ids Ended.");
            //}
            //else
            //{
            //    AcMELog.WriteLog("Error in Selecting Project IDs. " + resultArgs.Message);
            //}
            return SelectedProjectId;
        }

        private string GetProjectNames()
        {
            string SelectedProjectName = string.Empty;
            try
            {
                string pname = string.Empty;
                AcMELog.WriteLog("Get Selected Project Name");
                foreach (DataRowView drProject in chklstProjects.CheckedItems)
                {
                    pname += drProject[6].ToString() + ",";
                }
                SelectedProjectName = pname.TrimEnd(',');
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return SelectedProjectName;
        }

        private ResultArgs ExportXmlOffline(DataSet dsTransaction)
        {
            resultArgs.Success = false;
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Xml Files|.xml";
                //saveDialog.Title = "Export Vouchers";
                saveDialog.Title = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_VOUCHER_EXPORT_INFO);
                //saveDialog.FileName = "AcME_ERP_Vouchers_" + DateTime.Now.Ticks.ToString() + ".xml";
                saveDialog.FileName = "Acmeerp_"+ AppSetting.HeadofficeCode + "_" + AppSetting.PartBranchOfficeCode   +"_vouchers_" + DateTime.Now.Ticks.ToString() + ".xml";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    XMLConverter.WriteToXMLFile(dsTransaction, saveDialog.FileName);
                    resultArgs.Success = true;
                }
                else
                {
                    //resultArgs.Message = "Exporting Vouchers has been cancelled";
                    resultArgs.Message = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_VOUCHER_CANCEL_INFO);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
                AcMELog.WriteLog(ex.Message);
            }
            finally { }
            return resultArgs;
        }

        private ResultArgs ExportXmlOnline(DataSet dsTransaction)
        {
            //string XMLFileName = "AcME_ERP_Vouchers" + DateTime.Now.Ticks.ToString() + ".xml";
            string XMLFileName = "Acmeerp_" + AppSetting.HeadofficeCode + "_" + AppSetting.PartBranchOfficeCode + "_vouchers_" + DateTime.Now.Ticks.ToString() + ".xml";
            string VoucherFilePath = Path.Combine(Application.StartupPath.ToString(), XMLFileName);
            resultArgs.Success = false;
            try
            {
                //this.ShowWaitDialog("Connecting to Internet...");
                this.ShowWaitDialog(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_VOUCHER_CONNECTING_INTERNET_INFO));
                //lblUploadStatus.Text = "Connecting to Internet..";
                //if (this.CheckForInternetConnection())
                bool uploadbyhttp = (AppSetting.UpdaterDownloadBy == "1");
                uploadbyhttp = false;
                if ((!uploadbyhttp && this.CheckForInternetConnection()) ||
                     (uploadbyhttp && this.CheckForInternetConnectionhttp()))
                {
                    this.CloseWaitDialog();
                    //this.ShowWaitDialog("Internet connection succeeded.");
                    this.ShowWaitDialog(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_VOUCHER_INTERNET_CONNECTION_SUCCESS_INFO));
                    Application.DoEvents();
                    CloseWaitDialog();
                    //this.ShowWaitDialog("Preparing to send Vouchers to Portal..");
                    this.ShowWaitDialog(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_VOUCHER_SEND_TO_PORTAL_INFO));
                    XMLConverter.WriteToXMLFile(dsTransaction, VoucherFilePath);
                    resultArgs = XMLConverter.ConvertXMLToDataSetWithResultArgs(VoucherFilePath);
                    if (resultArgs.Success)
                    {
                        //On 21/04/2020, to check files size (allowed only for 20MB to be uploaded)
                        double filesize = this.UtilityMember.FileSet.GetFileSizeInMB(VoucherFilePath);
                        if (this.UtilityMember.NumberSet.ToDouble(filesize.ToString()) <= MaxUploadFileSize)
                        {
                            this.CloseWaitDialog();
                            DataSet dsVouchers = resultArgs.DataSource.TableSet;
                            if (dsVouchers != null && dsVouchers.Tables.Count > 0)
                            {
                                //this.ShowWaitDialog("Checking for the Mismatched Projects..");
                                this.ShowWaitDialog(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_VOUCHER_CHECKING_MISMATCH_PROJECT));
                                DataTable dtMistMatchedProjects = dataSyncExportVouchers.GetMismatchedProjects(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode, dsVouchers.Tables["Project"]);
                                if (dtMistMatchedProjects.Rows.Count == 0)
                                {
                                    CloseWaitDialog();

                                    //# 05/02/2020, Check Mismatched Ledgers from Acmeerp portal Ledgers
                                    // #1. To check all not mapped BO Ledgers (If Mapping not mandatory) in Acmeerp portal
                                    // #2. To check All mapped HO ledgers in Acmeerp Portal 
                                    DataTable dtBOLedgers = GetLedgers(dsVouchers.Tables["Ledger"]);
                                    if (AppSetting.MapHeadOfficeLedger == 0 && dtBOLedgers != null && dtBOLedgers.Rows.Count > 0)
                                    {
                                        //Skip not mapped BO Ledgers for mapping is not mandatory, it will cretae new Ledger in Acmeerp portal
                                        dtBOLedgers.DefaultView.RowFilter = "IS_BRANCH_LEDGER <>1";
                                        dtBOLedgers = dtBOLedgers.DefaultView.ToTable();
                                    }

                                    //# 05/02/2020, Check Mismatched Ledgers from Acmeerp portal
                                    //DataTable dtMismatchedLedgers = dataSyncExportVouchers.GetMismatchedLedgers(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode, GetLedgers(dsVouchers.Tables["Ledger"]));
                                    DataTable dtMismatchedLedgers = new DataTable();
                                    if (dtBOLedgers != null && dtBOLedgers.Rows.Count > 0)
                                    {
                                        // Temporely we have identify and fixed some columns instead of allowing all the Columns (08.01.2022)

                                        // dtBOLedgers = dtBOLedgers.DefaultView.ToTable(true, new string[] { "ledger_code", "ledger_name", "is_branch_ledger", "group_id", "ledger_group", "Nature_id", "Ledger_type", "Ledger_Sub_type" });

                                        // Temporalily mentioned the fields
                                        dtBOLedgers = dtBOLedgers.DefaultView.ToTable(true, new string[] { "ledger_code", "ledger_name", "group_id", "ledger_group" });
                                        dtBOLedgers.AcceptChanges();

                                        dtMismatchedLedgers = dataSyncExportVouchers.GetMismatchedLedgers(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode, dtBOLedgers);
                                    }
                                    if (dtMismatchedLedgers.Rows.Count == 0)
                                    {
                                        //this.ShowDialog("Uploading Voucher file in Portal..");
                                        lblUploadStatus.Visibility = lciProgressbar.Visibility = (VoucherExportMode == ExportMode.Online) ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                        //lblUploadStatus.Text = "Uploading voucher file started..";
                                        lblUploadStatus.Text = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_VOUCHER_UPLOADING_VOUCHER_FILE_START);
                                        resultArgs = UploadFileVoucherFileToAcmeerpPortal(VoucherFilePath);
                                        //  this.CloseWaitDialog();
                                        if (resultArgs.Success)
                                        {
                                            resultArgs.Success = true;
                                            string selectedProjects = this.GetProjectNames();
                                            string loginusername = this.LoginUser.LoginUserFullName;
                                            //On 26/02/2018, voucher details are updated in datasync in status (update name of projects, location and who is uploaded by in datasync details)
                                            //this.LoginUser.LoginUserName
                                            //selectedProjects, this.LoginUser.Location
                                            bool UpdateStatus = dataSyncExportVouchers.UpdateDsyncStatus(this.AppSetting.HeadofficeCode,
                                                                this.AppSetting.BranchOfficeCode, XMLFileName, selectedProjects, this.LoginUser.Location, this.LoginUser.LoginUserName);
                                            //bool UpdateStatus = dataSyncExportVouchers.UpdateDsyncStatus(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode, XMLFileName);
                                            if (!UpdateStatus)
                                            {
                                                //resultArgs.Message = "Synchronization status is not updated but the Voucher File is updated in the portal";
                                                resultArgs.Message = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_VOUCHER_SYNCH_STATUS_NOT_UPDATE_INFO);
                                            }

                                            //on 16/12/2020, To update closed date of Project-----------------------
                                            UpdateProjectClosedDate();
                                            //----------------------------------------------------------------------
                                        }
                                    }
                                    else
                                    {
                                        resultArgs.Message = "Branch Office Ledgers are mismatched in Head Office.";
                                        frmMismatchedLedgerList mismatched = new frmMismatchedLedgerList(dtMismatchedLedgers);
                                        mismatched.Owner = this;
                                        mismatched.ShowDialog();
                                    }
                                }
                                else
                                {
                                    CloseWaitDialog();
                                    //resultArgs.Message = "Branch Office Projects are mismatched in Head Office.";
                                    resultArgs.Message = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_VOUCHER_BO_PROJECT_MISMACTH_HO_INFO);
                                    frmMismatchedDetails mismatched = new frmMismatchedDetails(dtMistMatchedProjects);
                                    mismatched.Owner = this;
                                    mismatched.ShowDialog();
                                }
                            }
                        }
                        else
                        {
                            CloseWaitDialog();
                            resultArgs.Message = "Maximum Voucher file size must be less than or equal to " + MaxUploadFileSize.ToString()
                                    + "MB, Please reduce the number of Vouchers/Date Range/Projects";
                        }
                    }
                    else
                    {
                        CloseWaitDialog();
                    }
                }
                else
                {
                    CloseWaitDialog();
                    //resultArgs.Message = "Unable to reach Portal. Please check your internet connection.";
                    //this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_VOUCHER_UNABLE_REACH_PORTAL_INFO);
                    resultArgs.Message = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_UNABLE_REACH_PORTAL_CHECK_FTP_INFO);
                }
            }
            catch (FaultException<DataSyncService.AcMeServiceException> ex)
            {
                CloseWaitDialog();
                resultArgs.Message = ex.Detail.Message;
            }
            catch (CommunicationException ex)
            {
                CloseWaitDialog();
                //resultArgs.Message = "Unable to reach Portal. Please check your internet connection or FTP rights.";
                if (ex.Message.Contains("The remote server returned an unexpected response: (413) Request Entity Too Large."))
                {
                    resultArgs.Message = ex.Message;
                }
                else
                {
                    resultArgs.Message = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_UNABLE_REACH_PORTAL_CHECK_FTP_INFO);
                }
                AcMELog.WriteLog(ex.Message);
            }
            catch (Exception ex)
            {
                CloseWaitDialog();
                //resultArgs.Message = "Error in ExportXmlOnline " + ex.ToString();
                resultArgs.Message = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_ERROR_EXPORT_XML_ONLINE) + ex.ToString();
                AcMELog.WriteLog(ex.Message);
            }
            finally
            {
                File.Delete(VoucherFilePath);
            }
            return resultArgs;
        }


        /// <summary>
        /// 27/02/2020
        /// 
        /// *** This is temp purpose, it should be added in to online export xml and it will take care of server datasync later ****
        /// 
        /// This method is used to Export Sub Ledger Vouchers for given data range
        /// 
        /// </summary>
        /// <returns></returns>
        private ResultArgs ExportSubLedgetVouchersForTempPurpose()
        {
            string msg = string.Empty;
            using (ExportVoucherSystem voucherSubledgerSystem = new ExportVoucherSystem())
            {
                voucherSubledgerSystem.ProjectId = projectid;
                voucherSubledgerSystem.DateFrom = UtilityMember.DateSet.ToDate(deDateFrom.DateTime.ToShortDateString(), false);// dtDateFrom;
                voucherSubledgerSystem.DateTo = UtilityMember.DateSet.ToDate(deDateTo.DateTime.ToShortDateString(), false);// dtDateTo;
                voucherSubledgerSystem.VoucherImportType = ImportType.HeadOffice;
                ResultArgs resultArg = voucherSubledgerSystem.FetchVoucherSubLedgers();
                if (resultArg.Success)
                {
                    DataTable dtSubLedgerVouchers = resultArg.DataSource.Table;
                    if (dtSubLedgerVouchers != null)
                    {
                        DataSyncService.DataSynchronizerClient objservice = new DataSyncService.DataSynchronizerClient();
                        msg = objservice.UpdateSubLedgerVouchers(AppSetting.BranchOfficeCode, AppSetting.HeadofficeCode, AppSetting.Location, voucherSubledgerSystem.DateFrom, voucherSubledgerSystem.DateTo, dtSubLedgerVouchers);
                    }
                }
            }

            if (!string.IsNullOrEmpty(msg))
            {
                resultArgs.Message = "Export Sub Ledger Vouchers :: " + msg;
            }
            else
            {
                resultArgs.Success = true;
            }
            return resultArgs;
        }

        private DataTable GetLedgers(DataTable dtBranchLedgers)
        {
            DataTable dtBranchOfficeLedgers = null;
            if (dtBranchLedgers != null)
            {
                DataView dvLedgers = new DataView(dtBranchLedgers);
                dvLedgers.RowFilter = "GROUP_ID NOT IN(12,13,14)";
                dtBranchOfficeLedgers = dvLedgers.ToTable();
            }
            return dtBranchOfficeLedgers;
        }

        private void ftpClient_OnUpload(object sender, ProgressStatusEventArgs e)
        {
            lblUploadStatus.AllowHtmlStringInCaption = true;
            lblUploadStatus.Text = e.Status;
            pgUploadStatus.Properties.Maximum = this.UtilityMember.NumberSet.ToInteger(e.FileLength.ToString());
            pgUploadStatus.Properties.Step = 2048; //this.UtilityMember.NumberSet.ToInteger(e.ByteSent);
            pgUploadStatus.PerformStep();

            // UploadFileLength = e.FileLength;
            // BytesSent = e.ByteSent;
        }


        private ResultArgs UploadFileVoucherFileToAcmeerpPortal(string LocalFilePath)
        {
            ResultArgs result = new ResultArgs();
            result.Success = true;
            try
            {
                string uploadUrl = Path.Combine(ConfigurationManager.AppSettings["ftpURL"].ToString(), DATASYNC_FOLDER);
                string uploadFileName = Path.GetFileName(LocalFilePath);
                string ftpUrl = string.Format("{0}/{1}", uploadUrl, uploadFileName);
                using (AcMEERPFTP ftpUpload = new AcMEERPFTP())
                {
                    if (AppSetting.UpdaterDownloadBy == "1") //Upload by via http
                    {
                        ftpUpload.OnProgress += new EventHandler<ProgressStatusEventArgs>(ftpClient_OnUpload);
                        result = ftpUpload.upload(ftpUrl, LocalFilePath);

                        
                        //if (!string.IsNullOrEmpty(LocalFilePath) && File.Exists(LocalFilePath))
                        //{
                        //    result = ftpUpload.uploadDatabyWebClient(BranchUploadAction.BranchVouchers, this.AppSetting.HeadofficeCode, this.AppSetting.PartBranchOfficeCode,
                        //                    this.AppSetting.Location, uploadFileName, string.Empty, string.Empty, null, LocalFilePath);

                        //    /*using (MemoryStream ms = new MemoryStream())
                        //    {
                        //        using (FileStream fs = File.OpenRead(LocalFilePath))
                        //        {
                        //            fs.CopyTo(ms);
                        //        }

                        //        result = ftpUpload.uploadDatabyWebClient(BranchUploadAction.BranchVouchers, this.AppSetting.HeadofficeCode, this.AppSetting.PartBranchOfficeCode,
                        //                    this.AppSetting.Location, uploadFileName, string.Empty, string.Empty, ms, string.Empty);
                        //    }*/
                        //}
                    }
                    else
                    {
                        ftpUpload.OnProgress += new EventHandler<ProgressStatusEventArgs>(ftpClient_OnUpload);
                        result = ftpUpload.upload(ftpUrl, LocalFilePath);
                        //result = ftpUpload.uploadbyHTTP(uploadFileName, LocalFilePath);
                    }
                }
            }
            catch (Exception ex)
            {
                //result.Message = "Error in Upload Voucher file using FTP. " + ex.ToString();
                result.Message = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_ERROR_EXPORT_VOUCHER_FILE) + ex.ToString();
            }
            finally
            {
            }
            return result;
        }

        /// <summary>
        /// on 16/12/2020, To update closed date of Project
        /// </summary>
        private ResultArgs UpdateProjectClosedDate()
        {
            ResultArgs result = new ResultArgs();
            try
            {
                if (chklstProjects.DataSource != null)
                {
                    DataTable dtProjects = chklstProjects.DataSource as DataTable;
                    using (ProjectSystem projectsystem = new ProjectSystem())
                    {
                        foreach (DataRowView drProject in chklstProjects.CheckedItems)
                        {
                            if (drProject != null)
                            {
                                string projectname = drProject["PROJECT_NAME"].ToString().Trim();
                                string projectcloseddate = drProject[projectsystem.AppSchema.Project.DATE_CLOSEDColumn.ColumnName].ToString();

                                try
                                {
                                    bool rtn = dataSyncExportVouchers.UpdateProjectClosedDate(this.AppSetting.BranchOfficeCode, this.AppSetting.HeadofficeCode,
                                                                                              this.AppSetting.Location, projectname, projectcloseddate);
                                }
                                catch (FaultException<DataSyncService.AcMeServiceException> ex)
                                {
                                    this.ShowMessageBoxWarning(projectname + " - " + ex.Detail.Message);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                AcMELog.WriteLog(ex.Message);
            }
            return result;
        }


        /// <summary>
        /// This method is used to check exported voucher(s) are already available in portal for differnet project or different date range
        /// This issue will occur "Record is Available" issue in data sync
        /// </summary>
        /// <param name="dsExportedVouchers"></param>
        /// <returns></returns>
        private ResultArgs diagnosisExportedVouchersInPortal(DataSet dsExportedVouchers)
        {
            ResultArgs result = new ResultArgs();
            try
            {
                if (chkPreValidation.Checked)
                {
                    string msg = "Pre-Validation option is enabled, Are you sure to check Exported Voucher(s) with Portal already Exported Voucher(s) ?";
                    if (this.ShowConfirmationMessage(msg, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        this.ShowWaitDialog("Checking Vouchers in Portal");
                        using (ExportVoucherSystem vouchersystem = new ExportVoucherSystem())
                        {
                            DataTable dtPortalVoucherOthersDate = dataSyncExportVouchers.CheckVouchersInOtherProjectsOrDates(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode,
                                                                            this.AppSetting.Location, deDateFrom.DateTime, deDateTo.DateTime);
                            result = vouchersystem.CheckVouchersInPortalOtherProjectsOrDates(dsExportedVouchers, dtPortalVoucherOthersDate, deDateFrom.DateTime, deDateTo.DateTime);

                            if (!result.Success)
                            {
                                this.CloseWaitDialog();
                                DataTable dtAllExistsVouchers = result.DataSource.Table;
                                frmAvailableVouchersOtherProjectDate frmavailablevouchers = new frmAvailableVouchersOtherProjectDate(dtAllExistsVouchers);
                                frmavailablevouchers.ShowDialog();
                            }
                        }
                    }
                    else
                    {
                        result.Success = true;
                    }
                }
                else
                {
                    result.Success = true;
                }
            }
            catch (CommunicationException ex)
            {
                CloseWaitDialog();
                result.Message = ex.Message;
                AcMELog.WriteLog(ex.Message);
            }
            catch (Exception err)
            {
                this.CloseWaitDialog();
                result.Message = err.Message;
                this.ShowMessageBoxError(err.Message);
            }
            finally
            {
                this.CloseWaitDialog();
            }
            return result;
        }

        #endregion

        private void deDateTo_EditValueChanged(object sender, EventArgs e)
        {
            LoadProjects();
        }

        private void deDateFrom_EditValueChanged(object sender, EventArgs e)
        {
            LoadProjects();
        }

    }
}