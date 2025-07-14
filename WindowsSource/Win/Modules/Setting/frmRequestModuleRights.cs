using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Threading;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.XtraBars.Ribbon;

using Bosco.Utility;
using Bosco.DAO.Schema;
using Bosco.Model;
using Bosco.Model.UIModel;
using Bosco.Model.Setting;
using DevExpress.XtraGrid.Views.Grid;
using ACPP.Modules.TDS;
using Bosco.Model.TDS;
using Bosco.Utility.ConfigSetting;
using System.IO;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace ACPP.Modules.Master
{
    public partial class frmRequestModuleRights : frmFinanceBaseAdd
    {
        #region Declaration
        ResultArgs resultArgs = null;

        DataTable dtSetting = null;
        public DataTable dtUISetting = null;
        #endregion

        #region Property
        private static DataView dvSettingTemp = null;
        public DataView SettingInfoTemp
        {
            set
            {
                dvSettingTemp = value;
            }
            get
            {
                return dvSettingTemp;
            }
        }

        private static DataView dvUISettingTemp = null;
        public DataView UISettingInfoTemp
        {
            set
            {
                dvUISettingTemp = value;
            }
            get
            {
                return dvUISettingTemp;
            }
        }

        private int negativeValue = 0;
        private int NegativeValue
        {
            get
            {
                return negativeValue;
            }
            set
            {
                negativeValue = value;
            }
        }

        private int positiveValue = 0;
        private int PositiveValue
        {
            get
            {
                return positiveValue;
            }
            set
            {
                positiveValue = value;
            }
        }

        private int accperiodYearId = 0;
        private int AccperiodYearId
        {
            get
            {
                return accperiodYearId;
            }
            set
            {
                accperiodYearId = value;
            }
        }

        private string yearFrom = "";
        private string YearFrom
        {
            get
            {
                return yearFrom;
            }
            set
            {
                yearFrom = value;
            }
        }

        private string booksBeginningFrom = "";
        private string BooksBeginningFrom
        {
            get
            {
                return booksBeginningFrom;
            }
            set
            {
                booksBeginningFrom = value;
            }
        }

        private string yearTo = "";
        private string YearTo
        {
            get
            {
                return yearTo;
            }
            set
            {
                yearTo = value;
            }
        }
        #endregion

        #region Constructor
        public frmRequestModuleRights()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void frmSettings_Load(object sender, EventArgs e)
        {
            this.PageTitle = "Receipt Module Rights";
            //On 25/03/2022, To show Request Receipt Module
            lcReceiptModuleRightsDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lgRequestEnableReceiptModule.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcIPMACAddress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (this.AppSetting.IS_SDB_INM)
            {
                lblBaseBranchCode.Text = AppSetting.BaseLicenseBranchCode;
                lblBaseBranchName.Text = AppSetting.BaseLicenseBranchName;
                lblBaseDatabaseLocation.Text = AppSetting.BaseLicenseLocation;

                lblIPMACAddress.Text = this.GetIPAddress() + " - ";
                lblIPMACAddress.Text += this.GetMACAddress(); 

                lgRequestEnableReceiptModule.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                LCBranchModuleStatus branchmodulestatus = LCBranchModuleStatus.Disabled;
                if (this.AppSetting.BaseLicenseBranchStatus == LCBranchModuleStatus.Disabled ||
                    this.AppSetting.BaseLicenseBranchStatus == LCBranchModuleStatus.Requested ||
                    this.AppSetting.BaseLicenseBranchStatus == LCBranchModuleStatus.Approved)
                {
                    branchmodulestatus = this.AppSetting.BaseLicenseBranchStatus;
                    if (this.AppSetting.BaseLicenseBranchStatus== this.AppSetting.FINAL_RECEIPT_MODULE_STATUS)
                    {   //DB and acp key checking both are same
                        branchmodulestatus = this.AppSetting.FINAL_RECEIPT_MODULE_STATUS;
                    }
                    else //DB and acp key checking both are not same
                    {
                        //IF DB status is approved it, make it disabled
                        if (branchmodulestatus == LCBranchModuleStatus.Approved)
                        {
                            branchmodulestatus = LCBranchModuleStatus.Disabled;
                            this.AppSetting.FINAL_RECEIPT_MODULE_STATUS = branchmodulestatus;
                        }
                        else //IF DB status is not approved it, take the db status value
                        {
                            this.AppSetting.FINAL_RECEIPT_MODULE_STATUS = branchmodulestatus;
                        }
                    }
                }
                lblReceiptModuleStatus.Text = "Receipt Module Status :: " + branchmodulestatus.ToString();

                //For multi db enabled license alone will be acted
                if (AppSetting.AccesstoMultiDB > 0)
                {
                    if (AppSetting.BaseLicenseBranchCode != AppSetting.BranchOfficeCode || AppSetting.IsSplitPreviousYearAcmeerpDB)
                    {
                        lcBtnReceiptModuleStatus.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    }
                    if (branchmodulestatus != LCBranchModuleStatus.Approved)
                    {
                        lblReceiptModuleStatus.Text += " (Base License Key)";
                    }
                }
                
                //On 14/06/2022, If already approved, hide request button
                if (lcBtnReceiptModuleStatus.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always && this.AppSetting.FINAL_RECEIPT_MODULE_STATUS==LCBranchModuleStatus.Approved)
                {
                    lcBtnReceiptModuleStatus.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
            }

        }

      

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            // this.AppSetting.SettingInfo = SettingInfoTemp;

            //this.Close();
        }

        private void btnRequestEnableReceipt_Click(object sender, EventArgs e)
        {
            if (this.ShowConfirmationMessage("Are you sure to connect Acme.erp portal and make request/enable Receipt Module, Acme.erp will be restarted ? ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                ResultArgs result = this.RequestReceiptModule();

                if (result.Success && result.ReturnValue != null)
                {
                    LCBranchModuleStatus lcbranchModuleStatus = (LCBranchModuleStatus)result.ReturnValue;
                    if (String.IsNullOrEmpty(result.Message) && lcbranchModuleStatus == LCBranchModuleStatus.Approved)
                    {
                        this.ShowMessageBox("Successfully enabled Receipts Moudle, all the Receipts Vouchers will be tracked.");
                        lblReceiptModuleStatus.Text = "Receipt Module Status :: " + LCBranchModuleStatus.Approved.ToString();
                        SettingProperty.Is_Application_Logout = true;
                        Application.Restart();
                    }
                    else
                    {
                        lblReceiptModuleStatus.Text = "Receipt Module Status :: " + lcbranchModuleStatus.ToString();
                        this.ShowMessageBoxError(result.Message);

                        //Restart Acmeerp if and if only Disabled or Approved
                        if (lcbranchModuleStatus != LCBranchModuleStatus.Requested)
                        {
                            SettingProperty.Is_Application_Logout = true;
                            Application.Restart();
                        }
                    }
                }
                else
                {
                    this.ShowMessageBoxError(result.Message);
                }
            }
        }
        #endregion

        private void lblReceiptModuleNote_DoubleClick(object sender, EventArgs e)
        {
            string refcode = string.Empty;

            if (lcRequestOffline.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            {
                DialogResult diaresult = InputBox("Offline - Approve Receipt Module", "Enter password to approve Receipt Module", ref refcode, true);
                if (diaresult == System.Windows.Forms.DialogResult.OK)
                {
                    if (refcode != "acmerc*007")
                    {
                        this.ShowMessageBoxWarning("Invalid password to approve Receipt Module, Please try again.");
                    }
                    else if (refcode == "acmerc*007")
                    {
                        lcIPMACAddress.Visibility = (lcIPMACAddress.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always ? DevExpress.XtraLayout.Utils.LayoutVisibility.Never : DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
                        txtModuleRightsDetails.Text = this.AppSetting.ModuleRightsDetails;
                        lcReceiptModuleRightsDetails.Visibility = (lcReceiptModuleRightsDetails.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always ? DevExpress.XtraLayout.Utils.LayoutVisibility.Never : DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
                        lcRequestOffline.Visibility = (lcRequestOffline.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always ? DevExpress.XtraLayout.Utils.LayoutVisibility.Never : DevExpress.XtraLayout.Utils.LayoutVisibility.Always);


                        //For multi db enabled license alone will be acted //On 14/06/2022, If already approved, hide request button
                        if (AppSetting.AccesstoMultiDB > 0)
                        {
                            if (AppSetting.BaseLicenseBranchCode != AppSetting.BranchOfficeCode)
                            {
                                lcBtnReceiptModuleStatus.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                            }
                        }
                        else
                        {
                            lcBtnReceiptModuleStatus.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        }

                    }
                }
            }
            else
            {
                lcReceiptModuleRightsDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcRequestOffline.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcIPMACAddress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                
            }
        }

        private void btnRequestOffline_Click(object sender, EventArgs e)
        {
            //this.ShowMessageBoxError("Yet to be implemented");
            //On 22/01/2024, For offline temporarily
            if (this.ShowConfirmationMessage("Are you sure to approve Receipt Module for temporarily (This login session time alone) ? ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                string refcode = string.Empty;
                DialogResult diaresult = InputBox("Offline - Approve Receipt Module for temporarily (This login session time alone)", "Enter password to approve Receipt Module", ref refcode, true);
                if (diaresult == System.Windows.Forms.DialogResult.OK)
                {
                    if (refcode != "acmerc*007")
                    {
                        this.ShowMessageBoxWarning("Invalid password to approve Receipt Module, Please try again.");
                    }
                    else if (refcode == "acmerc*007")
                    {
                        this.AppSetting.FINAL_RECEIPT_MODULE_STATUS = LCBranchModuleStatus.Approved;
                        this.ShowMessageBoxWarning("Receipt Module is enabled for temporarily (This Login sesssion time alone)");
                    }
                }
            }


            /*string file = Path.Combine(Application.StartupPath, SettingProperty.AcmeerpLCFile);
            try
            {
                if (this.ShowConfirmationMessage("Are you sure to approve Receipt Module by Offline ? ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {

                    string refcode = string.Empty;
                    DialogResult diaresult = InputBox("Offline - Approve Receipt Module", "Enter password to approve Receipt Module", ref refcode, true);
                    if (diaresult == System.Windows.Forms.DialogResult.OK)
                    {
                        if (refcode != "acmerc*007")
                        {
                            this.ShowMessageBoxWarning("Invalid password to approve Receipt Module, Please try again.");
                        }
                        else if (refcode == "acmerc*007")
                        {
                            string ipaddress = this.GetIPAddress();
                            string macaddress = this.GetMACAddress();
                            string requestcode = "1";

                            ResultArgs result = CreateLocalCommunityEnableModuleKey(requestcode, this.AppSetting.BaseLicensekeyNumber, this.AppSetting.BaseLicenseHeadOfficeCode, this.AppSetting.BaseLicenseBranchCode, this.AppSetting.BaseLicenseLocation, ipaddress, macaddress);
                            if (result.Success)
                            {
                                if (File.Exists(file))
                                {
                                    result = UpdateLCdetails1(requestcode, this.AppSetting.LicenseKeyNumber, this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode,
                                                        this.AppSetting.Location, ipaddress, macaddress);
                                    if (result.Success)
                                    {
                                        result = UpdateLCdetails2(requestcode, this.AppSetting.LicenseKeyNumber, this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode,
                                                        this.AppSetting.Location, ipaddress, macaddress);
                                        if (result.Success)
                                        {
                                            result.ReturnValue = LCBranchModuleStatus.Approved;

                                            //To update Recent Module rights
                                            using (UISettingSystem uisystemsetting = new UISettingSystem())
                                            {
                                                uisystemsetting.BaseAcmeerpSaveUISettingDetails(FinanceSetting.BranchReceiptModuleStatus, ((Int32)LCBranchModuleStatus.Approved).ToString(), this.LoginUser.LoginUserId);
                                            }
                                        }
                                    }
                                }
                            }

                            if (result.Success)
                            {
                                this.ShowMessageBox("Successfully enabled Receipts Moudle, all the Receipts Vouchers will be tracked.");
                                lblReceiptModuleStatus.Text = "Receipt Module Status :: " + LCBranchModuleStatus.Approved.ToString();
                                SettingProperty.Is_Application_Logout = true;
                                Application.Restart();
                            }
                            else
                            {
                                this.ShowMessageBox(result.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                this.ShowMessageBoxError(err.Message);
            }*/
        
        }


        #region Methods
        public static ResultArgs CreateLocalCommunityEnableModuleKey(string licenserequestcode, string licensekey, string hocode, string bocode, string location,
                    string clientip, string clientmacaddress)
        {
            string file = Path.Combine(Application.StartupPath, SettingProperty.AcmeerpLCFile);
            ResultArgs resultarg = new ResultArgs();
            SimpleEncrypt.SimpleEncDec objDec = new SimpleEncrypt.SimpleEncDec();
            try
            {
                //string dllkeypath = Path.Combine(SettingProperty.ApplicationStartUpPath, licenserequestcode + "_" + hocode + "_" + bocode + "_" + SettingProperty.AcmeerpLCFile);
                //string dllkeypath = Path.Combine(Application.StartupPath, licenserequestcode + "_" + hocode + "_" + bocode + "_" + SettingProperty.AcmeerpLCFile);

                if (File.Exists(file))
                {
                    File.Delete(file);
                }

                CSharpCodeProvider provider = new CSharpCodeProvider(new Dictionary<String, String> { { "CompilerVersion", "v4.0" } });
                System.CodeDom.Compiler.CompilerParameters cparameters = new CompilerParameters();
                cparameters.GenerateExecutable = false;
                //cparameters.CompilerOptions= " /out:D:\\Temp\\" + dllname;
                //cparameters.CompilerOptions = " /out:" + PagePath.MultilicensekeySettingFileName + @"\LC\" + hocode + "_" + bocode + "_" + dllnameLC;
                cparameters.CompilerOptions = " /out:" + file; // SettingProperty.AcmeerpLCFile;
                cparameters.OutputAssembly = file; //SettingProperty.AcmeerpLCFile;
                string Licensecode = objDec.EncryptString(objDec.EncryptString(licenserequestcode));
                string Licensekey = objDec.EncryptString(objDec.EncryptString(licensekey));
                string Hocode = objDec.EncryptString(objDec.EncryptString(hocode));
                string Bocode = objDec.EncryptString(objDec.EncryptString(bocode));
                string Location = objDec.EncryptString(objDec.EncryptString(location));
                string Clientip = objDec.EncryptString(objDec.EncryptString(clientip));
                string Clientmacaddress = objDec.EncryptString(objDec.EncryptString(clientmacaddress));

                // provider is an instance of CodeDomProvider
                //cparameters.ReferencedAssemblies.Add("System.Xml.dll");
                //if (provider.Supports(GeneratorSupport.Resources))
                //{
                //    cparameters.EmbeddedResources.Add("c:\\t.txt");
                //}

                cparameters.TempFiles.Delete();
                string keyClass = string.Empty;
                StringBuilder strbuilder = new StringBuilder();
                //strbuilder.AppendLine("using System.Xml;");
                strbuilder.AppendLine("namespace Acme.erpLC");
                strbuilder.AppendLine("{");
                strbuilder.AppendLine("public class AcmeerpLC");
                strbuilder.AppendLine("{");
                strbuilder.AppendLine("public static string ref1=\"" + Licensecode + "\";");
                strbuilder.AppendLine("public static string ref2=\"" + Licensekey + "\";");
                strbuilder.AppendLine("public static string ref3=\"" + Hocode + "\";");
                strbuilder.AppendLine("public static string ref4=\"" + Bocode + "\";");
                strbuilder.AppendLine("public static string ref5=\"" + Location + "\";");
                strbuilder.AppendLine("public static string ref6=\"" + Clientip + "\";");
                strbuilder.AppendLine("public static string ref7=\"" + Clientmacaddress + "\";");
                strbuilder.AppendLine("}");
                strbuilder.AppendLine("}");

                keyClass = strbuilder.ToString();
                CompilerResults compilerResult = provider.CompileAssemblyFromSource(cparameters, keyClass);
                if (compilerResult.Errors.Count > 0)
                {
                    resultarg.Message = "Not generated";
                    resultarg.ReturnValue = false;
                    resultarg.Success = false;
                }
                else
                {
                    resultarg.Message = "generated";
                    resultarg.Success = true;
                    resultarg.ReturnValue = true;
                }
            }
            catch (Exception ex)
            {
                resultarg.Success = false;
                resultarg.ReturnValue = false;
                resultarg.Message = ex.Message;
            }

            return resultarg;
        }

        #endregion

        

        
    }
}



/*DataTable dtOfflineRequest = new DataTable("ReceiptModuleOfflineRequest");
                SimpleEncrypt.SimpleEncDec objDec = new SimpleEncrypt.SimpleEncDec();
                using (UISetting uiSetting = new UISetting())
                {
                    dtOfflineRequest.Columns.Add(uiSetting.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_LICENSE_KEY_NUMBERColumn.ColumnName, typeof(System.String));
                    dtOfflineRequest.Columns.Add(uiSetting.AppSchema.LcBranchEnableTrackModules.LC_HEAD_OFFICE_CODEColumn.ColumnName, typeof(System.String));
                    dtOfflineRequest.Columns.Add(uiSetting.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_OFFICE_CODEColumn.ColumnName, typeof(System.String));
                    dtOfflineRequest.Columns.Add(uiSetting.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_LOCATIONColumn.ColumnName, typeof(System.String));
                    dtOfflineRequest.Columns.Add(uiSetting.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_CLIENT_IP_ADDRESSColumn.ColumnName, typeof(System.String));
                    dtOfflineRequest.Columns.Add(uiSetting.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_CLIENT_MAC_ADDRESSColumn.ColumnName, typeof(System.String));
                    dtOfflineRequest.Columns.Add(uiSetting.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_REQUESTED_BYColumn.ColumnName, typeof(System.String));
                    dtOfflineRequest.Columns.Add(uiSetting.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_RECEIPT_MODULE_STATUSColumn.ColumnName, typeof(System.String));

                    DataRow dr = dtOfflineRequest.NewRow();
                    dr[uiSetting.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_LICENSE_KEY_NUMBERColumn.ColumnName] = objDec.EncryptString(this.AppSetting.BaseLicensekeyNumber);
                    dr[uiSetting.AppSchema.LcBranchEnableTrackModules.LC_HEAD_OFFICE_CODEColumn.ColumnName] = objDec.EncryptString(this.AppSetting.BaseLicenseHeadOfficeCode);
                    dr[uiSetting.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_OFFICE_CODEColumn.ColumnName] = objDec.EncryptString(this.AppSetting.BaseLicenseBranchCode);
                    dr[uiSetting.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_LOCATIONColumn.ColumnName] = objDec.EncryptString(this.AppSetting.BaseLicenseLocation);
                    dr[uiSetting.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_CLIENT_IP_ADDRESSColumn.ColumnName] = objDec.EncryptString(ipaddress);
                    dr[uiSetting.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_CLIENT_MAC_ADDRESSColumn.ColumnName] = objDec.EncryptString(macaddress);
                    dr[uiSetting.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_REQUESTED_BYColumn.ColumnName] = objDec.EncryptString(this.LoginUser.LoginUserName);
                    dr[uiSetting.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_RECEIPT_MODULE_STATUSColumn.ColumnName] = objDec.EncryptString(this.LoginUser.FINAL_RECEIPT_MODULE_STATUS.ToString());

                    dtOfflineRequest.Rows.Add(dr);

                    SaveFileDialog saveDialog = new SaveFileDialog();
                    saveDialog.Filter = "Xml Files|.xml";
                    //saveDialog.Title = "Export Vouchers";
                    saveDialog.Title = "Request Offline Receipt Module Request";
                    saveDialog.FileName = "OfflineReceiptModuleRequest_" + DateTime.Now.Ticks.ToString() + ".xml";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        DataSet ds = new DataSet("ReceiptModuleOfflineRequest");
                        ds.Tables.Add(dtOfflineRequest);
                        XMLConverter.WriteToXMLFile(ds, saveDialog.FileName);
                    }
                }*/