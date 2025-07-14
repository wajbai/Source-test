using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.Model;
using Bosco.Model.Inventory.Stock;

namespace Bosco.Model
{
    public class AssetStockFactory
    {
        //public static AssetStockProduct.IGroup GetGroupInstance(FinanceModule module)
        //{
        //    if (module == FinanceModule.Asset)
        //        return new AssetClassSystem();
        //    else if (module == FinanceModule.Stock)
        //        return new StockGroupSystem();
        //    else return null;
        //}

        public static AssetStockProduct.ICategory GetCategoryInstance(FinanceModule module)
        {
            if (module == FinanceModule.Asset)
                return new AssetCategorySystem();
            else if (module == FinanceModule.Stock)
                return new StockCategorySystem();
            else return null;
        }

        //public static AssetStockProduct.IMeasure GetUnitOfMeasureInstance(FinanceModule module)
        //{
        //    if (module == FinanceModule.Asset)
        //        return new AssetUnitOfMeassureSystem();
        //    else if (module == FinanceModule.Stock)
        //        return new StockUnitOfMeassureSystem();
        //    else return null;
        //}
        public static AssetStockProduct.IVendorManufacture GetUnitVendorInstance(VendorManufacture module)
        {
            if (module == VendorManufacture.Vendor)
                return new VendorInfoSystem(); 
            else if (module == VendorManufacture.Manufacture)
                return new ManufactureInfoSystem();
            else return null;
        }

        //public static AssetStockProduct.ILocation GetLocationInstance(FinanceModule module)
        //{
        //    if (module == FinanceModule.Asset)
        //        return new LocationSystem();
        //    else if (module == FinanceModule.Stock)
        //        return new StockLocationSystem();
        //    else return null;
        //}
    }
}
