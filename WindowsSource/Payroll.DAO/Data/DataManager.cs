/* Class        : DataManager.cs
 * Purpose      : Send SQL Parameter collection to SQLDataHandler
 * Author       : CS
 * Created on   : 22-Jun-2009
 */

using System;
using System.Data;
using Payroll.Utility;

namespace Payroll.DAO.Data
{
    public class DataManager : DataDrivenBase
    {
        private DataParameters dataParameters = new DataParameters();
        private DataCommandArguments dataCommandArgs = new DataCommandArguments();
        private ResultArgs resultArgs = null;

        public DataManager()
            : this("", "")
        {

        }

        public DataManager(object sqlCommandId)
            : this(sqlCommandId, "")
        {

        }

        public DataManager(object sqlCommandId, string tableName)
        {
            this.DataCommandArgs.SQLCommandId = sqlCommandId;
        }

        #region Properties

        /// <summary>
        /// set or get SQL Unique Id for SQL Statement
        /// </summary>
        public DataCommandArguments DataCommandArgs
        {
            get { return dataCommandArgs; }
        }

        /// <summary>
        /// Get Data Parameter
        /// </summary>
        public virtual DataParameters Parameters
        {
            get { return dataParameters; }
        }

        /// <summary>
        /// Get Result Arguments
        /// </summary>
        public virtual ResultArgs Result
        {
            get { return resultArgs; }
        }

        #endregion

        #region Data Driven Methods

        public ResultArgs UpdateData()
        {
            resultArgs = base.UpdateData(this);
            return resultArgs;
        }

        public ResultArgs UpdateData(string SQLStatement)
        {
            resultArgs = base.UpdateData(this, SQLStatement);
            return resultArgs;
        }

        public ResultArgs UpdateData(string SQLStatement, SQLType sqlType)
        {
            resultArgs = base.UpdateData(this, SQLStatement, sqlType);
            return resultArgs;
        }

        public ResultArgs FetchData(DataSource dataSourceType)
        {
            resultArgs = base.FetchData(this, dataSourceType);
            return resultArgs;
        }

        public ResultArgs FetchData(DataSource dataSourceType, ref object dataSource)
        {
            resultArgs = base.FetchData(this, dataSourceType, ref dataSource);
            return resultArgs;
        }

        public ResultArgs FetchData(DataSource dataSourceType, string SQLStatement)
        {
            resultArgs = base.FetchData(this, dataSourceType, SQLStatement);
            return resultArgs;
        }

        public ResultArgs FetchData(DataSource dataSourceType, string SQLStatement, SQLType sqlType)
        {
            resultArgs = base.FetchData(this, dataSourceType, SQLStatement, sqlType);
            return resultArgs;
        }

        public ResultArgs FetchData(DataSource dataSourceType, string SQLStatement, ref object dataSource)
        {
            resultArgs = base.FetchData(this, dataSourceType, SQLStatement, ref dataSource);
            return resultArgs;
        }

        #endregion

        #region IDisposable Members

        public override void Dispose()
        {
            if (dataCommandArgs != null) { dataCommandArgs = null; }
            if (dataParameters != null)
            {
                dataParameters.Clear();
                dataParameters = null;
            }

            base.Dispose();
        }

        #endregion
    }
}
