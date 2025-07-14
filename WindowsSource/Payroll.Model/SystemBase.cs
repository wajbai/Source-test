using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Bosco.Utility;
using Bosco.Utility.Common;
using Payroll.DAO.Schema;
using Bosco.DAO.Data;
using Bosco.Utility.ConfigSetting;

namespace Payroll.Model
{
    public class SystemBase : UserProperty
    {
        private string rowIdColumn = "";
        private string hideColumns = "";
        private string columnOrder = "";
        CommonMember utilityMember = null;
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
        protected CommonMember UtilityMember
        {
            get
            {
                if (utilityMember == null) { utilityMember = new CommonMember(); }
                return utilityMember;
            }
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
