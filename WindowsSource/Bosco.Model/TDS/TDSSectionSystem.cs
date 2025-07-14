using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;

namespace Bosco.Model.TDS
{
    public class TDSSectionSystem : SystemBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public TDSSectionSystem()
        {

        }
        public TDSSectionSystem(int SectionId)
        {
            FillTDSSectionProperties(SectionId);
        }
        #endregion

        #region properties
        public int TDS_section_Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int IsActive { get; set; }
        #endregion

        #region Methods
        public ResultArgs FetchTDSSectionDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSSection.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteTDSSectionDetails(int SectionId)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.TDSSection.Delete))
            {
                dataMember.Parameters.Add(this.AppSchema.TDSSection.TDS_SECTION_IDColumn, SectionId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveTDSSection()
        {
            using (DataManager dataManager = new DataManager((TDS_section_Id == 0) ? SQLCommand.TDSSection.Add : SQLCommand.TDSSection.Update))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSSection.CODEColumn, Code);
                dataManager.Parameters.Add(this.AppSchema.TDSSection.SECTION_NAMEColumn, Name);
                dataManager.Parameters.Add(this.AppSchema.TDSSection.TDS_SECTION_IDColumn, TDS_section_Id);
                dataManager.Parameters.Add(this.AppSchema.DeducteeTypes.STATUSColumn, IsActive);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public void FillTDSSectionProperties(int SectionId)
        {
            resultArgs = FetchTDSSectionDetailsById(SectionId);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                Code = resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSSection.CODEColumn.ColumnName].ToString();
                Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSSection.SECTION_NAMEColumn.ColumnName].ToString();
                IsActive = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DeducteeTypes.STATUSColumn.ColumnName].ToString());
            }
        }

        private ResultArgs FetchTDSSectionDetailsById(int SectionId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSSection.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSSection.TDS_SECTION_IDColumn, SectionId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int CheckTDSSection()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSSection.TDSSection))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSSection.TDS_SECTION_IDColumn, TDS_section_Id);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        #endregion
    }
}
