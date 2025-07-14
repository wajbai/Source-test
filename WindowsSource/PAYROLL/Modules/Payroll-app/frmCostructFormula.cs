/*****************************************************************************************
*					Interface       : frmConstructFormula
*					Purpose         : Build Formula for Payroll.
*					Date from       : 02-Dec-2014
*					Author          : Adaikala Praveen
*					Modified by     : 
*****************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;


using Bosco.Utility.Common;
using Payroll.Model.UIModel;
using System.Collections;
using Bosco.Utility;
using System.Text.RegularExpressions;


namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmCostructFormula :frmPayrollBase
    {
        #region Declaration
        public Hashtable htList = new Hashtable();
        private long lCompId = 0;
        private new frmBuildIfFormula Parent;
        private bool CheckedFormula = false;
        private int iFormula;
        public clsprCompBuild objBuild = new clsprCompBuild();
        public clsPayrollComponent objPayrollComp = new clsPayrollComponent();
        #endregion

        #region Properties

        #endregion

        #region Constructor
        public frmCostructFormula()
        {
            InitializeComponent();
        }
        public frmCostructFormula(frmBuildIfFormula frm, int iFormulaType, long compId)
            : this(frm, iFormulaType)
        {
            this.lCompId = compId;
        }

        public frmCostructFormula(frmBuildIfFormula frm, int iFormulaType)
        {
            InitializeComponent();
            Parent = frm;
            iFormula = iFormulaType;
            string strComName = Parent.lblComponentName.Text;
            if (strComName != "")
            {
                strComName = strComName.Substring(0, strComName.Length - 1);
                lblFormulaFor.Text += " " + strComName;
            }
            if ((Parent.txtFormula1.Text != "") & (iFormula == 1))
            {
                meFormula.Text = Parent.txtFormula1.Text;

            }
            if ((Parent.txtFormula1.Text != "") & (iFormula == 2))
            {
                meFormula.Text = Parent.txtFormula2.Text;

            }
        }
        #endregion

        #region Events
        private void frmCostructFormula_Load(object sender, EventArgs e)
        {
            try
            {
                if (meFormula.Text != "")
                {
                    //meFormula.Text = meFormula.Text.Remove(meFormula.Text.Length - 1, 1);
                   meFormula.SelectionStart = meFormula.Text.Length + 1;
                }

                DataTable dt = objPayrollComp.getPayrollComponentWithType();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        lstComponent.Items.Add("<" + dr[1].ToString() + ">");
                        if (!htList.Contains("<" + dr[1].ToString() + ">"))
                            htList.Add("<" + dr[1].ToString() + ">", dr[0].ToString());
                    }
                }
                else
                {
                    //XtraMessageBox.Show("There is no Component available", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.ConstructFormula.COSNSTRUCT_FORMULA_COMPONENT_INFO));
                    return;
                }

            }
            catch
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
            Parent.txtFormula1.Properties.ReadOnly = false;
            Parent.txtFormula2.Properties.ReadOnly = false;
            if (CheckedFormula == true)
            {
                if (Parent.Comp == 1)
                {
                    Parent.txtFormula1.Text = meFormula.Text.Trim();
                    this.Close();
                }
                else
                {
                    Parent.txtFormula2.Text = meFormula.Text.Trim();
                    this.Close();
                }

            }
            else
            {
                if (CheckFormula())
                {
                    if (Parent.Comp == 1)

                        Parent.txtFormula1.Text = meFormula.Text.Trim();
                    else
                        Parent.txtFormula2.Text = meFormula.Text.Trim();
                }
            }
            this.Close();
        }
        private void btnTest_Click(object sender, EventArgs e)
        {
            if (meFormula.Text != "")
            {
                CheckedFormula = CheckFormula();
            }
            else
            {
                //XtraMessageBox.Show("Enter the formula !", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                XtraMessageBox.Show(this.GetMessage(MessageCatalog.Payroll.ConstructFormula.COSNSTRUCT_FORMULA_ENTER_FORMULA_INFO),this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
                    //XtraMessageBox.Show(lstComponent.SelectedItem.ToString() + " component can not refer by itself", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    XtraMessageBox.Show(lstComponent.SelectedItem.ToString() + this.GetMessage(MessageCatalog.Payroll.ConstructFormula.COSNSTRUCT_FORMULA_COMP_REFER_INFO),this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                InsertText(lstComponent.SelectedItem.ToString());
            }
            catch { }
        }
        private void lstOperators_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                InsertText(lstOperators.SelectedItem.ToString());
            }
            catch { }
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
            try
            {
                int iPos;
                iPos = meFormula.SelectionStart;
                meFormula.Text = meFormula.Text.Insert(iPos, strText);
                meFormula.SelectionStart = meFormula.Text.Length + 1;
                meFormula.Focus();

                meFormula.SelectAll();
                meFormula.Select(meFormula.SelectionLength, 0);
                meFormula.ScrollToCaret();
            }
            catch
            {

            }

        }
        private string BuildFormulaId()
        {
            int i, nPos, nPos1, nStartPos;
            string sCompId = "";
            string sCompId1 = "";
            string sComp = "";
            sCompId = meFormula.Text;
            nStartPos = 0;
            try
            {
                for (i = 0; i < meFormula.Text.Length; i++)
                {
                    nPos = meFormula.Text.IndexOf("<", nStartPos);
                    if (nPos >= 0)
                    {
                        nStartPos = nPos + 1;
                        nPos1 = meFormula.Text.IndexOf(">", nStartPos);
                        nStartPos = nPos1 + 1;
                        if (nPos1 > 0)
                        {
                            sComp = meFormula.Text.Substring(nPos, nPos1 - (nPos - 1));
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

                if (objCheck.ToString() != "-2146826273")
                {
                    //XtraMessageBox.Show("Valid Formula !", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.ConstructFormula.COSNSTRUCT_FORMULA_VALID_FORMULA_INFO));
                    objBuild.Formula = meFormula.Text.Trim();
                    return true;
                }
                else
                {
                    //XtraMessageBox.Show("Invalid Formula !", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    XtraMessageBox.Show(this.GetMessage(MessageCatalog.Payroll.ConstructFormula.COSNSTRUCT_FORMULA_INVALID_FORMULA_INFO),this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }
            catch
            {
                return false;
            }

        }

        #endregion

    }
}