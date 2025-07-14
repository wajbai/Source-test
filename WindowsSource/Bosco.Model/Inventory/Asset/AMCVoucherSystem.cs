using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using System.Data;
using Bosco.Model.Transaction;

namespace Bosco.Model.Inventory.Asset
{
    public class AMCVoucherSystem : SystemBase
    {
        #region Constructor
        public AMCVoucherSystem(int Acmid)
        {
            this.AmcId = Acmid;
            FillAMCVoucherProperties(this.AmcId);
        }
        public AMCVoucherSystem()
        {
        }
        #endregion

        #region Variable Declearation
        private const string CASHBANK_LEDGER = "CASHBANK_LEDGER";
        ResultArgs resultArgs = new ResultArgs();
        public bool isEdit = false;
        # endregion

        #region Properties
        public int AmcId { get; set; }
        public int GroupId { get; set; }
        public int ItemId { get; set; }
        public int LocationId { get; set; }
        public int ProjectId { get; set; }
        public int BranchId { get; set; }
        public string AssetId { get; set; }
        public int VoucherId { get; set; }
        public int VoucherIDs { get; set; }
        public string Amctype { get; set; }
        public string Provider { get; set; }
        public string BillNo { get; set; }
        public string InvoiceNo { get; set; }
        public string ProjectName { get; set; }
        public int CashLedgerId { get; set; }
        public int ExpLedgerId { get; set; }
        public string NameAddress { get; set; }
        public string Narration { get; set; }
        public DateTime AmcDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public decimal Amount { get; set; }
        public decimal ItemAmount { get; set; }
        public DataTable dtVoucherEdit { get; set; }
        public int sequenceno { get; set; }
        public DataTable dtVoucherDetails { get; set; }
        public DataTable dtcashbankDetails { get; set; }
        #endregion

        #region Cash/BankLedger Transaction
        private ResultArgs SaveVoucherDetails(DataManager dm)
        {
            try
            {
                using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                {
                    voucherTransaction.VoucherId = VoucherId;
                    voucherTransaction.ProjectId = ProjectId;
                    voucherTransaction.TransVoucherMethod = (int)TransactionVoucherMethod.Automatic;
                    voucherTransaction.VoucherType = DefaultVoucherTypes.Payment.ToString();
                    voucherTransaction.VoucherDate = VoucherDate;
                    voucherTransaction.VoucherSubType = VoucherSubTypes.AST.ToString();
                    voucherTransaction.NameAddress = NameAddress;
                    voucherTransaction.Narration = Narration;
                    voucherTransaction.Status = 1;

                    resultArgs = voucherTransaction.ConstructVoucherData(ExpLedgerId, Amount);
                    if (resultArgs.Success)
                    {
                        this.TransInfo = resultArgs.DataSource.Table.DefaultView;

                        resultArgs = voucherTransaction.ConstructVoucherData(CashLedgerId, Amount);

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
            return resultArgs;
        }

        public ResultArgs FetchCashBankByVoucherId(int VoucherID)
        {
            using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
            {
                vouchersystem.VoucherId = VoucherID;
                resultArgs = vouchersystem.FetchCashBankDetails();
            }
            return resultArgs;
        }

        private ResultArgs ConstructLedgerTransaction()
        {
            int ItemId = 0;
            try
            {
                if (dtVoucherDetails != null && dtVoucherDetails.Rows.Count > 0)
                {
                    foreach (DataRow drAssetItem in dtVoucherDetails.Rows)
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
                resultArgs.DataSource.Data = dtVoucherDetails;
            }
            return resultArgs;
        }
        #endregion

        #region Methods
        //public ResultArgs SaveAMCVoucher()
        //{
        //    using (DataManager datamanager = new DataManager())
        //    {
        //        resultArgs.Success = true;
        //        datamanager.BeginTransaction();
        //        resultArgs = SaveVoucherDetails(datamanager);
        //        if (resultArgs != null && resultArgs.Success)
        //        {
        //            if (AmcId > 0)
        //            {
        //                resultArgs = DeleteAMCDetail(AmcId);
        //            }
        //            resultArgs = SaveAMCVoucherMaster();
        //            if (resultArgs != null && resultArgs.Success)
        //            {
        //                AmcId = AmcId == 0 ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : AmcId;
        //                resultArgs = SaveAMCVoucherDetail();
        //            }
        //        }
        //        datamanager.EndTransaction();
        //    }
        //    return resultArgs;
        //}

        public ResultArgs SaveAMCVoucher()
        {
            using (DataManager datamanager = new DataManager())
            {
                resultArgs.Success = true;
                using (AssetInwardOutwardSystem inoutsystem = new AssetInwardOutwardSystem())
                {
                    inoutsystem.VoucherId = VoucherId;
                    inoutsystem.ProjectId = ProjectId;
                    inoutsystem.InOutDate = VoucherDate;
                    inoutsystem.Narration = Narration;
                    inoutsystem.dtinoutword = dtVoucherEdit;
                    inoutsystem.dtCashBank = dtcashbankDetails;
                    inoutsystem.Flag = AssetInOut.AMC.ToString();

                    inoutsystem.SaveAssetVouchers(datamanager);

                    if (resultArgs != null && resultArgs.Success)
                    {
                        VoucherId = inoutsystem.VoucherId;
                        resultArgs = SaveAMCVoucherMaster();
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs SaveAMCVoucherMaster()
        {
            using (DataManager datamanager = new DataManager((AmcId == 0) ? SQLCommand.AssetAMCVoucher.Add : SQLCommand.AssetAMCVoucher.Update))
            {
                datamanager.Parameters.Add(this.AppSchema.AMCMaster.AMC_IDColumn, AmcId, true);
                datamanager.Parameters.Add(this.AppSchema.AMCDetails.BILL_INVOICE_NOColumn, BillNo);
                datamanager.Parameters.Add(this.AppSchema.AMCDetails.PROVIDERColumn, Provider);
                datamanager.Parameters.Add(this.AppSchema.AMCMaster.VOUCHER_IDColumn, VoucherId);
                datamanager.Parameters.Add(this.AppSchema.AMCMaster.BRANCH_IDColumn, BranchId);
                datamanager.Parameters.Add(this.AppSchema.AMCMaster.AMC_DATEColumn, VoucherDate);
                datamanager.Parameters.Add(this.AppSchema.AMCMaster.TOT_AMOUNTColumn, Amount);
                datamanager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.UpdateData();

                AmcId = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
            }
            return resultArgs;
        }

        public ResultArgs SaveAMCVoucherDetail()
        {
            try
            {
                {
                    int i = 0;
                    foreach (DataRow drAMCDetails in dtVoucherDetails.Rows)
                    {
                        using (DataManager datamanager = new DataManager(SQLCommand.AssetAMCVoucher.AddAmcVoucherDetail))
                        {
                            datamanager.Parameters.Add(this.AppSchema.AMCDetails.AMC_IDColumn, AmcId);
                            datamanager.Parameters.Add(this.AppSchema.AMCDetails.START_DATEColumn, StartDate);
                            datamanager.Parameters.Add(this.AppSchema.AMCDetails.DUE_DATEColumn, DueDate);
                            datamanager.Parameters.Add(this.AppSchema.AMCDetails.AMOUNTColumn, ItemAmount);
                            datamanager.Parameters.Add(this.AppSchema.AMCDetails.SEQUENCE_NOColumn, sequenceno);
                            datamanager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_DETAIL_IDColumn, drAMCDetails[this.AppSchema.ASSETItem.ITEM_DETAIL_IDColumn.ColumnName] != null ? this.NumberSet.ToInteger(drAMCDetails[this.AppSchema.ASSETItem.ITEM_DETAIL_IDColumn.ColumnName].ToString()) : 0);
                            AmcId = AmcId == 0 ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : AmcId;
                            resultArgs = datamanager.UpdateData();
                            i++;
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

        public ResultArgs DeleteAmcVoucherDetails(int AmcId)
        {
            try
            {
                using (DataManager datamanager = new DataManager())
                {
                    datamanager.BeginTransaction();
                    FillAMCVoucherProperties(AmcId);
                    using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                    {
                        resultArgs = FetchVoucherIdbyMasterId(AmcId);
                        if (resultArgs.Success && resultArgs.DataSource.Sclar.ToInteger > 0)
                        {
                            voucherTransaction.VoucherId = resultArgs.DataSource.Sclar.ToInteger;
                            resultArgs = voucherTransaction.RemoveVoucher(datamanager);
                        }
                    }
                    if (resultArgs.Success)
                    {
                        resultArgs = DeleteAMCDetail(AmcId);
                        if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            resultArgs = DeleteAmcMaster(AmcId);
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

        private ResultArgs DeleteAmcMaster(int AmcId)
        {
            try
            {
                using (DataManager datamanager = new DataManager(SQLCommand.AssetAMCVoucher.Delete))
                {
                    datamanager.Parameters.Add(this.AppSchema.AMCMaster.AMC_IDColumn, AmcId);
                    resultArgs = datamanager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }

            return resultArgs;
        }

        public ResultArgs DeleteAMCDetail(int AmcId)
        {
            try
            {
                using (DataManager datamanager = new DataManager(SQLCommand.AssetAMCVoucher.DeleteDetail))
                {
                    datamanager.Parameters.Add(this.AppSchema.AMCDetails.AMC_IDColumn, AmcId);
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

        public void FillAMCVoucherProperties(int AmcId)
        {
            resultArgs = FetchAMCMasterbyId();
            if (resultArgs != null)
            {
                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    AmcDate = DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AMCMaster.AMC_DATEColumn.ColumnName].ToString(), false);
                    VoucherIDs = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString());
                    BillNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AMCDetails.BILL_INVOICE_NOColumn.ColumnName].ToString();
                    // Narration = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.NARRATIONColumn.ColumnName].ToString();
                    // NameAddress = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.NAME_ADDRESSColumn.ColumnName].ToString();
                    // CashLedgerId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][CASHBANK_LEDGER].ToString());
                    // ExpLedgerId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
                    Provider = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AMCDetails.PROVIDERColumn.ColumnName].ToString();
                    // InvoiceNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetPurchaseMaster.INVOICE_NOColumn.ColumnName].ToString();
                }
            }
        }

        private ResultArgs FetchAMCMasterbyId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetAMCVoucher.FetchbyId))
            {
                dataManager.Parameters.Add(this.AppSchema.AMCMaster.AMC_IDColumn, this.AmcId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public DataSet FetchAssetAMCVoucherDetails()
        {
            DataSet dsAmcVoucher = new DataSet();
            string Amcid = string.Empty;
            try
            {
                resultArgs = FetchallAmcVoucherDetailsByProjectId();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null
                    && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    resultArgs.DataSource.Table.TableName = "Master";
                    dsAmcVoucher.Tables.Add(resultArgs.DataSource.Table);

                    foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                    {
                        Amcid += dr[this.AppSchema.AMCDetails.AMC_IDColumn.ColumnName].ToString() + ",";
                    }
                    Amcid = Amcid.TrimEnd(',');

                    resultArgs = FetchAMCVoucherDetailById(Amcid);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        resultArgs.DataSource.Table.TableName = "AmcDetails";
                        dsAmcVoucher.Tables.Add(resultArgs.DataSource.Table);
                    }
                    dsAmcVoucher.Relations.Add(dsAmcVoucher.Tables[1].TableName, dsAmcVoucher.Tables[0].Columns[this.AppSchema.AMCDetails.AMC_IDColumn.ColumnName], dsAmcVoucher.Tables[1].Columns[this.AppSchema.AMCDetails.AMC_IDColumn.ColumnName]);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
            return dsAmcVoucher;
        }

        private ResultArgs FetchallAmcVoucherDetailsByProjectId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetAMCVoucher.FetchAll))
            {
                dataManager.Parameters.Add(this.AppSchema.AMCMaster.AMC_IDColumn, AmcId);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, this.ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, this.DateFrom);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, this.DateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAMCVoucherDetailById(string Amcid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetAMCVoucher.FetchDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.AMCMaster.AMC_IDColumn, Amcid);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAMCDetailsById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetAMCVoucher.FetchAMCDetailbyId))
            {
                dataManager.Parameters.Add(this.AppSchema.AMCMaster.AMC_IDColumn, this.AmcId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs AutoFetchProviderName()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetAMCVoucher.AutoFetchProviderName))
            {
                //dataManager.Parameters.Add(this.AppSchema.AMCMaster.AMC_IDColumn, this.AmcId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs DeleteAssetItemDetailsByItemDeatilId(int amcid, int ItemDetailId)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.AssetAMCVoucher.DeleteAMCDetailsByAMCIdItemdetailID))
                {
                    dataManager.Parameters.Add(this.AppSchema.AMCMaster.AMC_IDColumn, amcid);
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

        public ResultArgs FetchVoucherIdbyMasterId(int AmcID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetAMCVoucher.FetchVoucherIdbyMasterId))
            {
                dataManager.Parameters.Add(AppSchema.AMCMaster.AMC_IDColumn, AmcID);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }
        #endregion

    }
}
