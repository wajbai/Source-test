/* Class        : SQLInterface.cs
 * Purpose      : Interface between UI and DataAccess
 * Author       : CS
 * Created on   : 19-Jul-2010
 */

using System;
using Bosco.Utility;

namespace Bosco.DAO.Data
{
    public class DataCommandArguments
    {
        private bool isDirectReplaceParameter = false;
        private object sqlCommandId = null;
        private string tableName = "";
        private DataBaseType databaseType=DataBaseType.Portal;
        private SQLAdapterType sqladaptertype = SQLAdapterType.BoscoSQL; //SQL adapter as HOSQLAdapter for DataSyn
        
        /// <summary>
        /// Get Set SQL Command Id
        /// </summary>
        public object SQLCommandId
        {
            get { return sqlCommandId; }
            set 
            { 
                sqlCommandId = value;
                this.tableName = this.Name;
            }
        }

        /// <summary>
        /// Get / Set Table Name
        /// </summary>
        public virtual string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        /// <summary>
        /// Get Full of the Identifier
        /// </summary>
        public string FullName
        {
            get
            {
                return this.sqlCommandId.GetType().FullName;
            }
        }

        /// <summary>
        /// Get Name of the Identifier
        /// </summary>
        public string Name
        {
            get
            {
                return this.sqlCommandId.GetType().Name;
            }
        }

        /// <summary>
        /// get/set Replace Parameter value into SQL statement
        /// </summary>
        public virtual bool IsDirectReplaceParameter
        {
            get { return isDirectReplaceParameter; }
            set { isDirectReplaceParameter = value; }
        }

        public virtual DataBaseType ActiveDatabaseType
        {
            get { return databaseType; }
            set { databaseType = value; }
        }

        //set SQL adapter as HOSQLAdapter for DataSyn
        public virtual SQLAdapterType ActiveSQLAdapterType
        {
            get { return sqladaptertype; }
            set { sqladaptertype = value; }
        }
    }
}
