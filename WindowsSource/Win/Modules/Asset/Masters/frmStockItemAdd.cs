using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Utility;

using Bosco.Model.UIModel;
using ACPP.Modules.Master;
using ACPP.Modules;
using System.Text.RegularExpressions;
using Bosco.Model.Stock;
using Bosco.Model.Asset;

namespace ACPP.Modules.Asset.Masters
{
    public partial class frmStockItemAdd : frmBaseAdd
    {
        #region Variable Declaration
        public event EventHandler UpdateHeld;
        ResultArgs resultArgs = new ResultArgs();
        int StockItemId = 0;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public frmStockItemAdd()
        {
            InitializeComponent();
        }

        public frmStockItemAdd(int ItemId)
            : this()
        {
            StockItemId = ItemId;
        }

        #endregion

        #region Events
        /// <summary>
        /// Load the Stock Add form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmStockItemAdd_Load(object sender, EventArgs e)
        {
            SetTittle();
            LoadStockCategory();
            LoadStockGroup();
            LoadStockUnit();
            AssignValuesToControls();
        }

        /// <summary>
        /// Save the Stock Item Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateStockDetails())
                {
                    ResultArgs resultArgs = null;
                    using (StockItemSystem stockItemSystem = new StockItemSystem())
                    {
                        stockItemSystem.ItemId = StockItemId == 0 ? this.UtilityMember.NumberSet.ToInteger(AddNewRow.NewRow.ToString()) : StockItemId;
                        stockItemSystem.CategoryId = this.UtilityMember.NumberSet.ToInteger(glkpCategory.EditValue.ToString());
                        stockItemSystem.GroupId = this.UtilityMember.NumberSet.ToInteger(glkpGroup.EditValue.ToString());
                        stockItemSystem.UnitId = this.UtilityMember.NumberSet.ToInteger(glkpUnit.EditValue.ToString());
                        stockItemSystem.Name = txtName.Text.Trim();
                        stockItemSystem.Quantity = this.UtilityMember.NumberSet.ToDecimal(txtQuantity.Text.Trim());
                        stockItemSystem.Rate = this.UtilityMember.NumberSet.ToDecimal(txtRate.Text.Trim());
                        stockItemSystem.Value = this.UtilityMember.NumberSet.ToDecimal(txtTotal.Text.Trim());
                        resultArgs = stockItemSystem.SaveStockItemDetails();
                        if (resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            ClearControls();
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                        }
                        else
                        {
                            glkpCategory.Focus();
                        }
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

        /// <summary>
        /// Set hte Border color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glkpCategory_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpCategory);
        }

        /// <summary>
        /// set color for group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glkpGroup_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpGroup);
        }

        /// <summary>
        /// Set Unit color 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glkpUnit_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpUnit);
        }

        /// <summary>
        /// Set Name color 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtName);
        }
        /// <summary>
        /// To view the Asset Category Add 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glkpCategory_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                frmAssetCategoriesAdd category = new frmAssetCategoriesAdd(0, FinanceModule.Stock);
                category.ShowDialog();
                LoadStockCategory();
            }
        }

        /// <summary>
        /// To view the Group Add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glkpGroup_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                frmAssetGroupAdd groupAdd = new frmAssetGroupAdd(0, FinanceModule.Stock);
                groupAdd.ShowDialog();
                LoadStockGroup();
            }
        }

        /// <summary>
        /// To view the Unit Add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glkpUnit_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                frmUnitofMeasureAdd UnitAdd = new frmUnitofMeasureAdd(0, FinanceModule.Stock);
                UnitAdd.ShowDialog();
                LoadStockUnit();
            }
        }

        /// <summary>
        /// Set color for unit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtQuantity);
        }

        /// <summary>
        /// Set Color for Rate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRate_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtRate);
            txtTotal.Text = (this.UtilityMember.NumberSet.ToInteger(txtQuantity.Text) * this.UtilityMember.NumberSet.ToInteger(txtRate.Text)).ToString();
        }

        /// <summary>
        /// Close the Stock Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Validate the Detials 
        /// </summary>
        /// <returns></returns>
        private bool ValidateStockDetails()
        {
            bool isItemTrue = true;
            if (string.IsNullOrEmpty(glkpCategory.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Stock.StockItem.STOCK_CATEGORY_EMPTY));
                this.SetBorderColor(glkpCategory);
                isItemTrue = false;
                this.glkpCategory.Focus();
            }
            else if (string.IsNullOrEmpty(glkpGroup.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Stock.StockItem.STOCK_GROUP_EMPTY));
                this.SetBorderColor(glkpGroup);
                isItemTrue = false;
                this.glkpGroup.Focus();

            }
            else if (string.IsNullOrEmpty(glkpUnit.Text))
            {
                this.SetBorderColor(glkpUnit);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Stock.StockItem.STOCK_UNIT_EMPTY));
                isItemTrue = false;
                this.glkpUnit.Focus();

            }
            else if (string.IsNullOrEmpty(txtName.Text))
            {
                this.SetBorderColor(txtName);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Stock.StockItem.STOCK_NAME_EMPTY));
                isItemTrue = false;
                this.txtName.Focus();
            }
            else if (string.IsNullOrEmpty(txtQuantity.Text))
            {
                this.SetBorderColor(txtQuantity);
                this.ShowMessageBox("Quantity is empyt");
                isItemTrue = false;
                this.txtQuantity.Focus();
            }
            else if (string.IsNullOrEmpty(txtRate.Text))
            {
                this.SetBorderColor(txtRate);
                this.ShowMessageBox("Rate is empyt");
                isItemTrue = false;
                this.txtRate.Focus();
            }
            return isItemTrue;
        }

        /// <summary>
        /// Clear the Stock Details
        /// </summary>
        private void ClearControls()
        {
            if (StockItemId == 0)
            {
                txtName.Text = txtQuantity.Text = txtRate.Text = txtTotal.Text = string.Empty;
                glkpCategory.EditValue = glkpGroup.EditValue = glkpUnit.EditValue = null;
            }
            else
            {
                this.Close();
            }
        }

        /// <summary>
        /// Set Title for particular Controls
        /// </summary>
        private void SetTittle()
        {
            this.Text = StockItemId == 0 ? this.GetMessage(MessageCatalog.Stock.StockItem.STOCK_ADD_CAPTION) : this.GetMessage(MessageCatalog.Stock.StockItem.STOCK_EDIT_CAPTION);
        }

        /// <summary>
        /// Load the Stock Group Details
        /// </summary>
        private void LoadStockGroup()
        {
            try
            {
                using (StockGroupSystem StockGroupSystem = new StockGroupSystem())
                {
                    resultArgs = StockGroupSystem.FetchGroupDetails();
                    if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs != null)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpGroup, resultArgs.DataSource.Table, StockGroupSystem.AppSchema.ASSETGroupDetails.NAMEColumn.ColumnName, StockGroupSystem.AppSchema.ASSETGroupDetails.GROUP_IDColumn.ColumnName);
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Load the Stock Category Details
        /// </summary>
        private void LoadStockCategory()
        {
            try
            {
                using (StockCategorySystem CategorySystem = new StockCategorySystem())
                {
                    resultArgs = CategorySystem.FetchCategoryDetails();
                    if (resultArgs.Success && resultArgs != null)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpCategory, resultArgs.DataSource.Table, CategorySystem.AppSchema.ASSETCategory.NAMEColumn.ColumnName, CategorySystem.AppSchema.ASSETCategory.CATEGORY_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Load the Stock Unit Details
        /// </summary>
        private void LoadStockUnit()
        {
            try
            {
                using (StockUnitOfMeassureSystem StockUnit = new StockUnitOfMeassureSystem())
                {
                    resultArgs = StockUnit.FetchMeasureDetails();
                    if (resultArgs.Success && resultArgs != null)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpUnit, resultArgs.DataSource.Table, StockUnit.AppSchema.ASSETUnitOfMeassure.NAMEColumn.ColumnName, StockUnit.AppSchema.ASSETUnitOfMeassure.UNIT_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Reassign the values to controls
        /// </summary>
        public void AssignValuesToControls()
        {
            if (StockItemId > 0)
            {
                using (StockItemSystem StockItemSystem = new StockItemSystem(StockItemId))
                {
                    glkpCategory.EditValue = StockItemSystem.CategoryId;
                    glkpGroup.EditValue = StockItemSystem.GroupId;
                    glkpUnit.EditValue = StockItemSystem.UnitId;
                    txtName.Text = StockItemSystem.Name;
                    txtQuantity.Text = StockItemSystem.Quantity.ToString();
                    txtRate.Text = StockItemSystem.Rate.ToString();
                    txtTotal.Text = StockItemSystem.Value.ToString();
                }
            }
        }

        #endregion

    }
}