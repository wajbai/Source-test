using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    class CustodiansSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.AssetCustodians).FullName)
            {
                query = GetLocationSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        private string GetLocationSQL()
        {
            string query = "";
            SQLCommand.AssetCustodians sqlCommandId = (SQLCommand.AssetCustodians)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.AssetCustodians.Add:
                    {
                        query = "INSERT INTO ASSET_CUSTODIAN\n" +
                                "  (CUSTODIAN, ROLE)\n" +
                                "VALUES\n" +
                                "  (?CUSTODIAN, ?ROLE)";
                        break;
                    }
                case SQLCommand.AssetCustodians.FetchAll:
                    {
                        query = "SELECT CUSTODIAN_ID,\n" +
                                "       CUSTODIAN,\n" +
                                "       ROLE \n" +
                                "  FROM ASSET_CUSTODIAN WHERE CUSTODIAN <> ''";
                        break;
                    }
                case SQLCommand.AssetCustodians.Fetch:
                    {
                        query = "SELECT CUSTODIAN_ID,\n" +
                                "       CUSTODIAN,\n" +
                                "       ROLE\n" +
                                "  FROM ASSET_CUSTODIAN\n" +
                                " WHERE CUSTODIAN_ID = ?CUSTODIAN_ID;";
                        break;
                    }
                case SQLCommand.AssetCustodians.Update:
                    {
                        query = "UPDATE ASSET_CUSTODIAN\n" +
                                "   SET CUSTODIAN       = ?CUSTODIAN,\n" +
                                "       ROLE       = ?ROLE\n" +
                                " WHERE CUSTODIAN_ID = ?CUSTODIAN_ID";
                        break;
                    }
                case SQLCommand.AssetCustodians.Delete:
                    {
                        query = "DELETE FROM ASSET_CUSTODIAN WHERE CUSTODIAN_ID=?CUSTODIAN_ID";
                        break;
                    }
                case SQLCommand.AssetCustodians.DeleteCustudianDetails:
                    {
                        query = "DELETE FROM ASSET_CUSTODIAN";
                        break;
                    }
                case SQLCommand.AssetCustodians.FetchCustodianNameByID:
                    {
                        query = "SELECT CUSTODIAN_ID FROM ASSET_CUSTODIAN WHERE CUSTODIAN=?CUSTODIAN";
                        break;
                    }
                case SQLCommand.AssetCustodians.FetchCustodianNameyByLocationID:
                    {
                        query = "SELECT ASL.CUSTODIAN_ID, AC.CUSTODIAN\n" +
                                "  FROM ASSET_LOCATION ASL\n" +
                                " INNER JOIN ASSET_CUSTODIAN AC\n" +
                                "    ON AC.CUSTODIAN_ID = ASL.CUSTODIAN_ID\n" +
                                " WHERE LOCATION_ID = ?LOCATION_ID";
                        break;
                    }
                case SQLCommand.AssetCustodians.FetchMappedCustodian:
                    {
                        query = "SELECT COUNT(AC.CUSTODIAN_ID)\n" +
                                "  FROM ASSET_CUSTODIAN AC\n" +
                                " INNER JOIN ASSET_LOCATION AL\n" +
                                "    ON AC.CUSTODIAN_ID = AL.CUSTODIAN_ID\n" +
                                " WHERE AC.CUSTODIAN_ID = ?CUSTODIAN_ID";
                        break;
                    }
            }
            return query;
        }
    }
}
