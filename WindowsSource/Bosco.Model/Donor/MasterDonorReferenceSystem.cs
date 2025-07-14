using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.Model.Donor
{
    public class MasterDonorReferenceSystem : SystemBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public MasterDonorReferenceSystem()
        {
        }
        public MasterDonorReferenceSystem(int ReferedStaffId)
        {
            FillReferedStaffProperties(ReferedStaffId);
        }
        #endregion

        #region MasterRefferedStaff Properties

        public int RefferedStaffId { get; set; }
        public string RefferedStaffName { get; set; }

        #endregion

        #region Methods

        public ResultArgs FetchReferedStaffDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.MasterDonorReference.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private void FillReferedStaffProperties(int refferedStaffId)
        {
            resultArgs = FetchReferedStaffDetailsById(refferedStaffId);

            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                RefferedStaffName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorReference.REFERED_STAFF_NAMEColumn.ColumnName].ToString();
                RefferedStaffId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorReference.REFERED_STAFF_IDColumn.ColumnName].ToString());
            }
        }

        private ResultArgs FetchReferedStaffDetailsById(int RefferedStaffId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.MasterDonorReference.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorReference.REFERED_STAFF_IDColumn, RefferedStaffId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveReferedStaffDetails()
        {
            using (DataManager dataManager = new DataManager((RefferedStaffId == 0) ? SQLCommand.MasterDonorReference.Add : SQLCommand.MasterDonorReference.Update))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorReference.REFERED_STAFF_IDColumn, RefferedStaffId, true);
                dataManager.Parameters.Add(this.AppSchema.DonorReference.REFERED_STAFF_NAMEColumn, RefferedStaffName);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteReferredStaffDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.MasterDonorReference.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorReference.REFERED_STAFF_IDColumn, RefferedStaffId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        #endregion

    }
}
