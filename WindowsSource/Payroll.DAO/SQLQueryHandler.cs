/* Pattern  : Creational - Factory Method
 *          : Creator -> SQLQueryHandler (defines the interface of objects the factory method creates)
 *          : Product -> IDatabaseQuery
 *          : ConcreteProduct -> Method GetQuery() (Creates the object of type IDatabaseQuery)
 * Created  : 14-Jul-2010
 * Author   : CS
 */

using System;
using System.Configuration;
using Payroll.DAO.Data;
using Payroll.Utility;

namespace Payroll.DAO
{
    public class SQLQueryHandler : SQLParser, IDatabaseQuery
    {
        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            string typeName = "";
            sqlType = SQLType.SQLStatic;
            IDatabaseQuery iSQLServerQuery = null;

            //typeName = ConfigurationManager.AppSettings[AppSettingName.SQLAdapter.ToString()];
            typeName = ConfigurationManager.AppSettings["PayrollSQLAdapter"];

            if (typeName != "")
            {
                object instance = this.GetDynamicInstance(typeName, null);

                if (instance != null)
                {
                    iSQLServerQuery = instance as IDatabaseQuery;
                }
            }

            if (iSQLServerQuery != null)
            {
                query = iSQLServerQuery.GetQuery(dataCommandArgs, ref sqlType);
            }
            else
            {
                throw new Exception("SQL Adapter assembly type not found or not properly configured in the config file :: " + this.GetType().Assembly.FullName + ".GetQuery()");
            }

            return query;
        }

        public object GetDynamicInstance(string instanceType, object[] args)
        {
            Type type = Type.GetType(instanceType, false, true);
            object instance = null;

            if (type != null)
            {
                try
                {
                    if (args != null)
                    {
                        instance = System.Activator.CreateInstance(type, args);
                    }
                    else
                    {
                        instance = System.Activator.CreateInstance(type);
                    }
                }
                catch (Exception e)
                {
                    throw new ExceptionHandler(e, true);
                }
            }

            return instance;
        }
    }
}
