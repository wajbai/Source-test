using System;
using System.Windows.Forms;

using Bosco.Model.UIModel;
using Bosco.Utility;

namespace ACPP.Modules.Master
{
    public partial class frmProjectCategoryView : frmFinanceBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        private int EditProjectCategoryView = 0;
        #endregion

        #region Constructor
        public frmProjectCategoryView()
        {
            InitializeComponent();
        }
        #endregion

        #region Property
        private int projectCategoryId = 0;
        public int ProjectCategoryId
        {
            get
            {
                RowIndex = gvProjectCategory.FocusedRowHandle;
                projectCategoryId = gvProjectCategory.GetFocusedRowCellValue(colProCategoryId) != null ? this.UtilityMember.NumberSet.ToInteger(gvProjectCategory.GetFocusedRowCellValue(colProCategoryId).ToString()) : 0;
                return projectCategoryId;
            }
            set
            {
                projectCategoryId = value;
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// To load the Project Catogory 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProjectCategoryView_Load(object sender, EventArgs e)
        {
            ApplyUserRights();

            //Set Visible to Add/Edit/Delete
            this.LockMasters(ucToolBarProjectCategory);

            //gvProjectCategory.OptionsBehavior.Editable = true;
            // gvProjectCategory.
            //gvProjectCategory.OptionsSelection.EnableAppearanceHideSelection = false;
            //gvProjectCategory.FocusedRowHandle = 0;
            //gvProjectCategory.SelectRow(0);
            //  gvProjectCategory.
            //  gvProjectCategory.FocusedColumn = colProjectCategoryName;
            //gvProjectCategory.SelectCells(0, colProjectCategoryName, 0, colProjectCategoryName);
            // gvProjectCategory.ShowEditor();
            //gcProjectCategory.Focus();

            if (this.AppSetting.IS_SDB_INM)
                colITRGroup.Visible = true;
            else
                colITRGroup.Visible = false;

        }

        private void frmProjectCategoryView_Activated(object sender, EventArgs e)
        {
            //Added by Carmel Raj
            SetVisibileShortCuts(false, true);
            GetProjectCategoryDetails();
        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(ProjectCategory.CreateProjectCategory);
            this.enumUserRights.Add(ProjectCategory.EditProjectCategory);
            this.enumUserRights.Add(ProjectCategory.DeleteProjectCategory);
            this.enumUserRights.Add(ProjectCategory.PrintProjectCategory);
            this.enumUserRights.Add(ProjectCategory.ViewProjectCategory);
            this.ApplyUserRights(ucToolBarProjectCategory, this.enumUserRights, (int)Menus.ProjectCategory);
        }

        /// <summary>
        /// To Show the Add form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarProjectCategory_AddClicked(object sender, EventArgs e)
        {
            ShowForm((int)AddNewRow.NewRow);
        }

        /// <summary>
        /// To Edit the Project Catogory details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarProjectCategory_EditClicked(object sender, EventArgs e)
        {
            EditProjectCategory();
        }

        /// <summary>
        /// To Edit the Project Catogory Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvProjectCategory_DoubleClick(object sender, EventArgs e)
        {
            EditProjectCategory();
        }

        /// <summary>
        /// To Delete Project Catogory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarProjectCategory_DeleteClicked(object sender, EventArgs e)
        {
            try
            {

                if (gvProjectCategory.RowCount != 0)
                {
                    if (ProjectCategoryId != 0)
                    {
                        using (ProjectCatogorySystem projectCategorySystem = new ProjectCatogorySystem())
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                resultArgs = projectCategorySystem.DeleteProjectCatogoryDetails(ProjectCategoryId);
                                if (resultArgs.Success)
                                {
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    GetProjectCategoryDetails();
                                }
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
        /// To Print the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarProjectCategory_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcProjectCategory, this.GetMessage(MessageCatalog.Master.ProjectCatogory.PROJECT_CATEGORY_PRINT_CAPTION), PrintType.DT, gvProjectCategory);
        }

        /// <summary>
        /// To View the Number of Record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvProjectCategory_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvProjectCategory.RowCount.ToString();
        }

        /// <summary>
        /// To Enable the AutoFilter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvProjectCategory.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvProjectCategory, colProjectCategoryName);
            }
        }

        /// <summary>
        /// To close the View Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarProjectCategory_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// To refresh the Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarProjectCategory_RefreshClicked(object sender, EventArgs e)
        {
            GetProjectCategoryDetails();
        }
        #endregion

        #region Methods
        /// <summary>
        /// To Get the Project Catogory Details
        /// </summary>
        public void GetProjectCategoryDetails()
        {
            try
            {
                using (ProjectCatogorySystem projectCategorySystem = new ProjectCatogorySystem())
                {
                    resultArgs = projectCategorySystem.FetchProjectCatogoryDetails();
                    gcProjectCategory.DataSource = resultArgs.DataSource.Table;
                    gcProjectCategory.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// To Show form based on the Selection
        /// </summary>
        /// <param name="projectCategoryId"></param>
        public void ShowForm(int projectCategoryId)
        {
            try
            {
                if (this.AppSetting.LockMasters == (int)YesNo.No)
                {
                    frmProjectCategoryAdd frmCategory = new frmProjectCategoryAdd(projectCategoryId);
                    frmCategory.UpdateHeld += new EventHandler(OnUpdateHeld);
                    frmCategory.ShowDialog();
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
        }

        /// <summary>
        /// To refresh the Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnUpdateHeld(object sender, EventArgs e)
        {

            GetProjectCategoryDetails();
            gvProjectCategory.FocusedRowHandle = RowIndex;
        }

        /// <summary>
        /// To set the Id of Project Category
        /// </summary>
        public void EditProjectCategory()
        {
            try
            {
                if (this.isEditable)
                {
                    if (gvProjectCategory.RowCount != 0)
                    {
                        if (ProjectCategoryId != 0)
                        {
                            ShowForm(ProjectCategoryId);
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
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
        #endregion
        private void frmProjectCategoryView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void frmProjectCategoryView_EnterClicked(object sender, EventArgs e)
        {
            EditProjectCategory();
        }
    }
}