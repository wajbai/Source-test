using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DevExpress.XtraTreeList.Nodes.Operations;
using DevExpress.XtraTreeList.Nodes;

namespace Bosco.Utility
{
    public class TreelistIterator:TreeListOperation
    {
        private string Id;
        private string FieldName = string.Empty;
        private TreeListNode myNode;
        public TreeListNode MyNode
        {
            get { return myNode; }
            set { myNode = value; }
        }

        public TreelistIterator(string id, string fieldName)
        {
            Id = id;
            FieldName = fieldName;
        }

        public override void Execute(DevExpress.XtraTreeList.Nodes.TreeListNode node)
        {
            if (node.GetValue(FieldName).ToString() == Id)
            {
                myNode = node;
            }
        }
    }
}
