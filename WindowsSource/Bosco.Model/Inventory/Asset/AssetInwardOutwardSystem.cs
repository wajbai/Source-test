using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using System.Data;
using Bosco.Utility;
using Bosco.Model.Transaction;
using Bosco.Utility.ConfigSetting;
using Bosco.Model.UIModel;

namespace Bosco.Model
{
    public class AssetInwardOutwardSystem : SystemBase
    {
        #region Variable Declaration
        ResultArgs resultArgs = new ResultArgs();
        public static string SELECT = "SELECT";
        public DataTable dtAssetDetails = new DataTable();
        public DataTable dtInsuranceDetails = new DataTable();
        public bool isEdit = false;

        public DataTable dtExcelDataSource = new DataTable();

        public int InsItemId { get; set; }
        public string InsAssetId { get; set; }

        #endregion

        #region Constructor
        public AssetInwardOutwardSystem()
        {
        }
        #endregion

        #region Properties
        public int InoutId { get; set; }
        public string InoutIdCollections { get; set; }
        public DateTime InOutDate { get; set; }
        public string BalanceOpDate { get; set; }
        public string BillInvoiceNo { get; set; }
        public int VendorId { get; set; }
        public int DonorId { get; set; }
        public string SoldTo { get; set; }
        public int ProjectId { get; set; }
        public string AssetClassId { get; set; }
        public string ProjectIds { get; set; }
        public int LocationId { get; set; }
        public int VoucherId { get; set; }
        public string VoucherNo { get; set; }
        public double TotalAmount { get; set; }
        public double DepreciationAmount { get; set; }
        public double TotalDepreciationAmount { get; set; }

        public string Flag { get; set; }
        public int Status { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public double Amount { get; set; }
        public double SalvageValue { get; set; }
        public int CashLedgerId { get; set; }
        public DataTable dtinoutword = new DataTable();
        public DataTable dtAssetVoucher = new DataTable();
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int InoutDetailId { get; set; }
        public int ItemDetailId { get; set; }
        public int purpose { get; set; }

        public decimal AssetAmount { get; set; }
        public decimal DeprAmount { get; set; }
        public decimal GainAmount { get; set; }
        public decimal LossAmount { get; set; }

        public string InoutDetailIds { get; set; }
        public string ItemDetailIds { get; set; }
        public int ItemRowNo { get; set; }
        public int ItemDetailRowNo { get; set; }
        public string Narration { get; set; }
        public string NameAddress { get; set; }
        public DataTable dtCashBank { get; set; }

        public int AssetCurrencyCountryId { get; set; }
        public decimal AssetExchangeRate { get; set; }
        public decimal AssetCalcAmount { get; set; }
        public decimal AssetCurrencyAmount { get; set; }
        public decimal AssetActualAmount { get; set; }
        public int AssetExchageCountryId { get; set; }
        public decimal AssetLiveExchangeRate { get; set; }
        /// <summary>
        /// Bank / Cash Properties
        /// </summary>
        /// <returns></returns>
        public int LedgerId { get; set; }
        #endregion

        #region Asset InOut Master

        public ResultArgs FetchAssetInOutMasterById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.FetchAssetInOutMasterById))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.IN_OUT_IDColumn, InoutId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveAssetInOutMaster(DataManager dmanager)
        {
            using (DataManager dataManager = new DataManager((InoutId == 0) ? SQLCommand.AssetInOut.SaveAssetInOutMaster : SQLCommand.AssetInOut.UpdateAssetInOutMaster))
            {
                dataManager.Database = dmanager.Database;
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.IN_OUT_IDColumn, InoutId, true);
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.IN_OUT_DATEColumn, InOutDate);
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.BILL_INVOICE_NOColumn, !string.IsNullOrEmpty(BillInvoiceNo) ? BillInvoiceNo : string.Empty);
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.VENDOR_IDColumn, VendorId);
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.DONOR_IDColumn, DonorId);
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.SOLD_TOColumn, !string.IsNullOrEmpty(SoldTo) ? SoldTo : string.Empty);
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.VOUCHER_IDColumn, VoucherId);
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.TOT_DEP_AMOUNTColumn, TotalDepreciationAmount);
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.TOT_AMOUNTColumn, TotalAmount);
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.FLAGColumn, Flag.ToString());
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteAssetInOutMaster()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.DeleteAssetInOutMaster))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.IN_OUT_IDColumn, InoutId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        public ResultArgs DeleteAssetInOutMasterDetailCollection()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.DeleteAssetInOutMasterDetailIds))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.IN_OUT_IDsColumn, InoutIdCollections);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs FetchInOutMastersByFlag()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetInOut.FetchAssetInOutMasterByFlag))
            {
                datamanager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, ProjectId);
                datamanager.Parameters.Add(this.AppSchema.AssetInOut.FLAGColumn, Flag);
                datamanager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                datamanager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        #endregion

        #region Asset InOut Details

        private ResultArgs FetchAssetInOutDetailIdByInOutId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.FetchAssetInOutDetailIdByInOutId))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.IN_OUT_IDColumn, InoutId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }

        public ResultArgs FetchAssetInOutDetailById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.FetchAssetInOutDetailById))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.IN_OUT_IDColumn, InoutId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public DataSet FetchAssetInOutDetailsByFlag()
        {
            DataSet dsAssetInOut = new DataSet();
            string InOutId = string.Empty;
            string ItemId = string.Empty;
            try
            {
                resultArgs = FetchInOutMastersByFlag();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null
                    && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    resultArgs.DataSource.Table.TableName = "Master";
                    dsAssetInOut.Tables.Add(resultArgs.DataSource.Table);

                    foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                    {
                        InOutId += dr[this.AppSchema.AssetInOut.IN_OUT_IDColumn.ColumnName].ToString() + ",";
                    }
                    InOutId = InOutId.TrimEnd(',');

                    resultArgs = FetchInOutDetailsByFlag(InOutId);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        resultArgs.DataSource.Table.TableName = "Asset Items";
                        dsAssetInOut.Tables.Add(resultArgs.DataSource.Table);
                        dsAssetInOut.Relations.Add(dsAssetInOut.Tables[1].TableName, dsAssetInOut.Tables[0].Columns[this.AppSchema.AssetInOut.IN_OUT_IDColumn.ColumnName], dsAssetInOut.Tables[1].Columns[this.AppSchema.AssetInOut.IN_OUT_IDColumn.ColumnName]);
                    }
                    //resultArgs = FetchAssetIdDetailsByFlag(Flag, this.Status);
                    //if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    //{
                    //    resultArgs.DataSource.Table.TableName = "Asset ID";
                    //    dsAssetInOut.Tables.Add(resultArgs.DataSource.Table);
                    //    dsAssetInOut.Relations.Add(dsAssetInOut.Tables[2].TableName, dsAssetInOut.Tables[1].Columns[this.AppSchema.AssetInOut.IN_OUT_DETAIL_IDColumn.ColumnName], dsAssetInOut.Tables[2].Columns[this.AppSchema.AssetInOut.IN_OUT_DETAIL_IDColumn.ColumnName]);
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
            return dsAssetInOut;
        }

        public ResultArgs FetchInOutDetailsByFlag(string AssInoutId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.FetchAssetInOutDetailByFlag))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, this.ProjectId);
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.FLAGColumn, this.Flag);
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, this.DateFrom);
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, this.DateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAssetIdDetailsByFlag(string flag, int status)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.FetchAssetIDDetailByFlag))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, this.ProjectId);
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.FLAGColumn, this.Flag);
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.STATUSColumn, status);
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, this.DateFrom);
                dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, this.DateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveAssetInOutDetail(DataManager Dmanager)
        {
            ItemRowNo = 0;
            if (AssetListCollection != null && AssetListCollection.Count > 0)
            {
                // by alex. One row is added extra.
                if (dtinoutword.Rows.Count > 0 && dtinoutword.Columns.Contains("ITEM_ID"))
                {
                    IEnumerable<DataRow> EnumurableInoutword = dtinoutword.Rows.Cast<DataRow>().Where(row =>
                        row.RowState != DataRowState.Deleted ?
                        string.IsNullOrEmpty(row["ITEM_ID"].ToString()) ? false : true : false);
                    if (EnumurableInoutword.Count() > 0)
                    {
                        dtinoutword = EnumurableInoutword.CopyToDataTable();
                    }
                }

                foreach (DataRow drInwardOutward in dtinoutword.Rows)
                {
                    InoutDetailId = (drInwardOutward[this.AppSchema.AssetInOut.IN_OUT_DETAIL_IDColumn.ColumnName] != null) ?
                            NumberSet.ToInteger(drInwardOutward[this.AppSchema.AssetInOut.IN_OUT_DETAIL_IDColumn.ColumnName].ToString()) : 0;

                    if (dtinoutword.Columns.Equals(this.AppSchema.AssetPurchaseDetail.LOCATION_IDColumn.ColumnName.ToString()))
                        LocationId = (drInwardOutward[this.AppSchema.AssetPurchaseDetail.LOCATION_IDColumn.ColumnName] != null) ?
                                    this.NumberSet.ToInteger(drInwardOutward[this.AppSchema.AssetPurchaseDetail.LOCATION_IDColumn.ColumnName].ToString()) : 0;

                    ItemId = (drInwardOutward[this.AppSchema.AssetInOut.ITEM_IDColumn.ColumnName] != null) ?
                                this.NumberSet.ToInteger(drInwardOutward[this.AppSchema.AssetInOut.ITEM_IDColumn.ColumnName].ToString()) : 0;

                    Quantity = (drInwardOutward[this.AppSchema.AssetInOut.QUANTITYColumn.ColumnName] != null) ?
                                 this.NumberSet.ToInteger(drInwardOutward[this.AppSchema.AssetInOut.QUANTITYColumn.ColumnName].ToString()) : 0;

                    Amount = (drInwardOutward[this.AppSchema.AssetInOut.AMOUNTColumn.ColumnName] != null) ?
                        this.NumberSet.ToDouble(drInwardOutward[this.AppSchema.AssetInOut.AMOUNTColumn.ColumnName].ToString()) : 0;

                    if (dtinoutword.Columns.Contains(this.AppSchema.AssetInOut.DEPRECIATION_AMOUNTColumn.ColumnName))

                        DepreciationAmount = (drInwardOutward[this.AppSchema.AssetInOut.DEPRECIATION_AMOUNTColumn.ColumnName] != null) ?
                        this.NumberSet.ToDouble(drInwardOutward[this.AppSchema.AssetInOut.DEPRECIATION_AMOUNTColumn.ColumnName].ToString()) : 0;
                    else
                        DepreciationAmount = 0;

                    //if (dtinoutword.Columns.Contains(this.AppSchema.AssetInOut.SALVAGE_VALUEColumn.ColumnName))
                    //    SalvageValue = (drInwardOutward[this.AppSchema.AssetInOut.SALVAGE_VALUEColumn.ColumnName] != null) ?
                    //                 this.NumberSet.ToDouble(drInwardOutward[this.AppSchema.AssetInOut.SALVAGE_VALUEColumn.ColumnName].ToString()) : 0;
                    // else
                    SalvageValue = 0;

                    //if (dtinoutword.Columns.Contains(this.AppSchema.AssetInOut.BALANCE_OP_DATEColumn.ColumnName))

                    if (dtinoutword.Columns.Contains(this.AppSchema.AssetInOut.BALANCE_OP_DATEColumn.ColumnName))
                        BalanceOpDate = (drInwardOutward[this.AppSchema.AssetInOut.BALANCE_OP_DATEColumn.ColumnName] != null) ?
                        drInwardOutward[this.AppSchema.AssetInOut.BALANCE_OP_DATEColumn.ColumnName].ToString() : string.Empty;
                    else
                        BalanceOpDate = string.Empty;

                    resultArgs = SaveInOutDetails(Dmanager);
                    if (resultArgs.Success)
                    {
                        if (InoutDetailId == 0)
                            InoutDetailId = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                        resultArgs = SaveFAItemDetails(Dmanager);
                        if (resultArgs.Success)
                            ItemRowNo++;
                        else
                            break;
                    }
                    else
                        break;
                }
            }
            return resultArgs;
        }

        private ResultArgs SaveInOutDetails(DataManager dmanager)
        {
            using (DataManager dataManager = new DataManager(InoutDetailId == 0 ? SQLCommand.AssetInOut.SaveAssetInOutDetail : SQLCommand.AssetInOut.UpdateAssetInOutDetail))
            {
                dataManager.Database = dmanager.Database;
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.IN_OUT_DETAIL_IDColumn, InoutDetailId, true);
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.IN_OUT_IDColumn, InoutId);
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.ITEM_IDColumn, ItemId);
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.QUANTITYColumn, Quantity);
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.DEPRECIATION_AMOUNTColumn, DepreciationAmount);
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.AMOUNTColumn, Amount);
                //dataManager.Parameters.Add(this.AppSchema.AssetInOut.SALVAGE_VALUEColumn, SalvageValue);
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.BALANCE_OP_DATEColumn, BalanceOpDate);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteAssetInOutDetail()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.DeleteAssetInOutDetail))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.IN_OUT_IDColumn, InoutId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteAssetInOutDetailByDetailId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.DeleteAssetInOutDetailbyId))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.IN_OUT_DETAIL_IDColumn, InoutDetailIds);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        #endregion

        #region Asset Trans

        private ResultArgs SaveAssetTrans(DataManager dmanger)
        {
            resultArgs = DeleteAssetTrans();
            if (resultArgs.Success)
                resultArgs = SaveFixedAssetTrans(dmanger);
            return resultArgs;
        }

        private ResultArgs DeleteAssetItemDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.DeleteAssetItemDetailById))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_DETAIL_IDsColumn, ItemDetailIds);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteAssetInsuranceDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.DeleteInsuranceDetailByItemDetailId))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_DETAIL_IDsColumn, ItemDetailIds);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteAssetTrans()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.DeleteAssetTrans))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.IN_OUT_DETAIL_IDColumn, InoutDetailId);
                dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_DETAIL_IDColumn, ItemDetailId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteAssetTransByInOutIds()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.DeleteAssetTransInOutDetailIds))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.IN_OUT_DETAIL_IDsColumn, InoutDetailIds);
                dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_DETAIL_IDsColumn, ItemDetailIds);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteAssetTransByInOutDetailID(string InoutDetailId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.DeleteAssetTransByInoutDetailId))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.IN_OUT_DETAIL_IDColumn, InoutDetailId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs SaveFixedAssetTrans(DataManager dmanager)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.SaveAssetTrans))
            {
                dataManager.Database = dmanager.Database;
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.IN_OUT_DETAIL_IDColumn, InoutDetailId);
                dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_DETAIL_IDColumn, ItemDetailId);
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.DEPRECIATION_AMOUNTColumn, DeprAmount);
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.AMOUNTColumn, AssetAmount);
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.GAIN_AMOUNTColumn, GainAmount);
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.LOSS_AMOUNTColumn, LossAmount);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs FetchAssetItemDetailIdByInOutDetailId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.FetchAssetItemDetailIdByInOutDetailId))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETItem.IN_OUT_DETAILS_IDsColumn, InoutDetailIds);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int CheckInsuranceByItemID(int InoutId, int ProjectId, int InoutDetailId, int ItemId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.CheckInsuranceByItemId))
            {
                if (InoutId > 0)
                    dataManager.Parameters.Add(this.AppSchema.AssetInOut.IN_OUT_IDColumn, InoutId);
                if (InoutDetailId > 0)
                    dataManager.Parameters.Add(this.AppSchema.AssetInOut.IN_OUT_DETAIL_IDColumn, InoutDetailId);
                dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_IDColumn, ItemId);
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.PROJECT_IDColumn, ProjectId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int CheckSoldAssetIdByItemID(int InoutId, int InoutDetailId, int ItemId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.CheckSoldAssetIdByItemID))
            {
                if (InoutId > 0)
                    dataManager.Parameters.Add(this.AppSchema.AssetInOut.IN_OUT_IDColumn, InoutId);
                dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_IDColumn, ItemId);
                if (InoutDetailId > 0)
                    dataManager.Parameters.Add(this.AppSchema.AssetInOut.IN_OUT_DETAIL_IDColumn, InoutDetailId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int CheckSoldAssetIdByItemDetailId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.CheckSoldAssetIdByItemDetailId))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_DETAIL_IDsColumn, ItemDetailIds);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        #endregion

        #region Asset Item Details

        private ResultArgs SaveFAItemDetails(DataManager dManager)
        {
            try
            {
                using (AssetItemSystem assetItemSystem = new AssetItemSystem())
                {
                    if (AssetListCollection.ContainsKey(ItemRowNo)) // Checking the Table exists in the Collections
                    {
                        dtAssetDetails = AssetListCollection[ItemRowNo];
                        if (dtAssetDetails != null && dtAssetDetails.Rows.Count > 0)
                        {
                            resultArgs = DeleteItemDetailByItemId(dtAssetDetails);
                            if (resultArgs.Success)
                            {
                                ItemDetailRowNo = 0;
                                foreach (DataRow drDetails in dtAssetDetails.Rows)
                                {
                                    ItemDetailId = assetItemSystem.ItemDetailId = (dtAssetDetails.Columns.Contains(this.AppSchema.ASSETItem.ITEM_DETAIL_IDColumn.ColumnName.ToString())
                                        ? NumberSet.ToInteger(drDetails[this.AppSchema.ASSETItem.ITEM_DETAIL_IDColumn.ColumnName].ToString()) : 0);
                                    assetItemSystem.ItemId = (dtAssetDetails.Columns.Contains(this.AppSchema.ASSETItem.ITEM_IDColumn.ColumnName.ToString())
                                        ? NumberSet.ToInteger(drDetails[this.AppSchema.ASSETItem.ITEM_IDColumn.ColumnName].ToString()) : 0);
                                    assetItemSystem.AssetID = (dtAssetDetails.Columns.Contains(this.AppSchema.ASSETItem.ASSET_IDColumn.ColumnName))
                                        ? drDetails[this.AppSchema.ASSETItem.ASSET_IDColumn.ColumnName].ToString() : string.Empty;

                                    assetItemSystem.DepreciationAmount = (dtAssetDetails.Columns.Contains("DEPRECIATION_AMOUNT"))
                                       ? NumberSet.ToDecimal(drDetails["DEPRECIATION_AMOUNT"].ToString()) : 0;

                                    // To Reduce the Depreciation Amount from the Item 
                                    assetItemSystem.Amount = (dtAssetDetails.Columns.Contains(this.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName))
                                        ? NumberSet.ToDecimal(drDetails[this.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString()) - assetItemSystem.DepreciationAmount : 0;

                                    // added by praveen to maintain the asset out amount
                                    if (Flag == AssetInOut.SL.ToString() || Flag == AssetInOut.DS.ToString() || Flag == AssetInOut.DN.ToString())
                                    {
                                        decimal CurrentAmount = (dtAssetDetails.Columns.Contains(this.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName))
                                        ? NumberSet.ToDecimal(drDetails[this.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString()) : 0;

                                        decimal actualAmount = (dtAssetDetails.Columns.Contains("TEMP_AMOUNT"))
                                        ? NumberSet.ToDecimal(drDetails["TEMP_AMOUNT"].ToString()) : 0;

                                        AssetAmount = actualAmount;
                                        if (CurrentAmount > actualAmount)
                                        {
                                            GainAmount = Math.Abs(CurrentAmount - actualAmount);
                                            LossAmount = 0;
                                        }
                                        else if (actualAmount > CurrentAmount)
                                        {
                                            LossAmount = Math.Abs(CurrentAmount - actualAmount);
                                            GainAmount = 0;
                                        }
                                        else
                                        {
                                            GainAmount = LossAmount = 0;
                                        }
                                    }
                                    else
                                    {
                                        AssetAmount = assetItemSystem.Amount;
                                        DeprAmount = assetItemSystem.DepreciationAmount;
                                    }
                                    assetItemSystem.ProjectId = ProjectId;
                                    assetItemSystem.ManufacturerId = (dtAssetDetails.Columns.Contains(this.AppSchema.Manufactures.IDColumn.ColumnName))
                                        ? NumberSet.ToInteger(drDetails[this.AppSchema.Manufactures.IDColumn.ColumnName].ToString()) : 0;
                                    assetItemSystem.Location_id = (dtAssetDetails.Columns.Contains(this.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName))
                                        ? NumberSet.ToInteger(drDetails[this.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName].ToString()) : 0;
                                    assetItemSystem.Custodian = (dtAssetDetails.Columns.Contains(this.AppSchema.AssetCustodians.CUSTODIAN_IDColumn.ColumnName))
                                        ? NumberSet.ToInteger(drDetails[this.AppSchema.AssetCustodians.CUSTODIAN_IDColumn.ColumnName].ToString()) : 0;
                                    assetItemSystem.AMCMonths = (dtAssetDetails.Columns.Contains(this.AppSchema.AMCDetails.AMC_MONTHSColumn.ColumnName))
                                        ? NumberSet.ToInteger(drDetails[this.AppSchema.AMCDetails.AMC_MONTHSColumn.ColumnName].ToString()) : 0;

                                    assetItemSystem.SalvageValue = (dtAssetDetails.Columns.Contains(this.AppSchema.AssetInOut.SALVAGE_VALUEColumn.ColumnName))
                                        ? NumberSet.ToDecimal(drDetails[this.AppSchema.AssetInOut.SALVAGE_VALUEColumn.ColumnName].ToString()) : 0;
                                    //assetItemSystem.Status = (dtAssetDetails.Columns.Contains(this.AppSchema.ASSETItem.STATUSColumn.ColumnName))
                                    //    ? NumberSet.ToInteger(drDetails[this.AppSchema.ASSETItem.STATUSColumn.ColumnName].ToString()) : 0;
                                    //assetItemSystem.Condition = (dtAssetDetails.Columns.Contains(this.AppSchema.ASSETItem.CONDITIONColumn.ColumnName))
                                    //    ? drDetails[this.AppSchema.ASSETItem.CONDITIONColumn.ColumnName].ToString() : string.Empty;
                                    assetItemSystem.Status = (Flag == AssetInOut.PU.ToString() || Flag == AssetInOut.OP.ToString() || Flag == AssetInOut.IK.ToString()) ? 1 : 0;

                                    if (assetItemSystem.ItemId > 0)
                                    {

                                        if (Flag == AssetInOut.PU.ToString() || Flag == AssetInOut.IK.ToString() || Flag == AssetInOut.OP.ToString())
                                        {
                                            // When the Entry type is Purchase or In kind or Opening balance then check the Details exists, If so Update the Details
                                            if (CheckItemDetailExists(assetItemSystem.ItemDetailId))
                                                resultArgs = assetItemSystem.SaveFixedAssetItemTransDetails(dManager);
                                            else // If not exists Add the Details
                                            {
                                                ItemDetailId = 0;
                                                resultArgs = assetItemSystem.SaveFixedAssetItemTransDetails(dManager);
                                                if (resultArgs.Success)
                                                    ItemDetailId = this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                            }
                                            // Saving the Insurance Details
                                            //if (AssetInsuranceCollection != null && AssetInsuranceCollection.Count > 0)
                                            //{
                                            if (AssetMultiInsuranceCollection != null && AssetMultiInsuranceCollection.Count > 0)
                                            {
                                                var Inskey = new Tuple<int, int>(ItemId, ItemDetailRowNo);
                                                if (AssetMultiInsuranceCollection.ContainsKey(Inskey)) // Checking whether the key exists
                                                {
                                                    dtInsuranceDetails = AssetMultiInsuranceCollection[Inskey];
                                                    if (dtInsuranceDetails != null && dtInsuranceDetails.Rows.Count > 0)
                                                    {
                                                        int itmID = NumberSet.ToInteger(dtInsuranceDetails.Rows[0][this.AppSchema.ASSETItem.ITEM_IDColumn.ColumnName].ToString());
                                                        if (assetItemSystem.ItemId == itmID)
                                                        {
                                                            foreach (DataRow drInsurance in dtInsuranceDetails.Rows)
                                                            {
                                                                assetItemSystem.ItemDetailId = ItemDetailId;
                                                                InsItemId = (dtInsuranceDetails.Columns.Contains(this.AppSchema.ASSETItem.ITEM_IDColumn.ColumnName))
                                                    ? NumberSet.ToInteger(drDetails[this.AppSchema.ASSETItem.ITEM_IDColumn.ColumnName].ToString()) : 0;
                                                                InsAssetId = (dtInsuranceDetails.Columns.Contains(this.AppSchema.ASSETItem.ASSET_IDColumn.ColumnName))
                                                    ? drDetails[this.AppSchema.ASSETItem.ASSET_IDColumn.ColumnName].ToString() : string.Empty;
                                                                assetItemSystem.InsurancePlanId = (dtInsuranceDetails.Columns.Contains(this.AppSchema.InsurancePlan.INSURANCE_PLAN_IDColumn.ColumnName))
                                                 ? NumberSet.ToInteger(drInsurance[this.AppSchema.InsurancePlan.INSURANCE_PLAN_IDColumn.ColumnName].ToString()) : 0;
                                                                assetItemSystem.PolicyNO = (dtInsuranceDetails.Columns.Contains(this.AppSchema.ASSETItem.POLICY_NOColumn.ColumnName))
                                                                    ? drInsurance[this.AppSchema.ASSETItem.POLICY_NOColumn.ColumnName].ToString() : string.Empty;

                                                                if (InsItemId.Equals(assetItemSystem.ItemId) && InsAssetId.Equals(assetItemSystem.AssetID))
                                                                {
                                                                    if (resultArgs.Success)
                                                                    {
                                                                        using (InsuranceRenewSystem renewsystem = new InsuranceRenewSystem())
                                                                        {
                                                                            renewsystem.ProjectId = ProjectId;
                                                                            renewsystem.ItemDetailId = ItemDetailId;
                                                                            resultArgs = renewsystem.SaveInsuranceVoucherDetails(dtInsuranceDetails, ItemId, ItemDetailRowNo, Flag);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // Update Status set 0
                                            // UpdateFixedAssetItemTransStatus((int)ActiveStatus.InActive);
                                        }

                                        if (resultArgs.Success)
                                            resultArgs = SaveAssetTrans(dManager);
                                        else
                                            break;
                                        ItemDetailRowNo++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        /// <summary>
        /// Delete the Item from Item Detail by Detail Id that are selected to be deleted.
        /// </summary>
        /// <param name="dtDetails"></param>
        /// <returns></returns>
        private ResultArgs DeleteItemDetailByItemId(DataTable dtDetails)
        {
            using (AssetItemSystem assetItemSystem = new AssetItemSystem())
            {
                string AssetItemdetlIDs = GetCommaSeparatedValue(dtDetails, SELECT, this.AppSchema.ASSETItem.ITEM_DETAIL_IDColumn.ColumnName);
                if (!string.IsNullOrEmpty(AssetItemdetlIDs))
                {
                    string[] IdList = AssetItemdetlIDs.Split(',');
                    foreach (string item in IdList)
                    {
                        assetItemSystem.ItemDetailId = ItemDetailId = NumberSet.ToInteger(item);
                        resultArgs = DeleteAssetTrans();
                        if (resultArgs.Success)
                        {
                            if (Flag == AssetInOut.PU.ToString() || Flag == AssetInOut.IK.ToString() || Flag == AssetInOut.OP.ToString())
                            {
                                resultArgs = DeleteInsuranceDetailsbyItemId();
                                if (resultArgs.Success)
                                    resultArgs = assetItemSystem.DeleteFixedAssetItemDetailsbyDetailId();
                            }
                        }
                    }
                }
            }

            if (Flag == AssetInOut.PU.ToString() || Flag == AssetInOut.IK.ToString() || Flag == AssetInOut.OP.ToString())    // Records to be deleted when SELECT is set to 1 in (Purchase and In Kind)
            {
                dtAssetDetails.AsEnumerable().Where(dr => NumberSet.ToInteger(dr[SELECT].ToString()) != 1);
                var ItemList = (from dr in dtAssetDetails.AsEnumerable()
                                where dr.RowState != DataRowState.Deleted ? dr.Field<Int32?>(SELECT) != 1 : false
                                select dr);
                if (ItemList.Count() > 0)
                    dtAssetDetails = ItemList.CopyToDataTable();
                else
                    dtAssetDetails = dtAssetDetails.Clone();
            }
            else  // Records to be Sold/Dispose/Donate when SELECT is set to 1 in (Other than Purchase and In Kind)
            {
                dtAssetDetails.AsEnumerable().Where(dr => NumberSet.ToInteger(dr[SELECT].ToString()) == 1);
                var ItemList = (from dr in dtAssetDetails.AsEnumerable()
                                where dr.RowState != DataRowState.Deleted ? dr.Field<Int32?>(SELECT) == 1 : false
                                select dr);
                if (ItemList.Count() > 0)
                    dtAssetDetails = ItemList.CopyToDataTable();
                else
                    dtAssetDetails = dtAssetDetails.Clone();
            }
            return resultArgs;
        }

        private ResultArgs DeleteInsuranceDetailsbyItemId(bool IsDelete = false)
        {
            using (InsuranceRenewSystem insurancesystem = new InsuranceRenewSystem())
            {
                resultArgs = insurancesystem.DeleteInsuranceDetailsByItemDetail(IsDelete == true ? ItemDetailIds : ItemDetailId.ToString());
            }
            return resultArgs;
        }

        /// <summary>
        /// Get Item Detail id Collections from the Table
        /// </summary>
        /// <param name="dtValue"></param>
        /// <param name="FilterColumn"></param>
        /// <param name="OutputColumnName"></param>
        /// <returns></returns>
        private string GetCommaSeparatedValue(DataTable dtValue, string FilterColumn, string OutputColumnName)
        {
            string retValue = String.Empty;
            if (dtValue != null && dtValue.Rows.Count > 0)
            {
                var rowVal = dtValue.AsEnumerable();
                if (Flag == AssetInOut.PU.ToString() || Flag == AssetInOut.IK.ToString() || Flag == AssetInOut.OP.ToString())
                {
                    dtValue.AsEnumerable().ToList<DataRow>().ForEach(dr =>
                    {
                        if (dr.RowState != DataRowState.Deleted)
                        {
                            if (!dtValue.Columns.Contains("TMP_STATUS"))
                            {
                                dtValue.Columns.Add("TMP_STATUS", typeof(string));
                            }
                            dr["TMP_STATUS"] = dr["STATUS"].ToString();
                            if (string.IsNullOrEmpty(dr["TMP_STATUS"].ToString()))
                            {
                                dr["TMP_STATUS"] = 1;
                            }
                        }
                    });

                    retValue = String.Join(",", (from r in rowVal
                                                 where r.RowState != DataRowState.Deleted ? r.Field<Int32?>(FilterColumn) == 1
                                                 && r.Field<string>("TMP_STATUS") == "1" : false
                                                 select r.Field<UInt32?>(OutputColumnName)));
                }
                else
                {
                    retValue = String.Join(",", (from r in rowVal
                                                 where r.RowState != DataRowState.Deleted ? r.Field<Int32?>(FilterColumn) != 1 : false
                                                 select r.Field<UInt32?>(OutputColumnName)));
                }
            }
            return retValue;
        }

        public ResultArgs UpdateFixedAssetItemTransStatus(int ItemStatus, bool IsDelete = false)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.UpdateAssetItemDetailStatus))
                {
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_DETAIL_IDsColumn, (!IsDelete) ? ItemDetailId.ToString() : ItemDetailIds);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.STATUSColumn, ItemStatus);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }
        #endregion

        #region Common Transactions

        public ResultArgs SaveAssetInwardOutward()
        {
            try
            {
                using (DataManager dataManager = new DataManager())
                {
                    resultArgs.Success = true;
                    dataManager.BeginTransaction();

                    if (Flag == AssetInOut.OP.ToString())
                    {
                        resultArgs = DeleteAssetInOutward(false);
                    }
                    else
                    {
                        resultArgs = SaveAssetVouchers(dataManager);
                        if (resultArgs.Success)
                            VoucherId = VoucherId == 0 ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : VoucherId;
                    }

                    if (resultArgs.Success)
                    {
                        isEdit = (InoutId > 0) ? true : false;
                        resultArgs = SaveAssetInOutMaster(dataManager);
                        if (resultArgs.Success)
                        {
                            if (InoutId == 0)
                                InoutId = this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());

                            if (!string.IsNullOrEmpty(SettingProperty.AssetDeletedInoutIds))
                            {
                                string InOutDetailId = SettingProperty.AssetDeletedInoutIds.TrimEnd(',');
                                InoutDetailIds = InOutDetailId;
                                if (!string.IsNullOrEmpty(InOutDetailId))
                                {
                                    resultArgs = FetchItemDetailIdByInoutDetailId();
                                    {
                                        if (resultArgs.Success)
                                        {
                                            ItemDetailIds = resultArgs.DataSource.Data.ToString();
                                        }
                                    }
                                    resultArgs = DeleteAssetTransByInOutDetailID(InOutDetailId);
                                    if (resultArgs.Success)
                                    {
                                        if (!string.IsNullOrEmpty(ItemDetailIds))
                                        {
                                            if (Flag == "OP" || Flag == "PU" || Flag == "IK")
                                            {
                                                resultArgs = DeleteAssetInsuranceDetails();
                                                if (resultArgs.Success)
                                                {
                                                    resultArgs = DeleteAssetItemDetails();
                                                    if (resultArgs.Success)
                                                    {
                                                        InoutDetailIds = InOutDetailId;
                                                        resultArgs = DeleteAssetInOutDetailByDetailId();
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                InoutDetailIds = InOutDetailId;
                                                resultArgs = DeleteAssetInOutDetailByDetailId();
                                            }
                                        }
                                    }
                                }
                            }

                            if (!string.IsNullOrEmpty(SettingProperty.AssetDeletedItemDetailIds))
                            {
                                SettingProperty.AssetDeletedItemDetailIds = SettingProperty.AssetDeletedItemDetailIds.TrimEnd(',');
                                string[] ItmDetCollections = SettingProperty.AssetDeletedItemDetailIds.Split(',');
                                foreach (string itemDetId in ItmDetCollections)
                                {
                                    if (!string.IsNullOrEmpty(itemDetId))
                                        resultArgs = DeleteItemDetail(NumberSet.ToInteger(itemDetId));
                                }
                            }

                            resultArgs = SaveAssetInOutDetail(dataManager);
                        }
                    }
                    if (!resultArgs.Success)
                        dataManager.TransExecutionMode = ExecutionMode.Fail;

                    dataManager.EndTransaction();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        public ResultArgs SaveAmcInwardOutward()
        {
            try
            {
                using (DataManager dataManager = new DataManager())
                {
                    resultArgs = SaveAssetVouchers(dataManager);
                    if (resultArgs.Success)
                        VoucherId = VoucherId == 0 ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : VoucherId;
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            finally
            { }
            return resultArgs;
        }

        public ResultArgs DeleteAssetInOutward(bool isDelete = true)
        {
            try
            {
                using (DataManager dataManager = new DataManager())
                {
                    if (isDelete)
                        dataManager.BeginTransaction();
                    resultArgs.Success = true;
                    if (InoutId > 0)
                    {
                        resultArgs = Flag == AssetInOut.OP.ToString() ? FetchAssetOPInOutIdsByProjectID() : FetchAssetInOutDetailIdByInOutId();
                        if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            InoutDetailIds = (!string.IsNullOrEmpty(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetInOut.IN_OUT_DETAIL_IDColumn.ColumnName].ToString())) ?
                                resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetInOut.IN_OUT_DETAIL_IDColumn.ColumnName].ToString() : string.Empty;
                            if (!string.IsNullOrEmpty(InoutDetailIds))
                            {
                                resultArgs = FetchAssetItemDetailIdByInOutDetailId();
                                ItemDetailIds = (!string.IsNullOrEmpty(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.ITEM_DETAIL_IDColumn.ColumnName].ToString())) ?
                                resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.ITEM_DETAIL_IDColumn.ColumnName].ToString() : string.Empty;
                                if (!string.IsNullOrEmpty(ItemDetailIds))
                                {
                                    resultArgs = DeleteAssetTransByInOutIds();
                                    if (resultArgs.Success)
                                    {
                                        if (Flag == AssetInOut.PU.ToString())  // This is to indicate Purchase / sales to be deleted.
                                        {
                                            resultArgs = DeleteInsuranceDetailsbyItemId(true);
                                            if (resultArgs.Success)
                                            {
                                                int SalesCount = CheckSoldAssetIdByItemDetailId();
                                                if (SalesCount > 0)
                                                {
                                                    resultArgs.Success = false;
                                                    resultArgs.Message = "Can not Delete. Sales / Disposal / Donation entry is made.";
                                                }
                                                else
                                                {
                                                    resultArgs = DeleteAssetItemDetails();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // Update Status Item Detail Id
                                            //using (AssetItemSystem itemSystem = new AssetItemSystem())
                                            //{
                                            //    UpdateFixedAssetItemTransStatus((int)ActiveStatus.Active, true);
                                            //}
                                        }
                                        if (resultArgs.Success)
                                        {
                                            resultArgs = DeleteAssetInOutDetail();
                                            if (resultArgs.Success)
                                            {
                                                using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
                                                {
                                                    resultArgs = FetchVoucherIdbyMasterId(InoutId);
                                                    if (resultArgs.Success && resultArgs.DataSource.Sclar.ToInteger > 0)
                                                    {
                                                        vouchersystem.VoucherId = resultArgs.DataSource.Sclar.ToInteger;
                                                        resultArgs = vouchersystem.RemoveVoucher(dataManager);
                                                    }
                                                }
                                                resultArgs = DeleteAssetInOutMaster();
                                            }
                                        }

                                    }
                                }
                            }
                        }
                        if (!resultArgs.Success)
                        {
                            dataManager.TransExecutionMode = ExecutionMode.Fail;
                        }
                        if (isDelete)
                            dataManager.EndTransaction();
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        public ResultArgs DeleteImportAssetOPDetails(bool isImport = false)
        {
            try
            {
                using (DataManager dataManager = new DataManager())
                {
                    resultArgs.Success = true;
                    resultArgs = FetchAssetOPInOutIdsByProjectID();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        InoutDetailIds = (!string.IsNullOrEmpty(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetInOut.IN_OUT_DETAIL_IDColumn.ColumnName].ToString())) ?
                            resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetInOut.IN_OUT_DETAIL_IDColumn.ColumnName].ToString() : string.Empty;
                        InoutIdCollections = (!string.IsNullOrEmpty(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetInOut.IN_OUT_IDColumn.ColumnName].ToString())) ?
                            resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetInOut.IN_OUT_IDColumn.ColumnName].ToString() : string.Empty;

                        if (!string.IsNullOrEmpty(InoutDetailIds) && !string.IsNullOrEmpty(InoutIdCollections))
                        {
                            resultArgs = FetchAssetItemDetailIdByInOutDetailId();
                            ItemDetailIds = (!string.IsNullOrEmpty(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.ITEM_DETAIL_IDColumn.ColumnName].ToString())) ?
                            resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.ITEM_DETAIL_IDColumn.ColumnName].ToString() : string.Empty;
                            if (!string.IsNullOrEmpty(ItemDetailIds))
                            {
                                resultArgs = DeleteAssetTransByInOutIds();
                                if (resultArgs.Success)
                                {
                                    if (isImport)
                                    {
                                        resultArgs = DeleteAssetItemDetails();
                                    }
                                    if (resultArgs.Success)
                                    {
                                        // Delete in out detail and in out master by In out Id
                                        resultArgs = DeleteAssetInOutMasterDetailCollection();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        //public ResultArgs DeleteFixedAssetItemDetailsById()
        //{
        //    try
        //    {
        //        using (DataManager dataManager = new DataManager())
        //        {
        //            if (AssetListCollection.Count - 1 >= ItemRowNo)
        //            {
        //                resultArgs = FetchAssetItemDetailIdByInOutDetailId(); // Fetch item Detail Ids by In out detail Id from Asset Trans 
        //                if (resultArgs.Success)
        //                {
        //                    ItemDetailIds = (!string.IsNullOrEmpty(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.ITEM_DETAIL_IDColumn.ColumnName].ToString())) ?
        //                      resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.ITEM_DETAIL_IDColumn.ColumnName].ToString() : string.Empty;
        //                    if (!string.IsNullOrEmpty(ItemDetailIds))
        //                    {
        //                        resultArgs = DeleteAssetTransByInOutIds();
        //                        if (resultArgs.Success)
        //                        {
        //                            resultArgs = DeleteInsuranceDetailsbyItemId();
        //                            if (resultArgs.Success)
        //                            {
        //                                resultArgs = DeleteAssetItemDetails();
        //                                if (resultArgs.Success)
        //                                {
        //                                    resultArgs = DeleteAssetInOutDetailByDetailId();
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            if (!resultArgs.Success)
        //            {
        //                dataManager.TransExecutionMode = ExecutionMode.Fail;
        //            }
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        resultArgs.Message = ex.ToString();
        //    }
        //    return resultArgs;
        //}

        /// <summary>
        /// Checking whether the Item Detail Id exists in the ASSET_ITEM_DETAIL Table
        /// </summary>
        /// <param name="ItemDetID"></param>
        /// <returns></returns>
        public bool CheckItemDetailExists(int ItemDetID)
        {
            bool isexists = false;
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.CheckItemDetailIdExists))
            {
                dataManager.Parameters.Add(AppSchema.ASSETItem.ITEM_DETAIL_IDColumn, ItemDetID);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                if (resultArgs != null && resultArgs.Success)
                {
                    if (resultArgs.DataSource.Sclar.ToInteger > 0)
                        isexists = true;
                }
            }
            return isexists;
        }

        public void AssigntoProperties()
        {
            if (this.InoutId > 0)
            {
                resultArgs = FetchAssetInOutMasterById();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    //DonorId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetInOut.DONOR_IDColumn.ColumnName].ToString());
                    InoutId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetInOut.IN_OUT_IDColumn.ColumnName].ToString());
                    VendorId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Vendors.VENDOR_IDColumn.ColumnName].ToString());
                    SoldTo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetInOut.SOLD_TOColumn.ColumnName].ToString();
                    ProjectId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString());
                    BillInvoiceNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetInOut.BILL_INVOICE_NOColumn.ColumnName].ToString();
                    InOutDate = this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetInOut.IN_OUT_DATEColumn.ColumnName].ToString(), false);
                    VoucherId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Voucher.VOUCHER_IDColumn.ColumnName].ToString());
                    TotalAmount = this.NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetInOut.TOT_AMOUNTColumn.ColumnName].ToString());
                    Flag = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetInOut.FLAGColumn.ColumnName].ToString();
                }
                if (resultArgs.Success)
                {
                    resultArgs = null;
                    resultArgs = FetchVoucherDetailsByVoucherId(this.VoucherId);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        VoucherNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_NOColumn.ColumnName].ToString();
                        DonorId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.DONOR_IDColumn.ColumnName].ToString());
                        purpose = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.PURPOSE_IDColumn.ColumnName].ToString());
                        Narration = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.NARRATIONColumn.ColumnName].ToString();
                        NameAddress = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.NAME_ADDRESSColumn.ColumnName].ToString();

                        AssetCurrencyCountryId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.CURRENCY_COUNTRY_IDColumn.ColumnName].ToString());
                        AssetExchangeRate = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.EXCHANGE_RATEColumn.ColumnName].ToString());
                        AssetCalcAmount = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.CALCULATED_AMOUNTColumn.ColumnName].ToString());
                        AssetCurrencyAmount = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.CONTRIBUTION_AMOUNTColumn.ColumnName].ToString());
                        AssetActualAmount = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.ACTUAL_AMOUNTColumn.ColumnName].ToString());
                        AssetExchageCountryId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.CURRENCY_COUNTRY_IDColumn.ColumnName].ToString());
                        AssetLiveExchangeRate = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.EXCHANGE_RATEColumn.ColumnName].ToString());
                    }
                }
            }
        }

        #endregion

        #region Cash/Bank Transactions

        // 24/10/2024 - Chinna
        // Included the Contribution,Acutal, Calculated Amount for Local Currency if Multi-Currency enabled
        public ResultArgs SaveAssetVouchers(DataManager dm)
        {
            try
            {
                DataView dvTransaction = null;
                resultArgs.Success = true;
                //if (Flag == AssetInOut.PU.ToString() || Flag == AssetInOut.AMC.ToString() || Flag == AssetInOut.IK.ToString()||)
                //{
                using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                {
                    voucherTransaction.VoucherId = VoucherId;
                    voucherTransaction.ProjectId = ProjectId;
                    voucherTransaction.TransVoucherMethod = (int)TransactionVoucherMethod.Automatic;
                    voucherTransaction.VoucherType = (Flag == AssetInOut.PU.ToString() || Flag == AssetInOut.AMC.ToString() || Flag == AssetInOut.INS.ToString()
                        ? DefaultVoucherTypes.Payment.ToString() : Flag == AssetInOut.IK.ToString() || Flag == AssetInOut.DN.ToString() || Flag == AssetInOut.DS.ToString()
                        // ? DefaultVoucherTypes.Journal.ToString() : DefaultVoucherTypes.Receipt.ToString());
                        // This is replaced for Journal Entries ( as JN rather JOurnal)
                        ? VoucherSubTypes.JN.ToString() : DefaultVoucherTypes.Receipt.ToString());
                    voucherTransaction.VoucherDefinitionId = (Flag == AssetInOut.PU.ToString() || Flag == AssetInOut.AMC.ToString() || Flag == AssetInOut.INS.ToString()
                        ? (Int32)DefaultVoucherTypes.Payment : Flag == AssetInOut.IK.ToString() || Flag == AssetInOut.DN.ToString() || Flag == AssetInOut.DS.ToString()
                        ? (Int32)DefaultVoucherTypes.Journal : (Int32)DefaultVoucherTypes.Receipt);
                    voucherTransaction.VoucherDate = InOutDate;
                    voucherTransaction.VoucherSubType = VoucherSubTypes.AST.ToString();
                    voucherTransaction.Narration = Narration;
                    voucherTransaction.NameAddress = NameAddress;
                    voucherTransaction.DonorId = DonorId;
                    voucherTransaction.PurposeId = purpose;
                    voucherTransaction.CreatedBy = NumberSet.ToInteger(this.LoginUserId);
                    voucherTransaction.ModifiedBy = NumberSet.ToInteger(this.LoginUserId);
                    voucherTransaction.Status = 1;
                    voucherTransaction.VoucherNo = VoucherNo;

                    //On 23/10/2024, If multi currency enabled, set local exchange rate and amount alone -----------------------------------------
                    //decimal exchangerate = 1;
                    //voucherTransaction.CurrencyCountryId = voucherTransaction.ExchageCountryId = 0;
                    //voucherTransaction.ExchangeRate = exchangerate;
                    //voucherTransaction.CalculatedAmount = 0;
                    //voucherTransaction.ActualAmount = 0;
                    //if (this.AllowMultiCurrency == 1)
                    //{
                    //    using (CountrySystem country = new CountrySystem())
                    //    {
                    //        exchangerate = country.FetchSettingCountryCurrencyExchangeRate(DateSet.ToDate(this.YearFrom, false));
                    //    }
                    //    voucherTransaction.CurrencyCountryId = this.NumberSet.ToInteger(string.IsNullOrEmpty(this.Country) ? "0" : this.Country);
                    //    voucherTransaction.ExchageCountryId = voucherTransaction.CurrencyCountryId;
                    //    voucherTransaction.ExchangeRate = exchangerate;
                    //}
                    //----------------------------------------------------------------------------------------------------------------------------


                    //On 03/12/2024, To Set Currency details 
                    voucherTransaction.CurrencyCountryId = 0;
                    voucherTransaction.ExchangeRate = 1;
                    voucherTransaction.CalculatedAmount = 0;
                    voucherTransaction.ActualAmount = 0;
                    voucherTransaction.ExchageCountryId = 0;
                    voucherTransaction.LedgerLiveExchangeRate = 0;

                    if (Flag == AssetInOut.PU.ToString() || Flag == AssetInOut.IK.ToString())
                    {
                        resultArgs = ConstructLedgerTransaction();
                        if (resultArgs.Success)
                        {
                            dvTransaction = resultArgs.DataSource.Table.DefaultView;
                            if (dtCashBank.Rows.Count > 0 && dtCashBank.Columns.Contains("LEDGER_ID"))
                            {
                                DataView dv = new DataView(dtCashBank);
                                dv.RowFilter = "LEDGER_ID>0";
                                dtCashBank = dv.ToTable();
                                //IEnumerable<DataRow> EnumurableInoutword = dtCashBank.Rows.Cast<DataRow>().Where(row => NumberSet.ToInteger(row["LEDGER_ID"].ToString()) > 0 ? false : true);
                                //if (EnumurableInoutword.Count() > 0)
                                //{
                                //    dtCashBank = EnumurableInoutword.CopyToDataTable();
                                //}
                            }
                            this.TransInfo = Flag == AssetInOut.PU.ToString() ? dvTransaction : dtCashBank.DefaultView;


                            this.CashTransInfo = dtCashBank.DefaultView;

                            decimal dTransDRAmount = 0;
                            decimal dTransCRAmount = 0;
                            decimal dCashBankAMount = 0;
                            if (Flag == AssetInOut.PU.ToString())
                            {
                                dTransDRAmount = this.NumberSet.ToDecimal(this.TransInfo.Table.Compute("SUM(" + "AMOUNT" + ")", "SOURCE" + "=" + (int)TransSource.Dr).ToString());
                                dTransCRAmount = this.NumberSet.ToDecimal(this.TransInfo.Table.Compute("SUM(" + "AMOUNT" + ")", "SOURCE" + "=" + (int)TransSource.Cr).ToString());
                                dCashBankAMount = this.NumberSet.ToDecimal(this.CashTransInfo.Table.Compute("SUM(" + "AMOUNT" + ")", string.Empty).ToString());
                            }
                            else if (Flag == AssetInOut.IK.ToString())
                            {
                                dTransDRAmount = this.NumberSet.ToDecimal(this.TransInfo.Table.Compute("SUM(" + "DEBIT" + ")", string.Empty).ToString());
                            }

                            //if (AllowMultiCurrency == 1)
                            //{
                            //    voucherTransaction.CalculatedAmount = (dTransDRAmount * exchangerate);
                            //    voucherTransaction.ContributionAmount = dTransDRAmount;
                            //    voucherTransaction.ActualAmount = (dTransDRAmount * exchangerate);
                            //}

                            if (this.AllowMultiCurrency == 1)
                            {
                                if (this.TransInfo != null)
                                {
                                    DataView dv = this.TransInfo;
                                    foreach (DataRowView drv in dv)
                                    {
                                        drv.BeginEdit();
                                        drv[voucherTransaction.AppSchema.VoucherTransaction.EXCHANGE_RATEColumn.ColumnName] = AssetExchangeRate;
                                        drv[voucherTransaction.AppSchema.VoucherTransaction.LIVE_EXCHANGE_RATEColumn.ColumnName] = AssetLiveExchangeRate;
                                        drv.EndEdit();
                                    }
                                }
                                voucherTransaction.CurrencyCountryId = AssetCurrencyCountryId;
                                voucherTransaction.ExchangeRate = AssetExchangeRate;
                                voucherTransaction.ContributionAmount = dTransDRAmount; //AssetCurrencyAmount;
                                voucherTransaction.CalculatedAmount = (AssetExchangeRate * dTransDRAmount);
                                voucherTransaction.ActualAmount = (AssetExchangeRate * dTransDRAmount);
                                voucherTransaction.ExchageCountryId = AssetExchageCountryId;


                                if (this.CashTransInfo != null)
                                {
                                    DataView dvCash = this.CashTransInfo;
                                    foreach (DataRowView drvCash in dvCash)
                                    {
                                        drvCash.BeginEdit();
                                        drvCash[voucherTransaction.AppSchema.VoucherTransaction.EXCHANGE_RATEColumn.ColumnName] = AssetExchangeRate;
                                        drvCash[voucherTransaction.AppSchema.VoucherTransaction.LIVE_EXCHANGE_RATEColumn.ColumnName] = AssetLiveExchangeRate;
                                        drvCash.EndEdit();
                                    }
                                }
                            }

                            resultArgs = voucherTransaction.SaveVoucherDetails(dm);
                            if (resultArgs.Success)
                            {
                                VoucherId = voucherTransaction.VoucherId;
                            }
                        }
                    }
                    else if (Flag == AssetInOut.AMC.ToString() || Flag == AssetInOut.INS.ToString())
                    {
                        this.TransInfo = dtinoutword.DefaultView;
                        this.CashTransInfo = dtCashBank.DefaultView;
                        resultArgs = voucherTransaction.SaveVoucherDetails(dm);
                        if (resultArgs.Success)
                        {
                            VoucherId = voucherTransaction.VoucherId;
                        }
                    }
                    else
                    {
                        IEnumerable<DataRow> EnumurableInoutword = dtAssetVoucher.Rows.Cast<DataRow>().Where(row => NumberSet.ToInteger(row["LEDGER_ID"].ToString()) == 0 ? false : true);
                        if (EnumurableInoutword.Count() > 0)
                        {
                            dtAssetVoucher = EnumurableInoutword.CopyToDataTable();
                            int LEDGID = 0;
                            if (dtAssetVoucher != null && dtAssetVoucher.Rows.Count > 0)
                            {
                                foreach (DataRow drLedger in dtAssetVoucher.Rows)
                                {
                                    LEDGID = NumberSet.ToInteger(drLedger["LEDGER_ID"].ToString());

                                    using (MappingSystem mapsystem = new MappingSystem())
                                    {
                                        mapsystem.ProjectId = ProjectId;
                                        mapsystem.LedgerId = LEDGID;
                                        resultArgs = mapsystem.MapProjectLedger();
                                    }
                                }
                            }
                        }

                        decimal dTransDRAmount = 0;
                        decimal dTransCRAmount = 0;

                        this.TransInfo = dtAssetVoucher.DefaultView;

                        if (Flag == AssetInOut.SL.ToString())
                        {
                            dTransDRAmount = this.NumberSet.ToDecimal(this.TransInfo.Table.Compute("SUM(" + "AMOUNT" + ")", "SOURCE" + "=" + (int)TransSource.Dr).ToString());
                            dTransCRAmount = this.NumberSet.ToDecimal(this.TransInfo.Table.Compute("SUM(" + "AMOUNT" + ")", "SOURCE" + "=" + (int)TransSource.Cr).ToString());
                        }
                        else if (Flag == AssetInOut.SL.ToString() || Flag == AssetInOut.DS.ToString() || Flag == AssetInOut.DN.ToString())
                        {
                            dTransCRAmount = this.NumberSet.ToDecimal(this.TransInfo.Table.Compute("SUM(" + "CREDIT" + ")", string.Empty).ToString());
                        }

                        //if (AllowMultiCurrency == 1)
                        //{
                        //    voucherTransaction.CalculatedAmount = (dTransCRAmount * exchangerate);
                        //    voucherTransaction.ContributionAmount = dTransCRAmount;
                        //    voucherTransaction.ActualAmount = (dTransCRAmount * exchangerate);
                        //}

                        IEnumerable<DataRow> EnumurableCash = dtCashBank.Rows.Cast<DataRow>().Where(row => NumberSet.ToInteger(row["LEDGER_ID"].ToString()) == 0 ? false : true);
                        if (EnumurableInoutword.Count() > 0)
                        {
                            dtCashBank = EnumurableCash.CopyToDataTable();
                            int CASHLEDGID = 0;
                            if (dtCashBank != null && dtCashBank.Rows.Count > 0)
                            {
                                foreach (DataRow drLedger in dtCashBank.Rows)
                                {
                                    CASHLEDGID = NumberSet.ToInteger(drLedger["LEDGER_ID"].ToString());

                                    using (MappingSystem mapsystem = new MappingSystem())
                                    {
                                        mapsystem.ProjectId = ProjectId;
                                        mapsystem.LedgerId = CASHLEDGID;
                                        resultArgs = mapsystem.MapProjectLedger();
                                    }
                                }
                            }
                        }
                        this.CashTransInfo = dtCashBank.DefaultView;


                        if (this.AllowMultiCurrency == 1)
                        {
                            if (this.TransInfo != null)
                            {
                                DataView dv = this.TransInfo;
                                foreach (DataRowView drv in dv)
                                {
                                    drv.BeginEdit();
                                    drv[voucherTransaction.AppSchema.VoucherTransaction.EXCHANGE_RATEColumn.ColumnName] = AssetExchangeRate;
                                    drv[voucherTransaction.AppSchema.VoucherTransaction.LIVE_EXCHANGE_RATEColumn.ColumnName] = AssetLiveExchangeRate;
                                    drv.EndEdit();
                                }
                            }
                            voucherTransaction.CurrencyCountryId = AssetCurrencyCountryId;
                            voucherTransaction.ExchangeRate = AssetExchangeRate;
                            voucherTransaction.ContributionAmount = dTransCRAmount; //AssetCurrencyAmount;
                            voucherTransaction.CalculatedAmount = (AssetExchangeRate * dTransCRAmount);
                            voucherTransaction.ActualAmount = (AssetExchangeRate * dTransCRAmount);
                            voucherTransaction.ExchageCountryId = AssetExchageCountryId;

                            if (this.CashTransInfo != null)
                            {
                                DataView dvCash = this.CashTransInfo;
                                foreach (DataRowView drvCash in dvCash)
                                {
                                    drvCash.BeginEdit();
                                    drvCash[voucherTransaction.AppSchema.VoucherTransaction.EXCHANGE_RATEColumn.ColumnName] = AssetExchangeRate;
                                    drvCash[voucherTransaction.AppSchema.VoucherTransaction.LIVE_EXCHANGE_RATEColumn.ColumnName] = AssetLiveExchangeRate;
                                    drvCash.EndEdit();
                                }
                            }
                        }

                        resultArgs = voucherTransaction.SaveVoucherDetails(dm);
                        if (resultArgs.Success)
                        {
                            VoucherId = voucherTransaction.VoucherId;
                        }
                    }
                }
                //}
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        private ResultArgs ConstructLedgerTransaction()
        {
            int ItemId = 0;
            try
            {
                //if (Flag == AssetInOut.PU.ToString())
                //{
                if (dtinoutword != null && dtinoutword.Rows.Count > 0)
                {
                    foreach (DataRow drAssetItem in dtinoutword.Rows)
                    {
                        ItemId = this.NumberSet.ToInteger(drAssetItem[this.AppSchema.ASSETItem.ITEM_IDColumn.ColumnName].ToString());
                        if (ItemId > 0)
                        {
                            using (AssetItemSystem assetItemSystem = new AssetItemSystem(ItemId))
                            {
                                drAssetItem[this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName] = assetItemSystem.AccountLeger;

                                using (MappingSystem mapsystem = new MappingSystem())
                                {
                                    mapsystem.ProjectId = ProjectId;
                                    mapsystem.LedgerId = assetItemSystem.AccountLeger;
                                    resultArgs = mapsystem.MapProjectLedger();
                                }
                            }
                        }
                    }
                    if (dtinoutword.Rows.Count > 0 && dtinoutword.Columns.Contains("LEDGER_ID"))
                    {
                        IEnumerable<DataRow> EnumurableInoutword = dtinoutword.Rows.Cast<DataRow>().Where(row => string.IsNullOrEmpty(row["ITEM_ID"].ToString()) ? false : true);
                        if (EnumurableInoutword.Count() > 0)
                        {
                            dtinoutword = EnumurableInoutword.CopyToDataTable();
                        }
                    }
                }
                //}
                //else if (Flag == AssetInOut.IK.ToString())
                //{
                //    //DataRow dr1 = new DataRow();
                //    //DataRow dr2 = new DataRow();
                //    //DataTable dt = new DataTable();
                //    //dt.Columns.Add("LEDGER_ID",typeof(int));
                //    //dt.Columns.Add("CREDIT", typeof(double));
                //    //dt.Columns.Add("DEBIT",typeof(double));
                //    //dr1["LEDGER_ID"] = this.LedgerId;
                //    //dr1["CREDIT"] = this.LedgerId;
                //    //dr1["DEBIT"] = 0;
                //    //dr2["LEDGER_ID"] = this.LedgerId;
                //    //dr2["CREDIT"] = 0;
                //    //dr2["DEBIT"] = 0;
                //    ////dt.Rows.Add(this.LedgerId);
                //    //dtinoutword = dt;


                //}

            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                resultArgs.DataSource.Data = dtinoutword;
            }
            return resultArgs;
        }

        public ResultArgs FetchCashBankByVoucherId(int VoucherID)
        {
            using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
            {
                vouchersystem.VoucherId = VoucherID;
                vouchersystem.VoucherType = this.Flag == AssetInOut.PU.ToString() ? DefaultVoucherTypes.Payment.ToString() : (this.Flag == AssetInOut.SL.ToString() || this.Flag == AssetInOut.DS.ToString()) ?
                    DefaultVoucherTypes.Receipt.ToString() : DefaultVoucherTypes.Journal.ToString();
                resultArgs = vouchersystem.FetchAssetCashBankDetails();
            }
            return resultArgs;
        }

        public ResultArgs FetchAMCInsuranceByVoucherId(int VoucherID)
        {
            using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
            {
                vouchersystem.VoucherId = VoucherID;
                vouchersystem.VoucherType = DefaultVoucherTypes.Payment.ToString();
                resultArgs = vouchersystem.FetchAssetInsuranceAMCDetails();
            }
            return resultArgs;
        }

        public ResultArgs FetchCashBankByVoucherIdForPurchase(int VoucherID)
        {
            using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
            {
                vouchersystem.VoucherId = VoucherID;
                vouchersystem.VoucherType = this.Flag == AssetInOut.PU.ToString() ? DefaultVoucherTypes.Payment.ToString() : (this.Flag == AssetInOut.SL.ToString() || this.Flag == AssetInOut.DS.ToString()) ?
                    DefaultVoucherTypes.Receipt.ToString() : DefaultVoucherTypes.Journal.ToString();
                resultArgs = vouchersystem.FetchCashBankByVoucherIdForPurchase();
            }
            return resultArgs;
        }


        public ResultArgs FetchVoucherIdbyMasterId(int InOutId, bool AllVoucher = false)
        {
            using (DataManager dataManager = new DataManager(AllVoucher == true ? SQLCommand.AssetInOut.FetchVoucherIdCollection : SQLCommand.AssetInOut.FetchVoucherIdbyMasterId))
            {
                if (!AllVoucher)
                {
                    dataManager.Parameters.Add(AppSchema.AssetInOut.IN_OUT_IDColumn, InOutId);
                }
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        #endregion

        #region Fetch Methods

        public ResultArgs FetchAssetList(AssetInOut AssetInOutType)
        {
            using (DataManager dataManager = new DataManager(AssetInOutType == AssetInOut.PU || AssetInOut.IK == AssetInOutType || AssetInOut.OP == AssetInOutType ?
                SQLCommand.AssetInOut.FetchAssetListItem : SQLCommand.AssetInOut.FetchAssetListItemByItemId))
            {
                dataManager.Parameters.Add(AppSchema.AssetInOut.IN_OUT_DETAIL_IDColumn, InoutDetailId);
                dataManager.Parameters.Add(AppSchema.AssetInOut.IN_OUT_DATEColumn, InOutDate);
                if (AssetInOutType != AssetInOut.PU || AssetInOut.IK != AssetInOutType || AssetInOut.OP != AssetInOutType)
                {
                    dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(AppSchema.AssetInOut.STATUSColumn, InoutId);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs FetchItemDetailIdByInoutDetailId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.FetchItemDetailIdByInoutDetailId))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.IN_OUT_DETAIL_IDColumn, InoutDetailIds);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        public int FetchItemDetailByAssetId(string AssetID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.FetchItemDetailIdByAssetId))
            {
                dataManager.Parameters.Add(AppSchema.ASSETItem.ASSET_IDColumn, AssetID);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int FetchAvailableQty()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.FetchAvailQty))
            {
                dataManager.Parameters.Add(AppSchema.AssetInOut.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(AppSchema.ASSETLocationDetails.LOCATION_IDColumn, LocationId);
                dataManager.Parameters.Add(AppSchema.ASSETItem.ITEM_IDColumn, ItemId);
                dataManager.Parameters.Add(AppSchema.AssetInOut.IN_OUT_DATEColumn, InOutDate);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int FetchCurrentQty(int InOutDetailId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.FetchCurrentQty))
            {
                dataManager.Parameters.Add(AppSchema.AssetInOut.IN_OUT_DETAIL_IDColumn, InOutDetailId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs FetchVoucherDetailsByVoucherId(int VoucherID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.FetchVoucherDetailsByVoucherId))
            {
                dataManager.Parameters.Add(AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherID);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchLatestAssetIdCount(int AssetItemId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.FetchLasterAssetId))
            {
                dataManager.Parameters.Add(AppSchema.ASSETItem.ITEM_IDColumn, AssetItemId);
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs AutoFetchSoldTo()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.AutoFetchSoldTo))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchFixedAssetRegister()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetInOut.FetchFixedAssetRegister))
            {
                datamanager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                datamanager.Parameters.Add(this.AppSchema.AssetInOut.IN_OUT_DATEColumn, InOutDate);
                if (!string.IsNullOrEmpty(AssetClassId))
                {
                    datamanager.Parameters.Add(this.AppSchema.ASSETClassDetails.ASSET_CLASS_IDColumn, AssetClassId);
                }
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion

        #region OP Methods
        public ResultArgs FetchAssetOPInOutIdsByProjectID()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.FetchOPInoutIdsByProjectId))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAssetOPDetailssByProjectID()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.FetchOPDetailsByProjectId))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.PROJECT_IDColumn, ProjectId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs CheckAssetItemIdExists(int ItemID, string AssetID, int ProjectID, int ItmDetailID = 0)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.CheckAssetIDExists))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.ITEM_IDColumn, ItemID);
                dataManager.Parameters.Add(this.AppSchema.ASSETItem.ASSET_IDColumn, AssetID);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectID);
                if (ItmDetailID > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_DETAIL_IDColumn, ItmDetailID);
                }
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        public ResultArgs DeleteItemDetail(int ItemDETilID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.DeleteAssetItemDetail))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_DETAIL_IDColumn, ItemDETilID);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteUnusedItemDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.DeleteAssetUnusedItems))
            {
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteAllTransaction()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.DeleteAllAssetTransaction))
            {
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs CheckTransactionExists()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.CheckAssetTransactionExists))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        public ResultArgs FetchAccountLedgerDetailsByItem(int ItmID, int ProjectID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.FetchAccountLedgerByItem))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_IDColumn, ItmID);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectID);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchTransactionDetailsByItemId(int ItmID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInOut.FetchTransactionDetailsByItemId))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_IDColumn, ItmID);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// Delete all the transactions
        /// </summary>
        /// <returns></returns>
        public ResultArgs DeleteAssetTransaction()
        {
            using (DataManager dataManager = new DataManager())
            {
                using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
                {
                    resultArgs = FetchVoucherIdbyMasterId(0, true);

                    if (resultArgs.Success && !string.IsNullOrEmpty(resultArgs.DataSource.Sclar.ToString))
                    {
                        string[] VoucherIDs = resultArgs.DataSource.Sclar.ToString.Split(',');

                        foreach (string vid in VoucherIDs)
                        {
                            vouchersystem.VoucherId = NumberSet.ToInteger(vid.ToString());
                            resultArgs = vouchersystem.RemoveVoucher(dataManager);
                            if (!resultArgs.Success) break;
                        }
                    }

                    if (resultArgs.Success)
                    {
                        resultArgs = DeleteAllTransaction();
                    }

                }
            }
            return resultArgs;
        }

        // it can be taken for the deleting based on projects
        //public ResultArgs DeleteAssetTransaction()
        //{
        //    using (DataManager dataManager = new DataManager())
        //    {
        //        using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
        //        {
        //            DataView dvValues = new DataView(dtExcelDataSource);
        //            DataTable distinctProjects = new DataTable();
        //            distinctProjects = dvValues.ToTable(true, "PROJECT");
        //            GetProjectIds(distinctProjects);

        //            resultArgs = GetProjectIds(distinctProjects);

        //            if (resultArgs.Success && (resultArgs.ReturnValue != null))
        //            {
        //                ProjectIds = resultArgs.ReturnValue != null ? resultArgs.ReturnValue.ToString() : "0";
        //                if (!string.IsNullOrEmpty(ProjectIds))
        //                {
        //                    resultArgs = FetchVoucherIdbyMasterId(0, true);
        //                    if (resultArgs.Success && !string.IsNullOrEmpty(resultArgs.DataSource.Sclar.ToString))
        //                    {
        //                        string[] VoucherIDs = resultArgs.DataSource.Sclar.ToString.Split(',');

        //                        foreach (string vid in VoucherIDs)
        //                        {
        //                            vouchersystem.VoucherId = NumberSet.ToInteger(vid.ToString());
        //                            resultArgs = vouchersystem.RemoveVoucher(dataManager);
        //                            if (!resultArgs.Success) break;
        //                        }
        //                    }

        //                    // Delete the Opening Balances 


        //                    if (resultArgs.Success)
        //                    {
        //                        resultArgs = DeleteAllTransaction();
        //                    }
        //                }
        //            }

        //        }
        //    }
        //    return resultArgs;
        //}


        /// <summary>
        /// This method is to get the Project Id's in the XML that is passed.
        /// These Id's will be used to fetch transaction of those projects and used for deletion.
        /// </summary>
        /// <returns></returns>
        private ResultArgs GetProjectIds(DataTable dtProjects)
        {
            try
            {
                string ProjectIds = string.Empty;
                if (dtProjects != null && dtProjects.Rows.Count > 0)
                {
                    foreach (DataRow drProject in dtProjects.Rows)
                    {
                        string ProjectName = drProject[this.AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                        int ProjectId = GetProjectId(ProjectName);

                        if (ProjectId > 0)
                        {
                            ProjectIds += ProjectId.ToString();
                            ProjectIds += ',';
                        }
                    }

                    if (!string.IsNullOrEmpty(ProjectIds))
                    {
                        resultArgs.ReturnValue = ProjectIds.TrimEnd(',');
                        resultArgs.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            return resultArgs;
        }

        private int GetProjectId(string projectName)
        {
            using (Bosco.Model.Dsync.ImportMasterSystem importSystem = new Bosco.Model.Dsync.ImportMasterSystem())
            {
                importSystem.ProjectName = projectName;
                return importSystem.GetMasterId(DataSync.Project);
            }
        }
        #endregion
    }
}