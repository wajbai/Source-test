using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;

namespace ACPP.Modules.UIControls
{
    public partial class UcBankAccountDetails : DevExpress.XtraEditors.XtraUserControl
    {

        #region Constructor
        public UcBankAccountDetails()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public string BankName
        {
            set { lblTextBankName.Text = value; }
        }
        public string BankBranchName
        {
            set { lblTextBranchName.Text = value; }
        }
        public string BankAccountNumber
        {
            set { lblTextAccountNumber.Text = value; }
            get{return lblTextAccountNumber.Text;}
        }
        public string BankCreatedOn
        {
            set { lblTextCreatedOn.Text = value; }
            get { return lblTextCreatedOn.Text; }
        }
        public string AccountNumberCaption
        {
            set { lblAccountNumber.Text = value; }
            get { return lblAccountNumber.Text; }

        }
        public string Project
        {
            get { return lblProjectName.Text; }
            set { lblProjectName.Text = value; }
        }
        public bool VisibleBanner
        {
            set { lcgBankDetails.Visibility = value.Equals(true) ? LayoutVisibility.Always : LayoutVisibility.Never; }
        }
        #endregion

    }
}
