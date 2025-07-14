using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;
using Bosco.Utility;

namespace Bosco.SQL
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
            // 17/10/2024 -Chinna
            // have modified and Included the columns of IS_MULTI_CURRENCY, Amount_FC, Country_Id, Country ( Bank and HOLedgers)
            string query = string.Empty;
            SQLCommand.ExportVouchers sqlCommandId = (SQLCommand.ExportVouchers)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {
                case SQLCommand.ExportVouchers.FetchMasterVouchers:
                    {
                        query = "SELECT MT.VOUCHER_ID,\n" +
                                "       DATE(MT.VOUCHER_DATE) AS VOUCHER_DATE,\n" +
                                "       MT.VOUCHER_NO,\n" +
                                "       MP.PROJECT,\n" +
                                "       MT.VOUCHER_TYPE,\n" +
                                "       MT.VOUCHER_SUB_TYPE,\n" +
                                "       MCH.CODE,\n" +
                                "       MCH.FC_PURPOSE,\n" +
                                "       MD.NAME,\n" +
                                "       MT.DONOR_ID,\n" +
                                "       MT.PURPOSE_ID,\n" +
                                "       MT.NAME_ADDRESS,\n" +
                                "       MT.CONTRIBUTION_TYPE,\n" +
                                "       MT.CONTRIBUTION_AMOUNT,\n" +
                                "       ME.COUNTRY AS CURRENCY_COUNTRY,\n" +
                                "       MT.EXCHANGE_RATE,\n" +
                                "       MC.COUNTRY AS EXCHANGE_COUNTRY,\n" +
                                "       MT.NARRATION,\n" +
                                "       MT.CALCULATED_AMOUNT,\n" +
                                "       MT.ACTUAL_AMOUNT,\n" +
                                "       MT.CLIENT_REFERENCE_ID, MT.CLIENT_CODE,\n" +
                                "       MT.CREATED_ON,\n" +
                                "       MT.CREATED_BY,\n" +
                                "       MT.MODIFIED_ON,\n" +
                                "       MT.MODIFIED_BY,\n" +
                                "       UC.USER_NAME AS CREATED_BY_NAME,\n" +
                                "       UM.USER_NAME AS MODIFIED_BY_NAME,\n" +
                                "       IFNULL(MT.GST_VENDOR_ID,0) AS GST_VENDOR_ID, ASV.VENDOR, MT.GST_VENDOR_INVOICE_NO,\n" +
                                "       MT.GST_VENDOR_INVOICE_TYPE, MT.GST_VENDOR_INVOICE_DATE,MT.IS_MULTI_CURRENCY,MT.IS_CASH_BANK_STATUS\n" +
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
                                "  LEFT JOIN USER_INFO UC\n" +
                                "    ON UC.USER_ID=MT.CREATED_BY\n" +
                                "  LEFT JOIN USER_INFO UM\n" +
                                "    ON UM.USER_ID=MT.MODIFIED_BY\n" +
                                "  LEFT JOIN ASSET_STOCK_VENDOR ASV ON ASV.VENDOR_ID = MT.GST_VENDOR_ID\n" +
                                "  WHERE MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                "   AND MT.BRANCH_ID=?BRANCH_OFFICE_ID AND MT.PROJECT_ID in (?PROJECT_ID) AND MT.VOUCHER_SUB_TYPE <> 'FD'\n" +
                                "   AND MT.STATUS = 1\n" +
                                " GROUP BY MT.VOUCHER_ID\n" +
                                " ORDER BY MT.VOUCHER_ID,MT.VOUCHER_DATE ASC";
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchBranchLastVoucherId:
                    {
                        query = "SELECT IFNULL(MAX(VOUCHER_ID),0) AS LAST_VOUCHER_ID FROM VOUCHER_MASTER_TRANS;";
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchProjects:
                    {
                        query = "SELECT PROJECT_ID,PROJECT FROM MASTER_PROJECT WHERE DELETE_FLAG=0 ORDER BY PROJECT ASC";
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchHOBranchList:
                    {
                        query = "SELECT BRANCH_OFFICE_ID,BRANCH_OFFICE_NAME FROM branch_office";
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchHOBranchProjects:
                    {
                        query = "SELECT MP.PROJECT_ID, MP.PROJECT\n" +
                                "  FROM PROJECT_BRANCH PB\n" +
                                " INNER JOIN MASTER_PROJECT MP\n" +
                                "    ON PB.PROJECT_ID = MP.PROJECT_ID\n" +
                                " WHERE BRANCH_ID = ?BRANCH_OFFICE_ID\n" +
                                "   AND MP.DELETE_FLAG = 0\n" +
                                " ORDER BY PROJECT ASC;";
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchDonors:
                    {
                        query = "SELECT NAME, \n" +
                                         "PLACE,\n" +
                                         "COMPANY_NAME,\n" +
                                         "IFNULL(COUNTRY, '') AS COUNTRY,\n" +
                                         "PINCODE,\n" +
                                         "MD.TYPE,\n" +
                                         "PHONE,\n" +
                                         "FAX,\n" +
                                         "EMAIL,\n" +
                                         "URL, \n" +
                                         "'' AS STATE, IFNULL(MS.STATE_NAME,'') AS STATE_NAME,\n" +
                                         "ADDRESS,\n" +
                                         "PAN\n" +
                                         "  FROM MASTER_DONAUD MD\n" +
                                         " INNER JOIN VOUCHER_MASTER_TRANS MT\n" +
                                         "    ON MT.DONOR_ID = MD.DONAUD_ID\n" +
                                         "  LEFT JOIN MASTER_COUNTRY MC\n" +
                                         "    ON MC.COUNTRY_ID = MD.COUNTRY_ID\n" +
                                         "  LEFT JOIN MASTER_STATE MS\n" +
                                         "    ON MS.STATE_ID = MD.STATE_ID\n" +
                                         " WHERE  MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                         "   AND MD.BRANCH_ID=?BRANCH_OFFICE_ID AND \n" +
                                         " PROJECT_ID in (?PROJECT_ID)\n" +
                                         "   AND MT.STATUS = 1 AND MT.VOUCHER_SUB_TYPE <> 'FD'";

                        break;
                    }
                case SQLCommand.ExportVouchers.FetchProjectDonors:
                    {
                        query = @"SELECT NAME, PLACE, COMPANY_NAME, IFNULL(COUNTRY, '') AS COUNTRY, PINCODE, MD.TYPE, 
                                PHONE, FAX, EMAIL, URL, '' AS STATE, IFNULL(MS.STATE_NAME,'') AS STATE_NAME, ADDRESS,
                                PAN FROM MASTER_DONAUD MD
                                INNER JOIN PROJECT_DONOR PD ON PD.DONOR_ID = MD.DONAUD_ID
                                LEFT JOIN MASTER_COUNTRY MC ON MC.COUNTRY_ID = MD.COUNTRY_ID
                                LEFT JOIN MASTER_STATE MS ON MS.STATE_ID = MD.STATE_ID
                                WHERE PD.PROJECT_ID IN (?PROJECT_ID) AND MD.BRANCH_ID =?BRANCH_OFFICE_ID";
                        break;
                    }
                case SQLCommand.ExportVouchers.CheckPrimaryLedgerGroup:
                    {
                        query = "SELECT * FROM MASTER_LEDGER_GROUP WHERE PARENT_GROUP_ID=0";
                        break;
                    }
                case SQLCommand.ExportVouchers.CheckMappedFDLedgers: //On 06/07/2017, Get list of FD legers from BO OR mapped FD HO ledgers
                    {
                        query = "SELECT ML.LEDGER_ID, ML.LEDGER_NAME, HL.LEDGER_NAME AS HO_LEDGER_NAME, ML.GROUP_ID, HL.GROUP_ID as HO_GROUP_ID\n" +
                                " FROM MASTER_LEDGER ML\n" +
                                " INNER JOIN HEADOFFICE_MAPPED_LEDGER HM ON HM.LEDGER_ID = ML.LEDGER_ID\n" +
                                " INNER JOIN MASTER_HEADOFFICE_LEDGER HL ON HL.HEADOFFICE_LEDGER_ID = HM.HEADOFFICE_LEDGER_ID\n" +
                                " WHERE (ML.GROUP_ID = " + (int)Utility.FixedLedgerGroup.FixedDeposit + " OR HL.GROUP_ID = " + (int)Utility.FixedLedgerGroup.FixedDeposit + ")";
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchLegalEntity:
                    {
                        query = "SELECT MIP.CUSTOMERID,\n" +
                                "       INSTITUTENAME,\n" +
                                "       SOCIETYNAME,\n" +
                                "       CONTACTPERSON,\n" +
                                "       ADDRESS,\n" +
                                "       PLACE,\n" +
                                "       MC.COUNTRY,\n" +
                                "       PINCODE,\n" +
                                "       PHONE,\n" +
                                "       FAX,\n" +
                                "       EMAIL,\n" +
                                "       URL,\n" +
                                "       REGNO,\n" +
                                "       REGDATE,\n" +
                                "       PERMISSIONNO,\n" +
                                "       PERMISSIONDATE,\n" +
                                "       A12NO,\n" +
                                "       PANNO,\n" +
                                "       GIRNO,\n" +
                                "       TANNO,\n" +
                                "       ASSOCIATIONNATURE,\n" +
                                "       DENOMINATION,\n" +
                                "       OTHER_ASSOCIATION_NATURE,\n" +
                                "       OTHER_DENOMINATION,\n" +
                                "       FCRINO,\n" +
                                "       FCRIREGDATE,\n" +
                                "       EIGHTYGNO,\n" +
                                "       GST_NO, EIGHTY_GNO_REG_DATE, \n" +
                                "       LEDGER_ID,\n" +
                                "       MIP.STATE_ID,\n" +
                                "       MS.STATE_NAME AS STATE\n" +
                                "  FROM MASTER_INSTI_PERFERENCE MIP\n" +
                                "  LEFT JOIN MASTER_PROJECT MP\n" +
                                "    ON MIP.CUSTOMERID = MP.CUSTOMERID\n" +
                                "  LEFT JOIN MASTER_COUNTRY MC\n" +
                                "    ON MC.COUNTRY_ID = MIP.COUNTRY_ID\n" +
                                "  LEFT JOIN MASTER_STATE MS\n" +
                                "    ON MS.STATE_ID = MIP.STATE_ID\n" +
                                " WHERE PROJECT_ID IN (?PROJECT_ID)\n" +
                                " GROUP BY MIP.CUSTOMERID;";
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchTransProjects:
                    {
                        //query = "SELECT *\n" +
                        //         "  FROM ((SELECT MP.PROJECT_ID,\n" +
                        //         "                PROJECT_CODE,\n" +
                        //         "                PROJECT,\n" +
                        //         "                DIVISION_ID,\n" +
                        //         "                ACCOUNT_DATE,\n" +
                        //         "                DATE_STARTED,\n" +
                        //         "                DATE_CLOSED,\n" +
                        //         "                DESCRIPTION,\n" +
                        //         "                NOTES,\n" +
                        //         "                MPC.PROJECT_CATOGORY_ID,\n" +
                        //         "                MPC.PROJECT_CATOGORY_NAME,\n" +
                        //         "                DELETE_FLAG,\n" +
                        //         "                CUSTOMERID\n" +
                        //         "           from MASTER_PROJECT MP\n" +
                        //         "          INNER JOIN VOUCHER_MASTER_TRANS VMT\n" +
                        //         "             ON VMT.PROJECT_ID = MP.PROJECT_ID\n" +
                        //         "           LEFT JOIN MASTER_PROJECT_CATOGORY MPC\n" +
                        //         "             ON MPC.PROJECT_CATOGORY_ID = MP.PROJECT_CATEGORY_ID\n" +
                        //         "          WHERE VMT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                        //         "            AND VMT.PROJECT_ID IN (?PROJECT_ID)\n" +
                        //         "            AND VMT.STATUS = 1) UNION\n" +
                        //         "        (SELECT MP.PROJECT_ID,\n" +
                        //         "                PROJECT_CODE,\n" +
                        //         "                PROJECT,\n" +
                        //         "                DIVISION_ID,\n" +
                        //         "                ACCOUNT_DATE,\n" +
                        //         "                DATE_STARTED,\n" +
                        //         "                DATE_CLOSED,\n" +
                        //         "                DESCRIPTION,\n" +
                        //         "                NOTES,\n" +
                        //         "                MPC.PROJECT_CATOGORY_ID,\n" +
                        //         "                MPC.PROJECT_CATOGORY_NAME,\n" +
                        //         "                DELETE_FLAG,\n" +
                        //         "                CUSTOMERID\n" +
                        //         "           from MASTER_PROJECT MP\n" +
                        //         "          INNER JOIN VOUCHER_MASTER_TRANS VMT\n" +
                        //         "             ON VMT.PROJECT_ID = MP.PROJECT_ID\n" +
                        //         "           LEFT JOIN MASTER_PROJECT_CATOGORY MPC\n" +
                        //         "             ON MPC.PROJECT_CATOGORY_ID = MP.PROJECT_CATEGORY_ID\n" +
                        //         "          WHERE VMT.VOUCHER_SUB_TYPE = 'FD'\n" +
                        //         "            AND VMT.PROJECT_ID IN (?PROJECT_ID)\n" +
                        //         "            AND VMT.STATUS = 1)) AS T\n" +
                        //         " GROUP BY PROJECT_ID;";

                        query = "SELECT MP.PROJECT_ID,\n" +
                                "       PROJECT_CODE,\n" +
                                "       PROJECT,\n" +
                                "       DIVISION_ID,\n" +
                                "       ACCOUNT_DATE,\n" +
                                "       DATE_STARTED,\n" +
                                "       DATE_CLOSED, CLOSED_BY,\n" +
                                "       DESCRIPTION,\n" +
                                "       NOTES,\n" +
                                "       MPC.PROJECT_CATOGORY_ID,\n" +
                                "       MPC.PROJECT_CATOGORY_NAME,\n" +
                                "       DELETE_FLAG,\n" +
                                "       MP.CUSTOMERID,SOCIETYNAME \n" +
                                "  from MASTER_PROJECT MP\n" +
                                "  LEFT JOIN MASTER_PROJECT_CATOGORY MPC\n" +
                                "    ON MPC.PROJECT_CATOGORY_ID = MP.PROJECT_CATEGORY_ID\n" +
                                "LEFT JOIN MASTER_INSTI_PERFERENCE IP\n" +
                                "    ON MP.CUSTOMERID=IP.CUSTOMERID\n" +
                                " WHERE MP.PROJECT_ID IN (?PROJECT_ID);";

                        //query = "SELECT MP.PROJECT_ID,\n" +
                        //            "       PROJECT_CODE,\n" +
                        //            "       PROJECT,\n" +
                        //            "       DIVISION_ID,\n" +
                        //            "       ACCOUNT_DATE,\n" +
                        //            "       DATE_STARTED,\n" +
                        //            "       DATE_CLOSED,\n" +
                        //            "       DESCRIPTION,\n" +
                        //            "       NOTES,\n" +
                        //            "       MPC.PROJECT_CATOGORY_ID,\n" +
                        //            "       MPC.PROJECT_CATOGORY_NAME,\n" +
                        //            "       DELETE_FLAG,\n" +
                        //            "       CUSTOMERID\n" +
                        //            "  from MASTER_PROJECT MP\n" +
                        //            " INNER JOIN VOUCHER_MASTER_TRANS VMT\n" +
                        //            "    ON VMT.PROJECT_ID = MP.PROJECT_ID\n" +
                        //            " LEFT JOIN MASTER_PROJECT_CATOGORY MPC\n" +
                        //            "    ON MPC.PROJECT_CATOGORY_ID=MP.PROJECT_CATEGORY_ID\n" +
                        //            " WHERE VMT.VOUCHER_DATE BETWEEN ?DATE_FROM AND\n" +
                        //            " ?DATE_TO\n" +
                        //            "   AND VMT.PROJECT_ID IN (?PROJECT_ID)\n" +
                        //            "   AND VMT.STATUS = 1 \n" +
                        //            " GROUP BY VMT.PROJECT_ID;";
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchVoucherTransactions:
                    {
                        //Modified to fetch Head Office ledgers if mapped otherwise Branch Ledger
                        query = "SELECT MT.VOUCHER_ID,\n" +
                                "       VT.SEQUENCE_NO,\n" +
                                "       IF(MHL.LEDGER_NAME IS NOT NULL, MHL.LEDGER_NAME, ML.LEDGER_NAME) AS LEDGER_NAME,\n" +
                                "       MHL.LEDGER_NAME,\n" +
                                "       (VT.AMOUNT * IF(MT.IS_MULTI_CURRENCY=1, VT.EXCHANGE_RATE, 1)) AMOUNT,\n" +
                                "       VT.TRANS_MODE,\n" +
                                "       VT.CHEQUE_NO,\n" +
                                "       VT.MATERIALIZED_ON,\n" +
                                "       VT.CHEQUE_REF_DATE,\n" +
                                "       VT.CHEQUE_REF_BANKNAME,\n" +
                                "       VT.CHEQUE_REF_BRANCH,VT.NARRATION\n" +
                                "  FROM VOUCHER_MASTER_TRANS MT\n" +
                                "  LEFT JOIN VOUCHER_TRANS VT\n" +
                                "    ON MT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                "  LEFT JOIN MASTER_LEDGER ML\n" +
                                "    ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                                "  LEFT JOIN HEADOFFICE_MAPPED_LEDGER HML\n" +
                                "    ON VT.LEDGER_ID = HML.LEDGER_ID\n" +
                                "  LEFT JOIN MASTER_HEADOFFICE_LEDGER MHL\n" +
                                "    ON HML.HEADOFFICE_LEDGER_ID = MHL.HEADOFFICE_LEDGER_ID\n" +
                                " WHERE MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND \n" +
                                " ?DATE_TO\n" +
                                "   AND MT.PROJECT_ID in (?PROJECT_ID)\n" +
                                "   AND MT.STATUS = 1\n" +
                                "   AND MT.VOUCHER_SUB_TYPE <> 'FD'\n" +
                                " GROUP BY VT.VOUCHER_ID, VT.SEQUENCE_NO ORDER BY MT.VOUCHER_ID, MT.VOUCHER_DATE ASC;";
                        break;

                        //query = "SELECT T.VOUCHER_ID,\n" +
                        //        "       T.SEQUENCE_NO,\n" +
                        //        "       T.LEDGER_NAME,\n" +
                        //        "       T.AMOUNT,\n" +
                        //        "       T.TRANS_MODE,\n" +
                        //        "       T.CHEQUE_NO,\n" +
                        //        "       T.MATERIALIZED_ON\n" +
                        //        "  FROM (SELECT MT.VOUCHER_ID,\n" +
                        //        "               VT.SEQUENCE_NO,\n" +
                        //        "               ML.LEDGER_NAME,\n" +
                        //        "               VT.AMOUNT,\n" +
                        //        "               VT.TRANS_MODE,\n" +
                        //        "               VT.CHEQUE_NO,\n" +
                        //        "               VT.MATERIALIZED_ON\n" +
                        //        "          FROM VOUCHER_MASTER_TRANS MT\n" +
                        //        "         INNER JOIN VOUCHER_TRANS VT\n" +
                        //        "            ON VT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                        //        "          LEFT JOIN HEADOFFICE_MAPPED_LEDGER HML\n" +
                        //        "            ON HML.LEDGER_ID = VT.LEDGER_ID\n" +
                        //        "          LEFT JOIN MASTER_HEADOFFICE_LEDGER ML\n" +
                        //        "            ON ML.HEADOFFICE_LEDGER_ID = HML.HEADOFFICE_LEDGER_ID\n" +
                        //        "\n" +
                        //        "         WHERE MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                        //        "           AND MT.PROJECT_ID in (?PROJECT_ID)\n" +
                        //        "           AND ML.GROUP_ID NOT IN (12, 13, 14)\n" +
                        //        "           AND MT.STATUS = 1\n" +
                        //        "           AND MT.VOUCHER_SUB_TYPE <> 'FD'\n" +
                        //        "         GROUP BY VT.VOUCHER_ID, VT.SEQUENCE_NO\n" +
                        //        "        UNION\n" +
                        //        "\n" +
                        //        "        SELECT MT.VOUCHER_ID,\n" +
                        //        "               VT.SEQUENCE_NO,\n" +
                        //        "               ML.LEDGER_NAME,\n" +
                        //        "               VT.AMOUNT,\n" +
                        //        "               VT.TRANS_MODE,\n" +
                        //        "               VT.CHEQUE_NO,\n" +
                        //        "               VT.MATERIALIZED_ON\n" +
                        //        "          FROM VOUCHER_MASTER_TRANS MT\n" +
                        //        "         INNER JOIN VOUCHER_TRANS VT\n" +
                        //        "            ON VT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                        //        "         INNER JOIN MASTER_LEDGER ML\n" +
                        //        "            ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                        //        "         WHERE MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                        //        "           AND MT.PROJECT_ID in (?PROJECT_ID)\n" +
                        //        "           AND MT.STATUS = 1\n" +
                        //        "           AND MT.VOUCHER_SUB_TYPE <> 'FD'\n" +
                        //        "           AND ML.GROUP_ID IN (12, 13, 14)) AS T\n" +
                        //        " ORDER BY T.VOUCHER_ID;";
                    }
                case SQLCommand.ExportVouchers.FetchHOBranchVoucherTransactions:
                    {
                        query = "SELECT MT.VOUCHER_ID,\n" +
                                "       VT.SEQUENCE_NO,\n" +
                                "       ML.LEDGER_NAME,\n" +
                                "       VT.AMOUNT,\n" +
                                "       VT.TRANS_MODE,\n" +
                                "       VT.CHEQUE_NO,\n" +
                                "       VT.MATERIALIZED_ON,\n" +
                                "       VT.CHEQUE_REF_DATE,\n" +
                                "       VT.CHEQUE_REF_BANKNAME,\n" +
                                "       VT.CHEQUE_REF_BRANCH, VT.FUND_TRANSFER_TYPE_NAME, VT.NARRATION,\n" +
                                "       VT.LEDGER_GST_CLASS_ID, GST.SLAB, VT.GST, VT.CGST, VT.SGST, VT.IGST\n" +
                                "  FROM VOUCHER_MASTER_TRANS MT\n" +
                                "  LEFT JOIN VOUCHER_TRANS VT\n" +
                                "    ON MT.VOUCHER_ID = VT.VOUCHER_ID AND MT.BRANCH_ID=VT.BRANCH_ID\n" +
                                "  LEFT JOIN MASTER_LEDGER ML\n" +
                                "    ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                                "  LEFT JOIN (SELECT TCP.LEDGER_ID, TCP.GST_ID, TCP.GST_NO, MGC.SLAB\n" +
                                "    FROM TDS_CREDTIORS_PROFILE TCP\n" +
                                "     LEFT JOIN MASTER_GST_CLASS MGC ON MGC.GST_ID = TCP.GST_ID) AS GST ON GST.LEDGER_ID = ML.LEDGER_ID AND GST.GST_ID = VT.LEDGER_GST_CLASS_ID\n" +
                                " WHERE MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND \n" +
                                " ?DATE_TO\n" +
                                "   AND MT.BRANCH_ID=?BRANCH_OFFICE_ID AND MT.PROJECT_ID in (?PROJECT_ID)\n" +
                                "   AND MT.STATUS = 1\n" +
                                "   AND MT.VOUCHER_SUB_TYPE <> 'FD'\n" +
                                " GROUP BY VT.VOUCHER_ID, VT.SEQUENCE_NO ORDER BY MT.VOUCHER_DATE;";
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchVoucherCostCentres:
                    {
                        //Modified to fetch Head Office ledgers if mapped otherwise Branch Ledger
                        query = "SELECT MT.VOUCHER_ID,\n" +
                                "       VCC.SEQUENCE_NO, VCC.LEDGER_SEQUENCE_NO,\n" +
                                "       MCC.COST_CENTRE_NAME,\n" +
                                "       IF(MHL.LEDGER_NAME IS NOT NULL, MHL.LEDGER_NAME, ML.LEDGER_NAME) AS LEDGER_NAME,\n" +
                                "       (VCC.AMOUNT * IF(MT.IS_MULTI_CURRENCY=1, MT.EXCHANGE_RATE, 1)) AS AMOUNT,\n" +
                                "       MCCC.COST_CENTRE_CATEGORY_NAME\n" +
                                "  FROM VOUCHER_MASTER_TRANS MT\n" +
                                " INNER JOIN VOUCHER_CC_TRANS VCC\n" +
                                "    ON VCC.VOUCHER_ID = MT.VOUCHER_ID\n" +
                                "  LEFT JOIN MASTER_LEDGER ML\n" +
                                "    ON VCC.LEDGER_ID = ML.LEDGER_ID\n" +
                                "  LEFT JOIN HEADOFFICE_MAPPED_LEDGER HML\n" +
                                "    ON HML.LEDGER_ID = VCC.LEDGER_ID\n" +
                                "  LEFT JOIN MASTER_HEADOFFICE_LEDGER MHL\n" +
                                "    ON MHL.HEADOFFICE_LEDGER_ID = HML.HEADOFFICE_LEDGER_ID\n" +
                                "  LEFT JOIN MASTER_COST_CENTRE MCC\n" +
                                "    ON MCC.COST_CENTRE_ID = VCC.COST_CENTRE_ID\n" +
                                "  LEFT JOIN COSTCATEGORY_COSTCENTRE CCCC\n" +
                                "    ON CCCC.COST_CENTRE_ID = MCC.COST_CENTRE_ID\n" +
                                "  LEFT JOIN MASTER_COST_CENTRE_CATEGORY MCCC\n" +
                                "    ON MCCC.COST_CENTRECATEGORY_ID = CCCC.COST_CATEGORY_ID\n" +
                                " WHERE MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND\n" +
                                "       ?DATE_TO\n" +
                                "   AND MT.PROJECT_ID in (?PROJECT_ID)\n" +
                                "   AND MT.STATUS = 1 ORDER BY MT.VOUCHER_ID, MT.VOUCHER_DATE ASC;";

                        //query = "SELECT MT.VOUCHER_ID,VCC.SEQUENCE_NO,\n" +
                        //                "       MCC.COST_CENTRE_NAME,\n" +
                        //                "       ML.LEDGER_NAME,\n" +
                        //                "       VCC.AMOUNT,\n" +
                        //                "       MCCC.COST_CENTRE_CATEGORY_NAME\n"+
                        //                "  FROM VOUCHER_MASTER_TRANS MT\n" +
                        //                "\n" +
                        //                " INNER JOIN VOUCHER_CC_TRANS VCC\n" +
                        //                "    ON VCC.VOUCHER_ID = MT.VOUCHER_ID\n" +
                        //                " LEFT JOIN HEADOFFICE_MAPPED_LEDGER HML\n" +
                        //                "   ON HML.LEDGER_ID = VCC.LEDGER_ID\n" +
                        //                " LEFT JOIN MASTER_HEADOFFICE_LEDGER ML\n" +
                        //                "   ON ML.HEADOFFICE_LEDGER_ID = HML.HEADOFFICE_LEDGER_ID\n" +
                        //                //" LEFT JOIN MASTER_LEDGER ML\n" +
                        //                //"    ON ML.LEDGER_ID = VCC.LEDGER_ID\n" +
                        //                " LEFT JOIN MASTER_COST_CENTRE MCC\n" +
                        //                "    ON MCC.COST_CENTRE_ID = VCC.COST_CENTRE_ID\n" +
                        //                " LEFT JOIN COSTCATEGORY_COSTCENTRE CCCC\n"+
                        //                 "   ON CCCC.COST_CENTRE_ID = MCC.COST_CENTRE_ID\n"+
                        //                 "  LEFT JOIN MASTER_COST_CENTRE_CATEGORY MCCC\n"+
                        //                 "  ON MCCC.COST_CENTRECATEGORY_ID = CCCC.COST_CATEGORY_ID\n"+
                        //                "\n" +
                        //                " WHERE MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                        //                "   AND MT.PROJECT_ID in (?PROJECT_ID)\n" +
                        //                "   AND MT.STATUS = 1";

                        break;
                    }
                case SQLCommand.ExportVouchers.FetchVoucherSubLedgers:
                    {
                        //Modified to fetch Head Office ledgers if mapped otherwise Branch Ledger
                        query = "SELECT VST.VOUCHER_ID, VST.LEDGER_ID, VST.SUB_LEDGER_ID, MP.PROJECT,\n" +
                                "IF(MHL.LEDGER_NAME IS NOT NULL, MHL.LEDGER_NAME, ML.LEDGER_NAME) AS LEDGER_NAME, MSL.SUB_LEDGER_NAME, VST.TRANS_MODE, VST.AMOUNT\n" +
                                "FROM VOUCHER_MASTER_TRANS VM\n" +
                                "INNER JOIN VOUCHER_TRANS VT ON VM.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                "INNER JOIN VOUCHER_SUB_LEDGER_TRANS VST ON VST.VOUCHER_ID = VT.VOUCHER_ID AND VST.LEDGER_ID = VT.LEDGER_ID\n" +
                                "INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VST.LEDGER_ID\n" +
                                "INNER JOIN MASTER_SUB_LEDGER MSL ON MSL.SUB_LEDGER_ID = VST.SUB_LEDGER_ID\n" +
                                "INNER JOIN MASTER_PROJECT MP ON MP.PROJECT_ID = VM.PROJECT_ID\n" +
                                "LEFT JOIN HEADOFFICE_MAPPED_LEDGER HML ON HML.LEDGER_ID = VST.LEDGER_ID\n" +
                                "LEFT JOIN MASTER_HEADOFFICE_LEDGER MHL ON MHL.HEADOFFICE_LEDGER_ID = HML.HEADOFFICE_LEDGER_ID\n" +
                                "WHERE VM.STATUS =1 AND VM.PROJECT_ID in (?PROJECT_ID) AND VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO";
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchHOBranchVoucherCostCenter:
                    {
                        query = "SELECT MT.VOUCHER_ID,\n" +
                                "       VCC.SEQUENCE_NO, VCC.LEDGER_SEQUENCE_NO,\n" +
                                "       MCC.COST_CENTRE_NAME,\n" +
                                "       ML.LEDGER_NAME,\n" +
                                "       VCC.AMOUNT,\n" +
                                "       MCCC.COST_CENTRE_CATEGORY_NAME\n" +
                                "  FROM VOUCHER_MASTER_TRANS MT\n" +
                                " INNER JOIN VOUCHER_CC_TRANS VCC\n" +
                                "    ON VCC.VOUCHER_ID = MT.VOUCHER_ID\n" +
                                "  LEFT JOIN MASTER_LEDGER ML\n" +
                                "    ON VCC.LEDGER_ID = ML.LEDGER_ID\n" +
                                "  LEFT JOIN MASTER_COST_CENTRE MCC\n" +
                                "    ON MCC.COST_CENTRE_ID = VCC.COST_CENTRE_ID\n" +
                                "  LEFT JOIN COSTCATEGORY_COSTCENTRE CCCC\n" +
                                "    ON CCCC.COST_CENTRE_ID = MCC.COST_CENTRE_ID\n" +
                                "  LEFT JOIN MASTER_COST_CENTRE_CATEGORY MCCC\n" +
                                "    ON MCCC.COST_CENTRECATEGORY_ID = CCCC.COST_CATEGORY_ID\n" +
                                " WHERE MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND\n" +
                                "       ?DATE_TO\n" +
                                "   AND MT.BRANCH_ID=?BRANCH_OFFICE_ID AND MT.PROJECT_ID in (?PROJECT_ID)\n" +
                                "   AND MT.STATUS = 1 ORDER BY MT.VOUCHER_ID;";
                        break;
                    }

                case SQLCommand.ExportVouchers.FetchBankDetails:
                    {
                        //query = "SELECT MB.BANK_CODE,\n" +
                        //                  "       MB.BANK,\n" +
                        //                  "       MB.BRANCH,\n" +
                        //                  "       MB.ADDRESS,\n" +
                        //                  "       MB.IFSCCODE,\n" +
                        //                  "       MB.MICRCODE,\n" +
                        //                  "       MB.CONTACTNUMBER,\n" +
                        //                  "       MB.SWIFTCODE,\n" +
                        //                  "       ACCOUNT_CODE,\n" +
                        //                  "       ACCOUNT_NUMBER,\n" +
                        //                  "       ACCOUNT_HOLDER_NAME,\n" +
                        //                  "       IS_FCRA_ACCOUNT,\n" +
                        //                  "       MA.LEDGER_ID\n" +
                        //                  "  FROM MASTER_BANK_ACCOUNT MA\n" +
                        //                  "  LEFT JOIN MASTER_BANK MB\n" +
                        //                  "    ON MA.BANK_ID = MB.BANK_ID\n" +
                        //                  " GROUP BY MA.ACCOUNT_NUMBER\n" +
                        //                  " ORDER BY MB.BANK";

                        // Modified by Salamon ( To Fetch the Bank Account details based on the ledger balance)

                        //query = "SELECT MB.BANK_CODE,\n" +
                        //        "       MB.BANK,\n" +
                        //        "       MB.BRANCH,\n" +
                        //        "       MB.ADDRESS,\n" +
                        //        "       MB.IFSCCODE,\n" +
                        //        "       MB.MICRCODE,\n" +
                        //        "       MB.CONTACTNUMBER,\n" +
                        //        "       MB.SWIFTCODE,\n" +
                        //        "       ACCOUNT_CODE,\n" +
                        //        "       ACCOUNT_NUMBER,\n" +
                        //        "       ACCOUNT_HOLDER_NAME,\n" +
                        //        "       ACCOUNT_TYPE_ID,\n" +
                        //        "       DATE_OPENED,\n" +
                        //        "       DATE_CLOSED,\n" +
                        //        "       OPERATED_BY,\n" +
                        //        "       IS_FCRA_ACCOUNT\n" +
                        //        "  FROM LEDGER_BALANCE LB\n" +
                        //        " INNER JOIN MASTER_LEDGER ML\n" +
                        //        "    ON ML.LEDGER_ID = LB.LEDGER_ID\n" +
                        //        " INNER JOIN MASTER_BANK_ACCOUNT MBA\n" +
                        //        "    ON ML.LEDGER_ID = MBA.LEDGER_ID\n" +
                        //        "  LEFT JOIN MASTER_BANK MB\n" +
                        //        "    ON MBA.BANK_ID = MB.BANK_ID\n" +
                        //        " WHERE ML.GROUP_ID IN (12)\n" +
                        //        "   AND PROJECT_ID IN (?PROJECT_ID)\n" +
                        //        " GROUP BY LB.LEDGER_ID;";


                        query = "SELECT *\n" +
                                            "  FROM ((SELECT MB.BANK_ID,\n" +
                                            "                MB.BANK_CODE,\n" +
                                            "                MB.BANK,\n" +
                                            "                MB.BRANCH,\n" +
                                            "                MB.ADDRESS,\n" +
                                            "                MB.IFSCCODE,\n" +
                                            "                MB.MICRCODE,\n" +
                                            "                MB.CONTACTNUMBER,\n" +
                                            "                MB.SWIFTCODE,\n" +
                                            "                MBA.BANK_ACCOUNT_ID,\n" +
                                            "                ACCOUNT_CODE,\n" +
                                            "                ACCOUNT_NUMBER,\n" +
                                            "                ACCOUNT_HOLDER_NAME,\n" +
                                            "                ACCOUNT_TYPE_ID,\n" +
                                            "                DATE_OPENED,\n" +
                                            "                MBA.DATE_CLOSED,\n" +
                                            "                OPERATED_BY,\n" +
                                            "                IS_FCRA_ACCOUNT,\n" +
                                            "                MC.COUNTRY_ID,\n" +
                                            "                MC.COUNTRY\n" +
                                            "           FROM LEDGER_BALANCE LB\n" +
                                            "          INNER JOIN MASTER_LEDGER ML\n" +
                                            "             ON ML.LEDGER_ID = LB.LEDGER_ID\n" +
                                            "          INNER JOIN MASTER_BANK_ACCOUNT MBA\n" +
                                            "             ON ML.LEDGER_ID = MBA.LEDGER_ID\n" +
                                            "          INNER JOIN MASTER_BANK MB\n" +
                                            "             ON MBA.BANK_ID = MB.BANK_ID\n" +
                                            "            LEFT JOIN MASTER_COUNTRY MC\n" +
                                            "              ON MC.COUNTRY_ID = ML.CUR_COUNTRY_ID\n" +
                                            "          WHERE ML.GROUP_ID IN (12)\n" +
                                            "            AND LB.BRANCH_ID=?BRANCH_OFFICE_ID AND PROJECT_ID IN (?PROJECT_ID)\n" +
                            //  "            AND LB.AMOUNT > 0\n" +
                                            "          GROUP BY LB.LEDGER_ID) UNION\n" +
                                            "        (SELECT MB.BANK_ID,\n" +
                                            "                MB.BANK_CODE,\n" +
                                            "                MB.BANK,\n" +
                                            "                MB.BRANCH,\n" +
                                            "                MB.ADDRESS,\n" +
                                            "                MB.IFSCCODE,\n" +
                                            "                MB.MICRCODE,\n" +
                                            "                MB.CONTACTNUMBER,\n" +
                                            "                MB.SWIFTCODE,\n" +
                                            "                '' AS BANK_ACCOUNT_ID,\n" +
                                            "                '' AS ACCOUNT_CODE,\n" +
                                            "                '' AS ACCOUNT_NUMBER,\n" +
                                            "                '' AS ACCOUNT_HOLDER_NAME,\n" +
                                            "                '' AS ACCOUNT_TYPE_ID,\n" +
                                            "                '' AS DATE_OPENED,\n" +
                                            "                '' AS DATE_CLOSED,\n" +
                                            "                '' AS OPERATED_BY,\n" +
                                            "                '' AS IS_FCRA_ACCOUNT,\n" +
                                            "                 '' AS COUNTRY_ID,\n" +
                                            "                 '' AS COUNTRY\n" +
                                            "           FROM FD_ACCOUNT FA\n" +
                                            "          INNER JOIN MASTER_BANK MB\n" +
                                            "             ON FA.BANK_ID = MB.BANK_ID\n" +
                                            "          WHERE FA.BRANCH_ID=?BRANCH_OFFICE_ID AND FA.PROJECT_ID IN (?PROJECT_ID))) AS T\n";

                        //query = "SELECT T.BANK_ID,\n" +
                        //        "       T.BANK_ACCOUNT_ID,\n" +
                        //        "       MB.BANK_CODE,\n" +
                        //        "       MB.BANK,\n" +
                        //        "       MB.BRANCH,\n" +
                        //        "       MB.ADDRESS,\n" +
                        //        "       MB.IFSCCODE,\n" +
                        //        "       MB.MICRCODE,\n" +
                        //        "       MB.CONTACTNUMBER,\n" +
                        //        "       MB.SWIFTCODE,\n" +
                        //        "       ACCOUNT_CODE,\n" +
                        //        "       ACCOUNT_NUMBER,\n" +
                        //        "       ACCOUNT_HOLDER_NAME,\n" +
                        //        "       ACCOUNT_TYPE_ID,\n" +
                        //        "       DATE_OPENED,\n" +
                        //        "       DATE_CLOSED,\n" +
                        //        "       OPERATED_BY,\n" +
                        //        "       IS_FCRA_ACCOUNT\n" +
                        //        "  FROM MASTER_BANK MB\n" +
                        //        "\n" +
                        //        "  LEFT JOIN (SELECT MB1.BANK_ID,\n" +
                        //        "                    ACCOUNT_CODE,\n" +
                        //        "                    MBA.BANK_ACCOUNT_ID,\n" +
                        //        "                    ACCOUNT_NUMBER,\n" +
                        //        "                    ACCOUNT_HOLDER_NAME,\n" +
                        //        "                    ACCOUNT_TYPE_ID,\n" +
                        //        "                    DATE_OPENED,\n" +
                        //        "                    DATE_CLOSED,\n" +
                        //        "                    OPERATED_BY,\n" +
                        //        "                    IS_FCRA_ACCOUNT\n" +
                        //        "               FROM MASTER_BANK MB1\n" +
                        //        "\n" +
                        //        "               LEFT JOIN MASTER_BANK_ACCOUNT MBA\n" +
                        //        "                 ON MB1.BANK_ID = MBA.BANK_ID\n" +
                        //        "               LEFT JOIN MASTER_LEDGER ML\n" +
                        //        "                 ON ML.LEDGER_ID = MBA.LEDGER_ID\n" +
                        //        "               LEFT JOIN LEDGER_BALANCE LB\n" +
                        //        "                 ON LB.LEDGER_ID = ML.LEDGER_ID\n" +
                        //        "              WHERE ML.GROUP_ID IN (12)\n" +
                        //        "                AND PROJECT_ID IN (?PROJECT_ID)\n" +
                        //        "              GROUP BY MB1.BANK_ID, MBA.BANK_ACCOUNT_ID) AS T\n" +
                        //        "    ON T.BANK_ID = MB.BANK_ID";
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchBankDetailsForSplitProject:
                    {
                        query = "SELECT *\n" +
                                            "  FROM ((SELECT MB.BANK_ID,\n" +
                                            "                MB.BANK_CODE,\n" +
                                            "                MB.BANK,\n" +
                                            "                MB.BRANCH,\n" +
                                            "                MB.ADDRESS,\n" +
                                            "                MB.IFSCCODE,\n" +
                                            "                MB.MICRCODE,\n" +
                                            "                MB.CONTACTNUMBER,\n" +
                                            "                MB.SWIFTCODE,\n" +
                                            "                MBA.BANK_ACCOUNT_ID,\n" +
                                            "                ACCOUNT_CODE,\n" +
                                            "                ACCOUNT_NUMBER,\n" +
                                            "                ACCOUNT_HOLDER_NAME,\n" +
                                            "                ACCOUNT_TYPE_ID,\n" +
                                            "                DATE_OPENED,\n" +
                                            "                MBA.DATE_CLOSED,\n" +
                                            "                OPERATED_BY,\n" +
                                            "                IS_FCRA_ACCOUNT\n" +
                                            "           FROM PROJECT_LEDGER PL\n" +
                                            "          INNER JOIN MASTER_LEDGER ML\n" +
                                            "             ON ML.LEDGER_ID = PL.LEDGER_ID\n" +
                                            "          INNER JOIN MASTER_BANK_ACCOUNT MBA\n" +
                                            "             ON ML.LEDGER_ID = MBA.LEDGER_ID\n" +
                                            "          INNER JOIN MASTER_BANK MB\n" +
                                            "             ON MBA.BANK_ID = MB.BANK_ID\n" +
                                            "          WHERE ML.GROUP_ID IN (12)\n" +
                                            "            AND PL.PROJECT_ID IN (?PROJECT_ID) { AND (ML.DATE_CLOSED IS NULL OR ML.DATE_CLOSED >= ?DATE_FROM)}\n" +
                                            "          GROUP BY PL.LEDGER_ID) UNION\n" +
                                            "        (SELECT MB.BANK_ID,\n" +
                                            "                MB.BANK_CODE,\n" +
                                            "                MB.BANK,\n" +
                                            "                MB.BRANCH,\n" +
                                            "                MB.ADDRESS,\n" +
                                            "                MB.IFSCCODE,\n" +
                                            "                MB.MICRCODE,\n" +
                                            "                MB.CONTACTNUMBER,\n" +
                                            "                MB.SWIFTCODE,\n" +
                                            "                '' AS BANK_ACCOUNT_ID,\n" +
                                            "                '' AS ACCOUNT_CODE,\n" +
                                            "                '' AS ACCOUNT_NUMBER,\n" +
                                            "                '' AS ACCOUNT_HOLDER_NAME,\n" +
                                            "                '' AS ACCOUNT_TYPE_ID,\n" +
                                            "                '' AS DATE_OPENED,\n" +
                                            "                '' AS DATE_CLOSED,\n" +
                                            "                '' AS OPERATED_BY,\n" +
                                            "                '' AS IS_FCRA_ACCOUNT\n" +
                                            "           FROM FD_ACCOUNT FA\n" +
                                            "          INNER JOIN MASTER_BANK MB\n" +
                                            "             ON FA.BANK_ID = MB.BANK_ID\n" +
                                            "          WHERE FA.BRANCH_ID=?BRANCH_OFFICE_ID AND FA.PROJECT_ID IN (?PROJECT_ID))) AS T\n";
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchLedgerBalance:
                    {
                        //Modified to fetch Head Office ledgers if mapped otherwise Branch Ledger
                        query = "SELECT MP.PROJECT,\n" +
                                "       LB.BALANCE_DATE,\n" +
                                "       LB.PROJECT_ID,\n" +
                                "       IF(MHL.LEDGER_NAME IS NOT NULL, MHL.LEDGER_NAME, ML.LEDGER_NAME) AS LEDGER_NAME,\n" +
                            //"       AMOUNT,\n" + //23/01/2020 to sum op balance for same ledgers mapped with one HO--------------------------
                            //"       TRANS_MODE,\n" + 
                                "       ABS(SUM(IF(TRANS_MODE='CR', AMOUNT, -AMOUNT))) AS AMOUNT,\n" +
                                "       ABS(SUM(IF(TRANS_MODE='CR', AMOUNT_FC, -AMOUNT_FC))) AS AMOUNT_FC,\n" +
                                "       CASE WHEN COUNT(*) <= 1 THEN TRANS_MODE\n" +
                                "       ELSE\n" +
                                "           IF( SUM(IF(TRANS_MODE='CR', AMOUNT, -AMOUNT))<0, 'DR', 'CR')\n" +
                                "       END AS TRANS_MODE, \n" +
                                "       TRANS_FLAG\n" +
                                "  FROM LEDGER_BALANCE LB\n" +
                                "  LEFT JOIN MASTER_PROJECT MP\n" +
                                "    ON MP.PROJECT_ID = LB.PROJECT_ID\n" +
                                "  LEFT JOIN MASTER_LEDGER ML\n" +
                                "    ON LB.LEDGER_ID = ML.LEDGER_ID\n" +
                                "  LEFT JOIN HEADOFFICE_MAPPED_LEDGER HML\n" +
                                "    ON HML.LEDGER_ID = LB.LEDGER_ID\n" +
                                "  LEFT JOIN MASTER_HEADOFFICE_LEDGER MHL\n" +
                                "    ON MHL.HEADOFFICE_LEDGER_ID = HML.HEADOFFICE_LEDGER_ID\n" +
                                " WHERE TRANS_FLAG = 'OP' AND IF(MHL.LEDGER_NAME IS NULL, ML.LEDGER_NAME IS NOT NULL, MHL.LEDGER_NAME IS NOT NULL)\n" +
                                "   AND MP.PROJECT_ID IN (?PROJECT_ID) GROUP BY MP.PROJECT_ID, IFNULL(MHL.HEADOFFICE_LEDGER_ID, -ML.LEDGER_ID)";
                        //ML.LEDGER_ID On 05/10/2023, If more than one FD ledger mapped wtih FD ledger (sum op balance)

                        //On 13/01/2024, Cash/Bank Ledger's OP amount are update with HO Ledgers which are having same Cash/Bank ids
                        //Cash and Bank Ledgers (not mapped ledgers), Head office id will be null
                        //IFNULL(MHL.HEADOFFICE_LEDGER_ID, ML.LEDGER_ID)
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchSplitProjectLedgerBalance:
                    {
                        // To fetch opening balance if Date From is Book beginning in voucher entry
                        query = "SELECT MP.PROJECT,\n" +
                                "       LB.BALANCE_DATE,\n" +
                                "       LB.PROJECT_ID,\n" +
                                "       ML.LEDGER_NAME,\n" +
                                "       AMOUNT,\n" +
                                "       TRANS_MODE,\n" +
                                "       TRANS_FLAG, ML.GROUP_ID, ML.ACCESS_FLAG\n" +
                                "  FROM LEDGER_BALANCE LB\n" +
                                " INNER JOIN MASTER_PROJECT MP\n" +
                                "    ON MP.PROJECT_ID = LB.PROJECT_ID\n" +
                                " INNER JOIN MASTER_LEDGER ML\n" +
                                "    ON LB.LEDGER_ID = ML.LEDGER_ID\n" +
                                " WHERE TRANS_FLAG = 'OP' AND ML.LEDGER_ID IS NOT NULL\n" +
                                "   AND MP.PROJECT_ID IN (?PROJECT_ID)\n" +
                                "   AND MP.PROJECT_ID NOT IN\n" +
                                "       (SELECT PROJECT_ID\n" +
                                "          FROM VOUCHER_MASTER_TRANS\n" +
                                "         WHERE STATUS = 1\n" +
                                "           AND VOUCHER_DATE < ?DATE_FROM\n" +
                                "         GROUP BY PROJECT_ID\n" +
                                "         ORDER BY VOUCHER_DATE ASC);";

                        //query = "SELECT LB.BALANCE_DATE,\n" +
                        //        "       ML.LEDGER_ID,\n" +
                        //        "       ML.LEDGER_NAME,\n" +
                        //        "       MP.PROJECT,\n" +
                        //        "       LB.BRANCH_ID,\n" +
                        //        "       LB.PROJECT_ID,\n" +
                        //        "       LB.AMOUNT,\n" +
                        //        "       LB.TRANS_MODE,\n" +
                        //        "       LB.TRANS_FLAG\n" +
                        //        "  FROM LEDGER_BALANCE AS LB\n" +
                        //        "  LEFT JOIN MASTER_LEDGER ML\n" +
                        //        "    ON LB.LEDGER_ID = ML.LEDGER_ID\n" +
                        //        "  LEFT JOIN MASTER_PROJECT MP\n" +
                        //        "    ON LB.PROJECT_ID = MP.PROJECT_ID\n" +
                        //        //"  LEFT JOIN HEADOFFICE_MAPPED_LEDGER HML\n" +
                        //        //"    ON HML.LEDGER_ID = LB.LEDGER_ID\n" +
                        //        //"  LEFT JOIN MASTER_HEADOFFICE_LEDGER MHL\n" +
                        //        //"    ON MHL.HEADOFFICE_LEDGER_ID = HML.HEADOFFICE_LEDGER_ID\n" +
                        //        "  JOIN (SELECT LBA.LEDGER_ID AS LED_ID, LBA.BRANCH_ID,\n" +
                        //        "               MAX(LBA.BALANCE_DATE) AS BAL_DATE,\n" +
                        //        "               PROJECT_ID\n" +
                        //        "          FROM LEDGER_BALANCE LBA\n" +
                        //        "         WHERE LBA.BRANCH_ID=?BRANCH_OFFICE_ID AND LBA.PROJECT_ID IN (?PROJECT_ID)\n" +
                        //        "           AND LBA.BALANCE_DATE < ?DATE_FROM\n" +
                        //        "         GROUP BY LBA.LEDGER_ID, PROJECT_ID, BRANCH_ID) AS LB1\n" +
                        //        "    ON LB.LEDGER_ID = LB1.LED_ID\n" +
                        //        "   AND LB.PROJECT_ID = LB1.PROJECT_ID\n" +
                        //        "   AND LB.BRANCH_ID=LB1.BRANCH_ID\n" +
                        //        "   AND LB.BALANCE_DATE = LB1.BAL_DATE AND LB.AMOUNT>0;";
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchSplitProjectLedgerBalanceForSplitFY:
                    {
                        // To fetch opening balance if Date From is Book beginning in voucher entry
                        query = "SELECT MP.PROJECT, ?DATE_FROM AS BALANCE_DATE, LB.PROJECT_ID, ML.LEDGER_NAME, AMOUNT, TRANS_MODE, TRANS_FLAG, ML.GROUP_ID, ML.ACCESS_FLAG\n" +
                                "FROM LEDGER_BALANCE LB\n" +
                                "INNER JOIN MASTER_PROJECT MP ON MP.PROJECT_ID = LB.PROJECT_ID\n" +
                                "INNER JOIN MASTER_LEDGER ML ON LB.LEDGER_ID = ML.LEDGER_ID\n" +
                                "INNER JOIN PROJECT_LEDGER PL ON PL.PROJECT_ID = LB.PROJECT_ID AND PL.LEDGER_ID = ML.LEDGER_ID AND PL.PROJECT_ID IN (?PROJECT_ID)\n" +
                                "INNER JOIN MASTER_LEDGER_GROUP LG ON LG.GROUP_ID = ML.GROUP_ID\n" +
                                "INNER JOIN (SELECT LBA.PROJECT_ID, LBA.LEDGER_ID, MAX(LBA.BALANCE_DATE) AS BAL_DATE\n" +
                                "FROM LEDGER_BALANCE LBA\n" +
                                "WHERE 1 = 1 AND LBA.PROJECT_ID IN (?PROJECT_ID) AND LBA.BALANCE_DATE <?DATE_FROM\n" +
                                "GROUP BY LBA.PROJECT_ID, LBA.LEDGER_ID) LB1 ON LB1.PROJECT_ID = LB.PROJECT_ID AND LB1.LEDGER_ID = LB.LEDGER_ID\n" +
                                "AND LB1.BAL_DATE = LB.BALANCE_DATE\n" +
                                "WHERE ML.LEDGER_ID IS NOT NULL AND MP.PROJECT_ID IN (?PROJECT_ID) AND AMOUNT > 0 AND \n" +
                                "LG.NATURE_ID IN (" + (int)Utility.Natures.Assert + ", " + (int)Utility.Natures.Libilities + ") AND \n" +
                                "ML.GROUP_ID NOT IN (" + (int)Utility.FixedLedgerGroup.FixedDeposit + ") { AND (ML.DATE_CLOSED IS NULL OR ML.DATE_CLOSED >= ?DATE_FROM)}";
                        //"ML.GROUP_ID IN (" + (int)Utility.FixedLedgerGroup.Cash + ", " + (int)Utility.FixedLedgerGroup.BankAccounts + ")";
                        break;
                    }
                case SQLCommand.ExportVouchers.CheckHeadofficeLedgerExists:
                    {
                        // Mapping is required only for the Income and expense ledgers if not mapped.
                        // Ledgers in the transaction date range and all the opening balance ledgers.

                        query = "SELECT T.LEDGER_ID,\n" +
                                 "       T.LEDGER_CODE,\n" +
                                 "       T.LEDGER_NAME,\n" +
                                 "       T.LEDGER_GROUP,\n" +
                                 "       T.HEADOFFICE_LEDGER_ID,\n" +
                                 "       T.LEDGER_TYPE,\n" +
                                 "       T.LEDGER_SUB_TYPE,\n" +
                                 "       T.GROUP_ID,\n" +
                                 "       T.IS_BANK_INTEREST_LEDGER, T.IS_BANK_FD_PENALTY_LEDGER, T.IS_BANK_SB_INTEREST_LEDGER, T.IS_BANK_COMMISSION_LEDGER,\n" +
                                 "       T.SORT_ID\n" +
                                 "  FROM ((SELECT LB.LEDGER_ID,\n" +
                                 "                ML.LEDGER_CODE,\n" +
                                 "                CONCAT(ML.LEDGER_NAME,\n" +
                                 "                       CONCAT('  (', ML.LEDGER_CODE),\n" +
                                 "                       CONCAT('  -  ', MLG.LEDGER_GROUP),\n" +
                                 "                       ') ') AS 'LEDGER_NAME',\n" +
                                 "                '' AS HEADOFFICE_LEDGER_ID,\n" +
                                 "                MLG.LEDGER_GROUP,\n" +
                                 "                ML.LEDGER_TYPE,\n" +
                                 "                ML.GROUP_ID,\n" +
                                 "                ML.IS_BANK_INTEREST_LEDGER, ML.IS_BANK_FD_PENALTY_LEDGER, ML.IS_BANK_SB_INTEREST_LEDGER, ML.IS_BANK_COMMISSION_LEDGER,\n" +
                                 "                ML.LEDGER_SUB_TYPE,\n" +
                                 "                ML.SORT_ID\n" +
                                 "           FROM LEDGER_BALANCE LB\n" +
                                 "          INNER JOIN MASTER_LEDGER ML\n" +
                                 "             ON ML.LEDGER_ID = LB.LEDGER_ID\n" +
                                 "           LEFT JOIN MASTER_LEDGER_GROUP MLG\n" +
                                 "             ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                                 "          WHERE \n" +    //MLG.NATURE_ID NOT IN (3, 4)
                                 "            MLG.GROUP_ID NOT IN(12,13) AND  LB.PROJECT_ID IN (?PROJECT_ID)\n" +
                                 "            AND LB.BALANCE_DATE BETWEEN ?DATE_FROM AND\n" +
                                 "                ?DATE_TO\n" +
                                 "            AND LB.LEDGER_ID NOT IN\n" +
                                 "                (SELECT LEDGER_ID FROM HEADOFFICE_MAPPED_LEDGER)) UNION\n" +
                                 "        (SELECT LB.LEDGER_ID,\n" +
                                 "                ML.LEDGER_CODE,\n" +
                                 "                CONCAT(ML.LEDGER_NAME,\n" +
                                 "                       CONCAT('  (', ML.LEDGER_CODE),\n" +
                                 "                       CONCAT('  -  ', MLG.LEDGER_GROUP),\n" +
                                 "                       ') ') AS 'LEDGER_NAME',\n" +
                                 "                '' AS HEADOFFICE_LEDGER_ID,\n" +
                                 "                MLG.LEDGER_GROUP,\n" +
                                 "                ML.LEDGER_TYPE,\n" +
                                 "                ML.GROUP_ID,\n" +
                                 "                ML.IS_BANK_INTEREST_LEDGER,  ML.IS_BANK_FD_PENALTY_LEDGER, ML.IS_BANK_SB_INTEREST_LEDGER, ML.IS_BANK_COMMISSION_LEDGER,\n" +
                                 "                ML.LEDGER_SUB_TYPE,\n" +
                                 "                ML.SORT_ID\n" +
                                 "           FROM LEDGER_BALANCE LB\n" +
                                 "          INNER JOIN MASTER_LEDGER ML\n" +
                                 "             ON ML.LEDGER_ID = LB.LEDGER_ID\n" +
                                 "           LEFT JOIN MASTER_LEDGER_GROUP MLG\n" +
                                 "             ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                                 "          WHERE \n" +   //MLG.NATURE_ID NOT IN (3, 4)
                                 "            MLG.GROUP_ID NOT IN(12,13) AND LB.PROJECT_ID IN (?PROJECT_ID)\n" +
                                 "            AND LB.TRANS_FLAG = 'OP'\n" +
                                 "            AND LB.LEDGER_ID NOT IN\n" +
                                 "                (SELECT LEDGER_ID FROM HEADOFFICE_MAPPED_LEDGER)) UNION\n" +
                                 "        (SELECT LB.LEDGER_ID,\n" +
                                 "                ML.LEDGER_CODE,\n" +
                                 "                CONCAT(ML.LEDGER_NAME,\n" +
                                 "                       CONCAT('  (', ML.LEDGER_CODE),\n" +
                                 "                       CONCAT('  -  ', MLG.LEDGER_GROUP),\n" +
                                 "                       ') ') AS 'LEDGER_NAME',\n" +
                                 "                '' AS HEADOFFICE_LEDGER_ID,\n" +
                                 "                MLG.LEDGER_GROUP,\n" +
                                 "                ML.LEDGER_TYPE,\n" +
                                 "                ML.GROUP_ID,\n" +
                                 "                ML.IS_BANK_INTEREST_LEDGER,  ML.IS_BANK_FD_PENALTY_LEDGER, ML.IS_BANK_SB_INTEREST_LEDGER, ML.IS_BANK_COMMISSION_LEDGER,\n" +
                                 "                ML.LEDGER_SUB_TYPE,\n" +
                                 "                ML.SORT_ID\n" +
                                 "           FROM LEDGER_BALANCE LB\n" +
                                 "          INNER JOIN MASTER_LEDGER ML\n" +
                                 "             ON ML.LEDGER_ID = LB.LEDGER_ID\n" +
                                 "           LEFT JOIN MASTER_LEDGER_GROUP MLG\n" +
                                 "             ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                                 "          WHERE \n" +  //MLG.NATURE_ID NOT IN (3, 4)
                                 "          MLG.GROUP_ID NOT IN(12,13) AND  LB.PROJECT_ID IN (?PROJECT_ID)\n" +
                                 "            AND ML.LEDGER_SUB_TYPE = 'FD'\n" +
                                 "            AND LB.LEDGER_ID NOT IN\n" +
                                 "                (SELECT LEDGER_ID FROM HEADOFFICE_MAPPED_LEDGER))) AS T\n" +
                                 " GROUP BY T.LEDGER_ID;";


                        break;
                    }
                case SQLCommand.ExportVouchers.AllUnMappedMappedLedgers:
                    {
                        //query = "SELECT MHL.LEDGER_CODE,ML.LEDGER_NAME as LEDGER_NAME,ML.LEDGER_ID, " +
                        //                    "    HML.HEADOFFICE_LEDGER_ID, " +
                        //                    "    MHL.LEDGER_NAME AS HEADOFFICELEDGER, " +
                        //                    "   MLG.LEDGER_GROUP, " +
                        //                    "   ML.LEDGER_TYPE, " +
                        //                    "   ML.LEDGER_SUB_TYPE, " +
                        //                    "   ML.SORT_ID " +
                        //                    "   FROM LEDGER_BALANCE LB " +
                        //                    "   INNER JOIN HEADOFFICE_MAPPED_LEDGER HML " +
                        //                    "   ON HML.LEDGER_ID = LB.LEDGER_ID " +
                        //                    "    LEFT JOIN MASTER_HEADOFFICE_LEDGER MHL " +
                        //                    "   ON MHL.HEADOFFICE_LEDGER_ID = HML.HEADOFFICE_LEDGER_ID " +
                        //                    "    LEFT JOIN MASTER_LEDGER ML " +
                        //                    "   ON ML.LEDGER_ID = HML.LEDGER_ID " +
                        //                    "   LEFT JOIN MASTER_LEDGER_GROUP MLG ON MLG.GROUP_ID = ML.GROUP_ID "+
                        //                    "   WHERE  ML.GROUP_ID NOT IN (12, 13, 14) " +
                        //                    " GROUP BY MHL.HEADOFFICE_LEDGER_ID;";
                        query = "SELECT ML.LEDGER_CODE,ML.GROUP_ID, ML.LEDGER_ID, MHL.HEADOFFICE_LEDGER_ID, " +
                                            " CONCAT(ML.LEDGER_NAME,CONCAT('  (',ML.LEDGER_CODE), CONCAT('  -  ',MLG.LEDGER_GROUP), ') ') AS 'LEDGER_NAME', " +
                                            "  ML.IS_BANK_INTEREST_LEDGER, ML.IS_BANK_FD_PENALTY_LEDGER, ML.IS_BANK_SB_INTEREST_LEDGER, ML.IS_BANK_COMMISSION_LEDGER, IFNULL(MP.PROJECT, '') AS PROJECT," +
                                            "  MHL.LEDGER_NAME AS 'HEADOFFICELEDGER', MLG.LEDGER_GROUP, ML.LEDGER_TYPE, ML.LEDGER_SUB_TYPE," +
                                            "  ML.IS_BANK_INTEREST_LEDGER, ML.SORT_ID " +
                                            "  FROM MASTER_LEDGER ML " +
                                            "  LEFT JOIN HEADOFFICE_MAPPED_LEDGER HML ON HML.LEDGER_ID = ML.LEDGER_ID " +
                                            "  LEFT JOIN MASTER_HEADOFFICE_LEDGER MHL ON MHL.HEADOFFICE_LEDGER_ID = HML.HEADOFFICE_LEDGER_ID " +
                                            "  LEFT JOIN MASTER_LEDGER_GROUP MLG ON MLG.GROUP_ID = ML.GROUP_ID " +
                                            "  LEFT JOIN (SELECT PL.LEDGER_ID, GROUP_CONCAT(MP.PROJECT, '') AS PROJECT FROM PROJECT_LEDGER PL " +
                                            "  INNER JOIN MASTER_PROJECT MP ON MP.PROJECT_ID = PL.PROJECT_ID GROUP BY PL.LEDGER_ID) MP ON MP.LEDGER_ID = ML.LEDGER_ID " +
                                            "  WHERE  ML.GROUP_ID NOT IN (12, 13) " +
                                            "  ORDER BY HEADOFFICELEDGER;";
                        break;
                    }
                case SQLCommand.ExportVouchers.DeleteMappingLedgersAll:
                    {
                        query = "DELETE FROM HEADOFFICE_MAPPED_LEDGER WHERE LEDGER_ID=?LEDGER_ID";
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchHeadOfficebyLedger:
                    {
                        query = "SELECT HEADOFFICE_LEDGER_ID,MHL.GROUP_ID, CONCAT(LEDGER_NAME,' ( ',LEDGER_GROUP,' )') AS HEADOFFICELEDGER," +
                                "MHL.IS_BANK_INTEREST_LEDGER, MHL.IS_BANK_FD_PENALTY_LEDGER, MHL.IS_BANK_SB_INTEREST_LEDGER, MHL.IS_BANK_COMMISSION_LEDGER " +
                                "FROM MASTER_HEADOFFICE_LEDGER MHL " +
                                "INNER JOIN MASTER_LEDGER_GROUP MLG ON MHL.GROUP_ID=MLG.GROUP_ID ORDER BY LEDGER_NAME ASC";
                        break;
                    }
                case SQLCommand.ExportVouchers.MapBrachHeadOffice:
                    {
                        query = "INSERT INTO HEADOFFICE_MAPPED_LEDGER(LEDGER_ID, HEADOFFICE_LEDGER_ID) VALUES (?LEDGER_ID,?HEADOFFICE_LEDGER_ID)";
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchCountry:
                    {
                        query = "SELECT MC.COUNTRY, COUNTRY_CODE, CURRENCY_CODE, CURRENCY_SYMBOL, CURRENCY_NAME\n" +
                                " FROM MASTER_COUNTRY MC";

                        /*query = "(SELECT MC.COUNTRY,\n" +
                                "       COUNTRY_CODE,\n" +
                                "       CURRENCY_CODE,\n" +
                                "       CURRENCY_SYMBOL,\n" +
                                "       CURRENCY_NAME\n" +
                                "  FROM MASTER_COUNTRY MC\n" +
                                " INNER JOIN VOUCHER_MASTER_TRANS MT\n" +
                                "    ON MC.COUNTRY_ID = MT.CURRENCY_COUNTRY_ID\n" +
                                "    OR MC.COUNTRY_ID = MT.EXCHANGE_COUNTRY_ID\n" +
                                "WHERE MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                " GROUP BY MC.COUNTRY_ID)\n" +
                                " UNION\n" +
                                "(SELECT MC.COUNTRY,\n" +
                                "       COUNTRY_CODE,\n" +
                                "       CURRENCY_CODE,\n" +
                                "       CURRENCY_SYMBOL,\n" +
                                "       CURRENCY_NAME\n" +
                                "  FROM master_donaud MD\n" +
                                "\n" +
                                " INNER JOIN VOUCHER_MASTER_TRANS MT\n" +
                                "    ON MT.DONOR_ID = MD.DONAUD_ID\n" +
                                " INNER JOIN MASTER_COUNTRY MC\n" +
                                "ON MC.COUNTRY_ID=MD.COUNTRY_ID\n" +
                                "WHERE MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                " GROUP BY MC.COUNTRY )";*/
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchState:
                    {
                        query = "SELECT MS.STATE_ID, IFNULL(MS.STATE_CODE, '') AS STATE_CODE, MS.STATE_NAME, MC.COUNTRY\n" +
                                "FROM MASTER_STATE MS\n" +
                                "INNER JOIN MASTER_COUNTRY MC ON MC.COUNTRY_ID = MS.COUNTRY_ID;";
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchHOBranchLedger:
                    {
                        query = "SELECT T.LEDGER_CODE,\n" +
                              "       T.LEDGER_NAME,\n" +
                              "       T.IS_BRANCH_LEDGER,\n" +
                              "       T.GROUP_ID,\n" +
                              "       T.LEDGER_GROUP,\n" +
                              "       T.NATURE_ID,\n" +
                              "       T.LEDGER_TYPE,\n" +
                              "       T.LEDGER_SUB_TYPE, IFNULL(T.FD_INVESTMENT_TYPE_ID,0 ) AS FD_INVESTMENT_TYPE_ID,\n" +
                              "       T.SORT_ID,\n" +
                              "       T.IS_COST_CENTER, T.IS_TDS_LEDGER, T.IS_BANK_INTEREST_LEDGER,\n" +
                              "       T.IS_INKIND_LEDGER, T.IS_DEPRECIATION_LEDGER, T.IS_ASSET_GAIN_LEDGER, T.IS_ASSET_LOSS_LEDGER, T.IS_DISPOSAL_LEDGER, T.IS_SUBSIDY_LEDGER,\n" +
                              "       T.IS_GST_LEDGERS, T.GST_SERVICE_TYPE, T.DATE_CLOSED, T.CLOSED_BY,\n" +
                              "       T.BUDGET_GROUP_ID, T.BUDGET_SUB_GROUP_ID\n" +
                              "  FROM ((SELECT ML.LEDGER_CODE,\n" +
                              "                ML.LEDGER_NAME,\n" +
                              "                MLG.LEDGER_GROUP,\n" +
                              "                1 AS IS_BRANCH_LEDGER,\n" +
                              "                ML.GROUP_ID,\n" +
                              "                MLG.NATURE_ID,\n" +
                              "                ML.LEDGER_TYPE,\n" +
                              "                ML.LEDGER_SUB_TYPE, IFNULL(ML.FD_INVESTMENT_TYPE_ID, 0 ) AS FD_INVESTMENT_TYPE_ID,\n" +
                              "                ML.SORT_ID,\n" +
                              "                ML.IS_COST_CENTER, ML.IS_TDS_LEDGER, ML.IS_BANK_INTEREST_LEDGER,\n" +
                              "                ML.IS_INKIND_LEDGER, ML.IS_DEPRECIATION_LEDGER,\n" +
                              "                ML.IS_ASSET_GAIN_LEDGER, ML.IS_ASSET_LOSS_LEDGER, ML.IS_DISPOSAL_LEDGER, ML.IS_SUBSIDY_LEDGER, \n" +
                              "                ML.IS_GST_LEDGERS, ML.GST_SERVICE_TYPE, ML.DATE_CLOSED, ML.CLOSED_BY,\n" +
                              "                ML.BUDGET_GROUP_ID, ML.BUDGET_SUB_GROUP_ID \n" +
                              "           FROM LEDGER_BALANCE LB\n" +
                              "           LEFT JOIN MASTER_LEDGER ML\n" +
                              "             ON ML.LEDGER_ID = LB.LEDGER_ID\n" +
                              "           LEFT JOIN MASTER_LEDGER_GROUP MLG\n" +
                              "             ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                              "          WHERE LB.BRANCH_ID=?BRANCH_OFFICE_ID AND PROJECT_ID IN (?PROJECT_ID)\n" +
                              "            AND LB.BALANCE_DATE BETWEEN ?DATE_FROM AND ?DATE_TO) UNION\n" +
                              "        (SELECT ML.LEDGER_CODE,\n" +
                              "                ML.LEDGER_NAME,\n" +
                              "                MLG.LEDGER_GROUP,\n" +
                              "                1 AS IS_BRANCH_LEDGER,\n" +
                              "                ML.GROUP_ID,\n" +
                              "                MLG.NATURE_ID,\n" +
                              "                ML.LEDGER_TYPE,\n" +
                              "                ML.LEDGER_SUB_TYPE, IFNULL(ML.FD_INVESTMENT_TYPE_ID, 0 ) AS FD_INVESTMENT_TYPE_ID,\n" +
                              "                ML.SORT_ID,\n" +
                              "                ML.IS_COST_CENTER, ML.IS_TDS_LEDGER, ML.IS_BANK_INTEREST_LEDGER,\n" +
                              "                ML.IS_INKIND_LEDGER, ML.IS_DEPRECIATION_LEDGER,\n" +
                              "                ML.IS_ASSET_GAIN_LEDGER, ML.IS_ASSET_LOSS_LEDGER, ML.IS_DISPOSAL_LEDGER, ML.IS_SUBSIDY_LEDGER, \n" +
                              "                ML.IS_GST_LEDGERS, ML.GST_SERVICE_TYPE, ML.DATE_CLOSED, ML.CLOSED_BY,\n" +
                              "                ML.BUDGET_GROUP_ID, ML.BUDGET_SUB_GROUP_ID \n" +
                              "           FROM LEDGER_BALANCE LB\n" +
                              "           LEFT JOIN MASTER_LEDGER ML\n" +
                              "             ON ML.LEDGER_ID = LB.LEDGER_ID\n" +
                              "           LEFT JOIN MASTER_LEDGER_GROUP MLG\n" +
                              "             ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                              "          WHERE LB.BRANCH_ID=?BRANCH_OFFICE_ID AND PROJECT_ID IN (?PROJECT_ID)\n" +
                              "            AND LB.TRANS_FLAG = 'OP') UNION\n" +
                              "        (SELECT ML.LEDGER_CODE,\n" +
                              "                ML.LEDGER_NAME,\n" +
                              "                MLG.LEDGER_GROUP,\n" +
                              "                1 AS IS_BRANCH_LEDGER,\n" +
                              "                ML.GROUP_ID,\n" +
                              "                MLG.NATURE_ID,\n" +
                              "                ML.LEDGER_TYPE,\n" +
                              "                ML.LEDGER_SUB_TYPE, IFNULL(ML.FD_INVESTMENT_TYPE_ID, 0 ) AS FD_INVESTMENT_TYPE_ID, \n" +
                              "                ML.SORT_ID,\n" +
                              "                ML.IS_COST_CENTER, ML.IS_TDS_LEDGER, ML.IS_BANK_INTEREST_LEDGER,\n" +
                              "                ML.IS_INKIND_LEDGER, ML.IS_DEPRECIATION_LEDGER,\n" +
                              "                ML.IS_ASSET_GAIN_LEDGER, ML.IS_ASSET_LOSS_LEDGER, ML.IS_DISPOSAL_LEDGER, ML.IS_SUBSIDY_LEDGER, \n" +
                              "                ML.IS_GST_LEDGERS, ML.GST_SERVICE_TYPE, ML.DATE_CLOSED, ML.CLOSED_BY,\n" +
                              "                ML.BUDGET_GROUP_ID, ML.BUDGET_SUB_GROUP_ID \n" +
                              "           FROM LEDGER_BALANCE LB\n" +
                              "           LEFT JOIN MASTER_LEDGER ML\n" +
                              "             ON ML.LEDGER_ID = LB.LEDGER_ID\n" +
                              "           LEFT JOIN MASTER_LEDGER_GROUP MLG\n" +
                              "             ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                              "          WHERE LB.BRANCH_ID=?BRANCH_OFFICE_ID AND  PROJECT_ID IN (?PROJECT_ID)\n" +
                              "            AND ML.LEDGER_SUB_TYPE = 'FD'\n" +
                              "             OR ML.IS_BANK_INTEREST_LEDGER = 1\n" +
                              ")) AS T\n" +
                              " WHERE T.GROUP_ID NOT IN (12)\n" +
                              " GROUP BY LEDGER_NAME;";
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchHOBranchLedgerSplitFY:
                    {
                        query = "SELECT ML.LEDGER_CODE, ML.IS_COST_CENTER, ML.IS_BANK_INTEREST_LEDGER, ML.LEDGER_NAME, MLG.LEDGER_GROUP,\n" +
                                    "1 AS IS_BRANCH_LEDGER, ML.GROUP_ID, MLG.NATURE_ID, ML.ACCESS_FLAG, \n" +
                                    "ML.LEDGER_TYPE, ML.LEDGER_SUB_TYPE, IFNULL(ML.FD_INVESTMENT_TYPE_ID, 0 ) AS FD_INVESTMENT_TYPE_ID, ML.SORT_ID,\n" +
                                    "ML.IS_COST_CENTER, ML.IS_TDS_LEDGER, ML.IS_BANK_FD_PENALTY_LEDGER, ML.IS_BANK_SB_INTEREST_LEDGER, ML.IS_BANK_COMMISSION_LEDGER,\n" +
                                    "ML.IS_INKIND_LEDGER, ML.IS_DEPRECIATION_LEDGER, ML.IS_ASSET_GAIN_LEDGER, ML.IS_ASSET_LOSS_LEDGER,\n" +
                                    "ML.IS_DISPOSAL_LEDGER, ML.IS_SUBSIDY_LEDGER,\n" +
                                    "ML.IS_GST_LEDGERS, ML.GST_SERVICE_TYPE, GST.GST_ID, GST.GST_NO, GST.SLAB, ML.DATE_CLOSED, ML.CLOSED_BY,\n" +
                                    "IFNULL(ML.GST_HSN_SAC_CODE,'') AS GST_HSN_SAC_CODE,\n" +
                                    "ML.BUDGET_GROUP_ID, ML.BUDGET_SUB_GROUP_ID\n" +
                                    "FROM MASTER_LEDGER ML\n" +
                                    "INNER JOIN MASTER_LEDGER_GROUP MLG ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                                    "INNER JOIN PROJECT_LEDGER PL ON PL.LEDGER_ID = ML.LEDGER_ID\n" +
                                    "LEFT JOIN (SELECT TCP.LEDGER_ID, TCP.GST_ID, TCP.GST_NO, MGC.SLAB\n" +
                                        "FROM TDS_CREDTIORS_PROFILE TCP\n" +
                                        "LEFT JOIN MASTER_GST_CLASS MGC ON MGC.GST_ID = TCP.GST_ID) AS GST ON GST.LEDGER_ID = ML.LEDGER_ID\n" +
                                    "WHERE PL.PROJECT_ID IN (?PROJECT_ID) AND ML.GROUP_ID NOT IN (" + (int)Utility.FixedLedgerGroup.BankAccounts + ")\n" +
                                    "{ AND (ML.DATE_CLOSED IS NULL OR ML.DATE_CLOSED >= ?DATE_FROM)}";
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchLedgerProfile:
                    {
                        query = "SELECT TCP.CREDITORS_PROFILE_ID, ML.LEDGER_NAME, TCP.NAME, TCP.ADDRESS, MS.STATE_NAME, MC.COUNTRY, TCP.PIN_CODE,\n" +
                                    "TCP.CONTACT_PERSON, TCP.CONTACT_NUMBER, TCP.EMAIL, TCP.PAN_NUMBER, TCP.PAN_IT_HOLDER_NAME, TCP.GST_NO, MGS.SLAB\n" +
                                    "FROM TDS_CREDTIORS_PROFILE TCP\n" +
                                    "INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = TCP.LEDGER_ID\n" +
                                    "INNER JOIN PROJECT_LEDGER PL ON PL.LEDGER_ID = ML.LEDGER_ID\n" +
                                    "LEFT JOIN MASTER_COUNTRY MC ON MC.COUNTRY_ID =TCP.COUNTRY_ID\n" +
                                    "LEFT JOIN MASTER_STATE MS ON MS.STATE_ID = TCP.STATE_ID\n" +
                                    "LEFT JOIN MASTER_GST_CLASS MGS ON MGS.GST_ID = TCP.GST_ID\n" +
                                    "WHERE PL.PROJECT_ID IN (?PROJECT_ID) { AND (ML.DATE_CLOSED IS NULL OR ML.DATE_CLOSED >= ?DATE_FROM)}";
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchMasterGSTClass:
                    {
                        query = "SELECT GST_ID, SLAB, GST, CGST, SGST, IGST, APPLICABLE_FROM, STATUS, SORT_ORDER\n" +
                                "FROM MASTER_GST_CLASS;";
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchAssetStockVendors:
                    {
                        query = @"SELECT VENDOR, ADDRESS, MS.STATE_NAME, MC.COUNTRY, PAN_NO, GST_NO, CONTACT_NO, EMAIL_ID, BRANCH_ID 
                                  FROM ASSET_STOCK_VENDOR ASV
                                  LEFT JOIN MASTER_STATE MS ON MS.STATE_ID = ASV.STATE_ID
                                  LEFT JOIN MASTER_COUNTRY MC ON MC.COUNTRY_ID = ASV.COUNTRY_ID";
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchGSTInvoiceMaster:
                    {
                        query = @"SELECT GIM.GST_INVOICE_ID, GIM.BOOKING_VOUCHER_ID, IFNULL(GIM.BOOKING_VOUCHER_TYPE, 'RC') AS BOOKING_VOUCHER_TYPE, ASV.VENDOR, 
                                        GIM.GST_VENDOR_INVOICE_NO, GIM.GST_VENDOR_INVOICE_DATE,
                                        GIM.GST_VENDOR_INVOICE_TYPE, GIM.GST_VENDOR_ID, GIM.IS_REVERSE_CHARGE, GIM.REVERSE_CHARGE_AMOUNT,
                                        GIM.TRANSPORT_MODE, GIM.VEHICLE_NUMBER, GIM.SUPPLY_DATE, GIM.SUPPLY_PLACE,
                                        GIM.BILLING_NAME, GIM.BILLING_GST_NO, GIM.BILLING_ADDRESS,
                                        GIM.BILLING_STATE_ID, GIM.BILLING_COUNTRY_ID, MSB.STATE_NAME AS BILLING_STATE_NAME, MCB.COUNTRY AS BILLING_COUNTRY,
                                        GIM.SHIPPING_NAME, GIM.SHIPPING_GST_NO, GIM.SHIPPING_ADDRESS, MSS.STATE_NAME AS SHIPPING_STATE_NAME, MCS.COUNTRY AS SHIPPING_COUNTRY,
                                        GIM.SHIPPING_STATE_ID, GIM.SHIPPING_COUNTRY_ID,
                                        GIM.CHEQUE_IN_FAVOUR, GIM.TOTAL_AMOUNT, GIM.TOTAL_CGST_AMOUNT, GIM.TOTAL_SGST_AMOUNT,
                                        GIM.TOTAL_IGST_AMOUNT, GIM.BRANCH_ID, GIM.STATUS
                                        FROM GST_INVOICE_MASTER GIM
                                        INNER JOIN GST_INVOICE_MASTER_DETAILS GIMD ON GIM.GST_INVOICE_ID = GIMD.GST_INVOICE_ID
                                        INNER JOIN VOUCHER_MASTER_TRANS MT ON MT.VOUCHER_ID  = GIM.BOOKING_VOUCHER_ID
                                        AND MT.PROJECT_ID IN (?PROJECT_ID) AND MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO
                                        LEFT JOIN ASSET_STOCK_VENDOR ASV ON ASV.VENDOR_ID = GIM.GST_VENDOR_ID
                                        LEFT JOIN MASTER_STATE MSB ON MSB.STATE_ID = GIM.BILLING_STATE_ID
                                        LEFT JOIN MASTER_COUNTRY MCB ON MCB.COUNTRY_ID = GIM.BILLING_COUNTRY_ID
                                        LEFT JOIN MASTER_STATE MSS ON MSS.STATE_ID = GIM.SHIPPING_STATE_ID
                                        LEFT JOIN MASTER_COUNTRY MCS ON MCS.COUNTRY_ID = GIM.SHIPPING_COUNTRY_ID
                                        GROUP BY GIM.GST_INVOICE_ID;";
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchGSTInvoiceMasterLedgerDetail:
                    {
                        query = @"SELECT MGC.GST_ID , GIMD.LEDGER_GST_CLASS_ID, GIMD.GST_INVOICE_ID, GIMD.LEDGER_ID, GIMD.LEDGER_GST_CLASS_ID,
                                        ML.LEDGER_NAME, MGC.SLAB,
                                        GIMD.QUANTITY, GIMD.UNIT_MEASUREMENT,
                                        GIMD.UNIT_AMOUNT, GIMD.DISCOUNT, GIMD.AMOUNT, GIMD.TRANS_MODE, GIMD.GST_HSN_SAC_CODE, GIMD.CGST, GIMD.SGST, GIMD.IGST, GIMD.BRANCH_ID
                                        FROM GST_INVOICE_MASTER GIM
                                        INNER JOIN GST_INVOICE_MASTER_DETAILS GIMD ON GIM.GST_INVOICE_ID = GIMD.GST_INVOICE_ID
                                        INNER JOIN MASTER_LEDGER AS ML ON ML.LEDGER_ID = GIMD.LEDGER_ID
                                        INNER JOIN VOUCHER_MASTER_TRANS MT ON MT.VOUCHER_ID  = GIM.BOOKING_VOUCHER_ID
                                        LEFT JOIN MASTER_GST_CLASS MGC ON MGC.GST_ID = GIMD.LEDGER_GST_CLASS_ID
                                        WHERE MT.PROJECT_ID IN (?PROJECT_ID) AND MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO;";

                        /*LEFT JOIN (SELECT TCP.LEDGER_ID, TCP.GST_ID, TCP.GST_NO, MGC.SLAB
                                                  FROM TDS_CREDTIORS_PROFILE TCP
                                                  LEFT JOIN MASTER_GST_CLASS MGC ON MGC.GST_ID = TCP.GST_ID) AS GST ON GST.LEDGER_ID = GIMD.LEDGER_ID AND GST.LEDGER_ID = ML.LEDGER_ID
                                                  AND GST.GST_ID = GIMD.LEDGER_GST_CLASS_ID*/
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchGSTInvoiceVoucher:
                    {
                        query = @"SELECT GIM.GST_INVOICE_ID, GIM.BOOKING_VOUCHER_ID, VGI.GST_INVOICE_ID, VGI.AMOUNT
                                FROM GST_INVOICE_MASTER GIM
                                INNER JOIN VOUCHER_GST_INVOICE VGI ON GIM.GST_INVOICE_ID = VGI.GST_INVOICE_ID
                                INNER JOIN VOUCHER_MASTER_TRANS MT ON MT.VOUCHER_ID  = GIM.BOOKING_VOUCHER_ID
                                WHERE MT.PROJECT_ID IN (?PROJECT_ID) AND MT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO;";
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchHeadOfficeLedger:
                    {
                        //Fetching Head Office Ledger details if BO ledgers mapped with HO ledger, Otherwise BO Ledgers based on Ledger Balance.
                        query = "SELECT T.LEDGER_CODE,\n" +
                              "       T.LEDGER_NAME,\n" +
                              "       T.IS_BRANCH_LEDGER,\n" +
                              "       T.GROUP_ID,\n" +
                              "       T.LEDGER_GROUP,\n" +
                              "       T.NATURE_ID,\n" +
                              "       T.LEDGER_TYPE,\n" +
                              "       T.LEDGER_SUB_TYPE,\n" +
                              "       T.SORT_ID,\n" +
                              "       T.IS_COST_CENTER, T.IS_TDS_LEDGER, T.IS_BANK_INTEREST_LEDGER,\n" +
                              "       T.IS_INKIND_LEDGER, T.IS_DEPRECIATION_LEDGER, T.IS_ASSET_GAIN_LEDGER, T.IS_ASSET_LOSS_LEDGER,\n" +
                              "       T.IS_DISPOSAL_LEDGER, T.IS_SUBSIDY_LEDGER,\n" +
                              "       T.IS_GST_LEDGERS, T.GST_SERVICE_TYPE,\n" +
                              "       T.BUDGET_GROUP_ID, T.BUDGET_SUB_GROUP_ID, T.COUNTRY_ID, T.COUNTRY, T.OP_EXCHANGE_RATE\n" +
                              "  FROM ((SELECT ML.LEDGER_CODE,\n" +
                              "                IF(MHL.LEDGER_NAME IS NOT NULL,\n" +
                              "                   MHL.LEDGER_NAME,\n" +
                              "                   ML.LEDGER_NAME) AS LEDGER_NAME,\n" +
                              "                IF(MHL.LEDGER_NAME IS NOT NULL,\n" +
                              "                   HLG.LEDGER_GROUP,\n" +
                              "                   MLG.LEDGER_GROUP) AS LEDGER_GROUP,\n" +
                              "                IF(MHL.LEDGER_NAME IS NOT NULL, 0, 1) AS IS_BRANCH_LEDGER,\n" +
                              "                ML.GROUP_ID,\n" +
                              "                MLG.NATURE_ID,\n" +
                              "                ML.LEDGER_TYPE,\n" +
                              "                ML.LEDGER_SUB_TYPE,\n" +
                              "                ML.SORT_ID,\n" +
                              "                ML.IS_COST_CENTER, ML.IS_TDS_LEDGER, ML.IS_BANK_INTEREST_LEDGER,\n" +
                              "                ML.IS_INKIND_LEDGER, ML.IS_DEPRECIATION_LEDGER, ML.IS_ASSET_GAIN_LEDGER, ML.IS_ASSET_LOSS_LEDGER,\n" +
                              "                ML.IS_DISPOSAL_LEDGER, ML.IS_SUBSIDY_LEDGER,\n" +
                              "                ML.IS_GST_LEDGERS, ML.GST_SERVICE_TYPE,\n" +
                              "                ML.BUDGET_GROUP_ID, ML.BUDGET_SUB_GROUP_ID,MC.COUNTRY, MC.COUNTRY_ID,ML.OP_EXCHANGE_RATE\n" +
                              "           FROM LEDGER_BALANCE LB\n" +
                              "           LEFT JOIN MASTER_LEDGER ML\n" +
                              "             ON ML.LEDGER_ID = LB.LEDGER_ID\n" +
                              "           LEFT JOIN MASTER_LEDGER_GROUP MLG\n" +
                              "             ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                              "           LEFT JOIN HEADOFFICE_MAPPED_LEDGER HML\n" +
                              "             ON HML.LEDGER_ID = LB.LEDGER_ID\n" +
                              "           LEFT JOIN MASTER_HEADOFFICE_LEDGER MHL\n" +
                              "             ON MHL.HEADOFFICE_LEDGER_ID = HML.HEADOFFICE_LEDGER_ID\n" +
                              "           LEFT JOIN MASTER_LEDGER_GROUP HLG\n" +
                              "             ON HLG.GROUP_ID = MHL.GROUP_ID\n" +
                              "            LEFT JOIN MASTER_COUNTRY MC\n" +
                              "              ON MC.COUNTRY_ID = ML.CUR_COUNTRY_ID\n" +
                              "          WHERE PROJECT_ID IN (?PROJECT_ID)\n" +
                              "            AND LB.BALANCE_DATE BETWEEN ?DATE_FROM AND ?DATE_TO) UNION\n" +
                              "        (SELECT ML.LEDGER_CODE,\n" +
                              "                IF(MHL.LEDGER_NAME IS NOT NULL,\n" +
                              "                   MHL.LEDGER_NAME,\n" +
                              "                   ML.LEDGER_NAME) AS LEDGER_NAME,\n" +
                              "                IF(MHL.LEDGER_NAME IS NOT NULL,\n" +
                              "                   HLG.LEDGER_GROUP,\n" +
                              "                   MLG.LEDGER_GROUP) AS LEDGER_GROUP,\n" +
                              "                IF(MHL.LEDGER_NAME IS NOT NULL, 0, 1) AS IS_BRANCH_LEDGER,\n" +
                              "                ML.GROUP_ID,\n" +
                              "                MLG.NATURE_ID,\n" +
                              "                ML.LEDGER_TYPE,\n" +
                              "                ML.LEDGER_SUB_TYPE,\n" +
                              "                ML.SORT_ID,\n" +
                              "                ML.IS_COST_CENTER, ML.IS_TDS_LEDGER, ML.IS_BANK_INTEREST_LEDGER,\n" +
                              "                ML.IS_INKIND_LEDGER, ML.IS_DEPRECIATION_LEDGER, ML.IS_ASSET_GAIN_LEDGER, ML.IS_ASSET_LOSS_LEDGER,\n" +
                              "                ML.IS_DISPOSAL_LEDGER, ML.IS_SUBSIDY_LEDGER,\n" +
                              "                ML.IS_GST_LEDGERS, ML.GST_SERVICE_TYPE,\n" +
                              "                ML.BUDGET_GROUP_ID, ML.BUDGET_SUB_GROUP_ID,MC.COUNTRY, MC.COUNTRY_ID,ML.OP_EXCHANGE_RATE\n" +
                              "           FROM LEDGER_BALANCE LB\n" +
                              "           LEFT JOIN MASTER_LEDGER ML\n" +
                              "             ON ML.LEDGER_ID = LB.LEDGER_ID\n" +
                              "           LEFT JOIN MASTER_LEDGER_GROUP MLG\n" +
                              "             ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                              "           LEFT JOIN HEADOFFICE_MAPPED_LEDGER HML\n" +
                              "             ON HML.LEDGER_ID = LB.LEDGER_ID\n" +
                              "           LEFT JOIN MASTER_HEADOFFICE_LEDGER MHL\n" +
                              "             ON MHL.HEADOFFICE_LEDGER_ID = HML.HEADOFFICE_LEDGER_ID\n" +
                              "           LEFT JOIN MASTER_LEDGER_GROUP HLG\n" +
                              "             ON HLG.GROUP_ID = MHL.GROUP_ID\n" +
                               "            LEFT JOIN MASTER_COUNTRY MC\n" +
                              "              ON MC.COUNTRY_ID = ML.CUR_COUNTRY_ID\n" +
                              "          WHERE PROJECT_ID IN (?PROJECT_ID)\n" +
                              "            AND LB.TRANS_FLAG = 'OP') UNION\n" +
                              "        (SELECT ML.LEDGER_CODE,\n" +
                              "                IF(MHL.LEDGER_NAME IS NOT NULL,\n" +
                              "                   MHL.LEDGER_NAME,\n" +
                              "                   ML.LEDGER_NAME) AS LEDGER_NAME,\n" +
                              "                IF(MHL.LEDGER_NAME IS NOT NULL,\n" +
                              "                   HLG.LEDGER_GROUP,\n" +
                              "                   MLG.LEDGER_GROUP) AS LEDGER_GROUP,\n" +
                              "                IF(MHL.LEDGER_NAME IS NOT NULL, 0, 1) AS IS_BRANCH_LEDGER,\n" +
                              "                ML.GROUP_ID,\n" +
                              "                MLG.NATURE_ID,\n" +
                              "                ML.LEDGER_TYPE,\n" +
                              "                ML.LEDGER_SUB_TYPE,\n" +
                              "                ML.SORT_ID,\n" +
                              "                ML.IS_COST_CENTER, ML.IS_TDS_LEDGER, ML.IS_BANK_INTEREST_LEDGER,\n" +
                              "                ML.IS_INKIND_LEDGER, ML.IS_DEPRECIATION_LEDGER, ML.IS_ASSET_GAIN_LEDGER, ML.IS_ASSET_LOSS_LEDGER,\n" +
                              "                ML.IS_DISPOSAL_LEDGER, ML.IS_SUBSIDY_LEDGER,\n" +
                              "                ML.IS_GST_LEDGERS, ML.GST_SERVICE_TYPE,\n" +
                              "                ML.BUDGET_GROUP_ID, ML.BUDGET_SUB_GROUP_ID,MC.COUNTRY, MC.COUNTRY_ID,ML.OP_EXCHANGE_RATE\n" +
                              "           FROM LEDGER_BALANCE LB\n" +
                              "           LEFT JOIN MASTER_LEDGER ML\n" +
                              "             ON ML.LEDGER_ID = LB.LEDGER_ID\n" +
                              "           LEFT JOIN MASTER_LEDGER_GROUP MLG\n" +
                              "             ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                              "           LEFT JOIN HEADOFFICE_MAPPED_LEDGER HML\n" +
                              "             ON HML.LEDGER_ID = LB.LEDGER_ID\n" +
                              "           LEFT JOIN MASTER_HEADOFFICE_LEDGER MHL\n" +
                              "             ON MHL.HEADOFFICE_LEDGER_ID = HML.HEADOFFICE_LEDGER_ID\n" +
                              "           LEFT JOIN MASTER_LEDGER_GROUP HLG\n" +
                              "             ON HLG.GROUP_ID = MHL.GROUP_ID\n" +
                               "            LEFT JOIN MASTER_COUNTRY MC\n" +
                              "              ON MC.COUNTRY_ID = ML.CUR_COUNTRY_ID\n" +
                              "          WHERE PROJECT_ID IN (?PROJECT_ID)\n" +
                              "            AND ML.LEDGER_SUB_TYPE = 'FD'\n" +
                              "             OR ML.IS_BANK_INTEREST_LEDGER = 1\n" +
                              ")) AS T\n" +
                              " WHERE T.GROUP_ID NOT IN (12)\n" +
                              " GROUP BY LEDGER_NAME;";

                        //query = "SELECT ML.LEDGER_CODE,\n" +
                        //        "       IF(MHL.LEDGER_NAME IS NOT NULL, MHL.LEDGER_NAME, ML.LEDGER_NAME) AS LEDGER_NAME,\n" +
                        //        "       IF(MHL.LEDGER_NAME IS NOT NULL, 0, 1) AS IS_BRANCH_LEDGER, \n" +
                        //        "       ML.GROUP_ID,\n" +
                        //        "       MLG.LEDGER_GROUP,\n" +
                        //        "       ML.LEDGER_TYPE,\n" +
                        //        "       ML.LEDGER_SUB_TYPE,\n" +
                        //        "       ML.SORT_ID\n" +
                        //        "  FROM LEDGER_BALANCE LB\n" +
                        //        "  LEFT JOIN MASTER_LEDGER ML\n" +
                        //        "    ON ML.LEDGER_ID = LB.LEDGER_ID\n" +
                        //        "  LEFT JOIN MASTER_LEDGER_GROUP MLG\n" +
                        //        "    ON MLG.GROUP_ID=ML.GROUP_ID\n" +
                        //        "  LEFT JOIN HEADOFFICE_MAPPED_LEDGER HML\n" +
                        //        "    ON HML.LEDGER_ID = LB.LEDGER_ID\n" +
                        //        "  LEFT JOIN MASTER_HEADOFFICE_LEDGER MHL\n" +
                        //        "    ON MHL.HEADOFFICE_LEDGER_ID = HML.HEADOFFICE_LEDGER_ID\n" +
                        //        " WHERE PROJECT_ID IN (?PROJECT_ID)\n" +
                        //    //  "   AND AMOUNT > 0\n" +
                        //        " GROUP BY LEDGER_NAME";

                        ////Fetching Ledgers based on the Ledger Opening Balance ( Modified By Salamon)
                        //query = "SELECT T.LEDGER_CODE,\n" +
                        //          "       T.LEDGER_NAME,\n" +
                        //          "       T.GROUP_ID,\n" +
                        //          "       T.LEDGER_TYPE,\n" +
                        //          "       T.LEDGER_SUB_TYPE,\n" +
                        //          "       T.SORT_ID\n" +
                        //          "  FROM (SELECT MHL.LEDGER_CODE,\n" +
                        //          "               MHL.LEDGER_NAME,\n" +
                        //          "               ML.GROUP_ID,\n" +
                        //          "               ML.LEDGER_TYPE,\n" +
                        //          "               ML.LEDGER_SUB_TYPE,\n" +
                        //          "               ML.SORT_ID\n" +
                        //          "          FROM LEDGER_BALANCE LB\n" +
                        //          "         INNER JOIN HEADOFFICE_MAPPED_LEDGER HML\n" +
                        //          "            ON HML.LEDGER_ID = LB.LEDGER_ID\n" +
                        //          "         INNER JOIN MASTER_HEADOFFICE_LEDGER MHL\n" +
                        //          "            ON MHL.HEADOFFICE_LEDGER_ID = HML.HEADOFFICE_LEDGER_ID\n" +
                        //          "         INNER JOIN MASTER_LEDGER ML\n" +
                        //          "            ON ML.LEDGER_ID = HML.LEDGER_ID\n" +
                        //          "         WHERE ML.GROUP_ID NOT IN (12, 13, 14)\n" +
                        //          "           AND PROJECT_ID IN (?PROJECT_ID)\n" +
                        //          "           AND AMOUNT > 0\n" +
                        //          "         GROUP BY MHL.HEADOFFICE_LEDGER_ID\n" +
                        //          "        UNION\n" +
                        //          "        SELECT ML.LEDGER_CODE,\n" +
                        //          "               ML.LEDGER_NAME,\n" +
                        //          "               ML.GROUP_ID,\n" +
                        //          "               ML.LEDGER_TYPE,\n" +
                        //          "               ML.LEDGER_SUB_TYPE,\n" +
                        //          "               ML.SORT_ID\n" +
                        //          "          FROM LEDGER_BALANCE LB\n" +
                        //          "         INNER JOIN MASTER_LEDGER ML\n" +
                        //          "            ON ML.LEDGER_ID = LB.LEDGER_ID\n" +
                        //          "         WHERE ML.GROUP_ID IN (13, 14)\n" +
                        //          "           AND PROJECT_ID IN (?PROJECT_ID)\n" +
                        //          "           AND lb.AMOUNT > 0\n" +
                        //          "         GROUP BY ML.LEDGER_ID) AS T\n" +
                        //          " GROUP BY T.LEDGER_NAME";

                        break;
                    }
                case SQLCommand.ExportVouchers.FetchLedgerGroup:
                    {
                        query = "SELECT lg.GROUP_CODE,\n" +
                                "       lg.LEDGER_GROUP,lg.PARENT_GROUP_ID,lg.NATURE_ID,lg.MAIN_GROUP_ID,\n" +
                                "       t.ledger_group  as ParentGroup,\n" +
                                "       mn.NATURE,\n" +
                                "       t1.ledger_group as MainGroup,\n" +
                                "       lg.IMAGE_ID,\n" +
                                "       lg.ACCESS_FLAG,\n" +
                                "       lg.SORT_ORDER\n" +
                                "  FROM master_ledger_group lg\n" +
                                "  LEFT JOIN MASTER_NATURE mn\n" +
                                "    on (mn.nature_id = lg.nature_id)\n" +
                                " INNER JOIN (SELECT GROUP_ID, LEDGER_GROUP\n" +
                                "               FROM MASTER_LEDGER_GROUP\n" +
                                "              WHERE PARENT_GROUP_ID) as t\n" +
                                "    ON lg.parent_group_id = t.group_id\n" +
                                " INNER JOIN (SELECT GROUP_ID, LEDGER_GROUP\n" +
                                "               FROM MASTER_LEDGER_GROUP\n" +
                                "              WHERE PARENT_GROUP_ID) as t1\n" +
                                "    ON lg.main_group_id = t1.group_id ORDER BY LG.GROUP_ID";
                        break;
                        //query = "SELECT  GROUP_CODE, LEDGER_GROUP, PARENT_GROUP_ID, NATURE_ID, MAIN_GROUP_ID FROM MASTER_LEDGER_GROUP;";
                        //break;
                    }
                case SQLCommand.ExportVouchers.FetchHOBranchFDAccounts:
                    {
                        query = "SELECT FDA.FD_ACCOUNT_ID,\n" +
                                "       FDA.FD_ACCOUNT_NUMBER,\n" +
                                "       FDA.FD_SCHEME,\n" +
                                "       MP.PROJECT,\n" +
                                "       ML.LEDGER_NAME,\n" +
                                "       MLA.LEDGER_NAME AS BANK_LEDGER,\n" +
                                "       MB.BANK,\n" +
                                "       FDA.FD_VOUCHER_ID,\n" +
                                "       FDA.AMOUNT,\n" +
                                "       FDA.TRANS_MODE,\n" +
                                "       FDA.TRANS_TYPE,\n" +
                                "       FDA.RECEIPT_NO,\n" +
                                "       FDA.ACCOUNT_HOLDER,\n" +
                                "       FDA.INVESTMENT_DATE,\n" +
                                "       FDA.MATURED_ON,\n" +
                                "       FDA.INTEREST_RATE,\n" +
                                "       FDA.INTEREST_AMOUNT,\n" +
                                "       FDA.INTEREST_TYPE,\n" +
                                "       IFNULL(FDA.MF_FOLIO_NO,'') AS MF_FOLIO_NO, IFNULL(FDA.MF_SCHEME_NAME, '')  AS MF_SCHEME_NAME, IFNULL(FDA.MF_NAV_PER_UNIT,0) AS MF_NAV_PER_UNIT,\n" +
                                "       IFNULL(FDA.MF_NO_OF_UNITS,0) AS MF_NO_OF_UNITS, IFNULL(FDA.MF_MODE_OF_HOLDING, 0) AS MF_MODE_OF_HOLDING,\n" +
                                "       FDA.STATUS,\n" +
                                "       FDA.FD_STATUS,\n" +
                                "       FDA.FD_SUB_TYPES,\n" +
                                "       FDA.NOTES\n" +
                                "  FROM FD_ACCOUNT FDA\n" +
                                "  LEFT JOIN MASTER_PROJECT MP\n" +
                                "    ON MP.PROJECT_ID = FDA.PROJECT_ID\n" +
                                "  LEFT JOIN MASTER_LEDGER MLA\n" +
                                "    ON FDA.BANK_LEDGER_ID = MLA.LEDGER_ID\n" +
                                "  LEFT JOIN MASTER_LEDGER ML\n" +
                                "    ON ML.LEDGER_ID = FDA.LEDGER_ID\n" +
                                "  LEFT JOIN MASTER_BANK MB\n" +
                                "    ON MB.BANK_ID = FDA.BANK_ID\n" +
                                " WHERE FDA.BRANCH_ID=?BRANCH_OFFICE_ID AND FDA.PROJECT_ID IN (?PROJECT_ID)\n" +
                                "   AND FDA.STATUS = 1;";
                        break;
                    }
                case SQLCommand.ExportVouchers.FDAccounts:
                    {
                        query = "SELECT FDA.FD_ACCOUNT_ID,\n" +
                                "       FDA.FD_ACCOUNT_NUMBER,\n" +
                                "       FDA.FD_SCHEME,\n" +
                                "       MP.PROJECT,\n" +
                                "       IF(MHL.LEDGER_NAME IS NOT NULL, MHL.LEDGER_NAME, ML.LEDGER_NAME) AS LEDGER_NAME,\n" +
                                "       MLA.LEDGER_NAME AS BANK_LEDGER,\n" +
                                "       MB.BANK,\n" +
                                "       FDA.FD_VOUCHER_ID,\n" +
                                "       FDA.AMOUNT,\n" +
                                "       FDA.TRANS_MODE,\n" +
                                "       FDA.TRANS_TYPE,\n" +
                                "       FDA.RECEIPT_NO,\n" +
                                "       FDA.ACCOUNT_HOLDER,\n" +
                                "       FDA.INVESTMENT_DATE,\n" +
                                "       FDA.MATURED_ON,\n" +
                                "       FDA.INTEREST_RATE,\n" +
                                "       FDA.INTEREST_AMOUNT,\n" +
                                "       FDA.INTEREST_TYPE,\n" +
                                "       FDA.STATUS,\n" +
                                "       FDA.FD_STATUS,\n" +
                                "       FDA.FD_SUB_TYPES,\n" +
                                "       FDA.NOTES,\n" +
                                "        FDA.IS_MULTI_CURRENCY,\n" +
                                "        FDA.CURRENCY_COUNTRY_ID,\n" +
                                "        FDA.CONTRIBUTION_AMOUNT,\n" +
                                "        FDA.EXCHANGE_RATE,\n" +
                                "        FDA.ACTUAL_AMOUNT\n" +
                                "     FROM FD_ACCOUNT FDA\n" +
                                "  LEFT JOIN MASTER_PROJECT MP\n" +
                                "    ON MP.PROJECT_ID = FDA.PROJECT_ID\n" +
                                "  LEFT JOIN MASTER_LEDGER MLA\n" +
                                "    ON FDA.BANK_LEDGER_ID = MLA.LEDGER_ID\n" +
                                "  LEFT JOIN MASTER_LEDGER ML\n" +
                                "    ON ML.LEDGER_ID = FDA.LEDGER_ID\n" +
                                "  LEFT JOIN HEADOFFICE_MAPPED_LEDGER HML\n" +
                                "    ON HML.LEDGER_ID = FDA.LEDGER_ID\n" +
                                "  LEFT JOIN MASTER_HEADOFFICE_LEDGER MHL\n" +
                                "    ON MHL.HEADOFFICE_LEDGER_ID = HML.HEADOFFICE_LEDGER_ID\n" +
                                "  LEFT JOIN MASTER_BANK MB\n" +
                                "    ON MB.BANK_ID = FDA.BANK_ID\n" +
                                " WHERE FDA.PROJECT_ID IN (?PROJECT_ID)\n" +
                                "   AND FDA.STATUS = 1;";

                        //query = "SELECT T.FD_ACCOUNT_ID,\n" +
                        //        "       T.FD_ACCOUNT_NUMBER,\n" +
                        //        "       MP.PROJECT,\n" +
                        //        "       ML.LEDGER_NAME,\n" +
                        //        "       MLA.LEDGER_NAME AS BANK_LEDGER,\n" +
                        //        "       MB.BANK,\n" +
                        //        "       T.FD_VOUCHER_ID,\n" +
                        //        "       T.AMOUNT,\n" +
                        //        "       T.TRANS_MODE,\n" +
                        //        "       T.TRANS_TYPE,\n" +
                        //        "       T.RECEIPT_NO,\n" +
                        //        "       T.ACCOUNT_HOLDER,\n" +
                        //        "       T.INVESTMENT_DATE,\n" +
                        //        "       T.MATURED_ON,\n" +
                        //        "       T.INTEREST_RATE,\n" +
                        //        "       T.INTEREST_AMOUNT,\n" +
                        //        "       T.INTEREST_TYPE,\n" +
                        //        "       T.STATUS,\n" +
                        //        "       T.FD_STATUS,\n" +
                        //        "       T.FD_SUB_TYPES,\n" +
                        //        "       T.NOTES\n" +
                        //        "  FROM FD_ACCOUNT FDA\n" +
                        //        "  LEFT JOIN MASTER_PROJECT MP\n" +
                        //        "    ON MP.PROJECT_ID = FDA.PROJECT_ID\n" +
                        //        "  LEFT JOIN MASTER_LEDGER MLA\n" +
                        //        "    ON FDA.BANK_LEDGER_ID = MLA.LEDGER_ID\n" +
                        //        "  LEFT JOIN MASTER_LEDGER ML\n" +
                        //        "    ON ML.LEDGER_ID = FDA.LEDGER_ID\n" +
                        //        "  LEFT JOIN MASTER_BANK MB\n" +
                        //        "    ON MB.BANK_ID = FDA.BANK_ID\n" +
                        //        "\n" +
                        //        "  JOIN((SELECT FD_ACCOUNT_ID,\n" +
                        //        "               FD_ACCOUNT_NUMBER,\n" +
                        //        "               PROJECT_ID,\n" +
                        //        "               LEDGER_ID,\n" +
                        //        "               BANK_LEDGER_ID,\n" +
                        //        "               BANK_ID,\n" +
                        //        "               FD_VOUCHER_ID,\n" +
                        //        "               AMOUNT,\n" +
                        //        "               TRANS_MODE,\n" +
                        //        "               TRANS_TYPE,\n" +
                        //        "               RECEIPT_NO,\n" +
                        //        "               ACCOUNT_HOLDER,\n" +
                        //        "               INVESTMENT_DATE,\n" +
                        //        "               MATURED_ON,\n" +
                        //        "               INTEREST_RATE,\n" +
                        //        "               INTEREST_AMOUNT,\n" +
                        //        "               INTEREST_TYPE,\n" +
                        //        "               STATUS,\n" +
                        //        "               FD_STATUS,\n" +
                        //        "               FD_SUB_TYPES,\n" +
                        //        "               NOTES\n" +
                        //        "          FROM FD_ACCOUNT FDA\n" +
                        //        "         WHERE FD_VOUCHER_ID IN\n" +
                        //        "               (SELECT VOUCHER_ID\n" +
                        //        "                  FROM VOUCHER_MASTER_TRANS VMT\n" +
                        //        "                 WHERE VOUCHER_SUB_TYPE = 'FD'\n" +
                        //        "                   AND VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO AND VMT.PROJECT_ID IN(?PROJECT_ID))\n" +
                        //        "           AND FDA.INVESTMENT_DATE <= ?DATE_TO)\n" +
                        //        "UNION (SELECT FD_ACCOUNT_ID,\n" +
                        //        "              FD_ACCOUNT_NUMBER,\n" +
                        //        "              PROJECT_ID,\n" +
                        //        "              LEDGER_ID,\n" +
                        //        "              BANK_LEDGER_ID,\n" +
                        //        "              BANK_ID,\n" +
                        //        "              FD_VOUCHER_ID,\n" +
                        //        "              AMOUNT,\n" +
                        //        "              TRANS_MODE,\n" +
                        //        "              TRANS_TYPE,\n" +
                        //        "              RECEIPT_NO,\n" +
                        //        "              ACCOUNT_HOLDER,\n" +
                        //        "              INVESTMENT_DATE,\n" +
                        //        "              MATURED_ON,\n" +
                        //        "              INTEREST_RATE,\n" +
                        //        "              INTEREST_AMOUNT,\n" +
                        //        "              INTEREST_TYPE,\n" +
                        //        "              STATUS,\n" +
                        //        "              FD_STATUS,\n" +
                        //        "              FD_SUB_TYPES,\n" +
                        //        "              NOTES\n" +
                        //        "         FROM FD_ACCOUNT FDA\n" +
                        //        "        WHERE FD_ACCOUNT_ID IN\n" +
                        //        "              (SELECT FDR.FD_ACCOUNT_ID\n" +
                        //        "                 FROM FD_RENEWAL FDR\n" +
                        //        "                WHERE FDR.FD_VOUCHER_ID IN\n" +
                        //        "                      (SELECT VOUCHER_ID\n" +
                        //        "                         FROM VOUCHER_MASTER_TRANS VMT\n" +
                        //        "                        WHERE VOUCHER_SUB_TYPE = 'FD'\n" +
                        //        "                          AND VOUCHER_DATE BETWEEN ?DATE_FROM AND \n" +
                        //        "                      ?DATE_TO AND VMT.PROJECT_ID IN(?PROJECT_ID)))\n" +
                        //        "          AND FDA.INVESTMENT_DATE <= ?DATE_TO)) AS T\n" +
                        //        "    ON FDA.FD_ACCOUNT_ID = T.FD_ACCOUNT_ID;";
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchHOBranchFDRenewals:
                    {
                        query = "SELECT FDR.FD_ACCOUNT_ID,\n" +
                                "       FD_RENEWAL_ID,\n" +
                                "       RENEWAL_DATE,\n" +
                                "       MATURITY_DATE, CLOSED_DATE,\n" +
                                "       IL.LEDGER_NAME AS INTEREST_LEDGER,\n" +
                                "       IFNULL(ML.LEDGER_NAME, '') AS BANK_LEDGER,\n" +
                                "       FD_INTEREST_VOUCHER_ID,\n" +
                                "       FDR.FD_VOUCHER_ID,\n" +
                                "       FDR.REINVESTED_AMOUNT,\n" +
                                "       FDR.INTEREST_AMOUNT,\n" +
                                "       WITHDRAWAL_AMOUNT,\n" +
                                "       FDR.INTEREST_RATE,\n" +
                                "       FDR.INTEREST_TYPE,\n" +
                                "       FDR.RECEIPT_NO,\n" +
                                "       RENEWAL_TYPE,\n" +
                                "       FDR.STATUS,\n" +
                                "       FDR.FD_TYPE, IFNULL(TDS_AMOUNT, 0) AS TDS_AMOUNT,\n" +
                                "       IFNULL(FDR.CHARGE_MODE, 0) AS CHARGE_MODE, IFNULL(FDR.CHARGE_AMOUNT, 0) AS CHARGE_AMOUNT,\n" +
                                "       IFNULL(FDR.CHARGE_LEDGER_ID, 0) AS CHARGE_LEDGER_ID, IFNULL(FDR.FD_TRANS_MODE, 'DR')  AS FD_TRANS_MODE\n" +
                                "  FROM FD_RENEWAL FDR\n" +
                                "  LEFT JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = FDR.BANK_LEDGER_ID\n" +
                                "  LEFT JOIN MASTER_LEDGER IL ON IL.LEDGER_ID = FDR.INTEREST_LEDGER_ID\n" +
                                " WHERE FD_ACCOUNT_ID IN (SELECT FDA.FD_ACCOUNT_ID\n" +
                                "                           FROM FD_ACCOUNT FDA\n" +
                                "                          WHERE FDA.BRANCH_ID=?BRANCH_OFFICE_ID AND FDA.PROJECT_ID IN (?PROJECT_ID)\n" +
                                "                            AND FDA.STATUS = 1);";
                        break;
                    }
                case SQLCommand.ExportVouchers.FDRenewals:
                    {
                        // Modified to fetch Head Office Ledgers if BO ledger mapped with HO Ledger, Otherwise BO Ledger
                        query = "SELECT FDR.FD_ACCOUNT_ID,\n" +
                                "       FD_RENEWAL_ID,\n" +
                                "       RENEWAL_DATE,\n" +
                                "       MATURITY_DATE,\n" +
                                "       IF(MHL.LEDGER_NAME IS NOT NULL, MHL.LEDGER_NAME, IL.LEDGER_NAME) AS INTEREST_LEDGER,\n" +
                                "       IFNULL(ML.LEDGER_NAME, '') AS BANK_LEDGER,\n" +
                                "       FD_INTEREST_VOUCHER_ID,\n" +
                                "       FDR.FD_VOUCHER_ID,\n" +
                                "       FDR.INTEREST_AMOUNT,\n" +
                                "       WITHDRAWAL_AMOUNT,\n" +
                                "       FDR.REINVESTED_AMOUNT,\n" +
                                "       FDR.INTEREST_RATE,\n" +
                                "       FDR.INTEREST_TYPE,\n" +
                                "       FDR.RECEIPT_NO,\n" +
                                "       RENEWAL_TYPE,\n" +
                                "       FDR.STATUS,\n" +
                                "       FDR.FD_TYPE, IFNULL(TDS_AMOUNT, 0) AS TDS_AMOUNT,\n" +
                                "       IFNULL(FDR.CHARGE_MODE, 0) AS CHARGE_MODE, IFNULL(FDR.CHARGE_AMOUNT, 0) AS CHARGE_AMOUNT, \n" +
                                "       IFNULL(FDR.CHARGE_LEDGER_ID, 0) AS CHARGE_LEDGER_ID, IFNULL(FDR.FD_TRANS_MODE, 'DR')  AS FD_TRANS_MODE\n" +
                                "  FROM FD_RENEWAL FDR\n" +
                                "  LEFT JOIN MASTER_LEDGER ML\n" +
                                "    ON ML.LEDGER_ID = FDR.BANK_LEDGER_ID\n" +
                                "  LEFT JOIN MASTER_LEDGER IL\n" +
                                "    ON IL.LEDGER_ID = FDR.INTEREST_LEDGER_ID\n" +
                                "  LEFT JOIN HEADOFFICE_MAPPED_LEDGER HML\n" +
                                "    ON HML.LEDGER_ID = FDR.INTEREST_LEDGER_ID\n" +
                                "  LEFT JOIN MASTER_HEADOFFICE_LEDGER MHL\n" +
                                "    ON MHL.HEADOFFICE_LEDGER_ID = HML.HEADOFFICE_LEDGER_ID\n" +
                                " WHERE FD_ACCOUNT_ID IN (SELECT FDA.FD_ACCOUNT_ID\n" +
                                "                           FROM FD_ACCOUNT FDA\n" +
                                "                          WHERE FDA.PROJECT_ID IN (?PROJECT_ID)\n" +
                                "                            AND FDA.STATUS = 1);";

                        //query = "SELECT FDR.FD_ACCOUNT_ID,\n" +
                        //          "       FD_RENEWAL_ID,\n" +
                        //          "       RENEWAL_DATE,\n" +
                        //          "       MATURITY_DATE,\n" +
                        //          "       IF(MHL.LEDGER_NAME IS NOT NULL, MHL.LEDGER_NAME, IL.LEDGER_NAME) AS INTEREST_LEDGER,\n" +
                        //          "       IFNULL(ML.LEDGER_NAME, '') AS BANK_LEDGER,\n" +
                        //          "       FD_INTEREST_VOUCHER_ID,\n" +
                        //          "       FDR.FD_VOUCHER_ID,\n" +
                        //          "       FDR.INTEREST_AMOUNT,\n" +
                        //          "       WITHDRAWAL_AMOUNT,\n" +
                        //          "       FDR.INTEREST_RATE,\n" +
                        //          "       FDR.INTEREST_TYPE,\n" +
                        //          "       FDR.RECEIPT_NO,\n" +
                        //          "       RENEWAL_TYPE,\n" +
                        //          "       FDR.STATUS\n" +
                        //          "  FROM FD_RENEWAL FDR\n" +
                        //          "  LEFT JOIN MASTER_LEDGER ML\n" +
                        //          "    ON ML.LEDGER_ID = FDR.BANK_LEDGER_ID\n" +
                        //          "  LEFT JOIN MASTER_LEDGER IL\n" +
                        //          "    ON IL.LEDGER_ID = FDR.INTEREST_LEDGER_ID\n" +
                        //          "  LEFT JOIN HEADOFFICE_MAPPED_LEDGER HML\n" +
                        //          "    ON HML.LEDGER_ID = FDR.INTEREST_LEDGER_ID\n" +
                        //          "  LEFT JOIN MASTER_HEADOFFICE_LEDGER MHL\n" +
                        //          "    ON MHL.HEADOFFICE_LEDGER_ID = HML.HEADOFFICE_LEDGER_ID\n" +
                        //          " WHERE FD_ACCOUNT_ID IN\n" +
                        //          "       (SELECT FDR.FD_ACCOUNT_ID\n" +
                        //          "          FROM FD_RENEWAL FDR\n" +
                        //          "         WHERE FDR.FD_VOUCHER_ID IN\n" +
                        //          "               (SELECT VOUCHER_ID\n" +
                        //          "                  FROM VOUCHER_MASTER_TRANS VMT\n" +
                        //          "                 WHERE VOUCHER_SUB_TYPE = 'FD'\n" +
                        //          "                   AND VOUCHER_DATE BETWEEN ?DATE_FROM AND\n" +
                        //          "                       ?DATE_TO\n" +
                        //          "                   AND PROJECT_ID IN (?PROJECT_ID)))\n" +
                        //          "   AND FDR.RENEWAL_DATE <= ?DATE_TO;";
                        break;
                    }
                case SQLCommand.ExportVouchers.FDVoucherMasterTrans:
                    {
                        query = "SELECT VOUCHER_ID,\n" +
                                "       VMT.VOUCHER_DATE,\n" +
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
                                "         WHERE FDA.BRANCH_ID=?BRANCH_OFFICE_ID AND FDA.PROJECT_ID IN (?PROJECT_ID)\n" +
                                "           AND FDA.STATUS = 1)\n" +
                            //"UNION (SELECT FD_VOUCHER_ID, '' AS FD_INTEREST_VOUCHER_ID\n" +
                            //"         FROM FD_RENEWAL FDR\n" +
                            //"        WHERE FD_ACCOUNT_ID IN (SELECT FDA.FD_ACCOUNT_ID\n" +
                            //"                                  FROM FD_ACCOUNT FDA\n" +
                            //"                                 WHERE FDA.PROJECT_ID IN (?PROJECT_ID)\n" +
                            //"                                   AND FDA.STATUS = 1))\n" +
                                "UNION (SELECT FDR.FD_VOUCHER_ID, FD_INTEREST_VOUCHER_ID\n" +
                                "         FROM FD_RENEWAL FDR\n" +
                                "        WHERE FD_ACCOUNT_ID IN (SELECT FDA.FD_ACCOUNT_ID\n" +
                                "                                  FROM FD_ACCOUNT FDA\n" +
                                "                                 WHERE FDA.BRANCH_ID=?BRANCH_OFFICE_ID AND FDA.PROJECT_ID IN (?PROJECT_ID)\n" +
                                "                                   AND FDA.STATUS = 1))) AS T\n" +
                                "    ON VMT.VOUCHER_ID = T.FD_VOUCHER_ID\n" +
                                "    OR VMT.VOUCHER_ID = T.FD_INTEREST_VOUCHER_ID WHERE BRANCH_ID=?BRANCH_OFFICE_ID AND VMT.VOUCHER_DATE <> '0001-01-01 00:00:00';";

                        //query = "SELECT VOUCHER_ID,\n" +
                        //          "       VMT.VOUCHER_DATE,\n" +
                        //          "       MP.PROJECT,\n" +
                        //          "       VOUCHER_NO,\n" +
                        //          "       VOUCHER_TYPE,\n" +
                        //          "       VOUCHER_SUB_TYPE,\n" +
                        //          "       DONOR_ID,\n" +
                        //          "       PURPOSE_ID,\n" +
                        //          "       CONTRIBUTION_TYPE,\n" +
                        //          "       CONTRIBUTION_AMOUNT,\n" +
                        //          "       CURRENCY_COUNTRY_ID,\n" +
                        //          "       EXCHANGE_RATE,\n" +
                        //          "       EXCHANGE_COUNTRY_ID,\n" +
                        //          "       NARRATION,\n" +
                        //          "       VMT.STATUS,\n" +
                        //          "       CREATED_BY,\n" +
                        //          "       MODIFIED_BY,\n" +
                        //          "       CALCULATED_AMOUNT,\n" +
                        //          "       ACTUAL_AMOUNT,\n" +
                        //          "       NAME_ADDRESS\n" +
                        //          "  FROM VOUCHER_MASTER_TRANS VMT\n" +
                        //          "  LEFT JOIN MASTER_PROJECT MP\n" +
                        //          "    ON VMT.PROJECT_ID = MP.PROJECT_ID\n" +
                        //          "  JOIN((SELECT FD_VOUCHER_ID, '' AS FD_INTEREST_VOUCHER_ID\n" +
                        //          "          FROM FD_ACCOUNT FDA\n" +
                        //          "         WHERE FD_VOUCHER_ID IN\n" +
                        //          "               (SELECT VOUCHER_ID\n" +
                        //          "                  FROM VOUCHER_MASTER_TRANS VMT\n" +
                        //          "                 WHERE VOUCHER_SUB_TYPE = 'FD'\n" +
                        //          "                   AND VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO AND PROJECT_ID IN(?PROJECT_ID))\n" +
                        //          "           AND FDA.INVESTMENT_DATE <= ?DATE_TO)\n" +
                        //          "UNION (SELECT FD_VOUCHER_ID, '' AS FD_INTEREST_VOUCHER_ID\n" +
                        //          "         FROM FD_ACCOUNT FDA\n" +
                        //          "        WHERE FD_ACCOUNT_ID IN\n" +
                        //          "              (SELECT FDR.FD_ACCOUNT_ID\n" +
                        //          "                 FROM FD_RENEWAL FDR\n" +
                        //          "                WHERE FDR.FD_VOUCHER_ID IN\n" +
                        //          "                      (SELECT VOUCHER_ID\n" +
                        //          "                         FROM VOUCHER_MASTER_TRANS VMT\n" +
                        //          "                        WHERE VOUCHER_SUB_TYPE = 'FD'\n" +
                        //          "                          AND VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO AND PROJECT_ID IN(?PROJECT_ID))\n" +
                        //          "                  AND FDA.INVESTMENT_DATE <= ?DATE_TO))\n" +
                        //          "UNION (SELECT FDR.FD_VOUCHER_ID, FD_INTEREST_VOUCHER_ID\n" +
                        //          "         FROM FD_RENEWAL FDR\n" +
                        //          "        WHERE FD_ACCOUNT_ID IN\n" +
                        //          "              (SELECT FDR.FD_ACCOUNT_ID\n" +
                        //          "                 FROM FD_RENEWAL FDR\n" +
                        //          "                WHERE FDR.FD_VOUCHER_ID IN\n" +
                        //          "                      (SELECT VOUCHER_ID\n" +
                        //          "                         FROM VOUCHER_MASTER_TRANS VMT\n" +
                        //          "                        WHERE VOUCHER_SUB_TYPE = 'FD'\n" +
                        //          "                          AND VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO AND PROJECT_ID IN(?PROJECT_ID)))\n" +
                        //          "          AND FDR.RENEWAL_DATE <= ?DATE_TO)) AS T\n" +
                        //          "    ON VMT.VOUCHER_ID = T.FD_VOUCHER_ID\n" +
                        //          "    OR VMT.VOUCHER_ID = T.FD_INTEREST_VOUCHER_ID ";
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchHOBranchFDVoucherTrans:
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
                                "         WHERE FDA.BRANCH_ID=?BRANCH_OFFICE_ID AND FDA.PROJECT_ID IN (?PROJECT_ID)\n" +
                                "           AND FDA.STATUS = 1)\n" +
                            //"UNION (SELECT FD_VOUCHER_ID, '' AS FD_INTEREST_VOUCHER_ID\n" +
                            //"         FROM FD_RENEWAL FDR\n" +
                            //"        WHERE FD_ACCOUNT_ID IN (SELECT FDA.FD_ACCOUNT_ID\n" +
                            //"                                  FROM FD_ACCOUNT FDA\n" +
                            //"                                 WHERE FDA.PROJECT_ID IN (?PROJECT_ID)\n" +
                            //"                                   AND FDA.STATUS = 1))\n" +
                                "UNION (SELECT FDR.FD_VOUCHER_ID, FD_INTEREST_VOUCHER_ID\n" +
                                "         FROM FD_RENEWAL FDR\n" +
                                "        WHERE FD_ACCOUNT_ID IN (SELECT FDA.FD_ACCOUNT_ID\n" +
                                "                                  FROM FD_ACCOUNT FDA\n" +
                                "                                 WHERE FDA.BRANCH_ID=?BRANCH_OFFICE_ID AND FDA.PROJECT_ID IN (?PROJECT_ID)\n" +
                                "                                   AND FDA.STATUS = 1))) AS T\n" +
                                "    ON VT.VOUCHER_ID = T.FD_VOUCHER_ID\n" +
                                "    OR VT.VOUCHER_ID = T.FD_INTEREST_VOUCHER_ID WHERE VT.BRANCH_ID=?BRANCH_OFFICE_ID;";
                        break;
                    }
                case SQLCommand.ExportVouchers.FDVoucherTrans:
                    {
                        //Modified to fetch Head Office Ledgers if mapped, otherwise BO ledgers
                        query = "SELECT VT.VOUCHER_ID,\n" +
                                "       SEQUENCE_NO,\n" +
                                "       IF(MHL.LEDGER_NAME IS NOT NULL, MHL.LEDGER_NAME, ML.LEDGER_NAME) AS LEDGER_NAME,\n" +
                                "       (VT.AMOUNT * IF(MT.IS_MULTI_CURRENCY=1, MT.EXCHANGE_RATE, 1)) AS AMOUNT,\n" +
                                "       TRANS_MODE,\n" +
                                "       LEDGER_FLAG,\n" +
                                "       CHEQUE_NO,\n" +
                                "       MATERIALIZED_ON,\n" +
                                "       VT.STATUS\n" +
                                "  FROM VOUCHER_TRANS VT\n" +
                                "  INNER JOIN VOUCHER_MASTER_TRANS MT\n" +
                                "  ON MT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                "  LEFT JOIN MASTER_LEDGER ML\n" +
                                "    ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                                "  LEFT JOIN HEADOFFICE_MAPPED_LEDGER HML\n" +
                                "    ON VT.LEDGER_ID = HML.LEDGER_ID\n" +
                                "  LEFT JOIN MASTER_HEADOFFICE_LEDGER MHL\n" +
                                "    ON HML.HEADOFFICE_LEDGER_ID = MHL.HEADOFFICE_LEDGER_ID\n" +
                                "\n" +
                                "  JOIN((SELECT FD_VOUCHER_ID, '' AS FD_INTEREST_VOUCHER_ID\n" +
                                "          FROM FD_ACCOUNT FDA\n" +
                                "         WHERE FDA.PROJECT_ID IN (?PROJECT_ID)\n" +
                                "           AND FDA.STATUS = 1)\n" +
                            //"UNION (SELECT FD_VOUCHER_ID, '' AS FD_INTEREST_VOUCHER_ID\n" +
                            //"         FROM FD_RENEWAL FDR\n" +
                            //"        WHERE FD_ACCOUNT_ID IN (SELECT FDA.FD_ACCOUNT_ID\n" +
                            //"                                  FROM FD_ACCOUNT FDA\n" +
                            //"                                 WHERE FDA.PROJECT_ID IN (?PROJECT_ID)\n" +
                            //"                                   AND FDA.STATUS = 1))\n" +
                                "UNION (SELECT FDR.FD_VOUCHER_ID, FD_INTEREST_VOUCHER_ID\n" +
                                "         FROM FD_RENEWAL FDR\n" +
                                "        WHERE FD_ACCOUNT_ID IN (SELECT FDA.FD_ACCOUNT_ID\n" +
                                "                                  FROM FD_ACCOUNT FDA\n" +
                                "                                 WHERE FDA.PROJECT_ID IN (?PROJECT_ID)\n" +
                                "                                   AND FDA.STATUS = 1 AND FDR.RENEWAL_DATE <> '0001-01-01 00:00:00'))) AS T\n" +
                                "    ON VT.VOUCHER_ID = T.FD_VOUCHER_ID\n" +
                                "    OR VT.VOUCHER_ID = T.FD_INTEREST_VOUCHER_ID;";

                        //query = "SELECT VT.VOUCHER_ID,\n" +
                        //          "       SEQUENCE_NO,\n" +
                        //          "       IF(MHL.LEDGER_NAME IS NOT NULL, MHL.LEDGER_NAME, ML.LEDGER_NAME) AS LEDGER_NAME,\n" +
                        //          "       AMOUNT,\n" +
                        //          "       TRANS_MODE,\n" +
                        //          "       LEDGER_FLAG,\n" +
                        //          "       CHEQUE_NO,\n" +
                        //          "       MATERIALIZED_ON,\n" +
                        //          "       VT.STATUS\n" +
                        //          "  FROM VOUCHER_TRANS VT\n" +
                        //          "  LEFT JOIN MASTER_LEDGER ML\n" +
                        //          "    ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                        //          "  LEFT JOIN HEADOFFICE_MAPPED_LEDGER HML\n" +
                        //          "    ON VT.LEDGER_ID = HML.LEDGER_ID\n" +
                        //          "  LEFT JOIN MASTER_HEADOFFICE_LEDGER MHL\n" +
                        //          "    ON HML.HEADOFFICE_LEDGER_ID = MHL.HEADOFFICE_LEDGER_ID\n" +
                        //          "\n" +
                        //          "  JOIN((SELECT FD_VOUCHER_ID, '' AS FD_INTEREST_VOUCHER_ID\n" +
                        //          "          FROM FD_ACCOUNT FDA\n" +
                        //          "         WHERE FD_VOUCHER_ID IN\n" +
                        //          "               (SELECT VOUCHER_ID\n" +
                        //          "                  FROM VOUCHER_MASTER_TRANS VMT\n" +
                        //          "                 WHERE VOUCHER_SUB_TYPE = 'FD'\n" +
                        //          "                   AND VOUCHER_DATE BETWEEN ?DATE_FROM AND\n" +
                        //          "                       ?DATE_TO\n" +
                        //          "                   AND PROJECT_ID IN (?PROJECT_ID))\n" +
                        //          "           AND FDA.INVESTMENT_DATE <= ?DATE_TO)\n" +
                        //          "UNION (SELECT FD_VOUCHER_ID, '' AS FD_INTEREST_VOUCHER_ID\n" +
                        //          "         FROM FD_ACCOUNT FDA\n" +
                        //          "        WHERE FD_ACCOUNT_ID IN\n" +
                        //          "              (SELECT FDR.FD_ACCOUNT_ID\n" +
                        //          "                 FROM FD_RENEWAL FDR\n" +
                        //          "                WHERE FDR.FD_VOUCHER_ID IN\n" +
                        //          "                      (SELECT VOUCHER_ID\n" +
                        //          "                         FROM VOUCHER_MASTER_TRANS VMT\n" +
                        //          "                        WHERE VOUCHER_SUB_TYPE = 'FD'\n" +
                        //          "                          AND VOUCHER_DATE BETWEEN ?DATE_FROM AND\n" +
                        //          "                              ?DATE_TO\n" +
                        //          "                          AND PROJECT_ID IN (?PROJECT_ID)))\n" +
                        //          "          AND FDA.INVESTMENT_DATE <= ?DATE_TO)\n" +
                        //          "UNION (SELECT FDR.FD_VOUCHER_ID, FD_INTEREST_VOUCHER_ID\n" +
                        //          "         FROM FD_RENEWAL FDR\n" +
                        //          "        WHERE FD_ACCOUNT_ID IN\n" +
                        //          "              (SELECT FDR.FD_ACCOUNT_ID\n" +
                        //          "                 FROM FD_RENEWAL FDR\n" +
                        //          "                WHERE FDR.FD_VOUCHER_ID IN\n" +
                        //          "                      (SELECT VOUCHER_ID\n" +
                        //          "                         FROM VOUCHER_MASTER_TRANS VMT\n" +
                        //          "                        WHERE VOUCHER_SUB_TYPE = 'FD'\n" +
                        //          "                          AND VOUCHER_DATE BETWEEN ?DATE_FROM AND\n" +
                        //          "                              ?DATE_TO\n" +
                        //          "                          AND PROJECT_ID IN (?PROJECT_ID)))\n" +
                        //          "          AND FDR.RENEWAL_DATE <= ?DATE_TO)) AS T\n" +
                        //          "    ON VT.VOUCHER_ID = T.FD_VOUCHER_ID\n" +
                        //          "    OR VT.VOUCHER_ID = T.FD_INTEREST_VOUCHER_ID";
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
                case SQLCommand.ExportVouchers.FetchProjectLedgers:
                    {
                        query = "SELECT PL.PROJECT_ID, PL.LEDGER_ID, MP.PROJECT, ML.LEDGER_NAME, ML.GROUP_ID, ML.ACCESS_FLAG\n" +
                                "FROM PROJECT_LEDGER PL\n" +
                                "INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = PL.LEDGER_ID\n" +
                                "INNER JOIN MASTER_PROJECT MP ON MP.PROJECT_ID = PL.PROJECT_ID\n" +
                                "WHERE PL.PROJECT_ID IN (?PROJECT_ID) { AND (ML.DATE_CLOSED IS NULL OR ML.DATE_CLOSED >= ?DATE_FROM)}";
                        break;
                    }
                case SQLCommand.ExportVouchers.FetchProjectCostCenters:
                    {
                        query = "SELECT MP.PROJECT, MC.ABBREVATION, MC.COST_CENTRE_NAME, MCC.COST_CENTRE_CATEGORY_NAME, MC.NOTES\n" +
                                "FROM PROJECT_COSTCENTRE PC\n" +
                                "INNER JOIN MASTER_PROJECT MP ON MP.PROJECT_ID = PC.PROJECT_ID\n" +
                                "INNER JOIN MASTER_COST_CENTRE MC ON MC.COST_CENTRE_ID = PC.COST_CENTRE_ID\n" +
                                "INNER JOIN COSTCATEGORY_COSTCENTRE CC ON CC.COST_CENTRE_ID= MC.COST_CENTRE_ID\n" +
                                "INNER JOIN MASTER_COST_CENTRE_CATEGORY MCC ON MCC.COST_CENTRECATEGORY_ID = CC.COST_CATEGORY_ID\n" +
                                "WHERE PC.PROJECT_ID IN (?PROJECT_ID) { AND (ML.DATE_CLOSED IS NULL OR ML.DATE_CLOSED >= ?DATE_FROM)}";
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
                case SQLCommand.ExportVouchers.FetchBudgetMaster:
                    query = "SELECT BM.BUDGET_ID, MP.PROJECT_ID, BM.BUDGET_TYPE_ID, BM.BUDGET_LEVEL_ID,\n" +
                            "MP.PROJECT,  BM.BUDGET_NAME,  BT.BUDGET_TYPE, BV.BUDGET_LEVEL_NAME,\n" +
                            "BM.DATE_FROM, BM.DATE_TO, BM.IS_MONTH_WISE,  BM.REMARKS, BM.IS_ACTIVE,\n" +
                            "BM.BUDGET_ACTION, BM.HO_HELP_PROPOSED_AMOUNT, BM.HO_HELP_APPROVED_AMOUNT, BM.BRANCH_ID\n" +
                            "FROM (SELECT BM.*, GROUP_CONCAT(BP.PROJECT_ID) AS PROJECT_IDs\n" +
                            "      FROM BUDGET_MASTER BM INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID\n" +
                            "      WHERE ( (DATE_FROM >= ?DATE_FROM AND DATE_TO <= ?DATE_TO) OR\n" +
                            "      (BM.BUDGET_TYPE_ID = " + (int)BudgetType.BudgetByCalendarYear + " AND DATE_FROM >= ?YEAR_FROM AND DATE_TO <= ?YEAR_TO))\n" +
                            "      GROUP BY BM.BUDGET_ID HAVING Find_In_Set(?PROJECT_ID, PROJECT_IDs ) > 0) AS BM\n" +
                            "INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID\n" +
                            "INNER JOIN MASTER_PROJECT MP ON MP.PROJECT_ID = BP.PROJECT_ID\n" +
                            "INNER JOIN BUDGET_TYPE BT ON BT.BUDGET_TYPE_ID = BM.BUDGET_TYPE_ID\n" +
                            "INNER JOIN BUDGET_LEVEL BV ON BV.BUDGET_LEVEL_ID = BM.BUDGET_LEVEL_ID WHERE BP.PROJECT_ID IN (?PROJECT_ID)";
                    break;
                case SQLCommand.ExportVouchers.FetchBudgetProject:
                    query = "SELECT BM.BUDGET_ID, MP.PROJECT_ID, BM.BUDGET_NAME, MP.PROJECT\n" +
                                "FROM (SELECT BM.*, GROUP_CONCAT(BP.PROJECT_ID) AS PROJECT_IDs\n" +
                                "      FROM BUDGET_MASTER BM INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID\n" +
                                "      WHERE ( (DATE_FROM >= ?DATE_FROM AND DATE_TO <= ?DATE_TO) OR\n" +
                                "      (BM.BUDGET_TYPE_ID = " + (int)BudgetType.BudgetByCalendarYear + " AND DATE_FROM >= ?YEAR_FROM AND DATE_TO <= ?YEAR_TO))\n" +
                                "      GROUP BY BM.BUDGET_ID HAVING Find_In_Set(?PROJECT_ID, PROJECT_IDs ) > 0) AS BM\n" +
                                "INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID\n" +
                                "INNER JOIN MASTER_PROJECT MP ON MP.PROJECT_ID = BP.PROJECT_ID;";
                    break;
                case SQLCommand.ExportVouchers.FetchBudgetLedger:
                    query = "SELECT BM.BUDGET_ID, BL.LEDGER_ID, ML.LEDGER_NAME, BL.PROPOSED_AMOUNT, BL.APPROVED_AMOUNT, BL.TRANS_MODE, BL.NARRATION, BL.HO_NARRATION\n" +
                                "FROM (SELECT BM.*, GROUP_CONCAT(BP.PROJECT_ID) AS PROJECT_IDs\n" +
                                "      FROM BUDGET_MASTER BM INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID\n" +
                                "      WHERE ( (DATE_FROM >= ?DATE_FROM AND DATE_TO <= ?DATE_TO) OR\n" +
                                "      (BM.BUDGET_TYPE_ID = " + (int)BudgetType.BudgetByCalendarYear + " AND DATE_FROM >= ?YEAR_FROM AND DATE_TO <= ?YEAR_TO))\n" +
                                "      GROUP BY BM.BUDGET_ID HAVING Find_In_Set(?PROJECT_ID, PROJECT_IDs ) > 0) AS BM\n" +
                                "INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID\n" +
                                "INNER JOIN BUDGET_LEDGER BL ON BL.BUDGET_ID = BM.BUDGET_ID\n" +
                                "INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = BL.LEDGER_ID WHERE BP.PROJECT_ID IN (?PROJECT_ID)";
                    break;
                case SQLCommand.ExportVouchers.FetchBudgetStatisticsDetails:
                    query = "SELECT BM.BUDGET_ID, MST.STATISTICS_TYPE_ID, MST.STATISTICS_TYPE, BS.TOTAL_COUNT\n" +
                            "FROM (SELECT BM.*, GROUP_CONCAT(BP.PROJECT_ID) AS PROJECT_IDs\n" +
                            "      FROM BUDGET_MASTER BM INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID\n" +
                            "      WHERE ( (DATE_FROM >= ?DATE_FROM AND DATE_TO <= ?DATE_TO) OR\n" +
                            "      (BM.BUDGET_TYPE_ID = " + (int)BudgetType.BudgetByCalendarYear + " AND DATE_FROM >= ?YEAR_FROM AND DATE_TO <= ?YEAR_TO))\n" +
                            "      GROUP BY BM.BUDGET_ID HAVING Find_In_Set(?PROJECT_ID, PROJECT_IDs ) > 0) AS BM\n" +
                            "INNER JOIN BUDGET_PROJECT BP ON BP.BUDGET_ID = BM.BUDGET_ID\n" +
                            "INNER JOIN BUDGET_STATISTICS_DETAIL BS ON BS.BUDGET_ID = BM.BUDGET_ID\n" +
                            "INNER JOIN MASTER_STATISTICS_TYPE MST ON MST.STATISTICS_TYPE_ID = BS.STATISTICS_TYPE_ID WHERE BP.PROJECT_ID IN (?PROJECT_ID)";
                    break;
                case SQLCommand.ExportVouchers.FetchBudgetProjectLedger:
                    query = "SELECT MP.PROJECT_ID, PBL.LEDGER_ID, MP.PROJECT, ML.LEDGER_NAME\n" +
                            "FROM PROJECT_BUDGET_LEDGER PBL\n" +
                            "INNER JOIN MASTER_PROJECT MP ON MP.PROJECT_ID = PBL.PROJECT_ID\n" +
                            "INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = PBL.LEDGER_ID\n" +
                            "WHERE PBL.PROJECT_ID IN (?PROJECT_ID);";
                    break;
                case SQLCommand.ExportVouchers.FetchBudgetType:
                    query = "SELECT BUDGET_TYPE_ID, BUDGET_TYPE FROM BUDGET_TYPE";
                    break;
                case SQLCommand.ExportVouchers.FetchBudgetLevel:
                    query = "SELECT BUDGET_LEVEL_ID, BUDGET_LEVEL_NAME FROM BUDGET_LEVEL";
                    break;
                //case SQLCommand.ExportVouchers.FetchActiveTransactionperiod: //On 15/07/2017, commented by alawr (unused)
                //    {
                //        query = " SELECT ACC_YEAR_ID, " +
                //               " YEAR_FROM, " +
                //               " YEAR_TO, " +
                //               " (SELECT BOOKS_BEGINNING_FROM FROM " +
                //               " ACCOUNTING_YEAR WHERE IS_FIRST_ACCOUNTING_YEAR =1 ORDER BY ACC_YEAR_ID ASC LIMIT 1) AS  BOOKS_BEGINNING_FROM " +
                //               " FROM ACCOUNTING_YEAR WHERE STATUS=1";
                //        break;
                //    }
            }
            return query;
        }
        #endregion
    }
}
