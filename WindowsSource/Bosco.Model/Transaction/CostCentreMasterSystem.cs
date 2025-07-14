using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.DAO.Schema;
using Bosco.DAO.Data;

namespace Bosco.Model.Transaction
{
    public class CostCentreMasterSystem :SystemBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public CostCentreMasterSystem()
        {

        }
        #endregion

        #region CostCentreProperty
        public int VoucherId { get; set; }
        public int LedgerId { get; set; }
        public int CostCenterId { get; set; }
        public decimal Amount { get; set; }
        #endregion

        public ResultArgs FetchVoucherCostCentre()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.MasterTransactionCostCentre.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataView);
            }
            return resultArgs;
        }

      

    }
}
