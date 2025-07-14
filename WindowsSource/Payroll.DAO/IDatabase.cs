/* Class        : IDatabase.cs
 * Purpose      : Interface to SQLDataHandler
 * Author       : CS
 * Created on   : 23-Jun-2010
 */

using System;
using Payroll.DAO.Data;
using Payroll.Utility;

namespace Payroll.DAO
{
    public interface IDatabase : IDisposable
    {
        ResultArgs HasConnectionEstablished();
        ResultArgs Execute(DataManager sqlData, SQLType sqlType);
        ResultArgs Execute(DataManager sqlData, string SQLStatement, SQLType sqlType);
        ResultArgs Fetch(DataManager dataManager, DataSource dataSourceType, string SQLStatement, SQLType sqlType, ref object dataSource);

        void BeginTransaction();
        void EndTransaction();
        //void CommitTransaction();
        //void RollBackTransaction();
        string PreviousTransactionSource { get; }
    }
}
