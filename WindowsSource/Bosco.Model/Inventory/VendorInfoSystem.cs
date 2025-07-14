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
    public class VendorInfoSystem : SystemBase, AssetStockProduct.IVendorManufacture
    {
        #region VariableDeclaration
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Constructor

        public VendorInfoSystem()
        {

        }
        public VendorInfoSystem(int id)
        {
            this.Id = id;
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

        public int ItemId { get; set; }
        public int LocationId { get; set; }
        #endregion

        #region Methods
        public ResultArgs FetchDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VendorInfo.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchDetailsWithGSTNo()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VendorInfo.FetchAllWtihGST))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchDetailsByItemId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VendorInfo.FetchVendorByItemId))
            {
                if (ItemId > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.StockItem.ITEM_IDColumn, ItemId);
                }
                if (LocationId > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.StockLocation.LOCATION_IDColumn, LocationId);
                }
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchDetailsByGSTNo()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VendorInfo.FetchByGSTNo))
            {
                dataManager.Parameters.Add(this.AppSchema.Vendors.VENDOR_IDColumn, Id);
                dataManager.Parameters.Add(this.AppSchema.Vendors.GST_NOColumn, GSTNo);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchDetailsByPANNo()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VendorInfo.FetchByPANNo))
            {
                dataManager.Parameters.Add(this.AppSchema.Vendors.VENDOR_IDColumn, Id);
                dataManager.Parameters.Add(this.AppSchema.Vendors.PAN_NOColumn, PanNo);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchDetailsByEmail()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VendorInfo.FetchByEmail))
            {
                dataManager.Parameters.Add(this.AppSchema.Vendors.VENDOR_IDColumn, Id);
                dataManager.Parameters.Add(this.AppSchema.Vendors.EMAIL_IDColumn, Email);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        
        public ResultArgs SaveDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager((Id == 0) ? SQLCommand.VendorInfo.Add : SQLCommand.VendorInfo.Update))
                {
                    dataManager.Parameters.Add(this.AppSchema.Vendors.VENDOR_IDColumn, Id,true);
                    dataManager.Parameters.Add(this.AppSchema.Vendors.VENDORColumn, Name);
                    dataManager.Parameters.Add(this.AppSchema.Vendors.ADDRESSColumn, Address);
                    if (StateId == 0)
                        dataManager.Parameters.Add(this.AppSchema.Vendors.STATE_IDColumn, null);
                    else
                        dataManager.Parameters.Add(this.AppSchema.Vendors.STATE_IDColumn, StateId);

                    if (CountryId==0)
                        dataManager.Parameters.Add(this.AppSchema.Vendors.COUNTRY_IDColumn, null);
                    else
                        dataManager.Parameters.Add(this.AppSchema.Vendors.COUNTRY_IDColumn, CountryId);

                    dataManager.Parameters.Add(this.AppSchema.Vendors.PAN_NOColumn, PanNo);
                    dataManager.Parameters.Add(this.AppSchema.Vendors.GST_NOColumn, GSTNo);
                    dataManager.Parameters.Add(this.AppSchema.Vendors.CONTACT_NOColumn, TelephoneNo);
                    dataManager.Parameters.Add(this.AppSchema.Vendors.EMAIL_IDColumn, Email);
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
            using (DataManager dataManager = new DataManager(SQLCommand.VendorInfo.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.Vendors.VENDOR_IDColumn, Id);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchSelectedDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VendorInfo.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.Vendors.VENDOR_IDColumn, this.Id);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public void FillProperties()
        {
            resultArgs = FetchSelectedDetails();
            if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs.DataSource.Table != null)
            {
                Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Vendors.VENDORColumn.ColumnName].ToString();
                Address = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Vendors.ADDRESSColumn.ColumnName].ToString();
                StateId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Vendors.STATE_IDColumn.ColumnName].ToString());
                CountryId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Vendors.COUNTRY_IDColumn.ColumnName].ToString());
                PanNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Vendors.PAN_NOColumn.ColumnName].ToString();
                GSTNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Vendors.GST_NOColumn.ColumnName].ToString();
                TelephoneNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Vendors.CONTACT_NOColumn.ColumnName].ToString();
                Email = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Vendors.EMAIL_IDColumn.ColumnName].ToString();
            }
        }

        public ResultArgs DeleteVendorDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VendorInfo.DeleteVendorDetails))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int FetchVendorNameByID()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VendorInfo.FetchVendorNameByID))
            {
                dataManager.Parameters.Add(this.AppSchema.Vendors.VENDORColumn, Name);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        #endregion

    }
}
