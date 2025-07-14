using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.DAO.Schema;
using Bosco.DAO.Data;
namespace Bosco.Model
{
    public class BlockSystem : SystemBase
    {
        #region VaribleDeclaration
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Constructor
        public BlockSystem()
        {
        }
        public BlockSystem(int BlockId)
        {
            FillBlockDetails(BlockId);
        }
        #endregion

        #region Blo
        public int BlockId { get; set; }
        public string Block { get; set; }
        #endregion

        #region Methods
        public ResultArgs FetchBlockDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Block.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteBlockDetails(int blockId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Block.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.Block.BLOCK_IDColumn.ColumnName, blockId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveBlockDetails()
        {
            using (DataManager dataManager = new DataManager((BlockId == 0) ? SQLCommand.Block.Add : SQLCommand.Block.Update))
            {
                dataManager.Parameters.Add(this.AppSchema.Block.BLOCK_IDColumn, BlockId,true);
                dataManager.Parameters.Add(this.AppSchema.Block.BLOCKColumn, Block);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FillBlockDetails(int BlockId)
        {
            resultArgs = BlockDetailsByID(BlockId);
            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                Block = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Block.BLOCKColumn.ColumnName].ToString();
            }
            return resultArgs;
        }

        public ResultArgs BlockDetailsByID(int BlockId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Block.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.Block.BLOCK_IDColumn, BlockId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public int FetchBlockNameById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Block.FetchBlockIDByName))
            {
                dataManager.Parameters.Add(this.AppSchema.Block.BLOCKColumn, Block);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        #endregion
    }
}
