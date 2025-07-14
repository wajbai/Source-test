using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class DonorAuditorSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.DonorAuditor).FullName)
            {
                query = GetDonorAuditorSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the Donor auditor details.
        /// </summary>
        /// <returns></returns>
        private string GetDonorAuditorSQL()
        {
            string query = "";
            SQLCommand.DonorAuditor sqlCommandId = (SQLCommand.DonorAuditor)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.DonorAuditor.Add:
                    {
                        //query = "INSERT INTO MASTER_DONAUD ( " +
                        //       "NAME, " +
                        //       "TYPE, " +
                        //       "PLACE," +
                        //       "COMPANY_NAME," +
                        //       "COUNTRY_ID," +
                        //       "PINCODE," +
                        //       "PHONE," +
                        //       "FAX," +
                        //       "EMAIL," +
                        //       "IDENTITYKEY," +
                        //       "URL," +
                        //       "STATE_ID," +
                        //       "ADDRESS," +
                        //       "NOTES," +
                        //       "PAN) VALUES( " +
                        //       "?NAME, " +
                        //       "?TYPE, " +
                        //       "?PLACE," +
                        //       "?COMPANY_NAME," +
                        //       "?COUNTRY_ID," +
                        //       "?PINCODE," +
                        //       "?PHONE," +
                        //       "?FAX," +
                        //       "?EMAIL," +
                        //       "?IDENTITYKEY," +
                        //       "?URL," +
                        //       "?STATE_ID," +
                        //       "?ADDRESS," +
                        //       "?NOTES," +
                        //       "?PAN)";

                        query = "INSERT INTO MASTER_DONAUD\n" +
                        "  (NAME,\n" +
                        "   REGISTER_NO,\n" +
                        "   TYPE,\n" +
                        "   ECS_DURATION,\n" +
                        "   PLACE,\n" +
                        "   COMPANY_NAME,\n" +
                        "   COUNTRY_ID,\n" +
                        "   PINCODE,\n" +
                        "   PHONE,\n" +
                        "   TELEPHONE,\n" +
                        "   FAX,\n" +
                        "   EMAIL,\n" +
                        "   IDENTITYKEY,\n" +
                        "   URL,\n" +
                        "   STATE_ID,\n" +
                        "   ADDRESS,\n" +
                        "   NOTES,\n" +
                        "   PAN,INSTITUTIONAL_TYPE_ID,REGISTRATION_TYPE_ID,TITLE,GENDER,DOB,LANGUAGE,RELIGION,REFERRED_STAFF,OCCUPATION,DATE_OF_JOIN,DATE_OF_EXIT,ORG_EMPLOYED,STATUS,PAYMENT_MODE_ID,MARITAL_STATUS,ANNIVERSARY_DATE,LASTNAME,REASON_FOR_ACTIVE)\n" +
                        "VALUES\n" +
                        "  (?NAME,\n" +
                        "   ?REGISTER_NO,\n" +
                        "   ?TYPE,\n" +
                        "   ?ECS_DURATION,\n" +
                        "   ?PLACE,\n" +
                        "   ?COMPANY_NAME,\n" +
                        "   ?COUNTRY_ID,\n" +
                        "   ?PINCODE,\n" +
                        "   ?PHONE,\n" +
                        "   ?TELEPHONE,\n" +
                        "   ?FAX,\n" +
                        "   ?EMAIL,\n" +
                        "   ?IDENTITYKEY,\n" +
                        "   ?URL,\n" +
                        "   IF(?STATE_ID=0,null,?STATE_ID),\n" +
                        "   ?ADDRESS,\n" +
                        "   ?NOTES,\n" +
                        "   ?PAN,?INSTITUTIONAL_TYPE_ID,?REGISTRATION_TYPE_ID,?TITLE,?GENDER,?DOB,?LANGUAGE,?RELIGION,?REFERRED_STAFF,?OCCUPATION,?DATE_OF_JOIN,?DATE_OF_EXIT,?ORG_EMPLOYED,?STATUS,?PAYMENT_MODE_ID,?MARITAL_STATUS,?ANNIVERSARY_DATE,?LASTNAME,?REASON_FOR_ACTIVE);";
                        break;
                    }
                case SQLCommand.DonorAuditor.Update:
                    {
                        query = "UPDATE MASTER_DONAUD SET " +
                                    "NAME = ?NAME, " +
                                    "REGISTER_NO = ?REGISTER_NO, " +
                                    "TYPE =?TYPE, " +
                                    "ECS_DURATION=?ECS_DURATION,\n" +
                                    "PLACE=?PLACE, " +
                                    "COMPANY_NAME=?COMPANY_NAME, " +
                                    "COUNTRY_ID=?COUNTRY_ID," +
                                    "PINCODE=?PINCODE," +
                                    "PHONE=?PHONE, " +
                                    "TELEPHONE=?TELEPHONE,\n" +
                                    "FAX=?FAX, " +
                                    "EMAIL=?EMAIL, " +
                                    "IDENTITYKEY=?IDENTITYKEY, " +
                                    "URL=?URL, " +
                                    "STATE_ID=IF(?STATE_ID=0,null,?STATE_ID), " +
                                    "ADDRESS=?ADDRESS ," +
                                    "STATUS=?STATUS," +
                                    "NOTES=?NOTES ," +
                                    "PAN=?PAN," +
                                    " INSTITUTIONAL_TYPE_ID=?INSTITUTIONAL_TYPE_ID,\n" +
                                    " REGISTRATION_TYPE_ID=?REGISTRATION_TYPE_ID,\n" +
                                    " TITLE=?TITLE,\n" +
                                    " GENDER=?GENDER,\n" +
                                    " DOB=?DOB,\n" +
                                    " LANGUAGE=?LANGUAGE,\n" +
                                    " RELIGION=?RELIGION,\n" +
                                    " REFERRED_STAFF=?REFERRED_STAFF,\n" +
                                    " OCCUPATION=?OCCUPATION,\n" +
                                    " DATE_OF_JOIN=?DATE_OF_JOIN,\n" +
                                    " DATE_OF_EXIT=?DATE_OF_EXIT,\n" +
                                    " ORG_EMPLOYED=?ORG_EMPLOYED,\n" +
                                    " PAYMENT_MODE_ID=?PAYMENT_MODE_ID,\n" +
                                    " MARITAL_STATUS=?MARITAL_STATUS,\n" +
                                    " ANNIVERSARY_DATE=?ANNIVERSARY_DATE,\n" +
                                    " LASTNAME=?LASTNAME,REASON_FOR_ACTIVE=?REASON_FOR_ACTIVE\n" +
                                    "WHERE DONAUD_ID=?DONAUD_ID ";
                        break;
                    }
                case SQLCommand.DonorAuditor.Delete:
                    {
                        query = "DELETE FROM MASTER_DONAUD WHERE DONAUD_ID=?DONAUD_ID";
                        break;
                    }
                case SQLCommand.DonorAuditor.GetDonorId:
                    {
                        query = "SELECT DONAUD_ID FROM MASTER_DONAUD WHERE NAME=?NAME AND ADDRESS=?ADDRESS AND PLACE=?PLACE";
                        break;
                    }
                case SQLCommand.DonorAuditor.GetIdDonorName:
                    {
                        query = "SELECT DONAUD_ID FROM MASTER_DONAUD WHERE NAME=?NAME";
                        break;
                    }
                case SQLCommand.DonorAuditor.Fetch:
                    {
                        query = "SELECT " +
                                "NAME, " +
                                "REGISTER_NO," +
                                "TYPE, " +
                                "ECS_DURATION, " +
                                "PLACE," +
                                "COMPANY_NAME," +
                                "COUNTRY_ID," +
                                "PINCODE," +
                                "PHONE," +
                                "FAX," +
                                "EMAIL," +
                                "IDENTITYKEY," +
                                "URL," +
                                "STATE_ID," +
                                "ADDRESS, " +
                                "NOTES, " +
                                "PAN," +
                                " INSTITUTIONAL_TYPE_ID,\n" +
                                " REGISTRATION_TYPE_ID,\n" +
                                " TITLE,\n" +
                                " GENDER,\n" +
                                " DOB,\n" +
                                " LANGUAGE,\n" +
                                " RELIGION,\n" +
                                " REFERRED_STAFF,\n" +
                                " OCCUPATION,\n" +
                                " DATE_OF_JOIN,\n" +
                                " DATE_OF_EXIT,\n" +
                                " ORG_EMPLOYED,\n" +
                                " STATUS,\n" +
                                " PAYMENT_MODE_ID,\n" +
                                " MARITAL_STATUS,\n" +
                                " ANNIVERSARY_DATE,\n" +
                                " LASTNAME,REASON_FOR_ACTIVE\n" +
                            "FROM " +
                                "MASTER_DONAUD " +
                                " WHERE DONAUD_ID=?DONAUD_ID ";
                        break;
                    }
                case SQLCommand.DonorAuditor.FetchDonor:
                    {
                        query = "SELECT " +
                                "DONAUD_ID, " +
                                "NAME, " +
                                "REGISTER_NO, " +
                                "PLACE," +
                                "D.COUNTRY_ID," +
                                "COUNTRY," +
                                "PHONE," +
                                "STATE_NAME," +
                                "RELIGION," +
                                "LANGUAGE," +
                                "ADDRESS," +
                                "PINCODE," +
                                "FAX," +
                                "EMAIL," +
                                "URL " +
                            "FROM " +
                                "MASTER_DONAUD D " +
                                "LEFT JOIN MASTER_COUNTRY C ON D.COUNTRY_ID=C.COUNTRY_ID " +
                                "LEFT JOIN MASTER_STATE MS ON D.STATE_ID=MS.STATE_ID " +
                                " WHERE IDENTITYKEY=0" +
                                " ORDER BY NAME ASC";
                        break;
                    }

                case SQLCommand.DonorAuditor.FetchDonorByStatus:
                    {
                        query = "SELECT " +
                                 "DONAUD_ID, " +
                                 "REGISTER_NO," +
                                 "TRIM(CONCAT(NAME,' ',IF(LASTNAME IS NULL,' ',LASTNAME))) AS NAME, " +
                                 "REGISTRATION_TYPE, " +
                                 "PLACE," +
                                 "D.COUNTRY_ID," +
                                 "COUNTRY," +
                                 "PHONE," +
                                 "TELEPHONE," +
                                 "REFERRED_STAFF," +
                                 "STATE_NAME," +
                                 "ADDRESS," +
                                 "PINCODE," +
                                 "FAX," +
                                 "EMAIL," +
                                 "URL," +
                                 "DATE_OF_JOIN," +
                                 "DATE_OF_EXIT," +
                                "       CASE\n" +
                                "         WHEN STATUS = 0 THEN\n" +
                                "          'InActive'\n" +
                                "         ELSE\n" +
                                "          'Active'\n" +
                                "       END AS STATUS\n" +
                             "FROM " +
                                 "MASTER_DONAUD D " +
                                 "LEFT JOIN MASTER_COUNTRY C ON D.COUNTRY_ID=C.COUNTRY_ID\n" +
                                 "LEFT JOIN MASTER_STATE MS ON D.STATE_ID=MS.STATE_ID\n " +
                                 "LEFT JOIN MASTER_DONAUD_REG_TYPE MDRT ON MDRT.REGISTRATION_TYPE_ID=D.REGISTRATION_TYPE_ID\n " +
                                 " WHERE IDENTITYKEY=0 \n" +
                                 "{ AND MDRT.REGISTRATION_TYPE_ID =?REGISTRATION_TYPE_ID }\n " +
                                 " AND STATUS IN (?STATUS)\n " +
                                 " ORDER BY NAME ASC";
                        break;
                    }
                case SQLCommand.DonorAuditor.FetchMailingContributedStatus:
                    {
                        query = "SELECT MAD.DONAUD_ID,\n" +
                                "       MAD.NAME,\n" +
                                "       VOUCHER_ID,\n" +
                                "       MP.PROJECT,\n" +
                                "       VOUCHER_DATE,\n" +
                                "       VOUCHER_NO AS 'VOUCHER NO',\n" +
                                "       ACTUAL_AMOUNT AS 'CONTRIBUTION AMOUNT',\n" +
                                "       MC.COUNTRY,\n" +
                                "       MS.STATE_NAME,\n" +
                                "       CONCAT(IFNULL(MS.STATE_NAME,' '), CONCAT(' ', MAD.PINCODE)) AS 'STATE',\n" +
                                "       PLACE,\n" +
                                "       PLACE AS CITYPLACE,\n" +
                                "       RELIGION,\n" +
                                "       ADDRESS,\n" +
                                "       PINCODE,\n" +
                                "       EMAIL,\n" +
                                "       PHONE AS 'MOBILE NO',\n" +
                                "CASE WHEN ?COMMUNICATION_MODE=1 THEN\n" +
                                "IF(DONOR_MAIL_STATUS=0,'Not Sent','Sent')\n" +
                                "ELSE IF(DONOR_SMS_STATUS=0,'Not Sent','Sent') END AS STATUS\n" +
                                "  FROM MASTER_DONAUD AS MAD\n" +
                                "  LEFT JOIN VOUCHER_MASTER_TRANS AS VMT\n" +
                                "    ON VMT.DONOR_ID = MAD.DONAUD_ID\n" +
                                "  LEFT JOIN MASTER_PROJECT AS MP\n" +
                                "    ON VMT.PROJECT_ID= MP.PROJECT_ID\n" +
                            //"  LEFT JOIN MASTER_DONAUD AS MAD\n" +
                            //"    ON VMT.DONOR_ID = MAD.DONAUD_ID\n" +
                                "  LEFT JOIN MASTER_COUNTRY AS MC\n" +
                                "    ON MC.COUNTRY_ID = MAD.COUNTRY_ID\n" +
                                "  LEFT JOIN MASTER_STATE AS MS\n" +
                                "    ON MS.STATE_ID = MAD.STATE_ID\n" +
                                " WHERE CASE\n" +
                                "         WHEN 1 =?COMMUNICATION_MODE  THEN\n" +
                                "           IF(?STATUS = 2, DONOR_MAIL_STATUS IN(0,1), DONOR_MAIL_STATUS IN(?STATUS))\n" +
                                "         ELSE\n" +
                                "          IF(?STATUS = 2, DONOR_SMS_STATUS IN(0,1), DONOR_SMS_STATUS IN (?STATUS))\n" +
                                "       END\n" +
                                "   AND CONTRIBUTION_AMOUNT > 0\n" +
                                "   AND VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                                " ORDER BY STATUS ASC;";

                        //query = "SELECT MAD.DONAUD_ID,\n" +
                        //     "       MAD.NAME,\n" +
                        //     "       VOUCHER_ID,\n" +
                        //     "       VOUCHER_DATE,\n" +
                        //     "       VOUCHER_NO,\n" +
                        //     "       ACTUAL_AMOUNT AS CONTRIBUTION_AMOUNT,\n" +
                        //     "       MC.COUNTRY,\n" +
                        //     "       MS.STATE_NAME,\n" +
                        //     "       PLACE,\n" +
                        //     "       ADDRESS,\n" +
                        //     "       EMAIL,\n" +
                        //     "       PHONE,VMT.DONOR_MAIL_STATUS AS 'MAIL_STATUS',VMT.DONOR_SMS_STATUS AS 'SMS_STATUS'\n" +
                        //     "  FROM VOUCHER_MASTER_TRANS AS VMT\n" +
                        //     "  LEFT JOIN MASTER_DONAUD AS MAD\n" +
                        //     "    ON VMT.DONOR_ID = MAD.DONAUD_ID\n" +
                        //     "  LEFT JOIN MASTER_COUNTRY AS MC\n" +
                        //     "    ON MC.COUNTRY_ID = MAD.COUNTRY_ID\n" +
                        //     "  LEFT JOIN MASTER_STATE AS MS\n" +
                        //     "    ON MS.STATE_ID = MAD.STATE_ID\n" +
                        //     " WHERE IF(?COMMUNICATION_MODE=1, 'DONOR_MAIL_STATUS IN(','DONOR_SMS_STATUS IN(')  AND CONTRIBUTION_AMOUNT > 0\n" +
                        //     "   AND VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED\n" +
                        //     "   ORDER BY VMT.DONOR_MAIL_STATUS ASC";
                        break;
                    }
                case SQLCommand.DonorAuditor.FetchMailingThanksByStatus:
                    {
                        query = "SELECT MAD.DONAUD_ID,\n" +
                             "       MAD.NAME,\n" +
                             "       VOUCHER_ID,\n" +
                             "       VOUCHER_DATE,\n" +
                             "       VOUCHER_NO AS 'VOUCHER NO',\n" +
                             "       ACTUAL_AMOUNT AS 'CONTRIBUTION AMOUNT',\n" +
                             "       MC.COUNTRY,\n" +
                             "       MS.STATE_NAME AS 'STATE',\n" +
                             "       PLACE,\n" +
                             "       ADDRESS,\n" +
                             "       EMAIL,\n" +
                             "       PHONE AS 'MOBILE NO',VMT.DONOR_MAIL_STATUS AS 'STATUS'\n" +
                             "  FROM VOUCHER_MASTER_TRANS AS VMT\n" +
                             "  LEFT JOIN MASTER_DONAUD AS MAD\n" +
                             "    ON VMT.DONOR_ID = MAD.DONAUD_ID\n" +
                             "  LEFT JOIN MASTER_COUNTRY AS MC\n" +
                             "    ON MC.COUNTRY_ID = MAD.COUNTRY_ID\n" +
                             "  LEFT JOIN MASTER_STATE AS MS\n" +
                             "    ON MS.STATE_ID = MAD.STATE_ID\n" +
                             " WHERE DONOR_MAIL_STATUS = ?STATUS AND CONTRIBUTION_AMOUNT > 0\n" +
                             "   AND VOUCHER_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED ORDER BY VMT.DONOR_MAIL_STATUS ASC";
                        break;
                    }
                case SQLCommand.DonorAuditor.FetchAuditor:
                    {
                        query = "SELECT " +
                                "DONAUD_ID, " +
                                "NAME, " +
                                "PLACE," +
                                "COMPANY_NAME," +
                                "COUNTRY," +
                                "PHONE," +
                                "STATE_NAME," +
                                "ADDRESS," +
                                "PINCODE," +
                                "FAX," +
                                "EMAIL," +
                                "URL " +
                            "FROM " +
                                "MASTER_DONAUD A " +
                                " INNER JOIN MASTER_COUNTRY C ON A.COUNTRY_ID=C.COUNTRY_ID " +
                                " INNER JOIN MASTER_STATE MS ON MS.STATE_ID=A.STATE_ID " +
                                " WHERE IDENTITYKEY=1 " +
                                " ORDER BY NAME ASC";
                        break;
                    }

                case SQLCommand.DonorAuditor.FetchAuditorList:
                    {
                        query = "SELECT DONAUD_ID, NAME, IDENTITYKEY\n" +
                                "  FROM MASTER_DONAUD\n" +
                                " WHERE IDENTITYKEY = 1\n" +
                                "ORDER BY NAME ASC";
                        ;
                        break;
                    }

                case SQLCommand.DonorAuditor.DeleteDonourDetails:
                    {
                        query = "DELETE FROM MASTER_DONAUD";
                        break;
                    }

                #region Donor Refernce Details
                case SQLCommand.DonorAuditor.DeleteDonorRefDetails:
                    {
                        query = "DELETE FROM MASTER_DONOR_INFO WHERE DONOR_ID=?DONOR_ID";
                        break;
                    }
                case SQLCommand.DonorAuditor.UpdateDonorReferenceDetails:
                    {
                        query = "UPDATE MASTER_DONOR_INFO SET " +
                                    "LANGUAGE = ?LANGUAGE, " +
                                    "RELIGION =?RELIGION, " +
                                    "OCCUPATION=?OCCUPATION, " +
                                    "DURATION=?DURATION, " +
                                    "GENDER=?GENDER," +
                                    "DATE_OF_BIRTH=?DATE_OF_BIRTH," +
                                    "PERIODIC=?PERIODIC, " +
                                    "REFEREDSTAFF=?REFEREDSTAFF " +
                                    "WHERE DONOR_ID=?DONOR_ID ";
                        break;
                    }
                case SQLCommand.DonorAuditor.AddDonorReferenceDetails:
                    {
                        query = "INSERT INTO MASTER_DONOR_INFO ( " +
                               "TITLE, " +
                               "LANGUAGE, " +
                               "RELIGION, " +
                               "OCCUPATION," +
                               "DURATION," +
                               "GENDER," +
                               "DATE_OF_BIRTH," +
                               "PERIODIC," +
                               "REFEREDSTAFF,DONOR_ID) VALUES( " +
                               "?TITLE, " +
                               "?LANGUAGE, " +
                               "?RELIGION, " +
                               "?OCCUPATION," +
                               "?DURATION," +
                               "?GENDER," +
                               "?DATE_OF_BIRTH," +
                               "?PERIODIC," +
                               "?REFEREDSTAFF,?DONOR_ID)";
                        break;
                    }
                case SQLCommand.DonorAuditor.FetchDonorReferenceDetails:
                    {
                        query = "SELECT " +
                               "LANGUAGE, " +
                               "RELIGION, " +
                               "OCCUPATION," +
                               "DURATION," +
                               "GENDER," +
                               "DATE_OF_BIRTH," +
                               "PERIODIC," +
                               "REFEREDSTAFF,DONOR_ID " +
                            "FROM " +
                                "MASTER_DONOR_INFO " +
                                " WHERE DONOR_ID=?DONOR_ID ";
                        break;
                    }
                case SQLCommand.DonorAuditor.FetchInstitutionalTypeByName:
                    {
                        query = "SELECT INSTITUTIONAL_TYPE_ID\n" +
                        "  FROM MASTER_DONAUD_INS_TYPE\n" +
                        " WHERE INSTITUTIONAL_TYPE IN (?INSTITUTIONAL_TYPE);";
                        break;
                    }
                case SQLCommand.DonorAuditor.FetchPaymentModeIdByPaymentMode:
                    {

                        query = "SELECT PAYMENT_MODE_ID\n" +
                        "  FROM MASTER_DONAUD_PAY_MODE\n" +
                        " WHERE PAYMENT_MODE IN (?PAYMENT_MODE);";
                        break;
                    }
                case SQLCommand.DonorAuditor.FetchRegTypeByRegTypeId:
                    {
                        query = "SELECT REGISTRATION_TYPE_ID\n" +
                        "  FROM MASTER_DONAUD_REG_TYPE\n" +
                        " WHERE REGISTRATION_TYPE =?REGISTRATION_TYPE;";
                        break;
                    }
                case SQLCommand.DonorAuditor.FetchReasonForInactive:
                    {
                        query = "SELECT REASON_FOR_ACTIVE FROM MASTER_DONAUD GROUP BY REASON_FOR_ACTIVE;";
                        break;
                    }
                case SQLCommand.DonorAuditor.FetchDonorByname:
                    {
                        query = "SELECT TRIM(CONCAT(NAME,' ',IF(LASTNAME IS NULL,' ',LASTNAME))) AS NAME,\n" +
                        "       TYPE,\n" +
                        "       PLACE,\n" +
                        "       COMPANY_NAME,\n" +
                        "       COUNTRY_ID,\n" +
                        "       PINCODE,\n" +
                        "       PHONE,\n" +
                        "       FAX,\n" +
                        "       EMAIL,\n" +
                        "       IDENTITYKEY,\n" +
                        "       URL,\n" +
                        "       FCDONOR,\n" +
                        "       STATE,\n" +
                        "       ADDRESS,\n" +
                        "       NOTES,\n" +
                        "       PAN,\n" +
                        "       BRANCH_ID,\n" +
                        "       CUSTOMERID,\n" +
                        "       STATE_ID,\n" +
                        "       INSTITUTIONAL_TYPE_ID,\n" +
                        "       REGISTRATION_TYPE_ID,\n" +
                        "       TITLE,\n" +
                        "       GENDER,\n" +
                        "       DOB,\n" +
                        "       LANGUAGE,\n" +
                        "       RELIGION,\n" +
                        "       REFERRED_STAFF,\n" +
                        "       OCCUPATION,\n" +
                        "       ORG_EMPLOYED,\n" +
                        "       STATUS,\n" +
                        "       PAYMENT_MODE_ID,\n" +
                        "       MARITAL_STATUS,\n" +
                        "       ANNIVERSARY_DATE,\n" +
                        "       REASON_FOR_ACTIVE,\n" +
                        "       LASTNAME\n" +
                        "  FROM master_donaud\n" +
                        " WHERE NAME LIKE '%?NAME%';";
                        break;
                    }
                #endregion
            }
            return query;
        }
        #endregion Bank SQL
    }
}
