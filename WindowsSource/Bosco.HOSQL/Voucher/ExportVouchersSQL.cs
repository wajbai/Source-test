using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.HOSQL
{
    public class ExportVouchersSQL : IDatabaseQuery
    {
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;
        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.ExportVouchers).FullName)
            {
                query = GetExportVoucherQuery();
            }

            sqlType = this.sqlType;
            return query;
        }

        #region Export Vouchers
        private string GetExportVoucherQuery()
        {
            string query = string.Empty;
            SQLCommand.ExportVouchers sqlCommandId = (SQLCommand.ExportVouchers)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {
                case SQLCommand.ExportVouchers.FetchMasterVouchers:
                    {
                        query = "SELECT MT.VOUCHER_ID,\n" +
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
                                            "   AND MT.PROJECT_ID in (?PROJECT_ID) AND MT.VOUCHER_SUB_TYPE <> 'FD'\n" +
                                            "   AND MT.STATUS = 1\n" +
                                            " GROUP BY MT.VOUCHER_ID\n" +
                                            " ORDER BY MT.PROJECT_ID, MT.VOUCHER_NO";

                        break;
                    }
                case SQLCommand.ExportVouchers.FetchProjects:
                    {
                        query = "SELECT PROJECT_ID,PROJECT FROM MASTER_PROJECT WHERE DELETE_FLAG=0";
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchDonors:
                    {
                        query = "SELECT NAME, \n" +
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
                                        "   AND MT.STATUS = 1 AND MT.VOUCHER_SUB_TYPE <> 'FD'";

                        break;
                    }
                case SQLCommand.ExportVouchers.FetchVoucherTransactions:
                    {
                        //query = "SELECT MT.VOUCHER_ID,\n" +
                        //            "       VT.SEQUENCE_NO,\n" +
                        //            "       ML.LEDGER_NAME,\n" +
                        //            "       VT.AMOUNT,\n" +
                        //            "       VT.TRANS_MODE,\n" +
                        //            "       VT.CHEQUE_NO,\n" +
                        //            "       VT.MATERIALIZED_ON\n" +
                        //            "  FROM VOUCHER_MASTER_TRANS MT\n" +
                        //            " INNER JOIN VOUCHER_TRANS VT\n" +
                        //            "    ON VT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                        //            " INNER JOIN MASTER_LEDGER ML\n" +
                        //            "    ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                        //            " WHERE MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO \n" +
                        //            "   AND MT.PROJECT_ID in (?PROJECT_ID)\n" +
                        //            "   AND MT.STATUS = 1 AND MT.VOUCHER_SUB_TYPE <> 'FD' ";
                        query = "SELECT T.VOUCHER_ID,\n" +
                                "       T.SEQUENCE_NO,\n" +
                                "       T.LEDGER_NAME,\n" +
                                "       T.AMOUNT,\n" +
                                "       T.TRANS_MODE,\n" +
                                "       T.CHEQUE_NO,\n" +
                                "       T.MATERIALIZED_ON\n" +
                                "  FROM (SELECT MT.VOUCHER_ID,\n" +
                                "               VT.SEQUENCE_NO,\n" +
                                "               ML.LEDGER_NAME,\n" +
                                "               VT.AMOUNT,\n" +
                                "               VT.TRANS_MODE,\n" +
                                "               VT.CHEQUE_NO,\n" +
                                "               VT.MATERIALIZED_ON\n" +
                                "          FROM VOUCHER_MASTER_TRANS MT\n" +
                                "         INNER JOIN VOUCHER_TRANS VT\n" +
                                "            ON VT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                                "          LEFT JOIN HEADOFFICE_MAPPED_LEDGER HML\n" +
                                "            ON HML.LEDGER_ID = VT.LEDGER_ID\n" +
                                "          LEFT JOIN MASTER_HEADOFFICE_LEDGER ML\n" +
                                "            ON ML.HEADOFFICE_LEDGER_ID = HML.HEADOFFICE_LEDGER_ID\n" +
                                "\n" +
                                "         WHERE MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                "           AND MT.PROJECT_ID in (?PROJECT_ID)\n" +
                                "           AND ML.GROUP_ID NOT IN (12, 13, 14)\n" +
                                "           AND MT.STATUS = 1\n" +
                                "         GROUP BY VT.VOUCHER_ID, VT.SEQUENCE_NO\n" +
                                "        UNION\n" +
                                "\n" +
                                "        SELECT MT.VOUCHER_ID,\n" +
                                "               VT.SEQUENCE_NO,\n" +
                                "               ML.LEDGER_NAME,\n" +
                                "               VT.AMOUNT,\n" +
                                "               VT.TRANS_MODE,\n" +
                                "               VT.CHEQUE_NO,\n" +
                                "               VT.MATERIALIZED_ON\n" +
                                "          FROM VOUCHER_MASTER_TRANS MT\n" +
                                "         INNER JOIN VOUCHER_TRANS VT\n" +
                                "            ON VT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                                "         INNER JOIN MASTER_LEDGER ML\n" +
                                "            ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                                "         WHERE MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                "           AND MT.PROJECT_ID in (?PROJECT_ID)\n" +
                                "           AND MT.STATUS = 1\n" +
                                "           AND MT.VOUCHER_SUB_TYPE <> 'FD'\n" +
                                "           AND ML.GROUP_ID IN (12, 13, 14)) AS T\n" +
                                " ORDER BY T.VOUCHER_ID;";


                        break;
                    }
                case SQLCommand.ExportVouchers.FetchVoucherCostCentres:
                    {
                        query = "SELECT MT.VOUCHER_ID,VCC.SEQUENCE_NO,\n" +
                                        "       MCC.COST_CENTRE_NAME,\n" +
                                        "       ML.LEDGER_NAME,\n" +
                                        "       VCC.AMOUNT\n" +
                                        "  FROM VOUCHER_MASTER_TRANS MT\n" +
                                        "\n" +
                                        " INNER JOIN VOUCHER_CC_TRANS VCC\n" +
                                        "    ON VCC.VOUCHER_ID = MT.VOUCHER_ID\n" +
                                        " LEFT JOIN MASTER_LEDGER ML\n" +
                                        "    ON ML.LEDGER_ID = VCC.LEDGER_ID\n" +
                                        " LEFT JOIN MASTER_COST_CENTRE MCC\n" +
                                        "    ON MCC.COST_CENTRE_ID = VCC.COST_CENTRE_ID\n" +
                                        "\n" +
                                        " WHERE MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                        "   AND MT.PROJECT_ID in (?PROJECT_ID)\n" +
                                        "   AND MT.STATUS = 1";

                        break;
                    }
                case SQLCommand.ExportVouchers.FetchBankDetails:
                    {
                        query = "SELECT MB.BANK_CODE,\n" +
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
                                "       MA.LEDGER_ID\n" +
                                "  FROM MASTER_BANK_ACCOUNT MA\n" +
                                "  LEFT JOIN MASTER_BANK MB\n" +
                                "    ON MA.BANK_ID = MB.BANK_ID\n" +
                                "  LEFT JOIN VOUCHER_TRANS VT\n" +
                                "    ON VT.LEDGER_ID = MA.LEDGER_ID\n" +
                                "  LEFT JOIN VOUCHER_MASTER_TRANS MT\n" +
                                "    ON MT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                " WHERE MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                "   AND MT.PROJECT_ID IN (?PROJECT_ID)\n" +
                                "   AND MT.STATUS = 1 AND MT.VOUCHER_SUB_TYPE <> 'FD' \n" +
                                " GROUP BY MA.ACCOUNT_NUMBER\n" +
                                " ORDER BY MB.BANK";
                        break;

                    }
                case SQLCommand.ExportVouchers.FetchLedgerBalance:
                    {
                        //query = "SELECT MP.PROJECT,\n" +
                        //                "       LB.BALANCE_DATE,\n" +
                        //                "       MHL.LEDGER_NAME,\n" +
                        //                "       AMOUNT,\n" +
                        //                "       TRANS_MODE,\n" +
                        //                "       TRANS_FLAG\n" +
                        //                "  FROM LEDGER_BALANCE LB\n" +
                        //                "  LEFT JOIN MASTER_PROJECT MP\n" +
                        //                "    ON MP.PROJECT_ID = LB.PROJECT_ID\n" +
                        //                " INNER JOIN HEADOFFICE_MAPPED_LEDGER HML\n" +
                        //                "    ON HML.LEDGER_ID = LB.LEDGER_ID\n" +
                        //                " INNER JOIN MASTER_HEADOFFICE_LEDGER MHL\n" +
                        //                "    ON MHL.HEADOFFICE_LEDGER_ID = HML.HEADOFFICE_LEDGER_ID\n" +
                        //                " WHERE LB.TRANS_FLAG = 'OP'\n" +
                        //                "   AND LB.PROJECT_ID IN (?PROJECT_ID)";

                        query = "SELECT T.PROJECT,\n" +
                                    "       T.BALANCE_DATE,\n" +
                                    "       T.LEDGER_NAME,\n" +
                                    "       T.AMOUNT,\n" +
                                    "       T.TRANS_MODE,\n" +
                                    "       T.TRANS_FLAG\n" +
                                    "  FROM (SELECT MP.PROJECT,\n" +
                                    "               LB.BALANCE_DATE,\n" +
                                    "               LB.PROJECT_ID, \n" +
                                    "               MHL.LEDGER_NAME,\n" +
                                    "               AMOUNT,\n" +
                                    "               TRANS_MODE,\n" +
                                    "               TRANS_FLAG\n" +
                                    "          FROM LEDGER_BALANCE LB\n" +
                                    "          LEFT JOIN MASTER_PROJECT MP\n" +
                                    "            ON MP.PROJECT_ID = LB.PROJECT_ID\n" +
                                    "         INNER JOIN HEADOFFICE_MAPPED_LEDGER HML\n" +
                                    "            ON HML.LEDGER_ID = LB.LEDGER_ID\n" +
                                    "         INNER JOIN MASTER_HEADOFFICE_LEDGER MHL\n" +
                                    "            ON MHL.HEADOFFICE_LEDGER_ID = HML.HEADOFFICE_LEDGER_ID\n" +
                                    "        UNION\n" +
                                    "        SELECT MP.PROJECT,\n" +
                                    "               LB.BALANCE_DATE,\n" +
                                    "               LB.PROJECT_ID, \n" +
                                    "               ML.LEDGER_NAME,\n" +
                                    "               AMOUNT,\n" +
                                    "               TRANS_MODE,\n" +
                                    "               TRANS_FLAG\n" +
                                    "          FROM LEDGER_BALANCE LB\n" +
                                    "          LEFT JOIN MASTER_PROJECT MP\n" +
                                    "            ON MP.PROJECT_ID = LB.PROJECT_ID\n" +
                                    "          LEFT JOIN MASTER_LEDGER ML\n" +
                                    "            ON LB.LEDGER_ID = ML.LEDGER_ID\n" +
                                    "         WHERE ML.GROUP_ID IN (12, 13, 14)) AS T\n" +
                                    " WHERE T.TRANS_FLAG = 'OP'\n" +
                                    "AND T.PROJECT_ID IN(?PROJECT_ID)";

                        break;
                    }
                case SQLCommand.ExportVouchers.CheckHeadofficeLedgerExists:
                    {
                        query = "SELECT VT.LEDGER_ID, ML.LEDGER_NAME\n" +
                                "  FROM VOUCHER_TRANS VT\n" +
                                " INNER JOIN VOUCHER_MASTER_TRANS MT\n" +
                                "    ON MT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                " INNER JOIN MASTER_LEDGER ML\n" +
                                "    ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                                " WHERE MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                "   AND PROJECT_ID IN(?PROJECT_ID)\n" +
                                "   AND ML.GROUP_ID NOT IN (12, 13, 14) AND MT.STATUS=1\n" +
                                "   AND VT.LEDGER_ID NOT IN (SELECT LEDGER_ID FROM HEADOFFICE_MAPPED_LEDGER)\n" +
                                " GROUP BY VT.LEDGER_ID;";
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchCountry:
                    {
                        query = "SELECT MC.COUNTRY,\n" +
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
                case SQLCommand.ExportVouchers.FetchHeadOfficeLedger:
                    {
                        //query = "SELECT LEDGER_CODE AS LEDGER_CODE, LEDGER_NAME AS LEDGER_NAME\n" +
                        //                "  FROM VOUCHER_MASTER_TRANS MT\n" +
                        //                " INNER JOIN VOUCHER_TRANS VT\n" +
                        //                "    ON VT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                        //                " INNER JOIN HEADOFFICE_MAPPED_LEDGER HML\n" +
                        //                "    ON HML.LEDGER_ID = VT.LEDGER_ID\n" +
                        //                " INNER JOIN MASTER_HEADOFFICE_LEDGER MHL\n" +
                        //                "    ON MHL.HEADOFFICE_LEDGER_ID = HML.HEADOFFICE_LEDGER_ID\n" +
                        //                " GROUP BY MHL.HEADOFFICE_LEDGER_ID;";
                        query = "SELECT T.LEDGER_CODE, T.LEDGER_NAME,T.GROUP_ID,T.LEDGER_TYPE,T.LEDGER_SUB_TYPE,T.SORT_ID\n" +
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
                                            "          FROM VOUCHER_MASTER_TRANS MT\n" +
                                            "         INNER JOIN VOUCHER_TRANS VT\n" +
                                            "            ON VT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                                            "         INNER JOIN MASTER_LEDGER ML\n" +
                                            "            ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                                            "         WHERE ML.GROUP_ID IN (12, 13, 14)\n" +
                                            "         GROUP BY ML.LEDGER_ID) AS T\n" +
                                            " GROUP BY T.LEDGER_NAME";
                        break;
                    }
                case SQLCommand.ExportVouchers.FDAccounts:
                    {
                        query = "SELECT T.FD_ACCOUNT_ID,\n" +
                                "       T.FD_ACCOUNT_NUMBER,\n" +
                                "       MP.PROJECT,\n" +
                                "       ML.LEDGER_NAME,\n" +
                                "       MLA.LEDGER_NAME AS BANK_LEDGER,\n" +
                                "       MB.BANK,\n" +
                                "       T.FD_VOUCHER_ID,\n" +
                                "       T.AMOUNT,\n" +
                                "       T.TRANS_MODE,\n" +
                                "       T.TRANS_TYPE,\n" +
                                "       T.RECEIPT_NO,\n" +
                                "       T.ACCOUNT_HOLDER,\n" +
                                "       T.INVESTMENT_DATE,\n" +
                                "       T.MATURED_ON,\n" +
                                "       T.INTEREST_RATE,\n" +
                                "       T.INTEREST_AMOUNT,\n" +
                                "       T.INTEREST_TYPE,\n" +
                                "       T.STATUS,\n" +
                                "       T.FD_STATUS,\n" +
                                "       T.FD_SUB_TYPES,\n" +
                                "       T.NOTES\n" +
                                "  FROM FD_ACCOUNT FDA\n" +
                                "  LEFT JOIN MASTER_PROJECT MP\n" +
                                "    ON MP.PROJECT_ID = FDA.PROJECT_ID\n" +
                                "  LEFT JOIN MASTER_LEDGER MLA\n" +
                                "    ON FDA.BANK_LEDGER_ID = MLA.LEDGER_ID\n" +
                                "  LEFT JOIN MASTER_LEDGER ML\n" +
                                "    ON ML.LEDGER_ID = FDA.LEDGER_ID\n" +
                                "  LEFT JOIN MASTER_BANK MB\n" +
                                "    ON MB.BANK_ID = FDA.BANK_ID\n" +
                                "\n" +
                                "  JOIN((SELECT FD_ACCOUNT_ID,\n" +
                                "               FD_ACCOUNT_NUMBER,\n" +
                                "               PROJECT_ID,\n" +
                                "               LEDGER_ID,\n" +
                                "               BANK_LEDGER_ID,\n" +
                                "               BANK_ID,\n" +
                                "               FD_VOUCHER_ID,\n" +
                                "               AMOUNT,\n" +
                                "               TRANS_MODE,\n" +
                                "               TRANS_TYPE,\n" +
                                "               RECEIPT_NO,\n" +
                                "               ACCOUNT_HOLDER,\n" +
                                "               INVESTMENT_DATE,\n" +
                                "               MATURED_ON,\n" +
                                "               INTEREST_RATE,\n" +
                                "               INTEREST_AMOUNT,\n" +
                                "               INTEREST_TYPE,\n" +
                                "               STATUS,\n" +
                                "               FD_STATUS,\n" +
                                "               FD_SUB_TYPES,\n" +
                                "               NOTES\n" +
                                "          FROM FD_ACCOUNT FDA\n" +
                                "         WHERE FD_VOUCHER_ID IN\n" +
                                "               (SELECT VOUCHER_ID\n" +
                                "                  FROM VOUCHER_MASTER_TRANS VMT\n" +
                                "                 WHERE VOUCHER_SUB_TYPE = 'FD'\n" +
                                "                   AND VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO))\n" +
                                "UNION (SELECT FD_ACCOUNT_ID,\n" +
                                "              FD_ACCOUNT_NUMBER,\n" +
                                "              PROJECT_ID,\n" +
                                "              LEDGER_ID,\n" +
                                "              BANK_LEDGER_ID,\n" +
                                "              BANK_ID,\n" +
                                "              FD_VOUCHER_ID,\n" +
                                "              AMOUNT,\n" +
                                "              TRANS_MODE,\n" +
                                "              TRANS_TYPE,\n" +
                                "              RECEIPT_NO,\n" +
                                "              ACCOUNT_HOLDER,\n" +
                                "              INVESTMENT_DATE,\n" +
                                "              MATURED_ON,\n" +
                                "              INTEREST_RATE,\n" +
                                "              INTEREST_AMOUNT,\n" +
                                "              INTEREST_TYPE,\n" +
                                "              STATUS,\n" +
                                "              FD_STATUS,\n" +
                                "              FD_SUB_TYPES,\n" +
                                "              NOTES\n" +
                                "         FROM FD_ACCOUNT FDA\n" +
                                "        WHERE FD_ACCOUNT_ID IN\n" +
                                "              (SELECT FDR.FD_ACCOUNT_ID\n" +
                                "                 FROM FD_RENEWAL FDR\n" +
                                "                WHERE FDR.FD_VOUCHER_ID IN\n" +
                                "                      (SELECT VOUCHER_ID\n" +
                                "                         FROM VOUCHER_MASTER_TRANS VMT\n" +
                                "                        WHERE VOUCHER_SUB_TYPE = 'FD'\n" +
                                "                          AND VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO)))) AS T\n" +
                                "    ON FDA.FD_ACCOUNT_ID = T.FD_ACCOUNT_ID;";
                        break;
                    }
                case SQLCommand.ExportVouchers.FDRenewals:
                    {
                        query = "SELECT FDR.FD_ACCOUNT_ID,\n" +
                                "       FD_RENEWAL_ID,\n" +
                                "       RENEWAL_DATE,\n" +
                                "       MATURITY_DATE,\n" +
                                "       ML.LEDGER_NAME AS INTEREST_LEDGER,\n" +
                                "       IFNULL(MLA.LEDGER_NAME, '') AS BANK_LEDGER,\n" +
                                "       FD_INTEREST_VOUCHER_ID,\n" +
                                "       FDR.FD_VOUCHER_ID,\n" +
                                "       FDR.INTEREST_AMOUNT,\n" +
                                "       WITHDRAWAL_AMOUNT,\n" +
                                "       FDR.INTEREST_RATE,\n" +
                                "       FDR.INTEREST_TYPE,\n" +
                                "       FDR.RECEIPT_NO,\n" +
                                "       RENEWAL_TYPE,\n" +
                                "       FDR.STATUS\n" +
                                "  FROM FD_RENEWAL FDR\n" +
                                "  LEFT JOIN MASTER_LEDGER ML\n" +
                                "    ON FDR.INTEREST_LEDGER_ID = ML.LEDGER_ID\n" +
                                "  LEFT JOIN MASTER_LEDGER MLA\n" +
                                "    ON MLA.LEDGER_ID = FDR.BANK_LEDGER_ID\n" +
                                "\n" +
                                " WHERE FD_ACCOUNT_ID IN\n" +
                                "       (SELECT FDR.FD_ACCOUNT_ID\n" +
                                "          FROM FD_RENEWAL FDR\n" +
                                "         WHERE FDR.FD_VOUCHER_ID IN\n" +
                                "               (SELECT VOUCHER_ID\n" +
                                "                  FROM VOUCHER_MASTER_TRANS VMT\n" +
                                "                 WHERE VOUCHER_SUB_TYPE = 'FD'\n" +
                                "                   AND VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO));";
                        break;
                    }
                case SQLCommand.ExportVouchers.FDVoucherMasterTrans:
                    {
                        query = "SELECT VOUCHER_ID,\n" +
                                "VMT.VOUCHER_DATE,\n" +
                                "       MP.PROJECT,\n" +
                                "       VOUCHER_NO,\n" +
                                "       VOUCHER_TYPE,\n" +
                                "       VOUCHER_SUB_TYPE,\n" +
                                "       DONOR_ID,\n" +
                                "       PURPOSE_ID,\n" +
                                "       CONTRIBUTION_TYPE,\n" +
                                "       CONTRIBUTION_AMOUNT,\n" +
                                "       CURRENCY_COUNTRY_ID,\n" +
                                "       EXCHANGE_RATE,\n" +
                                "       EXCHANGE_COUNTRY_ID,\n" +
                                "       NARRATION,\n" +
                                "       VMT.STATUS,\n" +
                                "       CREATED_BY,\n" +
                                "       MODIFIED_BY,\n" +
                                "       CALCULATED_AMOUNT,\n" +
                                "       ACTUAL_AMOUNT,\n" +
                                "       NAME_ADDRESS\n" +
                                "  FROM VOUCHER_MASTER_TRANS VMT\n" +
                                "  LEFT JOIN MASTER_PROJECT MP\n" +
                                "    ON VMT.PROJECT_ID = MP.PROJECT_ID\n" +
                                "  JOIN((SELECT FD_VOUCHER_ID, '' AS FD_INTEREST_VOUCHER_ID\n" +
                                "          FROM FD_ACCOUNT FDA\n" +
                                "         WHERE FD_VOUCHER_ID IN\n" +
                                "               (SELECT VOUCHER_ID\n" +
                                "                  FROM VOUCHER_MASTER_TRANS VMT\n" +
                                "                 WHERE VOUCHER_SUB_TYPE = 'FD'\n" +
                                "                   AND VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO))\n" +
                                "UNION (SELECT FD_VOUCHER_ID, '' AS FD_INTEREST_VOUCHER_ID\n" +
                                "         FROM FD_ACCOUNT FDA\n" +
                                "        WHERE FD_ACCOUNT_ID IN\n" +
                                "              (SELECT FDR.FD_ACCOUNT_ID\n" +
                                "                 FROM FD_RENEWAL FDR\n" +
                                "                WHERE FDR.FD_VOUCHER_ID IN\n" +
                                "                      (SELECT VOUCHER_ID\n" +
                                "                         FROM VOUCHER_MASTER_TRANS VMT\n" +
                                "                        WHERE VOUCHER_SUB_TYPE = 'FD'\n" +
                                "                          AND VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO)))\n" +
                                "UNION (SELECT FDR.FD_VOUCHER_ID, FD_INTEREST_VOUCHER_ID\n" +
                                "         FROM FD_RENEWAL FDR\n" +
                                "\n" +
                                "        WHERE FD_ACCOUNT_ID IN\n" +
                                "              (SELECT FDR.FD_ACCOUNT_ID\n" +
                                "                 FROM FD_RENEWAL FDR\n" +
                                "                WHERE FDR.FD_VOUCHER_ID IN\n" +
                                "                      (SELECT VOUCHER_ID\n" +
                                "                         FROM VOUCHER_MASTER_TRANS VMT\n" +
                                "                        WHERE VOUCHER_SUB_TYPE = 'FD'\n" +
                                "                          AND VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO )))) AS T\n" +
                                "    ON VMT.VOUCHER_ID = T.FD_VOUCHER_ID\n" +
                                "    OR VMT.VOUCHER_ID = T.FD_INTEREST_VOUCHER_ID;";
                        break;
                    }
                case SQLCommand.ExportVouchers.FDVoucherTrans:
                    {
                        query = "SELECT VT.VOUCHER_ID,\n" +
                                "       SEQUENCE_NO,\n" +
                                "       ML.LEDGER_NAME,\n" +
                                "       AMOUNT,\n" +
                                "       TRANS_MODE,\n" +
                                "       LEDGER_FLAG,\n" +
                                "       CHEQUE_NO,\n" +
                                "       MATERIALIZED_ON,\n" +
                                "       VT.STATUS\n" +
                                "  FROM VOUCHER_TRANS VT\n" +
                                "  LEFT JOIN MASTER_LEDGER ML\n" +
                                "    ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                                "  JOIN((SELECT FD_VOUCHER_ID, '' AS FD_INTEREST_VOUCHER_ID\n" +
                                "          FROM FD_ACCOUNT FDA\n" +
                                "         WHERE FD_VOUCHER_ID IN\n" +
                                "               (SELECT VOUCHER_ID\n" +
                                "                  FROM VOUCHER_MASTER_TRANS VMT\n" +
                                "                 WHERE VOUCHER_SUB_TYPE = 'FD'\n" +
                                "                   AND VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO))\n" +
                                "UNION (SELECT FD_VOUCHER_ID, '' AS FD_INTEREST_VOUCHER_ID\n" +
                                "         FROM FD_ACCOUNT FDA\n" +
                                "        WHERE FD_ACCOUNT_ID IN\n" +
                                "              (SELECT FDR.FD_ACCOUNT_ID\n" +
                                "                 FROM FD_RENEWAL FDR\n" +
                                "                WHERE FDR.FD_VOUCHER_ID IN\n" +
                                "                      (SELECT VOUCHER_ID\n" +
                                "                         FROM VOUCHER_MASTER_TRANS VMT\n" +
                                "                        WHERE VOUCHER_SUB_TYPE = 'FD'\n" +
                                "                          AND VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO)))\n" +
                                "UNION (SELECT FDR.FD_VOUCHER_ID, FD_INTEREST_VOUCHER_ID\n" +
                                "         FROM FD_RENEWAL FDR\n" +
                                "\n" +
                                "        WHERE FD_ACCOUNT_ID IN\n" +
                                "              (SELECT FDR.FD_ACCOUNT_ID\n" +
                                "                 FROM FD_RENEWAL FDR\n" +
                                "                WHERE FDR.FD_VOUCHER_ID IN\n" +
                                "                      (SELECT VOUCHER_ID\n" +
                                "                         FROM VOUCHER_MASTER_TRANS VMT\n" +
                                "                        WHERE VOUCHER_SUB_TYPE = 'FD'\n" +
                                "                          AND VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO)))\n" +
                                "\n" +
                                ") AS T\n" +
                                "    ON VT.VOUCHER_ID = T.FD_VOUCHER_ID\n" +
                                "    OR VT.VOUCHER_ID = T.FD_INTEREST_VOUCHER_ID;";
                        break;
                    }
                case SQLCommand.ExportVouchers.FDBankAccountDetails:
                    {
                        query = "SELECT  \n" +
                                " MB.BANK_ID,\n" +
                                "       MB.BANK_CODE,\n" +
                                "       MB.BANK,\n" +
                                "       MB.BRANCH,\n" +
                                "       MB.ADDRESS,\n" +
                                "       MB.IFSCCODE,\n" +
                                "       MB.MICRCODE,\n" +
                                "       MB.CONTACTNUMBER,\n" +
                                "       MB.ACCOUNTNAME,\n" +
                                "       MB.SWIFTCODE,\n" +
                                "       MB.NOTES,\n" +
                                "      MBA.ACCOUNT_CODE,\n" +
                                "       MBA.ACCOUNT_NUMBER,\n" +
                                "       MBA.ACCOUNT_HOLDER_NAME,\n" +
                                "       MBA.IS_FCRA_ACCOUNT,\n" +
                                "       MBA.LEDGER_ID\n" +
                                "  FROM FD_ACCOUNT FDA\n" +
                                "  LEFT JOIN MASTER_BANK_ACCOUNT MBA\n" +
                                "    ON FDA.BANK_LEDGER_ID = MBA.LEDGER_ID\n" +
                                " LEFT JOIN MASTER_BANK MB \n" +
                                " ON MB.BANK_ID=MBA.BANK_ID \n" +
                                "  JOIN((SELECT BANK_LEDGER_ID\n" +
                                "          FROM FD_ACCOUNT FDA\n" +
                                "         WHERE FD_VOUCHER_ID IN\n" +
                                "               (SELECT VOUCHER_ID\n" +
                                "                  FROM VOUCHER_MASTER_TRANS VMT\n" +
                                "                 WHERE VOUCHER_SUB_TYPE = 'FD'\n" +
                                "                   AND VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO))\n" +
                                "UNION (SELECT BANK_LEDGER_ID\n" +
                                "         FROM FD_ACCOUNT FDA\n" +
                                "        WHERE FD_ACCOUNT_ID IN\n" +
                                "              (SELECT FDR.FD_ACCOUNT_ID\n" +
                                "                 FROM FD_RENEWAL FDR\n" +
                                "                WHERE FDR.FD_VOUCHER_ID IN\n" +
                                "                      (SELECT VOUCHER_ID\n" +
                                "                         FROM VOUCHER_MASTER_TRANS VMT\n" +
                                "                        WHERE VOUCHER_SUB_TYPE = 'FD'\n" +
                                "                          AND VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO)))\n" +
                                "UNION (SELECT BANK_LEDGER_ID\n" +
                                "         FROM FD_RENEWAL FDR\n" +
                                "\n" +
                                "        WHERE FD_ACCOUNT_ID IN\n" +
                                "              (SELECT FDR.FD_ACCOUNT_ID\n" +
                                "                 FROM FD_RENEWAL FDR\n" +
                                "                WHERE FDR.FD_VOUCHER_ID IN\n" +
                                "                      (SELECT VOUCHER_ID\n" +
                                "                         FROM VOUCHER_MASTER_TRANS VMT\n" +
                                "                        WHERE VOUCHER_SUB_TYPE = 'FD'\n" +
                                "                          AND VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO)))\n" +
                                "\n" +
                                ") AS T\n" +
                                "    ON MBA.LEDGER_ID = T.BANK_LEDGER_ID\n" +
                                " GROUP BY T.BANK_LEDGER_ID";
                        break;
                    }
                case SQLCommand.ExportVouchers.FDBankDetails:
                    {
                        query = "SELECT MB.BANK_ID,\n" +
                                "       BANK_CODE,\n" +
                                "       BANK,\n" +
                                "       BRANCH,\n" +
                                "       ADDRESS,\n" +
                                "       IFSCCODE,\n" +
                                "       MICRCODE,\n" +
                                "       CONTACTNUMBER,\n" +
                                "       ACCOUNTNAME,\n" +
                                "       SWIFTCODE,\n" +
                                "       NOTES\n" +
                                "  FROM MASTER_BANK MB\n" +
                                "\n" +
                                "  JOIN((SELECT BANK_ID\n" +
                                "          FROM FD_ACCOUNT FDA\n" +
                                "         WHERE FD_VOUCHER_ID IN\n" +
                                "               (SELECT VOUCHER_ID\n" +
                                "                  FROM VOUCHER_MASTER_TRANS VMT\n" +
                                "                 WHERE VOUCHER_SUB_TYPE = 'FD'\n" +
                                "                   AND VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO))\n" +
                                "UNION (SELECT BANK_ID\n" +
                                "         FROM FD_ACCOUNT FDA\n" +
                                "        WHERE FD_ACCOUNT_ID IN\n" +
                                "              (SELECT FDR.FD_ACCOUNT_ID\n" +
                                "                 FROM FD_RENEWAL FDR\n" +
                                "                WHERE FDR.FD_VOUCHER_ID IN\n" +
                                "                      (SELECT VOUCHER_ID\n" +
                                "                         FROM VOUCHER_MASTER_TRANS VMT\n" +
                                "                        WHERE VOUCHER_SUB_TYPE = 'FD'\n" +
                                "                          AND VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO)))) AS T\n" +
                                "    ON MB.BANK_ID = T.BANK_ID;";
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchActiveTransactionperiod:
                    {
                        query = " SELECT ACC_YEAR_ID, " +
                               " YEAR_FROM, " +
                               " YEAR_TO, " +
                               " (SELECT BOOKS_BEGINNING_FROM FROM " +
                               " ACCOUNTING_YEAR ORDER BY ACC_YEAR_ID ASC LIMIT 1) AS  BOOKS_BEGINNING_FROM " +
                               " FROM ACCOUNTING_YEAR WHERE STATUS=1";
                        break;
                    }
            }
            return query;
        }
        #endregion
    }
}
