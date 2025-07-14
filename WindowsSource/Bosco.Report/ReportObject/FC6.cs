using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using Bosco.Report.Base;
using Bosco.Utility.ConfigSetting;

namespace Bosco.Report.ReportObject
{
    public partial class FC6 : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        SettingProperty settingProperty = new SettingProperty();
        string FCPurpose = string.Empty;
        //int FCID = 0;
        Bosco.Utility.ConfigSetting.SettingProperty settings = new Utility.ConfigSetting.SettingProperty();
        string InstitudeName = string.Empty;
        string SocityName = string.Empty;
        string ContactPerson = string.Empty;
        string Address = string.Empty;
        string Address1 = string.Empty;
        string State = string.Empty;
        string Place = string.Empty;
        string District = string.Empty;
        string PinCode = string.Empty;
        string NASS = string.Empty;
        string DASS = string.Empty;
        string REGNO = string.Empty;
        string PERNO = string.Empty;
        string OTHERDASS = string.Empty;
        string OTHERNASS = string.Empty;
        double INSTITUTEAMOUNT = 0;
        double IND_ABOVE_AMOUNT = 0;
        double IND_BELOW_AMOUNT = 0;
        #endregion

        #region Constructor
        public FC6()
        {
            InitializeComponent();
            //  this.AttachDrillDownToRecord(xrFC6Report, xrPaymentAmount,
            //new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName }, DrillDownType.LEDGER_SUMMARY_PAYMENTS, false);
            this.AttachDrillDownToRecord(xrFC6Report, xrPurpose,
               new ArrayList { this.ReportParameters.DONAUD_IDColumn.ColumnName, this.ReportParameters.CONTRIBUTION_IDColumn.ColumnName, this.ReportParameters.PURPOSEColumn.ColumnName }, DrillDownType.FC_REPORT, false);
        }
        #endregion

        #region Show Reports
        public override void ShowReport()
        {
            HideReportHeader = false;
            BindReceiptPaymentsSource();
        }
        #endregion

        #region Events
        private void xrTotalDonorAmount_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double totalDonor = (this.UtilityMember.NumberSet.ToDouble(xrInstitutionalName.Text) + this.UtilityMember.NumberSet.ToDouble(xrIndividualValues.Text) + this.UtilityMember.NumberSet.ToDouble(xrBelowOneLacValues.Text));
            e.Result = this.UtilityMember.NumberSet.ToNumber(totalDonor);
            e.Handled = true;
        }
        private void xrContributionAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double totalDonor = (this.UtilityMember.NumberSet.ToDouble(xrInstitutionalName.Text) + this.UtilityMember.NumberSet.ToDouble(xrIndividualValues.Text) + this.UtilityMember.NumberSet.ToDouble(xrBelowOneLacValues.Text));
            e.Result = this.UtilityMember.NumberSet.ToNumber(totalDonor);
            e.Handled = true;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Bind Values to the report
        /// </summary>
        public void BindReceiptPaymentsSource()
        {
            this.SetLandscapeHeader = 1110.25f;
            this.SetLandscapeFooter = 1105.25f;
            this.SetLandscapeFooterDateWidth = 950.5f;
            FC6Purpose FCP = xrSubDonorList.ReportSource as FC6Purpose;
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            SetReportTitle();
            FCP.ShowReport();
            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) && string.IsNullOrEmpty(this.ReportProperties.DateTo))
            {
                ShowReportFilterDialog();
            }
            else
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        //  this.ReportTitle = ReportProperty.Current.ReportTitle;
                        FetchInstPreferece();
                        base.ShowReport();
                    }
                    else
                    {
                        ShowReportFilterDialog();
                    }
                }
                else
                {
                    FetchInstPreferece();
                    base.ShowReport();
                }
            }
        }
        /// <summary>
        /// Assign All the properties to the concern fields
        /// </summary>
        private void FetchInstPreferece()
        {
            string query = this.GetReportForeginContribution(SQL.ReportSQLCommand.ForeginContribution.FCInstPreference);
            using (DataManager dataManager = new DataManager())
            {
                if (ReportProperty.Current.SocietyId != 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.CUSTOMERIDColumn, this.ReportProperties.SocietyId);
                }
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, query);
            }
            DataTable dtInstPreference = resultArgs.DataSource.Table;
            if (dtInstPreference != null && dtInstPreference.Rows.Count != 0)
            {
                Address = dtInstPreference.Rows[0]["NAMEADDRESS"].ToString();
                NASS = dtInstPreference.Rows[0]["NASS"].ToString();
                DASS = dtInstPreference.Rows[0]["DRASS"].ToString();
                REGNO = dtInstPreference.Rows[0]["REGNODATE"].ToString();
                PERNO = dtInstPreference.Rows[0]["PERNODATE"].ToString();
                OTHERNASS = dtInstPreference.Rows[0]["OTHER_ASSOCIATION_NATURE"].ToString();
                OTHERDASS = dtInstPreference.Rows[0]["OTHER_DENOMINATION"].ToString();
                xrchkCultural.Checked = xrchkEconomic.Checked = xrchkEducational.Checked = xrchkReligious.Checked = xrchkSocial.Checked = xrchkOthers.Checked = false;
                chkHindu.Checked = chkChristian.Checked = chkMuslim.Checked = chkSikh.Checked = chkBuddhist.Checked = chkOthers.Checked = false;
                lblOthersNatureasso.Visible = lblDenominationOthers.Visible = false;
                int Denomination = this.UtilityMember.NumberSet.ToInteger(DASS);

                SetAssociationNature(NASS);
                if (Denomination == 0)
                {
                    chkHindu.Checked = true;
                }
                else if (Denomination == 1)
                {
                    chkSikh.Checked = true;
                }
                else if (Denomination == 2)
                {
                    chkMuslim.Checked = true;
                }
                else if (Denomination == 3)
                {
                    chkChristian.Checked = true;
                }
                else if (Denomination == 4)
                {
                    chkBuddhist.Checked = true;
                }
                else if (Denomination == 5)
                {
                    chkOthers.Checked = true;
                    lblDenominationOthers.Visible = true;
                    lblDenominationOthers.Text = OTHERDASS;
                }
                xrNameofAddress.Text = (Address).ToUpper();
                xrInistutiudeAddress.Text = Address + " ," + REGNO;
                xrRegNo.Text = REGNO;
                xrPermissionNoDate.Text = (string.IsNullOrEmpty(REGNO)) ? PERNO : string.Empty;
            }
            FC6Settings();
            GetReportSource();
            FetchBankInterestAmount();
            FetchDesignatedBankAmount();
            FetchFixedDepositAmount();
            FetchDonorAmount();
            FetchBankAccount();


        }
        /// <summary>
        /// Set Association Nature Details 
        /// </summary>
        /// <param name="assNature"></param>
        private void SetAssociationNature(string assNature)
        {
            string[] nature = assNature.Split(',');
            for (int i = 0; i < nature.Length; i++)
            {
                if (this.UtilityMember.NumberSet.ToInteger(nature[i].ToString()) == (int)Association.Cultural)
                {
                    xrchkCultural.Checked = true;
                }
                if (this.UtilityMember.NumberSet.ToInteger(nature[i].ToString()) == (int)Association.Economic)
                {
                    xrchkEconomic.Checked = true;
                }
                if (this.UtilityMember.NumberSet.ToInteger(nature[i].ToString()) == (int)Association.Educational)
                {
                    xrchkEducational.Checked = true;
                }
                if (this.UtilityMember.NumberSet.ToInteger(nature[i].ToString()) == (int)Association.Religious)
                {
                    xrchkReligious.Checked = true;
                }
                if (this.UtilityMember.NumberSet.ToInteger(nature[i].ToString()) == (int)Association.Social)
                {
                    xrchkSocial.Checked = true;
                }
                if (this.UtilityMember.NumberSet.ToInteger(nature[i].ToString()) == (int)Association.Others)
                {
                    xrchkOthers.Checked = true;
                    lblOthersNatureasso.Text = OTHERNASS;
                    lblOthersNatureasso.Visible = true;
                }
            }
        }
        /// <summary>
        /// Fetch Report Source
        /// </summary>
        private void GetReportSource()
        {
            string dateason = GetProgressiveDate(this.ReportProperties.DateFrom);
            try
            {
                string Purpose = this.GetReportForeginContribution(SQL.ReportSQLCommand.ForeginContribution.FC6Purpose);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.UtilityMember.DateSet.ToDate(this.UtilityMember.DateSet.ToDate(dateason), false));
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, Purpose);

                    DataView dvCashFlow = resultArgs.DataSource.TableView;
                    if (dvCashFlow != null && dvCashFlow.Count != 0)
                    {
                        dvCashFlow.Table.TableName = "FC6PURPOSELIST";
                        this.DataSource = dvCashFlow;
                        this.DataMember = dvCashFlow.Table.TableName;
                    }
                    if (this.ReportProperties.IncludeAllPurposes == 0)
                    {
                        dvCashFlow.RowFilter = "";
                        dvCashFlow.RowFilter = reportSetting1.FC6PURPOSELIST.HAS_TRANSColumn.ColumnName + " = 1";
                        //this.DataSource = dvCashFlow;
                        //this.DataMember = dvCashFlow.Table.TableName;
                    }
                    DataTable dtForeign = dvCashFlow.ToTable();
                    double PRE = this.UtilityMember.NumberSet.ToDouble(dtForeign.Compute("Sum(PRE_CASH)", "").ToString());
                    xrPreviousYearBal.Text = this.UtilityMember.NumberSet.ToNumber(PRE);

                    double CUR = this.UtilityMember.NumberSet.ToDouble(dtForeign.Compute("Sum(UTILISED_TOTAL)", "").ToString());
                    xrThisPeriod.Text = this.UtilityMember.NumberSet.ToNumber(CUR);

                    double BAL = this.UtilityMember.NumberSet.ToDouble(dtForeign.Compute("Sum(BALANCE_CASH)", "").ToString());
                    xrBalance.Text = this.UtilityMember.NumberSet.ToNumber(BAL);


                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
        }
        /// <summary>
        /// Fetch Institute/Institute Donor Amount
        /// </summary>
        private void FetchDonorAmount()
        {
            try
            {
                string Purpose = this.GetReportForeginContribution(SQL.ReportSQLCommand.ForeginContribution.FC6DonorAmount);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, Purpose);

                    DataTable dvCashFlow = resultArgs.DataSource.Table;
                    if (dvCashFlow != null && dvCashFlow.Rows.Count != 0)
                    {
                        INSTITUTEAMOUNT = this.UtilityMember.NumberSet.ToDouble(dvCashFlow.Rows[0]["INSTITUTE"].ToString());
                        xrInstitutionalName.Text = this.UtilityMember.NumberSet.ToNumber(INSTITUTEAMOUNT);
                        IND_ABOVE_AMOUNT = this.UtilityMember.NumberSet.ToDouble(dvCashFlow.Rows[0]["INDIV_ABOVE"].ToString());
                        xrIndividualValues.Text = this.UtilityMember.NumberSet.ToNumber(IND_ABOVE_AMOUNT);
                        IND_BELOW_AMOUNT = this.UtilityMember.NumberSet.ToDouble(dvCashFlow.Rows[0]["INDIV_BELOW"].ToString());
                        xrBelowOneLacValues.Text = this.UtilityMember.NumberSet.ToNumber(IND_BELOW_AMOUNT);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
        }
        /// <summary>
        /// Fetch Bank Details
        /// </summary>
        private void FetchBankAccount()
        {
            try
            {
                string Purpose = this.GetReportForeginContribution(SQL.ReportSQLCommand.ForeginContribution.FC6BankAccount);
                using (DataManager dataManager = new DataManager())
                {
                    if (ReportProperty.Current.SocietyId != 0)
                    {
                        dataManager.Parameters.Add(this.ReportParameters.CUSTOMERIDColumn, ReportProperty.Current.SocietyId);
                    }
                    else
                    {
                        dataManager.Parameters.Add(this.ReportParameters.CUSTOMERIDColumn, "0");
                    }
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, Purpose);

                    DataTable dvCashFlow = resultArgs.DataSource.Table;
                    if (dvCashFlow != null && dvCashFlow.Rows.Count != 0)
                    {
                        xrAccountNumber.Text = dvCashFlow.Rows[0]["ACCOUNT_NUMBER"].ToString();
                        xrBankName.Text = dvCashFlow.Rows[0]["BANK"].ToString();
                        xrBranch.Text = dvCashFlow.Rows[0]["BRANCH"].ToString();
                        xrAddress.Text = dvCashFlow.Rows[0]["ADDRESS"].ToString();
                    }
                    else
                    {
                        xrAccountNumber.Text = xrBankName.Text = xrBranch.Text = xrAddress.Text = "";
                    }
                }

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
        }
        /// <summary>
        /// Default Settings
        /// </summary>
        private void FC6Settings()
        {
            xrReportDateTo.Text = ReportProperties.DateSet.ToDate(ReportProperties.DateTo.ToString(), false).Year.ToString();
            string yearper = ReportProperties.DateSet.ToDate(ReportProperties.DateFrom.ToString(), false).Year.ToString() + " - " + ReportProperties.DateSet.ToDate(ReportProperties.DateTo.ToString(), false).Year.ToString();
            xrthisYearTo.Text = yearper;
            xrYearTo.Text = ReportProperties.DateSet.ToDate(ReportProperties.DateTo.ToString(), false).Year.ToString();
            xrCertificateYearTo.Text = ReportProperties.DateSet.ToDate(ReportProperties.DateTo.ToString(), false).Year.ToString();
        }
        /// <summary>
        /// Fetch Bank Interest Amount
        /// </summary>
        private void FetchBankInterestAmount()
        {
            try
            {
                string BankInterest = this.GetReportForeginContribution(SQL.ReportSQLCommand.ForeginContribution.FC6BankInterestAmount);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, ReportProperty.Current.Project);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, ReportProperty.Current.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, ReportProperty.Current.DateTo);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, BankInterest);

                    DataTable dvInterest = resultArgs.DataSource.Table;
                    if (dvInterest != null && dvInterest.Rows.Count != 0)
                    {
                        double intamt = this.UtilityMember.NumberSet.ToDouble(dvInterest.Rows[0]["AMOUNT"].ToString());
                        xrInterest.Text = this.UtilityMember.NumberSet.ToNumber(intamt);
                    }
                    else
                    {
                        xrInterest.Text = "";
                    }
                }

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
        }
        /// <summary>
        /// Fetch Designated Bank Amount
        /// </summary>
        private void FetchDesignatedBankAmount()
        {
            try
            {
                string BankInterest = this.GetReportForeginContribution(SQL.ReportSQLCommand.ForeginContribution.FC6DesignatedBankAmount);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, ReportProperty.Current.Project);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, ReportProperty.Current.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, ReportProperty.Current.DateTo);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, BankInterest);

                    DataTable dvDesignate = resultArgs.DataSource.Table;
                    if (dvDesignate != null && dvDesignate.Rows.Count != 0)
                    {
                        double intamt = this.UtilityMember.NumberSet.ToDouble(dvDesignate.Rows[0]["CURRENT_AMOUNT"].ToString());
                        xrDesignatedBankAccount.Text = this.UtilityMember.NumberSet.ToNumber(intamt);
                    }
                    else
                    {
                        xrDesignatedBankAccount.Text = "";
                    }
                }

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
        }
        /// <summary>
        /// Fetch Fixed Deposit Amount
        /// </summary>
        private void FetchFixedDepositAmount()
        {
            try
            {
                string FDAmount = this.GetReportForeginContribution(SQL.ReportSQLCommand.ForeginContribution.FC6FixedDeposit);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, ReportProperty.Current.Project);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, ReportProperty.Current.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, ReportProperty.Current.DateTo);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, FDAmount);

                    DataTable dvDesignate = resultArgs.DataSource.Table;
                    if (dvDesignate != null && dvDesignate.Rows.Count != 0)
                    {
                        double FDamt = this.UtilityMember.NumberSet.ToDouble(dvDesignate.Rows[0]["BALANCE_AMOUNT"].ToString());
                        xrFixedDeposit.Text = this.UtilityMember.NumberSet.ToNumber(FDamt);
                    }
                    else
                    {
                        xrFixedDeposit.Text = "";
                    }
                }

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
        }
        #endregion


        //private void ContributionAmount()
        //{
        //    string query = this.GetReportForeginContribution(SQL.ReportSQLCommand.ForeginContribution.FCContribution);
        //    using (DataManager dataManager = new DataManager())
        //    {
        //        dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
        //        dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
        //        dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
        //        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
        //        resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, query);
        //    }
        //    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
        //    {
        //        xrContributionAmt.Text = resultArgs.DataSource.Table.Rows[0]["AMOUNT"] != null ? this.ReportProperties.NumberSet.ToCurrency(this.ReportProperties.NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0]["AMOUNT"].ToString())) : "0";
        //    }
        //}


        //    private void xrFCPurpose_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        //    {
        //        //e.Result = FCID;
        //        //e.Handled = true;
        //    }

        //    private void xrSNo_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        //    {
        //        //e.Result = FCPurpose;
        //        //e.Handled = true;
        //    }

        //    private void xrSNo_SummaryReset(object sender, EventArgs e)
        //    {
        //        //FCID = 0;
        //    }

        //    private void xrFCPurpose_SummaryReset(object sender, EventArgs e)
        //    {
        //        //FCPurpose = string.Empty;
        //    }

        //    private void xrFCPurpose_SummaryRowChanged(object sender, EventArgs e)
        //    {
        //        //FCPurpose += GetCurrentColumnValue(this.ReportParameters.FC_PURPOSEColumn.ColumnName).ToString();
        //    }

        //    private void xrSNo_SummaryRowChanged(object sender, EventArgs e)
        //    {
        //        //FCID += this.UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(this.ReportParameters.CONTRIBUTION_IDColumn.ColumnName).ToString());
        //    }
    }
}
