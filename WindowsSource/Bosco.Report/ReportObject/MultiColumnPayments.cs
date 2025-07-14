using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.Utility;
using Bosco.DAO.Data;
using DevExpress.XtraSplashScreen;
using System.Data;
using Bosco.Report.Base;

namespace Bosco.Report.ReportObject
{
    public partial class MultiColumnPayments : Bosco.Report.Base.ReportHeaderBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        #endregion

        public MultiColumnPayments()
        {
            InitializeComponent();
        }

        #region Methods
        public void CashAndBankBook()
        {
            this.HideReportHeader = false;
            this.HidePageFooter = false;

            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo)
                || this.ReportProperties.Project == "0")
            {

                ShowReportFilterDialog();
            }
            else
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        setHeaderTitleAlignment();
                        this.SetLandscapeHeader = 1040.25f;
                        this.SetLandscapeFooter = 1030.25f;
                        this.SetLandscapeFooterDateWidth = 890.00f;

                        this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                        this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                        resultArgs = GetReportSource();
                        this.DataSource = null;
                        DataView dvCashBankBook = resultArgs.DataSource.TableView;
                        dvCashBankBook.RowFilter = "VOUCHER_TYPE='Pymt'";
                        if (dvCashBankBook != null && dvCashBankBook.Table.Rows.Count > 0)
                        {
                            dvCashBankBook.Table.TableName = "CashBankBook";
                            this.DataSource = dvCashBankBook;
                            this.DataMember = dvCashBankBook.Table.TableName;
                        }
                        else
                        {
                            this.DataSource = null;
                        }
                    }
                    else
                    {
                        SetReportTitle();
                        ShowReportFilterDialog();
                    }
                }
                else
                {
                    setHeaderTitleAlignment();
                    this.SetLandscapeHeader = 1040.25f;
                    this.SetLandscapeFooter = 1030.25f;
                    this.SetLandscapeFooterDateWidth = 890.00f;

                    this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                    this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                    resultArgs = GetReportSource();
                    this.DataSource = null;
                    DataView dvCashBankBook = resultArgs.DataSource.TableView;
                    dvCashBankBook.RowFilter = "VOUCHER_TYPE='Pymt'";
                    if (dvCashBankBook != null && dvCashBankBook.Table.Rows.Count > 0)
                    {
                        dvCashBankBook.Table.TableName = "CashBankBook";
                        this.DataSource = dvCashBankBook;
                        this.DataMember = dvCashBankBook.Table.TableName;
                    }
                    else
                    {
                        this.DataSource = null;
                    }
                }
            }
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                string CashBankBookQueryPath = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.MultiColumnCashbank);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, CashBankBookQueryPath);
                }
            }
            catch (Exception ee)
            {
                MessageRender.ShowMessage(ee.Message, true);
            }
            finally { }
            return resultArgs;
        }
        #endregion

        private void xrPGDrMuticolumnCashBank_PrintCell(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportCellEventArgs e)
        {

        }
    }
}
