using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class CostCentreSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.CostCentre).FullName)
            {
                query = GetCostCentreSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the cost centre details.
        /// </summary>
        /// <returns></returns>
        private string GetCostCentreSQL()
        {
            string query = "";
            SQLCommand.CostCentre sqlCommandId = (SQLCommand.CostCentre)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.CostCentre.Add:
                    {
                        query = "INSERT INTO MASTER_COST_CENTRE ( " +
                               "ABBREVATION, " +
                               "COST_CENTRE_NAME, " +
                               "NOTES) VALUES( " +
                               "?ABBREVATION, " +
                               "?COST_CENTRE_NAME, " +
                               "?NOTES )";
                        break;
                    }
                case SQLCommand.CostCentre.Update:
                    {
                        query = "UPDATE MASTER_COST_CENTRE SET " +
                                    "ABBREVATION = ?ABBREVATION, " +
                                    "COST_CENTRE_NAME=?COST_CENTRE_NAME ," +
                                    "NOTES=?NOTES " +
                                    "WHERE COST_CENTRE_ID=?COST_CENTRE_ID ";
                        break;
                    }
                case SQLCommand.CostCentre.Delete:
                    {
                        query = "DELETE FROM MASTER_COST_CENTRE WHERE COST_CENTRE_ID=?COST_CENTRE_ID";
                        break;
                    }
                case SQLCommand.CostCentre.Fetch:
                    {
                        query = "SELECT " +
                                "COST_CENTRE_ID, " +
                                "ABBREVATION, " +
                                "COST_CENTRE_NAME, " +
                                "NOTES " +
                            "FROM " +
                                "MASTER_COST_CENTRE " +
                                " WHERE COST_CENTRE_ID=?COST_CENTRE_ID ";
                        break;
                    }
                case SQLCommand.CostCentre.FetchAll:
                    {
                        query = "SELECT MC.COST_CENTRE_ID, ABBREVATION, COST_CENTRE_NAME, CC.COST_CENTRE_CATEGORY_NAME, NOTES FROM MASTER_COST_CENTRE  MC \n"+
                                "LEFT JOIN COSTCATEGORY_COSTCENTRE CCA \n"+
                                "ON MC.COST_CENTRE_ID=CCA.COST_CENTRE_ID \n"+
                                "LEFT JOIN MASTER_COST_CENTRE_CATEGORY CC \n"+
                                "ON CC.COST_CENTRECATEGORY_ID=CCA.COST_CATEGORY_ID \n"+
                                "ORDER BY ABBREVATION ASC;";
                        break;
                    }
                case SQLCommand.CostCentre.FetchforLookupByProjectLedger:
                    {
                        query = "SELECT MS.COST_CENTRE_ID,\n" +
                                "       MS.ABBREVATION,\n" +
                                "       CONCAT(MS.COST_CENTRE_NAME, ' (', MCC.COST_CENTRE_CATEGORY_NAME, ')') AS COST_CENTRE_NAME,\n" +
                                "       MCC.COST_CENTRE_CATEGORY_NAME,\n" +
                                "       MS.NOTES\n" +
                                " FROM PROJECT_COSTCENTRE PCC\n" +
                                " INNER JOIN MASTER_COST_CENTRE MS ON PCC.COST_CENTRE_ID = MS.COST_CENTRE_ID\n" +
                                " INNER JOIN COSTCATEGORY_COSTCENTRE CCA ON MS.COST_CENTRE_ID = CCA.COST_CENTRE_ID\n" +
                                " INNER JOIN MASTER_COST_CENTRE_CATEGORY MCC ON MCC.COST_CENTRECATEGORY_ID = CCA.COST_CATEGORY_ID\n" +
                                " WHERE PCC.PROJECT_ID IN (?PROJECT_ID) {AND PCC.LEDGER_ID =?LEDGER_ID} GROUP BY MS.COST_CENTRE_ID\n" +
                                " ORDER BY ABBREVATION ASC;";
                        break;
                    }
                case SQLCommand.CostCentre.FetchforLookupByProject:
                    {
                        query = "SELECT MS.COST_CENTRE_ID,\n" +
                                "       MS.ABBREVATION,\n" +
                                "       CONCAT(MS.COST_CENTRE_NAME, ' (', MCC.COST_CENTRE_CATEGORY_NAME, ')') AS COST_CENTRE_NAME,\n" +
                                "       MCC.COST_CENTRE_CATEGORY_NAME,\n" +
                                "       MS.NOTES\n" +
                                " FROM PROJECT_COSTCENTRE PCC\n" +
                                " INNER JOIN MASTER_COST_CENTRE MS ON PCC.COST_CENTRE_ID = MS.COST_CENTRE_ID\n" +
                                " INNER JOIN COSTCATEGORY_COSTCENTRE CCA ON MS.COST_CENTRE_ID = CCA.COST_CENTRE_ID\n" +
                                " INNER JOIN MASTER_COST_CENTRE_CATEGORY MCC ON MCC.COST_CENTRECATEGORY_ID = CCA.COST_CATEGORY_ID\n" +
                                " WHERE PCC.PROJECT_ID =?PROJECT_ID {AND PCC.LEDGER_ID =?LEDGER_ID} GROUP BY MS.COST_CENTRE_ID\n" +
                                " ORDER BY ABBREVATION ASC;";
                        break;
                    }
                case SQLCommand.CostCentre.SetCostCentreSource:
                    {
                        query = @"SELECT P.PROJECT_ID, MCC.COST_CENTRECATEGORY_ID, M.COST_CENTRE_ID,CONCAT(M.COST_CENTRE_NAME, 
                                ' (', MCC.COST_CENTRE_CATEGORY_NAME, ')') AS COST_CENTRE_NAME,  M.COST_CENTRE_NAME AS COST_CENTRE
                                FROM MASTER_COST_CENTRE M 
                                LEFT JOIN PROJECT_COSTCENTRE P ON M.COST_CENTRE_ID=P.COST_CENTRE_ID
                                INNER JOIN COSTCATEGORY_COSTCENTRE CCA ON M.COST_CENTRE_ID = CCA.COST_CENTRE_ID
                                INNER JOIN MASTER_COST_CENTRE_CATEGORY MCC ON MCC.COST_CENTRECATEGORY_ID = CCA.COST_CATEGORY_ID
                                WHERE 1=1 {AND P.PROJECT_ID IN (?PROJECT_ID)} {AND P.LEDGER_ID IN (?LEDGER_ID_COLLECTION)} 
                                {AND MCC.COST_CENTRECATEGORY_ID IN (?COST_CENTRECATEGORY_ID)}    
                                GROUP BY M.COST_CENTRE_NAME";
                        break;
                    }
                case SQLCommand.CostCentre.FetchCostCentreCodes:
                    {
                        query = "SELECT COST_CENTRE_ID, ABBREVATION AS 'USED_CODE',COST_CENTRE_NAME AS 'NAME' FROM MASTER_COST_CENTRE ORDER BY COST_CENTRE_ID DESC";
                        break;
                    }
                case SQLCommand.CostCentre.FetchCostcentreByExistingCode:
                    {
                        query = "SELECT ABBREVATION AS 'EXIST_CODE' FROM MASTER_COST_CENTRE WHERE ABBREVATION=?ABBREVATION;";
                        break;
                    }
                case SQLCommand.CostCentre.IsCostCentreExist:
                    {
                        query = "SELECT COUNT(*) FROM MASTER_COST_CENTRE WHERE ABBREVATION=?ABBREVATION";
                        break;
                    }
                case SQLCommand.CostCentre.IsCostCentreNameExist:
                    {
                        query = "SELECT COUNT(*) FROM MASTER_COST_CENTRE WHERE COST_CENTRE_NAME=?COST_CENTRE_NAME";
                        break;
                    }
                case SQLCommand.CostCentre.FetchCostCentreId:
                    {
                        query = "SELECT COST_CENTRE_ID FROM MASTER_COST_CENTRE WHERE COST_CENTRE_NAME=?COST_CENTRE_NAME";
                        break;
                    }
                case SQLCommand.CostCentre.FetchCostCentreCategory:
                    {
                        query = @"SELECT COST_CENTRECATEGORY_ID,COST_CENTRE_CATEGORY_NAME FROM MASTER_COST_CENTRE_CATEGORY 
                                    WHERE COST_CENTRE_CATEGORY_NAME IS NOT NULL ORDER BY COST_CENTRE_CATEGORY_NAME ASC";
                        break;
                    }
                case SQLCommand.CostCentre.FetchCostCentreCategorybyId:
                    {
                        query = @"SELECT MC.COST_CENTRE_ID,CC.COST_CENTRECATEGORY_ID, ABBREVATION, COST_CENTRE_NAME, CC.COST_CENTRE_CATEGORY_NAME, NOTES FROM MASTER_COST_CENTRE  MC
                                    LEFT JOIN COSTCATEGORY_COSTCENTRE CCA
                                      ON MC.COST_CENTRE_ID=CCA.COST_CENTRE_ID
                                    LEFT JOIN MASTER_COST_CENTRE_CATEGORY CC
                                      ON CC.COST_CENTRECATEGORY_ID=CCA.COST_CATEGORY_ID
                                    WHERE MC.COST_CENTRE_ID=?COST_CENTRE_ID;";
                        break;
                    }
            }
            return query;
        }
        #endregion Bank SQL
    }
}
