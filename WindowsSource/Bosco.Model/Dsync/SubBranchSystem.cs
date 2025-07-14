using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using AcMEDSync;
using AcMEDSync.Model;
using System.IO;
using System.Configuration;

namespace Bosco.Model.Dsync
{
    public class SubBranchSystem : SystemBase
    {
        #region Variable Decelaration
        ResultArgs resultArgs = null;
        CommonMethod common = new CommonMethod();
        ImportVoucherSystem importVoucher = new ImportVoucherSystem();
        #endregion

        #region Constructor
        public SubBranchSystem()
        {
        }

        public SubBranchSystem(int DsyncId)
        {
            FillDsynStatusProperties(DsyncId);
        }
        #endregion

        #region properties
        public int ProjectId { get; set; }
        public int BranchId { get; set; }
        public string XmlFileName { get; set; }
        public int[] SelectedProjectList { get; set; }
        public string BranchCode { get; set; }
        public string Remarks { get; set; }
        #endregion

        #region Methods
        public ResultArgs FetchBranchList()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.SubBranchList.FetchAllBranches))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchDsyncStatus()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.SubBranchList.FetchDSyncStatus))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public void FillDsynStatusProperties(int Id)
        {
            resultArgs = FetchDsyncStatusById(Id);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                Remarks = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DataSyncStatus.REMARKSColumn.ColumnName].ToString();
            }
        }
        public ResultArgs FetchDsyncStatusById(int Id)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.SubBranchList.FetchDsyncStatusById))
            {
                dataManager.Parameters.Add(this.AppSchema.DataSyncStatus.IDColumn, Id);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }

        public ResultArgs ExportMasterstoSubBranch()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = MapProjectToBranch();
                if (resultArgs.Success)
                {
                    resultArgs = GetBranchMasters();
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs MapProjectToBranch()
        {
            resultArgs = DeleteMappedProjects();
            if (resultArgs.Success)
            {
                foreach (int ProjectValue in SelectedProjectList)
                {
                    int ProjectId = ProjectValue;
                    using (DataManager dataManager = new DataManager(SQLCommand.SubBranchList.MapProjectBranch))
                    {
                        dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                        dataManager.Parameters.Add(this.AppSchema.DataSyncStatus.BRANCH_OFFICE_IDColumn, BranchId);
                        resultArgs = dataManager.UpdateData();
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs DeleteMappedProjects()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.SubBranchList.DeleteMappedProjects))
            {
                dataManager.Database = dataManager.Database;
                dataManager.Parameters.Add(this.AppSchema.DataSyncStatus.BRANCH_OFFICE_IDColumn, BranchId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs GetBranchMasters()
        {
            resultArgs = FetchBranchCode();
            if (resultArgs != null && resultArgs.Success)
            {
                BranchCode = resultArgs.DataSource.Sclar.ToString;

                if (!string.IsNullOrEmpty(BranchCode))
                {
                    resultArgs = ConstructHeaderData();
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtHeader = resultArgs.DataSource.Table;
                        using (ExportMasters exportMasterSystem = new ExportMasters())
                        {
                            resultArgs = exportMasterSystem.GetMasters(BranchCode);
                            if (resultArgs.Success)
                            {
                                resultArgs.DataSource.TableSet.Tables.Add(dtHeader);
                            }
                        }
                    }
                    else
                    {
                        resultArgs.Message = "Table does not exist";
                    }
                }

                else
                {
                    resultArgs.Message = "Branch Code is Empty";
                }
            }
            return resultArgs;
        }

        private ResultArgs FetchBranchCode()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.SubBranchList.FetchBranchCode))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }
        public ResultArgs FetchMappedProjects()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.SubBranchList.FetchMappedProjects))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs ConstructHeaderData()
        {
            DataTable dtBranch = new DataTable("Header");
            AcMELog.WriteLog("Constructs Header data Started...");
            try
            {
                DataColumn dcolHeadOfficeCode = new DataColumn("HEAD_OFFICE_CODE", typeof(string));
                DataColumn dcolBranchOfficeCode = new DataColumn("BRANCH_OFFICE_CODE", typeof(string));
                dtBranch.Columns.Add(dcolHeadOfficeCode);
                dtBranch.Columns.Add(dcolBranchOfficeCode);
                if (!string.IsNullOrEmpty(this.BranchOfficeCode) && !string.IsNullOrEmpty(BranchCode))
                {
                    dtBranch.Rows.Add(CommonMethod.Encrept(this.BranchOfficeCode), CommonMethod.Encrept(BranchCode));
                    resultArgs.DataSource.Data = dtBranch;
                    resultArgs.Success = true;
                    AcMELog.WriteLog("Construct Header data Ended....");
                }
                else
                {
                    resultArgs.Message = "HeadOffice Code or BranchOffice Code is Empty. ";
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            return resultArgs;
        }

        public ResultArgs UploadVoucherFile(string VoucherXML)
        {
            DataSet dsVoucher = new DataSet();
            if (File.Exists(VoucherXML))
            {
                XmlFileName = Path.GetFileName(VoucherXML);
                resultArgs = XMLConverter.ConvertXMLToDataSetWithResultArgs(VoucherXML);
                if (resultArgs.Success)
                {
                    dsVoucher = resultArgs.DataSource.TableSet;
                    resultArgs = ValidateSubBranchCode(dsVoucher);
                    if (resultArgs.Success)
                    {
                        resultArgs = UpdateDataSynStatus(DataSyncMailType.Received, "File is Received");
                        if (resultArgs.Success)
                        {
                            resultArgs = ValidateHeadOfficeCode(dsVoucher);
                        }
                    }
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// This method Reads the Sub Branch Code from XML file and validates against the Branch Office Sub Branch Code.
        /// 1. Gets the Decrypted Sub Branch Office Code from the Voucher XML
        /// 2. Validates the XML Branch Code with the Main Branch Code.
        /// 3. If the Sub Branch Code is Valid, Sub Branch Office Id will be assigned to the global variable.
        ///     That brach Id will be used for the entire data sync process.
        /// </summary>
        /// <param name="VoucherXml">Vouchers</param>
        /// <returns>ResultArgs with its properties</returns>
        private ResultArgs ValidateSubBranchCode(DataSet dsVouchers)
        {
            AcMEDataSynLog.WriteLog("ValidateSubBranchCode Started..");
            try
            {
                resultArgs = common.GetBranchOfficeCode(dsVouchers);
                if (resultArgs.Success)
                {
                    if (!string.IsNullOrEmpty(resultArgs.ReturnValue.ToString()) && resultArgs.ReturnValue != null)
                    {
                        string SubBranchCode = resultArgs.ReturnValue.ToString();
                        if (!string.IsNullOrEmpty(SubBranchCode))
                        {
                            using (DataManager dataManager = new DataManager(SQLCommand.SubBranchList.AuthenticateBranchCode))
                            {
                                dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_CODEColumn, SubBranchCode);
                                resultArgs = dataManager.FetchData(DataSource.DataTable);

                                if (resultArgs.Success)
                                {
                                    DataView dvBranchCode = resultArgs.DataSource.Table.DefaultView;
                                    if (dvBranchCode != null && dvBranchCode.Count == 1)
                                    {
                                        BranchId = this.NumberSet.ToInteger(dvBranchCode[0][this.AppSchema.DataSyncStatus.BRANCH_OFFICE_IDColumn.ColumnName].ToString());
                                    }
                                    else
                                    {
                                        resultArgs.Message = "Sub-Branch Office Code not found in Branch Office. Data Syn could not proceed.";
                                    }
                                }
                                else
                                {
                                    resultArgs.Message = "Error in getting branch code from Branch office in ValidateSubBranchCode: " + resultArgs.Message;
                                }
                            }
                        }
                        else
                        {
                            resultArgs.Message = "Sub Branch Office Code is empty in the Voucher XML. Data Syn could not Succeed.";
                        }
                    }
                    else
                    {
                        resultArgs.Message = "Sub Branch Office Code not found in Voucher XML.";
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in ValidateSubBranchCode: " + ex.ToString();
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ValidateBranchCode Ended.");
            }
            return resultArgs;
        }

        /// <summary>
        /// This method reads the Head Office Code from the Voucher XML and validates against the Head Office.
        /// 1. Gets the Decrypted Head Office Office Code from the Voucher XML
        /// 2. Validates the XML Head Office Code with the Portal Head Office Code.
        /// 3. If the Head Office Code is Valid, Head Office Id will be assigned to the global variable.
        ///     That Head Office Id will be used for the entire data sync process wherever necessary.
        /// </summary>
        /// <param name="VoucherXml">Vouchers</param>
        /// <returns>ResultArgs with its properties</returns>
        private ResultArgs ValidateHeadOfficeCode(DataSet dsVouchers)
        {
            AcMEDataSynLog.WriteLog("ValidateHeadOfficeCode Started..");
            try
            {
                resultArgs = common.GetHeadOfficeCode(dsVouchers);
                if (resultArgs.Success)
                {
                    if (resultArgs.ReturnValue != null && !string.IsNullOrEmpty(resultArgs.ReturnValue.ToString()))
                    {
                        string HOCode = resultArgs.ReturnValue.ToString();
                        if (!string.IsNullOrEmpty(HOCode) && !string.IsNullOrEmpty(this.BranchOfficeCode))
                        {
                            bool isBranchCodeValid = HOCode.Equals(this.BranchOfficeCode);
                            if (!isBranchCodeValid)
                            {
                                resultArgs.Message = "License Sub Branch Code does not match with main Branch Code";
                            }
                        }
                        else
                        {
                            resultArgs.Message = "Head Office Code Empty in the Voucher XML. Data Syn could not Succeed.";
                        }
                    }
                    else
                    {
                        resultArgs.Message = "Head Office Code not found in Voucher XML.";
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in ValidateHeadOfficeCode: " + ex.ToString();
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("ValidateHeadOfficeCode Ended..");
            }
            return resultArgs;
        }

        public ResultArgs SynchronizeVoucher(string VoucherFileName)
        {
            importVoucher.SynchronizeHeld += new EventHandler(importVoucher_SynchronizeHeld);
            resultArgs = importVoucher.ImportVouchers(VoucherFileName);
            //IAcMEDataSyn dataSynVoucher = new AcMEDataSyn();
            //resultArgs = dataSynVoucher.SynchronizeVouchers(VoucherFileName);
            return resultArgs;
        }

        void importVoucher_SynchronizeHeld(object sender, EventArgs e)
        {
            MessageRender.ShowMessage(importVoucher.Caption);
        }

        public ResultArgs UpdateDataSynStatus(DataSyncMailType status, string Remark)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.SubBranchList.SaveDSyncStatus))
                {
                    dataManager.Parameters.Add(this.AppSchema.DataSyncStatus.BRANCH_OFFICE_IDColumn, BranchId);
                    dataManager.Parameters.Add(this.AppSchema.DataSyncStatus.XML_FILENAMEColumn, XmlFileName);
                    dataManager.Parameters.Add(this.AppSchema.DataSyncStatus.STATUSColumn, (int)status);
                    dataManager.Parameters.Add(this.AppSchema.DataSyncStatus.REMARKSColumn, Remark);

                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in UpdateDataSyncStatus: " + ex.ToString();
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("UpdateDataSynStatus Ended.");
            }
            return resultArgs;
        }
        #endregion
    }
}
