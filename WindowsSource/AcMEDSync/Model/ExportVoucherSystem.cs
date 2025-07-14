using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using Bosco.DAO.Schema;

namespace AcMEDSync.Model
{
    public class ExportVoucherSystem : DsyncSystemBase
    {
        #region Decelaration
        ResultArgs resultArgs = null;
        public DataSet dsTransaction = new DataSet("Vouchers");
        //Transactions Variables
        private const string MASTER_TABLE_NAME = "VoucherMasters";
        private const string TRANSACTION_TABLE_NAME = "VoucherTransaction";
        private const string DONOR_TABLE_NAME = "Donors";
        private const string BANKDETAILS_TABLE_NAME = "BankDetails";
        private const string LEDGERBALANCE_TABLE_NAME = "LedgerBalance";
        private const string COUNTRY_TABLE_NAME = "Country";
        private const string HEADOFFICE_LEDGER_TABLE_NAME = "Ledger";
        private const string COSTCENTRE_TABLE_NAME = "VoucherCostCentre";
        //Relation  Variables
        private const string RELATION_COSTCENTRE_NAME = "relCostCentre";
        private const string RELATION_TRANSACTION_NAME = "relTransaction";
        //FD Transaction Variables
        private const string FD_ACCOUNT_TABLE_NAME = "FD_Investment_Account";
        private const string FD_RENEWAL_TABLE_NAME = "FD_Renewal";
        private const string FD_VOUCHER_MASTER_TRANS_TABLE_NAME = "FD_Voucher_Master_Trans";
        private const string FD_VOUCHER_TRANS_TABLE_NAME = "FD_Voucher_Trans";
        private const string FD_BANK_ACCOUNT_DETAILS_TABLE_NAME = "FD_Bank_Account_Details";
        private const string FD_BANK_DETAILS_TABLE_NAME = "FD_Bank_Details";

        #endregion

        #region Properties
        public string ProjectId { get; set; }
        public string HeadOfficeCode { get; set; }
        public string BranchOfficeCode { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        #endregion

        #region Methods
        public ResultArgs FetchProjectsLookup()
        {
            //string sQuery = this.GetVoucherSQL(SQL.EnumDataSyncSQLCommand.ExportVouchers.FetchProjects);
            using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchProjects, SQLAdapterType.HOSQL))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs CheckHOLedgerExists()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.CheckHeadofficeLedgerExists, SQLAdapterType.HOSQL))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchActiveTransactionPeriod()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchActiveTransactionperiod, SQLAdapterType.HOSQL))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FillExportVoucherTransaction()
        {
            DataTable dtTransaction = new DataTable();
            DataTable dtCostCentre = new DataTable();
            DataRelation relCostcentre = null;
            DataRelation relVoucher = null;
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                AcMEDataSynLog.WriteLog("Constructs Header data ...");
                // Construct Header
                dsTransaction.Tables.Add(ConstructHeaderData());
                AcMEDataSynLog.WriteLog("Successfully Constructed Header data....");
                AcMEDataSynLog.WriteLog("Export Voucher Master Details Started.....");
                //Voucher Master Details
                resultArgs = FetchVoucherMaster();
                DataTable dtVoucherMaster = resultArgs.DataSource.Table;
                resultArgs = FDVoucherMasterTrans();
                DataTable dtFDVoucherMaster = resultArgs.DataSource.Table;

                if (dtVoucherMaster.Rows.Count > 0 || dtFDVoucherMaster.Rows.Count > 0) //&& resultArgs.DataSource.Table.Rows.Count > 0
                {
                    resultArgs = FetchVoucherMaster();
                    AcMEDataSynLog.WriteLog("Successfully Exported Voucher Masters....");
                    resultArgs.DataSource.Table.TableName = MASTER_TABLE_NAME;
                    dsTransaction.Tables.Add(resultArgs.DataSource.Table);
                    DataTable dtVoucher = resultArgs.DataSource.Table;
                    AcMEDataSynLog.WriteLog("Export Voucher Trans Details Started.....");
                    //Voucher Trans
                    resultArgs = FetchVoucherTransactions();
                    if (resultArgs.Success)
                    {
                        AcMEDataSynLog.WriteLog("Successfully Exported Voucher Masters....");
                        resultArgs.DataSource.Table.TableName = TRANSACTION_TABLE_NAME;
                        dsTransaction.Tables.Add(resultArgs.DataSource.Table);
                        dtTransaction = resultArgs.DataSource.Table;
                        //if (resultArgs.DataSource.Table.Rows.Count > 0)
                        //{
                        //    relVoucher = new DataRelation(RELATION_TRANSACTION_NAME, dtVoucher.Columns[this.AppSchema.Voucher.VOUCHER_IDColumn.ColumnName], dtTransaction.Columns[this.AppSchema.Voucher.VOUCHER_IDColumn.ColumnName]);
                        //    relVoucher.Nested = true;
                        //    dsTransaction.Relations.Add(relVoucher);
                        //}
                        AcMEDataSynLog.WriteLog("Export CostCentre Details Started.....");
                        //Voucher Cost Centre
                        resultArgs = FetchVoucherCostCentres();
                        if (resultArgs.Success)
                        {
                            AcMEDataSynLog.WriteLog("Successfully Exported CostCentre Details....");
                            resultArgs.DataSource.Table.TableName = COSTCENTRE_TABLE_NAME;
                            dsTransaction.Tables.Add(resultArgs.DataSource.Table);
                            dtCostCentre = resultArgs.DataSource.Table;
                            //if (resultArgs.DataSource.Table.Rows.Count > 0)
                            //{
                            //    relCostcentre = new DataRelation(RELATION_COSTCENTRE_NAME, dtVoucher.Columns[this.AppSchema.Voucher.VOUCHER_IDColumn.ColumnName], dtCostCentre.Columns[this.AppSchema.Voucher.VOUCHER_IDColumn.ColumnName]);
                            //    relCostcentre.Nested = true;
                            //    dsTransaction.Relations.Add(relCostcentre);
                            //}
                            AcMEDataSynLog.WriteLog("Export Donor Details Started.....");
                            //Donor Details
                            resultArgs = FetchDonors();
                            if (resultArgs.Success)
                            {
                                AcMEDataSynLog.WriteLog("Successfully Exported Donor Details....");
                                resultArgs.DataSource.Table.TableName = DONOR_TABLE_NAME;
                                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
                                AcMEDataSynLog.WriteLog("Export Bank Details Started.....");
                                //Bank Account Details
                                resultArgs = FetchBankDetails();
                                if (resultArgs.Success)
                                {
                                    AcMEDataSynLog.WriteLog("Successfully Exported Bank Details....");
                                    resultArgs.DataSource.Table.TableName = BANKDETAILS_TABLE_NAME;
                                    dsTransaction.Tables.Add(resultArgs.DataSource.Table);
                                    AcMEDataSynLog.WriteLog("Export Ledger Balance Details Started.....");
                                    //Ledger Balance Details
                                    resultArgs = FetchLedgerBalance();
                                    if (resultArgs.Success)
                                    {
                                        AcMEDataSynLog.WriteLog("Successfully Exported Ledger Balance....");
                                        resultArgs.DataSource.Table.TableName = LEDGERBALANCE_TABLE_NAME;
                                        dsTransaction.Tables.Add(resultArgs.DataSource.Table);
                                        AcMEDataSynLog.WriteLog("Export Country Details Started.....");
                                        //Country  Details
                                        resultArgs = FetchCountry();
                                        if (resultArgs.Success)
                                        {
                                            AcMEDataSynLog.WriteLog("Successfully Exported Country Details....");
                                            resultArgs.DataSource.Table.TableName = COUNTRY_TABLE_NAME;
                                            dsTransaction.Tables.Add(resultArgs.DataSource.Table);
                                            AcMEDataSynLog.WriteLog("Export Head Office Ledgers Details Started.....");
                                            //Head Office Ledger Details
                                            resultArgs = FetchHeadOfficeLedger();
                                            if (resultArgs.Success)
                                            {
                                                AcMEDataSynLog.WriteLog("Successfully Exported Head Office Ledgers....");
                                                resultArgs.DataSource.Table.TableName = HEADOFFICE_LEDGER_TABLE_NAME;
                                                dsTransaction.Tables.Add(resultArgs.DataSource.Table);

                                                AcMEDataSynLog.WriteLog("Export Fixed Deposit Investment Details Started.....");
                                                //Fixed Deposit Investment Details
                                                resultArgs = FDAccountInvestments();
                                                if (resultArgs.Success)
                                                {
                                                    AcMEDataSynLog.WriteLog("Successfully Exported Fixed Deposit Investment Details....");
                                                    resultArgs.DataSource.Table.TableName = FD_ACCOUNT_TABLE_NAME;
                                                    dsTransaction.Tables.Add(resultArgs.DataSource.Table);
                                                    AcMEDataSynLog.WriteLog("Export Fixed Deposit Renewals and withdraw Details Started.....");
                                                    //Fixed Deposit Renewals and withdraw Details
                                                    resultArgs = FDRenewals();
                                                    if (resultArgs.Success)
                                                    {
                                                        AcMEDataSynLog.WriteLog("Successfully Exported Fixed Deposit Renewals and withdraw Details...");
                                                        resultArgs.DataSource.Table.TableName = FD_RENEWAL_TABLE_NAME;
                                                        dsTransaction.Tables.Add(resultArgs.DataSource.Table);
                                                        AcMEDataSynLog.WriteLog("Export Fixed Deposit Voucher Master Transaction Details Started.....");
                                                        //Fixed Deposit Voucher Master Transaction Details
                                                        resultArgs = FDVoucherMasterTrans();
                                                        if (resultArgs.Success)
                                                        {
                                                            AcMEDataSynLog.WriteLog("Successfully Exported Fixed Deposit Voucher Master Transaction Details...");
                                                            resultArgs.DataSource.Table.TableName = FD_VOUCHER_MASTER_TRANS_TABLE_NAME;
                                                            dsTransaction.Tables.Add(resultArgs.DataSource.Table);
                                                            AcMEDataSynLog.WriteLog("Export Fixed Deposit Voucher  Transaction Details Started.....");
                                                            //Fixed Deposit Voucher Transaction Details
                                                            resultArgs = FDVoucherTrans();
                                                            if (resultArgs.Success)
                                                            {
                                                                AcMEDataSynLog.WriteLog("Successfully Exported Fixed Deposit Voucher Transaction Details...");
                                                                resultArgs.DataSource.Table.TableName = FD_VOUCHER_TRANS_TABLE_NAME;
                                                                dsTransaction.Tables.Add(resultArgs.DataSource.Table);
                                                                AcMEDataSynLog.WriteLog("Export Fixed Deposit Bank Account Details Started.....");
                                                                //Fixed Deposit Fixed Deposit Bank Account Details
                                                                resultArgs = FDBankAccountDetails();
                                                                if (resultArgs.Success)
                                                                {
                                                                    AcMEDataSynLog.WriteLog("Successfully Exported Fixed Deposit Bank Account Details...");
                                                                    resultArgs.DataSource.Table.TableName = FD_BANK_ACCOUNT_DETAILS_TABLE_NAME;
                                                                    dsTransaction.Tables.Add(resultArgs.DataSource.Table);
                                                                    AcMEDataSynLog.WriteLog("Export Fixed Deposit Bank Details Started.....");
                                                                    //Fixed Deposit Fixed Deposit Bank Details
                                                                    resultArgs = FDBankDetails();
                                                                    if (resultArgs.Success)
                                                                    {
                                                                        AcMEDataSynLog.WriteLog("Successfully Exported Fixed Deposit Bank Details...");
                                                                        resultArgs.DataSource.Table.TableName = FD_BANK_DETAILS_TABLE_NAME;
                                                                        dsTransaction.Tables.Add(resultArgs.DataSource.Table);
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
                    }
                }
                else
                {
                    resultArgs.Success = false;
                    resultArgs.Message = "No Transaction made during this Period";
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs FetchVoucherMaster()
        {
            // string sQuery = this.GetVoucherSQL(SQL.EnumDataSyncSQLCommand.ExportVouchers.FetchMasterVouchers);

            using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchMasterVouchers, SQLAdapterType.HOSQL))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            return resultArgs;
        }
        private DataTable ConstructHeaderData()
        {
            DataTable dtBranch = new DataTable("Header");
            DataColumn dcolHeadOfficeCode = new DataColumn("HEAD_OFFICE_CODE", typeof(string));
            DataColumn dcolBranchOfficeCode = new DataColumn("BRANCH_OFFICE_CODE", typeof(string));
            DataColumn dcolDateFrom = new DataColumn("DATE_FROM", typeof(DateTime));
            DataColumn dcolDateTo = new DataColumn("DATE_TO", typeof(DateTime));
            DataColumn dcolUploadedBy = new DataColumn("UPLOADED_BY", typeof(string));

            dtBranch.Columns.Add(dcolHeadOfficeCode);
            dtBranch.Columns.Add(dcolBranchOfficeCode);
            dtBranch.Columns.Add(dcolDateFrom);
            dtBranch.Columns.Add(dcolDateTo);
            dtBranch.Columns.Add(dcolUploadedBy);

            dtBranch.Rows.Add(HeadOfficeCode, BranchOfficeCode, this.DateSet.ToDate(DateFrom.ToShortDateString(), false), this.DateSet.ToDate(DateTo.ToShortDateString(), false), this.LoginUserName);
            return dtBranch;
        }
        private ResultArgs FetchVoucherTransactions()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchVoucherTransactions, SQLAdapterType.HOSQL))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            return resultArgs;
        }

        private ResultArgs FetchVoucherCostCentres()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchVoucherCostCentres, SQLAdapterType.HOSQL))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            return resultArgs;
        }

        private ResultArgs FetchDonors()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchDonors, SQLAdapterType.HOSQL))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            return resultArgs;
        }

        private ResultArgs FetchBankDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchBankDetails, SQLAdapterType.HOSQL))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            return resultArgs;
        }

        private ResultArgs FetchLedgerBalance()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchLedgerBalance, SQLAdapterType.HOSQL))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            return resultArgs;
        }

        private ResultArgs FetchCountry()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchCountry, SQLAdapterType.HOSQL))
            {
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            return resultArgs;
        }

        private ResultArgs FetchHeadOfficeLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FetchHeadOfficeLedger, SQLAdapterType.HOSQL))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            return resultArgs;
        }
        private ResultArgs FDAccountInvestments()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FDAccounts, SQLAdapterType.HOSQL))
            {
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            return resultArgs;
        }

        private ResultArgs FDRenewals()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FDRenewals, SQLAdapterType.HOSQL))
            {
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            return resultArgs;
        }
        private ResultArgs FDVoucherMasterTrans()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FDVoucherMasterTrans, SQLAdapterType.HOSQL))
            {
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            return resultArgs;
        }

        private ResultArgs FDVoucherTrans()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FDVoucherTrans, SQLAdapterType.HOSQL))
            {
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            return resultArgs;
        }

        private ResultArgs FDBankAccountDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FDBankAccountDetails, SQLAdapterType.HOSQL))
            {
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            return resultArgs;
        }

        private ResultArgs FDBankDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ExportVouchers.FDBankDetails, SQLAdapterType.HOSQL))
            {
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            return resultArgs;
        }

        #endregion
    }
}
