using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Utility;
using Bosco.Model.UIModel.Master;
using Bosco.Model.UIModel;
namespace ACPP.Modules.Master
{
    public partial class frmProjectCatogory : frmBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public frmProjectCatogory()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// To load the Project Catogory Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmProjectCatogory_Load(object sender, EventArgs e)
        {
            GetProjectCatogory();

        }

        /// <summary>
        /// To Save the Project Catogory Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (ProjectCatogorySystem projectCatogorySystem = new ProjectCatogorySystem())
                {
                    DataView dvProjectCatogory = ((DataTable)gcProjectCatogory.DataSource).DefaultView;
                    dvProjectCatogory.RowStateFilter = DataViewRowState.ModifiedCurrent | DataViewRowState.Added;
                    if (dvProjectCatogory != null && dvProjectCatogory.Count != 0)
                    {
                        for (int i = 0; i < dvProjectCatogory.Count; i++)
                        {
                            projectCatogorySystem.ProjectCatogoryId = this.UtilityMember.NumberSet.ToInteger(dvProjectCatogory[i][projectCatogorySystem.AppSchema.ProjectCatogory.PROJECT_CATOGORY_IDColumn.ColumnName].ToString());
                            projectCatogorySystem.ProjectCatogoryName = (dvProjectCatogory[i][projectCatogorySystem.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn.ColumnName].ToString());
                            resultArgs = projectCatogorySystem.SaveProjectCatogoryDetails();
                        }
                        if (resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            GetProjectCatogory();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            finally { }
        }

        /// <summary>
        /// To Delete the Project CatogoryDetails
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (ProjectCatogorySystem projectCatogorySystem = new ProjectCatogorySystem())
                {
                    if (gvProjectCagory.RowCount != 0)
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int ProjectCatogoryId = this.UtilityMember.NumberSet.ToInteger(gvProjectCagory.GetFocusedRowCellValue(col_Project_Catogory_Id).ToString());
                            resultArgs = projectCatogorySystem.DeleteProjectCatogoryDetails(ProjectCatogoryId);
                            if (resultArgs.Success)
                            {
                               // this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Master.ProjectCatogory.PROJECT_CATOGORY_DELETE));
                                GetProjectCatogory();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            finally { }
        }

        /// <summary>
        /// To Close the Add Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Validate the Catogory Name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvProjectCagory_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if (gvProjectCagory.FocusedColumn == col_Project_Catogory_Name)
            {
                string projectCatogoryName = gvProjectCagory.GetRowCellValue(e.RowHandle, col_Project_Catogory_Name).ToString();
                if (string.IsNullOrEmpty(projectCatogoryName.Trim()))
                {
                    e.Valid = false;
                    return;
                }
            }
        }

        /// <summary>
        /// Checking the Exists Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvProjectCagory_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            using (ProjectCatogorySystem projectCatogory = new ProjectCatogorySystem())
            {
                if (e.Exception.Message.Contains(projectCatogory.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn.ColumnName))
                {
                    e.ErrorText = this.GetMessage(MessageCatalog.Master.ProjectCatogory.PROJECT_CATOGORY_AVAILABLE);
                }
                else
                {
                    e.ErrorText = this.GetMessage(MessageCatalog.Master.ProjectCatogory.PROJECT_CATOGORY_EMPTY);
                }
                gvProjectCagory.FocusedColumn = col_Project_Catogory_Name;
                e.WindowCaption = this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// To get Project Catogory Details
        /// </summary>
        private void GetProjectCatogory()
        {
            try
            {
                using (ProjectCatogorySystem projectCatogorySystem = new ProjectCatogorySystem())
                {
                    resultArgs = projectCatogorySystem.FetchProjectCatogoryDetails();
                    if (resultArgs.Success)
                    {
                        resultArgs.DataSource.Table.Columns[projectCatogorySystem.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn.ColumnName].Unique = true;
                        gcProjectCatogory.DataSource = resultArgs.DataSource.Table;
                        gcProjectCatogory.RefreshDataSource();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            finally { }
        }
        #endregion

    
    }
}
