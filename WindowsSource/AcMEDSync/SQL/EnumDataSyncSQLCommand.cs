using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcMEDSync.SQL
{
    public class EnumDataSyncSQLCommand
    {
        public enum ImportSQL
        {
            IsLegalEntityExists,
            AddLegalEntity,
            IsProjectCatogoryExists,
            AddProjectCatogory,
            IsProjectExists,
            AddProject,
            IsLedgerGroupExists,
            AddLedgerGroup,
            IsLedgerExists,
            AddLedger,
            IsFCPurposeExists,
            AddFCPurpose,
            GetLegalEntityId,
            GetProjectCategoryId,
            GetProjectId,
            GetLedgerGroupId,
            GetLedgerId,
            GetFCPurposeId,
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
            IsGroupCodeExist

        }

        public enum Portal
        {
            FetchBranchDetails,
            FetchHeadOfficeDetails
        }

        public enum ImportVoucher
        {
            AuthenticateBranchCode,
            AuthenticateHeadOfficeCode,
            FetchDataBase,
            FetchBranchOfficeId,
            FetchTransactions,
            InsertVoucherMaster,
            InsertVoucherTrans,
            InsertVoucherCostCentre,
            InsertLedger,
            InsertCostCentre,
            InsertCountry,
            InsertDonor,
            InsertBank,
            InsertBankAccount,
            InsertLedgerBank,
            InsertLedgerBalance,
            DeleteLedgerBalance,
            DeleteVoucherMasterTrans,
            DeleteVoucherTrans,
            DeleteVoucherCostCentre,

            //Get Id
            GetProjectId,
            GetDonorId,
            GetLedgerId,
            GetBankId,
            GetPurposeId,
            GetCostCentreId,
            GetCountryId,

            //Exists
            IsProjectExists,
            IsDonorExists,
            IsBankAccountExists,
            IsBankExists,
            IsPurposeExists,
            IsCountryExists,
            IsLedgerExists,
            IsLedgerBankExists,
            IsCostCentreExists,

            //Mapping
            MapProjectLedger,
            MapProjectDonor,
            MapProjectCostcentre
        }

        public enum Id
        {
            Project,
            Donor,
            Ledger,
            Bank,
            Purpose,
            CostCentre,
            Country
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
            FetchMasterVouchers,
            FetchVoucherTransactions,
            FetchVoucherCostCentres,
            FetchProjects,
            FetchDonors,
            FetchBankDetails,
            FetchLedgerBalance,
            FetchCountry,
            FetchHeadOfficeLedger
        }

        public enum DeleteLedgers
        {
            BranchHeadOfficeLedger,
            MappedLedger,
            MasterLedger,
            UnmapProjectLedger,
            LedgerBalance,
            BudgetLedger
        }
    }
}
