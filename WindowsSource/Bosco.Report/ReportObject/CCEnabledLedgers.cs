using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Report.Base;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class CCEnabledLedgers : Bosco.Report.Base.ReportHeaderBase
    {
        public CCEnabledLedgers()
        {
            InitializeComponent();
        }

        #region ShowReport
        public override void ShowReport()
        {
            BindCCEnabledLedgers();
        }

        private void BindCCEnabledLedgers()
        {
            SetReportTitle();
            if (this.UIAppSetting.UICustomizationForm == "1")
            {
                if (ReportProperty.Current.ReportFlag == 0)
                {
                    SplashScreenManager.ShowForm(typeof(frmReportWait));
                    BindReportData();
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
                else
                {
                    ShowReportFilterDialog();
                    SetReportBorder();
                }
            }
            else
            {
                SplashScreenManager.ShowForm(typeof(frmReportWait));
                BindReportData();
                SplashScreenManager.CloseForm();
                base.ShowReport();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private ResultArgs BindReportData()
        {
            ResultArgs resultArgs = new ResultArgs(); ;
            string sqlCCEnabledLedgers = this.GetReportSQL(SQL.ReportSQLCommand.Report.CCEnabledLedgerList);

            using (DataManager dataManager = new DataManager())
            {   
                //dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlCCEnabledLedgers);
            }
             
            if (resultArgs.Success)
            {
                DataTable dtsqlCCEnabledLedgers = resultArgs.DataSource.Table;

                dtsqlCCEnabledLedgers.TableName = "CCEnabledLedgers";
                this.DataSource = dtsqlCCEnabledLedgers;
                this.DataMember = dtsqlCCEnabledLedgers.TableName;

                Detail.Visible = grpHeaderLedgerName.Visible = (dtsqlCCEnabledLedgers.Rows.Count > 0);
                
            }
            else
            {
                MessageRender.ShowMessage(resultArgs.Message);
            }
            SetReportBorder();
            return resultArgs;
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetReportBorder()
        {
            AlignGroupTable(xrTblCCVoucherMade);
            AlignContentTable(xrTblGrpLedgerName);
            AlignContentTable(xrtblCCDetails);

            this.HideReportSubTitle = true;
            this.HideDateRange = false;
            this.HideCostCenter = true;
            this.HideBudgetName = true;
        }

        public void AlignContentTable(XRTable table)
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
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right;
                        }
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;

                }
            }
            
        }
        #endregion
        
       

        private bool ISLedgerCCMadeVouchers()
        {
            bool rnt = true;
            if (GetCurrentColumnValue(reportSetting1.Ledger.RECORD_EXISTSColumn.ColumnName) != null)
            {
                Int32 ccexists = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.Ledger.RECORD_EXISTSColumn.ColumnName).ToString());
                if (ccexists == 0)
                {
                    rnt = false;
                }
            }
            return rnt;
        }
               

        private void grpHeaderVoucherMade_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            grpHeaderCCName.Visible = true;
            if (!ISLedgerCCMadeVouchers())
            {
                grpHeaderCCName.Visible = false;
            }
        }
               

        private void xrCCVooucherMode_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.Ledger.RECORD_EXISTSColumn.ColumnName) != null)
            {
                Int32 ccexists = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.Ledger.RECORD_EXISTSColumn.ColumnName).ToString());
                XRTableCell cell = sender as XRTableCell;
                cell.Text = (ccexists == 0 ? "Cost Centre enabled Ledgers with no Vouchers" : "Cost Centre enabled Ledgers with Vouchers");
                
            }
        }
    }
}
