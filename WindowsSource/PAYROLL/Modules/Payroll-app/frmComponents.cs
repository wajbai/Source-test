using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Utility.Common;

using System.Text.RegularExpressions;
using Payroll.Model.UIModel;
using Bosco.DAO.Data;
using Payroll.DAO.Schema;
using Bosco.Utility;

namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmComponents : DevExpress.XtraEditors.XtraForm
    {

        #region Variable Decalration
        
        private string strOperation = "";
        private string sRelatedComponents = "";
        private clsPayrollComponent objPayroll = new clsPayrollComponent();
        private clsPayrollLoan objLoan = new clsPayrollLoan();
        private clsPayrollStaff objStaff = new clsPayrollStaff();
        private clsprCompBuild objCompBuild = new clsprCompBuild();
        private clsLoanManagement objLoanMgmt = new clsLoanManagement();
        private clsPrComponent Objprcomponent = new clsPrComponent();
        private frmComponentView Parent;
        CommonMember commonmem = new CommonMember();
        private DataTable dt = new DataTable();
        public string CValue = "";	   //Component Equation Id
        public int ComponentId = 0;
        private string sCompStr = "";
        private string StrValue = "";
        private string strLinkValue = "";
        public int IFCon = 0;
        private DataView dvComponents = null;
        private string sCircularComponentName = "";
        ResultArgs resultArgs = null;
        private Regex objR = new Regex("[0-9]|\b");
        public event EventHandler UpdateHeld;
        FormMode frmmode = new FormMode();
        #endregion

        #region Constructor
        public frmComponents()
        {
            InitializeComponent();
        }

        public frmComponents(string strAdd)
        {
            InitializeComponent();
            strOperation = strAdd;
        }
        public frmComponents(int cId,string stredit)
            : this()
        {
            ComponentId = cId;
            AssignProperties();
            strOperation = stredit;
        }
        #endregion

        #region Methods
        private bool ValidateComponent()
        {
            if (txtComponent.Text == "")
            {
                XtraMessageBox.Show("Component field can't be empty !", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtComponent.Focus();
                return false;
            }
            if (cbeType.EditValue == null)
            {
                XtraMessageBox.Show("Select the type !", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbeType.Focus();
                return false;
            }
            if (rgDefaultValue.SelectedIndex == 0 && txtFixedValue.Text == "")
            {
                XtraMessageBox.Show("Fixed value can't be empty !", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFixedValue.Focus();
                return false;
            }
            if (rgDefaultValue.SelectedIndex == 1 && glkpLinkValue.EditValue == null)
            {
                XtraMessageBox.Show("Link value can't be empty !", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                glkpLinkValue.Focus();
                return false;
            }
            if (rgDefaultValue.SelectedIndex == 2 && txtEquationBuild.Text == "")
            {
                XtraMessageBox.Show("Equation can not be empty !", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnEquationBuild.Focus();
                return false;
            }

            return true;
        }

        private bool checkExistComponent()
        {
            dt = objCompBuild.CheckDuplicateComponent();
            bool isExist = false;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr[0].ToString().ToUpper() == txtComponent.Text.Trim().ToUpper())
                    {
                        return true;
                    }
                    isExist = false;
                }
                isExist = false;
            }
            return isExist;
        }

        private void ClearControls()
        {
            txtComponent.Text = "";
            txtDescription.Text = "";
            cbeType.SelectedIndex = -1;
           // cbeType.EditValue = 0;
            glkpLinkValue.EditValue = 0;
            txtFixedValue.Text = "";
            txtMaxSlab.Text = "";
            txtEquationBuild.Text = "";
            CValue = string.Empty;

        }

        private bool checkEditComponent()
        {
            dt = objCompBuild.CheckEditComponent(ComponentId);
            bool isExist = false;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr[0].ToString().ToUpper() == txtComponent.Text.Trim().ToUpper())
                    {
                        return true;
                    }
                    isExist = false;
                }
                isExist = false;
            }
            return isExist;
        }

        private void AssignValues()
        {
            string SeletedLinkValue = string.Empty;
            string LinkVlaueByIndex = string.Empty;
            if (txtFixedValue.Text.Trim() == "")
            {
                txtFixedValue.Text = "0";
            }
            if (txtDescription.Text.Trim() == "")
            {
                txtDescription.Text = "#";
            }
            if (txtEquationBuild.Text == "")
            {
                txtEquationBuild.Text = "#";
            }
            if (CValue == "")
            {
                CValue = "#";
            }
            if (txtMaxSlab.Text == "")
            {
                txtMaxSlab.Text = "0";
            }
            if (glkpLinkValue.EditValue != null)
            {
                try
                {
                    SeletedLinkValue = glkpLinkValue.EditValue.ToString();
                    strLinkValue = objCompBuild.GetLinkName(SeletedLinkValue, false);
                    strLinkValue = SeletedLinkValue;
                    LinkVlaueByIndex = glkpLinkValue.Properties.GetDisplayTextByKeyValue(commonmem.NumberSet.ToInteger(SeletedLinkValue)).ToString();

                }
                catch
                {
                }
            }
            if (glkpLinkValue.EditValue == null)
            {
                strLinkValue = "#";
            }
            sCompStr = sCompStr + "COMPONENT|" + txtComponent.Text.Trim() + "@DESCRIPTION|" + txtDescription.Text.Trim() + "@TYPE|" + cbeType.SelectedIndex.ToString();
            if (rgDefaultValue.SelectedIndex == 0)
                sCompStr = sCompStr + "@DEFVALUE|" + txtFixedValue.Text.Trim() + "@LNKVALUE|" + LinkVlaueByIndex + "@EQUATION|" + txtEquationBuild.Text.Trim() + "@EQUATIONID|" + CValue;
            if (rgDefaultValue.SelectedIndex == 1)
                sCompStr = sCompStr + "@DEFVALUE|" + txtFixedValue.Text.Trim() + "@LNKVALUE|" + LinkVlaueByIndex + "@EQUATION|" + txtEquationBuild.Text.Trim() + "@EQUATIONID|" + CValue;
            if (rgDefaultValue.SelectedIndex == 2)
            {
                sCompStr = sCompStr + "@DEFVALUE|" + txtFixedValue.Text.Trim() + "@LNKVALUE|" + LinkVlaueByIndex + "@EQUATION|" + txtEquationBuild.Text.Trim() + "@EQUATIONID|" + CValue + "@MAXSLAP|" + txtMaxSlab.Text.ToString();
            }
            else
            {
                sCompStr = sCompStr + "@MAXSLAP|" + txtMaxSlab.Text.ToString();
            }
            if (rgRoundedOption.SelectedIndex == 0)
                sCompStr = sCompStr + "@COMPROUND|" + "1";
            else if (rgRoundedOption.SelectedIndex == 1)
                sCompStr = sCompStr + "@COMPROUND|" + "2";
            else if (rgRoundedOption.SelectedIndex == 2)
                sCompStr = sCompStr + "@COMPROUND|" + "3";
            else
                sCompStr = sCompStr + "@COMPROUND|" + "0";

            sCompStr = sCompStr + "@IFCONDITION|" + IFCon.ToString();
            if (chkShowBrowse.Checked == true)
                sCompStr = sCompStr + "@DONT_SHOWINBROWSE|" + "1";
            else
                sCompStr = sCompStr + "@DONT_SHOWINBROWSE|" + "0";

            sRelatedComponents = new clsEvalExpr().BuildComponentIdFromFormula(txtEquationBuild.Text);
            sCompStr += "@RELATEDCOMPONENTS|" + (sRelatedComponents == "" ? "#" : sRelatedComponents);

        }

        private bool VerifyInterReference()
        {
            if (sRelatedComponents == "") return false;
            if (sRelatedComponents.IndexOf("ê" + ComponentId + "ê") > 0) return true;
            else return false;
        }

        private bool VerifyCircularReference()
        {
            if (sRelatedComponents == "") return false;

            if (dvComponents == null)
                dvComponents = objPayroll.getPayrollComponent().DefaultView;

            sCircularComponentName = "";
            if (VerifyFormulaReference(sRelatedComponents, txtComponent.Text))
            {
                XtraMessageBox.Show(txtComponent.Text + " circularly references with " + sCircularComponentName + " ,hence can not proceed, remove it from the formula", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Error);
                sCircularComponentName = "";
                return true;
            }
            return false;
        }

        //private bool VerifyCurrentPayrollDependency(long componentId, int payrollId)
        //{
        //   // object strQuery = objPayroll.getPayrollComponentQuery(clsPayrollConstants.PAYROLL_EDIT_VERIFY_COMP_LINK);

        //    //strQuery = strQuery.Replace("<PAYROLLID>",payrollId.ToString());
        //    //strQuery = strQuery.Replace("<COMPONENTID>",componentId.ToString());
        //    using (DataManager dataManager = new DataManager(clsPayrollConstants.PAYROLL_EDIT_VERIFY_COMP_LINK,""))
        //    {
        //        dataManager.Parameters.Add(Payrollschema.PAYROLLIDColumn, payrollId);
        //        dataManager.Parameters.Add(prComponent.COMPONENTIDColumn, componentId);
        //        resultArgs = dataManager.FetchData(DataSource.Scalar);
        //    }
        //    string componentMapped = resultArgs.DataSource.Sclar.ToString;
        //    if (componentMapped == "" || componentMapped == "0") return false;
        //    else return true;

        //}
        private bool VerifyFormulaReference(string relatedComponents, string sourceComponentName)
        {
            try
            {
                //1.If the related component list is empty it contains no reference
                if (relatedComponents == "" || relatedComponents == "#") return false;

                string[] aRelatedComp = relatedComponents.Split('ê');

                foreach (string relatedComponent in aRelatedComp)
                {
                    if (relatedComponent == "") continue;

                    if (relatedComponent == ComponentId.ToString())
                    {
                        //2.This is the one identifies if the component is the same as the current component
                        sCircularComponentName = sourceComponentName;
                        return true;
                    }

                    dvComponents.RowFilter = "[COMPONENTID]='" + relatedComponent + "'";
                    if (dvComponents.Count == 1)
                    {
                        //3.To call recursively until the current referring component finishes its life cycle
                        if (VerifyFormulaReference(dvComponents[0]["RELATEDCOMPONENTS"].ToString(), dvComponents[0]["COMPONENT"].ToString()))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }
        }

        private bool UpdateComponentChanges(string sCompStr, long componentId, long payrollId)
        {
            return objPayroll.UpdateComponentChanges(sCompStr, componentId, payrollId);
        }
        private void LoadIncomeValues()
        {
            resultArgs = objLoanMgmt.GetIncomeDetails();
            BindGridLookUpCombo(glkpLinkValue, resultArgs.DataSource.Table, "INCOME_NAME", "INCOME_ID");
        }
        private void LoadDeductionLinkValues()
        {
            resultArgs = objLoanMgmt.GetLoanDetails();
            BindGridLookUpCombo(glkpLinkValue, resultArgs.DataSource.Table, "INCOME_NAME", "INCOME_ID");
        }

        private void LoadTextLinkValues()
        {
            resultArgs = objLoanMgmt.GetTextValues();
            BindGridLookUpCombo(glkpLinkValue, resultArgs.DataSource.Table, "INCOME_NAME", "INCOME_ID");
        }

        public void BindGridLookUpCombo(DevExpress.XtraEditors.GridLookUpEdit dropDownCombo, DataTable dataSource, string listField, string valueField)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)dataSource;
            dropDownCombo.Properties.DataSource = dataSource;
            dropDownCombo.Properties.DisplayMember = listField;
            dropDownCombo.Properties.ValueMember = valueField;

        }

        private void AssignProperties()
        {
            objPayroll.GetPayrollComponentDetails(ComponentId);
            txtComponent.Text = objPayroll.Component;
            txtDescription.Text = objPayroll.Description;
            cbeType.SelectedIndex =commonmem.NumberSet.ToInteger(objPayroll.Type.ToString());
            txtFixedValue.Text = objPayroll.DefValue;
            if (objPayroll.DefValue != "0" && objPayroll.DefValue!="")
            {
                rgDefaultValue.SelectedIndex = 0;
            }
            else if (!string.IsNullOrEmpty(objPayroll.Equation) && objPayroll.Equation!=" ")
            {
                rgDefaultValue.SelectedIndex = 2;
                btnEquationBuild.Enabled = true;
            }
            else
            {
                if (objPayroll.LinkValue != "0" && objPayroll.LinkValue != "")
                {
                    glkpLinkValue.EditValue = objPayroll.LinkValueID;
                    rgDefaultValue.SelectedIndex = 1;
                }
            }
            //glkpLinkValue.Properties.GetDisplayTextByKeyValue(objPayroll.LinkValue);
            glkpLinkValue.EditValue = objPayroll.LinkValueID;
            txtEquationBuild.Text = objPayroll.Equation;
            CValue = objPayroll.EquationId;
            txtMaxSlab.Text = objPayroll.MaxSlab.ToString();
            rgRoundedOption.SelectedIndex = objPayroll.CompRound;
        }
        #endregion

        #region Events
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int isEditable;
                /* set edit type 
                 * 1 - not editable
                 * 0 - Editable
                 */
                if (chkNonEditable.Checked.Equals(true))
                {
                    isEditable = 1;
                }
                else
                {
                    isEditable = 0;
                }

                if (ValidateComponent())
                {
                    //objPayroll.ComponentId = ComponentId == 0 ? commonmem.NumberSet.ToInteger(Row.RowNew.ToString()) : ComponentId;
                    if (checkExistComponent() && (strOperation == "Add"))
                    {
                        XtraMessageBox.Show("The Component Name Exists Already!", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        ClearControls();
                        sCompStr = "";
                        txtComponent.Focus();
                        return;
                    }

                    sCompStr = sRelatedComponents = "";

                    AssignValues();

                    if (VerifyInterReference())
                    {
                        XtraMessageBox.Show(txtComponent.Text + " can not refer itself in formula !", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (VerifyCircularReference()) { return; }

                    if (ComponentId == 0)
                    {
                        if (strOperation == "Add")
                        {
                            //Save data	
                            //if (objCompBuild.SaveComponent(ComponentId, sCompStr, txtComponent.Text.Trim(), txtDescription.Text.Trim(), isEditable))
                            //{
                            //    XtraMessageBox.Show(" Component Details are saved Successfully  !", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    ClearControls();
                            //    sCompStr = "";
                            //    txtComponent.Focus();
                            //    if (UpdateHeld != null)
                            //        UpdateHeld(this, e);
                            //    return;
                            //}
                            //else
                            //{
                            //    txtEquationBuild.Text = "";
                            //    txtDescription.Text = "";
                            //    XtraMessageBox.Show("The Component Exists Already!", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //    sCompStr = "";
                            //    ClearControls();
                            //    if (UpdateHeld != null)
                            //        UpdateHeld(this, e);
                            //    return;
                            //}
                        }
                    }
                    if (ComponentId != 0)
                    {
                        if (strOperation == "Edit")
                        {
                            //if (checkEditComponent())
                            //{
                            //    XtraMessageBox.Show("The Component Exists Already!", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //    txtComponent.Text = " ";
                            //    txtComponent.Focus();
                            //    if (UpdateHeld != null)
                            //        UpdateHeld(this, e);
                            //    return;
                            //}
                            ////Edit data
                            //if (objCompBuild.UpdateComponent(ComponentId, sCompStr, txtComponent.Text.Trim(), txtDescription.Text.Trim(), isEditable))
                            //{
                            //    int payrollId = new clsPrGateWay().GetCurrentPayroll();
                            //    bool refCompUpdateStatus = false;

                            //    if (Objprcomponent.VerifyCurrentPayrollDependency(ComponentId,payrollId)) //VerifyCurrentPayrollDependency(ComponentId, payrollId)
                            //    {
                            //        if (UpdateComponentChanges(sCompStr, ComponentId, payrollId))
                            //        {
                            //            refCompUpdateStatus = true;
                            //        }
                            //    }
                            //    else
                            //        refCompUpdateStatus = true;
                            //    if (refCompUpdateStatus)
                            //        XtraMessageBox.Show("Component is Updated Successfully !", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    else
                            //        XtraMessageBox.Show("Could not Updated the component !", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //    if (UpdateHeld != null)
                            //        UpdateHeld(this, e);
                            //    return;
                            //    this.Close();
                            //    //================================
                            //}
                            //else
                            //{
                            //    XtraMessageBox.Show("This Component exists Already !", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //    txtComponent.Text = " ";
                            //    txtComponent.Focus();
                            //    if (UpdateHeld != null)
                            //        UpdateHeld(this, e);
                            //    return;
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }

        private void btnEquationBuild_Click(object sender, EventArgs e)
        {
            string[] strEquation;
            if (txtEquationBuild.Text != "")
            {
                strEquation = txtEquationBuild.Text.Trim().Split(Convert.ToChar(160));
                switch (strEquation.Length)
                {
                    case 6:
                        IFCon = 1;
                        break;
                    case 10:
                        IFCon = 2;
                        break;
                    case 8:
                        IFCon = 3;
                        break;
                    case 12:
                        IFCon = 4;
                        break;
                    case 1:
                        IFCon = 0;
                        break;
                    default:
                        IFCon = 0;
                        break;
                }
            }
            if (ComponentId != 0 && strOperation == "Edit")
            {
                frmBuildIfFormula objBuild = new frmBuildIfFormula(this, "Edit", rgDefaultValue); //by sugan
                objBuild.ShowDialog();
            }
            if (ComponentId == 0 && strOperation == "Add")
            {
                frmBuildIfFormula objAdd = new frmBuildIfFormula(this, "Add", rgDefaultValue);//by sugan
                objAdd.ShowDialog();//by sugan
            }
            rgDefaultValue.SelectedIndex = 2;
            rgDefaultValue_SelectedIndexChanged(sender, new EventArgs());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rgDefaultValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rgDefaultValue.SelectedIndex == 0)
            {
                txtMaxSlab.Text = string.Empty;
                txtEquationBuild.Text = string.Empty;
                glkpLinkValue.Enabled = false;
                glkpLinkValue.EditValue = null;
                txtFixedValue.Enabled = true;
                glkpLinkValue.BackColor = Color.White;
                btnEquationBuild.Enabled = false;
                txtEquationBuild.Enabled = false;
                txtEquationBuild.BackColor = Color.White;
                txtMaxSlab.Enabled = false;
                txtMaxSlab.BackColor = Color.White;
            }
            else if (rgDefaultValue.SelectedIndex == 1)
            {
                txtFixedValue.Text = string.Empty;
                txtEquationBuild.Text = string.Empty;
                //CValue = string.Empty;
                glkpLinkValue.Enabled = true;
                txtFixedValue.Enabled = false;
                txtEquationBuild.Enabled = false;
                txtFixedValue.BackColor = Color.White;
                txtEquationBuild.BackColor = Color.White;
                btnEquationBuild.Enabled = false;
                txtMaxSlab.Enabled = false;
                txtMaxSlab.BackColor = Color.White;
            }
            else if (rgDefaultValue.SelectedIndex == 2)
            {
                txtFixedValue.Text = string.Empty;
                txtFixedValue.Enabled = false;
                glkpLinkValue.Enabled = false;
                btnEquationBuild.Enabled = true;
                txtEquationBuild.Enabled = true;
                txtMaxSlab.Enabled = true;
                txtFixedValue.BackColor = Color.White;
                glkpLinkValue.BackColor = Color.White;
                txtMaxSlab.BackColor = Color.White;
                glkpLinkValue.EditValue = null;
            }

        }

        private void txtMaxSlab_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !objR.IsMatch(e.KeyChar.ToString());
        }
        private void txtFixedValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !objR.IsMatch(e.KeyChar.ToString());
        }

        private void txtComponent_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex objText = new Regex("([a-zA-Z 0-9 ( ) + - * /])|-|\b");
            e.Handled = !objText.IsMatch(e.KeyChar.ToString());
        }

        private void txtDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex objText = new Regex("([a-zA-Z ])|\b");
            e.Handled = !objText.IsMatch(e.KeyChar.ToString());
        }

        private void txtFixedValue_Enter(object sender, EventArgs e)
        {
            rgDefaultValue.SelectedIndex = 0;
            rgDefaultValue_SelectedIndexChanged(sender, e);
        }

        private void cbeType_Leave(object sender, EventArgs e)
        {
            //rgDefaultValue.SelectedIndex = 0;
            txtFixedValue.Focus();
            //rgDefaultValue_SelectedIndexChanged(sender, e);
        }

        private void glkpLinkValue_Enter(object sender, EventArgs e)
        {
            //rgDefaultValue.SelectedIndex = 1;
            rgDefaultValue_SelectedIndexChanged(sender, e);
        }
        private void frmComponents_Load(object sender, EventArgs e)
        {
            //cbeType.SelectedIndex = 0;
           // btnEquationBuild.Enabled = false;
            //LoadIncomeValues();
        }

        private void cbeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbeType.SelectedIndex == 0)
            {
                rgDefaultValue.Properties.Items[0].Enabled = true;
                rgDefaultValue.Properties.Items[2].Enabled = true;
                glkpLinkValue.Properties.DataSource = null;
                LoadIncomeValues();
            }
            else if (cbeType.SelectedIndex == 1)
            {
                rgDefaultValue.Properties.Items[0].Enabled = true;
                rgDefaultValue.Properties.Items[2].Enabled = true;
                glkpLinkValue.Properties.DataSource = null;
                LoadDeductionLinkValues();
            }
            else
            {
                rgDefaultValue.Properties.Items[0].Enabled = false;
                rgDefaultValue.Properties.Items[2].Enabled = false;
                rgDefaultValue.SelectedIndex = 1;
                glkpLinkValue.Properties.DataSource = null;
                LoadTextLinkValues();
            }
        }
        #endregion
    }
}