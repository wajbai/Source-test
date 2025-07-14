using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class VoucherMasterSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.VoucherMaster).FullName)
            {
                query = GetVoucherMasterSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the bank details.
        /// </summary>
        /// <returns></returns>
        private string GetVoucherMasterSQL()
        {
            string query = "";
            SQLCommand.VoucherMaster sqlCommandId = (SQLCommand.VoucherMaster)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.VoucherMaster.Add:
                    {
                        query = "INSERT INTO VOUCHER_MASTER_TRANS ( " +
                               "VOUCHER_DATE, " +
                               "PROJECT_ID, " +
                               "VOUCHER_NO, " +
                               "VOUCHER_TYPE," +
                               "VOUCHER_SUB_TYPE," +
                               "DONOR_ID," +
                               "PURPOSE_ID," +
                               "CONTRIBUTION_TYPE," +
                               "CONTRIBUTION_AMOUNT," +
                               "CURRENCY_COUNTRY_ID," +
                               "EXCHANGE_RATE," +
                               "CALCULATED_AMOUNT," +
                               "ACTUAL_AMOUNT," +
                               "EXCHANGE_COUNTRY_ID," +
                               "NARRATION," +
                               "STATUS," +
                               "CREATED_ON," +
                            //"MODIFIED_ON," +
                               "CREATED_BY,NAME_ADDRESS,PAN_NUMBER,GST_NUMBER," +
                               "CLIENT_REFERENCE_ID,CLIENT_CODE,CLIENT_MODE," +
                               "GST_VENDOR_INVOICE_NO, GST_VENDOR_INVOICE_DATE, GST_VENDOR_ID,GST_VENDOR_INVOICE_TYPE," +
                               "CREATED_BY_NAME, " +
                               "VOUCHER_DEFINITION_ID, AUTHORIZATION_STATUS, AUTHORIZATION_UPDATED_ON, AUTHORIZATION_UPDATED_BY_NAME, IS_MULTI_CURRENCY,IS_CASH_BANK_STATUS) " +
                               "VALUES( " +
                               "?VOUCHER_DATE, " +
                               "?PROJECT_ID, " +
                               "?VOUCHER_NO, " +
                               "?VOUCHER_TYPE," +
                               "?VOUCHER_SUB_TYPE," +
                               "?DONOR_ID," +
                               "?PURPOSE_ID," +
                               "?CONTRIBUTION_TYPE," +
                               "?CONTRIBUTION_AMOUNT," +
                               "?CURRENCY_COUNTRY_ID," +
                               "?EXCHANGE_RATE," +
                               "?CALCULATED_AMOUNT," +
                               "?ACTUAL_AMOUNT," +
                               "?EXCHANGE_COUNTRY_ID," +
                               "?NARRATION," +
                               "?STATUS," +
                               " NOW()," +
                            //" NOW()," +
                               "?CREATED_BY,?NAME_ADDRESS,?PAN_NUMBER,?GST_NUMBER," +
                               "?CLIENT_REFERENCE_ID,?CLIENT_CODE,?CLIENT_MODE," +
                               "?GST_VENDOR_INVOICE_NO, ?GST_VENDOR_INVOICE_DATE, IF(?GST_VENDOR_ID = 0, NULL, ?GST_VENDOR_ID),?GST_VENDOR_INVOICE_TYPE," +
                               "?CREATED_BY_NAME," +
                               "?VOUCHER_DEFINITION_ID, ?AUTHORIZATION_STATUS, IF(?AUTHORIZATION_STATUS=1, NOW(), null)," +
                               "IF(?AUTHORIZATION_STATUS=1, ?AUTHORIZATION_UPDATED_BY_NAME, ''), ?IS_MULTI_CURRENCY,?IS_CASH_BANK_STATUS)";
                        break;
                    }
                case SQLCommand.VoucherMaster.AddTallyMigration:
                    {
                        query = @"INSERT INTO VOUCHER_MASTER_TRANS(VOUCHER_DATE,PROJECT_ID,VOUCHER_NO,VOUCHER_TYPE,NARRATION,CREATED_BY,MODIFIED_BY) 
                                VALUES(?VOUCHER_DATE,?PROJECT_ID,?VOUCHER_NO,?VOUCHER_TYPE,?NARRATION,?CREATED_BY,?MODIFIED_BY)";
                        break;
                    }
                case SQLCommand.VoucherMaster.UpdateTallyTransNarration:
                    {
                        query = "UPDATE VOUCHER_MASTER_TRANS SET NARRATION=?NARRATION WHERE VOUCHER_ID=?VOUCHER_ID;";
                        break;
                    }
                case SQLCommand.VoucherMaster.Update:
                    {
                        query = "UPDATE VOUCHER_MASTER_TRANS SET " +
                                    "VOUCHER_DATE = ?VOUCHER_DATE, " +
                                    "PROJECT_ID =?PROJECT_ID, " +
                                    "VOUCHER_NO =?VOUCHER_NO, " +
                                    "VOUCHER_TYPE=?VOUCHER_TYPE, " +
                                    "VOUCHER_SUB_TYPE=VOUCHER_SUB_TYPE," +
                                    "DONOR_ID=?DONOR_ID," +
                                    "PURPOSE_ID=?PURPOSE_ID," +
                                    "CONTRIBUTION_TYPE=?CONTRIBUTION_TYPE ," +
                                    "CONTRIBUTION_AMOUNT=?CONTRIBUTION_AMOUNT," +
                                    "CURRENCY_COUNTRY_ID=?CURRENCY_COUNTRY_ID," +
                                    "EXCHANGE_RATE=?EXCHANGE_RATE ," +
                                    "CALCULATED_AMOUNT=?CALCULATED_AMOUNT ," +
                                    "ACTUAL_AMOUNT=?ACTUAL_AMOUNT ," +
                                    "EXCHANGE_COUNTRY_ID=?EXCHANGE_COUNTRY_ID," +
                                    "NARRATION=?NARRATION ," +
                                    "STATUS=?STATUS," +
                                    "MODIFIED_ON= NOW()," +
                                    "MODIFIED_BY=?MODIFIED_BY, " +
                                    "MODIFIED_BY_NAME=?MODIFIED_BY_NAME," +
                                    "NAME_ADDRESS=?NAME_ADDRESS, " +
                                    "PAN_NUMBER=?PAN_NUMBER, " +
                                    "GST_NUMBER=?GST_NUMBER, " +
                                    "CLIENT_REFERENCE_ID=?CLIENT_REFERENCE_ID," + // Newly Added
                                    "CLIENT_CODE=?CLIENT_CODE," +
                                    "CLIENT_MODE=?CLIENT_MODE," +
                                    "GST_VENDOR_INVOICE_NO=?GST_VENDOR_INVOICE_NO, " +
                                    "GST_VENDOR_INVOICE_DATE=?GST_VENDOR_INVOICE_DATE, " +
                                    "GST_VENDOR_ID=IF(?GST_VENDOR_ID = 0, NULL, ?GST_VENDOR_ID), " +
                                    "GST_VENDOR_INVOICE_TYPE=?GST_VENDOR_INVOICE_TYPE, " +
                            //"IS_AUDITOR_MODIFIED =?IS_AUDITOR_MODIFIED,"+
                                    "VOUCHER_DEFINITION_ID=?VOUCHER_DEFINITION_ID, IS_MULTI_CURRENCY=?IS_MULTI_CURRENCY,IS_CASH_BANK_STATUS=?IS_CASH_BANK_STATUS " +
                                    " WHERE VOUCHER_ID=?VOUCHER_ID ";
                        break;
                    }
                case SQLCommand.VoucherMaster.UpdateVoucherNumber:
                    {
                        query = "UPDATE VOUCHER_MASTER_TRANS SET " +
                                    "VOUCHER_NO=?VOUCHER_NO " +
                                    "WHERE VOUCHER_ID=?VOUCHER_ID ";
                        break;
                    }
                case SQLCommand.VoucherMaster.UpdateModifiedDetailsbyLedgerIds:
                    {
                        //, IS_AUDITOR_MODIFIED = IF(?IS_AUDITOR = 1, 1, IS_AUDITOR_MODIFIED)
                        query = "UPDATE VOUCHER_MASTER_TRANS SET MODIFIED_ON = NOW(), MODIFIED_BY = ?MODIFIED_BY, \n" +
                                 "MODIFIED_BY_NAME = ?USER_NAME\n" +
                                 "WHERE STATUS =1 {AND PROJECT_ID IN (?PROJECT_ID)} AND VOUCHER_ID IN (SELECT VOUCHER_ID FROM VOUCHER_TRANS WHERE LEDGER_ID IN (?LEDGER_ID))";
                        break;
                    }
                case SQLCommand.VoucherMaster.UpdateModifiedDetailsbyVoucherIds:
                    {
                        query = "UPDATE VOUCHER_MASTER_TRANS SET MODIFIED_ON = NOW(), MODIFIED_BY = ?MODIFIED_BY, MODIFIED_BY_NAME = ?USER_NAME\n" +
                                "WHERE STATUS =1 {AND PROJECT_ID IN (?PROJECT_ID)} AND VOUCHER_ID IN (?VOUCHER_ID)";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchReGenerationVouchers:
                    {
                        query = " SELECT VOUCHER_ID,VOUCHER_DATE, PROJECT_ID, VOUCHER_NO, VOUCHER_TYPE\n" +
                                "  FROM VOUCHER_MASTER_TRANS\n" +
                                " WHERE PROJECT_ID = ?PROJECT_ID\n" +
                                " AND VOUCHER_TYPE=?VOUCHER_TYPE\n" +
                                " AND VOUCHER_DATE BETWEEN ?YEAR_FROM AND ?YEAR_TO\n" +
                                " AND VOUCHER_DEFINITION_ID=?VOUCHER_DEFINITION_ID" +
                                " AND STATUS<>0 {AND VOUCHER_NO>=?VOUCHER_NO AND VOUCHER_ID!=?VOUCHER_ID} ORDER BY VOUCHER_DATE, LENGTH(VOUCHER_NO),VOUCHER_NO ASC";
                        // ORDER BY VOUCHER_DATE, VOUCHER_ID";
                        // ORDER BY VOUCHER_DATE, LENGTH(VOUCHER_NO),VOUCHER_NO ASC";

                        break;
                    }
                case SQLCommand.VoucherMaster.SaveGSTInvoiceMasterDetails:
                    {
                        query = @"INSERT INTO GST_INVOICE_MASTER (GST_VENDOR_INVOICE_NO, BOOKING_VOUCHER_ID, BOOKING_VOUCHER_TYPE, GST_VENDOR_INVOICE_DATE, 
                                    GST_VENDOR_INVOICE_TYPE, GST_VENDOR_ID, DUE_DATE,
                                    TRANSPORT_MODE, VEHICLE_NUMBER, SUPPLY_DATE, SUPPLY_PLACE, BILLING_NAME, BILLING_GST_NO, BILLING_ADDRESS, BILLING_STATE_ID, BILLING_COUNTRY_ID,
                                    SHIPPING_NAME, SHIPPING_GST_NO, SHIPPING_ADDRESS, SHIPPING_STATE_ID, SHIPPING_COUNTRY_ID, CHEQUE_IN_FAVOUR, 
                                    TOTAL_AMOUNT, TOTAL_CGST_AMOUNT, TOTAL_SGST_AMOUNT, TOTAL_IGST_AMOUNT,
                                    IS_REVERSE_CHARGE, REVERSE_CHARGE_AMOUNT, STATUS)
                                VALUES (?GST_VENDOR_INVOICE_NO, ?BOOKING_VOUCHER_ID, ?BOOKING_VOUCHER_TYPE, ?GST_VENDOR_INVOICE_DATE, 
                                    ?GST_VENDOR_INVOICE_TYPE, ?GST_VENDOR_ID, ?DUE_DATE,
                                    ?TRANSPORT_MODE, ?VEHICLE_NUMBER, ?SUPPLY_DATE, ?SUPPLY_PLACE, ?BILLING_NAME, ?BILLING_GST_NO, ?BILLING_ADDRESS, 
                                    IF(?BILLING_STATE_ID=0, null,?BILLING_STATE_ID), IF(?BILLING_COUNTRY_ID=0, null, ?BILLING_COUNTRY_ID),
                                    ?SHIPPING_NAME, ?SHIPPING_GST_NO, ?SHIPPING_ADDRESS, IF(?SHIPPING_STATE_ID=0, null, ?SHIPPING_STATE_ID), 
                                    IF(?SHIPPING_COUNTRY_ID=0, null, ?SHIPPING_COUNTRY_ID), 
                                    ?CHEQUE_IN_FAVOUR, ?TOTAL_AMOUNT, ?TOTAL_CGST_AMOUNT, ?TOTAL_SGST_AMOUNT, 
                                    ?TOTAL_IGST_AMOUNT, ?IS_REVERSE_CHARGE, ?REVERSE_CHARGE_AMOUNT,  ?STATUS)";
                        break;
                    }
                case SQLCommand.VoucherMaster.UpdateGSTInvoiceMasterDetails:
                    {
                        query = @"UPDATE GST_INVOICE_MASTER SET GST_VENDOR_INVOICE_NO = ?GST_VENDOR_INVOICE_NO, BOOKING_VOUCHER_ID=?BOOKING_VOUCHER_ID,
                                    BOOKING_VOUCHER_TYPE=?BOOKING_VOUCHER_TYPE, GST_VENDOR_INVOICE_DATE = ?GST_VENDOR_INVOICE_DATE,
                                    GST_VENDOR_INVOICE_TYPE = ?GST_VENDOR_INVOICE_TYPE, GST_VENDOR_ID = ?GST_VENDOR_ID, DUE_DATE = ?DUE_DATE,
                                    TRANSPORT_MODE = ?TRANSPORT_MODE, VEHICLE_NUMBER = ?VEHICLE_NUMBER, SUPPLY_DATE = ?SUPPLY_DATE, SUPPLY_PLACE = ?SUPPLY_PLACE,
                                    BILLING_NAME=?BILLING_NAME, BILLING_GST_NO=?BILLING_GST_NO,
                                    BILLING_ADDRESS = ?BILLING_ADDRESS, BILLING_STATE_ID= IF(?BILLING_STATE_ID=0, null,?BILLING_STATE_ID),
                                    BILLING_COUNTRY_ID = IF(?BILLING_COUNTRY_ID = 0, null, ?BILLING_COUNTRY_ID),
                                    SHIPPING_NAME=?SHIPPING_NAME, SHIPPING_GST_NO=?SHIPPING_GST_NO,
                                    SHIPPING_ADDRESS = ?SHIPPING_ADDRESS, SHIPPING_STATE_ID = IF(?SHIPPING_STATE_ID = 0, null, ?SHIPPING_STATE_ID),
                                    SHIPPING_COUNTRY_ID = IF(?SHIPPING_COUNTRY_ID = 0, null,?SHIPPING_COUNTRY_ID),
                                    CHEQUE_IN_FAVOUR=?CHEQUE_IN_FAVOUR, TOTAL_AMOUNT=?TOTAL_AMOUNT, TOTAL_CGST_AMOUNT=?TOTAL_CGST_AMOUNT,
                                    TOTAL_SGST_AMOUNT=?TOTAL_SGST_AMOUNT, TOTAL_IGST_AMOUNT=?TOTAL_IGST_AMOUNT,
                                    IS_REVERSE_CHARGE=?IS_REVERSE_CHARGE, REVERSE_CHARGE_AMOUNT=?REVERSE_CHARGE_AMOUNT, STATUS=?STATUS
                                    WHERE GST_INVOICE_ID=?GST_INVOICE_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.DeleteGSTInvoiceLedgerDetails:
                    {
                        query = @"DELETE FROM GST_INVOICE_MASTER_DETAILS WHERE GST_INVOICE_ID=?GST_INVOICE_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.UpdateGSTInvoiceLedgerDetails:
                    {
                        query = @"INSERT INTO GST_INVOICE_MASTER_DETAILS (GST_INVOICE_ID, LEDGER_ID, LEDGER_GST_CLASS_ID, ITEM_NAME, ITEM_DESCRIPTION, AMOUNT, 
                                    TRANS_MODE, GST_HSN_SAC_CODE, QUANTITY, UNIT_MEASUREMENT, UNIT_AMOUNT, DISCOUNT, CGST, SGST, IGST, BRANCH_ID)
                                VALUES (?GST_INVOICE_ID, ?LEDGER_ID, ?LEDGER_GST_CLASS_ID, ?ITEM_NAME, ?ITEM_DESCRIPTION, ?AMOUNT, ?TRANS_MODE, ?GST_HSN_SAC_CODE,  
                                    ?QUANTITY, ?UNIT_MEASUREMENT, ?UNIT_AMOUNT, ?DISCOUNT, ?CGST, ?SGST, ?IGST, ?BRANCH_ID)";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchLastDateSyncDetails:
                    {
                        query = @"SELECT VMT.PROJECT_ID AS PROJECT_ID, MP.PROJECT, DATE_FORMAT(MAX(VMT.VOUCHER_DATE), '%d/%m/%Y') AS VOUCHER_DATE FROM
                            VOUCHER_MASTER_TRANS VMT INNER JOIN MASTER_PROJECT MP ON MP.PROJECT_ID = VMT.PROJECT_ID
                            WHERE VMT.CLIENT_CODE IS NOT NULL GROUP BY VMT.PROJECT_ID ORDER BY PROJECT_ID, VOUCHER_DATE";
                        break;
                    }
                case SQLCommand.VoucherMaster.SaveGSTInvoiceVocuhersByVoucherId:
                    {
                        query = @"INSERT INTO VOUCHER_GST_INVOICE (GST_INVOICE_ID, AMOUNT, VOUCHER_ID) VALUES(?GST_INVOICE_ID, ?AMOUNT, ?VOUCHER_ID)";
                        break;
                    }
                case SQLCommand.VoucherMaster.UpdateGSTInvoiceVocuhersByVoucherId:
                    {
                        query = @"UPDATE VOUCHER_GST_INVOICE SET AMOUNT=?AMOUNT WHERE GST_INVOICE_ID=?GST_INVOICE_ID AND VOUCHER_ID=?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.UpdateGSTInvoiceMasterStatus:
                    {
                        query = @"UPDATE GST_INVOICE_MASTER GIM
                                    INNER JOIN (SELECT VGI.GST_INVOICE_ID, GIM.TOTAL_AMOUNT, SUM(VGI.AMOUNT) AS PAID_AMOUNT, (GIM.TOTAL_AMOUNT - SUM(VGI.AMOUNT)) AS BALANCE
                                      FROM GST_INVOICE_MASTER GIM
                                      INNER JOIN VOUCHER_GST_INVOICE VGI ON VGI.GST_INVOICE_ID = GIM.GST_INVOICE_ID
                                      INNER JOIN VOUCHER_MASTER_TRANS VM ON VM.VOUCHER_ID = VGI.VOUCHER_ID AND VM.STATUS=1
                                      WHERE BOOKING_VOUCHER_TYPE = 'JN' AND GIM.GST_INVOICE_ID = ?GST_INVOICE_ID
                                      GROUP BY VGI.GST_INVOICE_ID) AS VGI ON VGI.GST_INVOICE_ID = GIM.GST_INVOICE_ID
                                    SET GIM.STATUS = IF(BALANCE>0, 1, 2)
                                    WHERE BOOKING_VOUCHER_TYPE = 'JN' AND GIM.GST_INVOICE_ID = ?GST_INVOICE_ID;";
                        break;
                    }
                case SQLCommand.VoucherMaster.DeleteRandPVoucherAgainsInvoiceById:
                    {
                        query = @"DELETE FROM VOUCHER_GST_INVOICE WHERE VOUCHER_ID=?VOUCHER_ID {AND GST_INVOICE_ID=?GST_INVOICE_ID}";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchGSTInvoiceMasterDetailsById:
                    {
                        query = @"SELECT GIM.GST_INVOICE_ID, GST_VENDOR_INVOICE_NO, GST_VENDOR_INVOICE_DATE, GST_VENDOR_INVOICE_TYPE, GST_VENDOR_ID, DUE_DATE,
                                    TRANSPORT_MODE, VEHICLE_NUMBER, SUPPLY_DATE, SUPPLY_PLACE, BILLING_NAME, BILLING_GST_NO, BILLING_ADDRESS, BILLING_STATE_ID, BILLING_COUNTRY_ID,
                                    SHIPPING_NAME, SHIPPING_GST_NO, SHIPPING_ADDRESS, SHIPPING_STATE_ID, SHIPPING_COUNTRY_ID, CHEQUE_IN_FAVOUR, TOTAL_AMOUNT, 
                                    TOTAL_CGST_AMOUNT, TOTAL_SGST_AMOUNT, TOTAL_IGST_AMOUNT,
                                    IS_REVERSE_CHARGE, REVERSE_CHARGE_AMOUNT,BRANCH_ID, STATUS
                                    FROM GST_INVOICE_MASTER GIM
                                    WHERE GIM.GST_INVOICE_ID = ?GST_INVOICE_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchGSTInvoiceMasterLedgerDetailsByGSTInvoiceId:
                    {
                        query = @"SELECT GIMD.GST_INVOICE_ID AS GST_INVOICE_ID, GIMD.LEDGER_ID, GIMD.LEDGER_GST_CLASS_ID, ML.LEDGER_NAME, 
                                    GIMD.ITEM_NAME, GIMD.ITEM_DESCRIPTION, MGS.SLAB AS LEDGER_GST_CLASS, 
                                    GIMD.GST_HSN_SAC_CODE, QUANTITY, UNIT_MEASUREMENT, UNIT_AMOUNT, DISCOUNT, GIMD.AMOUNT, GIMD.TRANS_MODE, 
                                    GIMD.CGST, GIMD.SGST, GIMD.IGST
                                    FROM GST_INVOICE_MASTER_DETAILS GIMD 
                                    INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID  = GIMD.LEDGER_ID 
                                    INNER JOIN MASTER_GST_CLASS MGS ON MGS.GST_ID = GIMD.LEDGER_GST_CLASS_ID
                                    WHERE GIMD.GST_INVOICE_ID = ?GST_INVOICE_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchGSTInvoiceVouchersById:
                    {
                        query = @"SELECT VOUCHER_ID, GST_INVOICE_ID, AMOUNT FROM VOUCHER_GST_INVOICE WHERE VOUCHER_ID = ?VOUCHER_ID AND GST_INVOICE_ID = ?GST_INVOICE_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchGSTInvoiceIdByVoucherId:
                    {
                        query = @"SELECT VOUCHER_ID, GST_INVOICE_ID FROM VOUCHER_GST_INVOICE WHERE VOUCHER_ID = ?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchVouchersForInsert:
                    {
                        query = " SELECT VOUCHER_ID,VOUCHER_DATE, PROJECT_ID, VOUCHER_NO, VOUCHER_TYPE\n" +
                               "  FROM VOUCHER_MASTER_TRANS\n" +
                               " WHERE PROJECT_ID = ?PROJECT_ID\n" +
                               " AND VOUCHER_TYPE=?VOUCHER_TYPE\n" +
                               " AND VOUCHER_DATE BETWEEN ?YEAR_FROM AND ?YEAR_TO\n" +
                               " AND VOUCHER_NO>?VOUCHER_NO AND STATUS<>0 ORDER BY VOUCHER_DATE,VOUCHER_NO ASC";
                        break;
                    }
                case SQLCommand.VoucherMaster.Delete:
                    {
                        query = "UPDATE  VOUCHER_MASTER_TRANS SET PREVIOUS_VOUCHER_NO = VOUCHER_NO, VOUCHER_NO=NULL, IS_AUDITOR_MODIFIED=?IS_AUDITOR_MODIFIED, STATUS=0,\n" +
                                "MODIFIED_ON=NOW(), MODIFIED_BY=?MODIFIED_BY, MODIFIED_BY_NAME=?MODIFIED_BY_NAME\n" +
                                "WHERE VOUCHER_ID=?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.DeleteReference:
                    {
                        query = "DELETE FROM VOUCHER_REFERENCE WHERE REC_PAY_VOUCHER_ID=?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.IsExistReferenceVoucherTrans:
                    {
                        query = "SELECT COUNT(REF_VOUCHER_ID) AS REF_VOUCHER_ID FROM VOUCHER_REFERENCE WHERE REF_VOUCHER_ID =?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.DeleteRefererdVoucherTrans:
                    {
                        query = "DELETE FROM VOUCHER_REFERENCE WHERE REC_PAY_VOUCHER_ID =?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.DeleteRefererdVouchersByJournalVoucher:
                    {
                        query = "DELETE FROM VOUCHER_REFERENCE WHERE REF_VOUCHER_ID =?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchGSTPendingInvoices:
                    {
                        query = @"SELECT GIM.GST_INVOICE_ID, GIM.GST_VENDOR_INVOICE_NO, GIM.GST_VENDOR_INVOICE_DATE, ASV.VENDOR, VM.PARTY_LEDGER_NAME, VM.LEDGER_NAME,
                                  VM.AMOUNT, VM.TAXABLE_AMOUNT, (VM.AMOUNT-(IFNULL(VGI.BALANCE, 0))) AS BALANCE, VM.GST, 
                                  CONCAT(CONCAT(GIM.GST_VENDOR_INVOICE_NO, CONCAT( ' - ' , DATE_FORMAT(GIM.GST_VENDOR_INVOICE_DATE, '%d/%m/%Y'))), CONCAT(' - ', ASV.VENDOR)) AS VENDOR_GST_INVOICE
                                  FROM GST_INVOICE_MASTER GIM
                                  INNER JOIN GST_INVOICE_MASTER_DETAILS GIMD ON GIMD.GST_INVOICE_ID = GIM.GST_INVOICE_ID
                                  INNER JOIN ASSET_STOCK_VENDOR ASV ON ASV.VENDOR_ID = GIM.GST_VENDOR_ID
                                  INNER JOIN (SELECT VM.VOUCHER_ID,GROUP_CONCAT(IF(GST=0, ML.LEDGER_NAME, null), '') AS PARTY_LEDGER_NAME,
                                      GROUP_CONCAT(IF(GST > 0, ML.LEDGER_NAME, VTAG.LEDGER_NAME), '') AS LEDGER_NAME,
                                      SUM(IF(GST = 0, VT.AMOUNT, 0)) AS AMOUNT,
                                      SUM(IF(GST > 0, VT.AMOUNT, 0)) AS TAXABLE_AMOUNT, 
                                      SUM(VT.GST) AS GST, SUM(VT.CGST) AS CGST, SUM(VT.SGST) AS SGST, SUM(VT.IGST) AS IGST
                                      FROM VOUCHER_MASTER_TRANS VM
                                      INNER JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VM.VOUCHER_ID
                                      INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID
                                      LEFT JOIN (SELECT VT.VOUCHER_ID, ML.LEDGER_NAME
                                                 FROM VOUCHER_TRANS VT INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID
                                                 WHERE VT.TRANS_MODE <> ?TRANS_MODE) AS VTAG ON VTAG.VOUCHER_ID = VM.VOUCHER_ID
                                      WHERE VM.STATUS = 1 AND VM.VOUCHER_TYPE ='JN' AND IF(?ENABLE_GST=0,  VT.TRANS_MODE = ?TRANS_MODE, 1=1)
                                        AND PROJECT_ID IN (?PROJECT_ID) AND VT.LEDGER_ID NOT IN (?CGST,?SGST,?IGST) AND 
                                        VM.GST_VENDOR_ID>0 AND LENGTH(VM.GST_VENDOR_INVOICE_NO) > 0 AND LENGTH(VM.GST_VENDOR_INVOICE_DATE) > 0
                                      GROUP BY VM.VOUCHER_ID) AS VM ON VM.VOUCHER_ID = GIM.BOOKING_VOUCHER_ID
                                    AND GIM.BOOKING_VOUCHER_TYPE='JN' AND GIMD.TRANS_MODE=?TRANS_MODE 
                                  LEFT JOIN (SELECT GIM.GST_INVOICE_ID, SUM(VGI.AMOUNT) AS BALANCE
                                        FROM VOUCHER_GST_INVOICE VGI
                                        INNER JOIN GST_INVOICE_MASTER GIM ON GIM.GST_INVOICE_ID = VGI.GST_INVOICE_ID
                                        WHERE VGI.VOUCHER_ID <> ?VOUCHER_ID AND GIM.BOOKING_VOUCHER_ID >0 AND GIM.BOOKING_VOUCHER_TYPE='JN' AND GIM.GST_VENDOR_INVOICE_DATE <= ?VOUCHER_DATE 
                                        GROUP BY VGI.GST_INVOICE_ID) AS VGI ON VGI.GST_INVOICE_ID = GIM.GST_INVOICE_ID
                                  WHERE GIM.GST_VENDOR_INVOICE_DATE <= ?VOUCHER_DATE AND GIM.BOOKING_VOUCHER_ID > 0
                                  GROUP BY GIM.GST_INVOICE_ID;";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchGSTPendingInvoicesBookingDetailsByInvoiceId:
                    {
                        query = @"SELECT GIM.GST_INVOICE_ID, VM.PARTY_LEDGER_ID, VM.LEDGER_ID, GIM.GST_VENDOR_INVOICE_NO, GIM.GST_VENDOR_INVOICE_DATE, ASV.VENDOR, VM.PARTY_LEDGER_NAME, VM.LEDGER_NAME,
                                    (VM.AMOUNT-(IFNULL(VGI.BALANCE, 0))) AS AMOUNT, VM.TAXABLE_AMOUNT, VM.GST,
                                    CONCAT(CONCAT(GIM.GST_VENDOR_INVOICE_NO, CONCAT( ' - ' , DATE_FORMAT(GIM.GST_VENDOR_INVOICE_DATE, '%d/%m/%Y'))), CONCAT(' - ', ASV.VENDOR)) AS VENDOR_GST_INVOICE
                                    FROM GST_INVOICE_MASTER GIM
                                    INNER JOIN ASSET_STOCK_VENDOR ASV ON ASV.VENDOR_ID = GIM.GST_VENDOR_ID
                                    INNER JOIN (SELECT VM.VOUCHER_ID, IF(GST = 0, ML.LEDGER_ID, NULL) AS PARTY_LEDGER_ID, IF(GST > 0, ML.LEDGER_ID, NULL) AS LEDGER_ID, 
                                          IF(GST=0, ML.LEDGER_NAME, '') AS PARTY_LEDGER_NAME, SUM(IF(GST = 0, VT.AMOUNT, 0)) AS AMOUNT,
                                          IF(GST>0, ML.LEDGER_NAME, '') AS LEDGER_NAME, SUM(IF(GST > 0, VT.AMOUNT, 0)) AS TAXABLE_AMOUNT, SUM(VT.GST) AS GST
                                          FROM VOUCHER_MASTER_TRANS VM
                                          INNER JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VM.VOUCHER_ID
                                          INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID
                                          WHERE VM.STATUS = 1 AND VM.VOUCHER_TYPE ='JN' AND IF(?ENABLE_GST=0, VT.TRANS_MODE = ?TRANS_MODE, 1=1) 
                                            AND VM.PROJECT_ID IN (?PROJECT_ID) AND VT.LEDGER_ID NOT IN (?CGST,?SGST,?IGST) AND
                                            VM.GST_VENDOR_ID>0 AND LENGTH(VM.GST_VENDOR_INVOICE_NO) > 0 AND LENGTH(VM.GST_VENDOR_INVOICE_DATE) > 0 
                                          GROUP BY VM.VOUCHER_ID, VT.LEDGER_ID) AS VM ON VM.VOUCHER_ID = GIM.BOOKING_VOUCHER_ID
                                    LEFT JOIN (SELECT GIM.GST_INVOICE_ID, SUM(VGI.AMOUNT) AS BALANCE
                                        FROM VOUCHER_GST_INVOICE VGI
                                        INNER JOIN GST_INVOICE_MASTER GIM ON GIM.GST_INVOICE_ID = VGI.GST_INVOICE_ID
                                        WHERE VGI.VOUCHER_ID <> ?VOUCHER_ID AND GIM.BOOKING_VOUCHER_ID >0 AND GIM.BOOKING_VOUCHER_TYPE='JN'
                                        GROUP BY VGI.GST_INVOICE_ID) AS VGI ON VGI.GST_INVOICE_ID = GIM.GST_INVOICE_ID
                                    WHERE GIM.BOOKING_VOUCHER_ID > 0 AND GIM.BOOKING_VOUCHER_TYPE='JN' AND GIM.GST_INVOICE_ID = ?GST_INVOICE_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.DeleteGSTInvoiceDetailsById:
                    {
                        // AND VOUCHER_ID =?VOUCHER_ID
                        query = @"DELETE FROM VOUCHER_GST_INVOICE WHERE GST_INVOICE_ID =?GST_INVOICE_ID;
                                  DELETE FROM GST_INVOICE_MASTER_DETAILS WHERE GST_INVOICE_ID =?GST_INVOICE_ID;
                                  DELETE FROM GST_INVOICE_MASTER WHERE GST_INVOICE_ID =?GST_INVOICE_ID;
                                  UPDATE VOUCHER_MASTER_TRANS SET GST_VENDOR_INVOICE_NO=null, GST_VENDOR_INVOICE_DATE =null, GST_VENDOR_INVOICE_DATE =null, GST_VENDOR_ID=null 
                                    WHERE VOUCHER_ID = ?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchPayIdByJournalVoucherId:
                    {
                        query = "SELECT REC_PAY_VOUCHER_ID FROM VOUCHER_REFERENCE WHERE REF_VOUCHER_ID =?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.IsExistReferenceNo:
                    {
                        query = "SELECT COUNT(*) AS COUNT FROM VOUCHER_TRANS WHERE REFERENCE_NUMBER =?REFERENCE_NUMBER AND LEDGER_ID <>?LEDGER_ID AND VOUCHER_ID <> ?VOUCHER_ID;";
                        break;
                    }
                case SQLCommand.VoucherMaster.IsExistGSTInvoiceNo:
                    {
                        query = "SELECT COUNT(*) AS COUNT FROM VOUCHER_MASTER_TRANS WHERE STATUS=1 AND GST_VENDOR_INVOICE_NO =?GST_VENDOR_INVOICE_NO AND VOUCHER_ID <> ?VOUCHER_ID;";
                        break;
                    }
                case SQLCommand.VoucherMaster.IsExistGSTVouchers:
                    {
                        query = "SELECT VM.VOUCHER_ID FROM VOUCHER_MASTER_TRANS VM\n" +
                                    "INNER JOIN VOUCHER_TRANS VT ON VM.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                    "WHERE VM.STATUS=1 AND VT.LEDGER_GST_CLASS_ID >0 LIMIT 1";
                        break;
                    }
                case SQLCommand.VoucherMaster.IsExistGSTVouchersByGSTClassId:
                    {
                        query = "SELECT VM.VOUCHER_ID FROM VOUCHER_MASTER_TRANS VM\n" +
                                    "INNER JOIN VOUCHER_TRANS VT ON VM.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                    "WHERE VM.STATUS=1 AND VT.LEDGER_GST_CLASS_ID=?LEDGER_GST_CLASS_ID LIMIT 1";
                        break;
                    }
                case SQLCommand.VoucherMaster.IsExistGSTVendorVouchers:
                    {
                        query = "SELECT VM.VOUCHER_ID FROM VOUCHER_MASTER_TRANS VM\n" +
                                  "WHERE VM.STATUS=1 AND VM.GST_VENDOR_ID > 0 LIMIT 1";
                        break;
                    }
                case SQLCommand.VoucherMaster.IsZeroValuedCashBankExistsInVouchers:
                    {
                        query = @"SELECT VM.VOUCHER_ID FROM VOUCHER_MASTER_TRANS VM INNER JOIN VOUCHER_TRANS VT ON VM.VOUCHER_ID = VT.VOUCHER_ID
                                    INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID AND ML.GROUP_ID IN (12, 13)
                                    WHERE VM.STATUS = 1 AND VT.AMOUNT=0 LIMIT 1;";
                        break;
                    }
                case SQLCommand.VoucherMaster.IsVouchersExists:
                    {
                        query = @"SELECT VM.VOUCHER_ID FROM VOUCHER_MASTER_TRANS VM INNER JOIN VOUCHER_TRANS VT ON VM.VOUCHER_ID = VT.VOUCHER_ID
                                    WHERE VM.STATUS = 1 LIMIT 1;";
                        break;
                    }
                case SQLCommand.VoucherMaster.IsCashBankEnbled:
                    {
                        query = @"select COUNT(IS_CASH_BANK_STATUS) AS STATUS from voucher_master_trans where voucher_type ='JN' and IS_CASH_BANK_STATUS=1";
                        break;
                    }
                case SQLCommand.VoucherMaster.IsVoucherMadeForCountry:
                    {
                        query = @"SELECT VM.VOUCHER_ID FROM VOUCHER_MASTER_TRANS VM INNER JOIN VOUCHER_TRANS VT ON VM.VOUCHER_ID = VT.VOUCHER_ID
                                    WHERE VM.CURRENCY_COUNTRY_ID = ?CURRENCY_COUNTRY_ID AND VM.STATUS = 1 LIMIT 1;";
                        break;
                    }
                case SQLCommand.VoucherMaster.IsReceiptPaymentVoucherMadeForOtherNatures:
                    {
                        query = @"SELECT VM.VOUCHER_ID FROM VOUCHER_MASTER_TRANS VM
                                    INNER JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VM.VOUCHER_ID
                                    INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID
                                    INNER JOIN MASTER_LEDGER_GROUP MLG ON MLG.GROUP_ID = ML.GROUP_ID
                                    WHERE VM.STATUS =1 AND VM.VOUCHER_TYPE =?VOUCHER_TYPE AND ML.GROUP_ID NOT IN (12, 13, 14) AND MLG.NATURE_ID NOT IN (?NATURE_ID) LIMIT 1;";
                        break;
                    }
                case SQLCommand.VoucherMaster.DeleteVoucherReferenceNo:
                    {
                        query = "UPDATE VOUCHER_TRANS SET REFERENCE_NUMBER = NULL WHERE VOUCHER_ID =?VOUCHER_ID";
                        break;
                    }

                case SQLCommand.VoucherMaster.ChangeCancelledVoucherStatus:
                    {
                        query = "UPDATE  VOUCHER_MASTER_TRANS SET VOUCHER_NO=NULL, STATUS=1 WHERE VOUCHER_ID=?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchRandPVoucherAgainstJournalInvoiceByVoucherId:
                    {
                        query = @"SELECT GIN.GST_INVOICE_ID, VM.VOUCHER_ID, VM.VOUCHER_DATE, VT.AMOUNT, GIN.GST_INVOICE_ID,
                                    GIN.BOOKING_VOUCHER_ID, GIN.GST_VENDOR_INVOICE_NO, GIN.TOTAL_AMOUNT
                                    FROM GST_INVOICE_MASTER GIN
                                    LEFT JOIN VOUCHER_GST_INVOICE VGST ON VGST.GST_INVOICE_ID = GIN.GST_INVOICE_ID
                                    LEFT JOIN VOUCHER_MASTER_TRANS VM ON VM.VOUCHER_ID = VGST.VOUCHER_ID
                                    LEFT JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VM.VOUCHER_ID AND VT.TRANS_MODE= 'CR'
                                    WHERE GIN.BOOKING_VOUCHER_ID = ?VOUCHER_ID;";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchRandPVoucherAgainstJournalInvoiceByInvoiceId:
                    {
                        query = @"SELECT GIN.GST_INVOICE_ID, VM.VOUCHER_ID, VM.VOUCHER_DATE, VT.AMOUNT, GIN.GST_INVOICE_ID,
                                    GIN.BOOKING_VOUCHER_ID, GIN.GST_VENDOR_INVOICE_NO, GIN.TOTAL_AMOUNT
                                    FROM GST_INVOICE_MASTER GIN
                                    LEFT JOIN VOUCHER_GST_INVOICE VGST ON VGST.GST_INVOICE_ID = GIN.GST_INVOICE_ID
                                    LEFT JOIN VOUCHER_MASTER_TRANS VM ON VM.VOUCHER_ID = VGST.VOUCHER_ID
                                    LEFT JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VM.VOUCHER_ID AND VT.TRANS_MODE= 'CR'
                                    WHERE GIN.GST_INVOICE_ID = ?GST_INVOICE_ID;";
                        break;
                    }
                //chinna
                case SQLCommand.VoucherMaster.PhysicalDelete:
                    {
                        query = "DELETE VMT.*, VT.* FROM VOUCHER_MASTER_TRANS VMT INNER JOIN VOUCHER_TRANS VT ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                " WHERE VMT.VOUCHER_ID = ?VOUCHER_ID";
                        break;

                    }
                case SQLCommand.VoucherMaster.DeletePhysicalCostCentreTrans:
                    {
                        query = "DELETE FROM VOUCHER_CC_TRANS WHERE VOUCHER_ID= ?VOUCHER_ID ";
                        break;
                    }
                case SQLCommand.VoucherMaster.ValidateDeletedVoucher:
                    {
                        query = " SELECT VMT.VOUCHER_ID, UI.USER_NAME\n" +
                                "  FROM VOUCHER_MASTER_TRANS VMT\n" +
                                " INNER JOIN USER_INFO UI\n" +
                                "    ON UI.USER_ID = VMT.CREATED_BY\n" +
                                " WHERE VMT.VOUCHER_ID = ?VOUCHER_ID\n" +
                                "   AND VMT.STATUS = 0";
                        break;
                    }
                case SQLCommand.VoucherMaster.ValidateManagementCode:
                    {
                        query = "SELECT CLIENT_CODE FROM VOUCHER_MASTER_TRANS VMT WHERE VMT.VOUCHER_ID = ?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchVoucherMethod:
                    {

                        query = "SELECT PV.PROJECT_ID, MV.VOUCHER_NAME, MV.VOUCHER_TYPE, MV.VOUCHER_METHOD\n" +
                                "  FROM project_voucher PV\n" +
                                "  LEFT JOIN MASTER_VOUCHER MV\n" +
                                "    ON PV.VOUCHER_ID = MV.VOUCHER_ID\n" +
                                " WHERE PROJECT_ID = ?PROJECT_ID\n" +
                                "   AND MV.VOUCHER_TYPE = ?VOUCHER_TYPE ";

                        break;
                    }
                case SQLCommand.VoucherMaster.Fetch:
                    {
                        query = "SELECT VOUCHER_ID,VOUCHER_DATE, " +
                                "CONCAT(MP.PROJECT,CONCAT(' - ',MD.DIVISION)) AS 'PROJECT', " +
                                "VOUCHER_NO, CASE VOUCHER_TYPE WHEN 'RC' THEN 'Receipt' WHEN 'PY' THEN 'Payment'" +
                                "WHEN 'CN' THEN 'Contra' ELSE 'JOURNAL' END AS VOUCHERTYPE," +
                                "CASE VOUCHER_TYPE WHEN 'RC' THEN CONTRIBUTION_AMOUNT WHEN 'CN'  THEN CONTRIBUTION_AMOUNT ELSE '' END AS DEBIT, " +
                                "CASE VOUCHER_TYPE WHEN 'PY' THEN CONTRIBUTION_AMOUNT ELSE '' END AS CREDIT, " +
                                "NAME AS DONOR_NAME FROM VOUCHER_MASTER_TRANS AS VM " +
                                "INNER JOIN MASTER_PROJECT AS MP ON VM.PROJECT_ID=MP.PROJECT_ID " +
                                "INNER JOIN MASTER_DIVISION AS MD ON MP.DIVISION_ID=MD.DIVISION_ID " +
                                "INNER JOIN MASTER_DONAUD AS MAD ON VM.DONOR_ID=MAD.DONAUD_ID WHERE VOUCHER_TYPE IN('RC','PY','CN') AND  VM.PROJECT_ID=?PROJECT_ID AND MP.DELETE_FLAG<>1 ORDER BY VOUCHER_DATE,VOUCHER_ID ASC  ";// FIND_IN_SET(VOUCHER_TYPE,?VOUCHER_TYPE) >0 
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchAll:
                    {
                        query = "SELECT VOUCHER_ID,VOUCHER_DATE, " +
                                "CONCAT(MP.PROJECT,CONCAT(' - ',MD.DIVISION)) AS 'PROJECT'," +
                                "VOUCHER_NO, CASE VOUCHER_TYPE WHEN 'RC' THEN 'Receipt' WHEN 'PY' THEN 'Payment'" +
                                "WHEN 'CN' THEN 'Contra' ELSE 'JOURNAL' END AS VOUCHERTYPE," +
                                "CASE VOUCHER_TYPE WHEN 'RC' THEN CONTRIBUTION_AMOUNT WHEN 'CN'  THEN CONTRIBUTION_AMOUNT ELSE '' END AS DEBIT," +
                                "CASE VOUCHER_TYPE WHEN 'PY' THEN CONTRIBUTION_AMOUNT ELSE '' END AS CREDIT," +
                                "NAME AS DONOR_NAME FROM VOUCHER_MASTER_TRANS AS VM " +
                                "INNER JOIN MASTER_PROJECT AS MP ON VM.PROJECT_ID=MP.PROJECT_ID " +
                                "INNER JOIN MASTER_DIVISION AS MD ON MP.DIVISION_ID=MD.DIVISION_ID " +
                                "INNER JOIN MASTER_DONAUD AS MAD ON VM.DONOR_ID=MAD.DONAUD_ID ";

                        break;
                    }
                case SQLCommand.VoucherMaster.FetchJournalDetails:
                    {
                        query = "SELECT VMT.VOUCHER_ID,\n" +
                        "   TB.BOOKING_ID,\n" +
                        "  TB.EXPENSE_LEDGER_ID,\n" +
                        "  TB.DEDUCTEE_TYPE_ID,\n" +
                        "       FD.FD_ACCOUNT_ID,\n" +
                        "       VMT.VOUCHER_NO,\n" +
                        "       VMT.VOUCHER_DATE,VMT.VOUCHER_TYPE AS VOUCHERTYPE,\n" +
                        "       VMT.VOUCHER_SUB_TYPE,\n" +
                        "       T.DEBIT AS AMOUNT, VMT.PROJECT_ID,\n" +
                        "       CONCAT(VMT.NARRATION, IF(VMT.VOUCHER_SUB_TYPE='FD', CONCAT(' FD: ', FD.FD_ACCOUNT_NUMBER), '' )) AS NARRATION, MC.CURRENCY_NAME,\n" +
                        "       IF(VMT.VOUCHER_DEFINITION_ID<=4, CASE VMT.VOUCHER_TYPE\n" +
                        "         WHEN 'RC' THEN 'Receipt'\n" +
                        "         WHEN 'PY' THEN 'Payment'\n" +
                        "         WHEN 'CN' THEN 'Contra'\n" +
                        "         ELSE 'Journal' END, \n" +
                        "       MV.VOUCHER_NAME) AS VOUCHER_TYPE, IF(VMT.IS_CASH_BANK_STATUS=1,'Yes', 'No') AS IS_CASH_BANK_STATUS, VMT.VOUCHER_DEFINITION_ID, IF(VMT.AUTHORIZATION_STATUS=1, 'Authorized', 'Unauthorized') AS AUTHORIZATION_STATUS,\n" +
                        "       CONCAT(CONCAT(VMT.GST_VENDOR_INVOICE_NO, CONCAT( ' - ' , DATE_FORMAT(VMT.GST_VENDOR_INVOICE_DATE, '%d/%m/%Y'))), CONCAT(' - ', ASV.VENDOR)) AS VENDOR_GST_INVOICE,\n" +
                        "       VT.EXCHANGE_RATE, VT.LIVE_EXCHANGE_RATE\n" +
                        " FROM VOUCHER_MASTER_TRANS VMT\n" +
                        " INNER JOIN VOUCHER_TRANS VT ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                        " LEFT JOIN MASTER_COUNTRY MC ON MC.COUNTRY_ID = VMT.CURRENCY_COUNTRY_ID\n" +
                        " LEFT JOIN MASTER_VOUCHER AS MV ON MV.VOUCHER_ID = VMT.VOUCHER_DEFINITION_ID\n" +
                        " LEFT JOIN (SELECT VMT.VOUCHER_ID, IFNULL(SUM( (AMOUNT *IF(VMT.IS_MULTI_CURRENCY=1, VMT.EXCHANGE_RATE, 1)) ), 0) AS DEBIT\n" +
                        "               FROM VOUCHER_MASTER_TRANS VMT\n" +
                        "               INNER JOIN VOUCHER_TRANS VT ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                        "               WHERE VMT.PROJECT_ID = ?PROJECT_ID AND VMT.VOUCHER_DATE\n" +
                        "               BETWEEN ?DATE_STARTED AND ?DATE_CLOSED AND VMT.VOUCHER_TYPE = 'JN'\n" +
                        "                AND VT.TRANS_MODE = 'DR' GROUP BY VMT.VOUCHER_ID\n" +
                        "               ORDER BY VMT.VOUCHER_NO, VT.LEDGER_ID) AS T\n" +
                        "    ON VT.VOUCHER_ID = T.VOUCHER_ID\n" +
                        " LEFT JOIN (SELECT FDA.FD_ACCOUNT_ID, 0 AS VOUCHER_ID, RNO.FD_INTEREST_VOUCHER_ID, RNO.FD_VOUCHER_ID,\n" +
                        "          RNO.RENEWAL_DATE, IFNULL(RNO.RECEIPT_NO,'') AS RECEIPT_NO,\n" +
                        "          CONCAT(FDA.FD_ACCOUNT_NUMBER,  IF(IFNULL(RNO.RECEIPT_NO,'')='', '', CONCAT(' (R: ', IFNULL(RNO.RECEIPT_NO,''),')')) ) AS FD_ACCOUNT_NUMBER\n" +
                        "          FROM FD_ACCOUNT AS FDA\n" +
                        "          INNER JOIN (SELECT FR.FD_ACCOUNT_ID, FR.FD_INTEREST_VOUCHER_ID, FR.FD_VOUCHER_ID,FR.RENEWAL_DATE, \n" +
                        "                   SUBSTRING_INDEX(GROUP_CONCAT(FR1.RECEIPT_NO ORDER BY FR1.RENEWAL_DATE DESC SEPARATOR '|'), '|', 1) AS RECEIPT_NO \n" +
                        "                   FROM FD_RENEWAL AS FR\n" +
                        "                   LEFT JOIN (SELECT FR1.FD_ACCOUNT_ID, FR1.RENEWAL_DATE AS RENEWAL_DATE, IFNULL(FR1.RECEIPT_NO, '') AS RECEIPT_NO\n" +
                        "                       FROM FD_RENEWAL AS FR1\n" +
                        "                       INNER JOIN FD_ACCOUNT FD1 ON FD1.FD_ACCOUNT_ID = FR1.FD_ACCOUNT_ID\n" +
                        "                       WHERE FD1.STATUS =1 AND FD1.PROJECT_ID IN (?PROJECT_ID) AND FR1.STATUS =1 AND FR1.FD_TYPE = 'RN' AND FR1.RENEWAL_DATE<=?DATE_CLOSED) AS FR1\n" +
                        "                   ON FR1.FD_ACCOUNT_ID= FR.FD_ACCOUNT_ID AND FR1.RENEWAL_DATE<FR.RENEWAL_DATE\n" +
                        "                   WHERE STATUS =1 GROUP BY FR.FD_ACCOUNT_ID, FR.FD_RENEWAL_ID) AS RNO\n" + //FR1.RENEWAL_DATE
                        "           ON RNO.FD_ACCOUNT_ID = FDA.FD_ACCOUNT_ID WHERE FDA.STATUS=1 AND FDA.PROJECT_ID IN (?PROJECT_ID)\n" +
                        "           UNION ALL SELECT FDA.FD_ACCOUNT_ID, FDA.FD_VOUCHER_ID, 0 AS FD_INTEREST_VOUCHER_ID, 0 AS FD_VOUCHER_ID, FDA.INVESTMENT_DATE, '' AS RECEIPT_NO, \n" +
                        "           FDA.FD_ACCOUNT_NUMBER FROM FD_ACCOUNT AS FDA WHERE FDA.STATUS=1 AND FDA.PROJECT_ID IN (?PROJECT_ID)) AS FD\n" +
                        "    ON FD.VOUCHER_ID = VMT.VOUCHER_ID OR FD.FD_INTEREST_VOUCHER_ID = VMT.VOUCHER_ID OR FD.FD_VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        " LEFT JOIN TDS_BOOKING AS TB ON VMT.VOUCHER_ID=TB.VOUCHER_ID\n" +
                        " LEFT JOIN ASSET_STOCK_VENDOR ASV ON ASV.VENDOR_ID = VMT.GST_VENDOR_ID\n" +
                        " WHERE VMT.PROJECT_ID = ?PROJECT_ID AND VMT.VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                        "   AND VMT.VOUCHER_TYPE = 'JN' AND VMT.STATUS = 1\n" +
                        " GROUP BY VMT.VOUCHER_ID\n" +
                            //"ORDER BY VMT.VOUCHER_DATE, LENGTH(VMT.VOUCHER_NO), VMT.VOUCHER_NO ASC;"; //" ORDER BY VMT.VOUCHER_NO, VT.LEDGER_ID"; //On 28/04/2021, to have proper Voucher Entry Order
                        "ORDER BY VMT.VOUCHER_DATE, VOUCHER_ID, LENGTH(VMT.VOUCHER_NO), VMT.VOUCHER_NO ASC;";


                        break;

                    }
                case SQLCommand.VoucherMaster.FetchJournalTransDetails:
                    {
                        query = "SELECT VMT.VOUCHER_ID, \n" +
                        "       ML.LEDGER_NAME, \n" +
                        "       CASE\n" +
                        "         WHEN VT.TRANS_MODE = 'DR' THEN\n" +
                        "          IFNULL(AMOUNT, 0)\n" +
                        "       END AS DEBIT ,\n" +
                        "       CASE\n" +
                        "         WHEN VT.TRANS_MODE = 'CR' THEN\n" +
                        "          IFNULL(AMOUNT, 0)\n" +
                        "       END AS 'CREDIT' \n" +
                        "  FROM VOUCHER_MASTER_TRANS VMT\n" +
                        " INNER JOIN VOUCHER_TRANS VT\n" +
                        "    ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                        " INNER JOIN MASTER_LEDGER ML\n" +
                        "    ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                        " WHERE \n" +
                        "  FIND_IN_SET(VMT.VOUCHER_ID ,?VOUCHER_ID)\n" +
                            //  "   AND VMT.VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                        "   AND VMT.VOUCHER_TYPE = 'JN' ORDER BY VMT.VOUCHER_NO, VT.SEQUENCE_NO"; //VT.LEDGER_ID, 
                        break;

                    }

                case SQLCommand.VoucherMaster.FetchMasterDetails:
                    {
                        //Added by Salamon VM.CLIENT_REFERENCE_ID
                        query = "SELECT VM.VOUCHER_ID,\n" +
                              "       CONCAT( IF(RCPYCN.NARRATION IS NULL OR RCPYCN.NARRATION='', VM.NARRATION, RCPYCN.NARRATION),\n" +
                              "       (CASE WHEN VM.VOUCHER_SUB_TYPE = 'FD' THEN CONCAT(' FD: ', FD.FD_ACCOUNT_NUMBER) ELSE '' END)) AS NARRATION, MC.CURRENCY_NAME,\n" +
                            //"       SUBSTRING_INDEX(VM.CLIENT_CODE, '-', -1) CLIENT_CODE,\n" +
                              "       CONCAT(SUBSTRING_INDEX(VM.CLIENT_CODE, '-', -1),' - ',VM.CLIENT_MODE) AS CLIENT_CODE,\n" +
                              "       FD.FD_ACCOUNT_ID,\n" +
                              "       RCPYCN.LEDGER_NAME AS LEDGER_NAME,\n" +
                              "       CASHBANK.LEDGER_NAME AS CASHBANK,REFNO.CHEQUE_NO,\n" +
                              "       VOUCHER_DATE,\n" +
                              "       VT.LEDGER_ID,\n" +
                              "       MP.PROJECT_ID,\n" +
                              "       CONCAT(MP.PROJECT, CONCAT(' - ', MD.DIVISION)) AS 'PROJECT',\n" +
                              "       VOUCHER_NO,\n" +
                              "       VOUCHER_SUB_TYPE, VM.CONTRIBUTION_AMOUNT, VM.EXCHANGE_RATE, VM.ACTUAL_AMOUNT,\n" +
                              "       IF(VM.VOUCHER_TYPE = 'PY', IF(CASHBANK.AMOUNT=0, RCPYCN.AMOUNT_DR, CASHBANK.AMOUNT) , 0) AS DEBIT_AMOUNT,\n" + //05/06/2017 changed '' to 0 (for number)
                              "       IF(VM.VOUCHER_TYPE = 'RC' OR VM.VOUCHER_TYPE = 'CN',\n" +
                              "          IF(CASHBANK.AMOUNT = 0 AND VM.VOUCHER_TYPE = 'RC', RCPYCN.AMOUNT_CR, CASHBANK.AMOUNT),\n" +
                              "          0) AS CREDIT_AMOUNT,\n" +                                         //05/06/2017 changed '' to 0 (for number)         
                              "       CASE VM.VOUCHER_TYPE\n" +
                              "         WHEN 'RC' THEN\n" +
                              "          'Receipt'\n" +
                              "         WHEN 'PY' THEN\n" +
                              "          'Payment'\n" +
                              "         WHEN 'CN' THEN\n" +
                              "          'Contra'\n" +
                              "         ELSE\n" +
                              "          'JOURNAL'\n" +
                              "       END AS VOUCHERTYPE,\n" +
                            //"       CONCAT(CASE VM.VOUCHER_TYPE\n" +
                            //"         WHEN 'RC' THEN\n" +
                            //"          'Receipt'\n" +
                            //"         WHEN 'PY' THEN\n" +
                            //"          'Payment'\n" +
                            //"         WHEN 'CN' THEN\n" +
                            //"          'Contra'\n" +
                            //"         ELSE\n" +
                            //"          'JOURNAL'\n" +
                            //"       END, IF(VM.VOUCHER_DEFINITION_ID<=4, '', CONCAT('-', MV.VOUCHER_NAME))) AS VOUCHER_TYPE,\n" +
                              "       IF(VM.VOUCHER_DEFINITION_ID<=4, CASE VM.VOUCHER_TYPE\n" +
                              "         WHEN 'RC' THEN 'Receipt'\n" +
                              "         WHEN 'PY' THEN 'Payment'\n" +
                              "         WHEN 'CN' THEN 'Contra'\n" +
                              "         ELSE 'Journal' END, \n" +
                              "       MV.VOUCHER_NAME) AS VOUCHER_TYPE, VM.VOUCHER_DEFINITION_ID,\n" +
                              "       MAD.NAME AS DONOR_NAME,\n" +
                              "       REGISTRATION_TYPE,\n" +
                              "       REGISTER_NO,\n" +
                              "       REFERRED_STAFF,\n" +
                              "       CASE VM.VOUCHER_TYPE\n" +
                              "         WHEN 'RC' THEN\n" +
                              "          CONTRIBUTION_AMOUNT\n" +
                              "         WHEN 'CN' THEN\n" +
                              "          CONTRIBUTION_AMOUNT\n" +
                              "         ELSE\n" +
                              "          ''\n" +
                              "       END AS DEBIT,\n" +
                              "       CASE VM.VOUCHER_TYPE\n" +
                              "         WHEN 'PY' THEN\n" +
                              "          CONTRIBUTION_AMOUNT\n" +
                              "         ELSE\n" +
                              "          ''\n" +
                              "       END AS CREDIT,\n" +
                              "       NAME_ADDRESS, PAN_NUMBER, GST_NUMBER,\n" +
                              " CONCAT(CONCAT(VM.GST_VENDOR_INVOICE_NO, CONCAT( ' - ' , DATE_FORMAT(VM.GST_VENDOR_INVOICE_DATE, '%d/%m/%Y'))), CONCAT(' - ', ASV.VENDOR)) AS VENDOR_GST_INVOICE,\n" +
                              "       CASE\n" +
                              "         WHEN VM.CONTRIBUTION_TYPE = 'F' THEN\n" +
                              "          'First'\n" +
                              "         ELSE\n" +
                              "          CASE\n" +
                              "            WHEN VM.CONTRIBUTION_TYPE = 'S' THEN\n" +
                              "             'Subsquent'\n" +
                              "            ELSE\n" +
                              "             ''\n" +
                              "          END\n" +
                              "       END AS RECEIPT_TYPE,\n" +
                              " FC_PURPOSE, FD.FD_ACCOUNT_NUMBER, IF(VM.AUTHORIZATION_STATUS=1, 'Authorized', 'Unauthorized') AS AUTHORIZATION_STATUS\n" +
                              "  FROM VOUCHER_MASTER_TRANS AS VM\n" +
                              " INNER JOIN MASTER_PROJECT AS MP ON VM.PROJECT_ID = MP.PROJECT_ID\n" +
                              " INNER JOIN MASTER_DIVISION AS MD ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                              " LEFT JOIN MASTER_COUNTRY MC ON MC.COUNTRY_ID = VM.CURRENCY_COUNTRY_ID\n" +
                              " LEFT JOIN MASTER_DONAUD AS MAD ON VM.DONOR_ID = MAD.DONAUD_ID\n" +
                              " LEFT JOIN MASTER_DONAUD_REG_TYPE MDRT ON MAD.REGISTRATION_TYPE_ID =MDRT.REGISTRATION_TYPE_ID\n" +
                              " LEFT JOIN MASTER_CONTRIBUTION_HEAD AS MCH ON VM.PURPOSE_ID = MCH.CONTRIBUTION_ID\n" +
                              " INNER JOIN VOUCHER_TRANS VT ON VM.VOUCHER_ID = VT.VOUCHER_ID\n" +
                              " LEFT JOIN (SELECT FDA.FD_ACCOUNT_ID, 0 AS VOUCHER_ID, RNO.FD_INTEREST_VOUCHER_ID, RNO.FD_VOUCHER_ID,\n" +
                              "          RNO.RENEWAL_DATE, IFNULL(RNO.RECEIPT_NO,'') AS RECEIPT_NO,\n" +
                              "          CONCAT(FDA.FD_ACCOUNT_NUMBER,  IF(IFNULL(RNO.RECEIPT_NO,'')='', '', CONCAT(' (R: ', IFNULL(RNO.RECEIPT_NO,''),')')) ) AS FD_ACCOUNT_NUMBER\n" +
                              "          FROM FD_ACCOUNT AS FDA\n" +
                              "          INNER JOIN (SELECT FR.FD_ACCOUNT_ID, FR.FD_INTEREST_VOUCHER_ID, FR.FD_VOUCHER_ID,FR.RENEWAL_DATE, \n" +
                              "                   SUBSTRING_INDEX(GROUP_CONCAT(FR1.RECEIPT_NO ORDER BY FR1.RENEWAL_DATE DESC SEPARATOR '|'), '|', 1) AS RECEIPT_NO \n" +
                              "                   FROM FD_RENEWAL AS FR\n" +
                              "                   LEFT JOIN (SELECT FR1.FD_ACCOUNT_ID, FR1.RENEWAL_DATE AS RENEWAL_DATE, IFNULL(FR1.RECEIPT_NO, '') AS RECEIPT_NO\n" +
                              "                       FROM FD_RENEWAL AS FR1\n" +
                              "                       INNER JOIN FD_ACCOUNT FD1 ON FD1.FD_ACCOUNT_ID = FR1.FD_ACCOUNT_ID\n" +
                              "                       WHERE FD1.STATUS =1 AND FD1.PROJECT_ID IN (?PROJECT_ID) AND FR1.STATUS =1 AND FR1.FD_TYPE = 'RN' AND FR1.RENEWAL_DATE<=?DATE_CLOSED) AS FR1\n" +
                              "                   ON FR1.FD_ACCOUNT_ID= FR.FD_ACCOUNT_ID AND FR1.RENEWAL_DATE<FR.RENEWAL_DATE\n" +
                              "                   WHERE STATUS =1 GROUP BY FR.FD_ACCOUNT_ID, FR.FD_RENEWAL_ID) AS RNO\n" + //FR1.RENEWAL_DATE
                              "           ON RNO.FD_ACCOUNT_ID = FDA.FD_ACCOUNT_ID WHERE FDA.STATUS=1 AND FDA.PROJECT_ID IN (?PROJECT_ID)\n" +
                              "           UNION ALL SELECT FDA.FD_ACCOUNT_ID, FDA.FD_VOUCHER_ID, 0 AS FD_INTEREST_VOUCHER_ID, 0 AS FD_VOUCHER_ID, FDA.INVESTMENT_DATE, '' AS RECEIPT_NO, \n" +
                              "           FDA.FD_ACCOUNT_NUMBER FROM FD_ACCOUNT AS FDA WHERE FDA.STATUS=1 AND FDA.PROJECT_ID IN (?PROJECT_ID)) AS FD\n" +
                              "    ON FD.VOUCHER_ID = VM.VOUCHER_ID OR FD.FD_INTEREST_VOUCHER_ID = VM.VOUCHER_ID OR FD.FD_VOUCHER_ID = VM.VOUCHER_ID\n" +
                              "  LEFT JOIN MASTER_VOUCHER AS MV ON MV.VOUCHER_ID = VM.VOUCHER_DEFINITION_ID\n" +
                              "\n" +
                              "  LEFT JOIN (SELECT T.VOUCHER_ID, T.LEDGER_NAME, T.NARRATION, SUM(T.AMOUNT) AS AMOUNT,\n" +
                              "               SUM(IF(T.TRANS_MODE = 'CR', T.AMOUNT, 0)) AS AMOUNT_CR, SUM(IF(T.TRANS_MODE = 'DR', T.AMOUNT, 0)) AS AMOUNT_DR\n" +
                              "               FROM (SELECT VT.VOUCHER_ID,\n" +
                              "                            CASE\n" +
                              "                              WHEN ML.LEDGER_SUB_TYPE = 'BK' THEN\n" +
                              "                               CONCAT(CONCAT(ML.LEDGER_NAME, ' - '),\n" +
                              "                                      CONCAT(MB.BANK, ' - '),\n" +
                              "                                      MB.BRANCH)\n" +
                              "                              ELSE\n" +
                              "                               CONCAT( IF(ML.LEDGER_CODE ='' OR ML.LEDGER_CODE IS NULL, '', CONCAT( ML.LEDGER_CODE,  '  ')),  ML.LEDGER_NAME)\n" +
                              "                            END AS LEDGER_NAME,\n" +
                              "                            VT.TRANS_MODE,\n" +
                              "                            IF(VM.IS_MULTI_CURRENCY=1, VM.ACTUAL_AMOUNT, VT.AMOUNT) AS AMOUNT, VT.NARRATION\n" +
                              "                       FROM VOUCHER_TRANS VT\n" +
                              "                      INNER JOIN VOUCHER_MASTER_TRANS VM ON VT.VOUCHER_ID = VM.VOUCHER_ID\n" +
                              "                       LEFT JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                              "                       LEFT JOIN MASTER_LEDGER_GROUP MLG ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                              "                       LEFT JOIN MASTER_BANK_ACCOUNT MBA ON MBA.LEDGER_ID = ML.LEDGER_ID\n" +
                              "                       LEFT JOIN MASTER_BANK MB ON MB.BANK_ID = MBA.BANK_ID\n" +
                              "                      WHERE VM.PROJECT_ID = ?PROJECT_ID\n" +
                              "                        AND VM.VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                              "                        AND IF(VM.VOUCHER_TYPE = 'CN' OR VM.VOUCHER_TYPE = 'RC',\n" + //On 11/04/2024, Temp, to show properp FD intrest against TDS
                              "                               VT.TRANS_MODE = 'CR',\n" +
                              "                               ML.GROUP_ID NOT IN (12, 13))\n" +
                              "                      ORDER BY VT.VOUCHER_ID, VT.SEQUENCE_NO) AS T\n" +
                              "              GROUP BY T.VOUCHER_ID) AS RCPYCN ON RCPYCN.VOUCHER_ID = VT.VOUCHER_ID\n" +
                              "\n" +
                              "  LEFT JOIN (SELECT T.VOUCHER_ID,\n" +
                              "                    T.LEDGER_NAME,\n" +
                              "                    SUM(T.AMOUNT) AS AMOUNT,\n" +
                              "                    T.TRANS_MODE\n" +
                              "               FROM (SELECT VT.VOUCHER_ID,\n" +
                              "                            CASE\n" +
                              "                              WHEN ML.LEDGER_SUB_TYPE = 'BK' THEN\n" +
                              "                               CONCAT(CONCAT(ML.LEDGER_NAME, ' - '),\n" +
                              "                                      CONCAT(MB.BANK, ' - '),\n" +
                              "                                      MB.BRANCH)\n" +
                              "                              ELSE\n" +
                              "                               ML.LEDGER_NAME\n" +
                              "                            END AS LEDGER_NAME,\n" +
                              "                            IF(VM.IS_MULTI_CURRENCY=1, VM.ACTUAL_AMOUNT, VT.AMOUNT) AS AMOUNT,\n" +
                              "                            TRANS_MODE\n" +
                              "                      FROM VOUCHER_TRANS VT\n" +
                              "                      INNER JOIN VOUCHER_MASTER_TRANS VM ON VT.VOUCHER_ID = VM.VOUCHER_ID\n" +
                              "                      LEFT JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                              "                      LEFT JOIN MASTER_LEDGER_GROUP MLG ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                              "                      LEFT JOIN MASTER_BANK_ACCOUNT MBA ON MBA.LEDGER_ID = ML.LEDGER_ID\n" +
                              "                      LEFT JOIN MASTER_BANK MB ON MB.BANK_ID = MBA.BANK_ID\n" +
                              "                      WHERE VM.PROJECT_ID = ?PROJECT_ID\n" +
                              "                        AND VM.VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                              "                        AND IF(VM.VOUCHER_TYPE = 'CN',\n" +
                              "                               VT.TRANS_MODE = 'DR' AND ML.GROUP_ID IN (12, 13, 14),\n" +
                              "                               ML.GROUP_ID IN (12, 13))\n" +
                              "                      ORDER BY VT.VOUCHER_ID, VT.SEQUENCE_NO) AS T\n" +
                              "              GROUP BY T.VOUCHER_ID) AS CASHBANK ON CASHBANK.VOUCHER_ID = VT.VOUCHER_ID\n" +
                              "\n" +
                              "  LEFT JOIN (SELECT T.VOUCHER_ID,\n" +
                              "                    T.LEDGER_NAME,\n" +
                              "                    SUM(T.AMOUNT) AS AMOUNT,IF(CHEQUE_NO = '', '',CONCAT(CHEQUE_NO,' - ',T.BANK,' - ',CONCAT(T.BRANCH))) AS CHEQUE_NO,\n" +
                              "                    T.TRANS_MODE\n" +
                              "               FROM (SELECT VT.VOUCHER_ID,\n" +
                              "                            CASE\n" +
                              "                              WHEN ML.LEDGER_SUB_TYPE = 'BK' THEN\n" +
                              "                               CONCAT(CONCAT(ML.LEDGER_NAME, ' - '),\n" +
                              "                                      CONCAT(MB.BANK, ' - '),\n" +
                              "                                      MB.BRANCH)\n" +
                              "                              ELSE\n" +
                              "                               ML.LEDGER_NAME\n" +
                              "                            END AS LEDGER_NAME,MB.BANK,BRANCH,\n" +
                              "                            VT.AMOUNT AS AMOUNT,CHEQUE_NO,\n" +
                              "                            TRANS_MODE\n" +
                              "                       FROM VOUCHER_TRANS VT\n" +
                              "                      INNER JOIN VOUCHER_MASTER_TRANS VM ON VT.VOUCHER_ID = VM.VOUCHER_ID\n" +
                              "                      LEFT JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                              "                      LEFT JOIN MASTER_LEDGER_GROUP MLG ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                              "                      LEFT JOIN MASTER_BANK_ACCOUNT MBA ON MBA.LEDGER_ID = ML.LEDGER_ID\n" +
                              "                      LEFT JOIN MASTER_BANK MB ON MB.BANK_ID = MBA.BANK_ID\n" +
                              "                      WHERE VM.PROJECT_ID = ?PROJECT_ID\n" +
                              "                        AND VM.VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                              "                        AND IF(VM.VOUCHER_TYPE = 'CN',\n" +
                              "                               VT.TRANS_MODE = 'DR',\n" +
                              "                               ML.GROUP_ID IN (12))\n" +
                              "                      ORDER BY VT.VOUCHER_ID, VT.SEQUENCE_NO) AS T\n" +
                              "              GROUP BY T.VOUCHER_ID) AS REFNO ON REFNO.VOUCHER_ID = VT.VOUCHER_ID\n" +
                              " LEFT JOIN ASSET_STOCK_VENDOR ASV ON ASV.VENDOR_ID = VM.GST_VENDOR_ID\n" +
                              "\n" +
                              " WHERE FIND_IN_SET(VM.VOUCHER_TYPE, ?VOUCHER_TYPE) > 0\n" +
                              "   AND VM.PROJECT_ID = ?PROJECT_ID\n" +
                              "   AND VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                              "   AND VM.STATUS = 1\n" +
                              " GROUP BY VT.VOUCHER_ID\n" +
                            //" ORDER BY VOUCHER_DATE, LENGTH(VOUCHER_NO), VOUCHER_NO ASC;"; //On 28/04/2021, to have proper Voucher Entry Order
                              " ORDER BY VOUCHER_DATE, LENGTH(VOUCHER_NO), VOUCHER_NO ASC, VOUCHER_ID;";

                        break;
                    }
                case SQLCommand.VoucherMaster.FetchVoucherAuditLogHistoryDetails:
                    {
                        query = "SELECT VAL.VOUCHER_ID, VAL.VOUCHER_DATE, VAL.VOUCHER_NO, VAL.VOUCHER_SUB_TYPE,\n" +
                                    "CASE VAL.VOUCHER_TYPE WHEN 'RC' THEN 'Receipt'\n" +
                                    "     WHEN 'PY' THEN 'Payment' WHEN 'CN' THEN 'Contra' ELSE 'JOURNAL' END AS VOUCHER_TYPE,\n" +
                                    "VAL.PROJECT_NAME AS PROJECT,\n" +
                                    "IFNULL(VAL.AMOUNT,0) AS AMOUNT, IFNULL(VAL.PREVIOUS_AMOUNT,0) AS PREVIOUS_AMOUNT,\n" +
                                    "VAL.MODIFIED_ON AS MODIFIED_ON,\n" +
                                    "VAL.MODIFIED_BY_NAME AS MODIFIED_BY_NAME,\n" +
                                    "VAL.PREVIOUS_MODIFIED_BY_NAME AS PREVIOUS_MODIFIED_BY_NAME,\n" +
                                    "VAL.ACTION AS AUDIT_ACTION, VAL.NUMBER_OF_REVISIONS, VAL.IS_AUDITOR_MODIFIED, IF(VAL.IS_AUDITOR_MODIFIED = 1, 'Yes', 'No') as AUDITOR_TRACK\n" +
                                    "FROM VOUCHER_MASTER_AUDIT_LOG AS VAL\n" +
                                    "LEFT JOIN VOUCHER_MASTER_TRANS VMT ON VMT.VOUCHER_ID = VAL.VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchMasterAuditLogHistory:
                    {
                        query = "SELECT VM.VOUCHER_ID, MP.PROJECT_ID, VOUCHER_DATE, VOUCHER_NO, VOUCHER_SUB_TYPE,\n" +
                              "       IF(RCPYCN.NARRATION IS NULL OR RCPYCN.NARRATION='', VM.NARRATION, RCPYCN.NARRATION) AS NARRATION,\n" +
                              "       RCPYCN.LEDGER_NAME AS LEDGER_NAME, CASHBANK.LEDGER_NAME AS CASHBANK, CASHBANK.CHEQUE_NO,\n" +
                              "       CONCAT(MP.PROJECT, CONCAT(' - ', MD.DIVISION)) AS 'PROJECT',\n" +
                              "       IF(VM.VOUCHER_TYPE = 'PY', CASHBANK.AMOUNT, IF(VM.VOUCHER_TYPE = 'JN', RCPYCN.AMOUNT,0)) AS DEBIT_AMOUNT,\n" +
                              "       IF(VM.VOUCHER_TYPE = 'RC' OR VM.VOUCHER_TYPE = 'CN', CASHBANK.AMOUNT,0) AS CREDIT_AMOUNT,\n" +
                              "       CASE VM.VOUCHER_TYPE\n" +
                              "         WHEN 'RC' THEN\n" +
                              "          'Receipt'\n" +
                              "         WHEN 'PY' THEN\n" +
                              "          'Payment'\n" +
                              "         WHEN 'CN' THEN\n" +
                              "          'Contra'\n" +
                              "         ELSE\n" +
                              "          'JOURNAL'\n" +
                              "       END AS VOUCHERTYPE,\n" +
                              "       IF(VM.VOUCHER_DEFINITION_ID<=4, CASE VM.VOUCHER_TYPE\n" +
                              "         WHEN 'RC' THEN 'Receipt'\n" +
                              "         WHEN 'PY' THEN 'Payment'\n" +
                              "         WHEN 'CN' THEN 'Contra'\n" +
                              "         ELSE 'Journal' END, \n" +
                              "     MV.VOUCHER_NAME) AS VOUCHER_TYPE, VM.VOUCHER_DEFINITION_ID, NAME_ADDRESS, \n" +
                              "     VM.CREATED_ON, VM.MODIFIED_ON, \n" +
                            //" IF(UC.USER_ID IS NULL, VM.CREATED_BY_NAME, UC.FIRSTNAME) AS CREATED_BY_NAME,\n" +
                            //" IF(UM.USER_ID IS NULL, VM.MODIFIED_BY_NAME, UM.FIRSTNAME) AS MODIFIED_BY_NAME,\n" +
                              " IF(CREATED_BY_NAME IS NULL OR CREATED_BY_NAME = '', IF(UC.USER_ID IS NULL, '', UC.FIRSTNAME), VM.CREATED_BY_NAME) AS CREATED_BY_NAME,\n" +
                              " IF(VM.MODIFIED_BY_NAME IS NULL OR VM.MODIFIED_BY_NAME = '', IF(UM.USER_ID IS NULL, '', UM.FIRSTNAME), VM.MODIFIED_BY_NAME) AS MODIFIED_BY_NAME,\n" +
                              " CASE WHEN VM.STATUS=0 THEN 'Deleted' WHEN VM.MODIFIED_BY>0 THEN 'Modified' ELSE 'Created' END AS AUDIT_ACTION, IF(VM.IS_AUDITOR_MODIFIED = 1, 'Yes', 'No') as AUDITOR_TRACK\n" +
                              " FROM VOUCHER_MASTER_TRANS AS VM\n" +
                              " INNER JOIN MASTER_PROJECT AS MP ON VM.PROJECT_ID = MP.PROJECT_ID\n" +
                              " INNER JOIN MASTER_DIVISION AS MD ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                              " LEFT JOIN MASTER_VOUCHER AS MV ON MV.VOUCHER_ID = VM.VOUCHER_DEFINITION_ID\n" +
                              "\n" +
                              "  LEFT JOIN (SELECT T.VOUCHER_ID, T.LEDGER_NAME, T.NARRATION, SUM(IF(TRANS_MODE='DR', T.AMOUNT, 0)) AS AMOUNT\n" +
                              "               FROM (SELECT VT.VOUCHER_ID,\n" +
                              "                            CASE\n" +
                              "                              WHEN ML.LEDGER_SUB_TYPE = 'BK' THEN\n" +
                              "                               CONCAT(CONCAT(ML.LEDGER_NAME, ' - '),\n" +
                              "                                      CONCAT(MB.BANK, ' - '),\n" +
                              "                                      MB.BRANCH)\n" +
                              "                              ELSE\n" +
                              "                               ML.LEDGER_NAME\n" +
                              "                            END AS LEDGER_NAME,\n" +
                              "                            VT.TRANS_MODE,\n" +
                              "                            VT.AMOUNT, VT.NARRATION\n" +
                              "                       FROM VOUCHER_TRANS VT\n" +
                              "                      INNER JOIN VOUCHER_MASTER_TRANS VM\n" +
                              "                         ON VT.VOUCHER_ID = VM.VOUCHER_ID\n" +
                              "                       LEFT JOIN MASTER_LEDGER ML\n" +
                              "                         ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                              "                       LEFT JOIN MASTER_LEDGER_GROUP MLG\n" +
                              "                         ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                              "                       LEFT JOIN MASTER_BANK_ACCOUNT MBA\n" +
                              "                         ON MBA.LEDGER_ID = ML.LEDGER_ID\n" +
                              "                       LEFT JOIN MASTER_BANK MB\n" +
                              "                         ON MB.BANK_ID = MBA.BANK_ID\n" +
                              "                      WHERE VM.PROJECT_ID = ?PROJECT_ID\n" +
                              "                        AND VM.VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                              "                        AND IF(VM.VOUCHER_TYPE = 'CN',\n" +
                              "                               VT.TRANS_MODE = 'CR',\n" +
                              "                               ML.GROUP_ID NOT IN (12, 13))\n" +
                              "                      ORDER BY VT.VOUCHER_ID, VT.SEQUENCE_NO) AS T\n" +
                              "              GROUP BY T.VOUCHER_ID) AS RCPYCN ON RCPYCN.VOUCHER_ID = VM.VOUCHER_ID\n" +
                              "\n" +
                              "  LEFT JOIN (SELECT T.VOUCHER_ID, T.LEDGER_NAME, SUM(T.AMOUNT) AS AMOUNT, T.CHEQUE_NO, T.TRANS_MODE\n" +
                              "               FROM (SELECT VT.VOUCHER_ID,\n" +
                              "                            CASE\n" +
                              "                              WHEN ML.LEDGER_SUB_TYPE = 'BK' THEN\n" +
                              "                               CONCAT(CONCAT(ML.LEDGER_NAME, ' - '),\n" +
                              "                                      CONCAT(MB.BANK, ' - '),\n" +
                              "                                      MB.BRANCH)\n" +
                              "                              ELSE\n" +
                              "                               ML.LEDGER_NAME\n" +
                              "                            END AS LEDGER_NAME, VT.AMOUNT AS AMOUNT, VT.CHEQUE_NO, TRANS_MODE\n" +
                              "                      FROM VOUCHER_TRANS VT\n" +
                              "                      INNER JOIN VOUCHER_MASTER_TRANS VM\n" +
                              "                         ON VT.VOUCHER_ID = VM.VOUCHER_ID\n" +
                              "                       LEFT JOIN MASTER_LEDGER ML\n" +
                              "                         ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                              "                       LEFT JOIN MASTER_LEDGER_GROUP MLG\n" +
                              "                         ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                              "                       LEFT JOIN MASTER_BANK_ACCOUNT MBA\n" +
                              "                         ON MBA.LEDGER_ID = ML.LEDGER_ID\n" +
                              "                       LEFT JOIN MASTER_BANK MB\n" +
                              "                         ON MB.BANK_ID = MBA.BANK_ID\n" +
                              "                      WHERE VM.PROJECT_ID = ?PROJECT_ID\n" +
                              "                        AND VM.VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                              "                        AND IF(VM.VOUCHER_TYPE = 'CN',\n" +
                              "                               VT.TRANS_MODE = 'DR',\n" +
                              "                               ML.GROUP_ID IN (12, 13))\n" +
                              "                      ORDER BY VT.VOUCHER_ID, VT.SEQUENCE_NO) AS T\n" +
                              "              GROUP BY T.VOUCHER_ID) AS CASHBANK ON CASHBANK.VOUCHER_ID = VM.VOUCHER_ID\n" +
                              "\n" +
                              " LEFT JOIN USER_INFO UC ON UC.USER_ID = VM.CREATED_BY\n" +   //Created By
                              " LEFT JOIN USER_INFO UM ON UM.USER_ID = VM.MODIFIED_BY\n" +  //Modified By
                              " WHERE FIND_IN_SET(VM.VOUCHER_TYPE, ?VOUCHER_TYPE) > 0 AND VM.PROJECT_ID = ?PROJECT_ID AND VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                              " ORDER BY VOUCHER_DATE, LENGTH(VOUCHER_NO), VOUCHER_NO ASC, VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchVouchersForAuthorization:
                    {
                        query = "SELECT VM.VOUCHER_ID, MP.PROJECT_ID, VM.VOUCHER_DATE, " +
                                 "CASE WHEN VM.STATUS = 0 THEN VM.PREVIOUS_VOUCHER_NO ELSE VM.VOUCHER_NO END AS VOUCHER_NO,  VM.VOUCHER_SUB_TYPE,\n" +
                                 "      IF(RCPYCN.NARRATION IS NULL OR RCPYCN.NARRATION='', VM.NARRATION, RCPYCN.NARRATION) AS NARRATION,\n" +
                                 "      RCPYCN.LEDGER_NAME AS LEDGER_NAME, CASHBANK.LEDGER_NAME AS CASH_BANK, CASHBANK.CHEQUE_NO,\n" +
                                 "      CONCAT(MP.PROJECT, CONCAT(' - ', MD.DIVISION)) AS 'PROJECT',\n" +
                                 "      IF(VM.VOUCHER_TYPE = 'PY', CASHBANK.AMOUNT, IF(VM.VOUCHER_TYPE = 'JN', RCPYCN.AMOUNT,0)) AS DEBIT,\n" +
                                 "      IF(VM.VOUCHER_TYPE = 'RC' OR VM.VOUCHER_TYPE = 'CN', CASHBANK.AMOUNT,0) AS CREDIT, IF(VM.VOUCHER_TYPE = 'JN', RCPYCN.AMOUNT, CASHBANK.AMOUNT) AS AMOUNT,\n" +
                                 "      CASE VM.VOUCHER_TYPE WHEN 'RC' THEN 'Receipt' WHEN 'PY' THEN 'Payment' WHEN 'CN' THEN 'Contra' ELSE 'JOURNAL' END AS VOUCHERTYPE,\n" +
                                 "      IF(VM.VOUCHER_DEFINITION_ID<=4, CASE VM.VOUCHER_TYPE\n" +
                                 "        WHEN 'RC' THEN 'Receipt' WHEN 'PY' THEN 'Payment' WHEN 'CN' THEN 'Contra' ELSE 'Journal' END, MV.VOUCHER_NAME) AS VOUCHER_TYPE,\n" +
                                 "        VM.VOUCHER_DEFINITION_ID, NAME_ADDRESS, VM.CREATED_ON,\n" +
                                 "        IF((VM.STATUS=1 AND VM.MODIFIED_BY >0 AND VM.MODIFIED_BY_NAME IS NOT NULL AND VM.MODIFIED_BY_NAME <>'' AND VM.MODIFIED_ON IS NOT NULL), VM.MODIFIED_ON, NULL) AS MODIFIED_ON,\n" +
                                 " IF(CREATED_BY_NAME IS NULL OR CREATED_BY_NAME = '', IF(UC.USER_ID IS NULL, '', UC.FIRSTNAME), VM.CREATED_BY_NAME) AS CREATED_BY_NAME,\n" +
                                 " IF(VM.MODIFIED_BY_NAME IS NULL OR VM.MODIFIED_BY_NAME = '', IF(UM.USER_ID IS NULL, '', UM.FIRSTNAME), VM.MODIFIED_BY_NAME) AS MODIFIED_BY_NAME\n" +
                                 "FROM VOUCHER_MASTER_TRANS AS VM\n" +
                                 "INNER JOIN MASTER_PROJECT AS MP ON VM.PROJECT_ID = MP.PROJECT_ID\n" +
                                 "INNER JOIN MASTER_DIVISION AS MD ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                                 "LEFT JOIN MASTER_VOUCHER AS MV ON MV.VOUCHER_ID = VM.VOUCHER_DEFINITION_ID\n" +
                                 "LEFT JOIN (SELECT T.VOUCHER_ID, T.LEDGER_NAME, T.NARRATION, SUM(IF(T.TRANS_MODE='DR', T.AMOUNT,0)) AS AMOUNT,\n" +
                                 "             SUM(T.GST) AS GST, SUM(T.CGST) AS CGST, SUM(T.SGST) AS SGST\n" +
                                 "             FROM (SELECT VT.VOUCHER_ID,\n" +
                                 "                          CASE WHEN ML.LEDGER_SUB_TYPE = 'BK' THEN CONCAT(CONCAT(ML.LEDGER_NAME, ' - '), CONCAT(MB.BANK, ' - '), MB.BRANCH)\n" +
                                 "                            ELSE ML.LEDGER_NAME END AS LEDGER_NAME, VT.TRANS_MODE, VT.AMOUNT, VT.GST, VT.CGST, VT.SGST, VT.NARRATION\n" +
                                 "                     FROM VOUCHER_TRANS VT\n" +
                                 "                     INNER JOIN VOUCHER_MASTER_TRANS VM ON VT.VOUCHER_ID = VM.VOUCHER_ID\n" +
                                 "                     LEFT JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                                 "                     LEFT JOIN MASTER_LEDGER_GROUP MLG ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                                 "                     LEFT JOIN MASTER_BANK_ACCOUNT MBA ON MBA.LEDGER_ID = ML.LEDGER_ID\n" +
                                 "                     LEFT JOIN MASTER_BANK MB ON MB.BANK_ID = MBA.BANK_ID\n" +
                                 "                     WHERE VM.PROJECT_ID IN (?PROJECT_ID) AND VM.VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                                 "                      AND IF(VM.VOUCHER_TYPE = 'CN', VT.TRANS_MODE = 'CR', ML.GROUP_ID NOT IN (12, 13))\n" +
                                 "                    ORDER BY VT.VOUCHER_ID, VT.SEQUENCE_NO) AS T\n" +
                                 "            GROUP BY T.VOUCHER_ID) AS RCPYCN ON RCPYCN.VOUCHER_ID = VM.VOUCHER_ID\n" +
                                 "\n" +
                                 "LEFT JOIN (SELECT T.VOUCHER_ID, T.LEDGER_NAME, SUM(T.AMOUNT) AS AMOUNT, T.CHEQUE_NO, T.TRANS_MODE\n" +
                                 "             FROM (SELECT VT.VOUCHER_ID,\n" +
                                 "                          CASE WHEN ML.LEDGER_SUB_TYPE = 'BK' THEN CONCAT(CONCAT(ML.LEDGER_NAME, ' - '), CONCAT(MB.BANK, ' - '), MB.BRANCH)\n" +
                                 "                             ELSE ML.LEDGER_NAME END AS LEDGER_NAME,\n" +
                                 "                          VT.AMOUNT AS AMOUNT, VT.CHEQUE_NO, TRANS_MODE\n" +
                                 "                    FROM VOUCHER_TRANS VT\n" +
                                 "                    INNER JOIN VOUCHER_MASTER_TRANS VM ON VT.VOUCHER_ID = VM.VOUCHER_ID\n" +
                                 "                    LEFT JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                                 "                    LEFT JOIN MASTER_LEDGER_GROUP MLG ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                                 "                    LEFT JOIN MASTER_BANK_ACCOUNT MBA ON MBA.LEDGER_ID = ML.LEDGER_ID\n" +
                                 "                    LEFT JOIN MASTER_BANK MB ON MB.BANK_ID = MBA.BANK_ID\n" +
                                 "                    WHERE VM.PROJECT_ID IN (?PROJECT_ID) AND VM.VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                                 "                      AND IF(VM.VOUCHER_TYPE = 'CN', VT.TRANS_MODE = 'DR', ML.GROUP_ID IN (12, 13))\n" +
                                 "                    ORDER BY VT.VOUCHER_ID, VT.SEQUENCE_NO) AS T\n" +
                                 "            GROUP BY T.VOUCHER_ID) AS CASHBANK ON CASHBANK.VOUCHER_ID = VM.VOUCHER_ID\n" +
                                 "\n" +
                                 "LEFT JOIN USER_INFO UC ON UC.USER_ID = VM.CREATED_BY\n" +
                                 "LEFT JOIN USER_INFO UM ON UM.USER_ID = VM.MODIFIED_BY\n" +
                                 "WHERE VM.STATUS=1 AND VM.AUTHORIZATION_STATUS= 0 AND VM.PROJECT_ID IN (?PROJECT_ID) AND VM.VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED {AND VM.VOUCHER_DEFINITION_ID IN (?VOUCHER_DEFINITION_ID)}\n" +
                                 "ORDER BY VM.VOUCHER_DATE, LENGTH(VM.VOUCHER_NO), VM.VOUCHER_NO ASC, VM.VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.InsertVoucherAuditLogHistory:
                    {
                        query = "INSERT INTO VOUCHER_MASTER_AUDIT_LOG\n" +
                               "(VOUCHER_ID, VOUCHER_DATE, PROJECT_NAME, VOUCHER_NO, VOUCHER_TYPE, VOUCHER_SUB_TYPE, AMOUNT, PREVIOUS_AMOUNT,\n" +
                               "IS_AUDITOR_MODIFIED, MODIFIED_ON, MODIFIED_BY, MODIFIED_BY_NAME, PREVIOUS_MODIFIED_BY, PREVIOUS_MODIFIED_BY_NAME, ACTION, NUMBER_OF_REVISIONS)\n" +
                               "VALUES (?VOUCHER_ID, ?VOUCHER_DATE, ?PROJECT, ?VOUCHER_NO, ?VOUCHER_TYPE, ?VOUCHER_SUB_TYPE, ?AMOUNT, ?PREVIOUS_AMOUNT,\n" +
                                       "?IS_AUDITOR_MODIFIED, NOW(), ?MODIFIED_BY, ?1MODIFIED_BY_NAME, ?PREVIOUS_MODIFIED_BY, ?1PREVIOUS_MODIFIED_BY_NAME, ?ACTION, 1)";
                        break;
                    }
                case SQLCommand.VoucherMaster.UpdateVoucherAuditLogHistory:
                    {
                        query = "UPDATE VOUCHER_MASTER_AUDIT_LOG SET VOUCHER_DATE = ?VOUCHER_DATE, PROJECT_NAME= ?PROJECT," +
                                "VOUCHER_NO =?VOUCHER_NO, VOUCHER_TYPE=?VOUCHER_TYPE, " +
                                "AMOUNT=?AMOUNT, PREVIOUS_AMOUNT = IF(?ACTION = '" + Utility.AuditAction.Deleted + "', PREVIOUS_AMOUNT ,?PREVIOUS_AMOUNT)," +
                                "MODIFIED_ON = NOW(), MODIFIED_BY_NAME = ?1MODIFIED_BY_NAME,\n" +
                                "PREVIOUS_MODIFIED_BY=?PREVIOUS_MODIFIED_BY, \n" +
                                "ACTION = ?ACTION, NUMBER_OF_REVISIONS=NUMBER_OF_REVISIONS+1\n" +
                                "WHERE VOUCHER_ID=?VOUCHER_ID AND MODIFIED_BY = ?MODIFIED_BY"; //AND IS_AUDITOR_MODIFIED = ?IS_AUDITOR_MODIFIED
                        //PREVIOUS_MODIFIED_BY_NAME = ?1PREVIOUS_MODIFIED_BY_NAME,

                        break;
                    }
                case SQLCommand.VoucherMaster.IsVoucherModifiedByAuditor:
                    {
                        query = "SELECT VM.VOUCHER_ID, VM.IS_AUDITOR_MODIFIED FROM VOUCHER_MASTER_TRANS VM WHERE VM.VOUCHER_ID = ?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchVoucherAuditUserDetails:
                    {
                        query = "SELECT VM.VOUCHER_ID, VM.IS_AUDITOR_MODIFIED, VM.CREATED_BY,  VM.MODIFIED_BY,\n" +
                              "IF(CREATED_BY_NAME IS NULL OR CREATED_BY_NAME = '', IF(UC.USER_ID IS NULL, '', UC.FIRSTNAME), VM.CREATED_BY_NAME) AS CREATED_BY_NAME,\n" +
                              "IF(VM.MODIFIED_BY_NAME IS NULL OR VM.MODIFIED_BY_NAME = '', IF(UM.USER_ID IS NULL, '', UM.FIRSTNAME), VM.MODIFIED_BY_NAME) AS MODIFIED_BY_NAME\n" +
                              "FROM VOUCHER_MASTER_TRANS VM\n" +
                              "LEFT JOIN USER_INFO UC ON UC.USER_ID = VM.CREATED_BY\n" +   //Created By
                              "LEFT JOIN USER_INFO UM ON UM.USER_ID = VM.MODIFIED_BY\n" +  //Modified By
                              "WHERE VM.VOUCHER_ID = ?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.UpdateAuditorModifiedFlag:
                    {
                        query = "UPDATE VOUCHER_MASTER_TRANS SET IS_AUDITOR_MODIFIED=1 WHERE VOUCHER_ID=?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.UpdateVoucherFileDetailsByVoucher:
                    {
                        query = @"INSERT INTO VOUCHER_FILES (VOUCHER_ID, SEQUENCE_NO, FILE_NAME, ACTUAL_FILE_NAME, REMARK, BRANCH_ID, LOCATION_ID)
                                 VALUES(?VOUCHER_ID, ?SEQUENCE_NO, ?FILE_NAME, ?ACTUAL_FILE_NAME, ?REMARK, ?BRANCH_ID, ?LOCATION_ID)";
                        break;
                    }
                case SQLCommand.VoucherMaster.DeleteVoucherFileDetailsByVoucher:
                    {
                        query = @"DELETE FROM VOUCHER_FILES WHERE VOUCHER_ID = ?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchVoucherFileDetailsByVoucher:
                    {
                        query = @"SELECT VOUCHER_ID, SEQUENCE_NO, FILE_NAME, ACTUAL_FILE_NAME, REMARK
                                     FROM VOUCHER_FILES WHERE VOUCHER_ID = ?VOUCHER_ID ORDER BY SEQUENCE_NO";
                        break;
                    }
                case SQLCommand.VoucherMaster.IsExistsVoucherFiles:
                    {
                        query = @"SELECT VOUCHER_ID, SEQUENCE_NO, FILE_NAME, ACTUAL_FILE_NAME, REMARK FROM VOUCHER_FILES LIMIT 1";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchVoucherPreviousDetailsForAuditLog:
                    {
                        query = "SELECT VM.VOUCHER_ID, VM.VOUCHER_DATE, VM.PROJECT_ID, MP.PROJECT, VM.VOUCHER_NO, VT.AMOUNT, VM.VOUCHER_TYPE, VM.VOUCHER_SUB_TYPE,\n" +
                                    "VM.IS_AUDITOR_MODIFIED, VM.CREATED_BY,  VM.MODIFIED_BY,\n" +
                                    "IF(CREATED_BY_NAME IS NULL OR CREATED_BY_NAME = '', IF(UC.USER_ID IS NULL, '', UC.FIRSTNAME), VM.CREATED_BY_NAME) AS CREATED_BY_NAME,\n" +
                                    "IF(VM.MODIFIED_BY_NAME IS NULL OR VM.MODIFIED_BY_NAME = '', IF(UM.USER_ID IS NULL, '', UM.FIRSTNAME), VM.MODIFIED_BY_NAME) AS MODIFIED_BY_NAME\n" +
                                    "FROM VOUCHER_MASTER_TRANS VM\n" +
                                    "INNER JOIN (SELECT VOUCHER_ID, SUM(AMOUNT) AS AMOUNT FROM VOUCHER_TRANS\n" +
                                           "WHERE VOUCHER_ID = ?VOUCHER_ID AND TRANS_MODE = 'CR') AS VT ON VT.VOUCHER_ID = VM.VOUCHER_ID\n" +
                                    "INNER JOIN MASTER_PROJECT MP ON MP.PROJECT_ID = VM.PROJECT_ID\n" +
                                    "LEFT JOIN USER_INFO UC ON UC.USER_ID = VM.CREATED_BY\n" +   //Created By
                                    "LEFT JOIN USER_INFO UM ON UM.USER_ID = VM.MODIFIED_BY\n" +  //Modified By
                                    "WHERE VM.VOUCHER_ID = ?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.IsExistAuditLogHistoryExitsByUserOrAuditor:
                    {
                        query = "SELECT VOUCHER_ID FROM VOUCHER_MASTER_AUDIT_LOG\n" +
                                    "WHERE VOUCHER_ID = ?VOUCHER_ID {AND IS_AUDITOR_MODIFIED = ?IS_AUDITOR_MODIFIED} {AND MODIFIED_BY=?MODIFIED_BY};";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchDeleteMasterDetails:
                    {
                        query = "SELECT VM.VOUCHER_ID,VM.NARRATION,\n" +
                        "       FD.FD_ACCOUNT_ID,\n" +
                        "       CASE VOUCHER_TYPE\n" +
                        "         WHEN 'CN' THEN\n" +
                        "         ML.LEDGER_NAME\n" +
                        "         WHEN 'JN' THEN\n" +
                        "         ML.LEDGER_NAME ELSE\n" +
                        "       LED.LEDGER_NAME END AS LEDGER_NAME,\n" +
                        "       CT.CASHBANK,\n" +
                        "       VOUCHER_DATE,\n" +
                        "       VT.LEDGER_ID,\n" +
                        "       MP.PROJECT_ID,\n" +
                        "       CONCAT(MP.PROJECT, CONCAT(' - ', MD.DIVISION)) AS 'PROJECT',\n" +
                        "       VOUCHER_NO,\n" +
                        "       VOUCHER_SUB_TYPE,\n" +
                        "       CASE WHEN VM.VOUCHER_TYPE ='RC'\n" +
                        "           THEN\n" +
                        "              RECEIPT.RECEIPT\n" +
                        "           WHEN VM.VOUCHER_TYPE ='PY'\n" +
                        "             THEN PAYMENT.PAYMENT ELSE VT.AMOUNT\n" +
                        "       END AS AMOUNT,\n" +
                        "       CASE VOUCHER_TYPE\n" +
                        "         WHEN 'RC' THEN\n" +
                        "          'Receipt'\n" +
                        "         WHEN 'PY' THEN\n" +
                        "          'Payment'\n" +
                        "         WHEN 'CN' THEN\n" +
                        "          'Contra'\n" +
                        "         WHEN 'JN' THEN\n" +
                        "          'journal'\n" +
                        "         ELSE\n" +
                        "          ''\n" +
                        "       END AS VOUCHERTYPE,\n" +
                        "       NAME AS DONOR_NAME,\n" +
                        "       CASE VOUCHER_TYPE\n" +
                        "         WHEN 'RC' THEN\n" +
                        "          CONTRIBUTION_AMOUNT\n" +
                        "         WHEN 'CN' THEN\n" +
                        "          CONTRIBUTION_AMOUNT\n" +
                        "         ELSE\n" +
                        "          ''\n" +
                        "       END AS DEBIT,\n" +
                        "       CASE VOUCHER_TYPE\n" +
                        "         WHEN 'PY' THEN\n" +
                        "          CONTRIBUTION_AMOUNT\n" +
                        "         ELSE\n" +
                        "          ''\n" +
                        "       END AS CREDIT\n" +
                        "  FROM VOUCHER_MASTER_TRANS AS VM\n" +
                        " INNER JOIN MASTER_PROJECT AS MP\n" +
                        "    ON VM.PROJECT_ID = MP.PROJECT_ID\n" +
                        " INNER JOIN MASTER_DIVISION AS MD\n" +
                        "    ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                        "  LEFT JOIN MASTER_DONAUD AS MAD\n" +
                        "    ON VM.DONOR_ID = MAD.DONAUD_ID\n" +
                        " INNER JOIN VOUCHER_TRANS VT\n" +
                        "    ON VM.VOUCHER_ID = VT.VOUCHER_ID\n" +
                        "  LEFT JOIN MASTER_LEDGER ML\n" +
                        "    ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                        " INNER JOIN MASTER_LEDGER_GROUP LG\n" +
                        "    ON ML.GROUP_ID = LG.GROUP_ID\n" +
                        "  LEFT JOIN FD_ACCOUNT AS FD\n" +
                        "    ON VM.VOUCHER_ID = FD.FD_VOUCHER_ID\n" +
                        "\n" +
                        " LEFT JOIN (SELECT VMT.VOUCHER_ID,\n" +
                        "             CASE\n" +
                        "               WHEN ML.LEDGER_SUB_TYPE='BK' THEN\n" +
                        "               CONCAT(CONCAT(ML.LEDGER_NAME, ' - '),CONCAT(MB.BANK,' - '),MB.BRANCH)\n" +
                        "               ELSE\n" +
                        "               ML.LEDGER_NAME\n" +
                        "             END AS CASHBANK\n" +
                        "             FROM VOUCHER_MASTER_TRANS VMT\n" +
                        "             INNER JOIN VOUCHER_TRANS VT\n" +
                        "             ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                        "             LEFT JOIN MASTER_LEDGER ML\n" +
                        "             ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                        "             LEFT JOIN MASTER_LEDGER_GROUP MLG\n" +
                        "             ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                        "             LEFT JOIN MASTER_BANK_ACCOUNT MBA\n" +
                        "             ON MBA.LEDGER_ID =ML.LEDGER_ID\n" +
                        "             LEFT JOIN MASTER_BANK MB\n" +
                        "             ON MB.BANK_ID=MBA.BANK_ID\n" +
                        "             WHERE MLG.GROUP_ID IN (12, 13)) AS CT\n" +
                        " ON CT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                        "\n" +
                        "  LEFT JOIN (SELECT VT.VOUCHER_ID, SUM(VT.AMOUNT) AS RECEIPT\n" +
                        "               FROM VOUCHER_MASTER_TRANS AS VM\n" +
                        "              INNER JOIN MASTER_PROJECT AS MP\n" +
                        "                 ON VM.PROJECT_ID = MP.PROJECT_ID\n" +
                        "              INNER JOIN MASTER_DIVISION AS MD\n" +
                        "                 ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                        "               LEFT JOIN MASTER_DONAUD AS MAD\n" +
                        "                 ON VM.DONOR_ID = MAD.DONAUD_ID\n" +
                        "              INNER JOIN VOUCHER_TRANS VT\n" +
                        "                 ON VM.VOUCHER_ID = VT.VOUCHER_ID\n" +
                        "               LEFT JOIN FD_ACCOUNT AS FD\n" +
                        "                 ON VM.VOUCHER_ID = FD.FD_VOUCHER_ID\n" +
                        "              WHERE VOUCHER_TYPE IN ('RC','PY')\n" +
                        "                AND VM.PROJECT_ID = ?PROJECT_ID\n" +
                        "                AND VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                        "                AND VT.TRANS_MODE = 'CR' GROUP BY VM.VOUCHER_ID) AS RECEIPT\n" +
                        "    ON RECEIPT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                        "\n" +
                        "     LEFT JOIN (SELECT VT.VOUCHER_ID, SUM(VT.AMOUNT) AS PAYMENT\n" +
                        "               FROM VOUCHER_MASTER_TRANS AS VM\n" +
                        "              INNER JOIN MASTER_PROJECT AS MP\n" +
                        "                 ON VM.PROJECT_ID = MP.PROJECT_ID\n" +
                        "              INNER JOIN MASTER_DIVISION AS MD\n" +
                        "                 ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                        "               LEFT JOIN MASTER_DONAUD AS MAD\n" +
                        "                 ON VM.DONOR_ID = MAD.DONAUD_ID\n" +
                        "              INNER JOIN VOUCHER_TRANS VT\n" +
                        "                 ON VM.VOUCHER_ID = VT.VOUCHER_ID\n" +
                        "               LEFT JOIN FD_ACCOUNT AS FD\n" +
                        "                 ON VM.VOUCHER_ID = FD.FD_VOUCHER_ID\n" +
                        "              WHERE VOUCHER_TYPE IN ('RC','PY' )\n" +
                        "                AND VM.PROJECT_ID = ?PROJECT_ID\n" +
                        "                AND VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                        "                AND VT.TRANS_MODE = 'DR'  GROUP BY VM.VOUCHER_ID) AS PAYMENT\n" +
                        "    ON PAYMENT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                        "\n" +
                        "      LEFT JOIN (SELECT VT.VOUCHER_ID, VT.LEDGER_ID, ML.LEDGER_NAME\n" +
                        "               FROM VOUCHER_MASTER_TRANS AS VM\n" +
                        "              INNER JOIN MASTER_PROJECT AS MP\n" +
                        "                 ON VM.PROJECT_ID = MP.PROJECT_ID\n" +
                        "              INNER JOIN MASTER_DIVISION AS MD\n" +
                        "                 ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                        "               LEFT JOIN MASTER_DONAUD AS MAD\n" +
                        "                 ON VM.DONOR_ID = MAD.DONAUD_ID\n" +
                        "              INNER JOIN VOUCHER_TRANS VT\n" +
                        "                 ON VM.VOUCHER_ID = VT.VOUCHER_ID\n" +
                        "               LEFT JOIN MASTER_LEDGER ML\n" +
                        "                 ON VT.LEDGER_ID = ML.LEDGER_ID\n" +
                        "              INNER JOIN MASTER_LEDGER_GROUP LG\n" +
                        "                 ON ML.GROUP_ID = LG.GROUP_ID\n" +
                        "               LEFT JOIN FD_ACCOUNT AS FD\n" +
                        "                 ON VM.VOUCHER_ID = FD.FD_VOUCHER_ID\n" +
                        "              WHERE FIND_IN_SET(VOUCHER_TYPE, ?VOUCHER_TYPE) > 0\n" +
                        "                AND VM.PROJECT_ID = ?PROJECT_ID\n" +
                        "                AND VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                        "                   --  AND VT.TRANS_MODE='DR'\n" +
                        "                and ML.GROUP_ID NOT IN (12, 13, 14)\n" +
                        "             -- AND VT.TRANS_MODE = 'CR'\n" +
                        "             ) AS LED\n" +
                        "    ON LED.VOUCHER_ID = VT.VOUCHER_ID\n" +
                        " WHERE FIND_IN_SET(VOUCHER_TYPE, ?VOUCHER_TYPE) > 0 \n" +
                        "   AND VM.PROJECT_ID = ?PROJECT_ID\n" +
                        "   AND VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                        "   AND VT.TRANS_MODE = 'DR'\n" +
                        "   AND VM.STATUS <> 1\n" +
                        " GROUP BY VT.VOUCHER_ID\n" +
                        " ORDER BY VOUCHER_DATE, VOUCHER_NO ASC";


                        break;
                    }

                case SQLCommand.VoucherMaster.FetchMasterByID:
                    {
                        query = "SELECT " +
                                "MT.VOUCHER_ID," +
                                "MT.PROJECT_ID," +
                                "MP.PROJECT," +
                                "VOUCHER_DATE," +
                                "VOUCHER_NO," +
                                "MT.VOUCHER_TYPE," +
                                "VOUCHER_SUB_TYPE," +
                                "DONOR_ID," +
                                "PURPOSE_ID," +
                                "CONTRIBUTION_TYPE," +
                                "CONTRIBUTION_AMOUNT," +
                                "CURRENCY_COUNTRY_ID," +
                                "EXCHANGE_RATE," +
                                "CALCULATED_AMOUNT," +
                                "ACTUAL_AMOUNT," +
                                "EXCHANGE_COUNTRY_ID," +
                                "MT.STATUS," +
                            //"CREATED_ON," +
                            //"MODIFIED_ON," +
                                "CREATED_BY," +
                                "MODIFIED_BY," +
                                "NARRATION, NAME_ADDRESS, PAN_NUMBER,GST_NUMBER, MT.GST_VENDOR_INVOICE_NO, MT.GST_VENDOR_INVOICE_DATE, MT.GST_VENDOR_INVOICE_TYPE, MT.GST_VENDOR_ID," +
                                "VOUCHER_DEFINITION_ID, IFNULL(MT.IS_AUDITOR_MODIFIED , 0) AS IS_AUDITOR_MODIFIED, AUTHORIZATION_STATUS, AUTHORIZATION_UPDATED_BY_NAME,  " +
                                "AUTHORIZATION_UPDATED_ON, GIM.GST_INVOICE_ID AS GST_INVOICE_ID, IFNULl(INV.GST_INVOICE_ID, 0) AS BOOKING_INVOICE_ID " +
                                "FROM VOUCHER_MASTER_TRANS MT " +
                                "INNER JOIN MASTER_PROJECT MP ON MT.PROJECT_ID=MP.PROJECT_ID " +
                                "LEFT JOIN GST_INVOICE_MASTER GIM ON ((GIM.BOOKING_VOUCHER_ID = MT.VOUCHER_ID AND GIM.BOOKING_VOUCHER_ID = ?VOUCHER_ID) " +
                                "      OR (GIM.BOOKING_VOUCHER_ID IS NULL AND GIM.GST_VENDOR_INVOICE_NO = MT.GST_VENDOR_INVOICE_NO) )" +
                                "LEFT JOIN VOUCHER_GST_INVOICE VGI ON VGI.VOUCHER_ID = MT.VOUCHER_ID " +
                                "LEFT JOIN (SELECT GIM.GST_INVOICE_ID,  VGI.VOUCHER_ID, IFNULL(VGI.AMOUNT, 0) AS AMOUNT " +
                                "      FROM GST_INVOICE_MASTER GIM " +
                                "      INNER JOIN VOUCHER_GST_INVOICE VGI ON VGI.GST_INVOICE_ID = GIM.GST_INVOICE_ID AND VGI.VOUCHER_ID = ?VOUCHER_ID " +
                                "      AND GIM.BOOKING_VOUCHER_ID > 0 AND GIM.BOOKING_VOUCHER_TYPE = 'JN') AS INV ON INV.VOUCHER_ID = MT.VOUCHER_ID " +
                                "WHERE MT.VOUCHER_ID=?VOUCHER_ID ";
                        break;
                    }

                case SQLCommand.VoucherMaster.FetchVoucherStartingNo:
                    {
                        query = "SELECT" +
                                " CONCAT(PREFIX_CHAR,CONCAT('#',STARTING_NUMBER ),CONCAT('#',SUFFIX_CHAR)) AS 'VOUCHER_NUMBER' " +
                                " FROM MASTER_VOUCHER AS MV " +
                                " INNER JOIN PROJECT_VOUCHER AS MPV ON " +
                                " MV.VOUCHER_ID=MPV.VOUCHER_ID " +
                                " WHERE MPV.PROJECT_ID=?PROJECT_ID " +
                                " AND MV.VOUCHER_TYPE=?VOUCHER_TYPE AND VOUCHER_TYPE NOT IN (4)";
                        break;
                    }
                case SQLCommand.VoucherMaster.IsTransactionMadeForProject:
                    {
                        query = "SELECT VT.LEDGER_ID,LEDGER_NAME FROM VOUCHER_TRANS VT " +
                                         "INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID= VT.LEDGER_ID " +
                                         "INNER JOIN VOUCHER_MASTER_TRANS VMT ON VMT.VOUCHER_ID =VT.VOUCHER_ID " +
                                 "WHERE  VMT.PROJECT_ID=?PROJECT_ID AND FIND_IN_SET(VT.LEDGER_ID,?IDs) AND VMT.STATUS=1 " +
                                 "GROUP BY LEDGER_ID;";
                        break;
                    }
                case SQLCommand.VoucherMaster.IsTransactionMadeForDonor:
                    {
                        query = "SELECT VOUCHER_ID,PROJECT_ID FROM VOUCHER_MASTER_TRANS WHERE STATUS=1 AND PROJECT_ID=?PROJECT_ID AND FIND_IN_SET(DONOR_ID,?IDs);";
                        break;
                    }

                case SQLCommand.VoucherMaster.IsTransMadeSigleLedger:
                    {
                        query = @"SELECT VT.LEDGER_ID,LEDGER_NAME,VMT.VOUCHER_ID FROM VOUCHER_TRANS VT
                                         INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID= VT.LEDGER_ID
                                         INNER JOIN VOUCHER_MASTER_TRANS VMT ON VMT.VOUCHER_ID =VT.VOUCHER_ID AND VMT.STATUS=1
                                 WHERE  VT.LEDGER_ID in(?LEDGER_ID) AND VMT.STATUS=1
                                 GROUP BY LEDGER_ID LIMIT 1
                                 UNION ALL   
                                 SELECT FD.LEDGER_ID, ML.LEDGER_NAME, 0 AS VOUCHER_ID FROM FD_ACCOUNT FD
                                 INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID= FD.LEDGER_ID 
                                 WHERE FD.LEDGER_ID in(?LEDGER_ID) AND FD.STATUS = 1 GROUP BY FD.LEDGER_ID LIMIT 1";
                        break;
                    }
                case SQLCommand.VoucherMaster.IsLedgerMapped:
                    {
                        query = "SELECT LEDGER_ID FROM PROJECT_LEDGER WHERE LEDGER_ID=?LEDGER_ID;";
                        break;
                    }
                case SQLCommand.VoucherMaster.DeleteDonorVouchers:
                    {
                        query = "UPDATE VOUCHER_MASTER_TRANS SET STATUS=0 WHERE CLIENT_CODE='DMS'";
                        break;
                    }
                case SQLCommand.VoucherMaster.DeleteVoucherImport:
                    {
                        query = "UPDATE VOUCHER_MASTER_TRANS SET STATUS=0 WHERE VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED AND PROJECT_ID IN (?PROJECT_ID) AND CLIENT_CODE='EMTV'";
                        break;
                    }
                case SQLCommand.VoucherMaster.IsTransactionMadeForLedger:
                    {
                        query = "SELECT VT.LEDGER_ID,LEDGER_NAME FROM VOUCHER_TRANS VT " +
                                        "INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID= VT.LEDGER_ID " +
                                        "INNER JOIN VOUCHER_MASTER_TRANS VMT ON VMT.VOUCHER_ID =VT.VOUCHER_ID " +
                                "WHERE VT.LEDGER_ID=?LEDGER_ID AND FIND_IN_SET(VMT.PROJECT_ID,?PROJECT_IDs) " +
                                "GROUP BY LEDGER_ID;";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchLastVoucherDate:
                    {
                        query = @"SELECT VOUCHER_DATE FROM VOUCHER_MASTER_TRANS WHERE STATUS=1 {AND PROJECT_ID=?PROJECT_ID} 
                                    {AND VOUCHER_DATE BETWEEN ?YEAR_FROM AND ?YEAR_TO} ORDER BY VOUCHER_DATE DESC LIMIT 1";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchBRS:
                    {
                        //13/10/2017, Report based on Data As On or Date Range (DateFrom, DateTo)
                        //  "       CHEQUE_REF_DATE,\n" +
                        query = "SELECT MT.VOUCHER_ID, MT.SEQUENCE_NO, VOUCHER_DATE, CHEQUE_NO,\n" +
                        "   IF(CHEQUE_REF_DATE='0001-01-01 00:00:00','',DATE_FORMAT(CHEQUE_REF_DATE,'%d/%m/%Y')) AS CHEQUE_REF_DATE,\n" +
                        "   MATERIALIZED_ON, T.LEDGER_ID, T.LEDGER_NAME,\n" +
                        "   CASE\n" +
                        "    WHEN VMT.VOUCHER_TYPE = 'PY' THEN IFNULL(MT.AMOUNT, 0)\n" +
                        "    WHEN VMT.VOUCHER_TYPE IN ('CN','JN') AND TRANS_MODE='CR' THEN IFNULL(MT.AMOUNT, 0) ELSE 0.00 END {* IF(?IDENTITY_FLAG=0 AND VMT.IS_MULTI_CURRENCY=1, VMT.EXCHANGE_RATE, 1)} AS 'PAYMENT',\n" +
                        "   CASE\n" +
                        "    WHEN VMT.VOUCHER_TYPE = 'RC' THEN IFNULL(MT.AMOUNT, 0) \n" +
                        "    WHEN VMT.VOUCHER_TYPE IN ('CN','JN') AND TRANS_MODE='DR' THEN IFNULL(MT.AMOUNT, 0) ELSE 0.00 END {* IF(?IDENTITY_FLAG=0 AND VMT.IS_MULTI_CURRENCY=1, VMT.EXCHANGE_RATE, 1)} AS 'RECEIPT',\n" +
                        "   MT.AMOUNT, TRANS_MODE, CLIENT_CODE, CLIENT_MODE,\n" +
                        "   CASE\n" +
                        "       WHEN (MT.MATERIALIZED_ON IS NULL AND VMT.VOUCHER_TYPE = 'PY') OR (MT.MATERIALIZED_ON IS NULL AND VMT.VOUCHER_TYPE = 'JN') OR \n" +
                        "            (MT.MATERIALIZED_ON IS NULL AND VMT.VOUCHER_TYPE = 'CN' AND TRANS_MODE='CR') THEN\n" +
                        "            'Uncleared'\n" +
                        "       WHEN (MT.MATERIALIZED_ON IS NOT NULL AND VMT.VOUCHER_TYPE = 'PY') OR (MT.MATERIALIZED_ON IS NOT NULL AND VMT.VOUCHER_TYPE = 'JN') OR \n" +
                        "            (MT.MATERIALIZED_ON IS NOT NULL AND VMT.VOUCHER_TYPE = 'CN' AND TRANS_MODE='CR') THEN\n" +
                        "            'Cleared'\n" +
                        "       WHEN (MT.MATERIALIZED_ON IS NULL AND VMT.VOUCHER_TYPE = 'RC') OR (MT.MATERIALIZED_ON IS NULL AND VMT.VOUCHER_TYPE = 'JN') OR \n" +
                        "            (MT.MATERIALIZED_ON IS NULL AND VMT.VOUCHER_TYPE = 'CN' AND TRANS_MODE='DR') THEN\n" +
                        "            'Unrealized'\n" +
                        "       WHEN (MT.MATERIALIZED_ON IS NOT NULL AND VMT.VOUCHER_TYPE = 'RC') OR (MT.MATERIALIZED_ON IS NOT NULL AND VMT.VOUCHER_TYPE = 'JN') OR \n" +
                        "            (MT.MATERIALIZED_ON IS NOT NULL AND VMT.VOUCHER_TYPE = 'CN' AND TRANS_MODE='DR') THEN\n" +
                        "            'Realized'\n" +
                        "   END AS 'STATUS', CONCAT(CONCAT(BA.ACCOUNT_NUMBER, ' - '),CONCAT(BANK,' - '),BRANCH) AS BANK\n" +
                        " FROM MASTER_PROJECT PL\n" +
                        " INNER JOIN VOUCHER_MASTER_TRANS VMT ON PL.PROJECT_ID = VMT.PROJECT_ID\n" +
                        " INNER JOIN VOUCHER_TRANS MT ON VMT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                        " INNER JOIN (SELECT MT.VOUCHER_ID,\n" +
                        "                    MT.LEDGER_ID,\n" +
                        "                    ML.LEDGER_CODE,\n" +
                        "                    ML.LEDGER_NAME AS LEDGER_NAME\n" +
                        "              FROM PROJECT_LEDGER PL\n" +
                        "              INNER JOIN VOUCHER_MASTER_TRANS VMT ON PL.PROJECT_ID = VMT.PROJECT_ID\n" +
                        "              INNER JOIN VOUCHER_TRANS MT ON VMT.VOUCHER_ID = MT.VOUCHER_ID AND MT.LEDGER_ID = PL.LEDGER_ID\n" + // Added By Praveen
                        "              INNER JOIN MASTER_LEDGER ML ON MT.LEDGER_ID = ML.LEDGER_ID\n" +
                        "              WHERE PL.PROJECT_ID = ?PROJECT_ID\n" +
                            //"                AND VMT.VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED \n" +    // Commented by Praveen  "AND MT.LEDGER_ID = ML.LEDGER_ID\n" +
                        "                {AND VMT.VOUCHER_DATE <= ?DATE_CLOSED} {AND VMT.VOUCHER_DATE <= ?DATE_AS_ON}\n" +                                 // Commented by Praveen  "AND MT.LEDGER_ID = ML.LEDGER_ID\n" +
                        "              AND (VMT.VOUCHER_TYPE IN ('RC', 'PY','JN') AND ML.GROUP_ID NOT IN(12)) OR (VMT.VOUCHER_TYPE = 'CN' {AND ML.LEDGER_ID NOT IN (?LEDGER_ID)})\n" +
                        "              GROUP BY VMT.VOUCHER_ID\n" +
                        "              ORDER BY VMT.VOUCHER_ID) AS T\n" +
                        "    ON MT.VOUCHER_ID = T.VOUCHER_ID\n" +
                        "\n" +
                        " INNER JOIN MASTER_LEDGER ML ON MT.LEDGER_ID = ML.LEDGER_ID\n" +
                        " INNER JOIN master_bank_ACCOUNT ba ON ML.LEDGER_ID = BA.LEDGER_ID\n" +
                        " INNER JOIN MASTER_BANK MB ON BA.BANK_ID = MB.BANK_ID\n" +
                            //" WHERE VMT.VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED \n" +
                        " WHERE {VMT.VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED }\n" +          //13/10/2017, Report based on Data As On or Date Range (DateFrom, DateTo)
                        " {VMT.VOUCHER_DATE <= ?DATE_AS_ON} {AND MT.LEDGER_ID =?LEDGER_ID} \n" +
                        "   AND ML.LEDGER_SUB_TYPE = 'BK'\n" +
                        "   AND VMT.STATUS=1\n" +
                        "   AND VMT.VOUCHER_TYPE IN ('PY', 'RC', 'CN','JN') AND (VMT.IS_CASH_BANK_STATUS=1) AND VMT.VOUCHER_SUB_TYPE NOT IN ('FD') \n" +
                        "   AND PL.PROJECT_ID = ?PROJECT_ID\n" +
                        " ORDER BY VOUCHER_DATE,CHEQUE_NO DESC, MT.VOUCHER_ID"; //STATUS DESC
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchBRSByMaterialized:
                    {
                        query = "SELECT MT.VOUCHER_ID, T.LEDGER_ID,VMT.VOUCHER_SUB_TYPE,T.TRANS_MODE,VMT.VOUCHER_TYPE, MT.CHEQUE_NO,\n" +
                        "  VMT.VOUCHER_DATE, T.LEDGER_CODE, MATERIALIZED_ON as 'DATE', T.LEDGER_NAME,\n" +
                        "  CASE\n" +
                        "   WHEN VMT.VOUCHER_TYPE = 'PY' THEN IF(T.TRANS_MODE ='DR', MT.AMOUNT, MT.AMOUNT)\n" +
                        "   WHEN (VMT.VOUCHER_TYPE = 'CN' AND T.TRANS_MODE ='DR') THEN MT.AMOUNT ELSE 0.00 END {* IF(?IDENTITY_FLAG=0 AND VMT.IS_MULTI_CURRENCY=1, VMT.EXCHANGE_RATE, 1)} AS 'UnCleared',\n" +
                        "  CASE\n" +
                        "   WHEN VMT.VOUCHER_TYPE = 'RC' THEN IF(T.TRANS_MODE ='CR', MT.AMOUNT, MT.AMOUNT)\n" +
                        "   WHEN (VMT.VOUCHER_TYPE = 'CN' AND T.TRANS_MODE ='CR') THEN MT.AMOUNT ELSE 0.00 END {* IF(?IDENTITY_FLAG=0 AND VMT.IS_MULTI_CURRENCY=1, VMT.EXCHANGE_RATE, 1)} AS 'Unrealised'\n" +
                        " FROM MASTER_PROJECT PL\n" +
                        " INNER JOIN VOUCHER_MASTER_TRANS VMT ON PL.PROJECT_ID = VMT.PROJECT_ID\n" +
                        " INNER JOIN VOUCHER_TRANS MT ON VMT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                        " INNER JOIN MASTER_LEDGER ML ON MT.LEDGER_ID = ML.LEDGER_ID\n" +
                        " INNER JOIN\n" +
                        "(SELECT MT.VOUCHER_ID,MT.LEDGER_ID,ML.GROUP_ID,ML.LEDGER_CODE,ML.LEDGER_NAME,MT.AMOUNT,MT.TRANS_MODE,MT.SEQUENCE_NO\n" +
                        "        FROM MASTER_LEDGER AS ML\n" +
                        "        INNER JOIN VOUCHER_TRANS AS MT ON MT.LEDGER_ID=ML.LEDGER_ID\n" +
                        "        INNER JOIN VOUCHER_MASTER_TRANS AS VMT ON VMT.VOUCHER_ID = MT.VOUCHER_ID\n" +
                        "        WHERE (VMT.VOUCHER_TYPE IN ('RC', 'PY') AND ML.GROUP_ID NOT IN(12)) OR (VMT.VOUCHER_TYPE = 'CN' {AND ML.LEDGER_ID NOT IN (?LEDGER_ID)})) AS T\n" +
                        " ON VMT.VOUCHER_ID = T.VOUCHER_ID\n" +
                        " WHERE ML.LEDGER_SUB_TYPE = 'BK' AND VMT.STATUS = 1 AND PL.PROJECT_ID IN (?PROJECT_ID) {AND ML.LEDGER_ID IN (?LEDGER_ID)}\n" +
                        "  AND ((MT.MATERIALIZED_ON > ?DATE_AS_ON AND VMT.VOUCHER_DATE <= ?DATE_AS_ON)\n" +
                        "        OR IF(MT.MATERIALIZED_ON IS NULL,VMT.VOUCHER_DATE<=?DATE_AS_ON,''))\n" +
                        "  AND VMT.VOUCHER_TYPE IN ('PY', 'RC','CN')  AND VMT.VOUCHER_SUB_TYPE NOT IN ('FD') \n" +
                        " GROUP BY VMT.VOUCHER_ID, MT.SEQUENCE_NO ORDER BY VOUCHER_DATE,CHEQUE_NO DESC, MT.VOUCHER_ID, T.SEQUENCE_NO";
                        break;
                    }
                case SQLCommand.VoucherMaster.UpdateBRS:
                    {
                        //, IS_AUDITOR_MODIFIED = IF(?IS_AUDITOR = 1, 1, IS_AUDITOR_MODIFIED)
                        //"UPDATE VOUCHER_MASTER_TRANS SET MODIFIED_ON = NOW(), MODIFIED_BY = ?MODIFIED_BY,\n" +
                        //"MODIFIED_BY_NAME = ?MODIFIED_BY_NAME WHERE STATUS=1 AND VOUCHER_ID= ?VOUCHER_ID\n" +

                        query = "UPDATE VOUCHER_TRANS SET MATERIALIZED_ON=?MATERIALIZED_ON WHERE VOUCHER_ID= ?VOUCHER_ID AND SEQUENCE_NO=?SEQUENCE_NO";
                        break;
                    }
                case SQLCommand.VoucherMaster.CheckProjectExist:
                    {
                        query = "SELECT COUNT(*) FROM VOUCHER_MASTER_TRANS WHERE PROJECT_ID=?PROJECT_ID AND STATUS NOT IN(0)";
                        break;
                    }
                case SQLCommand.VoucherMaster.VoucherFDInterestAdd:
                    {
                        query = "INSERT INTO VOUCHER_FD_INTEREST\n" +
                        "  (FD_VOUCHER_ID, FD_LEDGER_ID, BK_INT_VOUCHER_ID, BK_INT_LEDGER_ID)\n" +
                        "VALUES\n" +
                        "  (?FD_VOUCHER_ID,\n" +
                        "   ?FD_LEDGER_ID,\n" +
                        "   ?BK_INT_VOUCHER_ID,\n" +
                        "   ?BK_INT_LEDGER_ID)";

                        break;
                    }
                case SQLCommand.VoucherMaster.VoucherFDInterestDelete:
                    {
                        query = "DELETE FROM VOUCHER_FD_INTEREST WHERE BK_INT_VOUCHER_ID=?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchFDVoucherInterest:
                    {
                        query = "SELECT FD_VOUCHER_ID,BK_INT_VOUCHER_ID\n" +
                        "  FROM VOUCHER_FD_INTEREST\n" +
                        " WHERE FD_VOUCHER_ID = ?VOUCHER_ID\n" +
                        "    OR BK_INT_VOUCHER_ID = ?VOUCHER_ID";

                        break;
                    }
                case SQLCommand.VoucherMaster.FetchFDVoucherInterestByVoucherId:
                    {
                        query = "SELECT VT.LEDGER_ID, VT.AMOUNT, VMT.NARRATION, VT.TRANS_MODE\n" +
                         "  FROM VOUCHER_MASTER_TRANS VMT\n" +
                         " INNER JOIN VOUCHER_TRANS VT\n" +
                         "    ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                         " WHERE VMT.VOUCHER_ID = ?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchFDVoucherPostedInterest:
                    {
                        query = "SELECT  ROUND(AMOUNT * (INTEREST_RATE / 100),2) AS INTEREST_RATE\n" +
                        "  FROM MASTER_BANK_ACCOUNT\n" +
                        " WHERE BANK_ACCOUNT_ID =?BANK_ACCOUNT_ID ";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchVoucherByProjectId:
                    {
                        query = "SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS WHERE PROJECT_ID=?PROJECT_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.DeleteVouchers:
                    {
                        query = @"DELETE FROM VOUCHER_TRANS WHERE VOUCHER_ID IN (SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS WHERE PROJECT_ID=?PROJECT_ID);
                                  DELETE FROM VOUCHER_CC_TRANS WHERE VOUCHER_ID IN(SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS WHERE PROJECT_ID=?PROJECT_ID);
                                  DELETE FROM VOUCHER_MASTER_TRANS WHERE PROJECT_ID=?PROJECT_ID;";
                        break;
                    }
                case SQLCommand.VoucherMaster.DeleteRefNumberDetails:
                    {
                        query = @"DELETE FROM VOUCHER_REFERENCE WHERE REC_PAY_VOUCHER_ID = ?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchActiveVoucherCounts:
                    {
                        query = "SELECT COUNT(*) FROM VOUCHER_MASTER_TRANS WHERE STATUS = 1";
                        break;
                    }
                case SQLCommand.VoucherMaster.CheckTransExists:
                    {
                        query = "SELECT COUNT(*) FROM VOUCHER_MASTER_TRANS WHERE PROJECT_ID=?PROJECT_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.CheckTransExistsByProject:
                    {
                        query = "SELECT COUNT(*) FROM VOUCHER_MASTER_TRANS WHERE PROJECT_ID=?PROJECT_ID AND STATUS = 1";
                        break;
                    }
                case SQLCommand.VoucherMaster.CheckFirstVoucherDateByProject:
                    {
                        query = "SELECT VOUCHER_DATE FROM VOUCHER_MASTER_TRANS WHERE PROJECT_ID=?PROJECT_ID AND STATUS = 1 ORDER BY VOUCHER_DATE LIMIT 1;";
                        break;
                    }
                case SQLCommand.VoucherMaster.ReindexTables:
                    {
                        query = @"ALTER TABLE VOUCHER_MASTER_TRANS DROP FOREIGN KEY FK_voucher_master_trans_PROJECT_ID;
                                  ALTER TABLE VOUCHER_MASTER_TRANS DROP INDEX `FK_voucher_master_trans_PROJECT_ID`;
                                  ALTER TABLE VOUCHER_MASTER_TRANS ADD CONSTRAINT `FK_voucher_master_trans_PROJECT_ID`
                                    FOREIGN KEY `FK_voucher_master_trans_PROJECT_ID` (`PROJECT_ID`) REFERENCES `master_project` (`PROJECT_ID`) ON DELETE RESTRICT ON UPDATE RESTRICT;
                                  ALTER TABLE voucher_master_trans DROP INDEX `FK_VOUCHER_MASTER_PROJECT_ID`,
                                    ADD INDEX `FK_VOUCHER_MASTER_PROJECT_ID` USING BTREE(`VOUCHER_DATE`, `PROJECT_ID`, `VOUCHER_TYPE`);";
                        break;
                    }
                case SQLCommand.VoucherMaster.OptimizeMainTables:
                    {
                        query = @"OPTIMIZE TABLE VOUCHER_CC_TRANS;
                                  OPTIMIZE TABLE VOUCHER_TRANS;
                                  OPTIMIZE TABLE VOUCHER_MASTER_TRANS;
                                  OPTIMIZE TABLE LEDGER_BALANCE;";
                        break;
                    }
                case SQLCommand.VoucherMaster.CheckLastVoucherDateByProject:
                    {
                        query = "SELECT VOUCHER_DATE FROM VOUCHER_MASTER_TRANS WHERE PROJECT_ID=?PROJECT_ID AND STATUS = 1 ORDER BY VOUCHER_DATE DESC LIMIT 1;";
                        break;
                    }
                case SQLCommand.VoucherMaster.CheckTransExistsByDateProject:
                    {
                        query = "SELECT COUNT(*) \n" +
                                "  FROM VOUCHER_MASTER_TRANS VMT\n" +
                                " INNER JOIN VOUCHER_TRANS VT\n" +
                                "    ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                                " WHERE VMT.VOUCHER_DATE > ?VOUCHER_DATE\n" +
                                "   AND VMT.PROJECT_ID = ?PROJECT_ID AND VMT.STATUS = 1 ";
                        break;
                    }
                case SQLCommand.VoucherMaster.CheckFDVoucheExistsByProjectAndDateRange:
                    {
                        query = "SELECT SUM(COUNT) AS COUNT\n" +
                                "FROM (SELECT COUNT(*) AS COUNT FROM VOUCHER_MASTER_TRANS VMT\n" +
                                "INNER JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                                "WHERE VMT.VOUCHER_SUB_TYPE ='FD' AND VMT.VOUCHER_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                "AND VMT.PROJECT_ID = ?PROJECT_ID AND VMT.STATUS = 1\n" +
                                "UNION ALL\n" +
                                "SELECT COUNT(*) AS COUNT FROM FD_RENEWAL WHERE STATUS=1 AND RENEWAL_DATE BETWEEN ?DATE_FROM AND ?DATE_TO\n" +
                                "AND FD_ACCOUNT_ID IN (SELECT FD_ACCOUNT_ID FROM FD_ACCOUNT WHERE PROJECT_ID = ?PROJECT_ID AND STATUS=1)) AS T;";
                        break;
                    }
                case SQLCommand.VoucherMaster.CheckFDVoucheExistsByProject:
                    {
                        query = "SELECT SUM(COUNT) AS COUNT\n" +
                                "FROM (SELECT COUNT(*) AS COUNT FROM VOUCHER_MASTER_TRANS VMT\n" +
                                "INNER JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                                "WHERE VMT.VOUCHER_SUB_TYPE ='FD' AND VMT.PROJECT_ID = ?PROJECT_ID AND VMT.STATUS = 1\n" +
                                "UNION ALL\n" +
                                "SELECT COUNT(*) AS COUNT FROM FD_ACCOUNT WHERE PROJECT_ID = ?PROJECT_ID AND STATUS=1\n" +
                                "UNION ALL\n" +
                                "SELECT COUNT(*) AS COUNT FROM FD_RENEWAL WHERE STATUS = 1 AND \n" +
                                "FD_ACCOUNT_ID IN (SELECT FD_ACCOUNT_ID FROM FD_ACCOUNT WHERE PROJECT_ID = ?PROJECT_ID AND STATUS=1)) AS T;";
                        break;
                    }
                case SQLCommand.VoucherMaster.AutoFetchNarration:
                    {
                        //query = "SELECT NARRATION,VOUCHER_ID FROM VOUCHER_MASTER_TRANS WHERE STATUS=1 AND CREATED_BY=?CREATED_BY GROUP BY NARRATION ";

                        //On 26/04/2021, to show ledger line narration 
                        query = "SELECT NARRATION\n" +
                                "FROM (SELECT IFNULL(NARRATION,'') NARRATION FROM VOUCHER_MASTER_TRANS WHERE STATUS=1 {AND CREATED_BY=?CREATED_BY} GROUP BY NARRATION\n" +
                                "UNION ALL\n" +
                                "SELECT IFNULL(VT.NARRATION,'') NARRATION FROM VOUCHER_TRANS VT\n" +
                                "INNER JOIN VOUCHER_MASTER_TRANS VM ON VM.VOUCHER_ID = VT.VOUCHER_ID WHERE VM.STATUS=1 {AND VM.CREATED_BY=?CREATED_BY} GROUP BY VT.NARRATION) T";

                        //query = "SELECT T.NARRATION, T.VOUCHER_ID, T.LAST_VOUCHER_ID\n" +
                        //        "  FROM (SELECT NARRATION,\n" +
                        //        "               MAX(VOUCHER_ID) AS VOUCHER_ID,\n" +
                        //        "               1 AS LAST_VOUCHER_ID\n" +
                        //        "          FROM VOUCHER_MASTER_TRANS\n" +
                        //        "        UNION ALL\n" +
                        //        "        SELECT NARRATION, VOUCHER_ID, 0 AS LAST_VOUCHER_ID\n" +
                        //        "          FROM VOUCHER_MASTER_TRANS\n" +
                        //        "         GROUP BY NARRATION) AS T";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchNarration:
                    {
                        query = "SELECT (@row_number:=@row_number + 1) AS ROW, NARRATION, COUNT(*) AS No_of_Vouchers\n" +
                                "FROM (SELECT IFNULL(NARRATION,'') NARRATION FROM VOUCHER_MASTER_TRANS WHERE STATUS=1 {AND CREATED_BY=?CREATED_BY}\n" +
                                "UNION ALL\n" +
                                "SELECT IFNULL(VT.NARRATION,'') NARRATION FROM VOUCHER_TRANS VT\n" +
                                "INNER JOIN VOUCHER_MASTER_TRANS VM ON VM.VOUCHER_ID = VT.VOUCHER_ID WHERE VM.STATUS=1 {AND VM.CREATED_BY=?CREATED_BY} GROUP BY VT.VOUCHER_ID) T,\n" +
                                "(SELECT @row_number := 0) x\n" +
                                "GROUP BY T.NARRATION";
                        break;
                    }
                case SQLCommand.VoucherMaster.BulkUpdateNarration:
                    {
                        //, IS_AUDITOR_MODIFIED = IF(?IS_AUDITOR = 1, 1, IS_AUDITOR_MODIFIED)
                        query = "UPDATE VOUCHER_MASTER_TRANS SET NARRATION=?NEW_NARRATION, MODIFIED_ON=NOW(), MODIFIED_BY=?MODIFIED_BY,\n" +
                                "MODIFIED_BY_NAME = ?MODIFIED_BY_NAME\n" +
                                "WHERE STATUS = 1 AND NARRATION = ?EXISTING_NARRATION {AND CREATED_BY=?CREATED_BY};\n" +
                                "UPDATE VOUCHER_TRANS VT\n" +
                                "INNER JOIN VOUCHER_MASTER_TRANS VM ON VM.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                "SET VT.NARRATION=?NEW_NARRATION, MODIFIED_ON=NOW(), MODIFIED_BY=?MODIFIED_BY, MODIFIED_BY_NAME = ?MODIFIED_BY_NAME\n" +
                                "WHERE VM.STATUS=1 {AND VM.CREATED_BY=?CREATED_BY} AND VT.NARRATION=?EXISTING_NARRATION";
                        break;
                    }
                case SQLCommand.VoucherMaster.AuthorizeVoucherByVoucherId:
                    {
                        query = "UPDATE VOUCHER_MASTER_TRANS SET AUTHORIZATION_STATUS=?AUTHORIZATION_STATUS, AUTHORIZATION_UPDATED_ON=NOW(), AUTHORIZATION_UPDATED_BY_NAME=?AUTHORIZATION_UPDATED_BY_NAME\n" +
                                "WHERE VOUCHER_ID = ?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchAutoFetchNames:
                    {
                        query = "SELECT NAME_ADDRESS\n" +
                                "  FROM VOUCHER_MASTER_TRANS\n" +
                                " WHERE NAME_ADDRESS <> ''\n" +
                                " GROUP BY NAME_ADDRESS";
                        break;
                    }
                case SQLCommand.VoucherMaster.IsCostCenterLedger:
                    {
                        query = "SELECT LEDGER_ID,LEDGER_NAME,IS_COST_CENTER FROM MASTER_LEDGER WHERE LEDGER_ID=?LEDGER_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.IsInkindLedger:
                    {
                        query = @"select ML.LEDGER_ID from master_ledger ML
                                    INNER JOIN PROJECT_LEDGER PL ON ML.LEDGER_ID=PL.LEDGER_ID
                                    where IS_INKIND_LEDGER=1 AND PROJECT_ID=?PROJECT_ID ORDER BY ML.LEDGER_ID ASC LIMIT 1 ";
                        break;
                    }
                case SQLCommand.VoucherMaster.IsTDSLedger:
                    {
                        query = "SELECT LEDGER_ID, LEDGER_NAME, GROUP_ID, LEDGER_TYPE, IS_TDS_LEDGER\n" +
                        " FROM MASTER_LEDGER\n" +
                        " WHERE LEDGER_ID = ?LEDGER_ID;";
                        break;
                    }

                case SQLCommand.VoucherMaster.IsGStLedger:
                    {
                        query = "SELECT LEDGER_ID, LEDGER_NAME, GROUP_ID, LEDGER_TYPE, IS_GST_LEDGERS\n" +
                        " FROM MASTER_LEDGER\n" +
                        " WHERE LEDGER_ID = ?LEDGER_ID;";
                        break;
                    }
                case SQLCommand.VoucherMaster.IsIGSTLedgerApplied:
                    {
                        query = "SELECT ML.LEDGER_ID, TDP.STATE_ID, MS.STATE_NAME,ML.IS_GST_LEDGERS\n" +
                       "  FROM MASTER_LEDGER ML\n" +
                       " INNER JOIN TDS_CREDTIORS_PROFILE TDP\n" +
                       "    ON ML.LEDGER_ID = TDP.LEDGER_ID\n" +
                       " INNER JOIN MASTER_STATE MS\n" +
                       "    ON MS.STATE_ID = TDP.STATE_ID\n" +
                       " WHERE ML.LEDGER_ID = ?LEDGER_ID\n" +
                       "   AND MS.STATE_NAME NOT LIKE ?STATE_NAME\n" +
                       "   AND ML.GROUP_ID IN (17, 26)";

                        //query = "SELECT ML.LEDGER_ID, TDP.STATE_ID, MS.STATE_NAME,ML.IS_GST_LEDGERS\n" +
                        //"  FROM MASTER_LEDGER ML\n" +
                        //" INNER JOIN TDS_CREDTIORS_PROFILE TDP\n" +
                        //"    ON ML.LEDGER_ID = TDP.LEDGER_ID\n" +
                        //" INNER JOIN MASTER_STATE MS\n" +
                        //"    ON MS.STATE_ID = TDP.STATE_ID\n" +
                        //" WHERE ML.LEDGER_ID = ?LEDGER_ID\n" +
                        //"   AND MS.STATE_NAME !=?STATE_NAME\n" +
                        //"   AND ML.GROUP_ID IN (17, 26)";
                        break;
                    }

                case SQLCommand.VoucherMaster.FetchVoucherIdByClientRefCode:
                    {
                        query = "SELECT VOUCHER_ID\n" +
                                "  FROM VOUCHER_MASTER_TRANS\n" +
                                " WHERE CLIENT_REFERENCE_ID = ?CLIENT_REFERENCE_ID\n" +
                                "   AND CLIENT_CODE = ?CLIENT_CODE\n" +
                                "   AND STATUS = 1;";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchVoucherTransforSSPUnique:
                    {
                        query = " SELECT * FROM VOUCHER_MASTER_TRANS VMT INNER JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                                  " INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID WHERE CLIENT_CODE <> ''\n" +
                                  " AND (CLIENT_REFERENCE_ID =?CLIENT_REFERENCE_ID) AND PROJECT_ID IN (?PROJECT_ID) AND VMT.STATUS=1 GROUP BY VMT.VOUCHER_ID;";
                        // This is to remove the materilized date on equal to voucher date for ssp concept
                        //  query = "SELECT * FROM VOUCHER_MASTER_TRANS WHERE (VOUCHER_DATE =?VOUCHER_DATE AND CHEQUE_NO ='') AND CLIENT_CODE <> ''  AND PROJECT_ID IN (?PROJECT_ID)";

                        //query = "SELECT * FROM VOUCHER_MASTER_TRANS VMT INNER JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VMT.VOUCHER_ID \n" +
                        //         " INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID WHERE \n" +
                        //        "(VMT.VOUCHER_DATE =?VOUCHER_DATE AND CHEQUE_NO =?CHEQUE_NO)  AND CLIENT_CODE <> ''  AND PROJECT_ID IN (?PROJECT_ID) AND ML.GROUP_ID IN (12, 13) AND VMT.STATUS=1 GROUP BY VMT.VOUCHER_ID;";

                        //query = "SELECT * FROM VOUCHER_MASTER_TRANS VMT INNER JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VMT.VOUCHER_ID \n" +
                        //" INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID WHERE (VMT.VOUCHER_DATE =?VOUCHER_DATE AND (VT.MATERIALIZED_ON  = ?VOUCHER_DATE OR VT.MATERIALIZED_ON IS NULL)) \n" +
                        //" AND CLIENT_CODE <> '' AND CLIENT_REFERENCE_ID <> '0'  AND PROJECT_ID IN (?PROJECT_ID) \n" +
                        //" AND VMT.STATUS=1 GROUP BY VMT.VOUCHER_ID;";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchVoucherTransforSSPDeletion:
                    {
                        // New Old (08/07/2022)
                        query = "SELECT * FROM VOUCHER_MASTER_TRANS VMT INNER JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                                " INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID WHERE CLIENT_CODE <> '' AND ML.GROUP_ID IN (13) \n" +
                                " AND ( (VMT.VOUCHER_DATE =?VOUCHER_DATE OR VT.MATERIALIZED_ON  = ?VOUCHER_DATE))\n" +
                                " AND PROJECT_ID IN (?PROJECT_ID) AND VMT.STATUS=1 GROUP BY VMT.VOUCHER_ID;";

                        //.LATEST OLD 07.07.2022
                        //// Commanded by chinna at 5 PM 06.07.2022, it has to be commanded and need to add the update the values done
                        //query = "SELECT * FROM VOUCHER_MASTER_TRANS VMT INNER JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VMT.VOUCHER_ID \n" +
                        //" INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID WHERE (VMT.VOUCHER_DATE =?VOUCHER_DATE AND (VT.MATERIALIZED_ON  = ?VOUCHER_DATE OR VT.MATERIALIZED_ON IS NULL)) \n" +
                        //" AND CLIENT_CODE <> '' AND CLIENT_REFERENCE_ID <> '0'  AND PROJECT_ID IN (?PROJECT_ID) \n" +
                        //" AND VMT.STATUS=1 GROUP BY VMT.VOUCHER_ID;";

                        //-----OLD1 05.07.2022
                        //query = "SELECT * FROM VOUCHER_MASTER_TRANS VMT INNER JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        //           "INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID INNER JOIN MASTER_LEDGER_GROUP MLG ON MLG.GROUP_ID = ML.GROUP_ID\n" +
                        //           " WHERE ML.GROUP_ID IN (13) AND  VMT.VOUCHER_DATE = ?VOUCHER_DATE \n" +
                        //           " AND VOUCHER_TYPE = 'RC' AND CLIENT_CODE <> '' AND CLIENT_REFERENCE_ID <> '0'  AND PROJECT_ID IN (?PROJECT_ID);";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchVoucherIdByClientRefId:
                    {
                        query = " SELECT VMT.VOUCHER_ID,VMT.VOUCHER_NO FROM VOUCHER_MASTER_TRANS VMT \n" +
                             " INNER JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VMT.VOUCHER_ID LEFT JOIN VOUCHER_TRANS VTT ON VTT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                             " AND VTT.VOUCHER_ID = VT.VOUCHER_ID AND VTT.TRANS_MODE ='DR' LEFT JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VTT.LEDGER_ID\n" +
                             " WHERE CLIENT_CODE = ?CLIENT_CODE AND ML.GROUP_ID NOT IN (13) AND (VMT.CLIENT_REFERENCE_ID =?CLIENT_REFERENCE_ID)\n" +
                             " AND VT.LEDGER_ID IN (?LEDGER_ID) AND PROJECT_ID IN (?PROJECT_ID) AND VMT.STATUS=1 GROUP BY VMT.VOUCHER_ID;";

                        //query = " SELECT VMT.VOUCHER_ID,VMT.VOUCHER_NO  FROM VOUCHER_MASTER_TRANS VMT INNER JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        // " INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID WHERE CLIENT_CODE = ?CLIENT_CODE\n" +
                        // " AND (VMT.CLIENT_REFERENCE_ID =?CLIENT_REFERENCE_ID) AND ML.LEDGER_ID IN (?LEDGER_ID) AND PROJECT_ID IN (?PROJECT_ID) AND VMT.STATUS=1 GROUP BY VMT.VOUCHER_ID;";

                        // BEFORE 21/11/2022
                        //query = " SELECT VMT.VOUCHER_ID,VMT.VOUCHER_NO  FROM VOUCHER_MASTER_TRANS VMT INNER JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        // " INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID WHERE CLIENT_CODE = ?CLIENT_CODE\n" +
                        // " AND (VMT.CLIENT_REFERENCE_ID =?CLIENT_REFERENCE_ID) AND PROJECT_ID IN (?PROJECT_ID) AND VMT.STATUS=1 GROUP BY VMT.VOUCHER_ID;";

                        //query = " SELECT VMT.VOUCHER_ID,VMT.VOUCHER_NO  FROM VOUCHER_MASTER_TRANS VMT INNER JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        // " INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID WHERE CLIENT_CODE = ?CLIENT_CODE\n" +
                        // " AND (VMT.CLIENT_REFERENCE_ID =?CLIENT_REFERENCE_ID) AND PROJECT_ID IN (?PROJECT_ID) AND VMT.STATUS=1 GROUP BY VMT.VOUCHER_ID;";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchVoucherIdByCash:
                    {
                        query = " SELECT VMT.VOUCHER_ID,VMT.VOUCHER_NO  FROM VOUCHER_MASTER_TRANS VMT INNER JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                            " INNER JOIN VOUCHER_TRANS VTT ON VTT.VOUCHER_ID = VMT.VOUCHER_ID AND VTT.VOUCHER_ID = VT.VOUCHER_ID  AND VTT.LEDGER_ID IN (?LEDGER_ID)\n" +
                         " INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID WHERE CLIENT_CODE = ?CLIENT_CODE AND VMT.VOUCHER_DATE =?VOUCHER_DATE AND ML.GROUP_ID =13 \n" +
                         " AND PROJECT_ID IN (?PROJECT_ID) AND VMT.STATUS=1 GROUP BY VMT.VOUCHER_ID;";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchVoucherIdByIndividualCash:
                    {
                        query = " SELECT VMT.VOUCHER_ID,VMT.VOUCHER_NO  FROM VOUCHER_MASTER_TRANS VMT INNER JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                           " INNER JOIN VOUCHER_TRANS VTT ON VTT.VOUCHER_ID = VMT.VOUCHER_ID AND VTT.VOUCHER_ID = VT.VOUCHER_ID  AND VTT.LEDGER_ID IN (?LEDGER_ID)\n" +
                        " INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID WHERE CLIENT_CODE = ?CLIENT_CODE AND VMT.VOUCHER_DATE =?VOUCHER_DATE AND ML.GROUP_ID =13 AND (VMT.CLIENT_REFERENCE_ID =?CLIENT_REFERENCE_ID)\n" +
                        " AND PROJECT_ID IN (?PROJECT_ID) AND VMT.STATUS=1 GROUP BY VMT.VOUCHER_ID;";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchVoucherOnlineCollections: // WHERE CLIENT_CODE LIKE '%ACMETEST - bosdev%
                    {
                        query = "SELECT * FROM VOUCHER_MASTER_TRANS VMT INNER JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                                " INNER JOIN VOUCHER_TRANS VTT ON VTT.VOUCHER_ID = VMT.VOUCHER_ID AND VT.VOUCHER_ID = VTT.VOUCHER_ID\n" +
                                " AND VTT.MATERIALIZED_ON IS NULL AND VTT.LEDGER_ID IN (?SUB_LEDGER_ID)\n" +
                                " WHERE CLIENT_CODE <> '' AND CLIENT_MODE = 'ONLINE' AND ((VMT.VOUCHER_DATE =?VOUCHER_DATE))\n" +
                                " AND PROJECT_ID IN (?PROJECT_ID) AND VT.LEDGER_ID IN (?LEDGER_ID)  AND VMT.STATUS=1 GROUP BY VMT.VOUCHER_ID;";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchOnlineVDateMDateCollections:
                    {
                        query = "SELECT * FROM VOUCHER_MASTER_TRANS VMT INNER JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                                " INNER JOIN VOUCHER_TRANS VTT ON VTT.VOUCHER_ID = VMT.VOUCHER_ID AND VT.VOUCHER_ID = VTT.VOUCHER_ID\n" +
                                " AND IF (?CHEQUE_NO='', VTT.MATERIALIZED_ON IS NULL, VTT.MATERIALIZED_ON =?MATERIALIZED_ON) AND VTT.LEDGER_ID IN (?SUB_LEDGER_ID)\n" +
                                " WHERE CLIENT_CODE <> '' AND CLIENT_MODE = 'ONLINE' AND ((VMT.VOUCHER_DATE =?VOUCHER_DATE))\n" +
                                " AND PROJECT_ID IN (?PROJECT_ID) AND VT.LEDGER_ID IN (?LEDGER_ID)  AND VMT.STATUS=1 GROUP BY VMT.VOUCHER_ID;";

                        //query = "SELECT * FROM VOUCHER_MASTER_TRANS VMT INNER JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        //" INNER JOIN VOUCHER_TRANS VTT ON VTT.VOUCHER_ID = VMT.VOUCHER_ID AND VTT.MATERIALIZED_ON  = ?MATERIALIZED_ON AND VTT.LEDGER_ID IN (?SUB_LEDGER_ID)\n" +
                        //" WHERE CLIENT_CODE <> '' AND CLIENT_MODE = 'ONLINE' AND ((VMT.VOUCHER_DATE =?VOUCHER_DATE))\n" + //  AND VTT.LEDGER_ID IN ("")
                        //" AND PROJECT_ID IN (?PROJECT_ID) AND VT.LEDGER_ID IN (?LEDGER_ID) AND VMT.STATUS=1 GROUP BY VMT.VOUCHER_ID;";

                        //query = "SELECT * FROM VOUCHER_MASTER_TRANS VMT INNER JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                        //      " INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID WHERE CLIENT_CODE <> ''\n" +
                        //      " AND CLIENT_MODE = 'ONLINE' AND ((VMT.VOUCHER_DATE =?VOUCHER_DATE AND VT.MATERIALIZED_ON  = ?MATERIALIZED_ON))\n" +
                        //      " AND PROJECT_ID IN (?PROJECT_ID) AND ML.LEDGER_ID IN (?LEDGER_ID) AND VMT.STATUS=1 GROUP BY VMT.VOUCHER_ID;";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchMatNullVouchers:
                    {
                        query = " SELECT * FROM VOUCHER_MASTER_TRANS VMT INNER JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                                 " WHERE (VMT.VOUCHER_ID = ?VOUCHER_ID AND TRANS_MODE ='DR' AND VT.MATERIALIZED_ON IS NULL AND CLIENT_CODE <> ''\n" +
                                 " AND CLIENT_MODE = 'ONLINE') GROUP BY VMT.VOUCHER_ID;";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchVoucherTransforSSPEmpty:
                    {
                        // query = "SELECT * FROM VOUCHER_MASTER_TRANS WHERE VOUCHER_DATE =?VOUCHER_DATE AND CLIENT_CODE <> ''";
                        query = "SELECT * FROM VOUCHER_MASTER_TRANS WHERE VOUCHER_DATE =?VOUCHER_DATE AND PROJECT_ID IN (?PROJECT_ID) AND CLIENT_CODE <> ''";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchVoucherViewCostcentre:
                    {
                        query = "SELECT VC.LEDGER_ID,VMT.VOUCHER_ID, VC.LEDGER_SEQUENCE_NO, ML.LEDGER_NAME,\n" +
                                "       MC.COST_CENTRE_ID,\n" +
                                "       CONCAT(MCC.COST_CENTRE_CATEGORY_NAME, '-', COST_CENTRE_NAME) AS COST_CENTRE_NAME,\n" +
                                "       AMOUNT\n" +
                                "  FROM VOUCHER_CC_TRANS AS VC\n" +
                                " INNER JOIN MASTER_COST_CENTRE MC\n" +
                                "    ON VC.COST_CENTRE_ID = MC.COST_CENTRE_ID\n" +
                                " INNER JOIN COSTCATEGORY_COSTCENTRE CCC\n" +
                                "    ON CCC.COST_CENTRE_ID = MC.COST_CENTRE_ID\n" +
                                " INNER JOIN MASTER_COST_CENTRE_CATEGORY MCC\n" +
                                "    ON MCC.COST_CENTRECATEGORY_ID = CCC.COST_CATEGORY_ID\n" +
                                " INNER JOIN VOUCHER_MASTER_TRANS VMT\n" +
                                "    ON VMT.VOUCHER_ID = VC.VOUCHER_ID\n" +
                                " INNER JOIN MASTER_LEDGER ML \n" +
                                "   ON ML.LEDGER_ID=VC.LEDGER_ID \n" +
                                " WHERE VMT.PROJECT_ID = ?PROJECT_ID AND VMT.STATUS=1 \n" +
                                "   AND VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED AND VMT.VOUCHER_ID IN (?VOUCHER_ID) GROUP BY VOUCHER_ID,LEDGER_ID,COST_CENTRE_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchDeletedVoucherViewCostCentre:
                    {
                        query = "SELECT VC.LEDGER_ID,VMT.VOUCHER_ID,ML.LEDGER_NAME,\n" +
                                "       MC.COST_CENTRE_ID,\n" +
                                "       CONCAT(MCC.COST_CENTRE_CATEGORY_NAME, '-', COST_CENTRE_NAME) AS COST_CENTRE_NAME,\n" +
                                "       AMOUNT\n" +
                                "  FROM VOUCHER_CC_TRANS AS VC\n" +
                                " INNER JOIN MASTER_COST_CENTRE MC\n" +
                                "    ON VC.COST_CENTRE_ID = MC.COST_CENTRE_ID\n" +
                                " INNER JOIN COSTCATEGORY_COSTCENTRE CCC\n" +
                                "    ON CCC.COST_CENTRE_ID = MC.COST_CENTRE_ID\n" +
                                " INNER JOIN MASTER_COST_CENTRE_CATEGORY MCC\n" +
                                "    ON MCC.COST_CENTRECATEGORY_ID = CCC.COST_CATEGORY_ID\n" +
                                " INNER JOIN VOUCHER_MASTER_TRANS VMT\n" +
                                "    ON VMT.VOUCHER_ID = VC.VOUCHER_ID\n" +
                                " INNER JOIN MASTER_LEDGER ML \n" +
                                "   ON ML.LEDGER_ID=VC.LEDGER_ID \n" +
                                " WHERE VMT.PROJECT_ID = ?PROJECT_ID AND VMT.STATUS=0 \n" +
                                "   AND VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED AND VMT.VOUCHER_ID IN (?VOUCHER_ID)";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchNegativeBalanceHistory:
                    {
                        //query = "SELECT BALANCE_DATE,\n" +
                        //        "       CASE\n" +
                        //        "         WHEN ML.GROUP_ID = 13 THEN\n" +
                        //        "          AMOUNT\n" +
                        //        "         ELSE\n" +
                        //        "          0.00\n" +
                        //        "       END AS CASH,\n" +
                        //        "       CASE\n" +
                        //        "         WHEN ML.GROUP_ID = 12 THEN\n" +
                        //        "         AMOUNT\n" +
                        //        "         ELSE\n" +
                        //        "          0.00\n" +
                        //        "       END AS BANK,'CR' as TRANS_MODE\n" +
                        //        "  FROM LEDGER_BALANCE LB\n" +
                        //        " INNER JOIN MASTER_LEDGER ML\n" +
                        //        "    ON ML.LEDGER_ID = LB.LEDGER_ID\n" +
                        //        " WHERE MONTH(BALANCE_DATE) = MONTH(?VOUCHER_DATE)\n" +
                        //        "   AND TRANS_MODE = 'CR'\n" +
                        //        "   AND PROJECT_ID = ?PROJECT_ID\n" +
                        //        "   AND AMOUNT > 0\n" +
                        //        "   AND ML.GROUP_ID IN (12, 13)\n" +
                        //        "   AND LB.LEDGER_ID IN (SELECT LEDGER_ID\n" +
                        //        "                          FROM LEDGER_BALANCE\n" +
                        //        "                         WHERE PROJECT_ID = ?PROJECT_ID\n" +
                        //        "                           AND AMOUNT > 0)";
                        query = "SELECT TT.BALANCE_DATE,\n" +
                                "       SUM(TT.CASH) AS CASH,\n" +
                                "       SUM(TT.BANK) AS BANK,\n" +
                                "       TT.TRANS_MODE\n" +
                                "  FROM (SELECT BALANCE_DATE,\n" +
                                "       CASE\n" +
                                "         WHEN ML.GROUP_ID = 13 THEN\n" +
                                "          AMOUNT\n" +
                                "         ELSE\n" +
                                "          0.00\n" +
                                "       END AS CASH,\n" +
                                "       CASE\n" +
                                "         WHEN ML.GROUP_ID = 12 THEN\n" +
                                "          AMOUNT\n" +
                                "         ELSE\n" +
                                "          0.00\n" +
                                "       END AS BANK,\n" +
                                "       'CR' AS TRANS_MODE\n" +
                                "  FROM LEDGER_BALANCE LB\n" +
                                " INNER JOIN MASTER_LEDGER ML\n" +
                                "    ON ML.LEDGER_ID = LB.LEDGER_ID\n" +
                                " WHERE MONTH(BALANCE_DATE) = MONTH(?VOUCHER_DATE)\n" +
                                "   AND TRANS_MODE = 'CR'\n" +
                                "   AND LB.PROJECT_ID = ?PROJECT_ID\n" +
                                "   AND AMOUNT > 0\n" +
                                "   AND ML.GROUP_ID IN (12, 13)\n" +
                                "   AND LB.BALANCE_DATE IN\n" +
                                "       (SELECT VMT.VOUCHER_DATE\n" +
                                "          FROM VOUCHER_MASTER_TRANS VMT\n" +
                                "         INNER JOIN VOUCHER_TRANS VT\n" +
                                "            ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                "         INNER JOIN MASTER_LEDGER ML\n" +
                                "            ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                                "         WHERE VMT.STATUS = 1\n" +
                                "           AND ML.GROUP_ID IN (12, 13)\n" +
                                "           AND MONTH(VOUCHER_DATE) = MONTH(?VOUCHER_DATE)\n" +
                                "           AND VMT.PROJECT_ID = ?PROJECT_ID\n" +
                                "         GROUP BY VMT.VOUCHER_DATE) \n" +
                                "         ORDER BY BALANCE_DATE) AS TT\n" +
                                " GROUP BY BALANCE_DATE";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchTDSBookingId:
                    {
                        query = "SELECT CLIENT_REFERENCE_ID, CLIENT_CODE FROM VOUCHER_MASTER_TRANS WHERE VOUCHER_ID=?VOUCHER_ID AND STATUS=1";
                        break;
                    }

                case SQLCommand.VoucherMaster.FetchPostIdByVoucherId:
                    {
                        query = "SELECT VMT.CLIENT_REFERENCE_ID\n" +
                                "  FROM VOUCHER_MASTER_TRANS VMT\n" +
                                " INNER JOIN VOUCHER_TRANS VT\n" +
                                "    ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                                " WHERE VT.VOUCHER_ID = ?VOUCHER_ID AND VMT.VOUCHER_SUB_TYPE='PAY' AND VMT.STATUS=1;";
                        break;
                    }
                case SQLCommand.VoucherMaster.CheckLedgerMappedByProjectVoucher:
                    {
                        query = "SELECT IF(LEDCOUNT = COUNT(*), 1 , 0) AS MAPPEDCOUNT\n" +  // LEDCOUNT
                                "  FROM PROJECT_LEDGER PL\n" +
                                "\n" +
                                "  JOIN (SELECT LEDGER_ID\n" +
                                "          FROM VOUCHER_MASTER_TRANS VMT\n" +
                                "         INNER JOIN VOUCHER_TRANS VT\n" +
                                "            ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                "         WHERE VMT.VOUCHER_ID IN(?VOUCHER_ID)) AS T\n" +
                                "    ON PL.LEDGER_ID = T.LEDGER_ID\n" +
                                "   AND PL.PROJECT_ID IN(?PROJECT_ID)\n" +
                                "\n" +
                                "  JOIN (SELECT COUNT(LEDGER_ID) AS LEDCOUNT\n" +
                                "          FROM VOUCHER_MASTER_TRANS VMT\n" +
                                "         INNER JOIN VOUCHER_TRANS VT\n" +
                                "            ON VMT.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                "         WHERE VMT.VOUCHER_ID IN(?VOUCHER_ID)) AS TCOUNT;";
                        break;
                    }
                case SQLCommand.VoucherMaster.CheckLedgerMappedByProject:
                    {
                        query = "SELECT COUNT(*) AS LEDGER_ID\n" +
                                "  FROM PROJECT_LEDGER\n" +
                                " WHERE LEDGER_ID =?LEDGER_ID\n" +
                                " AND PROJECT_ID =?PROJECT_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.CheckVoucherTypeMappedByProject:
                    {
                        query = "SELECT COUNT(*) AS VOUCHER_ID\n" +
                                "  FROM PROJECT_VOUCHER\n" +
                                " WHERE VOUCHER_ID=?VOUCHER_ID\n" +
                                " AND PROJECT_ID =?PROJECT_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.CheckVoucherDeleted:
                    {
                        query = "SELECT VOUCHER_ID FROM VOUCHER_MASTER_TRANS WHERE STATUS = 1 AND VOUCHER_ID = ?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchReferenceBalance:
                    {
                        query = "SELECT VT.VOUCHER_ID AS REF_VOUCHER_ID, VT.LEDGER_ID, VT.REFERENCE_NUMBER, DATE_FORMAT(VMT.VOUCHER_DATE,'%d/%m/%Y') AS VOUCHER_DATE, \n" +
                                "CONCAT(CONCAT(VT.REFERENCE_NUMBER , CONCAT(' - ', SUM(VT.AMOUNT))), ' - ' , DATE_FORMAT(VMT.VOUCHER_DATE,'%d/%m/%Y')) AS REFERENCE_NUMBER,\n" +
                                 "ML.LEDGER_NAME,\n" +
                                " (SUM(VT.AMOUNT) - IFNULL(BILLING.AMOUNT,0)) AS BALANCE, SUM(VT.AMOUNT) AS TOTAL_REF_AMOUNT\n" +
                                " FROM VOUCHER_MASTER_TRANS VMT\n" +
                                " INNER JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID = VMT.VOUCHER_ID\n" +
                                " INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID\n" +
                                " LEFT JOIN (SELECT REF_VOUCHER_ID,  LEDGER_ID, SUM(IFNULL(AMOUNT,0)) AS AMOUNT FROM VOUCHER_REFERENCE WHERE REC_PAY_VOUCHER_ID <> ?REC_PAY_VOUCHER_ID \n" +
                                " GROUP BY REF_VOUCHER_ID, LEDGER_ID) BILLING ON BILLING.REF_VOUCHER_ID = VT.VOUCHER_ID AND BILLING.LEDGER_ID = VT.LEDGER_ID\n" +
                                " WHERE VMT.PROJECT_ID =?PROJECT_ID AND VT.LEDGER_ID = ?LEDGER_ID AND VMT.STATUS = 1 AND VT.REFERENCE_NUMBER <>'' AND \n" +
                                " VMT.VOUCHER_TYPE ='" + Utility.VoucherSubTypes.JN.ToString() + "' AND VT.TRANS_MODE ='" + Utility.TransSource.Cr.ToString() + "'\n" +
                                " AND ML.GROUP_ID IN (" + (Int32)Utility.TDSDefaultLedgers.SunderyCreditors + "," + (Int32)Utility.TDSDefaultLedgers.SundryDebtors + ")\n" +
                                " AND VMT.VOUCHER_DATE <= ?VOUCHER_DATE\n" +
                                " GROUP BY VT.VOUCHER_ID, VT.LEDGER_ID, VT.REFERENCE_NUMBER\n" +
                                " ORDER BY VMT.VOUCHER_DATE, VT.REFERENCE_NUMBER";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchVoucherLedgerReferenceDetails:
                    {
                        //query = "SELECT REC_PAY_VOUCHER_ID, REF_VOUCHER_ID, LEDGER_ID, AMOUNT, AMOUNT AS REF_VALIDATION_AMOUNT FROM VOUCHER_REFERENCE \n" +
                        //        " WHERE REC_PAY_VOUCHER_ID=?REC_PAY_VOUCHER_ID";

                        query = "SELECT REC_PAY_VOUCHER_ID, REF_VOUCHER_ID, LEDGER_ID, AMOUNT, AMOUNT AS REF_VALIDATION_AMOUNT,DATE_FORMAT(VMT.VOUCHER_DATE,'%d/%m/%Y') AS REF_VOUCHER_DATE FROM VOUCHER_REFERENCE VR\n" +
                                "INNER JOIN VOUCHER_MASTER_TRANS VMT ON VMT.VOUCHER_ID = VR.REF_VOUCHER_ID\n" +
                                    " WHERE REC_PAY_VOUCHER_ID=?REC_PAY_VOUCHER_ID";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchVoucherLedgerSubLedgerVouchers:
                    {
                        query = "SELECT IFNULL(VT.VOUCHER_ID, 0) AS VOUCHER_ID, IFNULL(VT.SEQUENCE_NO, 0) AS SEQUENCE_NO, LSL.LEDGER_ID,\n" +
                                "MSL.SUB_LEDGER_ID, MSL.SUB_LEDGER_NAME, VT.AMOUNT, VT.AMOUNT AS TMP_AMOUNT, '0' AS SUB_LEDGER_BALANCE, \n" +
                                "IFNULL(BU.BUDGET_ID, 0) AS BUDGET_ID, IF(BU.BUDGET_ID IS NULL, 0, 1) AS IS_BUDGET, IFNULL(BU.SUB_LEDGER_AMOUNT,0 ) AS BUDGET_AMOUNT, \n" +
                                "BU.DATE_FROM, BU.DATE_TO, '0' AS BUDGET_VARIANCE, BU.BUDGET_TRANS_MODE\n" +
                                "FROM MASTER_SUB_LEDGER MSL\n" +
                                "INNER JOIN LEDGER_SUB_LEDGER LSL ON LSL.SUB_LEDGER_ID = MSL.SUB_LEDGER_ID\n" +
                                "LEFT JOIN (SELECT VM.VOUCHER_ID, IFNULL(VST.SEQUENCE_NO,0) AS SEQUENCE_NO, VT.LEDGER_ID, IFNULL(VST.SUB_LEDGER_ID,0) AS SUB_LEDGER_ID, IFNULL(VST.Amount, 0) AS AMOUNT\n" +
                                " FROM VOUCHER_MASTER_TRANS VM\n" +
                                " INNER JOIN VOUCHER_TRANS VT ON VM.VOUCHER_ID = VT.VOUCHER_ID\n" +
                                " LEFT JOIN VOUCHER_SUB_LEDGER_TRANS VST ON VST.VOUCHER_ID = VT.VOUCHER_ID AND VST.LEDGER_ID = VT.LEDGER_ID\n" +
                                " WHERE VM.PROJECT_ID =?PROJECT_ID AND VM.VOUCHER_ID = ?VOUCHER_ID) AS VT ON VT.LEDGER_ID = LSL.LEDGER_ID AND VT.SUB_LEDGER_ID = MSL.SUB_LEDGER_ID\n" +
                                "LEFT JOIN (SELECT BM.BUDGET_ID, BM.DATE_FROM, BM.DATE_TO, BSL.LEDGER_ID, BSL.SUB_LEDGER_ID, 0 AS LEDGER_AMOUNT,\n" +
                                "BSL.APPROVED_AMOUNT AS SUB_LEDGER_AMOUNT,BSL.TRANS_MODE AS BUDGET_TRANS_MODE\n" +
                                " FROM BUDGET_MASTER AS BM\n" +
                                " -- INNER JOIN BUDGET_LEDGER AS BL ON BL.BUDGET_ID = BM.BUDGET_ID\n" +
                                " INNER JOIN BUDGET_SUB_LEDGER AS BSL ON BM.BUDGET_ID = BSL.BUDGET_ID \n" + //AND BSL.LEDGER_ID = BL.LEDGER_ID
                                " WHERE BM.IS_ACTIVE=1 \n" + //AND BM.BUDGET_ACTION = 0 
                                "{AND ?VOUCHER_DATE BETWEEN BM.DATE_FROM AND BM.DATE_TO} {AND BSL.TRANS_MODE = ?TRANS_MODE}\n" +
                                " AND (BSL.APPROVED_AMOUNT >0)) AS BU ON BU.LEDGER_ID = LSL.LEDGER_ID AND BU.SUB_LEDGER_ID = MSL.SUB_LEDGER_ID\n" + //BL.APPROVED_AMOUNT >0 OR 
                                "ORDER BY MSL.SUB_LEDGER_NAME";
                        break;
                    }
                case SQLCommand.VoucherMaster.FetchLedgerByAmount:
                    {
                        query = "SELECT SUM(AMOUNT) AS JOURNAL_REF_AMOUNT FROM VOUCHER_REFERENCE WHERE REF_VOUCHER_ID = ?REF_VOUCHER_ID";
                        break;
                    }
            }
            return query;
        }
        #endregion Bank SQL
    }
}
