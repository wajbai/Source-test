using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.DAO;

namespace Bosco.SQL
{
    public class AssetAMCRenewalSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.AssetAMCRenewal).FullName)
            {
                query = GetAssetAMCRenewal();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        public string GetAssetAMCRenewal()
        {
            string query = "";
            SQLCommand.AssetAMCRenewal SqlcommandId = (SQLCommand.AssetAMCRenewal)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.AssetAMCRenewal.AddAMCRenewalMaster:
                    {
                        query = "INSERT INTO ASSET_AMC_RENEWAL_MASTER\n" +
                                    "  (AMC_GROUP, PROVIDER, PROJECT_ID)\n" +
                                    "VALUES\n" +
                                    "  (?AMC_GROUP, ?PROVIDER, ?PROJECT_ID)";
                        break;
                    }
                case SQLCommand.AssetAMCRenewal.AddAMCItemMapping:
                    {
                        query = "INSERT INTO ASSET_AMC_ITEM_MAPPING\n" +
                                  "(AMC_ID, ITEM_DETAIL_ID)\n" +
                               " VALUES\n" +
                                  "(?AMC_ID, ?ITEM_DETAIL_ID)";
                        break;
                    }
                case SQLCommand.AssetAMCRenewal.AddAMCRenewalHistory:
                    {
                        query = "INSERT INTO ASSET_AMC_RENEWAL_HISTORY\n" +
                                "  (AMC_ID,RENEWAL_DATE,AMC_FROM,AMC_TO,PREMIUM_AMOUNT,VOUCHER_ID)\n" +
                                "VALUES\n" +
                                "(?AMC_ID,?RENEWAL_DATE,?AMC_FROM,?AMC_TO,?PREMIUM_AMOUNT,?VOUCHER_ID)";
                        break;
                    }
                case SQLCommand.AssetAMCRenewal.EditAMCRenewalMaster:
                    {
                        query = "UPDATE ASSET_AMC_RENEWAL_MASTER\n" +
                         "   SET AMC_GROUP  =?AMC_GROUP,\n" +
                         "       PROVIDER   =?PROVIDER,\n" +
                         "       PROJECT_ID =?PROJECT_ID\n" +
                         " WHERE AMC_ID =?AMC_ID";
                        break;
                    }
                case SQLCommand.AssetAMCRenewal.EditAMCRenewalHistory:
                    {
                        query = "UPDATE ASSET_AMC_RENEWAL_HISTORY\n" +
                                    "   SET RENEWAL_DATE   =?RENEWAL_DATE,\n" +
                                    "       AMC_FROM       =?AMC_FROM,\n" +
                                    "       AMC_TO         =?AMC_TO,\n" +
                                    "       PREMIUM_AMOUNT =?PREMIUM_AMOUNT,\n" +
                                    "       VOUCHER_ID =?VOUCHER_ID\n" +
                                    " WHERE AMC_ID =?AMC_ID AND AMC_RENEWAL_ID =?AMC_RENEWAL_ID";
                        break;
                    }
                case SQLCommand.AssetAMCRenewal.DeleteAMCRenewMaster:
                    {
                        query = "DELETE FROM ASSET_AMC_RENEWAL_MASTER WHERE AMC_ID =?AMC_ID";
                        break;
                    }
                case SQLCommand.AssetAMCRenewal.DeleteAMCRenewlHistory:
                    {
                        query = "DELETE FROM ASSET_AMC_RENEWAL_HISTORY WHERE AMC_ID=?AMC_ID";
                        break;

                    }
                case SQLCommand.AssetAMCRenewal.DeleteAmcItemMapping:
                    {
                        query = "DELETE FROM ASSET_AMC_ITEM_MAPPING WHERE AMC_ID =?AMC_ID";
                        break;
                    }
                case SQLCommand.AssetAMCRenewal.Fetch:
                    {
                        query = "SELECT AMC_GROUP, PROVIDER, AARH.AMC_FROM, AARH.AMC_TO, AARH.PREMIUM_AMOUNT,AARH.RENEWAL_DATE,PROJECT_ID,AARH.VOUCHER_ID\n" +
                                "  FROM ASSET_AMC_RENEWAL_MASTER AARM\n" +
                                " INNER JOIN ASSET_AMC_RENEWAL_HISTORY AARH\n" +
                                "    ON AARH.AMC_ID = AARM.AMC_ID\n" +
                                " WHERE AARM.AMC_ID = ?AMC_ID {AND AARH.AMC_RENEWAL_ID =?AMC_RENEWAL_ID};";
                        break;
                    }
                case SQLCommand.AssetAMCRenewal.FetchAMCRenewalMasterDetails:
                    {

                        //query = "SELECT AARM.AMC_ID,\n" +
                        //"       AARM.AMC_GROUP,\n" +
                        //"       AARM.PROVIDER,\n" +
                        //"       AARM.PROJECT_ID,\n" +
                        //"       MP.PROJECT\n" +
                        //"  FROM ASSET_AMC_RENEWAL_MASTER AARM\n" +
                        //"  LEFT JOIN MASTER_PROJECT MP\n" +
                        //"    ON MP.PROJECT_ID = AARM.PROJECT_ID;";

                        query = "SELECT AARM.AMC_ID,\n" +
                        "       AARM.AMC_GROUP,\n" +
                        "       AL.LOCATION,\n" +
                        "       AC.ASSET_CLASS,\n" +
                        "       AARM.PROVIDER,\n" +
                        "       AARM.PROJECT_ID,\n" +
                        "       AARH.AMC_FROM,\n" +
                        "       AARH.AMC_TO,\n" +
                        "       AID.ASSET_ID,\n" +
                        "       AARH.PREMIUM_AMOUNT,\n" +
                        "       AARH.RENEWAL_DATE,\n" +
                        "       MP.PROJECT\n" +
                        "  FROM ASSET_AMC_RENEWAL_MASTER AARM\n" +
                        "  LEFT JOIN ASSET_AMC_ITEM_MAPPING AAIM\n" +
                        "    ON AAIM.AMC_ID = AARM.AMC_ID\n" +
                        "  LEFT JOIN ASSET_ITEM_DETAIL AID\n" +
                        "    ON AID.ITEM_DETAIL_ID = AAIM.ITEM_DETAIL_ID\n" +
                        "  LEFT JOIN ASSET_STOCK_MANUFACTURER ASM\n" +
                        "    ON ASM.MANUFACTURER_ID = AID.MANUFACTURER_ID\n" +
                        "  LEFT JOIN ASSET_ITEM AI\n" +
                        "    ON AI.ITEM_ID = AID.ITEM_ID\n" +
                        "  LEFT JOIN ASSET_CLASS AC\n" +
                        "    ON AC.ASSET_CLASS_ID = AI.ASSET_CLASS_ID\n" +
                        "  LEFT JOIN ASSET_LOCATION AL\n" +
                        "    ON AL.LOCATION_ID = AID.LOCATION_ID\n" +
                        "  LEFT JOIN ASSET_AMC_RENEWAL_HISTORY AARH\n" +
                        "    ON AARH.AMC_ID = AARM.AMC_ID\n" +
                        "  LEFT JOIN MASTER_PROJECT MP\n" +
                        "    ON MP.PROJECT_ID = AARM.PROJECT_ID\n" +
                        " WHERE AMC_RENEWAL_ID IN\n" +
                        "       (SELECT MAX(AMC_RENEWAL_ID) FROM ASSET_AMC_RENEWAL_HISTORY GROUP BY AMC_ID)AND AARM.PROJECT_ID=?PROJECT_ID \n" +
                        " GROUP BY AMC_ID;";


                        break;
                    }
                case SQLCommand.AssetAMCRenewal.FetchAMCRenewalMappedItems:
                    {
                        query = "SELECT AAIM.AMC_ID,\n" +
                                "       AI.ITEM_ID,\n" +
                                "       AC.ASSET_CLASS,\n" +
                                "PARENT.ASSET_CLASS AS PARENT_CLASS,\n" +
                                "       AI.ASSET_ITEM,\n" +
                                "       AID.ASSET_ID,\n" +
                                "       AAIM.ITEM_DETAIL_ID,\n" +
                                "       ASM.MANUFACTURER,AL.LOCATION\n" +
                                "  FROM ASSET_AMC_ITEM_MAPPING AAIM\n" +
                                "  LEFT JOIN ASSET_AMC_RENEWAL_MASTER AARM\n" +
                                "    ON AARM.AMC_ID = AAIM.AMC_ID\n" +
                                "  LEFT JOIN ASSET_ITEM_DETAIL AID\n" +
                                "    ON AID.ITEM_DETAIL_ID = AAIM.ITEM_DETAIL_ID\n" +
                                "  LEFT JOIN ASSET_LOCATION AL\n" +
                                "    ON AL.LOCATION_ID = AID.LOCATION_ID\n" +
                                "  LEFT JOIN ASSET_STOCK_MANUFACTURER ASM\n" +
                                "    ON ASM.MANUFACTURER_ID = AID.MANUFACTURER_ID\n" +
                                "  LEFT JOIN ASSET_ITEM AI\n" +
                                "    ON AI.ITEM_ID = AID.ITEM_ID\n" +
                                "  LEFT JOIN ASSET_CLASS AC\n" +
                                "    ON AI.ASSET_CLASS_ID = AC.ASSET_CLASS_ID\n" +
                                "  LEFT JOIN ASSET_CLASS PARENT\n" +
                                "    ON AC.PARENT_CLASS_ID = PARENT.ASSET_CLASS_ID\n" +
                                " WHERE AI.IS_AMC = 1\n" +
                                "   AND AAIM.AMC_ID IN (?AMC_ID) ORDER BY AC.ASSET_CLASS,AI.ASSET_ITEM ASC;";
                        break;
                    }
                case SQLCommand.AssetAMCRenewal.FetchItemDetails:
                    {
                        query = "SELECT ITEM_DETAIL_ID, ASSET_ID, ASSET_ITEM, MANUFACTURER, LOCATION\n" +
                            "  FROM ASSET_ITEM AI\n" +
                            " INNER JOIN ASSET_ITEM_DETAIL AID\n" +
                            "    ON AI.ITEM_ID = AID.ITEM_ID\n" +
                            "  LEFT JOIN ASSET_STOCK_MANUFACTURER ASM\n" +
                            "    ON ASM.MANUFACTURER_ID = AID.MANUFACTURER_ID\n" +
                            "  LEFT JOIN ASSET_LOCATION AL\n" +
                            "    ON AL.LOCATION_ID = AID.LOCATION_ID\n" +
                            " WHERE AI.IS_AMC = 1";
                        break;
                    }
                case SQLCommand.AssetAMCRenewal.FetchAMCRenewalHistoryByAmCId:
                    {
                        query = "SELECT AMC_ID,AMC_RENEWAL_ID,\n" +
                        "       RENEWAL_DATE,\n" +
                        "       AMC_FROM,\n" +
                        "       AMC_TO,\n" +
                        "       IF((DATEDIFF(AMC_TO,CURDATE()))<=0,0,DATEDIFF(AMC_TO,CURDATE())) AS 'AMC_DAYS',\n" +
                        "       PREMIUM_AMOUNT,\n" +
                        "       VOUCHER_ID,\n" +
                        "       BRANCH_ID\n" +
                        "  FROM ASSET_AMC_RENEWAL_HISTORY\n" +
                        " WHERE AMC_ID IN (?AMC_ID) ORDER BY AMC_RENEWAL_ID DESC;";

                        break;
                    }
                case SQLCommand.AssetAMCRenewal.FetchMappedItemAvailableList:
                    {
                        query = "SELECT\n" +
                        " FNL.AMC_ID AS AMC_ID,\n" +
                        " FNL.ITEM_DETAIL_ID,\n" +
                        " FNL.ASSET_ID,\n" +
                        " FNL.ASSET_ITEM,\n" +
                        " FNL.MANUFACTURER,\n" +
                        " FNL.LOCATION,FNL.ASSET_CLASS,\n" +
                        " FNL.SELECT_TEMP\n" +
                        " FROM (SELECT IFNULL(T.AMC_ID, 0) AS AMC_ID,\n" +
                        "       AID.ITEM_DETAIL_ID,\n" +
                        "       AId.ASSET_ID,\n" +
                        "       AI.ASSET_ITEM,\n" +
                        "       ASM.MANUFACTURER,\n" +
                        "       AL.LOCATION,AC.ASSET_CLASS,\n" +
                        "       CASE\n" +
                        "         WHEN T.ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID THEN\n" +
                        "          1\n" +
                        "         ELSE\n" +
                        "          0\n" +
                        "       END AS 'SELECT_TEMP'\n" +
                        "  FROM ASSET_ITEM_DETAIL AID\n" +
                        " INNER JOIN ASSET_ITEM AI\n" +
                        "    ON AI.ITEM_ID = AID.ITEM_ID\n" +
                        " INNER JOIN ASSET_CLASS AC\n" +
                        "    ON AI.ASSET_CLASS_ID = AC.ASSET_CLASS_ID\n" +
                        " INNER JOIN ASSET_AMC_RENEWAL_MASTER ARM\n" +
                        "ON AID.PROJECT_ID = ARM.PROJECT_ID\n" +
                        "  LEFT JOIN ASSET_STOCK_MANUFACTURER ASM\n" +
                        "    ON ASM.MANUFACTURER_ID = AID.MANUFACTURER_ID\n" +
                        "  LEFT JOIN ASSET_LOCATION AL\n" +
                        "    ON AL.LOCATION_ID = AID.LOCATION_ID\n" +
                        "  LEFT JOIN (SELECT ITEM_DETAIL_ID, AMC_ID\n" +
                        "               FROM ASSET_AMC_ITEM_MAPPING) AS T\n" +
                            //"              WHERE AMC_ID =?AMC_ID ) AS T\n" +
                        "    ON T.ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID\n" +
                        " WHERE AI.IS_AMC = 1 AND AID.PROJECT_ID = ?PROJECT_ID) AS FNL\n" +
                        " WHERE FNL.SELECT_TEMP <>1 GROUP BY FNL.ITEM_DETAIL_ID";
                        break;
                    }
                case SQLCommand.AssetAMCRenewal.FetchMappedItemSelectedList:
                    {
                        query = "SELECT FNL.AMC_ID AS AMC_ID,\n" +
                                "       FNL.ITEM_DETAIL_ID,\n" +
                                "       FNL.ASSET_ID,\n" +
                                "       FNL.ASSET_ITEM,\n" +
                                "       FNL.MANUFACTURER,\n" +
                                "       FNL.LOCATION,\n" +
                                "       FNL.STATUS\n" +
                                "  FROM (SELECT IFNULL(T.AMC_ID, 0) AS AMC_ID,\n" +
                                "               AID.ITEM_DETAIL_ID,\n" +
                                "               AID.ASSET_ID,\n" +
                                "               AI.ASSET_ITEM,\n" +
                                "               ASM.MANUFACTURER,\n" +
                                "               AL.LOCATION,\n" +
                                "               CASE\n" +
                                "                 WHEN T.ITEM_DETAIL_ID = TT.SOLD_ITEM_DETAIL_ID THEN\n" +
                                "                  0\n" +
                                "                 ELSE\n" +
                                "                  1\n" +
                                "               END AS 'STATUS'\n" +
                                "          FROM ASSET_ITEM_DETAIL AID\n" +
                                "         INNER JOIN ASSET_ITEM AI\n" +
                                "            ON AI.ITEM_ID = AID.ITEM_ID\n" +
                                "          LEFT JOIN ASSET_STOCK_MANUFACTURER ASM\n" +
                                "            ON ASM.MANUFACTURER_ID = AID.MANUFACTURER_ID\n" +
                                "          LEFT JOIN ASSET_LOCATION AL\n" +
                                "            ON AL.LOCATION_ID = AID.LOCATION_ID\n" +
                                "         INNER JOIN (SELECT ITEM_DETAIL_ID, AMC_ID\n" +
                                "                      FROM ASSET_AMC_ITEM_MAPPING\n" +
                                "                     WHERE AMC_ID = ?AMC_ID) AS T\n" +
                                "            ON T.ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID\n" +
                                "          LEFT JOIN (SELECT AIM.IN_OUT_ID,\n" +
                                "                           AID.IN_OUT_DETAIL_ID,\n" +
                                "                           AT.ITEM_DETAIL_ID AS SOLD_ITEM_DETAIL_ID\n" +
                                "                      FROM ASSET_IN_OUT_MASTER AIM\n" +
                                "                     INNER JOIN ASSET_IN_OUT_DETAIL AID\n" +
                                "                        ON AIM.IN_OUT_ID = AID.IN_OUT_ID\n" +
                                "                     INNER JOIN ASSET_TRANS AT\n" +
                                "                        ON AID.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID\n" +
                                "                     WHERE FLAG IN ('SL', 'DN', 'DS')) AS TT\n" +
                                "            ON TT.SOLD_ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID\n" +
                                "         WHERE AI.IS_AMC = 1) AS FNL\n" +
                                " GROUP BY FNL.ITEM_DETAIL_ID";

                        break;
                    }
                case SQLCommand.AssetAMCRenewal.DeleteAMCRenewalHistoryByAMCRenewalId:
                    {
                        query = "DELETE FROM ASSET_AMC_RENEWAL_HISTORY WHERE AMC_ID=?AMC_ID AND AMC_RENEWAL_ID=?AMC_RENEWAL_ID;";
                        break;
                    }
                case SQLCommand.AssetAMCRenewal.FetchMaximumRenewalIdByAMCId:
                    {
                        query = "SELECT MAX(AMC_RENEWAL_ID) FROM ASSET_AMC_RENEWAL_HISTORY WHERE AMC_ID=?AMC_ID";
                        break;
                    }
                case SQLCommand.AssetAMCRenewal.FetchUnmappedItems:
                    {
                        query = "SELECT AMC_ID,\n" +
                                "       TT.ITEM_DETAIL_ID,\n" +
                                "       TT.ASSET_ID,\n" +
                                "       TT.ASSET_ITEM,\n" +
                                "       TT.MANUFACTURER,\n" +
                                "       TT.ASSET_CLASS,\n" +
                                "       TT.LOCATION,\n" +
                                "       TT.STATUS\n" +
                                "  FROM (SELECT 0 AS AMC_ID,\n" +
                                "               AID.ITEM_DETAIL_ID,\n" +
                                "               AID.ASSET_ID,\n" +
                                "               AI.ASSET_ITEM,\n" +
                                "               ASM.MANUFACTURER,\n" +
                                "               AC.ASSET_CLASS,\n" +
                                "               AL.LOCATION,\n" +
                                "               CASE\n" +
                                "                 WHEN AID.ITEM_DETAIL_ID = T.SOLD_ITEM_DETAIL_ID THEN\n" +
                                "                  0\n" +
                                "                 ELSE\n" +
                                "                  1\n" +
                                "               END AS 'STATUS'\n" +
                                "          FROM ASSET_ITEM_DETAIL AID\n" +
                                "          LEFT JOIN ASSET_ITEM AI\n" +
                                "            ON AI.ITEM_ID = AID.ITEM_ID\n" +
                                "          LEFT JOIN ASSET_CLASS AC\n" +
                                "            ON AI.ASSET_CLASS_ID = AC.ASSET_CLASS_ID\n" +
                                "         LEFT JOIN ASSET_AMC_RENEWAL_MASTER ARM\n" +
                                "            ON AID.PROJECT_ID = ARM.PROJECT_ID\n" +
                                "          LEFT JOIN ASSET_STOCK_MANUFACTURER ASM\n" +
                                "            ON ASM.MANUFACTURER_ID = AID.MANUFACTURER_ID\n" +
                                "          LEFT JOIN ASSET_LOCATION AL\n" +
                                "            ON AL.LOCATION_ID = AID.LOCATION_ID\n" +
                                "          LEFT JOIN (SELECT AIM.IN_OUT_ID,\n" +
                                "                           AID.IN_OUT_DETAIL_ID,\n" +
                                "                           AT.ITEM_DETAIL_ID AS SOLD_ITEM_DETAIL_ID\n" +
                                "                      FROM ASSET_IN_OUT_MASTER AIM\n" +
                                "                     INNER JOIN ASSET_IN_OUT_DETAIL AID\n" +
                                "                        ON AIM.IN_OUT_ID = AID.IN_OUT_ID\n" +
                                "                     INNER JOIN ASSET_TRANS AT\n" +
                                "                        ON AID.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID\n" +
                                "                     WHERE FLAG IN ('SL', 'DN', 'DS')) AS T\n" +
                                "            ON T.SOLD_ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID\n" +
                                "         WHERE AI.IS_AMC = 1\n" +
                                "           AND AID.PROJECT_ID = ?PROJECT_ID\n" +
                                "           AND AID.ITEM_DETAIL_ID NOT IN\n" +
                                "               (SELECT ITEM_DETAIL_ID FROM ASSET_AMC_ITEM_MAPPING)\n" +
                                "         GROUP BY AID.ITEM_DETAIL_ID) AS TT\n" +
                                " WHERE TT.STATUS = 1";


                        break;
                    }
                case SQLCommand.AssetAMCRenewal.FetchLastRenewaldateByAMCId:
                    {
                        query = @"SELECT AMC_TO FROM ASSET_AMC_RENEWAL_HISTORY WHERE AMC_RENEWAL_ID=(SELECT MAX(AMC_RENEWAL_ID) FROM `asset_amc_renewal_history` WHERE AMC_ID=?AMC_ID);";
                        break;
                    }
                case SQLCommand.AssetAMCRenewal.FetchVocuherIdByAMCRenewalId:
                    {

                        query = "SELECT VOUCHER_ID\n" +
                        "  FROM ASSET_AMC_RENEWAL_HISTORY\n" +
                        " WHERE AMC_RENEWAL_ID IN (?AMC_RENEWAL_ID);";
                        break;
                    }
                case SQLCommand.AssetAMCRenewal.FetchledgerIdByVoucherID:
                    {

                        query = "SELECT LEDGER_ID, TRANS_MODE, VMT.VOUCHER_DATE\n" +
                        "  FROM VOUCHER_TRANS VT\n" +
                        "  LEFT JOIN VOUCHER_MASTER_TRANS VMT\n" +
                        "    ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                        " WHERE VT.VOUCHER_ID = ?VOUCHER_ID;";

                        break;
                    }
                case SQLCommand.AssetAMCRenewal.FetchRenewalHistoryCountByAMCId:
                    {
                        query = "SELECT COUNT(*) FROM ASSET_AMC_RENEWAL_HISTORY WHERE AMC_ID = ?AMC_ID;";
                        break;
                    }
                case SQLCommand.AssetAMCRenewal.FetchVoucherIdByAMCId:
                    {
                        query = "SELECT VOUCHER_ID FROM ASSET_AMC_RENEWAL_HISTORY WHERE AMC_ID=?AMC_ID;";
                        break;
                    }

                case SQLCommand.AssetAMCRenewal.FetchPreviousRenewal:
                    {
                        query = "SELECT *\n" +
                                "  FROM ASSET_AMC_RENEWAL_HISTORY\n" +
                                " WHERE AMC_RENEWAL_ID = (SELECT MAX(AMC_RENEWAL_ID)\n" +
                                "                   FROM ASSET_AMC_RENEWAL_HISTORY\n" +
                                "                  WHERE AMC_RENEWAL_ID < ?AMC_RENEWAL_ID\n" +
                        "                    AND AMC_ID = ?AMC_ID)";
                        break;
                    }

                case SQLCommand.AssetAMCRenewal.FetchNextRenewal:
                    {
                        query = "SELECT *\n" +
                                "  FROM ASSET_AMC_RENEWAL_HISTORY\n" +
                                " WHERE AMC_RENEWAL_ID = (SELECT MAX(AMC_RENEWAL_ID)\n" +
                                "                   FROM ASSET_AMC_RENEWAL_HISTORY\n" +
                                "                  WHERE AMC_RENEWAL_ID > ?AMC_RENEWAL_ID\n" +
                        "                    AND AMC_ID = ?AMC_ID)";
                        break;
                    }
            }
            return query;
        }
        #endregion
    }
}
