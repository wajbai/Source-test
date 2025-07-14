using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.Utility.ConfigSetting;
using System.Data;
using Bosco.Model.UIModel;
using Bosco.Model.Business;
using AcMEDSync.Model;
using Bosco.Model.TDS;
using Bosco.Model.Dsync;
using Bosco.Model.UIModel.Master;

namespace Bosco.Model.Transaction
{
    public class VoucherTransactionSystem : SystemBase
    {
        #region Constructor
        public VoucherTransactionSystem()
        {


        }
        public VoucherTransactionSystem(int VoucherID)
        {
            FillVoucherDetails(VoucherID);
        }

        #endregion

        #region Decelaration
        ResultArgs resultArgs = new ResultArgs();
        bool isEditMode = false;
        public DataTable dtTransInfo = null;
        private DataSet dsCostCentre = new DataSet();
        NumberFormat numberFormatType;
        public int VoucherDefinitionId { get; set; }
        DefaultVoucherTypes vTransType;
        public TDSTransType tdsTransType;
        public string MoveTransactionType { get; set; }
        int VoucherMethodType = 0;
        #endregion

        #region Properties
        #region Voucher Master Properties
        public int VoucherId { get; set; }
        public int[] VoucherIdList { get; set; }
        public int FDVoucherId { get; set; }  //FD Realization
        public int FDInterestVoucherId { get; set; }
        public string FDWithdrwalReceiptVoucherNo { get; set; }

        public DateTime VoucherDate { get; set; }

        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int TransVoucherMethod { get; set; }
        public string VoucherNo { get; set; }
        public string PreviousRunningDigit { get; set; }
        public string PreviousVoucherNo { get; set; }
        public string VoucherType { get; set; }
        public int CashBankEntry { get; set; }
        public int DonorId { get; set; }
        public int PurposeId { get; set; }
        public string ContributionType { get; set; }
        public decimal ContributionAmount { get; set; }
        public int CurrencyCountryId { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal CalculatedAmount { get; set; }
        public decimal ActualAmount { get; set; }
        public int ExchageCountryId { get; set; }
        public string Narration { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public int FDGroupId { get; set; }
        //public int FDYear { get; set; }
        //public int FDMth { get; set; }
        //public int FDDay { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedByName { get; set; }
        public string VoucherSubType { get; set; }
        public string FDTransType { get; set; }
        public string NameAddress { get; set; }
        public string PanNumber { get; set; }
        public string GSTNumber { get; set; }
        public string ClientReferenceId { get; set; }
        public string ClientCode { get; set; }
        public string ClientMode { get; set; }
        public string ClientRefBankId { get; set; }

        private List<string> TDSBooking = new List<string>();
        private List<string> TDSMasters = new List<string>();
        public DateTime dteWithdrawalReceiptDate { get; set; }

        //26/04/2019, for Vendor GST Invoice details
        public Int32 GST_INVOICE_ID { get; set; }
        public string GST_VENDOR_INVOICE_NO { get; set; }
        public string GST_VENDOR_INVOICE_DATE { get; set; }
        public Int32 GST_VENDOR_INVOICE_TYPE { get; set; }
        public Int32 GST_VENDOR_ID { get; set; }
        public DataTable dtGSTInvoiceMasterDetails { get; set; }
        public DataTable dtGSTInvoiceMasterLedgerDetails { get; set; }

        public bool IsMultiNarrationEnabled { get; set; }
        public string MultiNarration { get; set; }

        public DataTable dtPostVoucherDetails { get; set; }

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

        //On 01/09/2023, To assign booked gst invoices id
        public Int32 BookingGSTInvoiceId { get; set; }

        // Budget Properties

        public string BudgetDateFrom { get; set; }
        public string BudgetDateTo { get; set; }

        #endregion

        #region Common Properties
        public int TransVoucherType { get; set; }
        public int GroupId { get; set; }
        public DateTime BalanceDate { get; set; }
        public int VoucherApplicableMonth { get; set; }
        public int VoucherDuration { get; set; }
        public int ResetMonth { get; set; }
        public bool isInsertVoucher = false;
        public bool EditVoucherTypeChanged = false;
        public int EditVoucherIndex = 0;
        public DateTime EditVoucherDate { get; set; }
        public DefaultVoucherTypes Vtype;
        public int BudgetTypeId { get; set; }
        public int BudgetMonthDistribution { get; set; }
        #endregion

        #region TDS Properties
        public DataSet dsTDSBooking { get; set; }
        public DataSet dsTDSDeductionLater { get; set; }
        public DataTable dtTDSBooking { get; set; }
        public DataTable dtTDSDeductionLater { get; set; }
        public DataView dvTdsUcTransSummary { get; set; }
        public int TDSPaymentId { get; set; }
        public int TDSCashBankId { get; set; }
        public int PartyLedgerId { get; set; }
        public int DeductionId { get; set; }
        public int DeducteeTypeId { get; set; }
        public int ExpenseLedgerId { get; set; }
        public string TDSPartyNarration { get; set; }
        #endregion

        #region TDS Booking  Properties
        public int TDSBookingId { get; set; }
        public int TDSBookingVoucherId { get; set; }
        public Dictionary<int, double> TDSBookingDic = new Dictionary<int, double>();
        public DateTime dteTDSBookingDate { get; set; }
        #endregion

        #region TDS Party Payment Properties
        public int TDSPartyPaymentId { get; set; }
        public int TDSPartyPaymentCashBankId { get; set; }
        #endregion

        #region Voucher Transaction Properties
        EventArgs EventArg;
        public int SequenceNo { get; set; }
        public int LedgerId { get; set; }
        public decimal Amount { get; set; }
        public string TransMode { get; set; }
        public string CashTransMode { get; set; }
        public string LedgerFlag { get; set; }
        public string ChequeNo { get; set; }
        public string MaterializedOn { get; set; }
        public int IdentityFlag { get; set; }
        public decimal GSt { get; set; }
        public decimal CGSt { get; set; }
        public decimal SGSt { get; set; }
        public decimal IGSt { get; set; }
        public Nullable<int> LedgerGSTClassID { get; set; }
        public string ChequeRefDate { get; set; }
        public string ChequeRefBankName { get; set; }
        public string ChequeRefBankBranch { get; set; }
        public string ChequeRefFundTransfer { get; set; }
        public string ReferenceNo { get; set; }
        public int SubLedgerSequenceNo { get; set; }

        public decimal LedgerLiveExchangeRate { get; set; }
        public decimal LedgerExchangeRate { get; set; }
        public decimal LedgerActualAmount { get; set; }

        #endregion

        #region Voucher CostCentre Properties
        public int CostCenterId { get; set; }
        public string CostCenterTable { get; set; }
        public decimal CostCentreAmount { get; set; }
        public int CostCentreSequenceNo { get; set; }

        #endregion

        #region Reference Properties
        public int REC_PAY_VOUCHER_ID { get; set; }
        public int RefLEDGER_ID { get; set; }
        public decimal RefAMOUNT { get; set; }
        public int REF_VOUCHER_ID { get; set; }

        #endregion

        #region Denomination Properties
        public int DenominationLedgerID { get; set; }
        public int DenominationSequnce { get; set; }
        public int Denomination { get; set; }
        private int DenominationID { get; set; }
        public int Count { get; set; }
        public decimal DenominationAmount { get; set; }
        public DataTable dtDenomination { get; set; }
        public string DenominationTable { get; set; }

        public int CashBankModeId { get; set; }
        #endregion

        #region MultipleTransaction

        public DataTable dtBulkTransactions = new DataTable();

        #endregion

        /// <summary>
        /// On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
        /// 
        /// This date is apart from balance date. If we take reports for given period (DATE_FROM and DATE_TO)
        /// When we show closing balance for DATE_TO, we have to check bank ledger closed date for DATE_FROM
        /// </summary>
        public string BankClosedDate { get; set; }

        #endregion

        #region Voucher FD Properties
        public string FDAccountNo { get; set; }
        public int FDLedgerId { get; set; }
        public string FDInterestId { get; set; }
        public bool InterestType { get; set; }
        public string FDType { get; set; }
        #endregion

        #region Voucher Audit Properties
        //01/09/2021, For Auditor Log Properties  --------------------------------------
        public DateTime PrevAuditVoucherDate { get; set; }
        public int PrevAuditProjectId { get; set; }
        public string PrevAuditProjectName { get; set; }
        public string PrevAuditVoucherNo { get; set; }
        public string PrevAuditVoucherType { get; set; }
        public string PrevAuditVoucherSubType { get; set; }
        public decimal PrevAuditAmount { get; set; }
        public int PrevAuditModifiedBy { get; set; }
        public string PrevAuditModifiedByName { get; set; }
        public bool IsVoucherModifiedByAuditor { get; set; }

        private bool IsEnableVoucherChangesHistory { get; set; }

        DateTime LockFromDate = DateTime.MinValue;
        DateTime LockToDate = DateTime.MinValue;
        string LockProjectName = string.Empty;
        //-----------------------------------------------------------------------------
        #endregion

        #region Voucher Iamge Details
        public DataTable dtVoucherFiles = new DataTable("VoucherFiles");
        # endregion

        #region Voucher Master Methods

        private ResultArgs FetchVoucherMasterDetails()
        {
            using (DataManager dataManger = new DataManager(SQLCommand.VoucherMaster.FetchAll))
            {
                resultArgs = dataManger.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs FetchMaterDetailsById(int VoucherID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchMasterByID))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherID);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs MadeTransaction(string LedgerIDCollection)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.IsTransactionMadeForProject))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.IDsColumn, LedgerIDCollection);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs MadeTransactionForBudget(string LedgerIDCollection)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.IsMadeBudgetForLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.IDsColumn, LedgerIDCollection);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs MadeTransactionDonor(string DonorIdCollection)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.IsTransactionMadeForDonor))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.IDsColumn, DonorIdCollection);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs MadeTransactionForLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.IsTransMadeSigleLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs MappedLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.IsLedgerMapped))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteDonorVouchers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.DeleteDonorVouchers))
            {
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteVouchersforImport(string DateFrom, string DateTo, string projectIds)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.DeleteVoucherImport))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, DateTo);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs MadeTransactionForLedger(string ProjectIDCollection)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.IsTransactionMadeForLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDsColumn, ProjectIDCollection);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs SaveVoucherMasterDetails(DataManager dm)
        {
            try
            {
                //On 01/09/2021, To assign Audit log details
                AssignVoucherAuditDetails();

                //for temp purpose
                resultArgs = CheckProperVoucherTypeDefintion(VoucherDefinitionId, VoucherType);
                if (resultArgs.Success)
                {
                    //On 19/08/2024, To Set Currency based voucher entry ---------------------------------------------------------------
                    if (!(CurrencyCountryId > 0 && ContributionAmount > 0 && ExchangeRate > 0 && ActualAmount > 0))
                    {
                        CurrencyCountryId = 0;
                        ContributionAmount = 0;
                        ExchangeRate = 1;
                        ActualAmount = 0;
                    }
                    //------------------------------------------------------------------------------------------------------------------

                    using (DataManager dataManager = new DataManager((VoucherId == 0) ? SQLCommand.VoucherMaster.Add : SQLCommand.VoucherMaster.Update))
                    {
                        dataManager.Database = dm.Database;
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId, true);
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_DATEColumn, VoucherDate);
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectId);
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_NOColumn, VoucherNo);
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_TYPEColumn, VoucherType);
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_SUB_TYPEColumn, VoucherSubType);
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.DONOR_IDColumn, DonorId);
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PURPOSE_IDColumn, PurposeId);
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CONTRIBUTION_TYPEColumn, (string.IsNullOrEmpty(ContributionType) ? "N" : ContributionType));
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CONTRIBUTION_AMOUNTColumn, ContributionAmount);
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CURRENCY_COUNTRY_IDColumn, CurrencyCountryId);
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.EXCHANGE_RATEColumn, ExchangeRate);
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CALCULATED_AMOUNTColumn, CalculatedAmount);
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.ACTUAL_AMOUNTColumn, ActualAmount);
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.EXCHANGE_COUNTRY_IDColumn, ExchageCountryId);
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.NARRATIONColumn, Narration);
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.STATUSColumn, Status);
                        //dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CREATED_ONColumn, CreatedOn);
                        //dataManager.Parameters.Add(this.AppSchema.VoucherMaster.MODIFIED_ONColumn, ModifiedOn);
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CREATED_BYColumn, CreatedBy);
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.MODIFIED_BYColumn, ModifiedBy);
                        //On 19/08/2021
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CREATED_BY_NAMEColumn, CreatedByName);
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.MODIFIED_BY_NAMEColumn, ModifiedByName);
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.NAME_ADDRESSColumn, NameAddress);

                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PAN_NUMBERColumn, PanNumber);
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.GST_NUMBERColumn, GSTNumber);

                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CLIENT_CODEColumn, ClientCode);
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CLIENT_REFERENCE_IDColumn, ClientReferenceId);
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CLIENT_MODEColumn, ClientMode);
                        dataManager.Parameters.Add(this.AppSchema.VoucherNumber.VOUCHER_DEFINITION_IDColumn, VoucherDefinitionId);

                        //26/04/2019, for Vendor GST Invoice details
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.GST_VENDOR_INVOICE_NOColumn, GST_VENDOR_INVOICE_NO);
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.GST_VENDOR_INVOICE_DATEColumn, GST_VENDOR_INVOICE_DATE);
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.GST_VENDOR_INVOICE_TYPEColumn, GST_VENDOR_INVOICE_TYPE);
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.GST_VENDOR_IDColumn, GST_VENDOR_ID);

                        //24/07/2023, To pass Authorization of the voucher
                        if (VoucherId == 0)
                        {
                            dataManager.Parameters.Add(this.AppSchema.VoucherMaster.AUTHORIZATION_STATUSColumn, AuthorizedStatus);
                            dataManager.Parameters.Add(this.AppSchema.VoucherMaster.AUTHORIZATION_UPDATED_BY_NAMEColumn, CreatedByName);
                        }
                        //On 30/08/2024, Is Multu currency Vocuhers
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.IS_MULTI_CURRENCYColumn, this.AllowMultiCurrency);

                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.IS_CASH_BANK_STATUSColumn, VoucherType == "JN" ? CashBankEntry : 1);

                        //dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        //On 01/09/2021, To set Voucher modifed by auditor 
                        //dataManager.Parameters.Add(this.AppSchema.VoucherMaster.IS_AUDITOR_MODIFIEDColumn, (IsVoucherModifiedByAuditor ?  1 : 0));

                        resultArgs = dataManager.UpdateData();
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        public ResultArgs SaveTallyVoucherMasterDetails(DataManager dm)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.AddTallyMigration))
            {
                dataManager.Database = dm.Database;
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_DATEColumn, VoucherDate);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_NOColumn, VoucherNo);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_TYPEColumn, VoucherType);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.NARRATIONColumn, Narration);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CREATED_BYColumn, CreatedBy);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.MODIFIED_BYColumn, ModifiedBy);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs FetchGSTPercentages(int GStId)
        {
            using (GSTClassSystem gstclass = new GSTClassSystem())
            {
                resultArgs = gstclass.FetchGSt(GStId);
            }
            return resultArgs;
        }


        public ResultArgs RevertCancelledVouchers(int[] VoucherIds)
        {
            try
            {
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.BeginTransaction();
                    foreach (int voucherId in VoucherIds)
                    {
                        resultArgs = FillVoucherDetails(voucherId);
                        if (resultArgs != null && resultArgs.Success)
                        {
                            if (VoucherSubType == ledgerSubType.GN.ToString())
                            {
                                resultArgs = ChangeCancelledVoucherStatus(voucherId);
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    using (BalanceSystem balanceSystem = new BalanceSystem())
                                    {
                                        resultArgs = balanceSystem.UpdateTransBalance(voucherId, TransactionAction.New);
                                        if (resultArgs.Success)
                                        {
                                            if (this.NumberSet.ToInteger(this.EnableVoucherRegenerationDeletion) == (int)YesNo.Yes)
                                            {
                                                resultArgs = RegenerateVoucherNumbers(dataManager, VoucherDate, VoucherDate);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (!resultArgs.Success)
                        {
                            dataManager.TransExecutionMode = ExecutionMode.Fail;
                            break;
                        }
                    }
                    dataManager.EndTransaction();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        public ResultArgs ChangeCancelledVoucherStatus(int voucherId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.ChangeCancelledVoucherStatus))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, voucherId);
                dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs UpdateVoucherTransNarration(DataManager dm)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.UpdateTallyTransNarration))
            {
                dataManager.Database = dm.Database;
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.NARRATIONColumn, Narration);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }



        public ResultArgs RemoveVoucher(DataManager dManager)
        {
            resultArgs = FillVoucherDetails(VoucherId);
            if (resultArgs.Success)
            {
                if (CommonMethod.ValidateLicensePeriod(VoucherDate, SettingProperty.Current.LicenseKeyYearFrom, SettingProperty.Current.LicenseKeyYearTo))
                {
                    if (!IsAuditLockedVoucherDate(ProjectId, VoucherDate))
                    {
                        //using (ImportMasterSystem importSystem = new ImportMasterSystem())
                        //{
                        using (DataManager dataManager = new DataManager())
                        {
                            using (BalanceSystem balanceSystem = new BalanceSystem())
                            {
                                dataManager.Database = dManager.Database;
                                resultArgs = balanceSystem.UpdateTransBalance(VoucherId, TransactionAction.Cancel);
                                if (resultArgs.Success)
                                {
                                    //resultArgs = importSystem.RemoveVoucherByVoucherId(VoucherId.ToString()); //Removing vouches physically
                                    resultArgs = DeleteVoucherReferenceDetails(dataManager); // Remove physically the voucher Reference Details
                                    if (resultArgs.Success)
                                    {
                                        //On 06/09/2021, To assign Voucher Previous details for Audit history log details
                                        resultArgs = AssignGetVoucherPreviousDetailsForAuditLog(VoucherId, dataManager);
                                        if (resultArgs.Success)
                                        {
                                            resultArgs = DeleteVoucherMasterDetails(dataManager);

                                            //On 06/09/2021, to update audit log details
                                            if (resultArgs.Success)
                                            {
                                                resultArgs = UpdateAuditLogHistory(dataManager, AuditAction.Deleted);

                                                if (resultArgs.Success && VoucherId > 0 && GST_INVOICE_ID > 0) //Delete booked invocie and its all related invoice 
                                                {
                                                    resultArgs = RemoveGSTVendorInvoiceDetailsById(VoucherId, GST_INVOICE_ID, dataManager); // RemoveGSTVendorInvoiceDetailsByVoucherId(VoucherId, dataManager);
                                                }
                                                else if (BookingGSTInvoiceId > 0) //Delete agaist voucher against invovice
                                                {
                                                    resultArgs = DeleteRandPVoucherAgainsInvoice(dataManager, VoucherId, BookingGSTInvoiceId);
                                                }
                                            }

                                            //On 31/07/2024, To remove Voucher Details
                                            if (resultArgs.Success)
                                            {
                                                resultArgs = DeleteVoucherImageDetailsByVoucher(dataManager);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        //}
                    }
                    else
                    {
                        resultArgs.Success = false;
                        string lockedmessage = "Unable to Delete/Modify Vouchers." + System.Environment.NewLine + "Voucher is locked for '" + LockProjectName + "'" +
                            " during the period of " + DateSet.ToDate(LockFromDate.ToShortDateString()) + " - " + DateSet.ToDate(LockToDate.ToShortDateString());
                        resultArgs.Message = lockedmessage;
                    }
                }
                else
                {
                    resultArgs.Success = false;
                    resultArgs.Message = "You are not allowed to make an entry for expired/future license period";
                }
            }
            return resultArgs;
        }


        /// <summary>
        /// Delete the Voucher from DB
        /// </summary>
        /// <param name="dManager"></param>
        /// <returns></returns>
        public ResultArgs RemovePhysicalVoucher(DataManager dManager)
        {
            using (DataManager dataManager = new DataManager())
            {
                using (BalanceSystem balanceSystem = new BalanceSystem())
                {
                    dataManager.Database = dManager.Database;
                    resultArgs = balanceSystem.UpdateTransBalance(VoucherId, TransactionAction.Cancel);
                    if (resultArgs.Success)
                    {
                        if (resultArgs.Success)
                        {
                            resultArgs = DeletePhysicalVoucherMasterDetails();
                        }
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs RemoveAssetStockVoucher()
        {
            using (DataManager dataManager = new DataManager())
            {
                resultArgs = RemoveVoucher(dataManager);
                if (resultArgs != null && resultArgs.Success)
                {
                    resultArgs = FillVoucherDetails(VoucherId);
                    if (resultArgs != null && resultArgs.Success)
                    {
                        if (this.NumberSet.ToInteger(this.EnableVoucherRegenerationDeletion) == (int)YesNo.Yes)
                        {
                            resultArgs = RegenerateVoucherNumbers(dataManager, VoucherDate, VoucherDate);
                        }
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs DeleteVoucher(DataManager dataMgr)
        {
            using (DataManager dataManager = new DataManager())
            {
                dataMgr.Database = dataManager.Database;
                //dataManager.BeginTransaction();
                resultArgs = RemoveVoucher(dataManager);
                if (resultArgs != null && resultArgs.Success)
                {
                    resultArgs = RemoveTDS(dataManager);
                    if (resultArgs != null && resultArgs.Success)
                    {
                        resultArgs = FillVoucherDetails(VoucherId);
                        if (resultArgs != null && resultArgs.Success)
                        {
                            if (this.NumberSet.ToInteger(this.EnableVoucherRegenerationDeletion) == (int)YesNo.Yes)
                            {
                                resultArgs = RegenerateVoucherNumbers(dataManager, VoucherDate, VoucherDate);
                            }
                        }
                    }
                }

                //if (!resultArgs.Success)
                //{
                //    dataManager.TransExecutionMode = ExecutionMode.Fail;
                //}

                //  dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public ResultArgs DeleteVoucherTrans()
        {
            resultArgs = FillVoucherDetails(VoucherId);
            if (resultArgs.Success)
            {
                if (CommonMethod.ValidateLicensePeriod(VoucherDate, SettingProperty.Current.LicenseKeyYearFrom, SettingProperty.Current.LicenseKeyYearTo))
                {
                    if (!IsAuditLockedVoucherDate(ProjectId, VoucherDate))
                    {
                        using (DataManager dataManager = new DataManager())
                        {
                            dataManager.BeginTransaction();
                            resultArgs = DeleteVoucher(dataManager);
                            if (!resultArgs.Success)
                            {
                                dataManager.TransExecutionMode = ExecutionMode.Fail;
                            }
                            dataManager.EndTransaction();
                        }
                    }
                    else
                    {
                        resultArgs.Success = false;
                        string lockedmessage = "Unable to Delete/Modify Vouchers." + System.Environment.NewLine + " Voucher is locked for '" + LockProjectName + "'" +
                            " during the period of " + DateSet.ToDate(LockFromDate.ToShortDateString()) + " - " + DateSet.ToDate(LockToDate.ToShortDateString());
                        resultArgs.Message = lockedmessage;
                    }
                }
                else
                {
                    resultArgs.Success = false;
                    resultArgs.Message = "You are not allowed to make an entry for expired/future license period";
                }
            }
            return resultArgs;
        }

        public int IsExistVoucherJournalRefTrans()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.IsExistReferenceVoucherTrans))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs DeleteRefererdVoucher()
        {
            int voucherid = FetchRecPayIdByRefVoucherId();
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.DeleteRefererdVoucherTrans))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, voucherid);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteRefererdVouchersByJournalVoucher()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.DeleteRefererdVouchersByJournalVoucher))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }


        public int FetchRecPayIdByRefVoucherId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchPayIdByJournalVoucherId))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        public int IsExistReferenceNo(string refNo, int voucherid, int Ledgerid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.IsExistReferenceNo))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.REFERENCE_NUMBERColumn, refNo);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, voucherid);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int IsExistsGSTInvoceNno(string gstInvoiceno, int voucherid)
        {
            int counts = -1;
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.IsExistGSTInvoiceNo))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, voucherid);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.GST_VENDOR_INVOICE_NOColumn, gstInvoiceno);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            if (resultArgs.Success)
            {
                counts = resultArgs.DataSource.Sclar.ToInteger;
            }
            return counts;
        }

        public bool IsExistsGSTVouchersByGSTClassId(Int32 gstclassid)
        {
            bool rtn = true;
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.IsExistGSTVouchersByGSTClassId))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.LEDGER_GST_CLASS_IDColumn, gstclassid);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            if (resultArgs.Success)
            {
                rtn = (resultArgs.DataSource.Sclar.ToInteger > 1);
            }
            return rtn;
        }

        public bool IsExistsGSTVouchers()
        {
            bool rtn = true;
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.IsExistGSTVouchers))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            if (resultArgs.Success)
            {
                rtn = (resultArgs.DataSource.Sclar.ToInteger > 1);
            }
            return rtn;
        }

        public ResultArgs FetchRandPVoucherAgainstJournalInvoiceByVoucherId(int voucherid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchRandPVoucherAgainstJournalInvoiceByVoucherId))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, voucherid);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchRandPVoucherAgainstJournalInvoiceByInvoiceId(int invoiceid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchRandPVoucherAgainstJournalInvoiceByInvoiceId))
            {
                dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.GST_INVOICE_IDColumn, invoiceid);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public bool IsZeroValuedCashBankExistsInVouchers()
        {
            bool rtn = true;
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.IsZeroValuedCashBankExistsInVouchers))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            if (resultArgs.Success)
            {
                rtn = (resultArgs.DataSource.Sclar.ToInteger > 1);
            }
            return rtn;
        }

        public bool IsVouchersExists()
        {
            bool rtn = true;
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.IsVouchersExists))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            if (resultArgs.Success)
            {
                rtn = (resultArgs.DataSource.Sclar.ToInteger > 1);
            }
            return rtn;
        }

        public bool isCashBankEnabledJournal()
        {
            bool rtnstatus = false;
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.IsCashBankEnbled))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            if (resultArgs.Success)
            {
                rtnstatus = (resultArgs.DataSource.Sclar.ToInteger > 1);
            }
            return rtnstatus;
        }

        public bool IsVoucherMadeForCountry(Int32 CountryId)
        {
            bool rtn = true;
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.IsVoucherMadeForCountry))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CURRENCY_COUNTRY_IDColumn, CountryId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            if (resultArgs.Success)
            {
                rtn = (resultArgs.DataSource.Sclar.ToInteger > 1);
            }
            return rtn;
        }


        /// <summary>
        /// On 21/11/2024, To check Receipt Voucher Made for not natures in finance setting
        /// To check Payment Voucher Made for not natures in finance setting
        /// </summary>
        /// <param name="VoucherType"></param>
        /// <param name="AllowedNatures"></param>
        /// <returns></returns>
        public bool IsReceiptPaymentVoucherMadeForOtherNatures(string VoucherType, string AllowedNatures)
        {
            bool rtn = false;
            if (AllowedNatures != "0")
            {
                using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.IsReceiptPaymentVoucherMadeForOtherNatures))
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_TYPEColumn, VoucherType);
                    dataManager.Parameters.Add(this.AppSchema.LedgerGroup.NATURE_IDColumn.ColumnName, AllowedNatures);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
                if (resultArgs.Success)
                {
                    rtn = (resultArgs.DataSource.Sclar.ToInteger > 1);
                }
            }
            return rtn;
        }

        public bool IsExistsGSTVendorDetails()
        {
            bool rtn = true;
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.IsExistGSTVendorVouchers))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            if (resultArgs.Success)
            {
                rtn = (resultArgs.DataSource.Sclar.ToInteger > 1);
            }
            return rtn;
        }

        public ResultArgs DeleteMultipleVoucherTrans()
        {
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.BeginTransaction();
                    foreach (DataRow drTransactions in dtBulkTransactions.Rows)
                    {
                        int SelectedVoucherId = this.NumberSet.ToInteger(drTransactions[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString());
                        //string iDeleteVoucherType = drTransactions["VOUCHERTYPE"].ToString();
                        string iDeleteVoucherType = drTransactions["VOUCHER_TYPE"].ToString();
                        VoucherSubType = drTransactions[this.AppSchema.VoucherMaster.VOUCHER_SUB_TYPEColumn.ColumnName].ToString();
                        VoucherId = SelectedVoucherId;
                        VoucherDefinitionId = this.NumberSet.ToInteger(drTransactions[this.AppSchema.VoucherNumber.VOUCHER_DEFINITION_IDColumn.ColumnName].ToString());
                        if (VoucherSubType == ledgerSubType.FD.ToString())
                        {
                            using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                            {
                                fdAccountSystem.VoucherId = SelectedVoucherId;
                                resultArgs = fdAccountSystem.FetchFDAccountId();
                                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                                {
                                    fdAccountSystem.FDVoucherId = SelectedVoucherId;
                                    fdAccountSystem.FDAccountId = fdAccountSystem.FetchFDAId();
                                    if (fdAccountSystem.FDAccountId != 0)
                                    {
                                        if (fdAccountSystem.CheckFDAccountExists() == 0)
                                        {
                                            resultArgs = DeleteVoucher(dataManager);
                                            if (resultArgs.Success)
                                            {
                                                resultArgs = fdAccountSystem.DeleteFDAccountDetails();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        string RenewalType = string.Empty;
                                        string VoucherIdfd = string.Empty;
                                        string VoucherId1 = string.Empty;
                                        string VoucherId2 = string.Empty;
                                        DataTable dtRenewalType = fdAccountSystem.FetchFDRenewalType();
                                        foreach (DataRow dr in dtRenewalType.Rows)
                                        {
                                            RenewalType = dr[fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName] != null ? dr[fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName].ToString() : string.Empty;
                                            fdAccountSystem.FDAccountId = dr[fdAccountSystem.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn.ColumnName] != null ? this.NumberSet.ToInteger(dr[fdAccountSystem.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn.ColumnName].ToString()) : 0;
                                            VoucherIdfd = dr[fdAccountSystem.AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn.ColumnName] != null ? dr[fdAccountSystem.AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn.ColumnName].ToString() : string.Empty;
                                            VoucherId1 = dr[fdAccountSystem.AppSchema.FDRenewal.FD_VOUCHER_IDColumn.ColumnName] != null ? dr[fdAccountSystem.AppSchema.FDRenewal.FD_VOUCHER_IDColumn.ColumnName].ToString() : string.Empty;
                                            VoucherId2 = VoucherId1 + "," + VoucherIdfd;
                                            if (RenewalType == FDRenewalTypes.WDI.ToString())
                                            {
                                                string[] voucherId = VoucherId2.Split(',');
                                                foreach (string sValue in voucherId)
                                                {
                                                    VoucherId = this.NumberSet.ToInteger(sValue);
                                                    resultArgs = DeleteVoucher(dataManager);
                                                }
                                                if (resultArgs.Success)
                                                {
                                                    resultArgs = fdAccountSystem.DeleteFDRenewalsByVoucherId();
                                                }
                                            }
                                            else
                                            {
                                                if (fdAccountSystem.CheckFDRenewalClosed() == 0)
                                                {
                                                    resultArgs = DeleteVoucher(dataManager);
                                                    if (resultArgs.Success)
                                                    {
                                                        resultArgs = fdAccountSystem.DeleteFDRenewalsByVoucherId();
                                                    }
                                                }
                                                else
                                                {
                                                    resultArgs.Message = MessageCatalog.Transaction.VocherTransaction.VOUCHER_CANNOT_DELETE_FD_RECEIPT_ENTRY;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    fdAccountSystem.FDVoucherId = SelectedVoucherId;
                                    resultArgs = fdAccountSystem.DeleteFDAccountByVoucherId();
                                }
                            }
                        }
                        else
                        {
                            if (!IsFullRightsReservedUser)
                            {
                                if (iDeleteVoucherType == DefaultVoucherTypes.Receipt.ToString())
                                {
                                    if (CommonMethod.ApplyUserRightsForTransaction((int)Receipt.DeleteReceiptVoucher) != 0)
                                    {
                                        resultArgs = DeleteVoucher(dataManager);
                                    }
                                    else
                                    {
                                        resultArgs.Message = "No rights to delete this Receipt Vouchers";
                                    }
                                }
                                else if (iDeleteVoucherType == DefaultVoucherTypes.Payment.ToString())
                                {
                                    if (CommonMethod.ApplyUserRightsForTransaction((int)Payment.DeletePaymentVoucher) != 0)
                                    {
                                        resultArgs = DeleteVoucher(dataManager);
                                    }
                                    else
                                    {
                                        resultArgs.Message = "No rights to delete this Payment Vouchers";
                                    }
                                }
                                else
                                {
                                    if (CommonMethod.ApplyUserRightsForTransaction((int)Contra.DeleteContraVoucher) != 0)
                                    {
                                        resultArgs = DeleteVoucher(dataManager);

                                    }
                                    else
                                    {
                                        resultArgs.Message = "No rights to delete this Contra Vouchers";
                                    }
                                }
                            }
                            else
                            {
                                resultArgs = DeleteVoucher(dataManager);
                            }
                            if (!resultArgs.Success)
                            {
                                break;
                            }
                        }
                    }
                    if (!resultArgs.Success)
                    {
                        dataManager.TransExecutionMode = ExecutionMode.Fail;
                    }
                    dataManager.EndTransaction();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }
            finally { }

            return resultArgs;
        }

        public ResultArgs FetchVoucherNumberDefinition()
        {
            SetRegenerateMethod(VoucherType, VoucherDefinitionId);
            using (ProjectSystem projectSystem = new ProjectSystem())
            {
                resultArgs = projectSystem.FetchVoucherByProjectId(ProjectId, VoucherMethodType.ToString(), VoucherDefinitionId);
            }
            return resultArgs;
        }

        /// <summary>
        /// Delete the Voucher Transaction from the DB physically
        /// </summary>
        /// <returns></returns>
        public ResultArgs DeletePhysicalVoucherTrans()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = RemovePhysicalVoucher(dataManager);
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        /// <summary>
        /// On 26/08/2021, to update voucher modified details for given ledger's vouchers and project
        /// </summary>
        /// <param name="ledgerids"></param>
        /// <param name="projectids"></param>
        /// <param name="dManager"></param>
        /// <returns></returns>
        public ResultArgs UpdateVoucherModifiedDetailsByLedgerIds(string ledgerids, string projectids, DataManager dManager)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.UpdateModifiedDetailsbyLedgerIds))
            {
                dataManager.Database = dManager.Database;
                dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_IDColumn, ledgerids);
                if ((!string.IsNullOrEmpty(projectids)) && projectids != "0")
                {
                    dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, projectids);
                }
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.MODIFIED_BYColumn, this.NumberSet.ToInteger(this.LoginUserId.ToString()));
                dataManager.Parameters.Add(this.AppSchema.User.USER_NAMEColumn, this.FirstName); //MODIFIED_BY_NAME

                //On 02/09/2021, To update Auditor Modification flag for Auditor users/for other user leave as it is.
                //dataManager.Parameters.Add(this.AppSchema.User.IS_AUDITORColumn, (IsLoginUserAuditor ? 1 : 0));

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// On 27/08/2021, to update voucher modified details for given ledger's vouchers and project
        /// </summary>
        /// <param name="ledgerids"></param>
        /// <param name="projectids"></param>
        /// <param name="dManager"></param>
        /// <returns></returns>
        public ResultArgs UpdateVoucherModifiedDetailsByVouchers(string voucherids, string projectids, DataManager dManager)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.UpdateModifiedDetailsbyVoucherIds))
            {
                dataManager.Database = dManager.Database;
                dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_IDColumn, voucherids);
                if ((!string.IsNullOrEmpty(projectids)) && projectids != "0")
                {
                    dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, projectids);
                }
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.MODIFIED_BYColumn, this.NumberSet.ToInteger(this.LoginUserId.ToString()));
                dataManager.Parameters.Add(this.AppSchema.User.USER_NAMEColumn, this.LoginUserName); //MODIFIED_BY_NAME
                dataManager.DataCommandArgs.IsDirectReplaceParameter = false;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs RemoveTDS(DataManager dmanager = null)
        {
            try
            {
                int JVoucherID = VoucherId;  // Actual Voucher ID having in temp to be used later
                bool isTDSVoucher = IsTDSVouchers(); // Checking the voucher is TDS Voucher
                if (isTDSVoucher)
                {
                    if (tdsTransType == TDSTransType.TDSPayment) // Checking whether the current voucher is TDS Payment 
                    {
                        string PaymentVoucherIDs = string.Empty;
                        resultArgs = RemoveTDSPayment(ref PaymentVoucherIDs); // Delete the TDS Payment details in TDS Payment Table
                    }
                    else
                    {
                        string PaymentVoucherIDs = string.Empty;
                        resultArgs = RemoveTDSPayment(ref PaymentVoucherIDs); // Deleting the TDS Payment  in (TDS Payment Tables)If done against the Voucher 
                        if (resultArgs.Success)
                        {
                            if (!string.IsNullOrEmpty(PaymentVoucherIDs)) // Obtained the Collections of TDS Payment Voucher Id to be deleted
                            {
                                foreach (string TDSPyVoucherId in PaymentVoucherIDs.Split(','))
                                {
                                    if (resultArgs.Success)
                                    {
                                        VoucherId = NumberSet.ToInteger(TDSPyVoucherId);
                                        if (IsVoucherDeleted() > 0) // This is to check whether the voucher deleted or not, If not deleted delete now
                                        {
                                            resultArgs = RemoveVoucher(dmanager); // Deleting the TDS Payment Vouchers in Voucher Trans Table
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            if (resultArgs.Success)
                            {
                                resultArgs = RemoveTDSBooking(); // Deleting the TDS Booking Voucher If exists
                                if (resultArgs.Success)
                                {
                                    // Fetch the Party Payment Voucher : If the user deletes the TDS Booking and If booking is done from while Making Party Payment
                                    int PaymentVId = FetchTDSPartyVIDbyBookingVID(JVoucherID);
                                    if (PaymentVId > 0)
                                    {
                                        VoucherId = PaymentVId;
                                        if (IsVoucherDeleted() > 0) // This is to check whether the voucher deleted or not, If not deleted delete now
                                        {
                                            resultArgs = RemoveVoucher(dmanager); // Obtained Party Voucher to be deleted
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                resultArgs.Message = e.Message;
            }
            // Commented by Praveen : 14-10-2016

            //bool isTDSVoucher = IsTDSVouchers();
            //if (isTDSVoucher && tdsTransType.Equals(TDSTransType.TDSBooking))
            //{
            //    resultArgs = RemoveTDSBooking();
            //}
            //else if (isTDSVoucher && tdsTransType.Equals(TDSTransType.TDSPayment))
            //{
            //    resultArgs = RemoveTDSPayment();
            //}
            return resultArgs;
        }

        private ResultArgs RemoveTDSPayment(ref string TDSPayID)
        {
            using (TDSPaymentSystem TDSPaySystem = new TDSPaymentSystem())
            {
                TDSPaySystem.VoucherId = VoucherId;
                resultArgs = tdsTransType == TDSTransType.TDSPayment ? TDSPaySystem.FetchTDSPaymentId() : TDSPaySystem.FetchTDSBookingMappedPaymentId(); // Fetch TDS Payment Voucher ID Collections from TDS Payment Table for the selected Voucher
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    foreach (DataRow dritem in resultArgs.DataSource.Table.Rows)
                    {
                        TDSPaySystem.TDSPaymentId = dritem[TDSPaySystem.AppSchema.TDSPayment.TDS_PAYMENT_IDColumn.ColumnName] != DBNull.Value ?
                                                 NumberSet.ToInteger(dritem[TDSPaySystem.AppSchema.TDSPayment.TDS_PAYMENT_IDColumn.ColumnName].ToString()) : 0;
                        TDSPayID += dritem["VOUCHER_ID"].ToString() + ',';

                        if (TDSPaySystem.TDSPaymentId > 0)
                        {
                            resultArgs = TDSPaySystem.DeleteTDS();
                        }
                    }
                }
                // Commented by Praveen in order to delete multiple Payment for single Voucher : 14-10-2016

                //resultArgs = TDSPaySystem.FetchTDSPaymentId();
                //if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                //{
                //    TDSPaySystem.TDSPaymentId = resultArgs.DataSource.Table.Rows[0][TDSPaySystem.AppSchema.TDSPayment.TDS_PAYMENT_IDColumn.ColumnName] != DBNull.Value ?
                //                             NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][TDSPaySystem.AppSchema.TDSPayment.TDS_PAYMENT_IDColumn.ColumnName].ToString()) : 0;
                //    if (TDSPaySystem.TDSPaymentId > 0)
                //    {
                //        resultArgs = TDSPaySystem.DeleteTDS();
                //    }
                //}
            }
            TDSPayID = TDSPayID.Trim(',');
            return resultArgs;
        }

        private ResultArgs RemoveTDSBookingold()
        {
            //using (TDSBookingSystem BookingSystem = new TDSBookingSystem())
            //{
            //    BookingSystem.VoucherId = VoucherId;
            //    if (CheckTDSBooking() == 0)
            //    {
            //        if (BookingSystem.CheckVoucherInBooking() > 0)
            //        {
            //            resultArgs = BookingSystem.DeleteBookingByVoucher();
            //        }
            //        else
            //        {
            //            resultArgs = BookingSystem.DeleteDeductionByVoucherId();
            //        }
            //    }
            //    else
            //    {
            //        resultArgs = BookingSystem.DeleteDeductionByVoucherId();
            //    }
            //}
            return resultArgs;
        }

        private ResultArgs RemoveTDSBooking()
        {
            using (TDSBookingSystem BookingSystem = new TDSBookingSystem())
            {
                BookingSystem.VoucherId = VoucherId;
                if (BookingSystem.CheckTDSBookingExists() > 0)
                {
                    resultArgs = BookingSystem.DeleteBookingByVoucher(); // Delete the TDS Booking 
                    if (resultArgs.Success)
                    {
                        resultArgs = BookingSystem.DeleteDeductionByVoucherId(); // Delete the TDS Deduction
                    }
                }
            }
            return resultArgs;
        }

        private bool IsTDSVouchers()
        {
            bool isTDSVoucher = false;
            using (TDSBookingSystem BookingSystem = new TDSBookingSystem())
            {
                BookingSystem.VoucherId = VoucherId;
                isTDSVoucher = BookingSystem.IsTDSVouchers();
                tdsTransType = BookingSystem.tdsTransType;
            }
            return isTDSVoucher;
        }

        //public ResultArgs SavePostVoucherTransaction()
        //{
        //    using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
        //    {
        //        foreach (DataRow dr in dtPostVoucherDetails.Rows)
        //        {

        //            //Voucher Master Details
        //            voucherTransaction.VoucherId = 0;
        //            voucherTransaction.ProjectId = ProjectId;
        //            voucherTransaction.VoucherDate = this.DateSet.ToDate(dr["VOUCHER_DATE"].ToString(), false);

        //            //On 15/02/2019, for multi voucher type
        //            //voucherTransaction.VoucherType = dtVouchers.Rows[0][VOUCHER_TYPE].ToString();
        //            string vtype = dr["VOUCHER_TYPE"].ToString().ToUpper();
        //            voucherTransaction.VoucherType = vtype;
        //            voucherTransaction.VoucherDefinitionId = (Int32)DefaultVoucherTypes.Receipt;
        //            if (vtype == "RC") voucherTransaction.VoucherDefinitionId = (Int32)DefaultVoucherTypes.Receipt;
        //            if (vtype == "PY") voucherTransaction.VoucherDefinitionId = (Int32)DefaultVoucherTypes.Payment;
        //            if (vtype == "CN") voucherTransaction.VoucherDefinitionId = (Int32)DefaultVoucherTypes.Contra;
        //            if (vtype == "JN") voucherTransaction.VoucherDefinitionId = (Int32)DefaultVoucherTypes.Journal;

        //            voucherTransaction.VoucherSubType = LedgerTypes.GN.ToString();
        //            voucherTransaction.TransVoucherMethod = (int)TransactionVoucherMethod.Automatic;
        //            voucherTransaction.VoucherNo = "";
        //            voucherTransaction.DonorId = 0;
        //            voucherTransaction.PurposeId = 0;
        //            voucherTransaction.ContributionType = "N";
        //            voucherTransaction.ContributionAmount = 0.00m;
        //            voucherTransaction.CurrencyCountryId = 0;
        //            voucherTransaction.ExchangeRate = 1;
        //            voucherTransaction.CalculatedAmount = 0;
        //            voucherTransaction.ActualAmount = 0;
        //            voucherTransaction.ExchageCountryId = 0;
        //            voucherTransaction.Narration = dr["NARRATION"].ToString();
        //            voucherTransaction.Status = 1;
        //            voucherTransaction.FDGroupId = 0;
        //            voucherTransaction.CreatedBy = this.NumberSet.ToInteger(voucherTransaction.LoginUserId);
        //            voucherTransaction.ModifiedBy = this.NumberSet.ToInteger(voucherTransaction.LoginUserId);
        //            voucherTransaction.NameAddress = "";
        //            voucherTransaction.ClientReferenceId = this.NumberSet.ToInteger(dr["CLIENT_REFERENCE_ID"].ToString());
        //            voucherTransaction.ClientCode = dr["CLIENT_CODE"].ToString();

        //            //Voucher Trans Details
        //            DataView dvTrans = new DataView(dtTransaction);
        //            this.Transaction.TransInfo = dvTrans;

        //            DataView dvCashTrans = new DataView(dtCashTransaction);
        //            this.Transaction.CashTransInfo = dvCashTrans;

        //            // Fetch Voucher id to update voucher details

        //            resultArgs = voucherTransaction.FetchVoucherIdByClientRefCode();
        //            if (resultArgs.Success && resultArgs.DataSource.Sclar.ToInteger > 0)
        //            {
        //                voucherTransaction.VoucherId = rtnVoucherId = resultArgs.DataSource.Sclar.ToInteger;
        //            }

        //            resultArgs = voucherTransaction.SaveTransactions();
        //            if (resultArgs.Success && resultArgs.RowsAffected > 0)
        //            {
        //                lock (objlock)
        //                {
        //                    frmPopup alert = new frmPopup(PopupSkins.InfoSkin);
        //                    // string Vdate = UtilityMember.DateSet.ToDate(dr[VOUCHER_DATE].ToString(), false).ToShortDateString();
        //                    PostVoucherId = voucherTransaction.VoucherId > 0 ? voucherTransaction.VoucherId : 0;
        //                    // alert.ShowPopup(dr[CLIENT_CODE].ToString() + " Source :  ", "Voucher Posted for the project  " + dr[PROJECT].ToString() + " on " + Vdate + "\nVoucher Reference No :" + PostVoucherId + "", 500, 6000, 500);
        //                }
        //                AcMELog.WriteLog("Post Vouchers ended..");
        //            }
        //            else
        //            {
        //                PostVoucherId = -1;
        //                AcMELog.WriteLog("Failed in Posted Voucher:" + resultArgs.Message);
        //            }
        //        }
        //    }

        //    return resultArgs;
        //}

        public ResultArgs SaveTransactions()
        {
            try
            {
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.BeginTransaction();
                    resultArgs = SaveVoucherDetails(dataManager);

                    if (resultArgs.Success)
                    {
                        if (this.NumberSet.ToInteger(this.TDSEnabled.ToString()).Equals((int)SetDefaultValue.DefaultValue) && dsTDSBooking != null && dsTDSBooking.Tables.Count > 0)
                        {
                            resultArgs = SaveTDSBooking(dataManager);
                            TDSBookingVoucherId = VoucherId;
                            if (resultArgs != null && resultArgs.Success)
                            {
                                if (this.NumberSet.ToInteger(this.TDSEnabled.ToString()).Equals((int)SetDefaultValue.DefaultValue) && dtTDSDeductionLater != null && dtTDSDeductionLater.Rows.Count > 0)
                                {
                                    VoucherId = 0;
                                    this.TransInfo = dvTdsUcTransSummary;
                                    Narration = TDSPartyNarration;
                                    VoucherType = VoucherSubTypes.JN.ToString();
                                    VoucherSubType = ledgerSubType.GN.ToString();
                                    resultArgs = SaveVoucherDetails(dataManager);
                                    if (resultArgs != null && resultArgs.Success)
                                    {
                                        resultArgs = SaveTDSDeductionLater(dataManager);
                                    }
                                }

                            }
                        }
                        else if (this.NumberSet.ToInteger(this.TDSEnabled.ToString()).Equals((int)SetDefaultValue.DefaultValue) && dtTDSDeductionLater != null && dtTDSDeductionLater.Rows.Count > 0)
                        {
                            resultArgs = SaveTDSDeductionLater(dataManager);
                        }

                        //This is to deduct tds later from Journal Add Form.

                        //else if (this.NumberSet.ToInteger(this.TDSEnabled.ToString()).Equals((int)SetDefaultValue.DefaultValue) && dsTDSDeductionLater != null && dsTDSDeductionLater.Tables.Count > 0)
                        //{
                        //    resultArgs = SaveTDSDeductionLater(dataManager);
                        //}

                        else if (this.NumberSet.ToInteger(this.TDSEnabled.ToString()).Equals((int)SetDefaultValue.DefaultValue) && TDSPartyPayment != null && TDSPartyPayment.Rows.Count > 0)
                        {
                            resultArgs = saveTDSPartyPayment();
                            this.TDSPartyPayment = null;
                            this.TDSPartyPaymentBank = null;
                        }
                        else if (this.NumberSet.ToInteger(this.TDSEnabled.ToString()).Equals((int)SetDefaultValue.DefaultValue) && TDSPayment != null && TDSPayment.Rows.Count > 0)
                        {
                            resultArgs = saveTDSPayment();
                            this.TDSPaymentBank = null;
                            this.TDSPayment = null;
                        }
                    }
                    else
                    {
                        dataManager.TransExecutionMode = ExecutionMode.Fail;
                    }
                    dataManager.EndTransaction();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        #region TDS Deletion

        //public ResultArgs RemoveTDSVouchers(DataManager DManager)
        //{
        //    using (ImportMasterSystem importSystem = new ImportMasterSystem())
        //    {
        //        using (DataManager dataManager = new DataManager())
        //        {
        //            dataManager.Database = DManager.Database;
        //            using (BalanceSystem balanceSystem = new BalanceSystem())
        //            {
        //                resultArgs = balanceSystem.UpdateTransBalance(VoucherId, TransactionAction.Cancel);
        //                if (resultArgs.Success)
        //                {
        //                    resultArgs = DeleteVoucherMasterDetails();
        //                }
        //            }
        //        }
        //    }
        //    return resultArgs;
        //}

        public int FetchTDSPartyVIDbyBookingVID(int JVoucherID)
        {
            int TDSBookingVID = 0;
            using (TDSBookingSystem tdsbookingsystem = new TDSBookingSystem())
            {
                tdsbookingsystem.VoucherId = JVoucherID;
                resultArgs = tdsbookingsystem.FetchTDSPartyVIDbyBookingVID();

                if (resultArgs != null && resultArgs.DataSource.Sclar.ToInteger > 0)
                {
                    TDSBookingVID = resultArgs.DataSource.Sclar.ToInteger;
                }
            }
            return TDSBookingVID;
        }

        #endregion


        private ResultArgs saveTDSPayment()
        {
            try
            {
                using (TDSPaymentSystem TDSPayment = new TDSPaymentSystem())
                {
                    TDSPayment.TDSPaymentId = TDSPaymentId;
                    TDSPayment.ProjectId = ProjectId;
                    TDSPayment.PartyLedgerId = this.TDSPaymentBank != null && this.TDSPaymentBank.Rows.Count > 0 && this.TDSPaymentBank.Rows[0][TDSPayment.AppSchema.Ledger.LEDGER_IDColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(this.TDSPaymentBank.Rows[0][TDSPayment.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()) : 0;
                    TDSPayment.VoucherId = VoucherId;
                    TDSPayment.BookingDate = VoucherDate;
                    TDSPayment.TDSPaymentDetail = this.TDSPayment;
                    TDSPayment.VoucherNo = VoucherNo = VoucherNo;
                    TDSPayment.Narration = Narration = Narration;
                    resultArgs = TDSPayment.SaveTDS();
                    TDSPaymentId = TDSPaymentId > 0 ? TDSPayment.TDSPaymentId : 0;
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        private ResultArgs saveTDSPartyPayment()
        {
            try
            {
                using (PartyPaymentSystem TDSPartyPayment = new PartyPaymentSystem())
                {
                    TDSPartyPayment.ProjectId = ProjectId;
                    TDSPartyPayment.VoucherId = VoucherId;
                    TDSPartyPayment.VocherDate = VoucherDate;
                    TDSPartyPayment.PartyLedgerId = this.TDSPartyPayment != null && this.TDSPartyPayment.Rows.Count > 0 ? this.NumberSet.ToInteger(this.TDSPartyPayment.Rows[0][this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()) : 0;
                    TDSPartyPayment.PaymentLedgerId = this.TDSPartyPaymentBank != null && this.TDSPartyPaymentBank.Rows.Count > 0 ? this.NumberSet.ToInteger(this.TDSPartyPaymentBank.Rows[0][this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()) : 0;
                    TDSPartyPayment.PartyPaymentId = TDSPartyPaymentId;

                    resultArgs = TDSPartyPayment.SavePartyPayments();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        private ResultArgs RemoveFixedDeposit()
        {
            resultArgs = FetchTransactions(VoucherId.ToString());
            if (resultArgs.Success)
            {
                DataTable dtTrans = resultArgs.DataSource.Table;
                if (dtTrans != null)
                {
                    foreach (DataRow dr in dtTrans.Rows)
                    {
                        int ledId = this.NumberSet.ToInteger(dr[this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
                        using (LedgerSystem ledgersystem = new LedgerSystem())
                        {
                            ledgersystem.LedgerId = ledId;
                            int LedgerGroupId = ledgersystem.FetchLedgerGroupById();
                            if (LedgerGroupId == (int)FixedLedgerGroup.FixedDeposit)
                            {
                                int BankAccountId;
                                int FDStatus;
                                BankAccountId = new LedgerSystem().FetchBankAccountById(ledId);
                                FDStatus = FetchFixedDepositStatus(BankAccountId);
                                if (FDStatus == (int)FixedDepositStatus.Deposited)    //Deposited FD
                                {
                                    resultArgs = DeleteFixedDepositByID(BankAccountId);
                                    if (resultArgs.Success)
                                    {
                                        resultArgs = UpdateBankAccountById(BankAccountId);
                                    }
                                }
                                else     //Realized FD
                                {
                                    resultArgs = UpdateFDStatusByID((int)FixedDepositStatus.Realized, BankAccountId);
                                }
                            }
                        }
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs DeleteVoucherMasterDetails(DataManager dm)
        {
            //On 01/09/2021, To assign Audit log details
            AssignVoucherAuditDetails();

            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.Delete))
            {
                dataManager.Database = dm.Database;
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);

                //25/08/2021, to update Modified details
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.MODIFIED_BYColumn, ModifiedBy);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.MODIFIED_BY_NAMEColumn, ModifiedByName);

                //On 01/09/2021, To set Voucher modifed by auditor 
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.IS_AUDITOR_MODIFIEDColumn, (IsVoucherModifiedByAuditor ? 1 : 0));
                resultArgs = dataManager.UpdateData();
            }

            return resultArgs;
        }

        public ResultArgs DeleteVoucherReferenceDetails(DataManager dm)
        {
            resultArgs = DeleteVoucherReferenceNo(dm);
            if (resultArgs.Success)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.DeleteReference))
                {
                    dataManager.Database = dm.Database;
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.UpdateData();
                }
            }
            return resultArgs;
        }

        private ResultArgs DeleteVoucherReferenceNo(DataManager dm)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.DeleteVoucherReferenceNo))
            {
                dataManager.Database = dm.Database;
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// Delete the Transaction Permanently from DB
        /// </summary>
        /// <returns></returns>
        private ResultArgs DeletePhysicalVoucherMasterDetails()
        {
            //foreach (int Id in VoucherIdList)
            //{
            //int VoucherId = Id;
            resultArgs = DeletePhysicalCostCentreVoucher(VoucherId);
            if (resultArgs.Success)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.PhysicalDelete))
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                    resultArgs = dataManager.UpdateData();
                }
                // }
            }
            return resultArgs;
        }
        /// <summary>
        /// Delete the Cost centre Transaction Permanently from DB
        /// </summary>
        /// <returns></returns>
        private ResultArgs DeletePhysicalCostCentreVoucher(int VouId)
        {
            using (DataManager dataManage = new DataManager(SQLCommand.VoucherMaster.DeletePhysicalCostCentreTrans))
            {
                dataManage.Parameters.Add(this.AppSchema.VoucherCostCentre.VOUCHER_IDColumn, VouId);
                resultArgs = dataManage.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchDeletedVoucher()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.ValidateDeletedVoucher))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchCashBankNegativeBalanceHistory()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchNegativeBalanceHistory))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_DATEColumn, VoucherDate);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchCostcentreVoucherView(int ProjectId, DateTime dtDateFrom, DateTime dtDateTo, string VoucherId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchVoucherViewCostcentre))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, dtDateFrom);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, dtDateTo);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// Deleted cost Center Details (chinna)
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="dtDateFrom"></param>
        /// <param name="dtDateTo"></param>
        /// <param name="VoucherId"></param>
        /// <returns></returns>
        public ResultArgs FetchDeletedCostcentreVoucherView(int ProjectId, DateTime dtDateFrom, DateTime dtDateTo, string VoucherId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchDeletedVoucherViewCostCentre))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, dtDateFrom);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, dtDateTo);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion

        #region voucher Transaction Methods
        public ResultArgs Deletevouchertransactiondetails(DataManager dm, int voucherId)
        {


            using (DataManager datamanager = new DataManager(SQLCommand.VoucherTransDetails.Delete))
            {
                datamanager.Database = dm.Database;
                datamanager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, voucherId);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs FetchTransactionDetails(int ProjectID, string Voucher_Type, DateTime dateFrom, DateTime dateTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchMasterDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_TYPEColumn, Voucher_Type);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectID);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, dateFrom);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, dateTo);

                //Currency enabled for currecny FY
                dataManager.Parameters.Add(this.AppSchema.Ledger.APPLICABLE_FROMColumn, YearFrom);
                dataManager.Parameters.Add(this.AppSchema.Ledger.APPLICABLE_TOColumn, YearTo);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetch the Deleted Transactions (chinna)
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="Voucher_Type"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        private ResultArgs FetchDeleteTransactionDetails(int ProjectID, string Voucher_Type, DateTime dateFrom, DateTime dateTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchDeleteMasterDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_TYPEColumn, Voucher_Type);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectID);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, dateFrom);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, dateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchJournalTransactionDetails(int ProjectID, DateTime dateFrom, DateTime dateTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchJournalDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectID);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, dateFrom);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, dateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs FetchTransactions(string VoucherID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.FetchTransactionDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherID);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs FetchVoucherTrans(int ProjectID, string Voucher_Type, DateTime dateFrom, DateTime dateTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.FetchVoucherTrans))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_TYPEColumn, Voucher_Type);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectID);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, dateFrom);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, dateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs DeleteFixedDepositByID(int BankAccountId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.DeleteFDByID))
            {
                dataManager.Parameters.Add(this.AppSchema.BankAccount.BANK_ACCOUNT_IDColumn, BankAccountId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private int FetchFixedDepositStatus(int BankAccountId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.FetchFixedDepositStatus))
            {
                dataManager.Parameters.Add(this.AppSchema.BankAccount.BANK_ACCOUNT_IDColumn, BankAccountId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        private ResultArgs UpdateFDStatusByID(int Status, int BankAccountId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.UpdateStatusByID))
            {
                dataManager.Parameters.Add(AppSchema.FDRegisters.STATUSColumn, 1);
                dataManager.Parameters.Add(AppSchema.BankAccount.BANK_ACCOUNT_IDColumn, BankAccountId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs UpdateBankAccountById(int BankAccountId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FixedDeposit.UpdateFD))
            {
                int zero = 0;
                dataManager.Parameters.Add(AppSchema.BankAccount.PERIOD_YEARColumn, zero);
                dataManager.Parameters.Add(AppSchema.BankAccount.PERIOD_MTHColumn, zero);
                dataManager.Parameters.Add(AppSchema.BankAccount.PERIOD_DAYColumn, zero);
                dataManager.Parameters.Add(AppSchema.BankAccount.INTEREST_RATEColumn, zero);
                dataManager.Parameters.Add(AppSchema.BankAccount.MATURITY_DATEColumn, string.Empty);
                dataManager.Parameters.Add(AppSchema.BankAccount.AMOUNTColumn, zero);
                dataManager.Parameters.Add(AppSchema.BankAccount.BANK_ACCOUNT_IDColumn, BankAccountId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs MoveProjectVoucher(MoveTransForm moveForm, DateTime transFromVoucherDate, int transFromProjectId, DataManager da)
        {
            using (DataManager dataManager = new DataManager())
            {
                da.Database = da.Database;
                if (MoveTransactionType != MultiMoveTransType.Multiple.ToString())
                {
                    resultArgs = AssignTransactionDetails(moveForm);
                }
                else
                {
                    resultArgs = AssignTransaction(moveForm);
                }

                if (resultArgs.Success)
                {
                    resultArgs = SaveVoucherDetails(dataManager);
                    if (resultArgs.Success)
                    {
                        if (this.NumberSet.ToInteger(this.EnableVoucherRegenerationInsert) == (int)YesNo.Yes)
                        {
                            ProjectId = transFromProjectId;
                            resultArgs = RegenerateVoucherNumbers(dataManager, transFromVoucherDate, transFromVoucherDate);
                        }
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs MoveVoucherDetails(MoveTransForm moveForm, DateTime transFromVoucherDate, int transFromProjectId)
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = MoveProjectVoucher(moveForm, transFromVoucherDate, transFromProjectId, dataManager);
                if (!resultArgs.Success)
                {
                    dataManager.TransExecutionMode = ExecutionMode.Fail;
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs AssignTransactionDetails(MoveTransForm moveForm)
        {
            if (moveForm == MoveTransForm.Transaction)
            {
                resultArgs = FetchTransDetails();
            }
            else
            {
                resultArgs = FetchJournalDetails();
            }

            if (resultArgs.Success)
            {
                DataTable dtTrans = resultArgs.DataSource.Table;
                this.TransInfo = dtTrans.DefaultView;
                dsCostCentre.Clear();
                dsCostCentre.Tables.Clear();
                if (dtTrans != null)
                {
                    foreach (DataRow drTrans in dtTrans.Rows)
                    {
                        LedgerId = this.NumberSet.ToInteger(drTrans[this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
                        GroupId = this.NumberSet.ToInteger(drTrans[this.AppSchema.LedgerGroup.GROUP_IDColumn.ColumnName].ToString());
                        VoucherDefinitionId = this.NumberSet.ToInteger(drTrans[this.AppSchema.VoucherNumber.VOUCHER_DEFINITION_IDColumn.ColumnName].ToString());
                        if (!drTrans[this.AppSchema.VoucherMaster.VOUCHER_SUB_TYPEColumn.ColumnName].Equals("FD"))
                        {
                            if (GroupId == (int)FixedLedgerGroup.BankAccounts)
                            {
                                resultArgs = CheckLedgerMappedByProject(LedgerId, ProjectId);
                                if (resultArgs.Success && resultArgs != null && resultArgs.DataSource.Sclar.ToInteger > 0)
                                {
                                    resultArgs = MapProjectLedger(LedgerId);
                                }
                                else
                                {
                                    resultArgs.Message = "Bank account is not mapped"; // to move transaction
                                }
                            }
                            else
                            {
                                resultArgs = MapProjectLedger(LedgerId);
                            }
                        }
                        else
                        {
                            resultArgs.Message = "FD Accounts Transaction can not be Moved";
                        }

                        //Check Voucher Defintion mapped with moved project
                        if (resultArgs.Success)
                        {
                            resultArgs = CheckVoucherTypeMappedByProject(ProjectId, VoucherDefinitionId);
                            if (resultArgs.Success && resultArgs != null && resultArgs.DataSource.Sclar.ToInteger == 0)
                            {
                                resultArgs.Message = "Voucher Type is not mapped"; //to move transaction
                            }
                        }

                        if (resultArgs.Success)
                        {
                            CostCenterTable = dtTrans.Rows.IndexOf(drTrans) + "LDR" + LedgerId;
                            resultArgs = GetCostCentreDetails();
                            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                            {
                                DataTable CostCentreInfo = resultArgs.DataSource.Table;
                                CostCentreInfo.TableName = dtTrans.Rows.IndexOf(drTrans) + "LDR" + LedgerId;
                                if (CostCentreInfo != null)
                                {
                                    dsCostCentre.Tables.Add(CostCentreInfo);
                                }
                                this.CostCenterInfo = dsCostCentre;

                                MapProjectCostCenter(CostCentreInfo);
                            }
                        }

                        if (!resultArgs.Success)
                        {
                            //resultArgs.Success = true;
                            break;
                        }
                    }
                }
            }

            if (resultArgs.Success)
            {
                resultArgs = FetchCashBankDetails();
                if (resultArgs.Success)
                {
                    DataTable dtCashTrans = resultArgs.DataSource.Table;
                    this.CashTransInfo = dtCashTrans.DefaultView;

                    if (dtCashTrans != null)
                    {
                        foreach (DataRow drCashTrans in dtCashTrans.Rows)
                        {
                            LedgerId = this.NumberSet.ToInteger(drCashTrans[this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
                            GroupId = this.NumberSet.ToInteger(drCashTrans[this.AppSchema.LedgerGroup.GROUP_IDColumn.ColumnName].ToString());
                            VoucherDefinitionId = this.NumberSet.ToInteger(drCashTrans[this.AppSchema.VoucherNumber.VOUCHER_DEFINITION_IDColumn.ColumnName].ToString());

                            if (GroupId == (int)FixedLedgerGroup.BankAccounts)
                            {
                                resultArgs = CheckLedgerMappedByProject(LedgerId, ProjectId);
                                if (resultArgs.Success && resultArgs != null && resultArgs.DataSource.Sclar.ToInteger > 0)
                                {
                                    resultArgs = MapProjectLedger(LedgerId);
                                }
                                else
                                {
                                    resultArgs.Message = "Bank account is not mapped"; // to move transaction
                                }
                            }
                            else
                            {
                                resultArgs = MapProjectLedger(LedgerId);
                            }

                            //Check Voucher Defintion mapped with moved project
                            if (resultArgs.Success)
                            {
                                resultArgs = CheckVoucherTypeMappedByProject(ProjectId, VoucherDefinitionId);
                                if (resultArgs.Success && resultArgs != null && resultArgs.DataSource.Sclar.ToInteger == 0)
                                {
                                    resultArgs.Message = "Voucher Type is not mapped"; // to move transaction
                                }
                            }
                        }
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs AssignTransaction(MoveTransForm moveForm)
        {
            string Messages = string.Empty;

            if (moveForm == MoveTransForm.Transaction)
            {
                resultArgs = FetchCashBankDetails();
            }
            if (resultArgs.Success)
            {
                DataTable dtCashTrans = resultArgs.DataSource.Table;
                this.CashTransInfo = dtCashTrans.DefaultView;

                if (dtCashTrans != null)
                {
                    foreach (DataRow drCashTrans in dtCashTrans.Rows)
                    {
                        LedgerId = this.NumberSet.ToInteger(drCashTrans[this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
                        GroupId = this.NumberSet.ToInteger(drCashTrans[this.AppSchema.LedgerGroup.GROUP_IDColumn.ColumnName].ToString());
                        VoucherDefinitionId = this.NumberSet.ToInteger(drCashTrans[this.AppSchema.VoucherNumber.VOUCHER_DEFINITION_IDColumn.ColumnName].ToString());

                        if (GroupId == (int)FixedLedgerGroup.BankAccounts)
                        {
                            resultArgs = CheckLedgerMappedByProject(LedgerId, ProjectId);
                            if (resultArgs.Success && resultArgs != null && resultArgs.DataSource.Sclar.ToInteger > 0)
                            {
                                resultArgs = MapProjectLedger(LedgerId);
                            }
                            else
                            {
                                resultArgs.Message = "Bank account is not mapped"; // to move transaction
                            }
                        }
                        else
                        {
                            resultArgs = MapProjectLedger(LedgerId);
                        }

                        //Check Voucher Defintion mapped with moved project
                        if (resultArgs.Success)
                        {
                            resultArgs = CheckVoucherTypeMappedByProject(ProjectId, VoucherDefinitionId);
                            if (resultArgs.Success && resultArgs != null && resultArgs.DataSource.Sclar.ToInteger == 0)
                            {
                                resultArgs.Message = "Voucher Type is not mapped";// to move transaction
                            }
                        }
                    }
                }
            }
            if (resultArgs.Success)
            {
                if (moveForm == MoveTransForm.Transaction)
                {
                    resultArgs = FetchTransDetails();
                }
                else
                {
                    resultArgs = FetchJournalDetails();
                }

                if (resultArgs.Success)
                {
                    DataTable dtTrans = resultArgs.DataSource.Table;
                    this.TransInfo = dtTrans.DefaultView;
                    dsCostCentre.Clear();
                    dsCostCentre.Tables.Clear();
                    if (dtTrans != null)
                    {
                        foreach (DataRow drTrans in dtTrans.Rows)
                        {
                            LedgerId = this.NumberSet.ToInteger(drTrans[this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
                            GroupId = this.NumberSet.ToInteger(drTrans[this.AppSchema.LedgerGroup.GROUP_IDColumn.ColumnName].ToString());
                            VoucherDefinitionId = this.NumberSet.ToInteger(drTrans[this.AppSchema.VoucherNumber.VOUCHER_DEFINITION_IDColumn.ColumnName].ToString());

                            if (GroupId == (int)FixedLedgerGroup.BankAccounts)
                            {
                                resultArgs = CheckLedgerMappedByProject(LedgerId, ProjectId);
                                if (resultArgs.Success && resultArgs != null && resultArgs.DataSource.Sclar.ToInteger > 0)
                                {
                                    resultArgs = MapProjectLedger(LedgerId);
                                }
                                else
                                {
                                    resultArgs.Message = "Bank account is not mapped"; // to move transaction
                                }
                            }
                            else
                            {
                                resultArgs = MapProjectLedger(LedgerId);
                            }

                            //Check Voucher Defintion mapped with moved project
                            if (resultArgs.Success)
                            {
                                resultArgs = CheckVoucherTypeMappedByProject(ProjectId, VoucherDefinitionId);
                                if (resultArgs.Success && resultArgs != null && resultArgs.DataSource.Sclar.ToInteger == 0)
                                {
                                    resultArgs.Message = "Voucher Type is not mapped"; // to move transaction
                                }
                            }

                            if (resultArgs.Success)
                            {
                                CostCenterTable = dtTrans.Rows.IndexOf(drTrans) + "LDR" + LedgerId;
                                resultArgs = GetCostCentreDetails();
                                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                                {
                                    DataTable CostCentreInfo = resultArgs.DataSource.Table;
                                    CostCentreInfo.TableName = dtTrans.Rows.IndexOf(drTrans) + "LDR" + LedgerId;
                                    if (CostCentreInfo != null) { dsCostCentre.Tables.Add(CostCentreInfo); }
                                    this.CostCenterInfo = dsCostCentre;
                                    MapProjectCostCenter(CostCentreInfo);
                                }
                            }
                        }
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs MoveBulkTransactions()
        {
            //ResultArgs resultArgs = null;
            ResultArgs resultArgs = new ResultArgs();
            using (DataManager dataManager = new DataManager())
            {
                foreach (DataRow drTransactions in dtBulkTransactions.Rows)
                {
                    DateTime TransVoucherDate = this.DateSet.ToDate(drTransactions[this.AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName].ToString(), false);
                    VoucherId = this.NumberSet.ToInteger(drTransactions[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString());
                    Narration = drTransactions[this.AppSchema.VoucherMaster.NARRATIONColumn.ColumnName].ToString();
                    NameAddress = drTransactions[this.AppSchema.VoucherMaster.NAME_ADDRESSColumn.ColumnName].ToString();
                    ProjectId = ProjectId;
                    VoucherDate = TransVoucherDate;
                    VoucherType = drTransactions["VOUCHERTYPE"].ToString();
                    Status = 1;
                    VoucherDefinitionId = this.NumberSet.ToInteger(drTransactions[this.AppSchema.VoucherNumber.VOUCHER_DEFINITION_IDColumn.ColumnName].ToString());
                    VoucherNo = drTransactions[this.AppSchema.VoucherMaster.VOUCHER_NOColumn.ColumnName].ToString();
                    Int32 Pid = this.NumberSet.ToInteger(drTransactions[this.AppSchema.VoucherMaster.PROJECT_IDColumn.ColumnName].ToString());

                    if (!this.IsAuditLockedVoucherDate(Pid, VoucherDate))
                    {
                        //resultArgs = CheckLedgerareMappedByVoucher(VoucherId, ProjectId);
                        //if (resultArgs.Success && resultArgs != null && resultArgs.DataSource.Sclar.ToInteger > 0)
                        //{

                        resultArgs = FetchVoucherNumberDefinition();
                        if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            TransVoucherMethod = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Voucher.VOUCHER_METHODColumn.ColumnName].ToString());
                            if (TransVoucherMethod == (int)TransactionVoucherMethod.Automatic)
                            {
                                TransVoucherMethod = TransVoucherMethod;
                            }
                            else
                            {
                                VoucherNo = string.Empty;
                            }
                        }

                        //}
                        //else
                        //{
                        //    resultArgs.Message = "All the Transaction Ledgers are not Mapped to the Selected Project. Map the Ledger and Try again";
                        //}

                        if (resultArgs.Success)
                        {
                            resultArgs = MoveProjectVoucher(MoveTransForm.Transaction, TransVoucherDate, ProjectId, dataManager);
                        }
                    }
                    else
                    {
                        resultArgs.Success = false;
                        string lockedmessage = "Unable to Delete/Modify Vouchers." + System.Environment.NewLine + "Voucher is locked for '" + LockProjectName + "'" +
                            " during the period of " + DateSet.ToDate(LockFromDate.ToShortDateString()) + " - " + DateSet.ToDate(LockToDate.ToShortDateString());
                        resultArgs.Message = lockedmessage;
                        break;
                    }
                }
            }

            return resultArgs;
        }

        private ResultArgs MapProjectCostCenter(DataTable dtCostCentreInfo)
        {
            DataTable dtCostCentre = dtCostCentreInfo;
            using (MappingSystem mapping = new MappingSystem())
            {
                foreach (DataRow drCostCentreInfo in dtCostCentreInfo.Rows)
                {
                    int CostCentreId = 0;
                    string TransMode = string.Empty;
                    CostCentreId = this.NumberSet.ToInteger(drCostCentreInfo[mapping.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName].ToString());

                    mapping.CostCenterId = CostCentreId;
                    mapping.ProjectId = ProjectId;

                    resultArgs = mapping.MapProjectCostCenter();
                    if (!resultArgs.Success)
                    {
                        break;
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs MapProjectLedger(int LedgerId)
        {
            using (MappingSystem mapping = new MappingSystem())
            {
                mapping.LedgerId = LedgerId;
                mapping.ProjectId = ProjectId;

                resultArgs = mapping.MapProjectLedger();
                //if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count == 0)
                //{
                //    resultArgs.Message = "Ledgers are not mapped to this project";
                //    resultArgs.Success = false;
                //}
            }
            return resultArgs;
        }

        public ResultArgs FetchTransDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.FetchTransDetails))
            {
                SetVoucherMethod();
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherId);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.LEDGER_GST_CLASS_IDColumn, this.GSTZeroClassId);
                //   dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.TRANS_MODEColumn, TransMode);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// To Fetch Trans Details while moving the transaction
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchMoveTransDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.FetchMoveTransDetails))
            {
                SetVoucherMethod();
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherId);
                //   dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.TRANS_MODEColumn, TransMode);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            MessageRender.ShowMessage("");
            return resultArgs;
        }

        public ResultArgs FetchJournalDetails()
        {
            ResultArgs resulatJournalArg = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.FetchJournalDetailById))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherId);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.LEDGER_GST_CLASS_IDColumn, this.GSTZeroClassId);

                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.SUB_LEDGER_IDColumn, CashBankModeId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resulatJournalArg = dataManager.FetchData(DataSource.DataTable);
            }

            //On 04/11/2022, to get GST Invoice details by Vocuehrid
            dtGSTInvoiceMasterDetails = null;
            dtGSTInvoiceMasterLedgerDetails = null;
            GST_INVOICE_ID = 0;
            GST_VENDOR_INVOICE_NO = null;
            GST_VENDOR_INVOICE_DATE = null;
            GST_VENDOR_INVOICE_TYPE = 0;
            GST_VENDOR_ID = 0;

            if (resulatJournalArg.Success && this.NumberSet.ToInteger(this.EnableGST.ToString()).Equals((int)SetDefaultValue.DefaultValue) ||
                (this.IsCountryOtherThanIndia || this.AllowMultiCurrency == 1))
            {
                if (resulatJournalArg.Success && resulatJournalArg.DataSource.Table != null && resulatJournalArg.DataSource.Table.Rows.Count > 0)
                {
                    if (this.IncludeGSTVendorInvoiceDetails == "2")
                    {
                        GST_INVOICE_ID = NumberSet.ToInteger(resulatJournalArg.DataSource.Table.Rows[0][AppSchema.GSTInvoiceMaster.GST_INVOICE_IDColumn.ColumnName].ToString());

                        ResultArgs result = FetchGSTInvoiceMasterDetails(GST_INVOICE_ID);
                        if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                        {
                            dtGSTInvoiceMasterDetails = result.DataSource.Table;
                            //GST_INVOICE_ID = NumberSet.ToInteger(dtGSTInvoiceMasterDetails.Rows[0][AppSchema.GSTInvoiceMaster.GST_INVOICE_IDColumn.ColumnName].ToString());

                            //Get GST Invoice Ledger details
                            if (GST_INVOICE_ID > 0)
                            {
                                result = FetchGSTInvoiceMasterLedgersDetails(GST_INVOICE_ID);
                                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                                {
                                    dtGSTInvoiceMasterLedgerDetails = result.DataSource.Table;
                                }
                            }

                            //26/04/2019, for Vendor GST Invoice details
                            GST_VENDOR_INVOICE_NO = dtGSTInvoiceMasterDetails.Rows[0][this.AppSchema.VoucherMaster.GST_VENDOR_INVOICE_NOColumn.ColumnName].ToString();
                            GST_VENDOR_INVOICE_DATE = dtGSTInvoiceMasterDetails.Rows[0][this.AppSchema.VoucherMaster.GST_VENDOR_INVOICE_DATEColumn.ColumnName].ToString();
                            GST_VENDOR_INVOICE_TYPE = this.NumberSet.ToInteger(dtGSTInvoiceMasterDetails.Rows[0][this.AppSchema.VoucherMaster.GST_VENDOR_INVOICE_TYPEColumn.ColumnName].ToString());
                            GST_VENDOR_ID = this.NumberSet.ToInteger(dtGSTInvoiceMasterDetails.Rows[0][this.AppSchema.VoucherMaster.GST_VENDOR_IDColumn.ColumnName].ToString());
                        }
                    }
                }
            }

            if (resulatJournalArg.Success && resulatJournalArg.DataSource.Table != null && resulatJournalArg.DataSource.Table.Rows.Count > 0)
            {
                ContributionAmount = this.NumberSet.ToDecimal(resulatJournalArg.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.CONTRIBUTION_AMOUNTColumn.ColumnName].ToString());
                CurrencyCountryId = this.NumberSet.ToInteger(resulatJournalArg.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.CURRENCY_COUNTRY_IDColumn.ColumnName].ToString());
                ExchangeRate = this.NumberSet.ToDecimal(resulatJournalArg.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.EXCHANGE_RATEColumn.ColumnName].ToString());
                CalculatedAmount = this.NumberSet.ToDecimal(resulatJournalArg.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.CALCULATED_AMOUNTColumn.ColumnName].ToString());
                ActualAmount = this.NumberSet.ToDecimal(resulatJournalArg.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.ACTUAL_AMOUNTColumn.ColumnName].ToString());
                ExchageCountryId = this.NumberSet.ToInteger(resulatJournalArg.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.EXCHANGE_COUNTRY_IDColumn.ColumnName].ToString());
            }

            return resulatJournalArg;
        }

        public ResultArgs FetchCashBankDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.FetchCashBankDetails))
            {
                SetVoucherMethod();
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.TRANS_MODEColumn, CashTransMode);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        //public ResultArgs FetchReferedVoucherById(int ledgerId, int VoucherID)
        //{
        //    using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.FetchReferedVoucherLedgerId))
        //    {
        //        dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherID);
        //        dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.LEDGER_IDColumn, ledgerId);
        //        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
        //        resultArgs = dataManager.FetchData(DataSource.Scalar);
        //    }
        //    return resultArgs;
        //}

        public ResultArgs FetchTransBalance()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.TransOPBalance))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, GroupId);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BALANCE_DATEColumn, BalanceDate);

                if (!string.IsNullOrEmpty(BankClosedDate))
                {
                    dataManager.Parameters.Add(this.AppSchema.BankAccount.DATE_CLOSEDColumn, BankClosedDate);
                }

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchFDOpBalance()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.FetchFDOPBalance))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, GroupId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchTransClosingBalance()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.TransCBBalance))
            {
                if (ProjectId > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                }

                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, GroupId);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BALANCE_DATEColumn, BalanceDate);

                if (!string.IsNullOrEmpty(BankClosedDate))
                {
                    dataManager.Parameters.Add(this.AppSchema.BankAccount.DATE_CLOSEDColumn, BankClosedDate);
                }

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchTransNegativeClosingBalance()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.TransCBNegativeBalance))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, GroupId);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BALANCE_DATEColumn, BalanceDate);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchTransFDClosingBalance()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.TransFDCBalance))
            {
                //dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);

                if (ProjectId > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                }

                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, GroupId);
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.BALANCE_DATEColumn, BalanceDate);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchCostCentreLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.IsCostCenterLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchInKindLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.IsInkindLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);

                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        public ResultArgs FetchIsTDSLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.IsTDSLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchIsGStLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.IsGStLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchIsIGSTApplicable()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.IsIGSTLedgerApplied))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(this.AppSchema.State.STATE_NAMEColumn, "%" + State + "%");
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs FetchVoucherTransactionDetails()
        {
            using (DataManager dataManger = new DataManager(SQLCommand.VoucherTransDetails.FetchAll))
            {
                resultArgs = dataManger.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }




        private void CloseFixedDeposit()
        {
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                ledgerSystem.LedgerId = LedgerId;
                int FDGroup = ledgerSystem.FetchLedgerGroupById();
                if (FDGroup == (int)FixedLedgerGroup.FixedDeposit) { UpdateFDStatus(); }
            }
        }

        public ResultArgs ConstructVoucherData(int LedgerId, decimal Amount, string cheque = "", string maton = "", decimal exchangerate = 1, decimal liveexchangrerate = 1)
        {
            ResultArgs resultArgs = new ResultArgs();
            resultArgs.Success = true;
            DataTable dtTrans = new DataTable("TransInfo");
            try
            {
                dtTrans.Columns.Add("LEDGER_ID", typeof(Int32));
                dtTrans.Columns.Add("AMOUNT", typeof(decimal));
                dtTrans.Columns.Add("CHEQUE_NO", typeof(string));
                dtTrans.Columns.Add("MATERIALIZED_ON", typeof(string));

                dtTrans.Columns.Add(this.AppSchema.VoucherTransaction.EXCHANGE_RATEColumn.ColumnName, typeof(decimal));
                dtTrans.Columns.Add(this.AppSchema.VoucherTransaction.LIVE_EXCHANGE_RATEColumn.ColumnName, typeof(decimal));

                DataRow drTrans = dtTrans.NewRow();
                drTrans["LEDGER_ID"] = LedgerId;
                drTrans["AMOUNT"] = Amount;
                drTrans["CHEQUE_NO"] = cheque;
                drTrans["MATERIALIZED_ON"] = maton;

                drTrans[this.AppSchema.VoucherTransaction.EXCHANGE_RATEColumn.ColumnName] = exchangerate;
                drTrans[this.AppSchema.VoucherTransaction.LIVE_EXCHANGE_RATEColumn.ColumnName] = liveexchangrerate;

                dtTrans.Rows.Add(drTrans);
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                resultArgs.DataSource.Data = dtTrans;
            }
            return resultArgs;
        }

        public ResultArgs ConstructVoucherData(int LedgerId, decimal Amount, string CheqNo, DateTime MaterialisedOn, decimal exchangerate = 1, decimal liveexchangrerate = 1)
        {
            ResultArgs resultArgs = new ResultArgs();
            resultArgs.Success = true;
            DataTable dtTrans = new DataTable("TransInfo");
            try
            {
                dtTrans.Columns.Add(this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName, typeof(Int32));
                dtTrans.Columns.Add(this.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName, typeof(decimal));
                dtTrans.Columns.Add(this.AppSchema.VoucherTransaction.CHEQUE_NOColumn.ColumnName, typeof(string));
                dtTrans.Columns.Add(this.AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName, typeof(DateTime));
                dtTrans.Columns.Add(this.AppSchema.VoucherTransaction.EXCHANGE_RATEColumn.ColumnName, typeof(decimal));
                dtTrans.Columns.Add(this.AppSchema.VoucherTransaction.LIVE_EXCHANGE_RATEColumn.ColumnName, typeof(decimal));

                DataRow drTrans = dtTrans.NewRow();
                drTrans[this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName] = LedgerId;
                drTrans[this.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName] = Amount;
                drTrans[this.AppSchema.VoucherTransaction.CHEQUE_NOColumn.ColumnName] = CheqNo;
                drTrans[this.AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName] = MaterialisedOn;

                drTrans[this.AppSchema.VoucherTransaction.EXCHANGE_RATEColumn.ColumnName] = exchangerate;
                drTrans[this.AppSchema.VoucherTransaction.LIVE_EXCHANGE_RATEColumn.ColumnName] = liveexchangrerate;

                dtTrans.Rows.Add(drTrans);
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                resultArgs.DataSource.Data = dtTrans;
            }
            return resultArgs;
        }

        public ResultArgs AttachVoucherFiles(int VoucherId, string FileName, string ActualFileName, byte[] newAttachVoucherImage, string Remark, string VoucherRefPath)
        {
            ResultArgs resultArgs = new ResultArgs();

            try
            {
                if (dtVoucherFiles.Columns.Count == 0)
                {
                    dtVoucherFiles.Columns.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName, typeof(Int32));
                    dtVoucherFiles.Columns.Add(this.AppSchema.VoucherMaster.FILE_NAMEColumn.ColumnName, typeof(string));
                    dtVoucherFiles.Columns.Add(this.AppSchema.VoucherMaster.ACTUAL_FILE_NAMEColumn.ColumnName, typeof(string));
                    dtVoucherFiles.Columns.Add(this.AppSchema.VoucherMaster.VOUCHER_FILEColumn.ColumnName, typeof(byte[]));
                    dtVoucherFiles.Columns.Add(this.AppSchema.VoucherMaster.REMARKColumn.ColumnName, typeof(string));
                    dtVoucherFiles.Columns.Add(this.AppSchema.VoucherMaster.VOUCHER_FILE_REF_PATHColumn.ColumnName, typeof(string));
                }

                //Bitmap newAttachImage = new Bitmap(image);
                //byte[] newAttachVoucherImage = null;
                //if (newAttachVoucherImage != null)
                //{
                //    newAttachVoucherImage = ImageProcessing.ImageToByteArray(VoucherImage as Bitmap);
                //}

                DataRow drVoucherIamgeRow = dtVoucherFiles.NewRow();
                drVoucherIamgeRow[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName] = VoucherId;
                drVoucherIamgeRow[this.AppSchema.VoucherMaster.FILE_NAMEColumn.ColumnName] = FileName;
                drVoucherIamgeRow[this.AppSchema.VoucherMaster.ACTUAL_FILE_NAMEColumn.ColumnName] = ActualFileName;
                drVoucherIamgeRow[this.AppSchema.VoucherMaster.VOUCHER_FILEColumn.ColumnName] = newAttachVoucherImage;
                drVoucherIamgeRow[this.AppSchema.VoucherMaster.REMARKColumn.ColumnName] = Remark;
                drVoucherIamgeRow[this.AppSchema.VoucherMaster.VOUCHER_FILE_REF_PATHColumn.ColumnName] = VoucherRefPath;

                dtVoucherFiles.Rows.Add(drVoucherIamgeRow);
                resultArgs.DataSource.Data = dtVoucherFiles;
                resultArgs.Success = true;
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            return resultArgs;
        }

        /// <summary>
        /// Compressing Bitmap images 
        /// </summary>
        /// <param name="image">UnCompress Bitmap Image</param>
        /// <returns>Compressed Bitmap Image</returns>
        //  Added by Carmel Raj M on July-07-2015
        private Bitmap CompressImage(Bitmap image)
        {
            long ImageSize;
            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;

            EncoderParameters encoderParameters = new EncoderParameters(1);
            EncoderParameter encoderParameter = new EncoderParameter(myEncoder, 50L);
            encoderParameters.Param[0] = encoderParameter;
            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, jpgEncoder, encoderParameters);
            ImageSize = memoryStream.Length;
            image = (Bitmap)Image.FromStream(memoryStream);
            ImageSize = memoryStream.Length;
            return image;
        }

        //  Added by Carmel Raj M on July-07-2015
        private ImageCodecInfo GetEncoder(ImageFormat Format)
        {
            ImageCodecInfo Encoder = null;
            ImageCodecInfo[] Codec = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in Codec)
            {
                if (codec.FormatID == Format.Guid)
                {
                    Encoder = codec;
                }
            }
            return Encoder;
        }

        private ResultArgs SaveTransactionDetails(DataManager dm)
        {
            try
            {
                DataTable dtTransInfo = this.TransInfo.ToTable();
                int Count = 1;
                SubLedgerSequenceNo = 1;
                if (dtTransInfo != null && dtTransInfo.Rows.Count > 0)
                {
                    foreach (DataRow drTrans in dtTransInfo.Rows)
                    {
                        Amount = 0;
                        //Journal Trasaction Save
                        if (VoucherType == "JN")
                        {
                            LedgerId = this.NumberSet.ToInteger(drTrans["LEDGER_ID"].ToString());
                            DenominationLedgerID = this.NumberSet.ToInteger(drTrans["LEDGER_ID"].ToString());

                            if (this.NumberSet.ToDecimal(drTrans["DEBIT"].ToString()) > 0)
                            {
                                Amount = this.NumberSet.ToDecimal(drTrans["DEBIT"].ToString());
                                TransMode = "DR";
                            }
                            else if (this.NumberSet.ToDecimal(drTrans["CREDIT"].ToString()) > 0)
                            {
                                Amount = this.NumberSet.ToDecimal(drTrans["CREDIT"].ToString());
                                TransMode = "CR";
                            }

                            if (VoucherSubType != ledgerSubType.FD.ToString() && VoucherSubType != ledgerSubType.TDS.ToString())  // && VoucherSubType != VoucherSubTypes.AST.ToString()
                            {
                                CostCenterTable = Count - 1 + "LDR" + LedgerId;
                                if (this.HasCostCentre(CostCenterTable))
                                {
                                    int ledgersequenceno = SequenceNo + 1;
                                    resultArgs = SaveCostCentreInfo(ledgersequenceno);
                                }
                            }
                            if (VoucherSubType != ledgerSubType.FD.ToString() && VoucherSubType != ledgerSubType.TDS.ToString() && VoucherSubType != VoucherSubTypes.AST.ToString())// && VoucherSubType != VoucherSubTypes.c.ToString()) 
                            {
                                DenominationTable = Count - 1 + "LDR" + DenominationLedgerID;
                                if (this.HasDenomination(DenominationTable)) { resultArgs = SaveDenomination(); }
                            }
                            if (dtTransInfo.Columns.Contains("REFERENCE_NUMBER"))
                            {
                                if (!string.IsNullOrEmpty(drTrans["REFERENCE_NUMBER"].ToString()))
                                {
                                    ReferenceNo = drTrans["REFERENCE_NUMBER"].ToString();
                                }
                                else
                                {
                                    ReferenceNo = null;
                                }
                            }

                            SequenceNo = Count++;

                            GSt = (dtTransInfo.Columns.Contains(this.AppSchema.VoucherTransaction.GSTColumn.ColumnName)) ? this.NumberSet.ToDecimal(drTrans[this.AppSchema.VoucherTransaction.GSTColumn.ColumnName].ToString()) : 0;
                            CGSt = (dtTransInfo.Columns.Contains(this.AppSchema.VoucherTransaction.CGSTColumn.ColumnName)) ? this.NumberSet.ToDecimal(drTrans[this.AppSchema.VoucherTransaction.CGSTColumn.ColumnName].ToString()) : 0;
                            SGSt = (dtTransInfo.Columns.Contains(this.AppSchema.VoucherTransaction.SGSTColumn.ColumnName)) ? this.NumberSet.ToDecimal(drTrans[this.AppSchema.VoucherTransaction.SGSTColumn.ColumnName].ToString()) : 0;
                            IGSt = (dtTransInfo.Columns.Contains(this.AppSchema.VoucherTransaction.IGSTColumn.ColumnName)) ? this.NumberSet.ToDecimal(drTrans[this.AppSchema.VoucherTransaction.IGSTColumn.ColumnName].ToString()) : 0;

                            //On 28/11/2019, Ledger GST Classification id
                            LedgerGSTClassID = (dtTransInfo.Columns.Contains(this.AppSchema.VoucherTransaction.LEDGER_GST_CLASS_IDColumn.ColumnName)) ? this.NumberSet.ToInteger(drTrans[this.AppSchema.VoucherTransaction.LEDGER_GST_CLASS_IDColumn.ColumnName].ToString()) : 0;

                            if (LedgerGSTClassID > 0 && NumberSet.ToInteger(this.EnableGST).Equals((int)YesNo.No)) //28/12/2019, to check applicable from 
                            {
                                LedgerGSTClassID = 0;
                            }


                            //26/02/2020, if default gst class is assigned, make it as null gst
                            if (LedgerGSTClassID == this.GSTZeroClassId && NumberSet.ToInteger(this.EnableGST).Equals((int)YesNo.Yes))
                            {
                                LedgerGSTClassID = 0;
                            }

                            LedgerGSTClassID = (LedgerGSTClassID == 0 ? null : LedgerGSTClassID);

                            // 06/02/2025, to Save the Cheque and Materilized Features
                            ChequeNo = (dtTransInfo.Columns.Contains(this.AppSchema.VoucherTransaction.CHEQUE_NOColumn.ColumnName)) ?
                               drTrans[this.AppSchema.VoucherTransaction.CHEQUE_NOColumn.ColumnName].ToString() : string.Empty;
                            MaterializedOn = (dtTransInfo.Columns.Contains(this.AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName)) ?
                                drTrans[this.AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName].ToString() != null ?
                                drTrans[this.AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName].ToString() : string.Empty : string.Empty;

                            Int32 LedgerGrpId = GetLedgerGroupId(LedgerId);
                            //On 06.02.2025, to get Cheque reference details (Cheque date, cheque bank name, cheque branch)
                            ChequeRefDate = string.Empty;
                            ChequeRefBankName = string.Empty;
                            ChequeRefBankBranch = string.Empty;
                            ChequeRefFundTransfer = string.Empty;

                            if (LedgerGrpId == (int)FixedLedgerGroup.BankAccounts && !string.IsNullOrEmpty(ChequeNo))
                            {
                                ChequeRefDate = (dtTransInfo.Columns.Contains(this.AppSchema.VoucherTransaction.CHEQUE_REF_DATEColumn.ColumnName)) ?
                                                drTrans[this.AppSchema.VoucherTransaction.CHEQUE_REF_DATEColumn.ColumnName].ToString() : string.Empty;
                                ChequeRefBankName = (dtTransInfo.Columns.Contains(this.AppSchema.VoucherTransaction.CHEQUE_REF_BANKNAMEColumn.ColumnName)) ?
                                                drTrans[this.AppSchema.VoucherTransaction.CHEQUE_REF_BANKNAMEColumn.ColumnName].ToString() : string.Empty;
                                ChequeRefBankBranch = (dtTransInfo.Columns.Contains(this.AppSchema.VoucherTransaction.CHEQUE_REF_BRANCHColumn.ColumnName)) ?
                                                drTrans[this.AppSchema.VoucherTransaction.CHEQUE_REF_BRANCHColumn.ColumnName].ToString() : string.Empty;
                                ChequeRefFundTransfer = (dtTransInfo.Columns.Contains(this.AppSchema.VoucherTransaction.FUND_TRANSFER_TYPE_NAMEColumn.ColumnName)) ?
                                               drTrans[this.AppSchema.VoucherTransaction.FUND_TRANSFER_TYPE_NAMEColumn.ColumnName].ToString() : string.Empty;
                            }

                            MultiNarration = drTrans["NARRATION"].ToString();  // Added By Praveen to save Narration in the VoucherTrans table
                        }
                        else
                        {
                            LedgerId = FDLedgerId = this.NumberSet.ToInteger(drTrans[this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
                            Int32 LedgerGrpId = GetLedgerGroupId(LedgerId);
                            Amount = this.NumberSet.ToDecimal(drTrans[this.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString());
                            if (FDType == FDTypes.IN.ToString() || FDType == FDTypes.RN.ToString() || FDTypes.WD.ToString() == FDType)
                            {
                                TransMode = drTrans[this.AppSchema.VoucherTransaction.TRANS_MODEColumn.ColumnName].ToString();
                            }
                            else if (dtTransInfo.Columns.Contains(this.AppSchema.VoucherTransaction.SOURCEColumn.ColumnName))
                            {
                                TransMode = this.NumberSet.ToInteger(drTrans[this.AppSchema.VoucherTransaction.SOURCEColumn.ColumnName].ToString()) == (int)Source.To ? "CR" : "DR";  //TransMode;;
                            }
                            ChequeNo = (dtTransInfo.Columns.Contains(this.AppSchema.VoucherTransaction.CHEQUE_NOColumn.ColumnName)) ?
                                drTrans[this.AppSchema.VoucherTransaction.CHEQUE_NOColumn.ColumnName].ToString() : string.Empty;
                            MaterializedOn = (dtTransInfo.Columns.Contains(this.AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName)) ?
                                drTrans[this.AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName].ToString() != null ?
                                drTrans[this.AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName].ToString() : string.Empty : string.Empty;
                            LedgerFlag = string.Empty;
                            CostCenterTable = Count - 1 + "LDR" + LedgerId;
                            DenominationTable = Count - 1 + "LDR" + DenominationLedgerID;
                            SequenceNo = Count++;

                            GSt = (dtTransInfo.Columns.Contains(this.AppSchema.VoucherTransaction.GSTColumn.ColumnName)) ? this.NumberSet.ToDecimal(drTrans[this.AppSchema.VoucherTransaction.GSTColumn.ColumnName].ToString()) : 0;
                            CGSt = (dtTransInfo.Columns.Contains(this.AppSchema.VoucherTransaction.CGSTColumn.ColumnName)) ? this.NumberSet.ToDecimal(drTrans[this.AppSchema.VoucherTransaction.CGSTColumn.ColumnName].ToString()) : 0;
                            SGSt = (dtTransInfo.Columns.Contains(this.AppSchema.VoucherTransaction.SGSTColumn.ColumnName)) ? this.NumberSet.ToDecimal(drTrans[this.AppSchema.VoucherTransaction.SGSTColumn.ColumnName].ToString()) : 0;
                            IGSt = (dtTransInfo.Columns.Contains(this.AppSchema.VoucherTransaction.IGSTColumn.ColumnName)) ? this.NumberSet.ToDecimal(drTrans[this.AppSchema.VoucherTransaction.IGSTColumn.ColumnName].ToString()) : 0;

                            //On 28/11/2019, Ledger GST Classification id
                            LedgerGSTClassID = (dtTransInfo.Columns.Contains(this.AppSchema.VoucherTransaction.LEDGER_GST_CLASS_IDColumn.ColumnName)) ? this.NumberSet.ToInteger(drTrans[this.AppSchema.VoucherTransaction.LEDGER_GST_CLASS_IDColumn.ColumnName].ToString()) : 0;

                            if (LedgerGSTClassID > 0 && NumberSet.ToInteger(this.EnableGST).Equals((int)YesNo.No)) //28/12/2019, to check applicable from 
                            {
                                LedgerGSTClassID = 0;
                            }


                            //26/02/2020, if default gst class is assigned, make it as null gst
                            if (LedgerGSTClassID == this.GSTZeroClassId && NumberSet.ToInteger(this.EnableGST).Equals((int)YesNo.Yes))
                            {
                                LedgerGSTClassID = 0;
                            }

                            LedgerGSTClassID = (LedgerGSTClassID == 0 ? null : LedgerGSTClassID);

                            //On 17/10/2017, to get Cheque reference details (Cheque date, cheque bank name, cheque branch)
                            ChequeRefDate = string.Empty;
                            ChequeRefBankName = string.Empty;
                            ChequeRefBankBranch = string.Empty;
                            ChequeRefFundTransfer = string.Empty;

                            if (LedgerGrpId == (int)FixedLedgerGroup.BankAccounts && !string.IsNullOrEmpty(ChequeNo))
                            {
                                ChequeRefDate = (dtTransInfo.Columns.Contains(this.AppSchema.VoucherTransaction.CHEQUE_REF_DATEColumn.ColumnName)) ?
                                                drTrans[this.AppSchema.VoucherTransaction.CHEQUE_REF_DATEColumn.ColumnName].ToString() : string.Empty;
                                ChequeRefBankName = (dtTransInfo.Columns.Contains(this.AppSchema.VoucherTransaction.CHEQUE_REF_BANKNAMEColumn.ColumnName)) ?
                                                drTrans[this.AppSchema.VoucherTransaction.CHEQUE_REF_BANKNAMEColumn.ColumnName].ToString() : string.Empty;
                                ChequeRefBankBranch = (dtTransInfo.Columns.Contains(this.AppSchema.VoucherTransaction.CHEQUE_REF_BRANCHColumn.ColumnName)) ?
                                                drTrans[this.AppSchema.VoucherTransaction.CHEQUE_REF_BRANCHColumn.ColumnName].ToString() : string.Empty;
                                ChequeRefFundTransfer = (dtTransInfo.Columns.Contains(this.AppSchema.VoucherTransaction.FUND_TRANSFER_TYPE_NAMEColumn.ColumnName)) ?
                                               drTrans[this.AppSchema.VoucherTransaction.FUND_TRANSFER_TYPE_NAMEColumn.ColumnName].ToString() : string.Empty;
                            }

                            // string values2 = values.Split('+'));
                            //string values3 = values.s('+')[0];
                            if (VoucherSubType != ledgerSubType.FD.ToString() && VoucherSubType != ledgerSubType.TDS.ToString()) // && VoucherSubType != VoucherSubTypes.AST.ToString()
                            {
                                if (this.HasCostCentre(CostCenterTable)) { resultArgs = SaveCostCentreInfo(SequenceNo); }
                            }

                            //if (VoucherType == "PY" || VoucherType == DefaultVoucherTypes.Payment.ToString())
                            //{
                            //    resultArgs = SaveReferenceNo();
                            //}

                            if (VoucherSubType != ledgerSubType.FD.ToString() && VoucherSubType != ledgerSubType.TDS.ToString() && VoucherSubType != VoucherSubTypes.AST.ToString())// && VoucherSubType != VoucherSubTypes.c.ToString()) 
                            {
                                if (this.HasDenomination(DenominationTable)) { resultArgs = SaveDenomination(); }
                            }

                            if (dtTransInfo.Columns.Contains("NARRATION"))    // chinna on 09.12.2019

                                MultiNarration = drTrans["NARRATION"].ToString();  // On 28/08/2019, to get ledger narration
                        }

                        //12/11/2024, For Ledger based currency details
                        LedgerExchangeRate = LedgerLiveExchangeRate = LedgerActualAmount = 0;
                        if (this.AllowMultiCurrency == 1)
                        {
                            LedgerExchangeRate = NumberSet.ToDecimal(drTrans[this.AppSchema.VoucherTransaction.EXCHANGE_RATEColumn.ColumnName].ToString());
                            LedgerLiveExchangeRate = NumberSet.ToDecimal(drTrans[this.AppSchema.VoucherTransaction.LIVE_EXCHANGE_RATEColumn.ColumnName].ToString());
                            //To have common actual amount for all ledgers for local currency, so we take voucher exchange rate
                            //if and if only ledger balance will be tallied 
                            LedgerActualAmount = (Amount * LedgerExchangeRate); //(LedgerExchangeRate * Amount);
                        }

                        resultArgs = SaveVoucherTransactionDetails(dm);

                        //10/02/2020, To Save Ledger Sub Ledgers Vouchers
                        if (resultArgs.Success && VoucherSubType != ledgerSubType.FD.ToString() && VoucherSubType != ledgerSubType.TDS.ToString())
                        {

                            //14/09/2023, Journal GST Invoice Booking agains Receipt and Payment
                            if (BookingGSTInvoiceId > 0 && this.NumberSet.ToInteger(this.IncludeGSTVendorInvoiceDetails) == 2)
                            {
                                resultArgs = UpdateGSTVoucherAgainsInvoice(dm, VoucherId, BookingGSTInvoiceId, Amount);
                            }

                            if (this.IS_DIOMYS_DIOCESE && EnableSubLedgerVouchers == "1" && LedgerId > 0)
                            {
                                resultArgs = SaveSubLedgersVouchers(dm);
                            }
                        }

                        if (!resultArgs.Success) { break; }
                    }
                }

                if (this.CashTransInfo != null && VoucherType != "JN" && resultArgs.Success)
                {
                    DataTable dvCashTransInfo = this.CashTransInfo.ToTable();
                    foreach (DataRow drTrans in dvCashTransInfo.Rows)
                    {
                        LedgerId = this.NumberSet.ToInteger(drTrans[this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
                        Int32 LedgerGrpId = GetLedgerGroupId(LedgerId);
                        Amount = this.NumberSet.ToDecimal(drTrans[this.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString());
                        ChequeNo = (dtTransInfo.Columns.Contains(this.AppSchema.VoucherTransaction.CHEQUE_NOColumn.ColumnName)) ?
                            drTrans[this.AppSchema.VoucherTransaction.CHEQUE_NOColumn.ColumnName].ToString() : string.Empty;
                        // This is to written to replace the \, ' symbols to empty (12PM). Chinna
                        ChequeNo = ChequeNo.Replace("\\", "").Replace("'", "");
                        MaterializedOn = (dtTransInfo.Columns.Contains(this.AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName)) ?
                            drTrans[this.AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName].ToString() != null ?
                            drTrans[this.AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName].ToString() : string.Empty : string.Empty;

                        //On 17/10/2017, to get Cheque reference details (Cheque date, cheque bank name, cheque branch)
                        ChequeRefDate = string.Empty;
                        ChequeRefBankName = string.Empty;
                        ChequeRefBankBranch = string.Empty;
                        ChequeRefFundTransfer = string.Empty;
                        if (LedgerGrpId == (int)FixedLedgerGroup.BankAccounts && !string.IsNullOrEmpty(ChequeNo))
                        {
                            ChequeRefDate = (dtTransInfo.Columns.Contains(this.AppSchema.VoucherTransaction.CHEQUE_REF_DATEColumn.ColumnName)) ?
                                            drTrans[this.AppSchema.VoucherTransaction.CHEQUE_REF_DATEColumn.ColumnName].ToString() : string.Empty;
                            ChequeRefBankName = (dtTransInfo.Columns.Contains(this.AppSchema.VoucherTransaction.CHEQUE_REF_BANKNAMEColumn.ColumnName)) ?
                                            drTrans[this.AppSchema.VoucherTransaction.CHEQUE_REF_BANKNAMEColumn.ColumnName].ToString() : string.Empty;
                            ChequeRefBankBranch = (dtTransInfo.Columns.Contains(this.AppSchema.VoucherTransaction.CHEQUE_REF_BRANCHColumn.ColumnName)) ?
                                            drTrans[this.AppSchema.VoucherTransaction.CHEQUE_REF_BRANCHColumn.ColumnName].ToString() : string.Empty;
                            ChequeRefFundTransfer = (dtTransInfo.Columns.Contains(this.AppSchema.VoucherTransaction.FUND_TRANSFER_TYPE_NAMEColumn.ColumnName)) ?
                                          drTrans[this.AppSchema.VoucherTransaction.FUND_TRANSFER_TYPE_NAMEColumn.ColumnName].ToString() : string.Empty;

                            //if (!string.IsNullOrEmpty(ChequeRefFundTransfer))
                            //{
                            //    ChequeRefFundTransfer = ChequeRefFundTransfer.Equals("Select") ? null : ChequeRefFundTransfer;
                            //}

                        }

                        //12/11/2024, For Ledger based currency details
                        LedgerExchangeRate = LedgerLiveExchangeRate = LedgerActualAmount = 0;
                        if (this.AllowMultiCurrency == 1)
                        {
                            LedgerExchangeRate = NumberSet.ToDecimal(drTrans[this.AppSchema.VoucherTransaction.EXCHANGE_RATEColumn.ColumnName].ToString());
                            LedgerLiveExchangeRate = NumberSet.ToDecimal(drTrans[this.AppSchema.VoucherTransaction.LIVE_EXCHANGE_RATEColumn.ColumnName].ToString());
                            LedgerActualAmount = (Amount * LedgerExchangeRate); //(LedgerExchangeRate * Amount);
                        }

                        LedgerFlag = string.Empty;
                        TransMode = CashTransMode;
                        IdentityFlag = 2;
                        SequenceNo = Count++;

                        resultArgs = SaveVoucherTransactionDetails(dm);

                        if (!resultArgs.Success) { break; }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        //public ResultArgs UpdateFDDetails(BankAccountSystem fdUpdation)
        //{
        //    using (DataManager dataManager = new DataManager())
        //    {
        //        using (BankAccountSystem fd = new BankAccountSystem())
        //        {
        //            fdUpdation.BankAccountId = new LedgerSystem().FetchBankAccountById(LedgerId);
        //            fdUpdation.TransMode = "TR";
        //            fdUpdation.InvestedOn = VoucherDate;
        //            resultArgs = fdUpdation.UpdateTransFD(dataManager, FDAccountNo);
        //        }
        //    }
        //    return resultArgs;
        //}

        public ResultArgs SaveVoucherTransactionDetails(DataManager dm)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.Add))  //((VoucherId == 0) ? SQLCommand.VoucherTransDetails.Add : SQLCommand.VoucherTransDetails.Edit))
                {
                    dataManager.Database = dm.Database;
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherId);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.SEQUENCE_NOColumn, SequenceNo);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.LEDGER_IDColumn, LedgerId);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.AMOUNTColumn, Amount);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.TRANS_MODEColumn, TransMode);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.LEDGER_FLAGColumn, LedgerFlag);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.CHEQUE_NOColumn, ChequeNo);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.MATERIALIZED_ONColumn, MaterializedOn);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.STATUSColumn, Status);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.NARRATIONColumn, MultiNarration); // Added By Praveen to save Multi Narration
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.GSTColumn, GSt);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.CGSTColumn, CGSt);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.SGSTColumn, SGSt);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.IGSTColumn, IGSt);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.REFERENCE_NUMBERColumn, ReferenceNo);
                    //On 17/10/2017, to get Cheque reference details (Cheque date, cheque bank name, cheque branch)
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.CHEQUE_REF_DATEColumn, ChequeRefDate);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.CHEQUE_REF_BANKNAMEColumn, ChequeRefBankName);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.CHEQUE_REF_BRANCHColumn, ChequeRefBankBranch);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.FUND_TRANSFER_TYPE_NAMEColumn, ChequeRefFundTransfer);

                    //28/11/2019, for Ledger GST Class Id
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.LEDGER_GST_CLASS_IDColumn, LedgerGSTClassID);

                    //12/11/2024, To set Ledger Currency detaisl
                    if (this.AllowMultiCurrency == 0)
                    {
                        LedgerExchangeRate = LedgerLiveExchangeRate = LedgerActualAmount = 0;
                    }

                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.EXCHANGE_RATEColumn, LedgerExchangeRate);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.LIVE_EXCHANGE_RATEColumn, LedgerLiveExchangeRate);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.ACTUAL_AMOUNTColumn, LedgerActualAmount);

                    //dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        private ResultArgs FetchTransactionDetailsById(int VoucherID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.FetchTransactionByID))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherID);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    VoucherId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherTransaction.VOUCHER_IDColumn.ColumnName].ToString());
                    SequenceNo = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherTransaction.SEQUENCE_NOColumn.ColumnName].ToString());
                    LedgerId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
                    Amount = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString());
                    TransMode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherTransaction.TRANS_MODEColumn.ColumnName].ToString();
                    LedgerFlag = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherTransaction.LEDGER_FLAGColumn.ColumnName].ToString();
                    ChequeNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherTransaction.CHEQUE_NOColumn.ColumnName].ToString();
                    if (resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName] != DBNull.Value)
                    {
                        MaterializedOn = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName].ToString();
                    }
                    Status = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherTransaction.STATUSColumn.ColumnName].ToString());
                }
            }
            return resultArgs;
        }

        private ResultArgs FetchTransVoucherDetails(string VoucherId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchJournalTransDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 05/07/2018, Check transaction is avilable upto given date for particular project
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="DateClosed"></param>
        /// <returns></returns>
        public ResultArgs CheckTransVoucherDetailsByDateProject(Int32 ProjectId, DateTime VoucherDate)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.CheckTransExistsByDateProject))
            {
                dataManager.Parameters.Add(this.AppSchema.Voucher.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_DATEColumn, VoucherDate);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 26/11/2024 to get number of active voucehrs in current database
        /// </summary>
        /// <returns></returns>
        public Int32 FetchActiveVouchersCount()
        {
            Int32 rtn = 0;
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchActiveVoucherCounts))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }

            if (resultArgs.Success)
            {
                rtn = resultArgs.DataSource.Sclar.ToInteger;
            }
            return rtn;
        }
        #endregion

        #region Voucher CostCentre Methods
        public ResultArgs FetchVoucherCostCentre()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.MasterTransactionCostCentre.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataView);
            }
            return resultArgs;
        }

        public ResultArgs GetCostCentreDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.MasterTransactionCostCentre.FetchCostCentreByLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.VOUCHER_IDColumn, VoucherId);
                dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.COST_CENTRE_TABLEColumn, CostCenterTable);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveVoucherCostCentre(Int32 LedgerSequenceNo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.MasterTransactionCostCentre.Add))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.COST_CENTRE_TABLEColumn, CostCenterTable);
                dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.VOUCHER_IDColumn, VoucherId);
                dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.COST_CENTRE_IDColumn, CostCenterId);
                dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.AMOUNTColumn, CostCentreAmount);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.SEQUENCE_NOColumn, CostCentreSequenceNo);
                //On 22/03/2019, to update ledger sequence no from voucher tranas (CR/DR same ledger in same vouhcer)
                //To differenant Trans mode
                dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.LEDGER_SEQUENCE_NOColumn, LedgerSequenceNo);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveReferenceNumber(DataManager dm)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.MasterTransactionCostCentre.AddReferenceNo))
            {
                dataManager.Database = dm.Database;
                dataManager.Parameters.Add(this.AppSchema.VoucherReference.REC_PAY_VOUCHER_IDColumn, REC_PAY_VOUCHER_ID);
                dataManager.Parameters.Add(this.AppSchema.VoucherReference.LEDGER_IDColumn, RefLEDGER_ID);
                dataManager.Parameters.Add(this.AppSchema.VoucherReference.AMOUNTColumn, RefAMOUNT);
                dataManager.Parameters.Add(this.AppSchema.VoucherReference.REF_VOUCHER_IDColumn, REF_VOUCHER_ID);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveVoucherDenomination()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Denomination.Add))
            {
                dataManager.Parameters.Add(this.AppSchema.Denomination.DENOMINATION_IDColumn, DenominationID);
                dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.VOUCHER_IDColumn, VoucherId);
                dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.LEDGER_IDColumn, DenominationLedgerID);
                dataManager.Parameters.Add(this.AppSchema.Denomination.COUNTColumn, Count);
                dataManager.Parameters.Add(this.AppSchema.Denomination.AMOUNTColumn, DenominationAmount);
                dataManager.Parameters.Add(this.AppSchema.Denomination.SEQUENCE_IDColumn, DenominationSequnce);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteDenomination()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Denomination.DeleteDenomination))
            {
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        public ResultArgs SaveVoucherCostCentre(DataManager CostCenterDataManager)
        {
            resultArgs = MakeLedgerCostCentre(CostCenterDataManager);
            if (resultArgs != null && resultArgs.Success)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.MasterTransactionCostCentre.Add))
                {
                    dataManager.Database = CostCenterDataManager.Database;
                    dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.COST_CENTRE_TABLEColumn, CostCenterTable);
                    dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.VOUCHER_IDColumn, VoucherId);
                    dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.LEDGER_IDColumn, LedgerId);
                    dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.COST_CENTRE_IDColumn, CostCenterId);
                    dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.AMOUNTColumn, CostCentreAmount);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.SEQUENCE_NOColumn, SequenceNo);
                    resultArgs = dataManager.UpdateData();
                }
            }
            return resultArgs;
        }

        private ResultArgs MakeLedgerCostCentre(DataManager CostCenterDataManager)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.MasterTransactionCostCentre.MakeAsCostCenterLedger))
            {
                dataManager.Database = CostCenterDataManager.Database;
                dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        //private ResultArgs SaveVoucherFDInterest(DataManager voucherDataManager)
        //{
        //    using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.VoucherFDInterestAdd))
        //    {
        //        dataManager.Parameters.Add(this.AppSchema.VoucherFDInterest.FD_VOUCHER_IDColumn, FDVoucherId);
        //        dataManager.Parameters.Add(this.AppSchema.VoucherFDInterest.FD_LEDGER_IDColumn, FDLedgerId);
        //        dataManager.Parameters.Add(this.AppSchema.VoucherFDInterest.BK_INT_VOUCHER_IDColumn, FDInterestVoucherId);
        //        dataManager.Parameters.Add(this.AppSchema.VoucherFDInterest.BK_INT_LEDGER_IDColumn, LedgerId);

        //        resultArgs = dataManager.UpdateData();
        //    }
        //    return resultArgs;
        //}

        private ResultArgs UpdateFDStatus()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FDRenewal.UpdateFDStatus))
            {
                int BankAcctId = new LedgerSystem().FetchBankAccountById(LedgerId);
                dataManager.Parameters.Add(this.AppSchema.FDRegisters.BANK_ACCOUNT_IDColumn, BankAcctId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs SaveCostCentreInfo(Int32 LedgerSequenceNo)
        {
            try
            {
                DataTable dtCostCentreInfo = this.GetCostCentreByLedgerID(CostCenterTable).ToTable();

                if (dtCostCentreInfo != null && dtCostCentreInfo.Rows.Count > 0)
                {
                    foreach (DataRow drCostCentre in dtCostCentreInfo.Rows)
                    {
                        if (!string.IsNullOrEmpty(drCostCentre[this.AppSchema.VoucherCostCentre.COST_CENTRE_IDColumn.ColumnName].ToString()))
                        {
                            CostCenterId = this.NumberSet.ToInteger(drCostCentre[this.AppSchema.VoucherCostCentre.COST_CENTRE_IDColumn.ColumnName].ToString());
                            CostCentreAmount = this.NumberSet.ToDecimal(drCostCentre[this.AppSchema.VoucherCostCentre.AMOUNTColumn.ColumnName].ToString());
                            CostCentreSequenceNo = CostCentreSequenceNo + 1;

                            resultArgs = SaveVoucherCostCentre(LedgerSequenceNo);
                        }

                        if (!resultArgs.Success) { break; }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            return resultArgs;
        }

        private ResultArgs SaveReferenceNo(DataManager dm)
        {
            try
            {
                if (ReferenceNumberInfo != null && ReferenceNumberInfo.Rows.Count > 0)
                {
                    foreach (DataRow drReferenceNo in ReferenceNumberInfo.Rows)
                    {
                        if (!string.IsNullOrEmpty(drReferenceNo[this.AppSchema.VoucherReference.LEDGER_IDColumn.ColumnName].ToString()))
                        {
                            REC_PAY_VOUCHER_ID = VoucherId;
                            RefLEDGER_ID = this.NumberSet.ToInteger(drReferenceNo[this.AppSchema.VoucherReference.LEDGER_IDColumn.ColumnName].ToString());
                            RefAMOUNT = this.NumberSet.ToDecimal(drReferenceNo[this.AppSchema.VoucherReference.AMOUNTColumn.ColumnName].ToString());
                            REF_VOUCHER_ID = this.NumberSet.ToInteger(drReferenceNo[this.AppSchema.VoucherReference.REF_VOUCHER_IDColumn.ColumnName].ToString());

                            resultArgs = SaveReferenceNumber(dm);
                        }
                        if (!resultArgs.Success) { break; }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            return resultArgs;
        }

        /// <summary>
        /// # 10/02/2020, To save Sub Ledger Vouchers
        /// </summary>
        /// <param name="dm"></param>
        /// <returns></returns>
        private ResultArgs SaveSubLedgersVouchers(DataManager dm)
        {
            try
            {
                if (LedgerSubLedgerVouchers != null && LedgerSubLedgerVouchers.Rows.Count > 0)
                {
                    LedgerSubLedgerVouchers.DefaultView.RowFilter = this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName + " = " + LedgerId;
                    foreach (DataRowView drvSubLedgerVouchers in LedgerSubLedgerVouchers.DefaultView)
                    {
                        int subLedgerId = this.NumberSet.ToInteger(drvSubLedgerVouchers[this.AppSchema.VoucherTransaction.SUB_LEDGER_IDColumn.ColumnName].ToString());
                        decimal amount = this.NumberSet.ToInteger(drvSubLedgerVouchers[this.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString());

                        if (subLedgerId > 0 && amount > 0)
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.UpdateSubLedgerVouchers))
                            {
                                dataManager.Database = dm.Database;
                                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherId);
                                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.SEQUENCE_NOColumn, SubLedgerSequenceNo);
                                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.LEDGER_IDColumn, LedgerId);
                                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.SUB_LEDGER_IDColumn, subLedgerId);
                                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.AMOUNTColumn, amount);
                                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.TRANS_MODEColumn, TransMode);
                                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                                resultArgs = dataManager.UpdateData();
                            }
                        }
                        if (!resultArgs.Success)
                        {
                            break;
                        }
                        else
                        {
                            SubLedgerSequenceNo++;
                        }
                    }

                    LedgerSubLedgerVouchers.DefaultView.RowFilter = string.Empty;
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            return resultArgs;
        }

        private ResultArgs SaveDenomination()
        {
            try
            {
                if (isEditMode == true)
                {
                    resultArgs = DeleteDenomination();
                }
                DataTable dtDenominationInfo = this.GetDenominationByLedgerID(DenominationTable).ToTable();

                if (dtDenominationInfo != null && dtDenominationInfo.Rows.Count > 0)
                {
                    foreach (DataRow drDenomination in dtDenominationInfo.Rows)
                    {
                        int count = this.NumberSet.ToInteger(drDenomination["COUNT"].ToString());
                        if (count == 0 || count == null) { }
                        else if (!string.IsNullOrEmpty(drDenomination[this.AppSchema.Denomination.DENOMINATION_IDColumn.ColumnName].ToString()))
                        {
                            DenominationID = this.NumberSet.ToInteger(drDenomination[this.AppSchema.Denomination.DENOMINATION_IDColumn.ColumnName].ToString());
                            Count = this.NumberSet.ToInteger(drDenomination[this.AppSchema.Denomination.COUNTColumn.ColumnName].ToString());
                            DenominationAmount = this.NumberSet.ToDecimal(drDenomination[this.AppSchema.Denomination.AMOUNTColumn.ColumnName].ToString());
                            DenominationSequnce = DenominationSequnce + 1;

                            resultArgs = SaveVoucherDenomination();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            return resultArgs;
        }
        public ResultArgs DeleteVoucherCostCentreDetails(DataManager dm)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.MasterTransactionCostCentre.Delete))
            {
                dataManager.Database = dm.Database;
                dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteVoucherReferenceNumberDetails(DataManager dm, int VoucherId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.DeleteRefNumberDetails))
            {
                dataManager.Database = dm.Database;
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }


        /// <summary>
        /// On 10/02/2020, to remove existing sub ledger voucher 
        /// </summary>
        /// <param name="dm"></param>
        /// <param name="VoucherId"></param>
        /// <returns></returns>
        public ResultArgs DeleteSubLedgerVouchers(DataManager dm, int VoucherId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.DeleteSubLedgerVouchers))
            {
                dataManager.Database = dm.Database;
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteVoucherFDInterest(DataManager voucherDataManager)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.VoucherFDInterestDelete))
            {
                dataManager.Database = voucherDataManager.Database;
                dataManager.Parameters.Add(this.AppSchema.VoucherCostCentre.VOUCHER_IDColumn, FDInterestVoucherId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        #endregion

        #region Voucher Regeneration
        //public ResultArgs RegenerateVoucher(int ProjectId, string VoucherType, DateTime dtPeriodFrom, DateTime dtPeriodTo)
        //{
        //    using (DataManager dataManager = new DataManager())
        //    {
        //        dataManager.BeginTransaction();
        //        resultArgs = GenerateVoucher(ProjectId, VoucherType, dtPeriodFrom, dtPeriodTo);
        //        dataManager.EndTransaction();
        //    }
        //    return resultArgs;
        //}

        public void SetRegenerateMethod(string Voutype, Int32 voucherdefinitionid)
        {
            if (voucherdefinitionid == 0)
            {

            }

            if (Voutype == "RC" || Voutype == DefaultVoucherTypes.Receipt.ToString())
            {
                Vtype = DefaultVoucherTypes.Receipt;
                numberFormatType = NumberFormat.ReceiptNumber;
                VoucherMethodType = (int)DefaultVoucherTypes.Receipt;
                VoucherDefinitionId = voucherdefinitionid;
            }
            else if (Voutype == "PY" || Voutype == DefaultVoucherTypes.Payment.ToString())
            {
                Vtype = DefaultVoucherTypes.Payment;
                numberFormatType = NumberFormat.VoucherNumber;
                VoucherMethodType = (int)DefaultVoucherTypes.Payment;
                VoucherDefinitionId = voucherdefinitionid;
            }
            else if (Voutype == "CN" || Voutype == DefaultVoucherTypes.Contra.ToString())
            {
                Vtype = DefaultVoucherTypes.Contra;
                numberFormatType = NumberFormat.ContraVoucherNumber;
                VoucherMethodType = (int)DefaultVoucherTypes.Contra;
                VoucherDefinitionId = voucherdefinitionid;
            }
            else
            {
                Vtype = DefaultVoucherTypes.Journal;
                numberFormatType = NumberFormat.JournalVoucherNumber;
                VoucherMethodType = (int)DefaultVoucherTypes.Journal;
                VoucherDefinitionId = voucherdefinitionid;
            }
        }

        //public ResultArgs GenerateVoucher(DataManager dm, int ProjectId, string VoucherType, DateTime dtPeriodFrom, DateTime dtPeriodTo, int ResetVoucherMonth, int ResetVoucherYear)
        //{
        //    SetRegenerateMethod(VoucherType);
        //    resultArgs = GetTransaction(ProjectId, VoucherType, dtPeriodFrom, dtPeriodTo);  //Get the transaction based on the date from and date to

        //    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
        //    {
        //        DataTable dtVouchers = resultArgs.DataSource.Table;
        //        using (NumberSystem numberSystem = new NumberSystem())
        //        {
        //            resultArgs = numberSystem.DeleteVoucherNumberFormat(numberFormatType, ProjectId.ToString(), ResetVoucherMonth, ResetVoucherYear);
        //            if (resultArgs != null && resultArgs.Success)
        //            {
        //                foreach (DataRow drVoucher in dtVouchers.Rows)
        //                {
        //                    VoucherId = this.NumberSet.ToInteger(drVoucher[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString());
        //                    VoucherDate = this.DateSet.ToDate(drVoucher[this.AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName].ToString(), false);

        //                    VoucherNo = numberSystem.getNewNumber(dm, numberFormatType, ProjectId.ToString(), (int)Vtype, VoucherDate, false);
        //                    if (!string.IsNullOrEmpty(VoucherNo))
        //                    {
        //                        resultArgs = UpdateVoucherNumber(VoucherId, VoucherNo, dm);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //int VoucherId = 0;
        //DateTime VoucherDate;
        //int VoucherMth = 0;
        //int RunningDigit = 0;
        //int StartingNumber = 0;

        //DataTable dtVouchers = result.DataSource.Table;

        //resultArgs = FetchVoucherNumberFormat(ProjectId, Vtype);  // Fetch the active voucher number definition
        //if (resultArgs.Success && resultArgs.RowsAffected > 0)
        //{
        //    DataTable dtNoFormat = resultArgs.DataSource.Table;
        //    RunningDigit = StartingNumber = this.NumberSet.ToInteger(dtNoFormat.Rows[0][this.AppSchema.Voucher.STARTING_NUMBERColumn.ColumnName].ToString()) - 1;
        //}

        //if (dtVouchers != null && dtVouchers.Rows.Count > 0)
        //{
        //    //To check whether the voucher month is changed or not.
        //    DateTime dtTempMth = this.DateSet.ToDate(dtVouchers.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName].ToString(), false);
        //    int PreVoucherMth = dtTempMth.Month;

        //    foreach (DataRow drVoucher in dtVouchers.Rows)
        //    {
        //        VoucherId = this.NumberSet.ToInteger(drVoucher[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString());
        //        VoucherDate = this.DateSet.ToDate(drVoucher[this.AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName].ToString(), false);
        //        VoucherMth = VoucherDate.Month;

        //        if (VoucherMth != PreVoucherMth)
        //        {
        //            if (IsResetVoucher(ProjectId, Vtype, VoucherDate.Month, VoucherDate.Year)) { RunningDigit = StartingNumber; }
        //        }

        //        VoucherNo = GetNewVoucherNo(dm, ProjectId, Vtype, VoucherDate, RunningDigit);

        //        if (!string.IsNullOrEmpty(VoucherNo))
        //        {
        //            result = UpdateVoucherNumber(VoucherId, VoucherNo, dm);
        //            RunningDigit = RunningDigit + 1;
        //            PreVoucherMth = VoucherMth;
        //        }
        //        if (!result.Success) { break; }
        //    }
        //}

        //    return resultArgs;
        //}

        //public ResultArgs GenerateInsertVoucher(int ProjectId, string VoucherType, DateTime dtPeriodFrom, DateTime dtPeriodTo, DataManager dm)
        //{
        //    SetRegenerateMethod(VoucherType);
        //    ResultArgs result = GetTransactionForInsertVoucher(ProjectId, VoucherType, dtPeriodFrom, dtPeriodTo, dm, PreviousVoucherNo);  //Get the transaction based on the date from and date to

        //    if (result.Success && result.DataSource.Table.Rows.Count > 0)
        //    {
        //        int VoucherId = 0;
        //        DateTime VoucherDate;
        //        int VoucherMth = 0;
        //        int RunningDigit = 0;
        //        int StartingNumber = 0;

        //        DataTable dtVouchers = result.DataSource.Table;

        //        //resultArgs = FetchVoucherNumberFormat(ProjectId, Vtype);  // Fetch the active voucher number definition
        //        //if (resultArgs.Success && resultArgs.RowsAffected > 0)
        //        //{
        //        //    DataTable dtNoFormat = resultArgs.DataSource.Table;
        //        RunningDigit = StartingNumber = this.NumberSet.ToInteger(PreviousRunningDigit); //this.NumberSet.ToInteger(dtNoFormat.Rows[0][this.AppSchema.Voucher.STARTING_NUMBERColumn.ColumnName].ToString()) - 1;
        //        //}

        //        if (dtVouchers != null && dtVouchers.Rows.Count > 0)
        //        {
        //            //To check whether the voucher month is changed or not.
        //            DateTime dtTempMth = this.DateSet.ToDate(dtVouchers.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName].ToString(), false);
        //            int PreVoucherMth = dtTempMth.Month;

        //            foreach (DataRow drVoucher in dtVouchers.Rows)
        //            {
        //                VoucherId = this.NumberSet.ToInteger(drVoucher[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString());
        //                VoucherDate = this.DateSet.ToDate(drVoucher[this.AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName].ToString(), false);
        //                VoucherMth = VoucherDate.Month;

        //                if (VoucherMth != PreVoucherMth)
        //                {
        //                    if (IsResetVoucher(ProjectId, Vtype, VoucherDate.Month, VoucherDate.Year)) { RunningDigit = StartingNumber; }
        //                }

        //                VoucherNo = GetNewVoucherNo(dm, ProjectId, Vtype, VoucherDate, RunningDigit);

        //                if (!string.IsNullOrEmpty(VoucherNo))
        //                {
        //                    result = UpdateVoucherNumber(VoucherId, VoucherNo, dm);
        //                    RunningDigit = RunningDigit + 1;
        //                    PreVoucherMth = VoucherMth;
        //                }
        //                if (!result.Success) { break; }
        //            }
        //        }
        //    }

        //    return result;
        //}

        //private string GetNewVoucherNo(DataManager dm, int ProjectId, VoucherTransType TransType, DateTime VoucherDate, int RunningDigit)
        //{
        //    using (NumberSystem numberSystem = new NumberSystem())
        //    {
        //        DateTime ResetDate = GetResetStartDate(VoucherDate, ProjectId, TransType);
        //        VoucherNo = numberSystem.getRegeneratedNumber(dm, numberFormatType, ProjectId.ToString(), (int)TransType, VoucherDate, RunningDigit, ResetDate.Month);
        //    }
        //    return VoucherNo;
        //}

        //public ResultArgs FetchVoucherNumberFormat(int ProjectID, DefaultVoucherTypes TransType)
        //{
        //    using (DataManager dataManager = new DataManager(SQLCommand.Voucher.FetchVoucherNumberFormat))
        //    {
        //        dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectID);
        //        dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_TYPEColumn, (int)TransType);
        //        resultArgs = dataManager.FetchData(DataSource.DataTable);

        //        if (resultArgs.Success && resultArgs.RowsAffected > 0)
        //        {
        //            DataTable dtNoFormat = resultArgs.DataSource.Table;
        //            VoucherApplicableMonth = this.NumberSet.ToInteger(dtNoFormat.Rows[0][this.AppSchema.Voucher.MONTHColumn.ColumnName].ToString()) + 1;
        //            VoucherDuration = this.NumberSet.ToInteger(dtNoFormat.Rows[0][this.AppSchema.Voucher.DURATIONColumn.ColumnName].ToString());
        //        }
        //    }
        //    return resultArgs;
        //}

        private ResultArgs GetTransaction(int ProjectId, string VoucherType, DateTime dtPeriodFrom, DateTime dtPeriodTo, Int32 voucherdefinitionid)
        {
            ResultArgs result = null;

            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchReGenerationVouchers))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_TYPEColumn, VoucherType);
                dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_FROMColumn, dtPeriodFrom);
                dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_TOColumn, dtPeriodTo);
                dataManager.Parameters.Add(this.AppSchema.VoucherNumber.VOUCHER_DEFINITION_IDColumn, voucherdefinitionid);
                if (isInsertVoucher)
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_NOColumn, PreviousVoucherNo);
                }

                result = dataManager.FetchData(DataSource.DataTable);
            }

            return result;
        }

        private ResultArgs GetTransactionForInsertVoucher(int ProjectId, string VoucherType, DateTime dtPeriodFrom, DateTime dtPeriodTo, DataManager dm, string voucherNo)
        {
            ResultArgs result = null;

            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchVouchersForInsert))
            {
                dataManager.Database = dm.Database;
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_TYPEColumn, VoucherType);
                dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_FROMColumn, dtPeriodFrom);
                dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_TOColumn, dtPeriodTo);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_NOColumn, voucherNo);

                result = dataManager.FetchData(DataSource.DataTable);
            }

            return result;
        }

        private ResultArgs UpdateVoucherNumber(int UpdateVoucherId, string VoucherNo, DataManager dm)
        {
            ResultArgs result = null;

            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.UpdateVoucherNumber))
            {
                dataManager.Database = dm.Database;
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, UpdateVoucherId);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_NOColumn, VoucherNo);

                result = dataManager.UpdateData();
            }

            return result;
        }

        public ResultArgs FetchVoucherMethod(int Project, int TransType)
        {
            ResultArgs result = null;

            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchVoucherMethod))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, Project);
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_TYPEColumn, TransType);

                result = dataManager.FetchData(DataSource.DataTable);
            }
            return result;
        }

        /// <summary>
        /// To check the  Voucher Number  wheather to reset or not.
        /// </summary>
        /// <returns></returns>
        //public bool IsResetVoucher(int Project, DefaultVoucherTypes VoucherType, int VoucherMth, int VoucherYr)
        //{
        //    bool isReset = false;
        //    int voucherStartMth = 0;
        //    resultArgs = FetchVoucherNumberFormat(Project, VoucherType);

        //    if (resultArgs.Success && resultArgs.RowsAffected > 0)
        //    {
        //        voucherStartMth = VoucherApplicableMonth;
        //        DateTime dtYearFrom = this.DateSet.ToDate(this.YearFrom, false);
        //        if (VoucherYr != dtYearFrom.Year)
        //        {
        //            voucherStartMth = 1;
        //        }
        //        for (int i = 0; i < 12; i++)
        //        {
        //            if (voucherStartMth == VoucherMth)
        //            {
        //                isReset = true;
        //                break;
        //            }
        //            voucherStartMth = voucherStartMth + VoucherDuration;
        //        }
        //    }
        //    return isReset;
        //}

        //public DateTime GetResetStartDate(DateTime dtVoucherDate, int Project, DefaultVoucherTypes vType)
        //{
        //    DateTime ResetDate = dtVoucherDate;
        //    int VoucherStartMth = 0;
        //    int[] ResetMonths = new int[12];

        //    bool isReset = IsResetVoucher(Project, vType, dtVoucherDate.Month, dtVoucherDate.Year);
        //    if (isReset)
        //    {
        //        ResetDate = this.DateSet.ToDate("01/" + dtVoucherDate.Month + "/" + dtVoucherDate.Year, false);
        //    }
        //    else
        //    {
        //        resultArgs = FetchVoucherNumberFormat(Project, vType);
        //        if (resultArgs.Success && resultArgs.RowsAffected > 0)
        //        {
        //            VoucherStartMth = VoucherApplicableMonth;

        //            for (int i = 0; i < 12; i++)
        //            {
        //                ResetMonths[i] = VoucherStartMth;
        //                VoucherStartMth = VoucherStartMth + VoucherDuration;
        //            }

        //            for (int i = 11; i >= 0; i--)
        //            {
        //                if (ResetMonths[i] < dtVoucherDate.Month)
        //                {
        //                    ResetDate = this.DateSet.ToDate("01/" + ResetMonths[i] + "/" + dtVoucherDate.Year, false);
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    return ResetDate;
        //}

        //private DateTime GetResetEndDate(DateTime dtVoucherDate, int Project, VoucherTransType vType)
        //{
        //    DateTime ResetEndDate = dtVoucherDate;
        //    int ResetStartMonth = 0;
        //    int ResetEndMonth = 0;
        //    int VoucherStartMth = 0;
        //    int[] ResetMonths = new int[12];

        //    bool isReset = IsResetVoucher(Project, vType, dtVoucherDate.Month, dtVoucherDate.Year);
        //    if (isReset)
        //    {
        //        ResetStartMonth = dtVoucherDate.Month;
        //        //ResetEndDate = this.DateSet.ToDate("01/" + dtVoucherDate.Month + "/" + dtVoucherDate.Year, false);
        //    }
        //    else
        //    {
        //        resultArgs = FetchVoucherNumberFormat(Project, vType);
        //        if (resultArgs.Success && resultArgs.RowsAffected > 0)
        //        {
        //            VoucherStartMth = VoucherApplicableMonth;

        //            for (int i = 0; i < 12; i++)
        //            {
        //                ResetMonths[i] = VoucherStartMth;
        //                VoucherStartMth = VoucherStartMth + VoucherDuration;
        //            }

        //            for (int i = 11; i >= 0; i--)
        //            {
        //                if (ResetMonths[i] < dtVoucherDate.Month)
        //                {
        //                    ResetStartMonth = ResetMonths[i];
        //                    //ResetEndDate = this.DateSet.ToDate("01/" + ResetMonths[i] + "/" + dtVoucherDate.Year, false);
        //                    break;
        //                }
        //            }
        //        }
        //    }

        //    if (ResetEndDate != null)
        //    {
        //        ResetEndMonth = ResetStartMonth + VoucherDuration - 1;
        //        ResetEndDate = this.DateSet.ToDate("01/" + ResetEndMonth + "/" + dtVoucherDate.Year, false);
        //    }

        //    return ResetEndDate;
        //}
        #endregion

        #region Common Methods
        private void SetVoucherMethod()
        {
            try
            {
                if (VoucherType == DefaultVoucherTypes.Receipt.ToString() || VoucherType == "RC")
                {
                    VoucherType = "RC";
                    TransVoucherType = (int)DefaultVoucherTypes.Receipt;
                    TransMode = TransactionMode.CR.ToString();
                    CashTransMode = TransactionMode.DR.ToString();
                    vTransType = DefaultVoucherTypes.Receipt;
                    numberFormatType = NumberFormat.ReceiptNumber;
                }
                else if (VoucherType == DefaultVoucherTypes.Payment.ToString() || VoucherType == "PY")
                {
                    VoucherType = "PY";
                    TransVoucherType = (int)DefaultVoucherTypes.Payment;
                    TransMode = TransactionMode.DR.ToString();
                    CashTransMode = TransactionMode.CR.ToString();
                    vTransType = DefaultVoucherTypes.Payment;
                    numberFormatType = NumberFormat.VoucherNumber;
                }
                else if (VoucherType == DefaultVoucherTypes.Contra.ToString() || VoucherType == "CN")
                {
                    VoucherType = "CN";
                    TransVoucherType = (int)DefaultVoucherTypes.Contra;
                    TransMode = TransactionMode.CR.ToString();
                    CashTransMode = TransactionMode.DR.ToString();
                    numberFormatType = NumberFormat.ContraVoucherNumber;
                    vTransType = DefaultVoucherTypes.Contra;
                }
                else
                {
                    VoucherType = "JN";
                    TransVoucherType = (int)DefaultVoucherTypes.Journal;
                    numberFormatType = NumberFormat.JournalVoucherNumber;
                    vTransType = DefaultVoucherTypes.Journal;
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
        }

        public DataSet LoadVoucherDetails(int ProjectID, string VoucherType, DateTime dateFrom, DateTime dateTo)
        {
            //string VoucherID = string.Empty;
            DataSet dsTransaction = new DataSet();
            try
            {
                resultArgs = FetchTransactionDetails(ProjectID, VoucherType, dateFrom, dateTo);
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    resultArgs.DataSource.Table.TableName = "Master";
                    dsTransaction.Tables.Add(resultArgs.DataSource.Table);
                    //for (int i = 0; i < resultArgs.DataSource.Table.Rows.Count; i++)
                    //{
                    //    VoucherID += resultArgs.DataSource.Table.Rows[i][this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString() + ",";
                    //}
                    //VoucherID = VoucherID.TrimEnd(',');

                    //resultArgs = FetchVoucherTransactionDetails();
                    //VoucherId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString());

                    resultArgs = FetchVoucherTrans(ProjectID, VoucherType, dateFrom, dateTo);
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        resultArgs.DataSource.Table.TableName = "Ledger";
                        dsTransaction.Tables.Add(resultArgs.DataSource.Table);
                        dsTransaction.Relations.Add(dsTransaction.Tables[1].TableName, dsTransaction.Tables[0].Columns[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName], dsTransaction.Tables[1].Columns[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName]);
                    }

                    //resultArgs = FetchCostcentreVoucherView(ProjectID, dateFrom, dateTo, VoucherID);
                    //if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    //{
                    //    resultArgs.DataSource.Table.TableName = "CostCentre";
                    //    dsTransaction.Tables.Add(resultArgs.DataSource.Table);

                    //    // dsTransaction.Relations.Add("CC_LedgerID", dsTransaction.Tables[1].Columns[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName], dsTransaction.Tables[2].Columns[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName]);
                    //    dsTransaction.Relations.Add(dsTransaction.Tables[2].TableName, dsTransaction.Tables[0].Columns[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName], dsTransaction.Tables[2].Columns[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName]);
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, false);
            }
            finally
            {
            }
            return dsTransaction;
        }


        public DataTable LoadVoucherMasterDetails(int ProjectID, string VoucherType, DateTime dateFrom, DateTime dateTo)
        {
            DataTable dtMaster = new DataTable();
            try
            {
                resultArgs = FetchTransactionDetails(ProjectID, VoucherType, dateFrom, dateTo);
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    resultArgs.DataSource.Table.TableName = "Master";
                    dtMaster = resultArgs.DataSource.Table;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, false);
            }
            finally
            {
            }
            return dtMaster;
        }

        public DataTable LoadVoucherTranDetails(int ProjectID, string VoucherType, DateTime dateFrom, DateTime dateTo)
        {
            DataTable dtTrans = new DataTable();
            try
            {
                resultArgs = FetchVoucherTrans(ProjectID, VoucherType, dateFrom, dateTo);
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    resultArgs.DataSource.Table.TableName = "Ledger";
                    dtTrans = resultArgs.DataSource.Table;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, false);
            }
            finally
            {
            }
            return dtTrans;
        }

        public DataTable LoadVoucherCCDetails(int ProjectID, string VoucherType, DateTime dateFrom, DateTime dateTo)
        {
            DataTable dtCCMaster = new DataTable();
            string VoucherID = "";
            try
            {
                resultArgs = FetchTransactionDetails(ProjectID, VoucherType, dateFrom, dateTo);
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    resultArgs.DataSource.Table.TableName = "Master";
                    for (int i = 0; i < resultArgs.DataSource.Table.Rows.Count; i++)
                    {
                        VoucherID += resultArgs.DataSource.Table.Rows[i][this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString() + ",";
                    }
                    VoucherID = VoucherID.TrimEnd(',');

                    resultArgs = FetchCostcentreVoucherView(ProjectID, dateFrom, dateTo, VoucherID);
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        resultArgs.DataSource.Table.TableName = "CostCentre";
                        dtCCMaster = resultArgs.DataSource.Table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, false);
            }
            finally
            {
            }
            return dtCCMaster;
        }

        /// <summary>
        /// Criteria to Choose Deleted voucher details based on the Criteria (chinna)
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="VoucherType"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public DataSet loadDeleteVoucherDetails(int ProjectID, string VoucherType, DateTime dateFrom, DateTime dateTo)
        {
            string VoucherID = string.Empty;
            DataSet dsTransaction = new DataSet();
            try
            {
                resultArgs = FetchDeleteTransactionDetails(ProjectID, VoucherType, dateFrom, dateTo);
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    resultArgs.DataSource.Table.TableName = "Master";
                    dsTransaction.Tables.Add(resultArgs.DataSource.Table);
                    for (int i = 0; i < resultArgs.DataSource.Table.Rows.Count; i++)
                    {
                        VoucherID += resultArgs.DataSource.Table.Rows[i][this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString() + ",";
                    }
                    VoucherID = VoucherID.TrimEnd(',');
                    resultArgs = FetchTransactions(VoucherID);
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        resultArgs.DataSource.Table.TableName = "Ledger";
                        dsTransaction.Tables.Add(resultArgs.DataSource.Table);
                        dsTransaction.Relations.Add(dsTransaction.Tables[1].TableName, dsTransaction.Tables[0].Columns[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName], dsTransaction.Tables[1].Columns[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName]);
                    }
                    resultArgs = FetchDeletedCostcentreVoucherView(ProjectID, dateFrom, dateTo, VoucherID);
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        resultArgs.DataSource.Table.TableName = "CostCentre";
                        dsTransaction.Tables.Add(resultArgs.DataSource.Table);

                        dsTransaction.Relations.Add(dsTransaction.Tables[2].TableName, dsTransaction.Tables[0].Columns[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName], dsTransaction.Tables[2].Columns[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, false);
            }
            finally
            {
            }
            return dsTransaction;
        }

        public ResultArgs SaveVoucherDetails(DataManager saveTransDataManager)
        {
            try
            {
                if (CommonMethod.ValidateLicensePeriod(VoucherDate, SettingProperty.Current.LicenseKeyYearFrom, SettingProperty.Current.LicenseKeyYearTo))
                {
                    //On 21/10/2021
                    resultArgs = ValidateVouchers();
                    if (resultArgs.Success)
                    {
                        if (!IsAuditLockedVoucherDate(ProjectId, VoucherDate))
                        {
                            using (DataManager dataManager = new DataManager())
                            {
                                resultArgs.Success = true;

                                dataManager.Database = saveTransDataManager.Database;
                                SetVoucherMethod();

                                if (VoucherId == 0)
                                {
                                    if (TransVoucherMethod == (int)TransactionVoucherMethod.Automatic)
                                    {
                                        using (NumberSystem numberSystem = new NumberSystem())
                                        {
                                            if (this.NumberSet.ToInteger(this.EnableVoucherRegenerationInsert) == (int)YesNo.Yes)
                                            {
                                                if (isInsertVoucher)
                                                {
                                                    numberSystem.PreviourRunningDigit = this.NumberSet.ToInteger(PreviousRunningDigit) - 1;
                                                    numberSystem.IsInsertVoucher = true;
                                                    VoucherNo = numberSystem.getNewNumber(dataManager, numberFormatType, ProjectId.ToString(), TransVoucherType, VoucherDate, true, VoucherDefinitionId);
                                                }
                                                else
                                                {
                                                    VoucherNo = numberSystem.getNewNumber(dataManager, numberFormatType, ProjectId.ToString(), TransVoucherType, VoucherDate, false, VoucherDefinitionId);
                                                }

                                            }
                                            else
                                            {
                                                VoucherNo = numberSystem.getNewNumber(dataManager, numberFormatType, ProjectId.ToString(), TransVoucherType, VoucherDate, false, VoucherDefinitionId);
                                            }
                                        }
                                    }
                                }

                                //On 01/09/2021, To assign Voucher Previous details for Audit history log details
                                resultArgs = AssignGetVoucherPreviousDetailsForAuditLog(VoucherId, dataManager);

                                //13/12/2022, Attach GST Voucher details -----------------------------------------
                                if (resultArgs.Success)
                                {
                                    resultArgs = AttachVoucherLedgetsToGSTInvoiceLedgerDetails(false, VoucherType, dtGSTInvoiceMasterLedgerDetails, this.TransInfo.ToTable());
                                    if (resultArgs.Success)
                                    {
                                        dtGSTInvoiceMasterLedgerDetails = resultArgs.DataSource.Table;
                                    }
                                    else
                                    {
                                        dtGSTInvoiceMasterLedgerDetails = null;
                                    }
                                }
                                //------------------------------------------------------------------------------

                                // Delete the Transaction, Cost Centre details of the Voucher in Edit.
                                if (VoucherId > 0 && resultArgs.Success)
                                {
                                    isEditMode = true;
                                    using (BalanceSystem balanceSystem = new BalanceSystem())
                                    {
                                        resultArgs = balanceSystem.UpdateTransBalance(VoucherId, TransactionAction.EditBeforeSave);
                                        if (resultArgs.Success)
                                        {
                                            resultArgs = Deletevouchertransactiondetails(dataManager, VoucherId);
                                            if (resultArgs.Success)
                                            {
                                                resultArgs = DeleteVoucherCostCentreDetails(dataManager);
                                                if (resultArgs.Success)
                                                {
                                                    resultArgs = DeleteVoucherReferenceNumberDetails(dataManager, VoucherId);

                                                    if (resultArgs.Success && VoucherId > 0)
                                                    {
                                                        resultArgs = DeleteRandPVoucherAgainsInvoice(dataManager, VoucherId);
                                                    }

                                                    if (resultArgs.Success && IS_DIOMYS_DIOCESE && EnableSubLedgerVouchers == "1") //#10/02/2020, To clear Existing Sub Ledger Vouchers
                                                    {
                                                        resultArgs = DeleteSubLedgerVouchers(dataManager, VoucherId);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                //Save Voucher Master Details, Transaction Details.
                                if (resultArgs.Success)
                                {
                                    resultArgs = SaveVoucherMasterDetails(dataManager);

                                    if (resultArgs.Success && resultArgs.RowsAffected != 0)
                                    {
                                        VoucherId = (VoucherId == 0) ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : VoucherId;
                                        FDVoucherId = VoucherId;
                                        resultArgs = SaveTransactionDetails(dataManager);
                                        if (resultArgs.Success)
                                        {
                                            if (VoucherType == "PY" || VoucherType == DefaultVoucherTypes.Payment.ToString())
                                            {
                                                resultArgs = SaveReferenceNo(dataManager);
                                            }

                                            if (resultArgs.Success)
                                            {
                                                using (BalanceSystem balanceSystem = new BalanceSystem())
                                                {
                                                    if (isEditMode)
                                                        resultArgs = balanceSystem.UpdateTransBalance(VoucherId, TransactionAction.EditAfterSave);
                                                    else
                                                        resultArgs = balanceSystem.UpdateTransBalance(VoucherId, TransactionAction.New);
                                                }

                                                //On 25/07/2017, commented by alwar, 
                                                //For every voucher entry (All entries), whenever entry is added, they call RegenerateVoucherNumbers
                                                //This will take all voucher entries and regenerate voucher numbers
                                                //RegenerateVoucherNumbers will be invoked only VOUCHER INSERT, VOUCHER DELETE

                                                if (resultArgs.Success)
                                                {
                                                    if (this.NumberSet.ToInteger(this.EnableVoucherRegenerationInsert) == (int)YesNo.Yes && isInsertVoucher) //if (this.NumberSet.ToInteger(this.EnableRegenerateVoucher) == (int)YesNo.Yes)
                                                    {
                                                        resultArgs = RegenerateVoucherNumbers(dataManager, VoucherDate, VoucherDate);
                                                        if (resultArgs.Success)
                                                        {
                                                            if (EditVoucherTypeChanged)
                                                            {
                                                                VoucherType = (EditVoucherIndex == 0) ? DefaultVoucherTypes.Receipt.ToString() : (EditVoucherIndex == 1) ? DefaultVoucherTypes.Payment.ToString() : DefaultVoucherTypes.Contra.ToString();
                                                                SetVoucherMethod();
                                                                resultArgs = RegenerateVoucherNumbers(dataManager, EditVoucherDate, EditVoucherDate);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                //On 03/09/2021, To update Audit Log Hisotry
                                //If and If only for Auditor User or Voucher is alrady modifed by Auditor
                                if (resultArgs.Success && (this.IsLoginUserAuditor || IsVoucherModifiedByAuditor || IsEnableVoucherChangesHistory))
                                {
                                    UpdateAuditLogHistory(dataManager, (isEditMode ? AuditAction.Modified : AuditAction.Created));
                                }

                                // Save Fixed Deposit Interest details.
                                if (resultArgs.Success)
                                {
                                    //To post the FD Interest
                                    dtTransInfo = this.FixedDepositInterestInfo;
                                    if (dtTransInfo != null && dtTransInfo.Rows.Count > 0)
                                    {
                                        SaveFixedDepositInterest(dataManager);
                                        this.FixedDepositInterestInfo = null;
                                    }

                                    //03/11/2022, Save GST 
                                    if (this.NumberSet.ToInteger(this.EnableGST.ToString()).Equals((int)SetDefaultValue.DefaultValue) ||
                                        (this.IsCountryOtherThanIndia || this.AllowMultiCurrency == 1))
                                    {
                                        if (this.NumberSet.ToInteger(this.IncludeGSTVendorInvoiceDetails) != 0)
                                        {
                                            resultArgs = SaveGSTInvoiceMasterDetails(dataManager);
                                        }
                                    }
                                }

                                //29/07/2024, To update Voucher Image Details --------------------------------
                                if (resultArgs.Success && base.AttachVoucherFiles == 1)
                                {
                                    resultArgs = UpdateVoucherFileDetailsByVoucher(dataManager);
                                    //If any problem occurs when saving voucher images, let us remove voucher image details and images
                                    if (!resultArgs.Success)
                                    {
                                        RemoveVoucherFileFromPathByVoucher();
                                    }
                                    else
                                    {
                                        UploadVoucherFileToAcmeerpServer();
                                    }
                                }
                                //-----------------------------------------------------------------------------
                            }
                        }
                        else
                        {
                            resultArgs.Success = false;
                            //string lockedmessage = "Unable to Post Vouchers. Voucher is locked for this Project.";
                            string lockedmessage = "Unable to Post/Modify Vouchers." + System.Environment.NewLine + "Voucher is locked for '" + LockProjectName + "'" +
                                " during the period of " + DateSet.ToDate(LockFromDate.ToShortDateString()) + " - " + DateSet.ToDate(LockToDate.ToShortDateString());
                            resultArgs.Message = lockedmessage;
                        }
                    }
                }
                else
                {
                    resultArgs.Success = false;
                    resultArgs.Message = "You are not allowed to make an entry for expired/future license period";
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            return resultArgs;
        }

        public ResultArgs RegenerateVoucher(DateTime VoucherFrom, DateTime VoucherTo, bool CanRegenerateBasedOnDate)
        {
            using (DataManager dm = new DataManager())
            {
                dm.BeginTransaction();
                SetRegenerateMethod(VoucherType, VoucherDefinitionId);
                resultArgs = DeleteVoucherNoFormat();
                if (resultArgs.Success)
                {
                    resultArgs = RegenerateVoucherNumbers(dm, VoucherFrom, VoucherTo, CanRegenerateBasedOnDate);
                }
                if (!resultArgs.Success)
                {
                    dm.TransExecutionMode = ExecutionMode.Fail;
                }
                dm.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs DeleteVoucherNoFormat()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Voucher.DeleteVoucherNumberFormatByTransType))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherNumber.NUMBER_FORMAT_IDColumn, (int)numberFormatType);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.VoucherNumber.VOUCHER_DEFINITION_IDColumn, VoucherDefinitionId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs RegenerateVoucherNoMoveTrans(DateTime TransFromVoucherDate)
        {
            using (DataManager dataManager = new DataManager())
            {
                resultArgs = RegenerateVoucherNumbers(dataManager, TransFromVoucherDate, TransFromVoucherDate);
            }
            return resultArgs;
        }

        public ResultArgs RegenerateVoucherNumbers(DataManager dm, DateTime VoucherDateFrom, DateTime VoucherDateTo, bool CanRegenerateBasedOnDate = false)
        {
            try
            {
                int ResetStartMonth = 0;
                int ResetEndMonth = 0;
                int ResetStartYear = 0;
                int ResetEndYear = 0;
                int TempVoucherId = 0;
                DateTime StartDate = DateTime.Now;
                DateTime EndDate = DateTime.Now;

                SetRegenerateMethod(VoucherType, VoucherDefinitionId);
                using (NumberSystem numberSystem = new NumberSystem())
                {
                    numberSystem.VoucherDefinitionId = VoucherDefinitionId;
                    resultArgs = FetchVoucherNumberDefinition();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        TransVoucherMethod = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Voucher.VOUCHER_METHODColumn.ColumnName].ToString());
                        numberSystem.VoucherDuration = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Voucher.DURATIONColumn.ColumnName].ToString());
                        if (TransVoucherMethod == (int)TransactionVoucherMethod.Automatic)
                        {
                            numberSystem.VoucherMonth = VoucherDateFrom.Month;
                            numberSystem.VoucherYear = VoucherDateFrom.Year;
                            resultArgs = numberSystem.GetResetStartMonth();
                            if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                            {
                                ResetStartMonth = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["MONTH"].ToString());
                                ResetStartYear = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["YEAR"].ToString());
                                StartDate = new DateTime(ResetStartYear, ResetStartMonth, 1);
                                numberSystem.VoucherMonth = VoucherDateTo.Month;
                                numberSystem.VoucherYear = VoucherDateTo.Year;
                                resultArgs = numberSystem.GetResetEndMonth();
                                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                                {
                                    ResetEndMonth = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["MONTH"].ToString());
                                    ResetEndYear = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["YEAR"].ToString());
                                    EndDate = new DateTime(ResetEndYear, ResetEndMonth, 1).AddMonths(1).AddDays(-1);
                                    if (StartDate != null && EndDate != null)
                                    {
                                        resultArgs = GetTransaction(ProjectId, VoucherType, StartDate, EndDate, VoucherDefinitionId);  //Get the transaction based on the date from and date to
                                        if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                                        {
                                            DataTable dtVouchers = resultArgs.DataSource.Table;

                                            //As on 28/10/2021 To have proper regenerate Voucher Number based on Voucher Date and Entry Order--
                                            if (CanRegenerateBasedOnDate)
                                            {
                                                dtVouchers.DefaultView.Sort = AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName + "," + AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName;
                                                //dtVouchers.DefaultView.Sort = AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName;
                                                dtVouchers = dtVouchers.DefaultView.ToTable();
                                            }
                                            //-------------------------------------------------------------------------------------------------

                                            if (!isInsertVoucher)
                                            {
                                                resultArgs = numberSystem.DeleteVoucherNumberFormat(numberFormatType, ProjectId.ToString(), ResetStartMonth, ResetStartYear, VoucherDefinitionId);
                                            }
                                            if (resultArgs != null && resultArgs.Success)
                                            {
                                                if (isInsertVoucher) { numberSystem.PreviourRunningDigit = this.NumberSet.ToInteger(PreviousRunningDigit); }
                                                foreach (DataRow drVoucher in dtVouchers.Rows)
                                                {
                                                    TempVoucherId = this.NumberSet.ToInteger(drVoucher[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString());
                                                    VoucherDate = this.DateSet.ToDate(drVoucher[this.AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName].ToString(), false);
                                                    VoucherNo = numberSystem.getNewNumber(dm, numberFormatType, ProjectId.ToString(), (int)Vtype, VoucherDate, false, VoucherDefinitionId);
                                                    if (!string.IsNullOrEmpty(VoucherNo))
                                                    {
                                                        numberSystem.PreviourRunningDigit = 0;
                                                        resultArgs = UpdateVoucherNumber(TempVoucherId, VoucherNo, dm);
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
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Regenerating Voucher. " + ex.ToString();
            }
            return resultArgs;
        }

        public string TempVoucherNo()
        {
            using (DataManager dataManager = new DataManager())
            {
                SetVoucherMethod();
                using (NumberSystem numberSystem = new NumberSystem())
                {
                    VoucherNo = numberSystem.getNewNumber(dataManager, numberFormatType, ProjectId.ToString(), TransVoucherType, VoucherDate, true, VoucherDefinitionId);
                }
            }
            return VoucherNo;
        }

        public ResultArgs SaveFixedDepositInterest(DataManager voucherdataManager)
        {
            try
            {
                if (CommonMethod.ValidateLicensePeriod(VoucherDate, SettingProperty.Current.LicenseKeyYearFrom, SettingProperty.Current.LicenseKeyYearTo))
                {
                    if (!IsAuditLockedVoucherDate(ProjectId, VoucherDate))
                    {
                        using (DataManager dataManager = new DataManager())
                        {
                            dataManager.Database = voucherdataManager.Database;
                            FDVoucherId = VoucherId == 0 ? VoucherId : FDVoucherId;
                            VoucherId = 0;
                            VoucherType = (InterestType == true) ? "JN" : "RC";
                            VoucherDefinitionId = (InterestType == true) ? (int)DefaultVoucherTypes.Journal : (int)DefaultVoucherTypes.Receipt;
                            TransVoucherType = (InterestType == true) ? (int)DefaultVoucherTypes.Journal : (int)DefaultVoucherTypes.Receipt;
                            TransMode = TransactionMode.CR.ToString();
                            CashTransMode = TransactionMode.DR.ToString();
                            SetVoucherMethod();
                            if (TransVoucherMethod == (int)TransactionVoucherMethod.Automatic)
                            {
                                using (NumberSystem numberSystem = new NumberSystem())
                                {
                                    if (FDTransType == FDTypes.WD.ToString())
                                    {
                                        VoucherNo = numberSystem.getNewNumber(dataManager, numberFormatType, ProjectId.ToString(), TransVoucherType, VoucherDate, false, VoucherDefinitionId);
                                    }
                                    else
                                    {
                                        //On 04/10/2022, to have receipt voucher number
                                        //VoucherNo = numberSystem.getNewNumber(dataManager, NumberFormat.VoucherNumber, ProjectId.ToString(), TransVoucherType, VoucherDate, false, VoucherDefinitionId);
                                        VoucherNo = numberSystem.getNewNumber(dataManager, numberFormatType, ProjectId.ToString(), TransVoucherType, VoucherDate, false, VoucherDefinitionId);
                                    }
                                    //VoucherNo = numberSystem.getNewNumber(dataManager, numberFormatType, ProjectId.ToString(), TransVoucherType, VoucherDate);
                                }
                            }
                            else
                            {
                                //On 06/10/2022, for fd withdrwal receipt interest nubmer
                                if (VoucherType == VoucherSubTypes.RC.ToString() && FDTransType == FDTypes.WD.ToString() || FDTransType == FDTypes.PWD.ToString())
                                {
                                    VoucherNo = FDWithdrwalReceiptVoucherNo;
                                }
                            }

                            //if (!string.IsNullOrEmpty(VoucherNo))
                            //{
                            //    if (TransVoucherMethod == (int)TransactionVoucherMethod.Automatic)
                            //    {
                            //        using (NumberSystem numberSystem = new NumberSystem())
                            //        {
                            //            if (FDTransType == FDTypes.WD.ToString())
                            //            {
                            //                VoucherNo = numberSystem.getNewNumber(dataManager, numberFormatType, ProjectId.ToString(), TransVoucherType, VoucherDate);
                            //            }
                            //            else
                            //            {
                            //                VoucherNo = numberSystem.getNewNumber(dataManager, NumberFormat.VoucherNumber, ProjectId.ToString(), TransVoucherType, VoucherDate);
                            //            }
                            //        }
                            //    }
                            //}


                            //On 01/09/2021, To assign Voucher Previous details for Audit history log details
                            Amount = 0;
                            resultArgs = AssignGetVoucherPreviousDetailsForAuditLog(FDInterestVoucherId, dataManager);

                            if (FDInterestVoucherId > 0 && resultArgs.Success)
                            {
                                isEditMode = true;
                                using (BalanceSystem balanceSystem = new BalanceSystem())
                                {
                                    balanceSystem.UpdateTransBalance(FDInterestVoucherId, TransactionAction.EditBeforeSave);
                                }
                                Deletevouchertransactiondetails(dataManager, FDInterestVoucherId);
                            }

                            Narration = dtTransInfo.Rows[0][this.AppSchema.VoucherMaster.NARRATIONColumn.ColumnName].ToString();
                            ActualAmount = NumberSet.ToDecimal(dtTransInfo.Rows[0]["AMOUNT"].ToString());
                            VoucherDate = dteWithdrawalReceiptDate != DateTime.MinValue ? dteWithdrawalReceiptDate : VoucherDate;

                            if (isEditMode) { VoucherId = FDInterestVoucherId; }

                            if (ActualAmount != 0)
                            {
                                //On 16/10/2024, for fd withdrwal when we update receipt intrest voucher, let us take interest amount for currency exchange
                                /*if (this.AllowMultiCurrency == 1  && (FDTransType == FDTypes.WD.ToString() || FDTransType == FDTypes.PWD.ToString()))
                                {
                                    ContributionAmount = ActualAmount;
                                    ActualAmount = (ActualAmount * ExchangeRate);
                                    CalculatedAmount = ActualAmount;
                                }*/
                                if (this.AllowMultiCurrency == 1)
                                {
                                    ContributionAmount = ActualAmount;
                                    ActualAmount = (ActualAmount * ExchangeRate);
                                    CalculatedAmount = ActualAmount;
                                }

                                resultArgs = SaveVoucherMasterDetails(dataManager);
                                if (resultArgs.Success && resultArgs.RowsAffected != 0)
                                {
                                    VoucherId = FDInterestVoucherId == 0 ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : FDInterestVoucherId;
                                    // VoucherId = FDInterestVoucherId == 0 ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : VoucherId;
                                    // FDVoucherId = VoucherId;
                                    SequenceNumber.ReSetSequenceNumber();
                                    foreach (DataRow dr in dtTransInfo.Rows)
                                    {
                                        LedgerId = this.NumberSet.ToInteger(dr[this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
                                        Amount = this.NumberSet.ToDecimal(dr["AMOUNT"].ToString());
                                        TransMode = dr["TRANS_MODE"].ToString();
                                        SequenceNo = SequenceNumber.GetSequenceNumber();
                                        MaterializedOn = dr["MATERIALIZED_ON"].ToString();

                                        //12/11/2024, For Ledger based currency details
                                        LedgerExchangeRate = LedgerLiveExchangeRate = LedgerActualAmount = 0;
                                        if (this.AllowMultiCurrency == 1)
                                        {
                                            LedgerExchangeRate = NumberSet.ToDecimal(dr[this.AppSchema.VoucherTransaction.EXCHANGE_RATEColumn.ColumnName].ToString());
                                            LedgerLiveExchangeRate = NumberSet.ToDecimal(dr[this.AppSchema.VoucherTransaction.LIVE_EXCHANGE_RATEColumn.ColumnName].ToString());
                                            LedgerActualAmount = (Amount * LedgerExchangeRate); //(LedgerExchangeRate * Amount);
                                        }

                                        resultArgs = SaveVoucherTransactionDetails(dataManager);
                                    }
                                    if (resultArgs.Success)
                                    {
                                        using (BalanceSystem balanceSystem = new BalanceSystem())
                                        {
                                            if (isEditMode)
                                                balanceSystem.UpdateTransBalance(VoucherId, TransactionAction.EditAfterSave);
                                            else
                                                balanceSystem.UpdateTransBalance(resultArgs.RowUniqueId != null ? NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : 0, TransactionAction.New);
                                        }
                                    }
                                    FDInterestVoucherId = FDInterestVoucherId == 0 ? VoucherId : FDInterestVoucherId;
                                }
                            }
                            else
                            {
                                using (BalanceSystem balanceSystem = new BalanceSystem())
                                {
                                    if (isEditMode)
                                        balanceSystem.UpdateTransBalance(VoucherId, TransactionAction.EditAfterSave);
                                    else
                                        balanceSystem.UpdateTransBalance(resultArgs.RowUniqueId != null ? NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : 0, TransactionAction.New);
                                }
                            }

                            if (resultArgs.Success)
                            {
                                if (this.NumberSet.ToInteger(this.EnableVoucherRegenerationInsert) == (int)YesNo.Yes)
                                {
                                    resultArgs = RegenerateVoucherNumbers(dataManager, VoucherDate, VoucherDate);
                                }
                            }

                            //On 03/09/2021, To update Audit Log Hisotry
                            //If and If only for Auditor User or Voucher is alrady modifed by Auditor
                            if (resultArgs.Success && (this.IsLoginUserAuditor || IsVoucherModifiedByAuditor))
                            {
                                UpdateAuditLogHistory(dataManager, (isEditMode ? AuditAction.Modified : AuditAction.Created));
                            }
                        }
                    }
                    else
                    {
                        resultArgs.Success = false;
                        string lockedmessage = "Unable to Delete/Modify Vouchers." + System.Environment.NewLine + "Voucher is locked for '" + LockProjectName + "'" +
                            " during the period of " + DateSet.ToDate(LockFromDate.ToShortDateString()) + " - " + DateSet.ToDate(LockToDate.ToShortDateString());
                        resultArgs.Message = lockedmessage;
                    }
                }
                else
                {
                    resultArgs.Success = false;
                    resultArgs.Message = "You are not allowed to make an entry for expired/future license period";
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        public ResultArgs FillVoucherDetails(int VoucherID)
        {
            resultArgs = FetchMaterDetailsById(VoucherID);
            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
            {
                VoucherId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString());
                if (resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName] != DBNull.Value)
                {
                    VoucherDate = this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName].ToString(), false);
                }
                ProjectId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.PROJECT_IDColumn.ColumnName].ToString());
                ProjectName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                VoucherNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_NOColumn.ColumnName].ToString();
                VoucherType = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_TYPEColumn.ColumnName].ToString();
                VoucherSubType = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_SUB_TYPEColumn.ColumnName].ToString();
                DonorId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.DONOR_IDColumn.ColumnName].ToString());
                PurposeId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.PURPOSE_IDColumn.ColumnName].ToString());
                ContributionType = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.CONTRIBUTION_TYPEColumn.ColumnName].ToString();
                ContributionAmount = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.CONTRIBUTION_AMOUNTColumn.ColumnName].ToString());
                CurrencyCountryId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.CURRENCY_COUNTRY_IDColumn.ColumnName].ToString());
                ExchangeRate = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.EXCHANGE_RATEColumn.ColumnName].ToString());
                CalculatedAmount = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.CALCULATED_AMOUNTColumn.ColumnName].ToString());
                ActualAmount = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.ACTUAL_AMOUNTColumn.ColumnName].ToString());
                ExchageCountryId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.EXCHANGE_COUNTRY_IDColumn.ColumnName].ToString());
                Narration = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.NARRATIONColumn.ColumnName].ToString();

                ///   Status = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.STATUSColumn.ColumnName].ToString());

                //if (resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.CREATED_ONColumn.ColumnName] != DBNull.Value)
                //{
                //    CreatedOn = this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.CREATED_ONColumn.ColumnName].ToString(), false);
                //}

                //if (resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.MODIFIED_ONColumn.ColumnName] != DBNull.Value)
                //{
                //    ModifiedOn = this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.MODIFIED_ONColumn.ColumnName].ToString(), false);
                //}
                CreatedBy = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.CREATED_BYColumn.ColumnName].ToString());
                ModifiedBy = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.MODIFIED_BYColumn.ColumnName].ToString());
                NameAddress = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.NAME_ADDRESSColumn.ColumnName].ToString();

                PanNumber = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.PAN_NUMBERColumn.ColumnName].ToString();
                GSTNumber = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.GST_NUMBERColumn.ColumnName].ToString();

                VoucherDefinitionId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherNumber.VOUCHER_DEFINITION_IDColumn.ColumnName].ToString());


                //On 24/07/2023, Authorization of Vouchers-------------------------------------------------------------------------------------
                AuthorizedStatus = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.AUTHORIZATION_STATUSColumn.ColumnName].ToString());
                authorizedbyname = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.AUTHORIZATION_UPDATED_BY_NAMEColumn.ColumnName].ToString();

                authorizedon = null;
                if (resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.AUTHORIZATION_UPDATED_ONColumn.ColumnName] != null &&
                    !string.IsNullOrEmpty(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.AUTHORIZATION_UPDATED_ONColumn.ColumnName].ToString()))
                    authorizedon = DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.AUTHORIZATION_UPDATED_ONColumn.ColumnName].ToString(), false);
                //-----------------------------------------------------------------------------------------------------------------------------

                //01/09/2021, For Auditor modified
                int isvouchermodifiedbyauditor = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.IS_AUDITOR_MODIFIEDColumn.ColumnName].ToString());
                IsVoucherModifiedByAuditor = (isvouchermodifiedbyauditor == 1);

                //On 04/11/2022, to get GST Invoice details by Vocuehrid
                dtGSTInvoiceMasterDetails = null;
                dtGSTInvoiceMasterLedgerDetails = null;
                GST_INVOICE_ID = 0;
                GST_VENDOR_INVOICE_NO = null;
                GST_VENDOR_INVOICE_DATE = null;
                GST_VENDOR_INVOICE_TYPE = 0;
                GST_VENDOR_ID = 0;
                BookingGSTInvoiceId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.GSTInvoiceMaster.BOOKING_INVOICE_IDColumn.ColumnName].ToString());

                //if (this.NumberSet.ToInteger(this.EnableGST.ToString()).Equals((int)SetDefaultValue.DefaultValue))
                if (resultArgs.Success && this.NumberSet.ToInteger(this.EnableGST.ToString()).Equals((int)SetDefaultValue.DefaultValue) ||
                        (this.IsCountryOtherThanIndia || this.AllowMultiCurrency == 1))
                {
                    GST_INVOICE_ID = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][AppSchema.GSTInvoiceMaster.GST_INVOICE_IDColumn.ColumnName].ToString());
                    if (this.IncludeGSTVendorInvoiceDetails == "1")
                    {
                        //GST_INVOICE_ID = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][AppSchema.GSTInvoiceMaster.GST_INVOICE_IDColumn.ColumnName].ToString());
                        resultArgs = FetchGSTInvoiceMasterDetails(GST_INVOICE_ID);  //FetchGSTInvoiceMasterDetailsByVoucherId(VoucherId);
                        if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            dtGSTInvoiceMasterDetails = resultArgs.DataSource.Table;
                            GST_INVOICE_ID = NumberSet.ToInteger(dtGSTInvoiceMasterDetails.Rows[0][AppSchema.GSTInvoiceMaster.GST_INVOICE_IDColumn.ColumnName].ToString());
                            //26/04/2019, for Vendor GST Invoice details
                            GST_VENDOR_INVOICE_NO = dtGSTInvoiceMasterDetails.Rows[0][this.AppSchema.VoucherMaster.GST_VENDOR_INVOICE_NOColumn.ColumnName].ToString();
                            GST_VENDOR_INVOICE_DATE = dtGSTInvoiceMasterDetails.Rows[0][this.AppSchema.VoucherMaster.GST_VENDOR_INVOICE_DATEColumn.ColumnName].ToString();
                            GST_VENDOR_INVOICE_TYPE = this.NumberSet.ToInteger(dtGSTInvoiceMasterDetails.Rows[0][this.AppSchema.VoucherMaster.GST_VENDOR_INVOICE_TYPEColumn.ColumnName].ToString());
                            GST_VENDOR_ID = this.NumberSet.ToInteger(dtGSTInvoiceMasterDetails.Rows[0][this.AppSchema.VoucherMaster.GST_VENDOR_IDColumn.ColumnName].ToString());

                            //Get GST Invoice Ledger details
                            if (GST_INVOICE_ID > 0)
                            {
                                resultArgs = FetchGSTInvoiceMasterLedgersDetails(GST_INVOICE_ID);
                                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                                {
                                    dtGSTInvoiceMasterLedgerDetails = resultArgs.DataSource.Table;
                                }

                            }
                        }
                    }
                }

                //On 30/07/2024 - Attach Voucher Images
                if (base.AttachVoucherFiles == 1)
                {
                    FetchVoucherFilesByVoucher();
                }
            }
            return resultArgs;
        }

        //public ResultArgs FetchVoucherNo()
        //{
        //    using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchVoucherStartingNo))
        //    {
        //        dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
        //        dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_TYPEColumn, TransVoucherType);
        //        resultArgs = dataManager.FetchData(DataSource.Scalar);
        //    }
        //    return resultArgs;
        //}

        public string FetchLastVoucherDate(int proId, DateTime dtYearFrom, DateTime dtYearTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchLastVoucherDate))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, proId);
                dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_FROMColumn, dtYearFrom);
                dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_TOColumn, dtYearTo);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToString;
        }


        public string FetchLastVoucherDate()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchLastVoucherDate))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToString;
        }

        public string FetchLastVoucherDate(int proId, string dtYearFrom, string dtYearTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchLastVoucherDate))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, proId);
                if (!string.IsNullOrEmpty(dtYearFrom))
                {
                    dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_FROMColumn, dtYearFrom);
                }

                if (!string.IsNullOrEmpty(dtYearTo))
                {
                    dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_TOColumn, dtYearTo);
                }
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToString;
        }

        public ResultArgs FetchBRSDetails(int ProjectID, Int32 BankAccountLedgerId, DateTime DateFrom, DateTime DateTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchBRS))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectID);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, BankAccountLedgerId);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, DateTo);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.IDENTITY_FLAGColumn, (BankAccountLedgerId == 0 ? "0" : "1"));
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBRSDetails(int ProjectID, DateTime DateFrom, DateTime DateTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchBRS))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectID);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, DateTo);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.IDENTITY_FLAGColumn, "0");
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        public ResultArgs FetchBRSDetails(int ProjectID, Int32 BankAccountLedgerId, DateTime DateAsOn)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchBRS))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectID);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, BankAccountLedgerId);
                dataManager.Parameters.Add(this.AppSchema.DashBoard.DATE_AS_ONColumn, DateAsOn);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.IDENTITY_FLAGColumn, (BankAccountLedgerId == 0 ? "0" : "1"));
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBRSDetailsByMaterialized(int ProjectID, Int32 BankAccountLedgerId, DateTime DateAsOn)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchBRSByMaterialized))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectID);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, BankAccountLedgerId);
                dataManager.Parameters.Add(this.AppSchema.DashBoard.DATE_AS_ONColumn, DateAsOn);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.IDENTITY_FLAGColumn, (BankAccountLedgerId == 0 ? "0" : "1"));
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBRSDetailsByMaterialized(int ProjectID, DateTime DateAsOn)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchBRSByMaterialized))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectID);
                dataManager.Parameters.Add(this.AppSchema.DashBoard.DATE_AS_ONColumn, DateAsOn);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.IDENTITY_FLAGColumn, "0");
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBRSDetails(int ProjectID, DateTime DateAsOn)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchBRS))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectID);
                dataManager.Parameters.Add(this.AppSchema.DashBoard.DATE_AS_ONColumn, DateAsOn);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.IDENTITY_FLAGColumn, "0");
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        //public ResultArgs IsFDInterestPosted()
        //{
        //    using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchFDVoucherInterest))
        //    {
        //        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
        //        resultArgs = dataManager.FetchData(DataSource.DataTable);
        //    }
        //    return resultArgs;
        //}

        public ResultArgs AutoFetchNarration()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.AutoFetchNarration))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CREATED_BYColumn, LoginUserId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        /// <summary>
        /// On 23/04/2021, To get Narrtion
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchNarration()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchNarration))
            {
                if (!IsFullRightsReservedUser)
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CREATED_BYColumn, LoginUserId);
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 24/04/2021, To change/Update Voucher Narrations 
        /// </summary>
        /// <param name="ExistingNarration"></param>
        /// <param name="NewNarration"></param>
        /// <returns></returns>
        public ResultArgs BulKUpdateNarration(string ExistingNarration, string NewNarration)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.BulkUpdateNarration))
            {
                if (!IsFullRightsReservedUser)
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CREATED_BYColumn, LoginUserId);
                }

                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.EXISTING_NARRATIONColumn, ExistingNarration);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.NEW_NARRATIONColumn, NewNarration);

                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.MODIFIED_BYColumn, NumberSet.ToInteger(this.LoginUserId));
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.MODIFIED_BY_NAMEColumn, this.FirstName);

                //On 02/09/2021, To update Auditor Modification flag for Auditor users/for other user leave as it is.
                //dataManager.Parameters.Add(this.AppSchema.User.IS_AUDITORColumn, (IsLoginUserAuditor ? 1 : 0));

                //dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// On 19/07/2023, To Authorize given vouchers
        /// </summary>
        /// <param name="vouchersids"></param>
        /// <returns></returns>
        public ResultArgs AuthorizeVouchers(int vid)
        {
            ResultArgs result = new ResultArgs();
            if (vid > 0)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.AuthorizeVoucherByVoucherId))
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, vid);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.AUTHORIZATION_STATUSColumn, (int)ActiveStatus.Active);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.AUTHORIZATION_UPDATED_BY_NAMEColumn, this.FirstName);
                    result = dataManager.UpdateData();
                }
            }
            else
            {
                result.Message = "Voucher(s) not found";
            }
            return result;
        }

        public ResultArgs FetchAutoFetchNames()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchAutoFetchNames))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public BalanceProperty FetchBudgetAmount()
        {
            BalanceProperty balanceProperty = new BalanceProperty();
            double amount = 0;
            string transMode = "";
            if (BudgetMonthDistribution == 0)  // Added by chinna if Monthly Distribution 17.12.2019
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchBudgetAmount))
                {
                    dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, VoucherDate);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.LEDGER_IDColumn, LedgerId);
                    dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_TYPE_IDColumn, BudgetTypeId);

                    //// Chinna 30/04/2024 to show Budgeted Amount
                    //if (BudgetTypeId == (int)BudgetType.BudgetPeriod)
                    //{
                    dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_FROMColumn, BudgetDateFrom);
                    dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_TOColumn, BudgetDateTo);
                    //}
                    //else
                    //{
                    //    dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_FROMColumn, YearFrom);
                    //    dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_TOColumn, YearTo);
                    //}
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.TRANS_MODEColumn, TransMode);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                    if (resultArgs.Success)
                    {
                        DataTable dtBalance = resultArgs.DataSource.Table;

                        if (dtBalance != null && dtBalance.Rows.Count > 0)
                        {
                            DataRow drBalance = dtBalance.Rows[0];

                            amount = this.NumberSet.ToDouble(drBalance[AppSchema.LedgerBalance.AMOUNTColumn.ColumnName].ToString());
                            transMode = drBalance[AppSchema.LedgerBalance.TRANS_MODEColumn.ColumnName].ToString();
                        }
                    }
                    balanceProperty.Amount = amount;
                    balanceProperty.TransMode = transMode;
                    balanceProperty.Result = resultArgs;
                }
            }
            else
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchBudgetMonthDistributionAmount))
                {
                    // this is to get new datamanager and to get voucherdate date of begining month ( 17.12.2019).Chinna
                    dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, new DateTime(this.DateSet.ToDate(VoucherDate.ToString(), false).Year, this.DateSet.ToDate(VoucherDate.ToString(), false).Month, 1));
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.LEDGER_IDColumn, LedgerId);
                    dataManager.Parameters.Add(this.AppSchema.Budget.BUDGET_TYPE_IDColumn, BudgetTypeId);
                    dataManager.Parameters.Add(this.AppSchema.Budget.IS_MONTH_WISEColumn, BudgetMonthDistribution);
                    dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_FROMColumn, YearFrom);
                    dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_TOColumn, YearTo);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.TRANS_MODEColumn, TransMode);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                    if (resultArgs.Success)
                    {
                        DataTable dtBalance = resultArgs.DataSource.Table;

                        if (dtBalance != null && dtBalance.Rows.Count > 0)
                        {
                            DataRow drBalance = dtBalance.Rows[0];

                            amount = this.NumberSet.ToDouble(drBalance[AppSchema.LedgerBalance.AMOUNTColumn.ColumnName].ToString());
                            transMode = drBalance[AppSchema.LedgerBalance.TRANS_MODEColumn.ColumnName].ToString();
                        }
                    }
                    balanceProperty.Amount = amount;
                    balanceProperty.TransMode = transMode;
                    balanceProperty.Result = resultArgs;
                }
            }
            return balanceProperty;
        }

        //public ResultArgs FetchFDPostedInterest()
        //{
        //    using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchFDVoucherInterestByVoucherId))
        //    {
        //        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, FDInterestVoucherId);
        //        resultArgs = dataManager.FetchData(DataSource.DataTable);
        //    }
        //    return resultArgs;
        //}

        public string FetchFDVoucherPostedInterest(int FDAccountId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchFDVoucherPostedInterest))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.BANK_ACCOUNT_IDColumn, FDAccountId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToString;
        }

        public ResultArgs UpdateBRSDetails(DataTable dtBRS)
        {
            foreach (DataRow dr in dtBRS.Rows)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.UpdateBRS))
                {
                    MaterializedOn = dr[this.AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName].ToString() != null ? dr[this.AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName].ToString() : string.Empty;
                    dataManager.Parameters.Add(AppSchema.VoucherTransaction.MATERIALIZED_ONColumn, MaterializedOn);
                    dataManager.Parameters.Add(AppSchema.Voucher.VOUCHER_IDColumn, dr[AppSchema.Voucher.VOUCHER_IDColumn.ColumnName]);
                    dataManager.Parameters.Add(AppSchema.VoucherTransaction.SEQUENCE_NOColumn, dr[AppSchema.VoucherTransaction.SEQUENCE_NOColumn.ColumnName]);
                    dataManager.Parameters.Add(AppSchema.VoucherMaster.MODIFIED_BYColumn, NumberSet.ToInteger(this.LoginUserId));
                    dataManager.Parameters.Add(AppSchema.VoucherMaster.MODIFIED_BY_NAMEColumn, this.FirstName);

                    //On 02/09/2021, To update Auditor Modification flag for Auditor users/for other user leave as it is.
                    //dataManager.Parameters.Add(this.AppSchema.User.IS_AUDITORColumn, (IsLoginUserAuditor ? 1 : 0));

                    resultArgs = dataManager.UpdateData();
                }
            }
            return resultArgs;
        }

        public DataSet LoadJournalVoucherDetails(int projectId, DateTime dtDateFrom, DateTime dtDateTo)
        {
            string VoucherId = string.Empty;
            DataSet dsJournal = new DataSet();

            resultArgs = FetchJournalTransactionDetails(projectId, dtDateFrom, dtDateTo);

            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
            {
                resultArgs.DataSource.Table.TableName = "Master";
                dsJournal.Tables.Add(resultArgs.DataSource.Table);

                for (int i = 0; i < resultArgs.DataSource.Table.Rows.Count; i++)
                {
                    VoucherId += this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[i][this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString()) + ",";
                }
                VoucherId = VoucherId.TrimEnd(',');

                resultArgs = FetchTransVoucherDetails(VoucherId);
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    resultArgs.DataSource.Table.TableName = "Ledger";
                    dsJournal.Tables.Add(resultArgs.DataSource.Table);
                    dsJournal.Relations.Add(dsJournal.Tables[1].TableName, dsJournal.Tables[0].Columns[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName], dsJournal.Tables[1].Columns[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName]);
                }
                resultArgs = FetchCostcentreVoucherView(projectId, dtDateFrom, dtDateTo, VoucherId);
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    resultArgs.DataSource.Table.TableName = "CostCentre";
                    dsJournal.Tables.Add(resultArgs.DataSource.Table);
                    dsJournal.Relations.Add(dsJournal.Tables[2].TableName, dsJournal.Tables[0].Columns[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName], dsJournal.Tables[2].Columns[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName]);
                }
            }
            return dsJournal;
        }

        public ResultArgs FetchVoucherIdByClientRefCode()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchVoucherIdByClientRefCode))
            {
                dataManager.Parameters.Add(AppSchema.VoucherMaster.CLIENT_CODEColumn, ClientCode);
                dataManager.Parameters.Add(AppSchema.VoucherMaster.CLIENT_REFERENCE_IDColumn, ClientReferenceId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        public ResultArgs FetchVoucherbyGeneral()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchVoucherTransforSSP))
            {
                dataManager.Parameters.Add(AppSchema.VoucherMaster.CLIENT_CODEColumn, ClientCode);
                dataManager.Parameters.Add(AppSchema.VoucherTransaction.CHEQUE_NOColumn, ChequeNo);
                dataManager.Parameters.Add(AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_DATEColumn, VoucherDate);
                //dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchCBVoucherByFetchedDate()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchVoucherTransforSSPDeletion))
            {
                dataManager.Parameters.Add(AppSchema.VoucherMaster.CLIENT_CODEColumn, ClientCode);
                dataManager.Parameters.Add(AppSchema.VoucherTransaction.SUB_LEDGER_NAMEColumn, ClientRefBankId);
                dataManager.Parameters.Add(AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_DATEColumn, VoucherDate);
                // dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchVoucherIdByClientReferenceId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchVoucherIdByClientRefId))
            {
                dataManager.Parameters.Add(AppSchema.VoucherMaster.CLIENT_CODEColumn, ClientCode);
                dataManager.Parameters.Add(AppSchema.VoucherMaster.CLIENT_REFERENCE_IDColumn, ClientReferenceId);
                dataManager.Parameters.Add(AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(AppSchema.VoucherTransaction.LEDGER_IDColumn, LedgerId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchVoucherIdByCashExists()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchVoucherIdByCash))
            {
                dataManager.Parameters.Add(AppSchema.VoucherMaster.CLIENT_CODEColumn, ClientCode);
                dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_DATEColumn, VoucherDateFrom);
                dataManager.Parameters.Add(AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(AppSchema.VoucherTransaction.LEDGER_IDColumn, LedgerId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchVoucherIdByCashIndividualExists()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchVoucherIdByIndividualCash))
            {
                dataManager.Parameters.Add(AppSchema.VoucherMaster.CLIENT_CODEColumn, ClientCode);
                dataManager.Parameters.Add(AppSchema.VoucherMaster.CLIENT_REFERENCE_IDColumn, ClientReferenceId);
                dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_DATEColumn, VoucherDateFrom);
                dataManager.Parameters.Add(AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(AppSchema.VoucherTransaction.LEDGER_IDColumn, LedgerId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchVoucherOnlineCollections()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchVoucherOnlineCollections))
            {
                dataManager.Parameters.Add(AppSchema.VoucherMaster.CLIENT_CODEColumn, ClientCode);
                dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(AppSchema.VoucherTransaction.SUB_LEDGER_IDColumn, TDSCashBankId);
                dataManager.Parameters.Add(AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_DATEColumn, VoucherDate);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchVouchersOnlineVouDateMaterDateCollections()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchOnlineVDateMDateCollections))
            {
                dataManager.Parameters.Add(AppSchema.VoucherMaster.CLIENT_CODEColumn, ClientCode);
                dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_DATEColumn, VoucherDate);
                if (string.IsNullOrEmpty(MaterializedOn))
                {
                    dataManager.Parameters.Add(AppSchema.VoucherTransaction.MATERIALIZED_ONColumn, MaterializedOn);
                    dataManager.Parameters.Add(AppSchema.VoucherTransaction.CHEQUE_NOColumn, "");
                }
                else
                {
                    dataManager.Parameters.Add(AppSchema.VoucherTransaction.MATERIALIZED_ONColumn, MaterializedOn);
                    dataManager.Parameters.Add(AppSchema.VoucherTransaction.CHEQUE_NOColumn, MaterializedOn);
                }

                dataManager.Parameters.Add(AppSchema.VoucherTransaction.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(AppSchema.VoucherTransaction.SUB_LEDGER_IDColumn, TDSCashBankId);
                dataManager.Parameters.Add(AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int FetchMatNullVouchers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchMatNullVouchers))
            {
                dataManager.Parameters.Add(AppSchema.VoucherMaster.CLIENT_CODEColumn, ClientCode);
                dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);

            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs FetchVoucherByUniqueIdwithBankTransaction()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchVoucherTransforSSPUnique))
            {
                dataManager.Parameters.Add(AppSchema.VoucherMaster.CLIENT_CODEColumn, ClientCode);
                dataManager.Parameters.Add(AppSchema.VoucherMaster.CLIENT_REFERENCE_IDColumn, ClientReferenceId);
                dataManager.Parameters.Add(AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_DATEColumn, VoucherDate);
                //dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchVoucherByTransEmpty()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchVoucherTransforSSPEmpty))
            {
                dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_DATEColumn, VoucherDate);
                dataManager.Parameters.Add(AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        #endregion

        #region TDS Booking
        private ResultArgs SaveTDSBooking(DataManager dataManager)
        {
            try
            {
                using (Bosco.Model.TDS.TDSBookingSystem BookingSystem = new TDS.TDSBookingSystem())
                {
                    using (DataManager TDSDataManager = new DataManager())
                    {
                        TDSDataManager.Database = dataManager.Database;
                        BookingSystem.dtBookingDetail = dsTDSBooking.Tables[0];
                        BookingSystem.PartyLedgerId = PartyLedgerId;
                        BookingSystem.DeducteeId = DeducteeTypeId;
                        BookingSystem.ExpenseLedgerId = ExpenseLedgerId;
                        BookingSystem.TDSNetPayableAmount = this.NumberSet.ToDecimal(dsTDSBooking.Tables[0].Compute("SUM(PARTY_AMOUNT)", "").ToString());
                        BookingSystem.Amount = this.NumberSet.ToDouble(dsTDSBooking.Tables[0].Compute("SUM(ASSESS_AMOUNT)", "").ToString());
                        BookingSystem.ProjectId = ProjectId;
                        BookingSystem.VoucherId = VoucherId;
                        BookingSystem.BookingDate = dteTDSBookingDate;
                        BookingSystem.BookingId = TDSBookingId;
                        BookingSystem.TDSAmountDic = TDSBookingDic;
                        BookingSystem.dtVoucherMasterTrans = this.TransInfo.ToTable();

                        resultArgs = BookingSystem.SaveTDSBooking(TDSDataManager);
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        private ResultArgs SaveTDSDeductionLater(DataManager dataManager)
        {
            try
            {
                using (Bosco.Model.TDS.TDSDeductionSystem DeductionSystem = new TDS.TDSDeductionSystem())
                {
                    using (DataManager TDSDataManager = new DataManager())
                    {
                        TDSDataManager.Database = dataManager.Database;
                        DeductionSystem.dtDeductionDetails = dtTDSDeductionLater;
                        DeductionSystem.PartyLedgerId = PartyLedgerId;
                        DeductionSystem.ProjectId = ProjectId;
                        DeductionSystem.VoucherId = VoucherId;
                        DeductionSystem.DeductionDate = VoucherDate;
                        DeductionSystem.BookingId = TDSBookingId;
                        resultArgs = DeductionSystem.SaveTDSDeductionLater();
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        public int CheckTDSBooking()
        {
            int isExists = 0;
            using (TDSBookingSystem BookingSystem = new TDSBookingSystem())
            {
                BookingSystem.VoucherId = VoucherId;
                isExists = BookingSystem.CheckTDSBookingExists();
            }
            return isExists;
        }

        public ResultArgs FetchBookingIdByVoucher()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchTDSBookingId))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int IsVoucherDeleted()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.CheckVoucherDeleted))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }


        #endregion

        #region Post Payment
        public int FetchPostIdByVoucherId()
        {
            using (DataManager dtManager = new DataManager(SQLCommand.VoucherMaster.FetchPostIdByVoucherId))
            {
                dtManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                dtManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dtManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        #endregion

        #region Voucher Routine
        public ResultArgs VocherRouterAnalyzerForStock()
        {
            using (DataManager dataManger = new DataManager(SQLCommand.VoucherTransDetails.VoucherRouterAnalyzerStock))
            {
                dataManger.Parameters.Add(AppSchema.Voucher.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManger.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs VoucherRouterAnalyzerForAsset()
        {
            using (DataManager dataManger = new DataManager(SQLCommand.VoucherTransDetails.VoucherRouterAnalyzerAsset))
            {
                dataManger.Parameters.Add(AppSchema.Voucher.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManger.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs VoucherRouterAnalyzerForPayRole()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.VoucherRouterAnalyzerPayRole))
            {
                dataManager.Parameters.Add(AppSchema.Voucher.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAssetCashBankDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.FetchAssetCashBankDetails))
            {
                SetVoucherMethod();
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.TRANS_MODEColumn, CashTransMode);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_TYPEColumn, VoucherType);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAssetInsuranceAMCDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.FetchAssetInsuranceAMCDetails))
            {
                SetVoucherMethod();
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchCashBankByVoucherIdForPurchase()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.FetchAssetCashBankDetailsForPurchase))
            {
                SetVoucherMethod();
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.TRANS_MODEColumn, CashTransMode);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_TYPEColumn, VoucherType);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchReportSetting(string reportId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Setting.FetchReportSetting))
            {
                dataManager.Parameters.Add(this.AppSchema.Settings.REPORT_IDColumn, reportId);

                resultArgs = dataManager.FetchData(DataSource.DataView);
            }
            return resultArgs;
        }

        public ResultArgs CheckLedgerareMappedByVoucher(int VoucID, int ProjId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.CheckLedgerMappedByProjectVoucher))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjId);
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_IDColumn, VoucID);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        /// <summary>
        /// This method is used to get LedgerGroupId for given Ledger 
        /// </summary>
        /// <param name="ledgerId"></param>
        /// <returns></returns>
        private Int32 GetLedgerGroupId(Int32 ledgerId)
        {
            Int32 LedgerGroupId = 0;
            try
            {
                using (LedgerSystem ledger = new LedgerSystem())
                {
                    ledger.LedgerId = ledgerId;
                    LedgerGroupId = ledger.FetchLedgerGroupById();
                }
            }
            catch (Exception Err)
            {
                MessageRender.ShowMessage("Could not get Ledger Group " + Err.Message);
            }
            return LedgerGroupId;
        }
        public ResultArgs CheckLedgerMappedByProject(int Ledger, int ProjId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.CheckLedgerMappedByProject))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, Ledger);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        public ResultArgs CheckVoucherTypeMappedByProject(int ProjectId, int VoucherDefinitionId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.CheckVoucherTypeMappedByProject))
            {
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_IDColumn, VoucherDefinitionId);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        /// <summary>
        /// Check Vouhcer Defintion is exists and check its proper under Receipts/Payments/Contra
        /// </summary>
        /// <returns></returns>
        public ResultArgs CheckProperVoucherTypeDefintion(Int32 voucherdefId, string BaseVoucherType)
        {
            using (VoucherSystem vouchersystem = new VoucherSystem())
            {
                resultArgs = vouchersystem.CheckProperVoucherTypeDefintion(voucherdefId, BaseVoucherType);
            }
            return resultArgs;
        }
        #endregion

        #region Reference

        /// <summary>
        /// This method is used to get list of pending refence numbers for given ledger and project and voucher date must be less than journal voucher date
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="voucherid"></param>
        /// <param name="ledgerid"></param>
        /// <param name="voucherdate"></param>
        /// <returns></returns>
        public ResultArgs FetchReferenceBalances(Int32 projectid, Int32 recpayvoucherid, Int32 ledgerid, DateTime voucherdate)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchReferenceBalance))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, projectid);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.REC_PAY_VOUCHER_IDColumn, recpayvoucherid);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.LEDGER_IDColumn, ledgerid);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_DATEColumn, voucherdate);
                resultArgs = dataManager.FetchData(DataSource.DataTable);

                if (resultArgs.Success)
                {
                    //Filter only balance >0 
                    DataTable dtReferences = resultArgs.DataSource.Table;
                    dtReferences.DefaultView.RowFilter = "BALANCE > 0";
                    resultArgs.DataSource.Data = dtReferences.DefaultView.ToTable();
                }

            }
            return resultArgs;
        }

        /// <summary>
        /// This method is used to get selected voucher's all ledger's reference details
        /// </summary>
        /// <param name="voucherid"></param>
        /// <returns></returns>
        public ResultArgs FetchVoucherLedgerReferenceDetails(Int32 recpayvoucherid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchVoucherLedgerReferenceDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.REC_PAY_VOUCHER_IDColumn, recpayvoucherid);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// This method is used to get selected voucher's all ledger's sub ledger details
        /// </summary>
        /// <param name="voucherid"></param>
        /// <returns></returns>
        public ResultArgs FetchVoucherLedgerSubLedgerVouchers(Int32 ProjectId, Int32 VoucherId, Nullable<DateTime> voucherdateBudget = null, string transmode = "")
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchVoucherLedgerSubLedgerVouchers))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherId);

                if (voucherdateBudget != null)
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_DATEColumn, voucherdateBudget);
                }

                if (!string.IsNullOrEmpty(transmode))
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.TRANS_MODEColumn, transmode);
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgerAmountValidation(Int32 ReferenceVoucherId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchLedgerByAmount))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.REF_VOUCHER_IDColumn, ReferenceVoucherId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 08/06/2020, Check Cheque number, already exists or not
        /// </summary>
        /// <param name="ReferenceVoucherId"></param>
        /// <returns></returns>
        public ResultArgs CheckChequeNumberExists(string chequenumber, Int32 voucherid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.ChequeNumberExists))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.CHEQUE_NOColumn, chequenumber);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, voucherid);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            if (resultArgs.Success && resultArgs.DataSource.Table != null)
            {
                Int32 chequenumbercount = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][AppSchema.VoucherTransaction.CHEQUE_NOColumn.ColumnName].ToString());
                resultArgs.ReturnValue = chequenumbercount;
            }
            return resultArgs;
        }


        public ResultArgs FetchFundTransferList()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherTransDetails.FetchFundTransferList))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        #endregion

        #region Audit Log Details

        /// <summary>
        /// Voucher's Auditor Log details
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="VoucherType"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public DataTable LoadVoucherAuditHistoryDetails(int ProjectID, string VoucherType, DateTime dateFrom, DateTime dateTo)
        {
            DataTable dtAuditHistoryDetails = new DataTable();
            try
            {

                using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchVoucherAuditLogHistoryDetails))
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectID);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_TYPEColumn, VoucherType);
                    dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, dateFrom);
                    dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, dateTo);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    resultArgs.DataSource.Table.TableName = "AuditHistoryDetails";
                    dtAuditHistoryDetails = resultArgs.DataSource.Table;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, false);
            }
            finally
            {
            }
            return dtAuditHistoryDetails;
        }

        /// <summary>
        /// Get Vouchers list with audit details for given project, date range
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="VoucherType"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public DataTable FetchMasterAuditLog(int ProjectID, string VoucherType, DateTime dateFrom, DateTime dateTo)
        {
            DataTable dtMaster = new DataTable();
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchMasterAuditLogHistory))
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_TYPEColumn, VoucherType);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectID);
                    dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, dateFrom);
                    dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, dateTo);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    resultArgs.DataSource.Table.TableName = "Master";
                    dtMaster = resultArgs.DataSource.Table;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, false);
            }
            finally
            {
            }
            return dtMaster;
        }

        public ResultArgs FetchVouchersForAuthorization(int ProjectID, string VoucherTypeDefination, DateTime dateFrom, DateTime dateTo)
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchVouchersForAuthorization))
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_DEFINITION_IDColumn, VoucherTypeDefination);
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectID);
                    dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, dateFrom);
                    dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, dateTo);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultarg = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultarg.Message = ex.Message;
            }
            finally
            {
            }
            return resultarg;
        }

        /// <summary>
        /// On 01/09/2021, To get if given voucher is modified by Auditor
        /// </summary>
        /// <param name="VoucherId"></param>
        /// <returns></returns>
        public bool HasVoucherModifiedByAuditor1(Int32 VoucherId)
        {
            bool rtn = false;
            using (DataManager dataManger = new DataManager(SQLCommand.VoucherMaster.IsVoucherModifiedByAuditor))
            {
                dataManger.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManger.FetchData(DataSource.DataTable);
            }
            if (resultArgs.Success && resultArgs.DataSource.Table != null)
            {
                int isvouchermodifiedbyauditor = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.IS_AUDITOR_MODIFIEDColumn.ColumnName].ToString());
                IsVoucherModifiedByAuditor = (isvouchermodifiedbyauditor == 1);
                rtn = IsVoucherModifiedByAuditor;
            }
            return rtn;
        }

        /// <summary>
        /// On 03/09/2021, To get Voucher details previous voucher values, Save Voucehr master method might be called from many places
        /// so we get voucher's previous values for Audit Log
        /// </summary>
        /// <param name="VoucherId"></param>
        /// <returns></returns>
        public ResultArgs AssignGetVoucherPreviousDetailsForAuditLog(Int32 VoucherId, DataManager dm)
        {
            using (DataManager dataManger = new DataManager(SQLCommand.VoucherMaster.FetchVoucherPreviousDetailsForAuditLog))
            {
                dataManger.Database = dm.Database;
                dataManger.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManger.FetchData(DataSource.DataTable);
            }

            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                DataTable dtAuditVoucher = resultArgs.DataSource.Table;
                if (this.IsLoginUserAuditor)
                {
                    IsVoucherModifiedByAuditor = true;
                }
                else
                {
                    int isvouchermodifiedbyauditor = this.NumberSet.ToInteger(dtAuditVoucher.Rows[0][this.AppSchema.VoucherMaster.IS_AUDITOR_MODIFIEDColumn.ColumnName].ToString());
                    IsVoucherModifiedByAuditor = (isvouchermodifiedbyauditor == 1);
                }

                if (dtAuditVoucher.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName] != DBNull.Value)
                {
                    PrevAuditVoucherDate = this.DateSet.ToDate(dtAuditVoucher.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName].ToString(), false);
                }
                PrevAuditProjectId = this.NumberSet.ToInteger(dtAuditVoucher.Rows[0][this.AppSchema.VoucherMaster.PROJECT_IDColumn.ColumnName].ToString());
                PrevAuditProjectName = dtAuditVoucher.Rows[0][this.AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                PrevAuditVoucherNo = dtAuditVoucher.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_NOColumn.ColumnName].ToString();
                PrevAuditAmount = this.NumberSet.ToDecimal(dtAuditVoucher.Rows[0][this.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString()); ;
                PrevAuditVoucherType = dtAuditVoucher.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_TYPEColumn.ColumnName].ToString();
                PrevAuditVoucherSubType = dtAuditVoucher.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_SUB_TYPEColumn.ColumnName].ToString();
                PrevAuditModifiedBy = this.NumberSet.ToInteger(dtAuditVoucher.Rows[0][this.AppSchema.VoucherMaster.MODIFIED_BYColumn.ColumnName].ToString());
                PrevAuditModifiedByName = dtAuditVoucher.Rows[0][this.AppSchema.VoucherMaster.MODIFIED_BY_NAMEColumn.ColumnName].ToString();
                //If not yet modified, let us take created details
                if (PrevAuditModifiedBy == 0) PrevAuditModifiedBy = this.NumberSet.ToInteger(dtAuditVoucher.Rows[0][this.AppSchema.VoucherMaster.CREATED_BYColumn.ColumnName].ToString()); ;
                if (string.IsNullOrEmpty(PrevAuditModifiedByName)) PrevAuditModifiedByName = dtAuditVoucher.Rows[0][this.AppSchema.VoucherMaster.CREATED_BY_NAMEColumn.ColumnName].ToString();
            }
            else if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count == 0)
            {
                PrevAuditVoucherDate = VoucherDate;
                PrevAuditProjectId = ProjectId;
                PrevAuditProjectName = GetProjectName(ProjectId);
                PrevAuditVoucherNo = VoucherNo;
                PrevAuditAmount = Amount;
                PrevAuditVoucherType = VoucherType;
                PrevAuditVoucherSubType = VoucherSubType;
                PrevAuditModifiedBy = 0; //ModifiedBy;
                PrevAuditModifiedByName = "";
            }
            return resultArgs;
        }

        private string GetProjectName(Int32 pid)
        {
            string rtn = string.Empty;
            using (ProjectSystem projectsystem = new ProjectSystem(pid))
            {
                rtn = projectsystem.ProjectName;
            }
            return rtn;
        }
        /// <summary>
        /// On 01/09/2021, To assign Audit details
        /// </summary>
        private void AssignVoucherAuditDetails()
        {
            //On 25/08/2021, To fix User details by dfault ---------------------------------------------
            CreatedBy = this.NumberSet.ToInteger(this.LoginUserId.ToString());
            ModifiedBy = this.NumberSet.ToInteger(this.LoginUserId.ToString());

            CreatedByName = FirstName; //LoginUserName.ToString();
            ModifiedByName = FirstName; //LoginUserName.ToString();
            //------------------------------------------------------------------------------------------

            //On 01/09/2021, To set mode Voucher Modified by Auditor -----------------------------------
            //for other users, it will be leave as it is
            //On 01/09/2021, To set mode Voucher Modified by Auditor -----------------------------------
            //for other users, it will be leave as it is
            IsVoucherModifiedByAuditor = false;
            if (this.IsLoginUserAuditor)
            {
                IsVoucherModifiedByAuditor = true;
            }
            else if (VoucherId > 0)
            {
                //IsVoucherModifiedByAuditor = HasVoucherModifiedByAuditor(VoucherId);
                //If voucher is exists, get existing audit details 
                using (DataManager dataManger = new DataManager(SQLCommand.VoucherMaster.FetchVoucherAuditUserDetails))
                {
                    dataManger.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                    resultArgs = dataManger.FetchData(DataSource.DataTable);
                }
                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    int isvouchermodifiedbyauditor = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.IS_AUDITOR_MODIFIEDColumn.ColumnName].ToString());
                    IsVoucherModifiedByAuditor = (isvouchermodifiedbyauditor == 1);

                    CreatedBy = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.CREATED_BYColumn.ColumnName].ToString());
                    CreatedByName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.CREATED_BY_NAMEColumn.ColumnName].ToString();

                    IsEnableVoucherChangesHistory = (CreatedBy != this.NumberSet.ToInteger(this.LoginUserId.ToString()));
                }
            }
            //--------------------------------------------------------------------------------------------
        }

        /// <summary>
        /// On 02/09/2021, To Maintain Audit Log Voucher History for Vouchers that are modified by Auditor user
        /// Pre-condition 1: Recent Voucher Amount changes history alone will be maintained only for concern 
        ///                  Vouchers which were modified/created by Auditor User
        /// Pre-condition 2: We plan to maintain only two recent history alone 1. Recent Auditor Modifed amount 2. Recent Generl User modified amount after Auditor changes
        /// Pre-condition 3: This method will be called at first time when Auditor modified voucher/whenever Auditor modified voucher gets updated by other general user.
        /// Pre-condition 4: Only Recent Auditor history, after that history, recent general user history
        /// 
        /// On 03/08/2023 : For keep voucher changes history - We shall make use of existing auditor track features)
        ///                 To track changes if vouhcer is modified by other than created user
        /// </summary>
        public ResultArgs UpdateAuditLogHistory(DataManager dm, AuditAction auditaction)
        {
            bool IsexistauditLogHistoryExitsByUser = false;
            resultArgs.Success = true;

            try
            {
                //1. Check Auditor history available for user as we plan to have only two history alone to be maintained like (1. Auditor 2. General User)
                //If and if only when Auditor user changes voucher amount for a particular voucher, history will be maintained for those voucher alone (next who chagned or modifed it). 
                AssignVoucherAuditDetails();

                //2. Update Audit Log History - Logged user Auditor or current voucher is already modified by Auditor
                // If and If only voucher value changed
                if (isVoucherValueChanged() && (this.IsLoginUserAuditor || IsVoucherModifiedByAuditor || IsEnableVoucherChangesHistory))
                {
                    //3. Check if Audit Log History is availalbe or not
                    //resultArgs = IsExistAuditLogHistoryExitsByUserOrAuditor( (IsVoucherModifiedByAuditor? true:false) );
                    resultArgs = IsExistAuditLogHistoryExitsByUserOrAuditor(false);
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        Int32 vid = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString());
                        IsexistauditLogHistoryExitsByUser = (vid > 0);
                    }

                    using (DataManager dataManager = new DataManager((IsexistauditLogHistoryExitsByUser ? SQLCommand.VoucherMaster.UpdateVoucherAuditLogHistory :
                                                   SQLCommand.VoucherMaster.InsertVoucherAuditLogHistory)))
                    {
                        dataManager.Database = dm.Database;
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_DATEColumn, PrevAuditVoucherDate);
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, PrevAuditProjectId);
                        dataManager.Parameters.Add(this.AppSchema.Project.PROJECTColumn, PrevAuditProjectName);
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_NOColumn, PrevAuditVoucherNo);
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_TYPEColumn, PrevAuditVoucherType);
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_SUB_TYPEColumn, PrevAuditVoucherSubType);
                        dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.AMOUNTColumn, (auditaction == AuditAction.Deleted ? PrevAuditAmount : Amount));
                        dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.PREVIOUS_AMOUNTColumn, PrevAuditAmount);
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.MODIFIED_BYColumn, ModifiedBy);
                        dataManager.Parameters.Add("1MODIFIED_BY_NAME", ModifiedByName, DataType.Varchar); //For Temp
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PREVIOUS_MODIFIED_BYColumn, PrevAuditModifiedBy);
                        dataManager.Parameters.Add("1PREVIOUS_MODIFIED_BY_NAME", PrevAuditModifiedByName, DataType.Varchar); //For Temp
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.ACTIONColumn, auditaction.ToString());
                        dataManager.Parameters.Add(this.AppSchema.VoucherMaster.IS_AUDITOR_MODIFIEDColumn, (IsLoginUserAuditor ? 1 : 0));
                        //dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        resultArgs = dataManager.UpdateData();
                    }

                    //On 13/09/2021, To update Voucher modified falg in Vouhcer master after updating voucher auditor log
                    if (resultArgs != null && resultArgs.Success && (this.IsLoginUserAuditor || IsVoucherModifiedByAuditor))
                    {
                        UpdateAuditorModifiedFlag(dm);
                    }
                }
            }
            catch (Exception err)
            {
                resultArgs.Message = "Problem in updating Audit Log " + err.Message;
            }

            return resultArgs;
        }

        /// <summary>
        /// On 13/09/2021, To update Voucher modified falg in Vouhcer master after updating voucher auditor log
        /// </summary>
        /// <param name="dm"></param>
        /// <returns></returns>
        public ResultArgs UpdateAuditorModifiedFlag(DataManager dm)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.UpdateAuditorModifiedFlag))
                {
                    dataManager.Database = dm.Database;
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception err)
            {
                resultArgs.Message = "Problem in Update Voucher modified flag" + err.Message;
            }
            return resultArgs;
        }


        /// <summary>
        /// On 29/07/2024, To Delete and Update Attached Voucher files for concern Voucher
        /// </summary>
        /// <param name="dm"></param>
        /// <returns></returns>
        private ResultArgs UpdateVoucherFileDetailsByVoucher(DataManager dm)
        {
            try
            {
                string VoucherUploadPath = Path.Combine(SettingProperty.ApplicationStartUpPath, SettingProperty.Folder_VoucherFiles);
                if (base.AttachVoucherFiles == 1 && dtVoucherFiles != null)
                {
                    resultArgs = DeleteVoucherImageDetailsByVoucher(dm);

                    if (resultArgs.Success)
                    {

                        foreach (DataRow dr in dtVoucherFiles.Rows)
                        {
                            Int32 ImageRowNumber = dtVoucherFiles.Rows.IndexOf(dr) + 1;
                            string actualfilenmae = dr[this.AppSchema.VoucherMaster.ACTUAL_FILE_NAMEColumn.ColumnName].ToString();
                            string filename = this.PrefixVoucherImageName + "_" + ImageRowNumber.ToString() + "_" + "V" + VoucherId.ToString() + Path.GetExtension(actualfilenmae);
                            VoucherUploadPath = Path.Combine(VoucherUploadPath, filename);
                            string remark = dr[this.AppSchema.VoucherMaster.REMARKColumn.ColumnName].ToString();
                            byte[] arrVoucherIamge = null;
                            if (dr[this.AppSchema.VoucherMaster.VOUCHER_FILEColumn.ColumnName] != DBNull.Value)
                            {
                                arrVoucherIamge = (byte[])dr[this.AppSchema.VoucherMaster.VOUCHER_FILEColumn.ColumnName];
                            }

                            //Bitmap bmp = ImageProcessing.ByteArrayToImage(arrVoucherIamge);
                            //arrVoucherIamge = ImageProcessing.ImageToByteArray(CompressImage(bmp));

                            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.UpdateVoucherFileDetailsByVoucher))
                            {
                                dataManager.Database = dm.Database;
                                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.SEQUENCE_NOColumn, ImageRowNumber);
                                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.FILE_NAMEColumn, filename);
                                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.ACTUAL_FILE_NAMEColumn, actualfilenmae);
                                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.REMARKColumn, remark);
                                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.BRANCH_IDColumn, 0, DataType.Int32); //Default 0 in branch application
                                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.LOCATION_IDColumn, 0, DataType.Int32); //Default 0 in branch application

                                //dataManager.Parameters.Add(this.AppSchema.User.USER_PHOTOColumn, arrVoucherIamge);

                                //dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                                resultArgs = dataManager.UpdateData();

                                if (resultArgs.Success)
                                {
                                    resultArgs = SaveVoucherFileToPath(filename, arrVoucherIamge);
                                }

                                if (!resultArgs.Success)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    resultArgs.Success = true;
                }
            }
            catch (Exception err)
            {
                resultArgs.Message = "Problem in Update Voucher Voucher files " + err.Message;
            }

            return resultArgs;
        }

        /// <summary>
        /// On 29/07/2024, To delete voucher files by voucher
        /// </summary>
        /// <param name="dm"></param>
        /// <returns></returns>
        private ResultArgs DeleteVoucherImageDetailsByVoucher(DataManager dm)
        {
            try
            {
                resultArgs = RemoveVoucherFileFromPathByVoucher();
                if (resultArgs.Success)
                {
                    using (DataManager dataManagerDelete = new DataManager(SQLCommand.VoucherMaster.DeleteVoucherFileDetailsByVoucher))
                    {
                        dataManagerDelete.Database = dm.Database;
                        dataManagerDelete.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                        resultArgs = dataManagerDelete.UpdateData();
                    }
                }
            }
            catch (Exception err)
            {
                resultArgs.Message = "Problem in deleting Voucher file " + err.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// On 29/07/2024, To get voucher files by voucher
        /// </summary>
        /// <param name="dm"></param>
        /// <returns></returns>
        public ResultArgs FetchVoucherFilesByVoucher()
        {
            try
            {
                dtVoucherFiles.Rows.Clear();
                //if (base.AttachVoucherFiles == 1)
                //{
                using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchVoucherFileDetailsByVoucher))
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        DataTable dtvoucherimage = resultArgs.DataSource.Table;
                        dtVoucherFiles.Clear();
                        foreach (DataRow dr in dtvoucherimage.Rows)
                        {
                            byte[] imgArray = null;
                            string VoucherUploadPath = Path.Combine(SettingProperty.ApplicationStartUpPath, SettingProperty.Folder_VoucherFiles);
                            string filename = dr[AppSchema.VoucherMaster.FILE_NAMEColumn.ColumnName].ToString();
                            string actualfilename = dr[AppSchema.VoucherMaster.ACTUAL_FILE_NAMEColumn.ColumnName].ToString();
                            VoucherUploadPath = Path.Combine(VoucherUploadPath, filename);
                            //string fileactualimage = dr[AppSchema.VoucherMaster.ACTUAL_FILE_NAMEColumn.ColumnName].ToString();
                            string remark = dr[AppSchema.VoucherMaster.REMARKColumn.ColumnName].ToString();

                            if (File.Exists(VoucherUploadPath) && Path.GetExtension(actualfilename) == ".pdf")
                            {
                                //Image img = Image.FromFile(VoucherUploadPath);
                                imgArray = File.ReadAllBytes(VoucherUploadPath);
                            }
                            else
                            {
                                imgArray = ImageProcessing.ImageToByteArray(VoucherUploadPath);
                            }
                            AttachVoucherFiles(VoucherId, filename, actualfilename, imgArray, remark, VoucherUploadPath);
                        }
                    }
                }
                //}
                resultArgs.DataSource.Data = dtVoucherFiles;
                resultArgs.Success = true;
            }
            catch (Exception err)
            {
                resultArgs.Message = "Problem in getting Voucher File " + err.Message;
            }
            return resultArgs;
        }

        public ResultArgs IsExistsVoucherFiles()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.IsExistsVoucherFiles))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        resultArgs.Success = (resultArgs.DataSource.Table.Rows.Count > 0);
                    }
                }
            }
            catch (Exception err)
            {
                resultArgs.Message = "Problem in getting Voucher File " + err.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// On 06/08/2024, To check voucher files exits
        /// </summary>
        /// <param name="Voucherid"></param>
        /// <returns></returns>
        public ResultArgs IsExistsVoucherFiles(Int32 Voucherid)
        {
            ResultArgs resultarg = new ResultArgs();
            VoucherId = Voucherid;
            try
            {

                resultarg = FetchVoucherFilesByVoucher();
                if (resultarg.Success)
                {
                    resultarg.Success = resultarg.RowsAffected > 0;
                }

            }
            catch (Exception ex)
            {
                resultarg.Message = ex.ToString();
            }

            return resultarg;
        }

        /// <summary>
        /// On 31/07/2024, To save Voucher image into Applocation path
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private ResultArgs SaveVoucherFileToPath(string filename, byte[] VoucherImage) //Image img
        {
            try
            {
                if (base.AttachVoucherFiles == 1)
                {
                    string VoucherUploadPath = Path.Combine(SettingProperty.ApplicationStartUpPath, SettingProperty.Folder_VoucherFiles);
                    VoucherUploadPath = Path.Combine(VoucherUploadPath, filename);
                    if (VoucherImage != null)
                    {
                        File.WriteAllBytes(VoucherUploadPath, VoucherImage);
                    }
                    //img.Save(VoucherUploadPath, ImageFormat.Jpeg);
                }
                resultArgs.Success = true;
            }
            catch (FileNotFoundException)
            {
                resultArgs.Message = "The file or directory cannot be found.";
            }
            catch (DirectoryNotFoundException)
            {
                resultArgs.Message = "The file or directory cannot be found.";
            }
            catch (PathTooLongException)
            {
                resultArgs.Message = "'path' exceeds the maximum supported path length.";
            }
            catch (UnauthorizedAccessException)
            {
                resultArgs.Message = "You do not have permission to create this file.";
            }
            catch (IOException e)
            {
                int HResult = System.Runtime.InteropServices.Marshal.GetHRForException(e);
                if ((HResult & 0xFFFF) == 32)
                {
                    resultArgs.Message = "File is already opened or it is being used.";
                }
            }
            catch (Exception err)
            {
                resultArgs.Message = "Problem in saving Voucher file into the Path" + err.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// On 31/07/2024, To delete Voucher image from Applocation path
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        private ResultArgs RemoveVoucherFileFromPathByVoucher()
        {
            try
            {
                if (VoucherId > 0)
                {
                    string VoucherUploadPath = Path.Combine(SettingProperty.ApplicationStartUpPath, SettingProperty.Folder_VoucherFiles);

                    string[] voucherfiles = Directory.GetFiles(VoucherUploadPath, "*_V" + VoucherId.ToString() + ".*");

                    foreach (string file in voucherfiles)
                    {
                        if (File.Exists(file))
                        {
                            File.Delete(file);
                        }
                    }

                    resultArgs.Success = true;
                }
                else
                    resultArgs.Success = true;
            }
            catch (FileNotFoundException)
            {
                resultArgs.Message = "The file or directory cannot be found.";
            }
            catch (DirectoryNotFoundException)
            {
                resultArgs.Message = "The file or directory cannot be found.";
            }
            catch (PathTooLongException)
            {
                resultArgs.Message = "'path' exceeds the maximum supported path length.";
            }
            catch (UnauthorizedAccessException)
            {
                resultArgs.Message = "You do not have permission to create this file.";
            }
            catch (IOException e)
            {
                int HResult = System.Runtime.InteropServices.Marshal.GetHRForException(e);
                if ((HResult & 0xFFFF) == 32)
                {
                    resultArgs.Message = "File is already opened or it is being used.";
                }
            }
            catch (Exception err)
            {
                resultArgs.Message = "Problem in deleting Voucher File from Path" + err.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// On 01/08/2024, To upload vouchers to Portal
        /// </summary>
        /// <returns></returns>
        private ResultArgs UploadVoucherFileToAcmeerpServer()
        {
            ResultArgs result = new ResultArgs();

            try
            {
                if (base.AttachVoucherFiles == 1)
                {
                    foreach (DataRow dr in dtVoucherFiles.Rows)
                    {
                        Int32 ImageRowNumber = dtVoucherFiles.Rows.IndexOf(dr) + 1;
                        string VoucherUploadPath = Path.Combine(SettingProperty.ApplicationStartUpPath, SettingProperty.Folder_VoucherFiles);
                        string actualfilenmae = dr[this.AppSchema.VoucherMaster.ACTUAL_FILE_NAMEColumn.ColumnName].ToString();
                        string filename = this.PrefixVoucherImageName + "_" + ImageRowNumber.ToString() + "_" + "V" + VoucherId.ToString() + Path.GetExtension(actualfilenmae);
                        VoucherUploadPath = Path.Combine(VoucherUploadPath, filename);
                        string remark = dr[this.AppSchema.VoucherMaster.REMARKColumn.ColumnName].ToString();
                        //Image img = dr[this.AppSchema.VoucherMaster.VOUCHER_IMAGEColumn.ColumnName] as Image;

                        if (dr[this.AppSchema.VoucherMaster.VOUCHER_FILEColumn.ColumnName] != DBNull.Value)
                        {
                            byte[] arrVoucherIamge = (byte[])dr[this.AppSchema.VoucherMaster.VOUCHER_FILEColumn.ColumnName];

                            using (MemoryStream strmVoucherImage = new MemoryStream(arrVoucherIamge))
                            {
                                using (AcMEERPFTP ftpFileTransfer = new AcMEERPFTP())
                                {
                                    result = ftpFileTransfer.uploadDatabyWebClient(BranchUploadAction.BranchReport, this.HeadofficeCode, this.PartBranchOfficeCode,
                                        this.Location, filename, "", VoucherDate.Year.ToString(), strmVoucherImage, string.Empty);
                                    if (!result.Success)
                                    {
                                        string responsemsg = result.Message;
                                    }
                                }
                            }
                        }
                    }
                    result.Success = true;
                }
                else
                {
                    result.Success = true;
                }
            }
            catch (Exception err)
            {
                result.Message = "Problem in saving Voucher File into the Path" + err.Message;
            }
            return result;
        }


        /// <summary>
        /// On 06/09/2021, To check is voucher values changed to maintain Audit Log History
        /// </summary>
        /// <returns></returns>
        private bool isVoucherValueChanged()
        {
            bool rtn = false;

            if ((VoucherDate != PrevAuditVoucherDate) || (ProjectId != PrevAuditProjectId) || (VoucherNo != PrevAuditVoucherNo) ||
                (VoucherType != PrevAuditVoucherType) || (VoucherSubType != PrevAuditVoucherSubType) || (Amount != PrevAuditAmount))
            {
                rtn = true;
            }

            return rtn;
        }

        /// <summary>
        /// On 02/09/2021, Get if exists of Audit log history available for Audior user and General user
        /// </summary>
        /// <returns></returns>
        private ResultArgs IsExistAuditLogHistoryExitsByUserOrAuditor(bool Audit)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.IsExistAuditLogHistoryExitsByUserOrAuditor))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                if (Audit)
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.IS_AUDITOR_MODIFIEDColumn, (IsLoginUserAuditor ? 1 : 0));
                else
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.MODIFIED_BYColumn, ModifiedBy);

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 22/09/2021, To Check Voucher date is locked
        /// </summary>
        /// <returns></returns>
        private bool IsAuditLockedVoucherDate(int projectid, DateTime voucherdate)
        {
            bool rtn = false;
            try
            {
                ResultArgs Result = new ResultArgs();
                using (AuditLockTransSystem AuditSystem = new AuditLockTransSystem())
                {
                    AuditSystem.ProjectId = projectid;
                    AuditSystem.VoucherDateTo = voucherdate;

                    Result = AuditSystem.FetchAuditLockDetailsForProjectAndDate();
                    if (Result != null && Result.Success && Result.DataSource.Table != null && Result.DataSource.Table.Rows.Count > 0)
                    {
                        rtn = true;
                        LockFromDate = DateSet.ToDate(Result.DataSource.Table.Rows[0][AuditSystem.AppSchema.AuditLockTransType.DATE_FROMColumn.ColumnName].ToString(), false);
                        LockToDate = DateSet.ToDate(Result.DataSource.Table.Rows[0][AuditSystem.AppSchema.AuditLockTransType.DATE_TOColumn.ColumnName].ToString(), false);
                        LockProjectName = Result.DataSource.Table.Rows[0][AuditSystem.AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                    }
                    else
                    {
                        rtn = false;
                        LockProjectName = string.Empty;
                        LockFromDate = DateTime.MinValue;
                        LockToDate = DateTime.MinValue;
                    }
                }

                //On 07/02/2024, To lock vouchers based on defined grace days in portal
                if (this.IS_SDB_INM && this.VoucherGraceDays > 0)
                {
                    //Check temporary relaxation
                    bool isEnforceTmpRelaxation = this.IsTemporaryGraceLockRelaxDate(voucherdate);

                    string pname = this.GetProjectName(projectid);
                    //rtn = (this.GraceLockDateFrom <= voucherdate) && (voucherdate <= this.GraceLockDateTo) && !isEnforceTmpRelaxation; 
                    rtn = (this.GraceLockDateFrom <= voucherdate) && (voucherdate < this.GraceLockDateTo) && !isEnforceTmpRelaxation;
                    if (rtn && !string.IsNullOrEmpty(pname))
                    {
                        LockProjectName = pname;
                        LockFromDate = this.GraceLockDateFrom;
                        LockToDate = this.GraceLockDateTo;
                    }
                    //LockToDate = DateSet.ToDate(Result.DataSource.Table.Rows[0][AuditSystem.AppSchema.AuditLockTransType.DATE_TOColumn.ColumnName].ToString(), false);
                    //LockProjectName = Result.DataSource.Table.Rows[0][AuditSystem.AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage("Unable to Check Voucher date is locked - " + ex.Message + Environment.NewLine + ex.Source);
                rtn = true;
            }
            finally
            {

            }
            return rtn;
        }

        /// <summary>
        /// On 21/10/2021, To Check valid vouchers
        /// 1. Sum of cr amont with sum of dr amount
        /// 2. check ledgers closed date
        /// </summary>
        /// <returns></returns>
        private ResultArgs ValidateVouchers()
        {
            try
            {
                bool valid = true;
                resultArgs.Success = true;
                //1. Check Vouchers amount ledger amount with cash/bank amount
                double dramount = 0;
                double cramount = 0;
                if (TransInfo != null && CashTransInfo != null)
                {
                    if (VoucherType == "JN")
                    {
                        dramount = NumberSet.ToDouble(TransInfo.Table.Compute("SUM(CREDIT)", string.Empty).ToString());
                        cramount = NumberSet.ToDouble(TransInfo.Table.Compute("SUM(DEBIT)", string.Empty).ToString());
                    }
                    else
                    {
                        double transCr = 0;
                        double transDr = 0;
                        double cashbankCr = 0;
                        double cashbankDr = 0;


                        if (TransInfo.Table.Columns.Contains("SOURCE"))
                        {
                            transCr = NumberSet.ToDouble(TransInfo.Table.Compute("SUM(AMOUNT)", "SOURCE = 1").ToString());
                            transDr = NumberSet.ToDouble(TransInfo.Table.Compute("SUM(AMOUNT)", "SOURCE = 2").ToString());
                        }
                        else if (TransInfo.Table.Columns.Contains("TRANS_MODE"))
                        {
                            transCr = NumberSet.ToDouble(TransInfo.Table.Compute("SUM(AMOUNT)", "TRANS_MODE='CR' OR TRANS_MODE='FD'").ToString());
                            transDr = NumberSet.ToDouble(TransInfo.Table.Compute("SUM(AMOUNT)", "TRANS_MODE='DR'").ToString());
                        }
                        else
                        {
                            transCr = NumberSet.ToDouble(TransInfo.Table.Compute("SUM(AMOUNT)", string.Empty).ToString());
                        }

                        if (CashTransInfo.Table.Columns.Contains("SOURCE"))
                        {
                            cashbankCr = NumberSet.ToDouble(CashTransInfo.Table.Compute("SUM(AMOUNT)", "SOURCE = 1").ToString());
                            cashbankDr = NumberSet.ToDouble(CashTransInfo.Table.Compute("SUM(AMOUNT)", "SOURCE = 2").ToString());
                        }
                        else if (CashTransInfo.Table.Columns.Contains("TRANS_MODE"))
                        {
                            cashbankCr = NumberSet.ToDouble(CashTransInfo.Table.Compute("SUM(AMOUNT)", "TRANS_MODE='CR'").ToString());
                            cashbankDr = NumberSet.ToDouble(CashTransInfo.Table.Compute("SUM(AMOUNT)", "TRANS_MODE='DR' OR TRANS_MODE='FD'").ToString());
                        }
                        else
                        {
                            transDr = NumberSet.ToDouble(CashTransInfo.Table.Compute("SUM(AMOUNT)", string.Empty).ToString());
                        }


                        dramount = transCr + cashbankCr;
                        cramount = transDr + cashbankDr;
                    }
                }

                // On 25/11/2022, To have common decimal ponit for Transactoin and Cash and Bank 
                //if (cramount != dramount) //On 23/11/2024, for multi currency contra, let us allow different amount for differnt currency ledgers
                if ((cramount.ToString("n2") != dramount.ToString("n2"))
                    && (this.AllowMultiCurrency == 0 || (VoucherType != DefaultVoucherTypes.Contra.ToString() && VoucherType != VoucherSubTypes.CN.ToString())))
                {
                    resultArgs.Message = MessageRender.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_AMOUNT_MISMATCH);
                    valid = false;
                }
                else if (VoucherType != "JN" && cramount == 0 && dramount == 0) //On 21/06/2024, if both cr and dr are zero, let us alert
                {
                    resultArgs.Message = MessageRender.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_AMOUNT_MISMATCH);
                    valid = false;
                }

                //2. Check Ledger Closed date
                if (valid)
                {
                    resultArgs = ValidateVoucherLedgers();
                    valid = resultArgs.Success;
                }

            }
            catch (Exception err)
            {
                resultArgs.Message = err.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// On 21/10/2021, to check ledgers clsoed date
        /// </summary>
        /// <returns></returns>
        private ResultArgs ValidateVoucherLedgers()
        {
            try
            {
                //1. Check General Ledgers's closed date
                if (TransInfo != null)
                {
                    resultArgs = CheckVoucherLedgers(TransInfo);
                }

                //2. Check Cash/Bank ledger closed date
                if (VoucherType != "JN" && resultArgs.Success && CashTransInfo != null)
                {
                    resultArgs = CheckVoucherLedgers(CashTransInfo, true);
                }
            }
            catch (Exception err)
            {
                resultArgs.Message = err.Message;
            }

            return resultArgs;
        }

        /// <summary>
        /// On 25/10/2021, To check Voucher Ledgers id, amount and Clsoed date
        /// 
        /// and check if currency for cash and bank too if multi currency enabled 
        /// </summary>
        /// <returns></returns>
        private ResultArgs CheckVoucherLedgers(DataView dv, bool isCashBank = false)
        {
            int Id = 0;
            decimal Amt = 0;
            decimal AmtCR = 0;
            decimal AmtDR = 0;
            decimal ExchangeAmt = 0;
            decimal LiveExchangeAmt = 0;
            bool isValid = false;

            DateTime dtClosedDate = DateTime.MinValue;
            string validateMessage = string.Empty;

            try
            {
                //On 21/06/2024, if there is no ledgers 
                //if (dv != null && dv.Count > 0)
                if (dv != null && dv.Count > 0)
                {
                    isValid = true;
                    foreach (DataRowView drTrans in dv)
                    {
                        Id = this.NumberSet.ToInteger(drTrans["LEDGER_ID"].ToString());
                        if (VoucherType == "JN")
                        {
                            AmtCR = NumberSet.ToDecimal(drTrans["CREDIT"].ToString());
                            AmtDR = NumberSet.ToDecimal(drTrans["DEBIT"].ToString());
                        }
                        else
                            Amt = this.NumberSet.ToDecimal(drTrans["AMOUNT"].ToString());

                        if (this.AllowMultiCurrency == 1)
                        {
                            ExchangeAmt = this.NumberSet.ToDecimal(drTrans["EXCHANGE_RATE"].ToString());
                            LiveExchangeAmt = this.NumberSet.ToDecimal(drTrans["LIVE_EXCHANGE_RATE"].ToString());
                        }

                        resultArgs = CheckLedgerClosed(Id, VoucherDate);

                        if ((Id == 0 || (Amt == 0 && (AmtCR == 0 && AmtDR == 0))) || (!resultArgs.Success))
                        {
                            //19/06/2024, To allow cash and bank zero value only for Receipt and Payment
                            bool allowzerovaluedledger = (AllowZeroValuedCashBankVoucherEntry && isCashBank &&
                                            (VoucherType == DefaultVoucherTypes.Receipt.ToString() || VoucherType == DefaultVoucherTypes.Payment.ToString()));
                            if (Id == 0)
                            {
                                validateMessage = MessageRender.GetMessage(MessageCatalog.Transaction.VocherTransaction.REQUIRED_INFORMATION_LEDGER_EMPTY);
                            }
                            else if (Amt == 0 && (AmtCR == 0 && AmtDR == 0))
                            {
                                if (!allowzerovaluedledger)
                                {
                                    validateMessage = MessageRender.GetMessage(MessageCatalog.Transaction.VocherTransaction.REQUIRED_INFORMATION_AMOUNT_EMPTY);
                                }
                            }
                            else if (!resultArgs.Success)
                            {
                                validateMessage = resultArgs.Message;
                            }

                            if (!string.IsNullOrEmpty(validateMessage))
                            {
                                resultArgs.Message = validateMessage;
                                isValid = false;
                                break;
                            }
                        }

                        //On 27/08/2024, For Multi Currency,  check cash and bank Currency with voucher currency
                        if (AllowMultiCurrency == 1)
                        {
                            if ((ExchangeAmt != ExchangeRate) &&
                                 (this.AllowMultiCurrency == 0 || !isCashBank ||
                                 (VoucherType != DefaultVoucherTypes.Contra.ToString() && VoucherType != VoucherSubTypes.CN.ToString())))
                            {
                                resultArgs.Message = "Mismatching Voucher Exchange Rate with Ledger Exchange Rate.";
                                isValid = false;
                                break;
                            }
                            else if (VoucherType == DefaultVoucherTypes.Contra.ToString() || VoucherType == VoucherSubTypes.CN.ToString())
                            {
                                if (dv.Count > 1)
                                {
                                    resultArgs.Message = "Single Cash/Bank Ledger is allowed for Contra Voucher.";
                                    isValid = false;
                                    break;
                                }
                            }

                            if (isValid)
                            {
                                if ((isCashBank && (VoucherType != DefaultVoucherTypes.Contra.ToString() && VoucherType != VoucherSubTypes.CN.ToString())) ||
                                    (!isCashBank && (VoucherType == DefaultVoucherTypes.Contra.ToString() || VoucherType == VoucherSubTypes.CN.ToString())))
                                {
                                    using (LedgerSystem ledgersystem = new LedgerSystem(Id))
                                    {
                                        if (ledgersystem.GroupId > 0 && (ledgersystem.GroupId == (int)FixedLedgerGroup.Cash || ledgersystem.GroupId == (int)FixedLedgerGroup.BankAccounts
                                            || ledgersystem.GroupId == (int)FixedLedgerGroup.FixedDeposit))
                                        {
                                            isValid = (ledgersystem.LedgerCurrencyCountryId == CurrencyCountryId);
                                            if (!isValid)
                                            {
                                                resultArgs.Message = "Mismatching Cash/Bank Ledger's Currency with Voucher Country.";
                                                break;
                                            }
                                        }
                                    }
                                }
                            }

                            //VoucherType != VoucherSubTypes.JN.ToString() &&  
                            if (isValid && !(CurrencyCountryId > 0 && ExchangeAmt > 0 && LiveExchangeAmt > 0 && ContributionAmount > 0 && ExchangeRate > 0 && ActualAmount > 0))
                            {
                                isValid = false;
                                resultArgs.Message = "As Multi Currency option is enabled, All the Currecny details should be filled.";
                            }
                        }

                    }
                }
                else //On 21/06/2024, if there is no ledgers 
                {
                    resultArgs.Message = MessageRender.GetMessage(MessageCatalog.Transaction.VocherTransaction.REQUIRED_INFORMATION_LEDGER_EMPTY); ;
                    isValid = false;
                }
            }
            catch (Exception err)
            {
                resultArgs.Message = err.Message;
            }

            return resultArgs;
        }

        /// <summary>
        /// On 25/10/2021, To check given leger closed date, if exists validate with voucher date
        /// Initaly Closed date was available only for Bank Ledgers, so for Bank Ledgers, we have to validate to Bank Accounts
        /// for other Ledgers, we have to check in Legers
        /// </summary>
        /// <returns></returns>
        public ResultArgs CheckLedgerClosed(Int32 LId, DateTime VDate)
        {
            int GrpLegerId = 0;
            DateTime dtClosedDate = DateTime.MinValue;

            try
            {
                GrpLegerId = GetLedgerGroupId(LId);
                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    if ((GrpLegerId == (Int32)FixedLedgerGroup.BankAccounts)) //For Bank Ledger alone
                    {
                        dtClosedDate = ledgersystem.GetBankLedgerClosedDate(LId);
                    }
                    else //For Cash or FD ledger
                    {
                        dtClosedDate = ledgersystem.GetLedgerClosedDate(LId);
                    }
                }

                if ((!(dtClosedDate >= VDate)) && (!(dtClosedDate == DateTime.MinValue)))
                {
                    string ledgername = string.Empty;
                    using (LedgerSystem ledgersystem = new LedgerSystem())
                    {
                        ledgername = ledgersystem.GetLegerName(LId);
                    }
                    resultArgs.Message = "Ledger '" + ledgername + "' is closed on '" + dtClosedDate.ToShortDateString() + "'";
                }
            }
            catch (Exception err)
            {
                resultArgs.Message = err.Message;
            }
            return resultArgs;
        }

        private string GetLedgerName(int LedId)
        {
            string LedgerName = string.Empty;
            using (LedgerSystem LedgerSystem = new LedgerSystem())
            {
                LedgerSystem.LedgerId = LedId;
                resultArgs = LedgerSystem.FetchLedgersByLedgerId();
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    LedgerName = resultArgs.DataSource.Table.Rows[0][LedgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                }
            }

            return LedgerName;
        }

        private DataRow GetLedgerDetails(int LedId)
        {
            DataRow dr = null;
            using (LedgerSystem LedgerSystem = new LedgerSystem())
            {
                LedgerSystem.LedgerId = LedId;
                resultArgs = LedgerSystem.FetchLedgersByLedgerId();
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    dr = resultArgs.DataSource.Table.Rows[0];
                }
            }

            return dr;
        }
        /// <summary>
        /// On 07/02/2023, to reindex the following tables
        /// 1. Master Voucher Trans
        /// 
        /// </summary>
        /// <returns></returns>
        public ResultArgs MakeReindex()
        {
            ResultArgs result = new ResultArgs();
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.ReindexTables))
                {
                    result = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.Source);
            }
            finally { }

            return result;
        }


        #endregion

        #region GST Vendor Invoice Details

        /// <summary>
        /// On 13/12/202, once again Voucher GST Ledgers amount to GST Invoice leger details if anything changed
        /// </summary>
        public ResultArgs AttachVoucherLedgetsToGSTInvoiceLedgerDetails(bool entry, string vtype, DataTable dtGSTInvoiceLedgers, DataTable dtVoucherGSTLegers)
        {
            bool isgeneralinvoice = (this.AllowMultiCurrency == 1 || this.IsCountryOtherThanIndia);
            string amounts = string.Empty;
            DataTable dtHSNCodes = new DataTable();
            //Assing GST Ledger details
            try
            {
                if (!string.IsNullOrEmpty(GST_VENDOR_INVOICE_NO) || entry)
                {
                    using (VoucherTransactionSystem sysvoucher = new VoucherTransactionSystem())
                    {
                        if (dtGSTInvoiceLedgers == null)
                        {
                            dtGSTInvoiceLedgers = sysvoucher.AppSchema.GSTInvoiceMasterLedgerDetails.DefaultView.ToTable();
                        }

                        if (!isgeneralinvoice)
                        {
                            if (dtGSTInvoiceLedgers.Columns.Contains(sysvoucher.AppSchema.VoucherTransaction.MODIFIEDColumn.ColumnName))
                                dtGSTInvoiceLedgers.Columns.Remove(sysvoucher.AppSchema.VoucherTransaction.MODIFIEDColumn.ColumnName);

                            DataColumn dcModified = new DataColumn(sysvoucher.AppSchema.VoucherTransaction.MODIFIEDColumn.ColumnName, typeof(System.Int32));
                            dcModified.DefaultValue = 0;
                            dtGSTInvoiceLedgers.Columns.Add(dcModified);

                            DataTable dtGSTLegersTrans = dtVoucherGSTLegers.DefaultView.ToTable(); //dtTransInfo.DefaultView.ToTable();

                            //On 20/10/2023 Allow GST Invoice always without GST Amount
                            //dtGSTLegersTrans.DefaultView.RowFilter = sysvoucher.AppSchema.VoucherTransaction.GSTColumn.ColumnName + " > 0";
                            if (this.EnableGST == "1")
                            {
                                dtGSTLegersTrans.DefaultView.RowFilter = sysvoucher.AppSchema.Ledger.IS_GST_LEDGERSColumn.ColumnName + " = 1";
                            }

                            DataTable dtUniqueGSTLedgters = dtGSTLegersTrans.DefaultView.ToTable(true, new string[] { "LEDGER_ID", "LEDGER_GST_CLASS_ID" });
                            if ((vtype == DefaultVoucherTypes.Receipt.ToString() || vtype == VoucherSubTypes.RC.ToString()) ||
                                 (vtype == DefaultVoucherTypes.Payment.ToString() || vtype == VoucherSubTypes.PY.ToString()))
                            {
                                dtUniqueGSTLedgters = dtGSTLegersTrans.DefaultView.ToTable(true, new string[] { "LEDGER_ID", "LEDGER_GST_CLASS_ID", "SOURCE" });
                                dtUniqueGSTLedgters.DefaultView.RowFilter = "LEDGER_GST_CLASS_ID >0 ";
                                dtUniqueGSTLedgters = dtUniqueGSTLedgters.DefaultView.ToTable();
                            }

                            foreach (DataRow dr in dtUniqueGSTLedgters.Rows)
                            {
                                double amt = 0;
                                double creditamt = 0;
                                double debitamt = 0;
                                string ledgername = string.Empty; //GetLedgerName(ledgerid);
                                string hsnsaccode = string.Empty;

                                Int32 ledgerid = NumberSet.ToInteger(dr[sysvoucher.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString());
                                Int32 ledgergstclassid = NumberSet.ToInteger(dr[sysvoucher.AppSchema.VoucherTransaction.LEDGER_GST_CLASS_IDColumn.ColumnName].ToString());
                                string filter = sysvoucher.AppSchema.Ledger.LEDGER_IDColumn.ColumnName + " = " + ledgerid + " AND " +
                                                sysvoucher.AppSchema.VoucherTransaction.LEDGER_GST_CLASS_IDColumn.ColumnName + " = " + ledgergstclassid;

                                DataRow drLedgerInfo = GetLedgerDetails(ledgerid);
                                if (drLedgerInfo != null)
                                {
                                    ledgername = drLedgerInfo["LEDGER"].ToString();
                                    hsnsaccode = drLedgerInfo[sysvoucher.AppSchema.Ledger.GST_HSN_SAC_CODEColumn.ColumnName].ToString();
                                }
                                string source = "1";
                                if ((vtype == DefaultVoucherTypes.Journal.ToString() || vtype == VoucherSubTypes.JN.ToString()))
                                {
                                    creditamt = NumberSet.ToDouble(dtGSTLegersTrans.Compute("SUM(" + sysvoucher.AppSchema.VoucherTransaction.CREDITColumn.ColumnName + ")", filter).ToString());
                                    debitamt = NumberSet.ToDouble(dtGSTLegersTrans.Compute("SUM(" + sysvoucher.AppSchema.VoucherTransaction.DEBITColumn.ColumnName + ")", filter).ToString());
                                    source = creditamt > 0 ? "1" : "2";
                                }
                                else
                                {
                                    source = dr[sysvoucher.AppSchema.VoucherTransaction.SOURCEColumn.ColumnName].ToString();
                                    amt = NumberSet.ToDouble(dtGSTLegersTrans.Compute("SUM(" + sysvoucher.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName + ")", filter).ToString());
                                }
                                string transmode = (source == "1") ? TransSource.Cr.ToString().ToUpper() : TransSource.Dr.ToString().ToUpper();
                                resultArgs = sysvoucher.FetchGSTPercentages(ledgergstclassid);

                                if (resultArgs.Success && resultArgs.DataSource.Table != null &&
                                    (resultArgs.DataSource.Table.Rows.Count > 0))
                                {
                                    string ledgergstclass = string.Empty;
                                    if (resultArgs.DataSource.Table.Rows.Count > 0)
                                    {
                                        ledgergstclass = resultArgs.DataSource.Table.Rows[0][sysvoucher.AppSchema.MasterGSTClass.SLABColumn.ColumnName].ToString();
                                    }

                                    dtGSTInvoiceLedgers.DefaultView.RowFilter = filter;
                                    DataRow drGSTLedgerDetails = null;
                                    bool newlyadded = false;

                                    if (dtGSTInvoiceLedgers.DefaultView.Count > 0)
                                    {
                                        drGSTLedgerDetails = dtGSTInvoiceLedgers.DefaultView[0].Row;
                                    }
                                    else
                                    {
                                        drGSTLedgerDetails = dtGSTInvoiceLedgers.NewRow();
                                        newlyadded = true;
                                    }

                                    drGSTLedgerDetails.BeginEdit();
                                    drGSTLedgerDetails[sysvoucher.AppSchema.GSTInvoiceMaster.GST_INVOICE_IDColumn.ColumnName] = GST_INVOICE_ID;
                                    drGSTLedgerDetails[sysvoucher.AppSchema.GSTInvoiceMaster.LEDGER_IDColumn.ColumnName] = ledgerid;
                                    drGSTLedgerDetails[sysvoucher.AppSchema.GSTInvoiceMaster.LEDGER_NAMEColumn.ColumnName] = ledgername;
                                    drGSTLedgerDetails[sysvoucher.AppSchema.GSTInvoiceMaster.LEDGER_GST_CLASS_IDColumn.ColumnName] = ledgergstclassid;
                                    drGSTLedgerDetails[sysvoucher.AppSchema.GSTInvoiceMasterLedgerDetails.LEDGER_GST_CLASSColumn.ColumnName] = ledgergstclass;


                                    if ((vtype == DefaultVoucherTypes.Journal.ToString() || vtype == VoucherSubTypes.JN.ToString()))
                                    {
                                        drGSTLedgerDetails[sysvoucher.AppSchema.GSTInvoiceMaster.AMOUNTColumn.ColumnName] = (creditamt > 0 ? creditamt : debitamt);
                                    }
                                    else
                                    {
                                        drGSTLedgerDetails[sysvoucher.AppSchema.GSTInvoiceMaster.AMOUNTColumn.ColumnName] = amt;
                                    }

                                    drGSTLedgerDetails[sysvoucher.AppSchema.GSTInvoiceMaster.TRANS_MODEColumn.ColumnName] = transmode;

                                    if (string.IsNullOrEmpty(drGSTLedgerDetails[sysvoucher.AppSchema.GSTInvoiceMasterLedgerDetails.GST_HSN_SAC_CODEColumn.ColumnName].ToString()))
                                    {   //04/04/2023, HSN code is not available, let us take it from master ledger 
                                        drGSTLedgerDetails[sysvoucher.AppSchema.GSTInvoiceMasterLedgerDetails.GST_HSN_SAC_CODEColumn.ColumnName] = hsnsaccode;
                                    }

                                    drGSTLedgerDetails[sysvoucher.AppSchema.GSTInvoiceMaster.CGSTColumn.ColumnName] = NumberSet.ToDouble(dtGSTLegersTrans.Compute("SUM(" + sysvoucher.AppSchema.VoucherTransaction.CGSTColumn.ColumnName + ")", filter).ToString());
                                    drGSTLedgerDetails[sysvoucher.AppSchema.GSTInvoiceMaster.SGSTColumn.ColumnName] = NumberSet.ToDouble(dtGSTLegersTrans.Compute("SUM(" + sysvoucher.AppSchema.VoucherTransaction.SGSTColumn.ColumnName + ")", filter).ToString());
                                    drGSTLedgerDetails[sysvoucher.AppSchema.GSTInvoiceMaster.IGSTColumn.ColumnName] = NumberSet.ToDouble(dtGSTLegersTrans.Compute("SUM(" + sysvoucher.AppSchema.VoucherTransaction.IGSTColumn.ColumnName + ")", filter).ToString());

                                    drGSTLedgerDetails[sysvoucher.AppSchema.VoucherTransaction.MODIFIEDColumn.ColumnName] = 1;
                                    drGSTLedgerDetails.EndEdit();
                                    if (newlyadded) //(dtGSTInvoiceLedgers.DefaultView.Count == 0)
                                    {
                                        dtGSTInvoiceLedgers.Rows.Add(drGSTLedgerDetails);
                                    }

                                }
                                else
                                {
                                    if (resultArgs.DataSource.Table == null)
                                    {
                                        resultArgs.Message = "GST Rate is not found";
                                    }
                                    else if (resultArgs.DataSource.Table.Rows.Count == 0)
                                    {
                                        resultArgs.Message = "GST Rate is not found";
                                    }
                                    break;
                                }
                            }
                        }
                        else //On 18/12/2024 For Multi Currency without GST amount
                        {
                            DataTable dtGSTLegersTrans = dtVoucherGSTLegers.DefaultView.ToTable();
                            Int32 ledgerid = 0;
                            string ledgername = string.Empty;

                            dtGSTLegersTrans.DefaultView.RowFilter = AppSchema.VoucherTransaction.DEBITColumn.ColumnName + " > 0"; //For Debit Ledger Alone
                            if (dtGSTLegersTrans.DefaultView.Count > 0)
                            {
                                ledgerid = NumberSet.ToInteger(dtGSTLegersTrans.DefaultView[0][sysvoucher.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString());
                                DataRow drLedgerInfo = GetLedgerDetails(ledgerid);
                                if (drLedgerInfo != null)
                                {
                                    ledgername = drLedgerInfo["LEDGER"].ToString();
                                }

                                if (ledgerid > 0 && !string.IsNullOrEmpty(ledgername))
                                {
                                    string filter = sysvoucher.AppSchema.GSTInvoiceMasterLedgerDetails.ITEM_NAMEColumn.ColumnName + " <> '' AND " +
                                                        sysvoucher.AppSchema.GSTInvoiceMasterLedgerDetails.QUANTITYColumn.ColumnName + " >0 AND " +
                                                        sysvoucher.AppSchema.GSTInvoiceMasterLedgerDetails.UNIT_AMOUNTColumn.ColumnName + " >0";
                                    dtGSTInvoiceLedgers.DefaultView.RowFilter = filter;

                                    foreach (DataRowView drvGSTLedgerDetails in dtGSTInvoiceLedgers.DefaultView)
                                    {
                                        double quantity = NumberSet.ToDouble(drvGSTLedgerDetails[AppSchema.GSTInvoiceMasterLedgerDetails.QUANTITYColumn.ColumnName].ToString());
                                        double unitprice = NumberSet.ToDouble(drvGSTLedgerDetails[AppSchema.GSTInvoiceMasterLedgerDetails.UNIT_AMOUNTColumn.ColumnName].ToString());

                                        drvGSTLedgerDetails.BeginEdit();
                                        drvGSTLedgerDetails[sysvoucher.AppSchema.GSTInvoiceMaster.GST_INVOICE_IDColumn.ColumnName] = GST_INVOICE_ID;
                                        drvGSTLedgerDetails[sysvoucher.AppSchema.GSTInvoiceMaster.LEDGER_IDColumn.ColumnName] = ledgerid;
                                        drvGSTLedgerDetails[sysvoucher.AppSchema.GSTInvoiceMaster.LEDGER_NAMEColumn.ColumnName] = ledgername;
                                        drvGSTLedgerDetails[sysvoucher.AppSchema.GSTInvoiceMaster.LEDGER_GST_CLASS_IDColumn.ColumnName] = this.GSTZeroClassId;
                                        drvGSTLedgerDetails[sysvoucher.AppSchema.GSTInvoiceMasterLedgerDetails.LEDGER_GST_CLASSColumn.ColumnName] = string.Empty;
                                        drvGSTLedgerDetails[sysvoucher.AppSchema.GSTInvoiceMasterLedgerDetails.AMOUNTColumn.ColumnName] = (quantity * unitprice);
                                        drvGSTLedgerDetails[sysvoucher.AppSchema.GSTInvoiceMasterLedgerDetails.TRANS_MODEColumn.ColumnName] = TransSource.Dr.ToString();
                                        drvGSTLedgerDetails.EndEdit();
                                    }
                                }
                                else
                                {
                                    resultArgs.Message = "Credit Ledger is not available in the Voucher.";
                                    dtGSTInvoiceLedgers.Rows.Clear();
                                }
                            }
                            else
                            {
                                resultArgs.Message = "Credit Ledger is not available in the Voucher";
                                dtGSTInvoiceLedgers.Rows.Clear();
                            }
                        }

                        if (resultArgs.Success)
                        {

                            dtGSTInvoiceLedgers.DefaultView.RowFilter = string.Empty;
                            if (!isgeneralinvoice)
                            {
                                dtGSTInvoiceLedgers.DefaultView.RowFilter = sysvoucher.AppSchema.VoucherTransaction.MODIFIEDColumn.ColumnName + "=1";
                            }
                            else
                            {
                                dtGSTInvoiceLedgers.DefaultView.RowFilter = sysvoucher.AppSchema.GSTInvoiceMasterLedgerDetails.ITEM_NAMEColumn.ColumnName + " <> '' AND " +
                                                    sysvoucher.AppSchema.GSTInvoiceMasterLedgerDetails.QUANTITYColumn.ColumnName + " > 0 AND " +
                                                    sysvoucher.AppSchema.GSTInvoiceMasterLedgerDetails.UNIT_AMOUNTColumn.ColumnName + "> 0";
                            }


                            dtGSTInvoiceLedgers = dtGSTInvoiceLedgers.DefaultView.ToTable();
                            resultArgs.DataSource.Data = dtGSTInvoiceLedgers;
                            resultArgs.Success = true;
                        }
                    }
                }
                else
                {
                    resultArgs.DataSource.Data = null;
                    resultArgs.Success = true;
                }
            }
            catch (Exception err)
            {
                resultArgs.Message = "Could not update GST Invoice Ledger Details, " + err.Message;
            }
            return resultArgs;
        }

        public ResultArgs FetchGSTInvoiceVouchersByGSTInvoiceId(int GSTInvId)
        {
            return FetchGSTInvoiceMasterDetails(GSTInvId);
        }

        public ResultArgs FetchGSTPenindgInvoices(Int32 VoucherId, Int32 projectid, DefaultVoucherTypes vtype, DateTime voucherdate)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchGSTPendingInvoices))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, projectid);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.TRANS_MODEColumn, vtype == DefaultVoucherTypes.Receipt ? TransSource.Dr.ToString() : TransSource.Cr.ToString());
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_DATEColumn, voucherdate);

                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.CGSTColumn, CGSTLedgerId);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.SGSTColumn, SGSTLedgerId);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.IGSTColumn, IGSTLedgerId);
                dataManager.Parameters.Add(this.AppSchema.MasterGSTClass.ENABLE_GSTColumn, NumberSet.ToInteger(this.EnableGST));

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }

        private ResultArgs FetchGSTPendingInvoicesBookingDetailsByInvoiceId(DefaultVoucherTypes vtype, Int32 VoucherId, int GSTInvId, Int32 projectid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchGSTPendingInvoicesBookingDetailsByInvoiceId))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, projectid);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.GST_INVOICE_IDColumn, GSTInvId);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.CGSTColumn, CGSTLedgerId);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.SGSTColumn, SGSTLedgerId);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.IGSTColumn, IGSTLedgerId);

                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.TRANS_MODEColumn, vtype == DefaultVoucherTypes.Receipt ? TransSource.Dr.ToString() : TransSource.Cr.ToString());
                dataManager.Parameters.Add(this.AppSchema.MasterGSTClass.ENABLE_GSTColumn, NumberSet.ToInteger(this.EnableGST));
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        public ResultArgs RemoveGSTVendorInvoiceDetailsById(Int32 VId, Int32 GSTInvoiceId, DataManager dm = null)
        {
            if (VId > 0 && GSTInvoiceId > 0)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.DeleteGSTInvoiceDetailsById))
                {
                    if (dm != null)
                        dataManager.Database = dm.Database;
                    else
                        dataManager.BeginTransaction();

                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VId);
                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.GST_INVOICE_IDColumn, GSTInvoiceId);
                    resultArgs = dataManager.UpdateData();
                    if (!resultArgs.Success && dm == null)
                    {
                        dataManager.TransExecutionMode = ExecutionMode.Fail;
                    }
                    else if (resultArgs.Success && dm == null)
                    {
                        dataManager.EndTransaction();
                    }

                }
            }
            else
            {
                resultArgs.Success = true;
            }
            return resultArgs;
        }


        private ResultArgs FetchGSTInvoiceVouchersById(DataManager dm, int VId, int GSTInvId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchGSTInvoiceVouchersById))
            {
                dataManager.Database = dm.Database;
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VId);
                dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.GST_INVOICE_IDColumn, GSTInvId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs FetchGSTInvoiceIdByVoucherId(DataManager dm, int VoucherId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchGSTInvoiceIdByVoucherId))
            {
                dataManager.Database = dm.Database;
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 03/11/2022, To update GST Invoice details
        /// </summary>
        /// <param name="dm"></param>
        /// <returns></returns>
        private ResultArgs SaveGSTInvoiceMasterDetails(DataManager dm)
        {
            double cgst = 0;
            double sgst = 0;
            double igst = 0;

            try
            {
                if (resultArgs.Success)
                {
                    if (dtGSTInvoiceMasterDetails != null && dtGSTInvoiceMasterDetails.Rows.Count > 0)
                    {
                        DataTable dtrans = this.TransInfo.ToTable();
                        cgst = (dtrans.Columns.Contains(this.AppSchema.VoucherTransaction.CGSTColumn.ColumnName)) ? this.NumberSet.ToDouble(dtrans.Compute("SUM(" + this.AppSchema.VoucherTransaction.CGSTColumn.ColumnName + ")", string.Empty).ToString()) : 0;
                        sgst = (dtrans.Columns.Contains(this.AppSchema.VoucherTransaction.SGSTColumn.ColumnName)) ? this.NumberSet.ToDouble(dtrans.Compute("SUM(" + this.AppSchema.VoucherTransaction.SGSTColumn.ColumnName + ")", string.Empty).ToString()) : 0;
                        igst = (dtrans.Columns.Contains(this.AppSchema.VoucherTransaction.IGSTColumn.ColumnName)) ? this.NumberSet.ToDouble(dtrans.Compute("SUM(" + this.AppSchema.VoucherTransaction.IGSTColumn.ColumnName + ")", string.Empty).ToString()) : 0;

                        DataRow dr = dtGSTInvoiceMasterDetails.Rows[0];
                        using (DataManager dataManager = new DataManager((GST_INVOICE_ID == 0) ? SQLCommand.VoucherMaster.SaveGSTInvoiceMasterDetails : SQLCommand.VoucherMaster.UpdateGSTInvoiceMasterDetails))
                        {
                            dataManager.Database = dm.Database;
                            dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.GST_INVOICE_IDColumn, GST_INVOICE_ID, true);
                            dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.GST_VENDOR_INVOICE_NOColumn, dr[AppSchema.GSTInvoiceMaster.GST_VENDOR_INVOICE_NOColumn.ColumnName].ToString());
                            dataManager.Parameters.Add(this.AppSchema.VoucherMaster.GST_VENDOR_INVOICE_DATEColumn, dr[AppSchema.GSTInvoiceMaster.GST_VENDOR_INVOICE_DATEColumn.ColumnName].ToString(), false);
                            dataManager.Parameters.Add(this.AppSchema.VoucherMaster.GST_VENDOR_INVOICE_TYPEColumn, NumberSet.ToInteger(dr[AppSchema.GSTInvoiceMaster.GST_VENDOR_INVOICE_TYPEColumn.ColumnName].ToString()));
                            dataManager.Parameters.Add(this.AppSchema.VoucherMaster.GST_VENDOR_IDColumn, NumberSet.ToInteger(dr[AppSchema.GSTInvoiceMaster.GST_VENDOR_IDColumn.ColumnName].ToString()));
                            dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.TRANSPORT_MODEColumn, dr[AppSchema.GSTInvoiceMaster.TRANSPORT_MODEColumn.ColumnName].ToString());
                            dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.VEHICLE_NUMBERColumn, dr[AppSchema.GSTInvoiceMaster.VEHICLE_NUMBERColumn.ColumnName].ToString());
                            if (string.IsNullOrEmpty(dr[AppSchema.GSTInvoiceMaster.SUPPLY_DATEColumn.ColumnName].ToString())
                                || DateSet.ToDate(dr[AppSchema.GSTInvoiceMaster.SUPPLY_DATEColumn.ColumnName].ToString(), false) == DateTime.MinValue)
                                dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.SUPPLY_DATEColumn, DBNull.Value);
                            else
                                dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.SUPPLY_DATEColumn, dr[AppSchema.GSTInvoiceMaster.SUPPLY_DATEColumn.ColumnName].ToString());

                            if (string.IsNullOrEmpty(dr[AppSchema.GSTInvoiceMaster.DUE_DATEColumn.ColumnName].ToString())
                                || DateSet.ToDate(dr[AppSchema.GSTInvoiceMaster.DUE_DATEColumn.ColumnName].ToString(), false) == DateTime.MinValue)
                                dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.DUE_DATEColumn, DBNull.Value);
                            else
                                dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.DUE_DATEColumn, dr[AppSchema.GSTInvoiceMaster.DUE_DATEColumn.ColumnName].ToString());

                            dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.SUPPLY_PLACEColumn, dr[AppSchema.GSTInvoiceMaster.SUPPLY_PLACEColumn.ColumnName].ToString());
                            dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.BILLING_NAMEColumn, dr[AppSchema.GSTInvoiceMaster.BILLING_NAMEColumn.ColumnName].ToString());
                            dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.BILLING_GST_NOColumn, dr[AppSchema.GSTInvoiceMaster.BILLING_GST_NOColumn.ColumnName].ToString());
                            dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.BILLING_ADDRESSColumn, dr[AppSchema.GSTInvoiceMaster.BILLING_ADDRESSColumn.ColumnName].ToString());
                            dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.BILLING_STATE_IDColumn, NumberSet.ToInteger(dr[AppSchema.GSTInvoiceMaster.BILLING_STATE_IDColumn.ColumnName].ToString()));
                            dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.BILLING_COUNTRY_IDColumn, NumberSet.ToInteger(dr[AppSchema.GSTInvoiceMaster.BILLING_COUNTRY_IDColumn.ColumnName].ToString()));
                            dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.SHIPPING_NAMEColumn, dr[AppSchema.GSTInvoiceMaster.SHIPPING_NAMEColumn.ColumnName].ToString());
                            dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.SHIPPING_GST_NOColumn, dr[AppSchema.GSTInvoiceMaster.SHIPPING_GST_NOColumn.ColumnName].ToString());
                            dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.SHIPPING_ADDRESSColumn, dr[AppSchema.GSTInvoiceMaster.SHIPPING_ADDRESSColumn.ColumnName].ToString());
                            dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.SHIPPING_STATE_IDColumn, NumberSet.ToInteger(dr[AppSchema.GSTInvoiceMaster.SHIPPING_STATE_IDColumn.ColumnName].ToString()));
                            dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.SHIPPING_COUNTRY_IDColumn, NumberSet.ToInteger(dr[AppSchema.GSTInvoiceMaster.SHIPPING_COUNTRY_IDColumn.ColumnName].ToString()));

                            dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.CHEQUE_IN_FAVOURColumn, dr[AppSchema.GSTInvoiceMaster.CHEQUE_IN_FAVOURColumn.ColumnName].ToString());
                            dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.TOTAL_AMOUNTColumn, Amount);
                            dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.TOTAL_CGST_AMOUNTColumn, cgst);
                            dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.TOTAL_SGST_AMOUNTColumn, sgst);
                            dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.TOTAL_IGST_AMOUNTColumn, igst);
                            if (NumberSet.ToInteger(dr[AppSchema.GSTInvoiceMaster.IS_REVERSE_CHARGEColumn.ColumnName].ToString()) == ((int)SetDefaultValue.DefaultValue))
                            {
                                dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.IS_REVERSE_CHARGEColumn, ((int)SetDefaultValue.DefaultValue).ToString());
                                dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.REVERSE_CHARGE_AMOUNTColumn, NumberSet.ToDecimal(dr[AppSchema.GSTInvoiceMaster.REVERSE_CHARGE_AMOUNTColumn.ColumnName].ToString()));
                            }
                            else
                            {
                                dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.IS_REVERSE_CHARGEColumn, ((int)SetDefaultValue.DisableValue).ToString());
                                dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.REVERSE_CHARGE_AMOUNTColumn, ((int)SetDefaultValue.DisableValue).ToString());
                            }

                            dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.STATUSColumn, NumberSet.ToInteger(dr[AppSchema.GSTInvoiceMaster.STATUSColumn.ColumnName].ToString()));

                            dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.BOOKING_VOUCHER_IDColumn.ColumnName, VoucherId);
                            if (VoucherType == DefaultVoucherTypes.Receipt.ToString() || VoucherType == VoucherSubTypes.RC.ToString())
                            {
                                dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.BOOKING_VOUCHER_TYPEColumn, VoucherSubTypes.RC.ToString());
                            }
                            else if (VoucherType == DefaultVoucherTypes.Payment.ToString() || VoucherType == VoucherSubTypes.PY.ToString())
                            {
                                dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.BOOKING_VOUCHER_TYPEColumn, VoucherSubTypes.PY.ToString());
                            }
                            else
                            {
                                dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.BOOKING_VOUCHER_TYPEColumn, VoucherSubTypes.JN.ToString());
                            }

                            //dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                            resultArgs = dataManager.UpdateData();
                        }

                        if (resultArgs.Success && resultArgs.RowUniqueId != null)
                        {
                            if (GST_INVOICE_ID == 0) GST_INVOICE_ID = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());

                            //On 11/08/2023, Update GST Vouchers (Receipts/Payments) agaist Journal GST Invoice Booking
                            //Later it will be maintained Receitps/Payments amount agaist GST Invoice
                            //GST direct invoice bookig in Receipt and Payment
                            if (VoucherType == DefaultVoucherTypes.Receipt.ToString() || VoucherType == DefaultVoucherTypes.Payment.ToString() ||
                                VoucherType == VoucherSubTypes.RC.ToString() || VoucherType == VoucherSubTypes.PY.ToString())
                            {
                                UpdateGSTVoucherAgainsInvoice(dm, VoucherId, GST_INVOICE_ID, NumberSet.ToDecimal(dr[AppSchema.GSTInvoiceMaster.TOTAL_AMOUNTColumn.ColumnName].ToString()));
                            }

                            //On 11/08/2023, Update GST ledger details 
                            if (resultArgs.Success)
                            {
                                UpdateGSTInvoiceLedgerDetails(dm);
                            }

                        }
                    }
                    else if (GST_INVOICE_ID > 0)
                    {
                        resultArgs = RemoveGSTVendorInvoiceDetailsById(VoucherId, GST_INVOICE_ID, dm);
                    }

                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        /// <summary>
        /// On 12/12/2022, To update gst invoice ledger details
        /// </summary>
        /// <param name="dm"></param>
        /// <returns></returns>
        private ResultArgs UpdateGSTInvoiceLedgerDetails(DataManager dm)
        {
            try
            {
                if (resultArgs.Success)
                {
                    if (dtGSTInvoiceMasterDetails != null && dtGSTInvoiceMasterDetails.Rows.Count > 0 &&
                        dtGSTInvoiceMasterLedgerDetails != null && dtGSTInvoiceMasterLedgerDetails.Rows.Count > 0)
                    {
                        //Clear GST Invoice Ledger details
                        resultArgs = DeleteGSTInvoiceLedgerDetails(GST_INVOICE_ID, dm);
                        if (resultArgs.Success)
                        {
                            foreach (DataRow dr in dtGSTInvoiceMasterLedgerDetails.Rows)
                            {
                                using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.UpdateGSTInvoiceLedgerDetails))
                                {
                                    dataManager.Database = dm.Database;
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.GST_INVOICE_IDColumn, GST_INVOICE_ID);
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.LEDGER_IDColumn, NumberSet.ToInteger(dr[this.AppSchema.GSTInvoiceMasterLedgerDetails.LEDGER_IDColumn.ColumnName].ToString()));
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.LEDGER_GST_CLASS_IDColumn, NumberSet.ToInteger(dr[this.AppSchema.GSTInvoiceMasterLedgerDetails.LEDGER_GST_CLASS_IDColumn.ColumnName].ToString()));
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.AMOUNTColumn, NumberSet.ToDouble(dr[this.AppSchema.GSTInvoiceMasterLedgerDetails.AMOUNTColumn.ColumnName].ToString()));
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.TRANS_MODEColumn, dr[this.AppSchema.GSTInvoiceMasterLedgerDetails.TRANS_MODEColumn.ColumnName].ToString());
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.GST_HSN_SAC_CODEColumn, dr[this.AppSchema.GSTInvoiceMasterLedgerDetails.GST_HSN_SAC_CODEColumn.ColumnName].ToString());

                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.QUANTITYColumn, NumberSet.ToDouble(dr[this.AppSchema.GSTInvoiceMasterLedgerDetails.QUANTITYColumn.ColumnName].ToString()));
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.UNIT_AMOUNTColumn, NumberSet.ToDouble(dr[this.AppSchema.GSTInvoiceMasterLedgerDetails.UNIT_AMOUNTColumn.ColumnName].ToString()));
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.UNIT_MEASUREMENTColumn, dr[this.AppSchema.GSTInvoiceMasterLedgerDetails.UNIT_MEASUREMENTColumn.ColumnName].ToString());
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.DISCOUNTColumn, NumberSet.ToDouble(dr[this.AppSchema.GSTInvoiceMasterLedgerDetails.DISCOUNTColumn.ColumnName].ToString()));

                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.CGSTColumn, NumberSet.ToDouble(dr[this.AppSchema.GSTInvoiceMasterLedgerDetails.CGSTColumn.ColumnName].ToString()));
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.SGSTColumn, NumberSet.ToDouble(dr[this.AppSchema.GSTInvoiceMasterLedgerDetails.SGSTColumn.ColumnName].ToString()));
                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.IGSTColumn, NumberSet.ToDouble(dr[this.AppSchema.GSTInvoiceMasterLedgerDetails.IGSTColumn.ColumnName].ToString()));

                                    if (dr.Table.Columns.Contains(this.AppSchema.GSTInvoiceMasterLedgerDetails.ITEM_NAMEColumn.ColumnName))
                                        dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.ITEM_NAMEColumn, dr[this.AppSchema.GSTInvoiceMasterLedgerDetails.ITEM_NAMEColumn.ColumnName].ToString());
                                    else
                                        dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.ITEM_NAMEColumn, string.Empty);

                                    if (dr.Table.Columns.Contains(this.AppSchema.GSTInvoiceMasterLedgerDetails.ITEM_DESCRIPTIONColumn.ColumnName))
                                        dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.ITEM_DESCRIPTIONColumn, dr[this.AppSchema.GSTInvoiceMasterLedgerDetails.ITEM_DESCRIPTIONColumn.ColumnName].ToString());
                                    else
                                        dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.ITEM_DESCRIPTIONColumn, string.Empty);

                                    dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.BRANCH_IDColumn, "0");
                                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                                    resultArgs = dataManager.UpdateData();

                                    if (!resultArgs.Success)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        /// <summary>
        /// On 14/09/2023, To update GST vouchers for the following 
        /// 1. GST direct invoice bookig in Receipt and Payment
        /// 2. Making Vouuches against Journal Booking Invoice
        /// </summary>
        /// <param name="dm"></param>
        /// <returns></returns>
        private ResultArgs UpdateGSTVoucherAgainsInvoice(DataManager dm, Int32 vid, Int32 gstinvoiceid, decimal amt)
        {
            try
            {
                if (resultArgs.Success)
                {
                    resultArgs = FetchGSTInvoiceVouchersById(dm, vid, gstinvoiceid);
                    if (resultArgs.Success)
                    {
                        bool exists = (resultArgs.DataSource.Table.Rows.Count == 0);
                        //GST Invoice - Voucher details
                        using (DataManager dataManager = new DataManager(exists ? SQLCommand.VoucherMaster.SaveGSTInvoiceVocuhersByVoucherId : SQLCommand.VoucherMaster.UpdateGSTInvoiceVocuhersByVoucherId))
                        {
                            dataManager.Database = dm.Database;
                            dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.VOUCHER_IDColumn, VoucherId);
                            dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.GST_INVOICE_IDColumn, gstinvoiceid);
                            dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.AMOUNTColumn, amt);
                            resultArgs = dataManager.UpdateData();

                            if (resultArgs.Success)
                            {
                                UpdateGSTInvoiceMasterStatus(dataManager, gstinvoiceid);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        /// <summary>
        /// Update GST Invoice status if invoice amount fully settled
        /// </summary>
        /// <param name="dm"></param>
        /// <param name="gstinvoiceid"></param>
        /// <returns></returns>
        private ResultArgs UpdateGSTInvoiceMasterStatus(DataManager dm, Int32 gstinvoiceid)
        {
            try
            {
                if (resultArgs.Success)
                {
                    //GST Invoice - Update status based on agaist amount
                    using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.UpdateGSTInvoiceMasterStatus))
                    {
                        dataManager.Database = dm.Database;
                        dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.GST_INVOICE_IDColumn, gstinvoiceid);
                        resultArgs = dataManager.UpdateData();
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        /// <summary>
        /// Delete Invoice agaist vouchers (Booking Invoice agaist Receipt/Payment Voucher)
        /// </summary>
        /// <param name="dm"></param>
        /// <param name="vid"></param>
        /// <param name="gstinvoiceid"></param>
        /// <param name="amt"></param>
        /// <returns></returns>
        private ResultArgs DeleteRandPVoucherAgainsInvoice(DataManager dm, Int32 vid, Int32 gstinvoiceid)
        {
            try
            {
                if (resultArgs.Success)
                {
                    //GST Invoice - Voucher details
                    using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.DeleteRandPVoucherAgainsInvoiceById))
                    {
                        dataManager.Database = dm.Database;
                        dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.VOUCHER_IDColumn, VoucherId);
                        dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.GST_INVOICE_IDColumn, gstinvoiceid);
                        resultArgs = dataManager.UpdateData();

                        if (resultArgs.Success)
                        {
                            resultArgs = UpdateGSTInvoiceMasterStatus(dataManager, gstinvoiceid);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        /// <summary>
        /// Delete Invoice agaist vouchers (Booking Invoice agaist Receipt/Payment Voucher)
        /// </summary>
        /// <param name="dm"></param>
        /// <param name="vid"></param>
        /// <param name="gstinvoiceid"></param>
        /// <param name="amt"></param>
        /// <returns></returns>
        private ResultArgs DeleteRandPVoucherAgainsInvoice(DataManager dm, Int32 vid)
        {
            Int32 gstinvoiceid = 0;
            try
            {
                if (resultArgs.Success)
                {
                    //GST Invoice - Voucher details
                    using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.DeleteRandPVoucherAgainsInvoiceById))
                    {
                        resultArgs = FetchGSTInvoiceIdByVoucherId(dataManager, vid);
                        if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            gstinvoiceid = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][AppSchema.GSTInvoiceMaster.GST_INVOICE_IDColumn.ColumnName].ToString());
                        }
                    }

                    using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.DeleteRandPVoucherAgainsInvoiceById))
                    {
                        dataManager.Database = dm.Database;
                        dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.VOUCHER_IDColumn, VoucherId);
                        resultArgs = dataManager.UpdateData();
                        if (resultArgs.Success && gstinvoiceid > 0)
                        {
                            resultArgs = UpdateGSTInvoiceMasterStatus(dataManager, gstinvoiceid);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        /// <summary>
        /// On 12/12/2022, To delete gst invoice ledger details
        /// </summary>
        /// <param name="dm"></param>
        /// <returns></returns>
        private ResultArgs DeleteGSTInvoiceLedgerDetails(Int32 GSTInvoiceId, DataManager dm)
        {
            try
            {
                if (resultArgs.Success)
                {
                    if (GSTInvoiceId > 0)
                    {
                        using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.DeleteGSTInvoiceLedgerDetails))
                        {
                            dataManager.Database = dm.Database;
                            dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMasterLedgerDetails.GST_INVOICE_IDColumn, GSTInvoiceId);
                            resultArgs = dataManager.UpdateData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        private ResultArgs FetchGSTInvoiceMasterDetails(int GSTInvId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchGSTInvoiceMasterDetailsById))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.GST_INVOICE_IDColumn, GSTInvId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchGSTInvoiceMasterLedgersDetails(int GSTInvId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchGSTInvoiceMasterLedgerDetailsByGSTInvoiceId))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(this.AppSchema.GSTInvoiceMaster.GST_INVOICE_IDColumn, GSTInvId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        public ResultArgs ValidateGSTInvoiceLedgers(DefaultVoucherTypes vtype, int vid, int pid, int GSTInvId, DataTable dtRPGSTLedgers)
        {
            Int32 RPLedgerID = 0;
            ResultArgs result = FetchGSTPendingInvoicesBookingDetailsByInvoiceId(vtype, vid, GSTInvId, pid);
            if (result.Success && result.DataSource.Table != null)
            {
                DataTable dtGSTInvoiceLedgers = result.DataSource.Table;
                //dtGSTInvoiceLedgers.DefaultView.RowFilter = AppSchema.GSTInvoiceMaster.AMOUNTColumn.ColumnName + " >0";
                //dtGSTInvoiceLedgers = dtGSTInvoiceLedgers.DefaultView.ToTable();
                foreach (DataRow drGSTInvoiceLedgers in dtGSTInvoiceLedgers.Rows)
                {
                    Int32 gstinvoicePartyLedgerId = NumberSet.ToInteger(drGSTInvoiceLedgers[AppSchema.GSTInvoiceMaster.PARTY_LEDGER_IDColumn.ColumnName].ToString());
                    string filter = AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName + " = " + gstinvoicePartyLedgerId;
                    string gstfilter = AppSchema.GSTInvoiceMaster.PARTY_LEDGER_IDColumn.ColumnName + " = " + gstinvoicePartyLedgerId;
                    string gstinvicePartyLedger = drGSTInvoiceLedgers[AppSchema.GSTInvoiceMaster.PARTY_LEDGER_NAMEColumn.ColumnName].ToString();
                    Double gstinvoicePartyLedgerAmt = NumberSet.ToDouble(dtGSTInvoiceLedgers.Compute("SUM(" + AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName + ")", gstfilter).ToString());
                    Double RPLedgerAmt = NumberSet.ToDouble(dtRPGSTLedgers.Compute("SUM(" + AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName + ")", filter).ToString());
                    RPLedgerID = dtRPGSTLedgers.Rows.Count > 0 ? NumberSet.ToInteger(dtRPGSTLedgers.Rows[0][AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString()) : 0;
                    if (gstinvoicePartyLedgerId != RPLedgerID) //RPLedgerAmt != gstinvoicePartyLedgerAmt 
                    {
                        result.Message = "Mismatching Invoice Ledger '" + gstinvicePartyLedger + "' with Voucher Ledger.";
                        break;
                    }
                    else if (RPLedgerAmt > gstinvoicePartyLedgerAmt) //RPLedgerAmt != gstinvoicePartyLedgerAmt 
                    {
                        result.Message = "Mismatching Invoice Amount '" + NumberSet.ToNumber(gstinvoicePartyLedgerAmt) + "' with Voucher Amount " + NumberSet.ToNumber(RPLedgerAmt);
                        break;
                    }
                }
            }
            return result;
        }

        public ResultArgs OptimizeMainTables()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.OptimizeMainTables))
            {
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        #endregion

        public ResultArgs GetlastSyncDate()
        {
            ResultArgs resultArgs = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchLastDateSyncDetails))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
    }
}
