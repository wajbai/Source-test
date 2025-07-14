/*  Class Name      : ExecutiveMemberSQL
 *  Purpose         : To have Manipulation query for Executive Member
 *  Author          : Chinna
 *  Created on      : 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;
namespace Bosco.SQL
{
    public class ExecutiveMemberSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.ExecutiveMembers).FullName)
            {
                query = GetExecuteMember();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQLScript
        /// <summary>
        /// Perform the action of Executive Member details
        /// </summary>
        /// <returns></returns>
        private string GetExecuteMember()
        {
            string query = "";
            SQLCommand.ExecutiveMembers sqlCommandId = (SQLCommand.ExecutiveMembers)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {
                case SQLCommand.ExecutiveMembers.Add:
                    {
                        query = "INSERT INTO MASTER_EXECUTIVE_COMMITTEE ( " +
                             "EXECUTIVE, " +
                             "NAME, " +
                             "DATE_OF_BIRTH, " +
                             "RELIGION, " +
                             "ROLE, " +
                             "NATIONALITY, " +
                             "OCCUPATION, " +
                             "ASSOCIATION, " +
                             "OFFICE_BEARER, " +
                             "PLACE, " +
                             "STATE_ID, " +
                             "COUNTRY_ID, " +
                             "ADDRESS, " +
                             "PIN_CODE, " +
                             "PAN_SSN, " +
                             "AADHAR_NO, " +
                             "PHONE, " +
                             "FAX, " +
                             "EMAIL, " +
                             "URL, " +
                             "DATE_OF_APPOINTMENT, " +
                             "DATE_OF_EXIT, " +
                            // "IMAGES, " +
                             "NOTES,CUSTOMERID) VALUES( " +
                             "?EXECUTIVE, " +
                             "?NAME, " +
                             "?DATE_OF_BIRTH, " +
                             "?RELIGION, " +
                             "?ROLE, " +
                             "?NATIONALITY, " +
                             "?OCCUPATION, " +
                             "?ASSOCIATION, " +
                             "?OFFICE_BEARER, " +
                             "?PLACE, " +
                             "?STATE_ID, " +
                             "?COUNTRY_ID, " +
                             "?ADDRESS, " +
                             "?PIN_CODE, " +
                             "?PAN_SSN, " +
                             "?AADHAR_NO, " +
                             "?PHONE, " +
                             "?FAX, " +
                             "?EMAIL, " +
                             "?URL, " +
                             "?DATE_OF_APPOINTMENT, " +
                             "?DATE_OF_EXIT, " +
                            //  "?IMAGES, " +
                             "?NOTES,?CUSTOMERID) ";
                        break;
                    }
                case SQLCommand.ExecutiveMembers.Update:
                    {
                        query = "UPDATE MASTER_EXECUTIVE_COMMITTEE SET " +
                                "EXECUTIVE =?EXECUTIVE, " +
                                "NAME =?NAME, " +
                                "DATE_OF_BIRTH =?DATE_OF_BIRTH, " +
                                "RELIGION =?RELIGION, " +
                                "ROLE =?ROLE, " +
                                "NATIONALITY =?NATIONALITY, " +
                                "OCCUPATION =?OCCUPATION, " +
                                "ASSOCIATION =?ASSOCIATION, " +
                                "OFFICE_BEARER =?OFFICE_BEARER, " +
                                "PLACE =?PLACE, " +
                                "STATE_ID =?STATE_ID, " +
                                "COUNTRY_ID =?COUNTRY_ID, " +
                                "ADDRESS =?ADDRESS, " +
                                "PIN_CODE =?PIN_CODE, " +
                                "PAN_SSN =?PAN_SSN, " +
                                "AADHAR_NO =?AADHAR_NO, " +
                                "PHONE =?PHONE, " +
                                "FAX =?FAX, " +
                                "EMAIL =?EMAIL, " +
                                "URL =?URL, " +
                                "DATE_OF_APPOINTMENT =?DATE_OF_APPOINTMENT, " +
                                "DATE_OF_EXIT =?DATE_OF_EXIT, " +
                            //  "IMAGES =?IMAGES, " +
                                "NOTES =?NOTES, " +
                                "CUSTOMERID=?CUSTOMERID " +
                                "WHERE EXECUTIVE_ID=?EXECUTIVE_ID ";
                        break;
                    }
                case SQLCommand.ExecutiveMembers.Delete:
                    {
                        query = "DELETE FROM MASTER_EXECUTIVE_COMMITTEE WHERE EXECUTIVE_ID=?EXECUTIVE_ID ";
                        break;
                    }
                case SQLCommand.ExecutiveMembers.DeleteAll:
                    {
                        query = "DELETE FROM MASTER_EXECUTIVE_COMMITTEE";
                        break;
                    }

                case SQLCommand.ExecutiveMembers.Fetch:
                    {
                        query = "SELECT " +
                            "MEC.EXECUTIVE_ID, " +
                            "MEC.EXECUTIVE, " +
                            "MEC.NAME, " +
                            "DATE_FORMAT(MEC.DATE_OF_APPOINTMENT,'%Y-%m-%d') AS DATE_OF_APPOINTMENT , " +
                            "DATE_FORMAT(MEC.DATE_OF_EXIT,'%Y-%m-%d') AS DATE_OF_EXIT, " +
                            "DATE_FORMAT(MEC.DATE_OF_BIRTH,'%Y-%m-%d') AS DATE_OF_BIRTH, " +
                            "MEC.RELIGION, " +
                            "MEC.ROLE, " +
                            "MEC.NATIONALITY, " +
                            "MEC.OCCUPATION, " +
                            "MEC.ASSOCIATION, " +
                            "MEC.OFFICE_BEARER, " +
                            "MEC.PLACE, " +
                            "MEC.STATE_ID, " +
                            "MEC.COUNTRY_ID, " +
                            "MEC.ADDRESS, " +
                            "MEC.PIN_CODE, " +
                            "MEC.PAN_SSN, " +
                            "MEC.AADHAR_NO, " +
                            "MEC.PHONE, " +
                            "MEC.FAX, " +
                            "MEC.EMAIL, " +
                            "MEC.URL, " +
                            //  "MEC.IMAGES, " +
                            "MEC.NOTES,MIP.CUSTOMERID " +
                            "FROM " +
                            "MASTER_EXECUTIVE_COMMITTEE MEC " +
                            " LEFT JOIN MASTER_INSTI_PERFERENCE MIP ON  " +
                                " MIP.CUSTOMERID=MEC.CUSTOMERID  " +
                            " WHERE EXECUTIVE_ID=?EXECUTIVE_ID";
                        break;
                    }
                case SQLCommand.ExecutiveMembers.FetchAll:
                    {
                        query = "SELECT " +
                            "EXECUTIVE_ID, " +
                            "EXECUTIVE, " +
                            "NAME, " +
                            "DATE_OF_BIRTH, " +
                            "RELIGION, " +
                            "ROLE, " +
                            "NATIONALITY, " +
                            "OCCUPATION, " +
                            "ASSOCIATION, " +
                            "OFFICE_BEARER, " +
                            "PLACE, " +
                            "STATE_NAME, " +
                            "COUNTRY, " +
                            "ADDRESS, " +
                            "PIN_CODE, " +
                            "PAN_SSN, " +
                            "AADHAR_NO, " +
                            "PHONE, " +
                            "FAX, " +
                            "EMAIL, " +
                            "URL, " +
                            "CUSTOMERID, " +
                            "DATE_OF_APPOINTMENT, " +
                            "DATE_OF_EXIT, " +
                            //  "IMAGES, " +
                            "NOTES " +
                            "FROM " +
                            "MASTER_EXECUTIVE_COMMITTEE E " +
                            "INNER JOIN MASTER_COUNTRY C ON E.COUNTRY_ID=C.COUNTRY_ID " +
                            "INNER JOIN MASTER_STATE MS ON MS.STATE_ID=E.STATE_ID WHERE (DATE_OF_APPOINTMENT BETWEEN ?YEAR_FROM AND ?YEAR_TO OR DATE_OF_EXIT BETWEEN ?YEAR_FROM AND ?YEAR_TO) { AND CUSTOMERID =?CUSTOMERID } " +
                            " ORDER BY EXECUTIVE ASC";
                        break;
                    }
            }
            return query;
        }
        #endregion
    }
}
