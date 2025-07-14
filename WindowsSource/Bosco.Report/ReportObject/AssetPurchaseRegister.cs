using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using Bosco.Report.Base;
using Bosco.Utility;
using System.Data;
using Bosco.DAO.Data;

namespace Bosco.Report.ReportObject
{
    public partial class AssetPurchaseRegister : Bosco.Report.Base.ReportHeaderBase
    {
        public AssetPurchaseRegister()
        {
            InitializeComponent();
        }

        #region Variables
        public int Flag = 0;
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                || this.ReportProperties.Project == "0")
            {
                SetReportTitle();
                ShowReportFilterDialog();
                SetReportBorder();
            }
            else
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        BindPurchaseRegisterSource();
                        SplashScreenManager.CloseForm();
                        base.ShowReport();
                    }
                    else
                    {
                        SetReportTitle();
                        ShowReportFilterDialog();
                        SetReportBorder();
                    }
                }
                else
                {
                    SplashScreenManager.ShowForm(typeof(frmReportWait));
                    BindPurchaseRegisterSource();
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
        }

        public void BindPurchaseRegisterSource()
        {
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            this.SetLandscapeFooterDateWidth = 250.00f;
            this.SetLandscapeHeader = 1125.10f;
            this.SetLandscapeFooter = 1125.10f;

            this.ReportTitle = ReportProperty.Current.ReportTitle;
            SetReportTitle();
            //  this.SetReportDate = this.ReportProperties.ReportDate;
            ResultArgs resultArgs = GetReportSource();
            DataView dvPurchase = resultArgs.DataSource.TableView;

            if (dvPurchase != null)
            {
                dvPurchase.Table.TableName = "PurchaseInKind";
                this.DataSource = dvPurchase;
                this.DataMember = dvPurchase.Table.TableName;
            }
            SetReportBorder();
            //SortByLedgerorGroup();
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlAssetPurcahse = this.GetAssetReports(SQL.ReportSQLCommand.Asset.PurchaseRegister);

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);

                //On 04/12/2024, To set Currnecy
                if (this.AppSetting.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, "0");
                }

                //if (!string.IsNullOrEmpty(this.ReportProperties.AssetItemID))
                //{
                //    dataManager.Parameters.Add(this.ReportParameters.ITEM_IDColumn, this.ReportProperties.AssetItemID);
                //}
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, sqlAssetPurcahse);
            }
            return resultArgs;
        }

        private void SetReportBorder()
        {
            xrTableHeader = AlignHeaderTable(xrTableHeader);
            xrTableDetail = AlignContentTable(xrTableDetail);

            this.SetCurrencyFormat(xrNetAmount.Text, xrNetAmount);
        }
        #endregion
    }
}
