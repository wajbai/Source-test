using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;
using System.Security.Principal;
using System.Security.Permissions;
using System.Configuration.Install;
using System.Reflection;
using System.Threading;
using System.ServiceModel;
using System.ServiceProcess;
using Bosco.Utility;

namespace AcMEERPInstaller
{
    public partial class frmMySQLInstaller : Form
    {
        DialogResult dbconfigResult = DialogResult.Cancel;
        string LicenceKey = Path.Combine(General.ACPERP_SETUP_PATH, "AcMEERPLicense.xml");
        string LicenceKeyTarget = Path.Combine(General.ACPERP_TARGET_PATH, "AcMEERPLicense.xml");
        
        public frmMySQLInstaller()
        {
            InitializeComponent();
            //General.MYSQL_INSTALL_DEFAULT_PATH =  General.GetDefaultMySQLInstallPath();
            this.Text = General.ACPERP_Title;
            ClearForm();
        }

        private void frmMySQLInstaller_Activated(object sender, EventArgs e)
        {
            txtLicenceKey.Select();
            txtLicenceKey.Focus();
        }

        private void btnInstalMySQL_Click(object sender, EventArgs e)
        {
           this.Cursor = Cursors.WaitCursor;
           bool bClose = false;
           General.WriteLog(String.Empty);
           General.WriteLog("Test Connection");
           if (AssignMySQLConnectionInfo())
           {
               if (optServer.Checked)
               {
                   using (DBHandler dbh = new DBHandler())
                   {
                       if (CheckPort())
                       {
                           General.WriteLog("Start:Server Installation");
                           if (General.ConfigureACPERPMySQL())
                           {
                               //dbconfigResult = System.Windows.Forms.DialogResult.OK;
                               General.WriteLog("Successfully AcMEERP DB is configured.");
                               MessageBox.Show("Acme.erp is installed successfully", General.ACPERP_Title, MessageBoxButtons.OK);
                               //General.ImportHeadOfficeMaster();
                               bClose = true;
                           }
                           else
                           {
                               General.WriteLog("AcMEERP Database is not configured");
                               MessageBox.Show("Acme.erp is not installed, Contact Support Team", General.ACPERP_Title, MessageBoxButtons.OK);
                           }
                           ChangeInstallationMode(); //Re fill all the properties...
                           General.WriteLog("Ended:Server Installation");
                       }
                       else
                           txtPort.Focus();
                   }
                   ShowSummery(true);
               }
               else
               {
                   General.WriteLog("Start:Client Installation");
                   //General.SetRootPassword();
                   using (DBHandler dbh = new DBHandler())
                   {
                       if (General.CreateACPERPconfig())
                       {
                           //dbconfigResult = System.Windows.Forms.DialogResult.OK;
                           General.WriteLog("AcMEERP is configured successfully");
                           MessageBox.Show("Acme.erp is installed successfully", General.ACPERP_Title, MessageBoxButtons.OK);
                           bClose = true;
                       }
                       else
                       {
                           General.WriteLog("AcMEERP is not installed successfully");
                           MessageBox.Show("Acme.erp is not installed, Contact Support Team", General.ACPERP_Title, MessageBoxButtons.OK);
                       }
                   }
                   General.WriteLog("Ended:Client Installation");
               }
           }
           else
           {
               MessageBox.Show("Fill MySQL Connection details", General.ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
               txtHostName.Focus();
           }

           if (bClose)
           {
               this.Close();
           }
           this.Cursor = Cursors.Default;
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            General.WriteLog("");
            General.WriteLog("Test Connection");
            if (AssignMySQLConnectionInfo())
            {
                using (DBHandler dbh = new DBHandler())
                {
                    //General.SetRootPassword();
                    if (dbh.TestConnection(false))
                    {
                        MessageBox.Show("Connection is succeeded, acperp database is working properly", General.ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Connection is not succeeded", General.ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Fill MySQL Connection details", General.ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtHostName.Focus();
            }
            this.Cursor = Cursors.Default;
        }
                
        private void optServer_CheckedChanged(object sender, EventArgs e)
        {
           if (optServer.Checked) ChangeInstallationMode(); 
        }

        private void optClient_CheckedChanged(object sender, EventArgs e)
        {
            if (optClient.Checked) ChangeInstallationMode();
        }

        private void btnServiceStatus_Click(object sender, EventArgs e)
        {
            if (AssignMySQLConnectionInfo())
            {
                //General.SetRootPassword();
                try
                {
                    ServiceController[] scServices = ServiceController.GetServices();
                    foreach (ServiceController scTemp in scServices)
                    {
                        if (scTemp.ServiceName == General.MySQLACPERP_SERVICE_NAME)
                        {
                            if (scTemp.Status != ServiceControllerStatus.Running)
                            {
                                this.Cursor = Cursors.WaitCursor;
                                General.WriteLog("MySQLACPERP going to be started manullay");
                                scTemp.Start();
                                TimeSpan timeout = TimeSpan.FromMilliseconds(1000 * 60);
                                scTemp.WaitForStatus(ServiceControllerStatus.Running, timeout);
                                lblMySQLACPERPInstallStatus.Text = General.MySQLACPERP_SERVICE_NAME + " Configured, Running";
                                this.Cursor = Cursors.Default;
                            }
                            else
                            {
                                MessageBox.Show(General.MySQLACPERP_SERVICE_NAME + " is already running",
                                            General.ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            General.WriteLog("MySQLACPERP is Started..");

                            if (scTemp.Status == ServiceControllerStatus.Running)
                            {
                                using (DBHandler dbh = new DBHandler())
                                {
                                    this.Cursor = Cursors.WaitCursor;
                                    if (dbh.RestoreACPERPdatabase())
                                    {
                                        this.Cursor = Cursors.Default;
                                        General.WriteLog("MySQLACPERP Started manullay..restored DB");
                                        MessageBox.Show("MySQLACPERP started",General.ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                btnServiceStatus.Enabled = false;
                            }
                            this.Cursor = Cursors.Default;
                            break;
                        }
                    }
                }
                catch (Exception err)
                {
                    string errmsg = err.Message;
                    this.Cursor = Cursors.Default;
                    MessageBox.Show(General.MySQLACPERP_SERVICE_NAME + " not started " + errmsg,
                        General.ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            this.Cursor = Cursors.Default;
        }
        
        private void btnSummary_Click(object sender, EventArgs e)
        {
            ShowSummery(!grpbxSummary.Visible);
        }

        private void btnDBpath_Click(object sender, EventArgs e)
        {
            folderBrowserMySQLPath.SelectedPath = txtDBPath.Text;
            folderBrowserMySQLPath.ShowDialog();
            txtDBPath.Text = folderBrowserMySQLPath.SelectedPath;
        }
                
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
            bool isLicenceKeyfound = false;
            try
            {
                if (txtLicenceKey.Text!= string.Empty)
                {
                    if (LicenceKey == LicenceKeyTarget)
                    {
                        isLicenceKeyfound = true;
                    }
                    else
                    {
                        if (lblBarachCodeValue.Text != string.Empty &&
                                lblBrachNameValue.Text != string.Empty)
                        {
                            this.Cursor = Cursors.WaitCursor;
                            File.SetAttributes(LicenceKey, FileAttributes.Normal);
                            File.Copy(LicenceKey, LicenceKeyTarget, true);
                            if (File.Exists(LicenceKey))
                            {
                                isLicenceKeyfound = true;
                            }
                            this.Cursor = Cursors.Default;
                        }
                        else
                        {
                            MessageBox.Show("Invalid License Key", General.ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Select License Key", General.ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (isLicenceKeyfound)
                {
                    this.Cursor = Cursors.WaitCursor;
                    General.LIC_UPDATED = true;
                    MessageBox.Show("License Updated", General.ACPERP_Title, MessageBoxButtons.OK);
                    grpDBInfo.Visible = true;
                    ChangeInstallationMode();
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception err)
            {
                General.WriteLog("Error in updating LicenseKey " + err.Message);
            }
            //------------------------------------------------------------------------------------------------
        }
        
        private bool AssignMySQLConnectionInfo()
        {
            bool Rtn = false;

            if (txtHostName.Text.Trim() != string.Empty &&
                txtPort.Text.Trim() != string.Empty)
            {
                General.MySQL_HOSTNAME = txtHostName.Text.Trim();
                General.MySQL_PORT = txtPort.Text.Trim();

                if (optServer.Checked && txtDBPath.Text != string.Empty)
                {
                    Rtn = true;
                    DirectoryInfo dir = new DirectoryInfo(txtDBPath.Text);
                    string datafolder = dir.Name;

                    if (datafolder.ToUpper() != "DATA")
                    {
                        txtDBPath.Text = Path.Combine(txtDBPath.Text, "DATA");
                    }
                    General.MySQLACPERP_DATA_PATH = txtDBPath.Text;
                }
                else
                {
                    Rtn = true;
                }
                
                //Set Mysql connection string
                General.SetMySQLConnectionString();
            }
            return Rtn;
        }

        private bool CheckPort()
        {
            bool Rtn = false;
            using (DBHandler dbh = new DBHandler())
            {
                if (txtPort.Enabled)
                {
                    int processport = General.GetMySQLPortAvilable(txtPort.Text);
                    if (processport > 0)
                    {
                        txtPort.Text = processport.ToString();
                        Application.DoEvents();
                        General.MySQL_PORT = txtPort.Text;
                        Rtn = true;
                    }
                    else
                    {
                        MessageBox.Show("MySQL Port is not avilable", General.ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPort.Select();
                        txtPort.Focus();
                    }
                }
                else
                    Rtn = true;
            }
            return Rtn;
        }

        /// <summary>
        /// This method is base method of the installation, 
        /// 1. this will initialize basic ui functionality based on the mode (server and client)
        /// 2. Check Mysql 5.6 instlalation status, acperpserive status and port
        /// 3. If mysql 5.6 status is not installed, prerequist migiht have not installed property
        /// 4. Installtion kit sould be exited and inform user that installation will be terminated
        /// </summary>
        private void ChangeInstallationMode()
        {
            General.WriteLog(string.Empty);
            General.WriteLog("Test Connection");
            bool serverMode = (optServer.Checked == true);
            txtPort.Enabled = true;
            lblMySQLInstallPath.Text = string.Empty;
            string mysqlacperpdatapath = string.Empty;
            btnTestConnection.Visible = false;
            txtDBPath.Visible = false;
            btnDBpath.Visible = false;
            lblDataPath.Visible = false;

            if (serverMode)
            {
                mysqlacperpdatapath = General.GetAcMEERPDataPath();
                General.MySQLACPERP_DATA_PATH = mysqlacperpdatapath;
                if (General.IsMySQL5_6Installed())
                {
                    lblMySQLInstallPath.Text = "MySQL already installed in (" + General.MYSQL_INSTALL_PATH + ")";
                    lblMySQLInstallPath.Visible = true;
                    if (General.IsMySQLACPERPServiceInstalled())
                    {
                        txtPort.Enabled = false;
                    }
                    else
                    {
                        using (DBHandler dbh = new DBHandler())
                        {
                            if (General.GetMySQLPortAvilable(txtPort.Text) == 0)
                            {
                                txtPort.Enabled = true;
                            }
                            else
                                txtPort.Enabled = false;
                        }
                    }
                    txtPort.Text = General.MySQL_PORT;
                    ShowSummery(false);
                }
                else
                {
                    lblMySQLInstallPath.Text = "MySQL Install Path " + General.MYSQL_INSTALL_PATH;
                    using (DBHandler dbh = new DBHandler())
                    {
                        if (General.GetMySQLPortAvilable(txtPort.Text) == 0)
                        {
                            txtPort.Enabled = true;
                        }
                        else
                            txtPort.Enabled = false;
                    }
                    txtPort.Text = General.MySQL_PORT;
                }

                lblDataPath.Visible = true;
                txtDBPath.Visible = true;
                txtDBPath.Enabled = true;
                btnDBpath.Visible = true;
                btnSummary.Visible = true;
                if (mysqlacperpdatapath != string.Empty)
                {
                    txtDBPath.Text = mysqlacperpdatapath;
                    txtDBPath.Enabled = false;
                    btnDBpath.Visible = false;
                }
                else
                {
                    txtDBPath.Text = Path.Combine(Path.GetPathRoot(General.ACPERP_TARGET_PATH), @"AcMEERP\DATA");
                }
            }
            else
            {
                lblMySQLInstallPath.Text = General.ACPERP_TARGET_PATH;
                btnTestConnection.Visible = true;
                ShowSummery(false);
                btnSummary.Visible = false;
            }
            txtHostName.Focus();
            this.Size = new Size(441, 300);
            this.CenterToScreen();

            if (!General.MYSQL_IS_INSTALLED)
            {
                MessageBox.Show("Acme.erp Prerequisites (MySQL 5.6) is not installed properly, Contact Support Team", General.ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult  = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            }
        }

        /// <summary>
        /// Check current logged in user has admin rights
        /// </summary>
        /// <returns></returns>
        private bool isAdminUser()
        {
            bool isadminuser = false;
            General.WriteLog("Start:isAdminUser");
            try
            {
                isadminuser = new WindowsPrincipal(WindowsIdentity.GetCurrent())
                              .IsInRole(WindowsBuiltInRole.Administrator);

            }
            catch (Exception err)
            {
                General.WriteLog("Error in installing checking current user group " + err.Message);
                isadminuser = false;
            }
            General.WriteLog("Ended:isAdminUser");
            return isadminuser;
        }

        private bool IsMySQLFileFound()
        {
            bool Rtn = false;

            foreach (DriveInfo d in DriveInfo.GetDrives().Where(x => x.IsReady == true))
            {
                IEnumerable<string> paths = test(@d.RootDirectory.FullName);
                IEnumerable<string> m_oEnum = Enumerable.Empty<string>();
                List<string> lst = paths.ToList<string>();

                foreach (string file in lst)
                {
                    if (File.Exists(file))
                    {
                        Rtn = true;
                        break;
                    }
                }
            }

            return Rtn;
        }

        private IEnumerable<string> test(string rootDirectory)
        {
            IEnumerable<string> files = Enumerable.Empty<string>();
            IEnumerable<string> directories = Enumerable.Empty<string>();
            try
            {
                // The test for UnauthorizedAccessException.
                var permission = new FileIOPermission(FileIOPermissionAccess.PathDiscovery, rootDirectory);
                permission.Demand();

                files = Directory.GetFiles(rootDirectory, "mysql.exe");
                directories = Directory.GetDirectories(rootDirectory);
            }
            catch
            {
                // Ignore folder (access denied).
                rootDirectory = null;
            }

            if (rootDirectory != null)
                yield return rootDirectory;

            foreach (var file in files)
            {
                yield return file;
            }

            // Recursive call for SelectMany.
            var subdirectoryItems = directories.SelectMany(test);
            foreach (var result in subdirectoryItems)
            {
                if (result.Contains("mysql"))
                    yield return result;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            bool bclose = false;
            dbconfigResult = System.Windows.Forms.DialogResult.OK;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                AssignMySQLConnectionInfo();
                if (optServer.Checked)
                {
                    //Checking MySQL and its server is configured sucessfully
                    if (!General.IsMySQL5_6Installed())
                    {
                        this.Cursor = Cursors.Default;
                        if (MessageBox.Show("MySQL is not installed, Do you want to cancel configuration?", General.ACPERP_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            dbconfigResult = System.Windows.Forms.DialogResult.Cancel;
                            bclose = true;
                        }
                    }
                    else if (!General.IsMySQLACPERPServiceInstalled())
                    {
                        this.Cursor = Cursors.Default;
                        if (MessageBox.Show("MySQL service for Acme.erp is not installed, Do you want to cancel configuration?", General.ACPERP_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            dbconfigResult = System.Windows.Forms.DialogResult.Cancel;
                            bclose = true;
                        }
                    }
                    else
                        bclose = true;
                }
                else
                    bclose = true;
            }
            catch (Exception err)
            {
                //MessageBox.Show("Error in checking ACPERP DB configuration", General.ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                General.WriteLog("Error in checking ACPERP DB configuration " + err.Message);
            }
            
            using (DBHandler dbh = new DBHandler())
            {
                //General.SetRootPassword();    
                if (!dbh.TestConnection(false))
                {
                    this.Cursor = Cursors.Default;
                    if (MessageBox.Show("Acme.erp database is not connected, Do you want to cancel the installation ?", General.ACPERP_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                    {
                        bclose = true;
                    }
                    else
                    {
                        bclose = false;
                    }
                }
            }

            this.Cursor = Cursors.Default;
            if (bclose)
            {
                this.DialogResult = dbconfigResult;
                this.Close();
            }
        }

        private void ShowSummery(bool show)
        {
            this.Cursor = Cursors.WaitCursor;

            if (show)
            {
                grpbxSummary.Visible = show;
                General.IsMySQL5_6Installed();
                General.IsMySQLACPERPServiceInstalled();

                //MySQL summary
                if (General.MYSQL_IS_INSTALLED)
                    lblMySQLInstallStatus.Text = "MySQL 5.6 installed";
                else
                    lblMySQLInstallStatus.Text = "MySQL 5.6 not installed";

                //MySQLACPERP Service summary
                if (General.MySQLACPERPSERVICE_IS_INSTALLED)
                {
                    lblMySQLACPERPInstallStatus.Text = General.MySQLACPERP_SERVICE_NAME + " Configured";
                    ServiceControllerStatus servicestatus = General.GetMySQLACPERPServiceStatus();
                    if (servicestatus == ServiceControllerStatus.Running)
                    {
                        lblMySQLACPERPInstallStatus.Text += ", Running";
                        btnServiceStatus.Visible = false;
                    }
                    else
                    {
                        lblMySQLACPERPInstallStatus.Text += ", " + servicestatus.ToString();
                        btnServiceStatus.Visible = true;
                        btnServiceStatus.Text = "Start";
                    }
                }
                else
                {
                    lblMySQLACPERPInstallStatus.Text = General.MySQLACPERP_SERVICE_NAME + " not configured";
                    btnServiceStatus.Visible = false;
                }

                this.Size = new Size(441, 395);
            }
            else
            {
                grpbxSummary.Visible = show;
                this.Size = new Size(441, 300);
            }
            this.CenterToScreen();
            this.Cursor = Cursors.Default;
        }

        private bool GetLicenceDetails()
        {
            bool Rtn = false;

            try
            {
                if (File.Exists(LicenceKey))
                {
                    lblBarachCodeValue.Text = General.GetLicBranchCode(LicenceKey).ToUpper();
                    lblBrachNameValue.Text = General.GetLicInstituteName(LicenceKey);

                    if (lblBarachCodeValue.Text != string.Empty &&
                        lblBrachNameValue.Text != string.Empty)
                    {
                        Rtn = true;
                    }
                }

            }
            catch (Exception err)
            {
                General.WriteLog("Error in Validating License Key " + err.Message);
                Rtn = false;
            }
            return Rtn;
        }

        private void txtLicenceKey_TextChanged(object sender, EventArgs e)
        {
            GetLicenceDetails();
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            openLicenceKey.InitialDirectory = General.ACPERP_SETUP_PATH;
            openLicenceKey.Filter = "XML Files (.xml)|*.xml";
            openLicenceKey.FileName = string.Empty;
            if (File.Exists(LicenceKey))
            {
                openLicenceKey.FileName = LicenceKey;
            }
            if (openLicenceKey.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LicenceKey = openLicenceKey.FileName;
                txtLicenceKey.Text = LicenceKey;
                if (!GetLicenceDetails())
                {
                    ClearForm();
                }
            }
            else
            {
                txtLicenceKey.Text = string.Empty;
                LicenceKey = string.Empty;
                ClearForm();
            }
        }
               
        private void ClearForm()
        {
            General.ClearLog();
            grpDBInfo.Visible = false;
            grpbxSummary.Visible = false;
            grpDBInfo.Visible = false;
            grpbxSummary.Visible = false;
            
            lblBrachNameValue.Text = string.Empty;
            lblBarachCodeValue.Text = string.Empty;
            General.LIC_UPDATED = false;
            this.Size = new Size(441, 157);
            this.CenterToScreen();
        }
    }
}