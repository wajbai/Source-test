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
using System.IO;
using System.Reflection;
using System.Threading;
using Bosco.Model.Dsync;
using System.ServiceModel;
using Bosco.Model.UIModel.Master;
using Bosco.Utility.ConfigSetting;
using AcMEDSync.Model;

namespace ACPP.Modules.Dsync
{
    public partial class frmPortalUpdates : frmFinanceBaseAdd
    {
        public static string ACPERP_LICENSE_PATH = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "AcMEERPLicense.xml");
        ResultArgs resultArgs = null;
        bool isLicenceKeyUpdated = false;
        string LicenceKey = string.Empty;
        string LicenceKeyTarget = string.Empty;
        SimpleEncrypt.SimpleEncDec objdec = new SimpleEncrypt.SimpleEncDec();
        public static string ACPERP_Title = "Acme.erp";
        public static string ACPERP_LICENSE_KEY = "AcMEERPLicense.xml";
        public static string ACPERP_GENERAL_LOG = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "AcppGeneralLog.txt");
        SettingProperty AppSetting = new SettingProperty();
        private string targetpath = "";

        #region Properties
        private string FileName { get; set; }

        // Portal Updates mode to proceed data sync
        private ExportMode dsyncmode = ExportMode.Offline;
        private ExportMode VoucherExportMode
        {
            get { return dsyncmode; }
            set { dsyncmode = value; }
        }
        #endregion

        #region Constructor
        public frmPortalUpdates()
        {
            InitializeComponent();
        }

        public frmPortalUpdates(PortalUpdates portalUpdates)
            : this()
        {
            this.Height = this.Height - 110;
            lcImportMasterNote.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcImportMasterRemarks.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (portalUpdates == PortalUpdates.ImportMasters)
            {
                lcgExportVouchers.Visibility = lcgLicenseKey.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcImportTDSMasters.Visibility = lcImportTDSMasters.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //this.Text = "Import Masters from Head Office";
                this.Text = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.IMPORT_MASTERS_FROM_HO_INFO);
                //lciSyncMode.Text = "Import Mode";
                lciSyncMode.Text = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.IMPORT_MODE);
                lcImportMasterNote.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                //this.Height = this.Height - 5;
            }
            else if (portalUpdates == PortalUpdates.UploadVouchers)
            {
                lcgImportMasters.Visibility = lcgLicenseKey.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcImportTDSMasters.Visibility = lcImportTDSMasters.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //this.Text = "Export Vouchers to Head Office";
                this.Text = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_VOUCHERS_TO_HO_INFO);
                //lciSyncMode.Text = "Export Mode";
                lciSyncMode.Text = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_MODE);
            }
            else if (portalUpdates == PortalUpdates.UpdateLicense)
            {
                lcgExportVouchers.Visibility = lcgImportMasters.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcImportTDSMasters.Visibility = lcImportTDSMasters.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //this.Text = "Update License";
                this.Text = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.UPDATE_LICENSE);
                //lciSyncMode.Text = "Update Mode";
                lciSyncMode.Text = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.UPDATE_MODE);
            }
            else if (portalUpdates == PortalUpdates.ImportTDSMasters)
            {
                lcgExportVouchers.Visibility = lcgImportMasters.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcgLicenseKey.Visibility = lcgLicenseKey.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //this.Text = "Import TDS Masters from Head Office";
                this.Text = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.IMPORT_TDS_MASTER_FROM_HO);
                //lciSyncMode.Text = "Import Mode";
                lciSyncMode.Text = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.IMPORT_MODE);
            }
            this.CenterToParent();
        }
        #endregion

        #region Methods
        private ResultArgs UpdateLicenseFile(string NewFilePath, string AcmeFilePath)
        {
            ResultArgs resultArgs = new ResultArgs();
            string DestFileName = string.Empty;
            string SourceLicenseData = string.Empty;
            try
            {
                SourceLicenseData = XMLConverter.ReadFromXMLFile(NewFilePath);
                if (SourceLicenseData.Contains("<LicenseKey>"))
                {
                    DestFileName = (AppSetting.AccesstoMultiDB == (int)YesNo.Yes) && SettingProperty.ActiveDatabaseName.Trim() != "acperp" ?
                        Path.Combine(LicenceKeyTarget, "AcMEERP" + DateTime.Now.Ticks.ToString() + ".xml") : AcmeFilePath;
                    if (!string.IsNullOrEmpty(SourceLicenseData))
                    {
                        resultArgs = XMLConverter.WriteToXMLFile(SourceLicenseData, DestFileName);
                        if (resultArgs.Success && AppSetting.AccesstoMultiDB == (int)YesNo.Yes)
                        {
                            string[] Fname = DestFileName.Split('\\');
                            foreach (string item in Fname)
                            {
                                if (item.Contains(".xml"))
                                {
                                    DestFileName = item;
                                }
                            }
                            UpdateMultiDbLicense(DestFileName);
                        }
                    }
                }
                else
                {
                    //XtraMessageBox.Show("License File is invalid. It does not contain license details.", ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.LICENSE_FILE_INVALID), ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private bool UpdateMultiDbLicense(string MultiLicenseKey)
        {
            bool IsSuccess = false;
            string DbName = SettingProperty.ActiveDatabaseName;
            string MultipleDBPath = SettingProperty.RestoreMultipleDBPath.ToString();
            try
            {
                if (DbName != string.Empty)
                {
                    DataSet dsRestore = new DataSet();
                    DataTable dt = new DataTable();
                    dsRestore = XMLConverter.ConvertXMLToDataSet(MultipleDBPath);
                    if (!dsRestore.Tables[0].Columns.Contains("MultipleLicenseKey"))
                    {
                        dsRestore.Tables[0].Columns.Add("MultipleLicenseKey", typeof(string));
                    }
                    if (!dsRestore.Tables[0].Columns.Contains("RestoreDBName"))
                    {
                        dsRestore.Tables[0].Columns.Add("RestoreDBName", typeof(string));
                    }

                    DataView dvDb = new DataView();
                    dvDb = dsRestore.Tables[0].AsDataView();
                    dvDb.RowFilter = "Restore_Db= '" + DbName + "'";
                    if (dvDb.Count == 0)
                    {
                        dsRestore.Tables[0].Rows.Add(DbName, MultiLicenseKey, !string.IsNullOrEmpty(dvDb.Table.Rows[0]["RestoreDBName"].ToString()) ?
                            dvDb.Table.Rows[0]["RestoreDBName"].ToString() : DbName);
                        XMLConverter.WriteToXMLFile(dsRestore, MultipleDBPath);
                    }
                    else
                    {
                        DataView dvfilter = dsRestore.Tables[0].AsDataView();
                        dvfilter.RowFilter = "Restore_Db <> '" + DbName + "'";
                        DataSet dsMultiDb = new DataSet();
                        dsMultiDb.Tables.Add(dvfilter.ToTable());
                        int temp = 0;
                        foreach (DataRow item in dvfilter.Table.Rows)
                        {
                            if (item["Restore_Db"].ToString().Equals(DbName.Trim()))
                            {
                                dsMultiDb.Tables[0].Rows.Add(DbName, MultiLicenseKey,
                                                      !string.IsNullOrEmpty(dvDb.Table.Rows[temp]["RestoreDBName"].ToString()) ?
                                                      dvDb.Table.Rows[temp]["RestoreDBName"].ToString() : DbName);
                            }
                            temp++;
                        }
                        XMLConverter.WriteToXMLFile(dsMultiDb, MultipleDBPath);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }

            return IsSuccess;

        }

        private bool isValidInput()
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(FileName))
            {
                //this.ShowMessageBoxWarning("Master file is not selected.");
                this.ShowMessageBoxWarning(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.MASTER_FILE_NOT_SELECT));
                isValid = false;
            }
            //else if (string.IsNullOrEmpty(txtPath.Text))
            //{
            //    XtraMessageBox.Show("Path is Empty.");
            //    isValid = false;
            //}
            else if (!File.Exists(FileName))
            {
                //this.ShowMessageBoxWarning("File does not exists in the path.");
                this.ShowMessageBoxWarning(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.FILE_DOES_NOT_EXISTS_IN_PATH));
                isValid = false;
            }

            return isValid;
        }
        private void HideOfflineControlsinImportMasters()
        {
            //simpleLabelItem5.Text = simpleLabelItem5.Text + "  ( To Download the Defined Masters from Portal)";
            simpleLabelItem5.Text = simpleLabelItem5.Text + "  " + this.GetMessage(MessageCatalog.Master.FinanceDataSynch.DOWNLOAD_DEFINED_MASTERS_FROM_PORTAL);
            lciLicnekeypath.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lciElipse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }
        private void HideOfflineControlsinImportLicense()
        {
            //simpleLabelItem1.Text = simpleLabelItem1.Text + "  ( License Model for the Branch)";
            simpleLabelItem1.Text = simpleLabelItem1.Text + "  " + this.GetMessage(MessageCatalog.Master.FinanceDataSynch.LICENSE_MODEL_FOR_BRANCH);
            lciPath.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        private void ShowOfflineControlsinImportTDSMasters()
        {
            lblTDSMasters.Text = string.Empty;
            //lblTDSMasters.Text = "<b>Import TDS Masters</b>";
            lblTDSMasters.Text = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.IMPORT_TDS_MASTERS);
            lcTDSMAsterPath.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        }
        private void HideOfflineControlsinImportTDSMasters()
        {
            //lblTDSMasters.Text = lblTDSMasters.Text + "  ( To Download the Defined TDS Masters from Portal)";
            lblTDSMasters.Text = lblTDSMasters.Text + "  " + this.GetMessage(MessageCatalog.Master.FinanceDataSynch.DOWNLOAD_DEFINED_TDS_MASTERS_FROM_PORTAL);
            lcTDSMAsterPath.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }
        private void ShowofflinecontrolsinImportMasters()
        {
            simpleLabelItem5.Text = string.Empty;
            //simpleLabelItem5.Text = "<b>Masters</b>";
            simpleLabelItem5.Text = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.MASTERS_CAPTION);
            lciLicnekeypath.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        }
        private void ShowOfflineControlsinImportLicense()
        {
            simpleLabelItem1.Text = string.Empty;
            //simpleLabelItem1.Text = "<b>License Key</b>";
            simpleLabelItem1.Text = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.LICENSE_KEY_CAPTION);
            lciPath.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            lciElipse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        }
        private bool ValidateLicenceKey()
        {
            bool Rtn = false;

            try
            {
                if (File.Exists(LicenceKey))
                {
                    lblBOCode.Text = GetLicBranchCode();
                    lblBName.Text = GetLicInstituteName();
                    lblHeadOfficeName.Text = GetLicHOName();
                    lblHOCode.Text = GetLicHOCode();

                    lblBName.OptionsToolTip.ToolTipTitle = "Branch Office Name";
                    lblBName.OptionsToolTip.ToolTip = lblBName.Text;

                    lblHeadOfficeName.OptionsToolTip.ToolTipTitle = "Head Office Name";
                    lblHeadOfficeName.OptionsToolTip.ToolTip = lblHeadOfficeName.Text;
                }

            }
            catch (Exception err)
            {
                WriteLog("Error in Validating License Key " + err.Message);
            }
            return Rtn;
        }
        public string GetLicBranchCode()
        {
            DataTable dtLicenseInfo = new DataTable();
            string BranchOfficeCode = " ";
            try
            {
                if (File.Exists(LicenceKey))
                {
                    DataSet dsLicenseInfo = XMLConverter.ConvertXMLToDataSet(LicenceKey);
                    if (dsLicenseInfo != null && dsLicenseInfo.Tables.Count > 0)
                    {
                        dtLicenseInfo = dsLicenseInfo.Tables["LicenseKey"];
                        if (dtLicenseInfo != null && dtLicenseInfo.Rows.Count > 0)
                        {
                            DataRow drLicense = dtLicenseInfo.Rows[0];
                            BranchOfficeCode = objdec.DecryptString(drLicense["BRANCH_OFFICE_CODE"].ToString());
                            BranchOfficeCode = BranchOfficeCode.Remove(0, 6).ToUpper();
                            BranchOfficeCode = BranchOfficeCode.ToUpper();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally { }
            return BranchOfficeCode;
        }
        public string GetLicHOCode()
        {
            DataTable dtLicenseInfo = new DataTable();
            string HeadOfficeCode = " ";
            try
            {
                if (File.Exists(LicenceKey))
                {
                    DataSet dsLicenseInfo = XMLConverter.ConvertXMLToDataSet(LicenceKey);
                    if (dsLicenseInfo != null && dsLicenseInfo.Tables.Count > 0)
                    {
                        dtLicenseInfo = dsLicenseInfo.Tables["LicenseKey"];
                        if (dtLicenseInfo != null && dtLicenseInfo.Rows.Count > 0)
                        {
                            DataRow drLicense = dtLicenseInfo.Rows[0];
                            HeadOfficeCode = objdec.DecryptString(drLicense["HEAD_OFFICE_CODE"].ToString());
                            HeadOfficeCode = HeadOfficeCode.ToUpper();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally { }
            return HeadOfficeCode;
        }
        public string GetLicInstituteName()
        {
            DataTable dtLicenseInfo = new DataTable();
            string HeadOfficeCode = " ";
            try
            {
                if (File.Exists(LicenceKey))
                {
                    DataSet dsLicenseInfo = XMLConverter.ConvertXMLToDataSet(LicenceKey);
                    if (dsLicenseInfo != null && dsLicenseInfo.Tables.Count > 0)
                    {
                        dtLicenseInfo = dsLicenseInfo.Tables["LicenseKey"];
                        if (dtLicenseInfo != null && dtLicenseInfo.Rows.Count > 0)
                        {
                            DataRow drLicense = dtLicenseInfo.Rows[0];
                            HeadOfficeCode = objdec.DecryptString(drLicense["InstituteName"].ToString());
                            HeadOfficeCode = HeadOfficeCode.ToUpper();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally { }
            return HeadOfficeCode;
        }
        public string GetLicHOName()
        {
            DataTable dtLicenseInfo = new DataTable();
            string HOName = " ";
            try
            {
                if (File.Exists(LicenceKey))
                {
                    DataSet dsLicenseInfo = XMLConverter.ConvertXMLToDataSet(LicenceKey);
                    if (dsLicenseInfo != null && dsLicenseInfo.Tables.Count > 0)
                    {
                        dtLicenseInfo = dsLicenseInfo.Tables["LicenseKey"];
                        if (dtLicenseInfo != null && dtLicenseInfo.Rows.Count > 0)
                        {
                            DataRow drLicense = dtLicenseInfo.Rows[0];
                            HOName = objdec.DecryptString(drLicense["HEAD_OFFICE_NAME"].ToString());
                            HOName = HOName.ToUpper();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally { }
            return HOName;
        }
        public static void WriteLog(string msg)
        {
            string logpath = ACPERP_GENERAL_LOG;
            try
            {
                StreamWriter sw = new StreamWriter(logpath, true);

                if (msg.Replace("-", "").Length > 0)
                {
                    msg = (DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt")) + " || " + msg;
                }
                sw.WriteLine(msg);
                sw.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in writing log " + ex.Message);
            }
        }

        #endregion

        #region Events
        private void btnMasterDownload_Click(object sender, EventArgs e)
        {
            resultArgs = new ResultArgs();
            try
            {
                if (!this.IsLicenseKeyMismatchedByLicenseKeyDBLocation())
                {
                    if (VoucherExportMode == ExportMode.Offline)
                    {
                        //frmImportHeadofficeMasters frmMasters = new frmImportHeadofficeMasters();
                        //frmMasters.Owner = this;
                        //frmMasters.ShowDialog();
                        DataSet dsMasters = null;
                        if (isValidInput())
                        {
                            using (ImportMasterSystem objMasters = new ImportMasterSystem())
                            {
                                resultArgs = XMLConverter.ConvertXMLToDataSetWithResultArgs(FileName);
                                if (resultArgs.Success)
                                {
                                    dsMasters = resultArgs.DataSource.TableSet;
                                }
                                if (dsMasters != null && dsMasters.Tables.Count > 0 && resultArgs != null && resultArgs.Success)
                                {
                                    //DialogResult dialogResult = this.ShowConfirmationMessage("Masters downloaded Successfully." + Environment.NewLine + "Do You want to update Ledger Mapping with Projects?" + Environment.NewLine + Environment.NewLine +
                                    //             "Yes      : Discard Previous Mappings and Update New Mapping." + Environment.NewLine + Environment.NewLine +
                                    //             "No       : Keep Previous Mapping." + Environment.NewLine + Environment.NewLine, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    DialogResult dialogResult = this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.MASTER_DOWNLOADED_SUCCESSFULLY_INFO) + " " + Environment.NewLine + this.GetMessage(MessageCatalog.Master.FinanceDataSynch.UPDATE_LEDGER_MAP_WITH_PROJECT) + " " + Environment.NewLine + Environment.NewLine +
                                                 this.GetMessage(MessageCatalog.Master.FinanceDataSynch.YES_COLON_DISCARD_PREVIOUS_MAPPING_AND_UPDATE_NEW_MAPPING) + " " + Environment.NewLine + Environment.NewLine +
                                                 this.GetMessage(MessageCatalog.Master.FinanceDataSynch.NO_COLON_KEEP_PREVIOUS_MAPPING) + " " + Environment.NewLine + Environment.NewLine, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    objMasters.ProjectLedgerMapping = dialogResult == System.Windows.Forms.DialogResult.No ? false : true;
                                    frmMapHeadOfficeLedgers frmMapMismatchedLedger = new frmMapHeadOfficeLedgers();
                                    frmMapHeadOfficeProjects frmMappedheadOfficeprojects = new frmMapHeadOfficeProjects();
                                    //this.ShowWaitDialog("Updating Masters in Branch Office..");
                                    this.ShowWaitDialog(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.UPDATING_MASTER_IN_BO));
                                    resultArgs = objMasters.ImportMasters(dsMasters, frmMapMismatchedLedger, frmMappedheadOfficeprojects);
                                    this.CloseWaitDialog();
                                    txtImportMasterRemarks.Text = string.Empty;
                                    lcImportMasterRemarks.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    this.Height = 225;
                                    if (!string.IsNullOrEmpty(objMasters.ImportMasterRemarks))
                                    {
                                        lcImportMasterRemarks.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                        txtImportMasterRemarks.Text = objMasters.ImportMasterRemarks;
                                        txtImportMasterRemarks.Height = 30;
                                        this.Height = 275;
                                    }
                                    this.CenterToParent();
                                }
                            }
                        }
                    }
                    else
                    {
                        //this.ShowWaitDialog("Connecting to Acme.erp Portal..");
                        this.ShowWaitDialog(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.CONNECTING_ACMEERP_PORTAL));
                        if (this.CheckForInternetConnectionhttp())
                        {
                            CloseWaitDialog();
                            if (!string.IsNullOrEmpty(this.AppSetting.BranchOfficeCode) && !string.IsNullOrEmpty(this.AppSetting.HeadofficeCode))
                            {
                                DataSet dsMaster = null;
                                Application.DoEvents();
                                //this.ShowWaitDialog("Fetching Masters from Portal..");
                                this.ShowWaitDialog(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.FETCHING_MASTER_FROM_PORTAL));
                                Application.DoEvents();
                                DataSyncService.DataSynchronizerClient dataclientService = new DataSyncService.DataSynchronizerClient();
                                if (dataclientService.IsLatestLicenseAvailable(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode, this.UtilityMember.DateSet.ToDate(this.AppSetting.LicenseKeyGeneratedDate, false)))
                                {
                                    dsMaster = dataclientService.GetMasterDetails(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode);
                                    this.CloseWaitDialog();
                                }
                                if (dsMaster != null && dsMaster.Tables.Count > 0)
                                {
                                    using (ImportMasterSystem objMasters = new ImportMasterSystem())
                                    {
                                        resultArgs.Success = true;

                                        //DialogResult dialogResult = this.ShowConfirmationMessage("Masters downloaded Successfully." + Environment.NewLine + "Do You want to update Ledger Mapping with Projects?" + Environment.NewLine + Environment.NewLine +
                                        //        "Yes      : Discard Previous Mappings and Update New Mapping." + Environment.NewLine + Environment.NewLine +
                                        //        "No       : Keep Previous Mapping." + Environment.NewLine + Environment.NewLine, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                        DialogResult dialogResult = this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.MASTER_DOWNLOADED_SUCCESSFULLY_INFO) + " " + Environment.NewLine + this.GetMessage(MessageCatalog.Master.FinanceDataSynch.UPDATE_LEDGER_MAP_WITH_PROJECT) + " " + Environment.NewLine + Environment.NewLine +
                                               this.GetMessage(MessageCatalog.Master.FinanceDataSynch.YES_COLON_DISCARD_PREVIOUS_MAPPING_AND_UPDATE_NEW_MAPPING) + " " + Environment.NewLine + Environment.NewLine +
                                               this.GetMessage(MessageCatalog.Master.FinanceDataSynch.NO_COLON_KEEP_PREVIOUS_MAPPING) + " " + Environment.NewLine + Environment.NewLine, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                        objMasters.ProjectLedgerMapping = dialogResult == System.Windows.Forms.DialogResult.No ? false : true;

                                        frmMapHeadOfficeLedgers frmMapMismatchedLedger = new frmMapHeadOfficeLedgers();
                                        frmMapHeadOfficeProjects frmMappedheadOfficeprojects = new frmMapHeadOfficeProjects();
                                        //this.ShowWaitDialog("Updating Masters in Branch Office..");
                                        this.ShowWaitDialog(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.UPDATING_MASTER_IN_BO));
                                        resultArgs = objMasters.ImportMasters(dsMaster, frmMapMismatchedLedger, frmMappedheadOfficeprojects);
                                        this.CloseWaitDialog();
                                    }
                                }
                                else
                                {
                                    //resultArgs.Message = "Dataset is Empty. It does not contain master details.";
                                    resultArgs.Message = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.DATASET_EMPTY_DOES_NOT_MASTER_DETAILS);
                                }
                            }
                        }
                        else
                        {
                            CloseWaitDialog();
                            //resultArgs.Message = "Unable to reach Portal. Please check your internet connection.";
                            resultArgs.Message = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_UNABLE_REACH_PORTAL_CHECK_FTP_INFO);
                        }
                    }
                }
                else {
                    this.ShowMessageBox("License key Location(s) and Database Location are mismatching, Check your License Key");
                }
            }
            catch (FaultException<DataSyncService.AcMeServiceException> ex)
            {
                CloseWaitDialog();
                resultArgs.Message = ex.Detail.Message;
                if (resultArgs.Message.Contains("Your license key is not up-to-date"))
                {
                    //if (DialogResult.OK == XtraMessageBox.Show(ex.Detail.Message, "Update License", MessageBoxButtons.OK))
                    if (DialogResult.OK == XtraMessageBox.Show(ex.Detail.Message, this.GetMessage(MessageCatalog.Master.FinanceDataSynch.UPDATE_LICENSE_INFO), MessageBoxButtons.OK))
                    {
                        new frmPortalUpdates(PortalUpdates.UpdateLicense).ShowDialog();
                    }
                }
            }
            catch (FaultException ex)
            {
                CloseWaitDialog();
                resultArgs.Message = ex.ToString();
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
                AcMELog.WriteLog(ex.ToString());
            }

            if (resultArgs.Success)
            {
                //XtraMessageBox.Show("Head Office Masters imported Successfully.", this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.IMPORT_HO_MASTERS_SUCCESS_INFO), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (!string.IsNullOrEmpty(resultArgs.Message))
                {
                    CloseWaitDialog();
                    XtraMessageBox.Show(resultArgs.Message, this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //else
                //{
                //    XtraMessageBox.Show("Failure in Importing Master Details.", MessageCatalog.DataSynchronization.Common.ACMEERP, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
            }
        }

        private void btnLicenseDownload_Click(object sender, EventArgs e)
        {
            resultArgs = new ResultArgs();
            try
            {
                if (VoucherExportMode == ExportMode.Offline)
                {
                    //Data_Utility.frmUpdateLicence updatelic = new Data_Utility.frmUpdateLicence();
                    //updatelic.ShowDialog();
                    //if (updatelic.DialogResult == DialogResult.OK)
                    //{
                    //    Bosco.Utility.ConfigSetting.SettingProperty.Is_Application_Logout = true;
                    //    Application.Restart();
                    //}

                    UpdateLicenseDetails();
                }
                else
                {
                    //this.ShowWaitDialog("Connecting to AcME ERP Portal..");
                    this.ShowWaitDialog(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.CONNECTING_ACMEERP_PORTAL));
                    bool IsExists = false;
                    if (this.CheckForInternetConnectionhttp())
                    {
                        CloseWaitDialog();
                        DataTable dtLicense = new DataTable();
                        if (!string.IsNullOrEmpty(this.AppSetting.BranchOfficeCode) && !string.IsNullOrEmpty(this.AppSetting.HeadofficeCode))
                        {
                            DataSyncService.DataSynchronizerClient dataclient = new DataSyncService.DataSynchronizerClient();
                            IsExists = dataclient.IsBranchExists(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode);
                            if (IsExists)
                            {
                                //this.ShowWaitDialog("Downloading License from Portal..");
                                this.ShowWaitDialog(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.DOWNLODING_LICENSE_FROM_PORTAL));
                                dtLicense = dataclient.GetLicenseDetails(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode);
                                this.CloseWaitDialog();
                                if (dtLicense != null && dtLicense.Rows.Count > 0)
                                {
                                    //if (File.Exists(ACPERP_LICENSE_PATH))
                                    //{
                                    //    File.Delete(ACPERP_LICENSE_PATH);
                                    //}

                                    //dtLicense.WriteXml(ACPERP_LICENSE_PATH);
                                    //using (LegalEntitySystem legalEntitySystem = new LegalEntitySystem())
                                    //{
                                    //    this.ShowWaitDialog("Updating License Details..");
                                    //    resultArgs = legalEntitySystem.ApplyLicensePeriod(ACPERP_LICENSE_PATH);
                                    //    this.CloseWaitDialog();
                                    //    if (resultArgs.Success)
                                    //    {
                                    //        this.ShowMessageBox("License Updated Successfully.");
                                    //        Bosco.Utility.ConfigSetting.SettingProperty.Is_Application_Logout = true;
                                    //        Application.Restart();
                                    //    }
                                    //}
                                    UpdateLicenseDetails(dtLicense);
                                }
                                else
                                {
                                    //XtraMessageBox.Show("License information is empty from Head Office.", "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.LICENSE_INFORMATION_EMPTY_FROM_HO), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                frmPortalLogin frmportalLogin = new frmPortalLogin();
                                frmportalLogin.Owner = this;
                                frmportalLogin.ShowDialog();
                                if (frmportalLogin.DialogResult == System.Windows.Forms.DialogResult.OK)
                                {
                                    this.DialogResult = DialogResult.OK;
                                    this.Close();
                                }
                            }
                        }
                        else
                        {
                            frmPortalLogin frmportalLogin = new frmPortalLogin();
                            frmportalLogin.Owner = this;
                            frmportalLogin.ShowDialog();
                            if (frmportalLogin.DialogResult == System.Windows.Forms.DialogResult.OK)
                            {
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                        }
                    }
                    else
                    {
                        CloseWaitDialog();
                        //this.ShowMessageBox("Unable to reach Portal. Please check your internet connection.");
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.UNABLE_TO_REACH_PORTAL_CHECK_INTERNET_CONNECTION));
                    }
                }
            }
            catch (FaultException<DataSyncService.AcMeServiceException> ex)
            {
                this.CloseWaitDialog();
                this.ShowMessageBox(ex.Detail.Message);
            }
            catch (FaultException ex)
            {
                CloseWaitDialog();
                resultArgs.Message = ex.ToString();
            }
            catch (CommunicationException ex)
            {
                CloseWaitDialog();
                //this.ShowMessageBoxWarning("Unable to reach Portal. Please check your internet connection.");
                this.ShowMessageBoxWarning(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.UNABLE_TO_REACH_PORTAL_CHECK_INTERNET_CONNECTION));
                AcMELog.WriteLog(ex.Message);
            }
        }

        private void UpdateLicenseDetails(DataTable dtOnlineLicenseDetails = null)
        {
            string TempPath = Path.Combine(Path.GetTempPath(), "AcmeLicense_" + DateTime.Now.Ticks.ToString() + ".xml"); // Temporary File Creation to write Online License Data

            string AcmeDefaultPath = Path.Combine(LicenceKeyTarget, ACPERP_LICENSE_KEY);
            if (VoucherExportMode == ExportMode.Online)
            {
                if (dtOnlineLicenseDetails != null && dtOnlineLicenseDetails.Rows.Count > 0)
                {
                    dtOnlineLicenseDetails.WriteXml(TempPath);
                    LicenceKey = TempPath;
                }
            }
            resultArgs = UpdateLicenseFile(LicenceKey, AcmeDefaultPath);

            if (resultArgs.Success)
            {
                isLicenceKeyUpdated = true;
                string DestFileName = string.Empty;
                string[] Fname = SettingProperty.ActiveDatabaseLicenseKeypath.Split('\\');
                foreach (string item in Fname)
                {
                    if (item.Contains(".xml"))
                    {
                        DestFileName = item;
                    }
                }
                if (AppSetting.AccesstoMultiDB == (int)YesNo.Yes && SettingProperty.ActiveDatabaseName.Trim() != "acperp" && DestFileName != ACPERP_LICENSE_KEY)
                {
                    if (VoucherExportMode != ExportMode.Online)
                        File.Delete(SettingProperty.ActiveDatabaseLicenseKeypath);
                    else
                        File.Delete(TempPath);
                }
                //XtraMessageBox.Show("License Key is Updated", ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.LICENSE_KEY_UPDATED), ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
                if (this.DialogResult == DialogResult.OK)
                {
                    if (Bosco.Utility.ConfigSetting.SettingProperty.Is_Application_Logout)
                    {
                        //28/01/2025, Get portal date and timing ----------------------------------------------
                        // and get vocuher grace days details 
                        UpdateVoucherGraceDetailsFromAcmeerpPortal();
                        //-------------------------------------------------------------------------------------

                        Application.Restart();
                    }
                }

            }
            //File.Copy(LicenceKey, TargetFileName, true);
            //if (File.Exists(LicenceKey))
            //{
            //    isLicenceKeyUpdated = true;
            //    MessageBox.Show("License Updated", ACPERP_Title, MessageBoxButtons.OK);
            //    this.DialogResult = DialogResult.OK;
            //    this.Close();
            //}
        }

        private void btnVoucherUpload_Click(object sender, EventArgs e)
        {
            if (!this.IsLicenseKeyMismatchedByLicenseKeyDBLocation())
            {
                this.Close();
                //frmExportBranchOfficeVouchers exportVouchers = new frmExportBranchOfficeVouchers(ExportMode.Online);
                frmExportBranchOfficeVouchers exportVouchers = new frmExportBranchOfficeVouchers(VoucherExportMode);
                exportVouchers.Owner = this;
                exportVouchers.ShowDialog();
            }
            else
            {
                this.ShowMessageBox("License key Location(s) and Database Location are mismatching, Check your License Key");
            }
        }

        private void frmPortalUpdates_Load(object sender, EventArgs e)
        {
            LicenceKeyTarget = Installedpath();
            LoadDefaults();
        }

        public string Installedpath()
        {
            //ACPERP_TARGET_PATH = @"C:\Program Files (x86)\BoscoSoft\AcMEERP\";
            //if (!File.Exists(Path.Combine(ACPERP_TARGET_PATH, "ACPP.exe")))
            //    ACPERP_TARGET_PATH = @"C:\Program Files\BoscoSoft\AcMEERP\";
            return Application.StartupPath.ToString();
        }

        public void LoadDefaults()
        {
            ShowOfflineControlsinImportLicense();
            ShowofflinecontrolsinImportMasters();
            ShowOfflineControlsinImportTDSMasters();
            if (File.Exists(ACPERP_LICENSE_PATH))
            {
                lblBOCode.Text = (!string.IsNullOrEmpty(this.AppSetting.PartBranchOfficeCode)) ? this.AppSetting.PartBranchOfficeCode.ToUpper() : " ";
                lblHOCode.Text = (!string.IsNullOrEmpty(this.AppSetting.HeadofficeCode)) ? this.AppSetting.HeadofficeCode.ToUpper() : " ";
                lblBName.Text = (!string.IsNullOrEmpty(this.AppSetting.InstituteName)) ? this.AppSetting.InstituteName.ToUpper() : " ";
                lblHeadOfficeName.Text = (!string.IsNullOrEmpty(this.AppSetting.HeadOfficeName)) ? this.AppSetting.HeadOfficeName.ToUpper() : " ";
            }
            else
            {
                //this.ShowMessageBox("License file is not updated.");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.LICENSE_FILE_NOT_UPDATE));
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rgUpdatesMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rgUpdatesMode.SelectedIndex == 0)
            {
                VoucherExportMode = ExportMode.Offline;
                ShowOfflineControlsinImportLicense();
                ShowofflinecontrolsinImportMasters();
                ShowOfflineControlsinImportTDSMasters();
                //btnMasterDownload.Text = "Import";
                //btnVoucherUpload.Text = "Export";
                //btnTDSImport.Text = "Import";
                btnMasterDownload.Text = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.IMPORT_CAPTION);
                btnVoucherUpload.Text = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.EXPORT_CAPTION);
                btnTDSImport.Text = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.IMPORT_CAPTION);
            }
            else
            {
                //btnMasterDownload.Text = "Update";
                //btnVoucherUpload.Text = "Upload";
                //btnTDSImport.Text = "Update";
                btnMasterDownload.Text = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.UPDATE_CAPTION);
                btnVoucherUpload.Text = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.UPLOAD_CAPTION);
                btnTDSImport.Text = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.UPDATE_CAPTION);
                VoucherExportMode = ExportMode.Online;
                HideOfflineControlsinImportLicense();
                HideOfflineControlsinImportMasters();
                HideOfflineControlsinImportTDSMasters();
            }
        }
        #endregion

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openLicenceKey.Filter = "XML Files (.xml)|*.xml";
            //  openLicenceKey.ShowDialog();
            if (openLicenceKey.ShowDialog() == DialogResult.OK)
            {
                LicenceKey = openLicenceKey.FileName;
                txtLicenceKey.Text = LicenceKey;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                AcMELog.WriteLog("Welcome to Acme.erp Data Sync...");
                OpenFileDialog opendialog = new OpenFileDialog();
                opendialog.Filter = "XML Files (.xml)|*.xml";
                if (opendialog.ShowDialog() == DialogResult.OK)
                {
                    FileName = opendialog.FileName;
                    txtPath.Text = FileName;
                    if (!string.IsNullOrEmpty(opendialog.FileName))
                    {
                        btnMasterDownload.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                AcMELog.WriteLog(ex.Message);
            }
            finally { }
        }

        private void txtLicenceKey_TextChanged(object sender, EventArgs e)
        {
            ValidateLicenceKey();
        }

        private void btnTDSBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                AcMELog.WriteLog("Welcome to Acme.erp Data Sync...");
                OpenFileDialog opendialog = new OpenFileDialog();
                opendialog.Filter = "XML Files (.xml)|*.xml";
                if (opendialog.ShowDialog() == DialogResult.OK)
                {
                    FileName = opendialog.FileName;
                    txtTDSMastersPath.Text = FileName;
                    if (!string.IsNullOrEmpty(opendialog.FileName))
                    {
                        btnTDSImport.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                AcMELog.WriteLog(ex.Message);
            }
            finally { }
        }

        private void btnTDSImport_Click(object sender, EventArgs e)
        {
            resultArgs = new ResultArgs();
            try
            {
                if (!this.IsLicenseKeyMismatchedByLicenseKeyDBLocation())
                {
                    if (VoucherExportMode == ExportMode.Offline)
                    {
                        //frmImportHeadofficeMasters frmMasters = new frmImportHeadofficeMasters();
                        //frmMasters.Owner = this;
                        //frmMasters.ShowDialog();
                        DataSet dsTDSMasters = null;
                        if (isValidInput())
                        {
                            using (ImportMasterSystem objMasters = new ImportMasterSystem())
                            {
                                resultArgs = XMLConverter.ConvertXMLToDataSetWithResultArgs(FileName);
                                if (resultArgs.Success)
                                {
                                    dsTDSMasters = resultArgs.DataSource.TableSet;
                                }
                                if (dsTDSMasters != null && dsTDSMasters.Tables.Count > 0 && resultArgs != null && resultArgs.Success)
                                {
                                    //frmMapHeadOfficeLedgers frmMapMismatchedLedger = new frmMapHeadOfficeLedgers();
                                    //frmMapHeadOfficeProjects frmMappedheadOfficeprojects = new frmMapHeadOfficeProjects();
                                    //this.ShowWaitDialog("Updating Masters in Branch Office..");
                                    this.ShowWaitDialog(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.UPDATING_MASTER_BO));
                                    resultArgs = objMasters.ImportTDSMasters(dsTDSMasters);
                                    this.CloseWaitDialog();
                                }
                            }
                        }
                    }
                    else
                    {
                        //this.ShowWaitDialog("Connecting to Acme.erp Portal..");
                        this.ShowWaitDialog(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.CONNECTING_ACMEERP_PORTAL));
                        if (this.CheckForInternetConnectionhttp())
                        {
                            CloseWaitDialog();
                            if (!string.IsNullOrEmpty(this.AppSetting.BranchOfficeCode) && !string.IsNullOrEmpty(this.AppSetting.HeadofficeCode))
                            {
                                Application.DoEvents();
                                //this.ShowWaitDialog("Fetching Masters from Portal..");
                                this.ShowWaitDialog(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.FETCHING_MASTER_FROM_PORTAL));
                                Application.DoEvents();
                                DataSyncService.DataSynchronizerClient dataclientService = new DataSyncService.DataSynchronizerClient();
                                DataSet dsMaster = dataclientService.GetTDSMasterDetails(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode);
                                this.CloseWaitDialog();
                                if (dsMaster != null && dsMaster.Tables.Count > 0)
                                {
                                    using (ImportMasterSystem objMasters = new ImportMasterSystem())
                                    {
                                        resultArgs.Success = true;
                                        //frmMapHeadOfficeLedgers frmMapMismatchedLedger = new frmMapHeadOfficeLedgers();
                                        //frmMapHeadOfficeProjects frmMappedheadOfficeprojects = new frmMapHeadOfficeProjects();
                                        //this.ShowWaitDialog("Updating Masters in Branch Office..");
                                        this.ShowWaitDialog(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.UPDATING_MASTER_HO_INFO));
                                        resultArgs = objMasters.ImportTDSMasters(dsMaster);
                                        this.CloseWaitDialog();
                                    }
                                }
                                else
                                {
                                    //resultArgs.Message = "Dataset is Empty. It does not contain master details.";
                                    resultArgs.Message = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.DATASET_EMPTY_DOES_NOT_MASTER_DETAILS);
                                }
                            }
                        }
                        else
                        {
                            CloseWaitDialog();
                            //resultArgs.Message = "Unable to reach Portal. Please check your internet connection.";
                            resultArgs.Message = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.UNABLE_TO_REACH_PORTAL_CHECK_INTERNET_CONNECTION);
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox("License key Location(s) and Database Location are mismatching, Check your License Key");
                }
            }
            catch (FaultException<DataSyncService.AcMeServiceException> ex)
            {
                CloseWaitDialog();
                resultArgs.Message = ex.Detail.Message;
            }
            catch (FaultException ex)
            {
                CloseWaitDialog();
                resultArgs.Message = ex.ToString();
            }
            catch (CommunicationException ex)
            {
                CloseWaitDialog();
                //resultArgs.Message = "Unable to reach Portal. Please check your internet connection.";
                resultArgs.Message = this.GetMessage(MessageCatalog.Master.FinanceDataSynch.UNABLE_TO_REACH_PORTAL_CHECK_INTERNET_CONNECTION);
                AcMELog.WriteLog(ex.Message);
            }
            catch (Exception ex)
            {
                CloseWaitDialog();

                AcMELog.WriteLog(ex.ToString());
            }

            if (resultArgs.Success)
            {
                //XtraMessageBox.Show("Head Office Masters imported Successfully.", this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.IMPORT_HO_MASTERS_SUCCESS_INFO), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (!string.IsNullOrEmpty(resultArgs.Message))
                {
                    CloseWaitDialog();
                    XtraMessageBox.Show(resultArgs.Message, this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //else
                //{
                //    XtraMessageBox.Show("Failure in Importing Master Details.", MessageCatalog.DataSynchronization.Common.ACMEERP, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
            }
        }
    }
}
