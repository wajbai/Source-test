using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using System.Data;
using Bosco.Utility;

namespace Bosco.Model.Inventory
{
    public class ManufactureSystem : SystemBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = new ResultArgs();

        #endregion

        #region Constructor

        public ManufactureSystem()
        {

        }
        public ManufactureSystem(int Manufacture_Id)
        {
            this.ManufactureId = Manufacture_Id;
            FillManufactureProperties();
        }
        #endregion

        #region Properties
        public int ManufactureId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PanNo { get; set; }
        public string TelephoneNo { get; set; }
        public string Email { get; set; }
        #endregion

        #region Method
        public ResultArgs FetchManufactureDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ManufactureInfo.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveManufactureDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager((ManufactureId == 0) ? SQLCommand.ManufactureInfo.Add : SQLCommand.ManufactureInfo.Update))
                {
                    dataManager.Parameters.Add(this.AppSchema.Manufactures.MANUFACTURE_IDColumn, ManufactureId);
                    dataManager.Parameters.Add(this.AppSchema.Manufactures.NAMEColumn, Name);
                    dataManager.Parameters.Add(this.AppSchema.Manufactures.ADDRESSColumn, Address);
                    dataManager.Parameters.Add(this.AppSchema.Manufactures.PAN_NOColumn, PanNo);
                    dataManager.Parameters.Add(this.AppSchema.Manufactures.CONTACT_NOColumn, TelephoneNo);
                    dataManager.Parameters.Add(this.AppSchema.Manufactures.EMAIL_IDColumn, Email);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
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

        public ResultArgs DeleteManufactureDetails(int ManufactureId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ManufactureInfo.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.Manufactures.MANUFACTURE_IDColumn, ManufactureId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchSelectedManufactureDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ManufactureInfo.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.Manufactures.MANUFACTURE_IDColumn.ColumnName, this.ManufactureId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public void FillManufactureProperties()
        {
            resultArgs = FetchSelectedManufactureDetails();
            if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs.DataSource.Table != null)
            {
                Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Manufactures.NAMEColumn.ColumnName].ToString();
                Address = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Manufactures.ADDRESSColumn.ColumnName].ToString();
                PanNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Manufactures.PAN_NOColumn.ColumnName].ToString();
                TelephoneNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Manufactures.CONTACT_NOColumn.ColumnName].ToString();
                Email = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Manufactures.EMAIL_IDColumn.ColumnName].ToString();
            }
        }
        #endregion

    }
}
