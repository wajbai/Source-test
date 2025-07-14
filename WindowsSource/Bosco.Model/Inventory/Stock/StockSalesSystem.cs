using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using System.Data;
using Bosco.Model.Transaction;

namespace Bosco.Model.Inventory.Stock
{
    public class StockSalesSystem : SystemBase
    {
        #region Decelartion
        ResultArgs resultArgs = new ResultArgs();
        DataTable dtsalesdetails = new DataTable();
        #endregion

        #region Constructor
        public StockSalesSystem(int slsId)
        {
            SalesId = slsId;
            FillSalesMasterProperties();
            FillItemDetailsbyId(slsId);
        }

        public StockSalesSystem()
        {

        }
        #endregion

        #region Properties
        // Stock Master Details
        public int SalesId { get; set; }
        public string SalesRefNo { get; set; }
        public string CustomerName { get; set; }
        public DateTime SalesDate { get; set; }
        public double Discount { get; set; }
        public double DiscountPer { get; set; }
        public double OtherCharges { get; set; }
        public double Tax { get; set; }
        public double TaxAmount { get; set; }
        public double NetPay { get; set; }
        public int CashBankLedgerId { get; set; }
        public string NameAddress { get; set; }
        public string Narration { get; set; }
        public int SalesFlag { get; set; }
        public string VoucherNo { get; set; }
        public int BranchId { get; set; }
        public int VoucherId { get; set; }
        public int SaleIncomeLedgerId { get; set; }
        public int SalesDisposalLedgerId { get; set; }

        // Stock Item Details
        public int ItemId { get; set; }
        public int LocationId { get; set; }
        public int Quantity { get; set; }
        public int TransQty { get; set; }
        public decimal UnitPrice { get; set; }
        public double Amount { get; set; }
        public int StockMode { get; set; }
        public int IncomeLedgerId { get; set; }
        public int ExpenceLedgerId { get; set; }

        // Stock Sales View
        public int ProjectId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public DataTable dtStockDetails = new DataTable();
        public static DataTable dttempStockDetails = new DataTable();
        public DataTable dtUpdateStockDetails { get; set; }
        public bool UpdatedStockDetailsbeforesave = false;
        public bool isEditMode = false;
        #endregion

        #region Methods

        public ResultArgs SaveStockSales()
        {
            try
            {
                using (DataManager dataManager = new DataManager())
                {
                    resultArgs.Success = true;
                    //if (SalesFlag == (int)StockSalesTransType.Sold)
                    //{
                    //    resultArgs = SaveSalesVoucherDetails(dataManager);
                    //}
                    //else if (SalesFlag == (int)StockSalesTransType.Disposal)
                    //{
                    //    resultArgs = SaveDisposalVoucherDetails(dataManager);
                    //}
                    // chinna 11.01.2020
                    //   resultArgs = (SalesFlag == (int)StockSalesTransType.Sold) ? SaveSalesVoucherDetails(dataManager) : resultArgs; //(SalesFlag == (int)StockSalesTransType.Disposal) ?

                    // SaveDisposalVoucherDetails(dataManager) : resultArgs;
                    if (resultArgs.Success)
                    {
                        dataManager.BeginTransaction();
                        if (SalesId > 0)
                        {
                            isEditMode = true;
                            using (StockBalanceSystem stockbalancesystem = new StockBalanceSystem())
                            {
                                resultArgs = stockbalancesystem.UpdatestockBalance(SalesId, StockType.Sales, TransactionAction.EditBeforeSave);
                                if (resultArgs.Success)
                                {
                                    resultArgs = DeleteItemDetails(dataManager);
                                }
                            }
                        }
                        if (resultArgs.Success)
                        {
                            resultArgs = SaveStockDetails(dataManager);
                            if (resultArgs.Success)
                            {
                                using (StockBalanceSystem stockbalancesystem = new StockBalanceSystem())
                                {
                                    resultArgs = stockbalancesystem.UpdatestockBalance(SalesId, StockType.Sales, TransactionAction.New);
                                }
                            }
                        }
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

        private ResultArgs SaveSalesVoucherDetails(DataManager dm)
        {
            try
            {
                foreach (DataRow dr in dtStockDetails.Rows)
                {
                    using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                    {
                        voucherTransaction.VoucherId = VoucherId;
                        voucherTransaction.ProjectId = ProjectId;
                        voucherTransaction.TransVoucherMethod = (int)TransactionVoucherMethod.Automatic;
                        voucherTransaction.VoucherType = DefaultVoucherTypes.Receipt.ToString();
                        voucherTransaction.VoucherDate = SalesDate;
                        voucherTransaction.VoucherSubType = VoucherSubTypes.STK.ToString();
                        voucherTransaction.NameAddress = NameAddress;
                        voucherTransaction.Narration = Narration;
                        voucherTransaction.Status = 1;

                        SaleIncomeLedgerId = this.NumberSet.ToInteger(dr.Table.Rows[0][this.AppSchema.ASSETItem.ACCOUNT_LEDGER_IDColumn.ColumnName].ToString());
                        if (SaleIncomeLedgerId > 0)
                        {
                            resultArgs = voucherTransaction.ConstructVoucherData(SaleIncomeLedgerId, NumberSet.ToDecimal(NetPay.ToString()));
                            if (resultArgs.Success)
                            {
                                this.TransInfo = resultArgs.DataSource.Table.DefaultView;

                                resultArgs = voucherTransaction.ConstructVoucherData(CashBankLedgerId, NumberSet.ToDecimal(NetPay.ToString()));

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

        private ResultArgs SaveDisposalVoucherDetails(DataManager dm)
        {
            try
            {
                foreach (DataRow dr in dtStockDetails.Rows)
                {
                    using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                    {
                        voucherTransaction.VoucherId = VoucherId;
                        voucherTransaction.ProjectId = ProjectId;
                        voucherTransaction.TransVoucherMethod = (int)TransactionVoucherMethod.Automatic;
                        voucherTransaction.VoucherType = DefaultVoucherTypes.Journal.ToString();
                        voucherTransaction.VoucherDate = SalesDate;
                        voucherTransaction.VoucherSubType = VoucherSubTypes.STK.ToString();
                        voucherTransaction.NameAddress = NameAddress;
                        voucherTransaction.Narration = Narration;
                        voucherTransaction.Status = 1;

                        SaleIncomeLedgerId = this.NumberSet.ToInteger(dr.Table.Rows[0][this.AppSchema.ASSETItem.ACCOUNT_LEDGER_IDColumn.ColumnName].ToString());
                        SalesDisposalLedgerId = this.NumberSet.ToInteger(dr.Table.Rows[0][this.AppSchema.ASSETItem.DISPOSAL_LEDGER_IDColumn.ColumnName].ToString());
                        //using (StockLedgerMapping stockledgermapping = new StockLedgerMapping())
                        //{
                        //    SalesAccountLedgerId = stockledgermapping.AccountLedgerId;
                        //    SalesDisposalLedgerId = stockledgermapping.DisposalLedgerId;
                        //}
                        resultArgs = ConstructDisposalTransData();
                        if (resultArgs.Success)
                        {
                            this.TransInfo = resultArgs.DataSource.Table.DefaultView;

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
            return resultArgs;
        }

        private ResultArgs ConstructDisposalTransData()
        {
            DataTable dtDisposal = new DataTable();
            try
            {
                dtDisposal.Columns.Add(this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName, typeof(Int32));
                dtDisposal.Columns.Add("DEBIT", typeof(decimal));
                dtDisposal.Columns.Add("CREDIT", typeof(decimal));

                DataRow drDebit = dtDisposal.NewRow();
                drDebit[this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName] = SalesDisposalLedgerId;
                drDebit["DEBIT"] = this.NumberSet.ToDecimal(NetPay.ToString());
                dtDisposal.Rows.Add(drDebit);

                DataRow drCredit = dtDisposal.NewRow();
                drCredit[this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName] = SaleIncomeLedgerId;
                drCredit["CREDIT"] = this.NumberSet.ToDecimal(NetPay.ToString());
                dtDisposal.Rows.Add(drCredit);

            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                resultArgs.DataSource.Data = dtDisposal;
            }
            return resultArgs;
        }

        private ResultArgs SaveStockDetails(DataManager saveTransDataManager)
        {
            try
            {
                using (DataManager dataManager = new DataManager())
                {
                    resultArgs = SaveStockMasterDetails(dataManager);
                    if (resultArgs.Success && resultArgs.RowsAffected != 0)
                    {
                        SalesId = (SalesId == 0) ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : SalesId;
                        DataTable dtTransInfo = dtStockDetails;
                        if (dtTransInfo != null && dtTransInfo.Rows.Count > 0)
                        {
                            foreach (DataRow drTrans in dtTransInfo.Rows)
                            {
                                ItemId = NumberSet.ToInteger(drTrans[this.AppSchema.StockSalesDetails.ITEM_IDColumn.ColumnName].ToString());
                                LocationId = NumberSet.ToInteger(drTrans[this.AppSchema.StockSalesDetails.LOCATION_IDColumn.ColumnName].ToString());
                                Quantity = NumberSet.ToInteger(drTrans[this.AppSchema.StockSalesDetails.QUANTITYColumn.ColumnName].ToString());
                                UnitPrice = NumberSet.ToDecimal(drTrans[this.AppSchema.StockSalesDetails.UNIT_PRICEColumn.ColumnName].ToString());
                                Amount = NumberSet.ToDouble(drTrans[this.AppSchema.StockSalesDetails.AMOUNTColumn.ColumnName].ToString());
                                IncomeLedgerId = NumberSet.ToInteger(drTrans[this.AppSchema.ASSETItem.ACCOUNT_LEDGER_IDColumn.ColumnName].ToString());
                                ExpenceLedgerId = NumberSet.ToInteger(drTrans[this.AppSchema.ASSETItem.DISPOSAL_LEDGER_IDColumn.ColumnName].ToString());
                                if (LocationId > 0 && ItemId > 0 && Quantity > 0 && UnitPrice > 0)
                                {
                                    resultArgs = SaveStockItemDetails(saveTransDataManager);
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

        private ResultArgs SaveStockMasterDetails(DataManager dm)
        {
            try
            {
                using (DataManager dataManager = new DataManager((SalesId == 0) ? SQLCommand.StockMasterSales.Add : SQLCommand.StockMasterSales.Update))
                {
                    dataManager.Database = dm.Database;

                    dataManager.Parameters.Add(this.AppSchema.StockMasterSales.SALES_IDColumn, SalesId, true);
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.StockMasterSales.SALES_REF_NOColumn, SalesRefNo);
                    dataManager.Parameters.Add(this.AppSchema.StockMasterSales.CUSTOMER_NAMEColumn, CustomerName);
                    dataManager.Parameters.Add(this.AppSchema.StockMasterSales.SALES_DATEColumn, SalesDate);
                    dataManager.Parameters.Add(this.AppSchema.AssetPurchaseMaster.DISCOUNT_PERColumn, DiscountPer);
                    dataManager.Parameters.Add(this.AppSchema.StockMasterSales.DISCOUNTColumn, Discount);
                    dataManager.Parameters.Add(this.AppSchema.StockMasterSales.OTHER_CHARGESColumn, OtherCharges);
                    dataManager.Parameters.Add(this.AppSchema.StockMasterSales.TAXColumn, Tax);
                    dataManager.Parameters.Add(this.AppSchema.StockMasterSales.TAMOUNTColumn, TaxAmount);
                    dataManager.Parameters.Add(this.AppSchema.StockMasterSales.NET_PAYColumn, NetPay);
                    dataManager.Parameters.Add(this.AppSchema.StockMasterSales.LEDGER_IDColumn, CashBankLedgerId);
                    dataManager.Parameters.Add(this.AppSchema.StockMasterSales.NAME_ADDRESSColumn, NameAddress);
                    dataManager.Parameters.Add(this.AppSchema.StockMasterSales.NARRATIONColumn, Narration);
                    dataManager.Parameters.Add(this.AppSchema.StockMasterSales.TRANS_TYPEColumn, SalesFlag);
                    dataManager.Parameters.Add(this.AppSchema.StockMasterSales.VOUCHER_NOColumn, VoucherNo);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherId);
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

        private ResultArgs SaveStockItemDetails(DataManager dm)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.StockSalesDetail.Add))
                {
                    dataManager.Database = dm.Database;
                    dataManager.Parameters.Add(this.AppSchema.StockSalesDetails.SALES_IDColumn, SalesId);
                    dataManager.Parameters.Add(this.AppSchema.StockSalesDetails.ITEM_IDColumn, ItemId);
                    dataManager.Parameters.Add(this.AppSchema.StockSalesDetails.LOCATION_IDColumn, LocationId);
                    dataManager.Parameters.Add(this.AppSchema.StockSalesDetails.QUANTITYColumn, Quantity);
                    dataManager.Parameters.Add(this.AppSchema.StockSalesDetails.UNIT_PRICEColumn, UnitPrice);
                    dataManager.Parameters.Add(this.AppSchema.StockMasterSales.AMOUNTColumn, Amount);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.ACCOUNT_LEDGER_IDColumn, IncomeLedgerId);
                    dataManager.Parameters.Add(this.AppSchema.ASSETItem.DISPOSAL_LEDGER_IDColumn, ExpenceLedgerId);
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        public ResultArgs FetchSalesMasterDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.StockMasterSales.FetchAll))
                {
                    dataManager.Parameters.Add(this.AppSchema.StockMasterSales.DATE_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(this.AppSchema.StockMasterSales.DATE_TOColumn, DateTo);
                    dataManager.Parameters.Add(this.AppSchema.StockMasterSales.TRANS_TYPEColumn, SalesFlag);
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        public ResultArgs FetchSalesDetailsbySalesId()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.StockMasterSales.Fetch))
                {
                    dataManager.Parameters.Add(this.AppSchema.StockMasterSales.SALES_IDColumn, SalesId);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        public ResultArgs FillSalesMasterProperties()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockMasterSales.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.StockMasterSales.SALES_IDColumn, SalesId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs != null && resultArgs.DataSource.Table != null)
                {
                    DataTable dtMasters = resultArgs.DataSource.Table;
                    ProjectId = NumberSet.ToInteger(dtMasters.Rows[0]["PROJECT_ID"].ToString());
                    SalesRefNo = dtMasters.Rows[0]["SALES_REF_NO"].ToString();
                    CustomerName = dtMasters.Rows[0]["CUSTOMER_NAME"].ToString();
                    SalesDate = DateSet.ToDate(dtMasters.Rows[0]["SALES_DATE"].ToString(), false);
                    Discount = NumberSet.ToDouble(dtMasters.Rows[0]["DISCOUNT"].ToString());
                    DiscountPer = NumberSet.ToDouble(dtMasters.Rows[0]["DISCOUNT_PER"].ToString());
                    OtherCharges = NumberSet.ToDouble(dtMasters.Rows[0]["OTHER_CHARGES"].ToString());
                    Tax = NumberSet.ToDouble(dtMasters.Rows[0]["TAX"].ToString());
                    TaxAmount = NumberSet.ToDouble(dtMasters.Rows[0]["TAX_AMOUNT"].ToString());
                    NetPay = NumberSet.ToDouble(dtMasters.Rows[0]["NET_PAY"].ToString());
                    CashBankLedgerId = NumberSet.ToInteger(dtMasters.Rows[0]["LEDGER_ID"].ToString());
                    NameAddress = dtMasters.Rows[0]["NAME_ADDRESS"].ToString();
                    Narration = dtMasters.Rows[0]["NARRATION"].ToString();
                    SalesFlag = NumberSet.ToInteger(dtMasters.Rows[0]["TRANS_TYPE"].ToString());
                    VoucherNo = dtMasters.Rows[0]["VOUCHER_NO"].ToString();
                    VoucherId = this.NumberSet.ToInteger(dtMasters.Rows[0][this.AppSchema.VoucherTransaction.VOUCHER_IDColumn.ColumnName].ToString());
                    IncomeLedgerId = this.NumberSet.ToInteger(dtMasters.Rows[0][this.AppSchema.ASSETItem.ACCOUNT_LEDGER_IDColumn.ColumnName].ToString());
                    ExpenceLedgerId = this.NumberSet.ToInteger(dtMasters.Rows[0][this.AppSchema.ASSETItem.DISPOSAL_LEDGER_IDColumn.ColumnName].ToString());
                }
            }
            return resultArgs;
        }

        public ResultArgs FillItemDetailsbyId(int MasterId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockSalesDetail.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.StockMasterSales.SALES_IDColumn, SalesId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs != null && resultArgs.DataSource.Table != null)
                {
                    dtStockDetails = resultArgs.DataSource.Table;
                }
            }
            return resultArgs;
        }

        public ResultArgs FetchitemsalesdetailsbyId(int MasterId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockSalesDetail.FetchDetailsbeforeDelete))
            {
                dataManager.Parameters.Add(this.AppSchema.StockMasterSales.SALES_IDColumn, MasterId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchItemDetailsbyid(string MasterId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockSalesDetail.FetchAll))
            {
                dataManager.Parameters.Add(this.AppSchema.StockMasterSales.SALES_IDColumn, MasterId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteItemDetails(DataManager dm)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.StockSalesDetail.Delete))
                {
                    dataManager.Database = dm.Database;
                    dataManager.Parameters.Add(this.AppSchema.StockSalesDetails.SALES_IDColumn, SalesId);
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        public ResultArgs DeleteMasterDetails(DataManager dm)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.StockMasterSales.Delete))
                {
                    dataManager.Database = dm.Database;
                    dataManager.Parameters.Add(this.AppSchema.StockSalesDetails.SALES_IDColumn, SalesId);
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        public ResultArgs DeleteSoldUtlized()
        {
            try
            {
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.BeginTransaction();
                    FillSalesMasterProperties();
                    using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                    {
                        if (SalesFlag == (int)StockSalesTransType.Sold || SalesFlag == (int)StockSalesTransType.Disposal)
                        {
                            voucherTransaction.VoucherId = VoucherId;
                            resultArgs = voucherTransaction.RemoveAssetStockVoucher();
                        }
                        if (resultArgs != null && resultArgs.Success)
                        {
                            resultArgs = FetchitemsalesdetailsbyId(SalesId);
                            if (resultArgs.Success)
                            {
                                dtUpdateStockDetails = resultArgs.DataSource.Table;
                                resultArgs = DeleteItemDetails(dataManager);
                                if (resultArgs.Success)
                                {
                                    resultArgs = DeleteMasterDetails(dataManager);
                                    if (resultArgs.Success)
                                    {
                                        resultArgs = UpdatePurchaseDetailsAfterDelete();
                                    }
                                }
                                else
                                {
                                    dataManager.TransExecutionMode = ExecutionMode.Fail;
                                }
                            }
                        }
                    }
                    dataManager.EndTransaction();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        public int GetSalesId(int VoucherId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockMasterSales.FetchSalesIdByVoucherId))
            {
                dataManager.Parameters.Add(AppSchema.Voucher.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        private ResultArgs UpdatePurchaseDetailsAfterDelete()
        {
            if (dtUpdateStockDetails != null && dtUpdateStockDetails.Rows.Count > 0)
            {
                foreach (DataRow drsales in dtUpdateStockDetails.Rows)
                {
                    using (StockBalanceSystem BalanceSystem = new StockBalanceSystem())
                    {
                        ProjectId = NumberSet.ToInteger(drsales[this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString());
                        SalesDate = DateSet.ToDate(drsales["BALANCE_DATE"].ToString(), false);
                        ItemId = NumberSet.ToInteger(drsales[this.AppSchema.StockSalesDetails.ITEM_IDColumn.ColumnName].ToString());
                        LocationId = NumberSet.ToInteger(drsales[this.AppSchema.StockSalesDetails.LOCATION_IDColumn.ColumnName].ToString());
                        Quantity = NumberSet.ToInteger(drsales[this.AppSchema.StockSalesDetails.QUANTITYColumn.ColumnName].ToString());
                        UnitPrice = NumberSet.ToDecimal(drsales[this.AppSchema.StockSalesDetails.UNIT_PRICEColumn.ColumnName].ToString());
                        resultArgs = BalanceSystem.UpdateStockDetails(ProjectId, SalesDate, LocationId, ItemId, Quantity, UnitPrice, (int)StockReturnType.InWards);
                        if (resultArgs != null && !resultArgs.Success) break;
                    }
                }
            }
            return resultArgs;
        }

        public DataSet FetchSalesDetails()
        {
            DataSet dsSales = new DataSet();
            string SLSID = string.Empty;
            try
            {
                using (DataManager dataManager = new DataManager())
                {
                    resultArgs = FetchSalesMasterDetails();
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        resultArgs.DataSource.Table.TableName = "Master";
                        dsSales.Tables.Add(resultArgs.DataSource.Table);
                        for (int i = 0; i < resultArgs.DataSource.Table.Rows.Count; i++)
                        {
                            SLSID += resultArgs.DataSource.Table.Rows[i][this.AppSchema.StockMasterSales.SALES_IDColumn.ColumnName].ToString() + ",";
                        }
                        SLSID = SLSID.TrimEnd(',');
                        resultArgs = FetchItemDetailsbyid(SLSID);
                        if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                        {
                            resultArgs.DataSource.Table.TableName = "Item Details";
                            dsSales.Tables.Add(resultArgs.DataSource.Table);
                            dsSales.Relations.Add(dsSales.Tables[1].TableName, dsSales.Tables[0].Columns[this.AppSchema.StockMasterSales.SALES_IDColumn.ColumnName], dsSales.Tables[1].Columns[this.AppSchema.StockMasterSales.SALES_IDColumn.ColumnName]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return dsSales;
        }

        public ResultArgs AutoCustomerName()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockMasterSales.AutoFetchCustomer))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs AutoNameAddress()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockMasterSales.AutoFetchNameAddress))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs AutoNarration()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockMasterSales.AutoFetchNarration))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion
    }
}