using System;
using System.Data;
using System.Windows.Forms;

using Bosco.Utility;
using Bosco.Model.UIModel;
using Bosco.Model.Transaction;


namespace ACPP.Modules.Master
{
    public partial class frmProjectView : frmFinanceBase
    {

        #region Constructor
        public frmProjectView()
        {
            InitializeComponent();
        }
        #endregion

        #region Variable Declaration
        private int RowIndex;
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Properties
        private int _ProjectId;
        private int ProjectId
        {
            get
            {
                RowIndex = gvProjectView.FocusedRowHandle;
                _ProjectId = gvProjectView.GetFocusedRowCellValue(colProjectId) != null ? this.UtilityMember.NumberSet.ToInteger(gvProjectView.GetFocusedRowCellValue(colProjectId).ToString()) : 0;
                return _ProjectId;
            }
            set
            {
                _ProjectId = value;
            }
        }

        private string _ProjectName;
        private string ProjectName
        {
            get
            {
                 RowIndex = gvProjectView.FocusedRowHandle;
                 _ProjectName = gvProjectView.GetFocusedRowCellValue(colProjectName) != null ? gvProjectView.GetFocusedRowCellValue(colProjectName).ToString() : "";
                 return _ProjectName;
            }
        }
        private DataTable dtProjectView { get; set; }
        #endregion

        #region Events
        /// <summary>
        /// Load the details of projects and vouchers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmProjectView_Load(object sender, EventArgs e)
        {
            ApplyUserRights();

            //Set Visible to Add/Edit/Delete
            this.LockMasters(ucToolBarProjectview);

            //On 26/08/2019, For SDB Congregation, Lock adding projects even Master is Locked -----------
            //This temp purpose, it shlould be taken from license key
            //if (this.AppSetting.IS_SDB_CONGREGATION)
            if (this.AppSetting.IS_SDB_INM)
            {
                ucToolBarProjectview.VisibleAddButton = ucToolBarProjectview.VisibleEditButton = ucToolBarProjectview.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            //--------------------------------------------------------------------------------------------

            //On 12/10/2020, Enable edit option to modify projectg closed on even if lock master is enabled 
            //On 22/09/2023, for sdb mumbai, lock ledger editing feature
            if (AppSetting.LockMasters == 1 && AppSetting.HeadofficeCode.ToUpper() != "SDBINB")
            {
                ucToolBarProjectview.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            if (AppSetting.HeadofficeCode.ToUpper() == "SDBINB")
            {
                this.isEditable = false;
            }
            //----------------------------------------------
            
            lblProjectNameValue.Text = string.Empty;
            detProjectDateClosed.Text = string.Empty;
        }

        private void frmProjectView_Activated(object sender, EventArgs e)
        {
            //Added by Carmel Raj
            SetVisibileShortCuts(false, true);
            LoadProjectDetails();
        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(Project.CreateProject);
            this.enumUserRights.Add(Project.EditProject);
            this.enumUserRights.Add(Project.DeleteProject);
            this.enumUserRights.Add(Project.PrintProject);
            this.enumUserRights.Add(Project.ViewProject);
            this.ApplyUserRights(ucToolBarProjectview, this.enumUserRights, (int)Menus.Project);
        }

        /// <summary>
        /// Add new project details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarProjectview_AddClicked(object sender, EventArgs e)
        {
            //if (isAddable)
            //{
            ShowForm((int)AddNewRow.NewRow);
            //}
            //else
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.NO_RIGHTS_FOR_ADDITION));
            //}
        }

        /// <summary>
        /// Edit the project details based on it Id.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarProjectview_EditClicked(object sender, EventArgs e)
        {
            ShowEditProjectForm();
        }

        /// <summary>
        /// Edit the project details based on its id.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvProjectView_DoubleClick(object sender, EventArgs e)
        {
            //if (!this.AppSetting.IS_SDB_CONGREGATION)
            
            //On 03/12/2020, To show edit option even if master is locked
            //if (!this.AppSetting.IS_SDB_INM)
            //{
            //    ShowEditProjectForm();
            //}
            ShowEditProjectForm();
        }

        /// <summary>
        /// Delete the project details based on project id.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarProjectview_DeleteClicked(object sender, EventArgs e)
        {
            DeleteProjectDetails();
        }

        /// <summary>
        /// Print the details of the Projects and vouchers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarProjectview_PrintClicked(object sender, EventArgs e)
        {
            //if (isPrintable)
            //{
            PrintGridViewDetails(gcProjectView, this.GetMessage(MessageCatalog.Master.Project.PROJECT_PRINT_CAPTION), PrintType.DS, gvProjectView, true);
            //}
            //else
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.NO_RIGHTS_FOR_PRINT));
            //}
        }

        /// <summary>
        /// Enable or disable auto row filter for Projects and vouchers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvProjectView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            gvLedger.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            gvVoucher.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvProjectView, colProjectName);
            }
            pnlProjectDetails.Visible = false;
        }

        /// <summary>
        /// Set record count for projects.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvProjectView_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvProjectView.RowCount.ToString();
        }

        /// <summary>
        /// Close the Project forms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarProjectview_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gcProjectView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {

            }
        }

        private void frmProjectView_ShowFilterClicked(object sender, EventArgs e)
        {

            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        /// <summary>
        /// Refresh the grid after adding and editing the values. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadProjectDetails();
            gvProjectView.FocusedRowHandle = RowIndex;
        }

        /// <summary>
        /// To refresh the Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarProjectview_RefreshClicked(object sender, EventArgs e)
        {
            LoadProjectDetails();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Add or Edit the project details based on its id.
        /// </summary>
        /// <param name="ProjectId"></param>
        private void ShowForm(int ProjectId)
        {
            try
            {
                if (this.AppSetting.LockMasters == (int)YesNo.No)
                {
                    frmProjectAdd frmProject = new frmProjectAdd(ProjectId);
                    frmProject.UpdateHeld += new EventHandler(OnUpdateHeld);
                    frmProject.ShowDialog();
                }
                else if (this.AppSetting.LockMasters == (int)YesNo.Yes)
                {
                    //On 12/10/2020, Enable edit option to modify projectg closed on even if lock master is enabled 
                    ShowEditProjectDetails();
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// Edit the project details based on its id.
        /// </summary>
        private void ShowEditProjectForm()
        {
            if (this.isEditable)
            {
                if (gvProjectView.RowCount != 0)
                {
                    if (ProjectId != 0)
                    {
                        ShowForm(ProjectId);
                    }
                    else
                    {
                        if (!chkShowFilter.Checked)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NOSELECTION_FOR_EDIT));
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_USER_RIGHTS));
            }
        }

        /// <summary>
        /// Load the detail of projects and Vouchers by setting up Relations for Projects and Vouchers.
        /// </summary>
        private void LoadProjectDetails()
        {
            try
            {
                using (ProjectSystem projectSystem = new ProjectSystem())
                {
                    gcProjectView.DataSource = projectSystem.LoadProjectsDetails();
                    gcProjectView.DataMember = "Project";
                    pnlProjectDetails.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// Delete the projects details based on its id.
        /// </summary>
        private void DeleteProjectDetails()
        {
            try
            {
                if (gvProjectView.RowCount != 0)
                {

                    if (ProjectId != 0)
                    {
                        using (ProjectSystem projectSystem = new ProjectSystem())
                        {
                            projectSystem.ProjectId = ProjectId;
                            if (projectSystem.CheckProjectExits() == 0)
                            {
                                if (projectSystem.CheckLedgerProjectExist() <= 1)
                                {
                                    int IsExist = projectSystem.CheckProjectExits();
                                    if (IsExist == 0)
                                    {
                                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            resultArgs = projectSystem.DeleteProjectDetails();
                                            if (resultArgs.Success)
                                            {
                                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                                LoadProjectDetails();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Project.PROJECT_CANNOT_DELETE_OPENING_BALANCE));
                                    }
                                }
                                else
                                {
                                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Project.PROJECT__LEDGER_MAPPED));
                                }
                            }
                            else
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Project.PROJECT_TRANSACTION_MADE));
                            }
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NOSELECTION_FOR_DELETE));
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// This method is used to modify project closed on, If License master is locked
        /// </summary>
        private void ShowEditProjectDetails()
        {
            Int32 closedby = 0;
            string msg = "As this Project is closed by Head Office, You can't modify its closed date.";

            pnlProjectDetails.Visible = true;
            lblProjectNameValue.Text = string.Empty;
            detProjectDateClosed.Text = string.Empty;
            if (pnlProjectDetails.Visible && gvProjectView.DataSource != null && gvProjectView.RowCount > 0)
            {
                if (gvProjectView.GetRowCellValue(gvProjectView.FocusedRowHandle, colProjectName.FieldName) != null)
                {
                    lblProjectNameValue.Text = gvProjectView.GetRowCellValue(gvProjectView.FocusedRowHandle, colProjectName.FieldName).ToString();
                    string closedon = gvProjectView.GetRowCellValue(gvProjectView.FocusedRowHandle, colClosedOn.FieldName).ToString();
                    closedby = UtilityMember.NumberSet.ToInteger(gvProjectView.GetRowCellValue(gvProjectView.FocusedRowHandle, colClosedBy.FieldName) == null ? "0" : gvProjectView.GetRowCellValue(gvProjectView.FocusedRowHandle, colClosedBy.FieldName).ToString());
                    if (!String.IsNullOrEmpty(closedon))
                    {
                        DateTime dtClosedOn = UtilityMember.DateSet.ToDate(closedon, false);
                        if (dtClosedOn == DateTime.MinValue)
                        {
                            detProjectDateClosed.Text = string.Empty;
                        }
                        else
                        {
                            detProjectDateClosed.DateTime = dtClosedOn;
                        }
                    }
                }
                  //On 04/07/2023, Lock to remove ledger closed date if it is closed by HO
                detProjectDateClosed.Enabled = (closedby == 0);
                detProjectDateClosed.ToolTip = (closedby == 1 ? msg : "");

                if (closedby == 0)
                {
                    detProjectDateClosed.Select();
                    detProjectDateClosed.Focus();
                }
                else
                {
                    pnlProjectDetails.Visible = false;
                    this.ShowMessageBox(msg);

                }
            }
        }
        #endregion

        private void frmProjectView_EnterClicked(object sender, EventArgs e)
        {
            ShowEditProjectForm();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            DateTime detDateStarted;
            DateTime detDateClosed;
            bool isProject = false;

            if (pnlProjectDetails.Visible)
            {
                if (gvProjectView.DataSource != null && gvProjectView.RowCount > 0)
                {
                    if (gvProjectView.GetRowCellValue(gvProjectView.FocusedRowHandle, colProjectName.FieldName) != null)
                    {
                        if (this.ShowConfirmationMessage("Are you sure to update Project Closed Date ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            string startedon = gvProjectView.GetRowCellValue(gvProjectView.FocusedRowHandle, colStartedOn.FieldName).ToString();
                            string closedon = detProjectDateClosed.DateTime.ToShortDateString();
                            if (!String.IsNullOrEmpty(startedon))
                            {
                                detDateStarted = UtilityMember.DateSet.ToDate(startedon, false);
                                detDateClosed = UtilityMember.DateSet.ToDate(closedon, false);
                                isProject = true;

                                if (detDateClosed != DateTime.MinValue)
                                {
                                    if (!this.UtilityMember.DateSet.ValidateDate(detDateStarted, detDateClosed))
                                    {
                                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Project.PROJECT_DATE_VALIDATION));
                                        detProjectDateClosed.Focus();
                                        isProject = false;
                                    }

                                    if (isProject && ProjectId > 0)
                                    {
                                        using (VoucherTransactionSystem vouchertranssystem = new VoucherTransactionSystem())
                                        {
                                            ResultArgs resultArgs = vouchertranssystem.CheckTransVoucherDetailsByDateProject(ProjectId, detProjectDateClosed.DateTime);
                                            if (resultArgs.Success && resultArgs.DataSource.Sclar.ToInteger > 0)
                                            {
                                                this.ShowMessageBox("Transaction is made for this Closed date, Project can not be closed.");
                                                detProjectDateClosed.Focus();
                                                isProject = false;
                                            }
                                        }
                                    }
                                }

                                if (isProject)
                                {
                                    using (ProjectSystem projectsystem = new ProjectSystem())
                                    {
                                        projectsystem.ProjectId = ProjectId;
                                        projectsystem.Closed_On = detProjectDateClosed.DateTime;
                                        projectsystem.ClosedBy = detProjectDateClosed.Enabled ? 0 : 1; //0 - Closed By BO, 1- Closed by HO
                                        ResultArgs resultArgs = projectsystem.UpdateProjectClosedDate();
                                        if (resultArgs != null)
                                        {
                                            isProject = resultArgs.Success;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (isProject)
            {
                pnlProjectDetails.Visible = false;
                LoadProjectDetails();
                gvProjectView.FocusedRowHandle = RowIndex;
            }
            else
            {
                detProjectDateClosed.Select();
                detProjectDateClosed.Focus();
            }
        }
    }
}