using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Bosco.DAO.Schema;
using Bosco.DAO.Data;
using Bosco.Utility.ConfigSetting;

namespace Bosco.Model
{
    public class SystemBase : UserProperty
    {
        private string rowIdColumn = "";
        private string hideColumns = "";
        private string columnOrder = "";
        private AppSchemaSet.ApplicationSchemaSet appSchema = null;

        public SystemBase()
        {
            appSchema = new AppSchemaSet().AppSchema;
        }

        public AppSchemaSet.ApplicationSchemaSet AppSchema
        {
            get { return appSchema; }
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
