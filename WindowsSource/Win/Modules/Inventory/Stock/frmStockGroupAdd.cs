using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Bosco.Utility;
using Bosco.Model;
using Bosco.DAO.Schema;
using ACPP.Modules.Inventory.Asset;

namespace ACPP.Modules.Inventory
{
    public partial class frmStockGroupsAdd : frmFinanceBaseAdd
    {
        #region Event Decelaration
        public event EventHandler UpdateHeld;
        AppSchemaSet appSchema = new AppSchemaSet();
        #endregion

        #region Variable Decelaration
        public int GroupId { get; set; }
        private ResultArgs resultArgs = null;
        private object mode = null;
        AssetStockProduct.IGroup Igroup = null;
        //StockClassSystem assetClassSystem = new StockClassSystem();
        int ClassItemId = 0;
        FormMode FrmMode;
        #endregion

        #region Construction
        public frmStockGroupsAdd()
        {
            InitializeComponent();
        }
        public frmStockGroupsAdd(int StockGroupId, FormMode Mode)
            : this()
        {
            // mode = mod;
            //  Igroup = AssetStockFactory.GetGroupInstance(mod);
            FrmMode = Mode;
            GroupId = StockGroupId;
        }
        #endregion

        #region Events
        private void frmAssetClassAdd_Load(object sender, EventArgs e)
        {
            LoadAssetClass();
            SetTittle();
            AssignAToProperties();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateItemDetails())
                {
                    using (StockGroupSystem stockgroupsystem = new StockGroupSystem())
                    {
                        stockgroupsystem.StockGroupId = FrmMode == 0 ? 0 : this.GroupId;
                        stockgroupsystem.StockGroup = txtAssetClassName.Text.Trim();
                        stockgroupsystem.ParentGroupId = this.UtilityMember.NumberSet.ToInteger(glkpParentClassName.EditValue.ToString());
                        resultArgs = stockgroupsystem.SaveGroupDetails();
                        //if (this.GroupId == 0)
                        //{
                        //    if (resultArgs != null && resultArgs.Success && glkpParentClassName.Text.Equals(AssetCategory.Primary.ToString()))
                        //    {
                        //        stockgroupsystem.StockGroupId = stockgroupsystem.ParentGroupId = UtilityMember.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                        //        resultArgs = stockgroupsystem.SaveGroupDetails();
                        //    }
                        //}
                        if (resultArgs != null && resultArgs.Success)
                        {
                            this.GroupId = (GroupId == 0) ? UtilityMember.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : this.GroupId;
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            ClearControls();
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void glkpParentClassName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpParentClassName);
        }

        private void txtAssetClass_Leave_1(object sender, EventArgs e)
        {
            this.SetBorderColor(txtAssetClassName);
            txtAssetClassName.Text = this.UtilityMember.StringSet.ToSentenceCase(txtAssetClassName.Text.Trim());
        }

        #endregion

        #region Methods

        private bool ValidateItemDetails()
        {
            bool isClassItemTrue = true;
            if (string.IsNullOrEmpty(glkpParentClassName.Text))
            {
                this.ShowMessageBox("Parent Group Name is empty.");
                this.SetBorderColor(glkpParentClassName);
                isClassItemTrue = false;
                this.glkpParentClassName.Focus();
            }
            else if (string.IsNullOrEmpty(txtAssetClassName.Text))
            {
                this.ShowMessageBox("Group Name is empty.");
                this.SetBorderColor(txtAssetClassName);
                isClassItemTrue = false;
                this.txtAssetClassName.Focus();
            }
            return isClassItemTrue;
        }
        private bool ValidateGroupLevel()
        {
            bool IsGroupLevel = true;
            //using (AssetGroupSystem groupSystem = new AssetGroupSystem())
            //{
            //    resultArgs = groupSystem.ValidateGroupId(this.UtilityMember.NumberSet.ToInteger(glpUnderGroup.EditValue.ToString()));
            //    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            //    {
            //        if (resultArgs.DataSource.Table.Rows[0][groupSystem.AppSchema.ASSETGroupDetails.PARENT_GROUP_IDColumn.ColumnName].ToString() != resultArgs.DataSource.Table.Rows[0][groupSystem.AppSchema.LedgerGroup.NATURE_IDColumn.ColumnName].ToString())
            //        {
            //            IsGroupLevel = false;
            //        }
            //    }
            //}
            return IsGroupLevel;
        }
        private void ClearControls()
        {
            if (FrmMode == FormMode.Add)
            {
                txtAssetClassName.Text = string.Empty;
                txtAssetClassName.Focus();
            }
            else
            {
                //     this.Close();
            }
            LoadAssetClass();
            // txtAssetClassName.Focus();
        }

        public void SetTittle()
        {
            this.Text = FrmMode == FormMode.Add ? "Stock Group(Add)" : "Stock Group(Edit)";
        }

        public void LoadAssetClass()
        {
            try
            {
                using (StockGroupSystem stockgroupsystem = new StockGroupSystem())
                {
                    resultArgs = stockgroupsystem.FetchParentGroupName();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpParentClassName, resultArgs.DataSource.Table, stockgroupsystem.AppSchema.ASSETClassDetails.ASSET_CLASSColumn.ColumnName, stockgroupsystem.AppSchema.ASSETClassDetails.ASSET_CLASS_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private void AssignAToProperties()
        {
            try
            {
                if (FrmMode == FormMode.Edit)
                {
                    if (GroupId != 0)
                    {
                        using (StockGroupSystem StockGroupSystem = new StockGroupSystem())
                        {
                            StockGroupSystem.StockGroupId = GroupId;
                            StockGroupSystem.AssignGroupProperties();
                            txtAssetClassName.Text = StockGroupSystem.StockGroup;
                            glkpParentClassName.EditValue = StockGroupSystem.ParentGroupId == 1 ? 0 : StockGroupSystem.ParentGroupId;
                            if (UtilityMember.NumberSet.ToInteger(glkpParentClassName.EditValue.ToString()) == 0)
                            {
                                glkpParentClassName.EditValue = 1;
                            }
                        }
                    }
                }
                else
                {
                    glkpParentClassName.EditValue = GroupId;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            { }
        }

        #endregion

        private void glkpParentClassName_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
