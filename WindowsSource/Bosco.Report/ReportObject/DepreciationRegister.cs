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
    public partial class DepreciationRegister : ReportHeaderBase
    {
        #region Declaration
        List<Tuple<String, String, Int32>> lstcolumnwidth = new List<Tuple<String, String, Int32>>();
        ResultArgs resultArgs = null;
        #endregion

        #region Property
        public const string METHOD_ID = "METHOD_ID";
        public const string LIFE_YRS = "LIFE_YRS";
        public const string SALVAGE_VALUE = "SALVAGE_VALUE";
        public const string PREV_COST = "PREV_COST";
        public const string CUR_COST = "CUR_COST";
        public const string TOTAL_VALUE = "TOTAL_VALUE";
        public const string DEPRECIATION_PERCENTAGE = "DEPRECIATION_PERCENTAGE";
        public const string DEPRECIATION_VALUE = "DEPRECIATION_VALUE";
        public const string BALANCE_AMOUNT = "BALANCE_AMOUNT";
        public const string DATE_OF_APPLY_FROM = "DATE_OF_APPLY";
        public const string DATE_OF_APPLY_TO = "DATE_OF_APPLY_TO";
        public const string NO_MONTHS = "NO_MONTHS";
        #endregion
        #region Constructor
        public DepreciationRegister()
        {
            InitializeComponent();
            this.AttachDrillDownToRecord(xrtblFdRegister, xrFDAcNoValue,
                        new ArrayList { this.ReportParameters.FD_ACCOUNT_IDColumn.ColumnName }, DrillDownType.LEDGER_VOUCHER, false, "VOUCHER_SUB_TYPE");
        }

        #endregion

        #region Show Reports
        public override void ShowReport()
        {
            FetchFDRegister();
        }
        #endregion

        #region Methods
        public void FetchFDRegister()
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
                        FetchDepreciationRegisterDetails();
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
                    FetchDepreciationRegisterDetails();
                    base.ShowReport();
                }
            }
        }

        public void FetchDepreciationRegisterDetails()
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

                string DeprRegister = this.GetAssetReports(SQL.ReportSQLCommand.Asset.DepreciationRegister);
                using (DataManager dataManager = new DataManager())
                {
                    //dataManager.Parameters.Add(this.ReportParameters
                    dataManager.Parameters.Add(this.ReportParameters.DEPRECIATION_IDColumn, this.ReportProperties.ShowIndividualDepreciationLedgers);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    //  resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, DeprRegister);
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, DeprRegister);

                    AttachShowBy();

                    DataTable rtnSource = SetDeprMethodDetails(resultArgs.DataSource.Table);

                    DataView dvDepreciation = rtnSource.DefaultView; //resultArgs.DataSource.TableView;


                    dvDepreciation.Table.TableName = "Depreciation";
                    this.DataSource = dvDepreciation;
                    this.DataMember = dvDepreciation.Table.TableName;
                    grpBankFooter.Visible = false;
                }
                SetReportBorder();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, false);
            }
            finally { }
        }

        private DataTable SetDeprMethodDetails(DataTable dtSource)
        {
            //gvDepreciation.UpdateCurrentRow();
            DataTable dtWDV = dtSource;
            if (dtWDV != null && dtWDV.Rows.Count > 0)
            {
                int MethodID = 0;
                double LifeYrs = 0;
                double SalvageValue = 0;
                double PrvAssetValue = 0;
                double CurAssetValue = 0;
                double DepPercent = 0;
                double DepValue = 0;
                double NoofMonths = 0;

                int ItemStatus = 1;

                double Salvalue = 0;
                double LifeValue = 0;
                double CurTotalValue = 0;
                double CurDepValue = 0;

                int CurPeriodValue = 0;
                int CurPeriodBalance = 0;

                DateTime dtePeriodFrom = this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false);
                DateTime dtDBPeriodFrom = this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false);
                DateTime dtePeriodTo = this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateTo, false);

                foreach (DataRow dr in dtWDV.Rows)
                {
                    MethodID = 0;
                    LifeYrs = SalvageValue =
                         PrvAssetValue = CurAssetValue = DepPercent = DepValue = NoofMonths = Salvalue = LifeValue = CurTotalValue = CurDepValue = 0;
                    ItemStatus = UtilityMember.NumberSet.ToInteger(dr["STATUS"].ToString());
                    // Depreciation Apply To
                    if (!dtWDV.Columns.Contains(DATE_OF_APPLY_TO))
                        dtWDV.Columns.Add(DATE_OF_APPLY_TO, typeof(DateTime));
                    dr[DATE_OF_APPLY_TO] = dtePeriodTo.ToShortDateString();

                    // Depreciation Apply To
                    //if (!dtWDV.Columns.Contains(DATE_OF_APPLY_FROM))
                    //    dtWDV.Columns.Add(DATE_OF_APPLY_FROM, typeof(DateTime));
                    //dr[DATE_OF_APPLY_FROM] = dtePeriodFrom.ToShortDateString();
                    dtDBPeriodFrom = UtilityMember.DateSet.ToDate(dr[DATE_OF_APPLY_FROM].ToString(), false);
                    // Depreciation No Of Months
                    if (!dtWDV.Columns.Contains(NO_MONTHS))
                        dtWDV.Columns.Add(NO_MONTHS, typeof(double));
                    var diffMonths = (dtePeriodTo.Month + dtePeriodTo.Year * 12) - (dtDBPeriodFrom.Month + dtDBPeriodFrom.Year * 12) + 1;
                    NoofMonths = UtilityMember.NumberSet.ToDouble(diffMonths.ToString());
                    dr[NO_MONTHS] = NoofMonths.ToString();
                    MethodID = UtilityMember.NumberSet.ToInteger(dr[METHOD_ID].ToString());

                    //  dr[METHOD_ID] = setting.ShowDepr == "0" ? (int)DepreciationMethods.WDV : (int)DepreciationMethods.SLV;
                    dr[METHOD_ID] = (int)DepreciationMethods.SLV;


                    LifeYrs = UtilityMember.NumberSet.ToInteger(dr[LIFE_YRS].ToString());
                    SalvageValue = UtilityMember.NumberSet.ToDouble(dr[SALVAGE_VALUE].ToString());
                    SalvageValue = SalvageValue == 0 ? 1 : SalvageValue;
                    PrvAssetValue = UtilityMember.NumberSet.ToDouble(dr[PREV_COST].ToString());
                    CurAssetValue = UtilityMember.NumberSet.ToDouble(dr[CUR_COST].ToString());
                    DepPercent = UtilityMember.NumberSet.ToDouble(dr[DEPRECIATION_PERCENTAGE].ToString());
                    CurTotalValue = UtilityMember.NumberSet.ToDouble(dr[TOTAL_VALUE].ToString());

                    if (MethodID == (int)DepreciationMethods.WDV)// && DepreciationId.Equals(0))  // && DepPercent.Equals(0)
                    {
                        // Chinna 23.03.2021

                        MethodID = (int)DepreciationMethods.WDV;

                        DepValue = Math.Abs((((CurTotalValue * DepPercent) / 100)));
                        CurDepValue = UtilityMember.NumberSet.ToDouble(diffMonths.ToString()) / 12.00;

                        CurPeriodValue = UtilityMember.NumberSet.ToInteger(Math.Abs(Math.Round(CurDepValue * DepValue)).ToString());
                        CurPeriodBalance = UtilityMember.NumberSet.ToInteger(Math.Abs(Math.Round(CurTotalValue - CurPeriodValue)).ToString());
                        dr[DEPRECIATION_VALUE] = String.Format("{0:0.00}", CurPeriodValue);
                        dr[BALANCE_AMOUNT] = String.Format("{0:0.00}", CurPeriodBalance);
                        dr[METHOD_ID] = MethodID;

                        //if (DepPercent == 0)
                        //{
                        //    Salvalue = ((SalvageValue) / (PrvAssetValue + CurAssetValue));
                        //    LifeValue = (1 / LifeYrs);
                        //    DepPercent = ((1 - (Math.Pow(Salvalue, LifeValue))) * 100);
                        //    dr[DEPRECIATION_PERCENTAGE] = String.Format("{0:0.00}", Math.Abs(DepPercent));
                        //}
                        //DepValue = Math.Abs((((CurTotalValue * DepPercent) / 100)));
                        //CurDepValue = UtilityMember.NumberSet.ToDouble(diffMonths.ToString()) / 12.00;

                        //CurPeriodValue = UtilityMember.NumberSet.ToInteger(Math.Abs(Math.Round(CurDepValue * DepValue)).ToString());
                        //CurPeriodBalance = UtilityMember.NumberSet.ToInteger(Math.Abs(Math.Round(CurTotalValue - CurPeriodValue)).ToString());
                        //dr[DEPRECIATION_VALUE] = String.Format("{0:0.00}", CurPeriodValue);
                        //dr[BALANCE_AMOUNT] = String.Format("{0:0.00}", CurPeriodBalance);
                    }
                    else if (MethodID == (int)DepreciationMethods.SLV)
                    {
                        DepValue = Math.Abs((((CurTotalValue * DepPercent) / 100)));
                        CurDepValue = UtilityMember.NumberSet.ToDouble(diffMonths.ToString()) / 12.00;

                        CurPeriodValue = UtilityMember.NumberSet.ToInteger(Math.Abs(Math.Round(CurDepValue * DepValue)).ToString());
                        CurPeriodBalance = UtilityMember.NumberSet.ToInteger(Math.Abs(Math.Round(CurTotalValue - CurPeriodValue)).ToString());
                        dr[DEPRECIATION_VALUE] = String.Format("{0:0.00}", CurPeriodValue);
                        dr[BALANCE_AMOUNT] = String.Format("{0:0.00}", CurPeriodBalance);

                        //if (DepPercent == 0)
                        //{
                        //    //Salvalue = ((PrvAssetValue + CurAssetValue) - SalvageValue) / LifeYrs;
                        //    //DepValue = ((Salvalue) / 12) * NoofMonths;

                        //    //CurPeriodValue = UtilityMember.NumberSet.ToInteger(Math.Abs(Math.Round(DepValue)).ToString());
                        //    //CurPeriodBalance = UtilityMember.NumberSet.ToInteger(Math.Abs(Math.Round(CurTotalValue - CurPeriodValue)).ToString());
                        //    //dr[DEPRECIATION_VALUE] = String.Format("{0:0.00}", CurPeriodValue);
                        //    //dr[BALANCE_AMOUNT] = String.Format("{0:0.00}", CurPeriodBalance);

                        //    Salvalue = ((PrvAssetValue + CurAssetValue) - SalvageValue) / LifeYrs;
                        //    DepValue = Salvalue * NoofMonths / 12; // Formula for Without Percentage

                        //    CurPeriodValue = UtilityMember.NumberSet.ToInteger(Math.Abs(Math.Round(DepValue)).ToString());
                        //    CurPeriodBalance = UtilityMember.NumberSet.ToInteger(Math.Abs(Math.Round(CurTotalValue - CurPeriodValue)).ToString());
                        //    dr[DEPRECIATION_VALUE] = String.Format("{0:0.00}", CurPeriodValue);
                        //    dr[BALANCE_AMOUNT] = String.Format("{0:0.00}", CurPeriodBalance);

                        //}
                        //else
                        //{
                        //    // Chinna for Calculating % Values

                        //    //Salvalue = ((PrvAssetValue + CurAssetValue) - SalvageValue) * DepPercent / 100 * NoofMonths / 12; // Formula for With pecentage

                        //    //CurPeriodValue = UtilityMember.NumberSet.ToInteger(Math.Abs(Math.Round(Salvalue)).ToString());
                        //    //CurPeriodBalance = UtilityMember.NumberSet.ToInteger(Math.Abs(Math.Round(CurTotalValue - CurPeriodValue)).ToString());

                        //    Salvalue = ((PrvAssetValue + CurAssetValue)) * DepPercent / 100 * NoofMonths / 12; // Formula for With pecentage

                        //    CurPeriodValue = UtilityMember.NumberSet.ToInteger(Math.Abs(Math.Round(Salvalue)).ToString());
                        //    CurPeriodBalance = UtilityMember.NumberSet.ToInteger(Math.Abs(Math.Round(CurTotalValue - CurPeriodValue)).ToString());

                        //    dr[DEPRECIATION_VALUE] = String.Format("{0:0.00}", CurPeriodValue);
                        //    dr[BALANCE_AMOUNT] = String.Format("{0:0.00}", CurPeriodBalance);
                        //}
                    }
                }
                dtWDV.AcceptChanges();
                // gcDepreciation.DataSource = dtWDV;
                //   SetFinanceDetails(dtWDV);
            }
            return dtWDV;
        }

        private void SetReportBorder()
        {
            //***To align header table dynamically---changed by sugan******************************************************************************************
            xrtblHeaderTable.SuspendLayout();
            xrtblFdRegister.SuspendLayout();
            xrtblGrandTotal.SuspendLayout();
            xrtblBankFooter.SuspendLayout();

            //28/11/2018, to lock reinvestment feature based on setting ---------------------------------------------------

            // 07/09/2023
            //if (this.UIAppSetting.EnableFlexiFD != "1")
            //{
            //    if (xrHeaderRow.Cells.Contains(xrReInvestAmount))
            //        xrHeaderRow.Cells.Remove(xrHeaderRow.Cells[xrReInvestAmount.Name]);

            //    if (xrDataRow.Cells.Contains(xrReInvestAmountValue))
            //        xrDataRow.Cells.Remove(xrDataRow.Cells[xrReInvestAmountValue.Name]);

            //    if (xrBankGrpFooterRow.Cells.Contains(xrSumReIvestAmtBank))
            //        xrBankGrpFooterRow.Cells.Remove(xrBankGrpFooterRow.Cells[xrSumReIvestAmtBank.Name]);


            //    if (xrgrandRow.Cells.Contains(xrGrandReInvestAmt))
            //        xrgrandRow.Cells.Remove(xrgrandRow.Cells[xrGrandReInvestAmt.Name]);
            //}
            //-------------------------------------------------------------------------------------------------------------------

            xrPreviousCost.Text = xrDeprValueUpto.Text = UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).AddDays(-1).ToShortDateString();
            xrDeprPeriod.Text = xrDeprforPeriod.Text = Convert.ToDateTime(this.ReportProperties.DateFrom).ToString("dd/MM/yyyy") + " To " + Convert.ToDateTime(this.ReportProperties.DateTo).ToString("dd/MM/yyyy");
            // xrDeprPeriod.Text = xrDeprforPeriod.Text = this.ReportProperties.DateFrom + " To " + this.ReportProperties.DateTo;
            xrApplyToPeriod.Text = Convert.ToDateTime(this.ReportProperties.DateTo).ToString("dd/MM/yyyy");
            xrtotalAssetValuedate.Text = Convert.ToDateTime(this.ReportProperties.DateFrom).ToString("dd/MM/yyyy");

            xrtblHeaderTable.PerformLayout();
            xrtblFdRegister.PerformLayout();
            xrtblGrandTotal.PerformLayout();
            xrtblBankFooter.PerformLayout();

            //*********************************************************************************************
            xrtblHeaderTable = AlignHeaderTable(xrtblHeaderTable);
            xrtblFdRegister = AlignContentTable(xrtblFdRegister);
            xrtblGrandTotal = AlignGrandTotalTable(xrtblGrandTotal);
            xrtblBankFooter = AlignGrandTotalTable(xrtblBankFooter);

            // this.SetCurrencyFormat(xrDepFrom.Text, xrDepFrom);
            //  this.SetCurrencyFormat(xrIntReceived.Text, xrIntReceived);
            // this.SetCurrencyFormat(xrtotalAssetValuedate.Text, xrtotalAssetValuedate);
            // this.SetCurrencyFormat(xrReInvestAmount.Text, xrReInvestAmount);
            //this.SetCurrencyFormat(xrDeprPer.Text, xrDeprPer);
            //this.SetCurrencyFormat(xrDepMethod.Text, xrDepMethod);
            // this.SetCurrencyFormat(xrDeprforPeriod.Text, xrDeprforPeriod);
            //  this.SetCurrencyFormat(xrWithdrawAmount.Text, xrWithdrawAmount);
            //this.SetCurrencyFormat(xrApplyToPeriod.Text, xrApplyToPeriod);
        }

        #endregion
        private void PrintingSystem_AfterMarginsChange(object sender, MarginsChangeEventArgs e)
        {
            //  ChangeReportSettings(sender);
        }

        private void PrintingSystem_PageSettingsChanged(object sender, EventArgs e)
        {
            //  ChangeReportSettings(sender);
        }

        private void ChangeReportSettings(object sender)
        {
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrAssetClass.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrAssetItem.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrAssetId.Name, 10));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrPreviousCost.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrDeprPeriod.Name, 5));
            //  lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrRenewOn.Name, 5));
            //  lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrMaturityOn.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrDeprValueUpto.Name, 5));
            //lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrInstMode.Name, 5));
            //lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrInterest.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrtotalAssetValuedate.Name, 5));
            // lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrIntReceived.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrDepFrom.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrDepMethod.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrDeprPer.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrDeprforPeriod.Name, 5));
            //lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrWithdrawAmount.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrApplyToPeriod.Name, 5));
            // lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblHeaderTable.Name, xrStatus.Name, 5));

            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrFDAcNoValue.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrBankValue.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrProjectValue.Name, 10));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrInvestNameValue.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrInvestOnValue.Name, 5));
            //  lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrRenewOnValue.Name, 5));
            //  lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrMaturityOnValue.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrClosedOnValue.Name, 5));
            //lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrInstModeValue.Name, 5));
            //lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrInterestValue.Name, 5));
            //    lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrPrincipalAmountValue.Name, 5));
            // lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrIntReceivedValue.Name, 5));
            //    lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrAccumulatedInterestValue.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrMatValue.Name, 5));
            //lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrTDSAmountValue.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrTotalAmountValue.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrWithdrawAmountValue.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrClosingBalanceValue.Name, 5));
            //lstcolumnwidth.Add(new Tuple<String, String, Int32>(xrtblFdRegister.Name, xrStatusValue.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(this.xrtblGrandTotal.Name, xrGrandPrincipleAmt.Name, 5));
            lstcolumnwidth.Add(new Tuple<String, String, Int32>(this.xrtblGrandTotal.Name, xrGrandReInvestAmt.Name, 5));


            PrintingSystemBase ps = sender as PrintingSystemBase;
            bool isLocationChanged = false;
            int newPageWidth =
                ps.PageBounds.Width - ps.PageMargins.Left - ps.PageMargins.Right;
            int currentPageWidth =
                this.PageWidth - this.Margins.Left - this.Margins.Right;
            int shift = currentPageWidth - newPageWidth;
            ResizeControls(this, newPageWidth);
            isLocationChanged = true;
            if (isLocationChanged == true)
            {
                this.Margins.Top = ps.PageMargins.Top;
                this.Margins.Bottom = ps.PageMargins.Bottom;
                this.Margins.Left = ps.PageMargins.Left;
                this.Margins.Right = ps.PageMargins.Right;
                this.PaperKind = ps.PageSettings.PaperKind;
                this.PaperName = ps.PageSettings.PaperName;
                this.Landscape = ps.PageSettings.Landscape;

                //xrInvestOn.WidthF= xrInvestOnValue.WidthF = 58;
                //xrRenewOn.WidthF = xrRenewOnValue.WidthF = 58;
                //xrMaturityOn.WidthF = xrMaturityOnValue.WidthF = 58;
                //xrClosedOn.WidthF = xrClosedOnValue.WidthF = 58;

                this.CreateDocument();
            }
        }

        private void ResizeControls(ReportBase rpt, Int32 pagewidh)
        {
            foreach (Band _band in rpt.Bands)
            {
                foreach (XRControl _control in _band.Controls)
                {
                    //_control.Location = new Point((_control.Location.X - shift), _control.Location.Y);
                    if (_control.GetType() == typeof(XRLabel))
                    {
                        _control.WidthF = pagewidh - 5;
                    }
                    else if (_control.GetType() == typeof(XRTable))
                    {
                        _control.WidthF = pagewidh - 5;
                        //XRTable xrtbl = _control as XRTable;

                        //foreach(XRTableCell cell in xrtbl.Rows[0].Cells)
                        //{
                        //    int index = lstcolumnwidth.FindIndex(t => t.Item1 == xrtbl.Name && t.Item2 == cell.Name);
                        //    float columnwidth = (pagewidh * 2) / 100;
                        //    if (index >= 0)
                        //    {
                        //        Tuple<String, String, Int32> cellwidth = new Tuple<String, String, Int32>(string.Empty, string.Empty, 0);
                        //        lstcolumnwidth.TryGetValue(index, ref cellwidth);
                        //        Int32 percentage = cellwidth.Item3;
                        //        columnwidth = (pagewidh * percentage) / 100;
                        //        cell.WidthF = columnwidth;
                        //    }
                        //    else
                        //    {

                        //    }
                        //}
                    }
                    else if (_control.GetType() == typeof(XRSubreport))
                    {
                        //XRSubreport subreport = _control as XRSubreport;
                        //subreport.WidthF = 
                        //ResizeControls(subreport.ReportSource as ReportBase, pagewidh);
                    }
                }
            }
        }

        private void xrSnoValue_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataRowView drvcrrentrow = (DataRowView)this.GetCurrentRow();
            if (drvcrrentrow == null)
            {
                //xrSnoValue.Visible = false;
            }
            else
            {
                // xrSnoValue.Visible = true;
            }
        }

        private void xrSnoValue_AfterPrint(object sender, EventArgs e)
        {

        }

        private void xrcellInvestmentName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (ReportProperty.Current.ShowByLedger == 1)
            {
                string investmentname = GetCurrentColumnValue(reportSetting1.FDRegister.ACCOUNT_HOLDERColumn.ColumnName).ToString();
                if (!string.IsNullOrEmpty(investmentname))
                {
                    //        xrcellInvestmentName.Text = "   " + investmentname;
                }
            }
        }

        private void xrgrpBankValue_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string bankname = GetCurrentColumnValue(reportSetting1.FDRegister.BANKColumn.ColumnName).ToString();

            if (ReportProperty.Current.ShowByLedger == 1 && (ReportProperty.Current.ShowByInvestment == 1 || ReportProperty.Current.ShowByLedger == 1))
            {
                if (!string.IsNullOrEmpty(bankname))
                {
                    //      xrgrpBankValue.Text = "     " + bankname;
                }
            }
            else if (ReportProperty.Current.ShowByLedger == 0 && ReportProperty.Current.ShowByInvestment == 1)
            {
                if (!string.IsNullOrEmpty(bankname))
                {
                    //    xrgrpBankValue.Text = "   " + bankname;
                }
            }
        }

        /// <summary>
        /// This method attach group details settings
        /// </summary>
        private void AttachShowBy()
        {
            //Add Bank Group
            if (ReportProperty.Current.ShowByBank == 0)
            {
                // grpBankHeader.GroupFields[0].FieldName = string.Empty;
            }
            else
            {
                // grpBankHeader.GroupFields[0].FieldName = this.ReportParameters.BANKColumn.ColumnName;
                //grpBankHeader.GroupFields.Add( new GroupField(this.ReportParameters.BANKColumn.ColumnName,XRColumnSortOrder.Ascending));
            }

            //Add Investment Group
            if (ReportProperty.Current.ShowByInvestment == 0)
            {
                // grpInvestmentHeader.GroupFields[0].FieldName = string.Empty;
            }
            else
            {
                // grpInvestmentHeader.GroupFields[0].FieldName = this.reportSetting1.FixedDepositStatement.ACCOUNT_HOLDERColumn.ColumnName;
                //grpBankHeader.GroupFields.Add( new GroupField(this.ReportParameters.BANKColumn.ColumnName,XRColumnSortOrder.Ascending));
            }

            //Add Ledger Group
            if (ReportProperty.Current.ShowByLedger == 0)
            {
                // grpFDLedgerHeader.GroupFields[0].FieldName = string.Empty;
            }
            else
            {
                // grpFDLedgerHeader.GroupFields[0].FieldName = this.reportSetting1.Ledger.LEDGER_NAMEColumn.ColumnName;
            }
        }

        private void xrBankTotalCaption_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string banktitleCaption = (sender as XRTableCell).Text.Trim();

            if (ReportProperty.Current.ShowByLedger == 1 && (ReportProperty.Current.ShowByInvestment == 1 || ReportProperty.Current.ShowByLedger == 1))
            {
                if (!string.IsNullOrEmpty(banktitleCaption))
                {
                    (sender as XRTableCell).Text = "     " + banktitleCaption;
                }
            }
            else if (ReportProperty.Current.ShowByLedger == 0 && ReportProperty.Current.ShowByInvestment == 1)
            {
                if (!string.IsNullOrEmpty(banktitleCaption))
                {
                    (sender as XRTableCell).Text = "   " + banktitleCaption;
                }
            }
        }

        private void xrInvestmentCaption_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string investmentnameCaption = (sender as XRTableCell).Text.Trim();
            (sender as XRTableCell).Text = "   " + investmentnameCaption.Trim();

            if (ReportProperty.Current.ShowByLedger == 1)
            {
                if (!string.IsNullOrEmpty(investmentnameCaption))
                {
                    investmentnameCaption = "   " + investmentnameCaption;
                }
            }
            // xrInvestmentCaption.Text = investmentnameCaption;
        }

    }
}
