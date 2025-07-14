using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using Bosco.Model.Dsync;
using AcMEDSync.Model;
using Bosco.Model.Setting;
using System.Text.RegularExpressions;

namespace Bosco.Model.UIModel
{
    public class LedgerSystem : SystemBase
    {

        #region Variable Decelaration
        ResultArgs resultArgs = new ResultArgs();
        CommonMethod UtilityMethod = new CommonMethod();
        public ledgerSubType ledgerTypes;
        #endregion

        #region Constructor
        public LedgerSystem()
        {
        }

        public LedgerSystem(int LedgerId)
        {
            FillLedgerProperties(LedgerId);
        }
        #endregion

        #region Ledger Properties
        public int LedgerId { get; set; }
        public int HeadofficeLedgerId { get; set; }
        public string LedgerCode { get; set; }
        public string LedgerName { get; set; }
        public int GroupId { get; set; }
        public int NatureId { get; set; }
        public int IsCostCentre { get; set; }
        public int IsTDSLedger { get; set; }
        public int IsGSTLedger { get; set; }
        public int CreditorsProfileId { get; set; }
        public int IsBankInterestLedger { get; set; }
        public int IsAssetGainLedger { get; set; }
        public int IsAssetLossLedger { get; set; }
        public int IsAssetDisposalLedger { get; set; }
        public int IsFCRAAccount { get; set; }
        public int IsSubsidyLedger { get; set; }
        public string LedgerType { get; set; }
        public string LedgerSubType { get; set; }
        public int BankAccountId { get; set; }
        public string AccountCode { get; set; }
        public string AccountNumber { get; set; }
        public string AccountHolderName { get; set; }
        public string AccountDate { get; set; }
        public int AccountTypeId { get; set; }
        public int TypeId { get; set; }
        public int BankId { get; set; }
        public string OpenedDate { get; set; }
        public string ClosedDate { get; set; }
        public string OperatedBy { get; set; }
        public string LedgerNotes { get; set; }
        public string BankAccNotes { get; set; }
        public int PeriodYr { get; set; }
        public int PeriodMth { get; set; }
        public int PeriodDay { get; set; }
        public decimal InterestRate { get; set; }
        public decimal Amount { get; set; }
        public string MaturityDate { get; set; }
        public int ProjectId { get; set; }
        public int SortId { get; set; }
        public int FDLedgerBankAccountId { get; set; }
        public DataTable dtMappingLedgers { get; set; }
        public DataTable dtProjectAmountMadeZero { get; set; }

        public bool FDLeger { get; set; }
        public int MapLedgerId { get; set; }
        public string FDType { get; set; }
        public DataTable dtLedgerProile { get; set; }
        public bool isTDSApplicable { get; set; }
        public bool isGSTApplicable { get; set; }
        private int LedgerProfileId { get; set; }
        public TDSLedgerTypes tdsLedgerTypes { get; set; }

        public int IsInKindLedger { get; set; }
        public int IsDepriciationLedger { get; set; }

        public int IsBankFDPenaltyLedger { get; set; }
        public int IsBankSBInterestLedger { get; set; }
        public int IsBankCommissionLedger { get; set; }

        public int FDInvestmentTypeId { get; set; }

        public int ModeId { get; set; }

        /// <summary>
        /// On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
        /// 
        /// This date is apart from balance date. If we take reports for given period (DATE_FROM and DATE_TO)
        /// When we show closing balance for DATE_TO, we have to check bank ledger closed date for DATE_FROM
        /// </summary>
        public string LedgerClosedDateForFilter { get; set; }

        public int BudgetGroupId { get; set; }

        public int BudgetSubGroupId { get; set; }
        #endregion

        #region Properties
        public int NatureofPaymentsId { get; set; }
        public int GStsId { get; set; }
        public string GSTNo { get; set; }
        public int GStServiceType { get; set; }
        public string GST_HSN_SAC_CODE { get; set; }
        public int DeducteeTypeId { get; set; }
        public int BankTransId { get; set; }
        public bool isBankDetails { get; set; }
        public string NickName { get; set; }
        public string FavouringName { get; set; }
        public string BankName { get; set; }
        public string BankAcNo { get; set; }
        public string IFSNo { get; set; }
        public string MailName { get; set; }
        public string MailAddress { get; set; }
        public string PANNo { get; set; }
        public string PANName { get; set; }
        public int State { get; set; }
        public int Country { get; set; }
        public string MobileNumber { get; set; }
        public string PinCode { get; set; }
        public string SalesNo { get; set; }
        public string CSTNo { get; set; }
        public int BankTransType { get; set; }
        public DataTable dtLedgerProfile { get; set; }

        public DateTime LedgerDateClosed { get; set; }
        public Int32 LedgerClosedBy { get; set; }
        public DataTable dtAllProjectLedgerApplicable { get; set; }

        public Int32 LedgerCurrencyCountryId { get; set; }
        public Double LedgerCurrencyOPExchangeRate { get; set; }

        #endregion

        #region Bank Account Details
        public DateTime dtDateOpened { get; set; }
        public DateTime dtDateClosed { get; set; }
        #endregion

        #region Methods
        public ResultArgs FetchLedgerDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);

                //On 12/09/2024, To Skip default ledgers for multi currency or other than country
                using (MappingSystem mappingsystem = new MappingSystem())
                {
                    resultArgs = mappingsystem.EnforceSkipDefaultLedgers(resultArgs, AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                }
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgerDetailsIntegration()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchAllIntegration))
            {
                if (!IsFullRightsReservedUser)
                {
                    dataManager.Parameters.Add(this.AppSchema.UserRole.USERROLE_IDColumn, this.LoginUserRoleId);
                }

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchMergeLedgers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchMergeLedgers))
            {

                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            return resultArgs;
        }

        public ResultArgs FetchBOLedgersForMerge()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchBOLedgerForMerge))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            return resultArgs;
        }

        public ResultArgs FetchBOLedgers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchBOLedgers))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            return resultArgs;
        }

        public ResultArgs FetchHOLedgersForMerge()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchHOLedgerForMerge))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            return resultArgs;
        }

        public ResultArgs FetchBankAccountDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.BankAccountFetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchUnmappedBankAccounts()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchUnmappedBankAccounts))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchFixedDepositDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FixedDepositFetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchFixedDepositCodes()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchFixedDepositCodes))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgerLookup()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerForLookup))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgerByGroup()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerByGroup))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Project.DIVISION_IDColumn, ModeId);

                if (!string.IsNullOrEmpty(LedgerClosedDateForFilter))
                {
                    dataManager.Parameters.Add(this.AppSchema.BankAccount.DATE_CLOSEDColumn, LedgerClosedDateForFilter);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAllHSNSACCode()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchAllHSNSACCode))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int FetchGStId()
        {
            int gstId = 0;
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchGSTId))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    gstId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.GST_IdColumn.ColumnName].ToString());
                }
            }
            return gstId;
        }

        public int FetchGSTLedgerId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchGSTLedgerId))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, LedgerName);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int CheckBankLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.IsBankLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int IsCashLedgerExists()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.IsCashLedgerExists))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs IsBudgetedLedger(Int32 LedgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.IsBudgetedLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchCashBankFDLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchCashBankFDLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchCashBankLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchCashBankLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                if (!string.IsNullOrEmpty(LedgerClosedDateForFilter))
                {
                    dataManager.Parameters.Add(this.AppSchema.BankAccount.DATE_CLOSEDColumn, LedgerClosedDateForFilter);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchCashBankLedger(int projectid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchAllCashBankLedger))
            {
                if (projectid > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                }
                if (!string.IsNullOrEmpty(LedgerClosedDateForFilter))
                {
                    dataManager.Parameters.Add(this.AppSchema.BankAccount.DATE_CLOSEDColumn, LedgerClosedDateForFilter);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchCashBankLedgerByProject(int projectid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchAllCashBankLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                if (!string.IsNullOrEmpty(LedgerClosedDateForFilter))
                {
                    dataManager.Parameters.Add(this.AppSchema.BankAccount.DATE_CLOSEDColumn, LedgerClosedDateForFilter);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchFDinterestLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchBankInterestLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);

                if (!string.IsNullOrEmpty(LedgerClosedDateForFilter))
                {
                    dataManager.Parameters.Add(this.AppSchema.Ledger.DATE_CLOSEDColumn, LedgerClosedDateForFilter);
                }

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteLedgerDetails(int LedgerId)
        {
            //On 13/09/2018, Delete all vouchers made by given ledger which was deleted
            resultArgs = ClearAllDeletedDataByLedger(LedgerId);
            if (resultArgs.Success)
            {
                using (MappingSystem mappingsystem = new MappingSystem())
                {
                    resultArgs = mappingsystem.DeleteProjectLedgerApplicableByLedger(LedgerId, null);
                    if (resultArgs.Success)
                    {
                        using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.Delete))
                        {
                            dataMember.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                            resultArgs = dataMember.UpdateData();
                        }
                    }
                }
            }
            return resultArgs;
        }



        public ResultArgs FetchFdLedgersByProject()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.FetchFDLedgers))
            {
                dataMember.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataMember.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveLedgerDetails()
        {
            using (DataManager dataManager = new DataManager((LedgerId == 0) ? SQLCommand.LedgerBank.Add : SQLCommand.LedgerBank.Update))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId, true);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_CODEColumn, LedgerCode);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, LedgerName);
                dataManager.Parameters.Add(this.AppSchema.Ledger.GROUP_IDColumn, GroupId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_TYPEColumn, LedgerType);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_SUB_TYPEColumn, LedgerSubType);
                dataManager.Parameters.Add(this.AppSchema.Ledger.BANK_ACCOUNT_IDColumn, BankAccountId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_COST_CENTERColumn, IsCostCentre);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_BANK_INTEREST_LEDGERColumn, IsBankInterestLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_ASSET_GAIN_LEDGERColumn, IsAssetGainLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_SUBSIDY_LEDGERColumn, IsSubsidyLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_ASSET_LOSS_LEDGERColumn, IsAssetLossLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_DISPOSAL_LEDGERColumn, IsAssetDisposalLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_INKIND_LEDGERColumn, IsInKindLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_DEPRECIATION_LEDGERColumn, IsDepriciationLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.NOTESColumn, LedgerNotes);
                dataManager.Parameters.Add(this.AppSchema.Ledger.SORT_IDColumn, SortId);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.IS_FCRA_ACCOUNTColumn, IsFCRAAccount);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_TDS_LEDGERColumn, IsTDSLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_GST_LEDGERSColumn, IsGSTLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.GST_SERVICE_TYPEColumn, GStServiceType);
                dataManager.Parameters.Add(this.AppSchema.Ledger.BUDGET_GROUP_IDColumn, BudgetGroupId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.BUDGET_SUB_GROUP_IDColumn, BudgetSubGroupId);

                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_BANK_FD_PENALTY_LEDGERColumn, IsBankFDPenaltyLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_BANK_SB_INTEREST_LEDGERColumn, IsBankSBInterestLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_BANK_COMMISSION_LEDGERColumn, IsBankCommissionLedger);

                dataManager.Parameters.Add(this.AppSchema.Ledger.FD_INVESTMENT_TYPE_IDColumn, FDInvestmentTypeId);

                //02/11/2022, GST HSN and SAC Codes
                if (IsGSTLedger == 0)
                {
                    GST_HSN_SAC_CODE = string.Empty;
                }
                dataManager.Parameters.Add(this.AppSchema.Ledger.GST_HSN_SAC_CODEColumn, GST_HSN_SAC_CODE);

                if (LedgerDateClosed == DateTime.MinValue)
                {
                    dataManager.Parameters.Add(this.AppSchema.Ledger.DATE_CLOSEDColumn, null);
                }
                else
                {
                    dataManager.Parameters.Add(this.AppSchema.Ledger.DATE_CLOSEDColumn, LedgerDateClosed);
                }

                dataManager.Parameters.Add(this.AppSchema.Ledger.CLOSED_BYColumn, LedgerClosedBy);

                if (LedgerCurrencyCountryId == 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.Ledger.CUR_COUNTRY_IDColumn, null);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.OP_EXCHANGE_RATEColumn, 1);
                }
                else
                {
                    dataManager.Parameters.Add(this.AppSchema.Ledger.CUR_COUNTRY_IDColumn, LedgerCurrencyCountryId);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.OP_EXCHANGE_RATEColumn, (LedgerCurrencyOPExchangeRate == 0 ? 1 : LedgerCurrencyOPExchangeRate));
                }

                //  dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();

                //  MapLedgerId = MapLedgerId.Equals(0) ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : MapLedgerId;
            }
            return resultArgs;

        }

        /// <summary>
        /// On 06/02/2023, To update Ledger Closed Date
        /// Check Transactions are available
        /// </summary>
        /// <returns></returns>
        public ResultArgs UpdateLedgerClosedDate(Int32 ledgerid, DateTime deClosedOn)
        {
            ResultArgs resultArgs = new ResultArgs();
            DateTime detDateStarted = this.FirstFYDateFrom;
            if ((deClosedOn != DateTime.MinValue) && !this.DateSet.ValidateDate(detDateStarted, deClosedOn))
            {
                resultArgs.Message = "Closed On can not be less than first Finance Year from.";
            }
            else
            {
                resultArgs.Success = true;
            }

            if (resultArgs.Success)
            {
                resultArgs = CheckLedgerClosedDate(ledgerid, deClosedOn);
                if (resultArgs.Success)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.UpdateClosedDate))
                    {
                        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, ledgerid);
                        if (deClosedOn == DateTime.MinValue)
                        {
                            dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_DATE_CLOSEDColumn, null);
                        }
                        else
                        {
                            dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_DATE_CLOSEDColumn, deClosedOn);
                        }
                        dataManager.Parameters.Add(this.AppSchema.Ledger.CLOSED_BYColumn, LedgerClosedBy);
                        resultArgs = dataManager.UpdateData();
                    }
                }
            }

            return resultArgs;
        }


        /// <summary>
        /// On 04/07/2019, When we import ledgers from Acmeerp portal, If ledgers is available in BO
        /// ledger option (EnableCC, enable bankinterest...etc ) should not be updated.
        /// </summary>
        /// <returns></returns>
        public ResultArgs SaveLedgerDetailsByImportMaster()
        {
            using (DataManager dataManager = new DataManager((LedgerId == 0) ? SQLCommand.LedgerBank.Add : SQLCommand.LedgerBank.UpdateByHeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId, true);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_CODEColumn, LedgerCode); //LedgerCode.ToUpper()
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, LedgerName);
                dataManager.Parameters.Add(this.AppSchema.Ledger.GROUP_IDColumn, GroupId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_TYPEColumn, LedgerType);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_SUB_TYPEColumn, LedgerSubType);
                dataManager.Parameters.Add(this.AppSchema.Ledger.BUDGET_GROUP_IDColumn, BudgetGroupId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.BUDGET_SUB_GROUP_IDColumn, BudgetSubGroupId);

                //Only Enable Ledger options, It will force to disable from portal
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_COST_CENTERColumn, IsCostCentre);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_BANK_INTEREST_LEDGERColumn, IsBankInterestLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_TDS_LEDGERColumn, IsTDSLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_DEPRECIATION_LEDGERColumn, IsDepriciationLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_ASSET_GAIN_LEDGERColumn, IsAssetGainLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_ASSET_LOSS_LEDGERColumn, IsAssetLossLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_DISPOSAL_LEDGERColumn, IsAssetDisposalLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_INKIND_LEDGERColumn, IsInKindLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_SUBSIDY_LEDGERColumn, IsSubsidyLedger);

                if (LedgerId == 0) //On 04/07/2019, to skip updation of BO ledger option
                {
                    dataManager.Parameters.Add(this.AppSchema.Ledger.BANK_ACCOUNT_IDColumn, BankAccountId);
                    dataManager.Parameters.Add(this.AppSchema.BankAccount.IS_FCRA_ACCOUNTColumn, IsFCRAAccount);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.IS_GST_LEDGERSColumn, IsGSTLedger);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.GST_SERVICE_TYPEColumn, GStServiceType);
                }

                //As on 08/11/2021, To update ledger clsoed date, by default it will be null.
                //If we give option in portal, we have to get date closed from Portal and update it.
                if (LedgerDateClosed == DateTime.MinValue)
                {
                    dataManager.Parameters.Add(this.AppSchema.Ledger.DATE_CLOSEDColumn, null);
                }
                else
                {
                    dataManager.Parameters.Add(this.AppSchema.Ledger.DATE_CLOSEDColumn, LedgerDateClosed);
                }
                dataManager.Parameters.Add(this.AppSchema.Ledger.CLOSED_BYColumn, LedgerClosedBy);

                dataManager.Parameters.Add(this.AppSchema.Ledger.NOTESColumn, LedgerNotes);
                dataManager.Parameters.Add(this.AppSchema.Ledger.SORT_IDColumn, SortId);

                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_BANK_SB_INTEREST_LEDGERColumn, IsBankSBInterestLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_BANK_COMMISSION_LEDGERColumn, IsBankCommissionLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_BANK_FD_PENALTY_LEDGERColumn, IsBankFDPenaltyLedger);

                //On 09/11/2022 - GST HSN/SAC Code -------------------------------------------------------------------------------
                dataManager.Parameters.Add(this.AppSchema.Ledger.GST_HSN_SAC_CODEColumn, GST_HSN_SAC_CODE);
                //----------------------------------------------------------------------------------------------------------------

                //On 10/05/2024, To set FD Investment Type
                dataManager.Parameters.Add(this.AppSchema.Ledger.FD_INVESTMENT_TYPE_IDColumn, FDInvestmentTypeId);

                if (LedgerCurrencyCountryId == 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.Ledger.CUR_COUNTRY_IDColumn, null);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.OP_EXCHANGE_RATEColumn, 1);
                }
                else
                {
                    dataManager.Parameters.Add(this.AppSchema.Ledger.CUR_COUNTRY_IDColumn, LedgerCurrencyCountryId);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.OP_EXCHANGE_RATEColumn, (LedgerCurrencyOPExchangeRate == 0 ? 1 : LedgerCurrencyOPExchangeRate));
                }

                //dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();

                //MapLedgerId = MapLedgerId.Equals(0) ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : MapLedgerId;
            }
            return resultArgs;

        }

        public ResultArgs CheckBankAccountMappedToProject()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.CheckBankAccountMappedToProject))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        public ResultArgs CheckBankAccountMappedToLegalEntity(int CustomerID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.CheckBankAccountMappedToLegalEntity))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(this.AppSchema.LegalEntity.CUSTOMERIDColumn, CustomerID);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        public ResultArgs SaveBranchHeadOfficeLedger()
        {
            using (DataManager dataManager = new DataManager((HeadofficeLedgerId == 0) ? SQLCommand.LedgerBank.HeadOfficeAdd : SQLCommand.LedgerBank.HeadOfficeLedgerUpdate))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, HeadofficeLedgerId, true);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_CODEColumn, LedgerCode);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, LedgerName);
                dataManager.Parameters.Add(this.AppSchema.Ledger.GROUP_IDColumn, GroupId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_TYPEColumn, LedgerType);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_SUB_TYPEColumn, LedgerSubType);
                dataManager.Parameters.Add(this.AppSchema.Ledger.BANK_ACCOUNT_IDColumn, BankAccountId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_COST_CENTERColumn, IsCostCentre);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_BANK_INTEREST_LEDGERColumn, IsBankInterestLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.NOTESColumn, LedgerNotes);
                dataManager.Parameters.Add(this.AppSchema.Ledger.SORT_IDColumn, SortId);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.IS_FCRA_ACCOUNTColumn, IsFCRAAccount);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_TDS_LEDGERColumn, IsTDSLedger);

                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_BANK_COMMISSION_LEDGERColumn, IsBankCommissionLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_BANK_SB_INTEREST_LEDGERColumn, IsBankSBInterestLedger);
                dataManager.Parameters.Add(this.AppSchema.Ledger.IS_BANK_FD_PENALTY_LEDGERColumn, IsBankFDPenaltyLedger);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs MappingLedger(DataManager dataManager)
        {
            using (MappingSystem mappingSystem = new MappingSystem())
            {
                mappingSystem.dtMappingLedger = dtMappingLedgers;
                mappingSystem.OpeningBalanceDate = BookBeginFrom;
                mappingSystem.dtMovedLedgerIDCollection = dtProjectAmountMadeZero;
                mappingSystem.IsFDLedger = FDLeger;
                mappingSystem.LedgerId = MapLedgerId;
                mappingSystem.FDTransType = FDType;
                mappingSystem.dtProjectLedgerApplicableDetails = dtAllProjectLedgerApplicable;
                resultArgs = mappingSystem.AccountMappingByLedgerId(dataManager);
            }
            return resultArgs;
        }

        public void FillLedgerProperties(int LedgerId)
        {
            resultArgs = FetchLedgerDetailsById(LedgerId);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                //Modified by   : Carmel Raj on 12-Nov-2015
                //Modified Line : Changed LedgerId to this.LedgerId
                //Purpose       : To set the LedgerId Property of LedgerSystem Class
                this.LedgerId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString());
                this.HeadofficeLedgerId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.HEADOFFICE_LEDGER_IDColumn.ColumnName].ToString());
                LedgerCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.LEDGER_CODEColumn.ColumnName].ToString();
                GroupId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.GROUP_IDColumn.ColumnName].ToString());
                NatureId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerGroup.NATURE_IDColumn.ColumnName].ToString());
                LedgerName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                LedgerType = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.LEDGER_TYPEColumn.ColumnName].ToString();
                LedgerSubType = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.LEDGER_SUB_TYPEColumn.ColumnName].ToString();
                BankAccountId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.BANK_ACCOUNT_IDColumn.ColumnName].ToString());
                IsCostCentre = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.IS_COST_CENTERColumn.ColumnName].ToString());
                IsBankInterestLedger = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.IS_BANK_INTEREST_LEDGERColumn.ColumnName].ToString());
                IsAssetGainLedger = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.IS_ASSET_GAIN_LEDGERColumn.ColumnName].ToString());
                IsSubsidyLedger = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.IS_SUBSIDY_LEDGERColumn.ColumnName].ToString());
                IsAssetLossLedger = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.IS_ASSET_LOSS_LEDGERColumn.ColumnName].ToString());
                IsAssetDisposalLedger = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.IS_DISPOSAL_LEDGERColumn.ColumnName].ToString());
                LedgerNotes = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.NOTESColumn.ColumnName].ToString();
                IsTDSLedger = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.IS_TDS_LEDGERColumn.ColumnName].ToString());
                IsGSTLedger = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.IS_GST_LEDGERSColumn.ColumnName].ToString());
                GStServiceType = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.GST_SERVICE_TYPEColumn.ColumnName].ToString());
                GST_HSN_SAC_CODE = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.GST_HSN_SAC_CODEColumn.ColumnName].ToString();

                NatureofPaymentsId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSBookingDetails.NATURE_OF_PAYMENT_IDColumn.ColumnName].ToString());
                GStsId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.GST_IdColumn.ColumnName].ToString());
                GSTNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.GST_NOColumn.ColumnName].ToString();
                DeducteeTypeId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.DEDUTEE_TYPE_IDColumn.ColumnName].ToString());
                FavouringName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.NAMEColumn.ColumnName].ToString();
                Address = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.ADDRESSColumn.ColumnName].ToString();
                PANNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.PAN_NUMBERColumn.ColumnName].ToString();
                Country = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Country.COUNTRY_IDColumn.ColumnName].ToString());
                State = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.State.STATE_IDColumn.ColumnName].ToString());
                Email = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.EMAILColumn.ColumnName].ToString();
                MobileNumber = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.CONTACT_NUMBERColumn.ColumnName].ToString();
                PinCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.PIN_CODEColumn.ColumnName].ToString();
                dtDateOpened = this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.BankAccount.DATE_OPENEDColumn.ColumnName].ToString(), false);
                dtDateClosed = resultArgs.DataSource.Table.Rows[0][this.AppSchema.BankAccount.DATE_CLOSEDColumn.ColumnName] != DBNull.Value ? this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.BankAccount.DATE_CLOSEDColumn.ColumnName].ToString(), false) : DateTime.MinValue;
                BankId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.BankAccount.BANK_IDColumn.ColumnName].ToString());
                TypeId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.BankAccount.TYPE_IDColumn.ColumnName].ToString());
                AccountHolderName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.BankAccount.ACCOUNT_HOLDER_NAMEColumn.ColumnName].ToString();
                LedgerCurrencyCountryId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.CUR_COUNTRY_IDColumn.ColumnName].ToString());
                LedgerCurrencyOPExchangeRate = this.NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.OP_EXCHANGE_RATEColumn.ColumnName].ToString());
                OperatedBy = resultArgs.DataSource.Table.Rows[0][this.AppSchema.BankAccount.OPERATED_BYColumn.ColumnName].ToString();

                IsInKindLedger = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.IS_INKIND_LEDGERColumn.ColumnName].ToString());
                IsDepriciationLedger = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.IS_DEPRECIATION_LEDGERColumn.ColumnName].ToString());
                BudgetGroupId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.BUDGET_GROUP_IDColumn.ColumnName].ToString());
                BudgetSubGroupId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.BUDGET_SUB_GROUP_IDColumn.ColumnName].ToString());

                IsBankFDPenaltyLedger = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.IS_BANK_FD_PENALTY_LEDGERColumn.ColumnName].ToString());
                IsBankSBInterestLedger = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.IS_BANK_SB_INTEREST_LEDGERColumn.ColumnName].ToString());
                IsBankCommissionLedger = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.IS_BANK_COMMISSION_LEDGERColumn.ColumnName].ToString());

                FDInvestmentTypeId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.FD_INVESTMENT_TYPE_IDColumn.ColumnName].ToString());

                LedgerDateClosed = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.LEDGER_DATE_CLOSEDColumn.ColumnName] != DBNull.Value ? this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.LEDGER_DATE_CLOSEDColumn.ColumnName].ToString(), false) : DateTime.MinValue;
                LedgerClosedBy = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Ledger.CLOSED_BYColumn.ColumnName].ToString());

                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    dtAllProjectLedgerApplicable = null;

                    resultArgs = mappingSystem.FetchProjectLedgerApplicableByLedgerId(LedgerId);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        dtAllProjectLedgerApplicable = resultArgs.DataSource.Table;
                        dtAllProjectLedgerApplicable.DefaultView.Sort = mappingSystem.AppSchema.Ledger.APPLICABLE_FROMColumn.ColumnName;
                        dtAllProjectLedgerApplicable = dtAllProjectLedgerApplicable.DefaultView.ToTable();
                    }
                }


            }
        }

        public ResultArgs FetchLedgerDetailsById(int LedgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteBankAccountDetails()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.BankAccountDelete))
            {
                dataMember.Parameters.Add(this.AppSchema.BankAccount.BANK_ACCOUNT_IDColumn, BankAccountId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveBankLedger(bool isFDledger)
        {
            using (DataManager dataManager = new DataManager())
            {
                //dataManager.BeginTransaction();
                //resultArgs = SaveBankAccountDetails(dataManager, MapLedgerId);
                //if (resultArgs.Success && resultArgs.RowsAffected > 0)
                //{
                //    BankAccountId = (BankAccountId == 0) ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : BankAccountId;
                //    resultArgs = SaveLedgerDetails(dataManager);
                //    MapLedgerId = MapLedgerId.Equals(0) ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : MapLedgerId;
                //    if (resultArgs.Success)
                //        if (!isFDledger) { MappingLedger(dataManager); }
                //}
                //dataManager.EndTransaction();
                dataManager.BeginTransaction();
                resultArgs = SaveLedgerDetails();
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    MapLedgerId = MapLedgerId.Equals(0) ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : MapLedgerId;
                    if (resultArgs.Success)
                        if (!isFDledger) { MappingLedger(dataManager); }
                    resultArgs = SaveBankAccountDetails(dataManager, MapLedgerId);
                    BankAccountId = (BankAccountId == 0) ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : BankAccountId;
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public ResultArgs SaveLedger()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = SaveLedgerDetails();
                MapLedgerId = MapLedgerId.Equals(0) ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : MapLedgerId;
                LedgerProfileId = MapLedgerId.Equals(0) ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : MapLedgerId;
                if (resultArgs != null && resultArgs.Success)
                {
                    resultArgs = MappingLedger(dataManager);
                    if (resultArgs != null && resultArgs.Success)
                    {
                        if (isTDSApplicable)
                        {
                            using (Bosco.Model.TDS.LedgerProfileSystem ledgerProfileSystem = new TDS.LedgerProfileSystem())
                            {
                                ledgerProfileSystem.LedgerID = LedgerProfileId;
                                resultArgs = ledgerProfileSystem.DeleteLedgerProfile();
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    resultArgs = SaveLedgerProfileInfo(dataManager);
                                }
                            }
                        }

                        if (isGSTApplicable)
                        {
                            using (Bosco.Model.TDS.LedgerProfileSystem ledgerProfileSystem = new TDS.LedgerProfileSystem())
                            {
                                ledgerProfileSystem.LedgerID = LedgerProfileId;
                                // ledgerProfileSystem.ProfileGStId = GStsId;
                                resultArgs = ledgerProfileSystem.DeleteLedgerProfileByGST();
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    resultArgs = SaveLedgerProfileInfo(dataManager);
                                }
                            }
                        }

                        else if (!string.IsNullOrEmpty(FavouringName) || !string.IsNullOrEmpty(NickName) || !string.IsNullOrEmpty(Email) ||
                                    !string.IsNullOrEmpty(MobileNumber) || State > 0 || Country > 0 || !string.IsNullOrEmpty(PinCode) ||
                                       !string.IsNullOrEmpty(PANNo))
                        {
                            using (Bosco.Model.TDS.LedgerProfileSystem ledgerProfileSystem = new TDS.LedgerProfileSystem())
                            {
                                ledgerProfileSystem.LedgerID = LedgerProfileId;
                                resultArgs = ledgerProfileSystem.DeleteLedgerProfile();
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    resultArgs = SaveLedgerProfileInfo(dataManager);
                                }
                            }
                        }
                        else
                        {
                            // Commented by Praveen : 14 - 10 - 2016 : If the TDS Option is disabled even then the Profile details to be maintained

                            //using (Bosco.Model.TDS.LedgerProfileSystem ledgerProfileSystem = new TDS.LedgerProfileSystem())
                            //{
                            //    ledgerProfileSystem.LedgerID = LedgerProfileId;
                            //    resultArgs = ledgerProfileSystem.DeleteLedgerProfile();
                            //}
                        }

                        //On 02/07/2019, to map head office ledger ------------------------------------------------------------------------------
                        if (resultArgs.Success)
                        {
                            using (ImportMasterSystem mappledger = new ImportMasterSystem())
                            {
                                resultArgs = mappledger.MapHeadOfficeLedger(MapLedgerId, HeadofficeLedgerId, dataManager);
                            }
                        }
                        //---------------------------------------------------------------------------------------------------------------------------
                    }
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs SaveLedgerProfileInfo(DataManager dataManager)
        {
            using (Bosco.Model.TDS.LedgerProfileSystem ledgerProfileSystem = new TDS.LedgerProfileSystem())
            {
                ledgerProfileSystem.LedgerID = LedgerProfileId;
                ledgerProfileSystem.CreditorsProfileId = CreditorsProfileId;
                ledgerProfileSystem.tdsLedgerTypes = tdsLedgerTypes;
                ledgerProfileSystem.FavouringName = FavouringName;
                ledgerProfileSystem.NickName = NickName;
                ledgerProfileSystem.State = State;
                ledgerProfileSystem.Country = Country;
                ledgerProfileSystem.Email = Email;
                ledgerProfileSystem.PANNo = PANNo;
                ledgerProfileSystem.PinCode = PinCode;
                ledgerProfileSystem.MobileNumber = MobileNumber;
                ledgerProfileSystem.NatureofPaymentsId = NatureofPaymentsId;
                ledgerProfileSystem.ProfileGStId = GStsId;
                ledgerProfileSystem.GSTNo = GSTNo;
                resultArgs = ledgerProfileSystem.SaveLedgeProfile(dataManager);
            }
            return resultArgs;
        }

        public ResultArgs DeleteBankAccount()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                //resultArgs = DeleteBankAccountDetails();
                //if (resultArgs.Success && resultArgs.RowsAffected > 0)
                //{
                //    LedgerId = FetchLedgerId(BankAccountId);
                //    resultArgs = DeleteLedgerDetails(LedgerId);
                //}

                LedgerId = FetchLedgerId(BankAccountId);
                resultArgs = DeleteLedgerDetails(LedgerId);
                if (resultArgs != null && resultArgs.Success)
                {
                    resultArgs = DeleteBankAccountDetails();
                }

                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        private int FetchLedgerId(int BankAccountId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.LedgerIdFetch))
            {
                dataManager.Parameters.Add(this.AppSchema.BankAccount.BANK_ACCOUNT_IDColumn, BankAccountId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs FetchClosedLedgersByDate(DateTime DateClosed)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchClosedLedgersByDate))
            {
                dataManager.Parameters.Add(this.AppSchema.BankAccount.DATE_CLOSEDColumn, DateClosed);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs CheckTransactionExistsByDateClosed(int CLedgerId, DateTime DateClosed)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.CheckTransactionExistsByDateClose))
            {
                dataManager.Parameters.Add(this.AppSchema.BankAccount.DATE_CLOSEDColumn, DateClosed);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.LEDGER_IDColumn, CLedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs CheckTransactionExistsByDateClosed(int ProjectId, int CLedgerId, DateTime DateClosed)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.CheckTransactionExistsByDateClose))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.LEDGER_IDColumn, CLedgerId);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.DATE_CLOSEDColumn, DateClosed);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs CheckTransactionExistsByDateFrom(int ProjectId, int CLedgerId, DateTime DateFrom)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.CheckTransactionExistsByDateFrom))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.LEDGER_IDColumn, CLedgerId);
                dataManager.Parameters.Add(this.AppSchema.ProjectImportExport.DATE_FROMColumn, DateFrom);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        /// <summary>
        /// On 11/10/2021, To update Closed date for ledger after checking its vouchers and balances
        /// #1. Check Vouchers are available for Clsoed Date and for al the Proejcts
        /// #2. Check closing balances for Clsoed Date and for all the Proejcts
        /// </summary>
        /// <param name="LedgerId"></param>
        /// <param name="DateClosed"></param>
        /// <returns></returns>
        public ResultArgs CheckLedgerClosedDate(Int32 LId, DateTime DateClosed)
        {
            ResultArgs result = new ResultArgs();
            // Validate while closing the Bank Accounts if there is Transaction Exists or not if exists make it false in order to do the Transaction (Chinna)
            if (LId > 0 && DateClosed != DateTime.MinValue)
            {
                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    //#1. Check Vouchers are available for clsoed date
                    result = ledgersystem.CheckTransactionExistsByDateClosed(LId, DateClosed);
                    if (result.Success)
                    {
                        if (result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                        {
                            result.Message = "Transaction is made for this Closed Date (" + DateSet.ToDate(DateClosed.ToShortDateString()) + "), Ledger can not be closed.";
                        }
                        else
                        {
                            //On 28/10/2021, To check Ledger Closing balance only for Bank and FD Ledgers alone
                            //#2. Check Closing Balances are available for all the projects
                            LedgerId = LId;
                            int LgrpId = FetchLedgerGroupById();

                            if (LgrpId == (int)FixedLedgerGroup.BankAccounts || LgrpId == (int)FixedLedgerGroup.FixedDeposit)
                            {
                                using (BalanceSystem balancesystem = new BalanceSystem())
                                {
                                    BalanceProperty ledgerclosingbalance = balancesystem.HasBalance(DateSet.ToDate(DateClosed.ToShortDateString()), 0, 0, LId, BalanceSystem.BalanceType.ClosingBalance);
                                    if (ledgerclosingbalance.Amount > 0 || ledgerclosingbalance.AmountFC > 0)
                                    {
                                        result.Message = "Ledger has closing balance, It can't be closed on " + DateSet.ToDate(DateClosed.ToShortDateString());
                                    }
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

            return result;
        }

        /// <summary>
        /// On 28/09/2023, To update Closed date for ledger after checking its vouchers and balances
        /// For Concern Project, check date closed
        /// #1. Check Vouchers are available for Clsoed Date and for al the Proejcts
        /// #2. Check closing balances for Clsoed Date and for all the Proejcts
        /// </summary>
        /// <param name="LedgerId"></param>
        /// <param name="DateClosed"></param>
        /// <returns></returns>
        public ResultArgs CheckLedgerClosedDate(Int32 PId, Int32 LId, DateTime DateClosed)
        {
            ResultArgs result = new ResultArgs();
            // Validate while closing the Bank Accounts if there is Transaction Exists or not if exists make it false in order to do the Transaction (Chinna)
            if (LId > 0 && DateClosed != DateTime.MinValue)
            {
                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    //#1. Check Vouchers are available for clsoed date
                    result = ledgersystem.CheckTransactionExistsByDateClosed(PId, LId, DateClosed);
                    if (result.Success)
                    {
                        if (result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                        {
                            result.Message = "Transaction is made for this Closed Date (" + DateSet.ToDate(DateClosed.ToShortDateString()) + "), Ledger can not be closed.";
                        }
                        else
                        {
                            //On 28/10/2021, To check Ledger Closing balance only for Bank and FD Ledgers alone
                            //#2. Check Closing Balances are available for all the projects
                            LedgerId = LId;
                            int LgrpId = FetchLedgerGroupById();

                            if (LgrpId == (int)FixedLedgerGroup.BankAccounts || LgrpId == (int)FixedLedgerGroup.FixedDeposit)
                            {
                                using (BalanceSystem balancesystem = new BalanceSystem())
                                {
                                    BalanceProperty ledgerclosingbalance = balancesystem.HasBalance(DateSet.ToDate(DateClosed.ToShortDateString()), 0, PId, LId, BalanceSystem.BalanceType.ClosingBalance);

                                    if (ledgerclosingbalance.Amount > 0 || ledgerclosingbalance.AmountFC > 0)
                                    {
                                        result.Message = "Ledger has closing balance, It can't be closed on " + DateSet.ToDate(DateClosed.ToShortDateString());
                                    }
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

            return result;
        }

        /// <summary>
        /// On 29/09/2023, checking its vouchers are avilable for before given date from 
        /// </summary>
        /// <param name="LedgerId"></param>
        /// <param name="DateClosed"></param>
        /// <returns></returns>
        public ResultArgs CheckLedgerDateFrom(Int32 PId, Int32 LId, DateTime DateFrom)
        {
            ResultArgs result = new ResultArgs();

            if (LId > 0 && DateFrom != DateTime.MinValue)
            {
                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    //#1. Check Vouchers are available for beofore date from
                    result = ledgersystem.CheckTransactionExistsByDateFrom(PId, LId, DateFrom);
                    if (result.Success)
                    {
                        if (result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                        {
                            result.Message = "Transaction is made before this Date (" + DateSet.ToDate(DateFrom.ToShortDateString()) + "), Ledger can not be Applicable From.";
                        }
                        else
                        {
                            /*LedgerId = LId;
                            int LgrpId = FetchLedgerGroupById();

                            if (LgrpId == (int)FixedLedgerGroup.BankAccounts || LgrpId == (int)FixedLedgerGroup.FixedDeposit)
                            {
                                using (BalanceSystem balancesystem = new BalanceSystem())
                                {
                                    BalanceProperty ledgerclosingbalance = balancesystem.HasBalance(DateSet.ToDate(DateFrom.ToShortDateString()), 0, PId, LId, BalanceSystem.BalanceType.ClosingBalance);

                                    if (ledgerclosingbalance.Amount > 0)
                                    {
                                        result.Message = "Ledger has closing balance, It can't be closed on " + DateSet.ToDate(DateFrom.ToShortDateString());
                                    }
                                }
                            }*/
                        }
                    }
                }
            }
            else
            {
                result.Success = true;
            }

            return result;
        }

        /// <summary>
        /// On 20/10/2021, This is to Retern the Bank Closed Date.... 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DateTime GetLedgerClosedDate(int Id)
        {
            DateTime DtClosedDate = DateTime.MinValue;
            resultArgs = FetchLedgerClosedDate(Id);
            if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                DtClosedDate = resultArgs.DataSource.Table.Rows[0]["DATE_CLOSED"] != DBNull.Value ? DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["DATE_CLOSED"].ToString(), false) : DateTime.MinValue;

            return DtClosedDate;
        }


        /// <summary>
        /// On 08/09/2022, to check given fd interest ledger has got renewals
        /// </summary>
        /// <returns></returns>
        public ResultArgs CheckFDInterestLedgerVoucherExists(Int32 lid)
        {
            ResultArgs result = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.CheckFDInterestLedgerVoucherExists))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.LEDGER_IDColumn, lid);
                result = dataManager.FetchData(DataSource.DataTable);
            }
            return result;
        }

        /// <summary>
        /// On 08/09/2022, to check given fd penalty ledger has got renewals
        /// </summary>
        /// <returns></returns>
        public ResultArgs CheckFDPenaltyLedgerVoucherExists(Int32 lid)
        {
            ResultArgs result = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.CheckFDPenaltyLedgerVoucherExists))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.LEDGER_IDColumn, lid);
                result = dataManager.FetchData(DataSource.DataTable);
            }
            return result;
        }

        /// <summary>
        /// On 20/10/2021, Ledger Closed Date 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResultArgs FetchLedgerClosedDate(int LedgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerClosedDateById))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 21/10/2021, To get Bank Closed Date
        /// </summary>
        /// <param name="LedgerId"></param>
        /// <returns></returns>
        public ResultArgs FetchBankLedgerClosedDate(int LedgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchBankLedgerClosedDateById))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.LEDGER_IDColumn, LedgerId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 21/10/2021, to get bank ledger closed 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DateTime GetBankLedgerClosedDate(int LedgerId)
        {
            DateTime DtClosedDate = DateTime.MinValue;
            resultArgs = FetchBankLedgerClosedDate(LedgerId);
            if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                DtClosedDate = resultArgs.DataSource.Table.Rows[0]["DATE_CLOSED"] != DBNull.Value ? this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["DATE_CLOSED"].ToString(), false) : DateTime.MinValue;
            }

            return DtClosedDate;
        }




        public ResultArgs FetchReferedByLedgerId(int ledgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchReferedVoucherByLedgerId))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.LEDGER_IDColumn, ledgerId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        public ResultArgs DeleteLedgerReferenceDetails(int Ledgerid)
        {
            resultArgs = UpdateVoucherReferenceNo(Ledgerid);
            if (resultArgs.Success)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.DeleteLedgerReferenceNo))
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.LEDGER_IDColumn, Ledgerid);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.UpdateData();
                }
            }
            return resultArgs;
        }

        private ResultArgs UpdateVoucherReferenceNo(int ledgerid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.UpdateLedgerReferenceNo))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.LEDGER_IDColumn, ledgerid);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchInKindLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerByLedgerGroup))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchLedgerCodes()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.LedgerBank.FetchLedgerCodes))
            {
                datamanager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchLedgersByLedgerCode(string LedgerCode)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.LedgerBank.FetchLedgersByLedgercode))
            {
                datamanager.Parameters.Add(this.AppSchema.Ledger.LEDGER_CODEColumn, LedgerCode);
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchMaxLedgerCode()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchMaxLedgerID))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveBankAccountDetails(DataManager bankAccManager, int SLedgerId)
        {
            using (DataManager dataManager = new DataManager((BankAccountId == 0) ? SQLCommand.LedgerBank.BankAccountAdd : SQLCommand.LedgerBank.BankAccountUpdate))
            {
                dataManager.Database = bankAccManager.Database;
                dataManager.Parameters.Add(this.AppSchema.BankAccount.BANK_ACCOUNT_IDColumn, BankAccountId);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.ACCOUNT_CODEColumn, LedgerCode);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.ACCOUNT_NUMBERColumn, LedgerName);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.ACCOUNT_HOLDER_NAMEColumn, AccountHolderName);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.ACCOUNT_TYPE_IDColumn, AccountTypeId);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.TYPE_IDColumn, TypeId);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.BANK_IDColumn, BankId);
                //dataManager.Parameters.Add(this.AppSchema.BankAccount.DATE_OPENEDColumn, OpenedDate);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.DATE_OPENEDColumn, dtDateOpened);
                //dataManager.Parameters.Add(this.AppSchema.BankAccount.DATE_CLOSEDColumn, (string.IsNullOrEmpty(ClosedDate)) ? null : dtDateClosed.ToShortTimeString());
                if (dtDateClosed == DateTime.MinValue)
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, null);
                }
                else
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, dtDateClosed);
                }
                dataManager.Parameters.Add(this.AppSchema.BankAccount.OPERATED_BYColumn, OperatedBy);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.PERIOD_YEARColumn, PeriodYr);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.PERIOD_MTHColumn, PeriodMth);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.PERIOD_DAYColumn, PeriodDay);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.INTEREST_RATEColumn, InterestRate);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.AMOUNTColumn, Amount);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.MATURITY_DATEColumn, (string.IsNullOrEmpty(MaturityDate) ? null : MaturityDate)); //MaturityDate //(string.IsNullOrEmpty(MaturityDate) ? "2013-10-24" : MaturityDate)
                dataManager.Parameters.Add(this.AppSchema.BankAccount.NOTESColumn, BankAccNotes);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.LEDGER_IDColumn, SLedgerId);
                dataManager.Parameters.Add(this.AppSchema.BankAccount.IS_FCRA_ACCOUNTColumn, IsFCRAAccount);


                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs FetchBankAccountDetailsById(int BankAccountId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.BankAccountFetch))
            {
                dataManager.Parameters.Add(this.AppSchema.BankAccount.BANK_ACCOUNT_IDColumn, BankAccountId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int FetchLedgerNature()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerNature))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int FetchLedgerGroupById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerGroupbyLedgerId))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int FetchBankAccountById(int ledgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchBankAccountById))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, ledgerId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs FetchFixedDepositByLedgersByProject()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.FixedDepositByLedger))
            {
                dataMember.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataMember.Parameters.Add(this.AppSchema.BankAccount.BANK_ACCOUNT_IDColumn, FDLedgerBankAccountId);
                resultArgs = dataMember.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int GetAccessFlag(int LedgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchAccessFlag))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs SaveProjectLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectLedgerMappingAdd))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgerByIncludeCostCentre()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerByIncludeCostCentre))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgerByIncludeHighValuePayments()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchHighValuePaymentbyLedgers))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgerByIncludeLocalDonations()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLocalDonationLedgers))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgerByIncludeGSTDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerByIncludeGST))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs UpdateLedgerOption(int LedgerId)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.UpdateLedgerOptions))
            {
                dataMember.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }
        public ResultArgs UpdateLedgerOptionHighValuePayment(int LedgerId)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.UpdateLedgerOptionsHighValuePaymentByLedger))
            {
                dataMember.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs UpdateLedgerOptionLocalDonations(int LedgerId)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.UpdateLedgerOptionsLocalDonationByLedger))
            {
                dataMember.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs UpdateLedgerOptionCostcentre()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.UpdateLedgerOptionsCostcentre))
            {
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs UpdateLedgerOptionHighValues()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.UpdaterLedgerOptionHighValuePayments))
            {
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs UpdateLedgerOptionLocalDonation()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.UpdateLedgerOptionLocalDonations))
            {
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }


        public ResultArgs FetchLedgerByIncludeBankInterestLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerByIncludeBankInterest))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgerByIncludeBankFDPenaltyLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerByIncludeBankFDPenaltyLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgerByIncludeBankSBInterestLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerByIncludeBankSBInterest))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgerByIncludeBankCommissionLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerByIncludeBankCommission))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        #region Include In kind Ledger Option

        public ResultArgs FetchLedgerByIncludeInKindLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerByIncludeInkindLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        // Load Asset Gain Ledgers to be mapped in the Ledger option screen.
        public ResultArgs FetchLedgerByEnableAssetGainLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgersByEnableAssetGainLedger))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        // Load Asset Loss Ledgers to be mapped in the Ledger option screen.
        public ResultArgs FetchLedgerByEnableAssetLossLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgersByEnableAssetLossLedger))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgerByEnableAssetDiposalLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgersByEnableAssetDisposalLedger))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgerByEnableSubsidyLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgersByEnableSubsidyLedger))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs UpdateLedgerOptionForInkindOne(int LedgerId)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.UpdateLedgerOptionsInkindLedgersetone))
            {
                dataMember.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }
        public ResultArgs UpdateLedgerOptionLossLedgerOne(int LedgerId)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.UpdateLedgerOptionsLossLedgerssetone))
            {
                dataMember.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs UpdateLedgerOptionForGainLedgerOne(int LedgerId)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.UpdateLedgerOptionsGainLedgerssetone))
            {
                dataMember.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }


        public ResultArgs UpdateLedgerOptionForInkindZero()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.UpdateLedgerOptionsInkindLedgersetzero))
            {
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }
        public ResultArgs UpdateLedgerOptionForGainLedgers()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.UpdateLedgerOptionsGainLedgerssetzero))
            {
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }
        public ResultArgs UpdateLedgerOptionForLossLedgers()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.UpdateLedgerOptionsLossLedgerssetzero))
            {
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        #endregion

        #region Include Depreciation Ledger Option

        public ResultArgs FetchLedgerByIncludeDepreciationLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerByIncludeDepreciationLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs UpdateLedgerOptionForDepreciationOne(int LedgerId)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.UpdateLedgerOptionsDepreciationsetone))
            {
                dataMember.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs UpdateLedgerOptionForDisposalOne(int LedgerId)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.UpdateLedgerOptionsDisposalsetone))
            {
                dataMember.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs UpdateLedgerOptionForSubsidyOne(int LedgerId)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.UpdateLedgerOptionsSubsidyone))
            {
                dataMember.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs UpdateLedgerOptionForGSTOne(int LedgerId, int gstServiceType, int gstId)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.UpdateLedgerOptionsGSTone))
            {
                dataMember.BeginTransaction();
                dataMember.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                dataMember.Parameters.Add(this.AppSchema.Ledger.GST_SERVICE_TYPEColumn, gstServiceType);
                dataMember.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataMember.UpdateData();

                if (resultArgs.Success)
                {
                    UpdateLedgerOptionForGSTCreditorsOne(LedgerId, gstId, dataMember);
                }
                dataMember.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs UpdateLedgerOptionForGSTCreditorsOne(int LedgerId, int gstId, DataManager DMBase)
        {
            bool LedgerProfileExists = false;
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchGSTId))
            {
                dataManager.Database = DMBase.Database;
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    LedgerProfileExists = true;
                }

                //# Update or Insert in Ledger Creditors table
                if (resultArgs.Success)
                {
                    using (DataManager dataMember = new DataManager(LedgerProfileExists ? SQLCommand.LedgerBank.UpdateCreditorsOptionsGST : SQLCommand.LedgerBank.InsertCreditorsOptionsGST))
                    {
                        dataMember.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                        dataMember.Parameters.Add(this.AppSchema.LedgerProfileData.GST_IdColumn, gstId);
                        dataMember.DataCommandArgs.IsDirectReplaceParameter = true;
                        resultArgs = dataMember.UpdateData();
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs UpdateLedgerOptionForDepreciationZero()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.UpdateLedgerOptionsDepreciationsetzero))
            {
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs UpdateLedgerOptionForDisposalZero()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.UpdateLedgerOptionsDisposalsetzero))
            {
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs UpdateLedgerOptionForSubsidyZero()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.UpdateLedgerOptionsSubsidyzero))
            {
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs UpdateLedgerOptionForGSTZero(int gstype, int gstclass)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.UpdateLedgerOptionsGSTZero))
            {
                dataMember.BeginTransaction();
                dataMember.Parameters.Add(this.AppSchema.Ledger.GST_SERVICE_TYPEColumn, gstype);
                dataMember.Parameters.Add(this.AppSchema.LedgerProfileData.GST_IdColumn, gstclass);
                dataMember.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataMember.UpdateData();
                dataMember.EndTransaction();
            }
            return resultArgs;
        }


        #endregion

        public ResultArgs UpdateLedgerOptionForbankInterestOne(int LedgerId)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.UpdateLedgerOptionsBankInterestsetone))
            {
                dataMember.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs UpdateLedgerOptionForBankInterestZero()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.UpdateLedgerOptionsBankInterestsetzero))
            {
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs UpdateLedgerOptionsBankSBInterest(int LedgerId)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.UpdateLedgerOptionsBankSBInterestByLedger))
            {
                dataMember.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs UpdateLedgerOptionsBankFDPenaltyLedgersSetDisableAll()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.UpdateLedgerOptionsBankFDPenaltyLedgersSetDisableAll))
            {
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs UpdateLedgerOptionsBankFDPenaltyLedgers(int LedgerId)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.UpdateLedgerOptionsBankFDPenaltyLedgers))
            {
                dataMember.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs UpdateLedgerOptionsBankSBInterestsetDisableAll()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.UpdateLedgerOptionsBankSBInterestsetDisableAll))
            {
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs UpdateLedgerOptionsBankCommissionByLedger(int LedgerId)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.UpdateLedgerOptionsBankCommissionByLedger))
            {
                dataMember.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs UpdateLedgerOptionsBankCommissionDisableAll()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.UpdateLedgerOptionsBankCommissionDisableAll))
            {
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs MadeTransactionByLedger(string LedgerIDCollection)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.CheckTransactionMadeByLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerIDCollection);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs MadeTransactionByAssetGainLedger(string LedgerIDCollection)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.CheckTransactionMadeByAssetGainLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerIDCollection);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs MadeTransactionByAssetDisposalLedger(string LedgerIDCollection)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.CheckTransactionMadeByAssetDisposalLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerIDCollection);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        public ResultArgs MadeTransactionByAssetLossLedger(string LedgerIDCollection)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.CheckTransactionMadeByAssetLossLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerIDCollection);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs MadeFDTransactionByLedger(string LedgeridColllection)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.LedgerBank.CheckFDTransactionMadeByLedger))
            {
                datamanager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgeridColllection);
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs MadeFDTransactionByInterestLedger(string LedgeridColllection)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.LedgerBank.CheckFDTransactionMadeByInterestLedger))
            {
                datamanager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgeridColllection);
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public string GetLegerName(int ledgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerName))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.LEDGER_IDColumn, ledgerId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToString;
        }

        public string GetGroupName(int groupId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchGroupName))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, groupId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToString;
        }
        public ResultArgs FetchLedgerIdByLedgerName(string LedgerName, bool IsCashBankLedger)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerIdByLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, LedgerName);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                if (IsCashBankLedger)
                {
                    dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, "12,13");
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgerIdByLedgerName(string LedgerName)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerId))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, LedgerName.Trim());
                dataManager.DataCommandArgs.IsDirectReplaceParameter = false;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        public ResultArgs FetchHOLedgerIdByLedgerName(string LedgerName)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchHOLedgerId))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, LedgerName.Trim());
                dataManager.DataCommandArgs.IsDirectReplaceParameter = false;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }


        public ResultArgs FetchCashBankLedgerById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchCashBankLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        //public ResultArgs FetchFDAccountsByMaturityDate(string datefrom, string dateto)
        //{
        //    using (DataManager dataManager = new DataManager(SQLCommand.FixedDeposit.FetchFDAccountByMaturityDate))
        //    {
        //        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
        //        dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
        //        dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, datefrom);
        //        dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, dateto);
        //        resultArgs = dataManager.FetchData(DataSource.DataTable);
        //    }
        //    return resultArgs;
        //}

        //Author  :Carmel Raj M
        //Date    :08-Aug-2014

        #region TDS

        /// <summary>
        /// Loads only Duties and Taxes Ledgers
        /// </summary>
        /// <returns>
        /// Collection of Duties and Taxes Ledgers
        /// </returns>
        public DataTable LoadDutiesAndTaxesLedgers()
        {
            DataTable dtDutiesAndTaxesLedgers = null;
            resultArgs = FetchLedgerDetails();
            if (resultArgs != null && resultArgs.Success)
            {
                dtDutiesAndTaxesLedgers = FilterDutiesAndTaxesLedgers(resultArgs.DataSource.Table);
            }
            return dtDutiesAndTaxesLedgers;
        }

        public DataTable LoadPartyLedgers()
        {
            DataTable dtDutiesAndTaxesLedgers = null;
            resultArgs = FetchLedgerDetails();
            if (resultArgs != null && resultArgs.Success)
            {
                dtDutiesAndTaxesLedgers = FilterPartyLedgers(resultArgs.DataSource.Table);
            }
            return dtDutiesAndTaxesLedgers;
        }
        /// <summary>
        /// Filters only Duties and Taxes Ledger alone from Collection of ledgers
        /// Duties and Taxes unique Group Id=24,according to the Database design(default values)
        /// Table Name        :master_ledger_group 
        /// Ledger Group Name :Duties and Taxes
        /// ID                :24
        /// </summary>
        /// <param name="dtAllLedgers">
        /// Collection of Ledgers
        /// </param>
        /// <returns>
        /// Collection of Duties and Taxes Ledgers
        /// </returns>
        private DataTable FilterDutiesAndTaxesLedgers(DataTable dtAllLedgers)
        {
            int DutiesAndTaxesID = (int)TDSLedgerGroup.DutiesAndTax;
            DataView dvFilterDutiesAndTaxesLedger = new DataView(dtAllLedgers);
            dvFilterDutiesAndTaxesLedger.RowFilter = String.Format("GROUP_ID={0}", DutiesAndTaxesID);
            return dvFilterDutiesAndTaxesLedger.ToTable();
        }

        private DataTable FilterPartyLedgers(DataTable dtAllLedgers)
        {
            int DutiesAndTaxesID = (int)TDSLedgerGroup.SundryCreditors;
            DataView dvFilterDutiesAndTaxesLedger = new DataView(dtAllLedgers);
            dvFilterDutiesAndTaxesLedger.RowFilter = String.Format("GROUP_ID={0}", DutiesAndTaxesID);
            return dvFilterDutiesAndTaxesLedger.ToTable();
        }

        public int IsTDSLedgerExits()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.IsTDSLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs FetchTDSLedgers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchTDSLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchNOPforTDSExpenseLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchTDSExpNOP))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchDutiesTaxLedgers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchDutiesTaxLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgerByNature()
        {

            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerByNature))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, GroupId);

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgerByNatures()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerByNatureAll))
            {
                // dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgerByLedgerGroup()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerByFixedGroup))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, GroupId);

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        // aldrin for asset user control
        public ResultArgs FetchLossLedgers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLossLedgers))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchDisposalLedgers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchAllDisposalLedgers))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        // aldrin for asset user control
        public ResultArgs FetchGainLedgers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchGainLedgers))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        // aldrin for asset user control
        public ResultArgs FetchAllInkindLedgers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchAllInkindLedgers))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAllExpenceLedgers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchAllExpenceLedgers))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.GROUP_IDColumn, GroupId);
                if (!string.IsNullOrEmpty(LedgerClosedDateForFilter))
                {
                    dataManager.Parameters.Add(this.AppSchema.Ledger.DATE_CLOSEDColumn, LedgerClosedDateForFilter);
                }
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchAllUnusedLedgers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchAllUnusedLedgers))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            return resultArgs;
        }

        public ResultArgs DeleteCategoryLedgerbyLedgerID(int LedgerID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ImportMaster.DeleteProjectCategoryByLedgerId))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerID);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgersByLedgerId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerById))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteAllUnusedLedgers(string DelLEDIDs)
        {
            try
            {
                string ProblemLedgers = string.Empty;
                string[] LedgerIds = DelLEDIDs.Split(',');
                using (ImportMasterSystem importsystem = new ImportMasterSystem())
                {
                    resultArgs.Success = true;
                    foreach (string LedgerId in LedgerIds)
                    {
                        //On 25/01/2022, to skip record association problem, move to next ledger
                        if (resultArgs.Success || resultArgs.Message.Contains(MessageRender.GetMessage(MessageCatalog.Common.CANNOT_DELETE))) // Cannot Delete. This Record has association
                        {
                            importsystem.LedgerId = NumberSet.ToInteger(LedgerId);
                            resultArgs = importsystem.DeleteLedger(SQLCommand.ImportMaster.DeleteProjectMappedLedger, true);
                            if (resultArgs.Success)
                            {
                                resultArgs = importsystem.DeleteLedger(SQLCommand.ImportMaster.DeleteLedgerBalance, true);
                                if (resultArgs.Success)
                                {
                                    resultArgs = importsystem.DeleteLedger(SQLCommand.ImportMaster.DeleteProjectBudgetMappedLedger, true);
                                    if (resultArgs.Success)
                                    {
                                        resultArgs = importsystem.DeleteLedger(SQLCommand.ImportMaster.DeleteBudgetLedger, true);
                                        if (resultArgs.Success)
                                        {
                                            resultArgs = importsystem.DeleteLedger(SQLCommand.ImportMaster.DeleteFDLedger, true);
                                            if (resultArgs.Success)
                                            {
                                                resultArgs = importsystem.DeleteVouchers();
                                                if (resultArgs.Success)
                                                {
                                                    resultArgs = importsystem.DeleteLedger(SQLCommand.ImportMaster.DeleteTDSCreditorProfile, true);
                                                    if (resultArgs.Success)
                                                    {
                                                        resultArgs = importsystem.DeleteLedger(SQLCommand.ImportMaster.DeleteHeadOfficeMappedLedger, true);
                                                        if (resultArgs.Success)
                                                        {
                                                            resultArgs = importsystem.DeleteLedger(SQLCommand.ImportMaster.DeleteHeadOfficeLedger, true);
                                                            if (resultArgs.Success)
                                                            {
                                                                resultArgs = importsystem.DeleteLedger(SQLCommand.ImportMaster.DeleteProjectCategoryByLedgerId, true);
                                                                if (resultArgs.Success)
                                                                {
                                                                    resultArgs = importsystem.DeleteLedger(SQLCommand.ImportMaster.DeleteLedgerOtherMappedDetails, true);
                                                                    if (resultArgs.Success)
                                                                    {
                                                                        resultArgs = importsystem.DeleteLedger(SQLCommand.ImportMaster.DeleteMasterLedger, true);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (!resultArgs.Success)
                        {
                            //string msg = (resultArgs.Exception as ExceptionHandler).Exception.Message;
                            //Match matchtblexists = Regex.Match(msg.ToUpper(), @"CANNOT DELETE OR UPDATE A PARENT ROW: A FOREIGN KEY CONSTRAINT FAILS '[^']*' FOREIGN KEY$", RegexOptions.IgnoreCase);
                            //if (!matchtblexists.Success)
                            //{

                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source);
            }
            return resultArgs;
        }

        public ResultArgs FetchAllUnusedLedgersGroups()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchAllUnusedGroups))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs DeleteAllUnusedLedgersGroups(string DelLEDIDs)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.DeleteAllUnusedGroups))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.IDsColumn, DelLEDIDs);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// On 13/09/2018, 
        /// This method is used to clear all deleted Vouchers for given ledger
        /// Delete Vouchers in Vouchers, TDS, FD, Budget, HO Mapping, Prject Mapping, Category Mapping 
        /// Bank Account and Ledger Balance
        /// </summary>
        /// <returns></returns>
        private ResultArgs ClearAllDeletedDataByLedger(Int32 LedgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.ClearAllDeletedDataByLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        #endregion


        public ResultArgs ChangeLedgerName(string OldLedgerName, string NewLedgerName)
        {
            //1. Check in Master Ledger, Change Ledger Name in Master
            ResultArgs resultArgs = FetchLedgerIdByLedgerName(NewLedgerName);
            if (!resultArgs.Success || resultArgs.DataSource.Sclar.ToInteger <= 0)
            {
                using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.ChangeLedgerNameInMaster))
                {
                    dataMember.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, NewLedgerName);
                    dataMember.Parameters.Add(this.AppSchema.Ledger.LEDGER_CODEColumn, OldLedgerName);
                    resultArgs = dataMember.UpdateData();
                }
            }

            //2. Check in Master Head office Ledger, Change Ledger Name in HO Master Ledger
            resultArgs = FetchHOLedgerIdByLedgerName(NewLedgerName);
            if (!resultArgs.Success || resultArgs.DataSource.Sclar.ToInteger <= 0)
            {
                using (DataManager dataMember = new DataManager(SQLCommand.LedgerBank.ChangeLedgerNameInHOMaster))
                {
                    dataMember.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, NewLedgerName);
                    dataMember.Parameters.Add(this.AppSchema.Ledger.LEDGER_CODEColumn, OldLedgerName);
                    resultArgs = dataMember.UpdateData();
                }
            }

            return resultArgs;
        }


        /// <summary>
        /// 
        /// TMP Purpose for mysore
        /// 
        /// On 08/01/2020, to update budget group for temp purpose
        /// </summary>
        /// <param name="OldLedgerName"></param>
        /// <param name="NewLedgerName"></param>
        /// <returns></returns>
        public void UpdateBudgetGroupForMysore()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.UpdateBudgetGroupRecurringByLedgerName))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
        }



        /// <summary>
        /// 
        /// TMP Purpose for mysore
        /// 
        /// On 08/01/2020, to update budget group for temp purpose
        /// </summary>
        /// <param name="OldLedgerName"></param>
        /// <param name="NewLedgerName"></param>
        /// <returns></returns>
        public void UpdateBudgetSubGroupForMysore(string ledgername, int budgetsubgrpid, int sortid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.UpdateMysoreBudgetSubGroupRecurringByLedgerName))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, ledgername);
                dataManager.Parameters.Add(this.AppSchema.Ledger.BUDGET_SUB_GROUP_IDColumn, budgetsubgrpid);
                dataManager.Parameters.Add(this.AppSchema.Ledger.SORT_IDColumn, sortid);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
        }

        /// <summary>
        /// On 13/02/2020, to update budget Group, Sub Group and Ledger Sort Order
        /// </summary>
        /// <param name="OldLedgerName"></param>
        /// <param name="NewLedgerName"></param>
        /// <returns></returns>
        public void UpdateBudgetGroupDetails(int ledgerid, int budgetgroupid, int budgetsubgroupid, int sortorder)
        {
            if (ledgerid > 0)
            {

                using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.UpdateBudgetGroupDetails))
                {
                    dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, ledgerid);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.BUDGET_GROUP_IDColumn, budgetgroupid);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.BUDGET_SUB_GROUP_IDColumn, budgetsubgroupid);
                    dataManager.Parameters.Add(this.AppSchema.Ledger.SORT_IDColumn, sortorder);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.UpdateData();
                }
            }
        }

        /// <summary>
        /// On 25/03/2021,
        /// 
        /// For SDBINM Auditors suggested to skip below mentioned Ledgers for Voucher Entries
        /// </summary>
        public string GetSDBINMAuditorSkippedLedgerIds()
        {
            string rtn = string.Empty;
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.GetSDBINMAuditorSkippedLedgerIds))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            if (resultArgs.Success && resultArgs.DataSource.Table != null)
            {
                rtn = resultArgs.DataSource.Table.Rows[0][AppSchema.Ledger.LEDGER_ID_COLLECTIONColumn.ColumnName].ToString();
            }
            return rtn;
        }

        public ResultArgs FetchBudgetGroupLookup()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchBudgetGroupLookup))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBudgetSubGroupLookup()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchBudgetSubGroupLookup))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBankBranchById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchBankBranch))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchCongregationLedgers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CongregationLedgers.FetchCongregationLedgers))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgersMappedWithCongregationLedgers(Natures connature)
        {
            resultArgs = FetchLedgersMappedWithCongregationLedgers(0, string.Empty, connature, false);
            return resultArgs;
        }


        public ResultArgs FetchLedgersMappedWithCongregationLedgers(Int32 conledgerid, string conledgercode, Natures connature, bool getOnlyMapped)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CongregationLedgers.FetchLedgersMappedWithCongregationLedgers))
            {
                //if (conledgerid > 0)
                //{
                //    dataManager.Parameters.Add(this.AppSchema.GenerlateReport.CON_LEDGER_IDColumn.ColumnName, conledgerid);
                //}
                dataManager.Parameters.Add(this.AppSchema.GenerlateReport.CON_NATURE_IDColumn.ColumnName, (Int32)connature);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = false;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            if (resultArgs.Success && resultArgs.DataSource.Table != null)
            {
                if (conledgerid > 0)
                {
                    string filter = AppSchema.GenerlateReport.CON_LEDGER_IDColumn.ColumnName + " IN (0," + conledgerid.ToString() + ")";

                    //On 15/02/2023, To load only Cash/Bank/FD Ledgers only for A-Financial Status -------------
                    string cashbankfd = ((Int32)FixedLedgerGroup.Cash).ToString() + "," + ((Int32)FixedLedgerGroup.BankAccounts).ToString() + "," + ((Int32)FixedLedgerGroup.FixedDeposit).ToString();
                    if (conledgerid == 1 || conledgercode == "A")
                    {
                        filter += " AND " + AppSchema.Ledger.GROUP_IDColumn.ColumnName + " IN (" + cashbankfd + ")";
                    }
                    else
                    {
                        filter += " AND " + AppSchema.Ledger.GROUP_IDColumn.ColumnName + " NOT IN (" + cashbankfd + ")";
                    }
                    //------------------------------------------------------------------------------------------

                    if (getOnlyMapped)
                    {
                        filter = AppSchema.GenerlateReport.CON_LEDGER_IDColumn + " = " + conledgerid.ToString();
                    }
                    DataTable dtMappedLedgers = resultArgs.DataSource.Table;
                    dtMappedLedgers.DefaultView.RowFilter = filter;
                    //dtMappedLedgers.DefaultView.Sort = "SELECT DESC, " + AppSchema.GenerlateReport.NATURE_IDColumn.ColumnName + ", "
                    //                                         + AppSchema.GenerlateReport.LEDGER_NAMEColumn.ColumnName;

                    dtMappedLedgers.DefaultView.Sort = "SELECT DESC, " + AppSchema.GenerlateReport.LEDGER_CODEColumn.ColumnName;
                    resultArgs.DataSource.Data = dtMappedLedgers.DefaultView.ToTable();
                }
            }
            return resultArgs;
        }

        public ResultArgs FetchCongregationFixedAssetDetails()
        {
            resultArgs = FetchCongregationFixedAssetDetails(DateSet.ToDate(this.YearFrom, false), DateSet.ToDate(this.YearTo, false));
            return resultArgs;
        }

        public ResultArgs FetchCongregationFixedAssetDetails(DateTime datefrom, DateTime dateto)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CongregationLedgers.FetchCongregationFixedAssetDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, datefrom);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, dateto);

                dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_FROMColumn, this.YearFrom);
                dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_TOColumn, this.YearTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAllCongregationFixedAssetDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CongregationLedgers.FetchCongregationFixedAssetDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, this.FirstFYDateFrom);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, this.LastFYDateTo);

                dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_FROMColumn, this.FirstFYDateFrom);
                dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_TOColumn, this.LastFYDateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// This methoid is used to update Congregation Ledgers list and Mapp by default branch ledgers to Congregation Ledgers
        /// </summary>
        /// <returns></returns>
        public ResultArgs CheckAndUpdateDefaultCongregationLedger(Natures connature)
        {
            string txt = this.ContributionFromLedgers;
            string txt1 = this.ContributionToLedgers;

            bool congregationLedgerExists = false;
            bool congregationLedgerMappingExists = false;

            //1. Check Congregation Ledgers and Update it
            ResultArgs result = FetchCongregationLedgers();
            if (result.Success && result.DataSource.Table != null)
            {
                congregationLedgerExists = (result.DataSource.Table.Rows.Count > 0);
                //If congregation ledger is not available, insert default congregation ledgers
                if (!congregationLedgerExists)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.CongregationLedgers.InsertUpdateDefaultCongregationLedgers))
                    {
                        result = dataManager.UpdateData();
                    }

                    if (result.Success)
                    {
                        congregationLedgerExists = result.RowsAffected > 0;
                    }
                }
            }

            //2. Check Mapping and Update Mapping 
            if (congregationLedgerExists)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.CongregationLedgers.FetchLedgersMappedWithCongregationLedgers))
                {
                    dataManager.Parameters.Add(this.AppSchema.GenerlateReport.CON_NATURE_IDColumn.ColumnName, (Int32)connature);
                    result = dataManager.FetchData(DataSource.DataTable);
                    if (result.Success && result.DataSource.Table != null)
                    {
                        result.DataSource.Table.DefaultView.RowFilter = AppSchema.GenerlateReport.CON_LEDGER_IDColumn.ColumnName + " > 0";

                        congregationLedgerMappingExists = (result.DataSource.Table.DefaultView.Count > 0);
                    }
                }

                if (!congregationLedgerMappingExists)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.CongregationLedgers.InsertUpdateDefaultMappingWithCongregationLedgers))
                    {
                        result = dataManager.UpdateData();
                    }

                    if (result.Success)
                    {
                        congregationLedgerExists = result.RowsAffected > 0;
                    }
                }
            }

            return result;
        }


        /// <summary>
        /// This method is used to Insert/Update Mapping Branch Ledtgers to Congregation Ledgters
        /// </summary>
        /// <returns></returns>
        public ResultArgs MapLedgerWithCongregationLedgers(Natures connature, Int32 ConLedgerId, DataTable dtMappedLedgers)
        {
            ResultArgs result = new ResultArgs();
            try
            {
                //1.Delete already mapped ledgers and Update selected branch ledgers with congregation ledgers
                dtMappedLedgers.DefaultView.RowFilter = string.Empty;
                dtMappedLedgers.DefaultView.RowFilter = "SELECT =  1";
                DataTable dtSelectedLedgers = dtMappedLedgers.DefaultView.ToTable();

                //Delete already mapped ledgers
                using (DataManager dm = new DataManager(SQLCommand.CongregationLedgers.DeleteLedgersMappedWithCongregationByConLedgerId))
                {
                    //Delete alrady mapped ledgers with ConLedgers
                    //dm.BeginTransaction();

                    dm.Parameters.Add(this.AppSchema.GenerlateReport.CON_LEDGER_IDColumn.ColumnName, ConLedgerId);
                    result = dm.UpdateData();
                    if (result.Success)
                    {
                        //Insert or update Ledgers
                        string selectedledgerids = string.Empty;
                        foreach (DataRow dr in dtSelectedLedgers.Rows)
                        {
                            Int32 ledgerid = NumberSet.ToInteger(dr[AppSchema.GenerlateReport.LEDGER_IDColumn.ColumnName].ToString());
                            double conopamount = NumberSet.ToDouble(dr[AppSchema.GenerlateReport.CON_OP_AMOUNTColumn.ColumnName].ToString());
                            string conoptransmode = dr[AppSchema.GenerlateReport.CON_OP_TRANS_MODEColumn.ColumnName].ToString().Trim();

                            selectedledgerids += dr[AppSchema.GenerlateReport.LEDGER_IDColumn.ColumnName].ToString() + ",";

                            using (DataManager dmUpdate = new DataManager(SQLCommand.CongregationLedgers.InsertLedgersMappedWithCongregationLedger))
                            {
                                dmUpdate.Parameters.Add(this.AppSchema.GenerlateReport.CON_NATURE_IDColumn.ColumnName, (int)connature);
                                dmUpdate.Parameters.Add(this.AppSchema.GenerlateReport.CON_LEDGER_IDColumn.ColumnName, ConLedgerId);
                                dmUpdate.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName, ledgerid);
                                dmUpdate.Parameters.Add("PROJECT_CATOGORY_GROUP_ID", 0, DataType.Int32);
                                dmUpdate.Parameters.Add(this.AppSchema.GenerlateReport.CON_OP_AMOUNTColumn.ColumnName, conopamount);
                                dmUpdate.Parameters.Add(this.AppSchema.GenerlateReport.CON_OP_TRANS_MODEColumn, conoptransmode, DataType.Varchar);
                                dmUpdate.DataCommandArgs.IsDirectReplaceParameter = true;
                                result = dmUpdate.UpdateData();
                            }
                            if (!result.Success)
                            {
                                break;
                            }

                        }
                        selectedledgerids = selectedledgerids.TrimEnd(',');
                    }
                    //dm.EndTransaction();
                }
            }
            catch (Exception err)
            {
                result.Message = "Problem in Inert/Updating mapping Ledgers with Congregation  Ledgers " + err.Message;
            }
            finally
            {
                dtMappedLedgers.DefaultView.RowFilter = string.Empty;

            }


            return result;
        }

        /// <summary>
        /// On 20/03/2023, To delete existing FA and Deprecaition details and insert new values
        /// </summary>
        /// <returns></returns>
        public ResultArgs UpdateCongregationCurrentFADetails(DataTable dtFACurrentYear)
        {
            bool isBeginTransaction = false;
            resultArgs = new ResultArgs();
            using (DataManager dm = new DataManager(SQLCommand.CongregationLedgers.DeleteCongregationFACurrentYearDetails))
            {
                try
                {
                    //Delete existing FA current year details 
                    dm.BeginTransaction();
                    isBeginTransaction = true;
                    dm.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_FROMColumn, this.YearFrom);
                    dm.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_TOColumn, this.YearTo);
                    resultArgs = dm.UpdateData();
                    if (resultArgs.Success)
                    {
                        foreach (DataRow dr in dtFACurrentYear.Rows)
                        {
                            Int32 conledgerid = NumberSet.ToInteger(dr[this.AppSchema.GenerlateReport.CON_LEDGER_IDColumn.ColumnName].ToString());
                            Int32 ledgerid = NumberSet.ToInteger(dr[this.AppSchema.GenerlateReport.LEDGER_IDColumn.ColumnName].ToString());
                            double debit = NumberSet.ToDouble(dr[this.AppSchema.GenerlateReport.DEBITColumn.ColumnName].ToString());
                            double credit = NumberSet.ToDouble(dr[this.AppSchema.GenerlateReport.CREDITColumn.ColumnName].ToString());

                            if (debit > 0 || credit > 0)
                            {
                                using (DataManager dmUpdate = new DataManager(SQLCommand.CongregationLedgers.InsertCongregationFACurrentYearDetails))
                                {
                                    dmUpdate.Database = dm.Database;
                                    dmUpdate.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_FROMColumn, this.YearFrom);
                                    dmUpdate.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_TOColumn, this.YearTo);
                                    dmUpdate.Parameters.Add(this.AppSchema.GenerlateReport.CON_LEDGER_IDColumn.ColumnName, conledgerid);
                                    dmUpdate.Parameters.Add(this.AppSchema.GenerlateReport.LEDGER_IDColumn.ColumnName, ledgerid);
                                    dmUpdate.Parameters.Add(this.AppSchema.GenerlateReport.DEBITColumn.ColumnName, debit);
                                    dmUpdate.Parameters.Add(this.AppSchema.GenerlateReport.CREDITColumn.ColumnName, credit);
                                    dmUpdate.DataCommandArgs.IsDirectReplaceParameter = true;
                                    resultArgs = dmUpdate.UpdateData();

                                    if (!resultArgs.Success)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception err)
                {
                    resultArgs.Message = err.Message;
                }
                finally
                {
                    if (!resultArgs.Success)
                    {
                        dm.TransExecutionMode = ExecutionMode.Fail;
                    }

                    if (isBeginTransaction)
                    {
                        dm.EndTransaction();
                    }
                }
            }

            return resultArgs;
        }


        /// <summary>
        /// On 08/01/2022, To map project imported ledgers with existing ledgers
        /// </summary>
        /// <returns></returns>
        public ResultArgs MapProjectImportedLedgerWithLedgers(DataTable dtProjectImportLedgers, DataTable dtExistingLedgers)
        {
            ResultArgs result = new ResultArgs();
            DataTable dtAlreadyMappedLedgers = new DataTable();
            try
            {
                if (dtProjectImportLedgers.Rows.Count > 0)
                {
                    //1. Delete mapped ledgers which are not  available in existing ledgers
                    using (DataManager dm = new DataManager(SQLCommand.LedgerBank.DeleteMapImportedProjectLedgerNotExists))
                    {
                        result = dm.UpdateData();
                    }

                    if (result.Success)
                    {
                        //2.Get already Mapped
                        //DataTable dtalreadymappedLedgers = 
                        using (DataManager dm = new DataManager(SQLCommand.LedgerBank.FetchMappedImportedProjectLedger))
                        {
                            result = dm.FetchData(DataSource.DataTable);
                            if (result != null && result.Success)
                            {
                                dtAlreadyMappedLedgers = result.DataSource.Table;
                            }
                        }

                        //3. Map Project Imported Ledgers with existing ledgers
                        foreach (DataRow dr in dtProjectImportLedgers.Rows)
                        {
                            string ledgername = dr["LEDGER_NAME"].ToString().Trim();
                            //Check in already mapped ledgers
                            ResultArgs resultFind = UtilityMethod.CheckValueCotainsInDataTable(dtAlreadyMappedLedgers, "IMPORT_LEDGER_NAME", ledgername);
                            if (resultFind != null && resultFind.Success)
                            {
                                Int32 existingledgerid = 0;
                                string existingledgername = string.Empty;
                                if (resultFind.DataSource.Table != null)
                                {
                                    DataTable dtResult = resultFind.DataSource.Table;
                                    if (dtResult != null && dtResult.Rows.Count > 0)
                                    {
                                        existingledgername = dtResult.Rows[0][AppSchema.ProjectImportExport.LEDGER_NAMEColumn.ColumnName].ToString().Trim();
                                        existingledgerid = NumberSet.ToInteger(dtResult.Rows[0][AppSchema.ProjectImportExport.LEDGER_IDColumn.ColumnName].ToString().Trim());
                                    }
                                }
                                else //Check in same ledger matching
                                {
                                    resultFind = UtilityMethod.CheckValueCotainsInDataTable(dtExistingLedgers, AppSchema.ProjectImportExport.MERGE_LEDGER_NAMEColumn.ColumnName, ledgername);
                                    if (resultFind != null && resultFind.Success && resultFind.DataSource.Table != null)
                                    {
                                        DataTable dtResult = resultFind.DataSource.Table;
                                        if (dtResult != null && dtResult.Rows.Count > 0)
                                        {
                                            existingledgername = dtResult.Rows[0][AppSchema.ProjectImportExport.MERGE_LEDGER_NAMEColumn.ColumnName].ToString().Trim();
                                            existingledgerid = NumberSet.ToInteger(dtResult.Rows[0][AppSchema.ProjectImportExport.MERGE_LEDGER_IDColumn.ColumnName].ToString().Trim());
                                        }
                                    }
                                }

                                if (existingledgerid > 0 && !string.IsNullOrEmpty(existingledgername))
                                {
                                    dr.BeginEdit();
                                    dr[AppSchema.ProjectImportExport.MERGE_LEDGER_IDColumn.ColumnName] = existingledgerid;
                                    dr[AppSchema.ProjectImportExport.MERGE_LEDGER_NAMEColumn.ColumnName] = existingledgername;
                                    dr.EndEdit();
                                }
                            }
                        }
                    }
                    result.Success = true;
                    result.DataSource.Data = dtProjectImportLedgers;
                }
            }
            catch (Exception err)
            {
                result.Message = "Problem in Map/Update mapping Project Import Ledgers with existing Ledgers " + err.Message;
            }
            return result;
        }

        public bool IsCurrencyEnabledCashBankLedgerExists()
        {
            bool rtn = false;
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchAllCashBankLedger))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);

                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    DataTable dtCurrencyEnabledCashBank = resultArgs.DataSource.Table;
                    dtCurrencyEnabledCashBank.DefaultView.RowFilter = AppSchema.Ledger.CUR_COUNTRY_IDColumn.ColumnName + " > 0";
                    rtn = (dtCurrencyEnabledCashBank.DefaultView.Count > 0);
                }
            }

            return rtn;
        }

        public bool IsCurrencyEnabledCashBankLedgerExists(Int32 CurrencyCountryId)
        {
            bool rtn = false;
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchAllCashBankLedger))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);

                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    DataTable dtCurrencyEnabledCashBank = resultArgs.DataSource.Table;
                    dtCurrencyEnabledCashBank.DefaultView.RowFilter = AppSchema.Ledger.CUR_COUNTRY_IDColumn.ColumnName + " = " + CurrencyCountryId;
                    rtn = (dtCurrencyEnabledCashBank.DefaultView.Count > 0);
                }
            }

            return rtn;
        }

        /// <summary>
        /// 08/01/2022, To Inssert or update Imported Ledger's Mapping
        /// </summary>
        /// <param name="dtProjectImportLedgers"></param>
        /// <returns></returns>
        public ResultArgs InsertUpdateProjectImportedLedgersWithLedgers(DataTable dtProjectImportLedgers)
        {
            ResultArgs result = new ResultArgs();
            try
            {
                //Insert/Update Project exported Mapped ledgers
                foreach (DataRow dr in dtProjectImportLedgers.Rows)
                {
                    using (DataManager dm1 = new DataManager(SQLCommand.LedgerBank.InsertUpdateMapImportedProjectLedger))
                    {
                        string existingledgername = dr[AppSchema.ProjectImportExport.LEDGER_NAMEColumn.ColumnName].ToString().Trim();
                        Int32 existingledgerid = NumberSet.ToInteger(dr[AppSchema.ProjectImportExport.MERGE_LEDGER_IDColumn.ColumnName].ToString().Trim());

                        if (existingledgerid > 0 && !String.IsNullOrEmpty(existingledgername))
                        {
                            dm1.DataCommandArgs.SQLCommandId = SQLCommand.LedgerBank.InsertUpdateMapImportedProjectLedger;
                            dm1.Parameters.Add("MAP_LEDGER_ID", existingledgerid, DataType.Int32);
                            dm1.Parameters.Add("IMPORT_LEDGER_NAME", existingledgername, DataType.String);
                            result = dm1.UpdateData();
                        }
                        else
                        {
                            result.Message = "";
                        }

                        if (!result.Success)
                        {
                            break;
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

        /// <summary>
        /// On 26/02/2024, To get list of ledger opening blance which are not mapped with concern Project
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchLedgerOpeningBalanceNotMappedWithProject()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerOpeningBalanceNotMappedWithProject))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 16/04/2024, To clear Invalid Ledger Balance data
        /// </summary>
        /// <returns></returns>
        public ResultArgs ClearInvalidLedgerBalanceData()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.ClearInvalidLedgerBalanceData))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// On 26/02/2024, To get list of ledgers in vouchers which are not mapped with concern Project
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchVoucherLedgersNotMappedWithProject()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchVoucherLedgersNotMappedWithProject))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 08/03/2024, To get list of Project Budget ledgers which are not mapped with concern Project
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchProjectBudgetLedgersNotMappedWithProject()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchProjectBudgetLedgersNotMappedWithProject))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 08/03/2024, To get list of Budget ledgers which are not mapped with concern Project Budget Ledger
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchBudgetLedgersNotMappedWithProjectBudgetLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchBudgetLedgersNotMappedWithProjectBudgetLedger))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 27/02/2024, To get list of fd ledger's fd opening are not matched with ledger openingbalance
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchFDLedgersMismatchingOpeningBalance()
        {
            using (DataManager dataManager = new DataManager(this.AllowMultiCurrency == 1 ?
                            SQLCommand.LedgerBank.FetchFDLedgersMismatchingOpeningBalanceFC : SQLCommand.LedgerBank.FetchFDLedgersMismatchingOpeningBalance))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 27/02/2024, To get list down more than one opening balance date in ledger balance 
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchMorethanOneLedgerBalanceDate()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchMorethanOneLedgerBalanceDate))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }



        #endregion
    }
}
