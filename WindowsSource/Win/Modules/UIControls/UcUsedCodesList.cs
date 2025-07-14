using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ACPP.Modules.Transaction;
using Bosco.Model.Business;

namespace ACPP.Modules.UIControls
{
    public partial class UcUsedCodesList : UserControl
    {

        #region Varaible Declaration
        #endregion

        #region Constructor
        public UcUsedCodesList()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public PopupContainerControl PopupContainerControl
        {
            get { return popupContainerControl1; }
            set { popupContainerControl1 = value; }
        }
        public DevExpress.XtraGrid.GridControl gcCodelist
        {
            get { return gcUsedCodeList; }
            set { gcUsedCodeList = value; }
        }
        public string UsedCode
        {
            get { return colUsedCode.Caption; }
            set { colUsedCode.Caption = value; }
        }
        public string Name
        {
            get { return colName.Caption; }
            set { colName.Caption = value; }
        }
        #endregion

        #region Events
        #endregion

        #region Methods
        #endregion
    }
}
