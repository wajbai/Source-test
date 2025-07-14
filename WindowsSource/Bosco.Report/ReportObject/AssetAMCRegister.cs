using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.Report.Base;
using Bosco.Utility;
using Bosco.DAO.Data;

namespace Bosco.Report.ReportObject
{
    public partial class AssetAMCRegister : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variable Declaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public AssetAMCRegister()
        {
            InitializeComponent();
        }
        #endregion

        #region Show Report
        public override void ShowReport()
        {
            BindAMCDetails();
        }
        #endregion

        #region Methods
        private void BindAMCDetails()
        {
            SetReportTitle();

            this.SetLandscapeHeader = 1065.25f;
            this.SetLandscapeFooter = 1065.25f;
            this.SetLandscapeFooterDateWidth = 900.00f;
            if (!string.IsNullOrEmpty(this.ReportProperties.DateFrom) && !string.IsNullOrEmpty(this.ReportProperties.DateTo))
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        //this.ReportTitle = ReportProperty.Current.ReportTitle;
                        //this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
                        setHeaderTitleAlignment();
                        //this.ReportPeriod = String.Format("For the Period: {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
                        SetReportTitle();
                        this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                        this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                        resultArgs = GetReportSource();
                        DataView dvAMCData = resultArgs.DataSource.TableView;
                        if (dvAMCData != null)
                        {
                            dvAMCData.Table.TableName = "AMCRegister";
                            this.DataSource = dvAMCData;
                            this.DataMember = dvAMCData.Table.TableName;
                        }
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
                    //this.ReportTitle = ReportProperty.Current.ReportTitle;
                    //this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
                    setHeaderTitleAlignment();
                    //this.ReportPeriod = String.Format("For the Period: {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
                    SetReportTitle();
                    this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                    this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                    resultArgs = GetReportSource();
                    DataView dvAMCRegister = resultArgs.DataSource.TableView;
                    if (dvAMCRegister != null)
                    {
                        dvAMCRegister.Table.TableName = "AMCRegister";
                        this.DataSource = dvAMCRegister;
                        this.DataMember = dvAMCRegister.Table.TableName;
                    }
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
        private ResultArgs GetReportSource()
        {
            try
            {
                string AMCRegister = this.GetAssetReports(SQL.ReportSQLCommand.Asset.AssetAMCRegister);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, AMCRegister);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
            return resultArgs;
        }

        private void SetReportBorder()
        {
            xrtAMCRegister = AlignHeaderTable(xrtAMCRegister);
            xtblAMCData = AlignContentTable(xtblAMCData);

            this.SetCurrencyFormat(xrcPremiumAmount.Text, xrcPremiumAmount);
        }
        #endregion
    }
}
