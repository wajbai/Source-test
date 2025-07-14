using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.DAO;

namespace Bosco.SQL
{
    public class AssetItemSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.AssetItem).FullName)
            {
                query = GetgroupSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        #region SQL Script
        public string GetgroupSQL()
        {
            string query = "";
            SQLCommand.AssetItem SqlcommandId = (SQLCommand.AssetItem)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.AssetItem.Add:
                    {
                        query = "INSERT INTO ASSET_ITEM\n" +
                                "  (ASSET_CLASS_ID,\n" +
                                "   DEPRECIATION_LEDGER_ID,\n" +
                                "   DISPOSAL_LEDGER_ID,\n" +
                                "   ACCOUNT_LEDGER_ID,\n" +
                                "   ASSET_ITEM,\n" +
                                "   UOM_ID,\n" +
                                "   PREFIX,\n" +
                                "   SUFFIX,\n" +
                                "   RETENTION_YRS,\n" +
                                "   DEPRECIATION_YRS,\n" +
                                "   IS_INSURANCE,\n" +
                                "   IS_AMC,\n" +
                                "   IS_ASSET_DEPRECIATION,\n" +
                                "   STARTING_NO,WIDTH,ASSET_MODE, DEPRECIATION_NO, ASSET_ACCESS_FLAG)\n" +
                                "VALUES\n" +
                                "  (?ASSET_CLASS_ID,\n" +
                                "   ?DEPRECIATION_LEDGER_ID,\n" +
                                "   ?DISPOSAL_LEDGER_ID,\n" +
                                "   ?ACCOUNT_LEDGER_ID,\n" +
                                "   ?ASSET_ITEM,\n" +
                                "   ?UOM_ID,\n" +
                                "   ?PREFIX,\n" +
                                "   ?SUFFIX,\n" +
                                "   ?RETENTION_YRS,\n" +
                                "   ?DEPRECIATION_YRS,\n" +
                                "   ?IS_INSURANCE,\n" +
                                "   ?IS_AMC,\n" +
                                "   ?IS_ASSET_DEPRECIATION,\n" +
                                "   ?STARTING_NO,?WIDTH,?ASSET_MODE,?DEPRECIATION_NO,?ASSET_ACCESS_FLAG);";
                        break;
                    }

                case SQLCommand.AssetItem.Delete:
                    {
                        query = "DELETE FROM ASSET_ITEM WHERE ITEM_ID=?ITEM_ID";
                        break;
                    }
                //case SQLCommand.AssetItem.AssetItemDetailsDelete:
                //    {
                //        query = "DELETE FROM ASSET_ITEM_DETAIL WHERE ITEM_ID=?ITEM_ID AND PROJECT_ID=?PROJECT_ID AND LOCATION_ID=?LOCATION_ID AND SOURCE_FLAG=1";
                //        break;
                //    }
                //case SQLCommand.AssetItem.AssetItemDetailsDeleteByPurchase:
                //    {
                //        query = "DELETE FROM ASSET_ITEM_DETAIL WHERE PURCHASE_ID=?PURCHASE_ID AND SALES_ID=0";
                //        break;
                //    }
                //case SQLCommand.AssetItem.FetchAssetOPDetails:
                //    {
                //        query = "SELECT AID.ITEM_ID,AID.ITEM_DETAIL_ID,ASL.LOCATION_ID, AID.CUSTODIANS_ID, AMOUNT,AID.ASSET_ID FROM ASSET_ITEM_DETAIL AID\n" +
                //                "LEFT JOIN ASSET_ITEM AI ON AID.ITEM_ID=AI.ITEM_ID \n" +
                //                "LEFT JOIN ASSET_STOCK_LOCATION ASL ON AID.LOCATION_ID=ASL.LOCATION_ID WHERE AID.ITEM_ID=?ITEM_ID AND AID.PROJECT_ID=?PROJECT_ID AND AID.LOCATION_ID=?LOCATION_ID AND SOURCE_FLAG=1 ORDER BY AID.SOURCE_FLAG,AID.ITEM_DETAIL_ID ASC";
                //        break;
                //    }
                //case SQLCommand.AssetItem.FetchAssetItemDetailById:
                //    {
                //        query = "SELECT AID.ITEM_ID,ASSET_ID,AID.ITEM_DETAIL_ID,ASL.LOCATION_ID,AMOUNT FROM ASSET_ITEM_DETAIL AID\n" +
                //                "LEFT JOIN ASSET_ITEM AI ON AID.ITEM_ID=AI.ITEM_ID \n" +
                //                "LEFT JOIN ASSET_STOCK_LOCATION ASL ON AID.LOCATION_ID=ASL.LOCATION_ID WHERE AID.ITEM_ID=?ITEM_ID ORDER BY AID.SOURCE_FLAG,AID.ITEM_DETAIL_ID ASC";
                //        break;
                //    }
                case SQLCommand.AssetItem.FetchAssetItemDetailAll:
                    {
                        //query = "SELECT AI.ITEM_ID,\n" +
                        //        "       AI.ASSET_ITEM,\n" +
                        //        "       ASSET_ID,\n" +
                        //        "       SYMBOL AS UNIT,\n" +
                        //        "       AG.ASSET_CLASS_ID,\n" +
                        //        "       AG.ASSET_CLASS,\n" +
                        //        "       ASL.LOCATION_ID,\n" +
                        //        "       ASL.LOCATION,\n" +
                        //        "       MP.PROJECT,\n" +
                        //        "       IOD.IN_OUT_ID,\n" +
                        //        "       AID.AMOUNT,\n" +
                        //        "       CUSTODIAN,\n" +
                        //        "       BLOCK,\n" +
                        //        "       CASE\n" +
                        //        "         WHEN IOM.FLAG = 'OP' THEN\n" +
                        //        "          'O/P'\n" +
                        //        "         ELSE\n" +
                        //        "          CASE\n" +
                        //        "            WHEN IOM.FLAG = 'PU' THEN\n" +
                        //        "             'PURCHASE'\n" +
                        //        "            ELSE\n" +
                        //        "             CASE\n" +
                        //        "               WHEN IOM.FLAG = 'SA' THEN\n" +
                        //        "                'SALES'\n" +
                        //        "               ELSE\n" +
                        //        "                CASE\n" +
                        //        "                  WHEN IOM.FLAG = 'IK' THEN\n" +
                        //        "                   'RECEIVE'\n" +
                        //        "                  ELSE\n" +
                        //        "                   CASE\n" +
                        //        "                     WHEN IOM.FLAG = 'DS' THEN\n" +
                        //        "                      'DISPOSAL'\n" +
                        //        "                   END\n" +
                        //        "                END\n" +
                        //        "             END\n" +
                        //        "          END\n" +
                        //        "       END AS FLAG,\n" +
                        //        "       CASE\n" +
                        //        "         WHEN AID.STATUS = 0 THEN\n" +
                        //        "          'INACTIVE'\n" +
                        //        "         ELSE\n" +
                        //        "          'ACTIVE'\n" +
                        //        "       END AS STATUS\n" +
                        //        "  FROM ASSET_ITEM_DETAIL AID\n" +
                        //        " INNER JOIN ASSET_ITEM AI\n" +
                        //        "    ON AID.ITEM_ID = AI.ITEM_ID\n" +
                        //        " INNER JOIN ASSET_IN_OUT_DETAIL IOD\n" +
                        //        "    ON AI.ITEM_ID = IOD.ITEM_ID\n" +
                        //        " INNER JOIN ASSET_IN_OUT_MASTER IOM\n" +
                        //        "    ON IOD.IN_OUT_ID = IOM.IN_OUT_ID\n" +
                        //        "  LEFT JOIN ASSET_CLASS AG\n" +
                        //        "    ON AI.ASSET_CLASS_ID = AG.ASSET_CLASS_ID\n" +
                        //        " INNER JOIN ASSET_LOCATION ASL\n" +
                        //        "    ON AID.LOCATION_ID = ASL.LOCATION_ID\n" +
                        //        " INNER JOIN MASTER_PROJECT MP\n" +
                        //        "    ON MP.PROJECT_ID = AID.PROJECT_ID\n" +
                        //        " INNER JOIN ASSET_CUSTODIAN AC\n" +
                        //        "    ON AID.CUSTODIAN_ID = AC.CUSTODIAN_ID\n" +
                        //        " INNER JOIN UOM UM\n" +
                        //        "    ON AI.UOM_ID = UM.UOM_ID\n" +
                        //        " INNER JOIN ASSET_BLOCK AB\n" +
                        //        "    ON ASL.BLOCK_ID = AB.BLOCK_ID\n" +
                        //        " GROUP BY AID.ITEM_DETAIL_ID\n" +
                        //        "    ORDER BY AID.ITEM_DETAIL_ID";
                        //sudhakr
                        //query = "SELECT AI.ITEM_ID,\n" +
                        //       "       AI.ASSET_ITEM,\n" +
                        //       "       ASSET_ID,\n" +
                        //       "       SYMBOL AS UNIT,\n" +
                        //       "       AG.ASSET_CLASS_ID,\n" +
                        //       "       AG.ASSET_CLASS,\n" +
                        //       "       ASL.LOCATION_ID,\n" +
                        //       "       ASL.LOCATION,\n" +
                        //       "       MP.PROJECT,\n" +
                        //       "       IOD.IN_OUT_ID,\n" +
                        //       "       AID.AMOUNT,\n" +
                        //       "       CUSTODIAN,\n" +
                        //       "       BLOCK,\n" +
                        //       "       CASE\n" +
                        //       "         WHEN IOM.FLAG = 'OP' THEN\n" +
                        //       "          'O/P'\n" +
                        //       "         ELSE\n" +
                        //       "          CASE\n" +
                        //       "            WHEN IOM.FLAG = 'PU' THEN\n" +
                        //       "             'PURCHASE'\n" +
                        //       "            ELSE\n" +
                        //       "             CASE\n" +
                        //       "               WHEN IOM.FLAG = 'SA' THEN\n" +
                        //       "                'SALES'\n" +
                        //       "               ELSE\n" +
                        //       "                CASE\n" +
                        //       "                  WHEN IOM.FLAG = 'IK' THEN\n" +
                        //       "                   'RECEIVE'\n" +
                        //       "                  ELSE\n" +
                        //       "                   CASE\n" +
                        //       "                     WHEN IOM.FLAG = 'DS' THEN\n" +
                        //       "                      'DISPOSAL'\n" +
                        //       "                   END\n" +
                        //       "                END\n" +
                        //       "             END\n" +
                        //       "          END\n" +
                        //       "       END AS FLAG,\n" +
                        //       "       CASE\n" +
                        //       "         WHEN IOM.FLAG = 'SL' OR AID.STATUS = 0 THEN\n" +
                        //       "          'INACTIVE'\n" +
                        //       "         ELSE\n" +
                        //       "          'ACTIVE'\n" +
                        //       "       END AS STATUS\n" +
                        //       "  FROM ASSET_ITEM_DETAIL AID\n" +
                        //       " INNER JOIN ASSET_ITEM AI\n" +
                        //       "    ON AID.ITEM_ID = AI.ITEM_ID\n" +
                        //       " INNER JOIN ASSET_IN_OUT_DETAIL IOD\n" +
                        //       "    ON AI.ITEM_ID = IOD.ITEM_ID\n" +
                        //       " INNER JOIN ASSET_IN_OUT_MASTER IOM\n" +
                        //       "    ON IOD.IN_OUT_ID = IOM.IN_OUT_ID\n" +
                        //       "  LEFT JOIN ASSET_CLASS AG\n" +
                        //       "    ON AI.ASSET_CLASS_ID = AG.ASSET_CLASS_ID\n" +
                        //       " INNER JOIN ASSET_LOCATION ASL\n" +
                        //       "    ON AID.LOCATION_ID = ASL.LOCATION_ID\n" +
                        //       " INNER JOIN MASTER_PROJECT MP\n" +
                        //       "    ON MP.PROJECT_ID = AID.PROJECT_ID\n" +
                        //       " INNER JOIN ASSET_CUSTODIAN AC\n" +
                        //       "    ON AID.CUSTODIAN_ID = AC.CUSTODIAN_ID\n" +
                        //       " INNER JOIN UOM UM\n" +
                        //       "    ON AI.UOM_ID = UM.UOM_ID\n" +
                        //       " INNER JOIN ASSET_BLOCK AB\n" +
                        //       "    ON ASL.BLOCK_ID = AB.BLOCK_ID\n" +
                        //       "  INNER JOIN ASSET_TRANS AT   ON AT.ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID\n" +
                        //       "  AND IOD.IN_OUT_DETAIL_ID=AT.IN_OUT_DETAIL_ID\n" +
                        //    " GROUP BY AID.ITEM_DETAIL_ID,IOM.IN_OUT_ID\n" +
                        //       "    ORDER BY AID.ITEM_DETAIL_ID";
                        //sudhakr
                        query = @"SELECT
                                                    ITEM_ID,
                                                    ASSET_ITEM,
                                                    ASSET_ID,
                                                    UNIT,
                                                    ASSET_CLASS_ID,
                                                    ASSET_CLASS,
                                                    LOCATION_ID,
                                                    LOCATION,
                                                    PROJECT,
                                                    IN_OUT_ID,
                                                    AMOUNT,
                                                    CUSTODIAN,
                                                    BLOCK,
                                                    FLAG,
                                                    STATUS
                                                FROM
                                                    (SELECT DISTINCT
                                                        AI.ITEM_ID,
                                                            AI.ASSET_ITEM,
                                                            ASSET_ID,
                                                            SYMBOL AS UNIT,
                                                            AG.ASSET_CLASS_ID,
                                                            AG.ASSET_CLASS,
                                                            ASL.LOCATION_ID,
                                                            ASL.LOCATION,
                                                            MP.PROJECT,
                                                            IOD.IN_OUT_ID,
                                                            AID.AMOUNT,
                                                            CUSTODIAN,
                                                            BLOCK,
                                                            CASE
                                                                WHEN IOM.FLAG = 'OP' THEN 'O/P'
                                                                ELSE CASE
                                                                    WHEN IOM.FLAG = 'PU' THEN 'PURCHASE'
                                                                    ELSE CASE
                                                                        WHEN IOM.FLAG = 'SL' THEN 'SALES'
                                                                        ELSE CASE
                                                                            WHEN IOM.FLAG = 'IK' THEN 'RECEIVE'
                                                                            ELSE CASE
                                                                                WHEN IOM.FLAG = 'DS' THEN 'DISPOSAL'
                                                                            END
                                                                        END
                                                                    END
                                                                END
                                                            END AS FLAG,
                                                            CASE
                                                                WHEN IOM.FLAG = 'SL' OR AID.STATUS = 0 THEN 'INACTIVE'
                                                                ELSE 'ACTIVE'
                                                            END AS STATUS,
                                                            AID.ITEM_DETAIL_ID
                                                    FROM
                                                        ASSET_ITEM_DETAIL AID
                                                    INNER JOIN ASSET_ITEM AI ON AID.ITEM_ID = AI.ITEM_ID
                                                    INNER JOIN ASSET_IN_OUT_DETAIL IOD ON AI.ITEM_ID = IOD.ITEM_ID
                                                    INNER JOIN ASSET_IN_OUT_MASTER IOM ON IOD.IN_OUT_ID = IOM.IN_OUT_ID
                                                    LEFT JOIN ASSET_CLASS AG ON AI.ASSET_CLASS_ID = AG.ASSET_CLASS_ID
                                                    INNER JOIN ASSET_LOCATION ASL ON AID.LOCATION_ID = ASL.LOCATION_ID
                                                    INNER JOIN MASTER_PROJECT MP ON MP.PROJECT_ID = AID.PROJECT_ID
                                                    INNER JOIN ASSET_CUSTODIAN AC ON AID.CUSTODIAN_ID = AC.CUSTODIAN_ID
                                                    INNER JOIN UOM UM ON AI.UOM_ID = UM.UOM_ID
                                                    INNER JOIN ASSET_BLOCK AB ON ASL.BLOCK_ID = AB.BLOCK_ID
                                                    INNER JOIN ASSET_TRANS AT ON AT.ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID
                                                        AND IOD.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID
                                                        AND IOD.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID
                                                    WHERE
                                                        IOM.FLAG = 'SL'
                                                    GROUP BY AT.IN_OUT_DETAIL_ID , AT.ITEM_DETAIL_ID UNION ALL SELECT DISTINCT
                                                        AI.ITEM_ID,
                                                            AI.ASSET_ITEM,
                                                            ASSET_ID,
                                                            SYMBOL AS UNIT,
                                                            AG.ASSET_CLASS_ID,
                                                            AG.ASSET_CLASS,
                                                            ASL.LOCATION_ID,
                                                            ASL.LOCATION,
                                                            MP.PROJECT,
                                                            IOD.IN_OUT_ID,
                                                            AID.AMOUNT,
                                                            CUSTODIAN,
                                                            BLOCK,
                                                            CASE
                                                                WHEN IOM.FLAG = 'OP' THEN 'O/P'
                                                                ELSE CASE
                                                                    WHEN IOM.FLAG = 'PU' THEN 'PURCHASE'
                                                                    ELSE CASE
                                                                        WHEN IOM.FLAG = 'SL' THEN 'SALES'
                                                                        ELSE CASE
                                                                            WHEN IOM.FLAG = 'IK' THEN 'RECEIVE'
                                                                            ELSE CASE
                                                                                WHEN IOM.FLAG = 'DS' THEN 'DISPOSAL'
                                                                            END
                                                                        END
                                                                    END
                                                                END
                                                            END AS FLAG,
                                                            CASE
                                                                WHEN IOM.FLAG = 'SL' OR AID.STATUS = 0 THEN 'INACTIVE'
                                                                ELSE 'ACTIVE'
                                                            END AS STATUS,
                                                            AID.ITEM_DETAIL_ID
                                                    FROM
                                                        ASSET_ITEM_DETAIL AID
                                                    INNER JOIN ASSET_ITEM AI ON AID.ITEM_ID = AI.ITEM_ID
                                                    INNER JOIN ASSET_IN_OUT_DETAIL IOD ON AI.ITEM_ID = IOD.ITEM_ID
                                                    INNER JOIN ASSET_IN_OUT_MASTER IOM ON IOD.IN_OUT_ID = IOM.IN_OUT_ID
                                                    LEFT JOIN ASSET_CLASS AG ON AI.ASSET_CLASS_ID = AG.ASSET_CLASS_ID
                                                    INNER JOIN ASSET_LOCATION ASL ON AID.LOCATION_ID = ASL.LOCATION_ID
                                                    INNER JOIN MASTER_PROJECT MP ON MP.PROJECT_ID = AID.PROJECT_ID
                                                    INNER JOIN ASSET_CUSTODIAN AC ON AID.CUSTODIAN_ID = AC.CUSTODIAN_ID
                                                    INNER JOIN UOM UM ON AI.UOM_ID = UM.UOM_ID
                                                    INNER JOIN ASSET_BLOCK AB ON ASL.BLOCK_ID = AB.BLOCK_ID
                                                    INNER JOIN ASSET_TRANS AT ON AT.ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID
                                                        AND IOD.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID
                                                        AND IOD.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID
                                                    WHERE
                                                        IOM.FLAG = 'PU'
                                                    GROUP BY AT.IN_OUT_DETAIL_ID , AT.ITEM_DETAIL_ID UNION ALL SELECT DISTINCT
                                                        AI.ITEM_ID,
                                                            AI.ASSET_ITEM,
                                                            ASSET_ID,
                                                            SYMBOL AS UNIT,
                                                            AG.ASSET_CLASS_ID,
                                                            AG.ASSET_CLASS,
                                                            ASL.LOCATION_ID,
                                                            ASL.LOCATION,
                                                            MP.PROJECT,
                                                            IOD.IN_OUT_ID,
                                                            AID.AMOUNT,
                                                            CUSTODIAN,
                                                            BLOCK,
                                                            CASE
                                                                WHEN IOM.FLAG = 'OP' THEN 'O/P'
                                                                ELSE CASE
                                                                    WHEN IOM.FLAG = 'PU' THEN 'PURCHASE'
                                                                    ELSE CASE
                                                                        WHEN IOM.FLAG = 'SL' THEN 'SALES'
                                                                        ELSE CASE
                                                                            WHEN IOM.FLAG = 'IK' THEN 'RECEIVE'
                                                                            ELSE CASE
                                                                                WHEN IOM.FLAG = 'DS' THEN 'DISPOSAL'
                                                                            END
                                                                        END
                                                                    END
                                                                END
                                                            END AS FLAG,
                                                            CASE
                                                                WHEN IOM.FLAG = 'SL' OR AID.STATUS = 0 THEN 'INACTIVE'
                                                                ELSE 'ACTIVE'
                                                            END AS STATUS,
                                                            AID.ITEM_DETAIL_ID
                                                    FROM
                                                        ASSET_ITEM_DETAIL AID
                                                    INNER JOIN ASSET_ITEM AI ON AID.ITEM_ID = AI.ITEM_ID
                                                    INNER JOIN ASSET_IN_OUT_DETAIL IOD ON AI.ITEM_ID = IOD.ITEM_ID
                                                    INNER JOIN ASSET_IN_OUT_MASTER IOM ON IOD.IN_OUT_ID = IOM.IN_OUT_ID
                                                    LEFT JOIN ASSET_CLASS AG ON AI.ASSET_CLASS_ID = AG.ASSET_CLASS_ID
                                                    INNER JOIN ASSET_LOCATION ASL ON AID.LOCATION_ID = ASL.LOCATION_ID
                                                    INNER JOIN MASTER_PROJECT MP ON MP.PROJECT_ID = AID.PROJECT_ID
                                                    INNER JOIN ASSET_CUSTODIAN AC ON AID.CUSTODIAN_ID = AC.CUSTODIAN_ID
                                                    INNER JOIN UOM UM ON AI.UOM_ID = UM.UOM_ID
                                                    INNER JOIN ASSET_BLOCK AB ON ASL.BLOCK_ID = AB.BLOCK_ID
                                                    INNER JOIN ASSET_TRANS AT ON AT.ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID
                                                        AND IOD.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID
                                                        AND IOD.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID
                                                    WHERE
                                                        IOM.FLAG = 'OP'
                                                    GROUP BY AT.IN_OUT_DETAIL_ID , AT.ITEM_DETAIL_ID UNION ALL SELECT DISTINCT
                                                        AI.ITEM_ID,
                                                            AI.ASSET_ITEM,
                                                            ASSET_ID,
                                                            SYMBOL AS UNIT,
                                                            AG.ASSET_CLASS_ID,
                                                            AG.ASSET_CLASS,
                                                            ASL.LOCATION_ID,
                                                            ASL.LOCATION,
                                                            MP.PROJECT,
                                                            IOD.IN_OUT_ID,
                                                            AID.AMOUNT,
                                                            CUSTODIAN,
                                                            BLOCK,
                                                            CASE
                                                                WHEN IOM.FLAG = 'OP' THEN 'O/P'
                                                                ELSE CASE
                                                                    WHEN IOM.FLAG = 'PU' THEN 'PURCHASE'
                                                                    ELSE CASE
                                                                        WHEN IOM.FLAG = 'SL' THEN 'SALES'
                                                                        ELSE CASE
                                                                            WHEN IOM.FLAG = 'IK' THEN 'RECEIVE'
                                                                            ELSE CASE
                                                                                WHEN IOM.FLAG = 'DS' THEN 'DISPOSAL'
                                                                            END
                                                                        END
                                                                    END
                                                                END
                                                            END AS FLAG,
                                                            CASE
                                                                WHEN IOM.FLAG = 'SL' OR AID.STATUS = 0 THEN 'INACTIVE'
                                                                ELSE 'ACTIVE'
                                                            END AS STATUS,
                                                            AID.ITEM_DETAIL_ID
                                                    FROM
                                                        ASSET_ITEM_DETAIL AID
                                                    INNER JOIN ASSET_ITEM AI ON AID.ITEM_ID = AI.ITEM_ID
                                                    INNER JOIN ASSET_IN_OUT_DETAIL IOD ON AI.ITEM_ID = IOD.ITEM_ID
                                                    INNER JOIN ASSET_IN_OUT_MASTER IOM ON IOD.IN_OUT_ID = IOM.IN_OUT_ID
                                                    LEFT JOIN ASSET_CLASS AG ON AI.ASSET_CLASS_ID = AG.ASSET_CLASS_ID
                                                    INNER JOIN ASSET_LOCATION ASL ON AID.LOCATION_ID = ASL.LOCATION_ID
                                                    INNER JOIN MASTER_PROJECT MP ON MP.PROJECT_ID = AID.PROJECT_ID
                                                    INNER JOIN ASSET_CUSTODIAN AC ON AID.CUSTODIAN_ID = AC.CUSTODIAN_ID
                                                    INNER JOIN UOM UM ON AI.UOM_ID = UM.UOM_ID
                                                    INNER JOIN ASSET_BLOCK AB ON ASL.BLOCK_ID = AB.BLOCK_ID
                                                    INNER JOIN ASSET_TRANS AT ON AT.ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID
                                                        AND IOD.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID
                                                        AND IOD.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID
                                                    WHERE
                                                        IOM.FLAG = 'IK'
                                                    GROUP BY AT.IN_OUT_DETAIL_ID , AT.ITEM_DETAIL_ID UNION ALL SELECT DISTINCT
                                                        AI.ITEM_ID,
                                                            AI.ASSET_ITEM,
                                                            ASSET_ID,
                                                            SYMBOL AS UNIT,
                                                            AG.ASSET_CLASS_ID,
                                                            AG.ASSET_CLASS,
                                                            ASL.LOCATION_ID,
                                                            ASL.LOCATION,
                                                            MP.PROJECT,
                                                            IOD.IN_OUT_ID,
                                                            AID.AMOUNT,
                                                            CUSTODIAN,
                                                            BLOCK,
                                                            CASE
                                                                WHEN IOM.FLAG = 'OP' THEN 'O/P'
                                                                ELSE CASE
                                                                    WHEN IOM.FLAG = 'PU' THEN 'PURCHASE'
                                                                    ELSE CASE
                                                                        WHEN IOM.FLAG = 'SL' THEN 'SALES'
                                                                        ELSE CASE
                                                                            WHEN IOM.FLAG = 'IK' THEN 'RECEIVE'
                                                                            ELSE CASE
                                                                                WHEN IOM.FLAG = 'DS' THEN 'DISPOSAL'
                                                                            END
                                                                        END
                                                                    END
                                                                END
                                                            END AS FLAG,
                                                            CASE
                                                                WHEN IOM.FLAG = 'SL' OR AID.STATUS = 0 THEN 'INACTIVE'
                                                                ELSE 'ACTIVE'
                                                            END AS STATUS,
                                                            AID.ITEM_DETAIL_ID
                                                    FROM
                                                        ASSET_ITEM_DETAIL AID
                                                    INNER JOIN ASSET_ITEM AI ON AID.ITEM_ID = AI.ITEM_ID
                                                    INNER JOIN ASSET_IN_OUT_DETAIL IOD ON AI.ITEM_ID = IOD.ITEM_ID
                                                    INNER JOIN ASSET_IN_OUT_MASTER IOM ON IOD.IN_OUT_ID = IOM.IN_OUT_ID
                                                    LEFT JOIN ASSET_CLASS AG ON AI.ASSET_CLASS_ID = AG.ASSET_CLASS_ID
                                                    INNER JOIN ASSET_LOCATION ASL ON AID.LOCATION_ID = ASL.LOCATION_ID
                                                    INNER JOIN MASTER_PROJECT MP ON MP.PROJECT_ID = AID.PROJECT_ID
                                                    INNER JOIN ASSET_CUSTODIAN AC ON AID.CUSTODIAN_ID = AC.CUSTODIAN_ID
                                                    INNER JOIN UOM UM ON AI.UOM_ID = UM.UOM_ID
                                                    INNER JOIN ASSET_BLOCK AB ON ASL.BLOCK_ID = AB.BLOCK_ID
                                                    INNER JOIN ASSET_TRANS AT ON AT.ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID
                                                        AND IOD.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID
                                                        AND IOD.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID
                                                    WHERE
                                                        IOM.FLAG = 'DS'
                                                    GROUP BY AT.IN_OUT_DETAIL_ID , AT.ITEM_DETAIL_ID) AS T
                                                GROUP BY ITEM_DETAIL_ID , ITEM_ID;";
                        break;
                    }
                case SQLCommand.AssetItem.UpdateAssetDetail:
                    {
                        query = "UPDATE ASSET_ITEM_DETAIL SET\n" +
                                  "       LOCATION_ID     = ?LOCATION_ID,\n" +
                                  "       MANUFACTURER_ID = ?MANUFACTURER_ID,\n" +
                                  "       CUSTODIAN_ID    = ?CUSTODIAN_ID,\n" +
                                  "       PROJECT_ID      = ?PROJECT_ID,\n" +
                                  "       CONDITIONS      = ?CONDITIONS\n" +
                            //   "       DEPRECIATION_NO = ?DEPRECIATION_NO\n" + 
                                  "  WHERE ITEM_DETAIL_ID = ?ITEM_DETAIL_ID;";
                        break;
                    }
                case SQLCommand.AssetItem.FetchUpdateAssetDetails:
                    {
                        //query   = "SELECT ASSET_CLASS,\n" +
                        //          "       ASSET_ITEM,\n" +
                        //          "       AI.IS_INSURANCE,\n" +
                        //          "       ASD.INSURANCE_DETAIL_ID,\n" +
                        //          "       AI.ITEM_ID,\n" +
                        //          "       AID.ASSET_ID,\n" +
                        //          "       AID.ITEM_DETAIL_ID,\n" +
                        //          "       AID.LOCATION_ID,\n" +
                        //          "       AID.MANUFACTURER_ID,\n" +
                        //          "       AID.CUSTODIAN_ID,\n" +
                        //          "       AID.CONDITIONS,\n" +
                        //          "       CASE\n" + 
                        //          "         WHEN AID.STATUS = 0 THEN\n" + 
                        //          "          'INACTIVE'\n" + 
                        //          "         ELSE\n" + 
                        //          "          'ACTIVE'\n" + 
                        //          "       END AS STATUS\n" + 
                        //          "  FROM ASSET_ITEM AI\n" + 
                        //          " INNER JOIN ASSET_CLASS AC\n" + 
                        //          "    ON AC.ASSET_CLASS_ID = AI.ASSET_CLASS_ID\n" +  
                        //          " INNER JOIN ASSET_ITEM_DETAIL AID\n" + 
                        //          "    ON AID.ITEM_ID = AI.ITEM_ID\n" +  
                        //          " INNER JOIN ASSET_IN_OUT_DETAIL IOD\n" + 
                        //          "    ON AI.ITEM_ID = IOD.ITEM_ID\n" +  
                        //          " LEFT JOIN ASSET_LOCATION AL\n" +
                        //          "    ON AL.LOCATION_ID = AID.LOCATION_ID\n" +
                        //          " LEFT JOIN ASSET_CUSTODIAN ACU\n" +
                        //          "    ON ACU.CUSTODIAN_ID = AL.CUSTODIAN_ID\n" +
                        //          " LEFT JOIN ASSET_STOCK_MANUFACTURER AM\n" +
                        //          "    ON AM.MANUFACTURER_ID= AID.MANUFACTURER_ID\n" +
                        //          " LEFT JOIN ASSET_INSURANCE_DETAIL ASD\n" +
                        //          "    ON ASD.ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID\n"+
                        //          "WHERE AID.PROJECT_ID=?PROJECT_ID\n" +
                        //          " GROUP BY AID.ASSET_ID\n" +
                        //          "ORDER BY AC.ASSET_CLASS, ASSET_ITEM, AID.ASSET_ID;";


                        //query = "SELECT PARENT.ASSET_CLASS AS PARENT_CLASS,\n" +
                        //"       AC.ASSET_CLASS,\n" +
                        //"       ASSET_ITEM,\n" +
                        //"       AI.IS_INSURANCE,\n" +
                        //"       ASD.INSURANCE_DETAIL_ID,\n" +
                        //"       AI.ITEM_ID,\n" +
                        //"       AID.ASSET_ID,\n" +
                        //"       AID.ITEM_DETAIL_ID,\n" +
                        //"       AID.LOCATION_ID,\n" +
                        //"       AID.MANUFACTURER_ID,\n" +
                        //"       AID.CUSTODIAN_ID,\n" +
                        //"       AID.CONDITIONS,\n" +
                        //"       CASE\n" +
                        //"         WHEN AID.STATUS = 0 THEN\n" +
                        //"          'INACTIVE'\n" +
                        //"         ELSE\n" +
                        //"          'ACTIVE'\n" +
                        //"       END AS STATUS\n" +
                        //"  FROM ASSET_ITEM AI\n" +
                        //" INNER JOIN ASSET_CLASS AC\n" +
                        //"    ON AC.ASSET_CLASS_ID = AI.ASSET_CLASS_ID\n" +
                        //" INNER JOIN ASSET_ITEM_DETAIL AID\n" +
                        //"    ON AID.ITEM_ID = AI.ITEM_ID\n" +
                        //" INNER JOIN ASSET_IN_OUT_DETAIL IOD\n" +
                        //"    ON AI.ITEM_ID = IOD.ITEM_ID\n" +
                        //"  LEFT JOIN ASSET_LOCATION AL\n" +
                        //"    ON AL.LOCATION_ID = AID.LOCATION_ID\n" +
                        //"  LEFT JOIN ASSET_CUSTODIAN ACU\n" +
                        //"    ON ACU.CUSTODIAN_ID = AL.CUSTODIAN_ID\n" +
                        //"  LEFT JOIN ASSET_STOCK_MANUFACTURER AM\n" +
                        //"    ON AM.MANUFACTURER_ID = AID.MANUFACTURER_ID\n" +
                        //"  LEFT JOIN ASSET_INSURANCE_DETAIL ASD\n" +
                        //"    ON ASD.ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID\n" +
                        //"  LEFT JOIN ASSET_CLASS PARENT\n" +
                        //"    ON AC.PARENT_CLASS_ID = PARENT.ASSET_CLASS_ID\n" +
                        //" WHERE AID.PROJECT_ID =?PROJECT_ID\n" +
                        //" GROUP BY AID.ASSET_ID\n" +
                        //" ORDER BY AC.ASSET_CLASS, ASSET_ITEM, AID.ITEM_DETAIL_ID ASC ;";
                        query = "SELECT IF(PARENT.ASSET_CLASS,AC.ASSET_CLASS,PARENT.ASSET_CLASS) AS PARENT_CLASS,\n" +
                                        "       AC.ASSET_CLASS,\n" +
                                        "       ASSET_ITEM,\n" +
                                        "       AI.IS_INSURANCE,\n" +
                                        "       ASD.INSURANCE_DETAIL_ID,\n" +
                                        "       AI.ITEM_ID,\n" +
                                        "       AID.ASSET_ID,\n" +
                                        "       AID.ITEM_DETAIL_ID,\n" +
                                        "       AID.LOCATION_ID,\n" +
                                        "       AID.MANUFACTURER_ID,\n" +
                                        "       AID.CUSTODIAN_ID,\n" +
                                        "       AID.CONDITIONS,\n" +
                                        "       CASE\n" +
                                        "         WHEN AID.ITEM_DETAIL_ID = T.SOLD_ITEM_DETAIL_ID THEN\n" +
                                        "          0\n" +
                                        "         ELSE\n" +
                                        "          1\n" +
                                        "       END AS FLAG,\n" +
                                        "       CASE\n" +
                                        "         WHEN AID.STATUS = 0 THEN\n" +
                                        "          'INACTIVE'\n" +
                                        "         ELSE\n" +
                                        "          'ACTIVE'\n" +
                                        "       END AS STATUS\n" +
                                        "  FROM ASSET_ITEM AI\n" +
                                        " INNER JOIN ASSET_CLASS AC\n" +
                                        "    ON AC.ASSET_CLASS_ID = AI.ASSET_CLASS_ID\n" +
                                        " INNER JOIN ASSET_ITEM_DETAIL AID\n" +
                                        "    ON AID.ITEM_ID = AI.ITEM_ID\n" +
                                        " INNER JOIN ASSET_IN_OUT_DETAIL IOD\n" +
                                        "    ON AI.ITEM_ID = IOD.ITEM_ID\n" +
                                        "  LEFT JOIN ASSET_LOCATION AL\n" +
                                        "    ON AL.LOCATION_ID = AID.LOCATION_ID\n" +
                                        "  LEFT JOIN ASSET_CUSTODIAN ACU\n" +
                                        "    ON ACU.CUSTODIAN_ID = AL.CUSTODIAN_ID\n" +
                                        "  LEFT JOIN ASSET_STOCK_MANUFACTURER AM\n" +
                                        "    ON AM.MANUFACTURER_ID = AID.MANUFACTURER_ID\n" +
                                        "  LEFT JOIN (SELECT AIM.IN_OUT_ID,\n" +
                                        "                    AID.IN_OUT_DETAIL_ID,\n" +
                                        "                    AT.ITEM_DETAIL_ID AS SOLD_ITEM_DETAIL_ID\n" +
                                        "               FROM ASSET_IN_OUT_MASTER AIM\n" +
                                        "              INNER JOIN ASSET_IN_OUT_DETAIL AID\n" +
                                        "                 ON AIM.IN_OUT_ID = AID.IN_OUT_ID\n" +
                                        "              INNER JOIN ASSET_TRANS AT\n" +
                                        "                 ON AID.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID\n" +
                                        "              WHERE FLAG IN ('SL', 'DN', 'DS')) AS T\n" +
                                        "    ON T.SOLD_ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID\n" +
                                        "  LEFT JOIN ASSET_INSURANCE_DETAIL ASD\n" +
                                        "    ON ASD.ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID\n" +
                                        "  LEFT JOIN ASSET_CLASS PARENT\n" +
                                        "    ON AC.PARENT_CLASS_ID = PARENT.ASSET_CLASS_ID\n" +
                                        " WHERE AID.PROJECT_ID = ?PROJECT_ID\n" +
                                        " GROUP BY AID.ASSET_ID\n" +
                                        " ORDER BY AC.ASSET_CLASS, ASSET_ITEM, AID.ITEM_DETAIL_ID ASC";

                        break;
                    }
                case SQLCommand.AssetItem.FetchAssetItemDetailByLocation:
                    {
                        query = "SELECT CONCAT(AI.ASSET_ITEM, ' - ',  ASSET_CLASS) AS ASSET_ITEM, AI.ITEM_ID,ACCOUNT_LEDGER_ID\n" +
                                "  FROM ASSET_ITEM AI\n" +
                                " INNER JOIN ASSET_CLASS AC\n" +
                                "    ON AI.ASSET_CLASS_ID = AC.ASSET_CLASS_ID\n" +
                                " INNER JOIN ASSET_ITEM_DETAIL AID\n" +
                                "    ON AI.ITEM_ID = AID.ITEM_ID \n" +
                            //   " INNER JOIN ASSET_PROJECT_LOCATION APL\n" +
                            //    "    ON AID.PROJECT_ID = APL.PROJECT_ID\n" +
                            //    "   AND AID.LOCATION_ID = APL.LOCATION_ID\n" +
                                " WHERE AID.PROJECT_ID IN (?PROJECT_ID)\n" +
                            //       " { AND AID.LOCATION_ID IN (?LOCATION_ID) }\n" +
                                " GROUP BY AI.ITEM_ID;";
                        break;
                    }
                case SQLCommand.AssetItem.FetchAll:
                    {
                        //query = "SELECT AI.ITEM_ID,\n" +
                        //        "       AG.ASSET_CLASS_ID,ASSET_ITEM AS ASSET_NAME,\n" +
                        //        "       AG.ASSET_CLASS AS ASSET_GROUP,\n" +
                        //        "       CONCAT(AG.ASSET_CLASS, ' - ', ASSET_ITEM) AS ASSET_ITEM,\n" +
                        //        "       AU.SYMBOL,\n" +
                        //        "       AI.ASSET_ITEM,\n" +
                        //        "       CONCAT(PREFIX,SUFFIX) AS PREFIXSUFFIX, \n" +
                        //        "       PREFIX,\n" +
                        //        "       SUFFIX,\n" +
                        //        "       RETENTION_YRS,\n" +
                        //        "       DEPRECIATION_YRS,\n" +
                        //        "       IS_INSURANCE,\n" +
                        //        "       IS_AMC,\n" +
                        //        "       STARTING_NO,\n" +
                        //    //"       ML.LEDGER_ID AS DEPRECIATION_LEDGER_ID,\n" +
                        //    //"       DP.LEDGER_ID AS DISPOSAL_LEDGER_ID,\n" +
                        //        "       AL.LEDGER_ID AS ACCOUNT_LEDGER_ID,\n" +
                        //    //"       ML.LEDGER_NAME AS DEPRECIATION_LEDGER,\n" +
                        //    //"       DP.LEDGER_NAME AS DISPOSAL_LEDGER,\n" +
                        //        "       AL.LEDGER_NAME AS ACCOUNT_LEDGER,\n" +
                        //        "       AID.LOCATION_ID\n" +
                        //        "  FROM ASSET_ITEM AI\n" +
                        //        "  LEFT JOIN ASSET_ITEM_DETAIL AID\n" +
                        //        "    ON AI.ITEM_ID = AID.ITEM_ID\n" +
                        //        "  LEFT JOIN ASSET_LOCATION SL\n" +
                        //        "    ON AID.LOCATION_ID = SL.LOCATION_ID\n" +
                        //        " INNER JOIN ASSET_CLASS AG\n" +
                        //        "    ON AG.ASSET_CLASS_ID = AI.ASSET_CLASS_ID\n" +
                        //        " INNER JOIN UOM AU\n" +
                        //        "    ON AU.UOM_ID = AI.UOM_ID\n" +
                        //    //" INNER JOIN MASTER_LEDGER ML\n" +
                        //    //"    ON ML.LEDGER_ID = AI.DEPRECIATION_LEDGER_ID\n" +
                        //    //" INNER JOIN MASTER_LEDGER DP\n" +
                        //    //"    ON DP.LEDGER_ID = AI.DISPOSAL_LEDGER_ID\n" +
                        //        " INNER JOIN MASTER_LEDGER AL\n" +
                        //        "    ON AL.LEDGER_ID = AI.ACCOUNT_LEDGER_ID\n" +
                        //        " GROUP BY AI.ITEM_ID, AG.ASSET_CLASS_ID;";


                        query = "SELECT AI.ITEM_ID,\n" +
                        "       AG.ASSET_CLASS_ID,\n" +
                        "       IF(PARENT.ASSET_CLASS='Primary', AG.ASSET_CLASS,PARENT.ASSET_CLASS)  AS PARENT_CLASS,\n" +
                        "       AG.ASSET_CLASS AS ASSET_GROUP,\n" +
                        "       AI.ASSET_ITEM AS ASSET_NAME,\n" +
                        "       -- AG.ASSET_CLASS,\n" +
                        "       CONCAT(ASSET_ITEM, ' - ', AG.ASSET_CLASS) AS ASSET_ITEM,\n" +
                        "       AU.SYMBOL,\n" +
                        "       -- AI.ASSET_ITEM,\n" +
                        "       CONCAT(PREFIX, SUFFIX) AS PREFIXSUFFIX,\n" +
                        "       PREFIX,\n" +
                        "       SUFFIX,\n" +
                        "       RETENTION_YRS,\n" +
                        "       DEPRECIATION_YRS,\n" +
                        "       IS_INSURANCE,\n" +
                        "       IS_AMC,\n" +
                        "       STARTING_NO,\n" +
                        "       WIDTH,\n" +
                        "       --       ML.LEDGER_ID AS DEPRECIATION_LEDGER_ID,\n" +
                        "       --       DP.LEDGER_ID AS DISPOSAL_LEDGER_ID,\n" +
                        "       AL.LEDGER_ID AS ACCOUNT_LEDGER_ID,AL.LEDGER_ID,\n" +
                        "       --       ML.LEDGER_NAME AS DEPRECIATION_LEDGER,\n" +
                        "       --       DP.LEDGER_NAME AS DISPOSAL_LEDGER,\n" +
                        "       AL.LEDGER_NAME  AS ACCOUNT_LEDGER,\n" +
                        "       AID.LOCATION_ID\n" +
                        "  FROM ASSET_ITEM AI\n" +
                        "  LEFT JOIN ASSET_ITEM_DETAIL AID\n" +
                        "    ON AI.ITEM_ID = AID.ITEM_ID\n" +
                        "  LEFT JOIN ASSET_LOCATION SL\n" +
                        "    ON AID.LOCATION_ID = SL.LOCATION_ID\n" +
                        " INNER JOIN ASSET_CLASS AG\n" +
                        "    ON AG.ASSET_CLASS_ID = AI.ASSET_CLASS_ID\n" +
                        " INNER JOIN UOM AU\n" +
                        "    ON AU.UOM_ID = AI.UOM_ID\n" +
                        "-- INNER JOIN MASTER_LEDGER ML\n" +
                        "--    ON ML.LEDGER_ID = AI.DEPRECIATION_LEDGER_ID\n" +
                        "-- INNER JOIN MASTER_LEDGER DP\n" +
                        "--    ON DP.LEDGER_ID = AI.DISPOSAL_LEDGER_ID\n" +
                        " INNER JOIN MASTER_LEDGER AL\n" +
                        "    ON AL.LEDGER_ID = AI.ACCOUNT_LEDGER_ID\n" +
                        "  INNER JOIN ASSET_CLASS PARENT\n" +
                        "    ON AG.PARENT_CLASS_ID = PARENT.ASSET_CLASS_ID\n" +
                        " GROUP BY AI.ITEM_ID, AG.ASSET_CLASS_ID\n" +
                        " ORDER BY  AG.ASSET_CLASS,AI.ASSET_ITEM ASC;";

                        break;
                    }
                case SQLCommand.AssetItem.FetchAllAssetDetails:
                    {
                        query = "SELECT AI.ITEM_ID,\n" +
                                        "       AG.GROUP_ID,\n" +
                                        "       AG.GROUP_NAME AS ASSET_GROUP,\n" +
                                        "       ASL.LOCATION_ID,\n" +
                                         "      ASL.LOCATION_NAME,\n" +
                                         "      AID.AMOUNT,\n" +
                                        "       AU.SYMBOL AS UNIT,\n" +
                                        "       ITEM_KIND,\n" +
                                        "       AI.ASSET_NAME,\n" +
                                        "       PREFIX,\n" +
                                        "       SUFFIX,\n" +
                                        "       STARTING_NO,\n" +
                                        "       WIDTH,\n" +
                                        "       COUNT(QUANTITY) AS QUANTITY,\n" +
                                        "       RUNNING_NUMBER,\n" +
                                        "       ML.LEDGER_ID AS DEPRECIATION_LEDGER_ID,\n" +
                                        "       DP.LEDGER_ID AS DISPOSAL_LEDGER_ID,\n" +
                                        "       AL.LEDGER_ID AS ACCOUNT_LEDGER_ID,\n" +
                                        "       ML.LEDGER_NAME AS DEPRECIATION_LEDGER,\n" +
                                        "       DP.LEDGER_NAME AS DISPOSAL_LEDGER,\n" +
                                        "       AL.LEDGER_NAME AS ACCOUNT_LEDGER\n" +
                                        "  FROM ASSET_ITEM AI\n" +
                                        "  LEFT JOIN ASSET_ITEM_DETAIL AID\n" +
                                        "    ON AI.ITEM_ID = AID.ITEM_ID\n" +
                                        "  LEFT JOIN ASSET_STOCK_LOCATION ASL\n" +
                                        "  ON ASL.LOCATION_ID = AID.LOCATION_ID\n" +
                                        " INNER JOIN ASSET_GROUP AG\n" +
                                        "    ON AG.GROUP_ID = AI.ASSET_GROUP_ID\n" +
                                        " INNER JOIN UOM AU\n" +
                                        "    ON AU.UNIT_ID = AI.UNIT_ID\n" +
                                        " INNER JOIN MASTER_LEDGER ML\n" +
                                        "    ON ML.LEDGER_ID = AI.DEPRECIATION_LEDGER_ID\n" +
                                        " INNER JOIN MASTER_LEDGER DP\n" +
                                        "    ON DP.LEDGER_ID = AI.DISPOSAL_LEDGER_ID\n" +
                                        " INNER JOIN MASTER_LEDGER AL\n" +
                                        "    ON AL.LEDGER_ID = AI.ACCOUNT_LEDGER_ID\n" +
                                        " GROUP BY AI.ITEM_ID";
                        break;
                    }
                case SQLCommand.AssetItem.Update:
                    {
                        query = "UPDATE ASSET_ITEM\n" +
                                "   SET ASSET_CLASS_ID         = ?ASSET_CLASS_ID,\n" +
                                "       DEPRECIATION_LEDGER_ID = ?DEPRECIATION_LEDGER_ID,\n" +
                                "       DISPOSAL_LEDGER_ID     = ?DISPOSAL_LEDGER_ID,\n" +
                                "       ACCOUNT_LEDGER_ID      = ?ACCOUNT_LEDGER_ID,\n" +
                                "       ASSET_ITEM             = ?ASSET_ITEM,\n" +
                                "       UOM_ID                = ?UOM_ID,\n" +
                                "       PREFIX                 = ?PREFIX,\n" +
                                "       SUFFIX                 = ?SUFFIX,\n" +
                                "       RETENTION_YRS          = ?RETENTION_YRS,\n" +
                                "       DEPRECIATION_YRS       = ?DEPRECIATION_YRS,\n" +
                                "       IS_INSURANCE              = ?IS_INSURANCE,\n" +
                                "       IS_AMC                    = ?IS_AMC,\n" +
                                "       IS_ASSET_DEPRECIATION    =?IS_ASSET_DEPRECIATION,\n" +
                                "       STARTING_NO            = ?STARTING_NO,WIDTH=?WIDTH,\n" +
                                "       ASSET_MODE             =?ASSET_MODE,\n" +
                                "       DEPRECIATION_NO    =?DEPRECIATION_NO\n" +
                                " WHERE ITEM_ID = ?ITEM_ID;";
                        break;
                    }
                case SQLCommand.AssetItem.Fetch:
                    {
                        query = "SELECT ITEM_ID,\n" +
                                    "       ML.LEDGER_NAME AS LEDGER,\n" +
                                    "       AI.ASSET_CLASS_ID,\n" +
                                    "       AC.ASSET_CLASS,\n" +
                                    "       DEPRECIATION_LEDGER_ID,\n" +
                                    "       DISPOSAL_LEDGER_ID,\n" +
                                    "       ACCOUNT_LEDGER_ID,\n" +
                                    "       ASSET_ITEM,\n" +
                                    "       AI.UOM_ID,\n" +
                                    "       SYMBOL,\n" +
                                    "       PREFIX,\n" +
                                    "       SUFFIX,\n" +
                                    "       RETENTION_YRS,\n" +
                                    "       DEPRECIATION_YRS,\n" +
                                    "       IS_INSURANCE,\n" +
                                    "       IS_AMC,\n" +
                                    "       IS_ASSET_DEPRECIATION,\n" +
                                    "       ASSET_MODE,\n" +
                                    "       STARTING_NO,WIDTH,ASSET_MODE,DEPRECIATION_NO\n" +
                                    "  FROM ASSET_ITEM AI\n" +
                                    " INNER JOIN ASSET_CLASS AC\n" +
                                    "    ON AI.ASSET_CLASS_ID=AC.ASSET_CLASS_ID\n" +
                                    " INNER JOIN MASTER_LEDGER ML\n" +
                                    "    ON ML.LEDGER_ID = AI.ACCOUNT_LEDGER_ID\n" +
                                    " LEFT JOIN UOM\n" +
                                    "    ON UOM.UOM_ID=AI.UOM_ID\n" +
                                    " WHERE ITEM_ID = ?ITEM_ID";
                        break;
                    }

                case SQLCommand.AssetItem.FetchItemDetailforTransfer:
                    {
                        query = "SELECT ITEM_ID,\n" +
                                "       AI.ASSET_CLASS_ID,\n" +
                                "       CONCAT(ASSET_ITEM, ' - ', ASSET_CLASS) AS ASSET_NAME\n" +
                                "  FROM ASSET_ITEM AI\n" +
                                " INNER JOIN ASSET_CLASS AG\n" +
                                "    ON AI.ASSET_CLASS_ID = AG.ASSET_CLASS_ID";
                        break;
                    }
                //case SQLCommand.AssetItem.FetchAssetItemByGroup:
                //    {
                //        query = "SELECT AI.ITEM_ID,\n" +
                //                "AI.ASSET_NAME,\n" +
                //                "AG.GROUP_ID,\n" +
                //                "AG.GROUP_NAME,\n" +
                //                "AG.DEP_PERCENTAGE FROM ASSET_ITEM AI INNER JOIN\n" +
                //                "ASSET_GROUP AG ON AG.GROUP_ID = AI.ASSET_GROUP_ID WHERE AG.GROUP_ID =?GROUP_ID";
                //        break;
                //    }
                case SQLCommand.AssetItem.FetchAssetIdByItem:
                    {
                        query = "SELECT AID.ITEM_ID,\n" +
                                "AID.AMOUNT,\n" +
                                "AID.ASSET_ID,\n" +
                                "AID.LOCATION_ID,\n" +
                                "AG.DEP_PERCENTAGE,\n" +
                                "STATUS,\n" +
                                "AID.PURCHASE_ID,\n" +
                                "APM.PURCHASE_DATE AS 'PURCHASED_ON' FROM ASSET_ITEM_DETAIL AID\n" +
                                "LEFT JOIN ASSET_ITEM AI ON AI.ITEM_ID = AID.ITEM_ID\n" +
                                "LEFT JOIN ASSET_PURCHASE_MASTER APM ON AID.PURCHASE_ID = APM.PURCHASE_ID\n" +
                                "LEFT JOIN ASSET_GROUP AG ON AG.GROUP_ID = AI.ASSET_GROUP_ID\n" +
                                "WHERE AID.STATUS=1 AND AID.ITEM_ID =?ITEM_ID";
                        break;

                    }
                //case SQLCommand.AssetItem.FetchActiveAssetItem:
                //    {
                //        query = "SELECT AID.ITEM_ID,\n" +
                //                "AID.AMOUNT,\n" +
                //                "AID.ASSET_ID,\n" +
                //                "AID.LOCATION_ID,\n" +
                //                "AG.DEP_PERCENTAGE,\n" +
                //                "STATUS,\n" +
                //                "AID.PURCHASE_ID,\n" +
                //                "APM.PURCHASE_DATE AS 'PURCHASED_ON', 0 AS 'SELECT' FROM ASSET_ITEM_DETAIL AID\n" +
                //                "LEFT JOIN ASSET_ITEM AI ON AI.ITEM_ID = AID.ITEM_ID\n" +
                //                "LEFT JOIN ASSET_PURCHASE_MASTER APM ON AID.PURCHASE_ID = APM.PURCHASE_ID\n" +
                //                "LEFT JOIN ASSET_GROUP AG ON AG.GROUP_ID = AI.ASSET_GROUP_ID\n" +
                //                "WHERE STATUS='1'";
                //        //  " WHERE AID.ASSET_ID NOT IN(SELECT ASSET_ID FROM ASSET_SALES_DETAIL)";
                //        break;
                //    }
                //case SQLCommand.AssetItem.FetchAssetIdDetailsAtEdit:
                //    {
                //        query = "SELECT AID.ITEM_ID,\n" +
                //                    "       AID.AMOUNT,\n" +
                //                    "       AID.ASSET_ID,\n" +
                //                    "       AID.LOCATION_ID,\n" +
                //                    "       AG.DEP_PERCENTAGE,\n" +
                //                    "       STATUS,\n" +
                //                    "       AID.PURCHASE_ID,\n" +
                //                    "       APM.PURCHASE_DATE AS 'PURCHASED_ON', 0 AS 'SELECT'\n" +
                //                    "  FROM ASSET_ITEM_DETAIL AID\n" +
                //                    "  LEFT JOIN ASSET_ITEM AI\n" +
                //                    "    ON AI.ITEM_ID = AID.ITEM_ID\n" +
                //                    "  LEFT JOIN ASSET_PURCHASE_MASTER APM\n" +
                //                    "    ON AID.PURCHASE_ID = APM.PURCHASE_ID\n" +
                //                    "  LEFT JOIN ASSET_GROUP AG\n" +
                //                    "    ON AG.GROUP_ID = AI.ASSET_GROUP_ID\n" +
                //                    " WHERE AID.ASSET_ID NOT IN (SELECT ASSET_ID FROM ASSET_SALES_DETAIL)\n" +
                //                    "\n" +
                //                    "UNION ALL\n" +
                //                    "\n" +
                //                    "SELECT ASM.ITEM_ID,\n" +
                //                    "       ASM.AMOUNT,\n" +
                //                    "       ASM.ASSET_ID,\n" +
                //                    "       ASM.LOCATION_ID,\n" +
                //                    "       0               AS DEP_PERCENTAGE,\n" +
                //                    "       0               AS STATUS,\n" +
                //                    "       0               AS PURCHASE_ID,\n" +
                //                    "       0               AS 'PURCHASED_ON',\n" +
                //                    "       0               AS 'SELECT'\n" +
                //                    "  FROM ASSET_SALES_DETAIL ASM\n" +
                //                    " WHERE ASM.SALES_ID = ?SALES_ID";
                //        break;
                //    }

                //case SQLCommand.AssetItem.FetchAssetItem:
                //    {
                //        query = "SELECT ASSET_ITEM,\n" +
                //                 "       ITEM_ID,\n" +
                //                 "       CONCAT(ASSET_ITEM, ' - ', ASSET_CLASS) AS ASSET_ITEM1\n" +
                //                 "  FROM ASSET_ITEM AI\n" +
                //                 " INNER JOIN ASSET_CLASS AC\n" +
                //                 "    ON AI.ASSET_CLASS_ID = AC.ASSET_CLASS_ID";
                //        //query = "SELECT AID.ITEM_ID,\n" +
                //        //            " AID.AMOUNT,\n" +
                //        //            " AID.ASSET_ID,\n" +
                //        //            " AID.LOCATION_ID,\n" +
                //        //            " AG.DEP_PERCENTAGE,\n" +
                //        //            " STATUS,\n" +
                //        //            " AID.PURCHASE_ID,\n" +
                //        //            " APM.PURCHASE_DATE AS 'PURCHASED_ON' FROM ASSET_ITEM_DETAIL AID\n" +
                //        //            " LEFT JOIN ASSET_ITEM AI ON AI.ITEM_ID = AID.ITEM_ID\n" +
                //        //            " LEFT JOIN ASSET_PURCHASE_MASTER APM ON AID.PURCHASE_ID = APM.PURCHASE_ID\n" +
                //        //            " LEFT JOIN ASSET_GROUP AG ON AG.GROUP_ID = AI.ASSET_GROUP_ID";

                //        break;
                //    }
                //case SQLCommand.AssetItem.FetchAssetCategoryItemDetail:
                //    {
                //        query = "SELECT AI.ITEM_ID,\n" +
                //                "       AI.ASSET_NAME,\n" +
                //                "       ASSET_ID,\n" +
                //                "       AG.GROUP_ID,\n" +
                //                "       AG.GROUP_NAME,\n" +
                //                "       AC.NAME AS CATEGORY_NAME,\n" +
                //                "       ASL.LOCATION_ID,\n" +
                //                "       ASL.LOCATION_NAME,\n" +
                //                "       PURCHASE_ID,\n" +
                //                "       SALES_ID,\n" +
                //                "       AMOUNT,\n" +
                //                "       STATUS,\n" +
                //                "       USEFUL_LIFE,\n" +
                //                "       SALVAGE_LIFE,\n" +
                //                "       CASE\n" +
                //                "         WHEN SOURCE_FLAG = 1 THEN\n" +
                //                "          'O/B'\n" +
                //                "         ELSE\n" +
                //                "          CASE\n" +
                //                "            WHEN SOURCE_FLAG = 2 THEN\n" +
                //                "             'Purchase'\n" +
                //                "            ELSE\n" +
                //                "             CASE\n" +
                //                "               WHEN SOURCE_FLAG = 3 THEN\n" +
                //                "                'Sales'\n" +
                //                "              ELSE\n" +
                //                "                CASE\n" +
                //                "                  WHEN SOURCE_FLAG = 4 THEN\n" +
                //                "                  'Receive'\n" +
                //                "                 END\n" +
                //                "             END\n" +
                //                "          END\n" +
                //                "       END AS SOURCE_FLAG\n" +
                //                "  FROM ASSET_ITEM_DETAIL AID\n" +
                //                " INNER JOIN ASSET_ITEM AI\n" +
                //                "    ON AID.ITEM_ID = AI.ITEM_ID\n" +
                //                "  LEFT JOIN ASSET_GROUP AG\n" +
                //                "    ON AI.ASSET_GROUP_ID = AG.GROUP_ID\n" +
                //                "  LEFT JOIN ASSET_CATEGORY AC\n" +
                //                "    ON AI.CATEGORY_ID = AC.CATEGORY_ID\n" +
                //                " INNER JOIN ASSET_STOCK_LOCATION ASL\n" +
                //                "    ON AID.LOCATION_ID = ASL.LOCATION_ID\n" +
                //                " WHERE AC.CATEGORY_ID IN (?CATEGORY_ID)\n" +
                //                " ORDER BY SOURCE_FLAG,AI.ITEM_ID";
                //        break;
                //    }
                //case SQLCommand.AssetItem.FetchItemDetailbyAssetId:
                //    {
                //        query = "SELECT AI.ITEM_ID,\n" +
                //                "SUM(AD.AMOUNT) AS AMOUNT,\n" +
                //                "       AD.ASSET_ID,\n" +
                //                "       AI.ASSET_NAME\n" +
                //                "  FROM ASSET_ITEM AI\n" +
                //                " INNER JOIN ASSET_ITEM_DETAIL AD\n" +
                //                "    ON AD.ITEM_ID = AI.ITEM_ID\n" +
                //                " WHERE FIND_IN_SET(AD.ASSET_ID,?ASSET_ID) GROUP BY AI.ITEM_ID";

                //        break;
                //    }
                case SQLCommand.AssetItem.FetchAssetIdDetail:
                    {
                        query = "SELECT ASSET_ID,ITEM_ID FROM ASSET_ITEM_DETAIL WHERE LOCATION_ID=?LOCATION_ID AND STATUS=1";
                        break;
                    }
                case SQLCommand.AssetItem.UpdateAccountLedgerToItem:
                    {
                        query = "UPDATE ASSET_ITEM SET ACCOUNT_LEDGER_ID = ?LEDGER_ID WHERE ITEM_ID = ?ITEM_ID;";
                        break;
                    }

                //case SQLCommand.AssetItem.UpdateAssetItemLocationById:
                //    {
                //        query = "UPDATE ASSET_ITEM_DETAIL\n" +
                //                "   SET LOCATION_ID = ?LOCATION_ID\n" +
                //                " WHERE ASSET_ID = ?ASSET_ID\n" +
                //                "   AND ITEM_ID = ?ITEM_ID";
                //        break;
                //    }
                //case SQLCommand.AssetItem.UpdateAssetItemDetailBySales:
                //    {
                //        //Update Asset Item Detail by New Sales
                //        query = "UPDATE ASSET_ITEM_DETAIL\n" +
                //                "   SET SALES_ID =?SALES_ID,\n" +
                //                "SOURCE_FLAG =?SOURCE_FLAG,\n" +
                //                "STATUS =?STATUS\n" +
                //                " WHERE ASSET_ID=?ASSET_ID";
                //        break;
                //    }
                //case SQLCommand.AssetItem.UpdateAssetItemDetailBySalesId:
                //    {
                //        //Update Asset Item Detail by Old Sales (Revert back)
                //        query = "UPDATE ASSET_ITEM_DETAIL\n" +
                //                "   SET SALES_ID =0,\n" +
                //                "SOURCE_FLAG =?SOURCE_FLAG,\n" +
                //                "STATUS =?STATUS\n" +
                //                " WHERE ASSET_ID=?ASSET_ID AND SALES_ID=?SALES_ID";
                //        break;
                //    }

                //case SQLCommand.AssetItem.DeleteAssetItems:
                //    {
                //        query = "DELETE FROM ASSET_ITEM";
                //        break;
                //    }
                //case SQLCommand.AssetItem.FetchAssetItemNameForReport:
                //    {
                //        query = "SELECT ASSET_NAME AS ITEM_NAME,AI.ITEM_ID\n" +
                //                "  FROM ASSET_ITEM AI\n" +
                //                " INNER JOIN ASSET_ITEM_DETAIL AID\n" +
                //                "    ON AID.ITEM_ID = AI.ITEM_ID\n" +
                //                " WHERE SOURCE_FLAG IN (2)\n" +
                //                " GROUP BY ITEM_NAME \n" +
                //                " ORDER BY ASSET_NAME;";
                //        break;
                //    }
                //case SQLCommand.AssetItem.FetchAssetSalesNameForReport:
                //    {
                //        query = "SELECT ASSET_NAME AS ITEM_NAME,AI.ITEM_ID\n" +
                //                "  FROM ASSET_ITEM AI\n" +
                //                " INNER JOIN ASSET_ITEM_DETAIL AID\n" +
                //                "    ON AID.ITEM_ID = AI.ITEM_ID\n" +
                //                " WHERE SOURCE_FLAG IN (3)\n" +
                //                " GROUP BY ITEM_NAME \n" +
                //                " ORDER BY ASSET_NAME;";
                //        break;
                //    }
                //case SQLCommand.AssetItem.FetchAssetReceiveNameForReport:
                //    {
                //        query = "SELECT ASSET_NAME AS ITEM_NAME,AI.ITEM_ID\n" +
                //                "  FROM ASSET_ITEM AI\n" +
                //                " INNER JOIN ASSET_ITEM_DETAIL AID\n" +
                //                "    ON AID.ITEM_ID = AI.ITEM_ID\n" +
                //                " WHERE SOURCE_FLAG IN (4)\n" +
                //                " GROUP BY ITEM_NAME \n" +
                //                " ORDER BY ASSET_NAME;";
                //        break;
                //    }
                //case SQLCommand.AssetItem.FetchAssetDisposalNameForReport:
                //    {
                //        query = "SELECT ASSET_NAME AS ITEM_NAME,AI.ITEM_ID\n" +
                //                "  FROM ASSET_ITEM AI\n" +
                //                " INNER JOIN ASSET_ITEM_DETAIL AID\n" +
                //                "    ON AID.ITEM_ID = AI.ITEM_ID\n" +
                //                " WHERE SOURCE_FLAG IN (5)\n" +
                //                " GROUP BY ITEM_NAME \n" +
                //                " ORDER BY ASSET_NAME;";
                //        break;
                //    }
                //case SQLCommand.AssetItem.FetchAssetRegisterSummaryReport:
                //    {
                //        query = "SELECT AID.ITEM_ID, ASSET_NAME AS ITEM_NAME\n" +
                //                "  FROM ASSET_ITEM AI\n" +
                //                " INNER JOIN ASSET_ITEM_DETAIL AID\n" +
                //                "    ON AID.ITEM_ID = AI.ITEM_ID\n" +
                //                " WHERE AID.STATUS = '1'\n" +
                //                " GROUP BY ASSET_NAME\n" +
                //                " ORDER BY ASSET_NAME;";
                //        break;
                //    }
                case SQLCommand.AssetItem.FetchProjectNameByAssetItem:
                    {
                        query = "SELECT MP.PROJECT_ID,MP.PROJECT\n" +
                                "  FROM ASSET_ITEM_DETAIL AID\n" +
                                "  LEFT JOIN MASTER_PROJECT MP\n" +
                                "    ON AID.PROJECT_ID = MP.PROJECT_ID\n" +
                                " WHERE ITEM_ID IN(?ITEM_ID)\n" +
                                " GROUP BY MP.PROJECT;";
                        break;
                    }
                case SQLCommand.AssetItem.FetchInoutIdByItemId:
                    {
                        query = "SELECT IN_OUT_ID FROM ASSET_IN_OUT_DETAIL WHERE ITEM_ID=?ITEM_ID;";
                        break;
                    }
                case SQLCommand.AssetItem.FetchAllAssetItems:
                    {
                        //query = "SELECT AI.ASSET_ITEM, AC.ASSET_CLASS\n" +
                        //        "  FROM ASSET_ITEM AI\n" +
                        //        " INNER JOIN ASSET_CLASS AC\n" +
                        //        "    ON AC.ASSET_CLASS_ID = AI.ASSET_CLASS_ID\n" +
                        //        " GROUP BY AI.ASSET_ITEM;";

                        query = "SELECT IF(PARENT.ASSET_CLASS='Primary', AC.ASSET_CLASS,PARENT.ASSET_CLASS)  AS PARENT_CLASS, AI.ASSET_ITEM, AC.ASSET_CLASS\n" +
                        "  FROM ASSET_ITEM AI\n" +
                        " INNER JOIN ASSET_CLASS AC\n" +
                        "    ON AC.ASSET_CLASS_ID = AI.ASSET_CLASS_ID\n" +
                        "  LEFT JOIN ASSET_CLASS PARENT\n" +
                        "    ON AC.PARENT_CLASS_ID = PARENT.ASSET_CLASS_ID\n" +
                        " GROUP BY AI.ASSET_ITEM\n" +
                        " ORDER BY AC.ASSET_CLASS, AI.ASSET_ITEM ASC;";


                        break;
                    }
                case SQLCommand.AssetItem.FetchAssetItemIDByName:
                    {
                        query = "SELECT ITEM_ID FROM ASSET_ITEM WHERE ASSET_ITEM=?ASSET_ITEM";
                        break;
                    }

                case SQLCommand.AssetItem.FetchAssetClassIDByAssetName:
                    {
                        query = "SELECT ASSET_CLASS_ID FROM ASSET_ITEM WHERE ASSET_ITEM=?ASSET_ITEM";
                        break;
                    }

                #region Fixed Asset Details
                case SQLCommand.AssetItem.DeleteFixedAsset:
                    {
                        query = "DELETE FROM ASSET_ITEM_DETAIL WHERE ASSET_ID IN(?ASSET_ID)";
                        break;
                    }
                case SQLCommand.AssetItem.DeleteFixedAssetbyDetailId:
                    {
                        query = "DELETE FROM ASSET_ITEM_DETAIL WHERE ITEM_DETAIL_ID IN(?ITEM_DETAIL_ID)";
                        break;
                    }
                case SQLCommand.AssetItem.SaveFixedAssetItemDetails:
                    {
                        //query = "INSERT INTO ASSET_ITEM_DETAIL\n" +
                        //        "  (ITEM_ID,\n" +
                        //        "   ASSET_ID,\n" +
                        //        "   AMOUNT,\n" +
                        //        "   PROJECT_ID,\n" +
                        //        "   MANUFACTURER_ID,\n" +
                        //        "   LOCATION_ID,\n" +
                        //        "   CUSTODIAN_ID,\n" +
                        //        "   INSURANCE_PLAN_ID,\n" +
                        //        "   POLICY_NO,\n" +
                        //        "   AMC_MONTHS,\n" +
                        //        "   STATUS,\n" +
                        //        "   CONDITIONS)\n" +
                        //        "VALUES\n" +
                        //        "  (?ITEM_ID,\n" +
                        //        "   ?ASSET_ID,\n" +
                        //        "   ?AMOUNT,\n" +
                        //        "   ?PROJECT_ID,\n" +
                        //        "   ?MANUFACTURER_ID,\n" +
                        //        "   ?LOCATION_ID,\n" +
                        //        "   ?CUSTODIAN_ID,\n" +
                        //        "   ?INSURANCE_PLAN_ID,\n" +
                        //        "   ?POLICY_NO,\n" +
                        //        "   ?AMC_MONTHS,\n" +
                        //        "   ?STATUS,\n" +
                        //        "   ?CONDITION);";

                        query = "INSERT INTO ASSET_ITEM_DETAIL\n" +
                               "  (ITEM_ID,\n" +
                               "   ASSET_ID,\n" +
                               "   DEPRECIATION_AMOUNT,\n" +
                               "   AMOUNT,\n" +
                               "   PROJECT_ID,\n" +
                               "   MANUFACTURER_ID,\n" +
                               "   LOCATION_ID,\n" +
                               "   CUSTODIAN_ID,\n" +
                               "   AMC_MONTHS,\n" +
                               "   STATUS,\n" +
                               "   CONDITIONS,SALVAGE_VALUE)\n" +
                               "VALUES\n" +
                               "  (?ITEM_ID,\n" +
                               "   ?ASSET_ID,\n" +
                               "   ?DEPRECIATION_AMOUNT,\n" +
                               "   ?AMOUNT,\n" +
                               "   ?PROJECT_ID,\n" +
                               "   ?MANUFACTURER_ID,\n" +
                               "   ?LOCATION_ID,\n" +
                               "   ?CUSTODIAN_ID,\n" +
                               "   ?AMC_MONTHS,\n" +
                               "   ?STATUS,\n" +
                               "   ?CONDITIONS,?SALVAGE_VALUE);";
                        break;
                    }
                case SQLCommand.AssetItem.SaveFixedAssetInsuranceItemDetails:
                    {
                        query = "INSERT INTO ASSET_ITEM_DETAIL\n" +
                                "   ASSET_ID,\n" +
                                "   INSURANCE_PLAN_ID,\n" +
                                "   POLICY_NO)\n" +
                                "VALUES\n" +
                                "  (?ASSET_ID,\n" +
                                "   ?INSURANCE_PLAN_ID,\n" +
                                "   ?POLICY_NO);";
                        break;
                    }
                case SQLCommand.AssetItem.UpdateFixedAssetItemDetails:
                    {
                        //query = "UPDATE ASSET_ITEM_DETAIL\n" +
                        //        "   SET ITEM_ID           = ?ITEM_ID,\n" +
                        //        "       ASSET_ID          = ?ASSET_ID,\n" +
                        //        "       AMOUNT            = ?AMOUNT,\n" +
                        //        "       PROJECT_ID        = ?PROJECT_ID,\n" +
                        //        "       MANUFACTURER_ID   = ?MANUFACTURER_ID,\n" +
                        //        "       LOCATION_ID       = ?LOCATION_ID,\n" +
                        //        "       CUSTODIAN_ID      = ?CUSTODIAN_ID,\n" +
                        //        "       INSURANCE_PLAN_ID = ?INSURANCE_PLAN_ID,\n" +
                        //        "       POLICY_NO         = ?POLICY_NO,\n" +
                        //        "       AMC_MONTHS        = ?AMC_MONTHS,\n" +
                        //        "       STATUS            = ?STATUS,\n" +
                        //        "       CONDITIONS         = ?CONDITION\n" +
                        //        " WHERE ITEM_DETAIL_ID = ?ITEM_DETAIL_ID;";


                        query = "UPDATE ASSET_ITEM_DETAIL\n" +
                                "   SET ITEM_ID           = ?ITEM_ID,\n" +
                                "       ASSET_ID          = ?ASSET_ID,\n" +
                                "       DEPRECIATION_AMOUNT  = ?DEPRECIATION_AMOUNT,\n" +
                                "       AMOUNT            = ?AMOUNT,\n" +
                                "       PROJECT_ID        = ?PROJECT_ID,\n" +
                                "       MANUFACTURER_ID   = ?MANUFACTURER_ID,\n" +
                                "       LOCATION_ID       = ?LOCATION_ID,\n" +
                                "       CUSTODIAN_ID      = ?CUSTODIAN_ID,\n" +
                                "       AMC_MONTHS        = ?AMC_MONTHS,\n" +
                                "       STATUS            = ?STATUS,\n" +
                                "       CONDITIONS         = ?CONDITIONS,\n" +
                                "       SALVAGE_VALUE         = ?SALVAGE_VALUE\n" +
                                " WHERE ITEM_DETAIL_ID = ?ITEM_DETAIL_ID;";

                        break;
                    }
                case SQLCommand.AssetItem.UpdateFixedAssetInsuranceItemDetails:
                    {
                        query = "UPDATE ASSET_ITEM_DETAIL\n" +
                                "   SET \n" +
                                "       INSURANCE_PLAN_ID = ?INSURANCE_PLAN_ID,\n" +
                                "       POLICY_NO         = ?POLICY_NO\n" +
                                " WHERE ITEM_DETAIL_ID = ?ITEM_DETAIL_ID";  // ASSET_ID = ?ASSET_ID;
                        break;
                    }
                case SQLCommand.AssetItem.FetchAMCAssetItems:
                    {

                        query = "SELECT AC.ASSET_CLASS,AI.ACCOUNT_LEDGER_ID,\n" +
                        "       AI.ITEM_ID,\n" +
                        "       CONCAT(AI.ASSET_ITEM, '-',AC.ASSET_CLASS) AS ASSET_ITEM\n" +
                        "  FROM ASSET_ITEM AI\n" +
                        "  LEFT JOIN ASSET_CLASS AC\n" +
                        "    ON AI.ASSET_CLASS_ID = AC.ASSET_CLASS_ID\n" +
                        " WHERE IS_AMC = 1;";

                        break;
                    }
                case SQLCommand.AssetItem.UpdateAssetItemDetailStatus:
                    {
                        query = "UPDATE ASSET_ITEM_DETAIL SET STATUS=?STATUS WHERE ITEM_DETAIL_ID IN(?ITEM_DETAIL_IDs)";

                        break;
                    }

                case SQLCommand.AssetItem.FetchAssetMapedLedgers:
                    {
                        query = "SELECT * FROM MASTER_SETTING WHERE SETTING_NAME=?SETTING_NAME";

                        break;
                    }

                case SQLCommand.AssetItem.FetchAssetAccessFlag:
                    {
                        query = "SELECT ASSET_ACCESS_FLAG FROM ASSET_ITEM WHERE ITEM_ID=?ITEM_ID";
                        break;
                    }

                #endregion
            }
            return query;
        }
        #endregion
    }
}
