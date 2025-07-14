using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Utility;
using Bosco.Report.Base;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraNavBar;
using DevExpress.Office.Utils;
using DevExpress.DocumentView;
using DevExpress.DocumentView.Controls;
using System.Reflection;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Bosco.Report.ReportObject;
using Bosco.DAO.Data;
using Bosco.Utility.ConfigSetting;
using Bosco.DAO.Schema;
using System.IO;
using DevExpress.Utils;
using DevExpress.XtraPrinting.Native;
using System.Diagnostics;
using System.Globalization;
//using Payroll.DAO.Schema;


namespace Bosco.Report.View
{
    public partial class ReportViewer : DevExpress.XtraEditors.XtraUserControl
    {
        private string lastSelectedReportId = "";
        ReportBase activeReport = null;
        public bool FromViewScreen = false;
        private Stack<EventDrillDownArgs> stackActiveDrillDownReports = new Stack<EventDrillDownArgs>();
        private CommonMember utilityMember = null;
        UserProperty settinguserpropertyproperty = new UserProperty();

        private ReportSetting reportSetting1 = new ReportSetting();

        //to store current scroll bar value (for drill back )
        Nullable<PointF> previousScrollPosition;

        public string ReportId
        {
            get { return lastSelectedReportId; }
            set
            {
                if (value != string.Empty)
                {
                    InitialDrillDownProperties(value);
                    //AssignSignDetails();//// Assign Values to Report Setup based on the values that are available in the properties.
                    ReportProperty.Current.AssignSignDetails(ReportProperty.Current.Project);

                    if (value == "RPT-179" || value == "RPT-189")
                    {
                        AssignBudgetNewProject(value);//// Assign Values to Report Setup based on the values that are available in the properties.
                    }

                    HandleReportViewerButtons();
                    LoadReport(value, true);
                }
            }
        }

        public bool IsResizingReports
        {
            get
            {
                return (this.ReportId == "RPT-001" || this.ReportId == "RPT-002" || this.ReportId == "RPT-003" || // Monthly Abstract Receipts, Payments and Both
                    this.ReportId == "RPT-027" || //Receipts and Payments
                    this.ReportId == "RPT-101" || this.ReportId == "RPT-062" ||  //Multi Column Cash Band Bank
                    //this.ReportId == "RPT-047" || // FD Register
                    this.ReportId == "RPT-153" || this.ReportId == "RPT-154" || this.ReportId == "RPT-206" || //Cash/Bank Receipts  (Voucher Number), Cash/Bank Payments (Voucher Number), Purpose-wise Receipts/Utilisation Distribution
                    this.ReportId == "RPT-069" || this.ReportId == "RPT-071"); //Pay Regiser and Pay Wages            
            }
        }

        protected CommonMember UtilityMember
        {
            get
            {
                if (utilityMember == null) { utilityMember = new CommonMember(); }
                return utilityMember;
            }
        }

        public ReportViewer()
        {
            InitializeComponent();
            previewBar1.Manager.AllowCustomization = previewBar1.Manager.AllowQuickCustomization =
                previewBar1.Manager.AllowShowToolbarsPopup = previewBar1.Manager.AllowMoveBarOnToolbar = false;

            previewBar2.Manager.AllowCustomization = previewBar2.Manager.AllowQuickCustomization =
                previewBar2.Manager.AllowShowToolbarsPopup = previewBar2.Manager.AllowMoveBarOnToolbar = false;
        }

        private void PrintingSystem_AfterMarginsChange(object sender, MarginsChangeEventArgs e)
        {
            ChangeReportSettings(sender);
        }

        //14/03/2017
        /// When drill-down , we use existing general ledger report for drill ledger report (for particular ledger).
        /// if user generate general ledger in another tab, it should not overlap drilled and general ledger
        public void LoadReport(string reportId, bool forceRefresh)
        {

            //On 04/03/2021, To load different Report format for Budget Annual Summary for Developmental Repors
            if (SettingProperty.Current.ConsiderBudgetNewProject == 1 && reportId == "RPT-179")
            {
                reportId = "RPT-189";
            }
            else if (SettingProperty.Current.ConsiderBudgetNewProject == 0 && reportId == "RPT-189")
            {
                reportId = "RPT-179";
            }

            if (this.activeReport != null && this.activeReport.IsDrillDownMode)
            {
                LoadReport(reportId, forceRefresh, true);
            }
            else
            {
                LoadReport(reportId, forceRefresh, false);
            }

            //if (this.ReportId == "RPT-047")
            //{
            if (pcReport.PrintingSystem != null)
            {
                //On 23/10/2017, resize all columns only for FD Register, multi Abstracts (Receipt, Payments and Both)
                //later,it will be implemented for all the reports

                if (this.IsResizingReports) //Pay Regiser and Pay Wages
                {
                    pcReport.PrintingSystem.AfterMarginsChange += new MarginsChangeEventHandler(PrintingSystem_AfterMarginsChange);
                    pcReport.PrintingSystem.PageSettingsChanged += new EventHandler(PrintingSystem_PageSettingsChanged);

                    //On 25/08/2022, To load report in Landscape mode by default if generlate ledger group ------------
                    if (reportId == "RPT-027")
                    {
                        if (!(ReportProperty.Current.ReportCodeType == (int)ReportCodeType.Standard))
                        {
                            activeReport.PrintingSystem.PageSettings.Landscape = false;
                        }
                        else
                        {
                            //On 03/02/2023, avaoid resetting report landscape
                            //activeReport.PrintingSystem.PageSettings.Landscape = false;
                            activeReport.PrintingSystem.PageSettings.Landscape = false;
                        }
                    }
                    //--------------------------------------------------------------------------------------------------

                    //as on 03/02/2023, To restore Page setting and Margin
                    if ((ReportProperty.Current.ReportId == "RPT-069" || ReportProperty.Current.ReportId == "RPT-071") ||
                        (pcReport.PrintingSystem.PageSettings.Landscape != (ReportProperty.Current.PaperLandscape == 1 ? true : false)))
                    {
                        //if (ReportProperty.Current.MarginLeft > 0)
                        pcReport.PrintingSystem.PageSettings.Margins.Left = ReportProperty.Current.MarginLeft;
                        pcReport.PrintingSystem.PageSettings.Margins.Right = ReportProperty.Current.MarginRight;
                        pcReport.PrintingSystem.PageSettings.Margins.Top = ReportProperty.Current.MarginTop;
                        pcReport.PrintingSystem.PageSettings.Margins.Bottom = ReportProperty.Current.MarginBottom;

                        pcReport.PrintingSystem.PageSettings.PaperKind = ReportProperty.Current.PaperKind;
                        pcReport.PrintingSystem.PageSettings.Landscape = (ReportProperty.Current.PaperLandscape == 1 ? true : false);
                    }
                }
                HandleReportViewerButtons();
            }
            //}

            /*using (MemoryStream ms = new MemoryStream())
            {
                activeReport.ExportToText(ms);
                ms.Position = 0;
                var sr = new StreamReader(ms);
                var myStr = sr.ReadToEnd();

                Clipboard.SetText(myStr);
            }*/

        }

        private void PrintingSystem_PageSettingsChanged(object sender, EventArgs e)
        {
            ChangeReportSettings(sender);
        }

        private void LoadReport(string reportId, bool forceRefresh, bool drilldown, int DrillHeaderInstituteSocietyName = -1, int DrillHeaderInstituteSocietyAddress = -1)
        {
            if (!string.IsNullOrEmpty(reportId))
            {
                if (reportId != lastSelectedReportId || forceRefresh)
                {
                    bbiProperty.Enabled = true;
                    bbiRefresh.Enabled = true;
                    bbiCustomPrint.Enabled = true;

                    //drilldownBarBtn.Enabled = RefreshbarButtonItem3.Enabled = true;

                    //On 06/06/2017, Reset Amount Filter condtion when user go for next report
                    if (reportId != lastSelectedReportId)
                    {
                        ReportProperty.Current.DonorConditionSymbol = string.Empty;
                        ReportProperty.Current.DonorCondtionName = 0;
                        ReportProperty.Current.DonorFilterAmount = 0;
                    }

                    try
                    {
                        if (reportId != lastSelectedReportId || ReportProperty.Current.ReportId != reportId)
                            ReportProperty.Current.ReportId = reportId;

                        string reportAssemblyType = ReportProperty.Current.ReportAssembly;
                        ReportBase report = UtilityMember.GetDynamicInstance(reportAssemblyType, null) as ReportBase;
                        activeReport = report;
                        report.ReportId = reportId;
                        if (drilldown)
                        {
                            report.IsDrillDownMode = true;
                        }
                        report.AfterPrint += new EventHandler(report_AfterPrint);
                        AttachDrillDownProperties(report);
                        ReportProperty.Current.stackActiveDrillDownHistory = stackActiveDrillDownReports;

                        //On 12/04/2019, to pass base report's Header proerpty
                        //and retain base report setting to drilled reports--------------------------------------
                        if (DrillHeaderInstituteSocietyName >= 0)
                        {
                            ReportProperty.Current.HeaderInstituteSocietyName = DrillHeaderInstituteSocietyName;
                        }

                        if (DrillHeaderInstituteSocietyAddress >= 0)
                        {
                            ReportProperty.Current.HeaderInstituteSocietyAddress = DrillHeaderInstituteSocietyAddress;
                        }
                        //------------------------------------------------------

                        report.FromViewScreen = FromViewScreen; //On 08/01/2020, set from view screen or report
                        report.ShowReport();
                        pcReport.PrintingSystem = report.PrintingSystem;


                    }
                    catch (Exception ex)
                    {
                        MessageRender.ShowMessage(ex.Message, true);
                    }
                    finally { }
                }

                lastSelectedReportId = reportId;
            }
        }


        /// <summary>
        /// This method is used to reassing scroll bar possition when drill back from other reports
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void report_AfterPrint(object sender, EventArgs e)
        {
            if (previousScrollPosition != null)
            {
                pcReport.ViewManager.SetHorizScroll(previousScrollPosition.Value.X);
                pcReport.ViewManager.SetVertScroll(previousScrollPosition.Value.Y);
                pcReport.UpdateScrollBars();
            }
        }

        private void bbiProperty_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenProperty();
        }

        private void OpenProperty()
        {
            if (activeReport != null)
            {
                //Clear previous scroll possition
                previousScrollPosition = null;
                if (activeReport.ReportId == "RPT-061") //ReportProperty.Current.ReportId == "RPT-061"
                {
                    activeReport.ShowTDSChallanReconciliationForm();
                }
                else if (activeReport.ReportId == "RPT-069" || activeReport.ReportId == "RPT-070" ||
                         activeReport.ReportId == "RPT-071" || activeReport.ReportId == "RPT-168" ||
                         activeReport.ReportId == "RPT-170" || activeReport.ReportId == "RPT-171" ||
                         activeReport.ReportId == "RPT-172" || activeReport.ReportId == "RPT-202" ||
                         activeReport.ReportId == "RPT-203" || activeReport.ReportId == "RPT-217")
                {
                    ReportProperty.Current.ReportId = this.ReportId;   // Added By Praveen to refresh Report ID So that while selecting Criteria form appropriate Report can be loaded...
                    activeReport.ShowPayslipForm();
                }
                else if (activeReport.ReportId == "RPT-147") //For Cheque Printing //ReportProperty.Current.ReportId == "RPT-147"
                {
                    activeReport.ShowBankVoucher();
                }
                else if (activeReport.ReportId != "RPT-025" && activeReport.ReportId != "RPT-024" && activeReport.ReportId != "RPT-026" &&
                            activeReport.ReportId != "RPT-144" && activeReport.ReportId != "RPT-151" && activeReport.ReportId != "RPT-207" &&
                            activeReport.ReportId != "RPT-212")
                {
                    //As on 11/05/2022, to have proper report fillter form (if all voucher prints from voucher screens)
                    //(ReportProperty.Current.ReportId != "RPT-025" && ReportProperty.Current.ReportId != "RPT-024" && ReportProperty.Current.ReportId != "RPT-026" &&
                    //ReportProperty.Current.ReportId != "RPT-144" && ReportProperty.Current.ReportId != "RPT-151")

                    ReportProperty.Current.ReportId = this.ReportId;   // Added By Praveen to refresh Report ID So that while selecting Criteria form appropriate Report can be loaded...
                    activeReport.ShowReportFilterDialog();
                    if (ReportProperty.Current.ReportId == "RPT-101" || // Only For Multi Column Cashbank Report Control to be re initialised so This method is called to re-initializes by Praveen
                        (ReportProperty.Current.ReportId == "RPT-185" && activeReport.dialogResult == DialogResult.OK) ||//For Budget Annual - Quaterly realizATion
                        (ReportProperty.Current.ReportId == "RPT-030" && activeReport.dialogResult == DialogResult.OK) ||
                        (ReportProperty.Current.ReportId == "RPT-182" && activeReport.dialogResult == DialogResult.OK) ||
                        (ReportProperty.Current.ReportId == "RPT-208" && activeReport.dialogResult == DialogResult.OK))  //for Budget Year comparision
                    {
                        LoadReport(this.ReportId, true);
                    }

                    //On 25/08/2022, To load report in Landscape mode by default if generlate ledger group ------------
                    if (ReportProperty.Current.ReportId == "RPT-027")
                    {
                        if (!(ReportProperty.Current.ReportCodeType == (int)ReportCodeType.Standard))
                        {
                            activeReport.PrintingSystem.PageSettings.Landscape = true;
                        }
                        else
                        {
                            activeReport.PrintingSystem.PageSettings.Landscape = false;
                        }
                    }
                }
                else
                {
                    activeReport.ShowFiancialReportFilterDialog();
                }

                HandleReportViewerButtons();
            }
        }

        /// <summary>
        /// On 11/12/2019, this method is used to assign Budget new projects for Budget Annual summary
        /// </summary>
        private void AssignBudgetNewProject(string currentreportid)
        {
            ReportSetting.ReportBudgetNewProjectDataTable dtReportBudgetNewProjects = new ReportSetting.ReportBudgetNewProjectDataTable();
            ReportProperty.Current.BudgetNewProjects = dtReportBudgetNewProjects;

            if (currentreportid == "RPT-179" || currentreportid == "RPT-189")
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Setting.FetchBudgetNewProjects))
                {
                    dataManager.Parameters.Add(dtReportBudgetNewProjects.ACC_YEAR_IDColumn, SettingProperty.Current.AccPeriodId);
                    if (SettingProperty.Current.CreateBudgetDevNewProjects == 1)
                    {
                        string budgetids = string.IsNullOrEmpty(ReportProperty.Current.Budget) || ReportProperty.Current.Budget == "" ? "0" : ReportProperty.Current.Budget;
                        dataManager.Parameters.Add(dtReportBudgetNewProjects.DEVELOPMENTAL_NEW_BUDGETIDColumn, budgetids);
                    }
                    else
                    {
                        dataManager.Parameters.Add(dtReportBudgetNewProjects.DEVELOPMENTAL_NEW_BUDGETIDColumn, "0");
                    }
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    ResultArgs resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);

                    if (resultArgs.Success)
                    {
                        DataTable dtBudgetNewProjects = resultArgs.DataSource.Table;
                        if (dtBudgetNewProjects.Rows.Count == 0)
                        {
                            dtBudgetNewProjects.Rows.Add(dtBudgetNewProjects.NewRow());
                        }
                        ReportProperty.Current.BudgetNewProjects = dtBudgetNewProjects;
                    }
                }
            }
        }


        #region Drill-Down Methods
        /// <summary>
        /// Key press event on the report viewer report control,
        /// If user press ESC, it will redirect to recent rpt file
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Escape))
            {
                DrillDownTarget(GetRecentDrillDown());
                return true;
            }
            else if (keyData == (Keys.P))
            {
                OpenProperty();
            }
            else if (keyData == (Keys.R))
            {
                LoadReport(this.ReportId, true);
            }


            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void InitialDrillDownProperties(string RptId)
        {
            if (RptId != string.Empty)
            {
                ClearDrillDown();
                //Add base report into the collection for drill down, when it gets loaded in the viewer
                EventDrillDownArgs baseRepotEventArug = new EventDrillDownArgs(DrillDownType.BASE_REPORT, RptId, new Dictionary<string, object>());
                AddDrillDown(baseRepotEventArug);
            }
        }


        /// <summary>
        /// This event will be called when user clicks, on the report record fields in report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void report_ReportDrillDown(object sender, EventDrillDownArgs e)
        {
            //MessageBox.Show("Drill-Down Level ::" + sReportId);
            if (DrillDownTarget(e))
            {
                AddDrillDown(e);
            }
        }

        /// <summary>
        /// This method used to load drilldown/ledger/end transaction screen based on the event triggered 
        /// when user clicks
        /// </summary>
        /// <param name="eDrilldownevent"></param>
        private bool DrillDownTarget(EventDrillDownArgs eDrilldownevent)
        {
            bool bSucessDrillDown = false;
            bool isDrilldown = false;
            if (eDrilldownevent != null && eDrilldownevent.DrillDownRpt != string.Empty)
            {
                // Load Drill-Down Report for selected Group
                //MessageBox.Show("Drill-Down Level ::" + stackActiveDrillDownReports.Count.ToString());
                switch (eDrilldownevent.DrillDownType)
                {
                    case DrillDownType.BASE_REPORT:
                    case DrillDownType.GROUP_SUMMARY:
                    case DrillDownType.GROUP_SUMMARY_RECEIPTS:
                    case DrillDownType.GROUP_SUMMARY_PAYMENTS:
                    case DrillDownType.LEDGER_SUMMARY:
                    case DrillDownType.LEDGER_SUMMARY_RECEIPTS:
                    case DrillDownType.LEDGER_SUMMARY_PAYMENTS:
                    case DrillDownType.LEDGER_CASH:
                    case DrillDownType.LEDGER_BANK:
                    case DrillDownType.TDS_PARTY_WISE:
                    case DrillDownType.TDS_OUTSTANDING_LEDGER:
                    case DrillDownType.TDS_OUTSTANDING_NOP:
                    case DrillDownType.FC_REPORT:
                    case DrillDownType.CC_LEDGER_SUMMARY:
                    case DrillDownType.CC_LEDGER_SUMMARY_PAYMENTS:
                    case DrillDownType.CC_LEDGER_SUMMARY_RECEIPTS:
                    case DrillDownType.DRILL_TO_IE_REPORT: //25/06/2019, drill to Diff.IE balance
                    case DrillDownType.DAYBOOK: // 0n 15/09/2021, to drill to daybook
                    case DrillDownType.AUDITLOG: // 0n 16/09/2021, to drill to Audit Log
                        ReportProperty.Current.DrillDownProperties = eDrilldownevent.DrillDownProperties;
                        isDrilldown = (eDrilldownevent.DrillDownType != DrillDownType.BASE_REPORT);
                        //On 12/04/2019, to pass base report's Header proerpty
                        LoadReport(eDrilldownevent.DrillDownRpt, true, isDrilldown, ReportProperty.Current.HeaderInstituteSocietyName, ReportProperty.Current.HeaderInstituteSocietyAddress);
                        bSucessDrillDown = true;

                        //On 23/02/2023, For Temp to set Page size details after paper size
                        if (eDrilldownevent.DrillDownType == DrillDownType.BASE_REPORT)
                        {
                            if (this.IsResizingReports)
                            {
                                //On 09/03/2023, To reset while drilling back
                                bool applyChangePageChanges = (pcReport.PrintingSystem.PageSettings.Landscape != (ReportProperty.Current.PaperLandscape == 1 ? true : false));

                                if (applyChangePageChanges)
                                {
                                    pcReport.PrintingSystem.PageSettings.Landscape = (ReportProperty.Current.PaperLandscape == 1 ? true : false);
                                    ChangeReportSettings(pcReport.PrintingSystem);
                                }

                                pcReport.PrintingSystem.AfterMarginsChange += new MarginsChangeEventHandler(PrintingSystem_AfterMarginsChange);
                                pcReport.PrintingSystem.PageSettingsChanged += new EventHandler(PrintingSystem_PageSettingsChanged);
                            }
                        }

                        break;
                    case DrillDownType.LEDGER_CASHBANK_VOUCHER:
                    case DrillDownType.LEDGER_JOURNAL_VOUCHER:
                    case DrillDownType.FD_VOUCHER:
                    case DrillDownType.DRILL_TO_LEDGER_DEFINE_OPENING_BALANCE: //25/06/2019, drill to Diff.opening balance
                    case DrillDownType.FD_RENEWAL_DRILLDOWN: //added by sugan to drill down fd history to concern record

                        isDrilldown = !IsBaseReport(this.ReportId);
                        DrillDownToTransaction(eDrilldownevent, isDrilldown);
                        break;
                    //****Added by sugan--To make drilldown for FD statement to FD History**********************************************************************************************************************************
                    case DrillDownType.FD_ACCOUNT:
                        ReportProperty.Current.DrillDownProperties = eDrilldownevent.DrillDownProperties;
                        isDrilldown = (eDrilldownevent.DrillDownType != DrillDownType.BASE_REPORT);
                        //On 12/04/2019, to pass base report's Header proerpty
                        LoadReport(eDrilldownevent.DrillDownRpt, true, isDrilldown, ReportProperty.Current.HeaderInstituteSocietyName, ReportProperty.Current.HeaderInstituteSocietyAddress);
                        bSucessDrillDown = true;
                        break;
                    //**************************************************************************************************************************************
                }

                HideDrillDownButtons();
            }
            return bSucessDrillDown;
        }

        private void DrillDownToTransaction(EventDrillDownArgs eDrilldownevent, bool isDrilldown)
        {
            string transactionScreen = string.Empty;
            ResultArgs resultFD = new ResultArgs();
            Int32 ReportFDAccountId = 0;
            Int32 ReportFDRenewalId = 0;

            transactionScreen = ReportProperty.Current.EnumSet.GetDescriptionFromEnumValue(eDrilldownevent.DrillDownType);
            if (transactionScreen != string.Empty)
            {
                if (eDrilldownevent.DrillDownRpt != string.Empty)
                {
                    if (eDrilldownevent.DrillDownProperties != null && eDrilldownevent.DrillDownProperties.Count > 1)
                    {
                        Int32 voucherid = 0;
                        string FDType = string.Empty;
                        string VoucherType = string.Empty;
                        string VoucherSubType = string.Empty;
                        Dictionary<string, object> dicDDProperties = eDrilldownevent.DrillDownProperties;
                        DrillDownType ddtypeLinkType = DrillDownType.BASE_REPORT;
                        ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), dicDDProperties["DrillDownLink"].ToString());
                        bool isvalidfd = true; //by default it will be true, make false when FD is invalid entry

                        if (dicDDProperties.ContainsKey("VOUCHER_PAYMENT_SUB_TYPE") || dicDDProperties.ContainsKey("VOUCHER_SUB_TYPE"))
                        {
                            if (dicDDProperties.ContainsKey("VOUCHER_SUB_TYPE"))
                                VoucherSubType = dicDDProperties["VOUCHER_SUB_TYPE"].ToString();

                            if (dicDDProperties.ContainsKey("VOUCHER_PAYMENT_SUB_TYPE"))
                                VoucherSubType = dicDDProperties["VOUCHER_PAYMENT_SUB_TYPE"].ToString();
                        }

                        if (dicDDProperties.ContainsKey("PAY_VOUCHER_ID"))
                        {
                            voucherid = UtilityMember.NumberSet.ToInteger(dicDDProperties["PAY_VOUCHER_ID"].ToString());
                        }
                        //******added by sugan-for FD statment drill down report***************************************************************************************************************
                        else if (dicDDProperties.ContainsKey("FD_ACCOUNT_ID"))
                        {
                            voucherid = UtilityMember.NumberSet.ToInteger(dicDDProperties["FD_ACCOUNT_ID"].ToString());
                            ReportFDAccountId = UtilityMember.NumberSet.ToInteger(dicDDProperties["FD_ACCOUNT_ID"].ToString());
                            FDType = dicDDProperties["FD_TYPE"].ToString();  // by aldrin to get the fd type
                        }
                        else if (dicDDProperties.ContainsKey("FD_VOUCHER_ID"))
                        {
                            voucherid = UtilityMember.NumberSet.ToInteger(dicDDProperties["FD_VOUCHER_ID"].ToString());
                            FDType = dicDDProperties["FD_TYPE"].ToString();  // by aldrin to get the fd type
                            if (FDType.ToUpper() == FDTypes.OP.ToString().ToUpper())
                            {
                                ReportFDAccountId = voucherid;
                            }
                        }
                        else if (dicDDProperties.ContainsKey("VOUCHER_PAYMENT_SUB_TYPE"))
                        {
                            voucherid = UtilityMember.NumberSet.ToInteger(dicDDProperties["VOUCHER_ID"].ToString());
                            VoucherSubType = dicDDProperties["FD_TYPE"].ToString();
                        }
                        else if (ddtypeLinkType == DrillDownType.DRILL_TO_LEDGER_DEFINE_OPENING_BALANCE) //25/06/2019, drill to Diff.opening balance
                        {
                            voucherid = 0;
                        }
                        //*********************************************************************************************************************
                        else if (dicDDProperties.ContainsKey("R_V1") || dicDDProperties.ContainsKey("R_V2") || dicDDProperties.ContainsKey("R_V3") ||
                                dicDDProperties.ContainsKey("P_V1") || dicDDProperties.ContainsKey("P_V2") || dicDDProperties.ContainsKey("P_V3"))
                        {
                            foreach (string key in dicDDProperties.Keys)
                            {
                                if (key.Equals("R_V1")) voucherid = UtilityMember.NumberSet.ToInteger(dicDDProperties[key].ToString());
                                if (key.Equals("R_V2")) voucherid = UtilityMember.NumberSet.ToInteger(dicDDProperties[key].ToString());
                                if (key.Equals("R_V3")) voucherid = UtilityMember.NumberSet.ToInteger(dicDDProperties[key].ToString());
                                if (key.Equals("P_V1")) voucherid = UtilityMember.NumberSet.ToInteger(dicDDProperties[key].ToString());
                                if (key.Equals("P_V2")) voucherid = UtilityMember.NumberSet.ToInteger(dicDDProperties[key].ToString());
                                if (key.Equals("P_V3")) voucherid = UtilityMember.NumberSet.ToInteger(dicDDProperties[key].ToString());
                                if (voucherid > 0)
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            voucherid = UtilityMember.NumberSet.ToInteger(dicDDProperties["VOUCHER_ID"].ToString());
                        }

                        object[] argsVoucher = new object[1];
                        argsVoucher[0] = voucherid;
                        // by aldrin only for FD alone adding one more parameter to the array.

                        if (dicDDProperties.ContainsKey("FD_VOUCHER_ID") || dicDDProperties.ContainsKey("FD_ACCOUNT_ID"))
                        {
                            Array.Resize<object>(ref argsVoucher, 2);  // this to increase the array size.
                            argsVoucher[1] = FDType;
                            //sometimes they are passing fd_account_id to FD_VOUCHER_ID, so generally we treat voucherid as fd_account_id
                            isvalidfd = activeReport.IsValidFDAccount(voucherid, FDType); //Check valid fd or not..few fd entires interest voucher_id is 0

                            if (FDType == "POI")
                            {
                                isvalidfd = false;
                            }

                            //----------------------------------------------------------------
                            //On 27/10/2021, for POST Interest and Zero values FD renewals
                            //Allow to drill to FD Post Interest voucner and no FD Interest Voucher FD renewals ------------------------------
                            if (!isvalidfd)
                            {
                                //MessageRender.ShowMessage("Invalid FD Voucher, Could not Drill-Down, Update the latest version", false);
                                // On 27/10/2021, Initally for FD renewlal, "FD_Account_ID" was passed in the name of "FD_VOUCHER_ID",
                                //If Post Interest, "FD_VOUCHER_ID" as passed as usaul as "FD_VOUCHER_ID".
                                //If FD renewal with no interest amount, there is no fd voucher id and drill to fd renewl detials.
                                //so we get actual fd accountid, fd voucher interest id and renewal id
                                Int32 fdaAccountid = 0;
                                Int32 fdInterestVoucherid = 0;
                                Int32 fdRenewalid = 0;
                                if (dicDDProperties.ContainsKey("FD_VOUCHER_ID"))
                                {
                                    fdInterestVoucherid = UtilityMember.NumberSet.ToInteger(dicDDProperties["FD_VOUCHER_ID"].ToString());
                                }

                                if (FDType == "POI")
                                {
                                    ResultArgs result = activeReport.GetFDAccountDetails(fdInterestVoucherid);

                                    if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                                    {
                                        DataTable dtIFDs = result.DataSource.Table;
                                        fdaAccountid = utilityMember.NumberSet.ToInteger(dtIFDs.Rows[0]["FD_ACCOUNT_ID"].ToString());
                                        fdRenewalid = utilityMember.NumberSet.ToInteger(dtIFDs.Rows[0]["FD_RENEWAL_ID"].ToString());
                                    }
                                }
                                else
                                {
                                    fdaAccountid = fdInterestVoucherid;
                                }

                                DataSet ds = activeReport.GetFDDetails(fdaAccountid);
                                if (ds != null && ds.Tables.Count == 2)
                                {
                                    if (ds.Tables[0] != null && ds.Tables[1] != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
                                    {
                                        FDTypes fdtype = FDType == "POI" ? FDTypes.POI : FDTypes.RN;
                                        if (dicDDProperties.ContainsKey("FD_RENEWAL_ID"))
                                        {
                                            fdRenewalid = UtilityMember.NumberSet.ToInteger(dicDDProperties["FD_RENEWAL_ID"].ToString());
                                        }
                                        Array.Resize<object>(ref argsVoucher, 6);  // this to increase the array size.
                                        argsVoucher[0] = fdRenewalid;
                                        argsVoucher[1] = fdaAccountid;
                                        argsVoucher[2] = fdtype;
                                        argsVoucher[3] = ds.Tables[0]; //For FD REnewals
                                        argsVoucher[4] = ds.Tables[1]; //For FD REnewals
                                        argsVoucher[5] = true;
                                        isvalidfd = true;
                                    }
                                }

                                ReportFDAccountId = fdaAccountid;
                                ReportFDRenewalId = fdRenewalid;
                                //---------------------------------------------------------------------------------------------------------------------------------------
                            }
                        }
                        if (voucherid > 0 && isvalidfd)
                        {
                            if (FDType != FDTypes.OP.ToString() && activeReport.IsVoucherAlreadyDeleted(voucherid))
                            {
                                MessageRender.ShowMessage("Voucher is already deleted", false);
                            }
                            else if (activeReport.isVoucherPostedByThirdParty(voucherid) && settinguserpropertyproperty.IS_SDB_INM)
                            {
                                MessageRender.ShowMessage("This Voucher is posted by Thrid Party application, can not deleted or modified");
                            }
                            else
                            {
                                Form transactionForm = ReportProperty.Current.GetDynamicInstance(transactionScreen, argsVoucher) as Form;
                                if (VoucherSubType == VoucherSubTypes.PAY.ToString())
                                {
                                    string transactionPayrollForm = "PAYROLL.Modules.Payroll_app.frmPostPaymentVoucher, PAYROLL";
                                    transactionForm = ReportProperty.Current.GetDynamicInstance(transactionPayrollForm, argsVoucher) as Form;
                                }

                                if (transactionForm != null)
                                {
                                    //On 17/10/2022, to lock eiditing FD module
                                    if (!string.IsNullOrEmpty(FDType))
                                    {
                                        //MessageRender.ShowMessage("Entry can be edited in the Fixed Deposit Module.");
                                        using (AcMEDSync.Model.BalanceSystem datasync = new AcMEDSync.Model.BalanceSystem())
                                        {
                                            if (ReportFDAccountId > 0 && ReportFDRenewalId > 0)
                                                resultFD = datasync.IsAllowToModifyFDVoucherEntry(ReportFDAccountId, ReportFDRenewalId, 0);
                                            else if (FDType.ToUpper() == FDTypes.OP.ToString().ToUpper() && ReportFDAccountId > 0 && ReportFDRenewalId == 0)
                                                resultFD = datasync.IsAllowToModifyFDVoucherEntry(ReportFDAccountId, 0, 0);
                                            else if (voucherid > 0)
                                                resultFD = datasync.IsAllowToModifyFDVoucherEntry(0, 0, voucherid);

                                            if (resultFD.Success)
                                            {
                                                //Store current scroll value, it will be used when drown back 
                                                previousScrollPosition = pcReport.ViewManager.ScrollPos;
                                                transactionForm.ShowDialog();
                                                LoadReport(this.ReportId, true);
                                            }
                                            else
                                            {
                                                MessageRender.ShowMessage(resultFD.Message);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //Store current scroll value, it will be used when drown back 
                                        previousScrollPosition = pcReport.ViewManager.ScrollPos;
                                        transactionForm.ShowDialog();
                                        LoadReport(this.ReportId, true);
                                    }
                                }
                            }
                        }
                        else if (voucherid == 0 && dicDDProperties.ContainsKey("FD_VOUCHER_ID") && isvalidfd)
                        {
                            Form transactionForm = ReportProperty.Current.GetDynamicInstance(transactionScreen, argsVoucher) as Form;
                            if (transactionForm != null)
                            {
                                //On 17/10/2022, to lock eiditing FD module
                                if (!string.IsNullOrEmpty(FDType))
                                {
                                    //MessageRender.ShowMessage("Entry can be edited in the Fixed Deposit Module.");
                                    using (AcMEDSync.Model.BalanceSystem datasync = new AcMEDSync.Model.BalanceSystem())
                                    {
                                        if (ReportFDAccountId > 0 && ReportFDRenewalId > 0)
                                            resultFD = datasync.IsAllowToModifyFDVoucherEntry(ReportFDAccountId, ReportFDRenewalId, 0);
                                        else if (FDType.ToUpper() == FDTypes.OP.ToString().ToUpper() && ReportFDAccountId > 0 && ReportFDRenewalId == 0)
                                            resultFD = datasync.IsAllowToModifyFDVoucherEntry(ReportFDAccountId, 0, 0);
                                        else if (voucherid > 0)
                                            resultFD = datasync.IsAllowToModifyFDVoucherEntry(0, 0, voucherid);

                                        if (resultFD.Success)
                                        {
                                            //Store current scroll value, it will be used when drown back 
                                            previousScrollPosition = pcReport.ViewManager.ScrollPos;
                                            transactionForm.ShowDialog();
                                            LoadReport(this.ReportId, true);
                                        }
                                        else
                                        {
                                            MessageRender.ShowMessage(resultFD.Message);
                                        }
                                    }
                                }
                                else
                                {
                                    //Store current scroll value, it will be used when drown back 
                                    previousScrollPosition = pcReport.ViewManager.ScrollPos;
                                    transactionForm.ShowDialog();
                                    LoadReport(this.ReportId, true);
                                }
                            }
                        }
                        else if (voucherid == 0 && ddtypeLinkType == DrillDownType.DRILL_TO_LEDGER_DEFINE_OPENING_BALANCE) //25/06/2019, drill to Diff.opening balance
                        {
                            DrillToOpeningBalance();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 24/06/2019, this method is used to show map project ledger form wtih opening opening balance
        /// </summary>
        private void DrillToOpeningBalance()
        {
            try
            {
                string[] projects = ReportProperty.Current.Project.Split(',');
                if (projects.Length > 0)
                {
                    int projectid = utilityMember.NumberSet.ToInteger(projects.GetValue(0).ToString());
                    string transactionScreen = ReportProperty.Current.EnumSet.GetDescriptionFromEnumValue(DrillDownType.DRILL_TO_LEDGER_DEFINE_OPENING_BALANCE);
                    object[] argsVoucher = new object[3];
                    argsVoucher[0] = MapForm.Transaction;
                    argsVoucher[1] = projectid;
                    argsVoucher[2] = "";

                    Form transactionForm = ReportProperty.Current.GetDynamicInstance(transactionScreen, argsVoucher) as Form;
                    if (transactionForm != null)
                    {
                        previousScrollPosition = pcReport.ViewManager.ScrollPos;
                        transactionForm.ShowDialog();
                        LoadReport(this.ReportId, true);
                    }
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage("Unable to Drill to Project Opening Balance, " + err.Message);
            }
        }

        /// <summary>
        /// 14/03/2017
        /// When drill-down , we use existing general ledger report for drill ledger report (for particular ledger).
        /// 
        /// if user generate general ledger in another tab, it should not overlap drilled and general ledger
        /// </summary>
        /// <param name="rpt"></param>
        /// <returns></returns>
        private bool IsBaseReport(string rpt)
        {
            bool rtn = false;
            foreach (object item in stackActiveDrillDownReports)
            {
                EventDrillDownArgs eventdrilldownarg = item as EventDrillDownArgs;
                if (eventdrilldownarg.DrillDownRpt == rpt &&
                    eventdrilldownarg.DrillDownType == DrillDownType.BASE_REPORT)
                {
                    rtn = true;
                    break;
                }
            }
            return rtn;
        }

        private void ClearDrillDown()
        {
            stackActiveDrillDownReports.Clear();
            if (ReportProperty.Current.DrillDownProperties != null)
                ReportProperty.Current.DrillDownProperties.Clear();
        }

        private void AddDrillDown(EventDrillDownArgs eventDrillDown)
        {
            stackActiveDrillDownReports.Push(eventDrillDown);
            HideDrillDownButtons();
        }

        private EventDrillDownArgs GetRecentDrillDown()
        {
            EventDrillDownArgs RtneventDrill = null;
            if (stackActiveDrillDownReports.Count > 1)
            {
                stackActiveDrillDownReports.Pop();
                //MessageBox.Show("Reverting back Drill-Down Level ::" + sReportId);
                RtneventDrill = stackActiveDrillDownReports.Peek();
            }
            return RtneventDrill;
        }

        private void AttachDrillDownProperties(ReportBase reportDrillDown)
        {
            reportDrillDown.ReportDrillDown += new EventHandler<EventDrillDownArgs>(report_ReportDrillDown);
        }

        /// <summary>
        /// This method is used to hide Property, and Refresh buttons and make visible Drill-Down button only in drill down reports
        /// when it drill back to main reports hide dirldownn button and make visible Property, and Refresh button buttons 
        /// </summary>
        private void HideDrillDownButtons()
        {
            bbiDilldown.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiProperty.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            if (ReportProperty.Current.DrillDownProperties != null && ReportProperty.Current.DrillDownProperties.Count > 1)
            {
                if (stackActiveDrillDownReports.Count > 1)
                {
                    bbiDilldown.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    bbiProperty.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    bbiRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
            }
        }
        #endregion


        private void bbiDilldown_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DrillDownTarget(GetRecentDrillDown());
        }

        private void bbiRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadReport(this.ReportId, true);
            //activeReport.ExportToPdf(@"c:\Multi_CashBank.pdf");
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenProperty();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadReport(this.ReportId, true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ViewManager vm = pcReport.ViewManager;
            ViewControl vctrl = pcReport.ViewControl;
            Type type = pcReport.GetType();
            DevExpress.XtraEditors.VScrollBar vsb = type.InvokeMember("vScrollBar", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField, null, pcReport, new object[] { }) as DevExpress.XtraEditors.VScrollBar;
            int vbarvalue = vsb.Value;
            DevExpress.XtraEditors.VScrollBar vsb1 = type.InvokeMember("vScrollBar", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField, null, pcReport, new object[] { }) as DevExpress.XtraEditors.VScrollBar;
            vsb1.Value = vbarvalue;
            vm.OffsetVertScroll(1500);
            pcReport.UpdateScrollBars();
        }

        private void bbiCustomPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //On 19/09/2022
            if (activeReport.ReportId == "RPT-070")
            {
                ExportPayslipIntoFiles();
            }
            else
            {
                if (activeReport != null)
                {
                    int currentpage = pcReport.SelectedPage.Index;
                    frmPrinterSetup frmprintsetup = new frmPrinterSetup(this, activeReport, currentpage);
                    frmprintsetup.ShowDialog();
                }
            }
        }

        private void ChangeReportSettings(object sender)
        {
            try
            {
                PrintingSystemBase ps = sender as PrintingSystemBase;
                bool isLocationChanged = false;
                int nleftMargin = ps.PageMargins.Left;
                if (activeReport.ReportId == "RPT-069" || activeReport.ReportId == "RPT-071")
                {   //On 02/02/2023, For Pay Register or Pay Wages, allow to resize margin and allow to change paper 
                    if (ps.PageSettings.PaperKind == System.Drawing.Printing.PaperKind.Legal)
                        nleftMargin = 70;
                    else
                        nleftMargin = 15;
                }

                int newPageWidth =
                    ps.PageBounds.Width - nleftMargin - ps.PageMargins.Right;
                int currentPageWidth =
                    activeReport.PageWidth - activeReport.Margins.Left - activeReport.Margins.Right;
                int shift = currentPageWidth - newPageWidth;

                ResizeControls(activeReport, newPageWidth);
                isLocationChanged = true;
                if (isLocationChanged == true)
                {
                    //activeReport.Margins.Top = ps.PageMargins.Top;
                    //activeReport.Margins.Bottom = ps.PageMargins.Bottom;
                    //activeReport.Margins.Left = ps.PageMargins.Left;
                    //activeReport.Margins.Right = ps.PageMargins.Right;
                    activeReport.Margins = new System.Drawing.Printing.Margins(nleftMargin, ps.PageMargins.Right, ps.PageMargins.Top, ps.PageMargins.Bottom);
                    activeReport.PaperKind = ps.PageSettings.PaperKind;
                    activeReport.PaperName = ps.PageSettings.PaperName;
                    activeReport.Landscape = ps.PageSettings.Landscape;

                    //On 03/02/2023, Retain when we change page and margin changes and Paper setting---------
                    ReportProperty.Current.MarginLeft = activeReport.Margins.Left;
                    ReportProperty.Current.MarginRight = activeReport.Margins.Right;
                    ReportProperty.Current.MarginTop = activeReport.Margins.Top;
                    ReportProperty.Current.MarginBottom = activeReport.Margins.Bottom;
                    ReportProperty.Current.PaperKind = activeReport.PaperKind;
                    ReportProperty.Current.PaperLandscape = (activeReport.Landscape ? 1 : 0);
                    ReportProperty.Current.SaveReportSetting();

                    //---------------------------------------------------------------------------------------

                    activeReport.CreateDocument();
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage("Could not resize repot " + err.Message);
            }
        }

        /// <summary>
        /// Resize all controls based on Bands or DetailBand
        /// </summary>
        /// <param name="rpt"></param>
        /// <param name="pagewidh"></param>
        private void ResizeControls(ReportBase rpt, Int32 pagewidh)
        {
            foreach (Band _band in rpt.Bands)
            {
                ResizeControlsExtracted(pagewidh, _band.Controls, true);
            }

            //foreach (Band _band in rpt.Bands)
            //{
            //    ResizeControlsExtracted(pagewidh, _band.Controls, true);
            //}
        }

        /// <summary>
        /// Resize all control width in band control collection or detailband control collection
        /// </summary>
        /// <param name="pagewidh"></param>
        /// <param name="ctls"></param>
        private void ResizeControlsExtracted(Int32 pagewidh, XRControlCollection ctls, bool SubReport = false)
        {
            foreach (XRControl _control in ctls)
            {
                //MessageRender.ShowMessage(_control.GetType().Name); 
                //_control.Location = new Point((_control.Location.X - shift), _control.Location.Y); 
                if (_control.GetType() == typeof(XRTable) || _control.GetType() == typeof(XRLabel) || _control.GetType() == typeof(XRLine))
                {

                    _control.WidthF = pagewidh - 5;

                    //On 11/07/2022 -----------------------------------------------------------------------------------------------------------
                    if (_control.Name == "xrlblReportDate")
                    {
                        if (_control.Visible)
                        {
                            _control.WidthF = 100;
                            _control.LocationF = new PointF(pagewidh - (_control.WidthF + 10), _control.TopF);
                        }
                    }
                    else if (_control.Name == "xrlblProjectName")
                    {
                        if (_control.Visible)
                        {
                            //_control.LocationF = new PointF(100, pagewidh-200);
                            _control.WidthF = pagewidh - 250;
                            //_control.LocationF = new PointF(200, _control.TopF);
                        }
                    }
                    //---------------------------------------------------------------------------------------------------------------------------
                }
                else if (_control.GetType() == typeof(XRPivotGrid))
                {
                    XRPivotGrid pivt = (_control as XRPivotGrid);
                    pivt.WidthF = pagewidh - 50;

                    //Pivot grid only for multi Abstracts (Receipt, Payments and Both)
                    if (ReportId == "RPT-004" || ReportId == "RPT-005" || ReportId == "RPT-006")
                    {
                        //Fix ledger code and ledger name with
                        //pivt.Fields[reportSetting1.MonthlyAbstract.LEDGER_NAMEColumn.ColumnName].Width += 10;
                        //Int32 fixedColumnsWidth = pivt.Fields[reportSetting1.MonthlyAbstract.LEDGER_CODEColumn.ColumnName].Width
                        //                           + pivt.Fields[reportSetting1.MonthlyAbstract.LEDGER_NAMEColumn.ColumnName].Width;

                        //foreach (DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fld in pivt.Fields)
                        //{
                        //    if (fld.Visible && fld.FieldName != reportSetting1.MonthlyAbstract.LEDGER_CODEColumn.ColumnName)
                        //    {
                        //        fld.Width = fixedColumnsWidth / 13;
                        //    }
                        //}

                        //Int32 LedgerNameIndex = 0;
                        //foreach (DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fld in pivt.Fields)
                        //{
                        //    if (fld.Visible)
                        //    {
                        //        if (pivt.Fields[reportSetting1.MonthlyAbstract.LEDGER_NAMEColumn.ColumnName] != null &&
                        //            pivt.Fields[reportSetting1.MonthlyAbstract.LEDGER_CODEColumn.ColumnName] != null)
                        //        {
                        //            fixedtotalwidth = pivt.Fields[reportSetting1.MonthlyAbstract.LEDGER_NAMEColumn.ColumnName].Width
                        //                  + (ReportProperty.Current.ShowLedgerCode == 1 ? pivt.Fields[reportSetting1.MonthlyAbstract.LEDGER_CODEColumn.ColumnName].Width : 0);
                        //            LedgerNameIndex = pivt.Fields[reportSetting1.MonthlyAbstract.LEDGER_NAMEColumn.ColumnName].Index;

                        //        }
                        //        else
                        //        {
                        //            fixedtotalwidth = pivt.Fields[0].Width; //165;
                        //            LedgerNameIndex = 0;
                        //        }

                        //        if (fld.FieldName.ToUpper() != reportSetting1.MonthlyAbstract.LEDGER_CODEColumn.ColumnName &&
                        //               fld.FieldName.ToUpper() != reportSetting1.MonthlyAbstract.LEDGER_NAMEColumn.ColumnName &&
                        //               fld.FieldName.ToUpper() != reportSetting1.MonthlyAbstract.GROUP_CODEColumn.ColumnName &&
                        //               fld.FieldName.ToUpper() != reportSetting1.MonthlyAbstract.LEDGER_GROUPColumn.ColumnName)
                        //        {
                        //            fld.MinWidth = 5;
                        //            double realwdith = utilityMember.NumberSet.ToDouble((pagewidh - fixedtotalwidth).ToString()) / 13;
                        //            fld.Width = (Int32)realwdith;

                        //            remainingwidth = utilityMember.NumberSet.ToDouble("0." + Math.DivRem((pagewidh - fixedtotalwidth), 7, out extraWidth).ToString());
                        //            extraWidth = utilityMember.NumberSet.ToDouble((pagewidh - fixedtotalwidth).ToString()) % 14.0;
                        //        }
                        //    }
                        //}
                        //pivt.Fields[reportSetting1.MonthlyAbstract.LEDGER_NAMEColumn.ColumnName].Width = (Int32)Math.Truncate(remainingwidth * 8);
                    }
                }
                else if (_control.GetType() == typeof(XRSubreport) && SubReport) //for Receipts and Payments
                {
                    XRSubreport subreport = _control as XRSubreport;
                    ReportBase subreportsource = subreport.ReportSource as ReportBase;

                    if (ReportId == "RPT-027" && subreport.Name.ToUpper() != "XRSUBSIGNFOOTER" && subreport.Name.ToUpper() != "XRSUBFOREXSPLITDETAILS")
                    {
                        ResizeReceiptsPayments(pagewidh / 2, subreport);
                        ResizeControls(subreportsource, pagewidh / 2);
                    }
                    else if (ReportId == "RPT-062")
                    {
                        //Int32 subreportwidth = pagewidh;

                        ////XRTableCell ReceiptCell = this.activeReport.FindControl("xrCapReceipt", true) as XRTableCell;
                        ////if (ReceiptCell != null)
                        ////{
                        ////    subreport.LeftF = 2;
                        ////    //subreport.LeftF = ReceiptCell.LeftF;
                        ////}

                        ////XRTableCell BankCell = this.activeReport.FindControl("xrTableCell3", true) as XRTableCell;
                        ////if (BankCell != null)
                        ////{
                        ////    subreport.WidthF = pagewidh; // -BankCell.LeftF;
                        ////    subreportwidth = pagewidh;
                        ////}
                        //subreport.WidthF = pagewidh;
                        //ResizeControls(subreport.ReportSource as ReportBase, subreportwidth);
                    }
                    else if (subreport.Name.ToUpper() != "XRSUBFOREXSPLITDETAILS")
                    {
                        subreport.WidthF = pagewidh;
                        ResizeControls(subreport.ReportSource as ReportBase, pagewidh);
                    }
                }
                else if (_control.GetType() == typeof(DetailBand))
                {
                    ResizeControlsExtracted(pagewidh - 2, _control.Controls, true);
                }
                else if (_control.GetType() == typeof(DetailReportBand))
                {
                    ResizeControlsExtracted(pagewidh - 2, _control.Controls, true);
                }
            }
        }

        private void ResizeReceiptsPayments(Int32 subreportpagewidh, XRSubreport subreport)
        {
            subreportpagewidh = (subreportpagewidh) + 1;

            XRTable ExpenseCaptionTable = this.activeReport.FindControl("xrTblExpenseCaption", true) as XRTable;
            if (ExpenseCaptionTable != null)
            {
                ExpenseCaptionTable.LeftF = subreportpagewidh + 1;
                ExpenseCaptionTable.WidthF = subreportpagewidh - 2;
            }

            if ((subreport.Name == "xrSubPayments" || subreport.Name == "xrSubClosingBalance"))
            {
                if (subreport.Name == "xrSubClosingBalance")
                {
                    subreport.LeftF = subreportpagewidh + 1;
                    subreport.WidthF = subreportpagewidh - 2;
                    XRTable closingTable = this.activeReport.FindControl("xtTblClosingBalance", true) as XRTable;
                    if (closingTable != null)
                    {
                        closingTable.LeftF = subreportpagewidh + 1;
                        closingTable.WidthF = subreportpagewidh - 7;
                    }
                }
                else
                {
                    subreport.WidthF = subreportpagewidh + 1;
                    subreport.LeftF = subreportpagewidh - 4;

                    //Vertical line
                    XRCrossBandLine crossbox = this.activeReport.CrossBandControls["xrCrossBandLine1"] as XRCrossBandLine;
                    if (crossbox != null)
                    {
                        crossbox.StartPointF = new PointF((subreport.LeftF + subreport.WidthF) - 2, crossbox.StartPointF.Y);
                        crossbox.EndPointF = new PointF((subreport.LeftF + subreport.WidthF) - 2, crossbox.EndPointF.Y);
                    }
                }
            }
            else if ((subreport.Name == "xrSubReceipts" || subreport.Name == "xrSubOpeningBalance"))
            {
                subreport.WidthF = subreportpagewidh; // -4;
                XRTable openingTable = this.activeReport.FindControl("xrTblOpeningBalance", true) as XRTable;
                if (openingTable != null)
                {
                    openingTable.WidthF = subreportpagewidh - 2;
                }

                XRTable IncomeCaptionTable = this.activeReport.FindControl("xrTblIncomeCaption", true) as XRTable;
                if (IncomeCaptionTable != null)
                {
                    IncomeCaptionTable.WidthF = subreportpagewidh - 2;
                }

                if (subreport.Name == "xrSubOpeningBalance")
                {
                    //XRTable openingTable1 = this.activeReport.FindControl("xrTblOpeningBalance", true) as XRTable;
                    //openingTable1.WidthF = subreportpagewidh + 2;
                }

                /*if (subreport.Name == "xrSubOpeningBalance")
                {
                    XRTable openingTable = this.activeReport.FindControl("xrTblOpeningBalance", true) as XRTable;
                    if (openingTable != null)
                    {
                        openingTable.WidthF = subreportpagewidh-2 ;
                    }
                    //subreport.WidthF = 400; // subreportpagewidh + 6;
                }
                else
                {
                    subreport.WidthF = subreportpagewidh;
                }*/
            }
        }

        /// <summary>
        /// On 19/09/2022, For SDBINB, to export indiviual staff payslips into separate pdf files, 
        /// this will be use to send as mail or take print out
        /// </summary>
        private void ExportPayslipIntoFiles()
        {
            int pidex = 0;
            string folderpath = string.Empty;
            bool PayslipExported = false;
            string staffcode = string.Empty;
            string pwd = string.Empty;
            XtraReport rptExport = new XtraReport();

            try
            {
                FolderBrowserDialog folderPath = new FolderBrowserDialog();
                folderPath.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
                if (folderPath.ShowDialog() == DialogResult.OK)
                {
                    folderpath = folderPath.SelectedPath;
                }

                if (!string.IsNullOrEmpty(folderpath))
                {
                    if ((MessageRender.ShowConfirmationMessage("Are you sure to export all the Payslips into different pdf files" +
                                         " in the selected path \"" + folderpath + "\" ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == DialogResult.Yes)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        foreach (Page p in activeReport.Pages)
                        {
                            //Get Staff Code
                            staffcode = string.Empty;
                            pwd = string.Empty;
                            /*NestedBrickIterator iterator = new NestedBrickIterator(p.InnerBricks);
                            while (iterator.MoveNext())
                            {
                                LabelBrick labelBrick = iterator.CurrentBrick as LabelBrick;
                                if (labelBrick != null && labelBrick.Value != null)
                                {
                                    staffcode = labelBrick.Value.ToString();
                                    if (!string.IsNullOrEmpty(staffcode))
                                    {
                                        break;
                                    }
                                }
                            };*/

                            if (p.Watermark != null && p.Watermark.Text != null)
                            {
                                staffcode = p.Watermark.Text;
                                if (staffcode.IndexOf("_") > 0)
                                {
                                    pwd = staffcode.Substring(0, staffcode.IndexOf("_"));
                                }
                            }

                            if (!string.IsNullOrEmpty(staffcode))
                            {
                                rptExport.Pages.Add(p);
                                string path = Path.Combine(folderpath, staffcode + "_Payslip_" + UtilityMember.DateSet.ToDate(ReportProperty.Current.PayrollPayrollDate, false).ToString("MMMM yyyy") + ".pdf");
                                if (!string.IsNullOrEmpty(pwd))
                                {
                                    rptExport.ExportOptions.Pdf.PasswordSecurityOptions.OpenPassword = pwd;
                                    rptExport.ExportOptions.Pdf.PasswordSecurityOptions.PermissionsPassword = pwd;
                                }
                                rptExport.ExportToPdf(path);
                                rptExport.Pages.Clear();
                                PayslipExported = true;
                                pidex++;
                            }
                        }

                        this.Cursor = Cursors.Default;
                        if (PayslipExported)
                        {
                            activeReport.CreateDocument();
                            MessageRender.ShowMessage(pidex.ToString() + " Payslip(s) have been exported in the selected \"" + folderpath + "\"");
                        }
                    }
                }
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default;
                MessageRender.ShowMessage(err.Message);
            }
        }

        /// <summary>
        /// On 21/09/2022, to handle custom print button will be used for Payslip export to pdf files
        /// </summary>
        private void HandleReportViewerButtons()
        {
            //1. HandleCustomPrintButton -----------------------------------------------------------------------------------
            string tooltipmessage = "Custom Print";
            this.bbiCustomPrint.Glyph = global::Bosco.Report.Properties.Resources.print1;
            this.bbiCustomPrint.LargeGlyph = global::Bosco.Report.Properties.Resources.print1;
            bbiCustomPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

            if (this.ReportId == "RPT-070")
            {
                //On 19/09/2022, for payslip, hide custom print, if export separate pages--------------
                bbiCustomPrint.Visibility = (ReportProperty.Current.PayrollPayslipInSeparatePages == 1 ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never);
                //-------------------------------------------------------------------------------------
                tooltipmessage = "Export all the Payslips into different pdf files";

                this.bbiCustomPrint.Glyph = global::Bosco.Report.Properties.Resources.Payslip;
                this.bbiCustomPrint.LargeGlyph = global::Bosco.Report.Properties.Resources.Payslip;
            }

            if (bbiCustomPrint.Visibility == DevExpress.XtraBars.BarItemVisibility.Always)
            {
                //On 19/09/2022, to set tooltip for custom button
                SuperToolTip sTooltip1 = new SuperToolTip();
                ToolTipTitleItem titleItem1 = new ToolTipTitleItem();
                titleItem1.Text = tooltipmessage;
                sTooltip1.Items.Add(titleItem1);
                bbiCustomPrint.SuperTip = sTooltip1;
            }
            //----------------------------------------------------------------------------------------------------------------

            //2. handle Export excel calculation buttons
            bbiExcelCalculation.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (this.ReportId == "RPT-027" ||
                this.ReportId == "RPT-001" || this.ReportId == "RPT-002" || this.ReportId == "RPT-003" ||
                this.ReportId == "RPT-004" || this.ReportId == "RPT-005" || this.ReportId == "RPT-006" ||
                this.ReportId == "RPT-028" || this.ReportId == "RPT-047" || this.ReportId == "RPT-030" ||
                this.ReportId == "RPT-016" || this.ReportId == "RPT-017" || this.ReportId == "RPT-011" ||
                this.ReportId == "RPT-012" || this.ReportId == "RPT-052" || this.ReportId == "RPT-191" ||
                this.ReportId == "RPT-214" || this.ReportId == "RPT-215" || this.ReportId == "RPT-216" || this.ReportId == "RPT-208" ||
                this.ReportId == "RPT-194" || this.ReportId == "RPT-195" || this.ReportId == "RPT-196" || this.ReportId == "RPT-197" ||
                this.ReportId == "RPT-199" ||
                this.ReportId == "RPT-173" || this.ReportId == "RPT-173")
            {
                bbiExcelCalculation.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                bbiExcelCalculation.Enabled = true;
            }

            //3. Handle Uplod Report to Acme.erp portal
            bbiUploadPortal.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (this.ReportId == "RPT-191")
            {
                bbiUploadPortal.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                bbiUploadPortal.Enabled = true;
            }


            /*//On 15/11/2022, Disable custom print button, if there are any only one page ---------------
            this.bbiCustomPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (this.activeReport != null && this.activeReport.Pages.Count > 1)
            {
                this.bbiCustomPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }*/
            //------------------------------------------------------------------------------------------
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ReportBase rptODDEVEN = new ReportBase();
            rptODDEVEN.CreateDocument();
            rptODDEVEN.PrintingSystem.ContinuousPageNumbering = false;

            foreach (Page p in activeReport.Pages)
            {
                if (((p.Index + 1) % 2) == 0)
                {
                    rptODDEVEN.Pages.Add(p);
                }
            }


        }

        private void bbiExcelCalculation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (this.activeReport != null)
                {
                    FolderBrowserDialog folderPath = new FolderBrowserDialog();
                    folderPath.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;

                    string folderpath = string.Empty;
                    if (folderPath.ShowDialog() == DialogResult.OK)
                    {
                        folderpath = folderPath.SelectedPath;
                    }

                    if (!string.IsNullOrEmpty(folderpath))
                    {
                        this.Cursor = Cursors.WaitCursor;
                        folderpath = Path.Combine(folderpath, this.activeReport.DisplayName);
                        folderpath = folderpath + ".csv";
                        using (MemoryStream ms = new MemoryStream())
                        {
                            this.activeReport.ExportOptions.Csv.TextExportMode = TextExportMode.Text;
                            this.activeReport.ExportOptions.Csv.QuoteStringsWithSeparators = true;
                            this.activeReport.ExportOptions.Csv.Encoding = Encoding.UTF8;
                            this.activeReport.ExportOptions.Csv.Separator = CultureInfo.CurrentCulture.TextInfo.ListSeparator.ToString();

                            this.activeReport.ExportToCsv(ms);
                            ms.Position = 0;
                            var sr = new StreamReader(ms);
                            var myStr = sr.ReadToEnd();
                            FileStream file = new FileStream(folderpath, FileMode.Create, FileAccess.Write);
                            ms.WriteTo(file);
                            file.Close();
                            ms.Close();
                        }

                        if (MessageRender.ShowConfirmationMessage("Are you sure to open exported Excel file ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (File.Exists(folderpath))
                            {
                                Process.Start(folderpath);
                            }
                            else
                            {
                                MessageRender.ShowMessage("Exported excel '" + folderpath + "' is not found");
                            }
                        }
                        this.Cursor = Cursors.Default;
                    }
                }
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default;
                MessageRender.ShowMessage(err);
            }
        }

        private void bbiUploadPortal_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.activeReport != null && activeReport.Pages.Count > 0)
            {
                frmUploadReport frmupload = new frmUploadReport(this.activeReport, ReportProperty.Current.ReportTitle,
                        ReportProperty.Current.DateFrom, ReportProperty.Current.DateTo);
                frmupload.ShowDialog();
            }
            else
            {
                MessageRender.ShowMessage("Report is not yet generated");
            }

        }

        private void printPreviewBarItem24_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void pcReport_Load(object sender, EventArgs e)
        {

        }

        private void bbiCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (this.activeReport != null && activeReport.Pages.Count > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        this.activeReport.ExportToText(ms);
                        ms.Position = 0;
                        var sr = new StreamReader(ms);
                        var myStr = sr.ReadToEnd();

                        Clipboard.SetText(myStr);
                    }
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    MessageRender.ShowMessage("Report is not yet generated");
                }
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default;
                MessageRender.ShowMessage(err.Message);
            }
        }
    }
}


//private void ChangeReportSettings(object sender)
//        {
//            PrintingSystemBase ps = sender as PrintingSystemBase;
//            bool isLocationChanged = false;
//            int newPageWidth =
//                ps.PageBounds.Width - ps.PageMargins.Left - ps.PageMargins.Right;
//            int currentPageWidth =
//                activeReport.PageWidth - activeReport.Margins.Left - activeReport.Margins.Right;
//            int shift = currentPageWidth - newPageWidth;
//            foreach (Band _band in activeReport.Bands)
//            {
//                foreach (XRControl _control in _band.Controls)
//                {
//                    isLocationChanged = true;
//                    //_control.Location = new Point((_control.Location.X - shift), _control.Location.Y);
//                    if (_control.GetType() == typeof(XRTable) || _control.GetType() == typeof(XRLabel))
//                    {
//                        _control.WidthF = newPageWidth - 5;
//                    }
//                }
//            }
//            if (isLocationChanged == true)
//            {
//                activeReport.Margins.Top = ps.PageMargins.Top;
//                activeReport.Margins.Bottom = ps.PageMargins.Bottom;
//                activeReport.Margins.Left = ps.PageMargins.Left;
//                activeReport.Margins.Right = ps.PageMargins.Right;
//                activeReport.PaperKind = ps.PageSettings.PaperKind;
//                activeReport.PaperName = ps.PageSettings.PaperName;
//                activeReport.Landscape = ps.PageSettings.Landscape;
//                activeReport.CreateDocument();
//            }
//        }