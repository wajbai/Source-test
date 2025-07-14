using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace ACPP.Modules.UIControls
{
    public partial class ucMailMergeOptions : UserControl
    {
        #region Declaration
        public event EventHandler ModifyTemplateClicked;
        public event EventHandler SendEmailClicked;
        public event EventHandler SendSMSClicked;
        public event EventHandler CloseClicked;
        public event EventHandler PrintLabelClicked;

        #endregion

        #region Properties
        public BarItemVisibility VisibleSendMail
        {
            get { return bbiSendMail.Visibility; }
            set { bbiSendMail.Visibility = value; }
        }

        public BarItemVisibility VisibleSendSMS
        {
            get { return bbiSendSMS.Visibility; }
            set { bbiSendSMS.Visibility = value; }
        }

        public BarItemVisibility VisibleModifyTemplate
        {
            get { return bbiModifyTemplate.Visibility; }
            set { bbiModifyTemplate.Visibility = value; }
        }

        public BarItemVisibility VisiblePrintLabel
        {
            get { return bbiPrintLabel.Visibility; }
            set { bbiPrintLabel.Visibility = value; }
        }
        #endregion

        #region Constructor
        public ucMailMergeOptions()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        #endregion

        private void bbiModifyTemplate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ModifyTemplateClicked != null)
            {
                ModifyTemplateClicked(this, e);
            }
        }

        private void bbiSendMail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SendEmailClicked != null)
            {
                SendEmailClicked(this, e);
            }
        }

        private void bbiSendSMS_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (SendSMSClicked != null)
            {
                SendSMSClicked(this, e);
            }
        }

        private void bbiClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (CloseClicked != null)
            {
                CloseClicked(this, e);
            }
        }

        private void bbiPrintLabel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (PrintLabelClicked != null)
            {
                PrintLabelClicked(this, e);
            }
        }
    }
}
