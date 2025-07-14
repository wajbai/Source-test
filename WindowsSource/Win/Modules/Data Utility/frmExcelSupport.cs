using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using Bosco.Utility;
using Bosco.DAO;
using Bosco.Model.UIModel.Master;
using Bosco.Model;
using Bosco.Model.Donor;


namespace ACPP.Modules.Data_Utility
{
    public partial class frmExcelSupport : frmFinanceBaseAdd
    {

        #region Variables
        string SheetName = string.Empty;
        public event EventHandler UpdateHeld;
        ResultArgs resultArgs = new ResultArgs();
        DataTable dtCustodian = new DataTable();
        string CUSTODIAN_NAME = "CUSTODIAN NAME";
        string MANUFACTURER_NAME = "MANUFACTURER NAME";
        string VENDOR_NAME = "VENDOR NAME";
        string COUNTRY = "COUNTRY";
        string VOUCHER_DATE = "VOUCHER_DATE";
        DataTable dtXlData;
        #endregion

        #region Properties
        string FileName { get; set; }
        MasterImport FrmName { get; set; }
        FinanceModule Module { get; set; }
        AssetStockProduct.IGroup Igroup { get; set; }
        #endregion

        #region Constructor
        public frmExcelSupport()
        {
            InitializeComponent();
        }

        public frmExcelSupport(string SheetName, MasterImport frmName)
            : this()
        {
            this.SheetName = SheetName + "$";
            this.FrmName = frmName;
            this.Text = "Import " + this.FrmName + "";
            lblCaption.Text = this.FrmName.ToString();
        }

        public frmExcelSupport(string SheetName, MasterImport frmName, FinanceModule module)
            : this()
        {
            this.SheetName = SheetName + "$";
            this.FrmName = frmName;
            this.Module = module;
            this.Text = "Import " + this.FrmName + "";
            lblCaption.Text = this.FrmName.ToString();
        }

        #endregion

        #region Events
        private void btnDownload_Click(object sender, EventArgs e)
        {
            // ConstructDataSet();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog() { Filter = "Excel Files|*.xls;*.xlsx;*.xlsm" };

            if (DialogResult.OK == file.ShowDialog())
            {
                if (!string.IsNullOrEmpty(file.FileName))
                {
                    txtPath.Text = file.FileName;
                    txtPath_Leave(sender, e);
                }
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(txtPath.Text))
                {
                    if (System.IO.File.Exists(txtPath.Text))
                    {
                        FileName = txtPath.Text;
                        DataTable dtExcelValue = new DataTable();
                        if (MasterImport.Item == FrmName)
                        {
                            ShowWaitDialog();
                            using (ExcelSupport ExcelSupportsystem = new ExcelSupport())
                            {
                                dtXlData = ReadMultiExcelData(MasterImport.Item.ToString() + '$');
                                if (dtXlData != null && dtXlData.Rows.Count > 0)
                                {
                                    ExcelSupportsystem.Igroup = Igroup;
                                    ExcelSupportsystem.dtItems = dtXlData;

                                    if (FinanceModule.Asset.Equals(Module))
                                    {
                                        resultArgs = ExcelSupportsystem.ImportAssetItemDetails();
                                    }
                                }
                                else
                                {
                                    CloseWaitDialog();
                                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.EMPTY_SHEET));
                                    txtPath.Text = string.Empty;
                                    txtPath.Focus();
                                }
                                if (resultArgs.Success)
                                {
                                    CloseWaitDialog();
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                                }
                            }
                        }
                        else if (MasterImport.Donor == FrmName)
                        {
                            ImportDonorDetails();
                        }
                        else if (MasterImport.Prospects == FrmName)
                        {
                            ImportProspectsDetails();
                        }
                        else if (MasterImport.Transaction == FrmName)
                        {
                            ImportTransactionDetails();
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.DataSynchronization.ImportExcel.FILE_NOT_EXIT));
                        txtPath.Text = string.Empty;
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.DataSynchronization.Import.FILE_NAME_EMPTY));
                    txtPath.Focus();
                }
            }
            catch (Exception ex)
            {
                CloseWaitDialog();
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
        }

        private void ImportLocationDetails(EventArgs e)
        {
            DataTable dtXlData = ReadMultiExcelData(SheetName);
            if (dtXlData != null && dtXlData.Rows.Count > 0)
            {
                using (ExcelSupport Excelupportsystem = new ExcelSupport())
                {
                    Excelupportsystem.dtLocation = dtXlData;
                    resultArgs = Excelupportsystem.ImportAssetLocation();
                    if (resultArgs.Success)
                    {
                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                        if (UpdateHeld != null)
                            UpdateHeld(this, e);
                    }
                }
            }
            else
            {
                ShowMessageBox(this.GetMessage(MessageCatalog.DataSynchronization.ImportExcel.LOCATION_NOT_AVAIL));
            }
        }

        private void ImportGroupDetails(EventArgs e)
        {
            resultArgs.Success = true;
            try
            {
                dtXlData = ReadMultiExcelData(SheetName);
                if (dtXlData != null && dtXlData.Rows.Count > 0)
                {
                    using (ExcelSupport ExcelSupportsystem = new ExcelSupport())
                    {
                        ExcelSupportsystem.dtGroup = dtXlData;
                        ExcelSupportsystem.module = this.Module;
                        ExcelSupportsystem.Igroup = Igroup;
                        DialogResult result = ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_IMPORT_CONFIRMATION) + Environment.NewLine + "Yes: Data will get Append " + Environment.NewLine + " No: Data will get Overwrite", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.No)
                        {
                            ShowWaitDialog();
                            resultArgs = ExcelSupportsystem.DeleteGroupDetails();
                            if (resultArgs.Success)
                                resultArgs = ExcelSupportsystem.ImportGroupDetails();
                            CloseWaitDialog();
                        }
                        if (result == DialogResult.Yes)
                        {
                            ShowWaitDialog();
                            if (resultArgs.Success)
                                resultArgs = ExcelSupportsystem.ImportGroupDetails();
                        }
                        if (result == DialogResult.Cancel)
                        {
                            resultArgs.Success = false;
                        }
                        if (resultArgs.Success)
                        {
                            CloseWaitDialog();
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            if (UpdateHeld != null)
                                UpdateHeld(this, e);
                        }
                    }
                }
                else
                {
                    ShowMessageBox(this.GetMessage(MessageCatalog.DataSynchronization.ImportExcel.GROUP_NOT_AVAIL));
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
                AcMELog.WriteLog(ex.Source + Environment.NewLine + ex.Message);
            }
            finally
            {
                CloseWaitDialog();
            }
        }

        private void ImportItemDetails(EventArgs e)
        {
            try
            {
                resultArgs.Success = true;
                DataTable dtStockItem = ReadMultiExcelData(SheetName);
                if (dtStockItem != null && dtStockItem.Rows.Count > 0)
                {
                    using (ExcelSupport ExcelSupport = new ExcelSupport())
                    {
                        ExcelSupport.Igroup = Igroup;
                        ExcelSupport.dtItems = dtStockItem;
                        DialogResult result = ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_IMPORT_CONFIRMATION) + Environment.NewLine + "Yes: Data will get Append " + Environment.NewLine + " No: Data will get Overwrite", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.No)
                        {
                            ShowWaitDialog();
                            if (FinanceModule.Stock.Equals(Module))
                            {
                                resultArgs = ExcelSupport.DeleteStockItemDetails();
                                if (resultArgs.Success)
                                    resultArgs = ExcelSupport.ImportStockItemDetails();
                            }
                            else if (FinanceModule.Asset.Equals(Module))
                            {
                                resultArgs = ExcelSupport.DeleteAssetItemDetails();
                                if (resultArgs.Success)
                                    resultArgs = ExcelSupport.ImportAssetItemDetails();
                            }
                            CloseWaitDialog();
                        }
                        if (result == DialogResult.Yes)
                        {
                            ShowWaitDialog();
                            if (FinanceModule.Stock.Equals(Module))
                            {
                                resultArgs = ExcelSupport.ImportStockItemDetails();
                            }
                            else if (FinanceModule.Asset.Equals(Module))
                            {
                                resultArgs = ExcelSupport.ImportAssetItemDetails();
                            }
                        }
                        if (result == DialogResult.Yes)
                        {
                            resultArgs.Success = false;
                        }
                        if (resultArgs.Success)
                        {
                            CloseWaitDialog();
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            if (UpdateHeld != null)
                                UpdateHeld(this, e);
                        }
                    }
                }
                else
                {
                    ShowMessageBox(this.GetMessage(MessageCatalog.DataSynchronization.ImportExcel.ITEM__NOT_AVAIL));
                }
            }
            catch (Exception ex)
            {
                AcMELog.WriteLog(ex.Message + Environment.NewLine + ex.Source);
                ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally
            {
                CloseWaitDialog();
            }
        }

        private void ImportTransactionDetails()
        {
            try
            {
                //this.ShowWaitDialog("Importing Transactions...");
                this.ShowWaitDialog(this.GetMessage(MessageCatalog.Master.DataUtilityForms.IMPORTING_TRANSACTIONS));
                DataTable dtVoucherTrans = ReadMultiExcelData(MasterImport.Voucher.ToString() + '$');

                if (dtVoucherTrans != null && dtVoucherTrans.Rows.Count > 0 && dtVoucherTrans !=null)
                {
                    //dtVoucherTrans.Columns["VOUCHER DATE"].ColumnName = "VOUCHER_DATE";
                    //dtVoucherTrans.AcceptChanges();

                    string DateFrom = dtVoucherTrans.Compute("MIN(VOUCHER_DATE)", String.Empty).ToString();
                    string DateTo = dtVoucherTrans.Compute("MAX(VOUCHER_DATE)", String.Empty).ToString();
                    bool CanOverwrite = chkOverride.Checked ? true : false;

                    string overwritemsg = "Overwritting will remove all the old records for the duration between '" + Convert.ToDateTime(DateFrom).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(DateTo).ToString("dd/MMM/yyyy") + "'. Do you really want to overwrite ?";
                    if (chkOverride.Checked ? ShowConfirmationMessage(overwritemsg, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning).Equals(DialogResult.OK) :
                        ShowConfirmationMessage(GetMessage(MessageCatalog.DataSynchronization.Import.APPEND_CONFORM), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning).Equals(DialogResult.OK))
                    {
                        if (dtVoucherTrans.Columns.Contains(VOUCHER_DATE))
                        {
                            using (ExcelSupport donorExcelSupport = new ExcelSupport())
                            {
                                donorExcelSupport.dtDonor = dtVoucherTrans;

                                donorExcelSupport.CanOverwrite = CanOverwrite;
                                donorExcelSupport.DateFrom = DateFrom.ToString();
                                donorExcelSupport.DateTo = DateTo.ToString();
                                resultArgs = donorExcelSupport.ImportDonorTransaction();

                                if (resultArgs.Success)
                                {
                                    this.CloseWaitDialog();
                                    if (UpdateHeld != null)
                                    {
                                        UpdateHeld(null, new EventArgs());
                                    }
                                    //this.ShowSuccessMessage("Transactions are imported successfully");
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Master.DataUtilityForms.TRANSACTION_IMPORTING_SUCCESS));
                                }
                                else
                                {
                                    this.CloseWaitDialog();
                                    this.ShowMessageBoxError(resultArgs.Message);
                                }
                            }
                        }
                    }
                    else
                    {
                        this.CloseWaitDialog();
                        //this.ShowMessageBox("The Excel file is invalid. It does not contain Donor details");
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.DataUtilityForms.INVALID_EXCEL_FILE));
                    }
                }
                else
                {
                    this.CloseWaitDialog();
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.EMPTY_SHEET));
                }
            }
            catch (Exception ex)
            {
                this.CloseWaitDialog();
                ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        private void ImportDonorDetails()
        {
            try
            {
                //this.ShowWaitDialog("Importing Donors...");
                this.ShowWaitDialog(this.GetMessage(MessageCatalog.Master.DataUtilityForms.IMPORTING_DONORS));
                DataTable dtDonor = ReadMultiExcelData(MasterImport.Donor.ToString() + '$');
                if (dtDonor != null && dtDonor.Rows.Count > 0)
                {
                    if (dtDonor.Columns.Contains(COUNTRY))
                    {
                        using (ExcelSupport donorExcelSupport = new ExcelSupport())
                        {
                            donorExcelSupport.dtDonor = dtDonor;
                            resultArgs = donorExcelSupport.ImportDonor();

                            if (resultArgs.Success)
                            {
                                this.CloseWaitDialog();
                                if (UpdateHeld != null)
                                {
                                    UpdateHeld(null, new EventArgs());
                                }
                                //this.ShowSuccessMessage("Donor details imported successfully");
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Master.DataUtilityForms.DONOR_IMPORTING_SUCCESS));
                            }
                            else
                            {
                                this.CloseWaitDialog();
                                this.ShowMessageBoxError(resultArgs.Message);
                            }
                        }
                    }
                    else
                    {
                        this.CloseWaitDialog();
                        //this.ShowMessageBox("The Excel file is invalid. It does not contain Donor details");
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.DataUtilityForms.INVALID_EXCEL_FILE));
                    }
                }
                else
                {
                    this.CloseWaitDialog();
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.EMPTY_SHEET));
                }
            }
            catch (Exception ex)
            {
                this.CloseWaitDialog();
                ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
                AcMELog.WriteLog(ex.Message + Environment.NewLine + ex.Source);
            }
            finally
            {
                this.CloseWaitDialog();
            }
        }

        /// <summary>
        /// to get prospects details
        /// </summary>
        private void ImportProspectsDetails()
        {
            try
            {
                //this.ShowWaitDialog("Importing Prospects...");
                this.ShowWaitDialog(this.GetMessage(MessageCatalog.Master.DataUtilityForms.IMPORTING_PROSPECTS));
                DataTable dtProspects = ReadMultiExcelData(MasterImport.Prospects.ToString() + '$');
                if (dtProspects != null && dtProspects.Rows.Count > 0)
                {
                    if (dtProspects.Columns.Contains(COUNTRY))
                    {
                        using (ExcelSupport donorExcelSupport = new ExcelSupport())
                        {
                            donorExcelSupport.dtProspects = dtProspects;
                            resultArgs = donorExcelSupport.ImportProspects();
                            if (resultArgs.Success)
                            {
                                this.CloseWaitDialog();
                                if (UpdateHeld != null)
                                {
                                    UpdateHeld(null, new EventArgs());
                                }
                                //this.ShowSuccessMessage("Prospects details imported successfully");
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Master.DataUtilityForms.IMPORTING_PROSPECTS_SUCCESS));
                            }
                            else
                            {
                                this.CloseWaitDialog();
                                this.ShowMessageBox(resultArgs.Message);
                            }
                        }
                    }
                    else
                    {
                        this.CloseWaitDialog();
                        //this.ShowMessageBox("The Excel file is invalid. It does not contain Donor details");
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.DataUtilityForms.INVALID_EXCEL_FILE));
                    }
                }
                else
                {
                    this.CloseWaitDialog();
                    //this.ShowMessageBox("Prospects list is empty to import");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.DataUtilityForms.PROSPECTS_EMPTY_lIST));
                }
            }
            catch (Exception ex)
            {
                this.CloseWaitDialog();
                ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
                AcMELog.WriteLog(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        private void ImportCustodainDetails(EventArgs e)
        {
            try
            {
                ShowWaitDialog();
                DataTable dtCustodian = ReadMultiExcelData(SheetName);
                if (dtCustodian.Rows.Count > 0)
                {
                    using (ExcelSupport donorExcelSupport = new ExcelSupport())
                    {
                        donorExcelSupport.dtCustodian = dtCustodian;
                        resultArgs = donorExcelSupport.ImportCustodianDetails();
                        if (resultArgs.Success)
                        {
                            CloseWaitDialog();
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.DataSynchronization.ImportExcel.CUSTODIAN_IMPORT_SUCCESSFULLY));
                            if (UpdateHeld != null)
                                UpdateHeld(this, e);
                        }
                    }
                }
                else
                {
                    CloseWaitDialog();
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.DataSynchronization.ImportExcel.CUSTODIAN_NOT_AVAIL));
                }
            }
            catch (Exception ex)
            {
                CloseWaitDialog();
                AcMELog.WriteLog(ex.Source + Environment.NewLine + ex.Message);
                ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
            finally
            {
                CloseWaitDialog();
            }
        }

        private void ImportManufactureDetails(EventArgs e)
        {
            try
            {
                ShowWaitDialog();
                DataTable dtManufacture = ReadMultiExcelData(SheetName);
                if (dtManufacture != null && dtManufacture.Rows.Count > 0)
                {
                    using (ExcelSupport ExcelSupportsystem = new ExcelSupport())
                    {
                        ExcelSupportsystem.DtDetails = dtManufacture;
                        resultArgs = ExcelSupportsystem.ImportManufactureDetails();
                        if (resultArgs.Success)
                        {
                            CloseWaitDialog();
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            if (UpdateHeld != null)
                                UpdateHeld(this, e);
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.DataSynchronization.ImportExcel.MANUFACTURER_NOT_AVAIL));
                }
            }
            catch (Exception ex)
            {
                AcMELog.WriteLog(ex.Message + Environment.NewLine + ex.Source);
                ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally
            {
                CloseWaitDialog();
            }
        }

        private void ImportVendorDetails(EventArgs e)
        {
            try
            {
                ShowWaitDialog();
                DataTable dtVendor = ReadMultiExcelData(SheetName);
                if (dtVendor != null && dtVendor.Rows.Count > 0)
                {
                    if (dtVendor.Columns.Contains(VENDOR_NAME))
                    {
                        using (ExcelSupport ExcelSupportsystem = new ExcelSupport())
                        {
                            ExcelSupportsystem.DtDetails = dtVendor;
                            resultArgs = ExcelSupportsystem.ImportVendorDetails();
                            if (resultArgs.Success)
                            {
                                CloseWaitDialog();
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                                if (UpdateHeld != null)
                                    UpdateHeld(this, e);
                            }
                        }
                    }
                    else
                    {
                        CloseWaitDialog();
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.DataSynchronization.ImportExcel.VENDOR_NOT_AVAIL));
                    }
                }
                else
                {
                    CloseWaitDialog();
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.DataSynchronization.ImportExcel.VENDOR_NOT_AVAIL));
                }
            }
            catch (Exception ex)
            {
                CloseWaitDialog();
                AcMELog.WriteLog(ex.Message + Environment.NewLine + ex.Source);
                ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally
            {
                CloseWaitDialog();
            }
        }

        #endregion

        #region Methods
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

        private DataTable ReadMultiExcelData(string SheetName)
        {
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            bool IsSheetNameValid = false;
            try
            {
                OleDbConnection conn = IsConnection();
                DataTable dtExcelSchema;
                cmdExcel.Connection = conn;
                conn.Open();
                dtExcelSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (dtExcelSchema != null && dtExcelSchema.Rows.Count > 0)
                {
                    foreach (DataRow drSheet in dtExcelSchema.Rows)
                    {
                        string ExcelSheetName = drSheet["TABLE_NAME"].ToString();
                        if (ExcelSheetName.Equals(SheetName))
                        {
                            IsSheetNameValid = true;
                            cmdExcel.CommandText = String.Format("SELECT * From [{0}]", SheetName);
                            oda.SelectCommand = cmdExcel;
                            oda.Fill(dt);
                            break;
                        }
                    }
                }
                else
                {
                    ShowMessageBox(this.GetMessage(MessageCatalog.Common.INVALID_SHEET));
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }
            return dt;
        }
        #endregion

        private void txtPath_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtPath);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}