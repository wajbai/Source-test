/*  Class Name      : IReport.cs
 *  Purpose         : Interface to ReportBase class object
 *  Author          : CS
 *  Created on      : 20-Jul-2009
 */

using System;
using System.Collections.Generic;
using DevExpress.XtraGrid.Views.Grid;
using System.Data;
using DevExpress.XtraEditors;

namespace Bosco.Report.Base
{
    public interface IReport
    {
        void ResetReportPropertyTransYearChange();//Reset Report properties when change transaction period
        void ShowReport();
        void ShowStandardReport(GridView gv, string reporttitle, GridView gvAdditional = null, string Additionalreporttitle = "");    //Generate Standard reports for all grid view
        void VoucherPrint(string VoucherId, string VoucherType, string ProjectName, Int32 ProjectId, Int32 InOutId = 0, string module = "");
        void ShowChequePrint(Int32 VoucherId, Int32 BankId = 0);
        void ShowPrintView(DataTable dtDataSouce, string ReportId);
        DevExpress.XtraReports.UI.XtraReport GetReport();
        //void ShowBudgetView(Int32 BudgetId, string BudgetName);
        void ShowBudgetView1(Int32 BudgetId, string BudgetProjectIds, string BudgetName, GridView gv);
        void ShowDepreciationCalculation(int DeprId, DateTime dtfrom, DateTime dtTo, int projectId, XtraForm frm);
        void ShowBudgetMonthDistribution(Int32 BudgetId, string ProjectId, GridView gv, DateTime Yeafrom, DateTime Yearto, DateTime Monthfrom, DateTime MonthTo, string BudgetName);
        void ShowBudgetExpenseApprovalByMonth(string BudgetId, string ProjectId, bool MonthbyTwo, GridView gv, string datefrom, string dateto, string BudgetName, string ProjectName);
    }
}
