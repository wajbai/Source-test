/*  Class Name      : CountrySystem.cs
 *  Purpose         : To have all the logic of Country Details
 *  Author          : Chinna
 *  Created on      : 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.DAO.Schema;
using Bosco.DAO.Data;
using System.Runtime.InteropServices;
using System.Data;
using Bosco.Model.Transaction;
using AcMEDSync.Model;
using Bosco.Model.Setting;
using Bosco.Utility.ConfigSetting;

namespace Bosco.Model.UIModel
{
    public class CountrySystem : SystemBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor

        public CountrySystem()
        {
        }
        public CountrySystem(int CountryId)
        {
            FillCountryDetails(CountryId);
        }
        #endregion

        #region Country Properties
        public int CountryId { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public new string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
        public string CurrencyName { get; set; }
        public double ExchangeRate { get; set; }
        #endregion

        #region Methods
        public ResultArgs FetchCountryDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Country.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchCountryCurrencyDetails(DateTime date)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Country.FetchAll))
            {
                dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_DATEColumn, date);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchCountryListDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Country.FetchCountryList))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchCountryCodeListDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Country.FetchCountryCodeList))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchCurrencySymbolsListDetails(int CountryId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Country.FetchCurrencySymbolsList))
            {
                dataManager.Parameters.Add(AppSchema.Country.COUNTRY_IDColumn, CountryId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchCurrencySymbolsList()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Country.FetchCurrencySymbols))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchCurrencyCodeListDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Country.FetchCurrencyCodeList))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchCurrencyNameListDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Country.FetchCurrencyNameList))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        public ResultArgs DeleteCountryDetails(int CountryId)
        {
            this.CountryId = CountryId;
            ResultArgs resultArgs = new ResultArgs();

            using (VoucherTransactionSystem vsystem = new VoucherTransactionSystem())
            {
                if (!vsystem.IsVoucherMadeForCountry(this.CountryId))
                {
                    using (LedgerSystem ledgersystem = new LedgerSystem())
                    {
                        bool isCurrecnyCashBankLedger = ledgersystem.IsCurrencyEnabledCashBankLedgerExists(CountryId);
                        if (isCurrecnyCashBankLedger)
                        {
                            resultArgs.Message = "As this country has Currency enabled Cash/Bank Ledgers, You can't delete";
                        }
                        else
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.Country.Delete))
                            {
                                dataManager.BeginTransaction();
                                dataManager.Parameters.Add(AppSchema.Country.COUNTRY_IDColumn, CountryId);
                                resultArgs = dataManager.UpdateData();
                                if (resultArgs.Success)
                                {
                                    resultArgs = DeleteAllCountryCurrencyExchangeRate(dataManager);
                                }
                                dataManager.EndTransaction();
                            }
                        }
                    }
                }
                else
                {
                    resultArgs.Message = "Can't delete Country, It has association with Vouchers.";
                }
            }

            return resultArgs;
        }

        public ResultArgs SaveCountryDetails()
        {
            using (DataManager dataManager = new DataManager((CountryId == 0) ? SQLCommand.Country.Add : SQLCommand.Country.Update))
            {
                dataManager.BeginTransaction();
                dataManager.Parameters.Add(AppSchema.Country.COUNTRY_IDColumn, CountryId, true);
                dataManager.Parameters.Add(AppSchema.Country.COUNTRYColumn, CountryName);
                dataManager.Parameters.Add(AppSchema.Country.COUNTRY_CODEColumn, CountryCode);
                dataManager.Parameters.Add(AppSchema.Country.CURRENCY_CODEColumn, CurrencyCode);
                dataManager.Parameters.Add(AppSchema.Country.CURRENCY_SYMBOLColumn, CurrencySymbol);
                dataManager.Parameters.Add(AppSchema.Country.CURRENCY_NAMEColumn, CurrencyName);
                resultArgs = dataManager.UpdateData();
                if (resultArgs.Success)
                {
                    CountryId = CountryId.Equals(0) ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : CountryId;

                    resultArgs = UpdateCountryCurrencyExchangeRate(dataManager);
                }
                dataManager.EndTransaction();

                //On 09/09/2024, Refresh Opening Balances after updating Opening Balances
                //For Temp pupose, we refresh entire year
                if (resultArgs.Success && this.AllowMultiCurrency == 1 && DateSet.ToDate(this.YearFrom, false) == this.FirstFYDateFrom)
                {
                    using (BalanceSystem balanceSystem = new BalanceSystem())
                    {
                        resultArgs = balanceSystem.UpdateCashBankOpBalanceByExchangeRate(this.BookBeginFrom);
                    }
                }

                //13/09/2024, To Reset Global setting country details concern country setting
                if (resultArgs.Success && base.Country == CountryId.ToString())
                {
                    using (GlobalSetting globalsetting = new GlobalSetting())
                    {
                        Int32 userid = NumberSet.ToInteger(this.LoginUserId);
                        resultArgs = globalsetting.SaveSetting(Bosco.Utility.Setting.Currency, CurrencySymbol, userid);
                        resultArgs = globalsetting.SaveSetting(Bosco.Utility.Setting.CurrencyName, CurrencyName, userid);
                        resultArgs = globalsetting.SaveSetting(Bosco.Utility.Setting.CurrencyCode, CurrencyCode, userid);
                        ResultArgs result = globalsetting.FetchSettingDetails(NumberSet.ToInteger(this.LoginUserId));
                        if (result.Success)
                        {
                            SettingProperty.Current.UISettingInfo = result.DataSource.TableView;
                        }
                    }
                }
            }
            return resultArgs;
        }



        private ResultArgs FillCountryDetails(int CountryId)
        {
            this.CountryId = CountryId;
            resultArgs = CountryDetailsbyId(CountryId);
            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                CountryName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Country.COUNTRYColumn.ColumnName].ToString();
                CountryCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Country.COUNTRY_CODEColumn.ColumnName].ToString();
                CurrencyCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Country.CURRENCY_CODEColumn.ColumnName].ToString();
                CurrencySymbol = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Country.CURRENCY_SYMBOLColumn.ColumnName].ToString();
                CurrencyName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Country.CURRENCY_NAMEColumn.ColumnName].ToString();

                //On 20/08/2024, To Update Bank Account Ledger's exchange rate
                ExchangeRate = 0;
                if (resultArgs.Success)
                {
                    resultArgs = FetchCountryCurrencyExchangeRate();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        DataTable dtExchangeRate = resultArgs.DataSource.Table;
                        if (dtExchangeRate.Rows.Count > 0)
                        {
                            ExchangeRate = NumberSet.ToDouble(dtExchangeRate.Rows[0][AppSchema.Country.EXCHANGE_RATEColumn.ColumnName].ToString());
                        }
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs FetchCountryCurrencyExchangeRate()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Country.FetchCountryCurrencyExchangeRate))
            {
                dataManager.Parameters.Add(this.AppSchema.Country.COUNTRY_IDColumn, CountryId);
                dataManager.Parameters.Add(this.AppSchema.Country.APPLICABLE_FROMColumn, YearFrom);
                dataManager.Parameters.Add(this.AppSchema.Country.APPLICABLE_TOColumn, YearTo);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        
        public ResultArgs UpdateCountryCurrencyExchangeRate(DataManager dm=null)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Country.UpdateCountryCurrencyExchangeRate))
            {
                if (dm!=null) dataManager.Database = dm.Database;
                resultArgs = DeleteCountryCurrencyExchangeRate(dataManager);
                if (resultArgs.Success)
                {
                    dataManager.Database = dm.Database;
                    dataManager.Parameters.Add(this.AppSchema.Country.COUNTRY_IDColumn, CountryId);
                    dataManager.Parameters.Add(this.AppSchema.Country.APPLICABLE_FROMColumn, YearFrom);
                    dataManager.Parameters.Add(this.AppSchema.Country.APPLICABLE_TOColumn, YearTo);
                    dataManager.Parameters.Add(this.AppSchema.Country.EXCHANGE_RATEColumn, ExchangeRate);
                    resultArgs = dataManager.UpdateData();
                }

            }
            return resultArgs;
        }

        public ResultArgs DeleteCountryCurrencyExchangeRate(DataManager dm=null)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Country.DeleteCountryCurrencyExchangeRate))
            {
                if (dm != null) dataManager.Database = dm.Database; 
                if (CountryId>0)
                    dataManager.Parameters.Add(this.AppSchema.Country.COUNTRY_IDColumn, CountryId);

                dataManager.Parameters.Add(this.AppSchema.Country.APPLICABLE_FROMColumn, YearFrom);
                dataManager.Parameters.Add(this.AppSchema.Country.APPLICABLE_TOColumn, YearTo);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteAllCountryCurrencyExchangeRate(DataManager dm)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Country.DeleteAllCountryCurrencyExchangeRate))
            {
                dataManager.Database = dm.Database;
                dataManager.Parameters.Add(this.AppSchema.Country.COUNTRY_IDColumn, CountryId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        

        private ResultArgs CountryDetailsbyId(int CountryId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Country.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.Country.COUNTRY_IDColumn, CountryId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchCountryCurrencyExchangeRateByCountryDate(int CountryId, DateTime date)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Country.FetchCountryCurrencyExchangeRateByCountryDate))
            {
                dataManager.Parameters.Add(this.AppSchema.Country.COUNTRY_IDColumn, CountryId);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_DATEColumn, date);
                resultArgs = dataManager.FetchData(DataSource.DataTable);

                if (resultArgs.Success && resultArgs.DataSource.Table!=null)
                {
                    DataTable dtCountyExchangeDate = resultArgs.DataSource.Table;
                    if (resultArgs.Success && dtCountyExchangeDate.Rows.Count > 0)
                    {
                        CountryName = dtCountyExchangeDate.Rows[0][this.AppSchema.Country.COUNTRYColumn.ColumnName].ToString();
                        CountryCode = dtCountyExchangeDate.Rows[0][this.AppSchema.Country.COUNTRY_CODEColumn.ColumnName].ToString();
                        CurrencyCode = dtCountyExchangeDate.Rows[0][this.AppSchema.Country.CURRENCY_CODEColumn.ColumnName].ToString();
                        CurrencySymbol = dtCountyExchangeDate.Rows[0][this.AppSchema.Country.CURRENCY_SYMBOLColumn.ColumnName].ToString();
                        CurrencyName = dtCountyExchangeDate.Rows[0][this.AppSchema.Country.CURRENCY_NAMEColumn.ColumnName].ToString();

                        //On 20/08/2024, To Update Bank Account Ledger's exchange rate
                        ExchangeRate = 0;
                        if (resultArgs.Success)
                        {
                            ExchangeRate = NumberSet.ToDouble(dtCountyExchangeDate.Rows[0][AppSchema.Country.EXCHANGE_RATEColumn.ColumnName].ToString());
                        }
                        if (ExchangeRate == 0) ExchangeRate = 1;
                    }
                }
            }
            return resultArgs;
        }

        public decimal FetchSettingCountryCurrencyExchangeRate(DateTime date)
        {
            decimal rtn = 1;
            ResultArgs result = FetchCountryCurrencyExchangeRateByCountryDate(NumberSet.ToInteger(base.Country), date);
            if (result.Success)
            {
                rtn = NumberSet.ToDecimal(ExchangeRate.ToString());
            }

            return rtn;
        }

        public ResultArgs FetchCountryCurrencyExchangeRateByFY()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Country.FetchCountryCurrencyExchangeRateByFY))
            {
                dataManager.Parameters.Add(this.AppSchema.Country.APPLICABLE_FROMColumn, this.YearFrom);
                dataManager.Parameters.Add(this.AppSchema.Country.APPLICABLE_TOColumn, this.YearTo);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }

        public int GetCountryId(string CountryName)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Country.FetchCountryIdByName))
            {
                dataManager.Parameters.Add(this.AppSchema.Country.COUNTRYColumn, CountryName);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs InsertACCountryCurrencyExchangeRate(Int32 AccYearId, string ApplicableFrom, string ApplicableTo, DataManager dm = null)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Country.InsertACCountryCurrencyExchangeRate))
            {
                if (dm != null) dataManager.Database = dm.Database;
                dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.ACC_YEAR_IDColumn, AccYearId);
                dataManager.Parameters.Add(this.AppSchema.Country.APPLICABLE_FROMColumn, ApplicableFrom);
                dataManager.Parameters.Add(this.AppSchema.Country.APPLICABLE_TOColumn, ApplicableTo);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteACCountryCurrencyExchangeRate(string ApplicableFrom, string ApplicableTo, DataManager dm = null)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Country.DeleteACCountryCurrencyExchangeRate))
            {
                if (dm != null) dataManager.Database = dm.Database;
                dataManager.Parameters.Add(this.AppSchema.Country.APPLICABLE_FROMColumn, ApplicableFrom);
                dataManager.Parameters.Add(this.AppSchema.Country.APPLICABLE_TOColumn, ApplicableTo);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        #endregion
    }
}
