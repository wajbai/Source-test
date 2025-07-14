using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.DAO;

using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Utility;

namespace Bosco.SQL
{
    public class DepriciationVoucherSQL : IDatabaseQuery
    {
        #region VariableDeclaration
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;
        #endregion

        #region Query
        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.Depriciation).FullName)
            {
                query = GetgroupSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        #region SQL
        public string GetgroupSQL()
        {
            string query = "";
            SQLCommand.Depriciation SqlcommandId = (SQLCommand.Depriciation)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.Depriciation.DepreciationMasteAdd:
                    {
                        query = "INSERT INTO ASSET_DEPRECIATION_MASTER(DEP_DATE,\n" +
                                "LOCATION_ID,\n" +
                                "GROUP_ID,\n" +
                                "PURPOSE_ID,\n" +
                                "TO_DATE,\n" +
                                "VOUCHER_ID,\n" +
                                "PROJECT_ID,\n" +
                                "BRANCH_ID)\n" +
                                "VALUES(?DEP_DATE,\n" +
                                "?LOCATION_ID,\n" +
                                "?GROUP_ID,\n" +
                                "?PURPOSE_ID,\n" +
                                "?TO_DATE,\n" +
                                "?VOUCHER_ID,\n" +
                                "?PROJECT_ID,\n" +
                                "?BRANCH_ID)";
                        break;
                    }

                case SQLCommand.Depriciation.DepreciationMasterEdit:
                    {
                        query = "UPDATE ASSET_DEPRECIATION_MASTER SET\n" +
                                "DEP_DATE =?DEP_DATE,\n" +
                                "LOCATION_ID =?LOCATION_ID,\n" +
                                "GROUP_ID = ?GROUP_ID,\n" +
                                "PURPOSE_ID = ?PURPOSE_ID,\n" +
                                "TO_DATE = ?TO_DATE,\n" +
                                "VOUCHER_ID =?VOUCHER_ID,\n" +
                                "PROJECT_ID =?PROJECT_ID,\n" +
                                "BRANCH_ID =?BRANCH_ID WHERE DEP_ID =?DEP_ID";
                        break;
                    }

                case SQLCommand.Depriciation.AddDepreciationVoucherDetail:
                    {
                        query = "INSERT INTO ASSET_DEPRECIATION_DETAIL(DEP_ID,\n" +
                                "ITEM_ID,\n" +
                                "ASSET_ID,\n" +
                                "PURCHASED_ON,\n" +
                                "VALUE,\n" +
                                "DEP_AMOUNT)\n" +
                                "VALUES(\n" +
                                "?DEP_ID,\n" +
                                "?ITEM_ID,\n" +
                                "?ASSET_ID,\n" +
                                "?PURCHASED_ON,\n" +
                                "?VALUE,\n" +
                                "?DEP_AMOUNT)";
                        break;
                    }

                case SQLCommand.Depriciation.FechDepreciationDetails:
                    {
                        query = "SELECT ADT.DEP_ID,AI.ASSET_NAME,\n" +
                                "AG.GROUP_NAME,\n" +
                                "AI.ITEM_ID,\n" +
                                "ADT.ASSET_ID,\n" +
                                "ADT.PURCHASED_ON AS 'PURCHASED_ON',\n" +
                                "ADT.VALUE,\n" +
                                "ADT.DEP_AMOUNT AS 'DEP_PERCENTAGE' FROM ASSET_DEPRECIATION_DETAIL ADT\n" +
                                "LEFT JOIN ASSET_ITEM AI ON AI.ITEM_ID = ADT.ITEM_ID\n" +
                                 "LEFT JOIN ASSET_GROUP AG ON AG.GROUP_ID= AI.ASSET_GROUP_ID\n" +
                                "WHERE DEP_ID IN (?DEP_ID)";
                        break;
                    }

                case SQLCommand.Depriciation.FetchAll:
                    {
                        query = "SELECT ADM.DEP_ID,\n" +
                                    "ADM.DEP_DATE,\n" +
                                    "ASL.LOCATION_NAME,\n" +
                                    "AG.GROUP_NAME,\n" +
                                    "ADM.PURPOSE_ID,\n" +
                                    "ADM.TO_DATE,\n" +
                                    "ADM.VOUCHER_ID,\n" +
                                    "ADM.BRANCH_ID\n" +
                                    "FROM ASSET_DEPRECIATION_MASTER ADM\n" +
                                    "LEFT JOIN ASSET_STOCK_LOCATION ASL ON ADM.LOCATION_ID = ASL.LOCATION_ID\n" +
                                    "LEFT JOIN ASSET_GROUP AG ON AG.GROUP_ID= ADM.GROUP_ID\n" +
                                    "WHERE DEP_DATE BETWEEN ?FROM_DATE AND ?TO_DATE AND PROJECT_ID=?PROJECT_ID";
                        break;
                    }

                case SQLCommand.Depriciation.DeleteMaster:
                    {
                        query = "DELETE FROM ASSET_DEPRECIATION_MASTER WHERE DEP_ID =?DEP_ID";
                        break;
                    }

                case SQLCommand.Depriciation.DeleteDepreciationDetail:
                    {
                        query = "DELETE FROM ASSET_DEPRECIATION_DETAIL WHERE DEP_ID =?DEP_ID";
                        break;
                    }

                case SQLCommand.Depriciation.FetchDepreciationMaster:
                    {
                        query = "SELECT ADM.DEP_ID,\n" +
                                     "ADM.DEP_DATE,\n" +
                                     "ASL.LOCATION_ID,\n" +
                                     "ASL.LOCATION_NAME,\n" +
                                     "AG.GROUP_ID,\n" +
                                     "AG.GROUP_NAME,\n" +
                                     "ADM.PURPOSE_ID,\n" +
                                     "ADM.TO_DATE,\n" +
                                     "ADM.VOUCHER_ID,\n" +
                                     "ADM.BRANCH_ID\n" +
                                     "FROM ASSET_DEPRECIATION_MASTER ADM\n" +
                                     "LEFT JOIN ASSET_DEPRECIATION_DETAIL ADT ON ADT.DEP_ID = ADM.DEP_ID\n" +
                                     "LEFT JOIN ASSET_STOCK_LOCATION ASL ON ADM.LOCATION_ID = ASL.LOCATION_ID\n" +
                                     "LEFT JOIN ASSET_GROUP AG ON AG.GROUP_ID= ADM.GROUP_ID WHERE ADM.DEP_ID =?DEP_ID";
                        break;
                    }
            }
            return query;
        #endregion
        }
    }
}
