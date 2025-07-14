using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    class AssetTransferSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.AssetTransferVoucher).FullName)
            {
                query = GetLocationSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        
        private string GetLocationSQL()
        {
            string query = "";
            SQLCommand.AssetTransferVoucher sqlCommandId = (SQLCommand.AssetTransferVoucher)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.AssetTransferVoucher.MasterAdd:
                    {
                        query = "INSERT INTO ASSET_TRANSFER_MASTER\n" +
                        "   (ITEM_ID,\n" +
                        "   LOCATION_FROM_ID,\n" +
                        "   LOCATION_TO_ID,\n" +
                        "   TRANSFER_DATE,\n" +
                        "   REFRENCE_ID,\n" +
                        "   NARRATION)\n" +
                        "VALUES\n" +
                        "   (?ITEM_ID,\n" +
                        "   ?LOCATION_FROM_ID,\n" +
                        "   ?LOCATION_TO_ID,\n" +
                        "   ?TRANSFER_DATE,\n" +
                        "   ?REFRENCE_ID,\n" +
                        "   ?NARRATION)";
                        break;
                    }
                case SQLCommand.AssetTransferVoucher.DetailAdd:
                    {
                        query = "INSERT INTO ASSET_TRANSFER_DETAIL\n" +
                        "   (TRANSFER_ID,\n" +
                        "   ITEM_ID,\n" +
                        "   ASSET_ID)\n" +
                        "VALUES\n" +
                        "   (?TRANSFER_ID,\n" +
                        "   ?ITEM_ID,\n" +
                        "   ?ASSET_ID)";
                        break;
                    }

                case SQLCommand.AssetTransferVoucher.FetchAllDetail:
                    {
                        query = "SELECT AD.TRANSFER_ID,\n" +
                                "       AD.ITEM_ID,\n" +
                                "       AD.ASSET_ID,\n" +
                                "       ALF.LOCATION_NAME AS FROM_LOCATION,\n" +
                                "       ALT.LOCATION_NAME AS TO_LOCATION,\n" +
                                "       AI.ASSET_NAME\n" +
                                "  FROM ASSET_TRANSFER_DETAIL AD\n" +
                                " INNER JOIN ASSET_ITEM AI\n" +
                                "    ON AD.ITEM_ID = AI.ITEM_ID\n" +
                                " INNER JOIN ASSET_TRANSFER_MASTER AM\n" +
                                "    ON AD.TRANSFER_ID = AM.TRANSFER_ID\n" +
                                " INNER JOIN ASSET_LOCATION ALF\n"+
                                "    ON AM.LOCATION_FROM_ID = ALF.LOCATION_ID\n"+
                                " INNER JOIN ASSET_LOCATION ALT\n"+
                                "    ON AM.LOCATION_TO_ID = ALT.LOCATION_ID\n"+
                                " WHERE AM.TRANSFER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED";
                        break;
                    }

                case SQLCommand.AssetTransferVoucher.FetchAllMaster:
                    {
                        query = "SELECT TRANSFER_ID,\n" +
                                "       REFRENCE_ID,\n" + 
                                "       TRANSFER_DATE,\n" + 
                                "       NARRATION,\n" + 
                               // "       AG.GROUP_NAME,\n" + 
                                "       AM.ITEM_ID,\n" + 
                                "       AI.ASSET_NAME     AS ITEM_NAME,\n" + 
                                "       ALF.LOCATION_NAME AS FROM_LOCATION,\n" +
                                "       ALT.LOCATION_NAME AS TO_LOCATION\n" + 
                                "  FROM ASSET_TRANSFER_MASTER AM\n" + 
                                " INNER JOIN ASSET_ITEM AI\n" + 
                                "    ON AM.ITEM_ID = AI.ITEM_ID\n" + 
                                " INNER JOIN ASSET_LOCATION ALF\n" + 
                                "    ON AM.LOCATION_FROM_ID = ALF.LOCATION_ID\n" + 
                                " INNER JOIN ASSET_LOCATION ALT\n" + 
                                "    ON AM.LOCATION_TO_ID = ALT.LOCATION_ID\n" + 
                              //  " INNER JOIN ASSET_GROUP AG\n" +
                              //  "    ON AI.ASSET_GROUP_ID = AG.GROUP_ID\n" +
                                " WHERE AM.TRANSFER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED";
                        break;
                    }
            }
            return query;
        }
        #endregion
    }
}
