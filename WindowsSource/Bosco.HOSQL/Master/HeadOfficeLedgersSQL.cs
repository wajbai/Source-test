using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.HOSQL
{
    public class HeadOfficeLedgersSQL : IDatabaseQuery
    {
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;
        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.ImportLedger).FullName)
            {
                query = GetLedgerQuery();
            }

            sqlType = this.sqlType;
            return query;
        }

        private string GetLedgerQuery()
        {
            string sQuery = string.Empty;
            SQLCommand.ImportLedger sqlCommandId = (SQLCommand.ImportLedger)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {
                case SQLCommand.ImportLedger.FetchHeadOfficeLedgers:
                    {
                        sQuery = "SELECT HEADOFFICE_LEDGER_ID,\n" +
                                "       LEDGER_CODE,\n" +
                                "       LEDGER_NAME AS HEADOFFICE_LEDGER_NAME,\n" +
                                "       LG.LEDGER_GROUP,\n" +
                                "       LEDGER_TYPE,\n" +
                                "       LEDGER_SUB_TYPE,\n" +
                                "       BANK_ACCOUNT_ID,\n" +
                                "       IS_COST_CENTER,\n" +
                                "       NOTES,\n" +
                                "       IS_BANK_INTEREST_LEDGER,\n" +
                                "       SORT_ID,\n" +
                                "       STATUS,\n" +
                                "       HL.ACCESS_FLAG\n" +
                                "  FROM master_headoffice_ledger HL\n" +
                                " INNER JOIN MASTER_LEDGER_GROUP LG\n" +
                                "    ON LG.GROUP_ID = HL.GROUP_ID";

                        break;
                    }
                case SQLCommand.ImportLedger.MapHeadOfficeLedger:
                    {
                        sQuery = "INSERT INTO HEADOFFICE_MAPPED_LEDGER( " +
                                 "LEDGER_ID, " +
                                 "HEADOFFICE_LEDGER_ID) " +
                                 "VALUES( " +
                                 "?LEDGER_ID, " +
                                 "?HEADOFFICE_LEDGER_ID) ";
                        break;
                    }
                case SQLCommand.ImportLedger.InsertMasterLedger:
                    {
                        sQuery = "INSERT INTO MASTER_LEDGER ( " +
                                "LEDGER_CODE, " +
                                "LEDGER_NAME, " +
                                "GROUP_ID,  " +
                                "LEDGER_TYPE, " +
                                "LEDGER_SUB_TYPE, " +
                            // "BANK_ACCOUNT_ID, " +
                                "IS_COST_CENTER,NOTES,SORT_ID,IS_BANK_INTEREST_LEDGER) " +
                                "VALUES( " +
                                "?LEDGER_CODE, " +
                                "?LEDGER_NAME, " +
                                "?GROUP_ID,  " +
                                "?LEDGER_TYPE, " +
                                "?LEDGER_SUB_TYPE, " +
                            //  "?BANK_ACCOUNT_ID, " +
                                 "?IS_COST_CENTER,?NOTES,?SORT_ID,?IS_BANK_INTEREST_LEDGER) ";
                        break;
                    }
                case SQLCommand.ImportLedger.InsertHeadOfficeLedger:
                    {
                        sQuery = "INSERT INTO MASTER_HEADOFFICE_LEDGER ( " +
                                "LEDGER_CODE, " +
                                "LEDGER_NAME, " +
                                "GROUP_ID,  " +
                                "LEDGER_TYPE, " +
                                "LEDGER_SUB_TYPE, " +
                            //  "BANK_ACCOUNT_ID, " +
                                "IS_COST_CENTER,NOTES,SORT_ID,IS_BANK_INTEREST_LEDGER) " +
                                "VALUES( " +
                                "?LEDGER_CODE, " +
                                "?LEDGER_NAME, " +
                                "?GROUP_ID,  " +
                                "?LEDGER_TYPE, " +
                                "?LEDGER_SUB_TYPE, " +
                            //   "?BANK_ACCOUNT_ID, " +
                                "?IS_COST_CENTER,?NOTES,?SORT_ID,?IS_BANK_INTEREST_LEDGER) ";
                        break;
                    }
                case SQLCommand.ImportLedger.InsertMasterLedgerGroup:
                    {

                        sQuery = "INSERT INTO MASTER_LEDGER_GROUP(" +
                                    "GROUP_CODE, " +
                                    "LEDGER_GROUP, " +
                                    "PARENT_GROUP_ID, " +
                                    "NATURE_ID, " +
                                    "MAIN_GROUP_ID) " +
                                    "VALUES(?GROUP_CODE, " +
                                    "?LEDGER_GROUP, " +
                                    "?PARENT_GROUP_ID, " +
                                    "?NATURE_ID, " +
                                    "?MAIN_GROUP_ID)";
                        break;
                    }
                case SQLCommand.ImportLedger.UpdateHeadOfficeLedger:
                    {
                        sQuery = "UPDATE MASTER_HEADOFFICE_LEDGER SET " +
                                 "  LEDGER_NAME=?LEDGER_NAME " +
                                 "WHERE LEDGER_NAME=?HEADOFFICE_LEDGER_NAME ";

                        break;
                    }
                case SQLCommand.ImportLedger.DeleteHeadOfficeMappedLedger:
                    {
                        sQuery = "DELETE FROM HEADOFFICE_MAPPED_LEDGER WHERE LEDGER_ID=?LEDGER_ID";
                        break;
                    }
                case SQLCommand.ImportLedger.DeleteHeadOfficeLedger:
                    {
                        sQuery = "DELETE FROM MASTER_HEADOFFICE_LEDGER WHERE HEADOFFICE_LEDGER_ID=?LEDGER_ID";
                        break;
                    }
                case SQLCommand.ImportLedger.DeleteMasterLedger:
                    {
                        sQuery = "DELETE FROM MASTER_LEDGER WHERE LEDGER_ID=?LEDGER_ID";
                        break;
                    }
                case SQLCommand.ImportLedger.DeleteProjectMappedLedger:
                    {
                        sQuery = "DELETE FROM PROJECT_LEDGER WHERE LEDGER_ID=?LEDGER_ID";
                        break;
                    }
                case SQLCommand.ImportLedger.DeleteLedgerBalance:
                    {
                        sQuery = "DELETE FROM LEDGER_BALANCE WHERE LEDGER_ID=?LEDGER_ID";
                        break;
                    }
                case SQLCommand.ImportLedger.DeleteBudgetLedger:
                    {
                        sQuery = "DELETE FROM BUDGET_LEDGER WHERE LEDGER_ID=?LEDGER_ID";
                        break;
                    }
                case SQLCommand.ImportLedger.FetchMappedLedgers:
                    {
                        sQuery = "SELECT * FROM HEADOFFICE_MAPPED_LEDGER WHERE HEADOFFICE_LEDGER_ID=?HEADOFFICE_LEDGER_ID";
                        break;
                    }
                case SQLCommand.ImportLedger.CheckTransactionCount:
                    {
                        sQuery = "SELECT COUNT(*) AS TRANS_COUNT\n" +
                                    "  FROM VOUCHER_TRANS VT\n" +
                                    " INNER JOIN HEADOFFICE_MAPPED_LEDGER HML\n" +
                                    "    ON HML.LEDGER_ID = VT.LEDGER_ID\n" +
                                    " WHERE HEADOFFICE_LEDGER_ID = ?HEADOFFICE_LEDGER_ID;";
                        break;
                    }
                case SQLCommand.ImportLedger.CheckLedgerBalance:
                    {
                        sQuery = "SELECT SUM(AMOUNT) FROM ledger_balance WHERE LEDGER_ID=?LEDGER_ID;";
                        break;
                    }
                case SQLCommand.ImportLedger.CheckBudgetLedgerAmount:
                    {
                        sQuery = "SELECT SUM(AMOUNT) FROM budget_ledger WHERE LEDGER_ID=?LEDGER_ID;";
                        break;
                    }
            }
            return sQuery;
        }
    }
}
