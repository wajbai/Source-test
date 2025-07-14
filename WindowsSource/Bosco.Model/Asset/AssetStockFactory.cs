using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.Model.ASSET;
using Bosco.Model.Asset;
using Bosco.Model.Stock;

namespace Bosco.Model
{
    public class AssetStockFactory
    {
        public static AssetStockProduct.ILocation GetInstance(Module module) 
        { 
            if (module== Module.Asset) 
                return new LocationSystem(); 
            else if (module== Module.Stock) 
                return new StockLocationSystem(); 
            else return null; 
        }
    }
}
