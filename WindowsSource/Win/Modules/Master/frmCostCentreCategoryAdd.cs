using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.Model.UIModel;
using System.Xml;
namespace ACPP.Modules.Master
{
    public partial class frmCostCentreCategoryAdd : frmFinanceBaseAdd
    {
        #region Events Declaration
        public event EventHandler UpdateHeld;
        #endregion

        #region Constructor
        public frmCostCentreCategoryAdd()
        {
            InitializeComponent();
        }

        public frmCostCentreCategoryAdd(int CostCategoryid)
            :this()
        {
            CostCategoryId = CostCategoryid;
        }
        #endregion

        #region Variable Declaration
        int CostCategoryId = 0;
        public string CostCentreCategory = string.Empty;
        #endregion

        #region Methods
        /// <summary>
        /// To Validate the Project Category Details
        /// </summary>
        /// <returns></returns>
        public bool ValidateCostCentreCategoryDetails()
        {
            bool isValue = true;
            if (string.IsNullOrEmpty(txtCostCategory.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.CostCentreCategory.COST_CENTRE_CATEGORY_EMPTY));
                this.SetBorderColor(txtCostCategory);
                isValue = false;
                txtCostCategory.Focus();
            }
            return isValue;
        }

        /// <summary>
        /// To Clear the Project Category Name
        /// </summary>
        private void ClearControl()
        {
            if (CostCategoryId == 0)
            {
                txtCostCategory.Text = string.Empty;
            }
             txtCostCategory.Focus();
        }

        /// <summary>
        /// To Set the Caption for Project Catogory
        /// </summary>
        private void SetTitle()
        {
            this.Text = CostCategoryId == 0 ? this.GetMessage(MessageCatalog.Master.CostCentreCategory.COST_CENTRE_CATEGORY_ADD_CAPTION) : this.GetMessage(MessageCatalog.Master.CostCentreCategory.COST_CENTRE_CATEGORY_EDIT_CAPTION);
            txtCostCategory.Focus();
        }

        /// <summary>
        /// To reload the Project Category
        /// </summary>
        public void AssignCostCentreCategoryDetails()
        {
            try
            {
                if (CostCategoryId!= 0)
                {
                    using (CostCentreCategorySystem CostCentreCategorySystem = new CostCentreCategorySystem(CostCategoryId))
                    {
                        txtCostCategory.Text = CostCentreCategorySystem.CostCentreCategoryName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
        #endregion

        #region Events
        /// <summary>
        /// To load the ProjectCategoryDetails
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCostCentreCategoryAdd_Load(object sender, EventArgs e)
        {
            SetTitle();
            AssignCostCentreCategoryDetails();
        }

        /// <summary>
        /// To Save Project Category Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateCostCentreCategoryDetails())
                {
                    ResultArgs resultArgs = null;
                    using ( CostCentreCategorySystem CostcentreCategorysystem= new CostCentreCategorySystem())
                    {
                        CostcentreCategorysystem.CostCentreCategoryId= CostCategoryId== 0 ? (int)AddNewRow.NewRow : CostCategoryId;
                        CostcentreCategorysystem.CostCentreCategoryName= txtCostCategory.Text.Trim();
                        resultArgs = CostcentreCategorysystem. SaveCostCentreCatogoryDetails();
                        if (resultArgs.Success)
                        {
                            this.ReturnValue = resultArgs.RowUniqueId;
                            this.ReturnDialog = System.Windows.Forms.DialogResult.OK;
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                            CostCentreCategory = txtCostCategory.Text.Trim();
                            ClearControl();
                            txtCostCategory.Focus();
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
        /// Close the Project Catogory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// To Set Color for Cost Centre Catogory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCostCategory_Leave(object sender, EventArgs e)
        {
            SetBorderColor(txtCostCategory);
            txtCostCategory.Text = this.UtilityMember.StringSet.ToSentenceCase(txtCostCategory.Text);
        }
        #endregion
    }
}