using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using System.Data;
using Bosco.DAO.Data;
using System.Web;

namespace Bosco.Report.ReportObject
{
    public partial class GeneralateReconciliazione : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variable
        int RecordNumber = 0;
        double BRSOpeningTotal = 0;
        double BRSClosingTotal = 0;
        #endregion

        #region Constructor
        public GeneralateReconciliazione()
        {
            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// 14/01/2020, for FDCCSI, to keep Management: NET Total
        /// </summary>
        /// 
        private double netTotalMgnActivity;
        public double NetTotalManagementActivity
        {
            get
            {
                return netTotalMgnActivity;
            }
            set
            {
                netTotalMgnActivity = value;
            }
        }

        /// <summary>
        /// 14/01/2020, for FDCCSI, to keep Movement FD: NET Total
        /// </summary>
        /// 
        private double nettotalmoveActivity;
        public double NetTotalMovementActivity
        {
            get
            {
                return nettotalmoveActivity;
            }
            set
            {
                nettotalmoveActivity = value;
            }
        }

        public GeneralateActivityIE activitySubReport = null;
        public GeneralateActivityIEFA activityFAReports = null;

        #region ShowReports
        public override void ShowReport()
        {
            RecordNumber = 0;
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                || this.ReportProperties.Project == "0")
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                BindSource();
            }

            base.ShowReport();
        }

        #endregion

        #region Methods
        public void BindSource(bool fromMasterReport = false)
        {
            RecordNumber = 0;
            BRSOpeningTotal = 0;
            BRSClosingTotal = 0;

            SetReportTitle();
            //on 29/01/2021
            //ResultArgs resultArgs = GetReportSource();
            this.SetLandscapeHeader = xrTblBRS.WidthF;
            this.SetLandscapeFooter = xrTblBRS.WidthF;
            this.SetLandscapeFooterDateWidth = xrTblBRS.WidthF;

            if (fromMasterReport)
            {
                this.HideReportHeader = this.HidePageFooter = false; // this.HidePageHeader = 09/08/2024
                //this.HideHeaderFully = true; // 09/08/2024
                xrlblYear.Text = "Year " + UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).Year.ToString();
            }

            //on 29/01/2021
            //if (resultArgs.Success && resultArgs.DataSource != null)
            //{
            //    DataTable dtCBBalances = resultArgs.DataSource.Table;
            //    if (dtCBBalances != null)
            //    {
            //        dtCBBalances.TableName = this.DataMember;
            //        this.DataSource = dtCBBalances;
            //        this.DataMember = dtCBBalances.TableName;
            //    }
            //}

            FillReportProperties();
        }


        private void FillReportProperties()
        {
            double CashOpSum = 0;
            double bankOpSum = 0;
            double fdOpSum = 0;

            double CashCLSum = 0;
            double bankCLSum = 0;
            double fdCLSum = 0;

            xrcellBRSLedgerName1.Text = "Opening balance Cash, money and cheques  on " + ReportProperties.DateFrom;
            xrcellBRSLedgerName3.Text = "Total interest bearing deposits on " + ReportProperties.DateFrom;
            xrcellBRSLedgerName5.Text = "Negative balance c/c on " + ReportProperties.DateFrom;
            xrcellBRSLedgerName7.Text = "Total initial balance as of " + ReportProperties.DateFrom + " (A + B + C)";

            xrcellBRSLedgerName13.Text = "Total closing balance as of " + ReportProperties.DateTo + " (D + E + F)";
            xrcellBRSLedgerName17.Text = "Closing bal. Cash, money and checks as at " + ReportProperties.DateTo;
            xrcellBRSLedgerName19.Text = "Total interest bearing deposits as at " + ReportProperties.DateTo;
            xrcellBRSLedgerName21.Text = "Current a/c overdraft as at " + ReportProperties.DateTo;
            xrcellBRSLedgerName23.Text = "Total closing balance as at " + ReportProperties.DateTo + " (H + I - L)"; ;

            //#. Filtler BRS details (Cash/Bank/FD Opening and Closing)
            string fitler = (int)FixedLedgerGroup.Cash + "," + (int)FixedLedgerGroup.BankAccounts + "," + (int)FixedLedgerGroup.FixedDeposit;
            ResultArgs resultbalance = this.GetBalanceDetail(true, this.ReportProperties.DateFrom, this.ReportProperties.Project, fitler);
            if (resultbalance.Success && resultbalance.DataSource.Table != null)
            {
                DataTable dtOpBalance = resultbalance.DataSource.Table;

                string cashfilter = reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.Cash;
                string bankfilter = reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.BankAccounts; ;
                string fdfilter = reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.FixedDeposit; ;

                CashOpSum = UtilityMember.NumberSet.ToDouble(dtOpBalance.Compute("SUM(" + reportSetting1.AccountBalance.AMOUNTColumn.ColumnName + ")", cashfilter).ToString());
                bankOpSum = UtilityMember.NumberSet.ToDouble(dtOpBalance.Compute("SUM(" + reportSetting1.AccountBalance.AMOUNTColumn.ColumnName + ")", bankfilter).ToString());
                fdOpSum = UtilityMember.NumberSet.ToDouble(dtOpBalance.Compute("SUM(" + reportSetting1.AccountBalance.AMOUNTColumn.ColumnName + ")", fdfilter).ToString());

                resultbalance = this.GetBalanceDetail(false, this.ReportProperties.DateTo, this.ReportProperties.Project, "12,13,14");
                if (resultbalance.Success && resultbalance.DataSource.Table != null)
                {
                    DataTable dtClBalance = resultbalance.DataSource.Table;
                    CashCLSum = UtilityMember.NumberSet.ToDouble(dtClBalance.Compute("SUM(" + reportSetting1.AccountBalance.AMOUNTColumn.ColumnName + ")", cashfilter).ToString());
                    bankCLSum = UtilityMember.NumberSet.ToDouble(dtClBalance.Compute("SUM(" + reportSetting1.AccountBalance.AMOUNTColumn.ColumnName + ")", bankfilter).ToString());
                    fdCLSum = UtilityMember.NumberSet.ToDouble(dtClBalance.Compute("SUM(" + reportSetting1.AccountBalance.AMOUNTColumn.ColumnName + ")", fdfilter).ToString());
                }

                BRSOpeningTotal = CashOpSum + bankOpSum + fdOpSum;
                BRSClosingTotal = CashCLSum + bankCLSum + fdCLSum;

            }

            //OP Balance as on date from
            xrcellBRSAmount1.Text = UtilityMember.NumberSet.ToNumber(CashOpSum);
            xrcellBRSAmount3.Text = UtilityMember.NumberSet.ToNumber(bankOpSum + fdOpSum);
            xrcellBRSCBOpeningTotal.Text = UtilityMember.NumberSet.ToNumber(BRSOpeningTotal);

            //CLS Balance as on date to
            xrcellBRSAmount17.Text = UtilityMember.NumberSet.ToNumber(CashCLSum);
            xrcellBRSAmount19.Text = UtilityMember.NumberSet.ToNumber(bankCLSum + fdCLSum);
            xrcellBRSCBClosingTotal.Text = UtilityMember.NumberSet.ToNumber(BRSClosingTotal);

            //xrcellBRSAmount9.Text = UtilityMember.NumberSet.ToNumber(this.NetTotalManagementActivity);
            //xrcellBRSAmount11.Text = UtilityMember.NumberSet.ToNumber(this.NetTotalMovementActivity);
            //xrcellBRSPLTotal.Text = UtilityMember.NumberSet.ToNumber(this.NetTotalMovementActivity + this.NetTotalManagementActivity + BRSOpeningTotal);


            xrcellBRSAmount9.Text = UtilityMember.NumberSet.ToNumber(activitySubReport.NetTotalManagementActivity);
            xrcellBRSAmount11.Text = UtilityMember.NumberSet.ToNumber(activityFAReports.NetTotalMovementActivity);
            xrcellBRSPLTotal.Text = UtilityMember.NumberSet.ToNumber(activityFAReports.NetTotalMovementActivity + activitySubReport.NetTotalManagementActivity + BRSOpeningTotal);
        }


        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlIncomeExpenditure = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.GeneralateReports.GeneralatePatrimonial);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);


                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                resultArgs = dataManager.FetchData(dataManager, DAO.Data.DataSource.DataTable, sqlIncomeExpenditure);
            }
            return resultArgs;
        }

        private void FillReportPropertiesOLD()
        {
            xrcellBRSLedgerName1.Text = "Opening balance Cash, money and cheques  on " + ReportProperties.DateFrom;
            xrcellBRSLedgerName3.Text = "Total interest bearing deposits on " + ReportProperties.DateFrom;
            xrcellBRSLedgerName5.Text = "Negative balance c/c on " + ReportProperties.DateFrom;
            xrcellBRSLedgerName7.Text = "Total initial balance as of " + ReportProperties.DateFrom + " (A + B + C)";

            xrcellBRSLedgerName13.Text = "Total closing balance as of " + ReportProperties.DateTo + " (D + E + F)";
            xrcellBRSLedgerName17.Text = "Closing bal. Cash, money and checks as at " + ReportProperties.DateTo;
            xrcellBRSLedgerName19.Text = "Total interest bearing deposits as at " + ReportProperties.DateTo;
            xrcellBRSLedgerName21.Text = "Current a/c overdraft as at " + ReportProperties.DateTo;
            xrcellBRSLedgerName23.Text = "Total closing balance as at " + ReportProperties.DateTo + " (H + I - L)"; ;

            //#. Filtler BRS details (Cash/Bank/FD Opening and Closing)
            if (this.DataSource != null)
            {
                DataTable dtReport = this.DataSource as DataTable;
                if (dtReport != null)
                {
                    string cashfilter = reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.Cash;
                    string bankfilter = reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.BankAccounts; ;
                    string fdfilter = reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.FixedDeposit; ;

                    double CashOpSum = UtilityMember.NumberSet.ToDouble(dtReport.Compute("SUM(" + "AMOUNT_OP" + ")", cashfilter).ToString());
                    double bankOpSum = UtilityMember.NumberSet.ToDouble(dtReport.Compute("SUM(" + "AMOUNT_OP" + ")", bankfilter).ToString());
                    double fdOpSum = UtilityMember.NumberSet.ToDouble(dtReport.Compute("SUM(" + "AMOUNT_OP" + ")", fdfilter).ToString());

                    double CashCLSum = UtilityMember.NumberSet.ToDouble(dtReport.Compute("SUM(" + "AMOUNT_CL" + ")", cashfilter).ToString());
                    double bankCLSum = UtilityMember.NumberSet.ToDouble(dtReport.Compute("SUM(" + "AMOUNT_CL" + ")", bankfilter).ToString());
                    double fdCLSum = UtilityMember.NumberSet.ToDouble(dtReport.Compute("SUM(" + "AMOUNT_CL" + ")", fdfilter).ToString());

                    BRSOpeningTotal = CashOpSum + bankOpSum + fdOpSum;
                    BRSClosingTotal = CashCLSum + bankCLSum + fdCLSum;

                    //OP Balance as on date from
                    xrcellBRSAmount1.Text = UtilityMember.NumberSet.ToNumber(CashOpSum);
                    xrcellBRSAmount3.Text = UtilityMember.NumberSet.ToNumber(bankOpSum + fdOpSum);
                    xrcellBRSCBOpeningTotal.Text = UtilityMember.NumberSet.ToNumber(BRSOpeningTotal);

                    //CLS Balance as on date to
                    xrcellBRSAmount17.Text = UtilityMember.NumberSet.ToNumber(CashCLSum);
                    xrcellBRSAmount19.Text = UtilityMember.NumberSet.ToNumber(bankCLSum + fdCLSum);
                    xrcellBRSCBClosingTotal.Text = UtilityMember.NumberSet.ToNumber(BRSClosingTotal);
                }
            }

            ResultArgs resultArgs = new ResultArgs();
            //Movement of FA and Capital 
            /*string sqlFACaptial= this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.GeneralateActivityIncomeExpenseFA);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Generalate.GeneralateActivityIncomeExpenseFA, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                if (this.ReportProperties.BranchOffice != null && this.ReportProperties.BranchOffice != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                }

                if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(dataManager, DAO.Data.DataSource.DataTable, sqlFACaptial);
                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    DataTable dtFACaptial = resultArgs.DataSource.Table; 
                    double income =  UtilityMember.NumberSet.ToDouble(dtFACaptial.Compute("SUM(" + reportSetting1.CongiregationProfitandLoss.NXTRECEIPTColumn.ColumnName + ")", string.Empty).ToString());
                    double expense = UtilityMember.NumberSet.ToDouble(dtFACaptial.Compute("SUM(" + reportSetting1.CongiregationProfitandLoss.NXTPAYMENTColumn.ColumnName + ")", string.Empty).ToString());
                    FACaptialTotal = income - expense;
                    xrcellBRSAmount9.Text = UtilityMember.NumberSet.ToNumber(FACaptialTotal);
                }
            }*/

            //Managment Activity Profilt and Loss
            /*string sqlManagementActivity = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.Generalate.GeneralateActivityIncomeExpense);
            using (DataManager dataManager = new DataManager(SQL.ReportSQLCommand.Generalate.GeneralateActivityIncomeExpense, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);

                if (this.ReportProperties.BranchOffice != null && this.ReportProperties.BranchOffice != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.BRANCH_OFFICE_IDColumn, this.ReportProperties.BranchOffice);
                }

                if (!string.IsNullOrEmpty(this.ReportProperties.Society) && this.ReportProperties.Society != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEGAL_ENTITY_IDColumn, this.ReportProperties.Society);
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(dataManager, DAO.Data.DataSource.DataTable, sqlManagementActivity);
                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    DataTable dtManagmentProfitLoss = resultArgs.DataSource.Table;

                    double income = UtilityMember.NumberSet.ToDouble(dtManagmentProfitLoss.Compute("SUM(" + reportSetting1.CongiregationProfitandLoss.NXTRECEIPTColumn.ColumnName + ")", string.Empty).ToString());
                    double expense = UtilityMember.NumberSet.ToDouble(dtManagmentProfitLoss.Compute("SUM(" + reportSetting1.CongiregationProfitandLoss.NXTPAYMENTColumn.ColumnName + ")", string.Empty).ToString());
                    ManagmentTotal = income - expense;
                    xrcellBRSAmount11.Text = UtilityMember.NumberSet.ToNumber(ManagmentTotal);
                }

            }*/

            //xrcellBRSAmount9.Text = UtilityMember.NumberSet.ToNumber(this.NetTotalManagementActivity);
            //xrcellBRSAmount11.Text = UtilityMember.NumberSet.ToNumber(this.NetTotalMovementActivity);
            //xrcellBRSPLTotal.Text = UtilityMember.NumberSet.ToNumber(this.NetTotalMovementActivity + this.NetTotalManagementActivity + BRSOpeningTotal);

            xrcellBRSAmount9.Text = UtilityMember.NumberSet.ToNumber(activitySubReport.NetTotalManagementActivity);
            xrcellBRSAmount11.Text = UtilityMember.NumberSet.ToNumber(activityFAReports.NetTotalMovementActivity);
            xrcellBRSPLTotal.Text = UtilityMember.NumberSet.ToNumber(activityFAReports.NetTotalMovementActivity + activitySubReport.NetTotalManagementActivity + BRSOpeningTotal);
        }

        #endregion

        private void xrcellBRSAmount9_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (activitySubReport != null)
            {
                XRTableCell cell = sender as XRTableCell;
                // cell.Text = UtilityMember.NumberSet.ToNumber(this.NetTotalManagementActivity);
                cell.Text = UtilityMember.NumberSet.ToNumber(activitySubReport.NetTotalManagementActivity);
            }
        }

        private void xrcellBRSAmount11_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (activityFAReports != null)
            {
                XRTableCell cell = sender as XRTableCell;
                // cell.Text = UtilityMember.NumberSet.ToNumber(this.NetTotalMovementActivity);
                cell.Text = UtilityMember.NumberSet.ToNumber(activityFAReports.NetTotalMovementActivity);
            }
        }

        private void xrcellBRSPLTotal_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (activitySubReport != null)
            {
                XRTableCell cell = sender as XRTableCell;
                //cell.Text = UtilityMember.NumberSet.ToNumber(this.NetTotalMovementActivity + this.NetTotalManagementActivity + BRSOpeningTotal);
                cell.Text = UtilityMember.NumberSet.ToNumber(activityFAReports.NetTotalMovementActivity + activitySubReport.NetTotalManagementActivity + BRSOpeningTotal);
            }
        }

    }
}
