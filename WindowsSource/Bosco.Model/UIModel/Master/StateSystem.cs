using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;

namespace Bosco.Model.UIModel.Master
{
    public class StateSystem : SystemBase
    {
        #region Variable Decelaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public StateSystem()
        {
        }

        public StateSystem(int StateId)
        {
            FillStateProperties(StateId);
        }
        #endregion

        #region State Properties
        public int StateId { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public int CountryId { get; set; }
        #endregion

        #region Methods

        public ResultArgs FetchStateDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.State.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteStateDetails(int StateId)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.State.Delete))
            {
                dataMember.Parameters.Add(this.AppSchema.State.STATE_IDColumn, StateId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveStateDetails()
        {
            using (DataManager dataManager = new DataManager((StateId == 0) ? SQLCommand.State.Add : SQLCommand.State.Update))
            {
                dataManager.Parameters.Add(this.AppSchema.State.STATE_IDColumn, StateId,true);
                dataManager.Parameters.Add(this.AppSchema.State.STATE_CODEColumn, StateCode);
                dataManager.Parameters.Add(this.AppSchema.State.STATE_NAMEColumn, StateName);
                dataManager.Parameters.Add(this.AppSchema.State.COUNTRY_IDColumn, CountryId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public void FillStateProperties(int SCID)
        {
            resultArgs = FetchStateById(SCID);
            if (resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                StateName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.State.STATE_NAMEColumn.ColumnName].ToString();
                StateCode= resultArgs.DataSource.Table.Rows[0][this.AppSchema.State.STATE_CODEColumn.ColumnName].ToString();
                CountryId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.State.COUNTRY_IDColumn.ColumnName].ToString());
            }
        }

        private ResultArgs FetchStateById(int SId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.State.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.State.STATE_IDColumn, SId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchStateByStateName(string StateName)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.State.FetchStateByStateName))
            {
                dataManager.Parameters.Add(this.AppSchema.State.STATE_NAMEColumn, StateName);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs GetStateId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.State.GetStateId))
            {
                dataManager.Parameters.Add(this.AppSchema.State.STATE_NAMEColumn, StateName);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        public ResultArgs FetchStateListDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.State.FetchState))
            {
                if (CountryId != 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.State.COUNTRY_IDColumn,CountryId);
                }
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion

    }
}
