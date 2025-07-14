using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class StockItemSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.StockItem).FullName)
            {
                query = GetstockItemSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion


        #region SQL Script
        public string GetstockItemSQL()
        {
            string query = "";
            SQLCommand.StockItem SqlcommandId = (SQLCommand.StockItem)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.StockItem.Add:
                    {
                        query = "INSERT INTO STOCK_ITEM (" +
                                "NAME," +
                                "GROUP_ID," +
                                "CATEGORY_ID," +
                                "UNIT_ID," +
                                "QUANTITY," +
                                "RATE," +
                                "VALUE)VALUES(" +
                                "?NAME," +
                                "?GROUP_ID, " +
                                "?CATEGORY_ID," +
                                "?UNIT_ID, " +
                                "?QUANTITY," +
                                "?RATE," +
                                "?VALUE)";
                        break;
                    }

                case SQLCommand.StockItem.Update:
                    {
                        query = "UPDATE STOCK_ITEM SET " +
                                "NAME=?NAME," +
                                "GROUP_ID=?GROUP_ID," +
                                "CATEGORY_ID=?CATEGORY_ID," +
                                "UNIT_ID=?UNIT_ID," +
                                "QUANTITY=?QUANTITY," +
                                "RATE=?RATE," +
                                "VALUE=?VALUE " +
                                "WHERE ITEM_ID =?ITEM_ID";
                        break;
                    }

                case SQLCommand.StockItem.FetchAll:
                    {
                        query = "SELECT ITEM_ID, SI.NAME, SI.GROUP_ID,SG.NAME AS 'GROUP_NAME', SC.NAME AS 'CATEGORY_NAME' , SU.NAME AS 'UNIT_NAME', QUANTITY, RATE, VALUE FROM STOCK_ITEM SI " +
                                "INNER JOIN STOCK_GROUP SG ON SI.GROUP_ID = SG.GROUP_ID LEFT JOIN STOCK_CATEGORY SC ON SI.CATEGORY_ID = SC.CATEGORY_ID LEFT JOIN " +
                                "STOCK_UNITOFMEASURE SU ON SU.UNIT_ID =SI.UNIT_ID ";
                        break;
                    }
                case SQLCommand.StockItem.Delete:
                    {
                        query = "DELETE FROM STOCK_ITEM WHERE ITEM_ID =?ITEM_ID";
                        break;
                    }
                case SQLCommand.StockItem.Fetch:
                    {
                        query = "SELECT ITEM_ID, NAME, GROUP_ID, CATEGORY_ID, UNIT_ID, QUANTITY, RATE, VALUE FROM STOCK_ITEM WHERE ITEM_ID =?ITEM_ID";
                        break;
                    }
            }
            return query;
        }
        #endregion
    }
}
