using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.DAO;
using Bosco.Utility;
using Bosco.Utility.ConfigSetting;
using System.IO;
using System.Reflection;
using DevExpress.XtraSplashScreen;
using Bosco.Model.UIModel.Master;
using Bosco.Model;
using Bosco.Model.Setting;

namespace ACPP.Modules.Data_Utility
{
    public partial class frmRestoreMultipleDB : frmFinanceBaseAdd
    {
        #region VariableDeclaration
        BackupAndRestore BackRestore = new BackupAndRestore();
        private string MultipleDBPath = SettingProperty.RestoreMultipleDBPath.ToString();
        string FilePath = string.Empty;
        string Readline = string.Empty;
        ResultArgs resultArgs = new ResultArgs();
        string LicenceKey = string.Empty;
        string LicenceKeyTarget = string.Empty;
        string MultiLicenseFileName = string.Empty;
        string ExistLicenseFileName = string.Empty;
        bool isDatabase;

        string SelectedDBAcmeerpBranchDetails = string.Empty;
        string SelectedDBHOCode = string.Empty;
        string SelectedDBBOCode = string.Empty;
        string SelectedDBBOLocation = string.Empty;
        Int32 SelectedDBBOActiveVouchers = -1;
        
        public static string ACPERP_LICENSE_KEY = "AcMEERPLicense.xml";
        #endregion

        #region Constructor

        public frmRestoreMultipleDB()
        {
            InitializeComponent();
        }
        #endregion

        #region Property
        public string filePathDBName { get; set; }
        #endregion

        #region Events
        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRestoreMultipleDB_Load(object sender, EventArgs e)
        {
            lblBranchDetails.Text = string.Empty;
            txtDatabaseName.Text = string.Empty;
            lciMultipleLicenseKey.Visibility = lciMultipleLicenseKeyBrowse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (this.AppSetting.AccesstoMultiDB == (int)YesNo.Yes)
            {
                lblCurrentDBName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lblNewDBNameData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.Height = 155;
                if (SettingProperty.EnableMultipleDBBrowseLicenseKey)
                {
                    lciMultipleLicenseKey.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    lciMultipleLicenseKeyBrowse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    this.Height = 185;
                }
                else
                {
                    lciMultipleLicenseKey.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lciMultipleLicenseKeyBrowse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }

                //07/02/2024, Handle allowed number of multi database, lock new restore db if it goes allowed multi db
                if (this.AppSetting.NoOfAllowedMultiDBs != 0)
                {
                    if (SettingProperty.dtLoginDB != null && SettingProperty.dtLoginDB.Rows.Count > this.AppSetting.NoOfAllowedMultiDBs)
                    {
                        rgOptionNewExist.SelectedIndex = 1;
                        rgOptionNewExist.Enabled = false;
                    }
                }
            }
            else
            {
                lblradio.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lblCurrentDBName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.Height = 135;
                txtDatabaseName.Enabled = false;
            }

        }
        /// <summary>
        /// Browse and Get the filePath
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog Openfile = new OpenFileDialog();
            Openfile.DefaultExt = "Sql And Acme files";
            Openfile.Filter = "Sql files (*.Sql)|*.Sql|Acme files (*.gz)|*.gz";
            Openfile.Title = "Restore";
            if (Openfile.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = FilePath = Openfile.FileName;
                if (Path.GetExtension(Openfile.FileName).Contains(".gz"))
                {
                    DirectoryInfo directorySelected = new DirectoryInfo(Openfile.FileName);
                    FilePath = BackRestore.DeCompress(directorySelected);
                    isDatabase = IsDatabaseSQL();
                }
                else
                {
                    isDatabase = IsDatabaseSQL();
                }

                if (isDatabase == false)
                {
                    if (this.AppSetting.AccesstoMultiDB == (int)YesNo.Yes)
                    {
                        txtDatabaseName.Text = filePathDBName;
                    }
                    else
                    {
                        txtDatabaseName.Text = "acperp";
                    }
                }
                else
                {
                    txtDatabaseName.Enabled = false;
                    txtDatabaseName.Text = "PROVIDE DataBase Name IN SQL PATH";
                }
            }
        }
        /// <summary>
        /// Restore Click Optiong
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (isValidInput())
            {
                RestoreDatabase();
            }
        }

        /// <summary>
        /// this is to Select the restoring Type 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rgOptionNewExist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rgOptionNewExist.SelectedIndex == 0)
            {
                lblNewDBNameData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lblNewDBName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lblCurrentDBName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                btnMultipleLicenseKey.Enabled = true;
                txtMultipleLicenseKey.Text = string.Empty;
            }
            else
            {
                lblCurrentDBName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                DataTable dtValue = SettingProperty.dtLoginDB;
                this.UtilityMember.ComboSet.BindGridLookUpCombo(glkExistingDB, dtValue, "Restore_Db", "Restore_Db");
                glkExistingDB.EditValue = glkExistingDB.Properties.GetDisplayValueByKeyValue(SettingProperty.ActiveDatabaseName);
                lblNewDBNameData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lblNewDBName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                glkExistingDB.Enabled = false;
                if (CheckLicenseKeyExists(SettingProperty.ActiveDatabaseName))
                {
                    btnMultipleLicenseKey.Enabled = false;
                    txtMultipleLicenseKey.Text = LicenceKey = Path.Combine(Application.StartupPath.ToString(), ExistLicenseFileName);
                }
                else
                {
                    btnMultipleLicenseKey.Enabled = true;
                }
            }
        }
        /// <summary>
        /// Close the Restore form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #endregion

        #region Methods
        /// <summary>
        /// This is to get the SQL DB from file Path
        /// </summary>
        /// <returns></returns>
        private bool IsDatabaseSQL()
        {
            bool isFirstLine = false;
            bool isDatabaseName = true;
            Assembly abyAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            using (StreamReader sr = new StreamReader(FilePath))
            {
                if (this.AppSetting.AccesstoMultiDB == (int)YesNo.Yes)
                {
                    while ((Readline = sr.ReadLine()) != null)
                    {
                        //On 24/08/2020, To get first line of acmeerp branch detials from selected db-------------
                        if (!isFirstLine)
                        {
                            SelectedDBAcmeerpBranchDetails = Readline;
                            isFirstLine = true;
                        }
                        //-----------------------------------------------------------------------------------------

                        if (isDatabaseName)
                        {
                            if (Readline.Contains("CREATE DATABASE"))
                            {
                                isDatabaseName = false;
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(Readline))
                            {
                                if (Readline.Contains('`'))
                                {
                                    string[] value = new string[2];
                                    string dbName = Readline.Substring(4, Readline.Length - 4);
                                    value[0] = Readline.Substring(0, 3);
                                    value[1] = dbName;
                                    if (value.Count() == 2)
                                    {
                                        filePathDBName = value[1];
                                        filePathDBName = filePathDBName.Trim('`').TrimEnd(';');
                                        filePathDBName = filePathDBName.Trim('`');
                                    }
                                    if (value.Count() > 2)
                                    {
                                        for (int i = 1; i <= value.Count() - 1; i++)
                                        {
                                            filePathDBName = filePathDBName + value[i].ToString() + " ";
                                        }
                                        filePathDBName = filePathDBName.TrimEnd(' ');
                                        filePathDBName = filePathDBName.Trim('`').TrimEnd(';');
                                        filePathDBName = filePathDBName.Trim('`');
                                    }

                                }
                                else
                                {
                                    string[] value = Readline.Split(' ');
                                    filePathDBName = value[1];
                                    filePathDBName = filePathDBName.Trim(';');
                                }
                                break;
                            }
                        }
                    }
                }
                else
                {
                    while ((Readline = sr.ReadLine()) != null)
                    {
                        //On 24/08/2020, To get first line of acmeerp branch detials from selected db-------------
                        if (!isFirstLine)
                        {
                            SelectedDBAcmeerpBranchDetails = Readline;
                            isFirstLine = true;
                        }
                        //-----------------------------------------------------------------------------------------

                        if (isDatabaseName)
                        {
                            if (Readline.Contains("CREATE DATABASE"))
                            {
                                isDatabaseName = false;
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(Readline))
                            {
                                if (Readline.Contains('`'))
                                {
                                    string[] value = Readline.Split(' ');

                                    filePathDBName = value[1];
                                    if (value.Count() > 2)
                                    {
                                        filePathDBName = string.Empty;
                                        for (int i = 1; i <= value.Count() - 1; i++)
                                        {
                                            filePathDBName = filePathDBName + value[i].ToString() + " ";
                                        }
                                        filePathDBName = filePathDBName.TrimEnd(' ');
                                    }
                                    filePathDBName = filePathDBName.Trim('`').TrimEnd(';');
                                    filePathDBName = filePathDBName.Trim('`');
                                }
                                else
                                {
                                    string[] value = Readline.Split(' ');
                                    filePathDBName = value[1];
                                    filePathDBName = filePathDBName.Trim(';');
                                }
                                break;
                            }
                        }
                    }
                }

                //On 24/08/2020
                ValidateAndAssignSelectedDBDetails();
            }
            return isDatabaseName;
        }

       
        /// <summary>
        /// Validate the Fields
        /// </summary>
        /// <returns></returns>
        private bool isValidInput()
        {
            bool isValid = true;
            bool isMismatchingLicenseKey = false;
            if (string.IsNullOrEmpty(FilePath))
            {
                //this.ShowMessageBox("Select the Database to restore.");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.DataUtilityForms.SELECT_DB_TO_RESTORE));
                isValid = false;
            }
            else if (string.IsNullOrEmpty(txtDatabaseName.Text))
            {
                //this.ShowMessageBox("Provide the database name to restore.");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.DataUtilityForms.PROVIDE_DB_NAME_RESTORE));
                isValid = false;
            }
            else if (string.IsNullOrEmpty(txtMultipleLicenseKey.Text) && this.AppSetting.AccesstoMultiDB == (int)YesNo.Yes && SettingProperty.EnableMultipleDBBrowseLicenseKey == true)
            {
                //this.ShowMessageBox("Select License Key for the Database");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.DataUtilityForms.SELECT_LICENSE_KEY_FOR_DB));
                isValid = false;
            }


            //On 24/08/2020, To check selected back db branch details with active license key detials
            if (isValid)
            {
                string msg = string.Empty;
                if (!string.IsNullOrEmpty(SelectedDBHOCode) && !string.IsNullOrEmpty(SelectedDBHOCode))
                {
                    if (!string.IsNullOrEmpty(txtMultipleLicenseKey.Text) && this.AppSetting.AccesstoMultiDB == (int)YesNo.Yes && SettingProperty.EnableMultipleDBBrowseLicenseKey == true)
                    {    //For Multi key - selected License key
                        string newHOCode = GetLicHOCode();
                        string newBOCode = GetLicBranchCode();

                        if (newHOCode.ToUpper() != SelectedDBHOCode.ToUpper() ||
                            newBOCode.ToUpper() != SelectedDBBOCode.ToUpper())
                        {
                            msg = "Mismatching selected License key with selected Acmeerp Database Backup file";
                            isValid = false;
                            isMismatchingLicenseKey = true;
                        }
                    }
                    else //For General
                    {
                        if (AppSetting.HeadofficeCode.ToUpper() != SelectedDBHOCode.ToUpper() ||
                            AppSetting.PartBranchOfficeCode.ToUpper() != SelectedDBBOCode.ToUpper())
                        {
                            msg = "Mismatching License key with selected Acmeerp Database Backup file";
                            isValid = false;
                            isMismatchingLicenseKey = true;
                        }
                    }
                }

                if (!isValid)
                {
                    //On 08/07/2024, If sdbinm, if license key is mismatching, let us lock to restore
                    if (SelectedDBHOCode.ToUpper()!="SDBINM")
                    {
                        if (ShowConfirmationMessage(msg + ", Are you sure to Proceed ? ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            isValid = true;
                        }
                    }
                    else if (isMismatchingLicenseKey)
                    {
                        this.ShowMessageBox("Mismatching License key with selected Acmeerp Database Backup file, You can't Restore.");
                    }
                }

                //On 26/11/2024 To validate active vouchers with backup and current database
                if (isValid && SelectedDBBOActiveVouchers>=0)
                {
                    Int32 noofvouchersActiveDB = this.GetNoActiveVouchers();
                    if (noofvouchersActiveDB != SelectedDBBOActiveVouchers)
                    {
                        msg = "Voucher(s) are mismatching with selected Database backup file.\n\rSelected Database backup file have (" + SelectedDBBOActiveVouchers.ToString() + ") Voucher(s) " +
                                "but active database have (" + noofvouchersActiveDB.ToString() + ") Voucher(s), Are you sure to Proceed ? ";

                        if (ShowConfirmationMessage(msg, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            isValid = true;
                        }
                        else
                        {
                            isValid = false;
                        }
                    }
                }

                //On 30/04/2021, If selected xml has valud BO and HO details but that is not license key
                if (isValid && this.AppSetting.AccesstoMultiDB == (int)YesNo.Yes)
                {
                    isValid  = this.ValidateLiceseKey(txtMultipleLicenseKey.Text);
                    if (!isValid)
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.LICENSE_FILE_INVALID));
                    }
                }
            }

            return isValid;
        }

        /// <summary>
        /// Restore functionality to do all the Operation
        /// </summary>
        private void RestoreDatabase()
        {
            bool ValidateDatabaseMysql = true;
            Int32 noofvouchersActiveDB = 0;

            //On 04/09/2019, to check corrupted db or not
            if (!BackRestore.IsDBCorrupted())
            {
                // this is to get the filepath DB Name
                bool isvalidateDBExistSQLPath = IsDatabaseSQL();
                ResultArgs result = new ResultArgs();
                if (isvalidateDBExistSQLPath == false)
                {
                    noofvouchersActiveDB = this.GetNoActiveVouchers();
                    BackRestore.No_of_Active_Vouchers = noofvouchersActiveDB;
                    if (this.AppSetting.AccesstoMultiDB == (int)YesNo.Yes)
                    {
                        if (rgOptionNewExist.SelectedIndex == 0)
                        {
                            lblCurrentDBName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                            ValidateDatabaseMysql = BackRestore.isACPERPDatabase(txtDatabaseName.Text);
                        }
                        else
                        {
                            lblNewDBNameData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                            ValidateDatabaseMysql = BackRestore.isACPERPDatabase(glkExistingDB.EditValue.ToString());
                        }
                    }
                    else
                    {
                        ValidateDatabaseMysql = BackRestore.isACPERPDatabase(txtDatabaseName.Text);
                    }
                    if (ValidateDatabaseMysql == true)
                    {
                        //this.ShowWaitDialog("Restoring");
                        this.ShowWaitDialog(this.GetMessage(MessageCatalog.Master.DataUtilityForms.RESTORING_INFO));
                        //On 23/05/2020, to call new Restore logic
                        //result = BackRestore.RestoreDB(FilePath, txtDatabaseName.Text, filePathDBName);
                        result = BackRestore.RestoreExistDBNew(FilePath, txtDatabaseName.Text, filePathDBName);
                        this.CloseWaitDialog();
                    }
                    else
                    {
                        //if (XtraMessageBox.Show("Database is available Do you want to Replace Database in Currrent DB", "ACPERP", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        if (XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.DataUtilityForms.CONFIRMATION_FOR_DB_REPLACE), "ACPERP", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            //this.ShowWaitDialog("Restoring");
                            this.ShowWaitDialog(this.GetMessage(MessageCatalog.Master.DataUtilityForms.RESTORING_INFO));

                            if (this.AppSetting.AccesstoMultiDB == (int)YesNo.Yes)
                            {
                                if (rgOptionNewExist.SelectedIndex == 0)
                                {
                                    //On 23/05/2020, to call new Restore logic
                                    //result = BackRestore.RestoreExistDB(FilePath, txtDatabaseName.Text, filePathDBName);
                                    result = BackRestore.RestoreExistDBNew(FilePath, txtDatabaseName.Text, filePathDBName);
                                }
                                else
                                {
                                    //On 23/05/2020, to call new Restore logic
                                    //result = BackRestore.RestoreExistDB(FilePath, glkExistingDB.EditValue.ToString(), filePathDBName);
                                    result = BackRestore.RestoreExistDBNew(FilePath, glkExistingDB.EditValue.ToString(), filePathDBName);
                                }
                            }
                            else
                            {
                                //On 23/05/2020, to call new Restore logic
                                //result = BackRestore.RestoreExistDB(FilePath, txtDatabaseName.Text, filePathDBName);
                                result = BackRestore.RestoreExistDBNew(FilePath, txtDatabaseName.Text, filePathDBName);
                            }
                            this.CloseWaitDialog();
                        }
                    }
                }
                else
                {
                    //XtraMessageBox.Show("Selected File is Invalid", "ACPERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.DataUtilityForms.SELECTED_FILE_INVALID), "ACPERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (result.Success)
                {
                    if (SettingProperty.EnableMultipleDBBrowseLicenseKey && this.AppSetting.AccesstoMultiDB == (int)YesNo.Yes)
                    {
                        if (rgOptionNewExist.SelectedIndex == 0)
                        {

                            UpdateMultipleDBLicenseKey();
                        }
                    }
                    else if (SettingProperty.EnableMultipleDBBrowseLicenseKey == false && this.AppSetting.AccesstoMultiDB == (int)YesNo.Yes && rgOptionNewExist.SelectedIndex == 0)
                    {
                        UpdateRestoreDb(txtDatabaseName.Text);
                    }

                    //On 25/11/2024, To update latest resoted db on -------------------------------------------------------------------------------
                    using (UISetting uisetting = new UISetting())
                    {
                        //1. Get Server Date
                        using (AcMEERPFTP acmeerpftp = new AcMEERPFTP())
                        {
                            resultArgs = acmeerpftp.GetAcMEERPServerDateTime();
                        }
                        if (resultArgs.Success)
                        {
                            string serverdatetime = resultArgs.ReturnValue.ToString();
                            resultArgs = uisetting.SaveSettingDetails(Setting.DBRestoredOn.ToString(), serverdatetime, this.ADMIN_USER_DEFAULT_ID);
                            if (resultArgs.Success)
                            {
                                string restoredremarks  = noofvouchersActiveDB.ToString() + "-" + SelectedDBBOActiveVouchers.ToString();
                                resultArgs = uisetting.SaveSettingDetails(Setting.DBRestoredRemarks.ToString(), restoredremarks, this.ADMIN_USER_DEFAULT_ID);
                                AcMELog.WriteLog("Database restoed on " + serverdatetime);
                            }
                        }
                    }
                    //--------------------------------------------------------------------------------------------------------------------------------

                    //this.ShowMessageBox("Restored successfully" + Environment.NewLine + "The File " + FilePath);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.DataUtilityForms.DB_RESORE_SUCCESS) + Environment.NewLine + this.GetMessage(MessageCatalog.Master.DataUtilityForms.DB_THE_FILE) + FilePath);
                    SettingProperty.Is_Application_Logout = true;
                    Application.Restart();
                }
                else
                {
                    if (!string.IsNullOrEmpty(result.Message))
                    {
                        this.ShowMessageBox(result.Message);
                    }
                }
            }
            else
            {
                MessageRender.ShowMessage(BackupAndRestore.RESTORE_CORRUPTION_MESSAGE);
            }
        }

        /// <summary>
        /// Get the Restore DB Name
        /// </summary>
        /// <param name="DbName"></param>
        /// <returns></returns>
        //private bool UpdateRestoreDb(string DbName)
        //{
        //    bool IsSuccess = false;
        //    try
        //    {
        //        using (DashBoardSystem dashboardsystem = new DashBoardSystem())
        //        {
        //            resultArgs = dashboardsystem.InsertRestoredDatabase(DbName);
        //            if (resultArgs.Success)
        //            {
        //                IsSuccess = true;
        //            }

        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        MessageRender.ShowMessage(Ex.Message);
        //    }
        //    finally { }

        //    return IsSuccess;
        //    //if (DbName != string.Empty)
        //    //{
        //    //    //string filePath = Application.StartupPath + "\\MultipleDB.xml";
        //    //    DataSet dsRestore = new DataSet();
        //    //    DataTable dt = new DataTable();
        //    //    dsRestore = XMLConverter.ConvertXMLToDataSet(MultipleDBPath);
        //    //    DataView dvDb = new DataView();
        //    //    dvDb = dsRestore.Tables[0].AsDataView();
        //    //    dvDb.RowFilter = "Restore_Db= '" + DbName + "'";
        //    //    if (dvDb.Count == 0)
        //    //    {
        //    //        dsRestore.Tables[0].Rows.Add(DbName);
        //    //    }
        //    //    XMLConverter.WriteToXMLFile(dsRestore, MultipleDBPath);
        //    //}
        //}

        private bool UpdateRestoreDb(string DbName)
        {
            bool IsSuccess = false;
            try
            {
                if (DbName != string.Empty)
                {
                    DataSet dsRestore = new DataSet();
                    DataTable dt = new DataTable();
                    dsRestore = XMLConverter.ConvertXMLToDataSet(MultipleDBPath);
                    DataView dvDb = new DataView();
                    dvDb = dsRestore.Tables[0].AsDataView();
                    dvDb.RowFilter = "Restore_Db= '" + DbName + "'";
                    if (dvDb.Count == 0)
                    {
                        dsRestore.Tables[0].Rows.Add(DbName);
                    }
                    XMLConverter.WriteToXMLFile(dsRestore, MultipleDBPath);


                    //On 06/05/2024, Update Multi DB xml file into database
                    using (SettingSystem settingsys = new SettingSystem())
                    {
                        settingsys.UpdateMultiDBXMLConfigurationInAcperp();
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

        //private bool UpdateRestoreDb(string DbName, string MultiLicenseKey)
        //{
        //    bool IsSuccess = false;
        //    try
        //    {
        //        if (DbName != string.Empty)
        //        {
        //            DataSet dsRestore = new DataSet();
        //            DataTable dt = new DataTable();
        //            dsRestore = XMLConverter.ConvertXMLToDataSet(MultipleDBPath);
        //            if (!dsRestore.Tables[0].Columns.Contains("MultipleLicenseKey"))
        //            {
        //                dsRestore.Tables[0].Columns.Add("MultipleLicenseKey", typeof(string));
        //            }
        //            DataView dvDb = new DataView();
        //            dvDb = dsRestore.Tables[0].AsDataView();
        //            dvDb.RowFilter = "Restore_Db= '" + DbName + "'";
        //            if (dvDb.Count == 0)
        //            {
        //                dsRestore.Tables[0].Rows.Add(DbName, MultiLicenseFileName);
        //                XMLConverter.WriteToXMLFile(dsRestore, MultipleDBPath);
        //            }
        //            else
        //            {
        //                DataView dvfilter = dsRestore.Tables[0].AsDataView();
        //                dvfilter.RowFilter = "Restore_Db <> '" + DbName + "'";
        //                DataSet dsMultiDb = new DataSet();
        //                dsMultiDb.Tables.Add(dvfilter.ToTable());
        //                dsMultiDb.Tables[0].Rows.Add(DbName, MultiLicenseFileName);
        //                XMLConverter.WriteToXMLFile(dsMultiDb, MultipleDBPath);
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        MessageRender.ShowMessage(Ex.Message);
        //    }
        //    finally { }

        //    return IsSuccess;

        //}

        private void btnMultipleLicenseKey_Click(object sender, EventArgs e)
        {
            OpenFileDialog openLicenceKey = new OpenFileDialog();
            openLicenceKey.Filter = "XML Files (.xml)|*.xml";
            if (openLicenceKey.ShowDialog() == DialogResult.OK)
            {
                txtMultipleLicenseKey.Text = LicenceKey = openLicenceKey.FileName;
                //string[] Fname = LicenceKey.Split('\\');
                //// To Rename the user provided license file into new file name
                //foreach (string item in Fname)
                //{
                //    if (item.Contains(".xml"))
                //    {
                //        MultiLicenseFileName = "AcMEERP" + DateTime.Now.Ticks.ToString() + ".xml";
                //    }

                //}
                //string myTempFile = Path.Combine(Path.GetTempPath(), MultiLicenseFileName);
                //File.Copy(LicenceKey, myTempFile, true);
                //LicenceKey = myTempFile;
            }
        }

        public bool UpdateMultipleDBLicenseKey()
        {
            bool IsLicenseUpdated = false;
            //if (rgOptionNewExist.SelectedIndex == 0)
            //{
            //    LicenceKey = LicenceKey + txtDatabaseName.Text;
            //}
            //else
            //{
            //    LicenceKey = LicenceKey + glkExistingDB.EditValue.ToString();
            //}
            Installedpath();
            try
            {
                if (LicenceKey.Trim() != string.Empty || string.IsNullOrEmpty(txtMultipleLicenseKey.Text))
                {
                    //File.Copy(LicenceKey, LicenceKeyTarget, true);
                    //if (File.Exists(LicenceKeyTarget))
                    //{
                    //    IsLicenseUpdated = true;
                    //    File.Delete(LicenceKey);
                    //}

                    string TargetFileName = Path.Combine(LicenceKeyTarget, ACPERP_LICENSE_KEY);
                    resultArgs = UpdateLicenseFile(LicenceKey, TargetFileName);
                    if (resultArgs.Success)
                    {
                        IsLicenseUpdated = true;
                        string DestFileName = string.Empty;
                        string[] Fname = SettingProperty.ActiveDatabaseLicenseKeypath.Split('\\');
                        foreach (string item in Fname)
                        {
                            if (item.Contains(".xml"))
                            {
                                DestFileName = item;
                            }
                        }
                        if (SettingProperty.ActiveDatabaseName.Trim() != "acperp" && DestFileName != ACPERP_LICENSE_KEY && rgOptionNewExist.SelectedIndex == 1)
                        {
                            File.Delete(SettingProperty.ActiveDatabaseLicenseKeypath);
                        }
                    }
                }
                else
                {
                    //MessageBox.Show("Select Multiple Database License Key", "AcME ERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show(this.GetMessage(MessageCatalog.Master.DataUtilityForms.SELECT_MULTIPLE_DB), "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception err)
            {

            }
            this.Cursor = Cursors.Default;
            return IsLicenseUpdated;
        }

        private ResultArgs UpdateLicenseFile(string SourceFilePath, string DestinationFilePath)
        {
            ResultArgs resultArgs = new ResultArgs();
            string DestFileName = string.Empty;
            string SourceLicenseData = string.Empty;
            try
            {
                SourceLicenseData = XMLConverter.ReadFromXMLFile(SourceFilePath);
                DestFileName = (AppSetting.AccesstoMultiDB == (int)YesNo.Yes) && txtDatabaseName.Text.Trim() != "acperp"
                    ?
                    Path.Combine(LicenceKeyTarget, "AcMEERP" + DateTime.Now.Ticks.ToString() + ".xml") : DestinationFilePath;
                //  && SettingProperty.ActiveDatabaseName.Trim() != "acperp"
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
                        UpdateMultiDbLicense(txtDatabaseName.Text, DestFileName);
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

        private bool UpdateMultiDbLicense(string DBName, string MultiLicenseKey)
        {
            bool IsSuccess = false;
            string MultipleDBPath = SettingProperty.RestoreMultipleDBPath.ToString();
            try
            {
                if (DBName != string.Empty)
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
                    dvDb.RowFilter = "Restore_Db= '" + DBName + "'";
                    if (dvDb.Count == 0)
                    {
                        dsRestore.Tables[0].Rows.Add(DBName, MultiLicenseKey, DBName);  // !string.IsNullOrEmpty(dvDb.Table.Rows[0]["RestoreDBName"].ToString()) ? dvDb.Table.Rows[0]["RestoreDBName"].ToString() : 
                        XMLConverter.WriteToXMLFile(dsRestore, MultipleDBPath);
                    }
                    else
                    {
                        DataView dvfilter = dsRestore.Tables[0].AsDataView();
                        dvfilter.RowFilter = "Restore_Db <> '" + DBName + "'";
                        DataSet dsMultiDb = new DataSet();
                        dsMultiDb.Tables.Add(dvfilter.ToTable());
                        int temp = 0;
                        foreach (DataRow item in dvfilter.Table.Rows)
                        {
                            if (item["Restore_Db"].ToString().Equals(DBName.Trim()))
                            {
                                dsMultiDb.Tables[0].Rows.Add(DBName, MultiLicenseKey, DBName);
                                // !string.IsNullOrEmpty(dvDb.Table.Rows[temp]["RestoreDBName"].ToString()) ?   dvDb.Table.Rows[temp]["RestoreDBName"].ToString() :
                            }
                            temp++;
                        }
                        XMLConverter.WriteToXMLFile(dsMultiDb, MultipleDBPath);
                    }

                    //On 15/12/2020, To take multiple db backup file for saftey puropose
                    this.TakeBackup_MultipleDBXML();
                    //-------------------------------------------------------------------
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }

            return IsSuccess;

        }

        public void Installedpath()
        {
            //string TargetPath = @"C:\Program Files (x86)\BoscoSoft\AcMEERP\";
            //if (File.Exists(Path.Combine(TargetPath, "ACPP.exe")))
            //{
            //    LicenceKeyTarget = Path.Combine(@"C:\Program Files (x86)\BoscoSoft\AcMEERP\", MultiLicenseFileName);
            //}
            //else
            //{
            //    LicenceKeyTarget = Path.Combine(@"C:\Program Files\BoscoSoft\AcMEERP\", MultiLicenseFileName);
            //}
            LicenceKeyTarget = Path.Combine(Application.StartupPath.ToString(), MultiLicenseFileName);
        }

        private bool CheckLicenseKeyExists(string DbName)
        {
            bool IsSuccess = true;
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
                    DataView dvDb = new DataView();
                    dvDb = dsRestore.Tables[0].AsDataView();
                    dvDb.RowFilter = "Restore_Db= '" + DbName + "'";
                    if (dvDb.Count > 0)
                    {
                        if (string.IsNullOrEmpty(dvDb.ToTable().Rows[0]["MultipleLicenseKey"].ToString()))
                        {
                            IsSuccess = false;
                        }
                        else
                        {
                            ExistLicenseFileName = dvDb.ToTable().Rows[0]["MultipleLicenseKey"].ToString();
                        }
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

        /// <summary>
        /// 
        /// </summary>
        private void ValidateAndAssignSelectedDBDetails()
        {
            lblBranchDetails.Text = string.Empty;
            SelectedDBHOCode = SelectedDBBOCode = SelectedDBBOLocation = string.Empty;
            SelectedDBBOActiveVouchers = -1;
            if (!string.IsNullOrEmpty(SelectedDBAcmeerpBranchDetails))
            {
                if (SelectedDBAcmeerpBranchDetails.ToUpper().StartsWith("-- ACMEERP" + Delimiter.ECap.ToUpper()))
                {
                    string[] AcmeerpBranchDetails = SelectedDBAcmeerpBranchDetails.Split(Delimiter.ECap.ToCharArray());

                    if (AcmeerpBranchDetails.Length >= 4) //(AcmeerpBranchDetails.Length == 4)
                    {
                        SelectedDBHOCode = AcmeerpBranchDetails.GetValue(1).ToString().ToUpper(); //Assign HO code
                        SelectedDBBOCode = AcmeerpBranchDetails.GetValue(2).ToString().ToUpper(); //Assign BO code
                        SelectedDBBOLocation = AcmeerpBranchDetails.GetValue(3).ToString(); //Assign BO code

                        if (AcmeerpBranchDetails.Length == 5 && !string.IsNullOrEmpty(AcmeerpBranchDetails.GetValue(4).ToString()))
                        {
                            SelectedDBBOActiveVouchers = UtilityMember.NumberSet.ToInteger(AcmeerpBranchDetails.GetValue(4).ToString());
                        }
                        //SelectedDBBOLocation = "BIPL Test Test 1234";
                        lblBranchDetails.Text =   SelectedDBHOCode + " : " + SelectedDBBOCode + (string.IsNullOrEmpty(SelectedDBBOLocation)? string.Empty :  " (" + SelectedDBBOLocation + ")");
                    }
                }
            } 
        }
        
        private string GetLicBranchCode()
        {
            SimpleEncrypt.SimpleEncDec objdec = new SimpleEncrypt.SimpleEncDec();
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
                MessageRender.ShowMessage("Error in Validating License Key " + ex.Message);
            }
            finally { }
            return BranchOfficeCode;
        }

        private string GetLicHOCode()
        {
            SimpleEncrypt.SimpleEncDec objdec = new SimpleEncrypt.SimpleEncDec();
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
                MessageRender.ShowMessage(ex.Message);
            }
            finally { }
            return HeadOfficeCode;
        }
        #endregion
    }
}