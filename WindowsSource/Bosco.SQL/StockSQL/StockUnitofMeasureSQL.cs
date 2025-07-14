using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class StockUnitofMeasureSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.StockUnitofMeasure).FullName)
            {
                query = GetstockUnitofMeasurSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        #region SQL Script
        public string GetstockUnitofMeasurSQL()
        {
            string query = "";
            SQLCommand.StockUnitofMeasure SqlcommandId = (SQLCommand.StockUnitofMeasure)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.StockUnitofMeasure.Add:
                    
{
                        query = "INSERT INTO STOCK_UNITOFMEASURE\n" +
                                "  (TYPE,\n" +
                                "   UNITTYPE_ID,\n" +
                                "   SYMBOL,\n" +
                                "   NAME,\n" +
                                "   DECIMAL_PLACE,\n" +
                                "   CONVERSION_OF,\n" +
                                "   FIRST_UNIT_ID,\n" +
                                "   SECOND_UNIT_ID)\n" +
                                "VALUES\n" +
                                "  (?TYPE,\n" +
                                 "  ?UNITTYPE_ID,\n" +
                                "   ?SYMBOL,\n" +
                                "   ?NAME,\n" +
                                "   ?DECIMAL_PLACE,\n" +
                                "   ?CONVERSION_OF,\n" +
                                "   ?FIRST_UNIT_ID,\n" +
                                "   ?SECOND_UNIT_ID)";

                        break;
                    }
                case SQLCommand.StockUnitofMeasure.Update:
                    {
                        query = "UPDATE STOCK_UNITOFMEASURE SET \n" +
                                "                           TYPE = ?TYPE,\n" +
                                "                           UNITTYPE_ID = ?UNITTYPE_ID,\n" +
                                "                           SYMBOL = ?SYMBOL,\n" +
                                "                           NAME = ?NAME,\n" +
                                "                           DECIMAL_PLACE = ?DECIMAL_PLACE,\n" +
                                "                           CONVERSION_OF = ?CONVERSION_OF,\n" +
                                "                           FIRST_UNIT_ID = ?FIRST_UNIT_ID,\n" +
                                "                           SECOND_UNIT_ID = ?SECOND_UNIT_ID\n" +
                                " WHERE UNIT_ID = ?UNIT_ID";
                        break;
                    }
                case SQLCommand.StockUnitofMeasure.FetchAll:
                    {
                        query = "SELECT UNIT_ID," +
                                "TYPE," +
                                "UNITTYPE_ID," +
                                "SYMBOL," +
                                "NAME," +
                                "DECIMAL_PLACE " +
                                "FROM STOCK_UNITOFMEASURE";
                        break;
                    }
                case SQLCommand.StockUnitofMeasure.Fetch:
                    {
                        query = "SELECT UNIT_ID,\n" +
                                "       TYPE,\n" +
                                "       UNITTYPE_ID,\n" +
                                "       SYMBOL,\n" +
                                "       NAME,\n" +
                                "       DECIMAL_PLACE,\n" +
                                "       CONVERSION_OF,\n" +
                                "       FIRST_UNIT_ID,\n" +
                                "       SECOND_UNIT_ID\n" +
                                "  FROM STOCK_UNITOFMEASURE\n" +
                                " WHERE UNIT_ID = ?UNIT_ID";
                        break;
                    }
                case SQLCommand.StockUnitofMeasure.FetchForFirstUnit:
                    {
                        query = "SELECT UNIT_ID, SYMBOL FROM STOCK_UNITOFMEASURE WHERE SYMBOL <> ''";
                        break;
                    }

                case SQLCommand.StockUnitofMeasure.Delete:
                    {
                        query = "DELETE FROM STOCK_UNITOFMEASURE WHERE UNIT_ID = ?UNIT_ID";
                        break;
                    }
            }
            return query;
        }
        #endregion
    }
}
