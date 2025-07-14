/*  Class Name      : EnumActivityDataCommand.cs
 *  Purpose         : Enum Data type for Indetifying SQL Statement from UI request
 *  Author          : CS
 *  Created on      : 02-Aug-2010
 */

namespace Payroll.DAO.Schema
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
            Authenticate
        }

        public enum PayrollDepartment
        {
            FetchPayrollDepartment,
            FetchPayrollDepartmentById,
            InsertPayrollDepartment,
            UpdatePayrollDepartment,
            DeletePayrollDepartment,         
        }

        public enum PayrollWorkLocation
        {
            FetchPayrollWorkLocation,
            FetchPayrollWorkLocationById,
            InsertPayrollWorkLocation,
            UpdatePayrollWorkLocation,
            DeletePayrollWorkLocation,
        }

        public enum NameTitle
        {
            FetchNameTitle,
            FetchNameTitleById,
            InsertNameTitle,
            UpdateNameTitle,
            DeleteNameTitle,
        }

        public enum Payroll
        {
            #region Payroll GateWay

            FetchPayrollGateWay,
            PayrollAdd,
            UpdatePayrollStatus,
            StaffGroupAdd,
            GetCurrentPayroll,
            GetPreviousPayrollMonth,
            GetLatestPayroll,
            LockPayroll,
            DeletePayrollStaff,
            DletePayrollTemp,
            DeleteStaffGroup,
            DeleteCompMonth,
            DeletePayrollStatus,
            DeletePayrollLoad,
            DeletePayroll,
            FetchPayrollGroupByDept,
            FetchPayrollGroupByGroup,
            FetchPayrollComponentByDept,
            FetchPayrollComponentByGroup,
            ConstructQuery,
            ConstructQueryToaddcombo,
            DeletePRStaff,
            DeletePRStaffTemo,
            DeletePRStaffTempByNotEditableValue,
            DeletePRStaffGroup,
            DeletePRCompMonth,
            DeletePRStatus,
            UpdateInstallment,
            DeletePRLoanPaid,
            DeletePRCreate,
            FetchVoucherMastersByPayrollId,
            FetchPrGateWay,
            GetServerDateTimeFormat,
            PayrollDetailsAdd,
            PayrollDetailsUpdate,
            FetchPayrollDateInterval,
            FetchPayrollProcessDate,
            DeleteProcessLedger,
            FetchPayrollComponentforpayment,
            UpdateComponentinEquation,
            UpdatePRStaffGroupByPayrollId,
            #endregion

            #region Payroll Grade

            NewPayrollCreate,
            PayrollDefineStatus,
            PayrollCheck,
            GetPayrollCreation,
            GetGradeUnalloted,
            ShowAllocatedGrade,
            GetGradeAlloted,
            UpdateStaffGrade,
            AllocateStaffGrade,
            GetPayrollId,
            GetPayrollData,
            UnMapStaffGroup,
            FetchPayrollSetting,
            InsertPayrollSetting,
            #endregion

            #region PayrollProcess

            PayrollProcessList,
            PayrollProcessUpdate,
            PayrollProcessGet,
            DeleteMapLedger,
            AddMapLedger,
            IsProcessLedgerExists,
            FetchMappedLedger,
            FetchMappedLedgersByTypeId,
            FetchLedgerByLedgerId,
            DeleteVoucherTransPayrollVoucher,
            FetchVoucherMasterPayrollVoucher,
            FetchMappedComponentsbyProjectId,
            FetchProcessByMappedLedger,
            #endregion

            #region Payroll Group
            PayrollGroupList,
            InsertPayrollGroup,
            GetPayrollGroup,
            GetPayrollGroupById,
            PayrollGroupUpdate,
            PayrollGroupDelete,
            PayrollGroupExist,
            PayrollGradeId,
            PayrollGroupbyGroupCategory,
            PayrollThirdPartyGroupId,
            GetDepartments,
            GetGroupByPayrollId,
            #endregion

            #region Payroll component
            PayrollComponenetSelect,
            PayrollComponent,
            PayrollComponentFetchAll,
            PayrollComponentWithType,
            PayrollComponentAdd,
            PayrollComponentEdit,
            PayrollComponentDelete,
            PayrollEditVerifyCompLink,
            PayrollEditCompUpdate,
            FetchLedger,
            PayrollCompSelect,
            FetchTopTwoPayrollId,
            FetchPreviousPayrollId,
            GetPreviousValue,
            FetchComponentByComponentId,
            DeleteComponentById,
            FetchPayrollcomponentTypeID,
            FetchPayrollComponent,
            FetchPayrollComponetName,
            FetchComponentByGroupIds,
            FetchStaffByGroupIds,
            FetchComponentValuetoProcess,
            FetchLoanComponentValue,
            IsComponentLedgerExists,
            IsLedgerMappedWithProject,
            IsLoanledgerMappedwithProject,
            FetchRangeListbyCompId,
            FetchRangeComponentById,
            CheckComponentMappedToGroup,
            FetchComponentInEquationAndMapped,
            #endregion

            #region Payroll CompMonth
            PrcompMonthAdd,
            ImportPreviousPayroll,
            ClearPayExtraInfo,
            ResetPayExtraInfo,
            #endregion

            #region Payroll Loan

            PayrollLoan,
            PayRollLoanDetail,
            PayRollLoanDetails,
            PayrollLoanList,
            PayrollLoanInsert,
            PayrollLoanOccur,
            PayrollLoanEdit,
            PayrollLoanDelete,
            PayrollCategoryFetch,
            PayrollComponentFetch,
            PayrollFetchDetailsBrowse,
            PayrollFetchAssignDetiailBrowse,
            FetchCashBankLedgersofSelectedStaff,
            FetchLoanType,
            FetchStaff,
            FetchLoanPaid,
            FetchStaffLoanType,
            PayrollLoanUpdate,
            PayrollLoanGetUpdate,
            PayrollLoanDetailfroComponent,
            PayrollIncomeforComponent,
            PayrollTextvalforComponent,
            #endregion
            #region Staff Details

            FetchStaffDetails,
            FetchStaffLoanGet,
            FetchStaffLoanPaid,
            ProcessPayrollInOrder,
            GetComponentIdByGroupId,

            FetchComponentIDbySalaryGroupId,
            FetchComponentNotinPRMonth,
            DeleteExistingPayroll,
            FetchStaffDetailsToProcessPayroll,
            FetchStaffTempDetailComponent,
            UpdateProcessedPayroll,
            FetchStaffDetailsAfterProcess,
            InsertNewDataValueForStaff,
            FetchStaffTempDetails, UpdateBasicPay,
            UpdateEarning1, UpdateEarning2, UpdateEarning3, UpdateDeduction1, UpdateDeduction2, UpdatePayingSalaryDays,
            AddPrStaff,
            UpdatePrStaffForLastPayroll,
            ClearInvalidPaydetails,
            PayrollStaffEdit,
            PayrollStaffList,
            PaymonthStaffProfile,
            PayrollStaffInsert,
            PayrollStaffOccur,
            PayrollStaffDegreeOccur,
            PayrollStaffDesigOccur,
            PayrollStaffDegreeList,
            PayrollStaffDesigList,
            PayrollStaffDetails,
            PayrollStaffOutOfService,
            PayrollStaffInservice,
            PayrollStaffDeptList,
            PayrollStaffScale,
            PayrollStaffDelete,
            PayrollStaffDel,
            PayrollStaffDelSel,
            PayrollStaffNamesAndIds,
            PayrollStaffCommendsDelete,
            PayrollStaffSelectedNamesAndIds,
            FetchFormulaGroup,
            PayrollStaffserviceInsert,
            PayrollStaffserviceEdit,
            PayrollCommentPerformanceInsert,
            PayrollCommentPerformanceEdit,
            PayrollStaffIdByStaffRefUniqueId,
            PayrollStaffmodify,
            FetchprStaffDetails,
            FetchprDefValueDetails,
            PayrollStaffUpdate,
            PayrollStaffInsertDetails,
            AutoFetchDesignation,
            FetchProjectIdByStaffId,
            FetchGroupIdByStaffId,
            FetchStaffidBystaffCode,
            FetchIdByStaffIDAccountId,
            FetchPaymonthStaffProfile,
            DeleteStaffProfile,
            #endregion


            #region Payroll Loan
            FetchPayrollLoanId,
            FetchLoanDueAmount,
            FetchPaidAmountForExistingPayroll,
            InsertLoanPaidTable,
            FetchpayrollLoanPaid,
            FetchLoanDetails,
            UpdateInstallmentByLoanId,
            UpdateInstallmentStatusByLoanId,
            #endregion

            #region PayrollMonth

            FetchPayrollCompMonthByGroupId,
            DeletePayrollCompMonthByGrouId,
            UpdatePayrollCompMonthByGroupId,

            #endregion

            #region PayrollComponentAllocate
            PayrollComponentList,
            PayrollGetGroupList,
            PayrollFullCompList,
            PayrollCompInsert,
            PayrollCompCheckSelect,
            PayrollCompDelete,
            PayrollCompchange,
            PayrollFormulaForGroup,
            PayrollCompStaffID,
            PayrollCompName,
            PayrollProcessDelete,
            PayrollProcessCheck,
            PayrollInsertProcess,
            PayrollCompIdReturn,
            FetchPRCompMonthByComponentId,
            PrCreatecompMonthAdd,
            PRCompMonthDeleteByCompGroup,
            FetchPrReProcess,
            FetchMapComponent,
            FetchPayrollAbstractComponent,
            CheckComponentsMappedORNOt,
            CheckStaffGroupMapped,
            #endregion


            #region Payslip
            FetchReportCode,
            FetchPayrollDate,
            FetchPayrollDateForPayslip,
            FetchStaffDetailsByStaffId,
            FetchStaffAllDetails,
            FetchStaffDetailsForDailyReport,
            FetchStaffDetailsByDepartmentId,
            FetchPayrollCompMonth,
            FetchComponentForReport,
            FetchValuesbyComponent,
            FetchValuesbyComponentStaffGroup,
            FetchValuesForPaySlip,
            PostedPayrollVouchers,
            PostedPayrollVoucherDetail,
            RemoveDefaultComponentsForMultiCurrency,
            #endregion

            #region Reports
            FetchGroupByForReport,
            FetchReportPropertiesForReport,
            FetchReportFieldsForReport,
            FetchComponentReport,
            FetchComponent01,
            FetchDept01,
            FetchGroup01,
            FetchPayroll,
            FetchReportFieldsWithoutGroup,
            FetchReportFieldsWithGroup,
            FetchAbstractComponent,
            DropTable,
            FetchComponentByCompOrder,
            FetchComponentByCompOrderWithoutGroup,
            FetchComponentByCompOrderForWages,
            FetchComponentByCompOrderForWagesWithoutGroup,
            DeleteTable,
            FetchStaffByGroup,
            FetchStaffByGroupByDate,
            FetchFromTable,
            FetchRPTLLED01,
            FetchRPTLLED02,
            FetchDuplicateComponent,
            FetchDuplicateCaption,
            CheckEditComponent,
            InsertPrComponent,
            UpdatePrComponent,
            UpdatePrCompMonth,
            GetStaffComponentId,
            FetchFromTableWithWhere,
            PayrollExistOpen,
            PayrollList,
            PayrollLockStatus,
            PayrollSetlockStatus,
            PayrollCreatedDelete,
            PayrollStatusDelete,
            PayrollStaffIdColl,
            PayrollFormulaGroupId,
            PayrollFormulaUpdateGroupId,
            PayrollStaffSelectedUpdatedNamesAndIds,
            LockUnlockPayroll,
            PayrollLoanManagementStaff,
            PayrollLoanMgtDetails,
            FetchPayrollLoanDetails,
            InsertPayrollLoanDetails,
            PayrollLoanMntList,
            PayrollLoanMntAdd,
            PayrollLoanMntEdit,
            GetStaffRetirementDate,
            DeletePayrollLoan,
            DeletePrComponent,
            FetchNoOfAbsents,
            FetchPrStaffGroup,
            FetchCurrentInstallment,
            DeleteVoucherMasterTransByPrloanGetId,
            DeleteVoucherTransByPrloanGetId,
            FetchLoanStaffDetailsByStaffId,
            #endregion

            #region payroll grade
            GetGroupStaffSQL,
            GetUnassignedStaff,
            GetMappedStaffs,
            GetUnMappedStaffs,
            GetAllUnMappedStaffs,
            GetUnDefinedStaffGroupSQL,
            GetGroupSQL,
            FetchPRStaffStatutoryComplianceByPayrollId,
            GetMaxStaffSortOrder,
            InsertPRStaffGroup,
            InsertPRStaffGroupForAllMonths,
            DeleteProjectIdStaff,
            DeleteProjectStaff,
            InsertPRProjectStaff,
            UpdatePRStaffGroup,
            SaveStaffGroupOrder,
            InsertPRStaffStatutoryCompliance,

            DeletePRStaffByStaffId,
            DeletePrLoanGetByStaffId,
            DeletePrLoanPaidbyStaffId,
            DeletePrLoanPaidbyLoanId,
            DeletePrStaffGroupByStaffId,
            DeletePRStaffStatutoryCompliance,
            GetProjectGroupMappedStaffs,

            DeletePayrollByPayrollId,
            #endregion

            #region Payroll PRocess

            FetchPRStaff, FetchPayrollStaffGroup, FetchPProcessStaffGroup, FetchPrCompMonthStaffOrder, ReprocessDate, FetchReprocessDate,
            #endregion
            
            #region  Reports
            DailyPFReport,
            DailyPFReports,
            DailyPFReportsForAll,
            MonthlyPFReports,
            MonthlyPFReportsForAll,
            EmployeePFReport,
            FetchWagesReport,
            PaymentAdviceBank,
            StaffEPF,
            StaffPTRegister,
            PTRateDetails,
            #endregion

            #region Mapping and Transactions
            ProjectLedgerMapping,
            ProjectLedgerMappingDelete,
            IsLedgerExists,
            FetchLedgerByLedgerName,
            FetchLedgerIdLedgerNameByLedgerId,
            CheckLoanExists,
            #endregion

            #region Payroll Project
            DeleteProjectpayroll,
            MapProjectToPayroll,
            FetchProjectsforPayroll,
            FetchMappedPayrollProjects,
            FetchMappedProjectCashBankLedgers,
            CheckComponentsProcessedforProject,
            CheckProjectExists,
            CheckProjectExistforPayroll,
            FetchprojectIdByProjectName,
            #endregion

            #region Payroll Range Formula
            InsertRangeCondtions,
            FetchRangeComponents,
            FetchRangeValuesByComponentId,
            DeleteRangeByComponentId,
            #endregion

            #region Payroll Post payemnt
            FetchLegderDetailsByPorjectId,
            FetchLiabilityLedgersByProjectId,
            FetchProcessedValuesBySelectedComponentID,
            FetchPayrollpostpayment,
            UpdatePostPaymentDetails,
            FetchVoucherIdByPostId,
            DeletePostPaymentDetails,
            FetchPostVoucherBalanceDetails,
            FetchDecutionComponents,
            FetchProcessedValuesofDeductionComponents,
            FetchsumofPostPaymentAmountByPayrollId,
            ValidateSumofPostvoucheramountBypayrollid,

            FetchPayrollPostPaymentsByPayrollId,
            FetchPayrollPostPaymentsByPayrollIdCompId,
            FetchPayrollGroupByPosting,
            FetchPayrollPostPending,
            FetchPayrollPostPaymentByVoucherId,
            FetchPayrollPostPaymentVouhcerMaster,
            FetchPayrollPostPaymentVouhcerDetails,
            IsPayrollPostPaymentPosted,
            InsertPostPaymentDetails,
            DeletePayrollPostPaymentVouchersByVoucherId,
            DeletePayrollPostByVoucherId,
            #endregion

            #region Payroll Paymentmode
            FetchPayrollPaymentMode,
            #endregion

        }
    }
}