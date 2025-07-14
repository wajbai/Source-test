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
using System.Reflection;

namespace Bosco.Report.ReportObject
{
    public partial class MultiAbstractReceiptsProjectCashBank : Bosco.Report.Base.ReportHeaderBase
    {
        private Graphics gr = Graphics.FromHwnd(IntPtr.Zero);
        bool IsMultiReceipts = true;
        int ProjectNumber = 1;
        bool rowemptyremoved = false;
        DataTable dtProjectsList = new DataTable();
        public MultiAbstractReceiptsProjectCashBank()
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
            IsMultiReceipts = (this.ReportProperties.ReportId == "RPT-215" ? false : true);
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
                        BindMultiAbstractReceiptSource(IsMultiReceipts);
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
                    BindMultiAbstractReceiptSource(IsMultiReceipts);
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
        }

        #endregion

        public void BindMultiAbstractReceiptSource(bool IsReceipt)
        {
            lblProjectName.Text = string.Empty;
            IsMultiReceipts = IsReceipt;
            
            //On 15/11/2024
            if (this.ReportProperties.ReportId == "RPT-228") 
                this.fieldPROJECTID.FieldName = "PROJECT_CATOGORY_ITRGROUP_ID";
            else
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
            }
            else
            {
                detailClosingBalance.Visible = xrSubClosingBalanceMulti.Visible = true;
            }

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
                
                
                resultArgs = this.GetProjects();
                if (resultArgs != null && resultArgs.Success)
                {
                    dtProjectsList = resultArgs.DataSource.Table;
                }

                if (IsMultiReceipts) //for Multi Year Receipts Opening Balance
                {
                    AccountBalanceMultiProjectCashBank accountBalanceMultiProject = xrSubOpeningBalanceMulti.ReportSource as AccountBalanceMultiProjectCashBank;
                    SetReportSetting(dtReceiptPaymentYear.DefaultView, accountBalanceMultiProject);
                    accountBalanceMultiProject.BankClosedDate = this.ReportProperties.DateFrom; //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                    //xrLabelCLBal.Text = (IsMultiReceipts ? "Opening Balance" : "Closing Balance");
                    accountBalanceMultiProject.ShowColumnHeader = true;
                    accountBalanceMultiProject.ProjectListDetails = dtProjectsList;
                    accountBalanceMultiProject.BindBalance(true);
                }
                else //for Multi Year Payments closing balance
                {
                    AccountBalanceMultiProjectCashBank accountBalanceMultiProject = xrSubClosingBalanceMulti.ReportSource as AccountBalanceMultiProjectCashBank;
                    SetReportSetting(dtReceiptPaymentYear.DefaultView, accountBalanceMultiProject);
                    accountBalanceMultiProject.BankClosedDate = this.ReportProperties.DateFrom; //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                    //xrLabelCLBal.Text = (IsMultiReceipts ? "Opening Balance" : "Closing Balance");
                    accountBalanceMultiProject.ProjectListDetails = dtProjectsList;
                    accountBalanceMultiProject.BindBalance(false);
                }
                
                
            }
        }

        private void AttachProjectSummary()
        {
            xrTblTitleProjectSummary.LeftF = xrPGMultiAbstractReceipt.LeftF;
            xrTblTitleProjectSummary.Visible = xrTblProjectSummary.Visible = false;
            xrTblProjectSummary.Rows.Clear();
            xrcellProjectSummaryTitile.Text = "Project Cash & Bank " + (IsMultiReceipts ? "Receipts" : "Payments") +" Summary";

            if (xrPGMultiAbstractReceipt.DataSource != null)
            {
                xrTblTitleProjectSummary.Visible = xrTblProjectSummary.Visible = true;
                float cashwidth = fieldAMOUNTCASH.Width +5 ;
                float bankwidth = fieldAMOUNTBANK.Width +5 ;
                float amountwidth = (cashwidth + bankwidth);
                float captionwidth = (fieldLEDGERCODE.Width + fieldLEDGERNAME.Width + + (fieldLEDGERGROUP.Visible ? fieldLEDGERGROUP.Width : 0)) + 10;
                
                xrTblTitleProjectSummary.WidthF = captionwidth + cashwidth + bankwidth + amountwidth;
                xrTblProjectSummary.WidthF = xrTblTitleProjectSummary.WidthF ;
                
                DataTable dtReportData = xrPGMultiAbstractReceipt.DataSource as DataTable;
                DataTable dtReportSummary = dtReportData.DefaultView.ToTable();

                string[] arrProjectSummary = new string[] { reportSetting1.ProfitandLossbyHouse.PROJECTColumn.ColumnName, reportSetting1.AccountBalance.PROJECT_IDColumn.ColumnName };
                DataTable dtProjectSummary = dtReportSummary.DefaultView.ToTable(true, arrProjectSummary);
                dtProjectSummary.DefaultView.Sort = reportSetting1.AccountBalance.PROJECT_IDColumn.ColumnName;
                dtProjectSummary = dtProjectSummary.DefaultView.ToTable();
                xrcellProjectTitle.WidthF = captionwidth;
                xrcellProjectTotalTitlelCash.WidthF = cashwidth;
                xrcellProjectTotalTitleBank.WidthF = bankwidth;
                xrcellProjectTotalAmount.WidthF = amountwidth;
                
                foreach (DataRow dr in dtProjectSummary.Rows)
                {
                    xrTblProjectSummary.BeginInit();
                    XRTableRow xrRowProjectSummary = new XRTableRow();
                    XRTableCell cellProject = new XRTableCell();
                    XRTableCell cellProjectCashAmount = new XRTableCell();
                    XRTableCell cellProjectBankAmount = new XRTableCell();
                    XRTableCell cellProjectAmount = new XRTableCell();

                    cellProject.Font = xrcellProjectTitle.Font;
                    cellProject.Padding = xrcellProjectTitle.Padding;
                    cellProject.Borders = xrcellProjectTitle.Borders;
                    cellProject.BorderColor = xrcellProjectTitle.BorderColor;
                    cellProject.WidthF = captionwidth;

                    cellProjectCashAmount.Font= cellProjectBankAmount.Font= cellProjectAmount.Font = xrcellProjectTotalAmount.Font;
                    cellProjectCashAmount.Padding = cellProjectBankAmount.Padding = cellProjectAmount.Padding = xrcellProjectTotalAmount.Padding;
                    cellProjectCashAmount.TextAlignment = cellProjectBankAmount.TextAlignment = cellProjectAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                    cellProjectCashAmount.Borders = cellProjectBankAmount.Borders = cellProjectAmount.Borders = xrcellProjectTotalAmount.Borders;
                    cellProjectCashAmount.BorderColor = cellProjectBankAmount.BorderColor = cellProjectAmount.BorderColor = xrcellProjectTotalAmount.BorderColor;
                    cellProjectAmount.WidthF = amountwidth;
                    cellProjectCashAmount.WidthF = cashwidth;
                    cellProjectBankAmount.WidthF = bankwidth;
                    

                    Int32 projectid = UtilityMember.NumberSet.ToInteger(dr[reportSetting1.AccountBalance.PROJECT_IDColumn.ColumnName].ToString());
                    string project = dr[reportSetting1.ProfitandLossbyHouse.PROJECTColumn.ColumnName].ToString();
                    string filter = reportSetting1.AccountBalance.PROJECT_IDColumn.ColumnName + " = " + projectid;
                    double cashtotal = UtilityMember.NumberSet.ToDouble(dtReportSummary.Compute("SUM(" + reportSetting1.MultiAbstract.AMOUNT_CASHColumn.ColumnName + ")", filter).ToString());
                    double banktotal = UtilityMember.NumberSet.ToDouble(dtReportSummary.Compute("SUM(" + reportSetting1.MultiAbstract.AMOUNT_BANKColumn.ColumnName + ")", filter).ToString());
                    cellProject.Text = project;
                    cellProjectCashAmount.Text = UtilityMember.NumberSet.ToNumber(cashtotal);
                    cellProjectBankAmount.Text = UtilityMember.NumberSet.ToNumber(banktotal);
                    cellProjectAmount.Text = UtilityMember.NumberSet.ToNumber(cashtotal + banktotal);

                    //cellProjectCashAmount.XlsxFormatString = "n";
                    //cellProjectBankAmount.XlsxFormatString = "n";
                    xrRowProjectSummary.Cells.Add(cellProject);
                    if (this.ReportProperties.Consolidated == 0)
                    {
                        xrRowProjectSummary.Cells.Add(cellProjectCashAmount);
                        xrRowProjectSummary.Cells.Add(cellProjectBankAmount);
                    }
                    xrRowProjectSummary.Cells.Add(cellProjectAmount);
                    xrTblProjectSummary.Rows.Add(xrRowProjectSummary);
                    xrTblProjectSummary.EndInit();
                }
            }

            if (this.ReportProperties.Consolidated == 1)
            {
                /*xrTblTitleProjectSummary.SuspendLayout();
                if (xrRowxrProjectTitleSummary.Cells.Contains(xrcellProjectTotalTitlelCash))
                    xrRowxrProjectTitleSummary.Cells.Remove(xrRowxrProjectTitleSummary.Cells[xrcellProjectTotalTitlelCash.Name]);

                if (xrRowxrProjectTitleSummary.Cells.Contains(xrcellProjectTotalTitleBank))
                    xrRowxrProjectTitleSummary.Cells.Remove(xrRowxrProjectTitleSummary.Cells[xrcellProjectTotalTitleBank.Name]);
                xrTblTitleProjectSummary.PerformLayout();*/
            }

        }

        private ResultArgs GetReportSource(bool ReceiptReport)
        {
            ResultArgs resultArgs = null;
            string sqlMultiAbstractReceipts = this.GetReportSQL(SQL.ReportSQLCommand.Report.MultiAbstractProjectCashBank);
            if (this.ReportProperties.ReportId == "RPT-228") sqlMultiAbstractReceipts = this.GetReportSQL(SQL.ReportSQLCommand.Report.MonitorMultiAbstractProjectCashBank);
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
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlMultiAbstractReceipts);
            }

            return resultArgs;
        }

        private DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist)
        {
            DataTable dt = new DataTable();
            PropertyInfo[] columns = null;

            if (Linqlist == null) return dt;

            foreach (T Record in Linqlist)
            {

                if (columns == null)
                {
                    columns = ((Type)Record.GetType()).GetProperties();
                    foreach (PropertyInfo GetProperty in columns)
                    {
                        Type colType = GetProperty.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                               == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dt.Columns.Add(new DataColumn(GetProperty.Name, colType));
                    }
                }

                DataRow dr = dt.NewRow();

                foreach (PropertyInfo pinfo in columns)
                {
                    dr[pinfo.Name] = pinfo.GetValue(Record, null) == null ? DBNull.Value : pinfo.GetValue
                           (Record, null);
                }

                dt.Rows.Add(dr);
            }
            return dt;
        } 

        private void BindGrandTotal(DataTable dtGrantTotal)
        {
            DataTable dtGrantTotalBalance = new DataTable();
            if (IsMultiReceipts)
            {
                AccountBalanceMultiProjectCashBank accountBalanceMulti = xrSubOpeningBalanceMulti.ReportSource as AccountBalanceMultiProjectCashBank;
                dtGrantTotalBalance = accountBalanceMulti.GrantTotalBalance;
            }
            else
            {
                AccountBalanceMultiProjectCashBank accountBalanceMulti = xrSubClosingBalanceMulti.ReportSource as AccountBalanceMultiProjectCashBank;
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
                if (e.DisplayText =="Grand Total")
                    e.DisplayText = IsMultiReceipts ? "Total Receipts" : "Total Payments";
                else
                    e.DisplayText = e.DisplayText.Replace("Grand", string.Empty);
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
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
                    e.Appearance.BorderColor = xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.BorderColor;
                    e.Appearance.BackColor = xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.BackColor;
                    e.Appearance.Font = xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font;
                    Int32 projectid = UtilityMember.NumberSet.ToInteger(e.Brick.Text);
                    string ProjectCategory = string.Empty;
                    string ProjectITRGroup = string.Empty;
                    if (dtProjectsList != null && dtProjectsList.Rows.Count>0)
                    {
                        dtProjectsList.DefaultView.RowFilter = string.Empty;
                        if (ReportProperties.ReportId == "RPT-228")
                        {
                            dtProjectsList.DefaultView.RowFilter = "PROJECT_CATOGORY_ITRGROUP_ID = " + projectid;
                            if (dtProjectsList.DefaultView.Count > 0)
                            {
                                ProjectITRGroup = dtProjectsList.DefaultView[0]["PROJECT_CATOGORY_ITRGROUP"].ToString().Trim();
                                ProjectCategory = string.Empty;
                                e.Brick.Text = ProjectITRGroup;
                            }
                        }
                        else
                        {
                            dtProjectsList.DefaultView.RowFilter = "PROJECT_ID = " + projectid;
                            if (dtProjectsList.DefaultView.Count > 0)
                            {
                                ProjectCategory = dtProjectsList.DefaultView[0]["PROJECT_CATOGORY_NAME"].ToString().Trim();
                                ProjectITRGroup = string.Empty;
                                e.Brick.Text = "P" + ProjectNumber.ToString() + (string.IsNullOrEmpty(ProjectCategory) ? "" : " (" + ProjectCategory + ")");
                            }
                        }
                    }
                    
                    e.Brick.TextValue = e.Brick.Text;
                    ProjectNumber++;
                }
                else if (e.Field.Name == fieldAMOUNTCASH.Name || e.Field.Name == fieldAMOUNTBANK.Name)
                {
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
                    e.Appearance.BorderColor = xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.BorderColor;
                    e.Appearance.BackColor = xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.BackColor;
                    e.Appearance.Font = xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font;
                }

                if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
                {
                    //e.Appearance.ForeColor = xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.ForeColor;
                    e.Appearance.Font = xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.Font;
                    //e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;

                    e.Appearance.Font = fieldLEDGERGROUP.Appearance.FieldValue.Font;
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
                }
                else if (e.ValueType == PivotGridValueType.GrandTotal)
                {
                    e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
                    //e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
                    e.Brick.SeparableHorz = true;
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
            if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.TotalCell ||
                e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.GrandTotalCell)
            {
                //e.Appearance.BackColor = xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.BackColor;
                //e.Appearance.Font = xrPGMultiAbstractReceipt.Styles.HeaderGroupLineStyle.Font;
                e.Appearance.Font = xrPGMultiAbstractReceipt.Styles.FieldHeaderStyle.Font;
                e.Appearance.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            }

            if (e.RowValue.ItemType == DevExpress.XtraPivotGrid.Data.PivotFieldValueItemType.Cell)
            {
                if (e.Brick.TextValue == null || this.UtilityMember.NumberSet.ToDouble(e.Brick.TextValue.ToString()) == 0) { e.Brick.Text = ""; }

                e.Appearance.Font = xrPGMultiAbstractReceipt.Styles.CellStyle.Font;
            }
        }

        private void xrPGMultiAbstractReceipt_AfterPrint(object sender, EventArgs e)
        {
            //xrPGGrandTotal.Visible = false;
                       
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

        private void SetReportSetting(DataView dvReceipt, AccountBalanceMultiProjectCashBank accountBalanceMultiYear)
        {
            this.VerticalContentSplitting = DevExpress.XtraPrinting.VerticalContentSplitting.Smart;
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
            xrPGMultiAbstractReceipt.Styles.GrandTotalCellStyle.BackColor = fieldPROJECTID.Appearance.FieldHeader.BackColor;
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
                         + "(" + reportSetting1.MultiAbstract.AMOUNT_CASHColumn.ColumnName + " <> 0 AND " + reportSetting1.MultiAbstract.AMOUNT_BANKColumn.ColumnName + " <> 0)";
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
                    drvReceipt[reportSetting1.MultiAbstract.AMOUNT_CASHColumn.ColumnName] = 0;
                    drvReceipt[reportSetting1.MultiAbstract.AMOUNT_BANKColumn.ColumnName] = 0;
                    drvReceipt[reportSetting1.MonthlyAbstract.HAS_TRANSColumn.ColumnName] = 1;


                    if (this.ReportProperties.ReportId == "RPT-228" && dtProjectsList!=null)
                    {
                        drvReceipt["PROJECT_CATOGORY_ITRGROUP_ID"] = 0;
                        drvReceipt["PROJECT_CATOGORY_ITRGROUP"] = string.Empty;

                        dtProjectsList.DefaultView.RowFilter = string.Empty;
                        dtProjectsList.DefaultView.RowFilter = reportSetting1.MultiAbstract.PROJECT_IDColumn.ColumnName + "="+ ProId ;
                        if (dtProjectsList.DefaultView.Count > 0)
                        {
                            drvReceipt["PROJECT_CATOGORY_ITRGROUP_ID"] = UtilityMember.NumberSet.ToInteger(dtProjectsList.DefaultView[0]["PROJECT_CATOGORY_ITRGROUP_ID"].ToString());
                            drvReceipt["PROJECT_CATOGORY_ITRGROUP"] = (dtProjectsList.DefaultView[0]["PROJECT_CATOGORY_ITRGROUP_ID"]==null ? string.Empty : dtProjectsList.DefaultView[0]["PROJECT_CATOGORY_ITRGROUP_ID"].ToString());
                        }
                        
                    }
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
            xrPGGrandTotal.WidthF = xrPGMultiAbstractReceipt.WidthF;
            if (fieldGROUPCODE.Visible) { rowWidth = fieldGROUPCODE.Width; }
            if (fieldLEDGERGROUP.Visible) { rowWidth += fieldLEDGERGROUP.Width; }
            if (fieldLEDGERCODE.Visible) { rowWidth += fieldLEDGERCODE.Width; }
            if (fieldLEDGERNAME.Visible) { rowWidth += fieldLEDGERNAME.Width; }
            fieldGRANTTOTALPARTICULARS.Width = rowWidth;
            fieldGRANTTOTALPROJECTID.Width = fieldAMOUNTCASH.Width;// fieldPROJECTID.Width;
            fieldGRANTTOTALAMOUNT.Width = fieldAMOUNTCASH.Width;

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
            xrSubOpeningBalanceMulti.WidthF = xrPGMultiAbstractReceipt.WidthF;
            
            //Set Subreport Properties for closing 
            xrSubClosingBalanceMulti.LeftF = xrPGMultiAbstractReceipt.LeftF;
            accountBalanceMultiYear.LeftPosition = (xrPGMultiAbstractReceipt.LeftF - 5);
            accountBalanceMultiYear.GroupCodeColumnWidth = fieldGROUPCODE.Width;
            accountBalanceMultiYear.GroupNameColumnWidth = fieldLEDGERGROUP.Width;
            accountBalanceMultiYear.LedgerCodeColumnWidth = fieldLEDGERCODE.Width;
            accountBalanceMultiYear.LedgerNameColumnWidth = fieldLEDGERNAME.Width;
            accountBalanceMultiYear.AmountColumnWidth = fieldAMOUNTCASH.Width;
            accountBalanceMultiYear.ApplyParentReportStyle = xrPGMultiAbstractReceipt.Styles;
            //accountBalanceMultiYear.ShowColumnHeader = false;
            
            xrTblTitleProjectSummary = AlignGrandTotalTable(xrTblTitleProjectSummary);
            xrTblProjectSummary = AlignGrandTotalTable(xrTblProjectSummary);
            
            //Assign Project Summary
            AttachProjectSummary();
         }

        private void xrPGMultiAbstractReceipt_CustomFieldSort_1(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs e)
        {
            if (e.Field.Name == fieldPROJECTID.Name)
            {
                if (e.Value1 != null && e.Value2 != null)
                {
                    if (this.ReportProperties.ReportId == "RPT-228")
                    {
                        Int32 projectid1 = UtilityMember.NumberSet.ToInteger(e.GetListSourceColumnValue(e.ListSourceRowIndex1, "PROJECT_CATOGORY_ITRGROUP_ID").ToString());
                        Int32 projectid2 = UtilityMember.NumberSet.ToInteger(e.GetListSourceColumnValue(e.ListSourceRowIndex2, "PROJECT_CATOGORY_ITRGROUP_ID").ToString());
                        e.Result = Comparer.Default.Compare(projectid1, projectid2);
                    }
                    else
                    {
                        Int32 projectid1 = UtilityMember.NumberSet.ToInteger(e.GetListSourceColumnValue(e.ListSourceRowIndex1, reportSetting1.AccountBalance.PROJECT_IDColumn.ColumnName).ToString());
                        Int32 projectid2 = UtilityMember.NumberSet.ToInteger(e.GetListSourceColumnValue(e.ListSourceRowIndex2, reportSetting1.AccountBalance.PROJECT_IDColumn.ColumnName).ToString());
                        e.Result = Comparer.Default.Compare(projectid1, projectid2);
                    }

                    /*string projectid1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, reportSetting1.MultiAbstract.PROJECTColumn.ColumnName).ToString().ToString();
                    string projectid2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, reportSetting1.MultiAbstract.PROJECTColumn.ColumnName).ToString().ToString();
                    e.Result = Comparer.Default.Compare(projectid1, projectid2);*/
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

            if (this.ReportProperties.Consolidated == 1)
            {
                for (int i = e.GetCellCount(true) - 1; i >= 0; i--)
                {
                    DevExpress.XtraReports.UI.PivotGrid.FieldValueCell cell = e.GetCell(true, i);
                    if ((cell.Field == fieldAMOUNTCASH || cell.Field == fieldAMOUNTBANK) && cell.ValueType == PivotGridValueType.Value)
                    {
                        if (cell == null) continue;
                        if (cell.EndLevel == e.GetLevelCount(true) - 1)
                        {
                            e.Remove(cell);
                        }
                    }
                }
            }
        }

        private bool IsValueEmpty(bool isColumn, int valueIndex, PivotCustomFieldValueCellsEventArgs e)
        {
            if (isColumn)
                return IsCollumnEmpty(valueIndex, e);
            else
                return IsRowEmpty(valueIndex, e);
        }

        private bool IsCollumnEmpty(int columnIndex, PivotCustomFieldValueCellsEventArgs e)
        {

            for (int j = 0; j < e.RowCount; j++)
            {
                decimal value = Convert.ToDecimal(e.GetCellValue(columnIndex, j));
                if (value != 0)
                    return false;
            }
            return true;
        }

        private bool IsRowEmpty(int rowIndex, PivotCustomFieldValueCellsEventArgs e)
        {
            for (int j = 0; j < e.ColumnCount; j++)
            {
                decimal value = Convert.ToDecimal(e.GetCellValue(j, rowIndex));
                if (value != 0)
                    return false;
            }
            return true;
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

        private void xrcellProjectCaption_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //XRTableCell cell = sender as XRTableCell;
            //cell.WidthF = fieldLEDGERCODE.Width + fieldLEDGERNAME.Width + fieldLEDGERGROUP.Width;
        }

        private void xrPGMultiAbstractReceipt_PrintHeader(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportHeaderEventArgs e)
        {
            if (e.Field != null)
            {
                DevExpress.XtraPrinting.TextBrick textBrick = e.Brick as DevExpress.XtraPrinting.TextBrick;
                textBrick.Location = new PointF(textBrick.Location.X, 0);
                textBrick.Size = new SizeF(textBrick.Size.Width, 135);
                //textBrick.Size = new SizeF(textBrick.Size.Width, 225);
            }
        }
    }
}
