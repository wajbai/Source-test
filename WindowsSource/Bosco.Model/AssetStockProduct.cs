using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.Utility;

namespace Bosco.Model
{
    public class AssetStockProduct
    {
        public interface ILocation
        {
            int LocationId { get; set; }
            string Name { get; set; }
            string LocationAddress { get; set; }
            string Type { get; set; }

            ResultArgs FetchLocationDetails();
            ResultArgs SaveLocationDetails();
            ResultArgs DeleteLocationDetails();
            void AssignToLocationPoroperties();
        }

        public interface IGroup
        {
            int ParentGroupId { get; set; }
            int Method { get; set; }
            int GroupId { get; set; }
            string Name { get; set; }
            string GroupIds { get; set; }
            double Depreciation { get; set; }
            int ImageId { get; set; }

            ResultArgs FetchGroupDetails();
            ResultArgs SaveGroupDetails();
            ResultArgs DeleteGroupDetails();
            ResultArgs FetchSelectedGroupDetails();
            void AssignGroupProperties();
        }

        public interface IMeasure
        {
            int ConversionOf { get; set; }
            int unitId { get; set; }
            int DecimalPlace { get; set; }
            int TypeId { get; set; }
            string TYPE { get; set; }
            string SYMBOL { get; set; }
            string NAME { get; set; }
            string FirstUnitId { get; set; }
            string SecondUnitId { get; set; }

            ResultArgs FetchMeasureDetails();
            ResultArgs SaveMeasureDetails();
            ResultArgs DeleteMeasureDetails();
            ResultArgs FetchUnitsForGridLookUP();
            void AssignMeasureProperties();
        }

        public interface ICategory
        {
            int CategoryId { get; set; }
            int CategoryParentId { get; set; }
            int ImageId { get; set; }
            string GroupIds { get; set; }
            string Name { get; set; }

            ResultArgs FetchCategoryDetails();
            ResultArgs SaveCategoryDetails();
            ResultArgs DeleteCategoryDetails();
            ResultArgs FetchSelectedCategoryDetails();
            void FillCategoryProperties();
        }
    }
}
