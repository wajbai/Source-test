/*************************************************************************************************************************
 *                                              Purpose     :Connectin setup and test connectin and data migration
 *                                                           from MS Access DB(AcMe+) to MySQL(AcMe++)
 *                                              Author      :Carmel Raj M
 *                                              Date        :21-Jan-2014
 *                                              Modified On :03-Jun-2014
 *                                              Modified By :Carmel Raj M
 *                                              Reviewed By :
 *************************************************************************************************************************/
using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Data.OleDb;
using DevExpress.XtraLayout.Utils;
using Bosco.DAO;
using Bosco.Utility.ConfigSetting;
using Bosco.Utility;
using Bosco.Model.TallyMigration;

namespace ACPP.Modules.Data_Utility
{
    public partial class DataMigrationConnectionSetUp : frmFinanceBaseAdd
    {
        #region Variables
        AcMePlusMigrationSystem AcMeplusMigration;
        //  DataView dvSettings;
        //  DataTable dtAcMeData;
        OleDbConnection OleDbConn;
        DataTable dtYear;
        bool ConnectinSucceed = false;
        bool OverWrite = true;
        bool IsFirstMigration = false;
        string Query = string.Empty;
        string tempQuery = string.Empty;
        string ValueField = string.Empty;
        string MigrationStartTime = string.Empty;
        //   int ImportedRowCount = 0;
        int MasterCount = 0;
        int MappingCount = 0;
        int ProjectCount = 0;
        int TransactionCount = 0;
        //   int ZeroRow = 0;
        // int CashId = 1;//Cash ID is always 1
        // int MasterProjectCount = 0;

        private DateTime StartTime;
        private TimeSpan CurrentElapsedTime = TimeSpan.Zero;
        private TimeSpan TotalElapsedTime = TimeSpan.Zero;
        #endregion

        #region Constructor
        public DataMigrationConnectionSetUp()
        {
            InitializeComponent();
        }
        #endregion

        #region Connectin Methods
        private bool ValidateConnectionProperty()
        {
            bool IsValid = true;
            if (string.IsNullOrEmpty(txtAcMePlusDB.Text))
            {
                XtraMessageBox.Show(this.GetMessage(MessageCatalog.DataMigration.MIGRATION_ACCESS_DB_EMPTY), this.GetMessage(MessageCatalog.DataMigration.MIGRATION_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAcMePlusDB.Focus();
                IsValid = false;
            }
            return IsValid;
        }

        private void DisableControls()
        {
            txtAcMePlusDB.Enabled = rgMigrationType.Enabled = btnSelectAcMeDB.Enabled = btnTestConnection.Enabled = glkpAcYears.Enabled =
                deDateFrom.Enabled = deDateTo.Enabled = false;
        }
        #endregion

        #region Connection Events
        private void DataMigrationConnectionSetUp_Load(object sender, EventArgs e)
        {
            this.Size = new Size(480, 175);//426, 221
        }

        private void MigrationTypeAllResize_Visible()
        {
            LayoutProgress.Visibility = LayoutlblMessage.Visibility = LayoutVisibility.Always;
            this.Size = new Size(486, 226);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var timeSinceStartTime = DateTime.Now - StartTime;
            timeSinceStartTime = new TimeSpan(timeSinceStartTime.Hours, timeSinceStartTime.Minutes, timeSinceStartTime.Seconds);
            CurrentElapsedTime = timeSinceStartTime + TotalElapsedTime;
            lblTimer.Text = CurrentElapsedTime.ToString();
        }


        private void rgMigrationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rgMigrationType.SelectedIndex == 1) EnableByPeriodControl(); else DisableByPeriodControl();
        }

        private void glkpAcYears_EditValueChanged(object sender, EventArgs e)
        {
            SetMinDateAndMaxDate(true);
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            ConnectinSucceed = TestConnection();
        }

        private bool TestConnection()
        {
            ConnectinSucceed = false;
            if (ValidateConnectionProperty())
            {
                //Reading Connection Format for Ole DB(MS Access DB)
                string OleDBCon = ConfigurationManager.AppSettings.Get("OleDBConFormat");
                if (!string.IsNullOrEmpty(OleDBCon))
                {
                    OleDBCon = OleDBCon.Replace("@AccessDB@", txtAcMePlusDB.Text);
                    //Constructing Ole DB Connection String
                    ConfigurationManager.AppSettings.Set("OleDBConStr", OleDBCon);
                    AcMeplusMigration = new AcMePlusMigrationSystem();
                    using (DataAccess RefDataAccess = new DataAccess())
                    {
                        if (RefDataAccess.OleDbTestConnection())
                        {
                            if (RefDataAccess.TestConnection())
                            {
                                ConnectinSucceed = true;
                                XtraMessageBox.Show(this.GetMessage(MessageCatalog.DataMigration.MIGRATION_CONNECTION_SUCCEED), this.GetMessage(MessageCatalog.DataMigration.MIGRATION_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.AcceptButton = btnMigrateMaster;
                            }
                            else
                                XtraMessageBox.Show(this.GetMessage(MessageCatalog.DataMigration.MIGRATION_DESTINATION_DB_FAILS), this.GetMessage(MessageCatalog.DataMigration.MIGRATION_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            XtraMessageBox.Show(this.GetMessage(MessageCatalog.DataMigration.MIGRATION_SOURCE_DB_FAILS), this.GetMessage(MessageCatalog.DataMigration.MIGRATION_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    //XtraMessageBox.Show("OleDbConnection tag is missing in app.config file", this.GetMessage(MessageCatalog.DataMigration.MIGRATION_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.DataUtilityForms.OLEDB_CONN_TAG_MISSING_IN_APPCONFIG_FILE), this.GetMessage(MessageCatalog.DataMigration.MIGRATION_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            return ConnectinSucceed;
        }

        private void btnSelectAcMeDB_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog() { Filter = "MS Access DB Files (*.mdb)|*.mdb" };
            if (DialogResult.OK == file.ShowDialog())
            {
                if (!string.IsNullOrEmpty(file.FileName))
                {
                    txtAcMePlusDB.Text = file.FileName;
                    if (rgMigrationType.SelectedIndex == 1)
                        LoadAvailableAccountingYear();
                }
                else
                    ShowMessageBox(GetMessage(MessageCatalog.DataMigration.MIGRATION_FILE_EMPTY));
            }
        }

        private void DataMigrationConnectionSetUp_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (SettingProperty.Is_Application_Logout)
                Application.Restart();
            else
                this.Close();
        }
        #endregion

        #region Migration Events
        private void btnMigrateMaster_Click(object sender, EventArgs e)
        {
            if (btnMigrateMaster.Text.Equals("&Close"))
            {
                SettingProperty.Is_Application_Logout = true;
                this.Close();
            }
            else
            {
                if (TestConnection())
                {
                    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.DataMigration.MIGRATION_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using (AcMePlusMigrationSystem acmeplusmigration = new AcMePlusMigrationSystem())
                        {
                            if (acmeplusmigration.CheckValidFYPeriod())
                            {
                                bool allyears = (rgMigrationType.SelectedIndex == 1) ? false: true;
                                Int32 fyyearid = 0;
                                if (!allyears)
                                {
                                    fyyearid = glkpAcYears.EditValue == null ? 0 : UtilityMember.NumberSet.ToInteger(glkpAcYears.EditValue.ToString());
                                }

                                if (!acmeplusmigration.IsAuditVouchersLocked(allyears, fyyearid))
                                {
                                    DisableControls();
                                    MigrationTypeAllResize_Visible();
                                    btnMigrateMaster.Enabled = false;
                                    Migrate();
                                    btnMigrateMaster.Enabled = true;
                                    SettingProperty.Is_Application_Logout = true;
                                }
                            }
                            else
                            {
                                this.ShowMessageBox("Acmeerp and Acmeplus Finance Year Period (April-March/January-December) differs. Change Acmeerp Finance Period.");
                            }
                        }
                    }
                }
                else
                    ShowMessageBox(GetMessage(MessageCatalog.DataMigration.MIGRATION_CONNECTION_FAILED));
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.F3))
            {
                deDateFrom.Focus();
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        #endregion

        #region Methods

        #region Transaction Flow
        private void Migrate()
        {
            try
            {
                chkClearSummary.Enabled = chkClearLog.Enabled = false;
                DataAccess RefAccess = new DataAccess();
                RefAccess.Logger("None", string.Empty, true, "Stated On " + DateTime.Now.ToShortDateString(), false);
                StartTimer();

                if (IsMigrated(RefAccess))
                {
                    //string strMessage = String.Format("Record is available,Do you want to delete the records and continue migration?{3}{0}Yes      : Delete and Continue{1}No       : Merge with old records{2}Cancel: Stop Migration.",
                                 //Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine);

                    string strMessage = String.Format(this.GetMessage(MessageCatalog.Master.DataUtilityForms.DELETE_RECORD_AND_CONTINUE_MIGRATION),
                                 Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine);
                    DialogResult result = this.ShowConfirmationMessage(strMessage, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        OverWrite = false;
                        IsFirstMigration = true;
                        AcMeplusMigration.RemovePriviousMigration();
                        AcMeplusMigration.MigrateUsers();
                        AcMeplusMigration.dtMappedLedgers = AcMeplusMigration.GetAllLedgersFromAcMePlus();
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        SettingProperty.Is_Application_Logout = true;
                        this.UseWaitCursor = false;
                        return;
                    }
                    else
                    {
                        frmMapBeforeMigration MapMigration = new frmMapBeforeMigration(AcMeplusMigration.GetAllLedgersFromAcMePlus());
                        MapMigration.ShowDialog();
                        AcMeplusMigration.dtMappedLedgers = MapMigration.dtLedgerToBeMigrated;
                    }
                }
                else
                {
                    AcMeplusMigration.dtMappedLedgers = AcMeplusMigration.GetAllLedgersFromAcMePlus();
                    IsFirstMigration = true;
                    AcMeplusMigration.RemovePriviousMigration();
                    AcMeplusMigration.MigrateUsers();
                }
                AcMeplusMigration.MigrateByYear = (rgMigrationType.SelectedIndex == 1) ? true : false;
                AcMeplusMigration.deDateFrom = deDateFrom.DateTime;
                AcMeplusMigration.YearFrom = deDateFrom.DateTime;
                AcMeplusMigration.deDateTo = deDateTo.DateTime;

                AcMeplusMigration.ProgressBarEvent += new EventHandler(AcMeplusMigration_ProgressBarEvent);
                AcMeplusMigration.InitProgressBar += new EventHandler(SetProgressBar);
                AcMeplusMigration.IncreaseProgressBar += new EventHandler(UpdateProgressBar);
                AcMeplusMigration.MySQLExecutionCount += new EventHandler(AcMeplusMigration_MySQLExecutionCount);

                AcMeplusMigration.MigrateAcMePlusToAcMeERP();

                frmBalanceRefresh refreshBalance = new frmBalanceRefresh(AcMeplusMigration.ProjectId, (rgMigrationType.SelectedIndex == 0 ? GetFirstTransDate() :
                    deDateFrom.DateTime), true);
                refreshBalance.ShowDialog(this);

                // ResultArgs resultArgs = AcMeplusMigration.MigrateAcMePlusToAcMeERP();
                // if (resultArgs.Success)
                // {
                RefAccess.Logger("None", String.Empty, false, "", false, true);
                this.UseWaitCursor = false;

                UpdateMessage(GetMessage(MessageCatalog.DataMigration.MIGRATION_COMPLETED_SUCCESSFULLY), true);
                LayoutProgress.Visibility = LayoutVisibility.Never;
                //btnMigrateMaster.Text = "&Close";
                btnMigrateMaster.Text = this.GetMessage(MessageCatalog.Master.DataUtilityForms.CLOSE_CAPTION);
                MasterCount = AcMeplusMigration.MasterCount;
                MappingCount = AcMeplusMigration.MappingCount;
                ProjectCount = AcMeplusMigration.MappingCount;
                TransactionCount = AcMeplusMigration.TransactionCount;

                //MigrationSummary(RefAccess);
                //if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.DataMigration.MIGRATION_LOG_FILE_OPEN), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //{
                //    ProcessStartInfo startInfo = new ProcessStartInfo();
                //    startInfo.WindowStyle = ProcessWindowStyle.Maximized;
                //    startInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\APP_Migrationlog.txt";
                //    Process Process = Process.Start(startInfo);
                //}

                timer1.Stop();
                RefAccess.Logger("None", string.Empty, true, "End");
                // }
            }
            catch (Exception ee)
            {
                //XtraMessageBox.Show("Could not proceed Data Migration: " + ee.Message);
                XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.DataUtilityForms.DATA_MIGRATION_COULD_NOT_PROCEED_INFO)+" "+ ee.Message);
                timer1.Stop();
                this.Close();
                this.UseWaitCursor = false;
                //btnMigrateMaster.Text = "&Close";
                btnMigrateMaster.Text = this.GetMessage(MessageCatalog.Master.DataUtilityForms.CLOSE_CAPTION);
            }
        }

        private void SetProgressBar(object sender, EventArgs e)
        {
            progressBar.Properties.Minimum = 0;
            progressBar.Properties.Maximum = 0;
            progressBar.Properties.Step = 1;
            progressBar.PerformStep();
            progressBar.Visible = true;
            if (AcMeplusMigration.MigratingTransaction)
            {
                LayoutTrans1.Visibility = LayoutTrans2.Visibility = LayoutVisibility.Always;
                this.Size = new Size(486, 245);
                lblTotalDB.Text = String.Format("No.Trans Year : {0}", AcMeplusMigration.TotalYearCount.ToString());
                lblMigratedDB.Text = String.Format("Migrated Year: {0}", AcMeplusMigration.MigratedYearCount.ToString());
                if (AcMeplusMigration.TransactionEnds)
                    LayoutTrans1.Visibility = LayoutTrans2.Visibility = LayoutVisibility.Never;
            }
        }

        void AcMeplusMigration_ProgressBarEvent(object sender, EventArgs e)
        {
            progressBar.Properties.Maximum = AcMeplusMigration.ProgressBarMaxCount;
            if (!AcMeplusMigration.IsMigrateionCompleted)
                lblMessageInfo.Text = String.Format("Migrating {0}.....", AcMeplusMigration.MigrationStatusMessage);
            else
                lblMessageInfo.Text = String.Format("{0}...........", AcMeplusMigration.MigrationStatusMessage);
            lblMessageInfo.Update();
            Application.DoEvents();
        }

        private void AcMeplusMigration_MySQLExecutionCount(object sender, EventArgs e)
        {
            lblMySQLCount.Text = AcMeplusMigration.iMySQLExecutionCount.ToString();
            Application.DoEvents();
        }

        private void UpdateProgressBar(object sender, EventArgs e)
        {
            progressBar.PerformStep();
            Application.DoEvents();
        }

        #endregion

        #region Common Methods
        //private byte[] AddDefaultPhoto()
        //{
        //    byte[] bmpBytes = null;
        //    try
        //    {
        //        Bitmap imageIn = global::ACPP.Properties.Resources.Default_Photo;
        //        MemoryStream ms = new MemoryStream();
        //        imageIn.Save(ms, ImageFormat.Jpeg);
        //        bmpBytes = ms.GetBuffer();
        //        ms.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message, "Data Migration", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    finally { }
        //    return bmpBytes;
        //}

        private void StartTimer()
        {
            StartTime = DateTime.Now;
            TotalElapsedTime = CurrentElapsedTime;
            timer1.Start();
        }

        //private void SetVariables()
        //{
        //    Query = string.Empty;
        //    tempQuery = string.Empty;
        //    ValueField = string.Empty;
        //    ImportedRowCount = 0;
        //    ZeroRow = 0;
        //}

        //private void UpdateDateChanges(DataAccess RefAccess)
        //{
        //    string UpdateEmptyDate1 = String.Format("UPDATE MASTER_PROJECT SET ACCOUNT_DATE=\"{0}\" WHERE ACCOUNT_DATE='0001-01-01';", MySQLDateFormat(UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false)));
        //    string UpdateEmptyDate2 = String.Format("UPDATE MASTER_PROJECT SET DATE_STARTED=\"{0}\" WHERE DATE_STARTED='0001-01-01';", MySQLDateFormat(UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false)));
        //    string UpdateEmptyDate3 = "UPDATE MASTER_PROJECT SET DATE_CLOSED=NULL WHERE DATE_CLOSED='0001-01-01';";
        //    string UpdateEmptyDate4 = "UPDATE LEDGER_BALANCE SET BALANCE_DATE=NULL WHERE BALANCE_DATE='0001-01-01';";
        //    string UpdateEmptyOpenDate = String.Format("UPDATE MASTER_BANK_ACCOUNT SET DATE_OPENED=\"{0}\" WHERE DATE_OPENED='0001-01-01';", MySQLDateFormat(UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false)));
        //    string UpdateEmptyCloseDate = "UPDATE MASTER_BANK_ACCOUNT SET DATE_CLOSED=NULL WHERE DATE_CLOSED='0001-01-01';";
        //    string UpdateEmptyDate5 = "UPDATE FD_REGISTERS SET INVESTED_ON=NULL WHERE INVESTED_ON='0001-01-01';";
        //    string UpdateEmptyDate6 = "UPDATE FD_REGISTERS SET MATURITY_DATE=NULL WHERE MATURITY_DATE='0001-01-01';";
        //    string UpdateEmptyDate7 = "UPDATE ACCOUNTING_YEAR SET YEAR_FROM=null WHERE YEAR_FROM='0001-01-01';";
        //    string UpdateEmptyDate8 = "UPDATE ACCOUNTING_YEAR SET YEAR_TO=null WHERE YEAR_TO='0001-01-01';";
        //    string UpdateEmptyDate9 = "UPDATE ACCOUNTING_YEAR SET BOOKS_BEGINNING_FROM=null WHERE BOOKS_BEGINNING_FROM='0001-01-01';";
        //    RefAccess.ExecuteCommand(UpdateEmptyDate1 + UpdateEmptyDate2 + UpdateEmptyDate3 + UpdateEmptyDate4 + UpdateEmptyDate5 + UpdateEmptyDate6 + UpdateEmptyOpenDate + UpdateEmptyCloseDate + UpdateEmptyDate7 + UpdateEmptyDate8 + UpdateEmptyDate9);
        //}

        private void SetProgressBar()
        {
            progressBar.Properties.Minimum = 0;
            progressBar.Properties.Maximum = 0;
            progressBar.Properties.Step = 1;
            progressBar.PerformStep();
            progressBar.Visible = true;
        }

        private void UpdateMessage(string Message, bool IsCompleted = false)
        {
            if (!IsCompleted)
                lblMessageInfo.Text = String.Format("Migrating {0}.....", Message);
            else
                lblMessageInfo.Text = String.Format("{0}...........", Message);
            lblMessageInfo.Update();
            Application.DoEvents();
        }

        private void MigrationSummary(DataAccess RefAccess)
        {
            RefAccess.WriteMigrationSummary(MasterCount, TransactionCount, MasterCount, lblTimer.Text, chkClearSummary.Checked.Equals(true) ? false : true, MigrationStartTime, ProjectCount);
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Maximized;
            startInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MigrationSummary.txt";
            Process.Start(startInfo);
        }

        //        private void CleanUpPreviousMigration(DataAccess RefAccess)
        //        {
        //            //--------------------------------------Ledger------------------------------------------
        //            //                           1-Cash Ledger,
        //            //                           2-Fixed Deposit
        //            //                           3-Capital Fund
        //            //-------------------------------------Ledger Ends here----------------------------------

        //            //--------------------------------------Ledger Group ------------------------------------
        //            //                           1-Income          12-Bank Accounts           
        //            //                           2-Expenses        13-Cash-in-hand
        //            //                           3-Assets          14-Deposit(Asset)
        //            //                           4-Liabilities     21-Capital Fund
        //            //------------------------------------Ledger Group Ends here------------------------------

        //            UpdateMessage("Cleaning up the the Old records", true);
        //            string Query = @"DELETE FROM PROJECT_COSTCENTRE;
        //                            DELETE FROM USER_INFO WHERE USER_ID <> 1;
        //                            DELETE FROM PROJECT_DONOR;
        //                            DELETE FROM PROJECT_LEDGER;
        //                            DELETE FROM PROJECT_VOUCHER;
        //                            DELETE FROM VOUCHER_CC_TRANS;
        //                            DELETE FROM VOUCHER_TRANS;
        //                            DELETE FROM LEDGER_BALANCE;
        //                            DELETE FROM VOUCHER_MASTER_TRANS;
        //                            DELETE FROM FD_REGISTERS;
        //                            DELETE FROM ALLOT_FUND;
        //                            DELETE FROM BUDGET_LEDGER;
        //                            DELETE FROM BUDGET_MASTER;
        //                            DELETE FROM MASTER_EXECUTIVE_COMMITTEE;
        //                            DELETE FROM MASTER_DONAUD;
        //                            DELETE FROM ACCOUNTING_YEAR;
        //                            DELETE FROM FD_RENEWAL;
        //                            DELETE FROM FD_ACCOUNT;
        //                            DELETE FROM MASTER_BANK_ACCOUNT;
        //                            DELETE FROM MASTER_BANK;
        //                            DELETE FROM TDS_BOOKING;
        //                            DELETE FROM TDS_BOOKING_DETAIL;
        //                            DELETE FROM TDS_CREDTIORS_PROFILE;
        //                            DELETE FROM TDS_TAX_RATE;
        //                            DELETE FROM TDS_POLICY;
        //                            DELETE FROM TDS_DEDUCTEE_TYPE;
        //                            DELETE FROM TDS_DEDUCTION;
        //                            DELETE FROM TDS_DEDUCTION_DETAIL;
        //                            DELETE FROM TDS_DEDUCTION_DETAIL;
        //                            DELETE FROM TDS_DUTY_TAXTYPE;
        //                            DELETE FROM TDS_NATURE_PAYMENT;
        //                            DELETE FROM TDS_PARTY_PAYMENT;
        //                            DELETE FROM TDS_PAYMENT;
        //                            DELETE FROM TDS_PAYMENT_DETAIL;
        //                            DELETE FROM TDS_POLICY;
        //                            DELETE FROM TDS_SECTION;
        //                            DELETE FROM TDS_TAX_RATE;
        //                            DELETE FROM MASTER_LEDGER WHERE LEDGER_ID NOT IN (1, 2, 3); 
        //                            DELETE FROM MASTER_LEDGER_GROUP WHERE GROUP_ID NOT IN (1,2,3,4, 12, 13, 14, 21);
        //                            DELETE FROM USER_PROJECT;
        //                            DELETE FROM PROJECT_BRANCH;
        //                            DELETE FROM MASTER_PROJECT;
        //                            DELETE FROM MASTER_PROJECT_CATOGORY;
        //                            DELETE FROM MASTER_CONTRIBUTION_HEAD;
        //                            DELETE FROM MASTER_COST_CENTRE_CATEGORY;
        //                            DELETE FROM MASTER_COST_CENTRE;
        //                            DELETE FROM MASTER_INSTI_PERFERENCE;
        //                            DELETE FROM MASTER_STATE;
        //                            DELETE FROM MASTER_COUNTRY;";
        //            RefAccess.ExecuteCommand(Query);
        //        }

        private bool IsMigrated(DataAccess RefAccess)
        {
            bool Yes = false;
            int RecordCount = UtilityMember.NumberSet.ToInteger(RefAccess.ExecuteScalarValue("SELECT COUNT(*) AS COUNT FROM VOUCHER_MASTER_TRANS").ToString());
            object master = RefAccess.ExecuteScalarValue("SELECT count(*) as Count FROM master_project");
            //  object ledger = RefAccess.ExecuteScalarValue("SELECT count(*) as Count FROM master_ledger");
            int MasterCount = UtilityMember.NumberSet.ToInteger(master.ToString());
            // int ledgerCount = UtilityMember.NumberSet.ToInteger(ledger.ToString());
            if ((RecordCount > 0 && MasterCount > 0) || (RecordCount > 0 || MasterCount > 0))
                Yes = true;
            return Yes;
        }

        private string MySQLDateFormat(DateTime deDate)
        {
            return deDate.ToString("yyyy-MM-dd");
        }

        private void DisableByPeriodControl()
        {
            LayoutDateFrom.Visibility = LayoutDateTo.Visibility = LayoutTransYear.Visibility = LayoutVisibility.Never;
            this.Size = new Size(480, 175);
        }

        private void EnableByPeriodControl()
        {
            if (ValidateConnectionProperty())
            {
                this.Size = new Size(480, 185);
                LayoutDateFrom.Visibility = LayoutDateTo.Visibility = LayoutTransYear.Visibility = LayoutVisibility.Always;
                LoadAvailableAccountingYear();
            }
            else rgMigrationType.SelectedIndex = 0;
        }

        private void LoadAvailableAccountingYear()
        {
            using (DataAccess dataAccess = new DataAccess())
            {
                //Reading Connection Format for Ole DB(MS Access DB)
                string OleDBCon = ConfigurationManager.AppSettings.Get("OleDBConFormat");
                if (!string.IsNullOrEmpty(OleDBCon))
                {
                    OleDBCon = OleDBCon.Replace("@AccessDB@", txtAcMePlusDB.Text);
                    //Constructing Ole DB Connection String
                    ConfigurationManager.AppSettings.Set("OleDBConStr", OleDBCon);
                    OleDbConn = dataAccess.OpenOleDbConnection();
                    dtYear = dataAccess.ExecuteOleDbQuery("SELECT YEAR(DATEFROM) AS ACYEAR,DATEFROM,ACORDER FROM ACYEAR", OleDbConn);
                    if (dtYear != null)
                    {
                        UtilityMember.ComboSet.BindGridLookUpCombo(glkpAcYears, dtYear, "ACYEAR", "ACORDER");
                        glkpAcYears.EditValue = glkpAcYears.Properties.GetKeyValue(0);
                        SetMinDateAndMaxDate();
                    }
                }
                else
                {
                    //XtraMessageBox.Show("OleDbConnection tag is missing in App.Configuration file", this.GetMessage(MessageCatalog.DataMigration.MIGRATION_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.DataUtilityForms.OLEDB_CONN_TAG_MISSING_IN_APPCONFIG_FILE), this.GetMessage(MessageCatalog.DataMigration.MIGRATION_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void SetMinDateAndMaxDate(bool IsComboBoxIndexChnagedEvent = false)
        {
            using (DataAccess dataAccess = new DataAccess())
            {
                if (dtYear != null)
                {
                    string MinYear;
                    string MaxYear;
                    if (IsComboBoxIndexChnagedEvent) MinYear = glkpAcYears.Text;
                    else MinYear = dtYear.Compute("MIN(ACYEAR)", string.Empty).ToString();

                    MaxYear = dtYear.Compute("MAX(ACYEAR)", string.Empty).ToString();
                    object MinDate = dataAccess.ExecuteOleDbScalarValue(String.Format("SELECT MIN(CBDATE) as CBDATE FROM CBTRANS{0}", MinYear), OleDbConn);
                    object MaxDate = dataAccess.ExecuteOleDbScalarValue(String.Format("SELECT MAX(CBDATE) as CBDATE FROM CBTRANS{0}", MaxYear), OleDbConn);
                    deDateFrom.Properties.MinValue = deDateFrom.DateTime = UtilityMember.DateSet.ToDate(MinDate.ToString(), false);
                    deDateFrom.Properties.MaxValue = deDateTo.DateTime = UtilityMember.DateSet.ToDate(MaxDate.ToString(), false);
                    deDateTo.Properties.MinValue = deDateFrom.DateTime;
                    deDateTo.Properties.MaxValue = deDateTo.DateTime;
                }
            }
        }

        private DateTime GetFirstTransDate()
        {
            DateTime dtFirstDate = new DateTime();
            using (DataAccess dataAccess = new DataAccess())
            {
                string OleDBCon = ConfigurationManager.AppSettings.Get("OleDBConFormat");
                if (!string.IsNullOrEmpty(OleDBCon))
                {
                    OleDBCon = OleDBCon.Replace("@AccessDB@", txtAcMePlusDB.Text);
                    ConfigurationManager.AppSettings.Set("OleDBConStr", OleDBCon);
                    OleDbConn = dataAccess.OpenOleDbConnection();
                    //object objYear = dataAccess.ExecuteOleDbScalarValue("SELECT YEAR(DATEFROM) AS ACYEAR,DATEFROM,ACORDER FROM ACYEAR", OleDbConn);
                    object objYear = dataAccess.ExecuteOleDbScalarValue("SELECT DATEFROM AS ACYEAR,DATEFROM,ACORDER FROM ACYEAR ORDER BY DATEFROM", OleDbConn);
                    if (objYear != null)
                    {
                        //dtFirstDate = new DateTime(UtilityMember.NumberSet.ToInteger(objYear.ToString()), 04, 01);
                        dtFirstDate = new DateTime(UtilityMember.DateSet.ToDate(objYear.ToString(), false).Year, UtilityMember.DateSet.ToDate(objYear.ToString(), false).Month, 01);
                    }
                }
                else
                {
                    //XtraMessageBox.Show("OleDbConnection tag is missing in App.Configuration file", this.GetMessage(MessageCatalog.DataMigration.MIGRATION_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.DataUtilityForms.OLEDB_CONN_TAG_MISSING_IN_APPCONFIG_FILE), this.GetMessage(MessageCatalog.DataMigration.MIGRATION_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            return dtFirstDate;
        }

        #endregion

        #endregion

        #region Mutliple DB Migration
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
