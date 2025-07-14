using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using System.IO;
using System.Data;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.Report.Base;
using Bosco.Utility.ConfigSetting;
using System.Linq;

namespace Bosco.Report.ReportObject
{
    public partial class FixedAssetsRegisterItems : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        string ItemName = string.Empty;
        string PrvItemName = string.Empty;
        double ItemQuantity = 0;
        double ItemBalanceAmount = 0;
        #endregion

        #region Constructor
        public FixedAssetsRegisterItems()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindFixedAssetRegisterItemSource();
            base.ShowReport();
            //if (String.IsNullOrEmpty(this.ReportProperties.DateAsOn)
            //   || this.ReportProperties.Project == "0")
            //{
            //    SetReportTitle();
            //    ShowReportFilterDialog();
            //    SetReportBorder();
            //}
            //else
            //{
            //    if (this.UIAppSetting.UICustomizationForm == "1")
            //    {
            //        if (ReportProperty.Current.ReportFlag == 0)
            //        {
            //            SplashScreenManager.ShowForm(typeof(frmReportWait));
            //            BindFixedAssetRegisterSource();
            //            SplashScreenManager.CloseForm();
            //            base.ShowReport();
            //        }
            //        else
            //        {
            //            SetReportTitle();
            //            ShowReportFilterDialog();
            //            SetReportBorder();
            //        }
            //    }
            //    else
            //    {
            //        SplashScreenManager.ShowForm(typeof(frmReportWait));
            //        BindFixedAssetRegisterSource();
            //        SplashScreenManager.CloseForm();
            //        base.ShowReport();
            //    }
            //}
        }
        #endregion

        #region Methods
        public void BindFixedAssetRegisterItemSource()
        {
            this.HideCostCenter = true;
            this.SetLandscapeFooterDateWidth = 250.00f;
            this.SetLandscapeHeader = 1125.00f;
            this.SetLandscapeFooter = 1125.00f;
            this.ReportTitle = ReportProperty.Current.ReportTitle;
            //this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
            //this.CosCenterName = ReportProperty.Current.CostCentreName;

            this.SetLandscapeCostCentreWidth = 1125.0f;

            if (this.ReportProperties.Project == "0")
            {
                ShowReportFilterDialog();
                SetReportTitle();
            }
            if (this.UIAppSetting.UICustomizationForm == "1")
            {
                if (ReportProperty.Current.ReportFlag == 0)
                {
                    string Date = !string.IsNullOrEmpty(this.ReportProperties.DateAsOn) ? this.ReportProperties.DateAsOn :
                  UtilityMember.DateSet.ToDate(SettingProperty.Current.YearTo, false).ToShortDateString();

                    this.ReportPeriod = MessageCatalog.ReportCommonTitle.ASON + " " + Date;
                    ResultArgs resultArgs = GetReportSource();
                    DataView dvFixedAssetRegister = resultArgs.DataSource.TableView;

                    ItemQuantity = ItemBalanceAmount = 0;
                    if (dvFixedAssetRegister != null)
                    {
                        dvFixedAssetRegister.Table.TableName = "FixedAssetRegister";
                        this.DataSource = dvFixedAssetRegister;
                        this.DataMember = dvFixedAssetRegister.Table.TableName;

                        DataTable dtSum = dvFixedAssetRegister.ToTable();
                        if (dtSum != null && dtSum.Rows.Count > 0)
                        {
                            // chinna modified the source as double? on 19.01.2021
                            // 12/12/2024
                            //dtSum = dtSum.AsEnumerable().GroupBy(r => r.Field<UInt32>("ITEM_ID")).Select(g => g.Last()).CopyToDataTable();

                            // decimal item = dtSum.AsEnumerable().Sum(r => r.Field<decimal?>("BALANCE_NO"));

                            //12/12/2024
                            //ItemQuantity = UtilityMember.NumberSet.ToInteger(dtSum.AsEnumerable().Sum(r => r.Field<double?>("BALANCE_NO")).ToString());

                            //ItemQuantity = this.UtilityMember.NumberSet.ToInteger(dtSum.Compute("SUM(BALANCE_NO)", "").ToString());

                            //12/12/2024
                            //ItemBalanceAmount = UtilityMember.NumberSet.ToDouble(dtSum.AsEnumerable().Sum(r => r.Field<double?>("BALANCE_AMOUNT")).ToString());

                            // ItemBalanceAmount = this.UtilityMember.NumberSet.ToDouble(dtSum.Compute("SUM(BALANCE_AMOUNT)", "").ToString());
                        }
                    }
                    SetReportBorder();

                    //SplashScreenManager.ShowForm(typeof(frmReportWait));
                    //DataTable dtFARegister = GetReportSource();
                    //if (dtFARegister != null)
                    //{
                    //    this.DataSource = dtFARegister;
                    //    this.DataMember = dtFARegister.TableName;
                    //}

                    //SplashScreenManager.CloseForm();
                    //base.ShowReport();
                }
                else
                {
                    ShowReportFilterDialog();
                }
            }
            else
            {
                string Date = !string.IsNullOrEmpty(this.ReportProperties.DateAsOn) ? this.ReportProperties.DateAsOn :
                 UtilityMember.DateSet.ToDate(SettingProperty.Current.YearTo, false).ToShortDateString();

                this.ReportPeriod = MessageCatalog.ReportCommonTitle.ASON + " " + Date;
                ResultArgs resultArgs = GetReportSource();
                DataView dvFixedAssetRegister = resultArgs.DataSource.TableView;

                ItemQuantity = ItemBalanceAmount = 0;
                if (dvFixedAssetRegister != null)
                {
                    dvFixedAssetRegister.Table.TableName = "FixedAssetRegister";
                    this.DataSource = dvFixedAssetRegister;
                    this.DataMember = dvFixedAssetRegister.Table.TableName;

                    DataTable dtSum = dvFixedAssetRegister.ToTable();
                    if (dtSum != null && dtSum.Rows.Count > 0)
                    {
                        // chinna modified the source as double? on 19.01.2021
                        // 12/12/2024
                        //dtSum = dtSum.AsEnumerable().GroupBy(r => r.Field<UInt32>("ITEM_ID")).Select(g => g.Last()).CopyToDataTable();

                        // decimal item = dtSum.AsEnumerable().Sum(r => r.Field<decimal?>("BALANCE_NO"));

                        //12/12/2024
                        //ItemQuantity = UtilityMember.NumberSet.ToInteger(dtSum.AsEnumerable().Sum(r => r.Field<double?>("BALANCE_NO")).ToString());

                        //ItemQuantity = this.UtilityMember.NumberSet.ToInteger(dtSum.Compute("SUM(BALANCE_NO)", "").ToString());

                        //12/12/2024
                        //ItemBalanceAmount = UtilityMember.NumberSet.ToDouble(dtSum.AsEnumerable().Sum(r => r.Field<double?>("BALANCE_AMOUNT")).ToString());

                        // ItemBalanceAmount = this.UtilityMember.NumberSet.ToDouble(dtSum.Compute("SUM(BALANCE_AMOUNT)", "").ToString());
                    }
                }
                SetReportBorder();

                //SplashScreenManager.ShowForm(typeof(frmReportWait));

                //DataTable dtFARegister = GetReportSource();
                //if (dtFARegister != null)
                //{
                //    this.DataSource = dtFARegister;
                //    this.DataMember = dtFARegister.TableName;
                //}

                //SplashScreenManager.CloseForm();
                //base.ShowReport();
            }


        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string FixedAssetRegister = this.GetAssetReports(SQL.ReportSQLCommand.Asset.FixedAssetItemRegister);

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, !string.IsNullOrEmpty(this.ReportProperties.DateAsOn) ? this.ReportProperties.DateAsOn :
                    SettingProperty.Current.YearTo.ToString());
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project != "0" ? this.ReportProperties.Project :
                    "0");

                //dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project != "0" ? this.ReportProperties.Project :
                //   SettingProperty.Current.UserProjectId.ToString());

                string assetClassID = string.IsNullOrEmpty(this.ReportProperties.Assetclass) ? "0" : this.ReportProperties.Assetclass;
                if (assetClassID != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.ASSET_CLASS_IDColumn, assetClassID);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, FixedAssetRegister);
            }
            return resultArgs;
        }

        private void SetReportBorder()
        {
            xrtblFixedAssetRegisterHeader = AlignHeaderTable(xrtblFixedAssetRegisterHeader);
            //     xrtFixedAssetRegister = AlignContentTable(xrtFixedAssetRegister);

            // this.SetCurrencyFormat(xrlCostAmount.Text, xrlCostAmount);
            //   this.SetCurrencyFormat(xrlSoldAmount.Text, xrlSoldAmount);
            //  this.SetCurrencyFormat(xrlBalanceAmount.Text, xrlBalanceAmount);
        }
        #endregion

        private void FixedAssetsRegister_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrTableCell22_SummaryRowChanged(object sender, EventArgs e)
        {


        }

        private void xrTableCell22_SummaryReset(object sender, EventArgs e)
        {

        }

        private void xrTableCell22_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //e.Result = ItemQuantity.ToString();
            //e.Handled = true;
        }

        private void xrTableCell9_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //e.Result = this.UtilityMember.NumberSet.ToNumber(ItemBalanceAmount);
            //e.Handled = true;
        }
    }
}
