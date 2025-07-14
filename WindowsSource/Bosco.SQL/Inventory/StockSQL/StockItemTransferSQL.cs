using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class StockItemTransferSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.StockItemTransfer).FullName)
            {
                query = ManipulateStockItmeTransferSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        #region SQL Scripts
        public string ManipulateStockItmeTransferSQL()
        {
            string Query = "";
            SQLCommand.StockItemTransfer SqlcommandId = (SQLCommand.StockItemTransfer)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.StockItemTransfer.Add:
                    {
                        Query = "INSERT INTO STOCK_ITEM_TRANSFER(\n" +
                                "    TRANSFER_DATE,\n" +
                                "    PROJECT_ID,\n" +
                                "    ITEM_ID,\n" +
                                "    QUANTITY,\n" +
                                "    FROM_LOCATION_ID,\n" +
                                "    TO_LOCATION_ID,\n" +
                                "    EDIT_ID\n" +
                                "  )\n" +
                                "VALUES\n" +
                                "  (?TRANSFER_DATE,\n" +
                                "   ?PROJECT_ID,\n" +
                                "   ?ITEM_ID,\n" +
                                "   ?QUANTITY,\n" +
                                "   ?FROM_LOCATION_ID,\n" +
                                "   ?TO_LOCATION_ID,?EDIT_ID)";
                        break;
                    }
                case SQLCommand.StockItemTransfer.FetchByProjectId:
                    {
                        Query = "SELECT TRANSFER_ID,EDIT_ID,TRANSFER_DATE,\n" +
                                "       MP.PROJECT,\n" +
                                "       SI.NAME,\n" +
                                "       SIT.QUANTITY,\n" +
                                "       ASLFROM.LOCATION AS FROM_LOCATION,\n" +
                                "       ASLTO.LOCATION   AS TO_LOCATION\n" +
                                "  FROM stock_item_transfer SIT\n" +
                                "  LEFT JOIN MASTER_PROJECT MP\n" +
                                "    ON SIT.PROJECT_ID = MP.PROJECT_ID\n" +
                                "  LEFT JOIN STOCK_ITEM SI\n" +
                                "    ON SIT.ITEM_ID = SI.ITEM_ID\n" +
                                "  LEFT JOIN ASSET_LOCATION ASLFROM\n" +
                                "    ON SIT.FROM_LOCATION_ID = ASLFROM.LOCATION_ID\n" +
                                "  LEFT JOIN ASSET_LOCATION ASLTO\n" +
                                "    ON SIT.TO_LOCATION_ID = ASLTO.LOCATION_ID\n" +
                                " WHERE MP.DELETE_FLAG = 0\n" +
                                "   AND SIT.PROJECT_ID IN (?PROJECT_ID) AND TRANSFER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED ORDER BY EDIT_ID;";
                        break;
                    }
                case SQLCommand.StockItemTransfer.FetchByEditId:
                    {
                        //Query = "SELECT TRANSFER_ID,\n" +
                        //        "       PROJECT_ID,\n" +
                        //        "       TRANSFER_DATE,\n" +
                        //        "       ITEM_ID AS ITEM_NAME,\n" +
                        //        "       QUANTITY,\n" +
                        //        "       FROM_LOCATION_ID AS FROM_LOCATION,\n" +
                        //        "       TO_LOCATION_ID AS TO_LOCATION,\n" +
                        //        "       EDIT_ID\n" +
                        //        "  FROM STOCK_ITEM_TRANSFER\n" +
                        //        " WHERE EDIT_ID =?EDIT_ID;";

                        Query = "SELECT T.PROJECT_ID,T.TRANSFER_ID,\n" +
                        "       T.TRANSFER_DATE    AS TRANSFER_DATE,\n" +
                        "       T.ITEM_ID AS ITEM_NAME,\n" +
                        "       T.FROM_LOCATION_ID AS FROM_LOCATION,\n" +
                        "       T.TO_LOCATION_ID  AS TO_LOCATION,\n" +
                        "       T.QUANTITY,\n" +
                        "       ISV.QUANTITY       AS AVAIL_QUANTITY\n" +
                        "  FROM INVENTORY_STOCK ISV\n" +
                        "  JOIN (SELECT SIT.PROJECT_ID,SIT.TRANSFER_ID,\n" +
                        "               SIT.ITEM_ID,\n" +
                        "               SIT.TRANSFER_DATE,\n" +
                        "               SIT.FROM_LOCATION_ID,\n" +
                        "               SIT.TO_LOCATION_ID,\n" +
                        "               SIT.QUANTITY\n" +
                        "          FROM STOCK_ITEM AS SI\n" +
                        "         INNER JOIN STOCK_ITEM_TRANSFER AS SIT\n" +
                        "            ON SI.ITEM_ID = SIT.ITEM_ID\n" +
                        "         WHERE SIT.EDIT_ID = ?EDIT_ID) AS T\n" +
                        "    ON T.PROJECT_ID = ISV.PROJECT_ID\n" +
                        "   AND T.ITEM_ID = ISV.ITEM_ID\n" +
                        "   AND T.FROM_LOCATION_ID = ISV.LOCATION_ID\n" +
                        "   AND T.TRANSFER_DATE = ISV.BALANCE_DATE\n" +
                        " ORDER BY T.TRANSFER_DATE DESC";

                        break;
                    }
                case SQLCommand.StockItemTransfer.Update:
                    {
                        Query = "UPDATE STOCK_ITEM_TRANSFER\n" +
                                "   SET TRANSFER_DATE    = ?TRANSFER_DATE,\n" +
                                "       ITEM_ID          = ?ITEM_ID,\n" +
                                "       QUANTITY         = ?QUANTITY,\n" +
                                "       FROM_LOCATION_ID = ?FROM_LOCATION_ID,\n" +
                                "       TO_LOCATION_ID   = ?TO_LOCATION_ID\n" +
                                " WHERE TRANSFER_ID = ?TRANSFER_ID";
                        break;
                    }
                case SQLCommand.StockItemTransfer.Delete:
                    {
                        Query = "DELETE FROM STOCK_ITEM_TRANSFER WHERE TRANSFER_ID=?TRANSFER_ID;";
                        break;
                    }
                case SQLCommand.StockItemTransfer.GetNewEditId:
                    {
                        Query = "SELECT MAX(EDIT_ID) AS EDIT_ID FROM STOCK_ITEM_TRANSFER;";
                        break;
                    }
            }
            return Query;
        }

        #endregion
    }
}
