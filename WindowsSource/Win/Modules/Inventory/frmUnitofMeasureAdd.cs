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

namespace ACPP.Modules.Inventory
{
    public partial class frmUnitofMeasureAdd : frmFinanceBaseAdd
    {
        #region Event Declearation
        public event EventHandler UpdateHeld;
        #endregion

        #region Variable Declearation
        ResultArgs resultArgs = null;
        int unitId = 0;
        #endregion

        #region Constructors
        public frmUnitofMeasureAdd()
        {
            InitializeComponent();
        }

        public frmUnitofMeasureAdd(int UnitId)
            : this()
        {
            unitId = UnitId;
        }
        #endregion

        #region Events

        private void txtSymbol_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtSymbol);
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
                    using (AssetUnitOfMeassureSystem UnitOfMeasure = new AssetUnitOfMeassureSystem())
                    {
                        UnitOfMeasure.DecimalPlace = 0;
                        UnitOfMeasure.SYMBOL = string.Empty;
                        UnitOfMeasure.NAME = string.Empty;
                        UnitOfMeasure.ConversionOf = 0;
                        UnitOfMeasure.unitId = unitId;
                        UnitOfMeasure.NAME = txtFormalName.Text;
                        UnitOfMeasure.SYMBOL = txtSymbol.Text;
                        resultArgs = UnitOfMeasure.SaveMeasureDetails();
                        if (resultArgs.Success)
                        {
                            this.ReturnValue = resultArgs.RowUniqueId;
                            this.ReturnDialog = System.Windows.Forms.DialogResult.OK;
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
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
        }

        private void frmUnitofMeasure_Load(object sender, EventArgs e)
        {
            SetTitle();
            AssignToControls();
            this.CenterToParent();
        }

        private void txtFormalName_Leave(object sender, EventArgs e)
        {
            txtFormalName.Text = this.UtilityMember.StringSet.ToSentenceCase(txtFormalName.Text.Trim());
        }

        #endregion

        #region Methods

        public void ClearControls()
        {
            if (unitId == 0)
            {
                txtFormalName.Text = txtSymbol.Text = string.Empty;
                txtSymbol.Focus();
            }
            else
            {
                //this.Close();
            }
        }

        public bool ValidateUnitOfMeassure()
        {
            bool isUnitOfMeasuretrue = true;
            if (string.IsNullOrEmpty(txtSymbol.Text))
            {
                //this.ShowMessageBox("UoM is empty");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.UnitOfMeassure.SYMBOL_EMPTY));
                txtSymbol.Focus();
                this.SetBorderColor(txtSymbol);
                isUnitOfMeasuretrue = false;
            }
            return isUnitOfMeasuretrue;
        }

        public void AssignToControls()
        {
            if (unitId > 0)
            {
                using (AssetUnitOfMeassureSystem UnitOfMeasure = new AssetUnitOfMeassureSystem())
                {
                    UnitOfMeasure.unitId = unitId;
                    UnitOfMeasure.AssignMeasureProperties();
                    txtFormalName.Text = UnitOfMeasure.NAME;
                    txtSymbol.Text = UnitOfMeasure.SYMBOL;
                }
            }
        }

        public void SetTitle()
        {
            this.Text = unitId == 0 ? this.GetMessage(MessageCatalog.Asset.UnitOfMeassure.UNIT_OF_MEASURE_ADD) : this.GetMessage(MessageCatalog.Asset.UnitOfMeassure.UNIT_OF_MEASURE_EDIT);
        }
        #endregion


    }
}
