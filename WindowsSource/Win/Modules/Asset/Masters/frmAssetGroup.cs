using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ACPP.Modules.Asset.Masters
{
    public partial class frmAssetGroup : frmBaseAdd
    {
        public frmAssetGroup()
        {
            InitializeComponent();
        }

        private void glpUnderGroup_KeyDown(object sender, KeyEventArgs e)
        {
            //if(glpUnderGroup.Properties.Buttons
        }

        //private void glpUnderGroup_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
        //    {
        //        LoadGroupfrm();
        //    }
        //}

        //private void LoadGroupfrm()
        //{
        //    frmAssetGroup assetGroup = new frmAssetGroup();
        //    assetGroup.ShowDialog();
        //}

        private void glkDepreciationMethod_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                LoadDepreciationMethodfrm();
            }
        }

        private void LoadDepreciationMethodfrm()
        {
            frmDepreciationAdd depreciationmethod = new frmDepreciationAdd();
            depreciationmethod.ShowDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
