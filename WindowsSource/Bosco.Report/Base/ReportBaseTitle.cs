using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Bosco.Report.Base
{
    public partial class ReportBaseTitle : ReportBase
    {
        public ReportBaseTitle()
        {
            InitializeComponent();

            xrlblInstitute.Text = "Don Bosco Center, Yellagiri Hills";
        }
        
        public string ReportTitle
        {
            set { xrlblReportTitle.Text = value; }
        }

        public string ReportSubTitle
        {
            set { xrlblReportSubTitle.Text = value; }
        }
        
        public bool ShowPrintOn
        {
            set { xrDateInfo.Visible = value; }
        }
    }
}
