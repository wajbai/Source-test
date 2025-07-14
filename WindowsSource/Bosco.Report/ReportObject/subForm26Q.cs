using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Bosco.DAO.Data;
using Bosco.Utility;

namespace Bosco.Report.ReportObject
{
    public partial class subForm26Q : Bosco.Report.Base.ReportBase
    {
        public subForm26Q()
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
