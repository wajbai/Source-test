using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Model.UIModel.Master;
using Bosco.Model.UIModel;
using Bosco.Utility;
using System.Xml;
namespace ACPP.Modules.Master
{
    public partial class frmAuditingInfo : frmFinanceBaseAdd
    {
        #region EventsDeclaration
        public event EventHandler OnUpdate;
        #endregion

        #region VariableDeclaration
        ResultArgs resultArgs = null;
        private int AuditId = 0;
        #endregion

        #region Constructor
        public frmAuditingInfo()
        {
            InitializeComponent();
        }
        public frmAuditingInfo(int AuditInfo)
            : this()
        {
            AuditId = AuditInfo;
        }
        #endregion

        #region Properties

        private DataView dvProjectInfo = null;
        private DataView ProjectInfo
        {
            get
            {
                return dvProjectInfo;
            }
            set
            {
                dvProjectInfo = value;
            }
        }

        #endregion

        #region Events
        /// <summary>
        /// To load Auditor Info Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAuditingInfo_Load(object sender, EventArgs e)
        {
            LoadAuditTypeDetails();
            LoadProject();
            GetProjectList();
            GetAuditorList();
            LoadProjectDate();
            SetTitle();
            AssignAuditorInfoDetails();
            if (AuditId > 0)
            {
                if (dtAuditedOn.DateTime == DateTime.MinValue)
                {
                    dtAuditedOn.Text = string.Empty;
                }
            }
            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                ApplyRights();
            }
            else
            {
                glkpAuditor.Properties.Buttons[1].Visible = true;
                glkpAuditTypeData.Properties.Buttons[1].Visible = true;
                glkProject.Properties.Buttons[1].Visible = true;
            }
        }

        /// <summary>
        /// To Save the Auditor Info Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidatAuditorInfoDetails())
                {
                    using (AuditInfoSystem auditInfoSystem = new AuditInfoSystem())
                    {
                        auditInfoSystem.AuditInfoId = AuditId == 0 ? (int)AddNewRow.NewRow : AuditId;
                        auditInfoSystem.ProjectId = this.UtilityMember.NumberSet.ToInteger(glkProject.EditValue.ToString());
                        auditInfoSystem.AuditBegin = dtAuditBegin.DateTime;
                        auditInfoSystem.AuditEnd = dtAuditEnd.DateTime;
                        auditInfoSystem.AuditedON = dtAuditedOn.DateTime;
                        auditInfoSystem.AuditTypeId = this.UtilityMember.NumberSet.ToInteger(glkpAuditTypeData.EditValue.ToString());
                        auditInfoSystem.AuditorId = this.UtilityMember.NumberSet.ToInteger(glkpAuditor.EditValue.ToString());
                        auditInfoSystem.Notes = txtMemoNotes.Text;
                        resultArgs = auditInfoSystem.SaveAuditorInfoDetails();
                        if (resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            if (OnUpdate != null)
                            {
                                OnUpdate(this, e);
                            }
                            ClearControl();
                            glkProject.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        private void lkpProject_EditValueChanged(object sender, EventArgs e)
        {
            LoadProjectDate();
        }

        /// <summary>
        /// To Close the Auditor Info form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region User Rights
        private void ApplyRights()
        {
            bool createauditorrights = (CommonMethod.ApplyUserRights((int)Forms.CreateAuditor) != 0);
            glkpAuditor.Properties.Buttons[1].Visible = createauditorrights;

            bool createaudittyperights = (CommonMethod.ApplyUserRights((int)Forms.CreateAuditType) != 0);
            glkpAuditTypeData.Properties.Buttons[1].Visible = createaudittyperights;

            bool createprojectrights = (CommonMethod.ApplyUserRights((int)Forms.CreateProject) != 0);
            glkProject.Properties.Buttons[1].Visible = createprojectrights;
        }
        #endregion


        #region Methods
        /// <summary>
        /// To fetch Auditor Members
        /// </summary>
        private void GetAuditorList()
        {
            try
            {
                using (DonorAuditorSystem AuditorsList = new DonorAuditorSystem())
                {
                    resultArgs = AuditorsList.FetchAuditorList();
                    if (resultArgs.Success)
                    {
                        //this.UtilityMember.ComboSet.BindLookUpEditCombo(, resultArgs.DataSource.Table, AuditorsList.AppSchema.DonorAuditor.NAMEColumn.ToString(), AuditorsList.AppSchema.DonorAuditor.DONAUD_IDColumn.ToString());
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpAuditor, resultArgs.DataSource.Table, AuditorsList.AppSchema.DonorAuditor.NAMEColumn.ToString(), AuditorsList.AppSchema.DonorAuditor.DONAUD_IDColumn.ToString());
                        //lkpAuditor.EditValue = lkpAuditor.Properties.GetDataSourceValue(lkpAuditor.Properties.ValueMember, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        private void LoadAuditTypeDetails()
        {
            try
            {
                using (AuditSystem auditSystem = new AuditSystem())
                {
                    resultArgs = auditSystem.FetchAuditTypeDetails();
                    if (resultArgs.Success)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpAuditTypeData, resultArgs.DataSource.Table, auditSystem.AppSchema.Audit.AUDIT_TYPEColumn.ColumnName, auditSystem.AppSchema.Audit.AUDIT_TYPE_IDColumn.ColumnName);
                        Refresh();
                    }
                    else
                    {
                        MessageRender.ShowMessage(resultArgs.Message, true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            finally
            {
            }

        }

        /// <summary>
        /// To Set Current Date Details
        /// </summary>
        private void LoadProjectDate()
        {
            try
            {
                using (ProjectSystem projectSystem = new ProjectSystem())
                {

                    int ProjectId = glkProject.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkProject.EditValue.ToString()) : 0;
                    if (dvProjectInfo != null)
                    {
                        dvProjectInfo.RowFilter = projectSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName.ToString() + "=" + ProjectId;
                        foreach (DataRow dr in dvProjectInfo.ToTable().Rows)
                        {
                            dtAuditBegin.DateTime = this.UtilityMember.DateSet.ToDate(dr[projectSystem.AppSchema.Project.DATE_STARTEDColumn.ColumnName].ToString(), false);
                            dtAuditBegin.Properties.MinValue = this.UtilityMember.DateSet.ToDate(dr[projectSystem.AppSchema.Project.DATE_STARTEDColumn.ColumnName].ToString(), false);
                            dtAuditEnd.DateTime = this.UtilityMember.DateSet.ToDate(dr[projectSystem.AppSchema.Project.DATE_STARTEDColumn.ColumnName].ToString(), false);
                            dtAuditedOn.DateTime = this.UtilityMember.DateSet.ToDate(dr[projectSystem.AppSchema.Project.DATE_STARTEDColumn.ColumnName].ToString(), false);
                            dtAuditedOn.Properties.MinValue = this.UtilityMember.DateSet.ToDate(dr[projectSystem.AppSchema.Project.DATE_STARTEDColumn.ColumnName].ToString(), false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }

        /// <summary>
        /// To Get Project Members
        /// </summary>
        private void GetProjectList()
        {
            try
            {
                using (MappingSystem Map = new MappingSystem())
                {
                    resultArgs = Map.FetchPJLookup();
                    if (resultArgs.Success)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkProject, resultArgs.DataSource.Table, Map.AppSchema.Project.PROJECTColumn.ColumnName, Map.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                        // glkProject.EditValue = glkProject.Properties.GetKeyValue(0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }

        }

        /// <summary>
        /// To Validate the Auditor Info Details
        /// </summary>
        /// <returns></returns>
        private bool ValidatAuditorInfoDetails()
        {
            bool isAuditorInfo = true;
            if (string.IsNullOrEmpty(glkProject.Text))
            {
                ShowMessageBox(this.GetMessage(MessageCatalog.Master.AuditorInfo.AUDITOR_INFO_PROJECT_EMPTY));
                SetBorderColor(glkProject);
                isAuditorInfo = false;
                glkProject.Focus();
            }
            else if (string.IsNullOrEmpty(dtAuditBegin.Text))
            {
                ShowMessageBox(this.GetMessage(MessageCatalog.Master.AuditorInfo.AUDITOR_INFO_STARTEDON_EMPTY));
                SetBorderColor(dtAuditBegin);
                isAuditorInfo = false;
                dtAuditBegin.Focus();
            }
            else if (string.IsNullOrEmpty(dtAuditEnd.Text))
            {
                ShowMessageBox(this.GetMessage(MessageCatalog.Master.AuditorInfo.AUDITOR_INFO_CLOSEDON_EMPTY));
                SetBorderColor(dtAuditEnd);
                isAuditorInfo = false;
                dtAuditEnd.Focus();
            }
            else if (string.IsNullOrEmpty(glkpAuditTypeData.Text))
            {
                ShowMessageBox(this.GetMessage(MessageCatalog.Master.AuditLockType.AUDIT_TYPE_IS_EMPTY));
                SetBorderColor(glkpAuditTypeData);
                isAuditorInfo = false;
                glkpAuditTypeData.Focus();
            }
            else if (string.IsNullOrEmpty(glkpAuditor.Text))
            {
                ShowMessageBox(this.GetMessage(MessageCatalog.Master.AuditLockType.AUDITOR_IS_EMPTY));
                SetBorderColor(glkpAuditor);
                isAuditorInfo = false;
                glkpAuditor.Focus();
            }
            else if (!this.UtilityMember.DateSet.ValidateDate(dtAuditBegin.DateTime, dtAuditEnd.DateTime))
            {
                ShowMessageBox(this.GetMessage(MessageCatalog.Master.AuditorInfo.AUDITOR_BEGIN_DATE));
                isAuditorInfo = false;
                dtAuditBegin.Focus();
            }
            else if (dtAuditedOn.DateTime != DateTime.MinValue)
            {
                if (!this.UtilityMember.DateSet.ValidateDate(dtAuditBegin.DateTime, dtAuditedOn.DateTime))
                {
                    ShowMessageBox(this.GetMessage(MessageCatalog.Master.AuditorInfo.AUDITOR_ON_FROM));
                    isAuditorInfo = false;
                    dtAuditedOn.Focus();
                }
                else if (!this.UtilityMember.DateSet.ValidateDate(dtAuditedOn.DateTime, dtAuditEnd.DateTime))
                {
                    ShowMessageBox(this.GetMessage(MessageCatalog.Master.AuditorInfo.AUDITOR_ON_TO));
                    isAuditorInfo = false;
                    dtAuditedOn.Focus();
                }

            }
            //else if (!this.UtilityMember.DateSet.ValidateDate(dtAuditBegin.DateTime, dtAuditedOn.DateTime))
            //{
            //   ShowMessageBox(this.GetMessage(MessageCatalog.Master.AuditorInfo.AUDITOR_ON_FROM));
            //    isAuditorInfo = false;
            //    dtAuditedOn.Focus();
            //}
            //else if (!this.UtilityMember.DateSet.ValidateDate(dtAuditEnd.DateTime, dtAuditedOn.DateTime))
            //{
            //    ShowMessageBox(this.GetMessage(MessageCatalog.Master.AuditorInfo.AUDITOR_ON_TO));
            //    isAuditorInfo = false;
            //    dtAuditedOn.Focus();
            //}
            else
            {
            }
            return isAuditorInfo;
        }

        /// <summary>
        /// To Clear the Auditor Info Details
        /// </summary>
        private void ClearControl()
        {
            if (AuditId == 0)
            {
                txtMemoNotes.Text = string.Empty;
                glkProject.EditValue = null;
                glkpAuditTypeData.EditValue = null;
                glkpAuditor.EditValue = null;
            }
        }

        /// <summary>
        /// To Set the Title for add form
        /// </summary>
        private void SetTitle()
        {
            this.Text = AuditId == 0 ? this.GetMessage(MessageCatalog.Master.AuditorInfo.AUDITOR_INFO_ADD) : this.GetMessage(MessageCatalog.Master.AuditorInfo.AUDITOR_INFO_EDIT);
            glkProject.Focus();
        }

        /// <summary>
        /// To Assign the Data to the Controls 
        /// </summary>
        private void AssignAuditorInfoDetails()
        {
            try
            {
                if (AuditId != 0)
                {
                    using (AuditInfoSystem auditInfoSystem = new AuditInfoSystem(AuditId))
                    {
                        glkProject.EditValue = auditInfoSystem.ProjectId;
                        dtAuditBegin.EditValue = auditInfoSystem.AuditBegin;
                        dtAuditEnd.EditValue = auditInfoSystem.AuditEnd;
                        if (!auditInfoSystem.AuditedON.Equals(dtAuditedOn.Properties.MinValue))
                        {
                            dtAuditedOn.DateTime = this.UtilityMember.DateSet.ToDate(auditInfoSystem.AuditedON.ToString(), false);
                        }
                        glkpAuditTypeData.EditValue = auditInfoSystem.AuditTypeId;
                        glkpAuditor.EditValue = auditInfoSystem.AuditorId;
                        txtMemoNotes.Text = auditInfoSystem.Notes;
                        //LoadAuditTypeDetails();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void LoadProject()
        {
            try
            {
                using (ProjectSystem projectSystem = new ProjectSystem())
                {
                    resultArgs = projectSystem.FetchProjects();
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        dvProjectInfo = resultArgs.DataSource.Table.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }

        // Load the immediate added Audit Type details

        public void LoadAuditTypeCombo()
        {
            if (this.AppSetting.LockMasters == (int)YesNo.No)
            {
                frmAuditTypeAdd frmAuditTypeAdd = new frmAuditTypeAdd();
                frmAuditTypeAdd.ShowDialog();
                if (frmAuditTypeAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                {
                    LoadAuditTypeDetails();
                    if (frmAuditTypeAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmAuditTypeAdd.ReturnValue.ToString()) > 0)
                    {
                        glkpAuditTypeData.EditValue = this.UtilityMember.NumberSet.ToInteger(frmAuditTypeAdd.ReturnValue.ToString());
                    }
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
            }
        }

        // Load the immediate added Project in to the Combo

        public void LoadProjectCombo()
        {
            if (this.AppSetting.LockMasters == (int)YesNo.No)
            {
                frmProjectAdd frmprojectAdd = new frmProjectAdd();
                frmprojectAdd.ShowDialog();
                if (frmprojectAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                {
                    GetProjectList();
                    if (frmprojectAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmprojectAdd.ReturnValue.ToString()) > 0)
                    {
                        glkProject.EditValue = this.UtilityMember.NumberSet.ToInteger(frmprojectAdd.ReturnValue.ToString());
                    }
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
            }
        }

        public void LoadAuditorCombo()
        {
            if (this.AppSetting.LockMasters == (int)YesNo.No)
            {
                frmAddAuditor frmAddAuditor = new frmAddAuditor();
                frmAddAuditor.ShowDialog();
                if (frmAddAuditor.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                {
                    GetAuditorList();
                    if (frmAddAuditor.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmAddAuditor.ReturnValue.ToString()) > 0)
                    {
                        glkpAuditor.EditValue = this.UtilityMember.NumberSet.ToInteger(frmAddAuditor.ReturnValue.ToString());
                    }
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
            }
        }

        #endregion


        private void glkpAuditType_Properties_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                LoadAuditTypeCombo();
                Refresh();
            }
        }

        private void glkpAuditor_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                LoadAuditorCombo();
            }
        }

        private void glkpAuditor_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpAuditor);
        }

        private void glkpAuditTypeData_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpAuditTypeData);
        }

        private void glkProject_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                LoadProjectCombo();
            }
        }
    }
}