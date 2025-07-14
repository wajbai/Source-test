using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    class ManufactureInfoSQL : IDatabaseQuery
    {
        #region ISQLServerQuery Members
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
        #endregion

        #region SQL Script
        public string GetManufactureSQL()
        {
            string query = "";
            SQLCommand.ManufactureInfo SqlcommandId = (SQLCommand.ManufactureInfo)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.ManufactureInfo.Add:
                    {
                        query = "INSERT INTO ASSET_STOCK_MANUFACTURER(MANUFACTURER,\n" +
                                "ADDRESS,\n" +
                                "CONTACT_NO,\n" +
                                "EMAIL_ID,\n" +
                                "PAN_NO,\n" +
                                "GST_NO)\n" +
                                "VALUES(?MANUFACTURER,\n" +
                                "?ADDRESS,\n" +
                                "?CONTACT_NO,\n" +
                                "?EMAIL_ID,\n" +
                                "?PAN_NO,\n" +
                                "?GST_NO)";
                        break;
                    }

                case SQLCommand.ManufactureInfo.Update:
                    {
                        query = "UPDATE ASSET_STOCK_MANUFACTURER SET MANUFACTURER=?MANUFACTURER,\n" +
                                " ADDRESS=?ADDRESS,\n" +
                                "CONTACT_NO=?CONTACT_NO,\n" +
                                "EMAIL_ID=?EMAIL_ID, " +
                                "PAN_NO=?PAN_NO,\n" +
                                "GST_NO=?GST_NO\n" +
                                "WHERE MANUFACTURER_ID =?MANUFACTURER_ID ";
                        break;
                    }

                case SQLCommand.ManufactureInfo.FetchAll:
                    {
                        query = "SELECT MANUFACTURER_ID AS ID,MANUFACTURER_ID,\n" +
                                "                               MANUFACTURER AS NAME,MANUFACTURER,\n" + 
                                "                               ADDRESS,\n" + 
                                "                               CONTACT_NO,\n" + 
                                "                               EMAIL_ID,\n" +
                                "                               PAN_NO,\n" +
                                "                               GST_NO\n" +
                                "                          FROM ASSET_STOCK_MANUFACTURER;";

                        break;
                    }
                case SQLCommand.ManufactureInfo.Delete:
                    {
                        query = "DELETE FROM ASSET_STOCK_MANUFACTURER WHERE MANUFACTURER_ID =?MANUFACTURER_ID";
                        break;
                    }
                case SQLCommand.ManufactureInfo.Fetch:
                    {
                        query = "SELECT MANUFACTURER_ID,MANUFACTURER, ADDRESS,\n" +
                                "CONTACT_NO,PAN_NO,GST_NO, EMAIL_ID FROM ASSET_STOCK_MANUFACTURER WHERE MANUFACTURER_ID =?MANUFACTURER_ID";
                        break;
                    }
                case SQLCommand.ManufactureInfo.DeleteManufactureDetails:
                    {
                        query = "DELETE FROM ASSET_STOCK_MANUFACTURER";
                        break;
                    }
                case SQLCommand.ManufactureInfo.FetchManufactureNameByID:
                    {
                        query = "SELECT MANUFACTURER_ID FROM ASSET_STOCK_MANUFACTURER WHERE MANUFACTURER =?MANUFACTURER";
                        break;
                    }
            }
            return query;
        }
        #endregion
    }
}

