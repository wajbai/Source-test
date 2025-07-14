using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    class InsuranceRenewalSQL : IDatabaseQuery
    {
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;

        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.AssetInsuranceRenewal).FullName)
            {
                query = GetInsuranceSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #region SQL Script
        public string GetInsuranceSQL()
        {
            string query = "";
            SQLCommand.AssetInsuranceRenewal SqlcommandId = (SQLCommand.AssetInsuranceRenewal)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.AssetInsuranceRenewal.FetchAllAssetItem:
                    {
                        query = "SELECT AID.ITEM_ID, ASSET_NAME\n" +
                                "  FROM ASSET_INSURANCE_MASTER_DETAIL AID\n" +
                                " INNER JOIN ASSET_ITEM AI\n" +
                                "    ON AID.ITEM_ID = AI.ITEM_ID GROUP BY ITEM_ID;";
                        break;
                    }
                case SQLCommand.AssetInsuranceRenewal.FetchAllRenewal:
                    {
                        query = "SELECT AIM.RENEWAL_ID,\n" +
                                "       INS_ID,\n" +
                                "       VOUCHER_DATE,\n" +
                                "       PROJECT_ID,\n" +
                                "       AID.ITEM_ID,\n" +
                                "       ASSET_NAME,\n" +
                                "       ASSET_ID,\n" +
                                "       RENEWAL_AMOUNT,\n" +
                                "       DUE_DATE\n" +
                                "  FROM ASSET_INSURANCE_RENEWAL_MASTER AIM\n" +
                                " INNER JOIN ASSET_INSURANCE_RENEWAL_DETAIL AID\n" +
                                "    ON AIM.RENEWAL_ID = AID.RENEWAL_ID\n" +
                                "  LEFT JOIN ASSET_ITEM AI\n" +
                                "    ON AID.ITEM_ID = AI.ITEM_ID\n" +
                            //   " WHERE INS_ID = ?INS_ID\n" + 
                                " WHERE AID.ITEM_ID = ?ITEM_ID\n" +
                            //    "  { AND AIM.RENEWAL_ID = ?RENEWAL_ID}\n" +
                                " AND AIM.PROJECT_ID = ?PROJECT_ID;";
                        break;
                    }
                case SQLCommand.AssetInsuranceRenewal.AddInsRenMaster:
                    {
                        query = "INSERT INTO ASSET_INSURANCE_RENEWAL_MASTER (RENEWAL_ID,\n" +
                                "INS_ID,\n" +
                                "VOUCHER_DATE,\n" +
                                "PROJECT_ID,\n" +
                                "VOUCHER_ID)\n" +
                                "VALUES\n" +
                                "(?RENEWAL_ID,\n" +
                                "?INS_ID,\n" +
                                "?VOUCHER_DATE,\n" +
                                "?PROJECT_ID,\n" +
                                "?VOUCHER_ID)";
                        break;
                    }
                case SQLCommand.AssetInsuranceRenewal.ValidateDate:
                    {
                        query = "SELECT AIRM.RENEWAL_ID, DUE_DATE\n" +
                                "  FROM ASSET_INSURANCE_RENEWAL_MASTER AIRM\n" +
                                " INNER JOIN ASSET_INSURANCE_RENEWAL_DETAIL AIRD\n" +
                                "    ON AIRM.RENEWAL_ID = AIRD.RENEWAL_ID\n" +
                                " WHERE PROJECT_ID = ?PROJECT_ID\n" +
                                "   AND ITEM_ID = ?ITEM_ID\n" +
                                "   AND DUE_DATE > ?DUE_DATE;";
                        break;
                    }

                case SQLCommand.AssetInsuranceRenewal.AddInsRenDetails:
                    {
                        query = "INSERT INTO ASSET_INSURANCE_RENEWAL_DETAIL (RENEWAL_ID,\n" +
                                "ITEM_ID,\n" +
                                "ASSET_ID,\n" +
                                "RENEWAL_AMOUNT,\n" +
                                "DUE_DATE)\n" +
                                "VALUES\n" +
                                "(?RENEWAL_ID,\n" +
                                "?ITEM_ID,\n" +
                                "?ASSET_ID,\n" +
                                "?RENEWAL_AMOUNT,\n" +
                                "?DUE_DATE)";
                        break;
                    }

                case SQLCommand.AssetInsuranceRenewal.Update:
                    {
                        query = "UPDATE ASSET_INSURANCE_RENEWAL_MASTER SET\n" +
                                    "RENEWAL_ID = ?RENEWAL_ID,\n" +
                                    "INS_ID =?INS_ID,\n" +
                                    "VOUCHER_DATE=?VOUCHER_DATE,\n" +
                                    "VOUCHER_ID=?VOUCHER_ID,\n" +
                                    "PROJECT_ID=?PROJECT_ID\n" +
                                    "WHERE RENEWAL_ID =?RENEWAL_ID";
                        break;
                    }
                case SQLCommand.AssetInsuranceRenewal.FetchInsuranceDetail:
                    {
                        query = "SELECT\n" +
                        " FNL.RENEWAL_ID,\n" +
                        " FNL.ITEM_ID,\n" +
                        " FNL.INS_ID,\n" +
                        " FNL.VOUCHER_DATE,\n" +
                        " FNL.PROJECT_ID,\n" +
                        " FNL.RENEWAL_AMOUNT,\n" +
                        " FNL.VOUCHER_NO,\n" +
                        " FNL.VOUCHER_ID,\n" +
                        " FNL.NAME_ADDRESS,\n" +
                        " FNL.NARRATION,\n" +
                        " FNL.ASSET_ID,\n" +
                        " trim(GROUP_CONCAT(FNL.LEDGER ORDER BY FNL.VOUCHER_ID DESC SEPARATOR ', ')) AS CASHBANK_LEDGER,\n" +
                        " trim(GROUP_CONCAT(FNL.EXPENSE ORDER BY FNL.VOUCHER_ID DESC SEPARATOR ', ')) AS LEDGER_ID\n" +
                        "  FROM (SELECT\n" +
                        "         T1.RENEWAL_ID,\n" +
                        "         T1.ITEM_ID,\n" +
                        "         T1.INS_ID,\n" +
                        "         T1.VOUCHER_DATE,\n" +
                        "         T1.PROJECT_ID,\n" +
                        "         T1.RENEWAL_AMOUNT,\n" +
                        "         VMT.VOUCHER_NO,\n" +
                        "         VMT.VOUCHER_ID,\n" +
                        "         VMT.NAME_ADDRESS,\n" +
                        "         VMT.NARRATION,\n" +
                        "         T1.ASSET_ID,\n" +
                        "         CASE\n" +
                        "           WHEN VT.TRANS_MODE = 'DR' THEN\n" +
                        "            VT.LEDGER_ID\n" +
                        "         END AS EXPENSE,\n" +
                        "         CASE\n" +
                        "           WHEN VT.TRANS_MODE = 'CR' THEN\n" +
                        "            VT.LEDGER_ID\n" +
                        "         END AS LEDGER\n" +
                        "          FROM VOUCHER_MASTER_TRANS VMT\n" +
                        "\n" +
                        "         INNER JOIN VOUCHER_TRANS VT\n" +
                        "            ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                        "          JOIN (SELECT\n" +
                        "                      T.RENEWAL_ID,\n" +
                        "                      T.ITEM_ID,\n" +
                        "                      T.INS_ID,\n" +
                        "                      T.VOUCHER_DATE,\n" +
                        "                      T.VOUCHER_ID,\n" +
                        "                      T.PROJECT_ID,\n" +
                        "                      T.RENEWAL_AMOUNT,\n" +
                        "                     trim(GROUP_CONCAT(T.ASSET_ID ORDER BY T.RENEWAL_ID DESC SEPARATOR ', ')) AS ASSET_ID\n" +
                        "                      FROM (SELECT AIRD.RENEWAL_ID,\n" +
                        "                              AIRD.ITEM_ID,\n" +
                        "                              AIRD.ASSET_ID AS ASSET_ID,\n" +
                        "                              AIRN.INS_ID,\n" +
                        "                              AIRN.VOUCHER_DATE,\n" +
                        "                              AIRN.VOUCHER_ID,\n" +
                        "                              AIRN.PROJECT_ID,\n" +
                        "                              RENEWAL_AMOUNT\n" +
                        "                              FROM ASSET_INSURANCE_RENEWAL_MASTER AIRN\n" +
                        "                              INNER JOIN ASSET_INSURANCE_RENEWAL_DETAIL AIRD\n" +
                        "                              ON AIRD.RENEWAL_ID = AIRN.RENEWAL_ID\n" +
                        "                              WHERE AIRN.RENEWAL_ID IN (?RENEWAL_ID) GROUP BY RENEWAL_ID,ASSET_ID) as T) AS T1\n" +
                        "                              ON VMT.VOUCHER_ID = T1.VOUCHER_ID) AS FNL GROUP BY VOUCHER_ID";
                        break;
                    }
                case SQLCommand.AssetInsuranceRenewal.FetchDetailById:
                    {
                        query = "SELECT AIRD.RENEWAL_ID,\n" +
                                   " AIRD.ITEM_ID,\n" +
                                    "AI.ASSET_NAME,\n" +
                                    " trim(GROUP_CONCAT(AIRD.ASSET_ID ORDER BY AIRD.RENEWAL_ID DESC SEPARATOR ', ')) AS ASSET_ID,\n" +
                                   " RENEWAL_AMOUNT,\n" +
                                   " DUE_DATE\n" +
                                   " FROM ASSET_INSURANCE_RENEWAL_DETAIL AIRD\n" +
                                   "LEFT JOIN ASSET_ITEM AI\n" +
                                   "ON AI.ITEM_ID = AIRD.ITEM_ID\n" +
                                   "WHERE AIRD.RENEWAL_ID IN (?RENEWAL_ID)";
                        break;
                    }

                case SQLCommand.AssetInsuranceRenewal.Delete:
                    {
                        query = "DELETE FROM ASSET_INSURANCE_RENEWAL_MASTER WHERE RENEWAL_ID=?RENEWAL_ID;";
                        break;
                    }
                case SQLCommand.AssetInsuranceRenewal.DeleteRenewalByInsID:
                    {
                        query = "SELECT RENEWAL_ID,VOUCHER_ID FROM ASSET_INSURANCE_RENEWAL_MASTER WHERE INS_ID=?INS_ID;";
                        break;
                    }

                case SQLCommand.AssetInsuranceRenewal.DeleteDetail:
                    {
                        query = "DELETE FROM ASSET_INSURANCE_RENEWAL_DETAIL WHERE RENEWAL_ID =?RENEWAL_ID;";
                        break;
                    }
            }
            return query;
        }
        #endregion
    }
}
