using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Schema;
using Bosco.DAO.Data;


namespace Bosco.SQL
{
    public class CostCentreCategorySQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.CostCentreCategory).FullName)
            {
                query = GetCostCentreCatogorySQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        #region SQL Script
        private string GetCostCentreCatogorySQL()
        {
            string query = "";
            SQLCommand.CostCentreCategory sqlCommandId = (SQLCommand.CostCentreCategory)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {
                case SQLCommand.CostCentreCategory.Add:
                    {
                        query = "INSERT INTO MASTER_COST_CENTRE_CATEGORY ( " +
                            "COST_CENTRE_CATEGORY_NAME) VALUES( " +
                            "?COST_CENTRE_CATEGORY_NAME)";
                        break;
                    }
                case SQLCommand.CostCentreCategory.Update:
                    {
                        query = "UPDATE MASTER_COST_CENTRE_CATEGORY SET " +
                            "COST_CENTRE_CATEGORY_NAME =?COST_CENTRE_CATEGORY_NAME " +
                            "WHERE COST_CENTRECATEGORY_ID = ?COST_CENTRECATEGORY_ID";

                        break;
                    }
                case SQLCommand.CostCentreCategory.Delete:
                    {
                        query = "DELETE FROM MASTER_COST_CENTRE_CATEGORY WHERE COST_CENTRECATEGORY_ID =?COST_CENTRECATEGORY_ID";
                        break;
                    }
                case SQLCommand.CostCentreCategory.Fetch:
                    {
                        query = "SELECT " +
                        "COST_CENTRECATEGORY_ID, " +
                        "COST_CENTRE_CATEGORY_NAME " +
                        "FROM " +
                        "MASTER_COST_CENTRE_CATEGORY WHERE COST_CENTRECATEGORY_ID=?COST_CENTRECATEGORY_ID";
                        break;
                    }
                case SQLCommand.CostCentreCategory.FetchAll:
                    {
                        query = "SELECT " +
                            "COST_CENTRECATEGORY_ID, " +
                            "COST_CENTRE_CATEGORY_NAME " +
                            "FROM " +
                            "MASTER_COST_CENTRE_CATEGORY ORDER BY COST_CENTRE_CATEGORY_NAME ASC ";
                        break;
                    }
                case SQLCommand.CostCentreCategory.IsCostCentreCategory:
                    {
                        query = "SELECT COUNT(*) FROM MASTER_COST_CENTRE_CATEGORY WHERE COST_CENTRE_CATEGORY_NAME=?COST_CENTRE_CATEGORY_NAME";
                        break;
                    }
                case SQLCommand.CostCentreCategory.FetchCostCentreCategoryId:
                    {
                        query = "SELECT COST_CENTRECATEGORY_ID FROM MASTER_COST_CENTRE_CATEGORY WHERE COST_CENTRE_CATEGORY_NAME=?COST_CENTRE_CATEGORY_NAME";
                        break;
                    }
                case SQLCommand.CostCentreCategory.CheckCostcentreCostCategoryExists:
                    {
                        query = "SELECT COUNT(*) FROM COSTCATEGORY_COSTCENTRE WHERE COST_CATEGORY_ID=?COST_CENTRECATEGORY_ID";
                        break;
                    }
            }
            return query;

        }
        #endregion
    }
}
