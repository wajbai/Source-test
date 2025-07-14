using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.DAO;

namespace Bosco.SQL
{
    public class AMCVoucherSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.AssetAMCVoucher).FullName)
            {
                query = GetAMCVoucherSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        public string GetAMCVoucherSQL()
        {
            string query = "";
            SQLCommand.AssetAMCVoucher SqlcommandId = (SQLCommand.AssetAMCVoucher)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.AssetAMCVoucher.Add:
                    {
                        query = "INSERT INTO ASSET_AMC_MASTER(AMC_ID,\n" +
                                "BILL_INVOICE_NO,\n" +
                                "PROVIDER,\n" +
                                "TOT_AMOUNT,\n" +
                                "BRANCH_ID,\n" +
                                "VOUCHER_ID,\n" +
                                "AMC_DATE,\n" +
                                "PROJECT_ID)\n" +
                                "VALUES(?AMC_ID,\n" +
                                "?BILL_INVOICE_NO,\n" +
                                "?PROVIDER,\n" +
                                "?TOT_AMOUNT,\n" +
                                "?BRANCH_ID,\n" +
                                "?VOUCHER_ID,\n" +
                                "?AMC_DATE,\n" +
                                "?PROJECT_ID)";
                        break;
                    }
                case SQLCommand.AssetAMCVoucher.AddAmcVoucherDetail:
                    {
                        query = "INSERT INTO ASSET_AMC_DETAIL(AMC_ID,\n" +
                                  "SEQUENCE_NO,\n" +
                                //  "QUANTITY,\n" +
                                  "START_DATE,\n" +
                                  "DUE_DATE,\n" +
                                  "AMOUNT,\n" +
                                  //"ASSET_ID,\n" +
                                  "ITEM_DETAIL_ID)\n" +
                                  "VALUES (?AMC_ID,\n" +
                                 "?SEQUENCE_NO,\n" +
                                 // "?QUANTITY,\n" +
                                  "?START_DATE,\n" +
                                  "?DUE_DATE,\n" +
                                  "?AMOUNT,\n" +
                                  //"?ASSET_ID,\n" +
                                  "?ITEM_DETAIL_ID)";
                        break;
                    }
                case SQLCommand.AssetAMCVoucher.Update:
                    {
                        query = "UPDATE ASSET_AMC_MASTER SET\n" +
                        "AMC_ID =?AMC_ID,\n" +
                        "BILL_INVOICE_NO =?BILL_INVOICE_NO,\n" +
                        "BRANCH_ID =?BRANCH_ID,\n" +
                        "PROVIDER =?PROVIDER,\n" +
                        "AMC_DATE =?AMC_DATE\n" +
                        "WHERE AMC_ID =?AMC_ID";
                        break;
                    }
                case SQLCommand.AssetAMCVoucher.UpdteAmcDetail:
                    {
                        query = "UPDATE ASSET_AMC_DETAIL SET\n" +
                                "       AMC_ID =?AMC_ID,\n" +
                                "       SEQUENCE_NO    = ?SEQUENCE_NO,\n" +
                                "       START_DATE     = ?START_DATE,\n" +
                                "       AMOUNT         = ?AMOUNT,\n" +
                                "       ITEM_DETAIL_ID = ?ITEM_DETAIL_ID,\n" +
                                "   WHERE AMC_ID = ?AMC_ID;";
                        break;
                    }
                case SQLCommand.AssetAMCVoucher.Delete:
                    {
                        query = "DELETE FROM ASSET_AMC_MASTER WHERE AMC_ID=?AMC_ID";
                        break;
                    }
                case SQLCommand.AssetAMCVoucher.DeleteDetail:
                    {
                        query = "DELETE FROM ASSET_AMC_DETAIL WHERE AMC_ID=?AMC_ID";
                        break;
                    }
                case SQLCommand.AssetAMCVoucher.FetchAll:
                    {
                        //query = "SELECT AMC_ID,BILL_INVOICE_NO,AMC_DATE,VMT.NARRATION,VMT.NAME_ADDRESS,VMT.VOUCHER_ID,PROVIDER,TOT_AMOUNT\n" +
                        //        " FROM ASSET_AMC_MASTER AAM\n" +
                        //        " LEFT JOIN VOUCHER_MASTER_TRANS VMT\n" +
                        //        "  ON VMT.VOUCHER_ID = AAM.VOUCHER_ID\n"+
                        //        "WHERE AAM.AMC_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED AND AAM.PROJECT_ID=?PROJECT_ID";


                        query = "SELECT AAM.AMC_ID,\n" +
                        "       BILL_INVOICE_NO,\n" +
                        "       T.QUANTITY,\n" +
                        "       AMC_DATE,\n" +
                        "       PROVIDER,\n" +
                        "       TOT_AMOUNT\n" +
                        "  FROM ASSET_AMC_MASTER AAM\n" +
                        "  LEFT JOIN ASSET_AMC_DETAIL AAD\n" +
                        "    ON AAD.AMC_ID = AAM.AMC_ID\n" +
                        "  JOIN (SELECT COUNT(AMC_ID) AS QUANTITY, AMC_ID\n" +
                        "          FROM ASSET_AMC_DETAIL\n" +
                        "         GROUP BY AMC_ID) AS T\n" +
                        "    ON T.AMC_ID = AAM.AMC_ID\n" +
                        " WHERE AAM.AMC_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                        "   AND AAM.PROJECT_ID = ?PROJECT_ID\n" +
                        " GROUP BY AAM.AMC_ID;";

                        break;
                    }
                case SQLCommand.AssetAMCVoucher.FetchDetails:
                    {
                        query = "SELECT AMC.AMC_ID,\n" +
                                "       AID.ASSET_ID,\n" + 
                                "       AC.ASSET_CLASS,\n" + 
                                "       AID.ITEM_ID,\n" + 
                                "       AMM.AMC_DATE,\n" + 
                                "       AMM.BILL_INVOICE_NO,\n" + 
                                "       AMM.VOUCHER_ID,\n" + 
                              //  "       MP.PROJECT,\n" + 
                              //  "       VMT.VOUCHER_NO,\n" + 
                              //  "       VMT.NARRATION,\n" + 
                             //   "       VMT.NAME_ADDRESS,\n" + 
                                "       START_DATE,\n" + 
                                "       DUE_DATE,\n" + 
                                "       AMC.AMOUNT,\n" + 
                                "       AI.ASSET_ITEM,\n" + 
                                "       ASL.LOCATION\n" + 
                                "  FROM ASSET_AMC_DETAIL AMC\n" + 
                                " INNER JOIN ASSET_AMC_MASTER AMM\n" + 
                                "    ON AMM.AMC_ID = AMC.AMC_ID\n" + 
                                " INNER JOIN ASSET_ITEM_DETAIL AID\n" + 
                                "    ON AID.ITEM_DETAIL_ID = AMC.ITEM_DETAIL_ID\n" + 
                                " INNER JOIN ASSET_ITEM AI\n" + 
                                "    ON AI.ITEM_ID = AID.ITEM_ID\n" + 
                                " INNER JOIN ASSET_LOCATION ASL\n" + 
                                "    ON ASL.LOCATION_ID = AID.LOCATION_ID\n" + 
                              //  " INNER JOIN VOUCHER_MASTER_TRANS VMT\n" + 
                              //  "    ON VMT.VOUCHER_ID = AMM.VOUCHER_ID\n" + 
                              //  " INNER JOIN MASTER_PROJECT MP\n" + 
                             //   "    ON MP.PROJECT_ID = VMT.PROJECT_ID\n" + 
                                " INNER JOIN ASSET_CLASS AC\n" + 
                                "    ON AI.ASSET_CLASS_ID = AC.ASSET_CLASS_ID\n" + 
                                " WHERE AMM.AMC_ID IN (?AMC_ID)";


                        break;
                    }
                case SQLCommand.AssetAMCVoucher.FetchbyId:
                    {
                        //query ="SELECT\n" + 
                        //        " FNL.AMC_ID,\n" + 
                        //        " FNL.BILL_INVOICE_NO,\n" + 
                        //        " FNL.AMC_DATE,\n" + 
                        //        " FNL.PROJECT_ID,\n" + 
                        //       // " FNL.ASSET_CLASS_ID,\n" + 
                        //        " FNL.LOCATION_ID,\n" + 
                        //        " FNL.TOT_AMOUNT,\n" + 
                        //        " FNL.ITEM_ID,\n" + 
                        //        " FNL.VOUCHER_NO,\n" + 
                        //        " FNL.VOUCHER_ID,\n" + 
                        //        " FNL.NAME_ADDRESS,\n" + 
                        //        " FNL.NARRATION,\n" +
                        //        " FNL.PROVIDER,\n" +
                        //        " FNL.ASSET_ID,\n" +
                        //        " trim(GROUP_CONCAT(FNL.LEDGER ORDER BY FNL.VOUCHER_ID DESC SEPARATOR ', ')) AS CASHBANK_LEDGER,\n" +
                        //        " trim(GROUP_CONCAT(FNL.EXPENSE ORDER BY FNL.VOUCHER_ID DESC SEPARATOR ', ')) AS LEDGER_ID\n" + 
                        //        "  FROM (SELECT\n" + 
                        //        "         T1.AMC_ID,\n" + 
                        //        "         T1.BILL_INVOICE_NO,\n" + 
                        //        "         T1.AMC_DATE,\n" + 
                        //        "         T1.PROJECT_ID,\n" + 
                        //       // "         T1.ASSET_CLASS_ID,\n" + 
                        //        "         T1.LOCATION_ID,\n" +
                        //        "         T1.PROVIDER,\n" +
                        //        "         T1.AMC_TYPE,\n" + 
                        //        "         T1.TOT_AMOUNT,\n" + 
                        //        "         T1.ITEM_ID,\n" +
                        //        "         VMT.VOUCHER_NO,\n" + 
                        //        "         VMT.VOUCHER_ID,\n" + 
                        //        "         VMT.NAME_ADDRESS,\n" + 
                        //        "         VMT.NARRATION,\n" + 
                        //        "         T1.ASSET_ID,\n" + 
                        //        "         CASE\n" + 
                        //        "           WHEN VT.TRANS_MODE = 'DR' THEN\n" + 
                        //        "            VT.LEDGER_ID\n" + 
                        //        "         END AS EXPENSE,\n" + 
                        //        "         CASE\n" + 
                        //        "           WHEN VT.TRANS_MODE = 'CR' THEN\n" + 
                        //        "            VT.LEDGER_ID\n" + 
                        //        "         END AS LEDGER\n" + 
                        //        "          FROM VOUCHER_MASTER_TRANS VMT\n" + 
                        //        "\n" + 
                        //        "         INNER JOIN VOUCHER_TRANS VT\n" + 
                        //        "            ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" + 
                        //        "          JOIN (SELECT T.AMC_ID,\n" + 
                        //        "                      T.VOUCHER_ID,\n" + 
                        //        "                      T.BILL_INVOICE_NO,\n" +
                        //        "                      T.PROVIDER,\n" +
                        //        "                      T.AMC_DATE,\n" + 
                        //        "                      T.PROJECT_ID,\n" + 
                        //       // "                      T.ASSET_CLASS_ID,\n" + 
                        //        "                      T.LOCATION_ID,\n" + 
                        //        "                      T.TOT_AMOUNT,\n" + 
                        //        "                      T.ITEM_ID,\n" +
                        //        "                      trim(GROUP_CONCAT(T.ASSET_ID ORDER BY T.AMC_ID DESC\n" + 
                        //        "                                        SEPARATOR ', ')) AS ASSET_ID\n" + 
                        //        "                 FROM (SELECT AAM.AMC_ID,\n" + 
                        //        "                              AAM.VOUCHER_ID,\n" + 
                        //        "                              AAM.BILL_INVOICE_NO,\n" + 
                        //        "                              AAM.AMC_DATE,\n" + 
                        //        "                              AAM.PROJECT_ID,\n" +
                        //        "                              AAM.PROVIDER,\n" +
                        //      //  "                              AAD.ASSET_CLASS_ID,\n" + 
                        //      //  "                              AAD.LOCATION_ID,\n" + 
                        //        "                              AAD.AMOUNT,\n" + 
                        //        "                              AAD.ASSET_ID,\n" + 
                        //        "                              AAD.ITEM_ID\n" +
                        //        "                         FROM ASSET_AMC_MASTER AAM\n" + 
                        //        "                        INNER JOIN ASSET_AMC_DETAIL AAD\n" + 
                        //        "                           ON AAM.AMC_ID = AAD.AMC_ID\n" + 
                        //        "                        WHERE AAM.AMC_ID =?AMC_ID\n" + 
                        //        "                        GROUP BY AMC_ID, ASSET_ID) as T) AS T1\n" + 
                        //        "            ON VMT.VOUCHER_ID = T1.VOUCHER_ID) AS FNL\n" + 
                        //        " GROUP BY VOUCHER_ID";

                        query = "SELECT AMC.AMC_ID,\n" +
                                "       AID.ASSET_ID,AMM.PROVIDER,\n" +
                              //  "       AC.ASSET_CLASS,\n" +
                                "       AID.ITEM_ID,\n" +
                                "       AMM.AMC_DATE,\n" +
                                "       AMM.BILL_INVOICE_NO,\n" +
                                "       AMM.VOUCHER_ID,\n" +
                             //   "       MP.PROJECT,\n" +
                              //  "       VMT.VOUCHER_NO,\n" +
                             //   "       VMT.NARRATION,\n" +
                             //   "       VMT.NAME_ADDRESS,\n" +
                                "       START_DATE,\n" +
                                "       DUE_DATE,\n" +
                                "       AMC.AMOUNT,\n" +
                                "       AI.ASSET_ITEM,\n" +
                                "       ASL.LOCATION\n" +
                                "  FROM ASSET_AMC_DETAIL AMC\n" +
                                " INNER JOIN ASSET_AMC_MASTER AMM\n" +
                                "    ON AMM.AMC_ID = AMC.AMC_ID\n" +
                                " INNER JOIN ASSET_ITEM_DETAIL AID\n" +
                                "    ON AID.ITEM_DETAIL_ID = AMC.ITEM_DETAIL_ID\n" +
                                " INNER JOIN ASSET_ITEM AI\n" +
                                "    ON AI.ITEM_ID = AID.ITEM_ID\n" +
                                " INNER JOIN ASSET_LOCATION ASL\n" +
                                "    ON ASL.LOCATION_ID = AID.LOCATION_ID\n" +
                             //   " INNER JOIN VOUCHER_MASTER_TRANS VMT\n" +
                              //  "    ON VMT.VOUCHER_ID = AMM.VOUCHER_ID\n" +
                              //  " INNER JOIN MASTER_PROJECT MP\n" +
                              //  "    ON MP.PROJECT_ID = VMT.PROJECT_ID\n" +
                              //  " INNER JOIN ASSET_CLASS AC\n" +
                              //  "    ON AI.ASSET_CLASS_ID = AC.ASSET_CLASS_ID\n" +
                                " WHERE AMM.AMC_ID IN (?AMC_ID)"; ;
                        break;
                    }
                case SQLCommand.AssetAMCVoucher.FetchAMCDetailbyId:
                    {
                        query = "SELECT AMM.AMC_ID,\n" +
                                "       AC.ASSET_CLASS,\n" +
                                "       AI.ITEM_ID,AI.ACCOUNT_LEDGER_ID AS LEDGER_ID,\n" +
                                "       AL.LOCATION_ID,\n" +
                                "       AMD.AMOUNT,\n" +
                                "       AMM.TOT_AMOUNT,\n" +
                                "       AMD.ITEM_DETAIL_ID,COUNT(AMD.SEQUENCE_NO) AS QUANTITY,\n" +
                            // "       VMT.VOUCHER_NO,\n" +
                                "       AMD.START_DATE,\n" +
                                "       AMD.DUE_DATE\n" +
                                "  FROM ASSET_AMC_MASTER AMM\n" +
                                "  LEFT JOIN ASSET_AMC_DETAIL AMD\n" +
                                "    ON AMM.AMC_ID = AMD.AMC_ID\n" +
                                " INNER JOIN ASSET_ITEM_DETAIL AID\n" +
                                "    ON AID.ITEM_DETAIL_ID = AMD.ITEM_DETAIL_ID\n" +
                                "  LEFT JOIN ASSET_ITEM AI\n" +
                                "    ON AI.ITEM_ID = AID.ITEM_ID\n" +
                                "  LEFT JOIN ASSET_CLASS AC\n" +
                                "    ON AC.ASSET_CLASS_ID = AI.ASSET_CLASS_ID\n" +
                                "  LEFT JOIN ASSET_LOCATION AL\n" +
                                "    ON AL.LOCATION_ID = AID.LOCATION_ID\n" +
                            //    "  LEFT JOIN VOUCHER_MASTER_TRANS VMT\n" +
                            //    "    ON VMT.VOUCHER_ID = AMM.VOUCHER_ID\n" +
                            //   " INNER JOIN VOUCHER_TRANS VT\n" +
                            //   "    ON VT.VOUCHER_ID = AMM.VOUCHER_ID\n" +
                            //   " INNER JOIN MASTER_PROJECT MP\n" +
                            //    "    ON MP.PROJECT_ID = VMT.PROJECT_ID\n" +
                                " WHERE AMM.AMC_ID IN (?AMC_ID)\n" +
                                "GROUP BY AI.ITEM_ID";
                        //query="SELECT AMM.AMC_ID,\n" +
                        //    "       AC.ASSET_CLASS,\n" + 
                        //    "       AI.ASSET_ITEM,\n" + 
                        //    "       AL.LOCATION,\n" + 
                        //    "       AMD.AMOUNT,\n" + 
                        //    "       AMD.ITEM_ID,\n" +
                        //    "       AMD.QUANTITY,\n" + 
                        //    "       AMD.ASSET_CLASS_ID,\n" + 
                        //    "       AMD.LOCATION_ID,\n" + 
                        //    "       VMT.VOUCHER_NO,\n" + 
                        //    "       AMD.START_DATE,\n" + 
                        //    "       AMD.DUE_DATE\n" + 
                        //  //  "       trim(GROUP_CONCAT(AMD.ASSET_ID ORDER BY AMM.AMC_ID DESC SEPARATOR ', ')) as ASSET_ID\n" + 
                        //    "  FROM ASSET_AMC_MASTER AMM\n" + 
                        //    "  LEFT JOIN ASSET_AMC_DETAIL AMD\n" + 
                        //    "    ON AMM.AMC_ID = AMD.AMC_ID\n" + 
                        //    "  LEFT JOIN ASSET_ITEM AI\n" + 
                        //    "    ON AI.ITEM_ID = AMD.ITEM_ID\n" + 
                        //    "  LEFT JOIN ASSET_CLASS AC\n" + 
                        //    "    ON AMD.GROUP_ID = AC.GROUP_ID\n" + 
                        //    "  LEFT JOIN ASSET_LOCATION AL\n" + 
                        //    "    ON AMD.LOCATION_ID = AL.LOCATION_ID\n" + 
                        //    "  LEFT JOIN VOUCHER_MASTER_TRANS VMT\n" + 
                        //    "    ON VMT.VOUCHER_ID = AMM.VOUCHER_ID\n" + 
                        //    " WHERE AMM.AMC_ID IN (?AMC_ID)\n" + 
                        //    " GROUP BY AMM.AMC_ID,AMD.ITEM_ID,AMD.LOCATION_ID";


                       

                        break;
                    }
                case SQLCommand.AssetAMCVoucher.AutoFetchProviderName:
                    {
                        query = "SELECT PROVIDER FROM ASSET_AMC_MASTER";
                        break;
                    }
                case SQLCommand.AssetAMCVoucher.DeleteAMCDetailsByAMCIdItemdetailID:
                    {
                        query = "DELETE FROM ASSET_AMC_DETAIL WHERE AMC_ID=?AMC_ID AND ITEM_DETAIL_ID=?ITEM_DETAIL_ID;";
                        break;
                    }
                case SQLCommand.AssetAMCVoucher.FetchVoucherIdbyMasterId:
                    {
                        query = "SELECT VOUCHER_ID FROM ASSET_AMC_MASTER WHERE AMC_ID IN(?AMC_ID)";
                        break;
                    }
            }
            return query;
        }

        #endregion
    }
}
