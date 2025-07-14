using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Schema;
using Bosco.DAO.Data;

namespace Bosco.SQL
{
    public class AssetInwardOutwardSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.AssetInOut).FullName)
            {
                query = GetAssetInOutSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        #region SQL Script
        private string GetAssetInOutSQL()
        {
            string query = "";
            SQLCommand.AssetInOut sqlCommandId = (SQLCommand.AssetInOut)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {
                case SQLCommand.AssetInOut.FetchAssetInOutMasterAll:
                    {
                        query = "SELECT IN_OUT_ID,\n" +
                                "       IN_OUT_DATE,\n" +
                                "       BILL_INVOICE_NO,\n" +
                                "       VENDOR_ID,\n" +
                                "       SOLD_TO,\n" +
                                "       PROJECT_ID,\n" +
                                "       VOUCHER_ID,\n" +
                                "       TOT_AMOUNT,\n" +
                                "       FLAG,\n" +
                                "       BRANCH_ID\n" +
                                "  FROM ASSET_IN_OUT_MASTER;";
                        break;
                    }

                case SQLCommand.AssetInOut.FetchAssetInOutMasterByFlag:
                    {
                        query = "SELECT AM.IN_OUT_ID,\n" +
                                "       IN_OUT_DATE,\n" +
                                "       BILL_INVOICE_NO,\n" +
                                "       ASV.VENDOR_ID,\n" +
                                "       SOLD_TO,\n" +
                                "       CASE WHEN FLAG='DN' THEN 'Donate'\n" +
                                "             WHEN FLAG='PU' THEN 'Purchase'\n" +
                                "             WHEN FLAG='DS' THEN 'Dispose'\n" +
                                "             WHEN FLAG='SL' THEN 'Sales'\n" +
                                "             WHEN FLAG='IK' THEN 'In-Kind' END AS VOUCHER_TYPE,\n" +
                                "       T.QUANTITY,\n" +
                                "       ASV.VENDOR,\n" +
                                "       AM.PROJECT_ID,\n" +
                                "       AM.VOUCHER_ID,\n" +
                                "       VOUCHER_NO,\n" +
                                "       TOT_AMOUNT,\n" +
                                "       FLAG,\n" +
                                "       ASV.BRANCH_ID\n" +
                                "  FROM ASSET_IN_OUT_MASTER AM\n" +
                                "  LEFT JOIN ASSET_STOCK_VENDOR ASV\n" +
                                "    ON AM.VENDOR_ID = ASV.VENDOR_ID\n" +
                                "  JOIN (SELECT SUM(QUANTITY) AS QUANTITY, IN_OUT_ID\n" +
                                "          FROM ASSET_IN_OUT_DETAIL\n" +
                                "         GROUP BY IN_OUT_ID) AS T\n" +
                                "    ON AM.IN_OUT_ID=T.IN_OUT_ID\n" +
                                "  LEFT JOIN VOUCHER_MASTER_TRANS VMT\n" +
                                "    ON AM.VOUCHER_ID=VMT.VOUCHER_ID\n" +
                                " WHERE AM.PROJECT_ID = ?PROJECT_ID\n" +
                                "   AND IN_OUT_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                "   AND FIND_IN_SET(FLAG, ?FLAG);";
                        break;
                    }

                case SQLCommand.AssetInOut.AutoFetchSoldTo:
                    {
                        query = "SELECT SOLD_TO FROM ASSET_IN_OUT_MASTER WHERE FIND_IN_SET (FLAG,'SL,DN,DS');";
                        break;
                    }

                case SQLCommand.AssetInOut.FetchAssetInOutDetailByFlag:
                    {
                        query = "SELECT AID.IN_OUT_ID,IF(PARENT.ASSET_CLASS='Primary', AC.ASSET_CLASS,PARENT.ASSET_CLASS)  AS PARENT_CLASS, AC.ASSET_CLASS,AI.ITEM_ID,AID.IN_OUT_DETAIL_ID, ASSET_ITEM, QUANTITY, U.NAME, AMOUNT,RETENTION_YRS,DEPRECIATION_YRS\n" +
                                "  FROM ASSET_IN_OUT_DETAIL AID\n" +
                                " INNER JOIN ASSET_ITEM AI\n" +
                                "    ON AID.ITEM_ID = AI.ITEM_ID\n" +
                                " INNER JOIN ASSET_CLASS AC\n" +
                                "    ON AC.ASSET_CLASS_ID = AI.ASSET_CLASS_ID\n" +
                                " INNER JOIN UOM U\n" +
                                "    ON U.UOM_ID = AI.UOM_ID\n" +
                                " INNER JOIN ASSET_IN_OUT_MASTER AIM\n" +
                                "    ON AID.IN_OUT_ID = AIM.IN_OUT_ID\n" +
                                " LEFT JOIN ASSET_CLASS PARENT\n" +
                                " ON AC.PARENT_CLASS_ID = PARENT.ASSET_CLASS_ID\n" +
                                " WHERE PROJECT_ID = ?PROJECT_ID\n" +
                                "   AND IN_OUT_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                "   AND FIND_IN_SET(FLAG, ?FLAG) ORDER BY AC.ASSET_CLASS,ASSET_ITEM ASC";
                        break;
                    }

                case SQLCommand.AssetInOut.FetchAssetIDDetailByFlag:
                    {
                        query = "SELECT AID.ITEM_ID, ASSET_ID, AID.STATUS, AID.AMOUNT, AID.LOCATION_ID,LOCATION,AT.IN_OUT_DETAIL_ID\n" +
                                    "  FROM ASSET_ITEM_DETAIL AID\n" +
                                    " INNER JOIN ASSET_TRANS AT\n" +
                                    "    ON AID.ITEM_DETAIL_ID = AT.ITEM_DETAIL_ID\n" +
                                    " INNER JOIN ASSET_IN_OUT_DETAIL IOU\n" +
                                    "    ON AT.IN_OUT_DETAIL_ID = IOU.IN_OUT_DETAIL_ID\n" +
                                    " INNER JOIN ASSET_IN_OUT_MASTER AIM\n" +
                                    "    ON IOU.IN_OUT_ID = AIM.IN_OUT_ID\n" +
                                    " INNER JOIN ASSET_LOCATION AL\n" +
                                    "    ON AID.LOCATION_ID=AL.LOCATION_ID\n" +
                                    " WHERE AID.STATUS = ?STATUS\n" +
                                    " AND AIM.PROJECT_ID = ?PROJECT_ID\n" +
                                    " AND IN_OUT_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                    " AND AIM.FLAG IN (?FLAG)";
                        break;
                    }

                case SQLCommand.AssetInOut.FetchAssetListItem:
                    {
                        query = "SELECT ASSET_ID,\n" +
                                "       AID.ITEM_DETAIL_ID,\n" +
                                "       AID.DEPRECIATION_AMOUNT,\n" +
                                "      (AID.AMOUNT + AID.DEPRECIATION_AMOUNT) AS AMOUNT,\n" +
                                "      (AID.AMOUNT) AS BALANCE,\n" +
                                "       AID.ITEM_ID,\n" +
                                "       MANUFACTURER_ID AS ID,\n" +
                                "       LOCATION_ID,\n" +
                                "       CUSTODIAN_ID,\n" +
                                "       CASE\n" +
                                "         WHEN AID.ITEM_DETAIL_ID = T.SOLD_ITEM_DETAIL_ID THEN\n" +
                                "          0\n" +
                                "         ELSE\n" +
                                "          1\n" +
                                "       END AS STATUS,0 AS GAIN_AMOUNT,0 AS LOSS_AMOUNT,AID.SALVAGE_VALUE\n" +
                                "  FROM ASSET_IN_OUT_DETAIL IOD\n" +
                                "  LEFT JOIN ASSET_TRANS AT\n" +
                                "    ON IOD.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID\n" +
                                "  LEFT JOIN ASSET_ITEM_DETAIL AID\n" +
                                "    ON AT.ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID\n" +
                                " LEFT JOIN (SELECT AIM.IN_OUT_ID,\n" +
                                "               AID.IN_OUT_DETAIL_ID,\n" +
                                "               AT.ITEM_DETAIL_ID AS SOLD_ITEM_DETAIL_ID\n" +
                                "          FROM ASSET_IN_OUT_MASTER AIM\n" +
                                "                 INNER JOIN ASSET_IN_OUT_DETAIL AID\n" +
                                "            ON AIM.IN_OUT_ID = AID.IN_OUT_ID\n" +
                                "                 INNER JOIN ASSET_TRANS AT\n" +
                                "            ON AID.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID\n" +
                                "         WHERE FLAG IN ('SL', 'DN', 'DS')) AS T\n" +
                                "    ON T.SOLD_ITEM_DETAIL_ID=AID.ITEM_DETAIL_ID\n" +
                                " WHERE IOD.IN_OUT_DETAIL_ID = ?IN_OUT_DETAIL_ID;";
                        break;
                    }
                case SQLCommand.AssetInOut.FetchAssetListItemByItemId:
                    {
                        // Commented on 17-03-2016 for depreciation value

                        //query = "SELECT TT.ITEM_DETAIL_ID,\n" +
                        //        "       TT.ITEM_ID,\n" +
                        //        "       TT.ASSET_ITEM,\n" +
                        //        "       TT.ASSET_ID,\n" +
                        //        "      IF(TT.GAIN_AMOUNT=0 AND LOSS_AMOUNT =0 ,TT.AMOUNT , IF(TT.GAIN_AMOUNT > 0, TT.AMOUNT + TT.GAIN_AMOUNT, TT.AMOUNT - TT.LOSS_AMOUNT)) AS AMOUNT,TT.AMOUNT AS TEMP_AMOUNT,\n" +
                        //        "       TT.PROJECT_ID,\n" +
                        //        "       TT.ID,\n" +
                        //        "       TT.LOCATION_ID,\n" +
                        //        "       TT.CUSTODIAN_ID,\n" +
                        //        "       TT.STATUS,TT.ASSET_AMOUNT,TT.GAIN_AMOUNT,TT.LOSS_AMOUNT,TT.SALVAGE_VALUE\n" +
                        //        "  FROM (SELECT AID.ITEM_DETAIL_ID,\n" +
                        //        "               AID.ITEM_ID,\n" +
                        //        "               AI.ASSET_ITEM,\n" +
                        //        "               AID.ASSET_ID,\n" +
                        //        "              IF(IFNULL(DS.BALANCE_AMOUNT,0) = 0, AID.AMOUNT,DS.BALANCE_AMOUNT) AS AMOUNT,\n" +
                        //        "               AID.PROJECT_ID,\n" +
                        //        "               AID.MANUFACTURER_ID AS ID,\n" +
                        //        "               AID.LOCATION_ID,\n" +
                        //        "               AID.CUSTODIAN_ID,AID.SALVAGE_VALUE,\n" +
                        //        "               CASE\n" +
                        //        "                 WHEN AID.ITEM_DETAIL_ID = T.SOLD_ITEM_DETAIL_ID THEN\n" +
                        //        "                  0\n" +
                        //        "                 ELSE\n" +
                        //        "                  1\n" +
                        //        "               END AS STATUS,IFNULL(T.AMOUNT,0) AS ASSET_AMOUNT,IFNULL(T.GAIN_AMOUNT,0) AS GAIN_AMOUNT,IFNULL(T.LOSS_AMOUNT,0) AS LOSS_AMOUNT\n" +
                        //        "          FROM ASSET_ITEM_DETAIL AID\n" +
                        //        "          LEFT JOIN ASSET_ITEM AI\n" +
                        //        "            ON AI.ITEM_ID = AID.ITEM_ID\n" +
                        //        "\n" +
                        //        "LEFT JOIN (SELECT APD.BALANCE_AMOUNT, APD.ITEM_DETAIL_ID\n" +
                        //        "                      FROM ASSET_DEPRECIATION_MASTER ADM\n" +
                        //        "\n" +
                        //        "                     INNER JOIN ASSET_DEPRECIATION_DETAIL APD\n" +
                        //        "                        ON ADM.DEPRECIATION_ID = APD.DEPRECIATION_ID\n" +
                        //        "\n" +
                        //        "                     INNER JOIN (SELECT DEPRECIATION_ID\n" +
                        //        "                                  FROM ASSET_DEPRECIATION_MASTER ADM\n" +
                        //        "                                 WHERE ?IN_OUT_DATE BETWEEN\n" +
                        //        "                                       DEPRECIATION_PERIOD_FROM AND\n" +
                        //        "                                       DEPRECIATION_PERIOD_TO) AS T\n" +
                        //        "                        ON T.DEPRECIATION_ID = ADM.DEPRECIATION_ID\n" +
                        //        "\n" +
                        //        "                     GROUP BY ITEM_DETAIL_ID) AS DS\n" +
                        //        "            ON AID.ITEM_DETAIL_ID = DS.ITEM_DETAIL_ID\n" +
                        //        "          LEFT JOIN (SELECT AIM.IN_OUT_ID,\n" +
                        //        "                           AID.IN_OUT_DETAIL_ID,\n" +
                        //        "                           AT.ITEM_DETAIL_ID AS SOLD_ITEM_DETAIL_ID,AT.AMOUNT,AT.GAIN_AMOUNT,AT.LOSS_AMOUNT\n" +
                        //        "                      FROM ASSET_IN_OUT_MASTER AIM\n" +
                        //        "                     INNER JOIN ASSET_IN_OUT_DETAIL AID\n" +
                        //        "                        ON AIM.IN_OUT_ID = AID.IN_OUT_ID\n" +
                        //        "                     INNER JOIN ASSET_TRANS AT\n" +
                        //        "                        ON AID.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID\n" +
                        //        "                     WHERE FLAG IN ('SL', 'DN', 'DS')) AS T\n" +
                        //        "            ON T.SOLD_ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID\n" +
                        //        "         WHERE AID.ITEM_ID = ?IN_OUT_DETAIL_ID\n" +
                        //        "           AND AID.PROJECT_ID = ?PROJECT_ID AND T.IN_OUT_DETAIL_ID=?STATUS OR T.IN_OUT_DETAIL_ID IS NULL) AS TT\n" +
                        //        " WHERE TT.ITEM_ID=?IN_OUT_DETAIL_ID AND TT.PROJECT_ID = ?PROJECT_ID AND CASE\n" +
                        //        "   WHEN ?STATUS = 0 THEN\n" +
                        //        "    TT.STATUS = 1\n" +
                        //        "   ELSE\n" +
                        //        "    TRUE\n" +
                        //        " END ORDER BY ITEM_DETAIL_ID;";

                        query = "SELECT TT.ITEM_DETAIL_ID,\n" +
                               "       TT.ITEM_ID,\n" +
                               "       TT.ASSET_ITEM,\n" +
                               "       TT.ASSET_ID,\n" +
                               "      IF(TT.GAIN_AMOUNT=0 AND LOSS_AMOUNT =0 ,TT.AMOUNT , IF(TT.GAIN_AMOUNT > 0, TT.AMOUNT + TT.GAIN_AMOUNT, TT.AMOUNT - TT.LOSS_AMOUNT)) AS AMOUNT,TT.AMOUNT AS TEMP_AMOUNT,\n" +
                               "       TT.PROJECT_ID,\n" +
                               "       TT.ID,\n" +
                               "       TT.LOCATION_ID,\n" +
                               "       TT.CUSTODIAN_ID,\n" +
                               "       TT.STATUS,TT.ASSET_AMOUNT,TT.GAIN_AMOUNT,TT.LOSS_AMOUNT,TT.SALVAGE_VALUE\n" +
                               "  FROM (SELECT AID.ITEM_DETAIL_ID,\n" +
                               "               AID.ITEM_ID,\n" +
                               "               AI.ASSET_ITEM,\n" +
                               "               AID.ASSET_ID,\n" +
                               "              IF(IFNULL(DS.BALANCE_AMOUNT,0) = 0, AID.AMOUNT,DS.BALANCE_AMOUNT) AS AMOUNT,\n" +
                               "               AID.PROJECT_ID,\n" +
                               "               AID.MANUFACTURER_ID AS ID,\n" +
                               "               AID.LOCATION_ID,\n" +
                               "               AID.CUSTODIAN_ID,AID.SALVAGE_VALUE,\n" +
                               "               CASE\n" +
                               "                 WHEN AID.ITEM_DETAIL_ID = T.SOLD_ITEM_DETAIL_ID THEN\n" +
                               "                  0\n" +
                               "                 ELSE\n" +
                               "                  1\n" +
                               "               END AS STATUS,IFNULL(T.AMOUNT,0) AS ASSET_AMOUNT,IFNULL(T.GAIN_AMOUNT,0) AS GAIN_AMOUNT,IFNULL(T.LOSS_AMOUNT,0) AS LOSS_AMOUNT\n" +
                               "          FROM ASSET_ITEM_DETAIL AID\n" +
                               "          LEFT JOIN ASSET_ITEM AI\n" +
                               "            ON AI.ITEM_ID = AID.ITEM_ID\n" +
                               "\n" +
                                "          LEFT JOIN (\n" +
                                "                    SELECT APD.BALANCE_AMOUNT, APD.ITEM_DETAIL_ID\n" +
                                "                      FROM ASSET_DEPRECIATION_MASTER ADM\n" +
                                "                     INNER JOIN ASSET_DEPRECIATION_DETAIL APD\n" +
                                "                        ON ADM.DEPRECIATION_ID = APD.DEPRECIATION_ID\n" +
                                "                     WHERE ADM.DEPRECIATION_ID IN\n" +
                                "                           (SELECT *\n" +
                                "                              FROM (SELECT *\n" +
                                "                                      FROM (SELECT *\n" +
                                "                                              FROM (SELECT DF\n" +
                                "                                                      FROM (SELECT ADM.DEPRECIATION_ID AS PRVID,\n" +
                                "                                                                   IF(?IN_OUT_DATE BETWEEN\n" +
                                "                                                                      DEPRECIATION_PERIOD_FROM AND\n" +
                                "                                                                      DEPRECIATION_PERIOD_TO,\n" +
                                "                                                                      ADM.DEPRECIATION_ID,\n" +
                                "                                                                      0) AS DF\n" +
                                "                                                              FROM ASSET_DEPRECIATION_MASTER ADM) AS T1) AS T2\n" +
                                "                                            UNION ALL\n" +
                                "                                            SELECT DEPRECIATION_ID AS DF\n" +
                                "                                              FROM ASSET_DEPRECIATION_MASTER ADM\n" +
                                "                                              JOIN (SELECT MAX(DEPRECIATION_PERIOD_FROM) AS PERIOD_FROM,\n" +
                                "                                                           MAX(DEPRECIATION_PERIOD_TO) AS PERIOD_TO\n" +
                                "                                                      FROM ASSET_DEPRECIATION_MASTER) AS D1\n" +
                                "                                             WHERE ADM.DEPRECIATION_PERIOD_FROM =\n" +
                                "                                                   PERIOD_FROM\n" +
                                "                                               AND DEPRECIATION_PERIOD_TO =\n" +
                                "                                                   PERIOD_TO) AS DCV\n" +
                                "                                     WHERE DF > 0 LIMIT 1) AS D)\n" +
                                "                    ) AS DS\n" +
                                "            ON AID.ITEM_DETAIL_ID = DS.ITEM_DETAIL_ID\n" +
                               "          LEFT JOIN (SELECT AIM.IN_OUT_ID,\n" +
                               "                           AID.IN_OUT_DETAIL_ID,\n" +
                               "                           AT.ITEM_DETAIL_ID AS SOLD_ITEM_DETAIL_ID,AT.AMOUNT,AT.GAIN_AMOUNT,AT.LOSS_AMOUNT\n" +
                               "                      FROM ASSET_IN_OUT_MASTER AIM\n" +
                               "                     INNER JOIN ASSET_IN_OUT_DETAIL AID\n" +
                               "                        ON AIM.IN_OUT_ID = AID.IN_OUT_ID\n" +
                               "                     INNER JOIN ASSET_TRANS AT\n" +
                               "                        ON AID.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID\n" +
                               "                     WHERE FLAG IN ('SL', 'DN', 'DS')) AS T\n" +
                               "            ON T.SOLD_ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID\n" +
                               "         WHERE AID.ITEM_ID = ?IN_OUT_DETAIL_ID\n" +
                               "           AND AID.PROJECT_ID = ?PROJECT_ID AND T.IN_OUT_DETAIL_ID=?STATUS OR T.IN_OUT_DETAIL_ID IS NULL) AS TT\n" +
                               " WHERE TT.ITEM_ID=?IN_OUT_DETAIL_ID AND TT.PROJECT_ID = ?PROJECT_ID AND CASE\n" +
                               "   WHEN ?STATUS = 0 THEN\n" +
                               "    TT.STATUS = 1\n" +
                               "   ELSE\n" +
                               "    TRUE\n" +
                               " END ORDER BY ITEM_DETAIL_ID;";

                        break;
                    }

                case SQLCommand.AssetInOut.FetchAssetInOutDetailIdByInOutId:
                    {
                        query = "SELECT TRIM(GROUP_CONCAT(IN_OUT_DETAIL_ID ORDER BY IN_OUT_DETAIL_ID DESC\n" +
                                "                         SEPARATOR ',')) AS IN_OUT_DETAIL_ID\n" +
                                "  FROM ASSET_IN_OUT_DETAIL\n" +
                                " WHERE IN_OUT_ID = ?IN_OUT_ID";
                        break;
                    }

                case SQLCommand.AssetInOut.FetchAssetItemDetailIdByInOutDetailId:
                    {
                        query = "SELECT TRIM(GROUP_CONCAT(ITEM_DETAIL_ID ORDER BY IN_OUT_DETAIL_ID DESC\n" +
                                "                         SEPARATOR ',')) AS ITEM_DETAIL_ID\n" +
                                "  FROM ASSET_TRANS\n" +
                                " WHERE IN_OUT_DETAIL_ID IN(?IN_OUT_DETAILS_IDs);";
                        break;
                    }
                case SQLCommand.AssetInOut.CheckInsuranceByItemId:
                    {
                        query = "SELECT COUNT(ASSET_ID) AS COUNT, ASSET_ID, AI.ITEM_ID\n" +
                                "  FROM ASSET_ITEM_DETAIL AID\n" +
                                " INNER JOIN ASSET_INSURANCE_DETAIL AIDD\n" +
                                "    ON AID.ITEM_DETAIL_ID = AIDD.ITEM_DETAIL_ID\n" +
                                " INNER JOIN ASSET_IN_OUT_DETAIL AIO\n" +
                                "    ON AID.ITEM_ID = AIO.ITEM_ID\n" +
                                " INNER JOIN ASSET_IN_OUT_MASTER AIM\n" +
                                "    ON AIO.IN_OUT_ID = AIM.IN_OUT_ID\n" +
                                " INNER JOIN ASSET_ITEM AI\n" +
                                "    ON AI.ITEM_ID = AID.ITEM_ID\n" +
                                "   AND AI.ITEM_ID = AIO.ITEM_ID\n" +
                                " WHERE AI.ITEM_ID IN (?ITEM_ID) AND AID.PROJECT_ID IN (?PROJECT_ID)\n" +
                                "  { AND AIM.IN_OUT_ID IN (?IN_OUT_ID)}\n" +
                                "  { AND AIO.IN_OUT_DETAIL_ID IN (?IN_OUT_DETAIL_ID)};";
                        break;
                    }
                case SQLCommand.AssetInOut.CheckSoldAssetIdByItemID:
                    {
                        //query = "SELECT COUNT(ASSET_ID) AS COUNT,\n" +
                        //        "       IM.IN_OUT_ID,\n" +
                        //        "       FLAG,\n" +
                        //        "       AIOD.ITEM_ID,\n" +
                        //        "       AID.ASSET_ID\n" +
                        //        "  FROM ASSET_IN_OUT_MASTER IM\n" +
                        //        " INNER JOIN ASSET_IN_OUT_DETAIL AIOD\n" +
                        //        "    ON IM.IN_OUT_ID = AIOD.IN_OUT_ID\n" +
                        //        " INNER JOIN ASSET_TRANS AT\n" +
                        //        "    ON AIOD.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID\n" +
                        //        " INNER JOIN ASSET_ITEM_DETAIL AID\n" +
                        //        "    ON AT.ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID\n" +
                        //        " WHERE AIOD.ITEM_ID IN (?ITEM_ID)\n" +
                        //        "   AND IM.IN_OUT_ID IN (?IN_OUT_ID)\n" +
                        //        "   AND AIOD.IN_OUT_DETAIL_ID IN (?IN_OUT_DETAIL_ID)\n" +
                        //        "   AND FIND_IN_SET(FLAG, 'SL,DS,DN');";

                        query = "SELECT COUNT(AID.ITEM_DETAIL_ID) AS COUNT, AID.ITEM_DETAIL_ID\n" +
                                "  FROM ASSET_ITEM_DETAIL AID\n" +
                                " INNER JOIN ASSET_ITEM AI\n" +
                                "    ON AID.ITEM_ID = AI.ITEM_ID\n" +
                                " INNER JOIN ASSET_IN_OUT_DETAIL IOD\n" +
                                "    ON AID.ITEM_ID = IOD.ITEM_ID\n" +
                                " INNER JOIN ASSET_TRANS AT\n" +
                                "    ON AT.IN_OUT_DETAIL_ID = IOD.IN_OUT_DETAIL_ID\n" +
                                "   AND AID.ITEM_DETAIL_ID = AT.ITEM_DETAIL_ID\n" +
                                " INNER JOIN ASSET_IN_OUT_MASTER AIM\n" +
                                "    ON IOD.IN_OUT_ID = AIM.IN_OUT_ID\n" +
                            //" WHERE AIM.IN_OUT_ID IN (?IN_OUT_ID)\n" + 
                            //"   AND AID.ITEM_ID IN (?ITEM_ID)\n" + 
                                " WHERE AID.ITEM_ID IN (?ITEM_ID) { AND AIM.IN_OUT_ID IN (?IN_OUT_ID)} { AND IOD.IN_OUT_DETAIL_ID IN (?IN_OUT_DETAIL_ID)}\n" +
                                "   AND AID.ITEM_DETAIL_ID IN\n" +
                                "       (SELECT ID.ITEM_DETAIL_ID\n" +
                                "          FROM ASSET_IN_OUT_MASTER AIM\n" +
                                "         INNER JOIN ASSET_IN_OUT_DETAIL AID\n" +
                                "            ON AIM.IN_OUT_ID = AID.IN_OUT_ID\n" +
                                "         INNER JOIN ASSET_TRANS AT\n" +
                                "            ON AID.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID\n" +
                                "         INNER JOIN ASSET_ITEM_DETAIL ID\n" +
                                "            ON AID.ITEM_ID = AID.ITEM_ID\n" +
                                "           AND AT.ITEM_DETAIL_ID = ID.ITEM_DETAIL_ID\n" +
                                "           AND FIND_IN_SET(FLAG, 'SL,DS,DN'));";
                        break;
                    }

                case SQLCommand.AssetInOut.CheckSoldAssetIdByItemDetailId: // Checking any sales entry made before deleting purchase entry.
                    {
                        query = "SELECT COUNT(AIM.IN_OUT_ID) AS COUNT,\n" +
                                "       AIM.IN_OUT_ID,\n" +
                                "       AID.ITEM_ID,\n" +
                                "       AID.ITEM_DETAIL_ID,\n" +
                                "       ASSET_ID,\n" +
                                "       FLAG\n" +
                                "  FROM ASSET_ITEM_DETAIL AID\n" +
                                " INNER JOIN ASSET_TRANS AT\n" +
                                "    ON AID.ITEM_DETAIL_ID = AT.ITEM_DETAIL_ID\n" +
                                " INNER JOIN ASSET_IN_OUT_DETAIL IOD\n" +
                                "    ON AT.IN_OUT_DETAIL_ID = IOD.IN_OUT_DETAIL_ID\n" +
                                " INNER JOIN ASSET_IN_OUT_MASTER AIM\n" +
                                "    ON AIM.IN_OUT_ID = IOD.IN_OUT_ID\n" +
                                " WHERE AID.ITEM_DETAIL_ID IN (?ITEM_DETAIL_IDs)\n" +
                                "   AND FLAG IN ('DN', 'DS', 'SL');";
                        break;
                    }

                case SQLCommand.AssetInOut.FetchItemDetailIdByInoutDetailId:
                    {
                        query = "SELECT GROUP_CONCAT(ITEM_DETAIL_ID) AS ITEM_DETAIL_ID FROM ASSET_TRANS WHERE IN_OUT_DETAIL_ID IN (?IN_OUT_DETAIL_ID)";
                        break;
                    }
                case SQLCommand.AssetInOut.DeleteInsuranceDetailByItemDetailId:
                    {
                        query = "DELETE FROM ASSET_INSURANCE_DETAIL WHERE ITEM_DETAIL_ID IN (?ITEM_DETAIL_IDs)";
                        break;
                    }

                case SQLCommand.AssetInOut.FetchAssetInOutMasterById:
                    {
                        query = "SELECT IN_OUT_ID,\n" +
                                "       IN_OUT_DATE,\n" +
                                "       BILL_INVOICE_NO,\n" +
                                "       VENDOR_ID,\n" +
                                "       DONOR_ID,\n" +
                                "       SOLD_TO,\n" +
                                "       PROJECT_ID,\n" +
                                "       VOUCHER_ID,\n" +
                                "       TOT_AMOUNT,\n" +
                                "       FLAG,\n" +
                                "       BRANCH_ID\n" +
                                "  FROM ASSET_IN_OUT_MASTER\n" +
                                " WHERE IN_OUT_ID = ?IN_OUT_ID;";
                        break;
                    }
                case SQLCommand.AssetInOut.SaveAssetInOutMaster:
                    {
                        query = "INSERT INTO ASSET_IN_OUT_MASTER\n" +
                                "  (IN_OUT_DATE,\n" +
                                "   BILL_INVOICE_NO,\n" +
                                "   VENDOR_ID,\n" +
                                "   DONOR_ID,\n" +
                                "   SOLD_TO,\n" +
                                "   PROJECT_ID,\n" +
                                "   VOUCHER_ID,\n" +
                                "   TOT_DEP_AMOUNT,\n" +
                                "   TOT_AMOUNT,\n" +
                                "   FLAG)\n" +
                                "VALUES\n" +
                                "  (?IN_OUT_DATE,\n" +
                                "   ?BILL_INVOICE_NO,\n" +
                                "   ?VENDOR_ID,\n" +
                                "   ?DONOR_ID,\n" +
                                "   ?SOLD_TO,\n" +
                                "   ?PROJECT_ID,\n" +
                                "   ?VOUCHER_ID,\n" +
                                "   ?TOT_DEP_AMOUNT,\n" +
                                "   ?TOT_AMOUNT,\n" +
                                "   ?FLAG);";
                        break;
                    }
                case SQLCommand.AssetInOut.UpdateAssetInOutMaster:
                    {
                        query = "UPDATE ASSET_IN_OUT_MASTER\n" +
                                 "   SET IN_OUT_DATE     = ?IN_OUT_DATE,\n" +
                                 "       BILL_INVOICE_NO = ?BILL_INVOICE_NO,\n" +
                                 "       VENDOR_ID       = ?VENDOR_ID,\n" +
                                 "       DONOR_ID        = ?DONOR_ID,\n" +
                                 "       SOLD_TO         = ?SOLD_TO,\n" +
                                 "       PROJECT_ID      = ?PROJECT_ID,\n" +
                                 "       VOUCHER_ID      = ?VOUCHER_ID,\n" +
                                 "       TOT_DEP_AMOUNT  = ?TOT_DEP_AMOUNT,\n" +
                                 "       TOT_AMOUNT      = ?TOT_AMOUNT,\n" +
                                 "       FLAG            = ?FLAG\n" +
                                 " WHERE IN_OUT_ID = ?IN_OUT_ID;";
                        break;
                    }
                case SQLCommand.AssetInOut.DeleteAssetInOutMaster:
                    {
                        query = "DELETE FROM ASSET_IN_OUT_MASTER WHERE IN_OUT_ID=?IN_OUT_ID";
                        break;
                    }

                case SQLCommand.AssetInOut.DeleteAssetInOutMasterDetailIds:
                    {
                        query = "DELETE FROM ASSET_IN_OUT_DETAIL WHERE IN_OUT_ID IN(?IN_OUT_IDs);" +
                                "DELETE FROM ASSET_IN_OUT_MASTER WHERE IN_OUT_ID IN(?IN_OUT_IDs);";
                        break;
                    }

                case SQLCommand.AssetInOut.FetchAssetInOutDetailAll:
                    {
                        query = "SELECT IN_OUT_DETAIL_ID, IN_OUT_ID, ITEM_ID, QUANTITY, AMOUNT, BRANCH_ID\n" +
                              "  FROM ASSET_IN_OUT_DETAIL";
                        break;
                    }
                case SQLCommand.AssetInOut.FetchAssetInOutDetailById:
                    {
                        //query = "SELECT IN_OUT_DETAIL_ID,\n" +
                        //         "       LOCATION_ID,\n" +
                        //         "       AIM.IN_OUT_ID,\n" +
                        //         "       AID.ITEM_ID,\n" +
                        //         "       QUANTITY,\n" +
                        //         "       AID.AMOUNT,AI.AMOUNT AS TEMP_AMOUNT,\n" +
                        //         "       AID.BRANCH_ID,\n" +
                        //         "       ACCOUNT_LEDGER_ID AS LEDGER_ID,\n" +
                        //         "       FNL.AVAIL_QTY AS AVAILABLE_QUANTITY \n" +
                        //         "  FROM ASSET_IN_OUT_MASTER AIM\n" +
                        //         " INNER JOIN ASSET_IN_OUT_DETAIL AID\n" +
                        //         "    ON AIM.IN_OUT_ID = AID.IN_OUT_ID\n" +
                        //         " INNER JOIN ASSET_ITEM_DETAIL AI\n" +
                        //         "    ON AID.ITEM_ID = AI.ITEM_ID\n" +
                        //         " INNER JOIN ASSET_ITEM A\n" +
                        //         "    ON AID.ITEM_ID = A.ITEM_ID\n" +
                        //         " LEFT JOIN (SELECT SUM(IN_QTY) - SUM(OUT_QTY) AS AVAIL_QTY,\n" +
                        //         "                    ITEM_ID,\n" +
                        //         "                    PROJECT_ID\n" +
                        //         "               FROM (SELECT ASSET_ID,\n" +
                        //         "                            FLAG,\n" +
                        //         "                            CASE\n" +
                        //         "                              WHEN FLAG IN ('PU', 'IK', 'OP') THEN\n" +
                        //         "                               COUNT(FLAG)\n" +
                        //         "                              ELSE\n" +
                        //         "                               0\n" +
                        //         "                            END AS IN_QTY,\n" +
                        //         "                            CASE\n" +
                        //         "                              WHEN FLAG IN ('SL', 'DN', 'DS') THEN\n" +
                        //         "                               COUNT(FLAG)\n" +
                        //         "                              ELSE\n" +
                        //         "                               0\n" +
                        //         "                            END AS OUT_QTY,\n" +
                        //         "                            IOD.ITEM_ID,\n" +
                        //         "                            AIM.PROJECT_ID,\n" +
                        //         "                            CASE\n" +
                        //         "                              WHEN AID.ITEM_DETAIL_ID = T.SOLD_ITEM_DETAIL_ID THEN\n" +
                        //         "                               0\n" +
                        //         "                              ELSE\n" +
                        //         "                               1\n" +
                        //         "                            END AS STATUS\n" +
                        //         "                       FROM ASSET_IN_OUT_MASTER AIM\n" +
                        //         "                      INNER JOIN ASSET_IN_OUT_DETAIL IOD\n" +
                        //         "                         ON AIM.IN_OUT_ID = IOD.IN_OUT_ID\n" +
                        //         "                       LEFT JOIN ASSET_TRANS AT\n" +
                        //         "                         ON IOD.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID\n" +
                        //         "                       LEFT JOIN ASSET_ITEM_DETAIL AID\n" +
                        //         "                         ON AT.ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID\n" +
                        //         "                       LEFT JOIN (SELECT AIM.IN_OUT_ID,\n" +
                        //         "                                        AID.IN_OUT_DETAIL_ID,\n" +
                        //         "                                        AT.ITEM_DETAIL_ID AS SOLD_ITEM_DETAIL_ID\n" +
                        //         "                                   FROM ASSET_IN_OUT_MASTER AIM\n" +
                        //         "                                  INNER JOIN ASSET_IN_OUT_DETAIL AID\n" +
                        //         "                                     ON AIM.IN_OUT_ID = AID.IN_OUT_ID\n" +
                        //         "                                  INNER JOIN ASSET_TRANS AT\n" +
                        //         "                                     ON AID.IN_OUT_DETAIL_ID =\n" +
                        //         "                                        AT.IN_OUT_DETAIL_ID\n" +
                        //         "                                  WHERE FLAG IN ('SL', 'DN', 'DS')) AS T\n" +
                        //         "                         ON T.SOLD_ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID\n" +
                        //         "                      GROUP BY IOD.IN_OUT_DETAIL_ID) AS TT\n" +
                        //         "              GROUP BY ITEM_ID) AS FNL\n" +
                        //         "    ON AID.ITEM_ID = FNL.ITEM_ID\n" +
                        //         "   AND AIM.PROJECT_ID = FNL.PROJECT_ID\n" +
                        //         " WHERE AIM.IN_OUT_ID = ?IN_OUT_ID\n" +
                        //         " GROUP BY IN_OUT_DETAIL_ID;";

                        query = "SELECT AID.IN_OUT_DETAIL_ID,\n" +
                                    "       LOCATION_ID,\n" +
                                    "       AIM.IN_OUT_ID,\n" +
                                    "       AID.ITEM_ID,\n" +
                                    "       QUANTITY,\n" +
                                    "       AID.AMOUNT,AI.SALVAGE_VALUE,\n" +
                                    "       ACT_AMT              AS TEMP_AMOUNT,\n" +
                                    "       ABS(AID.AMOUNT - ACT_AMT) AS DIFFERENCE,\n" +
                                    "       IF((AID.AMOUNT - ACT_AMT)>0, 'Gain',IF((AID.AMOUNT - ACT_AMT)=0,'','Loss')) AS TYPE,\n" +
                                    "       AID.BRANCH_ID,\n" +
                                    "       ACCOUNT_LEDGER_ID    AS LEDGER_ID,\n" +
                                    "       FNL.AVAIL_QTY        AS AVAILABLE_QUANTITY\n" +
                                    "  FROM ASSET_IN_OUT_MASTER AIM\n" +
                                    " INNER JOIN ASSET_IN_OUT_DETAIL AID\n" +
                                    "    ON AIM.IN_OUT_ID = AID.IN_OUT_ID\n" +
                                    " INNER JOIN ASSET_ITEM_DETAIL AI\n" +
                                    "    ON AID.ITEM_ID = AI.ITEM_ID\n" +
                                    " INNER JOIN ASSET_ITEM A\n" +
                                    "    ON AID.ITEM_ID = A.ITEM_ID\n" +
                                    "  LEFT JOIN (SELECT SUM(at.amount) as ACT_AMT, AT.IN_OUT_DETAIL_ID\n" +
                                    "\n" +
                                    "               FROM ASSET_IN_OUT_MASTER AIM\n" +
                                    "              INNER JOIN ASSET_IN_OUT_DETAIL AID\n" +
                                    "                 ON AIM.IN_OUT_ID = AID.IN_OUT_ID\n" +
                                    "              INNER JOIN ASSET_TRANS AT\n" +
                                    "                 ON AID.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID\n" +
                                    "              WHERE FLAG IN ('SL', 'DN', 'DS')\n" +
                                    "                AND AIM.IN_OUT_ID = ?IN_OUT_ID\n" +
                                    "              group by AT.in_out_detail_id, item_id) AS TF\n" +
                                    "    ON AID.IN_OUT_DETAIL_ID = TF.IN_OUT_DETAIL_ID\n" +
                                    "  LEFT JOIN (SELECT SUM(IN_QTY) - SUM(OUT_QTY) AS AVAIL_QTY,\n" +
                                    "                    ITEM_ID,\n" +
                                    "                    PROJECT_ID\n" +
                                    "               FROM (SELECT ASSET_ID,\n" +
                                    "                            FLAG,\n" +
                                    "                            CASE\n" +
                                    "                              WHEN FLAG IN ('PU', 'IK', 'OP') THEN\n" +
                                    "                               COUNT(FLAG)\n" +
                                    "                              ELSE\n" +
                                    "                               0\n" +
                                    "                            END AS IN_QTY,\n" +
                                    "                            CASE\n" +
                                    "                              WHEN FLAG IN ('SL', 'DN', 'DS') THEN\n" +
                                    "                               COUNT(FLAG)\n" +
                                    "                              ELSE\n" +
                                    "                               0\n" +
                                    "                            END AS OUT_QTY,\n" +
                                    "                            IOD.ITEM_ID,\n" +
                                    "                            AIM.PROJECT_ID,\n" +
                                    "                            CASE\n" +
                                    "                              WHEN AID.ITEM_DETAIL_ID =\n" +
                                    "                                   T.SOLD_ITEM_DETAIL_ID THEN\n" +
                                    "                               0\n" +
                                    "                              ELSE\n" +
                                    "                               1\n" +
                                    "                            END AS STATUS\n" +
                                    "                       FROM ASSET_IN_OUT_MASTER AIM\n" +
                                    "                      INNER JOIN ASSET_IN_OUT_DETAIL IOD\n" +
                                    "                         ON AIM.IN_OUT_ID = IOD.IN_OUT_ID\n" +
                                    "                       LEFT JOIN ASSET_TRANS AT\n" +
                                    "                         ON IOD.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID\n" +
                                    "                       LEFT JOIN ASSET_ITEM_DETAIL AID\n" +
                                    "                         ON AT.ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID\n" +
                                    "                       LEFT JOIN (SELECT AIM.IN_OUT_ID,\n" +
                                    "                                        AID.IN_OUT_DETAIL_ID,\n" +
                                    "                                        AT.ITEM_DETAIL_ID AS SOLD_ITEM_DETAIL_ID\n" +
                                    "                                   FROM ASSET_IN_OUT_MASTER AIM\n" +
                                    "                                  INNER JOIN ASSET_IN_OUT_DETAIL AID\n" +
                                    "                                     ON AIM.IN_OUT_ID = AID.IN_OUT_ID\n" +
                                    "                                  INNER JOIN ASSET_TRANS AT\n" +
                                    "                                     ON AID.IN_OUT_DETAIL_ID =\n" +
                                    "                                        AT.IN_OUT_DETAIL_ID\n" +
                                    "                                  WHERE FLAG IN ('SL', 'DN', 'DS')) AS T\n" +
                                    "                         ON T.SOLD_ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID\n" +
                                    "                      GROUP BY IOD.IN_OUT_DETAIL_ID) AS TT\n" +
                                    "              GROUP BY ITEM_ID) AS FNL\n" +
                                    "    ON AID.ITEM_ID = FNL.ITEM_ID\n" +
                                    "   AND AIM.PROJECT_ID = FNL.PROJECT_ID\n" +
                                    " WHERE AIM.IN_OUT_ID = ?IN_OUT_ID\n" +
                                    " GROUP BY IN_OUT_DETAIL_ID;";
                        break;
                    }
                case SQLCommand.AssetInOut.SaveAssetInOutDetail:
                    {
                        query = "INSERT INTO ASSET_IN_OUT_DETAIL\n" +
                                "  (IN_OUT_ID, ITEM_ID, QUANTITY, DEPRECIATION_AMOUNT, AMOUNT,BALANCE_OP_DATE)\n" +  // SALVAGE_VALUE,
                                "VALUES\n" +
                                "  (?IN_OUT_ID, ?ITEM_ID, ?QUANTITY, ?DEPRECIATION_AMOUNT, ?AMOUNT, ?BALANCE_OP_DATE)";  // ?SALVAGE_VALUE,
                        break;
                    }
                case SQLCommand.AssetInOut.UpdateAssetInOutDetail:
                    {
                        query = "UPDATE ASSET_IN_OUT_DETAIL\n" +
                                "   SET \n" +
                                "       ITEM_ID   = ?ITEM_ID,\n" +
                                "       QUANTITY  = ?QUANTITY,\n" +
                                "       DEPRECIATION_AMOUNT  = ?DEPRECIATION_AMOUNT,\n" +
                                "       AMOUNT    = ?AMOUNT, \n" +
                                "       BALANCE_OP_DATE   = ?BALANCE_OP_DATE \n" +
                                " WHERE IN_OUT_DETAIL_ID = ?IN_OUT_DETAIL_ID";  //  "       SALVAGE_VALUE  = ?SALVAGE_VALUE,\n" +
                        break;
                    }
                case SQLCommand.AssetInOut.DeleteAssetInOutDetail:
                    {
                        query = "DELETE FROM ASSET_IN_OUT_DETAIL WHERE IN_OUT_ID=?IN_OUT_ID";
                        break;
                    }
                case SQLCommand.AssetInOut.DeleteAssetInOutDetailbyId:
                    {
                        query = "DELETE FROM ASSET_IN_OUT_DETAIL WHERE IN_OUT_DETAIL_ID IN (?IN_OUT_DETAIL_ID)";
                        break;
                    }
                case SQLCommand.AssetInOut.DeleteAssetTrans:
                    {
                        query = "DELETE FROM ASSET_TRANS WHERE IN_OUT_DETAIL_ID IN (?IN_OUT_DETAIL_ID) AND ITEM_DETAIL_ID IN (?ITEM_DETAIL_ID)";
                        break;
                    }
                case SQLCommand.AssetInOut.DeleteAssetTransInOutDetailIds:
                    {
                        query = "DELETE FROM ASSET_TRANS WHERE IN_OUT_DETAIL_ID IN (?IN_OUT_DETAIL_IDs) AND ITEM_DETAIL_ID IN (?ITEM_DETAIL_IDs)";
                        break;
                    }
                case SQLCommand.AssetInOut.DeleteAssetTransByInoutDetailId:
                    {
                        query = "DELETE FROM ASSET_TRANS WHERE IN_OUT_DETAIL_ID IN (?IN_OUT_DETAIL_ID)";
                        break;
                    }
                case SQLCommand.AssetInOut.SaveAssetTrans:
                    {
                        //query = "INSERT INTO ASSET_TRANS(IN_OUT_DETAIL_ID,ITEM_DETAIL_ID) VALUES(?IN_OUT_DETAIL_ID,?ITEM_DETAIL_ID)";
                        query = "INSERT INTO ASSET_TRANS(IN_OUT_DETAIL_ID,ITEM_DETAIL_ID,DEPRECIATION_AMOUNT,AMOUNT,GAIN_AMOUNT,LOSS_AMOUNT) \n" +
                                "VALUES(?IN_OUT_DETAIL_ID,?ITEM_DETAIL_ID,?DEPRECIATION_AMOUNT,?AMOUNT,?GAIN_AMOUNT,?LOSS_AMOUNT)";
                        break;
                    }
                case SQLCommand.AssetInOut.DeleteAssetItemDetailById:
                    {
                        query = "DELETE FROM ASSET_ITEM_DETAIL WHERE ITEM_DETAIL_ID IN (?ITEM_DETAIL_IDs)";
                        break;
                    }
                case SQLCommand.AssetInOut.DeleteAssetItemDetail:
                    {
                        query = "DELETE FROM ASSET_TRANS WHERE ITEM_DETAIL_ID IN (?ITEM_DETAIL_ID);DELETE FROM ASSET_ITEM_DETAIL WHERE ITEM_DETAIL_ID IN (?ITEM_DETAIL_ID);";
                        break;
                    }

                case SQLCommand.AssetInOut.FetchItemDetailIdByAssetId:
                    {
                        query = "SELECT ITEM_DETAIL_ID FROM ASSET_ITEM_DETAIL WHERE ASSET_ID IN(?ASSET_ID)";
                        break;
                    }
                case SQLCommand.AssetInOut.CheckItemDetailIdExists:
                    {
                        query = "SELECT ITEM_DETAIL_ID FROM ASSET_ITEM_DETAIL WHERE ITEM_DETAIL_ID IN(?ITEM_DETAIL_ID)";
                        break;
                    }
                case SQLCommand.AssetInOut.FetchAvailQty:
                    {
                        //query = "SELECT COUNT(AID.ITEM_DETAIL_ID) AS AVAIL_QTY\n" +
                        //        "  FROM ASSET_IN_OUT_MASTER AM\n" +
                        //        " INNER JOIN ASSET_IN_OUT_DETAIL AIO\n" +
                        //        "    ON AM.IN_OUT_ID = AIO.IN_OUT_ID\n" +
                        //        " INNER JOIN ASSET_TRANS AT\n" +
                        //        "    ON AT.IN_OUT_DETAIL_ID = AIO.IN_OUT_DETAIL_ID\n" +
                        //        " INNER JOIN ASSET_ITEM_DETAIL AID\n" +
                        //        "    ON AID.ITEM_DETAIL_ID = AT.ITEM_DETAIL_ID\n" +
                        //        " WHERE AM.FLAG NOT IN ('SL','DS','DN')\n" +
                        //        "   AND AID.PROJECT_ID IN (?PROJECT_ID) \n" +
                        //        "   AND AID.ITEM_ID IN (?ITEM_ID);";
                        query = "SELECT SUM(IN_QTY) - SUM(OUT_QTY) AS AVAIL_QTY\n" +
                                "  FROM (SELECT ASSET_ID,\n" +
                                "               FLAG,\n" +
                                "               CASE\n" +
                                "                 WHEN FLAG IN ('PU', 'IK', 'OP') THEN\n" +
                                "                  COUNT(FLAG)\n" +
                                "                 ELSE\n" +
                                "                  0\n" +
                                "               END AS IN_QTY,\n" +
                                "               CASE\n" +
                                "                 WHEN FLAG IN ('SL', 'DN', 'DS') THEN\n" +
                                "                  COUNT(FLAG)\n" +
                                "                 ELSE\n" +
                                "                  0\n" +
                                "               END AS OUT_QTY,\n" +
                                "               CASE\n" +
                                "                 WHEN AID.ITEM_DETAIL_ID = T.SOLD_ITEM_DETAIL_ID THEN\n" +
                                "                  0\n" +
                                "                 ELSE\n" +
                                "                  1\n" +
                                "               END AS STATUS\n" +
                                "          FROM ASSET_IN_OUT_MASTER AIM\n" +
                                "\n" +
                                "         INNER JOIN ASSET_IN_OUT_DETAIL IOD\n" +
                                "            ON AIM.IN_OUT_ID = IOD.IN_OUT_ID\n" +
                                "          LEFT JOIN ASSET_TRANS AT\n" +
                                "            ON IOD.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID\n" +
                                "          LEFT JOIN ASSET_ITEM_DETAIL AID\n" +
                                "            ON AT.ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID\n" +
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
                                "         WHERE AIM.PROJECT_ID IN (?PROJECT_ID)\n" +
                                "           AND IOD.ITEM_ID IN (?ITEM_ID)\n" +
                                "           AND AIM.IN_OUT_DATE<=?IN_OUT_DATE\n" +
                                "         GROUP BY FLAG) AS TT;";

                        break;
                    }
                case SQLCommand.AssetInOut.FetchCurrentQty:
                    {
                        query = "SELECT QUANTITY FROM ASSET_IN_OUT_DETAIL WHERE IN_OUT_DETAIL_ID IN(?IN_OUT_DETAIL_ID)";
                        break;
                    }
                case SQLCommand.AssetInOut.FetchVoucherIdbyMasterId:
                    {
                        query = "SELECT VOUCHER_ID FROM ASSET_IN_OUT_MASTER {WHERE IN_OUT_ID IN(?IN_OUT_ID)}";
                        break;
                    }
                case SQLCommand.AssetInOut.FetchVoucherIdCollection:
                    {
                        query = "SELECT GROUP_CONCAT(VOUCHER_ID) FROM ASSET_IN_OUT_MASTER WHERE VOUCHER_ID >0";
                        break;
                    }
                case SQLCommand.AssetInOut.FetchVoucherDetailsByVoucherId:
                    {
                        query = " SELECT VMT.VOUCHER_ID,VMT.VOUCHER_NO, VMT.NARRATION,VMT.PURPOSE_ID,VMT.DONOR_ID,VMT.NAME_ADDRESS,VMT.CURRENCY_COUNTRY_ID, VMT.CONTRIBUTION_AMOUNT, VMT.EXCHANGE_RATE, VMT.CALCULATED_AMOUNT, VMT.ACTUAL_AMOUNT, VMT.EXCHANGE_COUNTRY_ID " +
                                " FROM VOUCHER_MASTER_TRANS VMT WHERE VOUCHER_ID IN(?VOUCHER_ID)";
                        break;
                    }
                case SQLCommand.AssetInOut.FetchLasterAssetId:
                    {
                        // query = "SELECT COUNT(*)  FROM ASSET_ITEM_DETAIL AID WHERE ITEM_ID=?ITEM_ID;";
                        //query = "SELECT ASSET_ID  FROM ASSET_ITEM_DETAIL AID WHERE ITEM_ID=?ITEM_ID ORDER BY ITEM_DETAIL_ID DESC LIMIT 1";
                        query = "SELECT ASSET_ID  FROM ASSET_ITEM_DETAIL AID WHERE ITEM_ID=?ITEM_ID AND PROJECT_ID=?PROJECT_ID ORDER BY ITEM_DETAIL_ID DESC LIMIT 1";
                        break;
                    }
                case SQLCommand.AssetInOut.FetchFixedAssetRegister:
                    {
                        query = "SELECT IOD.IN_OUT_DETAIL_ID,IOD.ITEM_ID,\n" +
                                "       IOD.IN_OUT_ID,\n" +
                                "       IN_OUT_DATE,\n" +
                                "       VENDOR,\n" +
                                "       BILL_INVOICE_NO,\n" +
                                "       PROJECT,\n" +
                                "       IO.PROJECT_ID,\n" +
                                "       AC.ASSET_CLASS,\n" +
                                "       AI.ASSET_ITEM,\n" +
                                "     CASE WHEN IO.FLAG IN ('OP', 'PU', 'IK') THEN    SUM(IOD.QUANTITY) END AS COST_NO,\n" +
                                "     CASE WHEN IO.FLAG IN ('OP', 'PU', 'IK') THEN  SUM(IOD.AMOUNT) END AS COST_AMOUNT,\n" +
                                "        SUM(SOLD.SOLD_NO) AS SOLD_NO,\n" +
                                "       SUM(SOLD.SOLD_AMOUNT) AS SOLD_AMOUNT,\n" +
                                "       (IFNULL(IOD.QUANTITY, 0) - IFNULL(SUM(SOLD.SOLD_NO), 0)) AS BALANCE_NO,\n" +
                                "       (IFNULL(IOD.AMOUNT, 0) - IFNULL(SUM(SOLD.SOLD_AMOUNT), 0)) AS BALANCE_AMOUNT\n" +
                                "  FROM ASSET_IN_OUT_MASTER IO\n" +
                                "  LEFT JOIN ASSET_IN_OUT_DETAIL IOD\n" +
                                "    ON IOD.IN_OUT_ID = IO.IN_OUT_ID\n" +
                                "  LEFT JOIN ASSET_STOCK_VENDOR V\n" +
                                "    ON IO.VENDOR_ID = V.VENDOR_ID\n" +
                                "  LEFT JOIN MASTER_PROJECT MP\n" +
                                "    ON IO.PROJECT_ID = MP.PROJECT_ID\n" +
                                "  LEFT JOIN ASSET_ITEM AI\n" +
                                "    ON IOD.ITEM_ID = AI.ITEM_ID\n" +
                                "  LEFT JOIN ASSET_CLASS AC\n" +
                                "   ON AI.ASSET_CLASS_ID=AC.ASSET_CLASS_ID\n" +
                                "  LEFT JOIN (SELECT IOD.IN_OUT_DETAIL_ID,IOD.ITEM_ID,\n" +
                                "                    IOD.IN_OUT_ID,\n" +
                                "                    COUNT(*) AS SOLD_NO,\n" +
                                "                    SUM(AID.AMOUNT) AS SOLD_AMOUNT\n" +
                                "               FROM ASSET_IN_OUT_MASTER IO\n" +
                                "               LEFT JOIN ASSET_IN_OUT_DETAIL IOD\n" +
                                "                 ON IOD.IN_OUT_ID = IO.IN_OUT_ID\n" +
                                "               LEFT JOIN ASSET_TRANS AT\n" +
                                "                 ON IOD.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID\n" +
                                "               LEFT JOIN ASSET_ITEM_DETAIL AID\n" +
                                "                 ON AT.ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID\n" +
                                "              WHERE IO.FLAG IN ('SL', 'DN', 'DS')\n" +
                                "              GROUP BY AT.IN_OUT_DETAIL_ID) AS SOLD\n" +
                                "    ON IOD.IN_OUT_DETAIL_ID = SOLD.IN_OUT_DETAIL_ID\n" +   // IO.FLAG IN ('OP', 'PU', 'IK','SL', 'DN', 'DS')
                                "GROUP BY IN_OUT_DATE,ITEM_ID ORDER BY IN_OUT_DATE;";
                        //  " WHERE \n" +
                        //  "IO.PROJECT_ID IN(?PROJECT_ID) {AND AI.ASSET_CLASS_ID IN(?ASSET_CLASS_ID)} AND IN_OUT_DATE <= ?IN_OUT_DATE GROUP BY IN_OUT_DATE,ITEM_ID ORDER BY IN_OUT_DATE;";
                        break;
                    }
                case SQLCommand.AssetInOut.FetchOPDetailsByProjectId:
                    {
                        query = "SELECT IN_OUT_DETAIL_ID,\n" +
                                "       LOCATION_ID,\n" +
                                "       AID.IN_OUT_ID,\n" +
                                "       AID.ITEM_ID,\n" +
                                "       QUANTITY,\n" +
                                "       AID.DEPRECIATION_AMOUNT,AID.AMOUNT, BALANCE_OP_DATE, SALVAGE_VALUE,\n" +
                                "       AID.BRANCH_ID,\n" +
                                "       ACCOUNT_LEDGER_ID AS LEDGER_ID,\n" +
                                "      CONCAT(ML.LEDGER_NAME,' - ' ,MLG.LEDGER_GROUP, IF(LEDGER_CODE = '', '', CONCAT('(', LEDGER_CODE, ')'))) AS LEDGER_NAME,\n" +
                                "       IFNULL(LEDTBLE.AMOUNT,0.00)    AS LED_AMOUNT,\n" +
                                "       FLAG,\n" +
                                "       AVAIL_QTY         AS AVAILABLE_QUANTITY\n" +
                                "  FROM ASSET_IN_OUT_MASTER AIM\n" +
                                " INNER JOIN ASSET_IN_OUT_DETAIL AID\n" +
                                "    ON AIM.IN_OUT_ID = AID.IN_OUT_ID\n" +
                                " INNER JOIN ASSET_ITEM_DETAIL AI\n" +
                                "    ON AID.ITEM_ID = AI.ITEM_ID\n" +
                                " INNER JOIN ASSET_ITEM A\n" +
                                "    ON AID.ITEM_ID = A.ITEM_ID\n" +
                                " INNER JOIN MASTER_LEDGER ML\n" +
                                "    ON ML.LEDGER_ID= A.ACCOUNT_LEDGER_ID\n" +
                                "INNER JOIN MASTER_LEDGER_GROUP MLG \n" +
                                "   ON ML.GROUP_ID = MLG.GROUP_ID \n" +
                                "\n" +
                                "  LEFT JOIN (SELECT LB.LEDGER_ID, AMOUNT, CONCAT(LEDGER_NAME,' - ' ,MLG.LEDGER_GROUP, IF(LEDGER_CODE = '', '', CONCAT('(', LEDGER_CODE, ')'))) AS LEDGER_NAME\n" +
                                "               FROM LEDGER_BALANCE LB\n" +
                                "              INNER JOIN MASTER_LEDGER ML\n" +
                                "                 ON ML.LEDGER_ID = LB.LEDGER_ID\n" +
                                " INNER JOIN MASTER_LEDGER_GROUP MLG \n" +
                                "       ON ML.GROUP_ID = MLG.GROUP_ID \n" +
                                "              WHERE LB.PROJECT_ID IN (?PROJECT_ID) AND BALANCE_DATE IN\n" +
                                "                    (SELECT MAX(BALANCE_DATE)\n" +
                                "                       FROM LEDGER_BALANCE LB\n" +
                                "                      INNER JOIN ASSET_ITEM A\n" +
                                "                         ON A.ACCOUNT_LEDGER_ID = LB.LEDGER_ID\n" +
                                "                        AND TRANS_FLAG = 'OP')) AS LEDTBLE\n" +
                                "    ON A.ACCOUNT_LEDGER_ID = LEDTBLE.LEDGER_ID\n" +
                                "\n" +
                                "  LEFT JOIN (SELECT SUM(IN_QTY) - SUM(OUT_QTY) AS AVAIL_QTY, ITEM_ID\n" +
                                "               FROM (SELECT ASSET_ID,\n" +
                                "                            FLAG,\n" +
                                "                            CASE\n" +
                                "                              WHEN FLAG IN ('PU', 'IK', 'OP') THEN\n" +
                                "                               1\n" +
                                "                              ELSE\n" +
                                "                               0\n" +
                                "                            END AS IN_QTY,\n" +
                                "                            CASE\n" +
                                "                              WHEN FLAG IN ('SL', 'DN', 'DS') THEN\n" +
                                "                               1\n" +
                                "                              ELSE\n" +
                                "                               0\n" +
                                "                            END AS OUT_QTY,\n" +
                                "                            CASE\n" +
                                "                              WHEN AID.ITEM_DETAIL_ID = T.SOLD_ITEM_DETAIL_ID THEN\n" +
                                "                               0\n" +
                                "                              ELSE\n" +
                                "                               1\n" +
                                "                            END AS STATUS,\n" +
                                "                            IOD.ITEM_ID\n" +
                                "                       FROM ASSET_IN_OUT_MASTER AIM\n" +
                                "\n" +
                                "                      INNER JOIN ASSET_IN_OUT_DETAIL IOD\n" +
                                "                         ON AIM.IN_OUT_ID = IOD.IN_OUT_ID\n" +
                                "                       LEFT JOIN ASSET_TRANS AT\n" +
                                "                         ON IOD.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID\n" +
                                "                       LEFT JOIN ASSET_ITEM_DETAIL AID\n" +
                                "                         ON AT.ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID\n" +
                                "                       LEFT JOIN (SELECT AIM.IN_OUT_ID,\n" +
                                "                                        AID.IN_OUT_DETAIL_ID,\n" +
                                "                                        AT.ITEM_DETAIL_ID AS SOLD_ITEM_DETAIL_ID\n" +
                                "                                   FROM ASSET_IN_OUT_MASTER AIM\n" +
                                "                                  INNER JOIN ASSET_IN_OUT_DETAIL AID\n" +
                                "                                     ON AIM.IN_OUT_ID = AID.IN_OUT_ID\n" +
                                "                                  INNER JOIN ASSET_TRANS AT\n" +
                                "                                     ON AID.IN_OUT_DETAIL_ID =\n" +
                                "                                        AT.IN_OUT_DETAIL_ID\n" +
                                "                                  WHERE FLAG IN ('SL', 'DN', 'DS')) AS T\n" +
                                "                         ON T.SOLD_ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID\n" +
                                "                      WHERE AIM.PROJECT_ID IN (?PROJECT_ID)) AS TT\n" +
                                "              GROUP BY ITEM_ID) AS FN\n" +
                                "    ON AID.ITEM_ID = FN.ITEM_ID\n" +
                                " WHERE FLAG IN ('OP')\n" +
                                "   AND AIM.PROJECT_ID IN (?PROJECT_ID)\n" +
                                " GROUP BY IN_OUT_DETAIL_ID;";
                        break;
                    }
                case SQLCommand.AssetInOut.FetchOPInoutIdsByProjectId:
                    {
                        //query = "SELECT GROUP_CONCAT(IN_OUT_ID) AS IN_OUT_ID FROM ASSET_IN_OUT_MASTER WHERE FLAG='OP' AND PROJECT_ID IN (?PROJECT_ID)";
                        query = "SELECT GROUP_CONCAT(AID.IN_OUT_DETAIL_ID) AS IN_OUT_DETAIL_ID,GROUP_CONCAT(AIM.IN_OUT_ID) AS IN_OUT_ID\n" +
                                "  FROM ASSET_IN_OUT_MASTER AIM\n" +
                                " INNER JOIN ASSET_IN_OUT_DETAIL AID\n" +
                                "    ON AIM.IN_OUT_ID = AID.IN_OUT_ID\n" +
                                " WHERE FLAG = 'OP'\n" +
                                "   AND PROJECT_ID IN (?PROJECT_ID);";
                        break;
                    }
                case SQLCommand.AssetInOut.CheckAssetIDExists:
                    {
                        query = "SELECT GROUP_CONCAT(ASSET_ID) AS ASSET_ID FROM ASSET_ITEM_DETAIL WHERE ITEM_ID IN (?ITEM_ID) AND ASSET_ID IN (?ASSET_ID) AND PROJECT_ID IN (?PROJECT_ID) { AND ITEM_DETAIL_ID NOT IN (?ITEM_DETAIL_ID) }";
                        break;
                    }
                case SQLCommand.AssetInOut.DeleteAssetUnusedItems:
                    {
                        query = "SELECT GROUP_CONCAT(ASSET_ID) AS ASSET_ID FROM ASSET_ITEM_DETAIL WHERE ITEM_ID IN (?ITEM_ID) AND ASSET_ID IN (?ASSET_ID)";
                        break;
                    }

                case SQLCommand.AssetInOut.CheckAssetTransactionExists:
                    {
                        query = "SELECT COUNT(IN_OUT_ID) AS RECCOUNT FROM ASSET_IN_OUT_MASTER";
                        break;
                    }
                case SQLCommand.AssetInOut.FetchAccountLedgerByItem:
                    {
                        //query = "SELECT LB.LEDGER_ID, AMOUNT AS LED_AMOUNT, CONCAT(LEDGER_NAME,' - ' ,MLG.LEDGER_GROUP, ' (' ,LEDGER_CODE,')') AS LEDGER_NAME\n" +
                        //        "  FROM LEDGER_BALANCE LB\n" +
                        //        " INNER JOIN ASSET_ITEM AI\n" +
                        //        "    ON AI.ACCOUNT_LEDGER_ID = LB.LEDGER_ID\n" +
                        //        " INNER JOIN MASTER_LEDGER ML\n" +
                        //        "    ON ML.LEDGER_ID = LB.LEDGER_ID\n" +
                        //        " INNER JOIN MASTER_LEDGER_GROUP MLG \n" +
                        //        "       ON ML.GROUP_ID = MLG.GROUP_ID \n" +
                        //        "  LEFT JOIN (SELECT MAX(BALANCE_DATE) AS BAL_DATE\n" +
                        //        "               FROM LEDGER_BALANCE\n" +
                        //        "              WHERE TRANS_FLAG = 'OP') AS T\n" +
                        //        "    ON LB.BALANCE_DATE = T.BAL_DATE\n" +
                        //        " WHERE AI.ITEM_ID IN (?ITEM_ID)\n" +
                        //        " GROUP BY LEDGER_ID;";

                        query = "SELECT FNL.LED_ID AS LEDGER_ID,SUM(FNL.LED_AMOUNT) AS LED_AMOUNT, FNL.LEDGER_NAME\n" +
                           "\n" +
                           "  FROM (SELECT ML.LEDGER_ID AS LED_ID,\n" +
                           "               0 AS LED_AMOUNT,\n" +
                           "              CONCAT(LEDGER_NAME,' - ' ,MLG.LEDGER_GROUP, IF(LEDGER_CODE = '', '', CONCAT('(', LEDGER_CODE, ')'))) AS LEDGER_NAME\n" +
                           "          FROM MASTER_LEDGER ML\n" +
                           "\n" +
                           "          LEFT JOIN ASSET_ITEM AI\n" +
                           "            ON AI.ACCOUNT_LEDGER_ID = ML.LEDGER_ID\n" +
                           "          LEFT JOIN MASTER_LEDGER_GROUP MLG\n" +
                           "            ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                           "         WHERE AI.ITEM_ID IN (?ITEM_ID)\n" +
                           "         GROUP BY LEDGER_ID\n" +
                           "\n" +
                           "        UNION ALL\n" +
                           "\n" +
                           "        SELECT LB.LEDGER_ID  AS LED_ID,\n" +
                           "               LB.AMOUNT     AS LED_AMOUNT,\n" +
                           "               T.LEDGER_NAME\n" +
                           "          FROM LEDGER_BALANCE LB\n" +
                           "\n" +
                           "         INNER JOIN (SELECT ML.LEDGER_ID AS LED_ID,\n" +
                           "                            0 AS LED_AMOUNT,\n" +
                           "                            CONCAT(LEDGER_NAME,\n" +
                           "                                   ' - ',\n" +
                           "                                   MLG.LEDGER_GROUP,\n" +
                           "                                   ' ',\n" +
                           "                                   IF(LEDGER_CODE = '',\n" +
                           "                                      '',\n" +
                           "                                      CONCAT('(', LEDGER_CODE, ')'))) AS LEDGER_NAME,\n" +
                           "                            IF(LEDGER_CODE = '',\n" +
                           "                               '',\n" +
                           "                               CONCAT('(', LEDGER_CODE, ')')) AS DD,\n" +
                           "                            LEDGER_CODE\n" +
                           "                       FROM MASTER_LEDGER ML\n" +
                           "\n" +
                           "                       LEFT JOIN ASSET_ITEM AI\n" +
                           "                         ON AI.ACCOUNT_LEDGER_ID = ML.LEDGER_ID\n" +
                           "                       LEFT JOIN MASTER_LEDGER_GROUP MLG\n" +
                           "                         ON ML.GROUP_ID = MLG.GROUP_ID\n" +
                           "                      WHERE AI.ITEM_ID IN (?ITEM_ID)\n" +
                           "                      GROUP BY LEDGER_ID) AS T\n" +
                           "            ON T.LED_ID = LB.LEDGER_ID\n" +
                           "           AND LB.BALANCE_DATE IN\n" +
                           "               (SELECT MAX(BALANCE_DATE) AS BAL_DATE\n" +
                           "                  FROM LEDGER_BALANCE\n" +
                           "                 WHERE TRANS_FLAG = 'OP')\n" +
                           "           and LB.PROJECT_ID IN (?PROJECT_ID)) AS FNL\n" +
                           " GROUP BY LED_ID;";
                        break;
                    }

                case SQLCommand.AssetInOut.DeleteAllAssetTransaction:
                    {
                        //query = "DELETE FROM ASSET_TRANS;DELETE FROM ASSET_AMC_ITEM_MAPPING;\n" +
                        //        "DELETE FROM ASSET_INSURANCE_DETAIL;\n" +
                        //        "DELETE FROM ASSET_AMC_RENEWAL_MASTER;\n" +
                        //        "DELETE FROM ASSET_AMC_ITEM_MAPPING;\n" +
                        //        "DELETE FROM ASSET_AMC_RENEWAL_HISTORY;\n" +
                        //        "DELETE FROM ASSET_ITEM_DETAIL;\n" +
                        //        "DELETE FROM ASSET_IN_OUT_DETAIL;\n" +
                        //        "DELETE FROM ASSET_IN_OUT_MASTER;";

                        query = "DELETE FROM ASSET_TRANS;\n" +
                                "DELETE FROM ASSET_PROJECT_LOCATION ;\n" +
                                "DELETE FROM ASSET_AMC_ITEM_MAPPING;\n" +
                                "DELETE FROM ASSET_AMC_RENEWAL_HISTORY;\n" +
                                "DELETE FROM ASSET_AMC_RENEWAL_MASTER;\n" +
                                "DELETE FROM ASSET_INSURANCE_DETAIL;\n" +
                                "DELETE FROM ASSET_ITEM_DETAIL ;\n" +
                                "DELETE FROM ASSET_IN_OUT_DETAIL ;\n" +
                                "DELETE FROM ASSET_IN_OUT_MASTER ;\n" +
                            //sudhakar
                                "DELETE FROM asset_trans;\n" +
                                "DELETE FROM ASSET_ITEM_DETAIL ;\n" +
                                "DELETE FROM asset_in_out_detail;\n" +
                                "DELETE FROM asset_in_out_master;\n" +
                                "DELETE FROM asset_item;\n" +
                                "DELETE FROM asset_stock_manufacturer;\n" +
                                "DELETE FROM asset_trans;\n" +
                                "DELETE FROM asset_location ;\n" +
                                "DELETE FROM asset_custodian;\n" +
                                "DELETE FROM asset_block ;\n" +
                                "DELETE FROM asset_item;";
                        //  "DELETE FROM uom;";

                        break;
                    }
                case SQLCommand.AssetInOut.FetchTransactionDetailsByItemId:
                    {
                        query = "SELECT AI.ITEM_ID, AIM.PROJECT_ID, AID.AMOUNT\n" +
                                "  FROM ASSET_ITEM AI\n" +
                                " INNER JOIN ASSET_IN_OUT_DETAIL AID\n" +
                                "    ON AI.ITEM_ID = AID.ITEM_ID\n" +
                                " INNER JOIN ASSET_IN_OUT_MASTER AIM\n" +
                                "    ON AIM.IN_OUT_ID = AID.IN_OUT_ID\n" +
                                " WHERE FLAG = 'OP'\n" +
                                "   AND AI.ITEM_ID IN (?ITEM_ID)\n" +
                                " GROUP BY AIM.IN_OUT_ID, AID.IN_OUT_DETAIL_ID;";
                        break;
                    }
            }
            return query;
        }
        #endregion
    }
}
