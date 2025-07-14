using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class AssetInsurancePlanSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.AssetInsurance).FullName)
            {
                query = GetInsuranceSql();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        #region SQL Script
        public string GetInsuranceSql()
        {
            string query = "";
            SQLCommand.AssetInsurance SqlCommandId = (SQLCommand.AssetInsurance)(this.dataCommandArgs.SQLCommandId);
            switch (SqlCommandId)
            {
                case SQLCommand.AssetInsurance.Add:
                    {
                        query = "INSERT INTO ASSET_INSURANCE_PLAN (" +
                               "INSURANCE_PLAN," +
                               "COMPANY) VALUES(" +
                               "?INSURANCE_PLAN," +
                               "?COMPANY)";
                               
                        break;

                    }
                case SQLCommand.AssetInsurance.Update:
                    {
                        query = "UPDATE ASSET_INSURANCE_PLAN SET " +
                                           " INSURANCE_PLAN= ?INSURANCE_PLAN," +
                                           "COMPANY=?COMPANY" +
                                           "WHERE INSURANCE_PLAN_ID= ?INSURANCE_PLAN_ID";
                        break;
                    }

                case SQLCommand.AssetInsurance.Delete:
                    {
                        query = "DELETE FROM ASSET_INSURANCE_PLAN WHERE INSURANCE_PLAN_ID =?INSURANCE_PLAN_ID";
                        break;

                    }
                case SQLCommand.AssetInsurance.FetchAll:
                    {
                        query = "SELECT INSURANCE_PLAN_ID, INSURANCE_PLAN,COMPANY " +
                                "FROM ASSET_INSURANCE_PLAN";

                        break;
                    }
                case SQLCommand.AssetInsurance.FetchInsuranceDetails:
                    {
                        query = "SELECT PROVIDER,AGENT,POLICY " +
                                "FROM ASSET_INSURANCE_DETAIL";

                        break;
                    }
                case SQLCommand.AssetInsurance.Fetch:
                    {
                        query = "SELECT INSURANCE_PLAN_ID, INSURANCE_PLAN,COMPANY FROM ASSET_INSURANCE_PLAN WHERE INSURANCE_PLAN_ID = ?INSURANCE_PLAN_ID";
                        break;
                    }
                case SQLCommand.AssetInsurance.AutoFetchInsurance:
                    {
                        query = "SELECT INSURANCE_PLAN,COMPANY " +
                                 "FROM ASSET_INSURANCE_PLAN";
                        break;
                    }
                case SQLCommand.AssetInsurance.GetInsuranceType:
                    {
                        query = "SELECT INSURANCE_PLAN_ID,INSURANCE_PLAN FROM ASSET_INSURANCE_PLAN";
                        break;
                    }

            }
            return query;
        }

        #endregion
    }
}
