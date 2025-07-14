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
    public partial class AssetTransferRegister : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        public int Flag = 0;
        #endregion

        #region Constructors
        public AssetTransferRegister()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReport

        public override void ShowReport()
        {
            BindTransferRegisterSource();
        }

        public void BindTransferRegisterSource()
        {
            this.ReportTitle = ReportProperty.Current.ReportTitle;
            SetReportTitle();
            this.SetReportDate = this.ReportProperties.ReportDate;
            xrAmount.Text = this.SetCurrencyFormat(xrAmount.Text);
            xrCashAmount.Text = this.SetCurrencyFormat(xrCashAmount.Text);
            if (this.ReportProperties.DateFrom == "" || this.ReportProperties.DateTo == "")
            {
                ShowReportFilterDialog();
            }
            else
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));

                        DataTable dtFARegister = GetReportSource();
                        if (dtFARegister != null)
                        {
                            this.DataSource = dtFARegister;
                            this.DataMember = dtFARegister.TableName;
                        }

                        SplashScreenManager.CloseForm();
                        base.ShowReport();
                    }
                    else
                    {
                        ShowReportFilterDialog();
                    }
                }
                else
                {
                    SplashScreenManager.ShowForm(typeof(frmReportWait));

                    DataTable dtTransferRegister = GetReportSource();
                    if (dtTransferRegister != null)
                    {
                        this.DataSource = dtTransferRegister;
                        this.DataMember = dtTransferRegister.TableName;
                    }

                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
            xrtTransferHead = AlignHeaderTable(xrtTransferHead);
            xrtTransferDetails = AlignContentTable(xrtTransferDetails);
        }
        private DataTable GetReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlAssetTransferRegister = this.GetAssetReports(SQL.ReportSQLCommand.Asset.AssetTransferRegister);

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlAssetTransferRegister);
            }
            return resultArgs.DataSource.Table;
        }

        private void SetReportBorder()
        {
            xrtTransferHead = AlignHeaderTable(xrtTransferHead);
            xrtTransferDetails = AlignContentTable(xrtTransferDetails);
        }

        #endregion

    }
}
