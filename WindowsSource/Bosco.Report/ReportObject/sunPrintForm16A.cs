using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Bosco.Report.ReportObject
{
    public partial class sunPrintForm16A : Bosco.Report.Base.ReportBase
    {
        public sunPrintForm16A()
        {
            InitializeComponent();
        }

        public override void ShowReport()
        {
            BindDetails(null);
            base.ShowReport();
        }

        public void BindDetails(DataTable dtForm16A)
        {
            if (dtForm16A != null)
            {
                this.DataSource = dtForm16A;
                this.DataMember = dtForm16A.TableName;
            }
        }
    }
}
