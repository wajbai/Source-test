using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;

namespace Bosco.Model.ASSET
{
   public  class AssetItemSystem: SystemBase
   {

       #region Variable Declearation
       ResultArgs resultArgs = new ResultArgs();
       #endregion

       #region Constructor

       public AssetItemSystem(int Item_Id)
        {
            this.ItemId = Item_Id;
            AssignToAssetItemPoroperties();
        }

       public AssetItemSystem()
       {
           
       }

       #endregion

       #region Properties
       public int ItemId { get; set; }
       public int AssetGroupId { get; set; }
       public int DepreciationLedger { get; set; }
       public int DisposalLedger { get; set; }
       public int AccountLeger{ get; set; }
       public int Catogery { get; set; }
       public string Name { get; set; }
       public string ItemKind { get; set; }
       public int Unit { get; set; }
       public string Method{ get; set; }
       public string Prefix { get; set; }
       public string Suffix { get; set; }
       public int StartingNo{ get; set; }
       public int Quantity { get; set; }
       public Decimal RatePerItem { get; set; }
       public Decimal Total { get; set; }
       #endregion

       #region Methods
       /// <summary>
       /// Save asset item details.
       /// </summary>
       /// <returns></returns>
       public ResultArgs SaveItemDetails()
       {
           try
           {
               using (DataManager dataManager = new DataManager((ItemId == 0) ? SQLCommand.AssetItem.Add : SQLCommand.AssetItem.Update))
               {
                   dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_IDColumn, ItemId);
                   dataManager.Parameters.Add(this.AppSchema.ASSETItem.ASSET_GROUP_IDColumn, AssetGroupId);
                   dataManager.Parameters.Add(this.AppSchema.ASSETItem.DEPRECIATION_LEDGER_IDColumn, DepreciationLedger);
                   dataManager.Parameters.Add(this.AppSchema.ASSETItem.DISPOSAL_LEDGER_IDColumn, DisposalLedger);
                   dataManager.Parameters.Add(this.AppSchema.ASSETItem.ACCOUNT_LEDGER_IDColumn, AccountLeger);
                   dataManager.Parameters.Add(this.AppSchema.ASSETItem.CATEGORY_IDColumn, Catogery);
                   dataManager.Parameters.Add(this.AppSchema.ASSETItem.NAMEColumn, Name);
                   dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_KINDColumn, ItemKind);
                   dataManager.Parameters.Add(this.AppSchema.ASSETItem.UNIT_IDColumn, Unit);
                   dataManager.Parameters.Add(this.AppSchema.ASSETItem.METHODColumn, Method);
                   dataManager.Parameters.Add(this.AppSchema.ASSETItem.PREFIXColumn, Prefix);
                   dataManager.Parameters.Add(this.AppSchema.ASSETItem.SUFFIXColumn, Suffix);
                   dataManager.Parameters.Add(this.AppSchema.ASSETItem.STARTING_NOColumn, StartingNo);
                   dataManager.Parameters.Add(this.AppSchema.ASSETItem.QUANTITYColumn, Quantity);
                   dataManager.Parameters.Add(this.AppSchema.ASSETItem.RATE_PER_ITEMColumn, RatePerItem);
                   dataManager.Parameters.Add(this.AppSchema.ASSETItem.TOTALColumn, Total);
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
       /// <summary>
       /// Fetch jenral ledgers to Account Ledger, Depreciation Ledger, Disposal Ledger 
       /// </summary>
       /// <returns></returns>
       public ResultArgs FetchDefaultLedgers()
       {
           using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchFixedDepositLedgers))
           {
               resultArgs = dataManager.FetchData(DataSource.DataTable);
           }
           return resultArgs;
       }
       /// <summary>
       /// Fetch all the asset item details for the view form.
       /// </summary>
       /// <returns></returns>
       public ResultArgs FetchAssetItemDetails()
       {
           using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.FetchAll))
           {
               resultArgs = dataManager.FetchData(DataSource.DataTable);
           }
           return resultArgs;
       }
       /// <summary>
       /// Fetch asset item details by Id for Edit
       /// </summary>
       /// <returns></returns>
       public ResultArgs FetchAssetItemDetailsById()
       {
           using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.Fetch))
           {
               dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_IDColumn, this.ItemId);
               resultArgs = dataManager.FetchData(DataSource.DataTable);
           }
           return resultArgs;
       }
       /// <summary>
       /// Assign values to AssetItemSystem properties for edit.
       /// </summary>
       public void AssignToAssetItemPoroperties()
       {
           resultArgs = FetchAssetItemDetailsById();
           if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs.DataSource.Table != null)
           {
               AssetGroupId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.ASSET_GROUP_IDColumn.ColumnName].ToString());
               DepreciationLedger = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.DEPRECIATION_LEDGER_IDColumn.ColumnName].ToString());
               DisposalLedger = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.DISPOSAL_LEDGER_IDColumn.ColumnName].ToString());
               AccountLeger = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.ACCOUNT_LEDGER_IDColumn.ColumnName].ToString());
               Catogery = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.CATEGORY_IDColumn.ColumnName].ToString());
               Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.NAMEColumn.ColumnName].ToString();
               ItemKind = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.ITEM_KINDColumn.ColumnName].ToString();
               Unit = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.UNIT_IDColumn.ColumnName].ToString());
               Method = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.METHODColumn.ColumnName].ToString();
               Prefix = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.PREFIXColumn.ColumnName].ToString();
               Suffix = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.SUFFIXColumn.ColumnName].ToString();
               StartingNo = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.STARTING_NOColumn.ColumnName].ToString());
               Quantity = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.QUANTITYColumn.ColumnName].ToString());
               RatePerItem = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.RATE_PER_ITEMColumn.ColumnName].ToString());
               Total = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ASSETItem.TOTALColumn.ColumnName].ToString());
           }
       }
       /// <summary>
       /// Delete asset item details by the ItemId.
       /// </summary>
       /// <param name="ItemId"></param>
       /// <returns></returns>
       public ResultArgs DeleteAssetItem(int ItemId)
       {
           using (DataManager dataManager=new DataManager(SQLCommand.AssetItem.Delete))
           {
               dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_IDColumn, ItemId);
               resultArgs = dataManager.UpdateData();
           }
           return resultArgs;
       }

       #endregion Methods
      

   }
}
