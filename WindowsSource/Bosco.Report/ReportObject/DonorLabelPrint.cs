using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.Utility;
using DevExpress.XtraSplashScreen;
using Bosco.Report.Base;
using System.Data;
using Bosco.Report.View;
using Bosco.DAO.Data;

namespace Bosco.Report.ReportObject
{
    public partial class DonorLabelPrint : Bosco.Report.Base.ReportHeaderBase
    {

        private int currentPage;

        ResultArgs resultArgs = new ResultArgs();
        public DonorLabelPrint()
        {
            InitializeComponent();
        }

        #region Methods
        public override void ShowReport()
        {
            LoadDonorDetails();
        }

        public override void ShowPrintDialogue()
        {
            this.HideReportHeader = this.HidePageFooter = false;
            //LoadDonorDetails();

            // this.Print();
            this.ShowPreviewDialog();
        }

        private void LoadDonorDetails()
        {
            SetReportTitle();
            this.HideReportHeader = false;
            this.HideDateRange = false;
            this.HidePageFooter = false;
            if (string.IsNullOrEmpty(this.ReportProperties.SelectedTaskName))
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
                        setHeaderTitleAlignment();
                        resultArgs = GetReportSource();
                        this.DataSource = null;
                        DataView dvDonor = resultArgs.DataSource.TableView;
                        if (dvDonor != null && dvDonor.Table.Rows.Count > 0)
                        {
                            dvDonor.Table.TableName = "ProspectIndividual";
                            this.DataSource = dvDonor;
                            this.DataMember = dvDonor.Table.TableName;
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
                    resultArgs = GetReportSource();
                    this.DataSource = null;
                    DataView dvDonor = resultArgs.DataSource.TableView;
                    if (dvDonor != null && dvDonor.Table.Rows.Count > 0)
                    {
                        dvDonor.Table.TableName = "ProspectIndividual";
                        this.DataSource = dvDonor;
                        this.DataMember = dvDonor.Table.TableName;
                    }
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }

            ////Commanded by Chinna Since salamon did Testing and checked in 
            //SetReportTitle();
            //this.HideReportHeader = false;
            //this.HideDateRange = false;
            //this.HidePageFooter = false;

            //setHeaderTitleAlignment();

            //DataView dvDonor = new DataView(this.ReportProperties.DonorLabelPrint);
            //if (dvDonor != null)
            //{
            //    this.DataSource = dvDonor;
            //    this.DataMember = "ProspectIndividual";
            //}
            //else //If gridview datasource is empy, prompt proper message
            //{
            //    //prompt some messsage
            //}

            base.ShowReport();
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                string Query = this.GetNetWorkingReports(SQL.ReportSQLCommand.NetWorking.LabelPrint);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.FD_ACCOUNT_IDColumn, !string.IsNullOrEmpty(this.ReportProperties.SelectedTaskName) ? this.ReportProperties.SelectedTaskName : "0");
                    dataManager.Parameters.Add(this.ReportParameters.STATE_IDColumn, string.IsNullOrEmpty(this.ReportProperties.StateDonaud) ? "0" : this.ReportProperties.StateDonaud);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, Query);
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
    }
}
