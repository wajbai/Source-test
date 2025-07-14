using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using Bosco.Report.Base;
namespace Bosco.Report.ReportObject
{
    public partial class ExecutiveMembers : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        int SerialNumber = 0;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public ExecutiveMembers()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindExecutiveMembers();
        }
        #endregion

        #region Method
        private void BindExecutiveMembers()
        {
            this.SetLandscapeHeader = 1125.25f;
            this.SetLandscapeFooter = 1125.25f;
            this.SetLandscapeFooterDateWidth = 960.00f;

            if (this.ReportProperties.DateFrom != string.Empty || this.ReportProperties.DateTo != string.Empty)
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));

                        // this.ReportTitle = ReportProperty.Current.ReportTitle;
                        setHeaderTitleAlignment();
                        SetReportTitle();
                        //  this.ReportSubTitle = "Foreign Projects"; //ReportProperty.Current.ProjectTitle;
                        // this.ReportPeriod = String.Format("For the Period: {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
                        this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                        this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;


                        DataTable dtExecutiveMembers = GetReportSource();
                        if (dtExecutiveMembers != null)
                        {
                            if (ReportProperty.Current.FDRegisterStatus == 1)
                            {
                                resultArgs.DataSource.Table.DefaultView.RowFilter = "DATE_OF_EXIT IS NOT NULL";
                                dtExecutiveMembers = resultArgs.DataSource.Table.DefaultView.ToTable();
                            }
                            else if (ReportProperty.Current.FDRegisterStatus == 2)
                            {
                                resultArgs.DataSource.Table.DefaultView.RowFilter = "DATE_OF_EXIT IS NULL";
                                dtExecutiveMembers = resultArgs.DataSource.Table.DefaultView.ToTable();
                            }
                            else
                            {
                                dtExecutiveMembers = resultArgs.DataSource.Table;
                            }

                            this.DataSource = dtExecutiveMembers;
                            this.DataMember = dtExecutiveMembers.TableName;
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

                    // this.ReportTitle = ReportProperty.Current.ReportTitle;
                    setHeaderTitleAlignment();
                    SetReportTitle();
                    //  this.ReportSubTitle = "Foreign Projects"; //ReportProperty.Current.ProjectTitle;
                    // this.ReportPeriod = String.Format("For the Period: {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
                    this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                    this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                    DataTable dtExcutiveMembers = GetReportSource();
                    if (dtExcutiveMembers != null)
                    {
                        this.DataSource = dtExcutiveMembers;
                        this.DataMember = dtExcutiveMembers.TableName;
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

        private DataTable GetReportSource()
        {
            string ExecutiveMembers = this.GetReportForeginContribution(SQL.ReportSQLCommand.ForeginContribution.ExecutiveMembers);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.YEAR_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.YEAR_TOColumn, this.ReportProperties.DateTo);
                if (this.ReportProperties.SocietyId != 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.CUSTOMERIDColumn, this.ReportProperties.SocietyId);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, ExecutiveMembers);
            }
            return resultArgs.DataSource.Table;
        }
        private void SetReportBorder()
        {
            xrtblHeader = AlignHeaderTable(xrtblHeader);
            xrtblExecutiveMembers = AlignContentTable(xrtblExecutiveMembers);
        }

    }
        #endregion
}
