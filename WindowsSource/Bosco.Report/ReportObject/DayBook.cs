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
    public partial class DayBook : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public DayBook()
        {
            InitializeComponent();
            this.AttachDrillDownToRecord(xrTableSource, xrtblLedger,
               new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName }, DrillDownType.LEDGER_VOUCHER, false, "VOUCHER_SUB_TYPE");
        }
        #endregion

        #region Variables
        ResultArgs resultArgs = null;

        string BaseReportId = string.Empty;
        string RecentDrillingReportId = string.Empty;

        Int32 DrillVoucherDefinationId = 0;
        string DrillVoucherDefinationName = string.Empty;
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            if (IsDrillDownMode)
            {
                Dictionary<string, object> dicDDProperties = this.ReportProperties.DrillDownProperties;
                DrillDownType ddtypeLinkType = DrillDownType.BASE_REPORT;
                ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), dicDDProperties["DrillDownLink"].ToString());

                BaseReportId = GetBaseReportId();

                if (dicDDProperties.ContainsKey("REPORT_ID"))
                    RecentDrillingReportId = dicDDProperties["REPORT_ID"].ToString();

                if (dicDDProperties.ContainsKey("VOUCHER_ID"))
                    DrillVoucherDefinationId = UtilityMember.NumberSet.ToInteger(dicDDProperties["VOUCHER_ID"].ToString());

                if (dicDDProperties.ContainsKey("VOUCHER_NAME"))
                    DrillVoucherDefinationName= dicDDProperties["VOUCHER_NAME"].ToString().Trim();

                if (dicDDProperties.ContainsKey(this.reportSetting1.CashBankFlow.DATEColumn.ColumnName))
                {
                    //LedgerDate = dicDDProperties[this.reportSetting1.CashBankFlow.DATEColumn.ColumnName].ToString();
                    //this.ReportProperties.BankAccount = "0";
                    //datefrom = dateto = LedgerDate;
                }
            }
            else
            {
                //datefrom = this.ReportProperties.DateFrom;
                //dateto = this.ReportProperties.DateTo;
            }
            BindDayBook();
        }
        #endregion

        #region Methods
        private void BindDayBook()
        {
            //Set Report title
            if (this.ReportProperties.DayBookVoucherType != 0 || DrillVoucherDefinationId!=0)
            {
                //Utility.DefaultVoucherTypes vouchertype = (Utility.DefaultVoucherTypes)UtilityMember.EnumSet.GetEnumItemType(typeof(Utility.DefaultVoucherTypes),
                //                                            this.ReportProperties.DayBookVoucherType.ToString());
                string selectedvtype = string.Empty;
                if (IsDrillDownMode)
                {
                    selectedvtype = DrillVoucherDefinationName;   
                }
                else
                {
                    selectedvtype = ReportProperty.Current.DayBookVoucherTypeName;
                }
                if (ReportProperty.Current.ReportTitle.IndexOf(" (" + selectedvtype + ")") <= 0)
                {
                    ReportProperty.Current.ReportTitle += " (" + selectedvtype + ")";
                }
            }

            if (!string.IsNullOrEmpty(this.ReportProperties.DateFrom) || !string.IsNullOrEmpty(this.ReportProperties.DateTo) || this.ReportProperties.Project != "0")
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        setHeaderTitleAlignment();
                        SetReportTitle();

                        this.SetLandscapeHeader = 1065.25f;
                        this.SetLandscapeFooter = 1065.25f;
                        this.SetLandscapeFooterDateWidth = 910.25f;

                        this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                        this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                        this.CosCenterName = null;
                                                

                        resultArgs = GetReportSource();
                        if (resultArgs.Success)
                        {
                            DataView dvDayBook = resultArgs.DataSource.TableView;
                            if (dvDayBook != null && dvDayBook.Count != 0)
                            {
                                dvDayBook.Table.TableName = "DAYBOOK";
                                //On 05/06/2017, To add Amount filter condition
                                AttachAmountFilter(dvDayBook);
                                this.DataSource = dvDayBook;
                                this.DataMember = dvDayBook.Table.TableName;
                            }
                            else
                            {
                                this.DataSource = null;
                            }
                        }
                        else
                        {
                            MessageRender.ShowMessage("Could not generate Report " + resultArgs.Message, true);
                        }
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
                    setHeaderTitleAlignment();
                    SetReportTitle();

                    this.SetLandscapeHeader = 1065.25f;
                    this.SetLandscapeFooter = 1065.25f;
                    this.SetLandscapeFooterDateWidth = 910.25f;

                    this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                    this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                    this.CosCenterName = null;

                    
                    resultArgs = GetReportSource();
                    if (resultArgs.Success)
                    {
                        DataView dvDayBook = resultArgs.DataSource.TableView;
                        if (dvDayBook != null && dvDayBook.Count != 0)
                        {
                            dvDayBook.Table.TableName = "DAYBOOK";
                            //On 05/06/2017, To add Amount filter condition
                            AttachAmountFilter(dvDayBook);
                            this.DataSource = dvDayBook;
                            this.DataMember = dvDayBook.Table.TableName;
                        }
                        else
                        {
                            this.DataSource = null;
                        }
                    }
                    else
                    {
                        MessageRender.ShowMessage("Could not generate Report " + resultArgs.Message, true);
                    }
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

        private void SetReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrTableSource = AlignContentTable(xrTableSource);
            xrtblGrandTotal = AlignContentTable(xrtblGrandTotal);
            this.SetCurrencyFormat(xrCapDebit.Text, xrCapDebit);
            this.SetCurrencyFormat(xrCapCredit.Text, xrCapCredit);
        }
        private ResultArgs GetReportSource()
        {
            try
            {
                string DayBook = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.DayBook);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);

                    //On 04/12/2024, To to currency based reports --------------------------------------------------------------------------------------
                    if (this.AppSetting.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                    {
                        dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                    }
                    else
                    {
                        dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, "0");
                    }
                    //----------------------------------------------------------------------------------------------------------------------------------

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, DayBook);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
            return resultArgs;
        }

        private string GetBaseReportId()
        {
            string Rtn = string.Empty;

            foreach (object item in this.ReportProperties.stackActiveDrillDownHistory)
            {
                EventDrillDownArgs eventdrilldownarg = item as EventDrillDownArgs;
                if (eventdrilldownarg.DrillDownType == DrillDownType.BASE_REPORT)
                {
                    Rtn = eventdrilldownarg.DrillDownRpt;
                    break;
                }
            }

            return Rtn;
        }
        #endregion

        #region Events

        #endregion


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
                dv.RowFilter = "(CREDIT > 0 AND CREDIT " + AmountFilter + ") OR (DEBIT > 0 AND DEBIT " + AmountFilter + ")";
                lblAmountFilter.Text = "Amount filtered by " + this.UtilityMember.NumberSet.ToNumber(this.ReportProperties.DonorFilterAmount);
                lblAmountFilter.Visible = true;
            }

            //On 30/08/2018, to filter based on Voucher Type
            string VoucherTypeFilter = string.Empty;
            if (IsDrillDownMode)
            {
                VoucherTypeFilter = "(VOUCHER_DEFINITION_ID =  " + DrillVoucherDefinationId + ")";
            }
            else
            {
                if (this.ReportProperties.DayBookVoucherType != 0)
                {
                    VoucherTypeFilter = "(VOUCHER_DEFINITION_ID =  " + this.ReportProperties.DayBookVoucherType.ToString() + ")";
                }
            }
            dv.RowFilter = VoucherTypeFilter;
            return dv;
        }
    }
}
