using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace PAYROLL.Modules.Payroll_app
{
    public partial class ucComponentAllocation : DevExpress.XtraEditors.XtraUserControl
    {
        #region Variables
        public event EventHandler CreateComponentClicked;
        public event EventHandler SaveOrderClicked;
        public event EventHandler MapComponentClicked;
        #endregion

        #region Constructor
        public ucComponentAllocation()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void bbiCreate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (CreateComponentClicked != null)
            {
                CreateComponentClicked(this, e);
            }
        }
        private void bbiMapComponent_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MapComponentClicked != null)
            {
                MapComponentClicked(this, e);
            }
        }
      
        #endregion

        #region Methods
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            EventArgs e = new EventArgs();
            if (keyData == (Keys.Alt | Keys.T))
            {
                if (CreateComponentClicked != null)
                {
                    CreateComponentClicked(keyData, e);
                }
                return true;
            }
            if (keyData == (Keys.Alt | Keys.M))
            {
                if (MapComponentClicked != null)
                {
                    MapComponentClicked(this, e);
                }
                return true;
            }
            if (keyData == (Keys.Alt | Keys.S))
            {
                if (SaveOrderClicked != null)
                {
                    SaveOrderClicked(this, e);
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
    }
}
