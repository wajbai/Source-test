using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.ServiceModel.Configuration;
using System.ServiceModel;
using System.Diagnostics;
using IWshRuntimeLibrary;


namespace AcMEERPUpdater
{
    public partial class frmUpdater : Form
    {
        private bool isMultiDatabaseLicense = false;
        private DataTable dtMultiDatabases = null;
        //public string LicensePath = Path.Combine(General.ACMEERP_INSTALLED_PATH, "AcMEERPLicense.xml");
        public frmUpdater()
        {
            InitializeComponent();


            lblMultiDBDetails.Text = string.Empty;
            lblMultiDBDetails.Visible = btnExit.Visible = lblBranch.Visible = cbbrancheslist.Visible = btnMultiDBUpdate.Visible = false;
            pnlStatus.Visible = General.IS_SILENT_INSTALLATION;
            pnlUpdaterInfo.Visible = !General.IS_SILENT_INSTALLATION;
            lblStatus.Text = string.Empty;

            if (CheckMySQLComponent())
            {
                General.ClearLog();
                this.Text = General.ACMEERP_UPDATER_TITLE;
                General.AssignAcMEERPProperties();
                lblInstalledPathValue.Text = General.ACMEERP_INSTALLED_PATH;
                lblVersionValue.Text = General.ACMEERP_VERSION;
                lblUpdaterVersionValue.Text = General.ACMEERP_UPDATE_VERSION;
                gvUpdatefileslist.DataSource = General.ACMEERP_UPDATE_FILES;
                if (gvUpdatefileslist.Rows.Count > 0)
                {
                    lblNoofFile.Text = gvUpdatefileslist.Rows.Count + " file(s)";
                }
                else
                {
                    lblNoofFile.Text = "No Updation";
                }

                if (General.IS_SILENT_INSTALLATION)
                {
                    this.Size = new Size(465, 90);
                    pnlStatus.Top = 5;

                    isMultiDatabaseLicense = General.IsAccesstoMultipleDatabase();
                    if (isMultiDatabaseLicense)
                    {
                        dtMultiDatabases = General.GetDatabasesfromXml();

                        if (isMultiDatabaseLicense && dtMultiDatabases != null && dtMultiDatabases.Rows.Count > 1)
                        {
                            lblMultiDBDetails.Top = pbarUpdater.Top + 5;
                            lblMultiDBDetails.Left = pbarUpdater.Left;
                            lblMultiDBDetails.Width = pbarUpdater.Width;
                            string msg = "As there are " + dtMultiDatabases.Rows.Count.ToString() + " Branches are available. " +
                                   "It will take some minute(s) to update all the Branches or Select particular Branch and update.";
                            lblMultiDBDetails.Text = msg;
                            pbarUpdater.Visible = false;

                            LoadBranchesList();
                            cbbrancheslist.Top = (pnlStatus.Top + pnlStatus.Height) + 2;
                            lblBranch.Top = cbbrancheslist.Top + 4;
                            btnMultiDBUpdate.Top = cbbrancheslist.Top - 1;
                            btnMultiExit.Top = btnMultiDBUpdate.Top;
                            lblMultiDBDetails.Visible = lblBranch.Visible = cbbrancheslist.Visible = btnMultiDBUpdate.Visible = btnMultiExit.Visible = true;
                            this.Size = new Size(465, 120);
                            this.CenterToScreen();
                        }
                    }
                }
                else
                {
                    this.Size = new Size(465, 282);//332
                }
                this.CenterToScreen();
            }
            else
            {
                Application.Exit();
            }
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.Size = new Size(465, 332); //to show status panel
            this.CenterToScreen();
            DoUpdater();

            this.Cursor = Cursors.Default;
            // ChangeIcon();
        }



        /// <summary>
        /// Method to construct connection string 
        /// </summary>
        /// <param name="connString">Connection string from app.config</param>
        /// <param name="option">Data to find Default:database</param>
        /// <param name="value">Data to replace Default:connection string db name</param>
        /// <returns>New connection string</returns>
        private String changeConnStringItem(string connString, string option, string value)
        {
            String[] conItems = connString.Split(';');
            String result = "";
            foreach (String item in conItems)
            {
                if (item.Trim().StartsWith(option.Trim()))
                {
                    result += option + "=" + value + ";";
                }
                else
                {
                    result += item + ";";
                }
            }
            return result.TrimEnd(';');
        }




        private void UpdatedConfigFile()
        {
            //General.ACMEERP_INSTALLED_PATH
            // var configuration = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap { ExeConfigFilename = @"D:\config\AppConfigUpdate\AppConfigUpdate\App.config" }, ConfigurationUserLevel.None);
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap { ExeConfigFilename = General.ACMEERP_INSTALLED_PATH + "\\ACPP.exe.config" }, ConfigurationUserLevel.None);
            long size = 2147483647;
            TimeSpan tsMinutes = new TimeSpan(0, 0, 40, 0);
            ServiceModelSectionGroup serviceModelGroup = configuration.GetSectionGroup("system.serviceModel") as ServiceModelSectionGroup;
            BasicHttpBindingElement bshttpElement = new BasicHttpBindingElement();
            bshttpElement.Name = "BasicHttpBinding_IDataSynchronizer";
            bshttpElement.MaxReceivedMessageSize = size;
            bshttpElement.CloseTimeout = tsMinutes;
            bshttpElement.OpenTimeout = tsMinutes;
            bshttpElement.ReceiveTimeout = tsMinutes;
            bshttpElement.SendTimeout = tsMinutes;
            bshttpElement.MaxBufferSize = (int)size;
            bshttpElement.MaxBufferPoolSize = size;

            if (serviceModelGroup.Bindings.BasicHttpBinding.Bindings.Count > 0)
            {
                serviceModelGroup.Bindings.BasicHttpBinding.Bindings.Remove(bshttpElement);
            }
            serviceModelGroup.Bindings.BasicHttpBinding.Bindings.Add(bshttpElement);

            // This is made for new changes

            BasicHttpBindingElement bshttpElement1 = new BasicHttpBindingElement();
            bshttpElement1.Name = "SSPAcmeIntegrationSoap";
            bshttpElement1.MaxReceivedMessageSize = size;
            bshttpElement1.CloseTimeout = tsMinutes;
            bshttpElement1.OpenTimeout = tsMinutes;
            bshttpElement1.ReceiveTimeout = tsMinutes;
            bshttpElement1.SendTimeout = tsMinutes;
            bshttpElement1.MaxBufferSize = (int)size;
            bshttpElement1.MaxBufferPoolSize = size;
            if (serviceModelGroup.Bindings.BasicHttpBinding.Bindings.Count > 0)
            {
                // bshttpElement1.Security.Mode = BasicHttpSecurityMode.Transport;
                bshttpElement1.Security.Mode = BasicHttpSecurityMode.Transport;
                serviceModelGroup.Bindings.BasicHttpBinding.Bindings.Remove(bshttpElement1);
            }
            serviceModelGroup.Bindings.BasicHttpBinding.Bindings.Add(bshttpElement1);

            string contract = "DataSyncService.IDataSynchronizer";
            EndpointAddress baseAddress = new EndpointAddress(new Uri("http://acmeerp.org/DataSyncService/DataSynchronizer.svc"));
            ChannelEndpointElement chalpoint = new ChannelEndpointElement(baseAddress, contract);
            chalpoint.Name = "BasicHttpBinding_IDataSynchronizer";
            chalpoint.Binding = "basicHttpBinding";
            chalpoint.BindingConfiguration = "BasicHttpBinding_IDataSynchronizer";

            // To get the Head Office Code
            string HeadofficeCode = GetLicHeadCode();

            string contract1 = "SSPAcmeIntegration.SSPAcmeIntegrationSoap";
            // EndpointAddress baseAddress1 = new EndpointAddress(new Uri("https://test.smartschoolplus.co.in/Webservice/SSPAcmeintegration.asmx"));
            // EndpointAddress baseAddress1 = new EndpointAddress(new Uri("http://108.170.11.170/2.0TEST/Webservice/SSPAcmeintegration.asmx"));
            EndpointAddress baseAddress1;
            if (HeadofficeCode == "sdbinm")
            {
                baseAddress1 = new EndpointAddress(new Uri("https://sdbinmsmartschoolplus.co.in//WebService/SSPAcmeIntegration.asmx"));
            }
            else
            {
                baseAddress1 = new EndpointAddress(new Uri("https://test.smartschoolplus.co.in/Webservice/SSPAcmeintegration.asmx"));
            }

            ChannelEndpointElement chalpoint1 = new ChannelEndpointElement(baseAddress1, contract1);
            chalpoint1.Name = "SSPAcmeIntegrationSoap";
            chalpoint1.Binding = "basicHttpBinding";
            chalpoint1.BindingConfiguration = "SSPAcmeIntegrationSoap";

            if (serviceModelGroup.Client.Endpoints.Count > 0)
            {
                serviceModelGroup.Client.Endpoints.Remove(chalpoint);
            }
            serviceModelGroup.Client.Endpoints.Add(chalpoint);

            if (serviceModelGroup.Client.Endpoints.Count > 0)
            {
                serviceModelGroup.Client.Endpoints.Remove(chalpoint1);
            }
            serviceModelGroup.Client.Endpoints.Add(chalpoint1);

            //Update Version
            if (configuration.AppSettings.Settings["version"] == null)
            {
                configuration.AppSettings.Settings.Add("version", General.ACMEERP_UPDATE_VERSION);
            }
            else
            {
                configuration.AppSettings.Settings["version"].Value = General.ACMEERP_UPDATE_VERSION;
            }

            //To add payroll SQL Assembley
            // <add key="PayrollSQLAdapter" value="Payroll.SQL.SQLAdapter,Payroll.SQL"/>
            if (configuration.AppSettings.Settings["PayrollSQLAdapter"] == null)
                configuration.AppSettings.Settings.Add("PayrollSQLAdapter", "Payroll.SQL.SQLAdapter,Payroll.SQL");
            if (configuration.AppSettings.Settings["HOSQLAdapter"] == null)
                configuration.AppSettings.Settings.Add("HOSQLAdapter", "Bosco.HOSQL.SQLAdapter,Bosco.HOSQL");
            if (configuration.AppSettings.Settings["ftpURL"] == null)
                configuration.AppSettings.Settings.Add("ftpURL", "httpdocs/Module/Software/Uploads/");

            configuration.Save(ConfigurationSaveMode.Minimal);
        }

        /// <summary>
        /// To Get the Head Office Code 
        /// </summary>
        /// <returns></returns>
        private string GetLicHeadCode()
        {
            DataTable dtLicense = new DataTable();
            string HeadOfficeCode = string.Empty;

            DataSet dsLicenseInfo = new DataSet();
            try
            {
                string fileName = Path.Combine(General.ACMEERP_INSTALLED_PATH, "AcMEERPLicense.xml");
                if (System.IO.File.Exists(fileName))
                {
                    dsLicenseInfo.ReadXml(fileName);  //XMLConverter.ConvertXMLToDataSet(fileName);
                    if (dsLicenseInfo != null && dsLicenseInfo.Tables.Count > 0)
                    {
                        dtLicense = dsLicenseInfo.Tables["LicenseKey"];
                        if (dtLicense != null)
                        {
                            DataRow drLicense = dtLicense.Rows[0];
                            HeadOfficeCode = Decrept(drLicense["HEAD_OFFICE_CODE"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                General.WriteLog("We are not able to get the Default Headoffice Code" + ex.Message);
            }
            finally { }
            return HeadOfficeCode;
        }

        /// <summary>
        /// Encrept the values
        /// </summary>
        /// <param name="EncreptValue"></param>
        /// <returns></returns>
        public static string Decrept(string EncreptValue)
        {
            string decreptValue = string.Empty;
            try
            {
                SimpleEncrypt.SimpleEncDec objDec = new SimpleEncrypt.SimpleEncDec();
                if (!string.IsNullOrEmpty(EncreptValue))
                {
                    decreptValue = objDec.DecryptString(EncreptValue);
                }
            }
            catch (Exception ex)
            {
                General.WriteLog("--" + ex.Message);
            }
            finally
            {
            }
            return decreptValue;
        }

        private bool CheckMySQLComponent()
        {
            bool Rtn = false;
            try
            {
                using (MySql.Data.MySqlClient.MySqlConnection conn
                    = new MySql.Data.MySqlClient.MySqlConnection())
                {
                    Rtn = true;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("MySQL component is missing in Acme.erp Updater, Contact BoscoSoft Admin", General.ACMEERP_UPDATER_TITLE,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                General.WriteLog("Error in CheckMySQLComponent " + err.Message);
            }
            return Rtn;
        }

        private void ChangeIcon()
        {
            String path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            String startuppath = Environment.GetFolderPath(Environment.SpecialFolder.Programs);
            String oldstartupiconpath = System.IO.Path.Combine(startuppath + "\\AcMEERP", "AcMEERP.lnk");
            String oldiconpath = System.IO.Path.Combine(path, "AcME ERP.lnk");
            String olduninstallstartupiconpath = System.IO.Path.Combine(startuppath + "\\AcMEERP", "Uninstall AcMEERP.lnk");////Uninstall AcMEERP

            if (System.IO.File.Exists(oldiconpath))
            {
                System.IO.File.Delete(oldiconpath);
                String newshortcut = System.IO.Path.Combine(path, "Acme.erp.lnk");

                WshShell shell = new WshShell();

                IWshShortcut link = (IWshShortcut)shell.CreateShortcut(newshortcut);
                link.TargetPath = Path.Combine(General.ACMEERP_INSTALLED_PATH, "acpp.exe");
                link.Description = "Acme.erp is an Accounting ERP for Religious and NGOs";
                link.IconLocation = Path.Combine(General.ACMEERP_INSTALLED_PATH, "Acme.erpLogo.ico");
                link.Save();
                if (System.IO.File.Exists(oldstartupiconpath))
                {
                    System.IO.File.Delete(oldstartupiconpath);
                    String newstartupdshortcut = System.IO.Path.Combine(startuppath + "\\AcMEERP", "Acme.erp.lnk");

                    WshShell startupshell = new WshShell();
                    IWshShortcut startuplink = (IWshShortcut)shell.CreateShortcut(newstartupdshortcut);
                    startuplink.TargetPath = Path.Combine(General.ACMEERP_INSTALLED_PATH, "acpp.exe");
                    startuplink.Description = "Acme.erp is an Accounting ERP for Religious and NGOs";
                    startuplink.IconLocation = Path.Combine(General.ACMEERP_INSTALLED_PATH, "Acme.erpLogo.ico");
                    startuplink.Save();
                }

            }
        }


        private void DoUpdater()
        {
            this.Cursor = Cursors.WaitCursor;

            // DateTime starttime = DateTime.Now;
            bool updatedsucessfully = false;
            if (General.ACMEERP_IS_INSTALLED)
            {
                if (!General.IsAcMEERPRunning())
                {
                    pnlStatus.Visible = true;

                    //General.ConfirmUpdateDBChangesActiveBranch();
                    InitializeProgressBar();
                    General.OnProgress -= new EventHandler<ProgressStatusEventArgs>(General_OnProgress);
                    General.OnProgress += new EventHandler<ProgressStatusEventArgs>(General_OnProgress);
                    if (General.UpdateFiles())
                    {
                        Application.DoEvents();
                        if (General.ACMEERP_IS_SQLSCRIPTAVILABLE)
                        {
                            if (General.TestConnection())
                            {
                                if (isMultiDatabaseLicense) //If multi database, default database (acperp) will be available
                                {
                                    dtMultiDatabases = General.GetDatabasesfromXml();
                                    if (!General.UpdateDBScriptSelectedBranchAlone)
                                    {
                                        if (dtMultiDatabases != null)
                                        {
                                            for (int i = 0; i < dtMultiDatabases.Rows.Count; i++)
                                            {
                                                string ConnectionString = General.ACMEERP_DB_CONNECTION;
                                                string NewConString = changeConnStringItem(ConnectionString, "database", dtMultiDatabases.Rows[i]["Restore_Db"].ToString());
                                                General.ACMEERP_MULTIDB_CONNECTION = NewConString;
                                                General.ACMEERP_MULTIDB_NAME = dtMultiDatabases.Rows[i]["Restore_Db"].ToString();
                                                if (General.UpdateAcMEERPDBchanges())
                                                {
                                                    updatedsucessfully = true;
                                                }
                                                else
                                                {
                                                    updatedsucessfully = true;
                                                    // MessageBox.Show("Could not update SQL script, Contact BoscoSoft Admin", General.ACMEERP_UPDATER_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        string ConnectionString = General.ACMEERP_DB_CONNECTION;
                                        string NewConString = changeConnStringItem(ConnectionString, "database", General.ActiveBranchDB);
                                        General.ACMEERP_MULTIDB_CONNECTION = NewConString;

                                        General.UpdateAcMEERPDBchanges();
                                        updatedsucessfully = true;
                                    }
                                }
                                else   //If not multi database enabled, App.config database will be taken and db changes executed
                                {
                                    if (General.UpdateAcMEERPDBchanges())
                                    {
                                        updatedsucessfully = true;
                                    }
                                    else
                                    {
                                        updatedsucessfully = true;
                                    }
                                }

                                //To update the Config file
                                UpdatedConfigFile();
                            }
                            else
                            {
                                updatedsucessfully = true;
                                // MessageBox.Show("DB connection failure, could not update SQL script, Contact BoscoSoft Admin", General.ACMEERP_UPDATER_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            updatedsucessfully = true;
                        }

                    }
                    else
                    {
                        MessageBox.Show("Failure in Updating Files", General.ACMEERP_UPDATER_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Acme.erp is running, Close Acme.erp Application", General.ACMEERP_UPDATER_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Acme.erp updater could not find the installed path, Contact Support Team", General.ACMEERP_UPDATER_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (updatedsucessfully)
            {
                ChangeIcon();

                if (isMultiDatabaseLicense && General.UpdateDBScriptSelectedBranchAlone)
                {
                    //MessageBox.Show(General.ACMEERP_MULTIDB_CONNECTION);
                    General.UpdateConnectionString(Path.Combine(General.ACMEERP_INSTALLED_PATH, "ACPP.exe.config"), "AppConnectionString", General.ACMEERP_MULTIDB_CONNECTION);
                }

                //Encrpt config file-------------------------
                General.EncryptConnectionSettings(Path.Combine(General.ACMEERP_INSTALLED_PATH, "ACPP.exe.config"), "connectionStrings");
                //--------------------------------------------
                //TimeSpan ts = DateTime.Now - starttime;
                //Math.Round( ts.TotalMinutes,2).ToString()
                
                //On 05/11/2024, To reassing report properties before updater
                General.ReAssignReportSettingProperties();
                //------------------------------------------------------------


                MessageBox.Show("Successfully Updated Files", General.ACMEERP_UPDATER_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (!isMultiDatabaseLicense || !General.UpdateDBScriptSelectedBranchAlone)
                {
                    Application.Exit();
                    StartAcMEERP();
                }
            }
            else
            {
                Application.Exit();
            }
            this.Cursor = Cursors.Default;
        }

        void General_OnProgress(object sender, ProgressStatusEventArgs e)
        {
            lblStatus.Text = e.Status;
            // pbarUpdater.Maximum = e.NoofUpdates;
            pbarUpdater.PerformStep();
            Application.DoEvents();
        }

        private void InitializeProgressBar()
        {
            pbarUpdater.Value = 0;
            pbarUpdater.Minimum = 0;
            pbarUpdater.Step = 1;
            pbarUpdater.Maximum = GetNoOfUpdates();
        }

        private Int32 GetNoOfUpdates()
        {
            Int32 nooftasks = 0;
            Int32 DbScriptLength = 0;

            //no of files to be updated
            if (General.ACMEERP_UPDATE_FILES != null)
                nooftasks = nooftasks + General.ACMEERP_UPDATE_FILES.Rows.Count;

            //database changes script count
            DbScriptLength = General.GetDbChangesLength();

            //no of dbs updated if multi DB
            if (isMultiDatabaseLicense)
            {
                DataTable dtDatabase = General.GetDatabasesfromXml();
                if (!General.UpdateDBScriptSelectedBranchAlone)
                {
                    if (dtDatabase != null && dtDatabase.Rows.Count > 0)
                    {
                        nooftasks = nooftasks + (dtDatabase.Rows.Count * DbScriptLength);
                    }
                }
                else
                {
                    nooftasks = nooftasks + (1 * DbScriptLength);
                }
            }
            else
            {
                // Default database (acperp) database changes script count is added
                nooftasks = nooftasks + DbScriptLength;
            }
            return nooftasks;
        }

        private void frmUpdater_Shown(object sender, EventArgs e)
        {

            if (General.IS_SILENT_INSTALLATION)
            {
                if (!isMultiDatabaseLicense || (dtMultiDatabases != null && dtMultiDatabases.Rows.Count == 1))
                {
                    DoUpdater();
                }
                else if (cbbrancheslist.Visible)
                {
                    cbbrancheslist.Select();
                    cbbrancheslist.Focus();
                }
            }
        }

        private void StartAcMEERP()
        {
            try
            {
                using (Process myProcess = new Process())
                {
                    myProcess.StartInfo.UseShellExecute = false;
                    myProcess.StartInfo.FileName = Path.Combine(General.ACMEERP_INSTALLED_PATH, "ACPP.exe");
                    myProcess.StartInfo.CreateNoWindow = false;
                    myProcess.Start();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not update latest version ", e.Message);
            }
        }

        private void LoadBranchesList()
        {
            cbbrancheslist.Visible = true;
            cbbrancheslist.Items.Clear();
            if (isMultiDatabaseLicense)
            {
                if (dtMultiDatabases != null && dtMultiDatabases.Rows.Count > 1)
                {
                    DataRow dr = dtMultiDatabases.NewRow();
                    dr["RestoreDBName"] = "All Branches";
                    dr["Restore_Db"] = "";
                    dtMultiDatabases.Rows.InsertAt(dr, 0);
                    cbbrancheslist.DataSource = dtMultiDatabases;
                    cbbrancheslist.DisplayMember = "RestoreDBName";
                    cbbrancheslist.ValueMember = "Restore_Db";

                    string activeBranchDB = General.GetActiveAcmeerpBranchDB();
                    if (!string.IsNullOrEmpty(activeBranchDB) && cbbrancheslist.Items.Count > 0)
                    {
                        cbbrancheslist.SelectedValue = activeBranchDB;
                    }
                }
            }
            else
            {
                cbbrancheslist.Items.Add(new { Text = "acperp", Value = "acperp" });
            }
        }

        private void btnMultiDBUpdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbbrancheslist.Text))
            {
                //MessageBox.Show(cbbrancheslist.SelectedValue.ToString());
                if (General.ConfirmUpdateDBChangesSelectedBranch(cbbrancheslist.Text))
                {
                    pbarUpdater.Visible = true;
                    lblMultiDBDetails.Visible = false;
                    DoUpdater();
                    lblStatus.Text = string.Empty;
                    pbarUpdater.Visible = false;
                    lblMultiDBDetails.Visible = true;
                }
            }
            else
            {
                MessageBox.Show("Select any Branch to update", General.ACMEERP_UPDATER_TITLE);
            }
        }

        private void cbbrancheslist_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnMultiExit_Click(object sender, EventArgs e)
        {
            if (isMultiDatabaseLicense || General.UpdateDBScriptSelectedBranchAlone)
            {
                Application.Exit();
                StartAcMEERP();
            }
        }
    }
}
