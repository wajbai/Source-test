using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using Bosco.Utility;
using System.Reflection;
using Bosco.Utility.ConfigSetting;
using DevExpress.XtraEditors;

namespace ACPP.Modules.Data_Utility
{
    public partial class frmUpdateLicence : frmFinanceBaseAdd
    {
        bool isLicenceKeyUpdated = false;
        string LicenceKey = string.Empty;
        string LicenceKeyTarget = string.Empty;
        SimpleEncrypt.SimpleEncDec objdec = new SimpleEncrypt.SimpleEncDec();
        public static string ACPERP_Title = "AcME ERP";
        public static string ACPERP_LICENSE_KEY = "AcMEERPLicense.xml";
        public static string ACPERP_GENERAL_LOG = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "AcppGeneralLog.txt");
        SettingProperty AppSetting = new SettingProperty();
        private string targetpath = "";
        public string ACPERP_TARGET_PATH
        {
            set { targetpath = value; }
            get { return targetpath; }
        }
        //private string setuppath = "";
        //public string ACPERP_SETUP_PATH
        //{
        //    set { setuppath = value; }
        //    get { return Path.Combine(setuppath, "AcMEERPLicense.xml"); }
        //}
        public frmUpdateLicence()
        {
            InitializeComponent();
        }

        private void frmUpdateLicence_Load(object sender, EventArgs e)
        {
            //if (File.Exists(LicenceKey))
            //{
            //    txtLicenceKey.Text = LicenceKey;
            //}
            LicenceKey = "";
            LicenceKeyTarget = Installedpath();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ResultArgs resultArgs = new ResultArgs();
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (LicenceKey.Trim() != string.Empty)
                {
                    string TargetFileName = Path.Combine(LicenceKeyTarget, ACPERP_LICENSE_KEY);
                    resultArgs = UpdateLicenseFile(LicenceKey, TargetFileName);
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
                            File.Delete(SettingProperty.ActiveDatabaseLicenseKeypath);
                        }
                        //XtraMessageBox.Show("License Key is Updated", ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.DataUtilityForms.LICENSE_KEY_UPDATE), ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
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
                else
                {
                    //XtraMessageBox.Show("Select License Key", ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.DataUtilityForms.LICENSE_KEY_SELECT), ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception err)
            {
                WriteLog("Error in updating LicenseKey " + err.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (!isLicenceKeyUpdated)
            {
                //if (XtraMessageBox.Show("License Key is not yet updated, Do you want to Close ?", ACPERP_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                if (XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.DataUtilityForms.LICENSE_KEY_NOT_UPDATE_CLOSE), ACPERP_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    //SettingProperty.Is_Application_Logout = true;
                    //Application.Exit();
                    this.Close();
                }
            }
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            openLicenceKey.Filter = "XML Files (.xml)|*.xml";
            //  openLicenceKey.ShowDialog();
            if (openLicenceKey.ShowDialog() == DialogResult.OK)
            {
                LicenceKey = openLicenceKey.FileName;
                txtLicenceKey.Text = LicenceKey;
            }
        }

        private bool ValidateLicenceKey()
        {
            bool Rtn = false;

            try
            {
                if (File.Exists(LicenceKey))
                {
                    lblBarachCodeValue.Text = GetLicBranchCode();
                    lblBrachNameValue.Text = GetLicInstituteName();
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
            string BranchOfficeCode = string.Empty;
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

        public string GetLicInstituteName()
        {
            DataTable dtLicenseInfo = new DataTable();
            string HeadOfficeCode = string.Empty;
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

        private void txtLicenceKey_TextChanged(object sender, EventArgs e)
        {
            ValidateLicenceKey();
        }

        public string Installedpath()
        {
            //ACPERP_TARGET_PATH = @"C:\Program Files (x86)\BoscoSoft\AcMEERP\";
            //if (!File.Exists(Path.Combine(ACPERP_TARGET_PATH, "ACPP.exe")))
            //    ACPERP_TARGET_PATH = @"C:\Program Files\BoscoSoft\AcMEERP\";
            ACPERP_TARGET_PATH = Application.StartupPath.ToString();
            return ACPERP_TARGET_PATH;
        }
        private ResultArgs UpdateLicenseFile(string SourceFilePath, string DestinationFilePath)
        {
            ResultArgs resultArgs = new ResultArgs();
            string DestFileName = string.Empty;
            string SourceLicenseData = string.Empty;
            try
            {
                SourceLicenseData = XMLConverter.ReadFromXMLFile(SourceFilePath);
                DestFileName = (AppSetting.AccesstoMultiDB == (int)YesNo.Yes) && SettingProperty.ActiveDatabaseName.Trim() != "acperp" ?
                    Path.Combine(LicenceKeyTarget, "AcMEERP" + DateTime.Now.Ticks.ToString() + ".xml") : DestinationFilePath;
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

        #region Write Log
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

        public static void ClearLog()
        {
            try
            {
                string logpath = ACPERP_GENERAL_LOG;
                if (File.Exists(logpath))
                {
                    using (var file = new FileStream(logpath, FileMode.Truncate))
                    {
                        file.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in clearing log " + ex.Message);
            }
        }
        #endregion
    }
}
