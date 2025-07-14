using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    class InsuranceSQL:IDatabaseQuery
    {
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;

        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.AssetInsuranchVoucher).FullName)
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
            SQLCommand.AssetInsuranchVoucher SqlcommandId = (SQLCommand.AssetInsuranchVoucher)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {

                #region Insurance Masters

                case SQLCommand.AssetInsuranchVoucher.SaveInsuranceMaster:
                    {
                        query = "INSERT INTO ASSET_INSURANCE_MASTER\n" +
                                "  (VOUCHER_DATE, VOUCHER_ID, NAME_ADDRESS, NARRATION,PROJECT_ID,LEDGER_ID)\n" + 
                                "VALUES\n" +
                                "  (?VOUCHER_DATE, ?VOUCHER_ID, ?NAME_ADDRESS, ?NARRATION,?PROJECT_ID,?LEDGER_ID);";
                        break;
                    }
                case SQLCommand.AssetInsuranchVoucher.UpdateInsuranceMaster:
                    {
                        query = "UPDATE ASSET_INSURANCE_MASTER\n" +
                                "   SET VOUCHER_DATE    = ?VOUCHER_DATE,\n" +
                            //    "       VOUCHER_NO = ?VOUCHER_NO,\n" +
                                "       LEDGER_ID = ?LEDGER_ID,\n" +
                                "       NAME_ADDRESS = ?NAME_ADDRESS,\n" +
                                "       NARRATION             = ?NARRATION\n" +
                                " WHERE INS_ID = ?INS_ID";
                        break;
                    }
               
                case SQLCommand.AssetInsuranchVoucher.FetchInsDetailbyProject:
                    {
                        //query = "SELECT AIM.INS_ID,\n" +
                        //        "       AID.ITEM_ID,\n" + 
                        //        "       AIM.VOUCHER_DATE,\n" + 
                        //        "       VMT.VOUCHER_NO,\n" + 
                        //        "       LEDGER_NAME,\n" + 
                        //        "       AIM.NAME_ADDRESS,\n" + 
                        //        "       AIM.NARRATION,\n" + 
                        //        "       AIM.PROJECT_ID\n" + 
                        //        "  FROM ASSET_INSURANCE_MASTER AIM\n" + 
                        //        "  LEFT JOIN MASTER_LEDGER ML\n" + 
                        //        "    ON ML.LEDGER_ID = AIM.LEDGER_ID\n" + 
                        //        " INNER JOIN VOUCHER_MASTER_TRANS VMT\n" + 
                        //        "    ON AIM.VOUCHER_ID = VMT.VOUCHER_ID\n" + 
                        //        "  LEFT JOIN ASSET_INSURANCE_MASTER_DETAIL AID\n" + 
                        //        "    ON AIM.INS_ID = AID.INS_ID\n" + 
                        //        " WHERE AIM.PROJECT_ID = ?PROJECT_ID\n" + 
                        //        "   AND AIM.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO";
                        query = "SELECT AIM.INS_ID,\n" +
                                "       AIMD.ITEM_ID,\n" +
                                "       ASSET_NAME,\n" +
                                "       AIMD.ASSET_GROUP_ID,\n" +
                                "       GROUP_NAME,\n" +
                                "       AIMD.ASSET_LOCATION_ID,\n" +
                                "       LOCATION_NAME as LOCATION,\n" +
                                "       VALUE,\n" +
                                "       CASE\n" +
                                "         WHEN IS_INSURANCE_NEED = 1 THEN\n" +
                                "          'Yes'\n" +
                                "         else\n" +
                                "          'No'\n" +
                                "       end as ISINSURANCE,\n" +
                                "       ASSET_ID,\n" +
                                "       AIM.VOUCHER_DATE,\n" +
                                "       --  VMT.VOUCHER_NO,\n" +
                                "       --  LEDGER_NAME,\n" +
                                "       --  AIM.NAME_ADDRESS,\n" +
                                "       --  AIM.NARRATION,\n" +
                                "       AID.AGENT,\n" +
                                "       AID.PROVIDER,\n" +
                                "       AID.INSURANCE_TYPE_ID,\n" +
                                "       POLICY,\n" +
                                "       POLICY_NO,\n" +
                                "       START_DATE,\n" +
                                "       DUE_DATE,\n" +
                                "       PREMIUM_AMOUNT,\n" +
                                "       AIM.PROJECT_ID\n" +
                                "  FROM ASSET_INSURANCE_MASTER AIM\n" +
                                "  LEFT JOIN MASTER_LEDGER ML\n" +
                                "    ON ML.LEDGER_ID = AIM.LEDGER_ID\n" +
                                "  LEFT JOIN VOUCHER_MASTER_TRANS VMT\n" +
                                "    ON AIM.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                                "  LEFT JOIN ASSET_INSURANCE_MASTER_DETAIL AIMD\n" +
                                "    ON AIM.INS_ID = AIMD.INS_ID\n" +
                                " LEFT JOIN ASSET_ITEM AI\n" +
                                "    ON AIMD.ITEM_ID = AI.ITEM_ID\n" +
                                " LEFT JOIN ASSET_GROUP AG\n" +
                                "    ON AIMD.ASSET_GROUP_ID = AG.GROUP_ID\n" +
                                " LEFT JOIN ASSET_STOCK_LOCATION AST\n" +
                                "    ON AIMD.ASSET_LOCATION_ID = AST.LOCATION_ID\n" +
                                "  LEFT JOIN ASSET_INSURANCE_DETAIL AID\n" +
                                "    ON AIMD.INS_ID = AID.INS_ID\n" +
                                " LEFT JOIN ASSET_INSURANCE_TYPE AIT\n" +
                                "    ON AID.INSURANCE_TYPE_ID = AIT.INSURANCE_TYPE_ID\n" +
                                " WHERE AIM.PROJECT_ID = ?PROJECT_ID\n" +
                                "   AND AIM.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO;"; 

                        break;
                    }
                case SQLCommand.AssetInsuranchVoucher.FetchRenewalByProject:
                    { 
                        query= "SELECT AIRM.RENEWAL_ID,\n" +
                                "       AIRM.INS_ID,\n" + 
                                "       AIRM.VOUCHER_DATE,\n" + 
                                "       AIRM.VOUCHER_ID,\n" + 
                                "       AID.ITEM_ID,\n" + 
                                "       ASSET_NAME,\n" + 
                                "       AID.ASSET_ID,\n" + 
                                "       RENEWAL_AMOUNT,\n" + 
                                "       DUE_DATE\n" + 
                                "  FROM ASSET_INSURANCE_RENEWAL_MASTER AIRM\n" + 
                                " INNER JOIN ASSET_INSURANCE_RENEWAL_DETAIL AID\n" + 
                                "    ON AIRM.RENEWAL_ID = AID.RENEWAL_ID\n" + 
                                " INNER JOIN ASSET_ITEM AI\n" + 
                                "    ON AID.ITEM_ID = AI.ITEM_ID\n" + 
                                " INNER JOIN ASSET_INSURANCE_MASTER AIM\n" + 
                                "    ON AIM.INS_ID = AIRM.INS_ID\n" + 
                                " WHERE AIRM.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" + 
                                "   AND AIRM.PROJECT_ID = ?PROJECT_ID;";

                        break;
                    }

                case SQLCommand.AssetInsuranchVoucher.DeleteInsuranceMaster:
                    {
                        query = "DELETE FROM ASSET_INSURANCE_MASTER WHERE INS_ID=?INS_ID;";
                        break;
                    }

                case SQLCommand.AssetInsuranchVoucher.FetchInsuanceMaster:
                    {
                        query = "SELECT VOUCHER_DATE,\n" +
                                "       LEDGER_ID,\n" +
                                "       AIM.PROJECT_ID,\n" +
                                "       PROJECT,\n" +
                                "       NAME_ADDRESS,\n" +
                                "       NARRATION\n" +
                                "  FROM ASSET_INSURANCE_MASTER AIM\n" +
                                " INNER JOIN MASTER_PROJECT MP\n" +
                                "    ON AIM.PROJECT_ID = MP.PROJECT_ID\n" +
                                " WHERE INS_ID = ?INS_ID;";
                        break;
                    }
                case SQLCommand.AssetInsuranchVoucher.RenewInsurance:
                    {
                        query = "SELECT INS_ID,\n" +
                                "       ASSET_NAME,\n" +
                                "       AIM.ITEM_ID,0 as RENEWAL_AMOUNT,AID.ASSET_ID\n" + 
                           //     "       trim(GROUP_CONCAT(AID.ASSET_ID ORDER BY INS_ID DESC SEPARATOR ', ')) as ASSET_ID\n" + 
                                "  FROM ASSET_INSURANCE_MASTER_DETAIL AIM\n" + 
                                " INNER JOIN ASSET_ITEM AI\n" + 
                                "    ON AIM.ITEM_ID = AI.ITEM_ID\n" + 
                                " INNER JOIN ASSET_ITEM_DETAIL AID\n" + 
                                "    ON AID.ASSET_ID = AIM.ASSET_ID\n" + 
                                " WHERE INS_ID = ?INS_ID\n" + 
                                "   AND AIM.ITEM_ID = ?ITEM_ID\n" + 
                                " GROUP BY INS_ID, ASSET_NAME, VALUE;";
                        break;
                    }

                #endregion

                #region Asset Insurance Masters Details

                case SQLCommand.AssetInsuranchVoucher.AddInsuranceMastersDetail:
                    {
                        query = "INSERT INTO ASSET_INSURANCE_MASTER_DETAIL\n" +
                                "  (INS_ID,ASSET_GROUP_ID, ASSET_LOCATION_ID,ITEM_ID,ASSET_ID, VALUE, IS_INSURANCE_NEED)\n" + 
                                "VALUES\n" +
                                "  (?INS_ID,?ASSET_GROUP_ID, ?ASSET_LOCATION_ID, ?ITEM_ID, ?ASSET_ID, ?VALUE, ?IS_INSURANCE_NEED)";
                        break;
                    }
                case SQLCommand.AssetInsuranchVoucher.UpdaterInsuranceMastersDetail:
                    {
                        query = "UPDATE ASSET_INSURANCE_MASTER_DETAIL\n" +
                                "   SET ASSET_GROUP_ID    = ?ASSET_GROUP_ID,\n" + 
                                "       ASSET_LOCATION_ID = ?ASSET_LOCATION_ID,\n" +
                                "       ITEM_ID = ?ITEM_ID,\n" +
                                "       ASSET_ID = ?ASSET_ID,\n" +
                                "       VALUE             = ?VALUE,\n" +
                                "       IS_INSURANCE_NEED   = ?IS_INSURANCE_NEED\n" + 
                                " WHERE INS_ID = ?INS_ID";
                        break;
                    }
                case SQLCommand.AssetInsuranchVoucher.DeleteInsuranceMastersDetail:
                    {
                        query = "DELETE FROM ASSET_INSURANCE_MASTER_DETAIL WHERE INS_ID =?INS_ID";
                        break;
                    }
                case SQLCommand.AssetInsuranchVoucher.FetchAllInsuranceMastersDetail:
                    {
                        query = "SELECT INS_ID,\n" +
                                "       AG.GROUP_NAME AS ASSET_GROUP,\n" + 
                                "       AL.LOCATION_NAME AS LOCATION,\n" + 
                                "       ASSET_NAME,\n" + 
                                "       trim(GROUP_CONCAT(AID.ASSET_ID ORDER BY INS_ID DESC SEPARATOR ', ')) as ASSET_ID,\n" + 
                                "       VALUE,\n" + 
                                "       CASE\n" + 
                                "         WHEN IS_INSURANCE_NEED = 1 THEN\n" + 
                                "          'Yes'\n" + 
                                "         else\n" + 
                                "          'No'\n" + 
                                "       end as ISINSURANCE\n" + 
                                "  FROM ASSET_INSURANCE_MASTER_DETAIL AIM\n" + 
                                " INNER JOIN ASSET_GROUP AG\n" + 
                                "    ON AG.GROUP_ID = AIM.ASSET_GROUP_ID\n" + 
                                " INNER JOIN ASSET_ITEM AI\n" + 
                                "    ON AI.ITEM_ID = AIM.ITEM_ID\n" + 
                                " INNER JOIN ASSET_ITEM_DETAIL AID\n" + 
                                "    ON AID.ASSET_ID = AIM.ASSET_ID\n" + 
                                " INNER JOIN ASSET_STOCK_LOCATION AL\n" + 
                                "    ON AL.LOCATION_ID = AIM.ASSET_LOCATION_ID WHERE INS_ID=?INS_ID\n" + 
                                " GROUP BY INS_ID, ASSET_GROUP, LOCATION, ASSET_NAME, VALUE;";




                        break;
                    }
                case SQLCommand.AssetInsuranchVoucher.FetchInsuranceMastersDetail:
                    {
                       // query = "SELECT INS_ID,ASSET_GROUP_ID  AS GROUP_ID,ASSET_LOCATION_ID AS LOCATION_ID,ITEM_ID,ASSET_ID,VALUE,IS_INSURANCE_NEED  AS Id FROM ASSET_INSURANCE_MASTER_DETAIL WHERE INS_ID=?INS_ID";
                        query = "SELECT INS_ID,\n" +
                                "       ASSET_GROUP_ID AS GROUP_ID,\n" + 
                                "       ASSET_LOCATION_ID AS LOCATION_ID,\n" + 
                                "       ITEM_ID,\n" + 
                                "       trim(GROUP_CONCAT(ASSET_ID ORDER BY INS_ID DESC SEPARATOR ', ')) as ASSET_ID,\n" + 
                                "       VALUE,\n" + 
                                "       IS_INSURANCE_NEED AS Id\n" + 
                                "  FROM ASSET_INSURANCE_MASTER_DETAIL\n" + 
                                " WHERE INS_ID = ?INS_ID\n" + 
                                "\n" + 
                                " GROUP BY ASSET_LOCATION_ID, ITEM_ID;";

                        break;
                    }

                #endregion

                #region Asset Insurance details

                case SQLCommand.AssetInsuranchVoucher.AddInsuranceDetails:
                    {
                        query = "INSERT INTO ASSET_INSURANCE_DETAIL\n" +
                                    "  (INS_ID,\n" + 
                                    "   INSURANCE_TYPE_ID,\n" + 
                                    "   PROVIDER,\n" + 
                                    "   AGENT,\n" + 
                                    "   POLICY,\n" +
                                    "   POLICY_NO,\n" + 
                                    "   START_DATE,\n" + 
                                    "   DUE_DATE,\n" + 
                                    "   PREMIUM_AMOUNT,ITEM_ID)\n" + 
                                    "VALUES\n" + 
                                    "  (?INS_ID,\n" + 
                                    "   ?INSURANCE_TYPE_ID,\n" + 
                                    "   ?PROVIDER,\n" + 
                                    "   ?AGENT,\n" + 
                                    "   ?POLICY,\n" +
                                    "   ?POLICNO,\n" + 
                                    "   ?START_DATE,\n" + 
                                    "   ?DUE_DATE,\n" + 
                                    "   ?PREMIUM_AMOUNT,?ITEM_ID)";

                        break;
                    }

                case SQLCommand.AssetInsuranchVoucher.UpdateInsuranceDetails:
                    {
                        query = "UPDATE ASSET_INSURANCE_DETAIL SET\n" +
                            //    "   SET INS_ID            = ?INS_ID,\n" +
                                "       INSURANCE_TYPE_ID = ?INSURANCE_TYPE_ID,\n" +
                                "       PROVIDER          = ?PROVIDER,\n" +
                                "       AGENT             = ?AGENT,\n" +
                                "       POLICY            = ?POLICY,\n" +
                                "       POLICY_NO         = ?POLICY_NO,\n" +
                                "       START_DATE        = ?START_DATE,\n" +
                                "       DUE_DATE          = ?DUE_DATE,\n" +
                                "       PREMIUM_AMOUNT    = ?PREMIUM_AMOUNT\n" +
                                " WHERE INS_ID = ?INS_ID;";

                        break;
                    }

                case SQLCommand.AssetInsuranchVoucher.FetchAllInsuranceDetails:
                    {
                        query = "SELECT INS_ID, AIT.NAME AS NAME,\n" +
                                "       PROVIDER,\n" + 
                                "       AGENT,\n" + 
                                "       POLICY,\n" + 
                                "       POLICY_NO,\n" + 
                                "       START_DATE,\n" + 
                                "       DUE_DATE,\n" + 
                                "       PREMIUM_AMOUNT\n" + 
                                "  FROM ASSET_INSURANCE_DETAIL AID\n" + 
                                " LEFT JOIN ASSET_INSURANCE_TYPE AIT\n" + 
                                "    ON AIT.INSURANCE_TYPE_ID = AID.INSURANCE_TYPE_ID WHERE INS_ID=?INS_ID;";


                        break;
                    }
                case SQLCommand.AssetInsuranchVoucher.DeleteInsuranceDetails:
                    {
                        query = "DELETE FROM ASSET_INSURANCE_DETAIL WHERE INS_ID=?INS_ID {AND ITEM_ID=?ITEM_ID}";
                        break;
                    }
                case SQLCommand.AssetInsuranchVoucher.FetchInsuranceDetails:
                    {
                        query = "SELECT INS_ID, INSURANCE_TYPE_ID, PROVIDER, AGENT, POLICY,POLICY_NO as POLICNO, START_DATE, DUE_DATE, PREMIUM_AMOUNT FROM ASSET_INSURANCE_DETAIL WHERE INS_ID=?INS_ID { AND ITEM_ID=?ITEM_ID}";
                        break;
                    }
                #endregion

            }
            return query;
        }
        #endregion
    }
}
