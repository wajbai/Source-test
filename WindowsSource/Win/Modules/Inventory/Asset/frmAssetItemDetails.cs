/*************************************************************************************************************************
 *                                              Purpose     : A common form to get Asset Item details of Purchase,Asset Opening Balance, 
 *                                                            Insurance and AMC
 *                                              Author      : Carmel Raj M
 *                                              Created On  : 28-October-2015
 *************************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmAssetItemDetails : frmBaseAdd
    {
        #region Variables

        #endregion

        #region Properties

        #endregion

        #region Constructor
        public frmAssetItemDetails()
        {
            InitializeComponent();
        }

        public frmAssetItemDetails(int AssetId, int ItemCount, ref decimal Amount)
            : this()
        {
            UcAssetItem.AssetId = AssetId;
            UcAssetItem.ItemCount = ItemCount;
            UcAssetItem.Amount = Amount;
        }
        #endregion

        #region Method

        #endregion

        #region Events
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}