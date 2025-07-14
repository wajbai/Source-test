using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ACPP.Modules.Transaction;
using Bosco.Model.Business;
using Bosco.Utility;
using Bosco.Model.Transaction;
using AcMEDSync.Model;
using Bosco.Model.UIModel;

namespace ACPP.Modules.UIControls
{
    public partial class ucTransViewOpeningBalDetails : DevExpress.XtraEditors.XtraUserControl
    {
        #region VariableDeclaration
        private string CashTransModeOpen = "";
        private string BankTransModeOpen = "";
        CommonMember utilityMember = null;
        ucTransBalance ucTrans = new ucTransBalance();
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Constructor
        public ucTransViewOpeningBalDetails()
        {
            InitializeComponent();
        }
        #endregion

        #region Property
        private int projectid = 0;
        public int ProjectId
        {
            get { return projectid; }
            set { projectid = value; }
        }
        private string dateFrom = string.Empty;
        public string OpeningDateFrom
        {
            get { return dateFrom; }
            set { dateFrom = value; }
        }

        private string dateto = string.Empty;
        public string OpeningDateTo
        {
            get { return dateto; }
            set { dateto = value; }
        }
        private CommonMember UtilityMember
        {
            get
            {
                if (utilityMember == null)
                {
                    utilityMember = new CommonMember();
                }
                return utilityMember;
            }
        }

        public DevExpress.XtraEditors.PopupContainerControl AssignCash
        {
            get { return popupceCash.Properties.PopupControl; }
            set { popupceCash.Properties.PopupControl = value; }
        }

        public DevExpress.XtraEditors.PopupContainerControl AssingBank
        {
            get { return popupceBank.Properties.PopupControl; }
            set { popupceBank.Properties.PopupControl = value; }
        }

        public DevExpress.XtraEditors.PopupContainerControl AssingFD
        {
            get { return popupecFD.Properties.PopupControl; }
            set { popupecFD.Properties.PopupControl = value; }
        }

        /// <summary>
        /// On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
        /// 
        /// This date is apart from balance date. If we take reports for given period (DATE_FROM and DATE_TO)
        /// When we show closing balance for DATE_TO, we have to check bank ledger closed date for DATE_FROM
        /// </summary>
        public string BankClosedDate { get; set; }

        #endregion

        #region Events
        private void ucTransViewOpeningBalDetails_Load(object sender, EventArgs e)
        {
            // GetOpBalance();
        }
        private void popupceCash_Click(object sender, EventArgs e)
        {
            AssignCash = ucTrans.PopupContainerControl;
            ucTrans.gcData.DataSource = null;
            resultArgs = FetchTransBalance((int)FixedLedgerGroup.Cash);
            ucTrans.LedgerName = "Cash Ledger";  //objbase(MessageCatalog.Transaction.VocherTransaction.TRANSACTION_CASH_LEDGER);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
            {
                ucTrans.gcData.DataSource = resultArgs.DataSource.Table;
                ucTrans.gcData.RefreshDataSource();
            }
        }

        private void popupceBank_Click(object sender, EventArgs e)
        {
            AssingBank = ucTrans.PopupContainerControl;
            ucTrans.gcData.DataSource = null;
            resultArgs = FetchTransBalance((int)FixedLedgerGroup.BankAccounts);
            ucTrans.LedgerName = "Bank Ledger";//this.GetMessage(MessageCatalog.Transaction.VocherTransaction.TRANSACTION_BANK_LEDGER);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
            {
                ucTrans.gcData.DataSource = resultArgs.DataSource.Table;
                ucTrans.gcData.RefreshDataSource();
            }
        }

        private void popupecFD_Click(object sender, EventArgs e)
        {
            AssingFD = ucTrans.PopupContainerControl;
            ucTrans.gcData.DataSource = null;
            // resultArgs = FetchFDOPBalance((int)FixedLedgerGroup.FixedDeposit);
            resultArgs = FetchTransFDClosingBalance((int)FixedLedgerGroup.FixedDeposit, FDTypes.OP);
            ucTrans.LedgerName = "FD Ledger"; //this.GetMessage(MessageCatalog.Transaction.VocherTransaction.TRANSACTION_FD_LEDGER);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
            {
                DataTable dtFDAccountList = resultArgs.DataSource.Table;//added by sugan--to remove zero balance FD Accounts
                DataView dvFDAccountList = dtFDAccountList.AsDataView();//added by sugan
                dvFDAccountList.RowFilter = "AMOUNT<>0";
                ucTrans.gcData.DataSource = dvFDAccountList.ToTable();
                ucTrans.ShowBaseLedgerGroupRow = true;
                dvFDAccountList.RowFilter = "";
               // ucTrans.gcData.DataSource = resultArgs.DataSource.Table;
                ucTrans.gcData.RefreshDataSource();
            }
        }

        #endregion

        #region Methods
        private ResultArgs FetchTransFDClosingBalance(int GroupId, FDTypes TransType)
        {
            using (VoucherTransactionSystem voucherTransSystem = new VoucherTransactionSystem())
            {
                voucherTransSystem.ProjectId = ProjectId;
                voucherTransSystem.GroupId = GroupId;
                if (TransType == FDTypes.OP)
                {
                    // voucherTransSystem.BalanceDate = deDateFrom.DateTime.AddDays(-1);
                    voucherTransSystem.BalanceDate = UtilityMember.DateSet.ToDate(OpeningDateFrom, false).AddDays(-1);
                }
                else
                {
                    // voucherTransSystem.BalanceDate = deDateTo.DateTime;
                    voucherTransSystem.BalanceDate = UtilityMember.DateSet.ToDate(OpeningDateTo, false);
                }
                resultArgs = voucherTransSystem.FetchTransFDClosingBalance();
                //On 11/10/2024, To Skip default ledgers for multi currency or other than country
                using (MappingSystem mappingsystem = new MappingSystem())
                {
                    resultArgs = mappingsystem.EnforceSkipDefaultLedgers(resultArgs, "ID");
                }
            }
            return resultArgs;
        }
        private ResultArgs FetchTransBalance(int GroupId)
        {
            using (VoucherTransactionSystem voucherTransSystem = new VoucherTransactionSystem())
            {
                voucherTransSystem.ProjectId = ProjectId;
                voucherTransSystem.GroupId = GroupId;
                voucherTransSystem.BalanceDate = UtilityMember.DateSet.ToDate(OpeningDateFrom, false).AddDays(-1);
                voucherTransSystem.BankClosedDate = BankClosedDate;
                resultArgs = voucherTransSystem.FetchTransBalance();
                
                //On 11/10/2024, To Skip default ledgers for multi currency or other than country
                using (MappingSystem mappingsystem = new MappingSystem())
                {
                    resultArgs = mappingsystem.EnforceSkipDefaultLedgers(resultArgs, "ID");
                }
            }
            return resultArgs;
        }
        //to get Opening Balances
        public void GetOpBalance()
        {
            using (BalanceSystem balanceSystem = new BalanceSystem())
            {
                balanceSystem.BankClosedDate = BankClosedDate;
                BalanceProperty balanceProperty = balanceSystem.GetBankBalance(ProjectId, OpeningDateFrom, BalanceSystem.BalanceType.OpeningBalance);
                lblBankCurrency.Text = balanceSystem.Currency;
                lblBank.Text = this.UtilityMember.NumberSet.ToNumber(balanceProperty.Amount) + " " + balanceProperty.TransMode;
                BankTransModeOpen = balanceProperty.TransMode.ToString();
                BalanceProperty cashBalanceProperty = balanceSystem.GetCashBalance(ProjectId, OpeningDateFrom, BalanceSystem.BalanceType.OpeningBalance);
                lblCashCurrency.Text = balanceSystem.Currency;
                lblCash.Text = this.UtilityMember.NumberSet.ToNumber(cashBalanceProperty.Amount) + " " + cashBalanceProperty.TransMode.ToString();
                CashTransModeOpen = cashBalanceProperty.TransMode.ToString();
                BalanceProperty FDBalanceProperty = balanceSystem.GetFDBalance(ProjectId, OpeningDateFrom, BalanceSystem.BalanceType.OpeningBalance);
                lblFDCurrency.Text = balanceSystem.Currency;
                lblFD.Text = this.UtilityMember.NumberSet.ToNumber(FDBalanceProperty.Amount) + " " + FDBalanceProperty.TransMode.ToString();
            }

            //On 03/10/2024, To fix currency symbol width
            lblCashCurrency.Width = 15;
            if (lblCashCurrency.Text.Trim().Length > 1) lblCashCurrency.Width = 25;
            lblBankCurrency.Width = 15;
            if (lblBankCurrency.Text.Trim().Length > 1) lblBankCurrency.Width = 25;
            lblFDCurrency.Width = 15;
            if (lblFDCurrency.Text.Trim().Length > 1) lblFDCurrency.Width = 25;
        }

        public void ShowOpNegative()
        {
            // to show negative for opening balance
            if (CashTransModeOpen == TransactionMode.CR.ToString())
            {
                lblCash.AppearanceItemCaption.ForeColor = Color.Red;
            }
            else
            {
                lblCash.AppearanceItemCaption.ForeColor = Color.Blue;
            }
            if (BankTransModeOpen == TransactionMode.CR.ToString())
            {
                lblBank.AppearanceItemCaption.ForeColor = Color.Red;
            }
            else
            {
                lblBank.AppearanceItemCaption.ForeColor = Color.Blue;
            }
        }
        #endregion
    }
}
