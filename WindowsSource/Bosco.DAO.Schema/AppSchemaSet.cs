using System;
using System.Collections.Generic;
using System.Text;
using Bosco.Utility;
using Bosco.Utility.CommonMemberSet;

namespace Bosco.DAO.Schema
{
    public class AppSchemaSet : CommonMember
    {
        private ApplicationSchemaSet appSchemaSet = null;
        public ApplicationSchemaSet AppSchema
        {
            get
            {
                if (appSchemaSet == null)
                {
                    appSchemaSet = new ApplicationSchemaSet();
                }
                return appSchemaSet;
            }
        }

        public class ApplicationSchemaSet
        {
            private EnumTypeSchema.EnumTypeDataTable enumSchema = null;
            public EnumTypeSchema.EnumTypeDataTable EnumSchema
            {
                get
                {
                    if (enumSchema == null)
                    {
                        enumSchema = new EnumTypeSchema.EnumTypeDataTable();
                    }
                    return enumSchema;
                }
            }

            private ApplicationSchema.UserDataTable userSchema = null;
            public ApplicationSchema.UserDataTable User
            {
                get
                {
                    if (userSchema == null)
                    {
                        userSchema = new ApplicationSchema.UserDataTable();
                    }
                    return userSchema;
                }
            }

            private ApplicationSchema.LedgerGroupDataTable ledgerGroupSchema = null;
            public ApplicationSchema.LedgerGroupDataTable LedgerGroup
            {
                get
                {
                    if (ledgerGroupSchema == null)
                    {
                        ledgerGroupSchema = new ApplicationSchema.LedgerGroupDataTable();
                    }
                    return ledgerGroupSchema;
                }
            }

            private ApplicationSchema.CountryDataTable CountrySchema = null;
            public ApplicationSchema.CountryDataTable Country
            {
                get
                {
                    if (CountrySchema == null)
                    {
                        CountrySchema = new ApplicationSchema.CountryDataTable();
                    }
                    return CountrySchema;
                }
            }

            private ApplicationSchema.BankDataTable bankSchema = null;
            public ApplicationSchema.BankDataTable Bank
            {
                get
                {
                    if (bankSchema == null)
                    {
                        bankSchema = new ApplicationSchema.BankDataTable();
                    }
                    return bankSchema;
                }

            }

            private ApplicationSchema.AuditDataTable auditSchema = null;
            public ApplicationSchema.AuditDataTable Audit
            {
                get
                {
                    if (auditSchema == null)
                    {
                        auditSchema = new ApplicationSchema.AuditDataTable();
                    }
                    return auditSchema;
                }

            }

            private ApplicationSchema.STATEDataTable stateSchema = null;
            public ApplicationSchema.STATEDataTable State
            {
                get
                {
                    if (stateSchema == null)
                    {
                        stateSchema = new ApplicationSchema.STATEDataTable();
                    }
                    return stateSchema;
                }

            }
            private ApplicationSchema.CostCentreDataTable costCentreSchema = null;
            public ApplicationSchema.CostCentreDataTable CostCentre
            {
                get
                {
                    if (costCentreSchema == null) { costCentreSchema = new ApplicationSchema.CostCentreDataTable(); }
                    return costCentreSchema;
                }
            }

            private ApplicationSchema.DonorAuditorDataTable donorAuditorSchema = null;
            public ApplicationSchema.DonorAuditorDataTable DonorAuditor
            {
                get
                {
                    if (donorAuditorSchema == null) { donorAuditorSchema = new ApplicationSchema.DonorAuditorDataTable(); }
                    return donorAuditorSchema;
                }
            }

            private ApplicationSchema.tds_sectionDataTable tdssection = null;
            public ApplicationSchema.tds_sectionDataTable TDSSection
            {
                get
                {
                    if (tdssection == null)
                    {
                        tdssection = new ApplicationSchema.tds_sectionDataTable();
                    }
                    return tdssection;
                }
            }


            private ApplicationSchema.InKindArticleDataTable inKindArticle = null;
            public ApplicationSchema.InKindArticleDataTable InKindArticle
            {
                get
                {
                    if (inKindArticle == null) { inKindArticle = new ApplicationSchema.InKindArticleDataTable(); }
                    return inKindArticle;
                }
            }

            private ApplicationSchema.ExecutiveMemberDataTable executiveMembers = null;
            public ApplicationSchema.ExecutiveMemberDataTable ExecutiveMembers
            {
                get
                {
                    if (executiveMembers == null) { executiveMembers = new ApplicationSchema.ExecutiveMemberDataTable(); }
                    return executiveMembers;
                }
            }

            private ApplicationSchema.CurrencySymbolsDataTable currencySymbols = null;
            public ApplicationSchema.CurrencySymbolsDataTable CurrencySymbols
            {
                get
                {
                    if (currencySymbols == null) { currencySymbols = new ApplicationSchema.CurrencySymbolsDataTable(); }
                    return currencySymbols;
                }
            }

            private ApplicationSchema.CurrencyCodeDataTable currencyCode = null;
            public ApplicationSchema.CurrencyCodeDataTable CurrencyCode
            {
                get
                {
                    if (currencyCode == null) { currencyCode = new ApplicationSchema.CurrencyCodeDataTable(); }
                    return currencyCode;
                }
            }

            private ApplicationSchema.BankAccountDataTable BankAccountShema = null;
            public ApplicationSchema.BankAccountDataTable BankAccount
            {
                get
                {
                    if (BankAccountShema == null) { BankAccountShema = new ApplicationSchema.BankAccountDataTable(); }
                    return BankAccountShema;
                }
            }

            private ApplicationSchema.BudgetDataTable BudgetSchema = null;
            public ApplicationSchema.BudgetDataTable Budget
            {
                get
                {
                    if (BudgetSchema == null) { BudgetSchema = new ApplicationSchema.BudgetDataTable(); }
                    return BudgetSchema;
                }
            }

            private ApplicationSchema.BUDGET_STATISTICS_DETAILDataTable BudgetStatisticsSchema = null;
            public ApplicationSchema.BUDGET_STATISTICS_DETAILDataTable BudgetStatistics
            {
                get
                {
                    if (BudgetStatisticsSchema == null) { BudgetStatisticsSchema = new ApplicationSchema.BUDGET_STATISTICS_DETAILDataTable(); }
                    return BudgetStatisticsSchema;
                }
            }

            private ApplicationSchema.BUDGET_STRENGTH_DETAILDataTable BudgetStrengthSchema = null;
            public ApplicationSchema.BUDGET_STRENGTH_DETAILDataTable BudgetStrength
            {
                get
                {
                    if (BudgetStrengthSchema == null) { BudgetStrengthSchema = new ApplicationSchema.BUDGET_STRENGTH_DETAILDataTable(); }
                    return BudgetStrengthSchema;
                }
            }



            private ApplicationSchema.AllotFundDataTable AllotingFund = null;
            public ApplicationSchema.AllotFundDataTable AllotFund
            {
                get
                {
                    if (AllotingFund == null) { AllotingFund = new ApplicationSchema.AllotFundDataTable(); }
                    return AllotingFund;
                }
            }

            private ApplicationSchema.LedgerDataTable LedgerShema = null;
            public ApplicationSchema.LedgerDataTable Ledger
            {
                get
                {
                    if (LedgerShema == null) { LedgerShema = new ApplicationSchema.LedgerDataTable(); }
                    return LedgerShema;
                }
            }

            private ApplicationSchema.VoucherDataTable VoucherSchema = null;
            public ApplicationSchema.VoucherDataTable Voucher
            {
                get
                {
                    if (VoucherSchema == null) VoucherSchema = new ApplicationSchema.VoucherDataTable();
                    return VoucherSchema;
                }

            }

            private ApplicationSchema.LegalEntityDataTable LegalEntitySchema = null;
            public ApplicationSchema.LegalEntityDataTable LegalEntity
            {
                get
                {
                    if (LegalEntitySchema == null) LegalEntitySchema = new ApplicationSchema.LegalEntityDataTable();
                    return LegalEntitySchema;
                }

            }


            private ApplicationSchema.ProjectDataTable ProjectSchema = null;
            public ApplicationSchema.ProjectDataTable Project
            {
                get
                {
                    if (ProjectSchema == null) { ProjectSchema = new ApplicationSchema.ProjectDataTable(); }
                    return ProjectSchema;
                }
            }

            private ApplicationSchema.PurposesDataTable PurposeSchema = null;
            public ApplicationSchema.PurposesDataTable Purposes
            {
                get
                {
                    if (PurposeSchema == null) { PurposeSchema = new ApplicationSchema.PurposesDataTable(); }
                    return PurposeSchema;
                }
            }
            private ApplicationSchema.BranchOfficeDataTable branchOffice = null;
            public ApplicationSchema.BranchOfficeDataTable BranchOffice
            {
                get
                {
                    if (branchOffice == null) { branchOffice = new ApplicationSchema.BranchOfficeDataTable(); }
                    return branchOffice;
                }
            }
            private ApplicationSchema.Project_VoucherDataTable ProjectVoucherSchema = null;
            public ApplicationSchema.Project_VoucherDataTable ProjectVoucher
            {
                get
                {
                    if (ProjectVoucherSchema == null) { ProjectVoucherSchema = new ApplicationSchema.Project_VoucherDataTable(); }
                    return ProjectVoucherSchema;
                }
            }

            private ApplicationSchema.SettingDataTable SettingSchema = null;
            public ApplicationSchema.SettingDataTable Settings
            {
                get
                {
                    if (SettingSchema == null) { SettingSchema = new ApplicationSchema.SettingDataTable(); }
                    return SettingSchema;
                }
            }

            private ApplicationSchema.CultureDataTable CultureSchema = null;
            public ApplicationSchema.CultureDataTable Culture
            {
                get
                {
                    if (CultureSchema == null) { CultureSchema = new ApplicationSchema.CultureDataTable(); }
                    return CultureSchema;
                }
            }

            private ApplicationSchema.dtSettingDataTable dtSetting = null;
            public ApplicationSchema.dtSettingDataTable Setting
            {
                get
                {
                    if (dtSetting == null) { dtSetting = new ApplicationSchema.dtSettingDataTable(); }
                    return dtSetting;
                }
            }

            private ApplicationSchema.DivisionDataTable DivisionSchema = null;
            public ApplicationSchema.DivisionDataTable Division
            {
                get
                {
                    if (DivisionSchema == null) { DivisionSchema = new ApplicationSchema.DivisionDataTable(); }
                    return DivisionSchema;
                }
            }

            private ApplicationSchema.ACCOUNT_TYPEDataTable AccountTypeSchema = null;
            public ApplicationSchema.ACCOUNT_TYPEDataTable AccountType
            {
                get
                {
                    if (AccountTypeSchema == null) { AccountTypeSchema = new ApplicationSchema.ACCOUNT_TYPEDataTable(); }
                    return AccountTypeSchema;
                }
            }

            private ApplicationSchema.Audit_InfoDataTable AuditInfoSchema = null;
            public ApplicationSchema.Audit_InfoDataTable AuditInfo
            {
                get
                {
                    if (AuditInfoSchema == null) { AuditInfoSchema = new ApplicationSchema.Audit_InfoDataTable(); }
                    return AuditInfoSchema;
                }
            }

            private ApplicationSchema.UserRightsDataTable AcmeUserRights = null;
            public ApplicationSchema.UserRightsDataTable UserRights
            {
                get
                {
                    if (AcmeUserRights == null) { AcmeUserRights = new ApplicationSchema.UserRightsDataTable(); }
                    return AcmeUserRights;
                }
            }

            private ApplicationSchema.AddressBookDataTable AddressBookSchema;
            public ApplicationSchema.AddressBookDataTable AddressBook
            {
                get
                {
                    if (AddressBookSchema == null) { AddressBookSchema = new ApplicationSchema.AddressBookDataTable(); }
                    return AddressBookSchema;
                }
            }

            private ApplicationSchema.ProjectCatogoryDataTable _ProjectCatogory;
            public ApplicationSchema.ProjectCatogoryDataTable ProjectCatogory
            {
                get
                {
                    if (_ProjectCatogory == null) { _ProjectCatogory = new ApplicationSchema.ProjectCatogoryDataTable(); }
                    return _ProjectCatogory;
                }
            }

            private ApplicationSchema.ProjectCatogoryITRGroupDataTable _ProjectCatogoryitrGroup;
            public ApplicationSchema.ProjectCatogoryITRGroupDataTable ProjectCatogoryITRGroup
            {
                get
                {
                    if (_ProjectCatogoryitrGroup == null) { _ProjectCatogoryitrGroup = new ApplicationSchema.ProjectCatogoryITRGroupDataTable(); }
                    return _ProjectCatogoryitrGroup;
                }
            }

            private ApplicationSchema.CostCentreCategoryDataTable _CostCentreCategory;
            public ApplicationSchema.CostCentreCategoryDataTable CostCentreCategory
            {
                get
                {
                    if (_CostCentreCategory == null) { _CostCentreCategory = new ApplicationSchema.CostCentreCategoryDataTable(); }
                    return _CostCentreCategory;
                }
            }

            private ApplicationSchema.UserRoleDataTable userRole;
            public ApplicationSchema.UserRoleDataTable UserRole
            {
                get
                {
                    if (userRole == null) { userRole = new ApplicationSchema.UserRoleDataTable(); }
                    return userRole;
                }
            }

            private ApplicationSchema.MasterRightsDataTable masterRightsTable;
            public ApplicationSchema.MasterRightsDataTable MasterRights
            {
                get
                {
                    if (masterRightsTable == null) { masterRightsTable = new ApplicationSchema.MasterRightsDataTable(); }
                    return masterRightsTable;
                }
            }

            private ApplicationSchema.VoucherMasterDataTable voucherMasterTable;
            public ApplicationSchema.VoucherMasterDataTable VoucherMaster
            {
                get
                {
                    if (voucherMasterTable == null) { voucherMasterTable = new ApplicationSchema.VoucherMasterDataTable(); }
                    return voucherMasterTable;
                }
            }

            private ApplicationSchema.VoucherTransactionDataTable voucherTransactionTable;
            public ApplicationSchema.VoucherTransactionDataTable VoucherTransaction
            {
                get
                {
                    if (voucherTransactionTable == null) { voucherTransactionTable = new ApplicationSchema.VoucherTransactionDataTable(); }
                    return voucherTransactionTable;
                }
            }

            private ApplicationSchema.LedgerBalanceDataTable ledgerBalanceTable;
            public ApplicationSchema.LedgerBalanceDataTable LedgerBalance
            {
                get
                {
                    if (ledgerBalanceTable == null) { ledgerBalanceTable = new ApplicationSchema.LedgerBalanceDataTable(); }
                    return ledgerBalanceTable;
                }
            }

            private ApplicationSchema.VoucherCostCentreDataTable Voucher_Cost_Centre;
            public ApplicationSchema.VoucherCostCentreDataTable VoucherCostCentre
            {
                get
                {
                    if (Voucher_Cost_Centre == null) { Voucher_Cost_Centre = new ApplicationSchema.VoucherCostCentreDataTable(); }
                    return Voucher_Cost_Centre;
                }
            }

            private ApplicationSchema.SubLedgerDataTable Sub_Ledger;
            public ApplicationSchema.SubLedgerDataTable SubLedger
            {
                get
                {
                    if (Sub_Ledger == null) { Sub_Ledger = new ApplicationSchema.SubLedgerDataTable(); }
                    return Sub_Ledger;
                }
            }


            private ApplicationSchema.VoucherSubLegerDataTable Voucher_Sub_Ledger;
            public ApplicationSchema.VoucherSubLegerDataTable VoucherSubLedger
            {
                get
                {
                    if (Voucher_Sub_Ledger == null) { Voucher_Sub_Ledger = new ApplicationSchema.VoucherSubLegerDataTable(); }
                    return Voucher_Sub_Ledger;
                }
            }


            private ApplicationSchema.BreakUpDataTable _BreakUp;
            public ApplicationSchema.BreakUpDataTable BreakUp
            {

                get
                {
                    if (_BreakUp == null) { _BreakUp = new ApplicationSchema.BreakUpDataTable(); }
                    return _BreakUp;
                }
            }

            private ApplicationSchema.VoucherNumberDataTable voucherNumber;
            public ApplicationSchema.VoucherNumberDataTable VoucherNumber
            {

                get
                {
                    if (voucherNumber == null) { voucherNumber = new ApplicationSchema.VoucherNumberDataTable(); }
                    return voucherNumber;
                }
            }

            private ApplicationSchema.AccountingYearDataTable accountingPeriod;
            public ApplicationSchema.AccountingYearDataTable AccountingPeriod
            {

                get
                {
                    if (accountingPeriod == null) { accountingPeriod = new ApplicationSchema.AccountingYearDataTable(); }
                    return accountingPeriod;
                }
            }

            private ApplicationSchema.FDRegistersDataTable fdRegisters;
            public ApplicationSchema.FDRegistersDataTable FDRegisters
            {
                get
                {
                    if (fdRegisters == null)
                    {
                        fdRegisters = new ApplicationSchema.FDRegistersDataTable();
                    }
                    return fdRegisters;
                }
            }

            private ApplicationSchema.InKindTransactionDataTable inKindTrans;
            public ApplicationSchema.InKindTransactionDataTable InKindTrans
            {
                get
                {
                    if (inKindTrans == null)
                    {
                        inKindTrans = new ApplicationSchema.InKindTransactionDataTable();
                    }
                    return inKindTrans;
                }
            }

            private ApplicationSchema.VoucherFDInterestDataTable voucherFDInterest;
            public ApplicationSchema.VoucherFDInterestDataTable VoucherFDInterest
            {
                get
                {
                    if (voucherFDInterest == null)
                    {
                        voucherFDInterest = new ApplicationSchema.VoucherFDInterestDataTable();
                    }
                    return voucherFDInterest;
                }
            }

            private ApplicationSchema.FDAccountDataTable fdAccount;
            public ApplicationSchema.FDAccountDataTable FDAccount
            {
                get
                {
                    if (fdAccount == null)
                    {
                        fdAccount = new ApplicationSchema.FDAccountDataTable();
                    }
                    return fdAccount;
                }
            }

            private ApplicationSchema.FDRenewalDataTable fdRenewal;
            public ApplicationSchema.FDRenewalDataTable FDRenewal
            {
                get
                {
                    if (fdRenewal == null)
                    {
                        fdRenewal = new ApplicationSchema.FDRenewalDataTable();
                    }
                    return fdRenewal;
                }

            }

            private ApplicationSchema.DashBoardDataTable Dashboard;
            public ApplicationSchema.DashBoardDataTable DashBoard
            {
                get
                {
                    if (Dashboard == null)
                    {
                        Dashboard = new ApplicationSchema.DashBoardDataTable();
                    }
                    return Dashboard;
                }
            }

            private ApplicationSchema.AcMEERPLicenseDataTable licenseDataTable;
            public ApplicationSchema.AcMEERPLicenseDataTable LicenseDataTable
            {
                get
                {
                    if (licenseDataTable == null)
                    {
                        licenseDataTable = new ApplicationSchema.AcMEERPLicenseDataTable();
                    }
                    return licenseDataTable;
                }
            }

            private ApplicationSchema.NatureofPaymentsDataTable Natureofpaymentschema = null;
            public ApplicationSchema.NatureofPaymentsDataTable NatureofPayment
            {
                get
                {
                    if (Natureofpaymentschema == null) { Natureofpaymentschema = new ApplicationSchema.NatureofPaymentsDataTable(); }
                    return Natureofpaymentschema;
                }
            }

            private ApplicationSchema.DutyTaxDataTable dutyTax;
            public ApplicationSchema.DutyTaxDataTable DutyTax
            {
                get
                {
                    if (dutyTax == null)
                    {
                        dutyTax = new ApplicationSchema.DutyTaxDataTable();
                    }
                    return dutyTax;
                }
            }

            private ApplicationSchema.DutyTaxTypeDataTable dutyTaxType;
            public ApplicationSchema.DutyTaxTypeDataTable DutyTaxType
            {
                get
                {
                    if (dutyTaxType == null)
                    {
                        dutyTaxType = new ApplicationSchema.DutyTaxTypeDataTable();
                    }
                    return dutyTaxType;
                }
            }

            private ApplicationSchema.DeducteeTypesDataTable deducteetypes;
            public ApplicationSchema.DeducteeTypesDataTable DeducteeTypes
            {
                get
                {
                    if (deducteetypes == null)
                    {
                        deducteetypes = new ApplicationSchema.DeducteeTypesDataTable();
                    }
                    return deducteetypes;
                }
            }

            private ApplicationSchema.ExportVouchersDataTable exportvouchers;
            public ApplicationSchema.ExportVouchersDataTable ExportVouchers
            {
                get
                {
                    if (exportvouchers == null)
                    {
                        exportvouchers = new ApplicationSchema.ExportVouchersDataTable();
                    }
                    return exportvouchers;
                }
            }

            private ApplicationSchema.DataSynStatusDataTable dataSynStatus;
            public ApplicationSchema.DataSynStatusDataTable DataSyncStatus
            {
                get
                {
                    if (dataSynStatus == null)
                    {
                        dataSynStatus = new ApplicationSchema.DataSynStatusDataTable();
                    }
                    return dataSynStatus;
                }
            }

            private ApplicationSchema.LedgerProfileDataTable ledgerProfile;
            public ApplicationSchema.LedgerProfileDataTable LedgerProfileData
            {
                get
                {
                    if (ledgerProfile == null)
                    {
                        ledgerProfile = new ApplicationSchema.LedgerProfileDataTable();
                    }
                    return ledgerProfile;
                }
            }

            private ApplicationSchema.TDS_BOOKINGDataTable tdsBooking;
            public ApplicationSchema.TDS_BOOKINGDataTable TDSBooking
            {
                get
                {
                    if (tdsBooking == null)
                    {
                        tdsBooking = new ApplicationSchema.TDS_BOOKINGDataTable();
                    }
                    return tdsBooking;
                }
            }

            private ApplicationSchema.TDS_DEDUCTION_DETAILDataTable tdsDeductionDetails;
            public ApplicationSchema.TDS_DEDUCTION_DETAILDataTable TDSDeductionDetails
            {
                get
                {
                    if (tdsDeductionDetails == null)
                    {
                        tdsDeductionDetails = new ApplicationSchema.TDS_DEDUCTION_DETAILDataTable();
                    }
                    return tdsDeductionDetails;
                }
            }

            private ApplicationSchema.TDS_DEDUCTIONDataTable tdsDeduction;
            public ApplicationSchema.TDS_DEDUCTIONDataTable TDSDeduction
            {
                get
                {
                    if (tdsDeduction == null)
                    {
                        tdsDeduction = new ApplicationSchema.TDS_DEDUCTIONDataTable();
                    }
                    return tdsDeduction;
                }
            }

            private ApplicationSchema.TDS_BOOKING_DETAILDataTable tdsBookingDetails;
            public ApplicationSchema.TDS_BOOKING_DETAILDataTable TDSBookingDetails
            {
                get
                {
                    if (tdsBookingDetails == null)
                    {
                        tdsBookingDetails = new ApplicationSchema.TDS_BOOKING_DETAILDataTable();
                    }
                    return tdsBookingDetails;
                }
            }

            private ApplicationSchema.TDS_PAYMENTDataTable tdsPayment;
            public ApplicationSchema.TDS_PAYMENTDataTable TDSPayment
            {
                get
                {
                    if (tdsPayment == null)
                    {
                        tdsPayment = new ApplicationSchema.TDS_PAYMENTDataTable();
                    }
                    return tdsPayment;
                }
            }

            private ApplicationSchema.TDS_PAYMENT_DETAILDataTable tdsPaymentDetail;
            public ApplicationSchema.TDS_PAYMENT_DETAILDataTable TDSPaymentDetail
            {
                get
                {
                    if (tdsPaymentDetail == null)
                    {
                        tdsPaymentDetail = new ApplicationSchema.TDS_PAYMENT_DETAILDataTable();
                    }
                    return tdsPaymentDetail;
                }
            }

            private ApplicationSchema.PARTY_PAYMENTDataTable tdsPartyPayment;
            public ApplicationSchema.PARTY_PAYMENTDataTable TDSPartyPayment
            {
                get
                {
                    if (tdsPartyPayment == null)
                    {
                        tdsPartyPayment = new ApplicationSchema.PARTY_PAYMENTDataTable();
                    }
                    return tdsPartyPayment;
                }
            }

            private ApplicationSchema.TDSCompanyDeductorsDataTable tdsCompanyDeductor;
            public ApplicationSchema.TDSCompanyDeductorsDataTable TdsCompanyDeductor
            {
                get
                {
                    if (tdsCompanyDeductor == null)
                    {
                        tdsCompanyDeductor = new ApplicationSchema.TDSCompanyDeductorsDataTable();
                    }
                    return tdsCompanyDeductor;
                }
            }

            private ApplicationSchema.PARTY_PAYMENT_DETAILDataTable tdsPartyPaymentDetail;
            public ApplicationSchema.PARTY_PAYMENT_DETAILDataTable TDSPartyPaymentDetail
            {
                get
                {
                    if (tdsPartyPaymentDetail == null)
                    {
                        tdsPartyPaymentDetail = new ApplicationSchema.PARTY_PAYMENT_DETAILDataTable();
                    }
                    return tdsPartyPaymentDetail;
                }
            }

            private ApplicationSchema.AuditTypeDataTable auditType;
            public ApplicationSchema.AuditTypeDataTable AuditLockType
            {
                get
                {
                    if (auditType == null)
                    {
                        auditType = new ApplicationSchema.AuditTypeDataTable();
                    }
                    return auditType;
                }
            }

            private ApplicationSchema.AuditTransTypeDataTable auditTransType;
            public ApplicationSchema.AuditTransTypeDataTable AuditLockTransType
            {
                get
                {
                    if (auditTransType == null)
                    {
                        auditTransType = new ApplicationSchema.AuditTransTypeDataTable();
                    }
                    return auditTransType;
                }
            }

            private ApplicationSchema.AssetServiceDataTable assetServiceDetails;
            public ApplicationSchema.AssetServiceDataTable ASSSETerviceDetails
            {
                get
                {
                    if (assetServiceDetails == null)
                    {
                        assetServiceDetails = new ApplicationSchema.AssetServiceDataTable();
                    }
                    return assetServiceDetails;
                }

            }

            private ApplicationSchema.AssetDepreciationDataTable assetDepreciationDetails;
            public ApplicationSchema.AssetDepreciationDataTable ASSETDepreciationDetails
            {
                get
                {
                    if (assetDepreciationDetails == null)
                    {
                        assetDepreciationDetails = new ApplicationSchema.AssetDepreciationDataTable();
                    }
                    return assetDepreciationDetails;
                }
            }


            private ApplicationSchema.AssetLocationDataTable assetLocationDetails;
            public ApplicationSchema.AssetLocationDataTable ASSETLocationDetails
            {
                get
                {
                    if (assetLocationDetails == null)
                    {
                        assetLocationDetails = new ApplicationSchema.AssetLocationDataTable();
                    }
                    return assetLocationDetails;
                }
            }

            private ApplicationSchema.AssetInsurancePlanDataTable assetInsurancePlan;
            public ApplicationSchema.AssetInsurancePlanDataTable InsurancePlan
            {
                get
                {
                    if (assetInsurancePlan == null)
                    {
                        assetInsurancePlan = new ApplicationSchema.AssetInsurancePlanDataTable();
                    }
                    return assetInsurancePlan;

                }
            }

            private ApplicationSchema.AssetClassDataTable assetClassDetails;
            public ApplicationSchema.AssetClassDataTable ASSETClassDetails
            {
                get
                {
                    if (assetClassDetails == null)
                    {
                        assetClassDetails = new ApplicationSchema.AssetClassDataTable();
                    }
                    return assetClassDetails;

                }
            }

            private ApplicationSchema.AssetUnitOfMeassureDataTable assetUnitOfMeassure;
            public ApplicationSchema.AssetUnitOfMeassureDataTable ASSETUnitOfMeassure
            {
                get
                {
                    if (assetUnitOfMeassure == null)
                    {
                        assetUnitOfMeassure = new ApplicationSchema.AssetUnitOfMeassureDataTable();
                    }
                    return assetUnitOfMeassure;

                }
            }

            private ApplicationSchema.STATISTICS_TYPEDataTable StatisticsTypeSchema = null;
            public ApplicationSchema.STATISTICS_TYPEDataTable StatisticsType
            {
                get
                {
                    if (StatisticsTypeSchema == null) { StatisticsTypeSchema = new ApplicationSchema.STATISTICS_TYPEDataTable(); }
                    return StatisticsTypeSchema;
                }
            }

            private ApplicationSchema.AssetItemDataTable assetitem;
            public ApplicationSchema.AssetItemDataTable ASSETItem
            {
                get
                {
                    if (assetitem == null)
                    {
                        assetitem = new ApplicationSchema.AssetItemDataTable();
                    }
                    return assetitem;

                }
            }

            private ApplicationSchema.insurance_renewal_masterDataTable insuranceMaster;
            public ApplicationSchema.insurance_renewal_masterDataTable InsuranceRenewalMaster
            {
                get
                {
                    if (insuranceMaster == null)
                    {
                        insuranceMaster = new ApplicationSchema.insurance_renewal_masterDataTable();
                    }
                    return insuranceMaster;

                }
            }

            private ApplicationSchema.insurance_renewal_detailDataTable insurancedetails;
            public ApplicationSchema.insurance_renewal_detailDataTable InsuranceRenewalDetails
            {
                get
                {
                    if (insurancedetails == null)
                    {
                        insurancedetails = new ApplicationSchema.insurance_renewal_detailDataTable();
                    }
                    return insurancedetails;

                }
            }

            private ApplicationSchema.Asset_CategoryDataTable AssetCateghory;
            public ApplicationSchema.Asset_CategoryDataTable ASSETCategory
            {
                get
                {
                    if (AssetCateghory == null)
                    {
                        AssetCateghory = new ApplicationSchema.Asset_CategoryDataTable();
                    }
                    return AssetCateghory;

                }
            }

            private ApplicationSchema.StockGroupDataTable stockgroup;
            public ApplicationSchema.StockGroupDataTable StockGroup
            {
                get
                {
                    if (stockgroup == null)
                    {
                        stockgroup = new ApplicationSchema.StockGroupDataTable();
                    }
                    return stockgroup;

                }
            }

            private ApplicationSchema.StockCategoryDataTable stockcategory;
            public ApplicationSchema.StockCategoryDataTable StockCategory
            {
                get
                {
                    if (stockcategory == null)
                    {
                        stockcategory = new ApplicationSchema.StockCategoryDataTable();
                    }
                    return stockcategory;

                }
            }

            private ApplicationSchema.StockItemDataTable stockitem;
            public ApplicationSchema.StockItemDataTable StockItem
            {
                get
                {
                    if (stockitem == null)
                    {
                        stockitem = new ApplicationSchema.StockItemDataTable();
                    }
                    return stockitem;

                }
            }

            private ApplicationSchema.StockLocationDataTable stocklocation;
            public ApplicationSchema.StockLocationDataTable StockLocation
            {
                get
                {
                    if (stocklocation == null)
                    {
                        stocklocation = new ApplicationSchema.StockLocationDataTable();
                    }
                    return stocklocation;

                }
            }

            private ApplicationSchema.StockUnitofMeasureDataTable stockunitofmeasure;
            public ApplicationSchema.StockUnitofMeasureDataTable StockUnitofMeasure
            {
                get
                {
                    if (stockunitofmeasure == null)
                    {
                        stockunitofmeasure = new ApplicationSchema.StockUnitofMeasureDataTable();
                    }
                    return stockunitofmeasure;

                }
            }

            private ApplicationSchema.AssetCustodiansDataTable assetcustodians;
            public ApplicationSchema.AssetCustodiansDataTable AssetCustodians
            {
                get
                {
                    if (assetcustodians == null)
                    {
                        assetcustodians = new ApplicationSchema.AssetCustodiansDataTable();
                    }
                    return assetcustodians;

                }
            }

            private ApplicationSchema.AssetVendorDataTable vendors;
            public ApplicationSchema.AssetVendorDataTable Vendors
            {
                get
                {
                    if (vendors == null)
                    {
                        vendors = new ApplicationSchema.AssetVendorDataTable();
                    }
                    return vendors;

                }
            }

            private ApplicationSchema.ManufactureDataTable manufactures;
            public ApplicationSchema.ManufactureDataTable Manufactures
            {
                get
                {
                    if (manufactures == null)
                    {
                        manufactures = new ApplicationSchema.ManufactureDataTable();
                    }
                    return manufactures;

                }
            }

            private ApplicationSchema.Insurance_DetailsDataTable insuranceDetails;
            public ApplicationSchema.Insurance_DetailsDataTable InsuranceData
            {
                get
                {
                    if (insuranceDetails == null)
                    {
                        insuranceDetails = new ApplicationSchema.Insurance_DetailsDataTable();
                    }
                    return insuranceDetails;
                }
            }

            private ApplicationSchema.AssetTransferVoucherDataTable assetTransferDetails;
            public ApplicationSchema.AssetTransferVoucherDataTable AssetTransferDetails
            {
                get
                {
                    if (assetTransferDetails == null)
                    {
                        assetTransferDetails = new ApplicationSchema.AssetTransferVoucherDataTable();
                    }
                    return assetTransferDetails;
                }
            }

            private ApplicationSchema.VoucherAMCMasterDataTable amcVoucherMaster;
            public ApplicationSchema.VoucherAMCMasterDataTable AMCMaster
            {
                get
                {
                    if (amcVoucherMaster == null)
                    {
                        amcVoucherMaster = new ApplicationSchema.VoucherAMCMasterDataTable();
                    }
                    return amcVoucherMaster;

                }
            }

            private ApplicationSchema.VoucherAMCDetailDataTable amcVoucherDetails;
            public ApplicationSchema.VoucherAMCDetailDataTable AMCDetails
            {
                get
                {
                    if (amcVoucherDetails == null)
                    {
                        amcVoucherDetails = new ApplicationSchema.VoucherAMCDetailDataTable();
                    }
                    return amcVoucherDetails;

                }
            }
            private ApplicationSchema.VoucherSalesAssetDataTable salesAsset;
            public ApplicationSchema.VoucherSalesAssetDataTable SalesAsset
            {
                get
                {
                    if (salesAsset == null)
                    {
                        salesAsset = new ApplicationSchema.VoucherSalesAssetDataTable();
                    }
                    return salesAsset;

                }
            }
            private ApplicationSchema.AssetPurchaseMasterDataTable purchasemaster;
            public ApplicationSchema.AssetPurchaseMasterDataTable AssetPurchaseMaster
            {
                get
                {
                    if (purchasemaster == null)
                    {
                        purchasemaster = new ApplicationSchema.AssetPurchaseMasterDataTable();
                    }
                    return purchasemaster;
                }
            }
            private ApplicationSchema.AssetPurchaseDetailDataTable purchasedetail;
            public ApplicationSchema.AssetPurchaseDetailDataTable AssetPurchaseDetail
            {
                get
                {
                    if (purchasedetail == null)
                    {
                        purchasedetail = new ApplicationSchema.AssetPurchaseDetailDataTable();
                    }
                    return purchasedetail;
                }
            }
            private ApplicationSchema.Insurance_Master_DataDataTable insuranceMasterDeta;
            public ApplicationSchema.Insurance_Master_DataDataTable InsuranceMasterData
            {
                get
                {
                    if (insuranceMasterDeta == null)
                    {
                        insuranceMasterDeta = new ApplicationSchema.Insurance_Master_DataDataTable();
                    }
                    return insuranceMasterDeta;
                }
            }
            private ApplicationSchema.DepriciationVoucherMasterDataTable depVoucherData;
            public ApplicationSchema.DepriciationVoucherMasterDataTable DepriciationVoucherMaster
            {
                get
                {
                    if (depVoucherData == null)
                    {
                        depVoucherData = new ApplicationSchema.DepriciationVoucherMasterDataTable();
                    }
                    return depVoucherData;
                }
            }

            private ApplicationSchema.DepreciationVoucherDetailDataTable depVoucherdetails;
            public ApplicationSchema.DepreciationVoucherDetailDataTable DepriciationVoucherDetail
            {
                get
                {
                    if (depVoucherdetails == null)
                    {
                        depVoucherdetails = new ApplicationSchema.DepreciationVoucherDetailDataTable();
                    }
                    return depVoucherdetails;
                }
            }

            private ApplicationSchema.STOCK_MASTER_PURCHASEDataTable stockMasterPurchase;
            public ApplicationSchema.STOCK_MASTER_PURCHASEDataTable StockMasterPurchase
            {
                get
                {
                    if (stockMasterPurchase == null)
                    {
                        stockMasterPurchase = new ApplicationSchema.STOCK_MASTER_PURCHASEDataTable();
                    }
                    return stockMasterPurchase;
                }
            }

            private ApplicationSchema.STOCK_PURCHAE_DETAILDataTable stockPurchaseDetails;
            public ApplicationSchema.STOCK_PURCHAE_DETAILDataTable StockPurchaseDetails
            {
                get
                {
                    if (stockPurchaseDetails == null)
                    {
                        stockPurchaseDetails = new ApplicationSchema.STOCK_PURCHAE_DETAILDataTable();
                    }
                    return stockPurchaseDetails;
                }
            }

            private ApplicationSchema.STOCK_SALES_DETAILSDataTable stockSalesDetails;
            public ApplicationSchema.STOCK_SALES_DETAILSDataTable StockSalesDetails
            {
                get
                {
                    if (stockSalesDetails == null)
                    {
                        stockSalesDetails = new ApplicationSchema.STOCK_SALES_DETAILSDataTable();
                    }
                    return stockSalesDetails;
                }
            }
            private ApplicationSchema.STOCK_MASTER_SALESDataTable stockMasterSales;
            public ApplicationSchema.STOCK_MASTER_SALESDataTable StockMasterSales
            {
                get
                {
                    if (stockMasterSales == null)
                    {
                        stockMasterSales = new ApplicationSchema.STOCK_MASTER_SALESDataTable();
                    }
                    return stockMasterSales;
                }
            }


            private ApplicationSchema.STOCK_PURCHASE_SALESDataTable stockPurchasesalesDetails;
            public ApplicationSchema.STOCK_PURCHASE_SALESDataTable StockPurchaseSalesDetails
            {
                get
                {
                    if (stockPurchasesalesDetails == null)
                    {
                        stockPurchasesalesDetails = new ApplicationSchema.STOCK_PURCHASE_SALESDataTable();
                    }
                    return stockPurchasesalesDetails;
                }
            }

            private ApplicationSchema.STOCK_PURCHASE_RETURNS_DETAILSDataTable stockPurchaseReturnsDetails;
            public ApplicationSchema.STOCK_PURCHASE_RETURNS_DETAILSDataTable StockPurchaseReturnsDetails
            {
                get
                {
                    if (stockPurchaseReturnsDetails == null)
                    {
                        stockPurchaseReturnsDetails = new ApplicationSchema.STOCK_PURCHASE_RETURNS_DETAILSDataTable();
                    }
                    return stockPurchaseReturnsDetails;
                }
            }

            private ApplicationSchema.STOCK_MASTER_PURCHASE_RETURNSDataTable stockmasterPurchaseReturns;
            public ApplicationSchema.STOCK_MASTER_PURCHASE_RETURNSDataTable StockMasterPurchaseReturns
            {
                get
                {
                    if (stockmasterPurchaseReturns == null)
                    {
                        stockmasterPurchaseReturns = new ApplicationSchema.STOCK_MASTER_PURCHASE_RETURNSDataTable();
                    }
                    return stockmasterPurchaseReturns;
                }
            }

            private ApplicationSchema.STOCK_ITEM_TRANSFERDataTable stockItemTransfer;
            public ApplicationSchema.STOCK_ITEM_TRANSFERDataTable StockItemTransfer
            {
                get
                {
                    if (stockItemTransfer == null)
                    {
                        stockItemTransfer = new ApplicationSchema.STOCK_ITEM_TRANSFERDataTable();
                    }
                    return stockItemTransfer;
                }
            }
            private ApplicationSchema.STOCK_BALANCEDataTable stockBalance;
            public ApplicationSchema.STOCK_BALANCEDataTable StockBalance
            {
                get
                {
                    if (stockBalance == null)
                    {
                        stockBalance = new ApplicationSchema.STOCK_BALANCEDataTable();
                    }
                    return stockBalance;
                }
            }

            private ApplicationSchema.DepreciationMasterDataTable depmaster;
            public ApplicationSchema.DepreciationMasterDataTable DepreciationMaster
            {
                get
                {
                    if (depmaster == null)
                    {
                        depmaster = new ApplicationSchema.DepreciationMasterDataTable();
                    }
                    return depmaster;
                }
            }

            private ApplicationSchema.DepreciationDetailDataTable depDetail;
            public ApplicationSchema.DepreciationDetailDataTable DepreciationDetail
            {
                get
                {
                    if (depDetail == null)
                    {
                        depDetail = new ApplicationSchema.DepreciationDetailDataTable();
                    }
                    return depDetail;
                }
            }


            private ApplicationSchema.PortalMessageDataTable portal;
            public ApplicationSchema.PortalMessageDataTable PortalMessage
            {
                get
                {
                    if (portal == null)
                    {
                        portal = new ApplicationSchema.PortalMessageDataTable();
                    }
                    return portal;
                }
            }
            private ApplicationSchema.BroadcastMessageDataTable broadcastmessage;
            public ApplicationSchema.BroadcastMessageDataTable BroadCastMessage
            {
                get
                {
                    if (broadcastmessage == null)
                    {
                        broadcastmessage = new ApplicationSchema.BroadcastMessageDataTable();
                    }
                    return broadcastmessage;
                }
            }
            private ApplicationSchema.TROUBLE_TICKETDataTable troubleticket;
            public ApplicationSchema.TROUBLE_TICKETDataTable TroubleTicket
            {
                get
                {
                    if (troubleticket == null)
                    {
                        troubleticket = new ApplicationSchema.TROUBLE_TICKETDataTable();
                    }
                    return troubleticket;
                }
            }

            private ApplicationSchema.BlockDataTable block;
            public ApplicationSchema.BlockDataTable Block
            {
                get
                {
                    if (block == null)
                    {
                        block = new ApplicationSchema.BlockDataTable();
                    }
                    return block;
                }
            }

            private ApplicationSchema.AssetInsuranceDetailDataTable details;
            public ApplicationSchema.AssetInsuranceDetailDataTable AssetInsuranceDetail
            {
                get
                {
                    if (details == null)
                    {
                        details = new ApplicationSchema.AssetInsuranceDetailDataTable();
                    }
                    return details;
                }
            }

            private ApplicationSchema.AssetInOutDataTable assetInOut;
            public ApplicationSchema.AssetInOutDataTable AssetInOut
            {
                get
                {
                    if (assetInOut == null)
                    {
                        assetInOut = new ApplicationSchema.AssetInOutDataTable();
                    }
                    return assetInOut;
                }
            }

            private ApplicationSchema.ASSET_AMC_RENEWAL_MASTERDataTable amcRenewalMaster;
            public ApplicationSchema.ASSET_AMC_RENEWAL_MASTERDataTable AmcRenewalMaster
            {
                get
                {
                    if (amcRenewalMaster == null)
                    {
                        amcRenewalMaster = new ApplicationSchema.ASSET_AMC_RENEWAL_MASTERDataTable();
                    }
                    return amcRenewalMaster;
                }
            }

            private ApplicationSchema.ASSET_AMC_RENEWAL_HISTORYDataTable amcrenewalHistory;
            public ApplicationSchema.ASSET_AMC_RENEWAL_HISTORYDataTable AMCRenewalHistory
            {
                get
                {
                    if (amcrenewalHistory == null)
                    {
                        amcrenewalHistory = new ApplicationSchema.ASSET_AMC_RENEWAL_HISTORYDataTable();
                    }
                    return amcrenewalHistory;
                }
            }

            private ApplicationSchema.AMC_ITEM_MAPPINGDataTable amcitemMapping;
            public ApplicationSchema.AMC_ITEM_MAPPINGDataTable AMCItemMapping
            {
                get
                {
                    if (amcitemMapping == null)
                    {
                        amcitemMapping = new ApplicationSchema.AMC_ITEM_MAPPINGDataTable();
                    }
                    return amcitemMapping;
                }
            }
            private ApplicationSchema.MASTER_DONAUD_PROSPECTSDataTable donorProspects;
            public ApplicationSchema.MASTER_DONAUD_PROSPECTSDataTable DonorProspects
            {
                get
                {
                    if (donorProspects == null)
                    {
                        donorProspects = new ApplicationSchema.MASTER_DONAUD_PROSPECTSDataTable();
                    }
                    return donorProspects;
                }
            }

            private ApplicationSchema.MASTER_DONAUD_INS_TYPEDataTable donorInstitutionalType;
            public ApplicationSchema.MASTER_DONAUD_INS_TYPEDataTable DonorInstitutionalType
            {
                get
                {
                    if (donorInstitutionalType == null)
                    {
                        donorInstitutionalType = new ApplicationSchema.MASTER_DONAUD_INS_TYPEDataTable();
                    }
                    return donorInstitutionalType;
                }
            }
            private ApplicationSchema.MASTER_DONAUD_PAY_MODEDataTable donorPaymentMode;
            public ApplicationSchema.MASTER_DONAUD_PAY_MODEDataTable DonorPaymentMode
            {
                get
                {
                    if (donorPaymentMode == null)
                    {
                        donorPaymentMode = new ApplicationSchema.MASTER_DONAUD_PAY_MODEDataTable();
                    }
                    return donorPaymentMode;
                }
            }

            private ApplicationSchema.MASTER_DONAUD_REG_TYPEDataTable donorRegistrationType;
            public ApplicationSchema.MASTER_DONAUD_REG_TYPEDataTable DonorRegistrationType
            {
                get
                {
                    if (donorRegistrationType == null)
                    {
                        donorRegistrationType = new ApplicationSchema.MASTER_DONAUD_REG_TYPEDataTable();
                    }
                    return donorRegistrationType;
                }
            }
            private ApplicationSchema.MASTER_DONOR_TEMPLATE_TYPEDataTable donormaitTemplateType;
            public ApplicationSchema.MASTER_DONOR_TEMPLATE_TYPEDataTable DonorMailTemplateType
            {
                get
                {
                    if (donormaitTemplateType == null)
                    {
                        donormaitTemplateType = new ApplicationSchema.MASTER_DONOR_TEMPLATE_TYPEDataTable();
                    }
                    return donormaitTemplateType;
                }
            }

            private ApplicationSchema.MASTER_DONOR_TAGSDataTable donortags;
            public ApplicationSchema.MASTER_DONOR_TAGSDataTable DonorTags
            {
                get
                {
                    if (donortags == null)
                    {
                        donortags = new ApplicationSchema.MASTER_DONOR_TAGSDataTable();
                    }
                    return donortags;
                }
            }
            private ApplicationSchema.MASTER_DONOR_MAILING_HISTORYDataTable donormailhistory;
            public ApplicationSchema.MASTER_DONOR_MAILING_HISTORYDataTable Donormailhistory
            {
                get
                {
                    if (donormailhistory == null)
                    {
                        donormailhistory = new ApplicationSchema.MASTER_DONOR_MAILING_HISTORYDataTable();
                    }
                    return donormailhistory;
                }
            }

            private ApplicationSchema.TEMPLATE_FIELDS_MEMBERDataTable memberFields;
            public ApplicationSchema.TEMPLATE_FIELDS_MEMBERDataTable MemberFields
            {
                get
                {
                    if (memberFields == null)
                    {
                        memberFields = new ApplicationSchema.TEMPLATE_FIELDS_MEMBERDataTable();
                    }
                    return memberFields;
                }
            }

            private ApplicationSchema.TEMPLATE_FIELDS_THANKSGIVINGDataTable thanksgivingFields;
            public ApplicationSchema.TEMPLATE_FIELDS_THANKSGIVINGDataTable ThanksgivingFields
            {
                get
                {
                    if (thanksgivingFields == null)
                    {
                        thanksgivingFields = new ApplicationSchema.TEMPLATE_FIELDS_THANKSGIVINGDataTable();
                    }
                    return thanksgivingFields;
                }
            }
            private ApplicationSchema.MASTER_DONOR_REFERENCEDataTable donorReference;
            public ApplicationSchema.MASTER_DONOR_REFERENCEDataTable DonorReference
            {
                get
                {
                    if (donorReference == null)
                    {
                        donorReference = new ApplicationSchema.MASTER_DONOR_REFERENCEDataTable();
                    }
                    return donorReference;
                }
            }

            private ApplicationSchema.DONORTITLEDataTable donorTitle;
            public ApplicationSchema.DONORTITLEDataTable DonorTitle
            {
                get
                {
                    if (donorTitle == null)
                    {
                        donorTitle = new ApplicationSchema.DONORTITLEDataTable();
                    }
                    return donorTitle;
                }
            }

            private ApplicationSchema.DenominationDataTable denomination;
            public ApplicationSchema.DenominationDataTable Denomination
            {
                get
                {
                    if (denomination == null)
                    {
                        denomination = new ApplicationSchema.DenominationDataTable();
                    }
                    return denomination;
                }
            }

            private ApplicationSchema.REPORT_CUSTOMIZATIONDataTable reportcustomization;
            public ApplicationSchema.REPORT_CUSTOMIZATIONDataTable ReportCustomization
            {
                get
                {
                    if (reportcustomization == null)
                    {
                        reportcustomization = new ApplicationSchema.REPORT_CUSTOMIZATIONDataTable();
                    }
                    return reportcustomization;
                }
            }
            private ApplicationSchema.MasterGSTClassDataTable gstclass;
            public ApplicationSchema.MasterGSTClassDataTable MasterGSTClass
            {
                get
                {
                    if (gstclass == null)
                    {
                        gstclass = new ApplicationSchema.MasterGSTClassDataTable();
                    }
                    return gstclass;
                }
            }

            private ApplicationSchema.GST_MASTER_INVOICEDataTable gstinvoicemaster;
            public ApplicationSchema.GST_MASTER_INVOICEDataTable GSTInvoiceMaster
            {
                get
                {
                    if (gstinvoicemaster == null)
                    {
                        gstinvoicemaster = new ApplicationSchema.GST_MASTER_INVOICEDataTable();
                    }
                    return gstinvoicemaster;
                }
            }

            private ApplicationSchema.GST_MASTER_INVOICE_LEDGERDataTable gstinvoicemasterledgerdetails;
            public ApplicationSchema.GST_MASTER_INVOICE_LEDGERDataTable GSTInvoiceMasterLedgerDetails
            {
                get
                {
                    if (gstinvoicemasterledgerdetails == null)
                    {
                        gstinvoicemasterledgerdetails = new ApplicationSchema.GST_MASTER_INVOICE_LEDGERDataTable();
                    }
                    return gstinvoicemasterledgerdetails;
                }
            }

            private ApplicationSchema.CHEQUE_PRINTINGDataTable chequeprinting;
            public ApplicationSchema.CHEQUE_PRINTINGDataTable ChequePrinting
            {
                get
                {
                    if (chequeprinting == null)
                    {
                        chequeprinting = new ApplicationSchema.CHEQUE_PRINTINGDataTable();
                    }
                    return chequeprinting;
                }
            }

            private ApplicationSchema.VOUCHER_REFERENCEDataTable VoucherRef;
            public ApplicationSchema.VOUCHER_REFERENCEDataTable VoucherReference
            {
                get
                {
                    if (VoucherRef == null)
                    {
                        VoucherRef = new ApplicationSchema.VOUCHER_REFERENCEDataTable();
                    }
                    return VoucherRef;
                }
            }

            private ApplicationSchema.ReportSignDataTable ReportSignSchema = null;
            public ApplicationSchema.ReportSignDataTable ReportSign
            {
                get
                {
                    if (ReportSignSchema == null) { ReportSignSchema = new ApplicationSchema.ReportSignDataTable(); }
                    return ReportSignSchema;
                }
            }

            private ApplicationSchema.ReportBudgetNewProjectDataTable ReportNewBudgetProjectSchema = null;
            public ApplicationSchema.ReportBudgetNewProjectDataTable ReportNewBudgetProject
            {
                get
                {
                    if (ReportNewBudgetProjectSchema == null) { ReportNewBudgetProjectSchema = new ApplicationSchema.ReportBudgetNewProjectDataTable(); }
                    return ReportNewBudgetProjectSchema;
                }
            }

            private ApplicationSchema.BudgetGroupDataTable budgetgroupschema = null;
            public ApplicationSchema.BudgetGroupDataTable BudgetGroup
            {
                get
                {
                    if (budgetgroupschema == null) { budgetgroupschema = new ApplicationSchema.BudgetGroupDataTable(); }
                    return budgetgroupschema;
                }
            }

            private ApplicationSchema.BudgetSubGroupDataTable budgetsubgroupschema = null;
            public ApplicationSchema.BudgetSubGroupDataTable BudgetSubGroup
            {
                get
                {
                    if (budgetsubgroupschema == null) { budgetsubgroupschema = new ApplicationSchema.BudgetSubGroupDataTable(); }
                    return budgetsubgroupschema;
                }
            }


            private ApplicationSchema.GENERALATE_REPORTSDataTable generlatereport = null;
            public ApplicationSchema.GENERALATE_REPORTSDataTable GenerlateReport
            {
                get
                {
                    if (generlatereport == null) { generlatereport = new ApplicationSchema.GENERALATE_REPORTSDataTable(); }
                    return generlatereport;
                }
            }

            private ApplicationSchema.PROJECT_IMPORT_EXPORTDataTable projectimportexport = null;
            public ApplicationSchema.PROJECT_IMPORT_EXPORTDataTable ProjectImportExport
            {
                get
                {
                    if (projectimportexport == null) { projectimportexport = new ApplicationSchema.PROJECT_IMPORT_EXPORTDataTable(); }
                    return projectimportexport;
                }
            }

            private ApplicationSchema.BRANCH_LC_ENABLE_TRACK_MODULESDataTable lcbranchenabletrackmodules;
            public ApplicationSchema.BRANCH_LC_ENABLE_TRACK_MODULESDataTable LcBranchEnableTrackModules
            {
                get
                {
                    if (lcbranchenabletrackmodules == null)
                    {
                        lcbranchenabletrackmodules = new ApplicationSchema.BRANCH_LC_ENABLE_TRACK_MODULESDataTable();
                    }
                    return lcbranchenabletrackmodules;
                }
            }

            private ApplicationSchema.GeneralateGroupLedgerDataTable generalategroupledgers;
            public ApplicationSchema.GeneralateGroupLedgerDataTable GeneralateGroupLedger
            {
                get
                {
                    if (generalategroupledgers == null)
                    {
                        generalategroupledgers = new ApplicationSchema.GeneralateGroupLedgerDataTable();
                    }
                    return generalategroupledgers;
                }
            }

            private ApplicationSchema.LockInVoucherEntryDataTable lockvoucherentry;
            public ApplicationSchema.LockInVoucherEntryDataTable LockVoucherEntry
            {
                get
                {
                    if (lockvoucherentry == null)
                    {
                        lockvoucherentry = new ApplicationSchema.LockInVoucherEntryDataTable();
                    }
                    return lockvoucherentry;
                }
            }

            private ApplicationSchema.BudgetCostCentreDataTable budgetcostcentre;
            public ApplicationSchema.BudgetCostCentreDataTable BudgetCostCentre
            {
                get
                {
                    if (budgetcostcentre == null)
                    {
                        budgetcostcentre = new ApplicationSchema.BudgetCostCentreDataTable();
                    }
                    return budgetcostcentre;
                }
            }

            private ApplicationSchema.AuditorNoteSignDetailsDataTable auditornotesign;
            public ApplicationSchema.AuditorNoteSignDetailsDataTable AuditorNoteSign
            {
                get
                {
                    if (auditornotesign == null)
                    {
                        auditornotesign = new ApplicationSchema.AuditorNoteSignDetailsDataTable();
                    }
                    return auditornotesign;
                }
            }

            private ApplicationSchema.FDInvestmentTypeDataTable fdInvestmentTypeShema = null;
            public ApplicationSchema.FDInvestmentTypeDataTable FDInvestmentType
            {
                get
                {
                    if (fdInvestmentTypeShema == null) { fdInvestmentTypeShema = new ApplicationSchema.FDInvestmentTypeDataTable(); }
                    return fdInvestmentTypeShema;
                }
            }

            private ApplicationSchema.USERMANUAL_FEATURESDataTable usermanualfeature = null;
            public ApplicationSchema.USERMANUAL_FEATURESDataTable UsermanualFeature
            {
                get
                {
                    if (usermanualfeature == null) { usermanualfeature = new ApplicationSchema.USERMANUAL_FEATURESDataTable(); }
                    return usermanualfeature;
                }
            }

            private ApplicationSchema.BranchVoucherGraceDaysDataTable branchvoucerhgracedays;
            public ApplicationSchema.BranchVoucherGraceDaysDataTable BranchVoucherGraceDays
            {
                get
                {
                    if (branchvoucerhgracedays == null)
                    {
                        branchvoucerhgracedays = new ApplicationSchema.BranchVoucherGraceDaysDataTable();
                    }
                    return branchvoucerhgracedays;
                }
            }
        }
    }
}

