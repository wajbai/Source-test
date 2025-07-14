using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using System.Data;

namespace Bosco.Model
{
    public class DepriciationVoucherSystem : SystemBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Property
        public int DepriciationId { get; set; }
        public DateTime DepreciationDate { get; set; }
        public int LocationId { get; set; }
        public int AssetGroupId { get; set; }
        public int VoucherId { get; set; }
        public int BranchId { get; set; }
        public int purpose { get; set; }
        public DateTime ToDate { get; set; }
        public int ProjectId { get; set; }

        public int ItemId { get; set; }
        public int AssetId { get; set; }
        public DateTime purchaseOn { get; set; }
        public decimal value { get; set; }
        public decimal DepreciationAmount { get; set; }

        public DateTime FromDate { get; set; }
        public DataTable dtDepreciationDetails { get; set; }
        #endregion

        #region Constructor
        public DepriciationVoucherSystem()
        {

        }
        public DepriciationVoucherSystem(int depreciationId)
        {
            DepriciationId = depreciationId;
            FillDepreciationVoucherDetails(depreciationId);
        }
        #endregion

        #region Methods
        public ResultArgs SaveDepreciation()
        {
            using (DataManager datamanager = new DataManager())
            {
                datamanager.BeginTransaction();

                if (DepriciationId > 0)
                {
                    resultArgs = DeleteDepreciationDetail(DepriciationId);
                }
                resultArgs = SaveDepreciationVoucherMaster();
                if (resultArgs != null && resultArgs.Success)
                {
                    if (DepriciationId == 0)
                    {
                        DepriciationId = resultArgs.RowUniqueId != null ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : 0;
                    }
                    if (DepriciationId > 0)
                    {
                        resultArgs = SaveDepreciationVoucherDetail();
                    }
                }
                datamanager.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs SaveDepreciationVoucherMaster()
        {
            using (DataManager datamanager = new DataManager((DepriciationId == 0) ? SQLCommand.Depriciation.DepreciationMasteAdd : SQLCommand.Depriciation.DepreciationMasterEdit))
            {
                datamanager.Parameters.Add(this.AppSchema.DepriciationVoucherMaster.DEP_IDColumn, DepriciationId, true);
                datamanager.Parameters.Add(this.AppSchema.DepriciationVoucherMaster.DEP_DATEColumn, DepreciationDate);
                datamanager.Parameters.Add(this.AppSchema.DepriciationVoucherMaster.LOCATION_IDColumn, LocationId);
                datamanager.Parameters.Add(this.AppSchema.DepriciationVoucherMaster.GROUP_IDColumn, AssetGroupId);
                datamanager.Parameters.Add(this.AppSchema.DepriciationVoucherMaster.PURPOSE_IDColumn, purpose);
                datamanager.Parameters.Add(this.AppSchema.DepriciationVoucherMaster.TO_DATEColumn, ToDate);
                datamanager.Parameters.Add(this.AppSchema.DepriciationVoucherMaster.VOUCHER_IDColumn, VoucherId);
                datamanager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                datamanager.Parameters.Add(this.AppSchema.DepriciationVoucherMaster.BRANCH_IDColumn, BranchId);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs SaveDepreciationVoucherDetail()
        {
            foreach (DataRow drDepreciation in dtDepreciationDetails.Rows)
            {
                using (DataManager datamanager = new DataManager(SQLCommand.Depriciation.AddDepreciationVoucherDetail))
                {
                    datamanager.Parameters.Add(this.AppSchema.DepriciationVoucherDetail.DEP_IDColumn, DepriciationId);
                    datamanager.Parameters.Add(this.AppSchema.DepriciationVoucherDetail.ITEM_IDColumn, (drDepreciation[this.AppSchema.DepriciationVoucherDetail.ITEM_IDColumn.ColumnName] != null) ? this.NumberSet.ToInteger(drDepreciation[this.AppSchema.DepriciationVoucherDetail.ITEM_IDColumn.ColumnName].ToString()) : 0);
                    datamanager.Parameters.Add(this.AppSchema.DepriciationVoucherDetail.ASSET_IDColumn, drDepreciation[this.AppSchema.DepriciationVoucherDetail.ASSET_IDColumn.ColumnName] != null ? drDepreciation[this.AppSchema.DepriciationVoucherDetail.ASSET_IDColumn.ColumnName].ToString() : string.Empty);
                    datamanager.Parameters.Add(this.AppSchema.DepriciationVoucherDetail.PURCHASED_ONColumn, drDepreciation[this.AppSchema.DepriciationVoucherDetail.PURCHASED_ONColumn.ColumnName] != null ? DateSet.ToDate(drDepreciation[this.AppSchema.DepriciationVoucherDetail.PURCHASED_ONColumn.ColumnName].ToString(), false) : DepreciationDate);
                    datamanager.Parameters.Add(this.AppSchema.DepriciationVoucherDetail.VALUEColumn, drDepreciation[this.AppSchema.DepriciationVoucherDetail.VALUEColumn.ColumnName] != null ? this.NumberSet.ToInteger(drDepreciation[this.AppSchema.DepriciationVoucherDetail.VALUEColumn.ColumnName].ToString()) : 0);
                    datamanager.Parameters.Add(this.AppSchema.DepriciationVoucherDetail.DEP_AMOUNTColumn, drDepreciation["DEP_PERCENTAGE"] != null ? this.NumberSet.ToDecimal(drDepreciation["DEP_PERCENTAGE"].ToString()) : 0);
                    resultArgs = datamanager.UpdateData();

                }
            }
            return resultArgs;
        }

        private void FillDepreciationVoucherDetails(int DepId)
        {
            resultArgs = FetchById(DepId);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                DepriciationId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DepriciationVoucherMaster.DEP_IDColumn.ColumnName].ToString());
                DepreciationDate = DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DepriciationVoucherMaster.DEP_DATEColumn.ColumnName].ToString(), false);
                LocationId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DepriciationVoucherMaster.LOCATION_IDColumn.ColumnName].ToString());
                AssetGroupId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DepriciationVoucherMaster.GROUP_IDColumn.ColumnName].ToString());
                purpose = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DepriciationVoucherMaster.PURPOSE_IDColumn.ColumnName].ToString());
                ToDate = DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DepriciationVoucherMaster.TO_DATEColumn.ColumnName].ToString(), false);
                BranchId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DepriciationVoucherMaster.BRANCH_IDColumn.ColumnName].ToString());
                VoucherId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DepriciationVoucherMaster.VOUCHER_IDColumn.ColumnName].ToString());
            }
        }

        private ResultArgs FetchById(int DepreciationID)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.Depriciation.FetchDepreciationMaster))
            {
                datamanager.Parameters.Add(this.AppSchema.DepriciationVoucherMaster.DEP_IDColumn, DepreciationID);
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteDepreciation(int DepreciationId)
        {
            try
            {
                using (DataManager datamanager = new DataManager())
                {
                    datamanager.BeginTransaction();
                    resultArgs = DeleteDepreciationDetail(DepreciationId);
                    if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        resultArgs = DeleteDepreciationMaster(DepreciationId);
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

        private ResultArgs DeleteDepreciationMaster(int DepreciationId)
        {
            try
            {
                using (DataManager datamanager = new DataManager(SQLCommand.Depriciation.DeleteMaster))
                {
                    datamanager.Parameters.Add(this.AppSchema.DepriciationVoucherMaster.DEP_IDColumn, DepreciationId);
                    resultArgs = datamanager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }

            return resultArgs;
        }

        private ResultArgs DeleteDepreciationDetail(int DepreciationId)
        {
            try
            {
                using (DataManager datamanager = new DataManager(SQLCommand.Depriciation.DeleteDepreciationDetail))
                {
                    datamanager.Parameters.Add(this.AppSchema.DepriciationVoucherMaster.DEP_IDColumn, DepreciationId);
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

        public DataSet FetchAssetDepreciationDetails()
        {
            DataSet dsDepreciation = new DataSet();
            string DepId = string.Empty;
            try
            {
                resultArgs = FetchDepreciation();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null
                    && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    resultArgs.DataSource.Table.TableName = "Master";
                    dsDepreciation.Tables.Add(resultArgs.DataSource.Table);

                    foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                    {
                        DepId += dr[this.AppSchema.DepriciationVoucherMaster.DEP_IDColumn.ColumnName].ToString() + ",";
                    }
                    DepId = DepId.TrimEnd(',');

                    resultArgs = FetchDepreciationDetails(DepId);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        resultArgs.DataSource.Table.TableName = "Details";
                        dsDepreciation.Tables.Add(resultArgs.DataSource.Table);
                    }
                    dsDepreciation.Relations.Add(dsDepreciation.Tables[1].TableName, dsDepreciation.Tables[0].Columns[this.AppSchema.DepriciationVoucherMaster.DEP_IDColumn.ColumnName], dsDepreciation.Tables[1].Columns[this.AppSchema.DepriciationVoucherMaster.DEP_IDColumn.ColumnName]);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
            return dsDepreciation;
        }

        private ResultArgs FetchDepreciation()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.Depriciation.FetchAll))
            {
                datamanager.Parameters.Add(this.AppSchema.DepriciationVoucherMaster.TO_DATEColumn, ToDate);
                datamanager.Parameters.Add(this.AppSchema.DepriciationVoucherMaster.FROM_DATEColumn, FromDate);
                datamanager.Parameters.Add(this.AppSchema.DepriciationVoucherMaster.PROJECT_IDColumn, ProjectId);
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchDepreciationDetails(string DepreciationId)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.Depriciation.FechDepreciationDetails))
            {
                datamanager.Parameters.Add(this.AppSchema.DepriciationVoucherMaster.DEP_IDColumn, DepreciationId);
                datamanager.Parameters.Add(this.AppSchema.DepriciationVoucherMaster.TO_DATEColumn, ToDate);
                datamanager.Parameters.Add(this.AppSchema.DepriciationVoucherMaster.FROM_DATEColumn, FromDate);
                datamanager.Parameters.Add(this.AppSchema.DepriciationVoucherMaster.PROJECT_IDColumn, ProjectId);
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion
    }
}
