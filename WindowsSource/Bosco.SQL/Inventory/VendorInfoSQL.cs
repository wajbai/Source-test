using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    class VendorInfoSQL : IDatabaseQuery
    {
        DataCommandArguments dataCommandArgs;
        SQLType sqlType;

        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.VendorInfo).FullName)
            {
                query = GetstockItemSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #region SQL Script
        public string GetstockItemSQL()
        {
            string query = "";
            SQLCommand.VendorInfo SqlcommandId = (SQLCommand.VendorInfo)(this.dataCommandArgs.SQLCommandId);
            switch (SqlcommandId)
            {
                case SQLCommand.VendorInfo.Add:
                    {
                        query = "INSERT INTO ASSET_STOCK_VENDOR(VENDOR,\n" +
                                "ADDRESS, STATE_ID, COUNTRY_ID,\n" +
                                "PAN_NO,\n" +
                                "GST_NO,\n" +
                                "CONTACT_NO,\n" +
                                "EMAIL_ID)\n" +
                                "VALUES(?VENDOR,\n" +
                                "?ADDRESS, ?STATE_ID, ?COUNTRY_ID,\n" +
                                "?PAN_NO,\n" +
                                "?GST_NO,\n" +
                                "?CONTACT_NO,\n" +
                                "?EMAIL_ID)";
                        break;
                    }

                case SQLCommand.VendorInfo.Update:
                    {
                        query = "UPDATE ASSET_STOCK_VENDOR SET VENDOR=?VENDOR,\n" +
                                " ADDRESS=?ADDRESS, STATE_ID = ?STATE_ID, COUNTRY_ID = ?COUNTRY_ID,\n" +
                                "PAN_NO=?PAN_NO,\n" +
                                "GST_NO=?GST_NO,\n" +
                                "CONTACT_NO=?CONTACT_NO,\n" +
                                "EMAIL_ID=?EMAIL_ID " +
                                "WHERE VENDOR_ID =?VENDOR_ID ";
                        break;
                    }

                case SQLCommand.VendorInfo.FetchAll:
                    {
                        query = "SELECT VENDOR_ID AS ID, VENDOR AS NAME, GST_NO, ADDRESS, PAN_NO, CONTACT_NO, EMAIL_ID\n" +
                                "  FROM ASSET_STOCK_VENDOR ORDER BY VENDOR";
                        break;
                    }
                case SQLCommand.VendorInfo.FetchAllWtihGST:
                    {
                        query = "SELECT VENDOR_ID AS ID, CONCAT(VENDOR, IF(GST_NO IS NULL OR GST_NO ='', '', CONCAT(' - GST No: ',  GST_NO))) AS VENDOR, ADDRESS, PAN_NO, CONTACT_NO, EMAIL_ID\n" +
                                "  FROM ASSET_STOCK_VENDOR ORDER BY VENDOR";
                        break;
                    }
                case SQLCommand.VendorInfo.FetchByPANNo:
                    {
                        query = "SELECT * FROM ASSET_STOCK_VENDOR WHERE PAN_NO =?PAN_NO AND VENDOR_ID<>?VENDOR_ID";
                        break;
                    }
                case SQLCommand.VendorInfo.FetchByGSTNo:
                    {
                        query = "SELECT * FROM ASSET_STOCK_VENDOR WHERE GST_NO =?GST_NO AND VENDOR_ID<>?VENDOR_ID";
                        break;
                    }
                case SQLCommand.VendorInfo.FetchByEmail:
                    {
                        query = "SELECT * FROM ASSET_STOCK_VENDOR WHERE EMAIL_ID =?EMAIL_ID AND VENDOR_ID<>?VENDOR_ID";
                        break;
                    }
                case SQLCommand.VendorInfo.Delete:
                    {
                        query = "DELETE FROM ASSET_STOCK_VENDOR WHERE VENDOR_ID =?VENDOR_ID";
                        break;
                    }
                case SQLCommand.VendorInfo.Fetch:
                    {
                        query = "SELECT ASV.VENDOR_ID, ASV.VENDOR, ASV.ADDRESS,\n"+
                                "ASV.PAN_NO, ASV.GST_NO, ASV.CONTACT_NO, ASV.EMAIL_ID,\n" +
                                "IFNULL(ASV.STATE_ID, 0) AS STATE_ID, IFNULL(ASV.COUNTRY_ID,0) AS COUNTRY_ID, \n" +
                                "IFNULL(MS.STATE_NAME, '') AS STATE_NAME, IFNULL(MC.COUNTRY, '') AS COUNTRY\n" +
                                "FROM ASSET_STOCK_VENDOR ASV\n" +
                                "LEFT JOIN MASTER_COUNTRY MC ON MC.COUNTRY_ID = ASV.COUNTRY_ID\n"+
                                "LEFT JOIN MASTER_STATE MS ON MS.STATE_ID = ASV.STATE_ID\n" +
                                "WHERE VENDOR_ID = ?VENDOR_ID";
                        break;
                    }

                case SQLCommand.VendorInfo.DeleteVendorDetails:
                    {
                        query = "DELETE FROM ASSET_STOCK_VENDOR";
                        break;
                    }

                case SQLCommand.VendorInfo.FetchVendorNameByID:
                    {
                        query = "SELECT VENDOR_ID FROM ASSET_STOCK_VENDOR WHERE VENDOR=?VENDOR;";

                        break;
                    }
                case SQLCommand.VendorInfo.FetchVendorByItemId:
                    {
                        query = "SELECT ASV.VENDOR_ID AS ID,ITEM_ID,LOCATION_ID, VENDOR, ADDRESS, PAN_NO, CONTACT_NO, EMAIL_ID\n" +
                                "  FROM ASSET_STOCK_VENDOR ASV\n" +
                                " INNER JOIN STOCK_MASTER_PURCHASE SMP\n" +
                                "    ON ASV.VENDOR_ID = SMP.VENDOR_ID\n" +
                                " INNER JOIN STOCK_PURCHASE_DETAILS SPD\n" +
                                "    ON SMP.PURCHASE_ID = SPD.PURCHASE_ID\n" +
                                "{ WHERE SPD.ITEM_ID = ?ITEM_ID}\n" +
                                " { AND SPD.LOCATION_ID = ?LOCATION_ID}\n" +
                                "GROUP BY ASV.VENDOR_ID";
                        break;
                    }
            }
            return query;
        }
        #endregion
    }
}
