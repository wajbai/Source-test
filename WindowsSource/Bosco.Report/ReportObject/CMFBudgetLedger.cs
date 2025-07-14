using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;
using AcMEDSync.Model;
using Bosco.DAO.Schema;

namespace Bosco.Report.ReportObject
{
    public partial class CMFBudgetLedger : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variables
        DateTime dtPrevBudgetDateFrom = new DateTime();
        DateTime dtPrevBudgetDateTo = new DateTime();
        DateTime dtBudgetDateFrom = new DateTime();
        DateTime dtBudgetDateTo = new DateTime();
        string BudgetProjects = string.Empty;
        Int32 ActiveGroup = 0;
        decimal HOBudgetHelpPropsedAmt = 0;
        decimal HOBudgetHelpApprovedAmt = 0;

        TransMode BudgetTransMode = TransMode.CR;

        #endregion

        public CMFBudgetLedger()
        {
            InitializeComponent();
        }

        #region Show Reports
        public override void ShowReport()
        {
            
            BudgetViewSource();
            base.ShowReport();
        }
        #endregion

        #region Methods
        private void BudgetViewSource()
        {
            this.ReportTitle = "CMF-CHE All Communities Budget Summary";
            this.ReportPeriod = "Period : 01-01-2022 : 31-12-2022 ";

            //SetReportTitle();

            ResultArgs resultArgs = new ResultArgs();
            string budgetledger = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.cmfBudgetLedgers);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetledger);
                this.DataSource = resultArgs.DataSource.Table;
                this.DataMember = resultArgs.DataSource.Table.TableName;

                SetReportBorder();
            }

        }
        private void SetReportBorder()
        {

            SetTitleWidth(xrtblHeaderCaption.WidthF);
            this.SetLandscapeBudgetNameWidth = xrtblHeaderCaption.WidthF;
            this.SetLandscapeHeader = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooter = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooterDateWidth = xrtblHeaderCaption.WidthF - 160;
            setHeaderTitleAlignment();

            xrtblLedger = AlignContentTable(xrtblLedger);
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrtblGrpLedgerGroup = AlignContentTable(xrtblGrpLedgerGroup);
            //xrTblSumFooter = AlignContentTable(xrTblSumFooter);

            if (AppSetting.IS_CMF_CONGREGATION)
            {

                float actualCodeWidth = xrCapCode.WidthF;

                //Include / Exclude Code
                if (xrCapCode.Tag != null && xrCapCode.Tag.ToString() != "")
                {
                    actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrCapCode.Tag.ToString());
                }
                else
                {
                    xrCapCode.Tag = xrCapCode.WidthF;
                }

                //xrCapCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);
                //xrCapCode1.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);
                //xrLedgerCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);
            }
        }
        #endregion

        private void xrTableCell1_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (this.GetCurrentColumnValue("BUDGET_TRANS_MODE") != null)
            {
                if (this.GetCurrentColumnValue("BUDGET_TRANS_MODE").ToString().ToUpper() == "CR")
                {
                    e.Value = "INCOME";
                }
                else
                {
                    e.Value = "EXPENDITURE";
                }
            }
            
        }
    }
}