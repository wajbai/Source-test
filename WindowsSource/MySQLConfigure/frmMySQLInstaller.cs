/*
Purpose: This form is used to configure mysql connection details and configure database

SERVER : Connect existing MySQL Service and create application database if not exists
SERVER_NEW_INSTANCE : Copy Mysql base folder and create new service and create application database if not exists
CLIENT : Connect our application database (locally or remotely)

Precondtion : 
1. This exe can be run individually and from setup installer
2. Base MySQL folder (for create new instance) should be added with in the setup (With root password and remote acces rights)
3. Initialize_MySQLConfigure_Settings : Initialize method, to set all the properties, customzied our own application properties
4. My.exe.config, DB_Changes_Script.sql, DB_Create_Script.sql are Embedded Resource shoulde be updated based on our application, content of these files
 * will be updated to concern files
5. Licence Key validation is Mandatory

Steps:
1.After Sucessfull licence validation, will ask mode of installation (Server, Client)
2.Server: If server mode installation will ask new instance connection or use existing mysql server connection
3.If installation mode (new instance): show server url, port (fixed avilable free port starts with 3320 by default) and MySQL Path (where path of the new instance)
    a. Base MySQL folder which is avilable in setup, will be copied to defined mysql path, 
    b. This Base mysql folder contains mysql base db with fields and root password with remote acesss
    c. Create new mysql service by using MySQL Base folder
    d. Configure dataabse and update connection string into application configure file (Refer:My Application Variables in general)
4. if use existing mysql connecton, get server url, port, username and passowrd
    a. Configure dataabse and update connection string into application configure file (Refer:My Application Variables in general)
5. If client mode: get server url, port, username and passowrd
     a.Connect dataabse and update into aod configure file (this will not create application database, 
     just it will connect to our application database  (Refer:My Application Variables in general)
*/

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
using System.ServiceProcess;
using Bosco.Utility;

namespace MySQLConfigure
{
    public partial class frmMySQLInstaller : Form
    {
        DialogResult dbconfigResult = DialogResult.Cancel;
        string LicenceKey = Path.Combine(General.TARGET_PATH, General.name_license_file);
        string LicenceKeyTarget = Path.Combine(General.TARGET_PATH, General.name_license_file);
        bool SameResoultion = true;
        Int32 FormDefaultWidth = 500;
        Int32 FormDefaultHeight = 100;
        public frmMySQLInstaller()
        {
            InitializeComponent();

            if (FormDefaultWidth != this.Width)
            {
                FormDefaultWidth = this.Width + 50;
                FormDefaultHeight = 160; //100;
                SameResoultion = false;
            }
            else
            {
                SameResoultion = true;
                FormDefaultHeight = 115; //100;
            }

            this.Text = General.Title;

            General.ClearLog();
            ClearForm();
        }

        private void frmMySQLInstaller_Activated(object sender, EventArgs e)
        {
            txtLicenceKey.Select();
            txtLicenceKey.Focus();
        }

        /// <summary>
        /// Configure mysql based on setting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInstalMySQL_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            bool bClose = false;
            General.WriteLog("Test Connection");

            if (AssignMySQLConnectionInfo())
            {
                //if installation mode is server and cancreatenew instance, create new aod mysql service and restore db
                if (General.enInstallationMode == General.InstallationMode.SERVER_NEW_INSTANCE)
                {
                    if (CheckPort())
                    {
                        General.WriteLog("Start:Server Installation");
                        if (General.ConfigureMyService())
                        {
                            General.WriteLog("Successfully My DB is configured.");
                            bClose = true;
                        }
                        else
                        {
                            General.WriteLog("My Database is not configured");
                        }
                        ChangeInstallationMode(); //Re fill all the properties...
                        General.WriteLog("Ended:Server Installation");
                    }
                    else
                        txtPort.Focus();
                    ShowSummery(true);
                }
                else //Existing Instance (server and client)
                {
                    //if installation mode is server and cancreatenew instance false, use existing mysql connection and restore db
                    //if installation mode is client use existing mysql connection.
                    General.WriteLog("Start:Client Installation");
                    using (DBHandler dbh = new DBHandler())
                    {
                        if (dbh.TestConnection(true))
                        {
                            if (General.enInstallationMode == General.InstallationMode.SERVER) //server mode installation and use existing mysql conneciton
                            {
                                if (General.ConfigureMyDataBase())
                                {
                                    General.WriteLog("My Application is configured successfully");
                                    bClose = true;
                                }
                                else
                                {
                                    MessageBox.Show(General.MYAPPLICATION + " Database is not Configured, Contact Support Team", General.Title, MessageBoxButtons.OK);
                                    General.WriteLog("My Application is not installed successfully");
                                }
                            }
                            else
                            {
                                if (General.ConnectMyDataBase())
                                {
                                    General.WriteLog("My Application is configured successfully");
                                    bClose = true;
                                }
                                else
                                {
                                    MessageBox.Show("Could not find " + General.MY_DBNAME + " database, check " + General.MY_DBNAME + " database in Server '" + General.MySQL_HOSTNAME + "'", General.Title, MessageBoxButtons.OK);
                                    General.WriteLog("My Application is not installed successfully");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Could not connect to specified instance, Access denied for User '" + txtUserName.Text + "@" + txtHostName.Text + "'.", General.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtHostName.Focus();
                        }

                        General.WriteLog("Ended:Client Installation");
                    }
                }
            }
            else
            {
                MessageBox.Show("Fill MySQL Connection details", General.Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtHostName.Focus();
            }

            if (bClose)
            {
                MessageBox.Show(General.MYAPPLICATION + " is installed successfully", General.Title, MessageBoxButtons.OK);
                this.Close();
            }
            else
            {
                MessageBox.Show(General.MYAPPLICATION + " is not installed, Contact Support Team", General.Title, MessageBoxButtons.OK);
                txtHostName.Focus();
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
                    if (dbh.TestConnection(true))
                    {
                        MessageBox.Show("Connection is succeeded", General.Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtHostName.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Could not connect to specified instance, Access denied for User '" + txtUserName.Text + "@" + txtHostName.Text + "'.", General.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtHostName.Focus();
                    }
                }
            }
            else
            {
                MessageBox.Show("Fill MySQL Connection details", General.Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtHostName.Focus();
            }
            this.Cursor = Cursors.Default;
        }

        private void btnServiceStatus_Click(object sender, EventArgs e)
        {
            if (AssignMySQLConnectionInfo())
            {
                try
                {
                    ServiceController[] scServices = ServiceController.GetServices();
                    foreach (ServiceController scTemp in scServices)
                    {
                        if (scTemp.ServiceName == General.MYSERVICE_NAME)
                        {
                            if (scTemp.Status != ServiceControllerStatus.Running)
                            {
                                this.Cursor = Cursors.WaitCursor;
                                General.WriteLog("My Service is going to be started manullay");
                                scTemp.Start();
                                TimeSpan timeout = TimeSpan.FromMilliseconds(1000 * 60);
                                scTemp.WaitForStatus(ServiceControllerStatus.Running, timeout);
                                lblMyService.Text = General.MYSERVICE_NAME + " Configured, Running";
                                this.Cursor = Cursors.Default;
                            }
                            else
                            {
                                MessageBox.Show(General.MYSERVICE_NAME + " is already running",
                                            General.Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            General.WriteLog("My Service is Started..");

                            if (scTemp.Status == ServiceControllerStatus.Running)
                            {
                                using (DBHandler dbh = new DBHandler())
                                {
                                    this.Cursor = Cursors.WaitCursor;
                                    if (dbh.RestoreMyDatabase())
                                    {
                                        this.Cursor = Cursors.Default;
                                        General.WriteLog("MyService is Started manullay..restored DB");
                                        MessageBox.Show(General.MYSERVICE_NAME + " started", General.Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show(General.MYSERVICE_NAME + " not started " + errmsg,
                        General.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (txtLicenceKey.Text != string.Empty)
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
                            MessageBox.Show("Invalid License Key", General.Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Select License Key", General.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (isLicenceKeyfound)
                {
                    this.Cursor = Cursors.WaitCursor;
                    General.LIC_UPDATED = true;
                    MessageBox.Show("License Updated", General.Title, MessageBoxButtons.OK);
                    grpDBInfo.Visible = true;
                    ChangeInstallationMode();
                    this.Cursor = Cursors.Default;

                    //Check supporting files are exists in application path ------------------------------------------
                    //if (File.Exists(Path.Combine(General.TARGET_PATH, General.MYAPPLICATION_EXE_FILE)) &&
                    //   File.Exists(Path.Combine(General.TARGET_PATH, General.MYAPPLICATION_EXE_CONFIG_FILE)))
                    //{
                    //    grpDBInfo.Visible = true;
                    //    ChangeInstallationMode();
                    //    this.Cursor = Cursors.Default;
                    //}
                    //else
                    //{
                    //    MessageBox.Show(General.MYAPPLICATION + " files are not available, Contact Support Team", General.Title, MessageBoxButtons.OK);
                    //}
                    //------------------------------------------------------------------------------------------------
                }
            }
            catch (Exception err)
            {
                General.WriteLog("Error in updating LicenseKey " + err.Message);
            }
            //------------------------------------------------------------------------------------------------
        }


        /// <summary>
        /// this method is used to checl mandatory fields and assing to connection properties
        /// </summary>
        /// <returns></returns>
        private bool AssignMySQLConnectionInfo()
        {
            bool Rtn = false;

            if (txtHostName.Text.Trim() != string.Empty && txtPort.Text.Trim() != string.Empty)
            {
                General.MySQL_HOSTNAME = txtHostName.Text.Trim();
                General.MySQL_PORT = txtPort.Text.Trim();

                if (General.enInstallationMode == General.InstallationMode.SERVER_NEW_INSTANCE && txtDBPath.Text != string.Empty)
                {
                    Rtn = true;
                    if (!General.ACMEERP_FOUND_OLD_INSTALLATION)
                    {
                        DirectoryInfo dir = new DirectoryInfo(txtDBPath.Text);
                        string datafolder = dir.Name;

                        if (datafolder.ToUpper() != General.MYSERVICE_NAME.ToUpper())
                        {
                            txtDBPath.Text = Path.Combine(txtDBPath.Text, General.MYSERVICE_NAME.ToUpper());
                        }
                        General.MYSQL_INSTALL_PATH = txtDBPath.Text;
                        General.MY_DATA_PATH = Path.Combine(txtDBPath.Text, "DATA");
                    }
                }
                else if (General.enInstallationMode != General.InstallationMode.SERVER_NEW_INSTANCE)
                {
                    if (txtUserName.Text.Trim() != string.Empty)
                    {
                        General.MySQL_ROOT_USERNAME = txtUserName.Text;
                        General.MySQL_ROOT_PASSWORD = txtPassword.Text;
                        Rtn = true;
                    }
                    else
                    {
                        Rtn = false;
                    }
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

        /// <summary>
        /// Check propsed port is free or not
        /// </summary>
        /// <returns></returns>
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
                        MessageBox.Show("MySQL Port is not avilable", General.Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// 3. If mysql 5.6 status is not installed, prerequist migiht have not installed property before aod setup
        /// 4. Installtion kit sould be exited and inform user that installation will be terminated
        /// </summary>
        private void ChangeInstallationMode()
        {
            General.WriteLog(string.Empty);
            General.WriteLog("Test Connection");

            //Make all the controls made visible
            txtPort.Enabled = true;
            lblMySQLInstallPath.Text = string.Empty;

            lblUserName.Visible = false;
            txtUserName.Visible = false;
            lblPassword.Visible = false;
            txtPassword.Visible = false;
            lblDataPath.Visible = false;
            txtDBPath.Visible = false;
            btnDBpath.Visible = false;
            btnTestConnection.Visible = false;

            //Assign MyService properties, MySQL properties
            General.Initialize_MySQLConfigure_Settings();

            int btnTop = 0;
            if (General.enInstallationMode == General.InstallationMode.SERVER_NEW_INSTANCE) //For Server (new)
            {
                lblMySQLInstallPath.Text = "MySQL Path " + General.MYSQL_INSTALL_PATH;
                lblMySQLInstallPath.Visible = true;

                //Get avilable port, if myservice not installed, get its port or free port ------------------------------------------
                //Assing MyService properties to controls
                if (General.MYSERVICE_IS_INSTALLED) //Assign Myservice properties
                {
                    if (General.ACMEERP_FOUND_OLD_INSTALLATION)
                    {
                        txtDBPath.Text = General.MY_DATA_PATH;
                    }
                    else
                    {
                        txtDBPath.Text = General.MYSQL_INSTALL_PATH;
                    }
                    txtDBPath.Enabled = false;
                    btnDBpath.Enabled = false;
                    optServer.Checked = true;
                    optClient.Enabled = false;
                    txtDBPath.Enabled = false;
                    btnDBpath.Enabled = false;
                    txtPort.Enabled = false;
                }
                else //Get Free Port
                {
                    if (General.GetMySQLPortAvilable(txtPort.Text) == 0)
                    {
                        txtPort.Enabled = true;
                    }
                    else
                    {
                        txtPort.Enabled = false;
                    }

                    txtDBPath.Text = Path.Combine(Path.GetPathRoot(General.TARGET_PATH), General.MYSERVICE_NAME);
                    txtDBPath.Enabled = true;
                    btnDBpath.Enabled = true;
                }
                txtPort.Text = General.MySQL_PORT;
                //-------------------------------------------------------------------------------------------------------------------------
                ShowSummery(false);

                lblDataPath.Visible = true;
                txtDBPath.Visible = true;
                btnDBpath.Visible = true;
                btnSummary.Visible = true;

                lblDataPath.Top = lblUserName.Top;
                txtDBPath.Top = txtUserName.Top;
                btnDBpath.Top = txtUserName.Top - 1;
                btnTop = txtDBPath.Top + txtDBPath.Height + 10;
            } //for Server (Existing, client)
            else
            {
                lblMySQLInstallPath.Text = General.TARGET_PATH;
                btnTestConnection.Visible = true;
                ShowSummery(false);
                btnSummary.Visible = false;

                //For client 
                lblUserName.Visible = true;
                txtUserName.Visible = true;
                lblPassword.Visible = true;
                txtPassword.Visible = true;
                btnTop = txtPassword.Top + txtPassword.Height + 10;
            }

            btnSummary.Top = btnTop;
            btnInstalMySQL.Top = btnTop;
            btnClose.Top = btnTop;
            btnTestConnection.Top = btnTop;

            grpDBInfo.Height = btnTop + btnClose.Height + 10;
            //grpDBInfo.Top = grpLicense.Top + grpDBInfo.Height + 50;
            grpbxSummary.Top = grpDBInfo.Top + grpDBInfo.Height + 10;

            if (grpbxSummary.Visible)
            {
                this.Size = new Size(FormDefaultWidth, grpDBInfo.Top + grpbxSummary.Height + (SameResoultion ? 70: 65)); //35
            }
            else
            {
                this.Size = new Size(FormDefaultWidth, grpDBInfo.Top + grpDBInfo.Height + 60); //35
            }

            //Fix caption of the db info frame
            txtHostName.Enabled = false;
            if (General.enInstallationMode == General.InstallationMode.CLIENT)
            {
                txtHostName.Enabled = true;
                grpDBInfo.Text = "Client Installation";
            }
            else if (General.enInstallationMode == General.InstallationMode.SERVER)
            {
                grpDBInfo.Text = "Server Installation (Existing Service)";
            }
            else if (General.enInstallationMode == General.InstallationMode.SERVER_NEW_INSTANCE)
            {
                grpDBInfo.Text = "Server Installation (Acme.erp Service : " + General.MYSERVICE_NAME + ")";
            }

            this.CenterToScreen();
            txtHostName.Focus();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            bool bclose = false;
            dbconfigResult = System.Windows.Forms.DialogResult.OK;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                AssignMySQLConnectionInfo();
                if (General.enInstallationMode == General.InstallationMode.SERVER_NEW_INSTANCE)
                {
                    if (!General.IsMyServiceInstalled())
                    {
                        this.Cursor = Cursors.Default;
                        if (MessageBox.Show("MySQL service for " + General.MYAPPLICATION + " is not installed, Do you want to cancel configuration?", General.Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
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
                General.WriteLog("Error in checking My DB configuration " + err.Message);
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
            grpbxSummary.Visible = show;

            //My Service summary
            if (General.MYSERVICE_IS_INSTALLED)
            {
                lblMyService.Text = General.MYSERVICE_NAME + " Configured";
                ServiceControllerStatus servicestatus = General.GetMyServiceStatus();
                if (servicestatus == ServiceControllerStatus.Running)
                {
                    lblMyService.Text += ", Running";
                    btnServiceStatus.Visible = false;
                }
                else
                {
                    lblMyService.Text += ", " + servicestatus.ToString();
                    btnServiceStatus.Visible = true;
                    btnServiceStatus.Text = "Start";
                }
            }
            else
            {
                lblMyService.Text = General.MYSERVICE_NAME + " not configured";
                btnServiceStatus.Visible = false;
            }

            if (grpbxSummary.Visible)
            {
                this.Size = new Size(FormDefaultWidth, grpbxSummary.Top + grpbxSummary.Height + 45);
            }
            else
            {
                this.Size = new Size(FormDefaultWidth, grpDBInfo.Top + grpDBInfo.Height + 45);
            }

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
            openLicenceKey.InitialDirectory = General.TARGET_PATH;
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
                ValidateLicence();
            }
            else
            {
                ClearForm();
            }
        }



        /// <summary>
        /// This method is inital method, to clear all the variables and controls
        /// </summary>
        private void ClearForm()
        {
            txtLicenceKey.Text = string.Empty;
            LicenceKey = string.Empty;

            grpDBInfo.Visible = false;
            grpbxSummary.Visible = false;

            lblBrachNameValue.Text = string.Empty;
            lblBarachCodeValue.Text = string.Empty;
            General.LIC_UPDATED = false;
            grpLicense.Height = FormDefaultHeight - (SameResoultion? 45  : 50);

            //--------------------------------------------------------------------------------------------
            bool ismyserviceinstalled = General.IsMyServiceInstalled();
            optClient.Enabled = true;
            if (ismyserviceinstalled)
            {
                optServer.Checked = ismyserviceinstalled;
                optClient.Enabled = !ismyserviceinstalled;
            }
            //---------------------------------------------------------------------------------------------

            this.Size = new Size(FormDefaultWidth, FormDefaultHeight); //157
            this.CenterToScreen();
        }

        /// <summary>
        /// This method is used to check licence details 
        /// </summary>
        private void ValidateLicence()
        {
            if (GetLicenceDetails())
            {
                if (SameResoultion)
                {
                    grpLicense.Height = FormDefaultHeight + 30;
                    this.Size = new Size(FormDefaultWidth, grpLicense.Top + grpLicense.Height + 55); //35
                }
                else
                {
                    grpLicense.Height = FormDefaultHeight + 50;//140;
                    this.Size = new Size(FormDefaultWidth, grpLicense.Top + grpLicense.Height + 60); //35
                }

                this.CenterToScreen();
            }
            else
            {
                if (!String.IsNullOrEmpty(LicenceKey))
                {
                    MessageBox.Show("Invalid Licence Key", General.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ClearForm();

            }

            General.enInstallationMode = General.InstallationMode.CLIENT;
            if (optServer.Checked)
                General.enInstallationMode = General.InstallationMode.SERVER;
        }
        private void optServer_CheckedChanged(object sender, EventArgs e)
        {
            ValidateLicence();
        }

        private void optClient_CheckedChanged(object sender, EventArgs e)
        {
            ValidateLicence();
        }

        private void frmMySQLInstaller_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                grpLicense.Width = grpDBInfo.Width = grpbxSummary.Width = (this.Width - (grpLicense.Left + 40));
            }
            else
            {
                grpLicense.Width = grpDBInfo.Width = grpbxSummary.Width = (this.Width - (grpLicense.Left + 40));
            }
        }

        private void frmMySQLInstaller_Load(object sender, EventArgs e)
        {

        }
    }
}