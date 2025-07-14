using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using System.Reflection;
using System.Text.RegularExpressions;
using Payroll.Model.UIModel;
using Bosco.DAO.Data;
using Payroll.DAO.Schema;
using Bosco.Utility;
using Bosco.Utility.ConfigSetting;
using Bosco.Utility.CommonMemberSet;
using Bosco.Utility.Common;
using ACPP.Modules;
using Bosco.Model.UIModel;

namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmComponentAdd : frmPayrollBase
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
        private frmConstructRangeFormula parentRangeFormula = new frmConstructRangeFormula();
        CommonMember commonmem = new CommonMember();
        CommonMember UtilityMember = new CommonMember();
        private DataTable dt = new DataTable();
        public string CValue = "";	   //Component Equation Id
        public int ComponentId = 0;
        private string sCompStr = "";
        private string StrValue = "";
        private string strLinkValue = "";
        public int IFCon = 0;
        int AccFlag = 0;
        int IsEditable = 0;
        int DontShowInBrowse = 0;
        private DataView dvComponents = null;
        private string sCircularComponentName = "";
        ResultArgs resultArgs = null;
        private Regex objR = new Regex("[0-9]|\b");
        public event EventHandler UpdateHeld;
        FormMode frmmode = new FormMode();
        private int LedgerAccId = 0;
        Random rand = new Random();

        public string PrevCompName = string.Empty;

        #endregion

        #region Constructor

        public frmComponentAdd()
        {
            InitializeComponent();
            lcLblNote.Text = " " ;
            LoadProcessComponentValues();
        }

        public frmComponentAdd(string strAdd)
            : this()
        {
            this.Height = 420; //397;
            //this.Text = "Component (Add)";
            this.Text = this.GetMessage(MessageCatalog.Payroll.Component.COMPONENT_ADD_CAPTION);
            cbeType.SelectedIndex = 0;
            strOperation = strAdd;
            // LoadProcessLedgers();
        }
        public frmComponentAdd(int cId, string stredit)
            : this()
        {
            //this.Text = "Component (Edit)";
            this.Text = this.GetMessage(MessageCatalog.Payroll.Component.COMPONENT_EDIT_CAPTION);
            this.Height = 420; //397;
            ComponentId = cId;
            EnableControls();
            //cbeType.SelectedIndex = 0;
            AssignProperties();
            strOperation = stredit;
        }

        #endregion

        #region Properties

        private string LedgerName { get; set; }
        private string Ledgerid { get; set; }
        private DataTable dtProject { get; set; }
        public DataTable dtRangeSource { get; set; }
        public int Payable { get; set; }

        #endregion

        #region Payroll Transaction Methods

        private void CheckLedgerExists()
        {
            Processtype LedgerProcessType = new Processtype();
            DataView dvLedgerComponent = GetDescriptionfromEnumType(LedgerProcessType);
            DataTable dtLedgerComponent = dvLedgerComponent.ToTable();
            DataTable dtLedgerIds = new DataTable();
            foreach (DataRow dr in dtLedgerComponent.Rows)
            {
                LedgerName = dr["Name"].ToString();
                resultArgs = Objprcomponent.IsLedgerExists(LedgerName);
                if (resultArgs.Success)
                {
                    if (resultArgs.DataSource.Sclar.ToInteger <= 0)
                    {
                        resultArgs = ProcessLedger(LedgerName);
                    }
                    resultArgs = Objprcomponent.FetchLedger(LedgerName);
                    dtLedgerIds = resultArgs.DataSource.Table;
                    if (dtLedgerIds.Rows.Count > 0)
                    {
                        foreach (DataRow datarow in dtLedgerIds.Rows)
                        {
                            Ledgerid += datarow["LEDGER_ID"].ToString() + ",";
                        }
                    }
                }

            }
            Ledgerid = Ledgerid.TrimEnd(',');
        }

        private void LoadProcessLedgers()
        {
            //CheckLedgerExists();
            resultArgs = Objprcomponent.FetchLedgerById(Ledgerid);
            using (MappingSystem mappingSystem = new MappingSystem())
            {
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    commonmem.ComboSet.BindGridLookUpCombo(glkpProcessledger, resultArgs.DataSource.Table, mappingSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, mappingSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                    if (ComponentId == 0)
                    {
                        glkpProcessledger.EditValue = glkpProcessledger.Properties.GetKeyValue(0);
                    }
                    else
                    {
                        // glkpProcessledger.EditValue = objPayroll.ProcessLedgerId;
                    }
                }
            }
        }

        private void GetProcessTypeLedgers()
        {
            Processtype LedgerProcessType = new Processtype();
            DataView dvProcessTypeLedgers = GetDescriptionfromEnumType(LedgerProcessType);
            DataTable dtProcessTypeLedgers = dvProcessTypeLedgers.ToTable();
            using (MappingSystem mappingSystem = new MappingSystem())
            {
                if (dtProcessTypeLedgers != null && dtProcessTypeLedgers.Rows.Count > 0)
                {
                    commonmem.ComboSet.BindGridLookUpCombo(glkpProcessledger, dtProcessTypeLedgers, "Name", "Id");
                    if (ComponentId == 0)
                    {
                        glkpProcessledger.EditValue = glkpProcessledger.Properties.GetKeyValue(0);
                    }
                    else
                    {
                        // glkpProcessledger.EditValue = objPayroll.ProcessLedgerId;
                    }
                }
            }
        }

        public DataView GetDescriptionfromEnumType(Enum enumType)
        {
            DataView dvEnumSource = null;
            DataRow drEnumSource = null;
            EnumTypeSchema.EnumTypeDataTable dtEnumSource = new EnumTypeSchema.EnumTypeDataTable();

            if (enumType != null)
            {
                try
                {
                    int enumValue = 0;
                    int i = 0;
                    string[] descs = new string[4];
                    string[] names = enumType.GetType().GetEnumNames();
                    foreach (string name in names)
                    {
                        FieldInfo fi = enumType.GetType().GetField(name);
                        object[] da = fi.GetCustomAttributes(typeof(DescriptionAttribute), true);
                        foreach (DescriptionAttribute ds in da)
                        {
                            descs[i] = ds.Description;
                            i++;
                        }
                    }
                    foreach (string description in descs)
                    {
                        drEnumSource = dtEnumSource.NewRow();
                        drEnumSource[dtEnumSource.IdColumn.ColumnName] = enumValue;
                        drEnumSource[dtEnumSource.NameColumn.ColumnName] = description;
                        dtEnumSource.Rows.Add(drEnumSource);
                        enumValue++;
                    }

                    dtEnumSource.AcceptChanges();
                    dvEnumSource = dtEnumSource.DefaultView;

                }
                catch (Exception e)
                {
                    new ExceptionHandler(e, true);
                }
            }

            return dvEnumSource;
        }

        public ResultArgs ProcessLedger(string ledgername)
        {
            try
            {

                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    //Voucher Master Details

                    ledgerSystem.LedgerName = ledgername;
                    ledgerSystem.GroupId = 23;
                    ledgerSystem.IsCostCentre = 0;
                    ledgerSystem.IsBankInterestLedger = 0;
                    ledgerSystem.LedgerType = "GN";
                    ledgerSystem.LedgerSubType = "GN";
                    ledgerSystem.LedgerId = (LedgerAccId == (int)AddNewRow.NewRow) ? (int)AddNewRow.NewRow : LedgerAccId;
                    ledgerSystem.LedgerCode = "PAY" + ledgername.Substring(0, 3) + rand.Next(100000);
                    ledgerSystem.LedgerNotes = "";
                    ledgerSystem.SortId = 255;
                    ledgerSystem.IsTDSLedger = 0;
                    resultArgs = ledgerSystem.SaveLedger();
                }

            }
            catch (Exception ed)
            {
                resultArgs.Message = ed.Message;
            }
            return resultArgs;
        }

        public static int GetEnumFromDescription(string description, Type enumType)
        {
            foreach (var field in enumType.GetFields())
            {
                DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute == null)
                    continue;
                if (attribute.Description == description)
                {
                    return (int)field.GetValue(null);
                }                                                                                                                                                                                                                                                                                                                           
            }
            return 0;
        }

        private void MapProjectLedger()
        {

            string[] MapLedgerIds = Ledgerid.Split(',');
            using (MappingSystem mappingSystem = new MappingSystem())
            {
                resultArgs = mappingSystem.FetchProjectsLookup();
                dtProject = resultArgs.DataSource.Table;
                foreach (string ledgerId in MapLedgerIds)
                {
                    int LedgerId = commonmem.NumberSet.ToInteger(ledgerId);
                    Objprcomponent.DeleteProjectLedgerMapping(LedgerId);
                    if (dtProject.Rows.Count > 0 && dtProject != null)
                    {
                        Objprcomponent.MapProjectLedger(LedgerId, dtProject);
                    }
                }
            }
        }
        private void FetchRangeValues()
        {
            dtRangeSource = objPayroll.FetchRangeValues(ComponentId);
        }
        #endregion

        #region Methods

        private bool ValidateComponentSymbols()
        {
            bool rtn = true;
            //(txtComponent.Text.Contains("/")) ||  (txtComponent.Text.Contains("&")) || 
            if ((txtComponent.Text.Contains("%")) || (txtComponent.Text.Contains("*")) ||
               (txtComponent.Text.Contains("$")) ||  (txtComponent.Text.Contains("-")) ||
               (txtComponent.Text.Contains("_")) || (txtComponent.Text.Contains("{")) ||
               (txtComponent.Text.Contains("}")) || (txtComponent.Text.Contains("[")) || (txtComponent.Text.Contains("]")) ||
               (txtComponent.Text.Contains("!")) || (txtComponent.Text.Contains("#")) || (txtComponent.Text.Contains("^")) ||
               (txtComponent.Text.Contains("`")) || (txtComponent.Text.Contains("~")) || (txtComponent.Text.Contains(":")) ||
               (txtComponent.Text.Contains(";")) || (txtComponent.Text.Contains(",")) || (txtComponent.Text.Contains("'")) ||
               (txtComponent.Text.Contains("+")) || (txtComponent.Text.Contains("-")) || (txtComponent.Text.Contains("='")))
            {
                //XtraMessageBox.Show("Component Name is invalid", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Component.COMPONENT_NAME_INVALID_INFO));
                txtComponent.Focus();
                rtn  = false;
            }

            if (rtn && (txtComponent.Text.Trim().ToUpper() == "TOTAL" || txtComponent.Text.Trim().ToUpper() == "TOTAL EMPLOYEE" ||
                       txtComponent.Text.Trim().ToUpper() == "TOTAL EMPLOYER"))
            {
                this.ShowMessageBox("Compenent Name '" + txtComponent.Text + "' is reserve name, This Compenent will be used in Reports.");
                txtComponent.Focus();
                rtn = false;
            }

            return rtn;
        }

        private bool ValidateComponent()
        {
            if (txtComponent.Text == "")
            {
                //XtraMessageBox.Show("Component is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Component.COMPONENT_NAME_EMPTY));
                txtComponent.Focus();
                return false;
            }
            else
            {
                bool rtn = ValidateComponentSymbols();
                return rtn;
            }

            if (cbeType.EditValue == null)
            {
                //XtraMessageBox.Show("Type is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Component.COMPONENT_TYPE_EMPTY));
                cbeType.Focus();
                return false;
            }
            if (SettingProperty.PayrollFinanceEnabled == true)
            {
                if (glkpLedger.Enabled == true)
                {
                    if (string.IsNullOrEmpty(glkpLedger.Text))
                    {
                        //XtraMessageBox.Show("Ledger is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Component.COMPONENT_LEDGER_EMPTY));
                        glkpLedger.Focus();
                        return false;
                    }
                }
                if (glkpProcessledger.Enabled == true)
                {
                    if (string.IsNullOrEmpty(glkpProcessledger.Text))
                    {
                        //XtraMessageBox.Show("Process ledger type is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Component.COMPONENT_PROCESS_TYPE_LEDGER_EMPTY));
                        glkpProcessledger.Focus();
                        return false;
                    }
                }
            }

            //if (glkpLedger.EditValue == null)
            //{
            //    XtraMessageBox.Show("Select the Ledger !", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    glkpLedger.Focus();
            //    return false;
            //}
            if (rgDefaultValue.SelectedIndex == 0)
            {
                if (cbeType.SelectedIndex != 2)
                {
                    //if (txtFixedValue.Text == "" || commonmem.NumberSet.ToInteger(txtFixedValue.Text) <= 0)
                    if (txtFixedValue.Text == "0.00")
                    {
                        //XtraMessageBox.Show("Fixed value is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtFixedValue.Focus();
                        return false;
                    }
                }
            }
            if (rgDefaultValue.SelectedIndex == 1)
            {
                if (string.IsNullOrEmpty(glkpLinkValue.Text) || glkpLinkValue.Text == "" || glkpLinkValue.EditValue == "0" || glkpLinkValue.EditValue == null)
                {
                    //XtraMessageBox.Show("Link value is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Component.COMPONENT_LINK_VALUE_EMPTY));
                    glkpLinkValue.Focus();
                    return false;
                }
            }
            if (rgDefaultValue.SelectedIndex == 2)
            {
                if (cbeType.SelectedIndex != 2)
                {
                    if (txtEquationBuild.Text == "" || string.IsNullOrEmpty(txtEquationBuild.Text))
                    {
                        //XtraMessageBox.Show("Equation is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Component.COMPONENT_EQUATION_EMPTY));
                        btnEquationBuild.Focus();
                        return false;
                    }
                }
            }

            if (rgDefaultValue.SelectedIndex == 3)
            {
                if (dtRangeSource == null || dtRangeSource.Rows.Count == 0)
                {
                    this.ShowMessageBox("Range values are empty");
                    rgDefaultValue.Focus();
                    return false;
                }
            }

            return true;
        }

        protected void SetBorderColor(TextEdit txtEdit)
        {
            txtEdit.Properties.Appearance.BorderColor = string.IsNullOrEmpty(txtEdit.Text) ? Color.Red : Color.Empty;
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
            cbeType.SelectedIndex = 0;
            rgDefaultValue.SelectedIndex = 0;
            //cbeType.EditValue = 0;
            glkpLinkValue.EditValue = 0;
            txtFixedValue.Text = "";
            txtMaxSlab.Text = "";
            txtEquationBuild.Text = "";
            CValue = string.Empty;
            glkpLedger.EditValue = 0;
            glkpProcessComponent.EditValue = 0;
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
                txtDescription.Text = string.Empty;
            }
            if (txtEquationBuild.Text == "")
            {
                txtEquationBuild.Text = string.Empty;
            }
            if (string.IsNullOrEmpty(txtEquationBuild.Text))
            {
                CValue = string.Empty;
            }
            if (txtMaxSlab.Text == "")
            {
                txtMaxSlab.Text = "0.00";
            }
            if (glkpLinkValue.EditValue != null)
            {
                try
                {
                    SeletedLinkValue = glkpLinkValue.EditValue.ToString();
                    strLinkValue = objCompBuild.GetLinkName(SeletedLinkValue, false);
                    strLinkValue = SeletedLinkValue;
                    LinkVlaueByIndex = glkpLinkValue.Properties.GetDisplayTextByKeyValue(commonmem.NumberSet.ToInteger(SeletedLinkValue)).ToString();
                    //On 12/08/2019 if link exists, clear already assinged rnage value
                    dtRangeSource.Rows.Clear();
                }
                catch
                {
                }
            }
            if (glkpLinkValue.EditValue == null)
            {
                strLinkValue = "#";
            }
            sCompStr = sCompStr + "COMPONENT|" + txtComponent.Text.Trim() + "@DESCRIPTION|" + txtDescription.Text.Trim() + "@TYPE|" + cbeType.SelectedIndex.ToString(); // +"@LEDGERNAME|" + glkpLedger.EditValue.ToString();
            if (rgDefaultValue.SelectedIndex == 0)
                sCompStr = sCompStr + "@DEFVALUE|" + txtFixedValue.Text + "@LNKVALUE|" + LinkVlaueByIndex + "@EQUATION|" + txtEquationBuild.Text.Trim() + "@EQUATIONID|" + CValue;
            if (rgDefaultValue.SelectedIndex == 1)
                sCompStr = sCompStr + "@DEFVALUE|" + txtFixedValue.Text + "@LNKVALUE|" + LinkVlaueByIndex + "@EQUATION|" + txtEquationBuild.Text.Trim() + "@EQUATIONID|" + CValue;
            if (rgDefaultValue.SelectedIndex == 2)
            {
                sCompStr = sCompStr + "@DEFVALUE|" + txtFixedValue.Text + "@LNKVALUE|" + LinkVlaueByIndex + "@EQUATION|" + txtEquationBuild.Text.Trim() + "@EQUATIONID|" + CValue;
            }
            if (rgDefaultValue.SelectedIndex == 3)
            {
                sCompStr = sCompStr + "@DEFVALUE|" + txtFixedValue.Text + "@LNKVALUE|" + LinkVlaueByIndex + "@EQUATION|" + txtEquationBuild.Text.Trim() + "@EQUATIONID|" + CValue + "@MAXSLAP|" + txtMaxSlab.Text.ToString();
            }
            else
            {
                sCompStr = sCompStr + "@MAXSLAP|" + txtMaxSlab.Text.ToString();
            }
            if (rgRoundedOption.SelectedIndex == 0)
                sCompStr = sCompStr + "@COMPROUND|" + "0";
            else if (rgRoundedOption.SelectedIndex == 1)
                sCompStr = sCompStr + "@COMPROUND|" + "1";
            else if (rgRoundedOption.SelectedIndex == 2)
                sCompStr = sCompStr + "@COMPROUND|" + "2";
            else
                sCompStr = sCompStr + "@COMPROUND|" + "3";

            sCompStr = sCompStr + "@IFCONDITION|" + IFCon.ToString();

            if (cbeType.SelectedIndex <= 1) //On 11/02/2022, For Earninings and Deductions
            {
                sCompStr = sCompStr + "@DONT_SHOWINBROWSE|" + "0";
            }
            else
            {
                if (chkDontShowInView.Checked == true)
                    sCompStr = sCompStr + "@DONT_SHOWINBROWSE|" + "1";
                else
                    sCompStr = sCompStr + "@DONT_SHOWINBROWSE|" + "0";
            }

            sRelatedComponents = new clsEvalExpr().BuildComponentIdFromFormula(txtEquationBuild.Text);
            sCompStr += "@RELATEDCOMPONENTS|" + (sRelatedComponents == "" ? "0.00" : sRelatedComponents);
            if (glkpLedger.EditValue == null)
            {
                sCompStr = sCompStr + "@LEDGERNAME|0";
            }
            else
            {
                sCompStr = sCompStr + "@LEDGERNAME|" + glkpLedger.EditValue.ToString();
            }
            if (glkpProcessledger.EditValue == null)
            {
                sCompStr = sCompStr + "@PROCESS_LEDGER_NAME|0" + "@PAYABLE|" + Payable;
            }
            else
            {
                sCompStr = sCompStr + "@PROCESS_LEDGER_NAME|" + glkpProcessledger.EditValue.ToString() + "@PAYABLE|" + Payable;
            }
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
                //this.ShowMessageBox(txtComponent.Text + this.GetMessage(MessageCatalog.Payroll.Component.COMPONENT_CIRCULAR_REF_INFO + sCircularComponentName + this.GetMessage(MessageCatalog.Payroll.Component.COMPONENT_REMOVE_FORMULA_INFO)));
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

        private void LoadProcessComponentValues()
        {
            PayRollProcessComponent payrollcomponent = new PayRollProcessComponent();
            DataView dvProcessComponent = this.UtilityMember.EnumSet.GetEnumDataSource(payrollcomponent, Sorting.Descending);
            string grossnetsortorder = "IIF(ID IN (" + (Int32)PayRollProcessComponent.None + ","  +
                               (Int32)PayRollProcessComponent.NetPay + "," + (Int32)PayRollProcessComponent.GrossWages + "," +
                              (Int32)PayRollProcessComponent.Deductions+ "), ID, 5)";

            dvProcessComponent.Table.Columns.Add("SORT", typeof(System.Int32), grossnetsortorder);
            dvProcessComponent.Sort = "SORT, Name";
            this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProcessComponent, dvProcessComponent.ToTable(), "Name", "Id");
            glkpProcessComponent.EditValue = 0;
        }

        private void LoadDeductionLinkValues()
        {
            resultArgs = objLoanMgmt.GetLoanDetails();
            BindGridLookUpCombo(glkpLinkValue, resultArgs.DataSource.Table, "INCOME_NAME", "INCOME_ID");
        }

        /// <summary>
        /// On 08/02/2022, To all all link values (Earninings and Deductions)
        /// </summary>
        private void LoadAllEarningAndDeductionLinkValues()
        {
            glkpLinkValue.Properties.DataSource = null;
            resultArgs = objLoanMgmt.GetAllIncomeExpensesLinkValues();
            if (resultArgs.Success)
            {
                DataTable dtAllEDLinkvalues = resultArgs.DataSource.Table;
                BindGridLookUpCombo(glkpLinkValue, dtAllEDLinkvalues, "INCOME_NAME", "INCOME_ID");
            }
            
        }

        private void LoadTextLinkValues()
        {
            resultArgs = objLoanMgmt.GetTextValues();
            if (resultArgs.Success)
            {
                DataTable dtLnkValue = resultArgs.DataSource.Table;
                dtLnkValue.DefaultView.RowFilter = "INCOME_NAME NOT IN ('MAXWAGESBASIC', 'MAXWAGESHRA', 'Increment Date')";
                dtLnkValue = dtLnkValue.DefaultView.ToTable();
                BindGridLookUpCombo(glkpLinkValue, dtLnkValue, "INCOME_NAME", "INCOME_ID");
            }
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
            txtComponent.Text = PrevCompName = objPayroll.Component;
            AccFlag = objPayroll.CompAccessFlag;
            txtComponent.Enabled = (AccFlag == (int)AccessFlag.Readonly) || (AccFlag == (int)AccessFlag.Editable) ? false : true;
            txtDescription.Text = objPayroll.Description;
            glkpLedger.EditValue = SettingProperty.PayrollFinanceEnabled == true ? Convert.ToInt32(objPayroll.LedgerId.ToString()) : 0;
            // LoadProcessLedgers();
            GetProcessTypeLedgers();
            glkpProcessledger.EditValue = SettingProperty.PayrollFinanceEnabled == true ? commonmem.NumberSet.ToInteger(objPayroll.ProcessTypeId.ToString()) : 0;
            cbeType.SelectedIndex = commonmem.NumberSet.ToInteger(objPayroll.Type.ToString());
            txtFixedValue.Text = objPayroll.DefValue;
            //if (!string.IsNullOrEmpty(objPayroll.DefValue) && objPayroll.DefValue == "0.00" && objPayroll.DefValue == "0"
            //    && objPayroll.LinkValue=="0" && objPayroll.Equation=="0" && objPayroll.EquationId=="0" )
            if (Objprcomponent.FetchRangeComponent(ComponentId) <= 0)
            {
                if (!string.IsNullOrEmpty(objPayroll.DefValue) && objPayroll.DefValue != "0.00" && objPayroll.DefValue != "0")
                {
                    rgDefaultValue.SelectedIndex = 0;
                }
                else if ((objPayroll.DefValue == "0.00" || objPayroll.DefValue == "0") && (objPayroll.LinkValue == "0" || objPayroll.LinkValue == "") && 
                    (objPayroll.Equation == "0" || objPayroll.Equation == "") && (objPayroll.EquationId == "0" || objPayroll.EquationId == ""))
                {
                    //if (objPayroll.LinkValue == "0" && objPayroll.Equation == "0" && objPayroll.EquationId == "0")
                    //{
                    rgDefaultValue.SelectedIndex = 0;
                    //}
                }

                else if (!string.IsNullOrEmpty(objPayroll.Equation) && objPayroll.Equation != " " && objPayroll.Equation != "0")
                {
                    rgDefaultValue.SelectedIndex = 2;
                    //layoutControlItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    //simpleLabelItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    //layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    //emptySpaceItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    //emptySpaceItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    btnEquationBuild.Enabled = true;
                }
                else
                {
                    if (!string.IsNullOrEmpty(objPayroll.LinkValue) && objPayroll.LinkValue != "0")
                    {
                        //On 21/02/2022, to show-----------------------------------------------
                        ///For calculation components, income and expenses will be loaded
                        if (cbeType.SelectedIndex == 3)
                        {
                            Int32 rtn = objLoanMgmt.GetActualLinkValueId(objPayroll.LinkValue);
                            if (rtn > 0)
                            {
                                objPayroll.LinkValueID = rtn;
                            }
                        }
                        //----------------------------------------------------------------------

                        glkpLinkValue.EditValue = objPayroll.LinkValueID;
                        rgDefaultValue.SelectedIndex = rgDefaultValue.Properties.Items.Count==1 ? 0:1;
                        this.Height = this.Height + 50;
                    }
                    else
                    {
                        rgDefaultValue.SelectedIndex = 3;
                    }
                }
            }
            else
            {
                rgDefaultValue.SelectedIndex = 3;
            }
            //glkpLinkValue.Properties.GetDisplayTextByKeyValue(objPayroll.LinkValue);
            glkpLinkValue.EditValue = objPayroll.LinkValueID;
            txtEquationBuild.Text = objPayroll.Equation;
            // Added to specify whether the componenet has equation by Praveen
            if (txtEquationBuild.Text != string.Empty)
            {
                if (!string.IsNullOrEmpty(txtEquationBuild.Text))
                {
                    if (txtEquationBuild.Text != " ")
                    {
                        IFCon = 1;
                    }
                }
            }
            CValue = objPayroll.EquationId;
            txtMaxSlab.Text = objPayroll.MaxSlab.ToString();
            rgRoundedOption.SelectedIndex = objPayroll.CompRound;
            if (objPayroll.Payable == 1)
            {
                chkPayable.Checked = true;
            }
            else
            {
                chkPayable.Checked = false;
            }

            IsEditable = objPayroll.IsEditable;
            if (objPayroll.IsEditable == 0)
            {
                chkNonEditable.Checked = false;
            }
            else
            {
                chkNonEditable.Checked = true;
            }
            
            //On 14/02/2024, Don't import value from previous value
            chkDontImportModifiedValue.Checked = false;
            if (objPayroll.DontImportValuePreviousPR== 1)
            {
               chkDontImportModifiedValue.Checked = true;
            }

            //chkNonEditable.Enabled =  (AccFlag == (int)AccessFlag.Readonly) || (AccFlag == (int)AccessFlag.Editable) ? false : true;

            this.glkpProcessComponent.EditValue = objPayroll.ProcessComponentType;

            if (IsPostedPaymentExistsForComponent())
            {
                cbeType.Enabled = glkpProcessComponent.Enabled = chkPayable.Enabled = false;
                lcLblNote.Text = "Payroll Payment Voucher is posted for this Component.";
            }

            DontShowInBrowse = objPayroll.DontShowInBrowse;
            if (objPayroll.DontShowInBrowse == 0)
            {
                chkDontShowInView.Checked = false;
            }
            else
            {
                chkDontShowInView.Checked = true;
            }

        }


        /// <summary>
        /// On 11/07/2019, to check payroll payment voucher is exits or already posted for given componnet
        /// If so, we should not change processs component type and type. it will make confusion for already posted voucher to finance
        /// </summary>
        /// <returns></returns>
        private bool IsPostedPaymentExistsForComponent()
        {
            bool Rtn = true;
            try
            {
                using (clsPrGateWay paygateway = new clsPrGateWay())
                {
                    //Rtn = paygateway.ExistsPayrollPostPaymentsByPayrollIdCompId(clsGeneral.PAYROLL_ID, ComponentId);
                    Rtn = paygateway.ExistsPayrollPostPaymentsByCompId(ComponentId);
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
                Rtn = false;
            }
            return Rtn;
        }

        private void EnableControls()
        {
            if (SettingProperty.PayrollFinanceEnabled == false)
            {
                lblLedger.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem15.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.Height = this.Height - 48;
            }
            else
            {
                lblLedger.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem15.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }
        #endregion

        #region Events

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //On 17/05/2019, to fix editable or not editable
                //based on type (Fixed value can be editable-----------------------------------
                //chkNonEditable.Checked = true;
                //if (rgDefaultValue.Properties.Items.Count > 0 && rgDefaultValue.EditValue != null )
                //{
                //    if (rgDefaultValue.EditValue.ToString() == "Fixed Value")
                //    {
                //        chkNonEditable.Checked = false;
                //    }
                //}
                //-------------------------------------------------------------------------------------


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
                        //XtraMessageBox.Show("Component exists already", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Component.COMPONENT_EXITS_ALREADY_INFO));
                        ClearControls();
                        sCompStr = "";
                        txtComponent.Focus();
                        return;
                    }

                    sCompStr = sRelatedComponents = "";

                    AssignValues();

                    if (VerifyInterReference())
                    {
                        //XtraMessageBox.Show(txtComponent.Text + " can not refer itself in formula", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.ShowMessageBox(txtComponent.Text + " " + this.GetMessage(MessageCatalog.Payroll.Component.COMPONENT_FORMULA_CANNOT_REFER_ITSELF_INFO));
                        return;
                    }

                    if (VerifyCircularReference()) { return; }
                    //-----------------------------------------------------------------------------------------------
                    int processcomponenttype = (glkpProcessComponent.EditValue==null?0: UtilityMember.NumberSet.ToInteger(glkpProcessComponent.EditValue.ToString()));

                    //On 06/09/2019, to clear Range values if default values is changed-------------------------------------------------------------------------
                    if (rgDefaultValue.SelectedIndex != 3)
                    {
                        dtRangeSource.Clear();
                    }
                    //------------------------------------------------------------------------------------------------------------------------------------------

                    if (ComponentId == 0)
                    {    
                        if (strOperation == "Add")
                        {
                            //Save data	

                            //int iGroupId = glkpGroups.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkpGroups.EditValue.ToString()) : 0;

                            int selectedledgerid = glkpLedger.EditValue != null ? commonmem.NumberSet.ToInteger(glkpLedger.EditValue.ToString()) : 0;
                            //int SelectedProcessLedgerid = glkpProcessledger.EditValue != null ? commonmem.NumberSet.ToInteger(glkpProcessledger.EditValue.ToString()) : 0;
                            int SelectedProcessTypeid = GetEnumFromDescription(glkpProcessledger.Properties.GetDisplayText(glkpProcessledger.EditValue), typeof(Processtype));
                            int dontimportvalueprevpayroll = chkDontImportModifiedValue.Visible && chkDontImportModifiedValue.Checked ? 1 : 0;
                            if (objCompBuild.SaveComponent(ComponentId, sCompStr, txtComponent.Text.Trim(), txtDescription.Text.Trim(), selectedledgerid, 
                                SelectedProcessTypeid, isEditable,Payable, processcomponenttype, dontimportvalueprevpayroll))
                            {
                                if (objCompBuild.SaveRangeFormula(dtRangeSource) > 0)
                                   // this.ShowSuccessMessage(MessageCatalog.Payroll.Component.COMPONENT_SAVE_SUCCESS);
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.Component.COMPONENT_SAVE_SUCCESS));
                                //XtraMessageBox.Show(" Component Saved", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ClearControls();
                                sCompStr = "";
                                txtComponent.Focus();
                                if (UpdateHeld != null)
                                    UpdateHeld(this, e);
                                return;
                            }
                            else
                            {
                                txtEquationBuild.Text = "";
                                txtDescription.Text = "";
                                //XtraMessageBox.Show("Component exists already", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Component.COMPONENT_EXITS_ALREADY_INFO));
                                sCompStr = "";
                                ClearControls();
                                if (UpdateHeld != null)
                                    UpdateHeld(this, e);
                                return;
                            }
                        }
                    }
                    if (ComponentId != 0)
                    {
                        if (strOperation == "Edit")
                        {
                            if (checkEditComponent())
                            {
                                //XtraMessageBox.Show("Component exists already", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Component.COMPONENT_EXITS_ALREADY_INFO));
                                txtComponent.Text = " ";
                                txtComponent.Focus();
                                if (UpdateHeld != null)
                                    UpdateHeld(this, e);
                                return;
                            }
                            //Edit data
                            int selectedledgerid = glkpLedger.EditValue != null ? commonmem.NumberSet.ToInteger(glkpLedger.EditValue.ToString()) : 0;
                            // int SelectedProcessLedgerid = glkpProcessledger.EditValue != null ? commonmem.NumberSet.ToInteger(glkpProcessledger.EditValue.ToString()) : 0;
                            int SelectedProcessTypeid = GetEnumFromDescription(glkpProcessledger.Properties.GetDisplayText(glkpProcessledger.EditValue), typeof(Processtype));
                            int dontimportvalueprevpayroll = chkDontImportModifiedValue.Visible && chkDontImportModifiedValue.Checked ? 1 : 0;
                            if (objCompBuild.UpdateComponent(ComponentId, sCompStr, txtComponent.Text.Trim(), txtDescription.Text.Trim(), selectedledgerid,
                                    SelectedProcessTypeid, isEditable, Payable, processcomponenttype, dontimportvalueprevpayroll))
                            {
                                int payrollId = new clsPrGateWay().GetCurrentPayroll();
                                bool refCompUpdateStatus = false;

                                if (Objprcomponent.VerifyCurrentPayrollDependency(ComponentId, payrollId)) //VerifyCurrentPayrollDependency(ComponentId, payrollId)
                                {
                                    if (UpdateComponentChanges(sCompStr, ComponentId, payrollId))
                                    {
                                        refCompUpdateStatus = true;
                                    }
                                }
                                else
                                    refCompUpdateStatus = true;
                                if (refCompUpdateStatus)
                                    resultArgs = objPayroll.DeleteRangeValuesBycomponentId(ComponentId);
                                if (resultArgs.Success)
                                {
                                    if (objCompBuild.SaveRangeFormula(dtRangeSource) > 0)
                                        //this.ShowSuccessMessage(MessageCatalog.Payroll.Component.COMPONENT_SAVE_SUCCESS);
                                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.Component.COMPONENT_SAVE_SUCCESS));
                                    else
                                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.Component.COMPONENT_SAVE_SUCCESS));
                                }
                                //XtraMessageBox.Show("Component Updated!", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    //XtraMessageBox.Show("Could not update the component", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Component.COMPONENT_CANNOT_UPDATE_INFO));
                                resultArgs = objCompBuild.UpdateComponentNamesInEquations(PrevCompName, txtComponent.Text.Trim(), ComponentId);
                                if (UpdateHeld != null)
                                    UpdateHeld(this, e);
                               
                                return;
                                this.Close();
                                //================================
                            }
                            else
                            {
                                //XtraMessageBox.Show("Component exists already", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Component.COMPONENT_EXITS_ALREADY_INFO));
                                txtComponent.Text = " ";
                                txtComponent.Focus();
                                if (UpdateHeld != null)
                                    UpdateHeld(this, e);
                                return;
                            }
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
                frmpayrollconstructformula objBuild = new frmpayrollconstructformula(this, "Edit", rgDefaultValue);
                objBuild.ShowDialog();
            }
            if (ComponentId == 0)
            {
                if (ComponentId == 0 && strOperation == "Add" && !string.IsNullOrEmpty(txtComponent.Text))
                {
                    frmpayrollconstructformula objAdd = new frmpayrollconstructformula(this, "Add", rgDefaultValue);
                    objAdd.ShowDialog();
                }
                else
                {
                    //XtraMessageBox.Show("Component is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Component.COMPONENT_NAME_EMPTY));
                    txtComponent.Focus();
                }
            }
            //rgDefaultValue.SelectedIndex = 2;
            this.Height = 420;//397;
            this.Height = SettingProperty.PayrollFinanceEnabled == false ? this.Height - 48 : 420; //397;
            rgDefaultValue_SelectedIndexChanged(sender, new EventArgs());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rgDefaultValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            //On 28/05/2019, to allow editable or non editable for Components
            AllowNonEditableOption();

            if (rgDefaultValue.SelectedIndex == 0)
            {
                this.Height = SettingProperty.PayrollFinanceEnabled == true ? 380 : 365;// 356;
                lcDefaultCombo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcFixedValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcLinkValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcEqucationBuild.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcEqucation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcMAXSlab.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                emptySpaceItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                emptySpaceItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                txtMaxSlab.Text = string.Empty;
                txtEquationBuild.Text = string.Empty;
                glkpLinkValue.EditValue = null;
                glkpLinkValue.BackColor = Color.White;
                txtEquationBuild.Enabled = false;
                txtEquationBuild.BackColor = Color.White;
                txtMaxSlab.Enabled = false;
                txtMaxSlab.BackColor = Color.White;
                txtFixedValue.Focus();
                lcBuild.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else if (rgDefaultValue.SelectedIndex == 1)
            {
                if (cbeType.SelectedIndex != 2)
                {
                    this.Height = SettingProperty.PayrollFinanceEnabled == true ? 380 : 370;// 356;
                    lcDefaultCombo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    lcFixedValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lcLinkValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    lcEqucationBuild.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lcEqucation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lcMAXSlab.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    emptySpaceItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    emptySpaceItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    txtFixedValue.Text = string.Empty;
                    txtEquationBuild.Text = string.Empty;
                    glkpLinkValue.Enabled = true;
                    txtEquationBuild.Enabled = false;
                    txtFixedValue.BackColor = Color.White;
                    txtEquationBuild.BackColor = Color.White;
                    txtMaxSlab.Enabled = false;
                    txtMaxSlab.BackColor = Color.White;
                    glkpLinkValue.Focus();
                }
                else
                {
                    this.Height = SettingProperty.PayrollFinanceEnabled == true ? 380 : 370;// 356;
                    emptySpaceItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lcFixedValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lcLinkValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    rgDefaultValue.Properties.Items.Insert(2, "Equation");
                    rgDefaultValue.Properties.Items.RemoveAt(2);
                }
                lcBuild.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else if (rgDefaultValue.SelectedIndex == 2)
            {
                this.Height = SettingProperty.PayrollFinanceEnabled == true ? 410 : this.Height + 35;
                lcDefaultCombo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcFixedValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcLinkValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcEqucationBuild.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcEqucation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcMAXSlab.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                emptySpaceItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                emptySpaceItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                txtFixedValue.Text = string.Empty;
                txtMaxSlab.Enabled = true;
                txtFixedValue.BackColor = Color.White;
                glkpLinkValue.BackColor = Color.White;
                txtMaxSlab.BackColor = Color.White;
                glkpLinkValue.EditValue = null;
                lcBuild.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else if (rgDefaultValue.SelectedIndex == 3)
            {
                this.Height = SettingProperty.PayrollFinanceEnabled == true ? 410 : this.Height; // + 30
                lcDefaultCombo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcFixedValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcLinkValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcEqucationBuild.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcEqucation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcMAXSlab.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                emptySpaceItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                emptySpaceItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcBuild.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtFixedValue.Text = string.Empty;
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

        private void txtComponent_Leave(object sender, EventArgs e)
        {
            ValidateComponentSymbols();
            this.SetBorderColor(txtComponent);
            txtComponent.Text = this.commonmem.StringSet.ToSentenceCase(txtComponent.Text);
        }

        private void txtDescription_Leave(object sender, EventArgs e)
        {
            txtDescription.Text = this.commonmem.StringSet.ToSentenceCase(txtDescription.Text);
        }

        private void rgDefaultValue_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(rgDefaultValue);
        }

        private void cbeType_Leave(object sender, EventArgs e)
        {
            //rgDefaultValue.SelectedIndex = 0;
            txtFixedValue.Focus();
            this.SetBorderColor(cbeType);

            //rgDefaultValue_SelectedIndexChanged(sender, e);
        }

        private void glkpLinkValue_Enter(object sender, EventArgs e)
        {
            //rgDefaultValue.SelectedIndex = 1;
            rgDefaultValue_SelectedIndexChanged(sender, e);
        }

        private void frmComponents_Load(object sender, EventArgs e)
        {
            EnableControls();
           
            GetProcessTypeLedgers();
            FetchRangeValues();
            //MapProjectLedger();
            // this.Height = this.Height+50;\
            if (ComponentId == 0)
            {
                cbeType.SelectedIndex = 0;
                rgDefaultValue.SelectedIndex = 0;
            }
            //LoadLedgerValues();
            rgDefaultValue_SelectedIndexChanged(sender, e);
            //cbeType.SelectedIndex = 0;
            // btnEquationBuild.Enabled = false;
            //LoadIncomeValues();
            //FetchLedgerDetails();

            if (IsEditable == 0)
            {
                chkNonEditable.Checked = false;
                chkDontImportModifiedValue.Enabled = (glkpLinkValue.Text.ToString().ToUpper() == PayRollExtraPayInfo.BASICPAY.ToString() ? false : true);
            }
            else
            {
                chkNonEditable.Checked = true;
                chkDontImportModifiedValue.Enabled = false;
            }
        }

        private void LoadLedgerValues()
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    resultArgs = ledgerSystem.FetchLedgerDetails();
                    if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtcomponent = resultArgs.DataSource.Table;
                        glkpLedger.Properties.DataSource = dtcomponent;
                        glkpLedger.Properties.ValueMember = "LEDGER_ID";
                        glkpLedger.Properties.DisplayMember = "LEDGER_NAME";
                        glkpLedger.Refresh();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void cbeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lcDontShowInView.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            glkpProcessComponent.Enabled = true;
            if (cbeType.SelectedIndex == 0)
            {
                chkDontShowInView.Checked = false;
                if (rgDefaultValue.Properties.Items.Count < 2)
                {
                    rgDefaultValue.Properties.Items.Insert(0, "Fixed Value");
                }
                if (rgDefaultValue.Properties.Items.Count < 3)
                {
                    rgDefaultValue.Properties.Items.Insert(2, "Equation");
                }

                if (rgDefaultValue.Properties.Items.Count == 4)
                {
                    rgDefaultValue.Properties.Items.RemoveAt(3);
                }
                rgDefaultValue.SelectedIndex = 0;
                glkpLedger.Enabled = true;
                glkpProcessledger.Enabled = true;
                glkpLinkValue.Properties.DataSource = null;
                LoadIncomeValues();
                LoadLedgerByNature("2");

                lblLedger.Text = "Ledger <color=red>*";
                lblLedger.AllowHtmlStringInCaption = true;
                lcPayable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else if (cbeType.SelectedIndex == 1 || cbeType.SelectedIndex == 3)
            {
                chkDontShowInView.Checked = false;
                if (rgDefaultValue.Properties.Items.Count < 2)
                {
                    rgDefaultValue.Properties.Items.Insert(0, "Fixed Value");
                }
                if (rgDefaultValue.Properties.Items.Count < 3)
                {
                    rgDefaultValue.Properties.Items.Insert(2, "Equation");
                }
                if (rgDefaultValue.Properties.Items.Count < 4)
                {
                    rgDefaultValue.Properties.Items.Insert(3, "Range");
                }
                glkpLedger.Enabled = true;
                glkpProcessledger.Enabled = true;
                glkpLinkValue.Properties.DataSource = null;
                LoadDeductionLinkValues();
                LoadLedgerByNature("4");
                lblLedger.Text = "Ledger <color=red>*";
                lblLedger.AllowHtmlStringInCaption = true;
                lcPayable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                //On 08/02/2022, To all link values (Earninings and Deductions)
                if (cbeType.SelectedIndex == 3)
                {
                    //LoadIncomeValues();
                    LoadAllEarningAndDeductionLinkValues();
                    chkPayable.Checked = false;
                    lcPayable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    //glkpProcessComponent.Enabled = false;
                    //glkpProcessComponent.EditValue = 0;
                    lcDontShowInView.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
            }
            else
            {
                lcDontShowInView.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                LoadLedgerByNature("0");
                if (rgDefaultValue.Properties.Items.Count == 3)
                {
                    rgDefaultValue.Properties.Items.RemoveAt(2);
                }
                if (rgDefaultValue.Properties.Items.Count == 4)
                {
                    rgDefaultValue.Properties.Items.RemoveAt(3);
                    rgDefaultValue.Properties.Items.RemoveAt(2);
                }
                rgDefaultValue.SelectedIndex = 0;
                lcFixedValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcEqucationBuild.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcEqucation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcMAXSlab.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                glkpLedger.Enabled = false;
                glkpProcessledger.Enabled = false;
                glkpLinkValue.Properties.DataSource = null;
                LoadTextLinkValues();
                lblLedger.Text = "Ledger";
                lblLedger.AllowHtmlStringInCaption = false;
                lcPayable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            //On 28/05/2019, to allow editable or non editable for Components
            AllowNonEditableOption();
        }

        private void LoadLedgerByNature(string NatureId)
        {
            LoadLedgerValues();
            if (glkpLedger.Properties.DataSource != null)
            {
                DataTable dtLedger = (DataTable)glkpLedger.Properties.DataSource;
                DataView dvLedgerdetails = dtLedger.AsDataView();
                dvLedgerdetails.RowFilter = "NATURE_ID IN (" + NatureId + ")";
                dtLedger = dvLedgerdetails.ToTable();
                glkpLedger.Properties.DataSource = dtLedger;
                dvLedgerdetails.RowFilter = string.Empty;
            }
        }
        private void btnrangebuild_Click(object sender, EventArgs e)
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
                if (rgDefaultValue.SelectedIndex == 3)
                {

                    frmConstructRangeFormula objConsRangeFormula = new frmConstructRangeFormula(this, ComponentId, "Edit", dtRangeSource);
                    // objConsRangeFormula.gcFormulaCalucaltion.DataSource = dtRangeSource;
                    objConsRangeFormula.ShowDialog();
                    if (objConsRangeFormula.DialogResult == DialogResult.OK)
                    {
                        dtRangeSource = objConsRangeFormula.dtRangeConditions;
                    }
                }
            }
            if (ComponentId == 0)
            {
                if (ComponentId == 0 && strOperation == "Add" && !string.IsNullOrEmpty(txtComponent.Text))
                {
                    if (rgDefaultValue.SelectedIndex == 3)
                    {
                        frmConstructRangeFormula objConsRangeFormula = new frmConstructRangeFormula(this, "Add", dtRangeSource);
                        // objConsRangeFormula.gcFormulaCalucaltion.DataSource = dtRangeSource;
                        objConsRangeFormula.ShowDialog();
                        if (objConsRangeFormula.DialogResult == DialogResult.OK)
                        {
                            dtRangeSource = objConsRangeFormula.dtRangeConditions;
                        }
                    }
                }

                else
                {
                    //XtraMessageBox.Show("Component is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Component.COMPONENT_NAME_EMPTY));

                }
            }
            //rgDefaultValue.SelectedIndex = 2;
            rgDefaultValue_SelectedIndexChanged(sender, new EventArgs());
        }
        #endregion

        private void chkPayable_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkPayable.Checked)
                {
                    Payable = 1;
                }
                else
                {
                    Payable = 0;
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void rgRoundedOption_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// On 28/05/2019, to allow editable or non editable for Components
        /// </summary>
        private void AllowNonEditableOption()
        {
            //if (AccFlag == (int)AccessFlag.Accessable)
            //{
            chkNonEditable.Enabled = false;
            chkNonEditable.Checked = true;
            if (this.rgDefaultValue.SelectedIndex == 0) //for Fixed Value 
            {
                chkNonEditable.Enabled = true;
                chkNonEditable.Checked = false;
            }
            else if (this.rgDefaultValue.SelectedIndex == 1) //for Income Link Value (allow for link values like basic pay, grade, overtime, performace, specialpay and decrement)
            {
                string lnkvalue = glkpLinkValue.Text.ToString().ToUpper();
                if (lnkvalue == PayRollExtraPayInfo.EARNING1.ToString() || lnkvalue == PayRollExtraPayInfo.EARNING2.ToString()
                    || lnkvalue == PayRollExtraPayInfo.EARNING3.ToString()
                    || lnkvalue == PayRollExtraPayInfo.DEDUCTION1.ToString() || lnkvalue == PayRollExtraPayInfo.DEDUCTION2.ToString()
                    || lnkvalue == PayRollExtraPayInfo.PAYING_SALARY_DAYS.ToString().Replace("_", "").ToString()
                    || lnkvalue == PayRollExtraPayInfo.BASICPAY.ToString()
                    || lnkvalue == "TOTALDAYSINPAYMONTH")
                {
                    chkNonEditable.Enabled = true;
                    chkNonEditable.Checked = false;
                }
            }
            else if (this.rgDefaultValue.SelectedIndex == 3) //for Deduction  - Range 
            {
                if (this.rgDefaultValue.SelectedIndex == 3) //For Range 
                {
                    chkNonEditable.Enabled = true;
                    chkNonEditable.Checked = false;
                }
            }
            else if (this.rgDefaultValue.SelectedIndex == 2) //for foruma
            {
                //As on 01/02/2022, For formula component to allow change values after processed
                chkNonEditable.Enabled = true;
            }
            //}

            //On 14/02/2024, Based on Allow edit, don't import values form previous month will be enabled/disabled
            //On 14/02/2024, If not editable, lset us make enable dont import
            chkDontImportModifiedValue.Enabled = true;
            if ((chkNonEditable.Enabled ==false && chkNonEditable.Checked) || glkpLinkValue.Text.ToString().ToUpper() == PayRollExtraPayInfo.BASICPAY.ToString()) 
            { //For Basic pay always import
                chkDontImportModifiedValue.Checked = false;
                chkDontImportModifiedValue.Enabled = false;
            }
            else
            {
                chkDontImportModifiedValue.Enabled = chkNonEditable.Enabled;

                if (chkNonEditable.Checked)
                {
                    chkDontImportModifiedValue.Checked = true;
                    chkDontImportModifiedValue.Enabled= false;
                }
            }
        }

        private void glkpLinkValue_EditValueChanged(object sender, EventArgs e)
        {
            AllowNonEditableOption();
        }

        private void chkNonEditable_MouseUp(object sender, MouseEventArgs e)
        {
            //On 14/02/2024, If not editable, lset us make enable dont import
            if (glkpLinkValue.Text.ToString().ToUpper() != PayRollExtraPayInfo.BASICPAY.ToString())
            {
                chkDontImportModifiedValue.Enabled = true;
                if (chkNonEditable.Checked)
                {
                    chkDontImportModifiedValue.Checked = true;
                    chkDontImportModifiedValue.Enabled = false;
                }
            }
        }

      
    }
}
