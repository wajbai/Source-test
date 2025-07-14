using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Bosco.DAO.Schema;
using Bosco.DAO.Data;
using Bosco.Utility.ConfigSetting;
using System.Resources;
using Bosco.Utility;
using System.Reflection;

namespace AcMEDSync.Model
{
    public class DsyncSystemBase : UserProperty
    {
        private string rowIdColumn = "";
        private string hideColumns = "";
        private string columnOrder = "";
        private AppSchemaSet.ApplicationSchemaSet appSchema = null;

        public DsyncSystemBase()
        {
            appSchema = new AppSchemaSet().AppSchema;
        }

        /// <summary>
        /// it is rooting the Branch Office, Head Office SQL Library. if Acme.erp Windows application 
        /// Updater want to be taken enable True if not, it means (DataSync Build) enable False for IsClientBranch Property.
        /// </summary>
        public bool IsClientBranch
        {
            get
            {
                // bool isClientBranch = true;
                bool isClientBranch = true;
                return isClientBranch;
            }
        }

        public DataBaseType DataBaseTypeName
        {
            get
            {
                DataBaseType dbType = DataBaseType.HeadOffice;

                if (IsClientBranch)
                {
                    dbType = DataBaseType.BranchOffice;
                }

                return dbType;
            }
        }

        public SQLAdapterType SQLAdapterTypeName
        {
            get
            {
                SQLAdapterType adapterType = SQLAdapterType.HOSQL;  //Always HOSQL for Balance and Export Masters and Synchronization.

                //if (IsClientBranch)
                //{
                //    adapterType = SQLAdapterType.BoscoSQL;
                //}

                return adapterType;
            }
        }

        public AppSchemaSet.ApplicationSchemaSet AppSchema
        {
            get { return appSchema; }
        }

        public string GetMessage(string keyCode)
        {
            ResourceManager resourceManger = new ResourceManager("AcMEDSync.Resources.Messages.Messages", Assembly.GetExecutingAssembly());
            string msg = "";
            try
            {
                msg = resourceManger.GetString(keyCode);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage("Resoure File is not available", false);
            }
            return msg;
        }

        public string RowIdColumn
        {
            get { return rowIdColumn; }
            set { rowIdColumn = value; }
        }

        public string HideColumns
        {
            get { return hideColumns; }
            set { hideColumns = value; }
        }

        public string ColumnOrder
        {
            get { return columnOrder; }
            set { columnOrder = value; }
        }

        #region IDisposable Members

        public override void Dispose()
        {
            if (appSchema != null)
            {
                appSchema = null;
                base.Dispose();
            }
        }

        #endregion
    }
}
