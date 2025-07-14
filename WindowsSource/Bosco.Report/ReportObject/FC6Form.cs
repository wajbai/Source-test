using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using System.Linq;
using Bosco.Report.Base;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using Bosco.Utility.ConfigSetting;
using System.Text;

namespace Bosco.Report.ReportObject
{
    public partial class FC6Form : Bosco.Report.Base.ReportBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        SettingProperty settingProperty = new SettingProperty();
        string CurrencyFormat = string.Empty;

        string InsAddress = string.Empty;
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
        public FC6Form()
        {
            InitializeComponent();
        }
        #endregion

        #region Show Reports
        public override void ShowReport()
        {
            BindReceiptPaymentsSource();
        }
        #endregion


        #region Methods
        public void BindReceiptPaymentsSource()
        {

            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) && string.IsNullOrEmpty(this.ReportProperties.DateTo))
            {
                ShowReportFilterDialog();
            }
            else
            {
                LoadDefaults();
                base.ShowReport();
            }
        }

        private void LoadDefaults()
        {
            FC6Purpose FCP = xrSubDonorList.ReportSource as FC6Purpose;
            FCP.ShowReport();

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
                InsAddress = dtInstPreference.Rows[0]["NAMEADDRESS"].ToString();
                string temp = string.Empty;
                var elements = InsAddress.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
                foreach (string items in elements)
                {
                    temp += items.Trim() + ",\n";
                }
                InsAddress = !string.IsNullOrEmpty(temp) && temp.Length > 0 ? temp.Remove(temp.Length - 2) : string.Empty;

                NASS = dtInstPreference.Rows[0]["NASS"].ToString();
                DASS = dtInstPreference.Rows[0]["DRASS"].ToString();
                // REGNO = dtInstPreference.Rows[0]["REGNODATE"].ToString();
                REGNO = dtInstPreference.Rows[0]["FCRIREGDATE"].ToString();
                PERNO = dtInstPreference.Rows[0]["PERNODATE"].ToString();
                OTHERNASS = dtInstPreference.Rows[0]["OTHER_ASSOCIATION_NATURE"].ToString();
                OTHERDASS = dtInstPreference.Rows[0]["OTHER_DENOMINATION"].ToString();
                State = dtInstPreference.Rows[0]["STATE_NAME"].ToString();
                xrCultural.Checked = xrEconomic.Checked = xrEducational.Checked = xrReligious.Checked = xrSocial.Checked = false;
                xrHindu.Font = xrSikh.Font = xrMuslim.Font = xrChristian.Font = xrBuddhist.Font = xrOthers.Font = new System.Drawing.Font(xrHindu.Font, FontStyle.Regular);
                lblOthersNatureasso.Visible = lblDenominationOthers.Visible = false;
                int Denomination = this.UtilityMember.NumberSet.ToInteger(DASS);

                SetAssociationNature(NASS);
                xrHindu.Checked = xrSikh.Checked = xrMuslim.Checked = xrchkOthers.Checked = xrBuddhist.Checked = xrOthers.Checked = false;
                if (Denomination == 0)
                {
                  //  xrHindu.Font = new System.Drawing.Font(xrHindu.Font, FontStyle.Bold);
                    xrHindu.Checked = true;
                }
                else if (Denomination == 1)
                {
                    //xrSikh.Font = new System.Drawing.Font(xrSikh.Font, FontStyle.Bold);
                    xrSikh.Checked = true;
                }
                else if (Denomination == 2)
                {
                    //xrMuslim.Font = new System.Drawing.Font(xrMuslim.Font, FontStyle.Bold);
                    xrMuslim.Checked = true;
                }
                else if (Denomination == 3)
                {
                    //xrChristian.Font = new System.Drawing.Font(xrChristian.Font, FontStyle.Bold);
                    xrChristian.Checked = true;
                }
                else if (Denomination == 4)
                {
                    //xrBuddhist.Font = new System.Drawing.Font(xrBuddhist.Font, FontStyle.Bold);
                    xrBuddhist.Checked = true;
                }
                else if (Denomination == 5)
                {
                    //xrOthers.Font = new System.Drawing.Font(xrOthers.Font, FontStyle.Bold);
                    xrOthers.Checked = true;
                    lblDenominationOthers.Visible = true;
                    lblDenominationOthers.Text = OTHERDASS;
                }
                xrNameofAddress.Text = (InsAddress).ToUpper();
                xrRegNo.Text = !string.IsNullOrEmpty(REGNO) ? REGNO : string.Empty; // + " (Under Foreign Contribution Regulation Act 1976)" : string.Empty;
                xrPermissionNoDate.Text = string.IsNullOrEmpty(REGNO) ? PERNO : "         NOT APPLICABLE   ";
            }
            FC6Settings();
            FetchDesignatedBankAmount();
            FetchFixedDepositAmount();
            FetchBankAccount();
            FetchDonorAmount();
            GetReportSource();

            string consstmet = "  We have audited the account of   <b> " + xrNameofAddress.Text + " Society FCRA Reg No.  " + REGNO + "</b> State of Registration <b>" + State + " " +
            "</b> for the year ending  " + ReportProperties.DateSet.ToDate(ReportProperties.DateTo.ToString(), false).ToString("dd MMMM yyyy") + "" +
            " and examined all relevent books and vouchers and certify that according to the audited account:";
            System.IO.MemoryStream stream = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(consstmet));
            xrCCStatemem.LoadFile(stream, DevExpress.XtraReports.UI.XRRichTextStreamType.HtmlText);
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
                    xrCultural.Checked = true;
                }
                if (this.UtilityMember.NumberSet.ToInteger(nature[i].ToString()) == (int)Association.Economic)
                {
                    xrEconomic.Checked = true;
                }
                if (this.UtilityMember.NumberSet.ToInteger(nature[i].ToString()) == (int)Association.Educational)
                {
                    xrEducational.Checked = true;
                }
                if (this.UtilityMember.NumberSet.ToInteger(nature[i].ToString()) == (int)Association.Religious)
                {
                    xrReligious.Checked = true;
                }
                if (this.UtilityMember.NumberSet.ToInteger(nature[i].ToString()) == (int)Association.Social)
                {
                    xrSocial.Checked = true;
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
        /// Default Settings
        /// </summary>
        private void FC6Settings()
        {
            xrReportDateTo.Text = ReportProperties.DateSet.ToDate(ReportProperties.DateTo.ToString(), false).ToString("dd MMMM yyyy");
            //  xrFooterDate.Text = ReportProperties.DateSet.ToDate(ReportProperties.DateTo.ToString(), false).ToShortDateString();
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
                        xrBankInterest.Text = SetCurrencyFormat(this.UtilityMember.NumberSet.ToNumber(intamt));
                    }
                    else
                    {
                        xrBankInterest.Text = "";
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
                        xrFDInterest.Text = SetCurrencyFormat(this.UtilityMember.NumberSet.ToNumber(FDamt));
                    }
                    else
                    {
                        xrFDInterest.Text = "";
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
                        xrlblInstiDonors.Text = SetCurrencyFormat(this.UtilityMember.NumberSet.ToNumber(INSTITUTEAMOUNT));
                        IND_ABOVE_AMOUNT = this.UtilityMember.NumberSet.ToDouble(dvCashFlow.Rows[0]["INDIV_ABOVE"].ToString());
                        xrlblDonorAboveOneLakh.Text = SetCurrencyFormat(this.UtilityMember.NumberSet.ToNumber(IND_ABOVE_AMOUNT));
                        IND_BELOW_AMOUNT = this.UtilityMember.NumberSet.ToDouble(dvCashFlow.Rows[0]["INDIV_BELOW"].ToString());
                        xrlblDonorBelowOneLakh.Text = SetCurrencyFormat(this.UtilityMember.NumberSet.ToNumber(IND_BELOW_AMOUNT));

                        double totalDonor = INSTITUTEAMOUNT + IND_ABOVE_AMOUNT + IND_BELOW_AMOUNT;
                        xrTotalDonorAmount.Text = xrContributionAmt.Text = SetCurrencyFormat(this.UtilityMember.NumberSet.ToNumber(totalDonor));
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
                        xrBankAcNo.Text = dvCashFlow.Rows[0]["ACCOUNT_NUMBER"].ToString();
                        xrBankName.Text = dvCashFlow.Rows[0]["BANK"].ToString();
                        xrBankBranch.Text = dvCashFlow.Rows[0]["BRANCH"].ToString();
                        xrBankAddress.Text = dvCashFlow.Rows[0]["ADDRESS"].ToString();
                    }
                    else
                    {
                        xrBankAcNo.Text = xrBankName.Text = xrBankBranch.Text = xrBankAddress.Text = "";
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
                    }
                    DataTable dtForeign = dvCashFlow.ToTable();

                    xrCurrentBalance.Text = xrBeginnningBalance.Text = xrClosingBalance.Text = string.Empty;

                    // beginning Year Starts
                    double PrevCash = this.UtilityMember.NumberSet.ToDouble(dtForeign.Compute("Sum(PRE_CASH)", "").ToString());
                    double PreKind = this.UtilityMember.NumberSet.ToDouble(dtForeign.Compute("Sum(PRE_KIND)", "").ToString());
                    string clsbalprev = "The brought forward foreign contribution at the beginning of the year was " +
                        "<b>Rs. " + this.UtilityMember.NumberSet.ToNumber(PrevCash) + " in cash and Rs. " + PreKind + " in Kind</b>";
                    System.IO.MemoryStream streambeg = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(clsbalprev));
                    xrBeginnningBalance.LoadFile(streambeg, DevExpress.XtraReports.UI.XRRichTextStreamType.HtmlText);
                    // beginning Year ends

                    // Current Transaction starts
                    double CUR = this.UtilityMember.NumberSet.ToDouble(dtForeign.Compute("Sum(FIRST_CASH)", "").ToString()) +
                        this.UtilityMember.NumberSet.ToDouble(dtForeign.Compute("Sum(SECOND_CASH)", "").ToString());
                    string yearper = ReportProperties.DateSet.ToDate(ReportProperties.DateFrom.ToString(), false).Year.ToString() + " - " +
                        ReportProperties.DateSet.ToDate(ReportProperties.DateTo.ToString(), false).Year.ToString();


                    string clscurrent = "Foreign contribution of/worth  <b>Rs." + this.UtilityMember.NumberSet.ToNumber(CUR) +
                    "</b>  was received by the association during the year " + yearper + "";

                    System.IO.MemoryStream stream = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(clscurrent));
                    xrCurrentBalance.LoadFile(stream, DevExpress.XtraReports.UI.XRRichTextStreamType.HtmlText);
                    // Current Transaction ends

                    // Closing Balance Starts
                    double ClBalanceCash = this.UtilityMember.NumberSet.ToDouble(dtForeign.Compute("Sum(BALANCE_CASH)", "").ToString());
                    double ClBalanceKind = this.UtilityMember.NumberSet.ToDouble(dtForeign.Compute("Sum(BALANCE_KIND)", "").ToString());
                    string clsbal = "The balance of unutilized foreign contribution with the association at the end of the year March " +
                        ReportProperties.DateSet.ToDate(ReportProperties.DateTo.ToString(), false).Year.ToString() +
                        " was <b>Rs. " + this.UtilityMember.NumberSet.ToNumber(ClBalanceCash) + " in cash and Rs. " + ClBalanceKind + " in Kind </b>";
                    System.IO.MemoryStream streamcls = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(clsbal));
                    xrClosingBalance.LoadFile(streamcls, DevExpress.XtraReports.UI.XRRichTextStreamType.HtmlText);
                    // Closing Balance Starts
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
        }

        public string SetCurrencyFormat(string Caption)
        {
            if (!Caption.Contains(settingProperty.Currency))
                CurrencyFormat = String.Format("{0}. {1}", settingProperty.Currency, Caption);
            else { CurrencyFormat = Caption; }
            return CurrencyFormat;
        }

        #endregion

        private void xrContributionAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //double totalDonor = (this.UtilityMember.NumberSet.ToDouble(xrlblInstiDonors.Text) +
            //    this.UtilityMember.NumberSet.ToDouble(xrlblDonorAboveOneLakh.Text) +
            //    this.UtilityMember.NumberSet.ToDouble(xrlblDonorBelowOneLakh.Text));
            //e.Result = this.UtilityMember.NumberSet.ToNumber(totalDonor);
            //e.Handled = true;
        }

        private void xrTotalDonorAmount_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //double totalDonor = (this.UtilityMember.NumberSet.ToDouble(xrlblInstiDonors.Text) +
            //   this.UtilityMember.NumberSet.ToDouble(xrlblDonorAboveOneLakh.Text) +
            //   this.UtilityMember.NumberSet.ToDouble(xrlblDonorBelowOneLakh.Text));
            //e.Result = this.UtilityMember.NumberSet.ToNumber(totalDonor);
            //e.Handled = true;
        }

        private void FC6Form_PrintProgress(object sender, DevExpress.XtraPrinting.PrintProgressEventArgs e)
        {
            //if (e.PageIndex == 0 || e.PageIndex == this.Pages.Count - 2 || e.PageIndex == this.Pages.Count - 1)
            //{
            //    e.PageSettings.Landscape = false;
            //}
        }

        private void xrTableCell90_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string RecDate = (GetCurrentColumnValue(this.reportSetting1.FC6DONORLIST.SNOColumn.ColumnName) == null) ?
               string.Empty :
              GetCurrentColumnValue(this.reportSetting1.FC6DONORLIST.DOF_RECEIPTSColumn.ColumnName).ToString();
            if (!string.IsNullOrEmpty(RecDate))
            {
                xrTableCell81.Text = this.UtilityMember.DateSet.ToDate(RecDate, false).ToString("dd-MMM-yyyy");
            }

        }

        private string roman(int number)
        {
            StringBuilder result = new StringBuilder();
            int[] digitsValues = { 1, 4, 5, 9, 10, 40, 50, 90, 100, 400, 500, 900, 1000 };
            string[] romanDigits = { "I", "IV", "V", "IX", "X", "XL", "L", "XC", "C", "CD", "D", "CM", "M" };
            while (number > 0)
            {
                for (int i = digitsValues.Count() - 1; i >= 0; i--)
                    if (number / digitsValues[i] >= 1)
                    {
                        number -= digitsValues[i];
                        result.Append(romanDigits[i]);
                        break;
                    }
            }
            return result.ToString();
        }

        private void xrTableCell88_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int RecNo = this.UtilityMember.NumberSet.ToInteger(xrTableCell90.Text);
            xrTableCell88.Text = roman(RecNo);
        }
    }
}
