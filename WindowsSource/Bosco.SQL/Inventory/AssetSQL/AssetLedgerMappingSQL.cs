using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.DAO;

namespace Bosco.SQL
{
    public class AssetLedgerMappingSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.AssetLedgerMapping).FullName)
            {
                query = GetAssetLedgerSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        #region SQL Script
        public string GetAssetLedgerSQL()
        {
            string Query = string.Empty;
            SQLCommand.AssetLedgerMapping SqlcommandId = (SQLCommand.AssetLedgerMapping)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.AssetLedgerMapping.FetchLocation:
                    {
                        Query = "SELECT AL.LOCATION_ID, BLOCK, LOCATION\n" +
                                "  FROM ASSET_LOCATION AL\n" +
                                "  LEFT JOIN ASSET_BLOCK AB\n" +
                                "    ON AL.BLOCK_ID = AB.BLOCK_ID\n" +
                                " WHERE LOCATION_ID NOT In\n" +
                                "       (SELECT LOCATION_ID FROM ASSET_PROJECT_LOCATION WHERE PROJECT_ID =?PROJECT_ID);";
                        break;
                    }
                case SQLCommand.AssetLedgerMapping.FetchAssetLedgerAll:
                    {
                        Query = "SELECT NAME,LEDGER_ID FROM ASSET_LEDGER";
                        break;
                    }
                //case SQLCommand.AssetLedgerMapping.FetchAssetLedgerAll:
                //    {
                //        Query = "SELECT MONTH, ACCOUNT_LEDGER, DISPOSAL_LEDGER, DEPRECIATION_LEDGER FROM ASSET_LEDGER";
                //        break;
                //    }

                case SQLCommand.AssetLedgerMapping.SaveAssetLedgers:
                    {
                        Query = "INSERT INTO ASSET_LEDGER ( " +
                               "NAME, " +
                               "LEDGER_ID )VALUES( " +
                               " ?NAME, " +
                               " ?LEDGER_ID) ON DUPLICATE KEY UPDATE LEDGER_ID=?LEDGER_ID";
                        break;
                    }

                //case SQLCommand.AssetLedgerMapping.DeleteAssetMappedLedger:
                //    {
                //        Query = "DELETE FROM ASSET_LEDGER";
                //        break;
                //    }


                //case SQLCommand.AssetLedgerMapping.SaveAssetLedgers:
                //    {
                //        Query = "INSERT INTO ASSET_LEDGER\n" +
                //                "  (MONTH, ACCOUNT_LEDGER, DISPOSAL_LEDGER, DEPRECIATION_LEDGER)\n" +
                //                "VALUES\n" +
                //                "  (?MONTH, ?ACCOUNT_LEDGER, ?DISPOSAL_LEDGER, ?DEPRECIATION_LEDGER);"; ;
                //        break;
                //    }

                case SQLCommand.AssetLedgerMapping.GetMappedProjectLocation:
                    {
                        Query = "SELECT AL.LOCATION_ID, BLOCK, LOCATION\n" +
                                "  FROM ASSET_LOCATION AL\n" +
                                "  LEFT JOIN ASSET_BLOCK AB\n" +
                                "    ON AL.BLOCK_ID = AB.BLOCK_ID\n" +
                                " WHERE LOCATION_ID In\n" +
                                "       (SELECT LOCATION_ID FROM ASSET_PROJECT_LOCATION WHERE PROJECT_ID =?PROJECT_ID);";
                        break;
                    }
            }
            return Query;
        }
        #endregion
    }
}
