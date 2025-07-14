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
    public partial class UcCCDonorDetail : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public UcCCDonorDetail()
        {
            InitializeComponent();
        }
        #endregion

        #region Variables
        ResultArgs resultArgs = null;
        double LedgerDebit = 0;
        double LedgerCredit = 0;
        int ResetTotal = 0;

        public float DonorTableWidth
        {
            set
            {
                xrtblHeaderCaption.WidthF = value;
                xrTblDonorDetail.WidthF = value;
                xrTblLegerSum.WidthF = value;
                xrTblProjectDetails.WidthF = value;
            }
        }

        public float ProjectNameWidth
        {
            set
            {
                xrCellProject.WidthF = value; ;
            }
        }

        public float DateWidth
        {
            set
            {
                xrCapDate.WidthF = value;
                xrDate.WidthF = value; ;
            }
        }

        public float DonorNameWidth
        {
            set
            {
                xrCapDonorName.WidthF = value; // -2;
                xrCellDonorName.WidthF = value; ;
                xrCellCCLedgerSumName.WidthF = value; ;
            }
        }

        public float DonorCreditWidth
        {
            set
            {
                if (xrRowCaption.Cells.Contains(xrCapCredit)) xrCapCredit.WidthF = value;
                if (xrRowCC.Cells.Contains(xrcellCCCredit)) xrcellCCCredit.WidthF = value;
                if (xrRowCCLedgerSum.Cells.Contains(xrcellCCLedgerCreditSum)) xrcellCCLedgerCreditSum.WidthF = value;
            }
        }

        public float DonorDebitWidth
        {
            set
            {
                xrCapDebit.WidthF = value;
                xrcellDonorDebit.WidthF = value; ;
                xrcellCCLedgerDebitSum.WidthF = value; ;
            }
        }

        public string DonorDebitCaption
        {
            set
            {
                xrCapDebit.Text = value;
            }
        }

        public string DonorCreditCaption
        {
            set
            {
                xrCapCredit.Text = value;
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


        public void BindDonorDetails(DataTable dtDonorDetails, bool IsMonthlyAbstract)
        {
            SetTitleWidth(xrtblHeaderCaption.WidthF);
            this.SetLandscapeHeader = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooter = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooterDateWidth = xrtblHeaderCaption.WidthF;

            if (!IsMonthlyAbstract)
            {
                xrtblHeaderCaption.SuspendLayout();
                if (xrRowCaption.Cells.Contains(xrCapCredit))
                    xrRowCaption.Cells.Remove(xrRowCaption.Cells[xrCapCredit.Name]);
                xrtblHeaderCaption.PerformLayout();

                xrTblDonorDetail.SuspendLayout();
                if (xrRowCC.Cells.Contains(xrcellCCCredit))
                    xrRowCC.Cells.Remove(xrRowCC.Cells[xrcellCCCredit.Name]);
                xrTblDonorDetail.PerformLayout();

                xrtblDetails.SuspendLayout();
                if (xrRowCCDetails.Cells.Contains(xrCrdit))
                    xrRowCCDetails.Cells.Remove(xrRowCCDetails.Cells[xrCrdit.Name]);
                xrtblDetails.PerformLayout();

                xrTblLegerSum.SuspendLayout();
                if (xrRowCCLedgerSum.Cells.Contains(xrcellCCLedgerCreditSum))
                    xrRowCCLedgerSum.Cells.Remove(xrRowCCLedgerSum.Cells[xrcellCCLedgerCreditSum.Name]);
                xrTblLegerSum.PerformLayout();
            }

            BindProperty(dtDonorDetails);
            setReportBorder();
        }

        private void BindProperty(DataTable dtDonorDetails)
        {
            setHeaderTitleAlignment();
            SetReportTitle();
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            this.CosCenterName = null;
            this.HideReportHeader = false;

            //if (this.ReportProperties.ShowByCostCentreCategory == 1)
            //{
            //    GrpCostCentreCategoryName.Visible = true;
            //}
            //else
            //{
            //    GrpCostCentreCategoryName.Visible = GrpCostCentreCategoryAmount.Visible = false;
            //    grpLedgerName.Visible = Detail.Visible = true;
            //}

           
            // To show by costcentre ends
            PageHeader.Visible = grpHeaderCC.Visible =grpFooterLedgerName.Visible= grpProjectHeader.Visible = false;
            if (dtDonorDetails != null && dtDonorDetails.Rows.Count != 0)
            {
                dtDonorDetails.TableName = "Ledger";
                this.DataSource = dtDonorDetails;
                this.DataMember = dtDonorDetails.TableName;
                PageHeader.Visible = grpHeaderCC.Visible = grpFooterLedgerName.Visible = grpProjectHeader.Visible = true;
            }
            else
            {
                this.DataSource = null;
            }

            //On 06/10/2021, to show poject wise if and if only more than one projects are seleceted
            if ((string.IsNullOrEmpty(this.ReportProperties.Project) || this.ReportProperties.Project == "0" || this.ReportProperties.Project.Split(',').Length==1)   
                && this.ReportProperties.ShowDonorDetails== 1)
            {
                grpProjectHeader.GroupFields[0].FieldName = string.Empty;
                grpProjectHeader.Visible = false;
            }
        }

        private void setReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption, true);
            xrtblDetails = AlignContentTable(xrtblDetails);
            xrTblDonorDetail = AlignContentTable(xrTblDonorDetail);
            xrTblProjectDetails = AlignContentTable(xrTblProjectDetails);
            
            //xrtblLedger = AlignCCCategoryTable(xrtblLedger);
            //xrtblCCCName = AlignCCCategoryTable(xrtblCCCName);
            this.SetCurrencyFormat(xrCapDebit.Text, xrCapDebit);
            this.SetCurrencyFormat(xrCapCredit.Text, xrCapCredit);
        }
        
        private XRTable AlignCCCategoryTable(XRTable table)
        {
            int j = table.Rows.Count;
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                        {
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                        }
                        else if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Bottom;
                            if (count == trow.Cells.Count)
                            {
                                tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            }
                        }
                        else if (count == trow.Cells.Count)
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }

                        else
                        {
                            tcell.Borders = BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                        {
                            tcell.Borders = BorderSide.Left;
                        }
                        else if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;

                }
            }
            return table;
        }
        #endregion

        #region Events
       
        private void xrLedgerDebitBalance_SummaryReset(object sender, EventArgs e)
        {
            LedgerDebit = LedgerCredit = 0;
        }
        #endregion

    }
}

