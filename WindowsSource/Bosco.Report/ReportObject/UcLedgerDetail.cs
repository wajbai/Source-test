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
    public partial class UcLedgerDetail : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public UcLedgerDetail()
        {
            InitializeComponent();
        }
        #endregion

        #region Variables
        ResultArgs resultArgs = null;
       
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

        private void HideReportHeaderFooter()
        {
            this.HideReportHeader = false;
            this.HidePageFooter = false;
        }


        public void BindLedgerDetails(DataTable dtLegentLedger)
        {
            HideReportHeaderFooter();
            SetTitleWidth(xrtblLedgerGroup.WidthF);
            this.SetLandscapeHeader = xrtblLedgerGroup.WidthF;
            this.SetLandscapeFooter = xrtblLedgerGroup.WidthF;
            this.SetLandscapeFooterDateWidth = xrtblLedgerGroup.WidthF;

            this.DataSource = dtLegentLedger;
            this.DataMember = dtLegentLedger.TableName;
        }       
        #endregion

        #region Events
       
        #endregion

    }
}

