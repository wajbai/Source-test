using Bosco.DAO.Schema;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using System.Collections;
using System;
using Bosco.Model.UIModel;
using Bosco.Model.Business;
using AcMEDSync.Model;


namespace Bosco.Model.Transaction
{
    public class FDAccountSystem : SystemBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        private string VoucherType = "Contra";
        bool isEditMode = false;
        double LedgerAmt = 0;
        Bosco.Utility.ConfigSetting.SettingProperty setting = new Utility.ConfigSetting.SettingProperty();
        Int32 CreatedBy = 0;
        Int32 ModifiedBy = 0;
        string CreatedByName = string.Empty;
        string ModifiedByName = string.Empty;

        /// <summary>
        /// On 20/10/2021, This property is used to skip bank ledger's which is closed on or equal to this date
        /// 
        /// This date is apart from balance date. If we take reports for given period (DATE_FROM and DATE_TO)
        /// When we show closing balance for DATE_TO, we have to check bank ledger closed date for DATE_FROM
        /// </summary>
        public string LedgerClosedDateForFilter { get; set; }

        #endregion

        #region Constructor
        public FDAccountSystem()
        {
            //On 15/09/2021, To fix User details by dfault ---------------------------------------------
            CreatedBy = this.NumberSet.ToInteger(this.LoginUserId.ToString());
            ModifiedBy = this.NumberSet.ToInteger(this.LoginUserId.ToString());

            CreatedByName = FirstName; //LoginUserName.ToString();
            ModifiedByName = FirstName; //LoginUserName.ToString();
            //------------------------------------------------------------------------------------------
        }

        public FDAccountSystem(int FDAccountId)
            : this()
        {
            FillFDAccountDetails(FDAccountId);
        }
        #endregion

        #region Ledger Properties
        public int LedgerId { get; set; }
        public string LedgerCode { get; set; }
        public string LedgerName { get; set; }
        public string FDLedgerName { get; set; }
        public string FDBankLedgerName { get; set; }
        public int GroupId { get; set; }
        public int IsCostCentre { get; set; }
        public int IsBankInterestLedger { get; set; }
        public string LedgerType { get; set; }
        public string LedgerSubType { get; set; }
        public int BankAccountId { get; set; }
        public string AccountDate { get; set; }
        public int AccountTypeId { get; set; }
        public int BankId { get; set; }
        public string OpenedDate { get; set; }
        //public string ClosedDate { get; set; }
        public string OperatedBy { get; set; }
        public string LedgerNotes { get; set; }
        public string BankAccNotes { get; set; }
        public int FDVoucherId { get; set; }
        public int FDContraVoucherId { get; set; }
        public int[] FdVoucherIdList { get; set; }
        public int ProjectId { get; set; }
        public int SortId { get; set; }
        public string ProId { get; set; }
        public string FDSubTypes { get; set; }
        private int FDRenewalInterestVoucherId { get; set; }
        private int FDWithdrawalContraVoucherId { get; set; }
        private int FDWithdrawalInterestVoucherId { get; set; }
        public int FDLastRenewalId { get; set; }
        #endregion

        #region FD Account Properties

        public int FDAccountId { get; set; }
        public string FDAccountNumber { get; set; }
        public int FDScheme { get; set; }
        public string FDAccountHolderName { get; set; }
        public double FdAmount { get; set; }
        public double FDInterestAmount { get; set; }
        public double FDExpectedMaturityValue { get; set; }
        public double FDInterestRate { get; set; }
        public double FDPrinicipalAmount { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime FDMaturityDate { get; set; }
        public string FDTransType { get; set; }
        public string FDRINTransType { get; set; }
        public string FDTransMode { get; set; }
        public string FDProjectName { get; set; }
        public string FDLedgerCurBalance { get; set; }
        public int CashBankId { get; set; }
        public string ReceiptNo { get; set; }
        public string FDVoucherNo { get; set; }
        public string FDWithdrwalReceiptVoucherNo { get; set; }
        public int TransVoucherMethod { get; set; }
        public string FDOPInvestmentDate { get; set; }
        public DataTable dtFDAcountDetails { get; set; }
        public DataTable dtCashBankLedger { get; set; }
        public DataTable dtFDLedger { get; set; }
        public string CashBankLedgerGroup { get; set; }
        public string CashBankLedgerAmt { get; set; }
        public string FDLedgerAmt { get; set; }
        public int VoucherId { get; set; }
        public int PrevProjectId { get; set; }
        public int PrevLedgerId { get; set; }
        public int VoucherIntrestMode { get; set; }
        public int FDStatus { get; set; }
        public int InterestType { get; set; }
        public string FDActiveStatus { get; set; }
        public DateTime ClosedDate { get; set; }
        public DateTime dteWithdrawReceiptDate { get; set; }
        public int VoucherDefinitionId { get; set; }

        //On 24/07/2023, Voucher Authorization details
        public int AuthorizedStatus { get; set; }

        private Nullable<DateTime> authorizedon;
        public Nullable<DateTime> AuthorizedOn
        {
            get
            {
                return authorizedon;
            }
        }

        private string authorizedbyname = string.Empty;
        public string AuthorizedByName
        {
            get
            {
                return authorizedbyname;
            }
        }


        //On 20/05/2022, to Assign Pentaly details
        public Int32 ChargeMode { get; set; } //0 - None 1 - Deduct form Principal, 2- Deduct form Interest
        public double ChargeAmount { get; set; }
        public double ChargeAmountByInterest { get; set; }
        public double ChargeAmountByPrincipal { get; set; }
        public Int32 ChargeLedgerId { get; set; }

        //On 23/06/2022, Expected Interest Amount
        public double ExpectedInterestAmount { get; set; }

        //On 08/05/2024, Mutual Fund Properties
        public string MFFolioNo { get; set; }
        public string MFSchemeName { get; set; }
        public double MFNAVPerUnit { get; set; }
        public double MFNoOfUnit { get; set; }
        public Int32 MFModeOfHolding { get; set; }

        #endregion

        #region FD Renewal Properties
        public double IntrestAmount { get; set; }
        public double WithdrawAmount { get; set; }
        public double ReInvestmentAmount { get; set; }
        public double FDTDSAmount { get; set; }
        public double FDExpectedRenewMaturityValue { get; set; }
        public int IntrestLedgerId { get; set; }
        public int BankLedgerId { get; set; }
        public int FDIntrestVoucherId { get; set; }
        public DateTime RenewedDate { get; set; }
        public DateTime WithdrawDate { get; set; }
        public string RenewalType { get; set; }
        public double IntrestRate { get; set; }
        public int FDRenewalStatus { get; set; }
        public int FDLedgerId { get; set; }
        public string FDRenewalType { get; set; }
        public double PrinicipalAmount { get; set; }
        public double FDInterstCalAmount { get; set; }
        public double FDCashBankWithdrawAmount { get; set; }
        public int FDWithdrwalInterestVoucherId { get; set; }
        public double PrinicipalInsAmount { get; set; }
        public int FDRenewalId { get; set; }
        public string FDType { get; set; }

        //On 26/10/2023, FD Renewal Transmode for Post Interest, for other FD related vouchers are fixed as DR
        public TransSource FDRenewalTransMode { get; set; }

        public Int32 InvestmentTypeId { get; set; }


        public decimal ContributionAmount { get; set; }
        public int CurrencyCountryId { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal LiveExchangeRate { get; set; }
        public decimal CalculatedAmount { get; set; }
        public decimal ActualAmount { get; set; }

        #endregion

        #region Ledger Methods
        public ResultArgs FetchLedger()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.FetchFDLedgers))
            {
                resultArgs = dataMember.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchFDLedgerById(int FDledgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchFDLedgerById))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.LEDGER_IDColumn, FDledgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            return resultArgs;
        }

        public ResultArgs FetchProjectByLedger()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.FDAccount.FetchProjectByLedger))
            {
                resultArgs = dataMember.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchFDRegistersView(DateTime dtFrom, DateTime dtTo, string projectId, Int32 fdinvestmenttype)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.FDAccount.FetchFDRegistersView))
            {
                dataMember.Parameters.Add(this.AppSchema.FDRegisters.DATE_FROMColumn, dtFrom);
                dataMember.Parameters.Add(this.AppSchema.FDRegisters.DATE_TOColumn, dtTo);
                if (!string.IsNullOrEmpty(projectId) && !projectId.Equals("0"))
                {
                    dataMember.Parameters.Add(this.AppSchema.FDRegisters.PROJECT_IDColumn, projectId);
                }

                if (fdinvestmenttype != (Int32)FDInvestmentType.None)
                {
                    dataMember.Parameters.Add(this.AppSchema.Ledger.FD_INVESTMENT_TYPE_IDColumn, fdinvestmenttype);
                }

                if (!IsFullRightsReservedUser)
                {
                    dataMember.Parameters.Add(this.AppSchema.UserRole.USERROLE_IDColumn, this.LoginUserRoleId);
                }

                dataMember.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataMember.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 19/10/2022, To how FD full history
        /// </summary>
        /// <param name="FDAccountid"></param>
        /// <returns></returns>
        public ResultArgs FetchFDFullHistory(Int32 FDAccountid = 0)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.FDAccount.FetchFDFullHistory))
            {
                if (FDAccountid > 0)
                {
                    dataMember.Parameters.Add(this.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn.ColumnName, FDAccountid);
                }
                dataMember.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataMember.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        public ResultArgs FetchAllProjectId()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.FDAccount.FetchAllProjectId))
            {
                resultArgs = dataMember.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs SaveFdLedger()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = SaveLedgerDetails(dataManager);
                if (resultArgs.Success)
                {
                    if (LedgerId != 0)
                    {
                        LedgerId = LedgerId == 0 ? NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : LedgerId;
                        //string[] projectId = ProId.Split(',');
                        //for (int i = 0; i < projectId.Length; i++)
                        //{
                        //    ProjectId = NumberSet.ToInteger(projectId[i].ToString());
                        resultArgs = DeleteProjectLedger(dataManager);
                        // }
                    }
                    else
                    {
                        LedgerId = LedgerId == 0 ? NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : LedgerId;
                        string[] projectId = ProId.Split(',');
                        for (int i = 0; i < projectId.Length; i++)
                        {
                            ProjectId = NumberSet.ToInteger(projectId[i].ToString());
                            resultArgs = SaveProjectLedger(dataManager);
                        }
                    }
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs SaveProjectLedger(DataManager projectLedgerManager)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectLedgerMappingAdd))
            {
                dataManager.Database = projectLedgerManager.Database;
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs SaveLedgerDetails(DataManager ledgerDataManager)
        {
            using (DataManager dataManager = new DataManager((LedgerId == 0) ? SQLCommand.LedgerBank.Add : SQLCommand.LedgerBank.Update))
            {
                dataManager.Database = ledgerDataManager.Database;
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId, true);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_CODEColumn, LedgerCode); //LedgerCode.ToUpper()
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, LedgerName);
                dataManager.Parameters.Add(this.AppSchema.Ledger.GROUP_IDColumn, GroupId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_TYPEColumn, LedgerType);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_SUB_TYPEColumn, LedgerSubType);
                dataManager.Parameters.Add(this.AppSchema.Ledger.BANK_ACCOUNT_IDColumn, BankAccountId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_COST_CENTERColumn, IsCostCentre);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_BANK_INTEREST_LEDGERColumn, IsBankInterestLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.NOTESColumn, LedgerNotes);
                dataManager.Parameters.Add(this.AppSchema.Ledger.SORT_IDColumn, SortId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteProjectLedger(DataManager projectLedgerManager)
        {
            using (DataManager dataManger = new DataManager(SQLCommand.FDAccount.DeleteProjectLedger))
            {
                dataManger.Database = projectLedgerManager.Database;
                //  dataManger.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManger.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManger.UpdateData();
                if (resultArgs.Success)
                {
                    string[] projectId = ProId.Split(',');
                    for (int i = 0; i < projectId.Length; i++)
                    {
                        ProjectId = NumberSet.ToInteger(projectId[i].ToString());
                        resultArgs = SaveProjectLedger(dataManger);
                    }
                }
            }
            return resultArgs;
        }

        #endregion

        #region FD Account Details

        private ResultArgs SaveFDAccountDetails(DataManager dataOpBalance)
        {
            using (DataManager dataManager = new DataManager(FDAccountId == 0 ? SQLCommand.FDAccount.Add : SQLCommand.FDAccount.Update))
            {
                dataManager.Database = dataOpBalance.Database;
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.BANK_IDColumn, BankId);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.BANK_LEDGER_IDColumn, BankLedgerId);
                //dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_VOUCHER_IDColumn, VoucherId);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_VOUCHER_IDColumn, FDWithdrawalContraVoucherId);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_NUMBERColumn, FDAccountNumber);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_SCHEMEColumn, FDScheme);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.ACCOUNT_HOLDERColumn, FDAccountHolderName);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.INVESTMENT_DATEColumn, CreatedOn);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.AMOUNTColumn, FdAmount);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.INTEREST_RATEColumn, FDInterestRate);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.INTEREST_AMOUNTColumn, FDInterestAmount);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.INTEREST_TYPEColumn, InterestType);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.MATURED_ONColumn, FDMaturityDate);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.TRANS_TYPEColumn, FDTransType);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.TRANS_MODEColumn, FDTransMode);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.NOTESColumn, LedgerNotes);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.EXPECTED_MATURITY_VALUEColumn, FDExpectedMaturityValue);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.RECEIPT_NOColumn, ReceiptNo);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_SUB_TYPESColumn, FDSubTypes);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.EXPECTED_INTEREST_VALUEColumn, ExpectedInterestAmount);

                // On 15/10/20224, To set currency details
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CONTRIBUTION_AMOUNTColumn, ContributionAmount);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CURRENCY_COUNTRY_IDColumn, CurrencyCountryId);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.EXCHANGE_RATEColumn, ExchangeRate);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CALCULATED_AMOUNTColumn, CalculatedAmount);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.ACTUAL_AMOUNTColumn, ActualAmount);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.EXCHANGE_COUNTRY_IDColumn, CurrencyCountryId);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.IS_MULTI_CURRENCYColumn, this.AllowMultiCurrency);

                //On 09/05/2024, To set Mutual Fund Properties
                dataManager.Parameters.Add(this.AppSchema.FDAccount.MF_FOLIO_NOColumn, MFFolioNo);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.MF_SCHEME_NAMEColumn, MFSchemeName);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.MF_NAV_PER_UNITColumn, MFNAVPerUnit);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.MF_NO_OF_UNITSColumn, MFNoOfUnit);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.MF_MODE_OF_HOLDINGColumn, MFModeOfHolding);

                dataManager.Parameters.Add(this.AppSchema.FDAccount.STATUSColumn, 1);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public string FetchCurrentLedgerBalance()
        {
            string LedgerCurBalance = string.Empty;
            //FetchOpBalanceList
            //  using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchLedgerBalance))
            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchOpBalanceList))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, (int)FixedLedgerGroup.FixedDeposit);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    foreach (DataRow drLedgerBalance in resultArgs.DataSource.Table.Rows)
                    {
                        FdAmount = drLedgerBalance[this.AppSchema.FDAccount.AMOUNTColumn.ColumnName] != null ? NumberSet.ToDouble(drLedgerBalance[this.AppSchema.FDAccount.AMOUNTColumn.ColumnName].ToString()) : 0;
                        FDTransMode = drLedgerBalance[this.AppSchema.FDAccount.TRANS_MODEColumn.ColumnName] != null ? drLedgerBalance[this.AppSchema.FDAccount.TRANS_MODEColumn.ColumnName].ToString() : string.Empty;
                        LedgerCurBalance = NumberSet.ToCurrency(FdAmount) + " " + FDTransMode;
                    }
                }
            }
            return LedgerCurBalance;
        }

        public ResultArgs FetchFDAccounts()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.Fetch))
            {
                string FdOpening = "OP";
                string FdInvestment = "IN";
                FDRINTransType = FdOpening.Trim() + "','" + FdInvestment.Trim();
                if (FDTransType == "OP" || FDTransType == "IN")
                {
                    //dataManager.Parameters.Add(this.AppSchema.FDAccount.TRANS_TYPEColumn, FDType.Equals(FDTypes.RIN.ToString()) ? FDRINTransType : FDTransType);
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.TRANS_TYPEColumn, FDTransType);
                }
                else
                {
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.TRANS_TYPEColumn, FDRINTransType);
                }

                if (!IsFullRightsReservedUser)
                {
                    setting.UserAllProjectId = FetchUserProject();
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.PROJECT_IDColumn, (!string.IsNullOrEmpty(setting.UserAllProjectId)) ? setting.UserAllProjectId : "0");
                }

                if (FDType == FDTypes.RIN.ToString())
                {
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_SCHEMEColumn, 1);
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private string FetchUserProject()
        {
            string UserProject = string.Empty;
            using (DataManager dataManager = new DataManager(SQLCommand.UserRights.FetchUserProject))
            {
                dataManager.Parameters.Add(this.AppSchema.UserRights.USER_ROLE_IDColumn, LoginUserRoleId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    UserProject = resultArgs.DataSource.Table.Rows[0]["PROJECT_ID"].ToString();
            }
            return UserProject;
        }

        public DataSet LoadFDRenewalDetails()
        {
            DataSet dsFDRenewal = new DataSet();
            try
            {
                string FDAccountId = string.Empty;
                resultArgs = FetchFDAccounts();
                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    DataView dvFDAccount = resultArgs.DataSource.Table.DefaultView;
                    //if (FDType.Equals(FDTypes.RN.ToString()) || FDType.Equals(FDTypes.POI.ToString()))
                    if (FDType.Equals(FDTypes.RN.ToString()) || FDType.Equals(FDTypes.POI.ToString()) || FDType.Equals(FDTypes.WD.ToString()) || FDType.Equals(FDTypes.RIN.ToString()))
                        dvFDAccount.RowFilter = "CLOSING_STATUS<>'Closed'";

                    //On 09/05/2024, To skip mutual fund FDs for Renewals----------------------------------------------------------------------------------------
                    if (FDType.Equals(FDTypes.RN.ToString()))
                    {
                        string fitler = dvFDAccount.RowFilter;
                        dvFDAccount.RowFilter = fitler + (!string.IsNullOrEmpty(fitler) ? " AND " : "") +
                            this.AppSchema.Ledger.FD_INVESTMENT_TYPE_IDColumn.ColumnName + " <> " + (int)FDInvestmentType.MutualFund;
                    }
                    //-------------------------------------------------------------------------------------------------------------------------------------------

                    dvFDAccount.ToTable().TableName = "Master";
                    resultArgs.DataSource.Table.TableName = "Master";
                    dsFDRenewal.Tables.Add(dvFDAccount.ToTable());

                    for (int i = 0; i < dvFDAccount.ToTable().Rows.Count; i++)
                    {
                        FDAccountId += dvFDAccount.ToTable().Rows[i][AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName].ToString() + ",";
                    }

                    FDAccountId = FDAccountId.TrimEnd(',');
                    if (!string.IsNullOrEmpty(FDAccountId))
                    {
                        if (FDType.Equals(FDTypes.RN.ToString()))
                        {
                            resultArgs = FetchFDRenewals(FDAccountId);
                            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                            {
                                DataTable dtRenewal = resultArgs.DataSource.Table;
                                dtRenewal.Columns.Add("SELECT", typeof(int));
                                dtRenewal.TableName = "Renewal History";
                                dsFDRenewal.Tables.Add(dtRenewal);
                                if (FDType.Equals(FDTypes.RN.ToString()))
                                {
                                    dsFDRenewal.Relations.Add(dsFDRenewal.Tables[1].TableName, dsFDRenewal.Tables[0].Columns[this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName], dsFDRenewal.Tables[1].Columns[this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName]);
                                    //resultArgs = FetchFDPostInterestDetails(FDAccountId);
                                    //if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                                    //{
                                    //    resultArgs.DataSource.Table.Columns.Add("SELECT", typeof(int));
                                    //    resultArgs.DataSource.Table.TableName = "Post Interest History";
                                    //    dsFDRenewal.Tables.Add(resultArgs.DataSource.Table);
                                    //    dsFDRenewal.Relations.Add(dsFDRenewal.Tables[2].TableName, dsFDRenewal.Tables[0].Columns[this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName], dsFDRenewal.Tables[2].Columns[this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName]);
                                    //}
                                }

                            }
                        }
                        // 15/01/2025, to have order as desc 
                        else if (FDType.Equals(FDTypes.POI.ToString()))// if it has only post interest
                        {
                            resultArgs = FetchFDPostInterestDetails(FDAccountId);
                            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                            {
                                resultArgs.DataSource.Table.DefaultView.Sort = "RENEWAL_DATE DESC";
                                resultArgs.DataSource.Data = resultArgs.DataSource.Table.DefaultView.ToTable();

                                resultArgs.DataSource.Table.Columns.Add("SELECT", typeof(int));
                                resultArgs.DataSource.Table.TableName = "Post Interest History";
                                dsFDRenewal.Tables.Add(resultArgs.DataSource.Table);
                                dsFDRenewal.Relations.Add(dsFDRenewal.Tables[1].TableName, dsFDRenewal.Tables[0].Columns[this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName], dsFDRenewal.Tables[1].Columns[this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName]);
                            }
                        }
                        else if (FDType.Equals(FDTypes.WD.ToString()))
                        {
                            resultArgs = FetchFDWithDrawalDetails(FDAccountId);
                            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                            {
                                resultArgs.DataSource.Table.Columns.Add("SELECT", typeof(int));
                                resultArgs.DataSource.Table.TableName = "Withdrawal History";
                                dsFDRenewal.Tables.Add(resultArgs.DataSource.Table);
                                dsFDRenewal.Relations.Add(dsFDRenewal.Tables[1].TableName, dsFDRenewal.Tables[0].Columns[this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName], dsFDRenewal.Tables[1].Columns[this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName]);
                            }
                        }
                        else if (FDType.Equals(FDTypes.RIN.ToString()))
                        {
                            resultArgs = FetchFDReInvestmentDetails(FDAccountId);
                            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                            {
                                resultArgs.DataSource.Table.Columns.Add("SELECT", typeof(int));
                                resultArgs.DataSource.Table.TableName = "Re-Investment History";
                                dsFDRenewal.Tables.Add(resultArgs.DataSource.Table);
                                dsFDRenewal.Relations.Add(dsFDRenewal.Tables[1].TableName, dsFDRenewal.Tables[0].Columns[this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName], dsFDRenewal.Tables[1].Columns[this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, false);
            }
            return dsFDRenewal;
        }

        public ResultArgs FetchFDRenewals(string FD_ACCOUNT_ID)
        {

            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchFDRenewalById))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FD_ACCOUNT_ID);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchFDHistoryByFDId(string FD_ACCOUNT_ID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchFDHistoryByFDId))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FD_ACCOUNT_ID);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchFDPostInterestDetails(string FD_ACCOUNT_ID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchFDPostInterestDetailsById))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FD_ACCOUNT_ID);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public void FillFDAccountDetails(int FdAccId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchFDById))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FdAccId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    FDAccountId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName].ToString()) : 0;
                    VoucherId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.FD_VOUCHER_IDColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.FD_VOUCHER_IDColumn.ColumnName].ToString()) : 0;
                    PrevProjectId = ProjectId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.PROJECT_IDColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.PROJECT_IDColumn.ColumnName].ToString()) : 0;
                    PrevLedgerId = LedgerId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.LEDGER_IDColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.LEDGER_IDColumn.ColumnName].ToString()) : 0;
                    BankId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.BANK_IDColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.BANK_IDColumn.ColumnName].ToString()) : 0;
                    BankLedgerId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDRenewal.BANK_LEDGER_IDColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDRenewal.BANK_LEDGER_IDColumn.ColumnName].ToString()) : 0;
                    FDProjectName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.PROJECTColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.PROJECTColumn.ColumnName].ToString() : string.Empty;
                    FDAccountHolderName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.ACCOUNT_HOLDERColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.ACCOUNT_HOLDERColumn.ColumnName].ToString() : string.Empty;
                    FDAccountNumber = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.FD_ACCOUNT_NUMBERColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.FD_ACCOUNT_NUMBERColumn.ColumnName].ToString() : string.Empty;
                    FDScheme = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.FD_SCHEMEColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.FD_SCHEMEColumn.ColumnName].ToString()) : 0;
                    FDVoucherNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_NOColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_NOColumn.ColumnName].ToString() : string.Empty;
                    FDMaturityDate = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.MATURED_ONColumn.ColumnName] != DBNull.Value ? DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.MATURED_ONColumn.ColumnName].ToString(), false) : DateTime.Now;
                    CreatedOn = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.INVESTMENT_DATEColumn.ColumnName] != DBNull.Value ? DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.INVESTMENT_DATEColumn.ColumnName].ToString(), false) : DateTime.Now;
                    FdAmount = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.AMOUNTColumn.ColumnName] != DBNull.Value ? NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.AMOUNTColumn.ColumnName].ToString()) : 0;
                    FDInterestRate = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.INTEREST_RATEColumn.ColumnName] != DBNull.Value ? NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.INTEREST_RATEColumn.ColumnName].ToString()) : 0;
                    FDInterestAmount = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.INTEREST_AMOUNTColumn.ColumnName] != DBNull.Value ? NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.INTEREST_AMOUNTColumn.ColumnName].ToString()) : 0;
                    InterestType = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.INTEREST_TYPEColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.INTEREST_TYPEColumn.ColumnName].ToString()) : 0;
                    FDTransMode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.TRANS_MODEColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.TRANS_MODEColumn.ColumnName].ToString() : string.Empty;
                    FDTransType = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.TRANS_TYPEColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.TRANS_TYPEColumn.ColumnName].ToString() : string.Empty;
                    ReceiptNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.RECEIPT_NOColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.RECEIPT_NOColumn.ColumnName].ToString() : string.Empty;
                    //FDProjectName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.NOTESColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.NOTESColumn.ColumnName].ToString() : string.Empty;
                    LedgerNotes = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.NOTESColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.NOTESColumn.ColumnName].ToString() : string.Empty;
                    FDLedgerName = resultArgs.DataSource.Table.Rows[0]["FD_LEDGER_NAME"] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0]["FD_LEDGER_NAME"].ToString() : string.Empty;
                    FDBankLedgerName = resultArgs.DataSource.Table.Rows[0]["BANK"] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0]["BANK"].ToString() : string.Empty;
                    FDExpectedMaturityValue = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.EXPECTED_MATURITY_VALUEColumn.ColumnName] != DBNull.Value ? NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.EXPECTED_MATURITY_VALUEColumn.ColumnName].ToString()) : 0;
                    ExpectedInterestAmount = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.EXPECTED_INTEREST_VALUEColumn.ColumnName] != DBNull.Value ? NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.EXPECTED_INTEREST_VALUEColumn.ColumnName].ToString()) : 0;

                    //09/05/2024, To get Mutual Fund details
                    InvestmentTypeId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.FD_INVESTMENT_TYPE_IDColumn.ColumnName] != DBNull.Value ?
                                NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.FD_INVESTMENT_TYPE_IDColumn.ColumnName].ToString()) : (Int32)FDInvestmentType.FD;
                    MFFolioNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.MF_FOLIO_NOColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.MF_FOLIO_NOColumn.ColumnName].ToString() : string.Empty;
                    MFSchemeName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.MF_SCHEME_NAMEColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.MF_SCHEME_NAMEColumn.ColumnName].ToString() : string.Empty;
                    MFNAVPerUnit = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.MF_NAV_PER_UNITColumn.ColumnName] != DBNull.Value ?
                                NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.MF_NAV_PER_UNITColumn.ColumnName].ToString()) : 0;
                    MFNoOfUnit = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.MF_NO_OF_UNITSColumn.ColumnName] != DBNull.Value ?
                                NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.MF_NO_OF_UNITSColumn.ColumnName].ToString()) : 0;
                    MFModeOfHolding = resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.MF_MODE_OF_HOLDINGColumn.ColumnName] != DBNull.Value ?
                                NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.MF_MODE_OF_HOLDINGColumn.ColumnName].ToString()) : 0;

                    //15/10/2024, To set currency details 
                    CurrencyCountryId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.CURRENCY_COUNTRY_IDColumn.ColumnName].ToString());
                    ContributionAmount = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.CONTRIBUTION_AMOUNTColumn.ColumnName].ToString());
                    ExchangeRate = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.FDAccount.EXCHANGE_RATEColumn.ColumnName].ToString());
                    ActualAmount = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.ACTUAL_AMOUNTColumn.ColumnName].ToString());
                    CalculatedAmount = ActualAmount;

                    FDLedgerCurBalance = NumberSet.ToNumber(FdAmount) + " " + FDTransMode;
                }
            }
        }
        public ResultArgs FetchLedgerByProject()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchLedgerByProject))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.PROJECT_IDColumn, ProjectId);

                if (!string.IsNullOrEmpty(LedgerClosedDateForFilter))
                {
                    dataManager.Parameters.Add(this.AppSchema.BankAccount.DATE_CLOSEDColumn, LedgerClosedDateForFilter);
                }

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public string FetchProjectFDAccountId()
        {
            string ProjectId = string.Empty;
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchProjectId))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                    {
                        ProjectId += dr[this.AppSchema.FDAccount.PROJECT_IDColumn.ColumnName].ToString() + ",";
                    }
                    ProjectId = ProjectId.TrimEnd(',');
                }
            }
            return ProjectId;
        }

        public string FetchProjectFDLedgerId()
        {
            string TransType = string.Empty;
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchProjectId))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                TransType = resultArgs.DataSource.Data != null ? resultArgs.DataSource.Data.ToString() : "";
            }
            return TransType;
        }

        private ResultArgs SaveFDInvestmentDetails(DataManager dataManagerIns)
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Database = dataManagerIns.Database;
                resultArgs = VoucherTrans(dataManager);
                if (resultArgs.Success)
                {
                    // VoucherId = VoucherId == 0 ? FDVoucherId : VoucherId;
                    resultArgs = SaveFDAccountDetails(dataManager);
                }
            }
            return resultArgs;
        }

        private ResultArgs SaveFDOpeningBalance(DataManager dataManagerOP)
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Database = dataManagerOP.Database;
                resultArgs = SaveFDAccountDetails(dataManager);
                if (resultArgs.Success)
                {
                    using (BalanceSystem balanceSystem = new BalanceSystem())
                    {
                        if (isEditMode)
                        {
                            if (ProjectId != PrevProjectId || LedgerId != PrevLedgerId)
                            {
                                LedgerAmt = FetchLedgerBalance(PrevProjectId, PrevLedgerId);
                                resultArgs = balanceSystem.UpdateOpBalance(FDOPInvestmentDate, PrevProjectId, PrevLedgerId, LedgerAmt, FDTransMode, TransactionAction.EditAfterSave);
                            }
                            LedgerAmt = FetchLedgerBalance(ProjectId, LedgerId);
                            resultArgs = balanceSystem.UpdateOpBalance(FDOPInvestmentDate, ProjectId, LedgerId, LedgerAmt, FDTransMode, TransactionAction.New);
                        }
                        else
                        {
                            LedgerAmt = FetchLedgerBalance(ProjectId, LedgerId);
                            resultArgs = balanceSystem.UpdateOpBalance(FDOPInvestmentDate, ProjectId, LedgerId, LedgerAmt, FDTransMode, TransactionAction.New);
                        }
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs SaveFDAccount()
        {
            bool canProceed = true;
            //On 24/05/2022, If penalty mode/amount is enabled, check penalty amount and if deduct mode is both, reset withdrwal amount and Interest Amount ---
            if (ChargeMode > 0)
            {
                resultArgs = ValidatePenaltyAmount(ChargeMode, ChargeAmount, ChargeLedgerId, FDInterstCalAmount, WithdrawAmount);
                canProceed = resultArgs.Success;
                ChargeAmountByInterest = ChargeAmountByPrincipal = 0;
                if (ChargeMode == 3) //Deduct Mode is Both (Fully from Interest and Priciapal amount)
                {
                    double remainingamt = (FDInterstCalAmount - ChargeAmount);
                    if (remainingamt <= 0)
                    {
                        FDInterstCalAmount = 0;
                        IntrestAmount = 0;
                        FDTDSAmount = 0;
                    }
                    ChargeAmountByPrincipal = Math.Abs(remainingamt);
                }
            }
            //---------------------------------------------------------------------------------------------------------------------------------------------------

            //On 21/10/2022, To check FD Validation
            resultArgs = ValidateFDAccount(FDAccountId, FDRenewalId, CreatedOn, FDMaturityDate, FDType, FDRenewalType, InterestType, IntrestAmount, ReInvestmentAmount, WithdrawAmount, FDRenewalMode.Edit);

            if (canProceed && resultArgs.Success)
            {
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.BeginTransaction();
                    isEditMode = (FDAccountId != 0);
                    if (FDTransType == FDTypes.IN.ToString())
                    {
                        SaveFDInvestmentDetails(dataManager);
                    }
                    else if (FDTransType == FDTypes.RN.ToString() || FDTransType == FDTypes.RIN.ToString() || FDTransType == FDTypes.WD.ToString() || FDTransType == FDTypes.PWD.ToString() || FDTransType == FDTypes.POI.ToString())
                    {
                        if (FDAccountId > 0 && IntrestAmount == 0)
                        {
                            resultArgs = DeleteIntrestVoucher(this.FDIntrestVoucherId); // aldrin delete voucher when the interest amount is 0 in the edit mode.
                            // it is changed voucherId into FdInterest Id with help of Mr Alex
                            if (resultArgs.Success)
                            {
                                SaveFdRenewalDetails(dataManager);
                            }
                            else
                            {
                                MessageRender.ShowMessage(resultArgs.Message + Environment.NewLine + resultArgs.Exception);
                            }
                        }
                        else
                        {
                            SaveFdRenewalDetails(dataManager);
                        }
                    }
                    else
                    {
                        SaveFDOpeningBalance(dataManager);
                    }
                    dataManager.EndTransaction();
                }
            }

            return resultArgs;
        }


        /// <summary>
        /// On 24/05/2022, To validate Penalty amount should be proper amount
        /// </summary>
        /// <returns></returns>
        public ResultArgs ValidatePenaltyAmount(Int32 chargemode, double chargeamt, double chargeledgerid, double interestamt, double principalamt)
        {
            ResultArgs result = new ResultArgs();
            string msg = string.Empty;
            try
            {
                if (chargemode > 0 && chargeamt > 0 && chargeledgerid > 0)
                {
                    if (chargeamt > 0)
                    {
                        if (chargemode == 0) //0. None
                        {
                            msg = string.Empty;
                        }
                        else if (chargemode == 1) //1. Deduct from Interest Amount
                        {
                            if (chargeamt > interestamt)
                            {
                                msg = "Penalty Amount should be less than Interest Amount";
                            }
                            else if (chargeamt == interestamt)
                            {
                                msg = "Can't process as Penalty amount and Interest Amount are tailed equally";
                            }
                        }
                        else if (chargemode == 2) //2. Deduct from Priciapl Amount
                        {
                            if (chargeamt > principalamt)
                            {
                                msg = "Penalty Amount should be less than Withdrwal Amount";
                            }
                            else if (chargeamt == principalamt)
                            {
                                msg = "Can't process as Penalty Amount and Interest Aamount are tailed equally";
                            }
                        }
                        else if (chargemode == 3) //3. Deduct from both (fully from Interest and remaining from Pricipal)
                        {
                            double dedPreicipleAmount = chargeamt - interestamt;
                            if (chargeamt == interestamt)
                            {
                                msg = "Can't process as Penalty dedcuation Mode is 'Both', Penalty Amount and Interest Amount are tailed equally";
                            }
                            else if (interestamt == 0)
                            {
                                msg = "Can't process as Penalty dedcuation Mode is 'Both', Interest Amount is empty," +
                                                "You can deduct Penalty Amount directly from Principal Amount. Change deduction Penalty Mode as 'Deduct from Principal'";
                            }
                            else if (chargeamt <= interestamt)
                            {
                                msg = "Can't process as Penalty dedcuation Mode is 'Both', Penalty Amount is less than Interest Amount," +
                                                "You can deduct Penalty Amount directly from Interest/Principal Amount. Change deduction Penalty Mode as 'Deduct from Interest'";
                            }
                            else if (dedPreicipleAmount >= principalamt)
                            {
                                msg = "Can't process as Penalty dedcuation Mode is 'Both', " +
                                        "Remaining amount which is deducted from Interest Amount should be less than Withdrwal Amount";
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                result.Message = err.Message;
            }
            finally
            {
                if (!string.IsNullOrEmpty(msg))
                {
                    result.Message = msg;
                }
                else
                {
                    result.Success = true;
                }
            }

            return result;
        }

        private ResultArgs VoucherTrans(DataManager dataManagerVouTrans) // There
        {
            using (VoucherTransactionSystem voucherTrans = new VoucherTransactionSystem())
            {
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Database = dataManagerVouTrans.Database;
                    voucherTrans.AuthorizedStatus = AuthorizedStatus;
                    if (FDTransType != FDTypes.RN.ToString() && FDTransType != FDTypes.WD.ToString() && FDTransType != FDTypes.PWD.ToString() && FDTransType != FDTypes.POI.ToString())
                    {
                        //On 29/09/2017, to assing Matrilised date for bank ledgers
                        string MaterializedDate = string.Empty;
                        if (IsBankAccountLedger(CashBankId))
                        {
                            MaterializedDate = FDTransType != FDTypes.POI.ToString() ? DateSet.ToDate(CreatedOn.ToShortDateString()) : DateSet.ToDate(FDMaturityDate.ToShortDateString());
                        }

                        dtCashBankLedger.Rows.Add(CashBankId, ExchangeRate, LiveExchangeRate, FdAmount, CashBankLedgerAmt, CashBankLedgerGroup, "", MaterializedDate, LedgerNotes);
                        TransInfo = dtCashBankLedger.DefaultView;
                        dtFDLedger.Rows.Add(LedgerId, ExchangeRate, LiveExchangeRate, FdAmount, FDLedgerAmt, ledgerSubType.FD.ToString(), "", "", LedgerNotes);

                        CashTransInfo = dtFDLedger.DefaultView;
                        voucherTrans.FDType = FDTransType.Equals(FDTypes.RIN.ToString()) ? FDTypes.RIN.ToString() : FDTypes.IN.ToString();
                        FixedDepositInterestInfo = null;
                    }
                    else if (FDTransType == FDTypes.RN.ToString() || FDTransType == FDTypes.POI.ToString())
                    {
                        //On 29/09/2017, to assing Matrilised date for bank ledgers
                        string MaterializedDate = string.Empty;
                        if (IsBankAccountLedger(CashBankId))
                        {
                            //On 12/01/2024, to have proper materialized for POST Interest
                            //MaterializedDate = FDTransType != FDTypes.POI.ToString() ? DateSet.ToDate(CreatedOn.ToShortDateString()) : DateSet.ToDate(FDMaturityDate.ToShortDateString());
                            MaterializedDate = FDTransType != FDTypes.POI.ToString() ? DateSet.ToDate(CreatedOn.ToShortDateString()) : DateSet.ToDate(CreatedOn.ToShortDateString());
                        }

                        //dtCashBankLedger.Rows.Add(LedgerId, FDInterstCalAmount, CashBankLedgerAmt, CashBankLedgerGroup, "", "", LedgerNotes);
                        //dtCashBankLedger.Rows.Add(CashBankId, FDInterstCalAmount, FDLedgerAmt, TransactionMode.DR.ToString(), "", MaterializedDate, LedgerNotes);

                        //On 14/08/2018, to make TDS amount in Voucher entry --------------------------------------
                        if (this.TDSOnFDInterestLedgerId > 0 && FDTDSAmount > 0)
                        {
                            dtCashBankLedger.Rows.Add(CashBankId, ExchangeRate, LiveExchangeRate, FDInterstCalAmount, FDLedgerAmt, TransactionMode.DR.ToString(), "", MaterializedDate, LedgerNotes);
                            dtCashBankLedger.Rows.Add(this.TDSOnFDInterestLedgerId, ExchangeRate, LiveExchangeRate, FDTDSAmount, 0, TransactionMode.DR.ToString(), "", "", LedgerNotes);

                            dtCashBankLedger.Rows.Add(LedgerId, ExchangeRate, LiveExchangeRate, (FDInterstCalAmount + FDTDSAmount), CashBankLedgerAmt, CashBankLedgerGroup, "", "", LedgerNotes);
                        }
                        else
                        {
                            //On 27/10/2023, For Post interest, if fd is credit mode, change trans modes in finance vouchers
                            if (this.FDRenewalType.ToString().ToUpper() == FDRenewalTypes.ACI.ToString().ToUpper() &&
                                 FDRenewalTransMode.ToString().ToUpper() == TransSource.Cr.ToString().ToUpper() &&
                                (FDTransType == FDTypes.POI.ToString().ToUpper() || FDTransType == FDTypes.RN.ToString().ToUpper()))
                            {
                                dtCashBankLedger.Rows.Add(CashBankId, ExchangeRate, LiveExchangeRate, FDInterstCalAmount, FDLedgerAmt, TransactionMode.CR.ToString(), "", MaterializedDate, LedgerNotes);
                                dtCashBankLedger.Rows.Add(LedgerId, ExchangeRate, LiveExchangeRate, FDInterstCalAmount, CashBankLedgerAmt, TransactionMode.DR.ToString(), "", "", LedgerNotes);
                            }
                            else
                            {
                                dtCashBankLedger.Rows.Add(CashBankId, ExchangeRate, LiveExchangeRate, FDInterstCalAmount, FDLedgerAmt, TransactionMode.DR.ToString(), "", MaterializedDate, LedgerNotes);
                                dtCashBankLedger.Rows.Add(LedgerId, ExchangeRate, LiveExchangeRate, FDInterstCalAmount, CashBankLedgerAmt, CashBankLedgerGroup, "", "", LedgerNotes);
                            }
                        }
                        //-------------------------------------------------------------------------------------
                        voucherTrans.dtTransInfo = dtCashBankLedger;
                        voucherTrans.FDType = FDTransType == FDTypes.RN.ToString() ? FDTypes.RN.ToString() : FDTypes.POI.ToString();
                        FixedDepositInterestInfo = null;
                    }
                    else
                    {
                        //On 29/09/2017, to assing Matrilised date for bank ledgers
                        string MaterializedDate = string.Empty;
                        if (IsBankAccountLedger(CashBankId))
                        {
                            MaterializedDate = FDTransType != FDTypes.POI.ToString() ? DateSet.ToDate(CreatedOn.ToShortDateString()) : DateSet.ToDate(FDMaturityDate.ToShortDateString());
                        }

                        dtCashBankLedger.Rows.Add(FDLedgerId, ExchangeRate, LiveExchangeRate, FdAmount, CashBankLedgerAmt, TransactionMode.CR.ToString(), "", "", LedgerNotes);
                        TransInfo = dtCashBankLedger.DefaultView;

                        //On 20/05/2022, to Assign Pentaly details to Contra Voucher
                        if (FDTransType == FDTypes.WD.ToString() || FDTransType == FDTypes.PWD.ToString())
                        {
                            if (ChargeAmount > 0 && (ChargeMode == 2 || ChargeMode == 3) && ChargeLedgerId > 0) //amount, mode (deduct form principal and penalty ledger must be selected)
                            {
                                dtFDLedger.Rows.Add(ChargeLedgerId, ExchangeRate, LiveExchangeRate, (ChargeMode == 3 ? ChargeAmountByPrincipal : ChargeAmount), FDLedgerAmt,
                                        TransactionMode.DR.ToString(), string.Empty, string.Empty, string.Empty);
                                FdAmount -= (ChargeMode == 3 ? ChargeAmountByPrincipal : ChargeAmount); //ChargeAmount;
                            }
                        }

                        dtFDLedger.Rows.Add(CashBankId, ExchangeRate, LiveExchangeRate, FdAmount, FDLedgerAmt, TransactionMode.DR.ToString(), "", MaterializedDate, LedgerNotes);
                        CashTransInfo = dtFDLedger.DefaultView;

                        if (FDInterstCalAmount > 0 || FDIntrestVoucherId != 0)
                        {
                            DataTable dtReceiptEntry = dtCashBankLedger.Clone();
                            //dtReceiptEntry.Rows.Add(LedgerId, FDInterstCalAmount, CashBankLedgerAmt, TransactionMode.CR.ToString(), "", "", LedgerNotes);
                            //dtReceiptEntry.Rows.Add(CashBankId, FDInterstCalAmount, FDLedgerAmt, TransactionMode.DR.ToString(), "", MaterializedDate, LedgerNotes);

                            //On 14/08/2018, to make TDS amount in Receipt Voucher entry -------------------------------------------------------------------------
                            if (FDInterstCalAmount > 0 && ((this.TDSOnFDInterestLedgerId > 0 && FDTDSAmount > 0) ||
                               (this.ChargeMode == 1 && this.ChargeLedgerId > 0 && ChargeAmount > 0))) //02/12/2021, Dedcut from Penalty
                            {   //amount, mode (deduct form interest and penalty ledger must be selected)
                                dtReceiptEntry.Rows.Add(LedgerId, ExchangeRate, LiveExchangeRate, (FDInterstCalAmount + FDTDSAmount), CashBankLedgerAmt, TransactionMode.CR.ToString(), "", "", LedgerNotes);
                                if (this.TDSOnFDInterestLedgerId > 0 && FDTDSAmount > 0) //For TDS
                                {
                                    dtReceiptEntry.Rows.Add(this.TDSOnFDInterestLedgerId, ExchangeRate, LiveExchangeRate, FDTDSAmount, 0, TransactionMode.DR.ToString(), "", "", LedgerNotes);
                                }
                                if ((ChargeMode == 1 || ChargeMode == 3) && this.ChargeLedgerId > 0 && ChargeAmount > 0) //For Penalty amount
                                {
                                    dtReceiptEntry.Rows.Add(this.ChargeLedgerId, ExchangeRate, LiveExchangeRate, (ChargeMode == 3 ? ChargeAmountByInterest : ChargeAmount), 0, TransactionMode.DR.ToString(), string.Empty, string.Empty, string.Empty);
                                    FDInterstCalAmount -= ChargeAmount;
                                }

                                dtReceiptEntry.Rows.Add(CashBankId, ExchangeRate, LiveExchangeRate, FDInterstCalAmount, FDLedgerAmt, TransactionMode.DR.ToString(), "", MaterializedDate, LedgerNotes);
                            }
                            else
                            {
                                dtReceiptEntry.Rows.Add(LedgerId, ExchangeRate, LiveExchangeRate, FDInterstCalAmount, CashBankLedgerAmt, TransactionMode.CR.ToString(), "", "", LedgerNotes);
                                dtReceiptEntry.Rows.Add(CashBankId, ExchangeRate, LiveExchangeRate, FDInterstCalAmount, FDLedgerAmt, TransactionMode.DR.ToString(), "", MaterializedDate, LedgerNotes);
                            }
                            //------------------------------------------------------------------------------------------------------------------------------------

                            FixedDepositInterestInfo = dtReceiptEntry;
                        }
                        else
                        {
                            FixedDepositInterestInfo = null;
                        }
                        voucherTrans.FDType = FDTypes.WD.ToString();
                    }

                    voucherTrans.VoucherId = VoucherId;
                    voucherTrans.FDInterestVoucherId = FDInterstCalAmount > 0 ? FDIntrestVoucherId : 0;     // while making the amount 0 in the withdrawal and assign the values
                    //On 12/01/2024, to have proper materialized for POST Interest
                    //voucherTrans.VoucherDate = FDTransType != FDTypes.POI.ToString() ? CreatedOn : FDMaturityDate;
                    voucherTrans.VoucherDate = FDTransType != FDTypes.POI.ToString() ? CreatedOn : CreatedOn;

                    if (FDTransType == FDTypes.WD.ToString() || FDTransType == FDTypes.PWD.ToString())
                    {
                        voucherTrans.dteWithdrawalReceiptDate = dteWithdrawReceiptDate;

                        //On 06/10/2022, to keep fd withdrwal receipt voucher no
                        voucherTrans.FDWithdrwalReceiptVoucherNo = FDWithdrwalReceiptVoucherNo;
                    }
                    voucherTrans.VoucherType = VoucherType;
                    voucherTrans.VoucherDefinitionId = VoucherDefinitionId;
                    voucherTrans.FDTransType = FDTransType;
                    voucherTrans.TransVoucherMethod = TransVoucherMethod;
                    voucherTrans.VoucherSubType = LedgerTypes.FD.ToString();
                    voucherTrans.VoucherNo = FDVoucherNo;
                    voucherTrans.ProjectId = ProjectId;
                    voucherTrans.InterestType = VoucherIntrestMode == 1 ? true : false;
                    voucherTrans.Status = 1;
                    voucherTrans.CreatedOn = DateTime.Now;
                    voucherTrans.ModifiedOn = DateTime.Now;
                    voucherTrans.CreatedBy = NumberSet.ToInteger(LoginUserId.ToString());
                    voucherTrans.ModifiedBy = NumberSet.ToInteger(LoginUserId.ToString());
                    voucherTrans.Narration = LedgerNotes;

                    //On 15/10/2024, To set currency details 
                    voucherTrans.CurrencyCountryId = 0;
                    voucherTrans.ContributionAmount = 0;
                    voucherTrans.ExchangeRate = 1;
                    voucherTrans.CalculatedAmount = 0;
                    voucherTrans.ActualAmount = 0;

                    if (this.AllowMultiCurrency == 1)
                    {
                        voucherTrans.CurrencyCountryId = voucherTrans.ExchageCountryId = CurrencyCountryId;
                        voucherTrans.ContributionAmount = ContributionAmount;
                        voucherTrans.ExchangeRate = ExchangeRate;
                        voucherTrans.CalculatedAmount = CalculatedAmount;
                        voucherTrans.ActualAmount = ActualAmount;
                    }

                    if (FDTransType != FDTypes.RN.ToString() && FDTransType != FDTypes.POI.ToString())
                    {
                        resultArgs = voucherTrans.SaveVoucherDetails(dataManager);
                        //FDVoucherId = voucherTrans.FDVoucherId;
                        FDWithdrawalContraVoucherId = voucherTrans.FDVoucherId;
                        FDWithdrawalInterestVoucherId = voucherTrans.FDInterestVoucherId;
                    }
                    else
                    {
                        resultArgs = voucherTrans.SaveFixedDepositInterest(dataManager);
                        FDRenewalInterestVoucherId = voucherTrans.FDInterestVoucherId;
                    }
                }
            }
            return resultArgs;
        }

        private double FetchLedgerBalance(int ProjId, int LedId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchLedgerBalance))
            //   using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchOpBalanceList))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.PROJECT_IDColumn, ProjId);
                dataManager.Parameters.Add(this.AppSchema.FDAccount.LEDGER_IDColumn, LedId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return NumberSet.ToDouble(resultArgs.DataSource.Data.ToString());
        }

        public ResultArgs RemoveFDAccountDetails()
        {
            string FDVouId = string.Empty;

            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = DeleteFdAccountDetails();


                if (FDRenewalType == FDTypes.RN.ToString() || FDRenewalType == FDTypes.WD.ToString())
                {
                    FDVouId = FetchVoucherID();
                }

                if (FDVouId == string.Empty)
                {
                    if (resultArgs.Success)
                    {
                        if (FDTransType == FDTypes.OP.ToString())
                        {
                            using (BalanceSystem balanceSystem = new BalanceSystem())
                            {
                                resultArgs = balanceSystem.UpdateOpBalance(FDOPInvestmentDate, ProjectId, LedgerId, 0, "DR", TransactionAction.EditAfterSave);
                                FdAmount = FetchLedgerBalance(ProjectId, LedgerId);
                                resultArgs = balanceSystem.UpdateOpBalance(FDOPInvestmentDate, ProjectId, LedgerId, FdAmount, "DR", TransactionAction.New);
                            }
                        }
                        else
                        {
                            using (VoucherTransactionSystem voucherTransSystem = new VoucherTransactionSystem())
                            {
                                voucherTransSystem.VoucherId = FDVoucherId;
                                resultArgs = voucherTransSystem.RemoveVoucher(dataManager);
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    resultArgs = voucherTransSystem.FillVoucherDetails(voucherTransSystem.VoucherId);
                                    if (resultArgs != null && resultArgs.Success)
                                    {
                                        resultArgs = voucherTransSystem.FetchVoucherNumberDefinition();
                                        if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                                        {
                                            TransVoucherMethod = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Voucher.VOUCHER_METHODColumn.ColumnName].ToString());
                                            if (TransVoucherMethod == (int)TransactionVoucherMethod.Automatic)
                                            {
                                                resultArgs = voucherTransSystem.RegenerateVoucherNumbers(dataManager, this.DateSet.ToDate(FDOPInvestmentDate, false), this.DateSet.ToDate(FDOPInvestmentDate, false));
                                            }
                                        }
                                    }
                                }
                                if (!resultArgs.Success)
                                {
                                    dataManager.TransExecutionMode = ExecutionMode.Fail;
                                }
                            }
                        }
                    }
                }
                else
                {
                    string[] ArrayOfFDVoucherID = FDVouId.Split(',');
                    if (resultArgs.Success)
                    {
                        if (FDTransType == FDTypes.OP.ToString())
                        {
                            using (BalanceSystem balanceSystem = new BalanceSystem())
                            {
                                resultArgs = balanceSystem.UpdateOpBalance(FDOPInvestmentDate, ProjectId, LedgerId, 0, FDTransMode, TransactionAction.EditAfterSave);
                                FdAmount = FetchLedgerBalance(ProjectId, LedgerId);
                                resultArgs = balanceSystem.UpdateOpBalance(FDOPInvestmentDate, ProjectId, LedgerId, FdAmount, FDTransMode, TransactionAction.New);

                                for (int i = 0; i < ArrayOfFDVoucherID.Length; i++)
                                {
                                    using (VoucherTransactionSystem voucherTransSystem = new VoucherTransactionSystem())
                                    {
                                        voucherTransSystem.VoucherId = NumberSet.ToInteger(ArrayOfFDVoucherID[i].ToString());
                                        resultArgs = voucherTransSystem.RemoveVoucher(dataManager);
                                    }
                                }
                            }
                        }
                        else
                        {
                            using (VoucherTransactionSystem voucherTransSystem = new VoucherTransactionSystem())
                            {
                                voucherTransSystem.VoucherId = FDVoucherId;
                                resultArgs = voucherTransSystem.RemoveVoucher(dataManager);
                                for (int i = 0; i < ArrayOfFDVoucherID.Length; i++)
                                {
                                    voucherTransSystem.VoucherId = NumberSet.ToInteger(ArrayOfFDVoucherID[i].ToString());
                                    resultArgs = voucherTransSystem.RemoveVoucher(dataManager);
                                }
                            }
                        }
                    }
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs DeleteFdAccountDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.DeleteFDAcountDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private string FetchVoucherID()
        {
            string FDVoucherId = string.Empty;
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchVoucherId))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                    {
                        FDVoucherId += dr[this.AppSchema.FDAccount.FD_VOUCHER_IDColumn.ColumnName] != null ? dr[this.AppSchema.FDAccount.FD_VOUCHER_IDColumn.ColumnName].ToString() + "," : string.Empty;

                    }
                    FDVoucherId = FDVoucherId.TrimEnd(',');
                }
            }
            return FDVoucherId;
        }
        #endregion

        #region FD Renewal  Details
        public ResultArgs SaveFdRenewalDetails(DataManager dataManagerVouchers)
        {


            using (DataManager dataManager = new DataManager())
            {
                dataManager.Database = dataManagerVouchers.Database;
                using (VoucherTransactionSystem voucherTransSystem = new VoucherTransactionSystem())
                {
                    if (FDType == FDTypes.RN.ToString() || FDType == FDTypes.POI.ToString())
                    {
                        if (FDInterstCalAmount > 0 || FDIntrestVoucherId != 0 || ReInvestmentAmount != 0)
                        {
                            resultArgs = VoucherTrans(dataManager);
                            if (resultArgs.Success)
                            {
                                resultArgs = SaveFdDetails(dataManager);
                            }
                        }
                        else
                        {
                            resultArgs = SaveFdDetails(dataManager);
                        }
                    }
                    else
                    {
                        resultArgs = VoucherTrans(dataManager);
                        if (resultArgs.Success)
                        {
                            FDVoucherId = VoucherId != 0 ? VoucherId : FDVoucherId;
                            resultArgs = SaveFdDetails(dataManager);
                        }
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs SaveFdDetails(DataManager dataFDRenewal)
        {
            using (DataManager dataManager = new DataManager(FDRenewalId == 0 ? SQLCommand.FDRenewal.Add : SQLCommand.FDRenewal.Update))
            {
                dataManager.Database = dataFDRenewal.Database;
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_RENEWAL_IDColumn, FDRenewalId);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn, FDAccountId);

                //On 18/12/2020, For Reinvestment (Needs FD Ledger Id for Voucher Trans not for FD renewals)
                //dataManager.Parameters.Add(this.AppSchema.FDRenewal.INTEREST_LEDGER_IDColumn, LedgerId);
                if (FDRenewalType.ToUpper() == FDRenewalTypes.RIN.ToString())
                {
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.INTEREST_LEDGER_IDColumn, "0");
                }
                else
                {
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.INTEREST_LEDGER_IDColumn, LedgerId);
                }
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.BANK_LEDGER_IDColumn, FDRenewalType == FDRenewalTypes.ACI.ToString() ? 0 : CashBankId);
                //if (FDRenewalId == 0)
                //{
                //    if (FDType == FDTypes.WD.ToString() && FDInterstCalAmount <= 0.0)
                //    {
                //        // dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn, FDTransType == FDTypes.RN.ToString() ? FDVoucherId : 0);
                //    }
                //    else
                //    {
                //        // dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn, FDTransType == FDTypes.RN.ToString() ? FDVoucherId : FDRenewalInterestId);
                //    }
                //}
                //else
                //{
                //if (FDType == FDTypes.WD.ToString())
                //{
                //    dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn, FDVoucherId);
                //}
                //else
                //{
                //  dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn, FDIntrestVoucherId);
                //}
                //}

                if (FDType.Equals(FDTypes.RIN.ToString()))
                {
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn, "0");
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_VOUCHER_IDColumn, FDWithdrawalContraVoucherId);
                }
                if (FDType.Equals(FDTypes.WD.ToString()) || FDType.Equals(FDTypes.PWD.ToString()))
                {
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn, FDWithdrawalInterestVoucherId);
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_VOUCHER_IDColumn, FDWithdrawalContraVoucherId);
                }
                else if (FDType.Equals(FDTypes.RN.ToString()) || FDType.Equals(FDTypes.POI.ToString()))
                {
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn, FDRenewalInterestVoucherId = IntrestAmount == 0 ? 0 : FDRenewalInterestVoucherId);  // by aldrin when interest amount is greater than 0 update teh FD Voucher id if not update 0.
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_VOUCHER_IDColumn, FDRenewalInterestVoucherId = IntrestAmount == 0 ? 0 : FDRenewalInterestVoucherId); // by aldrin when interest amount is greater than 0 update teh FD Voucher id if not update 0.
                }
                //On 23/05/2022, To have Penalty Amount
                if (ChargeMode > 0 && ChargeAmount > 0 && ChargeLedgerId > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.CHARGE_MODEColumn, ChargeMode);
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.CHARGE_AMOUNTColumn, ChargeAmount);
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.CHARGE_LEDGER_IDColumn, ChargeLedgerId);
                }
                else
                {
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.CHARGE_MODEColumn, NumberSet.ToInteger("0"));
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.CHARGE_AMOUNTColumn, NumberSet.ToDouble("0"));
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.CHARGE_LEDGER_IDColumn, NumberSet.ToDouble("0"));
                }

                //On 27/10/2023, To update FD Renewal Trans Mode- Post Interest (accumulated) Wtihdrwal
                string renewaltransmode = TransSource.Dr.ToString();
                if ((FDType.Equals(FDTypes.POI.ToString()) || FDType.Equals(FDTypes.RN.ToString())) && VoucherIntrestMode == 1 && FDRenewalTransMode == TransSource.Cr)
                {
                    renewaltransmode = FDRenewalTransMode.ToString();
                }
                else if (FDType.Equals(FDTypes.WD.ToString()) || FDType.Equals(FDTypes.PWD.ToString()))
                {
                    renewaltransmode = TransSource.Cr.ToString();
                }
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_TRANS_MODEColumn, renewaltransmode.ToUpper());

                dataManager.Parameters.Add(this.AppSchema.FDRenewal.CLOSED_DATEColumn, ClosedDate);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.RENEWAL_DATEColumn, CreatedOn);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.MATURITY_DATEColumn, FDMaturityDate);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.INTEREST_AMOUNTColumn, IntrestAmount);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.WITHDRAWAL_AMOUNTColumn, WithdrawAmount);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.REINVESTED_AMOUNTColumn, ReInvestmentAmount);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.TDS_AMOUNTColumn, FDTDSAmount);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.EXPECTED_MATURITY_VALUEColumn, FDExpectedRenewMaturityValue);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.INTEREST_RATEColumn, FDInterestRate);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.INTEREST_TYPEColumn, InterestType);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.RECEIPT_NOColumn, ReceiptNo);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.PRINCIPAL_AMOUNTColumn, PrinicipalAmount);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.RENEWAL_TYPEColumn, FDRenewalType);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.STATUSColumn, (int)YesNo.Yes);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.IS_DELETEDColumn, (int)YesNo.Yes);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_TYPEColumn, FDType);

                dataManager.Parameters.Add(this.AppSchema.FDRenewal.EXPECTED_INTEREST_VALUEColumn, ExpectedInterestAmount);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs GetLastRenewalDate()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.GetLastFDRenewalDate))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs GetMaturityValue()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.GetMaturityValue))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_RENEWAL_IDColumn, FDRenewalId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public double FetchAccumulatedAmount()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchAccumulatedAmount))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Data != null ? NumberSet.ToDouble(resultArgs.DataSource.Data.ToString()) : 0;
        }

        public double FetchWithdrawalAmount(int Status)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchWithdrawAmount))
            {
                //if (Status == 1)
                //{
                //    dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                //    dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_RENEWAL_IDColumn, FDRenewalId);
                //}
                //else
                //{
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                // }
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Data != null ? NumberSet.ToDouble(resultArgs.DataSource.Data.ToString()) : 0;
        }

        public double FetchWithdrawalAmount()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchWithdrawAmount))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                //  dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_RENEWAL_IDColumn, FDRenewalId);
                if (FDVoucherId > 0)
                {
                    //temp
                    // On 07/09/2022, since existing, they dont get withdrwal/partial amount when we edit from voucher view receipt screen
                    // modify receipt interest voucher
                    if (FDContraVoucherId > 0)
                    {
                        dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_VOUCHER_IDColumn, FDContraVoucherId);
                    }
                    else
                    {
                        dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_VOUCHER_IDColumn, FDVoucherId);
                    }
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn, FDVoucherId);
                }
                else
                {
                    if (FDContraVoucherId > 0) //on 12/10/2022, to allow modify partially withdrwal from fd module
                    {
                        dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_VOUCHER_IDColumn, FDContraVoucherId);
                        dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn, FDContraVoucherId);
                    }
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Data != null ? NumberSet.ToDouble(resultArgs.DataSource.Data.ToString()) : 0;
        }

        public double FetchFullWithdrawal()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchWithdrawAmountUptoCurrent))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.RENEWAL_DATEColumn, RenewedDate);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Data != null ? NumberSet.ToDouble(resultArgs.DataSource.Data.ToString()) : 0;
        }

        private ResultArgs UpdateFdAccountStatus(DataManager closeFDDataManager)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.UpdateFDStatus))
            {
                dataManager.Database = closeFDDataManager.Database;
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchFDAccountId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchAccountIdByVoucherId))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn, VoucherId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// fetches the deleted Records which has renewal, withdrawl, 
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchPhysicalFDAccountId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchPhysicalAccountIdbyVoucherId))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchRenewalDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchRenewalDetailsById))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn, FDVoucherId);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.TDS_ON_FDINTEREST_LEDGER_IDColumn, this.TDSOnFDInterestLedgerId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int FetchFDStatus()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchFDStatus))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int CountRenewalDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.CountFDRenewalDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public double FetchAccumulatedInterestAmount()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchACIAmount))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.RENEWAL_DATEColumn, RenewedDate);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn, FDAccountId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Data != DBNull.Value ? NumberSet.ToDouble(resultArgs.DataSource.Data.ToString()) : 0;
        }

        public double FetchReInvestedAmount()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchRINAmount))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.RENEWAL_DATEColumn, RenewedDate);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn, FDAccountId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Data != DBNull.Value ? NumberSet.ToDouble(resultArgs.DataSource.Data.ToString()) : 0;
        }

        public int FetchVoucherId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.FetchVoucherID))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_RENEWAL_IDColumn, FDRenewalId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int FetchFDAccountIdByRenewalId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.FetchFDAccountIdByRenewalId))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_RENEWAL_IDColumn, FDRenewalId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs DeleteFDRenewals()
        {

            //On 21/10/2022, To check FD Validation
            FDAccountId = FetchFDAccountIdByRenewalId();
            resultArgs = ValidateFDAccount(FDAccountId, FDRenewalId, CreatedOn, FDMaturityDate, FDType, FDRenewalType, InterestType, IntrestAmount, ReInvestmentAmount, WithdrawAmount, FDRenewalMode.Delete);

            if (resultArgs.Success)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.Delete))
                {
                    dataManager.BeginTransaction();

                    FDVoucherId = FetchVoucherId();
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_RENEWAL_IDColumn, FDRenewalId);
                    resultArgs = dataManager.UpdateData();
                    if (resultArgs.Success)
                    {
                        using (VoucherTransactionSystem voucherTrans = new VoucherTransactionSystem())
                        {
                            voucherTrans.VoucherId = FDVoucherId;
                            resultArgs = voucherTrans.RemoveVoucher(dataManager);
                            if (resultArgs != null && resultArgs.Success)
                            {
                                resultArgs = voucherTrans.FillVoucherDetails(FDVoucherId);
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    if (RenewedDate != DateTime.MinValue)
                                    {
                                        if (this.NumberSet.ToInteger(this.EnableVoucherRegenerationDeletion) == (int)YesNo.Yes)
                                        {
                                            resultArgs = voucherTrans.RegenerateVoucherNumbers(dataManager, RenewedDate, RenewedDate);
                                        }
                                    }
                                }

                                //12/10/2022, For removing withdrwal interest voucher id 
                                if (resultArgs.Success && FDWithdrwalInterestVoucherId > 0)
                                {
                                    voucherTrans.VoucherId = FDWithdrwalInterestVoucherId;
                                    resultArgs = voucherTrans.RemoveVoucher(dataManager);
                                    if (resultArgs != null && resultArgs.Success)
                                    {
                                        resultArgs = voucherTrans.FillVoucherDetails(FDWithdrwalInterestVoucherId);
                                        if (resultArgs != null && resultArgs.Success)
                                        {
                                            if (RenewedDate != DateTime.MinValue)
                                            {
                                                if (this.NumberSet.ToInteger(this.EnableVoucherRegenerationDeletion) == (int)YesNo.Yes)
                                                {
                                                    resultArgs = voucherTrans.RegenerateVoucherNumbers(dataManager, RenewedDate, RenewedDate);
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (!resultArgs.Success)
                            {
                                dataManager.TransExecutionMode = ExecutionMode.Fail;
                            }

                        }
                    }

                    dataManager.EndTransaction();
                }
            }
            return resultArgs;
        }

        public ResultArgs DeleteFDRenewalsByVoucherId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.DeleteFDRenewal))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_VOUCHER_IDColumn, FDVoucherId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// Delete the Fd Renewal physically from DataBase
        /// </summary>
        /// <returns></returns>
        public ResultArgs DeleteFDPhysicalRenewalsByVoucherId()
        {
            //foreach (int id in FdVoucherIdList)
            //{
            //    int FDVouId = id;
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.DeleteFDPhysicalReneval))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_VOUCHER_IDColumn, FDVoucherId);
                resultArgs = dataManager.UpdateData();
            }
            // }
            return resultArgs;
        }
        /// <summary>
        /// Fetch the FD Records
        /// </summary>
        /// <returns></returns>
        public int FetchFDAId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.FetchFDAccountId))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_VOUCHER_IDColumn, FDVoucherId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int CheckFDAccountExists()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.CheckFDAccountExists))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        /// <summary>
        /// check whether exists or not
        /// </summary>
        /// <returns></returns>
        public int CheckPhysicalFDAccountExists()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.CheckPhysicalAccountExists))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        public ResultArgs DeleteFDAccountDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.DeleteFDAccount))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_VOUCHER_IDColumn, FDVoucherId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// Delete the fd Accounts physically
        /// </summary>
        /// <returns></returns>
        public ResultArgs DeleteFDPhysicalAccountDetails()
        {
            //foreach (int Id in FdVoucherIdList)
            //{
            // int FdVouId = Id;
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.DeleteFDPhysicalAccount))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_VOUCHER_IDColumn, FDVoucherId);
                resultArgs = dataManager.UpdateData();
            }
            //}
            return resultArgs;
        }

        /// <summary>
        /// Delete Opening Details 
        /// </summary>
        /// <returns></returns>
        public ResultArgs DeleteFDPhysicalOpeningDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.DeleteFDPhysicalOpeningAccount))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        /// <summary>
        /// Delete the Fd renewal details from DB physically
        /// </summary>
        /// <returns></returns>
        public ResultArgs DeleteFDPhysicalRenewalDetails()
        {
            using (DataManager dataManger = new DataManager(SQLCommand.FDRenewal.DeleteFDPhysicalRenewalAccount))
            {
                dataManger.Parameters.Add(this.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManger.UpdateData();

            }
            return resultArgs;
        }
        public DataTable FetchFDRenewalType()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.FetchRenewalType))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_VOUCHER_IDColumn, FDVoucherId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs.DataSource.Table;
        }

        /// <summary>
        /// get Deleted Renewal type
        /// </summary>
        /// <returns></returns>
        public DataTable FetchPhysicalFDRenewalType()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.FetchDeletedRenewalType))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_VOUCHER_IDColumn, FDVoucherId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs.DataSource.Table;
        }


        public ResultArgs UpdateFDAccountStatus()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.UpdateFDAccountStatus))
            {

                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public int CheckFDRenewalClosed()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.CheckFDClosed))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int CheckFDPhysicalRenewalClosed()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.CheckFDPhysicalClosd))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }


        public string CheckRenewalTypeByVoucherId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.CheckByVoucherId))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Data != null ? resultArgs.DataSource.Data.ToString() : string.Empty;
        }

        public ResultArgs FetchRenewalByVoucherId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.FetchRenewalByVoucherId))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchFDAccountByVoucherId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.FetchFDAccountByVoucherId))
            {
                if (FDType == FDTypes.OP.ToString())
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, VoucherId);
                else
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchActualFDAccountByVoucherId(Int32 Vid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchActualFDAccountByVoucherId))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_VOUCHER_IDColumn, Vid);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int CheckFDWithdrawal()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.CheckFDRenewalWithdraw))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int HasFDRenewal()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.HasFDRenewal))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int HasFDReInvestment()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.HasFDReInvestmentByFDAccountId))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int HasFDWithdrawal()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.HasFDWithdrawal))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn, FDAccountId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int HasFDPartialwithdrawal()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.HasFDPartialwithdrawal))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }


        public int HasFDAccount()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.HasFDAccount))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_NUMBERColumn, FDAccountNumber);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs UpdateFDInvestmentScheme(int fdscheme)
        {
            bool reinvestmentexists = true;
            if (FDAccountId > 0)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.HasFDReInvestmentByFDAccountId))
                {
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn, FDAccountId);
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
                reinvestmentexists = (resultArgs.Success && resultArgs.DataSource.Sclar != null && resultArgs.DataSource.Sclar.ToInteger > 0);

                if (!reinvestmentexists)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.UpdateFDScheme))
                    {
                        dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn, FDAccountId);
                        dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_SCHEMEColumn, fdscheme);
                        resultArgs = dataManager.UpdateData();
                    }
                }
                else if (fdscheme == 0)
                {
                    resultArgs.Message = "Re-Investment is made, can not change the Investment scheme.";
                }
            }
            else
            {
                resultArgs.Message = "Investment Account is not found.";
            }

            return resultArgs;
        }

        public ResultArgs DeleteFDAccountByVoucherId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.DeleteFDAccountByVoucherId))
            {
                dataManager.BeginTransaction();
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_VOUCHER_IDColumn, FDVoucherId);
                resultArgs = dataManager.UpdateData();
                if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    using (VoucherTransactionSystem voucherTrans = new VoucherTransactionSystem())
                    {
                        voucherTrans.VoucherId = FDVoucherId;
                        resultArgs = voucherTrans.RemoveVoucher(dataManager);
                    }
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        /// <summary>
        /// chinna
        /// </summary>
        /// <returns></returns>
        public ResultArgs DeletePhysicalFDAccountByVoucherId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.DeletePhysicalFDAccountByVoucherId))
            {
                dataManager.BeginTransaction();
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_VOUCHER_IDColumn, FDVoucherId);
                resultArgs = dataManager.UpdateData();
                if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    using (VoucherTransactionSystem voucherTrans = new VoucherTransactionSystem())
                    {
                        voucherTrans.VoucherId = FDVoucherId;
                        resultArgs = voucherTrans.RemoveVoucher(dataManager);
                    }
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public ResultArgs FetchVoucherByFDAccount()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchVoucherByAccount))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        #endregion

        #region PostInterestmethods
        public ResultArgs GetLastRenewalIdByFDAccount()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.GetLastRenewalIdByFDAccountId))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            FDLastRenewalId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["MAX_FD_RENEWAL_ID"].ToString());
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.GetLastRenwalDetailsByRenewalId))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_RENEWAL_IDColumn, FDLastRenewalId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs GetPostInterestIdByFDAccount()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.GetLastPostInterestIdByFDAccountId))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            FDLastRenewalId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["MAX_FD_RENEWAL_ID"].ToString());
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.GetLastPostInterestDetailsByRenewalId))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_RENEWAL_IDColumn, FDLastRenewalId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int HasFDPostInterests()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.HasFDPostInterests))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs FetchFDAccountDetailsByFDAccountId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.FetchFDAccountDetailsByFDAccountID))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs GetNoOfPostInterests()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.GetNoOfPostInterest))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int GetNoOfPostInterestsCount()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.GetNoOfPostInterestCount))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs GetNoOfPostInterestsBayDateRange(DateTime deRenewOndate)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.GetNoOfPostInterestByDateRange))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.RENEWAL_DATEColumn, deRenewOndate);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs GetNoOfReInvestment()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.GetNoofReInvestment))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs GetmaxRenewals(DateTime dtMaxDate)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.GetMaxFDRenewal))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FDAccountId);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.RENEWAL_DATEColumn, dtMaxDate);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchFDWithDrawalDetails(string FD_ACCOUNT_ID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchFDWithdrawalsByFDAccountId))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FD_ACCOUNT_ID);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchFDReInvestmentDetails(string FD_ACCOUNT_ID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchFDReInvestmentByFDAccountId))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FD_ACCOUNT_ID);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchCurrentPrincipalAmount(string FD_ACCOUNT_ID, string Renewaldate)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchPrinicpalAmountBydate))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, FD_ACCOUNT_ID);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.RENEWAL_DATEColumn, Renewaldate);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteIntrestVoucher(int FDVoucherId)
        {
            ResultArgs result = new ResultArgs();
            result.Success = true;
            using (DataManager dataManager = new DataManager())
            {
                using (BalanceSystem balanceSystem = new BalanceSystem())
                {
                    //dataManager.BeginTransaction();
                    result = balanceSystem.UpdateTransBalance(FDVoucherId, TransactionAction.Cancel); // By aldrin the amount is not refreshed while update.
                    if (result.Success)
                    {
                        result = DeleteIntrestVouchersinVoucherTrans(FDVoucherId);
                        if (result.Success)
                        {
                            result = DeleteIntrestVouchersinVoucherMasterTrans(FDVoucherId);
                        }
                        if (result.Success)
                        {
                            result = UpdateFDRenewalVoucherIdBYZero(FDVoucherId);  // This is to update the voucher Id and amount is 0 in the Fd Renewal (chinna:20.04.2017)
                        }
                    }
                    //dataManager.EndTransaction();
                }
            }
            return result;

        }

        private ResultArgs DeleteIntrestVouchersinVoucherTrans(int FDVoucherId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.DeleteIntrestVouchersinVoucherTrans))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, FDVoucherId);
                // dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_SUB_TYPEColumn, VoucherSubTypes.FD);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteIntrestVouchersinVoucherMasterTrans(int FDVoucherId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.DeleteIntrestVouchersinVoucherMasterTrans))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, FDVoucherId);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_SUB_TYPEColumn, VoucherSubTypes.FD);
                //dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs UpdateFDRenewalVoucherIdBYZero(int FDVId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.UpdateFDRenewalVoucherIdByZero))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, FDVId);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.INTEREST_AMOUNTColumn, IntrestAmount);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        #endregion

        /// <summary>
        /// This mehtod is used to check given ledger is bank account ledger
        /// </summary>
        /// <param name="CashBankLedgerId"></param>
        /// <returns></returns>
        public bool IsBankAccountLedger(Int32 CashBankLedgerId)
        {
            bool Rtn = false;
            using (LedgerSystem ledgersystem = new LedgerSystem())
            {
                ledgersystem.LedgerId = CashBankLedgerId;
                int bankledger = ledgersystem.FetchLedgerGroupById();
                Rtn = (bankledger == (Int32)FixedLedgerGroup.BankAccounts);
            }
            return Rtn;
        }

        /// <summary>
        /// This method is used to delete given FD Account's history (starting from its creation to widthdwal)
        /// 
        /// after deleting refesh balance, since we dont how many contra vouchers and interest vouchers,
        /// so we refesh balance from its creation date
        /// </summary>
        /// <param name="FDAccount"></param>
        /// <param name="ProjectId"></param>
        /// <param name="FDCreatedOn"></param>
        /// <returns></returns>
        public ResultArgs DropFDAccountHistory(int FDAccount, Int32 ProjectId, string FDCreatedOn)
        {
            ResultArgs result = new ResultArgs();
            DateTime FDLastDate = DateSet.ToDate(FDCreatedOn, false);
            result = FetchFDHistoryByFDId(FDAccount.ToString());
            if (result.Success && result.DataSource.Table != null)
            {
                DataTable dtRenewals = result.DataSource.Table;
                if (dtRenewals != null && dtRenewals.Rows.Count > 0)
                {
                    //Last date of Renewals
                    FDLastDate = DateSet.ToDate(dtRenewals.Compute("MAX(RENEWAL_DATE)", string.Empty).ToString(), false);
                }
            }

            if (!IsAuditVouchersLockedVoucherDate(ProjectId, DateSet.ToDate(FDCreatedOn, false), FDLastDate))
            {
                using (DataManager dataManager = new DataManager())
                {
                    result = dataManager.UpdateData("CALL DROPFD(" + FDAccount + ")");
                }

                if (result.Success)
                {
                    using (BalanceSystem balanceSystem = new BalanceSystem())
                    {
                        balanceSystem.ProjectId = ProjectId;
                        balanceSystem.VoucherDate = DateSet.ToDate(FDCreatedOn, false).ToShortDateString();
                        result = balanceSystem.UpdateBulkTransBalance();
                    }
                    if (result.Success)
                    {
                        result.Message = "Removed";
                        result.Success = true;
                    }
                    else
                    {
                        result.Message = "Removed, but balance is not refreshed";
                    }
                }
            }

            return result;
        }


        /// <summary>
        /// This method is used to check reivestment exists
        /// </summary>
        /// <returns></returns>
        public bool HasFlxiFD()
        {
            bool Rtn = false;
            ResultArgs resultArgs = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.HasFlxiFD))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }

            if (resultArgs.Success)
            {
                Int32 noofReinvested = resultArgs.DataSource.Sclar.ToInteger;
                Rtn = (noofReinvested > 0);
            }
            else
            {
                Rtn = true; //if any issues comes, return to lock (exists) 
            }

            return Rtn;
        }

        /// <summary>
        /// This method is used to check fd adjustment vouchers exists
        /// </summary>
        /// <returns></returns>
        public bool HasFDAdjustmentEntries()
        {
            bool Rtn = false;
            ResultArgs resultArgs = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.HasFDAdjustmentEntry))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }

            if (resultArgs.Success)
            {
                Int32 nooffdadjustmententries = resultArgs.DataSource.Sclar.ToInteger;
                Rtn = (nooffdadjustmententries > 0);
            }
            else
            {
                Rtn = true; //if any issues comes, return to lock (exists) 
            }

            return Rtn;
        }

        /// <summary>
        /// on 14/12/2020, This method is used to change project for existing FD Account
        /// 1. Map FD ledgers, 
        /// 2. Update FD invested/interest voucher/withdrwa voucher. 
        /// 3. update fd opening balance
        /// 4. Refresh both projects for fd start date (ivested or opening date)
        /// </summary>
        /// <returns></returns>
        public ResultArgs ChangeFDProject(Int32 fdaccountid, string fdaccountnumber, FDTypes fdtype, Int32 projectid, Int32 newprojectid, Int32 fdledgerid)
        {
            ResultArgs result = new ResultArgs();
            if ((!string.IsNullOrEmpty(fdaccountnumber)) && projectid > 0 && newprojectid > 0 && fdledgerid > 0 && fdaccountid > 0)
            {
                string openingdate = DateSet.ToDate(this.BookBeginFrom, false).AddDays(-1).ToShortDateString();
                SQLCommand.FixedDeposit changefdproject = SQLCommand.FixedDeposit.ChangeProjectForOPFD;
                if (fdtype == FDTypes.IN)
                {
                    changefdproject = SQLCommand.FixedDeposit.ChangeProjectForInvestmentFD;
                }
                using (DataManager dataManager = new DataManager(changefdproject))
                {
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn, fdaccountid);
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_NUMBERColumn, fdaccountnumber);
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.PROJECT_IDColumn, projectid);
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.NEW_PROJECT_IDColumn, newprojectid);
                    dataManager.Parameters.Add(this.AppSchema.FDAccount.LEDGER_IDColumn, fdledgerid);
                    dataManager.Parameters.Add(this.AppSchema.FDRenewal.FD_TYPEColumn, fdtype);
                    dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BALANCE_DATEColumn, openingdate);

                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.MODIFIED_BYColumn, ModifiedBy);
                    dataManager.Parameters.Add(this.AppSchema.User.USER_NAMEColumn, ModifiedByName);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    result = dataManager.UpdateData();
                }
            }
            else
            {
                result.Message = "Invalid FD details";
            }
            return result;
        }


        /// <summary>
        /// On 09/02/2021, To check FD Vouchers or Its renewasl exists during given period and given project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="datefrom"></param>
        /// <param name="dateto"></param>
        /// <returns></returns>
        public bool IsFDVouchersExists(Int32 projectId, int BankLedgerId, string datefrom, string dateto)
        {
            bool rtn = true;
            resultArgs = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.FixedDeposit.IsFDVouchersExists))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.PROJECT_IDColumn, projectId);
                dataManager.Parameters.Add(this.AppSchema.FDRegisters.DATE_FROMColumn, datefrom);
                dataManager.Parameters.Add(this.AppSchema.FDRegisters.DATE_TOColumn, dateto);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, BankLedgerId);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);

                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    rtn = (resultArgs.DataSource.Table.Rows.Count > 0);
                }
            }

            return rtn;
        }

        /// <summary>
        /// On 08/05/2024, To get FD Accoutns based on Investment Type exists
        /// </summary>
        public ResultArgs FetchFDAccountsExistsByInvestmentType(Int32 fdinvestmenttype)
        {
            resultArgs = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.FixedDeposit.FetchFDAccountsExistsByInvestmentType))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.FD_INVESTMENT_TYPE_IDColumn, fdinvestmenttype);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }

        /// <summary>
        /// On 14/05/2024, Get FD by FD ledger id
        /// </summary>
        /// <param name="fdinvestmenttype"></param>
        /// <returns></returns>
        public ResultArgs FetchByLedgerId(Int32 lid)
        {
            resultArgs = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.FDAccount.FetchByLedgerId))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, lid);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }

        /// <summary>
        /// On 08/05/2024, To check Investment Type exists
        /// </summary>
        public bool IsFDAccountsExistsByInvestmentType(FDInvestmentType fdinvestmenttype)
        {
            bool rtn = true;
            resultArgs = new ResultArgs();
            resultArgs = FetchFDAccountsExistsByInvestmentType((Int32)fdinvestmenttype);

            if (resultArgs.Success && resultArgs.DataSource.Table != null)
            {
                rtn = (resultArgs.DataSource.Table.Rows.Count > 0);
            }

            return rtn;
        }

        /// <summary>
        /// On 29/09/2022, To fetch renewals and withdrwals for given fd account id
        /// </summary>
        /// <param name="fdaccountid"></param>
        /// <returns></returns>
        public ResultArgs FetchFDRenewalsWithdrwals(Int32 fdaccountid)
        {
            resultArgs = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.FixedDeposit.FetchFDRenewalsWithdrwals))
            {
                dataManager.Parameters.Add(this.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName, fdaccountid);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }

        /// <summary>
        /// On 29/09/2022, To check given post interest date is valid,
        /// 
        /// # 1. If withdrwal done or partial withdrwal done, not allowed post in between date
        /// # 2. Post interest date must be with in the concern renewl date or invested or opening date
        /// </summary>
        /// <param name="fdaccountid"></param>
        /// <param name="PostInterestDate"></param>
        /// <returns></returns>
        public ResultArgs ValidatePostInterestDate(Int32 fdaccountid, DateTime PostInterestDate)
        {
            string filter = "";
            resultArgs = FetchFDRenewalsWithdrwals(fdaccountid);
            if (resultArgs.Success && resultArgs.DataSource.Table != null)
            {
                DataTable dt = resultArgs.DataSource.Table;
                if (dt.Rows.Count > 0)
                {
                    filter = AppSchema.FDAccount.TRANS_TYPEColumn.ColumnName + " IN ('" + FDTypes.WD.ToString() + "')";
                    dt.DefaultView.RowFilter = filter;
                    if (dt.DefaultView.Count == 0)
                    {
                        filter = AppSchema.FDAccount.TRANS_TYPEColumn.ColumnName + " NOT IN ('" + FDTypes.WD.ToString() + "')"; ;
                        dt.DefaultView.RowFilter = string.Empty;
                        dt.DefaultView.RowFilter = filter;
                        if (dt.DefaultView.Count > 0)
                        {
                            DateTime dMinDate = DateSet.ToDate(dt.Compute("MIN(" + AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName + ")", filter).ToString(), false);
                            DateTime dMaxDate = DateSet.ToDate(dt.Compute("MAX(" + AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName + ")", filter).ToString(), false);
                            if (dMinDate < FirstFYDateFrom) dMinDate = FirstFYDateFrom;
                            if (dMaxDate < FirstFYDateFrom) dMaxDate = FirstFYDateFrom;

                            if (PostInterestDate < dMinDate || PostInterestDate > dMaxDate)
                            {
                                resultArgs.Message = "Post Interest Date '" + PostInterestDate.ToShortDateString() + "' is not between Created/Renewed date ranges " +
                                                    "('" + dMinDate.ToShortDateString() + "' and '" + dMaxDate.ToShortDateString() + "'.)";
                            }
                        }
                        else
                        {
                            resultArgs.Message = "Invalid FD Account, Could not find FD details.";
                        }
                    }
                    else
                    {
                        resultArgs.Message = "FD Partial Withdrawal/Withdrawal is done, you can't Post Interest to this FD Account.";
                    }
                }
                else
                {
                    resultArgs.Message = "Invalid FD Account, Could not find FD details.";
                }
            }

            return resultArgs;
        }

        public bool IsAuditVouchersLockedVoucherDate(Int32 pId, DateTime frmDate, DateTime toDate)
        {
            bool rnt = true;
            string projectname = string.Empty;
            ResultArgs result = new ResultArgs();
            AcMELog.WriteLog("Check Audit Voucher Lock Started..");
            result.Success = true;
            try
            {
                using (DataManager datamanager = new DataManager(SQLCommand.AuditLockTrans.FetchAuditLockDetailByProjectDateRange))
                {
                    datamanager.Parameters.Add(this.AppSchema.Voucher.PROJECT_IDColumn, pId);
                    datamanager.Parameters.Add(this.AppSchema.FDRegisters.DATE_FROMColumn, frmDate);
                    datamanager.Parameters.Add(this.AppSchema.FDRegisters.DATE_TOColumn, toDate);
                    result = datamanager.FetchData(DataSource.DataTable);

                    if (result != null && result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                    {
                        projectname = result.DataSource.Table.Rows[0][AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                        string lockedmessage = "Unable to Delete/Modify Vouchers." + System.Environment.NewLine + "Voucher is locked for '" + projectname + "'" +
                            " during the period of " + DateSet.ToDate(frmDate.ToShortDateString()) + " - " + DateSet.ToDate(toDate.ToShortDateString());
                        MessageRender.ShowMessage(lockedmessage);
                        rnt = true;
                    }
                    else
                    {
                        rnt = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Message = "Not able to check for Audit Lock Vouchers " + ex.Message;
                rnt = true;
            }
            finally { }

            if (result.Success)
            {
                AcMELog.WriteLog("Check Audit Voucher Lock Ended.");
            }
            else
            {
                AcMELog.WriteLog("Problem in IsAuditVouchersLocked : " + result.Message);
            }

            return rnt;
        }

        /// <summary>
        /// On 03/11/2021, To get FD details
        /// </summary>
        /// <param name="fdaccountid"></param>
        /// <param name="fdtype"></param>
        /// <returns></returns>
        public DataSet GetFDMasterRenewalDetails(Int32 fdaccountid)
        {
            DataSet ds = new DataSet();

            using (DataManager dataManager = new DataManager(SQLCommand.FixedDeposit.FetchFDMasterByFDAccountId))
            {
                dataManager.Parameters.Add(AppSchema.FDAccount.FD_ACCOUNT_IDColumn, fdaccountid);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                ResultArgs resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
                if (resultArgs.Success)
                {
                    DataTable dtMasterFD = resultArgs.DataSource.Table;
                    if (dtMasterFD != null && dtMasterFD.Rows.Count > 0)
                    {
                        dtMasterFD.TableName = "FD Master";
                        using (DataManager dataManager1 = new DataManager(SQLCommand.FixedDeposit.FetchFDRenewalsByFDAccountId))
                        {
                            dataManager1.Parameters.Add(AppSchema.FDRenewal.FD_ACCOUNT_IDColumn, fdaccountid);
                            dataManager1.DataCommandArgs.IsDirectReplaceParameter = true;
                            resultArgs = dataManager1.FetchData(DAO.Data.DataSource.DataTable);
                            if (resultArgs.Success)
                            {
                                DataTable dtRenewalsFD = resultArgs.DataSource.Table;
                                dtRenewalsFD.TableName = "FD REnewals";

                                ds.Tables.Clear();
                                ds.Tables.Add(dtMasterFD);
                                ds.Tables.Add(dtRenewalsFD);
                            }
                        }
                    }
                }
            }
            return ds;
        }


        /// <summary>
        /// Validate FD Account's renewal date range
        /// </summary>
        /// <returns></returns>
        public ResultArgs ValidateFDAccount(Int32 FDAccountid, Int32 FDRenewalid, DateTime FDDate, DateTime MaturityDate, string FDType, string FDRenewalType,
                                            Int32 FDInterestType, double InterestAmount, double ReInvestmentAmount, double WithdrwalAmount, FDRenewalMode FDRenewalmode)
        {
            resultArgs = new ResultArgs();
            DataTable dtFDHistory = new DataTable();
            string fdtype = string.Empty;
            string renewaltype = string.Empty;
            DateTime renewaldate = FDDate;
            DateTime maturitydate;
            Int32 interestType = -1;
            double receivedinterestamt = 0;
            double accumulatedinterestamt = 0;
            double reinvestmentamt = 0;
            double withdrawalamt = 0;
            if (FDRenewalType == FDRenewalTypes.ACI.ToString()) FDInterestType = 1;
            resultArgs.Success = true;
            if (FDAccountid > 0)
            {
                resultArgs = FetchFDFullHistory(FDAccountid);
                if (resultArgs.Success)
                {
                    dtFDHistory = resultArgs.DataSource.Table;
                    if (FDRenewalid > 0)
                    {
                        //1. Get current renewal details
                        dtFDHistory.DefaultView.RowFilter = AppSchema.FDRenewal.FD_RENEWAL_IDColumn.ColumnName + " = " + FDRenewalid;
                        if (dtFDHistory.DefaultView.Count > 0)
                        {
                            renewaldate = DateSet.ToDate(dtFDHistory.DefaultView[0][AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName].ToString(), false);
                            maturitydate = DateSet.ToDate(dtFDHistory.DefaultView[0][AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName].ToString(), false);
                            fdtype = dtFDHistory.DefaultView[0][AppSchema.FDRenewal.FD_TYPEColumn.ColumnName].ToString();
                            renewaltype = dtFDHistory.DefaultView[0][AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName].ToString();
                            interestType = NumberSet.ToInteger(dtFDHistory.DefaultView[0][AppSchema.FDRenewal.INTEREST_TYPEColumn.ColumnName].ToString());
                            if (renewaltype == FDRenewalTypes.ACI.ToString()) interestType = 1;
                            receivedinterestamt = NumberSet.ToInteger(dtFDHistory.DefaultView[0][AppSchema.FDRegisters.RECEIVED_INTEREST_AMOUNTColumn.ColumnName].ToString());
                            accumulatedinterestamt = NumberSet.ToInteger(dtFDHistory.DefaultView[0][AppSchema.FDRegisters.ACCUMULATED_INTEREST_AMOUNTColumn.ColumnName].ToString());
                            reinvestmentamt = NumberSet.ToInteger(dtFDHistory.DefaultView[0][AppSchema.FDRegisters.RECEIVED_INTEREST_AMOUNTColumn.ColumnName].ToString());
                            withdrawalamt = NumberSet.ToInteger(dtFDHistory.DefaultView[0][AppSchema.FDRegisters.AMOUNTColumn.ColumnName].ToString());

                            if (FDRenewalmode == FDRenewalMode.Delete)
                            {
                                FDDate = renewaldate;
                                MaturityDate = maturitydate;
                                FDType = fdtype;
                                FDRenewalType = renewaltype;
                                FDInterestType = interestType;
                                InterestAmount = (FDRenewalType != FDRenewalTypes.ACI.ToString() ? accumulatedinterestamt : receivedinterestamt);
                                ReInvestmentAmount = reinvestmentamt;
                                withdrawalamt = WithdrwalAmount;
                            }
                        }
                        dtFDHistory.DefaultView.RowFilter = string.Empty;
                    }

                    //2. Assign next or bounded details
                    string filter = AppSchema.FDRenewal.FD_TYPEColumn.ColumnName + "='" + FDTypes.WD.ToString() + "' AND " +
                            AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName + " = '" + FDRenewalTypes.WDI.ToString() + "'";
                    bool isClosed = (dtFDHistory.Select(filter).Length != 0);

                    filter = AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName + " >'" + DateSet.ToDate(FDDate.ToShortDateString(), false) + "' AND " +
                             AppSchema.FDRenewal.FD_TYPEColumn.ColumnName + "= '" + FDTypes.WD.ToString() + "' AND " +
                             AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName + " = '" + FDRenewalTypes.PWD.ToString() + "'";
                    bool isPartiallWithdrwal = (dtFDHistory.Select(filter).Length != 0);

                    filter = AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName + " >'" + DateSet.ToDate(FDDate.ToShortDateString(), false) + "' AND " +
                                AppSchema.FDRenewal.FD_TYPEColumn.ColumnName + " IN ('" + FDTypes.RN.ToString() + "','" + FDTypes.POI.ToString() + "')";
                    DataRow[] drREnewals = dtFDHistory.Select(filter);

                    if ((isClosed || isPartiallWithdrwal) && (fdtype == FDTypes.RIN.ToString() || renewaltype == FDRenewalTypes.ACI.ToString()))
                    { //# 3. Changing Accumulated Interest
                        if (FDRenewalmode == FDRenewalMode.Edit)
                        {
                            if (FDInterestType == 0 && interestType == 1)
                            {
                                resultArgs.Message = "Can't change Interest Mode, This FD Account has Withdrawal/Partially Withdrawal Vouchers in the FD History.";
                            }
                            else if (renewaltype == FDRenewalTypes.ACI.ToString() && InterestAmount != accumulatedinterestamt)
                            {
                                resultArgs.Message = "Can't change Interest Amount, This FD Account has Withdrawal/Partially Withdrawal Vouchers in the FD History.";
                            }
                            else if (fdtype == FDRenewalTypes.RIN.ToString() && ReInvestmentAmount != reinvestmentamt)
                            {
                                resultArgs.Message = "Can't change Re-Investment Amount, This FD Account has Withdrawal/Partially Withdrawal Vouchers in the FD History.";
                            }
                        }
                        else
                        {
                            resultArgs.Message = "Can't Delete, This FD Account has Withdrawal/Partially Withdrawal Vouchers in the FD History.";
                        }
                    }
                    else if (fdtype == FDTypes.WD.ToString() && renewaltype == FDRenewalTypes.PWD.ToString())
                    {
                        if (FDRenewalmode == FDRenewalMode.Edit)
                        {
                            if (drREnewals.Length > 0 && FDType == FDTypes.WD.ToString() && FDRenewalType == FDRenewalTypes.WDI.ToString())
                            {
                                resultArgs.Message = "Can't close, This FD Account has Renewals/Post Interest Vouchers in the FD History.";
                            }
                        }
                    }

                }
            }
            else if (FDRenewalid > 0)
            {
                resultArgs.Message = "Fixed Deposit Account is not available";
            }

            //On 26/02/2025 - When date change in between
            if (resultArgs.Success && FDDate!=renewaldate)
            {
                string daternage = string.Empty;
                if (renewaldate > FDDate)
                {
                    daternage = "((" + AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName + "< '" + FDDate + "' AND " +
                                                            AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName + ">'" + renewaldate + "') AND " +
                                                            AppSchema.FDRenewal.FD_TYPEColumn.ColumnName + "='" + FDTypes.WD.ToString() + "')";
                }
                else if (renewaldate < FDDate)
                {
                    daternage = "((" + AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName + ">'" + renewaldate + "' AND " +
                                                            AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName + "<'" + FDDate + "') AND " +
                                                            AppSchema.FDRenewal.FD_TYPEColumn.ColumnName + "='" + FDTypes.WD.ToString() + "')";
                }
                if (!string.IsNullOrEmpty(daternage))
                {
                    dtFDHistory.DefaultView.RowFilter = daternage;
                    if (dtFDHistory.DefaultView.Count > 0)
                    {
                        resultArgs.Message = "Can't change Renewal Date, This FD Account has Withdrawal/Partially Withdrawal Vouchers in the FD History.";
                    }
                }
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

                if (FDAccountId > 0 || FDRenewalId > 0 || FDVoucherId > 0)
                {
                    using (DataManager dataMember = new DataManager(SQLCommand.FDAccount.FetchRecentFDRenewal))
                    {
                        dataMember.Parameters.Add(this.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn.ColumnName, FDAccountId);
                        dataMember.DataCommandArgs.IsDirectReplaceParameter = true;
                        result = dataMember.FetchData(DataSource.DataTable);
                    }

                    if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtRecentFDRenewal = result.DataSource.Table;
                        dtRecentFDRenewal.DefaultView.RowFilter = AppSchema.FDRenewal.FD_RENEWAL_IDColumn.ColumnName + " > 0";
                        if (dtRecentFDRenewal.DefaultView.Count > 0)
                        {  //Get Recent FD Renewal details
                            RecentRenewalExists = true;
                            RecentRenewalId = NumberSet.ToInteger(dtRecentFDRenewal.DefaultView[0][AppSchema.FDRenewal.FD_RENEWAL_IDColumn.ColumnName].ToString());
                            RecentFDVoucherId = NumberSet.ToInteger(dtRecentFDRenewal.DefaultView[0][AppSchema.FDRenewal.FD_VOUCHER_IDColumn.ColumnName].ToString());
                            RecentFDInterestVoucherId = NumberSet.ToInteger(dtRecentFDRenewal.DefaultView[0][AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn.ColumnName].ToString());
                            RecentRenewalDate = dtRecentFDRenewal.DefaultView[0][AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName].ToString();
                        }
                        else
                        {   //Get FD Account details
                            dtRecentFDRenewal.DefaultView.RowFilter = string.Empty;
                            if (dtRecentFDRenewal.Rows.Count > 0)
                            {
                                //InvestmentDate = dtRecentFDRenewal.DefaultView[0][AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName].ToString();
                                //RecentFDVoucherId = NumberSet.ToInteger(dtRecentFDRenewal.DefaultView[0][AppSchema.FDRenewal.FD_VOUCHER_IDColumn.ColumnName].ToString());
                                InvestmentDate = dtRecentFDRenewal.Rows[0][AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName].ToString();
                                RecentFDVoucherId = NumberSet.ToInteger(dtRecentFDRenewal.Rows[0][AppSchema.FDRenewal.FD_VOUCHER_IDColumn.ColumnName].ToString());
                            }
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
                        else if (!RecentRenewalExists)
                        { //#. If not renewal exists, check is it investment and with in the current FY
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
                        else
                        {
                            result.Message = "Not able to check FD Voucher to be modified";
                        }
                    }
                    else
                    {
                        AcMELog.WriteLog("Given details FD Account :" + FDAccountId.ToString() + " FD Renewal :" + FDRenewalId.ToString() + " VoucherId : " + FDVoucherId.ToString());
                        result.Message = "Not able to check FD Voucher to be modified :  Invalid FD details";
                    }
                }
                else
                {
                    AcMELog.WriteLog("Given details FD Account :" + FDAccountId.ToString() + " FD Renewal :" + FDRenewalId.ToString() + " VoucherId : " + FDVoucherId.ToString());
                    result.Message = "Not able to check FD Voucher to be modified :  Invalid FD details";
                }
            }
            catch (Exception err)
            {
                AcMELog.WriteLog("Given details FD Account :" + FDAccountId.ToString() + " FD Renewal :" + FDRenewalId.ToString() + " VoucherId : " + FDVoucherId.ToString());
                result.Message = "Not able to check FD Voucher to be modified :  " + err.Message;
            }

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ResultArgs CorrectACKPMAFDMutualFund_Temp()
        {
            ResultArgs result = new ResultArgs();
            Int32 Lid = 0;
            bool docorrection = false;

            try
            {
                if (this.HeadofficeCode.ToUpper() == "ACKPMA")
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.DisableFDInterestDeprecaitionLedger))
                    {
                        result = dataManager.UpdateData();
                    }

                    result = FetchFDAccounts();
                    if (result.Success && result.DataSource.Table != null)
                    {
                        DataTable dtFDs = result.DataSource.Table;
                        dtFDs.DefaultView.RowFilter = AppSchema.FDAccount.MF_SCHEME_NAMEColumn.ColumnName + " <> ''";
                        dtFDs = dtFDs.DefaultView.ToTable();

                        if (dtFDs.Rows.Count == 0)
                        {
                            docorrection = true;
                            using (LedgerSystem ledgersystem = new LedgerSystem())
                            {
                                result = ledgersystem.FetchLedgerIdByLedgerName("Mutual Funds");
                                if (result.Success)
                                {
                                    if (result.DataSource.Sclar.ToInteger > 0)
                                    {
                                        Lid = result.DataSource.Sclar.ToInteger;

                                        using (DataManager dataManager = new DataManager(SQLCommand.FixedDeposit.CorrectACKPMAFDMutualFund_Temp))
                                        {
                                            dataManager.Parameters.Add(AppSchema.FDAccount.LEDGER_IDColumn, Lid);
                                            //dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                                            result = dataManager.UpdateData();
                                        }

                                    }
                                    else
                                    {
                                        result.Message = "\"Mutual Funds\" FD ledger is not found";
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    result.Success = true;
                }
            }
            catch (Exception err)
            {
                result.Message = err.Message;
            }

            if (docorrection)
            {
                if (result.Success)
                {
                    MessageRender.ShowMessage("Mutual Fund enabled FD Account(s) are corrected, Please refresh Ledger Balance.");
                }
                else
                {
                    MessageRender.ShowMessage("Not able to correct Mutual Fund enabled FD Account(s) - " + result.Message);
                }
            }

            return result;
        }

    }
}