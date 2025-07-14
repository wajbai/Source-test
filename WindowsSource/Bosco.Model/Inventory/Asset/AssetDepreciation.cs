using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using System.Data;
using Bosco.Model.Transaction;
using Bosco.Model.UIModel;

namespace Bosco.Model
{
    public class AssetDepreciation : SystemBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Property
        public int DepId { get; set; }
        public DateTime DepApplied { get; set; }
        public int ItemId { get; set; }
        public string Narration { get; set; }
        public string VoucherNo { get; set; }
        public DateTime DepPeriodFrom { get; set; }
        public DateTime DepPeriodTo { get; set; }
        public int DepreciationPer { get; set; }
        public int ProjectId { get; set; }
        public int VoucherId { get; set; }
        public DateTime FenceDeprec { get; set; }
        public int DepMethod { get; set; }
        public DataTable dtDepreciation { get; set; }

        public DataTable dtFinanceLedgerDetails { get; set; }
        public DataTable dtDepreciationLedgerDetails { get; set; }
        public int dtFinanceDeprLedgerID { get; set; }
        public double dtFinanceDeprLedgerAmount { get; set; }
        #endregion

        #region Constructor
        public AssetDepreciation()
        {

        }
        #endregion

        #region Methods
        public ResultArgs SaveDepreciation()
        {
            using (DataManager datamanager = new DataManager())
            {
                datamanager.BeginTransaction();
                // Delete concenrn Period Details
                if (DepId > 0)
                {
                    resultArgs = DeleteDepreciation(DepId);
                    if (resultArgs.Success)
                    {
                        resultArgs = DeleteMasterDepreciation(DepId);
                    }
                }
                // Save Voucher Details
                resultArgs = SaveDepreciationVouchers(datamanager);
                if (resultArgs != null && resultArgs.Success)
                {
                    // Save Master Details
                    resultArgs = SaveDepreciationMaster();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        DepId = resultArgs.RowUniqueId != null ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : 0;
                        if (DepId > 0)
                        {
                            // Save Details
                            resultArgs = SaveDepreciationDetails();
                        }
                    }
                }
                datamanager.EndTransaction();
            }
            return resultArgs;
        }

        public ResultArgs SaveDepreciationMaster()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherDepreciation.AddDepreciationMaster))
            {
                DepId = 0;
                //  dataManager.Parameters.Add(this.AppSchema.DepreciationMaster.DEPRECIATION_IDColumn, DepId, true);
                dataManager.Parameters.Add(this.AppSchema.DepreciationMaster.DEPRECIATION_APPLIED_ONColumn, DepApplied);
                dataManager.Parameters.Add(this.AppSchema.DepreciationMaster.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.DepreciationMaster.DEPRECIATION_PERIOD_FROMColumn, DepPeriodFrom);
                dataManager.Parameters.Add(this.AppSchema.DepreciationMaster.DEPRECIATION_PERIOD_TOColumn, DepPeriodTo);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                dataManager.Parameters.Add(this.AppSchema.DepreciationMaster.NARRATIONColumn, Narration);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveDepreciationDetails()
        {
            foreach (DataRow drDepreciation in dtDepreciation.Rows)
            {
                using (DataManager datamanager = new DataManager(SQLCommand.VoucherDepreciation.AddDepreciationDetail))
                {
                    datamanager.Parameters.Add(this.AppSchema.DepreciationDetail.DEPRECIATION_IDColumn, DepId);

                    datamanager.Parameters.Add(this.AppSchema.DepreciationDetail.ITEM_DETAIL_IDColumn,
                        (drDepreciation[this.AppSchema.DepreciationDetail.ITEM_DETAIL_IDColumn.ColumnName] != null) ?
                        this.NumberSet.ToInteger(drDepreciation[this.AppSchema.DepreciationDetail.ITEM_DETAIL_IDColumn.ColumnName].ToString()) : 0);

                    datamanager.Parameters.Add(this.AppSchema.DepreciationDetail.METHOD_IDColumn,
                        (drDepreciation[this.AppSchema.DepreciationDetail.METHOD_IDColumn.ColumnName] != null) ?
                        this.NumberSet.ToInteger(drDepreciation[this.AppSchema.DepreciationDetail.METHOD_IDColumn.ColumnName].ToString()) : 0);

                    datamanager.Parameters.Add(this.AppSchema.DepreciationDetail.DEPRECIATION_PERCENTAGEColumn,
                        (drDepreciation[this.AppSchema.DepreciationDetail.DEPRECIATION_PERCENTAGEColumn.ColumnName] != null) ?
                        this.NumberSet.ToDouble(drDepreciation[this.AppSchema.DepreciationDetail.DEPRECIATION_PERCENTAGEColumn.ColumnName].ToString()) : 0);

                    datamanager.Parameters.Add(this.AppSchema.DepreciationDetail.DEPRECIATION_VALUEColumn,
                        (drDepreciation[this.AppSchema.DepreciationDetail.DEPRECIATION_VALUEColumn.ColumnName] != null) ?
                        this.NumberSet.ToDouble(drDepreciation[this.AppSchema.DepreciationDetail.DEPRECIATION_VALUEColumn.ColumnName].ToString()) : 0);

                    datamanager.Parameters.Add(this.AppSchema.DepreciationDetail.BALANCE_AMOUNTColumn,
                        (drDepreciation[this.AppSchema.DepreciationDetail.BALANCE_AMOUNTColumn.ColumnName] != null) ?
                        this.NumberSet.ToDouble(drDepreciation[this.AppSchema.DepreciationDetail.BALANCE_AMOUNTColumn.ColumnName].ToString()) : 0);

                    datamanager.Parameters.Add(this.AppSchema.DepreciationDetail.DEPRECIATION_APPLY_FROMColumn,
                        DateSet.ToDate(drDepreciation["DATE_OF_APPLY"].ToString(), false));

                    datamanager.Parameters.Add(this.AppSchema.DepreciationDetail.DEPRECIATON_PERIOD_TOColumn,
                        DateSet.ToDate(drDepreciation["DATE_OF_APPLY_TO"].ToString(), false));

                    resultArgs = datamanager.UpdateData();

                }
            }
            return resultArgs;
        }

        public ResultArgs DeleteDepreciation(int DepId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherDepreciation.DeleteDepreciation))
            {
                dataManager.Parameters.Add(this.AppSchema.DepreciationMaster.DEPRECIATION_IDColumn, DepId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteMasterDepreciation(int DepId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherDepreciation.DeleteMasterDepreciation))
            {
                dataManager.Parameters.Add(this.AppSchema.DepreciationMaster.DEPRECIATION_IDColumn, DepId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        public ResultArgs FetchDepreciationDetails()
        {

            using (DataManager datamanager = new DataManager(SQLCommand.VoucherDepreciation.FetchAll))
            {
                datamanager.Parameters.Add(this.AppSchema.DepreciationMaster.PROJECT_IDColumn, ProjectId);
                datamanager.Parameters.Add(this.AppSchema.DepreciationMaster.MONTH_DATEColumn, FenceDeprec);
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAccountLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherDepreciation.FetchAccountLedger))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchDepreciationMappedLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherDepreciation.FetchDepreciationLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.DepreciationMaster.PROJECT_IDColumn, ProjectId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion

        #region Depreciation Add Methods

        public ResultArgs FetchApplyDepreciationDetails()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.VoucherDepreciation.FetchApplyDepreciationDetails))
            {
                datamanager.Parameters.Add(this.AppSchema.DepreciationMaster.DEPRECIATION_IDColumn, DepId);
                datamanager.Parameters.Add(this.AppSchema.DepreciationMaster.PROJECT_IDColumn, ProjectId);
                datamanager.Parameters.Add(this.AppSchema.DepreciationMaster.DEPRECIATION_PERIOD_FROMColumn, DepPeriodFrom);
                datamanager.Parameters.Add(this.AppSchema.DepreciationMaster.DEPRECIATION_PERIOD_TOColumn, DepPeriodTo);
                if (DepId.Equals(0))
                {
                    datamanager.Parameters.Add(this.AppSchema.VoucherMaster.STATUSColumn, 1);
                }
                else
                {
                    datamanager.Parameters.Add(this.AppSchema.DepriciationVoucherDetail.DEP_STATUSColumn, 1);
                }
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchPreviousRenewal()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherDepreciation.FetchPreviousRenewal))
            {
                dataManager.Parameters.Add(this.AppSchema.DepreciationMaster.DEPRECIATION_IDColumn, DepId);
                dataManager.Parameters.Add(this.AppSchema.DepreciationMaster.DEPRECIATION_PERIOD_FROMColumn, DepPeriodFrom);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchNextRenewal()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherDepreciation.FetchNextRenewal))
            {
                dataManager.Parameters.Add(this.AppSchema.DepreciationMaster.DEPRECIATION_IDColumn, DepId);
                dataManager.Parameters.Add(this.AppSchema.DepreciationMaster.DEPRECIATION_PERIOD_FROMColumn, DepPeriodFrom);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchMaxRenewal()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherDepreciation.FetchMaxRenewal))
            {
                dataManager.Parameters.Add(this.AppSchema.DepreciationMaster.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.DepreciationMaster.DEPRECIATION_PERIOD_FROMColumn, DateSet.ToDate(YearFrom));
                dataManager.Parameters.Add(this.AppSchema.DepreciationMaster.DEPRECIATION_PERIOD_TOColumn, DateSet.ToDate(YearTo));
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchDeprVoucherProperties()
        {
            resultArgs = FetchVoucherMasterById();
            if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                DepApplied = DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DepreciationMaster.DEPRECIATION_APPLIED_ONColumn.ColumnName].ToString(), false);
                DepPeriodFrom = DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DepreciationMaster.DEPRECIATION_PERIOD_FROMColumn.ColumnName].ToString(), false);
                DepPeriodTo = DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DepreciationMaster.DEPRECIATION_PERIOD_TOColumn.ColumnName].ToString(), false);
                Narration = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DepreciationMaster.NARRATIONColumn.ColumnName].ToString();
                VoucherId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString());
            }
            return resultArgs;
        }

        public ResultArgs FetchVoucherMasterById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherDepreciation.FetchVoucherMasterById))
            {
                dataManager.Parameters.Add(this.AppSchema.DepreciationMaster.DEPRECIATION_IDColumn, DepId);
                dataManager.Parameters.Add(this.AppSchema.DepreciationMaster.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int FetchDepreciationExist()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherDepreciation.FetchExistorNot))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs FetchFinanceVoucherDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherDepreciation.FetchFinanceVoucherDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        public ResultArgs SaveDepreciationVouchers(DataManager dm)
        {
            try
            {
                resultArgs.Success = true;
                using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                {
                    voucherTransaction.VoucherId = VoucherId;
                    voucherTransaction.ProjectId = ProjectId;
                    voucherTransaction.TransVoucherMethod = (int)TransactionVoucherMethod.Automatic;
                    voucherTransaction.VoucherType = VoucherSubTypes.JN.ToString();  //DefaultVoucherTypes.Journal.ToString();
                    voucherTransaction.VoucherDefinitionId = (int)DefaultVoucherTypes.Journal;
                    voucherTransaction.VoucherDate = DepApplied;
                    voucherTransaction.VoucherSubType = VoucherSubTypes.AST.ToString();
                    voucherTransaction.Narration = Narration;
                    voucherTransaction.NameAddress = string.Empty;
                    voucherTransaction.CreatedBy = NumberSet.ToInteger(this.LoginUserId);
                    voucherTransaction.ModifiedBy = NumberSet.ToInteger(this.LoginUserId);
                    voucherTransaction.Status = 1;
                    voucherTransaction.VoucherNo = VoucherNo;

                    this.TransInfo = dtFinanceLedgerDetails.DefaultView;
                    //this.CashTransInfo = dtDepreciationLedgerDetails.DefaultView;

                    decimal exchangerate = 1;
                    decimal dTransDRAmount = this.NumberSet.ToDecimal(this.TransInfo.Table.Compute("SUM(" + "DEBIT" + ")", string.Empty).ToString());
                    //On 21/10/2024, If multi currency enabled, set local exchange rate and amount alone -----------------------------------------
                    voucherTransaction.CurrencyCountryId = voucherTransaction.ExchageCountryId = 0;
                    voucherTransaction.ExchangeRate = exchangerate;
                    voucherTransaction.CalculatedAmount = 0;
                    voucherTransaction.ActualAmount = 0;
                    if (this.AllowMultiCurrency == 1)
                    {
                        using (CountrySystem country = new CountrySystem())
                        {
                            exchangerate = country.FetchSettingCountryCurrencyExchangeRate(DateSet.ToDate(this.YearFrom, false));
                        }
                        voucherTransaction.CurrencyCountryId = this.NumberSet.ToInteger(string.IsNullOrEmpty(this.Country) ? "0" : this.Country);
                        voucherTransaction.ExchageCountryId = voucherTransaction.CurrencyCountryId;
                        voucherTransaction.ExchangeRate = 1;
                        voucherTransaction.CalculatedAmount = (dTransDRAmount * exchangerate);
                        voucherTransaction.ContributionAmount = dTransDRAmount;
                        voucherTransaction.ActualAmount = (dTransDRAmount * exchangerate);
                    }

                    resultArgs = voucherTransaction.SaveVoucherDetails(dm);
                    if (resultArgs.Success)
                    {
                        VoucherId = voucherTransaction.VoucherId;
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }
        #endregion
    }
}
