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
using AcMEDSync.Model;
using System.IO;
using System.Threading;
using AcMEDSync.View;
using DevExpress.XtraSplashScreen;
using System.Reflection;

namespace AcMEDSync.Forms
{
    public partial class frmExportBranchOfficeVouchers : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        ResultArgs resultArgs = new ResultArgs();
        CommonMember UtilityMember = new CommonMember();
        CommonMethod UtilityMethod = new CommonMethod();
        private const string DATASET_NAME = "Vouchers";
        DsyncSystemBase acMEDSyncSQL = new DsyncSystemBase();
        private string LICENSE_FILENAME = Path.Combine(Application.StartupPath.ToString(), "AcMEERPLicense.xml");
        //private string LICENSE_FILENAME = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "AcMEERPLicense.xml");
        //private string LICENSE_FILENAME = @"D:\ACPP\Win\Bin\AcMEERPLicense.xml";
        #endregion

        #region Properties
        private string ProjectId { get; set; }
        private DateTime dtDateFrom { get; set; }
        private DateTime dtDateTo { get; set; }
        #endregion

        #region Events

        public frmExportBranchOfficeVouchers()
        {
            InitializeComponent();
        }
       
        private void frmExportBranchOfficeVouchers_Load(object sender, EventArgs e)
        {
            LoadDefaults();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                AcMEDataSynLog.WriteLog("-----Exporting Branch office Voucher to Head Office Started----");
                SplashScreenManager.ShowForm(typeof(frmWait));
                SplashScreenManager.Default.SetWaitFormDescription("Exporting Branch Office Vouchers...");
                resultArgs.Message = string.Empty;
                resultArgs = ExportVoucherDetails();
                SplashScreenManager.CloseForm();
                if (!resultArgs.Success)
                {
                    if (!string.IsNullOrEmpty(resultArgs.Message))
                    {
                        XtraMessageBox.Show(resultArgs.Message, MessageCatalog.DataSynchronization.Common.ACMEERP, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        AcMEDSync.AcMEDataSynLog.WriteLog(resultArgs.Message);
                    }
                    else
                    {
                        XtraMessageBox.Show(acMEDSyncSQL.GetMessage(MessageCatalog.DataSynchronization.Export.EXPORT_VOUCHERS_FAILURE), MessageCatalog.DataSynchronization.Common.ACMEERP, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        AcMEDSync.AcMEDataSynLog.WriteLog(acMEDSyncSQL.GetMessage(MessageCatalog.DataSynchronization.Export.EXPORT_VOUCHERS_SUCCESS));
                    }
                }
                else
                {
                    XtraMessageBox.Show(acMEDSyncSQL.GetMessage(MessageCatalog.DataSynchronization.Export.EXPORT_VOUCHERS_SUCCESS), MessageCatalog.DataSynchronization.Common.ACMEERP, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AcMEDSync.AcMEDataSynLog.WriteLog(acMEDSyncSQL.GetMessage(MessageCatalog.DataSynchronization.Export.EXPORT_VOUCHERS_SUCCESS));
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            finally
            {
                AcMEDataSynLog.WriteLog("-----Exporting Branch office Voucher to Head Office Ended ----");
                //waitdialog.Close();
            }
        }

        private void deDateFrom_Leave(object sender, EventArgs e)
        {
            deDateTo.DateTime = deDateFrom.DateTime.AddMonths(1);
            deDateTo.DateTime = deDateTo.DateTime.AddDays(-1);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
                XtraMessageBox.Show(acMEDSyncSQL.GetMessage(MessageCatalog.DataSynchronization.Export.SELECT_ONE_PROJECT), "AcME ERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }
            else if (deDateTo.DateTime < deDateFrom.DateTime)
            {
                XtraMessageBox.Show(acMEDSyncSQL.GetMessage(MessageCatalog.DataSynchronization.Export.VALIDATE_DATE), "AcME ERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }
            //else if (!CheckHeadofficeLdgerExists())
            //{
            //    isValid = false;
            //}
            return isValid;
        }

        /// <summary>
        /// Loading the project details
        /// </summary>
        public void LoadProjects()
        {
            using (ExportVoucherSystem vouchersystem = new ExportVoucherSystem())
            {
                ResultArgs resultArgs = vouchersystem.FetchProjectsLookup();
                if (resultArgs.Success && resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    chklstProjects.DataSource = resultArgs.DataSource.Table;
                    chklstProjects.DisplayMember = "PROJECT";
                    chklstProjects.ValueMember = "PROJECT_ID";
                }
            }

        }
        /// <summary>
        /// Check wherther Headoffice Ledger Exists 
        /// </summary>
        public ResultArgs CheckHeadofficeLdgerExists()
        {
            string msg = string.Empty;
            int NoofUmmap = 0;
            using (ExportVoucherSystem vouchersystem = new ExportVoucherSystem())
            {
                vouchersystem.ProjectId = ProjectId;
                vouchersystem.DateFrom = dtDateFrom;
                vouchersystem.DateTo = dtDateTo;
                resultArgs = vouchersystem.CheckHOLedgerExists();
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    resultArgs.Success = false;
                    foreach (DataRow item in resultArgs.DataSource.Table.Rows)
                    {
                        msg += item["Ledger_Name"].ToString();
                        msg += Environment.NewLine;
                        NoofUmmap++;
                    }
                    XtraMessageBox.Show("The No of Unmapped Ledgers :" + NoofUmmap + "\n" + "The Unmapped Ledgers : \n" + msg + "\n");
                }
            }
            return resultArgs;
        }
        /// <summary>
        /// Loading the Default details
        /// </summary>
        public void LoadDefaults()
        {
            LoadProjects();
            deDateFrom.DateTime = DateTime.Now;
            deDateTo.DateTime = DateTime.Now.AddMonths(1);
            SetActiveTransactionPeriod();
        }

        private ResultArgs SetActiveTransactionPeriod()
        {
            DataTable dtPeriod = null;
            using (ExportVoucherSystem exportSystem = new ExportVoucherSystem())
            {
                resultArgs = exportSystem.FetchActiveTransactionPeriod();
                if (resultArgs.Success)
                {
                    dtPeriod = resultArgs.DataSource.Table;
                    if (dtPeriod != null && dtPeriod.Rows.Count >= 0)
                    {
                        deDateFrom.DateTime = exportSystem.DateSet.ToDate(dtPeriod.Rows[0][exportSystem.AppSchema.AccountingPeriod.YEAR_FROMColumn.ColumnName].ToString(), false);
                        //deDateTo.DateTime = exportSystem.DateSet.ToDate(dtPeriod.Rows[0][exportSystem.AppSchema.AccountingPeriod.YEAR_TOColumn.ColumnName].ToString(), false);
                        deDateTo.DateTime = deDateFrom.DateTime.AddMonths(1);
                        deDateTo.DateTime = deDateTo.DateTime.AddDays(-1);
                    }
                    else
                    {
                        deDateFrom.DateTime = DateTime.Now;
                        deDateTo.DateTime = DateTime.Now.AddMonths(1);
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs ExportVoucherDetails()
        {
            try
            {
                if (IsValidInput())
                {
                    string pid = string.Empty;
                    ProjectId = string.Empty;
                    foreach (DataRowView drProject in chklstProjects.CheckedItems)
                    {
                        pid += drProject[0].ToString();
                        pid += ',';
                    }
                    ProjectId = pid.TrimEnd(',');
                    dtDateFrom = UtilityMember.DateSet.ToDate(deDateFrom.DateTime.ToShortDateString(), false);
                    dtDateTo = UtilityMember.DateSet.ToDate(deDateTo.DateTime.ToShortDateString(), false);
                    DataSet dsTransaction = new DataSet(DATASET_NAME);
                    using (ExportVoucherSystem vouchersystem = new ExportVoucherSystem())
                    {
                        vouchersystem.ProjectId = ProjectId;
                        vouchersystem.DateFrom = dtDateFrom;
                        vouchersystem.DateTo = dtDateTo;
                        vouchersystem.HeadOfficeCode = GetLicHeadofficeCode();
                        vouchersystem.BranchOfficeCode = GetLicBranchCode();
                        resultArgs = CheckHeadofficeLdgerExists();
                        if (resultArgs.Success)
                        {
                            resultArgs = vouchersystem.FillExportVoucherTransaction();
                            if (resultArgs.Success)
                            {
                                dsTransaction = vouchersystem.dsTransaction;
                                if (dsTransaction.Tables.Count > 0)
                                {
                                    if (rgSynMode.SelectedIndex == (int)Bosco.Utility.Mode.Offline)
                                    {
                                        ExportXmlOffline(dsTransaction);
                                    }
                                    else
                                    {
                                        ExportXmlOnline(dsTransaction);
                                    }
                                }
                                //else
                                //{
                                //    XtraMessageBox.Show(acMEDSyncSQL.GetMessage(MessageCatalog.DataSynchronization.Export.NO_RECORD_EXISTS), acMEDSyncSQL.GetMessage(MessageCatalog.DataSynchronization.Common.ACMEERP), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //}
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            finally { }
            return resultArgs;
        }

        private ResultArgs ExportXmlOffline(DataSet dsTransaction)
        {
            resultArgs.Success = false;
            try
            {
                if (XtraMessageBox.Show("Vouchers Exported Successfully.Do you want to Save ?", "AcME ERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SaveFileDialog saveDialog = new SaveFileDialog();
                    saveDialog.Filter = "Xml Files|.xml";
                    saveDialog.Title = "Export Vouchers";
                    saveDialog.FileName = "AcME_ERP_Vouchers_" + DateTime.Now.Ticks.ToString() + ".xml";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        XMLConverter.WriteToXMLFile(dsTransaction, saveDialog.FileName);
                        resultArgs.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog(ex.Message);
            }
            finally { }
            return resultArgs;
        }

        private ResultArgs ExportXmlOnline(DataSet dsTransaction)
        {
            resultArgs.Success = false;
            try
            {
                byte[] bVouchers = CommonMethod.CompressData(dsTransaction);
                if (bVouchers != null)
                {
                    DataSyncService.DataSynchronizerClient dataSyncExportVouchers = new DataSyncService.DataSynchronizerClient();
                    resultArgs.Success = dataSyncExportVouchers.UploadVoucherCompressedDataset(bVouchers);
                }
                else
                {
                    AcMEDataSynLog.WriteLog("Problem in compressing dataset to Byte Array..");
                }
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog(ex.Message);
            }
            finally { }
            return resultArgs;
        }

        public string GetLicBranchCode()
        {
            DataTable dtLicenseInfo = new DataTable();
            string BranchOfficeCode = string.Empty;
            try
            {
                if (File.Exists(LICENSE_FILENAME))
                {
                    DataSet dsLicenseInfo = XMLConverter.ConvertXMLToDataSet(LICENSE_FILENAME);
                    if (dsLicenseInfo != null && dsLicenseInfo.Tables.Count > 0)
                    {
                        dtLicenseInfo = dsLicenseInfo.Tables["LicenseKey"];
                        if (dtLicenseInfo != null)
                        {
                            DataRow drLicense = dtLicenseInfo.Rows[0];
                            BranchOfficeCode = drLicense["BRANCH_OFFICE_CODE"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            finally { }
            return BranchOfficeCode;
        }

        public string GetLicHeadofficeCode()
        {
            DataTable dtLicenseInfo = new DataTable();
            string HeadOfficeCode = string.Empty;
            try
            {
                if (File.Exists(LICENSE_FILENAME))
                {
                    DataSet dsLicenseInfo = XMLConverter.ConvertXMLToDataSet(LICENSE_FILENAME);
                    if (dsLicenseInfo != null && dsLicenseInfo.Tables.Count > 0)
                    {
                        dtLicenseInfo = dsLicenseInfo.Tables["LicenseKey"];
                        if (dtLicenseInfo != null)
                        {
                            DataRow drLicense = dtLicenseInfo.Rows[0];
                            HeadOfficeCode = drLicense["HEAD_OFFICE_CODE"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            finally { }
            return HeadOfficeCode;
        }
        #endregion

    }
}