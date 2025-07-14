using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class AssetDepreciationSQL : IDatabaseQuery
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
            switch (SqlcommandId)
            {
                case SQLCommand.AssetDepreciation.Add:
                    {
                        query = "INSERT INTO ASSET_DEPRECIATION_METHOD (" +
                                "NAME," +
                                "DESCRIPTION)VALUES(" +
                                "?NAME, " +
                                "?DESCRIPTION)";
                        break;
                    }
                case SQLCommand.AssetDepreciation.Update:
                    {
                        query = "UPDATE ASSET_DEPRECIATION_METHOD SET " +
                               " NAME= ?NAME," +
                               "DESCRIPTION= ?DESCRIPTION " +
                               "WHERE METHOD_ID= ?METHOD_ID";
                        break;
                    }
                case SQLCommand.AssetDepreciation.FetchAll:
                    {
                        query = "SELECT METHOD_ID, DEP_METHOD " +
                                "FROM ASSET_DEP_METHOD";
                        break;
                    }

                case SQLCommand.AssetDepreciation.FetchDepMethods:
                    {
                        query = "SELECT METHOD_ID ,IF(METHOD_ID = 1, 'S L M', 'W D V') AS DEP_METHOD " +
                                "FROM ASSET_DEP_METHOD";
                        break;
                    }
                case SQLCommand.AssetDepreciation.Fetch:
                    {
                        query = "SELECT METHOD_ID,NAME, DESCRIPTION FROM ASSET_DEPRECIATION_METHOD WHERE METHOD_ID=?METHOD_ID";
                        break;
                    }
                case SQLCommand.AssetDepreciation.Delete:
                    {
                        query = "DELETE FROM ASSET_DEPRECIATION_METHOD WHERE METHOD_ID=?METHOD_ID";
                        break;
                    }
                case SQLCommand.AssetDepreciation.FetchDepreciationMaster:
                    {
                        query = "SELECT DEPRECIATION_ID,VOUCHER_ID,\n" +
                        "       DEPRECIATION_APPLIED_ON,\n" +
                        "       ADM.PROJECT_ID,\n" +
                        "       MP.PROJECT,\n" +
                        "       DEPRECIATION_PERIOD_FROM,\n" +
                        "       DEPRECIATION_PERIOD_TO,\n" +
                        "       NARRATION\n" +
                        "  FROM ASSET_DEPRECIATION_MASTER ADM\n" +
                        "  LEFT JOIN MASTER_PROJECT MP\n" +
                        "    ON MP.PROJECT_ID = ADM.PROJECT_ID\n" +
                        " WHERE ADM.PROJECT_ID = ?PROJECT_ID\n" +
                        "   AND DEPRECIATION_PERIOD_FROM >= ?DEPRECIATION_PERIOD_FROM\n" +
                        "   AND DEPRECIATION_PERIOD_TO <= ?DEPRECIATION_PERIOD_TO ORDER BY DEPRECIATION_PERIOD_FROM;";
                        ;

                        break;
                    }
                case SQLCommand.AssetDepreciation.FetchDepreciationDetailById:
                    {

                        query = "SELECT ADPD.DEPRECIATION_ID,\n" +
                        "       ADPD.ITEM_DETAIL_ID,\n" +
                        "       ADPD.DEPRECIATION_PERCENTAGE,\n" +
                        "       ADPD.DEPRECIATION_VALUE,\n" +
                        "       ADPD.BALANCE_AMOUNT,\n" +
                        "       AC.ASSET_CLASS,\n" +
                        "       AI.ASSET_ITEM,\n" +
                        "       AID.ASSET_ID\n" +
                        "  FROM ASSET_DEPRECIATION_DETAIL ADPD\n" +
                        "  INNER JOIN ASSET_DEPRECIATION_MASTER ADM\n" +
                        "    ON ADPD.DEPRECIATION_ID = ADM.DEPRECIATION_ID\n" +
                        "  INNER JOIN ASSET_ITEM_DETAIL AID\n" +
                        "    ON AID.ITEM_DETAIL_ID = ADPD.ITEM_DETAIL_ID\n" +
                        "  INNER JOIN ASSET_ITEM AI\n" +
                        "    ON AI.ITEM_ID = AID.ITEM_ID\n" +
                        "  INNER JOIN ASSET_CLASS AC\n" +
                        "    ON AC.ASSET_CLASS_ID = AI.ASSET_cLASS_ID\n" +
                        " WHERE ADPD.DEPRECIATION_ID IN (?DEPRECIATION_ID);";
                        break;
                    }
                case SQLCommand.AssetDepreciation.DeleteDepreciationDetailById:
                    {
                        query="DELETE FROM ASSET_DEPRECIATION_DETAIL WHERE DEPRECIATION_ID=?DEPRECIATION_ID;";
                        break;
                    }
                case SQLCommand.AssetDepreciation.DeleteDepreciationMasterById:
                    {
                        query = "DELETE FROM ASSET_DEPRECIATION_MASTER WHERE DEPRECIATION_ID=?DEPRECIATION_ID;";
                        break;
                    }
            }
            return query;
        }

        #endregion

    }
}
