using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Payroll.Model.UIModel;
using Bosco.Utility.Common;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.Utility.CommonMemberSet;
using DevExpress.XtraEditors.Mask;
using Bosco.Model.UIModel;
using System.Reflection;
using Bosco.Utility.ConfigSetting;

namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmStaffDetails : frmPayrollBase
    {
        #region Declaration
        ResultArgs resultArgs;
        int RetrunValue;
        ComboSetMember objCommon = new ComboSetMember();
        EnumSetMember enumSetMember = new EnumSetMember();
        CommonMember commem = new CommonMember();
        SettingProperty settingProperty = new SettingProperty();
        public event EventHandler UpdateHeld;
        #endregion

        #region Properties
        private int staffId = 0;
        private int StaffId
        {
            get { return staffId; }
            set { staffId = value; }
        }
        private string gender = string.Empty;
        private string Gender
        {
            get { return gender; }
            set { gender = value; }
        }
        private string bloodgroup = string.Empty;
        private string Blood
        {
            get { return bloodgroup; }
            set { bloodgroup = value; }
        }
        private string category = string.Empty;
        private string Category
        {
            get { return category; }
            set { category = value; }
        }
        private string leavingreason = string.Empty;
        private string LeavingReason
        {
            get { return leavingreason; }
            set { leavingreason = value; }
        }
        private string month = string.Empty;
        private string Month
        {
            get { return month; }
            set { month = value; }
        }
        private int projectid = 0;
        private int ProjectId
        {
            get { return projectid; }
            set { projectid = value; }
        }
        private int groupid = 0;
        private int GroupId
        {
            get { return groupid; }
            set { groupid = value; }
        }

        private string activeAcId = string.Empty;
        public string ActiveAccId
        {
            get { return this.settingProperty.AccPeriodId; }
            set { activeAcId = value; }
        }

        private string ApplicableStatutoryCompliances
        {
            get
            {
                string fromledgers = string.Empty;
                chkListStatutoryCompliances.RefreshEditValue();
                List<object> selecteditems = chkListStatutoryCompliances.Properties.Items.GetCheckedValues();

                foreach (object item in selecteditems)
                {
                    fromledgers += item.ToString() + ",";
                }
                fromledgers = fromledgers.TrimEnd(',');
                return fromledgers;
            }
            set
            {
                chkListStatutoryCompliances.SetEditValue(value);
            }
        }

        private int departmentid = 0;
        private int DepartmentId
        {
            get { return departmentid; }
            set { departmentid = value; }
        }

        private int worklocationid = 0;
        private int WorkLocationId
        {
            get { return worklocationid; }
            set { worklocationid = value; }
        }

        private int nametitleid = 0;
        private int NameTitleid
        {
            get { return nametitleid; }
            set { nametitleid = value; }
        }
        #endregion

        #region Constractors

        public frmStaffDetails()
        {
            InitializeComponent();
        }

        public frmStaffDetails(int staffid)
            : this()
        {
            StaffId = staffid;
            if (StaffId > 0)
            {
                //this.Text = "Staff (Edit)";
                this.Text = this.GetMessage(MessageCatalog.Payroll.Staff.STAFF_EDIT_CAPTION);
                LoadStatutoryCompliances();
                AssignValues();
                LoadCombo();
            }
            else
            {
                LoadCombo();
                //On 09/08/2019 to lock staff comment ------------------------------------------------------
                LockStaffComment();
                //------------------------------------------------------------------------------------------
            }
        }

        #endregion

        #region Events
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveStaffDetails() > 0)
            {
                if (UpdateHeld != null)
                {
                    UpdateHeld(this, e);
                }
                LoadDesignationAutoComplete();
                LockStaffComment();
            }

            if (UpdateHeld != null)
            {
                UpdateHeld(this, e);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearValues();
            txtStaffCode.Focus();
        }

        private void txtStaffCode_Leave(object sender, EventArgs e)
        {
            SetBorderColor(txtStaffCode);
        }

        private void txtFirstName_Leave(object sender, EventArgs e)
        {
            SetBorderColor(txtFirstName);
            txtFirstName.Text = this.commem.StringSet.ToSentenceCase(txtFirstName.Text);
        }

        private void glkpGender_Leave(object sender, EventArgs e)
        {
            SetBorderColor(glkpGender);
        }

        private void deDateofJoining_Leave(object sender, EventArgs e)
        {
            SetBorderColor(deDateofJoining);
        }

        private void deDateofBirth_Leave(object sender, EventArgs e)
        {
            SetBorderColor(deDateofBirth);
        }

        private void glkGroup_Leave(object sender, EventArgs e)
        {
            SetBorderColor(glkGroup);
        }

        private void txtDesignation_Leave(object sender, EventArgs e)
        {
            SetBorderColor(txtDesignation);
            txtDesignation.Text = this.commem.StringSet.ToSentenceCase(txtDesignation.Text);
        }

        private void frmStaffDetails_Load(object sender, EventArgs e)
        {
            if (SettingProperty.PayrollFinanceEnabled)
            {
                this.lciProject.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                this.lciProject.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            xtcStaff.SelectedTabPageIndex = 0;
            txtStaffCode.Select();
            LoadProjects();
            LoadGroupList();
            SetTitle();

            lcgrpComment.Text = "Comment on Performance for April " + this.UtilityMember.DateSet.ToDate(settingProperty.YearFrom, false).ToShortDateString() + " to March " + this.UtilityMember.DateSet.ToDate(settingProperty.YearTo, false).ToShortDateString() + "";

            //On 06/12/2024, To hide few properties for other or multi currency enabled ----------------
            if (this.AppSetting.AllowMultiCurrency == 1 || this.AppSetting.IsCountryOtherThanIndia)
            {
                lciUAN.Visibility = lcPAN.Visibility = lcAADHARNo.Visibility = lcESIIPNo.Visibility = lciSComplianceApplicable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                
            }
            //------------------------------------------------------------------------------------------
        }

        private void txtScalePay1_Leave(object sender, EventArgs e)
        {
            SetBorderColor(txtScalePay1);
        }

        private void txtScalePay2_Leave(object sender, EventArgs e)
        {
        }

        private void txtScalePay3_Leave(object sender, EventArgs e)
        {
        }

        private void deLeavingDate_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void glkpLeavingReason_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void txtScalePay1_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void txtScalePay2_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void txtScalePay3_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void glkIncrementMonth_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void deRetirementDate_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void xtpPayInfo_EnabledChanged(object sender, EventArgs e)
        {
            deLeavingDate.Focus();
        }

        private void LoadProjects()
        {
            try
            {
                using (PayrollSystem Paysystem = new PayrollSystem())
                {
                    resultArgs = Paysystem.FetchPayrollProjects();
                    glkpProject.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        commem.ComboSet.BindGridLookUpCombo(glkpProject, resultArgs.DataSource.Table, Paysystem.AppSchema.PRPayrollProject.PROJECTColumn.ColumnName, Paysystem.AppSchema.PRPayrollProject.PROJECT_IDColumn.ColumnName);
                        this.glkpProject.EditValueChanged -= new System.EventHandler(this.glkpProject_EditValueChanged);
                        glkpProject.EditValue = ProjectId != 0 ? ProjectId : 0;
                        this.glkpProject.EditValueChanged += new System.EventHandler(this.glkpProject_EditValueChanged);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        public void SetTitle()
        {
            this.Text = StaffId == 0 ? this.GetMessage(MessageCatalog.Payroll.Staff.STAFF_ADD_CAPTION) : this.GetMessage(MessageCatalog.Payroll.Staff.STAFF_EDIT_CAPTION);
        }

        private void LoadGroupList()
        {
            try
            {
                DataTable dtGradeList;
                using (clsPayrollGrade Grade = new clsPayrollGrade())
                {
                    dtGradeList = Grade.getPayrollGradeList();
                    glkGroup.Properties.DataSource = null;
                    if (dtGradeList != null && dtGradeList.Rows.Count > 0)
                    {
                        commem.ComboSet.BindGridLookUpCombo(glkGroup, dtGradeList, "Group Name", "GROUP ID");
                        // glkGroup.EditValue = Grade.getGroupByCategoryGroup(Category);
                        glkGroup.EditValue = GroupId != 0 ? GroupId : 0;
                        if (GroupId == 0)
                        {
                            glkGroup.EditValue = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
        }

        private void glkGroup_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
                {
                    frmPayrollGroup frmGroup = new frmPayrollGroup();
                    frmGroup.ShowDialog();
                    LoadGroupList();

                    if (GroupId == 0)
                    {
                        DataTable dt = glkGroup.Properties.DataSource as DataTable;
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            dt.DefaultView.Sort = "[GROUP ID] DESC";
                            int grpid = UtilityMember.NumberSet.ToInteger(dt.DefaultView[0]["GROUP ID"].ToString());
                            if (grpid > 0)
                            {
                                glkGroup.EditValue = grpid;
                            }
                            dt.DefaultView.Sort = "GROUP NAME";
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

        private void txtLastName_Leave(object sender, EventArgs e)
        {
            txtLastName.Text = this.commem.StringSet.ToSentenceCase(txtLastName.Text);
        }

        private void txtKnownAs_Leave(object sender, EventArgs e)
        {
            txtKnownAs.Text = this.commem.StringSet.ToSentenceCase(txtKnownAs.Text);
        }

        private void txtQualificationDegree_Leave(object sender, EventArgs e)
        {
            txtQualificationDegree.Text = this.commem.StringSet.ToSentenceCase(txtQualificationDegree.Text);
        }

        private void glkpLeavingReason_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
            }
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Close)
            {
                glkpLeavingReason.Text = string.Empty;
                glkpLeavingReason.EditValue = null;
            }
        }

        private void glkIncrementMonth_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                //LoadSociety();
            }
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Close)
            {
                //glkIncrementMonth.Text = string.Empty;
                glkIncrementMonth.EditValue = (int)clsGeneral.IncrementMonth.None;
            }
        }

        private void txtScalePay2_Enter(object sender, EventArgs e)
        {
            //txtScalePay2.Properties.Mask.MaskType = MaskType.RegEx;
            //txtScalePay2.Properties.Mask.EditMask = @"\d*\.?\d*";
        }

        private void txtScalePay2_Validating(object sender, CancelEventArgs e)
        {
            //txtScalePay2.Properties.Mask.MaskType = MaskType.Numeric;
            //txtScalePay2.Properties.Mask.EditMask = "n";
            //txtScalePay2.Properties.Mask.UseMaskAsDisplayFormat = true;
        }

        private void txtScalePay3_Enter(object sender, EventArgs e)
        {
            //txtScalePay3.Properties.Mask.MaskType = MaskType.RegEx;
            //txtScalePay3.Properties.Mask.EditMask = @"\d*\.?\d*";
        }

        private void txtScalePay3_Validating(object sender, CancelEventArgs e)
        {
            //txtScalePay3.Properties.Mask.MaskType = MaskType.Numeric;
            //txtScalePay3.Properties.Mask.EditMask = "n";
            //txtScalePay3.Properties.Mask.UseMaskAsDisplayFormat = true;
        }

        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            if (CheckLoanExistsforStaff(staffId.ToString()))
            {
                //XtraMessageBox.Show("Cannot unmap, It has association ", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Staff.STAFF_CANNOT_UNMAP_INFO));
                this.glkpProject.EditValueChanged -= new System.EventHandler(this.glkpProject_EditValueChanged);
                glkpProject.EditValue = projectid;
                this.glkpProject.EditValueChanged += new System.EventHandler(this.glkpProject_EditValueChanged);
            }
        }
        #endregion

        #region Methods

        private bool isValidRecord()
        {
            xtcStaff.SelectedTabPageIndex = 0;

            if ((SettingProperty.PayrollFinanceEnabled))
            {
                if (glkpProject.EditValue == null || commem.NumberSet.ToInteger(glkpProject.EditValue.ToString()) == 0)
                {
                    //XtraMessageBox.Show(MessageCatalog.Payroll.Staff.STAFF_PROJECT_EMPTY, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Staff.STAFF_PROJECT_EMPTY));
                    glkpProject.Select();
                    //SetBorderColor(glkpProject);
                    return false;
                }
            }
            if (string.IsNullOrEmpty(txtStaffCode.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Staff.STAFF_CODE_NULL));
                txtStaffCode.Focus();
                SetBorderColor(txtStaffCode);
                return false;
            }

            else if (string.IsNullOrEmpty(txtFirstName.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Staff.STAFF_FIRST_NAME_NULL));
                txtFirstName.Focus();
                SetBorderColor(txtFirstName);
                return false;
            }
            else if (string.IsNullOrEmpty(glkpGender.Text.Trim()))
            {
                //XtraMessageBox.Show(MessageCatalog.Payroll.Staff.STAFF_GENDGER_NULL, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Staff.STAFF_GENDGER_NULL));
                glkpGender.Focus();
                SetBorderColor(glkpGender);
                return false;
            }
            else if (string.IsNullOrEmpty(deDateofBirth.Text.Trim()))
            {
                //XtraMessageBox.Show(MessageCatalog.Payroll.Staff.STAFF_DATE_OF_BIRTH_NULL, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Staff.STAFF_DATE_OF_BIRTH_NULL));
                deDateofBirth.Focus();
                SetBorderColor(deDateofBirth);
                return false;
            }

            else if (glkGroup.EditValue == null || commem.NumberSet.ToInteger(glkGroup.EditValue.ToString()) == 0)
            {
                //XtraMessageBox.Show(MessageCatalog.Payroll.Staff.STAFF_GROUP_EMPTY, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Staff.STAFF_GROUP_EMPTY));
                glkGroup.Select();
                SetBorderColor(glkGroup);
                return false;
            }
            else if (string.IsNullOrEmpty(deDateofJoining.Text.Trim()))
            {
                //XtraMessageBox.Show(MessageCatalog.Payroll.Staff.STAFF_DATE_OF_JOINING_NULL, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Staff.STAFF_DATE_OF_JOINING_NULL));
                deDateofJoining.Focus();
                SetBorderColor(deDateofJoining);
                return false;
            }
            else if (string.IsNullOrEmpty(txtDesignation.Text.Trim()))
            {
                //XtraMessageBox.Show(MessageCatalog.Payroll.Staff.STAFF_DESIGNATION_NULL, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Staff.STAFF_DESIGNATION_NULL));
                xtcStaff.SelectedTabPageIndex = 0;
                txtDesignation.Focus();
                SetBorderColor(txtDesignation);
                return false;
            }
            //else if (glkpProject.EditValue == null || commem.NumberSet.ToInteger(glkpProject.EditValue.ToString()) == 0)
            //{
            //    XtraMessageBox.Show(MessageCatalog.Payroll.Staff.STAFF_PROJECT_EMPTY, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    glkpProject.Select();
            //    //SetBorderColor(glkpProject);
            //    return false;
            //}

            else if (commem.NumberSet.ToDecimal(txtScalePay1.Text.Trim()) == 0)
            {
                //XtraMessageBox.Show(MessageCatalog.Payroll.Staff.STAFF_SCALE_OF_PAY_EMPTY, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Staff.STAFF_SCALE_OF_PAY_EMPTY));
                xtcStaff.SelectedTabPageIndex = 1;
                txtScalePay1.Focus();
                txtScalePay1.Select();
                SetBorderColor(txtScalePay1);
                return false;
            }
            //else if (glkpProject.EditValue == null || commem.NumberSet.ToInteger(glkpProject.EditValue.ToString()) == 0)
            //{
            //    XtraMessageBox.Show(MessageCatalog.Payroll.Staff.STAFF_PROJECT_EMPTY, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    glkpProject.Select();
            //    SetBorderColor(glkpProject);
            //    return false;
            //}
            else if (glkGroup.EditValue == null || commem.NumberSet.ToInteger(glkGroup.EditValue.ToString()) == 0)
            {
                //XtraMessageBox.Show(MessageCatalog.Payroll.Staff.STAFF_GROUP_EMPTY, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Staff.STAFF_GROUP_EMPTY));
                glkGroup.Select();
                SetBorderColor(glkGroup);
                return false;
            }
            else if ((StaffId == 0) && !(isStaffCodeExists()))
            {
                txtStaffCode.Focus();
                return false;
            }
            else if (commem.NumberSet.ToDecimal(txtScalePay1.Text.Trim()) < 0) // || commem.NumberSet.ToDecimal(txtScalePay2.Text.Trim()) < 0 || commem.NumberSet.ToDecimal(txtScalePay3.Text.Trim()) < 0
            {
                if (commem.NumberSet.ToDecimal(txtScalePay1.Text.Trim()) < 0)
                {
                    // XtraMessageBox.Show(MessageCatalog.Payroll.Staff.STAFF_SCALE_OF_PAY_ZERO, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Staff.STAFF_SCALE_OF_PAY_ZERO));
                    txtScalePay1.Focus();
                    SetBorderColor(txtScalePay1);
                }
                //else if (commem.NumberSet.ToDecimal(txtScalePay2.Text.Trim()) < 0)
                //{
                //    XtraMessageBox.Show(MessageCatalog.Payroll.Staff.STAFF_SCALE_OF_PAY_ZERO, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtScalePay2.Focus();
                //    SetBorderColor(txtScalePay2);
                //}
                //else if (commem.NumberSet.ToDecimal(txtScalePay3.Text.Trim()) < 0)
                //{
                //    XtraMessageBox.Show(MessageCatalog.Payroll.Staff.STAFF_SCALE_OF_PAY_ZERO, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtScalePay3.Focus();
                //    SetBorderColor(txtScalePay3);
                //}
                xtcStaff.SelectedTabPageIndex = 1;
                return false;
            }
            //else if (string.IsNullOrEmpty(txtAccountNumber.Text.Trim()))
            //{
            //    this.ShowMessageBox("Account Number is empty");
            //    xtcStaff.SelectedTabPageIndex = 1;
            //    txtAccountNumber.Focus();
            //    SetBorderColor(txtAccountNumber);
            //    return false;
            //}
            else if (UtilityMember.NumberSet.ToDouble(txtPayingDays.Text) > 31)
            {
                this.ShowMessageBox("Paying Days should be less than 31 days");
                xtcStaff.SelectedTabPageIndex = 1;
                txtPayingDays.Focus();
                SetBorderColor(txtPayingDays);
                return false;
            }
            else
                return true;
        }

        private bool isValidDate()
        {
            if (!string.IsNullOrEmpty(deDateofBirth.Text.Trim()) || !string.IsNullOrEmpty(deDateofJoining.Text.Trim()))
            {
                if (DateTime.Compare(deDateofBirth.DateTime, DateTime.Parse(System.DateTime.Now.ToShortDateString()).Date) >= 0)
                {
                    //XtraMessageBox.Show(MessageCatalog.Payroll.Staff.STAFF_NOT_VALID_DATE_OF_BIRTH, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Staff.STAFF_NOT_VALID_DATE_OF_BIRTH));
                    deDateofBirth.Focus();
                    return false;
                }
                //else if (DateTime.Compare(deDateofJoining.DateTime, DateTime.Parse(System.DateTime.Now.ToShortDateString()).Date) > 0)
                //{
                //    XtraMessageBox.Show(MessageCatalog.Payroll.Staff.STAFF_NOT_VALID_DATE_OF_JOINING_DATE, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    deDateofJoining.Focus();
                //    return false;
                //}

                else if (DateTime.Compare(deDateofBirth.DateTime, deDateofJoining.DateTime) >= 0)
                {
                    //XtraMessageBox.Show(MessageCatalog.Payroll.Staff.STAFF_NOT_VALID_DATE_OF_JOINING_DATE_AGAINEST_DATE_OF_BIRTH, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Staff.STAFF_NOT_VALID_DATE_OF_JOINING_DATE_AGAINEST_DATE_OF_BIRTH));
                    //deDateofBirth.Focus();
                    deDateofJoining.Focus();
                    return false;
                }
            }

            // else if statement is changed into another If Statement
            if ((!string.IsNullOrEmpty(deRetirementDate.Text.Trim())) || string.IsNullOrEmpty(deRetirementDate.Text.Trim()))
            {
                if (DateTime.Compare(deRetirementDate.DateTime, DateTime.Parse(System.DateTime.Now.ToShortDateString()).Date) == 0)
                {
                    //XtraMessageBox.Show(MessageCatalog.Payroll.Staff.STAFF_NOT_VALID_RETIREMENT_DATE, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Staff.STAFF_NOT_VALID_RETIREMENT_DATE));
                    xtcStaff.SelectedTabPageIndex = 1;
                    deRetirementDate.Focus();
                    return false;
                }
                else if ((!(deRetirementDate.Text == "")) && DateTime.Compare(deDateofJoining.DateTime, deRetirementDate.DateTime) > 0)
                {
                    //XtraMessageBox.Show(MessageCatalog.Payroll.Staff.STAFF_NOT_VALID_DATE_OF_JOINING_DATE_AGAINEST_DATE_OF_BIRTH, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowMessageBox("Date of Joining is greater than Retirement Date");
                    xtcStaff.SelectedTabPageIndex = 1;
                    deRetirementDate.Focus();
                    return false;
                }
                else if (!string.IsNullOrEmpty(deLeavingDate.Text.Trim()))
                {
                    if (DateTime.Compare(deDateofJoining.DateTime, deLeavingDate.DateTime) == 0)
                    {
                        //XtraMessageBox.Show(MessageCatalog.Payroll.Staff.STAFF_NOT_VALID_LEAVEING_DATE, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Staff.STAFF_NOT_VALID_LEAVEING_DATE));
                        xtcStaff.SelectedTabPageIndex = 1;
                        deLeavingDate.Focus();
                        return false;
                    }
                }
            }
            return true;
        }

        private bool isStaffCodeExists()
        {
            bool isvalid = false;
            if (StaffId == 0)
            {
                string StaffCode = txtStaffCode.Text;
                using (clsPayrollStaff staff = new clsPayrollStaff())
                {
                    int StaffCodeExists = staff.GetstaffIdByStaffCode(StaffCode);
                    if (StaffCodeExists > 0)
                    {
                        XtraMessageBox.Show("Staff Code is Available", "Staff", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtStaffCode.Focus();
                        isvalid = false;
                        //if (this.ShowConfirmationMessage("Staff Code is Available,Do you want to update it?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        //{
                        //    isvalid = true;
                        //}
                        //else
                        //{
                        //    txtStaffCode.Focus();
                        //    isvalid = false;
                        //}
                    }
                    else
                    {
                        isvalid = true;
                    }
                }
            }
            return isvalid;

        }

        private void ClearValues()
        {
            txtStaffCode.Text = txtStaffCode.Text = txtFirstName.Text = txtLastName.Text = txtMiddleName.Text = txtFatherHusbandName.Text = txtMotherName.Text = txtNoofChildren.Text = string.Empty;
            glkpGender.Text = glkGroup.Text = deDateofBirth.Text = deDateofJoining.Text = deRetirementDate.Text = glkpBloodGroup.Text = string.Empty;
            txtKnownAs.Text = deLeavingDate.Text = glkpLeavingReason.Text = txtDesignation.Text = txtQualificationDegree.Text = string.Empty;
            txtCommentPerformance.Text = txtAddress.Text = txtTelephoneNo.Text = txtMobileNo.Text = txtEmergency.Text = txtEmail.Text = txtDependent1.Text = txtDependent2.Text = txtDependent3.Text = txtWorkExperience.Text = string.Empty;
            deContractDate.EditValue = string.Empty;
            txtScalePay1.Text = txtScalePay2.Text = txtScalePay3.Text = txtAccountNumber.Text = glkIncrementMonth.SelectedText = txtUAN.Text = txtYOS.Text = string.Empty;
            txtEarning1.Text = txtEarning2.Text = txtEarning3.Text = txtDeduction1.Text = Deduction2.Text = txtPayingDays.Text = txtPayingDays.Text = txtPAN.Text = txtUAN.Text = txtAADHARNo.Text = string.Empty;
            txtESIIPNo.Text = string.Empty;
            glkpProject.EditValue = 0;
            ApplicableStatutoryCompliances = string.Empty;
        }

        private void AssignValues()
        {
            try
            {
                using (clsPayrollStaff Staff = new clsPayrollStaff())
                {
                    clsGeneral.EMP_No = StaffId.ToString();
                    Staff.AccountYearId = this.UtilityMember.NumberSet.ToInteger(ActiveAccId);
                    Staff.getStaffDetails();
                    this.txtStaffCode.Text = Staff.EmpNo;
                    if (Staff.DOJ != "")
                        deDateofJoining.DateTime = commem.DateSet.ToDate(Staff.DOJ, false);
                    else
                        this.deDateofBirth.Text = "";
                    txtFirstName.Text = Staff.FirstName;
                    txtMiddleName.Text = Staff.MiddleName;
                    txtFatherHusbandName.Text = Staff.FatherHusbandName;
                    txtMotherName.Text = Staff.MotherName;
                    txtNoofChildren.Text = Staff.NoofChildren;
                    Blood = Staff.BloodGroup;
                    deContractDate.Text = Staff.lastDateofContract;
                    this.txtLastName.Text = Staff.LastName;
                    Gender = Staff.Gender;
                    //Category = Staff.Category;
                    this.txtKnownAs.Text = Staff.KnownAs;
                    if (Staff.DOB != "")
                        deDateofBirth.DateTime = commem.DateSet.ToDate(Staff.DOB, false);
                    else
                        deDateofBirth.Text = "";
                    if (Staff.LeaveDate != "")
                    {
                        deLeavingDate.DateTime = commem.DateSet.ToDate(Staff.LeaveDate, false);
                    }

                    txtQualificationDegree.Text = Staff.Degree;
                    txtDesignation.Text = Staff.Designation;
                    string split = Staff.ScaleofPay;

                    if (split != "--" && split != "")
                    {
                        string[] getscale = new string[4000];
                        getscale = split.Split('-');
                        if (getscale.Count() >= 1)
                        {
                            this.txtScalePay1.Text = getscale[0];
                        }
                        if (getscale.Count() >= 2)
                        {
                            this.txtScalePay2.Text = getscale[1];
                        }
                        if (getscale.Count() >= 3)
                        {
                            this.txtScalePay3.Text = getscale[2];
                        }
                    }
                    else
                    {
                        txtScalePay1.Text = txtScalePay2.Text = txtScalePay3.Text = "";
                    }
                    if (Staff.RetireDate != "")
                    {
                        deRetirementDate.DateTime = commem.DateSet.ToDate(Staff.RetireDate, false);
                    }
                    txtDesignation.Text = Staff.Designation;
                    LeavingReason = Staff.LeaveRemarks;
                    //    txtWagesBasic.Text = Staff.MaxWagesBasic.ToString();
                    //    txtWagesDA.Text = Staff.MaxWagesHRA.ToString();
                    txtEarning1.Text = Staff.Earning1;
                    txtEarning2.Text = Staff.Earning2;
                    txtEarning3.Text = Staff.Earning3;
                    txtDeduction1.Text = Staff.Deduction1;
                    Deduction2.Text = Staff.Deduction2;
                    txtPayingDays.Text = Staff.PayingSalaryDays;
                    txtUAN.Text = Staff.UAN;
                    txtPAN.Text = Staff.PanNo;
                    txtAADHARNo.Text = Staff.AAdharNo;
                    Month = Staff.IncrementMonth;
                    txtAccountNumber.Text = Staff.AccountNo;
                    txtYOS.Text = Staff.YOS.ToString();
                    txtCommentPerformance.Text = Staff.Commandonperformance;
                    txtAddress.Text = Staff.Address;
                    txtTelephoneNo.Text = Staff.Telephone;
                    txtMobileNo.Text = Staff.Mobile;
                    txtEmergency.Text = Staff.EmergencyContact;
                    txtEmail.Text = Staff.Email;
                    txtDependent1.Text = Staff.Dependent1;
                    txtDependent2.Text = Staff.Dependent2;
                    txtDependent3.Text = Staff.Dependent3;
                    txtWorkExperience.Text = Staff.WorkExperience;

                    txtAccountIFSCCode.Text = Staff.AccountIFSCCODE.Trim();
                    txtAccountBankBranch.Text = Staff.AccountBankBranch.Trim();
                    ApplicableStatutoryCompliances = Staff.StaffStatutoryCompliance;
                    txtESIIPNo.Text = Staff.StaffESIIPNo.Trim();
                    clsGeneral.EMP_No = null;

                    AssignProjectGroupValues();

                    //On 09/08/2019 to lock staff comment ------------------------------------------------------
                    LockStaffComment();
                    //------------------------------------------------------------------------------------------

                    glkpPaymentMode.EditValue = Staff.PayrollPaymentModeId;
                    glkpDepartment.EditValue = DepartmentId =   Staff.PayrollDepartmentId;
                    glkpWorkLocation.EditValue = WorkLocationId =  Staff.PayrollWorkLocationId;
                    glkpNameTitle.EditValue = NameTitleid =  Staff.NameTitleId;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
        }

        private void AssignProjectGroupValues()
        {
            using (clsPayrollStaff Staff = new clsPayrollStaff())
            {
                ProjectId = Staff.GetProjectidByStaffId(StaffId.ToString());
                GroupId = Staff.GetGroupidByStaffId(StaffId.ToString());
            }
        }

        private void LoadCombo()
        {
            LoadLeavingReason();
            LoadIncrementMonth();
            LoadGendger();
            LoadBloodGroup();
            LoadDesignationAutoComplete();

            //On 15/02/2022, to load applicalbe statutory compliances
            LoadStatutoryCompliances();
            
            //On 14/02/2023, To load payment modes
            LoadPaymentModes();

            //On 21/11/2023, To load Payroll Departments and Work Location
            LoadPayrollDepartments();
            LoadPayrollWorkLocation();
            LoadNameTitle();
        }

        private void LoadStatutoryCompliances()
        {
            PayRollProcessComponent payrollcomponent = new PayRollProcessComponent();
            DataView dvProcessComponent = this.UtilityMember.EnumSet.GetEnumDataSource(payrollcomponent, Sorting.Descending);
            string onlyStatutoryCompliances = "ID IN (" + (Int32)PayRollProcessComponent.EPF + "," + (Int32)PayRollProcessComponent.ESI + "," + (Int32)PayRollProcessComponent.PT + ")";
            dvProcessComponent.RowFilter = onlyStatutoryCompliances;
            dvProcessComponent.Sort = "NAME";
            
            chkListStatutoryCompliances.Properties.DataSource = dvProcessComponent.ToTable();
            chkListStatutoryCompliances.Properties.ValueMember = "Id";
            chkListStatutoryCompliances.Properties.DisplayMember = "Name";
        }

        private void LoadDesignationAutoComplete()
        {
            try
            {
                using (clsPayrollStaff staffSystem = new clsPayrollStaff())
                {
                    resultArgs = staffSystem.AutoFetchDesignation();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataView dvNarration = resultArgs.DataSource.Table.AsDataView();
                        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            collection.Add(dr[staffSystem.AppSchema.STFPERSONAL.DESIGNATIONColumn.ColumnName].ToString());
                        }
                        txtDesignation.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        txtDesignation.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        txtDesignation.MaskBox.AutoCompleteCustomSource = collection;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void LoadLeavingReason()
        {
            clsGeneral.LeavingReason leavingReason = new clsGeneral.LeavingReason();
            DataView dvLeavingReason = GetDescriptionfromEnumType(leavingReason);
            objCommon.BindGridLookUpCombo(glkpLeavingReason, dvLeavingReason.ToTable(), "Name", "Name");
            glkpLeavingReason.EditValue = LeavingReason;

        }

        private void LoadPaymentModes()
        {
            try
            {
                using (PayrollSystem Paysystem = new PayrollSystem())
                {
                    resultArgs = Paysystem.FetchPayrollPaymentMode();
                    glkpPaymentMode.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        commem.ComboSet.BindGridLookUpComboEmptyItem(glkpPaymentMode, resultArgs.DataSource.Table, Paysystem.AppSchema.STFPERSONAL.PAYMENT_MODEColumn.ColumnName,
                                Paysystem.AppSchema.STFPERSONAL.PAYMENT_MODE_IDColumn.ColumnName, true, " ");
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void LoadPayrollDepartments()
        {
            try
            {
                using (PayrollDepartmentSystem payrolldep = new PayrollDepartmentSystem())
                {
                    resultArgs = payrolldep.FetchPayrollDepartments();
                    glkpDepartment.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        commem.ComboSet.BindGridLookUpComboEmptyItem(glkpDepartment, resultArgs.DataSource.Table,
                                payrolldep.AppSchema.PayrollDepartment.DEPARTMENTColumn.ColumnName,
                                payrolldep.AppSchema.PayrollDepartment.DEPARTMENT_IDColumn.ColumnName, true, " ");

                        glkpDepartment.EditValue = DepartmentId != 0 ? DepartmentId : 0;
                        if (DepartmentId == 0)
                        {
                            glkpDepartment.EditValue = "";
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void LoadPayrollWorkLocation()
        {
            try
            {
                using (PayrollWorkLocationSystem payrollworklocation = new PayrollWorkLocationSystem())
                {
                    resultArgs = payrollworklocation.FetchPayrollWorkLocation();
                    glkpWorkLocation.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        commem.ComboSet.BindGridLookUpComboEmptyItem(glkpWorkLocation, resultArgs.DataSource.Table,
                                payrollworklocation.AppSchema.PayrollWorkLocation.WORK_LOCATIONColumn.ColumnName,
                                payrollworklocation.AppSchema.PayrollWorkLocation.WORK_LOCATION_IDColumn.ColumnName, true, " ");

                        glkpWorkLocation.EditValue = WorkLocationId != 0 ? WorkLocationId : 0;
                        if (WorkLocationId == 0)
                        {
                            glkpWorkLocation.EditValue = "";
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void LoadNameTitle()
        {
            try
            {
                using (NameTitleSystem nametitle = new NameTitleSystem())
                {
                    resultArgs = nametitle.FetchNameTitle();
                    glkpNameTitle.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        commem.ComboSet.BindGridLookUpComboEmptyItem(glkpNameTitle, resultArgs.DataSource.Table,
                                nametitle.AppSchema.NameTitle.NAME_TITLEColumn.ColumnName,
                                nametitle.AppSchema.NameTitle.NAME_TITLE_IDColumn.ColumnName, true, " ");

                        glkpNameTitle.EditValue = NameTitleid != 0 ? NameTitleid : 0;
                        if (NameTitleid == 0)
                        {
                            glkpNameTitle.EditValue = "";
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        //private void LoadLeavingReason()
        //{
        //    //Processtype LedgerProcessType = new Processtype();
        //    clsGeneral.LeavingReason leavingReason = new clsGeneral.LeavingReason();
        //    DataView dvLeavingReason = GetDescriptionfromEnumType(leavingReason);
        //    DataTable dtLeavingReason = dvLeavingReason.ToTable();
        //    using (MappingSystem mappingSystem = new MappingSystem())
        //    {
        //        if (dtLeavingReason != null && dtLeavingReason.Rows.Count > 0)
        //        {
        //            commem.ComboSet.BindGridLookUpCombo(glkpLeavingReason, dtLeavingReason, "Name", "Id");
        //            //if (ComponentId == 0)
        //            //{
        //            glkpLeavingReason.EditValue = glkpLeavingReason.Properties.GetKeyValue(0);
        //            //}
        //            //else
        //            //{
        //            //    // glkpProcessledger.EditValue = objPayroll.ProcessLedgerId;
        //            //}
        //        }
        //    }
        //}

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
                    string[] descs = new string[15];
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

        private void LoadIncrementMonth()
        {
            clsGeneral.IncrementMonth IncrementMonth = new clsGeneral.IncrementMonth();
            DataView dvIncrementMonth = enumSetMember.GetEnumDataSource(IncrementMonth, Sorting.None);
            objCommon.BindGridLookUpCombo(glkIncrementMonth, dvIncrementMonth.ToTable(), "Name", "Id");
            glkIncrementMonth.EditValue = Month;
        }

        private void LoadGendger()
        {

            clsGeneral.Gender clsGender = new clsGeneral.Gender();
            DataView dvGendger = enumSetMember.GetEnumDataSource(clsGender, Sorting.None);
            objCommon.BindGridLookUpCombo(glkpGender, dvGendger.ToTable(), "Name", "Name");
            glkpGender.EditValue = Gender;
        }

        private void LoadBloodGroup()
        {
            clsGeneral.BloodGroup bloodgroup = new clsGeneral.BloodGroup();
            DataView dvBloodgroup = GetDescriptionfromEnumType(bloodgroup);
            objCommon.BindGridLookUpCombo(glkpBloodGroup, dvBloodgroup.ToTable(), "Name", "Name");
            glkpBloodGroup.EditValue = Blood;
        }

        private int SaveStaffDetails()
        {
            try
            {
                using (clsPayrollStaff Staff = new clsPayrollStaff())
                {
                    if (isValidRecord() && isValidDate()) //&& IsvalidDteofJoin())
                    {
                        if (clsGeneral.checkPayrollexists())
                        {
                            Int32 StaffPayGroupId = glkGroup.EditValue != null ? commem.NumberSet.ToInteger(glkGroup.EditValue.ToString()) : 0;
                            int ProjectId = glkpProject.EditValue != null ? commem.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;

                            Staff.StaffId = StaffId == 0 ? 0 : StaffId;
                            Staff.IncrementMonth = "0";//glkIncrementMonth.EditValue == null ? "0" : glkIncrementMonth.EditValue.ToString();
                            Staff.ScaleofPay = txtScalePay1.Text + "-" + txtScalePay2.Text + "-" + txtScalePay3.Text;
                            Staff.ThirdPartyId = "";
                            List<object> selecteditems = chkListStatutoryCompliances.Properties.Items.GetCheckedValues();
                            Staff.StaffStatutoryCompliance = string.Join(",", selecteditems);
                            //On 16/02/2002, staff esi details
                            Staff.StaffStatutoryCompliance = ApplicableStatutoryCompliances;
                            Staff.StaffESIIPNo = txtESIIPNo.Text.Trim();

                            //On 14/02/2023, To assing Bank Account, IFSCOOCDE, BankBranch and payment Mode
                            Staff.AccountNo = txtAccountNumber.Text.Trim();
                            Staff.AccountIFSCCODE = txtAccountIFSCCode.Text.Trim();
                            Staff.AccountBankBranch= txtAccountBankBranch.Text.Trim();
                            Staff.PayrollPaymentModeId = glkpPaymentMode.EditValue == null ? 0 : UtilityMember.NumberSet.ToInteger(glkpPaymentMode.EditValue.ToString());

                            //On 21/11/2023, To assign Payroll department and Payroll Work location
                            Staff.PayrollDepartmentId = glkpDepartment.EditValue == null ? 0 : UtilityMember.NumberSet.ToInteger(glkpDepartment.EditValue.ToString());
                            Staff.PayrollWorkLocationId = glkpWorkLocation.EditValue == null ? 0 : UtilityMember.NumberSet.ToInteger(glkpWorkLocation.EditValue.ToString());
                            Staff.NameTitleId = glkpNameTitle.EditValue == null ? 0 : UtilityMember.NumberSet.ToInteger(glkpNameTitle.EditValue.ToString());
                            
                            resultArgs = Staff.savePayrollStaffData(StaffPayGroupId, txtStaffCode.Text,
                                txtStaffCode.Text,
                                txtFirstName.Text,
                                txtMiddleName.Text,
                                txtFatherHusbandName.Text,
                                txtMotherName.Text,
                                txtNoofChildren.Text,
                                glkpBloodGroup.Text,
                                deContractDate.Text,
                                txtLastName.Text,
                                glkpGender.Text,
                                deDateofBirth.Text,
                                deDateofJoining.Text,
                                deRetirementDate.Text,
                                txtKnownAs.Text,
                                deLeavingDate.Text,
                                glkpLeavingReason.Text,
                                txtDesignation.Text,
                                txtQualificationDegree.Text,
                                string.Empty, 0, 0,
                                txtEarning1.Text,
                                txtEarning2.Text,
                                txtEarning3.Text,
                                txtDeduction1.Text,
                                Deduction2.Text,
                                txtPayingDays.Text,
                                txtUAN.Text,
                                StaffId == 0 ? clsPayrollConstants.PAYROLL_STAFF_INSERT : clsPayrollConstants.PAYROLL_STAFF_EDIT,
                                0,
                                txtScalePay1.Text, txtAccountNumber.Text, this.commem.NumberSet.ToDouble(txtYOS.Text), txtCommentPerformance.Text,
                                txtAddress.Text,
                                txtMobileNo.Text,
                                txtTelephoneNo.Text,
                                txtEmergency.Text,
                                txtEmail.Text,
                                txtDependent1.Text,
                                txtDependent2.Text,
                                txtDependent3.Text,
                                txtWorkExperience.Text,
                                txtPAN.Text,
                                txtAADHARNo.Text, txtAccountIFSCCode.Text, txtAccountBankBranch.Text);

                            if (resultArgs.Success)
                            {
                                string grpStaff = StaffId != 0 ? StaffId.ToString() : Staff.RtnStaffid;
                                RetrunValue = commem.NumberSet.ToInteger(grpStaff);
                                /*using (clsPrGroupStaff GroupStaff = new clsPrGroupStaff())
                                {
                                    resultArgs = GroupStaff.DeleteStaffGroup(grpStaff);
                                    if (resultArgs.Success)
                                    {
                                        string strResult = GroupStaff.SaveNewStaffInGroup(GroupId, grpStaff);
                                        if (SettingProperty.PayrollFinanceEnabled)
                                        {
                                            if (!string.IsNullOrEmpty(grpStaff))
                                            {
                                                resultArgs = GroupStaff.DeleteProjectStaff(grpStaff);
                                                if (resultArgs.Success)
                                                {
                                                    resultArgs = GroupStaff.SaveProjectStaff(ProjectId, grpStaff);
                                                }
                                                else
                                                {
                                                    if (resultArgs.Success)
                                                    {
                                                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.Staff.STAFF_DETAILS_SAVED));
                                                        glkpProject.Focus();
                                                    }
                                                }
                                            }
                                        }
                                    }  
                                }*/
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.Staff.STAFF_DETAILS_SAVED));
                                    if (StaffId == 0)
                                    {
                                        ClearValues();
                                        txtStaffCode.Focus();
                                    }
                                }
                                else
                                {
                                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Staff.STAFF_DETAILS_NOT_SAVED));
                                }
                            }
                            else
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.Staff.STAFF_DETAILS_NOT_SAVED) + ", " + resultArgs.Message);
                            }
                        }
                        else
                        {
                            this.ShowMessageBox("There is no active PayRoll Month");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
            return RetrunValue;
        }

        protected void SetBorderColor(TextEdit txtEdit)
        {
            txtEdit.Properties.Appearance.BorderColor = string.IsNullOrEmpty(txtEdit.Text) ? Color.Red : Color.Empty;
        }

        protected void SetBorderColor(GridLookUpEdit glkpEdit)
        {
            glkpEdit.Properties.Appearance.BorderColor = string.IsNullOrEmpty(glkpEdit.Text) ? Color.Red : Color.Empty;
        }

        protected void SetBorderColor(DateEdit deEdit)
        {
            deEdit.Properties.Appearance.BorderColor = string.IsNullOrEmpty(deEdit.Text) ? Color.Red : Color.Empty;
        }

        private bool CheckLoanExistsforStaff(string staffId)
        {
            bool isvalid = false;
            using (clsPrGroupStaff ObjGroupStaff = new clsPrGroupStaff())
            {
                int IsLoanExist = ObjGroupStaff.CheckLoanExists(staffId);
                if (IsLoanExist > 0)
                {
                    isvalid = true;
                }
            }
            return isvalid;
        }
        #endregion

        private void btnLockUnlock_Click(object sender, EventArgs e)
        {
            //On 09/08/2019, lock and unlock staff comments based on passwords
            if (!string.IsNullOrEmpty(AppSetting.PayrollPassword))
            {
                
                if (txtPassword.Text == this.AppSetting.PayrollPassword)
                {
                    if (lciCommentPerformance.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                    {
                        if (this.ShowConfirmationMessage("Are you sure to lock Staff Comments ?. You could modify Staff Comments after giving Payroll Staff Comment Password.",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            LockStaffComment();
                            txtPassword.Text = string.Empty;
                            txtPassword.Select();
                            txtPassword.Focus();
                        }
                    }
                    else
                    {
                        if (txtPassword.Text == this.AppSetting.PayrollPassword)
                        {
                            btnLockUnlock.Text = "Lock";
                            txtPassword.Text = string.Empty;
                            lciCommentPerformance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            txtCommentPerformance.Select();
                            txtCommentPerformance.Focus();
                        }      
                    }
                }
                else
                {
                    this.ShowMessageBox("Invalid Payroll Staff Comment Password");
                    txtPassword.Select();
                    txtPassword.Focus();
                }
            }
            else
            {
                this.ShowMessageBox("Invalid Payroll Staff Comment Password");
                txtPassword.Select();
                txtPassword.Focus();
            }
        }

        /// <summary>
        /// On 09/08/2019, to lock staff comment
        /// </summary>
        private void LockStaffComment()
        {
            if (!string.IsNullOrEmpty(AppSetting.PayrollPassword))
            {
                lciCommentPerformance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcPassword.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                btnLockUnlock.Text = "Unlock";
            }
            else
            {
                lciCommentPerformance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcPassword.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            lcLockUnlock.Visibility = lcPassword.Visibility;
        }

        private void glkpDepartment_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
                {
                    frmPayrollDepartment frmDepartment = new frmPayrollDepartment();
                    frmDepartment.ShowDialog();
                    LoadPayrollDepartments();

                    if (DepartmentId == 0)
                    {
                        DataTable dt = glkpDepartment.Properties.DataSource as DataTable;
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            using (PayrollDepartmentSystem depsystem = new PayrollDepartmentSystem())
                            {
                                dt.DefaultView.Sort = depsystem.AppSchema.PayrollDepartment.DEPARTMENT_IDColumn.ColumnName + " DESC";
                                int depid = UtilityMember.NumberSet.ToInteger(dt.DefaultView[0][depsystem.AppSchema.PayrollDepartment.DEPARTMENT_IDColumn.ColumnName].ToString());
                                if (depid > 0)
                                {
                                    glkpDepartment.EditValue = depid;
                                }
                                dt.DefaultView.Sort = depsystem.AppSchema.PayrollDepartment.DEPARTMENTColumn.ColumnName;
                            }
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

        private void glkpWorkLocation_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
                {
                    frmPayrollWorkLocation frmWorklocation = new frmPayrollWorkLocation();
                    frmWorklocation.ShowDialog();
                    LoadPayrollWorkLocation();

                    if (WorkLocationId == 0)
                    {
                        DataTable dt = glkpWorkLocation.Properties.DataSource as DataTable;
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            using (PayrollWorkLocationSystem worklocationsystem = new PayrollWorkLocationSystem())
                            {
                                dt.DefaultView.Sort = worklocationsystem.AppSchema.PayrollWorkLocation.WORK_LOCATION_IDColumn.ColumnName + " DESC";
                                int worklocationid = UtilityMember.NumberSet.ToInteger(dt.DefaultView[0][worklocationsystem.AppSchema.PayrollWorkLocation.WORK_LOCATION_IDColumn.ColumnName].ToString());
                                if (worklocationid > 0)
                                {
                                    glkpWorkLocation.EditValue = worklocationid;
                                }
                                dt.DefaultView.Sort = worklocationsystem.AppSchema.PayrollWorkLocation.WORK_LOCATIONColumn.ColumnName;
                            }
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

        private void glkpNameTitle_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
                {
                    frmStaffNameTitle frmstafnameTitle = new frmStaffNameTitle();
                    frmstafnameTitle.ShowDialog();
                    LoadNameTitle();

                    if (NameTitleid == 0)
                    {
                        DataTable dt = glkpNameTitle.Properties.DataSource as DataTable;
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            using (PayrollDepartmentSystem depsystem = new PayrollDepartmentSystem())
                            {
                                dt.DefaultView.Sort = depsystem.AppSchema.NameTitle.NAME_TITLE_IDColumn.ColumnName + " DESC";
                                int nametitleid = UtilityMember.NumberSet.ToInteger(dt.DefaultView[0][depsystem.AppSchema.NameTitle.NAME_TITLE_IDColumn.ColumnName].ToString());
                                if (nametitleid > 0)
                                {
                                    glkpNameTitle.EditValue = nametitleid;
                                }
                                dt.DefaultView.Sort = depsystem.AppSchema.NameTitle.NAME_TITLEColumn.ColumnName;
                            }
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
    }

}