using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class DepreciationSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.AssetDepreciation).FullName)
            {
                query = GetDepreciationSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        #region SQL Script
        public string GetDepreciationSQL()
        {
            string query = "";
            SQLCommand.AssetDepreciation SqlcommandId = (SQLCommand.AssetDepreciation)(this.dataCommandArgs.SQLCommandId);
                switch(SqlcommandId)
                {
                    case SQLCommand.AssetDepreciation.Add:
                        {
                            query= "INSERT INTO ASSET_DEPRECIATION (" +                                   
                                    "NAME," +
                                    "DESCRIPTION)VALUES(" + 
                                    "?NAME, " +
                                    "?DESCRIPTION)";
                            break;
                        }
                    case SQLCommand.AssetDepreciation.Update:
                        {
                            query = "UPDATE ASSET_DEPRECIATION SET " +
                                   " NAME= ?NAME," +
                                   "DESCRIPTION= ?DESCRIPTION " +
                                   "WHERE DEP_ID= ?DEP_ID";
                            break;
                        }
                    case SQLCommand.AssetDepreciation.FetchAll:
                        {
                            query= "SELECT DEP_ID, NAME, DESCRIPTION " +
                                    "FROM ASSET_DEPRECIATION";
                            break;
                        }
                    case SQLCommand.AssetDepreciation.Fetch:
                        {
                            query = "SELECT DEP_ID,NAME, DESCRIPTION FROM ASSET_DEPRECIATION WHERE DEP_ID=?DEP_ID";
                            break;
                        }
                    case SQLCommand.AssetDepreciation.Delete:
                        {
                            query = "DELETE FROM ASSET_DEPRECIATION WHERE DEP_ID=?DEP_ID";
                        break;
                        }
                }


            return query;
        }

        #endregion

    }
}
