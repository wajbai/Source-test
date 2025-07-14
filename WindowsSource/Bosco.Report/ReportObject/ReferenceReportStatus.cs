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

namespace Bosco.Report.ReportObject
{
    public partial class ReferenceReportStatus : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor

        public ReferenceReportStatus()
        {
            InitializeComponent();
            this.AttachDrillDownToRecord(xrtblDataSourceBind, xrtblReferenceNumber, new ArrayList { reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName,
                     reportSetting1.Voucher_Reference.REC_PAY_VOUCHER_IDColumn.ColumnName},
                    DrillDownType.LEDGER_SUMMARY, false, "", true);
         }

        #endregion

        #region ShowReport

        public override void ShowReport()
        {
            if (IsDrillDownMode)
            {
                Dictionary<string, object> dicDDProperties = this.ReportProperties.DrillDownProperties;
                DrillDownType ddtypeLinkType = DrillDownType.BASE_REPORT;
                ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), dicDDProperties["DrillDownLink"].ToString());

                if (dicDDProperties.ContainsKey(this.reportSetting1.CashBankFlow.DATEColumn.ColumnName))
                {
                }
            }
            else
            {
            }
            BindReference();
        }

        #endregion

        #region Methods
        /// <summary>
        /// Bind the Data to Controls
        /// </summary>
        private void BindReference()
        {
            if (!string.IsNullOrEmpty(this.ReportProperties.DateFrom) || !string.IsNullOrEmpty(this.ReportProperties.DateTo) || this.ReportProperties.Project != "0")
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        BindProperty();
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
                    BindProperty();
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            SetReportBorder();
        }

        private void BindProperty()
        {
            setHeaderTitleAlignment();
            SetReportTitle();

            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

            this.CosCenterName = null;
            resultArgs = GetReportSource();
            DataView dvReference = resultArgs.DataSource.TableView;
            if (dvReference != null && dvReference.Count != 0)
            {
                dvReference.Table.TableName = "Voucher_Reference";
                // To add Amount filter condition
                AttachAmountFilter(dvReference);
                this.DataSource = dvReference;
                this.DataMember = dvReference.Table.TableName;
            }
            else
            {
                this.DataSource = null;
            }
        }

        private void SetReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrtblDataSourceBind = AlignContentTable(xrtblDataSourceBind);
            xrtblGrandTotal = AlignContentTable(xrtblGrandTotal);
            this.SetCurrencyFormat(xrCapActualAmount.Text, xrCapActualAmount);
            this.SetCurrencyFormat(xrPendingAmount.Text, xrPendingAmount);
            this.SetCurrencyFormat(xrcapBalanceAmount.Text, xrcapBalanceAmount);
        }
        /// <summary>
        /// Get Reference Source
        /// </summary>
        /// <returns></returns>
        private ResultArgs GetReportSource()
        {
            try
            {
                string ReferenceNo = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.ReferenceNo);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, ReferenceNo);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// This method is used to add filter condition
        /// </summary>
        private DataView AttachAmountFilter(DataView dv)
        {
            //On 05/06/2017, To add Amount filter condition
            string AmountFilter = this.GetAmountFilter();
            lblAmountFilter.Visible = false;
            if (AmountFilter != "")
            {
                dv.RowFilter = "(ACTUAL_AMOUNT > 0 AND ACTUAL_AMOUNT " + AmountFilter + ") OR (PAID_AMOUNT > 0 AND PAID_AMOUNT " + AmountFilter + ") " +
                    " OR (BALANCE_AMOUNT > 0 AND BALANCE_AMOUNT " + AmountFilter + ")";
                lblAmountFilter.Text = "Amount filtered by " + this.UtilityMember.NumberSet.ToNumber(this.ReportProperties.DonorFilterAmount);
                lblAmountFilter.Visible = true;
            }
            return dv;
        }

        #endregion

    }
}
