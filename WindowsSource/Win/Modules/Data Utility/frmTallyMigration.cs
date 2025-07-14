/*************************************************************************************************************************
 *                                              Purpose     :Migrate records from Tally To Acme.erp
 *                                              Author      :Carmel Raj M
 *                                              Created On  :17-November-2014
 *                                              Modified On :03-Jun-2014
 *                                              Modified By :Carmel Raj M
 *                                              Reviewed By :Carmel Raj M
 *************************************************************************************************************************/

using System;
using System.Data;
using System.Windows.Forms;
using Bosco.Utility;
using Bosco.DAO;
using Bosco.Utility.ConfigSetting;
using System.Diagnostics;
using DevExpress.XtraLayout.Utils;
using Bosco.Model.TallyMigration;
using Bosco.Model.UIModel;

namespace ACPP.Modules.Data_Utility
{
    public partial class frmTallyMigration : frmFinanceBaseAdd
    {
        #region Variables
        //private TallyConnector tallyConnector = null;
        const int INCOME = 1;
        const int EXPENSES = 2;
        const int ASSETS = 3;
        const int LIABILITIES = 4;

        TallyMigrationSystem TallyMigration;
        DateTime StartTime;
        TimeSpan CurrentElapsedTime = TimeSpan.Zero;
        TimeSpan TotalElapsedTime = TimeSpan.Zero;
        //ResultArgs resultArg = null;
        int MasterCount = 0;
        int MappingCount = 0;
        int TransactionCount = 0; //Migrated Transaction Count
        bool IsConnectionEstablished = false;
        bool IsMigrated = false;
        #endregion

        #region Properties
        int ProjectId { get; set; }

        private string CurrentCompanyName { get; set; }
        private string Narration { get; set; }

        private DateTime StartingDate { get; set; }
        private DateTime BooksBeginningDate { get; set; }

        bool isDonorModuleAvailable = false;
        private bool IsDonorModuleEnabled
        {
            set { isDonorModuleAvailable = value; }
            get { return isDonorModuleAvailable; }
        }

        private DataTable dtCompany { get; set; }
        private DataTable dtGroup { get; set; }
        private DataTable dtLedger { get; set; }
        private DataTable dtVoucherType { get; set; }
        private DataTable dtCostCategory { get; set; }
        private DataTable dtCostCentre { get; set; }
        private DataTable dtCountry { get; set; }
        private DataTable dtState { get; set; }
        private DataTable dtDonor { get; set; }
        private DataTable dtPurpose { get; set; }
        private DataSet dsVoucher { get; set; }

        #endregion

        #region Constructor
        public frmTallyMigration()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void frmTallyMigration_Load(object sender, EventArgs e)
        {
            using (TallyConnector TallyERPConnector = new TallyConnector())
            {
                ResultArgs resultarug = TallyERPConnector.IsTallyConnected;
                if (!resultarug.Success)
                {
                    lgMigrationSummary.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    LoadDefaults();
                }
                else
                {
                    LoadCompany();
                    if (IsConnectionEstablished)
                    {
                        //this.ShowWaitDialog("Connecting Tally");
                        this.ShowWaitDialog("Connecting and Fetching Tally");
                        ClearSummary();
                        lcgCompanyInfo.Visibility = LayoutVisibility.Always;
                        lgMigrationSummary.Visibility = LayoutVisibility.Always;
                        lcgCompanyInfo.Expanded = true;

                        //For Temp on 02/09/2017, to fix to mirgate from 01/04/2017 on wards, always
                        deDataFrom.DateTime = BooksBeginningDate;
                        //deDataFrom.DateTime = UtilityMember.DateSet.ToDate("01/04/2017", false);
                        //deDataFrom.Properties.MinValue = UtilityMember.DateSet.ToDate("01/04/2017", false); //BooksBeginningDate
                        //deDataFrom.Enabled = false;

                        deDateTo.DateTime = DateTime.Now;
                        lblYearFrom.Text = StartingDate.ToShortDateString();
                        lblBookBeginningFromYear.Text = BooksBeginningDate.ToShortDateString();
                        lblCompanyName.Text = CurrentCompanyName.ToString();
                        lblConnectTally.Visibility = LayoutVisibility.Never;
                        resultarug = AnalyseTallyData();
                        this.CloseWaitDialog();
                        if (!resultarug.Success)
                        {
                            this.ShowMessageBoxError(resultarug.Message);
                            this.Close();
                        }
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            LoadDefaults();
            ConnectTally();
            AnalyseTallyData();
            lblProgressBar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lblProgressBarCaption.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        private void btnMigrate_Click(object sender, EventArgs e)
        {
            TallyMigration = new TallyMigrationSystem();
            if (Validate())
            {
                //Checking if the default voucherExists
                if (TallyMigration.IsDefaultVoucherExists())
                {
                    //-------------------------------------Comparing Dates Without Time----------------------
                    DateTime deFromDate = DateTime.ParseExact(deDataFrom.Text, "dd/MM/yyyy", null);

                    //For Temp on 02/09/2017, to fix Date from and books begning for Acme.erp **********************************************************
                    //For Temp on 02/09/2017, to fix to mirgate from 01/04/2017 on wards, always
                    DateTime deBookBegining = DateTime.ParseExact(BooksBeginningDate.ToShortDateString(), "dd/MM/yyyy", null); //01/04/2017
                    //DateTime deBookBegining = DateTime.ParseExact("01/04/2017", "dd/MM/yyyy", null); 
                    BooksBeginningDate = deBookBegining;
                    StartingDate = deBookBegining;

                    DateTime deToDate = DateTime.ParseExact(deDateTo.Text, "dd/MM/yyyy", null);
                    DateTime deNow = DateTime.ParseExact(DateTime.Now.Date.ToShortDateString(), "dd/MM/yyyy", null);
                    //if (deFromDate.CompareTo(deBookBegining) == 0 && deToDate.CompareTo(deNow) == 0)
                    if (deFromDate.CompareTo(deBookBegining) >= 0)
                    {
                        this.Size = new System.Drawing.Size(565, 450);
                        this.CenterToParent();
                        lblProgressBar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        lblProgressBarCaption.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        this.Cursor = Cursors.WaitCursor;
                        ResultArgs resultarg = FetchLedgers();
                        if (resultarg.Success)
                        {
                            resultarg = FetchVouchers();
                            if (resultarg.Success)
                            {
                                deDataFrom.Enabled = false;
                                deDateTo.Enabled = false;
                                btnMigrate.Enabled = false;
                                Migrate();
                                timer1.Stop();
                                this.Cursor = Cursors.Default;
                            }
                        }
                        if (!resultarg.Success)
                        {
                            this.Cursor = Cursors.Default;
                            ShowMessageBox(resultarg.Message);
                        }
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        ShowMessageBox("Invalid given date range");
                    }
                    lblProgressBarCaption.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
                else
                {
                    //ShowMessageBox("Could not start Tally migration, Default Master vouchers are missing.");
                    ShowMessageBox(this.GetMessage(MessageCatalog.Master.DataUtilityForms.COULD_NOT_START_TALLY_MIGRATION));
                    this.Close();
                }
            }
        }

        private void frmTallyMigration_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (IsMigrated)
            {
                SettingProperty.Is_Application_Logout = true;
                Application.Restart();
            }
        }

        private void frmTallyMigration_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var timeSinceStartTime = DateTime.Now - StartTime;
            timeSinceStartTime = new TimeSpan(timeSinceStartTime.Hours, timeSinceStartTime.Minutes, timeSinceStartTime.Seconds);
            CurrentElapsedTime = timeSinceStartTime + TotalElapsedTime;
            lblTimer.Text = CurrentElapsedTime.ToString();
        }
        #endregion

        #region Methods

        #region Analysing Defaults Values Before Migration

        private ResultArgs AnalyseTallyData()
        {
            ResultArgs resultarg = new ResultArgs();
            this.UseWaitCursor = true;
            resultarg = LoadTallyData(TallyConnector.TallyMasters.Country); //1. Fetch Countries
            UpdateMessage(this.GetMessage(MessageCatalog.Master.DataUtilityForms.FETCHING_COUNTRY_FROM_TALLY),true);
            if (resultarg.Success)
            {
                dtCountry = resultarg.DataSource.Table;
                UpdateMessage(this.GetMessage(MessageCatalog.Master.DataUtilityForms.FETCHING_STATE_FROM_TALLY), true);
                resultarg = LoadTallyData(TallyConnector.TallyMasters.States); //2. Fetch States
                if (resultarg.Success)
                {
                    dtState = resultarg.DataSource.Table;
                    UpdateMessage(this.GetMessage(MessageCatalog.Master.DataUtilityForms.FETCHING_PURPOSE_LIST_FROM_TALLY), true);
                    resultarg = LoadTallyData(TallyConnector.TallyMasters.Purposes); //3. Fetch Purposes
                    if (resultarg.Success)
                    {
                        dtPurpose = resultarg.DataSource.Table;
                        UpdateMessage(this.GetMessage(MessageCatalog.Master.DataUtilityForms.FETCHING_VOUCHER_TYPE_FROM_TALLY), true);
                        resultarg = LoadTallyData(TallyConnector.TallyMasters.VoucherType); //4. Fetch Voucher Types
                        if (resultarg.Success)
                        {
                            dtVoucherType = resultarg.DataSource.Table;
                            UpdateMessage(this.GetMessage(MessageCatalog.Master.DataUtilityForms.FETCHING_GROUP_DETAILS_FROM_TALLY), true);
                            resultarg = LoadTallyData(TallyConnector.TallyMasters.Groups); //5. Fetch Groups
                            if (resultarg.Success)
                            {
                                dtGroup = resultarg.DataSource.Table;
                                UpdateMessage(this.GetMessage(MessageCatalog.Master.DataUtilityForms.FETCHING_COST_CATEGORY_FROM_TALLY), true);
                                resultarg = LoadTallyData(TallyConnector.TallyMasters.CostCategory); //6. Fetch CC category
                                if (resultarg.Success)
                                {
                                    dtCostCategory = resultarg.DataSource.Table;
                                    UpdateMessage(this.GetMessage(MessageCatalog.Master.DataUtilityForms.FETCHING_COST_CENTER_FROM_TALLY), true);
                                    resultarg = LoadTallyData(TallyConnector.TallyMasters.CostCenters); //7. Fetch CC
                                    if (resultarg.Success)
                                    {
                                        dtCostCentre = resultarg.DataSource.Table;
                                        UpdateMessage(this.GetMessage(MessageCatalog.Master.DataUtilityForms.FETCHING_DONOR_DETAILS_FROM_TALLY), true);
                                        resultarg = LoadTallyData(TallyConnector.TallyMasters.Donors); //8. Fetch Donors
                                        if (resultarg.Success)
                                        {
                                            dtDonor = resultarg.DataSource.Table;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //Update Record counts for progress bar
            int GroupCount = dtGroup != null ? dtGroup.Rows.Count > 0 ? dtGroup.Rows.Count : 0 : 0;
            UpdateGroupTotalCount(GroupCount);
            int CostCategoryCount = dtCostCategory != null ? dtCostCategory.Rows.Count > 0 ? dtCostCategory.Rows.Count : 0 : 0;
            UpdateCCCategoryTotalCount(CostCategoryCount);
            int CCCount = dtCostCentre != null ? dtCostCentre.Rows.Count > 0 ? dtCostCentre.Rows.Count : 0 : 0;
            UpdateCCTotalCount(CCCount);
            int DonorCount = dtDonor != null ? dtDonor.Rows.Count > 0 ? dtDonor.Rows.Count : 0 : 0;
            UpdateDonorTotalCount(DonorCount);
            
            this.Size = new System.Drawing.Size(565, 411);
            lgMigrationSummary.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            btnMigrate.Enabled = true;
            this.AcceptButton = btnMigrate;
            btnMigrate.Focus();
            this.CenterToParent();
            this.UseWaitCursor = false;
            return resultarg;
        }

        private ResultArgs FetchLedgers()
        {
            ResultArgs resularg = new ResultArgs();
            //UpdateMessage("Fetching Ledger Details from Tally", true);
            UpdateMessage(this.GetMessage(MessageCatalog.Master.DataUtilityForms.FETCHING_LEDGER_DETAILS_FROM_TALLY), true);
            //dtLedger = LoadTallyData(TallyConnector.TallyMasters.Ledgers, MySQLDateFormat(deDataFrom.DateTime.AddDays(-1)));
            
            //On 02/09/2017, Insed of getting Closing Balance, Get Opening balance (Its because of Tally GST Version)
            //dtLedger = LoadTallyData(TallyConnector.TallyMasters.Ledgers, deDataFrom.DateTime.AddDays(-1).ToString("dd/MM/yyyy"));
            resularg = LoadTallyData(TallyConnector.TallyMasters.Ledgers, deDataFrom.DateTime.ToString("dd/MM/yyyy"));
            if (resularg.Success)
            {
                dtLedger = resularg.DataSource.Table;
            }
            int LedgerCount = dtLedger != null ? dtLedger.Rows.Count > 0 ? dtLedger.Rows.Count : 0 : 0;
            UpdateLedgerTotalCount(LedgerCount);
            return resularg;
        }

        private ResultArgs FetchVouchers()
        {
            ResultArgs resularg = new ResultArgs();
            using (TallyConnector TallyERPConnector = new TallyConnector())
            {
                //UpdateMessage("Fetching Voucher Details from Tally", true);
                UpdateMessage(this.GetMessage(MessageCatalog.Master.DataUtilityForms.FETCHING_VOUCHER_DETAILS_FROM_TALLY), true);
                string fromdate = deDataFrom.DateTime.ToString("dd/MM/yyyy");
                string todate = deDateTo.DateTime.ToString("dd/MM/yyyy");
                TallyERPConnector.dtTallyLedgers = dtLedger;
                TallyERPConnector.dtTallyVoucherTypes= dtVoucherType;
                resularg = TallyERPConnector.FetchVouchers(fromdate, todate);
                dsVoucher = null;
                if (resularg != null && resularg.Success)
                {
                    if (resularg.DataSource.Data != null)
                    {
                        dsVoucher = (DataSet)resularg.DataSource.Data;
                        if (dsVoucher.Tables.Contains("MASTER VOUCHER"))
                        {
                            DataTable dtVoucherMaster = dsVoucher.Tables["MASTER VOUCHER"];
                            int TransCount = dtVoucherMaster != null ? dtVoucherMaster.Rows.Count > 0 ? dtVoucherMaster.Rows.Count : 0 : 0; ;
                            UpdateTransTotalCount(TransCount);
                        }
                    }
                }
            }
            return resularg;
        }

        private void LoadDefaults()
        {
            ClearSummary();
            this.Size = new System.Drawing.Size(565, 436);
            layoutControlItemTimer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcgCompanyInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcgCompanyInfo.Expanded = false;
        }

        //private bool IsTallyMigrationMade(DataAccess RefAccessTally)
        //{
        //    bool Status = false;
        //    object Master = RefAccessTally.ExecuteScalarValue(String.Format("SELECT COUNT(*) AS COUNT FROM MASTER_PROJECT WHERE PROJECT=\"{0}\"", TallyCurrentCompanyName));
        //    int ProjectCount = UtilityMember.NumberSet.ToInteger(Master.ToString());
        //    if (ProjectCount > 0)
        //        Status = true;
        //    return Status;
        //}

        private void ClearSummary()
        {
            lblGroupTotalCount.Text = string.Empty;
            lblGroupMigratedCount.Text = string.Empty;
            lblGroupPendingCount.Text = string.Empty;

            lblLedgerTotalCount.Text = string.Empty;
            lblLedgerMigratedCount.Text = string.Empty;
            lblLedgerPendingCount.Text = string.Empty;

            lblCCCategoryTotalCount.Text = string.Empty;
            lblCCCategoryMigratedCount.Text = string.Empty;
            lblCCCategoryPendingCount.Text = string.Empty;

            lblCCTotalCount.Text = string.Empty;
            lblCCMigratedCount.Text = string.Empty;
            lblCCPendingCount.Text = string.Empty;

            lblDonorTotalCount.Text = string.Empty;
            lblDonorMigratedCount.Text = string.Empty;
            lblDonorPendingCount.Text = string.Empty;

            lblTransTotalCount.Text = string.Empty;
            lblTransMigratedCount.Text = string.Empty;
            lblTransPendingCount.Text = string.Empty;
        }

        #endregion

        #region UI Updation
        #region Group Summary
        private void UpdateGroupTotalCount(int Count)
        {
            lblGroupTotalCount.Text = Count.ToString();
            Application.DoEvents();
        }

        private void TallyMigration_UpdateGroupProcessedCount(object sender, EventArgs e)
        {
            lblGroupMigratedCount.Text = TallyMigration.GroupProcessedCount.ToString();
            Application.DoEvents();
        }

        private void TallyMigration_UpdateGroupPendingCountEve(object sender, EventArgs e)
        {
            lblGroupPendingCount.Text = TallyMigration.GroupPendingCount.ToString();
            Application.DoEvents();
        }


        private void UpdateGroupProcessedCount(int Count)
        {
            lblGroupMigratedCount.Text = Count.ToString();
            Application.DoEvents();
        }


        private void UpdateGroupPendingCount(int Count)
        {
            lblGroupPendingCount.Text = Count.ToString();
            Application.DoEvents();
        }
        #endregion

        #region Ledger Summary
        private void UpdateLedgerTotalCount(int Count)
        {
            lblLedgerTotalCount.Text = Count.ToString();
            Application.DoEvents();
        }

        private void TallyMigration_UpdateLedgerProcessedCount(object sender, EventArgs e)
        {
            lblLedgerMigratedCount.Text = TallyMigration.LedgerProcessedCount.ToString();
            Application.DoEvents();
        }


        private void TallyMigration_UpdateLedgerPendingCountEve(object sender, EventArgs e)
        {
            lblLedgerPendingCount.Text = TallyMigration.LedgerPendingCount.ToString();
            Application.DoEvents();
        }

        private void UpdateLedgerProcessedCount(int Count)
        {
            lblLedgerMigratedCount.Text = Count.ToString();
            Application.DoEvents();
        }

        private void UpdateLedgerPendingCount(int Count)
        {
            lblLedgerPendingCount.Text = Count.ToString();
            Application.DoEvents();
        }
        #endregion

        #region Cost Centre Category Summary
        private void UpdateCCCategoryTotalCount(int Count)
        {
            lblCCCategoryTotalCount.Text = Count.ToString();
            Application.DoEvents();
        }

        private void TallyMigration_UpdateCostCentreCategoryProcessedCount(object sender, EventArgs e)
        {
            lblCCCategoryMigratedCount.Text = TallyMigration.CCCategoryProcessedCount.ToString();
            Application.DoEvents();
        }

        private void TallyMigration_UpdateCostCentreCategpruPendingCount(object sender, EventArgs e)
        {
            lblCCCategoryPendingCount.Text = TallyMigration.CCCategoryPendingCount.ToString();
            Application.DoEvents();
        }

        private void UpdateCCCategoryProcessedCount(int Count)
        {
            lblCCCategoryMigratedCount.Text = Count.ToString();
            Application.DoEvents();
        }

        private void UpdateCCCategoryPendingCount(int Count)
        {
            lblCCCategoryPendingCount.Text = Count.ToString();
            Application.DoEvents();
        }
        #endregion

        #region Cost Centre Summary
        private void UpdateCCTotalCount(int Count)
        {
            lblCCTotalCount.Text = Count.ToString();
            Application.DoEvents();
        }

        private void TallyMigration_UpdateCostCentreProcessedCount(object sender, EventArgs e)
        {
            lblCCMigratedCount.Text = TallyMigration.CostCentrerocessedCount.ToString();
            Application.DoEvents();
        }

        private void TallyMigration_UpdateCostCentrePendingCount(object sender, EventArgs e)
        {
            lblCCPendingCount.Text = TallyMigration.CostCentrePendingCount.ToString();
            Application.DoEvents();
        }
        private void UpdateCCProcessedCount(int Count)
        {
            lblCCMigratedCount.Text = Count.ToString();
            Application.DoEvents();
        }

        private void UpdateCCPendingCount(int Count)
        {
            lblCCPendingCount.Text = Count.ToString();
            Application.DoEvents();
        }
        #endregion

        #region Donor Summary
        private void UpdateDonorTotalCount(int Count)
        {
            lblDonorTotalCount.Text = Count.ToString();
            Application.DoEvents();
        }

        private void TallyMigration_UpdateDonorProcessedCount(object sender, EventArgs e)
        {
            lblDonorMigratedCount.Text = TallyMigration.DonorProcessedCount.ToString();
            Application.DoEvents();
        }

        private void TallyMigration_UpdateDonorPendingCount(object sender, EventArgs e)
        {
            lblDonorPendingCount.Text = TallyMigration.DonorPendingCount.ToString();
            Application.DoEvents();
        }

        private void UpdateDonorProcessedCount(int Count)
        {
            lblDonorMigratedCount.Text = Count.ToString();
            Application.DoEvents();
        }

        private void UpdateDonorPendingCount(int Count)
        {
            lblDonorPendingCount.Text = Count.ToString();
            Application.DoEvents();
        }
        #endregion

        #region Transaction Summary
        private void UpdateTransTotalCount(int Count)
        {
            lblTransTotalCount.Text = Count.ToString();
            Application.DoEvents();
        }

        private void TallyMigration_UpdateVoucherTransProcessedCount(object sender, EventArgs e)
        {
            lblTransMigratedCount.Text = TallyMigration.VoucherTransProcessedCount.ToString();
            Application.DoEvents();
        }

        private void TallyMigration_UpdatevoucherTransPendingCount(object sender, EventArgs e)
        {
            lblTransPendingCount.Text = TallyMigration.VoucherTransPendingCount.ToString();
            Application.DoEvents();
        }

        private void UpdateTransProcessedCount(int Count)
        {
            lblTransMigratedCount.Text = Count.ToString();
            Application.DoEvents();
        }

        private void UpdateTransPendingCount(int Count)
        {
            lblTransPendingCount.Text = Count.ToString();
            Application.DoEvents();
        }
        #endregion
        #endregion

        private void ConnectTally()
        {
            LoadCompany();
            if (IsConnectionEstablished)
            {
                lcgCompanyInfo.Visibility = LayoutVisibility.Always;
                lcgCompanyInfo.Expanded = true;
                lblYearFrom.Text = StartingDate.ToShortDateString();
                lblBookBeginningFromYear.Text = BooksBeginningDate.ToShortDateString();
                lblCompanyName.Text = CurrentCompanyName.ToString();
                lblConnectTally.Visibility = LayoutVisibility.Never;
                deDataFrom.DateTime = BooksBeginningDate;
            }
        }

        private void Migrate()
        {
            if (IsConnectionEstablished)
            {
                using (DataAccess RefAccessTally = new DataAccess())
                {
                    RefAccessTally.IsTallyMigration = true;
                    layoutControlItemTimer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    RefAccessTally.Logger("None", string.Empty, true, "Stated On " + DateTime.Now.ToShortDateString(), false);
                    try
                    {
                        TallyMigration = new TallyMigrationSystem();

                        //Done by alwar on 02/12/2015 ---------------------------------------------------
                        //If map prject slected for migration, pass mapped project for migration else pass tally compnay name
                        //TallyMigration.CurrentCompanyName = TallyCurrentCompanyName;
                        string migrationproject = CurrentCompanyName;
                        if (glkpProject.EditValue!=null && this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) > 0)
                        {
                            migrationproject = glkpProject.Text;
                        }
                        TallyMigration.CurrentCompanyName = migrationproject;
                        
                        //12/02/2021, option to migrate multiple voucher type
                        TallyMigration.IncludeMultipleVoucherTypes = (chkMultipleVTypes.Checked);
                        //-------------------------------------------------------------------------------
                        

                        if (TallyMigration.IsTallyMigrationMade())
                        {
                           //string strMessage = String.Format("{4} is available.{5}Do you want to delete the Project and continue migration?{3}{0}Yes      : Delete and Continue{1}No       : Merge with old records{2}Cancel: Stop Migration.",
                           //string strMessage = String.Format(this.GetMessage(MessageCatalog.Master.DataUtilityForms.TALLY_MIGRATION_MODE_OPTION),
                           string strMessage = String.Format("{4} is available.{5}Do you want to delete the Project and continue migration?{3}{0}Yes      : Delete and Continue for the given period{1}No       : Merge with old records{2}Cancel: Stop Migration.",
                                Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, migrationproject, Environment.NewLine);
                            DialogResult result = this.ShowConfirmationMessage(strMessage, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                TallyMigration.RemovePriviousMigrationByDateRange(deDataFrom.DateTime, deDateTo.DateTime);
                                //frmMapBeforeMigration MapMigration = new frmMapBeforeMigration(dtLedger, MigrationType.Tally);
                                //MapMigration.ShowDialog();
                                //TallyMigration.dtLedger = MapMigration.dtLedgerToBeMigrated;
                            }
                            else if (result == DialogResult.Cancel)
                            {
                                SettingProperty.Is_Application_Logout = true;
                                btnMigrate.Enabled = true;
                                this.UseWaitCursor = false;
                                return;
                            }
                            else
                            {
                                //frmMapBeforeMigration MapMigration = new frmMapBeforeMigration(dtLedger, MigrationType.Tally);
                                //MapMigration.ShowDialog();
                                //TallyMigration.dtLedger = MapMigration.dtLedgerToBeMigrated;
                            }
                        }
                        else
                        {
                            //using (AcMePlusMigrationSystem ledgerCount = new AcMePlusMigrationSystem())
                            //{
                            //    if (ledgerCount.GenerateLedgerCode() > 0)
                            //    {
                            //        frmMapBeforeMigration MapMigration = new frmMapBeforeMigration(dtLedger, MigrationType.Tally);
                            //        MapMigration.ShowDialog();
                            //        TallyMigration.dtLedger = MapMigration.dtLedgerToBeMigrated;
                            //    }
                            //}
                        }

                        //this.Size = new System.Drawing.Size(530, 400);
                        //this.CenterToParent();
                        lgMigrationSummary.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        layoutControlItemTimer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        StartTimer();
                        lblProgressBar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        lblProgressBarCaption.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        this.UseWaitCursor = true;

                        AttachEventforUIUpdation();

                        TallyMigration.StartingDate = StartingDate;
                        TallyMigration.BooksBeginningDate = BooksBeginningDate; 
                        TallyMigration.MigrationDateFrom = UtilityMember.DateSet.ToDate(deDataFrom.DateTime.ToShortDateString(), false);
                        TallyMigration.MigrationDateTo = UtilityMember.DateSet.ToDate(deDateTo.DateTime.ToShortDateString(), false);

                        TallyMigration.dtTallyCompany = dtCompany;
                        TallyMigration.dtTallyGroup = dtGroup;
                        TallyMigration.dtTallyLedger = dtLedger;
                        TallyMigration.dtTallyVoucherType = dtVoucherType;
                        TallyMigration.dtTallyCostCategory = dtCostCategory;
                        TallyMigration.dtTallyCostCentre = dtCostCentre;
                        TallyMigration.dtTallyCountry = dtCountry;
                        TallyMigration.dtTallyState = dtState;
                        TallyMigration.dtTallyDonor = dtDonor;
                        TallyMigration.dtTallyPurpose = dtPurpose;
                        TallyMigration.ProjectId = ProjectId;
                        TallyMigration.dsTallyVoucher = dsVoucher;
                        
                        //Added by Salamon...
                        TallyMigration.IsDonorModuleEnabled = IsDonorModuleEnabled;
                        
                        // On 08/12/2017, whether to updte opening balance or not 
                        TallyMigration.IsUpdateLedgerOpeningBalance = chkIncludeOpeningBalance.Checked;

                        //TallyMigration.DeleteUnusedLedger = chkDeleteUnusedLedgers.Checked;
                        TallyMigration.ProcessTallyMigration();

                        IsMigrated = true;
                        frmBalanceRefresh refreshBalance = new frmBalanceRefresh(TallyMigration.ProjectId, deDataFrom.DateTime, true);
                        refreshBalance.ShowDialog(this);
                        //if (chkDeleteUnusedLedgers.Checked)
                        //    TallyMigration.DeleteUnusedLedgersFromAcMeERP();

                        RefAccessTally.Logger("None", String.Empty, false, "", false, true);
                        RefAccessTally.Logger("None", string.Empty, true, "End");
                        timer1.Stop();
                        this.UseWaitCursor = false;
                        lblProgressBar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        this.CenterToParent();
                        UpdateMessage(GetMessage(MessageCatalog.DataMigration.MIGRATION_COMPLETED_SUCCESSFULLY), true);
                        ShowMessageBox(GetMessage(MessageCatalog.DataMigration.MIGRATION_COMPLETED_SUCCESSFULLY));
                        IsConnectionEstablished = false;
                        lblConnectTally.Visibility = LayoutVisibility.Always;
                        deDataFrom.Enabled = true;
                        deDateTo.Enabled = true;
                        btnMigrate.Enabled = false;

                    }
                    catch (Exception ex)
                    {
                        //ShowMessageBox(String.Format("Could not process Data Migration.Due to the following reason {0} ", ex.Message));
                        ShowMessageBox(String.Format(this.GetMessage(MessageCatalog.Master.DataUtilityForms.COULD_NOT_PROCESS_DATA_MIGRATION), ex.Message));
                        timer1.Stop();
                        this.UseWaitCursor = false;
                        this.Close();
                    }
                }
            }
            //else ShowMessageBox("Could not establish connection with Tally");
            else ShowMessageBox(this.GetMessage(MessageCatalog.Master.DataUtilityForms.COULD_NOT_ESTABILISH_CONNECTION_WITH_TALLY));
        }

        private void AttachEventforUIUpdation()
        {
            TallyMigration.UpdateMessage += new EventHandler(TallyMigration_UpdateMessage);
            TallyMigration.InitProgressBar += new EventHandler(TallyMigration_InitProgressBar);
            TallyMigration.IncreaseProgressBar += new EventHandler(TallyMigration_IncreaseProgressBar);
            TallyMigration.UpdateGroupProcessedCount += new EventHandler(TallyMigration_UpdateGroupProcessedCount);
            TallyMigration.UpdateGroupPendingCountEve += new EventHandler(TallyMigration_UpdateGroupPendingCountEve);
            TallyMigration.UpdateLedgerProcessedCount += new EventHandler(TallyMigration_UpdateLedgerProcessedCount);
            TallyMigration.UpdateLedgerPendingCountEve += new EventHandler(TallyMigration_UpdateLedgerPendingCountEve);
            TallyMigration.UpdateCostCentreCategoryProcessedCount += new EventHandler(TallyMigration_UpdateCostCentreCategoryProcessedCount);
            TallyMigration.UpdateCostCentreCategoryPendingCount += new EventHandler(TallyMigration_UpdateCostCentreCategpruPendingCount);
            TallyMigration.UpdateCostCentreProcessedCount += new EventHandler(TallyMigration_UpdateCostCentreProcessedCount);
            TallyMigration.UpdateCostCentrePendingCount += new EventHandler(TallyMigration_UpdateCostCentrePendingCount);
            TallyMigration.UpdateDonorProcessedCount += new EventHandler(TallyMigration_UpdateDonorProcessedCount);
            TallyMigration.UpdateDonorPendingCount += new EventHandler(TallyMigration_UpdateDonorPendingCount);
            TallyMigration.UpdateVoucherTransProcessedCount += new EventHandler(TallyMigration_UpdateVoucherTransProcessedCount);
            TallyMigration.UpdatevoucherTransPendingCount += new EventHandler(TallyMigration_UpdatevoucherTransPendingCount);

        }

        //public TallyConnector TallyERPConnector
        //{
        //    get { if (tallyConnector == null) tallyConnector = new TallyConnector(); return tallyConnector; }
        //}
        
        private ResultArgs LoadTallyData(TallyConnector.TallyMasters enTallyTbl, string DateFrom="")
        {
            ResultArgs resultarg = new ResultArgs();
            using (TallyConnector TallyERPConnector = new TallyConnector())
            {
                if (string.IsNullOrEmpty(DateFrom))
                {
                    resultarg = TallyERPConnector.FetchTally(enTallyTbl);
                }
                else
                {
                    resultarg = TallyERPConnector.FetchTally(enTallyTbl, DateFrom);
                }
            }
            return resultarg;
        }

        private void LoadCompany()
        {
            using (TallyConnector TallyERPConnector = new TallyConnector())
            {
                ResultArgs resultArg = TallyERPConnector.FetchCurrentCompanyDetails();
                if (resultArg != null && resultArg.Success)
                {
                    dtCompany = resultArg.DataSource.Table;
                    if (dtCompany != null && dtCompany.Rows.Count > 0)
                    {
                        CurrentCompanyName = dtCompany.Rows[0]["Company_Name"].ToString();
                        StartingDate = UtilityMember.DateSet.ToDate(dtCompany.Rows[0]["Starting_From"].ToString(), false);
                        BooksBeginningDate = UtilityMember.DateSet.ToDate(dtCompany.Rows[0]["Books_Begin"].ToString(), false);
                        IsDonorModuleEnabled = dtCompany.Rows[0]["IsDonorModuleEnabled"].ToString().Equals("True") ? true : false;
                        deDataFrom.Properties.MinValue = BooksBeginningDate;
                        deDataFrom.Properties.MaxValue = DateTime.Now;
                        deDateTo.Properties.MinValue = BooksBeginningDate;
                        deDateTo.Properties.MaxValue = DateTime.Now;

                        IsConnectionEstablished = true;

                        //done by alwar on 02/12/2015 to load Acmerp.erp projects for mapping with tally------------------------
                        LoadProject();
                        //---------------------------------------------------------------------
                    }
                }
                else
                {
                    IsConnectionEstablished = false;
                    ShowMessageBox(resultArg.Message);
                }
            }
        }

        private string MySQLDateFormat(DateTime deDate)
        {
            return deDate.ToString("yyyy-MM-dd");
        }

        private void SetProgressBar()
        {
            progressBar.Properties.Minimum = 0;
            progressBar.Properties.Maximum = 0;
            progressBar.Properties.Step = 1;
            progressBar.PerformStep();
            progressBar.Visible = true;
        }

        private void TallyMigration_InitProgressBar(object sender, EventArgs e)
        {
            progressBar.Properties.Minimum = 0;
            progressBar.Properties.Maximum = 0;
            progressBar.Properties.Step = 1;
            progressBar.PerformStep();
            progressBar.Visible = true;
        }

        private void TallyMigration_IncreaseProgressBar(object sender, EventArgs e)
        {
            progressBar.PerformStep();
            Application.DoEvents();
        }

        private void TallyMigration_UpdateMessage(object sender, EventArgs e)
        {
            progressBar.Properties.Maximum = TallyMigration.ProgressMaxCount;
            if (!TallyMigration.IsMigrateionCompleted)
                lblMessageInfo.Text = String.Format("Migrating {0}.....", TallyMigration.StatusMessage);
            else
                lblMessageInfo.Text = String.Format("{0}...........", TallyMigration.StatusMessage);
            lblMessageInfo.Update();
            Application.DoEvents();
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

        private void StartTimer()
        {
            StartTime = DateTime.Now;
            TotalElapsedTime = CurrentElapsedTime;
            timer1.Start();
        }

        /// <summary>
        /// Created by alwar on 02/12/2015
        /// Load Acmerp.erp projects for mapping with tally
        /// </summary>
        private void LoadProject()
        {
            using (MappingSystem mappingSystem = new MappingSystem())
            {
                ResultArgs resultArgs = mappingSystem.FetchPJLookup();
                glkpProject.Properties.DataSource = null;
                if (resultArgs.Success)
                {
                    DataTable dtProjects = resultArgs.DataSource.Table;
                    this.UtilityMember.ComboSet.AddEmptyItem(dtProjects, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName,
                        mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, string.Empty);
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProject, dtProjects,
                        mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);

                    if (!String.IsNullOrEmpty(CurrentCompanyName))
                    {
                        //glkpProject.Text = CurrentCompanyName;
                        using (ProjectSystem projectsys = new ProjectSystem())
                        {
                            resultArgs = projectsys.FetchProjectIdByProjectName(CurrentCompanyName);
                            glkpProject.EditValue = 0;  //by default empty for new
                            if (resultArgs.Success && resultArgs.DataSource.Sclar.ToInteger > 0)
                            {
                                glkpProject.EditValue = resultArgs.DataSource.Sclar.ToInteger;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// On 12/02/2021, 
        /// </summary>
        /// <returns></returns>
        private bool Validate()
        {
            bool rtn = false;
            //On 12/02/2021, allow to migrate multiplse voucher types, (If more than one Receipts/Payments/Journal Voucher Types)
            if (chkMultipleVTypes.Checked)
            {
                if (this.ShowConfirmationMessage("As Include Multiple Voucher Types option is enabled," +
                            "Are you sure to Migrate Multiple Voucher Types from TALLY ?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    rtn = true;
                }
            }
            else
            {
                rtn = true;
            }

            if (rtn)
            {
                using (TallyMigrationSystem tallyMigration = new TallyMigrationSystem())
                {
                    string migrationproject = CurrentCompanyName;
                    if (glkpProject.EditValue != null && this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) > 0)
                    {
                        migrationproject = glkpProject.Text;
                    }
                    tallyMigration.CurrentCompanyName = migrationproject;

                    //On 07/07/2023, Don't update Books Begin (As we validated pre-condition to migrate greater than first fiance of Acme.erp)
                    if (deDataFrom.DateTime < AppSetting.FirstFYDateFrom)
                    {
                        this.ShowMessageBoxWarning("Migrating Date From (" + deDataFrom.DateTime.ToShortDateString() + ") " +
                                   "should be greater than Acme.erp first Finance Year (" + AppSetting.FirstFYDateFrom.ToShortDateString() + "). ");
                        rtn = false;
                    }
                    else
                    {
                        bool bVoucherLocked = tallyMigration.IsAuditVouchersLockedVoucherDate(migrationproject, deDataFrom.DateTime, deDateTo.DateTime);
                        if (bVoucherLocked)
                        {
                            rtn = false;
                        }
                    }
                }
            }

            return rtn;
        }

        #endregion
    }
}