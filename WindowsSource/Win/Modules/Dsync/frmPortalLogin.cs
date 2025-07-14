using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Schema;
using System.IO;
using Bosco.Model.UIModel.Master;
using System.ServiceModel;
using Bosco.Utility.ConfigSetting;
using System.Reflection;

namespace ACPP.Modules.Dsync
{
    public partial class frmPortalLogin : frmFinanceBaseAdd
    {
        #region Variables
        public static string ACPERP_LICENSE_PATH = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "AcMEERPLicense.xml");

        ResultArgs resultArgs = null;
        bool isLicenceKeyUpdated = false;
        string LicenceKey = string.Empty;
        string LicenceKeyTarget = string.Empty;
        SimpleEncrypt.SimpleEncDec objdec = new SimpleEncrypt.SimpleEncDec();
        public static string ACPERP_Title = "AcME ERP";
        public static string ACPERP_LICENSE_KEY = "AcMEERPLicense.xml";
        public static string ACPERP_GENERAL_LOG = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "AcppGeneralLog.txt");
        SettingProperty AppSetting = new SettingProperty();
        private string targetpath = "";


        #endregion

        #region Constructors
        public frmPortalLogin()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        #endregion

        #region Events
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidLogin())
                {
                    this.ShowWaitDialog("Connecting");
                    if (this.CheckForInternetConnection())
                    {
                        CloseWaitDialog();
                        DataTable dtBranch = new DataTable();
                        DataSyncService.DataSynchronizerClient dataClient = new DataSyncService.DataSynchronizerClient();
                        dtBranch = dataClient.GetBranchDetailsByCredentials(txtUserName.Text, txtPassword.Text);
                        if (dtBranch != null && dtBranch.Rows.Count > 0)
                        {
                            //if (File.Exists(ACPERP_LICENSE_PATH))
                            //{
                            //    File.Delete(ACPERP_LICENSE_PATH);
                            //}
                            //dtBranch.WriteXml(ACPERP_LICENSE_PATH);
                            //using (LegalEntitySystem legalEntitySystem = new LegalEntitySystem())
                            //{

                            //    ResultArgs result = legalEntitySystem.ApplyLicensePeriod(ACPERP_LICENSE_PATH);
                            //    if (result.Success)
                            //    {
                            //        this.ShowMessageBox("License Updated Successfully");
                            //    }
                            //}
                            UpdateLicenseDetails(dtBranch);
                        }
                    }
                    else
                    {
                        CloseWaitDialog();
                        //this.ShowMessageBox("Internet Connection is not  available.");
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.PORTAL_LOGIN_INTERNET_CONNECTION_NOT_AVAIL_INFO));
                    }
                }

            }
            catch (FaultException<DataSyncService.AcMeServiceException> ex)
            {
                this.ShowMessageBoxError(ex.Detail.Message);
            }
            catch (Exception ex)
            {
                this.ShowMessageBoxError(ex.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtUserName);
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtPassword);
        }
        #endregion

        #region Methods
        public bool IsValidLogin()
        {
            bool IsValid = true;
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                //this.ShowMessageBox("UserName is empty.");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.PORTAL_LOGIN_USERNAME_EMPTY));
                this.SetBorderColor(txtUserName);
                txtUserName.Focus();
                IsValid = false;
            }
            else if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                //this.ShowMessageBox("Password is empty.");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.PORTAL_LOGIN_PASSWORD_EMPTY));
                this.SetBorderColor(txtPassword);
                txtPassword.Focus();
                IsValid = false;
            }
            return IsValid;
        }
        #endregion

        #region License Methods

        private void UpdateLicenseDetails(DataTable dtOnlineLicenseDetails = null)
        {
            string TempPath = Path.Combine(Path.GetTempPath(), "AcmeLicense_" + DateTime.Now.Ticks.ToString() + ".xml"); // Temporary File Creation to write Online License Data
            string AcmeDefaultPath = Path.Combine(LicenceKeyTarget, ACPERP_LICENSE_KEY);
            if (dtOnlineLicenseDetails != null && dtOnlineLicenseDetails.Rows.Count > 0)
            {
                dtOnlineLicenseDetails.WriteXml(TempPath);
                LicenceKey = TempPath;
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
                    File.Delete(TempPath);
                }
                //XtraMessageBox.Show("License Key is Updated", ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.PORTAL_LOGIN_LICENSEKEY_UPDATE_INFO), this.GetMessage(MessageCatalog.Common.COMMON_ACMEERP_TITLE),MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

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
                    XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.PORTAL_LOGIN_LINCENSE_FILE_INVALID_INFO), this.GetMessage(MessageCatalog.Common.COMMON_ACMEERP_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        #endregion
    }
}