using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.Report.Base;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;
using System.Globalization;
using Bosco.Utility.ConfigSetting;
using Bosco.Report.View;
using DevExpress.XtraSplashScreen;
using System.Linq;
using DevExpress.XtraReports.UI.PivotGrid;
using DevExpress.Data.Filtering;
using DevExpress.XtraPivotGrid;

namespace Bosco.Report.ReportObject
{
    public partial class BudgetMultiYear : Bosco.Report.Base.ReportHeaderBase
    {

        public BudgetMultiYear()
        {
            InitializeComponent();

            this.SetTitleWidth(this.PageWidth - 25);
            this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = this.PageWidth - 25;
        }

        public void HideReportHeaderFooter()
        {
            this.HideReportHeader = false;
            this.HidePageFooter = false;
        }

        #region ShowReport
        public override void ShowReport()
        {
            base.ShowReport();
        }
        #endregion

        #region Methods
        public void BindBudgetMultiYear(DataTable dtBalance, XRTable baseReportTable)
        {
            setHeaderTitleAlignment();
            SetReportTitle();
            this.SetTitleWidth(baseReportTable.WidthF);
            this.HideDateRange = false;
            HideReportHeaderFooter();

            AlignContentTable(xrtblLedger);
            AlignContentTable(xrtblGrpLedgerGroup);
                        
            //30/07/2021, To remove bsaed on no of years
            HideYearColumns();
                        
            dtBalance.TableName = this.DataMember;
            this.DataSource = dtBalance;
            this.DataMember = dtBalance.TableName;
            if (dtBalance!=null)
            {
                bool records = (dtBalance.Rows.Count > 0);
                Detail.Visible = records;
                grpGrpLGBalanceHeader.Visible = grpGrpLGBalanceHeader.Visible  = records;
            }
        }
        
        
        private void HideYearColumns()
        {
            for (int i = this.ReportProperties.NoOfYears+2; i <= 6; i++)
            {
                if (i == 3)
                {
                    //Hide Y4 Year - Last Year
                    HideTableCell(xrtblGrpLedgerGroup, xrRowLG, xrGrpLGY3AcutualSum);
                    HideTableCell(xrtblLedger, XrValueRow, xrY3Actual);
                    HideTableCell(xrTblSum, xrRowSum, xrY3ActualSum);
                }
                else if (i == 4)
                {
                    //Hide Y3 Year - Last Year
                    HideTableCell(xrtblGrpLedgerGroup, xrRowLG, xrGrpLGY4AcutualSum);
                    HideTableCell(xrtblLedger, XrValueRow, xrY4Actual);
                    HideTableCell(xrTblSum, xrRowSum, xrY4ActualSum);
                }
                else if (i == 5)
                {
                    //Hide Y6 Year - Last Year
                    HideTableCell(xrtblGrpLedgerGroup, xrRowLG, xrGrpLGY5AcutualSum);
                    HideTableCell(xrtblLedger, XrValueRow, xrY5Actual);
                    HideTableCell(xrTblSum, xrRowSum, xrY5ActualSum);
                }
                else if (i == 6)
                {
                    //Hide Y5 Year - Last Year
                    HideTableCell(xrtblGrpLedgerGroup, xrRowLG, xrGrpLGY6AcutualSum);
                    HideTableCell(xrtblLedger, XrValueRow, xrY6Actual);
                    HideTableCell(xrTblSum, xrRowSum, xrY6ActualSum);
                }
            }

            if (this.ReportProperties.NoOfYears <= 3)
            {
                xrtblGrpLedgerGroup.WidthF = xrtblLedger.WidthF = xrTblSum.WidthF = 800;
            }
            else
            {
                xrtblGrpLedgerGroup.WidthF = xrtblLedger.WidthF = xrTblSum.WidthF = 1065;
                xrLedgerName.WidthF =  230;
                xrgrpLedgerGroup.WidthF = xrgrpLedgerGroupSubTotal.WidthF = xrCode.WidthF +  xrLedgerName.WidthF ;
            }
        }

        #endregion
    }
}

