using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using ACPP.Modules.UIControls;
using System.Globalization;
using Bosco.Model.UIModel;
using DevExpress.XtraEditors;
using Bosco.Utility;

namespace ACPP.Modules.Master
{
    public partial class frmAllotFund : frmFinanceBase
    {

        #region Variables
        const string AMOUNT = "AMOUNT";
        const string MONTH = "MONTH";
        const string LEDGERID = "LEDGER_ID";
        string LedgerName = string.Empty;
        int ControlCount = 0;
        DataTable dtLedgerDetails;
        int LedgerId, BudgetId = 0;
        private delegate void PanelBind(UcBudgetAllotFund tn);
        UcBudgetAllotFund AllotFund = null;
        #endregion

        #region Property
        public int MonthFrom
        {
            set;
            get;
        }

        public int MonthTo
        {
            set;
            get;
        }

        public int YearFrom
        {
            set;
            get;
        }
        public int YearTo
        {
            set;
            get;
        }

        double amount = 0.00;
        public double TotalAmount { get { return amount; } set { amount = value; } }

        #endregion

        #region Constructor
        public frmAllotFund()
        {
            InitializeComponent();
        }
        public frmAllotFund(string LedgerName, DataTable dtLedgerDetails, int LedgerId, int BudgetId)
            : this()
        {
            this.LedgerName = LedgerName;
            this.dtLedgerDetails = dtLedgerDetails;
            this.LedgerId = LedgerId;
            this.BudgetId = BudgetId;
        }
        #endregion

        #region Events
        private void frmAllotFund_Load(object sender, EventArgs e)
        {
            lblFund.Text = LedgerName;
            txtAmount.Text = TotalAmount.ToString();
            LoadData();
        }
        public void UpdateOnEdit_ValueChanged(object sender, EventArgs e)
        {
            double Amount = UtilityMember.NumberSet.ToDouble(UtilityMember.NumberSet.ToDouble(txtAmount.Text).ToString());
            double AmountAdjust = 0;
            for (int i = 0; i < ControlCount; i++)
            {
                AmountAdjust += UtilityMember.NumberSet.ToDouble(this.Controls[0].Controls[i].Controls[0].Text);
            }
            txtAmount.Text = AmountAdjust.ToString();
        }
        #endregion

        #region Methods
        private void LoadData()
        {
            try
            {
                using (BudgetSystem budgetSystem = new BudgetSystem(LedgerId, BudgetId))
                {
                    string Month = string.Empty;
                    if (YearTo > YearFrom) //Comparing the year Ex :   Mar-2012 to Feb-2013
                    {
                        for (int i = MonthFrom; i <= 12; i++) // From the month of YearFrom to December i.e 12 (From the Ex. Month 3-12 is calculated)
                            Month += i.ToString() + ',';
                        if (MonthFrom.Equals(MonthTo))
                        {
                            for (int j = 1; j < MonthTo; j++)  //Then concatenating the remaining month from YearTo (From the Ex. the reamaining month 1-2 is calculated and merged)
                                Month += j.ToString() + ',';
                        }
                        else
                        {
                            for (int j = 1; j <= MonthTo; j++)  //Then concatenating the remaining month from YearTo (From the Ex. the reamaining month 1-2 is calculated and merged)
                                Month += j.ToString() + ',';
                        }
                    }
                    else
                    {
                        for (int j = MonthFrom; j <= MonthTo; j++) //when the YearFrom and YearTo are in the Same year
                            Month += j.ToString() + ',';
                    }
                    Month = Month.Trim(',');
                    string[] arrMonth = Month.Split(',');

                    for (int i = 0; i < arrMonth.Length; i++)
                    {
                        AllotFund = new UcBudgetAllotFund();
                        AllotFund.objAllotFund = this;
                        AllotFund.Label = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(UtilityMember.NumberSet.ToInteger(arrMonth[i]));
                        AllotFund.ControlCount = i;
                        ControlCount = ++AllotFund.ControlCount;
                        AllotFund.BudgetSystem = budgetSystem;
                        AllotFund.dtFundDetails = dtLedgerDetails;
                        AllotFund.LedgerId = LedgerId;
                        AllotFund.BudgetId = BudgetId;
                        AllotFund.AssignContol();
                        CreatScreen(AllotFund);
                        this.Height += AllotFund.ControlName.Height;
                        Application.DoEvents();
                    }
                }
                this.CenterToScreen();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        private void CreatScreen(UcBudgetAllotFund UcCtrl)
        {
            this.flowLayoutPanel1.Controls.Add(UcCtrl);
        }
        #endregion


        private TextEdit txtCtrl = null;
        public TextEdit ControlName
        {
            get { return txtCtrl; }
            set { txtCtrl = value; }
        }
        private void btnSplit_Click(object sender, EventArgs e)
        {
            //double SplitAmount = UtilityMember.NumberSet.ToInteger(UtilityMember.NumberSet.ToDouble(txtAmount.Text).ToString()) / ControlCount; //by Aldrin. When the big amount is entred the split is not working.
            double SplitAmount = UtilityMember.NumberSet.ToDouble(txtAmount.Text) / ControlCount;
            int Reminder = UtilityMember.NumberSet.ToInteger(UtilityMember.NumberSet.ToDouble(txtAmount.Text).ToString()) % ControlCount;
            for (int i = 0; i < ControlCount; i++)
            {
                this.Controls[0].Controls[i].Controls[0].Text = i.Equals(0) ? (Reminder + SplitAmount).ToString() : SplitAmount.ToString();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ValidateSplitAmount())
            {
                double SplitAmount = 0;
                TotalAmount = UtilityMember.NumberSet.ToDouble(txtAmount.Text);
                for (int i = 0; i < ControlCount; i++)
                {
                    //Calculate value of all the texboxes
                    SplitAmount += UtilityMember.NumberSet.ToDouble(this.Controls[0].Controls[i].Controls[0].Text);
                }
                if ((SplitAmount.Equals(UtilityMember.NumberSet.ToDouble(txtAmount.Text)) || SplitAmount.Equals(0)) || string.IsNullOrEmpty(txtAmount.Text))
                {
                    for (int i = 0; i < ControlCount; i++)
                    {
                        //Getting the name of the Month name and converting Month name to respective Number (i.e March==3)
                        int Month = Array.IndexOf(CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames, (this.Controls[0].Controls[i].Controls[1].Text.Substring(0, 3))) + 1;
                        if (AllotFund.dtFundDetails != null)
                        {
                            foreach (DataRow dr in AllotFund.dtFundDetails.Rows)
                            {
                                if (UtilityMember.NumberSet.ToInteger(dr[MONTH].ToString()).Equals(Month) && UtilityMember.NumberSet.ToInteger(dr[LEDGERID].ToString()).Equals(LedgerId))
                                    dr[AMOUNT] = UtilityMember.NumberSet.ToDouble(this.Controls[0].Controls[i].Controls[0].Text);
                                if (UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString()).Equals(LedgerId))
                                {
                                    dr["TOTAL"] = TotalAmount;
                                }

                            }
                        }
                    }
                    this.Close();
                }
                else ShowMessageBox(this.GetMessage(MessageCatalog.Master.AuditLockType.AMOUNT_MISMATCHED_WITH_SPLITED_AMOUNT));
            }
        }

        private bool ValidateSplitAmount()
        {
            double SplitAmount = 0;
            bool IsTrue = true;
            for (int i = 0; i < ControlCount; i++)
            {
                //Calculate value of all the texboxes
                SplitAmount = UtilityMember.NumberSet.ToDouble(this.Controls[0].Controls[i].Controls[0].Text);
                if (SplitAmount == 0)
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Budget.NO_SPLIT_AMOUNT));
                    IsTrue = false;
                    break;
                }
            }
            return IsTrue;
        }

    }
}
