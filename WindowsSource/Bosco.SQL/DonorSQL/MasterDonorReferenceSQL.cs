using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.DAO;

namespace Bosco.SQL
{
    public class MasterDonorReferenceSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.MasterDonorReference).FullName)
            {
                query = GetMasterDonorReferenceSQL();
            }

            sqlType = this.sqlType;
            return query;
        }
        #endregion

        public string GetMasterDonorReferenceSQL()
        {
            string query = "";
            SQLCommand.MasterDonorReference sqlCommandId = (SQLCommand.MasterDonorReference)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {
                case SQLCommand.MasterDonorReference.Add:
                    {
                        query = "INSERT INTO MASTER_DONOR_REFERENCE (REFERED_STAFF_ID, REFERED_STAFF_NAME) VALUES(?REFERED_STAFF_ID, ?REFERED_STAFF_NAME)";
                        break;
                    }
                case SQLCommand.MasterDonorReference.Update:
                    {
                        query = "UPDATE MASTER_DONOR_REFERENCE SET REFERED_STAFF_NAME = ?REFERED_STAFF_NAME WHERE REFERED_STAFF_ID = ?REFERED_STAFF_ID;";
                        break;
                    }

                case SQLCommand.MasterDonorReference.Delete:
                    {
                        query = "DELETE FROM MASTER_DONOR_REFERENCE WHERE REFERED_STAFF_ID = ?REFERED_STAFF_ID";
                        break;
                    }
                case SQLCommand.MasterDonorReference.Fetch:
                    {
                        query = "SELECT REFERED_STAFF_ID, REFERED_STAFF_NAME FROM MASTER_DONOR_REFERENCE WHERE REFERED_STAFF_ID = ?REFERED_STAFF_ID";
                        break;
                    }

                case SQLCommand.MasterDonorReference.FetchAll:
                    {
                        query = "SELECT REFERED_STAFF_ID,REFERED_STAFF_NAME FROM MASTER_DONOR_REFERENCE";
                        break;
                    }
            }
            return query;
        }
    }
}
