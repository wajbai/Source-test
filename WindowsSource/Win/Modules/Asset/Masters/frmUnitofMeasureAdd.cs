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
using Bosco.Model.Asset;
using Bosco.Model;
using Bosco.DAO.Schema;

namespace ACPP.Modules.Asset.Masters
{
    public partial class frmUnitofMeasureAdd : frmBaseAdd
    {
        #region Event Declearation
        public event EventHandler UpdateHeld;
        #endregion

        #region Variable Declearation
        ResultArgs resultArgs = null;
        int unitId = 0;
        private FinanceModule financeModule { get; set; }
        private Bosco.Model.AssetStockProduct.IMeasure Imeasure = null;
        AppSchemaSet appSchema = new AppSchemaSet();
        #endregion

        #region Constructors
        public frmUnitofMeasureAdd()
        {
            InitializeComponent();
        }

        public frmUnitofMeasureAdd(int UnitId, FinanceModule module)
            : this()
        {
            unitId = UnitId;
            this.financeModule = module;
            Imeasure = AssetStockFactory.GetUnitOfMeasureInstance(module);
        }
        #endregion

        #region Events
        private void gklpType_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(gklpType);
        }

        private void txtSymbol_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtSymbol);
        }

        private void txtNoDecimalPlace_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtNoDecimalPlace);
        }

        private void gklpFirstUnit_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(gklpFirstUnit);
        }

        private void txtConversionOf_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtConversionOf);
        }

        private void glkpSecondUnit_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpSecondUnit);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateUnitOfMeassure())
                {
                    Imeasure.DecimalPlace = 0;
                    Imeasure.SYMBOL = string.Empty;
                    Imeasure.NAME = string.Empty;
                    Imeasure.ConversionOf = 0;
                    Imeasure.FirstUnitId = string.Empty;
                    Imeasure.SecondUnitId = string.Empty;
                    Imeasure.unitId = unitId;
                    Imeasure.NAME = txtFormalName.Text;
                    Imeasure.TYPE = gklpType.Text;
                    Imeasure.TypeId = this.UtilityMember.NumberSet.ToInteger(gklpType.EditValue.ToString());
                    Imeasure.SYMBOL = txtSymbol.Text;
                    Imeasure.DecimalPlace = UtilityMember.NumberSet.ToInteger(txtNoDecimalPlace.Text);
                    Imeasure.ConversionOf = UtilityMember.NumberSet.ToInteger(txtConversionOf.Text);
                    Imeasure.FirstUnitId = gklpFirstUnit.Text;
                    Imeasure.SecondUnitId = glkpSecondUnit.Text;
                    resultArgs = Imeasure.SaveMeasureDetails();
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
                        gklpType.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
        }

        private void frmUnitofMeasure_Load(object sender, EventArgs e)
        {
            LoadUnits();
            LoadTypes();
            SetTitle();
            AssignToControls();
        }

        private void gklpType_EditValueChanged(object sender, EventArgs e)
        {
            ShowTypeGRoup();
        }

        private void glkpSecondUnit_TextChanged(object sender, EventArgs e)
        {
            if (gklpFirstUnit.EditValue.Equals(glkpSecondUnit.EditValue))
            {
                this.SetBorderColor(glkpSecondUnit);
                this.ShowMessageBoxError(this.GetMessage(MessageCatalog.Asset.UnitOfMeassure.SAME_UNIT_ERROR));
                glkpSecondUnit.Text = null;
            }
        }
        #endregion

        #region Methods
        private void LoadTypes()
        {
            UnitOfMeasureType unitofMeasureType = new UnitOfMeasureType();
            DataView UnitofMeasureType = this.UtilityMember.EnumSet.GetEnumDataSource(unitofMeasureType, Sorting.Ascending);
            this.UtilityMember.ComboSet.BindGridLookUpCombo(gklpType, UnitofMeasureType.ToTable(), "Name", "Id");
            gklpType.EditValue = gklpType.Properties.GetKeyValue(1);
        }

        public void ClearControls()
        {
            if (unitId == 0)
            {
                this.gklpFirstUnit.Text = glkpSecondUnit.Text = txtConversionOf.Text = txtFormalName.Text = txtNoDecimalPlace.Text = txtSymbol.Text = string.Empty;
            }
            else
            {
                this.Close();
            }
        }

        public bool ValidateUnitOfMeassure()
        {
            bool isUnitOfMeasuretrue = true;
            if (gklpType.EditValue == gklpType.Properties.GetKeyValue(1))
            {
                if (string.IsNullOrEmpty(gklpType.Text))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.UnitOfMeassure.TYPE_EMPTY));
                    gklpType.Focus();
                    this.SetBorderColor(gklpType);
                    isUnitOfMeasuretrue = false;
                }
                else if (string.IsNullOrEmpty(txtSymbol.Text))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.UnitOfMeassure.SYMBOL_EMPTY));
                    this.SetBorderColor(txtSymbol);
                    isUnitOfMeasuretrue = false;
                    txtSymbol.Focus();
                }
                else if (string.IsNullOrEmpty(txtNoDecimalPlace.Text))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.UnitOfMeassure.DECIMAL_PLACE_EMPTY));
                    txtNoDecimalPlace.Focus();
                    this.SetBorderColor(txtNoDecimalPlace);
                    isUnitOfMeasuretrue = false;
                }
            }
            else if (gklpType.EditValue == gklpType.Properties.GetKeyValue(2))
            {
                {
                    if (string.IsNullOrEmpty(gklpFirstUnit.Text))
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.UnitOfMeassure.FIRSTUNIT_EMPTY));
                        this.SetBorderColor(gklpFirstUnit);
                        isUnitOfMeasuretrue = false;
                        gklpFirstUnit.Focus();
                    }
                    else if (string.IsNullOrEmpty(glkpSecondUnit.Text))
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.UnitOfMeassure.SECONDUNIT_EMPTY));
                        glkpSecondUnit.Focus();
                        this.SetBorderColor(glkpSecondUnit);
                        isUnitOfMeasuretrue = false;
                    }
                    else if (string.IsNullOrEmpty(txtConversionOf.Text))
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.UnitOfMeassure.CONVERSIONOF_EMPTY));
                        txtConversionOf.Focus();
                        this.SetBorderColor(txtConversionOf);
                        isUnitOfMeasuretrue = false;
                    }
                }
            }
            return isUnitOfMeasuretrue;
        }

        public void LoadUnits()
        {
            resultArgs = Imeasure.FetchUnitsForGridLookUP();
            if (resultArgs.Success && resultArgs != null)
            {
                this.UtilityMember.ComboSet.BindGridLookUpCombo(gklpFirstUnit, resultArgs.DataSource.Table, appSchema.AppSchema.ASSETUnitOfMeassure.SYMBOLColumn.ColumnName, appSchema.AppSchema.ASSETUnitOfMeassure.SYMBOLColumn.ColumnName);
                this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpSecondUnit, resultArgs.DataSource.Table, appSchema.AppSchema.ASSETUnitOfMeassure.SYMBOLColumn.ColumnName, appSchema.AppSchema.ASSETUnitOfMeassure.SYMBOLColumn.ColumnName);
            }
        }

        private void ShowTypeGRoup()
        {
            if (this.UtilityMember.NumberSet.ToInteger(gklpType.EditValue.ToString()).Equals((int)UnitOfMeasureType.Simple))
            {
                grpSimpleGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                grpCompoundGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.Height = 241 - 71;
                this.Width = 345;
                gklpFirstUnit.Text = string.Empty;
                glkpSecondUnit.Text = string.Empty;
                txtConversionOf.Text = string.Empty;
            }

            else if (this.UtilityMember.NumberSet.ToInteger(gklpType.EditValue.ToString()).Equals((int)UnitOfMeasureType.Compound))
            {
                grpCompoundGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                grpSimpleGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.Height = 241 - 62;
                this.Width = 345;
                txtNoDecimalPlace.Text = string.Empty;
                txtSymbol.Text = string.Empty;
                txtFormalName.Text = string.Empty;
            }
        }

        public void AssignToControls()
        {
            if (unitId > 0)
            {
                Imeasure.unitId = unitId;
                Imeasure.AssignMeasureProperties();
                gklpType.EditValue = Imeasure.TYPE.ToString();
                if (gklpType.EditValue.ToString().Equals(UnitOfMeasureType.Simple.ToString()))
                {
                    ShowFormforEdit();
                    gklpType.EditValue = Imeasure.TypeId;
                    txtFormalName.Text = Imeasure.NAME;
                    txtNoDecimalPlace.Text = Imeasure.DecimalPlace.ToString();
                    txtSymbol.Text = Imeasure.SYMBOL;
                }
                else if (gklpType.EditValue.ToString().Equals(UnitOfMeasureType.Compound.ToString()))
                {
                    ShowFormforEdit();
                    gklpType.EditValue = Imeasure.TypeId;
                    glkpSecondUnit.EditValue = Imeasure.SecondUnitId;
                    gklpFirstUnit.EditValue = Imeasure.FirstUnitId;
                    txtConversionOf.Text = Imeasure.ConversionOf.ToString();
                }
            }
        }

        public void SetTitle()
        {
            this.Text = unitId == 0 ? this.GetMessage(MessageCatalog.Asset.UnitOfMeassure.UNIT_OF_MEASURE_ADD) : this.GetMessage(MessageCatalog.Asset.UnitOfMeassure.UNIT_OF_MEASURE_EDIT);
        }

        public void ShowFormforEdit()
        {
            if (gklpType.EditValue.ToString().Equals(UnitOfMeasureType.Compound.ToString()))
            {
                grpCompoundGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                grpSimpleGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.Height = 241 - 62;
                this.Width = 345;
            }
        }
        #endregion

        

        
    }
}
