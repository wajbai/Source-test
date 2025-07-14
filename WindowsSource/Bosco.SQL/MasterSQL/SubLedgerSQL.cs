using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.DAO;

namespace Bosco.SQL
{
    public class SubLedgerSQL : IDatabaseQuery
    {
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;

        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.SubLedger).FullName)
            {
                query = GetBudgetSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        private string GetBudgetSQL()
        {
            string query = "";
            SQLCommand.SubLedger sqlCommandId = (SQLCommand.SubLedger)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.SubLedger.BudgetSubLedgerAdd:
                    {
                        query = "INSERT INTO MASTER_SUB_LEDGER (SUB_LEDGER_NAME) VALUES (?SUB_LEDGER_NAME)";
                        break;
                    }
                case SQLCommand.SubLedger.BudgetSubLedgerEdit:
                    {
                        query = "UPDATE MASTER_SUB_LEDGER SET SUB_LEDGER_NAME =?SUB_LEDGER_NAME WHERE SUB_LEDGER_ID=?SUB_LEDGER_ID";
                        break;
                    }
                case SQLCommand.SubLedger.IsExistSubLedger:
                    {
                        query = "SELECT SUB_LEDGER_ID FROM MASTER_SUB_LEDGER WHERE SUB_LEDGER_NAME =?SUB_LEDGER_NAME";
                        break;
                    }
                case SQLCommand.SubLedger.MapLedgerwithSubledger:
                    {
                        query = "DELETE FROM LEDGER_SUB_LEDGER WHERE LEDGER_ID =?LEDGER_ID AND SUB_LEDGER_ID =?SUB_LEDGER_ID;" +
                                   "INSERT INTO LEDGER_SUB_LEDGER (LEDGER_ID, SUB_LEDGER_ID) VALUES (?LEDGER_ID, ?SUB_LEDGER_ID)";
                        break;
                    }
                case SQLCommand.SubLedger.FetchBudgetLedgerGroup:
                    {
                        query = " SELECT " +
                                     " ML.BANK_ACCOUNT_ID, " +
                                     " ML.GROUP_ID, " +
                                     " MP.NATURE_ID, " +
                                     " ML.LEDGER_ID,ML.LEDGER_CODE, " +
                                     " ML.IS_COST_CENTER, " +
                                     " CONCAT(ML.LEDGER_NAME,CONCAT(' - ',MP.LEDGER_GROUP),IF(ML.LEDGER_CODE='','', CONCAT(' (',ML.LEDGER_CODE,')'))) AS LEDGER_NAME," +
                                     "ML.IS_BANK_INTEREST_LEDGER, IS_TDS_LEDGER, IS_GST_LEDGERS, GST_SERVICE_TYPE, IFNULL(TCP.GST_ID,0) AS GST_ID" +
                                     " FROM " +
                                     " MASTER_LEDGER  ML " +
                                     "  INNER JOIN PROJECT_BUDGET_LEDGER PBL" +
                                     "     ON ML.LEDGER_ID = PBL.LEDGER_ID" +
                                     "     AND PBL.PROJECT_ID IN (?PROJECT_ID)" +
                                     " LEFT JOIN TDS_CREDTIORS_PROFILE TCP ON TCP.LEDGER_ID = ML.LEDGER_ID " + //On 02/12/2019
                                     " LEFT JOIN PROJECT_LEDGER PL ON PL.LEDGER_ID=ML.LEDGER_ID, " +
                                     " MASTER_LEDGER_GROUP MP  WHERE ML.GROUP_ID IN (SELECT GROUP_ID FROM MASTER_LEDGER_GROUP WHERE ML.GROUP_ID NOT IN(12,13,14)) " +
                                     " AND  ML.GROUP_ID=MP.GROUP_ID " +
                                     " AND ML.STATUS=0 " +
                                     " AND ML.LEDGER_TYPE='GN' " +
                                     " AND PL.PROJECT_ID=?PROJECT_ID " +
                                     " ORDER  BY LEDGER_NAME ASC ";
                        break;
                    }
            }
            return query;
        }
    }
}
