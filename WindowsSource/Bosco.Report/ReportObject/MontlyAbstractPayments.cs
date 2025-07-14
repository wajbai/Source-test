using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Report.Base;
using System.Data;
using Bosco.Utility.ConfigSetting;

namespace Bosco.Report.ReportObject
{
    public partial class MontlyAbstractPayments : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor

        public MontlyAbstractPayments()
        {
            InitializeComponent();
            
            this.AttachDrillDownToRecord(xrTableLedgerGroup, tcGrpGroupName, new ArrayList { reportSetting1.MonthlyAbstract.GROUP_IDColumn.ColumnName }, 
                DrillDownType.GROUP_SUMMARY_PAYMENTS, false);
            
            this.AttachDrillDownToRecord(xrtblLedger, tcLedgerName, new ArrayList { reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName}, 
                    DrillDownType.LEDGER_SUMMARY, false);
        }

        #endregion

        #region ShowReport

        public override void ShowReport()
        {
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                || String.IsNullOrEmpty(this.ReportProperties.Project))
            {
                ShowReportFilterDialog();
            }
            else
            {
                BindPaymentSource();
            }

            base.ShowReport();
        }

        #endregion

        #region Methods

        public void HideReportHeaderFooter()
        {
            this.HideReportHeader = false;
            this.HidePageFooter = false;
        }

        public void BindPaymentSource()
        {
            this.ReportTitle = ReportProperty.Current.ReportTitle;
            this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
            this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + this.ReportProperties.DateFrom + " - " + this.ReportProperties.DateTo;

            ResultArgs resultArgs = GetReportSource();
            DataView dvPayment = resultArgs.DataSource.TableView;

            if (dvPayment != null)
            {
                dvPayment.Table.TableName = "MonthlyAbstract";
                this.DataSource = dvPayment;
                this.DataMember = dvPayment.Table.TableName;
            }

            AccountBalance accountBalance = xrSubBalance.ReportSource as AccountBalance;
            SetReportSetting(dvPayment, accountBalance);
            accountBalance.BindBalance(false, true);

            prBalancePeriodAmount.Value = accountBalance.PeriodBalanceAmount;
            prBalanceProgressiveAmount.Value = accountBalance.ProgressiveBalanceAmount;
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlMonthlyAbstractReceipts = this.GetReportSQL(SQL.ReportSQLCommand.Report.MonthlyAbstract);
            string dateProgress = this.GetProgressiveDate(this.ReportProperties.DateFrom);
            string liquidityGroupIds = this.GetLiquidityGroupIds();

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_PROGRESS_FROMColumn, dateProgress);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, TransType.PY.ToString());
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, liquidityGroupIds);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, sqlMonthlyAbstractReceipts);
            }

            return resultArgs;
        }

        private void SetReportSetting(DataView dvPayment, AccountBalance accountBalance)
        {
            float actualCodeWidth = tcCapCode.WidthF;
            bool isCapCodeVisible = true;

            tcCapAmountPeriod.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.FORTHEPERIOD);
            tcCapAmountProgress.Text = this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.PROGRESSIVETOTAL);

            //Attach / Detach all ledgers
            dvPayment.RowFilter = "";
            if (ReportProperties.IncludeAllLedger == 0)
            {
                dvPayment.RowFilter = reportSetting1.MonthlyAbstract.HAS_TRANSColumn.ColumnName + " = 1";
            }

            //Include / Exclude Code
            if (tcCapCode.Tag != null && tcCapCode.Tag.ToString() != "")
            {
                actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(tcCapCode.Tag.ToString());
            }
            else
            {
                tcCapCode.Tag = tcCapCode.WidthF;
            }

            isCapCodeVisible = (ReportProperties.ShowGroupCode == 1 || ReportProperties.ShowLedgerCode == 1);
            tcCapCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            tcGrpGroupCode.WidthF = ((ReportProperties.ShowGroupCode == 1) ? actualCodeWidth : 0); ;
            tcLedgerCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);

            //Include / Exclude Ledger group or Ledger
            grpLedgerGroup.Visible = (ReportProperties.ShowByLedgerGroup == 1);
            grpLedger.Visible = (ReportProperties.ShowByLedger == 1);
            grpLedgerGroup.GroupFields[0].FieldName = "";
            grpLedger.GroupFields[0].FieldName = "";

            if (grpLedgerGroup.Visible == false && grpLedger.Visible == false)
            {
                grpLedger.Visible = true;
            }

            if (grpLedgerGroup.Visible)
            {
                if (ReportProperties.SortByGroup == 1)
                {
                    grpLedgerGroup.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.LEDGER_GROUPColumn.ColumnName;

                }
                else
                {
                    grpLedgerGroup.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.GROUP_CODEColumn.ColumnName;
                }
            }

            if (grpLedger.Visible)
            {
                if (ReportProperties.SortByLedger == 1)
                {
                    grpLedger.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.LEDGER_NAMEColumn.ColumnName;
                }
                else
                {
                    grpLedger.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.LEDGER_CODEColumn.ColumnName;
                }
            }

            //Include / Exclude Report Lines

            //Sub Group Row Style
            if (ReportProperties.ShowVerticalLine == 1 && ReportProperties.ShowHorizontalLine == 1)
            {
                styleRow.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
            }
            else if (ReportProperties.ShowVerticalLine == 1)
            {
                styleRow.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right;
            }
            else if (ReportProperties.ShowHorizontalLine == 1)
            {
                styleRow.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            }
            else
            {
                styleRow.Borders = DevExpress.XtraPrinting.BorderSide.None;
            }

            //Group Row Style
            if (grpLedger.Visible == false)
            {
                styleGroupRow.BackColor = Color.White;
                styleGroupRow.Borders = styleRow.Borders;
                xrTableLedgerGroup.StyleName = styleGroupRow.Name;
            }
            else
            {
                xrTableLedgerGroup.StyleName = styleGroupRowBase.Name;
            }

            //Set Subreport Properties
            xrSubBalance.LeftF = xrTableHeader.LeftF;
            accountBalance.LeftPosition = (xrTableHeader.LeftF - 5);
            accountBalance.CodeColumnWidth = tcCapCode.WidthF;
            accountBalance.NameColumnWidth = tcCapParticulars.WidthF;
            accountBalance.AmountColumnWidth = tcCapAmountPeriod.WidthF;
            accountBalance.AmountProgressiveColumnWidth = tcCapAmountProgress.WidthF;
        }

        #endregion

        private void tcAmountPeriod_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double PeriodAmt = this.ReportProperties.NumberSet.ToDouble(tcAmountPeriod.Text);
            if (PeriodAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                tcAmountPeriod.Text = "";
            }
        }

        private void tcAmountProgress_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double ProgressAmt = this.ReportProperties.NumberSet.ToDouble(tcAmountProgress.Text);
            if (ProgressAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                tcAmountProgress.Text = "";
            }
        }
    }
}
