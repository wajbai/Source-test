using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
namespace Bosco.SQL
{
    public class ProspectSQL : IDatabaseQuery
    {
        #region ISQLServerQueryMembers
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;

        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.DonorProspect).FullName)
            {
                query = GetProspectSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        public string GetProspectSQL()
        {
            string query = "";
            SQLCommand.DonorProspect sqlCommandId = (SQLCommand.DonorProspect)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {
                case SQLCommand.DonorProspect.Add:
                    {
                        //query = "INSERT INTO MASTER_DONAUD_PROSPECTS ( " +
                        //        "NAME,TYPE,INSTITUTIONAL_TYPE_ID," +
                        //        "COUNTRY_ID,LANGUAGE,RELIGION,PLACE," +
                        //        "STATE_ID,ADDRESS,PINCODE,PHONE,FAX," +
                        //        "EMAIL,URL,SOURCE_INFORMATION,REGISTRATION_TYPE_ID," +
                        //        "REFERENCE_NUMBER,NOTES,PAN,TITLE,GENDER,DOB," +
                        //        "REFERRED_STAFF,OCCUPATION,PAYMENT_MODE_ID," +
                        //        "ORG_EMPLOYED,MARITAL_STATUS,ANNIVERSARY_DATE) VALUES( " +
                        //        "?NAME,?TYPE,?INSTITUTIONAL_TYPE_ID," +
                        //        "?COUNTRY_ID,?LANGUAGE,?RELIGION,?PLACE," +
                        //        "?STATE_ID,?ADDRESS,?PINCODE,?PHONE,?FAX," +
                        //        "?EMAIL,?URL,?SOURCE_INFORMATION,?REGISTRATION_TYPE_ID," +
                        //        "?REFERENCE_NUMBER,?NOTES,?PAN,?TITLE,?GENDER,?DOB," +
                        //        "?REFERRED_STAFF,?OCCUPATION," +
                        //       "?PAYMENT_MODE_ID,?ORG_EMPLOYED,?MARITAL_STATUS,?ANNIVERSARY_DATE)";

                        query = "INSERT INTO MASTER_DONAUD_PROSPECTS (NAME,REGISTER_NO,LASTNAME,TYPE,TITLE,GENDER,MARITAL_STATUS,DOB,ANNIVERSARY_DATE,RELIGION,OCCUPATION," +
                        "REFERRED_STAFF,LANGUAGE,INSTITUTIONAL_TYPE_ID,COUNTRY_ID,PLACE, STATE_ID, ADDRESS, PINCODE, PHONE,TELEPHONE," +
                        "FAX, EMAIL, URL, SOURCE_INFORMATION, REGISTRATION_TYPE_ID, REFERENCE_NUMBER, NOTES, PAN,ORG_EMPLOYED)\n" +
                        "VALUES\n" +
                        "  (?NAME,?REGISTER_NO,?LASTNAME,\n" +
                        "   ?TYPE,\n" +
                        "   ?TITLE,\n" +
                        "   ?GENDER,\n" +
                        "   ?MARITAL_STATUS,\n" +
                        "   ?DOB,\n" +
                        "   ?ANNIVERSARY_DATE,\n" +
                        "   ?RELIGION,\n" +
                        "   ?OCCUPATION,\n" +
                        "   ?REFEREDSTAFF,\n" +
                        "   ?LANGUAGE,\n" +
                        "   ?INSTITUTIONAL_TYPE_ID,\n" +
                        "   ?COUNTRY_ID,\n" +
                        "   ?PLACE,\n" +
                        "   ?STATE_ID,\n" +
                        "   ?ADDRESS,\n" +
                        "   ?PINCODE,\n" +
                        "   ?PHONE,\n" +
                        "   ?TELEPHONE,\n" +
                        "   ?FAX,\n" +
                        "   ?EMAIL,\n" +
                        "   ?URL,\n" +
                        "   ?SOURCE_INFORMATION,\n" +
                        "   ?REGISTRATION_TYPE_ID,\n" +
                        "   ?REFERENCE_NUMBER,\n" +
                        "   ?NOTES,\n" +
                        "   ?PAN,\n" +
                        "   ?ORG_EMPLOYED);";


                        break;
                    }
                case SQLCommand.DonorProspect.Update:
                    {
                        //query = "UPDATE MASTER_DONAUD_PROSPECTS SET " +
                        //         "NAME=?NAME,TYPE=?TYPE,INSTITUTIONAL_TYPE_ID=?INSTITUTIONAL_TYPE_ID," +
                        //         "COUNTRY_ID=?COUNTRY_ID,LANGUAGE=?LANGUAGE,RELIGION=?RELIGION,PLACE=?PLACE," +
                        //         "STATE_ID=?STATE_ID,ADDRESS=?ADDRESS,PINCODE=?PINCODE,PHONE=?PHONE,FAX=?FAX," +
                        //         "EMAIL=?EMAIL,URL=?URL,SOURCE_INFORMATION=?SOURCE_INFORMATION,REGISTRATION_TYPE_ID=?REGISTRATION_TYPE_ID," +
                        //         "REFERENCE_NUMBER=?REFERENCE_NUMBER,NOTES=?NOTES,PAN=?PAN,TITLE=?TITLE,GENDER=?GENDER,DOB=?DOB," +
                        //         "REFERRED_STAFF=?REFERRED_STAFF,OCCUPATION=?OCCUPATION," +
                        //         "PAYMENT_MODE_ID=?PAYMENT_MODE_ID,ORG_EMPLOYED=?ORG_EMPLOYED,MARITAL_STATUS=?MARITAL_STATUS,ANNIVERSARY_DATE=?ANNIVERSARY_DATE " +
                        //         "WHERE PROSPECT_ID=?PROSPECT_ID ";

                        query = "UPDATE MASTER_DONAUD_PROSPECTS\n" +
                        "   SET NAME                  = ?NAME,LASTNAME=?LASTNAME,\n" +
                        "       REGISTER_NO           = ?REGISTER_NO,\n" +
                        "       TYPE                  = ?TYPE,\n" +
                        "       TITLE                 = ?TITLE,\n" +
                        "       GENDER                = ?GENDER,\n" +
                        "       MARITAL_STATUS        = ?MARITAL_STATUS,\n" +
                        "       TYPE                  = ?TYPE,\n" +
                        "       TYPE                  = ?TYPE,\n" +
                        "       DOB                   = ?DOB,\n" +
                        "       ANNIVERSARY_DATE      = ?ANNIVERSARY_DATE,\n" +
                        "       RELIGION              = ?RELIGION,\n" +
                        "       LANGUAGE              = ?LANGUAGE,\n" +
                        "       OCCUPATION            = ?OCCUPATION,\n" +
                        "       REFERRED_STAFF        = ?REFEREDSTAFF,\n" +
                        "       INSTITUTIONAL_TYPE_ID = ?INSTITUTIONAL_TYPE_ID,\n" +
                        "       COUNTRY_ID            = ?COUNTRY_ID,\n" +
                        "       PLACE                 = ?PLACE,\n" +
                        "       STATE_ID              = ?STATE_ID,\n" +
                        "       ADDRESS               = ?ADDRESS,\n" +
                        "       PINCODE               = ?PINCODE,\n" +
                        "       PHONE                 = ?PHONE,\n" +
                        "       TELEPHONE             = ?TELEPHONE,\n" +
                        "       FAX                   = ?FAX,\n" +
                        "       EMAIL                 = ?EMAIL,\n" +
                        "       URL                   = ?URL,\n" +
                        "       SOURCE_INFORMATION    = ?SOURCE_INFORMATION,\n" +
                        "       REGISTRATION_TYPE_ID  = ?REGISTRATION_TYPE_ID,\n" +  // ORG_EMPLOYED
                        "       REFERENCE_NUMBER      = ?REFERENCE_NUMBER,\n" +
                        "       NOTES                 = ?NOTES,\n" +
                        "       PAN                   = ?PAN,\n" +
                        "       ORG_EMPLOYED          = ?ORG_EMPLOYED\n" +
                        " WHERE PROSPECT_ID = ?PROSPECT_ID;";
                        break;
                    }

                case SQLCommand.DonorProspect.Delete:
                    {
                        query = "DELETE FROM MASTER_DONAUD_PROSPECTS WHERE PROSPECT_ID=?PROSPECT_ID";
                        break;
                    }
                case SQLCommand.DonorProspect.GetProspectId:
                    {
                        query = "SELECT PROSPECT_ID FROM MASTER_DONAUD_PROSPECTS WHERE NAME=?NAME AND PLACE=?PLACE AND ADDRESS=?ADDRESS";
                        break;
                    }
                case SQLCommand.DonorProspect.Fetch:
                    {
                        query = "SELECT PROSPECT_ID,\n" +
                                "       NAME,REGISTER_NO,LASTNAME,\n" +
                                "       TYPE,\n" +
                                "       DOB,\n" +
                                "       ANNIVERSARY_DATE,\n" +
                               "       INSTITUTIONAL_TYPE_ID,\n" +
                                "       P.COUNTRY_ID,\n" +
                                "       COUNTRY,\n" +
                                "       LANGUAGE,\n" +
                                "       RELIGION,\n" +
                                "       PLACE,\n" +
                                "       P.STATE_ID,\n" +
                                "       STATE_NAME,\n" +
                                "       ADDRESS,\n" +
                                "       PINCODE,\n" +
                                "       PHONE,\n" +
                                "       FAX,\n" +
                                "       EMAIL,\n" +
                                "       URL,\n" +
                                "       SOURCE_INFORMATION,\n" +
                                "       REFERRED_STAFF,\n" +
                                "       REGISTRATION_TYPE_ID,\n" +
                                "       REFERENCE_NUMBER,\n" +
                                "       NOTES,\n" +
                                "       PAN,\n" +
                                "       TITLE,\n" +
                                "       GENDER,\n" +
                                "       DOB,\n" +
                                "       REFERRED_STAFF,\n" +
                                "       OCCUPATION,\n" +
                            // "       PAYMENT_MODE_ID,\n" +
                                "       ORG_EMPLOYED,\n" +
                                "       MARITAL_STATUS\n" +
                            //  "       ANNIVERSARY_DATE\n" +
                                "  FROM MASTER_DONAUD_PROSPECTS P\n" +
                                "  LEFT JOIN MASTER_COUNTRY C\n" +
                                "    ON P.COUNTRY_ID = C.COUNTRY_ID\n" +
                                "  LEFT JOIN MASTER_STATE MS\n" +
                                "    ON P.STATE_ID = MS.STATE_ID  WHERE PROSPECT_ID=?PROSPECT_ID\n" +
                                " ORDER BY NAME ASC";
                        break;
                    }

                case SQLCommand.DonorProspect.FetchAll:
                    {
                        query = "SELECT PROSPECT_ID,\n" +
                                " TRIM(CONCAT(NAME,' ',IF(LASTNAME IS NULL,' ',LASTNAME))) AS NAME,\n" +
                                "       REGISTER_NO,\n" +
                                "       TYPE,\n" +
                                "       DOB,\n" +
                                "       ANNIVERSARY_DATE,\n" +
                                "       REGISTRATION_TYPE,\n" +
                                "       INSTITUTIONAL_TYPE_ID,\n" +
                                "       P.COUNTRY_ID,\n" +
                                "       COUNTRY,\n" +
                                "       LANGUAGE,\n" +
                                "       RELIGION,\n" +
                                "       PLACE,\n" +
                                "       STATE_NAME,\n" +
                                "       ADDRESS,\n" +
                                "       PINCODE,\n" +
                                "       PHONE,\n" +
                                "       TELEPHONE,\n" +
                                "       FAX,\n" +
                                "       EMAIL,\n" +
                                "       URL,\n" +
                                "       SOURCE_INFORMATION,\n" +
                                "       P.REGISTRATION_TYPE_ID,\n" +
                                "       REFERENCE_NUMBER,\n" +
                                "       NOTES,\n" +
                                "       PAN,\n" +
                                "       TITLE,\n" +
                                "       GENDER,\n" +
                                "       DOB,\n" +
                                "       REFERRED_STAFF,\n" +
                                "       OCCUPATION,\n" +
                                "       PAYMENT_MODE_ID,\n" +
                                "       ORG_EMPLOYED,\n" +
                                "       MARITAL_STATUS,\n" +
                                "       ANNIVERSARY_DATE\n" +
                                "  FROM MASTER_DONAUD_PROSPECTS P\n" +
                                "  LEFT JOIN MASTER_COUNTRY C\n" +
                                "    ON P.COUNTRY_ID = C.COUNTRY_ID\n" +
                                "  LEFT JOIN MASTER_STATE MS\n" +
                                "    ON P.STATE_ID = MS.STATE_ID\n" +
                                "  LEFT JOIN MASTER_DONAUD_REG_TYPE MDRT\n" +
                                "    ON MDRT.REGISTRATION_TYPE_ID = P.REGISTRATION_TYPE_ID\n" +
                                " ORDER BY NAME ASC";
                        break;
                    }
                case SQLCommand.DonorProspect.GetDonorRegStatus:
                    {
                        query = "SELECT PROSPECT_ID,\n" +
                                " TRIM(CONCAT(NAME,' ',IF(LASTNAME IS NULL,' ',LASTNAME))) AS NAME,\n" +
                                "       REGISTER_NO," +
                                "       TYPE,\n" +
                                "       REGISTRATION_TYPE,\n" +
                                "       INSTITUTIONAL_TYPE_ID,\n" +
                                "       P.COUNTRY_ID,\n" +
                                "       COUNTRY,\n" +
                                "       LANGUAGE,\n" +
                                "       RELIGION,\n" +
                                "       PLACE,\n" +
                                "       STATE_NAME,\n" +
                                "       ADDRESS,\n" +
                                "       PINCODE,\n" +
                                "       PHONE,\n" +
                                "       FAX,\n" +
                                "       EMAIL,\n" +
                                "       URL,\n" +
                                "       SOURCE_INFORMATION,\n" +
                                "       P.REGISTRATION_TYPE_ID,\n" +
                                "       REFERENCE_NUMBER,\n" +
                                "       NOTES,\n" +
                                "       PAN,\n" +
                                "       TITLE,\n" +
                                "       GENDER,\n" +
                                "       DOB,\n" +
                                "       REFERRED_STAFF,\n" +
                                "       OCCUPATION,\n" +
                                "       PAYMENT_MODE_ID,\n" +
                                "       ORG_EMPLOYED,\n" +
                                "       MARITAL_STATUS,\n" +
                                "       ANNIVERSARY_DATE\n" +
                                "  FROM MASTER_DONAUD_PROSPECTS P\n" +
                                "  LEFT JOIN MASTER_COUNTRY C\n" +
                                "    ON P.COUNTRY_ID = C.COUNTRY_ID\n" +
                                "  LEFT JOIN MASTER_STATE MS\n" +
                                "    ON P.STATE_ID = MS.STATE_ID\n" +
                                "  LEFT JOIN MASTER_DONAUD_REG_TYPE MDRT\n" +
                                "    ON MDRT.REGISTRATION_TYPE_ID = P.REGISTRATION_TYPE_ID\n" +
                                 " { WHERE  P.REGISTRATION_TYPE_ID = ?REGISTRATION_TYPE_ID } \n" +
                                " ORDER BY NAME ASC";
                        break;
                    }
                case SQLCommand.DonorProspect.FetchInstitutionalType:
                    {
                        query = "SELECT INSTITUTIONAL_TYPE_ID, INSTITUTIONAL_TYPE FROM MASTER_DONAUD_INS_TYPE";
                        break;
                    }

                case SQLCommand.DonorProspect.FetchRegistrationType:
                    {
                        query = "SELECT REGISTRATION_TYPE_ID, REGISTRATION_TYPE FROM MASTER_DONAUD_REG_TYPE";
                        break;
                    }

                case SQLCommand.DonorProspect.FetchDonorPaymentMode:
                    {
                        query = "SELECT PAYMENT_MODE_ID, PAYMENT_MODE FROM MASTER_DONAUD_PAY_MODE";
                        break;
                    }
                case SQLCommand.DonorProspect.FetchSearchByName:
                    {
                        query = "SELECT PROSPECT_ID,\n" +
                                "CONCAT(CONCAT(NAME,' - '), LASTNAME) AS NAME " +
                                "TYPE,\n" +
                                "INSTITUTIONAL_TYPE_ID,\n" +
                                "COUNTRY_ID,\n" +
                                "LANGUAGE,\n" +
                                "RELIGION,\n" +
                                "PLACE,\n" +
                                "STATE_ID,\n" +
                                "ADDRESS,\n" +
                                "PINCODE,\n" +
                                "PHONE,\n" +
                                "FAX,\n" +
                                "EMAIL,\n" +
                                "URL,\n" +
                                "SOURCE_INFORMATION,\n" +
                                "REGISTRATION_TYPE_ID,\n" +
                                "REFERENCE_NUMBER,\n" +
                                "NOTES,\n" +
                                "PAN,\n" +
                                "TITLE,\n" +
                                "GENDER,\n" +
                                "DOB,\n" +
                                "REFERRED_STAFF,\n" +
                                "OCCUPATION,\n" +
                                "PAYMENT_MODE_ID,\n" +
                                "ORG_EMPLOYED,\n" +
                                "MARITAL_STATUS,\n" +
                                "ANNIVERSARY_DATE FROM MASTER_DONAUD_PROSPECTS WHERE NAME LIKE '%?NAME%'";
                        break;
                    }
                case SQLCommand.DonorProspect.GetRegistrationId:
                    {
                        query = "SELECT REGISTRATION_TYPE_ID FROM MASTER_DONAUD_REG_TYPE WHERE REGISTRATION_TYPE=?REGISTRATION_TYPE";
                        break;
                    }
                case SQLCommand.DonorProspect.GetInstitutionId:
                    {
                        query = "SELECT INSTITUTIONAL_TYPE_ID FROM MASTER_DONAUD_INS_TYPE WHERE INSTITUTIONAL_TYPE=?INSTITUTIONAL_TYPE";
                        break;
                    }
                case SQLCommand.DonorProspect.GetLanguage:
                    {
                        query = "SELECT MD.LANGUAGE FROM MASTER_DONAUD MD WHERE LANGUAGE <> 'NULL'\n" +
                                "UNION\n" +
                                "SELECT MDP.LANGUAGE FROM MASTER_DONAUD_PROSPECTS MDP WHERE LANGUAGE <> 'NULL';";
                        break;
                    }
                case SQLCommand.DonorProspect.GetStateDonaud:
                    {
                        //query = "SELECT MS.STATE_NAME, NAME, MS.STATE_ID, MD.DONAUD_ID\n" +
                        //        "  FROM MASTER_DONAUD MD\n" +
                        //        " RIGHT JOIN MASTER_STATE MS\n" +
                        //        "    ON MS.STATE_ID = MD.STATE_ID\n" +
                        //        "  GROUP BY MS.STATE_NAME ";

                        query = "SELECT MS.STATE_NAME, NAME, MS.STATE_ID, MD.DONAUD_ID\n" +
                                "  FROM MASTER_DONAUD MD\n" +
                                " RIGHT JOIN MASTER_STATE MS\n" +
                                "    ON MS.STATE_ID = MD.STATE_ID\n" +
                        "  GROUP BY MS.STATE_NAME ";
                        break;
                    }
                case SQLCommand.DonorProspect.GetDonaudByStateID:
                    {
                        query = "SELECT DONAUD_ID, NAME FROM MASTER_DONAUD WHERE STATE_ID IN (?PROJECT_ID);";
                        break;
                    }
                case SQLCommand.DonorProspect.GetTagID:
                    {
                        query = "SELECT TAG_ID,TAG_NAME FROM MASTER_DONOR_TAGS WHERE LETTER_TYPE_ID=5";
                        break;
                    }
                case SQLCommand.DonorProspect.GetDonorTags:
                    {
                        //query = "SELECT TAG_ID,TAG_NAME FROM MASTER_DONOR_TAGS {WHERE TAG_ID IN (?TAG_ID)};";
                        query = "SELECT TAG_ID,TAG_NAME FROM MASTER_DONOR_TAGS;";
                        break;
                    }
            }
            return query;
        }
    }
}
