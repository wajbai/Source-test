using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using System.Data;
using Bosco.Utility;
using Bosco.Model.Inventory.Stock;
using Bosco.Model.Transaction;


namespace Bosco.Model.Inventory.Stock
{
    public class StockPurcahseReturnSystem : SystemBase
    {
        #region Declaration

        ResultArgs resultArgs = new ResultArgs();

        #endregion

        #region Properties

        // Purchase Returns Master Properties
        public int ReturnId { set; get; }
        public int ProjectId { set; get; }
        public DateTime ReturnDate { set; get; }
        public int LedgerId { set; get; }
        public decimal NetPay { set; get; }
        public string Reason { set; get; }
        public int ReturnType { set; get; }

        // Purchase Returns Details Properties
        public int ItemId { set; get; }
        public int Quantity { set; get; }
        public decimal UnitPrice { set; get; }
        public int LocationId { set; get; }
        public int VendorId { set; get; }
        public decimal TotalAmount { set; get; }

        //Common Properties

        public DateTime StartDate { set; get; }
        public DateTime ToDate { set; get; }
        public string ReturnIds { set; get; }
        public DataTable dtItems { set; get; }
        public bool isEditMode = false;
        string LocationIds = string.Empty;
        string ItemIds = string.Empty;

        public int PurchaseAccountLedgerId { get; set; }
        public int VoucherId { get; set; }
        #endregion

        #region Methods
        public ResultArgs FetchMasterPurchaseReturnDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.StockPurchaseReturns.FetchPurchaseReturnsMasterDetails))
                {
                    dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    if (ReturnId > 0)
                    {
                        dataManager.Parameters.Add(AppSchema.StockPurchaseReturnsDetails.RETURN_IDColumn, ReturnId);
                    }
                    else
                    {
                        dataManager.Parameters.Add(AppSchema.Project.DATE_STARTEDColumn, StartDate);
                        dataManager.Parameters.Add(AppSchema.Project.DATE_CLOSEDColumn, ToDate);
                    }
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }
            return resultArgs;
        }

        public ResultArgs FetchPurchaseReturnDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.StockPurchaseReturns.FetchPurchaseReturnDetails))
                {
                    dataManager.Parameters.Add(AppSchema.StockPurchaseReturnsDetails.RETURN_IDColumn, ReturnIds);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }

            return resultArgs;
        }

        public DataSet FetchPurchaseReturns()
        {
            DataSet dsPurchaseReturns = new DataSet();
            try
            {
                resultArgs = FetchMasterPurchaseReturnDetails();
                if (resultArgs != null && resultArgs.DataSource != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    resultArgs.DataSource.Table.TableName = "Master";
                    dsPurchaseReturns.Tables.Add(resultArgs.DataSource.Table);
                }
                foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                {
                    ReturnIds += dr[AppSchema.StockPurchaseReturnsDetails.RETURN_IDColumn.ColumnName].ToString() + ",";
                }
                ReturnIds = resultArgs.RowsAffected > 0 ? ReturnIds.TrimEnd(',') : "0";

                resultArgs = FetchPurchaseReturnDetails();
                if (resultArgs != null && resultArgs.DataSource != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    resultArgs.DataSource.Table.TableName = "Detail";
                    dsPurchaseReturns.Tables.Add(resultArgs.DataSource.Table);
                    dsPurchaseReturns.Relations.Add(dsPurchaseReturns.Tables[1].TableName, dsPurchaseReturns.Tables[0].Columns[AppSchema.StockMasterPurchaseReturns.RETURN_IDColumn.ColumnName], dsPurchaseReturns.Tables[1].Columns[AppSchema.StockPurchaseReturnsDetails.RETURN_IDColumn.ColumnName]);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }
            return dsPurchaseReturns;
        }

        public DataSet FetchPurchaseReturnById()
        {

            DataSet dsPurchaseReturns = new DataSet();
            try
            {
                resultArgs = FetchMasterPurchaseReturnDetails();
                if (resultArgs != null && resultArgs.DataSource != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    resultArgs.DataSource.Table.TableName = "Master";
                    dsPurchaseReturns.Tables.Add(resultArgs.DataSource.Table);
                }
                foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                {
                    ReturnIds += dr[AppSchema.StockPurchaseReturnsDetails.RETURN_IDColumn.ColumnName].ToString() + ",";
                }
                ReturnIds = resultArgs.RowsAffected > 0 ? ReturnIds.TrimEnd(',') : "0";
                string returnDate = resultArgs.RowsAffected > 0 ? resultArgs.DataSource.Table.Rows[0][AppSchema.StockMasterPurchaseReturns.RETURN_DATEColumn.ColumnName].ToString() : string.Empty;
                if (!string.IsNullOrEmpty(returnDate))
                {
                    ReturnDate = DateSet.ToDate(returnDate, false);
                }
                resultArgs = FetchItemLocaionById();

                foreach (DataRow drItemLocation in resultArgs.DataSource.Table.Rows)
                {
                    ItemIds += drItemLocation[AppSchema.StockPurchaseReturnsDetails.ITEM_IDColumn.ColumnName].ToString() + ",";
                    LocationIds += drItemLocation[AppSchema.StockPurchaseReturnsDetails.LOCATION_IDColumn.ColumnName].ToString() + ",";
                }
                ItemIds = resultArgs.RowsAffected > 0 ? ItemIds.TrimEnd(',') : "0";
                LocationIds = resultArgs.RowsAffected > 0 ? LocationIds.TrimEnd(',') : "0";


                resultArgs = FetchPurchaseReturnDetailsById();
                if (resultArgs != null && resultArgs.DataSource != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    dsPurchaseReturns.Tables.Add(resultArgs.DataSource.Table);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }
            return dsPurchaseReturns;

        }

        public ResultArgs FetchPurchaseReturnDetailsById()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.StockPurchaseReturns.FetchPurchaseDetailsById))
                {
                    dataManager.Parameters.Add(AppSchema.StockPurchaseReturnsDetails.RETURN_IDColumn, ReturnId);
                    dataManager.Parameters.Add(AppSchema.StockPurchaseReturnsDetails.LOCATION_IDColumn, LocationIds);
                    dataManager.Parameters.Add(AppSchema.StockPurchaseReturnsDetails.ITEM_IDColumn, ItemIds);
                    dataManager.Parameters.Add(AppSchema.StockMasterPurchaseReturns.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(AppSchema.StockMasterPurchaseReturns.RETURN_DATEColumn, ReturnDate);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }
            return resultArgs;
        }

        public ResultArgs FetchItemLocaionById()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.StockPurchaseReturns.FetchItemLocationById))
                {
                    dataManager.Parameters.Add(AppSchema.StockPurchaseReturnsDetails.RETURN_IDColumn, ReturnId);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source);

            }
            return resultArgs;
        }

        public ResultArgs FetchPurchaseReturnDetailsBeforeDelete()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.StockPurchaseReturns.FetchPurchaseById))
                {
                    dataManager.Parameters.Add(AppSchema.StockPurchaseReturnsDetails.RETURN_IDColumn, ReturnId);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }
            return resultArgs;
        }

        public ResultArgs SavePurchaseReturns()
        {
            try
            {
                using (DataManager dataManager = new DataManager())
                {
                    // Hiden by Chinna 
                    resultArgs.Success = true;  //SaveReturnsVoucherDetails(dataManager);
                    if (resultArgs.Success)
                    {
                        dataManager.BeginTransaction();
                        resultArgs = SaveMasterPurchseReturns();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            if (ReturnId > 0)
                            {
                                isEditMode = true;
                                using (StockBalanceSystem stockbalancesystem = new StockBalanceSystem())
                                {
                                    resultArgs = stockbalancesystem.UpdatestockBalance(ReturnId, StockType.PurchaseReturns, TransactionAction.EditBeforeSave);
                                    if (resultArgs != null && resultArgs.Success)
                                    {
                                        resultArgs = DeletePurchaseReturnDetails();
                                    }
                                }
                            }
                            if (resultArgs != null && resultArgs.Success)
                            {
                                ReturnId = ReturnId.Equals(0) && resultArgs.RowUniqueId != null ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : ReturnId;

                                resultArgs = SavePurchaseReturnDetails();
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    using (StockBalanceSystem stockbalancesystem = new StockBalanceSystem())
                                    {
                                        resultArgs = stockbalancesystem.UpdatestockBalance(ReturnId, StockType.PurchaseReturns, TransactionAction.New);
                                    }
                                }
                            }
                        }
                        dataManager.EndTransaction();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }
            return resultArgs;
        }

        private ResultArgs SaveReturnsVoucherDetails(DataManager dm)
        {
            try
            {
                foreach (DataRow dr in dtItems.Rows)
                {
                    using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                    {
                        voucherTransaction.VoucherId = VoucherId;
                        voucherTransaction.ProjectId = ProjectId;
                        voucherTransaction.TransVoucherMethod = (int)TransactionVoucherMethod.Automatic;
                        voucherTransaction.VoucherType = DefaultVoucherTypes.Receipt.ToString();
                        voucherTransaction.VoucherDate = ReturnDate;
                        voucherTransaction.VoucherSubType = VoucherSubTypes.STK.ToString();
                        voucherTransaction.Narration = Reason;
                        voucherTransaction.Status = 1;
                        voucherTransaction.ClientReferenceId = ReturnId.ToString();

                        PurchaseAccountLedgerId = this.NumberSet.ToInteger(dr.Table.Rows[0][this.AppSchema.ASSETItem.ACCOUNT_LEDGER_IDColumn.ColumnName].ToString());

                        //using (StockLedgerMapping stockledgermapping = new StockLedgerMapping())
                        //{
                        //    PurchaseAccountLedgerId = stockledgermapping.AccountLedgerId;
                        //}
                        if (PurchaseAccountLedgerId > 0)
                        {
                            resultArgs = voucherTransaction.ConstructVoucherData(PurchaseAccountLedgerId, NumberSet.ToDecimal(NetPay.ToString()));
                            if (resultArgs.Success)
                            {
                                this.TransInfo = resultArgs.DataSource.Table.DefaultView;

                                resultArgs = voucherTransaction.ConstructVoucherData(LedgerId, NumberSet.ToDecimal(NetPay.ToString()));

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
                            MessageRender.ShowMessage("Stock Income Ledger is not mapped");
                            resultArgs.Success = false;
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

        private ResultArgs SaveMasterPurchseReturns()
        {
            try
            {
                using (DataManager dataManager = new DataManager(ReturnId == 0 ? SQLCommand.StockPurchaseReturns.AddMasterPurchaseReturns : SQLCommand.StockPurchaseReturns.UpdateMasterPurchaseReturns))
                {
                    dataManager.Parameters.Add(AppSchema.StockMasterPurchaseReturns.RETURN_IDColumn, ReturnId, true);
                    dataManager.Parameters.Add(AppSchema.StockMasterPurchaseReturns.RETURN_DATEColumn, ReturnDate);
                    dataManager.Parameters.Add(AppSchema.StockMasterPurchaseReturns.RETURN_TYPEColumn, ReturnType);
                    dataManager.Parameters.Add(AppSchema.StockMasterPurchaseReturns.LEDGER_IDColumn, LedgerId);
                    dataManager.Parameters.Add(AppSchema.StockMasterPurchaseReturns.REASONColumn, Reason);
                    dataManager.Parameters.Add(AppSchema.StockMasterPurchaseReturns.NET_PAYColumn, NetPay);
                    dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherId);
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }
            return resultArgs;
        }

        private ResultArgs SavePurchaseReturnDetails()
        {
            try
            {
                foreach (DataRow Item in dtItems.Rows)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.StockPurchaseReturns.AddPruchaseReturnDetails))
                    {
                        dataManager.Parameters.Add(AppSchema.StockPurchaseReturnsDetails.RETURN_IDColumn, ReturnId);
                        dataManager.Parameters.Add(AppSchema.StockPurchaseReturnsDetails.ITEM_IDColumn, NumberSet.ToInteger(Item[AppSchema.StockPurchaseReturnsDetails.ITEM_IDColumn.ColumnName].ToString()));
                        dataManager.Parameters.Add(AppSchema.StockPurchaseReturnsDetails.LOCATION_IDColumn, NumberSet.ToInteger(Item[AppSchema.StockPurchaseReturnsDetails.LOCATION_IDColumn.ColumnName].ToString()));
                        dataManager.Parameters.Add(AppSchema.StockPurchaseReturnsDetails.VENDOR_IDColumn, NumberSet.ToInteger(Item[AppSchema.StockPurchaseReturnsDetails.VENDOR_IDColumn.ColumnName].ToString()));
                        dataManager.Parameters.Add(AppSchema.StockPurchaseReturnsDetails.UNIT_PRICEColumn, NumberSet.ToDecimal(Item[AppSchema.StockPurchaseReturnsDetails.UNIT_PRICEColumn.ColumnName].ToString()));
                        dataManager.Parameters.Add(AppSchema.StockPurchaseReturnsDetails.QUANTITYColumn, NumberSet.ToInteger(Item[AppSchema.StockPurchaseReturnsDetails.QUANTITYColumn.ColumnName].ToString()));
                        dataManager.Parameters.Add(AppSchema.StockPurchaseReturnsDetails.TOTAL_AMOUNTColumn, NumberSet.ToDecimal(Item[AppSchema.StockPurchaseReturnsDetails.TOTAL_AMOUNTColumn.ColumnName].ToString()));
                        dataManager.Parameters.Add(AppSchema.ASSETItem.ACCOUNT_LEDGER_IDColumn, NumberSet.ToInteger(Item[AppSchema.ASSETItem.ACCOUNT_LEDGER_IDColumn.ColumnName].ToString()));
                        resultArgs = dataManager.UpdateData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }
            return resultArgs;
        }

        public ResultArgs UpdateStock()
        {
            try
            {
                if (dtItems != null)
                {
                    for (int i = 0; i < dtItems.Rows.Count; i++)
                    {

                        using (StockBalanceSystem stockBalanceSystem = new StockBalanceSystem())
                        {
                            ItemId = NumberSet.ToInteger(dtItems.Rows[i][AppSchema.StockPurchaseReturnsDetails.ITEM_IDColumn.ColumnName].ToString());
                            LocationId = NumberSet.ToInteger(dtItems.Rows[i][AppSchema.StockPurchaseReturnsDetails.LOCATION_IDColumn.ColumnName].ToString());
                            Quantity = NumberSet.ToInteger(dtItems.Rows[i][AppSchema.StockPurchaseReturnsDetails.QUANTITYColumn.ColumnName].ToString());
                            UnitPrice = NumberSet.ToDecimal(dtItems.Rows[i][AppSchema.StockPurchaseReturnsDetails.UNIT_PRICEColumn.ColumnName].ToString());
                            resultArgs = stockBalanceSystem.UpdateStockDetails(ProjectId, ReturnDate, LocationId, ItemId, Quantity, UnitPrice, (int)StockReturnType.InWards);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source);
            }
            return resultArgs;
        }

        public ResultArgs DeletePurchaseReturn()
        {
            try
            {
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.BeginTransaction();
                    resultArgs = FetchMasterPurchaseReturnDetails();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                        {
                            voucherTransaction.VoucherId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][AppSchema.VoucherTransaction.VOUCHER_IDColumn.ColumnName].ToString());
                            resultArgs = voucherTransaction.RemoveAssetStockVoucher();
                        }
                    }
                    if (resultArgs != null && resultArgs.Success)
                    {
                        resultArgs = DeletePurchaseReturnDetails();
                        if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            resultArgs = DeleteMasterPurchaseReturns();
                            if (resultArgs.Success)
                            {
                                resultArgs = UpdateStock();
                            }
                        }
                    }
                    dataManager.EndTransaction();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }
            return resultArgs;

        }

        public int GetPurchaseReturnId(int VoucherId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockPurchaseReturns.FetchPurchaseReturnIdByVoucherId))
            {
                dataManager.Parameters.Add(AppSchema.Voucher.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }


        private ResultArgs DeletePurchaseReturnDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.StockPurchaseReturns.DeletePruchaseReturnDetails))
                {
                    dataManager.Parameters.Add(AppSchema.StockPurchaseReturnsDetails.RETURN_IDColumn, ReturnId);
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }
            return resultArgs;
        }

        private ResultArgs DeleteMasterPurchaseReturns()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.StockPurchaseReturns.DeletePurchaseReturnMaster))
                {
                    dataManager.Parameters.Add(AppSchema.StockPurchaseReturnsDetails.RETURN_IDColumn, ReturnId);
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }
            return resultArgs;
        }

        #endregion

    }
}
