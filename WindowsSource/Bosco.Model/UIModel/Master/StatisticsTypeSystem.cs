using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.Model.UIModel.Master
{
    public class StatisticsTypeSystem : SystemBase
    {
          #region VariableDeclaration
        ResultArgs resultArgs = null;
        public int ProjectId { get; set; }
        #endregion

        #region Constructor
        public StatisticsTypeSystem()
        {
        }
        public StatisticsTypeSystem(int StatisticsTypeId)
        {
            FillStatisticsTypeDetails(StatisticsTypeId);
        }
        #endregion

        #region Property
        public int StatisticsTypeId { get; set; }
        public string StatisticsType { get; set; }
        #endregion

        #region Methods

        public ResultArgs FetchStatisticsTypeAll()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StatisticsType.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteStatisticsType(int StatisticsTypeId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StatisticsType.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.StatisticsType.STATISTICS_TYPE_IDColumn, StatisticsTypeId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveStatisticsTypeDetails()
        {
            using (DataManager dataManager = new DataManager((StatisticsTypeId == 0) ? SQLCommand.StatisticsType.Add : SQLCommand.StatisticsType.Update))
            {
                dataManager.Parameters.Add(this.AppSchema.StatisticsType.STATISTICS_TYPE_IDColumn, StatisticsTypeId);
                dataManager.Parameters.Add(this.AppSchema.StatisticsType.STATISTICS_TYPEColumn, StatisticsType);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FillStatisticsTypeDetails(int StatisticsTypeId)
        {
            resultArgs = StatisticsTypeDetailsbyId(StatisticsTypeId);
            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                StatisticsType = resultArgs.DataSource.Table.Rows[0][this.AppSchema.StatisticsType.STATISTICS_TYPEColumn.ColumnName].ToString();
            }
            return resultArgs;
        }

        public ResultArgs StatisticsTypeDetailsbyId(int StatisticsTypeId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StatisticsType.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.StatisticsType.STATISTICS_TYPE_IDColumn, StatisticsTypeId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion
    }
}
