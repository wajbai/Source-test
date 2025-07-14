/* Class        : IDatabaseQuery.cs
 * Purpose      : Interface to SQLQueryHandler
 * Author       : CS
 * Created on   : 14-Jun-2010
 */

using System;
using Payroll.DAO.Data;

namespace Payroll.DAO
{
    public interface IDatabaseQuery
    {
        string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType);
    }
}
