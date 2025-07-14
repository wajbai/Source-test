using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using Bosco.Utility;
using Bosco.Model.UIModel;
using DevExpress.XtraEditors;
using Bosco.DAO.Data;
using Bosco.Model;
using DevExpress.XtraGrid;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmRenewInsurance : frmBaseAdd
    {
        #region VariableDeclaration
        ResultArgs resultArgs = new ResultArgs();
        public event EventHandler UpdateHeld;
        public int InsuranceDetailId = 0;
        private string AssetId = string.Empty;
        private string AssetItem = string.Empty;
        private string PolicyNo = string.Empty;
        List<int> InsRenew = new List<int>();
        #endregion

        #region Constructor
        public frmRenewInsurance()
        {
            InitializeComponent();
        }
        public frmRenewInsurance(int insDetailsId, string AItem, string AId, string PoNo)
            : this()
        {
            InsuranceDetailId = insDetailsId;
            lblAssetItem.Text = AItem;
            lblAssetId.Text = AId;
            txtPolicyNo.Text = PoNo;

        }
        #endregion

        #region Properties

        private int renewalId = 0;
        private int SelectedInsRenewalId
        {
            get
            {
                renewalId = gvInsurance.GetFocusedRowCellValue(colInsDetailId) != null ? this.UtilityMember.NumberSet.ToInteger(gvInsurance.GetFocusedRowCellValue(colInsDetailId).ToString()) : 0;
                return renewalId;
            }
            set
            {
                renewalId = value;
            }
        }

        private int itemdetailid = 0;
        private int ItemDetailId
        {
            get
            {
                itemdetailid = gvInsurance.GetFocusedRowCellValue(colInsuranceItemDetailId) != null ? this.UtilityMember.NumberSet.ToInteger(gvInsurance.GetFocusedRowCellValue(colInsuranceItemDetailId).ToString()) : 0;
                return itemdetailid;
            }
            set
            {
                itemdetailid = value;
            }
        }


        #endregion

        #region Events
        /// <summary>
        /// load Insurance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRenewInsurance_Load(object sender, EventArgs e)
        {
            LoadInsurancePlan();
            LoadInsurance();
            // HideRenewDetails();
            // DefaultLoad();
            AssignValueToControls();
        }

        // <summary>
        /// Save renew Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateRenewDetails())
                {
                    using (InsuranceRenewSystem InsRenewSystem = new InsuranceRenewSystem())
                    {
                        InsRenewSystem.InsDetailId = InsuranceDetailId == 0 ? this.UtilityMember.NumberSet.ToInteger(AddNewRow.NewRow.ToString()) : InsuranceDetailId;
                        InsRenewSystem.PolicyNo = txtPolicyNo.Text.Trim();
                        InsRenewSystem.ItemDetailId = ItemDetailId;
                        InsRenewSystem.SumInsured = this.UtilityMember.NumberSet.ToDecimal(txtSumInsured.Text.Trim());
                        InsRenewSystem.PremiumAmount = this.UtilityMember.NumberSet.ToDecimal(txtPremiumAmt.Text.Trim());
                        InsRenewSystem.PeriodFrom = this.UtilityMember.DateSet.ToDate(dtPeriodFrom.Text, false);
                        InsRenewSystem.PeriodTo = this.UtilityMember.DateSet.ToDate(dtPeriodTo.Text, false);
                        resultArgs = InsRenewSystem.SaveRenewDetails();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            ClearControls();
                            LoadInsurance();
                            DefaultLoad();
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }

                        }
                        else
                        {
                            glkpInsPlan.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.ToString());
            }
            finally { }
        }

        private void HideRenewDetails()
        {
            lcgRenewalHistory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            Size = new System.Drawing.Size(500, 285);
            glkpInsPlan.Properties.PopupFormSize = new System.Drawing.Size(500, 300);

            this.CenterToScreen();
        }

        /// <summary>
        /// close the Details
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
        /// load insurace details
        /// </summary>
        private void LoadInsurancePlan()
        {
            try
            {
                using (InsuranceRenewSystem insuranceSystem = new InsuranceRenewSystem())
                {
                    resultArgs = insuranceSystem.FetchInsurancePlan();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpInsPlan, resultArgs.DataSource.Table, insuranceSystem.AppSchema.InsurancePlan.INSURANCE_PLANColumn.ColumnName, insuranceSystem.AppSchema.InsurancePlan.INSURANCE_PLAN_IDColumn.ColumnName);
                        glkpInsPlan.EditValue = glkpInsPlan.Properties.GetKeyValue(0);
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
        /// Load insurance
        /// </summary>
        private void LoadInsurance()
        {
            try
            {
                using (InsuranceRenewSystem renew = new InsuranceRenewSystem())
                {
                    resultArgs = renew.LoadInsRenewSystem();
                    if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        gcInsurance.DataSource = resultArgs.DataSource.Table;
                        gcInsurance.RefreshDataSource();
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }

        }

        private void DefaultLoad()
        {
            lblAssetItemData.Text = AssetItem;
            lblAssetIDData.Text = AssetId;
        }
        /// <summary>
        /// Validate Renew Details
        /// </summary>
        /// <returns></returns>
        private bool ValidateRenewDetails()
        {
            bool isRenewTrue = true;
            if (string.IsNullOrEmpty(glkpInsPlan.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.InsuranceRenew.INSURANCE_PLAN_EMPTY));
                isRenewTrue = false;
                this.SetBorderColorForGridLookUpEdit(glkpInsPlan);
                glkpInsPlan.Focus();
            }
            else if (string.IsNullOrEmpty(txtSumInsured.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.InsuranceRenew.INSURANCE_SUM_INSURCED_EMPTY));
                isRenewTrue = false;
                this.SetBorderColor(txtSumInsured);
                txtSumInsured.Focus();
            }
            else if (string.IsNullOrEmpty(txtPremiumAmt.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.InsuranceRenew.INSURANCE_PRIMIUM_AMOUNT_EMPTY));
                isRenewTrue = false;
                this.SetBorderColor(txtPremiumAmt);
                txtSumInsured.Focus();
            }
            else if (string.IsNullOrEmpty(dtPeriodFrom.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.InsuranceRenew.INSURANCE_PERION_FROM_EMPTY));
                isRenewTrue = false;
                this.SetBorderColor(dtPeriodFrom);
                dtPeriodFrom.Focus();
            }
            else if (string.IsNullOrEmpty(dtPeriodTo.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.InsuranceRenew.INSURANCE_PERION_TO_EMPTY));
                isRenewTrue = false;
                this.SetBorderColor(dtPeriodTo);
                dtPeriodTo.Focus();
            }
            return isRenewTrue;
        }

        /// <summary>
        /// Clear controls
        /// </summary>
        private void ClearControls()
        {
            if (InsuranceDetailId == 0)
            {
                glkpInsPlan.Text = string.Empty;
                txtPolicyNo.Text = txtSumInsured.Text = txtPremiumAmt.Text = string.Empty;
                dtRenewalDate.DateTime = dtPeriodFrom.DateTime = dtPeriodTo.DateTime = this.UtilityMember.DateSet.ToDate(DateTime.Now.ToString(), false);
                this.glkpInsPlan.Focus();
            }
            else
            {
                if (this.UIAppSetting.UITransClose == "1")
                {
                    this.Close();
                }
            }
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ripeDeleteInsuranceDetails_Click(object sender, EventArgs e)
        {
            DeleteInsHistoryDetail(SelectedInsRenewalId);
            if (UpdateHeld != null)
            {
                UpdateHeld(this, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void DeleteInsHistoryDetail(int RenewInsId)
        {
            try
            {
                using (InsuranceRenewSystem RenewSystem = new InsuranceRenewSystem())
                {
                    if (gvInsurance.RowCount > 0)
                    {
                        if (gvInsurance.FocusedRowHandle != GridControl.NewItemRowHandle)
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                gvInsurance.DeleteRow(gvInsurance.FocusedRowHandle);
                                gvInsurance.UpdateCurrentRow();
                                resultArgs = RenewSystem.DeleteInsuranceDetails(RenewInsId);
                                LoadInsurance();
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        private void rpbeInsuranceDetails_Click(object sender, EventArgs e)
        {
            if (SelectedInsRenewalId > 0)
            {
                InsuranceDetailId = SelectedInsRenewalId;
                AssignValueToControls();
            }
        }

        /// <summary>
        /// Assign value to Controls
        /// </summary>
        private void AssignValueToControls()
        {
            if (InsuranceDetailId > 0)
            {
                using (InsuranceRenewSystem RenewSystem = new InsuranceRenewSystem(InsuranceDetailId))
                {
                    RenewSystem.InsDetailId = InsuranceDetailId;
                    dtRenewalDate.DateTime = RenewSystem.RenewalDate;
                    txtPolicyNo.Text = RenewSystem.PolicyNo;
                    glkpInsPlan.EditValue = RenewSystem.InsurancePlanId.ToString();
                    txtSumInsured.Text = RenewSystem.SumInsured.ToString();
                    txtPremiumAmt.Text = RenewSystem.PremiumAmount.ToString();
                    dtPeriodFrom.DateTime = RenewSystem.PeriodFrom;
                    dtPeriodTo.DateTime = RenewSystem.PeriodTo;
                    txtPolicyNo.Text = RenewSystem.PolicyNo;
                }
            }
        }
    }
}