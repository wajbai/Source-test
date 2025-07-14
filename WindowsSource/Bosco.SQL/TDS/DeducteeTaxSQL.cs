using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class DeducteeTaxSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.DeducteeTax).FullName)
            {
                query = GetDeducteeTaxSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script

        private string GetDeducteeTaxSQL()
        {
            string query = "";
            SQLCommand.DeducteeTax sqlCommandId = (SQLCommand.DeducteeTax)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.DeducteeTax.DutyTaxAdd:
                    {
                        query = "INSERT INTO TDS_POLICY\n" +
                                "  (TDS_DEDUCTEE_TYPE_ID, TDS_NATURE_PAYMENT_ID, APPLICABLE_FROM)\n" +
                                "VALUES\n" +
                                "  (?TDS_DEDUCTEE_TYPE_ID, ?TDS_NATURE_PAYMENT_ID, ?APPLICABLE_FROM)";
                        break;
                    }
                case SQLCommand.DeducteeTax.DutyTaxRateAddByTaxTypeId:
                    {
                        query = "INSERT INTO TDS_TAX_RATE\n" +
                                "  (TDS_POLICY_ID, TDS_RATE, TDS_EXEMPTION_LIMIT, TDS_TAX_TYPE_ID)\n" +
                                "VALUES\n" +
                                "  (?TDS_POLICY_ID, ?TDS_RATE, ?TDS_EXEMPTION_LIMIT,?TDS_TAX_TYPE_ID )";
                        break;
                    }
                case SQLCommand.DeducteeTax.DutyTaxRateAdd: //Do not reuse this query except for DeducteeTax unless it has the same functionality
                    {
                        query = "INSERT INTO TDS_TAX_RATE\n" +
                                "  (TDS_POLICY_ID, TDS_RATE, TDS_EXEMPTION_LIMIT, TDS_TAX_TYPE_ID)\n" +
                                "VALUES {\n" +
                                "  (?TDS_POLICY_ID, ?TDS_RATE, ?TDS_EXEMPTION_LIMIT, 1) } {,\n" +
                                "  (?SUR_RATE_POLICY_ID, ?SUR_RATE, ?SUR_EXEMPTION, 3) } {,\n" +
                                "  (?ED_CESS_RATE_POLICY_ID, ?ED_CESS_RATE, ?ED_CESS_EXEMPTION,4) } {,\n" +
                                "  (?TDS_WITHOUT_PANT_POLICY_ID,?TDSRATE_WITHOUT_PAN,?TDSEXEMPTION_LIMIT_WITHOUT_PAN,2),} \n" +
                                "  (?SEC_ED_CESS_POLICY_ID, ?SEC_ED_CESS_RATE, ?SEC_ED_CESS_EXEMPTION, 5);";
                        break;
                    }
                case SQLCommand.DeducteeTax.DutyTaxUpdate:
                    {
                        query = "UPDATE TDS_POLICY\n" +
                                "   SET TDS_DEDUCTEE_TYPE_ID  = ?TDS_DEDUCTEE_TYPE_ID,\n" +
                                "       TDS_NATURE_PAYMENT_ID = ?TDS_NATURE_PAYMENT_ID,\n" +
                                "       APPLICABLE_FROM       = ?APPLICABLE_FROM\n" +
                                " WHERE TDS_POLICY_ID = ?TDS_POLICY_ID;";
                        break;
                    }
                case SQLCommand.DeducteeTax.DutyTaxDelete:
                    {
                        query = "DELETE FROM TDS_POLICY WHERE TDS_POLICY_ID=?TDS_POLICY_ID";
                        break;
                    }
                case SQLCommand.DeducteeTax.DutyTaxFetchAll:
                    {
                        query = "SELECT T.TDS_POLICY_ID,\n" +
                                "       T.NATURE_PAY_ID,\n" +
                                "       T.NAME AS NATURE_OF_PAYMENTS,\n" +
                                "       T.APPLICABLE_FROM,\n" +
                                "       T.TAX_TYPE_NAME,\n" +
                                "       SUM(T.TDS_RATE) AS TDS_RATE,\n" +
                                "       SUM(T.TDS_EXEMPTION_LIMIT) AS TDS_EXEMPTION_LIMIT,\n" +
                                "       SUM(T.TDS_EXEMPTION_LIMIT_WITHOUT_PAN) AS TDSEXEMPTION_LIMIT_WITHOUT_PAN,\n" +
                                "       SUM(T.TDS_RATE_WITHOUT_PAN) AS TDSRATE_WITHOUT_PAN,\n" +
                                "       SUM(T.SUR_RATE) AS SUR_RATE,\n" +
                                "       SUM(T.SUR_EXEMPTION) AS SUR_EXEMPTION,\n" +
                                "       SUM(T.ED_CESS_RATE) AS ED_CESS_RATE,\n" +
                                "       SUM(T.ED_CESS_EXEMPTION) AS ED_CESS_EXEMPTION,\n" +
                                "       SUM(T.SEC_ED_CESS_RATE) AS SEC_ED_CESS_RATE,\n" +
                                "       SUM(T.SEC_ED_CESS_EXEMPTION) AS SEC_ED_CESS_EXEMPTION\n" +
                                "\n" +
                                "  FROM (SELECT TP.TDS_POLICY_ID,\n" +
                                "               NP.NATURE_PAY_ID,\n" +
                                "               NP.NAME,\n" +
                                "               APPLICABLE_FROM,\n" +
                                "               TT.TAX_TYPE_NAME,\n" +
                                "               IF(TR.TDS_RATE IS NULL OR TR.TDS_RATE = '', 0, TR.TDS_RATE) AS TDS_RATE,\n" +
                                "               IF(TR.TDS_EXEMPTION_LIMIT IS NULL OR TR.TDS_RATE = '',\n" +
                                "                  0,\n" +
                                "                  TR.TDS_EXEMPTION_LIMIT) AS TDS_EXEMPTION_LIMIT,\n" +
                                "               0 AS TDS_RATE_WITHOUT_PAN,\n" +
                                "               0 AS TDS_EXEMPTION_LIMIT_WITHOUT_PAN,\n" +
                                "               0 AS SUR_RATE,\n" +
                                "               0 AS SUR_EXEMPTION,\n" +
                                "               0 AS ED_CESS_RATE,\n" +
                                "               0 AS ED_CESS_EXEMPTION,\n" +
                                "               0 AS SEC_ED_CESS_RATE,\n" +
                                "               0 AS SEC_ED_CESS_EXEMPTION\n" +
                                "          FROM TDS_NATURE_PAYMENT NP\n" +
                                "          LEFT JOIN TDS_POLICY TP\n" +
                                "            ON TP.TDS_NATURE_PAYMENT_ID = NP.NATURE_PAY_ID\n" +
                                "           AND TP.TDS_DEDUCTEE_TYPE_ID = ?TDS_DEDUCTEE_TYPE_ID\n" +
                                "          LEFT JOIN TDS_TAX_RATE TR\n" +
                                "            ON TP.TDS_POLICY_ID = TR.TDS_POLICY_ID\n" +
                                "           AND TR.TDS_TAX_TYPE_ID = 1\n" +
                                "          LEFT JOIN TDS_DUTY_TAXTYPE TT\n" +
                                "            ON TR.TDS_TAX_TYPE_ID = TT.TDS_DUTY_TAXTYPE_ID\n" +
                                "         WHERE NP.STATUS = 1\n" +
                                "         GROUP BY APPLICABLE_FROM, NAME\n" +
                                "\n" +
                                "        UNION\n" +
                                "        SELECT TP.TDS_POLICY_ID,\n" +
                                "               NP.NATURE_PAY_ID,\n" +
                                "               NP.NAME,\n" +
                                "               APPLICABLE_FROM,\n" +
                                "               TT.TAX_TYPE_NAME,\n" +
                                "               0 AS TDS_RATE,\n" +
                                "               0 AS TDS_EXEMPTION_LIMIT,\n" +
                                "               IF(TR.TDS_RATE IS NULL OR TR.TDS_RATE = '', 0, TR.TDS_RATE) AS TDS_RATE_WITHOUT_PAN,\n" +
                                "               IF(TR.TDS_EXEMPTION_LIMIT IS NULL OR TR.TDS_RATE = '',\n" +
                                "                  0,\n" +
                                "                  TR.TDS_EXEMPTION_LIMIT) AS TDS_EXEMPTION_LIMIT_WITHOUT_PAN,\n" +
                                "               0 AS SUR_RATE,\n" +
                                "               0 AS SUR_EXEMPTION,\n" +
                                "               0 AS ED_CESS_RATE,\n" +
                                "               0 AS ED_CESS_EXEMPTION,\n" +
                                "               0 AS SEC_ED_CESS_RATE,\n" +
                                "               0 AS SEC_ED_CESS_EXEMPTION\n" +
                                "          FROM TDS_NATURE_PAYMENT NP\n" +
                                "          LEFT JOIN TDS_POLICY TP\n" +
                                "            ON TP.TDS_NATURE_PAYMENT_ID = NP.NATURE_PAY_ID\n" +
                                "           AND TP.TDS_DEDUCTEE_TYPE_ID = ?TDS_DEDUCTEE_TYPE_ID\n" +
                                "          LEFT JOIN TDS_TAX_RATE TR\n" +
                                "            ON TP.TDS_POLICY_ID = TR.TDS_POLICY_ID\n" +
                                "           AND TR.TDS_TAX_TYPE_ID = 2\n" +
                                "          LEFT JOIN TDS_DUTY_TAXTYPE TT\n" +
                                "            ON TR.TDS_TAX_TYPE_ID = TT.TDS_DUTY_TAXTYPE_ID\n" +
                                "         WHERE NP.STATUS = 1\n" +
                                "         GROUP BY APPLICABLE_FROM, NAME\n" +
                                "        UNION\n" +
                                "        SELECT TP.TDS_POLICY_ID,\n" +
                                "               NP.NATURE_PAY_ID,\n" +
                                "               NP.NAME,\n" +
                                "               APPLICABLE_FROM,\n" +
                                "               TT.TAX_TYPE_NAME,\n" +
                                "               0 AS TDS_RATE,\n" +
                                "               0 AS TDS_EXEMPTION_LIMIT,\n" +
                                "               0 AS TDS_RATE_WITHOUT_PAN,\n" +
                                "               0 AS TDS_EXEMPTION_LIMIT_WITHOUT_PAN,\n" +
                                "               IF(TR.TDS_RATE IS NULL OR TR.TDS_RATE = '', 0, TR.TDS_RATE) AS SUR_RATE,\n" +
                                "               IF(TR.TDS_EXEMPTION_LIMIT IS NULL OR TR.TDS_RATE = '',\n" +
                                "                  0,\n" +
                                "                  TR.TDS_EXEMPTION_LIMIT) AS SUR_EXEMPTION,\n" +
                                "               0 AS ED_CESS_RATE,\n" +
                                "               0 AS ED_CESS_EXEMPTION,\n" +
                                "               0 AS SEC_ED_CESS_RATE,\n" +
                                "               0 AS SEC_ED_CESS_EXEMPTION\n" +
                                "          FROM TDS_NATURE_PAYMENT NP\n" +
                                "          LEFT JOIN TDS_POLICY TP\n" +
                                "            ON TP.TDS_NATURE_PAYMENT_ID = NP.NATURE_PAY_ID\n" +
                                "           AND TP.TDS_DEDUCTEE_TYPE_ID = ?TDS_DEDUCTEE_TYPE_ID\n" +
                                "          LEFT JOIN TDS_TAX_RATE TR\n" +
                                "            ON TP.TDS_POLICY_ID = TR.TDS_POLICY_ID\n" +
                                "           AND TR.TDS_TAX_TYPE_ID = 3\n" +
                                "          LEFT JOIN TDS_DUTY_TAXTYPE TT\n" +
                                "            ON TR.TDS_TAX_TYPE_ID = TT.TDS_DUTY_TAXTYPE_ID\n" +
                                "         WHERE NP.STATUS = 1\n" +
                                "         GROUP BY APPLICABLE_FROM, NAME\n" +
                                "        UNION\n" +
                                "        SELECT TP.TDS_POLICY_ID,\n" +
                                "               NP.NATURE_PAY_ID,\n" +
                                "               NP.NAME,\n" +
                                "               APPLICABLE_FROM,\n" +
                                "               TT.TAX_TYPE_NAME,\n" +
                                "               0 AS TDS_RATE,\n" +
                                "               0 AS TDS_EXEMPTION_LIMIT,\n" +
                                "               0 AS TDS_RATE_WITHOUT_PAN,\n" +
                                "               0 AS TDS_EXEMPTION_LIMIT_WITHOUT_PAN,\n" +
                                "               0 AS SUR_RATE,\n" +
                                "               0 AS SUR_EXEMPTION,\n" +
                                "               IF(TR.TDS_RATE IS NULL OR TR.TDS_RATE = '', 0, TR.TDS_RATE) ED_CESS_RATE,\n" +
                                "               IF(TR.TDS_EXEMPTION_LIMIT IS NULL OR TR.TDS_RATE = '',\n" +
                                "                  0,\n" +
                                "                  TR.TDS_EXEMPTION_LIMIT) AS ED_CESS_EXEMPTION,\n" +
                                "               0 AS SEC_ED_CESS_RATE,\n" +
                                "               0 AS SEC_ED_CESS_EXEMPTION\n" +
                                "          FROM TDS_NATURE_PAYMENT NP\n" +
                                "          LEFT JOIN TDS_POLICY TP\n" +
                                "            ON TP.TDS_NATURE_PAYMENT_ID = NP.NATURE_PAY_ID\n" +
                                "           AND TP.TDS_DEDUCTEE_TYPE_ID = ?TDS_DEDUCTEE_TYPE_ID\n" +
                                "          LEFT JOIN TDS_TAX_RATE TR\n" +
                                "            ON TP.TDS_POLICY_ID = TR.TDS_POLICY_ID\n" +
                                "           AND TR.TDS_TAX_TYPE_ID = 4\n" +
                                "          LEFT JOIN TDS_DUTY_TAXTYPE TT\n" +
                                "            ON TR.TDS_TAX_TYPE_ID = TT.TDS_DUTY_TAXTYPE_ID\n" +
                                "         WHERE NP.STATUS = 1\n" +
                                "         GROUP BY APPLICABLE_FROM, NAME\n" +
                                "\n" +
                                "        UNION\n" +
                                "        SELECT TP.TDS_POLICY_ID,\n" +
                                "               NP.NATURE_PAY_ID,\n" +
                                "               NP.NAME,\n" +
                                "               APPLICABLE_FROM,\n" +
                                "               TT.TAX_TYPE_NAME,\n" +
                                "               0 AS TDS_RATE,\n" +
                                "               0 AS TDS_EXEMPTION_LIMIT,\n" +
                                "               0 AS TDS_RATE_WITHOUT_PAN,\n" +
                                "               0 AS TDS_EXEMPTION_LIMIT_WITHOUT_PAN,\n" +
                                "               0 AS SUR_RATE,\n" +
                                "               0 AS SUR_EXEMPTION,\n" +
                                "               0 AS ED_CESS_RATE,\n" +
                                "               0 AS ED_CESS_EXEMPTION,\n" +
                                "               IF(TR.TDS_RATE IS NULL OR TR.TDS_RATE = '', 0, TR.TDS_RATE) AS SEC_ED_CESS_RATE,\n" +
                                "               IF(TR.TDS_EXEMPTION_LIMIT IS NULL OR TR.TDS_RATE = '',\n" +
                                "                  0,\n" +
                                "                  TR.TDS_EXEMPTION_LIMIT) AS SEC_ED_CESS_EXEMPTION\n" +
                                "          FROM TDS_NATURE_PAYMENT NP\n" +
                                "          LEFT JOIN TDS_POLICY TP\n" +
                                "            ON TP.TDS_NATURE_PAYMENT_ID = NP.NATURE_PAY_ID\n" +
                                "           AND TP.TDS_DEDUCTEE_TYPE_ID = ?TDS_DEDUCTEE_TYPE_ID\n" +
                                "          LEFT JOIN TDS_TAX_RATE TR\n" +
                                "            ON TP.TDS_POLICY_ID = TR.TDS_POLICY_ID\n" +
                                "           AND TR.TDS_TAX_TYPE_ID = 5\n" +
                                "          LEFT JOIN TDS_DUTY_TAXTYPE TT\n" +
                                "            ON TR.TDS_TAX_TYPE_ID = TT.TDS_DUTY_TAXTYPE_ID\n" +
                                "         WHERE NP.STATUS = 1\n" +
                                "         GROUP BY APPLICABLE_FROM, NAME\n" +
                                "         ORDER BY NAME) AS T\n" +
                                " GROUP BY T.APPLICABLE_FROM, T.NAME\n" +
                                " ORDER BY NAME";
                        break;
                    }
                case SQLCommand.DeducteeTax.DutyTaxFetchById:
                    {
                        query = "SELECT TDS_POLICY_ID,\n" +
                                "       TDS_DEDUCTEE_TYPE_ID,\n" +
                                "       TDS_NATURE_PAYMENT_ID,\n" +
                                "       APPLICABLE_FROM\n" +
                                "  FROM TDS_POLICY\n" +
                                " WHERE TDS_POLICY_ID = ?TDS_POLICY_ID;";
                        break;
                    }
                case SQLCommand.DeducteeTax.FetchTaxRateById:
                    {
                        query = "SELECT TAX_RATE_ID,\n" +
                                "       TDS_POLICY_ID,\n" +
                                "       TDS_RATE,\n" +
                                "       TDS_EXEMPTION_LIMIT,\n" +
                                "       TDS_DUTY_TAXTYPE_ID,\n" +
                                "       TAX_TYPE_NAME\n" +
                                "  FROM TDS_TAX_RATE TR\n" +
                                " INNER JOIN TDS_DUTY_TAXTYPE TT\n" +
                                "    ON TR.TDS_TAX_TYPE_ID = TT.TDS_DUTY_TAXTYPE_ID\n" +
                                " WHERE TDS_POLICY_ID = ?TDS_POLICY_ID;";
                        break;
                    }
                case SQLCommand.DeducteeTax.DutyTaxRateDelete:
                    {
                        query = "DELETE FROM tds_tax_rate\n" +
                                " WHERE TDS_POLICY_ID IN\n" +
                                "       (SELECT TDS_POLICY_ID\n" +
                                "          FROM tds_policy\n" +
                                "         WHERE TDS_DEDUCTEE_TYPE_ID = ?TDS_DEDUCTEE_TYPE_ID);\n" +
                                "DELETE FROM TDS_POLICY WHERE TDS_DEDUCTEE_TYPE_ID = ?TDS_DEDUCTEE_TYPE_ID;";
                        break;
                    }

                case SQLCommand.DeducteeTax.DutyTaxTypeAdd:
                    {
                        query = "INSERT INTO TDS_DUTY_TAXTYPE(TAX_TYPE_NAME,STATUS)VALUES(?TAX_TYPE_NAME,?STATUS)";
                        break;
                    }
                case SQLCommand.DeducteeTax.DutyTaxTypeUpdate:
                    {
                        query = "UPDATE TDS_DUTY_TAXTYPE SET TAX_TYPE_NAME=?TAX_TYPE_NAME ,STATUS=?STATUS WHERE TDS_DUTY_TAXTYPE_ID=?TDS_DUTY_TAXTYPE_ID";
                        break;
                    }
                case SQLCommand.DeducteeTax.DutyTaxTypeDelete:
                    {
                        query = "DELETE FROM TDS_DUTY_TAXTYPE WHERE TDS_DUTY_TAXTYPE_ID=?TDS_DUTY_TAXTYPE_ID";
                        break;
                    }
                case SQLCommand.DeducteeTax.DutyTaxTypeFetchAll:
                    {
                        query = "SELECT TDS_DUTY_TAXTYPE_ID,\n" +
                        "       TAX_TYPE_NAME,\n" +
                        "       0 AS TDS_RATE,\n" +
                        "       0 AS TDS_EXEMPTION_LIMIT,\n" +
                        "       CASE\n" +
                        "         WHEN STATUS = 1 THEN\n" +
                        "          'Active'\n" +
                        "         ELSE\n" +
                        "          'Inactive'\n" +
                        "       END AS STATUS\n" +
                        "  FROM TDS_DUTY_TAXTYPE";
                        break;
                    }
                case SQLCommand.DeducteeTax.FetchActiveDutyTaxType:
                    {
                        query = "SELECT TDS_DUTY_TAXTYPE_ID,\n" +
                        "       TAX_TYPE_NAME,\n" +
                        "       0 AS TDS_RATE,\n" +
                        "       0 AS TDS_EXEMPTION_LIMIT,\n" +
                        "       CASE\n" +
                        "         WHEN STATUS = 1 THEN\n" +
                        "          'Active'\n" +
                        "         ELSE\n" +
                        "          'Inactive'\n" +
                        "       END AS STATUS\n" +
                        "  FROM TDS_DUTY_TAXTYPE\n" +
                        " WHERE STATUS=1";
                        break;
                    }
                case SQLCommand.DeducteeTax.DutyTaxTypeFetchById:
                    {
                        query = "SELECT TDS_DUTY_TAXTYPE_ID,\n" +
                                "       TAX_TYPE_NAME,\n" +
                                "       0 AS TDS_RATE,\n" +
                                "       0 AS TDS_EXEMPTION_LIMIT,\n" +
                                "       STATUS\n" +
                                "  FROM TDS_DUTY_TAXTYPE\n" +
                                " WHERE TDS_DUTY_TAXTYPE_ID =?TDS_DUTY_TAXTYPE_ID";

                        break;
                    }
                case SQLCommand.DeducteeTax.FetchTaxPolicy:
                    {
                        query = "SELECT TDS_NATURE_PAYMENT_ID, TTR.TDS_RATE,\n" +
                            // "       MAX(TTR.TDS_EXEMPTION_LIMIT) AS TDS_EXEMPTION_LIMIT\n" +
                             "       TTR.TDS_EXEMPTION_LIMIT AS TDS_EXEMPTION_LIMIT\n" +
                        "  FROM TDS_POLICY AS TP\n" +
                        "  LEFT JOIN TDS_TAX_RATE AS TTR\n" +
                        "    ON TP.TDS_POLICY_ID = TTR.TDS_POLICY_ID\n" +
                        "  LEFT JOIN TDS_CREDTIORS_PROFILE AS TCP\n" +
                        "    ON TP.TDS_DEDUCTEE_TYPE_ID = TCP.DEDUTEE_TYPE_ID\n" +
                        "  LEFT JOIN TDS_DUTY_TAXTYPE AS TDT\n" +
                        "    ON TTR.TDS_TAX_TYPE_ID = TDT.TDS_DUTY_TAXTYPE_ID\n" +
                        " WHERE TP.APPLICABLE_FROM <= ?APPLICABLE_FROM\n" +
                        "   AND TP.TDS_DEDUCTEE_TYPE_ID = ?TDS_DEDUCTEE_TYPE_ID\n" +
                        "   AND TP.TDS_NATURE_PAYMENT_ID = ?TDS_NATURE_PAYMENT_ID\n" +
                        "   AND TDS_TAX_TYPE_ID NOT IN (2)\n" +
                        "   AND TCP.LEDGER_ID =  ?PARTY_LEDGER_ID\n" +
                        "   AND TDT.STATUS = 1\n" +
                        "   AND TDT.TDS_DUTY_TAXTYPE_ID =1\n"+
                      // " GROUP BY TP.TDS_NATURE_PAYMENT_ID\n" +
                        " ORDER BY APPLICABLE_FROM DESC LIMIT 1 ";
                        break;
                    }
            }

            return query;
        }
        #endregion Bank SQL
    }
}
