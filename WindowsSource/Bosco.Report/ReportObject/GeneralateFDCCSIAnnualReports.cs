using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using System.Data;
using Bosco.DAO.Data;
using Bosco.Utility.ConfigSetting;
//using Bosco.Model.UIModel;

namespace Bosco.Report.ReportObject
{
    public partial class GeneralateFDCCSIAnnualReports : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variable
        int RecordNumber = 0;
        SettingProperty settingProperty = new SettingProperty();

        private double netTotalMgnActivity;
        public double NetTotalManagementActivity
        {
            get
            {
                return netTotalMgnActivity;
            }
            set
            {
                netTotalMgnActivity = value;
            }



        }

        /// <summary>
        /// 14/01/2020, for FDCCSI, to keep Movement FD: NET Total
        /// </summary>
        /// 
        private double nettotalmoveActivity;
        public double NetTotalMovementActivity
        {
            get
            {
                return nettotalmoveActivity;
            }
            set
            {
                nettotalmoveActivity = value;
            }
        }
        #endregion

        #region Constructor
        public GeneralateFDCCSIAnnualReports()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReports
        public override void ShowReport()
        {
            RecordNumber = 0;
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                || this.ReportProperties.Project == "0")
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                BindSource();
            }

            base.ShowReport();
        }

        #endregion

        #region Methods
        public void BindSource()
        {
            RecordNumber = 0;
            SetReportTitle();
            ResultArgs resultArgs = GetReportSource();
            this.SetLandscapeHeader = xrTblCommunityInfo.WidthF;
            this.SetLandscapeFooter = xrTblCommunityInfo.WidthF;
            this.SetLandscapeFooterDateWidth = xrTblCommunityInfo.WidthF;

            // commanded 09/08/2024
            //this.HideReportHeader = this.HidePageHeader = false;
            //this.HideHeaderFully= true;

            FillReportProperties();

            //Bind Management Activities
            GeneralateActivityIE managementActiviteis = new GeneralateActivityIE();
            GeneralateActivityIEFA movementFAActiviteis = new GeneralateActivityIEFA();
            if (grpManagementActivities.Visible)
            {
                managementActiviteis = xrSubManagementActivitites.ReportSource as GeneralateActivityIE;
                managementActiviteis.BindSource(true);

                //Bind Movement FA Activities
                if (grpMovementFA.Visible)
                {
                    movementFAActiviteis = xrSubMovementFA.ReportSource as GeneralateActivityIEFA;
                    movementFAActiviteis.BindSource(true, managementActiviteis.dtSource);
                }

            }

            //Bind Commercial Activities
            if (grpCommercialActivities.Visible)
            {
                GeneralateCommercial movementCommercial = xrSubCommercialActivities.ReportSource as GeneralateCommercial;
                movementCommercial.BindSource(true);
            }

            //Bind Patrimonial
            if (grpPatrimonial.Visible)
            {
                GeneralatePatrimonial movementPatrimonial = xrSubPatrimonial.ReportSource as GeneralatePatrimonial;
                movementPatrimonial.BindSource(true);
            }

            //Bind Reconciliazione
            if (grpReconciliazione.Visible)
            {
                GeneralateReconciliazione movementReconciliazione = xrSubReconciliazione.ReportSource as GeneralateReconciliazione;
                movementReconciliazione.activitySubReport = managementActiviteis;
                movementReconciliazione.activityFAReports = movementFAActiviteis;
                movementReconciliazione.BindSource(true);
            }
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlIncomeExpenditure = this.GetGeneralateReportSQL(SQL.ReportSQLCommand.GeneralateReports.GeneralatePatrimonial);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                resultArgs = dataManager.FetchData(dataManager, DAO.Data.DataSource.DataTable, sqlIncomeExpenditure);
            }
            return resultArgs;
        }

        /// <summary>
        /// commanded on 09/08/2024
        /// </summary>
        private void FillReportProperties()
        {
            xrCellDeadline.Text = xrcellProvince.Text = string.Empty;
            xrCellCommunity.Text = this.settingProperty.InstituteName;
            xrCellAddress.Text = xrCellTel.Text = xrCellEmailAddress.Text = string.Empty;
            xrCellAddress.Text = this.settingProperty.Address;
            xrCellTel.Text = this.settingProperty.Phone;
            xrCellEmailAddress.Text = this.settingProperty.Email;

            //Int32 branchid = UtilityMember.NumberSet.ToInteger(ReportProperties.BranchOffice);

            //if (branchid > 0)
            //{
            //    using (BranchOfficeSystem branchsystem = new BranchOfficeSystem())
            //    {
            //        ResultArgs resultArgs = new ResultArgs();
            //        resultArgs = branchsystem.FillBranchOfficeDetails(branchid, DataBaseType.HeadOffice);

            //        if (resultArgs.Success)
            //        {
            //            xrCellAddress.Text = branchsystem.Address;
            //            xrCellTel.Text = branchsystem.PhoneNo;
            //            //xrCellEmailAddress.Text = branchsystem.BranchEmail;
            //        }
            //    }
            //}

            //Hide Sub reports based on Reports ----------------------------------------------------------------
            //1. RPT-170 - RPT-222 General Annual Report (Show all Sub Reports except Commericial Activitis)
            //2. RPT-175 - RPT-224 Show Only Management Activitis Reports (Community Budget)
            //3. RPT-176 - RPT-225 Show Only Commericial Activitis Reports (Apostolic Activities Budget)
            //4. RPT-179 - RPT-223 Show Only Commericial Activitis Reports (Annual Apostolic Activities)

            grpHeader.Visible = (this.ReportProperties.ReportId == "RPT-222");
            grpManagementActivities.Visible = (this.ReportProperties.ReportId == "RPT-222" || this.ReportProperties.ReportId == "RPT-224");
            grpMovementFA.Visible = (this.ReportProperties.ReportId == "RPT-222");
            grpCommercialActivities.Visible = (this.ReportProperties.ReportId == "RPT-225" || this.ReportProperties.ReportId == "RPT-223");
            grpPatrimonial.Visible = (this.ReportProperties.ReportId == "RPT-222");
            grpReconciliazione.Visible = (this.ReportProperties.ReportId == "RPT-222");
            ReportFooter.Visible = (this.ReportProperties.ReportId == "RPT-224" || this.ReportProperties.ReportId == "RPT-225");

            //grpHeader.Visible = (this.ReportProperties.Current.ReportId == "RPT-170");
            //grpManagementActivities.Visible = (this.ReportProperties.Current.ReportId == "RPT-170" || this.ReportProperties.Current.ReportId == "RPT-175");
            //grpMovementFA.Visible = (this.ReportProperties.Current.ReportId == "RPT-170");
            //grpCommercialActivities.Visible = (this.ReportProperties.Current.ReportId == "RPT-176" || this.ReportProperties.Current.ReportId == "RPT-179");
            //grpPatrimonial.Visible = (this.ReportProperties.Current.ReportId == "RPT-170");
            //grpReconciliazione.Visible = (this.ReportProperties.Current.ReportId == "RPT-170");
            //ReportFooter.Visible = (this.ReportProperties.Current.ReportId == "RPT-175" || this.ReportProperties.Current.ReportId == "RPT-176");
            if (this.ReportProperties.ReportId != "RPT-222")
            {
                xrPageBreak1.Visible = xrPageBreak2.Visible = xrPageBreak3.Visible = xrPageBreak4.Visible = xrPageBreak5.Visible = false;
            }

            //---------------------------------------------------------------------------------------------------
        }

        #endregion
        #region Events

        #endregion
    }
}
