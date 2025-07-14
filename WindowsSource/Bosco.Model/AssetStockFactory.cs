using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.Model.Stock;
using Bosco.Model.Asset;

namespace Bosco.Model
{
    public class AssetStockFactory
    {
        public static AssetStockProduct.ILocation GetLocationInstance(FinanceModule module)
        {
            if (module == FinanceModule.Asset)
                return new AssetLocationSystem();
            else if (module == FinanceModule.Stock) 
                return new StockLocationSystem(); 
            else return null; 
        }

        public static AssetStockProduct.IGroup GetGroupInstance(FinanceModule module)
        {
            if (module == FinanceModule.Asset)
                return new AssetGroupSystem();
            else if (module == FinanceModule.Stock)
                return new StockGroupSystem();
            else return null;
        }

        public static AssetStockProduct.ICategory GetCategoryInstance(FinanceModule module)
        {
            if (module == FinanceModule.Asset)
                return new AssetCategorySystem();
            else if (module == FinanceModule.Stock)
                return new StockCategorySystem();
            else return null;
        }

        public static AssetStockProduct.IMeasure GetUnitOfMeasureInstance(FinanceModule module)
        {
            if (module == FinanceModule.Asset)
                return new AssetUnitOfMeassureSystem();
            else if (module == FinanceModule.Stock)
                return new StockUnitOfMeassureSystem();
            else return null;
        }
    }
}
