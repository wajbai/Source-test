using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;


namespace Bosco.SQL
{
    public class SettingSQL :IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.Setting).FullName)
            {
                query = GetSettingSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the bank details.
        /// </summary>
        /// <returns></returns>
        private string GetSettingSQL()
        {
            string query = "";
            SQLCommand.Setting sqlCommandId = (SQLCommand.Setting)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.Setting.InsertUpdate:
                    {
                        query = "INSERT INTO MASTER_SETTING ( " +
                               "SETTING_NAME, " +
                               "VALUE ) VALUES( " +
                               "?SETTING_NAME, " +
                               "?VALUE) ON DUPLICATE KEY UPDATE VALUE=?VALUE";
                        break;
                    }
                case SQLCommand.Setting.InsertUpdateReportSetting:
                    {
                        query = "INSERT INTO MASTER_REPORT_SETTING ( " +
                               "REPORT_ID, " +
                               "SETTING_NAME, " +
                               "VALUE ) VALUES( " +
                               "?REPORT_ID, " +
                               "?SETTING_NAME, " +
                               "?VALUE) ON DUPLICATE KEY UPDATE VALUE=?VALUE";
                        break;
                    }
                case SQLCommand.Setting.FetchReportSetting:
                    {
                        query = "SELECT * FROM MASTER_REPORT_SETTING WHERE REPORT_ID=?REPORT_ID";
                        break;
                    }
                case SQLCommand.Setting.Update:
                    {
                        query = "UPDATE MASTER_SETTING SET " +
                                    "SETTING_NAME = ?SETTING_NAME, " +
                                    "VALUE=?VALUE " +
                                    " WHERE SETTING_NAME=?SETTING_NAME ";
                        break;
                    }
                case SQLCommand.Setting.Fetch:
                    {
                        query = "SELECT " +
                                "SETTING_NAME AS Name, " +
                                "VALUE AS Value " +
                            "FROM" +
                                " MASTER_SETTING ORDER BY SETTING_NAME ASC";
                        break;
                    }
                case SQLCommand.Setting.FetchCurrentDate:
                    {
                        query = "SELECT NOW();";
                        break;
                    }
                case SQLCommand.Setting.DeleteChequePrintingSetting:
                    {
                        query = "DELETE FROM MASTER_SETTING_CHEQUE_PRINTING WHERE BANK_ID =?BANK_ID AND BANK_ID >0;";
                        break;
                    }
                case SQLCommand.Setting.FetchChequePrintingSetting:
                    {
                        query = "SELECT BANK_ID, SETTING_NAME, SETTING_VALUE FROM MASTER_SETTING_CHEQUE_PRINTING WHERE BANK_ID=?BANK_ID;";
                        break;
                    }
                case SQLCommand.Setting.InsertUpdateChequePrintingSetting:
                    {
                        query = "INSERT INTO MASTER_SETTING_CHEQUE_PRINTING (BANK_ID, SETTING_NAME, SETTING_VALUE ) " +
                                        "VALUES (?BANK_ID, ?SETTING_NAME, ?SETTING_VALUE)" +
                                        "ON DUPLICATE KEY UPDATE BANK_ID = ?BANK_ID, SETTING_NAME = ?SETTING_NAME, SETTING_VALUE=?SETTING_VALUE";
                        //query = "UPDATE MASTER_SETTING_CHEQUE_PRINTING SET SETTING_VALUE = ?SETTING_VALUE " + 
                        //            "WHERE BANK_ID = ?BANK_IDAND SETTING_NAME = ?SETTING_NAME";
                        break;
                    }
                case SQLCommand.Setting.InsertACSignDetails:
                    {
                        query = "INSERT INTO MASTER_REPORT_SIGN_DETAIL (ACC_YEAR_ID, PROJECT_ID, ROLE_NAME, ROLE, SIGN_IMAGE, HIDE_REQUIRE_SIGN_NOTE, SIGN_NOTE,\n"+
                                "  SIGN_NOTE_ALIGNMENT, SIGN_NOTE_LOCATION, SIGN_ORDER)\n" +
                                "SELECT ?ACC_YEAR_ID, PROJECT_ID, ROLE_NAME, ROLE, SIGN_IMAGE, HIDE_REQUIRE_SIGN_NOTE, SIGN_NOTE,\n"+  
                                "  SIGN_NOTE_ALIGNMENT, SIGN_NOTE_LOCATION, SIGN_ORDER\n" +
                                "FROM MASTER_REPORT_SIGN_DETAIL\n" +
                                "WHERE ACC_YEAR_ID = (SELECT ACC_YEAR_ID FROM ACCOUNTING_YEAR WHERE ACC_YEAR_ID<>?ACC_YEAR_ID ORDER BY YEAR_FROM DESC Limit 1);";
                        break;
                    }
                case SQLCommand.Setting.InsertACAuditorNoteSignDetails:
                    {
                        query = "INSERT INTO MASTER_AUDITOR_SIGN_NOTE (ACC_YEAR_ID, AUDITOR_NOTE_SETTING, AUDITOR_NOTE_SETTING_VALUE)\n" +
                                "SELECT ?ACC_YEAR_ID, AUDITOR_NOTE_SETTING, AUDITOR_NOTE_SETTING_VALUE\n" +
                                "FROM MASTER_AUDITOR_SIGN_NOTE\n" +
                                "WHERE ACC_YEAR_ID = (SELECT ACC_YEAR_ID FROM ACCOUNTING_YEAR WHERE ACC_YEAR_ID<>?ACC_YEAR_ID ORDER BY YEAR_FROM DESC Limit 1);";
                        break;
                    }
                case SQLCommand.Setting.DeleteACSignDetails:
                    {
                        query = "DELETE FROM MASTER_REPORT_SIGN_DETAIL WHERE ACC_YEAR_ID =?ACC_YEAR_ID";
                        break;
                    }
                case SQLCommand.Setting.DeleteSign:
                    {
                        query = "DELETE FROM MASTER_REPORT_SIGN_DETAIL WHERE ACC_YEAR_ID =?ACC_YEAR_ID AND PROJECT_ID = ?PROJECT_ID AND SIGN_ORDER=?SIGN_ORDER";
                        break;
                    }
                case SQLCommand.Setting.InsertUpdateSignDetail:
                    {
                        query = "DELETE FROM MASTER_REPORT_SIGN_DETAIL WHERE ACC_YEAR_ID =?ACC_YEAR_ID AND PROJECT_ID = ?PROJECT_ID AND SIGN_ORDER=?SIGN_ORDER;\n" +
                                "INSERT INTO MASTER_REPORT_SIGN_DETAIL (ACC_YEAR_ID, PROJECT_ID, ROLE_NAME, ROLE,\n" +
                                "HIDE_REQUIRE_SIGN_NOTE, SIGN_NOTE, SIGN_NOTE_ALIGNMENT, SIGN_NOTE_LOCATION, SIGN_ORDER)\n" +
                                "VALUES(?ACC_YEAR_ID, ?PROJECT_ID, ?ROLE_NAME, ?ROLE, ?HIDE_REQUIRE_SIGN_NOTE, ?SIGN_NOTE, ?SIGN_NOTE_ALIGNMENT, ?SIGN_NOTE_LOCATION, ?SIGN_ORDER)";
                                //"ON DUPLICATE KEY UPDATE ACC_YEAR_ID=?ACC_YEAR_ID AND PROJECT_ID = ?PROJECT_ID AND ROLE_NAME=?ROLE_NAME, ROLE=?ROLE";
                        break;
                    }
                case SQLCommand.Setting.UpdateSignDetails:
                    {
                        query = @"UPDATE MASTER_REPORT_SIGN_DETAIL SET SIGN_IMAGE=?SIGN_IMAGE, HIDE_REQUIRE_SIGN_NOTE = ?HIDE_REQUIRE_SIGN_NOTE, 
                                    SIGN_NOTE=?SIGN_NOTE, SIGN_NOTE_ALIGNMENT=?SIGN_NOTE_ALIGNMENT, SIGN_NOTE_LOCATION=?SIGN_NOTE_LOCATION
                                    WHERE ACC_YEAR_ID=?ACC_YEAR_ID AND PROJECT_ID = ?PROJECT_ID AND SIGN_ORDER=?SIGN_ORDER";
                        break;
                    }
                case SQLCommand.Setting.UpdateSignDetailsForAllProjects:
                    {
                        query = @"DELETE FROM MASTER_REPORT_SIGN_DETAIL WHERE ACC_YEAR_ID = ?ACC_YEAR_ID AND PROJECT_ID > 0 AND SIGN_ORDER=?SIGN_ORDER; 
                                INSERT INTO MASTER_REPORT_SIGN_DETAIL (ACC_YEAR_ID, PROJECT_ID, ROLE_NAME, ROLE,
                                HIDE_REQUIRE_SIGN_NOTE, SIGN_NOTE, SIGN_NOTE_ALIGNMENT, SIGN_NOTE_LOCATION, SIGN_ORDER)
                                SELECT ?ACC_YEAR_ID, PROJECT_ID, ?ROLE_NAME, ?ROLE, ?HIDE_REQUIRE_SIGN_NOTE, ?SIGN_NOTE, ?SIGN_NOTE_ALIGNMENT, ?SIGN_NOTE_LOCATION, ?SIGN_ORDER
                                FROM MASTER_REPORT_SIGN_DETAIL WHERE ACC_YEAR_ID = ?ACC_YEAR_ID AND PROJECT_ID>0 GROUP BY PROJECT_ID;";
                        break;
                    }
                
                case SQLCommand.Setting.InsertAuditorNoteSign:
                    {
                        query = @"INSERT INTO MASTER_AUDITOR_SIGN_NOTE (AUDITOR_NOTE_SETTING, AUDITOR_NOTE_SETTING_VALUE, ACC_YEAR_ID)
                                    VALUES (?AUDITOR_NOTE_SETTING, ?VALUE, ?ACC_YEAR_ID) 
                                    ON DUPLICATE KEY UPDATE 
                                    AUDITOR_NOTE_SETTING = ?AUDITOR_NOTE_SETTING, AUDITOR_NOTE_SETTING_VALUE = ?VALUE, ACC_YEAR_ID = ?ACC_YEAR_ID";
                        break;
                    }
                case SQLCommand.Setting.DeleteAuditorNoteSign:
                    {
                        query = @"DELETE FROM MASTER_AUDITOR_SIGN_NOTE WHERE ACC_YEAR_ID = ?ACC_YEAR_ID";
                        break;
                    }
                case SQLCommand.Setting.FetchAuditorNoteSign:
                    {
                        query = @"SELECT IFNULL(AUDITOR_NOTE_SETTING, '') AS AUDITOR_NOTE_SETTING, IFNULL(AUDITOR_NOTE_SETTING_VALUE,'') AS AUDITOR_NOTE_SETTING_VALUE 
                                    FROM MASTER_AUDITOR_SIGN_NOTE WHERE ACC_YEAR_ID = ?ACC_YEAR_ID";
                        break;
                    }

//                case SQLCommand.Setting.SaveAuditorSignNote:
//                    {
//                        query = @"DELETE FROM MASTER_REPORT_AUDITOR_SIGN_NOTE WHERE ACC_YEAR_ID = ?ACC_YEAR_ID;
//                                  INSERT INTO MASTER_REPORT_AUDITOR_SIGN_NOTE (ACC_YEAR_ID, AUDITOR_SIGN_NOTE) 
//                                  VALUES(?ACC_YEAR_ID, COMPRESS(?AUDITOR_SIGN_NOTE))";
//                        break;
//                    }
//                case SQLCommand.Setting.FetchAuditorSignNote:
//                    {
//                        query = @"SELECT ACC_YEAR_ID, CONVERT(UNCOMPRESS(IFNULL(AUDITOR_SIGN_NOTE, '')) USING 'utf8') AS AUDITOR_SIGN_NOTE
//                                    FROM MASTER_REPORT_AUDITOR_SIGN_NOTE WHERE ACC_YEAR_ID = ?ACC_YEAR_ID";
//                        break;
//                    }
                case SQLCommand.Setting.FetchSignDetails:
                    {
                        query = "SELECT ACC_YEAR_ID, PROJECT_ID, ROLE_NAME, ROLE, SIGN_IMAGE, HIDE_REQUIRE_SIGN_NOTE, IFNULL(SIGN_NOTE,'') AS SIGN_NOTE,\n" +
                                    "IFNULL(SIGN_NOTE_LOCATION,0) AS SIGN_NOTE_LOCATION, IFNULL(SIGN_NOTE_ALIGNMENT, 1) AS SIGN_NOTE_ALIGNMENT, SIGN_ORDER\n"+ 
                                    "FROM MASTER_REPORT_SIGN_DETAIL WHERE ACC_YEAR_ID=?ACC_YEAR_ID;";
                        break;
                    }
                case SQLCommand.Setting.FetchBudgetNewProjects:
                    {
                        query = "SELECT AC.ACC_YEAR_ID, MRBN.BUDGET_ID, SEQUENCE_NO, MRBN.INCLUDE_REPORTS, MRBN.NEW_PROJECT,\n" +
                                    "MRBN.PROPOSED_EXPENSE_AMOUNT,\n" +
                                    "MRBN.PROPOSED_INCOME_AMOUNT, MRBN.GN_HELP_PROPOSED_AMOUNT, MRBN.HO_HELP_PROPOSED_AMOUNT, MRBN.REMARKS\n" +
                                    "FROM MASTER_REPORT_BUDGET_NEW_PROJECTS MRBN\n" +
                                    "LEFT JOIN ACCOUNTING_YEAR AC ON AC.ACC_YEAR_ID = MRBN.ACC_YEAR_ID\n"+
                                    "WHERE MRBN.ACC_YEAR_ID=?ACC_YEAR_ID AND MRBN.BUDGET_ID IN (?DEVELOPMENTAL_NEW_BUDGETID) ORDER BY SEQUENCE_NO";
                        break;
                    }
                case SQLCommand.Setting.FetchBudgetNewProjectsCCDetailsByAcYear:
                    {
                        query = "SELECT AC.ACC_YEAR_ID, MRBN.BUDGET_ID, DEVELOPMENTAL_PROJECT_SEQUENCE_NO AS LEDGER_ID, MRBND.COST_CENTRE_ID, MRBN.NEW_PROJECT AS LEDGER_NAME,\n" +
                                "MC.COST_CENTRE_NAME, IFNULL(MRBND.PROPOSED_EXPENSE_AMOUNT,0) AS PROPOSED_AMOUNT, '' TRANS_MODE\n" +
                                "FROM MASTER_REPORT_BUDGET_NEW_PROJECTS MRBN\n" + 
                                "INNER JOIN MASTER_REPORT_BUDGET_NEW_PROJECTS_DETAILS MRBND ON MRBND.ACC_YEAR_ID = MRBN.ACC_YEAR_ID AND MRBND.BUDGET_ID = MRBN.BUDGET_ID\n" +
                                " AND MRBND.DEVELOPMENTAL_PROJECT_SEQUENCE_NO = MRBN.SEQUENCE_NO\n" + 
                                "INNER JOIN ACCOUNTING_YEAR AC ON AC.ACC_YEAR_ID = MRBN.ACC_YEAR_ID\n"+ 
                                "INNER JOIN MASTER_COST_CENTRE MC ON MC.COST_CENTRE_ID = MRBND.COST_CENTRE_ID\n" +
                                "WHERE MRBN.ACC_YEAR_ID=?ACC_YEAR_ID {AND MRBN.BUDGET_ID IN (?DEVELOPMENTAL_NEW_BUDGETID)}";
                        break;
                    }
                case SQLCommand.Setting.FetchBudgetStrengthDetails:
                    {
                        query = "SELECT BSD.BUDGET_ID, BSD.COST_CENTRE_ID, BSD.NEW_COUNT, BSD.PRESENT_COUNT\n" +
                                "FROM BUDGET_STRENGTH_DETAIL BSD\n" +
                                "WHERE BSD.BUDGET_ID IN (?BUDGET_ID)";
                        break;
                    }
               
                case SQLCommand.Setting.DeleteBudgetStrengthDetails:
                    {
                        query = "DELETE FROM BUDGET_STRENGTH_DETAIL WHERE BUDGET_ID IN (?BUDGET_ID)";
                        break;
                    }
                case SQLCommand.Setting.DeleteBudgetNewProjectsByAcYear:
                    {
                        query = "DELETE FROM MASTER_REPORT_BUDGET_NEW_PROJECTS WHERE ACC_YEAR_ID=?ACC_YEAR_ID AND BUDGET_ID IN (?DEVELOPMENTAL_NEW_BUDGETID)";
                        break;
                    }
                case SQLCommand.Setting.DeleteBudgetNewProjectsCCDetailsByAcYear:
                    {
                        query = "DELETE FROM MASTER_REPORT_BUDGET_NEW_PROJECTS_DETAILS WHERE ACC_YEAR_ID=?ACC_YEAR_ID AND BUDGET_ID IN (?DEVELOPMENTAL_NEW_BUDGETID)";
                        break;
                    }
               
                case SQLCommand.Setting.UpdateBudgetStrengthDetails:
                    {
                        query = "INSERT INTO BUDGET_STRENGTH_DETAIL (BUDGET_ID, COST_CENTRE_ID, NEW_COUNT, PRESENT_COUNT)\n" +
                                  "VALUES(?BUDGET_ID, ?COST_CENTRE_ID, ?NEW_COUNT, ?PRESENT_COUNT)";
                        break;
                    }
                case SQLCommand.Setting.UpdateBudgetNewProjectsByAcYear:
                    {
                        query = "INSERT INTO MASTER_REPORT_BUDGET_NEW_PROJECTS\n" +
                                  "(ACC_YEAR_ID, BUDGET_ID, SEQUENCE_NO, NEW_PROJECT, PROPOSED_INCOME_AMOUNT, PROPOSED_EXPENSE_AMOUNT, GN_HELP_PROPOSED_AMOUNT,\n" +
                                        "HO_HELP_PROPOSED_AMOUNT, INCLUDE_REPORTS, REMARKS)\n" +
                                  "VALUES(?ACC_YEAR_ID, ?BUDGET_ID, ?SEQUENCE_NO, ?NEW_PROJECT, ?PROPOSED_INCOME_AMOUNT, ?PROPOSED_EXPENSE_AMOUNT, ?GN_HELP_PROPOSED_AMOUNT,\n" +
                                        "?HO_HELP_PROPOSED_AMOUNT, ?INCLUDE_REPORTS, ?REMARKS)";
                        break;
                    }
                case SQLCommand.Setting.UpdateBudgetNewProjectsCCDetailsByAcYear:
                    {
                        query = "INSERT INTO MASTER_REPORT_BUDGET_NEW_PROJECTS_DETAILS\n" +
                                  "(ACC_YEAR_ID, BUDGET_ID, DEVELOPMENTAL_PROJECT_SEQUENCE_NO, COST_CENTRE_ID, PROPOSED_EXPENSE_AMOUNT)\n" +
                                  "VALUES(?ACC_YEAR_ID, ?BUDGET_ID, ?DEVELOPMENTAL_SEQUENCE_NO, ?COST_CENTRE_ID, ?PROPOSED_EXPENSE_AMOUNT)";
                        break;
                    }
               
                case SQLCommand.Setting.ExistsBudgetNewProjectsByAcYear:
                    {
                        query = "SELECT NEW_PROJECT\n" +
                                "FROM MASTER_REPORT_BUDGET_NEW_PROJECTS MRBN\n" +
                                "WHERE MRBN.ACC_YEAR_ID<>?ACC_YEAR_ID AND NEW_PROJECT=?NEW_PROJECT AND BUDGET_ID <> ?BUDGET_ID AND SEQUENCE_NO<>?SEQUENCE_NO";
                        break;
                    }
                case SQLCommand.Setting.FetchMultiDBXMLConfigurationInAcperp:
                    {
                        query = "SELECT MULTI_DB_XML FROM ACPERP.MULTI_DB_XML_CONFIGURATION";
                        break;
                    }
                case SQLCommand.Setting.InsertMultiDBXMLConfigurationInAcperp:
                    {
                        query = @"INSERT INTO ACPERP.MULTI_DB_XML_CONFIGURATION (MULTI_DB_XML) VALUES(?MULTI_DB_XML)";
                        break;
                    }

                case SQLCommand.Setting.UpdateMultiDBXMLConfigurationInAcperp:
                    {
                        //DELETE FROM ACPERP.MULTI_DB_XML_CONFIGURATION;
                        query = @"UPDATE ACPERP.MULTI_DB_XML_CONFIGURATION SET MULTI_DB_XML=?MULTI_DB_XML";
                        break;
                    }
            }

            return query;
        }
        #endregion Bank SQL
    }
}
