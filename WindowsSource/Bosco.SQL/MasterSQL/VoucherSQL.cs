using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class VoucherSQL : IDatabaseQuery
    {
        #region ISQLServerQuery Members

        DataCommandArguments dataCommandArgs;
        SQLType sqlType;
        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string Query = string.Empty;
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.Voucher).FullName)
            {
                Query = VoucherSQLManipulition();
            }

            sqlType = this.sqlType;
            return Query;
        }
        #endregion

        #region SQL Script
        private string VoucherSQLManipulition()
        {
            string Query = string.Empty;
            SQLCommand.Voucher sqlQueryType = (SQLCommand.Voucher)(this.dataCommandArgs.SQLCommandId);
            switch (sqlQueryType)
            {
                #region Insert Query
                case SQLCommand.Voucher.Add:
                    Query = "INSERT INTO MASTER_VOUCHER" +
                            "(" +
                                 "VOUCHER_NAME," +
                                 "VOUCHER_TYPE," +
                                 "VOUCHER_METHOD," +
                                 "PREFIX_CHAR," +
                                 "SUFFIX_CHAR," +
                                 "STARTING_NUMBER," +
                                 "NUMBERICAL_WITH," +
                                 "PREFIX_WITH_ZERO," +
                                 "MONTH," +
                                 "DURATION," +
                                 "ALLOW_DUPLICATE," +
                                 "NOTE,IS_NARRATION_ENABLED" +
                            ")" +
                            " VALUES" +
                            "(" +
                                 "?VOUCHER_NAME," +
                                 "?VOUCHER_TYPE," +
                                 "?VOUCHER_METHOD," +
                                 "?PREFIX_CHAR," +
                                 "?SUFFIX_CHAR," +
                                 "?STARTING_NUMBER," +
                                 "?NUMBERICAL_WITH," +
                                 "?PREFIX_WITH_ZERO," +
                                 "?MONTH," +
                                 "?DURATION," +
                                 "?ALLOW_DUPLICATE," +
                                 "?NOTE," +
                                 "?IS_NARRATION_ENABLED" +
                            ");";
                    break;
                #endregion

                #region Updat Query
                case SQLCommand.Voucher.Update:
                    Query = "UPDATE MASTER_VOUCHER " +
                                  "SET " +
                                        "VOUCHER_NAME=?VOUCHER_NAME," +
                                        "VOUCHER_TYPE=?VOUCHER_TYPE," +
                                        "VOUCHER_METHOD= ?VOUCHER_METHOD," +
                                        "PREFIX_CHAR=?PREFIX_CHAR," +
                                        "SUFFIX_CHAR=?SUFFIX_CHAR," +
                                        "STARTING_NUMBER=?STARTING_NUMBER," +
                                        "NUMBERICAL_WITH=?NUMBERICAL_WITH," +
                                        "PREFIX_WITH_ZERO=?PREFIX_WITH_ZERO," +
                                        "MONTH=?MONTH," +
                                        "DURATION=?DURATION," +
                                        "ALLOW_DUPLICATE=?ALLOW_DUPLICATE," +
                                        "NOTE=?NOTE, " +
                                        "IS_NARRATION_ENABLED = ?IS_NARRATION_ENABLED " +
                                 "WHERE VOUCHER_ID=?VOUCHER_ID;";
                    break;
                #endregion

                #region Delete Query
                case SQLCommand.Voucher.Delete:
                    Query = "DELETE FROM MASTER_VOUCHER WHERE VOUCHER_ID=?VOUCHER_ID";
                    break;
                #endregion

                #region Fetch Query
                case SQLCommand.Voucher.FetchByVoucherId:
                    Query = "SELECT "
                                     + "VOUCHER_ID,"
                                     + "VOUCHER_NAME,"
                                     + "VOUCHER_TYPE,"
                                    + "VOUCHER_METHOD,"
                                    + "PREFIX_CHAR,"
                                    + "SUFFIX_CHAR,"
                                    + "STARTING_NUMBER,"
                                    + "NUMBERICAL_WITH,"
                                    + "PREFIX_WITH_ZERO,"
                                    + "MONTH,"
                                    + "DURATION,"
                                    + "ALLOW_DUPLICATE,"
                                    + "NOTE,IS_NARRATION_ENABLED"
                             + " FROM MASTER_VOUCHER"
                                  + " WHERE VOUCHER_ID=?VOUCHER_ID";
                    break;
                case SQLCommand.Voucher.FetchByVoucherTypeName:
                    Query = "SELECT "
                                     + "VOUCHER_ID,"
                                     + "VOUCHER_NAME,"
                                     + "VOUCHER_TYPE,"
                                    + "VOUCHER_METHOD,"
                                    + "PREFIX_CHAR,"
                                    + "SUFFIX_CHAR,"
                                    + "STARTING_NUMBER,"
                                    + "NUMBERICAL_WITH,"
                                    + "PREFIX_WITH_ZERO,"
                                    + "MONTH,"
                                    + "DURATION,"
                                    + "ALLOW_DUPLICATE,"
                                    + "NOTE,IS_NARRATION_ENABLED"
                             + " FROM MASTER_VOUCHER"
                                  + " WHERE VOUCHER_NAME=?VOUCHER_NAME";
                    break;
                case SQLCommand.Voucher.FetchAll:
                    Query = "SELECT " +
                                    "VOUCHER_ID," +
                                     "VOUCHER_NAME," +
                                     " CASE" +
                                        " WHEN" +
                                               " VOUCHER_TYPE=1 THEN 'Receipt'" +
                                         " WHEN" +
                                                " VOUCHER_TYPE=2 THEN 'Payment'" +
                                         " WHEN" +
                                                " VOUCHER_TYPE=3 THEN 'Contra'" +
                                         " ELSE" +
                                                 " 'Journal'" +
                                        " END  AS 'VOUCHER_TYPE'," +
                                      " CASE" +
                                         " WHEN" +
                                                 " VOUCHER_METHOD=1 THEN 'Automatic'" +
                                         " ELSE" +
                                                 " 'Manual'" +
                                     "END AS  VOUCHER_METHOD," +
                                    " PREFIX_CHAR," +
                                     "SUFFIX_CHAR," +
                                     "STARTING_NUMBER," +
                                     "NUMBERICAL_WITH," +
                                     "PREFIX_WITH_ZERO," +
                                     "MONTH" +
                                  " FROM MASTER_VOUCHER" +
                                  " ORDER BY VOUCHER_ID, VOUCHER_NAME ASC"; //" ORDER BY VOUCHER_NAME ASC;";
                    break;
                case SQLCommand.Voucher.FetchVoucherNumberFormat:
                    {
                        Query = "SELECT" +
                            // " CONCAT(PREFIX_CHAR,CONCAT('#',STARTING_NUMBER ),CONCAT('#',SUFFIX_CHAR)) AS 'VOUCHER_NUMBER' " +
                                " MV.VOUCHER_ID, PREFIX_CHAR,STARTING_NUMBER,SUFFIX_CHAR,NUMBERICAL_WITH,PREFIX_WITH_ZERO,MONTH,DURATION " +
                                " FROM MASTER_VOUCHER AS MV " +
                                " INNER JOIN PROJECT_VOUCHER AS MPV ON " +
                                " MV.VOUCHER_ID=MPV.VOUCHER_ID " +
                                " WHERE MPV.PROJECT_ID=?PROJECT_ID " +
                                " AND MV.VOUCHER_TYPE=?VOUCHER_TYPE AND MV.VOUCHER_ID=?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.Voucher.UpdateLastVoucherNumber:
                    {
                        Query = " UPDATE VOUCHER_NUMBER_FORMAT " +
                                    " SET " +
                                    " NUMBER_FORMAT_ID=?NUMBER_FORMAT_ID, " +
                                    " LAST_VOUCHER_NUMBER=?LAST_VOUCHER_NUMBER, " +
                                    " RUNNING_NUMBER=?RUNNING_NUMBER, " +
                                    " NUMBER_FORMAT=?NUMBER_FORMAT, " +
                                    " MONTH=?MONTH, " +
                                    " DURATION=?DURATION, " +
                                    " VOUCHER_MONTH=?VOUCHER_MONTH, " +
                                    " VOUCHER_YEAR =?VOUCHER_YEAR, " +
                                    " PROJECT_ID =?PROJECT_ID " +
                                    " WHERE NUMBER_ID=?NUMBER_ID ";

                        break;
                    }
                case SQLCommand.Voucher.InsertVoucherNumber:
                    {
                        Query = " INSERT INTO VOUCHER_NUMBER_FORMAT (NUMBER_FORMAT_ID, " +
                                              " LAST_VOUCHER_NUMBER, " +
                                              " RUNNING_NUMBER, " +
                                              " NUMBER_FORMAT,MONTH,VOUCHER_MONTH,DURATION,VOUCHER_YEAR,PROJECT_ID,VOUCHER_DEFINITION_ID) VALUES" +
                                              " (?NUMBER_FORMAT_ID,  " +
                                              " ?LAST_VOUCHER_NUMBER , " +
                                              " ?RUNNING_NUMBER,  " +
                                              " ?NUMBER_FORMAT,?MONTH,?VOUCHER_MONTH,?DURATION,?VOUCHER_YEAR,?PROJECT_ID,?VOUCHER_DEFINITION_ID) ";
                        break;
                    }
                case SQLCommand.Voucher.DeleteVoucherNumberFormat:
                    {
                        Query = "DELETE FROM VOUCHER_NUMBER_FORMAT " +
                            " WHERE NUMBER_FORMAT_ID=?NUMBER_FORMAT_ID AND VOUCHER_MONTH=?VOUCHER_MONTH AND " +
                            " VOUCHER_YEAR=?VOUCHER_YEAR AND PROJECT_ID=?PROJECT_ID AND VOUCHER_DEFINITION_ID=?VOUCHER_DEFINITION_ID";
                        break;
                    }
                case SQLCommand.Voucher.DeleteVoucherNumberFormatByTransType:
                    {
                        Query = "DELETE FROM VOUCHER_NUMBER_FORMAT " +
                            " WHERE NUMBER_FORMAT_ID=?NUMBER_FORMAT_ID AND PROJECT_ID=?PROJECT_ID AND VOUCHER_DEFINITION_ID=?VOUCHER_DEFINITION_ID";
                        break;
                    }
                case SQLCommand.Voucher.FetchVoucherNumberFormatExist:
                    {
                        Query = "SELECT " +
                                  " NUMBER_ID, " +
                                  " NUMBER_FORMAT_ID, " +
                                  " LAST_VOUCHER_NUMBER, " +
                                  " RUNNING_NUMBER, " +
                                  " NUMBER_FORMAT, " +
                                  " MONTH, " +
                                  " VOUCHER_MONTH,  " +
                                  " DURATION,  " +
                                  " VOUCHER_YEAR,PROJECT_ID " +
                                  " FROM VOUCHER_NUMBER_FORMAT WHERE NUMBER_FORMAT_ID=?NUMBER_FORMAT_ID  " +
                                  " {AND VOUCHER_MONTH=?VOUCHER_MONTH} " +
                                  " AND VOUCHER_YEAR=?VOUCHER_YEAR AND NUMBER_FORMAT=?NUMBER_FORMAT AND PROJECT_ID=?PROJECT_ID AND VOUCHER_DEFINITION_ID=?VOUCHER_DEFINITION_ID";
                        break;
                    }
                case SQLCommand.Voucher.FetchPreviousVoucherNumberFormatExist:
                    {
                        Query = "SELECT " +
                                  " NUMBER_ID, " +
                                  " NUMBER_FORMAT_ID, " +
                                  " LAST_VOUCHER_NUMBER, " +
                                  " RUNNING_NUMBER, " +
                                  " NUMBER_FORMAT, " +
                                  " MONTH, " +
                                  " VOUCHER_MONTH,  " +
                                  " DURATION,  " +
                                  " VOUCHER_YEAR,PROJECT_ID " +
                                  " FROM VOUCHER_NUMBER_FORMAT WHERE NUMBER_FORMAT_ID=?NUMBER_FORMAT_ID  " +
                                  " AND VOUCHER_YEAR=?VOUCHER_YEAR AND NUMBER_FORMAT=?NUMBER_FORMAT AND PROJECT_ID=?PROJECT_ID AND VOUCHER_DEFINITION_ID=?VOUCHER_DEFINITION_ID" +
                                  " ORDER BY NUMBER_ID DESC LIMIT 1";
                        break;
                    }
                case SQLCommand.Voucher.FetchLastResetMonth:
                    {
                        Query = "SELECT MONTH(VOUCHER_DATE) AS MONTH " +
                                  "FROM VOUCHER_MASTER_TRANS " +
                                 "WHERE VOUCHER_DATE >= ?DATE_STARTED " +
                                   "AND VOUCHER_DATE < ?VOUCHER_DATE AND VOUCHER_TYPE=?VOUCHER_TYPE " +
                                 "ORDER BY VOUCHER_DATE DESC LIMIT 1";
                        break;
                    }

                case SQLCommand.Voucher.UpdateLastAssetRunning:
                    {
                        Query = "UPDATE ASSET_ITEM SET RUNNING_NUMBER=?RUNNING_NUMBER WHERE ITEM_ID=?ITEM_ID";
                        break;
                    }
                case SQLCommand.Voucher.UpdateAssetID:
                    {
                        Query = "UPDATE ASSET_ITEM_DETAIL SET ASSET_ID=?ASSET_ID WHERE ITEM_DETAIL_ID=?ITEM_DETAIL_ID";
                        break;
                    }
                case SQLCommand.Voucher.CheckNarrationEnabledByTransType:
                    {
                        Query = "SELECT IS_NARRATION_ENABLED FROM MASTER_VOUCHER WHERE VOUCHER_TYPE=?VOUCHER_TYPE";
                        break;
                    }


                #region CashBankVoucherReceipts
                case SQLCommand.Voucher.CashBankVoucherReceipts:
                    {
                        Query = "SELECT VMT.VOUCHER_ID,VMT.VOUCHER_NO,VMT.VOUCHER_DATE,VMT.NAME_ADDRESS, " +
                                    "VT.SEQUENCE_NO, " +
                                    "ML.LEDGER_NAME, " +
                                    "SUM(VT.AMOUNT) AS AMOUNT, " +
                                    "CONCAT(IFNULL(VMT.NARRATION, ''), IF(VMT.VOUCHER_SUB_TYPE='FD', CONCAT(' FD: ', FD.FD_ACCOUNT_NUMBER), '') ) AS NARRATION " +
                                    "FROM VOUCHER_MASTER_TRANS VMT " +
                                    "LEFT JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID=VMT.VOUCHER_ID " +
                                    "LEFT JOIN MASTER_LEDGER ML ON ML.LEDGER_ID=VT.LEDGER_ID " +
                                    "LEFT JOIN MASTER_PROJECT MP ON MP.PROJECT_ID=VMT.PROJECT_ID " +
                                    "LEFT JOIN (SELECT FDA.FD_ACCOUNT_ID, 0 AS VOUCHER_ID, RNO.FD_INTEREST_VOUCHER_ID, RNO.FD_VOUCHER_ID,\n" +
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
                                    "WHERE MP.PROJECT_ID IN (?PROJECT_ID) AND VOUCHER_TYPE ='RC' AND TRANS_MODE ='CR' " +
                                    "AND VMT.VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED AND VMT.STATUS=1 GROUP BY  VT.VOUCHER_ID ORDER BY VMT.VOUCHER_DATE,VMT.VOUCHER_NO ASC"; // ,VT.LEDGER_ID
                        break;
                    }
                case SQLCommand.Voucher.CashBankVoucherContra: //On 01/02/2018, to show contra voucher also
                    {
                        Query = "SELECT VMT.VOUCHER_ID,VMT.VOUCHER_NO,VMT.VOUCHER_DATE,VMT.NAME_ADDRESS,\n" +
                                    "VT.SEQUENCE_NO,\n" +
                                    "ML.LEDGER_NAME,\n" +
                                    "SUM(VT.AMOUNT) AS AMOUNT,\n" +
                                    "CONCAT(VMT.NARRATION, ' FD: ', FD.FD_ACCOUNT_NUMBER) AS NARRATION\n" +
                                    "FROM VOUCHER_MASTER_TRANS VMT\n" +
                                    "LEFT JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID=VMT.VOUCHER_ID\n" +
                                    "LEFT JOIN MASTER_LEDGER ML ON ML.LEDGER_ID=VT.LEDGER_ID\n" +
                                    "LEFT JOIN MASTER_PROJECT MP ON MP.PROJECT_ID=VMT.PROJECT_ID\n" +
                                    "LEFT JOIN (SELECT FDA.FD_ACCOUNT_ID, 0 AS VOUCHER_ID, RNO.FD_INTEREST_VOUCHER_ID, RNO.FD_VOUCHER_ID,\n" +
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
                                    "WHERE MP.PROJECT_ID IN (?PROJECT_ID) AND VOUCHER_TYPE ='CN' AND TRANS_MODE ='CR' " +
                                    "AND VMT.VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED AND VMT.STATUS=1 GROUP BY  VT.VOUCHER_ID ORDER BY VMT.VOUCHER_DATE,VMT.VOUCHER_NO ASC"; // ,VT.LEDGER_ID
                        break;
                    }
                #endregion

                #region CashBankVoucherPayments
                case SQLCommand.Voucher.CashBankVoucher:
                    {
                        Query = "SELECT VMT.VOUCHER_ID,VMT.VOUCHER_NO,VOUCHER_DATE,VMT.NAME_ADDRESS,\n" +
                                    "VT.SEQUENCE_NO,\n" +
                                    "ML.LEDGER_NAME,\n" +
                                    "SUM(VT.AMOUNT) AS AMOUNT,\n" +
                                    "VMT.NARRATION\n" +
                                    "FROM VOUCHER_MASTER_TRANS VMT\n" +
                                    "LEFT JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID=VMT.VOUCHER_ID\n" +
                                    "LEFT JOIN MASTER_LEDGER ML ON ML.LEDGER_ID=VT.LEDGER_ID\n" +
                                    "LEFT JOIN MASTER_PROJECT MP ON MP.PROJECT_ID=VMT.PROJECT_ID\n" +
                                    "WHERE MP.PROJECT_ID  IN (?PROJECT_ID) AND VOUCHER_TYPE ='PY' AND TRANS_MODE='DR' " +
                                    "AND VMT.VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED  AND VMT.STATUS=1 GROUP BY VT.VOUCHER_ID ORDER BY VMT.VOUCHER_DATE,VMT.VOUCHER_NO "; // ,VT.LEDGER_ID
                        break;
                    }
                case SQLCommand.Voucher.GSTInvoiceVoucher:
                    {
                        Query = @"SELECT VMT.VOUCHER_ID, VMT.PROJECT_ID, VMT.VOUCHER_NO, VMT.VOUCHER_DATE, T.LEDGER_NAME,
                                  VMT.VOUCHER_SUB_TYPE, T.DEBIT AS AMOUNT, SUM(VT.GST) AS GST,  VMT.NARRATION AS NARRATION,  VMT.NAME_ADDRESS,
                                     CONCAT(CONCAT(VMT.GST_VENDOR_INVOICE_NO, CONCAT( ' - ' , DATE(VMT.GST_VENDOR_INVOICE_DATE))), CONCAT(' - ', ASV.VENDOR)) AS GST_VENDOR_INVOICE_NO
                                 FROM VOUCHER_MASTER_TRANS VMT
                                 INNER JOIN VOUCHER_TRANS VT ON VMT.VOUCHER_ID = VT.VOUCHER_ID
                                 LEFT JOIN MASTER_VOUCHER AS MV ON MV.VOUCHER_ID = VMT.VOUCHER_DEFINITION_ID
                                 LEFT JOIN (SELECT VMT.VOUCHER_ID, CONCAT(ML.LEDGER_NAME) AS LEDGER_NAME, IFNULL(SUM(AMOUNT), 0) AS DEBIT
                                             FROM VOUCHER_MASTER_TRANS VMT
                                             INNER JOIN VOUCHER_TRANS VT ON VMT.VOUCHER_ID = VT.VOUCHER_ID
                                             INNER JOIN MASTER_LEDGER ML ON ML.LEDGER_ID = VT.LEDGER_ID
                                             WHERE VMT.PROJECT_ID IN (?PROJECT_ID) AND VMT.VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED
                                                   AND ((VOUCHER_TYPE IN ('PY', 'JN') AND TRANS_MODE='DR') OR (VOUCHER_TYPE ='RC' AND TRANS_MODE='CR')) 
                                             GROUP BY VMT.VOUCHER_ID ORDER BY VMT.VOUCHER_NO, VT.LEDGER_ID) AS T ON VT.VOUCHER_ID = T.VOUCHER_ID
                                 INNER JOIN ASSET_STOCK_VENDOR ASV ON ASV.VENDOR_ID = VMT.GST_VENDOR_ID
                                 WHERE VMT.PROJECT_ID IN (?PROJECT_ID) AND VMT.VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED AND VMT.VOUCHER_TYPE IN (?VOUCHER_TYPE) AND VMT.STATUS = 1
                                 GROUP BY VMT.VOUCHER_ID ORDER BY VMT.VOUCHER_DATE, VOUCHER_ID, LENGTH(VMT.VOUCHER_NO), VMT.VOUCHER_NO ASC"; 
                        break;
                    }
                #endregion

                #region JournalVoucher
                case SQLCommand.Voucher.JournalVoucher:
                    {
                        Query = "SELECT VMT.VOUCHER_ID,VMT.VOUCHER_NO,VMT.VOUCHER_DATE,VMT.NAME_ADDRESS,\n" +
                            "VT.SEQUENCE_NO,\n" +
                            "ML.LEDGER_NAME,\n" +
                            "CASE WHEN VT.TRANS_MODE ='CR' THEN  VT.AMOUNT ELSE 0 END AS CREDIT,\n" +
                            "CASE WHEN VT.TRANS_MODE ='DR' THEN  VT.AMOUNT ELSE 0 END AS DEBIT,\n" +
                            "CASE WHEN VT.TRANS_MODE ='DR' THEN SUM(VT.AMOUNT)  WHEN VT.TRANS_MODE ='CR' THEN SUM(VT.AMOUNT)  ELSE 0 END AS AMOUNT," +
                            "CONCAT(VMT.NARRATION, IF(VMT.VOUCHER_SUB_TYPE='FD', CONCAT(' FD: ', FD.FD_ACCOUNT_NUMBER), '') ) AS NARRATION\n" +
                            "FROM VOUCHER_MASTER_TRANS VMT\n" +
                            "LEFT JOIN VOUCHER_TRANS VT ON VT.VOUCHER_ID=VMT.VOUCHER_ID\n" +
                            "LEFT JOIN MASTER_LEDGER ML ON ML.LEDGER_ID=VT.LEDGER_ID\n" +
                            "LEFT JOIN MASTER_PROJECT MP ON MP.PROJECT_ID=VMT.PROJECT_ID\n" +
                            "LEFT JOIN (SELECT FDA.FD_ACCOUNT_ID, 0 AS VOUCHER_ID, RNO.FD_INTEREST_VOUCHER_ID, RNO.FD_VOUCHER_ID,\n" +
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
                            "WHERE MP.PROJECT_ID  IN (?PROJECT_ID) AND VOUCHER_TYPE ='JN' AND TRANS_MODE='DR' " +
                             "AND VMT.VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED  AND VMT.STATUS=1 GROUP BY VT.VOUCHER_ID ORDER BY VMT.VOUCHER_DATE,VMT.VOUCHER_NO "; // ,VT.LEDGER_ID

                        break;
                    }
                #endregion

                #endregion
            }
            return Query;
        }

        #endregion
    }
}