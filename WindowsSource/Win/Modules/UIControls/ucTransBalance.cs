using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ACPP.Modules.Transaction;
using Bosco.Model.Business;
using Bosco.Model.Transaction;
using Bosco.Utility;
using Bosco.Utility.ConfigSetting;

namespace ACPP.Modules.UIControls
{
    public partial class ucTransBalance : DevExpress.XtraEditors.XtraUserControl
    {
        #region VariableDeclaration
        CommonMember utilityMember = null;
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public ucTransBalance()
        {
            InitializeComponent();

            colCurrency.Visible =  (SettingProperty.Current.AllowMultiCurrency == 1);
        }
        #endregion

        #region Property
        public PopupContainerControl PopupContainerControl
        {
            get { return popupContainerControl1; }
            set { popupContainerControl1 = value; }
        }
        public DevExpress.XtraGrid.GridControl gcData
        {
            get { return gcTransBalance; }
            set
            {
                gcTransBalance = value;
                
            }
        }

        public bool ShowBaseLedgerGroupRow
        {
            set
            {
                colBaseLedgerName.Visible = value;
                if (colBaseLedgerName.Visible)
                {
                    if (gcTransBalance.DataSource != null)
                    {
                        DataTable dtFDAccountList = gcTransBalance.DataSource as DataTable;
                        if (dtFDAccountList != null)
                        {
                            string sumBalance = "AMOUNT * IIF(TRANSMODE='CR', -1, 1)";
                            dtFDAccountList.Columns.Add("BALANCE", typeof(System.Double), sumBalance);
                            gcTransBalance.DataSource = null;
                            gcTransBalance.DataSource = dtFDAccountList;
                        }

                        gvTransBalance.GroupFormat = "{1}  :  {2}";
                        gvTransBalance.OptionsBehavior.AutoExpandAllGroups = true;
                    }
                }
                else
                {
                    colBaseLedgerName.Visible = false;
                    gvTransBalance.GroupFormat = "";
                }
            }
        }
        

        public string LedgerName
        {
            get { return colLedgerName.Caption; }
            set { colLedgerName.Caption = value; }
        }

        private int projectId;
        public int ProjectId
        {
            set { projectId = value; }
        }

        private DateTime deRecentDate;
        public DateTime dteRecentDate
        {
            get { return deRecentDate; }
            set { deRecentDate = value; }
        }


        public FixedLedgerGroup fixedLedgerGroup { get; set; }

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
        #endregion

        #region Events
        #endregion

        #region Method
        public ResultArgs FetchTransFDClosingBalance(int GroupId, FixedLedgerGroup TransType)
        {
            using (VoucherTransactionSystem voucherTransSystem = new VoucherTransactionSystem())
            {
                voucherTransSystem.ProjectId = projectId;
                voucherTransSystem.GroupId = GroupId;
                voucherTransSystem.BalanceDate = dteRecentDate;
                colBaseLedgerName.Visible = false;
                gvTransBalance.GroupFormat = "";
                switch (TransType)
                {
                    case FixedLedgerGroup.Cash:
                        {
                            LedgerName = "Cash Ledger";
                            resultArgs = voucherTransSystem.FetchTransClosingBalance();
                            break;
                        }
                    case FixedLedgerGroup.BankAccounts:
                        {
                            LedgerName = "Bank Ledger";
                            resultArgs = voucherTransSystem.FetchTransClosingBalance();
                            break;
                        }
                    case FixedLedgerGroup.FixedDeposit:
                        {
                            LedgerName = "FD Ledger";
                            resultArgs = voucherTransSystem.FetchTransFDClosingBalance();
                            voucherTransSystem.GroupId = (int)FixedLedgerGroup.FixedDeposit;
                                                        
                            break;
                        }
                }
                
                gcTransBalance.DataSource = null;
                gcTransBalance.DataSource = resultArgs.DataSource.Table;
                gcTransBalance.RefreshDataSource();
            }
            return resultArgs;
        }

      
        #endregion
    }
}
