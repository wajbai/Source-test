using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.DAO.Schema;
using Bosco.DAO.Data;
namespace Bosco.Model.UIModel
{
    public class ProjectCatogorySystem : SystemBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public ProjectCatogorySystem()
        {
        }
        public ProjectCatogorySystem(int ProjectCatogoryId)
        {
            FillProjectCatogoryDetails(ProjectCatogoryId);
        }
        #endregion

        #region ProjectCatogoryProperties
        public int ProjectCatogoryId { get; set; }
        public string ProjectCatogoryName { get; set; }

        // -----------
        public int ProjectCategoryGroupId { get; set; }
        public string ProjectCategoryITRGroup { get; set; }
        public int ProjectCategoryITRGroupId { get; set; }
        #endregion

        #region Methods
        public ResultArgs FetchProjectCatogoryDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ProjectCatogory.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteProjectCatogoryDetails(int ProjectCatogoryId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ProjectCatogory.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_IDColumn.ColumnName, ProjectCatogoryId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveProjectCatogoryDetails()
        {
            using (DataManager dataManager = new DataManager((ProjectCatogoryId == 0) ? SQLCommand.ProjectCatogory.Add : SQLCommand.ProjectCatogory.Update))
            {
                dataManager.Parameters.Add(this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_IDColumn, ProjectCatogoryId, true);
                dataManager.Parameters.Add(this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn, ProjectCatogoryName);
                dataManager.Parameters.Add(this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_ITRGROUP_IDColumn, ProjectCategoryITRGroupId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FillProjectCatogoryDetails(int ProjectCategoryId)
        {
            resultArgs = ProjectCatogoryDetailsById(ProjectCategoryId);
            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                ProjectCatogoryName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn.ColumnName].ToString();
            }
            return resultArgs;
        }

        public ResultArgs ProjectCatogoryDetailsById(int ProjectCatogoryId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ProjectCatogory.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_IDColumn.ColumnName, ProjectCatogoryId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion
    }
}
