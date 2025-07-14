using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.Model.Stock
{
    public class StockUnitOfMeassureSystem : SystemBase, AssetStockProduct.IMeasure
    {
        #region Variable Declaration
        public int UnitId = 0;
        ResultArgs resultArgs = null;
        CommonMember UtilityMember = new CommonMember();
        #endregion

        #region Constructor
        public StockUnitOfMeassureSystem()
        {

        }
        public StockUnitOfMeassureSystem(int Unitofmeasure_id)
        {
            UnitId = Unitofmeasure_id;
            AssignMeasureProperties();
        }
        #endregion

        #region Properties
        public int unitId { get; set; }
        public string TYPE { get; set; }
        public string SYMBOL { get; set; }
        public string NAME { get; set; }
        public int ConversionOf { get; set; }
        public string FirstUnitId { get; set; }
        public string SecondUnitId { get; set; }
        public int DecimalPlace { get; set; }
        public int TypeId { get; set; }
        #endregion

        #region Methods
        public ResultArgs SaveMeasureDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager((unitId == 0) ? SQLCommand.StockUnitofMeasure.Add : SQLCommand.StockUnitofMeasure.Update))
                {
                    dataManager.Parameters.Add(this.AppSchema.StockUnitofMeasure.UNIT_IDColumn, unitId);
                    dataManager.Parameters.Add(this.AppSchema.StockUnitofMeasure.TYPEColumn, TYPE);
                    dataManager.Parameters.Add(this.AppSchema.StockUnitofMeasure.SYMBOLColumn, SYMBOL);
                    dataManager.Parameters.Add(this.AppSchema.StockUnitofMeasure.NAMEColumn, NAME);
                    dataManager.Parameters.Add(this.AppSchema.StockUnitofMeasure.CONVERSION_OFColumn, ConversionOf);
                    dataManager.Parameters.Add(this.AppSchema.StockUnitofMeasure.SECOND_UNIT_IDColumn, SecondUnitId);
                    dataManager.Parameters.Add(this.AppSchema.StockUnitofMeasure.FIRST_UNIT_IDColumn, FirstUnitId);
                    dataManager.Parameters.Add(this.AppSchema.StockUnitofMeasure.DECIMAL_PLACEColumn, DecimalPlace);
                    dataManager.Parameters.Add(this.AppSchema.StockUnitofMeasure.UNITTYPE_IDColumn, TypeId);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.UpdateData();
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

        public ResultArgs DeleteMeasureDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockUnitofMeasure.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.StockUnitofMeasure.UNIT_IDColumn, unitId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchMeasureDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockUnitofMeasure.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.StockUnitofMeasure.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.StockUnitofMeasure.UNIT_IDColumn, unitId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public void AssignMeasureProperties()
        {
            using (DataManager dataManager = new DataManager())
            {
                resultArgs = FetchById();
                if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs != null)
                {
                    NAME = resultArgs.DataSource.Table.Rows[0][this.AppSchema.StockUnitofMeasure.NAMEColumn.ColumnName].ToString();
                    TYPE = resultArgs.DataSource.Table.Rows[0][this.AppSchema.StockUnitofMeasure.TYPEColumn.ColumnName].ToString();
                    SYMBOL = resultArgs.DataSource.Table.Rows[0][this.AppSchema.StockUnitofMeasure.SYMBOLColumn.ColumnName].ToString();
                    ConversionOf = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.StockUnitofMeasure.CONVERSION_OFColumn.ColumnName].ToString());
                    FirstUnitId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.StockUnitofMeasure.FIRST_UNIT_IDColumn.ColumnName].ToString();
                    SecondUnitId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.StockUnitofMeasure.SECOND_UNIT_IDColumn.ColumnName].ToString();
                    TypeId = UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.StockUnitofMeasure.UNITTYPE_IDColumn.ColumnName].ToString());
                    unitId = UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.StockUnitofMeasure.UNIT_IDColumn.ColumnName].ToString());
                    DecimalPlace = UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.StockUnitofMeasure.DECIMAL_PLACEColumn.ColumnName].ToString());
                }
            }
        }

        public ResultArgs FetchUnitsForGridLookUP()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.StockUnitofMeasure.FetchForFirstUnit))
            {
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        #endregion
    }
}
