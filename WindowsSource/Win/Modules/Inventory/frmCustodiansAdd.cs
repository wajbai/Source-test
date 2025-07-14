using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bosco.Utility;
using Bosco.Model.Inventory;

namespace ACPP.Modules.Inventory
{
    public partial class frmCustodiansAdd : frmFinanceBaseAdd
    {
        #region Event Decelaration
        public event EventHandler UpdateHeld;
        #endregion

        #region Variable Decelaration
        int GroupId = 0;
        private ResultArgs resultArgs = null;
        //private object mode = null;
        //Bosco.Model.AssetStockProduct.IGroup Igroup = null;
        private int CustudiansId { get; set; }
        public int CustodianLastInsertedId { get; set; }
        int GroupItemId = 0;
        #endregion

        #region Construction
        public frmCustodiansAdd(int custudianId)
            :this()
        {
            CustudiansId = custudianId;
        }
        public frmCustodiansAdd()
        {
            InitializeComponent();
        }
        #endregion

        #region Events

        private void frmCustodians_Load(object sender, EventArgs e)
        {
            SetTittle();
            // LoadTypes();
            AssignCustodiansDetails();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateCustodian())
                {
                    using (CustodiansSystem objCustodiansSystem = new CustodiansSystem())
                    {
                        objCustodiansSystem.CustodiansId = this.CustudiansId;
                        objCustodiansSystem.Name = txtName.Text.Trim();
                        objCustodiansSystem.Role = txtRole.Text.Trim();
                        resultArgs = objCustodiansSystem.SaveCustodiansDetails();
                        if (resultArgs.Success)
                        {
                            this.ReturnValue = resultArgs.RowUniqueId;
                            this.ReturnDialog = System.Windows.Forms.DialogResult.OK;
                            
                            this.CustodianLastInsertedId = (CustudiansId == 0) ? UtilityMember.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : this.CustudiansId;
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            ClearControls();
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                        }
                        else
                        {
                            txtName.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtName);
            txtName.Text = this.UtilityMember.StringSet.ToSentenceCase(txtName.Text.Trim());
        }

        #endregion

        #region Methods
        private void AssignCustodiansDetails()
        {
            try
            {
                using (CustodiansSystem objCustudianSystem = new CustodiansSystem())
                {
                    if (this.CustudiansId != 0)
                    {
                        objCustudianSystem.AssignToProperties(CustudiansId);
                        objCustudianSystem.CustodiansId = this.CustudiansId;
                        txtName.Text = objCustudianSystem.Name;
                        txtRole.Text = objCustudianSystem.Role;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            { }
        }

        public void SetTittle()
        {
            this.Text = this.CustudiansId == 0 ? this.GetMessage(MessageCatalog.Asset.AssetCustodians.ASSETCUSTODIANS_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.AssetCustodians.ASSETCUSTODIANS_EDIT_CAPTION);
        }

        private bool ValidateCustodian()
        {
            bool iscustodianstrue = true;
            if (string.IsNullOrEmpty(txtName.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetCustodians.ASSETCUSTODIANS_NAME_EMPTY));
                txtName.Focus();
                this.SetBorderColor(txtName);
                iscustodianstrue = false;
            }
            else if (string.IsNullOrEmpty(txtRole.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetCustodians.ASSETCUSTODIANS_ROLE_EMPTY));
                txtRole.Focus();
                this.SetBorderColor(txtRole);
                iscustodianstrue = false;
            }
            return iscustodianstrue;
        }

        private void ClearControls()
        {
            if (this.CustudiansId == 0)
            {
                txtName.Text = txtRole.Text = string.Empty;
                txtName.Select();
            }
            else
            {
                //this.Close();
            }
        }

        #endregion

        private void txtRole_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtRole);
        }
    }
}
