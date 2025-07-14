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
using Bosco.Model.Transaction;

namespace Bosco.Model
{
    public class AssetInwardVoucherSystem : SystemBase
    {
        #region Variable Decelaration
        ResultArgs resultArgs = new ResultArgs();
        DataTable dtAssetItemDetail = new DataTable("AssetItemDetail");
        #endregion

        #region constructor
        public AssetInwardVoucherSystem()
        {
        }
        public AssetInwardVoucherSystem(int purchaseId, int sourceFlag)
        {
            PurchaseId = purchaseId;
            SourceFlag = sourceFlag;
            FillPurchaseVoucherProperties(purchaseId);
        }
        #endregion

        #region PurchaseVoucher Properties
        public int PurchaseId { get; set; }
        public int ProjectId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int VendorId { get; set; }
        public int GroupId { get; set; }
        public int ItemId { get; set; }
        public int LocationId { get; set; }
        public int VoucherId { get; set; }
        public int CashLedgerId { get; set; }
        public int BranchId { get; set; }
        public int Quantity { get; set; }
        public int ManufactureId { get; set; }
        public int SourceFlag { get; set; }
        public string VoucherNo { get; set; }
        public string NameAddress { get; set; }
        public string Narration { get; set; }


        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal UsefulLife { get; set; }
        public decimal SalvageLife { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TaxPercent { get; set; }
        public decimal OtherCharges { get; set; }
        public decimal NetAmount { get; set; }
        public decimal TotalAmount { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public string BillNo { get; set; }
        public string InvoiceNo { get; set; }
        public DataTable dtPurchaseDetail { get; set; }
        #endregion

        #region Purchase Methods

        public ResultArgs SaveAssetPurchase()
        {
            using (DataManager datamanager = new DataManager())
            {
                resultArgs.Success = true;
                datamanager.BeginTransaction();
                resultArgs = SaveVoucherDetails(datamanager);
                if (resultArgs.Success)
                {
                    if (PurchaseId > 0)
                    {
                        resultArgs = DeleteItemDetailByPurchase();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            //Delete purchase detail in edit mode
                            resultArgs = DeletePurchaseDetail(PurchaseId);
                        }
                    }
                    resultArgs = SavePurchaseVoucherMaster();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        PurchaseId = PurchaseId == 0 ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : PurchaseId;
                        resultArgs = SavePurchaseVoucherDetail();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            resultArgs = SaveAssetItems();
                        }
                    }
                }
                datamanager.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs SaveVoucherDetails(DataManager dm)
        {
            if (SourceFlag == (int)AssetSourceFlag.Purchase)
            {
                try
                {
                    using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                    {
                        voucherTransaction.VoucherId = VoucherId;
                        voucherTransaction.ProjectId = ProjectId;
                        voucherTransaction.TransVoucherMethod = (int)TransactionVoucherMethod.Automatic;
                        voucherTransaction.VoucherType = DefaultVoucherTypes.Payment.ToString();
                        voucherTransaction.VoucherDate = PurchaseDate;
                        voucherTransaction.VoucherSubType = VoucherSubTypes.AST.ToString();
                        voucherTransaction.NameAddress = NameAddress;
                        voucherTransaction.Narration = Narration;
                        voucherTransaction.Status = 1;

                        resultArgs = ConstructTransactionData();
                        if (resultArgs.Success)
                        {
                            this.TransInfo = resultArgs.DataSource.Table.DefaultView;

                            resultArgs = voucherTransaction.ConstructVoucherData(CashLedgerId, NetAmount);

                            if (resultArgs.Success)
                            {
                                this.CashTransInfo = resultArgs.DataSource.Table.DefaultView;

                                resultArgs = voucherTransaction.SaveVoucherDetails(dm);
                                if (resultArgs.Success)
                                {
                                    VoucherId = voucherTransaction.VoucherId;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    resultArgs.Message = ex.ToString();
                }
            }
            return resultArgs;
        }

        private ResultArgs ConstructTransactionData()
        {
            int ItemId = 0;
            try
            {
                if (dtPurchaseDetail != null && dtPurchaseDetail.Rows.Count > 0)
                {
                    dtPurchaseDetail.Columns.Add(this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName, typeof(Int32));
                    foreach (DataRow drAssetItem in dtPurchaseDetail.Rows)
                    {
                        ItemId = this.NumberSet.ToInteger(drAssetItem[this.AppSchema.ASSETItem.ITEM_IDColumn.ColumnName].ToString());
                        if (ItemId > 0)
                        {
                            using (AssetItemSystem assetItemSystem = new AssetItemSystem(ItemId))
                            {
                                drAssetItem[this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName] = assetItemSystem.AccountLeger;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                resultArgs.DataSource.Data = dtPurchaseDetail;
            }
            return resultArgs;
        }

        private ResultArgs SavePurchaseVoucherMaster()
        {
            using (DataManager datamanager = new DataManager((PurchaseId == 0) ? SQLCommand.AssetPurchaseVoucher.AddPurchaseMaster : SQLCommand.AssetPurchaseVoucher.UpdatePurchaseMaster))
            {
                datamanager.Parameters.Add(this.AppSchema.AssetPurchaseMaster.PURCHASE_IDColumn, PurchaseId, true);
                datamanager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                datamanager.Parameters.Add(this.AppSchema.AssetPurchaseMaster.PURCHASE_DATEColumn, PurchaseDate);
                datamanager.Parameters.Add(this.AppSchema.AssetPurchaseMaster.VENDOR_IDColumn, VendorId);
                datamanager.Parameters.Add(this.AppSchema.Manufactures.MANUFACTURER_IDColumn, ManufactureId);
                datamanager.Parameters.Add(this.AppSchema.AssetPurchaseMaster.BILL_NOColumn, BillNo);
                datamanager.Parameters.Add(this.AppSchema.AssetPurchaseMaster.INVOICE_NOColumn, InvoiceNo);
                datamanager.Parameters.Add(this.AppSchema.AssetPurchaseMaster.NET_AMOUNTColumn, NetAmount);
                datamanager.Parameters.Add(this.AppSchema.AssetPurchaseMaster.VOUCHER_IDColumn, VoucherId);
                datamanager.Parameters.Add(this.AppSchema.AssetPurchaseMaster.DISCOUNT_PERColumn, DiscountPercent);
                datamanager.Parameters.Add(this.AppSchema.AssetPurchaseMaster.DISCOUNTColumn, Discount);
                datamanager.Parameters.Add(this.AppSchema.AssetPurchaseMaster.TAX_AMOUNTColumn, TaxAmount);
                datamanager.Parameters.Add(this.AppSchema.AssetPurchaseMaster.TAX_PERColumn, TaxPercent);
                datamanager.Parameters.Add(this.AppSchema.AssetPurchaseMaster.OTHER_CHARGESColumn, OtherCharges);
                datamanager.Parameters.Add(this.AppSchema.AssetPurchaseMaster.BRANCH_IDColumn, BranchId);
                datamanager.Parameters.Add(this.AppSchema.AssetPurchaseMaster.TOTAL_AMOUNTColumn, TotalAmount);
                //datamanager.Parameters.Add(this.AppSchema.ASSETItem.SOURCE_FLAGColumn, SourceFlag);
                datamanager.Parameters.Add(this.AppSchema.InsuranceMasterData.NAME_ADDRESSColumn, NameAddress);
                datamanager.Parameters.Add(this.AppSchema.InsuranceMasterData.NARRATIONColumn, Narration);

                resultArgs = datamanager.UpdateData();

            }
            return resultArgs;
        }

        private ResultArgs SavePurchaseVoucherDetail()
        {
            foreach (DataRow drPurchase in dtPurchaseDetail.Rows)
            {
                using (DataManager datamanager = new DataManager(SQLCommand.AssetPurchaseVoucher.AddPurchaseDetail))
                {
                    datamanager.Parameters.Add(this.AppSchema.AssetPurchaseMaster.PURCHASE_IDColumn, PurchaseId);
                    datamanager.Parameters.Add(this.AppSchema.AssetPurchaseDetail.GROUP_IDColumn, (drPurchase[this.AppSchema.AssetPurchaseDetail.GROUP_IDColumn.ColumnName] != null) ? this.NumberSet.ToInteger(drPurchase[this.AppSchema.AssetPurchaseDetail.GROUP_IDColumn.ColumnName].ToString()) : 0);
                    datamanager.Parameters.Add(this.AppSchema.AssetPurchaseDetail.ITEM_IDColumn, drPurchase[this.AppSchema.AssetPurchaseDetail.ITEM_IDColumn.ColumnName] != null ? this.NumberSet.ToInteger(drPurchase[this.AppSchema.AssetPurchaseDetail.ITEM_IDColumn.ColumnName].ToString()) : 0);
                    datamanager.Parameters.Add(this.AppSchema.AssetPurchaseDetail.LOCATION_IDColumn, drPurchase[this.AppSchema.AssetPurchaseDetail.LOCATION_IDColumn.ColumnName] != null ? this.NumberSet.ToInteger(drPurchase[this.AppSchema.AssetPurchaseDetail.LOCATION_IDColumn.ColumnName].ToString()) : 0);
                    datamanager.Parameters.Add(this.AppSchema.AssetPurchaseDetail.QUANTITYColumn, drPurchase[this.AppSchema.AssetPurchaseDetail.QUANTITYColumn.ColumnName] != null ? this.NumberSet.ToInteger(drPurchase[this.AppSchema.AssetPurchaseDetail.QUANTITYColumn.ColumnName].ToString()) : 0);
                    datamanager.Parameters.Add(this.AppSchema.AssetPurchaseDetail.RATEColumn, drPurchase[this.AppSchema.AssetPurchaseDetail.RATEColumn.ColumnName] != null ? this.NumberSet.ToDecimal(drPurchase[this.AppSchema.AssetPurchaseDetail.RATEColumn.ColumnName].ToString()) : 0);
                    datamanager.Parameters.Add(this.AppSchema.AssetPurchaseDetail.AMOUNTColumn, drPurchase[this.AppSchema.AssetPurchaseDetail.AMOUNTColumn.ColumnName] != null ? this.NumberSet.ToDecimal(drPurchase[this.AppSchema.AssetPurchaseDetail.AMOUNTColumn.ColumnName].ToString()) : 0);
                    datamanager.Parameters.Add(this.AppSchema.AssetPurchaseDetail.USEFUL_LIFEColumn, drPurchase[this.AppSchema.AssetPurchaseDetail.USEFUL_LIFEColumn.ColumnName] != null ? this.NumberSet.ToDecimal(drPurchase[this.AppSchema.AssetPurchaseDetail.USEFUL_LIFEColumn.ColumnName].ToString()) : 0);
                    datamanager.Parameters.Add(this.AppSchema.AssetPurchaseDetail.SALVAGE_LIFEColumn, drPurchase[this.AppSchema.AssetPurchaseDetail.SALVAGE_LIFEColumn.ColumnName] != null ? this.NumberSet.ToDecimal(drPurchase[this.AppSchema.AssetPurchaseDetail.SALVAGE_LIFEColumn.ColumnName].ToString()) : 0);
                    resultArgs = datamanager.UpdateData();
                }
            }

            return resultArgs;
        }

        private void FillPurchaseVoucherProperties(int purchaseId)
        {
            resultArgs = this.SourceFlag == (int)AssetSourceFlag.Purchase ? FetchById(purchaseId) : FetchReceiveById(purchaseId);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                PurchaseId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetPurchaseMaster.PURCHASE_IDColumn.ColumnName].ToString());
                PurchaseDate = DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetPurchaseMaster.PURCHASE_DATEColumn.ColumnName].ToString(), false);
                VendorId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetPurchaseMaster.VENDOR_IDColumn.ColumnName].ToString());
                VoucherId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetPurchaseMaster.VOUCHER_IDColumn.ColumnName].ToString());
                BranchId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetPurchaseMaster.BRANCH_IDColumn.ColumnName].ToString());
                BillNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetPurchaseMaster.BILL_NOColumn.ColumnName].ToString();
                ManufactureId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Manufactures.MANUFACTURER_IDColumn.ColumnName].ToString());
                InvoiceNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetPurchaseMaster.INVOICE_NOColumn.ColumnName].ToString();
                NetAmount = NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetPurchaseMaster.NET_AMOUNTColumn.ColumnName].ToString());
                OtherCharges = NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetPurchaseMaster.OTHER_CHARGESColumn.ColumnName].ToString());
                Discount = NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetPurchaseMaster.DISCOUNTColumn.ColumnName].ToString());
                DiscountPercent = NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetPurchaseMaster.DISCOUNT_PERColumn.ColumnName].ToString());
                TaxAmount = NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetPurchaseMaster.TAX_AMOUNTColumn.ColumnName].ToString());
                TaxPercent = NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetPurchaseMaster.TAX_PERColumn.ColumnName].ToString());
                TotalAmount = NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetPurchaseMaster.TOTAL_AMOUNTColumn.ColumnName].ToString());
                //SourceFlag = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.SOURCE_FLAGColumn.ColumnName].ToString());
                VoucherId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetPurchaseMaster.VOUCHER_IDColumn.ColumnName].ToString());
                NameAddress = resultArgs.DataSource.Table.Rows[0][this.AppSchema.InsuranceMasterData.NAME_ADDRESSColumn.ColumnName].ToString();
                Narration = resultArgs.DataSource.Table.Rows[0][this.AppSchema.InsuranceMasterData.NARRATIONColumn.ColumnName].ToString();
                VoucherNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_NOColumn.ColumnName].ToString();
                CashLedgerId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
            }
        }

        public ResultArgs DeleteAssetPurchase(int purchaseId)
        {
            try
            {
                resultArgs.Success = true;
                using (DataManager datamanager = new DataManager())
                {
                    datamanager.BeginTransaction();
                    PurchaseId = purchaseId;

                    FillPurchaseVoucherProperties(PurchaseId);
                    using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                    {
                        if (SourceFlag == (int)AssetSourceFlag.Purchase)
                        {
                            voucherTransaction.VoucherId = VoucherId;
                            resultArgs = voucherTransaction.RemoveAssetStockVoucher();
                        }

                        if (resultArgs.Success)
                        {
                            resultArgs = DeleteItemDetailByPurchase();
                            if (resultArgs != null && resultArgs.Success)
                            {
                                resultArgs = DeletePurchaseDetail(purchaseId);
                                if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                                {
                                    resultArgs = DeletePurchaseMaster(purchaseId);
                                }
                            }
                        }
                    }
                    datamanager.EndTransaction();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);

            }
            finally
            {
            }
            return resultArgs;
        }

        public int GetPurchaseId(int VoucherId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetPurchaseVoucher.FetchPurchaseIdByVoucherId))
            {
                dataManager.Parameters.Add(AppSchema.Voucher.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        private ResultArgs DeletePurchaseMaster(int purchaseId)
        {
            try
            {
                using (DataManager datamanager = new DataManager(SQLCommand.AssetPurchaseVoucher.DeletePurchaseMaster))
                {
                    datamanager.Parameters.Add(this.AppSchema.AssetPurchaseMaster.PURCHASE_IDColumn, purchaseId);
                    resultArgs = datamanager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }

            return resultArgs;
        }

        private ResultArgs DeletePurchaseDetail(int purchaseId)
        {
            try
            {
                using (DataManager datamanager = new DataManager(SQLCommand.AssetPurchaseVoucher.DeletePurchaseDetail))
                {

                    datamanager.Parameters.Add(this.AppSchema.AssetPurchaseMaster.PURCHASE_IDColumn, purchaseId);
                    resultArgs = datamanager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally
            {
            }
            return resultArgs;
        }

        private ResultArgs FetchPurchaseMasters()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetPurchaseVoucher.FetchAll))
            {
                datamanager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, this.ProjectId);
                datamanager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, this.FromDate);
                datamanager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, this.ToDate);
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchPurchaseDetails(string AssPurchaseId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetPurchaseVoucher.FetchPurchaseDetail))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetPurchaseMaster.PURCHASE_IDColumn, AssPurchaseId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public DataSet FetchAssetPurchaseDetails()
        {
            DataSet dsAssetPurchase = new DataSet();
            string PurchaseId = string.Empty;
            try
            {
                resultArgs = FetchPurchaseMasters();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null
                    && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    resultArgs.DataSource.Table.TableName = "Master";
                    dsAssetPurchase.Tables.Add(resultArgs.DataSource.Table);

                    foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                    {
                        PurchaseId += dr[this.AppSchema.AssetPurchaseMaster.PURCHASE_IDColumn.ColumnName].ToString() + ",";
                    }
                    PurchaseId = PurchaseId.TrimEnd(',');

                    resultArgs = FetchPurchaseDetails(PurchaseId);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        resultArgs.DataSource.Table.TableName = "Purchase";
                        dsAssetPurchase.Tables.Add(resultArgs.DataSource.Table);
                    }
                    dsAssetPurchase.Relations.Add(dsAssetPurchase.Tables[1].TableName, dsAssetPurchase.Tables[0].Columns[this.AppSchema.AssetPurchaseMaster.PURCHASE_IDColumn.ColumnName], dsAssetPurchase.Tables[1].Columns[this.AppSchema.AssetPurchaseMaster.PURCHASE_IDColumn.ColumnName]);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
            return dsAssetPurchase;
        }

        private ResultArgs FetchById(int purchaseId)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetPurchaseVoucher.FetchPurchaseMaster))
            {
                datamanager.Parameters.Add(this.AppSchema.AssetPurchaseMaster.PURCHASE_IDColumn, purchaseId);
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        private ResultArgs FetchReceiveById(int purchaseId)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetPurchaseVoucher.FetchReceiveMasterByID))
            {
                datamanager.Parameters.Add(this.AppSchema.AssetPurchaseMaster.PURCHASE_IDColumn, purchaseId);
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs SaveAssetItems()
        {
            try
            {
                using (AssetItemSystem itemSystem = new AssetItemSystem())
                {
                    foreach (DataRow drPurchase in dtPurchaseDetail.Rows)
                    {
                        itemSystem.ItemId = drPurchase[this.AppSchema.AssetPurchaseDetail.ITEM_IDColumn.ColumnName] != null ?
                              this.NumberSet.ToInteger(drPurchase[this.AppSchema.AssetPurchaseDetail.ITEM_IDColumn.ColumnName].ToString()) : 0;
                        itemSystem.ProjectId = ProjectId;
                        int quantity = drPurchase[this.AppSchema.AssetPurchaseDetail.QUANTITYColumn.ColumnName] != null ? this.NumberSet.ToInteger(drPurchase[this.AppSchema.AssetPurchaseDetail.QUANTITYColumn.ColumnName].ToString()) : 0;
                        itemSystem.Location_id = drPurchase[this.AppSchema.AssetPurchaseDetail.LOCATION_IDColumn.ColumnName] != null ?
                               this.NumberSet.ToInteger(drPurchase[this.AppSchema.AssetPurchaseDetail.LOCATION_IDColumn.ColumnName].ToString()) : 0;
                        itemSystem.Custodian = drPurchase[this.AppSchema.AssetCustodians.CUSTODIAN_IDColumn.ColumnName] != null ?
                            this.NumberSet.ToInteger(drPurchase[this.AppSchema.AssetCustodians.CUSTODIAN_IDColumn.ColumnName].ToString()) : 0;
                        itemSystem.PurchaseId = PurchaseId;
                        itemSystem.SalesId = 0;
                        itemSystem.Amount = drPurchase[this.AppSchema.AssetPurchaseDetail.RATEColumn.ColumnName] != null ?
                            this.NumberSet.ToDecimal(drPurchase[this.AppSchema.AssetPurchaseDetail.RATEColumn.ColumnName].ToString()) : 0;
                        itemSystem.SourceFlag = SourceFlag;
                        itemSystem.UsefulLife = drPurchase[this.AppSchema.AssetPurchaseDetail.USEFUL_LIFEColumn.ColumnName] != null ?
                            this.NumberSet.ToDecimal(drPurchase[this.AppSchema.AssetPurchaseDetail.USEFUL_LIFEColumn.ColumnName].ToString()) : 0;
                        itemSystem.SalvageLife = drPurchase[this.AppSchema.AssetPurchaseDetail.SALVAGE_LIFEColumn.ColumnName] != null ?
                            this.NumberSet.ToDecimal(drPurchase[this.AppSchema.AssetPurchaseDetail.SALVAGE_LIFEColumn.ColumnName].ToString()) : 0;
                        for (int i = 0; i < quantity; i++)
                        {
                            itemSystem.AssetID = string.Empty;
                            resultArgs = itemSystem.SaveAssetItemDetail();
                        }
                        //Regenearte AssetId by ItemId
                        using (NumberSystem numberSystem = new NumberSystem())
                        {
                            resultArgs = numberSystem.RegenerateAssetID(drPurchase[this.AppSchema.AssetPurchaseDetail.ITEM_IDColumn.ColumnName] != null ? this.NumberSet.ToInteger(drPurchase[this.AppSchema.AssetPurchaseDetail.ITEM_IDColumn.ColumnName].ToString()) : 0);
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            return resultArgs;
        }

        private ResultArgs DeleteItemDetailByPurchase()
        {
            try
            {
                using (AssetItemSystem itemSystem = new AssetItemSystem())
                {
                    //Delete Items by Purchase
                    itemSystem.PurchaseId = PurchaseId;
                    resultArgs = itemSystem.DeleteAssetItemDetailByPurchase();
                }

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }

            return resultArgs;
        }

        public ResultArgs FetchSourceFlagbyPurchaseId()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetPurchaseVoucher.FetchAssetSourceFlagById))
            {
                datamanager.Parameters.Add(this.AppSchema.AssetPurchaseMaster.PURCHASE_IDColumn, PurchaseId);
                resultArgs = datamanager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        #endregion

        #region Receive Method
        public DataSet FetchAssetReceiveDetails()
        {
            DataSet dsAssetReceive = new DataSet();
            string ReceiveId = string.Empty;
            try
            {
                resultArgs = FetchReceiveMasters();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null
                    && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    resultArgs.DataSource.Table.TableName = "Master";
                    dsAssetReceive.Tables.Add(resultArgs.DataSource.Table);

                    foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                    {
                        ReceiveId += dr[this.AppSchema.AssetPurchaseMaster.PURCHASE_IDColumn.ColumnName].ToString() + ",";
                    }
                    ReceiveId = ReceiveId.TrimEnd(',');

                    resultArgs = FetchPurchaseDetails(ReceiveId);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        resultArgs.DataSource.Table.TableName = "Receive";
                        dsAssetReceive.Tables.Add(resultArgs.DataSource.Table);
                    }
                    dsAssetReceive.Relations.Add(dsAssetReceive.Tables[1].TableName, dsAssetReceive.Tables[0].Columns[this.AppSchema.AssetPurchaseMaster.PURCHASE_IDColumn.ColumnName], dsAssetReceive.Tables[1].Columns[this.AppSchema.AssetPurchaseMaster.PURCHASE_IDColumn.ColumnName]);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
            return dsAssetReceive;
        }

        private ResultArgs FetchReceiveMasters()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetPurchaseVoucher.FetchReceiveMaster))
            {
                datamanager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, FromDate);
                datamanager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, ToDate);
                datamanager.Parameters.Add(this.AppSchema.AssetPurchaseMaster.PROJECT_IDColumn, ProjectId);
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        #endregion
    }
}


