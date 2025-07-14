using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Bosco.Utility;
using Bosco.DAO.Schema;
using Bosco.Model;

namespace ACPP.Modules.Inventory
{
    public partial class frmCategoriesAdd : frmFinanceBaseAdd
    {
        #region Variable Declearation
        public event EventHandler UpdateHeld;
        ResultArgs resultArgs = null;
        public int categoryID { get; set; }
        FormMode FrmMode;
        private FinanceModule financeModule { get; set; }
        private AssetStockProduct.ICategory Category = null;
        AppSchemaSet appSchema = new AppSchemaSet();
        #endregion

        #region Constructor
        public frmCategoriesAdd()
        {
            InitializeComponent();
        }

        public frmCategoriesAdd(int CategoryId, FinanceModule module,FormMode Mode)
            : this()
        {
            FrmMode = Mode;
            this.categoryID = CategoryId;
            this.financeModule = module;
            Category = AssetStockFactory.GetCategoryInstance(financeModule);
        }
        #endregion

        #region Events
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateAssetCaterory())
            {
                Category.CategoryId =FrmMode==0 ? 0: this.categoryID;
                Category.Name = txtName.Text;
                Category.CategoryParentId = UtilityMember.NumberSet.ToInteger(glkUnderCategory.EditValue.ToString());
                //Category.ImageId = 0;

                resultArgs = Category.SaveCategoryDetails();
                if (this.categoryID == 0)
                {
                    if (glkUnderCategory.Text.Equals(AssetCategory.Primary.ToString()))
                    {
                        Category.CategoryId = Category.CategoryParentId = UtilityMember.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                        resultArgs = Category.SaveCategoryDetails();
                    }
                }
                if (resultArgs != null && resultArgs.Success)
                {
                   // this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.categoryID = (FrmMode == 0) ? UtilityMember.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : this.categoryID;
                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                    ClearControl();
                    txtName.Focus();
                    LoadCategory();
                    if (UpdateHeld != null)
                    {
                        UpdateHeld(this, e);
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAssetCategories_Load(object sender, EventArgs e)
        {
            LoadCategory();
            AssignToControls();
            SetTitle();
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtName);
            txtName.Text = this.UtilityMember.StringSet.ToSentenceCase(txtName.Text.Trim());
        }

        private void glkUnderGroup_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkUnderCategory);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Validate asset category fields.
        /// </summary>
        /// <returns></returns>
        public bool ValidateAssetCaterory()
        {
            bool isCategorytrue = true;
            if (string.IsNullOrEmpty(txtName.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetCustodians.ASSETCUSTODIANS_NAME_EMPTY));
                txtName.Focus();
                this.SetBorderColor(txtName);
                isCategorytrue = false;
            }
            else if (string.IsNullOrEmpty(glkUnderCategory.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.UnitOfMeassure.SYMBOL_EMPTY));
                this.SetBorderColor(glkUnderCategory);
                isCategorytrue = false;
                glkUnderCategory.Focus();
            }
            return isCategorytrue;
        }

        /// <summary>
        /// load category details to Tree List
        /// </summary>
        public void LoadCategory()
        {
            resultArgs = Category.FetchCategoryDetails();
            if (resultArgs != null && resultArgs.Success)
            {
                using (CommonMethod GetMethod = new CommonMethod())
                {
                    DataTable dtCategory = GetMethod.AddHeaderColumn(resultArgs.DataSource.Table, appSchema.AppSchema.ASSETCategory.CATEGORY_IDColumn.ColumnName, appSchema.AppSchema.ASSETCategory.NAMEColumn.ColumnName, "Primary");
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkUnderCategory, dtCategory, appSchema.AppSchema.ASSETCategory.NAMEColumn.ColumnName, appSchema.AppSchema.ASSETCategory.CATEGORY_IDColumn.ColumnName);
                }
            }
        }

        /// <summary>
        /// Assign value to the controls in edit mode
        /// </summary>
        public void AssignToControls()
        {
            if (FrmMode == FormMode.Edit)
            {
                if (this.categoryID > 0)
                {
                    Category.CategoryId = this.categoryID;
                    Category.FillCategoryProperties();
                    txtName.Text = Category.Name;
                    glkUnderCategory.EditValue = Category.CategoryParentId;
                    if (categoryID == this.UtilityMember.NumberSet.ToInteger(glkUnderCategory.EditValue.ToString()))
                    {
                        glkUnderCategory.EditValue = 0;
                    }
                }
            }
            else
            {
                glkUnderCategory.EditValue = categoryID.ToString();
            }
        }

        /// <summary>
        /// Set title for form.
        /// </summary>
        public void SetTitle()
        {
            this.Text = FrmMode==FormMode.Add ? this.GetMessage(MessageCatalog.Asset.AssetCategory.ASSETCATEGORY_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.AssetCategory.ASSETCATEGORY_EDIT_CAPTION);
        }

        /// <summary>
        /// clear control values.
        /// </summary>
        public void ClearControl()
        {
            if (FrmMode == FormMode.Add)
            {
                txtName.Text = string.Empty;
               // glkUnderCategory.EditValue = null;
                glkUnderCategory.Focus();
            }
            else
            {
                //this.Close();
            }
        }

        #endregion
    }
}
