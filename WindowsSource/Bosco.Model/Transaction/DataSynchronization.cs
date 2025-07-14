using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Bosco.DAO.Data;
using Bosco.Utility;

namespace Bosco.Model.Transaction
{
    public interface DataSyncOffline
    {
        void FetchRecords(DataSet dsReadXML);
        void DataSyncOffLine(DataSet dsReadXMLFile);
    }
    public class DataSynchronization : SystemBase, DataSyncOffline
    {

        #region Decelaration
        private DataTable dtTable = new DataTable();
        #endregion
        public void DataSyncOffLine(DataSet dsReadXMLFile)
        {
            throw new NotImplementedException();
        }

        public void FetchRecords(DataSet dsReadXML)
        {
            try
            {
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.BeginTransaction();
                    if (dsReadXML != null)
                    {
                        for (int i = 0; i < dsReadXML.Tables.Count; i++)
                        {
                            switch (dsReadXML.Tables[i].TableName)
                            {
                                case "OfficeData":
                                    {
                                        dtTable = dsReadXML.Tables["OfficeData"];
                                        break;
                                    }
                                case "LegalEntity":
                                    {
                                        dtTable = dsReadXML.Tables["LegalEntity"];
                                        break;
                                    }
                                case "ProjectCatogory":
                                    {
                                        dtTable = dsReadXML.Tables["ProjectCatogory"];
                                        break;
                                    }
                                case "Project":
                                    {
                                        dtTable = dsReadXML.Tables["Project"];
                                        break;
                                    }
                                case "LedgerGroup":
                                    {
                                        break;
                                    }
                                case "Ledger":
                                    {
                                        break;
                                    }
                                case "FCPurpose":
                                    {
                                        break;
                                    }
                            }
                        }
                    }
                    dataManager.EndTransaction();
                }
            }
            catch (Exception ex)
            {

            }
            finally { }
        }
    }
}
