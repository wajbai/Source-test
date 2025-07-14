using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Utility;
using Bosco.Model.UIModel.Master;
using System.Data.OleDb;
using ACPP.Modules.UIControls;
using Bosco.Model;
using Bosco.Utility.ConfigSetting;
using Bosco.DAO.Schema;
using System.IO;
using System.Reflection;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmAssetImportDetails : frmFinanceBaseAdd
    {
        public frmAssetImportDetails()
        {
            InitializeComponent();
        }

        #region Properties

        string FileName { get; set; }
        MasterImport FrmName { get; set; }
        FinanceModule Module { get; set; }
        ResultArgs resultArgs = new ResultArgs();
        DataTable dtXlData = new DataTable();
        DataTable dtXLtoAcME = new DataTable();
        DataTable dtXLtoAcMEInOut = new DataTable();
        DataTable dtXLtoAcMEItemDetail = new DataTable();
        public static string MigrationLog = string.Empty;
        string SheetName = string.Empty;

        const string ASSET_TABLE_NAME = "AssetItem";
        string ASSET_CLASS = "ASSET CLASS";
        string RETENTION_YRS = "RETENTION YRS";
        string DEPRECIATION_YRS = "DEPRECIATION YRS";
        string IS_INSURED = "IS INSURED";
        string IS_AMC = "IS AMC";
        string ASSET_ITEM = "ASSET ITEM";
        string ACCOUNT_LEDGER = "ACCOUNT LEDGER";
        string DEPRECIATION_LEDGER = "DEPRECIATION LEDGER";
        string DISPOSAL_LEDGER = "DISPOSAL LEDGER";
        string METHOD = "METHOD";
        string PREFIX = "PREFIX";
        string SUFFIX = "SUFFIX";
        string PROJECT = "PROJECT";
        string ASSETID = "ASSET ID";
        string ITEMID = "ITEM_ID";
        string QUANTITY = "QUANTITY";
        string AMOUNT = "AMOUNT";
        string MANUFACTURER = "Manufacturer";
        string CUSTODIAN = "Custodian";
        string DESCRIPTION = "Description";
        string BLOCK = "Block";
        string LOCATION = "Location";
        string LOCATIONTYPE = "Location Type";
        string ROLE = "Role";

        string INSURANCEAPPLICABLE = "Insurance Applicable?";
        string AMCAPPLICABLE = "AMC Applicable?";
        string RETENSIONYRS = "Retention Yrs";
        string DEPRECIATIONYRS = "Depreciation Yrs";

        int LocationID = 0;
        int ParentLocationId = 0;
        int ClassID = 0;
        int CustodianID = 0;
        int VendorID = 0;
        int ManufactureID = 0;
        int UnitID = 0;
        int ParentGroupID = 0;
        int ProjectID = 0;
        int ItemID = 0;
        int Quantity = 0;
        int ParentClassID = 0;
        decimal Amount = 0;
        decimal DepreciationAmount = 0;


        string Name = string.Empty;
        string Role = string.Empty;
        string UnitName = string.Empty;
        string ItemName = string.Empty;
        string CustudianName = string.Empty;
        string ProjectName = string.Empty;
        string VendarName = string.Empty;
        string ManufactureName = string.Empty;
        string AssetClass = string.Empty;
        string LedgerName = string.Empty;
        string Prefix = string.Empty;
        string Suffix = string.Empty;
        string AssetId = string.Empty;
        string Blockname = string.Empty;
        string parentClassName = string.Empty;

        bool isDeleteImport = true;
        int AssetItemCount = 0;
        int AssetOPCount = 0;

        #endregion

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog() { Filter = "Excel Files|*.xls;*.xlsx;*.xlsm" };

            if (DialogResult.OK == file.ShowDialog())
            {
                if (!string.IsNullOrEmpty(file.FileName))
                {
                    txtSelectedFile.Text = file.FileName;
                }
            }
        }


        /// <summary>
        /// This 01 -- Import the data from excel sheet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                AcMELog.WriteLog("Import Asset OP stock started..");
                if (!string.IsNullOrEmpty(txtSelectedFile.Text) || System.IO.File.Exists(txtSelectedFile.Text))
                {
                    FileName = txtSelectedFile.Text;
                    DataTable dtExcelValue = new DataTable();
                    resultArgs = CheckTransExists();
                    if (resultArgs.Success && resultArgs.DataSource.Sclar.ToInteger > 0)
                    {
                        // Name has been changed to Stock to Asset 
                        dtXlData = ReadMultiExcelData("'Opening Asset" + "$'");
                        AcMELog.WriteLog("Import Asset OP : Read Asset Details from Excel File..");
                        if (dtXlData != null && dtXlData.Rows.Count > 0)
                        {
                            AssetOPCount = AssetItemCount = dtXlData.Rows.Count;

                            //string strMessage = String.Format("Record is available, Do you want to delete all the Asset vouchers and continue Import?{0}{0}Yes            :  Delete and Continue{1}No             :  Import Asset Item{1}Cancel       :  Stop Importing Asset",
                            //         Environment.NewLine, Environment.NewLine);
                            //DialogResult result = this.ShowConfirmationMessage(strMessage, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                            string strMessage = String.Format("Record is available, Do you want to delete all the Asset vouchers and continue Import?{0}{0}Yes            :  Delete and Continue{1}No             :  Cancel",
                                     Environment.NewLine, Environment.NewLine);
                            DialogResult result = this.ShowConfirmationMessage(strMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                // resultArgs = DeleteAllTransaction();
                                if (resultArgs.Success)
                                {
                                    isDeleteImport = true;
                                }
                                else
                                {
                                    isDeleteImport = false;
                                }
                            }
                            else
                            {
                                isDeleteImport = false;
                                resultArgs.Success = false;
                            }
                            //else if (result == DialogResult.No)
                            //{
                            //    isDeleteImport = false;
                            //}
                            //else
                            //{
                            //    isDeleteImport = false;
                            //    resultArgs.Success = false;
                            //}
                        }
                    }

                    if (isDeleteImport)
                    {
                        ShowWaitDialog();
                        // 26/09/2024, Insert the Master details
                        //   resultArgs = ImportAssetMasterDetails();
                        // if (resultArgs.Success)
                        // {
                        // Changed Stock to Asset
                        dtXlData = ReadMultiExcelData("'Opening Asset" + "$'");
                        AcMELog.WriteLog("Import Asset OP : Read Asset Details from Excel File..");
                        if (dtXlData != null && dtXlData.Rows.Count > 0)
                        {
                            // Newly Inserted to get the count

                            AssetOPCount = AssetItemCount = dtXlData.Rows.Count;

                            resultArgs = ImportAssetOpBalance(dtXlData);
                        }
                        else
                        {
                            txtSelectedFile.Text = string.Empty;
                            txtSelectedFile.Focus();
                        }
                        //  }
                        CloseWaitDialog();
                    }
                    else
                    {
                        //if (resultArgs.Success)
                        //{
                        //    ShowWaitDialog();
                        //    resultArgs = ImportAssetMasterDetails();
                        //    CloseWaitDialog();
                        //}
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetImport.ASSET_IMPORT_FILE_NOT_EXITS_INFO));
                    txtSelectedFile.Text = string.Empty;
                }

                if (!resultArgs.Success)
                {
                    if (!string.IsNullOrEmpty(resultArgs.Message))
                        this.ShowMessageBox(resultArgs.Message);
                }
                else
                {
                    if (AssetItemCount == 0 && AssetOPCount == 0)
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetImport.ASSET_IMPORT_ASSET_DETAILS_EMPTY));
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetImport.ASSET_IMPORT_SUCCESS_INFO));
                    }
                }
            }
            catch (Exception ex)
            {
                CloseWaitDialog();
                if (ex.Source.Contains("Microsoft Office Access Database Engine"))
                {
                    //this.ShowMessageBox("Excel file is opened already close while Importing");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetImport.ASSET_IMPORT_EXCEL_FILE_ALREADY_OPENED));
                }
                else
                {
                    //this.ShowMessageBox("Problem While Importing Asset Opening Stock." + ex.Message);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetImport.ASSET_IMPORT_PROBLEM_WHILE_IMPORT_INFO) + ex.Message);
                    AcMELog.WriteLog(ex.Source + Environment.NewLine + ex.Message);
                }

            }
        }

        /// <summary>
        /// Delete the Transaction which has projects
        /// </summary>
        /// <returns></returns>
        private ResultArgs DeleteAllTransaction()
        {
            using (AssetInwardOutwardSystem assetinwardoutward = new AssetInwardOutwardSystem())
            {
                assetinwardoutward.dtExcelDataSource = dtXlData;
                resultArgs = assetinwardoutward.DeleteAssetTransaction();
            }
            return resultArgs;
        }

        private ResultArgs CheckTransExists()
        {
            using (AssetInwardOutwardSystem assetinwardoutward = new AssetInwardOutwardSystem())
            {
                resultArgs = assetinwardoutward.CheckTransactionExists();
            }
            return resultArgs;
        }

        // To check all the Master is available 02
        private ResultArgs ImportAssetMasterDetails()
        {
            dtXlData = ReadMultiExcelData("'Asset Item" + "$'");
            AcMELog.WriteLog("Import Asset OP : Read Asset Item Details from Excel File..");
            if (dtXlData != null && dtXlData.Rows.Count > 0)
            {
                AssetItemCount = dtXlData.Rows.Count;
                resultArgs = ImportAssetStock(dtXlData);
            }
            else
            {
                //ShowMessageBox("Excel file is opened already. Close while Importing Asset Opening Stock");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetImport.ASSET_IMPORT_EXCEL_FILE_OPEN_CLOSE_INFO));
                txtSelectedFile.Text = string.Empty;
                txtSelectedFile.Focus();
            }
            return resultArgs;
        }

        //asset 3
        private ResultArgs ImportAssetStock(DataTable dtXlData)
        {
            resultArgs.Success = true;
            IEnumerable<DataRow> EnumurableInoutword = dtXlData.Rows.Cast<DataRow>().Where(row => string.IsNullOrEmpty(row["Parent Class"].ToString())
                && string.IsNullOrEmpty(row["Asset Class"].ToString()) ? false : true);
            if (EnumurableInoutword.Count() > 0)
            {
                dtXlData = EnumurableInoutword.CopyToDataTable();

                dtXlData.AsEnumerable().ToList<DataRow>().ForEach(drItems =>
                {
                    if (resultArgs.Success)
                    {
                        using (ExcelSupport excelSupportSystem = new ExcelSupport())
                        {
                            AcMELog.WriteLog("Import Asset Items : Fetch Asset Class Id by Name..");
                            parentClassName = drItems["Parent Class"].ToString();
                            excelSupportSystem.AssetClass = AssetClass = drItems[ASSET_CLASS].ToString();

                            if (!string.IsNullOrEmpty(excelSupportSystem.AssetClass) && !string.IsNullOrEmpty(parentClassName))
                            {
                                //if (parentClassName != "")
                                //{
                                ClassID = excelSupportSystem.FetchAssetClassID(excelSupportSystem.AssetClass, parentClassName);
                                //}
                                //else
                                //{
                                //    MessageBox.Show("Test");
                                //}
                                //}

                            }
                            else if (string.IsNullOrEmpty(excelSupportSystem.AssetClass) && !string.IsNullOrEmpty(parentClassName))
                            {
                                ClassID = (excelSupportSystem.FetchAssetParentClassID(parentClassName)) == 0 ? (int)FixedAssetClass.Primary : ClassID;
                                AssetClass = parentClassName;
                            }
                            else
                            {
                                if (parentClassName == "")
                                {
                                    MessageBox.Show("Parent Class is Empty or Mismatched");
                                }
                            }

                            excelSupportSystem.ItemName = drItems[ASSET_ITEM].ToString();
                            if (!string.IsNullOrEmpty(excelSupportSystem.ItemName) && ClassID > 0)
                            {
                                AcMELog.WriteLog("Import Asset OP : Fetch Asset Item Id by Name..");
                                excelSupportSystem.Prefix = drItems[PREFIX].ToString();
                                excelSupportSystem.Suffix = drItems[SUFFIX].ToString();
                                excelSupportSystem.DepreciationYrs = 0; // UtilityMember.NumberSet.ToInteger(drItems[DEPRECIATION_YRS].ToString());
                                excelSupportSystem.RetentionYrs = 0;   // UtilityMember.NumberSet.ToInteger(drItems[RETENSIONYRS].ToString());
                                excelSupportSystem.AMCApplicable = 0; // drItems[AMCAPPLICABLE].ToString() == YesNo.Yes.ToString() ? 1 : 0;
                                excelSupportSystem.InsuranceApplicable = 0; //drItems[INSURANCEAPPLICABLE].ToString() == YesNo.Yes.ToString() ? 1 : 0;

                                resultArgs = excelSupportSystem.FetchAssetItemID(AssetClass, excelSupportSystem.ItemName, parentClassName);
                                if (resultArgs.Success)
                                {
                                    using (AssetItemSystem assetItemSystem = new AssetItemSystem())
                                    {
                                        assetItemSystem.Name = excelSupportSystem.ItemName;
                                        ItemID = assetItemSystem.FetchAssetItemIdByName();

                                        if (ItemID == 0)
                                        {
                                            resultArgs.Success = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                });
            }
            return resultArgs;
        }

        #region Methods

        private DataTable ReadMultiExcelData(string name)
        {
            DataTable dt = new DataTable();

            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            OleDbConnection conn = IsConnection();
            DataTable dtExcelSchema;
            cmdExcel.Connection = conn;
            conn.Open();
            dtExcelSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            if (dtExcelSchema != null && dtExcelSchema.Rows.Count > 0)
            {
                foreach (DataRow dr in dtExcelSchema.Rows)
                {
                    DataRow drSheetname = dr;
                    string nm = drSheetname["TABLE_NAME"].ToString();
                    if (nm.Equals(name))
                    {
                        cmdExcel.CommandText = String.Format("SELECT * From [{0}]", nm);
                        oda.SelectCommand = cmdExcel;
                        oda.Fill(dt);
                    }
                }
            }
            else
            {
                ShowMessageBox(this.GetMessage(MessageCatalog.Common.INVALID_SHEET));
            }
            conn.Close();

            return dt;
        }

        private OleDbConnection IsConnection()
        {
            OleDbConnection conn = new OleDbConnection();
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0'";
            if (!string.IsNullOrEmpty(FileName))
            {
                conStr = String.Format(conStr, FileName);
                conn = new OleDbConnection(conStr);
            }
            return conn;
        }

        // Import Opening Balance 03
        public ResultArgs ImportAssetOpBalance(DataTable dtData)
        {
            resultArgs.Success = true;
            int rowcount = 0;
            DataTable dtFilteredRows = new DataTable();
            try
            {
                dtXlData = dtData;
                using (ExcelSupport excelSupportSystem = new ExcelSupport())
                {
                    if (dtXlData != null && dtXlData.Rows.Count > 0)
                    {
                        AcMELog.WriteLog("Import Asset OP : Checking All the columns exists in the datatable..");
                        if (dtXlData.Columns.Contains(ASSET_ITEM) && dtXlData.Columns.Contains(PROJECT) && dtXlData.Columns.Contains(ASSETID))
                        {
                            // added by chinna 04/10/2024
                            AssetOPCount = AssetItemCount = dtXlData.Rows.Count;
                            AcMELog.WriteLog("Import Asset OP : Converting the excel data into acme.erp format started..");
                            //  dtXLtoAcME = ConstructExcelData(dtXlData);
                            dtXLtoAcME = ConstructExcelData1(dtXlData);
                            AcMELog.WriteLog("Import Asset OP : Converting the excel data into acme.erp format ended..");
                            if (dtXLtoAcME != null && dtXLtoAcME.Rows.Count > 0 && resultArgs.Success)
                            {
                                dtXLtoAcMEInOut = dtXLtoAcME.Clone();
                                int prvVal = 0;
                                string ProjIDs = string.Empty;
                                string PrevProjIDs = string.Empty;

                                string ClassIDs = string.Empty;
                                string PrevClassIDs = string.Empty;

                                AcMELog.WriteLog("Import Asset OP : Obtaining Project IDs..");
                                dtXLtoAcME.AsEnumerable().ToList<DataRow>().ForEach(drItems =>
                                {
                                    if (PrevProjIDs != drItems["PROJECT_ID"].ToString())
                                    {
                                        ProjIDs += drItems["PROJECT_ID"].ToString() + ",";
                                        PrevProjIDs = drItems["PROJECT_ID"].ToString();
                                    }

                                    if (PrevClassIDs != drItems["CLASS_ID"].ToString())
                                    {
                                        ClassIDs += drItems["CLASS_ID"].ToString() + ",";
                                        PrevClassIDs = drItems["CLASS_ID"].ToString();
                                    }
                                });

                                ProjIDs = ProjIDs.TrimEnd(',');
                                ClassIDs = ClassIDs.TrimEnd(',');
                                string[] projIDCollection = ProjIDs.Split(',');
                                string[] ClassIDCollection = ClassIDs.Split(',');
                                ProjectID = 0;


                                dtFilteredRows = new DataTable();

                                foreach (string prjId in projIDCollection)
                                {
                                    dtXLtoAcMEInOut.Clear();
                                    DataView dvInOut = dtXLtoAcME.AsDataView();
                                    dvInOut.RowFilter = "PROJECT_ID=" + prjId + "";

                                    if (dvInOut != null && dvInOut.ToTable().Rows.Count > 0)
                                    {
                                        dvInOut.Sort = "ITEM_ID ASC";
                                        dtFilteredRows = dvInOut.ToTable();
                                        AcMELog.WriteLog("Import Asset OP : Obtaining In Out Details from the Excel File to acme ...");
                                        foreach (DataRow drItm in dtFilteredRows.Rows)
                                        {
                                            dvInOut.Sort = "ITEM_ID ASC";
                                            dtFilteredRows = dvInOut.ToTable();
                                            if (prvVal != UtilityMember.NumberSet.ToInteger(drItm["ITEM_ID"].ToString()))
                                            {
                                                DataRow dr = dtXLtoAcMEInOut.NewRow();
                                                Amount = UtilityMember.NumberSet.ToDecimal(dtFilteredRows.Compute("SUM(AMOUNT)", "PROJECT_ID=" + prjId + " AND " + "ITEM_ID = " + drItm["ITEM_ID"].ToString()).ToString());
                                                DepreciationAmount = UtilityMember.NumberSet.ToDecimal(dtFilteredRows.Compute("SUM(DEPRECIATION_AMOUNT)", "PROJECT_ID=" + prjId + " AND " + "ITEM_ID = " + drItm["ITEM_ID"].ToString()).ToString());
                                                Quantity = dtXLtoAcME.Select("PROJECT_ID=" + prjId + "AND " + "ITEM_ID=" + UtilityMember.NumberSet.ToInteger(drItm["ITEM_ID"].ToString())).Length;

                                                dr["PROJECT_ID"] = UtilityMember.NumberSet.ToInteger(drItm["PROJECT_ID"].ToString());
                                                dr["ITEM_ID"] = UtilityMember.NumberSet.ToInteger(drItm["ITEM_ID"].ToString());
                                                dr["QUANTITY"] = Quantity > 0 ? Quantity : 1;
                                                dr["AMOUNT"] = Amount;
                                                dr["DEPRECIATION_AMOUNT"] = DepreciationAmount;
                                                dr["IN_OUT_DETAIL_ID"] = 0;
                                                dr["CLASS_ID"] = UtilityMember.NumberSet.ToInteger(drItm["CLASS_ID"].ToString()); ;
                                                dtXLtoAcMEInOut.Rows.Add(dr);
                                            }
                                            prvVal = UtilityMember.NumberSet.ToInteger(drItm["ITEM_ID"].ToString());
                                        }
                                        // if it is same asset item (assetid) for different projects, 
                                        // it does not save the items rather it comes out from the loop which we save opening Balances
                                        // so make it zero 09/09/2024 - Chinna
                                        prvVal = 0;
                                    }

                                    ProjectID = UtilityMember.NumberSet.ToInteger(prjId);
                                    rowcount = 0;
                                    dtFilteredRows = new DataTable();

                                    SettingProperty.AssetListCollection.Clear();
                                    foreach (DataRow drItemDetail in dtXLtoAcMEInOut.Rows)
                                    {
                                        ClassID = UtilityMember.NumberSet.ToInteger(drItemDetail["CLASS_ID"].ToString());
                                        DataView dvProjects = dtXLtoAcMEItemDetail.AsDataView();
                                        ItemID = UtilityMember.NumberSet.ToInteger(drItemDetail[ITEMID].ToString());
                                        dvProjects.RowFilter = "PROJECT_ID=" + ProjectID + " AND ITEM_ID =" + ItemID + "  AND CLASS_ID =" + ClassID + "";
                                        if (dvProjects != null && dvProjects.ToTable().Rows.Count > 0)
                                        {
                                            dtFilteredRows = dvProjects.ToTable();

                                            if (SettingProperty.AssetListCollection.ContainsKey(rowcount))
                                                SettingProperty.AssetListCollection.Remove(rowcount);

                                            SettingProperty.AssetListCollection[rowcount] = dtFilteredRows;
                                        }
                                        dvProjects.RowFilter = string.Empty;
                                        rowcount++;
                                    }
                                    this.CloseWaitDialog();
                                    frmAssetItemLedgerMapping frmitemmapping = new frmAssetItemLedgerMapping();
                                    frmitemmapping.ShowDialog();

                                    resultArgs = SaveImportOPBalance(dtXLtoAcMEInOut);
                                    if (!resultArgs.Success) break;
                                }
                            }
                            else
                            {
                               // MessageRender.ShowMessage(resultArgs.Message);
                                AcMELog.WriteLog(resultArgs.Message);
                                this.CloseWaitDialog();
                            }
                        }
                        else
                        {
                            //MessageRender.ShowMessage("Mandatory Columns does not exists..");
                            MessageRender.ShowMessage(this.GetMessage(MessageCatalog.Asset.AssetImport.ASSET_IMPORT_MADATORY_FIELD_DOESNOT_EXITS)); AcMELog.WriteLog("Mandatory Columns does not exists..");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Source + Environment.NewLine + ex.Message);
                AcMELog.WriteLog(ex.Source + Environment.NewLine + ex.Message);
            }
            return resultArgs;
        }

        /// <summary>
        ///  old to construct the methods
        /// </summary>
        /// <param name="dtData"></param>
        /// <returns></returns>
        public DataTable ConstructExcelData(DataTable dtData)
        {
            AcMELog.WriteLog("Import Asset OP : Construct excel data started..");
            DataTable dtAssettempSource = ConstructEmptyOPStructure();
            DataTable dtAssettempItemDetail = GenerateAssetItemStructure();

            IEnumerable<DataRow> EnumurableInoutword = dtData.Rows.Cast<DataRow>().Where(row => string.IsNullOrEmpty(row["Project"].ToString()) ? false : true);
            if (EnumurableInoutword.Count() > 0)
            {
                dtXlData = EnumurableInoutword.CopyToDataTable();

                dtXlData.AsEnumerable().ToList<DataRow>().ForEach(drItems =>
                {
                    if (resultArgs.Success)
                    {
                        using (ExcelSupport excelSupportSystem = new ExcelSupport())
                        {
                            excelSupportSystem.ProjectName = drItems[PROJECT].ToString();
                            AcMELog.WriteLog("Import Asset OP : Fetch Project Id by Name..");
                            ProjectID = excelSupportSystem.GetId(ID.ProjectId);
                            if (ProjectID > 0)
                            {
                                excelSupportSystem.ItemName = drItems[ASSET_ITEM].ToString();
                                if (!string.IsNullOrEmpty(excelSupportSystem.ItemName))
                                {
                                    AcMELog.WriteLog("Import Asset OP : Fetch Asset Item Id by Name..");
                                    using (AssetItemSystem assetItemSystem = new AssetItemSystem())
                                    {
                                        assetItemSystem.Name = excelSupportSystem.ItemName.Replace("'", "");
                                        ItemID = assetItemSystem.FetchAssetItemIdByName();

                                        if (ItemID > 0)
                                        {
                                            Quantity = 1;
                                            Amount = UtilityMember.NumberSet.ToDecimal(drItems["VALUE"].ToString()); // AMOUNT
                                            DepreciationAmount = UtilityMember.NumberSet.ToDecimal(drItems["Depreciation"].ToString()); // DeprAMOUNT
                                            AssetId = drItems[ASSETID].ToString();

                                            Blockname = drItems[BLOCK].ToString();

                                            ManufactureName = drItems[MANUFACTURER].ToString();
                                            if (!string.IsNullOrEmpty(ManufactureName))
                                            {
                                                ManufactureID = excelSupportSystem.FetchManufacturerID(ManufactureName);
                                            }
                                            excelSupportSystem.CustudianName = drItems[CUSTODIAN].ToString();
                                            //  CustodianID = excelSupportSystem.FetchCustodianID(excelSupportSystem.CustudianName);
                                            excelSupportSystem.Role = drItems[ROLE].ToString();
                                            CustodianID = excelSupportSystem.FetchCustodianID(excelSupportSystem.CustudianName, excelSupportSystem.Role);// 

                                            if (CustodianID != 0)
                                            {
                                                excelSupportSystem.Name = drItems[LOCATION].ToString();
                                                excelSupportSystem.Locationtype = drItems[DESCRIPTION].ToString();
                                                LocationID = excelSupportSystem.FetchLocationID(excelSupportSystem.Name, Blockname, CustodianID, excelSupportSystem.Locationtype, ProjectID); // Included the Project Id to Map it
                                            }
                                            if (ItemID != 0 && ClassID != 0 && ProjectID != 0)
                                            {
                                                if (!ExcelcheckIdExists(ItemID, AssetId, dtAssettempItemDetail, ProjectID))
                                                {
                                                    // In Out detail Generation
                                                    DataRow dr = dtAssettempSource.NewRow();
                                                    dr["PROJECT_ID"] = ProjectID;
                                                    dr["CLASS_ID"] = ClassID;
                                                    dr["ITEM_ID"] = ItemID;
                                                    dr["QUANTITY"] = Quantity;
                                                    dr["RATE"] = Amount / Quantity;
                                                    dr["AMOUNT"] = Amount;
                                                    dr["DEPRECIATION_AMOUNT"] = DepreciationAmount;
                                                    dr["IN_OUT_DETAIL_ID"] = 0;
                                                    dtAssettempSource.Rows.Add(dr);

                                                    // Item Detail  Generation
                                                    DataRow drItem = dtAssettempItemDetail.NewRow();
                                                    drItem["PROJECT_ID"] = ProjectID;
                                                    drItem["CLASS_ID"] = ClassID;
                                                    drItem["ITEM_ID"] = ItemID;
                                                    drItem["ITEM_DETAIL_ID"] = 0;
                                                    drItem["SELECT"] = 0;
                                                    drItem["ASSET_ID"] = AssetId;
                                                    drItem["LOCATION_ID"] = LocationID;
                                                    drItem["CUSTODIAN_ID"] = CustodianID;
                                                    drItem["ID"] = ManufactureID;
                                                    drItem["AMOUNT"] = Amount;
                                                    drItem["DEPRECIATION_AMOUNT"] = DepreciationAmount;
                                                    drItem["STATUS"] = 1;
                                                    dtAssettempItemDetail.Rows.Add(drItem);
                                                }
                                            }
                                            else
                                            {
                                                AcMELog.WriteLog(resultArgs.Message.ToString());
                                                resultArgs.Success = false;
                                                resultArgs.Message = "Problem While importing Location and Custodian Details";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    AcMELog.WriteLog(drItems[ASSET_CLASS].ToString() + "" + "has skipped, Class does not exists");
                                    //  WriteMigrationSummary(drItems[ASSET_CLASS].ToString() + "" + "has skipped, Class does not exists");
                                }
                            }
                            else
                            {
                                AcMELog.WriteLog("Importing Opening Stock has stopped." + drItems[PROJECT].ToString() + "" + ". Project does not exists");
                                resultArgs.Success = false;
                                resultArgs.Message = "Importing Opening Stock stopped." + drItems[PROJECT].ToString() + "" + ". Project does not exists";
                                //  WriteMigrationSummary(drItems[ASSET_CLASS].ToString() + "" + "has skipped, Class does not exists");
                            }
                        }
                    }
                });
            }
            dtXLtoAcMEItemDetail = dtAssettempItemDetail;
            AcMELog.WriteLog("Import Asset OP : Construct excel data ended..");
            return dtAssettempSource;
        }

        /// <summary>
        ///  above old method changed to new methods to contruct the data
        /// </summary>
        /// <param name="dtData"></param>
        /// <returns></returns>
        public DataTable ConstructExcelData1(DataTable dtData)
        {
            AcMELog.WriteLog("Import Asset OP : Construct excel data started..");
            DataTable dtAssettempSource = ConstructEmptyOPStructure();
            DataTable dtAssettempItemDetail = GenerateAssetItemStructure();

            IEnumerable<DataRow> EnumurableInoutword = dtData.Rows.Cast<DataRow>().Where(row => string.IsNullOrEmpty(row["Project"].ToString()) ? false : true);
            if (EnumurableInoutword.Count() > 0)
            {
                dtXlData = EnumurableInoutword.CopyToDataTable();

                foreach (DataRow drItems in dtXlData.Rows)
                {
                    using (ExcelSupport excelSupportSystem = new ExcelSupport())
                    {
                        excelSupportSystem.ProjectName = drItems[PROJECT].ToString();
                        AcMELog.WriteLog("Import Asset OP : Fetch Project Id by Name..");
                        ProjectID = excelSupportSystem.GetId(ID.ProjectId);
                        if (ProjectID > 0)
                        {
                            excelSupportSystem.ItemName = drItems[ASSET_ITEM].ToString();
                            if (!string.IsNullOrEmpty(excelSupportSystem.ItemName))
                            {
                                AcMELog.WriteLog("Import Asset OP : Fetch Asset Item Id by Name..");
                                using (AssetItemSystem assetItemSystem = new AssetItemSystem())
                                {
                                    assetItemSystem.Name = excelSupportSystem.ItemName.Replace("'", "");
                                    ItemID = assetItemSystem.FetchAssetItemIdByName();

                                    if (ItemID > 0)
                                    {
                                        Quantity = 1;
                                        Amount = UtilityMember.NumberSet.ToDecimal(drItems["VALUE"].ToString()); // AMOUNT
                                        DepreciationAmount = UtilityMember.NumberSet.ToDecimal(drItems["Depreciation"].ToString()); // DeprAMOUNT
                                        AssetId = drItems[ASSETID].ToString();

                                        ClassID = assetItemSystem.FetchClassIdByName();
                                        if (ClassID > 0)
                                        {
                                            Blockname = drItems[BLOCK].ToString();

                                            ManufactureName = drItems[MANUFACTURER].ToString();
                                            if (!string.IsNullOrEmpty(ManufactureName))
                                            {
                                                ManufactureID = excelSupportSystem.FetchManufacturerID(ManufactureName);
                                            }
                                            excelSupportSystem.CustudianName = drItems[CUSTODIAN].ToString();
                                            //  CustodianID = excelSupportSystem.FetchCustodianID(excelSupportSystem.CustudianName);
                                            excelSupportSystem.Role = drItems[ROLE].ToString();
                                            CustodianID = excelSupportSystem.FetchCustodianID(excelSupportSystem.CustudianName, excelSupportSystem.Role);// 

                                            if (CustodianID != 0)
                                            {
                                                excelSupportSystem.Name = drItems[LOCATION].ToString();
                                                excelSupportSystem.Locationtype = drItems[DESCRIPTION].ToString();
                                                LocationID = excelSupportSystem.FetchLocationID(excelSupportSystem.Name, Blockname, CustodianID, excelSupportSystem.Locationtype, ProjectID); // Included the Project Id to Map it
                                            }

                                            if (ItemID != 0 && ClassID != 0 && ProjectID != 0)
                                            {
                                                if (!ExcelcheckIdExists(ItemID, AssetId, dtAssettempItemDetail, ProjectID))
                                                {
                                                    // In Out detail Generation
                                                    DataRow dr = dtAssettempSource.NewRow();
                                                    dr["PROJECT_ID"] = ProjectID;
                                                    dr["CLASS_ID"] = ClassID;
                                                    dr["ITEM_ID"] = ItemID;
                                                    dr["QUANTITY"] = Quantity;
                                                    dr["RATE"] = Amount / Quantity;
                                                    dr["AMOUNT"] = Amount;
                                                    dr["DEPRECIATION_AMOUNT"] = DepreciationAmount;
                                                    dr["IN_OUT_DETAIL_ID"] = 0;
                                                    dtAssettempSource.Rows.Add(dr);

                                                    // Item Detail  Generation
                                                    DataRow drItem = dtAssettempItemDetail.NewRow();
                                                    drItem["PROJECT_ID"] = ProjectID;
                                                    drItem["CLASS_ID"] = ClassID;
                                                    drItem["ITEM_ID"] = ItemID;
                                                    drItem["ITEM_DETAIL_ID"] = 0;
                                                    drItem["SELECT"] = 0;
                                                    drItem["ASSET_ID"] = AssetId;
                                                    drItem["LOCATION_ID"] = LocationID;
                                                    drItem["CUSTODIAN_ID"] = CustodianID;
                                                    drItem["ID"] = ManufactureID;
                                                    drItem["AMOUNT"] = Amount;
                                                    drItem["DEPRECIATION_AMOUNT"] = DepreciationAmount;
                                                    drItem["STATUS"] = 1;
                                                    dtAssettempItemDetail.Rows.Add(drItem);
                                                }
                                            }
                                            else
                                            {
                                                AcMELog.WriteLog(resultArgs.Message.ToString());
                                                resultArgs.Success = false;
                                                resultArgs.Message = "Problem While importing Location and Custodian Details";
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            AcMELog.WriteLog("Importing Opening Stock has stopped." + drItems[ASSET_ITEM].ToString() + "" + ".  Asset Item class does not exists");
                                            resultArgs.Success = false;
                                            resultArgs.Message = "Importing Opening Stock has stopped." + drItems[ASSET_ITEM].ToString() + "" + ". Asset Item class does not exists, please check the Items";
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        AcMELog.WriteLog("Importing Opening Stock has stopped." + drItems[ASSET_ITEM].ToString() + "" + ". Asset Item does not exists");
                                        resultArgs.Success = false;
                                        resultArgs.Message = "Importing Opening Stock stopped." + drItems[ASSET_ITEM].ToString() + "" + ". Asset Item does not exists";
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                AcMELog.WriteLog(drItems[ASSET_CLASS].ToString() + "" + "has skipped, Class does not exists");
                                //  WriteMigrationSummary(drItems[ASSET_CLASS].ToString() + "" + "has skipped, Class does not exists");
                            }
                        }
                        else
                        {
                            AcMELog.WriteLog("Importing Opening Stock has stopped." + drItems[PROJECT].ToString() + "" + ". Project does not exists");
                            resultArgs.Success = false;
                            resultArgs.Message = "Importing Opening Stock stopped." + drItems[PROJECT].ToString() + "" + ". Project does not exists";
                            break;
                            //  WriteMigrationSummary(drItems[ASSET_CLASS].ToString() + "" + "has skipped, Class does not exists");
                        }

                    }

                }

                //dtXlData.AsEnumerable().ToList<DataRow>().ForEach(drItems =>
                //{
                //    if (resultArgs.Success)
                //    {
                //        using (ExcelSupport excelSupportSystem = new ExcelSupport())
                //        {
                //            excelSupportSystem.ProjectName = drItems[PROJECT].ToString();
                //            AcMELog.WriteLog("Import Asset OP : Fetch Project Id by Name..");
                //            ProjectID = excelSupportSystem.GetId(ID.ProjectId);
                //            if (ProjectID > 0)
                //            {
                //                excelSupportSystem.ItemName = drItems[ASSET_ITEM].ToString();
                //                if (!string.IsNullOrEmpty(excelSupportSystem.ItemName))
                //                {
                //                    AcMELog.WriteLog("Import Asset OP : Fetch Asset Item Id by Name..");
                //                    using (AssetItemSystem assetItemSystem = new AssetItemSystem())
                //                    {
                //                        assetItemSystem.Name = excelSupportSystem.ItemName.Replace("'", "");
                //                        ItemID = assetItemSystem.FetchAssetItemIdByName();

                //                        if (ItemID > 0)
                //                        {
                //                            Quantity = 1;
                //                            Amount = UtilityMember.NumberSet.ToDecimal(drItems["VALUE"].ToString()); // AMOUNT
                //                            DepreciationAmount = UtilityMember.NumberSet.ToDecimal(drItems["Depreciation"].ToString()); // DeprAMOUNT
                //                            AssetId = drItems[ASSETID].ToString();

                //                            Blockname = drItems[BLOCK].ToString();

                //                            ManufactureName = drItems[MANUFACTURER].ToString();
                //                            if (!string.IsNullOrEmpty(ManufactureName))
                //                            {
                //                                ManufactureID = excelSupportSystem.FetchManufacturerID(ManufactureName);
                //                            }
                //                            excelSupportSystem.CustudianName = drItems[CUSTODIAN].ToString();
                //                            //  CustodianID = excelSupportSystem.FetchCustodianID(excelSupportSystem.CustudianName);
                //                            excelSupportSystem.Role = drItems[ROLE].ToString();
                //                            CustodianID = excelSupportSystem.FetchCustodianID(excelSupportSystem.CustudianName, excelSupportSystem.Role);// 

                //                            if (CustodianID != 0)
                //                            {
                //                                excelSupportSystem.Name = drItems[LOCATION].ToString();
                //                                excelSupportSystem.Locationtype = drItems[DESCRIPTION].ToString();
                //                                LocationID = excelSupportSystem.FetchLocationID(excelSupportSystem.Name, Blockname, CustodianID, excelSupportSystem.Locationtype, ProjectID); // Included the Project Id to Map it
                //                            }
                //                            if (ItemID != 0 && ClassID != 0 && ProjectID != 0)
                //                            {
                //                                if (!ExcelcheckIdExists(ItemID, AssetId, dtAssettempItemDetail, ProjectID))
                //                                {
                //                                    // In Out detail Generation
                //                                    DataRow dr = dtAssettempSource.NewRow();
                //                                    dr["PROJECT_ID"] = ProjectID;
                //                                    dr["CLASS_ID"] = ClassID;
                //                                    dr["ITEM_ID"] = ItemID;
                //                                    dr["QUANTITY"] = Quantity;
                //                                    dr["RATE"] = Amount / Quantity;
                //                                    dr["AMOUNT"] = Amount;
                //                                    dr["DEPRECIATION_AMOUNT"] = DepreciationAmount;
                //                                    dr["IN_OUT_DETAIL_ID"] = 0;
                //                                    dtAssettempSource.Rows.Add(dr);

                //                                    // Item Detail  Generation
                //                                    DataRow drItem = dtAssettempItemDetail.NewRow();
                //                                    drItem["PROJECT_ID"] = ProjectID;
                //                                    drItem["CLASS_ID"] = ClassID;
                //                                    drItem["ITEM_ID"] = ItemID;
                //                                    drItem["ITEM_DETAIL_ID"] = 0;
                //                                    drItem["SELECT"] = 0;
                //                                    drItem["ASSET_ID"] = AssetId;
                //                                    drItem["LOCATION_ID"] = LocationID;
                //                                    drItem["CUSTODIAN_ID"] = CustodianID;
                //                                    drItem["ID"] = ManufactureID;
                //                                    drItem["AMOUNT"] = Amount;
                //                                    drItem["DEPRECIATION_AMOUNT"] = DepreciationAmount;
                //                                    drItem["STATUS"] = 1;
                //                                    dtAssettempItemDetail.Rows.Add(drItem);
                //                                }
                //                            }
                //                            else
                //                            {
                //                                AcMELog.WriteLog(resultArgs.Message.ToString());
                //                                resultArgs.Success = false;
                //                                resultArgs.Message = "Problem While importing Location and Custodian Details";
                //                            }
                //                        }
                //                    }
                //                }
                //                else
                //                {
                //                    AcMELog.WriteLog(drItems[ASSET_CLASS].ToString() + "" + "has skipped, Class does not exists");
                //                    //  WriteMigrationSummary(drItems[ASSET_CLASS].ToString() + "" + "has skipped, Class does not exists");
                //                }
                //            }
                //            else
                //            {
                //                AcMELog.WriteLog("Importing Opening Stock has stopped." + drItems[PROJECT].ToString() + "" + ". Project does not exists");
                //                resultArgs.Success = false;
                //                resultArgs.Message = "Importing Opening Stock stopped." + drItems[PROJECT].ToString() + "" + ". Project does not exists";
                //                //  WriteMigrationSummary(drItems[ASSET_CLASS].ToString() + "" + "has skipped, Class does not exists");
                //            }
                //        }
                //    }
                //});
            }
            dtXLtoAcMEItemDetail = dtAssettempItemDetail;
            AcMELog.WriteLog("Import Asset OP : Construct excel data ended..");
            return dtAssettempSource;
        }

        private DataTable ConstructEmptyOPStructure()
        {
            DataTable dtPurchaseVouhcerDetail = new DataTable();
            dtPurchaseVouhcerDetail.Columns.Add("PROJECT_ID", typeof(int));
            dtPurchaseVouhcerDetail.Columns.Add("CLASS_ID", typeof(int));
            dtPurchaseVouhcerDetail.Columns.Add("ITEM_ID", typeof(int));
            dtPurchaseVouhcerDetail.Columns.Add("QUANTITY", typeof(int));
            dtPurchaseVouhcerDetail.Columns.Add("AVAILABLE_QUANTITY", typeof(int));
            dtPurchaseVouhcerDetail.Columns.Add("RATE", typeof(decimal));
            dtPurchaseVouhcerDetail.Columns.Add("AMOUNT", typeof(decimal));
            dtPurchaseVouhcerDetail.Columns.Add("DEPRECIATION_AMOUNT", typeof(decimal));
            dtPurchaseVouhcerDetail.Columns.Add("IN_OUT_DETAIL_ID", typeof(int));
            return dtPurchaseVouhcerDetail;
        }


        /// <summary>
        /// Create empty structure of Asset Items to be generated
        /// </summary>
        /// <returns></returns>
        private DataTable GenerateAssetItemStructure()
        {
            DataTable dtAssetItemStructure = new DataTable();
            dtAssetItemStructure = new DataTable(ASSET_TABLE_NAME);
            dtAssetItemStructure.Columns.Add("PROJECT_ID", typeof(int));
            dtAssetItemStructure.Columns.Add("CLASS_ID", typeof(Int32));
            dtAssetItemStructure.Columns.Add("ITEM_ID", typeof(Int32));
            dtAssetItemStructure.Columns.Add("ITEM_DETAIL_ID", typeof(Int32));
            dtAssetItemStructure.Columns.Add("SELECT", typeof(Int32));
            dtAssetItemStructure.Columns.Add("ASSET_ID", typeof(string));
            dtAssetItemStructure.Columns.Add("LOCATION_ID", typeof(Int32));
            dtAssetItemStructure.Columns.Add("CUSTODIAN_ID", typeof(Int32));
            dtAssetItemStructure.Columns.Add("ID", typeof(Int32)); //manufacture Id
            dtAssetItemStructure.Columns.Add("AMOUNT", typeof(decimal));
            dtAssetItemStructure.Columns.Add("DEPRECIATION_AMOUNT", typeof(decimal));
            dtAssetItemStructure.Columns.Add("STATUS", typeof(Int32));
            dtAssetItemStructure.Rows.Add();
            return dtAssetItemStructure;
        }

        private ResultArgs SaveImportOPBalance(DataTable dtData)
        {
            DataTable dtFilteredRows = new DataTable();
            try
            {
                using (AssetInwardOutwardSystem inwardvouchersystem = new AssetInwardOutwardSystem())
                {
                    inwardvouchersystem.InoutId = 0;
                    inwardvouchersystem.ProjectId = ProjectID;
                    inwardvouchersystem.InOutDate = this.UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false);
                    inwardvouchersystem.Flag = AssetInOut.OP.ToString();
                    inwardvouchersystem.Status = 1;  //Active

                    dtFilteredRows = dtData; //dtxltoacme.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull)).CopyToDataTable();
                    inwardvouchersystem.TotalAmount = UtilityMember.NumberSet.ToDouble(dtXLtoAcME.Compute("SUM(AMOUNT)", string.Empty).ToString());
                    inwardvouchersystem.TotalDepreciationAmount = UtilityMember.NumberSet.ToDouble(dtXLtoAcME.Compute("SUM(DEPRECIATION_AMOUNT)", string.Empty).ToString());
                    inwardvouchersystem.dtinoutword = dtFilteredRows;

                    // It is enabled for deleting the opening Balances while importing (11/09/2024)
                    // Chinna
                    resultArgs = inwardvouchersystem.DeleteImportAssetOPDetails(true);
                    if (resultArgs.Success)
                    {
                        resultArgs = inwardvouchersystem.SaveAssetInwardOutward();
                    }
                    else
                    {
                        MessageRender.ShowMessage(resultArgs.Message.ToString());
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message + System.Environment.NewLine + Ex.Source);
            }
            finally
            {
                SettingProperty.AssetListCollection.Clear();
                SettingProperty.AssetInsuranceCollection.Clear();
            }
            return resultArgs;
        }

        public bool ExcelcheckIdExists(int ItmID, string AssID, DataTable dtData, int PrID)
        {
            bool isexists = false;

            var ItemDetailId = dtData.AsEnumerable().Where(drm => drm.Field<string>("ASSET_ID") == AssID && drm.Field<int>("ITEM_ID") == ItmID && drm.Field<int>("PROJECT_ID") == PrID);
            if (ItemDetailId.Count() > 0)
            {
                DataTable dtAssetDetailTemp = ItemDetailId.CopyToDataTable();
                if (dtAssetDetailTemp != null && dtAssetDetailTemp.Rows.Count > 0)
                {
                    isexists = true;
                    //WriteMigrationSummary(AssID + " skipped.This Asset ID Exists already in the given excel");
                }
            }
            return isexists;
        }

        public void WriteMigrationSummary(string Message)
        {
            try
            {
                MigrationLog += Message + "  " + Environment.NewLine;
            }
            catch (Exception e)
            {
                MessageRender.ShowMessage(e.Message);
            }
        }

        #endregion
    }
}