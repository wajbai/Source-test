using System;
using System.Collections;
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


namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmBuildIfFormula : frmPayrollBase
    {
        #region Declaration
        private string strCheck = "";
        // private RadioButton rdoActiveButton = new RadioButton();
        public frmComponents Parent;
        ComboSetMember objCommon = new ComboSetMember();
        private Regex objR = new Regex("[0-9]|\b");
        private string ActiveControl = "";
        private string strOption = "";
        private string strOperator1 = "";
        public string ComponentValue1 = "";
        public string ComponentValue2 = "";
        private string strLogical = "";
        public int Comp = 1;	//1- To build the equation for formula(comp1), 2- To build the equation for comp(2)
        private long CompId = 0;
        private string strOperator = "";
        public Hashtable htFormula = new Hashtable();
        private Hashtable htID = new Hashtable();
        private clsPayrollComponent objComp = new clsPayrollComponent();
        private bool newFormula = true;
        private string frmmode = string.Empty;
        #endregion

        #region Constructor
        public frmBuildIfFormula()
        {
            InitializeComponent();
        }

        public frmBuildIfFormula(frmComponents frm, string strOperation, RadioGroup rdoActiveControl)
        {

            InitializeComponent();
            frmmode = strOperation;
            Parent = frm;
            lblComponentName.Text = Parent.txtComponent.Text + " =";
            lblComponentName1.Text = Parent.txtComponent.Text + " =";
            //rdoActiveButton = rdoActiveControl;

            strCheck = strOperation;
        }
        #endregion

        #region Methods
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

        private void SelectIFCondition(int ifOption)
        {
            switch (ifOption)
            {
                case 1:
                    rdoIfcondition.SelectedIndex = 0;
                    break;
                case 2:
                    rdoIfcondition.SelectedIndex = 1;
                    break;
                case 3:
                    rdoIfcondition.SelectedIndex = 2;
                    lblComponentName.Text = txtComponent.Text + " =";
                    lblComponentName1.Text = txtComponent.Text + " =";
                    break;
                case 4:
                    rdoIfcondition.SelectedIndex = 3;
                    lblComponentName.Text = txtComponent.Text + " =";
                    lblComponentName1.Text = txtComponent.Text + " =";
                    break;
                case 0:
                    rdoIfcondition.SelectedIndex = 4;
                    break;
            }
        }

        private void DisableControls()
        {
            lblIf.Visible = false;
            txtComponent.Visible = false;
            txtOperator1.Visible = false;
            txtValue1.Visible = false;
            txtLogicalOperator.Visible = false;
            lblComponentName.Visible = true;
            txtFormula1.Visible = true;
            btnBuildFormula1.Visible = true;
            txtComponent1.Visible = false;
            txtOperator2.Visible = false;
            txtValue2.Visible = false;
            //lblThen.Visible = false;
            lblElse.Visible = false;
            lblComponentName1.Visible = false;
            btnBuildFormula2.Visible = false;
            txtFormula2.Visible = false;
            //lblEndIf.Visible = false;
            lbComponent.Visible = false;
            lstbOperator1.Visible = false;
            lstbOperator2.Visible = false;
            lbLogicalOperator.Visible = false;

            txtComponent.BackColor = Color.White;
            txtOperator1.BackColor = Color.White;
            txtLogicalOperator.BackColor = Color.White;
            txtFormula1.BackColor = Color.White;
            txtComponent1.BackColor = Color.White;
            txtOperator2.BackColor = Color.White;
            txtFormula2.BackColor = Color.White;

        }

        private void checkIfThen()
        {
            if ((txtComponent.Text.Trim() == "{COMPONENT1}") || (txtComponent.Text.Trim() == ""))
            {
                //XtraMessageBox.Show("Componenet field can't be empty !", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                XtraMessageBox.Show(this.GetMessage(MessageCatalog.Payroll.BuildFormula.BUILD_FORMULA_COMPONENT1_FIELD_EMPTY),this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtComponent.Focus();
                return;
            }
            if ((txtOperator1.Text.Trim() == "{OPR1}") || (txtOperator1.Text.Trim() == ""))
            {
                //XtraMessageBox.Show("Operator field can't be empty !", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                XtraMessageBox.Show(this.GetMessage(MessageCatalog.Payroll.BuildFormula.BUILD_FORMULA_OPERATOR1_FIELD_EMPTY), this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOperator1.Focus();
                return;
            }
            if ((txtValue1.Text.Trim() == "{VALUE1}") || (txtValue1.Text.Trim() == ""))
            {
                //XtraMessageBox.Show("Value field can't be empty !", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                XtraMessageBox.Show(this.GetMessage(MessageCatalog.Payroll.BuildFormula.BUILD_FORMULA_VALUE1_FIELD_EMPTY), this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtValue1.Focus();
                return;
            }
            if (txtFormula1.Text == "")
            {
                //XtraMessageBox.Show("Formula Can not be Empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                XtraMessageBox.Show(this.GetMessage(MessageCatalog.Payroll.BuildFormula.BUILD_FORMULA_FIELD_EMPTY), this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnBuildFormula1.Focus();
                return;
            }

        }
        private bool ValidateIfThen()
        {
            if ((txtComponent.Text.Trim() == "{COMPONENT1}") || (txtComponent.Text.Trim() == ""))
            {
                txtComponent.Focus();
                return false;
            }
            if ((txtOperator1.Text.Trim() == "{OPR1}") || (txtOperator1.Text.Trim() == ""))
            {
                txtOperator1.Focus();
                return false;
            }
            if ((txtValue1.Text.Trim() == "{VALUE1}") || (txtValue1.Text.Trim() == ""))
            {
                txtValue1.Focus();
                return false;
            }
            if (txtFormula1.Text.Trim() == "")
            {
                btnBuildFormula1.Focus();
                return false;
            }
            return true;

        }
        private void checkIfAndThen()
        {
            if ((txtComponent1.Text.Trim() == "{COMPONENT2}") || (txtComponent1.Text.Trim() == ""))
            {
                //XtraMessageBox.Show("Componenet2 field can't be empty !", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                XtraMessageBox.Show(this.GetMessage(MessageCatalog.Payroll.BuildFormula.BUILD_FORMULA_COMPONENT2_FIELD_EMPTY), this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtComponent1.Focus();
                return;
            }
            if ((txtOperator2.Text.Trim() == "{OPR2}") || (txtOperator2.Text.Trim() == ""))
            {
                //XtraMessageBox.Show("Operator field can't be empty !", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                XtraMessageBox.Show(this.GetMessage(MessageCatalog.Payroll.BuildFormula.BUILD_FORMULA_OPERATOR2_FIELD_EMPTY), this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOperator2.Focus();
                return;
            }
            if ((txtValue2.Text.Trim() == "{VALUE2}") || (txtValue2.Text.Trim() == ""))
            {
                //XtraMessageBox.Show("Value field can't be empty !", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                XtraMessageBox.Show(this.GetMessage(MessageCatalog.Payroll.BuildFormula.BUILD_FORMULA_VALUE2_FIELD_EMPTY), this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtValue2.Focus();
                return;
            }
        }
        private bool ValidateIfAndThen()
        {
            if ((txtComponent1.Text.Trim() == "{COMPONENT2}") || (txtComponent1.Text.Trim() == ""))
            {
                txtComponent1.Focus();
                return false;
            }
            if ((txtOperator2.Text.Trim() == "{OPR2}") || (txtOperator2.Text.Trim() == ""))
            {
                txtOperator2.Focus();
                return false;
            }
            if ((txtValue2.Text.Trim() == "{VALUE2}") || (txtValue2.Text.Trim() == ""))
            {
                txtValue2.Focus();
                return false;
            }
            return true;
        }

        private bool validateFields()
        {

            if (rdoIfcondition.SelectedIndex == 4)
            {
                if (txtFormula1.Text.Trim() == "")
                    return false;
                return true;
            }
            else if (rdoIfcondition.SelectedIndex == 0)
            {
                if (ValidateIfThen())
                    return true;
                return false;
            }
            else if (rdoIfcondition.SelectedIndex == 1)
            {
                if (ValidateIfThen() & ValidateIfAndThen())
                    return true;
                return false;
            }
            else if (rdoIfcondition.SelectedIndex == 2)
            {
                if (ValidateIfThen() & (txtFormula2.Text != ""))
                    return true;
                return false;
            }
            else
            {
                if (ValidateIfThen() & ValidateIfAndThen() & txtFormula2.Text != "")
                    return true;
                return false;
            }
        }

        private void FillFormula()
        {
            string formulaGroup = "";
            string[] aformulaGroup;
            string formulaGroupId = "";
            string[] aformulaGroupId;
            string formula = "";

            formulaGroup = Parent.txtEquationBuild.Text;
            formulaGroupId = Parent.CValue;

            aformulaGroup = formulaGroup.Split('$');
            aformulaGroupId = formulaGroupId.Split('$');

            if (formulaGroup != "")
            {
                for (int i = 0; i < aformulaGroup.Length; i++)
                {
                    formula = aformulaGroup[i] + "$" + aformulaGroupId[i];
                    lstFormula.Items.Add(formula);
                    if (frmmode == "Add")
                    {
                        txtFormula1.Text = formula;
                    }
                    else
                    {
                        txtFormula1.Text = "";
                    }

                }
            }
        }

        private bool ConstructFormula(ref string strFormulaConditions, ref string strFinalFormulaId)
        {
            bool validFormula = false;
            strFormulaConditions = "";
            strFinalFormulaId = "";

            if (rdoIfcondition.SelectedIndex == 0)
            {
                checkIfThen();
            }
            else if (rdoIfcondition.SelectedIndex == 1)
            {
                checkIfThen();
                checkIfAndThen();
            }
            else if (rdoIfcondition.SelectedIndex == 2)
            {
                checkIfThen();
                if (txtFormula2.Text == "")
                {
                    //XtraMessageBox.Show("Formula2 Can not be Empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    XtraMessageBox.Show(this.GetMessage(MessageCatalog.Payroll.BuildFormula.BUILD_FORMULA2_FIELD_EMPTY), this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnBuildFormula2.Focus();
                    return false;
                }
            }
            else if (rdoIfcondition.SelectedIndex == 3)
            {
                checkIfThen();
                checkIfAndThen();
                if (txtFormula2.Text == "")
                {
                    //XtraMessageBox.Show("Formula2 Can not be Empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    XtraMessageBox.Show(this.GetMessage(MessageCatalog.Payroll.BuildFormula.BUILD_FORMULA2_FIELD_EMPTY), this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnBuildFormula2.Focus();
                    return false;
                }
            }
            else
            {
                if (txtFormula1.Text == " ")
                {
                    //XtraMessageBox.Show("Formula Can not be Empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    XtraMessageBox.Show(this.GetMessage(MessageCatalog.Payroll.BuildFormula.BUILD_FORMULA_FIELD_EMPTY), this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnBuildFormula1.Focus();
                    return false;
                }
            }
            if (txtFormula1.Text != "")
            {
                ComponentValue1 = BuildFormulaId(txtFormula1.Text.Trim());
                ComponentValue1 = ComponentValue1.Replace(" ", "");
                ComponentValue1 = ComponentValue1.Replace(Convert.ToChar(160) + "<", "<");
                ComponentValue1 = ComponentValue1.Replace(">" + Convert.ToChar(160), ">");
            }
            if (txtFormula2.Text != "")
            {
                ComponentValue2 = BuildFormulaId(txtFormula2.Text.Trim());
                ComponentValue2 = ComponentValue2.Replace(" ", "");
                ComponentValue2 = ComponentValue2.Replace(Convert.ToChar(160) + "<", "<");
                ComponentValue2 = ComponentValue2.Replace(">" + Convert.ToChar(160), ">");
            }
            //  Relational Operators (nOpr1) Id : 1 =, 2 >, 3 <, 4 >=, 5 <=, 6 <>
            //change the operator type
            if (txtOperator1.Text != "")
            {
                switch (txtOperator1.Text.Trim())
                {
                    case "=":
                        strOperator = "1";
                        break;
                    case ">":
                        strOperator = "2";
                        break;
                    case "<":
                        strOperator = "3";
                        break;
                    case ">=":
                        strOperator = "4";
                        break;
                    case "<=":
                        strOperator = "5";
                        break;
                    case "<>":
                        strOperator = "6";
                        break;
                }
            }
            if (txtOperator2.Text != "")
            {
                switch (txtOperator2.Text.Trim())
                {
                    case "=":
                        strOperator1 = "1";
                        break;
                    case ">":
                        strOperator1 = "2";
                        break;
                    case "<":
                        strOperator1 = "3";
                        break;
                    case ">=":
                        strOperator1 = "4";
                        break;
                    case "<=":
                        strOperator1 = "5";
                        break;
                    case "<>":
                        strOperator1 = "6";
                        break;
                }
            }

            if (txtLogicalOperator.Text == "AND")
            {
                strLogical = "1";
            }
            if (txtLogicalOperator.Text == "OR")
            {
                strLogical = "2";
            }
            if (rdoIfcondition.SelectedIndex == 0)
            {
                strFormulaConditions = "{IF}" + Convert.ToChar(160) + "{" + txtComponent.Text + "}" + Convert.ToChar(160) + "{" + txtOperator1.Text + "}" + Convert.ToChar(160) + "{" + txtValue1.Text + "}" + Convert.ToChar(160) + "{THEN}" + Convert.ToChar(160) + "{" + txtFormula1.Text + "}";
                strFinalFormulaId = "{" + htFormula[txtComponent.Text.ToString()] + "}" + Convert.ToChar(160) + "{" + strOperator + "}" + Convert.ToChar(160) + "{" + txtValue1.Text + "}" + Convert.ToChar(160) + "{" + ComponentValue1 + "}~1";

                //Parent.txtEquation.Text = "{IF}"+ Convert.ToChar(160) +"{"+ txtComponent.Text + "}" +Convert.ToChar(160) + "{"+  txtOperator1.Text + "}" + Convert.ToChar(160) +"{"+ txtValue1.Text + "}" + Convert.ToChar(160) +"{THEN}"+ Convert.ToChar(160) +"{"+  txtFormula1.Text + "}";
                //Parent.CValue           = "{"+ htFormula[txtComponent.Text.ToString()] + "}" + Convert.ToChar(160) +"{"+  strOperator + "}" + Convert.ToChar(160) +"{"+ txtValue1.Text + "}" + Convert.ToChar(160) +"{"+  ComponentValue1 + "}";

            }
            else if (rdoIfcondition.SelectedIndex == 1)
            {
                strFormulaConditions = "{IF}" + Convert.ToChar(160) + "{" + txtComponent.Text + "}" + Convert.ToChar(160) + "{" + txtOperator1.Text + "}" + Convert.ToChar(160) + "{" + txtValue1.Text + "}" + Convert.ToChar(160) + "{" + txtLogicalOperator.Text + "}" + Convert.ToChar(160) + "{" + txtComponent1.Text + "}" + Convert.ToChar(160) + "{" + txtOperator2.Text + "}" + Convert.ToChar(160) + "{" + txtValue2.Text + "}" + Convert.ToChar(160) + "{THEN}" + Convert.ToChar(160) + "{" + txtFormula1.Text + "}";
                strFinalFormulaId = "{" + htFormula[txtComponent.Text.ToString()] + "}" + Convert.ToChar(160) + "{" + strOperator + "}" + Convert.ToChar(160) + "{" + txtValue1.Text + "}" + Convert.ToChar(160) + "{" + strLogical + "}" + Convert.ToChar(160) + "{" + htFormula[txtComponent1.Text.ToString()] + "}" + Convert.ToChar(160) + "{" + strOperator1 + "}" + Convert.ToChar(160) + "{" + txtValue2.Text + "}" + Convert.ToChar(160) + "{" + ComponentValue1 + "}~2";

                //Parent.txtEquation.Text     = "{IF}"+Convert.ToChar(160)+"{"+txtComponent.Text+ "}" + Convert.ToChar(160) +"{"+txtOperator1.Text + "}" +Convert.ToChar(160) + "{"+txtValue1.Text + "}"+Convert.ToChar(160) + "{"+txtLogicalOperator.Text + "}" +Convert.ToChar(160)+"{"+txtComponent1.Text + "}" +Convert.ToChar(160)+ "{"+txtOperator2.Text + "}" + Convert.ToChar(160) + "{"+txtValue2.Text + "}"+ Convert.ToChar(160) +"{THEN}"+ Convert.ToChar(160)+"{" + txtFormula1.Text + "}";
                //Parent.txtEquation.ReadOnly = true;
                //Parent.rdoEquation.Checked  = true;
                //Parent.txtMaxSlab.Focus();
                //Parent.CValue               = "{"+ htFormula[txtComponent.Text.ToString()] + "}" + Convert.ToChar(160) +"{"+strOperator + "}" + Convert.ToChar(160) +"{"+txtValue1.Text + "}" + Convert.ToChar(160) +"{"+ strLogical + "}" + Convert.ToChar(160) +"{"+ htFormula[txtComponent1.Text.ToString()] + "}" + Convert.ToChar(160) +"{"+ strOperator1 + "}" + Convert.ToChar(160) +"{"+txtValue2.Text + "}"+ Convert.ToChar(160) +"{" + ComponentValue1 + "}";
            }
            else if (rdoIfcondition.SelectedIndex == 2)
            {
                strFormulaConditions = "{IF}" + Convert.ToChar(160) + "{" + txtComponent.Text + "}" + Convert.ToChar(160) + "{" + txtOperator1.Text + "}" + Convert.ToChar(160) + "{" + txtValue1.Text + "}" + Convert.ToChar(160) + "{THEN}" + Convert.ToChar(160) + "{" + txtFormula1.Text + "}" + Convert.ToChar(160) + "{ELSE}" + Convert.ToChar(160) + "{" + txtFormula2.Text + "}";
                strFinalFormulaId = "{" + htFormula[txtComponent.Text.ToString()] + "}" + Convert.ToChar(160) + "{" + strOperator + "}" + Convert.ToChar(160) + "{" + txtValue1.Text + "}" + Convert.ToChar(160) + "{" + ComponentValue1 + "}" + Convert.ToChar(160) + "{" + ComponentValue2 + "}~3";

                //Parent.txtEquation.Text     = "{IF}"+Convert.ToChar(160)+"{"+ txtComponent.Text + "}" + Convert.ToChar(160) +"{"+txtOperator1.Text + "}" + Convert.ToChar(160) +"{" + txtValue1.Text + "}" + Convert.ToChar(160) +"{THEN}"+Convert.ToChar(160)+"{" + txtFormula1.Text + "}" + Convert.ToChar(160)+"{ELSE}"+Convert.ToChar(160) +"{"+ txtFormula2.Text + "}";
                //Parent.txtEquation.ReadOnly = true;
                //Parent.rdoEquation.Checked  = true;
                //Parent.CValue               = "{"+ htFormula[txtComponent.Text.ToString()] + "}" + Convert.ToChar(160) +"{"+strOperator + "}" + Convert.ToChar(160) +"{" + txtValue1.Text + "}" + Convert.ToChar(160) +"{" + ComponentValue1 + "}" + Convert.ToChar(160) +"{"+ ComponentValue2 + "}";
            }
            else if (rdoIfcondition.SelectedIndex == 3)
            {
                strFormulaConditions = "{IF}" + Convert.ToChar(160) + "{" + txtComponent.Text + "}" + Convert.ToChar(160) + "{" + txtOperator1.Text + "}" + Convert.ToChar(160) + "{" + txtValue1.Text + "}" + Convert.ToChar(160) + "{" + txtLogicalOperator.Text + "}" + Convert.ToChar(160) + "{" + txtComponent1.Text + "}" + Convert.ToChar(160) + "{" + txtOperator2.Text + "}" + Convert.ToChar(160) + "{" + txtValue2.Text + "}" + Convert.ToChar(160) + "{THEN}" + Convert.ToChar(160) + "{" + txtFormula1.Text + "}" + Convert.ToChar(160) + "{ELSE}" + Convert.ToChar(160) + "{" + txtFormula2.Text + "}";
                strFinalFormulaId = "{" + htFormula[txtComponent.Text.ToString()] + "}" + Convert.ToChar(160) + "{" + strOperator + "}" + Convert.ToChar(160) + "{" + txtValue1.Text + "}" + Convert.ToChar(160) + "{" + strLogical + "}" + Convert.ToChar(160) + "{" + htFormula[txtComponent1.Text.ToString()] + "}" + Convert.ToChar(160) + "{" + strOperator1 + "}" + Convert.ToChar(160) + "{" + txtValue2.Text + "}" + Convert.ToChar(160) + "{" + ComponentValue1 + "}" + Convert.ToChar(160) + "{" + ComponentValue2 + "}~4";

                //Parent.txtEquation.Text     = "{IF}"+Convert.ToChar(160)+"{"+txtComponent.Text+ "}" + Convert.ToChar(160) +"{"+txtOperator1.Text + "}" + Convert.ToChar(160) +"{"+txtValue1.Text + "}" + Convert.ToChar(160) +"{"+txtLogicalOperator.Text + "}" + Convert.ToChar(160) +"{"+txtComponent1.Text + "}" + Convert.ToChar(160) +"{"+txtOperator2.Text + "}" + Convert.ToChar(160) +"{"+txtValue2.Text + "}"+ Convert.ToChar(160) +"{THEN}"+Convert.ToChar(160)+"{" + txtFormula1.Text + "}"+ Convert.ToChar(160) +"{ELSE}"+Convert.ToChar(160)+"{" + txtFormula2.Text + "}";
                //Parent.txtEquation.ReadOnly = true;
                //Parent.rdoEquation.Checked  = true;
                //Parent.txtMaxSlab.Focus();
                //Parent.CValue               = "{"+ htFormula[txtComponent.Text.ToString()] + "}" + Convert.ToChar(160) +"{"+strOperator + "}" + Convert.ToChar(160) +"{"+ txtValue1.Text + "}" + Convert.ToChar(160) +"{"+ strLogical +"}" + Convert.ToChar(160) +"{"+ htFormula[txtComponent1.Text.ToString()] + "}" + Convert.ToChar(160)+"{"+strOperator1 + "}" + Convert.ToChar(160) +"{"+txtValue2.Text + "}"+ Convert.ToChar(160) +"{" + ComponentValue1 + "}"+ Convert.ToChar(160) +"{" + ComponentValue2 + "}";
            }
            else
            {
                //BuildFormulaId(txtFormula1.Text);
                strFormulaConditions = txtFormula1.Text;
                //strFinalFormulaId			 = txtFormula1.Text + "~0"; // + "!0";
                strFinalFormulaId = BuildFormulaId(txtFormula1.Text) + "~0";

                //Parent.txtEquation.Text      = txtFormula1.Text;
                //Parent.txtEquation.ReadOnly  = true;
                //Parent.CValue                = ComponentValue1;
            }

            /*if(lstFormula.Items.Count > 0)
            {
                for(int i =0; i < lstFormula.Items.Count; i++)
                {
                    strFinalString += lstFormula.Items[i].ToString()+ " ";
                }
                string[] sFormula ;
                string[] strValue;
                sFormula = strFinalString.Split('$');
                for ( int i=0; i< sFormula.Length; i++)
                {
                    strValue = sFormula[i].Split('~');
                    if(strValue.Length == 2)
                    {
                        Parent.txtEquation.Text += strValue[0].ToString()+ " ";
                        Parent.CValue          += strValue[1].ToString() + " ";
                    }
                }
            }*/

            //this.Close();
            if (validateFields())
            {
                validFormula = true;
                //XtraMessageBox.Show("Formula is added", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.BuildFormula.BUILD_FORMULA_ADDED_INFO));
            }
            else
            {
                //XtraMessageBox.Show("Invalid formula", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                XtraMessageBox.Show(this.GetMessage(MessageCatalog.Payroll.BuildFormula.BUILD_INVALID_FORMULA_INFO), this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return validFormula;
        }


        private void EditFormula(string formula, string formulaId, int ifCondition)
        {
            int nIdx;
            string[] aFormulaId = new string[50];
            try
            {
                if (ifCondition == 0)
                {
                    txtFormula1.Visible = true;
                    txtFormula1.Text = formula;
                }
                else
                {
                    if (formulaId != "")
                    {
                        aFormulaId = formulaId.Split(Convert.ToChar(160));
                        nIdx = Int32.Parse(RemoveBrace(aFormulaId[0].ToString(), false));
                        txtComponent.Text = htID[nIdx.ToString()].ToString();
                        nIdx = Int32.Parse(RemoveBrace(aFormulaId[1].ToString(), false));

                        if (nIdx > 0)
                            txtOperator1.Text = lstbOperator1.Items[nIdx - 1].ToString();
                        txtValue1.Text = RemoveBrace(aFormulaId[2].ToString(), false).ToString();

                        if ((ifCondition == 1) || (ifCondition == 3))
                        {
                            if (ifCondition == 1)
                            {
                                txtFormula1.Tag = RemoveBrace(aFormulaId[3].ToString(), false);
                                if (txtFormula1.Tag.ToString() != "")
                                {
                                    txtFormula1.Text = BuildFormulaId(txtFormula1.Tag.ToString());
                                    txtFormula1.Text = txtFormula1.Text.Substring(0, txtFormula1.Text.IndexOf('~'));
                                }
                            }
                            if (ifCondition == 3)
                            {
                                txtFormula1.Tag = RemoveBrace(aFormulaId[3].ToString(), false);
                                if (txtFormula1.Tag.ToString() != "")
                                {
                                    txtFormula1.Text = BuildFormulaId(txtFormula1.Tag.ToString());
                                    //txtFormula1.Text = txtFormula1.Text.Substring(0,txtFormula1.Text.IndexOf('~'));
                                }
                                txtFormula2.Tag = RemoveBrace(aFormulaId[4].ToString(), false);
                                if (txtFormula2.Tag.ToString() != "")
                                {
                                    txtFormula2.Text = BuildFormulaId(txtFormula2.Tag.ToString());
                                    txtFormula2.Text = txtFormula2.Text.Substring(0, txtFormula2.Text.IndexOf('~'));
                                }
                            }
                        }
                        else if (ifCondition == 2 || ifCondition == 4)
                        {
                            nIdx = Int32.Parse(RemoveBrace(aFormulaId[3].ToString(), false));
                            if (nIdx > 0)
                            {
                                txtLogicalOperator.Text = lbLogicalOperator.Items[nIdx - 1].ToString();
                            }
                            nIdx = Int32.Parse(RemoveBrace(aFormulaId[4].ToString(), false));
                            if (nIdx > 0) txtComponent1.Text = htID[nIdx.ToString()].ToString();
                            nIdx = Int32.Parse(RemoveBrace(aFormulaId[5].ToString(), false));
                            if (nIdx > 0)
                            {
                                txtOperator2.Text = lstbOperator1.Items[nIdx - 1].ToString();
                            }
                            txtValue2.Text = RemoveBrace(aFormulaId[6].ToString(), false);
                            txtFormula1.Tag = RemoveBrace(aFormulaId[7].ToString(), false);
                            if (txtFormula1.Tag.ToString() != "")
                            {
                                txtFormula1.Text = BuildFormulaId(txtFormula1.Tag.ToString());
                                //txtFormula1.Text = txtFormula1.Text.Substring(0,txtFormula1.Text.IndexOf('~'));
                            }
                            if (ifCondition == 4)
                            {
                                txtFormula2.Tag = RemoveBrace(aFormulaId[8].ToString(), false);
                                if (txtFormula2.Tag.ToString() != "")
                                {
                                    txtFormula2.Text = BuildFormulaId(txtFormula2.Tag.ToString());
                                    txtFormula2.Text = txtFormula2.Text.Substring(0, txtFormula2.Text.IndexOf('~'));
                                }

                            }

                        }

                    }
                }
            }
            catch
            {
            }
        }

        private string RemoveBrace(string sText, bool bSeparator)
        {
            string sVal = "";
            sVal = sText;
            sVal = sVal.Replace("{", "");
            sVal = sVal.Replace("}", "");
            if (bSeparator)
            {
                sVal = sVal.Replace("<", "");
                sVal = sVal.Replace(">", "");
            }
            return sVal;
        }
        private bool getComponentValue(ref string compId)
        {
            string strCom = RemoveBrace(compId, true);
            try
            {
                int i = Convert.ToInt32(strCom.ToString());
                if (i > 0)
                    compId = Convert.ToChar(160).ToString() + "<" + htID[i.ToString()].ToString() + ">" + Convert.ToChar(160).ToString();
            }
            catch
            {
                compId = Convert.ToChar(160).ToString() + "<" + htFormula[strCom.ToString()].ToString() + ">" + Convert.ToChar(160).ToString();
                return true;
            }
            return true;
        }
        /*
                private int GetIFArgumentIndex(ListBox lstBox, string sText)
                {
                    int index;
                    for ( index =1; index < lstBox.Items.Count; index++)
                    {
                        if ( lstBox.Items[index].ToString() == sText )
                        {
                            lstBox.SelectedIndex = index;
                            return int.Parse(lstBox.SelectedValue.ToString());
                        }
                    }
                    return 0;
                }
                private string GetFormula(ListBox lstBox, int iId)
                {
                    int index;
                    for ( index =1; index < lstBox.Items.Count; index++)
                    {
                            lstBox.SelectedIndex = index;
                        if (lstBox.SelectedValue.ToString()== iId.ToString())
                        {
                            return lstBox.ValueMember.ToString();
                        }
                    }
                    return "";
                } */

        private string BuildFormulaId(string strFormula)
        {
            int i, nPos, nPos1, nStartPos;
            string sCompId = "";
            string sCompId1 = "";
            string sComp = "";
            sCompId = strFormula;
            nStartPos = 0;
            try
            {
                for (i = 0; i < strFormula.Length; i++)
                {
                    nPos = strFormula.IndexOf("<", nStartPos);

                    if (nPos >= 0)
                    {
                        nStartPos = nPos + 1;
                        nPos1 = strFormula.IndexOf(">", nStartPos);
                        nStartPos = nPos1 + 1;
                        if (nPos1 > 0)
                        {
                            sComp = strFormula.Substring(nPos, nPos1 - (nPos - 1));
                            sCompId1 = sComp;
                            getComponentValue(ref sComp);
                            sCompId = sCompId.Replace(sCompId1, sComp.Trim());
                            nStartPos = nPos + sCompId1.Length;
                        }
                        else
                            break;
                    }
                    else
                        break;
                }

            }
            catch (Exception ex)
            {
                string str = ex.Message;
                return sCompId;
            }
            return sCompId;
        }

        #endregion

        #region Events
        private void frmBuildIfFormula_Load(object sender, EventArgs e)
        {
            this.Height = 465;
            lcibtnNew.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            rdoIfcondition.SelectedIndex = 4;
            objCommon.BindLookUpEditCombo(cboCategory, FetchRecords(SQLCommand.Payroll.PayrollCategoryFetch), "FORMULA_DESC", "FORMULAGROUPID");
            //clsGeneral.(cboCategory,"SELECT 0 AS FORMULAGROUPID, '<All>' AS FORMULA_DESC FROM DUAL UNION ALL SELECT FORMULAGROUPID,FORMULA_DESC FROM PRFORMULAGROUP ORDER BY FORMULA_DESC","FORMULA_DESC","FORMULAGROUPID");

            DisableControls();

            try
            {
                /*if( strCheck == "Add" )
                {
                        AddFormula();					
                }*/

                //clsGeneral.fillList(lbComponent,"SELECT COMPONENTID AS \"COMPONENTID\",COMPONENT AS \"COMPONENT\" FROM PRCOMPONENT ORDER BY COMPONENT","COMPONENT","COMPONENTID");
                //To store the ComponentID and Component.
                objCommon.BindDataList(lbComponent, FetchRecords(SQLCommand.Payroll.PayrollComponentFetch), "COMPONENT", "COMPONENTID");
                objCommon.BindDataList(lbComponent1, FetchRecords(SQLCommand.Payroll.PayrollComponentFetch), "COMPONENT", "COMPONENTID");
                DataTable dt = objComp.getPayrollComponent();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (!htFormula.Contains(dr[1].ToString()))
                            htFormula.Add(dr[1].ToString(), dr[0].ToString());
                        htID.Add(dr[0].ToString(), dr[1].ToString());
                    }
                }
                else
                {
                    //XtraMessageBox.Show("There is no component available. Add Default Components", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.BuildFormula.BUILD_COMPONENT_EMPTY));
                    this.Close();
                    return;
                }

                FillFormula();
                rdoIfcondition_SelectedIndexChanged(sender, e);

                /*if ( strCheck == "Edit" )
                {
                    EditFormula();
                }*/
            }
            catch
            {

            }
        }

        private void rdoIfcondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoIfcondition.SelectedIndex == 0)
            {
                this.Height = 525;
                lcilsttbOperator1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcilstbOperator2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcilbComponent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlGroup3.Text = "IF.....THEN";
                lciLstOperator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                emptySpaceItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcilbComponent1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                emptySpaceItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcilsttbOperator1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcilblIf.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lblIf.Visible = true;
                lciComponent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtComponent.Visible = true;
                lciopr1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtOperator1.Visible = true;
                lciValue1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtValue1.Visible = true;
                // emptySpaceItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                //emptySpaceItem10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcilblComponentname.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lblComponentName.Visible = true;
                lcilogicaloperator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                txtLogicalOperator.Visible = false;
                //lcilblThen.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                // lblThen.Visible = true;
                //lcilblEndIf.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                //lblEndIf.Visible = true;
                lciComponent2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                txtComponent1.Visible = false;
                lciOpr2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                txtOperator2.Visible = false;
                lciValue2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                txtValue2.Visible = false;
                lcilblComponentName1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lblComponentName1.Visible = false;
                lcitxtFormula2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                txtFormula2.Visible = false;
                lcibtnBuildFormula2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                btnBuildFormula2.Visible = false;
                lcilblElse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lblElse.Visible = false;
                //lcilblThen2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //lblThen2.Visible = false;
                //lblDescription.Text = "The formula which you Construct Between the Statement" +
                //                          " IF and END IF that is Evaluated based on the selected " +
                //                          " Component which is logically matches with the given value for " +
                //                          " one set of Condition.";
                lblDescription.Text = "The formula which you Construct Between the Statement" +
                                         " IF and END IF that is Evaluated based on the selected " +
                                         " Component for one set of Condition.";

            }
            else if (rdoIfcondition.SelectedIndex == 1)
            {
                this.Height = 525;
                emptySpaceItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcilbComponent1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                emptySpaceItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcilbComponent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                emptySpaceItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                emptySpaceItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                emptySpaceItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lciLstOperator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcilsttbOperator1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcilogicaloperator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcilbComponent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcilstbOperator2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlGroup3.Text = "IF.....AND....THEN";
                lciComponent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtComponent.Visible = true;
                lciopr1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtOperator1.Visible = true;
                lciValue1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtValue1.Visible = true;
                lcilogicaloperator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtLogicalOperator.Visible = true;
                //lcilblThen2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                //lblThen2.Visible = true;
                lciComponent2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtComponent1.Visible = true;
                lciOpr2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtOperator2.Visible = true;
                lciValue2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtValue2.Visible = true;
                lcitxtFormula1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtFormula1.Visible = true;
                emptySpaceItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                emptySpaceItem10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcilblComponentname.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lblComponentName.Visible = true;
                //lcilblEndIf.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                //lblEndIf.Visible = true;
                lcilblComponentName1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lblComponentName1.Visible = false;
                lcitxtFormula2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                txtFormula2.Visible = false;
                lcibtnBuildFormula2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                btnBuildFormula2.Visible = false;
                lcilblElse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lblElse.Visible = false;
                //lcilblThen.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //lblThen.Visible = false;
                //lblDescription.Text = "The formula which you Construct Between the Statement " +
                //                        " IF and END IF that is Evaluated based on the selected " +
                //                        " Component which is logically matches with the given value for" +
                //                        " one set of Condition.";
                lblDescription.Text = "The formula which you Construct Between the Statement " +
                                       " IF and END IF that is Evaluated based on the selected " +
                                       " Component  for one set of Condition.";
            }
            else if (rdoIfcondition.SelectedIndex == 2)
            {
                this.Height = 525;
                lcilstbOperator2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                emptySpaceItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcilbComponent1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                emptySpaceItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcilsttbOperator1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlGroup3.Text = "IF.....ELSE....THEN";
                lcilbComponent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcilblIf.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lblIf.Visible = true;
                lciComponent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtComponent.Visible = true;
                lciopr1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtOperator1.Visible = true;
                lciValue1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtValue1.Visible = true;
                lcilblComponentname.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lblComponentName.Visible = true;
                // lcilblThen.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                //lblThen.Visible           =  true;
                //lcilblEndIf.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                //lblEndIf.Visible          =  true;
                lcilblElse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lblElse.Visible = true;
                lcilblComponentName1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lblComponentName1.Visible = true;
                lciComponent2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                txtComponent1.Visible = false;
                lciOpr2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                txtOperator2.Visible = false;
                lciValue2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                txtValue2.Visible = false;
                lcitxtFormula1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtFormula1.Visible = true;
                lcitxtFormula2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtFormula2.Visible = true;
                lcibtnBuildFormula1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                btnBuildFormula1.Visible = true;
                lcibtnBuildFormula2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                btnBuildFormula2.Visible = true;
                //lcilblThen2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //lblThen2.Visible          =  false;
                //lblDescription.Text = "The formula which you Construct Between the Statement " +
                //                             " IF and END IF that is Evaluated based on the selected " +
                //                             " Component which is logically matches with the given value." +
                //                             " Otherwise the formula lies between the statement \'ELSE\' and \'END IF\' set of Condition.";

                lblDescription.Text = "The formula Between" +
                                         " IF and END IF is Evaluated based on the selected " +
                                         " Component Otherwise the formula between the statement" +
                                         " \'ELSE\' and \'END IF\' will be evaluated";
            }
            else if (rdoIfcondition.SelectedIndex == 3)
            {
                this.Height = 525;
                lcilsttbOperator1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                emptySpaceItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcilbComponent1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                emptySpaceItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                emptySpaceItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                emptySpaceItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                emptySpaceItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lciLstOperator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcilbComponent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcilstbOperator2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcilogicaloperator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcilbComponent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcilstbOperator2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlGroup3.Text = "IF.....AND....ELSE....THEN";
                lcilblIf.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lblIf.Visible = true;
                lciComponent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtComponent.Visible = true;
                lciopr1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtOperator1.Visible = true;
                lciValue1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtValue1.Visible = true;
                lcilblComponentname.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lblComponentName.Visible = true;
                lcilogicaloperator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtLogicalOperator.Visible = true;
                // lcilblThen.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                // lblThen.Visible = true;
                //  lcilblEndIf.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                // lblEndIf.Visible = true;
                lcilblElse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lblElse.Visible = true;
                lcilblComponentName1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lblComponentName1.Visible = true;
                lciComponent2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtComponent1.Visible = true;
                lciOpr2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtOperator2.Visible = true;
                lciValue2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtValue2.Visible = true;
                lcitxtFormula1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtFormula1.Visible = true;
                lcitxtFormula2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtFormula2.Visible = true;
                lcibtnBuildFormula1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                btnBuildFormula1.Visible = true;
                lcibtnBuildFormula2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                btnBuildFormula2.Visible = true;
                // lcilblThen2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                //lblThen2.Visible = true;
                // lcilblThen.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                // lblThen.Visible = false;
                //lblDescription.Text = "The formula which you Construct Between the Statement " +
                //                      " IF and END IF that is Evaluated based on the selected " +
                //                      " Component which is logically matches with the given value." +
                //                      " Otherwise the formula lies between the statement \'ELSE\' and \'END IF\' set of Condition.";
                lblDescription.Text = "The formula Between" +
                                    " IF and END IF is Evaluated based on the selected " +
                                    " Component Otherwise the formula between the statement" +
                                    " \'ELSE\' and \'END IF\' will be evaluated";

            }
            else if (rdoIfcondition.SelectedIndex == 4)
            {
                this.Height = 465;
                lcilsttbOperator1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcilstbOperator2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlGroup3.Text = "FORMULA";
                lcilogicaloperator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                txtLogicalOperator.Visible = false;
                lciLstOperator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcilbComponent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcilsttbOperator1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcilbComponent1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcilblIf.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lblIf.Visible = false;
                lciComponent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                txtComponent.Visible = false;
                lciopr1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                txtOperator1.Visible = false;
                lciValue1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                txtValue1.Visible = false;
                lcilblComponentname.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lblComponentName.Visible = true;
                lcilogicaloperator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                txtLogicalOperator.Visible = false;
                //lcilblThen.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //lblThen.Visible = false;
                //lcilblEndIf.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //lblEndIf.Visible = false;
                lcilblElse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lblElse.Visible = false;

                lcilblComponentName1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lblComponentName1.Visible = false;
                lciComponent2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                txtComponent1.Visible = false;
                lciOpr2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                txtOperator2.Visible = false;
                lciValue2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                txtValue2.Visible = false;
                lcitxtFormula1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtFormula1.Visible = true;
                lcitxtFormula2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                txtFormula2.Visible = false;
                lcibtnBuildFormula1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                btnBuildFormula1.Visible = true;
                lcibtnBuildFormula2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                btnBuildFormula2.Visible = false;
                lcilstbOperator2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                // lcilblThen2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //lblThen2.Visible = false;
                //lcilblThen.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                // lblThen.Visible = false;
                lblDescription.Text = "The constructed formula is evaluated without any condition.";
            }
        }

        private void btnAllocateFormulaGroup_Click(object sender, EventArgs e)
        {
            new frmFormulaGroup().ShowDialog();
            //clsGeneral.fillList(cboCategory, "SELECT 0 AS FORMULAGROUPID, '<All>' AS FORMULA_DESC FROM DUAL UNION ALL SELECT FORMULAGROUPID,FORMULA_DESC FROM PRFORMULAGROUP ORDER BY FORMULA_DESC", "FORMULA_DESC", "FORMULAGROUPID");
            objCommon.BindLookUpEditCombo(cboCategory, FetchRecords(SQLCommand.Payroll.PayrollCategoryFetch), "FORMULA_DESC", "FORMULAGROUPID");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string formulaGroup = "";
            string formula = "";
            string formulaId = "";
            string[] aformulaGroup;
            string[] aformulaId;

            int ifId = 0;
            string formulaGroupId = "";

            if (lstFormula.SelectedIndex >= 0)
            {
                newFormula = false;

                formulaGroup = lstFormula.Items[lstFormula.SelectedIndex].ToString();
                aformulaGroup = formulaGroup.Split('$');
                aformulaId = aformulaGroup[1].Split('~');

                formula = aformulaGroup[0];
                formulaId = aformulaGroup[1];

                if (aformulaId.Length > 1)
                {
                    ifId = Convert.ToInt32(aformulaId[1]);
                    formulaGroupId = aformulaId[2];
                }

                if (formulaGroupId != "")
                {
                    cboCategory.EditValue = formulaGroupId;
                }
                else
                {
                    cboCategory.EditValue = 0;
                }

                SelectIFCondition(ifId);
                EditFormula(formula, formulaId, ifId);
            }

            btnNew.Enabled = true;
        }

        private void btnRemoveFromList_Click(object sender, EventArgs e)
        {
            if (lstFormula.SelectedIndex >= 0)
            {
                lstFormula.Items.RemoveAt(lstFormula.SelectedIndex);
            }

            newFormula = true;
        }

        private void btnBuildFormula1_Click(object sender, EventArgs e)
        {
            Comp = 1;

            frmCostructFormula objConstruct = new frmCostructFormula(this, 1, Parent.ComponentId);
            objConstruct.ShowDialog();
        }

        private void btnBuildFormula2_Click(object sender, EventArgs e)
        {
            Comp = 2;
            frmCostructFormula objConstruct = new frmCostructFormula(this, 2);
            objConstruct.ShowDialog();
        }

        private void btnSavetoList_Click(object sender, EventArgs e)
        {
            string strFormula = "";
            string strFormulaId = "";
            string formulaStaffGroupId = "0";

            formulaStaffGroupId = cboCategory.EditValue.ToString();

            if (ConstructFormula(ref strFormula, ref strFormulaId))
            {
                if (strFormula != "")
                {
                    strFormula += "$" + strFormulaId + "~" + formulaStaffGroupId;

                    if (newFormula)
                    {
                        lstFormula.Items.Add(strFormula);
                    }
                    else
                    {
                        if (lstFormula.SelectedIndex >= 0)
                        {
                            int idx = lstFormula.SelectedIndex;
                            lstFormula.Items.RemoveAt(idx);
                            lstFormula.Items.Insert(idx, strFormula);
                        }
                    }
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            newFormula = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string formulaGroup = "";
            string formula = "";
            string formulaId = "";
            string formula1 = "";
            string formulaId1 = "";
            string[] aformulaGroup;
            string[] aformulaId;
            int formulaGroupId = 0;
            if (lstFormula.ItemCount <= 0)
            {
                btnSavetoList_Click(sender, e);
            }
            for (int i = 0; i < lstFormula.Items.Count; i++)
            {
                formulaGroup = lstFormula.Items[i].ToString();
                aformulaGroup = formulaGroup.Split('$');

                aformulaId = aformulaGroup[1].Split('~');

                formulaGroupId = (aformulaId.Count() >= 2) ? Convert.ToInt32(aformulaId[2]) : 0;

                //for restricted staff
                if (formulaGroupId > 0)
                {
                    formula1 += ((formula1 != "") ? "$" : "") + aformulaGroup[0];
                    formulaId1 += ((formulaId1 != "") ? "$" : "") + aformulaGroup[1];
                }
                else  //formula for all staff
                {
                    formula += ((formula != "") ? "$" : "") + aformulaGroup[0];
                    formulaId += ((formulaId != "") ? "$" : "") + aformulaGroup[1];
                }
            }

            if (formula1 != "")
            {
                formula = formula1 + (formula == "" ? "" : ("$" + formula));
                formulaId = formulaId1 + (formulaId == "" ? "" : ("$" + formulaId));
            }

            Parent.txtEquationBuild.Text = formula;
            Parent.CValue = formulaId;
            Parent.IFCon = 1;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmBuildIfFormula_Click(object sender, EventArgs e)
        {
            lbComponent.Visible = false;
            lstbOperator1.Visible = false;
            lstbOperator2.Visible = false;
            lbLogicalOperator.Visible = false;
        }
        #endregion

        private void lbLogicalOperator_MouseLeave(object sender, EventArgs e)
        {
        }

        private void frmBuildIfFormula_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void txtComponent_DoubleClick(object sender, EventArgs e)
        {
            //ActiveControl = "Comp";
            //lcilbComponent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            lbComponent1_SelectedIndexChanged(sender, e);
            //lbComponent.Visible = true;
        }

        private void lbComponent_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtComponent1.Text = lbComponent.Text;
            //if (ActiveControl == "Comp")
            //{
            //    txtComponent.Text = lbComponent1.Text;
            //    lbComponent.Visible = false;
            //}
            //if (ActiveControl == "Comp1")
            //{
            //    txtComponent1.Text = lbComponent.Text;
            //    lbComponent.Visible = false;
            //}
        }

        private void lstbOperator1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtOperator1.Text = lstbOperator1.Text;
        }

        private void lstbOperator2_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtOperator2.Text = lstbOperator2.Text;
            lstbOperator2.Visible = false;
        }

        private void lbLogicalOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtLogicalOperator.Text = lbLogicalOperator.Text;
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            string itemtoUp = "";
            string itemtoDown = "";

            int selIndex = lstFormula.SelectedIndex;
            try
            {
                if (selIndex > 0)
                {
                    itemtoUp = lstFormula.Items[selIndex].ToString();
                    itemtoDown = lstFormula.Items[selIndex - 1].ToString();

                    lstFormula.Items.RemoveAt(selIndex - 1);
                    lstFormula.Items.RemoveAt(selIndex - 1);

                    lstFormula.Items.Insert(selIndex - 1, itemtoUp);
                    lstFormula.Items.Insert(selIndex, itemtoDown);

                }
            }
            catch { }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            string itemtoUp = "";
            string itemtoDown = "";

            int selIndex = lstFormula.SelectedIndex;

            try
            {
                if (selIndex < (lstFormula.Items.Count - 1))
                {
                    itemtoUp = lstFormula.Items[selIndex + 1].ToString();
                    itemtoDown = lstFormula.Items[selIndex].ToString();

                    lstFormula.Items.RemoveAt(selIndex);
                    lstFormula.Items.RemoveAt(selIndex);

                    lstFormula.Items.Insert(selIndex, itemtoUp);
                    lstFormula.Items.Insert(selIndex + 1, itemtoDown);
                }
            }
            catch { }
        }

        private void txtOperator1_DoubleClick(object sender, EventArgs e)
        {
            //lcilstbOperator2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //lcilsttbOperator1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //lstbOperator1.Visible = true;
        }

        private void txtOperator2_DoubleClick(object sender, EventArgs e)
        {
            //lcilsttbOperator1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //lcilstbOperator2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //lstbOperator2.Visible = true;
        }

        private void txtComponent1_DoubleClick(object sender, EventArgs e)
        {
            lbComponent_SelectedIndexChanged(sender, e);
            //ActiveControl = "Comp1";
            //this.lbComponent.Location = new System.Drawing.Point(48, 60);//48, 64
            //lcilbComponent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //lbComponent.Visible = true;
        }

        private void lstbOperator2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            txtOperator2.Text = lstbOperator2.Text;
        }

        private void txtLogicalOperator_DoubleClick(object sender, EventArgs e)
        {
            //lciLstOperator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        }

        private void txtLogicalOperator_Leave(object sender, EventArgs e)
        {
            lciLstOperator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        private void txtOperator1_Leave(object sender, EventArgs e)
        {
            lcilsttbOperator1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        private void lstbOperator1_Leave(object sender, EventArgs e)
        {

        }

        private void lstbOperator2_Leave(object sender, EventArgs e)
        {
        }

        private void txtComponent_Leave(object sender, EventArgs e)
        {
            lcilbComponent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        private void txtComponent1_Leave(object sender, EventArgs e)
        {
            lcilbComponent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        private void lbComponent1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtComponent.Text = lbComponent1.Text;
            //if (ActiveControl == "Comp")
            //{
            //    txtComponent.Text = lbComponent1.Text;
            //    lbComponent.Visible = false;
            //}
            //if (ActiveControl == "Comp1")
            //{
            //    txtComponent1.Text = lbComponent.Text;
            //    lbComponent.Visible = false;
            //}
        }

    }
}

