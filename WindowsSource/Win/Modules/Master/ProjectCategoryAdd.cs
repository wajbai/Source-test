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
using Bosco.Utility;
using Bosco.Model.UIModel;
using System.Xml;

namespace ACPP.Modules.Master
{
    public partial class ProjectCategoryAdd : frmBaseAdd
    {
        #region EventsDeclaration
        public event EventHandler UpdateHeld;
        #endregion

        #region VariableDeclaration
        int ProjectCategoryId = 0;
        #endregion

        #region Constructor
        public ProjectCategoryAdd()
        {
            InitializeComponent();
        }
        public ProjectCategoryAdd(int projectCategoryid)
            :this()
        {
            ProjectCategoryId = projectCategoryid;
        }
        #endregion

        #region Events
        /// <summary>
        /// To load the ProjectCategoryDetails
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProjectCategoryAdd_Load(object sender, EventArgs e)
        {
            SetTitle();
            AssignProjectCategoryDetails();
        }

        /// <summary>
        /// To Save Project Category Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateProjectCategoryDetails())
                {
                    ResultArgs resultArgs = null;
                    using (ProjectCatogorySystem projectCategorySystem = new ProjectCatogorySystem())
                    {
                        projectCategorySystem.ProjectCatogoryId = ProjectCategoryId == 0 ? (int)AddNewRow.NewRow : ProjectCategoryId;
                        projectCategorySystem.ProjectCatogoryName = txtProjectCategory.Text.Trim();
                        resultArgs = projectCategorySystem.SaveProjectCatogoryDetails();
                        if (resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                            ClearControl();
                            txtProjectCategory.Focus();
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
        /// To Set Color for Project Catogory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtProjectCategory_Leave(object sender, EventArgs e)
        {
            SetBorderColor(txtProjectCategory);
        }

        /// <summary>
        /// Close the Project Catogory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Methods
        /// <summary>
        /// To Validate the Project Category Details
        /// </summary>
        /// <returns></returns>
        public bool ValidateProjectCategoryDetails()
        {
            bool isValue = true;
            if (string.IsNullOrEmpty(txtProjectCategory.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.ProjectCatogory.PROJECT_CATOGORY_EMPTY));
                this.SetBorderColor(txtProjectCategory);
                isValue = false;
                txtProjectCategory.Focus();
            }
            return isValue;
        }

        /// <summary>
        /// To Clear the Project Category Name
        /// </summary>
        private void ClearControl()
        {
            if (ProjectCategoryId == 0)
            {
                txtProjectCategory.Text =  string.Empty;
            }
            txtProjectCategory.Focus();
        }

        /// <summary>
        /// To Set the Caption for Project Catogory
        /// </summary>
        private void SetTitle()
        {
            this.Text = ProjectCategoryId == 0 ? this.GetMessage(MessageCatalog.Master.ProjectCatogory.PROJECT_CATEGORY_ADD_CAPTION) : this.GetMessage(MessageCatalog.Master.ProjectCatogory.PROJECT_CATEGORY_EDIT_CAPTION);
            txtProjectCategory.Focus();
        }

        /// <summary>
        /// To reload the Project Category
        /// </summary>
        public void AssignProjectCategoryDetails()
        {
            try
            {
                if (ProjectCategoryId != 0)
                {
                    using (ProjectCatogorySystem projectCategorySystem = new ProjectCatogorySystem(ProjectCategoryId)) 
                    {
                        txtProjectCategory.Text = projectCategorySystem.ProjectCatogoryName;
                    }
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