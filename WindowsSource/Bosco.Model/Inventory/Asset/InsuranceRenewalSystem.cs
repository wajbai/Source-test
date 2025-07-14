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
    public class InsuranceRenewalSystem : SystemBase
    {
        #region Constructor
        public InsuranceRenewalSystem()
        {

        }
        public InsuranceRenewalSystem(int renewalId)
        {
            this.RenewalId = renewalId;
            AssignToInsRenewalPropertise(this.RenewalId);
        }

        public InsuranceRenewalSystem(int insId, int itemId, int projectId)
        {
            this.InsId = insId;
            this.ItemId = itemId;
            this.ProjectId = projectId;
        }

        #endregion

        #region Properties
        ResultArgs resultArgs = new ResultArgs();
        public int ItemId { get; set; }
        public int InsId { get; set; }
        public int RenewalId { get; set; }
        public int ExpLedger { get; set; }
        public int CashLedId { get; set; }
        public string AssetId { get; set; }
        public DateTime VoucherDate { get; set; }
        public int LedgerId { get; set; }
        public string VoucherNo { get; set; }
        public string NameAddress { get; set; }
        public string Narration { get; set; }
        public DateTime DueDate { get; set; }
        public decimal RenewalAmount { get; set; }
        public DataTable dtRenewalDetails { get; set; }
        public int ProjectId { get; set; }
        public string Project { get; set; }
        public int VouId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<int> InsRenewal = new List<int>();
        private const string CASHBANK_LEDGER = "CASHBANK_LEDGER";

        #endregion

        #region Methods
        public ResultArgs SaveRenewal()
        {
            using (DataManager datamanager = new DataManager())
            {
                resultArgs.Success = true;
                if (InsRenewal.Count > 0)
                {
                    foreach (int iRenewalId in InsRenewal)
                    {
                        DeleteRenewal(iRenewalId);
                    }
                }
                datamanager.BeginTransaction();
                resultArgs = SaveVoucherDetails(datamanager);
                if (resultArgs != null && resultArgs.Success)
                {
                    if (RenewalId > 0)
                    {
                        resultArgs = DeleteInsRenewalDetail(RenewalId);
                    }
                    resultArgs = SaveInsuranceRenewal();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        RenewalId = RenewalId == 0 ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : RenewalId;
                        resultArgs = SaveInsRenewalDetail();
                    }
                    datamanager.EndTransaction();
                }
            }
            return resultArgs;
        }

        private ResultArgs SaveVoucherDetails(DataManager dm)
        {
            try
            {
                using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                {
                    voucherTransaction.VoucherId = VouId;
                    voucherTransaction.ProjectId = ProjectId;
                    voucherTransaction.TransVoucherMethod = (int)TransactionVoucherMethod.Automatic;
                    voucherTransaction.VoucherType = DefaultVoucherTypes.Payment.ToString();
                    voucherTransaction.VoucherDate = VoucherDate;
                    voucherTransaction.VoucherSubType = VoucherSubTypes.AST.ToString();
                    voucherTransaction.NameAddress = NameAddress;
                    voucherTransaction.Narration = Narration;
                    voucherTransaction.Status = 1;

                    resultArgs = voucherTransaction.ConstructVoucherData(ExpLedger, RenewalAmount);
                    if (resultArgs.Success)
                    {
                        this.TransInfo = resultArgs.DataSource.Table.DefaultView;

                        resultArgs = voucherTransaction.ConstructVoucherData(CashLedId, RenewalAmount);

                        if (resultArgs.Success)
                        {
                            this.CashTransInfo = resultArgs.DataSource.Table.DefaultView;

                            resultArgs = voucherTransaction.SaveVoucherDetails(dm);
                            if (resultArgs.Success)
                            {
                                VouId = voucherTransaction.VoucherId;
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

        public ResultArgs SaveInsuranceRenewal()
        {
            using (DataManager dataManager = new DataManager((RenewalId == 0) ? SQLCommand.AssetInsuranceRenewal.AddInsRenMaster : SQLCommand.AssetInsuranceRenewal.Update))
            {
                dataManager.Parameters.Add(this.AppSchema.InsuranceRenewalMaster.RENEWAL_IDColumn, RenewalId, true);
                dataManager.Parameters.Add(this.AppSchema.InsuranceMasterData.INS_IDColumn, InsId);
                dataManager.Parameters.Add(this.AppSchema.InsuranceMasterData.VOUCHER_DATEColumn, VoucherDate);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.InsuranceMasterData.VOUCHER_IDColumn, VouId);
                dataManager.Parameters.Add(this.AppSchema.InsuranceMasterData.NAME_ADDRESSColumn, NameAddress);
                dataManager.Parameters.Add(this.AppSchema.InsuranceMasterData.NARRATIONColumn, Narration);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveInsRenewalDetail()
        {
            foreach (DataRow drRenewalDetails in dtRenewalDetails.Rows)
            {
                using (DataManager datamanager = new DataManager(SQLCommand.AssetInsuranceRenewal.AddInsRenDetails))
                {
                    AssetId = drRenewalDetails[this.AppSchema.SalesAsset.ASSET_IDColumn.ColumnName] != null ?
                    (drRenewalDetails[this.AppSchema.SalesAsset.ASSET_IDColumn.ColumnName].ToString()) : string.Empty;
                    datamanager.Parameters.Add(this.AppSchema.InsuranceRenewalDetails.RENEWAL_IDColumn, RenewalId);
                    datamanager.Parameters.Add(this.AppSchema.InsuranceRenewalDetails.ITEM_IDColumn, drRenewalDetails[this.AppSchema.InsuranceRenewalDetails.ITEM_IDColumn.ColumnName] != null ? this.NumberSet.ToInteger(drRenewalDetails[this.AppSchema.InsuranceRenewalDetails.ITEM_IDColumn.ColumnName].ToString()) : 0);
                    datamanager.Parameters.Add(this.AppSchema.InsuranceRenewalDetails.RENEWAL_AMOUNTColumn, drRenewalDetails[this.AppSchema.InsuranceRenewalDetails.RENEWAL_AMOUNTColumn.ColumnName] != null ? this.NumberSet.ToDecimal(drRenewalDetails[this.AppSchema.InsuranceRenewalDetails.RENEWAL_AMOUNTColumn.ColumnName].ToString()) : 0);
                    datamanager.Parameters.Add(this.AppSchema.InsuranceRenewalDetails.DUE_DATEColumn, drRenewalDetails[this.AppSchema.InsuranceRenewalDetails.DUE_DATEColumn.ColumnName] != null ? this.DateSet.ToDate(drRenewalDetails[this.AppSchema.InsuranceRenewalDetails.DUE_DATEColumn.ColumnName].ToString(), false) : DueDate);
                    datamanager.Parameters.Add(this.AppSchema.AMCDetails.ASSET_IDColumn, AssetId);
                    if (AssetId.Contains(','))
                    {
                        string[] assetIdSplit = AssetId.Split(',');
                        for (int i = 0; i < assetIdSplit.Count(); i++)
                        {
                            AssetId = assetIdSplit[i].TrimStart().ToString();
                            datamanager.Parameters.RemoveItem(this.AppSchema.SalesAsset.ASSET_IDColumn.ColumnName);
                            datamanager.Parameters.Add(this.AppSchema.SalesAsset.ASSET_IDColumn, AssetId);

                            datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                            resultArgs = datamanager.UpdateData();
                        }
                    }
                    else
                    {
                        datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                        resultArgs = datamanager.UpdateData();
                    }
                }
            }
            return resultArgs;
        }

        public void AssignToInsRenewalPropertise(int RenewalId)
        {
            resultArgs = FetchRenewalDetail(RenewalId.ToString());
            if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                if (resultArgs.DataSource.Table != null)
                {
                    InsId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.InsuranceRenewalMaster.INS_IDColumn.ColumnName].ToString());
                    VoucherDate = this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.InsuranceRenewalMaster.VOUCHER_DATEColumn.ColumnName].ToString(), false);
                    NameAddress = resultArgs.DataSource.Table.Rows[0][this.AppSchema.InsuranceRenewalMaster.NAME_ADDRESSColumn.ColumnName].ToString();
                    Narration = resultArgs.DataSource.Table.Rows[0][this.AppSchema.InsuranceRenewalMaster.NARRATIONColumn.ColumnName].ToString();
                    CashLedId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][CASHBANK_LEDGER].ToString());
                    ExpLedger = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.InsuranceRenewalMaster.LEDGER_IDColumn.ColumnName].ToString());
                    ItemId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.InsuranceRenewalDetails.ITEM_IDColumn.ColumnName].ToString());
                    VouId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.InsuranceMasterData.VOUCHER_IDColumn.ColumnName].ToString());
                    VoucherNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.InsuranceRenewalMaster.VOUCHER_NOColumn.ColumnName].ToString();
                    VouId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString());
                }
            }
        }

        public ResultArgs DeleteRenewal(int RenID)
        {
            try
            {
                using (DataManager datamanager = new DataManager())
                {
                    AssignToInsRenewalPropertise(RenID);
                    using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                    {
                        datamanager.BeginTransaction();
                        voucherTransaction.VoucherId = VouId;
                        resultArgs = voucherTransaction.RemoveAssetStockVoucher();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            resultArgs = DeleteInsRenewalDetail(RenID);
                            if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                            {
                                resultArgs = DeleteRenewalMaster(RenID);
                            }
                        }
                        datamanager.EndTransaction();
                    }
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

        private ResultArgs DeleteInsRenewalDetail(int RenewalId)
        {
            try
            {
                using (DataManager datamanager = new DataManager(SQLCommand.AssetInsuranceRenewal.DeleteDetail))
                {
                    datamanager.Parameters.Add(this.AppSchema.InsuranceRenewalMaster.RENEWAL_IDColumn, RenewalId);
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

        public ResultArgs DeleteRenewalbyInsId(int InsuranceId)
        {
            try
            {
                using (DataManager datamanager = new DataManager(SQLCommand.AssetInsuranceRenewal.DeleteRenewalByInsID))
                {
                    datamanager.Parameters.Add(this.AppSchema.InsuranceRenewalMaster.INS_IDColumn, InsuranceId);
                    resultArgs = datamanager.FetchData(DataSource.DataTable);
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

        public ResultArgs DeleteRenewalMaster(int RenewalId)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetInsuranceRenewal.Delete))
            {
                datamanager.Parameters.Add(this.AppSchema.InsuranceRenewalMaster.RENEWAL_IDColumn, RenewalId);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchAllRenewal(int RenewalId)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetInsuranceRenewal.FetchAllRenewal))
            {
                datamanager.Parameters.Add(this.AppSchema.InsuranceMasterData.INS_IDColumn, InsId);
                if (RenewalId > 0)
                {
                    datamanager.Parameters.Add(this.AppSchema.InsuranceRenewalDetails.RENEWAL_IDColumn, RenewalId);
                }
                datamanager.Parameters.Add(this.AppSchema.InsuranceMasterData.ITEM_IDColumn, ItemId);
                datamanager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs ValidateDate(int ItemId, int ProjectId, string dueDate)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetInsuranceRenewal.ValidateDate))
            {
                datamanager.Parameters.Add(this.AppSchema.InsuranceMasterData.ITEM_IDColumn, ItemId);
                datamanager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                datamanager.Parameters.Add(this.AppSchema.InsuranceData.DUE_DATEColumn, dueDate);
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchRenewalMasterById()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetInsuranceRenewal.FetchInsuranceMasterById))
            {
                datamanager.Parameters.Add(this.AppSchema.AMCMaster.AMC_IDColumn, RenewalId);
                datamanager.Parameters.Add(this.AppSchema.VoucherMaster.PROJECT_IDColumn, this.ProjectId);
                datamanager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, this.DateFrom);
                datamanager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, this.DateTo);
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchRenewalDetail(string InsRenId)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetInsuranceRenewal.FetchInsuranceDetail))
            {
                datamanager.Parameters.Add(this.AppSchema.InsuranceRenewalMaster.RENEWAL_IDColumn, InsRenId);
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchRenewalDetailById(string InsRenId)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetInsuranceRenewal.FetchDetailById))
            {
                datamanager.Parameters.Add(this.AppSchema.InsuranceRenewalMaster.RENEWAL_IDColumn, InsRenId);
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs LoadItems()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInsuranceRenewal.FetchAllAssetItem))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion
    }
}
