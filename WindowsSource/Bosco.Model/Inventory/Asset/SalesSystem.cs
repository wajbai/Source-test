using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using System.Data;

namespace Bosco.Model.Inventory.Asset
{
   public  class SalesSystem:SystemBase
   {
       #region Constructor
        public SalesSystem(int salesId)
        {
            this.SalesId = salesId;
            AssignToSalesVoucherPoroperties();
        }
        public SalesSystem()
        {
        }
       #endregion

       #region Variable Declearation
        ResultArgs resultArgs = null;
        # endregion

        #region Properties
        public int SalesId { get; set; }
        public int GroupId { get; set; }
        public int LocationId { get; set; }
        public int AssetId { get; set; }
        public int Quantity { get; set; }
        public Decimal Rate { get; set; }
        public Decimal Amount { get; set; }
        public Decimal Discount { get; set; }
        public DataTable dtVoucherEdit { get; set; }
        #endregion

       #region Method
        public ResultArgs SaveSalesVoucherDetails()
        {
            using (DataManager datamanager = new DataManager((this.SalesId == 0) ? SQLCommand.AssetSalesVoucher.Add : SQLCommand.AssetSalesVoucher.Update))
            {
                datamanager.Parameters.Add(this.AppSchema.SalesAsset.SALES_IDColumn,SalesId);
                datamanager.Parameters.Add(this.AppSchema.SalesAsset.ASSET_GROUP_IDColumn, GroupId);
                datamanager.Parameters.Add(this.AppSchema.SalesAsset.ASSET_IDColumn, AssetId);
                datamanager.Parameters.Add(this.AppSchema.SalesAsset.ASSET_LOCATION_IDColumn, LocationId);
                datamanager.Parameters.Add(this.AppSchema.SalesAsset.QUANTITYColumn,Quantity);
                datamanager.Parameters.Add(this.AppSchema.SalesAsset.RATEColumn, Rate);
                datamanager.Parameters.Add(this.AppSchema.SalesAsset.PERCENTAGEColumn, Discount);
                datamanager.Parameters.Add(this.AppSchema.SalesAsset.AMOUNTColumn, Amount);
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }
        public ResultArgs FetchallSalesVoucherDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetSalesVoucher.FetchAll))
            {
                dataManager.Parameters.Add(this.AppSchema.SalesAsset.SALES_IDColumn,SalesId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs DeleteSalesVoucherDetails(int SalesId)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetSalesVoucher.Delete))
            {
                datamanager.Parameters.Add(this.AppSchema.SalesAsset.SALES_IDColumn, SalesId);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }
        private ResultArgs FetchSalesVoucherDetailsById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetSalesVoucher.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.SalesAsset.SALES_IDColumn, this.SalesId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public void AssignToSalesVoucherPoroperties()
        {
            resultArgs = FetchSalesVoucherDetailsById();
            if (resultArgs != null)
            {
                if (resultArgs.DataSource.Table != null)
                {
                    dtVoucherEdit = resultArgs.DataSource.Table;
                }
            }
        }

       #endregion
   }
}
