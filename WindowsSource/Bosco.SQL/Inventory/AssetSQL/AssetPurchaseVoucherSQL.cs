using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.DAO;
namespace Bosco.SQL
{
    public class AssetPurchaseVoucherSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.AssetPurchaseVoucher).FullName)
            {
                query = GetAssetPurchaseVoucherSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        #region SQL Script
        public string GetAssetPurchaseVoucherSQL()
        {
            string query = "";
            SQLCommand.AssetPurchaseVoucher SqlcommandId = (SQLCommand.AssetPurchaseVoucher)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.AssetPurchaseVoucher.AddPurchaseMaster:
                    {
                        query = "INSERT INTO ASSET_PURCHASE_MASTER\n" +
                                " (PURCHASE_DATE,\n" +
                                "VENDOR_ID,\n" +
                                "BILL_NO,\n" +
                                "MANUFACTURE_ID,\n" +
                                "TAX_AMOUNT,\n" +
                                "NET_AMOUNT,\n" +
                                "VOUCHER_ID,\n" +
                                "BRANCH_ID,\n" +
                                "INVOICE_NO,\n" +
                                "DISCOUNT,\n" +
                                "DISCOUNT_PER,\n" +
                                "TAX_PER,\n" +
                                "OTHER_CHARGES,\n" +
                                "TOTAL_AMOUNT,\n" +
                                "PROJECT_ID,\n" +
                                "SOURCE_FLAG,\n" +
                                "NAME_ADDRESS,\n" +
                                "NARRATION)\n" +
                                "VALUES\n" +
                                "  (?PURCHASE_DATE,\n" +
                                "   ?VENDOR_ID,\n" +
                                "   ?BILL_NO,\n" +
                                "   ?MANUFACTURE_ID,\n" +
                                "   ?TAX_AMOUNT,\n" +
                                "   ?NET_AMOUNT,\n" +
                                "   ?VOUCHER_ID,\n" +
                                "   ?BRANCH_ID,\n" +
                                "   ?INVOICE_NO,\n" +
                                "   ?DISCOUNT,\n" +
                                "   ?DISCOUNT_PER,\n" +
                                "   ?TAX_PER,\n" +
                                "   ?OTHER_CHARGES,\n" +
                                "   ?TOTAL_AMOUNT,\n" +
                                "   ?PROJECT_ID,\n" +
                                "   ?SOURCE_FLAG,\n" +
                                "   ?NAME_ADDRESS,\n" +
                                "   ?NARRATION)";
                        break;
                    }
                case SQLCommand.AssetPurchaseVoucher.AddPurchaseDetail:
                    {
                        query = "INSERT INTO ASSET_PURCHASE_DETAIL\n" +
                                "  (PURCHASE_ID,\n" +
                                "   ITEM_ID,\n" +
                                "   GROUP_ID,\n" +
                                "   LOCATION_ID,\n" +
                                "   QUANTITY,\n" +
                                "   RATE,\n" +
                                "   AMOUNT,\n" +
                                "   USEFUL_LIFE,\n" +
                                "   SALVAGE_LIFE)\n" +
                                "VALUES\n" +
                                "  (?PURCHASE_ID,\n" +
                                "   ?ITEM_ID,\n" +
                                "   ?GROUP_ID,\n" +
                                "   ?LOCATION_ID,\n" +
                                "   ?QUANTITY,\n" +
                                "   ?RATE,\n" +
                                "   ?AMOUNT,\n" +
                                "   ?USEFUL_LIFE,\n" +
                                "   ?SALVAGE_LIFE)";

                        break;
                    }
                case SQLCommand.AssetPurchaseVoucher.UpdatePurchaseMaster:
                    {
                        query = "UPDATE ASSET_PURCHASE_MASTER\n" +
                                "   SET PURCHASE_DATE = ?PURCHASE_DATE,\n" +
                                "   VENDOR_ID=?VENDOR_ID,\n" +
                                "   BILL_NO=?BILL_NO,\n" +
                                "   MANUFACTURE_ID=?MANUFACTURE_ID,\n" +
                                "   TAX_AMOUNT=?TAX_AMOUNT,\n" +
                                "   NET_AMOUNT=?NET_AMOUNT,\n" +
                                "   VOUCHER_ID=?VOUCHER_ID,\n" +
                                "   BRANCH_ID=?BRANCH_ID,\n" +
                                "   INVOICE_NO=?INVOICE_NO,\n" +
                                "   DISCOUNT=?DISCOUNT,\n" +
                                "   DISCOUNT_PER=?DISCOUNT_PER,\n" +
                                "   TAX_PER=?TAX_PER,\n" +
                                "   PROJECT_ID=?PROJECT_ID,\n" +
                                "   OTHER_CHARGES=?OTHER_CHARGES,\n" +
                                "   TOTAL_AMOUNT=?TOTAL_AMOUNT,\n" +
                                "   SOURCE_FLAG=?SOURCE_FLAG,\n" +
                                "   NAME_ADDRESS=?NAME_ADDRESS,\n" +
                                "   NARRATION=?NARRATION\n" +
                                "   WHERE PURCHASE_ID   = ?PURCHASE_ID";
                        break;
                    }
                case SQLCommand.AssetPurchaseVoucher.UpdatePurchaseDetail:
                    {
                        query = "UPDATE ASSET_PURCHASE_DETAIL\n" +
                                "   SET ITEM_ID      = ?ITEM_ID,\n" +
                                "       GROUP_ID     = ?GROUP_ID,\n" +
                                "       LOCATION_ID  = ?LOCATION_ID,\n" +
                                "       QUANTITY     = ?QUANTITY,\n" +
                                "       RATE         = ?RATE,\n" +
                                "       AMOUNT       = ?AMOUNT,\n" +
                                "       USEFUL_LIFE  = ?USEFUL_LIFE,\n" +
                                "       SALVAGE_LIFE = ?SALVAGE_LIFE)\n" +
                                " WHERE PURCHASE_ID = ?PURCHASE_ID";

                        break;
                    }
                case SQLCommand.AssetPurchaseVoucher.DeletePurchaseMaster:
                    {
                        query = "DELETE FROM ASSET_PURCHASE_MASTER WHERE PURCHASE_ID=?PURCHASE_ID";
                        break;
                    }
                case SQLCommand.AssetPurchaseVoucher.FetchPurchaseIdByVoucherId:
                    {
                        query = "SELECT PURCHASE_ID FROM ASSET_PURCHASE_MASTER WHERE VOUCHER_ID=?VOUCHER_ID;";
                        break;
                    }
                case SQLCommand.AssetPurchaseVoucher.DeletePurchaseDetail:
                    {
                        query = "DELETE FROM ASSET_PURCHASE_DETAIL WHERE PURCHASE_ID=?PURCHASE_ID";
                        break;
                    }
                case SQLCommand.AssetPurchaseVoucher.FetchAll:
                    {
                        //query = "SELECT APM.PURCHASE_ID,\n" +
                        //    "       APM.PURCHASE_DATE,\n" +
                        //    "       APM.VENDOR_ID,\n" +
                        //    "       APM.MANUFACTURE_ID,\n" +
                        //    "       VR.NAME AS VENDOR_NAME,\n" +
                        //    "       APM.BILL_NO,\n" +
                        //    "       APM.INVOICE_NO,\n" +
                        //    "       APM.NET_AMOUNT,\n" +
                        //    "       APM.DISCOUNT,\n" +
                        //    "       APM.DISCOUNT_PER,\n" +
                        //    "       APM.PROJECT_ID,\n" +
                        //    "       APM.TAX_PER,\n" +
                        //    "       APM.TAX_AMOUNT,\n" +
                        //    "       APM.OTHER_CHARGES,\n" +
                        //    "       APM.BRANCH_ID,\n" +
                        //    "       APM.TOTAL_AMOUNT,\n" +
                        //    "       APM.SOURCE_FLAG,\n" +
                        //    "       APM.NAME_ADDRESS,\n" +
                        //    "       APM.NARRATION,\n" +
                        //    "       VMT.VOUCHER_NO\n" +
                        //    "  FROM ASSET_PURCHASE_MASTER APM\n" +
                        //    " LEFT JOIN ASSET_STOCK_VENDOR VR\n" +
                        //    "    ON VR.VENDOR_ID = APM.VENDOR_ID\n" +
                        //    "  LEFT JOIN VOUCHER_MASTER_TRANS VMT\n" +
                        //    "    ON VMT.VOUCHER_ID = APM.VOUCHER_ID\n" +
                        //    " WHERE APM.SOURCE_FLAG = 2\n" +
                        //    "   AND PURCHASE_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                        //    "   AND APM.PROJECT_ID = ?PROJECT_ID";


                        query = "SELECT IN_OUT_ID,\n" +
                                "       IN_OUT_DATE,\n" +
                                "       BILL_INVOICE_NO,\n" +
                                "       VENDOR,\n" +
                                "       SOLD_TO,\n" +
                                "       PROJECT_ID,\n" +
                                "       VOUCHER_ID,\n" +
                                "       TOT_AMOUNT,\n" +
                                "       FLAG,\n" +
                                "       AIOM.BRANCH_ID\n" +
                                "  FROM ASSET_IN_OUT_MASTER AIOM\n" +
                                " INNER JOIN ASSET_STOCK_VENDOR ASV\n" +
                                "    ON AIOM.VENDOR_ID = ASV.VENDOR_ID";
                        break;
                    }
                case SQLCommand.AssetPurchaseVoucher.FetchPurchaseMaster:
                    {
                        query = "SELECT APM.PURCHASE_ID,\n" +
                                "       APM.PURCHASE_DATE,\n" +
                                "       APM.VENDOR_ID,\n" +
                                "       APM.MANUFACTURE_ID,\n" +
                                "       VR.NAME AS VENDOR_NAME,\n" +
                                "       APM.BILL_NO,\n" +
                                "       APM.INVOICE_NO,\n" +
                                "       APM.NET_AMOUNT,\n" +
                                "       APM.VOUCHER_ID,\n" +
                                "       APM.DISCOUNT,\n" +
                                "       APM.DISCOUNT_PER,\n" +
                                "       APM.TAX_PER,\n" +
                                "       APM.TAX_AMOUNT,\n" +
                                "       APM.PROJECT_ID,\n" +
                                "       APM.OTHER_CHARGES,\n" +
                                "       APM.BRANCH_ID,\n" +
                                "       APM.TOTAL_AMOUNT,\n" +
                                "       APM.SOURCE_FLAG,\n" +
                                "       APM.NAME_ADDRESS,\n" +
                                "       APM.NARRATION,\n" +
                                "       VMT.VOUCHER_NO,\n" +
                                "       VT.LEDGER_ID\n" +
                                "  FROM ASSET_PURCHASE_MASTER APM\n" +
                                " LEFT JOIN ASSET_STOCK_VENDOR VR\n" +
                                "    ON VR.VENDOR_ID = APM.VENDOR_ID\n" +
                                " LEFT JOIN VOUCHER_MASTER_TRANS VMT \n" +
                                " ON VMT.VOUCHER_ID=APM.VOUCHER_ID \n" +
                                " LEFT JOIN VOUCHER_TRANS VT \n" +
                                " ON VT.VOUCHER_ID=VMT.VOUCHER_ID\n" +
                                " WHERE APM.PURCHASE_ID = ?PURCHASE_ID AND VT.TRANS_MODE='CR'";

                        break;
                    }
                case SQLCommand.AssetPurchaseVoucher.FetchPurchaseDetail:
                    {
                        query = "SELECT APM.PURCHASE_ID,\n" +
                            "       AG.GROUP_NAME         ,\n" +
                            "       AI.ASSET_NAME         ,\n" +
                            "       AL.LOCATION_NAME       ,\n" +
                            "       APD.QUANTITY,\n" +
                            "       APD.RATE,\n" +
                            "       APD.AMOUNT,\n" +
                            "       APD.USEFUL_LIFE,\n" +
                            "       APD.ITEM_ID,\n" +
                            "       APD.GROUP_ID,\n" +
                            "       APD.LOCATION_ID,AID.CUSTODIANS_ID,\n" +
                            "       APD.SALVAGE_LIFE\n" +
                            "  FROM ASSET_PURCHASE_MASTER APM\n" +
                            "  LEFT JOIN ASSET_PURCHASE_DETAIL APD\n" +
                            "    ON APD.PURCHASE_ID = APM.PURCHASE_ID\n" +
                             "  LEFT JOIN ASSET_ITEM_DETAIL AID\n" +
                            "    ON AID.PURCHASE_ID = APD.PURCHASE_ID\n" +
                            "  LEFT JOIN ASSET_ITEM AI\n" +
                            "    ON AI.ITEM_ID = APD.ITEM_ID\n" +
                            "  LEFT JOIN ASSET_STOCK_VENDOR VR\n" +
                            "    ON APM.VENDOR_ID = VR.VENDOR_ID\n" +
                            "  LEFT JOIN ASSET_GROUP AG\n" +
                            "    ON APD.GROUP_ID = AG.GROUP_ID\n" +
                            "  LEFT JOIN ASSET_STOCK_LOCATION AL\n" +
                            "    ON APD.LOCATION_ID = AL.LOCATION_ID\n" +
                            " WHERE APM.PURCHASE_ID IN (?PURCHASE_ID) GROUP BY AI.ASSET_NAME;";
                        //query = "SELECT\n" +
                        //        "\n" +
                        //        " APM.PURCHASE_ID,\n" +
                        //        " AG.GROUP_NAME,\n" +
                        //        " AI.ASSET_NAME,\n" +
                        //        " AL.LOCATION_NAME,\n" +
                        //        " APD.QUANTITY,\n" +
                        //        " APD.RATE,\n" +
                        //        " APD.AMOUNT,\n" +
                        //        " APD.USEFUL_LIFE,\n" +
                        //        " APD.ITEM_ID,\n" +
                        //        " APD.GROUP_ID,\n" +
                        //        " APD.LOCATION_ID,\n" +
                        //        " APD.SALVAGE_LIFE,\n" +
                        //        " T.CUSTODIAN,T.CUSTODIANS_ID\n" +
                        //        "\n" +
                        //        "  FROM ASSET_PURCHASE_MASTER APM\n" +
                        //        " INNER JOIN ASSET_PURCHASE_DETAIL APD\n" +
                        //        "    ON APM.PURCHASE_ID = APD.PURCHASE_ID\n" +
                        //        "  LEFT JOIN ASSET_ITEM AI\n" +
                        //        "    ON AI.ITEM_ID = APD.ITEM_ID\n" +
                        //        "  LEFT JOIN ASSET_STOCK_VENDOR VR\n" +
                        //        "    ON APM.VENDOR_ID = VR.VENDOR_ID\n" +
                        //        "  LEFT JOIN ASSET_GROUP AG\n" +
                        //        "    ON APD.GROUP_ID = AG.GROUP_ID\n" +
                        //        "  LEFT JOIN ASSET_STOCK_LOCATION AL\n" +
                        //        "    ON APD.LOCATION_ID = AL.LOCATION_ID\n" +
                        //        "\n" +
                        //        " INNER JOIN (SELECT ASD.NAME AS CUSTODIAN,ASD.CUSTODIANS_ID,\n" +
                        //        "                    AID.PURCHASE_ID,\n" +
                        //        "                    LOCATION_ID,\n" +
                        //        "                    ITEM_ID\n" +
                        //        "               FROM ASSET_ITEM_DETAIL AID\n" +
                        //        "\n" +
                        //        "              INNER JOIN ASSET_CUSTODIANS ASD\n" +
                        //        "                 ON AID.CUSTODIANS_ID = ASD.CUSTODIANS_ID\n" +
                        //        "              WHERE PURCHASE_ID IN (?PURCHASE_ID)\n" +
                        //        "              GROUP BY PURCHASE_ID, AID.LOCATION_ID, AID.CUSTODIANS_ID) AS T\n" +
                        //        "\n" +
                        //        "    ON APD.LOCATION_ID = T.LOCATION_ID\n" +
                        //        "   and APD.ITEM_ID = T.ITEM_ID\n" +
                        //        " WHERE APM.PURCHASE_ID IN (?PURCHASE_ID);";

                        break;
                    }

                case SQLCommand.AssetPurchaseVoucher.FetchReceiveMaster:
                    {
                        query = "SELECT APM.PURCHASE_ID,\n" +
                                "       APM.PURCHASE_DATE,\n" +
                                "       APM.VENDOR_ID,\n" +
                                "       AM.NAME AS MANUFACTURE_NAME,\n" +
                                "       APM.BILL_NO,\n" +
                                "       APM.MANUFACTURE_ID,\n" +
                                "       APM.DISCOUNT,\n" +
                                "       APM.DISCOUNT_PER,\n" +
                                "       APM.TAX_PER,\n" +
                                "       APM.TAX_AMOUNT,\n" +
                                "       APM.OTHER_CHARGES,\n" +
                                "       APM.INVOICE_NO,\n" +
                                "       APM.NET_AMOUNT,\n" +
                                "       APM.PROJECT_ID,\n" +
                                "       APM.VOUCHER_ID,\n" +
                                "       APM.BRANCH_ID,\n" +
                                "       APM.TOTAL_AMOUNT,\n" +
                                "       APM.SOURCE_FLAG\n" +
                                "  FROM ASSET_PURCHASE_MASTER APM\n" +
                                " LEFT JOIN ASSET_STOCK_MANUFACTURE AM\n" +
                                "    ON AM.MANUFACTURE_ID = APM.MANUFACTURE_ID\n" +
                                " WHERE APM.SOURCE_FLAG = 4 AND PURCHASE_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED AND APM.PROJECT_ID=?PROJECT_ID";
                        break;
                    }

                case SQLCommand.AssetPurchaseVoucher.FetchReceiveMasterByID:
                    {
                        query = "SELECT APM.PURCHASE_ID,\n" +
                                "       APM.PURCHASE_DATE,\n" +
                                "       APM.VENDOR_ID,\n" +
                                "       APM.MANUFACTURE_ID,\n" +
                                "       VR.NAME AS VENDOR_NAME,\n" +
                                "       APM.BILL_NO,\n" +
                                "       APM.INVOICE_NO,\n" +
                                "       APM.NET_AMOUNT,\n" +
                                "       APM.VOUCHER_ID,\n" +
                                "       APM.DISCOUNT,\n" +
                                "       APM.DISCOUNT_PER,\n" +
                                "       APM.TAX_PER,\n" +
                                "       APM.TAX_AMOUNT,\n" +
                                "       APM.PROJECT_ID,\n" +
                                "       APM.OTHER_CHARGES,\n" +
                                "       APM.BRANCH_ID,\n" +
                                "       APM.TOTAL_AMOUNT,\n" +
                                "       APM.SOURCE_FLAG,\n" +
                                "       APM.NAME_ADDRESS,\n" +
                                "       APM.NARRATION,\n" +
                                "       VMT.VOUCHER_NO,\n" +
                                "       0 AS LEDGER_ID\n" +
                                "  FROM ASSET_PURCHASE_MASTER APM\n" +
                                "  LEFT JOIN ASSET_STOCK_VENDOR VR\n" +
                                "    ON VR.VENDOR_ID = APM.VENDOR_ID\n" +
                                "  LEFT JOIN VOUCHER_MASTER_TRANS VMT\n" +
                                "    ON VMT.VOUCHER_ID = APM.VOUCHER_ID\n" +
                                " WHERE APM.PURCHASE_ID = ?PURCHASE_ID";
                        break;
                    }
                case SQLCommand.AssetPurchaseVoucher.FetchAssetSourceFlagById:
                    {
                        query = "SELECT SOURCE_FLAG FROM ASSET_PURCHASE_MASTER WHERE PURCHASE_ID=?PURCHASE_ID";
                        break;
                    }

            }
            return query;
        }
        #endregion
    }
}
