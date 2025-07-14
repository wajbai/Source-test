using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.Report.Base;
using Bosco.Utility;
using Bosco.DAO.Data;
using AcMEDSync.Model;
using System.Data;
using System.Globalization;
using Bosco.Utility.ConfigSetting;
using Bosco.Report.View;
using DevExpress.XtraSplashScreen;
using System.Linq;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class MultiAbstractRPCurrency : Bosco.Report.Base.ReportHeaderBase
    {
        private Graphics gr = Graphics.FromHwnd(IntPtr.Zero);
        bool IsMultiReceipts = true;
        bool rowemptyremoved = false;
        double BalanceInEuro = 0;
        double CBBalanceInEuro = 0;
        double TotalBalanceInEuro = 0;
                
        double BudgetAmount = 0;
        double TotalBudgetAmount = 0;
        
        double Percentage = 0;
        private DataTable dtAllCurrencyEnabledCashBank = null;

        private string EURONAME = "EURO";
        private string DOLLARNAME = "DOLLAR";

        public MultiAbstractRPCurrency()
        {
            InitializeComponent();
            this.SetTitleWidth(xrPGMultiAbstractReceipt.WidthF);
        }

        public void HideReportHeaderFooter()
        {
            this.HideReportHeader = false;
            this.HidePageFooter = false;
        }

        #region ShowReport

        public override void ShowReport()
        {
            rowemptyremoved = false;
            
            bool isMultiReceipts = (this.ReportProperties.ReportId == "RPT-220" ? false : true);

            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                || String.IsNullOrEmpty(this.ReportProperties.Project))
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
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        BindMultiAbstractReceiptSource(isMultiReceipts);
                        SplashScreenManager.CloseForm();
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
                    SplashScreenManager.ShowForm(typeof(frmReportWait));
                    BindMultiAbstractReceiptSource(isMultiReceipts);
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
        }

        #endregion

        public void BindMultiAbstractReceiptSource(bool isMultiReceipts)
        {
            IsMultiReceipts = isMultiReceipts;
            BalanceInEuro = 0;
            TotalBalanceInEuro = 0;
            CBBalanceInEuro = 0;
            
            BudgetAmount = 0;
            TotalBudgetAmount = 0;
            Percentage = 0;
            setHeaderTitleAlignment();
            SetReportTitle();
            this.HideDateRange = true;
            this.HideReportSubTitle = false;
            /*grpOpeningBalane.Visible = xrSubOpeningBalanceMulti.Visible = xrLabelCLBal.Visible = false;
            detailClosingBalance.Visible = xrSubClosingBalanceMulti.Visible = xrLabelCLBal.Visible = false;
            
            if (IsMultiReceipts)
            {
                grpOpeningBalane.Visible = xrSubOpeningBalanceMulti.Visible = false;// true;
                xrLabelCLBal.Visible = false;    
            }
            else
            {
                detailClosingBalance.Visible = xrSubClosingBalanceMulti.Visible = xrLabelCLBal.Visible = false;// true;
            }*/
            
            ResultArgs resultArgs = GetReportSource(IsMultiReceipts);
            if (resultArgs.Success)
            {
                DataTable dtReceiptPaymentYear = resultArgs.DataSource.Table;

                if (dtReceiptPaymentYear != null)
                {
                    dtReceiptPaymentYear.TableName = "MultiAbstract";
                    xrPGMultiAbstractReceipt.DataSource = dtReceiptPaymentYear;
                    xrPGMultiAbstractReceipt.DataMember = dtReceiptPaymentYear.TableName;
                }

                SetReportSetting(dtReceiptPaymentYear.DefaultView); //, accountBalanceMultiProject

                
            }
        }

        private ResultArgs GetReportSource(bool ReceiptReport)
        {
            ResultArgs resultArgs = null;
            string sqlMultiAbstractReceipts = this.GetReportSQL(SQL.ReportSQLCommand.Report.MultiAbstractCurrency);
            string liquidityGroupIds = this.GetLiquidityGroupIds();

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, (ReceiptReport?  TransType.RC.ToString() :TransType.PY.ToString() ));
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, liquidityGroupIds);
                dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, (ReceiptReport?  TransMode.CR.ToString(): TransMode.DR.ToString()));

                dataManager.Parameters.Add(this.ReportParameters.YEAR_FROMColumn, settingProperty.YearFrom);
                dataManager.Parameters.Add(this.ReportParameters.YEAR_TOColumn, settingProperty.YearTo);

                //dataManager.Parameters.Add(this.reportSetting1.MultiAbstract.NO_OF_YEARColumn, this.ReportProperties.NoOfYears);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlMultiAbstractReceipts);
            }

            if (resultArgs.Success && resultArgs.DataSource.Table!=null)
            {
                DataTable dtReport = resultArgs.DataSource.Table;

                
                ResultArgs resultCB = this.FetchAllCashBankLedgerByProject(false);
                if (resultCB.Success && resultCB.DataSource.Table != null)
                {
                    dtAllCurrencyEnabledCashBank = resultCB.DataSource.Table;
                    dtAllCurrencyEnabledCashBank.DefaultView.RowFilter = reportSetting1.ReportParameter.CUR_COUNTRY_IDColumn + " > 0";
                    dtAllCurrencyEnabledCashBank = dtAllCurrencyEnabledCashBank.DefaultView.ToTable();

                    foreach (DataRow dr in dtAllCurrencyEnabledCashBank.Rows)
                    {
                        Int32 curcountryid = UtilityMember.NumberSet.ToInteger(dr[reportSetting1.Ledger.CUR_COUNTRY_IDColumn.ColumnName].ToString());
                        string currencyname = dr[reportSetting1.Country.CURRENCY_NAMEColumn.ColumnName].ToString();

                        //For Cash 
                        dtReport.DefaultView.RowFilter = reportSetting1.ReportParameter.CURRENCY_COUNTRY_IDColumn.ColumnName + " = " + curcountryid  + " AND IS_CASH_BANK = 0";
                        if (dtReport.DefaultView.Count == 0)
                        {
                            AttachEmptyRow(dtReport, curcountryid, currencyname, 0);
                        }
                        dtReport.DefaultView.RowFilter = string.Empty;

                        //For Bank
                        dtReport.DefaultView.RowFilter = reportSetting1.ReportParameter.CURRENCY_COUNTRY_IDColumn.ColumnName + " = " + curcountryid + " AND IS_CASH_BANK = 1";
                        if (dtReport.DefaultView.Count == 0)
                        {
                            AttachEmptyRow(dtReport, curcountryid, currencyname, 1);
                        }
                        dtReport.DefaultView.RowFilter = string.Empty;
                    }
                }
                
            }

            return resultArgs;
        }

        private void AttachEmptyRow(DataTable dtRpt, Int32 CurrencyCountryid, string CurrencyName, Int32 IsCashBank)
        {
            if (dtRpt != null)
            {
                DataRow dr = dtRpt.NewRow();
                
                dr[reportSetting1.ReportParameter.CURRENCY_COUNTRY_IDColumn.ColumnName] = CurrencyCountryid;
                dr[reportSetting1.Country.CURRENCY_NAMEColumn.ColumnName] = CurrencyName;
                dr[reportSetting1.ReportParameter.CURRENCY_NAMEColumn.ColumnName] = CurrencyName;
                dr[reportSetting1.ReportParameter.IS_CASH_BANKColumn.ColumnName] = IsCashBank;
                dr[reportSetting1.ReportParameter.CASH_BANKColumn.ColumnName] = (IsCashBank == 0 ? "Cash" : "Bank");
                dr[reportSetting1.MultiAbstract.LEDGER_GROUPColumn.ColumnName] = string.Empty;
                dr[reportSetting1.MultiAbstract.LEDGER_IDColumn.ColumnName] = 0;
                dr[reportSetting1.MultiAbstract.LEDGER_CODEColumn.ColumnName] = string.Empty;
                dr[reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName] = string.Empty;
                dr[reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName] = 0;
                dr[reportSetting1.ReportParameter.AMOUNT_LCColumn.ColumnName] = 0;
                dtRpt.Rows.Add(dr);
            }

        }


        private void BindGrandTotal()
        {
            if (xrPGMultiAbstractReceipt.DataSource != null)
            {
                DataTable dtGrantTotalBalance = new DataTable();
                DataTable dtRpt = xrPGMultiAbstractReceipt.DataSource as DataTable;

                string[] aCBLedgers = new string[] { ReportParameters.GROUP_IDColumn.ColumnName, ReportParameters.LEDGER_IDColumn.ColumnName, 
                            ReportParameters.CUR_COUNTRY_IDColumn.ColumnName, ReportParameters.CURRENCY_NAMEColumn.ColumnName};

                if (dtAllCurrencyEnabledCashBank != null)
                {
                    dtGrantTotalBalance = dtAllCurrencyEnabledCashBank.DefaultView.ToTable(false, aCBLedgers);
                    dtGrantTotalBalance.DefaultView.Sort = ReportParameters.CURRENCY_NAMEColumn.ColumnName;
                    dtGrantTotalBalance = dtGrantTotalBalance.DefaultView.ToTable();

                    dtGrantTotalBalance.Columns.Add(reportSetting1.MonthlyAbstract.PARENT_GROUPColumn.ColumnName, typeof(System.String));
                    dtGrantTotalBalance.Columns.Add(ReportParameters.LEDGER_NAMEColumn.ColumnName, typeof(System.String));
                    dtGrantTotalBalance.Columns.Add(ReportParameters.CASH_BANK_AMOUNTColumn.ColumnName, typeof(System.Double));
                    dtGrantTotalBalance.Columns.Add(ReportParameters.AMOUNTColumn.ColumnName, typeof(System.Double));
                    dtGrantTotalBalance.Columns.Add(ReportParameters.IS_CASH_BANKColumn.ColumnName, typeof(System.Int32),
                        "IIF(" + ReportParameters.GROUP_IDColumn.ColumnName + "= 12, 1, 0)");
                }

                foreach (DataRow drGrantTotalBal in dtGrantTotalBalance.Rows)
                {
                    Int32 lid = UtilityMember.NumberSet.ToInteger(drGrantTotalBal[ReportParameters.LEDGER_IDColumn.ColumnName].ToString());
                    Int32 curcountryid = UtilityMember.NumberSet.ToInteger(drGrantTotalBal[ReportParameters.CUR_COUNTRY_IDColumn.ColumnName].ToString());
                    Int32 isCashBank = UtilityMember.NumberSet.ToInteger(drGrantTotalBal[ReportParameters.IS_CASH_BANKColumn.ColumnName].ToString());

                    BalanceSystem.LiquidBalanceGroup balance = (isCashBank == 0 ? BalanceSystem.LiquidBalanceGroup.CashBalance :
                                BalanceSystem.LiquidBalanceGroup.BankBalance);
                    BalanceSystem.BalanceType balancetype = (IsMultiReceipts ? BalanceSystem.BalanceType.OpeningBalance :
                                BalanceSystem.BalanceType.ClosingBalance);
                    string BalaDate = (IsMultiReceipts ? ReportProperty.Current.DateFrom : ReportProperty.Current.DateTo);

                    string filter = ReportParameters.CURRENCY_COUNTRY_IDColumn.ColumnName + "= " + curcountryid + " AND " +
                                    ReportParameters.IS_CASH_BANKColumn.ColumnName + " = " + isCashBank;
                    double total = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(" + ReportParameters.AMOUNTColumn + ")", filter).ToString());
                    double cashbankBalance = base.GetBalance(ReportProperty.Current.Project, BalaDate, balance, balancetype, lid.ToString(), true);

                    drGrantTotalBal.BeginEdit();
                    drGrantTotalBal[reportSetting1.MonthlyAbstract.PARENT_GROUPColumn.ColumnName] = "Grand Total";
                    drGrantTotalBal[ReportParameters.LEDGER_NAMEColumn.ColumnName] = (IsMultiReceipts ? "Opening Balance" : "Closing Balance");
                    drGrantTotalBal[ReportParameters.CASH_BANK_AMOUNTColumn.ColumnName] = cashbankBalance;
                    drGrantTotalBal[ReportParameters.AMOUNTColumn.ColumnName] = (cashbankBalance + total);
                    drGrantTotalBal.EndEdit();
                }

                dtGrantTotalBalance.TableName = "MultiAbstract";
                xrPGGrandTotal.DataSource = dtGrantTotalBalance;
                xrPGGrandTotal.DataMember = dtGrantTotalBalance.TableName;

                xrPGOpeningClosingBalance.DataSource = dtGrantTotalBalance;
                xrPGOpeningClosingBalance.DataMember = dtGrantTotalBalance.TableName;
            }
        }

        
        private void xrPGMultiAbstractReceipt_FieldValueDisplayText(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
            {
                if (e.DataField != null && e.IsColumn )
                {
                    e.DisplayText = (e.DisplayText == "Grand Total" ? "Balance in Euro" : e.DisplayText);
                }
                else
                {
                    e.DisplayText = "Total ";// +(IsMultiReceipts ? "Receipts" : "Payments"); //"Balance in Euro "
                }
            }
            else if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
            {
                e.DisplayText = "Total";
            }
        }

        private void xrPGMultiAbstractReceipt_PrintFieldValue(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs e)
        {
            if (e.Field != null)
            {
                if (e.Field.Name == fieldLEDGERCODE.Name || e.Field.Name == fieldLEDGERNAME.Name
                    || e.Field.Name == fieldGROUPCODE.Name || e.Field.Name == fieldLEDGERGROUP.Name)
                {
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
                    
                    //On 16/09/2002, If content is splitted into two pages (bottom text), let us fix to anyother page fully and keep space ---------
                    if (e.Brick.SeparableVert)
                    {
                        //e.Brick.SeparableVert = false;
                    }
                    //-------------------------------------------------------------------------------------------------------------------------------
                }
                else if (e.Field.Name == fieldCURRENCYNAME.Name || e.Field.Name == fieldIS_CASH_BANK.Name)
                {
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
                    e.Appearance.BorderColor = Color.DarkGray;// xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.BorderColor;
                    e.Appearance.BackColor = xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.BackColor;
                    e.Appearance.Font = xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font;
                    if (e.Field.Name == fieldIS_CASH_BANK.Name)
                    {
                        e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
                        Int32 isCashBank = UtilityMember.NumberSet.ToInteger(e.Brick.Text);
                        e.Brick.Text = (isCashBank == 0 ? "Cash" : "Bank");
                        e.Brick.TextValue = (isCashBank == 0 ? "Cash" : "Bank");

                    }
                }
            }
            else
            {
                if (e.ValueType == PivotGridValueType.GrandTotal)
                {
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
                }
            }
        }

        private void xrPGGrandTotal_PrintFieldValue(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs e)
        {
            if (e.Field != null)
            {
                if (e.Field.Name == fieldGRANTTOTALPARTICULARS.Name)
                {
                    e.Appearance.BackColor = xrPGGrandTotal.Styles.GrandTotalCellStyle.BackColor;
                }
                else if (e.Field.Name == fieldGRANTTOTALCURRENCY.Name || e.Field.Name == fieldGRANDTOTAL_IS_CASH_BANK.Name ||
                         e.Field.Name == fieldGRANTTOTALAMOUNT.Name || e.Field.Name == fieldGRANDBUDGET.Name || e.Field.Name == fieldGRANDPERCENTAGE.Name)
                {
                    if (xrPGGrandTotal.OptionsView.ShowRowHeaders == false)
                    {
                        e.Brick.Text = "";
                        e.Brick.BorderWidth = 0;
                        e.Appearance.BackColor = Color.White;
                        DevExpress.XtraPrinting.TextBrick textBrick = e.Brick as DevExpress.XtraPrinting.TextBrick;
                        DevExpress.XtraPrinting.PanelBrick paneltextBrick = e.Brick as DevExpress.XtraPrinting.PanelBrick;

                        if (textBrick != null)
                        {
                            textBrick.Location = new PointF(textBrick.Location.X, 0);
                            textBrick.Size = new SizeF(textBrick.Size.Width, 0);
                        }
                        else if (paneltextBrick != null)
                        {
                            paneltextBrick.Location = new PointF(paneltextBrick.Location.X, 0);
                            paneltextBrick.Size = new SizeF(paneltextBrick.Size.Width, 0);
                        }
                    }
                }
            }
           
        }

        private void xrPGMultiAbstractReceipt_PrintCell(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportCellEventArgs e)
        {
           
            if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.Cell)
            {
                if (e.Brick.TextValue == null || this.UtilityMember.NumberSet.ToDouble(e.Brick.TextValue.ToString()) == 0) { e.Brick.Text = ""; }

                if (e.ColumnFieldIndex < 0 && e.DataField != null && e.DataField.Name == fieldAMOUNT.Name) //For Total
                {
                    Percentage = 0;
                    double tmpBalanceInEuro = BalanceInEuro;
                    BalanceInEuro = (BudgetAmount - BalanceInEuro);
                    if (BalanceInEuro > 0)
                    {
                        Percentage = Math.Round(  ((tmpBalanceInEuro / BudgetAmount) * 100), 2);
                    }
                    e.Brick.Text = UtilityMember.NumberSet.ToNumber(BalanceInEuro);
                    e.Brick.TextValue = UtilityMember.NumberSet.ToNumber(BalanceInEuro);
                    TotalBalanceInEuro += BalanceInEuro;
                    BalanceInEuro = 0;
                    BudgetAmount = 0;
                }
                else if (e.ColumnFieldIndex < 0 && e.DataField != null && e.DataField.Name == fieldBUDGET.Name) //For Budget
                {
                    BudgetAmount = UtilityMember.NumberSet.ToDouble(e.Brick.Text);
                    double exchangerate = (ReportProperty.Current.AvgEuroExchangeRate <= 0 ? 1 : ReportProperty.Current.AvgEuroExchangeRate);
                    if (BudgetAmount > 0)
                    {
                        BudgetAmount = (BudgetAmount / exchangerate);
                    }
                    BudgetAmount = Math.Round(BudgetAmount, 2);
                    TotalBudgetAmount += BudgetAmount;
                    e.Brick.Text = UtilityMember.NumberSet.ToNumber(BudgetAmount);
                    e.Brick.TextValue = UtilityMember.NumberSet.ToNumber(BudgetAmount);
                }
                else if (e.ColumnFieldIndex < 0 && e.DataField != null && e.DataField.Name == fieldPERCENTAGE.Name) //For Total
                {
                    e.Brick.Text = UtilityMember.NumberSet.ToDouble(Percentage.ToString()).ToString() + "%";
                    e.Brick.TextValue = UtilityMember.NumberSet.ToDouble(Percentage.ToString()).ToString() + "%";
                }
                else if (e.DataField.Name != fieldBUDGET.Name)
                {
                    string currencyname = (e.ColumnValue == null ? "" : e.ColumnValue.Text.ToString());
                    double amt = UtilityMember.NumberSet.ToDouble(e.Brick.Text);

                    if (!string.IsNullOrEmpty(currencyname) && amt > 0)
                    {
                        double convertedamt = amt;
                        double exchangerate = 1;
                        if (currencyname.Trim().ToUpper().Contains(AppSetting.CurrencyName.Trim().ToUpper()))
                        {
                            exchangerate = (ReportProperty.Current.AvgEuroExchangeRate <= 0 ? 1 : ReportProperty.Current.AvgEuroExchangeRate);
                        }
                        else if (currencyname.Trim().ToUpper().Contains(DOLLARNAME))
                        {
                            exchangerate = (ReportProperty.Current.AvgEuroDollarExchangeRate <= 0 ? 1 : ReportProperty.Current.AvgEuroDollarExchangeRate);
                        }
                        convertedamt = (amt / exchangerate);

                        BalanceInEuro += Math.Round(convertedamt, 2);
                    }
                }
            }
            else if (e.RowValue.IsTotal && e.ColumnFieldIndex < 0 && e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.GrandTotalCell)
            {
                if (e.DataField == fieldAMOUNT)
                {
                    e.Brick.Text = UtilityMember.NumberSet.ToNumber(TotalBalanceInEuro);
                    e.Brick.TextValue  = UtilityMember.NumberSet.ToNumber(TotalBalanceInEuro);
                }
                else if (e.DataField == fieldBUDGET)
                {
                    e.Brick.Text = UtilityMember.NumberSet.ToNumber(TotalBudgetAmount);
                    e.Brick.TextValue = UtilityMember.NumberSet.ToNumber(TotalBudgetAmount);
                }
            }
        }

        private void xrPGMultiAbstractReceipt_AfterPrint(object sender, EventArgs e)
        {
            BindGrandTotal();
        }

        private void SetReportSetting(DataView dvReceipt) //, AccountBalanceMultiProject accountBalanceMultiYear
        {
            try { fieldGROUPCODE.Visible = true; }
            catch { }
            try { fieldLEDGERGROUP.Visible = true; }
            catch { }
            try { fieldLEDGERCODE.Visible = true; }
            catch { }
            try { fieldLEDGERNAME.Visible = true; }
            catch { }

            fieldGROUPCODE.AreaIndex = 0;
            fieldLEDGERGROUP.AreaIndex = 1;
            fieldLEDGERCODE.AreaIndex = 2;
            fieldLEDGERNAME.AreaIndex = 3;
                                    
            bool isGroupVisible = (ReportProperties.ShowByLedgerGroup == 1);
            bool isLedgerVisible = (ReportProperties.ShowByLedger == 1);
            if (isGroupVisible == false && isLedgerVisible == false) { isLedgerVisible = true; }
            bool isGroupCodeVisible = (isGroupVisible && (ReportProperties.ShowGroupCode == 1));
            bool isLedgerCodeVisible = (isLedgerVisible && (ReportProperties.ShowLedgerCode == 1));
            bool isHorizontalLine = (ReportProperties.ShowHorizontalLine == 1);
            bool isVerticalLine = (ReportProperties.ShowVerticalLine == 1);
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

            //On 14/09/2022, to show Ledger Group Total----------------------------------------------------------------------------
            xrPGMultiAbstractReceipt.OptionsView.ShowRowTotals = false;
            if (ReportProperties.ShowByLedgerGroup == 1 && ReportProperties.ShowByLedger == 1)
            {
                xrPGGrandTotal.KeepTogether = true;
                xrPGOpeningClosingBalance.KeepTogether = true;
                xrPGMultiAbstractReceipt.OptionsView.ShowRowTotals = true;
                xrPGMultiAbstractReceipt.Appearance.FieldValueTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
                fieldGROUPCODE.TotalsVisibility = PivotTotalsVisibility.None;
                fieldLEDGERCODE.TotalsVisibility = PivotTotalsVisibility.None;
                xrPGMultiAbstractReceipt.OptionsView.ShowTotalsForSingleValues = true;
                xrPGMultiAbstractReceipt.Styles.TotalCellStyle = xrPGMultiAbstractReceipt.Styles.GrandTotalCellStyle;
                xrPGMultiAbstractReceipt.Styles.TotalCellStyle.Padding = new DevExpress.XtraPrinting.PaddingInfo(5);
                //xrPGMultiAbstractReceipt.OptionsView.RowTotalsLocation = PivotRowTotalsLocation.Far;
            }
            //---------------------------------------------------------------------------------------------------------------------

            xrPGMultiAbstractReceipt.OptionsView.ShowRowGrandTotals = true;
            xrPGOpeningClosingBalance.OptionsView.ShowColumnGrandTotals = true;
            xrPGGrandTotal.OptionsView.ShowColumnGrandTotals = true;
            fieldCBCurrencyName.Options.ShowValues = false;
            fieldCBName.Options.ShowValues = false;

            xrPGGrandTotal.OptionsView.ShowColumnHeaders = false;
            
            //On 14/03/2018, To set/reset amount column width based on Showcode
            fieldGROUPCODE.Width = 60;//35;
            fieldLEDGERCODE.Width = 45;// 40;//35;
            if (isLedgerCodeVisible || isGroupCodeVisible)
            {
                fieldLEDGERGROUP.Width = 120; //90;
                fieldLEDGERNAME.Width = 225; //190; //121 130;
                fieldCURRENCYNAME.Width = (isGroupVisible && isLedgerVisible) ? 100 : 95; //63 : 73; add 25
            }
            else
            {
                fieldLEDGERGROUP.Width = 125; //90;
                fieldLEDGERNAME.Width = 225; //118 130;
                fieldCURRENCYNAME.Width = (isGroupVisible && isLedgerVisible) ? 95 : 110; //66 : 76; add 25
            }
            fieldAMOUNT.Width = fieldBUDGET.Width = fieldCBBudget.Width =  100;
            fieldPERCENTAGE.Width = fieldCBPercentage.Width = fieldGRANDPERCENTAGE.Width  = 50;
            fieldBUDGET.Options.ShowValues = false;

            //Include / Exclude Code
            try { fieldGROUPCODE.Visible = (isGroupCodeVisible); }
            catch { }
            try { fieldLEDGERGROUP.Visible = isGroupVisible; }
            catch { }
            try { fieldLEDGERCODE.Visible = (isLedgerCodeVisible); }
            catch { }
            try { fieldLEDGERNAME.Visible = isLedgerVisible; }
            catch { }

            //Grant Total Grid
            int rowWidth = 0;
            xrPGGrandTotal.OptionsView.ShowRowHeaders = false;
            xrPGGrandTotal.LeftF = xrPGMultiAbstractReceipt.LeftF;
            xrPGGrandTotal.TopF = 0;
            xrPGOpeningClosingBalance.OptionsView.ShowRowHeaders = false;
            xrPGOpeningClosingBalance.LeftF = xrPGMultiAbstractReceipt.LeftF;
            xrPGOpeningClosingBalance.TopF = 0;

            if (fieldGROUPCODE.Visible) { rowWidth = fieldGROUPCODE.Width; }
            if (fieldLEDGERGROUP.Visible) { rowWidth += fieldLEDGERGROUP.Width; }
            if (fieldLEDGERCODE.Visible) { rowWidth += fieldLEDGERCODE.Width; }
            if (fieldLEDGERNAME.Visible) { rowWidth += fieldLEDGERNAME.Width; }
            fieldGRANTTOTALPARTICULARS.Width = rowWidth;
            fieldGRANTTOTALCURRENCY.Width = fieldCURRENCYNAME.Width;
            fieldGRANTTOTALAMOUNT.Width = fieldAMOUNT.Width;

            fieldCBName.Width = rowWidth;
            fieldCBCurrencyName.Width = fieldCURRENCYNAME.Width;
            fieldCBAmount.Width = fieldAMOUNT.Width;
            
            //Grid Lines
            if (isHorizontalLine)
            {
                xrPGMultiAbstractReceipt.OptionsPrint.PrintHorzLines = DevExpress.Utils.DefaultBoolean.True;
            }
            else
            {
                xrPGMultiAbstractReceipt.OptionsPrint.PrintHorzLines = DevExpress.Utils.DefaultBoolean.False;
            }

            if (isVerticalLine)
            {
                xrPGMultiAbstractReceipt.OptionsPrint.PrintVertLines = DevExpress.Utils.DefaultBoolean.True;
            }
            else
            {
                xrPGMultiAbstractReceipt.OptionsPrint.PrintVertLines = DevExpress.Utils.DefaultBoolean.False;
            }

            //22/09/2020, To fix Border color based on settings
            xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
            xrPGMultiAbstractReceipt.Styles.CellStyle.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
            xrPGMultiAbstractReceipt.Styles.GrandTotalCellStyle.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                        
            //On 16/04/2021
            float fontsize = float.Parse("9");
            fieldLEDGERGROUP.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, fontsize, FontStyle.Bold);
            fieldLEDGERNAME.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, fontsize, FontStyle.Regular);
            fieldLEDGERCODE.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, fontsize, FontStyle.Regular);
            fieldAMOUNT.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, fontsize, FontStyle.Regular);
            
            if (xrPGMultiAbstractReceipt.Styles.TotalCellStyle != null)
            {
                xrPGMultiAbstractReceipt.Styles.TotalCellStyle.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                xrPGMultiAbstractReceipt.Styles.TotalCellStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, fontsize, FontStyle.Bold);
            }
        }

        private void xrPGMultiAbstractReceipt_CustomFieldSort_1(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs e)
        {
            if (e.Field.Name == fieldCURRENCYNAME.Name)
            {
                if (e.Value1 != null && e.Value2 != null)
                {
                    string currency1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, reportSetting1.Country.CURRENCY_NAMEColumn.ColumnName).ToString();
                    string currency2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, reportSetting1.Country.CURRENCY_NAMEColumn.ColumnName).ToString();
                    Int32 c1 = GetCurrencyOrder(currency1);
                    Int32 c2 = GetCurrencyOrder(currency2);
                    
                    e.Result = Comparer.Default.Compare(c1, c2);
                    e.Handled = true;
                }
            }
        }

        private void xrPGMultiAbstractReceipt_CustomFieldValueCells(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotCustomFieldValueCellsEventArgs e)
        {
            //If particular year does not contain data, empty row will be displayed, it should be removed
            if (!rowemptyremoved)
            {
                bool isrowempty = false;
                for (int j = 0; j < e.ColumnCount; j++)
                {
                    DevExpress.XtraReports.UI.PivotGrid.FieldValueCell cell = e.GetCell(false, j);
                    if (cell != null && cell.Field != null && cell.Field.FieldName == "LEDGER_NAME")
                    {
                        isrowempty = string.IsNullOrEmpty(cell.Value.ToString());
                        break;
                    }
                }

                if (isrowempty)
                {
                    for (int j = 0; j < e.ColumnCount; j++)
                    {
                        DevExpress.XtraReports.UI.PivotGrid.FieldValueCell cell = e.GetCell(false, j);

                        if (cell == null) continue;
                        if (cell.EndLevel == e.GetLevelCount(false) - 1)
                        {
                            if (cell.Field != null && rowemptyremoved == false)
                            {
                                e.Remove(cell);
                                rowemptyremoved = true;
                            }
                        }
                    }
                }
            }
        }

        private void xrPGMultiAbstractReceipt_CustomRowHeight(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotCustomRowHeightEventArgs e)
        {
            int defaultrowheight = e.RowHeight;//Default height
            try
            {
                if (e.Field != null)
                {
                    if (e.ValueType != PivotGridValueType.Total && e.ValueType != PivotGridValueType.GrandTotal)
                    {
                        if (e.Field.Name == fieldLEDGERNAME.Name || e.Field.Name == fieldLEDGERGROUP.Name)
                        {
                            fieldLEDGERNAME.Appearance.Cell.WordWrap = true;
                            fieldLEDGERNAME.Appearance.FieldValue.WordWrap = true;
                            fieldLEDGERGROUP.Appearance.Cell.WordWrap = true;
                            fieldLEDGERGROUP.Appearance.FieldValue.WordWrap = true;

                            e.RowHeight = defaultrowheight;
                            string ledgergroup = string.Empty;
                            string ledgername = e.GetFieldValue(e.Field, e.RowIndex).ToString().Trim();
                           

                            //SizeF size = gr.MeasureString(ledgername, xrPGMultiAbstractReceipt.Styles.CellStyle.Font, fieldLEDGERNAME.Width - 2);
                            SizeF size = gr.MeasureString(ledgername, fieldLEDGERNAME.Appearance.Cell.Font, fieldLEDGERNAME.Width);
                            Int32 RowHeightLedgerName = Convert.ToInt32(size.Height + 0.5);
                            //Int32 RowHeightLedgerName = GetRowHeight(ledgername, fieldLEDGERNAME.Width, e.RowHeight);
                            Int32 RowHeightLedgerGroup = 0;
                            if (fieldLEDGERGROUP.Visible)
                            {
                                ledgergroup = e.GetFieldValue(fieldLEDGERGROUP, e.RowIndex).ToString().Trim();
                                //size = gr.MeasureString(ledgergroup, xrPGMultiAbstractReceipt.Styles.CellStyle.Font, fieldLEDGERGROUP.Width - 2);
                                size = gr.MeasureString(ledgergroup, fieldLEDGERGROUP.Appearance.Cell.Font, fieldLEDGERGROUP.Width);
                                //RowHeightLedgerGroup = GetRowHeight(ledgergroup, fieldLEDGERGROUP.Width, e.RowHeight);
                                RowHeightLedgerGroup = Convert.ToInt32(size.Height + 0.5);
                            }
                            e.RowHeight = Math.Max(RowHeightLedgerName, RowHeightLedgerGroup);
                        }
                        
                    }
                    else if (e.ValueType == PivotGridValueType.Total)
                    {
                        //On 16/09/2022, To hide empty row group (Ledger Group), as unable remove empty ledger group ---------------
                        if (e.RowIndex == 0 && e.Data.GetAvailableFieldValues(fieldLEDGERGROUP) != null &&
                                 ReportProperties.ShowByLedgerGroup == 1 && ReportProperties.ShowByLedger == 1)
                        {
                            //If Ledger Group value is empty,
                            string ledgergroup = (e.GetFieldValue(fieldLEDGERGROUP, e.RowIndex) != null ? e.GetFieldValue(fieldLEDGERGROUP, e.RowIndex).ToString() : string.Empty);
                            if (string.IsNullOrEmpty(ledgergroup))
                            {
                                e.RowHeight = 0;
                            }
                        }
                        //-----------------------------------------------------------------------------------------------------------
                    }
                }
            }
            catch (Exception err)
            {
                e.RowHeight = defaultrowheight;//Default height
                MessageRender.ShowMessage("Not able to set row right " + err.Message);
            }
        }

        private void xrPGMultiAbstractReceipt_PrintHeader(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportHeaderEventArgs e)
        {
            if (e.Field != null)
            {
                DevExpress.XtraPrinting.TextBrick textBrick = e.Brick as DevExpress.XtraPrinting.TextBrick;
                textBrick.Location = new PointF(textBrick.Location.X, 0);
                textBrick.Size = new SizeF(textBrick.Size.Width, 135);

                if (e.Field == fieldLEDGERCODE || e.Field == fieldLEDGERNAME)
                {
                    textBrick.Size = new SizeF(textBrick.Size.Width, 255);
                }
            }
        }

        private void xrPGOpeningClosingBalance_PrintFieldValue(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs e)
        {
            if (e.Field != null)
            {
                if (e.Field.Name == fieldCBName.Name)
                {
                    e.Appearance.BackColor = xrPGGrandTotal.Styles.GrandTotalCellStyle.BackColor;
                }
                else if (e.Field.Name == fieldCBCurrencyName.Name || e.Field.Name == fieldIsCashBank.Name || e.Field.Name == fieldCBAmount.Name ||
                    e.Field.Name == fieldCBBudget.Name || e.Field.Name == fieldCBPercentage.Name)
                {
                    if (xrPGOpeningClosingBalance.OptionsView.ShowRowHeaders == false)
                    {
                        e.Brick.Text = "";
                        e.Brick.BorderWidth = 0;
                        e.Appearance.BackColor = Color.White;
                        DevExpress.XtraPrinting.TextBrick textBrick = e.Brick as DevExpress.XtraPrinting.TextBrick;
                        DevExpress.XtraPrinting.PanelBrick paneltextBrick = e.Brick as DevExpress.XtraPrinting.PanelBrick;
                        
                        if (textBrick != null)
                        {
                            textBrick.Location = new PointF(textBrick.Location.X, 0);
                            textBrick.Size = new SizeF(textBrick.Size.Width, 0);
                        }
                        else if (paneltextBrick != null)
                        {
                            paneltextBrick.Location = new PointF(paneltextBrick.Location.X, 0);
                            paneltextBrick.Size = new SizeF(paneltextBrick.Size.Width, 0);
                        }
                    }
                }
               
            }
            else if (xrPGOpeningClosingBalance.OptionsView.ShowRowHeaders == false)       
            {
                e.Brick.Text = "";
                e.Brick.BorderWidth = 0;
                e.Appearance.BackColor = Color.White;
                DevExpress.XtraPrinting.TextBrick textBrick = e.Brick as DevExpress.XtraPrinting.TextBrick;
                DevExpress.XtraPrinting.PanelBrick paneltextBrick = e.Brick as DevExpress.XtraPrinting.PanelBrick;

                if (textBrick != null)
                {
                    textBrick.Location = new PointF(textBrick.Location.X, 0);
                    textBrick.Size = new SizeF(textBrick.Size.Width, 0);
                }
                else if (paneltextBrick != null)
                {
                    paneltextBrick.Location = new PointF(paneltextBrick.Location.X, 0);
                    paneltextBrick.Size = new SizeF(paneltextBrick.Size.Width, 0);
                }
                
            }
        }

        private void xrPGOpeningClosingBalance_CustomFieldSort(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs e)
        {
            if (e.Field.Name == fieldCBCurrencyName.Name)
            {
                if (e.Value1 != null && e.Value2 != null)
                {
                    string currency1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, reportSetting1.Country.CURRENCY_NAMEColumn.ColumnName).ToString();
                    string currency2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, reportSetting1.Country.CURRENCY_NAMEColumn.ColumnName).ToString();

                    Int32 c1 = GetCurrencyOrder(currency1);
                    Int32 c2 = GetCurrencyOrder(currency2);

                    //e.Result = Comparer.Default.Compare(currency1, currency2);
                    e.Result = Comparer.Default.Compare(c1, c2);
                    e.Handled = true;
                }
            }
        }

        private void xrPGGrandTotal_CustomFieldSort(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs e)
        {
            if (e.Field.Name == fieldGRANTTOTALCURRENCY.Name)
            {
                if (e.Value1 != null && e.Value2 != null)
                {
                    string currency1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, reportSetting1.Country.CURRENCY_NAMEColumn.ColumnName).ToString();
                    string currency2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, reportSetting1.Country.CURRENCY_NAMEColumn.ColumnName).ToString();
                    Int32 c1 = GetCurrencyOrder(currency1);
                    Int32 c2 = GetCurrencyOrder(currency2);

                    //e.Result = Comparer.Default.Compare(currency1, currency2);
                    e.Result = Comparer.Default.Compare(c1, c2);
                    e.Handled = true;
                }
            }
        }

        private Int32 GetCurrencyOrder(string currencyname)
        {
            Int32 currencyorder = 0;
            //if (currencyname.Trim().ToUpper().Contains(AppSetting.CurrencyName.Trim().ToUpper()))
            if (currencyname.Trim().ToUpper().Equals(AppSetting.CurrencyName.Trim().ToUpper()))
            {
                currencyorder = 0;
            }
            else if (currencyname.Trim().ToUpper().Contains(DOLLARNAME))
            {
                currencyorder = 1;
            }
            else if (currencyname.Trim().ToUpper().Contains(EURONAME))
            {
                currencyorder = 2;
            }
            else
            {
                currencyorder = 3;
            }
            return currencyorder;
        }

        private void xrPGOpeningClosingBalance_PrintCell(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportCellEventArgs e)
        {
            if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.Cell)
            {
                if (e.DataField != null && e.DataField.Name == fieldCBAmount.Name)
                {
                    if (e.ColumnField == null)
                    {
                        e.Brick.Text = UtilityMember.NumberSet.ToNumber(CBBalanceInEuro);
                        e.Brick.TextValue = UtilityMember.NumberSet.ToNumber(CBBalanceInEuro);
                        CBBalanceInEuro = UtilityMember.NumberSet.ToDouble(e.Brick.Text);
                    }
                    else
                    {
                        string currencyname = (e.ColumnValue == null ? "" : e.ColumnValue.Text.ToString());
                        double amt = UtilityMember.NumberSet.ToDouble(e.Brick.Text);

                        if (!string.IsNullOrEmpty(currencyname) && amt > 0)
                        {
                            double convertedamt = amt;
                            double exchangerate = 1;
                            if (currencyname.Trim().ToUpper().Contains(AppSetting.CurrencyName.Trim().ToUpper()))
                            {
                                exchangerate = (ReportProperty.Current.AvgEuroExchangeRate <= 0 ? 1 : ReportProperty.Current.AvgEuroExchangeRate);
                            }
                            else if (currencyname.Trim().ToUpper().Contains(DOLLARNAME))
                            {
                                exchangerate = (ReportProperty.Current.AvgEuroDollarExchangeRate <= 0 ? 1 : ReportProperty.Current.AvgEuroDollarExchangeRate);
                            }
                            convertedamt = (amt / exchangerate);

                            CBBalanceInEuro += Math.Round(convertedamt, 2);
                        }
                    }
                }
                else
                {
                    if (e.Brick.TextValue == null || this.UtilityMember.NumberSet.ToDouble(e.Brick.TextValue.ToString()) == 0) { e.Brick.Text = " "; }
                }
            }
        }

        private void xrPGGrandTotal_PrintCell(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportCellEventArgs e)
        {
            if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.Cell)
            {
                if (e.DataField != null && e.DataField.Name == fieldGRANTTOTALAMOUNT.Name)
                {
                    if (e.ColumnField==null)
                    {
                        e.Brick.Text = UtilityMember.NumberSet.ToNumber(TotalBalanceInEuro + CBBalanceInEuro);
                        e.Brick.TextValue = UtilityMember.NumberSet.ToNumber(TotalBalanceInEuro + CBBalanceInEuro);
                        CBBalanceInEuro = TotalBalanceInEuro = 0;
                    }
                }
                else
                {
                    if (e.Brick.TextValue == null || this.UtilityMember.NumberSet.ToDouble(e.Brick.TextValue.ToString()) == 0) { e.Brick.Text = ""; }
                }
            }
        }

    }
}
