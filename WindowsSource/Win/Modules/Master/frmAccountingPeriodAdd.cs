using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Utility;
using Bosco.Model.UIModel.Master;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraEditors.Controls;
using Bosco.Model.UIModel;
using Bosco.DAO.Data;
using Bosco.Model.Setting;

namespace ACPP.Modules.Master
{
    public partial class frmAccountingPeriodAdd : frmFinanceBaseAdd
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        public event EventHandler UpdateHeld;
        private int AccYearId = 0;
        private int AccCount = 0;
        private int AccountYearId = 0;
        private string SelectedLang;
        #endregion

        #region Properties
        private string booksBeginDate = "";
        private string BooksBeginDate
        {
            get { return booksBeginDate; }
            set { booksBeginDate = value; }
        }

        private bool isFirstAccYear;
        private bool IsFirstAccYear
        {
            get { return isFirstAccYear; }
            set { isFirstAccYear = value; }
        }
        private int accid;
        public int AccID
        {
            get { return accid; }
            set { accid = value; }
        }
        #endregion


        public frmAccountingPeriodAdd()
        {
            InitializeComponent();
        }

        public frmAccountingPeriodAdd(int accYearId, int AccCount)
            : this()
        {
            AccYearId = accYearId;
            this.AccCount = AccCount;
            AssignLedgerDetails();
        }

        /// <summary>
        /// To Get the Confirmation while Creating the Accounting Year and validate the Year (chinna)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateAccountPeriodDetails())
            {
                this.ShowWaitDialog();
                try
                {
                    using (AccouingPeriodSystem AccPeriodSystem = new AccouingPeriodSystem())
                    {
                        AccPeriodSystem.AccPeriodId = AccYearId;
                        AccPeriodSystem.YearFrom = this.UtilityMember.DateSet.ToDate(detYearFrom.Text.ToString(), DateFormatInfo.DateFormatYMD);
                        AccPeriodSystem.YearTo = this.UtilityMember.DateSet.ToDate(detYearTo.Text.ToString(), DateFormatInfo.DateFormatYMD);
                        AccPeriodSystem.BooksBeginingDate = this.UtilityMember.DateSet.ToDate(detBookbeginingFrom.Text.ToString(), DateFormatInfo.DateFormatYMD);
                        AccPeriodSystem.IsFirstAccYear = IsFirstAccYear;
                        //if (this.AccCount == 0)
                        //{
                        //    AccPeriodSystem.Status = 1;
                        //}
                        if (isFirstAccYear)
                        {
                            AccPeriodSystem.Status = 1;
                        }
                        if (isFirstAccYear)
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Master.AccountingPeriod.ACCOUNTING_SET_YEAR_VALIDATION) + " " + detYearFrom.Text + " - " + detYearTo.Text + ". " + this.GetMessage(MessageCatalog.Master.Mapping.WANT_TO_PROCEED), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                resultArgs = AccPeriodSystem.SaveAccountingPeriodDetails();
                                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                                {
                                    if (AccYearId == 0) { detYearFrom.DateTime = detYearTo.DateTime.AddDays(1); }
                                    detYearFrom.Enabled = detBookbeginingFrom.Enabled = false;
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                                    //if (AccYearId == 0) { ClearControls(); }
                                    if (UpdateHeld != null)
                                    {
                                        UpdateHeld(this, e);
                                    }
                                    AccountYearId = AccPeriodSystem.AccPeriodId;
                                    //this.Close();

                                    if (isFirstAccYear)
                                    {
                                        accid = AccPeriodSystem.AccPeriodId;
                                        this.Close();
                                    }
                                }
                            }
                        }
                        else
                        {
                            resultArgs = AccPeriodSystem.SaveAccountingPeriodDetails();
                            if (resultArgs.Success && resultArgs.RowsAffected > 0)
                            {
                                if (AccYearId == 0) { detYearFrom.DateTime = detYearTo.DateTime.AddDays(1); }

                                detYearFrom.Enabled = detBookbeginingFrom.Enabled = false;
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                                //if (AccYearId == 0) { ClearControls(); }
                                if (UpdateHeld != null)
                                {
                                    UpdateHeld(this, e);
                                }
                                AccountYearId = AccPeriodSystem.AccPeriodId;
                                //this.Close();

                                if (isFirstAccYear)
                                {
                                    accid = AccPeriodSystem.AccPeriodId;
                                    this.Close();
                                }

                                //On 04/07/2019 When Books begin gets changed, refill global setting of accounting info
                                DateTime dtBooksDate =  UtilityMember.DateSet.ToDate(AppSetting.BookBeginFrom, false);
                                DateTime dtNewBooksDate = UtilityMember.DateSet.ToDate(detBookbeginingFrom.Text.ToString(),false);
                                if (dtBooksDate != dtNewBooksDate)
                                {
                                    UpdateTransacationPeriod(AccountYearId);
                                }
                                //------------------------------------------------------------------------------------------------------------------------
                            }
                        }
                    }
                }
                catch (Exception Ex)
                {
                    MessageRender.ShowMessage(Ex.Message);
                }
                finally
                {
                    this.CloseWaitDialog();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadDate()
        {
            // detYearFrom.DateTime= this.UtilityMember.DateSet.GetDateToday();
        }

        private DateTime CalculateYearTo()
        {
            DateTime accDateTo = new DateTime(detYearFrom.DateTime.Year, detYearFrom.DateTime.Month, 1);
            return accDateTo.Date.AddYears(1).AddDays(-1);
        }

        private void detYearFrom_EditValueChanged(object sender, EventArgs e)
        {
            detYearTo.DateTime = CalculateYearTo();
            if (string.IsNullOrEmpty(BooksBeginDate) || AccCount == 1)
            {
                detBookbeginingFrom.DateTime = detYearFrom.DateTime;
            }
        }

        private bool ValidateAccountPeriodDetails()
        {
            bool IsAccPereidValid = true;
            
            if (detYearFrom.EditValue == null)
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.AccountingPeriod.ACCOUNTING_PERIOD_YEAR_FROM_EMPTY));
                this.SetBorderColor(detYearFrom);
                IsAccPereidValid = false;
                detYearFrom.Focus();
            }
            else if (detYearTo.EditValue == null)
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.AccountingPeriod.ACCOUNTING_PERIOD_YEAR_TO_EMPTY));
                this.SetBorderColor(detYearTo);
                IsAccPereidValid = false;
                detYearTo.Focus();
            }
            else if (!this.UtilityMember.DateSet.ValidateDate(detYearFrom.DateTime, detYearTo.DateTime))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.AccountingPeriod.ACCOUNTING_PERIOD_YEAR_EQUAL_EMPTY));
                this.SetBorderColor(detYearTo);
                IsAccPereidValid = false;
                detYearTo.Focus();
            }
            else
            {
                //As on 28/07/2022, to check FY year date range must be 12 months and first day and last day ---------------
                DateEdit ctlDate = detYearFrom;
                string msg = string.Empty;
                Int32 nMonths = UtilityMember.DateSet.GetDateDifferentInMonths(detYearFrom.DateTime, detYearTo.DateTime);
                if (nMonths == 12)
                {
                    var firstDay = new DateTime(detYearFrom.DateTime.Year, detYearFrom.DateTime.Month, 1);
                    var lastDay = new DateTime(detYearTo.DateTime.Year, detYearTo.DateTime.Month, 1).AddMonths(1).AddDays(-1);

                    if (detYearFrom.DateTime.Day != firstDay.Day)
                    {
                        IsAccPereidValid = false;
                        ctlDate = detYearFrom;
                        msg = "Year From must be first day of the month, Check Year From value";
                    }
                    else if (detYearTo.DateTime.Day != lastDay.Day)
                    {
                        IsAccPereidValid = false;
                        ctlDate = detYearTo;
                        msg = "Year To must be last day of the month, Check Year To value";
                    }
                }
                else
                {
                    IsAccPereidValid = false;
                    ctlDate = detYearTo;
                    msg ="Transaction Period/Finance Year must be one year, Check Year From and Year To";
                }
                if (!IsAccPereidValid)
                {
                    this.ShowMessageBox(msg);
                    this.SetBorderColor(ctlDate);
                    ctlDate.Focus();
                }
                //----------------------------------------------------------------------------------------------------------
                
            }
            return IsAccPereidValid;
        }

        /// <summary>
        /// To set the form title in runtime
        /// </summary>

        private void SetTitle()
        {
            this.Text = AccYearId == 0 ? this.GetMessage(MessageCatalog.Master.AccountingPeriod.ACCOUNTING_PERIOD_ADD_CAPTION) : this.GetMessage(MessageCatalog.Master.AccountingPeriod.ACCOUNTING_PERIOD_EDIT_CAPTION);
        }


        private void frmAccountingPeriodAdd_Load(object sender, EventArgs e)
        {

            SelectedLang = this.AppSetting.LanguageId;
            if (SelectedLang == "id-ID")
            {
                this.Width = 370;
                emptySpaceItem2.Width = 153;
            }
            else if (SelectedLang == "pt-PT")
            {
                emptySpaceItem2.Width = 127;
            }
            else
            {
                lcgBooksBeginning.Width = 216;
            }
            if (AccYearId == 0 && AccCount == 0)
            {
                //On 29/10/2024, To fix fy based on country or multi currency
                DateTime dtYearFrom = new DateTime(this.UtilityMember.DateSet.ToDate(this.UtilityMember.DateSet.GetDateToday(), false).Year, 4, 1);
                if (this.AppSetting.AllowMultiCurrency == 1 || this.AppSetting.IsCountryOtherThanIndia)
                {
                    dtYearFrom = new DateTime(this.UtilityMember.DateSet.ToDate(this.UtilityMember.DateSet.GetDateToday(), false).Year, 1, 1);
                }
                DateTime dtYearTo = dtYearFrom.AddYears(1).AddDays(-1);

                if (this.UtilityMember.DateSet.ToDate(this.UtilityMember.DateSet.GetDateToday(), false) < dtYearTo)
                {
                    if (this.UtilityMember.DateSet.ToDate(this.UtilityMember.DateSet.GetDateToday(), false).Month <= 3)
                    {
                        detYearFrom.DateTime = detBookbeginingFrom.DateTime = dtYearFrom.AddYears(-1);
                    }
                    else
                    {
                        detYearFrom.DateTime = detBookbeginingFrom.DateTime = dtYearFrom;
                    }
                }
                else
                {
                    detYearFrom.DateTime = detBookbeginingFrom.DateTime = dtYearFrom;
                }
            }
            SetBookBeginingDate();
            SetTitle();
            if (AccYearId == 0 && AccCount > 0) { YearFromDate(); }
        }

        private void AssignLedgerDetails()
        {
            try
            {
                if (AccYearId != 0)
                {
                    using (AccouingPeriodSystem accPeriodSystem = new AccouingPeriodSystem(AccYearId))
                    {
                        accPeriodSystem.AccPeriodId = AccYearId;
                        detBookbeginingFrom.Properties.MinValue = detYearFrom.DateTime = this.UtilityMember.DateSet.ToDate(accPeriodSystem.YearFrom, true);
                        detBookbeginingFrom.Properties.MaxValue = detYearTo.DateTime = this.UtilityMember.DateSet.ToDate(accPeriodSystem.YearTo, true);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }
        private void SetBookBeginingDate()
        {
            using (AccouingPeriodSystem accountingSystem = new AccouingPeriodSystem())
            {
                resultArgs = accountingSystem.FetchBooksBeginingFrom();
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    BooksBeginDate = resultArgs.DataSource.Table.Rows[0][accountingSystem.AppSchema.AccountingPeriod.BOOKS_BEGINNING_FROMColumn.ColumnName].ToString();
                    detBookbeginingFrom.DateTime = this.UtilityMember.DateSet.ToDate(BooksBeginDate, true);

                    if (AccYearId.ToString() != resultArgs.DataSource.Table.Rows[0][accountingSystem.AppSchema.AccountingPeriod.ACC_YEAR_IDColumn.ColumnName].ToString() || !ValidateBooksBegining())
                    {
                        detBookbeginingFrom.Enabled = false;
                        detYearFrom.Enabled = false;
                    }
                    if (booksBeginDate == string.Empty)
                    {
                        detBookbeginingFrom.Enabled = true;
                        IsFirstAccYear = true;
                    }
                }
                else
                {
                    IsFirstAccYear = true;
                    //  detBookbeginingFrom.Text = detYearFrom.Text;
                }
            }

        }
        private bool ValidateBooksBegining()
        {
            bool isValid = true;
            using (AccouingPeriodSystem accSystem = new AccouingPeriodSystem())
            {
                accSystem.BooksBeginingDate = detBookbeginingFrom.DateTime.ToString();
                resultArgs = accSystem.ValidateBooksBeginning();
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    isValid = false;
                }
            }
            return isValid;
        }

        private void YearFromDate()
        {
            using (AccouingPeriodSystem accountingSystem = new AccouingPeriodSystem())
            {
                resultArgs = accountingSystem.FetchYearTo();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    detYearFrom.DateTime = this.UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][accountingSystem.AppSchema.AccountingPeriod.YEAR_TOColumn.ColumnName].ToString(), false).AddDays(1);
                    detYearFrom.Properties.MinValue = this.UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][accountingSystem.AppSchema.AccountingPeriod.YEAR_TOColumn.ColumnName].ToString(), false).AddDays(1);
                    detYearFrom.Enabled = false;

                }
            }
        }

        private void detYearTo_Leave(object sender, EventArgs e)
        {
            if (detBookbeginingFrom.Enabled)
            {
                detBookbeginingFrom.Properties.MinValue = detYearFrom.DateTime;
                detBookbeginingFrom.Properties.MaxValue = detYearTo.DateTime;
            }
        }

        private void UpdateTransacationPeriod(int accid)
        {
            using (GlobalSetting globalSystem = new GlobalSetting())
            {
                resultArgs = globalSystem.UpdateAccountingPeriod(accid.ToString());
                if (resultArgs.Success)
                {
                    using (AccouingPeriodSystem accountingSystem = new AccouingPeriodSystem())
                    {
                        resultArgs = accountingSystem.FetchActiveTransactionPeriod();
                        if (resultArgs.DataSource != null && resultArgs.RowsAffected > 0)
                        {
                            this.AppSetting.AccPeriodInfo = resultArgs.DataSource.Table.DefaultView;
                            this.SetTransacationPeriod();
                        }
                    }
                }
            }

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.F3))
            {
                if (detYearFrom.Enabled)
                {
                    detYearFrom.Focus();
                }
                else
                {
                    detYearTo.Focus();
                }
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        private void detYearFrom_Leave(object sender, EventArgs e)
        {
            detYearTo.DateTime = CalculateYearTo();
            if (string.IsNullOrEmpty(BooksBeginDate) || AccCount == 0)
            {
                detBookbeginingFrom.DateTime = detYearFrom.DateTime;
                detBookbeginingFrom.Properties.MinValue = detYearFrom.DateTime;
                detBookbeginingFrom.Properties.MaxValue = detYearTo.DateTime;
            }
        }

        private void frmAccountingPeriodAdd_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (AccountYearId == 0 && AccYearId == 0 && IsFirstAccYear && AccCount == 0)
            {
                Bosco.Utility.ConfigSetting.SettingProperty.Is_Application_Logout = true;
                Application.Restart();
            }
        }


    }
}