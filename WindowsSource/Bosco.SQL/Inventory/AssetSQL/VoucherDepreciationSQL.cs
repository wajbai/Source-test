//chinna

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Utility;
using Bosco.DAO;

namespace Bosco.SQL
{
    public class VoucherDepreciationSQL : IDatabaseQuery
    {
        #region VariableDeclaration
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;
        #endregion
        #region Query
        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.VoucherDepreciation).FullName)
            {
                query = GetDepSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #region SQL
        public string GetDepSQL()
        {
            string query = "";
            SQLCommand.VoucherDepreciation SqlcommandId = (SQLCommand.VoucherDepreciation)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.VoucherDepreciation.AddDepreciationMaster:
                    {
                        query = "INSERT INTO ASSET_DEPRECIATION_MASTER\n" +
                                "  (DEPRECIATION_APPLIED_ON,PROJECT_ID,DEPRECIATION_PERIOD_FROM,DEPRECIATION_PERIOD_TO,NARRATION,VOUCHER_ID)\n" +
                                "VALUES\n" +
                                "  (?DEPRECIATION_APPLIED_ON,?PROJECT_ID,?DEPRECIATION_PERIOD_FROM,?DEPRECIATION_PERIOD_TO,?NARRATION,?VOUCHER_ID);";
                        break;
                    }
                case SQLCommand.VoucherDepreciation.EditDepreciationMaster:
                    {
                        query = "UPDATE ASSET_DEPRECIATION_MASTER\n" +
                                    "   SET DEPRECIATION_ID        = ?DEPRECIATION_ID,\n" +
                                    "       DEPRECIATION_APPLIED_ON   = ?DEPRECIATION_APPLIED_ON,\n" +
                                    "       PROJECT_ID    = ?PROJECT_ID,\n" +
                                    "       NARRATION    = ?NARRATION,\n" +
                                    "       VOUCHER_ID    = ?VOUCHER_ID\n" +
                                    " WHERE DEPRECIATION_ID = ?DEPRECIATION_ID";
                        break;
                    }
                case SQLCommand.VoucherDepreciation.AddDepreciationDetail:
                    {
                        query = "INSERT INTO ASSET_DEPRECIATION_DETAIL\n" +
                                "  (DEPRECIATION_ID, ITEM_DETAIL_ID, DEPRECIATION_PERCENTAGE, DEPRECIATION_VALUE,BALANCE_AMOUNT,METHOD_ID, DEPRECIATION_APPLY_FROM, DEPRECIATON_PERIOD_TO)\n" +
                                "VALUES\n" +
                                "  (?DEPRECIATION_ID, ?ITEM_DETAIL_ID, ?DEPRECIATION_PERCENTAGE, ?DEPRECIATION_VALUE, ?BALANCE_AMOUNT,?METHOD_ID, ?DEPRECIATION_APPLY_FROM, ?DEPRECIATON_PERIOD_TO);";
                        break;
                    }
                case SQLCommand.VoucherDepreciation.DeleteDepreciation:
                    {
                        query = "DELETE FROM ASSET_DEPRECIATION_DETAIL WHERE DEPRECIATION_ID =?DEPRECIATION_ID";
                        break;
                    }
                case SQLCommand.VoucherDepreciation.DeleteMasterDepreciation:
                    {
                        query = "DELETE FROM ASSET_DEPRECIATION_MASTER WHERE DEPRECIATION_ID =?DEPRECIATION_ID";
                        break;
                    }
                case SQLCommand.VoucherDepreciation.FetchDepreciationLedger:
                    {
                        query = "SELECT ML.LEDGER_ID, ML.LEDGER_NAME\n" +
                        "  FROM MASTER_LEDGER ML\n" +
                        " INNER JOIN PROJECT_LEDGER PL\n" +
                        "    ON ML.LEDGER_ID = PL.LEDGER_ID\n" +
                        " WHERE PL.PROJECT_ID = ?PROJECT_ID\n" +
                        "   AND ML.IS_DEPRECIATION_LEDGER = 1 ORDER BY LEDGER_NAME ASC";
                        break;
                    }
                case SQLCommand.VoucherDepreciation.FetchAll:
                    {
                        query = "SELECT TT.DEPRECIATION_ID,\n" +
                        "       TT.DEPRECIATION_APPLIED_ON,\n" +
                        "       AID.ITEM_DETAIL_ID,\n" +
                        "       MP.PROJECT_ID,\n" +
                        "       AI.ITEM_ID,\n" +
                        "       AC.ASSET_CLASS, IF(PARENT.ASSET_CLASS='Primary', AC.ASSET_CLASS,PARENT.ASSET_CLASS)  AS PARENT_CLASS,\n" +
                        "       AI.ASSET_ITEM,\n" +
                        "       AID.ASSET_ID,\n" +
                        "       IF(AM.METHOD_ID = 1, 'S L V', 'W D V') AS DEP_METHOD,\n" +
                        "       AID.AMOUNT AS OP_TRANS_TOTAL,\n" +
                        "       TT.DEPRECIATION_PERIOD_FROM,\n" +
                        "       TT.DEPRECIATION_PERIOD_TO,\n" +
                        "       TT.DEPRECIATION_PERCENTAGE,\n" +
                        "       TT.DEPRECIATION_VALUE,\n" +
                        "       TT.BALANCE_AMOUNT,\n" +
                        "       TT.NARRATION,\n" +
                        "       IFNULL(TRES.OP_AMOUNT, 0) AS OP_AMOUNT,\n" +
                        "       IFNULL(TRES.IN_AMOUNT, 0) AS IN_AMOUNT,\n" +
                        "       IFNULL(TRES.OUT_AMOUNT, 0) AS OUT_AMOUNT,\n" +
                        "\n" +
                        "       CASE\n" +
                        "         WHEN OP_AMOUNT > 0 THEN\n" +
                        "          OP_AMOUNT - (IN_AMOUNT - OUT_AMOUNT)\n" +
                        "         ELSE\n" +
                        "          (IN_AMOUNT - OUT_AMOUNT) - OP_AMOUNT\n" +
                        "       END AS TOTAL_AMOUNT\n" +
                        "\n" +
                        "  FROM ASSET_ITEM AI\n" +
                        " INNER JOIN ASSET_ITEM_DETAIL AID\n" +
                        "    ON AI.ITEM_ID = AID.ITEM_ID\n" +
                        "  LEFT JOIN MASTER_PROJECT MP\n" +
                        "    ON MP.PROJECT_ID = AID.PROJECT_ID\n" +
                        "  LEFT JOIN ASSET_CLASS AC\n" +
                        "    ON AC.ASSET_CLASS_ID = AI.ASSET_CLASS_ID\n" +
                        "  LEFT JOIN ASSET_DEP_METHOD AM\n" +
                        "    ON AM.METHOD_ID = AC.METHOD_ID\n" +
                        "  LEFT JOIN ASSET_DEPRECIATION_DETAIL AD\n" +
                        "    ON AD.ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID\n" +
                        "  LEFT JOIN (SELECT T.*\n" +
                        "               FROM (SELECT ADM.DEPRECIATION_ID,\n" +
                        "                            ADM.DEPRECIATION_APPLIED_ON,\n" +
                        "                            ADM.PROJECT_ID,\n" +
                        "                            ADM.DEPRECIATION_PERIOD_FROM,\n" +
                        "                            ADM.DEPRECIATION_PERIOD_TO,\n" +
                        "                            ADM.NARRATION,\n" +
                        "                            AT.ITEM_DETAIL_ID,\n" +
                        "                            AT.DEPRECIATION_PERCENTAGE,\n" +
                        "                            AT.DEPRECIATION_VALUE,\n" +
                        "                            AT.BALANCE_AMOUNT\n" +
                        "                       FROM ASSET_DEPRECIATION_MASTER ADM\n" +
                        "                      INNER JOIN ASSET_DEPRECIATION_DETAIL AT\n" +
                        "                         ON ADM.DEPRECIATION_ID = AT.DEPRECIATION_ID) AS T) AS TT\n" +
                        "    ON AID.ITEM_DETAIL_ID = TT.ITEM_DETAIL_ID\n" +
                        "\n" +
                        "  LEFT JOIN (\n" +
                        "\n" +
                        "             SELECT ITEM_DETAIL_ID,\n" +
                        "                     SUM(OP_AMOUNT) AS OP_AMOUNT,\n" +
                        "                     SUM(OUT_AMOUNT) AS OUT_AMOUNT,\n" +
                        "                     SUM(IN_AMOUNT) AS IN_AMOUNT\n" +
                        "               FROM (SELECT AID.ITEM_DETAIL_ID,\n" +
                        "                             CASE\n" +
                        "                               WHEN AIM.FLAG IN ('OP') THEN\n" +
                        "                                AID.AMOUNT\n" +
                        "                               ELSE\n" +
                        "                                0\n" +
                        "                             END AS OP_AMOUNT,\n" +
                        "                             CASE\n" +
                        "                               WHEN AIM.FLAG IN ('SL', 'DS', 'DN') THEN\n" +
                        "                                AID.AMOUNT\n" +
                        "                               ELSE\n" +
                        "                                0\n" +
                        "                             END AS OUT_AMOUNT,\n" +
                        "                             CASE\n" +
                        "                               WHEN AIM.FLAG IN ('PU', 'IK') THEN\n" +
                        "                                AID.AMOUNT\n" +
                        "                               ELSE\n" +
                        "                                0\n" +
                        "                             END AS IN_AMOUNT\n" +
                        "                        FROM ASSET_IN_OUT_MASTER AIM\n" +
                        "                       INNER JOIN ASSET_IN_OUT_DETAIL AIOD\n" +
                        "                          ON AIM.IN_OUT_ID = AIOD.IN_OUT_ID\n" +
                        "                       INNER JOIN ASSET_TRANS AT\n" +
                        "                          ON AIOD.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID\n" +
                        "                       INNER JOIN ASSET_ITEM_DETAIL AID\n" +
                        "                          ON AT.ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID) AS TT\n" +
                        "              GROUP BY ITEM_DETAIL_ID) AS TRES\n" +
                        "    ON AID.ITEM_DETAIL_ID = TRES.ITEM_DETAIL_ID\n" +
                        "    LEFT JOIN ASSET_CLASS PARENT \n" +
                        "      ON AC.PARENT_CLASS_ID = PARENT.ASSET_CLASS_ID \n" +
                        " WHERE MP.PROJECT_ID =?PROJECT_ID\n" +
                        "   AND AI.IS_ASSET_DEPRECIATION = 1\n" +
                        " GROUP BY ITEM_DETAIL_ID;";
                        break;
                    }
                case SQLCommand.VoucherDepreciation.FetchAccountLedger:
                    {
                        query = "\n" +
                        "SELECT LEDGER_ID,\n" +
                        "       LEDGER_NAME,\n" +
                        "       0.00 'AMOUNT',\n" +
                        "       ACCOUNT_LEDGER_ID,\n" +
                        "       DEPRECIATION_LEDGER_ID\n" +
                        "  FROM MASTER_LEDGER ML\n" +
                        " INNER JOIN ASSET_ITEM AI\n" +
                        "    ON AI.ACCOUNT_LEDGER_ID = ML.LEDGER_ID\n" +
                        " WHERE DEPRECIATION_LEDGER_ID = 1 GROUP BY ML.LEDGER_ID";
                        break;
                    }

                case SQLCommand.VoucherDepreciation.FetchPreviousRenewal:
                    {
                        //query = "SELECT * FROM ASSET_DEPRECIATION_MASTER\n" +
                        //        "                                     WHERE DEPRECIATION_ID IN\n" +
                        //        "                                           (SELECT MAX(DEPRECIATION_ID)\n" +
                        //        "                                              FROM ASSET_DEPRECIATION_MASTER \n" +
                        //        "                                             WHERE DEPRECIATION_ID < ?DEPRECIATION_ID)";
                        query = "SELECT * FROM ASSET_DEPRECIATION_MASTER\n" +
                                "                                     WHERE DEPRECIATION_ID IN\n" +
                                "                                           (SELECT MIN(DEPRECIATION_ID)\n" +
                                "                                              FROM ASSET_DEPRECIATION_MASTER \n" +
                                "                                             WHERE  DEPRECIATION_PERIOD_FROM < ?DEPRECIATION_PERIOD_FROM)";
                        break;
                    }

                case SQLCommand.VoucherDepreciation.FetchNextRenewal:
                    {
                        //query = "SELECT * FROM ASSET_DEPRECIATION_MASTER\n" +
                        //        "                                     WHERE DEPRECIATION_ID IN\n" +
                        //        "                                           (SELECT MIN(DEPRECIATION_ID)\n" +
                        //        "                                              FROM ASSET_DEPRECIATION_MASTER \n" +
                        //        "                                             WHERE  DEPRECIATION_ID > ?DEPRECIATION_ID)";

                        query = "SELECT * FROM ASSET_DEPRECIATION_MASTER\n" +
                                "                                     WHERE DEPRECIATION_ID IN\n" +
                                "                                           (SELECT MIN(DEPRECIATION_ID)\n" +
                                "                                              FROM ASSET_DEPRECIATION_MASTER \n" +
                                "                                             WHERE  DEPRECIATION_PERIOD_FROM > ?DEPRECIATION_PERIOD_FROM)";
                        break;
                    }

                case SQLCommand.VoucherDepreciation.FetchMaxRenewal:
                    {
                        query = "SELECT IFNULL(MAX(DEPRECIATION_PERIOD_FROM),'') AS PERIOD_FROM, IFNULL(MAX(DEPRECIATION_PERIOD_TO),'') AS PERIOD_TO \n" +
                                " FROM ASSET_DEPRECIATION_MASTER WHERE PROJECT_ID= ?PROJECT_ID AND DEPRECIATION_PERIOD_FROM >= ?DEPRECIATION_PERIOD_FROM  AND DEPRECIATION_PERIOD_TO <= ?DEPRECIATION_PERIOD_TO";
                        break;
                    }
                case SQLCommand.VoucherDepreciation.FetchExistorNot:
                    {
                        query = "SELECT COUNT(DEPRECIATION_ID) AS DEPID FROM ASSET_DEPRECIATION_MASTER";
                        break;
                    }
                case SQLCommand.VoucherDepreciation.FetchVoucherMasterById:
                    {
                        query = "SELECT\n" +
                                " AIM.DEPRECIATION_ID,\n" +
                                " AIM.DEPRECIATION_APPLIED_ON,\n" +
                                " AIM.PROJECT_ID,\n" +
                                " AIM.VOUCHER_ID,\n" +
                                " AIM.DEPRECIATION_PERIOD_FROM,\n" +
                                " AIM.DEPRECIATION_PERIOD_TO,\n" +
                                " AIM.NARRATION\n" +
                                "  FROM ASSET_DEPRECIATION_MASTER AIM\n" +
                                " WHERE AIM.DEPRECIATION_ID IN (?DEPRECIATION_ID)\n" +
                                "   AND AIM.PROJECT_ID IN (?PROJECT_ID);";
                        break;
                    }
                case SQLCommand.VoucherDepreciation.FetchVoucherDetailsById:
                    {
                        query = "SELECT\n" +
                                " AIM.DEPRECIATION_ID,\n" +
                                " AIM.DEPRECIATION_APPLIED_ON,\n" +
                                " AIM.PROJECT_ID,\n" +
                                " AIM.VOUCHER_ID,\n" +
                                " AIM.DEPRECIATION_PERIOD_FROM,\n" +
                                " AIM.DEPRECIATION_PERIOD_TO,\n" +
                                " AIM.NARRATION\n" +
                                "  FROM ASSET_DEPRECIATION_MASTER AIM\n" +
                                " WHERE AIM.DEPRECIATION_ID IN (?DEPRECIATION_ID)\n" +
                                "   AND AIM.PROJECT_ID IN (?PROJECT_ID);";
                        break;
                    }

                case SQLCommand.VoucherDepreciation.FetchFinanceVoucherDetails:
                    {
                        query = "SELECT VMT.VOUCHER_ID, VOUCHER_NO, VOUCHER_DATE, VT.LEDGER_ID, VT.AMOUNT\n" +
                                "  FROM VOUCHER_MASTER_TRANS VMT\n" +
                                " INNER JOIN VOUCHER_TRANS VT\n" +
                                "    ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                " INNER JOIN ASSET_DEPRECIATION_MASTER ADM\n" +
                                "    ON VMT.VOUCHER_ID = ADM.VOUCHER_ID\n" +
                                " WHERE VMT.VOUCHER_ID = ?VOUCHER_ID\n" +
                                "   AND VT.TRANS_MODE = 'DR'\n" +
                                "   AND VMT.STATUS = 1 LIMIT 1";
                        break;
                    }

                case SQLCommand.VoucherDepreciation.FetchApplyDepreciationDetails:
                    {
                        //query = "SELECT AC.ASSET_CLASS,T5.STATUS,\n" +
                        //        "       AI.ASSET_ITEM,\n" +
                        //        "       AAI.ASSET_ID,\n" +
                        //        "       ML.LEDGER_ID,\n" +
                        //        "       ML.LEDGER_NAME,\n" +
                        //        "       IFNULL(T1.PREV_COST, 0) AS PREV_COST,\n" +
                        //        "       IF(T1.PREV_COST > 0, 0, AAI.AMOUNT) AS CUR_COST,\n" +
                        //        "       AI.RETENTION_YRS AS LIFE_YRS,\n" +
                        //        "       AAI.SALVAGE_VALUE,AC.METHOD_ID,\n" +
                        //        "       AIM.IN_OUT_ID,\n" +
                        //        "       AID.IN_OUT_DETAIL_ID,\n" +
                        //        "       T2.ACC_AMOUNT AS ACCUMULATE_VALUE,((T1.PREV_COST + IF(T1.PREV_COST > 0, 0, AAI.AMOUNT)) - IFNULL(T2.ACC_AMOUNT,0)) AS TOTAL_VALUE,\n" +
                        //        "      IFNULL(T2.DOP,IN_OUT_DATE) AS DATE_OF_DOD,\n" +
                        //        "      IF(IFNULL(T2.DOP,'') = '' ,DATE(DATE_FORMAT(IN_OUT_DATE,CONCAT(YEAR(IN_OUT_DATE),'/',MONTH(IN_OUT_DATE),'/','01'))),DATE(?DEPRECIATION_PERIOD_FROM))AS DATE_OF_APPLY,\n" +
                        //        "  --    DATE_FORMAT(STR_TO_DATE(DATE_FORMAT(IN_OUT_DATE,concat(month(IN_OUT_DATE),'/','01/',year(IN_OUT_DATE))),'%c/%e/%Y'),'%d/%m/%Y') AS DATE_OF_APPLY,\n" +
                        //        "       AAI.ITEM_DETAIL_ID,0.00 AS BALANCE_AMOUNT,0.00 AS DEPRECIATION_VALUE,\n" +
                        //        "       IFNULL(IF(?DEPRECIATION_ID > 0,T3.DEPRECIATION_PERCENTAGE,0),0.00) AS DEPRECIATION_PERCENTAGE\n" +
                        //        "  FROM ASSET_IN_OUT_MASTER AIM\n" +
                        //        " INNER JOIN ASSET_IN_OUT_DETAIL AID\n" +
                        //        "    ON AIM.IN_OUT_ID = AID.IN_OUT_ID\n" +
                        //        " INNER JOIN ASSET_TRANS AT\n" +
                        //        "    ON AID.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID\n" +
                        //        " INNER JOIN ASSET_ITEM_DETAIL AAI\n" +
                        //        "    ON AAI.ITEM_DETAIL_ID = AT.ITEM_DETAIL_ID\n" +
                        //        " INNER JOIN ASSET_ITEM AI\n" +
                        //        "    ON AAI.ITEM_ID = AI.ITEM_ID\n" +
                        //        " INNER JOIN MASTER_LEDGER ML\n" +
                        //        "    ON ML.LEDGER_ID = AI.ACCOUNT_LEDGER_ID\n" +
                        //        " INNER JOIN ASSET_CLASS AC\n" +
                        //        "    ON AC.ASSET_CLASS_ID = AI.ASSET_CLASS_ID\n" +

                        //        "  LEFT JOIN (SELECT SUM(DEPRECIATION_VALUE) AS ACC_AMOUNT,\n" +
                        //        "                    MAX(DEPRECIATON_PERIOD_TO) AS DOP,\n" +
                        //        "                    ITEM_DETAIL_ID\n" +
                        //        "               FROM ASSET_DEPRECIATION_MASTER ADM\n" +
                        //        "\n" +
                        //        "              INNER JOIN ASSET_DEPRECIATION_DETAIL ADT\n" +
                        //        "                 ON ADM.DEPRECIATION_ID = ADT.DEPRECIATION_ID\n" +
                        //        "              WHERE ADM.PROJECT_ID=?PROJECT_ID AND ADT.DEPRECIATON_PERIOD_TO <= ?DEPRECIATION_PERIOD_FROM\n" +
                        //        "              GROUP BY ITEM_DETAIL_ID) AS T2\n" +
                        //        "    ON AAI.ITEM_DETAIL_ID = T2.ITEM_DETAIL_ID\n" +
                        //        "\n" +
                        //        "  LEFT JOIN (SELECT AID.ITEM_DETAIL_ID,\n" +
                        //        "                    CASE\n" +
                        //        "                      WHEN AID.ITEM_DETAIL_ID = T.SOLD_ITEM_DETAIL_ID THEN\n" +
                        //        "                       0\n" +
                        //        "                      ELSE\n" +
                        //        "                       1\n" +
                        //        "                    END AS STATUS\n" +
                        //        "               FROM ASSET_IN_OUT_DETAIL IOD\n" +
                        //        "               LEFT JOIN ASSET_TRANS AT\n" +
                        //        "                 ON IOD.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID\n" +
                        //        "               LEFT JOIN ASSET_ITEM_DETAIL AID\n" +
                        //        "                 ON AT.ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID\n" +
                        //        "               LEFT JOIN (SELECT AIM.IN_OUT_ID,\n" +
                        //        "                                AID.IN_OUT_DETAIL_ID,\n" +
                        //        "                                AT.ITEM_DETAIL_ID AS SOLD_ITEM_DETAIL_ID\n" +
                        //        "                           FROM ASSET_IN_OUT_MASTER AIM\n" +
                        //        "                          INNER JOIN ASSET_IN_OUT_DETAIL AID\n" +
                        //        "                             ON AIM.IN_OUT_ID = AID.IN_OUT_ID\n" +
                        //        "                          INNER JOIN ASSET_TRANS AT\n" +
                        //        "                             ON AID.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID\n" +
                        //        "                          WHERE FLAG IN ('SL', 'DN', 'DS')) AS T\n" +
                        //        "                 ON T.SOLD_ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID\n" +
                        //        "              GROUP BY ITEM_DETAIL_ID) AS T5\n" +
                        //        "    ON AAI.ITEM_DETAIL_ID = T5.ITEM_DETAIL_ID\n" +
                        //        "\n" +
                        //        "  LEFT JOIN (SELECT ADM.DEPRECIATION_ID,\n" +
                        //        "                    ITEM_DETAIL_ID,\n" +
                        //        "                    METHOD_ID,\n" +
                        //        "                    DEPRECIATION_PERCENTAGE,\n" +
                        //        "                    DEPRECIATION_VALUE,\n" +
                        //        "                    BALANCE_AMOUNT,\n" +
                        //        "                    DEPRECIATION_APPLY_FROM,\n" +
                        //        "                    DEPRECIATON_PERIOD_TO\n" +
                        //        "               FROM ASSET_DEPRECIATION_MASTER ADM\n" +
                        //        "\n" +
                        //        "              INNER JOIN ASSET_DEPRECIATION_DETAIL ADT\n" +
                        //        "                 ON ADM.DEPRECIATION_ID = ADT.DEPRECIATION_ID\n" +
                        //        "              WHERE ADM.PROJECT_ID = ?PROJECT_ID \n" +
                        //        "AND ADM.DEPRECIATION_PERIOD_FROM >= ?DEPRECIATION_PERIOD_FROM AND ADM.DEPRECIATION_PERIOD_TO <= ?DEPRECIATION_PERIOD_TO \n" +
                        //        "              GROUP BY ITEM_DETAIL_ID) AS T3\n" +
                        //        "    ON AAI.ITEM_DETAIL_ID = T3.ITEM_DETAIL_ID\n" +

                        //        "  INNER JOIN (SELECT\n" +
                        //        "              PREV_COST AS PREV_COST,\n" +
                        //        "              CUR_COST AS CUR_COST,\n" +
                        //        "              IN_OUT_ID,\n" +
                        //        "              IN_OUT_DETAIL_ID,\n" +
                        //        "              ITEM_DETAIL_ID\n" +
                        //        "               FROM (SELECT AAI.AMOUNT    AS PREV_COST,\n" +
                        //        "                            0   AS CUR_COST,\n" +
                        //        "                            AIM.IN_OUT_ID,\n" +
                        //        "                            AID.IN_OUT_DETAIL_ID,\n" +
                        //        "                            AAI.ITEM_DETAIL_ID\n" +
                        //        "                       FROM ASSET_IN_OUT_MASTER AIM\n" +
                        //        "                      INNER JOIN ASSET_IN_OUT_DETAIL AID\n" +
                        //        "                         ON AIM.IN_OUT_ID = AID.IN_OUT_ID\n" +
                        //        "                      INNER JOIN ASSET_TRANS AT\n" +
                        //        "                         ON AID.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID\n" +
                        //        "                      INNER JOIN ASSET_ITEM_DETAIL AAI\n" +
                        //        "                         ON AAI.ITEM_DETAIL_ID = AT.ITEM_DETAIL_ID\n" +
                        //        "                      WHERE IN_OUT_DATE < ?DEPRECIATION_PERIOD_FROM\n" +
                        //        "                        AND AIM.PROJECT_ID = ?PROJECT_ID\n" +
                        //        "                     UNION ALL\n" +
                        //        "                     SELECT 0                    AS PREV_COST,\n" +
                        //        "                            AAI.AMOUNT           AS CUR_COST,\n" +
                        //        "                            AIM.IN_OUT_ID,\n" +
                        //        "                            AID.IN_OUT_DETAIL_ID,\n" +
                        //        "                            AAI.ITEM_DETAIL_ID\n" +
                        //        "                       FROM ASSET_IN_OUT_MASTER AIM\n" +
                        //        "                      INNER JOIN ASSET_IN_OUT_DETAIL AID\n" +
                        //        "                         ON AIM.IN_OUT_ID = AID.IN_OUT_ID\n" +
                        //        "                      INNER JOIN ASSET_TRANS AT\n" +
                        //        "                         ON AID.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID\n" +
                        //        "                      INNER JOIN ASSET_ITEM_DETAIL AAI\n" +
                        //        "                         ON AAI.ITEM_DETAIL_ID = AT.ITEM_DETAIL_ID\n" +
                        //        "                      WHERE IN_OUT_DATE BETWEEN ?DEPRECIATION_PERIOD_FROM AND ?DEPRECIATION_PERIOD_TO\n" +
                        //        "                        AND AIM.PROJECT_ID = ?PROJECT_ID) AS T\n" +
                        //        "              GROUP BY ITEM_DETAIL_ID) AS T1\n" +
                        //        "    ON AAI.ITEM_DETAIL_ID = T1.ITEM_DETAIL_ID\n" +
                        //        " WHERE AIM.PROJECT_ID = ?PROJECT_ID AND IS_ASSET_DEPRECIATION=1 \n" +
                        //        " GROUP BY ITEM_DETAIL_ID;";

                        // Chinna method Id taken from Asset Class need to verify it (AC.METHOD_ID)

                        query = "SELECT AC.ASSET_CLASS,T5.STATUS,IFNULL(ISDEP,0) AS ISDEP,\n" +
                                "       AI.ASSET_ITEM,\n" +
                                "       AAI.ASSET_ID,\n" +
                                "       ML.LEDGER_ID,\n" +
                                "       ML.LEDGER_NAME,\n" +
                                "       IFNULL(T1.PREV_COST, 0) AS PREV_COST,\n" +
                                "       IF(T1.PREV_COST > 0, 0, AAI.AMOUNT) AS CUR_COST,\n" +
                                "       AI.RETENTION_YRS AS LIFE_YRS,\n" +
                                "       AAI.SALVAGE_VALUE,T3.METHOD_ID,\n" +
                                "       AIM.IN_OUT_ID,\n" +
                                "       AIM.FLAG,\n" +
                                "       T2.DOP AS PREDOP,\n" +
                                "       AID.IN_OUT_DETAIL_ID,\n" +
                                "       T2.ACC_AMOUNT AS ACCUMULATE_VALUE,((T1.PREV_COST + IF(T1.PREV_COST > 0, 0, AAI.AMOUNT)) - IFNULL(T2.ACC_AMOUNT,0)) AS TOTAL_VALUE,\n" +
                                "      IFNULL(T2.DOP,IN_OUT_DATE) AS DATE_OF_DOD,\n" +
                                " --     IF(IFNULL(T2.DOP,'') = '' ,DATE(DATE_FORMAT(IN_OUT_DATE,CONCAT(YEAR(IN_OUT_DATE),'/',MONTH(IN_OUT_DATE),'/','01'))),DATE(?DEPRECIATION_PERIOD_FROM))AS DATE_OF_APPLY,\n" +
                                "           IF(IFNULL(T2.DOP,'') = '' ,DATE(DATE_FORMAT(IN_OUT_DATE,CONCAT(YEAR(IN_OUT_DATE),'/',MONTH(IN_OUT_DATE),'/','01'))),DATE(ADDDATE(T2.DOP,1)))AS DATE_OF_APPLY,\n" +
                                "       AAI.ITEM_DETAIL_ID,0.00 AS BALANCE_AMOUNT,0.00 AS DEPRECIATION_VALUE,\n" +
                                "       IFNULL(IF(?DEPRECIATION_ID > 0,T3.DEPRECIATION_PERCENTAGE, AI.DEPRECIATION_NO),0.00) AS DEPRECIATION_PERCENTAGE\n" + // CHINNA 23.03.2021
                                "  FROM ASSET_IN_OUT_MASTER AIM\n" +
                                " INNER JOIN ASSET_IN_OUT_DETAIL AID\n" +
                                "    ON AIM.IN_OUT_ID = AID.IN_OUT_ID\n" +
                                " INNER JOIN ASSET_TRANS AT\n" +
                                "    ON AID.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID\n" +
                                " INNER JOIN ASSET_ITEM_DETAIL AAI\n" +
                                "    ON AAI.ITEM_DETAIL_ID = AT.ITEM_DETAIL_ID\n" +
                                " INNER JOIN ASSET_ITEM AI\n" +
                                "    ON AAI.ITEM_ID = AI.ITEM_ID\n" +
                                " INNER JOIN MASTER_LEDGER ML\n" +
                                "    ON ML.LEDGER_ID = AI.DEPRECIATION_LEDGER_ID\n" + "    -- ON ML.LEDGER_ID = AI.ACCOUNT_LEDGER_ID\n" +
                                " INNER JOIN ASSET_CLASS AC\n" +
                                "    ON AC.ASSET_CLASS_ID = AI.ASSET_CLASS_ID\n" +

                                "  LEFT JOIN (SELECT SUM(DEPRECIATION_VALUE) AS ACC_AMOUNT,\n" +
                                "                    MAX(DEPRECIATON_PERIOD_TO) AS DOP,\n" +
                                "                    ITEM_DETAIL_ID\n" +
                                "               FROM ASSET_DEPRECIATION_MASTER ADM\n" +
                                "\n" +
                                "              INNER JOIN ASSET_DEPRECIATION_DETAIL ADT\n" +
                                "                 ON ADM.DEPRECIATION_ID = ADT.DEPRECIATION_ID\n" +
                                "              WHERE ADM.PROJECT_ID=?PROJECT_ID AND ADT.DEPRECIATON_PERIOD_TO <= ?DEPRECIATION_PERIOD_FROM\n" +
                                "              GROUP BY ITEM_DETAIL_ID) AS T2\n" +
                                "    ON AAI.ITEM_DETAIL_ID = T2.ITEM_DETAIL_ID\n" +
                                "\n" +
                                "  LEFT JOIN (SELECT AID.ITEM_DETAIL_ID,\n" +
                                "                    CASE\n" +
                                "                      WHEN AID.ITEM_DETAIL_ID = T.SOLD_ITEM_DETAIL_ID THEN\n" +
                                "                       0\n" +
                                "                      ELSE\n" +
                                "                       1\n" +
                                "                    END AS STATUS\n" +
                                "               FROM ASSET_IN_OUT_DETAIL IOD\n" +
                                "               LEFT JOIN ASSET_TRANS AT\n" +
                                "                 ON IOD.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID\n" +
                                "               LEFT JOIN ASSET_ITEM_DETAIL AID\n" +
                                "                 ON AT.ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID\n" +
                                "               LEFT JOIN (SELECT AIM.IN_OUT_ID,\n" +
                                "                                AID.IN_OUT_DETAIL_ID,\n" +
                                "                                AT.ITEM_DETAIL_ID AS SOLD_ITEM_DETAIL_ID\n" +
                                "                           FROM ASSET_IN_OUT_MASTER AIM\n" +
                                "                          INNER JOIN ASSET_IN_OUT_DETAIL AID\n" +
                                "                             ON AIM.IN_OUT_ID = AID.IN_OUT_ID\n" +
                                "                          INNER JOIN ASSET_TRANS AT\n" +
                                "                             ON AID.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID\n" +
                                "                          WHERE FLAG IN ('SL', 'DN', 'DS')) AS T\n" +
                                "                 ON T.SOLD_ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID\n" +
                                "              GROUP BY ITEM_DETAIL_ID) AS T5\n" +
                                "    ON AAI.ITEM_DETAIL_ID = T5.ITEM_DETAIL_ID\n" +
                                "\n" +
                                "  LEFT JOIN (SELECT ADM.DEPRECIATION_ID,1 AS ISDEP,\n" +
                                "                    ITEM_DETAIL_ID,\n" +
                                "                    METHOD_ID,\n" +
                                "                    DEPRECIATION_PERCENTAGE,\n" +
                                "                    DEPRECIATION_VALUE,\n" +
                                "                    BALANCE_AMOUNT,\n" +
                                "                    DEPRECIATION_APPLY_FROM,\n" +
                                "                    DEPRECIATON_PERIOD_TO\n" +
                                "               FROM ASSET_DEPRECIATION_MASTER ADM\n" +
                                "\n" +
                                "              INNER JOIN ASSET_DEPRECIATION_DETAIL ADT\n" +
                                "                 ON ADM.DEPRECIATION_ID = ADT.DEPRECIATION_ID\n" +
                                "              WHERE ADM.PROJECT_ID = ?PROJECT_ID \n" +
                                "AND ADM.DEPRECIATION_PERIOD_FROM >= ?DEPRECIATION_PERIOD_FROM AND ADM.DEPRECIATION_PERIOD_TO <= ?DEPRECIATION_PERIOD_TO \n" +
                                "              GROUP BY ITEM_DETAIL_ID) AS T3\n" +
                                "    ON AAI.ITEM_DETAIL_ID = T3.ITEM_DETAIL_ID\n" +

                                "  INNER JOIN (SELECT\n" +
                                "              PREV_COST AS PREV_COST,\n" +
                                "              CUR_COST AS CUR_COST,\n" +
                                "              IN_OUT_ID,\n" +
                                "              IN_OUT_DETAIL_ID,\n" +
                                "              ITEM_DETAIL_ID\n" +
                                "               FROM (SELECT AAI.AMOUNT    AS PREV_COST,\n" +
                                "                            0   AS CUR_COST,\n" +
                                "                            AIM.IN_OUT_ID,\n" +
                                "                            AID.IN_OUT_DETAIL_ID,\n" +
                                "                            AAI.ITEM_DETAIL_ID\n" +
                                "                       FROM ASSET_IN_OUT_MASTER AIM\n" +
                                "                      INNER JOIN ASSET_IN_OUT_DETAIL AID\n" +
                                "                         ON AIM.IN_OUT_ID = AID.IN_OUT_ID\n" +
                                "                      INNER JOIN ASSET_TRANS AT\n" +
                                "                         ON AID.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID\n" +
                                "                      INNER JOIN ASSET_ITEM_DETAIL AAI\n" +
                                "                         ON AAI.ITEM_DETAIL_ID = AT.ITEM_DETAIL_ID\n" +
                                "                      WHERE IN_OUT_DATE < ?DEPRECIATION_PERIOD_FROM\n" +
                                "                        AND AIM.PROJECT_ID = ?PROJECT_ID\n" +
                                "                     UNION ALL\n" +
                                "                     SELECT 0                    AS PREV_COST,\n" +
                                "                            AAI.AMOUNT           AS CUR_COST,\n" +
                                "                            AIM.IN_OUT_ID,\n" +
                                "                            AID.IN_OUT_DETAIL_ID,\n" +
                                "                            AAI.ITEM_DETAIL_ID\n" +
                                "                       FROM ASSET_IN_OUT_MASTER AIM\n" +
                                "                      INNER JOIN ASSET_IN_OUT_DETAIL AID\n" +
                                "                         ON AIM.IN_OUT_ID = AID.IN_OUT_ID\n" +
                                "                      INNER JOIN ASSET_TRANS AT\n" +
                                "                         ON AID.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID\n" +
                                "                      INNER JOIN ASSET_ITEM_DETAIL AAI\n" +
                                "                         ON AAI.ITEM_DETAIL_ID = AT.ITEM_DETAIL_ID\n" +
                                "                      WHERE IN_OUT_DATE BETWEEN ?DEPRECIATION_PERIOD_FROM AND ?DEPRECIATION_PERIOD_TO\n" +
                                "                        AND AIM.PROJECT_ID = ?PROJECT_ID) AS T\n" +
                                "              GROUP BY ITEM_DETAIL_ID) AS T1\n" +
                                "    ON AAI.ITEM_DETAIL_ID = T1.ITEM_DETAIL_ID\n" +
                                " WHERE AIM.PROJECT_ID = ?PROJECT_ID AND IS_ASSET_DEPRECIATION=1 { AND T5.STATUS=?STATUS } { AND ISDEP=?DEP_STATUS }  \n" +
                                " GROUP BY ITEM_DETAIL_ID;";
                        break;
                    }

            }
            return query;
        #endregion
        }
        #endregion
    }
}
