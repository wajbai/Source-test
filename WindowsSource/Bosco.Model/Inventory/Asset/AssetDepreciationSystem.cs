using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using Bosco.Model.Transaction;


namespace Bosco.Model
{
    public class AssetDepreciationSystem : SystemBase
    {
        #region Variable Decelaration
        ResultArgs resultArgs = null;
        #endregion

        #region constructor
        public AssetDepreciationSystem()
        {
        }
        public AssetDepreciationSystem(int Depreciation_Id)
        {
            DepreciationId = Depreciation_Id;
            FillDepreciationProperties(Depreciation_Id);
        }
        #endregion

        #region Depreciation Properties
        public int DepreciationId { get; set; }
        public string DepreciationCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public DateTime DepFrom { get; set; }
        public DateTime DepTo { get; set; }
        public int VoucherID { get; set; }
        #endregion

        #region Methods

        public ResultArgs SaveDepreciationDetials()
        {
            using (DataManager datamanager = new DataManager((DepreciationId == 0) ? SQLCommand.AssetDepreciation.Add : SQLCommand.AssetDepreciation.Update))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETDepreciationDetails.METHOD_IDColumn, DepreciationId);

                datamanager.Parameters.Add(this.AppSchema.ASSETDepreciationDetails.DEP_METHODColumn, Name);
                datamanager.Parameters.Add(this.AppSchema.ASSETDepreciationDetails.DESCRIPTIONColumn, Description);
                resultArgs = datamanager.UpdateData();

            }
            return resultArgs;
        }

        public void FillDepreciationProperties(int DepreciationId)
        {
            resultArgs = FetchById(DepreciationId);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETDepreciationDetails.DEP_METHODColumn.ColumnName].ToString();
                Description = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETDepreciationDetails.DESCRIPTIONColumn.ColumnName].ToString();

            }

        }

        public ResultArgs FetchAll()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetDepreciation.FetchAll))
            {
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchDepreciationMethods()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetDepreciation.FetchDepMethods))
            {
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.FetchData(DataSource.DataTable);

            }
            return resultArgs;
        }

        public ResultArgs DeleteDepreciation(int DepreciationId)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetDepreciation.Delete))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETDepreciationDetails.METHOD_IDColumn, DepreciationId);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs FetchById(int Depreciation_Id)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetDepreciation.Fetch))
            {
                datamanager.Parameters.Add(this.AppSchema.ASSETDepreciationDetails.METHOD_IDColumn, Depreciation_Id);
                resultArgs = datamanager.FetchData(DataSource.DataTable);

            }
            return resultArgs;
        }
        public ResultArgs FetchAssetDepMaster()
        {
            using (DataManager dtManager = new DataManager(SQLCommand.AssetDepreciation.FetchDepreciationMaster))
            {
                dtManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dtManager.Parameters.Add(this.AppSchema.DepreciationMaster.DEPRECIATION_PERIOD_FROMColumn, DepFrom);
                dtManager.Parameters.Add(this.AppSchema.DepreciationMaster.DEPRECIATION_PERIOD_TOColumn, DepTo);
                resultArgs = dtManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchAssetDepDetail(string DepId)
        {
            using (DataManager dtManager = new DataManager(SQLCommand.AssetDepreciation.FetchDepreciationDetailById))
            {
                dtManager.Parameters.Add(this.AppSchema.DepreciationDetail.DEPRECIATION_IDColumn, DepId);
                dtManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dtManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public DataSet LoadDepreciationDetails()
        {
            string DepreciationId = string.Empty;
            DataSet dsDepMaster = new DataSet();
            try
            {
                resultArgs = FetchAssetDepMaster();
                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    DataView dvDepMaster = resultArgs.DataSource.Table.DefaultView;

                    dvDepMaster.ToTable().TableName = "Master";

                    resultArgs.DataSource.Table.TableName = "Master";

                    dsDepMaster.Tables.Add(dvDepMaster.ToTable());
                    for (int i = 0; i < dvDepMaster.ToTable().Rows.Count; i++)
                    {
                        DepreciationId += dvDepMaster.ToTable().Rows[i][AppSchema.DepreciationMaster.DEPRECIATION_IDColumn.ColumnName].ToString() + ",";
                    }
                    DepreciationId = DepreciationId.TrimEnd(',');

                    resultArgs = FetchAssetDepDetail(DepreciationId);
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        resultArgs.DataSource.Table.TableName = "Detail";
                        dsDepMaster.Tables.Add(resultArgs.DataSource.Table);
                        dsDepMaster.Relations.Add(dsDepMaster.Tables[1].TableName, dsDepMaster.Tables[0].Columns[this.AppSchema.DepreciationMaster.DEPRECIATION_IDColumn.ColumnName], dsDepMaster.Tables[1].Columns[this.AppSchema.DepreciationMaster.DEPRECIATION_IDColumn.ColumnName]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, false);
            }
            return dsDepMaster;
        }
        private ResultArgs DeleteDepDetail(DataManager dataProjectLedger)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetDepreciation.DeleteDepreciationDetailById))
            {
                dataManager.Database = dataProjectLedger.Database;
                dataManager.Parameters.Add(this.AppSchema.DepreciationDetail.DEPRECIATION_IDColumn, DepreciationId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        private ResultArgs DeleteDepMaster(DataManager dataProjectLedger)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetDepreciation.DeleteDepreciationMasterById))
            {
                dataManager.Database = dataProjectLedger.Database;
                dataManager.Parameters.Add(this.AppSchema.DepreciationDetail.DEPRECIATION_IDColumn, DepreciationId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        public ResultArgs DeleteDepreciationDetails()
        {
            using (DataManager deletedataManager = new DataManager())
            {
                deletedataManager.BeginTransaction();
                using (VoucherTransactionSystem transactionsystem = new VoucherTransactionSystem())
                {
                    transactionsystem.VoucherId = VoucherID;
                    resultArgs = transactionsystem.RemoveVoucher(deletedataManager);
                    if (resultArgs.Success)
                    {
                        resultArgs = DeleteDepDetail(deletedataManager);
                        if (resultArgs.Success)
                        {
                            resultArgs = DeleteDepMaster(deletedataManager);
                        }
                    }
                }
                deletedataManager.EndTransaction();
            }
            return resultArgs;
        }
        #endregion



    }
}
