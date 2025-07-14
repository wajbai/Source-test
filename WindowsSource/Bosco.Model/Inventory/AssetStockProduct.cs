using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.Utility;
using System.Data;

namespace Bosco.Model
{
    public class AssetStockProduct
    {
        public interface IGroup
        {
            int ParentClassId { get; set; }
            int Method { get; set; }
            int AssetClassId { get; set; }
            string Name { get; set; }
            string GroupIds { get; set; }
            double Depreciation { get; set; }

            ResultArgs FetchGroupDetails();
            ResultArgs SaveClassDetails();
            ResultArgs DeleteClassDetails();
            ResultArgs DeleteAll();
            int FetchAssetClassId();
            int FetchParentGroupId();
            ResultArgs FetchSelectedGroupDetails();
            void AssignClassProperties();
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
        public interface IVendorManufacture
        {
            int Id { get; set; }
            string Name { get; set; }
            string Address { get; set; }
            Int32 StateId { get; set; }
            Int32 CountryId { get; set; }
            string PanNo { get; set; }
            string GSTNo { get; set; }
            string TelephoneNo { get; set; }
            string Email { get; set; }

            ResultArgs FetchDetails();
            ResultArgs SaveDetails();
            ResultArgs DeleteDetails();
            ResultArgs FetchSelectedDetails();
            void FillProperties();
        }

        //public interface ILocation
        //{
        //    int AreaId { get; set; }
        //    int BuildingId { get; set; }
        //    int FloorId { get; set; }
        //    int RoomId { get; set; }
        //    string Name { get; set; }
        //    int CustodianId { get; set; }
        //    int LocationId { get; set; }
        //    int BlockId { get; set; }
        //    int ImageId { get; set; }
        //    string LocationIds { get; set; }
        //    int AssetId { get; set; }
        //    DateTime ResponsibleDate { get; set; }
        //    int LocationType { get; set; }
        //    FinanceModule Module { get; set; }

        //    ResultArgs FetchLocationDetails();
        //    ResultArgs FetchBlockDetails();
        //    ResultArgs FetchLocationDetailsByItem();
        //    ResultArgs SaveLocationDetails();
        //    void FillLocationProperties(int LocationId);
        //    ResultArgs DeleteLocationDetails(int locationId);
        //    ResultArgs FetchLocationById(int LocationId);
        //    ResultArgs FetchLocaitonByAssetId();
        //    ResultArgs FetchSelectedLocationDetails();
        //    int FetchLocationNameByID();
        //    ResultArgs DeleteLocationDetails();
        //}
    }
}
