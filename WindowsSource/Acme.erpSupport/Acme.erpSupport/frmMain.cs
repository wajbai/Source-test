using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Configuration;


namespace Acme.erpSupport
{
    public partial class frmMain : Form
    {

        public SupportRequest UserRequest
        {
            get
            {
                ResultArgs result = new ResultArgs();
                result = General.GetEnumItemType(cboAction.SelectedIndex.ToString());
                if (result.Success)
                {
                    return (SupportRequest)result.ReturnValue;
                }
                else
                {
                    return SupportRequest.GET_CONNECTION_STRING;
                }
            }
        }

        public frmMain()
        {
            string appName = @"C:\Program Files (x86)\BoscoSoft\Acme.erp\ACPP.exe";

            if (File.Exists(appName))
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(appName);
                ConnectionStringsSection section = config.GetSection("connectionStrings") as ConnectionStringsSection;
                if (!section.SectionInformation.IsProtected)
                {
                    section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                }
                config.Save();
            }

            ResultArgs result = new ResultArgs();
            InitializeComponent();
            this.Text = General.ACMEERP_TITLE;
            cboBranch.Visible = false;
            lblBranch.Visible = false;

            result = General.AssignAcmeerpProperties();      
            if (result.Success)
            {
                lblPathValue.Text = string.Empty;
                lblVersionValue.Text = string.Empty;
                lblLocalBOName.Text = string.Empty;
                result = BindRequests(true);
                if (result.Success)
                {
                    if (General.ACMEERP_IS_INSTALLED)
                    {
                        ConnectAcmeerp(true, "acperp");
                        if (LicenseDetails.AccesstoMultiDB == 1)
                        {
                            BindBranches();
                            cboBranch.Visible = true;
                            lblBranch.Visible = true;
                            
                            int defaultbranch = cboBranch.FindString("acperp");
                            if (!string.IsNullOrEmpty(General.ACMEERP_DB_NAME))
                            {
                                ResultArgs resultarg = General.GetBranches();
                                if (resultarg.Success && resultarg.DataSource.Data != null)
                                {
                                    DataTable dt = resultarg.DataSource.Table;
                                    dt.DefaultView.RowFilter = "RESTORE_DB='" + General.ACMEERP_DB_NAME + "'";
                                    if (dt.DefaultView.Count > 0)
                                    {
                                        string alisbranchname = dt.DefaultView[0]["RestoreDBName"].ToString();
                                        defaultbranch = cboBranch.FindString(alisbranchname);
                                    }
                                }
                            }

                            cboBranch.SelectedIndex = defaultbranch;
                        }
                        lblLocalBOName.Text = LicenseDetails.HeadOfficeCode + " - " + LicenseDetails.BranchOfficeCode + " - " + LicenseDetails.BranchName;
                        result =  FillMySQLAcmeerpDetails();
                    }
                    else
                    {
                        MessageRender.ShowMessage("Acme.erp is not Installed in this system",false);
                    }
                }
            }

            if (!result.Success)
            {
                MessageRender.ShowMessage(result);
                btnOk.Enabled = true;
            }


            if (General.IS_GET_USER_PWD_User)
            {
                int userpasswordindex = cboAction.FindString(SupportRequest.GET_USER_PASSWORD.ToString());
                cboAction.SelectedIndex = userpasswordindex;
                DoAction();
                cboAction.Enabled = false;
                lblRefreshMessage.Visible = false;
            }
            else if  (General.IS_UPDATE_LOCATION_User)
            {
                int userpasswordindex = cboAction.FindString(SupportRequest.UPDATE_LOCATION_IN_SETTING_FROM_LICENSE_KEY.ToString());
                cboAction.SelectedIndex = userpasswordindex;
                DoAction();
                cboAction.Enabled = false;
                lblRefreshMessage.Visible = false;
            }
            else if (General.IS_DELETE_PROJECT_User)
            {
                int userpasswordindex = cboAction.FindString(SupportRequest.DELETE_PROJECT.ToString());
                cboAction.SelectedIndex = userpasswordindex;
                DoAction();
                cboAction.Enabled = false;
                lblRefreshMessage.Visible = false;
            }
            else
            {
                if (cboAction.Items.Count > 0)
                {
                    cboAction.SelectedIndex = 0;
                    DoAction();
                }
            }

            ResizeFormBasedOnAction();
            
            richtxtResults.Select();
            richtxtResults.Focus();
        }

        private void cboBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ( !string.IsNullOrEmpty(cboBranch.Text))
            {
                if (cboBranch.SelectedIndex == 0)
                {
                    ConnectAcmeerp(true, cboBranch.Text);
                }
                else
                {
                    ConnectAcmeerp(false, cboBranch.Text);
                }

                if (gvLicense.DataSource != null)
                {
                    //if (General.IS_GET_USER_PWD_User)
                    //{
                    //    int userpasswordindex = cboAction.FindString(SupportRequest.GET_USER_PASSWORD.ToString());
                    //    cboAction.SelectedIndex = userpasswordindex;
                    //}
                    //else if (General.IS_UPDATE_LOCATION_User)
                    //{
                    //    int userpasswordindex = cboAction.FindString(SupportRequest.UPDATE_LOCATION_IN_SETTING_FROM_LICENSE_KEY.ToString());
                    //    cboAction.SelectedIndex = userpasswordindex;
                    //}
                    //else if (General.IS_DELETE_PROJECT_User)
                    //{
                    //    int userpasswordindex = cboAction.FindString(SupportRequest.DELETE_PROJECT.ToString());
                    //    cboAction.SelectedIndex = userpasswordindex;
                    //}
                    //else
                    //{
                    //    cboAction.SelectedIndex = 0;
                    //}

                    richtxtResults.Text = string.Empty;
                    DoAction();
                }
                else
                {
                    cboBranch.SelectedIndex = 0;
                }
            }
        }

        private void cbFDProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindFDAccounts();
        }
        
        private void btnConnectionStringUpdate_Click(object sender, EventArgs e)
        {
            ResultArgs result = new ResultArgs();
            MySqlConnectionStringBuilder acmeerp_conn_string;
            bool isValidConnectionString = false;
            try
            {
                acmeerp_conn_string = new MySqlConnectionStringBuilder(txtConnectionString.Text);
                isValidConnectionString = true;
            }
            catch (Exception err)
            {
                isValidConnectionString = false;
            }

            if (isValidConnectionString)
            {
                if (MessageBox.Show("Are you sure to update '" + txtConnectionString.Text + "' DB Connection string in Acmeerp config file ?",
                                General.ACMEERP_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    result = General.UpdateConnectionString(txtConnectionString.Text);
                    if (result.Success)
                    {
                        richtxtResults.Text = result.ReturnValue.ToString();
                    }
                }
            }
            else
            {
                MessageRender.ShowMessage("InValid Connection String");
                txtConnectionString.Select();
                txtConnectionString.Focus();
            }

            if (!result.Success)
            {
                MessageRender.ShowMessage(result);
            }
        }

        private void btnFDDelete_Click(object sender, EventArgs e)
        {
            if (cboFD.SelectedIndex > -1)
            {
                string fdaccount = cboFD.Text;
                Int32 fdaccountid = Int32.Parse(cboFD.SelectedValue.ToString());
                if (fdaccountid > 0)
                {
                    if (MessageBox.Show("Are you sure to delete FD Account '" + fdaccount + "' and its renew details",
                        General.ACMEERP_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        ResultArgs result = new ResultArgs();
                        result = General.DeleteFD(fdaccountid);
                        if (result.Success)
                        {
                            this.richtxtResults.Text = "'" + fdaccount + "' is removed";
                            BindFDAccounts();
                        }
                        this.Cursor = Cursors.Default;

                        if (!result.Success)
                        {
                            MessageRender.ShowMessage(result);
                        }
                    }
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DoAction();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void ResizeFormBasedOnAction()
        {
            SupportRequest selectedrequest = UserRequest;
            switch (selectedrequest)
            {
                case SupportRequest.UPDATE_CONNECTION_STRING:
                    gboxConnectionString.Top = pnlAcmeerp.Top + pnlAcmeerp.Height + 5;
                    btnClose.Top = gboxConnectionString.Top + gboxConnectionString.Height;
                    break;
                case SupportRequest.ACMEERP_SERVICE_INFO:
                    gboxAcmeerpService.Left = pnlAcmeerp.Left;
                    gboxAcmeerpService.Top = pnlAcmeerp.Top + pnlAcmeerp.Height + 5;
                    btnClose.Top = gboxAcmeerpService.Top + gboxAcmeerpService.Height;
                    break;
                case SupportRequest.DELETE_PROJECT:
                    gboxDeleteProject.Top = pnlAcmeerp.Top + pnlAcmeerp.Height+5;
                    btnClose.Top = gboxDeleteProject.Top + gboxDeleteProject.Height;
                    break;
                case SupportRequest.DELETE_FD_BY_PROJECTS:
                    gboxDeleteFD.Top = pnlAcmeerp.Top + pnlAcmeerp.Height + 5;
                    btnClose.Top = gboxDeleteFD.Top + gboxDeleteFD.Height;
                    break;
                case SupportRequest.DELETE_FD_ALL:
                    gboxDeleteFDAll.Left = pnlAcmeerp.Left;
                    gboxDeleteFDAll.Top = pnlAcmeerp.Top + pnlAcmeerp.Height + 5;
                    btnClose.Top = gboxDeleteFDAll.Top + gboxDeleteFDAll.Height;
                    break;
                case SupportRequest.UPDATE_LOCATION_IN_SETTING_FROM_LICENSE_KEY:
                    gboxUpdateLocation.Left = pnlAcmeerp.Left;
                    gboxUpdateLocation.Top = pnlAcmeerp.Top + pnlAcmeerp.Height + 5;
                    btnClose.Top = gboxUpdateLocation.Top + gboxUpdateLocation.Height;
                    break;
                case SupportRequest.CLEAR_AND_RESET_VOUCHERS:
                    gboxResetDBVouchers.Left = pnlAcmeerp.Left;
                    gboxResetDBVouchers.Top = pnlAcmeerp.Top + pnlAcmeerp.Height + 5;
                    btnClose.Top = gboxResetDBVouchers.Top + gboxResetDBVouchers.Height;
                    break;
                case SupportRequest.RESTORE_DB_BACKUP:
                     gboxRestoreDatabase.Left = pnlAcmeerp.Left;
                     gboxRestoreDatabase.Top = pnlAcmeerp.Top + pnlAcmeerp.Height + 5;
                     btnClose.Top = gboxRestoreDatabase.Top + gboxRestoreDatabase.Height;
                     break;
                default:
                    btnClose.Top = pnlAcmeerp.Top + pnlAcmeerp.Height;
                    break;
            }
            
            btnClose.Top = btnClose.Top + 5;
            lblLocalBOName.Top = btnClose.Top;
            lblRefreshMessage.Top = lblLocalBOName.Top + lblLocalBOName.Height+8;
            lblRefreshMessage.Left = lblLocalBOName.Left;
            //this.Height = btnClose.Top + btnClose.Height + 45;
            this.Height = lblRefreshMessage.Top + lblRefreshMessage.Height + 35;
            this.Width = pnlAcmeerp.Left + pnlAcmeerp.Width + 12;
            this.CenterToScreen();
        }

        private void btnRemoveProject_Click(object sender, EventArgs e)
        {
            if (cboProject.SelectedIndex > -1)
            {
                string projectname = cboProject.Text;
                Int32 projectid = Int32.Parse(cboProject.SelectedValue.ToString());
                if (projectid > 0)
                {
                    if (MessageBox.Show("Are you sure to delete all entries in '" + projectname + "'",
                        General.ACMEERP_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        ResultArgs result = new ResultArgs();
                        result = General.DeleteProject(projectid);
                        if (result.Success)
                        {
                            this.richtxtResults.Text = "'" + projectname + "' is removed";
                            result =  BindProject(cboProject);
                        }
                        this.Cursor = Cursors.Default;

                        if (!result.Success)
                        {
                            MessageRender.ShowMessage(result);
                        }
                    }
                }
            }
        }

        private ResultArgs BindProject(ComboBox cbProjectControl)
        {
            ResultArgs result = new ResultArgs();
            this.Cursor = Cursors.WaitCursor;
            result = General.GetProjects();
            if (result.Success)
            {
                DataView dv = result.DataSource.Data as DataView;
                if (dv != null)
                {
                    cbProjectControl.ValueMember = "PROJECT_ID";
                    cbProjectControl.DisplayMember = "PROJECT_NAME";
                    cbProjectControl.DataSource = dv;
                    
                    this.Cursor = Cursors.Default;
                }
            }
           
            return result;
        }

        private ResultArgs BindLocations(ComboBox cbLocationControl)
        {
            ResultArgs result = new ResultArgs();
            this.Cursor = Cursors.WaitCursor;
            result = General.GetLocations();
            if (result.Success)
            {
                DataTable dt = result.DataSource.Data as DataTable;
                if (dt != null)
                {
                    cbLocationControl.ValueMember = "LOCATION_NAME";
                    cbLocationControl.DisplayMember = "LOCATION_NAME";
                    cbLocationControl.DataSource = dt;
                    cbLocationControl.Text = lblLocationValue.Text;
                    this.Cursor = Cursors.Default;
                }
            }

            return result;
        }

        private ResultArgs BindRecentAY()
        {
            ResultArgs result = new ResultArgs();
            this.Cursor = Cursors.WaitCursor;
            result = General.GetRecentFY();
            if (result.Success)
            {
                DataTable dt = result.DataSource.Data as DataTable;
                if (dt != null)
                {
                    dateFrom.Text = dt.Rows[0]["YEAR_FROM"].ToString();
                    dateTo.Text = dt.Rows[0]["YEAR_TO"].ToString();
                    //dateBooksBegin.Text = dt.Rows[0]["BOOKS_BEGINNING_FROM"].ToString();
                    dateBooksBegin.Text = dt.Rows[0]["YEAR_FROM"].ToString();
                    this.Cursor = Cursors.Default;
                }
            }

            return result;
        }

        
        private void BindFDAccounts()
        {
            ResultArgs result = new ResultArgs();
            string projectname = cbFDProject.Text;
            btnFDDelete.Enabled = false;
            if (cbFDProject.SelectedValue != null)
            {
                Int32 projectid = Int32.Parse(cbFDProject.SelectedValue.ToString());
                if (projectid > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    result = General.GetFDAccounts(projectid);
                    if (result.Success)
                    {
                        cboFD.DataSource = result.DataSource.Data as DataView;
                        cboFD.DisplayMember = "fd_account_number";
                        cboFD.ValueMember = "fd_account_id";
                        this.Cursor = Cursors.Default;
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        MessageRender.ShowMessage(result);
                    }
                }
            }
   
            btnFDDelete.Enabled = (cboFD.Items.Count > 0);
        }

        private void FillAcmerpDetails()
        {
            if (General.ACMEERP_IS_INSTALLED)
            {
                ResultArgs result = new ResultArgs();
                lblPathValue.Text = General.ACMEERP_INSTALLED_PATH;
                lblVersionValue.Text = General.ACMEERP_VERSION;
                result = General.GetDBconnectionstring();
                if (result.Success)
                {
                    string connectionstring = result.ReturnValue.ToString();
                    MySql.Data.MySqlClient.MySqlConnectionStringBuilder mysqlconnectionstring = new MySql.Data.MySqlClient.MySqlConnectionStringBuilder(connectionstring);
                    lblDBserverValue.Text = mysqlconnectionstring.Server;
                    lblPortValue.Text = mysqlconnectionstring.Port.ToString();

                    if (cboBranch.Visible)
                    {
                        mysqlconnectionstring.Database = cboBranch.SelectedValue.ToString();
                    }

                    General.ACMEERP_DB_CONNECTION = mysqlconnectionstring.ConnectionString;

                    lblDBNameValue.Text = mysqlconnectionstring.Database;

                    result = General.GetCurrentLocation();
                    if (result.Success)
                    {
                        lblLocationValue.Text = result.ReturnValue.ToString();
                    }
                    else
                    {
                        MessageRender.ShowMessage(result);
                    }
                }
                else
                {
                    MessageRender.ShowMessage(result);
                }


                richtxtResults.Text = string.Empty;
            }
        }

        /// <summary>
        /// Method to load multi database name 
        /// </summary>
        private void BindBranches()
        {
            try
            {
                ResultArgs result = new ResultArgs();
                result = General.GetBranches();
                if (result.Success)
                {
                    cboBranch.DisplayMember = "RestoreDBName";
                    cboBranch.ValueMember = "Restore_DB";
                    cboBranch.DataSource = result.DataSource.Data as DataTable;
                }
                else
                {
                    MessageRender.ShowMessage(result);
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage("Error in loading multi branch " + Ex.Message);
            }
            finally { }
        }

        private ResultArgs BindRequests(bool bindonlyserviceinfo)
        {
            ResultArgs result = new ResultArgs();
            result = General.GetEnumDataSource();
            DataView dvSupportRequest =null;
            if (result.Success)
            {
                dvSupportRequest = result.DataSource.Data as DataView;
            }

            if (dvSupportRequest != null)
            {
                result.Success = true;
            }
            
            cboAction.DataSource = dvSupportRequest.ToTable(); ;
            cboAction.ValueMember = "Id";
            cboAction.DisplayMember = "Name";
            if (bindonlyserviceinfo)
            {
                cboAction.SelectedIndex = (int)SupportRequest.ACMEERP_SERVICE_INFO;
            }
            else
            {
                cboAction.SelectedIndex = 0;
            }
            DoAction();
            return result;
        }

        private void ConnectAcmeerp(bool IsLocalBranch, string branch)
        {
            ResultArgs result = new ResultArgs();
            string LicenseFile = "AcMEERPLicense.xml";

            this.Cursor = Cursors.WaitCursor;

            if (!IsLocalBranch)
            {
                LicenseFile = General.GetBranchLicensefile(branch);    
            }
            result = LicenseDetails.GetLicenseDetailsInfo(LicenseFile);


            if (result.Success)
            {
                DataView dv = result.DataSource.Data as DataView;
                gvLicense.DataSource = dv;
                FillAcmerpDetails();
                gvLicense.AutoResizeColumns();
                gvLicense.ColumnHeadersDefaultCellStyle.Font = new Font(gvLicense.ColumnHeadersDefaultCellStyle.Font,FontStyle.Bold);
                gvLicense.RowHeadersVisible = false;
                btnOk.Enabled = true;
                this.Cursor = Cursors.Default;
                if (result.Success)
                {
                    result = General.CreateSupportSPs();
                }
            }
            else
            {
                gvLicense.DataSource = null;
                
                btnOk.Enabled = false;
            }

            if (!result.Success)
            {
                this.Cursor = Cursors.Default;
                string errormessage = result.Message;
                if (errormessage.Contains("Unable to connect to any of the specified MySQL hosts"))
                {
                    MessageRender.ShowMessage("Could not connect Database server, Check MySQLAcperp Service is running or Check DataBase Server is reachable");
                }
                else
                {
                    MessageRender.ShowMessage(result);
                }
            }

        }
               
        private void DoAction()
        {
            ResultArgs result = new ResultArgs();
            richtxtResults.Text = string.Empty;
            if (General.ACMEERP_IS_INSTALLED)
            {
                gboxDeleteProject.Visible = false;
                gboxDeleteFD.Visible = false;
                gboxConnectionString.Visible = false;
                gboxAcmeerpService.Visible = false;
                gboxDeleteFDAll.Visible = false;
                gboxUpdateLocation.Visible = false;
                gboxResetDBVouchers.Visible = false;
                gboxRestoreDatabase.Visible = false;

                SupportRequest selectedrequest = UserRequest;
                
                this.Cursor = Cursors.WaitCursor;
                switch (selectedrequest)
                {
                    case SupportRequest.GET_CONNECTION_STRING:  //Get Connection string
                        richtxtResults.Text = General.ACMEERP_DB_CONNECTION;
                        break;
                    case SupportRequest.UPDATE_CONNECTION_STRING:  //Get Connection string
                        txtConnectionString.Text = General.ACMEERP_DB_CONNECTION;
                        gboxConnectionString.Visible = true;
                        break;
                    case SupportRequest.UPDATE_ACPERP_ROOT_PASSWORD:
                        result = General.UpdateAcmeerpRootPassword();
                        if (result.Success)
                        {
                            richtxtResults.Text = result.ReturnValue.ToString();
                        }
                        break;
                    case SupportRequest.ACMEERP_SERVICE_INFO:
                        gboxAcmeerpService.Visible = true;
                        result = FillMySQLAcmeerpDetails();
                        break;
                    case SupportRequest.TEST_CONNECTION:        //Test Connection
                        result = General.TestConnection();
                        if (result.Success)
                        {
                            richtxtResults.Text = result.ReturnValue.ToString();
                        }
                        break;
                    case SupportRequest.GET_USER_PASSWORD:      //User info
                        if (General.IS_ADMIN_User || General.IS_GET_USER_PWD_User)
                        {
                            result = General.GetUserInfo();
                            if (result.Success)
                            {
                                richtxtResults.Text = result.ReturnValue.ToString();
                            }
                        }
                        else
                        {
                            MessageRender.ShowMessage("Access denied");
                            richtxtResults.Focus();
                        }
                        break;
                    case SupportRequest.DELETE_PROJECT:         //Delete Project:
                        gboxDeleteProject.Visible = true;
                        result = BindProject(cboProject);
                        btnRemoveProject.Enabled = (cboProject.Items.Count > 0);
                        break;
                    case SupportRequest.DELETE_FD_BY_PROJECTS:          //Delete FD:
                        result = BindProject(cbFDProject);
                        gboxDeleteFD.Visible = true;
                        break;
                    case SupportRequest.DELETE_FD_ALL:          //Delete all FD:
                        result.Success = true;
                        gboxDeleteFDAll.Visible = true;
                        break;
                    case SupportRequest.DELETE_ALL_VOUCHERS_WHICH_ARE_OUT_OF_BOOKS_BEGIN: //Delete Vouchers which are out of books begin
                        result = General.DeleteVouchersOutofBooksBegin();
                        richtxtResults.Text = result.Message;
                        break;
                    case SupportRequest.UPDATE_LOCATION_IN_SETTING_FROM_LICENSE_KEY: //Upate proper location to Acmeerp setting from License Key
                        gboxUpdateLocation.Visible = true;
                        result = BindLocations(cboLocations);
                        btnUpdateLocation.Enabled = (cboLocations.Items.Count > 0);
                        break;
                    case SupportRequest.CLEAR_AND_RESET_VOUCHERS: //Clear and Rest Vouchers tables and rest auto number
                        gboxResetDBVouchers.Visible = true;
                        dateFrom.Format = dateTo.Format = dateBooksBegin.Format = DateTimePickerFormat.Custom;
                        dateFrom.CustomFormat = dateTo.CustomFormat = dateBooksBegin.CustomFormat = "dd/MM/yyyy";
                        lblNote.Text = "* All Vouchers will be cleared." + Environment.NewLine +
                                            "  All Master will be remain same." + Environment.NewLine + 
                                            "  Above FY will be created newly.";
                        BindRecentAY();
                        result.Success = true;; //result = BindLocations(cboLocations);
                        break;
                    case SupportRequest.RESTORE_DB_BACKUP: //Restore db backup
                        gboxRestoreDatabase.Visible = true;
                        result.Success = true;
                        break;
                    default:
                        result.Success = true;
                        richtxtResults.Text = "Yet to be developed";
                        break;
                }

                if (!result.Success)
                {
                    MessageRender.ShowMessage(result);
                }
                ResizeFormBasedOnAction();
                this.Cursor = Cursors.Default;
            }
            else
            {
                MessageBox.Show("Acme.erp is not installed in this system", General.ACMEERP_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            richtxtResults.Select();
            richtxtResults.Focus();
        }

        private ResultArgs FillMySQLAcmeerpDetails()
        {
            ResultArgs result = new ResultArgs();
            
            //Fill MySQLACPER service details
            lblServiceInstalledValue.Text = "No";
            lblServiceStatusValue.Text = "No";
            lblServiceINIValue.Text = string.Empty;
            lblServicePortValue.Text = "-1";
            lblMySQLInstalledPathValue.Text = string.Empty;
            
            result = AcmeerpMySQLServiceDetails.AssignMyServiceProperties();
            if (result.Success)
            {
                if (AcmeerpMySQLServiceDetails.MYSERVICE_INSTALLED)
                    lblServiceInstalledValue.Text = "Yes";
                
                lblServiceStatusValue.Text = AcmeerpMySQLServiceDetails.MYSERVICE_STATUS.ToString();
                lblServiceINIValue.Text = AcmeerpMySQLServiceDetails.MYSERVICE_INI_PATH;
                lblServicePortValue.Text = AcmeerpMySQLServiceDetails.MYSERVICE_PORT.ToString();
                lblMySQLInstalledPathValue.Text = AcmeerpMySQLServiceDetails.MYSQL_INSTALLED_PATH;
            }

            btnServiceAction.Left = lblServiceStatusValue.Left + lblServiceStatusValue.Width;
            btnINIOpen.Left = lblServiceINIValue.Left + lblServiceINIValue.Width;
            btnINIOpen.Enabled = File.Exists(AcmeerpMySQLServiceDetails.MYSERVICE_INI_PATH);
            btnServiceAction.Text = (AcmeerpMySQLServiceDetails.MYSERVICE_STATUS==System.ServiceProcess.ServiceControllerStatus.Running ? "Stop": "Start");
            btnServiceAction.Enabled = AcmeerpMySQLServiceDetails.MYSERVICE_INSTALLED;
            btnCreateService.Left = btnServiceAction.Left;
            btnRemoveService.Left = btnServiceAction.Left;
            btnRemoveService.Enabled = (AcmeerpMySQLServiceDetails.MYSERVICE_INSTALLED);
            btnCreateService.Enabled = !btnRemoveService.Enabled;
            return result;
        }

        private void btnINIOpen_Click(object sender, EventArgs e)
        {
            if (File.Exists(AcmeerpMySQLServiceDetails.MYSERVICE_INI_PATH))
            {
                if (MessageBox.Show("Do you want to open ini file ?",
                                General.ACMEERP_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    Process.Start("notepad.exe", AcmeerpMySQLServiceDetails.MYSERVICE_INI_PATH);
                }
            }
        }

        private void btnServiceAction_Click(object sender, EventArgs e)
        {
            ResultArgs result = new ResultArgs();
            bool servicestatus = !(AcmeerpMySQLServiceDetails.MYSERVICE_STATUS == System.ServiceProcess.ServiceControllerStatus.Running);
            if (MessageBox.Show("Are you sure to "+ (servicestatus? "Start" : "Stop" ) +" Acmeerp MySQL Service ?",
                                General.ACMEERP_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                this.Cursor = Cursors.WaitCursor;
                result = AcmeerpMySQLServiceDetails.ChangeServiceStatus(servicestatus);
                if (result.Success)
                {
                    FillMySQLAcmeerpDetails();
                    richtxtResults.Text = "Acmeerp MySQL Service is " + (servicestatus ? "Started" : "Stopped");
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    MessageRender.ShowMessage(result);
                }
            }
        }

        private void btnRemoveService_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete Acmeerp MySQL Service ?",
                                General.ACMEERP_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                ResultArgs result = new ResultArgs();
                this.Cursor = Cursors.WaitCursor;
                result =  AcmeerpMySQLServiceDetails.RemoveMyService();
                if (result.Success)
                {
                    FillMySQLAcmeerpDetails();
                    richtxtResults.Text = "Acmeerp MySQL Service is Removed";
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    this.Cursor = Cursors.WaitCursor;
                    MessageRender.ShowMessage(result);
                }
            }
        }

        private void btnCreateService_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to create Acmeerp MySQL Service ?",
                                General.ACMEERP_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                this.Cursor = Cursors.Default;
                ResultArgs result = new ResultArgs();
                result = AcmeerpMySQLServiceDetails.OpenDBConfigure();
                if (result.Success)
                {
                    FillMySQLAcmeerpDetails();
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    MessageRender.ShowMessage(result);
                }

                this.Cursor = Cursors.Default;
            }
        }

        private void btnDeleteAllFDs_Click(object sender, EventArgs e)
        {
            if (UserRequest == SupportRequest.DELETE_FD_ALL)
            {
                if (MessageBox.Show("Are you sure to delete all FD Accounts and its opening and renew details in Acmeerp ?",
                    General.ACMEERP_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    ResultArgs result = new ResultArgs();
                    result = General.DeleteAllFD();
                    if (result.Success)
                    {
                        this.richtxtResults.Text = "All FD vouchers and details are removed";
                    }
                    this.Cursor = Cursors.Default;

                    if (!result.Success)
                    {
                        MessageRender.ShowMessage(result);
                    }
                }
            }
        }

        private void btnUpdateLocation_Click(object sender, EventArgs e)
        {
            if (cboLocations.Items.Count > 0 && !string.IsNullOrEmpty(cboLocations.Text))
            {
                if (MessageBox.Show("Are you sure to chnage/update current location from '" + lblLocationValue.Text + "' to '" + cboLocations.Text + "'",
                            General.ACMEERP_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    ResultArgs result =  General.UpdateCurrentLocation(cboLocations.Text.Trim());

                    if (result.Success)
                    {
                        this.richtxtResults.Text = "Current Location '" + cboLocations.Text + "' is updated in Acmeerp";
                        this.richtxtResults.Focus();

                        result = General.GetCurrentLocation();
                        if (result.Success)
                        {
                            lblLocationValue.Text = result.ReturnValue.ToString();
                        }
                        else
                        {
                            MessageRender.ShowMessage(result);
                        }

                    }
                    else
                    {
                        this.richtxtResults.Text = string.Empty;
                        MessageRender.ShowMessage(result);
                    }
                }
            }
        }

        private void dateFrom_ValueChanged(object sender, EventArgs e)
        {
            dateTo.Text = dateFrom.Value.AddMonths(12).AddDays(-1).ToShortDateString();
            dateBooksBegin.Text = dateFrom.Text;
        }

        private void btnResetDBVoucher_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to Clear and Reset all Vouchers ?",
                           General.ACMEERP_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                this.Cursor = Cursors.WaitCursor;
                ResultArgs result = new ResultArgs();

                result = General.ClearResetVouchers(dateFrom.Value.Date, dateTo.Value.Date, dateBooksBegin.Value.Date, chkClearOP.Checked);
                if (result.Success)
                {
                    this.richtxtResults.Text = "All Vouchers and FY are cleared";
                }
                this.Cursor = Cursors.Default;

                if (!result.Success)
                {
                    MessageRender.ShowMessage(result);
                }
            }
        }

        private void btnFileBrowse_Click(object sender, EventArgs e)
        {
            string DBBackupFilePath = string.Empty;
            openFileDBBackup.FileName = string.Empty;
            openFileDBBackup.DefaultExt = "sql";
            openFileDBBackup.Filter = "Acmeerp Database Backup files (*.Sql)|*.Sql";
            openFileDBBackup.Title = "Acmeerp Database Backup";
            if (openFileDBBackup.ShowDialog() == DialogResult.OK)
            {
                DBBackupFilePath = openFileDBBackup.FileName;
                DBBackupFilePath = Path.ChangeExtension(DBBackupFilePath, ".sql");
                txtDBbackupPath.Text = string.Empty;
                if (!string.IsNullOrEmpty(DBBackupFilePath) && File.Exists(DBBackupFilePath))
                {
                    txtDBbackupPath.Text = DBBackupFilePath;
                }
                else
                {
                    MessageRender.ShowMessage("Acmeerp Database Backup not found");
                }
            }
        }

        private void btnDBRestore_Click(object sender, EventArgs e)
        {
            string DBBackupFilePath = txtDBbackupPath.Text;
            if (File.Exists(DBBackupFilePath))
            {
                string NewDBName = txtNewSchema.Text.Trim();
                string Message = "Are you sure to Restore/Overwrite current Database '" + lblDBNameValue.Text + "' ?";
                string RestoreDBName = lblDBNameValue.Text;
                if (!string.IsNullOrEmpty(NewDBName))
                {
                    Message = "Are you sure to Restore new Database '" + NewDBName + "' ?";
                    RestoreDBName = NewDBName;
                }

                if (MessageBox.Show(Message, General.ACMEERP_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    DateTime dtStartTime = DateTime.Now;
                    ResultArgs result = General.RestoreDBWithMySQLCommand(RestoreDBName, DBBackupFilePath);
                    this.Cursor = Cursors.Default;
                    if (result.Success)
                    {
                        DateTime dtEndTime = DateTime.Now;
                        double diffMinutes = dtEndTime.Subtract(dtStartTime).TotalSeconds/60;
                        this.richtxtResults.Text = "Acmeerp Database is restored successfully.";
                        this.richtxtResults.Text += Environment.NewLine + "Start Time : " + dtStartTime.ToString("hh:mm:ss tt") ;
                        this.richtxtResults.Text += " End Time " + dtEndTime.ToString("hh:mm:ss tt");
                        this.richtxtResults.Text += Environment.NewLine + "Taken time duration : " + Math.Round(diffMinutes,2).ToString() + " Minutes";
                        MessageRender.ShowMessage("Acmeerp Database is restored successfully.");
                    }
                    else
                    {
                        MessageRender.ShowMessage(result.Message);
                    }
                }
                txtNewSchema.Text = string.Empty;
            }
            else
            {
                MessageRender.ShowMessage("Acmeerp Database Backup path is not found");
            }
        }
        
    }
}
