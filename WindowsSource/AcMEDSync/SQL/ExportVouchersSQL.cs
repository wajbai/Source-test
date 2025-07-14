using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcMEDSync.SQL
{
    public class ExportVouchersSQL
    {
        #region Export Vouchers
        public string GetQuery(AcMEDSync.SQL.EnumDataSyncSQLCommand.ExportVouchers ExportSQL)
        {
            string sVouchersQuery = string.Empty;
            switch (ExportSQL)
            {
                case EnumDataSyncSQLCommand.ExportVouchers.FetchMasterVouchers:
                    {
                        sVouchersQuery =  "SELECT MT.VOUCHER_ID,\n" +
                                            "       MT.VOUCHER_DATE,\n" + 
                                            "       MT.VOUCHER_NO,\n" + 
                                            "       MP.PROJECT,\n" + 
                                            "       MT.VOUCHER_TYPE,\n" + 
                                            "       MT.VOUCHER_SUB_TYPE,\n" + 
                                            "       MCH.FC_PURPOSE,\n" + 
                                            "       MD.NAME,\n" + 
                                            "       MT.NAME_ADDRESS,\n" + 
                                            "       MT.CONTRIBUTION_TYPE,\n" + 
                                            "       MT.CONTRIBUTION_AMOUNT,\n" + 
                                            "       ME.COUNTRY AS CURRENCY_COUNTRY,\n" + 
                                            "       MT.EXCHANGE_RATE,\n" +
                                            "       MC.COUNTRY AS EXCHANGE_COUNTRY,\n" + 
                                            "       MT.NARRATION,\n" + 
                                            "       MT.CALCULATED_AMOUNT,\n" + 
                                            "       MT.ACTUAL_AMOUNT\n" + 
                                            "\n" + 
                                            "  FROM VOUCHER_MASTER_TRANS MT\n" + 
                                            " INNER JOIN VOUCHER_TRANS VT\n" + 
                                            "    ON VT.VOUCHER_ID = MT.VOUCHER_ID\n" + 
                                            " INNER JOIN MASTER_LEDGER ML\n" + 
                                            "    ON ML.LEDGER_ID = VT.LEDGER_ID\n" + 
                                            "  LEFT JOIN MASTER_DONAUD MD\n" + 
                                            "    ON MD.DONAUD_ID = MT.DONOR_ID\n" + 
                                            "  LEFT JOIN master_contribution_head MCH\n" + 
                                            "    ON MCH.CONTRIBUTION_ID = MT.PURPOSE_ID\n" + 
                                            "  LEFT JOIN MASTER_PROJECT MP\n" + 
                                            "    ON MP.PROJECT_ID = MT.PROJECT_ID\n" + 
                                            "  LEFT JOIN MASTER_COUNTRY MC\n" + 
                                            "    ON MC.COUNTRY_ID = MT.EXCHANGE_COUNTRY_ID\n" + 
                                            "   LEFT JOIN MASTER_COUNTRY ME\n" + 
                                            "    ON ME.COUNTRY_ID = MT.CURRENCY_COUNTRY_ID\n" + 
                                            " WHERE MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" + 
                                            "   AND MT.PROJECT_ID in (?PROJECT_ID)\n" + 
                                            "   AND MT.STATUS = 1\n" + 
                                            " GROUP BY MT.VOUCHER_ID\n" + 
                                            " ORDER BY MT.PROJECT_ID, MT.VOUCHER_NO";

                        break;
                    }
                case EnumDataSyncSQLCommand.ExportVouchers.FetchProjects:
                    {
                        sVouchersQuery = "SELECT PROJECT_ID,PROJECT FROM MASTER_PROJECT WHERE DELETE_FLAG=0";
                        break;
                    }
                case EnumDataSyncSQLCommand.ExportVouchers.FetchDonors:
                    {
                        sVouchersQuery = "SELECT NAME, \n" +
                                        "PLACE,\n" +
                                        "COMPANY_NAME,\n" +
                                        "COUNTRY,\n" +
                                        "PINCODE,\n" +
                                        "PHONE,\n" +
                                        "FAX,\n" +
                                        "EMAIL,\n" +
                                        "URL,\n" +
                                        "STATE,\n" +
                                        "ADDRESS,\n" +
                                        "PAN\n" + 
                                        "  FROM MASTER_DONAUD MD\n" +
                                        " INNER JOIN VOUCHER_MASTER_TRANS MT\n" +
                                        "    ON MT.DONOR_ID = MD.DONAUD_ID\n" +
                                        "  LEFT JOIN MASTER_COUNTRY MC\n" +
                                        "    ON MC.COUNTRY_ID = MD.DONAUD_ID\n" +
                                        " WHERE  MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                        "   AND  \n" +
                                        " PROJECT_ID in (?PROJECT_ID)\n" +
                                        "   AND MT.STATUS = 1 \n" +
                                         " GROUP BY MT.DONOR_ID ";
                                        
                        break;
                    }
                case EnumDataSyncSQLCommand.ExportVouchers.FetchVoucherTransactions:
                    {
                        sVouchersQuery = "SELECT MT.VOUCHER_ID,\n" +
                                    "       VT.SEQUENCE_NO,\n" +
                                    "       ML.LEDGER_NAME,\n" +
                                    "       VT.AMOUNT,\n" +
                                    "       VT.TRANS_MODE,\n" +
                                    "       VT.CHEQUE_NO,\n" +
                                    "       VT.MATERIALIZED_ON\n" +
                                    "  FROM VOUCHER_MASTER_TRANS MT\n" +
                                    " INNER JOIN VOUCHER_TRANS VT\n" +
                                    "    ON VT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                                    " INNER JOIN MASTER_LEDGER ML\n" +
                                    "    ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                                    " WHERE MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO \n" +
                                    "   AND MT.PROJECT_ID in (?PROJECT_ID)\n" +
                                    "   AND MT.STATUS = 1";

                        break;
                    }
                case EnumDataSyncSQLCommand.ExportVouchers.FetchVoucherCostCentres:
                    {
                        sVouchersQuery = "SELECT MT.VOUCHER_ID,VCC.SEQUENCE_NO,\n" +
                                        "       MCC.COST_CENTRE_NAME,\n" +
                                        "       ML.LEDGER_NAME,\n" +
                                        "       VCC.AMOUNT\n" +
                                        "  FROM VOUCHER_MASTER_TRANS MT\n" +
                                        "\n" +
                                        " INNER JOIN VOUCHER_TRANS VT\n" +
                                        "    ON VT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                                        " INNER JOIN VOUCHER_CC_TRANS VCC\n" +
                                        "    ON VCC.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                        " INNER JOIN MASTER_LEDGER ML\n" +
                                        "    ON ML.LEDGER_ID = VCC.LEDGER_ID\n" +
                                        " INNER JOIN MASTER_COST_CENTRE MCC\n" +
                                        "    ON MCC.COST_CENTRE_ID = VCC.COST_CENTRE_ID\n" +
                                        "\n" +
                                        " WHERE MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                        "   AND MT.PROJECT_ID in (?PROJECT_ID)\n" +
                                        "   AND MT.STATUS = 1\n" +
                                        " GROUP BY VOUCHER_ID, MCC.COST_CENTRE_NAME";

                        break;
                    }
                case EnumDataSyncSQLCommand.ExportVouchers.FetchBankDetails:
                    {
                        sVouchersQuery = "SELECT MB.BANK_CODE,\n" +
                                        "       MB.BANK,\n" +
                                        "       MB.BRANCH,\n" +
                                        "       MB.ADDRESS,\n" +
                                        "       MB.IFSCCODE,\n" +
                                        "       MB.MICRCODE,\n" +
                                        "       MB.CONTACTNUMBER,\n" +
                                        "       MB.SWIFTCODE,\n" +
                                        "       ACCOUNT_CODE,\n" +
                                        "       ACCOUNT_NUMBER,\n" +
                                        "       ACCOUNT_HOLDER_NAME,\n" +
                                        "       IS_FCRA_ACCOUNT,\n" +
                                        "       MBA.LEDGER_ID\n" + 
                                        "FROM VOUCHER_MASTER_TRANS MT\n" +
                                        " INNER JOIN VOUCHER_TRANS VT\n" +
                                        "    ON MT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                        " INNER JOIN MASTER_BANK_ACCOUNT MBA\n" +
                                        "    ON VT.LEDGER_ID = MBA.LEDGER_ID\n" +
                                        " INNER JOIN MASTER_BANK MB\n" +
                                        "    ON MBA.BANK_ID = MBA.BANK_ID\n" +
                                        " WHERE MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                        "   AND MT.PROJECT_ID IN (?PROJECT_ID)\n" +
                                        "   AND MT.STATUS = 1\n" +
                                        " GROUP BY MBA.LEDGER_ID";

                        break;
                    }
                case EnumDataSyncSQLCommand.ExportVouchers.FetchLedgerBalance:
                    {
                        sVouchersQuery = "SELECT MP.PROJECT,\n" +
                                        "       LB.BALANCE_DATE,\n" +
                                        "       MHL.LEDGER_NAME,\n" +
                                        "       AMOUNT,\n" +
                                        "       TRANS_MODE,\n" +
                                        "       TRANS_FLAG\n" +
                                        "  FROM LEDGER_BALANCE LB\n" +
                                        "  LEFT JOIN MASTER_PROJECT MP\n" +
                                        "    ON MP.PROJECT_ID = LB.PROJECT_ID\n" +
                                        " INNER JOIN HEADOFFICE_MAPPED_LEDGER HML\n" +
                                        "    ON HML.LEDGER_ID = LB.LEDGER_ID\n" +
                                        " INNER JOIN MASTER_HEADOFFICE_LEDGER MHL\n" +
                                        "    ON MHL.HEADOFFICE_LEDGER_ID = HML.HEADOFFICE_LEDGER_ID\n" +
                                        " WHERE LB.TRANS_FLAG = 'OP'\n" +
                                        "   AND LB.PROJECT_ID IN (?PROJECT_ID)";

                        break;
                    }
                case EnumDataSyncSQLCommand.ExportVouchers.FetchCountry:
                    {
                        sVouchersQuery = "SELECT MC.COUNTRY,\n" +
                                        "       COUNTRY_CODE,\n" +
                                        "       CURRENCY_CODE,\n" +
                                        "       CURRENCY_SYMBOL,\n" +
                                        "       CURRENCY_NAME\n" +
                                        "\n" +
                                        "  FROM MASTER_COUNTRY MC\n" +
                                        "\n" +
                                        " INNER JOIN VOUCHER_MASTER_TRANS MT\n" +
                                        "    ON MC.COUNTRY_ID = MT.CURRENCY_COUNTRY_ID\n" +
                                        "    OR MC.COUNTRY_ID = MT.EXCHANGE_COUNTRY_ID\n" +
                                        " INNER JOIN MASTER_DONAUD MD\n" +
                                        "    ON MD.COUNTRY_ID = MC.COUNTRY_ID\n" +
                                        " WHERE DONAUD_ID <> 0 AND \n" +
                                        " MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                        " GROUP BY MC.COUNTRY_ID;";


                        break;
                    }
                case EnumDataSyncSQLCommand.ExportVouchers.FetchHeadOfficeLedger:
                    {
                        //sVouchersQuery = "SELECT LEDGER_CODE AS LEDGER_CODE, LEDGER_NAME AS LEDGER_NAME\n" +
                        //                "  FROM VOUCHER_MASTER_TRANS MT\n" +
                        //                " INNER JOIN VOUCHER_TRANS VT\n" +
                        //                "    ON VT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                        //                " INNER JOIN HEADOFFICE_MAPPED_LEDGER HML\n" +
                        //                "    ON HML.LEDGER_ID = VT.LEDGER_ID\n" +
                        //                " INNER JOIN MASTER_HEADOFFICE_LEDGER MHL\n" +
                        //                "    ON MHL.HEADOFFICE_LEDGER_ID = HML.HEADOFFICE_LEDGER_ID\n" +
                        //                " GROUP BY MHL.HEADOFFICE_LEDGER_ID;";
                        sVouchersQuery = "SELECT T.LEDGER_CODE, T.LEDGER_NAME,T.GROUP_ID,T.LEDGER_TYPE,T.LEDGER_SUB_TYPE,T.SORT_ID\n" +
                                            "  FROM (SELECT MHL.LEDGER_CODE,\n" +
                                            "               MHL.LEDGER_NAME,\n" +
                                            "               ML.GROUP_ID,\n" +
                                            "               ML.LEDGER_TYPE,\n" +
                                            "               ML.LEDGER_SUB_TYPE,\n" +
                                            "               ML.SORT_ID\n" +
                                            "          FROM VOUCHER_MASTER_TRANS MT\n" +
                                            "         INNER JOIN VOUCHER_TRANS VT\n" +
                                            "            ON VT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                                            "         INNER JOIN HEADOFFICE_MAPPED_LEDGER HML\n" +
                                            "            ON HML.LEDGER_ID = VT.LEDGER_ID\n" +
                                            "         INNER JOIN MASTER_HEADOFFICE_LEDGER MHL\n" +
                                            "            ON MHL.HEADOFFICE_LEDGER_ID = HML.HEADOFFICE_LEDGER_ID\n" +
                                            "         INNER JOIN MASTER_LEDGER ML\n" +
                                            "            ON ML.LEDGER_ID = HML.LEDGER_ID\n" +
                                            "         GROUP BY MHL.HEADOFFICE_LEDGER_ID\n" +
                                            "\n" +
                                            "        UNION\n" +
                                            "\n" +
                                            "        SELECT ML.LEDGER_CODE,\n" +
                                            "               ML.LEDGER_NAME,\n" +
                                            "               ML.GROUP_ID,\n" +
                                            "               ML.LEDGER_TYPE,\n" +
                                            "               ML.LEDGER_SUB_TYPE,\n" +
                                            "               ML.SORT_ID GROUP_ID\n" +
                                            "          FROM VOUCHER_TRANS MT\n" +
                                            "         INNER JOIN VOUCHER_TRANS VT\n" +
                                            "            ON VT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                                            "         INNER JOIN MASTER_LEDGER ML\n" +
                                            "            ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                                            "         GROUP BY ML.LEDGER_ID) AS T\n" +
                                            " WHERE T.GROUP_ID NOT IN (12)\n" +
                                            " GROUP BY T.LEDGER_NAME";
                        break;
                    }
            }
            return sVouchersQuery;
        }
        #endregion
    }
}
