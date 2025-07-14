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
using Bosco.Model.TDS;
using Bosco.Utility.CommonMemberSet;

namespace ACPP.Modules.TDS
{
    public partial class frmDeducteeTypesAdd : frmFinanceBaseAdd
    {
        #region Variable Declaration
        private int DeducteeTypeId = 0;
        public event EventHandler UpdateHeld;
        #endregion

        #region Constructor
        public frmDeducteeTypesAdd()
        {
            InitializeComponent();
        }
        public frmDeducteeTypesAdd(int DeducteeId)
            : this()
        {
            DeducteeTypeId = DeducteeId;
        }
        #endregion

        #region Properties

        #endregion

        #region Events
        private void frmDeducteeTypesAdd_Load(object sender, EventArgs e)
        {
            SetTitle();
            LoadDefaults();
            AssignDefaults();

        }
        #endregion

        #region Methods
        public void LoadDefaults()
        {
            TDSResidentialStatus residential = new TDSResidentialStatus();
            TDSDeducteeStatus deductee = new TDSDeducteeStatus();
            BindEnumValue(lkpResidential, residential, EnumColumns.Name.ToString(), EnumColumns.Id.ToString(), 0);
            BindEnumValue(lkpDeducteeType, deductee, EnumColumns.Name.ToString(), EnumColumns.Id.ToString(), 0);

        }

        private void AssignDefaults()
        {

            try
            {
                if (DeducteeTypeId != 0)
                {
                    using (DeducteeTypeSystem deducteesystem = new DeducteeTypeSystem(DeducteeTypeId))
                    {
                        txtDeducteeName.Text = deducteesystem.DeducteeName;
                        lkpResidential.ItemIndex = this.UtilityMember.NumberSet.ToInteger(deducteesystem.ResidentialStatus.ToString());
                        lkpDeducteeType.ItemIndex = this.UtilityMember.NumberSet.ToInteger(deducteesystem.DeducteeStatus.ToString());
                        if (deducteesystem.status == (int)YesNo.No)
                            chkActive.Checked = false;
                        else
                            chkActive.Checked = true;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        public bool ValidateControlDetails()
        {
            bool isValue = true;
            if (string.IsNullOrEmpty(txtDeducteeName.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.TDS.DeducteeTypes.TDS_DEDUCTEE_NAME_EMPTY));
                this.SetBorderColor(txtDeducteeName);
                isValue = false;
                txtDeducteeName.Focus();
            }
            else if (this.DeducteeTypeId > 0)
            {
                if (ValidateDeducteeType() > 0 && !chkActive.Checked)
                {
                    //this.ShowMessageBox("Cannot set this inactive.Voucher is done for this Deductee.");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.TDS.DeducteeTypes.TDS_DEDUCTEE_TYPES_INACTIVE_INFO));
                    chkActive.Checked = true;
                    isValue = false;
                }
            }
            return isValue;
        }


        private int ValidateDeducteeType()
        {
            using (DeducteeTypeSystem DeducteeSystem = new DeducteeTypeSystem())
            {
                DeducteeSystem.DeducteeTypeid = this.DeducteeTypeId;
                return DeducteeSystem.ValidateDeducteeType();
            }
        }
        private void ClearControl()
        {
            txtDeducteeName.Text = string.Empty;
            chkActive.Checked = false;
            lkpDeducteeType.ItemIndex = lkpResidential.ItemIndex = 0;
            txtDeducteeName.Focus();
        }

        private void SetTitle()
        {
            this.Text = this.DeducteeTypeId == 0 ? this.GetMessage(MessageCatalog.TDS.DeducteeTypes.TDS_DEDUCTEETYPES_ADD_CAPTION) : this.GetMessage(MessageCatalog.TDS.DeducteeTypes.TDS_DEDUCTEETYPES_EDIT_CAPTION);
        }

        /// <summary>
        /// Binds enum values to LookUpEdit control
        /// </summary>
        /// <param name="lkpEdit">look up object to be binded</param>
        /// <param name="enumType">enum object</param>
        /// <param name="DisplayMember"> Display member of the lookup edit</param>
        /// <param name="ValueMember">Value member of the lookup edit</param>
        /// <param name="HidIndex">Index of the column to be hidden in the LookUpEdit</param>
        private void BindEnumValue(LookUpEdit lkpEdit, Enum enumType, string DisplayMember, string ValueMember, int HideIndex)
        {
            EnumSetMember eumSetMembers = new EnumSetMember();
            //To convert the enum type to DataSouce and binds it to the LookUpEdit
            this.UtilityMember.ComboSet.BindLookUpEditCombo(lkpEdit, eumSetMembers.GetEnumDataSource(enumType, Sorting.None).ToTable(), DisplayMember, ValueMember);
            lkpEdit.EditValue = lkpEdit.Properties.GetDataSourceValue(lkpEdit.Properties.ValueMember, 0);
        }
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateControlDetails())
                {
                    ResultArgs resultArgs = null;
                    using (DeducteeTypeSystem deducteesystem = new DeducteeTypeSystem())
                    {
                        deducteesystem.DeducteeTypeid = DeducteeTypeId == 0 ? (int)AddNewRow.NewRow : DeducteeTypeId;
                        deducteesystem.ResidentialStatus = (lkpResidential != null) ? this.UtilityMember.NumberSet.ToInteger(lkpResidential.EditValue.ToString()) : 0;
                        deducteesystem.DeducteeStatus = (lkpDeducteeType != null) ? this.UtilityMember.NumberSet.ToInteger(lkpDeducteeType.EditValue.ToString()) : 0;
                        deducteesystem.status = (chkActive.Checked) ? (int)YesNo.Yes : (int)YesNo.No;
                        deducteesystem.DeducteeName = txtDeducteeName.Text.Trim();
                        resultArgs = deducteesystem.SaveDeducteeDetails();
                        if (resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                            if (DeducteeTypeId.Equals(0))
                            {
                                ClearControl();
                            }
                            txtDeducteeName.Focus();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDeducteeName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtDeducteeName);
        }
    }
}