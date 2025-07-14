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
    public class TransferVoucherSystem : SystemBase
    {
        #region Variable Declearatio
        ResultArgs resultArgs = null;
        #endregion

        #region Properties
        public int TransferId { get; set; }
        public int ItemId { get; set; }
        public int ToLocationId { get; set; }
        public int FromLocationId { get; set; }
        public DateTime TransfetrDate { get; set; }
        public int GroupId { get; set; }
        public Decimal Amount { get; set; }
        public string AssetID { get; set; }
        public string Narration { get; set; }
        public string NameAddress { get; set; }
        public int quantity { get; set; }
        public int RefrenceId { get; set; }
        public int branchId { get; set; }
        public DataTable dtTransferDetail { get; set; }
        public DataTable dtTransferedDetail { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        #endregion

        #region Constructor
        public TransferVoucherSystem()
        {

        }

        public TransferVoucherSystem(int TransferId)
        {
            //this.TransferId = TransferId;
            //FillTransferProperties();
        }
        #endregion

        #region Methods

        public ResultArgs SaveAssetTransfer()
        {
            try
            {
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.BeginTransaction();
                    resultArgs = SaveAssetTransferMaster();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        TransferId = TransferId == 0 ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : TransferId;
                        resultArgs = SaveToAssetTransferDetails();
                    }
                    dataManager.EndTransaction();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Source + Environment.NewLine + ex.Message);
            }
            finally
            { }
            return resultArgs;
        }

        private ResultArgs SaveAssetTransferMaster()
        {
            try
            {
                using (DataManager datamanager = new DataManager(SQLCommand.AssetTransferVoucher.MasterAdd))
                {
                    datamanager.Parameters.Add(this.AppSchema.AssetTransferDetails.TRANSFER_IDColumn, TransferId, true);
                    datamanager.Parameters.Add(this.AppSchema.AssetTransferDetails.LOCATION_FROM_IDColumn, FromLocationId);
                    datamanager.Parameters.Add(this.AppSchema.AssetTransferDetails.TRANSFER_DATEColumn, TransfetrDate);
                    datamanager.Parameters.Add(this.AppSchema.AssetTransferDetails.REFRENCE_IDColumn, RefrenceId);
                    datamanager.Parameters.Add(this.AppSchema.AssetTransferDetails.NARRATIONColumn, Narration);
                    datamanager.Parameters.Add(this.AppSchema.AssetTransferDetails.NAME_ADDRESSColumn, NameAddress);
                    datamanager.Parameters.Add(this.AppSchema.AssetTransferDetails.LOCATION_TO_IDColumn, ToLocationId);
                    datamanager.Parameters.Add(this.AppSchema.AssetTransferDetails.ITEM_IDColumn, ItemId);
                    resultArgs = datamanager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Source + Environment.NewLine + ex.Message);
            }
            finally
            { }
            return resultArgs;
        }

        private ResultArgs SaveToAssetTransferDetails()
        {
            try
            {
                foreach (DataRow drTransfer in dtTransferDetail.Rows)
                {
                    using (DataManager Datamanager = new DataManager(SQLCommand.AssetTransferVoucher.DetailAdd))
                    {
                        Datamanager.Parameters.Add(this.AppSchema.AssetTransferDetails.ITEM_IDColumn, ItemId);
                        Datamanager.Parameters.Add(this.AppSchema.AssetTransferDetails.TRANSFER_IDColumn, resultArgs.RowUniqueId);
                        Datamanager.Parameters.Add(this.AppSchema.AssetTransferDetails.ASSET_IDColumn, (drTransfer[this.AppSchema.AssetTransferDetails.ASSET_IDColumn.ColumnName] != null) ? drTransfer[this.AppSchema.AssetTransferDetails.ASSET_IDColumn.ColumnName].ToString() : string.Empty);
                        Datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                        resultArgs = Datamanager.UpdateData();
                        AssetID = (drTransfer[this.AppSchema.AssetTransferDetails.ASSET_IDColumn.ColumnName] != null) ? drTransfer[this.AppSchema.AssetTransferDetails.ASSET_IDColumn.ColumnName].ToString() : string.Empty;
                        SaveToAssetItemLocation();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally
            { }
            return resultArgs;
        }

        private ResultArgs SaveFromAssetTransferDetails()
        {
            try
            {
                foreach (DataRow drTransfer in dtTransferedDetail.Rows)
                {
                    using (DataManager Datamanager = new DataManager(SQLCommand.AssetTransferVoucher.DetailAdd))
                    {
                        Datamanager.Parameters.Add(this.AppSchema.AssetTransferDetails.ITEM_IDColumn, ItemId);
                        Datamanager.Parameters.Add(this.AppSchema.AssetTransferDetails.TRANSFER_IDColumn, resultArgs.RowUniqueId);
                        Datamanager.Parameters.Add(this.AppSchema.AssetTransferDetails.ASSET_IDColumn, (drTransfer[this.AppSchema.AssetTransferDetails.ASSET_IDColumn.ColumnName] != null) ? drTransfer[this.AppSchema.AssetTransferDetails.ASSET_IDColumn.ColumnName].ToString() : string.Empty);
                        Datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                        resultArgs = Datamanager.UpdateData();
                        AssetID = (drTransfer[this.AppSchema.AssetTransferDetails.ASSET_IDColumn.ColumnName] != null) ? drTransfer[this.AppSchema.AssetTransferDetails.ASSET_IDColumn.ColumnName].ToString() : string.Empty;
                        SaveFromAssetItemLocation();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally
            { }
            return resultArgs;
        }

        private ResultArgs SaveToAssetItemLocation()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.UpdateAssetItemLocationById))
                {
                    dataManager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATION_IDColumn, ToLocationId);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.ASSET_IDColumn, this.AssetID);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_IDColumn, ItemId);
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally
            { }
            return resultArgs;
        }

        private ResultArgs SaveFromAssetItemLocation()
        {
            try
            {
                using (DataManager datamanager = new DataManager(SQLCommand.AssetItem.UpdateAssetItemLocationById))
                {
                    datamanager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATION_IDColumn, FromLocationId);
                    datamanager.Parameters.Add(this.AppSchema.ASSETItem.ASSET_IDColumn, this.AssetID);
                    datamanager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_IDColumn, ItemId);
                    resultArgs = datamanager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally
            { }
            return resultArgs;
        }

        public DataSet FetchAllTransferDetails()
        {
            DataSet dsAssetTransfer = new DataSet();
            string TransferId = string.Empty;
            try
            {
                    resultArgs = FetchMasterDetails();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null
                       && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        resultArgs.DataSource.Table.TableName = "Master";
                        dsAssetTransfer.Tables.Add(resultArgs.DataSource.Table);
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            TransferId += dr[this.AppSchema.AssetTransferDetails.TRANSFER_IDColumn.ColumnName].ToString() + ",";
                        }
                       this.TransferId =this.NumberSet.ToInteger(TransferId.Trim(','));
                        resultArgs = FetchSubViewTransferDetails(TransferId);
                        if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            resultArgs.DataSource.Table.TableName = "Transfer";
                            dsAssetTransfer.Tables.Add(resultArgs.DataSource.Table);
                        }
                        dsAssetTransfer.Relations.Add(dsAssetTransfer.Tables[1].TableName, dsAssetTransfer.Tables[0].Columns[this.AppSchema.AssetTransferDetails.TRANSFER_IDColumn.ColumnName], dsAssetTransfer.Tables[1].Columns[this.AppSchema.AssetTransferDetails.TRANSFER_IDColumn.ColumnName]);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally
            { }
            return dsAssetTransfer;
        }

        public ResultArgs FetchSubViewTransferDetails(string TransferId)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetTransferVoucher.FetchAllDetail))
            {
                datamanager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, FromDate);
                datamanager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, ToDate);
                datamanager.Parameters.Add(this.AppSchema.AssetTransferDetails.TRANSFER_IDColumn, TransferId);
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs EditSubViewTransferDetails(string transferId)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetTransferVoucher.FetchDetails))
            {
                datamanager.Parameters.Add(this.AppSchema.AssetTransferDetails.TRANSFER_IDColumn, transferId);
                datamanager.Parameters.Add(this.AppSchema.AssetTransferDetails.ITEM_IDColumn, ItemId);
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchTransferDetailsById()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetTransferVoucher.Fetch))
            {
                datamanager.Parameters.Add(this.AppSchema.AssetTransferDetails.TRANSFER_IDColumn, this.TransferId);
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteTranferDetails()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetTransferVoucher.Delete))
            {
                datamanager.Parameters.Add(this.AppSchema.AssetTransferDetails.TRANSFER_IDColumn, this.TransferId);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FillTransferProperties()
        {
            try
            {
                resultArgs = FetchTransferDetailsById();
                if (resultArgs != null && resultArgs.Success)
                {
                    ItemId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetTransferDetails.ITEM_IDColumn.ColumnName].ToString());
                    ToLocationId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetTransferDetails.LOCATION_TO_IDColumn.ColumnName].ToString());
                    FromLocationId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetTransferDetails.LOCATION_FROM_IDColumn.ColumnName].ToString());
                    RefrenceId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetTransferDetails.REFRENCE_IDColumn.ColumnName].ToString());
                    TransfetrDate = this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetTransferDetails.TRANSFER_DATEColumn.ColumnName].ToString(), false);
                    GroupId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETClassDetails.ASSET_CLASSColumn.ColumnName].ToString());
                    Narration = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetTransferDetails.NARRATIONColumn.ColumnName].ToString();
                    //AssetID = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetTransferDetails.ASSET_IDColumn.ColumnName].ToString();
                    Narration = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetTransferDetails.NARRATIONColumn.ColumnName].ToString();
                    NameAddress = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetTransferDetails.NAME_ADDRESSColumn.ColumnName].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Source + Environment.NewLine + ex.Message);
            }
            return resultArgs;
        }

        public ResultArgs fetchAssetDetailsById()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetTransferVoucher.FetchAssetDetails))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETItem.ASSET_CLASS_IDColumn, this.GroupId);
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs fetchAssetGroupDetails()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetTransferVoucher.FetchAssetGroup))
            {
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs FetchMasterDetails()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetTransferVoucher.FetchAllMaster))
                {
                    datamanager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, FromDate);
                    datamanager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, ToDate);
                    datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs= datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion
    }
}
