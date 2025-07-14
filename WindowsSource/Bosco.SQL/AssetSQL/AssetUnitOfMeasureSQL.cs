using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    class UnitOfMeasureSQL:IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.AssetUnitOfMeasure).FullName)
            {
                query = GetLocationSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the Location details.
        /// </summary>
        /// <returns></returns>
        private string GetLocationSQL()
        {
            string query = "";
            SQLCommand.AssetUnitOfMeasure sqlCommandId = (SQLCommand.AssetUnitOfMeasure)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.AssetUnitOfMeasure.Add:
                    {
                        query="INSERT INTO ASSET_UNITOFMEASURE\n" +
                                "  (TYPE,\n" + 
                                "   SYMBOL,\n" + 
                                "   NAME,\n" + 
                                "   DECIMAL_PLACE,\n" + 
                                "   CONVERSION_OF,\n" + 
                                "   FIRST_UNIT_ID,\n" +
                                "   UNITTYPE_ID,\n" + 
                                "   SECOND_UNIT_ID)\n" + 
                                "VALUES\n" + 
                                "  (?TYPE,\n" + 
                                "   ?SYMBOL,\n" + 
                                "   ?NAME,\n" + 
                                "   ?DECIMAL_PLACE,\n" + 
                                "   ?CONVERSION_OF,\n" + 
                                "   ?FIRST_UNIT_ID,\n" +
                                "   ?UNITTYPE_ID,\n" + 
                                "   ?SECOND_UNIT_ID)";
                        break;
                    }
                case SQLCommand.AssetUnitOfMeasure.Update:
                    {
                        query ="UPDATE ASSET_UNITOFMEASURE SET \n" +
                                "                           TYPE = ?TYPE,\n" + 
                                "                           SYMBOL = ?SYMBOL,\n" + 
                                "                           NAME = ?NAME,\n" + 
                                "                           DECIMAL_PLACE = ?DECIMAL_PLACE,\n" + 
                                "                           CONVERSION_OF = ?CONVERSION_OF,\n" + 
                                "                           FIRST_UNIT_ID = ?FIRST_UNIT_ID,\n" +
                                "                           UNITTYPE_ID = ?UNITTYPE_ID,\n" + 
                                "                           SECOND_UNIT_ID = ?SECOND_UNIT_ID\n" + 
                                " WHERE UNIT_ID = ?UNIT_ID";
                        break;                    
                    }
                case SQLCommand.AssetUnitOfMeasure.Delete:
                    {
                        query="DELETE FROM ASSET_UNITOFMEASURE WHERE UNIT_ID = ?UNIT_ID";

                        break;
                    }
                case SQLCommand.AssetUnitOfMeasure.Fetch:
                    {
                        query="SELECT UNIT_ID,\n" +
                                "       TYPE,\n" +
                                "       UNITTYPE_ID,\n" +
                                "       SYMBOL,\n" + 
                                "       NAME,\n" + 
                                "       DECIMAL_PLACE,\n" + 
                                "       CONVERSION_OF,\n" + 
                                "       FIRST_UNIT_ID,\n" + 
                                "       SECOND_UNIT_ID\n" + 
                                "  FROM ASSET_UNITOFMEASURE\n" + 
                                " WHERE UNIT_ID = ?UNIT_ID";

                        break;
                    }
                case SQLCommand.AssetUnitOfMeasure.FetchAll:
                    {
                        query = "SELECT UNIT_ID,\n" +
                                "       TYPE,\n" +
                                "       SYMBOL,\n" +
                                "       NAME,\n" +
                                "       DECIMAL_PLACE,\n" +
                                "       CONVERSION_OF,\n" +
                                "       FIRST_UNIT_ID,\n" +
                                "       SECOND_UNIT_ID\n" +
                                "  FROM ASSET_UNITOFMEASURE";
                        break;
                    }

                case SQLCommand.AssetUnitOfMeasure.FetchForFirstUnit:
                    {
                        query ="SELECT UNIT_ID, SYMBOL FROM ASSET_UNITOFMEASURE WHERE SYMBOL <> ''";


                        break;
                    }             
            }
            return query;
        }
    }
}

        #endregion