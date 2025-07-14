/* Class        : DataDrivenBase.cs
 * Purpose      : Mediator between UI and Data Access (execute/retrive data source)
 * Author       : CS
 * Created on   : 24-Jun-2010
 */

using System;
using System.Collections.Generic;
using System.Text;
using Payroll.Utility;
using Payroll.DAO.Data;

namespace Payroll.DAO
{
    public abstract class DataDrivenBase : IDisposable
    {
        private IDatabase database = null;
        private object dataSourceOptional = null;
        private static IDatabase SESSION_DB_WIN = null; //for Win App

        #region Properties for Get/Set Database for Transaction Begin till end

        public IDatabase Database
        {
            get { return database; }
            set { database = value; }
        }

        private IDatabase SessionDB
        {
            get 
            {
                //if (SESSION_DB_WIN != null) { database = SESSION_DB_WIN; }
                return SESSION_DB_WIN; //database; 
            }
            set 
            {
                SESSION_DB_WIN = value;
                if (value != null) { database = value; }
            }
        }

        public DataDrivenBase() 
        {
            database = SessionDB;

            if (database == null)
            {
                database = DatabaseFactory.Instance.CurrentDatabase();
            }
        }

        #endregion

        #region DataDriven Members

        /// <summary>
        /// Get the Database connection status
        /// </summary>
        public virtual ResultArgs HasDatabaseConnectionEstablished
        {
            get
            {
                ResultArgs resultArgs = database.HasConnectionEstablished();
                return resultArgs;
            }
        }

        #region Update Process

        //Update Data
        public virtual ResultArgs UpdateData(DataManager dataManager)
        {
            ResultArgs resultArgs = UpdateData(dataManager, "", SQLType.SQLStatic);
            return resultArgs;
        }

        public virtual ResultArgs UpdateData(DataManager dataManager, string SQLStatement)
        {
            ResultArgs resultArgs = UpdateData(dataManager, SQLStatement, SQLType.SQLStatic);
            return resultArgs;
        }

        public virtual ResultArgs UpdateData(DataManager dataManager, string SQLStatement, SQLType sqlType)
        {
            if (SessionDB != null) { database = SessionDB; }
            ResultArgs resultArgs = null;

            try
            {
                resultArgs = database.Execute(dataManager, SQLStatement, sqlType);
                HandleNullReference(resultArgs, null);
            }
            catch (Exception e)
            {
                EndTransaction();
                HandleNullReference(resultArgs, e);
            }

            return resultArgs;
        }

        #endregion

        #region Fetching Process

        //Fetching Data
        public virtual ResultArgs FetchData(DataManager dataManager, DataSource dataSourceType)
        {
            ResultArgs resultArgs = FetchData(dataManager, dataSourceType, "", ref dataSourceOptional);
            return resultArgs;
        }

        public virtual ResultArgs FetchData(DataManager dataManager, DataSource dataSourceType, ref object dataSource)
        {
            ResultArgs resultArgs = FetchData(dataManager, dataSourceType, "", ref dataSource);
            return resultArgs;
        }

        public virtual ResultArgs FetchData(DataManager dataManager, DataSource dataSourceType, string SQLStatement)
        {
            ResultArgs resultArgs = FetchData(dataManager, dataSourceType, SQLStatement, ref dataSourceOptional);
            return resultArgs;
        }

        public virtual ResultArgs FetchData(DataManager dataManager, DataSource dataSourceType, string SQLStatement, SQLType sqlType)
        {
            ResultArgs resultArgs = FetchData(dataManager, dataSourceType, SQLStatement, sqlType, ref dataSourceOptional);
            return resultArgs;
        }

        public virtual ResultArgs FetchData(DataManager dataManager, DataSource dataSourceType, string SQLStatement, ref object dataSource)
        {
            ResultArgs resultArgs = FetchData(dataManager, dataSourceType, SQLStatement, SQLType.SQLStatic, ref dataSource);
            return resultArgs;
        }

        public virtual ResultArgs FetchData(DataManager dataManager, DataSource dataSourceType, string SQLStatement,
            SQLType sqlType, ref object dataSource)
        {
            if (SessionDB != null) { database = SessionDB; }
            ResultArgs resultArgs = null;

            try
            {
                resultArgs = database.Fetch(dataManager, dataSourceType, SQLStatement, sqlType, ref dataSource);
            }
            catch (Exception e)
            {
                EndTransaction();
                HandleNullReference(resultArgs, e);
            }

            return resultArgs;
        }

        #endregion

        #region Handle Transaction

        public virtual void BeginTransaction()
        {
            if (SessionDB != null)
            {
                //string transactionSource = SessionDB.PreviousTransactionSource;
                //throw new System.Exception("Previous BEGIN TRANSACTION was not ended properly (" + transactionSource + ")");
                database = SessionDB;
            }

            SessionDB = database;
            database.BeginTransaction();
        }

        /*public virtual void CommitTransaction()
        {
            database.CommitTransaction();
            SessionDB = null;
        }

        public virtual void RollBackTransaction()
        {
            database.RollBackTransaction();
            SessionDB = null;
        }*/

        public virtual void EndTransaction()
        {
            database = SessionDB;
            database.EndTransaction();
            SessionDB = null;
        }

        #endregion

        #endregion

        private void HandleNullReference(ResultArgs resultArgs, Exception e)
        {
            if (resultArgs == null) 
            { 
                resultArgs = new ResultArgs();
                resultArgs.Exception = new Exception("Object is Null");
            }

            if (e != null) { resultArgs.Exception = e; }
        }

        #region IDisposable Members

        public virtual void Dispose()
        {
            if (dataSourceOptional != null)
            {
                dataSourceOptional = null;
            }

            GC.SuppressFinalize(true);
        }

        #endregion
    }
}
