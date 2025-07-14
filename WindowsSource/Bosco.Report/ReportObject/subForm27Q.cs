using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Bosco.Report.ReportObject
{
    public partial class subForm27Q : Bosco.Report.Base.ReportBase
    {
        public subForm27Q()
        {
            InitializeComponent();
        }

        public override void ShowReport()
        {
            BindDetails(null);
            base.ShowReport();
        }

        public void BindDetails(DataTable dtForm26Q)
        {
            if (dtForm26Q != null)
            {
                this.DataSource = dtForm26Q;
                this.DataMember = dtForm26Q.TableName;
            }
        }
    }
}
