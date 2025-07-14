using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using Bosco.Utility.Common;
using Bosco.Utility.CommonMemberSet;
using Bosco.Utility.Validations;
using PAYROLL.UserControl;
using Payroll.Model.UIModel;

namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmFormulaGroup : DevExpress.XtraEditors.XtraForm
    {
        #region variable Declaration
        private clsPayrollStaff objStaff = new clsPayrollStaff();
        DataTable dtGroups = new DataTable();
        ComboSetMember combosetmember = new ComboSetMember();
        private clsPayrollActivities objActivities = new clsPayrollActivities();
        private string sFormulaGroups = "";
        private bool bEdit = false;
        private bool bSave = false;
        private clsWorkSheet dgFormulaGroups;
        #endregion

        #region Constructor
        public frmFormulaGroup()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        private void getStaffsInfo()
        {
            //Make Grid Entry
            //0-COLUMNNAME			(NAME1, NAME2,...)
            //1-LOOKUPCOLUMN		(TRUE or FALSE) At Maximum of one column is possible
            //2-MANDATORYCOLUMN		(TRUE or FALSE)
            //3-VALIDATIONCOLUMN	(TRUE or FALSE)
            //4-READONLY			(TRUE or FALSE)
            //5-MAXLENGTH			(0, 20, 30, ...) Set Maxlength for only string column, for other datatypes set to 0
            //6-COLUMNWIDTH			(30, 40, 50, ...)
            //7-DATATYPE			(INT, BOOLEAN, , ...
            //8-Defailt Value		(				)						 
            //9-UNIQUE				(TRUE FALSE))
            //
            try
            {
                DataTable dtFormulaGroup = objStaff.getStaffNamesAndIds();
                if (dtFormulaGroup.Rows.Count <= 0)
                {
                    this.dgFormulaGroups.Enabled = false;
                    MessageBox.Show("No Records found for Staffs..", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dgFormulaGroups.DataSource = dtFormulaGroup.DefaultView;
                    string[,] TextBoxProperty1 = new string[,]{
																{"SELECT","FALSE","FALSE","FALSE","FALSE","1","50","BOOLEAN","0","FALSE"},
																{"STAFFID","FALSE","TRUE","TRUE","TRUE","25","0","INT","0","FALSE"},
																{"STAFFNAME","FALSE","TRUE","TRUE","TRUE","150","250","STRING","","FALSE"}
															  };

                    dgFormulaGroups.AllowAdd = dgFormulaGroups.AllowDelete = false;
                    dgFormulaGroups.CreateGridTextBox("FormulaGroup", TextBoxProperty1);

                }
            }
            catch { }

        }
        private void FillFormulaGroupsCombo()
        {
            try
            {
                cboFormulaGroup.Properties.DataSource = null;
                combosetmember.BindGridLookUpCombo(this.cboFormulaGroup, "FormulaGroup", "FORMULA_DESC", "FORMULAGROUPID");
               
                dtGroups = cboFormulaGroup.Properties.DataSource as DataTable;
                string[] sFormula = new string[dtGroups.Rows.Count];
                if (dtGroups.Rows.Count > 0)
                {
                    for (int i = 0; i < dtGroups.Rows.Count; i++)
                    {
                        cboFormulaGroup.EditValue = i;
                        sFormulaGroups = sFormulaGroups + cboFormulaGroup.Text + "@";
                    }
                }
            }
            catch { }
        }
        #endregion

        #region Events
        private void frmFormulaGroup_Load(object sender, EventArgs e)
        {
            try
            {
                txtFormula.Visible = false;
                layoutControlItem1.Text = " ";
                this.getStaffsInfo();
                this.FillFormulaGroupsCombo();
                cboFormulaGroup.EditValue = 0;
                layoutControlItem1.Text = "Edit Formula Category";
            }
            catch { }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int iSelectedIndex = 0;

                if (dtGroups.Rows.Count > 0)
                    iSelectedIndex = Convert.ToInt32(cboFormulaGroup.EditValue.ToString());
                DataTable dtFormulaGroup = new DataTable();

                dtFormulaGroup = ((DataView)dgFormulaGroups.DataSource).Table;

                string sStaffIdsColl = "@";
                for (int i = 0; i < dtFormulaGroup.Rows.Count; i++)
                {
                    if (dtFormulaGroup.Rows[i][0].ToString() == "1")
                    {
                        sStaffIdsColl = sStaffIdsColl + dtFormulaGroup.Rows[i][1].ToString() + "@";
                    }
                }
                if (sStaffIdsColl.Length <= 1)
                    sStaffIdsColl = "";
                if (this.txtFormula.Text != "")
                {
                    if (sFormulaGroups.IndexOf(this.txtFormula.Text, 0) >= 0 & bEdit == false)
                    {
                        MessageBox.Show("This Formula Category Exists Already..", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else if (!bEdit)
                    {
                        if (objActivities.saveSelectedStaffGroup(this.txtFormula.Text, sStaffIdsColl) == 1)
                        {
                            MessageBox.Show("Formula Category is Added Successfully", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtFormula.Text = "";
                            txtFormula.Focus();
                            this.FillFormulaGroupsCombo();
                            cboFormulaGroup.EditValue = dtGroups.Rows.Count;
                            return;
                        }
                    }
                    if (bEdit)
                    {

                        if (objActivities.updateSelectedStaffGroup(this.txtFormula.Text, sStaffIdsColl, cboFormulaGroup.SelectedText.ToString()) == 1)
                        {
                            MessageBox.Show("Formula Category Updated Successfully", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtFormula.Visible = false;
                            layoutControlItem1.Text = string.Empty;
                            this.FillFormulaGroupsCombo();
                            cboFormulaGroup.EditValue = iSelectedIndex;
                        }
                    }
                }
                else
                    MessageBox.Show("Formula Category is Empty.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bSave = false;
            }
            catch { }
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            txtFormula.Text = "";
            txtFormula.Focus();
            layoutControlItem1.Text = "Enter New Category Name";
            txtFormula.Visible = true;
            layoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //lblAddCategory.Visible = true;
            bEdit = false;
            bSave = false;
            getStaffsInfo();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (bSave)
            {
                if (MessageBox.Show("Would you like to save the changes ! ", "Payroll", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    btnSave_Click(sender, e);
            }
            this.Close();
        }
        private void cboFormulaGroup_EditValueChanged(object sender, EventArgs e)
        {
            txtFormula.Text = cboFormulaGroup.Text.ToString();
            bEdit = true;
            txtFormula.Visible = true;
            layoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //layoutControlItem1.Visible = true;
            layoutControlItem1.Text = "Edit Formula Category";
            try
            {
                int getFormulaId = Convert.ToInt32(this.cboFormulaGroup.EditValue.ToString());
                string sStaffIdColl = objActivities.getFormulaGroupStaffIdCollection(getFormulaId);
                if (sStaffIdColl != "")
                {
                    sStaffIdColl = sStaffIdColl.Substring(1, sStaffIdColl.Length - 1).Replace('@', ',');
                    sStaffIdColl = sStaffIdColl.TrimEnd(',');
                    DataTable dtSelectedStaffGroup = objStaff.getSelectStaffNamesAndIds(sStaffIdColl);
                    if (dtSelectedStaffGroup.Rows.Count <= 0)
                    {
                        this.dgFormulaGroups.Enabled = false;
                        MessageBox.Show("No Records found for Staffs..", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        dgFormulaGroups.DataSource = new DataView(objStaff.getSelectStaffNamesAndIds(sStaffIdColl));
                        string[,] TextBoxProperty1 = new string[,]{
															{"SELECT","FALSE","FALSE","FALSE","FALSE","1","50","BOOLEAN","0","FALSE"},
															{"STAFFID","FALSE","TRUE","TRUE","TRUE","25","0","INT","0","FALSE"},
															{"STAFFNAME","FALSE","TRUE","TRUE","TRUE","150","250","STRING","","FALSE"}
																  };
                        dgFormulaGroups.AllowAdd = dgFormulaGroups.AllowDelete = false;
                        dgFormulaGroups.CreateGridTextBox("FormulaGroup", TextBoxProperty1);

                    }
                }
                else
                    getStaffsInfo();
            }
            catch { }
        }
        private void btnSaveStaffGroup_Click(object sender, System.EventArgs e)
        {
            DataTable dtFormulaGroup = new DataTable();
            try
            {
                dtFormulaGroup = ((DataView)dgFormulaGroups.DataSource).Table;
                string sStaffIdsColl = "@";
                for (int i = 0; i < dtFormulaGroup.Rows.Count; i++)
                {
                    if (dtFormulaGroup.Rows[i][0].ToString() == "1")
                    {
                        sStaffIdsColl = sStaffIdsColl + dtFormulaGroup.Rows[i][1].ToString() + "@";
                    }
                }
            }
            catch { }
        }
        private void chkSelectAll_CheckedChanged(object sender, System.EventArgs e)
        {
            getStaffsInfo();
        }
        private void dgFormulaGroup_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            bSave = true;
        }
        #endregion
    }
}