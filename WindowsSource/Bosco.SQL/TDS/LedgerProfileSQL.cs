using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class LedgerProfileSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.LedgerProfile).FullName)
            {
                query = GetLedgerProfile();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script

        private string GetLedgerProfile()
        {
            string query = "";
            SQLCommand.LedgerProfile sqlCommandId = (SQLCommand.LedgerProfile)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.LedgerProfile.Add:
                    {
                        query = "INSERT INTO TDS_CREDTIORS_PROFILE\n" +
                                "  (DEDUTEE_TYPE_ID,\n" +
                                "   NATURE_OF_PAYMENT_ID,\n" +
                                "   NAME,\n" +
                                "   ADDRESS,\n" +
                                "   STATE_ID,\n" +
                                "   PIN_CODE,\n" +
                                "   CONTACT_PERSON,\n" +
                                "   CONTACT_NUMBER,\n" +
                                "   EMAIL,\n" +
                                "   LEDGER_ID,\n" +
                                "   IS_BANK_DETAILS,\n" +
                                "   NICK_NAME,\n" +
                                "   FAVOURING_NAME,\n" +
                                "   TRANSACTION_TYPE,\n" +
                                "   BANK_NAME,\n" +
                                "   ACCOUNT_NUMBER,\n" +
                                "   IFS_CODE,\n" +
                                "   PAN_NUMBER,\n" +
                                "   PAN_IT_HOLDER_NAME,\n" +
                                "   SALES_TAX_NO,\n" +
                                "   COUNTRY_ID,\n" +
                                "   GST_Id,\n" +
                                "   GST_NO,\n" +
                                "   CST_NUMBER)\n" +
                                "VALUES\n" +
                                "  (?DEDUTEE_TYPE_ID,\n" +
                                "   ?NATURE_OF_PAYMENT_ID,\n" +
                                "   ?NAME,\n" +
                                "   ?ADDRESS,\n" +
                                "   ?STATE_ID,\n" +
                                "   ?PIN_CODE,\n" +
                                "   ?CONTACT_PERSON,\n" +
                                "   ?CONTACT_NUMBER,\n" +
                                "   ?EMAIL,\n" +
                                "   ?LEDGER_ID,\n" +
                                "   ?IS_BANK_DETAILS,\n" +
                                "   ?NICK_NAME,\n" +
                                "   ?FAVOURING_NAME,\n" +
                                "   ?TRANSACTION_TYPE,\n" +
                                "   ?BANK_NAME,\n" +
                                "   ?ACCOUNT_NUMBER,\n" +
                                "   ?IFS_CODE,\n" +
                                "   ?PAN_NUMBER,\n" +
                                "   ?PAN_IT_HOLDER_NAME,\n" +
                                "   ?SALES_TAX_NO,\n" +
                                "   ?COUNTRY_ID,\n" +
                                "   ?GST_Id,\n" +
                                "   ?GST_NO,\n" +
                                "   ?CST_NUMBER)";
                        break;
                    }

                case SQLCommand.LedgerProfile.Update:
                    {
                        query = "UPDATE TDS_CREDTIORS_PROFILE\n" +
                                "   SET DEDUTEE_TYPE_ID    = ?DEDUTEE_TYPE_ID,\n" +
                                "       NATURE_OF_PAYMENT_ID=?NATURE_OF_PAYMENT_ID,\n" +
                                "       NAME               = ?NAME,\n" +
                                "       ADDRESS            = ?ADDRESS,\n" +
                                "       STATE_ID           = ?STATE_ID,\n" +
                                "       PIN_CODE           = ?PIN_CODE,\n" +
                                "       CONTACT_PERSON     = ?CONTACT_PERSON,\n" +
                                "       CONTACT_NUMBER     = ?CONTACT_NUMBER,\n" +
                                "       EMAIL              = ?EMAIL,\n" +
                                "       LEDGER_ID          = ?LEDGER_ID,\n" +
                                "       IS_BANK_DETAILS    = ?IS_BANK_DETAILS,\n" +
                                "       NICK_NAME          = ?NICK_NAME,\n" +
                                "       FAVOURING_NAME     = ?FAVOURING_NAME,\n" +
                                "       TRANSACTION_TYPE   = ?TRANSACTION_TYPE,\n" +
                                "       BANK_NAME          = ?BANK_NAME,\n" +
                                "       ACCOUNT_NUMBER     = ?ACCOUNT_NUMBER,\n" +
                                "       IFS_CODE           = ?IFS_CODE,\n" +
                                "       PAN_NUMBER         = ?PAN_NUMBER,\n" +
                                "       PAN_IT_HOLDER_NAME = ?PAN_IT_HOLDER_NAME,\n" +
                                "       SALES_TAX_NO       = ?SALES_TAX_NO,\n" +
                                "       COUNTRY_ID         = ?COUNTRY_ID,\n" +
                                "       CST_NUMBER         = ?CST_NUMBER\n" +
                                " WHERE CREDITORS_PROFILE_ID = ?CREDITORS_PROFILE_ID";
                        break;
                    }
                case SQLCommand.LedgerProfile.Delete:
                    {
                        query = "DELETE FROM TDS_CREDTIORS_PROFILE WHERE LEDGER_ID=?LEDGER_ID ";
                        break;
                    }
                case SQLCommand.LedgerProfile.DeleteGStProfile:
                    {
                        query = "DELETE FROM TDS_CREDTIORS_PROFILE WHERE LEDGER_ID=?LEDGER_ID ";
                        break;
                    }
                case SQLCommand.LedgerProfile.FetchAll:
                    {
                        query = "SELECT DEDUTEE_TYPE_ID,\n" +
                                "       CREDITORS_PROFILE_ID,\n" +
                                "       NAME,\n" +
                                "       ADDRESS,\n" +
                                "       STATE_ID,\n" +
                                "       PIN_CODE,\n" +
                                "       CONTACT_PERSON,\n" +
                                "       CONTACT_NUMBER,\n" +
                                "       EMAIL,\n" +
                                "       LEDGER_ID,\n" +
                                "       IS_BANK_DETAILS,\n" +
                                "       NICK_NAME,\n" +
                                "       FAVOURING_NAME,\n" +
                                "       TRANSACTION_TYPE,\n" +
                                "       BANK_NAME,\n" +
                                "       ACCOUNT_NUMBER,\n" +
                                "       IFS_CODE,\n" +
                                "       PAN_NUMBER,\n" +
                                "       PAN_IT_HOLDER_NAME,\n" +
                                "       SALES_TAX_NO,\n" +
                                "       COUNTRY_ID,\n" +
                                "       CST_NUMBER\n" +
                                "  FROM TDS_CREDTIORS_PROFILE\"";
                        break;
                    }
                case SQLCommand.LedgerProfile.Fetch:
                    {
                        query = "SELECT DEDUTEE_TYPE_ID,\n" +
                                "       CREDITORS_PROFILE_ID,\n" +
                                "       NAME,\n" +
                                "       ADDRESS,\n" +
                                "       STATE_ID,\n" +
                                "       COUNTRY_ID,\n" +
                                "       GST_Id,\n" +
                                "       PIN_CODE,\n" +
                                "       CONTACT_PERSON,\n" +
                                "       CONTACT_NUMBER,\n" +
                                "       EMAIL,\n" +
                                "       LEDGER_ID,\n" +
                                "       IS_BANK_DETAILS,\n" +
                                "       NICK_NAME,\n" +
                                "       FAVOURING_NAME,\n" +
                                "       TRANSACTION_TYPE,\n" +
                                "       BANK_NAME,\n" +
                                "       ACCOUNT_NUMBER,\n" +
                                "       IFS_CODE,\n" +
                                "       PAN_NUMBER,\n" +
                                "       PAN_IT_HOLDER_NAME,\n" +
                                "       SALES_TAX_NO,\n" +
                                "       CST_NUMBER\n" +
                                "  FROM TDS_CREDTIORS_PROFILE\n" +
                                " WHERE LEDGER_ID = ?LEDGER_ID";
                        break;
                    }
                case SQLCommand.LedgerProfile.CheckPANNumber:
                    {
                        query = "SELECT CASE\n" +
                         "         WHEN PAN_NUMBER <> '' THEN\n" +
                         "          1\n" +
                         "         ELSE\n" +
                         "          0\n" +
                         "       END AS PANCOUNT\n" +
                         "  FROM TDS_CREDTIORS_PROFILE\n" +
                         " WHERE LEDGER_ID = ?LEDGER_ID";
                        break;
                    }
            }
            return query;
        }
        #endregion TDS SQL
    }
}
