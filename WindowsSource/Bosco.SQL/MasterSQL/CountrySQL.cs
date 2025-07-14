/*  Class Name      : CountrySQL
 *  Purpose         : To have Manipulation query for Country
 *  Author          : Chinna
 *  Created on      : 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;
namespace Bosco.SQL
{
    public class CountrySQL : IDatabaseQuery
    {
        #region ISQLServerQueryMembers

        DataCommandArguments dataCommandArgs;
        SQLType sqlType;
        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.Country).FullName)
            {
                query = GetCountrySQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script

        private string GetCountrySQL()
        {
            string query = "";
            SQLCommand.Country sqlCommandId = (SQLCommand.Country)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {
                case SQLCommand.Country.Add:
                    {
                        query = "INSERT INTO MASTER_COUNTRY ( " +
                               "COUNTRY, " +
                               "COUNTRY_CODE, " +
                                "CURRENCY_CODE, " +
                               "CURRENCY_SYMBOL, " +
                               "CURRENCY_NAME ) VALUES( " +
                               "?COUNTRY, " +
                               "?COUNTRY_CODE, " +
                                "?CURRENCY_CODE, " +
                               "?CURRENCY_SYMBOL, " +
                               "?CURRENCY_NAME) ";

                        break;
                    }
                case SQLCommand.Country.Update:
                    {
                        query = "UPDATE MASTER_COUNTRY SET " +
                                    "COUNTRY =?COUNTRY, " +
                                    "COUNTRY_CODE =?COUNTRY_CODE, " +
                                     "CURRENCY_CODE =?CURRENCY_CODE, " +
                                    "CURRENCY_SYMBOL=?CURRENCY_SYMBOL, " +
                                    "CURRENCY_NAME=?CURRENCY_NAME " +
                                    "WHERE COUNTRY_ID=?COUNTRY_ID ";

                        break;
                    }

                case SQLCommand.Country.Delete:
                    {
                        query = "DELETE FROM MASTER_COUNTRY WHERE COUNTRY_ID=?COUNTRY_ID ";
                        break;
                    }

                case SQLCommand.Country.Fetch:
                    {
                        query = "SELECT " +
                               "COUNTRY_ID, " +
                               "COUNTRY, " +
                               "COUNTRY_CODE, " +
                                "CURRENCY_CODE, " +
                               "CURRENCY_SYMBOL, " +
                               "CURRENCY_NAME " +

                               "FROM " +
                               "MASTER_COUNTRY WHERE COUNTRY_ID=?COUNTRY_ID";
                        break;
                    }
                case SQLCommand.Country.FetchAll:
                    {

                        query = "SELECT MC.COUNTRY_ID, MC.COUNTRY, MC.COUNTRY_CODE, MC.CURRENCY_CODE, MC.CURRENCY_SYMBOL, MC.CURRENCY_NAME,\n" +
                                "      CONCAT('( ',\n" +
                                "              MC.CURRENCY_SYMBOL,\n" +
                                "              ' : ',\n" +
                                "              IFNULL(MC.COUNTRY, ''),\n" +
                                "              ') ',\n" +
                                "              ' - ',\n" +
                                "              IFNULL(MC.CURRENCY_NAME, '')) AS CURRENCY,\n" +
                                "       CONCAT('( ', MC.CURRENCY_SYMBOL, ')') AS CUR, IFNULL(MCCE.EXCHANGE_RATE, 0) AS EXCHANGE_RATE\n" +
                                "\n" +
                                "  FROM MASTER_COUNTRY MC\n" +
                                "  LEFT JOIN (SELECT COUNTRY_ID, EXCHANGE_RATE FROM MASTER_COUNTRY_CURRENCY_EXCHANGE_RATE MCCE" +
                                "   WHERE 1=1 { AND ?VOUCHER_DATE BETWEEN APPLICABLE_FROM AND APPLICABLE_TO} GROUP BY COUNTRY_ID) AS MCCE ON MCCE.COUNTRY_ID = MC.COUNTRY_ID" +
                                " WHERE MC.COUNTRY <> '' ORDER BY MC.COUNTRY ASC";

                        break;
                    }
                case SQLCommand.Country.FetchCountryCurrencyExchangeRateByCountryDate:
                    {
                        query = "SELECT MC.COUNTRY_ID, MC.COUNTRY, MC.COUNTRY_CODE, MC.CURRENCY_CODE, MC.CURRENCY_SYMBOL, MC.CURRENCY_NAME,\n" +
                                "      CONCAT('( ',\n" +
                                "              MC.CURRENCY_SYMBOL,\n" +
                                "              ' : ',\n" +
                                "              IFNULL(MC.COUNTRY, ''),\n" +
                                "              ') ',\n" +
                                "              ' - ',\n" +
                                "              IFNULL(MC.CURRENCY_NAME, '')) AS CURRENCY,\n" +
                                "       CONCAT('( ', MC.CURRENCY_SYMBOL, ')') AS CUR, IFNULL(MCCE.EXCHANGE_RATE, 0) AS EXCHANGE_RATE\n" +
                                "\n" +
                                "  FROM MASTER_COUNTRY MC\n" +
                                "  LEFT JOIN (SELECT COUNTRY_ID, EXCHANGE_RATE FROM MASTER_COUNTRY_CURRENCY_EXCHANGE_RATE MCCE" +
                                "   WHERE 1=1 { AND ?VOUCHER_DATE BETWEEN APPLICABLE_FROM AND APPLICABLE_TO} GROUP BY COUNTRY_ID) AS MCCE ON MCCE.COUNTRY_ID = MC.COUNTRY_ID" +
                                " WHERE 1=1 { AND MC.COUNTRY_ID = ?COUNTRY_ID}";
                        break;
                    }
                case SQLCommand.Country.FetchCountryCurrencyExchangeRateByFY:
                    {
                        query = "SELECT MC.COUNTRY_ID, MC.COUNTRY, MC.COUNTRY_CODE, MC.CURRENCY_CODE, MC.CURRENCY_SYMBOL, MC.CURRENCY_NAME,\n" +
                                "      CONCAT('( ',\n" +
                                "              MC.CURRENCY_SYMBOL,\n" +
                                "              ' : ',\n" +
                                "              IFNULL(MC.COUNTRY, ''),\n" +
                                "              ') ',\n" +
                                "              ' - ',\n" +
                                "              IFNULL(MC.CURRENCY_NAME, '')) AS CURRENCY,\n" +
                                "       CONCAT('( ', MC.CURRENCY_SYMBOL, ')') AS CUR, IFNULL(MCCE.EXCHANGE_RATE, 0) AS EXCHANGE_RATE\n" +
                                "\n" +
                                "  FROM MASTER_COUNTRY MC\n" +
                                "  LEFT JOIN (SELECT COUNTRY_ID, EXCHANGE_RATE FROM MASTER_COUNTRY_CURRENCY_EXCHANGE_RATE MCCE" +
                                "   WHERE 1=1 { AND APPLICABLE_FROM=?APPLICABLE_FROM AND APPLICABLE_TO=?APPLICABLE_TO} GROUP BY COUNTRY_ID) AS MCCE ON MCCE.COUNTRY_ID = MC.COUNTRY_ID" +
                                " WHERE 1=1 {MC.COUNTRY_ID = ?COUNTRY_ID}";
                        break;
                    }
                case SQLCommand.Country.FetchCountryList:
                    {
                        query = "SELECT " +
                               "COUNTRY_ID, " +
                               "COUNTRY " +
                           "FROM " +
                               "MASTER_COUNTRY ORDER BY COUNTRY ASC";
                        break;
                    }

                case SQLCommand.Country.FetchCountryCodeList:
                    {
                        query = "SELECT " +
                            "COUNTRY_ID, " +
                            "COUNTRY_CODE " +
                            "FROM " +
                            "MASTER_COUNTRY ORDER BY COUNTRY_CODE";
                        break;
                    }
                case SQLCommand.Country.FetchCurrencySymbolsList:
                    {
                        query = "SELECT " +
                             "COUNTRY_ID, " +
                             "CURRENCY_SYMBOL " +
                             "FROM " +
                             "MASTER_COUNTRY " +
                             "WHERE CURRENCY_SYMBOL <> '' AND COUNTRY_ID=?COUNTRY_ID " +
                             "ORDER BY CURRENCY_SYMBOL";
                        break;
                    }
                // to be changed
                //case SQLCommand.Country.FetchCurrencySymbols:
                //    {
                //        query = "SELECT " +
                //             "COUNTRY_ID, " +
                //             "CURRENCY_SYMBOL " +
                //             "FROM " +
                //             "MASTER_COUNTRY " +
                //             "WHERE CURRENCY_SYMBOL <> ''" +
                //             "ORDER BY CURRENCY_SYMBOL";
                //        break;
                //    }
                case SQLCommand.Country.FetchCurrencySymbols:
                    {
                        query = "SELECT distinct " +
                             "CURRENCY_SYMBOLS" +
                            " FROM " +
                             "COUNTRY_SYMBOLS " +
                             "WHERE CURRENCY_SYMBOLS <> ''" +
                            " ORDER BY CURRENCY_SYMBOLS";
                        break;
                    }

                case SQLCommand.Country.FetchCurrencyCodeList:
                    {
                        query = "SELECT " +
                             "COUNTRY_ID, " +
                             "CURRENCY_CODE " +
                             "FROM " +
                             "MASTER_COUNTRY ORDER BY CURRENCY_CODE";
                        break;
                    }
                case SQLCommand.Country.FetchCurrencyNameList:
                    {
                        query = "SELECT " +
                               "COUNTRY_ID, " +
                               "CURRENCY_NAME " +
                           "FROM " +
                               "MASTER_COUNTRY ORDER BY CURRENCY_NAME ASC";
                        break;
                    }

                case SQLCommand.Country.FetchCountryIdByName:
                    {
                        query = "SELECT COUNTRY_ID FROM MASTER_COUNTRY WHERE COUNTRY=?COUNTRY";
                        break;
                    }
                case SQLCommand.Country.FetchCountryCurrencyExchangeRate:
                    {
                        query = @"SELECT MCE.COUNTRY_ID, MCE.APPLICABLE_FROM, MCE.APPLICABLE_TO, MCE.EXCHANGE_RATE
                                    FROM MASTER_COUNTRY_CURRENCY_EXCHANGE_RATE AS MCE
                                    WHERE MCE.COUNTRY_ID =?COUNTRY_ID AND MCE.APPLICABLE_FROM>=?APPLICABLE_FROM AND MCE.APPLICABLE_FROM<=?APPLICABLE_TO";
                        break;
                    }
                case SQLCommand.Country.DeleteCountryCurrencyExchangeRate:
                    {
                        query = @"DELETE FROM MASTER_COUNTRY_CURRENCY_EXCHANGE_RATE 
                                    WHERE 1=1 {AND COUNTRY_ID =?COUNTRY_ID} AND APPLICABLE_FROM>=?APPLICABLE_FROM AND APPLICABLE_FROM<=?APPLICABLE_TO";
                        break;
                    }
                case SQLCommand.Country.DeleteAllCountryCurrencyExchangeRate:
                    {
                        query = @"DELETE FROM MASTER_COUNTRY_CURRENCY_EXCHANGE_RATE WHERE COUNTRY_ID =?COUNTRY_ID";
                        break;
                    }
                case SQLCommand.Country.UpdateCountryCurrencyExchangeRate:
                    {
                        query = @"INSERT INTO MASTER_COUNTRY_CURRENCY_EXCHANGE_RATE (COUNTRY_ID, APPLICABLE_FROM, APPLICABLE_TO, EXCHANGE_RATE)
                                      VALUES(?COUNTRY_ID, ?APPLICABLE_FROM, ?APPLICABLE_TO, ?EXCHANGE_RATE)";
                        break;
                    }
                case SQLCommand.Country.InsertACCountryCurrencyExchangeRate:
                    {
                        query = @"INSERT INTO MASTER_COUNTRY_CURRENCY_EXCHANGE_RATE (COUNTRY_ID, APPLICABLE_FROM, APPLICABLE_TO, EXCHANGE_RATE)
                                    SELECT MCR.COUNTRY_ID, ?APPLICABLE_FROM, ?APPLICABLE_TO, MCR.EXCHANGE_RATE
                                    FROM MASTER_COUNTRY_CURRENCY_EXCHANGE_RATE MCR
                                    INNER JOIN ACCOUNTING_YEAR AC ON AC.YEAR_FROM = MCR.APPLICABLE_FROM AND AC.YEAR_TO = MCR.APPLICABLE_TO
                                    WHERE AC.ACC_YEAR_ID = (SELECT ACC_YEAR_ID FROM ACCOUNTING_YEAR WHERE ACC_YEAR_ID<>?ACC_YEAR_ID ORDER BY YEAR_FROM DESC LIMIT 1);";
                        break;
                    }
                case SQLCommand.Country.DeleteACCountryCurrencyExchangeRate:
                    {
                        query = @"DELETE FROM MASTER_COUNTRY_CURRENCY_EXCHANGE_RATE
                                    WHERE APPLICABLE_FROM=?APPLICABLE_FROM AND APPLICABLE_TO=?APPLICABLE_TO";
                        break;
                    }
            }
            return query;

        }
        #endregion
    }
}

