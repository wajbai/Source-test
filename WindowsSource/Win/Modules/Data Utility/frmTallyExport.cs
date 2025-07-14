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
using System.Collections;

namespace ACPP.Modules.Data_Utility
{
    public partial class frmTallyExport : frmFinanceBaseAdd
    {
        #region Variables
        private TallyConnector tallyConnector = null;
        TallyMigrationSystem TallyMigration;
        #endregion
        
        #region Properties

        private bool IsConnectionEstablished
        {get;set;}

        private bool IsLicensedTally
        { get; set; }
        
        private Int32 ProjectId
        {
            get
            {
                Int32 projectid = glkpProject.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
                return projectid;
            }
        }

        private string TallyCurrentCompanyName { get; set; }
                        
        private DateTime TallyBooksBeginningDate { get; set; }

        bool isDonorModuleAvailable = false;
        private bool IsDonorModuleEnabled
        {
            set { isDonorModuleAvailable = value; }
            get { return isDonorModuleAvailable; }
        }

        #endregion

        #region Constructor
        public frmTallyExport()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void frmTallyMigration_Load(object sender, EventArgs e)
        {
            tallyConnector = new TallyConnector();

            this.ShowWaitDialog("Connecting to Tally");
            ResultArgs resultarug = tallyConnector.IsTallyConnectedByXML;
            this.Size = new System.Drawing.Size(595, 230); //215
            this.CenterToParent();
            if (!resultarug.Success)
            {
                MessageRender.ShowMessage(resultarug.Message);
                this.CloseWaitDialog();
                this.Close();
            }
            else
            {
                ResultArgs resultArg = ConnectTally();
                if (!resultArg.Success)
                {
                    this.CloseWaitDialog();
                    this.Close();
                }
            }
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            LoadDefaults();
            ConnectTally();
            lblProgressBar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lblProgressBarCaption.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        /// <summary>
        /// This is main event to call export process
        /// 1. get confirmation to start the export process
        /// 2. If vouchers is already exists in given date range, get confirmation to overwrite or append
        /// 3. Export data to tally
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime deFromDate = UtilityMember.DateSet.ToDate(deDataFrom.DateTime.ToShortDateString(), false);
                DateTime deToDate = UtilityMember.DateSet.ToDate(deDateTo.DateTime.ToShortDateString(), false);
                DateTime deNow = UtilityMember.DateSet.ToDate(DateTime.Now.Date.ToShortDateString(), false);
                bool IncludeOPBalance = chkIncludeOpeningBalance.Checked;
                bool includeAssetOpBalance = chkAssets.Checked; 
                bool includeExpenseOpBalance = chkExpenses.Checked; 
                bool includeIncomeOpBalance = chkIncome.Checked;
                bool includeLiabilitiesOpBalance = chkLiabilities.Checked;
                bool overwrite = false;
                string msg = string.Empty;

                if (IsValidInputs())
                {
                    string projectname = (glkpProject.EditValue != null ? glkpProject.Text : string.Empty);
                    //1. get confirmation to start the export process
                    if (this.ShowConfirmationMessage("Are you sure to export " + projectname + "'s data into Tally Company (" + TallyCurrentCompanyName + ")",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        deDataFrom.Enabled = false;
                        deDateTo.Enabled = false;
                        btnExport.Enabled = false;
                        this.Size = new System.Drawing.Size(595, 335);
                        this.CenterToParent();
                        lblProgressBar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        lblProgressBarCaption.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        UpdateMessage(this.GetMessage(MessageCatalog.Master.DataUtilityForms.CONNECTING_TALLY));

                        using (TallyMigrationSystem tallysystem = new TallyMigrationSystem())
                        {
                            tallysystem.UpdateExportProgressStatusMessage += new EventHandler(tallysystem_UpdateExportProgressStatusMessage);
                            tallysystem.IncreaseExportProgressBar += new EventHandler(tallysystem_IncreaseExportProgressBar);
                            this.Cursor = Cursors.WaitCursor;

                            //2. If vouchers is already exists in given date range, get confirmation to overwrite or append
                            ResultArgs resultargs = tallyConnector.FetchVouchersRegister(deFromDate, deToDate);
                            this.Cursor = Cursors.Default;
                            DialogResult dgresultOverwrite = System.Windows.Forms.DialogResult.No;
                            if (resultargs.Success)
                            {
                                if (resultargs.DataSource.Table != null && resultargs.DataSource.Table.Rows.Count > 0)
                                {
                                    string strMessage = String.Format("Vocuhers are available in Tally ( {4} ) for given date range. " +
                                                                      "{5}Do you want to delete the Vouchers and continue?{3}{0}Yes       : " +
                                                                      "Delete vouchers and Continue for the given period{1}No        : Append with existing Vouchers{2}Cancel : Cancel the process.",
                                                                       Environment.NewLine, Environment.NewLine, Environment.NewLine,
                                                                       Environment.NewLine, TallyCurrentCompanyName, Environment.NewLine);
                                    dgresultOverwrite = this.ShowConfirmationMessage(strMessage, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);   
                                }

                                if (dgresultOverwrite != DialogResult.Cancel)
                                {
                                    //3. Export data to tally
                                    overwrite = (dgresultOverwrite == DialogResult.Yes ? true : false);

                                    this.Cursor = Cursors.WaitCursor;
                                    tallysystem.IncludeMultipleVoucherTypes = (chkIncludeMultipleVoucherTypes.Checked);
                                    resultargs = tallysystem.ExportToTally(ProjectId, projectname, TallyCurrentCompanyName, deFromDate, deToDate, TallyBooksBeginningDate,
                                                        IncludeOPBalance, overwrite, includeAssetOpBalance, includeExpenseOpBalance, includeIncomeOpBalance, includeLiabilitiesOpBalance);

                                    if (resultargs.Success)
                                    {
                                        msg = "Export to Tally is completed";
                                    }
                                    else
                                    {
                                        msg = "Export to Tally is not completed, " + resultargs.Message;
                                    }
                                    this.Cursor = Cursors.Default;
                                }
                                else
                                {
                                    msg = "Export to Tally is cancelled";
                                }
                            }
                            
                            if (resultargs.Success)
                            {
                                this.Cursor = Cursors.Default;
                                lblProgressBar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                lblProgressBarCaption.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                this.Size = new System.Drawing.Size(595, 300);
                                this.CenterToParent();
                                UpdateMessage(msg);
                                MessageRender.ShowMessage(msg, false);
                            }
                            else
                            {
                                this.Cursor = Cursors.Default;
                                lblProgressBar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                lblProgressBarCaption.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                this.Size = new System.Drawing.Size(595, 300);
                                this.CenterToParent();
                                MessageRender.ShowMessage(resultargs.Message);
                            }

                            deDataFrom.Enabled = true;
                            deDateTo.Enabled = true;
                            btnExport.Enabled = true;
                        }
                    }
                }
            }
            catch(Exception err)
            {
                this.Cursor = Cursors.Default;
                MessageRender.ShowMessage(err.Message);
            }
        }
        
        #endregion

        #region Methods
       
        /// <summary>
        /// Default setting
        /// </summary>
        private void LoadDefaults()
        {
            lcgCompanyInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcgCompanyInfo.Expanded = false;
        }

        /// <summary>
        /// This method is used to connect to Tally, if connected, get current comapny name and books of beginging
        /// </summary>
        /// <returns></returns>
        private ResultArgs ConnectTally()
        {
            this.ShowWaitDialog("Connecting to Tally");
            ResultArgs resultArg = LoadCompanyInformation();
            if (resultArg.Success)
            {
                lcgCompanyInfo.Visibility = LayoutVisibility.Always;
                lcgCompanyInfo.Expanded = true;
                lblBookBeginningFromYear.Text = TallyBooksBeginningDate.ToShortDateString();
                lblCompanyName.Text = TallyCurrentCompanyName.ToString();
                lblConnectTally.Visibility = LayoutVisibility.Never;

                lcgCompanyInfo.Visibility = LayoutVisibility.Always;
                lcgCompanyInfo.Expanded = true;

                deDataFrom.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false);
                deDataFrom.Properties.MaxValue = DateTime.Now;
                deDateTo.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false); ;
                deDateTo.Properties.MaxValue = DateTime.Now;

                deDataFrom.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
                deDateTo.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
                lblBookBeginningFromYear.Text = TallyBooksBeginningDate.ToShortDateString();
                lblCompanyName.Text = TallyCurrentCompanyName.ToString();
                lblConnectTally.Visibility = LayoutVisibility.Never;
                this.Size = new System.Drawing.Size(595, 300);
                this.CenterToParent();
                this.CloseWaitDialog();
            }
            else
            {
                this.CloseWaitDialog();
                LoadDefaults();
                MessageRender.ShowMessage(resultArg.Message);
            }
            return resultArg;
        }

        

        /// <summary>
        /// This method is used to get current company details from Tally
        /// </summary>
        /// <returns></returns>
        private ResultArgs LoadCompanyInformation()
        {
            ResultArgs resultArgs = tallyConnector.IsMoreThanOneTallyRunningInstance;
            if (!resultArgs.Success)
            {
                resultArgs = tallyConnector.FetchCurrentCompanyDetails();
                if (resultArgs != null && resultArgs.Success)
                {
                    DataTable dtCompany = resultArgs.DataSource.Table;
                    if (dtCompany != null && dtCompany.Rows.Count > 0)
                    {
                        TallyCurrentCompanyName = dtCompany.Rows[0]["COMPANY_NAME"].ToString();
                        //IsDonorModuleEnabled = dtCompany.Rows[0]["IsDonorModuleEnabled"].ToString().Equals("True") ? true : false;
                        //StartingDate = UtilityMember.DateSet.ToDate(dtCompany.Rows[0]["$StartingFrom"].ToString(), false);
                        TallyBooksBeginningDate = UtilityMember.DateSet.ToDate(dtCompany.Rows[0]["BOOKS_BEGIN"].ToString(), false);
                        IsLicensedTally = Convert.ToBoolean(dtCompany.Rows[0]["LICENSEDTALLY"].ToString());

                        //For Temp for Kalis
                        //IsLicensedTally = true;

                        IsConnectionEstablished = true;

                        LoadProject();
                    }
                }
                else
                {
                    IsConnectionEstablished = false;
                }
            }
            else
            {
                resultArgs.Message = "Could not connect to Tally, more than one instance of Tally is running";
            }

            return resultArgs;
        }
        
        /// <summary>
        /// Capture Export process event and update message and record status status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tallysystem_IncreaseExportProgressBar(object sender, EventArgs e)
        {
            if (sender != null)
            {
                string recordcount = sender as string;
                if (!string.IsNullOrEmpty(recordcount))
                {
                    lblMessageInfo.Text = recordcount.ToString();
                }
            }
            progressBar.PerformStep();
            Application.DoEvents();
        }

        /// <summary>
        /// Capture Export process event and update message and reset progressbar status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tallysystem_UpdateExportProgressStatusMessage(object sender, EventArgs e)
        {
            if (sender != null)
            {
                ArrayList exportinginfo = sender as ArrayList;
                if (exportinginfo!=null && exportinginfo.Count == 2)
                {                    
                    string exportmessage = exportinginfo[0].ToString();
                    Int32 maxcount = UtilityMember.NumberSet.ToInteger(exportinginfo[1].ToString());
                    progressBar.EditValue = 0;
                    progressBar.Properties.Step = 1;
                    progressBar.Properties.Maximum = maxcount;
                    progressBar.Properties.Minimum = 0;
                    lblMessageInfo.Text = exportmessage;
                    Application.DoEvents();
                    progressBar.PerformStep();
                    progressBar.Visible = true;
                }
            }
        }
                
        private void UpdateMessage(string Message)
        {
            lblMessageInfo.Text = Message;
            lblMessageInfo.Update();
            Application.DoEvents();
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
                    
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProject, dtProjects,
                        mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);

                    if (!String.IsNullOrEmpty(TallyCurrentCompanyName))
                    {
                        //glkpProject.Text = CurrentCompanyName;
                        using (ProjectSystem projectsys = new ProjectSystem())
                        {
                            resultArgs = projectsys.FetchProjectIdByProjectName(TallyCurrentCompanyName);
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
        /// Validate date range project
        /// 1. Check Project
        /// 2. Check Date Range (Date from and Date To)
        /// 3. Check Tally version, licensed or not
        /// </summary>
        /// <returns></returns>
        private bool IsValidInputs()
        {
            bool Rtn = true;
            DateTime deFromDate = UtilityMember.DateSet.ToDate(deDataFrom.DateTime.ToShortDateString(), false);
            DateTime deToDate = UtilityMember.DateSet.ToDate(deDateTo.DateTime.ToShortDateString(), false);
            
            //1. Check valid inputs Project, DateFrom, DateTo
            if (ProjectId == 0)
            {
                MessageRender.ShowMessage("Project is empty");
                Rtn = false;
            }
            else if (deFromDate.CompareTo(TallyBooksBeginningDate) < 0)
            {
                MessageRender.ShowMessage("Date From is earlier than Tally Books of Beginning");
                Rtn = false;
            }
            else if (deFromDate.CompareTo(deToDate.Date) > 0)
            {
                MessageRender.ShowMessage("Date To is lesser than Date To");
                Rtn = false;
            }

            //2. Check Tally Licensed Mode
            if (Rtn)
            {
                //3. If Tally is not running license mode, date range must be 01 or 02 of any month
                if (!IsLicensedTally)
                {
                    if (deFromDate.Day <= 2)
                    {
                        double noofdays = Math.Abs((deFromDate - deToDate).Days);//  Math.Abs((deFromDate - deToDate).TotalDays);
                        if (noofdays >= 2)
                        {
                            MessageRender.ShowMessage("Tally is not running in licensed mode, Date range must be first two dates of the month");
                            Rtn = false;
                        }
                    }
                    else
                    {
                        MessageRender.ShowMessage("Tally is not running in licensed mode, Date range must be first two dates of the month");
                        Rtn = false;
                    }

                }
            }

            if (!Rtn)
            {
                this.glkpProject.Focus();
            }
            return Rtn;
        }
               
        #endregion       

        private void chkIncludeOpeningBalance_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIncludeOpeningBalance.Checked == false)
            {
                chkAssets.Checked = chkIncome.Checked = chkExpenses.Checked = chkLiabilities.Checked = false;
            }
            else if (chkAssets.Checked == false && chkIncome.Checked == false && chkExpenses.Checked == false && chkLiabilities.Checked == false)
            {
                chkAssets.Checked = chkIncome.Checked = chkExpenses.Checked = chkLiabilities.Checked = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void LedgerOpeningBalanceSelection()
        {
            if (chkAssets.Checked == false && chkIncome.Checked == false && chkExpenses.Checked == false && chkLiabilities.Checked == false)
            {
                chkIncludeOpeningBalance.Checked = false;
            }
            else
            {
                chkIncludeOpeningBalance.Checked = true;
            }
        }

        private void chkAssets_CheckedChanged(object sender, EventArgs e)
        {
            LedgerOpeningBalanceSelection();
        }

        private void chkExpenses_CheckedChanged(object sender, EventArgs e)
        {
            LedgerOpeningBalanceSelection();
        }

        private void chkIncome_CheckedChanged(object sender, EventArgs e)
        {
            LedgerOpeningBalanceSelection();
        }

        private void chkLiabilities_CheckedChanged(object sender, EventArgs e)
        {
            LedgerOpeningBalanceSelection();
        }
    }
}