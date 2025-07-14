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

namespace Bosco.Report.ReportObject
{
    public partial class CostcentrewiseLedgerDistribution : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variables

        #endregion

        #region Constructor
        public CostcentrewiseLedgerDistribution()
        {
            InitializeComponent();
            this.SetTitleWidth(xrPGMultiAbstractReceipt.WidthF);
        }
        #endregion

        #region Show Report
        public override void ShowReport()
        {
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
                        BindDistributionList();
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
                    BindDistributionList();
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
        }
        #endregion

        #region Events

        #endregion

        #region Methods
        public void BindDistributionList()
        {
            setHeaderTitleAlignment();
            SetReportTitle();
            
            ResultArgs resultArgs = GetReportSource();
            DataTable  dtTable = resultArgs.DataSource.TableView.ToTable();

            //if (dtTable != null && dtTable.Rows.Count > 0)
            //{
            //    dtTable.TableName = "Ledger";
            //    this.DataSource = dtTable;
            //    this.DataMember = dtTable.TableName;
            //}
            //else
            //{
            //    this.DataSource = null;
            //}

            if (dtTable != null && dtTable.Rows.Count > 0)
            {
                dtTable.TableName = "Ledger";
                xrPGMultiAbstractReceipt.DataSource = dtTable;
                xrPGMultiAbstractReceipt.DataMember = dtTable.TableName;
            }
            else
            {
                this.DataSource = null;
            }
            fieldCOSTCENTRENAME.BestFit();
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlReceipts = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CostCentreDistributionList);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, sqlReceipts);
            }

            return resultArgs;
        }
        #endregion
    }
}
