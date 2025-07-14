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
    public partial class frmAssetCategories : frmBaseAdd
    {
        public frmAssetCategories()
        {
            InitializeComponent();
        }

        private void glkUnderGroup_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                LoadAssetGroups();
            }
        }

        private void LoadAssetGroups()
        {
            frmAssetGroup objAssetGroup = new frmAssetGroup();
            objAssetGroup.ShowDialog();
        }
    }
}
