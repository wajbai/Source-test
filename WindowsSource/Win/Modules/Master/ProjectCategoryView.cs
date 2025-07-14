using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Model.UIModel;
using Bosco.Utility;

namespace ACPP.Modules.Master
{
    public partial class ProjectCategoryView : frmBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        #endregion

        #region Constructor
        public ProjectCategoryView()
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

            GetProjectCategoryDetails();
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
            if (ProjectCategoryId != 0)
            {
                EditProjectCategory();
            }
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
                if (ProjectCategoryId != 0)
                {
                    using (ProjectCatogorySystem projectCategorySystem = new ProjectCatogorySystem())
                    {
                        if (gvProjectCategory.RowCount != 0)
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
                        else
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
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

        /// <summary>
        /// To Print the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarProjectCategory_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcProjectCategory, this.GetMessage(MessageCatalog.Master.ProjectCatogory.PROJECT_CATEGORY_PRINT_CAPTION));
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
                ProjectCategoryAdd frmCategory = new ProjectCategoryAdd(projectCategoryId);
                frmCategory.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmCategory.ShowDialog();
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
                if (gvProjectCategory.RowCount != 0)
                {
                    ShowForm(ProjectCategoryId);
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
        #endregion

        
    }
}