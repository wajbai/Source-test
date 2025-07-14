using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.Utility;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using System.Data;

namespace Bosco.Model.UIModel.Master
{
    public class GSTClassSystem : SystemBase
    {
        #region Property
        public int GstId { get; set; }
        public string Slab { get; set; }
        public decimal Gst { get; set; }
        public decimal CGst { get; set; }
        public decimal SGst { get; set; }
        public DateTime ApplicableFrom { get; set; }
        public int Status { get; set; }

        ResultArgs resultArgs = new ResultArgs();
        public DataTable dtGstDetails = new DataTable();

        #endregion

        #region Constructor

        public GSTClassSystem()
        {

        }
        public GSTClassSystem(int gstId)
            : this()
        {
            FillGSTDetails(gstId);
        }

        #endregion

        #region Methods

        public ResultArgs GetGSTDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.GSTDetails.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveGStDetails()
        {
            using (DataManager dataManager = new DataManager((GstId == 0) ? SQLCommand.GSTDetails.Add : SQLCommand.GSTDetails.Edit))
            {
                dataManager.Parameters.Add(this.AppSchema.MasterGSTClass.SLABColumn, Slab);
                dataManager.Parameters.Add(this.AppSchema.MasterGSTClass.GSTColumn, Gst);
                dataManager.Parameters.Add(this.AppSchema.MasterGSTClass.CGSTColumn, CGst);
                dataManager.Parameters.Add(this.AppSchema.MasterGSTClass.SGSTColumn, SGst);
                dataManager.Parameters.Add(this.AppSchema.MasterGSTClass.APPLICABLE_FROMColumn, ApplicableFrom);
                dataManager.Parameters.Add(this.AppSchema.MasterGSTClass.STATUSColumn, Status);
                dataManager.Parameters.Add(this.AppSchema.MasterGSTClass.GST_IdColumn, GstId);
                //dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        public ResultArgs DeleteGStDetails(int GStId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.GSTDetails.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.MasterGSTClass.GST_IdColumn, GStId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FillGSTDetails(int Id)
        {
            resultArgs = FetchGSt(Id);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                Slab = resultArgs.DataSource.Table.Rows[0][this.AppSchema.MasterGSTClass.SLABColumn.ColumnName].ToString();
                Gst = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.MasterGSTClass.GSTColumn.ColumnName].ToString());
                CGst = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.MasterGSTClass.CGSTColumn.ColumnName].ToString());
                SGst = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.MasterGSTClass.SGSTColumn.ColumnName].ToString());
                ApplicableFrom = this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.MasterGSTClass.APPLICABLE_FROMColumn.ColumnName].ToString(), false);
                Status = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.MasterGSTClass.STATUSColumn.ColumnName].ToString());
            }
            return resultArgs;
        }

        public ResultArgs FetchGSt(int gstId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.GSTDetails.FetchById))
            {
                dataManager.Parameters.Add(this.AppSchema.MasterGSTClass.GST_IdColumn, gstId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public DataTable FetchGSTList()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.GSTDetails.FetchGSTList))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs.DataSource.Table;
        }

        public ResultArgs FetchGSTClassList()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.GSTDetails.FetchGSTLedgerClass))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        #endregion
    }
}
