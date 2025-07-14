using System;
using DevExpress.Data;
using DevExpress.XtraEditors.Filtering;
using DevExpress.Data.Filtering.Helpers;

namespace Bosco.Utility.Controls.FilterControl
{
    public class CustomFilterControl :DevExpress.XtraEditors.FilterControl {
        protected override WinFilterTreeNodeModel CreateModel() {
            return new CustomWinFilterTreeNodeModel(this);
        }

        void RaiseGetDefaultOperation(GetDefaultOperationEventArgs args) {
            EventHandler<GetDefaultOperationEventArgs> handler = Events[fGetDefaultOperation]
                as EventHandler<GetDefaultOperationEventArgs>;
            //if (handler != null)
            //    handler(this, args);

            switch (Type.GetTypeCode(args.OperandProperty.Type))
            {
                case TypeCode.DateTime: 
                    args.ClauseType = ClauseType.Equals; 
                    break;
                case TypeCode.Int32:
                case TypeCode.Int16:
                case TypeCode.Decimal:
                case TypeCode.Double:
                    args.ClauseType = ClauseType.Greater; 
                    break;
                 default:
                    args.ClauseType = ClauseType.Contains;
                    break;
            }
        }

        internal ClauseType GetDefaultOperationCore(IBoundProperty property, ClauseType operation) {
            GetDefaultOperationEventArgs args = new GetDefaultOperationEventArgs(property, operation);
            RaiseGetDefaultOperation(args);
            return args.ClauseType;
        }

        static readonly object fGetDefaultOperation = new object();
        public event EventHandler<GetDefaultOperationEventArgs> GetDefaultOperation {
            add { Events.AddHandler(fGetDefaultOperation, value); }
            remove { Events.RemoveHandler(fGetDefaultOperation, value); }
        }
    }
}
