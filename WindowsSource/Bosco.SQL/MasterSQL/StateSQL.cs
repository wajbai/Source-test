using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class StateSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.State).FullName)
            {
                query = GetStateSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the bank details.
        /// </summary>
        /// <returns></returns>
        private string GetStateSQL()
        {
            string query = "";
            SQLCommand.State sqlCommandId = (SQLCommand.State)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.State.Add:
                    {
                        query = "INSERT INTO MASTER_STATE(STATE_NAME, STATE_CODE, COUNTRY_ID) VALUES(?STATE_NAME, ?STATE_CODE, ?COUNTRY_ID);";
                        break;

                    }
                case SQLCommand.State.Update:
                    {
                        query = "UPDATE MASTER_STATE SET STATE_NAME = ?STATE_NAME, STATE_CODE = ?STATE_CODE, COUNTRY_ID = ?COUNTRY_ID WHERE STATE_ID = ?STATE_ID;";
                        break;
                    }

                case SQLCommand.State.Delete:
                    {
                        query = "DELETE FROM MASTER_STATE WHERE STATE_ID=?STATE_ID;";
                        break;

                    }

                case SQLCommand.State.Fetch:
                    {
                        query = "SELECT STATE_ID, STATE_NAME, IFNULL(STATE_CODE, '') AS STATE_CODE, COUNTRY_ID FROM MASTER_STATE WHERE STATE_ID = ?STATE_ID;";
                        break;

                    }
                case SQLCommand.State.GetStateId:
                    {
                        query = "SELECT STATE_ID FROM MASTER_STATE WHERE STATE_NAME=?STATE_NAME";
                        break;
                    }
                case SQLCommand.State.FetchStateByStateName:
                    {
                        query = "SELECT STATE_ID, IFNULL(STATE_CODE, '') AS STATE_CODE, STATE_NAME, COUNTRY\n" +
                                 "FROM MASTER_STATE MS INNER JOIN MASTER_COUNTRY MC ON MC.COUNTRY_ID= MS.COUNTRY_ID\n" +
                                 "WHERE STATE_NAME=?STATE_NAME";
                        break;
                    }
                case SQLCommand.State.FetchState:
                    {
                        query = "SELECT STATE_ID, STATE_CODE, STATE_NAME\n" +
                              "FROM MASTER_STATE {WHERE COUNTRY_ID IN(?COUNTRY_ID)} ORDER BY STATE_NAME ASC";
                        break;
                    }

                case SQLCommand.State.FetchAll:
                    {
                        query = "SELECT STATE_ID, IFNULL(STATE_CODE, '') AS STATE_CODE, STATE_NAME, COUNTRY\n" +  
                                  "FROM MASTER_STATE MS INNER JOIN MASTER_COUNTRY MC ON MC.COUNTRY_ID= MS.COUNTRY_ID";
                        break;

                    }

                case SQLCommand.State.FetchStateByCountryID:
                    {
                        query = "SELECT STATE_ID,STATE_NAME FROM MASTER_STATE WHERE COUNTRY_ID IN (?COUNTRY_ID)";
                        break;

                    }
            }

            return query;
        }
        #endregion
    }
}

