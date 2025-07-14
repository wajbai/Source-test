using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class LegalEntitySQL : IDatabaseQuery
    {
        #region ISQLServerQuery Members

        DataCommandArguments dataCommandArgs;
        SQLType sqlType;
        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string Query = string.Empty;
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.LegalEntity).FullName)
            {
                Query = VoucherSQLManipulition();
            }

            sqlType = this.sqlType;
            return Query;
        }
        #endregion

        #region SQL Script
        private string VoucherSQLManipulition()
        {
            string Query = string.Empty;
            SQLCommand.LegalEntity sqlQueryType = (SQLCommand.LegalEntity)(this.dataCommandArgs.SQLCommandId);
            switch (sqlQueryType)
            {
                #region Insert Query
                case SQLCommand.LegalEntity.Add:
                    {
                        //                        Query = @"INSERT INTO MASTER_INSTI_PERFERENCE(INSTITUTENAME, SOCIETYNAME, CONTACTPERSON, ADDRESS, PLACE, STATE, COUNTRY_ID, PINCODE, PHONE, FAX, EMAIL,
                        //                                URL, REGNO, REGDATE, PERMISSIONNO, PERMISSIONDATE, A12NO, PANNO, GIRNO, TANNO, ASSOCIATIONNATURE, DENOMINATION)
                        //                            VALUES(?INSTITUTENAME, ?SOCIETYNAME, ?CONTACTPERSON, ?ADDRESS, ?PLACE,?STATE, ?COUNTRY_ID, ?PINCODE, ?PHONE, ?FAX, ?EMAIL,
                        //                                ?URL, ?REGNO, ?REGDATE,?PERMISSIONNO, ?PERMISSIONDATE, ?A12NO, ?PANNO, ?GIRNO, ?TANNO, ?ASSOCIATIONNATURE, ?DENOMINATION);";

                        Query = " INSERT INTO MASTER_INSTI_PERFERENCE\n" +
                               "  (\n" +
                               "   SOCIETYNAME,\n" +
                               "   CONTACTPERSON,\n" +
                               "   ADDRESS,\n" +
                               "   PLACE,\n" +
                               "   STATE_ID,\n" +
                               "   COUNTRY_ID,\n" +
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
                               "   LEDGER_ID,\n" +
                               "   ASSOCIATIONNATURE,\n" +
                               "   DENOMINATION,\n" +
                               "   OTHER_ASSOCIATION_NATURE,\n" +
                               "   FCRINO,\n" +
                               "   FCRIREGDATE,\n" +
                               "   EIGHTYGNO, EIGHTY_GNO_REG_DATE,\n" +
                               "   GST_NO,\n" +
                               "   OTHER_DENOMINATION)\n" +
                               "VALUES\n" +
                               "  (\n" +
                               "   ?SOCIETYNAME,\n" +
                               "   ?CONTACTPERSON,\n" +
                               "   ?ADDRESS,\n" +
                               "   ?PLACE,\n" +
                               "   ?STATE_ID,\n" +
                               "   ?COUNTRY_ID,\n" +
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
                               "   ?LEDGER_ID,\n" +
                               "   ?ASSOCIATIONNATURE,\n" +
                               "   ?DENOMINATION,\n" +
                               "   ?OTHER_ASSOCIATION_NATURE,\n" +
                               "   ?FCRINO,\n" +
                               "   ?FCRIREGDATE,\n" +
                               "   ?EIGHTYGNO,?EIGHTY_GNO_REG_DATE,\n" +
                               "   ?GST_NO,\n" +
                               "   ?OTHER_DENOMINATION);";


                        break;
                    }
                #endregion

                #region Updat Query
                case SQLCommand.LegalEntity.Update:
                    {
                        Query = @"UPDATE MASTER_INSTI_PERFERENCE
                                         SET
                                            SOCIETYNAME=?SOCIETYNAME,
                                            CONTACTPERSON=?CONTACTPERSON,
                                            ADDRESS=?ADDRESS,
                                            PLACE=?PLACE,
                                            STATE_ID=?STATE_ID,
                                            COUNTRY_ID=?COUNTRY_ID,
                                            PINCODE=?PINCODE,
                                            PHONE=?PHONE,
                                            FAX=?FAX,
                                            EMAIL=?EMAIL,
                                            URL=?URL,
                                            REGNO=?REGNO,
                                            REGDATE=?REGDATE,
                                            PERMISSIONNO=?PERMISSIONNO,
                                            PERMISSIONDATE=?PERMISSIONDATE,
                                            A12NO=?A12NO,
                                            PANNO=?PANNO,
                                            GIRNO=?GIRNO,
                                            TANNO=?TANNO,
                                            LEDGER_ID=?LEDGER_ID,
                                            ASSOCIATIONNATURE=?ASSOCIATIONNATURE,
                                            DENOMINATION=?DENOMINATION,
                                            OTHER_ASSOCIATION_NATURE=?OTHER_ASSOCIATION_NATURE,
                                            OTHER_DENOMINATION=?OTHER_DENOMINATION,
                                            FCRINO=?FCRINO,
                                            FCRIREGDATE=?FCRIREGDATE,
                                            EIGHTYGNO=?EIGHTYGNO,
                                            EIGHTY_GNO_REG_DATE=?EIGHTY_GNO_REG_DATE,
                                            GST_NO=?GST_NO
                                        WHERE CUSTOMERID=?CUSTOMERID;";
                        break;
                    }
                #endregion

                #region Delete Query
                case SQLCommand.LegalEntity.Delete:
                    {
                        Query = "DELETE FROM MASTER_INSTI_PERFERENCE WHERE CUSTOMERID=?CUSTOMERID;";
                        break;
                    }
                #endregion

                #region Fetch Query
                case SQLCommand.LegalEntity.FetchAll:
                    {
                        //Query = "SELECT CUSTOMERID,\n" +
                        //        "       INSTITUTENAME,\n" +
                        //        "       SOCIETYNAME,\n" +
                        //        "       REGNO,\n" +
                        //        "       IF(REGDATE = '0001-01-01 00:00:00','',DATE_FORMAT(REGDATE, '%d/%m/%Y')) AS REGDATE,\n" +
                        //        "       ADDRESS\n" +
                        //        "  FROM MASTER_INSTI_PERFERENCE";

                        Query = "SELECT MIP.CUSTOMERID, MIP.INSTITUTENAME, MIP.SOCIETYNAME, MIP.REGNO, IF(MIP.REGDATE = '0001-01-01 00:00:00','',DATE_FORMAT(MIP.REGDATE, '%d/%m/%Y')) AS REGDATE,\n" +
                                    "MIP.PERMISSIONNO, IF(MIP.PERMISSIONDATE = '0001-01-01 00:00:00','',DATE_FORMAT(MIP.PERMISSIONDATE, '%d/%m/%Y')) AS PERMISSIONDATE,\n" +
                                    "MIP.CONTACTPERSON, MIP.ADDRESS, MIP.PLACE, MS.STATE_NAME, IFNULL(MS.STATE_CODE,'') AS STATE_CODE, MC.COUNTRY, MIP.PINCODE,MIP.PHONE, MIP.FAX, MIP.EMAIL, MIP.URL,\n" +
                                    "IFNULL(MIP.PERMISSIONNO,'') AS PERMISSIONNO, IFNULL(MIP.PERMISSIONDATE, '') AS PERMISSIONDATE, IFNULL(MIP.A12NO, '') AS A12NO,\n" +
                                    "IFNULL(MIP.PANNO, '') AS PANNO, IFNULL(MIP.GIRNO, '') AS GIRNO, IFNULL(MIP.TANNO, '') AS TANNO, IFNULL(MIP.GST_NO, '') AS GST_NO,\n" +
                                    "IFNULL(MIP.FCRINO, '') AS FCRINO, IFNULL(MIP.FCRIREGDATE, '') AS FCRIREGDATE,\n" +
                                    "IFNULL(MIP.EIGHTYGNO, '') AS EIGHTYGNO, IFNULL(EIGHTY_GNO_REG_DATE,'') AS EIGHTY_GNO_REG_DATE\n" +
                                    "FROM MASTER_INSTI_PERFERENCE MIP\n" +
                                    "LEFT JOIN MASTER_STATE MS ON MS.STATE_ID = MIP.STATE_ID\n" +
                                    "LEFT JOIN MASTER_COUNTRY MC ON MC.COUNTRY_ID = MIP.COUNTRY_ID\n" +
                                    "ORDER BY MIP.SOCIETYNAME;";
                        break;
                    }
                case SQLCommand.LegalEntity.FetchByID:
                    {
                        //                        Query = @"SELECT CUSTOMERID, INSTITUTENAME, SOCIETYNAME, CONTACTPERSON, ADDRESS, PLACE, STATE, COUNTRY_ID, PINCODE, PHONE, FAX, EMAIL, 
                        //                                        URL, REGNO, REGDATE, PERMISSIONNO, PERMISSIONDATE, A12NO, PANNO, GIRNO, TANNO, ASSOCIATIONNATURE, DENOMINATION
                        //                               FROM MASTER_INSTI_PERFERENCE WHERE CUSTOMERID=?CUSTOMERID";
                        Query = "SELECT CUSTOMERID,\n" +
                                "       SOCIETYNAME,\n" +
                                "       CONTACTPERSON,\n" +
                                "       ADDRESS,\n" +
                                "       PLACE,\n" +
                                "       STATE_ID,\n" +
                                "       COUNTRY_ID,\n" +
                                "       PINCODE,\n" +
                                "       PHONE,\n" +
                                "       FAX,\n" +
                                "       EMAIL,\n" +
                                "       URL,\n" +
                                "       REGNO,\n" +
                                "       REGDATE,\n" +
                                "       PERMISSIONNO,\n" +
                                "       PERMISSIONDATE,\n" +
                                "       A12NO,\n" +
                                "       PANNO,\n" +
                                "       GIRNO,\n" +
                                "       TANNO,\n" +
                                "       LEDGER_ID,\n" +
                                "       ASSOCIATIONNATURE,\n" +
                                "       DENOMINATION,\n" +
                                "       OTHER_ASSOCIATION_NATURE,\n" +
                                "       FCRINO,\n" +
                                "       FCRIREGDATE,\n" +
                                "       EIGHTYGNO, EIGHTY_GNO_REG_DATE,\n" +
                                "       GST_NO,\n" +
                                "       OTHER_DENOMINATION\n" +
                                "  FROM MASTER_INSTI_PERFERENCE\n" +
                                " WHERE CUSTOMERID = ?CUSTOMERID;";

                        break;
                    }
                case SQLCommand.LegalEntity.CheckLegalEntity:
                    {
                        Query = "SELECT MI.CUSTOMERID\n" +
                                "  FROM MASTER_PROJECT MP\n" +
                                " LEFT JOIN MASTER_INSTI_PERFERENCE MI\n" +
                                "    ON MI.CUSTOMERID = MP.CUSTOMERID\n" +
                                " WHERE MP.PROJECT_ID IN (?PROJECT_ID)\n" +
                                " GROUP BY MP.CUSTOMERID;";
                        break;
                    }
                #endregion
            }
            return Query;
        }
        #endregion
    }
}
