using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.Model
{
    public class AssetUnitOfMeassureSystem : SystemBase
    {
        #region Variable Declaration
        public int UnitId = 0;
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public AssetUnitOfMeassureSystem()
        {

        }

        public AssetUnitOfMeassureSystem(int Unitofmeasure_id)
        {
            UnitId = Unitofmeasure_id;
            AssignMeasureProperties();
        }
        #endregion

        #region Properties
        public int unitId { get; set; }
        public int TYPE { get; set; }
        public string SYMBOL { get; set; }
        public string NAME { get; set; }
        public int ConversionOf { get; set; }
        public int FirstUnitId { get; set; }
        public int SecondUnitId { get; set; }
        public int DecimalPlace { get; set; }
        public int TypeId { get; set; }
        #endregion

        #region Methods

        public ResultArgs SaveMeasureDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager((unitId == 0) ? SQLCommand.AssetUnitOfMeasure.Add : SQLCommand.AssetUnitOfMeasure.Update))
                {
                    dataManager.Parameters.Add(this.AppSchema.ASSETUnitOfMeassure.UOM_IDColumn, unitId,true);
                    dataManager.Parameters.Add(this.AppSchema.ASSETUnitOfMeassure.SYMBOLColumn, SYMBOL);
                    dataManager.Parameters.Add(this.AppSchema.ASSETUnitOfMeassure.NAMEColumn, NAME);
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
            using (DataManager dataManager = new DataManager(SQLCommand.AssetUnitOfMeasure.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETUnitOfMeassure.UOM_IDColumn, unitId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchMeasureDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetUnitOfMeasure.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetUnitOfMeasure.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETUnitOfMeassure.UOM_IDColumn, unitId);
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
                    NAME = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETUnitOfMeassure.NAMEColumn.ColumnName].ToString();
                    SYMBOL = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETUnitOfMeassure.SYMBOLColumn.ColumnName].ToString();
                    unitId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETUnitOfMeassure.UOM_IDColumn.ColumnName].ToString());
                }
            }
        }

        public ResultArgs FetchUnitsForGridLookUP()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetUnitOfMeasure.FetchForFirstUnit))
            {
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int FetchUnitOfMeasureId()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetUnitOfMeasure.FetchUnitOfMeasureId))
            {
                datamanager.Parameters.Add(this.AppSchema.StockUnitofMeasure.SYMBOLColumn, SYMBOL);
                resultArgs = datamanager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        #endregion
    }
}
