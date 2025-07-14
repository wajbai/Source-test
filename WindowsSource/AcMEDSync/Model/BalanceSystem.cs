using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;


namespace AcMEDSync.Model
{
    public class BalanceProperty
    {
        public Int32 GroupId { get; set; }
        public double Amount { get; set; }
        public double AmountFC { get; set; }
        public string TransMode { get; set; }
        public string TransFCMode { get; set; }
        public string CurrencySymbol { get; set; }
        public ResultArgs Result { get; set; }
    }

    public class BalanceSystem : DsyncSystemBase
    {
        public event EventHandler RefreshBalanceSetMaxValue;
        public event EventHandler RefreshBalanceUpdateProgressBar;
        public int RefreshProgressBarMaxCount { get; set; }
        public string VoucherDate { get; set; }
        public int ProjectId { get; set; }
        public int LedgerId { get; set; }

        public enum BalanceType
        {
            OpeningBalance,
            ClosingBalance,
            CurrentBalance
        }

        public enum LiquidBalanceGroup
        {
            None = 0,
            BankBalance = 12,
            CashBalance = 13,
            FDBalance = 14
        }

        /// <summary>
        /// On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
        /// 
        /// This date is apart from balance date. If we take reports for given period (DATE_FROM and DATE_TO)
        /// When we show closing balance for DATE_TO, we have to check bank ledger closed date for DATE_FROM
        /// </summary>
        public string BankClosedDate { get; set; }

        #region public Execute Methods

        public ResultArgs UpdateOpBalance(string opBalDate, int projectId, int ledgerId, double amountCurrent, 
            string transModeCurrent, TransactionAction transAction)
        {
            return UpdateOpBalance(opBalDate, 0, projectId, ledgerId, amountCurrent, transModeCurrent, transAction);
        }

        public ResultArgs UpdateOpBalance(string opBalDate, int branchId, int projectId, int ledgerId, double amountCurrent, string transModeCurrent, TransactionAction transAction)
        {
            ResultArgs result = null;
            DateTime dateOpBal = DateTime.Parse(opBalDate).AddDays(-1);
            Int32 groupid = 0;
            bool isCashBank = false;
            double CashBankBaseExchangerate = 1;
            bool hasOpBal = false;
            string ledgerOpBalDate = dateOpBal.ToShortDateString();
            double amount = 0;
            double amountfc = 0;
            double amountCurrentfc = 0;

            string transMode = "";
            string transFlag = "OP";

            //On 29/08/2024, if multi currency is enabled, update cash/bank opening balance in forien currency too
            if (this.AllowMultiCurrency == 1)
            {
                ResultArgs resulexchagnerate = FetchCashBankBaseCurrencyExchangeRate(opBalDate, ledgerId);
                if (resulexchagnerate.Success && resulexchagnerate.DataSource.Table != null && resulexchagnerate.DataSource.Table.Rows.Count > 0)
                {
                    DataTable dtBaseExchangeRate = resulexchagnerate.DataSource.Table;
                    if (dtBaseExchangeRate.Rows.Count > 0)
                    {
                        groupid = NumberSet.ToInteger(dtBaseExchangeRate.Rows[0][this.AppSchema.Ledger.GROUP_IDColumn.ColumnName].ToString());
                        if (groupid == (Int32)FixedLedgerGroup.Cash || groupid == (Int32)FixedLedgerGroup.BankAccounts || groupid == (Int32)FixedLedgerGroup.FixedDeposit)
                        {
                            CashBankBaseExchangerate = NumberSet.ToDouble(dtBaseExchangeRate.Rows[0][this.AppSchema.Ledger.OP_EXCHANGE_RATEColumn.ColumnName].ToString());
                            if (CashBankBaseExchangerate==0)
                                CashBankBaseExchangerate = NumberSet.ToDouble(dtBaseExchangeRate.Rows[0][this.AppSchema.Country.EXCHANGE_RATEColumn.ColumnName].ToString());
                            isCashBank = true;
                        }
                    }
                }
            }

            result = GetLedgerOpBalance(branchId, projectId, ledgerId);

            if (result.Success)
            {
                DataTable dtLedgerOpBal = result.DataSource.Table;

                if (dtLedgerOpBal != null && dtLedgerOpBal.Rows.Count > 0)
                {
                    hasOpBal = true;
                    DataRow drLedgerOpBal = dtLedgerOpBal.Rows[0];
                    ledgerOpBalDate = drLedgerOpBal[this.AppSchema.LedgerBalance.BALANCE_DATEColumn.ColumnName].ToString();
                    amount = this.NumberSet.ToDouble(drLedgerOpBal[this.AppSchema.LedgerBalance.AMOUNTColumn.ColumnName].ToString());
                    if (this.AllowMultiCurrency == 1 && isCashBank)
                    {
                        amountfc = this.NumberSet.ToDouble(drLedgerOpBal[this.AppSchema.LedgerBalance.AMOUNT_FCColumn.ColumnName].ToString());
                    }
                    transMode = drLedgerOpBal[this.AppSchema.LedgerBalance.TRANS_MODEColumn.ColumnName].ToString();
                }
            }

            if (isCashBank)
            {
                amountCurrentfc = amountCurrent;
                amountCurrent = (amountCurrent * CashBankBaseExchangerate);
            }
            else
            {
                amountfc = 0;
                amountCurrentfc = 0;
            }

            //Reset Balance
            if (hasOpBal)
            {
                amount = -amount;
                amountfc = -amountfc;
                result = UpdateBalance(ledgerOpBalDate, branchId, projectId, ledgerId, amount , amountfc, transMode, transFlag);
            }

            if (transAction != TransactionAction.Cancel)
            {
                result = UpdateBalance(ledgerOpBalDate, branchId, projectId, ledgerId, amountCurrent, amountCurrentfc, transModeCurrent, transFlag);
            }

            if (result.Success && transAction == TransactionAction.Cancel)
            {
                result = DeleteBalance(branchId, projectId, ledgerId);
            }

            return result;
        }

        public ResultArgs UpdateBulkTransBalance()
        {
            ResultArgs result = null;
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                result = UpdateBulkTransBalance(0, true);
                dataManager.EndTransaction();
            }
            return result;
        }

        public ResultArgs UpdateBulkTransBalance(int branchId, bool allLedgers)
        {
            int voucherId = 0;
            int locationid = 0;
            ResultArgs result = null;

            object cmdsql = SQLCommand.TransBalance.BulkBalanceRefresh;
            if (allLedgers && LedgerId==0)
                cmdsql = SQLCommand.TransBalance.AllProjectBalanceRefresh;
            else if (LedgerId>0)
                cmdsql = SQLCommand.TransBalance.ProjectLedgerBalanceRefreshByLedger;
            else
                cmdsql = SQLCommand.TransBalance.BulkBalanceRefresh;

            using (DataManager dataManager = new DataManager(cmdsql, DataBaseTypeName, SQLAdapterTypeName)) //DataBaseType.HeadOffice, SQLAdapterType.HOSQL
            {
                result = DeleteTransBalance(dataManager, branchId);

                if (result != null && result.Success)
                {
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.BRANCH_IDColumn, branchId);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_DATEColumn, VoucherDate);

                    if (ProjectId != 0) //Project Id =0 then it means that all projects
                    {
                        dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    }

                    if (LedgerId != 0) //Ledger Id = 0 then it means that all Ledgers
                    {
                        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                    }

                    result = dataManager.FetchData(DataSource.DataTable);

                    DataTable dtTrans = result.DataSource.Table;

                    if (dtTrans != null)
                    {
                        if (RefreshBalanceSetMaxValue != null)
                        {
                            RefreshProgressBarMaxCount = dtTrans.Rows.Count;
                            RefreshBalanceSetMaxValue(this, new EventArgs());
                        }

                        foreach (DataRow drTrans in dtTrans.Rows)
                        {
                            if (RefreshBalanceUpdateProgressBar != null)
                            {
                                RefreshBalanceUpdateProgressBar(this, new EventArgs());
                            }

                            voucherId = this.NumberSet.ToInteger(drTrans[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString());
                            //On 12/11/2020, To have proper refresh for portal too
                            //result = UpdateTransBalance(branchId, voucherId, TransactionAction.New);
                            if (branchId > 0)
                            {
                                locationid = this.NumberSet.ToInteger(drTrans["LOCATION_ID"].ToString());
                                result = UpdateTransBalance(branchId, locationid, voucherId, TransactionAction.New);
                            }
                            else
                            {
                                result = UpdateTransBalance(branchId, voucherId, TransactionAction.New);
                            }

                            if (!result.Success) { break; }
                        }
                    }
                }
            }
            return result;
        }

        public ResultArgs UpdateTransBalance(int voucherId, TransactionAction transAction)
        {
            return UpdateTransBalance(0, 0, voucherId, transAction);
        }

        public ResultArgs UpdateTransBalance(int branchId, int voucherId, TransactionAction transAction)
        {
            return UpdateTransBalance(branchId, 0, voucherId, transAction);
        }

        public ResultArgs UpdateTransBalance(int branchId, int locationId, int voucherId, TransactionAction transAction)
        {
            ResultArgs result = GetTransaction(branchId, voucherId, locationId);

            if (result.Success)
            {
                string transDate = "";
                int projectId = 0;
                int ledgerId = 0;
                int groupid = 0;
                double amount = 0;
                double amountfc = 0; //For cash and Bank Ledgers currency amount
                double exchangerate = 1;
                double ledgeractualamount = 1;
                string vouchertype = FinacialTransType.RC.ToString();

                string transMode = "";
                string transFlag = "TR";

                DataTable dtTrans = result.DataSource.Table;

                if (dtTrans != null)
                {
                    foreach (DataRow drTrans in dtTrans.Rows)
                    {
                        transDate = drTrans[this.AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName].ToString();
                        projectId = this.NumberSet.ToInteger(drTrans[this.AppSchema.VoucherMaster.PROJECT_IDColumn.ColumnName].ToString());
                        ledgerId = this.NumberSet.ToInteger(drTrans[this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
                        groupid = this.NumberSet.ToInteger(drTrans[this.AppSchema.Ledger.GROUP_IDColumn.ColumnName].ToString());
                        amount = this.NumberSet.ToDouble(drTrans[this.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString());
                        exchangerate = this.NumberSet.ToDouble(drTrans[this.AppSchema.VoucherMaster.EXCHANGE_RATEColumn.ColumnName].ToString());
                        ledgeractualamount = this.NumberSet.ToDouble(drTrans[this.AppSchema.VoucherMaster.ACTUAL_AMOUNTColumn.ColumnName].ToString());
                        vouchertype = drTrans[this.AppSchema.VoucherMaster.VOUCHER_TYPEColumn.ColumnName].ToString();
                        transMode = drTrans[this.AppSchema.VoucherTransaction.TRANS_MODEColumn.ColumnName].ToString();

                        if (transAction == TransactionAction.EditBeforeSave || transAction == TransactionAction.Cancel)
                        {
                            amount = -amount;
                            ledgeractualamount = -ledgeractualamount;
                        }

                        //On 29/08/2024, If multi currency amount based on exchange rate -------------------------------
                        amountfc = 0;
                        if (this.AllowMultiCurrency == 1 )
                        {
                            if (groupid == (Int32)FixedLedgerGroup.Cash || groupid == (Int32)FixedLedgerGroup.BankAccounts || groupid == (Int32)FixedLedgerGroup.FixedDeposit)
                            {
                                amountfc = amount; //For cash and Bank Ledgers currency amount
                                       
                                if (vouchertype == FinacialTransType.CN.ToString())
                                {
                                    amount = ledgeractualamount;
                                }
                                else
                                {
                                    amount = amount * (exchangerate == 0 ? 1 : exchangerate);
                                }
                            }
                            else
                            {
                                amount = amount * (exchangerate == 0 ? 1 : exchangerate);
                            }
                        }
                        //-----------------------------------------------------------------------------------------------

                        result = UpdateBalance(transDate, IsClientBranch ? 0 : branchId, projectId, ledgerId, amount, amountfc, transMode, transFlag);
                        if (!result.Success) { break; }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 08/05/2020, To reset (Make it as 0.00) ledger opening balance for given nature or given main group id
        /// 
        /// 1. It will affect all the ledgers opening balncae under given nature
        /// 2. It will affect all the ledgers opening balncae under given main group 
        /// </summary>
        /// <param name="NatureId"></param>
        /// <param name="MainGroupId"></param>
        /// <returns></returns>
        public ResultArgs ResetLedgerOpeningBalance(Int32 ProId, Int32 NatureId, Int32 MainGroupId, bool IncludeCashLedgers, bool IncludeBankLedgers, bool IncludeFDLedgers)
        {
            ResultArgs result = new ResultArgs();
            string skipCashBankFDGroups = "";

            if (MainGroupId != (Int32)FixedLedgerGroup.Cash && MainGroupId != (Int32)FixedLedgerGroup.BankAccounts)
            {
                if (!IncludeCashLedgers)
                {
                    skipCashBankFDGroups += (String.IsNullOrEmpty(skipCashBankFDGroups) ? "" : ",") + (int)FixedLedgerGroup.Cash;
                }

                if (!IncludeBankLedgers)
                {
                    skipCashBankFDGroups += (String.IsNullOrEmpty(skipCashBankFDGroups) ? "" : ",") + (int)FixedLedgerGroup.BankAccounts;
                }

                if (!IncludeFDLedgers)
                {
                    skipCashBankFDGroups += (String.IsNullOrEmpty(skipCashBankFDGroups) ? "" : ",") + (int)FixedLedgerGroup.FixedDeposit;
                }
            }

            ProjectId = ProId;
            VoucherDate = DateSet.ToDate(BookBeginFrom, false).ToShortDateString();
            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.ResetLedgerOpeningBalance, DataBaseTypeName, SQLAdapterTypeName)) 
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn.ColumnName, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.NATURE_IDColumn.ColumnName, NatureId);
                if (MainGroupId > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.LedgerGroup.MAIN_GROUP_IDColumn.ColumnName, MainGroupId);
                }
                if (!string.IsNullOrEmpty(skipCashBankFDGroups))
                {
                    dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn.ColumnName, skipCashBankFDGroups);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                result = dataManager.UpdateData();   
            }

            if (result.Success && result.RowsAffected > 0)
            {
                UpdateBulkTransBalance();
            }
            return result;
        }

        /// <summary>
        /// On 10/09/2024, To update Cash, Bank Op Balacne based on Exchange Rate
        /// </summary>
        /// <returns></returns>
        public ResultArgs UpdateCashBankOpBalanceByExchangeRate(string opdate)
        {
            ResultArgs result = new ResultArgs();
            try
            {
                result = GetLedgerOpBalance(0);
                if (result.Success && result.DataSource.Table != null)
                {
                    DataTable dtLedgerBalance = result.DataSource.Table;
                    dtLedgerBalance.DefaultView.RowFilter = AppSchema.LedgerBalance.GROUP_IDColumn.ColumnName + " IN (12, 13, 14)" ;
                    dtLedgerBalance = dtLedgerBalance.DefaultView.ToTable();

                    foreach (DataRow dr in dtLedgerBalance.Rows)
                    {
                        Int32 Pid = NumberSet.ToInteger(dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString());
                        Int32 Lid = NumberSet.ToInteger(dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString());
                        double amount = NumberSet.ToDouble(dr[this.AppSchema.LedgerBalance.AMOUNTColumn.ColumnName].ToString());
                        double amountfc = NumberSet.ToDouble(dr[this.AppSchema.LedgerBalance.AMOUNT_FCColumn.ColumnName].ToString());
                        string transmode = dr[this.AppSchema.LedgerBalance.TRANS_MODEColumn.ColumnName].ToString();
                        
                        if (Pid > 0 && Lid > 0)
                        {
                            result = UpdateOpBalance(opdate, Pid, Lid, amountfc, transmode, TransactionAction.EditAfterSave);
                            
                            if (!result.Success)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                result.Message = err.Message;
            }

            return result;
        }

        public ResultArgs DeleteBalance(int projectId, int ledgerId)
        {
            return DeleteBalance(0, projectId, ledgerId);
        }

        public ResultArgs DeleteBalance(int branchId, int projectId, int ledgerId)
        {
            ResultArgs result = new ResultArgs();
            bool hasBalance = HasBalance(branchId, projectId, ledgerId);

            if (!hasBalance)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.DeleteBalance, DataBaseTypeName, SQLAdapterTypeName)) //DataBaseType.HeadOffice, SQLAdapterType.HOSQL
                {
                    dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BRANCH_IDColumn, branchId);
                    dataManager.Parameters.Add(this.AppSchema.LedgerBalance.PROJECT_IDColumn, projectId);
                    dataManager.Parameters.Add(this.AppSchema.LedgerBalance.LEDGER_IDColumn, ledgerId);
                    result = dataManager.UpdateData();
                }
            }
            else
            {
                result.Message = "Cannot Delete the Ledger Balance. Because it contains balance";
            }

            return result;
        }

        private ResultArgs DeleteTransBalance(DataManager dataManagers, int branchId)
        {
            ResultArgs resultArg = null;
            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.DeleteTransBalance, DataBaseTypeName, SQLAdapterTypeName)) //DataBaseType.HeadOffice, SQLAdapterType.HOSQL
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.BRANCH_IDColumn, branchId);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_DATEColumn, VoucherDate);

                if (ProjectId != 0) //Project Id =0 then it means that all projects
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                }

                if (LedgerId != 0) //Ledger Id=0 then it means that all Ledgers
                {
                    dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                }
                
                dataManager.Database = dataManagers.Database;
                resultArg = dataManager.UpdateData();
            }

            return resultArg;
        }

        #endregion

        #region Private Execute Methods

        private ResultArgs UpdateBalance(string transDate, int branchId, int projectId, int ledgerId,
            double amount, double amountfc, string transMode, string transFlag)
        {
            ResultArgs result = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.UpdateBalance, DataBaseTypeName, SQLAdapterTypeName)) //DataBaseType.HeadOffice, SQLAdapterType.HOSQL
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BALANCE_DATEColumn, transDate);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BRANCH_IDColumn, branchId);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.PROJECT_IDColumn, projectId);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.LEDGER_IDColumn, ledgerId);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.AMOUNTColumn, amount);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.AMOUNT_FCColumn, amountfc);//For Cash and bank alone
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.TRANS_MODEColumn, transMode.ToUpper());
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.TRANS_FLAGColumn, transFlag);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = false;
                result = dataManager.UpdateData();
            }

            return result;
        }

        #endregion

        #region private Get Methods

        private ResultArgs GetTransaction(int branchId, int voucherId, int locationId)
        {
            ResultArgs result = null;

            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchTransaction, DataBaseTypeName, SQLAdapterTypeName)) //DataBaseType.HeadOffice, SQLAdapterType.HOSQL
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.BRANCH_IDColumn, branchId);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, voucherId);
                dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.LOCATION_IDColumn, locationId);
                result = dataManager.FetchData(DataSource.DataTable);
            }

            return result;
        }

        private ResultArgs GetLedgerOpBalance(int branchId, int projectId, int ledgerId)
        {
            ResultArgs result = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchOpBalance, DataBaseTypeName, SQLAdapterTypeName)) //DataBaseType.HeadOffice, SQLAdapterType.HOSQL
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BRANCH_IDColumn, branchId);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.PROJECT_IDColumn, projectId);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.LEDGER_IDColumn, ledgerId);
                result = dataManager.FetchData(DataSource.DataTable);
            }

            return result;
        }

        public ResultArgs GetLedgerOpBalance(int projectId, int ledgerId)
        {
            ResultArgs result = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchOpBalance, DataBaseTypeName, SQLAdapterTypeName))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BRANCH_IDColumn, "0");
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.PROJECT_IDColumn, projectId);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.LEDGER_IDColumn, ledgerId);
                result = dataManager.FetchData(DataSource.DataTable);
            }

            return result;
        }

        public ResultArgs FetchTotalLedgerOpBalance()
        {
            ResultArgs result = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchTotalLedgerOpBalance, DataBaseTypeName, SQLAdapterTypeName))
            {
                result = dataManager.FetchData(DataSource.DataTable);
            }

            return result;
        }


        private ResultArgs GetLedgerOpBalance(int branchId)
        {
            ResultArgs result = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchOpBalance, DataBaseTypeName, SQLAdapterTypeName)) //DataBaseType.HeadOffice, SQLAdapterType.HOSQL
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BRANCH_IDColumn, branchId);
                result = dataManager.FetchData(DataSource.DataTable);
            }

            return result;
        }
        
        
        /// <summary>
        /// On 29/08/2024, to get base currency exchange rate for cash and bank
        /// </summary>
        /// <param name="ledgerId"></param>
        /// <returns></returns>
        private ResultArgs FetchCashBankBaseCurrencyExchangeRate(string opBalDate, int ledgerId)
        {
            ResultArgs result = new ResultArgs();
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchCashBankBaseCurrencyExchangeRate, DataBaseTypeName, SQLAdapterTypeName)) //DataBaseType.HeadOffice, SQLAdapterType.HOSQL
                {
                    dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BALANCE_DATEColumn, opBalDate);
                    dataManager.Parameters.Add(this.AppSchema.LedgerBalance.LEDGER_IDColumn, ledgerId);
                    result = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception err)
            {
                result.Message = err.Message;
            }
            return result;
        }
        
        #endregion

        #region Public Get Methods

        //4 Overloading - Ledger
        public BalanceProperty GetBalance(int projectId, int ledgerId, string balanceDate, BalanceType balanceType,  Int32 CurrencyCurrencyId = 0)
        {
            return GetBalance("0", projectId.ToString(), ledgerId, balanceDate, balanceType, CurrencyCurrencyId);
        }

        public BalanceProperty GetBalance(string projectId, int ledgerId, string balanceDate, BalanceType balanceType, Int32 CurrencyCurrencyId = 0)
        {
            return GetBalance("0", projectId, ledgerId, balanceDate, balanceType, CurrencyCurrencyId);
        }

        public BalanceProperty GetBalance(int branchId, int projectId, int ledgerId, string balanceDate, BalanceType balanceType, Int32 CurrencyCurrencyId = 0)
        {
            return GetBalance(branchId.ToString(), projectId.ToString(), ledgerId, balanceDate, balanceType, CurrencyCurrencyId);
        }

        public BalanceProperty GetBalance(string branchId, string projectId, int ledgerId, string balanceDate, BalanceType balanceType, Int32 CurrencyCurrencyId = 0)
        {
            BalanceProperty balanceProperty = GetTransBalance(branchId, projectId, ledgerId.ToString(), (int)LiquidBalanceGroup.None, balanceDate, balanceType, CurrencyCurrencyId);
            return balanceProperty;
        }

        //4 Overloading   Separate the Cash Ledger so By Default & Introduce the one more Overloading 
        public BalanceProperty GetCashBalance(int projectId, string balanceDate, BalanceType balanceType, Int32 CurrencyCurrencyId = 0)
        {
            return GetCashBalance("0", projectId.ToString(), "0", balanceDate, balanceType, CurrencyCurrencyId);
        }

        public BalanceProperty GetCashBalance(string projectId, string balanceDate, BalanceType balanceType, Int32 CurrencyCurrencyId = 0)
        {
            return GetCashBalance("0", projectId, "0", balanceDate, balanceType, CurrencyCurrencyId);
        }

        public BalanceProperty GetCashBalance(int branchId, int projectId, string balanceDate, BalanceType balanceType, Int32 CurrencyCurrencyId = 0)
        {
            return GetCashBalance(branchId.ToString(), projectId.ToString(), "0", balanceDate, balanceType, CurrencyCurrencyId);
        }

        public BalanceProperty GetCashBalance(int branchId, int projectId, string LedgerId, string balanceDate, BalanceType balanceType, Int32 CurrencyCurrencyId = 0)
        {
            return GetCashBalance(branchId.ToString(), projectId.ToString(), LedgerId, balanceDate, balanceType, CurrencyCurrencyId);
        }
        public BalanceProperty GetCashBalance(string branchId, string projectId, string balanceDate, BalanceType balanceType, Int32 CurrencyCurrencyId = 0)
        {
            return GetCashBalance(branchId, projectId, "0", balanceDate, balanceType, CurrencyCurrencyId);
        }
        public BalanceProperty GetCashBalance(string branchId, string projectId, string LedgerId, string balanceDate, BalanceType balanceType, Int32 CurrencyCurrencyId = 0)
        {
            BalanceProperty balanceProperty = GetTransBalance(branchId, projectId, LedgerId, (int)LiquidBalanceGroup.CashBalance, balanceDate, balanceType, CurrencyCurrencyId);
            return balanceProperty;
        }

        //6 Overloading
        public BalanceProperty GetBankBalance(int projectId, string balanceDate, BalanceType balanceType, Int32 CurrencyCurrencyId = 0)
        {
            return GetBankBalance("0", projectId.ToString(), "0", balanceDate, balanceType, CurrencyCurrencyId);
        }

        public BalanceProperty GetBankBalance(int branchId, int projectId, string balanceDate, BalanceType balanceType,  Int32 CurrencyCurrencyId = 0)
        {
            return GetBankBalance(branchId.ToString(), projectId.ToString(), "0", balanceDate, balanceType, CurrencyCurrencyId);
        }

        public BalanceProperty GetBankBalance(int branchId, int projectId, int ledgerId, string balanceDate, BalanceType balanceType, Int32 CurrencyCurrencyId = 0)
        {
            return GetBankBalance(branchId.ToString(), projectId.ToString(), ledgerId.ToString(), balanceDate, balanceType, CurrencyCurrencyId);
        }

        public BalanceProperty GetBankBalance(string projectId, string balanceDate, BalanceType balanceType, Int32 CurrencyCurrencyId = 0)
        {
            return GetBankBalance("0", projectId, "0", balanceDate, balanceType, CurrencyCurrencyId);
        }

        public BalanceProperty GetBankBalance(string branchId, string projectId, string balanceDate, BalanceType balanceType, Int32 CurrencyCurrencyId = 0)
        {
            return GetBankBalance(branchId, projectId, "0", balanceDate, balanceType, CurrencyCurrencyId);
        }

        public BalanceProperty GetBankBalance(string branchId, string projectId, string ledgerId, string balanceDate, BalanceType balanceType, Int32 CurrencyCurrencyId = 0)
        {
            BalanceProperty balanceProperty = GetTransBalance(branchId, projectId, ledgerId, (int)LiquidBalanceGroup.BankBalance, balanceDate, balanceType, CurrencyCurrencyId);
            return balanceProperty;
        }

        //4 Overloading
        public BalanceProperty GetFDBalance(int projectId, string balanceDate, BalanceType balanceType, Int32 CurrencyCurrencyId = 0)
        {
            return GetFDBalance("0", projectId.ToString(), balanceDate, balanceType, CurrencyCurrencyId);
        }

        public BalanceProperty GetFDBalance(string projectId, string balanceDate, BalanceType balanceType, Int32 CurrencyCurrencyId = 0)
        {
            return GetFDBalance("0", projectId, balanceDate, balanceType, CurrencyCurrencyId);
        }

        public BalanceProperty GetFDBalance(int branchId, int projectId, string balanceDate, BalanceType balanceType, Int32 CurrencyCurrencyId = 0)
        {

            return GetFDBalance(branchId.ToString(), projectId.ToString(), balanceDate, balanceType, CurrencyCurrencyId);
        }

        public BalanceProperty GetFDBalance(string branchId, string projectId, string balanceDate, BalanceType balanceType, Int32 CurrencyCurrencyId = 0)
        {
            BalanceProperty balanceProperty = GetTransBalance(branchId, projectId, "0", (int)LiquidBalanceGroup.FDBalance, balanceDate, balanceType, CurrencyCurrencyId);
            return balanceProperty;
        }

        private BalanceProperty GetTransBalance(string branchId, string projectId, string ledgerId, int groupId, string balanceDate, 
            BalanceType balanceType, Int32 CurrencyCurrencyId = 0)
        {
            ResultArgs result = new ResultArgs();
            BalanceProperty balanceProperty = new BalanceProperty();
            Int32 groupid = 0;
            double amount = 0;
            double amountfc = 0;
            string transMode = "";
            string transFCMode = "";
            string balance_date = balanceDate;
            string currencysymbol = string.Empty;

            /* as on 28/04/201, if all these variables empty or null values
             * if (branchId == "") { branchId = "0"; }
            if (projectId == "") { projectId = "0"; }
            if (ledgerId == "") { ledgerId = "0"; }*/

            if (branchId == "" || string.IsNullOrEmpty(branchId) ) { branchId = "0"; }
            if (projectId == "" || string.IsNullOrEmpty(projectId)) { projectId = "0"; }
            if (ledgerId == "" || string.IsNullOrEmpty(ledgerId)) { ledgerId = "0"; }


            if (balanceType == BalanceType.OpeningBalance)
            {
                DateTime dateBal = DateTime.Parse(balanceDate).AddDays(-1);
                balance_date = dateBal.ToShortDateString();
            }
            else if (balanceType == BalanceType.CurrentBalance)
            {
                balance_date = "";
            }

            using (DataManager dataManager = new DataManager(DataBaseTypeName)) //DataBaseType.HeadOffice
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterTypeName; //SQLAdapterType.HOSQL;
                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.TransBalance.FetchBalance;

                if (groupId > 0)
                {
                    dataManager.DataCommandArgs.SQLCommandId = SQLCommand.TransBalance.FetchGroupSumBalance;
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                if (balance_date != "")
                {
                    dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BALANCE_DATEColumn, balance_date);
                }

                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BRANCH_IDColumn, branchId);

                if (!string.IsNullOrEmpty(projectId) && projectId != "0")
                {
                    dataManager.Parameters.Add(this.AppSchema.LedgerBalance.PROJECT_IDColumn, projectId);
                }

                if (groupId > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.Ledger.GROUP_IDColumn, groupId);
                }

                if (ledgerId != "0")
                {
                    dataManager.Parameters.Add(this.AppSchema.LedgerBalance.LEDGER_IDColumn, ledgerId);
                }

                if (!string.IsNullOrEmpty(BankClosedDate))
                {
                    dataManager.Parameters.Add(this.AppSchema.BankAccount.DATE_CLOSEDColumn, BankClosedDate);
                }

                //On 04/12/2024, To to currency based reports --------------------------------------------------------------------------------------
                if (AllowMultiCurrency == 1 && CurrencyCurrencyId > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CURRENCY_COUNTRY_IDColumn, CurrencyCurrencyId);
                }
                //----------------------------------------------------------------------------------------------------------------------------------

                result = dataManager.FetchData(DataSource.DataTable);
            }

            if (result.Success)
            {
                DataTable dtBalance = result.DataSource.Table;

                if (dtBalance != null && dtBalance.Rows.Count > 0)
                {
                    DataRow drBalance = dtBalance.Rows[0];

                    amount = this.NumberSet.ToDouble(drBalance[AppSchema.LedgerBalance.AMOUNTColumn.ColumnName].ToString());

                    if (this.AllowMultiCurrency == 1)
                    {
                        if (dtBalance.Columns.Contains(AppSchema.LedgerBalance.AMOUNT_FCColumn.ColumnName))
                        {
                            amountfc = this.NumberSet.ToDouble(drBalance[AppSchema.LedgerBalance.AMOUNT_FCColumn.ColumnName].ToString());
                        }

                        if (dtBalance.Columns.Contains(AppSchema.Country.CURRENCY_SYMBOLColumn.ColumnName))
                        {
                            currencysymbol = drBalance[AppSchema.Country.CURRENCY_SYMBOLColumn.ColumnName].ToString();
                        }
                    }

                    if (dtBalance.Columns.Contains(AppSchema.LedgerBalance.GROUP_IDColumn.ColumnName))
                    {
                        groupid = this.NumberSet.ToInteger(drBalance[AppSchema.LedgerBalance.GROUP_IDColumn.ColumnName].ToString());
                    }

                    transMode = drBalance[AppSchema.LedgerBalance.TRANS_MODEColumn.ColumnName].ToString();
                    transFCMode = drBalance[AppSchema.LedgerBalance.TRANS_FC_MODEColumn.ColumnName].ToString();
                }
            }
            
            balanceProperty.GroupId = groupid;
            balanceProperty.Amount = amount;
            balanceProperty.AmountFC = amountfc;
            balanceProperty.TransMode = transMode;
            balanceProperty.TransFCMode = transFCMode;
            balanceProperty.CurrencySymbol = currencysymbol;
            balanceProperty.Result = result;
            return balanceProperty;
        }

        public bool HasBalance(int projectId, int ledgerId)
        {
            return HasBalance(0, projectId, ledgerId);
        }

        public bool HasBalance(int branchId, int projectId, int ledgerId)
        {
            bool hasBalance = false;
            ResultArgs result = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.HasBalance, DataBaseTypeName, SQLAdapterTypeName)) //DataBaseType.HeadOffice, SQLAdapterType.HOSQL
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BRANCH_IDColumn, branchId);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.PROJECT_IDColumn, projectId);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.LEDGER_IDColumn, ledgerId);
                result = dataManager.FetchData(DataSource.DataTable);
            }

            if (result.Success)
            {
                DataTable dtBal = result.DataSource.Table;

                if (dtBal != null && dtBal.Rows.Count > 0)
                {
                    hasBalance = (dtBal.Rows.Count > 0);
                }
            }

            return hasBalance;
        }

        /// <summary>
        /// 19/10/2021, To check Ledger balance exists for given criteria
        /// </summary>
        /// <param name="balanceDate"></param>
        /// <param name="branchId"></param>
        /// <param name="projectId"></param>
        /// <param name="ledgerId"></param>
        /// <param name="balanceType"></param>
        /// <returns></returns>
        public BalanceProperty HasBalance(string balanceDate, int branchId, int projectId, int ledgerId, BalanceType balanceType)
        {
            ResultArgs result = new ResultArgs();
            BalanceProperty balanceProperty = new BalanceProperty();
            double amount = 0;
            string transMode = "";
            double amountfc = 0;
            string transFCMode = "";

            string balance_date = balanceDate;
            if (balanceType == BalanceType.OpeningBalance)
            {
                DateTime dateBal = DateTime.Parse(balanceDate).AddDays(-1);
                balance_date = dateBal.ToShortDateString();
            }
            
            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.HasLedgerBalanceByDate, DataBaseTypeName, SQLAdapterTypeName)) 
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BRANCH_IDColumn, branchId);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BALANCE_DATEColumn, balance_date);
                
                if (ledgerId > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.LedgerBalance.LEDGER_IDColumn, ledgerId);
                }

                if (projectId > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.LedgerBalance.PROJECT_IDColumn, projectId);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                result = dataManager.FetchData(DataSource.DataTable);
            }

            if (result.Success)
            {
                DataTable dtBalance = result.DataSource.Table;

                if (dtBalance != null && dtBalance.Rows.Count > 0)
                {
                    DataRow drBalance = dtBalance.Rows[0];

                    amount = this.NumberSet.ToDouble(drBalance[AppSchema.LedgerBalance.AMOUNTColumn.ColumnName].ToString());
                    transMode = drBalance[AppSchema.LedgerBalance.TRANS_MODEColumn.ColumnName].ToString();

                    if (this.AllowMultiCurrency == 1)
                    {
                        amountfc = this.NumberSet.ToDouble(drBalance[AppSchema.LedgerBalance.AMOUNTColumn.ColumnName].ToString());
                        transFCMode = drBalance[AppSchema.LedgerBalance.TRANS_MODEColumn.ColumnName].ToString();
                    }
                }
            }

            balanceProperty.Amount = amount;
            balanceProperty.TransMode = transMode;
            balanceProperty.AmountFC = amountfc;
            balanceProperty.TransFCMode = transFCMode;
            balanceProperty.Result = result;
            return balanceProperty;

        }
        
        
        /// <summary>
        /// By Aldrin to fetch budget ledger balance from the budget period.  ( chinna - 17.04.2018)
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="ledgerId"></param>
        /// <returns></returns>
        public BalanceProperty GetBudgetLedgereBalance(string projectIds, int ledgerId, DateTime DateFrom, DateTime DateTo)
        {
            ResultArgs result = new ResultArgs();
            BalanceProperty balanceProperty = new BalanceProperty();
            double amount = 0;
            string transMode = "";

            if (projectIds == string.Empty) { projectIds = string.Empty; }
            if (ledgerId == 0) { ledgerId = 0; }

            using (DataManager dataManager = new DataManager(DataBaseTypeName)) //DataBaseType.HeadOffice
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterTypeName; //SQLAdapterType.HOSQL;
                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.TransBalance.FetchBudgetLedgerBalance;
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.PROJECT_IDColumn, projectIds);
                // by alex To fetch the actual amount witin the fy year
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, DateFrom); // YearFrom
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, DateTo); // YearTo
                if (ledgerId != 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.LedgerBalance.LEDGER_IDColumn, ledgerId);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                result = dataManager.FetchData(DataSource.DataTable);
            }
            if (result.Success)
            {
                DataTable dtBalance = result.DataSource.Table;

                if (dtBalance != null && dtBalance.Rows.Count > 0)
                {
                    DataRow drBalance = dtBalance.Rows[0];

                    amount = this.NumberSet.ToDouble(drBalance[AppSchema.LedgerBalance.AMOUNTColumn.ColumnName].ToString());
                    transMode = drBalance[AppSchema.LedgerBalance.TRANS_MODEColumn.ColumnName].ToString();
                }
            }
            balanceProperty.Amount = amount;
            balanceProperty.TransMode = transMode;
            balanceProperty.Result = result;
            return balanceProperty;
        }

        /// <summary>
        /// This is to fetch the LedgerBalance
        /// </summary>
        /// <param name="projectIds"></param>
        /// <param name="ledgerId"></param>
        /// <param name="DateFrom"></param>
        /// <param name="DateTo"></param>
        /// <returns></returns>
        public BalanceProperty GetBudgetLedgereBalanceByTransMode(string projectIds, int ledgerId, DateTime DateFrom, DateTime DateTo, string TransMode)
        {
            ResultArgs result = new ResultArgs();
            BalanceProperty balanceProperty = new BalanceProperty();
            double amount = 0;
            string transMode = "";

            if (projectIds == string.Empty) { projectIds = string.Empty; }
            if (ledgerId == 0) { ledgerId = 0; }

            using (DataManager dataManager = new DataManager(DataBaseTypeName)) //DataBaseType.HeadOffice
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterTypeName; //SQLAdapterType.HOSQL;
                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.TransBalance.FetchBudgetLedgerBalanceByTransMode;
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.PROJECT_IDColumn, projectIds);
                // by alex To fetch the actual amount witin the fy year
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, DateFrom); // YearFrom
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, DateTo); // YearTo
                if (ledgerId != 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.LedgerBalance.LEDGER_IDColumn, ledgerId);
                }
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.TRANS_MODEColumn, TransMode);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                result = dataManager.FetchData(DataSource.DataTable);
            }
            if (result.Success)
            {
                DataTable dtBalance = result.DataSource.Table;

                if (dtBalance != null && dtBalance.Rows.Count > 0)
                {
                    DataRow drBalance = dtBalance.Rows[0];

                    if (TransMode == "CR")
                    {
                        amount = this.NumberSet.ToDouble(drBalance["AMOUNT_CR"].ToString());
                        transMode = drBalance[AppSchema.LedgerBalance.TRANS_MODEColumn.ColumnName].ToString();
                    }
                    else
                    {
                        amount = this.NumberSet.ToDouble(drBalance["AMOUNT_DR"].ToString());
                        transMode = drBalance[AppSchema.LedgerBalance.TRANS_MODEColumn.ColumnName].ToString();
                    }
                }
            }
            balanceProperty.Amount = amount;
            balanceProperty.TransMode = transMode;
            balanceProperty.Result = result;
            return balanceProperty;
        }


        /// <summary>
        /// 12/02/2020, To get Sub ledger balance for with/without given date range
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="ledgerId"></param>
        /// <returns></returns>
        public BalanceProperty GetSubLedgereBalance(string projectIds, int ledgerId, int subledgerid, Nullable<DateTime> DateFrom, Nullable<DateTime> DateTo)
        {
            ResultArgs result = new ResultArgs();
            BalanceProperty balanceProperty = new BalanceProperty();
            double amount = 0;
            string transMode = "";

            if (projectIds == string.Empty) { projectIds = string.Empty; }
            if (ledgerId == 0) { ledgerId = 0; }

            using (DataManager dataManager = new DataManager(DataBaseTypeName)) //DataBaseType.HeadOffice
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterTypeName; //SQLAdapterType.HOSQL;
                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.TransBalance.FetchSubLedgerBalance;
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.PROJECT_IDColumn, projectIds);
                
                if (DateFrom != null)
                {
                    dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, DateFrom);
                }

                if (DateTo!= null)
                {
                    dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, DateTo);
                }

                if (ledgerId != 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.LedgerBalance.LEDGER_IDColumn, ledgerId);
                }

                if (subledgerid != 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.SUB_LEDGER_IDColumn, subledgerid);
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                result = dataManager.FetchData(DataSource.DataTable);
            }
            if (result.Success)
            {
                DataTable dtBalance = result.DataSource.Table;

                if (dtBalance != null && dtBalance.Rows.Count > 0)
                {
                    DataRow drBalance = dtBalance.Rows[0];

                    amount = this.NumberSet.ToDouble(drBalance[AppSchema.LedgerBalance.AMOUNTColumn.ColumnName].ToString());
                    transMode = drBalance[AppSchema.LedgerBalance.TRANS_MODEColumn.ColumnName].ToString();
                }
            }
            balanceProperty.Amount = amount;
            balanceProperty.TransMode = transMode;
            balanceProperty.Result = result;
            return balanceProperty;
        }

        public ResultArgs FetchActualFDAccountByVoucherId(Int32 Vid)
        {
            ResultArgs resultArgs = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchActualFDAccountByVoucherId))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_VOUCHER_IDColumn, Vid);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        /// <summary>
        /// On 24/01/2023, To check is given FD related Voucher allowed to modifed
        /// #. Check given entry falls with in current FY
        /// #. Check given entry is last history of FD
        /// </summary>
        /// <param name="FDAccountId"></param>
        /// <param name="FDRenewalId"></param>
        /// <returns></returns>
        public ResultArgs IsAllowToModifyFDVoucherEntry(Int32 FDAccountId = 0, Int32 FDRenewalId = 0, Int32 FDVoucherId = 0)
        {
            ResultArgs result = new ResultArgs();
            bool RecentRenewalExists = false;
            string RecentRenewalDate = string.Empty;
            Int32 RecentRenewalId = 0;
            Int32 RecentFDVoucherId = 0;
            Int32 RecentFDInterestVoucherId = 0;
            string InvestmentDate = string.Empty;
            try
            {
                if (FDVoucherId > 0) //If get FD account id by passing vouhcer id
                {
                    result = FetchActualFDAccountByVoucherId(FDVoucherId);
                    if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                    {
                        FDAccountId = result.DataSource.Table.Rows[0][AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName] != null ? NumberSet.ToInteger(result.DataSource.Table.Rows[0][AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName].ToString()) : 0;
                        FDRenewalId = result.DataSource.Table.Rows[0][AppSchema.FDRenewal.FD_RENEWAL_IDColumn.ColumnName] != null ? NumberSet.ToInteger(result.DataSource.Table.Rows[0][AppSchema.FDRenewal.FD_RENEWAL_IDColumn.ColumnName].ToString()) : 0;
                    }
                }

                if (FDAccountId > 0 || FDRenewalId > 0)
                {
                    using (DataManager dataMember = new DataManager(SQLCommand.FDAccount.FetchRecentFDRenewal))
                    {
                        dataMember.Parameters.Add(this.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn.ColumnName, FDAccountId);
                        dataMember.DataCommandArgs.IsDirectReplaceParameter = true;
                        result = dataMember.FetchData(DataSource.DataTable);
                    }

                    if (result.Success && result.DataSource.Table != null)
                    {
                        DataTable dtRecentFDRenewal = result.DataSource.Table;
                        dtRecentFDRenewal.DefaultView.RowFilter = AppSchema.FDRenewal.FD_RENEWAL_IDColumn.ColumnName + " > 0";
                        if (dtRecentFDRenewal.DefaultView.Count > 0)
                        {
                            RecentRenewalExists = true;
                            RecentRenewalId = NumberSet.ToInteger(dtRecentFDRenewal.DefaultView[0][AppSchema.FDRenewal.FD_RENEWAL_IDColumn.ColumnName].ToString());
                            RecentFDVoucherId = NumberSet.ToInteger(dtRecentFDRenewal.DefaultView[0][AppSchema.FDRenewal.FD_VOUCHER_IDColumn.ColumnName].ToString());
                            RecentFDInterestVoucherId = NumberSet.ToInteger(dtRecentFDRenewal.DefaultView[0][AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn.ColumnName].ToString());
                            RecentRenewalDate = dtRecentFDRenewal.DefaultView[0][AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName].ToString();
                        }
                        else
                        {
                            dtRecentFDRenewal.DefaultView.RowFilter = string.Empty;
                            InvestmentDate = dtRecentFDRenewal.DefaultView[0][AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName].ToString();
                            RecentFDVoucherId = NumberSet.ToInteger(dtRecentFDRenewal.DefaultView[0][AppSchema.FDRenewal.FD_VOUCHER_IDColumn.ColumnName].ToString());
                        }

                        if (RecentRenewalExists)
                        {   //#. If renewal exists, check is it Last renewal and with in the current FY
                            if ((FDRenewalId > 0 && RecentRenewalId == FDRenewalId))
                            {
                                if (DateSet.ToDate(this.YearFrom, false) <= DateSet.ToDate(RecentRenewalDate, false) &&
                                    DateSet.ToDate(this.YearTo, false) >= DateSet.ToDate(RecentRenewalDate, false))
                                {
                                    result.Success = true;
                                }
                                else
                                {
                                    result.Message = "This Fixed Deposit Voucher doesn't fall with in the current Finance Year, You can't modify it.";
                                }
                            }
                            else
                            {
                                result.Message = "This Fixed Deposit Voucher has previous renewal history, You can't modify it.";
                            }
                        }
                        else if (!RecentRenewalExists )
                        { //#. If not renewal exists, check is it investment and with in the current FY
                            if (RecentFDVoucherId > 0) //For FD Investment
                            {
                                if (DateSet.ToDate(this.YearFrom, false) <= DateSet.ToDate(InvestmentDate, false) &&
                                        DateSet.ToDate(this.YearTo, false) >= DateSet.ToDate(InvestmentDate, false))
                                {
                                    result.Success = true;
                                }
                                else
                                {
                                    result.Message = "This Fixed Deposit Voucher doesn't fall with in the current Finance Year, You can't modify it.";
                                }
                            }
                        }
                        else
                        {
                            result.Message = "Not able to check FD Voucher to be modified";
                        }
                    }
                }
                else
                {
                    result.Message = "Not able to check FD Voucher to be modified :  Invalid FD details";
                }
            }
            catch (Exception err)
            {
                result.Message = "Not able to check FD Voucher to be modified :  " + err.Message;
            }

            return result;
        }

        #endregion
    }
}
