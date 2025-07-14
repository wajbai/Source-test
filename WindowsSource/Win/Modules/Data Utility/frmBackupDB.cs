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
using System.Configuration;
using System.IO;
using System.Reflection;
using Bosco.DAO.Configuration;
using System.Security.AccessControl;
using System.Security.Principal;
using Bosco.Model.Transaction;

namespace ACPP.Modules.Data_Utility
{
    public partial class frmBackup : frmFinanceBaseAdd
    {
        #region test
        StreamWriter OutputStream;
        string GetPathName = string.Empty;
        int RecordIncrease = 0;
        string cmd = string.Empty;
        string DataBaseName = string.Empty;
        private string ServicePath { get; set; }
        #endregion

        #region VariableDeclaration
        BackupAndRestore BackRestore = new BackupAndRestore();
        ResultArgs resultArgs = new ResultArgs();
        string FilePath = string.Empty;
        bool ForceToTake = false;
        #endregion

        #region Constructor
        public frmBackup()
        {
            InitializeComponent();
        }

        public frmBackup(bool forceToTake)
            : this()
        {
            ForceToTake = forceToTake;
        }
        #endregion

        #region Events
        /// <summary>
        /// Load the Backup form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmBackup_Load(object sender, EventArgs e)
        {
            string DBName = GetAppConfigDB();
            if (!string.IsNullOrEmpty(DBName))
            {
                txtDatabaseName.Text = DBName;
            }
            else
            {
                //MessageBox.Show("No Database Exists in the Config file");
                MessageBox.Show(this.GetMessage(MessageCatalog.Master.DataUtilityForms.NO_DB_EXITS_IN_CONFIG_FILE));
            }
            txtDatabaseName.Enabled = false;

        }

        /// <summary>
        /// Backup is Called to be executed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackup_Click(object sender, EventArgs e)
        {
            if (isValidInput())
            {
                Backup();
                if (ForceToTake)
                {
                    this.Close();
                }
            }
        }

        /// <summary>
        /// To close the Backup form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Methods
        private bool isValidInput()
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(txtDatabaseName.Text))
            {
                //this.ShowMessageBox("Enter the database name for Backup.");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.DataUtilityForms.ENTER_DB_NAME_FOR_BACKUP));
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        /// Select the PathName
        /// </summary>
        private bool SelectedPathName()
        {
            bool selectedfolderPath = false;
            DateTime Time = DateTime.Now;
            int Year = Time.Year;
            int Month = Time.Month;
            int Day = Time.Day;
            int Hour = Time.Hour;
            int Minites = Time.Minute;
            int Seconds = Time.Second;
            int MilliSecond = Time.Millisecond;
            SaveFileDialog saveDialog = new SaveFileDialog();

            // commanded by chinna 16.09.2020
            //  saveDialog.FileName = "Acmeerp" + "-" + Year + Month + Day + Hour + Minites + Seconds + MilliSecond + ".sql";

            saveDialog.FileName = this.AppSetting.InstituteName + "-" + "Acmeerp Backup" + "-" + Day + "-" + Month + "-" + Year + "(" + Hour + "." + Minites + ")" + ".sql";

            saveDialog.DefaultExt = "sql";
            saveDialog.Filter = "Sql files (*.Sql)|*.Sql";
            saveDialog.Title = "Backup";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                FilePath = saveDialog.FileName;
                FilePath = Path.ChangeExtension(FilePath, ".sql");
                selectedfolderPath = true;
            }
            return selectedfolderPath;
        }

        ///// <summary>
        ///// This is to get the Appconfig database Name
        ///// </summary>
        ///// <returns></returns>
        //private string GetAppConfigDB()
        //{
        //    string connString = string.Empty;
        //    string dbname = string.Empty;
        //    if (this.AppSetting.AccesstoMultiDB == (int)YesNo.Yes && !(string.IsNullOrEmpty(ConfigurationHandler.Instance.ConnectionString)))
        //    {
        //        connString = ConfigurationHandler.Instance.ConnectionString;
        //    }
        //    else { connString = ConfigurationManager.ConnectionStrings[AppSettingName.AppConnectionString.ToString()].ToString(); }
        //    string option = "database=";
        //    if (!string.IsNullOrEmpty(option))
        //    {
        //        string dbcon = connString.Substring(connString.IndexOf(option) + option.Length);
        //        string[] dbconfig = dbcon.Split(';');
        //        dbname = dbconfig[0];
        //    }
        //    return dbname;
        //}

        /// <summary>
        /// Backup
        /// </summary>
        private void Backup()
        {
            //On 04/09/2019, to check corrupted db or not
            if (!BackRestore.IsDBCorrupted())
            {
                bool ConfirmPath = SelectedPathName();
                if (ConfirmPath == true)
                {
                    this.ShowWaitDialog();

                    ResultArgs result = MySqlBackup(FilePath, txtDatabaseName.Text);
                    this.CloseWaitDialog();
                    if (result.Success)
                    {
                        //this.ShowMessageBox("The Backup was finished successfully" + Environment.NewLine + "The File " + FilePath);
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.DataUtilityForms.BACKUP_FINISED_SUCCESS) + Environment.NewLine + this.GetMessage(MessageCatalog.Master.DataUtilityForms.DB_THE_FILE) + FilePath);
                    }
                    else
                    {
                        this.ShowMessageBox(resultArgs.Message);
                    }
                }
            }
            else
            {
                MessageRender.ShowMessage(BackupAndRestore.BACKUP_CORRUPTION_MESSAGE);
            }
        }

        /// <summary>
        /// Backup Logic
        /// </summary>
        /// <param name="DrivePath"></param>
        /// <param name="DataBaseBackupName"></param>
        /// <returns></returns>
        public ResultArgs MySqlBackup(string DrivePath, string DataBaseBackupName)
        {
            string[] DB;
            string[] UName;
            string[] PWord;
            string[] CheckDataBase;
            resultArgs.Success = true;
            DateTime Time = DateTime.Now;
            int Year = Time.Year;
            int Month = Time.Month;
            int Day = Time.Day;
            int Hour = Time.Hour;
            int Minites = Time.Minute;
            int Seconds = Time.Second;
            int MilliSecond = Time.Millisecond;
            Int32 noofactivevouchers = this.GetNoActiveVouchers();
            GetPathName = DrivePath;
            string version = BackRestore.MysqlVersion();
            bool isLinuxMySQL = BackRestore.IsLinuxMySQL();
            string linuxTabDisable_GIT_PURED_TAG = (isLinuxMySQL ? " --set-gtid-purged=OFF" : string.Empty);

            string mysqlDefaultConnection = ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString;
            try
            {
                if (mysqlDefaultConnection.Contains("port") == true)
                {
                    string[] DefaultAppString = mysqlDefaultConnection.Split(';');
                    string[] SName = DefaultAppString[0].ToString().Split('=');
                    string[] Port = DefaultAppString[1].ToString().Split('=');
                    CheckDataBase = DefaultAppString[2].ToString().Split('=');
                    if (CheckDataBase[0].Contains("Connection Timeout"))
                    {
                        DB = DefaultAppString[3].ToString().Split('=');
                        UName = DefaultAppString[4].ToString().Split('=');
                        PWord = DefaultAppString[5].ToString().Split('=');
                    }
                    else
                    {
                        DB = DefaultAppString[2].ToString().Split('=');
                        UName = DefaultAppString[3].ToString().Split('=');
                        PWord = DefaultAppString[4].ToString().Split('=');
                    }
                    string serverName = SName[1];
                    string userName = UName[1];
                    string password = PWord[1];
                    string databaseName = DataBaseBackupName;   //"acperp";// DB[1];
                    string dbPort = Port[1];
                    string host = serverName;
                    string user = userName;
                    string pswd = password;
                    string dbnm = databaseName;
                    string dbport = dbPort;

                    // This is to take Backup in the Linux File
                    if (version.Contains("5.6"))
                    {
                        if (!string.IsNullOrEmpty(password))
                            cmd = String.Format("-h{0} -P{1} -u{2} -p{3} --databases  --hex-blob \"{4}\" -R", host, dbport, user, pswd, dbnm);
                        else
                            cmd = String.Format("-h{0} -P{1} -u{2}  --databases --hex-blob \"{3}\"  -R", host, dbport, user, dbnm);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(password))
                            cmd = String.Format("-h{0} -P{1} -u{2} -p{3} --databases  --hex-blob --set-gtid-purged=OFF \"{4}\"  -R", host, dbport, user, pswd, dbnm);
                        else
                            cmd = String.Format("-h{0} -P{1} -u{2}  --databases --hex-blob --set-gtid-purged=OFF \"{3}\"  -R", host, dbport, user, dbnm);
                    }
                }
                else
                {
                    string[] DefaultAppString = mysqlDefaultConnection.Split(';');
                    string[] SName = DefaultAppString[0].ToString().Split('=');
                    CheckDataBase = DefaultAppString[1].ToString().Split('=');
                    if (CheckDataBase[0].Contains("connection timeout"))
                    {
                        DB = DefaultAppString[2].ToString().Split('=');
                        UName = DefaultAppString[3].ToString().Split('=');
                        PWord = DefaultAppString[4].ToString().Split('=');
                    }
                    else
                    {
                        DB = DefaultAppString[1].ToString().Split('=');
                        UName = DefaultAppString[2].ToString().Split('=');
                        PWord = DefaultAppString[3].ToString().Split('=');
                    }

                    string serverName = SName[1];
                    string userName = UName[1];
                    string password = PWord[1];
                    string databaseName = DataBaseBackupName; //"acperp";// DB[1];
                    string host = serverName;
                    string user = userName;
                    string pswd = password;
                    string dbnm = databaseName;

                    if (version.Contains("5.6"))
                    {
                        if (!string.IsNullOrEmpty(password.TrimEnd()))
                            cmd = String.Format("-h{0} -u{1} -p{2}  --opt --databases --hex-blob {3} -R", host, user, pswd, dbnm);
                        else
                            cmd = String.Format("-h{0} -u{1} --opt --databases --hex-blob {2} -R", host, user, dbnm);
                    }
                    else  //This is to take Backup in the Linux Os
                    {
                        if (!string.IsNullOrEmpty(password.TrimEnd()))
                            cmd = String.Format("-h{0} -u{1} -p{2}  --opt --databases --hex-blob --set-gtid-purged=OFF {3} -R", host, user, pswd, dbnm);
                        else
                            cmd = String.Format("-h{0} -u{1} --opt --databases --hex-blob --set-gtid-purged=OFF {2} -R", host, user, dbnm);
                    }
                }

                //application path instead of registry path... chinna

                string InstallationPath = Application.StartupPath;
                InstallationPath = InstallationPath + "\\mysqldump.exe";

                // string InstallationPath = BackRestore.MysqlInstallPath();
                // if (System.IO.File.Exists(InstallationPath) && InstallationPath.IndexOf("mysqldump") > 0)

                if (File.Exists(InstallationPath))
                {
                    string mysqldumpPath = InstallationPath;
                    string filePath = GetPathName;
                    if (filePath != string.Empty)
                    {
                        OutputStream = new StreamWriter(filePath);
                    }
                    else
                    {
                        RecordIncrease = RecordIncrease + 1;
                        string NameDeclared = "/AcpERPSafetyBackUp" + RecordIncrease + "-" + Year + Month + Day + Hour + Minites + Seconds + MilliSecond + ".sql";
                        ServicePath = InstallationPath;
                        if (ServicePath.IndexOf("mysqldump.exe") > 0)
                            ServicePath = ServicePath.Substring(0, ServicePath.IndexOf("mysqldump") - 4);
                        ServicePath = ServicePath + "AcMeSafetyBackUp";
                        if (!Directory.Exists(ServicePath))
                        {
                            Directory.CreateDirectory(ServicePath);
                            Access(ServicePath);
                        }
                        string test = ServicePath + NameDeclared;
                        OutputStream = new StreamWriter(test);
                    }
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.FileName = mysqldumpPath;
                    startInfo.Arguments = cmd + linuxTabDisable_GIT_PURED_TAG;
                    startInfo.RedirectStandardError = true;
                    startInfo.RedirectStandardInput = false;
                    startInfo.RedirectStandardOutput = true;
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow = true;
                    startInfo.ErrorDialog = false;
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.StartInfo = startInfo;
                    proc.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(OnDataReceived);
                    proc.Start();
                    string readDataline = proc.StandardOutput.ReadToEnd();
                    //On 24/08/2020, To insert Current License details into backup file to validate it when it is restored ------------------
                    OutputStream.WriteLine("-- Acmeerp" + Delimiter.ECap + AppSetting.AcmeerpBranchDetailsReference + Delimiter.ECap + noofactivevouchers.ToString());
                    //-----------------------------------------------------------------------------------------------------------------------
                    OutputStream.WriteLine(readDataline);

                    proc.WaitForExit();
                    OutputStream.Flush();
                    OutputStream.Close();
                    proc.Close();

                    //On 04/09/2019, to check all the tables are taken in backup-----------------------
                    if (!readDataline.Contains("-- Dump completed on"))
                    {
                        resultArgs.Message = BackupAndRestore.BACKUP_WARNING_CORRUPTION_MESSAGE;
                        //File.Delete(filePath);
                    }
                    //---------------------------------------------------------------------------------
                }
                else
                {
                    resultArgs.Message = "mysqldump is not available in the Installation Path" + " " + InstallationPath;
                }
            }
            catch (Exception ex)
            {
                resultArgs.Success = false;
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        private void Access(string AccessPath)
        {
            DirectorySecurity sec = Directory.GetAccessControl(AccessPath);
            SecurityIdentifier everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
            sec.AddAccessRule(new FileSystemAccessRule(everyone, FileSystemRights.Modify | FileSystemRights.Synchronize, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
            Directory.SetAccessControl(AccessPath, sec);
        }

        #endregion


        public void OnDataReceived(object Sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                OutputStream.WriteLine(e.Data);
            }
        }
    }
}