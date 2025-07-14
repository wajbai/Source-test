using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;


namespace AcMEDSync.SQL
{
    public class ImportMasterSQL
    {
        #region Head Office
        public string GetQuery(AcMEDSync.SQL.EnumDataSyncSQLCommand.ImportSQL importSQL)
        {
            string sQuery = string.Empty;
            switch (importSQL)
            {

                case EnumDataSyncSQLCommand.ImportSQL.IsLegalEntityExists:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                            "  FROM MASTER_INSTI_PERFERENCE\n" +
                                            " WHERE INSTITUTENAME =?INSTITUTENAME AND SOCIETYNAME=?SOCIETYNAME";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportSQL.AddLegalEntity:
                    {
                        sQuery = "INSERT INTO MASTER_INSTI_PERFERENCE\n" +
                                            "  (INSTITUTENAME,\n" +
                                            "   SOCIETYNAME,\n" +
                                            "   CONTACTPERSON,\n" +
                                            "   ADDRESS,\n" +
                                            "   PLACE,\n" +
                                            "   STATE,\n" +
                                            "   COUNTRY,\n" +
                                            "   PINCODE,\n" +
                                            "   PHONE,\n" +
                                            "   FAX,\n" +
                                            "   EMAIL,\n" +
                                            "   URL,\n" +
                                            "   REGNO,\n" +
                                            "   REGDATE,\n" +
                                            "   PERMISSIONNO,\n" +
                                            "   PERMISSIONDATE,\n" +
                                            "   A12NO,\n" +
                                            "   PANNO,\n" +
                                            "   GIRNO,\n" +
                                            "   TANNO,\n" +
                                            "   ASSOCIATIONNATURE,\n" +
                                            "   DENOMINATION)\n" +
                                            "VALUES\n" +
                                            "  (?INSTITUTENAME,\n" +
                                            "   ?SOCIETYNAME,\n" +
                                            "   ?CONTACTPERSON,\n" +
                                            "   ?ADDRESS,\n" +
                                            "   ?PLACE,\n" +
                                            "   ?STATE,\n" +
                                            "   ?COUNTRY,\n" +
                                            "   ?PINCODE,\n" +
                                            "   ?PHONE,\n" +
                                            "   ?FAX,\n" +
                                            "   ?EMAIL,\n" +
                                            "   ?URL,\n" +
                                            "   ?REGNO,\n" +
                                            "   ?REGDATE,\n" +
                                            "   ?PERMISSIONNO,\n" +
                                            "   ?PERMISSIONDATE,\n" +
                                            "   ?A12NO,\n" +
                                            "   ?PANNO,\n" +
                                            "   ?GIRNO,\n" +
                                            "   ?TANNO,\n" +
                                            "   ?ASSOCIATIONNATURE,\n" +
                                            "   ?DENOMINATION)";

                        break;
                    }
                case EnumDataSyncSQLCommand.ImportSQL.IsProjectCatogoryExists:
                    {
                        sQuery = "SELECT COUNT(*)\n" +
                                            "  FROM MASTER_PROJECT_CATOGORY\n" +
                                            " WHERE PROJECT_CATOGORY_NAME = ?PROJECT_CATOGORY_NAME";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportSQL.AddProjectCatogory:
                    {
                        sQuery = "INSERT INTO MASTER_PROJECT_CATOGORY ( " +
                                            "PROJECT_CATOGORY_NAME) VALUES( " +
                                            "?PROJECT_CATOGORY_NAME)";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportSQL.IsProjectExists:
                    {
                        sQuery = "SELECT COUNT(*) FROM MASTER_PROJECT WHERE PROJECT_CODE=?PROJECT_CODE OR PROJECT = ?PROJECT";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportSQL.AddProject:
                    {
                        sQuery = "INSERT INTO MASTER_PROJECT ( " +
                               "PROJECT_CODE, " +
                               "PROJECT, " +
                               "DIVISION_ID, " +
                               "ACCOUNT_DATE," +
                               "DATE_STARTED," +
                               "DATE_CLOSED," +
                               "DESCRIPTION," +
                               "NOTES,CUSTOMERID, " +
                               "PROJECT_CATEGORY_ID ) VALUES( " +
                               "?PROJECT_CODE, " +
                               "?PROJECT, " +
                               "?DIVISION_ID, " +
                               "?ACCOUNT_DATE," +
                               "?DATE_STARTED," +
                               "?DATE_CLOSED," +
                               "?DESCRIPTION," +
                               "?NOTES, ?CUSTOMERID," +
                               "?PROJECT_CATEGORY_ID)";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportSQL.IsLedgerGroupExists:
                    {
                        sQuery = "SELECT COUNT(*) FROM MASTER_LEDGER_GROUP WHERE LEDGER_GROUP=?LEDGER_GROUP";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportSQL.IsMainGroupExist:
                    {
                        sQuery = "SELECT COUNT(*) FROM MASTER_LEDGER_GROUP WHERE MAIN_GROUP_ID=?MAIN_GROUP_ID";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportSQL.IsParentGroupExist:
                    {
                        sQuery = "SELECT COUNT(*) FROM MASTER_LEDGER_GROUP WHERE PARENT_GROUP_ID=?PARENT_GROUP_ID";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportSQL.IsNatureExist:
                    {
                        sQuery = "SELECT COUNT(*) FROM MASTER_LEDGER_GROUP WHERE NATURE_ID=?NATURE_ID";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportSQL.IsGroupCodeExist:
                    {
                        sQuery = "SELECT COUNT(*) FROM MASTER_LEDGER_GROUP WHERE GROUP_CODE=?GROUP_CODE";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportSQL.AddLedger:
                    {
                        sQuery = "INSERT INTO MASTER_LEDGER ( " +
                                    "LEDGER_CODE, " +
                                    "LEDGER_NAME, " +
                                    "GROUP_ID,  " +
                                    "LEDGER_TYPE, " +
                                    "LEDGER_SUB_TYPE, " +
                                    "BANK_ACCOUNT_ID, " +
                                    "IS_COST_CENTER,NOTES,SORT_ID,IS_BANK_INTEREST_LEDGER) " +
                                    "VALUES( " +
                                    "?LEDGER_CODE, " +
                                    "?LEDGER_NAME, " +
                                    "?GROUP_ID,  " +
                                    "?LEDGER_TYPE, " +
                                    "?LEDGER_SUB_TYPE, " +
                                    "?BANK_ACCOUNT_ID, " +
                                    "?IS_COST_CENTER,?NOTES,?SORT_ID,?IS_BANK_INTEREST_LEDGER) ";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportSQL.IsFCPurposeExists:
                    {
                        sQuery = "SELECT COUNT(*) FROM MASTER_CONTRIBUTION_HEAD WHERE CODE=?CODE AND FC_PURPOSE=?FC_PURPOSE";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportSQL.AddFCPurpose:
                    {
                        sQuery = "INSERT INTO MASTER_CONTRIBUTION_HEAD ( " +
                               "CODE, " +
                               "FC_PURPOSE ) VALUES( " +
                               "?CODE, " +
                               "?FC_PURPOSE) ";

                        break;
                    }
                case EnumDataSyncSQLCommand.ImportSQL.GetLegalEntityId:
                    {
                        sQuery = "SELECT CUSTOMERID FROM MASTER_INSTI_PERFERENCE WHERE  SOCIETYNAME=?SOCIETYNAME";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportSQL.GetProjectCategoryId:
                    {
                        sQuery = "SELECT PROJECT_CATOGORY_ID FROM MASTER_PROJECT_CATOGORY WHERE PROJECT_CATOGORY_NAME=?PROJECT_CATOGORY_NAME";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportSQL.GetProjectId:
                    {
                        sQuery = "SELECT PROJECT_ID FROM MASTER_PROJECT WHERE PROJECT=?PROJECT";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportSQL.GetLedgerGroupId:
                    {
                        sQuery = "SELECT GROUP_ID FROM MASTER_LEDGER_GROUP WHERE LEDGER_GROUP=?LEDGER_GROUP";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportSQL.GetParentId:
                    {
                        sQuery = "SELECT GROUP_ID FROM MASTER_LEDGER_GROUP WHERE LEDGER_GROUP=?LEDGER_GROUP";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportSQL.GetNatureId:
                    {
                        sQuery = "SELECT NATURE_ID FROM MASTER_NATURE WHERE NATURE=?NATURE";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportSQL.GetMainParentId:
                    {
                        sQuery = "SELECT GROUP_ID FROM MASTER_LEDGER_GROUP WHERE LEDGER_GROUP=?LEDGER_GROUP";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportSQL.GetLedgerId:
                    {
                        sQuery = "SELECT LEDGER_ID FROM MASTER_LEDGER WHERE LEDGER_NAME=?LEDGER_NAME";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportSQL.GetFCPurposeId:
                    {
                        sQuery = "SELECT CONTRIBUTION_ID FROM MASTER_CONTRIBUTION_HEAD WHERE FC_PURPOSE=?FC_PURPOSE";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportSQL.FetchLegalEntity:
                    {
                        sQuery = @"INSTITUTENAME, SOCIETYNAME, CONTACTPERSON, ADDRESS, PLACE, STATE, COUNTRY, PINCODE, PHONE, FAX, EMAIL, URL, REGNO, REGDATE, PERMISSIONNO,
                                   PERMISSIONDATE, A12NO, PANNO, GIRNO, TANNO, ASSOCIATIONNATURE, DENOMINATION FROM MASTER_INSTI_PERFERENCE";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportSQL.FetchProjectCategory:
                    {
                        sQuery = "SELECT PROJECT_CATOGORY_NAME FROM MASTER_PROJECT_CATOGORY";
                        break;
                    }

                case EnumDataSyncSQLCommand.ImportSQL.MapLedgers:
                    {
                        sQuery = "INSERT INTO PROJECT_LEDGER(PROJECT_ID,LEDGER_ID)VALUES(?PROJECT_ID,?LEDGER_ID)";
                        break;
                    }
                case EnumDataSyncSQLCommand.ImportSQL.MapVouchers:
                    {
                        sQuery = "INSERT INTO PROJECT_VOUCHER(PROJECT_ID,VOUCHER_ID)VALUES(?PROJECT_ID,?VOUCHER_ID)";
                        break;
                    }
            }
            return sQuery;
        }
        #endregion
    }
}
