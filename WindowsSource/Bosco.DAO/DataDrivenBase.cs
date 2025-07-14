/* Class        : DataDrivenBase.cs
 * Purpose      : Mediator between UI and Data Access (execute/retrive data source)
 * Author       : CS
 * Created on   : 24-Jun-2010
 */

using System;
using System.Collections.Generic;
using System.Text;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Configuration;
using Bosco.Utility.ConfigSetting;

namespace Bosco.DAO
{
    public abstract class DataDrivenBase : IDisposable
    {
        private IDatabase database = null;
        private object dataSourceOptional = null;
        private static IDatabase SESSION_DB_WIN = null; //for Win App
        SettingProperty setting=new SettingProperty();

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
        public virtual ResultArgs UpdateData(DataManager dataManager, bool DontShowErrorMessage = false)
        {
            ResultArgs resultArgs = UpdateData(dataManager, "", SQLType.SQLStatic, DontShowErrorMessage);
            return resultArgs;
        }

        public virtual ResultArgs UpdateData(DataManager dataManager, string SQLStatement, bool DontShowErrorMessage = false)
        {
            ResultArgs resultArgs = UpdateData(dataManager, SQLStatement, SQLType.SQLStatic, DontShowErrorMessage);
            return resultArgs;
        }

        public virtual ResultArgs UpdateData(DataManager dataManager, string SQLStatement, SQLType sqlType, bool DontShowErrorMessage = false)
        {
            if (SessionDB != null) { database = SessionDB; }
            ResultArgs resultArgs = null;
            SetConnection();
            try
            {
                resultArgs = database.Execute(dataManager, SQLStatement, sqlType, DontShowErrorMessage);
                HandleNullReference(resultArgs, null);
            }
            catch (Exception e)
            {
                EndTransaction();
                HandleNullReference(resultArgs, e);
            }

            return resultArgs;
        }
        
        /// <summary>
        /// 
        /// *********************************** ONLY FOR BACKGROUND PROCESS **********************************************************************************
        /// On 05/07/2019, After login we get port messages in background process, 
        /// during that time if user updates or process any db realted activity which is used by begin and commit (for example update ledger details) 
        /// It blocks background process or blocks user updation
        /// 
        /// so this method will use new db connection for only for background process
        /// *********************************** ONLY FOR BACKGROUND PROCESS **********************************************************************************
        /// 
        /// </summary>
        /// <param name="dataManager"></param>
        /// <returns></returns>
        public virtual ResultArgs UpdateDataInBackgroundProcess(DataManager dataManager)
        {
            //if (SessionDB != null) { database = SessionDB; }
            database = DatabaseFactory.Instance.CurrentDatabase();
            ResultArgs resultArgs = null;
            SetConnection();
            try
            {
                resultArgs = database.Execute(dataManager, string.Empty, SQLType.SQLStatic);
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
            SetConnection();
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

        public virtual ExecutionMode TransExecutionMode
        {
            set { database.TransExecutionMode = value; }
        }

        /// <summary>
        /// force to Rollback 
        /// </summary>
        public virtual void RollBackTransaction()
        {
            database = SessionDB;
            database.RollBackTransaction();
            SessionDB = null;
        }

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

        private void SetConnection()
        {
            DataManager dm = (DataManager)this;
           
          //  ConfigurationHandler.Instance.ConnectionString = "";      //Uncomment this for Synchronization
            if (dm.DataCommandArgs.ActiveDatabaseType == DataBaseType.HeadOffice)
            {
                using (Utility.ConfigSetting.UserProperty userProperty = new Utility.ConfigSetting.UserProperty())
                {
                    ConfigurationHandler.Instance.ConnectionString = "";
                    ConfigurationHandler.Instance.ConnectionString = SettingProperty.HOBConnectionString;
                }
            }
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
