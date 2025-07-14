using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Bosco.Report.ReportObject
{
    public partial class CashBankReceiptsBase : Bosco.Report.Base.ReportHeaderBase
    {
        public CashBankReceiptsBase()
        {
            InitializeComponent();

            if (this.ReportProperties.ReportId == "RPT-151")
            {
                xrSubOrginal.ReportSource = new JournalContraVoucher();
                xrSubDuplicate.ReportSource = new JournalContraVoucher();
            }
        }

        #region ShowReport
        public override void ShowReport()
        {
            if (this.ReportProperties.VoucherPrintShowLogo == "1")
            {
                HideReportLogoLeft = true;
            }
            else
            {
                HideReportLogoLeft = false;
            }
            this.HideReportHeader = this.HidePageFooter = false;
            SetSubReportProperties();
            base.ShowReport();
        }

        public override void ShowPrintDialogue()
        {
            this.HideReportHeader = this.HidePageFooter = false;
            SetSubReportProperties();
            this.ShowPreviewDialog();
        }

        private void SetSubReportProperties()
        {
            string voucherid = ReportProperties.PrintCashBankVoucherId;
                      
        }
        #endregion

        private void xrSubOrginal_AfterPrint(object sender, EventArgs e)
        {
            Int32 TotalRecords = 0;
            bool MakePageBreak = false;
            XRSubreport subreportorginal = sender as XRSubreport;
          
            if (MakePageBreak)
            {
                xrdotline.Visible = false;
                this.xrPageBreak1.Visible = true;
            }    
        }

    }
}
