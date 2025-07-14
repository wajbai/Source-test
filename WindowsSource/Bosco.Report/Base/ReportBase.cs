/*  Class Name      : ReportBase.cs
 *  Purpose         : Base Report Object
 *  Author          : CS
 *  Created on      : 12-Nov-2013
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using Bosco.Utility;
using Bosco.Utility.ConfigSetting;
using Bosco.Report;
using Bosco.Report.View;
using Bosco.DAO.Data;
using Bosco.Report.SQL;
using DevExpress.XtraReports.UI;
using System.Data;
//using DevExpress.XtraRichEdit.API.Word;
using System.Collections;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraPrinting;
using AcMEDSync.Model;
using Bosco.DAO.Schema;
using DevExpress.XtraPrinting.Native;

namespace Bosco.Report.Base
{
    public class ReportBase : DevExpress.XtraReports.UI.XtraReport
    {
        public AppSchemaSet.ApplicationSchemaSet appSchema = new AppSchemaSet.ApplicationSchemaSet();
        public SettingProperty settingProperty = new SettingProperty();
        private CommonMember utilityMember = null;
        private ReportSQL reportSQL = null;
        private ReportSetting.ReportParameterDataTable reportParameters = null;
        private ReportSetting.LedgerDataTable reportLedgers = null;
        private ReportBankSQL reportBankSQL = null;
        private ReportFinalAccounts reportFinalAccounts = null;
        private ReportTDS reporttds = null;
        private Stock reportStock = null;
        private ReportCostCenter reportCostCentre = null;
        private ReportCashBankVoucher reportCashBankVoucher = null;
        private ReportForeginContribution reportForeginContribution = null;
        private ReportBudgetVariance reportBudget = null;
        private ReportAssetSQL reportAssetSQL = null;
        private ReportNetworkingSQL reportNetWorkingSQL = null;
        private ReportAuditLog reportAuditLogSQL = null;
        private GeneralateReports generalateReports = null;

        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.DetailBand detailBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
        private string reportId = "";
        public bool FromViewScreen = false;
        private XRControlStyle styleInstitute;
        private XRControlStyle styleReportTitle;
        private XRControlStyle styleReportSubTitle;
        private XRControlStyle styleDateInfo;
        private XRControlStyle stylePageInfo;
        private XRControlStyle styleEvenRow;
        private XRControlStyle styleGroupRow;
        private XRControlStyle styleRow;
        private XRControlStyle styleColumnHeader;
        private XRControlStyle styleTotalRow;
        public event EventHandler<EventDrillDownArgs> ReportDrillDown;

        private bool isDrillDownMode = false;
        private XRControlStyle styleRowSmall;
        private XRControlStyle styleTotalRowSmall;
        private XRControlStyle styleColumnHeaderSmall;



        /// <summary>
        /// Adding collection of Keys and Values of Tables and Cells.
        /// </summary>
        private Dictionary<XRTable, XRTableCell> dicSupressLedgerCode = new Dictionary<XRTable, XRTableCell>();

        //On 09/08/2022 for TOC content (will be updated concern report for exp Ledger ----------------------
        public DataTable dtTOCList = new DataTable();
        public int NoOfTOCPages = 0;
        //----------------------------------------------------------------------------------------------------

        private DataTable dtCCOpeningBalance;

        /// <summary>
        /// Report Values for Conditionals.
        /// </summary>
        public enum ReportValue
        {
            Remove = 0,
            Add = 1
        }

        public enum TransType
        {
            RC,
            PY,
            IC,
            EP,
            CRC,
            CPY,
            CINC,
            CEXP,
            JN

        }
        public enum TransMode
        {
            CR,
            DR
        }

        public enum BankType
        {
            Cleared,
            Uncleared,
            Realized,
            Unrealized
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportBase));
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.detailBand1 = new DevExpress.XtraReports.UI.DetailBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.styleInstitute = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleReportTitle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleReportSubTitle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleDateInfo = new DevExpress.XtraReports.UI.XRControlStyle();
            this.stylePageInfo = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleEvenRow = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleGroupRow = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleRow = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleColumnHeader = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleTotalRow = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleRowSmall = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleTotalRowSmall = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleColumnHeaderSmall = new DevExpress.XtraReports.UI.XRControlStyle();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // topMarginBand1
            // 
            resources.ApplyResources(this.topMarginBand1, "topMarginBand1");
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // detailBand1
            // 
            resources.ApplyResources(this.detailBand1, "detailBand1");
            this.detailBand1.Name = "detailBand1";
            // 
            // bottomMarginBand1
            // 
            resources.ApplyResources(this.bottomMarginBand1, "bottomMarginBand1");
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // styleInstitute
            // 
            this.styleInstitute.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleInstitute.Name = "styleInstitute";
            this.styleInstitute.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.styleInstitute.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // styleReportTitle
            // 
            this.styleReportTitle.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleReportTitle.Name = "styleReportTitle";
            this.styleReportTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.styleReportTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // styleReportSubTitle
            // 
            this.styleReportSubTitle.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleReportSubTitle.Name = "styleReportSubTitle";
            this.styleReportSubTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.styleReportSubTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // styleDateInfo
            // 
            this.styleDateInfo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleDateInfo.Name = "styleDateInfo";
            this.styleDateInfo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.styleDateInfo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // stylePageInfo
            // 
            this.stylePageInfo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stylePageInfo.Name = "stylePageInfo";
            this.stylePageInfo.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.stylePageInfo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // styleEvenRow
            // 
            this.styleEvenRow.BackColor = System.Drawing.Color.White;
            this.styleEvenRow.BorderColor = System.Drawing.Color.Silver;
            this.styleEvenRow.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleEvenRow.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleEvenRow.BorderWidth = 1F;
            this.styleEvenRow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleEvenRow.Name = "styleEvenRow";
            this.styleEvenRow.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            // 
            // styleGroupRow
            // 
            this.styleGroupRow.BackColor = System.Drawing.Color.Linen;
            this.styleGroupRow.BorderColor = System.Drawing.Color.Silver;
            this.styleGroupRow.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleGroupRow.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.styleGroupRow.BorderWidth = 1F;
            this.styleGroupRow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleGroupRow.Name = "styleGroupRow";
            this.styleGroupRow.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            // 
            // styleRow
            // 
            this.styleRow.BorderColor = System.Drawing.Color.Gainsboro;
            this.styleRow.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleRow.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleRow.BorderWidth = 1F;
            this.styleRow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleRow.Name = "styleRow";
            this.styleRow.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.styleRow.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // styleColumnHeader
            // 
            this.styleColumnHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.styleColumnHeader.BorderColor = System.Drawing.Color.DarkGray;
            this.styleColumnHeader.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleColumnHeader.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleColumnHeader.BorderWidth = 1F;
            this.styleColumnHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleColumnHeader.Name = "styleColumnHeader";
            this.styleColumnHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            // 
            // styleTotalRow
            // 
            this.styleTotalRow.BorderColor = System.Drawing.Color.Gainsboro;
            this.styleTotalRow.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleTotalRow.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleTotalRow.BorderWidth = 1F;
            this.styleTotalRow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleTotalRow.Name = "styleTotalRow";
            this.styleTotalRow.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            // 
            // styleRowSmall
            // 
            this.styleRowSmall.BackColor = System.Drawing.Color.Empty;
            this.styleRowSmall.BorderColor = System.Drawing.Color.Gainsboro;
            this.styleRowSmall.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleRowSmall.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleRowSmall.BorderWidth = 1F;
            this.styleRowSmall.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleRowSmall.Name = "styleRowSmall";
            this.styleRowSmall.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.styleRowSmall.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // styleTotalRowSmall
            // 
            this.styleTotalRowSmall.BorderColor = System.Drawing.Color.Gainsboro;
            this.styleTotalRowSmall.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleTotalRowSmall.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleTotalRowSmall.BorderWidth = 1F;
            this.styleTotalRowSmall.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleTotalRowSmall.Name = "styleTotalRowSmall";
            this.styleTotalRowSmall.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            // 
            // styleColumnHeaderSmall
            // 
            this.styleColumnHeaderSmall.BackColor = System.Drawing.Color.Gainsboro;
            this.styleColumnHeaderSmall.BorderColor = System.Drawing.Color.DarkGray;
            this.styleColumnHeaderSmall.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleColumnHeaderSmall.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleColumnHeaderSmall.BorderWidth = 1F;
            this.styleColumnHeaderSmall.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleColumnHeaderSmall.Name = "styleColumnHeaderSmall";
            this.styleColumnHeaderSmall.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            // 
            // ReportBase
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.topMarginBand1,
            this.detailBand1,
            this.bottomMarginBand1});
            resources.ApplyResources(this, "$this");
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.styleInstitute,
            this.styleReportTitle,
            this.styleReportSubTitle,
            this.styleDateInfo,
            this.stylePageInfo,
            this.styleEvenRow,
            this.styleGroupRow,
            this.styleRow,
            this.styleColumnHeader,
            this.styleTotalRow,
            this.styleRowSmall,
            this.styleTotalRowSmall,
            this.styleColumnHeaderSmall});
            this.Version = "13.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        public ReportBase()
        {
            dtTOCList = new ReportSetting.TOCDataTable();
        }

        public bool IsDrillDownMode
        {
            //14/03/2017
            /// When drill-down , we use existing general ledger report for drill ledger report (for particular ledger).
            /// if user generate general ledger in another tab, it should not overlap drilled and general ledger
            /// 
            get;
            set;
            //get
            //{
            //    return (this.ReportProperties.DrillDownProperties != null &&
            //           this.ReportProperties.DrillDownProperties.Count > 1);
            //}
        }

        protected CommonMember UtilityMember
        {
            get
            {
                if (utilityMember == null) { utilityMember = new CommonMember(); }
                return utilityMember;
            }

        }

        protected ReportProperty ReportProperties
        {
            get
            {
                return ReportProperty.Current;
            }
        }

        protected ReportSetting.ReportParameterDataTable ReportParameters
        {
            get
            {
                if (reportParameters == null) { reportParameters = new ReportSetting.ReportParameterDataTable(); }
                return reportParameters;
            }
        }

        protected ReportSetting.LedgerDataTable LedgerParameters
        {
            get
            {
                if (reportLedgers == null) { reportLedgers = new ReportSetting.LedgerDataTable(); }
                return reportLedgers;
            }
        }

        protected string GetReportSQL(ReportSQLCommand.Report queryId)
        {
            if (reportSQL == null) { reportSQL = new ReportSQL(); }
            string sql = reportSQL.GetReportSQL(queryId);
            return sql;
        }

        protected string GetBankReportSQL(ReportSQLCommand.BankReport queryId)
        {
            if (reportBankSQL == null) { reportBankSQL = new ReportBankSQL(); }
            string sql = reportBankSQL.GetReportSQL(queryId);
            return sql;
        }

        protected string GetFinalAccountsReportSQL(ReportSQLCommand.FinalAccounts queryId)
        {
            if (reportFinalAccounts == null) { reportFinalAccounts = new ReportFinalAccounts(); }
            string sql = reportFinalAccounts.GetReportSQL(queryId);
            return sql;
        }

        protected string GetReportCostCentre(ReportSQLCommand.CostCentre queryId)
        {
            if (reportCostCentre == null) { reportCostCentre = new ReportCostCenter(); }
            string sql = reportCostCentre.GetCostCenterSQL(queryId);
            return sql;
        }

        protected string GetReportCashBankVoucher(ReportSQLCommand.CashBankVoucher queryId)
        {
            if (reportCashBankVoucher == null) { reportCashBankVoucher = new ReportCashBankVoucher(); }
            string sql = reportCashBankVoucher.GetCashBankSQL(queryId);
            return sql;
        }

        protected string GetReportForeginContribution(ReportSQLCommand.ForeginContribution queryId)
        {
            if (reportForeginContribution == null) { reportForeginContribution = new ReportForeginContribution(); }
            string sql = reportForeginContribution.GetReportSQL(queryId);
            return sql;
        }

        protected string GetBudgetvariance(ReportSQLCommand.BudgetVariance queryId)
        {
            if (reportBudget == null) { reportBudget = new ReportBudgetVariance(); }
            string sql = reportBudget.GetReportSQL(queryId);
            return sql;
        }
        protected string GetAssetReports(ReportSQLCommand.Asset queryId)
        {
            if (reportAssetSQL == null) { reportAssetSQL = new ReportAssetSQL(); }
            string sql = reportAssetSQL.GetAssetReportSQL(queryId);
            return sql;
        }
        protected string GetNetWorkingReports(ReportSQLCommand.NetWorking queryId)
        {
            if (reportNetWorkingSQL == null) { reportNetWorkingSQL = new ReportNetworkingSQL(); }
            string sql = reportNetWorkingSQL.GetNetWorkingReportSQL(queryId);
            return sql;
        }
        protected string GetReportTDS(ReportSQLCommand.TDS queryId)
        {
            if (reporttds == null) { reporttds = new ReportTDS(); }
            string sql = reporttds.GetReportSQL(queryId);
            return sql;
        }

        protected string GetReportTDS(ReportSQLCommand.Stock queryId)
        {
            if (reportStock == null) { reportStock = new Stock(); }
            string sql = reportStock.GetStockSQL(queryId);
            return sql;
        }

        protected string GetAuditReportSQL(ReportSQLCommand.AuditReports queryId)
        {
            if (reportAuditLogSQL == null) { reportAuditLogSQL = new ReportAuditLog(); }
            string sql = reportAuditLogSQL.GetAuditLogSQL(queryId);
            return sql;
        }

        protected string GetGeneralateReportSQL(ReportSQLCommand.GeneralateReports queryId)
        {
            if (generalateReports == null) { generalateReports = new GeneralateReports(); }
            string sql = generalateReports.GetGeneralateSQL(queryId);
            return sql;
        }



        public string ReportId
        {
            get { return reportId; }
            set { this.reportId = value; }
        }

        public DialogResult dialogResult = DialogResult.Cancel;
        public void ShowReportFilterDialog()
        {
            frmReportFilter reportFilterDialog = new frmReportFilter();
            //reportFilterDialog.ReportFilterApplied += new EventHandler(reportFilterDialog_ReportFilterApplied);
            //19/06/2020
            //DialogResult dialogResult = reportFilterDialog.ShowDialog();
            dialogResult = reportFilterDialog.ShowDialog();
            reportFilterDialog.Close();
            reportFilterDialog.Dispose();

            if (dialogResult == DialogResult.OK)
            {
                ReportProperty.Current.ReportFlag = 0;
                this.ShowReport();

            }
        }

        public void ShowFiancialReportFilterDialog()
        {
            frmFinancialRecords financialDialog = new frmFinancialRecords();
            DialogResult dialogResult = financialDialog.ShowDialog();
            financialDialog.Close();
            financialDialog.Dispose();
            if (dialogResult == DialogResult.OK)
            {
                this.ShowReport();
            }
        }

        public void ShowBankVoucher()
        {
            frmBankChequeVouchers bankvouchers = new frmBankChequeVouchers();
            DialogResult dialogResult = bankvouchers.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                this.ShowReport();
            }
        }

        public void ShowTDSChallanReconciliationForm()
        {
            frmTDSChallan frmChallan = new frmTDSChallan();
            DialogResult dialogResult = frmChallan.ShowDialog();
            frmChallan.Close();
            frmChallan.Dispose();
            if (dialogResult.Equals(DialogResult.OK))
            {
                this.ShowReport();
            }
        }

        public void ShowPayslipForm()
        {
            frmPaySlipViewer frmPayslip = new frmPaySlipViewer();
            DialogResult dialogResult = frmPayslip.ShowDialog();
            frmPayslip.Close();
            frmPayslip.Dispose();
            if (dialogResult.Equals(DialogResult.OK))
            {
                this.ShowReport();
            }
        }


        //void reportFilterDialog_ReportFilterApplied(object sender, EventArgs e)
        //{
        //dicSupressLedgerCode.Clear();
        //To dock left,bottom  
        //frmReportGallery frmGallery = new frmReportGallery();
        //frmGallery.CollapseDockManager();

        //this.ShowReport();
        //}

        public virtual void ShowReport()
        {
            //On 25/04/2017, when we show standard repot from all gird forms (view forms), 
            //reports form is getting minimized when other application is already opened.
            //so remove wait process form only (this isstandard report)
            bool isStandardRpt = (this.reportId == "RPT-STD" || this.reportId == "RPT-152" || FromViewScreen); //this.reportId == "RPT-163" || this.reportId == "RPT-180"
            if (isStandardRpt)
            {
                this.CreateDocument(true);
            }
            else
            {
                SplashScreenManager.ShowForm(typeof(frmReportWait));
                //this.PrintingSystem.AddCommandHandler(new ReportCommandHandler(this));
                this.CreateDocument(true);
                SplashScreenManager.CloseForm();
            }
        }

        public virtual void ShowPrintDialogue()
        {

        }

        protected string GetLiquidityGroupIds()
        {
            string groupIds = (int)FixedLedgerGroup.BankAccounts + "," +
                              (int)FixedLedgerGroup.Cash + "," +
                              (int)FixedLedgerGroup.FixedDeposit;
            return groupIds;
        }

        protected string GetLiquidityGroupIdsWithoutFD()
        {
            string groupIds = (int)FixedLedgerGroup.BankAccounts + "," +
                              (int)FixedLedgerGroup.Cash;
            return groupIds;
        }

        protected string GetProgressiveDate(string dateValue)
        {
            string prgDate = dateValue;
            string sqlAcYear = this.GetReportSQL(SQL.ReportSQLCommand.Report.AccountYear);

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                ResultArgs resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlAcYear);

                DataTable dtAcYear = resultArgs.DataSource.Table;

                if (dtAcYear != null && dtAcYear.Rows.Count > 0)
                {
                    prgDate = dtAcYear.Rows[0][this.ReportParameters.YEAR_FROMColumn.ColumnName].ToString();
                    prgDate = DateTime.Parse(prgDate).ToShortDateString();
                }
            }

            return prgDate;

            /*string fyBeginDate = SettingProperty.Current.YearFrom;
            int yearDiff = 0;

            DateTime dateFyBegin = DateTime.Parse(fyBeginDate);
            DateTime dateDateValue = DateTime.Parse(dateValue);
            //DateTime datePrgDate = DateTime.ParseExact(

            yearDiff = (dateDateValue.Year - dateFyBegin.Year);

            if (dateDateValue.Month < dateFyBegin.Month)
            {
                yearDiff--;
            }

            prgDate = dateFyBegin.AddYears(yearDiff).ToShortDateString();
            */
        }

        /// <summary>
        /// Add or Remove the Ledger code based on the XRTable and XRTableCell
        /// </summary>
        /// <param name="xrTable"></param>
        /// <param name="xrTblCell"></param>
        public void AddSuppresLedgerCode(XRTable xrTable, XRTableCell xrTblCell)
        {

            if (ReportProperty.Current.ShowLedgerCode == 0 && xrTable.Rows.FirstRow.Cells.IndexOf(xrTblCell) >= 0)
            {
                xrTable.Rows.FirstRow.Cells.Remove(xrTblCell);
            }
            else if (ReportProperty.Current.ShowLedgerCode == 1 && xrTable.Rows.FirstRow.Cells.IndexOf(xrTblCell) <= 0)
            {
                xrTable.Rows.FirstRow.InsertCell(xrTblCell, 0);
                xrTblCell.WidthF = ReportProperty.Current.NumberSet.ToInteger(ReportProperty.Current.SetWithForCode.ToString());
            }
            dicSupressLedgerCode.Add(xrTable, xrTblCell);
        }

        public bool AddHorizontalLine()
        {
            return ReportProperty.Current.ShowHorizontalLine != 0 ? true : false;
        }

        public bool AddNarration()
        {
            return ReportProperty.Current.IncludeNarration != 0 ? true : false;
        }

        public bool AddVerticalLine()
        {
            return ReportProperty.Current.ShowVerticalLine != 0 ? true : false;
        }

        public bool AddReportTitles()
        {
            return ReportProperty.Current.ShowTitles != 0 ? true : false;
        }
        //To allign ordinary tables
        public XRTable SetBorders(XRTable table, int HorizontalLine, int VerticalLine)
        {

            int j = table.Rows.Count;
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (HorizontalLine == 1 && VerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        else if (count == 3 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (HorizontalLine == 1)
                    {
                        if (count == 3 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (VerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        else if (count == 3 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    // tcell.BorderColor = ((int)BorderStyleCell.Regular==0)? System.Drawing.Color.Black :System.Drawing.Color.Black;
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
            return table;
        }
        // To allign heading tables
        public XRTable SetHeadingTableBorder(XRTable table, int HorizontalLine, int VerticalLine)
        {

            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (HorizontalLine == 1 && VerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.All;
                        else if (count == 3 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (HorizontalLine == 1)
                    {
                        if (count == 3 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (VerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        else if (count == 3 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    //tcell.BorderColor = ((int)BorderStyleCell.Regular == 0) ? System.Drawing.Color.Black : System.Drawing.Color.Black;
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                }
            }
            return table;
        }
        // To allign table which inclues narration
        public XRTable AlignTable(XRTable table, string bankNarration, string cashNarration, int count)
        {
            int rowcount = 0;

            foreach (XRTableRow row in table.Rows)
            {
                int cellcount = 0;
                ++rowcount;
                if (rowcount == 2 && ReportProperties.IncludeNarration != 1)
                {
                    row.Visible = false;
                }
                else if (bankNarration == string.Empty && cashNarration == string.Empty && ReportProperties.IncludeNarration == 1 && rowcount == 2)
                {
                    row.Visible = false;
                }
                else
                {
                    row.Visible = true;
                }
                foreach (XRTableCell cell in row)
                {
                    ++cellcount;
                    if (ReportProperties.IncludeNarration != 1 && rowcount == 1)
                    {
                        if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                        {
                            if (count == 1)
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                            else if (cellcount == 1)
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                            else if (cellcount == 3 && ReportProperties.ShowLedgerCode != 1)
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                            if (cellcount == 6)
                                cellcount = 1;
                        }
                        else if (ReportProperties.ShowHorizontalLine == 1)
                        {
                            if (count == 1)
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                            else if (cellcount == 3 && ReportProperties.ShowLedgerCode != 1)
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                            if (cellcount == 6)
                                cellcount = 1;
                        }
                        else if (ReportProperties.ShowVerticalLine == 1)
                        {
                            if (cellcount == 1)
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                            else if (cellcount == 3 && ReportProperties.ShowLedgerCode != 1)
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                            if (cellcount == 6)
                                cellcount = 1;
                        }
                        else
                        {
                            cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        }

                    }
                    else
                    {
                        if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                        {
                            if (rowcount == 1)
                            {
                                if (count == 1)
                                {
                                    if (bankNarration != string.Empty || cashNarration != string.Empty)
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top;
                                    else
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                                }
                                else if (cellcount == 1)
                                {
                                    if (bankNarration != string.Empty || cashNarration != string.Empty)
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                                    else
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                                }
                                else if (cellcount == 3 && ReportProperties.ShowLedgerCode != 1)
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                }
                                else
                                {
                                    if (bankNarration != string.Empty || cashNarration != string.Empty)
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                                    else
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                                }
                            }
                            else
                            {
                                if (cellcount == 1)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                                else if (cellcount == 3 && ReportProperties.ShowLedgerCode != 1)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                            }
                            if (cellcount == 6)
                            {
                                cellcount = 1;
                            }
                        }
                        else if (ReportProperties.ShowHorizontalLine == 1)
                        {
                            if (rowcount == 1)
                            {
                                if (count == 1)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                                else if (bankNarration != string.Empty || cashNarration != string.Empty)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                            }
                            else
                            {
                                if (cellcount == 3 && ReportProperties.ShowLedgerCode != 1)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                            }
                            if (cellcount == 6)
                            {
                                cellcount = 1;
                            }
                        }
                        else if (ReportProperties.ShowVerticalLine == 1)
                        {
                            if (rowcount == 1)
                            {
                                if (cellcount == 1)
                                {
                                    if (bankNarration != string.Empty || cashNarration != string.Empty)
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                                }
                                else if (cellcount == 3 && ReportProperties.ShowLedgerCode != 1)
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                }
                                else
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                                }
                            }
                            else
                            {
                                if (cellcount == 1)
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                                }
                                else if (cellcount == 3 && ReportProperties.ShowLedgerCode != 1)
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                }
                                else
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                                }
                            }
                            if (cellcount == 6)
                                cellcount = 1;
                        }
                        else
                        {
                            cell.Borders = BorderSide.None;
                        }
                    }
                    cell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        public XRTable SetTableBorder(XRTable table, int HorizontalLine, int VerticalLine)
        {


            int j = table.Rows.Count;
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (HorizontalLine == 1 && VerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                    }
                    else if (HorizontalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                        {
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Bottom;
                        }
                    }
                    else if (VerticalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                        {
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        }
                        else if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right;
                        }
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;

                }
            }
            return table;
        }
        // For only Receipts and Payments & Income and Expenditure Heeader table 
        public XRTable HeadingTableBorder(XRTable table, int HorizontalLine, int VerticalLine)
        {

            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (HorizontalLine == 1 && VerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.All;
                            if (ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else if (count == 4)
                        {
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                            if (ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            }

                        }
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (HorizontalLine == 1)
                    {
                        if (count == 4 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (VerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        else if (count == 4 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        public XRTable SetAbstructRortHeaderTableBorder(XRTable Table)
        {
            foreach (XRTableRow trow in Table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.All;
                            if (ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        else
                            tcell.Borders = BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                }
            }
            return Table;
        }

        public XRTable SetLedgerGroupeBorders(XRTable table, int HorizontalLine, int VerticalLine)
        {

            int j = table.Rows.Count;
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (HorizontalLine == 1 && VerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            if (count == 1 && ReportProperties.ShowGroupCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                    }
                    else if (HorizontalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowGroupCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (VerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                            if (count == 1 && ReportProperties.ShowGroupCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else
                            tcell.Borders = BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }

            return table;
        }

        public XRTable SetTotalTableBorder(XRTable table)
        {

            int j = table.Rows.Count;
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        else
                            tcell.Borders = BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    // tcell.BorderColor = ((int)BorderStyleCell.Regular==0)? System.Drawing.Color.Black :System.Drawing.Color.Black;
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        public XRTable SetGrandTotalTableBorders(XRTable table)
        {
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.All;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right;
                        else
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                }
            }
            return table;


        }

        public string GetAmountFilter()
        {
            string amountfilter = string.Empty;
            if (!string.IsNullOrEmpty(ReportProperty.Current.DonorConditionSymbol) && ReportProperty.Current.DonorFilterAmount > 0)
            {
                switch (ReportProperty.Current.DonorConditionSymbol)
                {
                    case "=":
                        amountfilter = " = " + ReportProperty.Current.DonorFilterAmount;
                        break;
                    case "<>":
                        amountfilter = " <> " + ReportProperty.Current.DonorFilterAmount;
                        break;
                    case ">":
                        amountfilter = " > " + ReportProperty.Current.DonorFilterAmount;
                        break;
                    case ">=":
                        amountfilter = " >= " + ReportProperty.Current.DonorFilterAmount;
                        break;
                    case "<":
                        amountfilter = " < " + ReportProperty.Current.DonorFilterAmount;
                        break;
                    case "<=":
                        amountfilter = " <= " + ReportProperty.Current.DonorFilterAmount;
                        break;
                }
            }
            return amountfilter;
        }

        /// <summary>
        /// This method is used to add filter condition
        /// </summary>
        public string AttachAmountFilter(DataView dv, string filtercolulmns)
        {
            string rtn = string.Empty;
            string AmountFilter = this.GetAmountFilter();
            if (AmountFilter != "")
            {
                string[] filtercolulmnsCol = filtercolulmns.Split(',');
                string amtfitlercondition = string.Empty;
                foreach (string fld in filtercolulmnsCol)
                {
                    if (dv.Table.Columns.Contains(fld))
                    {
                        amtfitlercondition += (string.IsNullOrEmpty(amtfitlercondition) ? string.Empty : " OR ") + "([" + fld + "]  > 0 AND [" + fld + "] " + AmountFilter + ")";
                    }
                }
                dv.RowFilter = amtfitlercondition;

                if (!string.IsNullOrEmpty(amtfitlercondition))
                {
                    rtn = "Amount filtered by " + this.UtilityMember.NumberSet.ToNumber(this.ReportProperties.DonorFilterAmount);
                }
            }
            return rtn;
        }

        #region AlingingTables

        // to align header tables
        public virtual XRTable AlignHeaderTable(XRTable table, bool UseSameFont = false)
        {
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.All;
                            if (ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else if (count == 4)
                        {
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                            if (ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            }
                        }
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Bottom | BorderSide.Top | BorderSide.Left;
                        else if (count == 4 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else if (count == trow.Cells.Count)
                            tcell.Borders = BorderSide.Bottom | BorderSide.Top | BorderSide.Right;
                        else
                            tcell.Borders = BorderSide.Bottom | BorderSide.Top;

                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.All;
                        else if (count == 4 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;

                    //For FD register, FD history and GST return
                    if (UseSameFont || ReportProperties.ReportId == "RPT-094" || ReportProperties.ReportId == "RPT-047" ||
                        ReportProperties.ReportId == "RPT-166" || ReportProperties.ReportId == "RPT-181" || ReportProperties.ReportId == "RPT-205" ||
                        ReportProperties.ReportId == "RPT-213")
                    {
                        tcell.Font = (ReportProperty.Current.ColumnCaptionFontStyle == 0 ?
                                new System.Drawing.Font(tcell.Font, System.Drawing.FontStyle.Bold) : new System.Drawing.Font(tcell.Font, System.Drawing.FontStyle.Regular));
                    }
                    else
                    {
                        tcell.Font = (ReportProperty.Current.ColumnCaptionFontStyle == 0 ?
                                new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))) :
                                new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))));
                    }

                }
            }
            return table;
        }

        // to align content tables
        public virtual XRTable AlignContentTable(XRTable table)
        {

            int j = table.Rows.Count;
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                        {
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                        }
                        else if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Bottom;
                            if (count == trow.Cells.Count)
                            {
                                tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            }
                        }
                        else if (count == trow.Cells.Count)
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }

                        else
                        {
                            tcell.Borders = BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                        {
                            tcell.Borders = BorderSide.Left;
                        }
                        else if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right;
                        }
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;

                }
            }
            return table;
        }

        // to align group tables
        public virtual XRTable AlignGroupTable(XRTable table)
        {
            int j = table.Rows.Count;
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            if (count == 1 && ReportProperties.ShowGroupCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowGroupCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                        else if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Bottom;
                        else if (count == trow.Cells.Count)
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            if (count == 1 && ReportProperties.ShowGroupCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }

            return table;

        }

        // to align total tables
        public virtual XRTable AlignTotalTable(XRTable table)
        {
            int j = table.Rows.Count;
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Bottom | BorderSide.Left;
                        else if (count == trow.Cells.Count)
                            tcell.Borders = BorderSide.Bottom | BorderSide.Right;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom | BorderSide.Top;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom | BorderSide.Top;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    // tcell.BorderColor = ((int)BorderStyleCell.Regular==0)? System.Drawing.Color.Black :System.Drawing.Color.Black;
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        // to align grand total tables
        public virtual XRTable AlignGrandTotalTable(XRTable table)
        {
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.All;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.All;
                    }
                    else
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
            return table;

        }

        public virtual XRLabel AlignBalanceLable(XRLabel lable)
        {
            if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
            {
                lable.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
            }
            else if (ReportProperties.ShowHorizontalLine == 1)
            {
                lable.Borders = BorderSide.Bottom;
            }
            else if (ReportProperties.ShowVerticalLine == 1)
            {
                lable.Borders = BorderSide.Left | BorderSide.Right;
            }
            else
            {
                lable.Borders = BorderSide.None;
            }
            return lable;
        }

        // to aling cash bank book report table
        public virtual XRTable AlignCashBankBookTable(XRTable table, string bankNarration, string cashNarration, int count)
        {
            int rowcount = 0;

            foreach (XRTableRow row in table.Rows)
            {
                int cellcount = 0;
                ++rowcount;
                if (rowcount == 2 && ReportProperties.IncludeNarration != 1)
                {
                    row.Visible = false;
                }
                else if (bankNarration == string.Empty && cashNarration == string.Empty && ReportProperties.IncludeNarration == 1 && rowcount == 2)
                {
                    row.Visible = false;
                }
                else
                {
                    row.Visible = true;
                }
                foreach (XRTableCell cell in row)
                {
                    ++cellcount;
                    if (ReportProperties.IncludeNarration != 1 && rowcount == 1)
                    {
                        if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                        {
                            if (cellcount == 1)
                                cell.Borders = BorderSide.Right | BorderSide.Left | BorderSide.Bottom;
                            else if (ReportProperties.ShowLedgerCode != 1)
                                if (cellcount == 3 || cellcount == 9)
                                    cell.Borders = BorderSide.None;
                                else
                                    cell.Borders = BorderSide.Right | BorderSide.Bottom;
                            else
                                cell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                        else if (ReportProperties.ShowHorizontalLine == 1)
                        {
                            if (cellcount == 1)
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                            else if (ReportProperties.ShowLedgerCode != 1)
                                if (cellcount == 3 || cellcount == 9)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                            else
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;

                        }
                        else if (ReportProperties.ShowVerticalLine == 1)
                        {
                            if (cellcount == 1)
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                            else if (ReportProperties.ShowLedgerCode != 1)
                                if (cellcount == 3 || cellcount == 9)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                            else
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                        }
                        else
                        {
                            cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        }

                    }
                    else
                    {
                        if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                        {
                            if (rowcount == 1)
                            {
                                if (count == 1)
                                {
                                    if (cellcount == 1)
                                    {
                                        if (bankNarration != string.Empty || cashNarration != string.Empty)
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top;
                                        else
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                                    }
                                    else if (ReportProperties.ShowLedgerCode != 1)
                                    {
                                        if (cellcount == 3 || cellcount == 9)
                                            cell.Borders = BorderSide.None;
                                        else
                                        {
                                            if (bankNarration != string.Empty || cashNarration != string.Empty)
                                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                                            else
                                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                                        }
                                    }
                                    else
                                    {
                                        if (bankNarration != string.Empty || cashNarration != string.Empty)
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                                        else
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                                    }
                                }
                                else
                                {
                                    if (cellcount == 1)
                                    {
                                        if (bankNarration != string.Empty || cashNarration != string.Empty)
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                                        else
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                                    }
                                    else if (ReportProperties.ShowLedgerCode != 1)
                                    {
                                        if (cellcount == 3 || cellcount == 9)
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                        else
                                        {
                                            if (bankNarration != string.Empty || cashNarration != string.Empty)
                                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                                            else
                                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                                        }
                                    }
                                    else
                                    {
                                        if (bankNarration != string.Empty || cashNarration != string.Empty)
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                                        else
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                                    }
                                }

                            }
                            else
                            {
                                if (cellcount == 1)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                                else if (ReportProperties.ShowLedgerCode != 1)
                                {
                                    if (cellcount == 3 || cellcount == 9)
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                    else
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                                }
                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                            }

                        }
                        else if (ReportProperties.ShowHorizontalLine == 1)
                        {
                            if (rowcount == 1)
                            {
                                if (count == 1)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                                else if (bankNarration != string.Empty || cashNarration != string.Empty)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                            }
                            else
                            {
                                if (cellcount == 3 && ReportProperties.ShowLedgerCode != 1)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                            }

                        }
                        else if (ReportProperties.ShowVerticalLine == 1)
                        {
                            if (rowcount == 1)
                            {
                                if (cellcount == 1)
                                {
                                    if (bankNarration != string.Empty || cashNarration != string.Empty)
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                                }
                                else if (cellcount == 3 && ReportProperties.ShowLedgerCode != 1)
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                }
                                else
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                                }
                            }
                            else
                            {
                                if (cellcount == 1)
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                                }
                                else if (cellcount == 3 && ReportProperties.ShowLedgerCode != 1)
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                }
                                else
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                                }
                            }

                        }
                        else
                        {
                            cell.Borders = BorderSide.None;
                        }
                    }
                    cell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
                cellcount = 0;
            }
            return table;
        }

        public virtual XRTable AlignOpeningBalanceTable(XRTable table)
        {
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Bottom | BorderSide.Right;
                        else if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 3 || count == 8)
                                tcell.Borders = BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Bottom | BorderSide.Right;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (ReportProperties.ShowLedgerCode != 1)
                        {
                            if (count == 3 || count == 8)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Bottom;
                        }
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        else if (ReportProperties.ShowLedgerCode != 1)
                        {
                            if (count == 3 || count == 8)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Right;
                        }
                        else
                            tcell.Borders = BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    //tcell.BorderColor = ((int)BorderStyleCell.Regular == 0) ? System.Drawing.Color.Black : System.Drawing.Color.Black;
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        public virtual XRTable AlignClosingBalance(XRTable table)
        {
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            if (ReportProperties.ShowDetailedBalance == 1)
                                tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            else
                                tcell.Borders = BorderSide.Left | BorderSide.Right;
                        else if (ReportProperties.ShowLedgerCode != 1)
                            if (ReportProperties.ShowDetailedBalance == 1)
                            {
                                if (count == 3 || count == 9)
                                    tcell.Borders = BorderSide.None;
                                else
                                    tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                            }
                            else
                            {
                                if (count == 3 || count == 9)
                                    tcell.Borders = BorderSide.None;
                                else
                                    tcell.Borders = BorderSide.Right;
                            }
                        else
                            if (ReportProperties.ShowDetailedBalance == 1)
                                tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                            else
                                tcell.Borders = BorderSide.Right;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (ReportProperties.ShowLedgerCode != 1)
                        {
                            if (count == 3 || count == 9)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Bottom;
                        }
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        else if (ReportProperties.ShowLedgerCode != 1)
                        {
                            if (count == 3 || count == 9)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Right;
                        }
                        else
                            tcell.Borders = BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    //tcell.BorderColor = ((int)BorderStyleCell.Regular == 0) ? System.Drawing.Color.Black : System.Drawing.Color.Black;
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        #endregion

        #region Drill-Down Methods
        /// <summary>
        /// This method is used to attach report drill down event to subreports with in the main report
        /// </summary>
        /// <param name="rptSubReport"></param>
        public void AttachDrillDownToSubReport(ReportBase rptSubReport)
        {
            if (ReportDrillDown != null)
            {
                rptSubReport.ReportDrillDown += new EventHandler<EventDrillDownArgs>(ReportDrillDown);
            }
        }

        /// <summary>
        /// This method is used to attach BeforePrint, double-click event to all the fields of the record
        /// </summary>
        /// <param name="xrRptTable"></param>
        /// <param name="xrLinkField"></param>
        public void AttachDrillDownToRecord(XRTable xrRptTable, XRTableCell xrLinkField,
                                   ArrayList arylnkFields, DrillDownType lnkDrillDownType, bool bAttachAllFields, string VoucherType = "", bool hasNarration = false)
        {
            xrLinkField.BeforePrint += new System.Drawing.Printing.PrintEventHandler(RptField_BeforePrint);
            if (bAttachAllFields)
            {
                foreach (XRTableCell RptField in xrRptTable.Rows.FirstRow.Cells)
                {
                    RptField.PreviewDoubleClick -= new PreviewMouseEventHandler(RptField_PreviewDoubleClick);
                    RptField.PreviewDoubleClick += new PreviewMouseEventHandler(RptField_PreviewDoubleClick);
                }
            }
            if (hasNarration)
            {
                ReportProperties.IncludeNarration = 1;
            }
            string drilldownsource = string.Empty;
            foreach (string linkField in arylnkFields)
            {
                drilldownsource += (drilldownsource != string.Empty ? Delimiter.Mew : "") + lnkDrillDownType.ToString() + Delimiter.ECap + linkField + Delimiter.ECap + VoucherType;
            }

            if (drilldownsource != string.Empty)
            {
                string fldname = xrLinkField.Name;
                xrLinkField.PreviewDoubleClick -= new PreviewMouseEventHandler(RptField_PreviewDoubleClick);
                xrLinkField.PreviewDoubleClick += new PreviewMouseEventHandler(RptField_PreviewDoubleClick);
                xrLinkField.PreviewMouseMove += new PreviewMouseEventHandler(xrLinkField_PreviewMouseMove);
                xrLinkField.Target = drilldownsource;
            }
        }

        void xrLinkField_PreviewMouseMove(object sender, PreviewMouseEventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }

        /// <summary>
        /// This evenet is used to hold current printing record to link field of the record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RptField_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRLabel)sender).Tag = GetCurrentRow();
        }

        /// <summary>
        /// This method is used to catch user's drill-down event on the record,
        /// and assing which drill report whould be shown(drill-downed) and drill information into EventDrillDownArgs
        /// and triggerd ReportDrillDown event to ReportViewer Usercontrol
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RptField_PreviewDoubleClick(object sender, PreviewMouseEventArgs e)
        {
            if (e.Brick.Value != null && e.Brick.Value.GetType() == typeof(DataRowView))
            {
                if (ReportDrillDown != null)
                {
                    DataRowView dataRow = e.Brick.Value as DataRowView;
                    XRTableRow xrtblRecord = ((XRTableCell)sender).Row;
                    DrillDown(xrtblRecord, ((XRTableCell)sender), dataRow);
                }
            }
            else if (this.ReportId == "RPT-031") //On 25/06/2019 for balancesheet, Without data row drilling like Drill to Diff.OP balance, Drill to IE report
            {
                if (ReportDrillDown != null)
                {
                    XRTableCell xrTblCell = (XRTableCell)sender;
                    //if (utilityMember.NumberSet.ToDouble(xrTblCell.Text) > 0)
                    //{
                    DrillDownSpecify(xrTblCell);
                    //}
                }
            }
        }

        /// <summary>
        /// This method is used to catch user's drill-down event on the record,
        /// and assing which drill report whould be shown(drill-downed) and drill information into EventDrillDownArgs
        /// and triggerd ReportDrillDown event to ReportViewer Usercontrol
        /// </summary>
        /// <param name="xrtblDrillRecord"></param>
        /// <param name="dataDrillDataRow"></param>
        private void DrillDown(XRTableRow xrtblDrillRecord, XRTableCell xrTblCell, DataRowView dataDrillDataRow)
        {
            try
            {
                Dictionary<string, object> dicDrillDownProperties = new Dictionary<string, object>();
                DrillDownType ddtypeLinkType = DrillDownType.DRILL_DOWN;
                dicDrillDownProperties.Add("DrillDownLink", ddtypeLinkType.ToString());

                string DrillToRptId = UtilityMember.EnumSet.GetDescriptionFromEnumValue(DrillDownType.DRILL_DOWN);
                if (isValidDrillDownRecord(xrtblDrillRecord) && dataDrillDataRow != null)
                {
                    string[] drilldownItmes = xrTblCell.Target.Split(Delimiter.Mew.ToCharArray());
                    foreach (string drilldownItem in drilldownItmes)
                    {
                        string[] sDrillDownLink = drilldownItem.Split(Delimiter.ECap.ToCharArray());
                        if (sDrillDownLink.Length >= 2)
                        {
                            string sLinkField = sDrillDownLink.GetValue(1).ToString();
                            string sVoucherSubTypeField = sDrillDownLink.GetValue(2).ToString();

                            string sLinkFieldValue = string.Empty;
                            string sVoucherType = string.Empty;
                            string sFdType = string.Empty;
                            string hasNarration = string.Empty;
                            if (sLinkField == this.ReportParameters.COST_CENTRE_IDColumn.ColumnName)
                            {
                                if (ReportProperties.ReportId.Equals("RPT-075"))
                                {
                                    sLinkFieldValue = dataDrillDataRow[sLinkField].ToString();
                                    sVoucherType = sVoucherSubTypeField != "" ? dataDrillDataRow[sVoucherSubTypeField].ToString() : string.Empty;
                                }
                                else
                                {
                                    //On 12/06/2017, To set costcentre id, if we show cost center wise or  CC summery report
                                    //should take it from report soure or if single CC, take it from criteria               
                                    if (this.ReportProperties.ShowByCostCentre == 1 || this.ReportId == "RPT-078")
                                    {
                                        sLinkFieldValue = dataDrillDataRow[sLinkField].ToString();
                                    }
                                    else
                                    {
                                        sLinkFieldValue = this.ReportProperties.CostCentre;
                                    }

                                    sVoucherType = sVoucherSubTypeField != "" ? dataDrillDataRow[sVoucherSubTypeField].ToString() : string.Empty;
                                }
                            }
                            else if (sLinkField == this.ReportParameters.DATE_AS_ONColumn.ColumnName)
                            {
                                sLinkFieldValue = this.ReportProperties.DateAsOn;
                                sVoucherType = sVoucherSubTypeField != "" ? dataDrillDataRow[sVoucherSubTypeField].ToString() : string.Empty;
                            }
                            else if (sLinkField == this.ReportParameters.BOOKING_DATEColumn.ColumnName)
                            {
                                sLinkFieldValue = dataDrillDataRow[sLinkField].ToString();
                                sVoucherType = sVoucherSubTypeField != "" ? dataDrillDataRow[sVoucherSubTypeField].ToString() : string.Empty;
                            }
                            //**************Added by sugan--To make the drill dowm for FD statement******************************************************************************************************************************
                            else if (sLinkField == this.ReportParameters.FD_ACCOUNT_IDColumn.ColumnName)
                            {
                                sLinkFieldValue = dataDrillDataRow[sLinkField].ToString(); // Commanded By Mr Alex due to the Drill down Fd Reports
                                // dicDrillDownProperties.Add("FD_TYPE", dataDrillDataRow.Row.Table.Columns.Contains(this.ReportParameters.FD_ACCOUNT_IDColumn.ColumnName)? dataDrillDataRow["FD_VOUCHER_TYPE"].ToString():);
                                sVoucherType = "FDHistory";
                            }
                            else if (sLinkField == "FD_VOUCHER_ID")
                            {
                                // by aldrin when the voucher id is zero get the fd_account_id for the drill down
                                string value = string.Empty;
                                value = sLinkFieldValue = dataDrillDataRow["FD_VOUCHER_ID"].ToString();
                                if (value != "0")
                                {
                                    sLinkFieldValue = dataDrillDataRow["FD_VOUCHER_ID"].ToString();
                                    if (!dicDrillDownProperties.ContainsKey("FD_TYPE"))
                                        dicDrillDownProperties.Add("FD_TYPE", dataDrillDataRow["FD_VOUCHER_TYPE"].ToString());     // to send the FD type
                                }
                                else
                                {
                                    sLinkFieldValue = dataDrillDataRow[this.ReportParameters.FD_ACCOUNT_IDColumn.ColumnName].ToString();
                                    if (!dicDrillDownProperties.ContainsKey("FD_TYPE"))
                                        dicDrillDownProperties.Add("FD_TYPE", dataDrillDataRow["FD_VOUCHER_TYPE"].ToString());
                                }
                                sVoucherType = "FDTRANS";
                            }
                            else if (sLinkField == "FD_RENEWAL_ID")
                            {
                                sLinkFieldValue = dataDrillDataRow[sLinkField].ToString();
                                sVoucherType = "FDTRANS";
                            }
                            //********************************************************************************************************************************************
                            else if (sLinkField == DrillDownPDLFlag.FromLedgerMonthSummary.ToString())
                            {
                                sLinkFieldValue = YesNo.Yes.ToString();
                            }
                            else
                            {
                                sLinkFieldValue = dataDrillDataRow[sLinkField].ToString();
                                sVoucherType = sVoucherSubTypeField != "" ? dataDrillDataRow[sVoucherSubTypeField].ToString() : string.Empty;
                                //On 21/09/2018, For Balancesheet (We show cash, bank accounts and FD group with in report, so when we drill current asset
                                //it takes bank account by default, so we fiexed current asset as parent group)
                                if (ReportProperties.ReportId.Equals("RPT-031") && sLinkField == this.ReportParameters.GROUP_IDColumn.ColumnName
                                    && (this.utilityMember.NumberSet.ToInteger(sLinkFieldValue) == (int)FixedLedgerGroup.Cash ||
                                    this.utilityMember.NumberSet.ToInteger(sLinkFieldValue) == (int)FixedLedgerGroup.BankAccounts ||
                                    this.utilityMember.NumberSet.ToInteger(sLinkFieldValue) == (int)FixedLedgerGroup.FixedDeposit))
                                {
                                    //sLinkFieldValue = "11"; //current asset
                                }
                            }
                            ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), sDrillDownLink.GetValue(0).ToString());
                            if ((ddtypeLinkType == DrillDownType.DRILL_DOWN ||
                                ddtypeLinkType == DrillDownType.LEDGER_VOUCHER) && sVoucherType != ledgerSubType.FD.ToString() && sVoucherType != "FDHistory"
                                && sVoucherType != "FDTRANS")
                            {
                                //On 17/02/2025 - to decide General Vocuher entry or Journal Vocuher Entry
                                //ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), dataDrillDataRow["PARTICULAR_TYPE"].ToString());
                                // 21/02/2025, To decide upon Journal, Receipt, payment and others in the Particular type
                                if (dataDrillDataRow.Row.Table.Columns.Contains("PARTICULAR_TYPE"))
                                {
                                    ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), dataDrillDataRow["PARTICULAR_TYPE"].ToString());
                                }
                                else
                                {
                                    string vouchertypelink = string.Empty;
                                    string vouchertype = string.Empty;
                                    if (drilldownItem.IndexOf("REC_VOUCHER_TYPE") > 0) vouchertypelink = "REC_VOUCHER_TYPE";
                                    else if (drilldownItem.IndexOf("PAY_VOUCHER_TYPE") > 0) vouchertypelink = "PAY_VOUCHER_TYPE";
                                    else if (drilldownItem.IndexOf("VOUCHER_TYPE") > 0) vouchertypelink = "VOUCHER_TYPE";
                                    if (!string.IsNullOrEmpty(vouchertypelink))
                                    {
                                        if (dataDrillDataRow.Row.Table.Columns.Contains(vouchertypelink)) vouchertype = dataDrillDataRow[vouchertypelink].ToString();

                                        if (!string.IsNullOrEmpty(vouchertype))
                                        {
                                            if (vouchertype == FinacialTransType.JN.ToString())
                                                ddtypeLinkType = DrillDownType.LEDGER_JOURNAL_VOUCHER;
                                            else
                                                ddtypeLinkType = DrillDownType.LEDGER_CASHBANK_VOUCHER;
                                        }
                                    }
                                }
                            }
                            //**************Added by sugan--To make the drill dowm for FD statement******************************************************************************************************************************
                            else if (sVoucherType == "FDHistory")
                            {
                                ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), "FD_ACCOUNT");
                            }
                            else if (sVoucherType == "FDTRANS")
                            {
                                ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), "FD_RENEWAL_DRILLDOWN");
                            }
                            else if (sVoucherType == "FD" && (ddtypeLinkType == DrillDownType.LEDGER_CASHBANK_VOUCHER || ddtypeLinkType == DrillDownType.LEDGER_VOUCHER))
                            {
                                //22/03/2017, If we drill from books of accounts reports, for FD entires..drill to FD form based on fd type
                                Int32 voucherid = this.UtilityMember.NumberSet.ToInteger(sLinkFieldValue);
                                ResultArgs result = GetFDDetailbyVoucherId(voucherid);
                                if (result.Success && result.DataSource.Table.Rows.Count > 0)
                                {
                                    ddtypeLinkType = DrillDownType.FD_VOUCHER; //Replace drill to fd account for
                                    sLinkField = "FD_VOUCHER_ID"; //Replace voucherid to FD_voucher_id
                                    if (!dicDrillDownProperties.ContainsKey("FD_TYPE"))
                                        dicDrillDownProperties.Add("FD_TYPE", result.DataSource.Table.Rows[0]["FD_TYPE"].ToString());
                                }
                            }
                            //********************************************************************************************************************************************
                            DrillToRptId = UtilityMember.EnumSet.GetDescriptionFromEnumValue(ddtypeLinkType);
                            dicDrillDownProperties["DrillDownLink"] = ddtypeLinkType.ToString();
                            if (!dicDrillDownProperties.ContainsKey(sLinkField))
                                dicDrillDownProperties.Add(sLinkField, sLinkFieldValue);
                            if (!string.IsNullOrEmpty(sVoucherSubTypeField) && !string.IsNullOrEmpty(sVoucherType) && sLinkField != this.ReportParameters.DATE_AS_ONColumn.ColumnName)
                            {
                                if (sVoucherType == "AST")
                                {
                                    return;
                                }
                                else
                                {
                                    if (!dicDrillDownProperties.ContainsKey(sVoucherSubTypeField))
                                    {
                                        if (!dicDrillDownProperties.ContainsKey(sVoucherSubTypeField))
                                            dicDrillDownProperties.Add(sVoucherSubTypeField, sVoucherType);
                                    }
                                }
                            }
                        }
                    }

                    //Define DrillDown properties
                    if (dicDrillDownProperties.Count > 1)
                    {
                        //On 18/09/2018, to add base report id by default to all links
                        dicDrillDownProperties.Add("REPORT_ID", ReportProperties.ReportId);

                        EventDrillDownArgs eventdrilldownArg = new EventDrillDownArgs(ddtypeLinkType, DrillToRptId, dicDrillDownProperties);
                        ReportDrillDown(this, eventdrilldownArg);
                    }
                }
            }
            catch (Exception Err)
            {
                //MessageBox.Show(Err.Message)
            }
        }


        /// <summary>
        /// On 25/06/2019
        /// This method is used to drill to specify tageget like
        /// # Drill down IE in Balance sheet to IE report
        /// # Drill down to Diff.Open.balance in balancesheet to MapProjectleger define opening balance
        /// </summary>
        /// <param name="xrTblCell"></param>
        private void DrillDownSpecify(XRTableCell xrTblCell)
        {
            try
            {
                Dictionary<string, object> dicDrillDownProperties = new Dictionary<string, object>();
                string[] drilldownItmes = xrTblCell.Target.Split(Delimiter.Mew.ToCharArray());
                foreach (string drilldownItem in drilldownItmes)
                {
                    string[] sDrillDownLink = drilldownItem.Split(Delimiter.ECap.ToCharArray());
                    if (sDrillDownLink.Length >= 2)
                    {
                        DrillDownType ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), sDrillDownLink.GetValue(0).ToString());
                        string DrillToRptId = UtilityMember.EnumSet.GetDescriptionFromEnumValue(ddtypeLinkType);
                        string sLinkField = sDrillDownLink.GetValue(1).ToString();
                        dicDrillDownProperties["DrillDownLink"] = ddtypeLinkType.ToString();
                        if (sLinkField == ReportParameters.DATE_AS_ONColumn.ColumnName)
                            dicDrillDownProperties.Add(sLinkField, this.ReportProperties.DateAsOn);

                        //Define DrillDown properties
                        if (dicDrillDownProperties.Count > 1)
                        {
                            //On 18/09/2018, to add base report id by default to all links
                            dicDrillDownProperties.Add("REPORT_ID", ReportProperties.ReportId);

                            EventDrillDownArgs eventdrilldownArg = new EventDrillDownArgs(ddtypeLinkType, DrillToRptId, dicDrillDownProperties);
                            ReportDrillDown(this, eventdrilldownArg);
                        }
                    }
                }
            }
            catch (Exception Err)
            {
                MessageBox.Show(Err.Message);
            }
        }

        /// <summary>
        /// check whether, xratable record row has drilldown link information 
        /// </summary>
        /// <param name="xrtblRecord"></param>
        /// <returns></returns>
        private bool isValidDrillDownRecord(XRTableRow xrtblRecord)
        {
            bool bValid = false;
            foreach (XRTableCell xrTblCell in xrtblRecord.Cells)
            {
                if (xrTblCell.Target != string.Empty)
                {
                    bValid = true;
                    break;
                }
            }
            return bValid;
        }

        /// <summary>
        /// //16/03/2017, 1. renewal (both fd_voucher_id, fd_interest_voucher_id should be 0) 2. fd_account for investment fd_voucher_id =0" +
        /// This method is used to check, FD voucer ids are 0 or valid
        /// </summary>
        /// <param name="fdaccountid"></param>
        /// <param name="fdtype"></param>
        /// <returns></returns>
        public bool IsValidFDAccount(Int32 fdaccountid, string fdtype)
        {
            bool bValid = false;
            string sqlinvalidFDsql = GetBankReportSQL(SQL.ReportSQLCommand.BankReport.CheckInValidRenewalDetailByFDId);

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.FD_ACCOUNT_IDColumn, fdaccountid);
                dataManager.Parameters.Add("FD_TYPE", fdtype, DataType.String);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                ResultArgs resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlinvalidFDsql);
                if (resultArgs.Success)
                {
                    DataTable dtInvalidFds = resultArgs.DataSource.Table;
                    if (dtInvalidFds != null && dtInvalidFds.Rows.Count == 0)
                    {
                        bValid = true;
                    }
                }
            }
            return bValid;
        }

        /// <summary>
        /// As on 27/10/2021, To get FD AccountId by passing voucher id
        /// </summary>
        /// <param name="FDVoucherId"></param>
        /// <returns></returns>
        public ResultArgs GetFDAccountDetails(Int32 FDVoucherId)
        {
            ResultArgs result = new ResultArgs();
            string sqlFDsql = GetBankReportSQL(SQL.ReportSQLCommand.BankReport.FetchFDAccountIDByVoucherId);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_IDColumn, FDVoucherId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                result = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlFDsql);
            }
            return result;
        }

        /// <summary>
        /// On 04/06/2024, To get fd details
        /// </summary>
        /// <param name="FDAccountId"></param>
        /// <returns></returns>
        public ResultArgs GetFDAccountDetailsByFDId(Int32 FDAccountId)
        {
            ResultArgs result = new ResultArgs();
            string sqlFD = GetBankReportSQL(SQL.ReportSQLCommand.BankReport.FetchFDMasterByFDAccountId);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.FD_ACCOUNT_IDColumn, FDAccountId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                result = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlFD);
            }
            return result;
        }

        /// <summary>
        /// On 18/01/2024, To get list of active FD Accounts
        /// </summary>
        /// <returns></returns>
        public ResultArgs GetFDAccounts(string ProjectIds)
        {
            ResultArgs result = new ResultArgs();
            string sqlFDsql = GetBankReportSQL(SQL.ReportSQLCommand.BankReport.FetchFDAccounts);
            using (DataManager dataManager = new DataManager())
            {
                if (!string.IsNullOrEmpty(ProjectIds) && ProjectIds != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, ProjectIds);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                result = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlFDsql);
            }
            return result;
        }

        /// <summary>
        /// On 26/10/2021, To get FD details
        /// </summary>
        /// <param name="fdaccountid"></param>
        /// <param name="fdtype"></param>
        /// <returns></returns>
        public DataSet GetFDDetails(Int32 fdaccountid)
        {
            DataSet ds = new DataSet();
            string sqlFD = GetBankReportSQL(SQL.ReportSQLCommand.BankReport.FetchFDMasterByFDAccountId);

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.FD_ACCOUNT_IDColumn, fdaccountid);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                ResultArgs resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlFD);
                if (resultArgs.Success)
                {
                    DataTable dtMasterFD = resultArgs.DataSource.Table;
                    if (dtMasterFD != null && dtMasterFD.Rows.Count > 0)
                    {
                        dtMasterFD.TableName = "FD Master";
                        sqlFD = GetBankReportSQL(SQL.ReportSQLCommand.BankReport.FetchFDRenewalsByFDAccountId);
                        using (DataManager dataManager1 = new DataManager())
                        {
                            dataManager1.Parameters.Add(this.ReportParameters.FD_ACCOUNT_IDColumn, fdaccountid);
                            dataManager1.DataCommandArgs.IsDirectReplaceParameter = true;
                            resultArgs = dataManager1.FetchData(DAO.Data.DataSource.DataTable, sqlFD);
                            if (resultArgs.Success)
                            {
                                DataTable dtRenewalsFD = resultArgs.DataSource.Table;
                                dtRenewalsFD.TableName = "FD REnewals";

                                ds.Tables.Clear();
                                ds.Tables.Add(dtMasterFD);
                                ds.Tables.Add(dtRenewalsFD);
                            }
                        }
                    }
                }
            }
            return ds;
        }

        /// <summary>
        /// 21/03/2017
        /// This method is used to get fd detials (FD_ACCOUNT_ID, FD_TYPE) by passing voucher id
        /// this method is called for drill-down for all boooks of accounts reports for FD entries 
        /// (Investment, Interest Receipts and Widthdrawn)
        /// </summary>
        /// <param name="voucherid"></param>
        /// <returns></returns>
        public ResultArgs GetFDDetailbyVoucherId(Int32 voucherid)
        {
            ResultArgs result = new ResultArgs();
            string sql = GetBankReportSQL(SQL.ReportSQLCommand.BankReport.FetchFDDetailByVoucherId);

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_IDColumn, voucherid);
                result = dataManager.FetchData(DAO.Data.DataSource.DataTable, sql);
            }
            return result;
        }
        #endregion

        #region Get Balances

        public ResultArgs GetCashBankFDGroupBalance(bool isOpening, string balDate, string projectIds, string fixedBankcloseddate = "")
        {
            string groupids = GetLiquidityGroupIds();
            ResultArgs resultArgs = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchGroupSumBalance))
            {
                if (!string.IsNullOrEmpty(projectIds))
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, projectIds);
                }

                if (isOpening)
                {
                    //For Opening Balance, Finding Balance Date
                    DateTime dateBalance = DateTime.Parse(balDate).AddDays(-1);
                    balDate = dateBalance.ToShortDateString();
                }
                dataManager.Parameters.Add(this.ReportParameters.BALANCE_DATEColumn, balDate);

                string bankcloseddate = balDate;
                if (!string.IsNullOrEmpty(fixedBankcloseddate))
                {
                    bankcloseddate = fixedBankcloseddate;
                }

                if (!string.IsNullOrEmpty(bankcloseddate))
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_CLOSEDColumn, bankcloseddate);
                }
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, groupids);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs GetCashBankFDGroupBalancePreviousYears(bool isOpening, string balDate, string projectIds, string fixedBankcloseddate = "")
        {
            string groupids = GetLiquidityGroupIds();
            ResultArgs resultArgs = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchGroupSumBalancePreviousYears))
            {
                if (!string.IsNullOrEmpty(projectIds))
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, projectIds);
                }

                if (isOpening)
                {
                    //For Opening Balance, Finding Balance Date
                    DateTime dateBalance = DateTime.Parse(balDate).AddDays(-1);
                    balDate = dateBalance.ToShortDateString();
                }
                dataManager.Parameters.Add(this.ReportParameters.BALANCE_DATEColumn, balDate);

                string bankcloseddate = balDate;
                if (!string.IsNullOrEmpty(fixedBankcloseddate))
                {
                    bankcloseddate = fixedBankcloseddate;
                }

                if (!string.IsNullOrEmpty(bankcloseddate))
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_CLOSEDColumn, bankcloseddate);
                }
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, groupids);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
            }
            return resultArgs;
        }

        public Double GetBalance(string ProjectId, string RptDate, BalanceSystem.LiquidBalanceGroup BalanceGroup,
            BalanceSystem.BalanceType BalanceType, string CashBankLedgerId = "", bool enforceCashBankCurrecy = false, Int32 CurrencyCurrencyId = 0)
        {
            double rtnBalance = 0;
            using (BalanceSystem balanceSystem = new BalanceSystem())
            {
                //On 25/04/2019, This property is used to skip bank ledger's which is closed on or equal to this date
                if (!string.IsNullOrEmpty(this.ReportProperties.DateFrom))
                {
                    balanceSystem.BankClosedDate = this.ReportProperties.DateFrom;
                }

                BalanceProperty balanceProperty = null;
                switch (BalanceGroup)
                {
                    // this is to separate the Multiple Cash Opening so Passing the Cash LEdger Id
                    case BalanceSystem.LiquidBalanceGroup.CashBalance:
                        //05/12/2019, to keep Cash Bank LedgerId
                        //balanceProperty = balanceSystem.GetCashBalance("0", ProjectId, this.ReportProperties.Ledger, RptDate, BalanceType);
                        balanceProperty = balanceSystem.GetCashBalance("0", ProjectId, CashBankLedgerId, RptDate, BalanceType, CurrencyCurrencyId);
                        break;
                    case BalanceSystem.LiquidBalanceGroup.BankBalance:
                        //05/12/2019, to keep Cash Bank LedgerId
                        //balanceProperty = balanceSystem.GetBankBalance("0", ProjectId,this.ReportProperties.Ledger, RptDate, BalanceType);
                        balanceProperty = balanceSystem.GetBankBalance("0", ProjectId, CashBankLedgerId, RptDate, BalanceType, CurrencyCurrencyId);
                        break;
                    case BalanceSystem.LiquidBalanceGroup.FDBalance:
                        balanceProperty = balanceSystem.GetFDBalance(ProjectId, RptDate, BalanceType, CurrencyCurrencyId);
                        break;
                }

                if (balanceProperty != null && balanceProperty.Result.Success)
                {
                    //On 30/08/3034, to enfoce multi currency 
                    double amount = (settingProperty.AllowMultiCurrency == 1 && enforceCashBankCurrecy ? balanceProperty.AmountFC : balanceProperty.Amount);
                    string transmode = (settingProperty.AllowMultiCurrency == 1 && enforceCashBankCurrecy ? balanceProperty.TransFCMode : balanceProperty.TransMode);
                    if (transmode == TransactionMode.CR.ToString())
                    {
                        rtnBalance = -amount; // -UtilityMember.NumberSet.ToDouble(balanceProperty.Amount.ToString());
                    }
                    else
                    {
                        rtnBalance = amount;
                    }

                }
            }
            return rtnBalance;
        }

        public ResultArgs GetBalanceDetail(bool isOpening, string balDate, string projectIds, string groupIds, string fixedBankcloseddate = "")
        {
            ResultArgs resultArgs = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchBalance))
            {
                if (!string.IsNullOrEmpty(projectIds))
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, projectIds);
                }
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, groupIds);

                if (isOpening)
                {
                    //For Opening Balance, Finding Balance Date
                    DateTime dateBalance = DateTime.Parse(balDate).AddDays(-1);
                    balDate = dateBalance.ToShortDateString();
                }

                dataManager.Parameters.Add(this.ReportParameters.BALANCE_DATEColumn, balDate);

                //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                string bankcloseddate = balDate;

                if (!string.IsNullOrEmpty(fixedBankcloseddate))
                {
                    bankcloseddate = fixedBankcloseddate;
                }

                if (!string.IsNullOrEmpty(bankcloseddate))
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_CLOSEDColumn, bankcloseddate);
                }

                //On 11/12/2024, To to currency based reports --------------------------------------------------------------------------------------
                if (settingProperty.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                }
                //----------------------------------------------------------------------------------------------------------------------------------

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs GetBalanceDetailProjectWise(bool isOpening, string balDate, string projectIds, string groupIds, string fixedBankcloseddate = "")
        {
            ResultArgs resultArgs = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchBalanceByProjectwise))
            {
                if (!string.IsNullOrEmpty(projectIds))
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, projectIds);
                }
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, groupIds);

                if (isOpening)
                {
                    //For Opening Balance, Finding Balance Date
                    DateTime dateBalance = DateTime.Parse(balDate).AddDays(-1);
                    balDate = dateBalance.ToShortDateString();
                }

                dataManager.Parameters.Add(this.ReportParameters.BALANCE_DATEColumn, balDate);

                //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                string bankcloseddate = balDate;

                if (!string.IsNullOrEmpty(fixedBankcloseddate))
                {
                    bankcloseddate = fixedBankcloseddate;
                }

                if (!string.IsNullOrEmpty(bankcloseddate))
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_CLOSEDColumn, bankcloseddate);
                }

                //On 11/12/2024, To to currency based reports --------------------------------------------------------------------------------------
                if (settingProperty.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                }
                //----------------------------------------------------------------------------------------------------------------------------------

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 12/07/2024, to get CC Opening Banacne based on cash and Bank
        /// </summary>
        public ResultArgs AssignCCDetailReportSource()
        {
            ResultArgs result = new ResultArgs();
            if (this.settingProperty.ShowCCOpeningBalanceInReports == 1)
            {
                string sqlccDetail = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CCCashBankOpeningBalance);
                using (DataManager dataManager = new DataManager())
                {
                    string datefrom = string.IsNullOrEmpty(this.ReportProperties.DateFrom) ? settingProperty.YearFrom : this.ReportProperties.DateFrom;
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, datefrom);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, !string.IsNullOrEmpty(this.ReportProperties.Project) ? this.ReportProperties.Project : "0");
                    dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, !string.IsNullOrEmpty(this.ReportProperties.CostCentre) ? this.ReportProperties.CostCentre : "0");
                    dataManager.Parameters.Add(this.ReportParameters.COSTCENTRE_MAPPINGColumn, settingProperty.CostCeterMapping);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    result = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlccDetail);

                    if (result.Success && result.DataSource.Table != null)
                    {
                        dtCCOpeningBalance = result.DataSource.Table;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// On 12/07/2024, to get cost centre Opening value
        /// </summary>
        /// <param name="CashOP"></param>
        /// <returns></returns>
        public double getCCOpeningBalance(bool IsCash, bool forAllCC)
        {
            double rtn = 0;
            Int32 ccid = 0;
            double opbaseamount = 0;
            double opcashamount = 0;
            double opbankamount = 0;
            if (this.settingProperty.ShowCCOpeningBalanceInReports == 1 && GetCurrentColumnValue(this.ReportParameters.COST_CENTRE_IDColumn.ColumnName) != null)
            {
                ccid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(this.ReportParameters.COST_CENTRE_IDColumn.ColumnName).ToString());
                if (dtCCOpeningBalance != null && dtCCOpeningBalance.Rows.Count > 0)
                {
                    string filter = ReportParameters.COST_CENTRE_IDColumn.ColumnName + "=" + ccid;
                    if (forAllCC) filter = string.Empty;
                    dtCCOpeningBalance.DefaultView.RowFilter = filter;

                    opbaseamount = UtilityMember.NumberSet.ToDouble(dtCCOpeningBalance.Compute("SUM(OP_BASE_AMOUNT)", filter).ToString());
                    opcashamount = UtilityMember.NumberSet.ToDouble(dtCCOpeningBalance.Compute("SUM(OP_CASH_AMOUNT)", filter).ToString());
                    opbankamount = UtilityMember.NumberSet.ToDouble(dtCCOpeningBalance.Compute("SUM(OP_BANK_AMOUNT)", filter).ToString());
                    if (IsCash) //In books begin, CC donot have cash and bank opening balance hence we consider cc general opening as bank opening balance
                    {
                        rtn = opcashamount;
                    }
                    else
                    {
                        rtn = opbaseamount + opbankamount;
                    }
                }
            }
            return rtn;
        }

        public ResultArgs GetBudgetDetails()
        {
            ResultArgs resultArgs = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchById))
            {
                dataManager.Parameters.Add(ReportParameters.BUDGET_IDColumn, this.ReportProperties.Budget);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
            }
            return resultArgs;
        }

        public string GetProjectName(int ProjectId)
        {
            string Rtn = string.Empty;

            ResultArgs resultArgs = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchProjectNameByProjectId))
            {
                dataManager.Parameters.Add(ReportParameters.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(Bosco.DAO.Data.DataSource.Scalar);
            }

            if (resultArgs.Success)
            {
                Rtn = resultArgs.DataSource.Sclar.ToString;
            }
            return Rtn;
        }

        public ResultArgs GetProjects()
        {
            ResultArgs resultArgs = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchProjects))
            {
                //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                dataManager.Parameters.Add(ReportParameters.DATE_CLOSEDColumn, this.ReportProperties.DateFrom);
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
            }
            return resultArgs;
        }

        public string GetBankNamesByProject()
        {
            ResultArgs resultArgs = new ResultArgs();
            string rtn = string.Empty;
            string projectid = ReportProperty.Current.Project;
            string bankclosedDate = ReportProperty.Current.DateFrom;
            if (!string.IsNullOrEmpty(ReportProperty.Current.DateFrom))
            {
                bankclosedDate = UtilityMember.DateSet.ToDate(ReportProperty.Current.DateFrom, false).AddMonths(-1).ToShortDateString();
            }

            using (DataManager dataManager = new DataManager(SQLCommand.Bank.FetchBankByProject))
            {
                dataManager.Parameters.Add(ReportParameters.PROJECT_IDColumn, projectid);
                if (!string.IsNullOrEmpty(bankclosedDate))
                {
                    dataManager.Parameters.Add(ReportParameters.DATE_CLOSEDColumn, bankclosedDate);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(Bosco.DAO.Data.DataSource.DataTable);
                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    DataTable dtBankNames = resultArgs.DataSource.Table;
                    foreach (DataRow dr in dtBankNames.Rows)
                    {
                        rtn += dr[ReportParameters.BANKColumn.ColumnName].ToString() + ",";
                    }
                    rtn = rtn.TrimEnd(',');
                }
            }

            return rtn;
        }
        #endregion

        public bool IsVoucherAlreadyDeleted(Int32 voucherid)
        {
            bool rtn = false;
            ResultArgs result = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.ValidateDeletedVoucher))
            {
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_IDColumn, voucherid);
                result = dataManager.FetchData(DAO.Data.DataSource.DataTable);
            }
            if (result != null && result.Success && result.RowsAffected > 0)
            {
                rtn = true;
            }
            return rtn;
        }

        public ResultArgs FetchProjectCategroy()
        {
            ResultArgs result = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.Project.Fetch))
            {
                result = dataManager.FetchData(DAO.Data.DataSource.DataTable);
            }
            return result;
        }

        public ResultArgs FetchAllCashBankLedgerByProject(bool enforcedClosed)
        {
            ResultArgs result = new ResultArgs();
            string projectid = string.IsNullOrEmpty(ReportProperty.Current.Project) ? "0" : ReportProperty.Current.Project;
            string bankclosedDate = string.IsNullOrEmpty(ReportProperty.Current.DateFrom) ? settingProperty.YearFrom : ReportProperty.Current.DateFrom;
            if (!string.IsNullOrEmpty(bankclosedDate) && enforcedClosed)
            {
                bankclosedDate = UtilityMember.DateSet.ToDate(bankclosedDate, false).AddMonths(-1).ToShortDateString();
            }

            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchAllCashBankLedgerByProject))
            {
                dataManager.Parameters.Add(ReportParameters.PROJECT_IDColumn, projectid);
                if (!string.IsNullOrEmpty(bankclosedDate))
                {
                    dataManager.Parameters.Add(ReportParameters.DATE_CLOSEDColumn, bankclosedDate);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                result = dataManager.FetchData(DAO.Data.DataSource.DataTable);
            }
            return result;
        }

        /// <summary>
        /// On 28/11/2024 - To get average exchange rate for given courrency country id for currenct FY
        /// </summary>
        /// <returns></returns>
        public decimal GetAvgCurrencyExchangeRateForFY(Int32 CurrencyCountryId)
        {
            ResultArgs result = new ResultArgs();
            decimal rtn = 1;
            if (CurrencyCountryId > 0)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Country.FetchCountryCurrencyExchangeRate))
                {
                    dataManager.Parameters.Add(appSchema.Country.COUNTRY_IDColumn, CurrencyCountryId);
                    dataManager.Parameters.Add(appSchema.Country.APPLICABLE_FROMColumn, settingProperty.YearFrom);
                    dataManager.Parameters.Add(appSchema.Country.APPLICABLE_TOColumn, settingProperty.YearTo);
                    result = dataManager.FetchData(DAO.Data.DataSource.DataTable);

                    if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                    {
                        rtn = utilityMember.NumberSet.ToDecimal(result.DataSource.Table.Rows[0][appSchema.Country.EXCHANGE_RATEColumn.ColumnName].ToString());
                    }
                }
            }
            return rtn;
        }

        /// <summary>
        /// On 28/11/2024 - To get average exchange rate for given courrency country id for currenct FY
        /// </summary>
        /// <returns></returns>
        public decimal GetAvgCurrencyExchangeRateForPreviousFY(Int32 CurrencyCountryId)
        {
            ResultArgs result = new ResultArgs();
            decimal rtn = 1;
            if (CurrencyCountryId > 0)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Country.FetchCountryCurrencyExchangeRate))
                {
                    dataManager.Parameters.Add(appSchema.Country.COUNTRY_IDColumn, CurrencyCountryId);
                    dataManager.Parameters.Add(appSchema.Country.APPLICABLE_FROMColumn, settingProperty.YearFromPrevious);
                    dataManager.Parameters.Add(appSchema.Country.APPLICABLE_TOColumn, settingProperty.YearToPrevious);
                    result = dataManager.FetchData(DAO.Data.DataSource.DataTable);

                    if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                    {
                        rtn = utilityMember.NumberSet.ToDecimal(result.DataSource.Table.Rows[0][appSchema.Country.EXCHANGE_RATEColumn.ColumnName].ToString());
                    }
                }
            }
            return rtn;
        }

        public string GetCurrencySymbol(Int32 CurrencyCountryId)
        {
            ResultArgs result = new ResultArgs();
            string rtn = string.Empty;
            if (CurrencyCountryId > 0)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Country.Fetch))
                {
                    dataManager.Parameters.Add(appSchema.Country.COUNTRY_IDColumn, CurrencyCountryId);
                    result = dataManager.FetchData(DAO.Data.DataSource.DataTable);
                }

                if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                {
                    rtn = result.DataSource.Table.Rows[0][appSchema.Country.CURRENCY_SYMBOLColumn.ColumnName].ToString();
                }
            }
            return rtn;
        }

        #region ThirdPartyCode
        public bool isVoucherPostedByThirdParty(int voucherId)
        {
            bool rtn = false;
            string name = "";
            ResultArgs result = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.ValidateManagementCode))
            {
                dataManager.Parameters.Add(this.reportParameters.VOUCHER_IDColumn, voucherId);
                result = dataManager.FetchData(DAO.Data.DataSource.DataTable);
                name = result.DataSource.Table.Rows[0]["CLIENT_CODE"].ToString();
            }
            if (!string.IsNullOrEmpty(name))
            {
                rtn = true;
            }
            return rtn;
        }

        #endregion
    }

    /// <summary>
    /// This class is used to hold drill-down information
    /// send through drilldown event to ui
    /// </summary>
    public class EventDrillDownArgs : EventArgs
    {
        private string drillDownRpt = string.Empty;
        private DrillDownType drillDownType = DrillDownType.DRILL_DOWN;
        private Dictionary<string, object> dicDrillDownProperties = new Dictionary<string, object>();

        public EventDrillDownArgs(DrillDownType ddType, string ddRptId, Dictionary<string, object> dicDDProperties)
        {
            drillDownType = ddType;
            this.drillDownRpt = ddRptId;
            this.dicDrillDownProperties = dicDDProperties;
        }

        public DrillDownType DrillDownType
        {
            get
            {
                return drillDownType;
            }
        }

        public string DrillDownRpt
        {
            get
            {
                return drillDownRpt;
            }
        }

        public Dictionary<string, object> DrillDownProperties
        {
            get
            {
                return dicDrillDownProperties;
            }
        }




    }


    public class ReportCommandHandler : ICommandHandler
    {

        private XtraReport Rpt;

        public ReportCommandHandler(XtraReport xrRpt)
        {
            this.Rpt = xrRpt;
        }

        public virtual void HandleCommand(PrintingSystemCommand command,
    object[] args, IPrintControl control, ref bool handled)
        {
            if (!CanHandleCommand(command, control)) return;

            if (Rpt.FindControl("tcLedgerName", false) != null)
                Rpt.FindControl("tcLedgerName", false).Visible = false;

            handled = true;
        }

        public virtual bool CanHandleCommand(PrintingSystemCommand command, IPrintControl control)
        {
            return command == PrintingSystemCommand.Print || command == PrintingSystemCommand.PrintDirect;
        }

    }


}
