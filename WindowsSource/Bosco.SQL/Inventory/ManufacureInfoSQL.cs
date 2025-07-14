using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    class ManufacureInfoSQL : IDatabaseQuery
    {
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;

        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.ManufactureInfo).FullName)
            {
                query = GetManufactureSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #region SQL Script
        public string GetManufactureSQL()
        {
            string query = "";
            SQLCommand.ManufactureInfo SqlcommandId = (SQLCommand.ManufactureInfo)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.ManufactureInfo.Add:
                    {
                        query = "INSERT INTO ASSET_MANUFACTURE_INFO(NAME,\n" +
                                "ADDRESS,\n" +
                                "CITY,\n" +
                                "STATE,\n" +
                                "POSTAL_CODE,\n" +
                                "PANNO,\n" +
                                "COUNTRY,\n" +
                                "TELEPHONE_NO,\n" +
                                "EMAIL_ID)\n" +
                                "VALUES(?NAME,\n" +
                                "?ADDRESS,\n" +
                                "?CITY,\n" +
                                "?STATE,\n" +
                                "?POSTAL_CODE,\n" +
                                "?PANNO,\n" +
                                "?COUNTRY,\n" +
                                "?TELEPHONE_NO,\n" +
                                "?EMAIL_ID)";
                        break;
                    }

                case SQLCommand.ManufactureInfo.Update:
                    {
                        query = "UPDATE ASSET_MANUFACTURE_INFO SET NAME=?NAME,\n" +
                                " ADDRESS=?ADDRESS,\n" +
                                "CITY=?CITY,\n" +
                                "STATE=?STATE,\n" +
                                "POSTAL_CODE =POSTAL_CODE,\n" +
                                "PANNO=?PANNO,\n" +
                                "COUNTRY=?COUNTRY,\n" +
                                "TELEPHONE_NO=?TELEPHONE_NO,\n" +
                                "EMAIL_ID=?EMAIL_ID " +
                                "WHERE MANUFACTURE_ID =?MANUFACTURE_ID ";
                        break;
                    }

                case SQLCommand.ManufactureInfo.FetchAll:
                    {
                        query = "SELECT MANUFACTURE_ID,\n" +
                        "       NAME,\n" +
                        "       ADDRESS,\n" +
                        "       CITY,\n" +
                        "       STATE_NAME AS STATE,\n" +
                        "       POSTAL_CODE,\n" +
                        "       PANNO,\n" +
                        "       MC.COUNTRY,\n" +
                        "       TELEPHONE_NO,\n" +
                        "       EMAIL_ID\n" +
                        "  FROM ASSET_MANUFACTURE_INFO AS AMI\n" +
                        "  LEFT JOIN MASTER_COUNTRY AS MC\n" +
                        "    ON AMI.COUNTRY = MC.COUNTRY_ID\n" +
                        "  LEFT JOIN MASTER_STATE AS MS\n" +
                        "    ON AMI.STATE = MS.STATE_ID";
                        break;
                    }
                case SQLCommand.ManufactureInfo.Delete:
                    {
                        query = "DELETE FROM ASSET_MANUFACTURE_INFO WHERE MANUFACTURE_ID =?MANUFACTURE_ID";
                        break;
                    }
                case SQLCommand.ManufactureInfo.Fetch:
                    {
                        query = "SELECT MANUFACTURE_ID,NAME, ADDRESS, CITY, STATE, POSTAL_CODE, PANNO,\n" +
                                "COUNTRY, TELEPHONE_NO, EMAIL_ID FROM ASSET_MANUFACTURE_INFO WHERE MANUFACTURE_ID = ?MANUFACTURE_ID";
                        break;
                    }
            }
            return query;
        }
        #endregion
    }
}

