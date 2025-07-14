using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;

namespace Bosco.Model
{
    public class InsuranceSystem : SystemBase
    {
        #region Variable Declearation

        ResultArgs resultArgs = new ResultArgs();

        #endregion

        #region Constructor

        public InsuranceSystem(int Insid)
        {
            this.InsId = Insid;
            FetchInsuranceMasters();
        }
        public InsuranceSystem(int insid, int itemId)
        {
            this.InsId = insid;
            this.ItemId = itemId;
        }
        public InsuranceSystem()
        {
        }

        #endregion

        #region Properties

        public DataTable dtEditInsuranceMasters { get; set; }
        public DataTable dtEditInsuranceDetails { get; set; }
        public DataTable dtAssetInsurance { get; set; }
        public DataSet dsInsuranceDetails { get; set; }

        public int InsuranceDetailsId { get; set; }
        public int InsuranceType { get; set; }
        public string Provider { get; set; }
        public string Agent { get; set; }
        public string Policy { get; set; }
        public string PolicyNo { get; set; }
        public double PremiumAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }


        public int GroupId { get; set; }
        public int LocationId { get; set; }
        public int ItemId { get; set; }
        public string AssetId { get; set; }
        public int IsInsuranceDetailsNeed { get; set; }
        public double Value { get; set; }

        public int InsId { get; set; }
        public DateTime VoucherDate { get; set; }
        public int LedgerId { get; set; }
        public string VoucherNo { get; set; }
        public string NameAddress { get; set; }
        public string Narration { get; set; }
        public int ProjectId { get; set; }
        public string Project { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        #endregion

        #region Methods

        public ResultArgs SaveAssetInsurance()
        {
            try
            {
                using (DataManager datamanager = new DataManager())
                {
                    datamanager.BeginTransaction();

                    if (InsId > 0)
                    {
                        resultArgs = DeleteInsuranceDetails();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            resultArgs = DeleteInsuranceMasterDetails();
                        }
                    }
                    resultArgs = SaveInsuranceMaster();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        InsId = InsId == 0 ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : InsId;
                        resultArgs = SaveInsuranceMasterDetails();
                    }
                    datamanager.EndTransaction();
                }

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            return resultArgs;
        }

        private ResultArgs SaveInsuranceDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.AssetInsuranchVoucher.AddInsuranceDetails))
                {
                    if (IsInsuranceDetailsNeed.Equals((int)YesNo.Yes))
                    {
                        dataManager.Parameters.Add(this.AppSchema.InsuranceMasterData.INS_IDColumn, InsId);
                        dataManager.Parameters.Add(this.AppSchema.InsuranceData.PROVIDERColumn, Provider);
                        dataManager.Parameters.Add(this.AppSchema.InsuranceData.AGENTColumn, Agent);
                        dataManager.Parameters.Add(this.AppSchema.InsuranceData.INSURANCE_TYPE_IDColumn, InsuranceType);
                        dataManager.Parameters.Add(this.AppSchema.InsuranceData.POLICYColumn, Policy);
                        dataManager.Parameters.Add(this.AppSchema.InsuranceMasterData.ITEM_IDColumn, ItemId);
                        dataManager.Parameters.Add(this.AppSchema.InsuranceData.POLICNOColumn, PolicyNo);
                        dataManager.Parameters.Add(this.AppSchema.InsuranceData.PREMIUM_AMOUNTColumn, PremiumAmount);
                        dataManager.Parameters.Add(this.AppSchema.InsuranceData.START_DATEColumn, StartDate, false);
                        dataManager.Parameters.Add(this.AppSchema.InsuranceData.DUE_DATEColumn, DueDate, false);
                    }
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

        private ResultArgs SaveInsuranceMasterDetails()
        {
            try
            {
                int InsuranceDetailCount = 0;
                foreach (DataRow drInsurance in dtAssetInsurance.Rows)
                {
                    string AssetIds = (drInsurance[this.AppSchema.ASSETItem.ITEM_IDColumn.ColumnName] != null) ?
                        (drInsurance[this.AppSchema.ASSETItem.ASSET_IDColumn.ColumnName].ToString()) : string.Empty;
                    string[] AssetID = AssetIds.Split(',');
                    for (int i = 0; i < AssetID.Length; i++)
                    {
                        GroupId = (drInsurance[this.AppSchema.AssetPurchaseDetail.GROUP_IDColumn.ColumnName] != null) ?
                            this.NumberSet.ToInteger(drInsurance[this.AppSchema.AssetPurchaseDetail.GROUP_IDColumn.ColumnName].ToString()) : 0;
                        LocationId = (drInsurance[this.AppSchema.AssetPurchaseDetail.LOCATION_IDColumn.ColumnName] != null) ?
                            this.NumberSet.ToInteger(drInsurance[this.AppSchema.AssetPurchaseDetail.LOCATION_IDColumn.ColumnName].ToString()) : 0;
                        ItemId = (drInsurance[this.AppSchema.AssetPurchaseDetail.ITEM_IDColumn.ColumnName] != null) ?
                            this.NumberSet.ToInteger(drInsurance[this.AppSchema.AssetPurchaseDetail.ITEM_IDColumn.ColumnName].ToString()) : 0;
                        AssetId = AssetID[i].Trim();
                        IsInsuranceDetailsNeed = (drInsurance["Id"] != null) ?
                            this.NumberSet.ToInteger(drInsurance["Id"].ToString()) : 0;
                        Value = (drInsurance[this.AppSchema.InsuranceMasterData.VALUEColumn.ColumnName] != null) ?
                            this.NumberSet.ToDouble(drInsurance[this.AppSchema.InsuranceMasterData.VALUEColumn.ColumnName].ToString()) : 0;
                        if (LocationId > 0 && ItemId > 0 && GroupId > 0)
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.AssetInsuranchVoucher.AddInsuranceMastersDetail))
                            {
                                dataManager.Parameters.Add(this.AppSchema.InsuranceMasterData.INS_IDColumn, InsId, true);
                                dataManager.Parameters.Add(this.AppSchema.InsuranceMasterData.ASSET_GROUP_IDColumn, GroupId);
                                dataManager.Parameters.Add(this.AppSchema.InsuranceMasterData.ASSET_LOCATION_IDColumn, LocationId);
                                dataManager.Parameters.Add(this.AppSchema.InsuranceMasterData.ITEM_IDColumn, ItemId);
                                dataManager.Parameters.Add(this.AppSchema.InsuranceMasterData.ASSET_IDColumn, AssetId);
                                dataManager.Parameters.Add(this.AppSchema.InsuranceMasterData.IS_INSURANCE_NEEDColumn, IsInsuranceDetailsNeed);
                                dataManager.Parameters.Add(this.AppSchema.InsuranceMasterData.VALUEColumn, Value);
                                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                                resultArgs = dataManager.UpdateData();
                                if (resultArgs.Success)
                                {
                                    if (IsInsuranceDetailsNeed.Equals((int)YesNo.Yes))
                                    {
                                        if (dsInsuranceDetails != null && dsInsuranceDetails.Tables.Count > 0)
                                        {
                                            ItemId = this.NumberSet.ToInteger(drInsurance["ITEM_ID"].ToString());
                                            string InsureanceDetail = InsuranceDetailCount + "INS" + ItemId;
                                            if (dsInsuranceDetails.Tables.Contains(InsureanceDetail))
                                            {
                                                DataTable dtInsuranceTemp = dsInsuranceDetails.Tables[InsureanceDetail];
                                                InsuranceType = dtInsuranceTemp.Rows[0][this.AppSchema.InsurancePlan.INSURANCE_PLAN_IDColumn.ColumnName] != null ? this.NumberSet.ToInteger(dtInsuranceTemp.Rows[0][this.AppSchema.InsurancePlan.INSURANCE_PLAN_IDColumn.ColumnName].ToString()) : 0;
                                                Provider = dtInsuranceTemp.Rows[0][this.AppSchema.InsuranceData.PROVIDERColumn.ColumnName] != null ? (dtInsuranceTemp.Rows[0][this.AppSchema.InsuranceData.PROVIDERColumn.ColumnName].ToString()) : string.Empty;
                                                Agent = dtInsuranceTemp.Rows[0][this.AppSchema.InsuranceData.AGENTColumn.ColumnName] != null ? (dtInsuranceTemp.Rows[0][this.AppSchema.InsuranceData.AGENTColumn.ColumnName].ToString()) : string.Empty;
                                                Policy = dtInsuranceTemp.Rows[0][this.AppSchema.InsuranceData.POLICYColumn.ColumnName] != null ? (dtInsuranceTemp.Rows[0][this.AppSchema.InsuranceData.POLICYColumn.ColumnName].ToString()) : string.Empty;
                                                PolicyNo = dtInsuranceTemp.Rows[0][this.AppSchema.InsuranceData.POLICNOColumn.ColumnName] != null ? (dtInsuranceTemp.Rows[0][this.AppSchema.InsuranceData.POLICNOColumn.ColumnName].ToString()) : string.Empty;
                                                PremiumAmount = dtInsuranceTemp.Rows[0][this.AppSchema.InsuranceData.PREMIUM_AMOUNTColumn.ColumnName] != null ? this.NumberSet.ToDouble(dtInsuranceTemp.Rows[0][this.AppSchema.InsuranceData.PREMIUM_AMOUNTColumn.ColumnName].ToString()) : 0;
                                                StartDate = this.DateSet.ToDate(dtInsuranceTemp.Rows[0][this.AppSchema.InsuranceData.START_DATEColumn.ColumnName].ToString(), false);
                                                DueDate = this.DateSet.ToDate(dtInsuranceTemp.Rows[0][this.AppSchema.InsuranceData.DUE_DATEColumn.ColumnName].ToString(), false);
                                                resultArgs = DeleteInsuranceDetails();
                                                if (resultArgs.Success)
                                                {
                                                    resultArgs = SaveInsuranceDetails();
                                                }
                                            }
                                        }
                                        //    InsuranceDetailCount++;
                                    }
                                }
                            }
                        }
                    }
                    InsuranceDetailCount++;
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

        private ResultArgs SaveInsuranceMaster()
        {
            try
            {
                using (DataManager dataManager = new DataManager((InsId == 0) ? SQLCommand.AssetInsuranchVoucher.SaveInsuranceMaster : SQLCommand.AssetInsuranchVoucher.UpdateInsuranceMaster))
                {
                    dataManager.Parameters.Add(this.AppSchema.InsuranceMasterData.INS_IDColumn, InsId, true);
                    dataManager.Parameters.Add(this.AppSchema.InsuranceMasterData.VOUCHER_DATEColumn, VoucherDate);
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, 1);
                    dataManager.Parameters.Add(this.AppSchema.InsuranceMasterData.LEDGER_IDColumn, LedgerId);
                    dataManager.Parameters.Add(this.AppSchema.InsuranceMasterData.NAME_ADDRESSColumn, NameAddress);
                    dataManager.Parameters.Add(this.AppSchema.InsuranceMasterData.NARRATIONColumn, Narration);
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

        public ResultArgs FetchAllInsuranceMasterDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.AssetInsuranchVoucher.FetchAllInsuranceMastersDetail))
                {
                    dataManager.Parameters.Add(this.AppSchema.InsuranceMasterData.INS_IDColumn, InsId);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
                return resultArgs;
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            { }

            return resultArgs;
        }

        public ResultArgs FetchInsRenewalDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.AssetInsuranchVoucher.RenewInsurance))
                {
                    dataManager.Parameters.Add(this.AppSchema.InsuranceMasterData.INS_IDColumn, InsId);
                    dataManager.Parameters.Add(this.AppSchema.InsuranceMasterData.ITEM_IDColumn, ItemId);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
                return resultArgs;
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            return resultArgs;
        }

        public ResultArgs FetchInsDetailsByProject()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInsuranchVoucher.FetchInsDetailbyProject))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, DateTo);
                dataManager.Parameters.Add(this.AppSchema.Budget.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchRenewalByProject()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInsuranchVoucher.FetchRenewalByProject))
            {
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.Budget.DATE_TOColumn, DateTo);
                dataManager.Parameters.Add(this.AppSchema.Budget.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public DataSet FetchInsuranceByProject()
        {
            string VoucherID = string.Empty;
            DataSet dsInsurance = new DataSet();
            try
            {
                resultArgs = FetchInsDetailsByProject();
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    InsId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.InsuranceMasterData.INS_IDColumn.ColumnName].ToString());
                    resultArgs.DataSource.Table.TableName = "Master";
                    dsInsurance.Tables.Add(resultArgs.DataSource.Table);

                    //  resultArgs = FetchAllInsuranceMasterDetails();
                    resultArgs = FetchRenewalByProject();
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        resultArgs.DataSource.Table.TableName = "InsuranceRenewalDetail";
                        dsInsurance.Tables.Add(resultArgs.DataSource.Table);
                        dsInsurance.Relations.Add(dsInsurance.Tables[1].TableName, dsInsurance.Tables[0].Columns[this.AppSchema.InsuranceMasterData.INS_IDColumn.ColumnName], dsInsurance.Tables[1].Columns[this.AppSchema.InsuranceMasterData.INS_IDColumn.ColumnName]);
                    }
                    //resultArgs = FetchAllInsuranceDetail();
                    //if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    //{
                    //    resultArgs.DataSource.Table.TableName = "AssetInsuranceDetail";
                    //    dsInsurance.Tables.Add(resultArgs.DataSource.Table);
                    //    dsInsurance.Relations.Add(dsInsurance.Tables[2].TableName, dsInsurance.Tables[0].Columns[this.AppSchema.InsuranceMasterData.INS_IDColumn.ColumnName], dsInsurance.Tables[2].Columns[this.AppSchema.InsuranceMasterData.INS_IDColumn.ColumnName]);
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
            return dsInsurance;
        }

        public ResultArgs FetchAllInsuranceDetail()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.AssetInsuranchVoucher.FetchAllInsuranceDetails))
                {
                    dataManager.Parameters.Add(this.AppSchema.InsuranceMasterData.INS_IDColumn, InsId);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
                return resultArgs;
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            { }

            return resultArgs;
        }

        public ResultArgs FetchInsuranceMasterDetail()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInsuranchVoucher.FetchInsuranceMastersDetail))
            {
                dataManager.Parameters.Add(this.AppSchema.InsuranceMasterData.INS_IDColumn.ColumnName, this.InsId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs != null && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    dtEditInsuranceMasters = resultArgs.DataSource.Table;
                    IsInsuranceDetailsNeed = NumberSet.ToInteger(dtEditInsuranceMasters.Rows[0]["Id"].ToString());
                }
            }
            return resultArgs;
        }

        public ResultArgs FetchInsuranceMasters()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInsuranchVoucher.FetchInsuanceMaster))
            {
                dataManager.Parameters.Add(this.AppSchema.InsuranceMasterData.INS_IDColumn.ColumnName, this.InsId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs != null && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    VoucherDate = DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.InsuranceMasterData.VOUCHER_DATEColumn.ColumnName].ToString(), false);
                    Project = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                    ProjectId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString());
                    //     VoucherNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.InsuranceMasterData.VOUCHER_NOColumn.ColumnName].ToString();
                    LedgerId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.InsuranceMasterData.LEDGER_IDColumn.ColumnName].ToString());
                    NameAddress = resultArgs.DataSource.Table.Rows[0][this.AppSchema.InsuranceMasterData.NAME_ADDRESSColumn.ColumnName].ToString();
                    Narration = resultArgs.DataSource.Table.Rows[0][this.AppSchema.InsuranceMasterData.NARRATIONColumn.ColumnName].ToString();
                }
            }
            return resultArgs;
        }

        public ResultArgs FetchInsuranceDetails(int MasterId, int ItemId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInsuranchVoucher.FetchInsuranceDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.InsuranceMasterData.INS_IDColumn.ColumnName, MasterId);
                if (ItemId > 0)
                    dataManager.Parameters.Add(this.AppSchema.InsuranceMasterData.ITEM_IDColumn.ColumnName, ItemId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                DataTable dtIns = dtEditInsuranceDetails = resultArgs.DataSource.Table;
                if (dtIns != null && dtIns.Rows.Count > 0)
                {
                    if (IsInsuranceDetailsNeed.Equals((int)YesNo.Yes))
                    {
                        if (dtIns != null && dtIns.Rows.Count > 0)
                        {
                            InsId = dtIns.Rows[0][this.AppSchema.InsuranceMasterData.INS_IDColumn.ColumnName] != null ? this.NumberSet.ToInteger(dtIns.Rows[0][this.AppSchema.InsuranceMasterData.INS_IDColumn.ColumnName].ToString()) : 0;
                            InsuranceType = dtIns.Rows[0][this.AppSchema.InsurancePlan.INSURANCE_PLAN_IDColumn.ColumnName] != null ? this.NumberSet.ToInteger(dtIns.Rows[0][this.AppSchema.InsurancePlan.INSURANCE_PLAN_IDColumn.ColumnName].ToString()) : 0;
                            Provider = dtIns.Rows[0][this.AppSchema.InsuranceData.PROVIDERColumn.ColumnName] != null ? (dtIns.Rows[0][this.AppSchema.InsuranceData.PROVIDERColumn.ColumnName].ToString()) : string.Empty;
                            Agent = dtIns.Rows[0][this.AppSchema.InsuranceData.AGENTColumn.ColumnName] != null ? (dtIns.Rows[0][this.AppSchema.InsuranceData.AGENTColumn.ColumnName].ToString()) : string.Empty;
                            Policy = dtIns.Rows[0][this.AppSchema.InsuranceData.POLICYColumn.ColumnName] != null ? (dtIns.Rows[0][this.AppSchema.InsuranceData.POLICYColumn.ColumnName].ToString()) : string.Empty;
                            PolicyNo = dtIns.Rows[0][this.AppSchema.InsuranceData.POLICNOColumn.ColumnName] != null ? (dtIns.Rows[0][this.AppSchema.InsuranceData.POLICNOColumn.ColumnName].ToString()) : string.Empty;
                            PremiumAmount = dtIns.Rows[0][this.AppSchema.InsuranceData.PREMIUM_AMOUNTColumn.ColumnName] != null ? this.NumberSet.ToDouble(dtIns.Rows[0][this.AppSchema.InsuranceData.PREMIUM_AMOUNTColumn.ColumnName].ToString()) : 0;
                            StartDate = this.DateSet.ToDate(dtIns.Rows[0][this.AppSchema.InsuranceData.START_DATEColumn.ColumnName].ToString(), false);
                            DueDate = this.DateSet.ToDate(dtIns.Rows[0][this.AppSchema.InsuranceData.DUE_DATEColumn.ColumnName].ToString(), false);
                        }
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs DeleteInsurance()
        {
            try
            {
                using (DataManager datamanager = new DataManager())
                {
                    datamanager.BeginTransaction();
                    if (InsId > 0)
                    {
                        resultArgs = DeleteInsuranceDetails();
                        if (resultArgs.Success)
                        {
                            resultArgs = DeleteInsuranceMasterDetails();
                            if (resultArgs.Success)
                            {
                                resultArgs = DeleteInsuranceMasters();
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
            return resultArgs;
        }

        public ResultArgs DeleteInsuranceDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInsuranchVoucher.DeleteInsuranceDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.InsuranceMasterData.INS_IDColumn, InsId);
                if (ItemId > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.InsuranceMasterData.ITEM_IDColumn, ItemId);
                }
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteInsuranceMasterDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInsuranchVoucher.DeleteInsuranceMastersDetail))
            {
                dataManager.Parameters.Add(this.AppSchema.InsuranceMasterData.INS_IDColumn, InsId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteInsuranceMasters()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetInsuranchVoucher.DeleteInsuranceMaster))
            {
                dataManager.Parameters.Add(this.AppSchema.InsuranceMasterData.INS_IDColumn, InsId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        #endregion
    }
}
