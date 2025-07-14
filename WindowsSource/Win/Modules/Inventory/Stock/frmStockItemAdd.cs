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
using Bosco.Model;

namespace ACPP.Modules.Inventory.Stock
{
    public partial class frmStockItemAdd : frmFinanceBaseAdd
    {
        #region Variable Declaration
        public event EventHandler UpdateHeld;
        ResultArgs resultArgs = new ResultArgs();
        int StockItemId = 0;
        FormMode FrmMode = FormMode.Add;
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
            //LoadStockCategory();
            LoadStockGroup();
            LoadStockUnit();
            LoadIncomeLedger();
            LoadExpenseLedger();
            IsLedgerCreationAllowed();
            //if (!(StockItemId > 0))
            //{
            //    AssignStockLedgers();
            //}
            AssignValuesToControls();
            this.Height = 180; //this.Height - (layoutControlItem3.Height - 1);
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
                        //stockItemSystem.CategoryId = this.UtilityMember.NumberSet.ToInteger(glkpCategory.EditValue.ToString());
                        stockItemSystem.GroupId = this.UtilityMember.NumberSet.ToInteger(glkpGroup.EditValue.ToString());
                        stockItemSystem.UnitId = this.UtilityMember.NumberSet.ToInteger(glkpUnit.EditValue.ToString());
                        stockItemSystem.ExpenseLedgerId = 0; //this.UtilityMember.NumberSet.ToInteger(glkpExpenseLedger.EditValue.ToString());
                        stockItemSystem.IncomeLedgerId = 0; //this.UtilityMember.NumberSet.ToInteger(glkpIncomeLedger.EditValue.ToString());
                        stockItemSystem.Name = txtName.Text.Trim();
                        stockItemSystem.Rate = this.UtilityMember.NumberSet.ToDecimal(txtRate.Text.Trim());
                        stockItemSystem.ReOrder = this.UtilityMember.NumberSet.ToInteger(txtReorder.Text);
                        resultArgs = stockItemSystem.SaveStockItems();
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

        private void glkpAccountLedger_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpExpenseLedger);
        }

        private void glkpDisposalLedger_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpIncomeLedger);

        }

        /// <summary>
        /// Set Name color 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtName);
            txtName.Text = this.UtilityMember.StringSet.ToSentenceCase(txtName.Text.Trim());
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
                frmCategoriesAdd category = new frmCategoriesAdd(0, FinanceModule.Stock, FrmMode);
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
                frmStockGroupsAdd groupAdd = new frmStockGroupsAdd();
                groupAdd.ShowDialog();
                groupAdd.Close();
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
                frmUnitofMeasureAdd UnitAdd = new frmUnitofMeasureAdd(0);
                UnitAdd.ShowDialog();
                LoadStockUnit();
            }
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
            //if (string.IsNullOrEmpty(glkpCategory.Text))
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Stock.StockItem.STOCK_CATEGORY_EMPTY));
            //    this.SetBorderColor(glkpCategory);
            //    isItemTrue = false;
            //    this.glkpCategory.Focus();
            //}
            if (string.IsNullOrEmpty(glkpGroup.Text))
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
            //else if (string.IsNullOrEmpty(glkpExpenseLedger.Text))
            //{
            //    this.SetBorderColor(glkpExpenseLedger);
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Stock.StockItem.EXPENSE_LEDGER_EMPTY));
            //    //this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETITEM_ACCOUNTLEDGER_EMPTY));
            //    isItemTrue = false;
            //    this.glkpExpenseLedger.Focus();
            //}
            //else if (string.IsNullOrEmpty(glkpIncomeLedger.Text))
            //{
            //    this.SetBorderColor(glkpIncomeLedger);
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Stock.StockItem.INCOME_LEDGER_EMPTY));
            //    //this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETITEM_DISPOSALLEDGER_EMPTY));
            //    isItemTrue = false;
            //    this.glkpIncomeLedger.Focus();
            //}
            return isItemTrue;
        }

        private void AssignStockLedgers()
        {
            using (StockLedgerMapping LedgerMapping = new StockLedgerMapping())
            {
                glkpExpenseLedger.EditValue = LedgerMapping.AccountLedgerId;
                glkpIncomeLedger.EditValue = LedgerMapping.DisposalLedgerId;
            }
        }

        private void LoadExpenseLedger()
        {
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                ledgerSystem.GroupId = (int)Natures.Expenses;
                resultArgs = ledgerSystem.FetchLedgerByNature();

                if (resultArgs != null && resultArgs.Success)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpExpenseLedger, resultArgs.DataSource.Table,
                        ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                }
            }
        }

        private void LoadIncomeLedger()
        {
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                ledgerSystem.GroupId = (int)Natures.Income;
                resultArgs = ledgerSystem.FetchLedgerByNature();

                if (resultArgs != null && resultArgs.Success)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpIncomeLedger, resultArgs.DataSource.Table,
                        ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                }
            }
        }

        /// <summary>
        /// Clear the Stock Details
        /// </summary>
        private void ClearControls()
        {
            if (StockItemId == 0)
            {
                txtName.Text = txtRate.Text = string.Empty;
                glkpIncomeLedger.EditValue = glkpExpenseLedger.EditValue = 0;
                glkpCategory.Focus();
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
                    resultArgs = StockGroupSystem.FetchStockGroupDetails();
                    if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs != null)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpGroup, resultArgs.DataSource.Table, StockGroupSystem.AppSchema.StockGroup.GROUP_NAMEColumn.ColumnName, StockGroupSystem.AppSchema.StockGroup.GROUP_IDColumn.ColumnName);
                        //glkpGroup.EditValue = glkpGroup.Properties.GetKeyValue(0);
                    }
                }

            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
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
                        glkpCategory.EditValue = glkpCategory.Properties.GetKeyValue(0);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        /// <summary>
        /// Load the Stock Unit Details
        /// </summary>
        private void LoadStockUnit()
        {
            try
            {
                using (AssetUnitOfMeassureSystem assetUnit = new AssetUnitOfMeassureSystem())
                {
                    resultArgs = assetUnit.FetchMeasureDetails();
                    if (resultArgs.Success && resultArgs != null)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpUnit, resultArgs.DataSource.Table, assetUnit.AppSchema.ASSETUnitOfMeassure.SYMBOLColumn.ColumnName, assetUnit.AppSchema.ASSETUnitOfMeassure.UOM_IDColumn.ColumnName);
                        glkpUnit.EditValue = glkpUnit.Properties.GetKeyValue(0);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
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
                    glkpExpenseLedger.EditValue = StockItemSystem.ExpenseLedgerId;
                    glkpIncomeLedger.EditValue = StockItemSystem.IncomeLedgerId;
                    txtName.Text = StockItemSystem.Name;
                    txtRate.Text = StockItemSystem.Rate.ToString();
                    txtReorder.Text = StockItemSystem.ReOrder.ToString();
                }
            }
        }

        private void IsLedgerCreationAllowed()
        {
            if (this.AppSetting.LockMasters == (int)YesNo.Yes)
            {
                glkpIncomeLedger.Properties.Buttons[1].Visible = false;
                glkpExpenseLedger.Properties.Buttons[1].Visible = false;
            }
        }

        #endregion

        private void glkpExpenseLedger_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                ACPP.Modules.Master.frmLedgerDetailAdd frmBank = new ACPP.Modules.Master.frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.GN);
                frmBank.ShowDialog();
                LoadExpenseLedger();
            }
        }

        private void glkpIncomeLedger_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                ACPP.Modules.Master.frmLedgerDetailAdd frmBank = new ACPP.Modules.Master.frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.GN);
                frmBank.ShowDialog();
                LoadIncomeLedger();
            }
        }
    }
}