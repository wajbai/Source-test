using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using Bosco.Model.UIModel;
using AcMEDSync.Model;

namespace Bosco.Model
{
    public class AssetItemSystem : SystemBase
    {

        #region Variable Declearation
        ResultArgs resultArgs = new ResultArgs();
        private static DataView dvSetting = null;
        private const string SettingNameField = "NAME";
        private const string SettingValueField = "LEDGER_ID";
        #endregion

        #region Constructor


        public AssetItemSystem(int Item_Id)
        {
            this.ItemId = Item_Id;
            AssignToAssetItemPoroperties();
        }

        public AssetItemSystem()
        {
            AssignAssetLedgers();
        }


        #endregion

        #region Properties

        public int ItemId { get; set; }
        public int ProjectId { get; set; }
        public int AccountLedgerId { get; set; }
        public double TempSummaryValue { get; set; }
        public double TotalSummmaryValue { get; set; }
        public string AssetID { get; set; }
        public int AssetClassId { get; set; }
        public string AssetClass { get; set; }
        public int DepreciationLedger { get; set; }
        public int DisposalLedger { get; set; }
        public int AccountLeger { get; set; }
        public int Catogery { get; set; }
        public string Name { get; set; }
        public string ItemKind { get; set; }
        public int Unit { get; set; }
        public int Custodian { get; set; }
        public string Method { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int StartingNo { get; set; }
        public int Quantity { get; set; }
        public decimal RatePerItem { get; set; }
        public decimal Amount { get; set; }
        public decimal DepreciationAmount { get; set; }
        public decimal SalvageValue { get; set; }
        public DataTable dtAssetDetail { get; set; }
        public int Width { get; set; }

        public DataTable dtMappedAssetItems { get; set; }

        public int Location_id { get; set; }
        public int PurchaseId { get; set; }
        public int SourceFlag { get; set; }
        public int Status { get; set; }
        public decimal UsefulLife { get; set; }
        public decimal SalvageLife { get; set; }
        public int SalesId { get; set; }
        public int RetentionYrs { get; set; }
        public int DepreciationYrs { get; set; }
        public int InsuranceApplicable { get; set; }
        public int AMCApplicable { get; set; }
        public int DepreciatonApplicable { get; set; }
        public int AssetItemMode { get; set; }
        public string UOM { get; set; }
        public string DepreciationNo { get; set; }

        public int ConditionID { get; set; }
        public int ManufacturerId { get; set; }
        public int InsurancePlanId { get; set; }
        public string PolicyNO { get; set; }
        public int AMCMonths { get; set; }
        public string Condition { get; set; }
        public int BranchId { get; set; }
        public int ItemDetailId { get; set; }
        public int InoutDetailId { get; set; }
        public DataTable dtUpdateAssetDetails { get; set; }
        public int LedgerGroupId { get; set; }

        #endregion

        #region Methods
        public ResultArgs SaveAssetOpening()
        {
            double CurBal = 0.00;
            double dCalculateCurBal = 0.00;
            string Mode = string.Empty;

            try
            {
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.BeginTransaction();
                    resultArgs = DeleteAssetItemDetail();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        resultArgs = SaveAssetItemDetailOpeningBalance();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            using (NumberSystem numberSystem = new NumberSystem())
                            {
                                resultArgs = numberSystem.RegenerateAssetID(ItemId);
                                if (resultArgs.Success)
                                {
                                    using (BalanceSystem balanceSystem = new BalanceSystem())
                                    {
                                        //  resultArgs = balanceSystem.UpdateOpBalance(this.BookBeginFrom, ProjectId,
                                        //                      AccountLedgerId, TempSummaryValue, "DR", TransactionAction.Cancel);
                                        //   if (resultArgs.Success)
                                        //    {
                                        BalanceProperty Balance = balanceSystem.GetBalance(ProjectId,
                                            AccountLedgerId, this.BookBeginFrom, BalanceSystem.BalanceType.OpeningBalance);

                                        if (Balance.TransMode == TransactionMode.CR.ToString())
                                        {
                                            CurBal = -(Balance.Amount);
                                        }
                                        else
                                        {
                                            CurBal = Balance.Amount;
                                        }

                                        dCalculateCurBal = CurBal + TotalSummmaryValue;
                                        if (dCalculateCurBal < 0)
                                        {
                                            Mode = TransactionMode.CR.ToString();
                                        }
                                        else
                                        {
                                            Mode = TransactionMode.DR.ToString();
                                        }

                                        resultArgs = balanceSystem.UpdateOpBalance(this.BookBeginFrom, ProjectId,
                                            AccountLedgerId, dCalculateCurBal, Mode, TransactionAction.New);
                                        //    }
                                    }
                                }
                            }
                        }
                    }

                    if (!resultArgs.Success)
                    {
                        dataManager.TransExecutionMode = ExecutionMode.Fail;
                    }
                    dataManager.EndTransaction();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            return resultArgs;
        }

        private ResultArgs SaveAssetItemDetailOpeningBalance()
        {
            try
            {
                dtAssetDetail.AcceptChanges();
                if (dtAssetDetail != null && dtAssetDetail.Rows.Count > 0)
                {

                    foreach (DataRow drAsset in dtAssetDetail.Rows)
                    {
                        Location_id = this.NumberSet.ToInteger(drAsset[this.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName].ToString());
                        Custodian = this.NumberSet.ToInteger(drAsset[this.AppSchema.AssetCustodians.CUSTODIAN_IDColumn.ColumnName].ToString());
                        Amount = this.NumberSet.ToDecimal(drAsset[this.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString());
                        AssetID = drAsset[this.AppSchema.ASSETItem.ITEM_IDColumn.ColumnName].ToString();
                        SourceFlag = (int)AssetSourceFlag.OpeningBalance;

                        resultArgs = SaveAssetItemDetail();

                        if (!resultArgs.Success)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            return resultArgs;
        }


        private int GetDefaultLedgers(string Name)
        {
            int Id = 0;
            using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.FetchAssetMapedLedgers))
            {
                dataManager.Parameters.Add(this.AppSchema.Settings.SETTING_NAMEColumn, Name);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    Id = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Settings.VALUEColumn.ToString()].ToString());
                }
            }
            return Id;
        }

        private void AssignAssetLedgers()
        {
            AccountLedgerId = GetDefaultLedgers(AssetLedgerType.AccountLedgerId.ToString()); //"AccountLedgerId");
            DepreciationLedger = GetDefaultLedgers(AssetLedgerType.DepreciationLedgerId.ToString());// "DepreciationLedgerId");
            DisposalLedger = GetDefaultLedgers(AssetLedgerType.DisposalLedgerId.ToString());// "DisposalLedgerId");
        }

        public ResultArgs UpdateAssetDetails()
        {
            try
            {
                if (dtUpdateAssetDetails != null && dtUpdateAssetDetails.Rows.Count > 0)
                {
                    foreach (DataRow drUpdateAsset in dtUpdateAssetDetails.Rows)
                    {
                        Location_id = this.NumberSet.ToInteger(drUpdateAsset[this.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName].ToString());
                        Custodian = this.NumberSet.ToInteger(drUpdateAsset[this.AppSchema.AssetCustodians.CUSTODIAN_IDColumn.ColumnName].ToString());
                        ManufacturerId = this.NumberSet.ToInteger(drUpdateAsset[this.AppSchema.Manufactures.MANUFACTURER_IDColumn.ColumnName].ToString());
                        ItemDetailId = this.NumberSet.ToInteger(drUpdateAsset[this.AppSchema.ASSETItem.ITEM_DETAIL_IDColumn.ColumnName].ToString());
                        Condition = drUpdateAsset[this.AppSchema.ASSETItem.CONDITIONSColumn.ColumnName].ToString();
                        resultArgs = SaveUpdateAssetDetails();
                        if (!resultArgs.Success)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            return resultArgs;
        }
        private ResultArgs SaveUpdateAssetDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.UpdateAssetDetail))
                {
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_DETAIL_IDColumn, ItemDetailId);
                    dataManager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATION_IDColumn, Location_id);
                    dataManager.Parameters.Add(this.AppSchema.AssetCustodians.CUSTODIAN_IDColumn, Custodian);
                    dataManager.Parameters.Add(this.AppSchema.Manufactures.MANUFACTURER_IDColumn, ManufacturerId);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.CONDITIONSColumn, Condition);
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
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
        public ResultArgs SaveAssetItemDetail()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.AssetItemDetailAdd))
                {
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_IDColumn, ItemId);
                    dataManager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATION_IDColumn, Location_id);
                    dataManager.Parameters.Add(this.AppSchema.AssetCustodians.CUSTODIAN_IDColumn, Custodian);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.AMOUNTColumn, Amount);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_IDColumn, AssetID);
                    dataManager.Parameters.Add(this.AppSchema.AssetPurchaseDetail.PURCHASE_IDColumn, PurchaseId);
                    dataManager.Parameters.Add(this.AppSchema.SalesAsset.SALES_IDColumn, SalesId);
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    //dataManager.Parameters.Add(this.AppSchema.ASSETItem.SOURCE_FLAGColumn, SourceFlag);
                    dataManager.Parameters.Add(this.AppSchema.AssetPurchaseDetail.SALVAGE_LIFEColumn, SalvageLife);

                    resultArgs = dataManager.UpdateData();
                }


            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        public ResultArgs DeleteAssetItemDetail()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.AssetItemDetailsDelete))
                {
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_IDColumn, ItemId);
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATION_IDColumn, Location_id);

                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }
        public ResultArgs DeleteAssetItemDetailByPurchase()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.AssetItemDetailsDeleteByPurchase))
                {
                    dataManager.Parameters.Add(this.AppSchema.AssetPurchaseDetail.PURCHASE_IDColumn, PurchaseId);
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

        public ResultArgs UpdateAssetItemDetailByOldSales()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.UpdateAssetItemDetailBySalesId))
                {
                    dataManager.Parameters.Add(this.AppSchema.SalesAsset.SALES_IDColumn, SalesId);
                    //dataManager.Parameters.Add(this.AppSchema.ASSETItem.SOURCE_FLAGColumn, SourceFlag);
                    //dataManager.Parameters.Add(this.AppSchema.ASSETItem.STATUSColumn, Status);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_IDColumn, AssetID);
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        public ResultArgs UpdateMappedAccountLedgerToItems()
        {
            foreach (DataRow dritem in dtMappedAssetItems.Rows)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.UpdateAccountLedgerToItem))
                {
                    int LedgerId = this.NumberSet.ToInteger(dritem["LEDGER_ID"].ToString());
                    int ItemId = this.NumberSet.ToInteger(dritem["ITEM_ID"].ToString());
                    if (LedgerId > 0 && ItemId > 0)
                    {
                        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                        dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_IDColumn, ItemId);
                        resultArgs = dataManager.UpdateData();
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs UpdateAssetItemDetailByNewSales()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.UpdateAssetItemDetailBySales))
                {
                    dataManager.Parameters.Add(this.AppSchema.SalesAsset.SALES_IDColumn, SalesId);
                    //dataManager.Parameters.Add(this.AppSchema.ASSETItem.SOURCE_FLAGColumn, SourceFlag);
                    //dataManager.Parameters.Add(this.AppSchema.ASSETItem.STATUSColumn, Status);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_IDColumn, AssetID);
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }
        /// <summary>
        /// Save asset item details.
        /// </summary>
        /// <returns></returns>
        public ResultArgs SaveItemDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager((ItemId == 0) ? SQLCommand.AssetItem.Add : SQLCommand.AssetItem.Update))
                {
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_IDColumn, ItemId);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.ASSET_CLASS_IDColumn, AssetClassId);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.DEPRECIATION_LEDGER_IDColumn, DepreciationLedger);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.DISPOSAL_LEDGER_IDColumn, DisposalLedger);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.ACCOUNT_LEDGER_IDColumn, AccountLeger);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.ASSET_ITEMColumn, Name);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.UOM_IDColumn, Unit);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.PREFIXColumn, Prefix);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.SUFFIXColumn, Suffix);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.STARTING_NOColumn, StartingNo);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.WIDTHColumn, Width);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.RETENTION_YRSColumn, RetentionYrs);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.DEPRECIATION_YRSColumn, DepreciationYrs);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.IS_INSURANCEColumn, InsuranceApplicable);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.IS_AMCColumn, AMCApplicable);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.IS_ASSET_DEPRECIATIONColumn, DepreciatonApplicable);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.ASSET_MODEColumn, AssetItemMode);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.DEPRECIATION_NOColumn, DepreciationNo);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.ASSET_ACCESS_FLAGColumn, "0");
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            { }

            return resultArgs;

        }


        public ResultArgs SaveDefaultAssetItem(string AssetClass, string AssetItem, string LedgerName, string prefix, string depreciationPercentage, string AssetAccessFlag)
        {
            // 08/04/2025, *Chinna, To Update the Asset Item too since it is 0 i update the ledger Id too
            try
            {
                ItemId = FetchByIdByAssetItem(AssetItem);
                // if (ItemId == 0)
                // {
                using (DataManager dataManager = new DataManager(ItemId == 0 ? SQLCommand.AssetItem.Add : SQLCommand.AssetItem.Update))
                {
                    AssetClassId = FetchIdByAssetClass(AssetClass);

                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.ASSET_CLASS_IDColumn, AssetClassId);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.DEPRECIATION_LEDGER_IDColumn, 1);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.DISPOSAL_LEDGER_IDColumn, 1);

                    AccountLeger = FetchIdByLedgerName(LedgerName);

                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.ACCOUNT_LEDGER_IDColumn, AccountLeger);

                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.ASSET_ITEMColumn, AssetItem);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.UOM_IDColumn, 1);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.PREFIXColumn, prefix);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.SUFFIXColumn, "");
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.RETENTION_YRSColumn, "0");
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.DEPRECIATION_YRSColumn, "0");
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.IS_INSURANCEColumn, "0");
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.IS_AMCColumn, "0");
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.IS_ASSET_DEPRECIATIONColumn, 1);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.STARTING_NOColumn, "0");
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.WIDTHColumn, "0");
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.ASSET_MODEColumn, "1");
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.DEPRECIATION_NOColumn, depreciationPercentage.Trim());
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.ASSET_ACCESS_FLAGColumn, AssetAccessFlag);

                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_IDColumn, ItemId);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.UpdateData();
                }
                // }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, false);
            }

            return resultArgs;
        }

        public DataSet FetchAssetItemMasterDetail()
        {

            string VoucherID = string.Empty;
            DataSet dsItem = new DataSet();
            try
            {
                resultArgs = FetchAssetItemDetails();
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    resultArgs.DataSource.Table.TableName = "Master";
                    dsItem.Tables.Add(resultArgs.DataSource.Table);

                    //commond by sudhakar puls button option to disable 
                    //resultArgs = FetchAssetItemDetailAll();
                    //if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    //{
                    //    resultArgs.DataSource.Table.TableName = "Item Details";
                    //    dsItem.Tables.Add(resultArgs.DataSource.Table);
                    //    // dsItem.Relations.Add(dsItem.Tables[1].TableName, dsItem.Tables[0].Columns[this.AppSchema.Project.PROJECT_IDColumn.ColumnName], dsItem.Tables[1].Columns[this.AppSchema.Project.PROJECT_IDColumn.ColumnName]);
                    //    dsItem.Relations.Add(dsItem.Tables[1].TableName, dsItem.Tables[0].Columns[this.AppSchema.ASSETItem.ITEM_IDColumn.ColumnName], dsItem.Tables[1].Columns[this.AppSchema.ASSETItem.ITEM_IDColumn.ColumnName]);
                    //}

                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, false);
            }
            finally
            {
            }
            return dsItem;
        }
        public DataTable FetchAssetItemDetail()
        {

            string VoucherID = string.Empty;
            DataTable dsItem = new DataTable();
            try
            {

                resultArgs = FetchAssetItemDetailAll();
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    dsItem = resultArgs.DataSource.Table;
                }

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, false);
            }
            finally
            {
            }
            return dsItem;
        }

        /// <summary>
        /// Fetch all the asset item details for the view form.
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchAssetItemDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.FetchAll))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            return resultArgs;
        }
        public ResultArgs FetchAssetItemDetailsLocation()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.FetchAllAssetDetails))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAssetItemDetailByItem()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.FetchAssetOPDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_IDColumn, ItemId);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATION_IDColumn, Location_id);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAssetItemDetailAll()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.FetchAssetItemDetailAll))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAssetItemDetailById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.FetchAssetItemDetailById))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_IDColumn, ItemId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAssetItemDetailByLocation(int ProjectId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.FetchAssetItemDetailByLocation))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetInOut.PROJECT_IDColumn, ProjectId);
                //if (Location > 0)
                //{
                //    dataManager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATION_IDColumn, Location);
                //}
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAssetItemDetailforTransfer()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.FetchItemDetailforTransfer))
            {
                //dataManager.Parameters.Add(this.AppSchema.ASSETItem.ASSET_GROUP_IDColumn, this.AssetGroupId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        //public ResultArgs FetchAssetItemDetailforTransferByID()
        //{
        //    using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.FetchItemDetailforTransferById))
        //    {
        //        dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_IDColumn, ItemId);
        //        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
        //        resultArgs = dataManager.FetchData(DataSource.DataTable);
        //    }
        //    return resultArgs;
        //}

        public ResultArgs FetchAssetIdDetail(int LocationId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.FetchAssetIdDetail))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATION_IDColumn, LocationId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAssetItemByGroup()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.FetchAssetItemByGroup))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETItem.ASSET_CLASS_IDColumn, AssetClassId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAssetItemByItem()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.FetchAssetIdByItem))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_IDColumn, ItemId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchActiveAssetItem()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.FetchActiveAssetItem))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchActiveAssetItemAtEdit()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.FetchAssetIdDetailsAtEdit))
            {
                dataManager.Parameters.Add(this.AppSchema.SalesAsset.SALES_IDColumn, this.SalesId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchAssetItem()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.FetchAssetItem))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetch asset item details by Id for Edit
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchAssetItemDetailsById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_IDColumn, this.ItemId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        /// <summary>
        /// Assign values to AssetItemSystem properties for edit.
        /// </summary>
        private void AssignToAssetItemPoroperties()
        {
            resultArgs = FetchAssetItemDetailsById();
            if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs.DataSource.Table != null)
            {
                AssetClassId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.ASSET_CLASS_IDColumn.ColumnName].ToString());
                AssetClass = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETClassDetails.ASSET_CLASSColumn.ColumnName].ToString();
                DepreciationLedger = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.DEPRECIATION_LEDGER_IDColumn.ColumnName].ToString());
                DisposalLedger = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.DISPOSAL_LEDGER_IDColumn.ColumnName].ToString());
                AccountLeger = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.ACCOUNT_LEDGER_IDColumn.ColumnName].ToString());
                //  Catogery = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.CATEGORY_IDColumn.ColumnName].ToString());
                Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.ASSET_ITEMColumn.ColumnName].ToString();
                Unit = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.UOM_IDColumn.ColumnName].ToString());
                UOM = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETUnitOfMeassure.SYMBOLColumn.ColumnName].ToString();
                Prefix = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.PREFIXColumn.ColumnName].ToString();
                Suffix = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.SUFFIXColumn.ColumnName].ToString();
                StartingNo = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.STARTING_NOColumn.ColumnName].ToString());
                Width = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.WIDTHColumn.ColumnName].ToString());
                RetentionYrs = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.RETENTION_YRSColumn.ColumnName].ToString());
                DepreciationYrs = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.DEPRECIATION_YRSColumn.ColumnName].ToString());
                AMCApplicable = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.IS_AMCColumn.ColumnName].ToString());
                InsuranceApplicable = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.IS_INSURANCEColumn.ColumnName].ToString());
                DepreciatonApplicable = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.IS_ASSET_DEPRECIATIONColumn.ColumnName].ToString());
                AssetItemMode = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.ASSET_MODEColumn.ColumnName].ToString());
                DepreciationNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.DEPRECIATION_NOColumn.ColumnName].ToString();

            }
        }
        /// <summary>
        /// Delete asset item details by the ItemId.
        /// </summary>
        /// <param name="ItemId"></param>
        /// <returns></returns>
        public ResultArgs DeleteAssetItem()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_IDColumn, ItemId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        /// <summary>
        /// Method for Transfer voucher
        /// </summary>
        /// <returns></returns>
        public ResultArgs UpdateAssetLocationById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.UpdateAssetItemLocationById))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_IDColumn, this.ItemId);
                dataManager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATION_IDColumn, this.Location_id);
                dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_IDColumn, this.AssetID);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchAssetItemDetailByAssetId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.FetchItemDetailbyAssetId))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_IDColumn, this.AssetID);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteAssetItems()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.DeleteAssetItems))
                {
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }
        public ResultArgs FetchProjectByAssetItem()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.FetchProjectNameByAssetItem))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_IDColumn, ItemId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAllAssetItems()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.FetchAllAssetItems))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int FetchAssetItemIdByName()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetItem.FetchAssetItemIDByName))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETItem.ASSET_ITEMColumn, Name);
                resultArgs = datamanager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int FetchClassIdByName()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetItem.FetchAssetClassIDByAssetName))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETItem.ASSET_ITEMColumn, Name);
                resultArgs = datamanager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int FetchByIdByAssetItem(string AssetItem)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.FetchAssetItemIDByName))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETItem.ASSET_ITEMColumn, AssetItem);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int FetchIdByAssetClass(string AssetClass)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetClass.FetchAssetClassNameByID))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETClassDetails.ASSET_CLASSColumn, AssetClass);
                resultArgs = datamanager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int FetchIdByLedgerName(string LedgerName)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchLedgerId))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, LedgerName);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs FetchUpdateAssetDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.FetchUpdateAssetDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int GetAccessFlag(int ItemId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.FetchAssetAccessFlag))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_IDColumn, ItemId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        #endregion Methods

        #region Fixed Asset Methods

        public ResultArgs DeleteFixedAssetItemDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.DeleteFixedAsset))
                {
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.ASSET_IDColumn, AssetID);
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        public ResultArgs DeleteFixedAssetItemDetailsbyDetailId()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.DeleteFixedAssetbyDetailId))
                {
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_DETAIL_IDColumn, ItemDetailId);
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        public ResultArgs SaveFixedAssetItemTransDetails(DataManager dmanger)
        {
            try
            {
                using (DataManager dataManager = new DataManager(ItemDetailId == 0 ? SQLCommand.AssetItem.SaveFixedAssetItemDetails : SQLCommand.AssetItem.UpdateFixedAssetItemDetails))
                {
                    dataManager.Database = dmanger.Database;
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_DETAIL_IDColumn, ItemDetailId, true);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_IDColumn, ItemId);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.ASSET_IDColumn, !string.IsNullOrEmpty(AssetID) ? AssetID : string.Empty);
                    dataManager.Parameters.Add(this.AppSchema.AssetInOut.DEPRECIATION_AMOUNTColumn, DepreciationAmount);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.AMOUNTColumn, Amount);
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.Manufactures.MANUFACTURER_IDColumn, ManufacturerId == 0 ? null : ManufacturerId.ToString());
                    dataManager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATION_IDColumn, Location_id == 0 ? null : Location_id.ToString());
                    dataManager.Parameters.Add(this.AppSchema.AssetCustodians.CUSTODIAN_IDColumn, Custodian == 0 ? null : Custodian.ToString());
                    dataManager.Parameters.Add(this.AppSchema.AMCDetails.AMC_MONTHSColumn, AMCMonths);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.STATUSColumn, Status);
                    dataManager.Parameters.Add(this.AppSchema.AssetInOut.SALVAGE_VALUEColumn, SalvageValue);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.CONDITIONSColumn, !string.IsNullOrEmpty(Condition) ? Condition : string.Empty);
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }



        public ResultArgs SaveFixedAssetInsuranceDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.UpdateFixedAssetInsuranceItemDetails))
                {
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_DETAIL_IDColumn, ItemDetailId, true);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.ASSET_IDColumn, !string.IsNullOrEmpty(AssetID) ? AssetID : string.Empty);
                    // dataManager.Parameters.Add(this.AppSchema.InsurancePlan.INSURANCE_PLAN_IDColumn, InsurancePlanId);
                    //dataManager.Parameters.Add(this.AppSchema.ASSETItem.POLICY_NOColumn, !string.IsNullOrEmpty(PolicyNO) ? PolicyNO : string.Empty);
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        public ResultArgs FetchAMCAssetItems()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.FetchAMCAssetItems))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public int FetchInoutItemByItemId(int Itemid)
        {

            using (DataManager datamanager = new DataManager(SQLCommand.AssetItem.FetchInoutIdByItemId))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_IDColumn, Itemid);
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        #endregion

    }
}

