using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class TDSCompanyDeductors : IDatabaseQuery
    {
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;

        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.TDSCompanyDeductor).FullName)
            {
                query = GetDeducteeTypeSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        /// <summary>
        /// Purpose:To Perform the action of the Deductee details.
        /// </summary>
        /// <returns></returns>
        private string GetDeducteeTypeSQL()
        {
            string query = "";
            SQLCommand.TDSCompanyDeductor sqlCommandId = (SQLCommand.TDSCompanyDeductor)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.TDSCompanyDeductor.Add:
                    {
                        query = "INSERT INTO TDS_COMPANY_DEDUCTORS\n" +
                                    "  (TAX_DEDUCTION_ACCOUNT_NO,\n" +
                                    "   HEAD_OFFICE_TAN_NO,\n" +
                                    "   PAN_NO,\n" +
                                    "   TAN_REGISTRATION_NO,\n" +
                                    "   INCOME_TAX_CIRCLE,\n" +
                                    "   DEDUCTOR_TYPE,\n" +
                                    "   RESPONSIBLE_PERSON,\n" +
                                    "   SON_DAUGHTER_OF,\n" +
                                    "   DESIGNATION,\n" +
                                    "   ADDRESS,\n" +
                                    "   FLAT_NO,\n" +
                                    "   PREMISES,\n" +
                                    "   STREET,\n" +
                                    "   LOCATION,\n" +
                                    "   DISTRICT,\n" +
                                    "   STATE,\n" +
                                    "   PINCODE,\n" +
                                    "   TELEPHONE_NO,\n" +
                                    "   EMAIL,FULL_NAME)\n" +
                                    "VALUES\n" +
                                    "  (?TAX_DEDUCTION_ACCOUNT_NO,\n" +
                                    "   ?HEAD_OFFICE_TAN_NO,\n" +
                                    "   ?PAN_NO,\n" +
                                    "   ?TAN_REGISTRATION_NO,\n" +
                                    "   ?INCOME_TAX_CIRCLE,\n" +
                                    "   ?DEDUCTOR_TYPE,\n" +
                                    "   ?RESPONSIBLE_PERSON,\n" +
                                    "   ?SON_DAUGHTER_OF,\n" +
                                    "   ?DESIGNATION,\n" +
                                    "   ?ADDRESS,\n" +
                                    "   ?FLAT_NO,\n" +
                                    "   ?PREMISES,\n" +
                                    "   ?STREET,\n" +
                                    "   ?LOCATION,\n" +
                                    "   ?DISTRICT,\n" +
                                    "   ?STATE,\n" +
                                    "   ?PINCODE,\n" +
                                    "   ?TELEPHONE_NO,\n" +
                                    "   ?EMAIL,?FULL_NAME)";
                        break;
                    }
                case SQLCommand.TDSCompanyDeductor.Update:
                    {
                        query = "UPDATE TDS_COMPANY_DEDUCTORS\n" +
                                    "   SET TAX_DEDUCTION_ACCOUNT_NO = ?TAX_DEDUCTION_ACCOUNT_NO,\n" +
                                    "       HEAD_OFFICE_TAN_NO       = ?HEAD_OFFICE_TAN_NO,\n" +
                                    "       PAN_NO                   = ?PAN_NO,\n" +
                                    "       TAN_REGISTRATION_NO      = ?TAN_REGISTRATION_NO,\n" +
                                    "       INCOME_TAX_CIRCLE        = ?INCOME_TAX_CIRCLE,\n" +
                                    "       DEDUCTOR_TYPE            = ?DEDUCTOR_TYPE,\n" +
                                    "       RESPONSIBLE_PERSON       = ?RESPONSIBLE_PERSON,\n" +
                                    "       SON_DAUGHTER_OF          = ?SON_DAUGHTER_OF,\n" +
                                    "       DESIGNATION              = ?DESIGNATION,\n" +
                                    "       ADDRESS                  = ?ADDRESS,\n" +
                                    "       FLAT_NO                  = ?FLAT_NO,\n" +
                                    "       PREMISES                 = ?PREMISES,\n" +
                                    "       STREET                   = ?STREET,\n" +
                                    "       LOCATION                 = ?LOCATION,\n" +
                                    "       DISTRICT                 = ?DISTRICT,\n" +
                                    "       STATE                    = ?STATE,\n" +
                                    "       PINCODE                  = ?PINCODE,\n" +
                                    "       TELEPHONE_NO             = ?TELEPHONE_NO,\n" +
                                    "       EMAIL                    = ?EMAIL,\n" +
                                    "       FULL_NAME                = ?FULL_NAME\n" +
                                    " WHERE ID = ?ID";
                        break;
                    }
                case SQLCommand.TDSCompanyDeductor.Fetch:
                    {
                        query = "SELECT ID, TAX_DEDUCTION_ACCOUNT_NO, HEAD_OFFICE_TAN_NO, PAN_NO, TAN_REGISTRATION_NO, INCOME_TAX_CIRCLE, DEDUCTOR_TYPE, RESPONSIBLE_PERSON, SON_DAUGHTER_OF, DESIGNATION, ADDRESS, FLAT_NO, PREMISES, STREET, LOCATION, DISTRICT, STATE, PINCODE, TELEPHONE_NO, EMAIL,FULL_NAME FROM TDS_COMPANY_DEDUCTORS";
                        break;
                    }
            }
            return query;
        }
    }
}
