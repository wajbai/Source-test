using System;
using System.Collections.Generic;

using System.Text;
using System.Data;

using Payroll.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;

namespace Payroll.Model.UIModel
{
    public class NameTitleSystem : SystemBase
    {
        #region Declaration
        private ResultArgs resultArgs = null;
        public Int32 NameTilteid = 0;
        public string NameTilte = string.Empty;
        #endregion

        public NameTitleSystem()
        {
            resultArgs = new ResultArgs();
        }

        public NameTitleSystem(Int32 nametitleid)
            : this()
        {
            NameTilteid= nametitleid;
            FetchNameTitleById();
        }

        public ResultArgs FetchNameTitle()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.NameTitle.FetchNameTitle,SQLAdapterType.PayrollSQL))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                resultArgs.DataSource.Table.DefaultView.Sort = this.AppSchema.NameTitle.NAME_TITLEColumn.ColumnName;
            }
            return resultArgs;
        }

        public ResultArgs FetchNameTitleById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.NameTitle.FetchNameTitleById, SQLAdapterType.PayrollSQL))
            {
                dataManager.Parameters.Add(this.AppSchema.NameTitle.NAME_TITLE_IDColumn, NameTilteid);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            NameTilte = string.Empty;
            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                NameTilte = resultArgs.DataSource.Table.Rows[0][this.AppSchema.NameTitle.NAME_TITLEColumn.ColumnName].ToString().Trim();
            }
            return resultArgs;
        }
        
        public ResultArgs SaveNameTitle()
        {
            using (DataManager dataManager = new DataManager(NameTilteid  == 0 ? SQLCommand.NameTitle.InsertNameTitle: 
                SQLCommand.NameTitle.UpdateNameTitle, SQLAdapterType.PayrollSQL))
            {
                dataManager.Parameters.Add(this.AppSchema.NameTitle.NAME_TITLE_IDColumn, NameTilteid);
                dataManager.Parameters.Add(this.AppSchema.NameTitle.NAME_TITLEColumn, NameTilte.Trim());
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteNameTitle()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.NameTitle.DeleteNameTitle, SQLAdapterType.PayrollSQL))
            {
                dataManager.Parameters.Add(this.AppSchema.NameTitle.NAME_TITLE_IDColumn, NameTilteid);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

    }
}
