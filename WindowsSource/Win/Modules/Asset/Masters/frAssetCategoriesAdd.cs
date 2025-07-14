using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Bosco.Utility;
using Bosco.Model.ASSET;
using Bosco.DAO.Schema;
using Bosco.Model.Asset;
using Bosco.Model;

namespace ACPP.Modules.Asset.Masters
{
    public partial class frmAssetCategories : frmBaseAdd
    {
        public event EventHandler updateHeld;
        ResultArgs resultArgs = null;
        private int categoryID { get; set; }
        private FinanceModule financeModule { get; set; }
        private Bosco.Model.AssetStockProduct.ICategory Category = null;
        AppSchemaSet appSchema = new AppSchemaSet();

        public frmAssetCategories()
        {
            InitializeComponent();
        }

        public frmAssetCategories(int CategoryId, FinanceModule module)
            : this()
        {
            this.categoryID = CategoryId;
            this.financeModule = module;
            Category = AssetStockFactory.GetCategoryInstance(financeModule);
        }

        private void LoadAssetGroups()
        {
            frmAssetGroupAdd objAssetGroup = new frmAssetGroupAdd();
            objAssetGroup.ShowDialog();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateAssetCaterory())
            {
                Category.CategoryId = this.categoryID;
                Category.Name = txtName.Text;
                Category.CategoryParentId = UtilityMember.NumberSet.ToInteger(glkUnderCategory.EditValue.ToString());
                Category.Image_Id = 1;

                resultArgs = Category.SaveCategoryDetails();

                if (glkUnderCategory.Text.Equals(AssetCategory.Primary.ToString()))
                {
                    Category.CategoryId = Category.CategoryParentId = UtilityMember.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                    resultArgs = Category.SaveCategoryDetails();
                }

                if (resultArgs != null && resultArgs.Success)
                {
                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                    ClearControl();
                    if (updateHeld != null)
                    {
                        updateHeld(this, e);
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


        public bool ValidateAssetCaterory()
        {
            bool isCategorytrue = true;
            if (string.IsNullOrEmpty(txtName.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.UnitOfMeassure.TYPE_EMPTY));
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

        public void LoadCategory()
        {
            resultArgs = Category.FetchCategoryDetails();
            if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs != null)
            {
                this.UtilityMember.ComboSet.BindGridLookUpCombo(glkUnderCategory, resultArgs.DataSource.Table, appSchema.AppSchema.ASSETCategory.NAMEColumn.ColumnName, appSchema.AppSchema.ASSETCategory.CATEGORY_IDColumn.ColumnName);
                glkUnderCategory.EditValue = glkUnderCategory.Properties.GetKeyValue(0);
            }
        }

        public void AssignToControls()
        {
            if (this.categoryID > 0)
            {
                Category.CategoryId = this.categoryID;
                Category.FillCategoryProperties();
                txtName.Text = Category.Name;
                glkUnderCategory.EditValue = Category.CategoryParentId; //UtilityMember.NumberSet.ToInteger(glkUnderGroup.EditValue.ToString());
            }
        }


        public void SetTitle()
        {
            this.Text = this.categoryID == 0 ? this.GetMessage(MessageCatalog.Asset.AssetCategory.ASSETCATEGORY_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.AssetCategory.ASSETCATEGORY_EDIT_CAPTION);
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtName);
        }

        private void glkUnderGroup_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkUnderCategory);
        }

        public void ClearControl()
        {
            if (categoryID == 0)
            {
                txtName.Text = string.Empty;
                glkUnderCategory.EditValue = null;
            }
        }
    }
}
