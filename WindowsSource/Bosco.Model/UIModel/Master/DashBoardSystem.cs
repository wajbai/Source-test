using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Utility;
using System.Data;

namespace Bosco.Model.UIModel.Master
{
    public class DashBoardSystem : SystemBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        CommonMember UtilityMember = new CommonMember();
        #endregion

        #region Constructor

        #endregion

        #region Properties
        /// <summary>
        /// On 28/06/2018, This property is used to skip projects which is closed on or equal to this date
        /// </summary>
        public string ProjectClosedDate { get; set; }
        #endregion

        #region Methods
        public ResultArgs FetchProjects()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchProjects))
            {
                if (!string.IsNullOrEmpty(ProjectClosedDate))
                {
                  dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, ProjectClosedDate);
                }

                if (!IsFullRightsReservedUser)
                {
                    dataManager.Parameters.Add(this.AppSchema.UserRole.USERROLE_IDColumn, this.LoginUserRoleId);
                }
                
                //On 09/02/2022, load Projects based on start of the project (Current FY To)
                if (!string.IsNullOrEmpty(YearTo) && DateSet.ToDate(YearTo, false) != DateTime.MinValue)
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, YearTo);
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchFD(int pid, DateTime dtVoucherdate)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DashBoard.FetchMaturedFD))
            {
                dataManager.Parameters.Add(this.AppSchema.DashBoard.PROJECT_IDColumn, pid);
                dataManager.Parameters.Add(this.AppSchema.DashBoard.RECENTVOUCHERColumn, dtVoucherdate);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchCharts(int ProjectID, DateTime dtYearFrom, DateTime dtYearTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DashBoard.FetchChartInfo))
            {
                dataManager.Parameters.Add(this.AppSchema.DashBoard.PROJECT_IDColumn, ProjectID);
                dataManager.Parameters.Add(this.AppSchema.DashBoard.DATE_FROMColumn, UtilityMember.DateSet.GetMySQLDateTime(dtYearFrom.ToString(), DateDataType.DateNoFormatBegin));
                dataManager.Parameters.Add(this.AppSchema.DashBoard.DATE_TOColumn, UtilityMember.DateSet.GetMySQLDateTime(dtYearTo.ToString(), DateDataType.DateNoFormatEnd));
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchDatabase()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DashBoard.FetchDatabases))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs UpdatePayrollSymbols()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DashBoard.UpdatePayrollSymbols))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs CheckDatabase(string Dbname)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DashBoard.CheckDatabaseExists))
            {
                dataManager.Parameters.Add(this.AppSchema.DashBoard.DATABASEColumn, Dbname);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs DropDatabase(string Dbname)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DashBoard.DropDatabase))
            {
                dataManager.Parameters.Add(this.AppSchema.DashBoard.DRPDATABASEColumn, Dbname);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        public ResultArgs InsertRestoredDatabase(string Dbname)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DashBoard.InsertRestoredDatabase))
            {
                dataManager.Parameters.Add(this.AppSchema.DashBoard.DATABASEColumn, Dbname);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public string FetchProjectsbySociety(int projectId)
        {
            string projectID = string.Empty;
            using (DataManager dataManager = new DataManager(SQLCommand.DashBoard.FetchProjectsbySociety))
            {
                dataManager.Parameters.Add(this.AppSchema.DashBoard.PROJECT_IDColumn, projectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    foreach (DataRow dritem in resultArgs.DataSource.Table.Rows)
                    {
                        projectID += dritem[this.AppSchema.DashBoard.PROJECT_IDColumn.ColumnName].ToString()+ ',';
                    }
                }
            }
            return projectID.TrimEnd(',');
        }

        public string FetchAllProjectId()
        {
            string projectID = string.Empty;
            using (MappingSystem mappingSystem = new MappingSystem())
            {
                resultArgs = mappingSystem.FetchPJLookup();
                if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    foreach (DataRow dritem in resultArgs.DataSource.Table.Rows)
                    {
                        projectID += dritem[this.AppSchema.DashBoard.PROJECT_IDColumn.ColumnName].ToString() + ',';
                    }
                }
            }
            return projectID.TrimEnd(',');
        }

        #endregion
    }
}
