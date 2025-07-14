using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Text.RegularExpressions;
using Bosco.Utility.Common;
using Bosco.Utility.CommonMemberSet;
using Payroll.Model.UIModel;
using Bosco.DAO.Data;
using Payroll.DAO.Schema;
using Bosco.Utility;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Mask;

namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmConstructRangeFormula : frmPayrollBase
    {
        #region Variable Declaration
        ComboSetMember objCommon = new ComboSetMember();
        private frmComponentAdd parentcondition;
        CommonMember UtilityMember = new CommonMember();
        clsPayrollComponent objpaycomp = new clsPayrollComponent();
        ResultArgs resultArgs = null;
        private string strOperation = "";
        public int ComponentId = 0;
        #endregion

        #region Constructor
        public frmConstructRangeFormula()
        {
            InitializeComponent();
        }
        public frmConstructRangeFormula(frmComponentAdd frm, string strAdd, DataTable dt)
        {
            parentcondition = frm;
            dtRangeConditions = dt;
            InitializeComponent();
            strOperation = strAdd;

        }
        public frmConstructRangeFormula(frmComponentAdd frm, int Cid, string strEdit, DataTable dt)
        {
            parentcondition = frm;
            dtRangeConditions = dt;
            InitializeComponent();
            ComponentId = Cid;
            strOperation = strEdit;
        }
        #endregion

        #region Properties
        public DataTable dtRangeConditions { get; set; }
        public int PreviousComponentId { get; set; }
        public decimal PrevousMaxvalue { get; set; }
        public int Rowindex { get; set; }
        private bool isEdit { get; set; }
        private DataTable dttemp { get; set; }
        #endregion

        #region Methods
        private void LoadComponentDetails()
        {
            objCommon.BindGridLookUpCombo(glkpSelectComponent, FetchRecords(SQLCommand.Payroll.PayrollComponentFetch), "COMPONENT", "COMPONENTID");
        }
        private object FetchRecords(object sqlQuery)
        {
            ResultArgs resultArgs = null;
            object dtCategory = null;
            using (DataManager dataManager = new DataManager(sqlQuery))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                    dtCategory = resultArgs.DataSource.Table;
            }
            return dtCategory;
        }
        private void ConstructRangeFormulaFields()
        {
            try
            {
                DataTable dtTable = new DataTable();
                dtTable.Columns.Add("LINK_COMPONENT_ID", typeof(int));
                dtTable.Columns.Add("LINK_COMPONENT", typeof(string));
                dtTable.Columns.Add("MIN_VALUE", typeof(string));
                dtTable.Columns.Add("MAX_VALUE", typeof(string));
                dtTable.Columns.Add("MAX_SLAB", typeof(string));
                //if (gvFormulaCalculation.RowCount != 0)
                //{
                //    dtRangeConditions = gcFormulaCalucaltion.DataSource as DataTable;
                //}

                gcFormulaCalucaltion.DataSource = dtTable;

            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }
        private void ClearControls()
        {
            if (gvFormulaCalculation.RowCount != 0)
            {
                glkpSelectComponent.EditValue = PreviousComponentId;
                glkpSelectComponent.Enabled = false;
                glkpSelectComponent.Properties.AllowFocused = false;
                glkpSelectComponent.Properties.ReadOnly = true;
                txtMinvalue.Text = (PrevousMaxvalue + 1).ToString();
                txtMinvalue.Properties.ReadOnly = true;
                txtMinvalue.Properties.AllowFocused = false;
                txtMinvalue.Enabled = false;
                txtMaxValue.Focus();
            }
            else
            {
                glkpSelectComponent.EditValue = txtMinvalue.Text = string.Empty;
                glkpSelectComponent.Enabled = true;
                glkpSelectComponent.Properties.AllowFocused = true;
                glkpSelectComponent.Properties.ReadOnly = false;
                txtMinvalue.Properties.ReadOnly = false;
                txtMinvalue.Properties.AllowFocused = true;
                txtMinvalue.Enabled = true;
                glkpSelectComponent.Focus();

            }
            txtMaxValue.Text = txtmaxSlab.Text = string.Empty;
        }
        private bool ValidateRangeFormula()
        {
            if (string.IsNullOrEmpty(glkpSelectComponent.EditValue.ToString()) || glkpSelectComponent.EditValue == " ")
            {
                //XtraMessageBox.Show("Component is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.RangeFormula.RANGE_FORMULA_COMPONENT_EMPTY));
                glkpSelectComponent.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtMinvalue.Text.Trim()))
            {
                //XtraMessageBox.Show("Minimum value is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.RangeFormula.RANGE_FORMULA_MIN_VALUE_EMPTY));
                this.SetBorderColor(txtMinvalue);
                txtMinvalue.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtMaxValue.Text.Trim()))
            {
                //XtraMessageBox.Show("Maximum value is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.RangeFormula.RANGE_FORMULA_MAX_VALUE_EMPTY));
                this.SetBorderColor(txtMaxValue);
                txtMaxValue.Focus();
                return false;
            }

            else if (string.IsNullOrEmpty(txtmaxSlab.Text.Trim()))
            {
                //XtraMessageBox.Show("MaxSlab is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.RangeFormula.RANGE_FORMULA_MAX_SALP_EMPTY));
                this.SetBorderColor(txtmaxSlab);
                txtmaxSlab.Focus();
                return false;
            }
            //if (!string.IsNullOrEmpty(txtMaxValue.Text) && !string.IsNullOrEmpty(txtMinvalue.Text))
            //{
            //    decimal MaxValue = UtilityMember.NumberSet.ToDecimal(txtMaxValue.Text);
            //    decimal MinValue = UtilityMember.NumberSet.ToDecimal(txtMinvalue.Text);
            //    if (MinValue > MaxValue)
            //    {
            //        XtraMessageBox.Show("Minimum value is greater than maximum value", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        SetBorderColor(txtMaxValue);
            //        txtMinvalue.Focus();
            //        return false;
            //    }
            //}
            if (!string.IsNullOrEmpty(txtMaxValue.Text) && !string.IsNullOrEmpty(txtMinvalue.Text))
            {
                decimal MaxValue = UtilityMember.NumberSet.ToDecimal(txtMaxValue.Text);
                decimal MinValue = UtilityMember.NumberSet.ToDecimal(txtMinvalue.Text);
                if (MinValue > MaxValue)
                {
                    //XtraMessageBox.Show("Maximum value should not be less than minimum value", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.RangeFormula.RANGE_FORMULA_MAX_MIN_VALID_INFO));
                    this.SetBorderColor(txtMaxValue);
                    txtMaxValue.Focus();
                    return false;
                }
            }
            return true;
        }
        private void AssignValues()
        {
            try
            {
                gcFormulaCalucaltion.DataSource = dtRangeConditions;
                // dtRangeConditions = objpaycomp.FetchRangeValues(ComponentId);
                if (gvFormulaCalculation.RowCount != 0)
                {
                    DataRow dr = gvFormulaCalculation.GetDataRow(gvFormulaCalculation.RowCount - 1);
                    if (dr != null)
                    {
                        PreviousComponentId = UtilityMember.NumberSet.ToInteger(dr["LINK_COMPONENT_ID"].ToString());
                        PrevousMaxvalue = UtilityMember.NumberSet.ToDecimal(dr["MAX_VALUE"].ToString());
                    }
                    else
                    {
                        PreviousComponentId = 0;
                        PrevousMaxvalue = 0;
                    }
                    ClearControls();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }
        private void DeleteRangeCondition()
        {
            try
            {
                if (gvFormulaCalculation.RowCount != 0)
                {
                    if (gvFormulaCalculation.FocusedRowHandle != GridControl.NewItemRowHandle)
                    {
                        //if (XtraMessageBox.Show("Are you sure to delete this record?", "Payroll", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_DELETED_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)//Are you sure to Delete?
                        {
                            if (gvFormulaCalculation.FocusedRowHandle == gvFormulaCalculation.RowCount - 1)
                            {
                                gvFormulaCalculation.DeleteRow(gvFormulaCalculation.FocusedRowHandle);
                                gvFormulaCalculation.RefreshData();
                                this.ShowSuccessMessage(this.GetMessage( MessageCatalog.Payroll.PayrollGroup.GROUP_DELETE_SUCCESS));
                            }
                            else
                            {
                                //XtraMessageBox.Show("Cannot delete, delete the following records.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.RangeFormula.RANGE_FORMULA_COMMON_DELETE_INFO));
                            }
                        }
                    }
                }
                dtRangeConditions = ((gvFormulaCalculation.DataSource) as DataView).ToTable();
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }
        private void EditRangeCondition()
        {
            try
            {
                DataRowView drv = gvFormulaCalculation.GetFocusedRow() as DataRowView;
                int rowHandle = gvFormulaCalculation.FocusedRowHandle;
                Rowindex = gvFormulaCalculation.GetVisibleIndex(rowHandle);
                glkpSelectComponent.EditValue = drv.Row["LINK_COMPONENT_ID"].ToString();
                txtMinvalue.Text = drv.Row["MIN_VALUE"].ToString();
                txtMaxValue.Text = drv.Row["MAX_VALUE"].ToString();
                txtmaxSlab.Text = drv.Row["MAX_SLAB"].ToString();

            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }

        }
        #endregion

        #region Events
        private void btnSave_Click(object sender, EventArgs e)
        {
            //if (gvFormulaCalculation.RowCount != 0)
            //{
            //    dtRangeConditions = gcFormulaCalucaltion.DataSource as DataTable;
            //    this.DialogResult = DialogResult.OK;
            //    ClearControls();
            //    this.Close();
            //}

            dtRangeConditions = gcFormulaCalucaltion.DataSource as DataTable;
            this.DialogResult = DialogResult.OK;
            ClearControls();
            this.Close();
        }

        private void frmConstructRangeFormula_Load(object sender, EventArgs e)
        {
            try
            {
                //lblComponentName.Text = "Define Range for " + parentcondition.txtComponent.Text;
                lblComponentName.Text = this.GetMessage(MessageCatalog.Payroll.RangeFormula.RANGE_FORMULA_DEFINE_RANGE_INFO) + " " + parentcondition.txtComponent.Text;
                LoadComponentDetails();
                ConstructRangeFormulaFields();
                AssignValues();
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int LinkComponentId = UtilityMember.NumberSet.ToInteger(glkpSelectComponent.EditValue.ToString());
                string ComponentName = glkpSelectComponent.Properties.GetDisplayText(glkpSelectComponent.EditValue);
                if (ValidateRangeFormula())
                {
                    ConstructRangeFormulaFields();
                    if (!isEdit)
                    {
                        dtRangeConditions.Rows.Add(LinkComponentId, ComponentName, txtMinvalue.Text, txtMaxValue.Text, txtmaxSlab.Text);
                    }
                    else
                    {
                        if (dtRangeConditions.Rows.Count > 0)
                        {
                            dtRangeConditions.Rows[Rowindex]["LINK_COMPONENT_ID"] = LinkComponentId;
                            dtRangeConditions.Rows[Rowindex]["LINK_COMPONENT"] = ComponentName;
                            dtRangeConditions.Rows[Rowindex]["MIN_VALUE"] = txtMinvalue.Text;
                            dtRangeConditions.Rows[Rowindex]["MAX_VALUE"] = txtMaxValue.Text;
                            dtRangeConditions.Rows[Rowindex]["MAX_SLAB"] = txtmaxSlab.Text;
                        }
                        else
                        {
                            dtRangeConditions.Rows.Add(LinkComponentId, ComponentName, txtMinvalue.Text, txtMaxValue.Text, txtmaxSlab.Text);
                        }
                        isEdit = false;
                    }
                    gcFormulaCalucaltion.DataSource = dtRangeConditions;
                    PreviousComponentId = LinkComponentId;
                    PrevousMaxvalue = UtilityMember.NumberSet.ToDecimal(txtMaxValue.Text.Trim());
                    ClearControls();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void rchkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteRangeCondition();
                DataRow dr = gvFormulaCalculation.GetDataRow(gvFormulaCalculation.RowCount - 1);
                if (dr != null)
                {
                    PreviousComponentId = UtilityMember.NumberSet.ToInteger(dr["LINK_COMPONENT_ID"].ToString());
                    PrevousMaxvalue = UtilityMember.NumberSet.ToDecimal(dr["MAX_VALUE"].ToString());
                }
                else
                {
                    PreviousComponentId = 0;
                    PrevousMaxvalue = 0;
                }
                ClearControls();
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void rchkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                EditRangeCondition();
                isEdit = true;
                // gvFormulaCalculation.DeleteRow(gvFormulaCalculation.FocusedRowHandle);
                gvFormulaCalculation.RefreshData();
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void txtMaxValue_Leave(object sender, EventArgs e)
        {

        }

        private void txtMinvalue_Leave(object sender, EventArgs e)
        {

        }
        private void txtMaxValue_Validating(object sender, CancelEventArgs e)
        {
            txtMaxValue.Properties.Mask.MaskType = MaskType.RegEx;
            txtMaxValue.Properties.Mask.EditMask = @"\d*\.?\d*";
        }

        private void txtMinvalue_Validating(object sender, CancelEventArgs e)
        {
            txtMinvalue.Properties.Mask.MaskType = MaskType.RegEx;
            txtMinvalue.Properties.Mask.EditMask = @"\d*\.?\d*";
        }

        private void txtmaxSlab_Validating(object sender, CancelEventArgs e)
        {
            txtmaxSlab.Properties.Mask.MaskType = MaskType.RegEx;
            txtmaxSlab.Properties.Mask.EditMask = @"\d*\.?\d*";
        }
        private void gcFormulaCalucaltion_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (gvFormulaCalculation.FocusedColumn == colDelete)
                {
                    btnSave.Focus();
                }
            }
        }

        private void glkpSelectComponent_Leave(object sender, EventArgs e)
        {
            try
            {
                int SelectedComponentId = UtilityMember.NumberSet.ToInteger(glkpSelectComponent.EditValue.ToString());
                if (SelectedComponentId == PreviousComponentId)
                {
                    txtMinvalue.Text = (PrevousMaxvalue + 1).ToString();
                    txtMinvalue.Properties.ReadOnly = true;
                }
                else
                {
                    txtMinvalue.Properties.ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }
        #endregion
    }
}