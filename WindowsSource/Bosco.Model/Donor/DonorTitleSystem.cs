using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;

namespace Bosco.Model.Donor
{
    public class DonorTitleSystem:SystemBase
    {
        #region Variable Decelaration
        ResultArgs resultArgs = null;
        #endregion

        #region Properties

        public int TitleId { get; set; }
        public string DonorTitle { get; set; }

        #endregion

        #region Constructor
        public DonorTitleSystem()
        {
        }

        public DonorTitleSystem(int TitleId)
            :this()
        {
            this.TitleId = TitleId;
            FillDonorTitleProperties();
        }
        #endregion

        #region Methods

        public ResultArgs FetchDonorTitleDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DonorTitle.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteDonorTitle()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.DonorTitle.Delete))
            {
                dataMember.Parameters.Add(this.AppSchema.DonorTitle.TITLE_IDColumn,TitleId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public void FillDonorTitleProperties()
        {
            resultArgs = FetchDonorTitleById();
            if (resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                DonorTitle = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorTitle.TITLEColumn.ColumnName].ToString();
            }
        }

        private ResultArgs FetchDonorTitleById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DonorTitle.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorTitle.TITLE_IDColumn, TitleId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveDonorTitleDetails()
        {
            using (DataManager dataManager = new DataManager((TitleId == 0) ? SQLCommand.DonorTitle.Add : SQLCommand.DonorTitle.Update))
            {
                 dataManager.Parameters.Add(this.AppSchema.DonorTitle.TITLE_IDColumn,TitleId,true);
                 dataManager.Parameters.Add(this.AppSchema.DonorTitle.TITLEColumn,DonorTitle);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        #endregion
    }
}
