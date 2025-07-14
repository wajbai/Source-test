using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
   public  class InsuranceSQL: IDatabaseQuery
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
                        query = "INSERT INTO ASSET_INSURANCE (" +
                               "NAME," +
                               "DESCRIPTION) VALUES(" +
                               "?NAME," +
                               "?DESCRIPTION)";
                        break;

                    }
                case SQLCommand.AssetInsurance.Update:
                    {
                        query = "UPDATE ASSET_INSURANCE SET " +
                                           " NAME= ?NAME," +
                                           "DESCRIPTION= ?DESCRIPTION " +
                                           "WHERE INSURANCE_ID= ?INSURANCE_ID";
                        break;
                    }
                    
                case SQLCommand.AssetInsurance.Delete:
                        {
                            query = "DELETE FROM ASSET_INSURANCE WHERE INSURANCE_ID =?INSURANCE_ID";
                            break;

                        }
                case SQLCommand.AssetInsurance.FetchAll:
                        {
                            query = "SELECT INSURANCE_ID, NAME, DESCRIPTION " +
                                    "FROM ASSET_INSURANCE";

                            break;
                        }
                case SQLCommand.AssetInsurance.Fetch:
                        {
                            query = "SELECT INSURANCE_ID, NAME, DESCRIPTION FROM ASSET_INSURANCE WHERE INSURANCE_ID = ?INSURANCE_ID";
                            break;
                        }


            }
            return query;
        }

        #endregion
   }
}
