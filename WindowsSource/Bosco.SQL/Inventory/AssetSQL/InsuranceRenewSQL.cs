using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    class InsuranceRenewSQL : IDatabaseQuery
    {
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;

        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.AssetRenewInsurance).FullName)
            {
                query = GetRenewSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #region SQL Script
        public string GetRenewSQL()
        {
            string query = "";
            SQLCommand.AssetRenewInsurance SqlcommandId = (SQLCommand.AssetRenewInsurance)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.AssetRenewInsurance.InsRenewAdd:
                    {
                        query = "INSERT INTO ASSET_INSURANCE_DETAIL\n" +
                                "  (ITEM_DETAIL_ID,\n" +
                                "   RENEWAL_DATE,\n" +
                                "   PERIOD_FROM,\n" +
                                "   PERIOD_TO,\n" +
                                "   SUM_INSURED,\n" +
                                "   PREMIUM_AMOUNT,\n" +
                                "   VOUCHER_ID,\n" +
                                "   INSURANCE_PLAN_ID,\n" +
                                "   POLICY_NO)\n" +
                                "VALUES\n" +
                                "  (?ITEM_DETAIL_ID,\n" +
                                "   ?RENEWAL_DATE,\n" +
                                "   ?PERIOD_FROM,\n" +
                                "   ?PERIOD_TO,\n" +
                                "   ?SUM_INSURED,\n" +
                                "   ?PREMIUM_AMOUNT,\n" +
                                "   ?VOUCHER_ID,\n" +
                                "   ?INSURANCE_PLAN_ID,\n" +
                                "   ?POLICY_NO)";
                        break;
                    }
                case SQLCommand.AssetRenewInsurance.FetchInsuranceDetails:
                    {
                        query = "SELECT AID.ITEM_ID,DF.CREATE_DATE AS RENEW_DATE,\n" +
                        "       AID.ITEM_DETAIL_ID,\n" +
                        "       AC.ASSET_CLASS_ID,\n" +
                        "       AID.AMOUNT,\n" +
                        "       TT.INSURANCE_PLAN_ID,\n" +
                        "       TT.INSURANCE_DETAIL_ID,\n" +
                        "       AC.ASSET_CLASS,ACPRNT.ASSET_CLASS AS PARENT_CLASS,\n" +
                        "       ASSET_ITEM,\n" +
                        "       AID.ASSET_ID,\n" +
                        "       CONCAT(AIP.INSURANCE_PLAN, CONCAT(' - ', AIP.COMPANY)) AS 'INSURANCE_PLAN',\n" +
                        "       TT.RENEWAL_DATE AS RENEW_DATE,\n" +
                        "       TT.POLICY_NO,\n" +
                        "       TT.PERIOD_FROM,\n" +
                        "       TT.PERIOD_TO,\n" +
                        "       TT.PREMIUM_AMOUNT,TT.SUM_INSURED,\n" +
                        "       TT.PERIOD_TO AS 'RENEWAL_DATE',\n" +
                        "       CASE\n" +
                        "         WHEN AID.ITEM_DETAIL_ID = T1.SOLD_ITEM_DETAIL_ID THEN\n" +
                        "          0\n" +
                        "         ELSE\n" +
                        "          1\n" +
                        "       END AS STATUS\n" +
                        "  FROM ASSET_ITEM AI\n" +
                        " INNER JOIN ASSET_ITEM_DETAIL AID\n" +
                        "    ON AI.ITEM_ID = AID.ITEM_ID\n" +
                        " INNER JOIN MASTER_PROJECT MP\n" +
                        "    ON MP.PROJECT_ID = AID.PROJECT_ID\n" +
                        "  LEFT JOIN ASSET_CLASS AC\n" +
                        "    ON AC.ASSET_CLASS_ID = AI.ASSET_CLASS_ID\n" +
                        "  LEFT JOIN ASSET_CLASS ACPRNT \n" +
                        "    ON AC.PARENT_CLASS_ID = ACPRNT.ASSET_CLASS_ID \n" +
                        "  LEFT JOIN ASSET_INSURANCE_DETAIL APD\n" +
                        "    ON APD.ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID\n" +
                        "  LEFT JOIN (SELECT MIN(RENEWAL_DATE)AS CREATE_DATE,ITEM_DETAIL_ID FROM  ASSET_INSURANCE_DETAIL GROUP BY ITEM_DETAIL_ID) AS DF\n" +
                        "     ON DF.ITEM_DETAIL_ID = AID.ITEM_DETAIL_ID\n" +
                        "  LEFT JOIN (SELECT AIM.IN_OUT_ID,\n" +
                        "               AID.IN_OUT_DETAIL_ID,\n" +
                        "               AT.ITEM_DETAIL_ID AS SOLD_ITEM_DETAIL_ID\n" +
                        "          FROM ASSET_IN_OUT_MASTER AIM\n" +
                        "                 INNER JOIN ASSET_IN_OUT_DETAIL AID\n" +
                        "            ON AIM.IN_OUT_ID = AID.IN_OUT_ID\n" +
                        "                 INNER JOIN ASSET_TRANS AT\n" +
                        "            ON AID.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID\n" +
                        "         WHERE FLAG IN ('SL', 'DN', 'DS')) AS T1\n" +
                        "    ON T1.SOLD_ITEM_DETAIL_ID=AID.ITEM_DETAIL_ID\n" +
                        "  LEFT JOIN (SELECT T.*\n" +
                        "               FROM (SELECT INSURANCE_DETAIL_ID,\n" +
                        "                            ITEM_DETAIL_ID,\n" +
                        "                            INSURANCE_PLAN_ID,\n" +
                        "                            POLICY_NO,\n" +
                        "                            PERIOD_FROM,\n" +
                        "                            PERIOD_TO,\n" +
                        "                            RENEWAL_DATE,\n" +
                        "                            PREMIUM_AMOUNT,\n" +
                        "                            SUM_INSURED\n" +
                        "                    FROM ASSET_INSURANCE_DETAIL\n" +
                        "                      ORDER BY RENEWAL_DATE DESC) AS T\n" +
                        "              GROUP BY ITEM_DETAIL_ID) AS TT\n" +
                        "    ON AID. ITEM_DETAIL_ID = TT.ITEM_DETAIL_ID\n" +
                        "  LEFT JOIN ASSET_INSURANCE_PLAN AIP\n" +
                        "    ON AIP.INSURANCE_PLAN_ID = APD.INSURANCE_PLAN_ID\n" +
                        " WHERE MP.PROJECT_ID =?PROJECT_ID {AND APD.PERIOD_TO>=?DATE_FROM}\n" +
                        "   AND AI.IS_INSURANCE = 1 \n" +
                        " GROUP BY ITEM_DETAIL_ID ORDER BY AC.ASSET_CLASS,AI.ASSET_ITEM ASC,TT.PERIOD_FROM DESC";
                        break;
                    }
                case SQLCommand.AssetRenewInsurance.DeleteDetails:
                    {
                        query = "DELETE FROM ASSET_INSURANCE_DETAIL WHERE INSURANCE_DETAIL_ID =?INSURANCE_DETAIL_ID";
                        break;
                    }

                case SQLCommand.AssetRenewInsurance.FetchVoucherIdByInsId:
                    {
                        query = "SELECT VOUCHER_ID FROM ASSET_INSURANCE_DETAIL WHERE INSURANCE_DETAIL_ID =?INSURANCE_DETAIL_ID";
                        break;
                    }

                case SQLCommand.AssetRenewInsurance.FetchVoucherIdByItemId:
                    {
                        query = "SELECT GROUP_CONCAT(VOUCHER_ID) AS VOUCHER_ID FROM ASSET_INSURANCE_DETAIL WHERE ITEM_DETAIL_ID = ?ITEM_DETAIL_ID";
                        break;
                    }

                case SQLCommand.AssetRenewInsurance.DeleteItemDetails:
                    {
                        query = "DELETE FROM ASSET_INSURANCE_DETAIL WHERE ITEM_DETAIL_ID =?ITEM_DETAIL_ID";
                        break;
                    }
                case SQLCommand.AssetRenewInsurance.FetchInsHistoryDetails:
                    {
                        query = "SELECT INSURANCE_DETAIL_ID,\n" +
                                "       AID.ITEM_DETAIL_ID,AID.PROJECT_ID,MP.PROJECT,\n" +
                                "       RENEWAL_DATE,\n" +
                                "       PERIOD_FROM,\n" +
                                "       PERIOD_TO,\n" +
                                "       SUM_INSURED,\n" +
                                "       PREMIUM_AMOUNT,\n" +
                                "       AI.INSURANCE_PLAN_ID,\n" +
                                "       AI.POLICY_NO,VOUCHER_ID,\n" +
                                "       VOUCHER_ID,\n" +
                                "       CASE\n" +
                                "         WHEN AID.ITEM_DETAIL_ID = T.SOLD_ITEM_DETAIL_ID THEN\n" +
                                "          0\n" +
                                "         ELSE\n" +
                                "          1\n" +
                                "       END AS STATUS\n" +
                                "  FROM ASSET_INSURANCE_DETAIL AI\n" +
                                " INNER JOIN ASSET_ITEM_DETAIL AID\n" +
                                "    ON AID.ITEM_DETAIL_ID = AI.ITEM_DETAIL_ID\n" +
                                " LEFT JOIN MASTER_PROJECT MP\n" +
                                "    ON AID.PROJECT_ID = MP.PROJECT_ID\n" +
                                "LEFT JOIN (SELECT AIM.IN_OUT_ID,\n" +
                                "               AID.IN_OUT_DETAIL_ID,\n" +
                                "               AT.ITEM_DETAIL_ID AS SOLD_ITEM_DETAIL_ID\n" +
                                "          FROM ASSET_IN_OUT_MASTER AIM\n" +
                                "                 INNER JOIN ASSET_IN_OUT_DETAIL AID\n" +
                                "            ON AIM.IN_OUT_ID = AID.IN_OUT_ID\n" +
                                "                 INNER JOIN ASSET_TRANS AT\n" +
                                "            ON AID.IN_OUT_DETAIL_ID = AT.IN_OUT_DETAIL_ID\n" +
                                "         WHERE FLAG IN ('SL', 'DN', 'DS')) AS T\n" +
                                "    ON T.SOLD_ITEM_DETAIL_ID=AID.ITEM_DETAIL_ID\n" +
                                " WHERE AID.ITEM_DETAIL_ID IN (?ITEM_DETAIL_ID) ORDER BY RENEWAL_DATE DESC ";
                        break;
                    }
                case SQLCommand.AssetRenewInsurance.FetchInsRenewDetails:
                    {
                        query = "SELECT INSURANCE_DETAIL_ID,ITEM_DETAIL_ID,\n" +
                                "       RENEWAL_DATE,\n" +
                                "       PERIOD_FROM,\n" +
                                "       PERIOD_TO,\n" +
                                "       SUM_INSURED,\n" +
                                "       PREMIUM_AMOUNT,\n" +
                                "       VOUCHER_ID,\n" +
                                "       INSURANCE_PALN_ID,\n" +
                                "       POLICY_NO\n" +
                                "  FROM ASSET_INSURANCE_DETAIL";
                        break;
                    }
                case SQLCommand.AssetRenewInsurance.InsRenewEdit:
                    {
                        query = "UPDATE ASSET_INSURANCE_DETAIL\n" +
                                "   SET RENEWAL_DATE   =?RENEWAL_DATE,\n" +
                                "       PERIOD_FROM    =?PERIOD_FROM,\n" +
                                "       PERIOD_TO      =?PERIOD_TO,\n" +
                                "       SUM_INSURED    =?SUM_INSURED,\n" +
                                "       PREMIUM_AMOUNT =?PREMIUM_AMOUNT,\n" +
                                "       INSURANCE_PLAN_ID =?INSURANCE_PLAN_ID,\n" +
                                "       POLICY_NO =?POLICY_NO,\n" +
                                "       VOUCHER_ID =?VOUCHER_ID\n" +
                                " WHERE INSURANCE_DETAIL_ID =?INSURANCE_DETAIL_ID";
                        break;
                    }
                case SQLCommand.AssetRenewInsurance.Fetch:
                    {
                        query = @"SELECT AID.ITEM_DETAIL_ID,AID.PROJECT_ID,MP.PROJECT,
                                   INSURANCE_PLAN_ID,
                                   POLICY_NO,VOUCHER_ID,
                                   RENEWAL_DATE,
                                   PERIOD_FROM,
                                   PERIOD_TO,
                                   SUM_INSURED,
                                   PREMIUM_AMOUNT
                                 FROM ASSET_INSURANCE_DETAIL ID
                                INNER JOIN ASSET_ITEM_DETAIL AID
                                ON AID.ITEM_DETAIL_ID = ID.ITEM_DETAIL_ID
                                LEFT JOIN MASTER_PROJECT MP
                                ON AID.PROJECT_ID = MP.PROJECT_ID
                                WHERE ID.INSURANCE_DETAIL_ID =?INSURANCE_DETAIL_ID ORDER BY RENEWAL_DATE ASC";
                        break;

                    }
                case SQLCommand.AssetRenewInsurance.LoadInsurancePLanDetails:
                    {
                        query = "SELECT INSURANCE_PLAN_ID, CONCAT(INSURANCE_PLAN,CONCAT(' - ',COMPANY)) AS 'INSURANCE_PLAN' FROM ASSET_INSURANCE_PLAN;";
                        break;
                    }
                case SQLCommand.AssetRenewInsurance.FetchItemDetailIdbyAssetId:
                    {
                        query = "SELECT ITEM_DETAIL_ID FROM ASSET_ITEM_DETAIL WHERE ASSET_ID =?ASSET_ID";
                        break;
                    }
                case SQLCommand.AssetRenewInsurance.DeleteInsuranceByDetailId:
                    {
                        query = "DELETE FROM ASSET_INSURANCE_DETAIL WHERE ITEM_DETAIL_ID  IN (?ITEM_DETAIL_IDs)";
                        break;
                    }
                case SQLCommand.AssetRenewInsurance.IsInsuranceMade:
                    {
                        query = "SELECT COUNT(INSURANCE_DETAIL_ID) AS INS_COUNT FROM ASSET_INSURANCE_DETAIL WHERE ITEM_DETAIL_ID=?ITEM_DETAIL_ID";
                        break;
                    }
                case SQLCommand.AssetRenewInsurance.FetchPreviousRenewal:
                    {
                        query = "SELECT *\n" +
                                    "  FROM asset_insurance_detail\n" +
                                    " WHERE INSURANCE_DETAIL_ID =\n" +
                                    "       (SELECT MAX(INSURANCE_DETAIL_ID)\n" +
                                    "          FROM asset_insurance_detail\n" +
                                    "         WHERE INSURANCE_DETAIL_ID < ?INSURANCE_DETAIL_ID\n" +
                                    "           AND ITEM_DETAIL_ID = ?ITEM_DETAIL_ID);";
                        break;
                    }
                case SQLCommand.AssetRenewInsurance.FetchNextRenewal:
                    {
                        query = "SELECT *\n" +
                                    "  FROM asset_insurance_detail\n" +
                                    " WHERE INSURANCE_DETAIL_ID =\n" +
                                    "       (SELECT MIN(INSURANCE_DETAIL_ID)\n" +
                                    "          FROM asset_insurance_detail\n" +
                                    "         WHERE INSURANCE_DETAIL_ID > ?INSURANCE_DETAIL_ID\n" +
                                    "           AND ITEM_DETAIL_ID = ?ITEM_DETAIL_ID);";
                        break;
                    }
                case SQLCommand.AssetRenewInsurance.FetchRegistrationDate:
                    {
                        query = "SELECT MAX(PERIOD_TO)AS PERIOD_TO FROM  ASSET_INSURANCE_DETAIL\n"+
                                "WHERE ITEM_DETAIL_ID=?ITEM_DETAIL_ID GROUP BY ITEM_DETAIL_ID";
                        break;
                    }
            }
            return query;
        }
        #endregion
    }
}
