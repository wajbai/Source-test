using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.Report.Base;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;
using System.Globalization;
using Bosco.Utility.ConfigSetting;
using Bosco.Report.View;
using DevExpress.XtraSplashScreen;
using System.Linq;
using DevExpress.XtraPivotGrid;

namespace Bosco.Report.ReportObject
{
    public partial class MultiAbstractReceiptsProject : Bosco.Report.Base.ReportHeaderBase
    {
        private Graphics gr = Graphics.FromHwnd(IntPtr.Zero);
        bool IsMultiReceipts = true;
        int ProjectNumber = 1;
        bool rowemptyremoved = false;
        public MultiAbstractReceiptsProject()
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
            ProjectNumber = 1;
            rowemptyremoved = false;
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
                        BindMultiAbstractReceiptSource();
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
                    BindMultiAbstractReceiptSource();
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
        }

        #endregion

        public void BindMultiAbstractReceiptSource()
        {
            lblProjectName.Text = string.Empty;
            IsMultiReceipts = (this.ReportProperties.ReportId == "RPT-165" ? false: true);
            lblProjectName.Text = ReportProperty.Current.ProjectNamewithSno;
            
            setHeaderTitleAlignment();
            SetReportTitle();
            this.HideDateRange = true;
            this.HideReportSubTitle = false;
            grpOpeningBalane.Visible = xrSubOpeningBalanceMulti.Visible = xrLabelCLBal.Visible = false;
            detailClosingBalance.Visible = xrSubClosingBalanceMulti.Visible = xrLabelCLBal.Visible = false;
            
            if (IsMultiReceipts)
            {
                grpOpeningBalane.Visible = xrSubOpeningBalanceMulti.Visible = true;
                xrLabelCLBal.Visible = false;    
            }
            else
            {
                detailClosingBalance.Visible = xrSubClosingBalanceMulti.Visible = xrLabelCLBal.Visible = true;
            }

            ResultArgs resultArgs = GetReportSource(IsMultiReceipts);
            if (resultArgs.Success)
            {
                DataTable dtReceiptPaymentYear = resultArgs.DataSource.Table;

                if (dtReceiptPaymentYear != null)
                {
                    dtReceiptPaymentYear.TableName = "MultiAbstract";
                    xrPGMultiAbstractReceipt.DataSource = dtReceiptPaymentYear.DefaultView;
                    xrPGMultiAbstractReceipt.DataMember = dtReceiptPaymentYear.TableName;
                }

                if (IsMultiReceipts) //for Multi Year Receipts Opening Balance
                {
                    AccountBalanceMultiProject accountBalanceMultiProject = xrSubOpeningBalanceMulti.ReportSource as AccountBalanceMultiProject;
                    SetReportSetting(dtReceiptPaymentYear.DefaultView, accountBalanceMultiProject);
                    accountBalanceMultiProject.BankClosedDate = this.ReportProperties.DateFrom; //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                    //xrLabelCLBal.Text = (IsMultiReceipts ? "Opening Balance" : "Closing Balance");
                    accountBalanceMultiProject.ShowColumnHeader = true;
                    accountBalanceMultiProject.BindBalance(true);
                }
                else //for Multi Year Payments closing balance
                {
                    AccountBalanceMultiProject accountBalanceMultiProject = xrSubClosingBalanceMulti.ReportSource as AccountBalanceMultiProject;
                    SetReportSetting(dtReceiptPaymentYear.DefaultView, accountBalanceMultiProject);
                    accountBalanceMultiProject.BankClosedDate = this.ReportProperties.DateFrom; //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                    //xrLabelCLBal.Text = (IsMultiReceipts ? "Opening Balance" : "Closing Balance");
                    accountBalanceMultiProject.BindBalance(false);
                }
            }
        }

        private ResultArgs GetReportSource(bool ReceiptReport)
        {
            ResultArgs resultArgs = null;
            string sqlMultiAbstractReceipts = this.GetReportSQL(SQL.ReportSQLCommand.Report.MultiAbstractProject);
            string liquidityGroupIds = this.GetLiquidityGroupIds();

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, (ReceiptReport?  TransType.RC.ToString() :TransType.PY.ToString() ));
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, liquidityGroupIds);
                dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, (ReceiptReport?  TransMode.CR.ToString(): TransMode.DR.ToString()));
                //dataManager.Parameters.Add(this.reportSetting1.MultiAbstract.NO_OF_YEARColumn, this.ReportProperties.NoOfYears);

                //On 06/12/2024 - To set currency details -----------------------------------------------------------------------------------
                if (this.AppSetting.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, "0");
                }
                //---------------------------------------------------------------------------------------------------------------------------

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlMultiAbstractReceipts);
            }

            return resultArgs;
        }

        private ResultArgs GetJournalSource(bool ReceiptReport)
        {
            ResultArgs resultArgs = null;
            string sqlMonthlyJournalReceipt = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.FinalReceiptJournal);
            string dateProgress = this.GetProgressiveDate(this.ReportProperties.DateFrom);
            string liquidityGroupIds = this.GetLiquidityGroupIds();

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_PROGRESS_FROMColumn, dateProgress);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, TransType.JN.ToString());
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, liquidityGroupIds);
                dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, (ReceiptReport ? TransMode.CR.ToString() : TransMode.DR.ToString()));
                int LedgerPaddingRequired = (ReportProperties.ShowLedgerCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;
                int GroupPaddingRequired = (ReportProperties.ShowGroupCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;

                dataManager.Parameters.Add(this.ReportParameters.SHOWLEDGERCODEColumn, LedgerPaddingRequired);
                dataManager.Parameters.Add(this.ReportParameters.SHOWGROUPCODEColumn, GroupPaddingRequired);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlMonthlyJournalReceipt);
            }

            return resultArgs;
        }

        /// <summary>
        /// On 13/08/2018, to show TDS on FD interest for accumulate interest
        /// We show FD renewal accumulated jounral entry interest amount in receipt side
        /// After adding TDS entry along with FD interest, for Accumulated interest TDS amount should be added with Payment side
        /// 
        /// this method will retrn entries which are made on TDS on FD intererest ledger while renewing accumulated intrest
        /// </summary>
        /// <returns></returns>
        private ResultArgs GetJournalTDSonFDInterestAmount()
        {
            ResultArgs resultArgs = null;
            string sqlReceiptJournal = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.FetchTDSOnFDInterest);
            string dateProgress = this.GetProgressiveDate(this.ReportProperties.DateFrom);
            string liquidityGroupIds = this.GetLiquidityGroupIds();

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_PROGRESS_FROMColumn, dateProgress);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, TransType.JN.ToString());
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, liquidityGroupIds);
                dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, TransMode.CR.ToString());
                int LedgerPaddingRequired = (ReportProperties.ShowLedgerCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;
                int GroupPaddingRequired = (ReportProperties.ShowGroupCode == 0 && ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;

                dataManager.Parameters.Add(this.ReportParameters.SHOWLEDGERCODEColumn, LedgerPaddingRequired);
                dataManager.Parameters.Add(this.ReportParameters.SHOWGROUPCODEColumn, GroupPaddingRequired);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlReceiptJournal);
            }
            return resultArgs;
        }

        private void BindGrandTotal(DataTable dtGrantTotal)
        {
            DataTable dtGrantTotalBalance = new DataTable();
            if (IsMultiReceipts)
            {
                AccountBalanceMultiProject accountBalanceMulti = xrSubOpeningBalanceMulti.ReportSource as AccountBalanceMultiProject;
                dtGrantTotalBalance = accountBalanceMulti.GrantTotalBalance;
            }
            else
            {
                AccountBalanceMultiProject accountBalanceMulti = xrSubClosingBalanceMulti.ReportSource as AccountBalanceMultiProject;
                dtGrantTotalBalance = accountBalanceMulti.GrantTotalBalance;
            }

            int rowIdx = 0;
            double amount = 0;

            foreach (DataRow drGrantTotalBal in dtGrantTotalBalance.Rows)
            {
                rowIdx = dtGrantTotalBalance.Rows.IndexOf(drGrantTotalBal);

                if (rowIdx < dtGrantTotal.Rows.Count)
                {
                    DataRow drGrantTotal = dtGrantTotal.Rows[rowIdx];
                    amount = this.UtilityMember.NumberSet.ToDouble(drGrantTotal[reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName].ToString());
                    amount += this.UtilityMember.NumberSet.ToDouble(drGrantTotalBal[reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName].ToString());

                    drGrantTotal.BeginEdit();
                    drGrantTotal[reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName] = amount;
                    drGrantTotal.EndEdit();
                }
            }

            dtGrantTotal.AcceptChanges();
            dtGrantTotal.TableName = "MultiAbstract";
            xrPGGrandTotal.DataSource = dtGrantTotal;
            xrPGGrandTotal.DataMember = dtGrantTotal.TableName;
        }

        private void xrPGMultiAbstractReceipt_CustomFieldSort(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs e)
        {
         
        }

        private void xrPGMultiAbstractReceipt_FieldValueDisplayText(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
            {
                e.DisplayText = "Total " + (IsMultiReceipts ? "Receipts" : "Payments");
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
                else if (e.Field.Name == fieldPROJECTID.Name)
                {
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
                    e.Appearance.BorderColor = xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.BorderColor;
                    e.Appearance.BackColor = xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.BackColor;
                    e.Appearance.Font = xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font;
                    Int32 projectid = UtilityMember.NumberSet.ToInteger(e.Brick.Text);
                    e.Brick.Text = "P" + ProjectNumber.ToString();
                    ProjectNumber++;
                }

                if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
                {
                    //e.Appearance.ForeColor = xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.ForeColor;
                    e.Appearance.Font = xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.Font;
                    //e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;

                    e.Appearance.Font = fieldLEDGERGROUP.Appearance.FieldValue.Font;
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
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
                else if (e.Field.Name == fieldGRANTTOTALPROJECTID.Name)
                {
                    if (xrPGGrandTotal.OptionsView.ShowRowHeaders == false)
                    {
                        e.Brick.Text = "";
                        e.Brick.BorderWidth = 0;
                        e.Appearance.BackColor = Color.White;
                        DevExpress.XtraPrinting.TextBrick textBrick = e.Brick as DevExpress.XtraPrinting.TextBrick;
                        textBrick.Size = new SizeF(textBrick.Size.Width, 0);
                    }
                }
            }
        }

        private void xrPGMultiAbstractReceipt_PrintCell(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportCellEventArgs e)
        {
            if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.TotalCell)
            {
                //e.Appearance.ForeColor = xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.ForeColor;
                //e.Appearance.Font = xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.Font;
                e.Appearance.Font = xrPGMultiAbstractReceipt.Styles.GrandTotalCellStyle.Font;
            }

            if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.Cell)
            {
                if (e.Brick.TextValue == null || this.UtilityMember.NumberSet.ToDouble(e.Brick.TextValue.ToString()) == 0) { e.Brick.Text = ""; }
            }
        }

        private void xrPGMultiAbstractReceipt_AfterPrint(object sender, EventArgs e)
        {
            DataTable dtGrantTotal = new DataTable();
            dtGrantTotal.Columns.Add(reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName, typeof(string));
            dtGrantTotal.Columns.Add(reportSetting1.MultiAbstract.PROJECT_IDColumn.ColumnName, typeof(int));
            dtGrantTotal.Columns.Add(reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName, typeof(double));
            object oTotVal = null;
            double totVal = 0;
            int row = xrPGMultiAbstractReceipt.RowCount - 1;

            for (int col = 0; col < xrPGMultiAbstractReceipt.ColumnCount; col++)
            {
                oTotVal = xrPGMultiAbstractReceipt.GetCellValue(col, row);
                totVal = this.UtilityMember.NumberSet.ToDouble(oTotVal.ToString());
                DataRow drGrantTotal = dtGrantTotal.NewRow();
                drGrantTotal[reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName] = "Grand Total";
                drGrantTotal[reportSetting1.MultiAbstract.PROJECT_IDColumn.ColumnName] = (col + 1);
                drGrantTotal[reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName] = totVal;
                dtGrantTotal.Rows.Add(drGrantTotal);
            }

            dtGrantTotal.AcceptChanges();
            BindGrandTotal(dtGrantTotal);
        }

        private void SetReportSetting(DataView dvReceipt, AccountBalanceMultiProject accountBalanceMultiYear)
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

            //On 14/09/2022, to show Ledger Group Total-----------------------
            xrPGMultiAbstractReceipt.OptionsView.ShowRowTotals = false;
            if (ReportProperties.ShowByLedgerGroup == 1 && ReportProperties.ShowByLedger == 1)
            {
                xrPGGrandTotal.KeepTogether = true;
                xrPGMultiAbstractReceipt.OptionsView.ShowRowTotals = true;
                xrPGMultiAbstractReceipt.Appearance.FieldValueTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
                fieldGROUPCODE.TotalsVisibility = PivotTotalsVisibility.None;
                fieldLEDGERCODE.TotalsVisibility = PivotTotalsVisibility.None;
                xrPGMultiAbstractReceipt.OptionsView.ShowTotalsForSingleValues = true;
                xrPGMultiAbstractReceipt.Styles.TotalCellStyle = xrPGMultiAbstractReceipt.Styles.GrandTotalCellStyle;
                xrPGMultiAbstractReceipt.Styles.TotalCellStyle.Padding = new DevExpress.XtraPrinting.PaddingInfo(5);
                //xrPGMultiAbstractReceipt.OptionsView.RowTotalsLocation = PivotRowTotalsLocation.Far;
            }
            //----------------------------------------------------------------

            //On 14/03/2018, To set/reset amount column width based on Showcode
            fieldGROUPCODE.Width = 70;//35;
            fieldLEDGERCODE.Width = 70;//35;
            if (isLedgerCodeVisible || isGroupCodeVisible)
            {
                fieldLEDGERGROUP.Width = 130; //90;
                fieldLEDGERNAME.Width = 220; //121 130;
                fieldPROJECTID.Width = (isGroupVisible && isLedgerVisible) ? 95 : 105; //63 : 73; add 25
            }
            else
            {
                fieldLEDGERGROUP.Width = 125; //90;
                fieldLEDGERNAME.Width = 225; //118 130;
                fieldPROJECTID.Width = (isGroupVisible && isLedgerVisible) ? 95 : 110; //66 : 76; add 25
            }

            //on 01/04/2019, take fist leder name, it will be used for dummmary projects
            //string LedgerDummyName = string.Empty;
            //Int32 LedgerDummyId = 0;
            //string LedgerDummyCode = string.Empty;
            //Int32 DummyGroupId = 0;
            //string DummyGroupName = string.Empty;

            //if (dvReceipt.Count > 0)
            //{
            //    LedgerDummyName = dvReceipt[0][reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName].ToString();
            //    LedgerDummyId = UtilityMember.NumberSet.ToInteger(dvReceipt[0][reportSetting1.MultiAbstract.LEDGER_IDColumn.ColumnName].ToString());
            //    LedgerDummyCode = dvReceipt[0][reportSetting1.MultiAbstract.LEDGER_CODEColumn.ColumnName].ToString();
            //    DummyGroupId = UtilityMember.NumberSet.ToInteger(dvReceipt[0][reportSetting1.MultiAbstract.LEDGER_GROUPColumn.ColumnName].ToString());
            //    DummyGroupName = dvReceipt[0][reportSetting1.MultiAbstract.LEDGER_GROUPColumn.ColumnName].ToString();
            //}

            //Add empty source if there is no records
            foreach (string projectid in this.ReportProperties.Project.Split(','))
            {
                int ProId = UtilityMember.NumberSet.ToInteger(projectid);
                dvReceipt.RowFilter = reportSetting1.MultiAbstract.PROJECT_IDColumn.ColumnName + "=" + ProId + " AND " 
                         + reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName + " <> 0";
                if (dvReceipt.Count == 0)
                {
                    dvReceipt.RowFilter = string.Empty;
                    DataRowView drvReceipt = dvReceipt.AddNew();
                    drvReceipt.BeginEdit();
                    drvReceipt[reportSetting1.MultiAbstract.PROJECT_IDColumn.ColumnName] = ProId;
                    drvReceipt[reportSetting1.MultiAbstract.PROJECTColumn.ColumnName] = GetProjectName(ProId);
                    drvReceipt[reportSetting1.MultiAbstract.GROUP_IDColumn.ColumnName] = 0;
                    drvReceipt[reportSetting1.MultiAbstract.LEDGER_GROUPColumn.ColumnName] = string.Empty ;
                    drvReceipt[reportSetting1.MultiAbstract.LEDGER_IDColumn.ColumnName] = 0;
                    drvReceipt[reportSetting1.MultiAbstract.LEDGER_CODEColumn.ColumnName] = string.Empty;
                    drvReceipt[reportSetting1.MultiAbstract.LEDGER_NAMEColumn.ColumnName] = string.Empty;
                    drvReceipt[reportSetting1.MultiAbstract.AMOUNTColumn.ColumnName] = 0;
                    drvReceipt[reportSetting1.MonthlyAbstract.HAS_TRANSColumn.ColumnName] = 1;
                    drvReceipt.EndEdit();
                }
            }

            //Attach / Detach all ledgers
            //dvReceipt.RowFilter = reportSetting1.MonthlyAbstract.HAS_TRANSColumn.ColumnName + " = 1";
            //if (ReportProperties.IncludeAllLedger == 1)
            //{
            //    dvReceipt.RowFilter = reportSetting1.MonthlyAbstract.HAS_TRANSColumn.ColumnName + " IN (1,0)";
            //}
            dvReceipt.RowFilter = reportSetting1.MonthlyAbstract.HAS_TRANSColumn.ColumnName + " IN (1)";
            
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
            if (fieldGROUPCODE.Visible) { rowWidth = fieldGROUPCODE.Width; }
            if (fieldLEDGERGROUP.Visible) { rowWidth += fieldLEDGERGROUP.Width; }
            if (fieldLEDGERCODE.Visible) { rowWidth += fieldLEDGERCODE.Width; }
            if (fieldLEDGERNAME.Visible) { rowWidth += fieldLEDGERNAME.Width; }
            fieldGRANTTOTALPARTICULARS.Width = rowWidth;
            fieldGRANTTOTALPROJECTID.Width = fieldPROJECTID.Width;
            fieldGRANTTOTALAMOUNT.Width = fieldAMOUNT.Width;

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
            float fontsize = float.Parse("8.5");
            fieldLEDGERGROUP.Appearance.FieldValue.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, fontsize, FontStyle.Bold);

            if (xrPGMultiAbstractReceipt.Styles.TotalCellStyle != null)
            {
                xrPGMultiAbstractReceipt.Styles.TotalCellStyle.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                xrPGMultiAbstractReceipt.Styles.TotalCellStyle.Font = new Font(xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font.FontFamily, fontsize, FontStyle.Bold);
            }

            //Set Subreport Properties for opening
            xrSubOpeningBalanceMulti.LeftF = xrPGMultiAbstractReceipt.LeftF;
            
            //Set Subreport Properties for closing 
            xrSubClosingBalanceMulti.LeftF = xrPGMultiAbstractReceipt.LeftF;
            accountBalanceMultiYear.LeftPosition = (xrPGMultiAbstractReceipt.LeftF - 5);
            accountBalanceMultiYear.GroupCodeColumnWidth = fieldGROUPCODE.Width;
            accountBalanceMultiYear.GroupNameColumnWidth = fieldLEDGERGROUP.Width;
            accountBalanceMultiYear.LedgerCodeColumnWidth = fieldLEDGERCODE.Width;
            accountBalanceMultiYear.LedgerNameColumnWidth = fieldLEDGERNAME.Width;
            accountBalanceMultiYear.AmountColumnWidth = fieldPROJECTID.Width;
            accountBalanceMultiYear.ApplyParentReportStyle = xrPGMultiAbstractReceipt.Styles;
            accountBalanceMultiYear.ShowColumnHeader = false;
        }

        private void xrPGMultiAbstractReceipt_CustomFieldSort_1(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs e)
        {
            if (e.Field.Name == fieldPROJECTID.Name)
            {
                if (e.Value1 != null && e.Value2 != null)
                {
                    Int32 projectid1 = UtilityMember.NumberSet.ToInteger(e.GetListSourceColumnValue(e.ListSourceRowIndex1, reportSetting1.AccountBalance.PROJECT_IDColumn.ColumnName).ToString());
                    Int32 projectid2 = UtilityMember.NumberSet.ToInteger(e.GetListSourceColumnValue(e.ListSourceRowIndex2, reportSetting1.AccountBalance.PROJECT_IDColumn.ColumnName).ToString());

                    e.Result = Comparer.Default.Compare(projectid1, projectid2);
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

                //for (int i = e.GetCellCount(false) - 1; i >= 0; i--)
                //{
                //    DevExpress.XtraReports.UI.PivotGrid.FieldValueCell cell = e.GetCell(false, i);
                //    if (cell != null)
                //    {
                //        if (object.Equals(cell.Value, string.Empty))
                //        {
                //           e.Remove(cell);
                //        }
                //    }
                //}
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
    }
}
