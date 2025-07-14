using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class GSTClassSQL : IDatabaseQuery
    {
        #region MyRegion
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;
        #endregion

        #region Methods

        #endregion
        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {

            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.GSTDetails).FullName)
            {
                query = GetGSTDetailSQL();
            }
            sqlType = this.sqlType;
            return query;
        }

        public string GetGSTDetailSQL()
        {
            string query = "";
            SQLCommand.GSTDetails SqlCommandId = (SQLCommand.GSTDetails)(this.dataCommandArgs.SQLCommandId);
            switch (SqlCommandId)
            {
                case SQLCommand.GSTDetails.Add:
                    {
                        query = "INSERT INTO MASTER_GST_CLASS(SLAB, GST, CGST, SGST, APPLICABLE_FROM,STATUS)" +
                        "VALUES(?SLAB, ?GST, ?CGST, ?SGST, ?APPLICABLE_FROM,?STATUS)";
                        break;
                    }
                case SQLCommand.GSTDetails.Edit:
                    {
                        query = "UPDATE MASTER_GST_CLASS SET SLAB =?SLAB, \n" +
                                 " GST =?GST,\n" +
                                 "CGST =?CGST,\n" +
                                 "SGST =?SGST,\n" +
                                 "APPLICABLE_FROM =?APPLICABLE_FROM,\n" +
                                 "STATUS =?STATUS WHERE GST_Id =?GST_Id";
                        break;
                    }
                case SQLCommand.GSTDetails.Delete:
                    {
                        query = "DELETE FROM MASTER_GST_CLASS WHERE GST_Id =?GST_Id";
                        break;
                    }
                case SQLCommand.GSTDetails.FetchAll:
                    {
                        query = @"SELECT GST_Id,SLAB, GST, CGST, SGST, APPLICABLE_FROM, IF(STATUS=1, 'Active','DeActive') as STATUS FROM MASTER_GST_CLASS ORDER BY GST";
                        break;
                    }
                case SQLCommand.GSTDetails.FetchById:
                    {
                        query = @"SELECT GST_Id, SLAB, GST,IGST, CGST, SGST, APPLICABLE_FROM, STATUS FROM MASTER_GST_CLASS WHERE GST_Id=?GST_Id";
                        break;
                    }
                case SQLCommand.GSTDetails.FetchGSTList:
                    {
                        query = @"SELECT GST_Id, CONCAT(CONCAT('GST-',GST),CONCAT(' (CGST:',CGST),CONCAT(', SGST:',SGST, ')')) AS GST_NAME FROM MASTER_GST_CLASS ORDER BY GST";
                        break;
                    }
                case SQLCommand.GSTDetails.FetchGSTLedgerClass:
                    {
                        query = @"SELECT GST_Id, GST AS GST_NAME, APPLICABLE_FROM FROM MASTER_GST_CLASS ORDER BY GST";
                        break;
                    }
                case SQLCommand.GSTDetails.FetchZeroLedgerClassId:
                    {
                        query = @"SELECT GST_Id FROM MASTER_GST_CLASS WHERE GST='0.00'";
                        break;
                    }


            }
            return query;
        }
    }
}
