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
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class UcTOC : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public UcTOC()
        {
            InitializeComponent();
        }
        #endregion

        #region Variables
        ResultArgs resultArgs = null;
        

        public float CCTableWidth
        {
            set
            {
                xrtblDetails.WidthF = value;
                xrTblTocHeader.WidthF = value;
            }
        }

        #endregion

        #region ShowReport
        //public override void ShowReport()
        //{
        //    LedgerDebit = 0;
        //    LedgerCredit = 0;
        //    BindConsolidatedStatement();
        //}
        #endregion

        #region Methods

        public void HideReportHeaderFooter()
        {
            this.HideReportHeader = false;
            this.HidePageFooter = false;
        }


        public void BindCCDetails(DataTable dtTOCDetails)
        {
            //NoOfTOCPages = noofTOCpages;
            SetTitleWidth(xrtblDetails.WidthF);
            this.SetLandscapeHeader = xrtblDetails.WidthF;
            this.SetLandscapeFooter = xrtblDetails.WidthF;
            this.SetLandscapeFooterDateWidth = xrtblDetails.WidthF;
            SetReportTitle();

            BindProperty(dtTOCDetails);
            setReportBorder();
        }

        private void BindProperty(DataTable dtTOCDetails)
        {
            SetReportHeaderFooterSetting();
            SetReportTitle();
            setHeaderTitleAlignment();

            this.HidePageFooter = false;

            // To show by costcentre ends
            if (dtTOCDetails != null)
            {
                dtTOCDetails.TableName = this.DataMember;
                this.DataSource = dtTOCDetails;
                this.DataMember = dtTOCDetails.TableName;
            }
            else
            {
                this.DataSource = null;
            }
        }

        private void setReportBorder()
        {
            xrtblDetails = AlignTotalTable(xrtblDetails);
        }
        #endregion

        #region Events
        private void xrCellPageNo_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (this.GetCurrentColumnValue(reportSetting1.TOC.PAGE_NOColumn.ColumnName) != null)
            {
                Int32 pageno = UtilityMember.NumberSet.ToInteger(this.GetCurrentColumnValue(reportSetting1.TOC.PAGE_NOColumn.ColumnName).ToString());
                Int32 realPno = (e.PageCount - pageno);

                XRTableCell cell = sender as XRTableCell;
                Int32 no = UtilityMember.NumberSet.ToInteger(cell.Text);
                cell.Text = (no + this.NoOfTOCPages).ToString();               
            }
        }
       
        #endregion

        private void xrCellPageNo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.GetCurrentColumnValue(reportSetting1.TOC.PAGE_NOColumn.ColumnName) != null)
            {
                XRControl label = sender as XRControl;
                label.Tag = String.Format("Link_{0}", this.GetCurrentColumnValue(reportSetting1.TOC.TOC_NAMEColumn.ColumnName));
            }
        }

        private void xrCellTOCName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.GetCurrentColumnValue(reportSetting1.TOC.PAGE_NOColumn.ColumnName) != null)
            {
                XRControl label = sender as XRControl;
                label.Tag = String.Format("Link_{0}", this.GetCurrentColumnValue(reportSetting1.TOC.TOC_NAMEColumn.ColumnName));
            }
        }

    }
}

