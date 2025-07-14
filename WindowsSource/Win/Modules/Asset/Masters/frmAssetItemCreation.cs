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
    public partial class frmAssetItemCreation : frmBaseAdd
    {
        public frmAssetItemCreation()
        {
            InitializeComponent();
        }

        private void glpAssetGroup_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                LoadAssetGroup();
            }
        }
        private void LoadAssetGroup()
        {
            frmAssetGroup objAssetGroup = new frmAssetGroup();
            objAssetGroup.ShowDialog();
        }

        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
