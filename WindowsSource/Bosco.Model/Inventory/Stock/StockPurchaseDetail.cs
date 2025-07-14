using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.Utility;
using Bosco.DAO;
using Bosco.DAO.Schema;
using Bosco.DAO.Data;
using System.Data;
using Bosco.Model.Transaction;


namespace Bosco.Model.Inventory.Stock
{
    public class StockPurchaseDetail : SystemBase
    {
        #region Constructor
        public StockPurchaseDetail()
        {

        }

        public StockPurchaseDetail(int PurchaseId)
        {
            this.PurchaseId = PurchaseId;
            FillStockProperties();
        }
        #endregion

        #region Variables
        #region Common Variables
        ResultArgs resultArgs = new ResultArgs();
        private string StockPurchaseId { get; set; }
        public int ProjectId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        #endregion

        #region Purchase Masters
        public int PurchaseId { get; set; }
        public int VendorId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string VoucherNo { get; set; }
        public string PurchaseOrderNo { get; set; }
        public decimal Discount { get; set; }
        public decimal OtherCharges { get; set; }
        public decimal Tax { get; set; }
        public decimal NetAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountPer { get; set; }
        public int LedgerId { get; set; }
        public string NameandAddress { get; set; }
        public string Narration { get; set; }
        public int PurchaseFlag { get; set; }
        public int BranchId { get; set; }
        public int VoucherId { get; set; }
        public DataTable dtUpdateStockDetails { get; set; }
        #endregion

        #region Puchase Detail
        public int ItemId { get; set; }
        public int LocationId { get; set; }
        public int Quantity { get; set; }
        public int TransQty { get; set; }
        public int PurchaseMode { get; set; }
        public decimal UnitPrice { get; set; }
        public DataTable dtStockPurchaseDetail { get; set; }
        public bool isEditMode = false;
        public int PurchaseAccountLedgerId { get; set; }

        #endregion

        #endregion

        #region Common Methods

        public ResultArgs SavePurchase()
        {
            using (DataManager dataManager = new DataManager())
            {
                resultArgs.Success = true;
                dataManager.BeginTransaction();
                // resultArgs = SaveVoucherDetails(dataManager);
                if (resultArgs.Success)
                {
                    resultArgs = SaveStockPurchaseMasters();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        if (PurchaseId > 0)
                        {
                            isEditMode = true;
                            using (StockBalanceSystem stockbalancesystem = new StockBalanceSystem())
                            {
                                resultArgs = stockbalancesystem.UpdatestockBalance(PurchaseId, StockType.Purchase, TransactionAction.EditBeforeSave);
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    resultArgs = DeleteStockPurchaseDetail();
                                }
                            }
                        }
                        if (resultArgs != null && resultArgs.Success)
                        {
                            PurchaseId = PurchaseId.Equals(0) && resultArgs.RowUniqueId != null ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : PurchaseId;

                            resultArgs = SaveStockPurchaseDetails();
                            if (resultArgs != null && resultArgs.Success)
                            {
                                using (StockBalanceSystem stockbalancesystem = new StockBalanceSystem())
                                {
                                    resultArgs = stockbalancesystem.UpdatestockBalance(PurchaseId, StockType.Purchase, TransactionAction.New);
                                }
                            }
                        }
                    }
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs SaveVoucherDetails(DataManager dm)
        {
            try
            {
                foreach (DataRow dr in dtStockPurchaseDetail.Rows)
                {
                    if (PurchaseFlag == (int)StockPurchaseTransType.Purchase)
                    {
                        using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                        {
                            voucherTransaction.VoucherId = VoucherId;
                            voucherTransaction.ProjectId = ProjectId;
                            voucherTransaction.TransVoucherMethod = (int)TransactionVoucherMethod.Automatic;
                            voucherTransaction.VoucherType = DefaultVoucherTypes.Payment.ToString();
                            voucherTransaction.VoucherDate = PurchaseDate;
                            voucherTransaction.VoucherSubType = VoucherSubTypes.STK.ToString();
                            voucherTransaction.NameAddress = NameandAddress;
                            voucherTransaction.Narration = Narration;
                            voucherTransaction.Status = 1;
                            PurchaseAccountLedgerId = this.NumberSet.ToInteger(dr.Table.Rows[0][this.AppSchema.ASSETItem.ACCOUNT_LEDGER_IDColumn.ColumnName].ToString());
                            //using (StockLedgerMapping stockledgermapping = new StockLedgerMapping())
                            //{
                            //    PurchaseAccountLedgerId = stockledgermapping.AccountLedgerId;
                            //}
                            if (PurchaseAccountLedgerId > 0)
                            {
                                resultArgs = voucherTransaction.ConstructVoucherData(PurchaseAccountLedgerId, NetAmount);
                                if (resultArgs.Success)
                                {
                                    this.TransInfo = resultArgs.DataSource.Table.DefaultView;

                                    resultArgs = voucherTransaction.ConstructVoucherData(LedgerId, NetAmount);

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
                            else
                            {
                                //resultArgs.Message = "Stock Account Ledger / Stock Disposal Ledger is not mapped";
                                MessageRender.ShowMessage("Stock Income Ledger / Stock Expense Ledger is not mapped");
                                resultArgs.Success = false;
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

        //private ResultArgs ConstructTransactionData()
        //{
        //    int ItemId = 0;
        //    try
        //    {
        //        if (dtStockPurchaseDetail != null && dtStockPurchaseDetail.Rows.Count > 0)
        //        {
        //            dtStockPurchaseDetail.Columns.Add(this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName, typeof(Int32));
        //            foreach (DataRow drAssetItem in dtStockPurchaseDetail.Rows)
        //            {
        //                ItemId = this.NumberSet.ToInteger(drAssetItem[this.AppSchema.ASSETItem.ITEM_IDColumn.ColumnName].ToString());
        //                if (ItemId > 0)
        //                {
        //                    using (StockLedgerMapping stockledgermapping = new StockLedgerMapping())
        //                    {
        //                        drAssetItem[this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName] = stockledgermapping.AccountLedgerId;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        resultArgs.Message = ex.ToString();
        //    }

        //    if (resultArgs.Success)
        //    {
        //        resultArgs.DataSource.Data = dtStockPurchaseDetail;
        //    }
        //    return resultArgs;
        //}


        public DataSet FetchStockDetail()
        {
            DataSet dsStockPurchase = new DataSet();

            using (DataManager dataManager = new DataManager())
            {
                resultArgs = FetchStockPurchaseMasters();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    resultArgs.DataSource.Table.TableName = "StockMasters";
                    dsStockPurchase.Tables.Add(resultArgs.DataSource.Table);
                    foreach (DataRow drPurchase in resultArgs.DataSource.Table.Rows)
                    {
                        StockPurchaseId += drPurchase[this.AppSchema.StockMasterPurchase.PURCHASE_IDColumn.ColumnName].ToString() + ",";
                    }
                    StockPurchaseId = StockPurchaseId.TrimEnd(',');
                    if (!string.IsNullOrEmpty(StockPurchaseId))
                    {
                        resultArgs = FetchStockPurchaseDetails();
                        if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            resultArgs.DataSource.Table.TableName = "StockPurchaseDetail";
                            dsStockPurchase.Tables.Add(resultArgs.DataSource.Table);

                            dsStockPurchase.Relations.Add(dsStockPurchase.Tables[1].TableName, dsStockPurchase.Tables[0].Columns[this.AppSchema.StockMasterPurchase.PURCHASE_IDColumn.ColumnName], dsStockPurchase.Tables[1].Columns[this.AppSchema.StockMasterPurchase.PURCHASE_IDColumn.ColumnName]);
                        }
                    }
                }
            }
            return dsStockPurchase;
        }

        public ResultArgs DeleteStock()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                FillStockProperties();
                using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                {
                    voucherTransaction.VoucherId = VoucherId;
                    resultArgs = voucherTransaction.RemoveAssetStockVoucher();
                }
                if (resultArgs != null && resultArgs.Success)
                {
                    resultArgs = FetchStockDetailsBeforeDelete();
                    dtUpdateStockDetails = resultArgs.DataSource.Table;
                    resultArgs = DeleteStockPurchaseDetail();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        resultArgs = DeleteStockPurchaseMaster();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            resultArgs = UpdatePurchaseDetailsAfterDelete();
                        }
                    }
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public int GetPurchaseId(int VoucherId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockPurchaseDetail.FetchPurchaseIdByVoucherId))
            {
                dataManager.Parameters.Add(AppSchema.Voucher.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }


        public ResultArgs FillStockProperties()
        {
            using (DataManager dataManager = new DataManager())
            {
                dtStockPurchaseDetail = FetchStockPurchaseDetailById();

                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    resultArgs = FetchStockPurchaseMasterById();
                    foreach (DataRow drPurchaseMaster in resultArgs.DataSource.Table.Rows)
                    {
                        PurchaseId = this.NumberSet.ToInteger(drPurchaseMaster[this.AppSchema.StockMasterPurchase.PURCHASE_IDColumn.ColumnName].ToString());
                        PurchaseDate = this.DateSet.ToDate(drPurchaseMaster[this.AppSchema.StockMasterPurchase.PURCHASE_DATEColumn.ColumnName].ToString(), false);
                        VoucherNo = drPurchaseMaster[this.AppSchema.StockMasterPurchase.VOUCHER_NOColumn.ColumnName].ToString();
                        PurchaseOrderNo = drPurchaseMaster[this.AppSchema.StockMasterPurchase.PURCHASE_ORDER_NOColumn.ColumnName].ToString();
                        VendorId = this.NumberSet.ToInteger(drPurchaseMaster[this.AppSchema.StockMasterPurchase.VENDOR_IDColumn.ColumnName].ToString());
                        Discount = this.NumberSet.ToDecimal(drPurchaseMaster[this.AppSchema.StockMasterPurchase.DISCOUNTColumn.ColumnName].ToString());
                        OtherCharges = this.NumberSet.ToDecimal(drPurchaseMaster[this.AppSchema.StockMasterPurchase.OTHER_CHARGESColumn.ColumnName].ToString());
                        Tax = this.NumberSet.ToDecimal(drPurchaseMaster[this.AppSchema.StockMasterPurchase.TAXColumn.ColumnName].ToString());
                        DiscountPer = this.NumberSet.ToDecimal(drPurchaseMaster[this.AppSchema.AssetPurchaseMaster.DISCOUNT_PERColumn.ColumnName].ToString());
                        NetAmount = this.NumberSet.ToDecimal(drPurchaseMaster[this.AppSchema.StockMasterPurchase.NET_PAYColumn.ColumnName].ToString());
                        TaxAmount = this.NumberSet.ToDecimal(drPurchaseMaster[this.AppSchema.StockMasterPurchase.TAX_AMOUNTColumn.ColumnName].ToString());
                        NameandAddress = drPurchaseMaster[this.AppSchema.StockMasterPurchase.NAME_ADDRESSColumn.ColumnName].ToString();
                        Narration = drPurchaseMaster[this.AppSchema.StockMasterPurchase.NARRATIONColumn.ColumnName].ToString();
                        LedgerId = this.NumberSet.ToInteger(drPurchaseMaster[this.AppSchema.StockMasterPurchase.LEDGER_IDColumn.ColumnName].ToString());
                        PurchaseFlag = this.NumberSet.ToInteger(drPurchaseMaster[this.AppSchema.StockMasterSales.TRANS_TYPEColumn.ColumnName].ToString());
                        VoucherId = this.NumberSet.ToInteger(drPurchaseMaster[this.AppSchema.VoucherTransaction.VOUCHER_IDColumn.ColumnName].ToString());
                    }
                }
            }
            return resultArgs;
        }

        private DataTable filterPurchaseDetails()
        {
            DataTable dtPurchaseFilter = new DataTable();
            if (dtStockPurchaseDetail != null && dtStockPurchaseDetail.Rows.Count > 0)
            {
                DataView dvPurchase = dtStockPurchaseDetail.DefaultView;
                dvPurchase.RowFilter = "ITEM_ID>0";
                dtPurchaseFilter = dvPurchase.ToTable();
                dvPurchase.RowFilter = "";
            }
            return dtPurchaseFilter;
        }

        public ResultArgs FetchStockDetailsBeforeDelete()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockPurchaseDetail.FetchPurchaseById))
            {
                dataManager.Parameters.Add(this.AppSchema.StockPurchaseDetails.PURCHASE_IDColumn, PurchaseId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs UpdatePurchaseDetailsAfterDelete()
        {
            if (dtUpdateStockDetails != null && dtUpdateStockDetails.Rows.Count > 0)
            {
                foreach (DataRow drPurchase in dtUpdateStockDetails.Rows)
                {
                    using (StockBalanceSystem BalanceSystem = new StockBalanceSystem())
                    {
                        ProjectId = NumberSet.ToInteger(drPurchase[this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString());
                        PurchaseDate = DateSet.ToDate(drPurchase[this.AppSchema.StockBalance.BALANCE_DATEColumn.ColumnName].ToString(), false);
                        LocationId = NumberSet.ToInteger(drPurchase[this.AppSchema.StockPurchaseDetails.LOCATION_IDColumn.ColumnName].ToString());
                        ItemId = NumberSet.ToInteger(drPurchase[this.AppSchema.StockPurchaseDetails.ITEM_IDColumn.ColumnName].ToString());
                        Quantity = NumberSet.ToInteger(drPurchase[this.AppSchema.StockPurchaseDetails.QUANTITYColumn.ColumnName].ToString());
                        UnitPrice = NumberSet.ToDecimal(drPurchase[this.AppSchema.StockPurchaseDetails.UNIT_PRICEColumn.ColumnName].ToString());
                        resultArgs = BalanceSystem.UpdateStockDetails(ProjectId, PurchaseDate, LocationId, ItemId, Quantity, UnitPrice, (int)StockReturnType.OutWards);
                        if (resultArgs != null && !resultArgs.Success) break;
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgerIdByItem(int itemId)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.StockMasterPurchase.FetchLedgerId))
            {
                datamanager.Parameters.Add(this.AppSchema.StockItem.ITEM_IDColumn, itemId);
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion

        #region Private Methods

        #region Purchase Masters

        private ResultArgs FetchStockPurchaseMasters()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockMasterPurchase.FetchAll))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, DateTo);
                dataManager.Parameters.Add(this.AppSchema.StockMasterSales.TRANS_TYPEColumn, PurchaseFlag);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs SaveStockPurchaseMasters()
        {
            using (DataManager dataManager = new DataManager(this.PurchaseId.Equals(0) ? SQLCommand.StockMasterPurchase.Add : SQLCommand.StockMasterPurchase.Update))
            {
                dataManager.Parameters.Add(this.AppSchema.StockMasterPurchase.PURCHASE_IDColumn, PurchaseId);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.StockMasterPurchase.PURCHASE_DATEColumn, PurchaseDate);
                dataManager.Parameters.Add(this.AppSchema.StockMasterPurchase.VOUCHER_NOColumn, VoucherNo);
                dataManager.Parameters.Add(this.AppSchema.StockMasterPurchase.PURCHASE_ORDER_NOColumn, PurchaseOrderNo);
                dataManager.Parameters.Add(this.AppSchema.StockMasterPurchase.VENDOR_IDColumn, VendorId);
                dataManager.Parameters.Add(this.AppSchema.AssetPurchaseMaster.DISCOUNT_PERColumn, DiscountPer);
                dataManager.Parameters.Add(this.AppSchema.StockMasterPurchase.DISCOUNTColumn, Discount);
                dataManager.Parameters.Add(this.AppSchema.StockMasterPurchase.OTHER_CHARGESColumn, OtherCharges);
                dataManager.Parameters.Add(this.AppSchema.StockMasterPurchase.TAXColumn, Tax);
                dataManager.Parameters.Add(this.AppSchema.StockMasterPurchase.TAX_AMOUNTColumn, TaxAmount);
                dataManager.Parameters.Add(this.AppSchema.StockMasterPurchase.NET_PAYColumn, NetAmount);
                dataManager.Parameters.Add(this.AppSchema.StockMasterPurchase.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(this.AppSchema.StockMasterPurchase.NAME_ADDRESSColumn, NameandAddress);
                dataManager.Parameters.Add(this.AppSchema.StockMasterPurchase.NARRATIONColumn, Narration);
                dataManager.Parameters.Add(this.AppSchema.StockMasterSales.TRANS_TYPEColumn, PurchaseFlag);
                dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteStockPurchaseMaster()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockMasterPurchase.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.StockMasterPurchase.PURCHASE_IDColumn, PurchaseId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs FetchStockPurchaseMasterById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockMasterPurchase.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.StockMasterPurchase.PURCHASE_IDColumn, PurchaseId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        #endregion

        #region Purchase Detail

        private ResultArgs FetchStockPurchaseDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockPurchaseDetail.FetchAll))
            {
                dataManager.Parameters.Add(this.AppSchema.StockMasterPurchase.PURCHASE_IDColumn, StockPurchaseId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs SaveStockPurchaseDetails()
        {
            filterPurchaseDetails();
            foreach (DataRow drStock in dtStockPurchaseDetail.Rows)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.StockPurchaseDetail.Add))
                {
                    dataManager.Parameters.Add(this.AppSchema.StockPurchaseDetails.PURCHASE_IDColumn, PurchaseId);
                    dataManager.Parameters.Add(this.AppSchema.StockPurchaseDetails.ITEM_IDColumn, this.NumberSet.ToInteger(drStock[this.AppSchema.StockPurchaseDetails.ITEM_IDColumn.ColumnName].ToString()));
                    dataManager.Parameters.Add(this.AppSchema.StockPurchaseDetails.LOCATION_IDColumn, this.NumberSet.ToInteger(drStock[this.AppSchema.StockPurchaseDetails.LOCATION_IDColumn.ColumnName].ToString()));
                    dataManager.Parameters.Add(this.AppSchema.StockPurchaseDetails.QUANTITYColumn, this.NumberSet.ToInteger(drStock[this.AppSchema.StockPurchaseDetails.QUANTITYColumn.ColumnName].ToString()));
                    dataManager.Parameters.Add(this.AppSchema.StockPurchaseDetails.UNIT_PRICEColumn, this.NumberSet.ToDecimal(drStock[this.AppSchema.StockPurchaseDetails.UNIT_PRICEColumn.ColumnName].ToString()));
                    dataManager.Parameters.Add(this.AppSchema.StockPurchaseDetails.AMOUNTColumn, this.NumberSet.ToDecimal(drStock[this.AppSchema.StockPurchaseDetails.AMOUNTColumn.ColumnName].ToString()));
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.ACCOUNT_LEDGER_IDColumn, this.NumberSet.ToInteger(drStock[this.AppSchema.ASSETItem.ACCOUNT_LEDGER_IDColumn.ColumnName].ToString()));
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.UpdateData();
                    if (resultArgs != null && !resultArgs.Success) break;
                }
            }
            return resultArgs;
        }

        private ResultArgs DeleteStockPurchaseDetail()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockPurchaseDetail.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.StockMasterPurchase.PURCHASE_IDColumn, PurchaseId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private DataTable FetchStockPurchaseDetailById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockPurchaseDetail.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.StockPurchaseDetails.PURCHASE_IDColumn, PurchaseId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs.DataSource.Table;
        }
        #endregion

        #endregion
    }
}
