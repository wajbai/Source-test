using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.Utility;

namespace Bosco.Model.Asset
{
    public class AssetStockProduct
    {
        public interface ILocation
        {
            ResultArgs FetchLocationDetails();
            ResultArgs SaveLocationDetails();
            ResultArgs DeleteLocationDetails();
        }

        public interface IGroup
        {
        }
    }
}
