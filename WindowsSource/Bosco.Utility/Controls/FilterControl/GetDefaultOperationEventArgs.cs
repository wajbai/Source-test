using System;
using System.Linq;
using DevExpress.Data;
using System.Collections.Generic;
using DevExpress.Data.Filtering.Helpers;

namespace Bosco.Utility.Controls.FilterControl
{
    public class GetDefaultOperationEventArgs :EventArgs {
        public GetDefaultOperationEventArgs(IBoundProperty property, ClauseType operation) {
            fOperandProperty = property;
            fClauseType = operation;
        }

        private ClauseType fClauseType;
        public ClauseType ClauseType {
            get { return fClauseType; }
            set { fClauseType = value; }
        }

        private IBoundProperty fOperandProperty;
        public IBoundProperty OperandProperty { get { return fOperandProperty; } }
    }
}
