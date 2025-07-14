using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using Bosco.Report.Base;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class FDInterestQuarterwiseRegister : ReportHeaderBase
    {
        #region Declaration
        List<Tuple<String, String, Int32>> lstcolumnwidth = new List<Tuple<String, String, Int32>>();
        ResultArgs resultArgs = null;
        private double TotalGrandCollectedInterest = 0;
        private Dictionary<Int32, double> dicFDBalance = new Dictionary<Int32, double>();
        private DataTable dtRenewlInterestAmount = new DataTable();
        #endregion

        #region Constructor
        public FDInterestQuarterwiseRegister()
        {
            InitializeComponent();
            //On 27/08/2020
            this.AttachDrillDownToRecord(xrtblFdRegister, xrFDAcNoValue,
                        new ArrayList { this.ReportParameters.FD_ACCOUNT_IDColumn.ColumnName }, DrillDownType.LEDGER_VOUCHER, false, "VOUCHER_SUB_TYPE");
        }

        #endregion

        #region Show Reports
        public override void ShowReport()
        {
            dicFDBalance.Clear();
            FetchFDInterestRegister();
        }
        #endregion

        #region Methods
        public void FetchFDInterestRegister()
        {
            
            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo))
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        FetchFDInterestRegisterDetails();
                        base.ShowReport();
                    }
                    else
                    {
                        SetReportTitle();
                        ShowReportFilterDialog();
                    }
                }
                else
                {
                    FetchFDInterestRegisterDetails();
                    base.ShowReport();
                }
            }
        }

        public void FetchFDInterestRegisterDetails()
        {
            try
            {
                //  this.ReportTitle = ReportProperty.Current.ReportTitle;
                //this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
                this.SetLandscapeHeader = xrtblHeaderTable.WidthF;
                this.SetLandscapeFooter = xrtblHeaderTable.WidthF;
                this.SetLandscapeFooterDateWidth = 970.00f;
                setHeaderTitleAlignment();
                // this.ReportPeriod = String.Format("For the Period: {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
                SetReportTitle();
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                //grpBankHeader.Visible = (ReportProperty.Current.ShowByBank == 1);
                //grpBankFooter.Visible = (ReportProperty.Current.ShowByBank == 1);
                
                DateTime Q1FromDate = this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false);
               //Q1FromDate = this.UtilityMember.DateSet.ToDate(settingProperty.YearFrom, false);
                DateTime Q1ToDate = Q1FromDate.AddMonths(3).AddDays(-1);

                DateTime Q2FromDate = Q1FromDate.AddMonths(3);
                DateTime Q2ToDate = Q2FromDate.AddMonths(3).AddDays(-1);

                DateTime Q3FromDate = Q2FromDate.AddMonths(3);
                DateTime Q3ToDate = Q3FromDate.AddMonths(3).AddDays(-1);

                DateTime Q4FromDate = Q3FromDate.AddMonths(3);
                DateTime Q4ToDate = Q4FromDate.AddMonths(3).AddDays(-1);

                AssingRenewlInteretRange();
                string FDRegister = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.FetchFDInterestQuarterwiseRegister);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);

                    dataManager.Parameters.Add(this.ReportParameters.Q1_DATE_FROMColumn, Q1FromDate);
                    dataManager.Parameters.Add(this.ReportParameters.Q1_DATE_TOColumn, Q1ToDate);
                    dataManager.Parameters.Add(this.ReportParameters.Q2_DATE_FROMColumn, Q2FromDate);
                    dataManager.Parameters.Add(this.ReportParameters.Q2_DATE_TOColumn, Q2ToDate);
                    dataManager.Parameters.Add(this.ReportParameters.Q3_DATE_FROMColumn, Q3FromDate);
                    dataManager.Parameters.Add(this.ReportParameters.Q3_DATE_TOColumn, Q3ToDate);
                    dataManager.Parameters.Add(this.ReportParameters.Q4_DATE_FROMColumn, Q4FromDate);
                    dataManager.Parameters.Add(this.ReportParameters.Q4_DATE_TOColumn, Q4ToDate);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, FDRegister);

                    DataView dvCashFlow = resultArgs.DataSource.TableView;
                    xrSnoValue.Summary.Running = SummaryRunning.Group;
                    
                    if (dvCashFlow != null && dvCashFlow.Count != 0)
                    {
                        string filter = "(" + this.reportSetting1.FDRegister.Q1_INTEREST_AMOUNTColumn.ColumnName + " <> 0 OR " +
                                        this.reportSetting1.FDRegister.Q2_INTEREST_AMOUNTColumn.ColumnName + "<> 0 OR " +
                                        this.reportSetting1.FDRegister.Q3_INTEREST_AMOUNTColumn.ColumnName + "<> 0 OR " +
                                        this.reportSetting1.FDRegister.Q4_INTEREST_AMOUNTColumn.ColumnName + "<> 0 )";
                       //+
                       //                "(" + this.reportSetting1.FDRegister.RENEWAL_DATEColumn.ColumnName + " >= '" + this.ReportProperties.DateFrom + "' AND " + this.reportSetting1.FDRegister.RENEWAL_DATEColumn.ColumnName + "<='" + this.ReportProperties.DateTo + "') )";
                        dvCashFlow.RowFilter = filter;
                        DataTable dtReport =  ResetFDRenewals(dvCashFlow.ToTable());
                        dtReport.TableName = "FDRegister";
                        this.DataSource = dtReport;
                        this.DataMember = dtReport.TableName;
                        dvCashFlow.RowFilter = "";
                    }
                    else
                    {
                        dvCashFlow.Table.TableName = "FDRegister";
                        this.DataSource = dvCashFlow.Table;
                        this.DataMember = dvCashFlow.Table.TableName;

                        grpBankHeader.Visible = grpBankFooter.Visible = ReportFooter.Visible = Detail.Visible = false;  

                    }
                }
                SetReportBorder();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, false);
            }
            finally { }
        }

        private void AssingRenewlInteretRange()
        {
            string RnewalInterestRage = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.FetchFDInterestAmountByRenewalDate);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, RnewalInterestRage);
            }
            if (resultArgs.Success)
            {
                dtRenewlInterestAmount = resultArgs.DataSource.Table;
            }
        }
                

        private DataTable ResetFDRenewals(DataTable dtReport )
        {
            if (dtReport != null && dtReport.Rows.Count > 0)
            {
                foreach (DataRow dr in dtReport.Rows)
                {
                    Int32 fdid = UtilityMember.NumberSet.ToInteger(dr["FD_ACCOUNT_ID"].ToString());
                    if (fdid == 42)
                    {

                    }
                    string matdate = dr[reportSetting1.FDRegister.MATURITY_DATEColumn.ColumnName].ToString();
                    double fdamount = UtilityMember.NumberSet.ToDouble(dr[reportSetting1.FDRegister.PRINCIPLE_AMOUNTColumn.ColumnName].ToString());
                    double fdOpAccumulated = UtilityMember.NumberSet.ToDouble(dr["OP_ACCUMULATED_INTEREST_AMOUNT"].ToString());
                    double fdCurrentAccumulated = 0;

                    if (dtRenewlInterestAmount.Rows.Count > 0)
                    {
                        string filter = "FD_ACCOUNT_ID = " + fdid + " AND RENEWAL_DATE <'" + matdate + "'";
                        string fdcurrentacc = dtRenewlInterestAmount.Compute("SUM(ACCUMULATED_INTEREST_AMOUNT)", filter).ToString();
                        fdCurrentAccumulated = UtilityMember.NumberSet.ToDouble(fdcurrentacc);
                    }

                    string Q1RenewalHistory = dr["Q1_FD_TYPE_HISTORY"].ToString();
                    string Q2RenewalHistory = dr["Q2_FD_TYPE_HISTORY"].ToString();
                    string Q3RenewalHistory = dr["Q3_FD_TYPE_HISTORY"].ToString();
                    string Q4RenewalHistory = dr["Q4_FD_TYPE_HISTORY"].ToString();


                    dr.BeginEdit();
                    if (Q1RenewalHistory.IndexOf("RN") >= 0)
                    {
                        dr[reportSetting1.FDRegister.Q2_RENEWAL_DATEColumn.ColumnName] = DBNull.Value;
                        dr[reportSetting1.FDRegister.Q2_INTEREST_AMOUNTColumn.ColumnName] = 0;
                        dr[reportSetting1.FDRegister.Q2_TDSColumn.ColumnName] = 0;

                        dr[reportSetting1.FDRegister.Q3_RENEWAL_DATEColumn.ColumnName] = DBNull.Value;
                        dr[reportSetting1.FDRegister.Q3_INTEREST_AMOUNTColumn.ColumnName] = 0;
                        dr[reportSetting1.FDRegister.Q3_TDSColumn.ColumnName] = 0;

                        dr[reportSetting1.FDRegister.Q4_RENEWAL_DATEColumn.ColumnName] = DBNull.Value;
                        dr[reportSetting1.FDRegister.Q4_INTEREST_AMOUNTColumn.ColumnName] = 0;
                        dr[reportSetting1.FDRegister.Q4_TDSColumn.ColumnName] = 0;
                    }
                    else if (Q2RenewalHistory.IndexOf("RN") >= 0)
                    {
                        dr[reportSetting1.FDRegister.Q3_RENEWAL_DATEColumn.ColumnName] = DBNull.Value;
                        dr[reportSetting1.FDRegister.Q3_INTEREST_AMOUNTColumn.ColumnName] = 0;
                        dr[reportSetting1.FDRegister.Q3_TDSColumn.ColumnName] = 0;

                        dr[reportSetting1.FDRegister.Q4_RENEWAL_DATEColumn.ColumnName] = DBNull.Value;
                        dr[reportSetting1.FDRegister.Q4_INTEREST_AMOUNTColumn.ColumnName] = 0;
                        dr[reportSetting1.FDRegister.Q4_TDSColumn.ColumnName] = 0;
                    }
                    else if (Q3RenewalHistory.IndexOf("RN") >= 0)
                    {
                        dr[reportSetting1.FDRegister.Q4_RENEWAL_DATEColumn.ColumnName] = DBNull.Value;
                        dr[reportSetting1.FDRegister.Q4_INTEREST_AMOUNTColumn.ColumnName] = 0;
                        dr[reportSetting1.FDRegister.Q4_TDSColumn.ColumnName] = 0;
                    }

                    dr[reportSetting1.FDRegister.PRINCIPLE_AMOUNTColumn.ColumnName] = (fdamount + fdOpAccumulated + fdCurrentAccumulated);
                    dr.EndEdit();
                    dr.AcceptChanges();
                }

            }

            return dtReport;
        }

        private void SetReportBorder()
        {
            xrtblHeaderTable = AlignHeaderTable(xrtblHeaderTable);
            xrtblFdRegister = AlignContentTable(xrtblFdRegister);
            xrtblGrandTotal = AlignGrandTotalTable(xrtblGrandTotal);
            xrtblGrpBank = AlignGrandTotalTable(xrtblGrpBank);
            xrtblBankFooter = AlignGrandTotalTable(xrtblBankFooter);
            xrtblBankQuaterSummary = AlignGrandTotalTable(xrtblBankQuaterSummary);
            
            this.SetCurrencyFormat(xrQtl1.Text, xrQtl1);
            this.SetCurrencyFormat(xrQtl2.Text, xrQtl2);
            this.SetCurrencyFormat(xrQtl3.Text, xrQtl3);
            this.SetCurrencyFormat(xrQtl4.Text, xrQtl4);
            this.SetCurrencyFormat(xrPrincipalAmount.Text, xrPrincipalAmount);
            this.SetCurrencyFormat(xrActual.Text, xrActual);
        }

        #endregion
        
        
        private void xrSnoValue_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataRowView drvcrrentrow = (DataRowView)this.GetCurrentRow();
            if (drvcrrentrow == null)
            {
                xrSnoValue.Visible = false;
            }
            else
            {
                xrSnoValue.Visible = true;
            }
        }
        
        private void xrActualAmountValue_EvaluateBinding(object sender, BindingEventArgs e)
        {
            //double actualinterestamunt = 0;
            //if (GetCurrentColumnValue(reportSetting1.FDRegister.Q1_INTEREST_AMOUNTColumn.ColumnName)!=null)
            //    actualinterestamunt +=UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.FDRegister.Q1_INTEREST_AMOUNTColumn.ColumnName).ToString());
            //if (GetCurrentColumnValue(reportSetting1.FDRegister.Q2_INTEREST_AMOUNTColumn.ColumnName) != null)
            //    actualinterestamunt += UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.FDRegister.Q2_INTEREST_AMOUNTColumn.ColumnName).ToString());
            //if (GetCurrentColumnValue(reportSetting1.FDRegister.Q3_INTEREST_AMOUNTColumn.ColumnName) != null)
            //    actualinterestamunt += UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.FDRegister.Q3_INTEREST_AMOUNTColumn.ColumnName).ToString());
            //if (GetCurrentColumnValue(reportSetting1.FDRegister.Q4_INTEREST_AMOUNTColumn.ColumnName) != null)
            //    actualinterestamunt += UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.FDRegister.Q4_INTEREST_AMOUNTColumn.ColumnName).ToString());
            ////TotalGrandCollectedInterest += actualinterestamunt;

            //e.Value = UtilityMember.NumberSet.ToNumber(actualinterestamunt);
            /*if (GetCurrentColumnValue("FD_ACCOUNT_ID") != null)
            {
                e.Value = GetPreviousRenewalValue(false);
            }*/
        }

       
        private void xrPrincipalAmountValue_EvaluateBinding(object sender, BindingEventArgs e)
        {
            /*if (GetCurrentColumnValue("FD_ACCOUNT_ID") != null)
            {
                Int32 fdid = UtilityMember.NumberSet.ToInteger( GetCurrentColumnValue("FD_ACCOUNT_ID").ToString());
                string matdate = GetCurrentColumnValue(reportSetting1.FDRegister.MATURITY_DATEColumn.ColumnName).ToString();
                double fdbalance = 0;
                double fdamount = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.FDRegister.PRINCIPLE_AMOUNTColumn.ColumnName).ToString());
                double fdOpAccumulated = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("OP_ACCUMULATED_INTEREST_AMOUNT").ToString());
                double fdCurrentAccumulated = 0;

                if (dtRenewlInterestRate.Rows.Count > 0)
                {
                    string filter = "FD_ACCOUNT_ID = " + fdid + " AND RENEWAL_DATE <'" + matdate + "'";
                    string fdcurrentacc = dtRenewlInterestRate.Compute("SUM(ACCUMULATED_INTEREST_AMOUNT)", filter).ToString();
                    fdCurrentAccumulated = UtilityMember.NumberSet.ToDouble(fdcurrentacc);
                }
                
                if (!dicFDBalance.ContainsKey(fdid))
                {
                    fdbalance = fdamount+ fdOpAccumulated;
                    dicFDBalance.Add(fdid, (fdbalance + fdCurrentAccumulated));
                    
                }
                else
                {
                    fdbalance = dicFDBalance[fdid];
                    dicFDBalance[fdid] = (fdbalance + fdCurrentAccumulated);
                }
                e.Value = fdbalance;
            }*/
            
        }

        private void xrIntPercentageValue_EvaluateBinding(object sender, BindingEventArgs e)
        {
            /*if (GetCurrentColumnValue("FD_ACCOUNT_ID") != null)
            {
                e.Value = GetPreviousRenewalValue(true);
            }*/
        }

        private void xrQtl1AmountValue_EvaluateBinding(object sender, BindingEventArgs e)
        {
            HideZeroValue(e);
        }

        private void HideZeroValue(BindingEventArgs e)
        {
            if (GetCurrentColumnValue("FD_ACCOUNT_ID") != null)
            {
                double amt = this.ReportProperties.NumberSet.ToDouble(e.Value.ToString());
                if (amt == 0) //amt <= 0
                {
                    e.Value = "";
                }
            }
        }

        private void NoteIsLastQuaterRenewed(object sender, System.Drawing.Printing.PrintEventArgs e, string QuaterDate)
        {
            if (GetCurrentColumnValue("FD_ACCOUNT_ID") != null )
            {
                DateTime FDMatdate = UtilityMember.DateSet.ToDate(GetCurrentColumnValue(reportSetting1.FDRegister.MATURITY_DATEColumn.ColumnName).ToString(), false);
                string renewldate = GetCurrentColumnValue(QuaterDate).ToString();
                
                XRTableCell cell = (sender) as XRTableCell;
                if (FDMatdate == UtilityMember.DateSet.ToDate(cell.Text, false))
                {
                    cell.Font = new Font(cell.Font, FontStyle.Underline);
                }
                else
                {
                    cell.Font = new Font(cell.Font, FontStyle.Regular);
                }
                if (!string.IsNullOrEmpty(renewldate))
                {
                    DateTime FDRenewalDate = UtilityMember.DateSet.ToDate(renewldate, false);
                    cell.Text = FDRenewalDate.ToShortDateString();
                }
            }
        }

        private void xrQtl1TDSValue_EvaluateBinding(object sender, BindingEventArgs e)
        {
            HideZeroValue(e);
        }

        private void xrQtl2AmountValue_EvaluateBinding(object sender, BindingEventArgs e)
        {
            HideZeroValue(e);
        }

        private void xrQtl2TDSValue_EvaluateBinding(object sender, BindingEventArgs e)
        {
            HideZeroValue(e);
        }

        private void xrQtl3AmountVAlue_EvaluateBinding(object sender, BindingEventArgs e)
        {
            HideZeroValue(e);
        }

        private void xrQtl3TDSValue_EvaluateBinding(object sender, BindingEventArgs e)
        {
            HideZeroValue(e);
        }

        private void xrQtl4AmountValue_EvaluateBinding(object sender, BindingEventArgs e)
        {
            HideZeroValue(e);
        }

        private void xrQtl4TDSValue_EvaluateBinding(object sender, BindingEventArgs e)
        {
            HideZeroValue(e);
        }

        private void xrQtl1DateValue_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           NoteIsLastQuaterRenewed(sender, e, reportSetting1.FDRegister.Q1_RENEWAL_DATEColumn.ColumnName);
        }

        private void xrQtl2DateValue_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            NoteIsLastQuaterRenewed(sender, e, reportSetting1.FDRegister.Q2_RENEWAL_DATEColumn.ColumnName);
        }

        private void xrQtl3DateValue_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            NoteIsLastQuaterRenewed(sender, e, reportSetting1.FDRegister.Q3_RENEWAL_DATEColumn.ColumnName);
        }

        private void xrQtl4DateValue_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            NoteIsLastQuaterRenewed(sender, e, reportSetting1.FDRegister.Q4_RENEWAL_DATEColumn.ColumnName);
        }

        private void xrsumTotalInterestAmount_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (GetCurrentColumnValue("FD_ACCOUNT_ID") != null)
            {
                if (this.DataSource != null)
                {
                    DataTable dtBind = this.DataSource as DataTable;
                    string smuoftotal = "SUM(" + reportSetting1.FDRegister.Q1_INTEREST_AMOUNTColumn.ColumnName + ") + " +
                                        "SUM(" + reportSetting1.FDRegister.Q2_INTEREST_AMOUNTColumn.ColumnName + ") + " +
                                        "SUM(" + reportSetting1.FDRegister.Q3_INTEREST_AMOUNTColumn.ColumnName + ") + " +
                                        "SUM(" + reportSetting1.FDRegister.Q4_INTEREST_AMOUNTColumn.ColumnName + ")";

                    e.Value = dtBind.Compute(smuoftotal, string.Empty);
                }
            }
        }

    }
}
