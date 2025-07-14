using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class DonorFrontOfficeSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.FrontOffice).FullName)
            {
                query = GetFrontOfficeSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        public string GetFrontOfficeSQL()
        {
            string query = "";
            SQLCommand.FrontOffice sqlCommandId = (SQLCommand.FrontOffice)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {
                case SQLCommand.FrontOffice.FetchFeastDonorTemplateTypes:
                    {
                        //To load the Feast day templates
                        query = "SELECT TEMPLATE_ID,LETTER_TYPE_ID,NAME AS TEMPLATE_NAME, TRIM(REPLACE(NAME, '.rtf', ' ')) AS NAME\n" +
                        "  FROM MASTER_DONOR_TEMPLATES\n" +
                        " WHERE LETTER_TYPE_ID = ?LETTER_TYPE_ID AND COMMUNICATION_MODE=?COMMUNICATION_MODE;";
                        //"   AND NAME <> 'FeastDay.rtf';";

                        break;
                    }
                case SQLCommand.FrontOffice.SaveTemplate:
                    {
                        query = "INSERT INTO MASTER_DONOR_TEMPLATES\n" +
                        "  (LETTER_TYPE_ID, NAME,CONTENT,COMMUNICATION_MODE)\n" +
                        "VALUES\n" +
                        "  (?LETTER_TYPE_ID,?NAME,?CONTENT,?COMMUNICATION_MODE) ON DUPLICATE KEY UPDATE CONTENT=?CONTENT;";
                        break;
                    }
                case SQLCommand.FrontOffice.CheckFeastNameExists:
                    {
                        query = "SELECT COUNT(*)\n" +
                        "  FROM MASTER_DONOR_TEMPLATES\n" +
                            //" WHERE TEMPLATE_TYPE = 'FEASTDAY'\n" +
                        "   WHERE NAME =?NAME AND COMMUNICATION_MODE=?COMMUNICATION_MODE;";

                        break;
                    }
                case SQLCommand.FrontOffice.ThanksgivingMailStatus:
                    {
                        query = "UPDATE VOUCHER_MASTER_TRANS SET DONOR_MAIL_STATUS =1 WHERE VOUCHER_ID =?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.FrontOffice.AppealMailStatus:
                    {
                        query = "UPDATE MASTER_DONAUD SET APPEAL_SENT_DATE_EMAIL= NOW() WHERE DONAUD_ID =?DONOR_ID AND IDENTITYKEY=0";
                        break;
                    }
                case SQLCommand.FrontOffice.UpdateAppealSMSStatus:
                    {
                        query = "UPDATE MASTER_DONAUD SET APPEAL_SENT_DATE_SMS = NOW() WHERE DONAUD_ID =?DONOR_ID AND IDENTITYKEY=0";
                        break;
                    }
                case SQLCommand.FrontOffice.FetchNonPerformingDonors:
                    {
                        //query = "SELECT DONAUD_ID,\n" +
                        //"       CONCAT(MD.NAME, ' ', MD.LASTNAME) AS 'NAME',\n" +
                        //"       CASE\n" +
                        //"         WHEN TYPE = 1 THEN\n" +
                        //"          'Institutional'\n" +
                        //"         ELSE\n" +
                        //"          'Individual'\n" +
                        //"       END AS TYPE,\n" +
                        //"       MC.COUNTRY,\n" +
                        //"       MD.PLACE AS 'CITYPLACE',\n" +
                        //"       MS.STATE_NAME,\n" +
                        //"       CONCAT(IFNULL(MS.STATE_NAME,' '), CONCAT(' ', MD.PINCODE)) AS 'STATE',\n" +
                        //"       PHONE AS 'MOBILE NO',\n" +
                        //"       MD.ADDRESS,\n" +
                        //"       VMT.ACTUAL_AMOUNT AS 'CONTRIBUTION AMOUNT',\n" +
                        //"       MD.PAN,\n" +
                        //"       'Donor' AS 'MEMBER TYPE',\n" +
                        //"       MDPM.PAYMENT_MODE AS 'PAYMENT MODE',\n" +
                        //"       MDRT.REGISTRATION_TYPE AS 'REGISTRATION TYPE', MCH.FC_PURPOSE AS 'PURPOSE',\n" +
                        //"       MD.URL,\n" +
                        //"       VMT.VOUCHER_NO AS 'VOUCHER NO',\n" +
                        //"       CASE\n" +
                        //"         WHEN ?COMMUNICATION_MODE = 1 THEN\n" +
                        //"          MD.APPEAL_SENT_DATE_EMAIL\n" +
                        //"         ELSE\n" +
                        //"          MD.APPEAL_SENT_DATE_SMS\n" +
                        //"       END AS LAST_APPEAL,\n" +
                        //"       MD.EMAIL\n" +
                        //"  FROM MASTER_DONAUD MD\n" +
                        //"  LEFT JOIN MASTER_COUNTRY MC\n" +
                        //"    ON MC.COUNTRY_ID = MD.COUNTRY_ID\n" +
                        //"  LEFT JOIN MASTER_STATE MS\n" +
                        //"    ON MS.STATE_ID = MD.STATE_ID\n" +
                        //"  LEFT JOIN VOUCHER_MASTER_TRANS VMT\n" +
                        //"    ON VMT.DONOR_ID = MD.DONAUD_ID\n" +
                        //"  LEFT JOIN MASTER_DONAUD_PAY_MODE MDPM\n" +
                        //"    ON MDPM.PAYMENT_MODE_ID = MD.PAYMENT_MODE_ID\n" +
                        //"  LEFT JOIN MASTER_DONAUD_REG_TYPE MDRT\n" +
                        //"    ON MDRT.REGISTRATION_TYPE_ID = MD.REGISTRATION_TYPE_ID\n" +
                        //"  LEFT JOIN MASTER_CONTRIBUTION_HEAD MCH\n" +
                        //"    ON MCH.CONTRIBUTION_ID = VMT.PURPOSE_ID\n" +
                        //" WHERE DONAUD_ID NOT IN (SELECT DONOR_ID\n" +
                        //"                           FROM VOUCHER_MASTER_TRANS\n" +
                        //"                          WHERE VOUCHER_DATE >= ?VOUCHER_DATE\n" +
                        //"                            AND DONOR_ID <> 0\n" +
                        //"                            AND STATUS = 1\n" +
                        //"                          GROUP BY DONOR_ID)\n" +
                        //"   AND MD.IDENTITYKEY = 0 GROUP BY MD.DONAUD_ID;";

                        query = "SELECT DONAUD_ID,\n" +
                                "       CONCAT(MD.NAME, ' ', MD.LASTNAME) AS 'NAME',\n" +
                                "       CASE\n" +
                                "         WHEN TYPE = 1 THEN\n" +
                                "          'Institutional'\n" +
                                "         ELSE\n" +
                                "          'Individual'\n" +
                                "       END AS TYPE,\n" +
                                "       MC.COUNTRY,\n" +
                                "       MD.PLACE AS 'CITYPLACE',\n" +
                                "       MS.STATE_NAME,\n" +
                                "       CONCAT(IFNULL(MS.STATE_NAME, ' '), CONCAT(' ', MD.PINCODE)) AS 'STATE',\n" +
                                "       RELIGION,\n" +
                                "       PHONE AS 'MOBILE NO',\n" +
                                "       MD.ADDRESS,\n" +
                                "       MD.PINCODE,\n" +
                                "       MD.PAN,\n" +
                                "       'Donor' AS 'MEMBER TYPE',\n" +
                                "       MDPM.PAYMENT_MODE AS 'PAYMENT MODE',\n" +
                                "       MDRT.REGISTRATION_TYPE AS 'REGISTRATION TYPE',\n" +
                                "       MD.URL,\n" +
                                "       CASE\n" +
                                "         WHEN 1 = 1 THEN\n" +
                                "          MD.APPEAL_SENT_DATE_EMAIL\n" +
                                "         ELSE\n" +
                                "          MD.APPEAL_SENT_DATE_SMS\n" +
                                "       END AS LAST_APPEAL,\n" +
                                "       MD.EMAIL\n" +
                                "  FROM MASTER_DONAUD MD\n" +
                                "  LEFT JOIN MASTER_COUNTRY MC\n" +
                                "    ON MC.COUNTRY_ID = MD.COUNTRY_ID\n" +
                                "  LEFT JOIN MASTER_STATE MS\n" +
                                "    ON MS.STATE_ID = MD.STATE_ID\n" +
                                "\n" +
                                "  LEFT JOIN MASTER_DONAUD_PAY_MODE MDPM\n" +
                                "    ON MDPM.PAYMENT_MODE_ID = MD.PAYMENT_MODE_ID\n" +
                                "  LEFT JOIN MASTER_DONAUD_REG_TYPE MDRT\n" +
                                "    ON MDRT.REGISTRATION_TYPE_ID = MD.REGISTRATION_TYPE_ID\n" +
                                " WHERE DONAUD_ID NOT IN (SELECT DONOR_ID\n" +
                                "                           FROM VOUCHER_MASTER_TRANS\n" +
                                "                          WHERE VOUCHER_DATE >= ?VOUCHER_DATE\n" +
                                "                            AND DONOR_ID <> 0\n" +
                                "                            AND STATUS = 1\n" +
                                "                          GROUP BY DONOR_ID)\n" +
                                "   AND MD.IDENTITYKEY = 0\n" +
                                " GROUP BY MD.DONAUD_ID;";

                        break;
                    }
                case SQLCommand.FrontOffice.InsertSentLetters:
                    {
                        query = "INSERT INTO MASTER_DONOR_MAILING_HISTORY\n" +
                        "  (DONPROS_ID,TYPE,LETTER_TYPE_ID, SENT_DATE,COMMUNICATION_MODE)\n" +
                        "VALUES\n" +
                        "  (?DONOR_ID,?TYPE,?NAME,?DOB,?COMMUNICATION_MODE);";
                        break;
                    }
                case SQLCommand.FrontOffice.UpdateFeastStatus:
                    {
                        query = "UPDATE master_donor_letter_tags\n" +
                                "   SET STATUS = 1, SENT_DATE = NOW()\n" +
                                " WHERE TAG_ID =?TAG_ID AND REF_ID =?REF_ID AND TYPE = ?TYPE_ID AND COMMUNICATION_MODE=?COMMUNICATION_MODE";
                        break;
                    }
                case SQLCommand.FrontOffice.InsertTask:
                    {
                        query = "INSERT INTO MASTER_DONOR_TAGS\n" +
                                "  (TAG_ID, TAG_NAME,  NEWS_LETTER,TAG_CREATED_DATE, LETTER_TYPE_ID,TEMPLATE_ID)\n" +
                                "VALUES\n" +
                                "  (?TAG_ID,\n" +
                                "   ?TAG_NAME,\n" +
                                "   ?NEWS_LETTER,\n" +
                                "   NOW(),\n" +
                                "   ?LETTER_TYPE_ID,?ID)";
                        break;
                    }
                case SQLCommand.FrontOffice.UpdateTask:
                    {
                        query = "UPDATE MASTER_DONOR_TAGS SET TAG_NAME=?TAG_NAME,NEWS_LETTER=?NEWS_LETTER,TAG_CREATED_DATE=NOW(),LETTER_TYPE_ID=?LETTER_TYPE_ID, TEMPLATE_ID=?ID WHERE LETTER_TYPE_ID=?LETTER_TYPE_ID";
                        break;
                    }
                case SQLCommand.FrontOffice.FetchTaskDetails:
                    {
                        query = "SELECT TAG_ID, TAG_NAME,NEWS_LETTER,TRIM(REPLACE(MT.NAME, '.rtf', ' ')) AS NAME,MT.TEMPLATE_ID FROM MASTER_DONOR_TAGS MDT\n" +
                                " LEFT JOIN MASTER_DONOR_TEMPLATES MT ON  MDT.TEMPLATE_ID=MT.TEMPLATE_ID\n" +
                                " WHERE MDT.LETTER_TYPE_ID=?LETTER_TYPE_ID";
                        break;
                    }
                case SQLCommand.FrontOffice.FetchTaskByTagId:
                    {
                        query = @"SELECT TAG_ID, TAG_NAME,NEWS_LETTER, TEMPLATE_ID AS ID FROM MASTER_DONOR_TAGS WHERE TAG_ID=?TAG_ID";
                        break;
                    }
                case SQLCommand.FrontOffice.FetchDonorMappedStatus:
                    {
                        query = "SELECT D.DONAUD_ID,\n" +
                                "       NAME,\n" +
                                "       PLACE,\n" +
                                "       D.COUNTRY_ID,\n" +
                                "       COUNTRY,\n" +
                                "       PHONE,\n" +
                                "       STATE_NAME,\n" +
                                "       RELIGION,\n" +
                                "       LANGUAGE,\n" +
                                "       ADDRESS,\n" +
                                "       PINCODE,\n" +
                                "       FAX,\n" +
                                "       EMAIL,\n" +
                                "       IF(T.DONAUD_ID IS NULL, 0, 1) AS MAPPED_STATUS,\n" +
                                "       URL\n" +
                                "  FROM MASTER_DONAUD D\n" +
                                "  LEFT JOIN MASTER_COUNTRY C\n" +
                                "    ON D.COUNTRY_ID = C.COUNTRY_ID\n" +
                                "  LEFT JOIN MASTER_STATE MS\n" +
                                "    ON D.STATE_ID = MS.STATE_ID\n" +
                                "  LEFT JOIN (SELECT MDLT.REF_ID AS DONAUD_ID\n" +
                                "               FROM MASTER_DONOR_LETTER_TAGS MDLT\n" +
                                "              WHERE MDLT.TAG_ID = ?TAG_ID\n" +
                                "                AND MDLT.TYPE = 0\n" +
                                "                AND MDLT.COMMUNICATION_MODE =?COMMUNICATION_MODE) AS T\n" +
                                "    ON D.DONAUD_ID = T.DONAUD_ID\n" +
                                " WHERE IDENTITYKEY = 0 AND ((IF(?COMMUNICATION_MODE=1, EMAIL<>'', PHONE<>'')) OR (D.ADDRESS<>'Nil' AND LENGTH(TRIM(D.ADDRESS))<>0)) \n" +
                                " ORDER BY NAME ASC;";
                        break;
                    }
                case SQLCommand.FrontOffice.FetchProspectsMappedStatus:
                    {
                        query = "SELECT P.PROSPECT_ID,\n" +
                                "       TRIM(CONCAT(NAME, ' ', IF(LASTNAME IS NULL, ' ', LASTNAME))) AS NAME,\n" +
                                "       P.TYPE,\n" +
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
                                "       IF(T.PROSPECT_ID IS NULL, 0, 1) AS MAPPED_STATUS,\n" +
                                "       ANNIVERSARY_DATE\n" +
                                "  FROM MASTER_DONAUD_PROSPECTS P\n" +
                                "  LEFT JOIN MASTER_COUNTRY C\n" +
                                "    ON P.COUNTRY_ID = C.COUNTRY_ID\n" +
                                "  LEFT JOIN MASTER_STATE MS\n" +
                                "    ON P.STATE_ID = MS.STATE_ID\n" +
                                "  LEFT JOIN MASTER_DONAUD_REG_TYPE MDRT\n" +
                                "    ON MDRT.REGISTRATION_TYPE_ID = P.REGISTRATION_TYPE_ID\n" +
                                "  LEFT JOIN (SELECT MDLT.REF_ID AS PROSPECT_ID\n" +
                                "               FROM MASTER_DONOR_LETTER_TAGS MDLT\n" +
                                "              WHERE MDLT.TAG_ID = ?TAG_ID\n" +
                                "                AND MDLT.TYPE = 1\n" +
                                "                AND MDLT.COMMUNICATION_MODE =?COMMUNICATION_MODE) AS T\n" +
                                "    ON P.PROSPECT_ID = T.PROSPECT_ID\n" +
                                " WHERE ((IF(?COMMUNICATION_MODE = 1, EMAIL <> '', PHONE <> '')) OR\n" +
                                "       (P.ADDRESS <> 'Nil' AND LENGTH(TRIM(P.ADDRESS)) <> 0))\n" +
                                " ORDER BY NAME ASC;";

                        //query = "SELECT PROSPECT_ID,\n" +
                        //    "       TRIM(CONCAT(NAME, ' ', IF(LASTNAME IS NULL, ' ', LASTNAME))) AS NAME,\n" +
                        //    "       P.TYPE,\n" +
                        //    "       REGISTRATION_TYPE,\n" +
                        //    "       INSTITUTIONAL_TYPE_ID,\n" +
                        //    "       P.COUNTRY_ID,\n" +
                        //    "       COUNTRY,\n" +
                        //    "       LANGUAGE,\n" +
                        //    "       RELIGION,\n" +
                        //    "       PLACE,\n" +
                        //    "       STATE_NAME,\n" +
                        //    "       ADDRESS,\n" +
                        //    "       PINCODE,\n" +
                        //    "       PHONE,\n" +
                        //    "       FAX,\n" +
                        //    "       EMAIL,\n" +
                        //    "       URL,\n" +
                        //    "       SOURCE_INFORMATION,\n" +
                        //    "       P.REGISTRATION_TYPE_ID,\n" +
                        //    "       REFERENCE_NUMBER,\n" +
                        //    "       NOTES,\n" +
                        //    "       PAN,\n" +
                        //    "       TITLE,\n" +
                        //    "       GENDER,\n" +
                        //    "       DOB,\n" +
                        //    "       REFERRED_STAFF,\n" +
                        //    "       OCCUPATION,\n" +
                        //    "       PAYMENT_MODE_ID,\n" +
                        //    "       ORG_EMPLOYED,\n" +
                        //    "       MARITAL_STATUS,\n" +
                        //    "       IF(MDLT.TAG_ID =?TAG_ID AND MDLT.COMMUNICATION_MODE=?COMMUNICATION_MODE, 1, 0) AS MAPPED_STATUS,\n" +
                        //    "       ANNIVERSARY_DATE\n" +
                        //    "  FROM MASTER_DONAUD_PROSPECTS P\n" +
                        //    "  LEFT JOIN MASTER_COUNTRY C\n" +
                        //    "    ON P.COUNTRY_ID = C.COUNTRY_ID\n" +
                        //    "  LEFT JOIN MASTER_STATE MS\n" +
                        //    "    ON P.STATE_ID = MS.STATE_ID\n" +
                        //    "  LEFT JOIN MASTER_DONAUD_REG_TYPE MDRT\n" +
                        //    "    ON MDRT.REGISTRATION_TYPE_ID = P.REGISTRATION_TYPE_ID\n" +
                        //    "  LEFT JOIN MASTER_DONOR_LETTER_TAGS MDLT\n" +
                        //    "    ON P.PROSPECT_ID = MDLT.REF_ID\n" +
                        //    "   AND MDLT.TAG_ID = ?TAG_ID\n" +
                        //    "   AND MDLT.TYPE = 1 AND MDLT.COMMUNICATION_MODE=?COMMUNICATION_MODE\n" +
                        //    "   WHERE ((IF(?COMMUNICATION_MODE=1, EMAIL<>'', PHONE<>'')) OR (P.ADDRESS<>'Nil' AND LENGTH(TRIM(P.ADDRESS))<>0))\n" +
                        //    " ORDER BY NAME ASC;";
                        break;
                    }
                case SQLCommand.FrontOffice.FetchMappedDonorByTagId:
                    {
                        query = "SELECT T.TAG_ID,\n" +
                                "       T.REF_ID,\n" +
                                "       T.EMAIL_STATUS,\n" +
                                "       T.EMAIL_SENT_DATE,\n" +
                                "       T.NAME,\n" +
                                "       T.COUNTRY,\n" +
                                "       T.STATE_NAME,\n" +
                                "       T.STATE,\n" +
                                "       T.PLACE,\n" +
                                "       T.CITYPLACE,\n" +
                                "       T.PINCODE,\n" +
                                "       T.RELIGION,\n" +
                                "       T.LANGUAGE,\n" +
                                "       T.PHONE AS 'MOBILE NO',\n" +
                                "       T.FAX,\n" +
                                "       T.EMAIL,\n" +
                                "       T.URL,\n" +
                                "       T.REGISTRATION_TYPE AS 'REGISTRATION TYPE',\n" +
                                "       T.PAYMENT_MODE AS 'PAYMENT MODE',\n" +
                                "       T.ADDRESS,\n" +
                                "       T.PAN,\n" +
                                "       T.TYPE AS TYPE_ID,\n" +
                                "       CASE WHEN T.EMAIL_STATUS=0 THEN 'Not Sent' else 'Sent' END AS STATUS,\n" +
                                "       CASE WHEN T.TYPE=0 THEN 'Donor' else 'Prospect' END AS TYPE\n" +
                                "  FROM ((SELECT MDL.TAG_ID,\n" +
                                "                MDL.REF_ID,\n" +
                                "                MDL.STATUS AS EMAIL_STATUS,\n" +
                                "                MDL.SENT_DATE AS EMAIL_SENT_DATE,\n" +
                                "                MDL.TYPE,\n" +
                                "                MD.NAME,\n" +
                                "                MD.PLACE,\n" +
                                "                MD.PLACE AS CITYPLACE,\n" +
                                "                MD.PINCODE,\n" +
                                "                MD.RELIGION,\n" +
                                "                MD.LANGUAGE,\n" +
                                "                MC.COUNTRY,\n" +
                                "                MS.STATE_NAME,\n" +
                                "               CONCAT(IFNULL(MS.STATE_NAME,' '), CONCAT(' ', MD.PINCODE)) AS 'STATE',\n" +
                                "                MD.PHONE,\n" +
                                "                MD.FAX,\n" +
                                "                MD.EMAIL,\n" +
                                "                MD.ADDRESS,\n" +
                                "                MD.PAN,\n" +
                                "                MD.URL,\n" +
                                "                RT.REGISTRATION_TYPE,\n" +
                                "                PM.PAYMENT_MODE\n" +
                                "           FROM MASTER_DONOR_LETTER_TAGS MDL\n" +
                                "          INNER JOIN MASTER_DONAUD MD\n" +
                                "             ON MDL.REF_ID = MD.DONAUD_ID\n" +
                                "          LEFT JOIN  master_donaud_reg_type RT\n" +
                                "             ON RT.REGISTRATION_TYPE_ID=MD.REGISTRATION_TYPE_ID\n" +
                                "          LEFT JOIN master_donaud_pay_mode PM\n" +
                                "             ON PM.PAYMENT_MODE_ID=MD.PAYMENT_MODE_ID\n" +
                                "           LEFT JOIN MASTER_COUNTRY MC\n" +
                                "               ON MC.COUNTRY_ID=MD.COUNTRY_ID\n" +
                                "          LEFT JOIN MASTER_STATE MS\n" +
                                "             ON MD.STATE_ID = MS.STATE_ID\n" +
                                "          WHERE MDL.TYPE = 0 AND MDL.COMMUNICATION_MODE=?TYPE_ID) UNION\n" +
                                "        (SELECT MDL.TAG_ID,\n" +
                                "                MDL.REF_ID,\n" +
                                "                MDL.STATUS AS EMAIL_STATUS,\n" +
                                "                MDL.SENT_DATE AS EMAIL_SENT_DATE,\n" +
                                "                MDL.TYPE,\n" +
                                "                MP.NAME,\n" +
                                "                MP.PLACE,\n" +
                                "                MP.PLACE AS CITYPLACE,\n" +
                                "                MP.PINCODE,\n" +
                                "                MP.RELIGION,\n" +
                                "                MP.LANGUAGE,\n" +
                                "                MC.COUNTRY,\n" +
                                "                MS.STATE_NAME,\n" +
                                "                CONCAT(IFNULL(MS.STATE_NAME,' '), CONCAT(' ', MP.PINCODE)) AS 'STATE',\n" +
                                "                MP.PHONE,\n" +
                                "                MP.FAX,\n" +
                                "                MP.EMAIL,\n" +
                                "                MP.ADDRESS,\n" +
                                "                MP.PAN,\n" +
                                "                MP.URL,\n" +
                                "                RT.REGISTRATION_TYPE,\n" +
                                "                PM.PAYMENT_MODE\n" +
                                "           FROM MASTER_DONOR_LETTER_TAGS MDL\n" +
                                "          INNER JOIN MASTER_DONAUD_PROSPECTS MP\n" +
                                "             ON MDL.REF_ID = MP.PROSPECT_ID\n" +
                                "          LEFT JOIN  master_donaud_reg_type RT\n" +
                                "             ON RT.REGISTRATION_TYPE_ID=MP.REGISTRATION_TYPE_ID\n" +
                                "          LEFT JOIN master_donaud_pay_mode PM\n" +
                                "             ON PM.PAYMENT_MODE_ID=MP.PAYMENT_MODE_ID\n" +
                                "           LEFT JOIN MASTER_COUNTRY MC\n" +
                                "               ON MC.COUNTRY_ID=MP.COUNTRY_ID\n" +
                                "          LEFT JOIN MASTER_STATE MS\n" +
                                "             ON MP.STATE_ID = MS.STATE_ID\n" +
                                "          WHERE MDL.TYPE = 1 AND MDL.COMMUNICATION_MODE=?TYPE_ID)) AS T\n" +
                                " WHERE T.TAG_ID = ?TAG_ID {AND T.EMAIL_STATUS=?STATUS} ORDER BY T.REF_ID ASC;";
                        break;
                    }
                case SQLCommand.FrontOffice.MapTagDonor:
                    {
                        query = @"INSERT INTO MASTER_DONOR_LETTER_TAGS(TAG_ID, REF_ID,TYPE,COMMUNICATION_MODE)VALUES(?TAG_ID,?DONAUD_ID,?TYPE_ID,?LETTER_TYPE_ID)";
                        break;
                    }
                case SQLCommand.FrontOffice.DeleteTaskDetails:
                    {
                        query = @"DELETE FROM MASTER_DONOR_LETTER_TAGS WHERE TAG_ID=?TAG_ID AND COMMUNICATION_MODE=?COMMUNICATION_MODE";
                        break;
                    }
                case SQLCommand.FrontOffice.FetchDonorByTask:
                    {
                        query = "SELECT T.TAG_ID,\n" +
                                 "       T.REF_ID,\n" +
                                 "       T.EMAIL_STATUS,\n" +
                                 "       T.EMAIL_SENT_DATE,\n" +
                                 "       T.NAME,\n" +
                                 "       T.PLACE,\n" +
                                  "      T.PLACE AS CITYPLACE,\n" +
                                 "       T.COUNTRY,\n" +
                                 "       T.STATE AS STATE_NAME,\n" +
                                 "       CONCAT(IFNULL(T.STATE,' '), CONCAT(' ', T.PINCODE)) AS 'STATE',\n" +
                                 "       T.RELIGION,\n" +
                                 "       T.PINCODE,\n" +
                                 "       T.PHONE AS 'MOBILE NO',\n" +
                                 "       T.FAX,\n" +
                                 "       T.EMAIL,\n" +
                                 "       T.ADDRESS,\n" +
                            //  "       T.NEWS_LETTER,\n" +
                                 "       T.PAN,\n" +
                                 "       T.TYPE AS TYPE_ID,\n" +
                                 "       T.EMAIL_STATUS AS STATUS,\n" +
                                 "       CASE WHEN T.EMAIL_STATUS=0 THEN 'Not Sent'\n" +
                                 "       ELSE 'Sent' END AS MAIL_STATUS,\n" +
                                 "       CASE WHEN T.TYPE=0 THEN 'Donor' else 'Prospect' END AS TYPE\n" +
                                 "  FROM ((SELECT MDL.TAG_ID,\n" +
                                 "                MDL.REF_ID,\n" +
                                 "                MDL.STATUS AS EMAIL_STATUS,\n" +
                                 "                MDL.SENT_DATE AS EMAIL_SENT_DATE,\n" +
                                 "                MDL.TYPE,\n" +
                                 "                MD.NAME,\n" +
                                 "                MD.PLACE,\n" +
                                 "                MC.COUNTRY,\n" +
                                 "                MS.STATE_NAME AS STATE,\n" +
                                   "              MD.RELIGION,\n" +
                                 "                MD.PINCODE,\n" +
                                 "                MD.PHONE,\n" +
                                 "                MD.FAX,\n" +
                                 "                MD.EMAIL,\n" +
                                 "                MD.ADDRESS,\n" +
                            //   "                MDT.NEWS_LETTER,\n" +
                                 "                MD.PAN\n" +
                                 "           FROM MASTER_DONOR_LETTER_TAGS MDL\n" +
                                 "          INNER JOIN MASTER_DONAUD MD\n" +
                                 "             ON MDL.REF_ID = MD.DONAUD_ID\n" +
                                 "          LEFT JOIN MASTER_DONOR_TAGS MDT\n" +
                                 "           ON MDT.TAG_ID = MDL.TAG_ID\n" +
                                 "           LEFT JOIN MASTER_COUNTRY MC\n" +
                                 "               ON MC.COUNTRY_ID=MD.COUNTRY_ID\n" +
                                 "          LEFT JOIN MASTER_STATE MS\n" +
                                 "             ON MD.STATE_ID = MS.STATE_ID\n" +
                                 "          WHERE MDL.TYPE = 0) UNION\n" +
                                 "        (SELECT MDL.TAG_ID,\n" +
                                 "                MDL.REF_ID,\n" +
                                 "                MDL.STATUS AS EMAIL_STATUS,\n" +
                                 "                MDL.SENT_DATE AS EMAIL_SENT_DATE,\n" +
                                 "                MDL.TYPE,\n" +
                                 "                MP.NAME,\n" +
                                 "                MP.PLACE,\n" +
                                 "                MC.COUNTRY,\n" +
                                 "                MS.STATE_NAME AS STATE,\n" +
                                 "                MP.RELIGION,\n" +
                                 "                MP.PINCODE,\n" +
                                 "                MP.PHONE,\n" +
                                 "                MP.FAX,\n" +
                                 "                MP.EMAIL,\n" +
                                 "                MP.ADDRESS,\n" +
                            //   "                '' AS 'NEWS_LETTER',\n" +
                                 "                MP.PAN\n" +
                                 "           FROM MASTER_DONOR_LETTER_TAGS MDL\n" +
                                 "          INNER JOIN MASTER_DONAUD_PROSPECTS MP\n" +
                                 "             ON MDL.REF_ID = MP.PROSPECT_ID\n" +
                                 "           LEFT JOIN MASTER_COUNTRY MC\n" +
                                 "               ON MC.COUNTRY_ID=MP.COUNTRY_ID\n" +
                                 "          LEFT JOIN MASTER_STATE MS\n" +
                                 "             ON MP.STATE_ID = MS.STATE_ID\n" +
                                 "          WHERE MDL.TYPE = 1)) AS T\n" +
                                 " WHERE T.TAG_ID = ?TAG_ID {AND T.EMAIL_STATUS=?STATUS} ORDER BY T.REF_ID ASC;";
                        break;
                    }
                case SQLCommand.FrontOffice.FetchNewsLetterByTask:
                    {
                        query = "SELECT NEWS_LETTER,TAG_ID FROM MASTER_DONOR_TAGS WHERE TAG_ID=?TAG_ID";
                        break;
                    }
                case SQLCommand.FrontOffice.FetchAnniversaryTypeDetails:
                    {
                        query = "SELECT MAD.DONAUD_ID,\n" +
                        "       CONCAT(MAD.NAME, ' ', MAD.LASTNAME) AS 'NAME',\n" +
                        "       MAD.DOB,\n" +
                        "       DATE_FORMAT(MAD.DOB,'%d') as DOB_DAY,\n" +
                        "       DATE_FORMAT(MAD.DOB,'%m') as DOB_MONTH,\n" +
                        "       MC.COUNTRY,\n" +
                        "       MS.STATE_NAME,\n" +
                        "      CONCAT(IFNULL(MS.STATE_NAME,' '), CONCAT(' ', MAD.PINCODE)) AS 'STATE',\n" +
                        "       RELIGION,\n" +
                        "       MAD.PLACE,\n" +
                        "       MAD.PLACE AS 'CITYPLACE',\n" +
                        "       MAD.EMAIL,\n" +
                        "       MAD.PHONE AS 'MOBILE NO',\n" +
                        "       MAD.ADDRESS,\n" +
                        "       MAD.PINCODE,\n" +
                            //"       MAD.ANNIVERSARY_DATE,\n" +
                        "       STR_TO_DATE(MAD.ANNIVERSARY_DATE,'%Y-%m-%d') as ANNIVERSARY_DATE,\n" +
                        "       CASE WHEN MAD.GENDER=1 THEN 'Male' ELSE 'Female' END AS GENDER,\n" +
                        "       CURRENT_BIRTHDAY_DATE_EMAIL,\n" +
                        "       CURRENT_MARRIAGE_DATE_EMAIL,\n" +
                        "       CURRENT_BIRTHDAY_DATE_SMS,\n" +
                        "       CURRENT_MARRIAGE_DATE_SMS,\n" +
                        "       MARITAL_STATUS,\n" +
                       "  CASE WHEN ?COMMUNICATION_MODE=1 THEN\n" +
                          "  IF(CURRENT_BIRTHDAY_DATE_EMAIL BETWEEN ?DATE_STARTED AND\n" +
                       " ?DATE_CLOSED, 'Sent','Not Sent')\n" +
                        "ELSE IF(CURRENT_MARRIAGE_DATE_EMAIL BETWEEN ?DATE_STARTED  AND \n" +
                        " ?DATE_CLOSED,'Sent','Not Sent') END AS STATUS\n" +
                        "  FROM MASTER_DONAUD AS MAD\n" +
                        " LEFT JOIN MASTER_DONOR_LETTER_TAGS MDLT\n" +
                        "    ON MAD.DONAUD_ID = MDLT.REF_ID\n" +
                        " LEFT JOIN MASTER_DONOR_TAGS MDT\n" +
                        "    ON MDT.TAG_ID = MDLT.TAG_ID\n" +
                        "  LEFT JOIN MASTER_COUNTRY AS MC\n" +
                        "    ON MC.COUNTRY_ID = MAD.COUNTRY_ID\n" +
                        "  LEFT JOIN MASTER_STATE AS MS\n" +
                        "    ON MS.STATE_ID = MAD.STATE_ID\n" +
                        "   JOIN (SELECT DONAUD_ID," +
                         "  STR_TO_DATE(concat(month(dob),'/',day(dob),'/',if( month(dob)  >= month(?DATE_STARTED),year(?DATE_STARTED), year(?DATE_CLOSED))) ,'%c/%e/%Y') AS DOB_DATE," +
                       "         STR_TO_DATE(concat(month(ANNIVERSARY_DATE),'/',day(ANNIVERSARY_DATE),'/',if( month(ANNIVERSARY_DATE)  >= month(?DATE_STARTED),year(?DATE_STARTED),year(?DATE_CLOSED))),'%c/%e/%Y') AS ANV_DATE" +
                        "   FROM master_donaud) AS T " +
                        "   ON T.DONAUD_ID=MAD.DONAUD_ID" +
                        " WHERE IF(?MARITAL_STATUS_IDs = 0 ,\n" +
                        "        DOB_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED,\n" +
                        "          ANV_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED)\n" +
                        "GROUP BY DONAUD_ID ORDER BY DOB_DAY,DOB_MONTH ASC";
                        break;
                    }
                case SQLCommand.FrontOffice.UpdateAnniversaryMailStatus:
                    {
                        query = "UPDATE MASTER_DONAUD\n" +
                                "   SET { CURRENT_BIRTHDAY_DATE_EMAIL =?CURRENT_BIRTHDAY_DATE_EMAIL,BIRTHDAY_WISHING_DATE_EMAIL =NOW()} {CURRENT_MARRIAGE_DATE_EMAIL =?CURRENT_MARRIAGE_DATE_EMAIL,MARRIAGE_WISHING_DATE_EMAIL =NOW()}\n" +
                                " WHERE DONAUD_ID =?DONAUD_ID";
                        break;
                    }
                case SQLCommand.FrontOffice.FetchAnniversaryTypeSMSDetails:
                    {
                        query = "SELECT MAD.DONAUD_ID,\n" +
                       "       MAD.NAME,\n" +
                       "       MAD.DOB,\n" +
                       "       MC.COUNTRY,\n" +
                            // "       MS.STATE_NAME AS 'STATE',\n" +
                       "     MS.STATE_NAME,\n" +
                       "     CONCAT(IFNULL(MS.STATE_NAME,' '), CONCAT(' ', MAD.PINCODE)) AS 'STATE',\n" +
                       "      MAD.RELIGION,\n" +
                       "       MAD.PLACE,\n" +
                       "       MAD.EMAIL,\n" +
                       "       MAD.PHONE AS 'MOBILE NO',\n" +
                       "       MAD.ADDRESS,\n" +
                       "       MAD.PINCODE,\n" +
                            // "       MAD.ANNIVERSARY_DATE,\n" +
                       "       STR_TO_DATE(MAD.ANNIVERSARY_DATE,'%Y-%m-%d') as ANNIVERSARY_DATE,\n" +
                       "       CASE WHEN MAD.GENDER=1 THEN 'Male' ELSE 'Female' END AS GENDER,\n" +
                       "       MAD.CURRENT_BIRTHDAY_DATE_EMAIL,\n" +
                       "       MAD.CURRENT_MARRIAGE_DATE_EMAIL,\n" +
                       "       MAD.CURRENT_BIRTHDAY_DATE_SMS,\n" +
                       "       MAD.CURRENT_MARRIAGE_DATE_SMS,\n" +
                       "       MAD.MARITAL_STATUS,\n" +
                          "  CASE WHEN ?COMMUNICATION_MODE=1 THEN\n" +
                         "  IF(CURRENT_BIRTHDAY_DATE_SMS  BETWEEN ?DATE_STARTED AND\n" +
                      " ?DATE_CLOSED, 'Sent','Not Sent')\n" +
                       "ELSE IF(CURRENT_MARRIAGE_DATE_SMS BETWEEN ?DATE_STARTED  AND \n" +
                       " ?DATE_CLOSED,'Sent','Not Sent') END AS STATUS\n" +
                       "  FROM MASTER_DONAUD AS MAD\n" +
                       " LEFT JOIN MASTER_DONOR_LETTER_TAGS MDLT\n" +
                       "    ON MAD.DONAUD_ID = MDLT.REF_ID\n" +
                       " LEFT JOIN MASTER_DONOR_TAGS MDT\n" +
                       "    ON MDT.TAG_ID = MDLT.TAG_ID\n" +
                       "  LEFT JOIN MASTER_COUNTRY AS MC\n" +
                       "    ON MC.COUNTRY_ID = MAD.COUNTRY_ID\n" +
                       "  LEFT JOIN MASTER_STATE AS MS\n" +
                       "    ON MS.STATE_ID = MAD.STATE_ID\n" +
                       "   JOIN (SELECT DONAUD_ID," +
                       "  STR_TO_DATE(concat(month(dob),'/',day(dob),'/',if( month(dob)  >= month(?DATE_STARTED),year(?DATE_STARTED), year(?DATE_CLOSED))) ,'%c/%e/%Y') AS DOB_DATE," +
                       "         STR_TO_DATE(concat(month(ANNIVERSARY_DATE),'/',day(ANNIVERSARY_DATE),'/',if( month(ANNIVERSARY_DATE)  >= month(?DATE_STARTED),year(?DATE_STARTED),year(?DATE_CLOSED))),'%c/%e/%Y') AS ANV_DATE" +
                       "   FROM master_donaud) AS T " +
                       "   ON T.DONAUD_ID=MAD.DONAUD_ID" +
                       " WHERE IF(?MARITAL_STATUS_IDs =0,\n" +
                       "      DOB_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED,\n" +
                       "      ANV_DATE BETWEEN ?DATE_STARTED AND ?DATE_CLOSED)\n" +
                       " GROUP BY DONAUD_ID";
                        break;
                    }
                case SQLCommand.FrontOffice.UpdateAnniversarySMSStatus:
                    {
                        query = "UPDATE MASTER_DONAUD\n" +
                                "   SET { CURRENT_BIRTHDAY_DATE_SMS =?CURRENT_BIRTHDAY_DATE_SMS,BIRTHDAY_WISHING_DATE_SMS = NOW()} {CURRENT_MARRIAGE_DATE_SMS =?CURRENT_MARRIAGE_DATE_SMS,MARRIAGE_WISHING_DATE_SMS = NOW()}\n" +
                                " WHERE DONAUD_ID =?DONAUD_ID";
                        break;
                    }
                case SQLCommand.FrontOffice.FetchDonorDetails:
                    {
                        query = "SELECT " +
                            // "DONAUD_ID, " +
                                "NAME, " +
                                "PLACE," +
                            // "D.COUNTRY_ID," +
                                "COUNTRY," +
                                "PHONE," +
                                "STATE_NAME," +
                                "ADDRESS," +
                                "PINCODE," +
                                "FAX," +
                                "EMAIL," +
                                "URL " +
                            "FROM " +
                                "MASTER_DONAUD D " +
                                "LEFT JOIN MASTER_COUNTRY C ON D.COUNTRY_ID=C.COUNTRY_ID " +
                                "LEFT JOIN MASTER_STATE MS ON D.STATE_ID=MS.STATE_ID " +
                                " WHERE IDENTITYKEY=0;";
                        //" ORDER BY NAME ASC";

                        break;
                    }
                case SQLCommand.FrontOffice.UpdateThanksgivingSMSStatus:
                    {
                        query = "UPDATE VOUCHER_MASTER_TRANS SET DONOR_SMS_STATUS =1 WHERE VOUCHER_ID =?VOUCHER_ID";
                        break;
                    }
                case SQLCommand.FrontOffice.FetchLetterTypes:
                    {
                        query = "SELECT LETTER_TYPE_ID,LETTER_NAME FROM MASTER_DONOR_LETTERS";
                        break;
                    }
                case SQLCommand.FrontOffice.FetchLetterTypesForSMS:
                    {
                        query = "SELECT LETTER_TYPE_ID,LETTER_NAME FROM MASTER_DONOR_LETTERS WHERE LETTER_TYPE_ID<>3;";
                        break;
                    }
                case SQLCommand.FrontOffice.FetchLetterTypeContent:
                    {
                        query = "SELECT TEMPLATE_ID,LETTER_TYPE_ID,NAME,CONTENT FROM MASTER_DONOR_TEMPLATES WHERE LETTER_TYPE_ID=?LETTER_TYPE_ID AND COMMUNICATION_MODE=?COMMUNICATION_MODE;";
                        break;
                    }
                case SQLCommand.FrontOffice.FetchLetterTypeIdByName:
                    {
                        query = "SELECT LETTER_TYPE_ID FROM MASTER_DONOR_LETTERS WHERE LETTER_NAME=?NAME;";
                        break;
                    }
                case SQLCommand.FrontOffice.FetchContentById:
                    {
                        query = "SELECT TEMPLATE_ID,LETTER_TYPE_ID,CONTENT FROM MASTER_DONOR_TEMPLATES WHERE TEMPLATE_ID=?ID AND COMMUNICATION_MODE=?COMMUNICATION_MODE;";
                        break;
                    }
                case SQLCommand.FrontOffice.FetchContentByName:
                    {
                        query = "SELECT TEMPLATE_ID,LETTER_TYPE_ID,CONTENT FROM MASTER_DONOR_TEMPLATES WHERE NAME=?NAME AND COMMUNICATION_MODE=?COMMUNICATION_MODE;";
                        break;
                    }
                case SQLCommand.FrontOffice.FetchDonorMappedProjects:
                    {
                        query = "SELECT MP.PROJECT_ID, MP.PROJECT\n" +
                        "  FROM MASTER_PROJECT MP\n" +
                        "  INNER JOIN PROJECT_DONOR PD\n" +
                        "    ON MP.PROJECT_ID = PD.PROJECT_ID\n" +
                        " GROUP BY MP.PROJECT_ID;";

                        break;
                    }
            }
            return query;
        }
    }
}
