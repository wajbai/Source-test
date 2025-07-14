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
    public partial class ReceiptPayment : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public ReceiptPayment()
        {
            InitializeComponent();
        }
        #endregion

        #region Decelartion


        #endregion

        #region ShowReport

        public override void ShowReport()
        {
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom) || String.IsNullOrEmpty(this.ReportProperties.DateTo))
            {
                ShowReportFilterDialog();
            }
            else
            {
                //   BindReceiptSource();
            }

            base.ShowReport();
        }

        #endregion

        #region Methods
        
        #endregion

    }
}
