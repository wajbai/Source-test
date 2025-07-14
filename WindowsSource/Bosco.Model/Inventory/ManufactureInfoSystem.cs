using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using System.Data;
using Bosco.Utility;

namespace Bosco.Model
{
    public class ManufactureInfoSystem : SystemBase, AssetStockProduct.IVendorManufacture
    {
        #region VariableDeclaration
        ResultArgs resultArgs = new ResultArgs();

        #endregion

        #region Constructor

        public ManufactureInfoSystem()
        {

        }
        public ManufactureInfoSystem(int Id)
        {
            this.VendorId = Id;
            FillProperties();
        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Int32 StateId { get; set; }
        public Int32 CountryId { get; set; }
        public string PanNo { get; set; }
        public string GSTNo { get; set; }
        public string TelephoneNo { get; set; }
        public string Email { get; set; }
        public int VendorId { get; set; }
        #endregion

        #region Mthods
        public ResultArgs FetchDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ManufactureInfo.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager((Id == 0) ? SQLCommand.ManufactureInfo.Add : SQLCommand.ManufactureInfo.Update))
                {
                    dataManager.Parameters.Add(this.AppSchema.Manufactures.MANUFACTURER_IDColumn, Id);
                    dataManager.Parameters.Add(this.AppSchema.Manufactures.MANUFACTURERColumn, Name);
                    dataManager.Parameters.Add(this.AppSchema.Manufactures.ADDRESSColumn, Address);
                    dataManager.Parameters.Add(this.AppSchema.Manufactures.CONTACT_NOColumn, TelephoneNo);                    
                    dataManager.Parameters.Add(this.AppSchema.Manufactures.EMAIL_IDColumn, Email);
                    dataManager.Parameters.Add(this.AppSchema.Manufactures.PAN_NOColumn, PanNo);
                    dataManager.Parameters.Add(this.AppSchema.Manufactures.GST_NOColumn, GSTNo);
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

        public ResultArgs DeleteDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ManufactureInfo.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.Manufactures.MANUFACTURER_IDColumn, Id);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchSelectedDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ManufactureInfo.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.Manufactures.MANUFACTURER_IDColumn.ColumnName, this.Id);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public void FillProperties()
        {
            resultArgs = FetchSelectedDetails();
            if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs.DataSource.Table != null)
            {
                Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Manufactures.MANUFACTURERColumn.ColumnName].ToString();
                Address = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Manufactures.ADDRESSColumn.ColumnName].ToString();
                TelephoneNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Manufactures.CONTACT_NOColumn.ColumnName].ToString();
                PanNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Manufactures.PAN_NOColumn.ColumnName].ToString();
                GSTNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Manufactures.GST_NOColumn.ColumnName].ToString();
                Email = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Manufactures.EMAIL_IDColumn.ColumnName].ToString();
            }
        }

        public ResultArgs DeleteManufactureDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ManufactureInfo.DeleteManufactureDetails))
            {
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public int FetchManufactureNameByID()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ManufactureInfo.FetchManufactureNameByID))
            {
                dataManager.Parameters.Add(this.AppSchema.Manufactures.MANUFACTURERColumn, Name);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        #endregion

    }
}
