using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using DevExpress.XtraEditors;
using Bosco.Utility;
using Bosco.Utility.Common;
using Bosco.Utility.CommonMemberSet;
using Bosco.DAO.Data;
using Payroll.DAO.Schema;
using Payroll.Model.UIModel;
using System.Collections;
namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmpayrollconstructformula : DevExpress.XtraEditors.XtraForm
    {
        #region Declaration
        public Hashtable htList = new Hashtable();
        private long lCompId = 0;
        private frmBuildIfFormula Parentformula;
        private frmComponentAdd parentcondition;
        private bool CheckedFormula = false;
        private int iFormula;
        public clsprCompBuild objBuild = new clsprCompBuild();
        public clsPayrollComponent objPayrollComp = new clsPayrollComponent();
        NumberSetMember numset = new NumberSetMember();
        ComboSetMember objCommon = new ComboSetMember();
        private Regex objR = new Regex("[0-9]|\b");
        private string ActiveControl = "";
        private string strOption = "";
        private string strOperator1 = "";
        public string ComponentValue1 = "";
        public string ComponentValue2 = "";
        public string ComponentValue3 = "";
        private string strLogical = "";
        public int Comp = 1;	//1- To build the equation for formula(comp1), 2- To build the equation for comp(2)
        private long CompId = 0;
        private string strOperator = "";
        public Hashtable htFormula = new Hashtable();
        private Hashtable htID = new Hashtable();
        // private clsPayrollComponent objComp = new clsPayrollComponent();
        private bool newFormula = true;
        private string frmmode = string.Empty;
        private string strCheck = "";
        private TextEdit txtActiveControl;

        #endregion

        #region Properties

        #endregion

        #region Constructor
        public frmpayrollconstructformula()
        {
            InitializeComponent();
        }
        public frmpayrollconstructformula(frmBuildIfFormula frm, int iFormulaType, long compId)
            : this(frm, iFormulaType)
        {
            this.lCompId = compId;
        }
        public frmpayrollconstructformula(frmBuildIfFormula frm, int iFormulaType)
        {
            InitializeComponent();
            Parentformula = frm;
            iFormula = iFormulaType;
            string strComName = Parentformula.lblComponentName.Text;
            if (strComName != "")
            {
                strComName = strComName.Substring(0, strComName.Length - 1);
                //lblFormulaFor.Text += " " + strComName;
            }
            if ((Parentformula.txtFormula1.Text != "") & (iFormula == 1))
            {
                meFormula.Text = Parentformula.txtFormula1.Text;

            }
            if ((Parentformula.txtFormula1.Text != "") & (iFormula == 2))
            {
                meFormula.Text = Parentformula.txtFormula2.Text;

            }
        }
        public frmpayrollconstructformula(frmComponentAdd frm, string strOperation, ComboBoxEdit rdoActiveControl)
        {

            InitializeComponent();
            frmmode = strOperation;
            parentcondition = frm;
            //rdoActiveButton = rdoActiveControl;
            strCheck = strOperation;
        }
        #endregion

        #region Methods
        private bool GetComponentId(ref string sComp)
        {
            sComp = Convert.ToChar(160).ToString() + "<" + htList[sComp].ToString() + ">" + Convert.ToChar(160).ToString();
            return true;
        }
        private void InsertText(string strText)
        {

            //int iPos;
            try
            {
                if (txtActiveControl == null)
                {
                    txtActiveControl = txtComponent;
                }

                if (txtActiveControl != null)
                {
                    if (txtActiveControl.TabIndex != 15)
                    {
                        int iPos;
                        iPos = txtActiveControl.SelectionStart;
                        txtActiveControl.Text = txtActiveControl.Text.Insert(iPos, strText);
                        txtActiveControl.SelectionStart = txtActiveControl.Text.Length + 1;
                        txtActiveControl.Focus();

                        txtActiveControl.SelectAll();
                        txtActiveControl.Select(txtActiveControl.SelectionLength, 0);
                        txtActiveControl.ScrollToCaret();
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch
            {
                throw;
            }

        }
        private string BuildFormulaId()
        {
            // int i, nPos, nPos1, nStartPos;
            // string sCompId = "";
            // string sCompId1 = "";
            // string sComp = "";
            //sCompId = meFormula.Text;

            // nStartPos = 0;
            // try
            // {
            //     for (i = 0; i < meFormula.Text.Length; i++)
            //     {
            //         nPos = meFormula.Text.IndexOf("<", nStartPos);
            //         if (nPos >= 0)
            //         {
            //             nStartPos = nPos + 1;
            //             nPos1 = meFormula.Text.IndexOf(">", nStartPos);
            //             nStartPos = nPos1 + 1;
            //             if (nPos1 > 0)
            //             {
            //                 sComp = meFormula.Text.Substring(nPos, nPos1 - (nPos - 1));
            //                 sCompId1 = sComp;
            //                 GetComponentId(ref sComp);
            //                 sCompId = sCompId.Replace(sCompId1, sComp.Trim());
            //                 nStartPos = nPos + sCompId1.Length;
            //             }
            //             else
            //                 break;
            //         }
            //         else
            //             break;
            //     }

            // }

            int i, nPos, nPos1, nStartPos;
            string sCompId = "";
            string sCompId1 = "";
            string sComp = "";
            sCompId = txtComponent.Text;

            nStartPos = 0;
            try
            {
                for (i = 0; i < txtFormula.Text.Length; i++)
                {
                    nPos = txtFormula.Text.IndexOf("<", nStartPos);
                    if (nPos >= 0)
                    {
                        nStartPos = nPos + 1;
                        nPos1 = txtFormula.Text.IndexOf(">", nStartPos);
                        nStartPos = nPos1 + 1;
                        if (nPos1 > 0)
                        {
                            sComp = txtFormula.Text.Substring(nPos, nPos1 - (nPos - 1));
                            sCompId1 = sComp;
                            GetComponentId(ref sComp);
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

        private bool ValidateComponentSymbols()
        {
            //if (((txtComponent.Text.Contains(".")) || (txtComponent.Text.Contains("&")) || (txtComponent.Text.Contains("_")) || (txtComponent.Text.Contains("@")) ||
            //  (txtComponent.Text.Contains("{")) || (txtComponent.Text.Contains("}")) || (txtComponent.Text.Contains("[")) || (txtComponent.Text.Contains("]")) ||
            //  (txtComponent.Text.Contains("!")) || (txtComponent.Text.Contains("#")) || (txtComponent.Text.Contains("`")) || (txtComponent.Text.Contains("~")) || (txtComponent.Text.Contains(":")) ||
            //  (txtComponent.Text.Contains(";")) || (txtComponent.Text.Contains("'"))))
            if (((txtComponent.Text.Contains("&")) || (txtComponent.Text.Contains("_")) || (txtComponent.Text.Contains("@")) ||
              (txtComponent.Text.Contains("{")) || (txtComponent.Text.Contains("}")) || (txtComponent.Text.Contains("[")) || (txtComponent.Text.Contains("]")) ||
              (txtComponent.Text.Contains("!")) || (txtComponent.Text.Contains("#")) || (txtComponent.Text.Contains("`")) || (txtComponent.Text.Contains("~")) || (txtComponent.Text.Contains(":")) ||
              (txtComponent.Text.Contains(";")) || (txtComponent.Text.Contains("'"))))
              {
                XtraMessageBox.Show("Component Name is invalid", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtComponent.Focus();
                return false;
              }
            return true;
        }
       
        private bool CheckFormula()
        {
            string sFormulaId = "";
            string sFormula = "";
            string sChkFormulaId = "";
            try
            {
                sFormulaId = BuildFormulaId();
                sChkFormulaId = sFormulaId;
                sChkFormulaId = sChkFormulaId.Replace("<", " ");
                sChkFormulaId = sChkFormulaId.Replace(">", " ");
                sFormula = meFormula.Text.Trim();
                object objCheck = (clsGeneral.ObjExcel).Evaluate(sChkFormulaId);

                if (objCheck.ToString() != "-2146826273" && !sFormulaId.StartsWith("+") && !sFormulaId.StartsWith("-"))
                {
                    XtraMessageBox.Show("Valid Formula", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    objBuild.Formula = txtFormula.Text.Trim();
                    return true;
                }
                else
                {
                    XtraMessageBox.Show("Invalid Formula", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

            }
            catch
            {
                return false;
            }

        }
        #endregion

        #region Formula Methods
        private DataTable createRelationalOperators()
        {
            DataTable dtOperator = new DataTable();
            dtOperator.Columns.Add("OPID", typeof(string));
            dtOperator.Columns.Add("OPNAME", typeof(string));

            dtOperator.Rows.Add("0", "=");
            dtOperator.Rows.Add("1", "<");
            dtOperator.Rows.Add("2", ">");
            dtOperator.Rows.Add("3", ">=");
            dtOperator.Rows.Add("4", "<=");
            dtOperator.Rows.Add("5", "<>");
            dtOperator.Columns["OPID"].ColumnMapping = MappingType.Hidden;
            return dtOperator;
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

        private void SelectIFCondition(int ifOption)
        {
            switch (ifOption)
            {
                case 1:
                    rdoIfcondition.SelectedIndex = 0; // IF..THEN
                    break;
                //  case 2:
                //rdoIfcondition.SelectedIndex = 1;
                //break;
                case 3:
                    rdoIfcondition.SelectedIndex = 1;// IF...ELSE..THEN
                    //lblComponentName.Text = txtComponent.Text + " =";
                    //lblComponentName1.Text = txtComponent.Text + " =";
                    lblComponentName.Text = lbComponent.SelectedText + "=";
                    lblComponentName1.Text = lbComponent2.SelectedText + "=";
                    break;
                //case 4:
                //    rdoIfcondition.SelectedIndex = 3;
                //    //lblComponentName.Text = txtComponent.Text + " =";
                //    //lblComponentName1.Text = txtComponent.Text + " =";
                //    lblComponentName.Text = lbComponent.SelectedText + "=";
                //    lblComponentName1.Text = lbComponent2.SelectedText + "=";
                //    break;
                case 0:
                    rdoIfcondition.SelectedIndex = 2;//FORMULA
                    break;
            }
        }


        private bool checkIfThen()
        {
            if ((txtComponent.Text.Trim() == "{COMPONENT1}") || (txtComponent.Text.Trim() == ""))
            {
                XtraMessageBox.Show("Condition field is empty.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtComponent.Focus();
                return false;
            }
            else if ((lstbOperator1.Text.Trim() == "{OPR1}") || (lstbOperator1.Text.Trim() == ""))
            {
                XtraMessageBox.Show("Operator field is empty.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lstbOperator1.Focus();
                return false;
            }
            else if ((txtValue1.Text.Trim() == "{VALUE1}") || (txtValue1.Text.Trim() == ""))
            {
                XtraMessageBox.Show("Value field is empty.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtValue1.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtFormula.Text.Trim()))
            {
                XtraMessageBox.Show("Formula is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFormula.Focus();
                return false;
            }
            return true;

        }
        private bool ValidateIfThen()
        {
            if ((txtComponent.Text.Trim() == "{COMPONENT1}") || (txtComponent.Text.Trim() == ""))
            {
                txtComponent.Focus();
                return false;
            }
            if ((lstbOperator1.Text.Trim() == "{OPR1}") || (lstbOperator1.Text.Trim() == ""))
            {
                lstbOperator1.Focus();
                return false;
            }
            if ((txtValue1.Text.Trim() == "{VALUE1}") || (txtValue1.Text.Trim() == ""))
            {
                txtValue1.Focus();
                return false;
            }
            if (txtFormula.Text.Trim() == "")
            {
                txtFormula.Focus();
                return false;
            }
            return true;

        }
        private void checkIfAndThen()
        {
            //if ((txtComponent1.Text.Trim() == "{COMPONENT2}") || (txtComponent1.Text.Trim() == ""))
            //{
            //    XtraMessageBox.Show("Componenet2 field can't be empty !", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtComponent1.Focus();
            //    return;
            //}
            //if ((txtOperator2.Text.Trim() == "{OPR2}") || (txtOperator2.Text.Trim() == ""))
            //{
            //    XtraMessageBox.Show("Operator field can't be empty !", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtOperator2.Focus();
            //    return;
            //}
            //if ((txtValue2.Text.Trim() == "{VALUE2}") || (txtValue2.Text.Trim() == ""))
            //{
            //    XtraMessageBox.Show("Value field can't be empty !", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtValue2.Focus();
            //    return;
            //}
        }
        private bool ValidateIfAndThen()
        {
            //if ((txtComponent1.Text.Trim() == "{COMPONENT2}") || (txtComponent1.Text.Trim() == ""))
            //{
            //    txtComponent1.Focus();
            //    return false;
            //}
            //if ((txtOperator2.Text.Trim() == "{OPR2}") || (txtOperator2.Text.Trim() == ""))
            //{
            //    txtOperator2.Focus();
            //    return false;
            //}
            //if ((txtValue2.Text.Trim() == "{VALUE2}") || (txtValue2.Text.Trim() == ""))
            //{
            //    txtValue2.Focus();
            //    return false;
            //}
            return true;
        }

        private bool validateFields()
        {
            if (rdoIfcondition.SelectedIndex == 0)
            {
                if (checkIfThen())
                    return true;
                return false;
            }
            //else if (rdoIfcondition.SelectedIndex == 1)
            //{
            //    if (ValidateIfThen() & ValidateIfAndThen())
            //        return true;
            //    return false;
            //}
            else if (rdoIfcondition.SelectedIndex == 1)
            {
                if (checkIfThen() && (txtFormula1.Text != ""))
                    return true;
                else
                {
                    XtraMessageBox.Show("Formula is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFormula1.Focus();
                    return false;
                }
               
            }
            else
            {
                //if (meFormula.Text.Trim() == "")
                if (txtComponent.Text.Trim() == "")
                {
                    XtraMessageBox.Show("Formula is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtComponent.Focus();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            //else
            //{
            //    if (ValidateIfThen() & ValidateIfAndThen() & txtFormula2.Text != "")
            //        return true;
            //    return false;
            //}
        }
        private bool ValidateFieldsOfPayrollFormula()
        {
            if (rdoIfcondition.SelectedIndex == 0) //if..then
            {
                checkIfThen();
            }

            else if (rdoIfcondition.SelectedIndex == 1)
            {
                checkIfThen();
                if (string.IsNullOrEmpty(txtFormula1.Text.Trim()))
                {
                    XtraMessageBox.Show("Formula is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFormula1.Focus();
                    //btnBuildFormula2.Focus();
                    return false;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtComponent.Text.Trim()))
                {
                    XtraMessageBox.Show("Formula is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtComponent.Focus();
                    return false;
                    //btnBuildFormula1.Focus();

                }

            }
            return true;
        }
        private void FillFormula()
        {
            string formulaGroup = "";
            string[] aformulaGroup;
            string formulaGroupId = "";
            string[] aformulaGroupId;
            string formula = "";

            formulaGroup = parentcondition.txtEquationBuild.Text;
            formulaGroupId = parentcondition.CValue;

            aformulaGroup = formulaGroup.Split('$');
            aformulaGroupId = formulaGroupId.Split('$');

            if (formulaGroup != "")
            {
                for (int i = 0; i < aformulaGroup.Length; i++)
                {
                    formula = aformulaGroup[i] + "$" + aformulaGroupId[i];
                    //lstFormula.Items.Add(formula);
                }
            }
        }

        private bool ConstructFormula(ref string strFormulaConditions, ref string strFinalFormulaId)
        {
            bool validFormula = false;
            strFormulaConditions = "";
            strFinalFormulaId = "";

            if (txtFormula.Text != "")
            {
                ComponentValue1 = BuildFormulaId(txtFormula.Text.Trim());
                ComponentValue1 = ComponentValue1.Replace(" ", "");
                ComponentValue1 = ComponentValue1.Replace(Convert.ToChar(160) + "<", "<");
                ComponentValue1 = ComponentValue1.Replace(">" + Convert.ToChar(160), ">");
            }
            if (txtFormula1.Text != "")
            {
                ComponentValue2 = BuildFormulaId(txtFormula1.Text.Trim());
                ComponentValue2 = ComponentValue2.Replace(" ", "");
                ComponentValue2 = ComponentValue2.Replace(Convert.ToChar(160) + "<", "<");
                ComponentValue2 = ComponentValue2.Replace(">" + Convert.ToChar(160), ">");
            }
            if (txtComponent.Text != "")
            {
                ComponentValue3 = BuildFormulaId(txtComponent.Text.Trim());
                ComponentValue3 = ComponentValue3.Replace(" ", "");
                ComponentValue3 = ComponentValue3.Replace(Convert.ToChar(160) + "<", "<");
                ComponentValue3 = ComponentValue3.Replace(">" + Convert.ToChar(160), ">");
            }
            //  Relational Operators (nOpr1) Id : 1 =, 2 >, 3 <, 4 >=, 5 <=, 6 <>
            //change the operator type
            if (lstbOperator1.Text != "")
            {
                switch (lstbOperator1.Text.Trim())
                {
                    case "=":
                        strOperator = "0";
                        break;
                    case "<":
                        strOperator = "1";
                        break;
                    case ">":
                        strOperator = "2";
                        break;
                    case ">=":
                        strOperator = "3";
                        break;
                    case "<=":
                        strOperator = "4";
                        break;
                    case "<>":
                        strOperator = "5";
                        break;
                }
            }
            if (lstbOperator2.Text != "")
            {
                switch (lstbOperator2.Text.Trim())
                {
                    case "=":
                        strOperator1 = "0";
                        break;
                    case "<":
                        strOperator1 = "1";
                        break;
                    case ">":
                        strOperator = "2";
                        break;
                    case ">=":
                        strOperator1 = "3";
                        break;
                    case "<=":
                        strOperator1 = "4";
                        break;
                    case "<>":
                        strOperator1 = "5";
                        break;
                }
            }


            if (rdoIfcondition.SelectedIndex == 0) // if.....then
            {
                strFormulaConditions = "{IF}" + Convert.ToChar(160) + "{" + txtComponent.Text + "}" + Convert.ToChar(160) + "{" + lstbOperator1.Text + "}" + Convert.ToChar(160) + "{" + txtValue1.Text + "}" + Convert.ToChar(160) + "{THEN}" + Convert.ToChar(160) + "{" + txtFormula.Text + "}";
                strFinalFormulaId = "{" + ComponentValue3 + "}" + Convert.ToChar(160) + "{" + strOperator + "}" + Convert.ToChar(160) + "{" + txtValue1.Text + "}" + Convert.ToChar(160) + "{" + ComponentValue1 + "}~1";

                //Parent.txtEquation.Text = "{IF}"+ Convert.ToChar(160) +"{"+ txtComponent.Text + "}" +Convert.ToChar(160) + "{"+  txtOperator1.Text + "}" + Convert.ToChar(160) +"{"+ txtValue1.Text + "}" + Convert.ToChar(160) +"{THEN}"+ Convert.ToChar(160) +"{"+  txtFormula1.Text + "}";
                //Parent.CValue           = "{"+ htFormula[txtComponent.Text.ToString()] + "}" + Convert.ToChar(160) +"{"+  strOperator + "}" + Convert.ToChar(160) +"{"+ txtValue1.Text + "}" + Convert.ToChar(160) +"{"+  ComponentValue1 + "}";

            }
            //else if (rdoIfcondition.SelectedIndex == 1)//if...and..then
            //{
            //    strFormulaConditions = "{IF}" + Convert.ToChar(160) + "{" + lbComponent.Text + "}" + Convert.ToChar(160) + "{" + lstbOperator1.Text + "}" + Convert.ToChar(160) + "{" + txtValue1.Text + "}" + Convert.ToChar(160) + "{" + lbLogicalOperator.SelectedItem.ToString() + "}" + Convert.ToChar(160) + "{" + lbComponent2.Text + "}" + Convert.ToChar(160) + "{" + lstbOperator2.Text + "}" + Convert.ToChar(160) + "{" + txtValue2.Text + "}" + Convert.ToChar(160) + "{THEN}" + Convert.ToChar(160) + "{" + txtFormula1.Text + "}";
            //    strFinalFormulaId = "{" + htFormula[lbComponent.Text] + "}" + Convert.ToChar(160) + "{" + strOperator + "}" + Convert.ToChar(160) + "{" + txtValue1.Text + "}" + Convert.ToChar(160) + "{" + strLogical + "}" + Convert.ToChar(160) + "{" + htFormula[lbComponent2.Text] + "}" + Convert.ToChar(160) + "{" + strOperator1 + "}" + Convert.ToChar(160) + "{" + txtValue2.Text + "}" + Convert.ToChar(160) + "{" + ComponentValue1 + "}~2";

            //    //Parent.txtEquation.Text     = "{IF}"+Convert.ToChar(160)+"{"+txtComponent.Text+ "}" + Convert.ToChar(160) +"{"+txtOperator1.Text + "}" +Convert.ToChar(160) + "{"+txtValue1.Text + "}"+Convert.ToChar(160) + "{"+txtLogicalOperator.Text + "}" +Convert.ToChar(160)+"{"+txtComponent1.Text + "}" +Convert.ToChar(160)+ "{"+txtOperator2.Text + "}" + Convert.ToChar(160) + "{"+txtValue2.Text + "}"+ Convert.ToChar(160) +"{THEN}"+ Convert.ToChar(160)+"{" + txtFormula1.Text + "}";
            //    //Parent.txtEquation.ReadOnly = true;
            //    //Parent.rdoEquation.Checked  = true;
            //    //Parent.txtMaxSlab.Focus();
            //    //Parent.CValue               = "{"+ htFormula[txtComponent.Text.ToString()] + "}" + Convert.ToChar(160) +"{"+strOperator + "}" + Convert.ToChar(160) +"{"+txtValue1.Text + "}" + Convert.ToChar(160) +"{"+ strLogical + "}" + Convert.ToChar(160) +"{"+ htFormula[txtComponent1.Text.ToString()] + "}" + Convert.ToChar(160) +"{"+ strOperator1 + "}" + Convert.ToChar(160) +"{"+txtValue2.Text + "}"+ Convert.ToChar(160) +"{" + ComponentValue1 + "}";
            //}
            else if (rdoIfcondition.SelectedIndex == 1)//if..else
            {
                strFormulaConditions = "{IF}" + Convert.ToChar(160) + "{" + txtComponent.Text + "}" + Convert.ToChar(160) + "{" + lstbOperator1.Text + "}" + Convert.ToChar(160) + "{" + txtValue1.Text + "}" + Convert.ToChar(160) + "{THEN}" + Convert.ToChar(160) + "{" + txtFormula.Text + "}" + Convert.ToChar(160) + "{ELSE}" + Convert.ToChar(160) + "{" + txtFormula1.Text + "}";
                strFinalFormulaId = "{" + ComponentValue3 + "}" + Convert.ToChar(160) + "{" + strOperator + "}" + Convert.ToChar(160) + "{" + txtValue1.Text + "}" + Convert.ToChar(160) + "{" + ComponentValue1 + "}" + Convert.ToChar(160) + "{" + ComponentValue2 + "}~3";

                //Parent.txtEquation.Text     = "{IF}"+Convert.ToChar(160)+"{"+ txtComponent.Text + "}" + Convert.ToChar(160) +"{"+txtOperator1.Text + "}" + Convert.ToChar(160) +"{" + txtValue1.Text + "}" + Convert.ToChar(160) +"{THEN}"+Convert.ToChar(160)+"{" + txtFormula1.Text + "}" + Convert.ToChar(160)+"{ELSE}"+Convert.ToChar(160) +"{"+ txtFormula2.Text + "}";
                //Parent.txtEquation.ReadOnly = true;
                //Parent.rdoEquation.Checked  = true;
                //Parent.CValue               = "{"+ htFormula[txtComponent.Text.ToString()] + "}" + Convert.ToChar(160) +"{"+strOperator + "}" + Convert.ToChar(160) +"{" + txtValue1.Text + "}" + Convert.ToChar(160) +"{" + ComponentValue1 + "}" + Convert.ToChar(160) +"{"+ ComponentValue2 + "}";
            }
            //else if (rdoIfcondition.SelectedIndex == 3)//if...and..else..then
            //{
            //    strFormulaConditions = "{IF}" + Convert.ToChar(160) + "{" + lbComponent.Text + "}" + Convert.ToChar(160) + "{" + lstbOperator1.Text + "}" + Convert.ToChar(160) + "{" + txtValue1.Text + "}" + Convert.ToChar(160) + "{" + lbLogicalOperator.SelectedItem.ToString() + "}" + Convert.ToChar(160) + "{" + lbComponent2.Text + "}" + Convert.ToChar(160) + "{" + lstbOperator2.Text + "}" + Convert.ToChar(160) + "{" + txtValue2.Text + "}" + Convert.ToChar(160) + "{THEN}" + Convert.ToChar(160) + "{" + txtFormula1.Text + "}" + Convert.ToChar(160) + "{ELSE}" + Convert.ToChar(160) + "{" + txtFormula2.Text + "}";
            //    strFinalFormulaId = "{" + htFormula[lbComponent.Text] + "}" + Convert.ToChar(160) + "{" + strOperator + "}" + Convert.ToChar(160) + "{" + txtValue1.Text + "}" + Convert.ToChar(160) + "{" + strLogical + "}" + Convert.ToChar(160) + "{" + htFormula[lbComponent2.SelectedText] + "}" + Convert.ToChar(160) + "{" + strOperator1 + "}" + Convert.ToChar(160) + "{" + txtValue2.Text + "}" + Convert.ToChar(160) + "{" + ComponentValue1 + "}" + Convert.ToChar(160) + "{" + ComponentValue2 + "}~4";

            //    //Parent.txtEquation.Text     = "{IF}"+Convert.ToChar(160)+"{"+txtComponent.Text+ "}" + Convert.ToChar(160) +"{"+txtOperator1.Text + "}" + Convert.ToChar(160) +"{"+txtValue1.Text + "}" + Convert.ToChar(160) +"{"+txtLogicalOperator.Text + "}" + Convert.ToChar(160) +"{"+txtComponent1.Text + "}" + Convert.ToChar(160) +"{"+txtOperator2.Text + "}" + Convert.ToChar(160) +"{"+txtValue2.Text + "}"+ Convert.ToChar(160) +"{THEN}"+Convert.ToChar(160)+"{" + txtFormula1.Text + "}"+ Convert.ToChar(160) +"{ELSE}"+Convert.ToChar(160)+"{" + txtFormula2.Text + "}";
            //    //Parent.txtEquation.ReadOnly = true;
            //    //Parent.rdoEquation.Checked  = true;
            //    //Parent.txtMaxSlab.Focus();
            //    //Parent.CValue               = "{"+ htFormula[txtComponent.Text.ToString()] + "}" + Convert.ToChar(160) +"{"+strOperator + "}" + Convert.ToChar(160) +"{"+ txtValue1.Text + "}" + Convert.ToChar(160) +"{"+ strLogical +"}" + Convert.ToChar(160) +"{"+ htFormula[txtComponent1.Text.ToString()] + "}" + Convert.ToChar(160)+"{"+strOperator1 + "}" + Convert.ToChar(160) +"{"+txtValue2.Text + "}"+ Convert.ToChar(160) +"{" + ComponentValue1 + "}"+ Convert.ToChar(160) +"{" + ComponentValue2 + "}";
            //}
            else
            {

                strFormulaConditions = txtComponent.Text;
                //strFinalFormulaId			 = txtFormula1.Text + "~0"; // + "!0";
                strFinalFormulaId = BuildFormulaId(txtComponent.Text) + "~0";

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
                XtraMessageBox.Show("Formula is attached", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                validFormula = false;
                //XtraMessageBox.Show("Invalid formula", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            return validFormula;
        }


        private void EditFormula(string formula, string formulaId, int ifCondition)
        {
            int nIdx;
            string[] aFormulaId = new string[50];
            try
            {
                if (ifCondition == 0)//FORMULA
                {
                    //txtFormula2.Visible = true;
                    meFormula.Text = formula;
                    txtComponent.Text = formula;
                    //txtformula.Text = formula;
                }
                else
                {
                    if (formulaId != "")
                    {
                        aFormulaId = formulaId.Split(Convert.ToChar(160));
                        //nIdx = Int32.Parse(RemoveBrace(aFormulaId[0].ToString(), false));
                        //lbComponent.EditValue = nIdx;

                        txtComponent.Tag = RemoveBrace(aFormulaId[0].ToString(), false);
                        if (txtComponent.Tag.ToString() != "")
                        {
                            txtComponent.Text = BuildFormulaId(txtComponent.Tag.ToString());
                            //txtComponent.Text = txtComponent.Text.Substring(0, txtComponent.Text.IndexOf('~'));
                        }

                        // lbComponent.EditValue = htID[nIdx.ToString()].ToString();
                        nIdx = Int32.Parse(RemoveBrace(aFormulaId[1].ToString(), false));

                        // if (nIdx > 0)
                        //lstbOperator1.EditValue = nIdx - 1;
                        lstbOperator1.EditValue = nIdx;
                        txtValue1.Text = RemoveBrace(aFormulaId[2].ToString(), false).ToString();

                        if ((ifCondition == 1) || (ifCondition == 3))
                        {
                            if (ifCondition == 1)//IF...THEN
                            {
                                txtFormula.Tag = RemoveBrace(aFormulaId[3].ToString(), false);
                                if (txtFormula.Tag.ToString() != "")
                                {
                                    txtFormula.Text = BuildFormulaId(txtFormula.Tag.ToString());
                                    txtFormula.Text = txtFormula.Text.Substring(0, txtFormula.Text.IndexOf('~'));
                                }
                            }
                            if (ifCondition == 3)
                            {
                                txtFormula.Tag = RemoveBrace(aFormulaId[3].ToString(), false);
                                if (txtFormula.Tag.ToString() != "")
                                {
                                    txtFormula.Text = BuildFormulaId(txtFormula.Tag.ToString());
                                    if (txtFormula.Text.Contains("~"))
                                    {
                                        txtFormula.Text = txtFormula.Text.Substring(0, txtFormula.Text.IndexOf('~'));
                                    }
                                    meFormula.Text = txtFormula.Text;
                                }
                                txtFormula1.Tag = RemoveBrace(aFormulaId[4].ToString(), false);
                                if (txtFormula1.Tag.ToString() != "")
                                {
                                    txtFormula1.Text = BuildFormulaId(txtFormula1.Tag.ToString());
                                    if (txtFormula1.Text.Contains("~"))
                                    {
                                        txtFormula1.Text = txtFormula1.Text.Substring(0, txtFormula1.Text.IndexOf('~'));
                                    }

                                }
                            }
                        }
                        else if (ifCondition == 2 || ifCondition == 4)
                        {
                            nIdx = Int32.Parse(RemoveBrace(aFormulaId[3].ToString(), false));

                            if (nIdx > 0)
                            {
                                ///lbLogicalOperator.SelectedIndex = nIdx - 1;
                                lbLogicalOperator.Text = lbLogicalOperator.Items[nIdx - 1].ToString();
                            }
                            nIdx = Int32.Parse(RemoveBrace(aFormulaId[4].ToString(), false));
                            if (nIdx > 0)
                                lbComponent2.EditValue = nIdx;
                            //lbComponent2.EditValue = htID[nIdx.ToString()].ToString();
                            nIdx = Int32.Parse(RemoveBrace(aFormulaId[5].ToString(), false));
                            if (nIdx > 0)
                            {
                                lstbOperator2.EditValue = nIdx - 1;
                                //lstbOperator2.SelectedText = lstbOperator2.Items[nIdx - 1].ToString();
                            }
                            txtValue2.Text = RemoveBrace(aFormulaId[6].ToString(), false);
                            txtFormula1.Tag = RemoveBrace(aFormulaId[7].ToString(), false);
                            if (txtFormula1.Tag.ToString() != "")
                            {
                                txtFormula.Text = BuildFormulaId(txtFormula.Tag.ToString());
                                if (txtFormula1.Text.Contains("~"))
                                {
                                    txtFormula1.Text = txtFormula1.Text.Substring(0, txtFormula1.Text.IndexOf('~'));
                                }
                                meFormula.Text = txtFormula1.Text;
                            }
                            if (ifCondition == 4)
                            {
                                txtFormula2.Tag = RemoveBrace(aFormulaId[8].ToString(), false);
                                if (txtFormula2.Tag.ToString() != "")
                                {
                                    txtFormula2.Text = BuildFormulaId(txtFormula2.Tag.ToString());
                                    if (txtFormula2.Text.Contains("~"))
                                    {
                                        txtFormula2.Text = txtFormula2.Text.Substring(0, txtFormula2.Text.IndexOf('~'));
                                    }
                                    meFormula.Text = txtFormula2.Text;
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

        private void Editexistingformula()
        {
            string formulaGroup = "";
            string formula = "";
            string formulaId = "";
            string[] aformulaGroup;
            string[] aformulaId;

            int ifId = 0;
            string formulaGroupId = "";

            //if (frmmode == "Edit")
            //{
            newFormula = false;

            formulaGroup = parentcondition.txtEquationBuild.Text + '$' + parentcondition.CValue;
            aformulaGroup = formulaGroup.Split('$');
            aformulaId = aformulaGroup[1].Split('~');

            formula = aformulaGroup[0];
            formulaId = aformulaGroup[1];

            if (aformulaId.Length > 1)
            {
                ifId = Convert.ToInt32(aformulaId[1]);
                formulaGroupId = aformulaId[2];
            }


            SelectIFCondition(ifId);
            EditFormula(formula, formulaId, ifId);
            // }

            //btnNew.Enabled = true;
        }

        private bool TestFormula()
        {
            bool Rtn = false;
            ValidateComponentSymbols();
            if (rdoIfcondition.SelectedIndex == 0) //IF...then
            {
                if (checkIfThen())
                {
                    CheckedFormula = CheckFormula();
                }
            }
            else if (rdoIfcondition.SelectedIndex == 1)  //IF...ELSE..THEN
            {
                if (checkIfThen())
                {
                    if (!string.IsNullOrEmpty(txtFormula1.Text))
                    {
                        CheckedFormula = CheckFormula();
                    }
                    else
                    {
                        XtraMessageBox.Show("Formula is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtFormula1.Focus();
                    }
                }
            }
            else //FORMULA
            {
                if (string.IsNullOrEmpty(txtComponent.Text.Trim()))
                {
                    XtraMessageBox.Show("Formula is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtComponent.Focus();
                }
                else
                {
                    CheckedFormula = CheckFormula();
                    if (!CheckedFormula)
                    {
                        txtComponent.Focus();
                    }
                }
            }
            Rtn = CheckedFormula;
            return Rtn;
        }
        #endregion

        #region Events
        private void frmCostructFormula_Load(object sender, EventArgs e)
        {
            rdoIfcondition.SelectedIndex = 2;
            lblComponentName.Text = parentcondition.txtComponent.Text + "=";
            // this.Height = 342;
            DataTable dtBindOperator = createRelationalOperators();
            try
            {
                DataTable dtComponent = FetchRecords(SQLCommand.Payroll.PayrollComponentFetch) as DataTable;
                dtComponent.Columns["COMPONENTID"].ColumnMapping = MappingType.Hidden;
                lbComponent.Properties.DataSource = dtComponent;
                lbComponent.Properties.DisplayMember = "COMPONENT";
                lbComponent.Properties.ValueMember = "COMPONENTID";

                lbComponent2.Properties.DataSource = dtComponent;
                lbComponent2.Properties.DisplayMember = "COMPONENT";
                lbComponent2.Properties.ValueMember = "COMPONENTID";

                lstbOperator1.Properties.DataSource = dtBindOperator;
                lstbOperator1.Properties.DisplayMember = "OPNAME";
                lstbOperator1.Properties.ValueMember = "OPID";

                lstbOperator2.Properties.DataSource = dtBindOperator;
                lstbOperator2.Properties.DisplayMember = "OPNAME";
                lstbOperator2.Properties.ValueMember = "OPID";
                //objCommon.BindGridLookUpCombo(lbComponent, FetchRecords(SQLCommand.Payroll.PayrollComponentFetch), "COMPONENT", "COMPONENTID");
                //objCommon.BindGridLookUpCombo(lbComponent2, FetchRecords(SQLCommand.Payroll.PayrollComponentFetch), "COMPONENT", "COMPONENTID");
                //objCommon.BindGridLookUpCombo(lstbOperator1, dtBindOerator, "OPNAME", "OPID");
                //objCommon.BindGridLookUpCombo(lstbOperator2, dtBindOerator, "OPNAME", "OPID");
                DataTable dt = objPayrollComp.getPayrollComponent();

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
                    XtraMessageBox.Show("There is no component available. Add Default Components", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }

                FillFormula();
                //if (frmmode == "Edit")
                //{
                Editexistingformula();
                //}

                if (meFormula.Text != "")
                {
                    //meFormula.Text = meFormula.Text.Remove(meFormula.Text.Length - 1, 1);
                    meFormula.SelectionStart = meFormula.Text.Length + 1;
                }

                DataTable dtComponenttype = objPayrollComp.getPayrollComponentWithType();
                if (dtComponenttype.Rows.Count > 0)
                {
                    //show earning, deduction and Text with number value
                    string filter = "'" + PayRollExtraPayInfo.EARNING1.ToString() + "','" + PayRollExtraPayInfo.EARNING1.ToString() + "'," +
                                    "'" + PayRollExtraPayInfo.EARNING2.ToString() + "','" + PayRollExtraPayInfo.EARNING3.ToString() + "'," +
                                    "'" + PayRollExtraPayInfo.DEDUCTION1.ToString() + "','" + PayRollExtraPayInfo.DEDUCTION2.ToString() + "'," +
                                    "'" + PayRollExtraPayInfo.PAYING_SALARY_DAYS.ToString().Replace("_", "") + "'," +
                                    "'" + PayRollExtraPayInfo.BASICPAY.ToString() + "'," +
                                    "'TOTALDAYSINPAYMONTH'";

                    dtComponenttype.DefaultView.RowFilter = "LINKVALUE='' OR LINKVALUE IN (" + filter + ")";

                    //On 30/11/2020, Show default Loan component which contains Loan:
                    dtComponenttype.DefaultView.RowFilter += " OR LINKVALUE LIKE 'Loan :%'";

                    dtComponenttype = dtComponenttype.DefaultView.ToTable();

                    foreach (DataRow dr in dtComponenttype.Rows)
                    {
                        lstComponent.Items.Add("<" + dr[1].ToString() + ">");
                        if (!htList.Contains("<" + dr[1].ToString() + ">"))
                            htList.Add("<" + dr[1].ToString() + ">", dr[0].ToString());
                    }
                }
                else
                {
                    XtraMessageBox.Show("There is no Component available", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                rdoIfcondition_SelectedIndexChanged(sender, e);
            }
            catch(Exception err)
            {
                return;
            }
            
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            txtFormula2.Properties.ReadOnly = false;
            txtFormula2.Properties.ReadOnly = false;
            if (CheckedFormula == true)
            {
                if (Comp == 1)
                {
                    txtFormula2.Text = meFormula.Text.Trim();
                    this.Close();
                }
                else
                {
                    txtFormula2.Text = meFormula.Text.Trim();
                    this.Close();
                }

            }
            else
            {
                if (CheckFormula())
                {
                    if (Comp == 1)

                        txtFormula2.Text = meFormula.Text.Trim();
                    else
                        txtFormula2.Text = meFormula.Text.Trim();
                }
            }
            this.Close();
        }
        private void btnTest_Click(object sender, EventArgs e)
        {
            TestFormula();
        }

        private void lstComponent_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string sComponentSelected = lstComponent.SelectedItem.ToString();
                GetComponentId(ref sComponentSelected);
                string sComponent1 = sComponentSelected.Trim();
                string sId = sComponent1.Substring(1, sComponent1.Length - 2);
                if (sId == lCompId.ToString())
                {
                    XtraMessageBox.Show(lstComponent.SelectedItem.ToString() + " component can not refer by itself", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                InsertText(lstComponent.SelectedItem.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void lstOperators_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                InsertText(lstOperators.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Formula Events
        private void rdoIfcondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtActiveControl = null;
            if (rdoIfcondition.SelectedIndex == 0)     //if..then
            {
                this.Height = 280;
                lblComponentName.Text = parentcondition.txtComponent.Text + " =";
                lblIf.Text = "If";

                lblIf.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem23.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem11.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem12.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                emptySpaceItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem22.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                emptySpaceItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lblElse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                emptySpaceItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem21.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem19.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            //else if (rdoIfcondition.SelectedIndex == 1)  //if...and..then
            //{
            // lblComponentName.Text = parentcondition.txtComponent.Text + " =";

               // layoutControlItem21.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // layoutControlItem22.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // // lblComponentName1.Text = parentcondition.txtComponent.Text + " =";
            // layoutControlItem20.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // //emptySpaceItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            // //emptySpaceItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

               // //emptySpaceItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //// emptySpaceItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            // //emptySpaceItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // layoutControlItem10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            // layoutControlItem11.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            // layoutControlItem12.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            // layoutControlItem13.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            // layoutControlItem14.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            // layoutControlItem15.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            // layoutControlItem16.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            // //emptySpaceItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            // layoutControlItem19.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            // layoutControlItem17.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            // layoutControlItem18.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //}
            else if (rdoIfcondition.SelectedIndex == 1)  //if..else...then
            {
                this.Height = 320;
                lblComponentName.Text = parentcondition.txtComponent.Text + " =";
                lblIf.Text = "If";
                lblComponentName1.Text = parentcondition.txtComponent.Text + " =";

                lblIf.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem23.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem11.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem12.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                emptySpaceItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem22.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                emptySpaceItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lblElse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                emptySpaceItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem21.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem19.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            //else if (rdoIfcondition.SelectedIndex == 3)  //if...and...else..then
            //{
            // lblComponentName.Text = parentcondition.txtComponent.Text + " =";
            // lblComponentName1.Text = parentcondition.txtComponent.Text + " =";

               // layoutControlItem21.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // layoutControlItem22.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

               // layoutControlItem20.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //// emptySpaceItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            // //emptySpaceItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

               // //emptySpaceItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //// emptySpaceItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //// emptySpaceItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            // layoutControlItem10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            // layoutControlItem11.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            // layoutControlItem12.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            // layoutControlItem13.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            // layoutControlItem14.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            // layoutControlItem15.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            // layoutControlItem16.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            // //emptySpaceItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            // layoutControlItem19.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            // layoutControlItem17.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            // layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            // layoutControlItem18.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //}
            else if (rdoIfcondition.SelectedIndex == 2) //formula
            {
                this.Height = 250;
                //lblComponentName.Text = parentcondition.txtComponent.Text + " =";
                lblIf.Text = parentcondition.txtComponent.Text + " =";
                //lblFormula.Text = parentcondition.txtComponent.Text + " =";
                lblIf.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem23.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem11.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem12.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                emptySpaceItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem22.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                emptySpaceItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lblElse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                emptySpaceItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem21.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem19.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string strFormula = "";
            string strFormulaId = "";
            string formulaStaffGroupId = "0";
            string formulaGroup = "";
            string formula = "";
            string formulaId = "";
            string formula1 = "";
            string formulaId1 = "";
            string[] aformulaGroup;
            string[] aformulaId;
            int formulaGroupId = 0;

            bool isValid = TestFormula();
            if (isValid)
            {
                ValidateComponentSymbols();
                if (ConstructFormula(ref strFormula, ref strFormulaId))
                {
                    if (strFormula != "")
                    {
                        strFormula += "$" + strFormulaId + "~" + formulaStaffGroupId;
                    }
                }
                else
                {
                    return;
                }
                formulaGroup = strFormula;
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

                if (formula1 != "")
                {
                    formula = formula1 + (formula == "" ? "" : ("$" + formula));
                    formulaId = formulaId1 + (formulaId == "" ? "" : ("$" + formulaId));
                }

                parentcondition.txtEquationBuild.Text = formula;
                parentcondition.CValue = formulaId;
                parentcondition.IFCon = 1;

                txtFormula2.Properties.ReadOnly = false;
                txtFormula2.Properties.ReadOnly = false;
                if (CheckedFormula == true)
                {
                    if (Comp == 1)
                    {
                        txtFormula2.Text = meFormula.Text.Trim();
                        this.Close();
                    }
                    else
                    {
                        txtFormula2.Text = meFormula.Text.Trim();
                        this.Close();
                    }
                }
                else
                {
                    if (Comp == 1)

                        txtFormula2.Text = meFormula.Text.Trim();
                    else
                        txtFormula2.Text = meFormula.Text.Trim();
                }
                this.Close();
            }
        }
        #endregion

        private void lbLogicalOperator_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtComponent_Enter(object sender, EventArgs e)
        {
            txtActiveControl = txtComponent;
        }

        private void txtFormula_Enter(object sender, EventArgs e)
        {
            txtActiveControl = txtFormula;
        }

        private void txtFormula1_Enter(object sender, EventArgs e)
        {
            txtActiveControl = txtFormula1;
        }

        private void txtValue1_Enter(object sender, EventArgs e)
        {
            txtActiveControl = txtValue1;
        }

        private void txtComponent_Leave(object sender, EventArgs e)
        {
            ValidateComponentSymbols();
        }

        private void frmpayrollconstructformula_Activated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtComponent.Text))
            {
                txtComponent.Select(txtComponent.Text.Length, 0);
                txtComponent.Focus();
            }
        }
    }
}