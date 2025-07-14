using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Bosco.Utility;
using Bosco.Model.UIModel;
using ACPP.Modules.Master;
using ACPP.Modules;
using System.Text.RegularExpressions;
using Bosco.Model.ASSET;

namespace ACPP.Modules.Asset.Masters
{
    public partial class frmDepreciationAdd : frmBaseAdd
    {
        public frmDepreciationAdd()
        {
            InitializeComponent();
        }
        #region Event Decelaration
        public event EventHandler UpdateHeld;
        #endregion

        #region Variable Decelaration
        private int DepreciationId = 0;
        private ResultArgs resultArgs = null;
        #endregion
        //#region Cunstructor
        //public frmDepreciationAdd()
        //{
        //    InitializeComponent();
        //}

        //public frmDepreciationAdd(int DepreciationId)
        //    : this()
        //{
        //    DepreciationId = DepreciationId;
        //    AssignDepreciationDetails();
        //}
        //#endregion

        private void frmDepreciationMethods_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void btnSave_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (ValidateDepreciationDetials())
        //        {
        //            ResultArgs resultArgs = null;
        //            using (DepreciationSystem DepreciationSystem = new DepreciationSystem())
        //            {
        //                DepreciationSystem.DepreciationId=DepreciationId== 0 ? this.UtilityMember.NumberSet.ToInteger(AddNewRow.NewRow.ToString()) : DepreciationId;
        //                DepreciationSystem.DepreciationCode = txtCode.Text.Trim().ToUpper();
        //                DepreciationSystem.Name = txtName.Text.Trim();
        //                DepreciationSystem.Description = meDescription.Text.Trim();
        //                resultArgs = DepreciationSystem.SaveDepreciationDetials();
                        
        //            }
        //          }
        //        }


        //    catch (Exception ex)
        //    {
        //        MessageRender.ShowMessage(ex.ToString(), true);
        //    }
        //    finally
        //    {
        //    }
            
        //}
        //private bool ValidateDepreciationDetials()
        //{
        //    return true();
        //}

    }
}
