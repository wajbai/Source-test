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
    public partial class ucTransviewClosingBalance : DevExpress.XtraEditors.XtraUserControl
    {
        #region VariableDeclaration
        string BankTransMode = string.Empty;
        string CashTransMode = string.Empty;
        ResultArgs resultArgs = new ResultArgs();
        ucTransBalance ucTrans = new ucTransBalance();
        CommonMember UtilityMemeber = null;
        #endregion

        #region Constructor
        public ucTransviewClosingBalance()
        {
            InitializeComponent();
        }
        #endregion

        #region Property
        public CommonMember UtilityMember
        {
            get
            {
                if (UtilityMemeber == null)
                {
                    UtilityMemeber = new CommonMember();
                }
                return UtilityMemeber;
            }
        }
                
        public string TitleCaption
        {
            set {
                lcEmptyLeft.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lblClsoingBalanceCap.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lblOtherTitle.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lblOtherTitle.Text = value;
            }
        }

        private int projectid = 0;
        public int ProjectId
        {
            get { return projectid; }
            set { projectid = value; }
        }

        private string datefrom = string.Empty;
        public string ClosingDateFrom
        {
            get { return datefrom; }
            set { datefrom = value; }
        }

        private string dateto = string.Empty;
        public string ClosingDateTo
        {
            get { return dateto; }
            set { dateto = value; }
        }

        public DevExpress.XtraEditors.PopupContainerControl AssingClosingCash
        {
            set { popupceCashClosingBal.Properties.PopupControl = value; }
        }

        public DevExpress.XtraEditors.PopupContainerControl AssingClosingBank
        {
            set { popupceBankClosingBal.Properties.PopupControl = value; }
        }

        public DevExpress.XtraEditors.PopupContainerControl AssingCloisngFD
        {
            set { popupceFDClosingBal.Properties.PopupControl = value; }
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
        private void popupceCashClosingBal_Click(object sender, EventArgs e)
        {
            AssingClosingCash = ucTrans.PopupContainerControl;
            ucTrans.gcData.DataSource = null;
            resultArgs = FetchTransClosingBalance((int)FixedLedgerGroup.Cash);
            //ucTrans.LedgerName = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.TRANSACTION_CASH_LEDGER);
            ucTrans.LedgerName = "Cash Ledger";
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
            {
                ucTrans.gcData.DataSource = resultArgs.DataSource.Table;
                ucTrans.gcData.RefreshDataSource();
            }
        }

        private void popupceBankClosingBal_Click(object sender, EventArgs e)
        {
            AssingClosingBank = ucTrans.PopupContainerControl;
            ucTrans.gcData.DataSource = null;
            resultArgs = FetchTransClosingBalance((int)FixedLedgerGroup.BankAccounts);
           // ucTrans.LedgerName = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.TRANSACTION_BANK_LEDGER); ;
            ucTrans.LedgerName = "Bank Ledger";
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
            {
                ucTrans.gcData.DataSource = resultArgs.DataSource.Table;
                ucTrans.gcData.RefreshDataSource();
            }
        }

        private void popupceFDClosingBal_Click(object sender, EventArgs e)
        {
            AssingCloisngFD = ucTrans.PopupContainerControl;
            ucTrans.gcData.DataSource = null;
            resultArgs = FetchTransFDClosingBalance((int)FixedLedgerGroup.FixedDeposit, FDTypes.IN);
           // ucTrans.LedgerName = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.TRANSACTION_FD_LEDGER);
            ucTrans.LedgerName = "FD Ledger";
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
            {
                DataTable dtFDAccountList = resultArgs.DataSource.Table;//added by sugan--to remove zero balance FD Accounts
                DataView dvFDAccountList = dtFDAccountList.AsDataView();//added by sugan
                dvFDAccountList.RowFilter = "AMOUNT<>0";
                ucTrans.gcData.DataSource = dvFDAccountList.ToTable();
                ucTrans.ShowBaseLedgerGroupRow = true;
                dvFDAccountList.RowFilter = "";
                //ucTrans.gcData.DataSource = resultArgs.DataSource.Table;
                ucTrans.gcData.RefreshDataSource();
            }
        }
        #endregion

        #region Methods
        private ResultArgs FetchTransClosingBalance(int GroupId)
        {
            using (VoucherTransactionSystem voucherTransSystem = new VoucherTransactionSystem())
            {
                voucherTransSystem.ProjectId = ProjectId;
                voucherTransSystem.GroupId = GroupId;
                // voucherTransSystem.BalanceDate = deDateTo.DateTime;
                voucherTransSystem.BalanceDate = UtilityMemeber.DateSet.ToDate(ClosingDateTo, false);
                voucherTransSystem.BankClosedDate = BankClosedDate;
                resultArgs = voucherTransSystem.FetchTransClosingBalance();

                //On 11/10/2024, To Skip default ledgers for multi currency or other than country
                using (MappingSystem mappingsystem = new MappingSystem())
                {
                    resultArgs = mappingsystem.EnforceSkipDefaultLedgers(resultArgs, "ID");
                }
            }
            return resultArgs;
        }

        // FD is to be verified
        private ResultArgs FetchTransFDClosingBalance(int GroupId, FDTypes TransType)
        {
            using (VoucherTransactionSystem voucherTransSystem = new VoucherTransactionSystem())
            {
                voucherTransSystem.ProjectId = ProjectId;
                voucherTransSystem.GroupId = GroupId;
                if (TransType == FDTypes.OP)
                {
                    // voucherTransSystem.BalanceDate = deDateFrom.DateTime.AddDays(-1);
                    voucherTransSystem.BalanceDate = UtilityMemeber.DateSet.ToDate(ClosingDateFrom, false).AddDays(-1);
                }
                else
                {
                    // voucherTransSystem.BalanceDate = deDateTo.DateTime;
                    voucherTransSystem.BalanceDate = UtilityMember.DateSet.ToDate(ClosingDateTo, false);
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

        public void GetClosingBalance()
        {
            using (BalanceSystem balanceSystem = new BalanceSystem())
            {
                balanceSystem.BankClosedDate = BankClosedDate;
                BalanceProperty bankBalanceProperty = balanceSystem.GetBankBalance(ProjectId, ClosingDateTo, BalanceSystem.BalanceType.ClosingBalance);
                lblBankClosingCurrency.Text = balanceSystem.Currency;
                lblBankClosingBal.Text = this.UtilityMember.NumberSet.ToNumber(bankBalanceProperty.Amount) + " " + bankBalanceProperty.TransMode;
                BankTransMode = bankBalanceProperty.TransMode;
                BalanceProperty cashBalanceProperty = balanceSystem.GetCashBalance(ProjectId, ClosingDateTo, BalanceSystem.BalanceType.ClosingBalance);
                lblCashClosinngCurrency.Text = balanceSystem.Currency;
                lblCashClosingBal.Text = this.UtilityMember.NumberSet.ToNumber(cashBalanceProperty.Amount) + " " + cashBalanceProperty.TransMode;
                CashTransMode = cashBalanceProperty.TransMode;
                BalanceProperty FDBalanceProperty = balanceSystem.GetFDBalance(ProjectId, ClosingDateTo, BalanceSystem.BalanceType.ClosingBalance);
                lblFDClosingCurrency.Text = balanceSystem.Currency;
                lblFDClosingBal.Text = this.UtilityMember.NumberSet.ToNumber(FDBalanceProperty.Amount) + " " + FDBalanceProperty.TransMode;
                BalanceProperty ledgerBalanceProperty = balanceSystem.GetBalance(ProjectId, 0, ClosingDateTo, BalanceSystem.BalanceType.ClosingBalance);
            }

            //On 03/10/2024, To fix currency symbol width
            lblCashClosinngCurrency.Width = 15;
            if (lblCashClosinngCurrency.Text.Trim().Length > 1) lblCashClosinngCurrency.Width = 25;
            lblBankClosingCurrency.Width = 15;
            if (lblBankClosingCurrency.Text.Trim().Length > 1) lblBankClosingCurrency.Width = 25;
            lblFDClosingCurrency.Width = 15;
            if (lblFDClosingCurrency.Text.Trim().Length > 1) lblFDClosingCurrency.Width = 25;
        }

        public void ShowClosingBalNegative()
        {
            //To show negative for closing balance
            if (CashTransMode == TransactionMode.CR.ToString())
            {
                lblCashClosingBal.AppearanceItemCaption.ForeColor = Color.Red;
            }
            else
            {
                lblCashClosingBal.AppearanceItemCaption.ForeColor = Color.Blue;
            }
            if (BankTransMode == TransactionMode.CR.ToString())
            {
                lblBankClosingBal.AppearanceItemCaption.ForeColor = Color.Red;
            }
            else
            {
                lblBankClosingBal.AppearanceItemCaption.ForeColor = Color.Blue;
            }
        }
        #endregion

        private void ucTransviewClosingBalance_Load(object sender, EventArgs e)
        {
            //this.UtilityMember.GridSet.AttachGridContextMenu(ucTrans.gcData);
            
        }
    }
}
