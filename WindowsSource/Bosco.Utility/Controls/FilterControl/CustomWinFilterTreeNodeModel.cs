using DevExpress.Data;
using DevExpress.XtraEditors.Filtering;

namespace Bosco.Utility.Controls.FilterControl
{
    public class CustomWinFilterTreeNodeModel :WinFilterTreeNodeModel {
        public CustomWinFilterTreeNodeModel(DevExpress.XtraEditors.FilterControl control)
            : base(control) { }

        protected override ClauseNode CreateDefaultClauseNode(IBoundProperty property) {
            ClauseNode result = base.CreateDefaultClauseNode(property);
            result.Operation = ((CustomFilterControl)Control).GetDefaultOperationCore(result.Property, result.Operation);
            return result;
        }

        public override ClauseNode CreateClauseNode() {
            return new CustomClauseNode(this);
        }
    }
}
