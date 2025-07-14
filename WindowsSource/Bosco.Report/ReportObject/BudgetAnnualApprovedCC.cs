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
    public partial class BudgetAnnualApprovedCC : Bosco.Report.Base.ReportHeaderBase
    {

        string BudgetTransMode = string.Empty;
        #region Constructor
        public BudgetAnnualApprovedCC()
        {
            InitializeComponent();
        }
        #endregion

        #region Variables
        public float CCTableWidth
        {
            set
            {
                xrtblHeaderCaption.WidthF = value;
                xrTblCC.WidthF = value;
                xrtblCCCName.WidthF = value;
                lblBreak.WidthF = value;
            }
        }
        
        public float CodeWidth
        {
            set
            {
                xrcellCCAbbrevation.WidthF = value;
                xrcellCCCEmpty.WidthF = value;
            }
        }

        public float CCNameWidth
        {
            set
            {
                xrcellCCName.WidthF = value;
            }
        }

        public float CCPrevApprovedAmount
        {
            set
            {
                xrcellPrevCCApproved.WidthF = value;
            }
        }


        public float CCPrevRelaized
        {
            set
            {
                xrcellPrevCCRealized.WidthF = value;
            }
        }

       
        public float CCProposedAmount
        {
            set
            {
                xrcellCCProposed.WidthF = value;
            }
        }

        public float CCApprovedAmount
        {
            set
            {
                xrcellCCApproved.WidthF = value;
            }
        }

        public float CCNote
        {
            set
            {
                xrCellCCNote.WidthF = value;
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


        public void BindCCDetails(DataTable dtCCDetails)
        {
            HideReportHeaderFooter();
            SetTitleWidth(xrtblHeaderCaption.WidthF);
            this.SetLandscapeHeader = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooter = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooterDateWidth = xrtblHeaderCaption.WidthF;
                       
            BindProperty(dtCCDetails);
            setReportBorder();
        }

        private void BindProperty(DataTable dtCCDetails)
        {
            setHeaderTitleAlignment();
            SetReportTitle();
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            this.CosCenterName = null;
            this.HideReportHeader = false;

            //On 03/02/2025 - Show CC Category ----------------------------------------------------------------------------------------------------------
            grHeaderCCCategory.Visible = false;
            grHeaderCCCategory.GroupFields[0].FieldName = string.Empty;
            if (this.AppSetting.EnableCostCentreBudget == 1 && this.ReportProperties.ShowCCDetails == 1 && this.ReportProperties.ShowByCostCentreCategory == 1)
            {
                grHeaderCCCategory.Visible = true;
                grHeaderCCCategory.GroupFields[0].FieldName = reportSetting1.BUDGETVARIANCE.COST_CENTRE_CATEGORY_NAMEColumn.ColumnName;//COST_CENTRE_CATEGORY_NAME";
            }
            //---------------------------------------------------------------------------------------------------------------------------------------------
            // To show by costcentre ends
            PageHeader.Visible = false;
            if (dtCCDetails != null && dtCCDetails.Rows.Count != 0)
            {
                dtCCDetails.TableName = "CCDetails";
                this.DataSource = dtCCDetails;
                this.DataMember = dtCCDetails.TableName;
                PageHeader.Visible = false;
                Detail.Visible = true;
                ReportFooter.Visible = true;
            }
            else
            {
                this.DataSource = null;
            }
        }

        private void setReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption, true);
            xrTblCC = AlignContentTable(xrTblCC);
            xrtblCCCName = AlignContentTable(xrtblCCCName);
            
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

     

        private void xrCellCCPercentage_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataRowView drvcrrentrow = (DataRowView)this.GetCurrentRow();
            if (drvcrrentrow != null)
            {                
                Double approvedamount = UtilityMember.NumberSet.ToDouble(drvcrrentrow["APPROVED_AMOUNT"].ToString());
                Double actualamount = UtilityMember.NumberSet.ToDouble(drvcrrentrow["ACTUAL_AMOUNT"].ToString());
                Double varianceamountamount = UtilityMember.NumberSet.ToDouble(drvcrrentrow["BUDGET_VARIANCE"].ToString());
                Double Percentage_number = UtilityMember.NumberSet.ToDouble(drvcrrentrow["PERCENTAGE_NUMBER"].ToString());

                XRTableCell xrpercentagecell = ((XRTableCell)sender);
                xrpercentagecell.Font = new Font(xrpercentagecell.Font, FontStyle.Regular);
                xrpercentagecell.ForeColor = Color.Black;
                if (BudgetTransMode.ToUpper() == TransMode.CR.ToString().ToUpper())
                {
                    if (actualamount > approvedamount)
                    {
                        xrpercentagecell.Font = new Font(xrpercentagecell.Font, FontStyle.Bold);
                        xrpercentagecell.ForeColor = Color.Green;
                    }
                }
                else
                {
                    if (actualamount > approvedamount)
                    {
                        xrpercentagecell.Font = new Font(xrpercentagecell.Font, FontStyle.Bold);
                        xrpercentagecell.ForeColor = Color.Red;
                    }
                }

                if (varianceamountamount == 0)
                {
                    //On 12/02/2002
                    xrpercentagecell.Text = "0%";
                }
            }
        }

        private void grHeaderCCCategory_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void xrcellCCCEmpty_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrcellCCCEmpty.Borders = BorderSide.Left | BorderSide.Right;
        }


        #region Events
       
       
        #endregion

    }
}

