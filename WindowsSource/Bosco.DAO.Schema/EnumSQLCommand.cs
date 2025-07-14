/*  Class Name      : EnumActivityDataCommand.cs
 *  Purpose         : Enum Data type for Indetifying SQL Statement from UI request
 *  Author          : CS
 *  Created on      : 02-Aug-2010
 */

namespace Bosco.DAO.Schema
{
    public class SQLCommand
    {
        public enum User
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
            Authenticate,
            CheckOldPassword,
            ResetPassword,
            FetchUserId,
            FetchUserProfile,
            FetchAllShortcuts,
            FetchVoucherUsers,
            AddLogo,
            DeleteLogo,
            FetchLogo
        }

        public enum State
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
            FetchState,
            GetStateId,
            FetchStateByCountryID,
            FetchStateByStateName,
        }

        public enum Audit
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,

        }

        public enum DashBoard
        {
            FetchChartInfo,
            FetchMaturedFD,
            FetchDatabases,
            CheckDatabaseExists,
            InsertRestoredDatabase,
            DropDatabase,
            UpdatePayrollSymbols,
            FetchProjectsbySociety
        }
        public enum Budget
        {
            FetchById,
            FetchByStatisticId,
            isExistsMonthlyBudget,
            FetchAll,
            FetchBudgetNames,
            FetchBudgetNamesByTwoMonths,
            FetchRecentBudgetList,
            AddNewBudgetFetchLedger,
            FetchBudgetLedgerAll,
            DeleteBudgetLedgerById,
            DeleteBudgetSubLedgerById,
            DeleteBudgetDistributionById,
            DeleteBudgetDistributionIDandDate,
            DeleteBudgetDistributionbyBudgetandDate,
            DeleteBudgetProjectById,
            DeleteBudgetStatisticsDetails,
            CheckStatus,
            AddPeriod,
            AddAnnual,
            AddStatisticDetails,
            BudgetLoad,
            Update,
            Delete,
            DeleteAllotFund,
            AnnualBudgetLedgerAdd,
            SaveLedgerSubLedger,
            FetchMysoreBudget,
            BindGrid,
            AnnualBudgetDistributionAdd,
            AnnualBudgetProjectAdd,
            BudgetLedgerAdd,
            BudgetSubLedgerAdd,
            BudgetSubLedgerEdit,
            BudgetLedgerUpdate,
            BudgetLedgerDelete,
            FetchMappedLedgers,
            ChangeStatusToInActive,
            FetchBudgetBalance,
            CheckBudgetByDate,
            FetchbyBudgetProject,
            AddAllotFund,
            UpdateAllotFund,
            FetchAllotFund,
            GetLedgerExist,
            GetRandomMonth,
            FetchBudgetByProject,
            ImportBudget,
            FetchBudgetAmount,
            FetchBudgetMonthDistributionAmount,
            AnnualBudgetFetchAdd,
            AnnualBudgetFetchEdit,
            BudgetAddEditDetails,
            BudgetAddEditDetailsAllLedgers,
            BudgetMysoreAddEditDetails,
            BudgetMonthlyDistribution,
            AnnualBudgetFetch,
            ExistMonthDistributionCount,
            CalendarYearBudget,
            AnnualBudgetProject,
            //InsertBudgetCostCentreDetails,
            //DeleteBudgetCCdetailsByBudgetId,
            FetchCostCentreByLedger,
            CheckForBudgetEntry,
            FetchBudgetedProjects,
            FetchBudgetProjectforLookup,
            FetchLastBudgetMonth,
            FetchProjectforBudget,
            UpdateBudgetAction,
            IsBudgetStatisticsExists,
            IsBudgetIncomeLedgerExists,
            FetchBudgetLedgerGroup,
            FetchBudgetIdByDateRangeProject,
            FetchUserDefinedBudgetDetails,
            FetchUserDefinedBudgetBalances,
            DeleteUserDefinedBudgetDetailsByYear,
            UpdateUserDefinedBudgetDetails,

            IsBudgetGroupExists,
            IsBudgetSubGroupExists,
            SaveBudgetGroup,
            SaveBudgetSubGroup,
            UpdateBudgetGroup,
            UpdateBudgetSubGroup,
            DeleteBudgetGroup,
            DeleteBudgetSubGroup,


            CheckAllCommunityLedger,
            DeleteAllCommunityLedger,
            InsertAllCommunityLedger,
            UpdateAllCommunityLedger,

            FetchBudgetCostCentre,
            DeleteBudgetCostCentre,
            UpdateBudgetCostCentre,

            FetchNewDevelopmentProjectsByFY,
            FetchUnLinkedNewProjects,
            UpdateUnLinkedNewProjects,

        }

        public enum SubLedger
        {
            isExistsMonthlyBudget,
            FetchBySubLedger,
            BudgetSubLedgerEdit,
            BudgetSubLedgerAdd,
            IsExistSubLedger,
            MapLedgerwithSubledger,
            DeleteBudgetLedgerById,
            BindGrid,
            FetchBudgetLedgerGroup

        }
        public enum Ledger
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
        }

        public enum Bank
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
            FetchforLookup,
            SetBankAccountSource,
            SelectAllBank,
            SelectAllFD,
            FetchFDByProject,
            FetchBankByProject,
            FetchAllBankAccounts,
            FetchAllCashLedgerByProject,
            FetchBankCodes,
            FetchSettingBankAccount,
            FetchBankCount,
            FetchBankByBankCode,
            FetchBankDetailsByProjectIds,
            FetchBankandBaranchByProjectId,
            FetchBankFDAccountDetailsByProjectId,
            FetchBankVoucher,
            FetchBankIdByLedgerId
        }

        public enum Setting
        {
            Fetch,
            FetchReportSetting,
            InsertUpdate,
            InsertUpdateReportSetting,
            InsertUpdateUI,
            Update,
            FetchCurrentDate,
            FetchChequePrintingSetting,
            DeleteChequePrintingSetting,
            InsertUpdateChequePrintingSetting,
            InsertACSignDetails,
            InsertACAuditorNoteSignDetails,
            DeleteACSignDetails,
            DeleteSign,
            InsertUpdateSignDetail,
            UpdateSignDetails,
            UpdateSignDetailsForAllProjects,
            FetchSignDetails,
            FetchBudgetNewProjects,
            FetchBudgetNewProjectsCCDetailsByAcYear,
            FetchBudgetStrengthDetails,
            DeleteBudgetNewProjectsByAcYear,
            DeleteBudgetNewProjectsCCDetailsByAcYear,
            DeleteBudgetStrengthDetails,
            UpdateBudgetNewProjectsByAcYear,
            UpdateBudgetNewProjectsCCDetailsByAcYear,
            UpdateBudgetStrengthDetails,
            ExistsBudgetNewProjectsByAcYear,

            //SaveAuditorSignNote,
            //FetchAuditorSignNote,

            InsertAuditorNoteSign,
            FetchAuditorNoteSign,
            DeleteAuditorNoteSign,

            FetchMultiDBXMLConfigurationInAcperp,
            InsertMultiDBXMLConfigurationInAcperp,
            UpdateMultiDBXMLConfigurationInAcperp,

        }

        public enum UISetting
        {
            FetchUI,
            BaseAcmeerpFetchUI,
            BaseAcmeerpInsertUpdateUI,
            InsertUpdateUI,
            DeleteUI,


        }

        public enum DonorAuditor
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchDonor,
            FetchDonorByStatus,
            FetchMailingThanksByStatus,
            FetchMailingContributedStatus,
            FetchAuditor,
            FetchAuditorList,
            FetchAuditType,
            DeleteDonourDetails,
            GetDonorId,

            AddDonorReferenceDetails,
            UpdateDonorReferenceDetails,
            DeleteDonorRefDetails,
            FetchDonorReferenceDetails,
            FetchInstitutionalTypeByName,
            FetchPaymentModeIdByPaymentMode,
            FetchRegTypeByRegTypeId,
            FetchReasonForInactive,
            FetchDonorByname,
            GetIdDonorName
        }
        public enum DonorTitle
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
        }
        public enum DonorProspect
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
            FetchRegistrationType,
            FetchInstitutionalType,
            FetchDonorPaymentMode,
            FetchProspectByname,
            FetchSearchByName,
            GetDonorRegStatus,
            GetRegistrationId,
            GetInstitutionId,
            GetLanguage,
            GetStateDonaud,
            GetDonaudByStateID,
            GetTagID,
            GetProspectId,
            GetDonorTags
        }

        public enum InstutionType
        {
            Add,
            Update,
            Delete,
            FetchAll,
            FetchById
        }

        public enum DonorRegistrationType
        {
            Add,
            Update,
            Delete,
            FetchAll,
            FetchRegistrationTypeByID
        }

        public enum MasterDonorReference
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll
        }

        public enum AddressBook
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchDonor,
            FetchAuditor,
            FetchOthers,
            FetchAll,
            FetchAuditorList
        }

        public enum FDAccount
        {
            FetchLedgers,
            FetchProjectByLedger,
            DeleteProjectLedger,
            FetchAllProjectId,
            Add,
            Update,
            Delete,
            Fetch,
            FetchByLedgerId,
            FetchLedgerCurBalance,
            FetchFDById,
            FetchLedgerByProject,
            FetchProjectId,
            FetchLedgerBalance,
            DeleteFDAcountDetails,
            FetchFDRenewalById,
            FetchFDHistoryByFDId,
            GetLastFDRenewalDate,
            GetMaturityValue,
            FetchFDRegistersView,
            FetchFDFullHistory,
            FetchAccumulatedAmount,
            FetchRenewalByRenewalId,
            UpdateFDStatus,
            FetchVoucherId,
            FetchAccountIdByVoucherId,
            FetchPhysicalAccountIdbyVoucherId,
            FetchRenewalDetailsById,
            FetchFDStatus,
            CountFDRenewalDetails,
            FetchACIAmount,
            FetchRINAmount,
            FetchWithdrawAmount,
            FetchWithdrawAmountUptoCurrent,
            FetchRecentFDRenewal,
            FetchActualFDAccountByVoucherId,
            HasFDAccount,
            HasFlxiFD,
            HasFDAdjustmentEntry,
            DeleteFDAccountByVoucherId,
            DeletePhysicalFDAccountByVoucherId,
            FetchVoucherByAccount,
            FetchFDPostInterestDetailsById,
            GetNoOfPostInterest,
            GetNoOfPostInterestCount,
            GetNoofReInvestment,
            GetNoOfPostInterestByDateRange,
            FetchFDWithdrawalsByFDAccountId,
            FetchFDReInvestmentByFDAccountId,
            FetchPrinicpalAmountBydate,
            DeleteIntrestVouchersinVoucherTrans,
            DeleteIntrestVouchersinVoucherMasterTrans,
            UpdateFDRenewalVoucherIdByZero,
            UpdateFDScheme,
        }

        public enum LedgerGroup
        {
            Add,
            AddGeneralate,
            UpdateGeneralate,
            Update,
            Delete,
            Fetch,
            FetchAll,
            FetchforLookup,
            FetchforLedgerLookup,
            FetchByGroupId,
            FetchNatureId,
            FetchValidateGroup,
            FetchAccessFlag,
            UpdateImageIndex,
            FetchAccoutType,
            FetchLedger,
            FetchLedgerList,
            FetchLedgerGroupCodes,
            FetchFDLedger,
            IsLedgerGroupCode,
            IsLedgerGroupName,
            GetParentId,
            GetNatureId,
            GetLedgerGroupId,
            FetchDefaultLedgers,
            FetchLedgerGroupId,
            GetNatureIdByLedgerGroup,
            FetchSortOrder,
            FetchMainGroupSortOrder,
            FetchLedgerGroupByGroupCode,
            UpdateParentGroupId,
            FetchLedgerGroupByNature,
            FetchLedgerGroupNature,
            UpdateGeneralateParentGroupId,
            UpdateLedgerGroupByLedgerGroupId
        }

        public enum Country
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
            FetchCountryList,
            FetchCountryCodeList,
            FetchCurrencySymbolsList,
            FetchCurrencySymbols,
            FetchCurrencyCodeList,
            FetchCurrencyNameList,
            FetchCountryIdByName,
            FetchCountryIdByStateId,

            FetchCountryCurrencyExchangeRateByCountryDate,
            FetchCountryCurrencyExchangeRateByFY,
            FetchCountryCurrencyExchangeRate,
            DeleteCountryCurrencyExchangeRate,
            DeleteAllCountryCurrencyExchangeRate,
            UpdateCountryCurrencyExchangeRate,
            InsertACCountryCurrencyExchangeRate,
            DeleteACCountryCurrencyExchangeRate,
        }

        public enum CostCentre
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
            FetchforLookupByProjectLedger,
            FetchforLookupByProject,
            SetCostCentreSource,
            FetchCostCentreCodes,
            IsCostCentreExist,
            IsCostCentreNameExist,
            FetchCostCentreId,
            FetchCostCentreCategory,
            FetchCostCentreCategorybyId,
            FetchCostcentreByExistingCode,
            FetchforLookup
        }

        public enum MasterTransactionCostCentre
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
            FetchCostCentre,
            FetchCostCentreByLedger,
            MakeAsCostCenterLedger,
            AddReferenceNo
        }

        public enum InKindArticle
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll
        }

        public enum ExecutiveMembers
        {
            Add,
            Update,
            Delete,
            DeleteAll,
            Fetch,
            FetchAll,
            FetchSociety
        }

        public enum Mapping
        {
            // FetchCostCenterByIDs,
            // LedgerProjectShowMapping,
            //ProjectLedgerShowMapping,
            // FetchProjectByIDs,
            // FetchByIDs, 
            // LoadProjectByIDs,
            FetchMappedCostCenter,
            FetchMappedCostCenterByProjectLedger,
            FetchMappedCostCenterByCostCenterId,
            FetchMappedProject,
            ProjectCostCentreShowMapping,
            FetchProjectforLookup,
            IsExistMapVoucherTransaction,
            FetchPJLookUp,
            FetchProjectForGridView,
            LoadProjectMappingGrid,
            LoadProjectDonorGrid,
            LoadProjectCostCentreGrid,
            ProjectLedgerMappingDelete,
            ProjectBudgetLedgerMappingDelete,
            ProjectBudgetLedgerByBudgetLedgerProject,
            DeleteUnMappedProjectLedgersInProjectBudgetLedger,
            DeleteProjectOpBalance,
            UnMapProjectLedger,
            MapMigratedLedgers,
            MapMigratedLedgersByProject,
            FetchOpLedgers,
            DeleteMergedOpBalance,
            DeleteMergedLedgers,
            DeleteMergedLedgersByProject,
            UpdateLedgerCostCentre,
            UpdateLedgerTDS,
            IsExistTDSLedger,
            IsExistBudgetEnabledLedger,
            FetchCommonProjectForMerge,
            DeleteCommonProjectForMerge,
            FetchMappedFDByFDId,
            FetchMappedLedgers,
            ProjectLedgerMappingAdd,
            ProjectBudgetLedgerMappingAdd,
            DeleteProjectCostCenterMapping,
            DeleteProjectGeneralateMapping,
            DeleteProjectPurposeMapping,
            UnMapCostCentreByCCId,
            ProjectCostCentreMappingAdd,
            ProjectGeneralateMappingAdd,
            ProjectPurposeMappingAdd,
            LedgerProjectMappingDelete,
            LoadAllLedgers,
            LoadAllCostCentre,
            LoadAllCostCentreforcostcategory,
            LoadLedgerFD,
            IsMadeBudgetForLedger,
            LoadAllDonor,
            LoadAllPurpose,
            LoadGeneralateLedger,
            DonorMap,
            DonorUnMap,
            DonorUnMapByDonorId,
            FetchDonorMapped,
            FetchPurposeMapped,
            FetchMappedDonorByDonorId,
            FixedDepositId,
            BankIdByLedgerId,
            FetchProjects,
            MapLedgersToProject,
            MapCostCentreToProject,
            CheckLedgerMapped,
            CheckCostCentreMapped,
            CheckDonorMapped,
            LoadLedgerByProId,
            LoadLedgerByProjectIds,
            LoadCCLedgerByProjectId,
            LoadProjectFDLedgerGrid,
            CostcentreCostCategoryMappingAdd,
            FetchMappedCostCategory,
            DeleteMapCostCategory,
            DeleteCostCategory,
            CostCentreCategoryUnmap,
            FetchCostCentreUnmapTransaction,
            FetchFCPurposeUnmapTransaction,
            MapProjectLedger,
            MapProjectAgainstLedgerByCashBankLedger,
            MapProjectAgainstLedgerByCashBankLedgerForPeriod,
            FetchMappedActiveDonors,
            FetchDonorStatus,
            FetchcategoryLedgerByProject,
            subLedgerLoadLedgerByProdId,
            MapBudgetRecLedgerForAllProjects,
            FetchIEMergeLedgersBudget,
            FetchAIMergeLedgersBudget,
            DeleteBudgetLedgerAmountByLedger,
            DeleteMappedBudgetByLedger,

            MergeCashBankLedgersFromBeginning,
            MergeCashBankLedgersForPeriod,
            MergeCashBankLedgersOPBalance,

            FetchPurposeCostCentreDistribution,
            DeletePurposeCCDistribution,
            InsertUpdateProjectPurposeCCDistribution,

            CheckCostCentreMappingBySetting,
            FetchProjectLedgerCostCentreDistribution,
            UpdateProjectLedgerCCDistribution,
            UpdateProjectLedgerCCDistributionZero,
            UpdateProjectCostCentreOPBalance,
            DeleteAllCostCentreMapping,
            ChangeAllCostCentreMappingLedgerBased,
            ChangeAllCostCentreMappingProjectBased,

            FetchProjectLedgerApplicable,
            DeleteProjectLedgerApplicable,
            UpdateProjectLedgerApplicableByProject,

            FetchInvestmentType,
            FetchInvestmentTypeIdByInvestmentType,

            FetchBudgetActualLedgers,

        }

        public enum LedgerBank
        {
            Add,
            Update,
            UpdateByHeadOffice,
            UpdateClosedDate,
            HeadOfficeLedgerUpdate,
            Delete,
            Fetch,
            FetchAll,
            FetchAllIntegration,
            BankAccountAdd,
            BankAccountUpdate,
            BankAccountDelete,
            BankAccountFetch,
            LedgerIdFetch,
            FetchMergeLedgers,
            BankAccountIdFetch,
            BankAccountFetchAll,
            FixedDepositFetchAll,
            FetchLedgerForLookup,
            FetchBudgetGroupLookup,
            FetchBudgetSubGroupLookup,
            FetchLedgerByGroup,
            FetchGSTId,
            FetchGSTLedgerId,
            FetchCashBankFDLedger,
            FetchAllHSNSACCode,
            IsBankLedger,
            FetchCostCenterId,
            FetchLedgerNature,
            SetLedgerSource,
            SetLedgerDetailSource,
            FetchLedgerGroupbyLedgerId,
            FetchBankAccountById,
            UpdateFDBankAccount,
            FetchLedgerByLedgerGroup,
            FetchMaturityDate,
            FetchCashBankLedger,
            CheckProjectExist,
            FetchLedgerCodes,
            FetchBankAccountCodes,
            FetchFixedDepositCodes,
            FetchBankInterestLedger,
            FetchFDLedgers,
            FixedDepositByLedger,
            FetchFDLedgerById,
            FDLedgerUpdate,
            FetchAccessFlag,
            IsCashLedgerExists,
            IsLedgerNameExists,
            IsLedgerCodeExists,
            IsBudgetedLedger,
            FetchDefaultLedgers,
            FetchLedgerId,
            FetchHOLedgerId,
            FetchLedgerByIncludeCostCentre,
            FetchHighValuePaymentbyLedgers,
            FetchLocalDonationLedgers,
            FetchLedgerByIncludeGST,
            UpdateLedgerOptions,
            UpdateLedgerOptionsCostcentre,
            UpdaterLedgerOptionHighValuePayments,
            UpdateLedgerOptionLocalDonations,
            FetchLedgerByIncludeBankInterest,
            FetchLedgerByIncludeBankFDPenaltyLedger,
            UpdateLedgerOptionsBankFDPenaltyLedgersSetDisableAll,
            UpdateLedgerOptionsBankFDPenaltyLedgers,
            FetchLedgerByIncludeBankSBInterest,
            FetchLedgerByIncludeBankCommission,
            UpdateLedgerOptionsBankInterestsetone,
            UpdateLedgerOptionsBankInterestsetzero,
            UpdateLedgerOptionsBankSBInterestByLedger,
            UpdateLedgerOptionsHighValuePaymentByLedger,
            UpdateLedgerOptionsLocalDonationByLedger,
            UpdateLedgerOptionsBankSBInterestsetDisableAll,
            UpdateLedgerOptionsBankCommissionByLedger,
            UpdateLedgerOptionsBankCommissionDisableAll,
            CheckTransactionMadeByLedger,
            CheckFDTransactionMadeByLedger,
            CheckFDTransactionMadeByInterestLedger,
            HeadOfficeAdd,
            FetchMaxLedgerID,
            IsTDSLedger,
            CheckTransactionExistsByDateClose,
            CheckTransactionExistsByDateFrom,
            FetchReferedVoucherByLedgerId,
            DeleteLedgerReferenceNo,
            UpdateLedgerReferenceNo,
            CheckBankAccountMappedToProject,
            CheckBankAccountMappedToLegalEntity,
            FetchUnmappedBankAccounts,
            FetchLedgersByLedgercode,
            FetchBankAccountsByCode,
            FetchTDSLedger,
            FetchTDSExpNOP,
            FetchLedgerIdByLedger,
            FetchDutiesTaxLedger,
            FetchLedgerByFixedGroup,
            FetchLedgerByNature,
            FetchAllCashBankLedger,
            FetchAllCashBankLedgerByProject,
            FetchLedgerName,
            FetchGroupName,

            FetchLedgerByIncludeInkindLedger,
            UpdateLedgerOptionsInkindLedgersetone,
            UpdateLedgerOptionsInkindLedgersetzero,
            UpdateLedgerOptionsGainLedgerssetzero,
            UpdateLedgerOptionsLossLedgerssetzero,
            UpdateLedgerOptionsGainLedgerssetone,
            UpdateLedgerOptionsLossLedgerssetone,

            FetchLedgersByEnableAssetGainLedger, // Loading Gain Ledgers to be mapped in the Ledger option screen.
            FetchLedgersByEnableAssetLossLedger, // Loading Loss Ledgers to be mapped in the Ledger option screen.
            FetchLedgersByEnableAssetDisposalLedger, // Loading Disposal Ledgers to be mapped in the Ledger option screen.
            FetchLedgersByEnableSubsidyLedger,  // Loading Subsidy Ledgers to be mapped in the Ledger option screen.

            CheckTransactionMadeByAssetGainLedger, // Checking any voucher is exists or not by this Asset Gain Ledger.
            CheckTransactionMadeByAssetLossLedger, // Checking any voucher is exists or not by this Asset Loss Ledger.
            CheckTransactionMadeByAssetDisposalLedger, // Checking any voucher is exists or not by this Asset Loss Ledger.

            FetchLedgerByIncludeDepreciationLedger,
            UpdateLedgerOptionsDepreciationsetone,
            UpdateLedgerOptionsDepreciationsetzero,
            UpdateLedgerOptionsDisposalsetone,
            UpdateLedgerOptionsDisposalsetzero,
            UpdateLedgerOptionsSubsidyone,
            UpdateLedgerOptionsGSTone,
            InsertCreditorsOptionsGST,
            UpdateCreditorsOptionsGST,
            UpdateLedgerOptionsSubsidyzero,
            UpdateLedgerOptionsGSTZero,
            FetchLedgerIdByName,
            FetchLossLedgers,
            FetchGainLedgers,
            FetchAllInkindLedgers,
            FetchAllDisposalLedgers,
            FetchAllExpenceLedgers,
            FetchCashBankLedgerByID,
            FetchAllUnusedLedgers,
            FetchLedgerById,
            FetchHOLedgerForMerge,
            FetchBOLedgerForMerge,
            FetchBOLedgers,
            DeleteAllUnusedLedgers,
            FetchAllUnusedGroups,
            DeleteAllUnusedGroups,
            ChangeLedgerNameInMaster,
            ChangeLedgerNameInHOMaster,
            ClearAllDeletedDataByLedger,
            FetchGroupIdByLedgerName,
            UpdateBudgetGroupDetails,
            GetSDBINMAuditorSkippedLedgerIds,
            UpdateBudgetGroupRecurringByLedgerName, //For Temp Purpose
            UpdateMysoreBudgetSubGroupRecurringByLedgerName, //For Temp Purpose
            FetchBankBranch,
            FetchLedgerClosedDateById,
            FetchBankLedgerClosedDateById,
            DeleteMapImportedProjectLedgerNotExists,
            InsertUpdateMapImportedProjectLedger,
            FetchMappedImportedProjectLedger,
            CheckFDInterestLedgerVoucherExists,
            CheckFDPenaltyLedgerVoucherExists,
            FetchClosedLedgersByDate,
            FetchLedgerOpeningBalanceNotMappedWithProject,
            FetchVoucherLedgersNotMappedWithProject,
            FetchFDLedgersMismatchingOpeningBalance,
            FetchFDLedgersMismatchingOpeningBalanceFC,
            FetchMorethanOneLedgerBalanceDate,
            FetchProjectBudgetLedgersNotMappedWithProject,
            FetchBudgetLedgersNotMappedWithProjectBudgetLedger,
            ClearInvalidLedgerBalanceData,
            FetchCashBankCurrencySymbolByLedger,

            FetchLedgerByNatureAll
        }

        public enum CongregationLedgers
        {
            FetchCongregationLedgers,
            FetchLedgersMappedWithCongregationLedgers,
            FetchCongregationFixedAssetDetails,

            InsertUpdateCongregationLedgers,
            LedgersMappedWithCongregationLedgers,
            DeleteLedgersMappedWithCongregationByConLedgerId,
            InsertLedgersMappedWithCongregationByConLedgerId,
            InsertLedgersMappedWithCongregationLedger,

            DeleteCongregationFACurrentYearDetails,
            InsertCongregationFACurrentYearDetails,

            InsertUpdateDefaultCongregationLedgers,
            InsertUpdateDefaultMappingWithCongregationLedgers,
        }

        public enum FixedDeposit
        {
            FixedDepositAdd,
            FixedDepositUpdate,
            FixedDepositDelete,
            FixedDepositFetch,
            FixedDepositFetchAll,
            BreakUpAdd,
            BreakUpDelete,
            BreakUpFetchByAccountNo,
            FetchFDByID,
            UpdateFD,
            FDRegisterAdd,
            FetchFDNumber,
            FDRegisterUpdate,
            FetchFDAccountByMaturityDate,
            ChangeProjectForOPFD,
            ChangeProjectForInvestmentFD,
            IsFDVouchersExists,
            FetchFDMasterByFDAccountId,
            FetchFDRenewalsByFDAccountId,
            FetchFDRenewalsWithdrwals,
            FetchFDAccountsExistsByInvestmentType,

            CorrectACKPMAFDMutualFund_Temp,
        }

        public enum Voucher
        {
            Add,
            Update,
            Delete,
            FetchByVoucherId,
            FetchByVoucherTypeName,
            FetchAll,
            FetchVoucherNumberFormat,
            FetchVoucherNumberFormatByProject,
            UpdateLastVoucherNumber,
            InsertVoucherNumber,
            InsertGeneratedNumber,
            FetchVoucherNumberFormatExist,
            FetchGenerateNumberExist,
            FetchPreviousVoucherNumberFormatExist,
            FetchLastResetMonth,
            CashBankVoucherReceipts,
            CashBankVoucher,
            CashBankVoucherContra,
            GSTInvoiceVoucher,
            JournalVoucher,
            DeleteVoucherNumberFormat,
            DeleteVoucherNumberFormatByTransType,
            CheckNarrationEnabledByTransType,

            // Asset ID
            UpdateLastAssetRunning,
            UpdateAssetID,
        }

        public enum LegalEntity
        {
            Add,
            Update,
            Delete,
            FetchAll,
            FetchByID,
            CheckLegalEntity
        }

        public enum GSTDetails
        {
            Add,
            Edit,
            Delete,
            FetchById,
            FetchGSTList,
            FetchGSTLedgerClass,
            FetchZeroLedgerClassId,
            FetchAll
        }

        public enum Project
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
            FilterVoucherTypeById,
            DeleteProjectVouchers,
            FetchDivision,
            FetchVoucherTypes,
            FetchSocietyNames,
            ITRGroupNames,
            FetchProjects,
            FetchProjectsIntegration,
            FetchVouchers,
            FetchLedgers,
            AddProjectVouchers,
            AvailableVoucher,
            ProjectVoucher,
            FetchProjectList,
            ProjectCostCentreShowMapping,
            ProjectCategory,
            FetchDefaultVouchers,
            FetchAvailableVouchers,
            FetchVoucherDetailsByProjectId,
            FetchRecentProject,
            DeleteProject,
            DeleteProjectLedgerBalance,
            DeleteVoucher,
            FetchProjectCodes,
            FetchProjectDetails,
            FetchDefaultProjectVouchers,
            FetchSelectedProjectVouchers,
            LoadAllLedgerByProjectId,
            DeleteProjectLedger,
            CheckLedgerBalanceForProject,
            FetchProjectId,
            IsProjectCodeExists,
            IsProjectNameExists,
            DeleteProjectMappedLedgers,
            UpdateImportMasterProjectNames,
            FetchProjectBySociety,
            FetchProjectByITRGroup,
            FetchTransactionDeatilsByProjectId,
            FetchProjectnameByProjectCode,
            FetchProjectIdByProjectName,
            FetchProjectNameByProjectId,
            DeleteProjectMasterVoucher,
            DeleteProjectTransVoucher,
            DeleteProjectTransCostCentre,
            DeleteProjectTransRenewal,
            DeleteProjectTransFDAccount,
            FetchDeletedVouchersByProject,
            UpdateClosedDate,
            //UpdateProjectCurrencyDetails
        }

        public enum Purposes
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
            FetchMappedAll,
            FetchPurposeCodes,
            isPurposeExists,
            CheckFCPurposeMapped
        }

        public enum AuditInfo
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll
        }

        public enum AccountingPeriod
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
            FetchAllDetails,
            FetchForSettings,
            UpdateStatus,
            UpdateBooksbeginningDate,
            FetchIsFirstAccountingyear,
            FetchBooksBeginingFrom,
            FetchTransactionYearTo,
            ValidateBooksBegining,
            FetchActiveTransactionperiod,
            CheckIstransacton,
            FecthRecentProjectDetails,
            CheckExistingDB,
            FetchRecentVoucherDate,
            FetchLeastDate,
            FetchmaxDate,
            CheckAccountingPeriod,
            IsAccountingPeriodExists,
            FetchPreviousYearAC,
            VerifyAccountingPeriods
        }

        public enum UserRights
        {
            Add,
            Update,
            Fetch,
            FetchProjectMapped,
            FetchUserProject,
            FetchAll,
            UpdateUserRights,
            UpdateUserProject,
            DeleteUserProject,
            DeleteUserRights,
            FetchUserRightsByRole
        }

        public enum ProjectCatogory
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
            IsProjectCategory,
            FetchProjectCategoryId
        }

        public enum CostCentreCategory
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
            IsCostCentreCategory,
            FetchCostCentreCategoryId,
            CheckCostcentreCostCategoryExists
        }
        public enum UserRole
        {
            Add,
            Edit,
            Delete,
            Fetch,
            FetchAll,
            FetchUserRole,
            FetchAuditorUserDetailsByName,
        }

        public enum ManageSecurity
        {
            Edit,
            Fetch,
            FetchUserRole
        }

        public enum MasterRights
        {
            FetchByMasterName
        }

        public enum VoucherMaster
        {
            Add,
            Update,
            Delete,
            DeleteReference,
            DeleteVoucherReferenceNo,
            IsExistReferenceVoucherTrans,
            DeleteRefererdVoucherTrans,
            DeleteRefererdVouchersByJournalVoucher,
            FetchPayIdByJournalVoucherId,
            IsExistReferenceNo,
            IsExistGSTInvoiceNo,
            IsExistGSTVouchers,
            IsExistGSTVouchersByGSTClassId,
            IsExistGSTVendorVouchers,
            IsZeroValuedCashBankExistsInVouchers,
            IsVouchersExists,
            IsCashBankEnbled,
            PhysicalDelete,
            DeletePhysicalCostCentreTrans,
            Fetch,
            FetchAll,
            FetchMasterByID,
            FetchMasterDetails,
            FetchDeleteMasterDetails,
            FetchMasterAuditLogHistory,
            FetchVouchersForAuthorization,
            FetchVoucherStartingNo,
            IsTransactionMadeForProject,
            IsTransactionMadeForDonor,
            IsTransMadeSigleLedger,
            IsLedgerMapped,
            IsTransactionMadeForLedger,
            IsVoucherMadeForCountry,
            IsReceiptPaymentVoucherMadeForOtherNatures,
            FetchLastVoucherDate,
            FetchBRS,
            FetchBRSByMaterialized,
            UpdateBRS,
            FetchJournalDetails,
            FetchJournalTransDetails,
            CheckProjectExist,
            VoucherFDInterestAdd,
            VoucherFDInterestDelete,
            FetchFDVoucherInterest,
            FetchFDVoucherInterestByVoucherId,
            FetchFDVoucherPostedInterest,
            FetchReGenerationVouchers,
            UpdateVoucherNumber,
            ValidateDeletedVoucher,
            ValidateManagementCode,
            FetchVoucherMethod,
            AddTallyMigration,
            UpdateTallyTransNarration,
            FetchVoucherByProjectId,
            DeleteVouchers,
            FetchActiveVoucherCounts,
            CheckTransExists,
            CheckTransExistsByDateProject,
            CheckTransExistsByProject,
            CheckFirstVoucherDateByProject,
            CheckLastVoucherDateByProject,
            CheckFDVoucheExistsByProjectAndDateRange,
            CheckFDVoucheExistsByProject,
            AutoFetchNarration,
            FetchNarration,
            BulkUpdateNarration,
            FetchAutoFetchNames,
            IsCostCenterLedger,
            IsInkindLedger,
            IsTDSLedger,
            IsGStLedger,
            IsIGSTLedgerApplied,
            FetchVoucherIdByClientRefCodeBTransId,
            FetchVoucherTransforSSP,
            FetchVoucherTransforSSPDeletion,
            FetchVoucherTransforSSPUnique,
            FetchVoucherIdByClientRefId,
            FetchVoucherIdByCash,
            FetchVoucherIdByIndividualCash,
            FetchVoucherOnlineCollections,
            FetchOnlineVDateMDateCollections,
            FetchMatNullVouchers,
            FetchVoucherTransforSSPEmpty,
            FetchNegativeBalanceHistory,
            FetchVouchersForInsert,
            FetchVoucherViewCostcentre,
            FetchDeletedVoucherViewCostCentre,
            FetchTDSBookingId,
            ChangeCancelledVoucherStatus,
            FetchPostIdByVoucherId,
            DeleteDonorVouchers,
            DeleteVoucherImport,
            CheckLedgerMappedByProjectVoucher,
            CheckLedgerMappedByProject,
            CheckVoucherTypeMappedByProject,
            CheckVoucherDeleted,
            FetchReferenceBalance,
            FetchVoucherLedgerReferenceDetails,
            FetchVoucherLedgerSubLedgerVouchers,
            FetchLedgerByAmount,
            DeleteRefNumberDetails,
            UpdateModifiedDetailsbyLedgerIds,
            UpdateModifiedDetailsbyVoucherIds,
            IsExistAuditLogHistoryExitsByUserOrAuditor,
            InsertVoucherAuditLogHistory,
            UpdateVoucherAuditLogHistory,
            UpdateAuditorModifiedFlag,
            IsVoucherModifiedByAuditor,
            FetchVoucherAuditUserDetails,
            FetchVoucherPreviousDetailsForAuditLog,
            FetchVoucherAuditLogHistoryDetails,
            FetchVoucherIdByClientRefCode,
            SaveGSTInvoiceMasterDetails,
            UpdateGSTInvoiceMasterDetails,
            SaveGSTInvoiceVocuhersByVoucherId,
            UpdateGSTInvoiceVocuhersByVoucherId,
            UpdateGSTInvoiceMasterStatus,
            DeleteRandPVoucherAgainsInvoiceById,
            FetchGSTInvoiceMasterDetailsById,
            FetchGSTInvoiceMasterLedgerDetailsByGSTInvoiceId,
            FetchGSTInvoiceVouchersById,
            FetchGSTInvoiceIdByVoucherId,
            DeleteGSTInvoiceDetailsById,

            DeleteGSTInvoiceLedgerDetails,
            UpdateGSTInvoiceLedgerDetails,

            UpdateVoucherFileDetailsByVoucher,
            DeleteVoucherFileDetailsByVoucher,
            FetchVoucherFileDetailsByVoucher,
            IsExistsVoucherFiles,

            FetchGSTPendingInvoices,
            FetchGSTPendingInvoicesBookingDetailsByInvoiceId,
            FetchLastDateSyncDetails,

            AuthorizeVoucherByVoucherId,
            ReindexTables,
            OptimizeMainTables,

            FetchRandPVoucherAgainstJournalInvoiceByVoucherId,
            FetchRandPVoucherAgainstJournalInvoiceByInvoiceId,
        }

        public enum VoucherTransDetails
        {
            Add,
            Edit,
            Delete,
            Fetch,
            FetchAll,
            FetchTransactionByID,
            FetchTransactionDetails,
            FetchTransDetails,
            FetchVoucherTrans,
            FetchCashBankDetails,
            FetchReferedVoucherLedgerId,
            TransOPBalance,
            TransCBBalance,
            FetchJournalDetailById,
            FetchFixedDepositStatus,
            FetchFDOPBalance,
            TransFDCBalance,
            FetchMoveTransDetails,
            TransCBNegativeBalance,
            VoucherRouterAnalyzerStock,
            VoucherRouterAnalyzerAsset,
            VoucherRouterAnalyzerPayRole,
            FetchAssetCashBankDetails,
            FetchAssetInsuranceAMCDetails,
            FetchAssetCashBankDetailsForPurchase,
            UpdateSubLedgerVouchers,
            DeleteSubLedgerVouchers,
            ChequeNumberExists,
            FetchFundTransferList
        }

        public enum TransBalance
        {
            UpdateBalance,
            FetchTransaction,
            FetchOpBalanceList,
            FetchOpBalanceListGeneralate,
            FetchExpenseLedgerListBudget,
            FetchBudgetMappedLedgerList,
            FetchGeneralateMappedLedgerList,
            FetchOpBalance,
            FetchCashBankBaseCurrencyExchangeRate,
            FetchTotalLedgerOpBalance,
            FetchGroupSumBalance,
            FetchGroupSumBalancePreviousYears,
            FetchBalance,
            FetchBalanceByProjectwise,
            HasBalance,
            DeleteBalance,
            FetchLiquidGroupBalance,
            //FetchLedgerName,
            //FetchGroupName,
            //FetchCCOPBalance,
            FetchBalanceIE,
            BulkBalanceRefresh,
            AllProjectBalanceRefresh,
            ProjectLedgerBalanceRefreshByLedger,
            DeleteTransBalance,
            FetchBudgetLedgerBalance,
            FetchBudgetLedgerBalanceByTransMode,
            FetchSubLedgerBalance,
            ResetLedgerOpeningBalance,
            HasLedgerBalanceByDate
            //FetchOpeningBalance
        }

        public enum AcMePlusMigration
        {
            //------------------------------------------Clearing and Migrating Default---------------------------------------------------
            ClearData,
            MigrateUsers,
            CashDefaultLedger,
            FixedDepositeDefaultLedger,
            CapitalFundDefaultLedger,
            DefaultSateId,
            InsertCashDefaultLedger,
            InsertFixedDepositeDefaultLedger,
            InsertCapitalFundDefaultLedger,
            InsertDefaultState,
            //----------------------------------------- Clearing and Migrating Ends here-------------------------------------------------
            //------------------------------------------Migration of Accouting Years-----------------------------------------------------
            MigrateAcYears,
            FindAccountingYear,
            FindAccountingYearForTALLY,
            SetActiveAccountingYear,
            GetActiveAccountingYearId,
            GetLeastBookBeginningYear,
            GetLeastAccountingDate,

            //----------------------------------------Migration of Accouting Years ends here---------------------------------------------
            //----------------------------------------Migration of Bank------------------------------------------------------------------
            GetBankId,
            MigrateMasterBank,
            GetBankCode,
            GenerateBankCode,
            //----------------------------------------Migration of Bank ends here----------------------------------------------------------
            //----------------------------------------Migration of country-----------------------------------------------------------------
            MigrateCountry,
            GetCountryId,
            GetCountryCode,
            GenereateCountryCode,
            GetStateId,
            //----------------------------------------Migration of country ends here--------------------------------------------------------
            //----------------------------------------Migration of Project------------------------------------------------------------------
            MigrateProject,
            GetProjetCategoryId,
            MigrateProjectCategory,
            GetProjectId,
            GetProjectCode,
            GenerateProjectCode,
            IsProjectLedgerMapped,
            GenerateNewProject,
            GetAllMasterVouchers,
            MapProjectVoucher,
            //----------------------------------------Migration of Project ends here--------------------------------------------------------
            //----------------------------------------Migration of Ledger Group-------------------------------------------------------------
            MigrateLedgerGroup,
            GetLedgerGroupId,
            GetLedgerGroupCode,
            GenerateGroupCode,
            GetNatureId,
            //----------------------------------------Migration of LedgerGroup ends here----------------------------------------------------
            //----------------------------------------Migration of Ledger-------------------------------------------------------------------
            MigrateLedger,
            GetLedgerId,
            UpdateLedgerCostCentre,
            GetLedgerCode,
            GenerateLedgerCode,
            //----------------------------------------Migration of Ledger ends here---------------------------------------------------------
            //----------------------------------------Mapping-------------------------------------------------------------------------------
            MapProjectCash,
            UpdateOpBalance,
            SumUpdateOPBalance,
            CheckOPBalUpdate,
            //----------------------------------------Migration of Mapping ends here---------------------------------------------------------
            //----------------------------------------Migration of Master Bank Accounts------------------------------------------------------
            MigrateBankAccount,
            GetBankAccountNo,
            GetBankAccountCode,
            GenerateBankAccountCode,
            //----------------------------------------Migration of Master Bank Accounts ends here---------------------------------------------
            //----------------------------------------Migration of Donor Auditor--------------------------------------------------------------
            MigrateDonorAuditor,
            GetDonorAuditorId,
            //----------------------------------------Migration of Donor Auditor ends here---------------------------------------------------
            //----------------------------------------Migration of Cost Centre---------------------------------------------------------------
            MigrateCostCentre,
            GetCostCentreId,
            GetCostCentreCode,
            GenerateCostCentreCode,
            IsProjectDonorMapped,
            IsProjectPurposeMapped,
            NewDonorId,
            ICostCategoryExists,
            AddDefaultCostCategory,
            MapCostCentreCategory,
            IsCostCategoryMapped,
            //----------------------------------------Migration of Cost Centre ends here-----------------------------------------------------
            //----------------------------------------Migration of Executive Committee-------------------------------------------------------
            MigrateExecutiveCommittee,
            GetExecutiveCommitteeId,
            //----------------------------------------Migration of Executive Committee ends here---------------------------------------------
            //----------------------------------------Migration of Purpose-------------------------------------------------------------------
            MigrateFCPurpose,
            MigrateFCPurposeOpening,
            GetPurposeId,
            GetPurposeIdByPurpose,
            GetPurposeCode,
            GeneratePurposeCode,
            MapPurpose,
            //----------------------------------------Migration of Purpose ends here--------------------------------------------------------
            //----------------------------------------Migration of Voucher----------------------------------------------------------
            MigrateVoucherMaster,
            MigrateVoucherTransJournal,
            MigrateVoucherMasterDonor,
            MigrateVoucherTrans,
            MigrateVoucherTransWithChequeNo,
            MigrateCostCentreTransaction,
            MapProjectDonor,
            MapVoucherCostCentre,
            GetBankAccountLedgerId

        }

        public enum TallyExport
        {
            FetchVoucherType,
            FetchLedgerGroup,
            FetchLedger,
            FetchCostCenter,
            FetchCostCategory,
            FetchMasterVoucher,
            FetchVoucherDetails,
            FetchCCVoucherDetails
        }

        public enum TallyMigration
        {
            //------------------------------------------Clearing and Migrating Default---------------------------------------------------
            ClearData,
            ClearDataByDateRange,
            MigrateUsers,
            CashDefaultLedger,
            FixedDepositeDefaultLedger,
            CapitalFundDefaultLedger,
            DefaultSateId,
            InsertCashDefaultLedger,
            InsertFixedDepositeDefaultLedger,
            InsertCapitalFundDefaultLedger,
            InsertDefaultState,
            //----------------------------------------- Clearing and Migrating Ends here-------------------------------------------------
            //------------------------------------------Migration of Master Voucher-----------------------------------------------------
            IsMasterVoucherExists,
            InsertMasterVocher,

            //------------------------------------------Migration of Accouting Years-----------------------------------------------------
            MigrateAcYears,
            FindAccountingYear,
            SetActiveAccountingYear,
            GetActiveAccountingYearId,
            GetLeastAccountingDate,
            GetBookBeginningDate,
            UpdateLedgerOpBalaceDate,
            GetCurrentMigrationYear,
            SetCurrentMigrationYear,
            UpdateVoucherMasterTransTableDates,
            UpdateVoucherCountry,
            //----------------------------------------Migration of Accouting Years ends here---------------------------------------------
            //----------------------------------------Migration of Bank------------------------------------------------------------------
            GetBankId,
            MigrateMasterBank,
            GetBankCode,
            GenerateBankCode,
            //----------------------------------------Migration of Bank ends here----------------------------------------------------------
            //----------------------------------------Migration of country-----------------------------------------------------------------
            MigrateCountry,
            GetCountryId,
            GetCountryCode,
            GenereateCountryCode,
            GetStateId,
            InsertState,
            IsSateExists,
            //----------------------------------------Migration of country ends here--------------------------------------------------------
            //----------------------------------------Migration of Project------------------------------------------------------------------
            MigrateProject,
            GetProjetCategoryId,
            MigrateProjectCategory,
            GetProjectId,
            GetProjectCode,
            GenerateProjectCode,
            GenerateNewProject,
            GetAllMasterVouchers,
            MapProjectVoucher,
            IsDefaultVoucherExists,
            //----------------------------------------Migration of Project ends here--------------------------------------------------------
            //----------------------------------------Migration of Ledger Group-------------------------------------------------------------
            MigrateLedgerGroup,
            GetLedgerGroupId,
            GetLedgerGroupCode,
            GenerateGroupCode,
            //----------------------------------------Migration of LedgerGroup ends here----------------------------------------------------
            //----------------------------------------Migration of Ledger-------------------------------------------------------------------
            MigrateLedger,
            GetLedgerId,
            GetLedgerCode,
            GenerateLedgerCode,
            EnableCostCentreLedger,
            //----------------------------------------Migration of Ledger ends here---------------------------------------------------------
            //----------------------------------------Mapping-------------------------------------------------------------------------------
            MapProjectCash,
            UpdateOpBalance,
            IsProjectLedgerMapped,
            //----------------------------------------Migration of Mapping ends here---------------------------------------------------------
            //----------------------------------------Migration of Master Bank Accounts------------------------------------------------------
            MigrateBankAccount,
            GetBankAccountNo,
            GetBankAccountCode,
            GenerateBankAccountCode,
            //----------------------------------------Migration of Master Bank Accounts ends here---------------------------------------------
            //----------------------------------------Migration of Donor Auditor--------------------------------------------------------------
            MigrateDonorAuditor,
            GetDonorAuditorId,
            IsDonorExists,
            //----------------------------------------Migration of Donor Auditor ends here---------------------------------------------------
            //----------------------------------------Migration of Cost Centre---------------------------------------------------------------
            MigrateCostCentre,
            GetCostCentreId,
            GetCostCentreCode,
            GenerateCostCentreCode,
            IsCostCentreCategoryExists,
            InsertCostCentreCategory,
            MapCostCentreToCostCategory,
            GetCostCentreCategoryId,
            //----------------------------------------Migration of Cost Centre ends here-----------------------------------------------------
            //----------------------------------------Migration of Executive Committee-------------------------------------------------------
            MigrateExecutiveCommittee,
            GetExecutiveCommitteeId,
            //----------------------------------------Migration of Executive Committee ends here---------------------------------------------
            //----------------------------------------Migration of Purpose-------------------------------------------------------------------
            MigrateFCPurpose,
            GetPurposeId,
            GetPurposeCode,
            GeneratePurposeCode,
            MapDonor,
            //----------------------------------------Migration of Purpose ends here--------------------------------------------------------
            //----------------------------------------Migration of Voucher----------------------------------------------------------
            MigrateVoucherMaster,
            MigrateVoucherMasterWithNameAddress,
            MigrateVoucherTransJournal,
            MigrateVoucherMasterDonor,
            MigrateVoucherTrans,
            MigrateVoucherTransWithChequeNo,
            MigrateCostCentreTransaction,
            MapProjectDonor,
            MapVoucherCostCentre,
            GetBankAccountLedgerId,
            UpdateDonorTransaction,

            //-----------------------------------Delete Migration-------------------------------------------------------------
            FetchAllOpeningBalace,
            UpdateDeleteOPBalance,
            DeleteTransaction,
            UpdateOPDate,
            DeleteOPBalance,
            DeleteUnusedLedgers

        }

        public enum FDRenewal
        {
            FetchFixedDepositStatus,
            Add,
            Update,
            UpdateById,
            UpdateFDStatus,
            UpdateStatusByID,
            DeleteFDByID,
            Fetch,
            FetchAll,
            FetchById,
            DeleteFDRegisters,
            UpdateLastFDRow,
            FetchFDRegisters,
            FetchVoucherID,
            FetchFDAccountIdByRenewalId,
            Delete,
            DeleteFDRenewal,
            DeleteFDPhysicalReneval,
            DeleteFDAccount,
            DeleteFDPhysicalAccount,
            DeleteFDPhysicalOpeningAccount,
            DeleteFDPhysicalRenewalAccount,
            CheckFDAccountExists,
            CheckPhysicalAccountExists,
            FetchFDAccountId,
            FetchRenewalType,
            FetchDeletedRenewalType,
            UpdateFDAccountStatus,
            CheckFDClosed,
            CheckFDPhysicalClosd,
            CheckByVoucherId,
            FetchRenewalByVoucherId,
            FetchFDAccountByVoucherId,
            CheckDuplicateRenewal,
            CheckFDRenewalWithdraw,
            GetVoucherId,
            HasFDRenewal,
            HasFDReInvestmentByFDAccountId,
            GetLastRenewalIdByFDAccountId,
            GetLastRenwalDetailsByRenewalId,
            HasFDPostInterests,
            HasFDPartialwithdrawal,
            GetLastPostInterestIdByFDAccountId,
            GetLastPostInterestDetailsByRenewalId,
            FetchFDAccountDetailsByFDAccountID,
            GetMaxFDRenewal,
            HasFDWithdrawal,

            DisableFDInterestDeprecaitionLedger,
        }

        public enum InKindReceived
        {
            Add,
            Update,
            Fetch,
            Delete,
            FetchAll
        }

        public enum InKindUtilised
        {
            Add,
            Update,
            Fetch,
            FetchAll
        }

        //Data Synchronization SQL
        public enum ImportMaster
        {
            IsLegalEntityExists,
            IsExecutiveMembers,
            AddLegalEntity,
            IsProjectCatogoryExists,
            AddProjectCatogory,
            IsProjectExists,
            IsProjectCodeExists,
            AddProject,
            IsLedgerGroupExists,
            AddLedgerGroup,
            IsLedgerExists,
            IsHeadOfficeLedgerExists,
            IsGeneralateLedgerExists,
            IsLedgerCodeExists,
            IsHeadOfficeCodeExists,
            AddLedger,
            IsFCPurposeExists,
            IsFCPurposeCodeExists,
            AddFCPurpose,
            GetLegalEntityId,
            GetExecutiveMemberId,
            GetProjectCategoryId,
            GetProjectCategoryITRId,
            GetProjectId,
            GetLedgerGroupId,
            GetLedgerId,
            GetHeadOfficeLedgerId,
            GetFCPurposeId,
            GetCountryId,
            GetStateId,
            FetchLegalEntity,
            FetchProjectCategory,
            MapLedgers,
            MapVouchers,
            IsParentGroupExist,
            IsNatureExist,
            IsMainGroupExist,
            GetNatureId,
            GetParentId,
            GetMainParentId,
            IsGroupCodeExist,
            DeleteHeadOfficeMappedLedger,
            FetchVoucherIdByLedgerId,
            DeleteHeadOfficeLedger,
            DeleteProjectCategoryByLedgerId,
            FetchLedgerById,
            DeleteProjectCategoryByLedgerIdCollection,
            DeleteMasterLedger,
            DeleteTDSCreditorProfile,
            DeleteProjectMappedLedger,
            DeleteProjectBudgetMappedLedger,
            DeleteLedgerBalance,
            DeleteVoucherCostCenter,
            DeleteVoucherTrans,
            DeleteVoucherMasterTrans,
            DeleteBudgetLedger,
            DeleteFDLedger,
            DeleteBankAccountByLedger,
            DeleteLedgerOtherMappedDetails,
            FetchHeadOfficeLedgers,
            MapHeadOfficeLedger,
            GetGeneralateId,
            GetGeneralateParentId,
            GetGeneralateMainParentId,
            DeleteHeadOfficewithGeneralateMappedLedgers,
            MapHeadOfficeLedgetWithGenealateLedgers,
            UpdateHeadOfficeLedger,
            UpdateMasterLedger,
            CheckTransactionCount,
            CheckLedgerBalance,
            CheckBudgetLedgerAmount,
            CheckProjectBudgetLedger,
            FetchMappedLedgers,
            IsledgerLedgerCodeExists,
            IsCashExists,
            IsFDExists,
            IsCapFundExists,
            IsBranchOfficeExist,
            FetchBranchOfficeProjects,
            IsCountryExists,
            FetchHeadOfficeLedgerCode,
            FetchLedgerIdByLedgerName,

            //TDS
            IsTDSSectionExists,
            IsTDSNatureOfPaymentExists,
            IsTDSDeducteeTypeExists,
            IsDutyTaxExists,
            IsTaxPolicyExists,
            GetTDSSectionId,
            GetTDSNatureOfPaymentId,
            GetTDSDeducteeTypeId,
            GetDutyTaxId,
            GetTDSPolicyId,

            // Map Category Ledger
            MapCategoryLedgers,
            DeleteAllCategoryLedgers,

            UpdateLedgerBudgetGroup,

            //On 28/01/2021
            DeleteUnusedAllMappedLedgersByProject,
            DeleteLedgerGroupNotExistPortal,
            UpdateParentGroupMainGroupwithGroupId,
            UpdateLocalHeadofficeLedgerFlag,
            FetchBranchLedgerLocalDatabase,
            DeleteUnusedAllBudgetMappedLedgersByProject,

            GetBudgetGroupId,
            GetBudgetSubGroupId
        }


        public enum Portal
        {
            FetchBranchDetails,
            FetchHeadOfficeDetails
        }

        public enum PortalMessage
        {
            AddDataSynMessage,
            AddAmenmentMessage,
            DeleteDataSynMessage,
            DeleteAmendmentMessage,
            FetchPortalDataSynMessage,
            FetchPortalAmendmentMessage,
            AddBroadCastMessage,
            DeleteBroadCastMessage,
            FetchBroadCastMessage,
            FetchTroubleTickets,
            FetchUserManualFeature,
            AddTroubleTickets,
            DeleteTroubleTickets,
            EditTroubleTickets,
            DeleteUserMaualsPaidFeatures,
            UpdateUserMaualsPaidFeatures,
        }
        public enum ImportVoucher
        {
            AuthenticateBranchCode,
            AuthenticateHeadOfficeCode,
            FetchDataBase,
            FetchLatestLicense,
            FetchBranchOfficeId,
            FetchLocationId,
            FetchTransactions,
            FetchBranchProjects,
            FetchProjects,
            InsertVoucherMaster,
            InsertVoucherMasterBranch,
            UpdateModifiedOn,
            InsertVoucherTrans,
            InsertVoucherCostCentre,
            InsertVoucherSubLedger,
            InsertProjectCategory,
            InsertProject,
            InsertLedger,
            EnableLedgerPropertiesDetails,
            InsertLedgerProfile,
            UpdateLedgerProfile,
            InsertLedgerGroup,
            UpdateLedgerGroup,
            InsertCostCentre,
            InsertCostCategory,
            InsertCountry,
            InsertState,
            InsertSubLedger,
            InsertGSTClass,
            InsertAssetStockVendor,
            CheckTransactionExistsByDateClose,
            CheckFDAccountsExistsByLedger,


            InsertDonor,
            InsertBank,
            InsertBankAccount,
            UpdateBankAccount,
            InsertLedgerBank,
            InsertLedgerBalance,
            DeleteLedgerBalance,
            DeleteVoucherMasterTrans,
            DeleteVoucherTrans,
            DeleteVoucherCostCentre,
            DeleteVoucherSubLedger,
            UpdateDataSynStatus,
            UpdateSubBranchDsyncStatus,
            FetchOPBalanceDate,
            UpdateOPBalanceDate,
            UpdateOPBalanceDateByDate,
            DeleteOPBalanceDateMoreThanOne,
            FetchMergeProject,
            GetPreviousOPBalance,
            MapDefaultVoucher,
            UpdateFDLedgerOpeningBalanceByProject,
            DeleteFDLedgerOpeningBalanceByProject,

            //Get Id
            GetProjectCategoryId,
            GetProjectId,
            GetDonorId,
            GetLedgerId,
            GetLedgerGroupId,
            GetBankId,
            GetBankAccountId,
            GetPurposeId,
            GetCostCentreId,
            GetCostCategoryId,
            GetStateId,
            GetCountryId,
            GetParentGroupId,
            GetMainParentId,
            GetNatureId,
            GetLegalEntityId,
            GetSubLedgerId,
            GetFDAccountId,
            GetStatisticsTypeId,
            GetGSTSlabClassId,
            GetVendorId,
            InsertFDAccount,
            InsertFDAcountForSplitProject,
            InsertFDRenewal,
            UpdateLedgerInterestLedger,
            DeleteFDAccount,
            DeleteFDRenewal,
            DeleteFDVoucherMasterTrans,
            DeleteFDVoucherTrans,
            FetchFDTransaction,

            //Exists
            IsProjectCategoryExists,
            IsProjectExists,
            IsDonorExists,
            IsBankAccountExists,
            IsBankAccountCodeExits,
            IsBankExists,
            IsBankCodeExists,
            IsPurposeExists,
            IsCountryExists,
            IsStateExists,
            IsLedgerExists,
            IsLedgerProfileExists,
            IsLedgerCodeExists,
            IsLedgerBankExists,
            IsCostCentreExists,
            IsCostCategoryExists,
            IsLedgerGroupExists,
            IsLegalEntityExists, //12/04/2017, to check legal entity exists 
            IsGSTClassExists,
            IsVendorExists,
            IsAuditVouchersLocked,
            IsVoucherExists,

            //Mapping
            MapProjectLedger,
            MapProjectDonor,
            MapProjectCostcentre,
            MapCostCategory,
            MapProjectPurpose,
            IsProjectBranchMapped,
            MapSubLedger,
            MapProjectBudgetLedger,

            //Check Is General Ledger
            IsGeneralLedger,

            //17/06/2021 Budget Module for split projects
            GetBudgetIdForSplitProject,
            DeleteBudgetForSplitProject,
            /*InsertBudgetMasterForSplitProject,
            UpdateBudgetMasterForSplitProject,
            InsertBudgetProjectForSplitProject,
            InsertBudgetLedgerForSplitProject,*/

            //Budget
            GetBudgetId,
            InsertBudgetMaster,
            UpdateBudgetMaster,
            InsertBudgetProject,
            InsertBudgetLedger,
            InsertBudgetStatisticsDetails,
            InsertStatisticsType,
            InsertBudgetSubLedger,
            DeleteBudgetProject,
            DeleteBudgetLedger,
            DeleteBudgetStatisticsDetails,
            DeleteBudgetSubLedger,

            //For GST Invoice
            DeleteGSTInvoicesByProjectSplit,
            InsertGSTInvoiceMaster,
            InsertGSTInvoiceMasterLedgerDetail,
            InsertGSTInvoiceVoucher,
            FetchMasterByBranchLocationVoucherId,

            UpdateAutoIncrementNumber,
        }

        public enum ExportMasters
        {
            LegalEntityFetchAll,
            ProjectCategoryFetchAll,
            ProjectFetchAll,
            LedgerGroupFetchAll,
            LedgerFetchAll,
            PurposeFetchAll,
            GoverningMemberFetchAll,
            GeneralateLedgerFetchAll,
            GeneralateLedgerMapAll,

            //TDS
            TDSSectionFetchAll,
            TDSNatureOfPaymentsFetchAll,
            TDSDeducteeTypesFetchAll,
            TDSDutyTaxFetchAll,
            TDSPolicyFetchAll,
            TDSTaxRateFetchAll,
            TDSPolicyDeducteesFetchAll,

            //Budget
            FetchBudgetMasterByDateRange,
            FetchBudgetProjectByDateRange,
            FetchBudgetLedgerByDateRange,
            FetchBudgetSubLedgerByDateRange,

            BudgetGroupFetchAll,
            BudgetSubGroupFetchAll
        }

        public enum ImportFD
        {
            InsertFDAccount,
            InsertFDRenewal,
            DeleteFDAccount,
            DeleteFDRenewal
        }

        public enum ImportLedger
        {
            InsertMasterLedger,
            InsertHeadOfficeLedger,
            MapHeadOfficeLedger,
            FetchHeadOfficeLedgers,
            FetchMappedLedgers,
            UpdateHeadOfficeLedger,
            InsertMasterLedgerGroup,
            DeleteHeadOfficeMappedLedger,
            DeleteMasterLedger,
            DeleteHeadOfficeLedger,
            DeleteProjectMappedLedger,
            DeleteLedgerBalance,
            DeleteBudgetLedger,
            CheckTransactionCount,
            CheckLedgerBalance,
            CheckBudgetLedgerAmount
        }

        public enum ExportVouchers
        {
            FetchTransProjects,
            FetchMasterVouchers,
            FetchVoucherTransactions,
            FetchVoucherCostCentres,
            FetchVoucherSubLedgers,
            FetchProjects,
            FetchDonors,
            FetchBankDetails,
            FetchBankDetailsForSplitProject,
            FetchBankAccountDetails,
            FetchLedgerBalance,
            FetchSplitProjectLedger,
            FetchCountry,
            FetchState,
            FetchHeadOfficeLedger,
            FetchLedgerGroup,
            FDAccounts,
            FDRenewals,
            FDVoucherMasterTrans,
            FDVoucherTrans,
            FDBankAccountDetails,
            FDBankDetails,
            CheckHeadofficeLedgerExists,
            AllUnMappedMappedLedgers,
            DeleteMappingLedgersAll,
            FetchActiveTransactionperiod,
            FetchUserName,
            FetchHeadOfficebyLedger,
            MapBrachHeadOffice,
            FetchSplitProjectLedgerBalance,
            FetchSplitProjectLedgerBalanceForSplitFY,
            FetchLegalEntity,
            CheckPrimaryLedgerGroup,
            CheckMappedFDLedgers,
            ValidateBookBegin,

            //Export Headoffice Branch
            FetchHOBranchVoucherTransactions,
            FetchHOBranchVoucherCostCenter,
            FetchHOBranchFDAccounts,
            FetchHOBranchFDRenewals,
            FetchHOBranchFDVoucherTrans,
            FetchHOBranchLedger,
            FetchHOBranchLedgerSplitFY,
            FetchLedgerProfile,
            FetchHOBranchProjects,
            FetchHOBranchList,
            FetchProjectLedgers,
            FetchProjectCostCenters,
            FetchProjectDonors,
            FetchMasterGSTClass,
            FetchAssetStockVendors,

            //15/06/2021, Export Budget Module
            FetchBudgetMaster,
            FetchBudgetProject,
            FetchBudgetLedger,
            FetchBudgetStatisticsDetails,
            FetchBudgetStatisticsTypes,
            FetchBudgetProjectLedger,
            FetchBudgetType,
            FetchBudgetLevel,

            //05/02/2024, GST Invoice Master and GST Invoice Details
            FetchGSTInvoiceMaster,
            FetchGSTInvoiceMasterLedgerDetail,
            FetchGSTInvoiceVoucher,

            //On 08/02/2024, To get Last Vocuher Id 
            FetchBranchLastVoucherId,
        }

        public enum LedgerProfile
        {
            Fetch,
            FetchAll,
            Add,
            Update,
            Delete,
            DeleteGStProfile,
            CheckPANNumber
        }

        public enum TDSBooking
        {
            Fetch,
            FetchAll,
            Add,
            Update,
            Delete,
            FetchTDSMaster,
            FetchTDSVoucher,
            FetchLedgerName,
            DeleteBooking,
            FetchTDSBooking,
            CheckTDSBookingByID,
            DeleteBookingByVoucher,
            CheckBookingByVoucher,
            FetchBookingDetailByVoucherId,
            UpdateIsTDSDeductedByBookingDetailId,
            DeleteDeduction,
            FetchBookingIdByVoucher,
            FetchLedgerDetailsById,
            FetchDeducteeType,
            TempTDSBooking,
            FetchBookingIdByVoucherId,
            CheckIsTDSBookingVoucher,
            CheckIsTDSPaymentVoucher,
            CheckIsTDSDeductionVoucher,
            ExpenseLedgerAmount,
            CheckVoucherInBooking,
            CheckHasBooking,
            CheckHasDeduction,

            FetchBookingVIDbyPartyVID
        }

        public enum TDSBookingDetail
        {
            Fetch,
            FetchAll,
            Add,
            Update,
            Delete,
            UpdateTaxDeductStatus
        }

        public enum TDSDeduction
        {
            Fetch,
            FetchAll,
            Add,
            Update,
            Delete,
            FetchPendingTransaction,
            FetchDeductionId,
            FetchByBooking,
            UpdateIsTDSDeductable,

        }

        public enum TDSDeductionDetail
        {
            Fetch,
            FetchAll,
            Add,
            Update,
            Delete
        }

        public enum TDSPartyPayment
        {
            FetchPendingPartyPayment,
            FetchAllPartyPayment,
            FetchPaymentByParyPaymentId,
            Add,
            Update,
            PhysicalDelete,
            LogicalDelete,
            GetPartyPaymentId,
            FetchPartyPaymentId
        }

        public enum TDSPartyPaymentDetail
        {
            Fetch,
            FetchAll,
            Add,
            Update,
            Delete,
            CheckPartyPayment,
            CheckIsPartyVoucher,
            CheckIsTDSPaymentVoucher,
            CheckIsTDSBookingVoucher
        }

        public enum TDSPayment
        {
            Fetch,
            FetchAll,
            Add,
            Update,
            Delete,
            DeleteTDSPayment,
            FetchPendingTDSPayment,
            FetchTDSPayment,
            FetchTDSPaymentDetail,
            FetchPaymentId,
            TDSChallanReport,
            FetchTDSInterest,
            FetchBookingMappedPaymentId

        }

        public enum TDSPayemtDetail
        {
            Fetch,
            FetchAll,
            Add,
            Update,
            Delete,
            CheckTDSPayment,
            HasTDSVoucher,
            HasTDSLedger
        }

        public enum DeducteeTax
        {
            DutyTaxAdd,
            DutyTaxUpdate,
            DutyTaxDelete,
            DutyTaxFetchAll,
            DutyTaxFetchById,
            DutyTaxRateAdd,
            DutyTaxRateAddByTaxTypeId,
            DutyTaxRateAddById,
            DutyTaxRateDelete,
            FetchTaxRateById,
            FetchTaxPolicy,
            DeleteTaxRateByTaxId,


            DutyTaxTypeAdd,
            DutyTaxTypeUpdate,
            DutyTaxTypeDelete,
            DutyTaxTypeFetchAll,
            DutyTaxSumAmount,
            FetchActiveDutyTaxType,
            DutyTaxTypeFetchById,
        }

        public enum TDSSection
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
            TDSSection
        }

        public enum DeducteeType
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
            FetchDeductType,
            FetchActiveDeductTypes,
            CheckTransDeducteeType
        }

        public enum TDSCompanyDeductor
        {
            Add,
            Update,
            Fetch
        }

        public enum NatureofPayments
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
            FetchSectionCodes,
            FetchNatureofPaymentsSection,
            FetchNatureofPayments,
            FetchTaxRate,
            FetchTDSLedger,
            FetchNatureOfPaymentWithCode,
            FetchTDSWithoutPAN,
            IsActiveNOP
        }
        public enum SubBranchList
        {
            MapProjectBranch,
            FetchAllBranches,
            AddBranch,
            UpdateBranch,
            DeleteMappedProjects,
            FetchMappedProjects,
            FetchBranchCode,
            FetchDSyncStatus,
            FetchDsyncStatusById,
            SaveDSyncStatus,
            AuthenticateBranchCode
        }

        public enum AuditLockTrans
        {

            AddAuditType,
            AddAuditTrans,
            UpdateAuditType,
            UpdateAuditTrans,
            DeleteAuditType,
            DeleteAuditTrans,
            DeleteAuditTransByProject,
            DeleteAuditTransByLockbyPortal,
            FetchAuditType,
            FetchAuditTypeByType,
            FetchAuditTrans,
            FetchAllAuditType,
            FetchAllAuditTrans,
            FetchAuditLockDetailsForProject,
            FetchAuditLockDetailsForProjectAndDate,
            FetchAuditLockDetailByProjectDateRange,
            FetchAuditLockDetailIdByProjectDateRange,
            IsValidPassword,
            ResetPassword,
            ValidatePasswordHint,
            FetchLockMastersInVoucherEntry,
        }

        public enum StatisticsType
        {
            Add,
            Update,
            Delete,
            Fetch,
            FetchAll,
        }

        #region Asset
        public enum AssetService
        {
            Add,
            Update,
            FetchAll,
            Fetch,
            Delete
        }


        public enum AssetLocation
        {
            Add,
            Delete,
            Update,
            FetchAll,
            Fetch,
            FetchToLocation,
            FetchFromLocation,
            FetchLocationByItem,
            FetchSelectedLocationDetails,
            FetchAssetLocationNameByID,
            FetchLocationByItemId,
            DeleteLocationDetails,
            FetchStockLocation,
            FetchValidateLocation,
            FetchEditValidateLocation,
            FetchParentLocationId,
            FetchBlockDetails,
            FetchLocationIDByName,
            FetchAssetLocationByProjectID,
            FetchProjectLocationProjectId
        }

        public enum AssetInsurance
        {
            Add,
            Delete,
            Update,
            FetchAll,
            Fetch,
            AutoFetchInsurance,
            GetInsuranceType,
            FetchInsuranceDetails,

        }
        public enum AssetClass
        {
            Add,
            Delete,
            Update,
            FetchAll,
            Fetch,
            FetchSelectedClass,
            FetchbyID,
            FetchAssetClassList,
            FetchAssetClassNameByID,
            DeleteAssetDetails,
            FetchValidateClass,
            FetchClassNameByParentID,
            FetchAssetParentClassNameByID,
            FetchAccessFlagClassId,
            FetchAssetParentClassIdByParentClassName,
            DeleteClass
        }

        public enum Depriciation
        {
            DepreciationMasteAdd,
            DepreciationMasterEdit,
            AddDepreciationVoucherDetail,
            DeleteMaster,
            DeleteDepreciationDetail,
            FetchAll,
            FechDepreciationDetails,
            FetchDepreciationMaster
        }

        public enum VoucherDepreciation
        {
            AddDepreciationMaster,
            EditDepreciationMaster,
            AddDepreciationDetail,
            DeleteDepreciation,
            DeleteMasterDepreciation,
            FetchAll,
            FetchAccountLedger,
            FetchDepreciationLedger,
            FetchApplyDepreciationDetails,
            FetchPreviousRenewal,
            FetchNextRenewal,
            FetchMaxRenewal,
            FetchVoucherMasterById,
            FetchVoucherDetailsById,
            FetchFinanceVoucherDetails,
            FetchExistorNot
        }

        public enum AssetUnitOfMeasure
        {
            Add,
            Delete,
            Update,
            FetchAll,
            Fetch,
            FetchForFirstUnit,
            FetchUnitOfMeasureId,
        }
        public enum AssetItem
        {
            Add,
            Delete,
            Update,
            FetchAll,
            FetchDepreledgerById,
            FetchDislegerById,
            FetchAccledgerById,
            FetchCatogeryById,
            FetchUnityById,
            FetchAssetItemByGroup,
            FetchAssetIdByItem,
            FetchAssetItem,
            Fetch,
            AssetItemDetailAdd,
            AssetItemDetailsDelete,
            FetchAssetItemNameByID,
            FetchAssetOPDetails,
            UpdateAssetItemLocationById,
            FetchAssetItemDetailAll,
            FetchAssetItemDetailById,
            FetchAssetItemDetailByLocation,
            FetchItemDetailforTransfer,
            FetchAssetCategoryItemDetail,
            FetchStockCategoryItemDetail,
            FetchAssetIdDetail,
            AssetItemDetailsDeleteByPurchase,
            UpdateAssetItemDetailBySales,
            UpdateAssetItemDetailBySalesId,
            UpdateAccountLedgerToItem,
            FetchAllAssetDetails,
            FetchItemDetailbyAssetId,
            FetchActiveAssetItem,
            AssetItemDetailsDeleteByReceive,
            FetchAssetIdDetailsAtEdit,
            DeleteAssetItems,
            FetchProjectNameByAssetItem,
            FetchAssetItemNameForReport,
            FetchAssetAMCNameForReport,
            FetchAssetSalesNameForReport,
            FetchAssetReceiveNameForReport,
            FetchAssetDisposalNameForReport,
            FetchAssetRegisterSummaryReport,
            FetchAssetLocationByLocationID,
            FetchAMCAssetItems,
            FetchInoutIdByItemId,
            FetchAllAssetItems,
            FetchAssetItemIDByName,
            FetchUpdateAssetDetails,
            UpdateAssetDetail,
            FetchAssetMapedLedgers,
            FetchAssetAccessFlag,
            FetchAssetClassIDByAssetName,

            #region Fixed Asset Items
            SaveFixedAssetItemDetails,
            SaveFixedAssetInsuranceItemDetails,
            UpdateFixedAssetItemDetails,
            UpdateFixedAssetInsuranceItemDetails,
            DeleteFixedAsset,
            DeleteFixedAssetbyDetailId,
            UpdateAssetItemDetailStatus
            #endregion

        }

        public enum AssetMapping
        {
            MapLocation,
            DeleteMapping,
            DeleteMapLocation
        }

        public enum AssetCategory
        {
            Add,
            Delete,
            Update,
            FetchAll,
            Fetch,
            FetchGroup,
            FetchSelectedCategoryDetails,
            FetchSelectedCategory,
            FetchSelectedGroups,
            FetchAssetCategory
        }

        public enum AssetCustodians
        {
            Add,
            Delete,
            Update,
            FetchAll,
            Fetch,
            DeleteCustudianDetails,
            FetchCustodianNameByID,
            FetchCustodianNameyByLocationID,
            FetchMappedCustodian
        }

        public enum Block
        {
            Add,
            Delete,
            Update,
            Fetch,
            FetchAll,
            FetchBlockIDByName
        }

        public enum VendorInfo
        {
            Add,
            Delete,
            Update,
            FetchAll,
            FetchAllWtihGST,
            Fetch,
            FetchByGSTNo,
            FetchByPANNo,
            FetchByEmail,
            DeleteVendorDetails,
            FetchVendorNameByID,
            FetchVendorByItemId
        }
        public enum ManufactureInfo
        {
            Add,
            Delete,
            Update,
            FetchAll,
            Fetch,
            DeleteManufactureDetails,
            FetchManufactureNameByID
        }
        public enum AssetTransferVoucher
        {
            DetailAdd,
            MasterAdd,
            Delete,
            DetailUpdate,
            MasterUpdate,
            FetchAllDetail,
            FetchAllMaster,
            Fetch,
            FetchAssetDetails,
            FetchAssetGroup,
            FetchDetails
        }

        public enum AssetInsuranchVoucher
        {
            #region Insurance Details

            AddInsuranceDetails,
            DeleteInsuranceDetails,
            UpdateInsuranceDetails,
            FetchAllInsuranceDetails,
            FetchInsuranceDetails,
            FetchRenewalByProject,

            #endregion

            #region Insurance Master Detail

            AddInsuranceMastersDetail,
            DeleteInsuranceMastersDetail,
            UpdaterInsuranceMastersDetail,
            FetchAllInsuranceMastersDetail,
            FetchInsuranceMastersDetail,

            #endregion

            #region Insurance Masters

            SaveInsuranceMaster,
            UpdateInsuranceMaster,
            DeleteInsuranceMaster,
            FetchInsuanceMaster,
            FetchAllInsuranceMasters,
            FetchInsDetailbyProject,
            RenewInsurance
            #endregion
        }

        public enum AssetInsuranceRenewal
        {
            AddInsRenMaster,
            AddInsRenDetails,
            Update,
            FetchAllRenewal,
            EditRenewal,
            FetchAllAssetItem,
            Delete,
            DeleteDetail,
            FetchDetailById,
            FetchInsuranceDetailsbyItem,
            FetchInsuranceMaster,
            FetchInsuranceDetail,
            FetchInsuranceMasterById,
            ValidateDate,
            DeleteRenewalByInsID
        }
        public enum AssetRenewInsurance
        {
            InsRenewAdd,
            InsRenewEdit,
            DeleteDetails,
            DeleteItemDetails,
            FetchInsuranceDetails,
            FetchInsHistoryDetails,
            FetchInsRenewDetails,
            FetchItemDetailIdbyAssetId,
            LoadInsurancePLanDetails,
            Fetch,
            DeleteInsuranceByDetailId,
            IsInsuranceMade,
            FetchPreviousRenewal,
            FetchNextRenewal,
            FetchVoucherIdByInsId,
            FetchVoucherIdByItemId,
            FetchRegistrationDate
        }

        public enum AssetAMCVoucher
        {
            Add,
            AddAmcVoucherDetail,
            Delete,
            DeleteDetail,
            Update,
            UpdteAmcDetail,
            FetchAll,
            FetchDetails,
            FetchbyId,
            FetchAMCDetailbyId,
            AutoFetchProviderName,
            FetchVoucherIdbyMasterId,
            DeleteAMCDetailsByAMCIdItemdetailID,

        }
        public enum AssetSalesVoucher
        {
            AddSalesMaster,
            DeleteSalesMaster,
            UpdateSalesMaster,
            AddSalesDetail,
            DeleteSalesDetail,
            UpdateSalesDetail,
            FetchSales,
            FetchDisposal,
            FetchDisposalDetailsByProjectId,
            FetchSalesMaster,
            FetchSalesDetail,
            FetchLocationNameByItemId,
            FetchSalesDetailbySalesId,
            FetchSalesDetailsByPartyName,
            FetchDisposalMasterByPartyName,
            FetchAssetIdsBySalesorDisposalId,
            FetchSalesIdByVoucherId
        }
        public enum AssetPurchaseVoucher
        {
            AddPurchaseMaster,
            DeletePurchaseMaster,
            UpdatePurchaseMaster,
            AddPurchaseDetail,
            DeletePurchaseDetail,
            UpdatePurchaseDetail,
            FetchAll,
            FetchPurchaseMaster,
            FetchPurchaseDetail,
            FetchReceiveMaster,
            FetchReceiveDetail,
            FetchReceiveMasterByID,
            FetchAssetSourceFlagById,
            FetchPurchaseIdByVoucherId
        }

        public enum AssetLedgerMapping
        {
            FetchLocation,
            FetchAssetLedgerAll,
            SaveAssetLedgers,
            GetMappedProjectLocation,
            DeleteAssetMappedLedger
        }
        public enum StockLedgerMapping
        {
            FetchStockLedgers,
            SaveStockMappedLedgers,
            MapLedgerToAllItems
        }

        #endregion

        #region Stock
        public enum StockGroup
        {
            Add,
            Delete,
            Update,
            FetchAll,
            FetchbyID,
            FetchSelectedGroups,
            FetchGroupId,
            DeleteStockGroupDetails,
            FetchParentGroupId,
            FetchGroupNameByParentID,
            FetchAssetParentGroupNameByID
        }
        public enum StockCategory
        {
            Add,
            Delete,
            Update,
            FetchAll,
            Fetch,
            FetchStockCategory,
            FetchSelectedCategoryDetails
        }
        public enum StockItem
        {
            Add,
            Delete,
            Update,
            Fetch,
            FetchAll,
            FetchDepreledgerById,
            FetchDislegerById,
            FetchAccledgerById,
            FetchCatogeryById,
            FetchUnityById,
            FetchReorderLevelByItem,
            FetchStockBalance,
            DeleteStockItemDetails,
            FetchStockItemNameByID,

            FetchStockLedgerStatus

        }
        public enum StockItemTransfer
        {
            Add,
            Update,
            Delete,
            FetchByProjectId,
            FetchByEditId,
            GetNewEditId
        }
        public enum StockLocation
        {
            Add,
            Delete,
            Update,
            FetchAll,
            Fetch,
            FetchToLocation,
            FetchLocationByItem,
            FetchSelectedLocationDetails,
            FetchStockLocation,
            FetchAssetLocationNameByID,
            FetchLocationByItemId,
            DeleteLocationDetails,
            FetchValidateLocation
        }
        public enum StockUnitofMeasure
        {
            Add,
            Delete,
            Update,
            FetchAll,
            Fetch,
            FetchForFirstUnit
        }

        public enum StockMasterPurchase
        {
            Add,
            Delete,
            Update,
            FetchAll,
            Fetch,
            FetchNameAddress,
            AutoFetchNarration,
            FetchLedgerId
        }

        public enum StockPurchaseDetail
        {
            Add,
            Delete,
            Update,
            FetchAll,
            Fetch,
            FetchPurchaseById,
            FetchPurchaseIdByVoucherId
        }

        public enum StockMasterSales
        {
            Add,
            Delete,
            Update,
            FetchAll,
            Fetch,
            AutoFetchCustomer,
            AutoFetchNameAddress,
            AutoFetchNarration,
            FetchSalesIdByVoucherId
        }

        public enum StockSalesDetail
        {
            Add,
            Delete,
            Update,
            FetchAll,
            Fetch,
            FetchDetailsbeforeDelete
        }

        public enum StockPurchaseSales
        {
            FetchItem,
            FetchLocationbyItem,
            FetchDashboardDetails,
            FetchReorderLevel,
            FetchUnitofMeasurebyItem,
            FetchLocations
        }

        public enum StockPurchaseReturns
        {
            AddMasterPurchaseReturns,
            AddPruchaseReturnDetails,
            DeletePruchaseReturnDetails,
            FetchPurchaseReturnDetails,
            FetchPurchaseReturnsMasterDetails,
            DeletePurchaseReturnMaster,
            DeletePurchaseReturnDetails,
            FetchPurchaseDetailsById,
            FetchQuantity,
            UpdateMasterPurchaseReturns,
            FetchPurchaseById,
            FetchItemLocationById,
            FetchPurchaseReturnIdByVoucherId
        }

        public enum StockUpdation
        {
            UpdatestockDetails,
            FetchStockBalance,
            FetchStockAvailabilityDetails,
            FetchStockOPBalance,
            DeleteOPBalance
        }
        #endregion

        public enum AssetInOut
        {
            //AssetInOutMaster
            FetchAssetInOutMasterAll,
            FetchAssetInOutMasterById,
            AutoFetchSoldTo,
            SaveAssetInOutMaster,
            UpdateAssetInOutMaster,
            DeleteAssetInOutMaster,
            DeleteAssetInOutMasterDetailIds,
            FetchAssetInOutMasterByFlag,
            FetchAssetInOutDetailByFlag,
            FetchAssetIDDetailByFlag,
            FetchVoucherIdbyMasterId,
            FetchFixedAssetRegister,
            FetchVoucherIdCollection,

            // Common
            FetchOPDetailsByProjectId,
            FetchOPInoutIdsByProjectId,
            CheckAssetIDExists,
            DeleteAssetUnusedItems,
            CheckAssetTransactionExists,
            DeleteAllAssetTransaction,
            FetchAccountLedgerByItem,
            CheckInsuranceByItemId,
            CheckSoldAssetIdByItemID,
            CheckSoldAssetIdByItemDetailId,
            FetchItemDetailIdByInoutDetailId,
            FetchTransactionDetailsByItemId,

            //AssetInOutDetail
            FetchAssetInOutDetailAll,
            FetchAssetInOutDetailById,
            SaveAssetInOutDetail,
            UpdateAssetInOutDetail,
            DeleteAssetInOutDetail,
            DeleteAssetInOutDetailbyId,
            FetchAssetInOutDetailIdByInOutId,
            DeleteAssetTransByInoutDetailId,
            DeleteInsuranceDetailByItemDetailId,

            //AssetTrans
            SaveAssetTrans,
            DeleteAssetTrans,
            DeleteAssetItemDetail,
            DeleteAssetTransInOutDetailIds,
            DeleteAssetItemDetailById,
            FetchAssetItemDetailIdByInOutDetailId,
            FetchAssetListItem,
            FetchAssetListItemByItemId,
            FetchItemDetailIdByAssetId,
            CheckItemDetailIdExists,
            FetchAvailQty,
            FetchCurrentQty,
            FetchVoucherDetailsByVoucherId,
            FetchLasterAssetId,

        }

        public enum Denomination
        {
            Add,
            Update,
            DeleteDenomination,
            FetchDenominationByID,
            FetchDenomination
        }

        #region Depreciation
        public enum AssetDepreciation
        {
            Add,
            Delete,
            Update,
            FetchAll,
            FetchDepMethods,
            Fetch,
            FetchDepreciationMaster,
            FetchDepreciationDetailById,
            DeleteDepreciationDetailById,
            DeleteDepreciationMasterById,
        }
        #endregion

        #region ASSET AMC
        public enum AssetAMCRenewal
        {
            AddAMCRenewalMaster,
            AddAMCRenewalHistory,
            AddAMCItemMapping,
            EditAMCItemMapping,
            EditAMCRenewalMaster,
            EditAMCRenewalHistory,
            DeleteAMCRenewMaster,
            DeleteAMCRenewlHistory,
            DeleteAmcItemMapping,
            FetchAMCRenewalMasterDetails,
            FetchAMCRenewalMappedItems,
            FetchItemDetails,
            Fetch,
            FetchAMCRenewalHistoryByAmCId,
            FetchMappedItemAvailableList,
            FetchMappedItemSelectedList,
            DeleteAMCRenewalHistoryByAMCRenewalId,
            FetchMaximumRenewalIdByAMCId,
            FetchUnmappedItems,
            FetchLastRenewaldateByAMCId,
            FetchVocuherIdByAMCRenewalId,
            FetchledgerIdByVoucherID,
            FetchRenewalHistoryCountByAMCId,
            FetchVoucherIdByAMCId,
            FetchNextRenewal,
            FetchPreviousRenewal
        }
        #endregion

        #region Frontoffice
        public enum FrontOffice
        {
            SaveTemplate,
            FetchFeastDonorTemplateTypes,
            CheckFeastNameExists,
            ThanksgivingMailStatus,
            AppealMailStatus,
            UpdateAppealSMSStatus,
            NewsLetterMailStatus,
            UpdateFeastStatus,
            FetchDonorByTask,
            FetchNewsLetterByTask,
            FetchAnniversaryTypeDetails,
            FetchAnniversaryTypeSMSDetails,
            UpdateAnniversaryMailStatus,
            UpdateAnniversarySMSStatus,
            FetchNonPerformingDonors,
            InsertSentLetters,
            InsertTask,
            UpdateTask,
            FetchTaskDetails,
            FetchTaskByTagId,
            FetchMappedDonorByTagId,
            FetchDonorMappedStatus,
            FetchProspectsMappedStatus,
            MapTagDonor,
            DeleteTaskDetails,
            FetchDonorDetails,
            UpdateThanksgivingSMSStatus,
            FetchLetterTypes,
            FetchLetterTypeContent,
            FetchLetterTypeIdByName,
            FetchContentById,
            FetchContentByName,
            FetchDonorMappedProjects,
            FetchLetterTypesForSMS
        }

        #endregion

        #region Report Customization
        public enum CustomReport
        {
            SaveCustomReport,
            FetchAllReport,
            FetchCustomReportByName,
            DeletePreviousReport
        }
        #endregion
    }
}