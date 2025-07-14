using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ACPP.Modules.Transaction;
using Bosco.Model.Business;
using Bosco.Utility;
using Bosco.Model.Transaction;
using Bosco.Model.UIModel;

namespace ACPP.Modules.UIControls
{
    public partial class UcUsedCodesIcon : UserControl
    {
        #region Declaration
        UcUsedCodesList UcCodelist = new UcUsedCodesList();
        ResultArgs resultArgs = new ResultArgs();
        ResultArgs ExistCode = new ResultArgs();
        public event EventHandler Iconclicked;
        //public event EventHandler TxtCodeLeave;
        #endregion

        #region  Properties
        //MapForm formtype;
        //public MapForm FormType
        //{
        //    set { formtype = value; }
        //    get { return formtype; }
        //}
        public DevExpress.XtraEditors.PopupContainerControl AssignCodelist
        {
            set { pceUsedCodesIcon.Properties.PopupControl = value; }
        }

        public string ClearLedgerName
        {
            set { lblLedgername.Text = value; }
        }
        private string Availcode = string.Empty;
        public string ExistUsedCode
        {
            set { Availcode = value; }
            get { return Availcode; }
        }
        #endregion

        #region Constructor
        public UcUsedCodesIcon()
        {
            InitializeComponent();
        }
        #endregion

        #region Main Methods
        public void FetchCodes(MapForm Formtype)
        {
            AssignCodelist = UcCodelist.PopupContainerControl;
            UcCodelist.gcCodelist.DataSource = null;
            switch (Formtype)
            {
                case MapForm.Project:
                    resultArgs = FetchProjectUsedCodes();
                    break;
                case MapForm.Ledger:
                    resultArgs = FetchLedgerUsedCodes();
                    break;
                case MapForm.BankAccount:
                    resultArgs = FetchBankAccountCodes();
                    break;
                case MapForm.CostCentre:
                    resultArgs = FetchCostcentrecodes();
                    break;
                case MapForm.Bank:
                    resultArgs = FetchBankcodes();
                    break;
                case MapForm.LedgerGroup:
                    resultArgs = FetchLedgerGroupCodes();
                    break;

            }
            //ucTrans.LedgerName = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.TRANSACTION_CASH_LEDGER);
            UcCodelist.UsedCode = "Used Codes";
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
            {
                UcCodelist.gcCodelist.DataSource = resultArgs.DataSource.Table;
                UcCodelist.gcCodelist.RefreshDataSource();
            }
        }

        public void FetchExistCodes(MapForm Formtype,string code)
        {
            switch (Formtype)
            {
                case MapForm.Project:
                    ProjectSystem projectSyetm = new ProjectSystem();
                    ExistCode = projectSyetm.FetchProjectByProjectCode(code);
                    break;
                case MapForm.Ledger:
                    LedgerSystem ledgerSystem = new LedgerSystem();
                    ExistCode = ledgerSystem.FetchLedgersByLedgerCode(code);
                    break;
                case MapForm.BankAccount:
                    BankAccountSystem Bankaccountsystem = new BankAccountSystem();
                    ExistCode = Bankaccountsystem.FetchBankaccountByAccountcode(code);
                    break;
                case MapForm.CostCentre:
                    CostCentreSystem Costcentresystem = new CostCentreSystem();
                    ExistCode = Costcentresystem.FetchCostcentreBycostcentrecode(code);
                    break;
                case MapForm.Bank:
                    BankSystem Banksystem = new BankSystem();
                    ExistCode = Banksystem.FecthBankByExistingCode(code);
                    break;
                case MapForm.LedgerGroup:
                    LedgerGroupSystem Ledgergroupsystem = new LedgerGroupSystem();
                    ExistCode = Ledgergroupsystem.FecthLedgerGroupByExistingCode(code);
                    break;
            }
            
            if (ExistCode.DataSource.Table != null && ExistCode.DataSource.Table.Rows.Count != 0)
            {
                lblLedgername.Text = ExistCode.DataSource.Table.Rows[0]["EXIST_CODE"].ToString()+" is used";
            }
            else
            {
                lblLedgername.Text = string.Empty;
            }

        }
        #endregion

        #region Form Methods
        private ResultArgs FetchProjectUsedCodes()
        {
            using (ProjectSystem projectSyetm = new ProjectSystem())
            {
                resultArgs = projectSyetm.FetchProjectCodes();
            }
            return resultArgs;
        }
        private ResultArgs FetchLedgerUsedCodes()
        {
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                resultArgs = ledgerSystem.FetchLedgerCodes();
            }
            return resultArgs;
        }
        private ResultArgs FetchBankAccountCodes()
        {
            using (BankAccountSystem Bankaccountsystem = new BankAccountSystem())
            {
                resultArgs = Bankaccountsystem.FetchBankAccountCodes();
            }
            return resultArgs;
        }
        private ResultArgs FetchCostcentrecodes()
        {
            using (CostCentreSystem Costcentresystem = new CostCentreSystem())
            {
                resultArgs = Costcentresystem.FetchCostCentreCodes();
            }
            return resultArgs;
        }
        private ResultArgs FetchBankcodes()
        {
            using (BankSystem Banksystem = new BankSystem())
            {
                resultArgs = Banksystem.FetchBankCodes();
            }
            return resultArgs;
        }
        private ResultArgs FetchLedgerGroupCodes()
        {
            using (LedgerGroupSystem Ledgergroupsystem = new LedgerGroupSystem())
            {
                resultArgs = Ledgergroupsystem.FecthLedgerGroupCodes();
            }
            return resultArgs;
        }

        #endregion

        #region Events
        private void pceUsedCodesIcon_Click(object sender, EventArgs e)
        {
            if (Iconclicked != null)
            {
                Iconclicked(this, e);
            }

        }
        #endregion

    }
}
