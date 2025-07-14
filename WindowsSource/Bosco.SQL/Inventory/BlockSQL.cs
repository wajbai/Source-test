using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Schema;
using Bosco.DAO.Data;

namespace Bosco.SQL
{
    public class BlockSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.Block).FullName)
            {
                query = GetBlock();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        #region SQL Script
        private string GetBlock()
        {
            string query = "";
            SQLCommand.Block sqlCommandId = (SQLCommand.Block)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {
                case SQLCommand.Block.Add:
                    {
                        query = "INSERT INTO ASSET_BLOCK ( " +
                            "BLOCK) VALUES( " +
                            "?BLOCK)";
                        break;
                    }
                case SQLCommand.Block.Update:
                    {
                        query = "UPDATE ASSET_BLOCK SET " +
                            "BLOCK =?BLOCK " +
                            "WHERE BLOCK_ID = ?BLOCK_ID";

                        break;
                    }
                case SQLCommand.Block.Delete:
                    {
                        query = "DELETE FROM ASSET_BLOCK WHERE BLOCK_ID =?BLOCK_ID";
                        break;
                    }
                case SQLCommand.Block.Fetch:
                    {
                        query = "SELECT " +
                        "BLOCK_ID, " +
                        "BLOCK " +
                        "FROM " +
                        "ASSET_BLOCK WHERE BLOCK_ID=?BLOCK_ID";
                        break;
                    }
                case SQLCommand.Block.FetchAll:
                    {
                        query = "SELECT " +
                            "BLOCK_ID, " +
                            "BLOCK " +
                            "FROM " +
                            "ASSET_BLOCK ORDER BY BLOCK ASC ";
                        break;
                    }
            }
            return query;
        }
        #endregion

    }
}
