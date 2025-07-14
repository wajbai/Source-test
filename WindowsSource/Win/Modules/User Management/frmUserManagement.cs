using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

using ACPP.Modules.Master;
using Bosco.Utility;
using Bosco.Model.UIModel;
using Bosco.Utility.CommonMemberSet;
using System.Collections;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList.Nodes;

namespace ACPP.Modules.User_Management
{
    public partial class frmUserManagement : frmFinanceBaseAdd
    {
        #region Variables
        private string _UserRole = string.Empty;
        const string SELECT_COL = "SELECT_COL";
        ResultArgs resultArgs;
        #endregion

        #region Constructor
        public frmUserManagement()
        {
            InitializeComponent();
        }

        public frmUserManagement(string UserRole)
            : this()
        {
            _UserRole = UserRole;
        }

        #endregion

        #region Events
        private void frmUserManagement_Load(object sender, EventArgs e)
        {
            LoadUserRole();
            LoadRights();
            LoadProject();
            if (!string.IsNullOrEmpty(_UserRole))
            {
                glkpUserRoles.Text = _UserRole;
                glkpUserRoles.Properties.ReadOnly = true;
            }

        }

        private void btnExpandAll_Click(object sender, EventArgs e)
        {
            trListUserManagement.ExpandAll();
        }

        private void btnCollapseAll_Click(object sender, EventArgs e)
        {
            trListUserManagement.CollapseAll();
        }

        private void gvProject_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle == DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                if (e.Column.FieldName == SELECT_COL)
                {
                    DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryTextEdit = new RepositoryItemTextEdit();
                    e.RepositoryItem = repositoryTextEdit;
                }
            }
        }

        private void glkpUserRoles_EditValueChanged(object sender, EventArgs e)
        {
            LoadProject();
            LoadRights();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvProject.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                gvProject.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                gvProject.FocusedColumn = gvColProjectName;
                gvProject.ShowEditor();
            }
        }

        private void glkpUserRoles_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                if (this.AppSetting.LockMasters == (int)YesNo.No)
                {
                    frmUserRoleAdd frmUserRoleAdd = new frmUserRoleAdd();
                    frmUserRoleAdd.ShowDialog();
                    if (frmUserRoleAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                    {
                        LoadUserRole();
                        if (frmUserRoleAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmUserRoleAdd.ReturnValue.ToString()) > 0)
                        {
                            glkpUserRoles.EditValue = this.UtilityMember.NumberSet.ToInteger(frmUserRoleAdd.ReturnValue.ToString());
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
                }
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateUserRights())
                {
                    using (UserRightsSystem userRightsSystem = new UserRightsSystem())
                    {
                        this.ShowWaitDialog();
                        userRightsSystem.UserRoleId = this.UtilityMember.NumberSet.ToInteger(glkpUserRoles.EditValue.ToString());
                        DataTable dtUserRights = GetCheckNodes((DataTable)trListUserManagement.DataSource);
                        userRightsSystem.dtUserRights = dtUserRights;
                        userRightsSystem.dtUserProject = (DataTable)gcProject.DataSource;
                        resultArgs = userRightsSystem.SaveUserRights();
                        if (resultArgs.Success)
                        {
                            ShowSuccessMessage(this.GetMessage(MessageCatalog.User.USER_RIGHTS_RESTART_CONFIRM));
                        }
                        this.CloseWaitDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dtMapping = gcProject.DataSource as DataTable;
            if (dtMapping.Columns.Contains(SELECT_COL))
                dtMapping.Select().ToList<DataRow>().ForEach(r => { r[SELECT_COL] = chkSelectAll.Checked; });
        }
        private void frmUserManagement_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void trListUserManagement_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.ParentNode == null)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.DimGray;
            }
            if (IsParentExpanded(e.Node))
            {
                if (e.Node.ParentNode == null)
                {
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.ForeColor = Color.Black;
                }
            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Methods
        public void LoadUserRole()
        {
            try
            {
                using (ManageSecuritySystem manageSecurityManagement = new ManageSecuritySystem())
                {
                    resultArgs = manageSecurityManagement.FetchUserRoles();
                    if (resultArgs.Success)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpUserRoles, resultArgs.DataSource.Table, manageSecurityManagement.AppSchema.UserRole.USERROLEColumn.ColumnName, manageSecurityManagement.AppSchema.UserRole.USERROLE_IDColumn.ColumnName);
                        glkpUserRoles.EditValue = glkpUserRoles.Properties.GetKeyValue(0);
                        //  glkpUserRoles.EditValue = this.LoginUser.LoginUserRoleId;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, false);
            }
            finally { }
        }

        private void LoadRights()
        {
            try
            {
                ResultArgs resultArgs = null;
                if (glkpUserRoles.EditValue != null)
                {
                    using (UserRightsSystem userRightsSystem = new UserRightsSystem())
                    {
                        userRightsSystem.UserRoleId = this.UtilityMember.NumberSet.ToInteger(glkpUserRoles.EditValue.ToString());
                        resultArgs = userRightsSystem.FetchUserRights();

                        // resultArgs.DataSource.Table.Columns.Add("SELECT", typeof(Int32));

                        trListUserManagement.DataSource = resultArgs.DataSource.Table;
                        trListUserManagement.RefreshDataSource();
                        trListUserManagement.ExpandAll();
                        DataTable dt = GetDefaultChecked(resultArgs.DataSource.Table);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, false);
            }
            finally { }
        }

        public DataTable GetDefaultChecked(DataTable dtRights)
        {
            foreach (DevExpress.XtraTreeList.Nodes.TreeListNode Modulenode in trListUserManagement.Nodes)
            {
                foreach (DevExpress.XtraTreeList.Nodes.TreeListNode Activitynode in Modulenode.Nodes)
                {
                    foreach (DevExpress.XtraTreeList.Nodes.TreeListNode Featurenode in Activitynode.Nodes)
                    {
                        foreach (DataRow dr in dtRights.Rows)
                        {
                            if ((dr["ACTIVITY_ID"].ToString()) == Featurenode["ACTIVITY_ID"].ToString() && this.UtilityMember.NumberSet.ToInteger(dr["HAS_RIGHTS"].ToString()) == 1)
                            {
                                trListUserManagement.SetNodeCheckState(Featurenode, CheckState.Checked, true);
                            }
                        }
                    }
                    if (!Activitynode.HasChildren)
                    {
                        foreach (DataRow dr in dtRights.Rows)
                        {
                            if ((dr["ACTIVITY_ID"].ToString()) == Activitynode["ACTIVITY_ID"].ToString() && this.UtilityMember.NumberSet.ToInteger(dr["HAS_RIGHTS"].ToString()) == 1)
                            {
                                trListUserManagement.SetNodeCheckState(Activitynode, CheckState.Checked, true);
                            }
                        }
                    }
                }
            }
            return dtRights;
        }

        private void LoadProject()
        {
            if (glkpUserRoles.EditValue != null)
            {
                using (UserRightsSystem userRightsSystem = new UserRightsSystem())
                {
                    userRightsSystem.UserRoleId = this.UtilityMember.NumberSet.ToInteger(glkpUserRoles.EditValue.ToString());
                    resultArgs = userRightsSystem.FetchUserProject();
                    if (resultArgs.Success && resultArgs.DataSource != null)
                        gcProject.DataSource = resultArgs.DataSource.Table;
                }
            }
        }

        private DataTable GetCheckNodes(DataTable dt)
        {
            dt = UnCheckedNodes(dt);
            foreach (DevExpress.XtraTreeList.Nodes.TreeListNode node in trListUserManagement.GetAllCheckedNodes())
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (node["ACTIVITY_ID"].ToString() == dr["ACTIVITY_ID"].ToString())
                    {
                        dr["HAS_RIGHTS"] = Convert.ToInt32(node.CheckState);
                    }
                }
            }
            return dt;
        }

        private DataTable UnCheckedNodes(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                dr["HAS_RIGHTS"] = 0;
            }
            return dt;
        }

        private bool IsParentExpanded(TreeListNode node)
        {
            if (node.ParentNode == null) return (node.Expanded);
            return IsParentExpanded(node.ParentNode);
        }

        private bool ValidateUserRights()
        {
            bool isValid = true;
            try
            {
                if (gcProject.DataSource == null)
                {
                    //this.ShowMessageBox("There is no project.");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.UserManagementForms.NO_PROJECT));
                    glkpUserRoles.Focus();
                    isValid = false;
                    return isValid;
                }
                else if (gcProject.DataSource != null)
                {
                    DataTable dtProject = gcProject.DataSource as DataTable;
                    DataView dvProject = dtProject.DefaultView;
                    dvProject.RowFilter = "SELECT_COL=1";
                    if (dvProject == null || dvProject.Count == 0)
                    {
                        LoadProject();
                        //this.ShowMessageBox("Project is not selected.Select a project to map to the user role.");
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.UserManagementForms.PROJECT_NOT_SELECT));
                        isValid = false;
                        gcProject.Focus();
                    }
                    dvProject.RowFilter = "";
                }
                else if (glkpUserRoles.EditValue == null || glkpUserRoles.EditValue.ToString().Equals("0") || string.IsNullOrEmpty(glkpUserRoles.Text))
                {
                    //ShowMessageBox("User Role can not be empty");
                    ShowMessageBox(this.GetMessage(MessageCatalog.Master.UserManagementForms.USER_ROLE_CANNOT_EMPTY));
                    this.SetBorderColorForGridLookUpEdit(glkpUserRoles);
                    isValid = false;
                    glkpUserRoles.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
            return isValid;
        }

        #endregion
    }
}