/************************************************************************************************************************
 *                                              Form Name  :frmBreakUp.cs
 *                                              Purpose    :To get the Breakup details
 *                                              Author     : Carmel Raj M
 ************************************************************************************************************************/
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Bosco.Utility;
using Bosco.Model.UIModel;
using DevExpress.XtraLayout.Utils;
using Bosco.Model.Transaction;
using DevExpress.XtraGrid;

namespace ACPP.Modules.Master
{
    public partial class frmBreakUp : frmFinanceBaseAdd
    {
        #region Variables
        //const int TRANSACTION_FROM_WITH = 550;
        //const int TRANSACTION_FORM_HEIGHT = 315;
        //const int MAPPING_FROM_WITH = 550;
        //const int MAPPING_FORM_HEIGHT = 340;
        //bool BreakUpFlag = true;
        bool IsTransaction = false;
        int BankAccountId;
        int BankAccountID;
        string Trans_Mode = string.Empty;
        string ProjectName = string.Empty;
        private string FDAccountNumber = string.Empty;
        string DataFlag = "Add";
        int ProjectId = 0;
        string Trans_mode = string.Empty;
        double Amount;
        int FDRegisterId = 0;
        private DateTime MaturityDate;

        //  DataTable dtDateTimeCheck = new DataTable();
        DateTime deMaturityDateTemp;
        ResultArgs resultArgs;
        // BankAccountSystem FDUpdation;
        //  frmMapProjectLedger RefAccountMapping;


        #endregion

        #region Constructor

        public frmBreakUp()
        {
            InitializeComponent();
        }

        public frmBreakUp(double Amount, int BankAccountID, string ProjectName)
            : this()
        {
            this.Amount = Amount;
            this.BankAccountID = BankAccountID;
            //  this.FDUpdation = FDUpdation;
            // this.RefAccountMapping = RefAccountMapping;
            this.ProjectName = ProjectName;
            // this.ProjectId = ProjectId;
            // this.Trans_mode = TransMode;
            this.FDAccountNumber = AccountNumber;
            //    this.MaturityDate = this.UtilityMember.DateSet.ToDate(MaturityDate, false);
            Trans_Mode = "OP";
        }
        //public frmBreakUp(double Amount, int BankAccountID, string ProjectName, int ProjectId = 0, string TransMode = "", string AccountNumber = "", string MaturityDate = "")
        //    : this()
        //{
        //    this.Amount = Amount;
        //    this.BankAccountID = BankAccountID;
        //    //  this.FDUpdation = FDUpdation;
        //    // this.RefAccountMapping = RefAccountMapping;
        //    this.ProjectName = ProjectName;
        //    this.ProjectId = ProjectId;
        //    this.Trans_mode = TransMode;
        //    this.FDAccountNumber = AccountNumber;
        //    this.MaturityDate = this.UtilityMember.DateSet.ToDate(MaturityDate, false);
        //    Trans_Mode = "OP";
        //}

        public frmBreakUp(double Amount, int BankAccountID, BankAccountSystem FDUpdation, string ProjectName)
            : this()
        {
            this.Amount = Amount;
            this.BankAccountID = BankAccountID;
            // this.FDUpdation = FDUpdation;
            this.ProjectName = ProjectName;
            IsTransaction = true;
            Trans_Mode = "TR";
        }
        #endregion

        #region Property
        public DateTime CreatedDate { get; set; }

        public decimal InterestAmount { get; set; }

        public string AccountNumber { get; set; }

        public DataTable dtFixedDeposite
        {
            get;
            set;
        }
        #endregion

        #region Events

        private void frmBreakUp_Load(object sender, EventArgs e)
        {
            UcAccountDetails.VisibleBanner = (BankAccountID > 0) ? true : false;
            deMaturityDate.Properties.MinValue = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            if (BankAccountID == 0)
            {
                layoutControlItem11.Visibility = LayoutVisibility.Never;
                this.Height = 205;
              //  deMaturityDate.DateTime = this.MaturityDate;
                this.MaturityDate = CreatedDate;
                deMaturityDate.DateTime = CreatedDate;
            }
            else
                layoutControlItem11.Visibility = LayoutVisibility.Always;
            lblPrincipalAmount.Text = UtilityMember.NumberSet.ToCurrency(Amount);
            FetchFDDetails();
            txtYear.Select();
            txtYear.Focus();
        }

        private void deMaturityDate_Leave(object sender, EventArgs e)
        {
            //CalculateMaturityDate();
            DateTime deMaturityD = deMaturityDateTemp = deMaturityDate.DateTime;
            CalculateMaturityDateByDate(deMaturityD);
        }

        private void CalculateMaturityDate()
        {
            if (BankAccountId != 0)
            {
                deMaturityDate.DateTime = CalculateMaturityDate(CreatedDate);
            }
            else
            {
                deMaturityDate.DateTime = CalculateMaturityDate(this.MaturityDate);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtYear_EditValueChanged(object sender, EventArgs e)
        {
            CalculateMaturityDate();
        }

        private void txtMonth_EditValueChanged(object sender, EventArgs e)
        {
            CalculateMaturityDate();
        }

        private void txtDay_EditValueChanged(object sender, EventArgs e)
        {
            //DateTime deMaturityD = deMaturityDateTemp = deMaturityDate.DateTime;
            //CalculateMaturityDateByDate(deMaturityD);
            CalculateMaturityDate();

        }

        private void txtInterestRate_Leave(object sender, EventArgs e)
        {
            double amount = 0;
            double interestRate = 0;
            try
            {
                if (UtilityMember.NumberSet.ToDecimal(txtInterestRate.Text) != 0)
                {
                    //  this.ShowMessageBox(GetMessage(MessageCatalog.Master.BreakUp.BREAKUP_INTEREST_RATE_NEGATIVE));
                    amount = this.UtilityMember.NumberSet.ToDouble(Amount.ToString());
                    interestRate = this.UtilityMember.NumberSet.ToDouble(txtInterestRate.Text);
                    if (interestRate != 0)
                    {
                        double calculateAmount = interestRate != 0 ? (amount / 100) * interestRate : amount;
                        InterestAmount = this.UtilityMember.NumberSet.ToDecimal(calculateAmount.ToString());
                        lblAmount.Text = UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(InterestAmount.ToString()));
                    }
                    txtInterestRate.Focus();
                }
                SetBorderColor(txtInterestRate);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFDDetails();
        }

        #endregion

        #region Methods

        private DataTable FetchBreakUpDetails()
        {
            using (BreakUpSystem breakUpSystem = new BreakUpSystem())
            {
                breakUpSystem.AccounNumber = UcAccountDetails.BankAccountNumber;
                resultArgs = breakUpSystem.FetchBreakUpDetails();
            }
            return resultArgs.DataSource.Table;
        }

        private bool ValidateUserInput()
        {
            bool Sucess = true;
            //if (string.IsNullOrEmpty(txtInterestRate.Text))
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FixedDeposit.FD_INTEREST_RATE_EMPTY));
            //    SetBorderColor(txtInterestRate);
            //    txtInterestRate.Focus();
            //    Sucess = false;
            //}
            //if (UtilityMember.NumberSet.ToDouble(txtInterestRate.Text) <= 0) //else 
            //{
            //    this.ShowMessageBox(GetMessage(MessageCatalog.Master.BreakUp.BREAKUP_INTEREST_RATE_EMPTY_ZERO));
            //    SetBorderColor(txtInterestRate);
            //    txtInterestRate.Focus();
            //    Sucess = false;
            //}
            if (string.IsNullOrEmpty(deMaturityDate.Text))//else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FixedDeposit.FD_MATURITY_DATE_EMPTY));
                SetBorderColor(deMaturityDate);
                deMaturityDate.Focus();
                Sucess = false;
            }
            else
            {
                if (deMaturityDate.DateTime.Date < this.UtilityMember.DateSet.ToDate(this.MaturityDate.ToShortDateString(), false).Date)
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FixedDeposit.FD_MATURITY_DATE_GREATER_THAN_CREATED_DATE));
                    deMaturityDate.Focus();
                    Sucess = false;
                }
            }
            return Sucess;
        }

        private DateTime CalculateMaturityDate(DateTime dteCreatedDate)
        {
            try
            {
                int PeriodYear = this.UtilityMember.NumberSet.ToInteger(txtYear.Text);
                int PeriodMth = this.UtilityMember.NumberSet.ToInteger(txtMonth.Text);
                int PeriodDay = this.UtilityMember.NumberSet.ToInteger(txtDay.Text);
                DateTime CreatedDate = dteCreatedDate.AddYears(PeriodYear).AddMonths(PeriodMth).AddDays(PeriodDay);
                return CreatedDate;
            }
            catch (Exception)
            {
                return CreatedDate;
            }
            finally { }
        }

        private void FetchFDDetails()
        {
            using (BankAccountSystem bankAccountSystem = new BankAccountSystem())
            {
                bankAccountSystem.BankAccountId = BankAccountID;
                resultArgs = bankAccountSystem.FetchFDNumberById(BankAccountID.ToString(), Trans_Mode);
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    DataFlag = "Update";
                if (dtFixedDeposite != null && dtFixedDeposite.Rows.Count > 0)
                {
                    if ((UtilityMember.NumberSet.ToInteger(dtFixedDeposite.Rows[0]["Day"].ToString()) > 0 || UtilityMember.NumberSet.ToInteger(dtFixedDeposite.Rows[0]["Month"].ToString()) > 0
                        || UtilityMember.NumberSet.ToInteger(dtFixedDeposite.Rows[0]["Year"].ToString()) > 0 || UtilityMember.NumberSet.ToDecimal(dtFixedDeposite.Rows[0]["InterestRate"].ToString()) > 0)
                        || (UtilityMember.NumberSet.ToInteger(dtFixedDeposite.Rows[0]["Day"].ToString()) > 0 && UtilityMember.NumberSet.ToInteger(dtFixedDeposite.Rows[0]["Month"].ToString()) > 0
                        && UtilityMember.NumberSet.ToInteger(dtFixedDeposite.Rows[0]["Year"].ToString()) > 0 && UtilityMember.NumberSet.ToDecimal(dtFixedDeposite.Rows[0]["InterestRate"].ToString()) > 0))
                    {
                        if (Trans_Mode.Equals("OP"))
                        {
                            DataTable dtTemp;
                            var RecordCount = (from d in dtFixedDeposite.AsEnumerable()
                                               where ((d.Field<Int32>("BankAccountId") == BankAccountID))
                                               select d);
                            if (RecordCount.Count() > 0)
                            {
                                dtTemp = RecordCount.CopyToDataTable();
                                txtYear.Text = dtTemp.Rows[0]["Year"].ToString();
                                txtMonth.Text = dtTemp.Rows[0]["Month"].ToString();
                                txtDay.Text = dtTemp.Rows[0]["Day"].ToString();
                                lblAmount.Text = dtTemp.Rows[0]["InterestAmount"].ToString();
                                deMaturityDate.Text = dtTemp.Rows[0]["MaturityDate"].ToString();
                                txtInterestRate.Text = dtTemp.Rows[0]["InterestRate"].ToString();
                                cboPeriod.SelectedIndex = dtTemp.Rows[0]["INTEREST_TERM"] != null ? this.UtilityMember.NumberSet.ToInteger(dtTemp.Rows[0]["INTEREST_TERM"].ToString()) : 0;
                                rboPeriodically.SelectedIndex = dtTemp.Rows[0]["IS_INTEREST_RECEIVED_PERIODICALLY"] != null ? this.UtilityMember.NumberSet.ToInteger(dtTemp.Rows[0]["IS_INTEREST_RECEIVED_PERIODICALLY"].ToString()) : 0;
                                txtInterestTerm.Text = dtTemp.Rows[0]["INTEREST_PERIOD"].ToString();
                                resultArgs = bankAccountSystem.FetchFDDetailsByID(Trans_Mode);
                                if (resultArgs.Success && resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
                                {
                                   // deMaturityDate.DateTime = string.IsNullOrEmpty(bankAccountSystem.MaturityDate) ? bankAccountSystem.OpenedDate :this.UtilityMember.DateSet.ToDate(bankAccountSystem.MaturityDate,false);
                                   // CreatedDate = bankAccountSystem.OpenedDate;
                                    BankAccountId = bankAccountSystem.BankAccountId;
                                    UcAccountDetails.BankAccountNumber = bankAccountSystem.AccountNumber;
                                //    UcAccountDetails.BankCreatedOn = bankAccountSystem.OpenedDate.ToShortDateString();
                                    UcAccountDetails.BankName = bankAccountSystem.BankName;
                                    UcAccountDetails.BankBranchName = bankAccountSystem.BranchName;
                                    UcAccountDetails.Project = ProjectName;
                                    // FDUpdation.FDRegisterId = bankAccountSystem.FDRegisterId;
                                }
                            }
                        }
                        else
                            FetchFDDetails(bankAccountSystem);
                    }
                }
                else
                    FetchFDDetails(bankAccountSystem);
                if (Trans_Mode.Equals("TR"))
                    AccountNumber = bankAccountSystem.AccountNumber;
            }
        }

        private void FetchFDDetails(BankAccountSystem bankAccountSystem)
        {
            resultArgs = bankAccountSystem.FetchFDDetailsByID(Trans_Mode);
            if (resultArgs.Success && resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
              //  deMaturityDate.DateTime = string.IsNullOrEmpty(bankAccountSystem.MaturityDate) ?bankAccountSystem.OpenedDate : this.UtilityMember.DateSet.ToDate(bankAccountSystem.MaturityDate,false);
               // CreatedDate = bankAccountSystem.OpenedDate;
                txtYear.Text = bankAccountSystem.PeriodYr.ToString();
                txtMonth.Text = bankAccountSystem.PeriodMth.ToString();
                txtDay.Text = bankAccountSystem.PeriodDay.ToString();
                lblAmount.Text = bankAccountSystem.InterestAmount.ToString();
                rboPeriodically.SelectedIndex = bankAccountSystem.IsInterestReceivedPeriodically == 0 ? 1 : 0;
                txtInterestTerm.Text = bankAccountSystem.InterestPeriod.ToString();
                cboPeriod.SelectedIndex = bankAccountSystem.InterestTerm;
                txtInterestRate.Text = bankAccountSystem.InterestRate.ToString();
                BankAccountId = bankAccountSystem.BankAccountId;
                FDRegisterId = bankAccountSystem.FDRegisterId;
                UcAccountDetails.BankAccountNumber = bankAccountSystem.AccountNumber;
              //  UcAccountDetails.BankCreatedOn = bankAccountSystem.OpenedDate.ToShortDateString();
                UcAccountDetails.BankName = bankAccountSystem.BankName;
                UcAccountDetails.BankBranchName = bankAccountSystem.BranchName;
                UcAccountDetails.Project = ProjectName;
            }
        }

        private DataTable AddBankIdColumn(DataTable dtTable)
        {
            if (!dtTable.Columns.Contains("BankAccountId"))
                dtTable.Columns.Add("BankAccountId", typeof(Int32));
            if (!dtTable.Columns.Contains("AccountNo"))
                dtTable.Columns.Add("AccountNo", typeof(String));
            return dtTable;
        }

        private bool CalculateMaturityDateByDate(DateTime deMaturityD, bool AddMaturityDate = false)
        {
            bool MaturityDateValid = true;
            try
            {
                if (AddMaturityDate)
                {
                    if (!(deMaturityD < CreatedDate))
                        deMaturityDate.DateTime = deMaturityD.AddYears(UtilityMember.NumberSet.ToInteger(txtYear.Text)).AddMonths(UtilityMember.NumberSet.ToInteger(txtMonth.Text)).AddDays(UtilityMember.NumberSet.ToInteger(txtDay.Text));
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FixedDeposit.FD_MATURITY_DATE_LESS_THAN_CREATED_DATE));
                        MaturityDateValid = false;
                    }
                }
                else
                {
                    if (!(deMaturityD < CreatedDate))
                    {
                        if (BankAccountId != 0)
                        {
                            DateTime TempMaturityDate = deMaturityDateTemp;
                            txtYear.Text = (TempMaturityDate.Year - CreatedDate.Year).ToString();
                            if (TempMaturityDate.Month > CreatedDate.Month)
                                txtMonth.Text = (TempMaturityDate.Month - CreatedDate.Month).ToString();
                            else
                                txtMonth.Text = (CreatedDate.Month - TempMaturityDate.Month).ToString();
                            if (TempMaturityDate.Day > CreatedDate.Day)
                                txtDay.Text = (TempMaturityDate.Day - CreatedDate.Day).ToString();
                            else
                                txtDay.Text = (CreatedDate.Day - TempMaturityDate.Day).ToString();
                        }
                        else
                        {
                            DateTime TempMaturityDate = deMaturityDateTemp;
                            txtYear.Text = (TempMaturityDate.Year - this.MaturityDate.Year).ToString();
                            if (TempMaturityDate.Month > CreatedDate.Month)
                                txtMonth.Text = (TempMaturityDate.Month - this.MaturityDate.Month).ToString();
                            else
                                txtMonth.Text = (this.MaturityDate.Month - TempMaturityDate.Month).ToString();
                            if (TempMaturityDate.Day > CreatedDate.Day)
                                txtDay.Text = (TempMaturityDate.Day - this.MaturityDate.Day).ToString();
                            else
                                txtDay.Text = (this.MaturityDate.Day - TempMaturityDate.Day).ToString();
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FixedDeposit.FD_MATURITY_DATE_LESS_THAN_CREATED_DATE));
                        MaturityDateValid = false;
                    }
                }
            }
            catch (Exception ee)
            {
                ShowMessageBox(ee.Message);
            }
            finally { }
            return MaturityDateValid;
        }

        private void SaveFDDetails()
        {
            try
            {
                if (ValidateUserInput())
                {
                    if (dtFixedDeposite == null)
                    {
                        dtFixedDeposite = new DataTable();
                        dtFixedDeposite.Columns.Add("Year", typeof(Int32));
                        dtFixedDeposite.Columns.Add("Month", typeof(Int32));
                        dtFixedDeposite.Columns.Add("Day", typeof(Int32));
                        dtFixedDeposite.Columns.Add("BankAccountId", typeof(Int32));
                        dtFixedDeposite.Columns.Add("InterestRate", typeof(Decimal));
                        dtFixedDeposite.Columns.Add("InterestAmount", typeof(Decimal));
                        dtFixedDeposite.Columns.Add("Amount", typeof(Decimal));
                        dtFixedDeposite.Columns.Add("MaturityDate", typeof(string));
                        dtFixedDeposite.Columns.Add("AccountNumber", typeof(String));
                        dtFixedDeposite.Columns.Add("DataFlag", typeof(String));
                        dtFixedDeposite.Columns.Add("TransMode", typeof(String));
                        dtFixedDeposite.Columns.Add("BreakUpAccountNo", typeof(String));
                        dtFixedDeposite.Columns.Add("PROJECT_ID", typeof(Int32));
                        dtFixedDeposite.Columns.Add("TRANS_MODE", typeof(String));
                        dtFixedDeposite.Columns.Add("IS_INTEREST_RECEIVED_PERIODICALLY", typeof(Int32));
                        dtFixedDeposite.Columns.Add("INTEREST_TERM", typeof(Int32));
                        dtFixedDeposite.Columns.Add("INTEREST_PERIOD", typeof(Int32));
                        dtFixedDeposite.Columns.Add("CREATED_ON", typeof(string));
                        dtFixedDeposite.Columns.Add("FDREGISTERID", typeof(Int32));
                    }
                    var query = dtFixedDeposite.AsEnumerable().Where(r => r.Field<Int32>("BankAccountId") == BankAccountID);
                    foreach (var row in query.ToList())
                        row.Delete();
                    DataRow dr = dtFixedDeposite.NewRow();
                    dr["Year"] = UtilityMember.NumberSet.ToInteger(txtYear.Text);
                    dr["Month"] = UtilityMember.NumberSet.ToInteger(txtMonth.Text);
                    dr["Day"] = UtilityMember.NumberSet.ToInteger(txtDay.Text);
                    dr["BankAccountId"] = this.BankAccountID;
                    dr["InterestRate"] = UtilityMember.NumberSet.ToDecimal(txtInterestRate.Text);
                    dr["InterestAmount"] = ((decimal)Amount * UtilityMember.NumberSet.ToDecimal(txtInterestRate.Text)) / 100;
                    dr["Amount"] = UtilityMember.NumberSet.ToDecimal(Amount.ToString());
                    dr["MaturityDate"] = UtilityMember.DateSet.ToDate(deMaturityDate.Text);
                    dr["IS_INTEREST_RECEIVED_PERIODICALLY"] = rboPeriodically.SelectedIndex == 1 ? 0 : 1;
                    dr["INTEREST_TERM"] = cboPeriod.SelectedIndex;
                    dr["INTEREST_PERIOD"] = this.UtilityMember.NumberSet.ToInteger(txtInterestTerm.Text);
                    // dr["FDNumber"] = txtFDNumber.Text;
                    //dr["AccountNumber"] = UcAccountDetails.BankAccountNumber;
                    dr["AccountNumber"] = FDAccountNumber;
                    dr["DataFlag"] = DataFlag;
                    dr["TransMode"] = Trans_Mode;
                    //dr["BreakUpAccountNo"] = UcAccountDetails.BankAccountNumber;
                    dr["BreakUpAccountNo"] = FDAccountNumber;
                    dr["PROJECT_ID"] = ProjectId;
                    dr["FDREGISTERID"] = FDRegisterId;
                    dr["TRANS_MODE"] = Trans_mode;
                    dr["CREATED_ON"] = this.MaturityDate;
                    dtFixedDeposite.Rows.Add(dr);
                    this.Close();
                }

            }
            catch (Exception ee)
            {
                MessageRender.ShowMessage(ee.Message, true);
            }
        }
        #endregion
        private void rboPeriodically_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rboPeriodically.SelectedIndex == 1)
            {
                cboPeriod.SelectedIndex = 0;
                txtInterestTerm.Text = "0";
            }
            cboPeriod.Enabled = txtInterestTerm.Enabled = rboPeriodically.SelectedIndex == 0 ? true : false;
        }

        private void CalculatePeriodicallyInterest()
        {
            if (rboPeriodically.SelectedIndex == (int)Status.Active)
            {

            }
        }

        private DateTime CalculateInterestRateDate()
        {
            int PeriodicallyDays = 0;
            DateTime dtePeriodicallyDate = this.UtilityMember.DateSet.ToDate(this.MaturityDate.ToShortDateString(), false);

            PeriodicallyDays = this.UtilityMember.NumberSet.ToInteger(txtInterestTerm.Text);
            switch (cboPeriod.SelectedIndex)
            {
                case 0:
                    deMaturityDate.DateTime = deMaturityDate.DateTime.AddDays(PeriodicallyDays);
                    break;
                case 1:
                    deMaturityDate.DateTime = deMaturityDate.DateTime.AddMonths(PeriodicallyDays);
                    break;
                case 2:
                    deMaturityDate.DateTime = deMaturityDate.DateTime.AddYears(PeriodicallyDays);
                    break;
            }
            return deMaturityDate.DateTime;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.F3))
            {
                    deMaturityDate.Focus();
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }
    }
}
