using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Alerter;

using Bosco.Utility;
using Bosco.Model.UIModel;
using ACPP.Modules.Master;
using ACPP.Modules;
using ACPP.Modules.UIControls;



namespace ACPP.Modules.Master
{
    public partial class frmCostCentreAdd : frmFinanceBaseAdd
    {
        #region Event Hanlder
        public event EventHandler UpdateHeld;
        #endregion

        #region Variable Decelartion

        private int costCenterId = 0;
        ResultArgs resultArgs = null;
        DialogResult mappingDialogResult = DialogResult.Cancel;
        #endregion

        #region Properties
        private int CCId = 0;
        private int CostCenterId
        {
            get { return CCId; }
            set { CCId = value; }
        }
        #endregion

        #region constructor

        public frmCostCentreAdd()
        {
            InitializeComponent();
        }

        public frmCostCentreAdd(int CostCenterId, int ProjetId = 0)
            : this()
        {
            UcMappingCostCentre.ProjectId = ProjetId;
            UcMappingCostCentre.Id = costCenterId = CostCenterId;
            UcMappingCostCentre.FormType = MapForm.CostCentre;
        }

        #endregion

        #region Events

        /// <summary>
        /// Load the basic details of the cost center.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void frmCostCentreAdd_Load(object sender, EventArgs e)
        {
            ucCostcentre.Visible = false;
            //lblCostCenterCode.Text = "Code";
            lblCostCenterCode.Text = this.GetMessage(MessageCatalog.Master.CostCentre.COST_CENTER_CODE_CAPTION);
            SetTitle();
            LoadCostCategory();
            AssignCostCentreDetails();
            ApplyUserrights();

            if (this.AppSetting.CostCeterMapping == 1)
            {
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.Height = 200;
                this.CenterToParent();
            }

            //To set default value 
            if (costCenterId==0)
            {
                glkpCostcentreCategory.EditValue = (int)FixedAssetClass.Primary;
            }
        }

        /// <summary>
        /// Save cost center details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateCostCentreDetails())
                {
                    using (CostCentreSystem costCentreSystem = new CostCentreSystem())
                    {
                        costCentreSystem.CostCentreId = costCentreSystem.MapCostCentreId = costCenterId == 0 ? (int)AddNewRow.NewRow : costCenterId;
                        costCentreSystem.CostCentreAbbrevation = txtCode.Text.Trim().ToUpper();
                        costCentreSystem.CostCentreName = txtCostCentreName.Text.Trim();
                        //costCentreSystem.CostCentreCategoryName = glkpCostcentreCategory.Text.Trim();
                        costCentreSystem.Notes = txtmeNotes.Text.Trim();
                        costCentreSystem.CostCategoryId = this.UtilityMember.NumberSet.ToInteger(glkpCostcentreCategory.EditValue.ToString());
                        if (AppSetting.CostCeterMapping == 0)
                        {
                            costCentreSystem.dtMapCostCentre = UcMappingCostCentre.GetMappingDetails;
                        }
                        resultArgs = costCentreSystem.SaveCostCentre();
                        if (resultArgs.Success)
                        {
                            //CostCenterId = this.UtilityMember.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                            mappingDialogResult = DialogResult.OK;
                            if (costCenterId == 0)
                            {
                                UcMappingCostCentre.GridClear = true;
                                ClearControls();
                            }
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            if (UpdateHeld != null)
                                UpdateHeld(this, e);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// Set border color based on the condition.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void txtCostCentreName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtCostCentreName);
            txtCostCentreName.Text = this.UtilityMember.StringSet.ToSentenceCase(txtCostCentreName.Text.Trim());
        }

        /// <summary>
        /// Handle dirty tracking form the froms.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        /// <summary>
        /// Fires when form is closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void frmCostCentreAdd_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnMapToProject_Click(object sender, EventArgs e)
        {
            frmMapProjectLedger frmCostCentre = new frmMapProjectLedger(MapForm.CostCentre, 0);
            frmCostCentre.ShowDialog();
        }

        private void btnCostCentreNew_Click(object sender, EventArgs e)
        {
            UcMappingCostCentre.GridClear = true;
            ClearControls();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Validate cost centers.
        /// </summary>
        /// <returns></returns>

        private bool ValidateCostCentreDetails()
        {
            bool isCostCentreTrue = true;
            try
            {
                if (string.IsNullOrEmpty(txtCostCentreName.Text.Trim()))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.CostCentre.COST_CENTER_NAME_EMPTY));
                    this.SetBorderColor(txtCostCentreName);
                    isCostCentreTrue = false;
                    txtCostCentreName.Focus();
                }
                else if (string.IsNullOrEmpty(glkpCostcentreCategory.Text.Trim()))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.CostCentreCategory.COST_CENTRE_CATEGORY_EMPTY));
                    this.SetBorderColor(glkpCostcentreCategory);
                    isCostCentreTrue = false;
                    glkpCostcentreCategory.Focus();

                }
                else
                {
                    btnSave.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
            return isCostCentreTrue;
        }

        /// <summary>
        /// Assign values to the controls while at edit mode.
        /// </summary>

        private void AssignCostCentreDetails()
        {
            try
            {
                if (costCenterId != 0)
                {
                    btnCostCentreNew.Enabled = false;
                    using (CostCentreSystem costCentreSystem = new CostCentreSystem(costCenterId))
                    {

                        txtCode.Text = costCentreSystem.CostCentreAbbrevation;
                        txtCostCentreName.Text = costCentreSystem.CostCentreName;
                        txtmeNotes.Text = costCentreSystem.Notes;
                        LoadCostCategory();
                        glkpCostcentreCategory.EditValue = costCentreSystem.CostCategoryId.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// Load Cost Centre Category
        /// </summary>
        private void LoadCostCategory()
        {
            using (CostCentreSystem CostcentreSystem = new CostCentreSystem())
            {
                resultArgs = CostcentreSystem.FetchCostCentreCategory();
                this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpCostcentreCategory, resultArgs.DataSource.Table, CostcentreSystem.AppSchema.CostCentreCategory.COST_CENTRE_CATEGORY_NAMEColumn.ColumnName, CostcentreSystem.AppSchema.CostCentreCategory.COST_CENTRECATEGORY_IDColumn.ColumnName);
                // if (CostCenterId == 0) { glkpCostcentreCategory.EditValue = glkpCostcentreCategory.Properties.GetKeyValue(0); }
            }
        }


        /// <summary>
        /// clear controls based on the cost center id.
        /// </summary>

        private void ClearControls()
        {
            if (costCenterId == 0) 
            { 
                txtCode.Text = txtCostCentreName.Text = txtmeNotes.Text = string.Empty; //glkpCostcentreCategory.EditValue = null; 
            }
            txtCode.Focus();
            UcMappingCostCentre.Id = costCenterId = CostCenterId;
            UcMappingCostCentre.FormType = MapForm.CostCentre;
        }

        /// <summary>
        /// Set title based on the costcenterid
        /// </summary>

        private void SetTitle()
        {
            this.Text = costCenterId == 0 ? this.GetMessage(MessageCatalog.Master.CostCentre.COST_CENTER_ADD_CAPTION) : this.GetMessage(MessageCatalog.Master.CostCentre.COST_CENTER_EDIT_CAPTION);
            txtCode.Focus();
        }

        //private void LoadCostCentreCodes()
        //{
        //    using (CostCentreSystem costCentreSystem = new CostCentreSystem())
        //    {
        //        resultArgs = costCentreSystem.FetchCostCentreCodes();
        //        if (resultArgs.DataSource != null && resultArgs.RowsAffected > 0)
        //        {
        //            this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpAvailableCostCentreCodes, resultArgs.DataSource.Table, costCentreSystem.AppSchema.CostCentre.ABBREVATIONColumn.ColumnName, costCentreSystem.AppSchema.CostCentre.ABBREVATIONColumn.ColumnName);
        //            glkpAvailableCostCentreCodes.EditValue = glkpAvailableCostCentreCodes.Properties.GetKeyValue(0);
        //            if (costCenterId == 0)
        //                txtCode.Text = CodePredictor(glkpAvailableCostCentreCodes.Properties.GetKeyValue(0).ToString(),resultArgs.DataSource.Table);
        //        }
        //    }
        //}
        #endregion

        private void UcMappingCostCentre_ProcessGridKey(object sender, EventArgs e)
        {
            if (UcMappingCostCentre.ucGridControl.IsLastRow)
            {
                txtmeNotes.Focus();
            }
        }

        private void glkpCostcentreCategory_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void glkpCostcentreCategory_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                LoadCategory();
            }
        }

        public void LoadCategory()
        {
            //if (this.AppSetting.LockMasters == (int)YesNo.No)
            //{
            frmCostCentreCategoryAdd frmcostcentrecatAdd = new frmCostCentreCategoryAdd();
            frmcostcentrecatAdd.ShowDialog();
            if (frmcostcentrecatAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
            {
                LoadCostCategory();
                if (frmcostcentrecatAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmcostcentrecatAdd.ReturnValue.ToString()) > 0)
                {
                    glkpCostcentreCategory.EditValue = this.UtilityMember.NumberSet.ToInteger(frmcostcentrecatAdd.ReturnValue.ToString());
                }
            }
            //}
            //else
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
            //}
        }

        public void ApplyUserrights()
        {
            if (CommonMethod.ApplyUserRights((int)CostCentreCategories.CreateCostCentreCategory) > 0)
            {
                glkpCostcentreCategory.Properties.Buttons[1].Visible = false;
            }
            else
            {
                glkpCostcentreCategory.Properties.Buttons[1].Visible = true;
            }
        }

        private void glkpCostcentreCategory_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpCostcentreCategory);
        }

    }
}