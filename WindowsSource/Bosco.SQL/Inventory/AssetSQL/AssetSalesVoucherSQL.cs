using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.DAO;

namespace Bosco.SQL
{
    public class AssetSalesVoucherSQL : IDatabaseQuery
    {
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;

        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.AssetSalesVoucher).FullName)
            {
                query = GetSalesVoucherSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        public string GetSalesVoucherSQL()
        {
            string query = "";
            SQLCommand.AssetSalesVoucher SqlcommandId = (SQLCommand.AssetSalesVoucher)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.AssetSalesVoucher.AddSalesMaster:
                    {
                        query = "INSERT INTO ASSET_SALES_MASTER\n" +
                                "  (SALES_DATE,\n" +
                                "   NAME,\n" +
                                "   VOUCHER_ID,\n" +
                                "   PROJECT_ID,\n" +
                                "   TAX_AMOUNT,\n" +
                                "   NET_AMOUNT,\n" +
                                "   TOTAL_AMOUNT,\n" +
                                "   DISCOUNT,\n" +
                                "   OTHER_CHARGES,\n" +
                                "   DISC_PER,\n" +
                                "   TAX_PER,\n" +
                                "   BRANCH_ID,\n" +
                                "   SOURCE_FLAG)\n" +
                                "   VALUES\n" +
                                "  (?SALES_DATE,\n" +
                                "   ?NAME,\n" +
                                "   ?VOUCHER_ID,\n" +
                                "   ?PROJECT_ID,\n" +
                                "   ?TAX_AMOUNT,\n" +
                                "   ?NET_AMOUNT,\n" +
                                "   ?TOTAL_AMOUNT,\n" +
                                "   ?DISCOUNT,\n" +
                                "   ?OTHER_CHARGES,\n" +
                                "   ?DISC_PER,\n" +
                                "   ?TAX_PER,\n" +
                                "   ?BRANCH_ID,\n" +
                                "   ?SOURCE_FLAG)";
                        break;
                    }
                case SQLCommand.AssetSalesVoucher.AddSalesDetail:
                    {
                        query = "INSERT INTO ASSET_SALES_DETAIL\n" +
                                "  (SALES_ID,\n" +
                                "   ITEM_ID,\n" +
                                "   ASSET_ID,\n" +
                                "   GROUP_ID,\n" +
                                "   LOCATION_ID,\n" +
                                "   QUANTITY,\n" +
                                "   PERCENTAGE,\n" +
                                "   AMOUNT)\n" +
                                "VALUES\n" +
                                "  (?SALES_ID,\n" +
                                "   ?ITEM_ID,\n" +
                                "   ?ASSET_ID,\n" +
                                "   ?GROUP_ID,\n" +
                                "   ?LOCATION_ID,\n" +
                                "   ?QUANTITY,\n" +
                                "   ?PERCENTAGE,\n" +
                                "   ?AMOUNT)";

                        break;
                    }
                case SQLCommand.AssetSalesVoucher.UpdateSalesMaster:
                    {
                        query = "UPDATE ASSET_SALES_MASTER\n" +
                                "   SET SALES_DATE = ?SALES_DATE,\n" +
                                "   NAME    = ?NAME,\n" +
                                "   VOUCHER_ID  = ?VOUCHER_ID,\n" +
                                "   PROJECT_ID  = ?PROJECT_ID,\n" +
                                "   TAX_AMOUNT = ?TAX_AMOUNT,\n" +
                                "   NET_AMOUNT = ?NET_AMOUNT,\n" +
                                "   TOTAL_AMOUNT = ?TOTAL_AMOUNT,\n" +
                                "   DISCOUNT = ?DISCOUNT,\n" +
                                "   OTHER_CHARGES = ?OTHER_CHARGES,\n" +
                                "   DISC_PER = ?DISC_PER,\n" +
                                "   TAX_PER = ?TAX_PER,\n" +
                                "   BRANCH_ID     = ?BRANCH_ID,\n" +
                                "   SOURCE_FLAG     = ?SOURCE_FLAG\n" +
                                "   WHERE SALES_ID   = ?SALES_ID";
                        break;
                    }
                case SQLCommand.AssetSalesVoucher.UpdateSalesDetail:
                    {
                        query = "UPDATE ASSET_SALES_DETAIL\n" +
                                "   SET ITEM_ID      = ?ITEM_ID,\n" +
                                "       GROUP_ID     = ?GROUP_ID,\n" +
                                "       ASSET_ID     = ?ASSET_ID,\n" +
                                "       LOCATION_ID  = ?LOCATION_ID,\n" +
                                "       QUANTITY     = ?QUANTITY,\n" +
                                "       PERCENTAGE     = ?PERCENTAGE,\n" +
                                "       AMOUNT       = ?AMOUNT)\n" +
                                " WHERE SALES_ID = ?SALES_ID";

                        break;
                    }
                case SQLCommand.AssetSalesVoucher.DeleteSalesMaster:
                    {
                        query = "DELETE FROM ASSET_SALES_MASTER WHERE SALES_ID=?SALES_ID";
                        break;
                    }
                case SQLCommand.AssetSalesVoucher.FetchSalesIdByVoucherId:
                    {
                        query = "SELECT SALES_ID FROM ASSET_SALES_MASTER WHERE VOUCHER_ID=?VOUCHER_ID;";
                        break;
                    }
                case SQLCommand.AssetSalesVoucher.DeleteSalesDetail:
                    {
                        query = "DELETE FROM ASSET_SALES_DETAIL WHERE SALES_ID=?SALES_ID";
                        break;
                    }
                case SQLCommand.AssetSalesVoucher.FetchSales:
                    {
                        query = "SELECT SALES_ID,\n" +
                                    "       SALES_DATE,\n" +
                                    "       SOURCE_FLAG,\n" +
                                    "       ASM.BRANCH_ID,\n" +
                                    "       TOTAL_AMOUNT,\n" +
                                    "       NAME,\n" +
                                    "       VMT.NARRATION,\n" +
                                    "       VMT.VOUCHER_NO,\n" +
                                    "       VMT.NAME_ADDRESS\n" +
                                    "  FROM ASSET_SALES_MASTER ASM\n" +
                                    " INNER JOIN VOUCHER_MASTER_TRANS VMT\n" +
                                    "    ON VMT.VOUCHER_ID = ASM.VOUCHER_ID\n" +
                                    " WHERE SOURCE_FLAG = 3\n" +
                                    "   AND ASM.PROJECT_ID = ?PROJECT_ID\n" +
                                    " AND SALES_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED;";

                        break;
                    }

                case SQLCommand.AssetSalesVoucher.FetchSalesDetailsByPartyName:
                    {
                        query = "SELECT NAME,PROJECT_ID FROM ASSET_SALES_MASTER WHERE SOURCE_FLAG =5";
                    }
                    break;

                case SQLCommand.AssetSalesVoucher.FetchDisposalMasterByPartyName:
                    {
                        query = "SELECT NAME,PROJECT_ID FROM ASSET_SALES_MASTER WHERE SOURCE_FLAG =3";
                    }
                    break;
                case SQLCommand.AssetSalesVoucher.FetchDisposal:
                    {
                        query = "SELECT SALES_ID,\n" +
                                 "       SALES_DATE,\n" +
                                 "       SOURCE_FLAG,\n" +
                                 "       ASM.BRANCH_ID,\n" +
                                 "       VMT.VOUCHER_NO,\n" +
                                 "       VMT.NARRATION,\n" +
                                 "       VMT.NAME_ADDRESS\n" +
                                 "  FROM ASSET_SALES_MASTER ASM\n" +
                                 " INNER JOIN VOUCHER_MASTER_TRANS VMT\n" +
                                 "    ON VMT.VOUCHER_ID =ASM.VOUCHER_ID\n" +
                                 " WHERE SOURCE_FLAG = 5\n" +
                                 "   AND ASM.PROJECT_ID = ?PROJECT_ID\n" +
                                 " AND SALES_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED;";

                        break;
                    }

                case SQLCommand.AssetSalesVoucher.FetchDisposalDetailsByProjectId:
                    {
                        query = "SELECT SALES_ID,\n" +
                                "       SALES_DATE,\n" +
                                "       ASM.VOUCHER_ID,\n" +
                                "       VMT.VOUCHER_NO,\n" +
                                "       VMT.NAME_ADDRESS,\n" +
                                "       VMT.NARRATION,\n" +
                                "       NAME,\n" +
                                "       SOURCE_FLAG,\n" +
                                "       ASM.BRANCH_ID\n" +
                                "  FROM ASSET_SALES_MASTER ASM INNER JOIN VOUCHER_MASTER_TRANS VMT ON VMT.VOUCHER_ID = ASM.VOUCHER_ID WHERE SOURCE_FLAG =5 AND ASM.PROJECT_ID=?PROJECT_ID AND SALES_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED";

                        break;
                    }

                case SQLCommand.AssetSalesVoucher.FetchSalesMaster:
                    {
                        query = "SELECT SALES_ID,\n" +
                          "       SALES_DATE,\n" +
                          "       ASM.VOUCHER_ID,\n" +
                          "       VMT.VOUCHER_NO,\n" +
                          "       VMT.NAME_ADDRESS,\n" +
                          "       MP.PROJECT,\n" +
                          "       MP.PROJECT_ID,\n" +
                          "       VMT.NARRATION,\n" +
                          "       VT.LEDGER_ID,\n" +
                          "       TAX_AMOUNT,\n" +
                          "       NET_AMOUNT,\n" +
                          "       TOTAL_AMOUNT,\n" +
                          "       DISCOUNT,\n" +
                          "       OTHER_CHARGES,\n" +
                          "       DISC_PER,\n" +
                          "       TAX_PER,\n" +
                          "       NAME,\n" +
                          "       ASM.BRANCH_ID\n" +
                          "  FROM ASSET_SALES_MASTER ASM  INNER JOIN VOUCHER_MASTER_TRANS VMT ON VMT.VOUCHER_ID=ASM.VOUCHER_ID\n" +
                          "INNER JOIN VOUCHER_TRANS VT ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                          "LEFT JOIN MASTER_PROJECT MP ON MP.PROJECT_ID = ASM.PROJECT_ID\n" +
                          " WHERE SALES_ID = ?SALES_ID AND VT.TRANS_MODE ='DR';";
                        //query = "SELECT SALES_ID,\n" +
                        //        "       SALES_DATE,\n" +
                        //        "       ASM.VOUCHER_ID,\n" +
                        //        "       VMT.VOUCHER_NO,\n" +
                        //        "       TAX_AMOUNT,\n" +
                        //        "       NET_AMOUNT,\n" +
                        //        "       TOTAL_AMOUNT,\n" +
                        //        "       DISCOUNT,\n" +
                        //        "       OTHER_CHARGES,\n" +
                        //        "       DISC_PER,\n" +
                        //        "       TAX_PER,\n" +
                        //        "       NAME,\n" +
                        //        "       ASM.BRANCH_ID\n" +
                        //        "  FROM ASSET_SALES_MASTER ASM  INNER JOIN VOUCHER_MASTER_TRANS VMT ON VMT.VOUCHER_ID=ASM.VOUCHER_ID\n" +
                        //        " WHERE SALES_ID = ?SALES_ID";

                        break;
                    }
                case SQLCommand.AssetSalesVoucher.FetchSalesDetail:
                    {
                        query = "SELECT ASM.SALES_ID,\n" +
                                "       AG.GROUP_NAME,\n" +
                                "       AI.ASSET_NAME,\n" +
                                "       AL.LOCATION_NAME,\n" +
                                "       CONCAT(1,'' ,AM.SYMBOL) AS QUANTITY,\n" +
                                "       ASD.PERCENTAGE,\n" +
                                "       TRUNCATE (ASD.AMOUNT/ASD.QUANTITY,2) AS AMOUNT,\n" +
                                "       ASD.ITEM_ID,\n" +
                                "       ASD.GROUP_ID,\n" +
                                "       ASD.LOCATION_ID,\n" +
                                "       ASD.ASSET_ID,\n" +
                                "       AID.AMOUNT AS RATE,\n" +
                                "       VMT.NARRATION,\n" +
                                "       VMT.NAME_ADDRESS\n" +
                                "  FROM ASSET_SALES_MASTER ASM\n" +
                                "  LEFT JOIN ASSET_SALES_DETAIL ASD\n" +
                                "    ON ASD.SALES_ID = ASM.SALES_ID\n" +
                                "  LEFT JOIN ASSET_ITEM AI\n" +
                                "    ON AI.ITEM_ID = ASD.ITEM_ID\n" +
                                "  LEFT JOIN ASSET_GROUP AG\n" +
                                "    ON ASD.GROUP_ID = AG.GROUP_ID\n" +
                                "  LEFT JOIN ASSET_STOCK_LOCATION AL\n" +
                                "    ON ASD.LOCATION_ID = AL.LOCATION_ID\n" +
                                "  LEFT JOIN VOUCHER_MASTER_TRANS VMT\n" +
                                "    ON VMT.VOUCHER_ID = ASM.VOUCHER_ID\n" +
                                "  LEFT JOIN ASSET_ITEM_DETAIL AID\n" +
                                "    ON AID.ASSET_ID = ASD.ASSET_ID\n" +
                                "  LEFT JOIN ASSET_STOCK_UNITOFMEASURE AM\n" +
                                "     ON AM.UNIT_ID = AI.UNIT_ID\n" +
                                " WHERE ASM.SALES_ID IN (?SALES_ID);";


                        break;
                    }
                case SQLCommand.AssetSalesVoucher.FetchSalesDetailbySalesId:
                    {
                        query = "SELECT ASM.SALES_ID,\n" +
                                "       AG.GROUP_NAME,\n" +
                                "       AI.ASSET_NAME,\n" +
                                "       AL.LOCATION_NAME,\n" +
                                "       ASD.QUANTITY,\n" +
                                "       ASD.PERCENTAGE,\n" +
                            // "       ASD.AMOUNT,\n" +
                                "       ASD.ITEM_ID,\n" +
                                "       ASD.GROUP_ID,\n" +
                                "       ASD.LOCATION_ID,\n" +
                                "       VMT.VOUCHER_NO,\n" +
                                "       TAX_AMOUNT,\n" +
                            //  "       TOTAL_AMOUNT AS AMOUNT,\n" + changed
                               "       SUM(AID.AMOUNT) AS AMOUNT,\n" +
                                "       NET_AMOUNT,\n" +
                                "       DISCOUNT,\n" +
                                "       OTHER_CHARGES,\n" +
                                "       DISC_PER,\n" +
                                "       TAX_PER,\n" +
                                "       trim(GROUP_CONCAT(ASD.ASSET_ID ORDER BY ASM.SALES_ID DESC SEPARATOR ', ')) as ASSET_ID\n" +
                                "  FROM ASSET_SALES_MASTER ASM\n" +
                                "  LEFT JOIN ASSET_SALES_DETAIL ASD\n" +
                                "    ON ASD.SALES_ID = ASM.SALES_ID\n" +
                                "  LEFT JOIN ASSET_ITEM AI\n" +
                                "    ON AI.ITEM_ID = ASD.ITEM_ID\n" +
                                "  LEFT JOIN ASSET_GROUP AG\n" +
                                "    ON ASD.GROUP_ID = AG.GROUP_ID\n" +
                                "  LEFT JOIN ASSET_STOCK_LOCATION AL\n" +
                                "    ON ASD.LOCATION_ID = AL.LOCATION_ID\n" +
                                "  LEFT JOIN VOUCHER_MASTER_TRANS VMT\n" +
                                "    ON VMT.VOUCHER_ID = ASM.VOUCHER_ID\n" +
                                "    LEFT JOIN ASSET_ITEM_DETAIL AID\n" +
                                "    ON AID.ASSET_ID = ASD.ASSET_ID\n" +
                                "  LEFT JOIN ASSET_STOCK_UNITOFMEASURE AM\n" +
                                "    ON AM.UNIT_ID = AI.UNIT_ID\n" +
                                " WHERE ASM.SALES_ID IN (?SALES_ID)\n" +
                            //" GROUP BY ASM.SALES_ID, ASD.ITEM_ID;";
                                " GROUP BY ASD.ITEM_ID,ASD.LOCATION_ID;";

                        break;
                    }

                case SQLCommand.AssetSalesVoucher.FetchLocationNameByItemId:
                    {
                        query = "SELECT AIT.ITEM_ID, AID.ASSET_ID, ASL.LOCATION_ID AS ASSET_LOCATION_ID, AGP.GROUP_ID AS ASSET_GROUP_ID,AMOUNT\n" +
                                "  FROM ASSET_ITEM_DETAIL AID\n" +
                                "  LEFT JOIN ASSET_STOCK_LOCATION ASL\n" +
                                "    ON ASL.LOCATION_ID = AID.LOCATION_ID\n" +
                                " INNER JOIN ASSET_ITEM AIT\n" +
                                "    ON AIT.ITEM_ID = AID.ITEM_ID\n" +
                                "  LEFT JOIN ASSET_GROUP AGP\n" +
                                "    ON AGP.GROUP_ID = AIT.ASSET_GROUP_ID\n" +
                                " WHERE AIT.ITEM_ID = ?ITEM_ID;";
                        break;
                    }
                case SQLCommand.AssetSalesVoucher.FetchAssetIdsBySalesorDisposalId:
                    {
                        query = "SELECT ASSET_ID,PURCHASE_ID,SALES_ID FROM ASSET_ITEM_DETAIL WHERE SALES_ID=?SALES_ID";
                        break;
                    }
            }
            return query;
        }
    }
}
